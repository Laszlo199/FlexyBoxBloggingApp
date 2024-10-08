﻿namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
    }
}
