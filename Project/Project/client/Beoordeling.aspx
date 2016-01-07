<%@ Page Title="" Language="C#" MasterPageFile="~/client/Client.Master" AutoEventWireup="true" CodeBehind="Beoordeling.aspx.cs" Inherits="Project.client.Beoordeling" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <div class="row" id="Beoordelingen">
            <div class="large-12 column secondary callout">
                <div class="large-8 columns">
                    <asp:Label ID="lbl_Beoordelingen" runat="server" Text="Mijn Beoordelingen:"></asp:Label>
                    <asp:ListBox ID="lbox_Beoordelingen" runat="server" Height="100%" Rows="10"></asp:ListBox>
                 
                    
                </div>
                <div class="large-4 columns">
                    <div class="text-center">
                        <asp:Button ID="btn_LoadBeoordeling" runat="server" Text="Beoordeling bekijken" CssClass="expanded button" />
                        <asp:TextBox ID="tbox_Beoordeling" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
