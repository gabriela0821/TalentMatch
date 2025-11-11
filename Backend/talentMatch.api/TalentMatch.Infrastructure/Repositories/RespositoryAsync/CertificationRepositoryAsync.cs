using TalentMatch.Domain.Entities;
using TalentMatch.Infrastructure.Settings;

namespace TalentMatch.Infrastructure.Repositories.RespositoryAsync
{
    public class CertificationRepositoryAsync : GenericRepository<Certification>
    {
        public CertificationRepositoryAsync(Context dbContext) : base(dbContext)
        {
        }
    }
}