using TalentMatch.Infrastructure.Exceptions;

namespace TalentMatch.Infrastructure.Extensions
{
    public static class ValidateNullEntity<T>
    {
        public static void IsNullEntity(T entity)
        {
            if (entity == null)
            {
                throw new InfrastructureException("{0} It can not be null.", "entity");
            }
        }

        public static void IsNullListEntity(IQueryable<T> entity)
        {
            if (entity == null || !entity.Any())
            {
                throw new InfrastructureException("The list of {0} cannot be null.", "entity");
            }
        }
    }
}