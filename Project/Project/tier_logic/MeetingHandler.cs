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
            return dbm.GetMeetings(volunteer);
        }

        public List<Meeting> GetClientMeetings(Client client)
        {
            return dbm.GetMeetings(client);
        }
        public void addmeeting(Meeting meeting)
        {
            try
            {
                dbm.AddMeeting(meeting);
            }
            catch
            {
                
            }
        }
    }
}