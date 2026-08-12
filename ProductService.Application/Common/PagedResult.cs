using System.Collections;
using System.Net.Http.Headers;

namespace ProductService.Application.Common;

public record PagedResult<T> where T : IEnumerable
{
    public T Values { get; }
    public int PageSize { get; }
    public int PageNumber { get; }
    public int TotalCount { get; }
    public bool HasNextPage => PageSize * PageNumber > TotalCount;
    public bool HasPreviousPage => PageNumber > 1;

    private PagedResult(T values, int pageSize, int pageNumber, int totalCount)
    {
        Values = values;
        PageSize = pageSize;
        PageNumber = pageNumber;
        TotalCount = totalCount;
    }

    /// <summary>
    /// Created a new PagedResult<out T> record with its parameters such as pageSize, pageNumber and totalCount
    /// </summary>
    /// <param name="values"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <param name="totalCount"></param>
    /// <returns>New PagedResult<out T> containing a List<T> of elements></returns>
    /// <exception cref="Exception"></exception>
    public static PagedResult<T> Create(T values, int pageSize, int pageNumber, int totalCount)
    {
        if (pageSize <= 0 || pageNumber <= 0)
            throw new Exception();

        return new PagedResult<T>(values, pageSize, pageNumber, totalCount);
    }
}