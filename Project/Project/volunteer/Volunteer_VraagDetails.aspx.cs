﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project
{
    public partial class Volunteer_VraagDetails : System.Web.UI.Page
    {
        private VolunteerHandler volunteerhandler = new VolunteerHandler();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadQuestion();

        }

        private void LoadQuestion()
        {
            if (Session["Question"] != null)
            {
                Question q = (Question)Session["Question"];

                lbl_Client.Text = $"Vroeg {q.Author.Username}";
                lbl_Critical.Visible = q.Critical;
                lbl_Date.Text = $"op {q.DateBegin.ToShortDateString()}";
                tbox_Location.Text = q.Location;
                tbox_Traveltime.Text = q.TravelTime;
                tbox_Transport.Text = q.Transport.Description;
                string grammar = "vrijwilligers hebben";
                if (q.AcceptedBy.Count == 1)
                {
                    grammar = "vrijwilliger heeft";
                }
                lbl_Volunteers.Text = $"{q.AcceptedBy.Count} {grammar}  gereageerd";
                if (q.AcceptedBy.Count == 0)
                {
                    lbl_Volunteers.Text = "Nog niemand heeft gereageerd";
                }
                tbox_VolunteerCount.Text = q.VolunteersNeeded.ToString();
                lbox_Question.Text = q.Description;

                lbox_AcceptedVolunteers.DataSource = q.AcceptedBy;
                lbox_AcceptedVolunteers.DataTextField = "Username";
                lbox_AcceptedVolunteers.DataValueField = "VolunteerID";
                lbox_AcceptedVolunteers.DataBind();
            }
            else
            {
                Response.Redirect("~/volunteer/volunteer_vragen.aspx");
            }
        }

        protected void btn_Answer_Click(object sender, EventArgs e)
        {
            if (Session["Question"] != null)
            {
                if (Session["currentUser"] != null)
                {
                    Volunteer user = (Volunteer)Session["currentUser"];
                    Question question = (Question)Session["Question"];
                    volunteerhandler.AnswerQuestion(question, user);
                    Question q = volunteerhandler.GetQuestionByIDfromCache(question.ID);
                    q = volunteerhandler.ExpandQuestionsWithClient(q);
                    q = volunteerhandler.ExpandQuestionWithVolunteers(q);
                    Session["Question"] = q;
                    Response.Redirect("~/volunteer/volunteer_vraagdetails.aspx");
                }
            }
        }

    }
}