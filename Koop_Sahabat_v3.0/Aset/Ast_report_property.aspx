<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Aset/Ast_report_property.aspx.cs" Inherits="Ast_report_property" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
          <script>
     $(function () {
         $('.select2').select2();
     });
</script> 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server" ScriptMode="Release">
    </asp:ScriptManager>

     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" style="height: 895px;">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>Laporan Hartanah</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Pengurusan Aset</a></li>
                            <li class="active">Laporan Hartanah</li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
      
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title">Pilihan Jana Laporan</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                         <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                               <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama Laporan <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txticno" runat="server" class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase"
                                                            ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Negeri</label>
                                    <div class="col-sm-8">
                                    <asp:DropDownList ID="ss_dd" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                   
                                    <div class="col-sm-8">
                                    <label>
                                                        <asp:CheckBox ID="chk_all" runat="server"/>
                                                        
                                                           &nbsp; Semua Hartanah
                                                        </label>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-4 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:CheckBox ID="cb1" runat="server" Text="" Checked="true"/> </label>
                                    <div class="col-sm-8">
                                   <label style="padding-top:10px;">Negeri</label>
                                    </div>
                                        
                                </div>
                            </div>
                                  <div class="col-md-4 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:CheckBox ID="cb5" runat="server" Text="" Checked="true"/></center> </label>
                                    <div class="col-sm-8">
                                   <label style="padding-top:10px;">No PT</label>
                                    </div>
                                        
                                </div>
                            </div>
                                  <div class="col-md-4 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:CheckBox ID="cb3" runat="server" Text="" /> </label>
                                    <div class="col-sm-8">
                                   <label style="padding-top:10px;">Bandar / Mukim</label>
                                    </div>
                                        
                                </div>
                            </div>
                                 </div>
                                </div>
                              <div class="row">
                             <div class="col-md-12">
                           
                                  <div class="col-md-4 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:CheckBox ID="cb4" runat="server" Text="" /> </label>
                                    <div class="col-sm-8">
                                   <label style="padding-top:10px;">No Lembaran Piawai</label>
                                    </div>
                                        
                                </div>
                            </div>
                                   <div class="col-md-4 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:CheckBox ID="cb2" runat="server" Text="" /> </label>
                                    <div class="col-sm-8">
                                   <label style="padding-top:10px;">No Permohonan Ukur</label>
                                    </div>
                                        
                                </div>
                            </div>
                                  <div class="col-md-4 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:CheckBox ID="cb6" runat="server" Text="" /> </label>
                                    <div class="col-sm-8">
                                   <label style="padding-top:10px;">No Fail</label>
                                    </div>
                                        
                                </div>
                            </div>
                                 </div>
                                </div>
                              <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-4 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:CheckBox ID="cb7" runat="server" Text="" /> </label>
                                    <div class="col-sm-8">
                                   <label style="padding-top:10px;">Luas Sementara (Meter Persegi)</label>
                                    </div>
                                        
                                </div>
                            </div>
                                  <div class="col-md-4 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:CheckBox ID="cb8" runat="server" Text="" /></label>
                                    <div class="col-sm-8">
                                   <label style="padding-top:10px;">Tarikh Geran Dikeluarkan</label>
                                    </div>
                                        
                                </div>
                            </div>
                                  <div class="col-md-4 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:CheckBox ID="cb9" runat="server" Text="" /></label>
                                    <div class="col-sm-8">
                                   <label style="padding-top:10px;">Penggunaan Tanah</label>
                                    </div>
                                        
                                </div>
                            </div>
                                 </div>
                                </div>
                              <div class="row">
                             <div class="col-md-12">
                           
                                  <div class="col-md-4 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:CheckBox ID="cb10" runat="server" Text="" /></label>
                                    <div class="col-sm-8">
                                   <label style="padding-top:10px;"> Tarikh Cukai Pintu</label>
                                    </div>
                                        
                                </div>
                            </div>
                                 <div class="col-md-4 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:CheckBox ID="cb11" runat="server" Text="" /> </label>
                                    <div class="col-sm-8">
                                   <label style="padding-top:10px;">Pembiayaan (RM)</label>
                                    </div>
                                        
                                </div>
                            </div>
                                  <div class="col-md-4 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  <asp:CheckBox ID="cb12" runat="server" Text="" /> </label>
                                    <div class="col-sm-8">
                                   <label style="padding-top:10px;"> Amaun Cukai Pintu (RM)</label>
                                    </div>
                                        
                                </div>
                            </div>
                                 </div>
                                </div>
                             
                              <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-4 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:CheckBox ID="cb13" runat="server" Text="" /></label>
                                    <div class="col-sm-8">
                                   <label style="padding-top:10px;">Bayaran Bulanan (RM)</label>
                                    </div>
                                        
                                </div>
                            </div>
                                  <div class="col-md-4 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:CheckBox ID="cb14" runat="server" Text="" /> </label>
                                    <div class="col-sm-8">
                                   <label style="padding-top:10px;">Tarikh Cukai Tanah</label>
                                    </div>
                                        
                                </div>
                            </div>
                                  <div class="col-md-4 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:CheckBox ID="cb15" runat="server" Text="" /> </label>
                                    <div class="col-sm-8">
                                   <label style="padding-top:10px;">Tempoh Pembiayaan</label>
                                    </div>
                                        
                                </div>
                            </div>
                                 </div>
                                </div>
                              <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-4 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:CheckBox ID="cb16" runat="server" Text="" /></label>
                                    <div class="col-sm-8">
                                   <label style="padding-top:10px;">Amaun Cukai Tanah (RM)</label>
                                    </div>
                                        
                                </div>
                            </div>
                            <div class="col-md-4 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:CheckBox ID="cb17" runat="server" Text="" /> </label>
                                    <div class="col-sm-8">
                                   <label style="padding-top:10px;">Kadar Faedah (%)</label>
                                    </div>
                                        
                                </div>
                            </div>
                                  <div class="col-md-4 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:CheckBox ID="cb18" runat="server" Text="" /> </label>
                                    <div class="col-sm-8">
                                   <label style="padding-top:10px;">Tarikh Cukai Bandar</label>
                                    </div>
                                        
                                </div>
                            </div>
                                 </div>
                                </div>
                              <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-4 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:CheckBox ID="cb19" runat="server" Text="" /></label>
                                    <div class="col-sm-8">
                                   <label style="padding-top:10px;">Nama Bank</label>
                                    </div>
                                        
                                </div>
                            </div>
                                  <div class="col-md-4 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:CheckBox ID="cb20" runat="server" Text="" /> </label>
                                    <div class="col-sm-8">
                                   <label style="padding-top:10px;">Amaun Cukai Bandar (RM)</label>
                                    </div>
                                        
                                </div>
                            </div>
                                  <div class="col-md-4 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:CheckBox ID="cb21" runat="server" Text="" /></label>
                                    <div class="col-sm-8">
                                   <label style="padding-top:10px;">Jenis Pegangan</label>
                                    </div>
                                        
                                </div>
                            </div>
                                 </div>
                                </div>
                              <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-4 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:CheckBox ID="cb22" runat="server" Text="" /> </label>
                                    <div class="col-sm-8">
                                   <label style="padding-top:10px;">Jenis Milikan</label>
                                    </div>
                                        
                                </div>
                            </div>
                            <div class="col-md-4 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:CheckBox ID="cb23" runat="server" Text="" /></label>
                                    <div class="col-sm-8">
                                   <label style="padding-top:10px;">Tempoh Pajakan (Tahun)</label>
                                    </div>
                                        
                                </div>
                            </div>
                                  <div class="col-md-4 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:CheckBox ID="cb24" runat="server" Text="" /> </label>
                                    <div class="col-sm-8">
                                   <label style="padding-top:10px;">Peratus Milikan (%)</label>
                                    </div>
                                        
                                </div>
                            </div>
                                 </div>
                                </div>
                              <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-4 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  <asp:CheckBox ID="cb25" runat="server" Text="" /> </label>
                                    <div class="col-sm-8">
                                   <label style="padding-top:10px;">Tarikh Akhir Pajakan</label>
                                    </div>
                                        
                                </div>
                            </div>
                                  <div class="col-md-4 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:CheckBox ID="cb26" runat="server" Text="" /></label>
                                    <div class="col-sm-8">
                                   <label style="padding-top:10px;">Nama Ejen</label>
                                    </div>
                                        
                                </div>
                            </div>
                                  <div class="col-md-4 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:CheckBox ID="cb27" runat="server" Text="" /> </label>
                                    <div class="col-sm-8">
                                   <label style="padding-top:10px;">Alamat Premis Disewa</label>
                                    </div>
                                        
                                </div>
                            </div>
                                 </div>
                                </div>
                              <div class="row">
                             <div class="col-md-12">
                                    <div class="col-md-4 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:CheckBox ID="cb28" runat="server" Text="" /> </label>
                                    <div class="col-sm-8">
                                   <label style="padding-top:10px;">Penyewa</label>
                                    </div>
                                        
                                </div>
                            </div>
                            <div class="col-md-4 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:CheckBox ID="cb29" runat="server" Text="" /> </label>
                                    <div class="col-sm-8">
                                   <label style="padding-top:10px;">Amaun Sewaan (RM)</label>
                                    </div>
                                        
                                </div>
                            </div>
                                  <div class="col-md-4 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:CheckBox ID="cb30" runat="server" Text="" /> </label>
                                    <div class="col-sm-8">
                                   <label style="padding-top:10px;">Tempoh Disewa</label>
                                    </div>
                                        
                                </div>
                            </div>
                                 </div>
                                </div>
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                 <asp:Button ID="Btn_Carian" runat="server" class="btn btn-primary" Text="Jana"  UseSubmitBehavior="false" OnClick="Button4_Click"/>
                                                <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" OnClick="Reset_btn" usesubmitbehavior="false"/>
                            </div>
                           </div>
                               </div>
                            <div class="box-header with-border" id="disp_hdr_txt" runat="server" visible="false">
                            <h3 class="box-title">Janaan Laporan</h3>
                        </div>
                             <div class="box-body">&nbsp;</div>
                             <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
               <div class="col-md-2 box-body"> &nbsp; </div>
                <div class="col-md-8 box-body">
                                <rsweb:ReportViewer ID="ReportViewer1" runat="server" width="100%" Height="100%" SizeToReportContent="True"></rsweb:ReportViewer>
                                                            <asp:Label runat="server" ID="ReportErrorMessage" Visible="false" CssClass="report-error-message"></asp:Label>
               </div>
                <div class="col-md-2 box-body"> &nbsp; </div>
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

