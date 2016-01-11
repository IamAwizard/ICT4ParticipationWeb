<%@ Page Title="" Language="C#" MasterPageFile="~/client/Client_Master.Master" AutoEventWireup="true" CodeBehind="Client_Vragen.aspx.cs" Inherits="Project.Client_Vragen" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <div class="row" id="Questions">
            <div class="large-12 column secondary callout">
                <div class="large-8 columns">
                    <asp:Label ID="lbl_Questions" runat="server" Text="Mijn Vragen:"></asp:Label>
                    <asp:ListBox ID="lbox_Questions" runat="server" Height="100%" Rows="10"></asp:ListBox>
                    <hr />
                    <asp:Label ID="lbl_AddQuestion" runat="server" Text="Vraag toevoegen:"></asp:Label>
                    <asp:TextBox ID="tbox_AddQuestion" runat="server" TextMode="MultiLine"></asp:TextBox>
                    <asp:Button ID="btn_AddQuestion" runat="server" Text="Vraag versturen" CssClass="expanded button" />
                </div>
                <div class="large-4 columns">
                    <div class="text-center">
                        <asp:Button ID="btn_LoadQuestion" runat="server" Text="Vraag bekijken" CssClass="expanded button" />
                        <asp:TextBox ID="tbox_Question" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
