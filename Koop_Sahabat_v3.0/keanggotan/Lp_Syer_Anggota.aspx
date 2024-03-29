﻿<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="Lp_Syer_Anggota.aspx.cs" Inherits="keanggotan_Lp_Syer_Anggota" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       <script type="text/javascript">
        $(function () {
            $('#gvSelected').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
                "responsive": true,
                "sPaginationType": "full_numbers"
            });
            $('#GridView1').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
                "responsive": true,
                "sPaginationType": "full_numbers"
            });
        });
    </script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">   
     <asp:ScriptManager ID="ScriptManagerCalendar" AsyncPostBackTimeOut="72000" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
      <script type="text/javascript">
             // Get the instance of PageRequestManager.
             var prm = Sys.WebForms.PageRequestManager.getInstance();
             // Add initializeRequest and endRequest
             prm.add_initializeRequest(prm_InitializeRequest);
             prm.add_endRequest(prm_EndRequest);

             // Called when async postback begins
             function prm_InitializeRequest(sender, args) {
                 // get the divImage and set it to visible
                 var panelProg = $get('divImage');
                 panelProg.style.display = '';
                 // reset label text
                 var lbl = $get('<%= this.lblText.ClientID %>');
                 lbl.innerHTML = '';

                 // Disable button that caused a postback
                 $get(args._postBackElement.id).disabled = true;
             }

             // Called when async postback ends
             function prm_EndRequest(sender, args) {
                 // get the divImage and hide it again
                 var panelProg = $get('divImage');
                 panelProg.style.display = 'none';

                 // Enable button that caused a postback
                 $get(sender._postBackSettings.sourceElement.id).disabled = false;
             }
         </script>

       <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>  Syer Anggota  </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i> Laporan Keanggotaan </a></li>
                            <li class="active"> Syer Anggota  </li>
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
                            <h3 class="box-title"><asp:Label ID="ps_lbl2" runat="server"></asp:Label> </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Mula <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                    <div class="input-group">                                                                    
                                                       <asp:TextBox ID="f_date" runat="server" AutoComplete="off" class="form-control datepicker mydatepickerclass" placeholder="Pick Date"></asp:TextBox>
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                       </div>
                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Tarikh Akhir  <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                       <div class="input-group">
                                                       <asp:TextBox ID="t_date" runat="server" AutoComplete="off" class="form-control datepicker mydatepickerclass" placeholder="Pick Date"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Jenis Syer <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                             <asp:DropDownList ID="DD_STS_ANGGO" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                 <asp:ListItem>--- PILIH ---</asp:ListItem>
                                                 <asp:ListItem Value="01">Tambah Syer</asp:ListItem>
                                                 <asp:ListItem Value="02">Penebusan Syer</asp:ListItem>
                                                 <asp:ListItem Value="03">Pemindahan Syer</asp:ListItem>
                                                 <asp:ListItem Value="05">Pemulangan Syer</asp:ListItem>
                                                 <asp:ListItem Value="06">Dana Tebus Syer</asp:ListItem>
                                                 <asp:ListItem Value="04">Kredit Ke Akaun Syer</asp:ListItem>
                                                                </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="You can't leave this Empty" CssClass="requiredFieldValidateStyle" ForeColor="Red" ControlToValidate="DD_STS_ANGGO" ValidationGroup="vgSubmit_lksn_simp">
                                    </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Jenis Laporan <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="DropDownList1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" >
                                                                <asp:ListItem Value="">--- PILIH ---</asp:ListItem>
                                                                    <asp:ListItem Value="01">RINGKASAN</asp:ListItem>
                                                                    <asp:ListItem Value="02">SENARAI</asp:ListItem>
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
                                <asp:Button ID="Button2" runat="server" class="btn btn-primary" Text="Jana" onclick="Button2_Click" />
                                <asp:Button ID="Button5" runat="server" class="btn btn-default" Text="Batal" OnClick="clk_batal" />
                                <asp:Button ID="Button1" runat="server" class="btn btn-warning" Visible="false"  Text="Export To PDF"  UseSubmitBehavior="false" onclick="ExportToPDF" />
                                <asp:Button ID="Button3" runat="server" Visible="false" class="btn btn-success"  Text="Export To EXCEL" UseSubmitBehavior="false" onclick="ExportToEXCEL" />    
                            </div>
                           </div>
                               </div>
                             <div class="box-header with-border" id="disp_hdr_txt" runat="server" visible="false">
                            <h3 class="box-title">Laporan Pengesahan Permohonan Anggota Baharu</h3>
                        </div>
                           <div class="box-body">&nbsp;</div>
                              <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">  
                                    <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
                 <div id="divImage" class="text-center" style="display:none; padding-top: 30px; font-weight:bold;">
                     <asp:Image ID="img1" runat="server" ImageUrl="../dist/img/LoaderIcon.gif" />&nbsp;&nbsp;&nbsp;Processing Please wait ... </div>                             
           <div class="col-md-12">
                 <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>    
               <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>     
                                                                         
                      </div> 
               </div>
                      
                               <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
               <div class="col-md-1 box-body"> &nbsp; </div>
                <div class="col-md-10 box-body">
                     
                                <rsweb:ReportViewer ID="ReportViewer1" runat="server" width="100%" Height="100%" ZoomMode="PageWidth" SizeToReportContent="True"></rsweb:ReportViewer>

                                                            <asp:Label runat="server" ID="ReportErrorMessage" Visible="false" CssClass="report-error-message"></asp:Label>
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
            <Triggers>
            <asp:PostBackTrigger ControlID="Button1"  />
             <asp:PostBackTrigger ControlID="Button3"  />
              
                 </Triggers>
    </asp:UpdatePanel>
        
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
   
</asp:Content>



