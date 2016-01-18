using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project
{
    public class MeetingHandler
    {
        private DatabaseHandler dbm;
        public MeetingHandler()
        {
            dbm = new DatabaseHandler();
        }

        public List<Meeting> GetVolunteerMeetings(Volunteer volunteer)
        {
            return null;
        }

        public List<Meeting> GetClientMeetings(Client client)
        {
            return null;
        }
    }
}