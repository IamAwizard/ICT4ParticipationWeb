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
                <asp:TextBox ID="tb_GebruikersNaam" runat="server" placeholder="GebruikersNaam" ></asp:TextBox>
                <asp:TextBox ID="tb_Wachtwoord" runat="server" placeholder="Wachtwoord" TextMode="Password"></asp:TextBox>
                <asp:TextBox ID="tb_Email" runat="server" placeholder="Email"></asp:TextBox>
                <asp:TextBox ID="tb_Naam" runat="server" placeholder="Naam"></asp:TextBox>
                <asp:TextBox ID="tb_Adres" runat="server" placeholder="Adres"></asp:TextBox>
                <asp:TextBox ID="tb_Woonplaats" runat="server" placeholder="Woonplaats"></asp:TextBox>
                <asp:TextBox ID="tb_Telefoonnummer" runat="server" placeholder="Telefoonnummer"></asp:TextBox>
                
               <ul>
                   
            <li  class="no-bullet"> <asp:CheckBox ID="cb_HeeftRijbewijs" runat="server"  /><asp:Label ID="Label2" runat="server" Text="Heeft Rijbewijs"></asp:Label></li>    
           <li  class="no-bullet">  <asp:CheckBox ID="cb_HeeftAuto" runat="server"  />     <asp:Label ID="Label3" runat="server" Text="Heeft Auto" ></asp:Label>  </li>
                      
                   </ul>
             
                <asp:Calendar ID="c_Uitschrijvingsdatum" runat="server"></asp:Calendar>
                <asp:Button ID="btn_Register" runat="server" Text="Register" class="button expanded" />
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
