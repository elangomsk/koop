<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_SEMAK_HAJI.aspx.cs" Inherits="HR_SEMAK_HAJI" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type = "text/javascript">
        function Check_Click(objRef) {
            //Get the Row based on checkbox
            var row = objRef.parentNode.parentNode;
            if (objRef.checked) {
                //If checked change color to Aqua
                row.style.backgroundColor = "aqua";
            }
            else {
                //If not checked change back to original color
                if (row.rowIndex % 2 == 0) {
                    //Alternating Row Color
                    row.style.backgroundColor = "#C2D69B";
                }
                else {
                    row.style.backgroundColor = "white";
                }
            }

            //Get the reference of GridView
            var GridView = row.parentNode;

            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {
                //The First element is the Header Checkbox
                var headerCheckBox = inputList[0];

                //Based on all or none checkboxes
                //are checked check/uncheck Header Checkbox
                var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;

        }
</script>
<script type = "text/javascript">
    function checkAll(objRef) {
        var GridView = objRef.parentNode.parentNode.parentNode;
        var inputList = GridView.getElementsByTagName("input");
        for (var i = 0; i < inputList.length; i++) {
            //Get the Cell To find out ColumnIndex
            var row = inputList[i].parentNode.parentNode;
            if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                if (objRef.checked) {
                    //If the header checkbox is checked
                    //check all checkboxes
                    //and highlight all rows
                    row.style.backgroundColor = "aqua";
                    inputList[i].checked = true;
                }
                else {
                    //If the header checkbox is checked
                    //uncheck all checkboxes
                    //and change rowcolor back to original 
                    if (row.rowIndex % 2 == 0) {
                        //Alternating Row Color
                        row.style.backgroundColor = "#C2D69B";
                    }
                    else {
                        row.style.backgroundColor = "white";
                    }
                    inputList[i].checked = false;
                }
            }
        }
    }
</script>
<script type = "text/javascript">
    function MouseEvents(objRef, evt) {
        var checkbox = objRef.getElementsByTagName("input")[0];
        if (evt.type == "mouseover") {
            objRef.style.backgroundColor = "orange";
        }
        else {
            if (checkbox.checked) {
                objRef.style.backgroundColor = "aqua";
            }
            else if (evt.type == "mouseout") {
                if (objRef.rowIndex % 2 == 0) {
                    //Alternating Row Color
                    objRef.style.backgroundColor = "#C2D69B";
                }
                else {
                    objRef.style.backgroundColor = "white";
                }
            }
        }
    }
</script>   
<script type="text/javascript">
    $("[src*=plus]").live("click", function () {
        $(this).closest("tr").after("<tr><td></td><td colspan = '1200'>" + $(this).next().html() + "</td></tr>")
        $(this).attr("src", "images/minus.png");
    });
    $("[src*=minus]").live("click", function () {
        $(this).attr("src", "images/plus.png");
        $(this).closest("tr").next().remove();
    });
