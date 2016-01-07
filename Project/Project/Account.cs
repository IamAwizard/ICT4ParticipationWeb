using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project
{
    public class Account
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int UserID { get; set; }

        public Account(string username, string password, string email)
        {
            this.Email = email;
            this.Username = username;
            this.Password = password;
        }

        public Account(int userid,string username, string password, string email)
        {
            this.UserID = userid;
            this.Email = email;
            this.Username = username;
            this.Password = password;
        }
    }
}