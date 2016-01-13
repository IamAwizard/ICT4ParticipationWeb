<%@ Page Title="" Language="C#" MasterPageFile="~/volunteer/Volunteer_Master.Master" AutoEventWireup="true" CodeBehind="Volunteer_Vragen.aspx.cs" Inherits="Project.Volunteer_Vragen" %>


<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <div class="row" id="Questions">
            <div class="large-12 column callout">
                <div class="large-12 columns">
                    <asp:Label ID="lbl_Questions" runat="server" Text="Openstaande Vragen:"></asp:Label>
                    <asp:ListBox ID="lbox_Questions" runat="server" Height="100%" Rows="10" AutoPostBack="True"></asp:ListBox>
                     <asp:Label runat="server" ID="lbl_errormsg" Text="iets ging fout met bla bla bla bla bla x" Visible="False" CssClass="alert large button label"></asp:Label>
                    <br />

                </div>
                <div class="large-12 columns">
                    <div class="large-12 columns callout">
                        <div class="large-10 columns">
                            Vraag van hulpbehoevende:
                        </div>
                        <div class="large-2 columns">
                            <asp:Button ID="btn_AnswerQuestion" runat="server" Text="Reageren" CssClass="expanded button" OnClick="btn_AnswerQuestion_Click" />
                        </div>
                        <div class="large-8 columns">
                            <asp:TextBox ID="tbox_GetQuestion" runat="server" Height="100%" MaxLength="3000" Rows="5" TextMode="MultiLine"></asp:TextBox>
                        </div>
                        <div class="large-4 columns">
                            <asp:Label ID="lbl_Date" runat="server" Text="Datum: "></asp:Label><br />
                            <asp:Label ID="lbl_Location" runat="server" Text="Locatie:"></asp:Label><br />
                            <asp:Label ID="lbl_VolunteersNeeded" runat="server" Text="Vrijwilligers nodig:"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
