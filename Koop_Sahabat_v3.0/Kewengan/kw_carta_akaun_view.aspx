<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Kewengan/kw_carta_akaun_view.aspx.cs" Inherits="kw_carta_akaun_view" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
               <!-- /.col -->
               
                <div class="col-md-12">

                    <div class="box">
                      
                        <div class="box-header">                            
                            <div class="box-body">
                                 <div class="row">

           <div class="col-md-12 box-body">
                                <div class="form-group">
                                    <div class="col-sm-12 text-right">
                                     <%--   <asp:TextBox ID="get_id" Visible="false" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>--%>
                                           <asp:Button ID="Button5" runat="server" class="btn btn-warning" Text="Cetak" OnClick="ctk_values"  UseSubmitBehavior="false" />
                                           <%--<asp:Button ID="Button2" runat="server" class="btn btn-danger sub_btn" Text="Baru" OnClick="clk_new"  UseSubmitBehavior="false" />--%>
                                         <%--  <asp:Button ID="Button3" runat="server" class="btn btn-warning sub_btn" Text="Edit" OnClick="clk_update"  UseSubmitBehavior="false" />
                                        <asp:Button ID="Button1" runat="server" class="btn btn-danger" Text="Hapus" UseSubmitBehavior="false" OnClick="btn_hups_Click" OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;" />--%>
                                    </div>
                                   
                                </div>
                            </div>
                                    
      </div> 
                               
                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                    <%--  <div class="row" >--%>
           <div class="col-md-12 box-body">
                <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="5000" GridLines="None" OnRowDataBound="GridView1_RowDataBound" ShowFooter="false">
                                                        <%--<HeaderStyle ForeColor="#ffffff" />--%>
                                                                               
                                                        <Columns>
                                                              <asp:TemplateField HeaderText="KOD AKAUN">
                                                                <ItemStyle HorizontalAlign="Left" Width="25%"></ItemStyle>
                                                                <ItemTemplate>
                                                                        <asp:Label ID="bal_type" runat="server" Text='<%# Eval("kod_akaun").ToString() %>' CssClass="uppercase"></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="NAMA AKAUN">
                                                                <ItemStyle HorizontalAlign="Left" Width="60%"></ItemStyle>
                                                                <ItemTemplate>
                                                                        <asp:Label ID="kat_cd" runat="server" Text='<%# Eval("nama_akaun").ToString() %>' CssClass="uppercase"></asp:Label>
                                                                         <asp:Label ID="og_genid" runat="server" Visible="false" Text='<%# Eval("Id") %>'></asp:Label>
                                                                    <asp:Label ID="Label1" runat="server" Visible="false" Text='<%# Eval("jenis_akaun_type") %>'></asp:Label>
                                                                    <asp:Label ID="Label2" runat="server" Visible="false" Text='<%# Eval("isHeader") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                           
                                                             <asp:TemplateField HeaderText="Action">
                                                                <ItemStyle HorizontalAlign="Right" Width="15%"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        
                                                                         <asp:LinkButton runat="server" ID="lnkView" ToolTip="Add" CommandArgument='Add' OnClick="clk_new"  Font-Bold> 
                                                                             <i class='fa fa-plus-square'></i>                                               
                                                  </asp:LinkButton>&nbsp;&nbsp;
                                                                         <asp:LinkButton ID="LinkButton1" runat="server" ToolTip="Edit" CommandArgument='Edit' OnClick="clk_update"  Font-Bold>
                                                                                                    <i class='fa fa-edit'></i>
                                                                                                </asp:LinkButton> 
                                                                        &nbsp;&nbsp;
                                                                          <asp:LinkButton ID="LinkButton2" runat="server" ToolTip="Delete" OnClick="btn_hups_Click" OnClientClick="if (!confirm('Are you sure to Delete this Carta Akaun?')) return false;" Font-Bold>
                                                                                                    <i class='fa fa-trash'></i>
                                                                                                </asp:LinkButton>
                                                                     
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                        </Columns>
                                        
                                                    </asp:GridView>
            
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
                                                                  <asp:Label ID="lbl2" runat="server" Text='<%# Eval("nama_akaun").ToString() %>'></asp:Label>
                                                                <asp:Label ID="lbl3" runat="server" Text='<%# Eval("Kw_open_amt").ToString() %>'></asp:Label>
                                                                <asp:Label ID="lbl4" runat="server" Text='<%# Eval("jenis_akaun_type").ToString() %>'></asp:Label>
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
                                      <asp:Panel ID="Panel3" runat="server" CssClass="modalPanel" Style="display: none;  overflow-y:auto; height: 80vh;">
                                          <a class="popupCloseButton" id="btnBack" runat="server"></a>
                                                   <table border="0" cellpadding="6" cellspacing="0" class="tblborder">
                                                                    <tr>
                                                                        <td width="170px" align="left"><span class="leftpadding " style="font-weight:bold;"><asp:Label ID="hdr_txt" runat="server"></asp:Label></td>
                                                                        <td width="10%" align="center"></td>
                                                                        <td width="230px" align="left">   
                                                                        </td>
                                                                        </tr>                                                       
                                                                    <tr>
                                                                        <td width="140px" align="left"><span class="leftpadding ">Kategori Akaun <span class="style1">*</span></td>
                                                                        <td width="10%" align="center">:</td>
                                                                        <td width="270px" align="left">    <asp:DropDownList id="kat_akaun" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                       </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                     <tr id="ss1" runat="server" visible="false">
                                                                        <td width="140px" align="left"><span class="leftpadding ">Parent Akaun</td>
                                                                        <td width="10%" align="center">:</td>
                                                                        <td width="270px" align="left"> <asp:TextBox ID="TextBox2" runat="server" Width="100%" class="form-control validate[optional] uppercase"></asp:TextBox>
                                                                            
                                                                        </td>
                                                                    </tr>
                                                                     <tr>
                                                                        <td width="140px" align="left"><span class="leftpadding ">Nama Akaun <span class="style1">*</span></td>
                                                                        <td width="10%" align="center">:</td>
                                                                        <td width="270px" align="left"> <asp:TextBox ID="TextBox1" runat="server" style="width:100%;" class="form-control validate[optional] uppercase"  MaxLength="100"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                     <tr>
                                                                        <td width="140px" align="left"><span class="leftpadding ">Kod Industri</td>
                                                                        <td width="10%" align="center">:</td>
                                                                        <td width="270px" align="left"><asp:DropDownList ID="dd_kodind" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                     
                                                         <tr>
                                                                        <td width="140px" align="left"><span class="leftpadding "></td>
                                                                        <td width="10%" align="center"></td>
                                                                        <td width="270px" align="left"> <asp:CheckBox ID="RadioButton1" runat="server" OnCheckedChanged="clk_yes" CssClass="mycheckbox" Text="Akaun Susut Nilai" AutoPostBack="true" />
                                                                        </td>

                                                                    </tr>
                                                                           <tr  id="set_kakaun" runat="server" visible="false">
                                                                        <td width="140px" align="left"><span class="leftpadding ">Kod Akaun</td>
                                                                        <td width="10%" align="center">:</td>
                                                                        <td width="270px" align="left"> <asp:DropDownList ID="dd_akaun" runat="server" Width="100%" CssClass=" form-control select2 uppercase"></asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                       <tr>
                                                                        <td width="140px" align="left"><span class="leftpadding "></td>
                                                                        <td width="10%" align="center"></td>
                                                                        <td width="270px" align="left">  <asp:CheckBox ID="CheckBox1" runat="server" CssClass="mycheckbox" Text="Kunci Kira-Kira" />
                                                                        </td>

                                                                    </tr>
                                                       <tr>
                                                                        <td width="140px" align="left"><span class="leftpadding "></td>
                                                                        <td width="10%" align="center"></td>
                                                                        <td width="270px" align="left"> <asp:CheckBox ID="CheckBox2" runat="server" CssClass="mycheckbox" Text="Pendapatan & Perbelanjan" OnCheckedChanged="pnl_chnaged" AutoPostBack="true" />
                                                                        </td>

                                                                    </tr>
                                                        <tr runat="server" id="pnl_shw" visible="false">
                                                                        <td width="140px" align="left"><span class="leftpadding ">P&L Header</td>
                                                                        <td width="10%" align="center">:</td>
                                                                        <td width="270px" align="left"> <asp:DropDownList ID="pnl_item" runat="server" Width="100%" class="form-control select2 validate[optional] uppercase">
                                                                
                                                                </asp:DropDownList>
                                                                        </td>

                                                                    </tr>
                                                       <tr>
                                                                        <td width="140px" align="left"><span class="leftpadding "></td>
                                                                        <td width="10%" align="center"></td>
                                                                        <td width="270px" align="left"> <asp:CheckBox ID="CheckBox3" runat="server" CssClass="mycheckbox" Text="Aliran Tunai" OnCheckedChanged="aliran_chnaged" AutoPostBack="true" />
                                                                        </td>

                                                                    </tr>
                                                         <tr runat="server" id="aliran_shw" visible="false">
                                                                        <td width="140px" align="left"><span class="leftpadding ">Aliran Tunai Item</td>
                                                                        <td width="10%" align="center">:</td>
                                                                        <td width="270px" align="left"> <asp:DropDownList ID="ali_item" runat="server" Width="100%" class="form-control select2 validate[optional] uppercase">
                                                                
                                                                </asp:DropDownList>
                                                                        </td>

                                                                    </tr>
                                                       <tr>
                                                                        <td width="140px" align="left"><span class="leftpadding "></td>
                                                                        <td width="10%" align="center"></td>
                                                                        <td width="270px" align="left">   <asp:CheckBox ID="CheckBox4" runat="server" CssClass="mycheckbox" Text="Pembahagian" />
                                        
                                                                        </td>

                                                                    </tr>
                                                       <tr>
                                                                        <td width="140px" align="left"><span class="leftpadding "></td>
                                                                        <td width="10%" align="center"></td>
                                                                        <td width="270px" align="left"><asp:CheckBox ID="CheckBox5" runat="server" CssClass="mycheckbox" Text="COGS" />
                                                                        </td>

                                                                    </tr>
                                                         <tr id="rpt_shw1" runat="server" visible="false">
                                                                        <td width="140px" align="left"><span class="leftpadding "></td>
                                                                        <td width="10%" align="center"></td>
                                                                        <td width="270px" align="left"><asp:CheckBox ID="CheckBox7" runat="server" CssClass="mycheckbox" Text="CYP" />                                                                         
                                                                        </td>
                                                                    </tr>
                                                          <tr id="rpt_shw2" runat="server" visible="false">
                                                                        <td width="140px" align="left"><span class="leftpadding "></td>
                                                                        <td width="10%" align="center"></td>
                                                                        <td width="270px" align="left"><asp:CheckBox ID="CheckBox8" runat="server" CssClass="mycheckbox" Text="RE" />
                                                                        </td>

                                                                    </tr>
                                                       
                                                         <tr >
                                                                        <td width="140px" align="left"><span class="leftpadding ">Status</td>
                                                                        <td width="10%" align="center">:</td>
                                                                        <td width="270px" align="left"> <asp:DropDownList ID="dd_list_sts" runat="server" Width="100%" class="form-control select2 validate[optional] uppercase">
                                                                    <asp:ListItem Text="AKTIF" Value="A" />
                                                                    <asp:ListItem Text="TIDAK AKTIF" Value="T" />
                                                                </asp:DropDownList>
                                                                        </td>

                                                                    </tr>
                                                        <tr>
                                                                        <td width="140px" align="left"><span class="leftpadding ">Account Header</td>
                                                                        <td width="10%" align="center">:</td>
                                                                        <td width="270px" align="left"><asp:CheckBox ID="CheckBox6" runat="server" CssClass="mycheckbox" />
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

