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
    <div data-sticky-container="">
        <div class="sticky" data-sticky="" data-margin-top="0" style="width: 100%; z-index: 100">
            <div class="title-bar" data-responsive-toggle="main-menu" data-hide-for="medium">
                <button class="menu-icon" type="button" data-toggle="data-toggle"></button>
                <div class="title-bar-title">Menu</div>
            </div>
            <div class="top-bar" id="main-menu">
                <div class="top-bar-left">
                    <ul class="dropdown menu" data-dropdown-menu="data-dropdown-menu">
                        <li class="menu-text">ICT 4 Participation</li>
                    </ul>
                </div>
                <div class="top-bar-right">
                    <ul class="menu vertical medium-horizontal" data-responsive-menu="drilldown medium-dropdown">
                        <li>
                            <asp:HyperLink ID="link_Login" runat="server" NavigateUrl="~/Login.aspx">Inloggen</asp:HyperLink></li>
                        <li>
                            <asp:HyperLink ID="link_Register" runat="server" NavigateUrl="~/Register.aspx">Registreren</asp:HyperLink></li>
                    </ul>
                </div>
            </div>
        </div>
        <br />
        <br />
        <br />
        <div class="row">
            <div class="large-4 large-centered columns callout">
                <form id="FormLogin" runat="server">
                    <h4 class="text-center">U wordt uitgelogd.</h4>
                    <asp:Label ID="lbl_Error" runat="server" Text="Er is niemand om uit te loggen!" Visible="False" CssClass="alert button expanded label"></asp:Label>
                </form>
            </div>
        </div>
    </div>
    <script src="js/vendor/jquery.min.js"></script>
    <script src="js/vendor/what-input.min.js"></script>
    <script src="js/foundation.min.js"></script>
    <script src="js/app.js"></script>
</body>
</html>
