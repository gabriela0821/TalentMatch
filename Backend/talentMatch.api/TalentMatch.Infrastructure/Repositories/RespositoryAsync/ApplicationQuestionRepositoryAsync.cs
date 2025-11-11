using TalentMatch.Domain.Entities;
using TalentMatch.Infrastructure.Settings;

namespace TalentMatch.Infrastructure.Repositories.RespositoryAsync
{
    public class ApplicationQuestionRepositoryAsync : GenericRepository<ApplicationQuestion>
    {
        public ApplicationQuestionRepositoryAsync(Context dbContext) : base(dbContext)
        {
        }
    }
}