<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="Project.Logout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Uitloggen - ICT4particpation</title>
    <link rel="stylesheet" href="css/foundation.css" />
    <link rel="stylesheet" href="css/app.css" />
</head>
<body>
    <div class="title-bar">
        <span class="title-bar-title"><a href="Login.aspx" style="color:white">ICT4Partipation</a></span>
        <span class="title-bar-right">
            <asp:HyperLink ID="link_Register" href="Register.aspx" runat="server">Registreren</asp:HyperLink></span>
    </div>
    <br />
    <div class="row">
        <div class="large-4 large-centered columns callout">
            <form id="FormLogin" runat="server">
                    <h4 class="text-center">U wordt uitgelogd.</h4>
                <asp:Label ID="lbl_Error" runat="server" Text="Er is niemand om uit te loggen!" Visible="False" CssClass="warning button expanded label"></asp:Label>
            </form>
        </div>
    </div>
    <script src="js/vendor/jquery.min.js"></script>
    <script src="js/vendor/what-input.min.js"></script>
    <script src="js/foundation.min.js"></script>
    <script src="js/app.js"></script>
</body>
</html>
