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
            <div class="medium-6 medium-centered large-4 large-centered columns callout">
                <asp:TextBox ID="tb_GebruikersNaam" runat="server" placeholder="GebruikersNaam"></asp:TextBox>
                <asp:TextBox ID="tb_Wachtwoord" runat="server" placeholder="Wachtwoord" TextMode="Password"></asp:TextBox>
                <asp:TextBox ID="tb_Email" runat="server" placeholder="Email"></asp:TextBox>
                <asp:TextBox ID="tb_Naam" runat="server" placeholder="Naam"></asp:TextBox>
                <asp:TextBox ID="tb_Adres" runat="server" placeholder="Adres"></asp:TextBox>
                <asp:TextBox ID="tb_Woonplaats" runat="server" placeholder="Woonplaats"></asp:TextBox>
                <asp:TextBox ID="tb_Telefoonnummer" runat="server" placeholder="Telefoonnummer"></asp:TextBox>

                <ul>

                    <li class="no-bullet">
                        <asp:CheckBox ID="cb_HeeftRijbewijs" runat="server" /><asp:Label ID="Label2" runat="server" Text="Heeft Rijbewijs"></asp:Label></li>
                    <li class="no-bullet">
                        <asp:CheckBox ID="cb_HeeftAuto" runat="server" />
                        <asp:Label ID="Label3" runat="server" Text="Heeft Auto"></asp:Label>
                    </li>

                </ul>




                <asp:RadioButton ID="rb_Volunteer" runat="server" GroupName="typeklant" OnCheckedChanged="rb_Volunteer_CheckedChanged" AutoPostBack="true" />
                <asp:Label ID="lbl_Volunteer" runat="server" Text="Vrijwilliger"></asp:Label>
                <br />
                <asp:RadioButton ID="rb_Client" runat="server" GroupName="typeklant" AutoPostBack="true" OnCheckedChanged="rb_Client_CheckedChanged" />
                <asp:Label ID="lbl_Client" runat="server" Text="Hulpbehoevende"></asp:Label>
                <asp:Button ID="btn_Register" runat="server" Text="Register" class="button expanded" />
            </div>
        </div>
        <div class="row" id="dikkeLAMBORGINIFERRARIPENINI" runat="server">
            <div class="medium-6 medium-centered large-4 large-centered columns callout">

                <div class="text-center">
                    <asp:Label ID="lbl_Hulpbehoevende" runat="server" Text="Hulpbehoevende" CssClass="medium-text-center" Font-Size="X-Large" Font-Bold="true">
                    </asp:Label>
                    <br />
                    <br />
                    <br />

                    <asp:Label ID="lbl_OVMogelijk" runat="server" Text="OVMogelijk"></asp:Label>
                    <asp:CheckBox ID="cb_OVMogelijk" runat="server" />
                </div>
            </div>
        </div>

        <div class="row" id="dikkebmw" runat="server">
            <div class="medium-6 medium-centered large-4 large-centered columns callout">

                <div class="text-center">
                    <div class="medium-centered">
                        <asp:Label ID="lbl_Vrijwilliger" runat="server" Text="Vrijwilliger" CssClass="medium-text-center" Font-Size="X-Large" Font-Bold="true">
                        </asp:Label>
                        <br />
                        <br />
                        <br />
                        <asp:Label ID="lbl_BirthDate" runat="server" Text="GeboorteDatum"></asp:Label>
                        <asp:Calendar ID="c_BirthDate" runat="server"></asp:Calendar>
                        <asp:FileUpload ID="FU_UploadFoto" runat="server" CssClass="button expanded"  />
                            <asp:Button ID="btn_UploadFoto" runat="server" Text="Upload foto" Class="button expanded" OnClick="btn_UploadFoto_Click" />
                            <br />
                            <asp:Button ID="btn_UploadVOG" runat="server" Text="Upload VOG" Class="button expanded" />
              


                    </div>
                </div>
            </div>
        </div>

        <br />
        <script src="js/vendor/jquery.min.js"></script>
        <script src="js/vendor/what-input.min.js"></script>
        <script src="js/foundation.min.js"></script>
        <script src="js/app.js"></script>
    </form>
</body>
</html>
