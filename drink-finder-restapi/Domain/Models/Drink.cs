namespace drink_finder_restapi.Domain.Models
{
    public class Drink
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }
        public double Volume { get; set; }
        public int drinkCategoryId { get; set; }
        public DrinkCategory? drinkCategory { get; set; }

        public List<EstablishmentDrink> establishemntDrinks { get; } = new();
    }
}
