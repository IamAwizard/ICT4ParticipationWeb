using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project
{
    class ChatHandler
    {
        // Properties

        // Constructor
        public ChatHandler()
        {
        }

        public bool CheckIfChatted(User currentuser, User otheruser, out int gesprekid)
        {
            throw new NotImplementedException();
        }

        // Methods
        public bool StartChat(User currentuser, User otheruser)
        {
            //int chatid;
            //if (!CheckIfChatted(currentuser, otheruser, out chatid))
            //{
            //    return DatabaseHandler.StartChat(currentuser, otheruser);
            //}
            //System.Windows.Forms.MessageBox.Show("Gebruikers hebben al een gesprek");
            //return false;
            throw new NotImplementedException();
        }

        public int GetChatID(User currentuser, User otheruser)
        {
            throw new NotImplementedException();
        }

        public List<Chat> GetMessages(int ChatID)
        {
            throw new NotImplementedException();
        }

        public Chat GetChat(int ChatID)
        {
            throw new NotImplementedException();
        }

        public bool SendMessage(Chat message)
        {
            throw new NotImplementedException();
        }

        public List<Chat> GetNewMessages(int ChatID, int MessageID)
        {
            throw new NotImplementedException();
        }
    }
}