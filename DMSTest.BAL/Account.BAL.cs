using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DMSTest.BAL.Utilities;
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
        SHA256Password sHA = new SHA256Password();
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

            userDMS.Email = user.User;
            userDMS.Password = sHA.StrongPassword(user.Password);

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
