namespace Application.Dtos.BlogPostDtos
{
    public class UpdateBlogPostDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public List<int> CategoryIds { get; set; } = new List<int>();
    }
}
