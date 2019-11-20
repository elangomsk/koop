<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Kewengan/kw_baki_pembukaan_view.aspx.cs" Inherits="kw_baki_pembukaan_view" enableEventValidation="false"%>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%-- <script type="text/javascript">
         
             function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
                 var tbl = gridId.replace("ContentPlaceHolder1_", "");
                
                 if (tbl) {
                     var DivHR = document.getElementById('DivHeaderRow');
                     var DivMC = document.getElementById('DivMainContent');
                     var DivFR = document.getElementById('DivFooterRow');

                     //*** Set divheaderRow Properties ****
                     DivHR.style.height = headerHeight + 'px';
                     DivHR.style.width = (parseInt(width) - 16) + 'px';
                     DivHR.style.position = 'relative';
                     DivHR.style.top = '0px';
                     DivHR.style.zIndex = '10';
                     DivHR.style.verticalAlign = 'top';

                     //*** Set divMainContent Properties ****
                     DivMC.style.width = width + 'px';
                     DivMC.style.height = height + 'px';
                     DivMC.style.position = 'relative';
                     DivMC.style.top = -headerHeight + 'px';
                     DivMC.style.zIndex = '1';

                     //*** Set divFooterRow Properties ****
                     DivFR.style.width = (parseInt(width) - 16) + 'px';
                     DivFR.style.position = 'relative';
                     DivFR.style.top = -headerHeight + 'px';
                     DivFR.style.verticalAlign = 'top';
                     DivFR.style.paddingtop = '2px';

                     if (isFooter) {
                         var tblfr = tbl.cloneNode(true);
                         tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
                         var tblBody = document.createElement('tbody');
                         tblfr.style.width = '100%';
                         tblfr.cellSpacing = "0";
                         tblfr.border = "0px";
                         tblfr.rules = "none";
                         //*****In the case of Footer Row *******
                         tblBody.appendChild(tbl.rows[tbl.rows.length - 1]);
                         tblfr.appendChild(tblBody);
                         DivFR.appendChild(tblfr);
                     }
                     //****Copy Header in divHeaderRow****
                     DivHR.appendChild(tbl.cloneNode(true));
                 }
             }



             function OnScrollDiv(Scrollablediv) {
                 document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
                 document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
             }

         
