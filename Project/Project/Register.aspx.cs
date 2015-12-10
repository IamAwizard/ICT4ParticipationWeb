using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.IO;



namespace Project
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            dikkebmw.Visible = false;
            dikkeLAMBORGINIFERRARIPENINI.Visible = false;
        }

       
        protected void rb_Volunteer_CheckedChanged(object sender, EventArgs e)
        {
            if(dikkebmw.Visible == true)
            {
                dikkebmw.Visible = false;
                MaintainScrollPositionOnPostBack = true;
            }
            else
            {
                dikkebmw.Visible = true;
                MaintainScrollPositionOnPostBack = true;

            }



        }

        protected void rb_Client_CheckedChanged(object sender, EventArgs e)
        {
            
                 if (dikkeLAMBORGINIFERRARIPENINI.Visible == true)
            {
                dikkeLAMBORGINIFERRARIPENINI.Visible = false;
                MaintainScrollPositionOnPostBack = true;

            }
            else
            {
                dikkeLAMBORGINIFERRARIPENINI.Visible = true;
                MaintainScrollPositionOnPostBack = true;

            }
        }

        protected void btn_UploadFoto_Click(object sender, EventArgs e)
        {
          
        }
    }
}