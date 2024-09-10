
namespace webapi.Helpers
{
    public class PaginationHelper
    {
        public static IEnumerable<T> GetPagedData<T>(
        IEnumerable<T> query,
        int pageIndex,
        int pageSize)
        where T : class
        {
            if (pageIndex <= 0 || pageSize <= 0)
            {
                throw new ArgumentException("Invalid page index or page size.");
            }

            return query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList(); // Materialize the data
        }
    }
}
