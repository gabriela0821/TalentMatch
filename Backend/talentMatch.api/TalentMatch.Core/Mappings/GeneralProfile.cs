using AutoMapper;

namespace TalentMatch.Core.Mappings
{
    public class GeneralProfile : Profile
    {
        protected readonly IMapper _mapper;

        public GeneralProfile(IMapper mapper)
        {
            _mapper = mapper;
        }

        public static List<T> UpdateEntity<T>(List<T> list, object updatedata, IMapper _mapper)
        {
            foreach (var item in list)
            {
                _mapper.Map(updatedata, item);
            }
            return list;
        }

        public GeneralProfile()
        { }
    }
}