using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C969___Scheduler.Entity_Classes
{
    public class User
    {
        public int userID { get; set; }
        public string username { get; set; }
        public string password { get; set; }    


        public User()
        {
            username = string.Empty;
        }
        public User(string loginUsername)
        {
            username = loginUsername;
           

        }
        public User(string loginUsername, string loginPassword)
        {
            username = loginUsername;
            password = loginPassword;

        }
    }
}
