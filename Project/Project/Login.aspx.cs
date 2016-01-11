using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Project.objects;



namespace Project
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        private DatabaseHandler db = new DatabaseHandler();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Login_Click(object sender, EventArgs e)
        {
            if (tbox_Email.Text == String.Empty)
            {
                string x;
                x = "alert(\"er is geen username ingevuld\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", x, true);
                MaintainScrollPositionOnPostBack = true;
            }
            else if (tbox_Password.Text == String.Empty)

            {
                string x;
                x = "alert(\"er is geen Wachtwoord ingevuld\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", x, true);
                MaintainScrollPositionOnPostBack = true;

            }

            if (db.AuthenticateUser(tbox_Email.Text, tbox_Password.Text))
            {
                LoadUser();
                Response.Redirect("Main.aspx");
            }
        }
              private void LoadUser()
        {
            UserCache.UpdateCache();
            Account item = UserCache.ListOfAccounts.Find(x => x.Username == tbox_Email.Text);
            Session["isLoggedIn"] = "true";
            Session["currentUser"] = item;
            Session.Timeout = 2000;
        }

    }
}
