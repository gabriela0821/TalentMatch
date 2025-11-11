using TalentMatch.BlazorApp.Models.DTOs.Certification.Request;
using TalentMatch.BlazorApp.Models.DTOs.JobSeekerProfile.Request;
using TalentMatch.BlazorApp.Models.DTOs.JobSeekerProfile.Response;
using TalentMatch.BlazorApp.Models.DTOs.WorkExperience.Request;

namespace TalentMatch.BlazorApp.Models.Interfaces.Services
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
