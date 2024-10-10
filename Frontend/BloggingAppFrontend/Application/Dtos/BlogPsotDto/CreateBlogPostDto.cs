using System.ComponentModel.DataAnnotations;

namespace BloggingAppFrontend.Application.Dtos.BlogPsotDto
{
    public class CreateBlogPostDto
    {
        [Required(ErrorMessage = "Title can not be empty!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content can not be empty!")]
        public string Content { get; set; }

        public int AuthorId { get; set; }

        [Required(ErrorMessage = "Content can not be empty!")]
        public List<int> CategoryIds { get; set; } = new List<int>();
    }
}
