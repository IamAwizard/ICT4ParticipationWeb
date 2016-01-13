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
            if (IsPostBack)
            {
               
                GetSelectedQuestionDetails();
            }
            else
            {
                LoadQuestions();
            }
        }
        protected void btn_AddQuestion_Click(object sender, EventArgs e)
        {

            if (tbox_AddQuestion.Text.Length > 10 && tbox_AddQuestion.Text.Length < 255)
            {
                if (Session["currentUser"] != null)
                {
                    Client currentuser = (Client)Session["currentUser"];
                    string content = tbox_AddQuestion.Text;
                    int id = currentuser.ClientID;
                    Question question = new Question(id, content, DateTime.Now, 1);
                    questionhandler.AddQuestion(question);
                    Response.Redirect("Client_vragen.aspx");
                    lbl_ErrorMsg.Visible = false;
                }
                else
                {
                    lbl_ErrorMsg.Text = "Niet ingelogd als gebruiker";
                    lbl_ErrorMsg.Visible = true;
                }
            }
            else
            {
                lbl_ErrorMsg.Text = "Inhoud van de vraag is tekort of te lang";
                lbl_ErrorMsg.Visible = true;
            }

        }
        protected void btn_LoadQuestion_Click(object sender, EventArgs e)
        {
            Response.Redirect("Client_VraagDetails.aspx?Question=" + Session["Question"] + "");
        }
        private void GetSelectedQuestionDetails()
        {
            if (lbox_Questions.SelectedItem != null)
            {
                int questionid = Convert.ToInt32(lbox_Questions.SelectedItem.Value);
                Question q = questionhandler.GetQuestionByIDCached(questionid);
                string traveltime = "";
                string location = "";
                string startdate = "Datum: " + q.DateBegin.ToShortDateString();
                string critical = "Urgent: ";
                string volunteersneeded = "Aantal vrijwilligers: " + q.VolunteersNeeded.ToString();
                string transport = "";
              
                if(q.Location != "")
                {
                     location = "Locatie: " + q.Location;
                }
                else
                {
                     location = "Locatie: Niet opgegeven";
                }
                if (q.TravelTime != "")
                {
                    traveltime = "Reistijd: " + q.TravelTime;
                }
                else
                {
                    traveltime = "Reistijd: Niet opgegeven";
                }
                if (q.Critical == true)
                {
                    critical += "JA";
                }
                else
                {
                    critical += "NEE";
                }
                if (q.Transport.Description != "")
                {
                    transport = "Vervoer: " + q.Transport.Description;
                }
                else
                {
                    transport = "Vervoer: Niet opgegeven";
                }
                lbox_getquestion.Items.Clear();
                lbox_getquestion.Items.Add(location);
                lbox_getquestion.Items.Add(traveltime);
                lbox_getquestion.Items.Add(startdate);
                lbox_getquestion.Items.Add(critical);
                lbox_getquestion.Items.Add(volunteersneeded);
                lbox_getquestion.Items.Add(transport);
                Session["Question"] = q.ID;
            }
        }
        private void LoadQuestions()
        {
            List<Question> allquestions;
            allquestions = questionhandler.GetAllQuestions();
            if (allquestions != null)
            {
                lbox_Questions.DataSource = allquestions;
                lbox_Questions.DataValueField = "ID";
                lbox_Questions.DataTextField = "FormattedForVolunteer";
                lbox_Questions.DataBind();
            }
        }
    }
}