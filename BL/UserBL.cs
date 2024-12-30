using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL;
namespace BL
{
    public class UserBL
    {
        public LoginDL loginDL = new LoginDL();

        public bool Login(string username, string password)
        {
            return loginDL.Login(username, password);
        }
    }
}
