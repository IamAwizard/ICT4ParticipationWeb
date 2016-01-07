using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project
{
    public class Admin
    {
        public int userID { get; set; }

        public Admin(int userid)
        {
            this.userID = userid;
        }
    }
}