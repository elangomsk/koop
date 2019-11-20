<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Aset/Ast_pengasahan_po.aspx.cs" Inherits="Ast_pengasahan_po" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

     <script type="text/javascript">
        function ValidateEmail(button) {
            var row = button.parentNode.parentNode;
            var label1 = GetChildControl(row, "txtCustomerId").value;
            var label2 = GetChildControl(row, "Label22").value;
            var label3 = GetChildControl(row, "txt_simp").value;
            var label4 = GetChildControl(row, "txt_amt").value;

            if (label1 != "0") {
                GetChildControl(row, "txt_simp").value = "1";
            }
            var Multi_l1 = (parseFloat(label1) * parseFloat(label2));
            if (label1 != "") {
                GetChildControl(row, "Label23").value = Multi_l1.toFixed(2);
                GetChildControl(row, "Label21_tamt").value = Multi_l1.toFixed(2);
            }
            else {
                //alert(label4);
                GetChildControl(row, "Label23").value = "";
                GetChildControl(row, "Label21_tamt").value = "";
            }
            //alert(Multi_l1);
            //$("#<%=gvSelected.ClientID %> [class*=TotalValue_bk]").val(Multi_l1.toFixed(2));
            //$("#<%=TextBox2.ClientID %>").val(Multi_l1.toFixed(2))
            // $("#<%=gvSelected.ClientID %> [class*=TotalValue_bk1]").val(Multi_l1.toFixed(2));

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
      <script type="text/javascript">
          function addTotal_bk1() {
            
                  var amt1 = Number($("#<%=TextBox13.ClientID %>").val());

                  $(".au_amt").val(amt1.toFixed(2));
              
          }
</script>
        <script>
     $(function () {

       <%--  $('#<%=GridView2.ClientID %>').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
             "responsive": true,
             "sPaginationType": "full_numbers",
             "iDisplayLength": 15,
             "aLengthMenu": [[15, 30, 50, 100], [15, 30, 50, 100]]
         });--%>

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
                        <h1>Pengesahan Belian Aset</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Pengurusan Aset</a></li>
                            <li class="active">Pengesahan Belian Aset</li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
      
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Belian Aset</h3>
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
                                          <asp:TextBox ID="txtnopo" runat="server" class="form-control validate[optional] uppercase"
                                                            MaxLength="1000"></asp:TextBox>
                                                             <asp:Panel ID="autocompleteDropDownPanel" runat="server" 
                                                                                    ScrollBars="Auto" Height="150px" Font-Size="Medium" 
                                                                                    HorizontalAlign="Left" Wrap="False" />
                                                                                 <cc1:AutoCompleteExtender ServiceMethod="GetCompletionList" MinimumPrefixLength="1"
                                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtnopo"
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
                            <h3 class="box-title">Maklumat Pemohon</h3>
                        </div>
                            <div class="box-body">&nbsp;</div>
                            <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Kakitangan</label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="txtnokak" runat="server" class="form-control validate[optional] uppercase"
                                                    MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama Kakitangan</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtnama" runat="server" class="form-control validate[optional] uppercase" ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                         
                              <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Organisasi</label>
                                    <div class="col-sm-8">
                                    <asp:TextBox ID="txtorg" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jabatan</label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="txtjab" runat="server" class="form-control validate[optional] uppercase" ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Unit</label>
                                    <div class="col-sm-8">
                                   <asp:TextBox ID="txtunit" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                           <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Permohonan</label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                        <asp:TextBox ID="txttper" runat="server" class="form-control"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Belian Aset</h3>
                        </div>
                            <div class="box-body">&nbsp;</div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Kategori </label>
                                    <div class="col-sm-8">
                                      <asp:DropDownList ID="DD_down" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional] uppercase" AutoPostBack="true" OnSelectedIndexChanged="DD_down_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Sub Kategori</label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="DD_down1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional] uppercase" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged1"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                               <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jenis Aset </label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="DropDownList5" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional] uppercase"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Keterangan</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox4" runat="server" class="form-control validate[optional] uppercase"
                                                            MaxLength="150"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Kuantiti </label>
                                    <div class="col-sm-8">
                                    <asp:TextBox ID="txtluan" runat="server" class="form-control validate[number]" MaxLength="4"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Harga / Unit (RM)</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox13" runat="server"  class="form-control validate[optional,custom[number] au_amt" onblur="addTotal_bk1(this)"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox3" Style="display: none;" runat="server" class="form-control "></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama Pembekal </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox15" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                            </div>
                                </div>
                                   <div class="col-md-1 box-body">
                                <div class="form-group">
                                    <div class="col-sm-8">
                                        <button id="btn_Show" runat="server" type="button" class="btn btn-success" data-toggle="modal" data-target="#modal_default"><span class="fa fa-search"></span>&nbsp; Carian</button>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                 
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Alamat </label>
                                    <div class="col-sm-8">
                                      <textarea id="txtalamat1" runat="server" class="form-control uppercase"></textarea>
                                                <asp:TextBox ID="TextBox1" style=" display:none; " runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Telefon Pembekal</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txtnotel" runat="server" class="form-control uppercase"></asp:TextBox> 
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
                                 <asp:Button ID="Simpan" runat="server" class="btn btn-danger" Text="Simpan" UseSubmitBehavior="false" OnClick="Simpan_Click" />
                                                        <asp:Button ID="Kemaskini" runat="server" class="btn btn-danger" Text="Kemaskini" UseSubmitBehavior="false" OnClick="Kemaskini_Click" />
                                                        <asp:Button ID="Button4" runat="server" class="btn btn-danger" Text="Hapus" UseSubmitBehavior="false" OnClick="Button4_Click" />
                            </div>
                           </div>
                               </div>
                           
                            <div class="box-body">&nbsp;</div>
                             <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                   <asp:GridView ID="gvSelected" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="true" GridLines="None" OnPageIndexChanging="gv_refdata_PageIndexChanging">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="BIL">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                            ItemStyle-Width="3%" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="ast_kategori_desc" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left"
                                                                    HeaderText="KATEGORI ASET" />
                                                                <asp:BoundField DataField="ast_subkateast_desc" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left"
                                                                    HeaderText="SUB KATEGORI ASET" />
                                                                <asp:BoundField DataField="ast_jeniaset_desc" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left"
                                                                    HeaderText="JENIS ASET" />
                                                                <%--<asp:BoundField DataField="ast_kodast_desc" HeaderText="KETERANGAN ASET" />--%>
                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" HeaderText="KETERANGAN ASET">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" ID="lnkView" OnClick="lnkView_Click">
                                                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("cas_asset_desc") %>'></asp:Label>
                                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("pur_asset_cat_cd") %>' Visible="false"></asp:Label>
                                                                        </asp:LinkButton>
                                                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("pur_asset_id") %>' Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="pur_asset_qty" ItemStyle-Width="10%" HeaderText="KUANTITI DIPOHON" />
                                                                <asp:TemplateField ItemStyle-Width="10%" HeaderText="KUANTITI DILULUSKAN ">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtCustomerId" runat="server" CssClass="form-control validate[optional,custom[number]]"
                                                                            onkeyup="ValidateEmail(this)" Text='<%# Eval("pur_verify_qty") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="10%" HeaderText="HARGA / UNIT (RM)" ItemStyle-HorizontalAlign="Right">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label3" runat="server"  Text='<%# Eval("pur_asset_amt","{0:n}") %>'></asp:Label>
                                                                        <asp:TextBox ID="Label22" Style="display: none;" runat="server" Text='<%# Eval("pur_asset_amt") %>'></asp:TextBox>
                                                                        <asp:TextBox ID="txt_amt" Style="display: none;" runat="server" Text='<%# Eval("pur_asset_tot_amt") %>'></asp:TextBox>
                                                                        <asp:TextBox ID="txt_simp" Style="display: none;" runat="server" Text='<%# Eval("a11") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="10%" HeaderText="JUMLAH (RM)">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox Style="display: none;" ID="Label21_tamt" Text='<%# Eval("pur_asset_tot_amt") %>'
                                                                            runat="server"></asp:TextBox>
                                                                        <asp:TextBox ID="Label23" CssClass="form-control TotalValue_bk" style=" text-align:right;" ReadOnly="true" runat="server"
                                                                            Text='<%# Eval("pur_asset_tot_amt","{0:n}") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <%--<asp:BoundField DataField="pur_asset_tot_amt" ItemStyle-HorizontalAlign="Right" CssClass="TotalValue_bk"  HeaderText="JUMLAH (RM)" DataFormatString="{0:n}"/>--%>
                                                                <asp:TemplateField HeaderText="HAPUS" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="CheckBox1" runat="server" />
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
                            <h3 class="box-title">Maklumat Pengesahan Belian Aset</h3>
                        </div>
                            <div class="box-body">&nbsp;</div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Status Pengesahan <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="dd_pengsahan" runat="server" class="form-control validate[optional] uppercase"></asp:DropDownList>
                                                        <asp:TextBox ID="TextBox2" Style="display: none;" CssClass="TotalValue_bk1" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Catatan</label>
                                    <div class="col-sm-8">
                                      <textarea id="txtalamat" runat="server" class="form-control uppercase"></textarea>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                 <asp:Button ID="Btn_Kemaskini" runat="server" class="btn btn-danger" Text="Simpan"
                                                            UseSubmitBehavior="false" OnClick="Btn_Kemaskini_Click" />
                                                        <asp:Button ID="Cetak" runat="server" class="btn btn-danger" Text="Cetak" UseSubmitBehavior="false"
                                                            OnClick="Btn_Cetak_Click" />
                                  <asp:Button ID="Button5" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" OnClick="Click_bck" />
                            </div>
                           </div>
                               </div>
                             <div class="box-body">&nbsp;</div>
                            <div class="modal fade" id="modal_default">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Senarai Pembekal</h4>
              </div>
              <div class="modal-body" style="overflow-y: auto; height:350px;">
                                                         
                                                                        <div class="dataTables_wrapper form-inline dt-bootstrap">
                                     <%-- <div class="row" >--%>
                                                            <div class="col-md-12 box-body">
                                                                                    <asp:GridView ID="GridView2" runat="server" Font-Size="10px" class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Width="100%" AllowPaging="true" PageSize="50" OnPageIndexChanging="gv_refdata_PageIndexChanging1">
                                                                                        <Columns>
                                                                                        <asp:TemplateField HeaderText="BIL" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                                                    ItemStyle-Width="3%" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                            <asp:BoundField DataField="ast_kategori_desc" ItemStyle-HorizontalAlign="Left" HeaderText="KATEGORI ASET" />
                                                                                            <asp:BoundField DataField="ast_subkateast_desc" ItemStyle-HorizontalAlign="Left" HeaderText="SUB KATEGORI ASET" />
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="NO DAFTAR SYARIKAT" >
                                                                                                <ItemTemplate>
                                                                                                  <asp:Label ID="ast_lbl_1" runat="server" Text='<%# Bind("sup_id") %>'></asp:Label>
                                                                                                    <asp:Label ID="Label2_id" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:BoundField DataField="sup_name" ItemStyle-HorizontalAlign="Left" HeaderText="NAMA SYARIKAT" />
                                                                                              <asp:TemplateField HeaderText="HAPUS" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkRow" runat="server" />
                                                                        
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                                            <%--<asp:BoundField DataField="sup_bumi_ind" HeaderText="STATUS BUMIPUTERA" />
                                                                                            <asp:BoundField DataField="sup_gst_ind" HeaderText="STATUS GST" />--%>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </div>
                                                                            </div>
                                                                        </div>
              <div class="modal-footer">
                  <asp:Button ID="Button6" runat="server" class="btn btn-danger sub_btn" Text="Save Changes" data-dismiss="modal" OnClick="btnGetNode_Click"  UseSubmitBehavior="false" />
                <button type="button" class="btn btn-default" data-dismiss="modal">Batal</button>
              </div>
            </div>

            <!-- /.modal-content -->
              <div class="row">
                                                <div class="col-md-12 col-sm-2" style="text-align: center">
                                                    <div class="body collapse in">
                                                        <rsweb:ReportViewer ID="ReportViewer1" runat="server">
                                                        </rsweb:ReportViewer>
                                                    </div>
                                                </div>
                                            </div>
          </div>
          <!-- /.modal-dialog -->
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

