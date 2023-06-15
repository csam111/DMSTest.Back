using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DMSTest.DTO.Common;
using DMSTest.DTO.Request;
using DMSTest.DTO.Response;
using DMSTest.Entity.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


namespace DMSTest.BAL
{
    public class Account
    {
        private readonly DMSTest.DataAccess.Account _acccountDataAccess;
        public IConfiguration _configuration;
        private readonly AppSettings _appSettings;

        public Account(DMSTest.DataAccess.Account accountDataAccess, IConfiguration configuration, IOptions<AppSettings> appSettings)
        {
            _acccountDataAccess = accountDataAccess;
            _configuration = configuration;
            _appSettings = appSettings.Value;
        }

        public UserResponse UserAuthorization(AuthRequest user)
        {
            User userDMS = new User();
            UserResponse model = new UserResponse();
            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(user.Password));
                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            user.Password = Sb.ToString();

            userDMS.Email = user.User;
            userDMS.Password = user.Password;

            User isUser = _acccountDataAccess.UserAuthorization(userDMS);

            if (isUser != null)
            {
                model.User = userDMS.Email;
                model.Token = GenerateToken(isUser);
            }

            return model;
        }


        private string GenerateToken(User User)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.SecretToken);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, User.IdUsers.ToString()),
                        new Claim(ClaimTypes.Email, User.Email),
                    }
                    ),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
