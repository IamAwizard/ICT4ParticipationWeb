using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project
{
    public class Meeting
    {
        private DateTime dateTime;

        public Meeting(Client client, Volunteer volunteer, DateTime dateTime, string location)
        {
            this.Client = client;
            this.Volunteer = volunteer;
            this.Date = dateTime;
            this.Location = location;
        }

        public Meeting(int id, Client client, Volunteer volunteer, DateTime dateTime, string location)
        {
            this.ID = id;
            this.Client = client;
            this.Volunteer = volunteer;
            this.Date = dateTime;
            this.Location = location;
        }

        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public Client Client { get; set; }
        public Volunteer Volunteer { get; set; }
        public string FormattedForVolunteer { get { return $"Op {Date.ToShortDateString()}, afspraak met {Client.Username} op locatie {Location}"; } }
        public string FormattedForClient { get { return $"Op {Date.ToShortDateString()}, afspraak met {Volunteer.Username} op locatie {Location}"; } }
    }
}