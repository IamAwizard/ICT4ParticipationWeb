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
            questions = databasehandler.GetAllOpenQuestions();
        }

        public bool AddQuestion(Question question)
        {
            
            if (databasehandler.AddNewQuestion(question))
                return true;
            else
                return false;
        }

        /// <summary>
        ///  Fills the client property of a question
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public Question ExpandQuestionWithClient(Question question)
        {
            return databasehandler.AddClientToQuestion(question);
        }

        /// <summary>
        /// Fills the AcceptedBy property of a question
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public Question ExpandQuestionWithVolunteers(Question q)
        {
            return databasehandler.ExpandQuestionWithVolunteers(q);
        }

        /// <summary>
        /// Adds a volunteer to the answered list of a question
        /// </summary>
        /// <param name="q"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public bool AnswerQuestion(Question q, Volunteer v)
        {
            return databasehandler.AnswerQuestion(q, v);
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

        public List<Question> GetQuestionsByAuthor(Client author)
        {
            Synchronize();
            try
            {
                return questions.FindAll(x => x.AuthorID == ((Client)author).ClientID);
            }
            catch (NullReferenceException ex)
            {
                return null;
            }
        }

        public Question GetQuestionByIDCached(int questionid)
        {
            Synchronize();
            try
            {
                return questions.Find(x => x.ID == questionid);
            }
            catch (NullReferenceException ex)
            {
                return null;
            }
        }


        public Question GetSingleQuestion(int id,string Discription)
        {
            try
            {
                return databasehandler.GetSingleQuestion(id, Discription);
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