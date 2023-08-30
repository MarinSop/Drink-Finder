using drink_finder_restapi.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace drink_finder_restapi.Resources
{
    public class SaveEstablishmentDrinkResource
    {
        public int EstablishemntId { get; set; }
        public int DrinkId { get; set; }
        public double price { get; set; }
    }
}
