using AutoMapper;
using Microsoft.Extensions.Configuration;
using TalentMatch.Core.DTOs.Application.Request;
using TalentMatch.Core.DTOs.Application.Response;
using TalentMatch.Core.Interfaces.Repositories;
using TalentMatch.Core.Interfaces.Services;
using TalentMatch.Core.Wrappers;
using TalentMatch.Domain.Entities;

namespace TalentMatch.Core.Features.Services
{
    public class ApplicationsService : IApplicationsService
    {
        #region Attributes

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        #endregion Attributes

        #region Builder

        public ApplicationsService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        #endregion Builder

        #region CreateApplication

        public async Task<Response<GetApplicationDtoResponse?>> CreateApplication(CreateApplicationDtoRequest create)
        {
            try
            {
                var jobSeeker = await Task.FromResult(_unitOfWork.JobSeekerProfileRepositoryAsync
                    .FindBy(x => x.ProfileId == create.JobSeekerId)
                    .FirstOrDefault());

                if (jobSeeker == null)
                {
                    return new Response<GetApplicationDtoResponse>(succeeded: false, "El jobSeeker no existe.");
                }

                var newApplication = new Application
                {
                    JobPostingId = create.JobPostingId,
                    JobSeekerId = create.JobSeekerId,
                    CoverLetter = create.CoverLetter,
                    Status = create.Status,
                    AppliedAt = DateTime.UtcNow
                };

                await _unitOfWork.ApplicationRepositoryAsync.AddAsync(newApplication);
                await _unitOfWork.CommitAsync();

                return new Response<GetApplicationDtoResponse>(_mapper.Map<GetApplicationDtoResponse>(newApplication));
            }
            catch (Exception ex)
            {
                return new Response<GetApplicationDtoResponse>(succeeded: false, $"Error al procesar la aplicación: {ex}");
            }
        }

        #endregion CreateApplication

        #region GetApplicationByJobId

        public async Task<Response<GetApplicationDtoResponse?>> GetApplicationByJobId(int jobId)
        {
            try
            {
                var application = await Task.FromResult(_unitOfWork.ApplicationRepositoryAsync
                    .FindBy(x => x.JobPostingId == jobId)
                    .FirstOrDefault());

                if (application == null)
                {
                    return new Response<GetApplicationDtoResponse>(succeeded: false, "No hay aplicación asociado a ese id.");
                }

                return new Response<GetApplicationDtoResponse>(_mapper.Map<GetApplicationDtoResponse>(application));
            }
            catch (Exception ex)
            {
                return new Response<GetApplicationDtoResponse>(succeeded: false, $"Error al procesar la aplicación: {ex}");
            }
        }

        #endregion GetApplicationByJobId

        #region GetApplicationByUserId

        public async Task<Response<GetApplicationDtoResponse?>> GetApplicationByUserId(int userId)
        {
            try
            {
                var application = await Task.FromResult(_unitOfWork.ApplicationRepositoryAsync
                    .FindBy(x => x.JobSeekerProfile.UserId == userId, "JobSeekerProfile")
                    .FirstOrDefault());

                if (application == null)
                {
                    return new Response<GetApplicationDtoResponse>(succeeded: false, "No hay aplicación asociado a ese id.");
                }

                return new Response<GetApplicationDtoResponse>(_mapper.Map<GetApplicationDtoResponse>(application));
            }
            catch (Exception ex)
            {
                return new Response<GetApplicationDtoResponse>(succeeded: false, $"Error al procesar la aplicación: {ex}");
            }
        }

        #endregion GetApplicationByUserId
    }
}