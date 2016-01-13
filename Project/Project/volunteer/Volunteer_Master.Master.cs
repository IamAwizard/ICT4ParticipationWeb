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
            CheckIfUserAllowed();
            UpdateTitleBarLinks();
        }

        private void UpdateTitleBarLinks()
        {
            switch (Request.Url.LocalPath.ToLower())
            {
                case "/volunteer/volunteer_vragen.aspx":
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

        private void CheckIfUserAllowed()
        {
            if (Session["isLoggedIn"] != null)
            {
                Account foo = (Account)Session["currentUser"];
                if (foo is Client)
                {
                    Response.Redirect("~/client/Client_Vragen.aspx");
                }
                if (foo is Admin)
                {
                    Response.Redirect("~/admin/admin_main.aspx");
                }
                else
                {
                    Volunteer bar = foo as Volunteer;
                    lbl_Username.Text = bar.Name;
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
    }
}