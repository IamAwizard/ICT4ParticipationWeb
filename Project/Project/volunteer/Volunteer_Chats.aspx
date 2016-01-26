<%@ Page Title="" Language="C#" MasterPageFile="~/volunteer/Volunteer_Master.Master" AutoEventWireup="true" CodeBehind="Volunteer_Chats.aspx.cs" Inherits="Project.Volunteer_Chats" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <div class="row" id="chat">
            <div class="large-12 column callout">
                <br />
                <div class="large-4 columns">
                    <div class="text-center">
                        <asp:ListBox ID="lbox_Clients" runat="server" Height="100%" Rows="20" AutoPostBack="True"></asp:ListBox>
                    </div>
                </div>
                <div class="large-8 columns">
                    <asp:Label ID="lbl_chatbox" runat="server" Text="Chat box:"></asp:Label>
                    <asp:ScriptManager runat="server" ID="ScriptManager_Refresh" />
                    <asp:UpdatePanel runat="server" ID="UpdatePanel_Refresh">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer_Refresh" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Timer ID="Timer_Refresh" OnTick="Timer_Refresh_Tick" runat="server" Interval="5000"></asp:Timer>
                            <asp:ListBox ID="lbox_chat" AutoPostBack="false" runat="server" Height="100%" Rows="10"></asp:ListBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <hr />
                    <asp:TextBox ID="tbox_Message" TextMode="MultiLine" Rows="1" Columns="40" runat="server" Width="100%"></asp:TextBox>
                    <asp:Button ID="btn_SendMessage" runat="server" Text="Verstuur bericht" CssClass="expanded button" OnClick="btn_SendMessage_Click" />

                </div>
            </div>
        </div>
    </form>
</asp:Content>
