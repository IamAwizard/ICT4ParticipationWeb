<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Uitschrijven.aspx.cs" Inherits="Project.Uitschrijven" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="../CSS/foundation.css" />
    <link rel="stylesheet" href="../CSS/app.css" />
    <title>UITSCHRIJVEN - ICT 4 Participation</title>
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
                            <asp:HyperLink ID="link_Logout" runat="server" NavigateUrl="~/Logout.aspx">Uitloggen</asp:HyperLink></li>
                    </ul>
                </div>
            </div>
        </div>
        <br />
        <br />
        <br />
        <form id="form1" runat="server">
            <div class="row">
                <div class="large-6 large-centered column callout">
                    <div class="row">
                        <div class="large-12 column">
                            <h2 class="text-center">Uitschrijven?</h2>
                            <p>Je account wordt uitgeschakeld en je kunt de applicatie niet meer gebruiken!</p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="large-6 columns">
                            <asp:Button ID="btn_Cancel" runat="server" Text="Haal me hier vandaan!" CssClass="expanded button" OnClick="btn_Cancel_Click" />
                        </div>
                        <div class="large-6 columns">
                            <asp:Button ID="btn_Ok" runat="server" Text="Ja, Uitschrijven" CssClass="expanded alert button" OnClick="btn_Ok_Click" />
                            <br />
                            <asp:Label ID="lbl_ErrorMsg" runat="server" ForeColor="Red" Text="Het is niet gelukt om uw account te verwijderen!" Visible="False"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
    <script src="../JS/vendor/jquery.min.js"></script>
    <script src="../JS/vendor/what-input.min.js"></script>
    <script src="../JS/foundation.min.js"></script>
    <script src="../JS/app.js"></script>
</body>
</html>
