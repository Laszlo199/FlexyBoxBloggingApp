using System.ComponentModel.DataAnnotations;

namespace BloggingAppFrontend.Application.Dtos.BlogPsotDto
{
    public class CreateBlogPostDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public int AuthorId { get; set; }

        [Required]
        public List<int> CategoryIds { get; set; } = new List<int>();
    }
}
