using AutoMapper;
using TalentMatch.Core.DTOs.User.Request;
using TalentMatch.Core.DTOs.User.Response;
using TalentMatch.Domain.Entities;

namespace TalentMatch.Core.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            #region RequestUser

            CreateMap<CreateUserDtoRequest, User>();
            CreateMap<UpdateUserDtoRequest, User>();

            #endregion RequestUser

            CreateMap<User, GetUserDtoResponse>();
        }
    }
}