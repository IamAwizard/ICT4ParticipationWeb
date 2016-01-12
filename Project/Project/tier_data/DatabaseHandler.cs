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
    class DatabaseHandler
    {
        // Fields

        // connectionstring = "User Id=loginname; Password=password;Data Source=localhost";
        private string connectionstring = "User Id=dbi259530;Password=ZBEB4DKxvr;Data Source=192.168.15.50/fhictora";
        private OracleConnection con;
        private OracleCommand cmd;
        private OracleDataReader dr;

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
        /// Safely get string values from the oracledatareader if they are null
        /// </summary>
        /// <param name="odr">oracle datareader</param>
        /// <param name="colindex">column index</param>
        /// <returns>empty string otherwise value from DB</returns>
        string SafeReadString(OracleDataReader odr, int colindex)
        {
            if (!odr.IsDBNull(colindex))
                return odr.GetString(colindex);
            else
                return string.Empty;
        }

        /// <summary>
        /// Safely reads int values from the database if they are null
        /// </summary>
        /// <param name="odr">oracle datareader</param>
        /// <param name="colindex">column index</param>
        /// <returns>-1 if null otherwise value from DB</returns>
        int SafeReadInt(OracleDataReader odr, int colindex)
        {
            if (!odr.IsDBNull(colindex))
                return odr.GetInt32(colindex);
            else
                return -1;
        }

        /// <summary>
        /// Safely reads decimal values from the database if they are null
        /// </summary>
        /// <param name="odr">oracle datareader</param>
        /// <param name="colindex">column index</param>
        /// <returns>0 if null otherwise value from DB</returns>
        decimal SafeReadDecimal(OracleDataReader odr, int colindex)
        {
            if (!odr.IsDBNull(colindex))
                return odr.GetDecimal(colindex);
            else
                return 0;
        }

        /// <summary>
        /// Safely reads datetime values from the database if they are null
        /// </summary>
        /// <param name="odr">oracle datareader</param>
        /// <param name="colindex">column index</param>
        /// <returns>datetime minimimvalue if null otherwise value from DB</returns>
        DateTime SafeReadDateTime(OracleDataReader odr, int colindex)
        {
            if (!odr.IsDBNull(colindex))
                return odr.GetDateTime(colindex);
            else
                return DateTime.MinValue;
        }

        /// <summary>
        /// Adds a new account to the database.
        /// Adding of admins is not support this way.
        /// Email for account has to be unique
        /// </summary>
        /// <param name="newaccount">Account to be added</param>
        /// <returns>true if success, otherwise false</returns>
        public bool AddAccount(Account newaccount)
        {
            try
            {
                Connect();
                using (cmd = new OracleCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT EMAIL FROM TACCOUNT WHERE LOWER(EMAIL) = LOWER(:newEmail)";
                    cmd.Parameters.Add("newEmail", newaccount.Email);
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        return false;
                    }
                }
                // Account
                using (cmd = new OracleCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO TACCOUNT(EMAIL, WACHTWOORD, GEBRUIKERSNAAM) VALUES(lower(:newEmail), :newPassword, :newUserName)";
                    cmd.Parameters.Add("newEmail", newaccount.Email);
                    cmd.Parameters.Add("newPassword", newaccount.Password);
                    cmd.Parameters.Add("newUserName", newaccount.Username);
                    cmd.ExecuteNonQuery();
                }

                // User
                if (newaccount is User)
                {
                    User newuser = newaccount as User;
                    int accountid = -1;
                    using (cmd = new OracleCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT ID FROM TACCOUNT WHERE LOWER(EMAIL) = LOWER(:newEmail)";
                        cmd.Parameters.Add("newEmail", newuser.Email);
                        dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            accountid = SafeReadInt(dr, 0);
                        }
                    }
                    if (accountid != -1)
                    {
                        using (cmd = new OracleCommand())
                        {
                            cmd.Connection = con;
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "INSERT INTO TGEBRUIKER(NAAM,ADRES,WOONPLAATS,TELEFOONNUMMER,HEEFTRIJBEWIJS,HEEFTAUTO,UITSCHRIJVINGSDATUM,ACCOUNTID) VALUES (:newName, :newAdress, :newLocation, :newPhoneNr, :newLicense, :newCar,NULL,:newAccountID)";
                            cmd.Parameters.Add("newName", newuser.Name);
                            cmd.Parameters.Add("newAdress", newuser.Adress);
                            cmd.Parameters.Add("newLocation", newuser.Location);
                            cmd.Parameters.Add("newPhoneNr", newuser.Phonenumber);
                            cmd.Parameters.Add("newLicense", newuser.License);
                            cmd.Parameters.Add("newCar", newuser.Hascar);
                            cmd.Parameters.Add("newAccountID", accountid);
                            cmd.ExecuteNonQuery();
                        }

                        // Fetch UserID
                        int userid = -1;
                        using (cmd = new OracleCommand())
                        {
                            cmd.Connection = con;
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT ID FROM TGEBRUIKER WHERE NAAM = :Name AND ADRES = :Adress AND WOONPLAATS = :Location AND TELEFOONNUMMER = :Phonenumber AND ROWNUM = 1";
                            cmd.Parameters.Add("Name", newuser.Name);
                            cmd.Parameters.Add("Adress", newuser.Adress);
                            cmd.Parameters.Add("Location", newuser.Location);
                            cmd.Parameters.Add("Phonenumber", newuser.Phonenumber);
                            dr = cmd.ExecuteReader();

                            while (dr.Read())
                            {
                                userid = SafeReadInt(dr, 0);
                            }
                        }
                        if (userid != -1)
                        {

                            // Client
                            if (newuser is Client)
                            {
                                Client newclient = newuser as Client;
                                using (cmd = new OracleCommand())
                                {
                                    cmd.Connection = con;
                                    cmd.CommandType = CommandType.Text;
                                    cmd.CommandText = "INSERT INTO THULPBEHOEVENDE(OVMOGELIJK,GEBRUIKERID) VALUES (:newOvPossible,:newUserID)";
                                    cmd.Parameters.Add("newOvPossible", newclient.OVpossible);
                                    cmd.Parameters.Add("newUserID", userid);
                                    cmd.ExecuteNonQuery();

                                    return true;
                                }
                            }
                            // Volunteer
                            if (newuser is Volunteer)
                            {
                                Volunteer newvoluntuur = newuser as Volunteer;
                                using (cmd = new OracleCommand())
                                {
                                    cmd.Connection = con;
                                    cmd.CommandType = CommandType.Text;
                                    cmd.CommandText = "INSERT INTO TVRIJWILLIGER(GEBOORTEDATUM, FOTO, VOG, GEBRUIKERID) VALUES(:newDoB, :newPhoto, :newVOG, :newUserID)";
                                    cmd.Parameters.Add("newDoB", newvoluntuur.DateOfBirth);
                                    cmd.Parameters.Add("newPhoto", newvoluntuur.Photo);
                                    cmd.Parameters.Add("newVOG", newvoluntuur.VOG);
                                    cmd.Parameters.Add("newUserID", userid);
                                    cmd.ExecuteNonQuery();
                                    return true;
                                }
                            }
                            return false;
                        }
                        return false;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                Disconnect();
            }
        }

        /// <summary>
        /// Finds a volunteer ID by email
        /// </summary>
        /// <param name="email">email to look for</param>
        /// <returns>-1 if not found otherwise volunteer id</returns>
        public int GetVolunteerIdByEmail(string email)
        {
            try
            {
                Connect();
                int returnid = -1;
                using (cmd = new OracleCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT V.ID FROM TVRIJWILLIGER V, TGEBRUIKER G, TACCOUNT A WHERE V.GEBRUIKERID = G.ID AND G.ACCOUNTID = A.ID AND LOWER(A.EMAIL) = LOWER(:email)";
                    cmd.Parameters.Add("email", email);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        returnid = SafeReadInt(dr, 0);
                    }
                    return returnid;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return -1;
            }
            finally
            {
                Disconnect();
            }
        }

        /// <summary>
        /// Finds a client ID by email
        /// </summary>
        /// <param name="email">email to look for</param>
        /// <returns>-1 if not found otherwise client id</returns>
        public int GetClientIdByEmail(string email)
        {
            try
            {
                Connect();
                int returnid = -1;
                using (cmd = new OracleCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT H.ID FROM THULPBEHOEVENDE H, TGEBRUIKER G, TACCOUNT A WHERE H.GEBRUIKERID = G.ID AND G.ACCOUNTID = A.ID AND LOWER(A.EMAIL) = LOWER(:email)";
                    cmd.Parameters.Add("email", email);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        returnid = SafeReadInt(dr, 0);
                    }
                    return returnid;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return -1;
            }
            finally
            {
                Disconnect();
            }
        }

        /// <summary>
        /// Finds a admin ID by email
        /// </summary>
        /// <param name="email">email to look for</param>
        /// <returns>-1 if not found otherwise admin id</returns>
        public int GetAdminIdByEmail(string email)
        {
            try
            {
                Connect();
                int returnid = -1;
                using (cmd = new OracleCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT B.ID FROM TBEHEERDER B, TACCOUNT A WHERE B.ACCOUNTID = A.ID AND LOWER(EMAIL) = LOWER(:email)";
                    cmd.Parameters.Add("email", email);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        returnid = SafeReadInt(dr, 0);
                    }
                    return returnid;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return -1;
            }
            finally
            {
                Disconnect();
            }
        }

        /// <summary>
        /// Checks if a account exists with given email and password
        /// </summary>
        /// <param name="email">email to login with</param>
        /// <param name="password">password to login with</param>
        /// <returns>true is exists, else false</returns>
        public bool ValidateCredentials(string email, string password)
        {
            try
            {
                Connect();
                using (cmd = new OracleCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM TACCOUNT WHERE LOWER(EMAIL) = LOWER(:Email) AND WACHTWOORD =:Password";
                    cmd.Parameters.Add("Email", email);
                    cmd.Parameters.Add("Password", password);
                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                        return true;
                    else
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

        /// <summary>
        /// Gets a account from the database
        /// </summary>
        /// <param name="email">email to login with</param>
        /// <returns></returns>
        public Account GetAccount(string email)
        {
            try
            {
                int someid = -1;
                someid = GetClientIdByEmail(email);
                if (someid != -1)
                {
                    Connect();
                    Client returnclient = null;
                    using (cmd = new OracleCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT * FROM TACCOUNT A, TGEBRUIKER G, THULPBEHOEVENDE H WHERE A.ID = G.ACCOUNTID AND H.GEBRUIKERID = G.ID AND LOWER(A.EMAIL) =  LOWER(:email)";
                        cmd.Parameters.Add("email", email);
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            var accountid = SafeReadInt(dr, 0);
                            var username = SafeReadString(dr, 1);
                            var password = SafeReadString(dr, 2);
                            var clientemail = SafeReadString(dr, 3);
                            var userid = SafeReadInt(dr, 4);
                            var name = SafeReadString(dr, 5);
                            var adress = SafeReadString(dr, 6);
                            var location = SafeReadString(dr, 7);
                            var phonenumber = SafeReadString(dr, 8);
                            var haslicense = SafeReadString(dr, 9);
                            var hascar = SafeReadString(dr, 10);
                            var unsubscribedate = SafeReadDateTime(dr, 11);
                            var trash1 = SafeReadInt(dr, 12);
                            var clientid = SafeReadInt(dr, 13);
                            var ovpossible = SafeReadString(dr, 14);
                            var thrash2 = SafeReadInt(dr, 15);
                            // Create
                            returnclient = new Client(accountid, username, password, email, userid, name, adress, location, phonenumber, haslicense, hascar, clientid, ovpossible, unsubscribedate);
                        }
                        return returnclient;
                    }
                }
                someid = GetVolunteerIdByEmail(email);
                if (someid != -1)
                {
                    Connect();
                    Volunteer returnvolunteer = null;
                    using (cmd = new OracleCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT * FROM TACCOUNT A, TGEBRUIKER G, TVRIJWILLIGER V WHERE A.ID = G.ACCOUNTID AND G.ID = V.GEBRUIKERID AND LOWER(A.EMAIL) = LOWER(:email)";
                        cmd.Parameters.Add("email", email);
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            var accountid = SafeReadInt(dr, 0);
                            var username = SafeReadString(dr, 1);
                            var password = SafeReadString(dr, 2);
                            var volunteeremail = SafeReadString(dr, 3);
                            var userid = SafeReadInt(dr, 4);
                            var name = SafeReadString(dr, 5);
                            var adress = SafeReadString(dr, 6);
                            var location = SafeReadString(dr, 7);
                            var phonenumber = SafeReadString(dr, 8);
                            var haslicense = SafeReadString(dr, 9);
                            var hascar = SafeReadString(dr, 10);
                            var unsubscribedate = SafeReadDateTime(dr, 11);
                            var trash1 = SafeReadInt(dr, 12);
                            var volunteerid = SafeReadInt(dr, 13);
                            var dateofbirth = SafeReadDateTime(dr, 14);
                            var pathtophoto = SafeReadString(dr, 15);
                            var pathtovog = SafeReadString(dr, 16);
                            var thrash2 = SafeReadInt(dr, 17);
                            // Create
                            returnvolunteer = new Volunteer(accountid, username, password, volunteeremail, userid, name, adress, location, phonenumber, haslicense, hascar, unsubscribedate, volunteerid, dateofbirth, pathtophoto, pathtovog);
                        }
                        return returnvolunteer;
                    }
                }
                someid = GetAdminIdByEmail(email);
                if (someid != -1)
                {
                    Connect();
                    Admin returnadmin = null;
                    using (cmd = new OracleCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT A.ID, A.GEBRUIKERSNAAM, A.WACHTWOORD, A.EMAIL, B.ID FROM TACCOUNT A, TBEHEERDER B WHERE A.ID = B.ACCOUNTID AND LOWER(A.EMAIL) = LOWER(:email)";
                        cmd.Parameters.Add("email", email);
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            var accountid = SafeReadInt(dr, 0);
                            var username = SafeReadString(dr, 1);
                            var password = SafeReadString(dr, 2);
                            var adminemail = SafeReadString(dr, 3);
                            var adminid = SafeReadInt(dr, 4);
                            // Create
                            returnadmin = new Admin(accountid, username, password, adminemail, adminid);
                        }
                        return returnadmin;
                    }
                }
                else // Nothing found with that email
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                Disconnect();
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
                cmd.Parameters.Add("NewAuteur", OracleDbType.Int32).Value = newquestion.AuthorID;
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

        public List<Question> getquestions()
        {
            List<Question> allquestions = new List<Question>();
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM THULPVRAAG H LEFT OUTER JOIN TVERVOER V ON H.VERVOERTYPE = V.ID";
                cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var questionid = SafeReadInt(dr, 0);
                    var description = SafeReadString(dr, 1);
                    var location = SafeReadString(dr, 2);
                    var traveltime = SafeReadString(dr, 3);
                    var startdate = SafeReadDateTime(dr, 4);
                    var enddate = SafeReadDateTime(dr, 5);
                    var critical = SafeReadString(dr, 6);
                    var volunteers = SafeReadInt(dr, 7);
                    var clientid = SafeReadInt(dr, 8);
                    var trash = SafeReadInt(dr, 9);
                    var transportid = SafeReadInt(dr, 10);
                    var transportdescription = SafeReadString(dr, 11);

                    allquestions.Add(new Question(questionid, description, location, traveltime, startdate, enddate, critical, volunteers, clientid, transportid, transportdescription));
                }

                return allquestions;
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
                this.cmd = new OracleCommand();
                this.cmd.Connection = con;
                this.cmd.CommandText = "UPDATE TGEBRUIKER SET uitschrijvingsdatum=':newUnsubscribedDate'where id="+usertodelete.UserID+"";
                this.cmd.CommandType = CommandType.Text;
                dr = this.cmd.ExecuteReader();
                cmd.Parameters.Add("newUnsubscribedDate", usertodelete.UnsubscribedDate);

                this.cmd = new OracleCommand();
                this.cmd.Connection = con;
                this.cmd.CommandText = "DELETE FROM TAFSPRAAK WHERE auteur='" + usertodelete.UserID + "'";
                this.cmd.CommandType = CommandType.Text;
                dr = this.cmd.ExecuteReader();
            

                this.cmd = new OracleCommand();
                this.cmd.Connection = con;
                this.cmd.CommandText = "DELETE FROM TREVIEW WHERE auteur='" + usertodelete.UserID + "'";
                this.cmd.CommandType = CommandType.Text;
                dr = this.cmd.ExecuteReader();

                if (usertodelete is Client)
                {
                    this.cmd = new OracleCommand();
                    this.cmd.Connection = con;
                    this.cmd.CommandText = "DELETE FROM THULPVRAAG WHERE auteur='" + usertodelete.UserID + "'";
                    this.cmd.CommandType = CommandType.Text;
                    dr = this.cmd.ExecuteReader();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;

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

                    chatmessages.Add(new Chat(id, bericht, tijdstip, client, volunteer));
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


                    returnlist.Add(new Review(id, beoordeling, opmerkingen, vrijwilligerid, hulpvraagid));

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
                cmd.CommandText = "SELECT ID, Opmerkingen,beoordeling, FROM TREVIEW WHERE CLIENT ='" + client + "'";
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
