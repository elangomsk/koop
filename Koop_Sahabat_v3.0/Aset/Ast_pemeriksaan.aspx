<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Aset/Ast_pemeriksaan.aspx.cs" Inherits="Ast_pemeriksaan" %>
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
                        <h1>Pemeriksaan Aset</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Pengurusan Aset</a></li>
                            <li class="active">Pemeriksaan Aset</li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
      
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title">Pilihan Carian Maklumat Aset</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                         <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                               <div class="row">
                             <div class="col-md-12">
                             <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                        <ContentTemplate>
                                                            <div class="col-md-3 col-sm-3">
                                                                <asp:RadioButton ID="TJ31" runat="server" CssClass="mycheckbox" Text=" Kategori Aset"
                                                                    AutoPostBack="true" OnCheckedChanged="RB11_CheckedChanged" />
                                                                <%--  <label>Warganegara</label>--%>
                                                            </div>
                                                            <div class="col-md-3 col-sm-3">
                                                                <asp:RadioButton ID="TJ32" runat="server" CssClass="mycheckbox" Text=" Lokasi" AutoPostBack="true"
                                                                    OnCheckedChanged="RB12_CheckedChanged" />
                                                            </div>
                                                            <div class="col-md-3 col-sm-3">
                                                                <asp:RadioButton ID="TJ33" runat="server" CssClass="mycheckbox" Text=" Kakitangan"
                                                                    AutoPostBack="true" OnCheckedChanged="RB13_CheckedChanged" />
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                 
                                 </div>
                                </div>
                            <div id="rcorners1" runat="server" visible="false">
                             <div class="box-header with-border">
                            <h3 class="box-title">Carian Maklumat Aset</h3>
                        </div>
                         <div class="box-body">&nbsp;</div>
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Kategori</label>
                                    <div class="col-sm-8">
                                          <asp:DropDownList style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"
                                                                ID="dd_kat" OnSelectedIndexChanged="OnSelectedIndexChanged" AutoPostBack="true">
                                                            </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Sub Kategori</label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged1"
                                                                ID="dd_skat">
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
                                           <asp:DropDownList style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" ID="dd_jenis">
                                                            </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Aset Id</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="TextBox1" runat="server" class="form-control validate[optional]" MaxLength="14"></asp:TextBox>
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
                               <asp:Button ID="Button4" runat="server" class="btn btn-primary" Text="Carian" UseSubmitBehavior="false"
                                                                OnClick="Button4_Click" />
                            </div>
                           </div>
                               </div>
                                </div>
                           <div id="rcorners2" runat="server" visible="false">
                                  <div class="box-header with-border">
                            <h3 class="box-title">Carian Maklumat Aset</h3>
                        </div>
                         <div class="box-body">&nbsp;</div>
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Organisasi</label>
                                    <div class="col-sm-8">
                                           <asp:DropDownList style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" ID="DD_ORGNSI" AutoPostBack="true" OnSelectedIndexChanged="cal_lokasi">
                                                            </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Lokasi</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" ID="DD_lokasi">
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
                                  <asp:Button ID="Button5" runat="server" class="btn btn-primary" Text="Carian" UseSubmitBehavior="false"
                                                                OnClick="Button4_Click" />
                                                            &nbsp;
                                                            <asp:TextBox ID="TextBox4" runat="server" class="form-control" Visible="false"></asp:TextBox>
                                                            <asp:TextBox ID="TextBox5" runat="server" class="form-control" Visible="false"></asp:TextBox>
                            </div>
                           </div>
                               </div>
                               </div>
                            <div id="rcorners3" runat="server" visible="false">
                                  <div class="box-header with-border">
                            <h3 class="box-title">Carian Maklumat Aset</h3>
                        </div>
                         <div class="box-body">&nbsp;</div>
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Kakitangan</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="TextBox2" runat="server" class="form-control validate[optional] uppercase" MaxLength="10"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama Kakitangan</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="tb1" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                               <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                 <asp:Button ID="Button6" runat="server" class="btn btn-primary" Text="Carian" UseSubmitBehavior="false"
                                                                OnClick="Button4_Click" />
                            </div>
                           </div>
                               </div>
                               </div>
                            <div class="box-header with-border">
                            <h3 class="box-title">Pendaftaran Maklumat Pemeriksaan Aset</h3>
                        </div>
                         <div class="box-body">&nbsp;</div>
                            <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                  <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" OnRowDataBound="OnRowDataBound"
                                                            OnRowEditing="OnRowEditing" OnPageIndexChanging="gv_refdata_PageIndexChanging">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="BIL">
                                                                     <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                            />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ORGANASASI">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("org_name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="JABATAN">
                                                                    <ItemStyle HorizontalAlign="center"  Width="20%"></ItemStyle>
                                                                    <ItemTemplate>
                                                                       <%-- <asp:LinkButton runat="server" ID="lnkView" OnClick="lnkView_Click">--%>
                                                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("hr_jaba_desc") %>'></asp:Label>
                                                                            <asp:Label ID="Label1" runat="server" Visible="false" Text='<%# Eval("org_name") %>'></asp:Label>
                                                                            <asp:Label ID="Label4" runat="server" Visible="false" Text='<%# Eval("sas_staff_no") %>'></asp:Label>
                                                                            <asp:Label ID="Label5" runat="server" Visible="false" Text='<%# Eval("sas_asset_id") %>'></asp:Label>
                                                                        <%--</asp:LinkButton>--%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Kategori Aset">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label6" runat="server" Text='<%# Eval("ast_kategori_desc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Sub Kategori Aset">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label6_1" runat="server" Text='<%# Eval("ast_subkateast_desc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Jenis Aset">
                                                                    <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label6_2" runat="server" Text='<%# Eval("ast_jeniaset_desc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Aset Id">
                                                                    <ItemStyle HorizontalAlign="center" Width="8%"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label6_aid" runat="server" Text='<%# Eval("sas_asset_id") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Status Pemeriksaan">
                                                                    <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_sts" runat="server" Text='<%# Eval("sas_cond_sts_cd") %>' Visible="false"></asp:Label>
                                                                        <asp:DropDownList ID="ddl_sts" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                        </asp:DropDownList>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Justifikasi">
                                                                    <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_justy" runat="server" Text='<%# Eval("sas_justify_cd") %>' Visible="false"></asp:Label>
                                                                        <asp:DropDownList ID="ddl_justy" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                        </asp:DropDownList>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Harga Semasa (RM)">
                                                                    <ItemStyle HorizontalAlign="Right" Width="3%"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TXT_HS" runat="server" Text='<%# Eval("sas_curr_price_amt","{0:N}") %>' style=" text-align:right;" CssClass="form-control"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Kos Baiki (RM)">
                                                                    <ItemStyle HorizontalAlign="Right" Width="3%"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TXT_KB" runat="server" style=" text-align:right;" Text='<%# Eval("sas_repair_amt","{0:N}") %>'  CssClass="form-control"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Pemeriksaan</label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                         <asp:TextBox ID="TextBox3" runat="server"  class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox6" runat="server" class="form-control" Visible="false"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox7" runat="server" class="form-control" Visible="false"></asp:TextBox>
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
                                   <asp:Button ID="Button3" runat="server" class="btn btn-danger" Text="Simpan" UseSubmitBehavior="false"
                                                            OnClick="Button2_Click" />
                                                        <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" UseSubmitBehavior="false" OnClick="Button5_Click" />
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

