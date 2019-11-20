<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../Pelaburan_Anggota/PP_pen_awal.aspx.cs" Inherits="PP_pen_awal" %>

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
                        <h1> Penyelesaian Awal</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i> Pelaburan Anggota </a></li>
                            <li class="active">Penyelesaian Awal</li>
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
                            <h3 class="box-title">Maklumat Pelanggan</h3>
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
                             <div class="row">
                             <div class="col-md-12">
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jumlah Belian (RM)</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="txtjumla" runat="server" style="text-align:right;" class="form-control "></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                    <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Akhir Bayar Balik</label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                         <asp:TextBox ID="TextBox7" runat="server" class="form-control"></asp:TextBox>
                                               <span class="input-group-addon" ><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                 <%-- <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Cawangan / Jabatan</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtcaw" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>   --%>                             
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
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jenis Pemulihan</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="TextBox6" runat="server" class="form-control validate[optional,custom[number]]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
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
                            </div>
                           </div>
                               </div>
                              <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Permohonan Penyelesaian Awal </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                            <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Jangkaan Penyelesaian 1 <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                    <asp:TextBox ID="TextBox8" runat="server" class="form-control datepicker mydatepickerclass"></asp:TextBox>
                                             <span class="input-group-addon" ><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Jangkaan Penyelesaian 2 <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                          <div class="col-sm-10">
                                         <div class="input-group">
                                       <asp:TextBox ID="TextBox10" runat="server" class="form-control datepicker mydatepickerclass" placeholder="PICK DATE"></asp:TextBox>
                                              <span class="input-group-addon" ><i class="fa fa-calendar"></i></span>
                                             </div>
                                              </div>
                                          <div class="col-sm-2">
                                              <asp:Button ID="Kira" runat="server" class="btn btn-primary" Text="Kira" UseSubmitBehavior="false" onclick="Kira_Click" />
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Baki Tempoh Bayar 1 (Bulan)</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="TextBox11" runat="server" class="form-control "></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Baki Tempoh Bayar 2 (Bulan)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox12" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>                                
                                  </div>
                         </div>
                               <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jumlah Simpanan Koop (RM)</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="txttn16d" style="text-align:right;" runat="server" class="form-control "></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jumlah Simpanan Koop (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txttp" runat="server" style="text-align:right;" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>                                
                                  </div>
                         </div>
                      <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Rebat 1 (RM)</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="TextBox13" runat="server" style="text-align:right;" class="form-control "></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Rebat 2 (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox14" runat="server" style="text-align:right;" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>                                
                                  </div>
                         </div>
                              <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Deposit Sekuriti</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="TextBox15" runat="server" style="text-align:right;" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Deposit Sekuriti</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox16" runat="server" style="text-align:right;" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>                                
                                  </div>
                         </div>
                              <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Caj Pengeposan (RM)</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="TextBox1" runat="server" style="text-align:right;" class="form-control "></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Caj Pengeposan (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox9" runat="server" style="text-align:right;" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>                                
                                  </div>
                         </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Caj Lewat (RM)</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="TextBox21" runat="server" style="text-align:right;" class="form-control "></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Caj Lewat (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox22" runat="server" style="text-align:right;" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>                                
                                  </div>
                         </div>
                              <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Caj Guaman (RM)</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="TextBox17" runat="server" style="text-align:right;" class="form-control "></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Caj Guaman (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox18" runat="server" style="text-align:right;" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>                                
                                  </div>
                         </div>
                              <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Baki Penyelesaian Awal 1 (RM)</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="TextBox23" runat="server" style="text-align:right;" class="form-control "></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Baki Penyelesaian Awal 2 (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox24" runat="server" style="text-align:right;" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
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



