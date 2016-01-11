using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project
{
    public class Admin : Account
    {
        public Admin(int accountid, string username, string password, string email) : base(accountid, username, password, email)
        {

        }

        public Admin(string username, string password, string email) : base(username, password, email)
        {

        }
    }
}