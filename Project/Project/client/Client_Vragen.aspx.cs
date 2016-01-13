using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project
{
    public partial class Client_Vragen : System.Web.UI.Page
    {
        QuestionHandler questionhandler = new QuestionHandler();
        protected void Page_Load(object sender, EventArgs e)
        {
  
            if (lbox_Questions.SelectedItem != null)
            {
                Client currentuser = (Client)Session["currentUser"];
                List<Question> question = new List<Question>();
                string discription = lbox_Questions.Text;
                discription = discription.Substring(10);
                int id = currentuser.ClientID;
                question = questionhandler.GetSingleQuestion(id, discription);
                lbox_getquestion.Items.Clear();
                foreach(Question Q in question)
                {
                    string location = "";
                    string traveltime ="";
                    string startdate = "Datum: " + Q.DateBegin.ToShortDateString();
                    string critical = "Urgent: ";
                    string volunteersneeded = "Aantal vrijwilligers: " + Q.VolunteersNeeded.ToString();
                    string transport = "";
                    if (Q.Location != "")
                    {
                        location = "Locatie: "  + Q.Location;
                    }
                    else
                    {
                        location = "Locatie: Nog geen locatie opgegeven";
                    }
                    if (Q.TravelTime != "")
                    {
                        traveltime = "Reistijd: "+ Q.TravelTime;
                    }
                    else
                    {
                        traveltime = "Reistijd: Nog geen reistijd opgegeven";
                    }
                    if (Q.Transport.Description.Length != 0)
                    {
                         transport = "Vervoer: " + Q.Transport.Description;
                    }
                    else
                    {
                        transport = "Vervoer: Nog geen vervoer opgegeven";
                    }
                    if(Q.Critical == true)
                    {
                         critical += "JA";
                    }
                    else
                    {
                        critical += "NEE";
                    }
                    lbox_getquestion.Items.Add(location);
                    lbox_getquestion.Items.Add(traveltime);
                    lbox_getquestion.Items.Add(startdate);
                    lbox_getquestion.Items.Add(critical);
                    lbox_getquestion.Items.Add(volunteersneeded);
                    lbox_getquestion.Items.Add(transport);
                }
            }
           


            if (Session["currentUser"] != null)
            {
                lbox_Questions.Items.Clear();
                Client currentuser = (Client)Session["currentUser"];
                List<Question> questions = new List<Question>();
                questions = questionhandler.GetQuestionsByAuthor(currentuser);
                if(questions.Count != 0)
                {
                    foreach (Question Q in questions)
                    {
                        string discriptionanddate = Q.DateBegin.ToShortDateString() + " " + Q.Description;
                        lbox_Questions.Items.Add(discriptionanddate);
                    }
                }     
            }
        }

        protected void btn_AddQuestion_Click(object sender, EventArgs e)
        {
           
            if (tbox_AddQuestion.Text.Length > 10 && tbox_AddQuestion.Text.Length < 255)
            {
                if (Session["currentUser"] != null)
                {
                    Client currentuser = (Client)Session["currentUser"];
                    string content = tbox_AddQuestion.Text;
                    int id = currentuser.ClientID;
                    Question question = new Question(id, content, DateTime.Now, 1);
                    questionhandler.AddQuestion(question);
                    Response.Redirect("Client_vragen.aspx");
             
                }
                
            }
            else
            {
                lbl_errormsg.ForeColor = System.Drawing.Color.Red;
                lbl_errormsg.Text = "Inhoud van de vraag is tekort of te lang";
            }
         
        }

        protected void btn_LoadQuestion_Click(object sender, EventArgs e)
        {
         
        }

       


        //Client currentuser = (Client)Session["currentUser"];
        //List<Question> questiondetails = new List<Question>();
        //questiondetails = questionhandler.GetQuestionsByAuthor(currentuser);
    }
}