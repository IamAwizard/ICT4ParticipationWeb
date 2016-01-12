using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["isLoggedIn"] != null)
            {
                if ((bool)Session["isLoggedIn"] == true)
                {
                    Session.Abandon();
                    Response.AddHeader("REFRESH", "2;URL=Default.aspx");
                }
            }
            else
            {
                lbl_Error.Visible = true;
                Response.AddHeader("REFRESH", "3;URL=Default.aspx");
            }
        }
    }
}