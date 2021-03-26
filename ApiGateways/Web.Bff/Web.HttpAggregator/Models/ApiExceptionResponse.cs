namespace Web.HttpAggregator.Models
{
    public class ApiExceptionResponse
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public string Detail { get; set; }
    }
}
