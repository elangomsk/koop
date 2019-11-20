<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Aset/Ast_klupus.aspx.cs" Inherits="Ast_klupus" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
      <%-- <script>
     $(function () {

         $('#<%=gvSelected.ClientID %>').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
             "responsive": true,
             "sPaginationType": "full_numbers",
             "iDisplayLength": 15,
             "aLengthMenu": [[15, 30, 50, 100], [15, 30, 50, 100]]
         });
     });
</script> --%>
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
                        <h1>Semakan Pelupusan Aset</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Pengurusan Aset</a></li>
                            <li class="active">Semakan Pelupusan Aset</li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
      
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title">Carian Maklumat Pelupusan Aset</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                         <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                         <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Mula </label>
                                    <div class="col-sm-8">
                                       <div class="input-group">
                                       <asp:TextBox ID="txt_dar" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                                        placeholder="DD/MM/YYYY"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Akhir</label>
                                    <div class="col-sm-8">
                                       <div class="input-group">
                                         <asp:TextBox ID="txt_seh" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                                        placeholder="DD/MM/YYYY"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Keterangan</label>
                                    <div class="col-sm-8">
                                          <asp:DropDownList ID="DropDownList3" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional] uppercase">
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
                                 <asp:Button ID="Btn_Carian" runat="server" class="btn btn-primary" Text="Carian" UseSubmitBehavior="false"
                                                            OnClick="clk_srch" />
                                                        <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula"
                                                            UseSubmitBehavior="false" OnClick="click_rst" />
                            </div>
                           </div>
                               </div>
                         <div class="box-header with-border">
                            <h3 class="box-title">Senarai Aset Untuk Dilupuskan</h3>
                        </div>
                        <div class="box-body">&nbsp;</div>
                               
                            <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                 <asp:GridView ID="gvSelected" runat="server" class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" ShowFooter="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25"
                                                            OnRowDataBound="grdViewProducts_RowDataBound" OnRowCreated="grdViewProducts_RowCreated"
                                                             OnPageIndexChanging="gv_refdata_PageIndexChanging">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="BIL">
                                                                    <ItemStyle HorizontalAlign="center" Width="3%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                            ItemStyle-Width="150" />
                                                                        <asp:RadioButton ID="RadioButton1" Visible="false" runat="server" Checked="true" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ORGANISASI">
                                                                    <ItemStyle HorizontalAlign="Left" Width="15%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("org") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="KETERANGAN ASET">
                                                                    <ItemStyle HorizontalAlign="Left" Width="15%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("naset") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="KUANTITI">
                                                                    <ItemStyle HorizontalAlign="center" Width="5%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label6" runat="server" Text='<%# Eval("qty") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="USIA ASET">
                                                                    <ItemStyle HorizontalAlign="center" Width="5%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label7" runat="server" Text='<%# Eval("uaset") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="SEUNIT (RM)">
                                                                    <ItemStyle HorizontalAlign="Right" Width="8%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label8" runat="server" Text='<%# Eval("hps","{0:n}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="JUMLAH (RM)">
                                                                    <ItemStyle HorizontalAlign="Right" Width="8%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label9" runat="server" Text='<%# Eval("hpj","{0:n}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="SEUNIT (RM)">
                                                                    <ItemStyle HorizontalAlign="Right" Width="8%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label10" runat="server" Text='<%# Eval("nss","{0:n}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="JUMLAH (RM)">
                                                                    <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                                    <ItemStyle HorizontalAlign="Right" Width="8%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label11" runat="server" Text='<%# Eval("nsj","{0:n}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="KAEDAH PELUPUSAN">
                                                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_kp" runat="server" Text='<%# Eval("kp") %>'></asp:Label>
                                                                         <asp:Label ID="lbl_ascd" Visible="false" runat="server" Text='<%# Eval("dis_asset_id") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="NILAI REZAB (RM)">
                                                                    <ItemStyle HorizontalAlign="Right" Width="15%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_ramt" runat="server" Text='<%# Eval("res_amt","{0:n}") %>'></asp:Label>
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
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Kelulusan </label>
                                    <div class="col-sm-8">
                                      <div class="input-group">
                                    <asp:TextBox ID="TextBox2" runat="server" class="form-control validate[optional] datepicker mydatepickerclass" placeholder="PICK DATE"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                           <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Jawatan Kuasa Penilaian Aset</h3>
                        </div>
                        <div class="box-body">&nbsp;</div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Pengerusi </label>
                                    <div class="col-sm-8">
                                      <asp:DropDownList ID="dd_pen" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional] uppercase">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">AJK 1</label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="dd_ajk1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional] uppercase">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">AJK 2</label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="dd_ajk2" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional] uppercase">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                           <div class="row" style="text-align: center; display:none;">
                             <div class="col-md-12">
                                  <div class="col-md-5 box-body" style="text-align:center;">&nbsp;</div>
                                  <div class="col-md-1 box-body" style="text-align:center;">
                                <asp:DropDownList runat="server" CssClass="form-control uppercase" ID="sel_frmt">
                                                <asp:ListItem Value="01">PDF</asp:ListItem>
                                                <asp:ListItem Value="02">EXCEL</asp:ListItem>
                                               <%-- <asp:ListItem  Value="03">Word</asp:ListItem>--%>
                                                </asp:DropDownList>
                            </div>
                                 <div class="col-md-1 box-body" style="text-align:center;">
                               <asp:Button ID="Btn_Cetak" runat="server" class="btn btn-warning" UseSubmitBehavior="false" OnClick="ctk_values" Text="Cetak" />
                            </div>
                                 <div class="col-md-5 box-body" style="text-align:center;">&nbsp;</div>
                           </div>
                               </div>
                             <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                   <asp:Button ID="Button2" runat="server" class="btn btn-danger" UseSubmitBehavior="false"
                                                                OnClick="insert_values" Text="Simpan" />
                                                            <asp:Button ID="Button3" runat="server" class="btn btn-warning" UseSubmitBehavior="false"
                                                                OnClick="ctk_values" Text="Cetak" />
                            </div>
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

