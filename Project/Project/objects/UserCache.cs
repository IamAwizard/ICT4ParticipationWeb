using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project.objects;
using Oracle.DataAccess;

namespace Project.objects
{
    public static class UserCache
    {
        //fields
      private static  DatabaseHandler db = new DatabaseHandler();
     

        public static List<Account> ListOfAccounts
        { get; set; }

        public static void UpdateCache()
        {
            List<Account> item = db.GetUserCache();
        }

    }
}