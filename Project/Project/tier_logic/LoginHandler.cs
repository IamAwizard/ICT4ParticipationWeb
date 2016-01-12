using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project
{
    class LoginHandler
    {
        // Fields
        private AccountHandler accm;
        // Constructor   
        public LoginHandler()
        {
            accm = new AccountHandler();
        }

        // Properties

        // Methods
        public bool Authenticate(string email, string password)
        {
            Account foo = accm.FindAccountByEmail(email);
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
            return accm.FindAccountByEmail(email);
        }

        public bool AddAccount(Account newaccount)
        {
            return accm.AddAccount(newaccount);
        }

        public int GetVolunteerIdByEmail(string email)
        {
            return accm.GetVolunteerIdByEmail(email);
        }

        public bool AddVolunteerFilePaths(int volunteerid, string photopath, string vogpath)
        {
            return false;
        }
    }
}