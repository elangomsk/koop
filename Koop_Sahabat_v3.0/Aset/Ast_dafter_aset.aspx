<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Aset/Ast_dafter_aset.aspx.cs" Inherits="Ast_dafter_aset" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
      <script>
     $(function () {
         $('.select2').select2();
     });
</script> 
       <style type="text/css">
        #rcorners1, #rcorners2, #rcorners3, #rcorners4
        {
            border-radius: 25px;
            border: 2px solid #FFCC66;
            padding: 10px;
        }
        #rcorners5
        {
            padding: 10px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $(".datepicker").datepicker({ dateFormat: 'dd-mm-yy' });
        });
    </script>
    <script type="text/javascript">
        function addTotal_bk1() {
            var amt1 = Number($("#<%=txt_hatga1.ClientID %>").val());
            $(".au_amt1").val(amt1.toFixed(2));
        }

        function addTotal_bk2() {
            var amt2 = Number($("#<%=txt_kosperb.ClientID %>").val());
            $(".au_amt2").val(amt2.toFixed(2));
        }

        function addTotal_bk3() {
            var amt3 = Number($("#<%=txt_haraga2.ClientID %>").val());
            $(".au_amt3").val(amt3.toFixed(2));
        }

        function addTotal_bk4() {
            var amt4 = Number($("#<%=txt_hargaper3.ClientID %>").val());
            $(".au_amt4").val(amt4.toFixed(2));

        }

        function addTotal_bk5() {
            var amt5 = Number($("#<%=txt_bel4.ClientID %>").val());
            $(".au_amt5").val(amt5.toFixed(2));

        }

        function addTotal_bk6() {
            var amt6 = Number($("#<%=txt_pembiayaan4.ClientID %>").val());
            $(".au_amt6").val(amt6.toFixed(2));

        }

        function addTotal_bk7() {
            var amt7 = Number($("#<%=txt_bayaran4.ClientID %>").val());
            $(".au_amt7").val(amt7.toFixed(2));

        }

        function addTotal_bk8() {
            var amt8 = Number($("#<%=txt_cukaipintu4.ClientID %>").val());
            $(".au_amt8").val(amt8.toFixed(2));

        }

        function addTotal_bk9() {
            var amt9 = Number($("#<%=txt_amauntanah4.ClientID %>").val());
            $(".au_amt9").val(amt9.toFixed(2));

        }

        function addTotal_bk10() {
            var amt10 = Number($("#<%=txt_amaunbardar4.ClientID %>").val());
            $(".au_amt10").val(amt10.toFixed(2));

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server" ScriptMode="Release">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1><asp:Label ID="ps_lbl1" runat="server"></asp:Label></h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i><asp:Label ID="ps_lbl2" runat="server"></asp:Label></a></li>
                            <li class="active"><asp:Label ID="ps_lbl3" runat="server"></asp:Label></li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
       <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title"><asp:Label ID="ps_lbl4" runat="server"></asp:Label></h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                         <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                               <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl5" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txt_nopo" runat="server" class="form-control validate[optional] uppercase" MaxLength="1000"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-8">
                                         <asp:Button ID="Button2" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" OnClick="Click_bck" />
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="box-header with-border">
                            <h3 class="box-title"><asp:Label ID="ps_lbl7" runat="server"></asp:Label></h3>
                        </div>
                             <div class="box-body">&nbsp;</div>
                              <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                <asp:GridView ID="GridView1"  runat="server" class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" OnPageIndexChanging="gv_refdata_PageIndexChanging1">
                                     <PagerStyle CssClass="pager" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="BIL" ItemStyle-HorizontalAlign="Center">
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
                                                                <asp:TemplateField HeaderText="SUB KATEGORI ASET">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("ast_subkateast_desc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="JENIS ASET">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("ast_jeniaset_desc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="KETERANGAN ASET">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" ID="lnkView" OnClick="lnkView_Click" Font-Bold Font-Underline>
                                                                            <asp:Label ID="Label6" runat="server" Text='<%# Eval("cas_asset_desc") %>'></asp:Label>
                                                                            <asp:Label ID="Label9" Visible="false" runat="server" Text='<%# Eval("acp_seq_no") %>'></asp:Label>
                                                                            <%--<asp:Label ID="Label2" Visible="false" runat="server" Text='<%# Eval("acp_po_no") %>'></asp:Label>
                                                                --%>
                                                                <asp:Label ID="Label4_do" Visible="false" runat="server" Text='<%# Eval("acp_do_no") %>'></asp:Label>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="KUANTITI">
                                                                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="LABKU" runat="server" Text='<%# Bind("acp_receive_qty") %>'></asp:Label>
                                                                        <asp:Label ID="all_qty" Style="display: none;" runat="server" Text='<%# Bind("acp_all_qty") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="BAKI KUANTITI">
                                                                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="BAK_qty" runat="server" Text='<%# Bind("q1") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="TARIKH TERIMAAN">
                                                                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label8" runat="server" Text='<%# Eval("acp_receive_upd_dt","{0:dd/MM/yyyy}") %>'></asp:Label>
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
                            <h3 class="box-title"><asp:Label ID="ps_lbl8" runat="server"></asp:Label> </h3>
                        </div>
                             <div class="box-body">&nbsp;</div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl9" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="DD_Perolehan" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                        </asp:DropDownList>  
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl10" runat="server"></asp:Label> <span class="style1">*</span> </label> 
                                    <div class="col-sm-8">
                                          <asp:DropDownList ID="DD_ketogri_Selection1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"
                                                            AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl11" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                      <asp:DropDownList ID="DropDownList1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                        </asp:DropDownList>    
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <div id="rcorners1" runat="server" visible="false">
                                                <div id="c001" runat="server">
                               <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl12" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="DD_SUBKATEG1" rstyle="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"
                                                                    AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged1">
                                                                </asp:DropDownList>  
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl13" runat="server"></asp:Label><span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                      <asp:DropDownList ID="DD_JENISASET1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"
                                                                    AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged2">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl14" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="DropDownList2" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl15" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txt_hatga1" runat="server" class="form-control validate[optional,custom[number]] au_amt1"
                                                                    onblur="addTotal_bk1(this)"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                                    <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl16" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                                     <asp:TextBox ID="txt_tdibeli" runat="server" class="form-control validate[optional,custom[dtfmt]]  datepicker mydatepickerclass"
                                                                        placeholder="DD/MM/YYYY"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div> 
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl17" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                     <div class="input-group">
                                           <asp:TextBox ID="txt_tditer" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                        placeholder="DD/MM/YYYY"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div> 
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl18" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="txt_nosiri" runat="server" class="form-control validate[optional] uppercase"
                                                                MaxLength="25"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl19" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txt_kodprod" runat="server" class="form-control validate[optional] uppercase"
                                                                MaxLength="25"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                                      <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl20" runat="server"></asp:Label> <span class="style1">*</span></label>                     
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="txt_jenamodel" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl21" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txt_jaminan" runat="server" class="form-control validate[optional] uppercase"
                                                                MaxLength="20"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                                      <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"></label>
                                    <div class="col-sm-8">
                                          <label><asp:CheckBox ID="ckbox_perlu" runat="server" />&nbsp; Perlu Diperbaharui</label>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl23" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="txt_kosperb" runat="server" class="form-control validate[optional,custom[number]] au_amt2"
                                                                onblur="addTotal_bk2(this)"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                                    <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl24" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="dd_pemp1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                            </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl25" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox6" runat="server" class="form-control validate[optional]"
                                                                MaxLength="8"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                                   
                                </div>
                               <div id="rcorners2" runat="server" visible="false">
                                                <div id="hm001" runat="server">
                              <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl26" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                      <asp:DropDownList ID="DD_SUBKATEG2" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"
                                                                    AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged31">
                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl27" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="DD_JENISASET2" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"
                                                                    AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged3">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl28" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                    <asp:DropDownList ID="DropDownList21" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl29" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txt_haraga2" runat="server" class="form-control validate[optional,custom[number]] au_amt3"
                                                                    onblur="addTotal_bk3(this)"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                                    <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl30" runat="server"></asp:Label> <span class="style1">*</span></label>                          
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                                   <asp:TextBox ID="txt_debil2" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker  mydatepickerclass"
                                                                        placeholder="DD/MM/YYY"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div> 
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl31" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                     <div class="input-group">
                                           <asp:TextBox ID="txt_diterima2" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                        placeholder="DD/MM/YYY"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div> 
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl32" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                   <asp:DropDownList ID="DD_JENAMA2" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                            </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl33" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="DD_Model2" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl34" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                     <asp:DropDownList ID="DD_Bahan2" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                            </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl35" runat="server"></asp:Label><span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txt_keupayaan2" runat="server" class="form-control validate[optional] uppercase"
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl36" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                   <asp:TextBox ID="txt_kelaskeg2" runat="server" class="form-control validate[optional] uppercase"
                                                                MaxLength="40"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl37" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txt_warna2" runat="server" class="form-control validate[optional] uppercase"
                                                                MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                                     <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl38" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                   <asp:TextBox ID="txt_tahun2" runat="server" class="form-control validate[optional] uppercase"
                                                                MaxLength="20"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl39" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="DD_jeniskend2" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl40" runat="server"></asp:Label> <%--<span class="style1">*</span>--%></label>                                                
                                    <div class="col-sm-8">
                                   <asp:TextBox ID="txt_nocasis2" runat="server" class="form-control validate[optional] uppercase"
                                                                MaxLength="20"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl41" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txt_jenisdan2" runat="server" class="form-control validate[optional] uppercase"
                                                                MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                                     <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl42" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                 <asp:TextBox ID="txt_jaminan2" runat="server" class="form-control validate[optional] uppercase"
                                                                MaxLength="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl43" runat="server"></asp:Label> <%--<span class="style1">*</span>--%></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txt_platno2" runat="server" class="form-control validate[optional] uppercase"
                                                                MaxLength="20"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                                     <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl44" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                   <asp:DropDownList ID="dd_pemp2" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                            </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"></label>
                                    <div class="col-sm-8">
                                        <label><asp:CheckBox ID="CheckBox1" runat="server" />&nbsp; Perlu Selenggara</label>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                                     <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl46" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                   <asp:TextBox ID="TextBox7" runat="server" class="form-control validate[optional]"
                                                                MaxLength="8"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                                   
                                   </div>
                            <div id="rcorners3" runat="server" visible="false">
                                                <div id="iv001" runat="server">
                               <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl47" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                      <asp:DropDownList ID="DD_SUBKATEG3" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"
                                                                    AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged41">
                                                                </asp:DropDownList>  
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl48" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                      <asp:DropDownList ID="DD_JENISASET3" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"
                                                                    AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged4">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl49" runat="server"></asp:Label><span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="DropDownList22" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl50" runat="server"></asp:Label> <span class="style1">*</span></label>                                                
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txt_hargaper3" runat="server" class="form-control validate[optional,custom[number]] au_amt4"
                                                                    onblur="addTotal_bk4(this)"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                                    <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl51" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                                      <asp:TextBox ID="txt_debeli3" runat="server" class="form-control validate[optional,custom[dtfmt]]  datepicker mydatepickerclass"
                                                                        placeholder="DD/MM/YYY"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div> 
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl52" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                     <div class="input-group">
                                          <asp:TextBox ID="txt_diterima3" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                        placeholder="DD/MM/YYY"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div> 
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl53" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                    <asp:TextBox ID="txt_kuantiti3" runat="server" class="form-control validate[optional]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl54" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="txt_jaminan3" runat="server" class="form-control validate[optional] uppercase"
                                                                MaxLength="20"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                                    
                                                    <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl55" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="dd_pemp3" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                            </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl56" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="txt_catan3" runat="server" class="form-control validate[optional] uppercase"
                                                                MaxLength="200"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                                      <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl57" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox8" runat="server" class="form-control validate[optional]"
                                                                MaxLength="8"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 
                                 </div>
                                </div>
                                                  
                                </div>
                             <div id="rcorners4" runat="server" visible="false">
                                   <div class="box-header with-border">
                            <h3 class="box-title"><asp:Label ID="ps_lbl58" runat="server"></asp:Label></h3>
                        </div>
                             <div class="box-body">&nbsp;</div>
                               <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl59" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                     <asp:DropDownList ID="DD_SUBKATEG4" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"
                                                                AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged51">
                                                            </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl60" runat="server"></asp:Label><span class="style1">*</span></label>                                                       
                                    <div class="col-sm-8">
                                      <asp:DropDownList ID="DD_JENISASET4" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"
                                                                AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged5">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl61" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                     <asp:DropDownList ID="DropDownList4" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                            </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl62" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                           <asp:TextBox ID="TextBox2" runat="server" class="form-control validate[optional] uppercase"
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl63" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="DropDownList3" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                            </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl64" runat="server"></asp:Label><span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="TextBox1" runat="server" class="form-control validate[optional] uppercase"
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl65" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox3" runat="server" class="form-control validate[optional] uppercase"
                                                                MaxLength="10"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl66" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txt_luas4" runat="server" class="form-control validate[optional,custom[number]] uppercase"
                                                                MaxLength="4"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                  <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl67" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="DD_Negeri4" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                            </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl68" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="txt_dearah4" runat="server" class="form-control validate[optional] uppercase"
                                                                MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                  <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl69" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="txt_bandar4" runat="server" class="form-control validate[optional] uppercase"
                                                                MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl70" runat="server"></asp:Label></label>                                             
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="TextBox4" runat="server" class="form-control validate[optional] uppercase"
                                                                MaxLength="40"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                  <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl71" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                       <textarea id="txt_ap" runat="server" rows="3" class="form-control validate[optional] uppercase"
                                                                maxlength="350"></textarea>
                                                            <asp:TextBox ID="txt_nama_aset" runat="server" Visible="false"></asp:TextBox>
                                                            <asp:TextBox ID="txt_pembekal4" runat="server" Visible="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl72" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                      <asp:DropDownList ID="DD_penggunaan4" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl73" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="DD_pegangan4" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl74" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="txt_fail4" runat="server" class="form-control validate[optional] "
                                                                MaxLength="20"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                  <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl75" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                      <asp:DropDownList ID="DD_MILIKAN4" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                            </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl76" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                    <div class="input-group">
                                                         <asp:TextBox ID="txt_tarikh4" runat="server" class="form-control validate[optional,custom[dtfmt]]  datepicker mydatepickerclass"
                                                                    placeholder="DD/MM/YYY"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl77" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                        <div class="col-sm-4">
                                      <label>
                                                                <asp:RadioButton ID="RadioButton1" runat="server" GroupName="radio1" />&nbsp; Ya
                                                            </label>
                                            </div>
                                        <div class="col-sm-4">
                                        <label>
                                                                <asp:RadioButton ID="RadioButton2" runat="server" GroupName="radio1" />&nbsp; Tidak
                                                            </label>
                                            </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl80" runat="server"></asp:Label> </label>                                                   
                                    <div class="col-sm-8">
                                    <asp:TextBox ID="txt_tempoh4" runat="server" class="form-control validate[optional] uppercase"
                                                                MaxLength="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>    
                                  <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl81" runat="server"></asp:Label>(%)</label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="txt_milikan4" runat="server" class="form-control validate[optional,custom[number]] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl82" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                   <textarea id="Textarea1" runat="server" rows="3" class="form-control validate[optional] uppercase"
                                                                maxlength="300"></textarea>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>  
                                  <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl83" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                   <asp:TextBox ID="TextBox9" runat="server" class="form-control validate[optional]"
                                                                MaxLength="8"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>   
                                  <div class="box-header with-border">
                            <h3 class="box-title"><asp:Label ID="ps_lbl84" runat="server"></asp:Label></h3>
                        </div>
                             <div class="box-body">&nbsp;</div>     
                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl85" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                        <div class="col-sm-4">
                                      <label>
                                                                <asp:RadioButton ID="RadioButton3" runat="server" GroupName="radio2" />&nbsp; Tunai
                                                            </label>
                                            </div>
                                        <div class="col-sm-4">
                                        <label>
                                                               <asp:RadioButton ID="RadioButton4" runat="server" GroupName="radio2" />&nbsp; Pembaiyaan
                                                            </label>
                                            </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl88" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                    <asp:TextBox ID="txt_bel4" runat="server" class="form-control validate[optional,custom[number]] au_amt5"
                                                                onblur="addTotal_bk5(this)"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>    
                                  <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl89" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                     <div class="input-group">
                                                  <asp:TextBox ID="txt_nopt4" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                    placeholder="DD/MM/YYY"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl90" runat="server"></asp:Label></label>                                                                  
                                    <div class="col-sm-8">
                                  <asp:TextBox ID="txt_pembiayaan4" runat="server" class="form-control validate[optional,custom[number]] au_amt6"
                                                                onblur="addTotal_bk6(this)"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>  
                                  <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl91" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                 <asp:DropDownList ID="DD_Namabank4" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                            </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl92" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                 <asp:TextBox ID="txt_pembiayan4" runat="server" class="form-control validate[optional] uppercase"
                                                                MaxLength="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                  <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl93" runat="server"></asp:Label>(%) </label>
                                    <div class="col-sm-8">
                                <asp:TextBox ID="txt_kadar4" runat="server" class="form-control validate[optional]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl94" runat="server"></asp:Label> (RM) </label>
                                    <div class="col-sm-8">
                                 <asp:TextBox ID="txt_bayaran4" runat="server" class="form-control validate[optional,custom[number]] au_amt7"
                                                                onblur="addTotal_bk7(this)"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                  <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl95" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                <asp:TextBox ID="txt_BD" runat="server" class="form-control validate[optional] uppercase"
                                                                MaxLength="150"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl96" runat="server"></asp:Label> <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                <asp:DropDownList ID="dd_ejen" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                            </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>  
                                  <div class="box-header with-border">
                            <h3 class="box-title"><asp:Label ID="ps_lbl97" runat="server"></asp:Label></h3>
                        </div>
                             <div class="box-body">&nbsp;</div>     
                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl98" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                                       <asp:TextBox ID="txt_pintu4" runat="server" class="form-control validate[optional,custom[dtfmt]]  datepicker mydatepickerclass"
                                                                    placeholder="DD/MM/YYY"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl99" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                    <asp:TextBox ID="txt_cukaipintu4" runat="server" class="form-control validate[optional,custom[number]] au_amt8"
                                                                onblur="addTotal_bk8(this)"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>    
                                  <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl100" runat="server"></asp:Label></label>                                                 
                                    <div class="col-sm-8">
                                     <div class="input-group">
                                         <asp:TextBox ID="txt_tariktanah4" runat="server" class="form-control validate[optional,custom[dtfmt]]  datepicker mydatepickerclass"
                                                                    placeholder="DD/MM/YYY"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl101" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                  <asp:TextBox ID="txt_amauntanah4" runat="server" class="form-control validate[optional,custom[number]] au_amt9"
                                                                onblur="addTotal_bk9(this)"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>  
                                  <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl102" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                   <div class="input-group">
                                         <asp:TextBox ID="txt_cukaibardar4" runat="server" class="form-control validate[optional,custom[dtfmt]]  datepicker mydatepickerclass"
                                                                    placeholder="DD/MM/YYY"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl103" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                 <asp:TextBox ID="txt_amaunbardar4" runat="server" class="form-control validate[optional,custom[number]] au_amt10"
                                                                onblur="addTotal_bk10(this)"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                   <div class="box-header with-border">
                            <h3 class="box-title"><asp:Label ID="ps_lbl104" runat="server"></asp:Label> </h3>
                        </div>
                             <div class="box-body">&nbsp;</div>
                                  <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl105" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                <asp:DropDownList ID="DD_PEGAWAI4" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                            </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl106" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                 <asp:DropDownList ID="DD_PEGAWAIAST4" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                            </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                 <div class="box-header with-border">
                            <h3 class="box-title"><asp:Label ID="ps_lbl107" runat="server"></asp:Label></h3>
                        </div>
                             <div class="box-body">&nbsp;</div>
                                  <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl108" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                               <asp:TextBox ID="txt_nodafter5" runat="server" class="form-control validate[optional]"
                                                                MaxLength="15"></asp:TextBox>
                                                            <asp:TextBox ID="TextBox11" runat="server" Visible="false" class="form-control validate[optional]"
                                                                MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl109" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                <asp:TextBox ID="txt_namasyarikat5" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>  
                                  <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl110" runat="server"></asp:Label> </label>                              
                                    <div class="col-sm-8">
                                <asp:TextBox ID="txt_alamat5" Columns="20" Rows="2" TextMode="multiline" runat="server"
                                                                class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl111" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                <asp:TextBox ID="txt_nama_hubu5" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>  
                                  <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl112" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                              <asp:TextBox ID="txt_poskod5" runat="server" class="form-control validate[optional]"
                                                                MaxLength="5"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl113" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                               <asp:TextBox ID="txt_notele5" runat="server" class="form-control validate[optional]"
                                                                MaxLength="11"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>  
                                  <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl114" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                               <asp:TextBox ID="txt_bardar5" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl115" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                <asp:TextBox ID="txt_email5" type="email" runat="server" class="form-control validate[optional]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>  
                                  <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl116" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                               <asp:DropDownList ID="DD_Negiri5" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                            </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>  
                                 </div>
                             <div id="btnsimpan" runat="server" visible="false">
                                 <hr />
                                  <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                  <asp:Button ID="Simpan" runat="server" class="btn btn-danger" Text="Simpan" UseSubmitBehavior="false"                                            
                                                                OnClick="Simpan_Click1" />
                            </div>
                                 </div>
                                </div>
                                 <div class="box-body">&nbsp;</div>  
                                 </div>
                              <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                <asp:GridView ID="GridView2" runat="server" class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" OnPageIndexChanging="gv_refdata_PageIndexChanging_com">
                                     <PagerStyle CssClass="pager" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="BIL">
                                                                     <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                            ItemStyle-Width="150" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ASET ID">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("com_asset_id") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="KETERANGAN ASET">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("cas_asset_desc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="No Siri ">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("com_serial_no") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Kod Produk">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("com_prod_no") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Price (RM)">
                                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("com_price_amt","{0:n}") %>'></asp:Label>
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
                            <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                 <asp:GridView ID="GridView3" runat="server" class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" OnPageIndexChanging="gv_refdata_PageIndexChanging_car"
                                                            Visible="false">
                                      <PagerStyle CssClass="pager" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="BIL">
                                                                     <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                            ItemStyle-Width="150" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ASET ID">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("car_asset_id") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="KETERANGAN ASET">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("cas_asset_desc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Plate No">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("car_plate_no") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText=" Model">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("ast_Model_desc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Jenis Dan No Enjin">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("car_engine_no") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Price (RM)">
                                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("car_price_amt","{0:n}") %>'></asp:Label>
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
                            <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                 <asp:GridView ID="GridView4" runat="server" class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" OnPageIndexChanging="gv_refdata_PageIndexChanging"
                                                            Visible="false">
                                      <PagerStyle CssClass="pager" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="BIL">
                                                                     <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                            ItemStyle-Width="150" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ASET ID">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("inv_asset_id") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="KETERANGAN ASET">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("cas_asset_desc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="KUANTITI">
                                                                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label3_qty" runat="server" Text='<%# Eval("inv_qty") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Jaminan (Tahun)">
                                                                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("inv_warranty") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Catatan">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("inv_remark") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="AMAUN (RM)">
                                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("inv_price_amt","{0:n}") %>'></asp:Label>
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
                             <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                <asp:GridView ID="GridView5" runat="server" class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" OnPageIndexChanging="gv_refdata_PageIndexChanging_pro"
                                                            Visible="false">
                                     <PagerStyle CssClass="pager" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="BIL">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                            ItemStyle-Width="150" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ASET ID">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("pro_asset_id") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="KETERANGAN ASET">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("cas_asset_desc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Negeri ">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label1_nri" runat="server" Text='<%# Eval("Decription") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="No PT">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("pro_pt") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="No Lot">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("pro_lot_no") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Nama Ejen">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("sup_name") %>'></asp:Label>
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
                           
                            
                            <div class="box-body">&nbsp;</div>
                        </div>

                    </div>
                </div>
            </div>
            <!-- /.row -->
            </ContentTemplate>
    </asp:UpdatePanel>
        
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
    
</asp:Content>

