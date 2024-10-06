namespace Application.Dtos.BlogPostDtos
{
    public class CreateBlogPostDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int AuthorId { get; set; }
        public List<int> CategoryIds { get; set; } = new List<int>();
    }
}
