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
        VolunteerHandler volunteerhandler = new VolunteerHandler();
        MeetingHandler meetinghandler = new MeetingHandler();
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
                 if(lbox_getquestion.SelectedItem != null)
                    {
                        locationdiv.Visible = true;
                        datedivday.Visible = true;
                        datedivyear.Visible = true;
                        datedivmonth.Visible = true;
                    }
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

        protected void btn_makeappointment_Click(object sender, EventArgs e)
        {
            if (lbox_getquestion.SelectedItem != null)
            {
                Question question = (Question)Session["Question"];
                Client currentuser = (Client)Session["currentUser"];
                Volunteer volun = volunteerhandler.getvolunteer(Convert.ToInt32(lbox_getquestion.SelectedItem.Value));
                if(tbox_Day.Text != "" && Regex.IsMatch(tbox_Day.Text, @"^\d+$"))
                {
                    if(tbox_Year.Text != "" && Regex.IsMatch(tbox_Year.Text, @"^\d+$"))
                    {
                        if(tb_location.Text != "")
                        {
                            DateTime date = new DateTime(Convert.ToInt32(tbox_Year.Text), Convert.ToInt32(ddl_Month.Text), Convert.ToInt32(tbox_Day.Text));
                            Meeting meeting = new Meeting(date, tb_location.Text, currentuser, volun);
                            meetinghandler.addmeeting(meeting);
                            tbox_Day.Text = "";
                            tb_location.Text = "";
                            tbox_Year.Text = "";
                            datedivday.Visible = false;
                            datedivmonth.Visible = false;
                            datedivyear.Visible = false;
                            locationdiv.Visible = false;
                            lbox_getquestion.ClearSelection();
                            errormsgmeeting.ForeColor = System.Drawing.Color.Green;
                            errormsgmeeting.Text = "Afspraak aangemaakt";
                            errormsgmeeting.Visible = true;
                        }
                        else
                        {
                            errormsgmeeting.ForeColor = System.Drawing.Color.Red;
                            errormsgmeeting.Text = "Geen geldige locatie ingevuld";
                            errormsgmeeting.Visible = true;
                        }
                   
                    }
                    else
                    {
                        errormsgmeeting.ForeColor = System.Drawing.Color.Red;
                        errormsgmeeting.Text = "Geen geldige jaar ingevuld";
                        errormsgmeeting.Visible = true;
                    }
                }
                else
                {
                    errormsgmeeting.ForeColor = System.Drawing.Color.Red;
                    errormsgmeeting.Text = "Geen geldige dag ingevuld";
                    errormsgmeeting.Visible = true;
                }
            
            }
        }
    }
}