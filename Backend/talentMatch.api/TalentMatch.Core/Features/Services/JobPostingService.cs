using AutoMapper;
using Microsoft.Extensions.Configuration;
using TalentMatch.Core.DTOs.ApplicationQuestion.Request;
using TalentMatch.Core.DTOs.JobPosting.Request;
using TalentMatch.Core.DTOs.JobPosting.Response;
using TalentMatch.Core.Interfaces.Pagination;
using TalentMatch.Core.Interfaces.Repositories;
using TalentMatch.Core.Interfaces.Services;
using TalentMatch.Core.Wrappers;
using TalentMatch.Domain.Entities;
using TalentMatch.Domain.QueryFilters;
using TalentMatch.Infrastructure.Exceptions;

namespace TalentMatch.Core.Features.Services
{
    public class JobPostingService : IJobPostingService
    {
        #region Attributes

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IPagedList _pagedList;

        #endregion Attributes

        #region Builder

        public JobPostingService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, IPagedList pagedList)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _pagedList = pagedList;
        }

        #endregion Builder

        #region CreateJobPosting

        public async Task<Response<GetJobPostingDtoResponse?>> CreateJobPosting(CreateJobPostingDtoRequest create)
        {
            try
            {
                var newJobPosting = new JobPosting
                {
                    EmployerId = create.EmployerId,
                    Title = create.Title,
                    Description = create.Description,
                    RequiredSkills = create.RequiredSkills,
                    MinEducationLevel = create.MinEducationLevel,
                    MinExperience = create.MinExperience,
                    Location = create.Location,
                    WorkMode = create.WorkMode,
                    SalaryMin = (decimal)create.SalaryMin,
                    SalaryMax = (decimal)create.SalaryMax,
                    IsActive = true,
                    PostedDate = DateTime.UtcNow
                };

                if (create.Questions != null || create.Questions.Count != 0)
                {
                    foreach (var q in create.Questions)
                    {
                        var question = new ApplicationQuestion
                        {
                            JobPostingId = q.JobPostingId,
                            QuestionText = q.QuestionText,
                            QuestionType = q.QuestionType,
                            IsRequired = q.IsRequired,
                            DisplayOrder = q.DisplayOrder,
                            CreatedAt = DateTime.UtcNow
                        };

                        newJobPosting.ApplicationQuestions.Add(question);
                    }
                }

                await _unitOfWork.JobPostingRepositoryAsync.AddAsync(newJobPosting);
                await _unitOfWork.CommitAsync();

                return new Response<GetJobPostingDtoResponse>(_mapper.Map<GetJobPostingDtoResponse>(newJobPosting));
            }
            catch (Exception ex)
            {
                return new Response<GetJobPostingDtoResponse>(succeeded: false, $"Error al procesar la aplicación: {ex}");
            }
        }

        #endregion CreateJobPosting

        #region CreateQuestions

        public async Task<Response<GetJobPostingDtoResponse?>> CreateQuestions(List<CreateApplicationQuestionDtoRequest> questions)
        {
            try
            {
                if (questions != null && questions.Any())
                {
                    var jobPosting = await Task.FromResult(_unitOfWork.JobPostingRepositoryAsync.FindBy(x => x.JobId == questions.FirstOrDefault().JobPostingId, "ApplicationQuestions").FirstOrDefault());

                    foreach (var q in questions)
                    {
                        var question = new ApplicationQuestion
                        {
                            JobPostingId = q.JobPostingId,
                            QuestionText = q.QuestionText,
                            QuestionType = q.QuestionType,
                            IsRequired = q.IsRequired,
                            DisplayOrder = q.DisplayOrder,
                            CreatedAt = DateTime.UtcNow
                        };
                        jobPosting.ApplicationQuestions.Add(question);
                    }

                    await _unitOfWork.JobPostingRepositoryAsync.UpdateAsync(jobPosting);
                    await _unitOfWork.CommitAsync();

                    var response = _mapper.Map<GetJobPostingDtoResponse>(jobPosting);
                    return new Response<GetJobPostingDtoResponse?>(response);
                }
                else
                {
                    return new Response<GetJobPostingDtoResponse?>(succeeded: false, "No hay preguntas.");
                }
            }
            catch (Exception ex)
            {
                return new Response<GetJobPostingDtoResponse?>(succeeded: false, $"Error al procesar la aplicación: {ex}");
            }
        }

        #endregion CreateQuestions

        #region GetJobPostingById

        public async Task<Response<GetJobPostingDtoResponse?>> GetJobPostingById(int jobId)
        {
            try
            {
                var job = await Task.FromResult(_unitOfWork.JobPostingRepositoryAsync
                    .FindBy(x => x.JobId == jobId, "ApplicationQuestions")
                    .FirstOrDefault());

                if (job == null)
                {
                    throw new CoreException("No se encuentran coincidencias.");
                }

                return new Response<GetJobPostingDtoResponse>(_mapper.Map<GetJobPostingDtoResponse>(job));
            }
            catch (Exception ex)
            {
                return new Response<GetJobPostingDtoResponse>(succeeded: false, $"Error al procesar la aplicación: {ex}");
            }
        }

        #endregion GetJobPostingById

        #region GetJobPostings

        public async Task<Response<PaginationResponse<GetJobPostingDtoResponse?>>> GetJobPostings(JobPostingQueryFilter filter)
        {
            try
            {
                var query = await Task.FromResult(_unitOfWork.JobPostingRepositoryAsync
                    .FindBy(x => x.IsActive == true, "ApplicationQuestions"));

                if (!string.IsNullOrEmpty(filter?.Title))
                {
                    query = query.Where(Job => Job.Title.Contains(filter.Title));
                }
                if (!string.IsNullOrEmpty(filter?.Description))
                {
                    query = query.Where(Job => Job.Description.Contains(filter.Description));
                }
                if (!string.IsNullOrEmpty(filter?.RequiredSkills))
                {
                    query = query.Where(Job => Job.RequiredSkills.Contains(filter.RequiredSkills));
                }
                if (!string.IsNullOrEmpty(filter?.MinEducationLevel))
                {
                    query = query.Where(Job => Job.MinEducationLevel.Contains(filter.MinEducationLevel));
                }
                if (filter?.MinExperience != null && filter?.MinExperience > 0)
                {
                    query = query.Where(Job => Job.MinExperience == filter.MinExperience);
                }
                if (!string.IsNullOrEmpty(filter?.Location))
                {
                    query = query.Where(Job => Job.Location.Contains(filter.Location));
                }
                if (!string.IsNullOrEmpty(filter?.WorkMode))
                {
                    query = query.Where(Job => Job.WorkMode.Contains(filter.WorkMode));
                }
                if (filter?.SalaryMin > 0)
                {
                    query = query.Where(Job => Job.SalaryMin == filter.SalaryMin);
                }
                if (filter?.SalaryMax > 0)
                {
                    query = query.Where(Job => Job.SalaryMax == filter.SalaryMax);
                }
                if (filter?.PostedDate != null)
                {
                    query = query.Where(Job => Job.PostedDate == filter.PostedDate);
                }

                var result = await _pagedList.CreatePagedGenericResponse<JobPosting, GetJobPostingDtoResponse>(query,
                      filter.PageNumber,
                      filter.PageSize,
                      filter.OrderBy!,
                      filter.OrderAsc);

                if (result.Data.Count == 0)
                {
                    return new Response<PaginationResponse<GetJobPostingDtoResponse>>(null, false, null, "No se encuentran coincidencias.", null);
                }

                return new Response<PaginationResponse<GetJobPostingDtoResponse>>(result, true, null, null, "Se encontraron las siguientes coincidencias");
            }
            catch (Exception ex)
            {
                return new Response<PaginationResponse<GetJobPostingDtoResponse?>>(succeeded: false, $"Error al procesar la aplicación: {ex}");
            }
        }

        #endregion GetJobPostings
    }
}