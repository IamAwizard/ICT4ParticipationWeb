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
using Project;

namespace Project
{
   public  class DatabaseHandler
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
        public void Connect()
        {
            con = new OracleConnection();
            con.ConnectionString = connectionstring;
            con.Open();
            Console.WriteLine("CONNECTION SUCCESFULL");

        }

        /// <summary>
        /// Disconnect from the database...
        /// </summary>
        public void Disconnect()
        {
            con.Close();
            con.Dispose();
        }

        /// <summary>
        ///  Used to replace null values with string "NULL" values
        ///  not in use as of 5-11-2015
        /// </summary>
        /// <param name="cmd"></param>
         void PopulateNullParameters(OracleCommand cmd)
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
         string SafeReadString(OracleDataReader odr, int ColIndex)
        {
            {
                if (!odr.IsDBNull(ColIndex))
                    return odr.GetString(ColIndex);
                else
                    return string.Empty;
            }
        }

         int SafeReadInt(OracleDataReader odr, int ColIndex)
        {
            {
                if (!odr.IsDBNull(ColIndex))
                    return odr.GetInt32(ColIndex);
                else
                    return -1;
            }
        }

         decimal SafeReadDecimal(OracleDataReader odr, int ColIndex)
        {
            {
                if (!odr.IsDBNull(ColIndex))
                    return odr.GetDecimal(ColIndex);
                else
                    return 0;
            }
        }

        // KLOPT NIET


