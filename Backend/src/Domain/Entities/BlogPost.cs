namespace Domain.Entities
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public int AuthorId { get; set; }
        public User User { get; set; }
        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
