using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project
{
    public class Volunteer : User
    {
        public Volunteer(int accountid, string username, string password, string email, int userid, string name, string adress, string location, string phonenumber, string license, string hascar, DateTime unsubscribeddate, int volunteerid, DateTime dateofbirth, string photo, string vog)
    : base(accountid, username, password, email, userid, name, adress, location, phonenumber, license, hascar, unsubscribeddate)
        {
            this.VolunteerID = volunteerid;
            this.DateOfBirth = dateofbirth;
            this.Photo = photo;
            this.VOG = vog;
        }

        public Volunteer(string username, string password, string email, string name, string adress, string location, string phonenumber, string license, string hascar, DateTime dateofbirth, string photo, string vog) 
            : base(username, password, email, name, adress, location, phonenumber, license, hascar)
        {
            this.DateOfBirth = dateofbirth;
            this.Photo = photo;
            this.VOG = vog;
        }

        public int VolunteerID { get; set; }
        public string Photo { get; set; }
        public string VOG { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}