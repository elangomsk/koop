<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Aset/Ast_kelulusan_po.aspx.cs" Inherits="Ast_kelulusan_po" %>
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

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1 id="h1_tag" runat="server">  Kelulusan Belian Aset</h1>
                        <ol class="breadcrumb">
                            <li id="bb1_text" runat="server"><a href="#"><i class="fa fa-dashboard"></i>Pengurusan Aset</a></li>
                            <li class="active" id="bb2_text" runat="server">Kelulusan Belian Aset</li>
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
                            <h3 class="box-title" id="h3_tag" runat="server">Maklumat Belian Aset</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                         <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                               <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl1_text" runat="server">No PO </label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="txt_nopo" runat="server" class="form-control validate[optional] uppercase"
                                                            MaxLength="1000"></asp:TextBox>
                                                             <asp:Panel ID="autocompleteDropDownPanel" runat="server" 
                                                                                    ScrollBars="Auto" Height="150px" Font-Size="Medium" 
                                                                                    HorizontalAlign="Left" Wrap="False" />
                                                                                 <cc1:AutoCompleteExtender ServiceMethod="GetCompletionList" MinimumPrefixLength="1"
                                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txt_nopo"
                                                            ID="AutoCompleteExtender1" CompletionListElementID="autocompleteDropDownPanel" CompletionListCssClass="form-control uppercase" runat="server" FirstRowSelected="false">
                                                        </cc1:AutoCompleteExtender>
                                    </div>
                                </div>
                            </div>
                                  <%-- <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-8">
                                       <asp:Button ID="Carian" runat="server" class="btn btn-danger" Text="Carian" OnClick="Carian_Click"  UseSubmitBehavior="false"/>
                                                <asp:Button ID="Button7" runat="server" class="btn btn-danger" Text="Set Semula" OnClick="Button5_Click" usesubmitbehavior="false"/>
                                    </div>
                                </div>
                            </div>--%>
                                 </div>
                                </div>
                           <div class="box-header with-border">
                            <h3 class="box-title" id="h3_tag1" runat="server">Maklumat Pemohon</h3>
                        </div>
                            <div class="box-body">&nbsp;</div>
                            <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl2_text" runat="server">No Kakitangan</label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="txt_nokaki" runat="server" class="form-control validate[optional] uppercase"
                                                    MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl3_text" runat="server">Nama Kakitangan</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txt_nanakaki" runat="server" class="form-control validate[optional] uppercase" ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                         
                              <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl4_text" runat="server">Organisasi</label>
                                    <div class="col-sm-8">
                                    <asp:TextBox ID="txt_org" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl5_text" runat="server">Jabatan</label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="txt_jaba" runat="server" class="form-control validate[optional] uppercase" ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl6_text" runat="server">Unit</label>
                                    <div class="col-sm-8">
                                   <asp:TextBox ID="txt_unit" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                           <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl7_text" runat="server">Tarikh Permohonan</label>
                                    <div class="col-sm-8">
                                   <asp:TextBox ID="txt_tarik" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="box-header with-border">
                            <h3 class="box-title" id="h3_tag2" runat="server">Senarai Aset Untuk Dibeli</h3>
                        </div>
                            <div class="box-body">&nbsp;</div>
                             <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                 <asp:GridView ID="GridView1" runat="server"  class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" OnPageIndexChanging="gv_refdata_PageIndexChanging">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="BIL">
                                                                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                            ItemStyle-Width="150" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="KATEGORI ASET">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("ast_kategori_desc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="SUB KATEGORI ASET">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("ast_subkateast_desc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="JENIS ASET">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("ast_jeniaset_desc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="KETERANGAN ASET">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("cas_asset_desc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="KUANTITI DIPOHON">
                                                                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label6" runat="server" Text='<%# Eval("pur_asset_qty") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="KUANTITI DILULUSKAN">
                                                                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label7" runat="server" Text='<%# Eval("pur_verify_qty") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="AMAUN (RM) / UNIT">
                                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label8" runat="server" Text='<%# Eval("pur_asset_amt","{0:N}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="JUMLAH (RM) ">
                                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label9" runat="server" Text='<%# Eval("pur_asset_tot_amt","{0:N}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PV No.">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label9" runat="server" Text='<%# Eval("pur_pv_no") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="Invois No.">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label9" runat="server" Text='<%# Eval("pur_inv_no") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="Cek No.">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label9" runat="server" Text='<%# Eval("pur_cek_no") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="Tarikh PV">
                                                                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label9" runat="server" Text='<%# Eval("pur_tarikh_bayaran") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="Mohon Bayar Status">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label9" runat="server" Text='<%# Eval("pur_mp_sts") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
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
                             <div class="box-body">&nbsp;
                                    </div>
                             <div class="box-header with-border">
                            <h3 class="box-title" id="h3_tag3" runat="server">Maklumat Pengesahan Belian Aset</h3>
                        </div>
                            <div class="box-body">&nbsp;</div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl8_text" runat="server">Status Pengesahan </label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="DD_stsPengesahan1" runat="server" class="form-control validate[optional] uppercase">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl9_text" runat="server">Catatan</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txt_Catatan1" TextMode="multiline" Columns="20" Rows="2" runat="server"
                                                            class="form-control validate[optional] uppercase" />
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                               <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl10_text" runat="server">Pegawai Pengesah </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="txt_Pengesah1" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl11_text" runat="server">Tarikh Pengesahan</label>
                                    <div class="col-sm-8">
                                           <div class="input-group">
                                          <asp:TextBox ID="txt_Pengesahan1" runat="server" class="form-control validate[optional]" ReadOnly="true"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <div class="box-header with-border">
                            <h3 class="box-title" id="h3_tag4" runat="server">Maklumat Kelulusan Belian Aset</h3>
                        </div>
                             <div class="box-body">&nbsp;</div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl12_text" runat="server">Status Kelulusan </label>
                                    <div class="col-sm-8">
                                    <asp:DropDownList ID="DD_stsPengesahan2" runat="server" class="form-control validate[optional] uppercase">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl13_text" runat="server">Catatan</label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="txt_Catatan2" TextMode="multiline" Columns="20" Rows="2" runat="server"
                                                            class="form-control validate[optional] uppercase" />
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl14_text" runat="server">Pegawai Kelulusan </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="txt_Pengesah2" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                            </div>
                                </div>
                                    <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl15_text" runat="server">Tarikh Kelulusan </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                         <asp:TextBox ID="txt_Pengesahan2" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                placeholder="DD/MM/YYYY"></asp:TextBox>
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
                                <asp:Button ID="Simpan" runat="server" class="btn btn-danger" Text="Simpan" UseSubmitBehavior="false" OnClick="Simpan_Click" />
                                <asp:Button ID="Cetak" runat="server" class="btn btn-warning" Text="Cetak" UseSubmitBehavior="false" OnClick="Cetak_Click" />
                                <asp:Button ID="Button5" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" OnClick="Click_bck" />
                            </div>
                           </div>
                               </div>
                           
                                  <div class="row">
                                                <div class="col-md-12" style="text-align: center">
                                                        <rsweb:ReportViewer ID="ReportViewer1" runat="server">
                                                        </rsweb:ReportViewer>
                                                </div>
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

