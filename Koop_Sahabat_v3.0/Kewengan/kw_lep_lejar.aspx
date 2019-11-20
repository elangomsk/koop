<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Kewengan/kw_lep_lejar.aspx.cs" Inherits="kw_lep_lejar" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
.progress-container{padding:0.01em 16px}
.progress-round{border-radius:4px}
.progress-container{padding:10px 16px}
.progress-blue{color:#fff!important;background-color:#2196F3!important}
.progress-light-grey{color:#000!important;background-color:#f1f1f1!important}
</style>
         <script type="text/javascript">
             $(document).ready(function () {
                 $(<%=chk_lst.ClientID%>).SumoSelect(
                    { selectAll: true });

                 $('.select2').select2();
        });
    </script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:ScriptManager ID="ScriptManagerCalendar" AsyncPostBackTimeOut="72000" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
      
    
     <!-- Content Wrapper. Contains page content -->
         
                <div class="content-wrapper" >
                    <div class="loader"></div>
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1><asp:Label ID="ps_lbl1" runat="server"></asp:Label></h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i><asp:Label ID="ps_lbl2" runat="server"></asp:Label></a></li>
                            <li class="active"><asp:Label ID="ps_lbl3" runat="server"></asp:Label></li>
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
                            <h3 class="box-title"><asp:Label ID="ps_lbl4" runat="server"></asp:Label></h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                         <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl5" runat="server"></asp:Label> <span class="style1">*</span> </label>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl6" runat="server"></asp:Label> <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
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
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl7" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList id="DropDownList1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" OnSelectedIndexChanged="bind_gview" AutoPostBack="true">
                                                            <asp:ListItem value="0">SEMUA COA</asp:ListItem>
                                                            <asp:ListItem Enabled="false" value="1">KATEGORY AKAUN</asp:ListItem>
                                                            <asp:ListItem value="2">PELANGGAN</asp:ListItem>
                                                           <asp:ListItem value="3">PEMBEKAL</asp:ListItem>
                                                            <asp:ListItem Enabled="false" value="4">KAKITANGAN</asp:ListItem>
                                                       </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                        <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl8" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                     <asp:listbox runat="server" class="form-control uppercase" id="chk_lst" selectionmode="Multiple"></asp:listbox>
                                    </div>
                                </div>
                              
                            </div>
                                 
                                 </div>
                                </div> 
                              <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl9" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList id="ddpro" style="width:100%; font-size:13px;" runat="server" OnSelectedIndexChanged="select_project" AutoPostBack="true" class="form-control select2 validate[optional]"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Invois No </label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList id="dd_invois_no" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl11" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="rpt_type" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                            <asp:ListItem Value="01">PORTRAIT</asp:ListItem>
                                                            <asp:ListItem Value="02">LANDSCAPE</asp:ListItem>
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                 <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jurnal No </label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList id="DropDownList2" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>
                                    </div>
                                </div>
                                        <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl10" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                        <asp:CheckBox ID="zero_bal" runat="server" />
                                    </div>
                                </div>
                            </div>
                                
                                 </div>
                         </div>
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                <asp:Button ID="Button4" runat="server" class="btn btn-danger sub_btn" Text="Jana Laporan" OnClick="clk_submit"  UseSubmitBehavior="false" />
                                <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" OnClick="btn_reset"  UseSubmitBehavior="false" />
                            </div>
                           </div>
                               </div>
                            <div class="box-body">&nbsp;</div>
                                   <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
               <div class="col-md-1 box-body"> &nbsp; </div>
                <div class="col-md-10 box-body">
                         <%--  <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
                <div id="divImage" class="text-center" style="display:none; padding-top: 30px; font-weight:bold;">
                     <asp:Image ID="img1" runat="server" ImageUrl="../dist/img/LoaderIcon.gif" />&nbsp;&nbsp;&nbsp;Processing Please wait ... </div>  --%>
                                 <rsweb:ReportViewer ID="Rptviwerlejar"  runat="server" width="100%" Height="100%" SizeToReportContent="True" ></rsweb:ReportViewer>
                                                            <asp:Label runat="server" ID="ReportErrorMessage" Visible="false" CssClass="report-error-message"></asp:Label>
               </div>
                <div class="col-md-1 box-body"> &nbsp; </div>
               </div>
          </div>
                           <div class="box-body">&nbsp;
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

