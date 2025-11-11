using AutoMapper;
using Microsoft.Extensions.Configuration;
using TalentMatch.Core.DTOs.Certification.Request;
using TalentMatch.Core.DTOs.JobSeekerProfile.Request;
using TalentMatch.Core.DTOs.JobSeekerProfile.Response;
using TalentMatch.Core.DTOs.WorkExperience.Request;
using TalentMatch.Core.Interfaces.Repositories;
using TalentMatch.Core.Interfaces.Services;
using TalentMatch.Core.Wrappers;
using TalentMatch.Domain.Entities;
using TalentMatch.Infrastructure.Exceptions;

namespace TalentMatch.Core.Features.Services
{
    public class JobSeekerService : IJobSeekerService
    {
        #region Attributes

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        #endregion Attributes

        #region Builder

        public JobSeekerService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        #endregion Builder

        #region CreateCertification

        public async Task<Response<GetJobSeekerProfileDtoResponse?>> CreateCertification(CreateCertificationDtoRequest create)
        {
            try
            {
                var cert = await Task.FromResult(_unitOfWork.CertificationRepositoryAsync
                    .FindBy(x => x.CertificationName.Equals(create.CertificationName) && x.JobSeekerId == create.JobSeekerId)
                    .FirstOrDefault());

                if (cert != null)
                {
                    throw new CoreException("La certificacion ya esta agregada.");
                }

                var newCert = new Certification
                {
                    JobSeekerId = create.JobSeekerId,
                    CertificationName = create.CertificationName,
                    IssuingOrganization = create.IssuingOrganization,
                    IssueDate = create.IssueDate,
                    ExpiryDate = create.ExpiryDate,
                    CredentialUrl = create.CredentialUrl,
                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork.CertificationRepositoryAsync.AddAsync(newCert);
                await _unitOfWork.CommitAsync();

                return new Response<GetJobSeekerProfileDtoResponse>(_mapper.Map<GetJobSeekerProfileDtoResponse>(newCert));
            }
            catch (Exception ex)
            {
                return new Response<GetJobSeekerProfileDtoResponse>(succeeded: false, $"Error al procesar la aplicación: {ex}");
            }
        }

        #endregion CreateCertification

        #region CreateExperience

        public async Task<Response<GetJobSeekerProfileDtoResponse?>> CreateExperience(CreateWorkExperienceDtoRequest create)
        {
            try
            {
                var workExp = await Task.FromResult(_unitOfWork.WorkExperienceRepositoryAsync
                    .FindBy(x => x.JobTitle.Equals(create.JobTitle) && x.CompanyName.Equals(create.CompanyName) && x.JobSeekerId == create.JobSeekerId)
                    .FirstOrDefault());

                if (workExp != null)
                {
                    throw new CoreException("Esta experiencia ya esta agregada.");
                }

                var newWorkExp = new WorkExperience
                {
                    JobSeekerId = create.JobSeekerId,
                    CompanyName = create.CompanyName,
                    JobTitle = create.JobTitle,
                    StartDate = create.StartDate,
                    EndDate = create.EndDate,
                    IsCurrentJob = create.IsCurrentJob,
                    Description = create.Description,
                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork.WorkExperienceRepositoryAsync.AddAsync(newWorkExp);
                await _unitOfWork.CommitAsync();

                return new Response<GetJobSeekerProfileDtoResponse>(_mapper.Map<GetJobSeekerProfileDtoResponse>(newWorkExp));
            }
            catch (Exception ex)
            {
                return new Response<GetJobSeekerProfileDtoResponse>(succeeded: false, $"Error al procesar la aplicación: {ex}");
            }
        }

        #endregion CreateExperience

        #region CreateProfile

        public async Task<Response<GetJobSeekerProfileDtoResponse?>> CreateProfile(CreateJobSeekerProfileDtoRequest create)
        {
            try
            {
                var profile = await Task.FromResult(_unitOfWork.JobSeekerProfileRepositoryAsync
                    .FindBy(x => x.UserId == create.UserId)
                    .FirstOrDefault());

                if (profile != null)
                {
                    throw new CoreException("El perfil ya fue creado.");
                }

                var newProfile = new JobSeekerProfile
                {
                    UserId = create.UserId,
                    FullName = create.FullName,
                    Age = (int)create.Age,
                    Phone = create.Phone,
                    City = create.City,
                    EducationLevel = create.EducationLevel,
                    YearsOfExperience = create.YearsOfExperience,
                    Skills = create.Skills,
                    ExpectedSalary = (decimal)create.ExpectedSalary,
                    PreferredLocation = create.PreferredLocation,
                    Summary = create.Summary,
                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork.JobSeekerProfileRepositoryAsync.AddAsync(newProfile);
                await _unitOfWork.CommitAsync();

                return new Response<GetJobSeekerProfileDtoResponse>(_mapper.Map<GetJobSeekerProfileDtoResponse>(newProfile));
            }
            catch (Exception ex)
            {
                return new Response<GetJobSeekerProfileDtoResponse>(succeeded: false, $"Error al procesar la aplicación: {ex}");
            }
        }

        #endregion CreateProfile

        #region GetProfileById

        public async Task<Response<GetJobSeekerProfileDtoResponse?>> GetProfileById(int userId)
        {
            try
            {
                var profile = await Task.FromResult(_unitOfWork.JobSeekerProfileRepositoryAsync
                    .FindBy(x => x.UserId == userId)
                    .FirstOrDefault());

                if (profile == null)
                {
                    throw new CoreException("No hay perfil asociado al usuario ingresado.");
                }

                return new Response<GetJobSeekerProfileDtoResponse>(_mapper.Map<GetJobSeekerProfileDtoResponse>(profile));
            }
            catch (Exception ex)
            {
                return new Response<GetJobSeekerProfileDtoResponse>(succeeded: false, $"Error al procesar la aplicación: {ex}");
            }
        }

        #endregion GetProfileById

        #region UpdateProfile

        public async Task<Response<GetJobSeekerProfileDtoResponse?>> UpdateProfile(UpdateJobSeekerProfileDtoRequest update)
        {
            try
            {
                var profile = await Task.FromResult(_unitOfWork.JobSeekerProfileRepositoryAsync
                    .FindBy(x => x.UserId == update.UserId && x.ProfileId == update.ProfileId)
                    .FirstOrDefault());

                if (profile == null)
                {
                    throw new CoreException("No hay perfil asociado al usuario.");
                }

                var newProfile = new JobSeekerProfile
                {
                    FullName = update.FullName,
                    Age = (int)update.Age,
                    Phone = update.Phone,
                    City = update.City,
                    EducationLevel = update.EducationLevel,
                    YearsOfExperience = update.YearsOfExperience,
                    Skills = update.Skills,
                    ExpectedSalary = (decimal)update.ExpectedSalary,
                    PreferredLocation = update.PreferredLocation,
                    Summary = update.Summary,
                };

                await _unitOfWork.JobSeekerProfileRepositoryAsync.UpdateAsync(newProfile);
                await _unitOfWork.CommitAsync();

                return new Response<GetJobSeekerProfileDtoResponse>(_mapper.Map<GetJobSeekerProfileDtoResponse>(newProfile));
            }
            catch (Exception ex)
            {
                return new Response<GetJobSeekerProfileDtoResponse>(succeeded: false, $"Error al procesar la aplicación: {ex}");
            }
        }

        #endregion UpdateProfile
    }
}