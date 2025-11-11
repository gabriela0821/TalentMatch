using TalentMatch.Domain.QueryFilters;

namespace TalentMatch.Core.Interfaces.Pagination
{
    public interface IPagedList
    {
        Task<PaginationResponse<T2>> CreatePagedGenericResponse<T, T2>(IQueryable<T> queryable, int page, int pageSize, string orderBy, bool ascending, bool distinct = false);
    }
}