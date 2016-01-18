using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project
{
    public partial class Uitschrijven : System.Web.UI.Page
    {
        LoginHandler loginhandler = new LoginHandler();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["currentUser"] == null)
            {
                Response.Redirect("~/login.aspx");
            }
        }

        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["sender"] != null)
            {
                string qstring = Request.QueryString["sender"];
                if(qstring.ToLower() == "client")
                {
                    Response.Redirect("~/client/client_vragen.aspx");
                }
                else if(qstring.ToLower() == "volunteer")
                {
                    Response.Redirect("~/volunteer/volunteer_vragen.aspx");
                }
                else
                {
                    Response.Redirect("~/login.aspx");
                }
            }
        }

        protected void btn_Ok_Click(object sender, EventArgs e)
        {
            if (Session["currentUser"] != null)
            {
                Account foo = (Account)Session["currentUser"];
                if(loginhandler.DeleteAccount(foo))
                {
                    Response.Redirect("~/logout.aspx");
                }
                else
                {
                    lbl_ErrorMsg.Visible = true;
                }
            }
        }
    }
}