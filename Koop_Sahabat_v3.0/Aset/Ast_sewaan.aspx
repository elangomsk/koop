<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Aset/Ast_sewaan.aspx.cs" Inherits="Ast_sewaan" %>
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
         
    </script>

   <script type="text/javascript">
       function addTotal_bk1() {
           var amt1 = Number($("#<%=TextBox6.ClientID %>").val());
           $(".au_amt1").val(amt1.toFixed(2));
       }
       function addTotal_bk2() {
           var amt2 = Number($("#<%=TextBox8.ClientID %>").val());
           $(".au_amt2").val(amt2.toFixed(2));
       }
       function addTotal_bk3() {
           var amt3 = Number($("#<%=TextBox9.ClientID %>").val());
           $(".au_amt3").val(amt3.toFixed(2));
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Negeri </label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="ss_dd" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                        </asp:DropDownList> 
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-8">
                                        <asp:Button ID="Carian" runat="server" class="btn btn-primary" Text="Carian" UseSubmitBehavior="false"
                                                            OnClick="Carian_Click" />
                                                        <asp:Button ID="Button3" runat="server" class="btn btn-default" Text="Set Semula"
                                                            OnClick="Reset_btn" UseSubmitBehavior="false" />
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                              <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                <asp:GridView ID="gvSelected" runat="server" class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" OnPageIndexChanging="gv_refdata_PageIndexChanging1">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="BIL" ItemStyle-Width="5%">
                                                                     <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <%# Container.DataItemIndex + 1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="pro_asset_id" HeaderText="Aset ID" ItemStyle-Width="15%" />
                                                                <asp:BoundField DataField="hr_negeri_desc" HeaderText="DAERAH" ItemStyle-Width="15%" />
                                                                <asp:BoundField DataField="pro_ownership_no" HeaderText="No Hak Milik" />
                                                                <asp:TemplateField HeaderText="No Lot" ItemStyle-Width="25%">
                                                                    <ItemTemplate>
                                                                        <div>
                                                                            <%#Eval("pro_lot_no")%></div>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("pro_ownership_no") %>' Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PILIH" ItemStyle-Width="5%">
                                                                     <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="RadioButton1" runat="server" AutoPostBack="true" onclick="RadioCheck(this);"
                                                                            OnCheckedChanged="rbName_CheckedChanged" />
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
                            <h3 class="box-title">Butiran Sewaan </h3>
                        </div>
                             <div class="box-body">&nbsp;</div>
                              <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Daftar Syarikat <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox1" runat="server" class="form-control validate[optional] uppercase"
                                                            MaxLength="15"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox13" runat="server" Visible="false"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox14" runat="server" Visible="false"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox15" runat="server" Visible="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama Syarikat </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="TextBox12" runat="server" class="form-control validate[optional] uppercase"
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Orang Hubungan</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox2" runat="server" class="form-control validate[optional] uppercase"
                                                            MaxLength="150"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Kad Pengenalan </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox3" runat="server" class="form-control validate[optional,custom[number]]"
                                                            MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Telefon</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox4" runat="server" class="form-control validate[optional,custom[phone]]"
                                                                MaxLength="11"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Deposit Sewa (RM) </label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="TextBox6" runat="server" class="form-control validate[optional,custom[number]] au_amt1" onblur="addTotal_bk1(this)"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <div class="row">
                             <div class="col-md-12">
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Sewaan Dari </label>
                                    <div class="col-sm-8">
                                       <div class="input-group">
                                    <asp:TextBox ID="TextBox5" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                placeholder="DD/MM/YYYY"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Sehingga</label>
                                    <div class="col-sm-8">
                                      <div class="input-group">
                                     <asp:TextBox ID="TextBox7" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Amaun Sewaan (RM) </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox8" runat="server" class="form-control validate[optional,custom[number]] au_amt2" onblur="addTotal_bk2(this)"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Deposit Utiliti (RM)</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox9" runat="server" class="form-control validate[optional,custom[number]] au_amt3" onblur="addTotal_bk3(this)"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <div class="row">
                             <div class="col-md-12">
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tingkat </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox11" runat="server" class="form-control validate[optional] uppercase"
                                                            MaxLength="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Lot Sewaan <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox10" runat="server" class="form-control validate[optional] uppercase"
                                                            MaxLength="10"></asp:TextBox>
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
                                      <textarea id="txt_area" runat="server" rows="2" class=" form-control validate[optional] uppercase"
                                                            maxlength="200"></textarea>
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
                                 <asp:Button ID="Btn_Simpan" runat="server" class="btn btn-danger" Text="Simpan" UseSubmitBehavior="false"
                                                            OnClick="Btn_Simpan_Click" />
                                                        <asp:Button ID="Button2" runat="server" class="btn btn-danger" Text="Kemaskini" UseSubmitBehavior="false"
                                                            OnClick="Btn_Simpan_Click" />
                                <asp:Button ID="Button5" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" OnClick="Click_bck" />
                            </div>
                           </div>
                               </div>
                            <div class="box-body">&nbsp;</div>
                             <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                               <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="1000000">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="BIL" ItemStyle-Width="3%">
                                                                    <ItemTemplate>
                                                                        <%# Container.DataItemIndex + 1 %></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="No Daftar" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" ID="lnkView" OnClick="lnkView_Click" Font-Bold Font-Underline>
                                                                            <asp:Label ID="gd_5" runat="server" Text='<%# Eval("ren_comp_reg_no") %>'></asp:Label>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ALAMAT PERMIS" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%">
                                                                    <ItemTemplate>
                                                                        <%#Eval("ren_ctc_address")%>
                                                                        <asp:Label ID="gd_1" runat="server" Text='<%# Eval("ren_state_cd") %>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="gd_2" runat="server" Text='<%# Eval("ren_ownership_no") %>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="gd_3" runat="server" Text='<%# Eval("ren_comp_reg_no") %>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="gd_4" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="ren_level" HeaderText="TINGKAT" ItemStyle-Width="10%" />
                                                                <asp:BoundField DataField="ren_rental_lot" HeaderText="LOT" ItemStyle-Width="15%" />
                                                                <asp:BoundField DataField="ren_comp_name" ItemStyle-HorizontalAlign="Left" HeaderText="PENYEWA"
                                                                    ItemStyle-Width="15%" />
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

