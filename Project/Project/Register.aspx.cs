using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web.UI;

namespace Project
{
    public partial class Register : System.Web.UI.Page
    {
        // Max file sizes for upload in KB
        const int MaxVOGFileSize = 3072; // 3MB
        const int MaxPhotoFileSize = 1024; // 1MB

        // Login handler
        private LoginHandler loginhandler = new LoginHandler();

        // Page load event
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            ShowExtendedForm();
        }

        /// <summary>
        /// Type of user selection changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rb_Volunteer_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_Volunteer.Checked)
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

        /// <summary>
        /// Type of user selection changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Button click to register a volunteer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Register_Volunteer_Click(object sender, EventArgs e)
        {
            if (CheckUserInput())
            {
                DateTime dateofbirth;
                if (CheckVolunteerInput(out dateofbirth))
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
                    string photofilename = DateTime.Now.ToString("yyyyMMddmmss") + Path.GetFileName(FU_UploadPhoto.FileName);
                    string vogfilename = DateTime.Now.ToString("yyyyMMddmmss") + Path.GetFileName(FU_UploadVog.FileName);

                    Volunteer newvolunteer = new Volunteer(username, password, email, givenname, adress, location, phonenumber, haslicense, hascar, dateofbirth, photofilename, vogfilename);

                    if (loginhandler.AddAccount(newvolunteer))
                    {
                        FU_UploadPhoto.SaveAs(Server.MapPath("~/profileimg/") + photofilename);
                        lbl_UploadPhotoError.Text = "SUCCESS";
                        FU_UploadVog.SaveAs(Server.MapPath("~/vog/") + vogfilename);
                        lbl_UploadPhotoError.Text = "SUCCESS";

                        Response.Redirect("login.aspx");
                    }
                    else
                    {

                    }
                }

            }
        }

        /// <summary>
        /// Buttonclick to register a client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                if (loginhandler.AddAccount(newclient))
                {
                    Response.Redirect("login.aspx");
                }
            }
        }

        /// <summary>
        /// Tries to not accidently close forms when a postback occurs.
        /// </summary>
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
            if (!rbtn_Client.Checked && !rbtn_Volunteer.Checked)
            {
                div_UserInformation.Visible = false;
            }
        }

        /// <summary>
        /// Compares passwords.
        /// </summary>
        /// <param name="password">given password</param>
        /// <param name="passwordrepeat">password confirmation</param>
        /// <returns>true if the same</returns>
        private bool CheckIfPasswordsMatch(string password, string passwordrepeat)
        {
            if (password == passwordrepeat)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///  Checks general user input
        /// </summary>
        /// <returns>true if all valid</returns>
        private bool CheckUserInput()
        {
            bool check = true;
            // Username
            if (tbox_Username.Text.Length > 2 && tbox_Username.Text.Length <= 30)
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
            if (emailchecker.IsValid(tbox_Email.Text) && tbox_Email.Text.Length <= 50)
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

        /// <summary>
        /// Checks for file uploads and date
        /// </summary>
        private bool CheckVolunteerInput(out DateTime dateofbirth)
        {
            bool check = true;
            dateofbirth = DateTime.MinValue;
            // Date of birth
            if (tbox_Year.Text.Length < 4)
            {
                lbl_BirthDateError.Visible = true;
                return false;
            }
            string day = tbox_Day.Text;
            string month = ddl_Month.SelectedValue;
            string year = tbox_Year.Text;
            string date = $"{day}-{month}-{year}";
            string dateformat = "d-m-yyyy";
            ;
            if (!DateTime.TryParseExact(date, dateformat, System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, out dateofbirth))
            {
                lbl_BirthDateError.Visible = true;
                check = false;
            }
            else
            {
                lbl_BirthDateError.Visible = false;
            }
            // Photo
            if (FU_UploadPhoto.HasFile)
            {
                try
                {
                    if (FU_UploadPhoto.PostedFile.ContentType == "image/jpeg" || FU_UploadPhoto.PostedFile.ContentType == "image/png")
                    {
                        if (FU_UploadPhoto.PostedFile.ContentLength < (MaxPhotoFileSize * 1000))
                        {
                            lbl_UploadPhotoError.Visible = false;
                        }
                        else
                        {
                            lbl_UploadPhotoError.Text = $"Upload mislukt: bestand moet kleiner zijn dan {MaxPhotoFileSize} kB!";
                            lbl_UploadPhotoError.Visible = true;
                            check = false;
                        }
                    }
                    else
                    {
                        lbl_UploadPhotoError.Text = "Upload mislukt: Alleen JPEG of PNG bestanden toegestaan!";
                        lbl_UploadPhotoError.Visible = true;
                        check = false;
                    }
                }
                catch (Exception ex)
                {
                    lbl_UploadPhotoError.Text = "Iets ging fout. " + ex.Message;
                    lbl_UploadPhotoError.Visible = true;
                    check = false;
                }
            }
            else
            {
                lbl_UploadPhotoError.Text = "Geen bestand gevonden";
                lbl_UploadPhotoError.Visible = true;
                check = false;
            }
            // VOG
            if (FU_UploadVog.HasFile)
            {
                try
                {
                    if (FU_UploadVog.PostedFile.ContentType == "application/pdf")
                    {
                        if (FU_UploadVog.PostedFile.ContentLength < (MaxVOGFileSize * 1000))
                        {
                            lbl_UploadVogError.Visible = false;
                        }
                        else
                        {
                            lbl_UploadVogError.Text = $"Upload mislukt: bestand moet kleiner zijn dan {MaxVOGFileSize} kB!";
                            lbl_UploadVogError.Visible = true;
                            check = false;
                        }
                    }
                    else
                    {
                        lbl_UploadVogError.Text = $"Upload mislukt: Alleen PDF bestanden toegestaan!";
                        lbl_UploadVogError.Visible = true;
                        check = false;
                    }
                }
                catch (Exception ex)
                {
                    lbl_UploadVogError.Text = "Iets ging fout. " + ex.Message;
                    lbl_UploadVogError.Visible = true;
                    check = false;
                }
            }
            else
            {
                lbl_UploadVogError.Text = "Geen bestand gevonden";
                lbl_UploadVogError.Visible = true;
                check = false;
            }
            return check;
        }
    }
}