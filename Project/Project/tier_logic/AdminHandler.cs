using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Windows.Forms;

namespace Project
{
    class AdminHandler
    {
        // Fields
        QuestionHandler questionhandler;
        ReviewHandler reviewhandler;
        // Constructor   
        public AdminHandler()
        {
            questionhandler = new QuestionHandler();
            reviewhandler = new ReviewHandler();
        }

        // Properties

        // Methods
        public bool DeleteQuestion(Question helprequest)
        {
            if (questionhandler.DeleteQuestion(helprequest))
            {
                return true;
            } else
            {
                return false;
            }
            
        }

        public bool DeleteReview(Review review)
        {
            throw new NotImplementedException();
        }

        public List<Review> GetReviews()
        {
            throw new NotImplementedException();
        }

        public List<Question> GetQuestions()
        {
            return questionhandler.GetAllQuestions();
        }

        public Question GetQuestionByID(int id)
        {
            return questionhandler.GetQuestionByIDCached(id);
        }
    }
}