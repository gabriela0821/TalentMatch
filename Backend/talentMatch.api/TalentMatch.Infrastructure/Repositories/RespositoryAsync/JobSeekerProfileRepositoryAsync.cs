using TalentMatch.Domain.Entities;
using TalentMatch.Infrastructure.Settings;

namespace TalentMatch.Infrastructure.Repositories.RespositoryAsync
{
    public class JobSeekerProfileRepositoryAsync : GenericRepository<JobSeekerProfile>
    {
        public JobSeekerProfileRepositoryAsync(Context dbContext) : base(dbContext)
        {
        }
    }
}