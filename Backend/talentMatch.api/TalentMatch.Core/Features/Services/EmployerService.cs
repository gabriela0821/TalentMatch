using AutoMapper;
using Microsoft.Extensions.Configuration;
using TalentMatch.Core.DTOs.EmployerProfile.Request;
using TalentMatch.Core.DTOs.EmployerProfile.Response;
using TalentMatch.Core.Interfaces.Repositories;
using TalentMatch.Core.Interfaces.Services;
using TalentMatch.Core.Wrappers;
using TalentMatch.Domain.Entities;
using TalentMatch.Infrastructure.Exceptions;

namespace TalentMatch.Core.Features.Services
{
    public class EmployerService : IEmployerService
    {
        #region Attributes

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        #endregion Attributes

        #region Builder

        public EmployerService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        #endregion Builder

        #region CreateProfile

        public async Task<Response<GetEmployerProfileDtoResponse?>> CreateProfile(CreateEmployerProfileDtoRequest create)
        {
            try
            {
                var profile = await Task.FromResult(_unitOfWork.EmployerProfileRepositoryAsync
                    .FindBy(x => x.UserId == create.UserId)
                    .FirstOrDefault());

                if (profile != null)
                {
                    throw new CoreException("El perfil ya fue creado.");
                }

                var newProfile = new EmployerProfile
                {
                    UserId = create.UserId,
                    CompanyName = create.CompanyName,
                    Industry = create.Industry,
                    City = create.City,
                    EmployeeCount = (int)create.EmployeeCount,
                    Phone = create.Phone,
                    WebsiteUrl = create.WebsiteUrl,
                    Description = create.Description,
                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork.EmployerProfileRepositoryAsync.AddAsync(newProfile);
                await _unitOfWork.CommitAsync();

                return new Response<GetEmployerProfileDtoResponse>(_mapper.Map<GetEmployerProfileDtoResponse>(newProfile));
            }
            catch (Exception ex)
            {
                return new Response<GetEmployerProfileDtoResponse>(succeeded: false, $"Error al procesar la aplicación: {ex}");
            }
        }

        #endregion CreateProfile

        #region GetProfileById

        public async Task<Response<GetEmployerProfileDtoResponse?>> GetProfileById(int userId)
        {
            try
            {
                var profile = await Task.FromResult(_unitOfWork.EmployerProfileRepositoryAsync
                    .FindBy(x => x.UserId == userId)
                    .FirstOrDefault());

                if (profile == null)
                {
                    throw new CoreException("No hay perfil asociado al usuario ingresado.");
                }

                return new Response<GetEmployerProfileDtoResponse>(_mapper.Map<GetEmployerProfileDtoResponse>(profile));
            }
            catch (Exception ex)
            {
                return new Response<GetEmployerProfileDtoResponse>(succeeded: false, $"Error al procesar la aplicación: {ex}");
            }
        }

        #endregion GetProfileById

        #region UpdateProfile

        public async Task<Response<GetEmployerProfileDtoResponse?>> UpdateProfile(UpdateEmployerProfileDtoRequest update)
        {
            try
            {
                var profile = await Task.FromResult(_unitOfWork.EmployerProfileRepositoryAsync
                    .FindBy(x => x.UserId == update.UserId && x.ProfileId == update.ProfileId)
                    .FirstOrDefault());

                if (profile == null)
                {
                    throw new CoreException("No hay perfil asociado al usuario.");
                }

                var newProfile = new EmployerProfile
                {
                    CompanyName = update.CompanyName,
                    Industry = update.Industry,
                    City = update.City,
                    EmployeeCount = (int)update.EmployeeCount,
                    Phone = update.Phone,
                    WebsiteUrl = update.WebsiteUrl,
                    Description = update.Description,
                };

                await _unitOfWork.EmployerProfileRepositoryAsync.UpdateAsync(newProfile);
                await _unitOfWork.CommitAsync();

                return new Response<GetEmployerProfileDtoResponse>(_mapper.Map<GetEmployerProfileDtoResponse>(newProfile));
            }
            catch (Exception ex)
            {
                return new Response<GetEmployerProfileDtoResponse>(succeeded: false, $"Error al procesar la aplicación: {ex}");
            }
        }

        #endregion UpdateProfile
    }
}