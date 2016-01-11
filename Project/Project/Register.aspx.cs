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
            //Register_Client.Visible = false;
            //Register_Volunteer.Visible = false;

        }
       
        protected void rb_Volunteer_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void rb_Client_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void btn_UploadFoto_Click(object sender, EventArgs e)
        {
          
        }

        protected void btn_Register_Click(object sender, EventArgs e)
        {
            if (rb_Volunteer.Checked == true)

            {
                if (FU_UploadFoto.HasFile == false)
                {
                    string x;
                    x = "alert(\"er is geen Foto ingevuld\");";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", x, true);
                    MaintainScrollPositionOnPostBack = true;
                }
                else if (FU_UploadVOG.HasFile == false)
                {
                    string x;
                    x = "alert(\"er is geen VOG geupload \");";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", x, true);
                    MaintainScrollPositionOnPostBack = true;

                }
            }

            
            else if (tb_Adres.Text == String.Empty)
            {
                string x;
                x = "alert(\"er is geen adres ingevuld\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", x, true);
                MaintainScrollPositionOnPostBack = true;
            }
            else if (tb_Email.Text == String.Empty)
            {
                string x;

                x = "alert(\"er is geen email ingevuld \");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", x, true);
                MaintainScrollPositionOnPostBack = true;
            }
            else if (tb_GebruikersNaam.Text == String.Empty)
            {
                string x;

                x = "alert(\"er is geen gebruikersnaam ingevoerd \");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", x, true);
                MaintainScrollPositionOnPostBack = true;
            }
            else if (tb_Naam.Text == String.Empty)
            {
                string x;

                x = "alert(\"er is geen naam ingevoerd \");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", x, true);
                MaintainScrollPositionOnPostBack = true;
            }
            else if (tb_Telefoonnummer.Text == String.Empty)
            {
                string x;

                x = "alert(\"er is geen telefoonnummer ingevoerd \");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", x, true);
                MaintainScrollPositionOnPostBack = true;
            }
            else if (tb_Wachtwoord.Text == String.Empty)
            {
                string x;

                x = "alert(\"er is geen wachtwoord ingevoerd \");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", x, true);
                MaintainScrollPositionOnPostBack = true;
            }
            else if (tb_Woonplaats.Text == String.Empty)
            {
                string x;

                x = "alert(\"er is geen Woonplaats ingevoerd\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", x, true);
                MaintainScrollPositionOnPostBack = true;
            }
            else if (rb_Client.Checked == false && rb_Client.Checked == false)
            {
                string x;

                x = "alert(\"er is geen type account geselecteerd \");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", x, true);
                MaintainScrollPositionOnPostBack = true;
            }

        }
    }
}