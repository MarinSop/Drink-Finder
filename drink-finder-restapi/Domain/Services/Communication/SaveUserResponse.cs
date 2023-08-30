using drink_finder_restapi.Domain.Models;

namespace drink_finder_restapi.Domain.Services.Communication
{
    public class SaveUserResponse : BaseResponse
    {
        public User _user { get; set; }

        private SaveUserResponse(bool success, string message, User user) : base(success, message)
        {
            _user = user;
        }

        public SaveUserResponse(User user) : this(true, string.Empty, user)
        { }

        public SaveUserResponse(string message) : this(false, message, null)
        { }
    }
}
