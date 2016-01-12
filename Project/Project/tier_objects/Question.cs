using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project
{
    public class Question
    {
        public Question(string description, DateTime datebegin, bool critical, int volunteersneeded)
        {
            this.Description = description;
            this.DateBegin = datebegin;
            this.Critical = critical;
            this.VolunteersNeeded = volunteersneeded;
        }
        public Question(int authorid, string description, DateTime datebegin,int volunteersneeded)
        {
            this.AuthorID = authorid;
            this.Description = description;
            this.DateBegin = datebegin;
            this.VolunteersNeeded = volunteersneeded;
        }
        public Question(int authorid, string description, DateTime datebegin, DateTime enddate,int volunteersneeded, int id ,string location,string traveltime, int transportid)
        {
            this.AuthorID = authorid;
            this.Description = description;
            this.DateBegin = datebegin;
            this.VolunteersNeeded = volunteersneeded;
            this.DateEnd = enddate;
            this.ID = id;
            this.Location = location;
            this.TravelTime = traveltime;
            this.Transport.ID = transportid;
        }




        public int ID { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string TravelTime { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }
        public bool Critical { get; set; }
        public int VolunteersNeeded { get; set; }
        public int AuthorID { get; set; }
        public Transport Transport { get; set; }
    }
}