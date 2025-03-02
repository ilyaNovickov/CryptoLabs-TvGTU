using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4WF
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsLimitationForPassword { get; set; }

        public User(string username, string password, bool isAdmin, bool isBlocked = false, bool isLimitationForPassword = false)
        {
            Username = username;
            Password = password;
            IsAdmin = isAdmin;
            IsBlocked = isBlocked;
            IsLimitationForPassword = isLimitationForPassword;
        }
    }
}
