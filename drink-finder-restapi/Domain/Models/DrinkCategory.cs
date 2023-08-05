namespace drink_finder_restapi.Domain.Models
{
    public class DrinkCategory
    {
        [Key]
        public int id { get; set; }
        public string? name { get; set; }

        public List<Drink> drinks { get; } = new();
    }
}
