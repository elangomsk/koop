<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_SELE_Mcuti.aspx.cs" Inherits="HR_SELE_Mcuti" %>

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
                        <h1>  SELENGGARA MAKLUMAT CUTI     </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>SUMBER_MANUSIA</a></li>
                            <li class="active">   MAKLUMAT CUTI            </li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
      
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title"> MAKLUMAT KELAYAKAN KAKITANGAN </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jenis Cuti</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="dd_jcuti" runat="server" class="form-control" AutoPostBack="true" selectedindexchanged="dd_jeniscuti"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                           
                                 </div>
                                </div>
                             <div class="chat-panel panel panel-primary">
                             <div class="panel-heading">
                                 <center><strong><asp:Label ID="M_title" runat="server"></asp:Label></strong></center>
                                 </div>
                                 </div>



                            <div id="l1" runat="server">
                                <div class="row">
                                    <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Jawatan </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="an_jawa" runat="server" class="form-control selectpicker">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Kategori Jawatan</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="an_katjawa" runat="server" class="form-control selectpicker">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" style="line-height:13px;"> Tempoh Khidmat Minimum (Tahun) </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="an_tk_min" runat="server" class="form-control validate[optional,custom[number]]" MaxLength="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" style="line-height:13px;">Tempoh Khidmat Maksimum (Tahun) </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="an_tk_mak" runat="server" class="form-control validate[optional,custom[number]]"  MaxLength="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                                <div class="row">
                                 <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Kelayakan Cuti (Hari) </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="an_kel_cuti" runat="server" class="form-control validate[optional,custom[number]]"  MaxLength="2"></asp:TextBox>
                                                        <asp:TextBox ID="ann_rno" Visible="false" runat="server" class="form-control validate[optional,custom[number]]"  MaxLength="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                     </div>
                                    </div>
                                </div>

                            <div id="l2" runat="server">
                                <div class="row">
                                           <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Kategori Cuti </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="com_cuti" runat="server" class="form-control jcuti"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                                               <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">    Kelayakan Cuti (Hari) </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="com_kel_cuti" runat="server" class="form-control validate[optional,custom[number]]"  MaxLength="2"></asp:TextBox>
                                                        <asp:TextBox ID="com_rno" Visible="false" runat="server" class="form-control validate[optional,custom[number]]"  MaxLength="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                     </div> 
                                </div>
                                </div>   
                            
                            <div id="l3" runat="server">
                            <div class="row">
                            <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Tempoh Khidmat Minimum (Tahun) </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="out_tk_min" runat="server" class="form-control validate[optional,custom[number]]" MaxLength="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                
                                <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Tempoh Khidmat Maksimum (Tahun) </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="out_tk_mak" runat="server" class="form-control validate[optional,custom[number]]"  MaxLength="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                    
                                <div class="row">
                                    <div class="col-md-12">
                                <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Kelayakan Cuti (Hari) </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="out_kel_cuti" runat="server" class="form-control validate[optional,custom[number]]"  MaxLength="2"></asp:TextBox>
                                                        <asp:TextBox ID="out_rno" Visible="false" runat="server" class="form-control validate[optional,custom[number]]"  MaxLength="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                    </div>
                                    </div>
                                </div>  
                                </div> 
                                </div>  
                            
                            <div id="l4" runat="server">
                                <div class="row">
                            <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Kelayakan Cuti (Hari) </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="gen_kel_cuti" runat="server" class="form-control validate[optional,custom[number]]"  MaxLength="2"></asp:TextBox>
                                                        <asp:TextBox ID="gen_rno" Visible="false" runat="server" class="form-control validate[optional,custom[number]]"  MaxLength="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                </div>
                                    </div>

                                </div>   
                            
                            <div id="l_umum" runat="server">
                                <div class="row">
                                           <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Organisasi </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="DropDownList1" runat="server" class="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                                               <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Negeri </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="DD_NegriBind1" runat="server" class="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                     </div> 
                                </div>

                                <div class="row">
                                           <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">    Dari </label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                                     <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        <asp:TextBox ID="td_date" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                        </div>
                                    </div>
                                </div>
                            </div>

                                               <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Sehingga </label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                                     <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        <asp:TextBox ID="ts_date" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Keterangan </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox1" runat="server" class="form-control uppercase"  MaxLength="100"></asp:TextBox>
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
                                <asp:TextBox ID="lbl_name" runat="server" class="form-control validate[optional] uppercase" Visible="false"></asp:TextBox>
                                 <asp:Button ID="Button4" runat="server" class="btn btn-danger" Text="Simpan"  UseSubmitBehavior="false" />
                                <asp:Button ID="btb_kmes" runat="server" Text="Kemaskini" UseSubmitBehavior="false" class="btn btn-danger"  />
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


