using AutoMapper;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Dtos.UserDtos;

namespace WebApplication1.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly PostgreSqlContext postgreSqlContext;
        private readonly IMapper mapper;
        private readonly IConfiguration _configuration;
        public UserService(PostgreSqlContext postgreSqlContext, IMapper mapper, IConfiguration configuration) 
        {
            this.postgreSqlContext = postgreSqlContext;
            this.mapper = mapper;
            _configuration = configuration;
        }
        
        public async Task<GetUserDto> CreateUser(AddUserDto newUser)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
            newUser.Password = passwordHash;

            var user = mapper.Map<User>( newUser );
            
            postgreSqlContext.Users.Add( user );
            await postgreSqlContext.SaveChangesAsync();

            var result = postgreSqlContext.Users.First(u => u.Email == newUser.Email);
            return mapper.Map<GetUserDto>(result); 
        }

        public async Task<string> Login(AddUserDto user)
        {
            var result = postgreSqlContext.Users.FirstOrDefault(u => u.Email == user.Email);

            if (result == null)
                return null;

            if (!BCrypt.Net.BCrypt.Verify(user.Password, result.Password))
                return null;

            string token = CreateToken(result);

            return token;
        }


        private string CreateToken(User user) 
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, "User")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

    }
}
