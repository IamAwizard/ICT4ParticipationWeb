<%@ Page Title="" Language="C#" MasterPageFile="~/client/Client_Master.Master" AutoEventWireup="true" CodeBehind="Client_Chats.aspx.cs" Inherits="Project.Client_Chats" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">

        <div class="row" id="chat">
            <div class="large-12 column callout">
                <br />
                <div class="large-4 columns">
                    <div class="text-center">
                        <asp:ListBox ID="lbox_Clients" AutoPostBack="true" runat="server" Height="100%" Rows="20"></asp:ListBox>
                    </div>
                </div>

           
      
                <div class="large-8 columns">
                    <asp:Label ID="lbl_chatbox" runat="server" Text="Chat box:"></asp:Label>
                  <asp:ScriptManager runat="server"   id="ScriptManager2"/>
                <asp:UpdatePanel runat="server" id="UpdatePanel1">
                       <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" />
                  </Triggers>
                <ContentTemplate>
                <asp:Timer ID="Timer1"  OnTick="Timer1_Tick" runat="server" Interval="5000"></asp:Timer>
                    <asp:ListBox ID="lbox_chat" AutoPostBack="false" runat="server" Height="100%" Rows="10"></asp:ListBox>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                    <hr />
                    <asp:TextBox ID="tbox_Bericht" TextMode="MultiLine" Rows="1" Columns="40" runat="server" Width="100%"></asp:TextBox>
                    <asp:Button ID="btn_Loadbericht" runat="server" OnClick="btn_Loadbericht_Click"  Text="Verstuur bericht" CssClass="expanded button" />

                </div>
            </div>
        </div>

    </form>
</asp:Content>
