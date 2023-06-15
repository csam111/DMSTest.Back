using DMSTest.DTO.Request;
using DMSTest.DTO.Response;

namespace DMSTest.Back.Services
{
    public interface IUserService
    {
        UserResponse Auth(AuthRequest model);
    }
}
