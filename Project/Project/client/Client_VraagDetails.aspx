<%@ Page Title="" Language="C#" MasterPageFile="~/client/Client_Master.Master" AutoEventWireup="true" CodeBehind="Client_VraagDetails.aspx.cs" Inherits="Project.Client_VraagDetails" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <div class="row">
        <div class="large-12 column callout">
        <div class="row" id="Questions">
  
        <div class="text-center">
            <asp:Label runat="server" ID="lbl_questiondetails" Text="Vraagstatus: Onbeantwoord"></asp:Label>
        </div>
            <br />
            <br />
                <div class="large-8 columns">
                    <asp:Label ID="lbl_date" runat="server" Text=""></asp:Label>
                    <div style="float:right;">
                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="URGENT"></asp:Label>
                    </div>
                    <br />
                    <asp:Label ID="lbl_user" runat="server" Text=""></asp:Label>
                    <br />
                    <br />
                    <asp:ListBox ID="lbox_Questions" runat="server" Height="100%" Rows="11"></asp:ListBox>
                   </div>
                <div class="large-4 columns">
                    <div >
                    <asp:Label ID="lvl_volunteer" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="Label2" runat="server" Text="Heeft gereageerd"></asp:Label>
                    <br />
                    <br />
                        <asp:ListBox runat="server" Height="100%" Rows="7" ID="lbox_getquestion"></asp:ListBox>
                        <div class="row">
                                            <div  style="float:left; padding-left:20px;">
                                         <asp:Button ID="btn_makeappointment" runat="server" Text="Afspraak maken" CssClass="button" />
                                                </div>
                                            <div  style="float:right;">
                                  <asp:Button ID="btn_writereview" runat="server" Text="Review schrijven" CssClass="button" />
                                                </div>
                            </div>
                      
                    </div>
                </div>
            </div>
      
         <div class="row">
                     <div class="large-4 columns" style="padding-left:30px;">
                         <div class="row">
                         <div  style="float:left;">
                      <p>Locatie</p>
                             </div>
                         
                                 <div  style="float:right;">
                             <asp:TextBox runat="server" ID="tb_location"></asp:TextBox>
                             </div>
                             </div>
                                   <div class="row">
                              <div  style="float:left;">
                      <p>Reistijd</p>
                             </div>
                                 <div  style="float:right;">
                             <asp:TextBox runat="server" ID="tb_traveltime"></asp:TextBox>
                             </div>
                                            </div>
                                                 <div class="row">
                              <div  style="float:left;">
                      <p>Vervoer</p>
                             </div>
                                 <div  style="float:right;">
                             <asp:TextBox runat="server" ID="tb_transport"></asp:TextBox>
                             </div>
                                                          </div>
                                                               <div class="row">
                              <div  style="float:left;">
                      <p>Aantal vrijwilligers</p>
                             </div>
                                 <div  style="float:right;">
                             <asp:TextBox runat="server" ID="tb_volunteers"></asp:TextBox>
                             </div>
                     </div>
                  </div>
      </div>
            </div>
            </div>
    </form>
</asp:Content>
