using TalentMatch.Domain.Entities;
using TalentMatch.Infrastructure.Settings;

namespace TalentMatch.Infrastructure.Repositories.RespositoryAsync
{
    public class EmployerProfileRepositoryAsync : GenericRepository<EmployerProfile>
    {
        public EmployerProfileRepositoryAsync(Context dbContext) : base(dbContext)
        {
        }
    }
}