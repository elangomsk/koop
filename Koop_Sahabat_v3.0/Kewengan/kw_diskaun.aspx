<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Kewengan/kw_diskaun.aspx.cs" Inherits="kw_diskaun" %>
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
                        <h1><asp:Label ID="ps_lbl1" Visible="false" runat="server"></asp:Label> Diskaun</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i><asp:Label ID="ps_lbl2" Visible="false" runat="server"></asp:Label>Kewangan</a></li>
                            <li class="active"><asp:Label ID="ps_lbl3" Visible="false" runat="server"></asp:Label> Diskaun</li>
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
                            <h3 class="box-title"><asp:Label ID="ps_lbl4" Visible="false" runat="server"></asp:Label> Maklumat Diskaun</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                         <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl5" Visible="false" runat="server"></asp:Label>Kod Diskaun <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox2" runat="server" class="form-control validate[optional] uppercase" OnTextChanged="katcd_TextChanged" AutoPostBack="True" MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl6"  runat="server"></asp:Label>  </label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                      <asp:TextBox ID="TextBox5" runat="server" class="form-control validate[optional] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl7" Visible="false" runat="server"></asp:Label>Nama Diskaun <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="TextBox1" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl8" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                           <div class="input-group">
                                            <asp:TextBox ID="TextBox6" runat="server" class="form-control validate[optional] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl9" runat="server"></asp:Label> <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList id="dd_akaun" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                       </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl10" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="TextBox7" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body" style="display:none;">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl11" runat="server"></asp:Label> (%) </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox3" runat="server" class="form-control validate[optional,custom[number]] uppercase" MaxLength="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                           <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl15" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                     <textarea id="alamat_akn" runat="server" class="form-control validate[optional] uppercase" rows="3"></textarea>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body" style="display:none;">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl12" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList id="DropDownList1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                       </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl13" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                      <asp:DropDownList id="DropDownList3" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                            <asp:ListItem value="">--- PILIH ---</asp:ListItem>
                                                           <asp:ListItem value="PEN">PENERIMAAN</asp:ListItem>
                                                           <asp:ListItem value="PEM">PEMBAYARAN</asp:ListItem>
                                                           <%--<asp:ListItem value="OTH">OTHERS</asp:ListItem>--%>
                                                       </asp:DropDownList>
                                    </div>
                                </div>
                                        <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl14" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="sts" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
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

