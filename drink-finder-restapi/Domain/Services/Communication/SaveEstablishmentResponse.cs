using drink_finder_restapi.Domain.Models;

namespace drink_finder_restapi.Domain.Services.Communication
{
    public class SaveEstablishmentResponse : BaseResponse
    {
            public Establishment _establishment { get; set; }

            private SaveEstablishmentResponse(bool success, string message, Establishment establishment) : base(success, message)
            {
            _establishment = establishment;
            }

            public SaveEstablishmentResponse(Establishment establishment) : this(true, string.Empty, establishment)
            { }

            public SaveEstablishmentResponse(string message) : this(false, message, null)
            { }
    }
}