</script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:ScriptManager ID="ScriptManagerCalendar" ScriptMode="Release" runat="server">
    </asp:ScriptManager>

   
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1 id="h1_tag" runat="server">   </h1>
                        <ol class="breadcrumb">
                            <li><a href="#" id="bb1_text" runat="server"><i class="fa fa-dashboard"></i>Sumber Manusia</a></li>
                            <li class="active" id="bb2_text" runat="server">  Semak Maklumat Penggajian </li>
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
                            <h3 class="box-title" id="h3_tag" runat="server"> Maklumat Gaji </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="lbl1_text" runat="server">Nama Syarikat / Organisasi <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="dd_org" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" AutoPostBack="true"  OnSelectedIndexChanged="sel_orgbind">
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
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="lbl2_text" runat="server">    Perniagaan </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="dd_org_pen" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" AutoPostBack="true" OnSelectedIndexChanged="sel_orgjaba">
                                            </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="lbl3_text" runat="server"> Jabatan</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="dd_jabatan" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                </div>
                                 </div>
                              
                                <%-- <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl4_text" runat="server"> Tarikh Mula </label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                                    <asp:TextBox ID="tm_date" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                        placeholder="DD/MM/YYYY" autocomplete="off"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl5_text" runat="server"> Tarikh Akhir </label>
                                    <div class="col-sm-8">
                                        <div class="input-group">                                                    
                                                    <asp:TextBox ID="ta_date" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                        placeholder="DD/MM/YYYY" autocomplete="off"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                </div>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>--%>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="lbl8_text" runat="server"> Tahun </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox4" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                    <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="lbl9_text" runat="server"> Gaji Bagi Bulan  </label>
                                    <div class="col-sm-8">
                                      <asp:DropDownList ID="DropDownList2" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="lbl6_text" runat="server"> Jenis Laporan  </label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="DropDownList1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                    <asp:ListItem>Laporan Ringkasan Butiran</asp:ListItem>
                                                  <%--  <asp:ListItem>Tetap Maklumat Elaun Laporan</asp:ListItem>
                                                    <asp:ListItem>Tambahan Elaun Laporan Butiran</asp:ListItem>--%>
                                                    <asp:ListItem>SENARAI PINDAHAN BANK</asp:ListItem>
                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body" style="display:none;">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="Label1" runat="server">  </label>
                                    <div class="col-sm-8">
                                          <label><asp:CheckBox ID="chk_assign_rkd" runat="server" /> &nbsp; Jana Jurnal</label>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body" >
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">  </label>
                                    <div class="col-sm-8">
                                          <label><asp:CheckBox ID="chk_kelulusan" runat="server" /> &nbsp; Kelulusan Gaji</label>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                               <div class="row" style="display:none;">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="Label2" runat="server"> Jurnal No  </label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="dd_jurnal" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
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
                               <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Senarai" OnClick="srch_click"
                                                    UseSubmitBehavior="false" />
                                                <asp:Button ID="Button4" runat="server" class="btn btn-default" Text="Set Semula"
                                                    UseSubmitBehavior="false" OnClick="clk_rset" />
                                                <asp:Button ID="Button2" runat="server" class="btn btn-warning" Text="Export Ke PDF" Visible="false" UseSubmitBehavior="false"
                                                    OnClick="Button2_Click" />
                                                      <asp:Button ID="Button5" runat="server" class="btn btn-danger" Visible="false" Text="Export Ke Excel"
                                                    UseSubmitBehavior="false" onclick="Button5_Click"  />
                                   <asp:Button ID="Button3" runat="server" class="btn btn-success" Visible="false" Text="Jana Jurnal & Post Ke Lejer"
                                                    UseSubmitBehavior="false" onclick="jana_clk"  />
                                <asp:Button ID="Button6" runat="server" class="btn btn-warning" Visible="false" Text="Mohon Kelulusan Gaji"
                                                    UseSubmitBehavior="false" onclick="upd_sts_clk"  />
                                 <asp:Button ID="Button7" runat="server" class="btn btn-warning" Visible="false" Text="Jana EFT File"
                                                    UseSubmitBehavior="false" onclick="gen_eft_file"  />
                            </div>
                           </div>
                               </div>
                                     <div class="box-header with-border" id="show_htxt" runat="server" visible="false">
                            <h3 class="box-title">Senarai Maklumat Gaji</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                                 <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                 <asp:GridView ID="gvCustomers" runat="server" class="table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="100" ShowFooter="true" GridLines="None"
                                        DataKeyNames="stf_name" onrowdatabound="gvCustomers_RowDataBound"  OnPageIndexChanging="gvCustomers_PageIndexChanging"  EnablePersistedSelection="true">
                                     <PagerStyle CssClass="pager" />
                                        <Columns>
                                          <asp:TemplateField HeaderText="BIL">
                                         <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                            ItemStyle-Width="150" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                            <asp:BoundField ItemStyle-Width="150px" DataField="stf_name" HeaderText="NAMA" />
                                              <asp:TemplateField HeaderText="NO KAKITANGAN" ItemStyle-Width="150px">  
                                            <ItemTemplate>  
                                                <asp:Label ID="labstf" runat="server" Text='<%# Bind("inc_staff_no") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Bulan" ItemStyle-Width="150px">  
                                            <ItemTemplate>  
                                                <asp:Label ID="labstf_mnth" runat="server" Text='<%# Bind("hr_month_desc") %>'></asp:Label>  
                                                   <asp:Label ID="ss_val1" Visible="false" runat="server" Text='<%# Bind("inc_month") %>'></asp:Label>  
                                                <asp:Label ID="ss_val2" Visible="false" runat="server" Text='<%# Bind("inc_year") %>'></asp:Label>  
                                               
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                           <%--  <asp:BoundField ItemStyle-Width="150px" DataField="inc_staff_no" HeaderText="NO KAKITANGAN" />--%>
                                            
                                             <asp:TemplateField HeaderText="GRED" Visible="false" ItemStyle-Width="100px">  
                                            <ItemTemplate>  
                                                <asp:Label ID="lagred" runat="server" Text='<%# Bind("inc_grade_cd") %>'></asp:Label>  
                                             
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                          <asp:TemplateField HeaderText="GAJI POKOK (RM)" ItemStyle-Width="100px">  
                                          <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="labsal" runat="server" Text='<%# Eval("inc_salary_amt","{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                 <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                  <asp:Label ID="ftr_001" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="ELAUN TETAP (RM)" ItemStyle-Width="150px">
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                <ItemTemplate >
                                                    <asp:Panel ID="pnlProducts" runat="server" Visible="true" Style="position: relative" >
                                                        <asp:GridView ID="gvProducts" runat="server" AutoGenerateColumns="false" PageSize="10"
                                                            AllowPaging="true" CssClass="Nested_ChildGrid" OnPageIndexChanging="gvProducts_PageIndexChanging" >
                                                            <Columns>
                                                                <asp:BoundField ItemStyle-Width="300px" DataField="hr_elau_desc" HeaderText="Keterangan" />
                                                                <asp:BoundField ItemStyle-Width="300px" DataField="fxa_allowance_amt" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" HeaderText="Amaun (RM)" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                     <asp:Label ID="txt_nama" runat="server" Text='<%# Eval("inc_cumm_fix_allwnce_amt" ,"{0:n}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                  <FooterTemplate>
                                                  <asp:Label ID="ftr_002" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="LAIN-LAIN ELAUN (RM)" ItemStyle-Width="150px">
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Panel ID="pnlProducts1" runat="server" Visible="true" Style="position: relative">
                                                        <asp:GridView ID="gvProducts1" runat="server" AutoGenerateColumns="false" PageSize="10"
                                                            AllowPaging="true" CssClass="Nested_ChildGrid" OnPageIndexChanging="gvProducts1_PageIndexChanging">
                                                            <Columns>
                                                                <asp:BoundField ItemStyle-Width="300px" DataField="hr_elau_desc" HeaderText="Keterangan" />
                                                                <asp:BoundField ItemStyle-Width="300px" DataField="xta_allowance_amt" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" HeaderText="Amaun (RM)" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                     <asp:Label ID="txt_nama1" runat="server" Text='<%# Eval("inc_cumm_xtra_allwnce_amt" ,"{0:n}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                 <FooterTemplate>
                                                  <asp:Label ID="ftr_003" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="KLM (RM)" ItemStyle-Width="100px">  
                                              <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="labot" runat="server" Text='<%# Bind("inc_ot_amt","{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                  <FooterStyle HorizontalAlign="Right" />
                                                   <FooterTemplate>
                                                  <asp:Label ID="ftr_005" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Bonus Tahunan (RM)" ItemStyle-Width="100px">  
                                              <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="labot_bonus" runat="server" Text='<%# Bind("inc_bonus_amt","{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                 <FooterStyle HorizontalAlign="Right" />
                                                   <FooterTemplate>
                                                  <asp:Label ID="ftr_004" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Bonus KPI (RM)" ItemStyle-Width="100px">  
                                              <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="labot_kpi" runat="server" Text='<%# Bind("inc_kpi_bonus_amt","{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                     <FooterStyle HorizontalAlign="Right" />
                                              <FooterTemplate>
                                                  <asp:Label ID="ftr_004_2" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                            
                                                 <asp:TemplateField HeaderText="Lain-Lain (RM)" ItemStyle-HorizontalAlign="Right">   
                                            <ItemTemplate>  
                                                 <asp:Label ID="tung_amt" runat="server" Text='<%# Bind("inc_tunggakan_amt","{0:n}") %>'></asp:Label>
                                            </ItemTemplate>  
                                                  <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                  <asp:Label ID="ftr_006_1" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="PENDAPATAN KASAR (RM)" ItemStyle-Width="100px"> 
                                           <ItemStyle HorizontalAlign="Right"></ItemStyle> 
                                            <ItemTemplate>  
                                                <asp:Label ID="labgross" runat="server" Text='<%# Bind("inc_gross_amt" ,"{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                               <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                  <asp:Label ID="ftr_012" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                           <asp:TemplateField HeaderText="CARUMAN KWSP (RM)" ItemStyle-Width="100px"> 
                                           <ItemStyle HorizontalAlign="Right"></ItemStyle> 
                                            <ItemTemplate>  
                                                <asp:Label ID="lapkwap" runat="server" Text='<%# Bind("inc_kwsp_amt" ,"{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                               <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                  <asp:Label ID="ftr_007" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                               
                                          <asp:TemplateField HeaderText="POTONGAN PERKESO (RM)" ItemStyle-Width="100px">  
                                          <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="labper" runat="server" Text='<%# Bind("inc_perkeso_amt" ,"{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                              <FooterStyle HorizontalAlign="Right" />
                                               <FooterTemplate>
                                                  <asp:Label ID="ftr_008" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                                
                                             <asp:TemplateField HeaderText="POTONGAN SIP (RM)" ItemStyle-Width="100px">  
                                          <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="labSIP" runat="server" Text='<%# Bind("inc_SIP_amt" ,"{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                 <FooterStyle HorizontalAlign="Right" />
                                                  <FooterTemplate>
                                                  <asp:Label ID="ftr_009" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="LAIN-LAIN POTONGAN (RM)" ItemStyle-Width="150px">
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Panel ID="pnlProducts3" runat="server" Visible="true" Style="position: relative">
                                                        <asp:GridView ID="gvProducts3" runat="server" AutoGenerateColumns="false" PageSize="10"
                                                            AllowPaging="true" CssClass="Nested_ChildGrid" OnPageIndexChanging="gvProducts3_PageIndexChanging">
                                                            <Columns>
                                                                <asp:BoundField ItemStyle-Width="300px" DataField="hr_poto_desc" HeaderText="Keterangan" />
                                                                <asp:BoundField ItemStyle-Width="300px" DataField="xta_allowance_amt" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" HeaderText="Amaun (RM)" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                     <asp:Label ID="txt_nama3" runat="server" Text='<%# Eval("inc_ded_amt" ,"{0:n}") %>'></asp:Label>
                                                </ItemTemplate>
                                                 <FooterStyle HorizontalAlign="Right" />
                                                 <FooterTemplate>
                                                  <asp:Label ID="ftr_014" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                          <asp:TemplateField HeaderText="PCB (RM)" ItemStyle-Width="100px">  
                                          <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="labpcb" runat="server" Text='<%# Bind("inc_pcb_amt" ,"{0:n}") %>'></asp:Label>  
                                            </ItemTemplate> 
                                              <FooterStyle HorizontalAlign="Right" />
                                               <FooterTemplate>
                                                  <asp:Label ID="ftr_010" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate> 
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="CP 38 (RM)" ItemStyle-Width="100px">  
                                              <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="labot_cp" runat="server" Text='<%# Bind("inc_cp38_amt","{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                     <FooterStyle HorizontalAlign="Right" />
                                                      <FooterTemplate>
                                                  <asp:Label ID="ftr_011" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                            
                                             <asp:TemplateField HeaderText="Jumlah Potongan Pendapatan (RM)" ItemStyle-Width="100px">  
                                              <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="labot_ded" runat="server" Text='<%# Bind("inc_total_deduct_amt","{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                     <FooterStyle HorizontalAlign="Right" />
                                                      <FooterTemplate>
                                                  <asp:Label ID="ftr_006" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                          
                                        
                                              <asp:TemplateField HeaderText="CARUMAN KWSP MAJIKAN (RM)" ItemStyle-Width="100px">  
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lapkwap1" runat="server" Text='<%# Bind("inc_emp_kwsp_amt" ,"{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                    <FooterStyle HorizontalAlign="Right" />
                                             <FooterTemplate>
                                                  <asp:Label ID="ftr_007_2" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="PERKESO MAJIKAN (RM)" ItemStyle-Width="100px">  
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="labper1" runat="server" Text='<%# Bind("inc_emp_perkeso_amt" ,"{0:n}") %>'></asp:Label>  
                                            </ItemTemplate> 
                                                <FooterStyle HorizontalAlign="Right" />
                                             <FooterTemplate>
                                                  <asp:Label ID="ftr_008_2" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate> 
                                                </asp:TemplateField>
                                              <asp:TemplateField HeaderText="SIP MAJIKAN (RM)" ItemStyle-Width="100px">  
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="labSIP1" runat="server" Text='<%# Bind("inc_emp_SIP_amt" ,"{0:n}") %>'></asp:Label>  
                                            </ItemTemplate> 
                                                     <FooterStyle HorizontalAlign="Right" />
                                             <FooterTemplate>
                                                  <asp:Label ID="ftr_009_2" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate> 
                                                </asp:TemplateField>
                                         
                                              <asp:TemplateField HeaderText="PENDAPATAN BERSIH (RM)" ItemStyle-Width="100px" ItemStyle-Font-Size="13px" ItemStyle-Font-Bold>  
                                          <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="labnet" runat="server" Text='<%# Bind("inc_nett_amt" ,"{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                              <FooterStyle HorizontalAlign="Right" />
                                               <FooterTemplate>
                                                  <asp:Label ID="ftr_013" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Gaji Status" ItemStyle-Width="100px">  
                                          <ItemStyle HorizontalAlign="center"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lbl_asts" runat="server" Text='<%# Bind("sts") %>'></asp:Label>  
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
                             <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None"
                                        DataKeyNames="stf_name" >
                    <PagerStyle CssClass="pager" />
                                        <Columns>
                                         <asp:TemplateField HeaderText="BIL">
                                         <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                            ItemStyle-Width="150" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                              <asp:TemplateField HeaderText="NO KAKITANGAN" ItemStyle-Width="200px">  
                                            <ItemTemplate>  
                                                <asp:Label ID="labstf" runat="server" Text='<%# Bind("inc_staff_no") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                
                                          <%--  <asp:BoundField ItemStyle-Width="150px" DataField="inc_staff_no" HeaderText="NO KAKITANGAN" />--%>
                                        
                                          
                                          <asp:TemplateField HeaderText="NAMA" ItemStyle-Width="200px">  
                                          <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="labsal" runat="server" Text='<%# Bind("stf_name") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>

                                             <asp:TemplateField HeaderText="NO. KAD PENGANALAN" ItemStyle-Width="200px">  
                                              <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="labot_bonus" runat="server" Text='<%# Bind("stf_icno") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="NAMA BANK" ItemStyle-Width="200px">  
                                              <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="labot_kpi" runat="server" Text='<%# Bind("Bank_Name") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                              <asp:TemplateField HeaderText="NO. AKAUN" ItemStyle-Width="200px">  
                                              <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="labot" runat="server" Text='<%# Bind("stf_bank_acc_no") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Jumlah(RM)" ItemStyle-Width="200px">  
                                              <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="labot_ded" runat="server" Text='<%# Bind("namt","{0:n}") %>'></asp:Label>  
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
                              <div class="col-md-12 box-body">
               <div class="col-md-1 box-body"> &nbsp; </div>
                <div class="col-md-10 box-body">
                                 <rsweb:ReportViewer ID="Rptviwer_lt" runat="server" Width="100%" style=" overflow:auto;" Height="100%" SizeToReportContent="True">
                                            </rsweb:ReportViewer>
                                            <asp:Label runat="server" ID="ReportErrorMessage" Visible="false" CssClass="report-error-message"></asp:Label>
               </div>
                <div class="col-md-1 box-body"> &nbsp; </div>
               </div>
                               
                           <div class="box-body">&nbsp;
                                    </div>

                        </div>

                    </div>
                </div>
            </div>
            <!-- /.row -->
             </ContentTemplate>
             <Triggers>
               <asp:PostBackTrigger ControlID="Button2"  />      
                 <asp:PostBackTrigger ControlID="Button5"  />               
                 <asp:PostBackTrigger ControlID="Button7"  />    
           </Triggers>
    </asp:UpdatePanel>
        
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
   
</asp:Content>





