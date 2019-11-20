<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_REKOD_KEHADIRAN.aspx.cs" Inherits="HR_REKOD_KEHADIRAN" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1 id="h1_tag" runat="server">Laporan Rekod Kehadiran</h1>
                        <ol class="breadcrumb">
                            <li id="bb1_text" runat="server"><a href="#"><i class="fa fa-dashboard"></i>Sumber Manusia</a></li>
                            <li class="active" id="bb2_text" runat="server"> Laporan Rekod Kehadiran </li>
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
                            <h3 class="box-title" id="h3_tag" runat="server">Maklumat Kehadiran</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">

                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl1_text" runat="server"> Tarikh Mula <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                                <asp:TextBox ID="txt_tmula" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            </div>
                                    </div>
                                </div>
                            </div>
                           
                               
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl2_text" runat="server"> Tarikh Sahingga <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                                <asp:TextBox ID="txt_takhir" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl3_text" runat="server"> Nama Organisasi  </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="DD_Orgnsi" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" AutoPostBack="true" OnSelectedIndexChanged="sel_orgbind">
                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                           
                                 
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl4_text" runat="server"> Perniagaan </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="dd_org_pen" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" AutoPostBack="true" OnSelectedIndexChanged="sel_orgjaba" >
                                            </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                
                            

                    <div class="row">
                             <div class="col-md-12">
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl5_text" runat="server"> Jabatan   </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="DD_JABAT" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" >
                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                        </div>
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                 <asp:Button ID="Btn_Senarai" runat="server" class="btn btn-danger" Text="Senarai" OnClick="Btn_Senarai_Click" />
                                                <asp:Button ID="Button3" runat="server" class="btn btn-default" Text="Set Semula" UseSubmitBehavior="false" OnClick="click_rst" />
                                
                            </div>
                           </div>
                               </div>
                           <div class="box-body">&nbsp;
                                    </div>
                                  <div class="box-header with-border" id="shw_hdr1" runat="server" visible="false">
                            <h3 class="box-title" id="h3_tag2" runat="server">Laporan Kehadiran Kakitangan</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                       

                                  <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                           <div class="col-md-12 box-body">
               <div class="col-md-2 box-body"> &nbsp; </div>
                <div class="col-md-8 box-body">
                                     <rsweb:ReportViewer ID="RptviwerStudent" runat="server" Width="100%" Height="100%" ZoomMode="PageWidth" CssClass="rpt" SizeToReportContent="True"></rsweb:ReportViewer>
                                    </div>
                                    </div>
                        </div>
                                 
 <div class="box-body">&nbsp;
                                    </div>
                    </div>
                </div>
            </div>
            <!-- /.row -->

         </ContentTemplate>
             <Triggers>
               <asp:PostBackTrigger ControlID="Btn_Senarai"  />
               
           </Triggers>
    </asp:UpdatePanel>
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
   
</asp:Content>

