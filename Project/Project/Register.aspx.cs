using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            MaintainScrollPositionOnPostBack = true;
            ShowExtendedForm();
        }

        protected void rb_Volunteer_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtn_Volunteer.Checked)
            {
                lbl_RegisteringAs.Text = "<h4 class=\"text-center\">Vrijwilliger</h4>";
                lbl_Volunteer.CssClass = "expanded button";
                lbl_Client.CssClass = "expanded secondary button";
                div_Register_Volunteer.Visible = true;
                div_UserInformation.Visible = true;
            }
            else
            {
                div_Register_Volunteer.Visible = false;
            }
        }

        protected void rb_Client_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_Client.Checked)
            {
                lbl_RegisteringAs.Text = "<h4 class=\"text-center\">Hulpbehoevende</h4>";
                lbl_Client.CssClass = "expanded button";
                lbl_Volunteer.CssClass = "expanded secondary button";
                div_Register_Client.Visible = true;
                div_UserInformation.Visible = true;
            }
            else
            {
                div_Register_Client.Visible = false;
            }
        }

        protected void btn_Register_Volunteer_Click(object sender, EventArgs e)
        {
            if(CheckUserInput())
            {

            }
        }

        protected void btn_Register_Client_Click(object sender, EventArgs e)
        {
            if (CheckUserInput())
            {
                string username = tbox_Username.Text;
                string password = tbox_Password.Text;
                string email = tbox_Email.Text;
                string givenname = tbox_Email.Text;
                string adress = tbox_GivenName.Text;
                string location = tbox_Location.Text;
                string phonenumber = tbox_PhoneNumber.Text;
                string haslicense = cbox_HasLicense.Checked.ToString();
                string hascar = cbox_HasCar.Checked.ToString();
                string ovpossible = cbox_OVPossible.Checked.ToString();
                Client newclient = new Client(username, password, email, givenname, adress, location, phonenumber, haslicense, hascar, ovpossible);
                // TODO SEND TO DB
            }
        }

        private void ShowExtendedForm()
        {
            if (!rbtn_Volunteer.Checked)
            {
                div_Register_Volunteer.Visible = false;
            }
            if (!rbtn_Client.Checked)
            {
                div_Register_Client.Visible = false;
            }
            if(!rbtn_Client.Checked && !rbtn_Volunteer.Checked)
            {
                div_UserInformation.Visible = false;
            }
        }

        private bool CheckIfPasswordsMatch(string password, string passwordrepeat)
        {
            if(password == passwordrepeat)
            {
                return true;
            }
            return false;
        }

        private bool CheckUserInput()
        {
            bool check = true;
            // Username
            if(tbox_Username.Text.Length > 2 && tbox_Username.Text.Length <= 30)
            {
                lbl_UsernameError.Visible = false;
            }
            else
            {
                lbl_UsernameError.Visible = true;
                check = false;
            }
            // Password
            string password = tbox_Password.Text;
            string confirm = tbox_PasswordConfirm.Text;
            if (password.Length > 2 && password.Length <= 50 && CheckIfPasswordsMatch(password, confirm))
            {
                lbl_PasswordError.Visible = false;
            }
            else
            {
                lbl_PasswordError.Visible = true;
                check = false;
            }
            // Email
            var emailchecker = new EmailAddressAttribute();
            if(emailchecker.IsValid(tbox_Email.Text) && tbox_Email.Text.Length <= 50)
            {
                lbl_EmailError.Visible = false;
            }
            else
            {
                lbl_EmailError.Visible = true;
                check = false;
            }
            // Name
            if (tbox_GivenName.Text.Length > 1 && tbox_GivenName.Text.Length <= 30)
            {
                lbl_GivenNameError.Visible = false;
            }
            else
            {
                lbl_GivenNameError.Visible = true;
                check = false;
            }
            // Adress
            if (tbox_Adress.Text.Length > 2 && tbox_Adress.Text.Length <= 50)
            {
                lbl_AdressError.Visible = false;
            }
            else
            {
                lbl_AdressError.Visible = true;
                check = false;
            }
            // Location
            if (tbox_Location.Text.Length > 1 && tbox_Location.Text.Length <= 30)
            {
                lbl_LocationError.Visible = false;
            }
            else
            {
                lbl_LocationError.Visible = true;
                check = false;
            }
            // Phonenumber
            if (tbox_PhoneNumber.Text.Length == 10)
            {
                lbl_PhoneNumerError.Visible = false;
            }
            else
            {
                lbl_PhoneNumerError.Visible = true;
                check = false;
            }
            return check;
        }

        private void CheckVolunteerInput()
        {

        }

    }
}