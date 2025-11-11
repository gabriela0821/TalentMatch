using AutoMapper;
using TalentMatch.Core.DTOs.EmployerProfile.Request;
using TalentMatch.Core.DTOs.EmployerProfile.Response;
using TalentMatch.Domain.Entities;

namespace TalentMatch.Core.Mappings
{
    public class EmployerProfileProfile : Profile
    {
        public EmployerProfileProfile()
        {
            #region RequestEmployerProfile

            CreateMap<CreateEmployerProfileDtoRequest, EmployerProfile>();
            CreateMap<UpdateEmployerProfileDtoRequest, EmployerProfile>();

            #endregion RequestEmployerProfile

            CreateMap<EmployerProfile, GetEmployerProfileDtoResponse>();
        }
    }
}