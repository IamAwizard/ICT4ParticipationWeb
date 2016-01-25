using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project
{
    public class Chats
    {
        // Fields
        DatabaseHandler db;
        // Properties
        public int ChatsID { get; }
        public List<Chat> ChatList { get; set; }

        // Constructor
        public Chats()
        {
            db = new DatabaseHandler();
        }

        public bool AddChat(Chat chat)
        {
            try
            {
                db.AddChatmessage(chat);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public List<Chat> getchat(Client client, Volunteer volunteer)
        {
            return db.GetChat(client, volunteer);
        }
    }
}
