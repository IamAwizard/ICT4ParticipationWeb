using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project
{
    class ClientHandler
    {
        // Fields
        ChatHandler chathandler;
        QuestionHandler questionhandler;
        ReviewHandler reviewhandler;
        AccountHandler accounthandler;
        DatabaseHandler databasehandler = new DatabaseHandler();

        // Constructor   
        public ClientHandler(Client activeuser)
        {
            questionhandler = new QuestionHandler();
            reviewhandler = new ReviewHandler();
            accounthandler = new AccountHandler();

            CurrentUser = activeuser;
        }

        public ClientHandler()
        {
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

        public List<Meeting> GetMeetings()
        {
            throw new NotImplementedException();
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
