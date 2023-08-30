﻿namespace drink_finder_restapi.Domain.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public List<Establishment> establishments { get; } = new();
    }
}
