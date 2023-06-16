using DMSTest.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSTest.DataAccess
{
    public class Registration
    {
        private readonly DMSTestContext _dmsTestContext;

        public Registration(DMSTestContext dmsTstContext)
        {
            _dmsTestContext = dmsTstContext;
        }

        public bool CreateUser(User user)
        {
            _dmsTestContext.Users.Add(user);
            _dmsTestContext.SaveChanges();
            return true;
        }  
        
        public User SearchUser(int idUser)
        {
            User userDMS = _dmsTestContext.Users.Where(x => x.IdUsers == idUser).FirstOrDefault();
            return userDMS;
        }       
        
        public List<User> GetListUser()
        {
            List<User> listUser = _dmsTestContext.Users.ToList();
            return listUser;
        }

        public bool UpdateUser(User user)
        {
            User userDMS = _dmsTestContext.Users.Where(x => x.IdUsers == user.IdUsers).FirstOrDefault();
            userDMS.Nombre = user.Nombre;
            userDMS.IdUsers = user.IdUsers;
            userDMS.Email = user.Email;
            //userDMS.Password = user.Password;
            _dmsTestContext.Entry(userDMS).State = EntityState.Modified;
            _dmsTestContext.SaveChanges();
            return true;
        } 
        
        public bool DeleteUser(int IdUser)
        {
            User userDMS = _dmsTestContext.Users.Where(x => x.IdUsers == IdUser).FirstOrDefault();
            _dmsTestContext.Remove(userDMS);
            _dmsTestContext.SaveChanges();
            return true;
        }


    }
}
