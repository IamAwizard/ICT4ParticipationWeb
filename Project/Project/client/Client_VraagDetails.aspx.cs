using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project
{
    public partial class Client_VraagDetails : System.Web.UI.Page
    {
        DatabaseHandler databasehandler = new DatabaseHandler();
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;

            string questionid = Session["Question"].ToString();
            int id = Convert.ToInt32(questionid);
            Question question;

            question = databasehandler.GetQuestionByID(id);
            if (question.Location != null)
            {
                tbox_Location.Text = question.Location;
            }
            else
            {

                tbox_Location.Text = "Geef een locatie mee";
            }
            if (question.Location != null)
            {
                tbox_Location.Text = question.Location;
            }
            else

                tbox_Location.Text = "Geef een locatie mee";
        }


    }
    
}