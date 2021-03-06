﻿using System;
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
        private string connectionstring = "User Id=Participation;Password=Participation;Data Source=localhost:1521";
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
        /// Gets a client name by ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string GetClientUserName(int ID)
        {
            string gebruikersnaam = "";
            try
            {

                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT G.naam FROM TGEBRUIKER G,THULPBEHOEVENDE H WHERE H.GEBRUIKERID = G.ID AND H.ID=" + ID + "";
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    gebruikersnaam = dr.GetString(0);

                }
                return gebruikersnaam;

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
                            returnclient = new Client(accountid, username, password, clientemail, userid, name, adress, location, phonenumber, haslicense, hascar, clientid, ovpossible, unsubscribedate);
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

        /// <summary>
        /// Gets a client by ID
        /// </summary>
        /// <param name="clientid"></param>
        /// <returns></returns>
        public Client GetClientByID(int clientid)
        {
            try
            {
                Connect();
                Client returnclient = null;
                using (cmd = new OracleCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM TACCOUNT A, TGEBRUIKER G, THULPBEHOEVENDE H WHERE A.ID = G.ACCOUNTID AND H.GEBRUIKERID = G.ID AND H.ID = :clientid";
                    cmd.Parameters.Add("clientid", clientid);
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
                        var id = SafeReadInt(dr, 13);
                        var ovpossible = SafeReadString(dr, 14);
                        var thrash2 = SafeReadInt(dr, 15);
                        // Create
                        returnclient = new Client(accountid, username, password, clientemail, userid, name, adress, location, phonenumber, haslicense, hascar, id, ovpossible, unsubscribedate);
                    }
                    return returnclient;
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


        /// <summary>
        /// Gets a volunteer by ID
        /// </summary>
        /// <param name="clientid"></param>
        /// <returns></returns>
        public Volunteer GetVolunteerByID(int volunteerid)
        {
            try
            {
                Connect();
                Volunteer returnvolunteer = null;
                using (cmd = new OracleCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM TACCOUNT A, TGEBRUIKER G, TVRIJWILLIGER V WHERE A.ID = G.ACCOUNTID AND G.ID = V.GEBRUIKERID AND V.ID = :volunteerid";
                    cmd.Parameters.Add("volunteerid", volunteerid);
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
                        var id = SafeReadInt(dr, 13);
                        var dateofbirth = SafeReadDateTime(dr, 14);
                        var pathtophoto = SafeReadString(dr, 15);
                        var pathtovog = SafeReadString(dr, 16);
                        var thrash2 = SafeReadInt(dr, 17);
                        // Create
                        returnvolunteer = new Volunteer(accountid, username, password, volunteeremail, userid, name, adress, location, phonenumber, haslicense, hascar, unsubscribedate, id, dateofbirth, pathtophoto, pathtovog);
                    }
                    return returnvolunteer;
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

        /// <summary>
        /// Gets a question from the database
        /// </summary>
        /// <param name="questionid">question id of question to get</param>
        /// <returns>question if found otherwise null</returns>
        public Question GetQuestionByID(int questionid)
        {
            try
            {
                Connect();
                using (cmd = new OracleCommand())
                {
                    Question returnvalue = null;
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from THULPVRAAG H LEFT OUTER JOIN TVERVOER V ON H.VERVOERTYPE = V.ID WHERE H.ID = :questionID";
                    cmd.Parameters.Add("questionID", questionid);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var id = SafeReadInt(dr, 0);
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

                        returnvalue = new Question(id, description, location, traveltime, startdate, enddate, critical, volunteers, clientid, transportid, transportdescription);
                    }
                    return returnvalue;
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

        /// <summary>
        /// Fills the AcceptedBy property of a question
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public Question ExpandQuestionWithVolunteers(Question q)
        {
            try
            {
                Connect();
                Volunteer returnvolunteer = null;
                using (cmd = new OracleCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM TACCOUNT A, TGEBRUIKER G, TVRIJWILLIGER V, THULPVRAAG_VRIJWILLIGER HV, THULPVRAAG H WHERE A.ID = G.ACCOUNTID AND G.ID = V.GEBRUIKERID AND V.ID = HV.VRIJWILLIGERID AND HV.HULPVRAAGID = H.ID AND H.ID = :questionid";
                    cmd.Parameters.Add("questionid", q.ID);
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
                        q.AcceptedBy.Add(returnvolunteer);
                    }
                    return q;
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

        /// <summary>
        /// Adds a volunteer to a question in the database
        /// </summary>
        /// <param name="q"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public bool AnswerQuestion(Question q, Volunteer v)
        {
            try
            {
                Connect();
                using (cmd = new OracleCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO THULPVRAAG_VRIJWILLIGER(HULPVRAAGID, VRIJWILLIGERID) VALUES(:questionid, :volunteerid)";
                    cmd.Parameters.Add("questionid", q.ID);
                    cmd.Parameters.Add("volunteerid", v.VolunteerID);
                    cmd.ExecuteNonQuery();

                    return true;
                }
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
        /// Get all types of transport in the database
        /// </summary>
        /// <returns></returns>
        public List<Transport> GetTransports()
        {
            List<Transport> transports = new List<Transport>();
            try
            {
                Connect();
                using (cmd = new OracleCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select ID,Omschrijving from TVervoer";
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        int ID = SafeReadInt(dr, 0);
                        var description = SafeReadString(dr, 1);
                        transports.Add(new Transport(ID, description));
                    }
                    return transports;
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

        public int GetSingleTransport(string description)
        {
            int ID = 0;
            try
            {
                Connect();
                using (cmd = new OracleCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select ID from TVervoer where omschrijving='" + description + "'";
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ID = SafeReadInt(dr, 0);
                    }
                    return ID;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return 0;
            }
            finally
            {
                Disconnect();
            }
        }

        /// <summary>
        /// Gets all users from the database
        /// </summary>
        /// <returns></returns>
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
            finally
            {
                Disconnect();
            }
        }

        /// <summary>
        /// Adds a complete question to the database
        /// </summary>
        /// <param name="newquestion"></param>
        /// <returns></returns>
        public bool AddQuestion(Question newquestion)
        {
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                    "Insert into THULPVRAAG(OMSCHRIJVING, LOCATIE, REISTIJD, STARTDATUM, EINDDATUM, URGENT, AANTALVRIJWILLIGERS, VERVOERTYPE, AUTEUR) VALUES (:NewOMSCHRIJVING, :NewLOCATIE, :NewREISTIJD, :NewSTARTDATUM, :NewEINDDATUM, :NewURGENT, :NewAANTALVRIJWILLIGERS, :NewVERVOERTYPE, :NewAUTEUR)";

                cmd.Parameters.Add("NewOMSCHRIJVING", OracleDbType.Varchar2).Value = newquestion.Description;
                cmd.Parameters.Add("NewLOCATIE", OracleDbType.Varchar2).Value = newquestion.Location;
                cmd.Parameters.Add("NewREISTIJD", OracleDbType.Varchar2).Value = newquestion.TravelTime;
                cmd.Parameters.Add("NewSTARTDATUM", OracleDbType.Date).Value = newquestion.DateBegin;
                cmd.Parameters.Add("NewEINDDATUM", OracleDbType.Date).Value = newquestion.DateEnd;
                cmd.Parameters.Add("NewURGENT", OracleDbType.Varchar2).Value = newquestion.Critical;
                cmd.Parameters.Add("NewAANTALVRIJWILLIGERS", OracleDbType.Varchar2).Value = newquestion.VolunteersNeeded;
                cmd.Parameters.Add("NewVERVOERTYPE", OracleDbType.Varchar2).Value = newquestion.Transport;
                cmd.Parameters.Add("NewAUTEUR", newquestion.AuthorID);

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

        public bool AddMeeting(Meeting meeting)
        {
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                    "Insert into TAFSPRAAK(hulpbehoevendeid, vrijwilligerid, datum, locatie) VALUES (:Newhulpbehoevendeid, :Newvrijwilligerid, :Newdatum, :Newlocatie)";

                cmd.Parameters.Add("Newhulpbehoevendeid", OracleDbType.Int32).Value = meeting.Client.ClientID;
                cmd.Parameters.Add("Newvrijwilligerid", OracleDbType.Int32).Value = meeting.Volunteer.VolunteerID;
                cmd.Parameters.Add("Newdatum", OracleDbType.Date).Value = meeting.Date;
                cmd.Parameters.Add("Newlocatie", OracleDbType.Varchar2).Value = meeting.Location;

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

        /// <summary>
        /// Adds a basic new question to the DB without any extra information
        /// </summary>
        /// <param name="newquestion"></param>
        /// <returns></returns>
        public bool AddNewQuestion(Question newquestion)
        {
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "Insert into THULPVRAAG(OMSCHRIJVING,auteur,startdatum,aantalvrijwilligers) VALUES (:NewOMSCHRIJVING,:NewAuteur,:NewStartdatum,:NewAantalvrijwilligers)";

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

        /// <summary>
        /// Updates a question
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public bool UpdateQuestion(Question question)
        {
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                   "UPDATE THULPVRAAG SET OMSCHRIJVING = :NewOMSCHRIJVING, LOCATIE = :NewLOCATIE, REISTIJD = :NewREISTIJD, URGENT = :newUrgent, AANTALVRIJWILLIGERS = :newAantalvrijwilligers, VERVOERTYPE = :newVervoertype WHERE ID = :newIDvalue";
                cmd.Parameters.Add("NewOMSCHRIJVING", question.Description);
                cmd.Parameters.Add("NewLOCATIE", question.Location);
                cmd.Parameters.Add("NewREISTIJD", question.TravelTime);
                cmd.Parameters.Add("newUrgent", question.Critical.ToString());
                cmd.Parameters.Add("newAantalvrijwilligers", question.VolunteersNeeded);
                cmd.Parameters.Add("newVervoertype", question.Transport.ID);
                cmd.Parameters.Add("newIDvalue", question.ID);
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

        /// <summary>
        /// Gets all questions which are not ended yet
        /// Ordered by date created (oldest first)
        /// </summary>
        /// <returns></returns>
        public List<Question> GetAllOpenQuestions()
        {
            List<Question> allquestions = new List<Question>();
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM THULPVRAAG H LEFT OUTER JOIN TVERVOER V ON H.VERVOERTYPE = V.ID WHERE H.EINDDATUM IS NULL ORDER BY STARTDATUM ASC";
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

        /// <summary>
        /// Gets a question from the database by authorid and description
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public Question GetSingleQuestion(int userid, string description)
        {
            Question question = null;
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM THULPVRAAG H LEFT OUTER JOIN TVERVOER V ON H.VERVOERTYPE = V.ID WHERE H.omschrijving :description AND H.auteur= :userid";
                cmd.Parameters.Add("description", description);
                cmd.Parameters.Add("userid", userid);
                cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var questionid = SafeReadInt(dr, 0);
                    var qdescription = SafeReadString(dr, 1);
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


                    question = new Question(questionid, qdescription, location, traveltime, startdate, enddate, critical, volunteers, clientid, transportid, transportdescription);
                }

                return question;
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

        /// <summary>
        /// Adds a review to the database
        /// </summary>
        /// <param name="newreview"></param>
        /// <returns></returns>
        public bool AddReview(Review newreview)
        {
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                    "Insert into TREVIEW(BEOORDELING, OPMERKINGEN, VRIJWILLIGERID, HULPVRAAGID) VALUES (:NewRating, :NewComments, :NewVolunteer, :NewQuestion)";
                cmd.Parameters.Add("NewRating", newreview.Rating);
                cmd.Parameters.Add("NewComments", newreview.Comments);
                cmd.Parameters.Add("NewVolunteer", newreview.VolunteerID);
                cmd.Parameters.Add("NewQuestion", newreview.QuestionID);

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

        /// <summary>
        /// Deletes a user from the database
        /// </summary>
        /// <param name="usertodelete"></param>
        /// <returns></returns>
        public bool DeleteAccount(Account accounttodelete)
        {
            try
            {
                User foo;
                if (accounttodelete is User)
                {
                    foo = accounttodelete as User;
                    Connect();

                    cmd = new OracleCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE TGEBRUIKER SET UITSCHRIJVINGSDATUM = :newUnsubscribedDate where id= :newUserID";
                    cmd.Parameters.Add("newUnsubscribedDate", DateTime.Now);
                    cmd.Parameters.Add("newUserID", foo.UserID);
                    cmd.ExecuteNonQuery();

                    if (foo is Client)
                    {
                        cmd = new OracleCommand();
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "DELETE FROM THULPBEHOEVENDE WHERE GEBRUIKERID = :gebruikerid";
                        cmd.Parameters.Add("gebruikerid", foo.UserID);
                        cmd.ExecuteNonQuery();
                        return true;
                    }

                    if (foo is Volunteer)
                    {
                        cmd = new OracleCommand();
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "DELETE FROM TVRIJWILLIGER WHERE GEBRUIKERID = :gebruikerid";
                        cmd.Parameters.Add("gebruikerid", foo.UserID);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
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
        /// Extends a question with additional information about the author
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public Question AddClientToQuestion(Question question)
        {
            try
            {
                Connect();
                Client c = null;
                using (cmd = new OracleCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM TACCOUNT A, TGEBRUIKER G, THULPBEHOEVENDE H WHERE A.ID = G.ACCOUNTID AND H.GEBRUIKERID = G.ID AND H.ID = :clientid";
                    cmd.Parameters.Add("clientid", question.AuthorID);
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
                        var id = SafeReadInt(dr, 13);
                        var ovpossible = SafeReadString(dr, 14);
                        var thrash2 = SafeReadInt(dr, 15);
                        // Create
                        c = new Client(accountid, username, password, clientemail, userid, name, adress, location, phonenumber, haslicense, hascar, id, ovpossible, unsubscribedate);
                        question.Author = c;
                    }
                    return question;
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

        /// <summary>
        /// Gets the meetings of the given user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<Meeting> GetMeetings(User user)
        {
            try
            {
                List<Meeting> meetings = new List<Meeting>();
                Connect();
                if (user is Client)
                {
                    Client actualuser = user as Client;
                    using (cmd = new OracleCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "select * from tafspraak a left join tvrijwilliger v on a.VRIJWILLIGERID = v.ID left join tgebruiker g on v.GEBRUIKERID = g.id left join taccount acc on g.ACCOUNTID = acc.ID where hulpbehoevendeid = :clientid";
                        cmd.Parameters.Add("clientid", actualuser.ClientID);
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            var meetingid = SafeReadInt(dr, 0);
                            var clientid = SafeReadInt(dr, 1);
                            var volunteerid = SafeReadInt(dr, 2);
                            var date = SafeReadDateTime(dr, 3);
                            var location = SafeReadString(dr, 4);
                            var trash = SafeReadInt(dr, 5);
                            var dateofbirth = SafeReadDateTime(dr, 6);
                            var profilephoto = SafeReadString(dr, 7);
                            var vogpath = SafeReadString(dr, 8);
                            var userid = SafeReadInt(dr, 9);
                            var trash2 = SafeReadInt(dr, 10);
                            var givenname = SafeReadString(dr, 11);
                            var address = SafeReadString(dr, 12);
                            var city = SafeReadString(dr, 13);
                            var phonenumber = SafeReadString(dr, 14);
                            var haslicense = SafeReadString(dr, 15);
                            var hascar = SafeReadString(dr, 16);
                            var unsubscribedate = SafeReadDateTime(dr, 17);
                            var accountid = SafeReadInt(dr, 18);
                            var thrash3 = SafeReadInt(dr, 19);
                            var username = SafeReadString(dr, 20);
                            var password = SafeReadString(dr, 21);
                            var email = SafeReadString(dr, 22);

                            Volunteer helper = new Volunteer(accountid, username, password, email, userid, givenname, address, city, phonenumber, haslicense, hascar, unsubscribedate, volunteerid, dateofbirth, profilephoto, vogpath);
                            Meeting toadd = new Meeting(meetingid, actualuser, helper, date , location);
                            meetings.Add(toadd);
                        }
                        return meetings;
                    }
                }
                else
                {
                    Volunteer actualuser = user as Volunteer;
                    using (cmd = new OracleCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "select * from tafspraak a left join thulpbehoevende h on a.HULPBEHOEVENDEID = h.ID left join tgebruiker g on h.GEBRUIKERID = g.id left join taccount acc on g.ACCOUNTID = acc.ID where a.hulpbehoevendeid = :volunteerid";
                        cmd.Parameters.Add("volunteerid", actualuser.VolunteerID);
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            var meetingid = SafeReadInt(dr, 0);
                            var clientid = SafeReadInt(dr, 1);
                            var volunteerid = SafeReadInt(dr, 2);
                            var date = SafeReadDateTime(dr, 3);
                            var location = SafeReadString(dr, 4);
                            var trash = SafeReadInt(dr, 5);
                            var ovpossible = SafeReadString(dr, 6);
                            var userid = SafeReadInt(dr, 7);
                            var trash2 = SafeReadInt(dr, 8);
                            var givenname = SafeReadString(dr, 9);
                            var address = SafeReadString(dr, 10);
                            var city = SafeReadString(dr, 11);
                            var phonenumber = SafeReadString(dr, 12);
                            var haslicense = SafeReadString(dr, 13);
                            var hascar = SafeReadString(dr, 14);
                            var unsubscribedate = SafeReadDateTime(dr, 15);
                            var accountid = SafeReadInt(dr, 16);
                            var thrash3 = SafeReadInt(dr, 17);
                            var username = SafeReadString(dr, 18);
                            var password = SafeReadString(dr, 19);
                            var email = SafeReadString(dr, 20);

                            Client helper = new Client(accountid, username, password, email, userid, givenname, address, city, phonenumber, haslicense, hascar, clientid, ovpossible, unsubscribedate);
                            Meeting toadd = new Meeting(meetingid, helper, actualuser, date, location);
                            meetings.Add(toadd);
                        }
                        return meetings;
                    }
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
                cmd.CommandText = "SELECT * FROM(SELECT BERICHT,tijdstip,VANHULPBEHOEVENDE FROM TCHAT WHERE HULPBEHOEVENDEID = :clientid AND VRIJWILLIGERID = :volunteerid ORDER BY TIJDSTIP DESC) WHERE ROWNUM <= 10 ORDER BY ROWNUM DESC";
                cmd.Parameters.Add("clientid", client.ClientID);
                cmd.Parameters.Add("volunteerid", volunteer.VolunteerID);

               cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var bericht = dr.GetString(0);
                    var tijdstip = dr.GetDateTime(1);
                    var sender = dr.GetInt32(2);
                    chatmessages.Add(new Chat(bericht, tijdstip, sender));
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
                   "DELETE FROM TREVIEW WHERE ID = :newID";
                cmd.Parameters.Add("newID", reviewID);
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
                   "DELETE FROM THULPVRAAG WHERE ID = :NewID";
                cmd.Parameters.Add("NewID", QuestionID);
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

        /// <summary>
        /// Gets all the volunteers
        /// </summary>
        /// <returns></returns>
        public List<Volunteer> GetAllVolunteers()
        {
            List<Volunteer> volunteers = new List<Volunteer>();
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select G.ID,G.naam,G.adres,G.woonplaats,G.telefoonnummer,G.heeftrijbewijs,G.heeftauto,a.GEBRUIKERSNAAM,a.WACHTWOORD,a.EMAIL,V.id from TGEBRUIKER G,Tvrijwilliger V,TACCOUNT A where V.GEBRUIKERID = G.id AND G.accountID = A.ID";
                cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    // Read from DB
                    int id = dr.GetInt32(0);
                    string naam = dr.GetString(1);
                    string adres = dr.GetString(2);
                    string woonplaats = dr.GetString(3);
                    string telefoonnummer = dr.GetString(4);
                    string heeftrijbewijs = dr.GetString(5);
                    string heeftauto = dr.GetString(6);
                    string username = dr.GetString(7);
                    string password = dr.GetString(8);
                    string email = dr.GetString(9);
                    int volunid = dr.GetInt32(10);
                    volunteers.Add(new Volunteer(id, naam, password, email, naam, adres, woonplaats, telefoonnummer, heeftrijbewijs, heeftauto,volunid));
                }
                return volunteers;
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

        /// <summary>
        /// Gets all the clients
        /// </summary>
        /// <returns></returns>
        public List<Client> GetAllClients()
        {
            List<Client> clients = new List<Client>();
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select G.ID, G.naam,G.adres,G.woonplaats,G.telefoonnummer,G.heeftrijbewijs,G.heeftauto,a.GEBRUIKERSNAAM,a.WACHTWOORD,a.EMAIL,H.id from TGEBRUIKER G,THULPBEHOEVENDE H,TACCOUNT A where H.GEBRUIKERID = G.id AND G.accountID = A.ID";
                cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    // Read from DB
                    int id = dr.GetInt32(0);
                    string naam = dr.GetString(1);
                    string adres = dr.GetString(2);
                    string woonplaats = dr.GetString(3);
                    string telefoonnummer = dr.GetString(4);
                    string heeftrijbewijs = dr.GetString(5);
                    string heeftauto = dr.GetString(6);
                    string username = dr.GetString(7);
                    string password = dr.GetString(8);
                    string email = dr.GetString(9);
                    int clientid = dr.GetInt32(10);
                    clients.Add(new Client(id, naam, password, email, naam, adres, woonplaats, telefoonnummer, heeftrijbewijs, heeftauto, clientid));
                }
                return clients;
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

        public bool AddChatmessage(Chat message)
        {
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                    "Insert into TCHAT(TIJDSTIP, BERICHT, HULPBEHOEVENDEID, VRIJWILLIGERID,VANHULPBEHOEVENDE) VALUES (:NewTIJDSTIP, :NewBERICHT, :NewHULPBEHOEVENDEID, :NewVRIJWILLIGERID,:NewVANHULPBEHOEVENDE)";

                cmd.Parameters.Add("NewTIJDSTIP", OracleDbType.Date).Value = DateTime.Now;
                cmd.Parameters.Add("NewBERICHT", OracleDbType.Varchar2).Value = message.Message;
                cmd.Parameters.Add("NewHULPBEHOEVENDEID", OracleDbType.Int32).Value = message.Client.ClientID;
                cmd.Parameters.Add("NewVRIJWILLIGERID", OracleDbType.Int32).Value = message.Volunteer.VolunteerID;
                cmd.Parameters.Add("NewVANHULPBEHOEVENDE", OracleDbType.Int32).Value = message.Sender;
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

        public bool UpdateLicense(int userid, bool yesno)
        {
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText =
                   "UPDATE TGEBRUIKER SET heeftrijbewijs =:NewRijbewijs WHERE id=:UserId";
                cmd.Parameters.Add("NewRijbewijs", yesno.ToString());
                cmd.Parameters.Add("UserId", userid);
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
        public bool SetAvailability(Availability available)
        {

            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "UPDATE TBESCHIKBAARHEID SET DAGDEEL = :newDagDeel WHERE DAGNAAM = :newDagNaam AND VRIJWILLIGERID = :newVrijwilligerid";
                cmd.Parameters.Add("newDagDeel", available.TimeOfDay);
                cmd.Parameters.Add("newDagNaam", available.Day);
                cmd.Parameters.Add("newVrijwilligerid", available.volunid);
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

        public List<Availability> GetAvailability(int volunID)
        {
            List<Availability> available = new List<Availability>();
            try
            {
                Connect();
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM TBESCHIKBAARHEID WHERE vrijwilligerID=" + volunID + "";
                cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var ID = dr.GetInt32(0);
                    var dayname = dr.GetString(1);
                    var daypart = dr.GetString(2);
                    var volunid = dr.GetInt32(3);

                    available.Add(new Availability(ID, dayname, daypart, volunid));
                }
                return available;
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
