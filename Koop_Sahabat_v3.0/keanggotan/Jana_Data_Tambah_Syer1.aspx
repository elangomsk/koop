<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../keanggotan/Jana_Data_Tambah_Syer1.aspx.cs" Inherits="Jana_Data_Tambah_Syer1" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
         <script type="text/javascript">
        $(function () {
            $('[id*=GridView1]').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
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
                        <h1> <asp:Label ID="ps_lbl1" runat="server"></asp:Label> </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>  <asp:Label ID="ps_lbl2" runat="server"></asp:Label> </a></li>
                            <li class="active"> <asp:Label ID="ps_lbl3" runat="server"></asp:Label> </li>
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
                            <h3 class="box-title"> <asp:Label ID="ps_lbl4" runat="server"></asp:Label> </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  <asp:Label ID="ps_lbl5" runat="server"></asp:Label><span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                    <div class="input-group">
                                                      <asp:TextBox ID="Txtfromdate" runat="server" class="form-control validate[optional] datepicker mydatepickerclass"
                                                                    placeholder="Pick Date"></asp:TextBox>
                                          <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                       </div>
                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  <asp:Label ID="ps_lbl6" runat="server"></asp:Label> <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                      <div class="input-group">
                                                        <asp:TextBox ID="Txttodate" runat="server" class="form-control  validate[optional] datepicker mydatepickerclass"
                                                                    placeholder="Pick Date"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl7" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="txtcurntdt" runat="server" class="form-control uppercase" ReadOnly></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                 <asp:Button ID="Button2" runat="server" class="btn btn-primary" Text="Proses" OnClick="Button2_Click" />
                                                        &nbsp;
                                                        <asp:Button ID="Button5" runat="server" class="btn btn-default" UseSubmitBehavior="false"
                                                            Text="Batal" />
                                 <asp:Button ID="Button1" runat="server" class="btn btn-warning" Visible="false"  Text="Export To PDF"  UseSubmitBehavior="false" onclick="ExportToPDF"  />
                                <asp:Button ID="Button3" runat="server" Visible="false" class="btn btn-success"  Text="Export To EXCEL" UseSubmitBehavior="false" onclick="ExportToEXCEL"  /> 
                                 
                            </div>
                           </div>
                               </div>
                        
                               <div class="box-header with-border" id="disp_hdr_txt" runat="server" visible="false">
                            <h3 class="box-title"><asp:Label ID="ps_lbl10" runat="server"></asp:Label></h3>
                        </div>
                           <div class="box-body">&nbsp;</div>

                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                   
           <div class="col-md-12">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" class="table table-striped"  Width="100%">
                                                                  <Columns>
                                                                                
                                                                                  
                                                                                        <asp:BoundField DataField="mem_new_icno" ItemStyle-HorizontalAlign="Left" HeaderText="NO KP BARU" />
                                                                                        <asp:BoundField DataField="mem_name" ItemStyle-HorizontalAlign="center" HeaderText="NAMA" />
                                                                                        <asp:BoundField DataField="mem_member_no" ItemStyle-HorizontalAlign="center" HeaderText="NO ANGGOTA" />
                                                                                        <asp:BoundField DataField="Wilayah_Name" ItemStyle-HorizontalAlign="center" HeaderText="WILAYAH" />
                                                                                        <asp:BoundField DataField="mem_centre" ItemStyle-HorizontalAlign="center" HeaderText="NAMA PUSAT" />
                                                                                        <asp:BoundField DataField="mem_address" ItemStyle-HorizontalAlign="center" HeaderText="ALAMAT" />
                                                                                        <asp:BoundField DataField="sha_ref" ItemStyle-HorizontalAlign="center" HeaderText="JENIS CARUMAN" />
                                                                                         <asp:BoundField DataField="sha_debit_amt" ItemStyle-HorizontalAlign="center" HeaderText="JUMLAH BELIAN SYER(RM)" DataFormatString="{0:n}" />
                                                             
                                                                                    </Columns>
                                      
                                                                                </asp:GridView>
                                                                                
    </div> 
               </div>
                              <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
               <div class="col-md-1 box-body"> &nbsp; </div>
                <div class="col-md-10 box-body">
                     <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
                 <div id="divImage" class="text-center" style="display:none; padding-top: 30px; font-weight:bold;">
                     <asp:Image ID="img1" runat="server" ImageUrl="../dist/img/LoaderIcon.gif" />&nbsp;&nbsp;&nbsp;Processing Please wait ... </div> 
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