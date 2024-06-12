using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusDBClasses.UserManagementClasses
{
    public class User
    {
        public int userid { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string rolekey { get; set; }
        public User(int userid, string username, string password, string rolekey)
        {
            this.userid = userid;
            this.username = username;
            this.password = password;
            this.rolekey = rolekey;
        }
    }
}
