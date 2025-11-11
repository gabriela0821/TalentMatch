using AutoMapper;
using TalentMatch.Core.DTOs.Certification.Request;
using TalentMatch.Core.DTOs.Certification.Response;
using TalentMatch.Domain.Entities;

namespace TalentMatch.Core.Mappings
{
    public class CertificationProfile : Profile
    {
        public CertificationProfile()
        {
            #region RequestCertification

            CreateMap<CreateCertificationDtoRequest, Certification>();
            CreateMap<UpdateCertificationDtoRequest, Certification>();

            #endregion RequestCertification

            CreateMap<Certification, GetCertificationDtoResponse>();
        }
    }
}