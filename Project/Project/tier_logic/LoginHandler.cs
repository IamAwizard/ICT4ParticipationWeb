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
        /// <summary>
        ///  Checks if a account exists in the database
        /// </summary>
        /// <param name="email">email to authenticate with</param>
        /// <param name="password">password to authenticate with</param>
        /// <returns>true if exists</returns>
        public bool ValidateCredentials(string email, string password)
        {
            return accm.ValidateCredentials(email, password);
        }

        /// <summary>
        /// Gets the account associated with the given email
        /// </summary>
        /// <param name="email">email of account to get</param>
        /// <returns>account if found, otherwise null</returns>
        public Account GetAccount(string email)
        {
            return accm.GetAccount(email);
        }

        /// <summary>
        /// Add a account, not compatible with admins
        /// </summary>
        /// <param name="newaccount">account to add</param>
        /// <returns></returns>
        public bool AddAccount(Account newaccount)
        {
            return accm.AddAccount(newaccount);
        }
    }
}