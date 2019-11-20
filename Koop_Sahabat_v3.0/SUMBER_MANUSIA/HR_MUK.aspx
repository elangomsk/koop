<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_MUK.aspx.cs" Inherits="HR_MUK" %>

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
                        <h1>   MAKLUMAT KAKITANGAN           </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>SUMBER_MANUSIA</a></li>
                            <li class="active">  MAKLUMAT KAKITANGAN            </li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
      
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title">MAKLUMAT PERIBADI </h3>
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
                                        <asp:TextBox ID="Applcn_no" runat="server" class="form-control validate[optional] uppercase"
                                                            MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <div class="col-md-3 col-sm-4">&nbsp;</div>
                                               <div class="col-md-6  col-lg-5" style=" text-align:center;">
                                                        <div  class="fileupload fileupload-new" data-provides="fileupload">
                                                            <div  style="width: 100px; height: 100px;">
                                                            <asp:Image ID="Image2" runat="server" Width="100" Height="100"/>
                                                            </div>
                                                        </div>
                                                    </div> </label>
                                    <div class="col-sm-8">
                                        
                                    </div>
                                </div>
                            </div>
                           
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   No KP Baru / Pasport </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox1" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                 
                                <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Nama </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtname" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Organisasi </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="dd_orgen" runat="server" class="form-control selectpicker">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Perniagaan </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="TextBox9" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jantina</label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="dd_gender" runat="server" class="form-control selectpicker">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Gelaran </label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="dd_gel" runat="server" class="form-control selectpicker">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Lahir</label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                <span class="input-group-addon" style="height:3px;"><i class="fa fa-calendar"></i></span>
                                <asp:TextBox ID="TxtTaradu" runat="server" class="form-control validate[required] datepicker mydatepickerclass" placeholder="Pick Date"></asp:TextBox>
                                <%--<input name="reservation" id="reservation" class="form-control" type="text">--%>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Pangkat </label>
                                    <div class="col-sm-8">
                                          <asp:DropDownList ID="ddpang1" runat="server" class="form-control">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Bangsa </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="dd_bangsa" runat="server" class="form-control selectpicker">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Umur </label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="TextBox3" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Agama </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="dd_agama" runat="server" class="form-control selectpicker">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Negeri Lahir</label>
                                    <div class="col-sm-8">
                                          <asp:DropDownList ID="dd_negeri" runat="server" class="form-control selectpicker">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Warganegara </label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="dd_wargan" runat="server" class="form-control selectpicker">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Status Perkahwinan</label>
                                    <div class="col-sm-8">
                                           <asp:DropDownList ID="dd_perkha" runat="server" class="form-control selectpicker">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Alamat Tetap </label>
                                    <div class="col-sm-8">
                                       <textarea id="TextBox7" runat="server" class="form-control uppercase"></textarea>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Alamat Surat-Menyurat</label>
                                    <div class="col-sm-8">
                                            <textarea id="Textarea2" runat="server" class="form-control uppercase"></textarea>
                                    </div>
                                </div>
                            </div>
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Poskod</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox2" runat="server"  class="form-control validate[optional,custom[phone]]" maxlength="5"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Poskod </label>
                                    <div class="col-sm-8">
                                            <asp:TextBox ID="TextBox4" runat="server"  class="form-control validate[optional,custom[phone]]" maxlength="5"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">    Bandar </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox5" runat="server"  class="form-control validate[optional] uppercase" ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Bandar</label>
                                    <div class="col-sm-8">
                                           <asp:TextBox ID="TextBox6" runat="server"  class="form-control validate[optional] uppercase" ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">    Negeri </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="DD_NegriBind1" runat="server" class="form-control selectpicker">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"> No Telefon (R)</label>
                                    <div class="col-sm-8">
                                           <asp:TextBox ID="Txtnotel_R" runat="server"  class="form-control validate[optional,custom[phone]]" maxlength="10"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   No Telefon (B) </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="Txtnotel_P" runat="server"  class="form-control validate[optional,custom[phone]]" MaxLength="10"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Email</label>
                                    <div class="col-sm-8">
                                            <asp:TextBox ID="TextBox8" runat="server"  class="form-control validate[optional,custom[email]]" ></asp:TextBox>
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

                               <div class="chat-panel panel panel-primary">
                                    <div class="panel-heading">
                                        <div class="box-header with-border">
                            <h3 class="box-title">MAKLUMAT PERIBADI </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                                        </div>
                                   </div>

                            <div id="disp" runat="server" style="display:none;">
                                            <div style="pointer-events:none;">
                                             <div class="row">

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Jawatan (R)</label>
                                    <div class="col-sm-8">
                                           <asp:DropDownList ID="dd_jawa" runat="server" class="form-control selectpicker">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Skim </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="dd_skim" runat="server" class="form-control selectpicker">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Kategori</label>
                                    <div class="col-sm-8">
                                           <asp:DropDownList ID="dd_kategori" runat="server" class="form-control selectpicker">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Gred </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="dd_gred" runat="server" class="form-control selectpicker">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Jabatan</label>
                                    <div class="col-sm-8">
                                          <asp:DropDownList ID="dd_jaba" runat="server" class="form-control selectpicker">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Status Penjawatan </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="dd_stspenj" runat="server" class="form-control selectpicker">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Unit </label>
                                    <div class="col-sm-8">
                                          <asp:DropDownList ID="dd_unit" runat="server" class="form-control selectpicker">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Tarikh Mula Khidmat </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        <asp:TextBox ID="tm_khid" runat="server"  class="form-control validate[optional] datepicker mydatepickerclass" placeholder="Pick Date"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Tarikh Mula Lantikan</label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        <asp:TextBox ID="t_lant" runat="server"  class="form-control validate[optional] datepicker mydatepickerclass" placeholder="Pick Date" ></asp:TextBox>
                                                        </div>
                                    </div>
                                </div>
                            </div>
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Tarikh Akhir Lantikan </label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        <asp:TextBox ID="t_hingga" runat="server"  class="form-control validate[optional] datepicker mydatepickerclass" placeholder="Pick Date"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Penyelia 1 </label>
                                    <div class="col-sm-8">
                                           <asp:TextBox ID="pen1" runat="server"  class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Penyelia 2 </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="pen2" runat="server"  class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                                                  <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Waktu Bekerja  </label>
                                    <div class="col-sm-8">
                                            <asp:DropDownList ID="DropDownList1" runat="server" class="form-control selectpicker">
                                                    </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Sebab Pergerakan  </label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="dd_sebab" runat="server" class="form-control selectpicker">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Tarikh Berhenti </label>
                                    <div class="col-sm-8">
                                            <div class="input-group">
                                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                         <asp:TextBox ID="t_berhanti" runat="server"  class="form-control validate[optional] datepicker mydatepickerclass" placeholder="Pick Date"></asp:TextBox>
                                                         </div>
                                    </div>
                                </div>
                            </div>
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Sebab Berhenti  </label>
                                    <div class="col-sm-8">
                                          <asp:DropDownList ID="dd_sebab_perh" runat="server" class="form-control selectpicker">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>




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





