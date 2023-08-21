namespace Drink_Finder_WebApp.Models
{
    public class Establishment
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? address { get; set; }
        public int cityId { get; set; }
        public City? city { get; set; }
    }
}
