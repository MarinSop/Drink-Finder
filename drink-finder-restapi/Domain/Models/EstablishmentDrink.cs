using System.ComponentModel.DataAnnotations.Schema;

namespace drink_finder_restapi.Domain.Models
{
    public class EstablishmentDrink
    {
        [Key, Column(Order = 0)]
        public int EstablishemntId { get; set; }
        [Key, Column(Order = 1)]
        public int DrinkId { get; set; }
        public Establishment establishment { get; set; }
        public Drink drink { get; set; }
        public double price { get; set; }
    }
}
