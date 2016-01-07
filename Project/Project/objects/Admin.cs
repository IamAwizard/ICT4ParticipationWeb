using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project
{
    public class Admin
    {
        public Admin(int accountid)
        {
            this.AccountID = accountid;
        }

        public int AccountID { get; set; }

    }
}