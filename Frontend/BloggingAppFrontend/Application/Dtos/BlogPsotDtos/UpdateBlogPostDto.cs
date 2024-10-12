namespace BloggingAppFrontend.Application.Dtos.BlogPsotDtos
{
    public class UpdateBlogPostDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public List<int> CategoryIds { get; set; } = new List<int>();
    }
}
