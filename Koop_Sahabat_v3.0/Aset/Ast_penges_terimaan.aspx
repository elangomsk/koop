<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Aset/Ast_penges_terimaan.aspx.cs" Inherits="Ast_penges_terimaan" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
      <script>
     $(function () {
         $('.select2').select2();
     });
</script> 
      
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server" ScriptMode="Release">
    </asp:ScriptManager>

     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" style="height: 895px;">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>Pendaftaran Terimaan Aset</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Pengurusan Aset</a></li>
                            <li class="active">Pendaftaran Terimaan Aset</li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
      
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Lokasi</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                         <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                               <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No PO </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtnopo" runat="server" class="form-control validate[optional] uppercase" MaxLength="1000"></asp:TextBox>
                                        <br />
                                               <label><asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBox2_CheckedChanged" />&nbsp; Semua DO</label> 
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="box-header with-border">
                            <h3 class="box-title">Senarai Maklumat Penghantaran Aset </h3>
                        </div>
                             <div class="box-body">&nbsp;</div>
                              <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                <asp:GridView ID="GridView2" runat="server" class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" OnPageIndexChanging="gv_refdata_PageIndexChanging">
                                    <PagerStyle CssClass="pager" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="BIL" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                            ItemStyle-Width="3%" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="NO DO" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lblSubItemName" runat="server" Text='<%# Eval("acp_do_no")%>'
                                                                            CommandArgument=' <%#Eval("acp_do_no")+","+ Eval("acp_receive_id")+","+ Eval("acp_receive_dt")%>'
                                                                            CommandName="Add" OnClick="lblSubItemName_Click">
                                                <a  href="#"></a>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="stf_name" HeaderText="PEGAWAI PENERIMA" />
                                                                <asp:BoundField DataField="acp_receive_dt" HeaderText="TARIKH TERIMAAN "  ItemStyle-HorizontalAlign="Center"/>
                                                                <asp:BoundField DataField="sup_name" HeaderText="NAMA PEMBEKAL" />
                                                            </Columns>
                                    <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                       <%-- <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />--%>
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                        </asp:GridView>
               </div>
          </div>
                              <div class="box-header with-border">
                            <h3 class="box-title">Senarai Maklumat Terimaan Aset </h3>
                        </div>
                             <div class="box-body">&nbsp;</div>
                              <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                 <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" OnPageIndexChanging="gv_refdata_PageIndexChanging1">
                                     <PagerStyle CssClass="pager" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="BIL" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="ast_kategori_desc" ItemStyle-HorizontalAlign="Left" HeaderText="KATEGORI ASET" />
                                                                <asp:BoundField DataField="ast_subkateast_desc" ItemStyle-HorizontalAlign="Left"
                                                                    HeaderText="SUB KATEGORI ASET" />
                                                                <asp:BoundField DataField="ast_jeniaset_desc" ItemStyle-HorizontalAlign="Left" HeaderText="JENIS ASET" />
                                                                <asp:BoundField DataField="cas_asset_desc" ItemStyle-HorizontalAlign="Left" HeaderText="KETERANGAN ASET " />
                                                                <asp:BoundField DataField="pur_verify_qty" HeaderText="KUANTITI DIPESAN" ItemStyle-HorizontalAlign="Center"/>
                                                                <asp:BoundField DataField="acp_receive_qty" HeaderText="KUANTITI DITERIMA " ItemStyle-HorizontalAlign="Center"/>
                                                                <asp:BoundField DataField="acp_receive_remark" ItemStyle-HorizontalAlign="Left" HeaderText="CATATAN " />
                                                                <asp:BoundField DataField="strTarikhTerimaan" HeaderText="TARIKH TERIMAAN" ItemStyle-HorizontalAlign="Center"/>
                                                            </Columns>
                                     <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                       <%-- <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />--%>
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                        </asp:GridView>
               </div>
          </div>
                            <div class="box-header with-border">
                            <h3 class="box-title">Pengesahan Penyelia </h3>
                        </div>
                             <div class="box-body">&nbsp;</div>
                              <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Status Pengesahan <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="DD_down" runat="server" class="form-control uppercase">
                                                            <asp:ListItem Value="">--- PILIH ---</asp:ListItem>
                                                            <asp:ListItem Value="S">SAH</asp:ListItem>
                                                            <asp:ListItem Value="TS">TIDAK SAH</asp:ListItem>
                                                        </asp:DropDownList>     
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama Penyelia </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox3" runat="server" class="form-control uppercase "></asp:TextBox>
                                                        <asp:TextBox ID="TextBox1" runat="server" Visible="false" class="form-control uppercase "></asp:TextBox>    
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Catatan </label>
                                    <div class="col-sm-8">
                                      <textarea id="txtalamat" runat="server" class="form-control uppercase"></textarea>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                          <asp:TextBox ID="TextBox2" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                Placeholder="DD/MM/YYYY"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
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
                                <asp:TextBox ID="ver_id" Visible="false" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                                                         <asp:TextBox ID="get_id" Visible="false" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                                <asp:Button ID="Btn_Kemaskini" runat="server" class="btn btn-danger" Text="Simpan" OnClick="Btn_Kemaskini_Click" />
                                <asp:Button ID="Button1" runat="server" class="btn btn-warning" Text="Cetak" UseSubmitBehavior="false" OnClick="Cetak_Click" />
                                <asp:Button ID="Button2" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" OnClick="Click_bck" />
                            </div>
                           </div>
                               </div>
                              <div class="row">
                             <div class="col-md-12">
                                    <rsweb:ReportViewer ID="ReportViewer1" runat="server">
                                                        </rsweb:ReportViewer>
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
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

