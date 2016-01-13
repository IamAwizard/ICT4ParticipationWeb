using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project
{
    public class AccountHandler
    {
        // Fields
        private DatabaseHandler dbm;


        // Constructor
        public AccountHandler()
        {
            dbm = new DatabaseHandler();
        }

        // Properties

        // Methods
        /// <summary>
        /// Adds an account to the DB
        /// </summary>
        /// <param name="account">account to add</param>
        /// <returns>true if success</returns>
        public bool AddAccount(Account account)
        {
            return dbm.AddAccount(account);
        }

        /// <summary>
        /// Checks if a account exists in the database
        /// </summary>
        /// <param name="email">email to authenticate with</param>
        /// <param name="password">password to authenticate with</param>
        /// <returns>true is exists, otherwise false</returns>
        public bool ValidateCredentials(string email, string password)
        {
            return dbm.ValidateCredentials(email, password);
        }

        /// <summary>
        /// Gets a complete account from the database by email
        /// </summary>
        /// <param name="email">email of account to fetch</param>
        /// <returns>account, or null if none found</returns>
        public Account GetAccountByEmail(string email)
        {
            return dbm.GetAccount(email);
        }

        public bool DeleteAccount(Account account)
        {
            return dbm.DeleteAccount(account);
        }

        public Volunteer ExtendVolunteer(Volunteer volun)
        {
            throw new NotImplementedException();
        }

        public bool UpdateVolunteer(Volunteer volun)
        {
            throw new NotImplementedException();
        }
    }
}