using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project
{
    public class VolunteerHandler
    {
        // Fields
        Volunteer currentuser;
        DatabaseHandler databasehandler;
        ChatHandler chathandler;
        QuestionHandler questions;
        AccountHandler accounthandler;

        // Properties

        // Constructor
        public VolunteerHandler()
        {
            databasehandler = new DatabaseHandler();
            questions = new QuestionHandler();
            accounthandler = new AccountHandler();
        }

        // Methods
        public bool UpdateQuestion(Question questiontoupdate)
        {
            if (questions.UpdateQuestion(questiontoupdate))
                return true;
            else
                return false;
        }

        public Question ExpandQuestionsWithClient(Question question)
        {
            return questions.ExpandQuestionWithClient(question);
        }

        public Question ExpandQuestionWithVolunteers(Question question)
        {
            return questions.ExpandQuestionWithVolunteers(question);
        }

        public bool AnswerQuestion(Question question, Volunteer volunteer)
        {
            return questions.AnswerQuestion(question, volunteer);
        }

        public bool UpdateProfile(bool driverslisence, string biography, string pathtophoto)
        {
            throw new NotImplementedException();
        }


        public Volunteer GetUserInfo()
        {
            return currentuser;
        }

        public List<Question> GetQuestions()
        {

            return questions.GetAllQuestions();
        }

        public Question GetQuestionByIDfromCache(int questionid)
        {
            return questions.GetQuestionByIDCached(questionid);
        }
        public List<Client> GetClients()
        {
            throw new NotImplementedException();
        }

        public Availability GetSchedule()
        {
            throw new NotImplementedException();
        }

        public bool DeleteVolunteer(Volunteer volunteer)
        {
            if (accounthandler.DeleteAccount(volunteer))
                return true;
            else
                return false;
        }

        public Volunteer ExtendVolunteer(Volunteer volunteer)
        {
            return accounthandler.ExtendVolunteer(volunteer);
        }

        public bool UpdateVolunteer(Volunteer volunteer)
        {
            return accounthandler.UpdateVolunteer(volunteer);
        }

        public List<Meeting> GetMyAppointments(Volunteer volunteer)
        {
            return databasehandler.GetMyAppointments(volunteer);
        }

        public List<Review> GetMyReviews(Volunteer volunteer)
        {

            return databasehandler.GetMyReviews(volunteer);
        }

        public List<Availability> GetAvailability(int ID)
        {

            return databasehandler.GetAvailability(ID);
        }
        public bool SetAvailablilty(Availability available)
        {
            try
            {
                 databasehandler.SetAvailability(available);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}