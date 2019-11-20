<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../keanggotan/PEN_MAK_DIV.aspx.cs" Inherits="PEN_MAK_DIV" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

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
                        <h1>  <asp:Label ID="ps_lbl1" runat="server"></asp:Label> </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i><asp:Label ID="ps_lbl2" runat="server"></asp:Label> </a></li>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">   <asp:Label ID="ps_lbl5" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="DropDownList1" class="form-control select2 validate[optional]" style="text-transform:uppercase;" runat="server">
                                                                    </asp:DropDownList>
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="You can't leave this Empty" CssClass="requiredFieldValidateStyle" ForeColor="Red" ControlToValidate="DropDownList1" ValidationGroup="vgSubmit_pd_simp">
                                    </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-9">
                                         <asp:Button ID="Button2" runat="server" class="btn btn-primary" Text="Papar" visible="false" onclick="Button2_Click" ValidationGroup="vgSubmit_pd_simp" />
                                              <asp:Button ID="Button3" runat="server" class="btn btn-danger" Text="Cetak" Visible="false"/>
                                            <asp:Button ID="Button1" runat="server" Visible="false" class="btn btn-default" usesubmitbehavior="false" Text="Batal" />
                                 <asp:Button ID="Button5" runat="server" class="btn btn-danger"  Text="Export To PDF" Visible="false"
                                                    UseSubmitBehavior="false" onclick="ExportToPDF"  />
                                                     <asp:Button ID="Button6" runat="server" class="btn btn-success"  Text="Export To EXCEL" Visible="false"
                                                    UseSubmitBehavior="false" onclick="ExportToEXCEL"  /> 
                                         <asp:Button ID="Button4" runat="server" class="btn btn-warning" Text="Hapus"  onclick="Hapus_Click" />
                                    </div>
                                </div>
                            </div>
                                  </div>
                                </div>
                     
                             <hr />
                            <div class="row">
                             <div class="col-md-12">
                                 <div class="col-md-12 col-sm-4" style="text-align:center; line-height:13px;">
                                           <asp:TextBox ID="txtError" runat="server" BorderStyle="None" ForeColor="#CC0000" Width="610px"  BackColor="White" Enabled="False" style="text-transform:uppercase;"></asp:TextBox>
                                           </div>
                                 </div>
                                </div>
                                     <div class="dataTables_wrapper form-inline dt-bootstrap" runat="server" style="overflow:auto;">
                                   
           <div class="col-md-12">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" class="table table-striped"  Width="100%">
                                                                  <Columns>
                                                                                
                                                                                        <asp:BoundField DataField="mem_new_icno" ItemStyle-HorizontalAlign="Left" HeaderText="NO KP BARU" />                                                                                       
                                                                                        <asp:BoundField DataField="mem_name" ItemStyle-HorizontalAlign="Left" HeaderText="NAMA" />
                                                                                        <asp:BoundField DataField="mem_sahabat_no" ItemStyle-HorizontalAlign="center" HeaderText="NO ANGGOTA" />
                                                                                        <asp:BoundField DataField="Wilayah_Name" ItemStyle-HorizontalAlign="Left" HeaderText="WILAYAH" />                                                                                                                                                                                       
                                                                                        <asp:BoundField DataField="cawangan_name" ItemStyle-HorizontalAlign="Left" HeaderText="CAWANGAN" />                                                                                        
                                                                                        <asp:BoundField DataField="mem_centre" ItemStyle-HorizontalAlign="Left" HeaderText="PUSAT" />
                                                                                        <asp:BoundField DataField="mem_address" ItemStyle-HorizontalAlign="Left" Visible="false" HeaderText="ALAMAT"  />
                                                                                        <asp:BoundField DataField="div_bank_acc_no" ItemStyle-HorizontalAlign="center" HeaderText="No. Akaun Bank"  />
                                                                                        <asp:BoundField DataField="Bank_Name" ItemStyle-HorizontalAlign="Left" HeaderText="Nama Bank"  />
                                                                                        <asp:BoundField DataField="jumlah" ItemStyle-HorizontalAlign="right" HeaderText="AMAUN SYER(RM)"  DataFormatString="{0:n}" />
                                                                                        <asp:BoundField DataField="div_debit_amt" ItemStyle-HorizontalAlign="right" HeaderText="AMAUN DIVIDEN(RM)"  DataFormatString="{0:n}" />
                                                             
                                                                                    </Columns>
                                       <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                       <%-- <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />--%>
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />
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
                      <div class="row">
 <div class="box-body">&nbsp;
                                    </div>
                               
                       
               
          </div>
                                <rsweb:ReportViewer ID="ReportViewer2" runat="server" width="100%" Height="100%" ZoomMode="PageWidth" SizeToReportContent="True"></rsweb:ReportViewer>
                                                            <asp:Label runat="server" ID="Label1" Visible="false" CssClass="report-error-message"></asp:Label>
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
             <Triggers>
           <asp:PostBackTrigger ControlID="Button5"  />
           <asp:PostBackTrigger ControlID="Button6"  />
              
                 </Triggers>
    </asp:UpdatePanel>
        
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
   
</asp:Content>
