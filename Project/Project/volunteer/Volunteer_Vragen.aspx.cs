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
        }

        private void LoadQuestions()
        {
            List<Question> allquestions;
            allquestions = volunteerhandler.GetQuestions();
            if (allquestions != null)
            {
                foreach (Question q in allquestions)
                {
                    lbox_Questions.Items.Add(q.ToString());
                }
            }
        }

        protected void lbox_Questions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbox_GetQuestion.SelectedIndex != -1)
            {
                lbox_GetQuestion.Items.Clear();
                var foo = lbox_Questions.SelectedItem;
                lbox_GetQuestion.Items.Add(foo);
            }
        }
    }
}