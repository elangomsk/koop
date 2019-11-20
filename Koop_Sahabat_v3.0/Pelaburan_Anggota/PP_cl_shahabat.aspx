<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../Pelaburan_Anggota/PP_cl_shahabat.aspx.cs" Inherits="PP_cl_shahabat" %>

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
                        <h1>  Senarai Semak Permohonan (Anggota)</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>  Pelaburan Anggota </a></li>
                            <li class="active">  Senarai Semak Permohonan (Anggota)</li>
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
                            <h3 class="box-title">Maklumat Pemohon</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No KP Baru <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="txticno" runat="server" class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase"
                                                            MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                   <div class="col-sm-8">
                                      <asp:Button ID="Button4" runat="server" class="btn btn-primary" Text="Carian" UseSubmitBehavior="false"
                                                        OnClick="btnsrch_Click" />
                                                    <asp:Button ID="Button5" runat="server" class="btn btn-default" Text="Set Semula"
                                                        UseSubmitBehavior="false" OnClick="btnreset_Click" />
                                       <asp:Button ID="Button7" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" OnClick="clk_bak" />
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
                                        <asp:TextBox ID="txtname" runat="server" class="form-control validate[optional,custom[textSp]]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Cawangan</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="txtcaw" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Pusat</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtpust" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>

                             
                                    
                            <div class="box-header with-border">
                            <h3 class="box-title">Senarai Semak Kelayakan Pemohon</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left">1. Berumur 75 tahun kebawah </label>
                                    <div class="col-sm-3 text-center">
                                      <asp:CheckBox ID="cbbtk" runat="server" Text="" />
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left">6. Peratusan kehadiran Mesyuarat Bulanan Usahawan </label>
                                    <div class="col-sm-3">
                                       <asp:TextBox ID="txtmeet" runat="server" class="form-control validate[optional,custom[number]]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                             <div class="row">
                             <div class="col-md-12">
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left">2. Tidak diisytiharkan muflis </label>
                                    <div class="col-sm-3 text-center">
                                      <asp:CheckBox ID="cbtdm" runat="server" Text="" />
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left">7. Mempunyai penjamin atau cagaran </label>
                                    <div class="col-sm-3 text-center">
                                       <asp:CheckBox ID="cbmpac" runat="server" Text="" />
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                            <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left">3. Anggota Koperasi Sahabat yang aktif </label>
                                    <div class="col-sm-3 text-center">
                                      <asp:CheckBox ID="cbaksya" runat="server" Text="" />
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left">8. Mempunyai daftar perniagaan </label>
                                    <div class="col-sm-3 text-center">
                                       <asp:CheckBox ID="cbmdp" runat="server" Text="" />
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                             <div class="row">
                             <div class="col-md-12">
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left">4. Nilai Modal syer (RM) </label>
                                    <div class="col-sm-3">
                                       <asp:TextBox ID="txttms" runat="server" class="form-control validate[optional,custom[number]]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left">9. Telah menyelesaikan pembiayaan minimum RM 15,000 di AIM </label>
                                    <div class="col-sm-3 text-center">
                                       <asp:CheckBox ID="cbtmpm" runat="server" Text="" />
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left">5. Peratusan jumlah kehadiran dalam Mesyuarat Pusat AIM </label>
                                    <div class="col-sm-3 text-center">
                                      <asp:TextBox ID="txtatten" runat="server" class="form-control validate[optional,custom[number]]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left">10. Mempunyai akaun perniagaan yang diselia oleh setiausaha syarikat bertauliah </label>
                                    <div class="col-sm-3 text-center">
                                       <asp:CheckBox ID="cbmapy" runat="server" Text="" />
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                               &nbsp;
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left">11. Purata pendapatan sebulan Sahabat (RM)  </label>
                                    <div class="col-sm-3">
                                       <asp:TextBox ID="txtincom" runat="server" class="form-control validate[optional,custom[number]]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                              <div class="box-header with-border">
                            <h3 class="box-title">Senarai Semak Dokumen Pemohon</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left">1. Salinan kad pengenalan pemohon </label>
                                    <div class="col-sm-3 text-center">
                                      <asp:CheckBox ID="cbskpp1" runat="server" Text="" />
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left">11. Salinan Perjanjian Sewa bagi kiosk/gerai/premisyang beroperasi (jika berkaitan) </label>
                                    <div class="col-sm-3 text-center">
                                       <asp:CheckBox ID="cbspsbk11" runat="server" Text="" />
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                             <div class="row">
                             <div class="col-md-12">
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left">2. Salinan kad pengenalan penjamin / jaminan bersilang / pemilik cagaran</label>
                                    <div class="col-sm-3 text-center">
                                      <asp:CheckBox ID="cbskpjp2" runat="server" Text="" />
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left"> 12. Salinan bil utiliti atas nama pemohon (satu sahaja) </label>
                                    <div class="col-sm-3 text-center">
                                       <asp:CheckBox ID="cbsbuanp12" runat="server" Text="" />
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                            <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left">3. Salinan kad pengenalan pencarum ke-2 TKH </label>
                                    <div class="col-sm-3 text-center">
                                      <asp:CheckBox ID="cbskppkt3" runat="server" Text="" />
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left">13. Borang Pecahan Modal </label>
                                    <div class="col-sm-3 text-center">
                                       <asp:CheckBox ID="cbbpm13" runat="server" Text="" />
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                             <div class="row">
                             <div class="col-md-12">
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left">4. Gambar pemohon berukuran pasport </label>
                                    <div class="col-sm-3 text-center">
                                       <asp:CheckBox ID="cbgpbp4" runat="server" Text="" /></center>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left">14. Sebut harga pembekal</label>
                                    <div class="col-sm-3 text-center">
                                       <asp:CheckBox ID="cbshp14" runat="server" Text="" />
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left">5. Borang Tabung Khairat Hutang </label>
                                    <div class="col-sm-3 text-center">
                                      <asp:CheckBox ID="cbbtkh5" runat="server" Text="" />
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left">15. Salinan Geran Cagaran (jika mengunakan Jaminan Cagaran) </label>
                                    <div class="col-sm-3 text-center">
                                       <asp:CheckBox ID="cbsgc15" runat="server" Text="" />
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                             <div class="row">
                             <div class="col-md-12">
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left">6. Salinan Pendaftaran Perniagaan (SSM) </label>
                                    <div class="col-sm-3 text-center">
                                       <asp:CheckBox ID="cbspp6" runat="server" Text="" />
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left">16. Bukti pendapatan seperti penyata bank 3 bulan terkini </label>
                                    <div class="col-sm-3 text-center">
                                       <asp:CheckBox ID="cbbpspb16" runat="server" Text="" />
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                               <div class="row">
                             <div class="col-md-12">
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left">7. Salinan Lesen Pihak Berkuasa Tempatan (jika Berkaitan)</label>
                                    <div class="col-sm-3 text-center">
                                       <asp:CheckBox ID="cbslpbt7" runat="server" Text="" />
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left">17. Rekod Kehadiran di Pusat L4 </label>
                                    <div class="col-sm-3 text-center">
                                       <asp:CheckBox ID="cbrkdpl17" runat="server" Text="" />
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                               <div class="row">
                             <div class="col-md-12">
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left">8. Salinan Muka Depan Buku Bank/Penyata Ringkas Akaun Bank Pemohon</label>
                                    <div class="col-sm-3 text-center">
                                       <asp:CheckBox ID="cbsmdbb8" runat="server" Text="" />
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left">18. Rekod Bayaran Balik Pembiayaan terakhir AIM </label>
                                    <div class="col-sm-3 text-center">
                                       <asp:CheckBox ID="cbrbbpta18" runat="server" Text="" />
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                               <div class="row">
                             <div class="col-md-12">
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left">9. Gambar -gambar lokasi, tapak dan kiosk/gerai/premis perniagaan</label>
                                    <div class="col-sm-3 text-center">
                                       <asp:CheckBox ID="cbggltdk9" runat="server" Text="" /></center>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left">19. Rancangan Perniagaan mengikut format yang disediakan </label>
                                    <div class="col-sm-3 text-center">
                                       <asp:CheckBox ID="cbrpmfyd19" runat="server" Text="" />
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                               <div class="row">
                             <div class="col-md-12">
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left">10. Peta lokasi, tapak dan kiosk/gerai/premis perniagaan</label>
                                    <div class="col-sm-3 text-center">
                                       <asp:CheckBox ID="cbpltdk10" runat="server" Text="" />
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left">20. Telah menjadi anggota koperasi dan jumlah saham Koperasi minimum RM 1000 </label>
                                    <div class="col-sm-3 text-center">
                                       <asp:CheckBox ID="cbtmakdjs20" runat="server" Text="" />
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                             <div class="row">
                             <div class="col-md-12">
                                 <div class="col-md-6 box-body">
                               &nbsp;
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left">21. Salinan Slip Gaji 3 bulan Penjamin (jika menggunakan Jaminan Penjamin Tetap) </label>
                                    <div class="col-sm-3 text-center">
                                       <asp:CheckBox ID="cbssgbp21" runat="server" Text="" />
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                               <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Latar Belakang Pemohon</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">                                                                        
                                    <label for="inputEmail3" class="col-sm-3 control-label">Status Muflis</label>
                                  <div class="col-sm-8">
                                      <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <div class="col-md-3 col-sm-4">
                                                                <asp:RadioButton ID="radiostssmm" runat="server" Text=" Muflis" GroupName="group1" />
                                                                <%--  <label>Warganegara</label>--%>
                                                            </div>
                                                            <div class="col-md-6 col-sm-5">
                                                                <asp:RadioButton ID="radiostssmtm" runat="server" Text=" Tidak Muflis" GroupName="group1" />
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">                                                                        
                                     <label for="inputEmail3" class="col-sm-3 control-label">Jika Muflis, Nyatakan </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txtjmn" runat="server" class="form-control uppercase" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">                                                                        
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tindakan Undang-Undang</label>
                                  <div class="col-sm-8">
                                       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <div class="col-md-3 col-sm-4">
                                                                <asp:RadioButton ID="radiotuuy" runat="server" Text=" Ya" GroupName="group2" />
                                                            </div>
                                                            <div class="col-md-3 col-sm-5">
                                                                <asp:RadioButton ID="radiotuut" runat="server" Text=" Tidak" GroupName="group2" />
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">                                                                        
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jika Ya, Nyatakan </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txtjyn" runat="server" class="form-control uppercase" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">                                                                        
                                    <label for="inputEmail3" class="col-sm-3 control-label">Status Anggota</label>
                                  <div class="col-sm-8">
                                       <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                        <ContentTemplate>
                                                            <div class="col-md-3 col-sm-4">
                                                                <asp:RadioButton ID="radiosaa" runat="server" Text=" Aktif" GroupName="group3" />
                                                                <%--  <label>Warganegara</label>--%>
                                                            </div>
                                                            <div class="col-md-4 col-sm-5">
                                                                <asp:RadioButton ID="radiostsata" runat="server" Text=" Tidak Aktif" GroupName="group3" />
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                                 
                                 </div>
                                 </div>
                            <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Sejarah Pembiayaan Pemohon</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">                                                                        
                                    <label for="inputEmail3" class="col-sm-3 control-label">Sahabat AIM</label>
                                  <div class="col-sm-8">
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                        <ContentTemplate>
                                                            <div class="col-md-3 col-sm-4">
                                                                <asp:RadioButton ID="radiosaimy" runat="server" Text=" Ya" GroupName="group4" />
                                                                <%--  <label>Warganegara</label>--%>
                                                            </div>
                                                            <div class="col-md-3 col-sm-5">
                                                                <asp:RadioButton ID="radiosaimta1" runat="server" Text=" Tidak" GroupName="group4" />
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">                                                                        
                                    <label for="inputEmail3" class="col-sm-3 control-label">Anggota Koperasi</label>
                                  <div class="col-sm-8">
                                       <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                        <ContentTemplate>
                                                            <div class="col-md-3 col-sm-4">
                                                                <asp:RadioButton ID="radioaks" runat="server" Text=" Ya" GroupName="group5" />
                                                                <%--  <label>Warganegara</label>--%>
                                                            </div>
                                                            <div class="col-md-3 col-sm-5">
                                                                <asp:RadioButton ID="radioakta" runat="server" Text=" Tidak" GroupName="group5" />
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                              <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Pembiayaan Dengan Institusi Kewangan Lain (Termasuk AIM)</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">                                                                        
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama Institusi Kewangan 1</label>
                                  <div class="col-sm-8">
                                         <asp:TextBox ID="txtnik1" runat="server" class="form-control validate[optional,custom[textSp]] uppercase"
                                                            MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">                                                                        
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jumlah Pembiayaan (RM) 1</label>
                                  <div class="col-sm-8">
                                        <asp:TextBox ID="txtjp1" runat="server" class="form-control validate[optional,custom[number]]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">                                                                        
                                    <label for="inputEmail3" class="col-sm-3 control-label">Ansuran Bulanan (RM) 1 :</label>
                                  <div class="col-sm-8">
                                        <asp:TextBox ID="txtab1" runat="server" class="form-control validate[optional,custom[number]] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                            <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">                                                                        
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama Institusi Kewangan 2</label>
                                  <div class="col-sm-8">
                                       <asp:TextBox ID="txtnik2" runat="server" class="form-control validate[optional,custom[textSp]] uppercase"
                                                            MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">                                                                        
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jumlah Pembiayaan (RM) 2</label>
                                  <div class="col-sm-8">
                                        <asp:TextBox ID="txtjp2" runat="server" class="form-control validate[optional,custom[number]] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">                                                                        
                                    <label for="inputEmail3" class="col-sm-3 control-label">Ansuran Bulanan (RM) 2 :</label>
                                  <div class="col-sm-8">
                                        <asp:TextBox ID="txtab2" runat="server" class="form-control validate[optional,custom[number]] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">                                                                        
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama Institusi Kewangan 3</label>
                                  <div class="col-sm-8">
                                        <asp:TextBox ID="txtnik3" runat="server" class="form-control validate[optional,custom[textSp]] uppercase"
                                                            MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">                                                                        
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jumlah Pembiayaan (RM) 3</label>
                                  <div class="col-sm-8">
                                        <asp:TextBox ID="txtjp3" runat="server" class="form-control validate[optional,custom[number]]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">                                                                        
                                    <label for="inputEmail3" class="col-sm-3 control-label">Ansuran Bulanan (RM) 3 :</label>
                                  <div class="col-sm-8">
                                        <asp:TextBox ID="txtab3" runat="server" class="form-control validate[optional,custom[number]]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                <asp:Button ID="Button2" runat="server" class="btn btn-danger" Text="Simpan" OnClick="btnsmmit_Click" />
                                                        <asp:Button ID="Button1" runat="server" class="btn btn-danger" Text="Kemaskini" OnClick="btnkems_Click" />
                                                        <asp:Button ID="Button6" runat="server" class="btn btn-warning" Text="Cetak" OnClick="btnpt_Click" />
                                                        <asp:Button ID="btnreset" runat="server" class="btn btn-default" Text="Set Semula"
                                                            OnClick="btnreset_Click" />
                                                        <asp:Button ID="Button3" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" OnClick="clk_bak" />
                                 
                            </div>
                           </div>
                               </div>
                                     <div class="row">
                                                        <div class="col-md-12 col-sm-2" style="text-align: center">
                                                           <rsweb:ReportViewer ID="Rptviwer_lt" runat="server"></rsweb:ReportViewer>
                                     <asp:Label runat="server" ID="ReportErrorMessage" Visible="false" CssClass="report-error-message"></asp:Label>
                                                        </div>
                                                    </div>
                                    
                           <div class="box-body">&nbsp;</div>
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



