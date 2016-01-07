<%@ Page Title="" Language="C#" MasterPageFile="~/volunteer/Volunteer.Master" AutoEventWireup="true" CodeBehind="Volunteer_Main.aspx.cs" Inherits="Project.volunteer.Volunteer_Main" %>






<asp:Content ContentPlaceHolderID="Body" runat="server">


    <div class="row">
        <div class="large-8 column secondary callout">

            <asp:Button ID="btn_Profiel" runat="server" Text="Mijn Profiel" CssClass="expanded button" />

            <asp:Label ID="lbl_Vragen" runat="server" Text="Vragen"></asp:Label>
            <asp:ListBox ID="lbox_Questions" runat="server"></asp:ListBox>
           
            <asp:Label ID="lbl_Afspraken" runat="server" Text="Mijn Afspraken"></asp:Label>
             <asp:Button ID="btn_Reageer" runat="server" Text="Reageer" CssClass="expanded button" />
            <asp:Label ID="lbl_beoordelingen" runat="server" Text="Mijn Beoordelingen"></asp:Label>
            <asp:Label ID="lbl_Reviews" runat="server" Text="Mijn Reviews"></asp:Label>
            <asp:ListBox ID="lbox_Appointments" runat="server"></asp:ListBox>

            <asp:ListBox ID="lbox_Reviews" runat="server"></asp:ListBox>
        </div>
        <div class=" large-4 column secondary callout">
            <asp:Label ID="lbl_chats" runat="server" Text="Chats"></asp:Label>
            <asp:ListBox ID="lbox_Clients" runat="server"></asp:ListBox>
        </div>
    </div>



</asp:Content>
