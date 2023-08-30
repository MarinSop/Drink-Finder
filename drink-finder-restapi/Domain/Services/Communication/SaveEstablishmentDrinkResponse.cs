using drink_finder_restapi.Domain.Models;

namespace drink_finder_restapi.Domain.Services.Communication
{
    public class SaveEstablishmentDrinkResponse : BaseResponse
    {
        public EstablishmentDrink _establishmentDrink { get; set; }

        private SaveEstablishmentDrinkResponse(bool success, string message, EstablishmentDrink establishmentDrink) : base(success, message)
        {
            _establishmentDrink = establishmentDrink;
        }

        public SaveEstablishmentDrinkResponse(EstablishmentDrink establishmentDrink) : this(true, string.Empty, establishmentDrink)
        { }

        public SaveEstablishmentDrinkResponse(string message) : this(false, message, null)
        { }
    }
}
