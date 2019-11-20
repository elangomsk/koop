<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Aset/Ast_pergerakan.aspx.cs" Inherits="Ast_pergerakan" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        
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
                        <h1>Selenggara Pergerakan Aset</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Pengurusan Aset</a></li>
                            <li class="active">Selenggara Pergerakan Aset</li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
      
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title">Carian Maklumat Aset</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                         <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                               <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Aset Id <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox1" runat="server" class="form-control validate[optional]" MaxLength="14"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-8">
                                      <asp:Button ID="btn_Show" runat="server" class="btn btn-danger" Text="Carian" UseSubmitBehavior="false"
                                                            OnClick="clk_carian" />
                                                          <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" UseSubmitBehavior="false" OnClick="Button5_Click" />
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Pergerakan Aset</h3>
                        </div>
                             <div class="box-body">&nbsp;</div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Kategori</label>
                                    <div class="col-sm-8">
                                                 <asp:TextBox ID="TextBox20" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox24" runat="server" Visible="false" class="form-control validate[optional] uppercase"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox27" runat="server" Visible="false" class="form-control validate[optional] uppercase"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox32" runat="server" Visible="false" class="form-control validate[optional] uppercase"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox33" runat="server" Visible="false" class="form-control validate[optional] uppercase"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox34" runat="server" Visible="false" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Sub Kategori </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox25" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jenis Aset</label>
                                    <div class="col-sm-8">                                        
                                                 <asp:TextBox ID="TextBox26" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama Aset </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox2" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox11" runat="server" Visible="false" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Aset ID</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox3" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jenis Pergerakan <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                      <asp:DropDownList ID="DropDownList2" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"
                                                            AutoPostBack="true" OnSelectedIndexChanged="slct_jp">
                                                            <asp:ListItem Value="">--- PILIH ---</asp:ListItem>
                                                            <asp:ListItem Value="01">Peminjaman</asp:ListItem>
                                                            <asp:ListItem Value="02">Pemulangan</asp:ListItem>
                                                            <asp:ListItem Value="03">Pindah Milik</asp:ListItem>
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Pemilikan Semasa Aset</h3>
                        </div>
                             <div class="box-body">&nbsp;</div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama Pegawai</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox7" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox28" runat="server" Visible="false" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jawatan </label>
                                    <div class="col-sm-8">
                                   <asp:TextBox ID="TextBox8" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                               <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Lokasi</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox9" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Pemulangan </label>
                                    <div class="col-sm-8">
                                   <div class="input-group">
                                        <asp:TextBox ID="TextBox10" runat="server" class="form-control validate[optional] datepicker mydatepickerclass"
                                                                placeholder="PICK DATE"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Pemilikan Baru / Peminjaman Aset</h3>
                        </div>
                             <div class="box-body">&nbsp;</div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama Pegawai</label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="DropDownList5" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"
                                                            OnSelectedIndexChanged="nam_peg" AutoPostBack="true">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Organisasi </label>
                                    <div class="col-sm-8">
                                <asp:TextBox ID="TextBox12" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox29" Visible="false" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jawatan</label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="TextBox13" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox30" Visible="false" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jabatan </label>
                                    <div class="col-sm-8">
                                <asp:TextBox ID="TextBox14" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox31" Visible="false" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Lokasi</label>
                                    <div class="col-sm-8">
                                                         <asp:DropDownList ID="DropDownList1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tujuan </label>
                                    <div class="col-sm-8">
                                                         <asp:TextBox ID="TextBox15" runat="server" class="form-control validate[optional] uppercase"
                                                            MaxLength="200"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Penempatan/Pinjaman Dari</label>
                                    <div class="col-sm-8">
                                                       <div class="input-group">
                                         <asp:TextBox ID="TextBox16" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                            placeholder="DD/MM/YYYY"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Penempatan/Pinjaman Sehingga </label>
                                    <div class="col-sm-8">
                                                        <div class="input-group">
                                        <asp:TextBox ID="TextBox17" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Masa Dari</label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                                        <asp:TextBox ID="TextBox18" CssClass="form-control timepicker" 
                                                            placeholder="PICK TIME" runat="server"></asp:TextBox>
                                            <div class="input-group-addon">
                      <i class="fa fa-clock-o"></i>
                    </div>
                  </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Masa Sehingga </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                                    <asp:TextBox ID="TextBox19" CssClass="form-control timepicker"
                                                            placeholder="PICK TIME" runat="server"></asp:TextBox>
                                        <div class="input-group-addon">
                      <i class="fa fa-clock-o"></i>
                    </div>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Catatan</label>
                                    <div class="col-sm-8">
                                                         <textarea id="txtarea1" runat="server" rows="3" class="form-control validate[optional] uppercase"
                                                            maxlength="1000"></textarea>
                                    </div>
                                </div>
                            </div>
                                 
                                 </div>
                                </div>
                             <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Pegawai Pengesah</h3>
                        </div>
                             <div class="box-body">&nbsp;</div>
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama Pegawai</label>
                                    <div class="col-sm-8">
                                                      <asp:TextBox ID="TextBox21" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jawatan </label>
                                    <div class="col-sm-8">
                                                    <asp:TextBox ID="TextBox22" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh</label>
                                    <div class="col-sm-8">
                                                        <div class="input-group">
                                         <asp:TextBox ID="TextBox23" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                            placeholder="DD/MM/YYYY"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Kondisi </label>
                                    <div class="col-sm-8">
                                         <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                        <ContentTemplate>
                                                         <div class="col-sm-2">
                                                                <asp:RadioButton ID="ch1" runat="server" class="mycheckbox" type="checkbox" Text="&nbsp;Baik"
                                                                    AutoPostBack="true" OnCheckedChanged="RB11_CheckedChanged" />
                                                                <%--  <label>Warganegara</label>--%>
                                                            </div>
                                                            <div class="col-sm-4">
                                                                <asp:RadioButton ID="ch2" runat="server" class="mycheckbox" type="checkbox" Text="&nbsp;Perlu Diselenggara"
                                                                    AutoPostBack="true" OnCheckedChanged="RB12_CheckedChanged" />
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <asp:RadioButton ID="ch3" runat="server" class="mycheckbox" type="checkbox" Text="&nbsp;Rosak"
                                                                    AutoPostBack="true" OnCheckedChanged="RB13_CheckedChanged" />
                                                            </div>
                                                            </ContentTemplate>
                                             </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Catatan</label>
                                    <div class="col-sm-8">
                                                      <textarea id="Textarea1" runat="server" rows="3" class="form-control validate[optional] uppercase"
                                                            maxlength="500"></textarea>
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
                                <asp:TextBox ID="ver_id" Visible="false" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                                                         <asp:TextBox ID="get_id" Visible="false" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                                   <asp:Button ID="Btn_Kemaskini" runat="server" class="btn btn-danger" UseSubmitBehavior="false"
                                                            Text="Simpan" OnClick="clk_simpan" />
                                <asp:Button ID="Button2" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" OnClick="Click_bck" />
                            </div>
                           </div>
                               </div>
                           
                            <div class="box-body">&nbsp;
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
    </asp:UpdatePanel>
</asp:Content>

