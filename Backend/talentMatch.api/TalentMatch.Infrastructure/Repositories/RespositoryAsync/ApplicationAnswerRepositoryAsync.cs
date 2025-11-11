using TalentMatch.Domain.Entities;
using TalentMatch.Infrastructure.Settings;

namespace TalentMatch.Infrastructure.Repositories.RespositoryAsync
{
    public class ApplicationAnswerRepositoryAsync : GenericRepository<ApplicationAnswer>
    {
        public ApplicationAnswerRepositoryAsync(Context dbContext) : base(dbContext)
        {
        }
    }
}