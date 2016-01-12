using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project
{
    public partial class Login : System.Web.UI.Page
    {
        private LoginHandler loginhandler = new LoginHandler();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Login_Click(object sender, EventArgs e)
        {
            if(loginhandler.ValidateCredentials(tbox_Email.Text, tbox_Password.Text))
            {
                Account loggedinuser = loginhandler.GetAccount(tbox_Email.Text);
                if(loggedinuser != null)
                {
                    Session["isLoggedIn"] = true;
                    Session["currentUser"] = loggedinuser;
                    Session.Timeout = 1440;
                    lbl_LoginError.Visible = false;
                    if(loggedinuser is Client)
                    {
                        Response.Redirect("~/client/Client_Vragen.aspx");
                    }
                    if(loggedinuser is Volunteer)
                    {
                        Response.Redirect("~/volunteer/Volunteer_Vragen.aspx");
                    }
                    if (loggedinuser is Admin)
                    {
                        Response.Redirect("~/admin/Admin_Main.aspx");
                    }

                }
                else
                {
                    lbl_LoginError.Visible = true;
                }
            }
            else
            {
                lbl_LoginError.Visible = true;
            }
        }
    }
}
