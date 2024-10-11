using Application.Dtos.CategoryDtos;

namespace Application.Dtos.BlogPostDtos
{
    public class BlogPostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
    }
}
