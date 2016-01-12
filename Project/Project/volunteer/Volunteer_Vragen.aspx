<%@ Page Title="" Language="C#" MasterPageFile="~/volunteer/Volunteer_Master.Master" AutoEventWireup="true" CodeBehind="Volunteer_Vragen.aspx.cs" Inherits="Project.Volunteer_Vragen" %>


<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <div class="row" id="Questions">
            <div class="large-12 column callout">
                <div class="large-12 columns">
                    <asp:Label ID="lbl_Questions" runat="server" Text="Openstaande Vragen:"></asp:Label>
                    <asp:ListBox ID="lbox_Questions" runat="server" Height="100%" Rows="10"></asp:ListBox>
                     <asp:Label runat="server" ID="lbl_errormsg" Text="errormessage"></asp:Label>
                    <br />
                    <asp:Button ID="btn_LoadQuestion" runat="server" Text="Vraag bekijken" CssClass="button" />

                </div>
                <div class="large-12 columns">
                    <div class="large-12 columns callout">
                        <div class="large-10 columns">
                            Vraag van hulpbehoevende:
                        </div>
                        <div class="large-2 columns">
                            <asp:Button ID="btn_AnswerQuestion" runat="server" Text="Reageren" CssClass="expanded button" />
                        </div>
                        <div class="large-8 columns">
                            <asp:ListBox runat="server" Height="100%" Rows="7" ID="lbox_getquestion"></asp:ListBox>
                        </div>
                        <div class="large-4 columns">
                            <asp:Label ID="lbl_Date" runat="server" Text="Datum: "></asp:Label><br />
                            <asp:Label ID="lbl_Location" runat="server" Text="Locatie:"></asp:Label><br />
                            <asp:Label ID="lbl_VolunteersNeeder" runat="server" Text="Vrijwilligers nodig:"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
