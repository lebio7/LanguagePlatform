namespace Login.API.Models
{
    public class PaginationResult<T>
    {
        public int Limit { get; set; }

        public int Offset { get; set; }

        public int TotalResult { get; set; }

        public IReadOnlyList<T>? Items { get; set; }
    }
}