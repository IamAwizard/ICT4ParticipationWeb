using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project
{
    class LoginHandler
    {
        // Fields
        AccountHandler accounthandler;
        // Constructor   
        public LoginHandler()
        {
            accounthandler = new AccountHandler();
        }

        // Properties

        // Methods
        public bool Authenticate(string email, string password)
        {
            Account foo = accounthandler.FindAccountByEmail(email);
            if (foo != null)
            {
                if (foo.Password == password)
                {
                    return true;
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(@"Foute email/wachtwoord combinatie");
                    return false;
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show(@"Email niet gevonden");
                return false;
            }
        }

        public Account GetUser(string email)
        {
            return accounthandler.FindAccountByEmail(email);
        }

        public bool AddClient(Client newclient)
        {
            return false;
        }

        public bool AddVolunteer(Volunteer newvolunteer)
        {
            return false;
        }
    }
}