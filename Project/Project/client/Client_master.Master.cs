using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project
{
    public partial class Client_Master : System.Web.UI.MasterPage
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
                case "/client/client_vragen.aspx":
                    link_Questions.Style.Add(HtmlTextWriterStyle.Color, "white");
                    break;
                case "/client/client_afspraken.aspx":
                    link_Meetings.Style.Add(HtmlTextWriterStyle.Color, "white");
                    break;
                case "/client/client_beoordelingen.aspx":
                    link_Reviews.Style.Add(HtmlTextWriterStyle.Color, "white");
                    break;
                case "/client/client_chats.aspx":
                    link_Chats.Style.Add(HtmlTextWriterStyle.Color, "white");
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
                if (foo is Volunteer)
                {
                    Response.Redirect("~/volunteer/Volunteer_Vragen.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
    }
}