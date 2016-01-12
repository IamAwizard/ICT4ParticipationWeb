using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project
{
	public partial class Volunteer_Master : System.Web.UI.MasterPage
	{
        protected void Page_Load(object sender, EventArgs e)
        {
            UpdateTitleBarLinks();
        }

        private void UpdateTitleBarLinks()
        {
            switch (Request.Url.LocalPath.ToLower())
            {
                case "/volunteer/cvolunteer_vragen.aspx":
                    link_Questions.Style.Add(HtmlTextWriterStyle.Color, "white");
                    break;
                case "/volunteer/volunteer_afspraken.aspx":
                    link_Meetings.Style.Add(HtmlTextWriterStyle.Color, "white");
                    break;
                case "/volunteer/volunteer_beoordelingen.aspx":
                    link_Reviews.Style.Add(HtmlTextWriterStyle.Color, "white");
                    break;
                case "/volunteer/volunteer_chats.aspx":
                    link_Chats.Style.Add(HtmlTextWriterStyle.Color, "white");
                    break;
                case "/volunteer/volunteer_profiel.aspx":
                    link_Profile.Style.Add(HtmlTextWriterStyle.Color, "white");
                    break;
                default:
                    break;
            }
        }
    }
}