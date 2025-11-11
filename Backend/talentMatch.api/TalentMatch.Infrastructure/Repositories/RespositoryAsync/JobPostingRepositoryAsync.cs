using TalentMatch.Domain.Entities;
using TalentMatch.Infrastructure.Settings;

namespace TalentMatch.Infrastructure.Repositories.RespositoryAsync
{
    public class JobPostingRepositoryAsync : GenericRepository<JobPosting>
    {
        public JobPostingRepositoryAsync(Context dbContext) : base(dbContext)
        {
        }
    }
}