<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Aset/Ast_pen_terimaan.aspx.cs" Inherits="Ast_pen_terimaan" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
      <script>
     $(function () {
         $('.select2').select2();
     });
</script> 
         <script type="text/javascript">
        function ValidateEmail(button) {
            var row = button.parentNode.parentNode;
            var sno = $("#<%=txt_nodo.ClientID %>").val();
         
            var label1 = GetChildControl(row, "TextBox2").value;
            var label2 = GetChildControl(row, "TextBox1").value;
            var label3 = GetChildControl(row, "txt_simp").value;



            if (sno != "") {
                var Multi_l1 = (parseFloat(label2) - parseFloat(label1));
                if (label1 != "") {
                    //alert(Multi_l1);
                   if (Multi_l1 >= "0") {
                    GetChildControl(row, "txtbku").value = Multi_l1
                    GetChildControl(row, "TextBox4").value = Multi_l1;
                    GetChildControl(row, "txt_simp").value = "1";
                }
                else {
                    GetChildControl(row, "txtbku").value = label2
                    GetChildControl(row, "TextBox2").value = "";
                    //alert("Allowed only for Maximum " + label2 + " Quantity.");
                    $.Zebra_Dialog('Allowed only for Maximum "' + label2 + '" Quantity', {
                        'type': 'warning',
                        'title': 'Warning',
                        'auto_close': 1500,
                        'buttons': [
                {
                    caption: 'Ok', callback: function () {
                    }
                }
                        ]
                    });
                }
                }
                else {
                 
                        GetChildControl(row, "txtbku").value = label2;
                   
                }
            }
            else {
                GetChildControl(row, "TextBox2").value = "";
                //alert("Sila Masukkan No DO !");
                $.Zebra_Dialog('Sila Masukkan No DO', {
                    'type': 'warning',
                    'title': 'Warning',
                    'auto_close': 1500,
                    'buttons': [
            {
                caption: 'Ok', callback: function () {
                }
            }
                    ]
                });
            }

        };
        function GetChildControl(element, id) {

            var child_elements = element.getElementsByTagName("*");
            for (var i = 0; i < child_elements.length; i++) {
                if (child_elements[i].id.indexOf(id) != -1) {
                    return child_elements[i];
                }
            }
        };
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
                                        <asp:TextBox ID="txt_nopo" runat="server" class="form-control validate[optional] uppercase" MaxLength="1000"></asp:TextBox>
                                              
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama Pembekal</label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="txt_nama_pembe" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Telefon Pembekal</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txt_notele" runat="server" class="form-control validate[optional]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Alamat</label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="txt_alamat" TextMode="multiline" Columns="20" Rows="2" runat="server"
                                                    class="form-control validate[optional]  uppercase" />
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="box-header with-border" >
                            <h3 class="box-title">Senarai Maklumat Pesanan Aset </h3>
                        </div>
                             <div class="box-body">&nbsp;</div>
                              <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" OnPageIndexChanging="gv_refdata_PageIndexChanging">
                                     <PagerStyle CssClass="pager" />
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
                                                        <asp:TemplateField HeaderText="KUANTITI">
                                                              <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("pur_verify_qty") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="HARGA / UNIT (RM)" ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label8" runat="server" Text='<%# Eval("pur_asset_amt","{0:N}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="JUMLAH (RM) " ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label9" runat="server" Text='<%# Eval("pur_asset_tot_amt","{0:N}") %>'></asp:Label>
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
                            <h3 class="box-title">Senarai Semak Maklumat Terimaan Aset</h3>
                        </div>
                             <div class="box-body">&nbsp;</div>
                              <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No DO <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                   <asp:TextBox ID="txt_nodo" runat="server" class="form-control validate[optional] uppercase" MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                              <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                               <asp:GridView ID="GridView2" runat="server" class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" OnPageIndexChanging="gv_refdata_PageIndexChanging1">
                                    <PagerStyle CssClass="pager" />
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
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("a1") %>'></asp:Label>
                                                               <%-- <asp:Label ID="Label2" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SUB KATEGORI ASET">
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("a2") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="JENIS ASET">
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("a3") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="KETERANGAN ASET">
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("a4") %>'></asp:Label>
                                                                 <asp:Label ID="Label2" Visible="false" runat="server" Text='<%# Eval("a7") %>'></asp:Label>
                                                                 <asp:Label ID="Label4" Visible="false" runat="server" Text='<%# Eval("a9") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="KUANTITI">
                                                        <ItemStyle HorizontalAlign="center" Width="5%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label4_1" runat="server" Text='<%# Eval("a10") %>'></asp:Label>
                                                                  <asp:TextBox ID="txtku" runat="server" CssClass="form-control uppercase" Text='<%# Eval("a10") %>'  style="display:none;" ></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Baki Kuantiti">
                                                        <ItemStyle HorizontalAlign="center" Width="7%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label4_2" runat="server" Text='<%# Eval("a11") %>' style="display:none;"></asp:Label>
                                                                  <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control uppercase" Text='<%# Eval("a11") %>'  style="display:none;" ></asp:TextBox>
                                                                  <asp:TextBox ID="txt_simp" runat="server" CssClass="form-control uppercase" Text='<%# Eval("a12") %>'  style="display:none;" ></asp:TextBox>
                                                                   <asp:TextBox ID="txtbku" runat="server" CssClass="form-control uppercase" Text='<%# Eval("a11") %>' style="pointer-events:none;"  readonly="true"  ></asp:TextBox>
                                                                    <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control uppercase"   style="display:none;"  ></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="KUANTITI DITERIMA">
                                                        <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control uppercase" Text='<%# Eval("a5") %>' onkeyup="ValidateEmail(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CATATAN">
                                                            <ItemTemplate>
                                                                <%--<asp:Label ID="Label9" runat="server" Text='<%# Eval("acp_receive_remark") %>'></asp:Label>--%>
                                                                <asp:TextBox ID="TextBox3" runat="server" Width="100%" Text='<%# Eval("a6") %>'
                                                                    CssClass="form-control uppercase"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                      <%--  <asp:TemplateField HeaderText="STATUS">
                                                            <ItemTemplate>
                                                                <%--<asp:Label ID="Label10" runat="server" Text='<%# Eval("acp_complete_ind") %>'></asp:Label>--%>
                                                            <%--    <asp:CheckBox ID="CheckBox1" runat="server"  />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>

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
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                 <asp:TextBox ID="lbl_name" runat="server" class="form-control validate[optional] uppercase" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="ver_id" Visible="false" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                                                         <asp:TextBox ID="get_id" Visible="false" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                               <asp:Button ID="Btn_Simpan" runat="server" class="btn btn-danger" Text="Simpan" UseSubmitBehavior="false" OnClick="Btn_Simpan_Click" />
                                <asp:Button ID="Button2" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" OnClick="Click_bck" />
                            </div>
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

