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
            List<Availability> available = new List<Availability>();
            Volunteer currentuser = (Volunteer)Session["currentUser"];
            available = volunhandler.GetAvailability(currentuser.VolunteerID);
            foreach(Availability A in available)
            {
                if(A.Day == "Maandag")
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

            if (IsPostBack)
            {
                Availability availablemonday = new Availability("Mondag", ddl_Monday.SelectedValue, currentuser.VolunteerID);
                volunhandler.SetAvailablilty(availablemonday);

                Availability availabletuesday = new Availability("'Dinsdag", ddl_Tuesday.SelectedValue, currentuser.VolunteerID);
                volunhandler.SetAvailablilty(availabletuesday);

                Availability availablewednesday = new Availability("Woensdag", ddl_Wednesday.SelectedValue, currentuser.VolunteerID);
                volunhandler.SetAvailablilty(availablewednesday);

                Availability availablethursday = new Availability("Donderdag", ddl_Thursday.SelectedValue, currentuser.VolunteerID);
                volunhandler.SetAvailablilty(availablethursday);

                Availability availablefriday = new Availability("Vrijdag", ddl_Friday.SelectedValue, currentuser.VolunteerID);
                volunhandler.SetAvailablilty(availablefriday);

                Availability availablesaturday = new Availability("Zaterdag", ddl_Saturday.SelectedValue, currentuser.VolunteerID);
                volunhandler.SetAvailablilty(availablesaturday);

                Availability availablesunday = new Availability("Zondag", ddl_Sunday.SelectedValue, currentuser.VolunteerID);
                volunhandler.SetAvailablilty(availablesunday);

            }
        }
    }
}