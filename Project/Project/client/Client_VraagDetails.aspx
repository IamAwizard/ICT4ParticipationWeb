﻿<%@ Page Title="" Language="C#" MasterPageFile="~/client/Client_Master.Master" AutoEventWireup="true" CodeBehind="Client_VraagDetails.aspx.cs" Inherits="Project.Client_VraagDetails" %>


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
                        <asp:Label ID="lbl_date" runat="server" Text="op 01-01-2016"></asp:Label>
                        <div style="float: right;">
                            <asp:Label ID="lbl_Critical" runat="server" ForeColor="Red" Text="URGENT"></asp:Label>
                        </div>
                        <br />
                        <asp:Label ID="lbl_user" runat="server" Text="Vroeg Lorem Ipsum"></asp:Label>
                        <br />
                        <br />
                        <asp:ListBox ID="lbox_Questions" runat="server" Height="100%" Rows="11"></asp:ListBox>
                        <div class="row">
                            <div class="large-12 column ">
                                <div class="row">
                                    <div class="large-4 columns">
                                        <asp:Label ID="lbl_Location" runat="server" Text="Locatie:" CssClass="middle" AssociatedControlID="tbox_Location"></asp:Label>
                                    </div>
                                    <div class="large-8 columns">
                                        <asp:TextBox ID="tbox_Location" AutoPostBack="true" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="large-4 columns">
                                        <asp:Label ID="lbl_Traveltime" runat="server" Text="Reistijd:" CssClass="middle" AssociatedControlID="tbox_Traveltime"></asp:Label>
                                    </div>
                                    <div class="large-8 columns">
                                        <asp:TextBox ID="tbox_Traveltime" AutoPostBack="true" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="large-4 columns">
                                        <asp:Label ID="lbl_Transport" runat="server" Text="Vervoer:" CssClass="middle" AssociatedControlID="selecttransport"></asp:Label>
                                    </div>
                                    <div class="large-8 columns">
                                        <asp:DropDownList runat="server" AutoPostBack="true"  ID="selecttransport"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="large-4 columns">
                                        <asp:Label ID="lbl_VolunteerCount" runat="server" Text="Aantal Vrijwilligers:" CssClass="middle" AssociatedControlID="tbox_VolunteerCount"></asp:Label>
                                    </div>
                                    <div class="large-8 columns">
                                        <asp:TextBox ID="tbox_VolunteerCount" AutoPostBack="true" runat="server"></asp:TextBox>
                                        <asp:Label ID="errormsg" ForeColor="Red" Visible="false" runat="server"></asp:Label>
                                    </div>
                                  
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="large-4 columns">
                    <div class="large-12 columns secondary callout">
                        <asp:Label ID="lbl_Volunteer" runat="server" Text="Jordy steenberg"></asp:Label>
                        <br />
                        <asp:Label ID="lbl_Commented" runat="server" Text="Heeft gereageerd"></asp:Label>
                        <br />
                        <asp:ListBox runat="server" Height="100%" Rows="7" ID="lbox_getquestion"></asp:ListBox>
                        <div class="row">
                            <div class="large-6 columns">
                                <asp:Button ID="btn_makeappointment" runat="server" Text="Afspraak maken" CssClass="button" />
                            </div>
                            <div class="large-6 columns">
                                <asp:Button ID="btn_writereview" runat="server" Text="Review schrijven" CssClass="button" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
