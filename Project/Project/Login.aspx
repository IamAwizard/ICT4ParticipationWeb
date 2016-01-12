<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Project.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Inloggen - ICT4particpation</title>
    <link rel="stylesheet" href="css/foundation.css" />
    <link rel="stylesheet" href="css/app.css" />
</head>
<body>
    <div class="title-bar">
        <span class="title-bar-title"><a href="Login.aspx" style="color:white">ICT4Partipation</a></span>
        <span class="title-bar-right">
            <asp:HyperLink ID="link_Register" runat="server" NavigateUrl="Register.aspx">Registreren</asp:HyperLink></span>
    </div>
    <br />
    <div class="row">
        <div class="large-4 large-centered columns callout">
            <form id="FormLogin" runat="server">
                <div class="row column log-in-form">
                    <h4 class="text-center">Log in met uw e-mailadres</h4>
                    <label for="tbox_Email">
                        E-mail
                         </label>
          <asp:TextBox ID="tbox_Email" runat="server" placeholder="somebody@example.com" required="required"></asp:TextBox>
                   
                    <label for="tbox_Password">
                        Wachtwoord
                        </label>
          <asp:TextBox ID="tbox_Password" runat="server" placeholder="Password" TextMode="Password" required="required"></asp:TextBox>
                    <asp:Label ID="lbl_LoginError" runat="server" Text="Combinatie email en wachtwoord niet gevonden!" Visible="False" CssClass="alert expanded button label"></asp:Label>
                    <p>
                        <asp:Button ID="btn_Login" runat="server" Text="Inloggen" class="button expanded" OnClick="btn_Login_Click" />
                    </p>
                    <p class="text-center">
                        <asp:HyperLink ID="link_ForgotPassword" runat="server">Wachtwoord vergeten?</asp:HyperLink>
                    </p>
                </div>
                  <ul>
                   
           
                      
                   </ul>
            </form>
        </div>
    </div>
    <script src="js/vendor/jquery.min.js"></script>
    <script src="js/vendor/what-input.min.js"></script>
    <script src="js/foundation.min.js"></script>
    <script src="js/app.js"></script>
</body>
</html>
