using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project
{
    public class Client : User
    {
        public Client(int accountid, string username, string password, string email, string name, int userid, string adress, string location, string phonenumber, string license, string hascar, int clientid, string ovpossible, DateTime unsubscribeddate)
    : base(accountid, username, password, email, userid, name, adress, location, phonenumber, license, hascar, unsubscribeddate)
        {
            this.ClientID = clientid;
            this.OVpossible = ovpossible;
        }
        public Client(string username, string password, string email, string name, string adress, string location, string phonenumber, string license, string hascar, string ovpossible)
            : base(username, password, email, name, adress, location, phonenumber, license, hascar)
        {
            this.OVpossible = ovpossible;
        }
        public int ClientID { get; set; }
        public string OVpossible { get; set; }
    }
}