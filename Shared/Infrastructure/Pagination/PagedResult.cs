using System.Linq.Expressions;

// Pagination Models
namespace Flota365.Platform.API.Shared.Infrastructure.Pagination
{
    public class PagedRequest
    {
        private int _page = 1;
        private int _pageSize = 10;

        public int Page
        {
            get => _page;
            set => _page = value < 1 ? 1 : value;
        }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value switch
            {
                < 1 => 10,
                > 100 => 100,
                _ => value
            };
        }

        public string? Search { get; set; }
        public string? SortBy { get; set; }
        public bool SortDescending { get; set; } = false;

        public int Skip => (Page - 1) * PageSize;
    }

    
}
