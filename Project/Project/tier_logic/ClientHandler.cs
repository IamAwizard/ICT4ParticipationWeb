using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project
{
    class ClientHandler
    {
        // Fields
        MeetingHandler meetinghandler;
        QuestionHandler questionhandler;
        ReviewHandler reviewhandler;
        AccountHandler accounthandler;
        DatabaseHandler databasehandler;

        // Constructor
        public ClientHandler()
        {
            meetinghandler = new MeetingHandler();
            questionhandler = new QuestionHandler();
            reviewhandler = new ReviewHandler();
            accounthandler = new AccountHandler();
            databasehandler = new DatabaseHandler();
        }

        // Properties
        public Client CurrentUser { get; set; }

        // Methods
        public bool AddQuestion(int auteur, string location, string content)
        {
            throw new NotImplementedException();
        }

        public bool AddQuestion(int auteur, string location, string discrepancy, string content)
        {
            throw new NotImplementedException();
        }

        public bool UpdateQuestion(Question questiontoupdate)
        {
            throw new NotImplementedException();
        }

        public bool AddReview(Review review)
        {
            throw new NotImplementedException();
        }

        public bool AddMeeting(Volunteer volunteer, DateTime date, string location)
        {
            throw new NotImplementedException();
        }

        public List<Question> GetMyQuestions()
        {
            throw new NotImplementedException();
        }

        public bool DeleteClient()
        {
            throw new NotImplementedException();
        }

        public List<User> GetVolunteers()
        {
            throw new NotImplementedException();
        }

        public Volunteer ExtendVolunteer(Volunteer volunteer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the meetings for this client
        /// </summary>
        /// <returns></returns>
        public List<Meeting> GetMeetings(Client client)
        {
            return meetinghandler.GetClientMeetings(client);
        }

        public List<Review> GetReviews()
        {
            throw new NotImplementedException();
        }
        public string GetUsername(int ID)
        {
            try
            {
                string username = databasehandler.GetClientUserName(ID);
                return username;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
        }
        public List<Transport> GetTransports()
        {
            List<Transport> transports = new List<Transport>();
            try
            {
                transports = databasehandler.GetTransports();
                return transports;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
        }
        public int GetTransportID(string description)
        {
            int ID = 0;
            try
            {
                ID = databasehandler.GetSingleTransport(description);
                return ID;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return 0;
            }
        }

        public Question ExpandQuestionsWithClient(Question question)
        {
            return questionhandler.ExpandQuestionWithClient(question);
        }

        public Question ExpandQuestionWithVolunteers(Question question)
        {
            return questionhandler.ExpandQuestionWithVolunteers(question);
        }
    }
}
