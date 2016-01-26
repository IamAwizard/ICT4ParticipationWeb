using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project
{
    public partial class Volunteer_Chats : System.Web.UI.Page
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
                LoadClients();
                Timer_Refresh.Enabled = false;
            }
            if (IsPostBack)
            {
                if (lbox_Clients.SelectedItem != null)
                    RefreshMessages();
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
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        private void LoadClients()
        {
            List<Client> clients = new List<Client>();
            clients = volunteerhandler.GetClients();
            lbox_Clients.DataSource = clients;
            lbox_Clients.DataValueField = "ClientID";
            lbox_Clients.DataTextField = "Username";
            lbox_Clients.DataBind();
        }

        protected void Timer_Refresh_Tick(object sender, EventArgs e)
        {

        }

        protected void btn_SendMessage_Click(object sender, EventArgs e)
        {
            if (tbox_Message.Text != string.Empty)
            {
                if (lbox_Clients.SelectedItem != null)
                {
                    Volunteer currentuser = (Volunteer)Session["currentUser"];
                    Client client = volunteerhandler.GetClient(Convert.ToInt32(lbox_Clients.SelectedValue));
                    Chat chat = new Chat(tbox_Message.Text, DateTime.Now, client, currentuser, 0);
                    chats.AddChat(chat);
                    tbox_Message.Text = "";
                    RefreshMessages(client, currentuser);
                }
            }
        }

        private void RefreshMessages()
        {
            Volunteer currentuser = (Volunteer)Session["currentUser"];
            Client client = volunteerhandler.GetClient(Convert.ToInt32(lbox_Clients.SelectedValue));
            Timer_Refresh.Enabled = true;
            lbox_chat.Items.Clear();
            List<Chat> chatmessages = new List<Chat>();
            chatmessages = chats.GetChat(client, currentuser);
            foreach (Chat chatmessage in chatmessages)
            {
                lbox_chat.Items.Add(chatmessage.FormattedForClients(client, currentuser));
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