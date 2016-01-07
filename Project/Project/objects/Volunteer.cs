using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project
{
    public class Volunteer : User
    {
        public Volunteer(int accountid, string name, string adress, string location, string phonenumber, string license, string hascar, int volunteerid ): base(accountid, name, adress, location, phonenumber, license, hascar)
        {
            this.AccountID = accountid;
            this.Name = name;
            this.Adress = adress;
            this.Location = location;
            this.Phonenumber = phonenumber;
            this.License = license;
            this.Hascar = hascar;
            this.ID = volunteerid; 
        }

        public int ID { get; set; }
        public string Photo { get; set; }
        public string VOG { get; set; }
    }
}