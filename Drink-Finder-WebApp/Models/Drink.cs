namespace Drink_Finder_WebApp.Models
{
    public class Drink
    {
        public int id { get; set; }
        public string? name { get; set; }
        public double volume { get; set; }
        public int drinkCategoryId { get; set; }
    }
}
