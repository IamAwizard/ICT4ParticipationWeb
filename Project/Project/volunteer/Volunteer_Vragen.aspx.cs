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
            if (!IsPostBack)
            {
                LoadQuestions();
            }
            else
            {
                if (lbox_Questions.SelectedItem != null)
                {
                    lbox_GetQuestion.Items.Clear();
                    lbox_GetQuestion.Items.Add(lbox_Questions.SelectedItem);
                }
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

        protected void lbox_Questions_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}