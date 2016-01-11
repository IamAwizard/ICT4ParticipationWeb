using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project
{
    class QuestionHandler
    {
        // Fields
        private List<Question> questions;

        // Constructor   
        public QuestionHandler()
        {

        }

        // Properties
        public List<Question> QuestionList
        {

            get { return questions; }
        }

        // Methods
        public void Synchronize()
        {
            // get all questions
            throw new NotImplementedException();
        }

        public bool AddQuestion(Question question)
        {
            if (DatabaseHandler.AddQuestion(question))
                return true;
            else
                return false;
        }

        public bool DeleteQuestion(Question question)
        {
            if (DatabaseHandler.DeleteQuestion(question.ID))
                return true;
            else
                return false;
        }

        public bool UpdateQuestion(Question questiontoupdate)
        {
            if (DatabaseHandler.UpdateQuestion(questiontoupdate))
                return true;
            else
                return false;
        }

        public List<Question> GetQuestionsByAuthor(Account author)
        {
            Synchronize();
            try
            {
                return questions.FindAll(x => x.ID == author.AccountID);
            }
            catch (NullReferenceException ex)
            {
                return null;
            }
        }

        public List<Question> GetAllQuestions()
        {
            Synchronize();
            return QuestionList;
        }


    }
}