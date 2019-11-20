<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_FAMI.aspx.cs" Inherits="HR_FAMI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>  MAKLUMAT KAKITANGAN     </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>SUMBER_MANUSIA</a></li>
                            <li class="active">  MAKLUMAT AHLI KELUARGA      </li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
      
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title"> MAKLUMAT PERIBADI </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> No Kakitangan</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txt_kaki" runat="server" class="form-control validate[optional] uppercase" MaxLength="10"></asp:TextBox>
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
                                        <asp:TextBox ID="txt_nama" runat="server" class="form-control validate[required] uppercase" MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Gred </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txt_gred" runat="server" class="form-control validate[required] uppercase" ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Nama Syarikat / Nama Organisasi </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox1" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Perniagaan  </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox3" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>


                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Jabatan </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txt_jabat" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Jawatan </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txt_jawa" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="box-header with-border">
                            <h3 class="box-title"> MAKLUMAT PASANGAN </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>




                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  No KP / Pasport </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txt_nokp1" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Nama  </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="txt_nama1" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Tarikh Lahir </label>
                                    <div class="col-sm-8">
                                       <div class="input-group">
                                <span class="input-group-addon" style="height:3px;"><i class="fa fa-calendar"></i></span>
                                                        <asp:TextBox ID="txt_tarlah" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>

                                                        </div>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Warganegara  </label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="DD_Wargan1" runat="server" class="form-control validate[required] uppercase" style="text-transform:uppercase;">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Alamat </label>
                                    <div class="col-sm-8">
                                        <textarea id="txt_alamat" runat="server" class="form-control uppercase"></textarea>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Umur  </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txt_umar" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  No Telefon  </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="txt_notf" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Status Pekerjaan </label>
                                    <div class="col-sm-8">
                                       <div class="col-md-2 col-sm-1">
                                                        <label>
                                                            <asp:RadioButton ID="Rdya" runat="server" AutoPostBack="true" GroupName="war" />&nbsp;Ya
                                                        </label>
                                                        <br />
                                                    </div>
                                                    <div class="col-md-2 col-sm-1">
                                                        <label>
                                                            <asp:RadioButton ID="Rdtidak" runat="server" AutoPostBack="true" GroupName="war" />&nbsp;Tidak
                                                        </label>
                                                        <br />
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Jawatan & Majikan</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txt_jama" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Status OKU  </label>
                                    <div class="col-sm-8">
                                      <div class="col-md-2 col-sm-1">
                                                        <label>
                                                            <asp:RadioButton ID="sts_oku1" runat="server" AutoPostBack="true" GroupName="war1" />&nbsp;Ya
                                                        </label>
                                                        <br />
                                                    </div>
                                                    <div class="col-md-2 col-sm-1">
                                                        <label>
                                                            <asp:RadioButton ID="sts_oku2" runat="server" AutoPostBack="true" GroupName="war1" />&nbsp;Tidak
                                                        </label>
                                                        <br />
                                                    </div>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                            <div class="box-header with-border">
                            <h3 class="box-title"> MAKLUMAT ANAK </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>

                             <div class="ma_dis" id="disma" runat="server" style="display:none;">
                                            <div class="row">

                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  No KP / Pasport </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="txt_nokp2" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Nama   </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="txt_nama2" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Tarikh Lahir</label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        <asp:TextBox ID="txt_tala2" runat="server" class="form-control validate[optional] datepicker"></asp:TextBox>
                                                        </div>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Umur   </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txt_umar2" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Warganegara </label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="DD_Wargan2" runat="server" class="form-control uppercase" style="text-transform:uppercase;">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jantina   </label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="DD_jant" runat="server" class="form-control selectpicker uppercase">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Status Pekerjaan </label>
                                    <div class="col-sm-8">
                                          <div class="col-md-2 col-sm-1">
                                                        <label>
                                                            <asp:RadioButton ID="Peke1" runat="server" AutoPostBack="true" GroupName="Peke" />&nbsp;Bekerja
                                                        </label>
                                                        <br />
                                                    </div>
                                                    <div class="col-md-2 col-sm-1">
                                                        <label>
                                                            <asp:RadioButton ID="Peke2" runat="server" AutoPostBack="true" GroupName="Peke" />&nbsp;Belajar
                                                        </label>
                                                        <br />
                                                    </div>
                                                    <div class="col-md-2 col-sm-1">
                                                        <label>
                                                            <asp:RadioButton ID="Peke3" runat="server" AutoPostBack="true" GroupName="Peke" />&nbsp;Tidak
                                                            Belajar
                                                        </label>
                                                        <br />
                                                    </div>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Hubungan   </label>
                                    <div class="col-sm-8">
                                      <asp:DropDownList ID="DD_Hubungan2" runat="server" class="form-control uppercase" style="text-transform:uppercase;">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                                <br />
                                            <br />
                                            <br />
                                            <div class="row">
                                                <br />
                                                <br />
                                                                <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">    Status Pendidikan </label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="DD_pend" runat="server" class="form-control selectpicker uppercase">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Status Perkahwinan    </label>
                                    <div class="col-sm-8">
                                      <div class="col-md-2 col-sm-1">
                                                        <label >
                                                            <asp:RadioButton ID="Perka1" runat="server" AutoPostBack="true" GroupName="Perka" />&nbsp;Berkahwin
                                                        </label>
                                                        <br />
                                                    </div>
                                                    <div class="col-md-2 col-sm-1">
                                                        <label>
                                                            <asp:RadioButton ID="Perka2" runat="server" AutoPostBack="true" GroupName="Perka" />&nbsp;Bujang
                                                        </label>
                                                        <br />
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Status Tanggungan </label>
                                    <div class="col-sm-8">
                                         <div class="col-md-2 col-sm-1">
                                                        <label>
                                                            <asp:RadioButton ID="Tang1" runat="server" AutoPostBack="true" GroupName="Tang" />&nbsp;Ya
                                                        </label>
                                                        <br />
                                                    </div>
                                                    <div class="col-md-2 col-sm-1">
                                                        <label>
                                                            <asp:RadioButton ID="Tang2" runat="server" AutoPostBack="true" GroupName="Tang" />&nbsp;Tidak
                                                        </label>
                                                        <br />
                                                    </div>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Status OKU   </label>
                                    <div class="col-sm-8">
                                        <div class="col-md-2 col-sm-1">
                                                        <label>
                                                            <asp:RadioButton ID="oku1" runat="server" AutoPostBack="true" GroupName="OKU" />&nbsp;Ya
                                                        </label>
                                                        <br />
                                                    </div>
                                                    <div class="col-md-2 col-sm-1">
                                                        <label>
                                                            <asp:RadioButton ID="oku2" runat="server" AutoPostBack="true" GroupName="OKU" />&nbsp;Tidak
                                                        </label>
                                                        <br />
                                                    </div>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                                                </div>
                                 </div>


                            <div class="box-header with-border">
                            <h3 class="box-title">ORANG HUBUNGAN KECEMASAN </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Nama </label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="txt_nama3" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> No Telefon (R/P)  </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txt_tf3" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Hubungan</label>
                                    <div class="col-sm-8">
                                          <asp:DropDownList ID="DD_Hubungan1" runat="server" class="form-control validate[required] uppercase" style="text-transform:uppercase;">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  No Telefon (B)  </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txt_tfb" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Alamat</label>
                                    <div class="col-sm-8">
                                          <textarea id="txt_Ala" runat="server" class="form-control validate[optional] uppercase" rows="3" style="text-transform:uppercase;"></textarea>
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
                                 <asp:Button ID="Button4" runat="server" class="btn btn-danger" Text="Simpan"  UseSubmitBehavior="false" />
                                <asp:Button ID="btb_kmes" runat="server" Text="Kemaskini" UseSubmitBehavior="false" class="btn btn-danger"  />
                                <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" UseSubmitBehavior="false"  OnClick="Button5_Click"   />
                                <asp:Button ID="Button2" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" OnClick="Click_bck"  />
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
             <Triggers>
               <asp:PostBackTrigger ControlID="Button4"  />
               <asp:PostBackTrigger ControlID="btb_kmes"  />
           </Triggers>
    </asp:UpdatePanel>
</asp:Content>



