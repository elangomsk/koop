<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_BATL_CUTI.aspx.cs" Inherits="HR_BATL_CUTI" %>

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
                        <h1>   MAKLUMAT CUTI          </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>SUMBER_MANUSIA</a></li>
                            <li class="active">   MAKLUMAT CUTI            </li>
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
                            <h3 class="box-title"> MAKLUMAT PERIBADI</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Kakitangan</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtsno" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Nama </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtnama" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                
                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Nama Syarikat / Nama Organisasi</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txt_org" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                     
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Perniagaan</label>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Jawatan</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtjaw" runat="server" disabled="disabled" class="form-control validate[required] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                     
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Gred</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txtgred" runat="server" disabled="disabled" class="form-control validate[required] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Jabatan </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtJab" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                     
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama Penyelia</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txtnamapen" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <div class="box-header with-border">
                            <h3 class="box-title"> MAKLUMAT PEMBATALAN CUTI </h3>
                        </div>
                            </div>
                        </div>
                    </div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Jenis Cuti </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddjenis" runat="server" class="form-control uppercase" AutoPostBack="true" >
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                     
                            <div class="col-md-6 box-body" >
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Cuti Tarikh Mula</label>
                                    <div class="col-sm-8">
                                       <div class="input-group">
                                         <asp:TextBox ID="txttcuti" runat="server" class="form-control validate[optional] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox2" Visible="false" runat="server" class="form-control validate[optional] datepicker mydatepickerclass" placeholder="Pick Date"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                           </div>
                                    </div>
                                </div>
                            </div>

                                <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body" >
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Cuti Tarikh Akhir </label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                         <asp:TextBox ID="txthin" runat="server" class="form-control validate[optional] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox5" Visible="false" runat="server" class="form-control validate[optional] datepicker mydatepickerclass" placeholder="Pick Date"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                           </div>
                                    </div>
                                    </div>
                                </div>
                                
                     
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Sebab Cuti</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txtscuti" runat="server" class="form-control validate[optional] uppercase" MaxLength="500"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                    </div>
                            </div>

                                <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">    Sebab Batal Cuti </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtsbcuti" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox1" runat="server" Visible="false" class="form-control validate[optional] uppercase"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox4" runat="server" Visible="false" class="form-control validate[optional] uppercase"></asp:TextBox>
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
                                </ContentTemplate>
             <Triggers>
               <asp:PostBackTrigger ControlID="Button4"  />
               <asp:PostBackTrigger ControlID="btb_kmes"  />
           </Triggers>
    </asp:UpdatePanel>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
    
</asp:Content>





