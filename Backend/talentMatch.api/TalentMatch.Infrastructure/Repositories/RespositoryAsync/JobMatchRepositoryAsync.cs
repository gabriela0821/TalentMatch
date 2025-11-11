using TalentMatch.Domain.Entities;
using TalentMatch.Infrastructure.Settings;

namespace TalentMatch.Infrastructure.Repositories.RespositoryAsync
{
    public class JobMatchRepositoryAsync : GenericRepository<JobMatch>
    {
        public JobMatchRepositoryAsync(Context dbContext) : base(dbContext)
        {
        }
    }
}