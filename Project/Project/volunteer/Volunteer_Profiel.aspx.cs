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
              
            Volunteer currentuser = (Volunteer)Session["currentUser"];
            if (IsPostBack)
            {
                //Maandag
                Availability availablemonday = new Availability("Mondag", ddl_Monday.SelectedValue, currentuser.VolunteerID);
                volunhandler.SetAvailablilty(availablemonday);

                //Dinsdag
                Availability availabletuesday = new Availability("'Dinsdag", ddl_Tuesday.SelectedValue, currentuser.VolunteerID);
                volunhandler.SetAvailablilty(availabletuesday);

                //Woensdag
                Availability availablewednesday = new Availability("Woensdag", ddl_Wednesday.SelectedValue, currentuser.VolunteerID);
                volunhandler.SetAvailablilty(availablewednesday);

                //Donderdag
                Availability availablethursday = new Availability("Donderdag", ddl_Thursday.SelectedValue, currentuser.VolunteerID);
                volunhandler.SetAvailablilty(availablethursday);

                //Vrijdag
                Availability availablefriday = new Availability("Vrijdag", ddl_Friday.SelectedValue, currentuser.VolunteerID);
                volunhandler.SetAvailablilty(availablefriday);

                //Zaterdag
                Availability availablesaturday = new Availability("Zaterdag", ddl_Saturday.SelectedValue, currentuser.VolunteerID);
                volunhandler.SetAvailablilty(availablesaturday);

                //Zondag
                Availability availablesunday = new Availability("Zondag", ddl_Sunday.SelectedValue, currentuser.VolunteerID);
                volunhandler.SetAvailablilty(availablesunday);

            }

            List<Availability> available = new List<Availability>();
                available = volunhandler.GetAvailability(currentuser.VolunteerID);
                foreach (Availability A in available)
                {
                    if (A.Day == "Maandag")
                    {
                        ddl_Monday.SelectedValue = A.TimeOfDay;
                    }
                    if (A.Day == "Dinsdag")
                    {
                        ddl_Tuesday.SelectedValue = A.TimeOfDay;
                    }
                    if (A.Day == "Woensdag")
                    {
                        ddl_Tuesday.SelectedValue = A.TimeOfDay;
                    }
                    if (A.Day == "Woensdag")
                    {
                        ddl_Tuesday.SelectedValue = A.TimeOfDay;
                    }
                    if (A.Day == "Donderdag")
                    {
                        ddl_Tuesday.SelectedValue = A.TimeOfDay;
                    }
                    if (A.Day == "Vrijdag")
                    {
                        ddl_Tuesday.SelectedValue = A.TimeOfDay;
                    }
                    if (A.Day == "Zaterdag")
                    {
                        ddl_Tuesday.SelectedValue = A.TimeOfDay;
                    }
                    if (A.Day == "Zondag")
                    {
                        ddl_Tuesday.SelectedValue = A.TimeOfDay;
                    }
                
            }
            
        }

        protected void cbox_HasLicense_CheckedChanged(object sender, EventArgs e)
        {
            Volunteer currentuser = (Volunteer)Session["currentUser"];
            if (cbox_HasLicense.Checked == true)
            {
                volunhandler.SetLicense(currentuser.UserID,true);
            }
            else
            {
                volunhandler.SetLicense(currentuser.UserID, false);
            }
       
         
        }
    }
}