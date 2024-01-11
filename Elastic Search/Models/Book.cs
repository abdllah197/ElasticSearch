namespace Elastic_Search.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public DateOnly PublishDate { get; set; }
        public string? Author { get; set; }
        public string? Name { get; set; }
        public string? IBN { get; set; }
        public double Price { get; set; }
    }
}
