using DMSTest.BAL.Utilities;
using DMSTest.DTO.Common;
using DMSTest.DTO.Response;
using DMSTest.Entity.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSTest.BAL
{
    public class Administration
    {
        private readonly DMSTest.DataAccess.Registration _registrationDataAccess;
        public IConfiguration _configuration;
        private readonly AppSettings _appSettings;
        SHA256Password sHA256 = new SHA256Password();

        public Administration(DMSTest.DataAccess.Registration registrationDataAccess, IConfiguration configuration, IOptions<AppSettings> appSettings)
        {
            _registrationDataAccess = registrationDataAccess;
            _configuration = configuration;
            _appSettings = appSettings.Value;
        }

        public Answer UpdateUser(DTO.Models.DTO.User user)
        {
            Answer answer = new Answer();
            User userDMS = new User();

            try
            {
                userDMS.IdUsers = user.IdUsers;
                userDMS.Nombre = user.Nombre;
                userDMS.Email = user.Email;
                userDMS.Password = sHA256.StrongPassword(user.Password);
                User validateUser = _registrationDataAccess.SearchUser(userDMS.IdUsers);

                if (validateUser != null)
                {
                    if (_registrationDataAccess.UpdateUser(userDMS))
                    {
                        answer.Success = 1;
                        answer.Message = "El usuario se actualizo exitosamente";
                        answer.Data = null;
                        return answer;
                    }
                    else
                    {
                        answer.Success = 0;
                        answer.Message = "El usuario no se actualizo";
                        answer.Data = null;
                        return answer;
                    }

                }
                else
                {
                    answer.Success = 0;
                    answer.Message = "El usuario no existe";
                    answer.Data = null;
                    return answer;
                }
            }
            catch (Exception ex)
            {
                answer.Success = 0;
                answer.Message = ex.Message;
                answer.Data = null;
                return answer;
            }
        }

        public Answer DeleteUser(int idUser)
        {
            Answer answer = new Answer();

            try
            {
                User validateUser = _registrationDataAccess.SearchUser(idUser);
                if (validateUser != null)
                {
                    if (_registrationDataAccess.DeleteUser(idUser))
                    {
                        answer.Success = 1;
                        answer.Message = "El usuario se elimino exitosamente";
                        answer.Data = null;
                        return answer;
                    }
                }

                answer.Success = 1;
                answer.Message = "El usuario no existe";
                answer.Data = null;
                return answer;

            }
            catch (Exception ex)
            {
                answer.Success = 0;
                answer.Message = ex.Message;
                answer.Data = null;
                return answer;
            }
        }
    }
}
