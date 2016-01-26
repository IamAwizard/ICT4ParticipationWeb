using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project
{
    public class Chat
    {
        public Chat(string message, DateTime timestamp,Client client, Volunteer volunteer,int sender)
        {
            this.Message = message;
            this.Client = client;
            this.Volunteer = volunteer;
            this.Sender = sender;
            this.TimeStamp = timestamp;
        }
        public Chat(string message, DateTime timestamp,int sender)
        {
            this.Message = message;
            this.Sender = sender;
            this.TimeStamp = timestamp;
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
        public int Sender { get; set; }
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }
        public Client Client { get; set; }
        public Volunteer Volunteer { get; set; }

        public override string ToString()
        {
            if (Sender == 1)
            {
                return $"{TimeStamp.ToShortTimeString()} - {Client.Username}: {Message}";
                //return "Tijd: " + TimeStamp.ToShortTimeString() + " " + "Bericht: " + Message;
            }
            else
            {
                return $"{TimeStamp.ToShortTimeString()} - {Volunteer.Username}: {Message}";
            }
        }

        public string FormattedForClients(Client client, Volunteer volun)
        {
            if(Sender == 1)
            {
                return $"{TimeStamp.ToShortTimeString()} - {client.Username}: {Message}";
            }
            {
                return $"{TimeStamp.ToShortTimeString()} - {volun.Username}: {Message}";
            }
        }
    }
}