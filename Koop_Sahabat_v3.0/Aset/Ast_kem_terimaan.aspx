<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Aset/Ast_kem_terimaan.aspx.cs" Inherits="Ast_kem_terimaan" %>
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
        
        var label1 = GetChildControl(row, "txtCustomerId").value; // enter value
        var label2 = GetChildControl(row, "TextBox1").value; // bal qty
        var label3 = GetChildControl(row, "tt1").value;  // recieve qty
        var label5 = GetChildControl(row, "ttqty").value; // tot qty
        var label4 = (parseFloat(label2) + parseFloat(label3));

        
        var Multi_l1 = (parseFloat(label4) - parseFloat(label1));
        
            if (label1 != "") {

                if (Multi_l1 >= "0") {
                    //alert(Multi_l1);
                    GetChildControl(row, "txtbku").value = Multi_l1
                    GetChildControl(row, "TextBox4").value = Multi_l1;
                }
                else {

                    GetChildControl(row, "txtbku").value = label2
                    GetChildControl(row, "txtCustomerId").value = label3;
                    alert("Maximum Increase " + label2 + " Quantity.");
                }
            }
            else {
                GetChildControl(row, "txtbku").value = label2;
                GetChildControl(row, "txtCustomerId").value = label3;
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
                                        <asp:TextBox ID="txtnopo" runat="server" class="form-control validate[optional] uppercase" MaxLength="1000"></asp:TextBox>
                                              
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
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Terimaan</label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="txttar" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="box-header with-border">
                            <h3 class="box-title">Senarai Maklumat Terimaan Aset </h3>
                        </div>
                             <div class="box-body">&nbsp;</div>
                              <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                               <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" OnPageIndexChanging="gv_refdata_PageIndexChanging" onrowdatabound="gvEmp_RowDataBound">
                                   
 <PagerStyle CssClass="pager" />
                                                            <Columns>
                                                             <asp:TemplateField HeaderText="BIL">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                    ItemStyle-Width="3%" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                                <asp:BoundField DataField="ast_kategori_desc" ItemStyle-HorizontalAlign="Left" HeaderText="KATEGORI ASET" />
                                                                <asp:BoundField DataField="ast_subkateast_desc" ItemStyle-HorizontalAlign="Left" HeaderText="SUB KATEGORI ASET" />
                                                                <asp:BoundField DataField="ast_jeniaset_desc" ItemStyle-HorizontalAlign="Left" HeaderText="JENIS ASET" />
                                                                <asp:BoundField DataField="cas_asset_desc" ItemStyle-HorizontalAlign="Left" HeaderText="KETERANGAN ASET" />
                                                                <asp:BoundField DataField="acp_do_no" ItemStyle-HorizontalAlign="Left" HeaderText="No DO" />
                                                                <asp:BoundField DataField="acp_tot_qty" ItemStyle-HorizontalAlign="center" HeaderText="KUANTITI" />
                                                                 <asp:TemplateField HeaderText="KUANTITI DITERIMA">
                                                        <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label4_2" runat="server" Text='<%# Eval("acp_bal_qty") %>' style="display:none;"></asp:Label>
                                                                  <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control uppercase" Text='<%# Eval("acp_bal_qty") %>'  style="display:none;" ></asp:TextBox>
                                                                   <asp:TextBox ID="txtbku" runat="server" CssClass="form-control uppercase" Text='<%# Eval("acp_bal_qty") %>'   readonly="true"  ></asp:TextBox>
                                                                   <asp:TextBox ID="ttqty" runat="server" CssClass="form-control uppercase" Text='<%# Eval("acp_tot_qty") %>'    style="display:none;"  ></asp:TextBox>
                                                                    <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control uppercase"   style="display:none;"  ></asp:TextBox>
                                                                    <asp:TextBox ID="txt_simp" runat="server" CssClass="form-control uppercase"  style="display:none;" ></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="KUANTITI DITERIMA " ItemStyle-Width="30">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtCustomerId" runat="server" CssClass="form-control uppercase"
                                                                            Text='<%# Eval("acp_receive_qty") %>' onkeyup="ValidateEmail(this)"/>
                                                                             <asp:TextBox ID="tt1" runat="server" CssClass="form-control uppercase" Text='<%# Eval("acp_receive_qty") %>'  style="display:none;" ></asp:TextBox>
                                                                        <asp:Label ID="Label4" Visible="false" runat="server" Text='<%# Eval("acp_seq_no") %>'></asp:Label>
                                                                        <asp:Label ID="txtnodo" Visible="false" runat="server" Text='<%# Eval("acp_do_no") %>'></asp:Label>
                                                                        <asp:Label ID="cmp_ind" Visible="false" runat="server" Text='<%# Eval("acp_complete_ind") %>'></asp:Label>
                                                                        <asp:Label ID="acppo" Visible="false" runat="server" Text='<%# Eval("acp_po_no") %>'></asp:Label>
                                                                        <asp:Label ID="acpctdt" Visible="false" runat="server" Text='<%# Eval("acpcdt") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CATATAN" ItemStyle-Width="150">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control uppercase" Text='<%# Eval("acp_receive_remark") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="comp_desc" ItemStyle-HorizontalAlign="Left" HeaderText="STATUS" />
                                                                <%--<asp:TemplateField HeaderText="PILIH">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Eval("acp_complete_ind").ToString()=="01" ? true : false %>' />
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
                                 <asp:Button ID="Btn_Kemaskini" runat="server" class="btn btn-danger" UseSubmitBehavior="false" Text="Kemaskini" OnClick="Btn_Kemaskini_Click" />
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

