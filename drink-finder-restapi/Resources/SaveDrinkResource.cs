namespace drink_finder_restapi.Resources
{
    public class SaveDrinkResource
    {
        [Required]
        [MaxLength(30)]
        public string? Name { get; set; }
        [Required]
        public double Volume { get; set; }
        [Required]
        public int drinkCategoryId { get; set; }
    }
}
