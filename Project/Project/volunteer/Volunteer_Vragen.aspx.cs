using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project
{
    public partial class Volunteer_Vragen : System.Web.UI.Page
    {
        private VolunteerHandler volunteerhandler = new VolunteerHandler();
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

        private void LoadQuestions()
        {
            List<Question> allquestions;
            allquestions = volunteerhandler.GetQuestions();
            if (allquestions != null)
            {
                lbox_Questions.DataSource = allquestions;
                lbox_Questions.DataValueField = "ID";
                lbox_Questions.DataTextField = "FormattedForVolunteer";
                lbox_Questions.DataBind();
            }
        }

        private void GetSelectedQuestionDetails()
        {
            if (lbox_Questions.SelectedItem != null)
            {
                int questionid = Convert.ToInt32(lbox_Questions.SelectedItem.Value);
                Question q = volunteerhandler.GetQuestionByIDfromCache(questionid);
                tbox_GetQuestion.Text = q.Description;
                lbl_Date.Text = $"Datum: {q.DateBegin.ToShortDateString()}";
                lbl_Location.Text = $"Locatie: {q.Location}";
                lbl_VolunteersNeeded.Text = $"Vrijwilligers nodig: {q.VolunteersNeeded}";
            }
        }

        protected void btn_AnswerQuestion_Click(object sender, EventArgs e)
        {
            if (lbox_Questions.SelectedItem != null)
            {
                int questionid = Convert.ToInt32(lbox_Questions.SelectedItem.Value);
                Question q = volunteerhandler.GetQuestionByIDfromCache(questionid);
                q = volunteerhandler.ExpandQuestionsWithClient(q);
                q = volunteerhandler.ExpandQuestionWithVolunteers(q);
                Session["Question"] = q;
                Response.Redirect("~/volunteer/volunteer_vraagdetails.aspx");
            }
        }
    }
}