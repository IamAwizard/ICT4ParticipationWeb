﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project
{
    public class Account
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int AccountID { get; set; }

        public Account(string username, string password, string email)
        {
            this.Email = email;
            this.Username = username;
            this.Password = password;
        }

        public Account(int accountid,string username, string password, string email)
        {
            this.AccountID = accountid;
            this.Email = email;
            this.Username = username;
            this.Password = password;
        }
    }
}