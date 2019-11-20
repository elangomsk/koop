<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Aset/Ast_aduan.aspx.cs" Inherits="Ast_aduan" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
                        <h1>Aduan Kerosakan</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Pengurusan Aset</a></li>
                            <li class="active">Aduan Kerosakan</li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
      
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Pengadu</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                         <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                               <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Kakitangan </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox4" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Organisasi</label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox5" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama Kakitangan</label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox6" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jabatan</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox7" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jawatan</label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox8" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Aduan</label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox9" runat="server" class="form-control validate[optional]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                               <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Aset Untuk Diselenggara</h3>
                        </div>
                            <div class="box-body">&nbsp;</div>
                            <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                  <asp:GridView ID="GridView1"  runat="server" class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" OnPageIndexChanging="gv_refdata_PageIndexChanging1">
                                      <PagerStyle CssClass="pager" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="BIL" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                                    ItemStyle-Width="150" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="KATEGORI ASET">
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%"/> 
                                                                            <ItemTemplate>
                                                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("ast_kategori_desc") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="SUB KATEGORI ASET">
                                                                        <ItemStyle HorizontalAlign="Left" Width="20%"/> 
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("ast_subkateast_desc") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="JENIS ASET">
                                                                        <ItemStyle HorizontalAlign="Left" Width="20%"/> 
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("ast_jeniaset_desc") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                         <asp:TemplateField HeaderText="KETERANGAN ASET">
                                                                         <ItemStyle HorizontalAlign="Left" Width="20%"/> 
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("cas_asset_desc") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                          <asp:TemplateField HeaderText="ASET ID"  ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label8" runat="server" Text='<%# Bind("sas_asset_id") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                         <asp:TemplateField HeaderText="PILIH"  ItemStyle-HorizontalAlign="Center">
                                                                           <ItemTemplate>
                                                                           <asp:Label ID="Label9" Visible="false" runat="server" Text='<%# Bind("sas_asset_cat_cd") %>'></asp:Label>
                                                                           <asp:Label ID="Label1" Visible="false" runat="server" Text='<%# Bind("sas_asset_sub_cat_cd") %>'></asp:Label>
                                                                           <asp:Label ID="Label4" Visible="false" runat="server" Text='<%# Bind("sas_asset_type_cd") %>'></asp:Label>
                                                                           <asp:Label ID="Label5" Visible="false" runat="server" Text='<%# Bind("sas_asset_cd") %>'></asp:Label>
                                                                        <asp:RadioButton ID="RadioButton1" runat="server" onclick = "RadioCheck(this);" />
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
                              <div class="box-header with-border">
                            <h3 class="box-title">Butiran Aduan</h3>
                        </div>
                            <div class="box-body">&nbsp;</div>
                              <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Aduan <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                       <textarea id="txt_area" runat="server" class=" form-control uppercase" rows="3" maxlength="1000"></textarea>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-8">
                                      <asp:Button ID="Button3" runat="server" class="btn btn-danger" Text="Simpan" OnClick="insert_values" UseSubmitBehavior="false" />
                                        <asp:Button ID="Button5" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" OnClick="Click_bck" />
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="box-header with-border">
                            <h3 class="box-title">Senarai Maklumat Aduan</h3>
                        </div>
                            <div class="box-body">&nbsp;</div>
                             <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                <asp:GridView ID="gvSelected"  runat="server" class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" OnPageIndexChanging="gv_refdata_PageIndexChanging">
                                    <PagerStyle CssClass="pager" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="BIL"  ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                                    ItemStyle-Width="150" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="NO ADUAN">
                                                                        <ItemStyle HorizontalAlign="Center" Width="10%"/> 
                                                                            <ItemTemplate>
                                                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("cmp_id") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Aset Id">
                                                                        <ItemStyle HorizontalAlign="Center" Width="10%"/> 
                                                                            <ItemTemplate>
                                                                                    <asp:Label ID="Label3_aid" runat="server" Text='<%# Eval("cmp_asset_id") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="TARIKH ADUAN">
                                                                        <ItemStyle HorizontalAlign="Center" Width="10%"/>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("cmp_complaint_dt","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="KETERANGAN ASET">
                                                                        <ItemStyle HorizontalAlign="Left"/>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("cas_asset_desc") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                         <asp:TemplateField HeaderText="PERIHAL KEROSAKAN">
                                                                         <ItemStyle HorizontalAlign="Left"/>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("cmp_remark") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                          <asp:TemplateField HeaderText="STATUS">
                                                                          <ItemStyle HorizontalAlign="center"/>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label8" runat="server" Text='<%# Bind("ss1") %>'></asp:Label>
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

