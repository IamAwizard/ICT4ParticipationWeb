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
        public int Phonenumber { get; set; }
        public string License { get; set; }
        public string Hascar { get; set; }
        public string Dateofwriteout { get; set; }

        public User(string name, string adress,string location, int phonenumber, string license, string hascar,string dateofwriteout)
        {
            this.Name = name;
            this.Adress = adress;
            this.Location = location;
            this.Phonenumber = phonenumber;
            this.License = license;
            this.Hascar = hascar;
            this.Dateofwriteout = dateofwriteout;
        }

        public User(int userid,string name, string adress, string location, int phonenumber, string license, string hascar, string dateofwriteout)
        {
            this.UserID = userid;
            this.Name = name;
            this.Adress = adress;
            this.Location = location;
            this.Phonenumber = phonenumber;
            this.License = license;
            this.Hascar = hascar;
            this.Dateofwriteout = dateofwriteout;
        }
    }
}