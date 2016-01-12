using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project
{
    public class AccountHandler
    {
        // Fields
        private List<Account> accounts;
        private DatabaseHandler dbm;


        // Constructor
        public AccountHandler()
        {
            accounts = new List<Account>();
            dbm = new DatabaseHandler();
        }

        // Properties
        public List<Account> Accounts
        {
            get { return accounts; }
            set { accounts = value; }
        }

        // Methods
        public Account FindAccountByName(string name)
        {
            throw new NotImplementedException();
        }

        public Account FindAccountByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public bool AddAccount(Account account)
        {
            return dbm.AddAccount(account);
        }

        public int GetVolunteerIdByEmail(string email)
        {
            return dbm.GetVolunteerIdByEmail(email);
        }

        public bool DeleteAccount(Account account)
        {
            throw new NotImplementedException();
        }

        public void Synchronize()
        {
            throw new NotImplementedException();
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