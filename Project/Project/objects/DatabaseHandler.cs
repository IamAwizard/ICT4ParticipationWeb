using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle;
using Oracle.DataAccess;
using Oracle.DataAccess.Client;

namespace ICT4Participation
{
    static class DatabaseHandler
    {
        // Fields

        // connectionstring = "User Id=loginname; Password=password;Data Source=localhost";
        static string connectionstring = "User Id=dbi259530;Password=ZBEB4DKxvr;Data Source=192.168.15.50/fhictora";
        static private OracleConnection con;
        static private OracleCommand cmd;
        static private OracleDataReader dr;

        // Properties

        // Constructor

        // Methods

        /// <summary>
        /// Connect to the database...
        /// </summary>
        public static void Connect()
        {
            con = new OracleConnection();
            con.ConnectionString = connectionstring;
            con.Open();
            Console.WriteLine("CONNECTION SUCCESFULL");

        }

        /// <summary>
        /// Disconnect from the database...
        /// </summary>
        public static void Disconnect()
        {
            con.Close();
            con.Dispose();
        }

        /// <summary>
        ///  Used to replace null values with string "NULL" values
        ///  not in use as of 5-11-2015
        /// </summary>
        /// <param name="cmd"></param>
        static void PopulateNullParameters(OracleCommand cmd)
        {
            foreach (OracleParameter p in cmd.Parameters)
            {
                if (p.Value == null)
                {
                    p.Value = "NULL";
                }
            }
        }

        /// <summary>
        /// Safely get string values from the oracledatareader if they are null
        /// </summary>
        /// <param name="odr"></param>
        /// <param name="ColIndex"></param>
        /// <returns></returns>
        static string SafeReadString(OracleDataReader odr, int ColIndex)
        {
            {
                if (!odr.IsDBNull(ColIndex))
                    return odr.GetString(ColIndex);
                else
                    return string.Empty;
            }
        }

        static int SafeReadInt(OracleDataReader odr, int ColIndex)
        {
            {
                if (!odr.IsDBNull(ColIndex))
                    return odr.GetInt32(ColIndex);
                else
                    return -1;
            }
        }

        static decimal SafeReadDecimal(OracleDataReader odr, int ColIndex)
        {
            {
                if (!odr.IsDBNull(ColIndex))
                    return odr.GetDecimal(ColIndex);
                else
                    return 0;
            }
        }

        public static List<Question> GetAllQuestions()
        {
            List<Question> questionlist = new List<Question>();
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT H.ID, H.OMSCHRIJVING, H.LOCATIE , H.REISTIJD, H.STARTDATUM, H.EINDDATUM, H.URGENT, H.AANTALVRIJWILLIGERS, H.VERVOERTYPE, TVAARDIGHEID.ID as vaardigheidID, TVRIJWILLIGER.ID as vrijwilligerID FROM THULPVRAAG H, TVAARDIGHEID, TVRIJWILLIGER, THULPVRAAG_VAARDIGHEID, THULPVRAAG_VRIJWILLIGER WHERE (H.ID = THULPVRAAG_VRIJWILLIGER.hulpvraagID AND THULPVRAAG_VRIJWILLIGER.vrijwilligerid = TVRIJWILLIGER.id) AND (H.ID = THULPVRAAG_VAARDIGHEID.hulpvraagID AND THULPVRAAG_VAARDIGHEID.vaardigheidID = TVAARDIGHEID.id)"; // QUERY
                cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteReader();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                Disconnect();
                return null;
            }

