<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Kewengan/kw_pelanggan.aspx.cs" Inherits="kw_pelanggan" %>
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
                        <h1><asp:Label ID="ps_lbl1" runat="server"></asp:Label></h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i><asp:Label ID="ps_lbl2" runat="server"></asp:Label></a></li>
                            <li class="active"><asp:Label ID="ps_lbl3" runat="server"></asp:Label></li>
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
                            <h3 class="box-title"><asp:Label ID="ps_lbl4" runat="server"></asp:Label></h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl5" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox5" runat="server" class="form-control validate[optional] uppercase" OnTextChanged="katcd_TextChanged" AutoPostBack="True" MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl6" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox1" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl7" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox7" runat="server" class="form-control validate[optional] uppercase" MaxLength="1000"></asp:TextBox>                                                        
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl8" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                           <asp:TextBox ID="TextBox4" runat="server" class="form-control validate[optional,custom[phone]] uppercase" MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl9" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox11" runat="server" class="form-control validate[optional] uppercase" MaxLength="1000"></asp:TextBox>                                                        
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl10" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                    <asp:TextBox ID="TextBox2" runat="server" class="form-control validate[optional,custom[phone]] uppercase" MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl11" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="TextBox9" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>                                                        
                                    </div>
                                </div>
                            </div>
                           
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl12" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox6" runat="server" class="form-control validate[optional] uppercase" MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl13" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddnegeri" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" >
                                                                                </asp:DropDownList>  
                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl14" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="TextBox8" runat="server" class="form-control validate[optional] uppercase" MaxLength="30"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl15" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="TextBox14" runat="server" class="form-control validate[optional,custom[number]] uppercase" MaxLength="5"></asp:TextBox>                                                        
                                    </div>
                                </div>
                            </div>
                           
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl16" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                      <asp:DropDownList ID="dd_akaun" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" OnSelectedIndexChanged="sel_jenis" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl17" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="dd_negera" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                                </asp:DropDownList> 
                                    </div>
                                </div>
                            </div>
                           
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl18" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox10" runat="server" class="form-control validate[optional] uppercase" MaxLength="20"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                            <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl19" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                            <div class="input-group">
                                   <asp:TextBox ID="TextBox3" runat="server" class="form-control validate[optional,custom[email]]"></asp:TextBox>
                                                <span class="input-group-addon" > <i class="fa fa-envelope"></i></span>
                                                </div>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl20" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="dd_kodind" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                             <div class="row">
                             <div class="col-md-12">
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">Nama Bank</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="DropDownList1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">Bank Akaun No</label>
                                    <div class="col-sm-8">
                                   <asp:TextBox ID="TextBox12" runat="server" class="form-control validate[optional]" MaxLength="20"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                             <div class="row">
                             <div class="col-md-12">
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">Kredit Limit (RM)</label>
                                    <div class="col-sm-8">
                                   <asp:TextBox ID="TextBox13" runat="server" style="text-align:right;" class="form-control validate[optional]" MaxLength="20"></asp:TextBox>
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
                                 <asp:Button ID="Button4" runat="server" class="btn btn-danger" Text="Simpan" OnClick="clk_submit" UseSubmitBehavior="false" />
                                <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" UseSubmitBehavior="false" OnClick="Button5_Click" />
                                <asp:Button ID="Button5" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" OnClick="Click_bck" />
                                 
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

