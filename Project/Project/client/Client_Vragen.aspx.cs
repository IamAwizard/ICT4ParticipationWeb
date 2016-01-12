using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project
{
    public partial class Client_Vragen : System.Web.UI.Page
    {
        QuestionHandler questionhandler = new QuestionHandler();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["currentUser"] != null)
            {
                lbox_Questions.Items.Clear();
                Client currentuser = (Client)Session["currentUser"];
                List<Question> questions = new List<Question>();
                questions = questionhandler.GetQuestionsByAuthor(currentuser);
                if(questions.Count != 0)
                {
                    foreach (Question Q in questions)
                    {
                        string date = Q.DateBegin.ToShortDateString();
                        string discription = Q.Description;
                        lbox_Questions.Items.Add(date);
                        lbox_Questions.Items.Add(discription);
                    }
                }     
            }
        }

        protected void btn_AddQuestion_Click(object sender, EventArgs e)
        {
           
            if (tbox_AddQuestion.Text.Length > 10)
            {
                if (Session["currentUser"] != null)
                {
                    Client currentuser = (Client)Session["currentUser"];
                    string content = tbox_AddQuestion.Text;
                    int id = currentuser.ClientID;
                    Question question = new Question(id, content, DateTime.Now, 1);

                    lbl_errormsg.ForeColor = System.Drawing.Color.Green;
                    questionhandler.AddQuestion(question);
                    lbl_errormsg.Text = "Vraag gemaakt";
                    Response.Redirect("Client_vragen.aspx");
             
                }
                
            }
            else
            {
                lbl_errormsg.ForeColor = System.Drawing.Color.Red;
                lbl_errormsg.Text = "Inhoud van de vraag is tekort";
            }
         
        }

        protected void btn_LoadQuestion_Click(object sender, EventArgs e)
        {

        }
    }
}