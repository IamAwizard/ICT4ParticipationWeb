using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project
{
    public class Review
    {
        public Review(int rating, string comments)
        {
            this.Rating = rating;
            this.Comments = comments;
        }
        public Review(int id, int rating, string comments)
        {
            this.ID = id;
            this.Rating = rating;
            this.Comments = comments;
        }

        public Review(int id, int rating, string comments, int volunteerid, int clientid)
        {
            this.ID = id;
            this.Rating = rating;
            this.Comments = comments;
            this.ClientID = clientid;
            this.VolunteerID = volunteerid;
        }


        public int ID { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
        public int VolunteerID { get; set; }
        public int ClientID { get; set; }
        
        
    }
}