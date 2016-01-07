using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project
{
    public class Client : User
    {
        public Client(int userid, string name, string adress, string location, string phonenumber, string license, string hascar, DateTime dateofwriteout, int accountid, int clientid, string ovpossible) :base(userid, name, adress, location, phonenumber, license, hascar, dateofwriteout, accountid)
        {
            this.UserID = userid;
            this.Name = name;
            this.Adress = adress;
            this.Location = location;
            this.AccountID = accountid;
            
        }
        public int ClientID { get; set; }
        public string OVpossible { get; set; }
    }
}