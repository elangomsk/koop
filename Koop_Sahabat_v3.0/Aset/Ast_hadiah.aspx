<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Aset/Ast_hadiah.aspx.cs" Inherits="Ast_hadiah" %>
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
                            <h3 class="box-title">Maklumat Pergerakan Aset</h3>
                        </div>
                        <div class="box-body">&nbsp;</div>
                               
                            <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                  <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" OnPageIndexChanging="gv_refdata_PageIndexChanging">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="BIL">
                                                                    <ItemStyle HorizontalAlign="center" Width="3%" />
                                                                    <ItemTemplate>
                                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="150"/> 
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="KETERANGAN ASET">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("naset") %>'></asp:Label>
                                                                        <asp:Label ID="Label1" Visible="false" runat="server" Text='<%# Eval("dis_asset_id") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                <asp:TemplateField HeaderText="KUANTITI">
                                                                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("qty") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="NILAI SEMASA (RM)">
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                    <ItemTemplate>
                                                                            <asp:Label ID="Label7" runat="server" Text='<%# Eval("nss","{0:n}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PILIH">
                                                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                            <asp:CheckBox  ID="chkRow" runat="server"/>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
               </div>
          </div>
                          
          
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama Penerima </label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="TextBox2" runat="server" class="form-control validate[optional] uppercase" MaxLength="150"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Serahan </label>
                                    <div class="col-sm-8">
                                     <div class="input-group">
                                         <asp:TextBox ID="TextBox1" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                                        placeholder="DD/MM/YYYY"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                           <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Sokongan</h3>
                        </div>
                        <div class="box-body">&nbsp;</div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama </label>
                                    <div class="col-sm-8">
                                    <asp:DropDownList ID="DD_nama" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional] uppercase" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jawatan</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox3" runat="server" class="form-control validate[optional] uppercase" MaxLength="150"></asp:TextBox>
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
                                       <asp:TextBox ID="TextBox4" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                                        placeholder="DD/MM/YYYY"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
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
                                                                OnClick="Button1_Click" Text="Simpan" />
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

