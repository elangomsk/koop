<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../Pelaburan_Anggota/PP_cl_staff.aspx.cs" Inherits="PP_cl_staff" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" style="height: 895px;">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>  Senarai Semak Permohonan (Kakitangan)</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>  Pelaburan Anggota </a></li>
                            <li class="active">  Senarai Semak Permohonan (Kakitangan)</li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
       <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Pemohon</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No KP Baru <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="txticno" runat="server" class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase"
                                                            MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                   <div class="col-sm-8">
                                     <asp:Button ID="Button4" runat="server" class="btn btn-primary" Text="Carian" usesubmitbehavior="false" OnClick="btnsrch_Click" />
                                                    <asp:Button ID="Button5" runat="server" class="btn btn-default" Text="Set Semula" OnClick="btnreset_Click" />
                                       <asp:Button ID="Button8" runat="server" class="btn btn-default" Text="Kembali"  UseSubmitBehavior="false" OnClick="clk_bak" />
                                       </div>
                                    
                                </div>
                            </div>
                                  </div>
                                </div>

                                
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtnama" runat="server" class="form-control validate[optional,custom[textSp]]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Pejabat</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="txtpeja" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jabatan</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtjaba" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>

                              <div class="box-header with-border">
                            <h3 class="box-title">Senarai Semak Dokumen Pemohon</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left">1. Salinan kad pengenalan pemohon </label>
                                    <div class="col-sm-3 text-center">
                                      <asp:CheckBox ID="cbskpp1" runat="server" Text="" />
                                    </div>
                                </div>
                            </div>
                                 
                                 </div>
                                 </div>
                             <div class="row">
                             <div class="col-md-12">
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left">2. Salinan kad pengenalan penjamin / jaminan bersilang / pemilik cagaran</label>
                                    <div class="col-sm-3 text-center">
                                      <asp:CheckBox ID="cbskpp2" runat="server" Text="" />
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                            <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left">3. Salinan slip gaji pemohon</label>
                                    <div class="col-sm-3 text-center">
                                      <asp:CheckBox ID="cbssgp3" runat="server" Text="" />
                                    </div>
                                </div>
                            </div>
                                  
                                 </div>
                                 </div>
                             <div class="row">
                             <div class="col-md-12">
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left">4. Salinan slip gaji penjamin</label>
                                    <div class="col-sm-3 text-center">
                                       <asp:CheckBox ID="cbssgp4" runat="server" Text="" /></center>
                                    </div>
                                </div>
                            </div>
                                 
                                 </div>
                                 </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left">5. Pengiraan DSR </label>
                                    <div class="col-sm-3 text-center">
                                      <asp:CheckBox ID="cbpd5" runat="server" Text="" />
                                    </div>
                                </div>
                            </div>
                                 
                                 </div>
                                 </div>
                             <div class="row">
                             <div class="col-md-12">
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left">6. Borang tabung khairat hutang</label>
                                    <div class="col-sm-3 text-center">
                                       <asp:CheckBox ID="cbbtkh6" runat="server" Text="" />
                                    </div>
                                </div>
                            </div>
                                  
                                 </div>
                                 </div>
                              
                               <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Latar Belakang Pemohon</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">                                                                        
                                    <label for="inputEmail3" class="col-sm-3 control-label">Status Muflis</label>
                                  <div class="col-sm-8">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                               <ContentTemplate>
                                                            <div class="col-md-3 col-sm-4">
                                                            <asp:RadioButton ID="rbsm1" runat="server"  Text=" Muflis" GroupName="Group1"  />
                                                          
                                                            </div>
                                                            <div class="col-md-4 col-sm-5"> 
                                                            <asp:RadioButton ID="rbsm2" runat="server" Text=" Tidak Muflis" GroupName="Group1" />
                                                            </div>
                                                            </ContentTemplate>
                                                             </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">                                                                        
                                     <label for="inputEmail3" class="col-sm-3 control-label">Jika Muflis, Nyatakan </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txtjmn" runat="server" class="form-control uppercase" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">                                                                        
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tindakan Undang-Undang</label>
                                  <div class="col-sm-8">
                                      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                               <ContentTemplate>
                                                            <div class="col-md-3 col-sm-4">
                                                            <asp:RadioButton ID="rbtuu1" runat="server"  Text=" Ya"  GroupName="Group2" />
                                                          
                                                            </div>
                                                            <div class="col-md-3 col-sm-5"> 
                                                            <asp:RadioButton ID="rbtuu2" runat="server" Text=" Tidak" GroupName="Group2"  />
                                                            </div>
                                                            </ContentTemplate>
                                                             </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">                                                                        
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jika Ya, Nyatakan </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txtjyn" runat="server" class="form-control uppercase" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">                                                                        
                                    <label for="inputEmail3" class="col-sm-3 control-label">Status Anggota</label>
                                  <div class="col-sm-8">
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                               <ContentTemplate>
                                                            <div class="col-md-3 col-sm-4">
                                                            <asp:RadioButton ID="rbsa1" runat="server"  Text=" Aktif"  GroupName="Group3" />
                                                          
                                                            </div>
                                                            <div class="col-md-4 col-sm-5"> 
                                                            <asp:RadioButton ID="rbsa2" runat="server" Text=" Tidak Aktif"  GroupName="Group3"  />
                                                            </div>
                                                            </ContentTemplate>
                                                             </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                               <asp:Button ID="Button6" runat="server" class="btn btn-danger" Text="Kemaskini" OnClick="btnkems_Click" />
                                                        <asp:Button ID="Button2" runat="server" class="btn btn-danger" Text="Simpan" OnClick="btnsmmit_Click" />
                                                        <asp:Button ID="Button7" runat="server" class="btn btn-warning" Text="Cetak" OnClick="btnpt_Click" />
                                                        <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" OnClick="btnreset_Click" />
                                                        <asp:Button ID="Button3" runat="server" class="btn btn-default" Text="Kembali"  UseSubmitBehavior="false" OnClick="clk_bak" />
                                 
                            </div>
                           </div>
                               </div>
                                     <div class="row">
                                                        <div class="col-md-12 col-sm-2" style="text-align: center">
                                                     <rsweb:ReportViewer ID="Rptviwer_lt" runat="server"></rsweb:ReportViewer>
                                     <asp:Label runat="server" ID="ReportErrorMessage" Visible="false" CssClass="report-error-message"></asp:Label>
                                                        </div>
                                                    </div>
                                    
                           <div class="box-body">&nbsp;</div>
                              </div>
                        </div>

                    </div>
                </div>
            </div>
            <!-- /.row -->
             </ContentTemplate>
    </asp:UpdatePanel>
        
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
   
</asp:Content>



