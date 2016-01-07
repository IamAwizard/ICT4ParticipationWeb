using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project
{
    public class User
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Location { get; set; }
        public string Phonenumber { get; set; }
        public string License { get; set; }
        public string Hascar { get; set; }
        public DateTime Dateofwriteout { get; set; }
        public int AccountID { get; set; }

        public User(int accountid, string name, string adress, string location, string phonenumber, string license, string hascar)
        {
            this.AccountID = accountid;
            this.Name = name;
            this.Adress = adress;
            this.Location = location;
            this.Phonenumber = phonenumber;
            this.License = license;
            this.Hascar = hascar;
        }

        public User(int userid,string name, string adress, string location, string phonenumber, string license, string hascar, DateTime dateofwriteout, int accountid)
        {
            this.UserID = userid;
            this.Name = name;
            this.Adress = adress;
            this.Location = location;
            this.Phonenumber = phonenumber;
            this.License = license;
            this.Hascar = hascar;
            this.Dateofwriteout = dateofwriteout;
            this.AccountID = accountid;
        }
    }
}