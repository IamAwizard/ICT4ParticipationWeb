<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Project.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Foundation | Welcome</title>
    <link rel="stylesheet" href="css/foundation.css" />
    <link rel="stylesheet" href="css/app.css" />
</head>
<body>
    <form id="FormRegister" runat="server">
        <div class="title-bar">
            <span class="title-bar-title">ICT4Partipation</span>
            <span class="title-bar-right">
                <asp:HyperLink ID="link_Register" href="Login.aspx" runat="server">Inloggen</asp:HyperLink></span>
        </div>
        <br />
        <div class="row">
            <div class="large-4 large-centered columns callout">
                <h4 class="text-center">Registreren</h4>
                
                <div class="text-center">
                    <div class="large-12 large-centered column">
                        <h6>Registreren als:</h6>
                <asp:RadioButton ID="rbtn_Client" runat="server" GroupName="AccountType" AutoPostBack="True" OnCheckedChanged="rb_Client_CheckedChanged" Style="display: none" />
                        <asp:Label ID="lbl_Client" runat="server" Text="Hulpbehoevende" AssociatedControlID="rbtn_Client" CssClass="expanded secondary button"></asp:Label>
                        <asp:RadioButton ID="rbtn_Volunteer" runat="server" GroupName="AccountType" AutoPostBack="True" OnCheckedChanged="rb_Volunteer_CheckedChanged" Style="display: none" />
                        <asp:Label ID="lbl_Volunteer" runat="server" Text="Vrijwilliger" AssociatedControlID="rbtn_Volunteer" CssClass="expanded secondary button"></asp:Label>
                    </div>
                </div>

                <div id="div_UserInformation" runat="server">
                    <asp:Label ID="lbl_RegisteringAs" runat="server" Text="onbekend"></asp:Label>
                    <asp:Label ID="lbl_UsernameError" runat="server" Text="Gebruikersnaam te kort of te lang" Visible="false" CssClass="warning label"></asp:Label>
                    <asp:TextBox ID="tbox_Username" runat="server" placeholder="Gebruikersnaam" required="required"></asp:TextBox>

                    <asp:Label ID="lbl_PasswordError" runat="server" Text="Wachtwoorden komen niet overeen of zijn te kort/lang" Visible="False" CssClass="warning label"></asp:Label>
                    <asp:TextBox ID="tbox_Password" runat="server" placeholder="Wachtwoord" TextMode="Password" required="required"></asp:TextBox>
                    <asp:TextBox ID="tbox_PasswordConfirm" runat="server" placeholder="Wachtwoord Herhalen" TextMode="Password" required="required"></asp:TextBox>

                    <asp:Label ID="lbl_EmailError" runat="server" Text="Ongeldige Email opgegeven of te lang" Visible="False" CssClass="warning label"></asp:Label>
                    <asp:TextBox ID="tbox_Email" runat="server" placeholder="Email" required="required"></asp:TextBox>

                    <asp:Label ID="lbl_GivenNameError" runat="server" Text="Naam te kort of te lang" Visible="False" CssClass="warning label"></asp:Label>
                    <asp:TextBox ID="tbox_GivenName" runat="server" placeholder="Naam" required="required"></asp:TextBox>

                    <asp:Label ID="lbl_AdressError" runat="server" Text="Adres te kort of the lang" Visible="False" CssClass="warning label"></asp:Label>
                    <asp:TextBox ID="tbox_Adress" runat="server" placeholder="Adres" required="required"></asp:TextBox>

                    <asp:Label ID="lbl_LocationError" runat="server" Text="Woonplaats te kort of te lang" Visible="False" CssClass="warning label"></asp:Label>
                    <asp:TextBox ID="tbox_Location" runat="server" placeholder="Woonplaats" required="required"></asp:TextBox>

                    <asp:Label ID="lbl_PhoneNumerError" runat="server" Text="Telefoonnummer bestaat niet uit 10 tekens" Visible="False" CssClass="warning label"></asp:Label>
                    <asp:TextBox ID="tbox_PhoneNumber" runat="server" placeholder="Telefoonnummer (10 tekens)" required="required" MaxLength="10"></asp:TextBox>

                    <ul class="no-bullet">
                        <li>
                            <asp:CheckBox ID="cbox_HasLicense" runat="server" />
                            <asp:Label ID="lbl__HasLicense" runat="server" Text="In bezit van rijbewijs" AssociatedControlID="cbox_HasLicense"></asp:Label></li>
                        <li>
                            <asp:CheckBox ID="cbox_HasCar" runat="server" />
                            <asp:Label ID="lbl_HasCar" runat="server" Text="In bezit van auto" AssociatedControlID="cbox_HasCar"></asp:Label>
                        </li>
                    </ul>
                </div>
                <div id="div_Register_Client" runat="server">
                    <asp:CheckBox ID="cbox_OVPossible" runat="server" />
                    <asp:Label ID="lbl_OVPossible" runat="server" Text="Reizen met ov mogelijk" AssociatedControlID="cbox_OVPossible"></asp:Label>
                    <hr />
                    <asp:Button ID="btn_Register_Client" runat="server" Text="Registeren" class="expanded success button" OnClick="btn_Register_Client_Click" />
                </div>
                <div id="div_Register_Volunteer" runat="server">
                    <asp:Label ID="lbl_BirthDate" runat="server" Text="Geboortedatum:"></asp:Label>
                    <asp:Calendar ID="calendar_BirthDate" runat="server" Style="margin-left: auto; margin-right: auto;">
                        <TodayDayStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                    </asp:Calendar>
                    <asp:Label ID="lbl_UploadPhoto" runat="server" Text="Profielfoto uploaden" AssociatedControlID="FU_UploadFoto"></asp:Label>
                    <asp:FileUpload ID="FU_UploadFoto" runat="server" CssClass="" />
                    <br />
                    <asp:Label ID="lbl_UploadVog" runat="server" Text="V.O.G. Uploaden (?)" AssociatedControlID="FU_UploadVOG" ToolTip="Verklaring Omtrent Gedrag"></asp:Label>
                    <asp:FileUpload ID="FU_UploadVOG" runat="server" CssClass="" />
                    <hr />
                    <asp:Button ID="btn_Register_Volunteer" runat="server" Text="Registreren" class="expanded success button" OnClick="btn_Register_Volunteer_Click" />
                </div>
            </div>
        </div>

        <script src="js/vendor/jquery.min.js"></script>
        <script src="js/vendor/what-input.min.js"></script>
        <script src="js/foundation.min.js"></script>
        <script src="js/app.js"></script>
    </form>
</body>
</html>
