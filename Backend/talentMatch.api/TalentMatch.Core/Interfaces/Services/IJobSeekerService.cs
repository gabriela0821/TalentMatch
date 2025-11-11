using TalentMatch.Core.DTOs.Certification.Request;
using TalentMatch.Core.DTOs.JobSeekerProfile.Request;
using TalentMatch.Core.DTOs.JobSeekerProfile.Response;
using TalentMatch.Core.DTOs.WorkExperience.Request;
using TalentMatch.Core.Wrappers;

namespace TalentMatch.Core.Interfaces.Services
{
    public interface IJobSeekerService
    {
        Task<Response<GetJobSeekerProfileDtoResponse?>> CreateProfile(CreateJobSeekerProfileDtoRequest create);

        Task<Response<GetJobSeekerProfileDtoResponse?>> UpdateProfile(UpdateJobSeekerProfileDtoRequest update);

        Task<Response<GetJobSeekerProfileDtoResponse?>> GetProfileById(int userId);

        Task<Response<GetJobSeekerProfileDtoResponse?>> CreateExperience(CreateWorkExperienceDtoRequest create);

        Task<Response<GetJobSeekerProfileDtoResponse?>> CreateCertification(CreateCertificationDtoRequest create);
    }
}