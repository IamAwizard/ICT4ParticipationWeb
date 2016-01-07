<%@ Page Title="" Language="C#" MasterPageFile="~/client/Client.Master" AutoEventWireup="true" CodeBehind="Chats.aspx.cs" Inherits="Project.client.Chats" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <div class="row" id="chat">
            <div class="large-12 column secondary callout">

                <div class="large-4 columns">
                    <div class="text-center">
                        <asp:TextBox ID="TextBox1" TextMode="MultiLine" Rows="10" Columns="20" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="large-8 columns">
                    <asp:Label ID="lbl_chatbox" runat="server" Text="Chat box:"></asp:Label>
                    <asp:ListBox ID="lbox_chat" runat="server" Height="100%" Rows="10"></asp:ListBox>
                    <hr />
                    <asp:TextBox ID="tbox_Bericht" TextMode="MultiLine" Rows="1" Columns="40" runat="server" Width="100%"></asp:TextBox>
                    <asp:Button ID="btn_Loadbericht" runat="server" Text="Verstuur bericht" CssClass="expanded button" />

                </div>
            </div>
        </div>
    </form>
</asp:Content>
