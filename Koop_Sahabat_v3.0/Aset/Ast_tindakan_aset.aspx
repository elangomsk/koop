﻿<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Aset/Ast_tindakan_aset.aspx.cs" Inherits="Ast_tindakan_aset" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
      <script>
     $(function () {
         $('.select2').select2();
     });
</script> 
      <script type="text/javascript">
   

    $(function () {
        $("#<%=TextBox5.ClientID%>").timepicker({
            showInputs: false //Set showInputs to false
        });
    })
</script>
<script type="text/javascript">
    function addTotal_bk1() {

        var amt1 = Number($("#<%=TextBox26.ClientID %>").val());

        $(".au_amt1").val(amt1.toFixed(2));

    }
    function addTotal_bk2() {

        var amt2 = Number($("#<%=TextBox11.ClientID %>").val());

        $(".au_amt2").val(amt2.toFixed(2));

    }
    function addTotal_bk3() {

        var amt3 = Number($("#<%=TextBox17.ClientID %>").val());

        $(".au_amt3").val(amt3.toFixed(2));

    }
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
                        <h1>Pendaftaran Kehilangan Aset</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Pengurusan Aset</a></li>
                            <li class="active">Pendaftaran Kehilangan Aset</li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
      
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Kehilangan Aset Kakitangan</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                         <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div style="pointer-events: none;">
                              <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Aduan</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox1" runat="server" class="form-control validate[optional]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Aduan </label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                         <asp:TextBox ID="TextBox2" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                    placeholder="DD/MM/YYYY"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Kejadian</label>
                                    <div class="col-sm-8">
                                      <div class="input-group">
                                    <asp:TextBox ID="TextBox3" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                    placeholder="DD/MM/YYYY"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Masa Kejadian </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox5" data-provide="timepicker" class="form-control timepicker"
                                                    runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tempat Kejadian</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox4" runat="server" class="form-control validate[optional] uppercase"
                                                    MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Laporan polis </label>
                                    <div class="col-sm-8">
                                    <asp:TextBox ID="TextBox6" runat="server" class="form-control validate[optional] uppercase"
                                                    MaxLength="30"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <div class="row">
                             <div class="col-md-12">
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Keterangan Aduan </label>
                                    <div class="col-sm-8">
                                       <textarea id="txt_ar1" runat="server" class="form-control uppercase" rows="3" maxlength="2000"></textarea>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Laporan polis</label>
                                    <div class="col-sm-8">
                                      <div class="input-group">
                                     <asp:TextBox ID="TextBox8" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                    placeholder="DD/MM/YYYY"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Orang Disyaki (jika ada) </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox7" runat="server" class="form-control validate[optional] uppercase"
                                                    MaxLength="100"></asp:TextBox>
                                        <asp:TextBox ID="TextBox13" Visible="false" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                    <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Pengesahan Penyelia</h3>
                        </div>
                             <div class="box-body">&nbsp;</div>
                              <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Status Pengesahan</label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="dd_list" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                              <asp:ListItem Value="">--- PILIH ---</asp:ListItem>
                                            <asp:ListItem Value="01">SAH</asp:ListItem>
                                            <asp:ListItem Value="02">TIDAK SAH</asp:ListItem>
                                              </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama Penyelia </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox9" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>    
                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Catatan </label>
                                    <div class="col-sm-8">
                                        <textarea id="Textarea1" runat="server" class="form-control uppercase" rows="3" maxlength="2000"></textarea>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Pengesahan </label>
                                    <div class="col-sm-8">
                                      <div class="input-group">
                                      <asp:TextBox ID="TextBox10" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>    
                                </div>    
                             <div class="box-header with-border">
                            <h3 class="box-title">Tindakan Unit Aset</h3>
                        </div>
                             <div class="box-body">&nbsp;</div>
                              <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nilai Belian (RM)</label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="TextBox26" runat="server" class="form-control validate[optional,custom[number]] au_amt1"  onblur="addTotal_bk1(this)"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nilai Semasa (RM) </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox11" runat="server" class="form-control validate[optional,custom[number]] au_amt2"  onblur="addTotal_bk2(this)"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>   
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Belian</label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                      <asp:TextBox ID="TextBox14" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Usia (Tahun) </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox15" runat="server" class="form-control validate[optional]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>    
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tindakan</label>
                                    <div class="col-sm-8">
                                          <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                        <ContentTemplate>
                                    <div class="col-md-6 col-sm-3">
                                    <asp:RadioButton ID="RB31" runat="server"  Text=" Bayar Balik"  AutoPostBack="true" oncheckedchanged="RB1_CheckedChanged"/>   
                                    </div>
                                    <div class="col-md-6 col-sm-3">                                                         
                                    <asp:RadioButton ID="RB32" runat="server" Text=" Hapus Kira"  AutoPostBack="true" oncheckedchanged="RB2_CheckedChanged" />
                                    </div>
                                    </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Amaun Bayar Balik (RM) </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox17" runat="server" class="form-control validate[optional,custom[number]] au_amt3"  onblur="addTotal_bk3(this)"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div> 
                              <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Syor & Ulasan</label>
                                    <div class="col-sm-8">
                                        <textarea id="Textarea2" runat="server" class="form-control uppercase" rows="3" maxlength="1000"></textarea>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>  
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                <asp:TextBox ID="lbl_name" runat="server" class="form-control validate[optional] uppercase" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="get_id" Visible="false" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                              <asp:Button ID="Button1" runat="server" class="btn btn-warning" Text="Papar Laporan Polis" OnClick="DownloadFile" UseSubmitBehavior="false" />
                                                
                                                    <asp:Button ID="Button4" runat="server" class="btn btn-danger" Text="Simpan" OnClick="insert_values" UseSubmitBehavior="false"/>
                                <asp:Button ID="Button5" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" OnClick="Click_bck" />
                            </div>
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
         <Triggers>
             <asp:PostBackTrigger ControlID="Button1" />
</Triggers>
    </asp:UpdatePanel>
</asp:Content>