</script>--%>
       <script type="text/javascript">
             $(document).ready(function () {
               
                 $('.select2').select2();
             });
            function addTotal_bk1() {
           
                var amt1 = Number($("#<%=TextBox4.ClientID %>").val().replace(",", ""));

                  $(".au_amt").val(amt1.toFixed(2));
            
          }
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
                        <h1><asp:Label ID="ps_lbl1" Visible="false" runat="server"></asp:Label> Baki Pembukaan</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i><asp:Label ID="ps_lbl2" Visible="false" runat="server"></asp:Label>Kewangan</a></li>
                            <li class="active"><asp:Label ID="ps_lbl3" Visible="false" runat="server"></asp:Label>Baki Pembukaan</li>
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
                         
                        <div class="form-horizontal">
                            <div class="box-body">&nbsp;</div>
                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tahun Kewangan <%--<span class="style1">*</span>--%> </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="tah_kewangan" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                       </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 
                                 <div class="col-md-4 box-body">
                                <div class="form-group">       
                                    <div class="col-sm-8">                                       
                                        <asp:Button ID="Button5" runat="server" class="btn btn-danger" Text="Carian" OnClick="clk_srch" UseSubmitBehavior="false" />
                                <asp:Button ID="Button6" runat="server" class="btn btn-warning" Text="Cetak" Visible="false" OnClick="ctk_values" UseSubmitBehavior="false" />
                                        <asp:Button ID="Button7" runat="server" class="btn btn-success" Text="Confirm" OnClick="fin_opn_close" UseSubmitBehavior="false" />
                                        <asp:Button ID="Button8" runat="server" Visible="false" class="btn btn-default" Text="Kempali" OnClick="Back_to_profile" UseSubmitBehavior="false" />
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                                                                               
                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                    <%--  <div class="row" >--%>
           <div class="col-md-12 box-body">
             <%-- <div id="DivRoot">
    <div style="overflow: hidden;" id="DivHeaderRow">
    </div>

    <div style="overflow:scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">--%>
                <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="5000" GridLines="None" OnRowDataBound="GridView1_RowDataBound" ShowFooter="true">
                                                                     
                                                        <Columns>
                                                           <%-- <asp:TemplateField HeaderText="BIL">
                                                                <ItemStyle HorizontalAlign="Center" Width="2%"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                              <asp:TemplateField HeaderText="KOD KAUN">
                                                                <ItemStyle HorizontalAlign="Left" Width="25%"></ItemStyle>
                                                                <ItemTemplate>
                                                                        <asp:Label ID="bal_type" runat="server" Text='<%# Eval("kod_akaun").ToString() %>' CssClass="uppercase"></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="NAMA AKAUN">
                                                                <ItemStyle HorizontalAlign="Left" Width="50%"></ItemStyle>
                                                                <ItemTemplate>
                                                                   <%-- <asp:LinkButton ID="lnkView" runat="server" OnClick="lnkView_Click"  commandName="LinkButtonClicked" commandArgument='<%# Container.DataItemIndex + 1 %>' Font-Bold>--%>
                                                                        <asp:Label ID="kat_cd" runat="server" Text='<%# Eval("nama_akaun").ToString() %>' CssClass="uppercase"></asp:Label>
                                                                         <asp:Label ID="og_genid" runat="server" Visible="false" Text='<%# Eval("Id") %>'></asp:Label>
                                                                    <asp:Label ID="Label2" runat="server" Visible="false" Text='<%# Eval("isHeader") %>'></asp:Label>
                                                                    <asp:Label ID="Label1" runat="server" Visible="false" Text='<%# Eval("jenis_akaun_type") %>'></asp:Label>
                                                                    <asp:Label ID="Label4" runat="server" Visible="false" Text='<%# Eval("kat_akaun") %>'></asp:Label>
                                                                   <%-- </asp:LinkButton>--%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Debit (RM)" HeaderStyle-HorizontalAlign="right">
                                                                <ItemStyle HorizontalAlign="Right" Font-Bold Width="15%"></ItemStyle>
                                                                <ItemTemplate>
                                                                        <asp:Label ID="Label3_1" runat="server" Text='<%# Eval("Amount1","{0:C}").Replace("RM","").Replace("$","") %>' CssClass="uppercase"></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>     
                                                              <asp:TemplateField HeaderText="Kredit (RM)" HeaderStyle-HorizontalAlign="right">
                                                                <ItemStyle HorizontalAlign="Right" Font-Bold Width="15%"></ItemStyle>
                                                                <ItemTemplate>
                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("Amount","{0:C}").Replace("RM","").Replace("$","") %>' CssClass="uppercase"></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>                                                      
                                                             <asp:TemplateField HeaderText="Action">
                                                                <ItemStyle HorizontalAlign="center" Width="5%"></ItemStyle>
                                                                    <ItemTemplate>
                                                                         <asp:LinkButton ID="LinkButton1" runat="server" ToolTip="Edit" CommandArgument='Edit' OnClick="clk_update"  Font-Bold>
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
                 <%--  </div>

    <div id="DivFooterRow" style="overflow:hidden">
    </div>
</div>--%>
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
                                                        <tr>
                                                                        <td width="140px" align="left"><span class="leftpadding ">Jenis Type <span class="style1">*</span></td>
                                                                        <td width="10%" align="center">:</td>
                                                                        <td width="270px" align="left"><asp:DropDownList ID="dd_kodind" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                               <asp:ListItem value="">--- PILIH ---</asp:ListItem>
                                                            <asp:ListItem value="D">Debit</asp:ListItem>
                                                            <asp:ListItem value="K">Kredit</asp:ListItem>
                                                                                                  </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                               <tr>
                                                                        <td width="140px" align="left"><span class="leftpadding ">Amaun (RM) <span class="style1">*</span></td>
                                                                        <td width="10%" align="center">:</td>
                                                                        <td width="270px" align="left"> <asp:TextBox ID="TextBox4" runat="server" style="width:100%; text-align:right;" class="form-control validate[optional] au_amt"  MaxLength="100"></asp:TextBox>
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
            
            <!-- /.row -->
           
        </ContentTemplate>
            <Triggers>
               <asp:PostBackTrigger ControlID="Button6"  />                 
                  </Triggers>
    </asp:UpdatePanel>
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
   
</asp:Content>

