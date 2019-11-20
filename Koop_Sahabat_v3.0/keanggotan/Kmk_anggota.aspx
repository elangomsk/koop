<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../keanggotan/Kmk_anggota.aspx.cs" Inherits="Kmk_anggota" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server" ScriptMode="Release">
    </asp:ScriptManager>

   
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1> <asp:Label ID="ps_lbl1" runat="server"></asp:Label> </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i><asp:Label ID="ps_lbl2" runat="server"></asp:Label> </a></li>
                            <li class="active">   <asp:Label ID="ps_lbl3" runat="server"></asp:Label>     </li>
                        </ol>
                    </section>
                      <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
                    <!-- Main content -->
                   <section class="content">
      
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl5" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox1" class="form-control validate[optional] uppercase" MaxLength="12" runat="server"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="You can't leave this Empty" CssClass="requiredFieldValidateStyle" ForeColor="Red" ControlToValidate="TextBox1" ValidationGroup="vgSubmit_kmk_simp">
                                    </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">                                    
                                    <div class="col-sm-8">
                                            <asp:Button ID="txtSearch" runat="server" class="btn btn-primary" Text="Carian" usesubmitbehavior="false" OnClick="btnsrch_Click"/>
                                                        <asp:Button ID="restbutn" runat="server" class="btn btn-default" Text="Set Semula" usesubmitbehavior="false" OnClick="btnreset_Click" />
                                    </div>
                                </div>
                            </div>
                                  </div>
                                </div>


                           
                                <div class="row">
                                    <div class="col-md-12">
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   <asp:Label ID="ps_lbl8" runat="server"></asp:Label>  </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox2" runat="server" class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase" MaxLength="150"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl9" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="DropDownList3" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                               </div>


                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl10" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="TextBox5" class="form-control validate[optional,custom[phone]]" runat="server" MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl11" runat="server"></asp:Label>  </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="textdate1" runat="server" class="form-control datepicker mydatepickerclass"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  <asp:Label ID="ps_lbl12" runat="server"></asp:Label> <span class="style1">*</span>  </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox22" class="form-control validate[optional,custom[bank]]" runat="server" MaxLength="15"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="You can't leave this Empty" CssClass="requiredFieldValidateStyle" ForeColor="Red" ControlToValidate="TextBox22" ValidationGroup="vgSubmit_kmk_simp">
                                    </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  <asp:Label ID="ps_lbl13" runat="server"></asp:Label> <span class="style1"></span>   </label>
                                    <div class="col-sm-8">
                                          <asp:DropDownList ID="bankdet" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="You can't leave this Empty" CssClass="requiredFieldValidateStyle" ForeColor="Red" ControlToValidate="bankdet" ValidationGroup="vgSubmit_kmk_simp">
                                    </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>



                     <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl14" runat="server"></asp:Label> <span class="style1">*</span>  </label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                                       <asp:TextBox ID="txtdate" runat="server" class="form-control validate[optional] datepicker mydatepickerclass"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                       </div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="You can't leave this Empty" CssClass="requiredFieldValidateStyle" ForeColor="Red" ControlToValidate="txtdate" ValidationGroup="vgSubmit_kmk_simp">
                                    </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  <asp:Label ID="ps_lbl15" runat="server"></asp:Label><span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="ddlstspm" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="You can't leave this Empty" CssClass="requiredFieldValidateStyle" ForeColor="Red" ControlToValidate="ddlstspm" ValidationGroup="vgSubmit_kmk_simp">
                                    </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                 <asp:Button ID="Button3" runat="server" class="btn btn-danger" UseSubmitBehavior="false" Text="Simpan" OnClick="Buttton_Click" ValidationGroup="vgSubmit_kmk_simp"/>
                                                        <asp:Button ID="Button5" runat="server" class="btn btn-default" UseSubmitBehavior="false" Text="Kembali" OnClick="Click_bck" />
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
             </ContentTemplate>
            
    </asp:UpdatePanel>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
   
</asp:Content>








