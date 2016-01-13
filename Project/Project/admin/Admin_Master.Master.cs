using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project
{
    public partial class Admin_Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckIfUserAllowed();
        }

        private void CheckIfUserAllowed()
        {
            if (Session["isLoggedIn"] != null)
            {
                Account foo = (Account)Session["currentUser"];
                if (foo is Volunteer)
                {
                    Response.Redirect("~/volunteer/Volunteer_Vragen.aspx");
                }
                if (foo is Client)
                {
                    Response.Redirect("~/client/Client_Vragen.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
    }
}