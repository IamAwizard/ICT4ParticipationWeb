using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Project;
using System.Windows.Forms;

namespace Project
{
    public partial class Admin_Main : System.Web.UI.Page
    {
        AdminHandler adminhandler = new AdminHandler();
        protected void Page_Load(object sender, EventArgs e)
        {
            loadPage();
        }

        public void Delete_Click(Object sender,
                          EventArgs e)
        {
           if(lbox_Questions.SelectedValue != null)
            {
               
            }
            
           
        }

        public void loadPage()
        {
            lbox_Questions.Items.Clear();
            lbox_Questions.DataSource = adminhandler.GetQuestions();
            lbox_Questions.DataTextField = "FormattedForVolunteer";
            lbox_Questions.DataValueField = "ID";
            lbox_Questions.DataBind();
        }
    }
}