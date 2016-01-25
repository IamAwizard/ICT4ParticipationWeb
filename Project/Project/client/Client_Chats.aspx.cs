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

        protected void btn_Loadbericht_Click(object sender, EventArgs e)
        {
          
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            Client currentuser = (Client)Session["currentUser"];
            if (!IsPostBack)
            {
                List<Volunteer> volunteers = new List<Volunteer>();
                volunteers = clienthandler.GetVolunteers();
                lbox_Clients.DataSource = volunteers;
                lbox_Clients.DataValueField = "VolunteerID";
                lbox_Clients.DataTextField = "Name";
                lbox_Clients.DataBind();

            }
            if (IsPostBack)
            {
                if (tbox_Bericht.Text != "")
                {
                    Volunteer volun = volunteerhandler.getvolunteer(Convert.ToInt32(lbox_Clients.SelectedValue));
                    Chat chat = new Chat(tbox_Bericht.Text, DateTime.Now, currentuser, volun, 1);
                    chats.AddChat(chat);
                    tbox_Bericht.Text = "";
                }

                lbox_chat.Items.Clear();
                Volunteer volunteer = volunteerhandler.getvolunteer(Convert.ToInt32(lbox_Clients.SelectedValue));
                List<Chat> chatmessages = new List<Chat>();
                chatmessages = chats.getchat(currentuser, volunteer);
                if (chatmessages.Count != 0)
                {
                    foreach (Chat chatmessage in chatmessages)
                    {
                        lbox_chat.Items.Add(chatmessage.TimeStamp.ToShortTimeString());
                        lbox_chat.Items.Add(chatmessage.Message);
                    }
                }
      
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            
                Client currentuser = (Client)Session["currentUser"];
                lbox_chat.Items.Clear();
                Volunteer volunteer = volunteerhandler.getvolunteer(Convert.ToInt32(lbox_Clients.SelectedValue));
                List<Chat> chatmessages = new List<Chat>();
                chatmessages = chats.getchat(currentuser, volunteer);
                if (chatmessages.Count != 0)
                {
                    foreach (Chat chatmessage in chatmessages)
                    {
                        lbox_chat.Items.Add(chatmessage.TimeStamp.ToShortTimeString());
                    lbox_chat.Items.Add(chatmessage.Message);
                    }
                }

            }
         
        }
    }
