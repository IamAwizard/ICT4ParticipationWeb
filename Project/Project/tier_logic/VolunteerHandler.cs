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
        public bool UpdateQuestion(Question questiontoupdate)
        {
            if (questions.UpdateQuestion(questiontoupdate))
                return true;
            else
                return false;
        }

        // Methods
        public bool AnswerQuestion(Question question, string answer)
        {
            throw new NotImplementedException();
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

        public List<Account> GetClients()
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
    }
}