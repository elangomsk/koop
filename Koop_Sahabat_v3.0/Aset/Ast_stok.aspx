<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Aset/Ast_stok.aspx.cs" Inherits="Ast_stok" %>
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
                        <h1>Keterangan Aset</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Kewangan</a></li>
                            <li class="active">Keterangan Aset</li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
      
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Keterangan Aset</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                         <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Kategori </label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="DD_Kategori" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional] uppercase" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Sub Kategori</label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="DD_Sub_Kateg" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional] uppercase" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged1">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jenis Aset </label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="DD_Jenis_ast" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional] uppercase">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Kod Aset</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="DD_NAMAAST" runat="server" class="form-control validate[optional,minSize[6]] uppercase" MaxLength="6"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Keterangan</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txt_Keterangan" runat="server" class="form-control validate[optional] uppercase" MaxLength="150"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox1" runat="server" class="form-control validate[optional] uppercase"
                                                            Visible="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                           <%-- <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Status</label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="DropDownList2" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional] uppercase">
                                                                    <asp:ListItem Text="Aktif" Value="A" />
                                                                    <asp:ListItem Text="Tidak Aktif" Value="T" />
                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>--%>

                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                 <asp:TextBox ID="lbl_name" runat="server" class="form-control validate[optional] uppercase" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="ver_id" Visible="false" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                                                         <asp:TextBox ID="get_id" Visible="false" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                                 <asp:Button ID="Button4" runat="server" class="btn btn-danger" Text="Simpan" OnClick="clk_submit" UseSubmitBehavior="false" />
                                <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" UseSubmitBehavior="false" OnClick="Button5_Click" />
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

