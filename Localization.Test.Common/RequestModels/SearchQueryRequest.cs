namespace Localization.Test.Common.RequestModels
{
    public class SearchQueryRequest
    {
        public string? Search { get; set; }
        public string? Order { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageCount { get; set; } = 0;
    }
}
