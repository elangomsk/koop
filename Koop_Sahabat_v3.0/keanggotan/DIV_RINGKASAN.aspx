<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../keanggotan/DIV_RINGKASAN.aspx.cs" Inherits="DIV_RINGKASAN" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       <script type="text/javascript">
           $(function () {
            $('#GridView1').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
                "responsive": true,
                "sPaginationType": "full_numbers",
               
            });
        });
    </script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
     <asp:ScriptManager ID="ScriptManagerCalendar" AsyncPostBackTimeOut="72000" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
      <script type="text/javascript">
             // Get the instance of PageRequestManager.
             var prm = Sys.WebForms.PageRequestManager.getInstance();
             // Add initializeRequest and endRequest
             prm.add_initializeRequest(prm_InitializeRequest);
             prm.add_endRequest(prm_EndRequest);

             // Called when async postback begins
             function prm_InitializeRequest(sender, args) {
                 // get the divImage and set it to visible
                 var panelProg = $get('divImage');
                 panelProg.style.display = '';
                 // reset label text
                 var lbl = $get('<%= this.lblText.ClientID %>');
                 lbl.innerHTML = '';

                 // Disable button that caused a postback
                 $get(args._postBackElement.id).disabled = true;
             }

             // Called when async postback ends
             function prm_EndRequest(sender, args) {
                 // get the divImage and hide it again
                 var panelProg = $get('divImage');
                 panelProg.style.display = 'none';

                 // Enable button that caused a postback
                 $get(sender._postBackSettings.sourceElement.id).disabled = false;
             }
         </script>

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>   <asp:Label ID="ps_lbl1" runat="server"></asp:Label> </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i> <asp:Label ID="ps_lbl2" runat="server"></asp:Label>   </a></li>
                            <li class="active">   <asp:Label ID="ps_lbl3" runat="server"></asp:Label> </li>
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
                            <h3 class="box-title"> <asp:Label ID="ps_lbl4" runat="server"></asp:Label> </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">     <asp:Label ID="ps_lbl5" Text="Jenis Agihan Dividen" runat="server"></asp:Label> <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                            <%--<div class="input-group">--%>
                                                           <asp:DropDownList ID="DD_JenisAD" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>         
                                                        <%--<asp:TextBox ID="f_date" runat="server" class="form-control datepicker mydatepickerclass" autocomplete="off"
                                                                    placeholder="PICK DATE"></asp:TextBox>
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>--%>
                                                       <%--</div>--%>
                                         <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="You can't leave this Empty" CssClass="requiredFieldValidateStyle" ForeColor="Red" ControlToValidate="f_date" ValidationGroup="vgSubmit_divs_simp">
                                    </asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                            </div>

                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">    <asp:Label ID="ps_lbl6" Text="Jenis Laporan" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                            <%--<div class="input-group">--%>
                                                            <asp:DropDownList ID="DD_JenisLap" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>          
                                                      <%--<asp:TextBox ID="t_date" runat="server" class="form-control datepicker mydatepickerclass" autocomplete="off"
                                                                    placeholder="PICK DATE"></asp:TextBox>
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                       </div>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="You can't leave this Empty" CssClass="requiredFieldValidateStyle" ForeColor="Red" ControlToValidate="t_date" ValidationGroup="vgSubmit_divs_simp">
                                    </asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                            </div>
                                  </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">   <asp:Label ID="ps_lbl7" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                              <asp:DropDownList ID="DD_kaw" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" AutoPostBack="true" onselectedindexchanged="DD_kaw_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">  <asp:Label ID="ps_lbl8" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                             <asp:DropDownList ID="DD_wilayah" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" AutoPostBack="true" onselectedindexchanged="DD_wilayah_SelectedIndexChanged">
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
                                    <label for="inputEmail3" class="col-sm-4 control-label">   <asp:Label ID="ps_lbl9" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                             <asp:DropDownList ID="DD_cawangan" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
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
                                <asp:Button ID="Button2" runat="server" class="btn btn-primary" Text="Jana" onclick="Button2_Click"/>                                                                
                                    <asp:Button ID="Button3" runat="server" class="btn btn-default" UseSubmitBehavior="false" onclick="rst_clk" Text="Set Semula" />                                
                            </div> 
                           </div>
                         
                           </div>
                                        <div class="box-body">&nbsp;</div>
                              <div class="dataTables_wrapper form-inline dt-bootstrap"  style="overflow:auto;">
                                    <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
                 <div id="divImage" class="text-center" style="display:none; padding-top: 30px; font-weight:bold;">
                     <asp:Image ID="img1" runat="server" ImageUrl="../dist/img/LoaderIcon.gif" />&nbsp;&nbsp;&nbsp;Processing Please wait ... </div>
                           <div class="col-md-12">
                               <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>                               
                               <br />
                               <asp:Label ID="lblMsgGrdV" runat="server" Visible="false"></asp:Label>
                           </div>
                           </div>
                           <div class="box-body">&nbsp;</div>

                             <div class="box-header with-border" id="ss1_stap1" runat="server" visible="false">
                            <h3 class="box-title"> <asp:Label ID="ps_lbl12" runat="server"></asp:Label> </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                   
                            
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
