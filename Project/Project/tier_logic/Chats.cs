using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project
{
    public class Chats
    {
        // Fields

        // Properties
        public int ChatsID { get; }
        public List<Chat> ChatList { get; set; }

        // Constructor
        public Chats()
        {
        }
    }
}