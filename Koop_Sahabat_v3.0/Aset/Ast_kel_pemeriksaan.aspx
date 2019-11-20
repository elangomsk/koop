<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Aset/Ast_kel_pemeriksaan.aspx.cs" Inherits="Ast_kel_pemeriksaan" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
         <script type="text/javascript">
        function RadioCheck(rb) {
            var gv = document.getElementById("<%=GridView1.ClientID%>");
            var rbs = gv.getElementsByTagName("input");
            var row = rb.parentNode.parentNode;
            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "radio") {
                    if (rbs[i].checked && rbs[i] != rb) {
                        rbs[i].checked = false;
                        break;
                    }
                }
            }
        }    
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
                        <h1>Kelulusan Pemeriksaan Aset</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Pengurusan Aset</a></li>
                            <li class="active">Kelulusan Pemeriksaan Aset</li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
      
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title">Carian Maklumat Aduan</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                         <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                               <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Dari <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                       <asp:TextBox ID="TextBox1" runat="server"  class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Kod Lokasi</label>
                                    <div class="col-sm-8">
                                      <div class="input-group">
                                        <asp:TextBox ID="TextBox4" runat="server"  class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Status Pemeriksaan</label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" ID="DD_stspemeri">
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
                                  <asp:Button ID="Button10" runat="server" class="btn btn-primary" Text="Carian" UseSubmitBehavior="false"
                                                            OnClick="Button1_Click" />
                                <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" UseSubmitBehavior="false" OnClick="Button5_Click" />
                                <asp:Button ID="Button2" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" OnClick="Click_bck" />
                            </div>
                           </div>
                               </div>
                            <div class="box-body">&nbsp;
                                    </div>
                           <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                  <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" OnPageIndexChanging="gv_refdata_PageIndexChanging1">
                                       <PagerStyle CssClass="pager" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="BIL">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                            ItemStyle-Width="150" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ORGANIASASI">
                                                                    <ItemStyle HorizontalAlign="Left" Font-Underline Font-Bold></ItemStyle>
                                                                    <ItemTemplate>
                                                                         <asp:LinkButton runat="server" ID="lnkView1" OnClick="lnkView1_Click">
                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("org_name") %>'></asp:Label>
                                                                              </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="JABATAN">
                                                                    <ItemStyle HorizontalAlign="Left" Width="25%"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label11" runat="server" Text='<%# Eval("sas_asset_id") %>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("hr_jaba_desc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                               
                                                                <asp:TemplateField HeaderText="KATEGORI ASET">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label6" runat="server" Text='<%# Eval("ast_kategori_desc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="SUB KATEGORI ASET">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label6_1" runat="server" Text='<%# Eval("ast_subkateast_desc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="JENIS ASET">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label6_2" runat="server" Text='<%# Eval("ast_jeniaset_desc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="ASET ID">
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label6_aid" runat="server" Text='<%# Eval("sas_asset_id") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="STATUS PEMIRIKASAAN">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label6_3" runat="server" Text='<%# Eval("ast_pemerikksaan_desc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="JUSTIFIKASI">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="Label6_4" runat="server" Text='<%# Eval("ast_justifikasi_desc") %>'></asp:Label>
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
                             <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                  <asp:GridView ID="GridView2" runat="server" class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" OnRowDataBound="gvEmp_RowDataBound"
                                                            ShowFooter="true" OnPageIndexChanging="gv_refdata_PageIndexChanging">
                                       <PagerStyle CssClass="pager" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="BIL" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                            ItemStyle-Width="150" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ORGANIASASI">
                                                                    <ItemStyle HorizontalAlign="Left" Font-Bold Font-Underline></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" ID="lnkView2" OnClick="lnkView2_Click">
                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("org_name") %>'></asp:Label>
                                                                              </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="JABATAN">
                                                                    <ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label10" runat="server" Text='<%# Eval("sas_asset_id") %>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("hr_jaba_desc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="KATEGORI ASET">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label6" runat="server" Text='<%# Eval("ast_kategori_desc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="SUB KATEGORI ASET">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label6_1" runat="server" Text='<%# Eval("ast_subkateast_desc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="JENIS ASET">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label6_2" runat="server" Text='<%# Eval("ast_jeniaset_desc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="ASET ID">
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label6_aid" runat="server" Text='<%# Eval("sas_asset_id") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="STATUS PEMIRIKASAAN">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label6_3" runat="server" Text='<%# Eval("ast_pemerikksaan_desc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="JUSTIFIKASI">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label6_5" runat="server" Text='<%# Eval("ast_justifikasi_desc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="KOS (RM)">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label6_4" runat="server" Text='<%# Eval("sas_repair_amt","{0:N}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbltotal" Style="text-align: right; font-weight: bold;" ReadOnly="true"
                                                                            runat="server"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <FooterStyle HorizontalAlign="Right" />
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
                            <div class="box-header with-border">
                            <h3 class="box-title">Ulasan Penyelia Pemeriksa</h3>
                        </div>
                         <div class="box-body">&nbsp;</div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Catatan</label>
                                    <div class="col-sm-8">
                                          <textarea id="TextBox2" runat="server" class="form-control validate[optional] uppercase" maxlength="500"></textarea>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox11" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh</label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                        <asp:TextBox ID="tb2" runat="server"  class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jawatan </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox3" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <div class="box-header with-border">
                            <h3 class="box-title">Ulasan Pihak Pengurusan</h3>
                        </div>
                         <div class="box-body">&nbsp;</div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Catatan</label>
                                    <div class="col-sm-8">
                                         <textarea id="TextBox6" runat="server" class="form-control validate[optional] uppercase" maxlength="500"></textarea>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama </label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="TextBox10" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh</label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                       <asp:TextBox ID="TextBox8" runat="server"  class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jawatan </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox7" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Pegawai Aset</h3>
                        </div>
                         <div class="box-body">&nbsp;</div>
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Kelulusan</label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                     <asp:TextBox ID="TextBox9" runat="server"  class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"> </asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama Pegawai </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox5" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox12" runat="server" Visible="false" class="form-control validate[optional] uppercase"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox13" runat="server" Visible="false" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                <asp:Button ID="Button5" runat="server" class="btn btn-warning" Text="Cetak Borang" UseSubmitBehavior="false"
                                                            OnClick="Button51_Click" />
                                                        <asp:Button ID="Button6" runat="server" class="btn btn-danger" Text="Simpan" UseSubmitBehavior="false" OnClick="Button2_Click" />
                                                        <asp:Button ID="Button3" runat="server" class="btn btn-warning" Text="Cetak" Visible="false" UseSubmitBehavior="false" OnClick="cetak_Click" />
                            </div>
                           </div>
                               </div>
                              <div class="row">
                                                <div class="col-md-12" style="text-align: center">
                                                        <rsweb:ReportViewer ID="ReportViewer1" runat="server">
                                                        </rsweb:ReportViewer>
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
    </asp:UpdatePanel>
</asp:Content>

