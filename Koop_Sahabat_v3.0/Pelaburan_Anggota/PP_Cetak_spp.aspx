﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../PELABURAN_ANGGOTA/PP_Cetak_spp.aspx.cs" Inherits="PP_Cetak_spp" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" style="height: 895px;">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>   Cetak Surat Pelepasan Pembiayaan    </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Pelaburan Anggota</a></li>
                            <li class="active">  Cetak Surat Pelepasan Pembiayaan    </li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
      
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Pelanggan</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  No Permohonan <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="Applcn_no" runat="server" class="form-control validate[optional] uppercase"
                                                                    MaxLength="12"></asp:TextBox>
                                        <asp:Panel ID="autocompleteDropDownPanel" runat="server" ScrollBars="Auto" Height="150px"
                                                            Font-Size="Medium" HorizontalAlign="Left" Wrap="False" />
                                                        <cc1:AutoCompleteExtender ServiceMethod="GetCompletionList" MinimumPrefixLength="1"
                                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="Applcn_no"
                                                            ID="AutoCompleteExtender1" CompletionListElementID="autocompleteDropDownPanel"
                                                            CompletionListCssClass="form-control uppercase" runat="server" FirstRowSelected="false">
                                                        </cc1:AutoCompleteExtender>
                                    </div>
                                </div>
                            </div>
                             <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-8">
                                            <asp:Button ID="Button3" runat="server" class="btn btn-primary" Text="Carian" UseSubmitBehavior="false"
                                                              OnClick="btnsrch_Click"  />
                                                            <asp:Button ID="Button6" runat="server" class="btn btn-default" Text="Set Semula"
                                                                UseSubmitBehavior="false" OnClick="btnreset_Click" />
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                            
                             <div class="row">
                             <div class="col-md-12">
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Nama  </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="MP_nama" runat="server" class="form-control validate[optional,custom[textSp]] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  No KP Baru </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="MP_icno" runat="server" class="form-control validate[optional] uppercase"
                                                                    MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                                <%-- <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Wilayah / Pejabat </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="MP_wilayah" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Cawangan / Jabatan   </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="MP_cawangan" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>--%>

                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Amaun Pengeluaran (RM)  </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox1" runat="server" class="form-control uppercase" style="text-align:right;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Tempoh (Bulan)   </label>
                                    <div class="col-sm-8">
                                            <asp:TextBox ID="TextBox2" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>


                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Baki Pelaburan (RM) </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox3" runat="server" class="form-control uppercase" style="text-align:right;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                   
                         
                                 </div>
                                </div>
                  <div class="row">
                             <div class="col-md-12">
                           <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  No Rujukan  </label>
                                    <div class="col-sm-8">
                                           <asp:TextBox ID="no_rujukan" runat="server" class="form-control validate[optional] uppercase" MaxLength="20"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                          
                                 </div>
                                </div>
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                <asp:Button ID="Button12" runat="server" class="btn btn-danger"  Text="Simpan" usesubmitbehavior="false"  onclick="Click_rujukan"/>
                                                                <asp:Button ID="Button1" runat="server" class="btn btn-warning" Text="Cetak"  OnClick="Button1_Click"/>
                            </div>
                           </div>
                               </div>
                           <div class="row">
                             <div class="col-md-12 col-sm-2" style="text-align: justify;">
                                                        <rsweb:ReportViewer ID="Reportviwer" runat="server">
                                                        </rsweb:ReportViewer>
                                                        <asp:Label runat="server" ID="ReportErrorMessage" Visible="false" CssClass="report-error-message"></asp:Label>
                                                    </div>
                               </div>

                        </div>

                    </div>
                </div>
            </div>
            <!-- /.row -->

        
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
    </ContentTemplate>
             
    </asp:UpdatePanel>
</asp:Content>



