﻿using DMSTest.BAL.Utilities;
using DMSTest.DTO.Common;
using DMSTest.DTO.Models.DTO;
using DMSTest.DTO.Response;
using DMSTest.Entity.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User = DMSTest.Entity.Models.User;

namespace DMSTest.BAL
{
    public class Registration
    {
       
        private readonly DMSTest.DataAccess.Registration _registrationDataAccess;
        public IConfiguration _configuration;
        private readonly AppSettings _appSettings;
        SHA256Password sHA256 = new SHA256Password();

        public Registration(DMSTest.DataAccess.Registration registrationDataAccess, IConfiguration configuration, IOptions<AppSettings> appSettings)
        {
            _registrationDataAccess = registrationDataAccess;
            _configuration = configuration;
            _appSettings = appSettings.Value;
        }

        public Answer CreateUpdateUser(DTO.Models.DTO.User user) 
        {
            Answer answer = new Answer();
            User userDMS = new User();

            try
            {
                userDMS.IdUsers = user.IdUsers;
                userDMS.Nombre = user.Nombre;
                userDMS.Email = user.Email;
                userDMS.Password = sHA256.StrongPassword(user.Password);

                if (user.IdUsers == 0)
                {
                    if(_registrationDataAccess.CreateUser(userDMS))
                    {
                        answer.Success = 1;
                        answer.Message = "El usuario se creo exitosamente";
                        answer.Data = null;
                        return answer;
                    }
                }
                else
                {
                    
                    User validateUser = _registrationDataAccess.SearchUser(userDMS.IdUsers);
                    if(validateUser != null)
                    {
                        if(_registrationDataAccess.UpdateUser(userDMS))
                        { 
                        answer.Success = 1;
                        answer.Message = "El usuario se actualizo exitosamente";
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
                answer.Success = 0;
                answer.Message = "El usuario no se creo ni actualizo";
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
