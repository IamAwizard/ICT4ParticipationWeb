using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project
{
    public partial class Client_Chats : System.Web.UI.Page
    {
        ClientHandler clienthandler = new ClientHandler();
        VolunteerHandler volunteerhandler = new VolunteerHandler();
        Chats chats = new Chats();

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckIfUserAllowed();
            Page.MaintainScrollPositionOnPostBack = true;
            ScriptManager_Refresh.RegisterAsyncPostBackControl(Timer_Refresh);
            if (!IsPostBack)
            {
                LoadVolunteers();
                Timer_Refresh.Enabled = false;
            }
            if (IsPostBack)
            {
                if (lbox_Volunteers.SelectedItem != null)
                    RefreshMessages();
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

        private void LoadVolunteers()
        {
            List<Volunteer> volunteers = new List<Volunteer>();
            volunteers = clienthandler.GetVolunteers();
            lbox_Volunteers.DataSource = volunteers;
            lbox_Volunteers.DataValueField = "VolunteerID";
            lbox_Volunteers.DataTextField = "Username";
            lbox_Volunteers.DataBind();
        }

        protected void Timer_Refresh_Tick(object sender, EventArgs e)
        {

        }

        protected void btn_SendMessage_Click(object sender, EventArgs e)
        {
            if (tbox_Bericht.Text != string.Empty)
            {
                if (lbox_Volunteers.SelectedItem != null)
                {
                    Client currentuser = (Client)Session["currentUser"];
                    Volunteer volun = volunteerhandler.GetVolunteer(Convert.ToInt32(lbox_Volunteers.SelectedValue));
                    Chat chat = new Chat(tbox_Bericht.Text, DateTime.Now, currentuser, volun, 1);
                    chats.AddChat(chat);
                    tbox_Bericht.Text = "";
                    RefreshMessages(currentuser, volun);
                }
            }
        }

        private void RefreshMessages()
        {
            Client currentuser = (Client)Session["currentUser"];
            Volunteer volunteer = volunteerhandler.GetVolunteer(Convert.ToInt32(lbox_Volunteers.SelectedValue));
            Timer_Refresh.Enabled = true;
            lbox_chat.Items.Clear();
            List<Chat> chatmessages = new List<Chat>();
            chatmessages = chats.GetChat(currentuser, volunteer);
            foreach (Chat chatmessage in chatmessages)
            {
                lbox_chat.Items.Add(chatmessage.FormattedForClients(currentuser, volunteer));
            }
        }

        private void RefreshMessages(Client client, Volunteer volunteer)
        {
            Timer_Refresh.Enabled = true;
            lbox_chat.Items.Clear();
            List<Chat> chatmessages = new List<Chat>();
            chatmessages = chats.GetChat(client, volunteer);
            foreach (Chat chatmessage in chatmessages)
            {
                lbox_chat.Items.Add(chatmessage.FormattedForClients(client, volunteer));
            }
        }
    }
}
