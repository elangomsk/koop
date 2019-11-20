<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../Kewengan/kw_jenis_jurnal.aspx.cs" Inherits="kw_jenis_jurnal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>Jenis Jurnal</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i><asp:Label ID="ps_lbl2" runat="server"></asp:Label></a></li>
                            <li class="active">Jenis Jurnal</li>
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
                            <h3 class="box-title">Maklumat Jenis Jurnal</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                         <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> kod <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txt_jenis" MaxLength="100" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jenis Jurnal <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox1" MaxLength="100" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                        
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            

                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jenis Akaun</label>

                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="DropDownList1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                    <asp:ListItem Text="Debit" Value="D" />
                                                                    <asp:ListItem Text="Kredit" Value="K" />
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Status</label>

                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="dd_list_sts" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                    <asp:ListItem Text="Aktif" Value="A" />
                                                                    <asp:ListItem Text="Tidak Aktif" Value="T" />
                                                                </asp:DropDownList>

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
                                 <asp:Button ID="Button4" runat="server" class="btn btn-danger" Text="Simpan" OnClick="Button4_Click" UseSubmitBehavior="false" />
                                <asp:Button ID="btb_kmes" runat="server" Text="Kemaskini" UseSubmitBehavior="false" class="btn btn-danger" OnClick="btb_kmes_Click" />
                                <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" UseSubmitBehavior="false" OnClick="Button5_Click" />
                                <asp:Button ID="Button2" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" OnClick="Click_bck" />
                                 <asp:TextBox ID="gen_id" runat="server" Visible="false"></asp:TextBox>
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
              </ContentTemplate>
    </asp:UpdatePanel>
        
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
  
</asp:Content>

