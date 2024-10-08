using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.BlogPostDtos
{
    public class UpdateBlogPostDto
    {
        [Required(ErrorMessage = "Title can not be empty!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Content can not be empty!")]
        public string Content { get; set; }
        [Required(ErrorMessage = "Category can not be empty!")]
        public List<int> CategoryIds { get; set; } = new List<int>();
    }
}
