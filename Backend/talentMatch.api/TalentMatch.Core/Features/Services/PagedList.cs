using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq.Expressions;
using TalentMatch.Core.Extensions;
using TalentMatch.Core.Interfaces.Pagination;
using TalentMatch.Domain.QueryFilters;

namespace TalentMatch.Core.Features.Services
{
    public class PagedList : IPagedList
    {
        private readonly IMapper _mapper;

        public PagedList(IMapper Mapper)
        {
            _mapper = Mapper;
        }

        public async Task<PaginationResponse<T2>> CreatePagedGenericResponse<T, T2>(IQueryable<T> queryable, int page, int pageSize, string orderBy, bool ascending, bool distinct = false)
        {
            int skipAmount = pageSize * (page - 1);
            int totalNumberOfRecords = await Task.FromResult(queryable.Count());
            new List<T2>();
            List<T2> data;
            if (string.IsNullOrWhiteSpace(orderBy))
            {
                List<T2> list = !distinct ? await Task.FromResult(queryable.ProjectTo(_mapper.ConfigurationProvider, Array.Empty<Expression<Func<T2, object>>>()).Skip(skipAmount).Take(pageSize)
                    .ToList()) : await Task.FromResult(queryable.ProjectTo(_mapper.ConfigurationProvider, Array.Empty<Expression<Func<T2, object>>>()).Skip(skipAmount).Take(pageSize)
                    .Distinct()
                    .ToList());
                data = list;
            }
            else
            {
                List<T2> list = !distinct ? await Task.FromResult(queryable.ProjectTo(_mapper.ConfigurationProvider, Array.Empty<Expression<Func<T2, object>>>()).OrderByPropertyOrField(orderBy, ascending).Skip(skipAmount)
                    .Take(pageSize)
                    .ToList()) : await Task.FromResult(queryable.ProjectTo(_mapper.ConfigurationProvider, Array.Empty<Expression<Func<T2, object>>>()).OrderByPropertyOrField(orderBy, ascending).Skip(skipAmount)
                    .Take(pageSize)
                    .Distinct()
                    .ToList());
                data = list;
            }

            int num = totalNumberOfRecords % pageSize;
            int totalPages = totalNumberOfRecords / pageSize + (num != 0 ? 1 : 0);
            return new PaginationResponse<T2>
            {
                Data = data,
                PageNumber = page,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalCount = totalNumberOfRecords
            };
        }
    }
}