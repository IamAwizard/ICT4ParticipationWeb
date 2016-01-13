<%@ Page Title="" Language="C#" MasterPageFile="~/volunteer/Volunteer_Master.Master" AutoEventWireup="true" CodeBehind="Volunteer_VraagDetails.aspx.cs" Inherits="Project.Volunteer_VraagDetails" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <div class="row">
            <div class="large-12 column callout">
                <div class="large-12 column">
                    <div class="text-center">
                        <asp:Label runat="server" ID="lbl_questiondetails" Text="Vraagstatus: Onbeantwoord"></asp:Label>
                    </div>
                    <br />
                    <br />
                </div>
                <div class="large-8 columns">
                    <div class="large-12 columns secondary callout">
                        <asp:Label ID="lbl_Date" runat="server" Text="op 01-01-2016"></asp:Label>
                        <div style="float: right;">
                            <asp:Label ID="lbl_Critical" runat="server" ForeColor="Red" Text="URGENT"></asp:Label>
                        </div>
                        <br />
                        <asp:Label ID="lbl_Client" runat="server" Text="Vroeg Lorem Ipsum"></asp:Label>
                        <br />
                        <br />
                        <asp:TextBox ReadOnly="true" ID="lbox_Question" runat="server" TextMode="MultiLine" Rows="5" Height="100%"></asp:TextBox>
                        <div class="row">
                            <div class="large-12 column ">
                                <div class="row">
                                    <div class="large-4 columns">
                                        <asp:Label ID="lbl_Location" runat="server" Text="Locatie:" CssClass="middle" AssociatedControlID="tbox_Location"></asp:Label>
                                    </div>
                                    <div class="large-8 columns">
                                        <asp:TextBox ID="tbox_Location"  ReadOnly="true" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="large-4 columns">
                                        <asp:Label ID="lbl_Traveltime"  runat="server" Text="Reistijd:" CssClass="middle" AssociatedControlID="tbox_Traveltime"></asp:Label>
                                    </div>
                                    <div class="large-8 columns">
                                        <asp:TextBox ID="tbox_Traveltime"  ReadOnly="true" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="large-4 columns">
                                        <asp:Label ID="lbl_Transport" runat="server" Text="Vervoer:" CssClass="middle" AssociatedControlID="tbox_Transport"></asp:Label>
                                    </div>
                                    <div class="large-8 columns">
                                        <asp:TextBox ID="tbox_Transport"  ReadOnly="true" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="large-4 columns">
                                        <asp:Label ID="lbl_VolunteerCount" runat="server" Text="Aantal Vrijwilligers:" CssClass="middle" AssociatedControlID="tbox_VolunteerCount"></asp:Label>
                                    </div>
                                    <div class="large-8 columns">
                                        <asp:TextBox ID="tbox_VolunteerCount"  ReadOnly="true" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="large-4 columns">
                    <div class="large-12 columns secondary callout">
                        <asp:Label ID="lbl_Volunteers" runat="server" Text="nog niemand"></asp:Label>
                        <br />
                        <br />
                        <asp:ListBox runat="server" Height="100%" Rows="7" ID="lbox_AcceptedVolunteers"></asp:ListBox>
                        <div class="row">
                            <div class="large-12 columns">
                                <asp:Button ID="btn_Answer" runat="server" Text="Beantwoorden!" CssClass="expanded button" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
