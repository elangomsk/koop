<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../Pelaburan_Anggota/PP_kem_kelulusan.aspx.cs" Inherits="PP_kem_kelulusan" %>

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
                        <h1>  Kemaskini Maklumat Kelulusan</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>  Pelaburan Anggota </a></li>
                            <li class="active">  Kemaskini Maklumat Kelulusan</li>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Permohonan <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="Icno" runat="server" class="form-control uppercase" MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                   <div class="col-sm-8">
                                     <asp:Button ID="Button4" runat="server" class="btn btn-primary" Text="Carian" usesubmitbehavior="false" OnClick="btnsrch_Click" />
                                                    <asp:Button ID="Button6" runat="server" class="btn btn-default" Text="Set Semula" OnClick="rst_clik" />
                                       <asp:Button ID="Button8" runat="server" class="btn btn-default" Text="Kembali"  UseSubmitBehavior="false" OnClick="clk_bak" />
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
                                        <asp:TextBox ID="MP_nama" runat="server" class="form-control validate[optional,custom[textSp]]"></asp:TextBox>
                                        <asp:TextBox ID="appage" Visible="false" runat="server" class="form-control validate[optional]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tujuan</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="MP_tujuan" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                          

                              <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Amaun Dipohon (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="MP_amaun" runat="server" style="text-align:right;" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tempoh (Bulan)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="MP_tempoh" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>

                              <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Kelulusan</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                             <div class="row">
                             <div class="col-md-12">
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Amaun Diluluskan (RM) </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="MK_amaun" style="text-align:right;" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tempoh Kelulusan (Bulan) </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="MK_tempoh" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">&nbsp;</div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">                                                                        
                                    <label for="inputEmail3" class="col-sm-3 control-label">Bilangan Penjamin <span style="color: Red">*</span></label>
                                     <div class="col-sm-8 text-left">
                                      <asp:TextBox ID="TxtBilangan" runat="server" class="form-control validate[optional,custom[number]]"></asp:TextBox>                                             
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                                <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Pengeluaran Dipersetujui Pemohon</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                              <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Amaun Pengeluaran (RM) <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="MPDP_amaun" style="text-align:right;" runat="server" class="form-control validate[optional]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tempoh Pengeluaran (Bulan) <span style="color: Red">*</span></label>
                                      <div class="col-sm-8 text-left">
                                         <div class="col-sm-9">
                                     <asp:TextBox ID="MPDP_tempoh" runat="server" class="form-control validate[required]" MaxLength="3"></asp:TextBox>
                                             </div>
                                       <div class="col-sm-3">
                                        <asp:Button ID="Button1" runat="server" class="btn btn-warning" UseSubmitBehavior="false" Visible="false" Text="Kira" OnClick="kira_Click" />
                                             </div>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>
                             <div class="box-header with-border">
                            <h3 class="box-title">Janaan FI, CAJ Dan Bayaran Bulanan</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                               <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Caj Duti Setem (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox2" runat="server" style="text-align:right;" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Premium Takaful (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox10" runat="server" style="text-align:right;" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>
                               <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Fi Pemprosesan (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox6" runat="server" style="text-align:right;" class="form-control validate[optional,custom[number]]" MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Fi Semakan Kredit (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox9" runat="server" style="text-align:right;" class="form-control validate[optional,custom[number]]" MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>
                              <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Deposit Sekuriti (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox5" runat="server" style="text-align:right;" class="form-control validate[optional,custom[number]]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Keuntungan (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox13" runat="server" style="text-align:right;"  class="form-control validate[optional,custom[number]] uppercase" MaxLength="12" ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>
                          
                               <div class="row">
                             <div class="col-md-12">
                                <%--  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Lain-lain Caj (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="Txtlain" runat="server" style="text-align:right;"  class="form-control validate[optional,custom[number]] uppercase" MaxLength="12" ></asp:TextBox>
                                    </div>
                                </div>
                            </div>--%>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Bayaran Bulanan (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox7" runat="server"  style="text-align:right;" class="form-control validate[optional,custom[number]] uppercase" MaxLength="12" ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>
                             <div class="box-header with-border">
                            <h3 class="box-title">Kemaskini Status Permohonan</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                               <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Keputusan Permohonan<span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                       <div class="col-md-6 col-sm-4">
                                                            <asp:RadioButton ID="RadioButton1" runat="server"  Text=" Lulus"  AutoPostBack="true" 
                                                                    oncheckedchanged="RadioButton1_CheckedChanged"/>                                                          
                                                            </div>
                                                            <div class="col-md-6 col-sm-5"> 
                                                            <asp:RadioButton ID="RadioButton2" runat="server" Text=" Tidak Lulus"  AutoPostBack="true" 
                                                                    oncheckedchanged="RadioButton2_CheckedChanged" />                                                          
                                                            </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Catatan</label>
                                    <div class="col-sm-8">
                                        <textarea id="KSP_catatan" runat="server" rows="3" class="form-control validate[optional,custom[onlyaddress]] uppercase" maxlength="250"></textarea>
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
                                                        <asp:Button ID="Button3" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" onclick="clk_bak" />
                                 
                            </div>
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



