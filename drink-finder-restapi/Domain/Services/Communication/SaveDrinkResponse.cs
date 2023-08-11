using drink_finder_restapi.Domain.Models;

namespace drink_finder_restapi.Domain.Services.Communication
{
    public class SaveDrinkResponse : BaseResponse
    {
        public Drink _drink { get; set; }

        private SaveDrinkResponse(bool success, string message, Drink drink) : base(success,message)
        {
            _drink = drink;
        }

        public SaveDrinkResponse(Drink drink) : this(true, string.Empty, drink)
        { }

        public SaveDrinkResponse(string message) : this(false, message, null)
        { }
    }
}
