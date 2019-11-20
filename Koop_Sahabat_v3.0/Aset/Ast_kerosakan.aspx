<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Aset/Ast_kerosakan.aspx.cs" Inherits="Ast_kerosakan" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        <script type="text/javascript">
    function addTotal_bk1() {

        var amt1 = Number($("#<%=txt_bx1.ClientID %>").val());

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
                              <%-- <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Dari <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                       <div class="input-group">
                                         <asp:TextBox ID="txt_dar" runat="server"  class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                       <label for="inputEmail3" class="col-sm-3 control-label">Sehingga <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                     <div class="input-group">
                                         <asp:TextBox ID="txt_seh" runat="server"  class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>--%>
                           <%-- <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                 <asp:Button ID="Btn_Carian" runat="server" class="btn btn-primary" Text="Carian" OnClick="clk_srch" UseSubmitBehavior="false" />
                                                <asp:Button ID="Button3" runat="server" class="btn btn-default" Text="Set Semula" UseSubmitBehavior="false" OnClick="btnreset_Click"/>
                            </div>
                           </div>
                               </div>--%>
                            <%-- <div class="box-header with-border">
                            <h3 class="box-title">Senarai Maklumat Aduan</h3>
                        </div>
                             <div class="box-body">&nbsp;</div>
                            <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                    
           <div class="col-md-12 box-body">
                                <asp:GridView ID="gvSelected" runat="server" class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" OnPageIndexChanging="gv_refdata_PageIndexChanging">
                                    <PagerStyle CssClass="pager" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="BIL" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                                    ItemStyle-Width="150" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="NO ADUAN">
                                                                        <ItemStyle HorizontalAlign="center" Width="10%"/> 
                                                                            <ItemTemplate>
                                                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("cmp_id") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                         <asp:TemplateField HeaderText="Aset Id" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%"/> 
                                                                            <ItemTemplate>
                                                                                    <asp:Label ID="Label3_aid" runat="server" Text='<%# Eval("cmp_asset_id") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="TARIKH ADUAN" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("cmp_complaint_dt") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                         <asp:TemplateField HeaderText="PERIHAL KEROSAKAN">
                                                                          <ItemStyle HorizontalAlign="Left" Font-Bold Font-Underline/>
                                                                            <ItemTemplate>
                                                                             <asp:LinkButton runat="server" ID="lnkView" OnClick="lnkView_Click">
                                                                             <asp:Label ID="cid" Visible="false" runat="server" Text='<%# Eval("cmp_id") %>'></asp:Label>
                                                                                <asp:Label ID="sno" Visible="false" runat="server" Text='<%# Eval("cmp_staff_no") %>'></asp:Label>
                                                                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("cmp_remark") %>'></asp:Label>
                                                                                </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="TARIKH TINDAKAN" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("cmp_action_dt") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="TINDAKAN">
                                                                         <ItemStyle HorizontalAlign="Left"/>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label9" runat="server" Text='<%# Eval("cmp_action_remark") %>'></asp:Label>
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
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                                </asp:GridView>
               </div>
          </div>             --%>
                           <%--  <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Pengadu</h3>
                        </div>
                             <div class="box-body">&nbsp;</div>--%>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jabatan </label>
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
                                                <asp:TextBox ID="TextBox1" Visible="false" runat="server" class="form-control validate[optional]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Aset Untuk Diselenggara</h3>
                        </div>
                             <div class="box-body">&nbsp;</div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Kategori</label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="DropDownList1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" AutoPostBack="true" OnSelectedIndexChanged="sel_kat">
                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Sub Kategori </label>
                                    <div class="col-sm-8">
                                   <asp:DropDownList ID="DD_Sub" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged1">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jenis Aset</label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="DropDownList2" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged2">
                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Keterangan Aset </label>
                                    <div class="col-sm-8">
                                <asp:DropDownList ID="DropDownList3" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body" style="pointer-events:none;">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Aset Id</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox10" runat="server" class="form-control validate[optional]" MaxLength="5"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"></label>
                                    <div class="col-sm-8">
                                           <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                        <ContentTemplate>
                                    <div class="col-md-4 col-sm-4">
                                   <%-- <asp:RadioButton ID="RB31" runat="server"  Text=" Naiktaraf"  AutoPostBack="true" oncheckedchanged="RB1_CheckedChanged"/>   --%>
                                    <asp:RadioButton ID="RB31" runat="server"  Text=" Naiktaraf" AutoPostBack="true" oncheckedchanged="RB1_CheckedChanged"/>   
                                    </div>
                                    <div class="col-md-4 col-sm-4">                                                         
                                    <%--<asp:RadioButton ID="RB32" runat="server" Text=" Pindahan"  AutoPostBack="true" oncheckedchanged="RB2_CheckedChanged" />--%>
                                    <asp:RadioButton ID="RB32" runat="server" Text=" Pindahan" AutoPostBack="true" oncheckedchanged="RB2_CheckedChanged"/>
                                    </div>
                                    </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                                 
                                 </div>
                                </div>
                             <div class="box-header with-border">
                            <h3 class="box-title">Butiran Aduan</h3>
                        </div>
                             <div class="box-body">&nbsp;</div>
                             <div class="row" style="pointer-events:none;">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Aduan</label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="TextBox2" runat="server" class="form-control validate[optional]" MaxLength="5"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Aduan</label>
                                    <div class="col-sm-8">
                                         <textarea id="txt_area" runat="server" class=" form-control uppercase" rows="3" maxlength="1000"></textarea>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                               <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Pengesahan Penyelia</h3>
                        </div>
                             <div class="box-body">&nbsp;</div>
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tindakan</label>
                                    <div class="col-sm-8">
                                        <textarea id="Textarea1" runat="server" class="form-control uppercase" rows="3" maxlength="1000"></textarea>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Kos (RM) </label>
                                    <div class="col-sm-8">
                                <asp:TextBox ID="txt_bx1" style="text-align:right;" runat="server" class="form-control validate[optional,custom[number]] au_amt"  onblur="addTotal_bk1(this)"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Status</label>
                                    <div class="col-sm-8">
                                                           <asp:DropDownList ID="DropDownList5" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
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
                                 <asp:TextBox ID="lbl_name" runat="server" class="form-control validate[optional] uppercase" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="ver_id" Visible="false" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                                                         <asp:TextBox ID="get_id" Visible="false" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                                  <asp:Button ID="Button1" runat="server" class="btn btn-danger" OnClick="clk_smpn" Text="Simpan" UseSubmitBehavior="false" />
                                                    <asp:Button ID="Button4" runat="server" class="btn btn-warning" OnClick="clk_cetak" Text="Cetak" UseSubmitBehavior="false" />
                                <asp:Button ID="Button2" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" OnClick="Click_bck" />
                            </div>
                           </div>
                               </div>
                           <div class="row">
                             <div class="col-md-12">
                                   <rsweb:ReportViewer ID="Rptviwer_lt" runat="server">
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

