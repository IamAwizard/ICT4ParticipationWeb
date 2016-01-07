using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project
{
    public class Transport
    {
        // Constructor
        public Transport(string description)
        {
            this.Description = description;
        }
        public Transport(int id, string description)
        {
            this.ID = id;
            this.Description = description;
        }

        // Properties
        public int ID { get; set; }
        public string Description { get; set; }
    }
}