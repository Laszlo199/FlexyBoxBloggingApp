namespace Application.Models
{
    public class BlogPostModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public List<int> CategoryIds { get; set; } = new List<int>();
        public List<CategoryModel> Categories { get; set; } = new List<CategoryModel>();
    }
}