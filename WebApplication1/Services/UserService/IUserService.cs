using WebApplication1.Dtos.UserDtos;

namespace WebApplication1.Services.UserService
{
    public interface IUserService
    {
        Task<GetUserDto> CreateUser(AddUserDto user);
        Task<string> Login(AddUserDto user);
    }
}
