<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../PELABURAN_ANGGOTA/PP_pen_Senaraihitam.aspx.cs" Inherits="PP_pen_Senaraihitam" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" style="height: 895px;">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>  Pendaftaran Senaraihitam       </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Pelaburan Anggota</a></li>
                            <li class="active">  Pendaftaran Senaraihitam       </li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
      
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">  No Permohonan <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="Applcn_no" runat="server"  class="form-control validate[optional] uppercase" MaxLength="11" ></asp:TextBox>
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
                                          <asp:Button ID="Button3" runat="server" class="btn btn-primary" Text="Carian" usesubmitbehavior="false" OnClick="btnsrch_Click" />
                                                    <asp:Button ID="Button6" runat="server" class="btn btn-default" Text="Set Semula" usesubmitbehavior="false" OnClick="btnreset_Click" />
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>


                         
                             <div class="row">
                             <div class="col-md-12">
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Nama  </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="MP_nama" runat="server" class="form-control validate[optional,custom[textSp]] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  No KP Baru </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="MP_icno" runat="server" class="form-control validate[optional,custom[phone]]" MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                                <%-- <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Wilayah / Pejabat </label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="MP_wilayah" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Cawangan / Jabatan   </label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="MP_cawangan" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>--%>

                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Amaun Pengeluaran (RM)  </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox1" runat="server" class="form-control uppercase" style="text-align:right;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Tempoh (Bulan)   </label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="TextBox2" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Amaun Tunggakan (RM) </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox6" runat="server" class="form-control uppercase" style="text-align:right;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Usia Tunggakan (Hari)  </label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="TextBox7" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>


                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Baki Pelaburan (RM) </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox8" runat="server" class="form-control uppercase" style="text-align:right;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                   
                                 </div>
                                </div>

                             <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Penjamin 1</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>


                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jenis Pengenalan</label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList runat="server" class="form-control validate[optional]" ID="DropDownList3">
                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   No Pengenalan </label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="MPP_jb" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox4" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                              <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Penjamin 2</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Jenis Pengenalan</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList runat="server" class="form-control validate[optional]" ID="DropDownList1">
                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   No Pengenalan </label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="TextBox3" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>


                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox5" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>

                              <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Penjamin 3</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jenis Pengenalan</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList runat="server" class="form-control validate[optional]" ID="DropDownList2">
                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   No Pengenalan  </label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="TextBox9" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox10" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                            
                              <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Senaraihitam</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>

                             <div class="row">
                                                <div class="col-md-6 box-body"  >
                                                    <label for="inputEmail3" class="col-sm-3 control-label"></label>
                                                            <div class="col-md-8 col-sm-1">
                                                                <div class="col-md-6">
                                                               <asp:CheckBox ID="chk1" CssClass="mycheckbox" runat="server" Text="Pelanggan" />
                                                                    </div>
                                                                 <div class="col-md-6">
                                                              <asp:CheckBox ID="chk3" runat="server" CssClass="mycheckbox" Text="Penjamin 2" />
                                                                    </div>
                                                            </div>
                                                        </div>
                                                <div class="col-md-6 box-body">
                                                    <div class="col-md-5 col-sm-1">
                                                       
                                                    </div>
                                                </div>
                                            </div> 

                             <br />
                                            <div class="row">
                                                  <div class="col-md-6 box-body"  >
                                                    <label for="inputEmail3" class="col-sm-3 control-label"></label>
                                                            <div class="col-md-8 col-sm-1">
                                                                <div class="col-md-6">
                                                               <asp:CheckBox ID="chk2" CssClass="mycheckbox" runat="server" Text="Penjamin 1" />
                                                                    </div>
                                                                 <div class="col-md-6">
                                                              <asp:CheckBox ID="chk4" runat="server" CssClass="mycheckbox" Text="Penjamin 3" />
                                                                    </div>
                                                            </div>
                                                        </div>
                                              
                                            </div> 
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                 <asp:Button ID="Button1" runat="server" class="btn btn-danger" Text="Senaraihitam" UseSubmitBehavior="false"  OnClick="submit_click"/>
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
             
    </asp:UpdatePanel>
</asp:Content>



