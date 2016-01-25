using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project
{
    public partial class Client_Afspraken : System.Web.UI.Page
    {
        ClientHandler clienthandler = new ClientHandler();
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckIfUserAllowed();
            if(!IsPostBack)
            {
                LoadMeetings();
            }
            else
            {
                GetSelectedMeetingDetails();
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
                if (foo is Admin)
                {
                    Response.Redirect("~/admin/admin_main.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        private void LoadMeetings()
        {
            List<Meeting> mymeetings;
            Client currentuser = (Client)Session["currentUser"];
            mymeetings = clienthandler.GetMeetings(currentuser);
            if (mymeetings != null)
            {
                lbox_Meetings.DataSource = mymeetings;
                lbox_Meetings.DataValueField = "ID";
                lbox_Meetings.DataTextField = "FormattedForClient";
                lbox_Meetings.DataBind();
                Session["MeetingList"] = mymeetings;
            }
        }

        private void GetSelectedMeetingDetails()
        {
            if (lbox_Meetings.SelectedItem != null && Session["MeetingList"] != null)
            {
                int meetingid = Convert.ToInt32(lbox_Meetings.SelectedItem.Value);
                List<Meeting> ms = (List<Meeting>)Session["MeetingList"];

                Meeting m = ms.Find(x => x.ID == meetingid);
                tbox_Meeting.Text = $"{m.Date.ToString()}\n{m.Volunteer.Name}\n{m.Location}";
            }
        }
    }
}