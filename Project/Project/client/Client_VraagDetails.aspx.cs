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
            if (Session["Question"] != null)
            {
                Question question = (Question)Session["Question"];

                MaintainScrollPositionOnPostBack = true;

                if (!IsPostBack)
                {
                    List<Transport> transport = new List<Transport>();
                    transport = clienthandler.GetTransports();
                    selecttransport.Items.Clear();

                    // DATABIND
                    selecttransport.DataSource = transport;
                    // Property van transport die word weergeven als tekst
                    selecttransport.DataTextField = "Description";
                    // Property van transport die gebruikt wordt als value (niet getoond)
                    selecttransport.DataValueField = "ID";
                    selecttransport.DataBind();

                    // Stel het selectedvalue (=transport.ID) in op het transport.id van de vraag (tostring!!!!)
                    selecttransport.SelectedValue = question.Transport.ID.ToString();

                    tbox_Question.Text = string.Empty;
                    string username = clienthandler.GetUsername(question.AuthorID);
                    lbl_date.Text = question.DateBegin.ToShortDateString();
                    lbl_user.Text = username;
                    tbox_Question.Text = question.Description;
                    cbox_Critical.Checked = question.Critical;
                    lbox_getquestion.DataSource = question.AcceptedBy;
                    lbox_getquestion.DataTextField = "Username";
                    lbox_getquestion.DataValueField = "VolunteerID";
                    lbox_getquestion.DataBind();
                    string grammar = "vrijwilligers hebben";
                    if (question.AcceptedBy.Count == 1)
                    {
                        grammar = "vrijwilliger heeft";
                    }
                    lbl_Volunteer.Text = $"{question.AcceptedBy.Count} {grammar}  gereageerd";
                    if (question.AcceptedBy.Count == 0)
                    {
                        lbl_Volunteer.Text = "Nog niemand heeft gereageerd";
                    }
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
                }
                if (IsPostBack)
                {
                    errormsg.Visible = false;
                    if (tbox_Question.Text.Length < 3000)
                    {
                        question.Description = tbox_Question.Text;
                        if (tbox_Location.Text.Length < 200)
                        {
                            question.Location = tbox_Location.Text;
                            if (tbox_Traveltime.Text.Length < 200)
                            {
                                question.TravelTime = tbox_Traveltime.Text;


                                if (Regex.IsMatch(tbox_VolunteerCount.Text, @"^\d+$") && tbox_VolunteerCount.Text.Length < 3)
                                {
                                    question.Transport.ID = Convert.ToInt32(selecttransport.SelectedValue);
                                    question.VolunteersNeeded = Convert.ToInt32(tbox_VolunteerCount.Text);
                                    question.Critical = cbox_Critical.Checked;
                                    questionhandler.UpdateQuestion(question);

                                }
                                else
                                {
                                    errormsg.Text = "Geen geldige invoer bij vrijwilligers, kan alleen een getal zijn";
                                    errormsg.Visible = true;
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
                }
            }
            else
            {
                Response.Redirect("~/client/client_vragen.aspx");
            }
        }
    }
}