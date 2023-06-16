using DMSTest.DTO.Common;
using DMSTest.DTO.Models.DTO;
using DMSTest.DTO.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace DMSTest.Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdministrationController : ControllerBase
    {

        private readonly DMSTest.BAL.Administration _administration;
        public IConfiguration _configuration;
        private readonly AppSettings _appSettings;

        public AdministrationController(DMSTest.BAL.Administration administrationDataAccess, IConfiguration configuration, IOptions<AppSettings> appSettings)
        {
            _administration = administrationDataAccess;
            _configuration = configuration;
            _appSettings = appSettings.Value;
        }


        [HttpPut]
        [Route("/UpdateUser")]
        public Answer CreateUpdateUser(User user)
        {
            Answer answer = _administration.UpdateUser(user);
            return answer;
        }


        [HttpDelete]
        [Route("/DeleteUser")]
        public Answer DeleteUser(int idUser)
        {
            Answer answer = _administration.DeleteUser(idUser);
            return answer;
        }


        [HttpGet]
        [Route("/GetListUsers")]
        public Answer GetListUser()
        {
            Answer answer = _administration.GetListUser();
            return answer;
        }

        [HttpGet]
        [Route("/GetPersonalData")]
        public Answer getPersonalData()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = userIdClaim.Value;
            Answer answer = _administration.GetPersonalData(int.Parse(userId));
            return answer;
        }


        [HttpPut]
        [Route("/UpdatePersonalUser")]
        public Answer UpdatePersonalUser(User user)
        {
            Answer answer = _administration.UpdatePersonalUser(user);
            return answer;
        }


    }
}
