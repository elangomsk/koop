<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../keanggotan/DIV_SENARI.aspx.cs" Inherits="DIV_SENARI" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       <script type="text/javascript">
           $(function () {
               var sel1 = "<%=DD_JenLap.SelectedValue %>";
               if (sel1 == "1") {
                   $('#GridView1').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
                       "responsive": true,
                       "sPaginationType": "full_numbers",
                   });
               }
               if (sel1 == "2") {
                   $('#GridView2').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
                       "responsive": true,
                       "sPaginationType": "full_numbers",
                   });
               }
           });
    </script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:ScriptManager ID="ScriptManagerCalendar" AsyncPostBackTimeOut="72000" runat="server" ScriptMode="Release"></asp:ScriptManager>
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
                        <h1>   Laporan Penyelesaian Anggota </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i> Keanggotaan   </a></li>
                            <li class="active">    Laporan Penyelesaian Anggota </li>
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
                            <h3 class="box-title"> Maklumat Anggota</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-6 box-body">
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4 control-label">   <asp:Label ID="lblJenPerm" Text="Jenis Permohonan" runat="server"></asp:Label> <span style="color: Red">*</span></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="DD_JenPerm" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                    <asp:ListItem Text="Tarik Diri" Value="TD"></asp:ListItem>
                                                    <asp:ListItem Text="Terhenti" Value="HN"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 box-body">
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4 control-label">   <asp:Label ID="lblJenLap" Text="Jenis Laporan" runat="server"></asp:Label> <span style="color: Red">*</span></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="DD_JenLap" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                    <asp:ListItem Text="Ringkasan" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Senarai" Value="2"></asp:ListItem>
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
                                    <label for="inputEmail3" class="col-sm-4 control-label">   <asp:Label ID="ps_lbl5" runat="server"></asp:Label> <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                            <div class="input-group">
                                                                   
                                                        <asp:TextBox ID="f_date" runat="server" class="form-control datepicker mydatepickerclass" autocomplete="off"
                                                                    placeholder="PICK DATE"></asp:TextBox>
                                                 <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                       </div>
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="You can't leave this Empty" CssClass="requiredFieldValidateStyle" ForeColor="Red" ControlToValidate="f_date" SetFocusOnError="true" ValidationGroup="vgSubmit_divs_simp">
                                    </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">  <asp:Label ID="ps_lbl6" runat="server"></asp:Label> <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                            <div class="input-group">       
                                                      <asp:TextBox ID="t_date" runat="server" class="form-control datepicker mydatepickerclass" autocomplete="off"
                                                                    placeholder="PICK DATE"></asp:TextBox>
                                                 <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                       </div>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="You can't leave this Empty" CssClass="requiredFieldValidateStyle" ForeColor="Red" ControlToValidate="t_date" SetFocusOnError="true" ValidationGroup="vgSubmit_divs_simp">
                                    </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                                  </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">   <asp:Label ID="ps_lbl7" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                              <asp:DropDownList ID="DD_kaw" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" AutoPostBack="true" onselectedindexchanged="DD_kaw_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">  <asp:Label ID="ps_lbl8" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                            <asp:DropDownList ID="DD_wilayah" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" AutoPostBack="true" onselectedindexchanged="DD_wilayah_SelectedIndexChanged">
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
                                    <label for="inputEmail3" class="col-sm-4 control-label">   <asp:Label ID="ps_lbl9" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                           <asp:DropDownList ID="DD_cawangan" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">   Status</label>
                                    <div class="col-sm-8">
                                           <asp:DropDownList ID="DropDownList1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                               <asp:ListItem Value="A">Permohonan Sah</asp:ListItem>
                                               <asp:ListItem Value="N">Permohonan Baru</asp:ListItem>
                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                                <%--  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">  <asp:Label ID="ps_lbl10" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                             <asp:TextBox ID="txt_pusat" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>--%>
                                  </div>
                                </div>

                             <hr />
                           <div class="row">
                           <div class="col-md-12">
                           <div class="col-md-12 box-body" style="text-align:center;">
                                      <asp:Button ID="Button2" runat="server" class="btn btn-primary" Text="Jana" onclick="Button2_Click" />                                                                
                                      <asp:Button ID="Button3" runat="server" class="btn btn-default" UseSubmitBehavior="false" Text="Set Semula" onclick="rst_clk" />
                                      <asp:Button ID="btnExpPDF" Visible="false" runat="server" class="btn btn-danger"  UseSubmitBehavior="false" Text="Export To PDF" OnClick="btnExpPDF_Click" />
                                      <asp:Button ID="btnExpExcel" Visible="false" runat="server" class="btn btn-warning"  UseSubmitBehavior="false" Text="Export To EXCEL" OnClick="btnExpExcel_Click" />
                           </div>   
                           </div>
                               </div>
                           <div class="col-md-12">&nbsp;</div>
                           <%--<div class="dataTables_wrapper form-inline dt-bootstrap"  style="overflow:auto;">--%>
                              <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
                 <div id="divImage" class="text-center" style="display:none; padding-top: 30px; font-weight:bold;">
                     <asp:Image ID="img1" runat="server" ImageUrl="../dist/img/LoaderIcon.gif" />&nbsp;&nbsp;&nbsp;Processing Please wait ... </div>  
                           <div class="col-md-12">
                                 <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>    
               <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>         
                                                        
                           </div>
                           <%--</div>--%>
                          
                           </div>
                           <div class="box-body">&nbsp;
                                    </div>

                             <div class="box-header with-border" id="ss1_stap1" runat="server" visible="false">
                            <h3 class="box-title">  <asp:Label ID="ps_lbl13" runat="server"></asp:Label> </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                   
                              <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
               <div class="col-md-1 box-body"> &nbsp; </div>
                <div class="col-md-10 box-body">
                     
                               <rsweb:ReportViewer ID="RptviwerPenAng" runat="server" width="100%" Height="100%" ZoomMode="PageWidth" SizeToReportContent="True"></rsweb:ReportViewer>
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
               </ContentTemplate>
              <Triggers>
            <asp:PostBackTrigger ControlID="btnExpPDF"  />
             <asp:PostBackTrigger ControlID="btnExpExcel"  />
              
                 </Triggers>
    </asp:UpdatePanel>
        
            </section>
            <!-- /.row -->
          
        <!-- /.row -->
    
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
   
</asp:Content>
