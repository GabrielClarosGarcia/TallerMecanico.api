
namespace TallerMecanico.Core.QueryFilters
{
    public class PaginationQueryFilter
    {
        public int PageNumber { get; set; } = 1; 
        public int PageSize { get; set; } = 10;  

        public PaginationQueryFilter() { }
    }
}
