<%@ Page Title="" Language="C#" MasterPageFile="~/volunteer/Volunteer_Master.Master" AutoEventWireup="true" CodeBehind="Volunteer_Profiel.aspx.cs" Inherits="Project.Volunteer_Profiel" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <div class="row">
            <div class="large-12 columns callout">
                <div class="row">
                    <div class="large-6 column">
                        <asp:Label ID="lbl_MyProfile" runat="server" Text="Mijn profiel:"></asp:Label>
                        <br />
                        <br />
                    </div>
                    <div class="large-6 column">
                        <asp:Label ID="lbl_Availability" runat="server" Text="Mijn beschikbaarheid:"></asp:Label>
                        <br />
                        <br />
                    </div>
                </div>
                <div class="large-6 columns">

                    <div class="large-6 columns">
                        <asp:Image ID="img_Photo" runat="server" ImageUrl="http://placehold.it/200x275" Height="275" Width="100%" />
                        <br />
                        <br />
                        <asp:FileUpload ID="FU_UploadPhoto" runat="server" />
                    </div>
                    <div class="large-6 columns">
                        <h4>
                            <asp:Label ID="lbl_GivenName" runat="server" Text="Jan Janssen"></asp:Label></h4>
                        <asp:Label ID="lbl_Rating" runat="server" Text="Rating: 0,0"></asp:Label><br /><br />
                        <asp:Label ID="lbl_Age" runat="server" Text="Leeftijd: 22"></asp:Label><br /><br />
                        <asp:CheckBox ID="cbox_HasLicense" runat="server" Text="Rijbewijs" /><br /><br />
                        <asp:LinkButton ID="link_VoGDownload" runat="server">VOG Downloaden</asp:LinkButton>
                    </div>
                </div>
                <div class="large-6 columns border-left-side">
                    <br />
                    <div class="row">

                        <div class="large-4 column">
                            <asp:Label ID="lbl_Monday" runat="server" Text="Maandag:" AssociatedControlID="ddl_Monday" CssClass="middle"></asp:Label>
                        </div>
                        <div class="large-8 column">
                            <asp:DropDownList ID="ddl_Monday" runat="server">
                                <asp:ListItem Value="1">Niet beschikbaar</asp:ListItem>
                                <asp:ListItem Value="1">Ochtend</asp:ListItem>
                                <asp:ListItem Value="3">Middag</asp:ListItem>
                                <asp:ListItem Value="4">Namiddag</asp:ListItem>
                                <asp:ListItem Value="5">Avond</asp:ListItem>
                                <asp:ListItem Value="6">Nacht</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">

                        <div class="large-4 column">
                            <asp:Label ID="lbl_Tuesday" runat="server" Text="Dinsdag:" AssociatedControlID="ddl_Tuesday" CssClass="middle"></asp:Label>
                        </div>
                        <div class="large-8 column">
                            <asp:DropDownList ID="ddl_Tuesday" runat="server">
                                <asp:ListItem Value="1">Niet beschikbaar</asp:ListItem>
                                <asp:ListItem Value="1">Ochtend</asp:ListItem>
                                <asp:ListItem Value="3">Middag</asp:ListItem>
                                <asp:ListItem Value="4">Namiddag</asp:ListItem>
                                <asp:ListItem Value="5">Avond</asp:ListItem>
                                <asp:ListItem Value="6">Nacht</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">

                        <div class="large-4 column">
                            <asp:Label ID="lbl_Wednesday" runat="server" Text="Woensdag:" AssociatedControlID="ddl_Wednesday" CssClass="middle"></asp:Label>
                        </div>
                        <div class="large-8 column">
                            <asp:DropDownList ID="ddl_Wednesday" runat="server">
                                <asp:ListItem Value="1">Niet beschikbaar</asp:ListItem>
                                <asp:ListItem Value="1">Ochtend</asp:ListItem>
                                <asp:ListItem Value="3">Middag</asp:ListItem>
                                <asp:ListItem Value="4">Namiddag</asp:ListItem>
                                <asp:ListItem Value="5">Avond</asp:ListItem>
                                <asp:ListItem Value="6">Nacht</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">

                        <div class="large-4 column">
                            <asp:Label ID="lbl_Thursday" runat="server" Text="Donderdag:" AssociatedControlID="ddl_Thursday" CssClass="middle"></asp:Label>
                        </div>
                        <div class="large-8 column">
                            <asp:DropDownList ID="ddl_Thursday" runat="server">
                                <asp:ListItem Value="1">Niet beschikbaar</asp:ListItem>
                                <asp:ListItem Value="1">Ochtend</asp:ListItem>
                                <asp:ListItem Value="3">Middag</asp:ListItem>
                                <asp:ListItem Value="4">Namiddag</asp:ListItem>
                                <asp:ListItem Value="5">Avond</asp:ListItem>
                                <asp:ListItem Value="6">Nacht</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">

                        <div class="large-4 column">
                            <asp:Label ID="lbl_Friday" runat="server" Text="Vrijdag:" AssociatedControlID="ddl_Friday" CssClass="middle"></asp:Label>
                        </div>
                        <div class="large-8 column">
                            <asp:DropDownList ID="ddl_Friday" runat="server">
                                <asp:ListItem Value="1">Niet beschikbaar</asp:ListItem>
                                <asp:ListItem Value="1">Ochtend</asp:ListItem>
                                <asp:ListItem Value="3">Middag</asp:ListItem>
                                <asp:ListItem Value="4">Namiddag</asp:ListItem>
                                <asp:ListItem Value="5">Avond</asp:ListItem>
                                <asp:ListItem Value="6">Nacht</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">

                        <div class="large-4 column">
                            <asp:Label ID="lbl_Saturday" runat="server" Text="Zaterdag:" AssociatedControlID="ddl_Saturday" CssClass="middle"></asp:Label>
                        </div>
                        <div class="large-8 column">
                            <asp:DropDownList ID="ddl_Saturday" runat="server">
                                <asp:ListItem Value="1">Niet beschikbaar</asp:ListItem>
                                <asp:ListItem Value="1">Ochtend</asp:ListItem>
                                <asp:ListItem Value="3">Middag</asp:ListItem>
                                <asp:ListItem Value="4">Namiddag</asp:ListItem>
                                <asp:ListItem Value="5">Avond</asp:ListItem>
                                <asp:ListItem Value="6">Nacht</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">

                        <div class="large-4 column">
                            <asp:Label ID="lbl_Sunday" runat="server" Text="Zondag:" AssociatedControlID="ddl_Sunday" CssClass="middle"></asp:Label>
                        </div>
                        <div class="large-8 column">
                            <asp:DropDownList ID="ddl_Sunday" runat="server">
                                <asp:ListItem Value="1">Niet beschikbaar</asp:ListItem>
                                <asp:ListItem Value="1">Ochtend</asp:ListItem>
                                <asp:ListItem Value="3">Middag</asp:ListItem>
                                <asp:ListItem Value="4">Namiddag</asp:ListItem>
                                <asp:ListItem Value="5">Avond</asp:ListItem>
                                <asp:ListItem Value="6">Nacht</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

            </div>
        </div>

    </form>
</asp:Content>
