<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Aset/Ast_pak_aset.aspx.cs" Inherits="Ast_pak_aset" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
      <script>
     $(function () {
         $('.select2').select2();
     });
</script> 
       <script type="text/javascript">
        function RadioCheck(rb) {
            var gv = document.getElementById("<%=gvSelected.ClientID%>");
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

        $(function () {
            $("#<%=TextBox5.ClientID%>").timepicker({
                showInputs: false //Set showInputs to false
            });
        })
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
                        <h1>Pendaftaran Kehilangan Aset</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Pengurusan Aset</a></li>
                            <li class="active">Pendaftaran Kehilangan Aset</li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
      
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Pemegang Aset</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                         <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                               <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama Pegawai </label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="TextBox9" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                     <label for="inputEmail3" class="col-sm-3 control-label">Jawatan </label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="TextBox10" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Organisasi </label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="TextBox11" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                     <label for="inputEmail3" class="col-sm-3 control-label">Jawatan </label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="TextBox12" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                              <div class="box-header with-border" id="disp_hdr_txt" runat="server" visible="false">
                            <h3 class="box-title">Carian Maklumat Aset</h3>
                        </div>
                             <div class="box-body">&nbsp;</div>
                              <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                 <asp:GridView ID="gvSelected" runat="server" class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" OnPageIndexChanging="gv_refdata_PageIndexChanging">
                                      <PagerStyle CssClass="pager" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="BIL">
                                                         <ItemStyle HorizontalAlign="center" Width="3%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                    ItemStyle-Width="150" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="KATEGORI ASET">
                                                            <ItemStyle HorizontalAlign="Left" Width="15%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("ast_kategori_desc") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SUB KATEGORI ASET">
                                                        <ItemStyle HorizontalAlign="Left" Width="15%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("ast_subkateast_desc") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="JENIS ASET">
                                                        <ItemStyle HorizontalAlign="Left" Width="15%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("ast_jeniaset_desc") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="KETERANGAN ASET">
                                                        <ItemStyle HorizontalAlign="Left" Width="15%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("cas_asset_desc") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ASET ID">
                                                        <ItemStyle HorizontalAlign="center" Width="10%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label8" runat="server" Text='<%# Bind("sas_asset_id") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PILIH">
                                                        <ItemStyle HorizontalAlign="center" Width="5%" />
                                                            <ItemTemplate>
                                                                <asp:RadioButton ID="RadioButton1" runat="server" AutoPostBack="true" OnCheckedChanged="Group1_CheckedChanged" onclick="RadioCheck(this);"/>
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
                            <h3 class="box-title">Maklumat Kehilangan Aset Kakitangan </h3>
                        </div>
                             <div class="box-body">&nbsp;</div>
                              <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Aduan</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox1" runat="server" class="form-control validate[optional]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Aduan </label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                         <asp:TextBox ID="TextBox2" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Kejadian</label>
                                    <div class="col-sm-8">
                                      <div class="input-group">
                                    <asp:TextBox ID="TextBox3" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                    placeholder="DD/MM/YYYY"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Masa Kejadian </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox5" data-provide="timepicker" class="form-control timepicker"
                                                    runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tempat Kejadian</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox4" runat="server" class="form-control validate[optional] uppercase"
                                                    MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Laporan polis </label>
                                    <div class="col-sm-8">
                                    <asp:TextBox ID="TextBox6" runat="server" class="form-control validate[optional] uppercase"
                                                    MaxLength="30"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <div class="row">
                             <div class="col-md-12">
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Keterangan Aduan </label>
                                    <div class="col-sm-8">
                                       <textarea id="txt_ar1" runat="server" class="form-control uppercase" rows="3" maxlength="2000"></textarea>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Laporan polis</label>
                                    <div class="col-sm-8">
                                      <div class="input-group">
                                     <asp:TextBox ID="TextBox8" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Orang Disyaki (jika ada) </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox7" runat="server" class="form-control validate[optional] uppercase"
                                                    MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Muatnaik Laporan Polis</label>
                                    <div class="col-sm-5">
                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                                <asp:TextBox ID="TextBox13" runat="server" class="form-control" Visible="false"></asp:TextBox>
                                    </div>
                                     <div class="col-sm-3">
                                         <asp:LinkButton ID="LinkButton1" runat="server" Text="Download" class="glyphicon glyphicon-download"
                                                    OnClick="DownloadFile"></asp:LinkButton>
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
                                <asp:TextBox ID="get_id" Visible="false" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                                <asp:Button ID="Button4" runat="server" class="btn btn-danger" Text="Simpan" OnClick="insert_values"
                                                    UseSubmitBehavior="false" />
                                <asp:Button ID="Button5" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" OnClick="Click_bck" />
                            </div>
                           </div>
                               </div>
                            <div class="box-body">&nbsp;</div>
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
         <Triggers>
<asp:PostBackTrigger ControlID="Button4" />
             <asp:PostBackTrigger ControlID="LinkButton1" />
</Triggers>
    </asp:UpdatePanel>
</asp:Content>

