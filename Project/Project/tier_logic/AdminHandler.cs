using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}