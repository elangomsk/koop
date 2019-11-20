<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Kewengan/kw_inventori.aspx.cs" Inherits="kw_inventori" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
       <asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                <span style="border-width: 0px; position: fixed; font-weight: bold; padding: 50px; background-color: #FFFFFF; font-size: 16px; left: 40%; top: 40%;">Sila Tunggu. Rekod
                    Sedang Diproses ...</span>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>Inventori</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Kewangan</a></li>
                            <li class="active">Inventori</li>
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
                            <h3 class="box-title">Maklumat Inventori</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                         <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jenis <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                     <asp:DropDownList id="DropDownList2" runat="server" class="form-control uppercase" OnSelectedIndexChanged="dd_jenis" AutoPostBack="true">
                                                       </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Kod Produk <span class="style1">*</span></label>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama Produk <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="TextBox4" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Kuantiti</label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox1" runat="server" class="form-control validate[optional,custom[number]] uppercase" MaxLength="5"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Kod Akaun </label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList id="DropDownList1" runat="server" class="form-control uppercase" OnSelectedIndexChanged="sel_jenis" AutoPostBack="true">
                                                       </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Baki Pembukaan (RM)</label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox10" style="text-align:right;" runat="server" class="form-control validate[optional]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                              <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Keterangan </label>
                                    <div class="col-sm-8">
                                         <textarea id="Textarea1" runat="server" class="form-control validate[optional] uppercase" rows="2" maxlength="500"></textarea>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                 <asp:TextBox ID="ver_id" Visible="false" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                                                         <asp:TextBox ID="get_id" Visible="false" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                                 <asp:Button ID="Button4" runat="server" class="btn btn-danger" Text="Simpan" OnClick="clk_submit" UseSubmitBehavior="false" />
                                <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" UseSubmitBehavior="false" OnClick="Button5_Click" />
                                <asp:Button ID="Button5" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" OnClick="Click_bck" />
                                <asp:Button ID="Button2" runat="server" class="btn btn-warning" Text="Tambah" UseSubmitBehavior="false" OnClick="Click_tambah" />
                                 
                            </div>
                           </div>
                               </div>
                            <div class="box-body">&nbsp;
                                    </div>
                               
           <div class="col-md-12 box-body">
                                  <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="20" OnPageIndexChanging="gvSelected_PageIndexChanging1">
                                                    <Columns>
                                                    <asp:TemplateField HeaderText="BIL">  
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tarikh">
                                                           <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("tarikh") %>' CssClass="uppercase"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Masa">
                                                            <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label2_fl" runat="server" Text='<%# Eval("masa") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kuantiti Masuk">
                                                            <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label2_ds" runat="server" Text='<%# Eval("kuantiti_masuk") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Kuantiti Keluar">
                                                             <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label2_bel" runat="server" Text='<%# Eval("kuantiti_keluar") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
               </div>
        
                            <div class="box-body">&nbsp;
                                    </div>
                             <cc1:ModalPopupExtender BackgroundCssClass="modalBg" DropShadow="true" ID="ModalPopupExtender1"
                                                                PopupControlID="Panel3" runat="server" TargetControlID="btnBack" PopupDragHandleControlID="Panel2"
                                                                CancelControlID="btnBack">
                                                            </cc1:ModalPopupExtender>
                                <asp:Panel ID="Panel3" runat="server" CssClass="modalPanel" Style="display: none">
                                                                <table border="0" cellpadding="6" cellspacing="0" class="tblborder">
                                                                    <tr><td colspan="3" ><h3>Aliran Stok </h3></td></tr>
                                                                    <tr>
                                                                        <td width="140px" align="left"><span class="leftpadding uppercase">Tarikh</td>
                                                                        <td width="10%" align="center">:</td>
                                                                        <td width="270px" align="left"> <asp:TextBox ID="TextBox5" runat="server" class="form-control validate[optional] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                     <tr>
                                                                        <td width="140px" align="left"><span class="leftpadding uppercase">Masa</td>
                                                                        <td width="10%" align="center">:</td>
                                                                        <td width="270px" align="left"><asp:TextBox ID="TextBox6" runat="server" class="form-control validate[optional] timepicker"></asp:TextBox></asp:TextBox></td>
                                                                    </tr>
                                                                     <tr>
                                                                        <td width="140px" align="left"><span class="leftpadding uppercase">Kuantiti Masuk</td>
                                                                        <td width="10%" align="center">:</td>
                                                                        <td width="270px" align="left"><asp:TextBox ID="TextBox8" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                     <tr>
                                                                        <td width="140px" align="left"><span class="leftpadding uppercase">Kuantiti Keluar</td>
                                                                        <td width="10%" align="center">:</td>
                                                                        <td width="270px" align="left"><asp:TextBox ID="TextBox9" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                     <tr>
                                                                        <td colspan="3" align="right">&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="3" align="center">
                                                                        <asp:TextBox ID="TextBox2" Visible="false" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                                                                        <asp:Button ID="Button3" runat="server" class="btn btn-danger sub_btn" Text="Simpan" OnClick="clk_submit1"  UseSubmitBehavior="false" />
                                                                            &nbsp;
                                                                            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-default"  Text="Keluar" />
                                                                        </td>
                                                                    </tr>
                                                                    </table>
                                    </asp:Panel>
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

