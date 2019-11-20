﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../PELABURAN_ANGGOTA/PP_MP_Tempoh.aspx.cs" Inherits="PP_MP_Tempoh" %>

 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" style="height: 895px;">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1> Permohonan Panjang Tempoh </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Pelaburan Anggota</a></li>
                            <li class="active"> Permohonan Panjang Tempoh </li>
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
                            <h3 class="box-title">Maklumat Pembiayaan</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Kata Kunci Carian <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="Applcn_no" runat="server"  class="form-control validate[optional] uppercase" MaxLength="12" ></asp:TextBox>
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
                                     <asp:Button ID="Button9" runat="server" class="btn btn-primary" Text="Carian" usesubmitbehavior="false" OnClick="btnsrch_Click" />
                                                    <asp:Button ID="Button10" runat="server" class="btn btn-default" Text="Set Semula" usesubmitbehavior="false" OnClick="btn_rstclick"/>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtname" runat="server" class="form-control validate[optional,custom[textSp]] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Jumlah Belian (RM) </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtjumla" runat="server" class="form-control" style="text-align:right;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                <%--  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Wilayah / Pejabat</label>
                                    <div class="col-sm-8">
                                            <asp:TextBox ID="txtwil" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>--%>
                                 </div>
                                </div>


                             <%--<div class="row">
                             <div class="col-md-12">
                           
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Cawangan / Jabatan</label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="txtcaw" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>--%>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jumlah Kumulatif Kena (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtamaun" runat="server" class="form-control" style="text-align:right;"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tempoh (Bulan)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txttempoh" runat="server" class="form-control uppercase" MaxLength="2"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jumlah Kumulatif Tunggakan (RM) </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox2" runat="server" class="form-control" style="text-align:right;"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Jumlah Kumulatif Bayar (RM)</label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="txttemp" runat="server" class="form-control" style="text-align:right;"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>

                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Jumlah Kumulatif Simpanan (RM)  </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="TextBox4" runat="server" class="form-control" style="text-align:right;"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jumlah Untung (RM)</label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="TextBox3" runat="server" class="form-control" style="text-align:right;"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Baki Kumulatif Pelaburan (RM) </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox5" runat="server" class="form-control" style="text-align:right;"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>

                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                               <asp:Button ID="Button1" runat="server" class="btn btn-warning" UseSubmitBehavior="false" Text="Semak JBB" OnClick="click_jbb" />
                                                        <asp:Button ID="Button5" runat="server" class="btn btn-warning" UseSubmitBehavior="false" Text="Litigasi Bercagar" OnClick="clk_Bercagar"/>
                                                        <asp:Button ID="Button6" runat="server" class="btn btn-warning" UseSubmitBehavior="false" Text="Litigasi Penjamin" onclick="clk_Penjamin"/>
                                        <asp:Button ID="Button2" runat="server" class="btn btn-default" Text="Kembali" usesubmitbehavior="false" OnClick="clk_bak"/>                
                                 
                                 
                            </div>
                           </div>
                               </div>
                           <div class="box-body">&nbsp;
                                    </div>




                            <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Permohonan Pemanjangan Tempoh Pembiayaan</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>

                            <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Baki Tempoh Bayar (Bulan) <span style="color: Red">*</span>  </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox6" runat="server" class="form-control validate[optional,custom[number]]" MaxLength="3"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Baki Pembiayaan (RM) <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="TextBox51" runat="server" class="form-control validate[optional,custom[number]]" style="text-align:right;" onblur="addTotal_bk(this)" MaxLength="12"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>

                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Tempoh Pemanjangan (Bulan) <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox10" runat="server" class="form-control validate[optional,custom[number]]" MaxLength="3"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Amaun Kena Baru (RM) </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox12" runat="server" class="form-control validate[optional,custom[number]] sum_akb" style="text-align:right;" MaxLength="12"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                            <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Status <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="DropDownList1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                        <asp:ListItem Value="" Text="--- PILIH ---"></asp:ListItem>
                                                        <asp:ListItem Value="01" Text="Permohonan"></asp:ListItem>
                                                        <asp:ListItem Value="02" Text="Batal Permohonan"></asp:ListItem>
                                                        </asp:DropDownList>

                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Catatan <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                         <textarea id="Textarea2" class="form-control uppercase" rows="3" runat="server" MaxLength="500"></textarea>

                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                             <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                               <asp:Button ID="Button3" runat="server" class="btn btn-danger" Text="Simpan" usesubmitbehavior="false" OnClick="btn_click" />
                                                    <asp:Button ID="Button7" runat="server" Visible="false" class="btn btn-warning" Text="Cetak" usesubmitbehavior="false"  OnClick="cetak_click"/>
                                 
                            </div>
                           </div>
                               </div>
                          <div class="row">
                                   <div class="col-md-12 col-sm-2" style="text-align:center">
                                     <rsweb:ReportViewer ID="ReportViewer1" runat="server"></rsweb:ReportViewer>
                                     <asp:Label runat="server" ID="ReportErrorMessage" Visible="false" CssClass="report-error-message"></asp:Label>
                                    </div>
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



