using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project
{
    public partial class Volunteer_VraagDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadQuestion();

        }

        private void LoadQuestion()
        {
            if(Session["Question"] != null)
            {
                Question q = (Question)Session["Question"];

                lbl_Client.Text = $"Vroeg {q.Author.Name}";
                lbl_Critical.Visible = q.Critical;
                lbl_Date.Text = $"op {q.DateBegin.ToShortDateString()}";
                
                tbox_Location.Text = q.Location;
                tbox_Traveltime.Text = q.TravelTime;
                tbox_Transport.Text = q.Transport.Description;
                tbox_VolunteerCount.Text = q.VolunteersNeeded.ToString();
                lbox_Question.Text = q.Description;
            }
            else
            {
                Response.Redirect("~/volunteer/volunteer_vragen.aspx");
            }
        }
    }
}