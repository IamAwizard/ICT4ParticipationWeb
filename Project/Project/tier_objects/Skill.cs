using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project
{
    public class Skill
    {
        public Skill(string description)
        {
            this.Description = description;
        }
        public Skill(int id, string description)
        {
            this.ID = id;
            this.Description = description;
        }
        public int ID { get; set; }
        public string Description { get; set; }
    }
}