<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Aset/Ast_pelupusan.aspx.cs" Inherits="Ast_pelupusan" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       <script>
     $(function () {

         $('#<%=gvSelected.ClientID %>').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
             "responsive": true,
             "sPaginationType": "full_numbers",
             "iDisplayLength": 15,
             "aLengthMenu": [[15, 30, 50, 100], [15, 30, 50, 100]]
         });
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
                        <h1>Pendaftaran Pelupusan Aset</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Pengurusan Aset</a></li>
                            <li class="active">Pendaftaran Pelupusan Aset</li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
      
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title">Senarai Aset Untuk Dilupuskan</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                         <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                               
                            <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                 <asp:GridView ID="gvSelected" runat="server" class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="1000000" OnRowDataBound="gvEmp_RowDataBound" OnPageIndexChanging="gv_refdata_PageIndexChanging">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="BIL">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                                    ItemStyle-Width="150" />
                                                                                     <asp:RadioButton ID="RadioButton1" Visible="false" runat="server" Checked="true"/>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="ORGANISASI">
                                                                        <ItemStyle HorizontalAlign="Left" Width="15%"/> 
                                                                            <ItemTemplate>
                                                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("org_name") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="KATEGORI ASET">
                                                                         <ItemStyle HorizontalAlign="Left" Width="10%"/>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("ast_kategori_desc") %>'></asp:Label>
                                                                                <asp:Label ID="Label_ccd" Visible="false" runat="server" Text='<%# Eval("sas_asset_cat_cd") %>'></asp:Label>
                                                                                <asp:Label ID="Label_sccd" Visible="false" runat="server" Text='<%# Eval("sas_asset_sub_cat_cd") %>'></asp:Label>
                                                                                <asp:Label ID="Label_atcd" Visible="false" runat="server" Text='<%# Eval("sas_asset_type_cd") %>'></asp:Label>
                                                                                <asp:Label ID="Label_acd" Visible="false" runat="server" Text='<%# Eval("sas_asset_cd") %>'></asp:Label>
                                                                                <asp:Label ID="Label_oid" Visible="false" runat="server" Text='<%# Eval("sas_org_id") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="JENIS ASET">
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%"/>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("ast_jeniaset_desc") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                         <asp:TemplateField HeaderText="KETERANGAN ASET">
                                                                         <ItemStyle HorizontalAlign="Left" Width="10%"/>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("cas_asset_desc") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                          <asp:TemplateField HeaderText="ASET ID">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label8" runat="server" Text='<%# Eval("sas_asset_id") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="TARIKH PEROLEHAN">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label9" runat="server" Text='<%# Eval("a1") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="USIA ASET">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label10" runat="server" Text='<%# Eval("a2") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="NILAI PEROLEHAN (RM)">
                                                                        <ItemStyle HorizontalAlign="Right" Width="10%"/> 
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label11" runat="server" Text='<%# Eval("a3","{0:n}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="NILAI SEMASA (RM)">
                                                                        <ItemStyle HorizontalAlign="Right" Width="10%"/> 
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label12" runat="server" Text='<%# Eval("sas_curr_price_amt","{0:n}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="KAEDAH PELUPUSAN">
                                                                        <ItemStyle HorizontalAlign="Center" Width="15%"/> 
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList ID="dd_lab1" CssClass=" form-control" runat="server">
                                                                                <asp:ListItem Value="">--- PILIH ---</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
               </div>
          </div>
                          
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-4 box-body" style="text-align:center;">&nbsp;</div>
                            <div class="col-md-1 box-body" style="text-align:center;">
                                 <asp:Button ID="Button3" runat="server" class="btn btn-danger" UseSubmitBehavior="false" OnClick="insert_values" Text="Simpan" />
                            </div>
                                  <div class="col-md-2 box-body" style="text-align:center;">
                                <asp:DropDownList runat="server" CssClass="form-control uppercase" ID="sel_frmt">
                                                <asp:ListItem Value="01">PDF</asp:ListItem>
                                                <asp:ListItem Value="02">EXCEL</asp:ListItem>
                                               <%-- <asp:ListItem  Value="03">Word</asp:ListItem>--%>
                                                </asp:DropDownList>
                            </div>
                                 <div class="col-md-1 box-body" style="text-align:center;">
                               <asp:Button ID="Btn_Cetak" runat="server" class="btn btn-warning" UseSubmitBehavior="false" OnClick="ctk_values" Text="Cetak" />
                            </div>
                                 <div class="col-md-4 box-body" style="text-align:center;">&nbsp;</div>
                           </div>
                               </div>
                              <div class="row">
                                   <div class="col-md-12 col-sm-4" style="text-align:center; line-height:13px;">
                                     <rsweb:ReportViewer ID="RptviwerStudent" runat="server" Width="50%"></rsweb:ReportViewer>
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

