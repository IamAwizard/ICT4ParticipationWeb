using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project
{
    public class Meeting
    {
        public Meeting(DateTime date, string location, Client client, Volunteer volunteer)
        {
            this.Date = date;
            this.Location = location;
            this.Client = client;
            this.Volunteer = volunteer;
        }

        public Meeting(int id, DateTime date, string location, Client client, Volunteer volunteer)
        {
            this.ID = id;
            this.Date = date;
            this.Location = location;
            this.Client = client;
            this.Volunteer = volunteer;
        }
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public Client Client { get; set; }
        public Volunteer Volunteer { get; set; }
    }
}