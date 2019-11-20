<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_BAHAGIAN_PENILAIAN.aspx.cs" Inherits="HR_BAHAGIAN_PENILAIAN" %>

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
                        <h1 id="h1_tag" runat="server"> Selenggara maklumat Bahagian  </h1>
                        <ol class="breadcrumb">
                            <li id="bb1_text" runat="server"><a href="#"><i class="fa fa-dashboard"></i>Sumber Manusai</a></li>
                            <li id="bb2_text" runat="server" class="active">  Selenggara maklumat Bahagian      </li>
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
                            <h3 class="box-title" id="h3_tag" runat="server"> Kemasukan Maklumat Bahagain </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl1_text" runat="server">Kod <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txt_bahagian" runat="server" class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase" MaxLength="3"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="Label3" runat="server">Kategory Jawatan </label>
                                    <div class="col-sm-8">
                                         <asp:listbox runat="server" class="form-control uppercase" id="chk_lst" selectionmode="Multiple"></asp:listbox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl2_text" runat="server">   Keterangan <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txt_keteran" runat="server" class="form-control validate[optional] uppercase" MaxLength="200"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                </div>
                                 </div>
                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Catatan</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox id="text_area" TextMode="multiline" class="form-control validate[optional] uppercase" Columns="20" Rows="2" MaxLength="1000" runat="server" />
                                                        <asp:TextBox id="TextBox1" Visible="false"  runat="server" />
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <div class="row">
                             <div class="col-md-12">
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="Label1" runat="server">   Markah Wajaran</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox2" runat="server" class="form-control validate[optional] uppercase" MaxLength="200"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                </div>
                                 </div>
                              <div class="row">
                             <div class="col-md-12">
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="Label2" runat="server">   Jenis</label>
                                    <div class="col-sm-8">
                                          <div class="col-sm-4">
                                        <asp:RadioButton ID="jenis_chk1" GroupName="ss1" runat="server" Text="Skala Objektif" />
                                              </div>
                                        &nbsp;&nbsp;&nbsp;
                                        <div class="col-sm-4">
                                        <asp:RadioButton ID="jenis_chk2" GroupName="ss1" runat="server" Text="Subjektif" />
                                            </div>
                                    </div>
                                </div>
                            </div>
                                </div>
                                 </div>
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                              <asp:Button ID="btn_simp" runat="server" class="btn btn-danger" Text="Simpan" UseSubmitBehavior="false" Type="submit" onclick="btn_simp_Click"  />
                              <asp:Button ID="btb_kmes" runat="server" Text="Kemaskini" UseSubmitBehavior="false" class="btn btn-danger" onclick="btb_kmes_Click"  />                                                        
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


