using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project
{
    class QuestionHandler
    {
        // Fields
        DatabaseHandler databasehandler;
        private List<Question> questions;

        // Constructor   
        public QuestionHandler()
        {
            databasehandler = new DatabaseHandler();
        }

        // Properties
        public List<Question> QuestionList
        {

            get { return questions; }
        }

        // Methods
        public void Synchronize()
        {
            questions = databasehandler.getquestions();
            throw new NotImplementedException();
        }

        public bool AddQuestion(Question question)
        {
            
            if (databasehandler.AddNewQuestion(question))
                return true;
            else
                return false;
        }

        public bool DeleteQuestion(Question question)
        {
            if (databasehandler.DeleteQuestion(question.ID))
                return true;
            else
                return false;
        }

        public bool UpdateQuestion(Question questiontoupdate)
        {
            if (databasehandler.UpdateQuestion(questiontoupdate))
                return true;
            else
                return false;
        }

        public List<Question> GetQuestionsByAuthor(Account author)
        {
            Synchronize();
            try
            {
                return questions.FindAll(x => x.AuthorID == author.AccountID);
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