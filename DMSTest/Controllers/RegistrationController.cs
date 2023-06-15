﻿using DMSTest.DTO.Common;
using DMSTest.DTO.Models.DTO;
using DMSTest.DTO.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DMSTest.Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly DMSTest.BAL.Registration _registration;
        public IConfiguration _configuration;
        private readonly AppSettings _appSettings;

        public RegistrationController(DMSTest.BAL.Registration accountDataAccess, IConfiguration configuration, IOptions<AppSettings> appSettings)
        {
            _registration = accountDataAccess;
            _configuration = configuration;
            _appSettings = appSettings.Value;
        }


        [HttpPost]
        [Route("/CreateUser")]
        public Answer CreateUpdateUser(User user)
        {
            Answer answer = _registration.CreateUpdateUser(user);
            return answer;
        }
        
        [HttpDelete]
        [Route("/DeleteUser")]
        public Answer DeleteUser(int idUser)
        {
            Answer answer = _registration.DeleteUser(idUser);
            return answer;
        }


    }
}
