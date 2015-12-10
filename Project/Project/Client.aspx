<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Client.aspx.cs" Inherits="Project.Client" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Client | Manager</title>
    <link rel="stylesheet" href="css/foundation.css" />
    <link rel="stylesheet" href="css/app.css" />
</head>
    
<body>
    <br />
    <div class="row">
        <div class="large-12 column">
            <h2 class="callout">Welkom:</h2>
        </div>
        </div>
    <form id="form1" runat="server">
        <div class="row">
            
            <div class="large-9  columns">
                
                <p class="callout"> <asp:ListBox ID="lbox_MijnVragen" runat="server" placeholder="Mijn Vragen" Rows="4" Height="100%" >
                    <asp:ListItem>zdfvzgdfg</asp:ListItem>
                    <asp:ListItem>dgdgdgfg</asp:ListItem>
                    <asp:ListItem>ggggg</asp:ListItem>
                    <asp:ListItem>gggggg</asp:ListItem>
                    <asp:ListItem>sdasdasddsasda</asp:ListItem>
                    <asp:ListItem>sfdsdfsdfsdfsd</asp:ListItem>
                    <asp:ListItem>fsadasfsfasff</asp:ListItem>
                    <asp:ListItem>fsdsdfsdfsdf</asp:ListItem>
                    <asp:ListItem>yolo</asp:ListItem>
                    <asp:ListItem>fdssdfff</asp:ListItem>
                    <asp:ListItem>sdfsdfsdfsdf</asp:ListItem>
                    <asp:ListItem>sfdfsdfsdf</asp:ListItem>
                    <asp:ListItem>sfdsdfsdfds</asp:ListItem>
                    <asp:ListItem>sfddsfsdfsdffds</asp:ListItem>
                    <asp:ListItem>sdfsdfsdfsdf</asp:ListItem>
                    </asp:ListBox></p>
               

            </div>
             <div class="large-3 callout columns">
                 </div>
        </div>
    </form>
</body>
</html>
