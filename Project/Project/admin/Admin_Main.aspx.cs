﻿using System;
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
           
            if (IsPostBack)
            {
                GetDetails();
            }
            else
            {
                loadPage();
            }
        }

        public void Delete_Click(Object sender,
                          EventArgs e)
        {
           if(Session["QuestionID"] != null)
            {
                int id = Convert.ToInt32(Session["QuestionID"]);
                Question q = adminhandler.GetQuestionByID(id);
                adminhandler.DeleteQuestion(q);
                Response.Redirect("~/admin/admin_Main.aspx");
                
            } 
            
            
           
        }

        public void loadPage()
        {
            
            lbox_Questions.Items.Clear();
            lbox_Questions.DataSource = adminhandler.GetQuestions();
            lbox_Questions.DataTextField = "FormattedForVolunteer";
            lbox_Questions.DataValueField = "ID";
            lbox_Questions.DataBind();
            lbox_Reviews.Items.Clear();
            lbox_Reviews.DataSource = adminhandler.GetReviews();
            lbox_Reviews.DataTextField = "Comments";
            lbox_Reviews.DataValueField = "ID";
            lbox_Reviews.DataBind();
          
        }

     

        public void GetDetails()
        {
            if(lbox_Questions.SelectedItem !=null)
            {
                int id = Convert.ToInt32(lbox_Questions.SelectedItem.Value);
                Session["QuestionID"] = id;
            }

            if(lbox_Reviews.SelectedItem != null)
            {
                int id = Convert.ToInt32(lbox_Reviews.SelectedItem.Value);
                Session["ReviewID"] = id;
            }
        }

        protected void btn_DeleteReview_Click(object sender, EventArgs e)
        {
            if (Session["ReviewID"] != null)
            {
                int id = Convert.ToInt32(Session["ReviewID"]);
                Review r = adminhandler.GetReviewByID(id);
                adminhandler.DeleteReview(r);
                Response.Redirect("~/admin/admin_Main.aspx");

            }
        }
    }
}