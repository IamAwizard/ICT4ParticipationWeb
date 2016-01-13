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
        public Availability(int id, string day, string timeofday,int volunid)
        {
            this.ID = id;
            this.Day = day;
            this.TimeOfDay = timeofday;
            this.volunid = volunid;
        }
        public int ID { get; set; }
        public string Day { get; set; }
        public string TimeOfDay { get; set; }
        public int volunid { get; set; }
    }
}