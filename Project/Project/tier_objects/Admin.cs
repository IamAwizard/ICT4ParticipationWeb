using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project
{
    public class Admin : Account
    {
        // Constructors
        public Admin(int accountid, string username, string password, string email, int adminid) : base(accountid, username, password, email)
        {
            this.AdminID = adminid;
        }

        public Admin(string username, string password, string email) : base(username, password, email)
        {

        }

        // Properties
        public int AdminID { get; set; }
    }
}