using TalentMatch.Domain.Entities;
using TalentMatch.Infrastructure.Settings;

namespace TalentMatch.Infrastructure.Repositories.RespositoryAsync
{
    public class UserRepositoryAsync : GenericRepository<User>
    {
        public UserRepositoryAsync(Context dbContext) : base(dbContext)
        {
        }
    }
}