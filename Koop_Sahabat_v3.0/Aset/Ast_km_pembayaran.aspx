<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Aset/Ast_km_pembayaran.aspx.cs" Inherits="Ast_km_pembayaran" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        <script type="text/javascript">
        function addTotal_bk1() {

            var amt1 = Number($("#<%=TextBox7.ClientID %>").val());

            $(".au_amt").val(amt1.toFixed(2));

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
                        <h1>Tindakan Aduan Aset</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Pengurusan Aset</a></li>
                            <li class="active">Tindakan Aduan Aset</li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
      
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Aduan</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                         <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Aduan</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox9" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Aduan</label>
                                    <div class="col-sm-8">
                                   <asp:TextBox ID="TextBox12" runat="server" class="form-control validate[optional]"></asp:TextBox>
                                                <asp:TextBox ID="TextBox13" Visible="false" runat="server" class="form-control validate[optional]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Kakitangan</label>
                                    <div class="col-sm-8">
                                                  <asp:TextBox ID="TextBox4" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Organisasi </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox1" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jabatan </label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="TextBox8" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            
                             <div class="box-header with-border">
                            <h3 class="box-title">Kemasukan Maklumat Invois</h3>
                        </div>
                             <div class="box-body">&nbsp;</div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Invois <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox2" runat="server" class="form-control validate[optional,custom[onlyLetterNumberSp]]" MaxLength="20"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Amaun (RM) <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="TextBox7" style="text-align:Right;" runat="server" class="form-control validate[optional,custom[number]] au_amt"  onblur="addTotal_bk1(this)"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  
                                 </div>
                                </div>
                               <div class="row">
                             <div class="col-md-12">
                          <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Daftar Syarikat <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                  <asp:TextBox ID="TextBox3" runat="server" class="form-control validate[optional]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-1 box-body">
                                <div class="form-group">
                                    <div class="col-sm-8">
                                        <button id="rol_set" runat="server" type="button" class="btn btn-success" data-toggle="modal" data-target="#modal_default"><span class="fa fa-search"></span>&nbsp; Carian</button>
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
                                <asp:TextBox ID="TextBox5" runat="server" class="form-control validate[optional,custom[number]] uppercase" ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                     <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama Bank</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList  ID="DD_NAMABANK" runat="server" CssClass="form-control uppercase">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Akaun</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox10" runat="server" class="form-control validate[optional,custom[number]]"
                                                            MaxLength="20"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Alamat Pembekal</label>
                                    <div class="col-sm-8">
                                     <textarea id="TextBox11" runat="server" class="form-control validate[optional,custom[OPTIONAL]] uppercase"
                                                            ></textarea>
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
                                   <asp:Button ID="Button5" runat="server" class="btn btn-danger" Text="Simpan" UseSubmitBehavior="false" onclick="Button5_Click1" />
                                <asp:Button ID="Button2" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" OnClick="Click_bck" />
                            </div>
                           </div>
                               </div>
                            
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
          </div>
          <!-- /.modal-dialog -->
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