        //public static List<Question> GetAllQuestions()
        //{
        //    List<Question> questionlist = new List<Question>();
        //    try
        //    {
        //        Connect();
        //        cmd = new OracleCommand();
        //        cmd.Connection = con;
        //        cmd.CommandText = "SELECT H.ID, H.OMSCHRIJVING, H.LOCATIE , H.REISTIJD, H.STARTDATUM, H.EINDDATUM, H.URGENT, H.AANTALVRIJWILLIGERS, H.VERVOERTYPE, TVAARDIGHEID.ID as vaardigheidID, TVRIJWILLIGER.ID as vrijwilligerID FROM THULPVRAAG H, TVAARDIGHEID, TVRIJWILLIGER, THULPVRAAG_VAARDIGHEID, THULPVRAAG_VRIJWILLIGER WHERE (H.ID = THULPVRAAG_VRIJWILLIGER.hulpvraagID AND THULPVRAAG_VRIJWILLIGER.vrijwilligerid = TVRIJWILLIGER.id) AND (H.ID = THULPVRAAG_VAARDIGHEID.hulpvraagID AND THULPVRAAG_VAARDIGHEID.vaardigheidID = TVAARDIGHEID.id)"; // QUERY
        //        cmd.CommandType = CommandType.Text;
        //        dr = cmd.ExecuteReader();
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.ToString());
        //        Disconnect();
        //        return null;
        //    }

        //    try
        //    {
        //        while (dr.Read())
        //        {
        //            // Read from DB
        //            int hulpvraagid = dr.GetInt32(0);
        //            var omschrijving = dr.GetInt32(1);
        //            var locatie = SafeReadString(dr, 2);
        //            var reistijd = SafeReadString(dr, 3);
        //            DateTime startdatum = dr.GetDateTime(4);
        //            DateTime einddatum = dr.GetDateTime(5);
        //            var urgentie = SafeReadString(dr, 6);
        //            int aantalvrijwilligers = SafeReadInt(dr, 7);
        //            int vervoertype = SafeReadInt(dr, 8);
        //            int vaardigheidid = SafeReadInt(dr, 9);
        //            int vrijwilligerid = SafeReadInt(dr, (10));

        //            //Question toadd;
        //            //toadd = new Question(null, auteur, locatie, vervoer, afstand, bijzonderheid, vraag, datum, opgelost);
        //            //toadd.ID = hulpvraagid;
        //            //toadd.VolunteerID = vrijwilligerid;
        //            //questionlist.Add(toadd);
        //        }
        //        foreach (Question q in questionlist)
        //        {
                    
        //            q.Client = (Client)GetUser(q.Auteur);
        //        }

        //        foreach (Question q in questionlist)
        //        {
        //            if (q. != -1)
        //            {
        //                q.Volunteer = (Volunteer)GetUser(q.VolunteerID);
        //            }
        //        }
        //        return questionlist;
        //    }
        //    catch (Exception ex)
        //    {
        //         MessageBox.Show(ex.Message);
        //        return null;
        //    }

        //}


            // MOET OPNIEUW GEMAAKT WORDEN

        //public static User GetUser(int ids)
        //{
        //    User toadd = null;
        //    try
        //    {
        //        Connect();
        //        cmd = new OracleCommand();
        //        cmd.Connection = con;
        //        cmd.CommandText = "SELECT USERID, NAAM, GEBOORTEDATUM, GESLACHT, WOONPLAATS, ADRES, EMAIL, WACHTWOORD, TYPE FROM TUSER WHERE USERID = " + ids; // QUERY
        //        cmd.CommandType = CommandType.Text;
        //        dr = cmd.ExecuteReader();
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.ToString());
        //        Disconnect();
        //        return null;
        //    }

        //    try
        //    {
        //        while (dr.Read())
        //        {
        //            // Read from DB
        //            var id = dr.GetInt32(0);
        //            var name = dr.GetString(1);
        //            var dateOfBirth = dr.GetDateTime(2);
        //            var gender = dr.GetString(3);
        //            var city = dr.GetString(4);
        //            var adress = dr.GetString(5);
        //            var email = dr.GetString(6);
        //            var password = dr.GetString(7);

        //            var type = dr.GetString(8);


        //            switch (type)
        //            {
        //                case "CLIENT":
        //                    Client newClient = new Client(name, dateOfBirth, gender, city, adress, email, password);
        //                    toadd = newClient;
        //                    toadd.UserID = id;
        //                    break;
        //                case "VOLUNTEER":
        //                    toadd = null;
        //                    Volunteer newUser = new Volunteer(name, dateOfBirth, gender, city, adress, email, password, false, "Niet Opgegeven", "ONBEKEND", "ONBEKEND");
        //                    toadd = newUser;
        //                    toadd.UserID = id;
        //                    break;
        //                case "ADMIN":
        //                    Admin newAdmin = new Admin(name, dateOfBirth, gender, city, adress, email, password);
        //                    toadd = newAdmin;
        //                    toadd.UserID = id;
        //                    break;
        //                default:
        //                    toadd = null;
        //                    break;
        //            }

        //        }
        //        Disconnect();
        //        return toadd;
        //    }
        //    catch (Exception ex)
        //    {
        //        Disconnect();
        //        MessageBox.Show(ex.ToString());
        //        return null;
        //    }
        //}


        public User GetUserNoConnect(int ids)
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

        public List<User> GetAllUsers()
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

        public void Read(string sql)
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

        //MOET HELEMAAL OPNIEUW GEMAAKT WORDEN

        //public static void AddUser(User newuser)
        //{
        //    try
        //    {
        //        Connect();
        //        cmd = new OracleCommand();
        //        cmd.Connection = con;
        //        cmd.CommandText =
        //            "Insert into TUSER(NAAM, WOONPLAATS, ADRES, EMAIL, WACHTWOORD, TYPE) VALUES (:NewNAAM, :NewWOONPLAATS, :NewADRES, :NewEMAIL, :NewWACHTWOORD, :NewTYPE)";

        //        cmd.Parameters.Add("NewNAAM", OracleDbType.Varchar2).Value = newuser.Name;
        //        cmd.Parameters.Add("NewWOONPLAATS", OracleDbType.Varchar2).Value = newuser.Location; 
        //        cmd.Parameters.Add("NewADRES", OracleDbType.Varchar2).Value = newuser.Adress; 

        //        cmd.Parameters.Add("NewWACHTWOORD", OracleDbType.Varchar2).Value = newuser.Password;
        //        if (newuser is Client)
        //            cmd.Parameters.Add("NewTYPE", OracleDbType.Varchar2).Value = "CLIENT";
        //        if (newuser is Volunteer)
        //            cmd.Parameters.Add("NewTYPE", OracleDbType.Varchar2).Value = "VOLUNTEER";
        //        if (newuser is Admin)
        //            cmd.Parameters.Add("NewTYPE", OracleDbType.Varchar2).Value = "ADMIN";
        //        cmd.ExecuteNonQuery();

        //        if(newuser is Volunteer)
        //        {
        //            ExtendVolunteer(GetUserID(newuser.Email));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        Disconnect();
        //    }
        //}

        public void AddAvatar()
        {

        }

        public bool AddQuestion(Question newquestion)
        {
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                    "Insert into THULPVRAAG(OMSCHRIJVING, LOCATIE, REISTIJD, STARTDATUM, EINDDATUM, URGENT, AANTALVRIJWILLIGERS, VERVOERTYPE) VALUES (:NewOMSCHRIJVING, :NewLOCATIE, :NewREISTIJD, :NewSTARTDATUM, :NewEINDDATUM, :NewURGENT, :NewAANTALVRIJWILLIGERS, :NewVERVOERTYPE)";

                cmd.Parameters.Add("NewOMSCHRIJVING", OracleDbType.Varchar2).Value = newquestion.Description;
                cmd.Parameters.Add("NewLOCATIE", OracleDbType.Varchar2).Value = newquestion.Location;
                cmd.Parameters.Add("NewREISTIJD", OracleDbType.Varchar2).Value = newquestion.TravelTime;
                cmd.Parameters.Add("NewSTARTDATUM", OracleDbType.Date).Value = newquestion.DateBegin;
                cmd.Parameters.Add("NewEINDDATUM", OracleDbType.Date).Value = newquestion.DateEnd;
                cmd.Parameters.Add("NewURGENT", OracleDbType.Varchar2).Value = newquestion.Critical;
                cmd.Parameters.Add("NewAANTALVRIJWILLIGERS", OracleDbType.Varchar2).Value = newquestion.VolunteersNeeded;
                cmd.Parameters.Add("NewVERVOERTYPE", OracleDbType.Varchar2).Value = newquestion.Transport;

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

        public bool AddNewQuestion(Question newquestion)
        {
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                    "Insert into THULPVRAAG(OMSCHRIJVING,auteur,startdatum,aantalvrijwilligers) VALUES (:NewOMSCHRIJVING,:NewAuteur,:NewStartdatum,:NewAantalvrijwilligers)";

              
                cmd.Parameters.Add("NewOMSCHRIJVING", OracleDbType.Varchar2).Value = newquestion.Description;
                cmd.Parameters.Add("NewDatum", OracleDbType.Date).Value = newquestion.AuthorID;
                cmd.Parameters.Add("NewDatum", OracleDbType.Date).Value = DateTime.Now;
                cmd.Parameters.Add("NewAantalvrijwilligers", OracleDbType.Int32).Value = newquestion.VolunteersNeeded;
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
        public bool AuthenticateUser(string Email, string Pass)
        {
            try
            {
                Connect();

                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM TACCOUNT WHERE email =:NewEmail AND Wachtwoord =:NewWachtwoord";
                cmd.Parameters.Add("NewEmail", Email);
                cmd.Parameters.Add("NewWachtwoord", Pass);
                cmd.CommandType = System.Data.CommandType.Text;
                dr = cmd.ExecuteReader();

                // ik gebruik hier de Hasrows methode. Als de waardes kloppen, dan zijn er rijen. 
                // Deze bool is dus handig om te gebruiken als je een overeenkomst wil weten of
                // er een overeenkomst is tussen de ingevulde waardes en de gebruiker.
                if (dr.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {

                return false;
            }
            finally
            {
                Disconnect();
            }
        }
        public List<Account> GetUserCache()
        {
            try
            {
                Connect();
                List<Account> requiredList = new List<Account>();
                cmd = new OracleCommand();
                cmd.CommandText =
                    "SELECT  email gebruikersnaam wachtwoord FROM TACCOUNT ";
                cmd.CommandType = System.Data.CommandType.Text;
                dr = cmd.ExecuteReader();
                while(dr.Read())
                {
                   
                    var email = SafeReadString(dr, 0);
                    var gebruikersnaam = SafeReadString(dr, 1);
                    var wachtwoord = SafeReadString(dr, 2);
                    Account user = new Account(gebruikersnaam, wachtwoord, email);
                }
                return requiredList;

            } catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return null;
            }
            finally
            {
                Disconnect();
            }
        }

        public bool UpdateQuestion(Question question)
        {
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                   "UPDATE THULPVRAAG SET OMSCHRIJVING = :NewOMSCHRIJVING, LOCATIE = :NewLOCATIE, REISTIJD = :NewREISTIJD, URGENT = :newUrgent, AANTALVRIJWILLIGERS = :newAantalvrijwilligers, VERVOERTYPE = :newVervoertype WHERE id = :newIDvalue";

                cmd.Parameters.Add("NewOMSCHRIJVING", OracleDbType.Varchar2).Value = question.Description;
                cmd.Parameters.Add("NewLOCATIE", OracleDbType.Varchar2).Value = question.Location;
                cmd.Parameters.Add("NewREISTIJD", OracleDbType.Varchar2).Value = question.TravelTime;
                cmd.Parameters.Add("NewURGENT", OracleDbType.Varchar2).Value = question.Critical;
                //cmd.Parameters.Add("NewAANTALVRIJWILLIGERS", OracleDbType.Varchar2).Value = question.; wat is dit?
                cmd.Parameters.Add("NewVERVOERTYPE", OracleDbType.Varchar2).Value = question.Transport;
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

        public bool AddReview(Review newreview)
        {
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                    "Insert into TREVIEW(VOLUNTEER, CLIENT, RATING) VALUES ( :NewVOLUNTEER, :NewCLIENT, :NewRATING)";

                cmd.Parameters.Add("NewVOLUNTEER", OracleDbType.Int32).Value = newreview.VolunteerID;
                cmd.Parameters.Add("NewCLIENT", OracleDbType.Int32).Value = newreview.ClientID;
                cmd.Parameters.Add("NewRATING", OracleDbType.Int32).Value = newreview.Rating;


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

        public bool DeleteUser(User usertodelete)
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


        public bool SendChatMessage(Client client, Volunteer volunteer, string message)
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

        public List<Chat> GetChat(Client client, Volunteer volunteer)
        {
            List<Chat> chatmessages = new List<Chat>();
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT CHATID, BERICHT,tijdstip FROM TCHAT WHERE HULPBEHOEVENDEID = " + client.UserID + " AND VRIJWILLIGERID = " + volunteer.UserID;
                cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var id = dr.GetInt32(0);
                    var bericht = dr.GetString(1);
                    var tijdstip = dr.GetDateTime(2);

                    chatmessages.Add(new Chat(id,bericht,tijdstip ,client, volunteer));
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


        public List<Review> GetAllReviews()
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

        public bool DeleteReview(int reviewID)
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

        public bool DeleteQuestion(int QuestionID)
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

        public int GetUserID(string email)
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

        public int GetUserIDbyMail(string email)
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

        public bool ExtendVolunteer(int volun)
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

        public Volunteer GetVolunteerDetails(Volunteer volun)
        {
          
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
                    volun.Photo = photo;
                    volun.VOG = vog;

                }

             
                return volun;
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

        //NOG WAT VAAG

        //public static bool UpdateVolunteer(Volunteer volun)
        //{
        //    try
        //    {
        //        Connect();
        //        cmd = new OracleCommand();
        //        cmd.Connection = con;
        //        cmd.CommandText =
        //           "UPDATE TVOLUNTEER SET RIJBEWIJS = :newRIJBEWIJS,  VOG = :newVOG, FOTO = :newFOTO WHERE USERID = " + volun.UserID;

        //        if(volun.License == "true")
        //            cmd.Parameters.Add("newRIJBEWIJS", OracleDbType.Varchar2).Value = "JA";
        //        else
        //            cmd.Parameters.Add("newRIJBEWIJS", OracleDbType.Varchar2).Value = "NEE";

        //        cmd.Parameters.Add("newVOG", OracleDbType.Varchar2).Value = volun.VOG;
        //        cmd.Parameters.Add("newFOTO", OracleDbType.Varchar2).Value = volun.Photo;
        //        cmd.ExecuteNonQuery();

        //        cmd = new OracleCommand();
        //        cmd.Connection = con;
        //        cmd.CommandText =
        //           "INSERT INTO TBESCHIKBAARHEID (dagnaam,dagdeel,vrijwilligerid) VALUES(:Newdagnaam,:Newdagdeel,:Newvrijwilligerid)";

        //        cmd.Parameters.Add("dagnaam", OracleDbType.Varchar2).Value = 
        //        cmd.Parameters.Add("dagdeel", OracleDbType.Varchar2).Value = volun.
        //        cmd.Parameters.Add("vrijwilligerid", OracleDbType.Int32).Value = volun.ID

              
              
        //        cmd.ExecuteNonQuery();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return false;
        //    }
        //    finally
        //    {
        //        Disconnect();
        //    }
        //}

        public bool AddAppointment(Meeting meeting)
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
                cmd.Parameters.Add("NewDATUMTIJD", OracleDbType.Varchar2).Value = meeting.Date;
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

        public bool UpdateVOG(string vogpath, int userid)
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

        public bool UpdatePhoto(string imgpath, int userid)
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

        public List<Meeting> GetMyAppointments(Client client)
        {
            List<Meeting> returnlist = new List<Meeting>();
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

                    returnlist.Add(new Meeting(client, GetUserNoConnect(volunid) as Volunteer, Convert.ToDateTime(datetime), location));
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

        public List<Meeting> GetMyAppointments(Volunteer volun)
        {
            List<Meeting> returnlist = new List<Meeting>();
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

                    returnlist.Add(new Meeting(GetUserNoConnect(clientid) as Client, volun as Volunteer, Convert.ToDateTime(datetime), location));
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

        public List<Review> GetMyReviews(Client client)
        {
            List<Review> returnlist = new List<Review>();
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT ID, Opmerkingen,beoordeling, FROM TREVIEW WHERE CLIENT ='"+client+"'";
                cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var volunid = dr.GetInt32(0);
                    var opmerkingen = dr.GetString(1);
                    var rating = dr.GetInt32(2);


                    returnlist.Add(new Review(volunid, rating, opmerkingen));
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


        //WERKT NOG NIET GOED
        public List<Review> GetMyReviews(Volunteer volun)
        {
            List<Review> returnlist = new List<Review>();
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT beoordeling,  opmerkingen FROM TREVIEW WHERE vrijwilligerid='" + volun.VolunteerID + "'";
                cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    var rating = dr.GetInt32(0);
                    var comment = dr.GetString(1);

                    returnlist.Add(new Review(rating, comment));
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