            try
            {
                while (dr.Read())
                {
                    // Read from DB
                    int hulpvraagid = dr.GetInt32(0);
                    var omschrijving = dr.GetInt32(1);
                    var locatie = SafeReadString(dr, 2);
                    var reistijd = SafeReadString(dr, 3);
                    DateTime startdatum = dr.GetDateTime(4);
                    DateTime einddatum = dr.GetDateTime(5);
                    var urgentie = SafeReadString(dr, 6);
                    int aantalvrijwilligers = SafeReadInt(dr, 7);
                    int vervoertype = SafeReadInt(dr, 8);
                    int vaardigheidid = SafeReadInt(dr, 9);
                    int vrijwilligerid = SafeReadInt(dr, (10));

                    //Question toadd;
                    //toadd = new Question(null, auteur, locatie, vervoer, afstand, bijzonderheid, vraag, datum, opgelost);
                    //toadd.ID = hulpvraagid;
                    //toadd.VolunteerID = vrijwilligerid;
                    //questionlist.Add(toadd);
                }
                foreach (Question q in questionlist)
                {
                    q.Client = (Client)GetUser(q.Auteur);
                }

                foreach (Question q in questionlist)
                {
                    if (q.VolunteerID != -1)
                    {
                        q.Volunteer = (Volunteer)GetUser(q.VolunteerID);
                    }
                }
                return questionlist;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }

        }

        public static User GetUser(int ids)
        {
            User toadd = null;
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT USERID, NAAM, GEBOORTEDATUM, GESLACHT, WOONPLAATS, ADRES, EMAIL, WACHTWOORD, TYPE FROM TUSER WHERE USERID = " + ids; // QUERY
                cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteReader();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                Disconnect();
                return null;
            }

