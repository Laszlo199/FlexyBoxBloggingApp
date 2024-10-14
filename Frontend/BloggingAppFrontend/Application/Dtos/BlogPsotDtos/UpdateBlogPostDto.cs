using System.ComponentModel.DataAnnotations;

namespace BloggingAppFrontend.Application.Dtos.BlogPsotDtos
{
    public class UpdateBlogPostDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public List<int> CategoryIds { get; set; } = new List<int>();
    }
}
