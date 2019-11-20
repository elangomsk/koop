<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../Pelaburan_Anggota/PP_jkpa_alk.aspx.cs" Inherits="PP_jkpa_alk" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
         <script type="text/javascript">
     
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      
    <asp:ScriptManager ID="ScriptManagerCalendar" ScriptMode="Release" runat="server">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" style="height: 895px;">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>  Maklumat Mesyuarat JKPA / ALK</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>  Pelaburan Anggota </a></li>
                            <li class="active"> Maklumat Mesyuarat JKPA / ALK</li>
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
                                     <asp:TextBox ID="Icno" runat="server" class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase"
                                                            MaxLength="12"></asp:TextBox>
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
                                        <asp:TextBox ID="MP_nama" runat="server" class="form-control validate[optional,custom[textSp]]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Wilayah / Pejabat</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="MP_wilayah" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                                   
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Cawangan / Jabatan</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="MP_cawangan" runat="server" class="form-control uppercase"></asp:TextBox>
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
                            <h3 class="box-title">Ulasan ALK</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                              <div id="Div3" class="nav-tabs-custom col-md-12 box-body" role="tabpanel" runat="server">
                                  <ul class="s1 nav nav-tabs" role="tablist">
                                                                <li id="uj11" runat="server" class="active"><a href="#ContentPlaceHolder1_uj1" aria-controls="uj1" role="tab" data-toggle="tab"><strong>Ulasan  ALK 1</strong></a></li>
                                                                <li id="uj12" runat="server"><a href="#ContentPlaceHolder1_uj2" aria-controls="uj2" role="tab" data-toggle="tab"><strong>Ulasan  ALK 2</strong></a></li>
                                                                <li id="uj13" runat="server"><a href="#ContentPlaceHolder1_uj3" aria-controls="uj3" role="tab" data-toggle="tab"><strong>Ulasan  ALK 3</strong></a></li>
                                                                                                                               
                                                            </ul>
                                  <div class="box-body">&nbsp;</div>
                                     <div class="tab-content">
                                                             <div role="tabpanel" class="tab-pane active" runat="server" id="uj1">
                               <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Ulasan <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                        <textarea id="UJ_ulasan1" runat="server" rows="3" class="form-control validate[optional,custom[onlyaddress]] uppercase" maxlength="1000"></textarea>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>
                               <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="UJ_nama1" runat="server" class="form-control validate[optional,custom[textSp]] uppercase"
                                                                                    MaxLength="150"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jawatan <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="UJ_jawatan1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                       <asp:TextBox ID="UJ_tarikh1" runat="server" class="form-control validate[optional] datepicker mydatepickerclass mydatepickerclass1" placeholder="PICK DATE"></asp:TextBox>
                                          <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>
                                                                 </div>
                                          <div role="tabpanel" class="tab-pane" runat="server" id="uj2">
                               <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Ulasan <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                        <textarea id="UJ_ulasan2" runat="server" rows="3" class="form-control validate[optional,custom[onlyaddress]] uppercase" maxlength="1000"></textarea>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>
                               <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="UJ_nama2" runat="server" class="form-control validate[optional,custom[textSp]] uppercase"
                                                                                    MaxLength="150"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jawatan <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                      <asp:DropDownList ID="UJ_jawatan2" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                      <asp:TextBox ID="UJ_tarikh2" runat="server" class="form-control validate[optional] datepicker mydatepickerclass"
                                                                                    placeholder="PICK DATE"></asp:TextBox>
                                          <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>
                                                                 </div>
                                         <div role="tabpanel" class="tab-pane" runat="server" id="uj3">
                               <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Ulasan </label>
                                    <div class="col-sm-8">
                                        <textarea id="UJ_ulasan3" runat="server" rows="3" class="form-control validate[optional,custom[onlyaddress]] uppercase" maxlength="1000"></textarea>
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
                                         <asp:TextBox ID="UJ_nama3" runat="server" class="form-control validate[optional,custom[textSp]] uppercase"
                                                                                    MaxLength="150"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jawatan </label>
                                    <div class="col-sm-8">
                                     <asp:DropDownList ID="UJ_jawatan3" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                     <asp:TextBox ID="UJ_tarikh3" runat="server" class="form-control validate[optional] datepicker mydatepickerclass"
                                                                                    placeholder="PICK DATE"></asp:TextBox>
                                          <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>
                                                                 </div>
                                         </div>
                                  </div>
                            <div class="row">
                                 <div class="col-md-12">
                             <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Kelulusan</h3>
                        </div>
                                     </div>
                                </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                               <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Bil Mesyuarat <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="jkkbil" runat="server" class="form-control validate[optional,custom[rkel]] uppercase" MaxLength="20"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jenis Mesyuarat</label>
                                    <div class="col-sm-8">
                                        <div class="col-md-4 col-sm-4">
                                                                        <asp:RadioButton ID="RadioButton3" runat="server" Text=" JKKPA" AutoPostBack="true"
                                                                            OnCheckedChanged="RadioButton3_CheckedChanged" />
                                                                        <%--  <label>Warganegara</label>--%>
                                                                    </div>
                                                                    <div class="col-md-4 col-sm-4">
                                                                        <asp:RadioButton ID="RadioButton1" runat="server" Text=" JKPA" AutoPostBack="true"
                                                                            OnCheckedChanged="RadioButton1_CheckedChanged" />
                                                                        <%--  <label>Warganegara</label>--%>
                                                                    </div>
                                                                    <div class="col-md-4 col-sm-5">
                                                                        <asp:RadioButton ID="RadioButton2" runat="server" Text=" ALK" AutoPostBack="true"
                                                                            OnCheckedChanged="RadioButton2_CheckedChanged" />
                                                                        <%--  <label>Bukan Warganegara</label>--%>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Amaun Diluluskan (RM) </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="MK_amaun" style="text-align:right;" runat="server" class="form-control validate[optional,custom[number]]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tempoh Kelulusan (Bulan)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="MK_tempoh" runat="server" class="form-control validate[optional,custom[number]]" MaxLength="3"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  
                                 
                                  </div>
                         </div>
                           
                              <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Mesyuarat <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                    <asp:TextBox ID="KJ_tarikh" runat="server" class="form-control validate[optional] datepicker mydatepickerclass"
                                                                    placeholder="PICK DATE"></asp:TextBox>
                                          <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Catatan </label>
                                    <div class="col-sm-8">
                                        <textarea id="catatan" runat="server" rows="3" class="form-control validate[optional] uppercase" maxlength="1000"></textarea>
                                    </div>
                                </div>
                            </div>
                                   
                                  </div>
                         </div>
                            
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                                        <%--<asp:Button ID="Button7" Visible="false" runat="server" class="btn btn-warning" Text="Cetak Templat" UseSubmitBehavior="false" OnClick="click_template" />--%>
                                                        <asp:Button ID="Button2" runat="server" class="btn btn-danger" Text="Simpan" OnClick="btnsmmit_Click" />                                                        
                                                        <asp:Button ID="Button5" Visible="false" runat="server" class="btn btn-warning" Text="Cetak" UseSubmitBehavior="false" OnClick="cetak_Click" />                                                        
                                                        <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" UseSubmitBehavior="false" OnClick="btnrst_click" />                                                        
                                                        <asp:Button ID="Button3" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" OnClick="clk_bak" />
                                 
                            </div>
                           </div>
                               </div>
                             <div class="row">
                                                        <div class="col-md-12 col-sm-2" style="text-align: center">
                                                            <rsweb:ReportViewer ID="Rptviwer_kelulusan" runat="server">
                                                            </rsweb:ReportViewer>
                                                            <asp:Label runat="server" ID="ReportErrorMessage" Visible="false" CssClass="report-error-message"></asp:Label>
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



