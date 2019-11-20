<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../PELABURAN_ANGGOTA/PP_L_Tangguh.aspx.cs" Inherits="PP_L_Tangguh" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" style="height: 895px;">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1> Kelulusan Permohonan Tangguh Pembiayaan </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Pelaburan Anggota</a></li>
                            <li class="active"> Kelulusan Permohonan Tangguh Pembiayaan </li>
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
                            <h3 class="box-title">Maklumat Pembiayaan</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Kata Kunci Carian <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="Applcn_no" runat="server"  class="form-control validate[optional] uppercase" MaxLength="12" ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">                                    
                                    <div class="col-sm-8">
                                     <asp:Button ID="Button9" runat="server" class="btn btn-primary" Text="Carian" usesubmitbehavior="false" OnClick="btnsrch_Click" />
                                                    <asp:Button ID="Button10" runat="server" class="btn btn-default" Text="Set Semula" usesubmitbehavior="false" OnClick="btn_rstclick"/>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                              
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtname" runat="server" class="form-control validate[optional,custom[textSp]] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Wilayah / Pejabat</label>
                                    <div class="col-sm-8">
                                            <asp:TextBox ID="txtwil" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>


                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Jumlah Belian (RM) </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txtjumla" runat="server" style="text-align:right;" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Cawangan / Jabatan </label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="txtcaw" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>


                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Jumlah Kumulatif Kena (RM)</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txtamaun" runat="server" style="text-align:right;" class="form-control"></asp:TextBox>

                                    </div>
                                </div>
                            </div>

                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Tempoh (Bulan)</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txttempoh" runat="server" class="form-control uppercase" MaxLength="2"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>

                            <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Jumlah Kumulatif Tunggakan (RM)</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox2" runat="server" style="text-align:right;" class="form-control"></asp:TextBox>

                                    </div>
                                </div>
                            </div>

                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Jumlah Kumulatif Bayar (RM) </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txttemp" runat="server" style="text-align:right;" class="form-control"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>

                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Jumlah Kumulatif Simpanan (RM)</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox4" runat="server" style="text-align:right;" class="form-control"></asp:TextBox>

                                    </div>
                                </div>
                            </div>

                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Jumlah Untung (RM) </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox3" runat="server" style="text-align:right;" class="form-control"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>


                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Baki Kumulatif Pelaburan (RM)</label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox1" runat="server" style="text-align:right;" class="form-control"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>



                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                <asp:Button ID="Button1" runat="server" class="btn btn-warning" UseSubmitBehavior="false" Text="Semak JBB" OnClick="click_jbb" />
                                                        <asp:Button ID="Button5" runat="server" class="btn btn-warning" UseSubmitBehavior="false" Text="Litigasi Bercagar" OnClick="clk_Bercagar"/>
                                                        <asp:Button ID="Button6" runat="server" class="btn btn-warning" UseSubmitBehavior="false" Text="Litigasi Penjamin" onclick="clk_Penjamin"/>
                                        <asp:Button ID="Button7" runat="server" class="btn btn-default" Text="Kembali" usesubmitbehavior="false" OnClick="clk_bak"/>                
                                 
                            </div>
                           </div>
                               </div>
                           <div class="box-body">&nbsp;
                                    </div>

                            <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Permohonan Tangguh Pembiayaan</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>

                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Tempoh Tangguh </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox5" runat="server" class="form-control validate[optional,custom[number]]" MaxLength="3"></asp:TextBox>

                                    </div>
                                </div>
                            </div>

                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Tarikh Mula Tangguh  </label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                                        <asp:TextBox ID="TextBox7" runat="server" class="form-control validate[optional] datepicker"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Tarikh Akhir Tangguh</label>
                                    <div class="col-sm-8">
                                       <div class="input-group">
                                                       <asp:TextBox ID="TextBox10" runat="server" class="form-control validate[optional] datepicker"></asp:TextBox>
                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                       </div>

                                    </div>
                                </div>
                            </div>

                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Tarikh Mula Bayar </label>
                                    <div class="col-sm-8">
                                      <div class="input-group">
                                                       <asp:TextBox ID="TextBox11" runat="server" class="form-control validate[optional] datepicker"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Akhir Bayar</label>
                                    <div class="col-sm-8">
                                      <div class="input-group">
                                                       <asp:TextBox ID="TextBox12" runat="server" class="form-control validate[optional] datepicker"></asp:TextBox>
                                          <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                       </div>

                                    </div>
                                </div>
                            </div>

                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Status</label>
                                    <div class="col-sm-8">
                                      <asp:DropDownList ID="DropDownList1" style="width:100%; font-size:13px;" runat="server" class="form-control uppercase validate[optional]">
                                                        <asp:ListItem Value="" Text="--- PILIH ---"></asp:ListItem>
                                                        <asp:ListItem Value="01" Text="Permohonan"></asp:ListItem>
                                                        <asp:ListItem Value="02" Text="Batal Permohonan"></asp:ListItem>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Catatan</label>
                                    <div class="col-sm-8">
                                        <textarea id="Textarea1" class="form-control uppercase" rows="3" runat="server" maxlength="500"></textarea>

                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>


                            <div class="box-header with-border">
                            <h3 class="box-title">Makllumat Kelulusan Tangguh Pembiayaan </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Status <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                      <asp:DropDownList ID="DropDownList2" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                        <asp:ListItem Value="" Text="--- PILIH ---"></asp:ListItem>
                                                        <asp:ListItem Value="A" Text="Lulus"></asp:ListItem>
                                                        <asp:ListItem Value="NA" Text="Tolak"></asp:ListItem>
                                                        </asp:DropDownList>

                                    </div>
                                </div>
                            </div>

                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Catatan <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                        <textarea id="catatan" class="form-control uppercase" rows="3" runat="server" maxlength="500"></textarea>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>

                             <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                 <asp:Button ID="Button2" runat="server" CssClass="btn btn-danger" Text="Simpan" usesubmitbehavior="false" OnClick="btn_click" />
                                                    <asp:Button ID="Button3" runat="server" class="btn btn-danger" Text="Set Semula" usesubmitbehavior="false" OnClick="btn_rstclick1" />
                                 
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


