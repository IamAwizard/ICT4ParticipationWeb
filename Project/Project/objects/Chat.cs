using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project
{
    public class Chat
    {
        public Chat(string message, Client client, Volunteer volunteer)
        {
            this.Message = message;
            this.Client = client;
            this.Volunteer = volunteer;
            this.TimeStamp = DateTime.Now;
        }
        public Chat(int id, string message, DateTime timestamp,  Client client, Volunteer volunteer)
        {
            this.ID = id;
            this.Message = message;
            this.TimeStamp = timestamp;
            this.Client = client;
            this.Volunteer = volunteer;
        }
        public int ID { get; set; }
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }
        public Client Client { get; set; }
        public Volunteer Volunteer { get; set; }
    }
}