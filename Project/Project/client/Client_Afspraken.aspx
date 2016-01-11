<%@ Page Title="" Language="C#" MasterPageFile="~/client/Client_Master.Master" AutoEventWireup="true" CodeBehind="Client_Afspraken.aspx.cs" Inherits="Project.Client_Afspraken" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <div class="row" id="Questions">
            <div class="large-12 column secondary callout">
                <div class="large-8 columns">
                    <asp:Label ID="lbl_Afspraken" runat="server" Text="Mijn Afspraken:"></asp:Label>
                    <asp:ListBox ID="lbox_Afspraak" runat="server" Height="100%" Rows="10"></asp:ListBox>
                 
                    
                </div>
                <div class="large-4 columns">
                    <div class="text-center">
                        <asp:Button ID="btn_LoadAfspraak" runat="server" Text="Afspraak bekijken" CssClass="expanded button" />
                        <asp:TextBox ID="tbox_Afspraak" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>

