<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/Admin.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Project.ADMIN.AdminQuestions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <div class="row" id="Questions">
            <div class="large-8 columns">
                <div class="secondary callout">
                    <asp:Label ID="lbl_Questions" runat="server" Text="Vragen:"></asp:Label>
                    <asp:ListBox ID="lbox_Questions" runat="server"></asp:ListBox>
                </div>
            </div>
            <div class="large-4 columns">
                <div class="secondary callout text-center">
                    <asp:Button ID="btn_DeleteQuestion" runat="server" Text="Verwijder vraag" CssClass="medium alert button" />
                </div>
            </div>
        </div>
        <div class="row" id="Reviews">
            <div class="large-8 columns">
                <div class="secondary callout">
                    <asp:Label ID="lbl_Reviews" runat="server" Text="Beoordelingen:"></asp:Label>
                    <asp:ListBox ID="lbox_Reviews" runat="server"></asp:ListBox>
                </div>
            </div>
            <div class="large-4 columns">
                <div class="secondary callout text-center">
                    <asp:Button ID="btn_DeleteReview" runat="server" Text="Verwijder beoordeling" CssClass="medium alert button" />
                </div>
            </div>
        </div>
    </form>
</asp:Content>
