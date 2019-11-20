<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Kewengan/kw_debtor_ageing.aspx.cs" Inherits="kw_debtor_ageing" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server" ScriptMode="Release">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
         <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1><asp:Label ID="ps_lbl1" runat="server"></asp:Label> Debtor Ageing</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i><asp:Label ID="ps_lbl2" runat="server"></asp:Label></a></li>
                            <li class="active"><asp:Label ID="ps_lbl3" Visible="false" runat="server"></asp:Label>Debtor Ageing</li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
  
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title"><asp:Label ID="ps_lbl4" Visible="false" runat="server"></asp:Label>Maklumat Ageing</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                         <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl5" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                         <asp:TextBox ID="Tk_mula"  runat="server" class="form-control validate[optional] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
                                               <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                  
                                    <div class="col-sm-8">
                                           <asp:Button ID="Button4" runat="server" class="btn btn-danger sub_btn" Text="Jana Laporan" OnClick="clk_submit"  UseSubmitBehavior="false" />
                                <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" OnClick="btn_reset"  UseSubmitBehavior="false" />
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                            <div class="row" style="display:none;">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl6" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                          <asp:DropDownList id="dd_type" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                            <asp:ListItem Value="01">RECEIVABLE</asp:ListItem>
                                                            <asp:ListItem Value="02">PAYABLE</asp:ListItem>
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <hr />        
                             <div class="box-body">&nbsp;</div>                    
                           <div class="row">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
               <div class="col-md-1 box-body"> &nbsp; </div>
                <div class="col-md-10 box-body">
                         <%--  <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
                <div id="divImage" class="text-center" style="display:none; padding-top: 30px; font-weight:bold;">
                     <asp:Image ID="img1" runat="server" ImageUrl="../dist/img/LoaderIcon.gif" />&nbsp;&nbsp;&nbsp;Processing Please wait ... </div>  --%>
                                 <rsweb:ReportViewer ID="Rptviwerlejar"  runat="server" width="100%" Height="100%" SizeToReportContent="True" ></rsweb:ReportViewer>
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

         
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
   </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

