<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../keanggotan/cetak_pt.aspx.cs" Inherits="cetak_pt" %>
 <%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       
   <style>
       .table label{
               font-size: 11px;
       }
   </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
   <asp:ScriptManager ID="ScriptManagerCalendar" AsyncPostBackTimeOut="72000" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
   

   
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>   Cetak Penyata Tahunan </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i> Keanggotaan </a></li>
                            <li class="active"> Cetak Penyata Tahunan </li>
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
                            <h3 class="box-title">Cetak Penyata Tahunan</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div style="pointer-events:none;">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Nama </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="Txtnama" runat="server" class="form-control validate[optional]" MaxLength="150" style="text-transform:uppercase;"></asp:TextBox>

                                    </div>
                                </div>
                                  <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Poskod  </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox3" runat="server"  class="form-control validate[optional]" maxlength="5"></asp:TextBox>
                                    </div>
                                </div>
                                  <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Negeri  </label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="ddlnegri" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                             <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Alamat </label>
                                    <div class="col-sm-8">
                                        <textarea ID="Textarea1" runat="server" class="form-control validate[optional] uppercase" rows="4"></textarea>

                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            
                               <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  No. KP Baru  </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="Txtnokp" runat="server" class="form-control validate[optional] uppercase" maxlength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jantina  </label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="DD_jantina" runat="server" class="form-control select2 validate[optional] uppercase">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Bangsa  </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="DD_bangsa" runat="server" class="form-control select2 validate[optional] uppercase">
                                                                    </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Kategory Pemohon  </label>
                                         <div class="col-sm-8">
                                      <asp:DropDownList ID="DD_pemohan" runat="server" style="width:100%; font-size:13px;" class="form-control select2 validate[optional] uppercase">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Cawangan  </label>
                                    <div class="col-sm-8">
                                          <asp:DropDownList ID="DD_cawangan" runat="server" class="form-control select2 uppercase" AutoPostBack="true" onselectedindexchanged="ddkaw_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Wilayah </label>
                                         <div class="col-sm-8">
                                    <asp:DropDownList ID="DD_wilayah" runat="server" class="form-control select2 uppercase">
                                                                    
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Pusat  </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox2" runat="server" class="form-control validate[optional] uppercase" maxlength="50"></asp:TextBox>
                                                                </div>
                                    </div>
                                </div>
                          
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Anggota </label>
                                         <div class="col-sm-8">
                                   <asp:TextBox ID="TextBox1" runat="server" class="form-control validate[optional] uppercase" maxlength="12"></asp:TextBox>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                   </div>
                                </div>
                            <hr />
                                    <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Tahun Penyata <span class="style1">*</span>  </label>
                                    <div class="col-sm-8">
                                          <asp:DropDownList ID="DropDownList4" runat="server" class="form-control select2 validate[required]">
                                                                    </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-8">
                                         <asp:Button ID="btnsave" runat="server" class="btn btn-danger" Text="Jana Penyata" UseSubmitBehavior="false" onclick="Click_print" />
                                                            &nbsp;
                                                            <asp:Button ID="btn_reset" runat="server" class="btn btn-danger" Visible="false" Text="Batal" usesubmitbehavior="false"/>
                                    </div>
                                </div>
                            </div>  
                            </div>
                                </div>
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center; display:none;">
                                 <rsweb:ReportViewer ID="RptviwerStudent" runat="server"></rsweb:ReportViewer>
                                     <asp:Label runat="server" ID="ReportErrorMessage" Visible="false" CssClass="report-error-message"></asp:Label>
                                                
                                 
                            </div>
                           </div>
                               </div>
                         
                        </div>

                    </div>
                </div>
            </div>
            <!-- /.row -->

          </ContentTemplate>
               <Triggers>
            <asp:PostBackTrigger ControlID="btnsave"  />              
                 </Triggers>
    </asp:UpdatePanel>
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
  
</asp:Content>












