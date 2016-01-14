using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project
{
    public partial class Volunteer_Profiel : System.Web.UI.Page
    {
        VolunteerHandler volunhandler = new VolunteerHandler();
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckIfUserAllowed();
            if (IsPostBack)
            {
                UpdateAvailability();
            }
            SetValues();
        }

        private int getIndex(string timeofday)
        {
            switch (timeofday.ToLower())
            {
                case "niet beschikbaar":
                    return 1;
                case "ochtend":
                    return 2;
                case "middag":
                    return 3;
                case "namiddag":
                    return 4;
                case "avond":
                    return 5;
                case "nacht":
                    return 6;
                default:
                    return -1;

            }
        }

        private void CheckIfUserAllowed()
        {
            if (Session["isLoggedIn"] != null)
            {
                Account foo = (Account)Session["currentUser"];
                if (foo is Client)
                {
                    Response.Redirect("~/client/Client_Vragen.aspx");
                }
                if (foo is Admin)
                {
                    Response.Redirect("~/admin/admin_main.aspx");
                }
                else
                {
                    Volunteer bar = foo as Volunteer;
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void cbox_HasLicense_CheckedChanged(object sender, EventArgs e)
        {
            Volunteer currentuser = (Volunteer)Session["currentUser"];
            if (cbox_HasLicense.Checked == true)
            {
                volunhandler.SetLicense(currentuser.UserID, true);
            }
            else
            {
                volunhandler.SetLicense(currentuser.UserID, false);
            }
        }

        private void UpdateAvailability()
        {
            Volunteer currentuser = (Volunteer)Session["currentUser"];
            //Maandag
            Availability availablemonday = new Availability("Maandag", ddl_Monday.SelectedItem.Text, currentuser.VolunteerID);
            volunhandler.SetAvailablilty(availablemonday);

            //Dinsdag
            Availability availabletuesday = new Availability("Dinsdag", ddl_Tuesday.SelectedItem.Text, currentuser.VolunteerID);
            volunhandler.SetAvailablilty(availabletuesday);

            //Woensdag
            Availability availablewednesday = new Availability("Woensdag", ddl_Wednesday.SelectedItem.Text, currentuser.VolunteerID);
            volunhandler.SetAvailablilty(availablewednesday);

            //Donderdag
            Availability availablethursday = new Availability("Donderdag", ddl_Thursday.SelectedItem.Text, currentuser.VolunteerID);
            volunhandler.SetAvailablilty(availablethursday);

            //Vrijdag
            Availability availablefriday = new Availability("Vrijdag", ddl_Friday.SelectedItem.Text, currentuser.VolunteerID);
            volunhandler.SetAvailablilty(availablefriday);

            //Zaterdag
            Availability availablesaturday = new Availability("Zaterdag", ddl_Saturday.SelectedItem.Text, currentuser.VolunteerID);
            volunhandler.SetAvailablilty(availablesaturday);

            //Zondag
            Availability availablesunday = new Availability("Zondag", ddl_Sunday.SelectedItem.Text, currentuser.VolunteerID);
            volunhandler.SetAvailablilty(availablesunday);
        }

        private void SetValues()
        {
            Volunteer currentuser = (Volunteer)Session["currentUser"];
            List<Availability> available = new List<Availability>();
            available = volunhandler.GetAvailability(currentuser.VolunteerID);
            foreach (Availability A in available)
            {
                switch (A.Day.ToLower())
                {
                    case "maandag":
                        ddl_Monday.SelectedValue = getIndex(A.TimeOfDay).ToString();
                        break;
                    case "dinsdag":
                        ddl_Tuesday.SelectedValue = getIndex(A.TimeOfDay).ToString();
                        break;
                    case "woensdag":
                        ddl_Wednesday.SelectedValue = getIndex(A.TimeOfDay).ToString();
                        break;
                    case "donderdag":
                        ddl_Thursday.SelectedValue = getIndex(A.TimeOfDay).ToString();
                        break;
                    case "vrijdag":
                        ddl_Friday.SelectedValue = getIndex(A.TimeOfDay).ToString();
                        break;
                    case "zaterdag":
                        ddl_Saturday.SelectedValue = getIndex(A.TimeOfDay).ToString();
                        break;
                    case "zondag":
                        ddl_Sunday.SelectedValue = getIndex(A.TimeOfDay).ToString();
                        break;
                    default:
                        break;
                }
            }
            if (currentuser.License.ToLower() == "true")
            {
                cbox_HasLicense.Checked = true;
            }
            else
            {
                cbox_HasLicense.Checked = false;
            }
        }
    }
}