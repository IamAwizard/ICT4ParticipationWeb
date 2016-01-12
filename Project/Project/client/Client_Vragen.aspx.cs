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

        }

        protected void btn_AddQuestion_Click(object sender, EventArgs e)
        {
            if(tbox_AddQuestion.Text.Length > 10)
            {
                string content = tbox_AddQuestion.Text;
                int id = Convert.ToInt32(Session["currentUser"]);
                Question question = new Question(id, content, DateTime.Now, 1);
               
                questionhandler.AddQuestion(question);
                lbl_errormsg.Text = "Vraag gemaakt";
            }
            else
            {
                lbl_errormsg.Text = "Inhoud van de vraag is tekort";
            }
            
  
        }

        protected void btn_LoadQuestion_Click(object sender, EventArgs e)
        {

        }
    }
}