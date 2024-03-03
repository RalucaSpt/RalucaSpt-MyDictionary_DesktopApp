using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary_App.MyApp
{
    public class Admin
    {
        public string username;
        public string password;
    }
    public class Admins
    {
        public static List<Admin> admins = new List<Admin>
        {
            new Admin { username = "admin", password = "admin" }
        };
    }   
}
