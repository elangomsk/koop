<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../settings/site_settings.aspx.cs" Inherits="site_settings" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function ShowImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=ImgPrv.ClientID%>').prop('src', e.target.result)
                        .width(100)
                        .height(100);
                };
                reader.readAsDataURL(input.files[0]);
                }
            }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <%--  <asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                <span style="border-width: 0px; position: fixed; font-weight: bold; padding: 50px; background-color: #FFFFFF; font-size: 16px; left: 40%; top: 40%;">Sila Tunggu. Rekod
                    Sedang Diproses ...</span>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
       <%--  <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>--%>
                <div class="content-wrapper" style="height: 895px;">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>Site Settings</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Kewangan</a></li>
                            <li class="active">Site Settings</li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
  
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title">Maklumat</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                         <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Logo <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                          <asp:FileUpload ID="FileUpload1" CssClass="fileupload" type="file" runat="server" onchange="ShowImagePreview(this);"/>
                                        <br />
                                        <asp:Image ID="ImgPrv" runat="server" Width="100px" Height="100px" />
                                        <span style="color: Red;">(NOTA : Saiz Imej Maksimum Adalah - 100 KB)</span>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Title <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="log_title"  runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Footer Company Content <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="foot_copy"  runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="row" style="display:none;">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Footer Company Content <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="foot_comp"  runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                <asp:Button ID="Button4" runat="server" class="btn btn-warning sub_btn" Text="Kemaskini" OnClick="clk_submit"  UseSubmitBehavior="false" />
                                <a href="../KSAIMB_Home.aspx"  class="btn btn-danger sub_btn">Kembali</a>
                                <%--<asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" OnClick="btn_reset"  UseSubmitBehavior="false" />--%>
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
   <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

