<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="HR_Dashboard.aspx.cs" Inherits="HR_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>
    <style>
        .fixed .content-wrapper, .fixed .right-side{
    padding-top: 0px;
}</style>
    <style>
    .content-header{
        padding: 6px 0px 0 0px;
    }
    .content-header > .breadcrumb{
        padding: 5px 0px;
    }
    .content{
        padding: 7px 0px 0px 0px;
    }
</style>
       <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper container-padding">
                    <!-- Content Header (Page header) -->
                  <%--  <section class="content-header">
                        <h1>Dashboard</h1>
                        <ol class="breadcrumb">
                            <li><a href="../KSAIMB_Home.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                            <li class="active">Dashboard</li>
                        </ol>
                    </section>--%>

                    <!-- Main content -->
    <section class="content">
        
         <!-- Small boxes (Stat box) -->
      <div class="row" style="margin:0px;">
          <div class="container-scroll-sty">
                <div class="col-md-12 col-sm-12 well profile-count-padd">
                    <div class="col-md-6 col-sm-6">
                  <div class="col-md-4 col-sm-4 profile-img">
                       <asp:Image ID="ImgPrv_top_small" runat="server" class="img-circle face" height="130" width="130" alt="Avatar" />
                  </div>
                   <div class="col-md-8 col-sm-8">
                    <div class="profile-name">
                    <a>Selamat Datang, <asp:Label ID="lbluname1" runat="server"></asp:Label>&nbsp<span style="color:Teal;"><%--<i class="fa fa-smile-o"></i>--%></span></a><br />
                     <a class="profile-address"><asp:Label ID="Label1" runat="server"></asp:Label></a><br />
                      <a class="profile-address"><%--<i class="fa fa-street-view"></i>--%> <asp:Label ID="Label2" runat="server"></asp:Label></a>
                     </div>
                  </div>
            <div class="col-md-12 col-sm-12 alert alert-success fade in alert-success-sec">
                   <%-- <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>--%>
                    <p><strong> <asp:Label ID="Label3" runat="server"></asp:Label></strong></p>
                  </div>
               </div>

            <div class="col-md-6 col-sm-6 profile-count-padd">
                 <div class="col-md-6 col-sm-6">
                           <div class="well profile-sec-count">
                                <div class="profile-sec-count-inner">
                                  <h3>Cuti Tahunan</h3><span class="icon-align face icon-align-info1"><i class="ion ion-person-add"></i></span>
                                </div>
                                <p id="hdr_txt_sa" runat="server"><asp:Label ID="sv1" runat="server"></asp:Label></p>
                               <div class="progress">
                                <div id="cu_avg" runat="server" class="progress-bar"></div>
                              </div>
                             </div>
                 </div>
                 <div class="col-md-6 col-sm-6">
                           <div class="well profile-sec-count">
                                <div class="profile-sec-count-inner">
                                  <h3>Cuti Sakit</h3><span class="icon-align face icon-align-info2"><i class="ion ion-person-add"></i></span>
                                </div>
                                <p id="hdr_txt_jum" runat="server"><asp:Label ID="sv2" runat="server"></asp:Label></p>
                               <div class="progress">
                                <div id="ct_avg" runat="server" class="progress-bar bg-warning"></div>
                              </div>
                             </div>
                 </div>
                    <div class="col-md-6 col-sm-6">
                           <div class="well profile-sec-count">
                                <div class="profile-sec-count-inner">
                                  <h3>Cuti Bawa Hadapan</h3><span class="icon-align face icon-align-info4"> <i class="ion ion-person-add"></i></span>
                                </div>
                                <p id="hdr_txt_pa" runat="server"><asp:Label ID="sv3" runat="server"></asp:Label></p>
                               <div class="progress">
                                <div id="cs_avg" runat="server" class="progress-bar bg-info"></div>
                              </div>
                             </div>
                 </div>
                    <div class="col-md-6 col-sm-6">
                           <div class="well profile-sec-count">
                                <div class="profile-sec-count-inner">
                                  <h3>Cuti Ganti</h3><span class="icon-align face icon-align-info5"><i class="ion ion-person-add"></i></span>
                                </div>
                                <p id="hdr_txt_ts" runat="server"><asp:Label ID="sv4" runat="server"></asp:Label></p>
                               <div class="progress">
                                <div id="cu_samp" runat="server" class="progress-bar bg-danger"></div>
                              </div>
                             </div>
                 </div>
                </div>
                 <div class="col-md-6 col-sm-6">
                    <div class="profile-footer">
                     <div class="col-md-4 col-sm-6" id="LinkButton1_btn1" runat="server">
                          <asp:LinkButton  runat="server" id="LinkButton1" onclick="clk_profile">
                         <div class="well text-center icon-profile-sec">
                              <div class="icon-profile face">
                                  <img class="port-image" src="../dist/img/icon/user_set.png"/>
                              </div> 
                                <p>Kemaskini Profil</p>
                         </div>
                              </asp:LinkButton>
                         </div>
                        <div class="col-md-4 col-sm-6" id="LinkButton1_btn2" runat="server">
                             <asp:LinkButton  runat="server" id="LinkButton2" onclick="clk_cuti">
                         <div class="well text-center icon-profile-sec">
                              <div class="icon-profile face">
                                  <%--<i class="glyphicon glyphicon-globe"></i>--%>
                                  <img class="port-image" src="../dist/img/icon/leave.png"/>
                              </div> 
                                <p>Permohonan cuti</p>
                         </div>
                                 </asp:LinkButton>
                         </div>
                       <div class="col-md-4 col-sm-6" id="LinkButton1_btn3" runat="server">
                             <asp:LinkButton  runat="server" id="LinkButton3" onclick="clk_payslip">
                         <div class="well text-center icon-profile-sec">
                            <div class="icon-profile face">
                                  <img class="port-image" src="../dist/img/icon/payroll.png"/>
                              </div> 
                                <p>Semak Gaji</p>
                         </div>
                                 </asp:LinkButton>
                         </div>
                         <div class="col-md-4 col-sm-6" id="LinkButton1_btn4" runat="server">
                             <asp:LinkButton  runat="server" id="LinkButton4" onclick="clk_claim">
                         <div class="well text-center icon-profile-sec">
                             <div class="icon-profile face">
                                  <img class="port-image" src="../dist/img/icon/refund.png"/>
                              </div> 
                                <p>Tuntutan</p>
                         </div>
                                 </asp:LinkButton>
                         </div>
                        
                        <div class="col-md-4 col-sm-6" id="LinkButton1_btn5" runat="server">
                             <asp:LinkButton  runat="server" id="LinkButton5" onclick="clk_semak_pen">
                         <div class="well text-center icon-profile-sec">
                              <div class="icon-profile face">
                                  <img class="port-image" src="/dist/img/icon/history.png"/>
                              </div> 
                                <p>Semak Penggajian</p>
                         </div>
                                 </asp:LinkButton>
                         </div>
                      
                        <div class="col-md-4 col-sm-6" id="LinkButton1_btn6" runat="server">
                             <asp:LinkButton  runat="server" id="LinkButton7" onclick="clk_sck">
                         <div class="well text-center icon-profile-sec">
                             <div class="icon-profile face">
                                  <img class="port-image" src="../dist/img/icon/history.png"/>
                              </div> 
                                <p>Cuti Kakitangan</p>
                         </div>
                             </asp:LinkButton>
                         </div>
                        <div class="col-md-4 col-sm-6" id="LinkButton1_btn7" runat="server">
                             <asp:LinkButton  runat="server" id="LinkButton8" onclick="clk_kg">
                         <div class="well text-center icon-profile-sec">
                              <div class="icon-profile face">
                                  <img class="port-image" src="/dist/img/icon/sal.png"/>
                              </div> 
                               <div class="profile-count">
                                  <a href="#"><span class="badge"><asp:Label ID="kg1" runat="server"></asp:Label></span></a>
   </div>
                                <p>Kelulusan Gaji</p>
                         </div>
                                 </asp:LinkButton>
                         </div>
                       <div class="col-md-4 col-sm-6" id="LinkButton1_btn8" runat="server">
                           <asp:LinkButton  runat="server" id="LinkButton9" onclick="clk_kt">
                         <div class="well text-center icon-profile-sec">
                             <div class="icon-profile face">
                                  <img class="port-image" src="/dist/img/icon/history.png"/>
                              </div> 
                             <div class="profile-count">
                                  <a href="#"><span class="badge"><asp:Label ID="kg2" runat="server"></asp:Label></span></a>
   </div>
                                <p>Kelulusan Tuntutan</p>
                         </div>
                               </asp:LinkButton>
                         </div>
                         <div class="col-md-4 col-sm-6" id="LinkButton1_btn10" runat="server">
                           <asp:LinkButton  runat="server" id="LinkButton10" onclick="clk_kc">
                         <div class="well text-center icon-profile-sec">
                             <div class="icon-profile face">
                                  <img class="port-image" src="/dist/img/icon/leave_ap.png"/>
                              </div> 
                                <div class="profile-count">
                                  <a href="#"><span class="badge"><asp:Label ID="kg3" runat="server"></asp:Label></span></a>
   </div>
                                <p>Kelulusan Cuti</p>
                         </div>
                               </asp:LinkButton>
                         </div>
                        <div class="col-md-4 col-sm-6" id="LinkButton1_btn11" runat="server">
                             <asp:LinkButton  runat="server" id="LinkButton11" onclick="clk_stun">
                         <div class="well text-center icon-profile-sec">
                             <div class="icon-profile face">
                                  <img class="port-image" src="/dist/img/icon/settings.png"/>
                              </div> 
                                <p>Status Tuntutan</p>
                         </div>
                                 </asp:LinkButton>
                         </div>
                          <div class="col-md-4 col-sm-6" id="LinkButton1_btn12" runat="server">
                             <asp:LinkButton  runat="server" id="LinkButton12" onclick="clk_jpen">
                         <div class="well text-center icon-profile-sec">
                             <div class="icon-profile face">
                                  <img class="port-image" src="/dist/img/icon/settings.png"/>
                              </div> 
                                <p>Janaan Pendapatan</p>
                         </div>
                                 </asp:LinkButton>
                         </div>
                         <div class="col-md-4 col-sm-6" id="LinkButton1_btn9" runat="server">
                             <asp:LinkButton  runat="server" id="LinkButton6" onclick="clk_settings">
                         <div class="well text-center icon-profile-sec">
                             <div class="icon-profile face">
                                  <img class="port-image" src="/dist/img/icon/settings.png"/>
                              </div> 
                                <p>Tukar Kata Laluan</p>
                         </div>
                                 </asp:LinkButton>
                         </div>
                   </div>
                </div>
                    <div class="col-md-6 col-sm-6">
                          <div class="box-cal box-primary">
                            <div class="box-body no-padding">
                              <!-- THE CALENDAR -->
                              <div id="calendar"></div>
                            </div>
                            <!-- /.box-body -->
                          </div>
                    </div>
                </div>
      </div>
     </div>
      <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->

</asp:Content>

