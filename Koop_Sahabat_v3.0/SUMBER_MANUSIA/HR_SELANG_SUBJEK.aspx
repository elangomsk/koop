<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_SELANG_SUBJEK.aspx.cs" Inherits="HR_SELANG_SUBJEK" %>

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
                        <h1 id="h1_tag" runat="server"> Maklumat Subjek  </h1>
                        <ol class="breadcrumb">
                            <li id="bb1_text" runat="server"><a href="#"><i class="fa fa-dashboard"></i>Sumber Manusia</a></li>
                            <li id="bb2_text" runat="server" class="active">Selenggara Maklumat Subjek</li>
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
                            <h3 class="box-title" id="h3_tag" runat="server">Kemasukan Maklumat Subjek</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body" id="ss1_show" runat="server">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl1_text" runat="server"> Bahagian <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="DDBAHA" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl2_text" runat="server"> Subjek <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="TextBox1" runat="server" class="form-control validate[optional] uppercase" MaxLength="200"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                
                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl3_text" runat="server"> Pemberat (%) <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox2" runat="server" class="form-control validate[optional,custom[number]]" MaxLength="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                 
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                <asp:TextBox ID="lbl_id" runat="server" Visible="false"></asp:TextBox>
                                <asp:Button ID="btn_jenis_simp" runat="server" class="btn btn-danger" UseSubmitBehavior="false" Text="Simpan" onclick="btn_jenis_simp_Click" />
                                            <asp:Button ID="btn_jenis_kmes" runat="server" Text="Kemaskini" Visible="false" class="btn btn-danger" UseSubmitBehavior="false" onclick="btn_jenis_kmes_Click" />   
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

         </ContentTemplate>
           
    </asp:UpdatePanel>
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
   
</asp:Content>


