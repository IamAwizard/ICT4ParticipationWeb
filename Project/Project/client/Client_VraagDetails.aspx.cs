using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project
{
    public partial class Client_VraagDetails : System.Web.UI.Page
    {

        DatabaseHandler databasehandler = new DatabaseHandler();
        ClientHandler clienthandler = new ClientHandler();
        QuestionHandler questionhandler = new QuestionHandler();
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Transport> transport = new List<Transport>();
            transport = clienthandler.GetTransports();
            selecttransport.Items.Clear();
            foreach (Transport T in transport)
            {
                
                selecttransport.Items.Add(T.Description);

            }
            MaintainScrollPositionOnPostBack = true;
            string questionid = Session["Question"].ToString();
            int id = Convert.ToInt32(questionid);
            Question question;
            question = databasehandler.GetQuestionByID(id);
            if (IsPostBack)
            {
                errormsg.Visible = false;
                if (lbox_Questions.Text.Length < 3000)
                {
                    question.Description = lbox_Questions.Text;
                    if (tbox_Location.Text.Length < 200)
                    {
                        question.Location = tbox_Location.Text;
                        if (tbox_Traveltime.Text.Length < 200)
                        {
                            question.TravelTime = tbox_Traveltime.Text;


                            if (Regex.IsMatch(tbox_VolunteerCount.Text, @"^\d+$") && tbox_VolunteerCount.Text.Length < 3)
                            {
                                question.Transport.ID = clienthandler.GetTransportID(selecttransport.SelectedValue);
                                question.VolunteersNeeded = Convert.ToInt32(tbox_VolunteerCount.Text);
                                questionhandler.UpdateQuestion(question);

                            }
                            else
                            {
                                errormsg.Text = "Geen geldige invoer bij vrijwilligers nodig kan alleen een getal zijn";
                                errormsg.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        errormsg.Text = "Reistijd tekst is te lang";
                        errormsg.Visible = true;
                    }
                }
                else
                {
                    errormsg.Text = "Locatie tekst is te lang";
                    errormsg.Visible = true;
                }
            }
            else
            {
                errormsg.Text = "Vraag omschrijving tekst is te lang";
                errormsg.Visible = true;
            }


        
    
            if (!IsPostBack)
            {
            lbox_Questions.Items.Clear();
            string username = clienthandler.GetUsername(question.AuthorID);
            lbl_date.Text = question.DateBegin.ToShortDateString();
            lbl_user.Text = username;
            lbox_Questions.Items.Add(question.Description);
            if (question.Location != "")
            {
                tbox_Location.Text = question.Location;
            }
            else
            {

                tbox_Location.Text = "Geef een locatie mee";
            }
            if (question.TravelTime != "")
            {
                tbox_Traveltime.Text = question.TravelTime;
            }
            else
            {
                tbox_Traveltime.Text = "Geef een reistijd mee";
            }
         
            if (question.VolunteersNeeded != 1)
            {
                tbox_VolunteerCount.Text = question.VolunteersNeeded.ToString();
            }
            else
            {
                tbox_VolunteerCount.Text = "1";
            }
                selecttransport.SelectedItem.Value = question.Transport.Description;
            }

        }


    }
    
}