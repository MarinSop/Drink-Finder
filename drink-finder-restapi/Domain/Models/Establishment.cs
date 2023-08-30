namespace drink_finder_restapi.Domain.Models
{
    public class Establishment
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int CityId { get; set; }
        public City? City { get; set; }
        public List<EstablishmentDrink> establishemntDrinks { get; } = new();
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