            try
            {
                while (dr.Read())
                {
                    // Read from DB
                    var id = dr.GetInt32(0);
                    var name = dr.GetString(1);
                    var dateOfBirth = dr.GetDateTime(2);
                    var gender = dr.GetString(3);
                    var city = dr.GetString(4);
                    var adress = dr.GetString(5);
                    var email = dr.GetString(6);
                    var password = dr.GetString(7);

                    var type = dr.GetString(8);


                    switch (type)
                    {
                        case "CLIENT":
                            Client newClient = new Client(name, dateOfBirth, gender, city, adress, email, password);
                            toadd = newClient;
                            toadd.UserID = id;
                            break;
                        case "VOLUNTEER":
                            toadd = null;
                            Volunteer newUser = new Volunteer(name, dateOfBirth, gender, city, adress, email, password, false, "Niet Opgegeven", "ONBEKEND", "ONBEKEND");
                            toadd = newUser;
                            toadd.UserID = id;
                            break;
                        case "ADMIN":
                            Admin newAdmin = new Admin(name, dateOfBirth, gender, city, adress, email, password);
                            toadd = newAdmin;
                            toadd.UserID = id;
                            break;
                        default:
                            toadd = null;
                            break;
                    }

                }
                Disconnect();
                return toadd;
            }
            catch (InvalidCastException ex)
            {
                Disconnect();
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        static User GetUserNoConnect(int ids)
        {
            User toadd = null;
            try
            {
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT ID, NAAM, ADRES, WOONPLAATS, TELEFOONNUMMER, HEEFTRIJBEWIJS, HEEFTAUTO, UITSCHRIJVINGSDATUM, ACCOUNTID FROM TGEBRUIKER WHERE ID = " + ids; // QUERY
                cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteReader();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                Disconnect();
                return null;
            }

            try
            {
                while (dr.Read())
                {
                    // Read from DB
                    int id = dr.GetInt32(0);
                    var naam = dr.GetString(1);
                    var adres = dr.GetDateTime(2);
                    var woonplaats = dr.GetString(3);
                    var telefoonnummer = dr.GetString(4);
                    bool heeftrijbewijs = (dr.GetString(5).ToUpper() == "TRUE");
                    bool heeftauto = (dr.GetString(6).ToUpper() == "TRUE");
                    DateTime uitschrijvingsdatum = dr.GetDateTime(7);
                    int accountid = dr.GetInt32(8);


                    /*
                      switch (type)
                    {
                        case "CLIENT":
                            Client newClient = new Client(name, dateOfBirth, gender, city, adress, email, password);
                            toadd = newClient;
                            toadd.UserID = id;
                            break;
                        case "VOLUNTEER":
                            toadd = null;
                            Volunteer newUser = new Volunteer(name, dateOfBirth, gender, city, adress, email, password, false, "Niet Opgegeven", "ONBEKEND", "ONBEKEND");
                            toadd = newUser;
                            toadd.UserID = id;
                            break;
                        case "ADMIN":
                            Admin newAdmin = new Admin(name, dateOfBirth, gender, city, adress, email, password);
                            toadd = newAdmin;
                            toadd.UserID = id;
                            break;
                        default:
                            toadd = null;
                            break;
                    }
                     */

                }
                return toadd;
            }
            catch (InvalidCastException ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        public static List<User> GetAllUsers()
        {

            List<User> userList = new List<User>();
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT g.ID, NAAM, ADRES, WOONPLAATS, TELEFOONNUMMER, HEEFTRIJBEWIJS, HEEFTAUTO, UITSCHRIJVINGSDATUM, ACCOUNTID, GEBRUIKERSNAAM, WACHTWOORD, EMAIL FROM TGEBRUIKER g, TACCOUNT WHERE g.accountid = taccount.id"; // QUERY
                cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteReader();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                Disconnect();
                return null;
            }

            try
            {
                while (dr.Read())
                {
                    // Read from DB
                    int id = dr.GetInt32(0);
                    var naam = dr.GetString(1);
                    var adres = dr.GetDateTime(2);
                    var woonplaats = dr.GetString(3);
                    var telefoonnummer = dr.GetString(4);
                    bool heeftrijbewijs = (dr.GetString(5).ToUpper() == "TRUE");
                    bool heeftauto = (dr.GetString(6).ToUpper() == "TRUE");
                    DateTime uitschrijvingsdatum = dr.GetDateTime(7);
                    int accountid = dr.GetInt32(8);
                    var gebruikersnaam = dr.GetString(9);
                    var wachtwoord = dr.GetString(10);
                    var email = dr.GetString(11);
                    /*
                    User toadd;
                    switch (type)
                    {
                        case "CLIENT":
                            Client newClient = new Client(name, dateOfBirth, gender, city, adress, email, password);
                            toadd = newClient;
                            toadd.UserID = id;
                            break;
                        case "VOLUNTEER":
                            toadd = null;
                            Volunteer newUser = new Volunteer(name, dateOfBirth, gender, city, adress, email, password, false, "", "", "");
                            toadd = newUser;
                            toadd.UserID = id;
                            break;
                        case "ADMIN":
                            Admin newAdmin = new Admin(name, dateOfBirth, gender, city, adress, email, password);
                            toadd = newAdmin;
                            toadd.UserID = id;
                            break;
                        default:
                            toadd = null;
                            break;
                    }

                    userList.Add(toadd);*/
                }
                Disconnect();
                return userList;
            }
            catch (InvalidCastException ex)
            {
                Disconnect();
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        static void Read(string sql)
        {
            try
            {
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void AddUser(User newuser)
        {
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                    "Insert into TUSER(NAAM, GEBOORTEDATUM, GESLACHT, WOONPLAATS, ADRES, EMAIL, WACHTWOORD, TYPE) VALUES (:NewNAAM, :NewGEBOORTEDATUM, :NewGESLACHT, :NewWOONPLAATS, :NewADRES, :NewEMAIL, :NewWACHTWOORD, :NewTYPE)";

                cmd.Parameters.Add("NewNAAM", OracleDbType.Varchar2).Value = newuser.Name;
                cmd.Parameters.Add("NewGEBOORTEDATUM", OracleDbType.Date).Value = newuser.DateOfBirth;
                cmd.Parameters.Add("NewGESLACHT", OracleDbType.Varchar2).Value = newuser.Gender; ;
                cmd.Parameters.Add("NewWOONPLAATS", OracleDbType.Varchar2).Value = newuser.City; ;
                cmd.Parameters.Add("NewADRES", OracleDbType.Varchar2).Value = newuser.Adress; ;
                cmd.Parameters.Add("NewEMAIL", OracleDbType.Varchar2).Value = newuser.Email; ;
                cmd.Parameters.Add("NewWACHTWOORD", OracleDbType.Varchar2).Value = newuser.Password;
                if (newuser is Client)
                    cmd.Parameters.Add("NewTYPE", OracleDbType.Varchar2).Value = "CLIENT";
                if (newuser is Volunteer)
                    cmd.Parameters.Add("NewTYPE", OracleDbType.Varchar2).Value = "VOLUNTEER";
                if (newuser is Admin)
                    cmd.Parameters.Add("NewTYPE", OracleDbType.Varchar2).Value = "ADMIN";
                cmd.ExecuteNonQuery();

                if(newuser is Volunteer)
                {
                    ExtendVolunteer(GetUserID(newuser.Email));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Disconnect();
            }
        }

        public static void AddAvatar()
        {

        }

        public static bool AddQuestion(Question newquestion)
        {
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                    "Insert into THULPVRAAG(OMSCHRIJVING, LOCATIE, REISTIJD, STARTDATUM, EINDDATUM, URGENT, AANTALVRIJWILLIGERS, VERVOERTYPE) VALUES (:NewOMSCHRIJVING, :NewLOCATIE, :NewREISTIJD, :NewSTARTDATUM, :NewEINDDATUM, :NewURGENT, :NewAANTALVRIJWILLIGERS, :NewVERVOERTYPE)";

                cmd.Parameters.Add("NewOMSCHRIJVING", OracleDbType.Varchar2).Value = newquestion.Omschrijving;
                cmd.Parameters.Add("NewLOCATIE", OracleDbType.Varchar2).Value = newquestion.Content.Locatie;
                cmd.Parameters.Add("NewREISTIJD", OracleDbType.Varchar2).Value = newquestion.Reistijd;
                cmd.Parameters.Add("NewSTARTDATUM", OracleDbType.Varchar2).Value = newquestion.Startdatum.ToString("dd-mm-yyyy");
                cmd.Parameters.Add("NewEINDDATUM", OracleDbType.Varchar2).Value = newquestion.Einddatum.ToString("dd-mm-yyyy");
                cmd.Parameters.Add("NewURGENT", OracleDbType.Varchar2).Value = newquestion.Urgent;
                cmd.Parameters.Add("NewAANTALVRIJWILLIGERS", OracleDbType.Varchar2).Value = newquestion.Aantalvrijwilligers;
                cmd.Parameters.Add("NewVERVOERTYPE", OracleDbType.Varchar2).Value = newquestion.Vervoertype;

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                Disconnect();
            }

        }

        public static bool UpdateQuestion(Question question)
        {
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                   "UPDATE THULPVRAAG SET OMSCHRIJVING = :NewOMSCHRIJVING, LOCATIE = :NewLOCATIE, REISTIJD = :NewREISTIJD, URGENT = :newUrgent, AANTALVRIJWILLIGERS = :newAantalvrijwilligers, VERVOERTYPE = :newVervoertype WHERE id = :newIDvalue";

                cmd.Parameters.Add("NewOMSCHRIJVING", OracleDbType.Varchar2).Value = newquestion.Omschrijving;
                cmd.Parameters.Add("NewLOCATIE", OracleDbType.Varchar2).Value = newquestion.Content.Locatie;
                cmd.Parameters.Add("NewREISTIJD", OracleDbType.Varchar2).Value = newquestion.Reistijd;
                cmd.Parameters.Add("NewURGENT", OracleDbType.Varchar2).Value = newquestion.Urgent;
                cmd.Parameters.Add("NewAANTALVRIJWILLIGERS", OracleDbType.Varchar2).Value = newquestion.Aantalvrijwilligers;
                cmd.Parameters.Add("NewVERVOERTYPE", OracleDbType.Varchar2).Value = newquestion.Vervoertype;

                cmd.Parameters.Add("newIDvalue", OracleDbType.Int32).Value = question.ID;

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                Disconnect();
            }
        }

        public static bool AddReview(Review newreview)
        {
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                    "Insert into TREVIEW(DATUM, VOLUNTEER, CLIENT, RATING, TEKST) VALUES (:NewDATUM, :NewVOLUNTEER, :NewCLIENT, :NewRATING, :NewTEKST)";

                cmd.Parameters.Add("NewDATUM", OracleDbType.Date).Value = newreview.Date.ToString("dd-MMM-yy");
                cmd.Parameters.Add("NewVOLUNTEER", OracleDbType.Int32).Value = newreview.Targetuser.UserID;
                cmd.Parameters.Add("NewCLIENT", OracleDbType.Int32).Value = newreview.Client.UserID;
                cmd.Parameters.Add("NewRATING", OracleDbType.Int32).Value = newreview.Rating;
                cmd.Parameters.Add("NewTEKST", OracleDbType.Varchar2).Value = newreview.Content;

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                Disconnect();
            }
        }

        public static bool DeleteUser(User usertodelete)
        {
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                   "DELETE FROM TUSER WHERE USERID = :deleteIDvalue";

                cmd.Parameters.Add("deleteIDvalue", OracleDbType.Varchar2).Value = usertodelete.UserID;
                cmd.ExecuteNonQuery();

                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                   "DELETE FROM TAFSPRAAK WHERE VOLUNTEER = :deleteIDvalue";

                cmd.Parameters.Add("deleteIDvalue", OracleDbType.Varchar2).Value = usertodelete.UserID;
                cmd.ExecuteNonQuery();

                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                   "DELETE FROM TAFSPRAAK WHERE CLIENT = :deleteIDvalue";

                cmd.Parameters.Add("deleteIDvalue", OracleDbType.Varchar2).Value = usertodelete.UserID;
                cmd.ExecuteNonQuery();

                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                   "DELETE FROM TCLIENT WHERE CLIENTID = :deleteIDvalue";

                cmd.Parameters.Add("deleteIDvalue", OracleDbType.Varchar2).Value = usertodelete.UserID;
                cmd.ExecuteNonQuery();

                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                   "DELETE FROM TQUESTION WHERE AUTEUR = :deleteIDvalue";

                cmd.Parameters.Add("deleteIDvalue", OracleDbType.Varchar2).Value = usertodelete.UserID;
                cmd.ExecuteNonQuery();

                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                   "DELETE FROM TREVIEW WHERE CLIENT = :deleteIDvalue";

                cmd.Parameters.Add("deleteIDvalue", OracleDbType.Varchar2).Value = usertodelete.UserID;
                cmd.ExecuteNonQuery();

                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                   "DELETE FROM TREVIEW WHERE VOLUNTEER = :deleteIDvalue";

                cmd.Parameters.Add("deleteIDvalue", OracleDbType.Varchar2).Value = usertodelete.UserID;
                cmd.ExecuteNonQuery();

                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                   "DELETE FROM TROOSTER WHERE VOLUNTEERID = :deleteIDvalue";

                cmd.Parameters.Add("deleteIDvalue", OracleDbType.Varchar2).Value = usertodelete.UserID;
                cmd.ExecuteNonQuery();

                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                   "DELETE FROM TVOLUNTEER WHERE VOLUNTEERID = :deleteIDvalue";

                cmd.Parameters.Add("deleteIDvalue", OracleDbType.Varchar2).Value = usertodelete.UserID;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return true;

            }
        }


        public static bool SendChatMessage(Client client, Volunteer volunteer, string message)
        {
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                    "Insert into TCHAT(HULPBEHOEVENDEID, VRIJWILLIGERID, BERICHT) VALUES (:NewClient, :NewVolunteer, :NewMessage)";

                cmd.Parameters.Add("NewClient", OracleDbType.Int32).Value = client.UserID;
                cmd.Parameters.Add("NewVolunteer", OracleDbType.Int32).Value = volunteer.UserID;
                cmd.Parameters.Add("NewMessage", OracleDbType.Varchar2).Value = message;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                Disconnect();
            }
        }

        public static List<Message> GetChat(Client client, Volunteer volunteer)
        {
            List<Message> chatmessages = new List<Message>();
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT CHATID, BERICHT, HULPBEHOEVENDEID, VRIJWILLIGERID FROM TCHAT WHERE HULPBEHOEVENDEID = " + client.UserID + " AND VRIJWILLIGERID = " + volunteer.UserID;
                cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var id = dr.GetInt32(0);
                    var bericht = dr.GetInt32(1);
                    var hulpbehoevendeid = dr.GetInt32(2);
                    var vrijwilligerid = dr.GetDateTime(3);

                    chatmessages.Add(new Message(id, bericht, hulpbehoevendeid, vrijwilligerid));
                }

                return chatmessages;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                Disconnect();
            }
        }


        public static List<Review> GetAllReviews()
        {
            List<Review> returnlist = new List<Review>();
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT ID, BEOORDELING, OPMERKINGEN, VRIJWILLIGERID, HULPVRAAGID FROM TREVIEW";
                cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var id = dr.GetInt32(0);
                    var beoordeling = dr.GetInt32(1);
                    var opmerkingen = dr.GetString(2);
                    var vrijwilligerid = dr.GetInt32(3);
                    var hulpvraagid = dr.GetInt32(4);
                   

                    returnlist.Add(new Review(id, beoordeling,opmerkingen,vrijwilligerid,hulpvraagid));
                
                    returnlist[returnlist.Count - 1].ReviewID = id;
                }

                return returnlist;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                Disconnect();
            }
        }

        public static bool DeleteReview(int reviewID)
        {
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                   "DELETE FROM TREVIEW WHERE REVIEWID = " + reviewID;

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                Disconnect();
            }
         }

        public static bool DeleteQuestion(int QuestionID)
        {
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                   "DELETE FROM TQUESTION WHERE QUESTIONID = " + QuestionID;

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                Disconnect();
            }
        }

        static int GetUserID(string email)
        {
            int returnvalue = -1;
            try
            {
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT USERID FROM TUSER WHERE EMAIL = :findEmail";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("findEmail", OracleDbType.Varchar2).Value = email;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var id = dr.GetInt32(0);
                    returnvalue = id;
                }
                return returnvalue;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        public static int GetUserIDbyMail(string email)
        {
            int returnvalue = -1;
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT USERID FROM TUSER WHERE EMAIL = :findEmail";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("findEmail", OracleDbType.Varchar2).Value = email;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var id = dr.GetInt32(0);
                    returnvalue = id;
                }
                return returnvalue;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            finally
            {
                Disconnect();
            }
        }

        static bool ExtendVolunteer(int volun)
        {
            try
            {
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                "Insert into TVOLUNTEER(USERID, RIJBEWIJS, BIOGRAFIE, VOG, FOTO) VALUES (:newUSERID, :newRIJBEWIJS, :newBIOGRAFIE, :newVOG, :newFOTO)";
                cmd.Parameters.Add("newUSERID", OracleDbType.Int32).Value = volun;
                cmd.Parameters.Add("newRIJBEWIJS", OracleDbType.Varchar2).Value = "NEE";
                cmd.Parameters.Add("newBIOGRAFIE", OracleDbType.Varchar2).Value = "Niet opgegeven";
                cmd.Parameters.Add("newVOG", OracleDbType.Varchar2).Value = "Onbekend";
                cmd.Parameters.Add("newFOTO", OracleDbType.Varchar2).Value = "Onbekend";
                cmd.ExecuteNonQuery();

                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                "Insert into TROOSTER(USERID, MAANDAG, DINSDAG, WOENSDAG, DONDERDAG, VRIJDAG, ZATERDAG, ZONDAG) VALUES (:newUSERID, 'Niet Opgegeven', 'Niet Opgegeven', 'Niet Opgegeven', 'Niet Opgegeven', 'Niet Opgegeven', 'Niet Opgegeven', 'Niet Opgegeven')";
                cmd.Parameters.Add("newUSERID", OracleDbType.Int32).Value = volun;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static Volunteer GetVolunteerDetails(Volunteer volun)
        {
            Volunteer toget = volun;
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM TVRIJWILLIGER WHERE ID = " + volun.UserID;
                cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    // Read from DB
                    int id = dr.GetInt32(0);
                    DateTime geboortedatum = dr.GetDateTime(1);
                    var photo = SafeReadString(dr, 2);
                    var vog = SafeReadString(dr, 3);
                    var gebruikerid = SafeReadInt(dr, 4);
      

                    // Fill
                    toget.PathToPhoto = photo;
                    toget.PathToVOG = vog;

                }

                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT ROUND(SUM(beoordeling)/COUNT(beoordeling)*2, 1) FROM TREVIEW WHERE vrijwilligerid = " + toget.UserID;
                cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    // Read from DB
                    var rating = SafeReadDecimal(dr, 0);

                    // Fill
                    toget.Rating = rating;
                }
                return toget;
            }
            catch (InvalidCastException ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
            finally
            {
                Disconnect();
            }
        }

        public static bool UpdateVolunteer(Volunteer volun)
        {
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                   "UPDATE TVOLUNTEER SET RIJBEWIJS = :newRIJBEWIJS, BIOGRAFIE = :newBIOGRAFIE, VOG = :newVOG, FOTO = :newFOTO WHERE USERID = " + volun.UserID;

                if(volun.DrivingLicense)
                    cmd.Parameters.Add("newRIJBEWIJS", OracleDbType.Varchar2).Value = "JA";
                else
                    cmd.Parameters.Add("newRIJBEWIJS", OracleDbType.Varchar2).Value = "NEE";

                cmd.Parameters.Add("newBIOGRAFIE", OracleDbType.Varchar2).Value = volun.Biogragphy;
                cmd.Parameters.Add("newVOG", OracleDbType.Varchar2).Value = volun.PathToVOG;
                cmd.Parameters.Add("newFOTO", OracleDbType.Varchar2).Value = volun.PathToPhoto;
                cmd.ExecuteNonQuery();

                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                   "UPDATE TROOSTER SET MAANDAG = :newMAANDAG, DINSDAG = :newDINSDAG, WOENSDAG = :newWOENSDAG, DONDERDAG = :newDONDERDAG, VRIJDAG = :newVRIJDAG, ZATERDAG = :newZATERDAG, ZONDAG = :newZONDAG WHERE USERID = " + volun.UserID;

                cmd.Parameters.Add("newMAANDAG", OracleDbType.Varchar2).Value = volun.Schedule.Monday;
                cmd.Parameters.Add("newDINSDAG", OracleDbType.Varchar2).Value = volun.Schedule.Tuesday;
                cmd.Parameters.Add("newWOENSDAG", OracleDbType.Varchar2).Value = volun.Schedule.Wednesday;
                cmd.Parameters.Add("newDONDERDAG", OracleDbType.Varchar2).Value = volun.Schedule.Thursday;
                cmd.Parameters.Add("newVRIJDAG", OracleDbType.Varchar2).Value = volun.Schedule.Friday;
                cmd.Parameters.Add("newZATERDAG", OracleDbType.Varchar2).Value = volun.Schedule.Saturday;
                cmd.Parameters.Add("newZONDAG", OracleDbType.Varchar2).Value = volun.Schedule.Sunday;

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                Disconnect();
            }
        }

        public static bool AddAppointment(Appointment meeting)
        {
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                    "Insert into TAFSPRAAK(CLIENT, VOLUNTEER, DATUMTIJD, LOCATIE) VALUES (:NewCLIENT, :NewVOLUNTEER, :NewDATUMTIJD, :NewLOCATIE)";

                cmd.Parameters.Add("NewCLIENT", OracleDbType.Int32).Value = meeting.Client.UserID;
                cmd.Parameters.Add("NewVOLUNTEER", OracleDbType.Int32).Value = meeting.Volunteer.UserID;
                cmd.Parameters.Add("NewDATUMTIJD", OracleDbType.Varchar2).Value = meeting.DateString;
                cmd.Parameters.Add("NewLOCATIE", OracleDbType.Varchar2).Value = meeting.Location;

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                Disconnect();
            }
        }

        public static bool UpdateVOG(string vogpath, int userid)
        {
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                   "UPDATE TVOLUNTEER SET VOG = :newVOG WHERE USERID = :someID";
                cmd.Parameters.Add("newVOG", OracleDbType.Varchar2).Value = vogpath;
                cmd.Parameters.Add("someID", OracleDbType.Int32).Value = userid;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                Disconnect();
            }
        }

        public static bool UpdatePhoto(string imgpath, int userid)
        {
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                   "UPDATE TVOLUNTEER SET FOTO = :newFOTO WHERE USERID = :someID";
                cmd.Parameters.Add("newVOG", OracleDbType.Varchar2).Value = imgpath;
                cmd.Parameters.Add("someID", OracleDbType.Int32).Value = userid;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                Disconnect();
            }
        }

        public static List<Appointment> GetMyAppointments(Client client)
        {
            List<Appointment> returnlist = new List<Appointment>();
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT VOLUNTEER, DATUMTIJD, LOCATIE FROM TAFSPRAAK WHERE CLIENT = :newUSERID";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("newUSERID", OracleDbType.Varchar2).Value = client.UserID;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var volunid = dr.GetInt32(0);
                    var datetime = dr.GetString(1);
                    var location = dr.GetString(2);

                    returnlist.Add(new Appointment(client, GetUserNoConnect(volunid) as Volunteer, Convert.ToDateTime(datetime), location));
                }
                return returnlist;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return returnlist;
            }
            finally
            {
                Disconnect();
            }
        }

        public static List<Appointment> GetMyAppointments(Volunteer volun)
        {
            List<Appointment> returnlist = new List<Appointment>();
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT CLIENT, DATUMTIJD, LOCATIE FROM TAFSPRAAK WHERE VOLUNTEER = :newUSERID";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("newUSERID", OracleDbType.Varchar2).Value = volun.UserID;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var clientid = dr.GetInt32(0);
                    var datetime = dr.GetString(1);
                    var location = dr.GetString(2);

                    returnlist.Add(new Appointment(GetUserNoConnect(clientid) as Client, volun as Volunteer, Convert.ToDateTime(datetime), location));
                }
                return returnlist;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return returnlist;
            }
            finally
            {
                Disconnect();
            }
        }

        public static List<Review> GetMyReviews(Client client)
        {
            List<Review> returnlist = new List<Review>();
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT VOLUNTEER, DATUM, RATING, TEKST FROM TREVIEW WHERE CLIENT = :newUSERID";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("newUSERID", OracleDbType.Varchar2).Value = client.UserID;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var volunid = dr.GetInt32(0);
                    var datetime = dr.GetDateTime(1);
                    var rating = dr.GetInt32(2);
                    var content = dr.GetString(3);

                    returnlist.Add(new Review(datetime, client, GetUserNoConnect(volunid) as Volunteer, rating, content));
                }
                return returnlist;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return returnlist;
            }
            finally
            {
                Disconnect();
            }
        }

        public static List<Review> GetMyReviews(Volunteer volun)
        {
            List<Review> returnlist = new List<Review>();
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT CLIENT, DATUM, RATING, TEKST FROM TREVIEW WHERE VOLUNTEER = :newUSERID";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("newUSERID", OracleDbType.Varchar2).Value = volun.UserID;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var clientid = dr.GetInt32(0);
                    var datetime = dr.GetDateTime(1);
                    var rating = dr.GetInt32(2);
                    var content = dr.GetString(3);

                    returnlist.Add(new Review(datetime, GetUserNoConnect(clientid) as Client, volun , rating, content));
                }
                return returnlist;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return returnlist;
            }
            finally
            {
                Disconnect();
            }
        }
    }
}
