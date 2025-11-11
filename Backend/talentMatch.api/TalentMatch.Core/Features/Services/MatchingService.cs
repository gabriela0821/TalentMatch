using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using TalentMatch.Core.DTOs.JobMatch.Request;
using TalentMatch.Core.DTOs.JobMatch.Response;
using TalentMatch.Core.Interfaces.Pagination;
using TalentMatch.Core.Interfaces.Repositories;
using TalentMatch.Core.Interfaces.Services;
using TalentMatch.Core.Wrappers;
using TalentMatch.Domain.Entities;
using TalentMatch.Domain.QueryFilters;
using TalentMatch.Infrastructure.Exceptions;

namespace TalentMatch.Core.Features.Services
{
    public class MatchingService : IMatchingService
    {
        #region Attributes

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IPagedList _pagedList;

        #endregion Attributes

        #region Builder

        public MatchingService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, IPagedList pagedList)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _pagedList = pagedList;
        }

        #endregion Builder

        #region CreateMatch

        public async Task<Response<GetJobMatchDtoResponse?>> CreateMatch(CreateJobMatchDtoRequest create)
        {
            try
            {
                var jobPosting = await _unitOfWork.JobPostingRepositoryAsync.GetByIdAsync(create.JobPostingId)
                        ?? throw new CoreException("Vacante no encontrada.");

                var jobSeekers = _unitOfWork.JobSeekerProfileRepositoryAsync.GetAll();

                foreach (var jobSeeker in jobSeekers)
                {
                    var existingMatch = _unitOfWork.JobMatchRepositoryAsync
                        .FindBy(x => x.JobPostingId == create.JobPostingId && x.JobSeekerId == jobSeeker.ProfileId)
                        .FirstOrDefault();

                    if (existingMatch != null)
                        continue;

                    var match = CalculateMatch(jobPosting, jobSeeker);

                    if (match == null)
                    {
                        return new Response<GetJobMatchDtoResponse?>(false, "Se presento un problema al calcular los match.");
                    }

                    await _unitOfWork.JobMatchRepositoryAsync.AddAsync(match.Data!);
                }

                await _unitOfWork.CommitAsync();

                return new Response<GetJobMatchDtoResponse?>(true, "Se realizaron los match correctamente.");
            }
            catch (Exception ex)
            {
                return new Response<GetJobMatchDtoResponse?>(false, $"Error al calcular el match: {ex}");
            }
        }

        #endregion CreateMatch

        #region GetMatches

        public async Task<Response<PaginationResponse<GetJobMatchDtoResponse?>>> GetMatches(MatchesQueryFilter filter)
        {
            try
            {
                var query = await Task.FromResult(_unitOfWork.JobMatchRepositoryAsync
                    .FindBy(x => !filter.JobPostingId.HasValue || x.JobPostingId == filter.JobPostingId, "JobSeekerProfile,JobPosting"));

                if (filter?.MatchId != null && filter?.MatchId > 0)
                {
                    query = query.Where(Job => Job.MatchId == filter.MatchId);
                }
                if (filter?.JobPostingId != null && filter?.JobPostingId > 0)
                {
                    query = query.Where(Job => Job.JobPostingId == filter.JobPostingId);
                }
                if (filter?.JobSeekerId != null && filter?.JobSeekerId > 0)
                {
                    query = query.Where(Job => Job.JobSeekerId == filter.JobSeekerId);
                }
                if (filter?.MatchScore != null && filter?.MatchScore > 0)
                {
                    query = query.Where(Job => Job.MatchScore == filter.MatchScore);
                }
                if (filter?.SkillsScore != null && filter?.SkillsScore > 0)
                {
                    query = query.Where(Job => Job.SkillsScore == filter.SkillsScore);
                }
                if (filter?.ExperienceScore != null && filter?.ExperienceScore > 0)
                {
                    query = query.Where(Job => Job.ExperienceScore == filter.ExperienceScore);
                }
                if (filter?.EducationScore != null && filter?.EducationScore > 0)
                {
                    query = query.Where(Job => Job.EducationScore == filter.EducationScore);
                }
                if (filter?.LocationScore != null && filter?.LocationScore > 0)
                {
                    query = query.Where(Job => Job.LocationScore == filter.LocationScore);
                }
                if (!string.IsNullOrEmpty(filter?.Status))
                {
                    query = query.Where(Job => Job.Status.Equals(filter.Status));
                }
                if (filter?.MatchedAt != null)
                {
                    query = query.Where(Job => Job.MatchedAt == filter.MatchedAt);
                }

                var result = await _pagedList.CreatePagedGenericResponse<JobMatch, GetJobMatchDtoResponse>(query,
                      filter.PageNumber,
                      filter.PageSize,
                      filter.OrderBy!,
                      filter.OrderAsc);

                if (result.Data.Count == 0)
                {
                    return new Response<PaginationResponse<GetJobMatchDtoResponse>>(null, false, null, "No se encuentran coincidencias.", null);
                }

                return new Response<PaginationResponse<GetJobMatchDtoResponse>>(result, true, null, null, "Se encontraron las siguientes coincidencias");
            }
            catch (Exception ex)
            {
                return new Response<PaginationResponse<GetJobMatchDtoResponse?>>(succeeded: false, $"Error al procesar la aplicación: {ex}");
            }
        }

        #endregion GetMatches

        #region UpdateMatchStatus

        public async Task<Response<bool>> UpdateMatchStatus(UpdateJobMatchDtoRequest update)
        {
            try
            {
                var match = await _unitOfWork.JobMatchRepositoryAsync.GetByIdAsync(update.MatchId)
                            ?? throw new CoreException("Match no encontrado.");

                match.Status = update.Status;
                await _unitOfWork.JobMatchRepositoryAsync.UpdateAsync(match);
                await _unitOfWork.CommitAsync();

                return new Response<bool>(true, "Se actualizo correctamente el status del match.");
            }
            catch (Exception ex)
            {
                return new Response<bool>(false, $"Error al actualizar el status: {ex}");
            }
        }

        #endregion UpdateMatchStatus

        #region OtherMethods

        public Response<JobMatch?> CalculateMatch(JobPosting jobPosting, JobSeekerProfile jobSeekerProfile)
        {
            try
            {
                #region Skills

                //Spli Required Skills (varchar)
                var requiredSkills = jobPosting.RequiredSkills?.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToList()
                                ?? new List<string>();

                var userSkills = jobSeekerProfile.Skills?.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToList()
                                  ?? new List<string>();

                //Score de Habilidades
                decimal skillsScore = 0;
                if (requiredSkills.Count > 0)
                {
                    var matching = requiredSkills.Intersect(userSkills, StringComparer.OrdinalIgnoreCase).Count();
                    skillsScore = (decimal)matching / requiredSkills.Count * 100;
                }

                #endregion Skills

                #region Experience

                //Experience Score
                decimal experienceScore = Math.Min(
                    (decimal)jobSeekerProfile.YearsOfExperience / jobPosting.MinExperience * 100,
                    100);

                #endregion Experience

                #region Education

                //Education Score

                Dictionary<string, int> _educationRank = new()
                {
                    { "Bachiller", 1 },
                    { "Técnico", 2 },
                    { "Tecnólogo", 3 },
                    { "Profesional", 4 },
                    { "Especialización", 5 },
                    { "Maestría", 6 },
                    { "Doctorado", 7 }
                };

                int candidateEducationRank = _educationRank.ContainsKey(jobSeekerProfile.EducationLevel)
                    ? _educationRank[jobSeekerProfile.EducationLevel]
                    : 0;

                int requiredEducationRank = _educationRank.ContainsKey(jobPosting.MinEducationLevel)
                    ? _educationRank[jobPosting.MinEducationLevel]
                    : 0;

                decimal educationScore;

                if (requiredEducationRank == 0)
                {
                    educationScore = 100;
                }
                else if (candidateEducationRank >= requiredEducationRank)
                {
                    educationScore = 100;
                }
                else
                {
                    educationScore = (decimal)candidateEducationRank / requiredEducationRank * 100;
                }

                #endregion Education

                #region Location

                //Location Score
                decimal locationScore =
                    jobPosting.Location.Equals(jobSeekerProfile.City, StringComparison.OrdinalIgnoreCase) ? 100 :
                    jobPosting.WorkMode == "Remote" ? 90 : 50;

                #endregion Location

                #region Final Score

                //Score final
                decimal matchScore =
                    (skillsScore * 0.6m) +
                    (experienceScore * 0.25m) +
                    (educationScore * 0.10m) +
                    (locationScore * 0.05m);

                #endregion Final Score

                var match = new JobMatch
                {
                    JobPostingId = jobPosting.JobId,
                    JobSeekerId = jobSeekerProfile.ProfileId,
                    SkillsScore = Math.Round(skillsScore, 2),
                    ExperienceScore = Math.Round(experienceScore, 2),
                    EducationScore = Math.Round(educationScore, 2),
                    LocationScore = Math.Round(locationScore, 2),
                    MatchScore = Math.Round(matchScore, 2),
                    Status = "Pending",
                    MatchedAt = DateTime.UtcNow
                };

                return new Response<JobMatch?>(match, true, null, null, "Se calculo correctamente.");
            }
            catch (Exception ex)
            {
                return new Response<JobMatch?>(null, false, null, null, $"Error al calcular el match: {ex}");
            }
        }

        #endregion OtherMethods
    }
}