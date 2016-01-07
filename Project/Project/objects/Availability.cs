using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project
{
    public class Availability
    {
        public Availability(string day, string timeofday)
        {
            this.Day = day;
            this.TimeOfDay = timeofday;
        }
        public Availability(int id, string day, string timeofday)
        {
            this.ID = id;
            this.Day = day;
            this.TimeOfDay = timeofday;
        }
        public int ID { get; set; }
        public string Day { get; set; }
        public string TimeOfDay { get; set; }
    }
}