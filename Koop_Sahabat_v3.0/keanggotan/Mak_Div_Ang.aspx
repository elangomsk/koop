<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../keanggotan/Mak_Div_Ang.aspx.cs" Inherits="Mak_Div_Ang" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       

   
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

    <script>
     $(function () {

         $('#<%=GridView1.ClientID %>').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
             "responsive": true,
             "sPaginationType": "full_numbers",
             "iDisplayLength": 15,
             "aLengthMenu": [[15, 30, 50, 100], [15, 30, 50, 100]]
         });
     });
</script>   
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>  <asp:Label ID="ps_lbl1" runat="server"></asp:Label>  </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i><asp:Label ID="ps_lbl2" runat="server"></asp:Label>   </a></li>
                            <li class="active">     <asp:Label ID="ps_lbl3" runat="server"></asp:Label>  </li>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">  <asp:Label ID="ps_lbl5" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                          <div class="input-group">
                                                        <asp:TextBox ID="fromdate" runat="server" class="form-control validate[optional] datepicker mydatepickerclass" autocomplete="off" placeholder="PICK DATE"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                       </div>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="You can't leave this Empty" CssClass="requiredFieldValidateStyle" ForeColor="Red" ControlToValidate="fromdate" ValidationGroup="vgSubmit_kd_simp">
                                    </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                     
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   <asp:Label ID="ps_lbl6" runat="server"></asp:Label> <span class="style1">*</span>  </label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                                        <asp:TextBox ID="todate" runat="server" class="form-control validate[optional] datepicker mydatepickerclass" autocomplete="off" placeholder="PICK DATE"></asp:TextBox>
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                       </div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="You can't leave this Empty" CssClass="requiredFieldValidateStyle" ForeColor="Red" ControlToValidate="todate" ValidationGroup="vgSubmit_kd_simp">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl7" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="s_anggota" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                     <asp:ListItem>ANGGOTA SAH</asp:ListItem>
                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl8" runat="server"></asp:Label> %<span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                           <asp:TextBox ID="TextBox1" runat="server" class="form-control validate[optional,custom[number]]"></asp:TextBox>
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="You can't leave this Empty" CssClass="requiredFieldValidateStyle" ForeColor="Red" ControlToValidate="TextBox1" ValidationGroup="vgSubmit_kd_simp">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl9" runat="server"></asp:Label>  </label>
                                    <div class="col-sm-8">
                                          <textarea id="remark" rows="3" class="form-control uppercase" runat="server"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  <asp:Label ID="ps_lbl10" runat="server"></asp:Label>   </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="kelompok" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  </div>
                                </div>

                             <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                   <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Proses" UseSubmitBehavior="false" ValidationGroup="vgSubmit_kd_simp" OnClick="Button1_Click"/>
                                                            <asp:Button ID="Button6" runat="server" class="btn btn-default" Text="Set Semula" usesubmitbehavior="false" OnClick="Reset_btn"/>
                            </div>
                           </div>
                               </div>
                            <div class="row">
                             <div class="col-md-12">
                                 <div class="col-md-12 col-sm-4" style="text-align:center; padding-top:30px; line-height:13px;">
                                           <asp:TextBox ID="txtError" runat="server" BorderStyle="None" ForeColor="#CC0000" Width="610px"  BackColor="White" Enabled="False" style="text-transform:uppercase;"></asp:TextBox>
                                           </div>
                                 </div>
                                </div>

                                    <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                   
           <div class="col-md-12">
                                <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="1000000" ShowFooter="false" GridLines="None">
                                <Columns>
                                <asp:BoundField DataField="mem_new_icno" ItemStyle-HorizontalAlign="Left" HeaderText="NO KP BARU" />                                                                                       
                                <asp:BoundField DataField="mem_name" ItemStyle-HorizontalAlign="center" HeaderText="NAMA" />
                                <asp:BoundField DataField="mem_member_no" ItemStyle-HorizontalAlign="center" HeaderText="NO ANGGOTA" />                                                                                      
                                <asp:BoundField DataField="cawangan_name" ItemStyle-HorizontalAlign="Left" HeaderText="CAWANGAN" />                                                                                        
                                <asp:BoundField DataField="mem_centre" ItemStyle-HorizontalAlign="center" HeaderText="PUSAT" />
                                <asp:BoundField DataField="jumlah" ItemStyle-HorizontalAlign="center" HeaderText="JUMLAH SYER(RM)" DataFormatString="{0:n}" />
                                <asp:BoundField DataField="sha_credit_amt" ItemStyle-HorizontalAlign="center" HeaderText="JUMLAH DIVIDEN (RM)" DataFormatString="{0:n}" />
                                <asp:BoundField DataField="mem_bank_acc_no" ItemStyle-HorizontalAlign="center" HeaderText="NO AKAUN BANK"  />
                                <asp:BoundField DataField="Bank_Name" ItemStyle-HorizontalAlign="center" HeaderText="BANK"  />
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
                      <div class="row">
 <div class="box-body">&nbsp;
                                    </div>
                               
                        
               
          </div>
                                <rsweb:ReportViewer ID="RptviwerStudent" runat="server" width="100%" Height="100%" ZoomMode="PageWidth" SizeToReportContent="True"></rsweb:ReportViewer>
                                                            <asp:Label runat="server" ID="Label1" Visible="false" CssClass="report-error-message"></asp:Label>
               </div>
                <div class="col-md-1 box-body"> &nbsp; </div>
               </div>
          </div>
                             <br />
                                       
                                                 <div class="row" id="d1" runat="server" visible="false">
                                                <div class="col-md-6">
                                                    <div class="col-md-5 col-sm-1">
                                                        <label>Peratusan Dividen(%) :</label>
                                                    </div>
                                                    <div class="col-md-7 col-sm-2">
                                                       <label>   <asp:Label ID="Label3" runat="server" Text=""></asp:Label></label>
                                                    </div>
                                                </div>
                                               
                                            </div>
                                            
                                            <br />
                                              <div class="row" id="d2" runat="server" visible="false">                                                 
                                                <div class="col-md-6">
                                                    <div class="col-md-5 col-sm-1">
                                                        <label>Catatan    :</label>
                                                    </div>
                                                    <div class="col-md-7 col-sm-2">
                                                        <label> <asp:Label ID="Label12" runat="server"  Text=""></asp:Label></label>
                                                    </div>
                                                </div>                                             
                                            </div>
                                            <br />
                                              <div class="row" id="d3" runat="server" visible="false">
                                               
                                               <div class="col-md-6">
                                                    <div class="col-md-5 col-sm-1">
                                                        <label>Jumlah Anggota layak :</label>
                                                    </div>
                                                    <div class="col-md-7 col-sm-2">
                                                        <label> <asp:Label ID="Label4" runat="server" Text=""></asp:Label></label>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                            <br />
                                             <div class="row" id="d4" runat="server" visible="false">                                                 
                                               
                                               <div class="col-md-6">
                                                    <div class="col-md-5 col-sm-1">
                                                        <label>Jumlah Syer : RM</label>
                                                    </div>
                                                    <div class="col-md-7 col-sm-2">
                                                        <label> <asp:Label ID="Label5" runat="server" Text=""></asp:Label></label>
                                                    </div>
                                                </div>
                                            
                                            </div>
                                            <br />
                                             <div class="row" id="d5" runat="server" visible="false">
                                              
                                               <div class="col-md-6">
                                                    <div class="col-md-5 col-sm-1">
                                                        <label>Jumlah Dividen : RM</label>
                                                    </div>
                                                    <div class="col-md-7 col-sm-2">
                                                        <label> <asp:Label ID="Label6" runat="server" Text=""></asp:Label></label>
                                                    </div>
                                                </div>                                            
                                            </div>
                                            <br />
                           <div class="box-body">&nbsp;
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
