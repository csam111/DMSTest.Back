using DMSTest.DTO.Request;
using DMSTest.DTO.Response;

namespace DMSTest.Back.Services
{
    public class UserService: IUserService
    {
        private readonly DMSTest.BAL.Account _account;


        public UserService(DMSTest.BAL.Account account)
        {
            _account = account;
        }

        public UserResponse Auth(AuthRequest user)
        {
            return _account.UserAuthorization(user);
        }
    }
}
