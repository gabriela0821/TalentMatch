using TalentMatch.Domain.Entities;
using TalentMatch.Infrastructure.Settings;

namespace TalentMatch.Infrastructure.Repositories.RespositoryAsync
{
    public class WorkExperienceRepositoryAsync : GenericRepository<WorkExperience>
    {
        public WorkExperienceRepositoryAsync(Context dbContext) : base(dbContext)
        {
        }
    }
}