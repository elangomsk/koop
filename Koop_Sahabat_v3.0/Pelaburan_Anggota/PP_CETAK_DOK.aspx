<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../PELABURAN_ANGGOTA/PP_CETAK_DOK.aspx.cs" Inherits="PP_CETAK_DOK" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
   <%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" style="height: 895px;">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1> Cetak Dokumen Perjanjian Tambahan</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Pelaburan Anggota</a></li>
                            <li class="active"> Cetak Dokumen Perjanjian Tambahan</li>
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
                            <h3 class="box-title">Maklumat Pembiayaan</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Kata Kunci Carian <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="Applcn_no" runat="server"  class="form-control validate[optional] uppercase" MaxLength="12" ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                <%--  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-8">
                                        <asp:Button ID="Button9" runat="server" class="btn btn-primary" Text="Carian" usesubmitbehavior="false" onclick="Button9_Click" />
                                                    <asp:Button ID="Button10" runat="server" class="btn btn-default" Text="Set Semula" usesubmitbehavior="false" OnClick="rset_click"  />
                                    </div>
                                </div>
                            </div>--%>
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Wilayah / Pejabat</label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="txtwil" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Cawangan / Jabatan</label>
                                    <div class="col-sm-8">
                                             <asp:TextBox ID="txtcaw" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtname" runat="server" class="form-control validate[optional,custom[textSp]] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Jumlah Belian (RM) </label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="txtjumla" runat="server" class="form-control"  style="text-align:right;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>

                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Tempoh (Bulan) </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txttempoh" runat="server" class="form-control uppercase" MaxLength="2"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Jumlah Kumulatif Kena (RM) </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtamaun" runat="server" class="form-control"  style="text-align:right;"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>

                              <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Jumlah Kumulatif Bayar (RM) </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txttemp" runat="server" class="form-control"  style="text-align:right;"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Jumlah Kumulatif Tunggakan (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox2" runat="server" class="form-control validate[optional]"  style="text-align:right;"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>

                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Jumlah Untung (RM)  </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox3" runat="server" class="form-control"  style="text-align:right;"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Jumlah Kumulatif Simpanan (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox4" runat="server" class="form-control" style="text-align:right;"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>

                            <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Baki Kumulatif Pelaburan (RM)   </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox5" runat="server" class="form-control"  style="text-align:right;"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>

                             <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Butiran Perjanjian</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>

                            <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Tarikh Permohonan  <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                       <div class="input-group">
                                                       <asp:TextBox ID="Txtfromdate" runat="server" class="form-control validate[optional] datepicker datepicker mydatepickerclass" placeholder="Pick Date"></asp:TextBox>
                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                       </div>

                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Bil. Mesyuarat  <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox6" runat="server" class="form-control validate[optional] uppercase" ></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>

                            <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Tarikh Mesyuarat <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                       <div class="input-group">
                                                                   
                                                       <asp:TextBox ID="TextBox7" runat="server" class="form-control validate[optional] datepicker datepicker mydatepickerclass" placeholder="Pick Date"></asp:TextBox>
                                                       <span class="input-group-addon"><i class="fa fa-calendar"></i></span>

                                       </div>

                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Tarikh Mula  <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                       <div class="input-group">
                                                                   
                                                        <asp:TextBox ID="txttarmula" runat="server" class="form-control validate[optional] datepicker datepicker mydatepickerclass" placeholder="Pick Date"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">    Tarikh Tamat <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                       <div class="input-group">
                                                                   
                                                        <asp:TextBox ID="txtTTamat" runat="server" class="form-control validate[optional] datepicker datepicker mydatepickerclass" placeholder="Pick Date"></asp:TextBox>
                                                       <span class="input-group-addon"><i class="fa fa-calendar"></i></span>

                                       </div>

                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Tempoh Baru Keseluruhan (Bulan)   <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox10" runat="server" class="form-control validate[optional, custom[number]]" MaxLength="3"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                             


                            <div class="row">
                                <div class="col-md-12">
                             <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Tempoh Pemanjangan (Bulan) <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="ahk" runat="server" class="form-control validate[optional, custom[number]] sum_akb" MaxLength="10"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                 </div>
                        </div>


                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                   <asp:Button ID="Button3" runat="server" class="btn btn-danger" Text="Simpan" UseSubmitBehavior="false" onclick="Button3_Click" />                                                        
                                                        <asp:Button ID="Button4" runat="server" class="btn btn-warning" Text="Cetak" onclick="Button4_Click" UseSubmitBehavior="false" />
                                <asp:Button ID="Button2" runat="server" class="btn btn-default" Text="Kembali" usesubmitbehavior="false" OnClick="clk_bak"/> 
                            </div>
                           </div>
                               </div>
                           <div class="row">
                                <div class="col-md-12 col-sm-2" style="text-align: justify;">
                                                    
                                                        <rsweb:ReportViewer ID="Rptviwer_cetakjbb" runat="server">
                                                        </rsweb:ReportViewer>
                                                    </div>
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


