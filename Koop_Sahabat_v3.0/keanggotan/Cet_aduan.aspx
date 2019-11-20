<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../keanggotan/Cet_aduan.aspx.cs" Inherits="Cet_aduan" %>

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
     
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server" ScriptMode="Release">
    </asp:ScriptManager>

     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>  <asp:Label ID="ps_lbl1" runat="server"></asp:Label> </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i><asp:Label ID="ps_lbl2" runat="server"></asp:Label></a></li>
                            <li class="active">   <asp:Label ID="ps_lbl3" runat="server"></asp:Label> </li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
      
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">  <asp:Label ID="ps_lbl5" runat="server"></asp:Label> <span class="style1">*</span>  </label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                                       <asp:TextBox ID="TxtFdate" runat="server" class="form-control validate[optional] datepicker mydatepickerclass" placeholder="PICK DATE"></asp:TextBox>
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                       </div>
                                    </div>
                                </div>
                            </div>
                           
                                 
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">    <asp:Label ID="ps_lbl6" runat="server"></asp:Label> <span class="style1">*</span>  </label>
                                    <div class="col-sm-8">
                                       <div class="input-group">
                                                        <asp:TextBox ID="TxtTdate" runat="server" class=" form-control validate[optional] datepicker mydatepickerclass" placeholder="PICK DATE"></asp:TextBox>
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
                                        <asp:DropDownList ID="ddadu" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                     </div>                 
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                  <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Jana Laporan" UseSubmitBehavior="false" OnClick="Button2_Click" />
                                <asp:Button ID="Button2" runat="server" class="btn btn-warning" Visible="false" Text="Cetak" UseSubmitBehavior="false" OnClick="Button1_Click" />
                                                        <asp:Button ID="Button3" runat="server" class="btn btn-default" Text="Set Semula" usesubmitbehavior="false" onclick="btnreset_Click"/>
                            </div>
                           </div>
                               </div>
                           <div class="box-body">&nbsp;
                                    </div>

                            <div id="ss1" runat="server" visible="false">
                            <div class="box-header with-border">
                            <h3 class="box-title"><asp:Label ID="ps_lbl10" runat="server"></asp:Label> </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                              <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
               <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" class="table table-striped uppercase"  Width="100%">
                                                                  <Columns>
                                                                                
                                                                                        <asp:BoundField DataField="com_new_icno" ItemStyle-HorizontalAlign="Left" HeaderText="NO KP BARU" />                                                                                       
                                                                                        <asp:BoundField DataField="mem_name" ItemStyle-HorizontalAlign="Left" HeaderText="NAMA" />
                                                                                        <asp:BoundField DataField="com_dt" ItemStyle-HorizontalAlign="center" HeaderText="Tarikh" />
                                                                                        <asp:BoundField DataField="Complaint_Type" ItemStyle-HorizontalAlign="Left" HeaderText="KATEGORI ADUAN" />                                                                                                                                                                                       
                                                                                        <asp:BoundField DataField="cawangan_name" ItemStyle-HorizontalAlign="Left" HeaderText="CAWANGAN" />                                                                                        
                                                                                        <asp:BoundField DataField="com_remark" ItemStyle-HorizontalAlign="Left" HeaderText="BUTIRAN ADUAN" />
                                                                                        <asp:BoundField DataField="keterangan" ItemStyle-HorizontalAlign="Left" Visible="false" HeaderText="STATUS"  />
                                                                                    </Columns>
                                       <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                       <%-- <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />--%>
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                                                </asp:GridView>
                <div class="col-md-10 box-body" style="display:none;">
                                <rsweb:ReportViewer ID="ReportViewer2" runat="server" width="100%" Height="100%" ZoomMode="PageWidth" SizeToReportContent="True"></rsweb:ReportViewer>
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
            </div>
            <!-- /.row -->

        
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
    </ContentTemplate>
                <Triggers>
                     <asp:PostBackTrigger ControlID="Button1"  />  
               <asp:PostBackTrigger ControlID="Button2"  />               
           </Triggers>
    </asp:UpdatePanel>
</asp:Content>




