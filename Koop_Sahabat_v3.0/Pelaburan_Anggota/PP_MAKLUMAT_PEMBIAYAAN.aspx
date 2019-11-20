<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../Pelaburan_Anggota/PP_MAKLUMAT_PEMBIAYAAN.aspx.cs" Inherits="PP_MAKLUMAT_PEMBIAYAAN" %>

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
                        <h1> Permohonan Hapus Kira Pembiayaan</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i> Pelaburan Anggota </a></li>
                            <li class="active">Permohonan Hapus Kira Pembiayaan</li>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Kata Kunci Carian <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="Applcn_no" runat="server" class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase"
                                                            MaxLength="12"></asp:TextBox>
                                         <asp:Panel ID="autocompleteDropDownPanel" runat="server" ScrollBars="Auto" Height="150px"
                                                            Font-Size="Medium" HorizontalAlign="Left" Wrap="False" />
                                                        <cc1:AutoCompleteExtender ServiceMethod="GetCompletionList" MinimumPrefixLength="1"
                                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="Applcn_no"
                                                            ID="AutoCompleteExtender1" CompletionListElementID="autocompleteDropDownPanel"
                                                            CompletionListCssClass="form-control uppercase" runat="server" FirstRowSelected="false">
                                                        </cc1:AutoCompleteExtender>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                   <div class="col-sm-8">
                                    <asp:Button ID="Button9" runat="server" class="btn btn-primary" Text="Carian" usesubmitbehavior="false" OnClick="btnsrch_Click" />
                                                    <asp:Button ID="Button10" runat="server" class="btn btn-default" Text="Set Semula" usesubmitbehavior="false" OnClick="btn_rstclick"/>                                     
                                       
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jumlah Belian (RM)</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="txtjumla" runat="server" style="text-align:right;" class="form-control "></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 <%-- <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Wilayah / Pejabat</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="txtwil" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>--%>
                                 </div>
                                </div>
                            
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jumlah Kumulatif Kena (RM)</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="txtamaun" runat="server" style="text-align:right;" class="form-control "></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tempoh (Bulan)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txttempoh" runat="server" class="form-control uppercase" MaxLength="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>                                
                                  </div>
                         </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jumlah Kumulatif Tunggakan (RM)</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="TextBox2" runat="server" style="text-align:right;" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jumlah Kumulatif Bayar (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txttemp" runat="server" style="text-align:right;" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>                                
                                  </div>
                         </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jumlah Kumulatif Simpanan (RM)</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="TextBox4" runat="server" style="text-align:right;" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jumlah Untung (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox3" runat="server" style="text-align:right;" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>                                
                                  </div>
                         </div>
                             <div class="row">
                             <div class="col-md-12">
                                 <%-- <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jenis Pemulihan</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="TextBox6" runat="server" class="form-control validate[optional,custom[number]]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>--%>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Baki Kumulatif Pelaburan (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox5" runat="server" style="text-align:right;" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>                                
                                  </div>
                         </div>
                             
                              <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                              <asp:Button ID="Button3" runat="server" class="btn btn-warning" UseSubmitBehavior="false" Text="Semak JBB" OnClick="click_jbb"/>                                                        
                                                        <asp:Button ID="Button4" runat="server" class="btn btn-warning" UseSubmitBehavior="false" Text="Litigasi Bercagar" OnClick="clk_Bercagar"/>                                                        
                                                        <asp:Button ID="Button5" runat="server" class="btn btn-warning" UseSubmitBehavior="false" Text="Litigasi Penjamin" onclick="clk_Penjamin"/>
                                <asp:Button ID="Button2" runat="server" class="btn btn-default" Text="Kembali" usesubmitbehavior="false" OnClick="clk_bak"/>                                     
                            </div>
                           </div>
                               </div>
                              <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Hapus Kira Pembiayaan </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                            <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jenis Hapus Kira <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                        <div class="col-sm-6">
                                       <asp:TextBox ID="jhk" runat="server" class="form-control validate[optional, custom[number]]"  onblur="addTotal_bk(this)" MaxLength="3"></asp:TextBox>
                                            </div>
                                        <div class="col-sm-6">
                                             % Dari Baki Pembiayaan
                                            </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Amaun Hapus Kira (RM)</label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="ahk" runat="server" style="text-align:right;" class="form-control validate[optional, custom[number]] sum_akb" MaxLength="10"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>
                              <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Status <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                           <asp:DropDownList runat="server" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" ID="k_status">
                                                                 <asp:ListItem Value="">--- PILIH ---</asp:ListItem>
                                                                 <asp:ListItem Value="01">PERMOHONAN</asp:ListItem>
                                                                 <asp:ListItem Value="02">BATAL PERMOHONAN</asp:ListItem>
                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Catatan <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                        <textarea id="catatan" runat="server" rows="3" class="form-control uppercase" maxlength="500"></textarea>
                                    </div>
                                </div>
                            </div>                                
                                  </div>
                         </div>
                                      <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                              <asp:Button ID="Button6" runat="server" class="btn btn-danger" Text="Simpan" OnClick="btn_click" UseSubmitBehavior="false"/>                                                        
                                                        <asp:Button ID="Button1" runat="server" class="btn btn-warning" Visible="false" Text="Cetak" OnClick="Cetakhapus_Click" UseSubmitBehavior="false"/>                                                        
                                                        <asp:Button ID="Button7" runat="server" class="btn btn-default" Text="Set Semula" UseSubmitBehavior="false" OnClick="btn_rstclick1"/>                                                        
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



