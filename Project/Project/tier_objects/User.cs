using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project
{
    public class User : Account
    {
        public User(int accountid, string username, string password, string email, int userid, string name, string adress, string location, string phonenumber, string license, string hascar, DateTime unsubscribeddate) : base(accountid, username, password, email)
        {
            this.UserID = userid;
            this.Name = name;
            this.Adress = adress;
            this.Location = location;
            this.Phonenumber = phonenumber;
            this.License = license;
            this.Hascar = hascar;
            this.UnsubscribedDate = unsubscribeddate;
        }

        public User(string username, string password, string email, string name, string adress, string location, string phonenumber, string license, string hascar) : base(username, password, email)
        {
            this.Name = name;
            this.Adress = adress;
            this.Location = location;
            this.Phonenumber = phonenumber;
            this.License = license;
            this.Hascar = hascar;
        }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Location { get; set; }
        public string Phonenumber { get; set; }
        public string License { get; set; }
        public string Hascar { get; set; }
        public DateTime UnsubscribedDate { get; set; }
    }
}