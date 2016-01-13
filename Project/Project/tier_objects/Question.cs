using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project
{
    public class Question
    {
        // Constructor
        public Question(string description, DateTime datebegin, bool critical, int volunteersneeded)
        {
            this.Description = description;
            this.DateBegin = datebegin;
            this.Critical = critical;
            this.VolunteersNeeded = volunteersneeded;
        }
        public Question(int authorid, string description, DateTime datebegin, int volunteersneeded)
        {
            this.AuthorID = authorid;
            this.Description = description;
            this.DateBegin = datebegin;
            this.VolunteersNeeded = volunteersneeded;
        }
        public Question(int questionid, string description, string location, string traveltime, DateTime datebegin, DateTime enddate, string critical, int volunteersneeded, int authorid, int transportid, string transportdescription)
        {
            this.Critical = bool.Parse(critical);
            this.AuthorID = authorid;
            this.Description = description;
            this.DateBegin = datebegin;
            this.VolunteersNeeded = volunteersneeded;
            this.DateEnd = enddate;
            this.ID = questionid;
            this.Location = location;
            this.TravelTime = traveltime;
            this.Transport = new Transport(transportid, transportdescription);
        }

        // Properties

        public int ID { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string TravelTime { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }
        public bool Critical { get; set; }
        public int VolunteersNeeded { get; set; }
        public int AuthorID { get; set; }
        public Client Author { get; set; }
        public Transport Transport { get; set; }
        public List<Volunteer> AcceptedBy { get; set; }

        public string FormattedForVolunteer
        {
            get
            {
                string summary;
                if (Description.Length < 60)
                {
                    summary = Description;
                }
                else
                {
                    summary = Description.Substring(0, 60) + "...";
                }
                return $"{DateBegin.ToShortDateString()}: {summary}";
            }
        }

        // Methodes

        public override string ToString()
        {
            string summary;
            if (Description.Length < 60)
            {
                summary = Description;
            }
            else
            {
                summary = Description.Substring(0, 60) + "...";
            }
            return DateBegin.ToShortDateString() + ": " + summary;
        }
    }
}