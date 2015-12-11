using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Project
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Login_Click(object sender, EventArgs e)
        {
            if (tbox_Email.Text == String.Empty)
            {
                string x;
                x = "alert(\"er is geen email ingevuld\");";
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

        }
    }
}