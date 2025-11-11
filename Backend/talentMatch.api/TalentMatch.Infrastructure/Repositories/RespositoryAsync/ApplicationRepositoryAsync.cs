using TalentMatch.Domain.Entities;
using TalentMatch.Infrastructure.Settings;

namespace TalentMatch.Infrastructure.Repositories.RespositoryAsync
{
    public class ApplicationRepositoryAsync : GenericRepository<Application>
    {
        public ApplicationRepositoryAsync(Context dbContext) : base(dbContext)
        {
        }
    }
}