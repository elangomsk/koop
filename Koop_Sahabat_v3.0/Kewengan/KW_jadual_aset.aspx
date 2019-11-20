<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Kewengan/KW_jadual_aset.aspx.cs" Inherits="KW_jadual_aset" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server" ScriptMode="Release">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
       
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>Jadual Aset</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Transaksi</a></li>
                            <li class="active">Jadual Aset</li>
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
                            <h3 class="box-title">Maklumat Aset</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                         <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Profil Syarikat <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                     <asp:DropDownList id="Tahun_kew" runat="server" class="form-control select2 uppercase" style="width:100%" OnSelectedIndexChanged="sel_tahun" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                              <div class="box-header with-border">
                            <h3 class="box-title">Aset Baharu</h3>
                        </div>
                       
                         <div class="box-body">&nbsp;</div>
                         <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">PO No <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                      <asp:DropDownList ID="dd_pono" runat="server" class="form-control select2 uppercase" style="width:100%" AutoPostBack="true" OnSelectedIndexChanged="bind_pono_list">
                                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Kategori Aset <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                      <asp:DropDownList ID="DropDownList2" runat="server" class="form-control select2 uppercase" style="width:100%">
                                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>   
                              <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nilai Belian (RM) </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox2" runat="server" class="form-control validate[optional]" style="text-align:right;"></asp:TextBox> 
                                                                                <asp:TextBox ID="txt_debit_akn" Visible="false" runat="server" class="form-control validate[optional]"></asp:TextBox> 
                                                                                <asp:TextBox ID="txt_inv" Visible="false" runat="server" class="form-control validate[optional]"></asp:TextBox> 
                                                                                <asp:TextBox ID="txt_pv_dt" Visible="false" runat="server" class="form-control validate[optional]"></asp:TextBox> 
                                          <asp:TextBox ID="txt_jurnal_no" Visible="false" runat="server" class="form-control validate[optional]"></asp:TextBox> 
                                                                                <asp:TextBox ID="txtpvno" Visible="false" runat="server" class="form-control validate[optional]"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nilai Pelupusan (RM) </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox3" runat="server" class="form-control validate[optional]" style="text-align:right;"></asp:TextBox>  
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>   
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Susut Nilai (%) </label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="TextBox6" runat="server" class="form-control validate[optional]"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>   
                               <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                  <asp:Button ID="Button1" runat="server" class="btn btn-success" Text="Lihat" UseSubmitBehavior="false" OnClick="temperary_Click" />
                                  <asp:Button ID="Button2" runat="server" class="btn btn-default" Text="Set Semula"  UseSubmitBehavior="false" onclick="btn_reset"    />
                                  <asp:Button ID="Button10" runat="server" class="btn btn-danger" Text="Simpan" UseSubmitBehavior="false" onclick="Button10_Click"    />
                           </div>
                               </div>
                                   </div>
                              <div class="box-header with-border">
                            <h3 class="box-title">Bagi Bulan</h3>
                        </div>
                       
                         <div class="box-body">&nbsp;</div>
                               <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jana Kiraan Susut Nilai Bagi Bulan </label>
                                    <div class="col-sm-8">
                                      <%-- <asp:DropDownList ID="DropDownList1" runat="server" class="form-control select2 uppercase" style="width:100%">
                                                                                </asp:DropDownList>--%>
                                         <div class="input-group">
                                       <asp:TextBox ID="Tk_akhir"  runat="server" class="form-control validate[optional] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
                                          <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div> 
                              <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                <asp:Button ID="Button4" runat="server" class="btn btn-default" Text="Set Semula"  onclick="btn_reset1"    />
                                <asp:Button ID="Button3" runat="server" class="btn btn-danger" Text="Kira" 
                                                                    UseSubmitBehavior="false" OnClick="kira_Click" />   
                                                                                  <asp:Button ID="Button5" runat="server" class="btn btn-warning" Text="Jurnal Susut Nilai Akaun" 
                                                                    UseSubmitBehavior="false" OnClick="jana_Click" />   
                           </div>
                               </div>
                                   </div>
                            <div class="box-body">&nbsp;</div>
                            <hr />
                             <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
               <div class="col-md-1 box-body"> &nbsp; </div>
                <div class="col-md-10 box-body">
                       <%--    <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
                <div id="divImage" class="text-center" style="display:none; padding-top: 30px; font-weight:bold;">
                     <asp:Image ID="img1" runat="server" ImageUrl="../dist/img/LoaderIcon.gif" />&nbsp;&nbsp;&nbsp;Processing Please wait ... </div>  --%>
                                 <rsweb:ReportViewer ID="Rptviwerlejar" runat="server" width="100%" Height="100%" SizeToReportContent="True"></rsweb:ReportViewer>
                                                            <asp:Label runat="server" ID="Label1" Visible="false" CssClass="report-error-message"></asp:Label>
               </div>
                <div class="col-md-1 box-body"> &nbsp; </div>
               </div>
          </div>
                                                        
                            <div class="box-body">&nbsp;</div>
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
