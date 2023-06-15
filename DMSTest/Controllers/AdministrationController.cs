using DMSTest.DTO.Common;
using DMSTest.DTO.Models.DTO;
using DMSTest.DTO.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

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
    }
}
