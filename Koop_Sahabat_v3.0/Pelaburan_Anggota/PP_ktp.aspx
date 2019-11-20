<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../Pelaburan_Anggota/PP_ktp.aspx.cs" Inherits="PP_ktp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      
    <asp:ScriptManager ID="ScriptManagerCalendar" ScriptMode="Release" runat="server">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" style="height: 895px;">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>  Kemaskini Maklumat Tawaran / Perjanjian</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>  Pelaburan Anggota </a></li>
                            <li class="active">  Kemaskini Maklumat Tawaran / Perjanjian</li>
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
                                     <asp:TextBox ID="txtnokp" runat="server" class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase"
                                                            MaxLength="12"></asp:TextBox>
                                           <asp:Panel ID="autocompleteDropDownPanel" runat="server" ScrollBars="Auto" Height="150px"
                                                            Font-Size="Medium" HorizontalAlign="Left" Wrap="False" />
                                                        <cc1:AutoCompleteExtender ServiceMethod="GetCompletionList" MinimumPrefixLength="1"
                                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtnokp"
                                                            ID="AutoCompleteExtender1" CompletionListElementID="autocompleteDropDownPanel"
                                                            CompletionListCssClass="form-control uppercase" runat="server" FirstRowSelected="false">
                                                        </cc1:AutoCompleteExtender>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                   <div class="col-sm-8">
                                     <asp:Button ID="Button4" runat="server" class="btn btn-primary" Text="Carian" usesubmitbehavior="false" OnClick="btnsrch_Click" />
                                                    <asp:Button ID="Button6" runat="server" class="btn btn-default" Text="Set Semula" OnClick="btnreset_Click" />
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
                                        <asp:TextBox ID="txtnama" runat="server" class="form-control validate[optional,custom[textSp]]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tujuan</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="Txttuj" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                <%--   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Wilayah / Pejabat</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="txtwil" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>--%>
                                 </div>
                                </div>
                          <%--   <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Cawangan / Jabatan</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="Txtcaw" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                
                                  </div>
                         </div>--%>
                              <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Amaun Pengeluaran (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TxtAmaun" runat="server" class="form-control validate[optional,custom[number]]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tempoh (Bulan)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TxtTem" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>
                              <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Pra-Pengeluaran</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                            <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Perjanjian <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                             <asp:TextBox ID="tar_per" runat="server" class="form-control validate[optional] datepicker mydatepickerclass" placeholder="PICK DATE"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Terimaan Surat Tawaran <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                        <asp:TextBox ID="TxtTarikh" runat="server" class="form-control validate[optional] datepicker mydatepickerclass" placeholder="PICK DATE"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Perjanjian Kemudahan <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                             <asp:TextBox ID="TxtTarTan" runat="server" class="form-control validate[optional] datepicker mydatepickerclass" placeholder="PICK DATE"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Perjanjian Penjamin 1 <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                        <asp:TextBox ID="TxtTarTanper1" runat="server" class="form-control validate[optional] datepicker mydatepickerclass" placeholder="PICK DATE"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Perjanjian Penjamin 2</label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                             <asp:TextBox ID="TxtTarTanPer2" runat="server" class="form-control validate[optional] datepicker mydatepickerclass" placeholder="PICK DATE"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Perjanjian Penjamin 3</label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                        <asp:TextBox ID="TxtTarTanPer3" runat="server" class="form-control validate[optioanl] datepicker mydatepickerclass" placeholder="PICK DATE"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Perjanjian Tambahan</label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                             <asp:TextBox ID="TxtTarTanPerTam" runat="server" class="form-control validate[optional] datepicker mydatepickerclass" placeholder="PICK DATE"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Duti Setem</label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                        <asp:TextBox ID="TxtTarDut" runat="server" class="form-control validate[optional] datepicker mydatepickerclass" placeholder="PICK DATE"></asp:TextBox>
                                             <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>
                              <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Semakan Pra-Pengeluaran</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-1">&nbsp;</div>
                                    <label class="col-sm-7 text-left">1. Bilangan Cek Tertunda Tarikh (Post Dated Cheque) yang lengkap </label>
                                    <div class="col-sm-3 text-center">
                                      <asp:CheckBox ID="chk1" runat="server" Text="" />
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
                                    <label class="col-sm-7 text-left">2. Tandatangan Surat Tawaran</label>
                                    <div class="col-sm-3 text-center">
                                      <asp:CheckBox ID="chk2" runat="server" Text="" />
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
                                    <label class="col-sm-7 text-left">3. Tandatangan Perjanjian Kemudahan</label>
                                    <div class="col-sm-3 text-center">
                                      <asp:CheckBox ID="chk3" runat="server" Text="" />
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
                                    <label class="col-sm-7 text-left">4. Tandatangan Perjanjian Penjamin 1 </label>
                                    <div class="col-sm-3 text-center">
                                       <asp:CheckBox ID="chk4" runat="server" Text="" />
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
                                    <label class="col-sm-7 text-left">5. Tandatangan Perjanjian Penjamin 2  </label>
                                    <div class="col-sm-3 text-center">
                                      <asp:CheckBox ID="chk5" runat="server" Text="" />
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
                                    <label class="col-sm-7 text-left">6. Tandatangan Perjanjian Penjamin 3 </label>
                                    <div class="col-sm-3 text-center">
                                       <asp:CheckBox ID="chk6" runat="server" Text="" />
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
                                    <label class="col-sm-7 text-left">7. Pengesahan Senarai Nombor Akaun Penerima</label>
                                    <div class="col-sm-3 text-center">
                                       <asp:CheckBox ID="chk7" runat="server" Text="" />
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
                                    <label class="col-sm-7 text-left">8. Telah melaksanakan proses jual beli komoditi di BSAS</label>
                                    <div class="col-sm-3 text-center">
                                       <asp:CheckBox ID="chk8" runat="server" Text="" />
                                    </div>
                                </div>
                            </div>
                                  
                                 </div>
                                 </div>
                              
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                <asp:Button ID="Button2" runat="server" class="btn btn-danger" Text="Simpan" UseSubmitBehavior="false" OnClick="btnsmmit_Click" />                                                        
                                                        <asp:Button ID="Button3" runat="server" class="btn btn-default" Text="Kembali"  UseSubmitBehavior="false" OnClick="clk_bak" />
                                 
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



