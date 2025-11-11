namespace TalentMatch.BlazorApp.Models
{
    public class PaginationRequest
    {
        private const int MaxPageSize = 50;

        private int _pagesize = 10;

        private int _pageNumber = 1;

        public int PageNumber
        {
            get
            {
                return _pageNumber;
            }
            set
            {
                _pageNumber = ((value == 0) ? 1 : value);
            }
        }

        public int PageSize
        {
            get
            {
                return _pagesize;
            }
            set
            {
                _pagesize = ((value > 50) ? 50 : ((value == 0) ? 10 : value));
            }
        }

        public string? OrderBy { get; set; }

        public bool OrderAsc { get; set; } = true;
    }
}
