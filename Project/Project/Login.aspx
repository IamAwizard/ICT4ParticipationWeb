<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Project.WebForm2" %>

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
<div class="title-bar">
        <span class="title-bar-title">ICT4Partipation</span>
        <span class="title-bar-right">
            <asp:HyperLink ID="link_Register" href="Register.aspx" runat="server">Registreren</asp:HyperLink></span>
    </div>
    <br />
    <div class="row">
        <div class="medium-6 medium-centered large-4 large-centered columns callout">
            <form id="form1" runat="server">
                <div class="row column log-in-form">
                    <h4 class="text-center">Log in met uw e-mailadres</h4>
                    <label for="tbox_Email">
                        E-mail
          <asp:TextBox ID="tbox_Email" runat="server" placeholder="somebody@example.com"></asp:TextBox>
                    </label>
                    <label for="tbox_Password">
                        Wachtwoord
          <asp:TextBox ID="tbox_Password" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
                    </label>
                    <p>
                        <asp:Button ID="btn_Login" runat="server" Text="Inloggen" class="button expanded" />
                    </p>
                    <p class="text-center">
                        <asp:HyperLink ID="link_ForgotPassword" runat="server">Wachtwoord vergeten?</asp:HyperLink>
                    </p>
                </div>

            </form>
        </div>
    </div>
    <script src="js/vendor/jquery.min.js"></script>
    <script src="js/vendor/what-input.min.js"></script>
    <script src="js/foundation.min.js"></script>
    <script src="js/app.js"></script>
</body>
</html>
