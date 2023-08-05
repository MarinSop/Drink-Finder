namespace drink_finder_restapi.Domain.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
