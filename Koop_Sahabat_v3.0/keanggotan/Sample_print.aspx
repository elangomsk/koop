<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../keanggotan/Sample_print.aspx.cs" Inherits="Sample_print" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       <style type="text/css">

.button {
 border:solid 1px #c0c0c0;
 background-color:#eeeeee;
 font-family:verdana;
 font-size:11px;
}

.modalBg {
background-color:#cccccc;
filter:alpha(opacity=80);
opacity:0.8;
}

.modalPanel {
background-color:#ffffff;
border-width:3px;
border-style:solid;
border-color:Gray;
padding:3px;
width:100%;
height:100%;
}

</style>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>  <asp:Label ID="ps_lbl1" runat="server"></asp:Label> Penyata Tahunan </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i> <asp:Label ID="ps_lbl2" runat="server"></asp:Label></a></li>
                            <li class="active">  <asp:Label ID="ps_lbl3" runat="server"></asp:Label> Penyata Tahunan </li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
      
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title"> <asp:Label ID="ps_lbl4" runat="server"></asp:Label> Maklumat Penyata Tahunan</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  <asp:Label ID="ps_lbl5" runat="server"></asp:Label> <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                       <div class="input-group">
                                                                    
                                                        <asp:TextBox ID="f_date" runat="server" class="form-control datepicker mydatepickerclass" autocomplete="off"
                                                                    placeholder="PICK DATE"></asp:TextBox>
                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                       </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">    <asp:Label ID="ps_lbl6" runat="server"></asp:Label> <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                                                   
                                                         <asp:TextBox ID="t_date" runat="server" class="form-control datepicker mydatepickerclass" autocomplete="off"
                                                                    placeholder="PICK DATE"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl7" runat="server"></asp:Label> <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="DD_Kategori" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" AutoPostBack="true" onselectedindexchanged="DD_Kategori_SelectedIndexChanged" >
                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   <asp:Label ID="ps_lbl8" runat="server"></asp:Label> <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="DD_Sub" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"  AutoPostBack="true" 
                                                   onselectedindexchanged="DD_Sub_SelectedIndexChanged" >
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">   <asp:Label ID="ps_lbl9" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="DropDownList1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                        </div>
                                    </div>
               
                              <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                               <asp:Button ID="Btn_Cetak" runat="server" class="btn btn-danger" Text="Cetak"  onclick="Btn_Cetak_Click" />                                                    
                                 <asp:Button ID="Button3" runat="server" class="btn btn-default" UseSubmitBehavior="false" onclick="rst_clk" Text="Set Semula" />
                            </div>
                           </div>
                               </div>
                           <div class="box-body">&nbsp;
                                    </div>


                           <div class="row">
                                        <div class="col-md-12 col-sm-2" style="text-align: center">
                                            <div class="body collapse in">
                                                <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
                                                </asp:ScriptManager>--%>
                                                   
                                     <rsweb:ReportViewer ID="Rptviwer_lt" runat="server"></rsweb:ReportViewer>
                                     <asp:Label runat="server" ID="ReportErrorMessage" Visible="false" CssClass="report-error-message"></asp:Label>
                                    
                                 
                                            </div>
                                        </div>
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
           
    </asp:UpdatePanel>
</asp:Content>
