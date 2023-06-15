using DMSTest.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSTest.DataAccess
{
    public class Account
    {

        private readonly DMSTestContext _dmsTestContext;

        public Account(DMSTestContext dmsTstContext)
        {
            _dmsTestContext = dmsTstContext;
        }

        public User UserAuthorization(User user)
        {
            User isUser = _dmsTestContext.Users.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
            return isUser;
        }
    }
}
