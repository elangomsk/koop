<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Kewengan/kw_lep_imbangan.aspx.cs" Inherits="kw_lep_imbangan"  %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
     



       <script type="text/javascript">
             $(document).ready(function () {
                 $(<%=dd_akaun.ClientID%>).SumoSelect(
                    { selectAll: true });

                 $('.select2').select2();
             });
            function addTotal_bk1() {
           
                var amt1 = Number($("#<%=TextBox4.ClientID %>").val().replace(",", ""));

                  $(".au_amt").val(amt1.toFixed(2));
            
          }
    </script>

    <script type="text/javascript">
    var IE = (navigator.userAgent.indexOf('MSIE') != -1);
    function no_scrollbar()
    {
	    if(!IE) return;
	    // no scrollbars no IE
	    var root = document.all[1]; // IE >= 4
	    var firstCall = (root.style.overflow != 'auto');
	    document.body.style.width = root.clientWidth + 'px';

	    if(firstCall)
		    root.style.overflow = 'auto';
    }
    onload = no_scrollbar;
    </script>

     <script type="text/javascript">
        $(function () {
            $('[id*=GridView1]').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
                "responsive": true,
                "sPaginationType": "full_numbers"
            });
        });
            </script>
    <style>
        .ZebraDialog{
            z-index: 1000001 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server" ScriptMode="Release">
    </asp:ScriptManager>

   
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1><asp:Label ID="ps_lbl1" Visible="false" runat="server"></asp:Label> Imbungan Duga</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i><asp:Label ID="ps_lbl2" Visible="false" runat="server"></asp:Label>Kewangan</a></li>
                            <li class="active"><asp:Label ID="ps_lbl3" Visible="false" runat="server"></asp:Label>Imbungan Duga</li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
       <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div class="row">
               <!-- /.col -->
               
                <div class="col-md-12">

                   <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title">Pilih Tahun Kewangan</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                         <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                                 <div class="row" style="display:none;">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Mula <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                         <asp:TextBox ID="Tk_mula"  runat="server" class="form-control validate[optional] datepicker mydatepickerclass" autocomplete="off" placeholder="DD/MM/YYYY"></asp:TextBox>
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Akhir <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                       <asp:TextBox ID="Tk_akhir"  runat="server" class="form-control validate[optional] datepicker mydatepickerclass" autocomplete="off" placeholder="DD/MM/YYYY"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tahun Kewangan <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList id="DropDownList1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jenis Imbangan Duga <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList id="kat_bajet" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                            <asp:ListItem Value="">--- PILIH ---</asp:ListItem>
                                            <asp:ListItem Value="01">Unadjusted Trial Balance</asp:ListItem>
                                            <asp:ListItem Value="02">The Adjusted Trial Balance</asp:ListItem>                                            
                                            <asp:ListItem Value="03">The Post-Closing Trial Balance</asp:ListItem>                                            
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">       
                                     
                                    <div class="col-sm-8">
                                        <asp:Button ID="Button7" runat="server" class="btn btn-success" Text="Jana Laporan" OnClick="clk_srch" UseSubmitBehavior="false" />
                                       <asp:Button ID="Button5" runat="server" class="btn btn-danger sub_btn" Text="Simpan" Visible="false" OnClick="sub_but"  UseSubmitBehavior="false" />
                                        <asp:Button ID="Button9" runat="server" class="btn btn-default" Text="Set Semula" OnClick="rest_but" UseSubmitBehavior="false" />
                                <asp:Button ID="Button6" runat="server" class="btn btn-warning" Text="Eksport PDF" Visible="false" OnClick="ctk_values" UseSubmitBehavior="false" />
                                        <asp:Button ID="Button8" runat="server" class="btn btn-warning" Text="Eksport Excel" OnClick="ExportToEXCEL" Visible="false" UseSubmitBehavior="false" />

                                        
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                         <hr />                     
                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                    <%--  <div class="row" >--%>
           <div class="col-md-12 box-body">
                <asp:GridView ID="GridView1" runat="server"  class="table table-bordered table-hover dataTable uppercase" AllowSorting="true"  AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="5000" GridLines="None" OnRowDataBound="GridView1_RowDataBound"  ShowFooter="true">
                                                        <%--<HeaderStyle ForeColor="#ffffff" />--%>
                                                                               
                                                        <Columns>
                                                       
                                                            <asp:TemplateField HeaderText = "No Akaun" ItemStyle-Width="25%">
                                                                
                                                               <ItemTemplate>
                                                              <asp:Label ID = "bal_type" runat="server" Text='<%# Eval("kod_akaun").ToString() %>' CssClass="uppercase"></asp:Label>
                                                                   <asp:Label ID="Label2_nw" runat="server" Visible="false" Text='<%# Eval("kw_acc_header") %>'></asp:Label>
                                                                   <asp:Label ID="Label1_nw" runat="server" Visible="false" Text='<%# Eval("jenis_akaun_type") %>'></asp:Label>

                                                          </ItemTemplate>

                                                       </asp:TemplateField>

                                                             <asp:TemplateField HeaderText = "Keterangan" ItemStyle-Width="25%" >
                                                                
                                                                <ItemTemplate>
                                                                        <asp:Label ID = "kat_cd" runat="server" Text='<%# Eval("nama_akaun").ToString() %>' CssClass="uppercase"></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            
                                                           
                                                       <asp:TemplateField HeaderText = "Debit (RM)" HeaderStyle-HorizontalAlign="right" ItemStyle-Width="15%">
                                                                <ItemStyle HorizontalAlign = "Right" Font-Bold Width="15%"></ItemStyle>
                                                                <ItemTemplate>
                                                                        <asp:Label ID = "Label3" runat="server" Text='<%#Eval("Amt1","{0:C}").Replace("$","").Replace("RM","")%>' CssClass="uppercase"></asp:Label>

                                                                   

                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText = "kredit (RM)" HeaderStyle-HorizontalAlign="right">
                                                                <ItemStyle HorizontalAlign = "Right" Font-Bold Width="15%"></ItemStyle>
                                                                <ItemTemplate>
                                                                        <asp:Label ID = "Label4" runat="server" Text='<%#Eval("Amt2","{0:C}").Replace("$","").Replace("RM","") %>' CssClass="uppercase"></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            
                                                            
                                                             <asp:TemplateField HeaderText = "Nota" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="3%" >
                                                              
                                                                    <ItemTemplate>
                                                                         <asp:LinkButton ID = "LinkButton1" runat="server" ToolTip="Edit" CommandArgument='Edit' OnClick="clk_update"  Font-Bold>
                                                                                                    <i class='fa fa-edit'></i>
                                                                                                </asp:LinkButton>                                                                      
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
             <%--   <table cellpadding='7' border="1" cellspacing='7' style="border-color:lightgray; line-height:15px;" width='80%' class='col-md-12 table-responsive'>
                                                    <tr style="background-color:#BDC4C7; color:black;">
                                                        <td style="width:20%; padding-left:20px;"><strong>KOD AKAUN</strong></td>
                                                        <td style="width:60%; padding-left:20px;"><strong>NAMA AKAUN</strong></td>
                                                        <td style="width:20%; text-align:right; padding-right:20px;"><strong>ACTIONS</strong></td>
                                                        
                                                    </tr>
                                                
                                                </table>--%>
               
                                              <%--  <asp:TreeView  ID="tvHierarchyView" CssClass="tree" runat="server" ontreenodeexpanded="MenuTree_TreeNodeExpanded" ForeColor="Black" NodeWrap="true" ShowLines="true"   ShowExpandCollapse="false">

                                                     <ParentNodeStyle Font-Bold="False"/>
               <HoverNodeStyle Font-Underline="False" ForeColor="Purple" />
               <SelectedNodeStyle Font-Underline="False" HorizontalPadding="0px" VerticalPadding="0px" ForeColor="#663300" />
               <RootNodeStyle />
               <NodeStyle Font-Size="10pt" Width="100%" BackColor="Transparent" ForeColor="DarkBlue" HorizontalPadding="2px" NodeSpacing="2px" VerticalPadding="2px"/>
               <LeafNodeStyle ForeColor="#CC0099"/>
      
</asp:TreeView>--%>
                                   <asp:GridView Visible="false" CellPadding="8" CellSpacing="8" ID="gv_refdata" runat="server" CssClass="table datatable dataTable no-footer uppercase" Width="100%" HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#507CD1" AllowPaging="false" AutoGenerateColumns="false">
                                                    <Columns>
                                                    <asp:TemplateField HeaderText="BIL">  
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="NAMA SYARIKAT">
                                                           <ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl1" runat="server" Text='<%# Eval("kod_akaun1").ToString() %>'></asp:Label>
                                                                  <asp:Label ID="lbl2" runat="server" Text='<%# Eval("nama_bajet").ToString() %>'></asp:Label>
                                                                <asp:Label ID="lbl3" runat="server" Text='<%# Eval("bjt_open_amt").ToString() %>'></asp:Label>
                                                                <asp:Label ID="lbl4" runat="server" Text='<%# Eval("jenis_bajet_type").ToString() %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>                                                    
                                                    </Columns>
                                                </asp:GridView>
                <rsweb:ReportViewer Visible="false" ID="Rptviwer_baki" runat="server" Width="50%"></rsweb:ReportViewer>
               </div>
        <%--  </div>--%>
                                    <cc1:ModalPopupExtender BackgroundCssClass="modalBg" DropShadow="true" ID="ModalPopupExtender1"
                                                                PopupControlID="Panel3" runat="server" TargetControlID="btnBack" PopupDragHandleControlID="Panel2"
                                                                CancelControlID="btnBack">
                                                            </cc1:ModalPopupExtender>
                                      <asp:Panel ID="Panel3" runat="server" CssClass="modalPanel" Style="display: none;  overflow-y:auto; height: 30vh;">
                                          <a class="popupCloseButton" id="btnBack" runat="server"></a>
                                                   <table border="0" cellpadding="6" cellspacing="0" class="tblborder">
                                                                    <tr>
                                                                        <td width="170px" align="left"><span class="leftpadding " style="font-weight:bold;"><asp:Label ID="hdr_txt" runat="server"></asp:Label></td>
                                                                        <td width="10%" align="center"></td>
                                                                        <td width="230px" align="left">   
                                                                        </td>
                                                                        </tr>                                                       
                                                                    <tr style="display: none;">
                                                                        <td width="140px" align="left"><span class="leftpadding ">Nama Induk Bajet</td>
                                                                        <td width="10%" align="center">:</td>
                                                                        <td width="270px" align="left">    <asp:DropDownList id="kat_akaun" readonly="true"  style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                       </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                     <tr id="ss1" runat="server" style="display: none;"  visible="false">
                                                                        <td width="140px" align="left"><span class="leftpadding ">Parent Bajet</td>
                                                                        <td width="10%" align="center">:</td>
                                                                        <td width="270px" align="left"> <asp:TextBox ID="TextBox2" readonly="true" runat="server" Width="100%" class="form-control validate[optional] uppercase"></asp:TextBox>
                                                                            <asp:TextBox ID="TextBox3" runat="server" Width="100%" Visible="false" class="form-control validate[optional] uppercase"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                     <tr style="display: none;">
                                                                        <td width="140px" align="left"><span class="leftpadding ">Nama Bajet</td>
                                                                        <td width="10%" align="center">:</td>
                                                                        <td width="270px" align="left"> <asp:TextBox ID="TextBox1" runat="server" readonly="true" style="width:100%;" class="form-control validate[optional] uppercase"  MaxLength="100"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    
                                                         <tr style="display: none;">
                                                                        <td width="140px" align="left"><span class="leftpadding ">Tarikh Mula</td>
                                                                        <td width="10%" align="center">:</td>
                                                                        <td width="270px" align="left">   <div class="input-group">
                                                          <asp:TextBox ID="TextBox5" runat="server" Width="100%" class="form-control datepicker mydatepickerclass" autocomplete="off"
                                                                                        placeholder="DD/MM/YYYY"></asp:TextBox>
                                               <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                                        </td>
                                                                    </tr>
                                                         <tr style="display: none;">
                                                                        <td width="140px" align="left"><span class="leftpadding ">Tarikh Akhir</td>
                                                                        <td width="10%" align="center">:</td>
                                                                        <td width="270px" align="left">   <div class="input-group">
                                                          <asp:TextBox ID="TextBox6" runat="server" class="form-control datepicker mydatepickerclass" Width="100%" autocomplete="off"
                                                                                        placeholder="DD/MM/YYYY"></asp:TextBox>
                                               <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                                        </td>
                                                                    </tr>
                                                        <tr style="display: none;">
                                                                        <td width="140px" align="left"><span class="leftpadding ">Kategory Bajet <span class="style1">*</span></td>
                                                                        <td width="10%" align="center">:</td>
                                                                        <td width="270px" align="left"><asp:DropDownList ID="dd_kodind" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                               <asp:ListItem value="">--- PILIH ---</asp:ListItem>
                                                            <asp:ListItem value="01">BAHARU</asp:ListItem>
                                                            <asp:ListItem value="02">PINDAAN</asp:ListItem>
                                                                                                  </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                               <tr style="display: none;">
                                                                        <td width="140px" align="left"><span class="leftpadding ">Amaun Bajet (RM) <span class="style1">*</span></td>
                                                                        <td width="10%" align="center">:</td>
                                                                        <td width="270px" align="left"> <asp:TextBox ID="TextBox4" runat="server" onblur="addTotal_bk1(this)" style="width:100%;" class="form-control validate[optional] au_amt"  MaxLength="100"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                        <tr>
                                                                        <td width="140px" align="left"><span class="leftpadding ">Nota</td>
                                                                        <td width="10%" align="center">:</td>
                                                                        <td width="270px" align="left"> <textarea id="txt_nota" class="form-control validate[optional] uppercase" style="width:100%;" runat="server" rows="3"></textarea>
                                                                        </td>
                                                                    </tr>
                                                                           <tr  id="set_kakaun" style="display:none;" runat="server">
                                                                        <td width="140px" align="left"><span class="leftpadding ">Kod Akaun</td>
                                                                        <td width="10%" align="center">:</td>
                                                                        <td width="270px" align="left"> <asp:listbox ID="dd_akaun" runat="server" Width="100%" selectionmode="Multiple" CssClass=" form-control uppercase"></asp:listbox>
                                                                        </td>
                                                                    </tr>
                                                       <tr style="display:none;">
                                                                        <td width="140px" align="left"><span class="leftpadding "></td>
                                                                        <td width="10%" align="center"></td>
                                                                        <td width="270px" align="left">  <asp:CheckBox ID="CheckBox1" runat="server" CssClass="mycheckbox" Text="Kunci Kira-Kira" />
                                                                        </td>

                                                                    </tr>
                                                       <tr style="display:none;">
                                                                        <td width="140px" align="left"><span class="leftpadding "></td>
                                                                        <td width="10%" align="center"></td>
                                                                        <td width="270px" align="left"> <asp:CheckBox ID="CheckBox2" runat="server" CssClass="mycheckbox" Text="Pendapatan & Perbelanjan" />
                                                                        </td>

                                                                    </tr>
                                                       <tr style="display:none;">
                                                                        <td width="140px" align="left"><span class="leftpadding "></td>
                                                                        <td width="10%" align="center"></td>
                                                                        <td width="270px" align="left"> <asp:CheckBox ID="CheckBox3" runat="server" CssClass="mycheckbox" Text="Aliran Tunai" />
                                                                        </td>

                                                                    </tr>
                                                       <tr style="display:none;">
                                                                        <td width="140px" align="left"><span class="leftpadding "></td>
                                                                        <td width="10%" align="center"></td>
                                                                        <td width="270px" align="left">   <asp:CheckBox ID="CheckBox4" runat="server" CssClass="mycheckbox" Text="Pembahagian" />
                                        
                                                                        </td>

                                                                    </tr>
                                                       <tr style="display:none;">
                                                                        <td width="140px" align="left"><span class="leftpadding "></td>
                                                                        <td width="10%" align="center"></td>
                                                                        <td width="270px" align="left"><asp:CheckBox ID="CheckBox5" runat="server" CssClass="mycheckbox" Text="COGS" />
                                                                        </td>

                                                                    </tr>
                                                      
                                                         <tr style="display:none;">
                                                                        <td width="140px" align="left"><span class="leftpadding ">Status</td>
                                                                        <td width="10%" align="center">:</td>
                                                                        <td width="270px" align="left"> <asp:DropDownList ID="dd_list_sts" runat="server" Width="100%" class="form-control select2 validate[optional] uppercase">
                                                                    <asp:ListItem Text="AKTIF" Value="A" />
                                                                    <asp:ListItem Text="TIDAK AKTIF" Value="T" />
                                                                </asp:DropDownList>
                                                                        </td>

                                                                    </tr>
                                                        <tr style="display:none;">
                                                                        <td width="140px" align="left"><span class="leftpadding ">Account Header</td>
                                                                        <td width="10%" align="center">:</td>
                                                                        <td width="270px" align="left"> <asp:CheckBox ID="RadioButton1" runat="server" CssClass="mycheckbox" />
                                                                        </td>

                                                                    </tr>
                                                                     <tr>
                                                                        <td colspan="3" align="right"><hr /></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="3" align="center">
                                                                       <asp:TextBox ID="lbl_name" runat="server" class="form-control validate[optional] uppercase" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="ver_id" Visible="false" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                                                         <asp:TextBox ID="get_id" Visible="false" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                                                        <asp:TextBox ID="get_cd" Visible="false" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                                 <asp:Button ID="Button4" runat="server" class="btn btn-danger" Text="Simpan" OnClick="clk_submit" UseSubmitBehavior="false" />
                                <asp:Button ID="Button1" runat="server" class="btn btn-default" Visible="false" Text="Set Semula" UseSubmitBehavior="false"  />
                                <asp:Button ID="Button2" runat="server" class="btn btn-default" Visible="false" Text="Kembali" UseSubmitBehavior="false" />
                                 <%--<asp:Button ID="btnBack" runat="server" CssClass="btn btn-default" style="display:none;" Text="Keluar" />--%>
                                                                            <asp:Button ID="Button3" runat="server" CssClass="btn btn-default" OnClick="Button5_Click" Text="Keluar" />
                                                                        </td>
                                                                    </tr>
                                                                    </table>          
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.col -->
             <div class="col-md-12 box-body">
        <div class="table-new-style-scr"> 
               <div class="col-md-1"> &nbsp; </div>
                <div class="col-md-10 col-sm-12 col-xs-12 table-new-style-scr">
                             <rsweb:ReportViewer ID="RptviwerStudent" runat="server" width="100%" Height="100%" ZoomMode="PageWidth" SizeToReportContent="True"></rsweb:ReportViewer>
                                                            <asp:Label runat="server" ID="ReportErrorMessage" Visible="false" CssClass="report-error-message"></asp:Label>
               </div>
        </div>
   </div>
            
            
            <!-- /.row -->
           
           
           
        </ContentTemplate>
            <Triggers>
               <asp:PostBackTrigger ControlID="Button6"/>
                <asp:PostBackTrigger ControlID="Button8"/>  
                           
           </Triggers>
    </asp:UpdatePanel>
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
   
</asp:Content>

