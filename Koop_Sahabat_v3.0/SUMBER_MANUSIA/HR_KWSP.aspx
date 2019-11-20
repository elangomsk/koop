<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_KWSP.aspx.cs" Inherits="HR_KWSP" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

     
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1 id="h1_tag" runat="server">   Jana Maklumat Potongan </h1>
                        <ol class="breadcrumb">
                            <li id="bb1_text" runat="server"><a href="#"><i class="fa fa-dashboard"></i>Sumber Manusia</a></li>
                            <li class="active" id="bb2_text" runat="server"> Jana Maklumat Potongan   </li>
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
                            <h3 class="box-title" id="h3_tag" runat="server"> Jana Laporan </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl1_text" runat="server"> Potongan Bagi Bulan</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"
                                                                    ID="DD_bulancaruman">
                                                                    <%--onselectedindexchanged="dd_kat_SelectedIndexChanged">--%>
                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                  
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl2_text" runat="server">  Tahun </label>
                                    <div class="col-sm-8">
                                         <%--<asp:TextBox ID="txt_tahun" runat="server" CssClass="form-control validate[optional,custom[number]] uppercase"></asp:TextBox>--%>
                                                                <asp:DropDownList style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"
                                                                    ID="txt_tahun">
                                                                    <%--onselectedindexchanged="dd_kat_SelectedIndexChanged">--%>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl3_text" runat="server">  Nama Syarikat / Organisasi</label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"
                                                                    ID="DropDownList1" AutoPostBack="true"  OnSelectedIndexChanged="sel_orgbind">
                                                                    
                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl4_text" runat="server"> Perniagaan</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="dd_org_pen" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" AutoPostBack="true" OnSelectedIndexChanged="sel_orgjaba">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl5_text" runat="server"> Jabatan </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="dd_jabatan" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl6_text" runat="server"> Nama Pegawai </label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"
                                                                    ID="DDL_NAMAPEGAWAI">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl7_text" runat="server">  Jenis Laporan</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddl_reporttye" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"
                                                                     AutoPostBack="true" onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                                                                    <%--onselectedindexchanged="DropDownList1_SelectedIndexChanged"--%>
                                                                    <asp:ListItem Value="00">--- PILIH ---</asp:ListItem>
                                                                    <asp:ListItem Value="01">KWSP</asp:ListItem>
                                                                    <asp:ListItem Value="02">PERKESO</asp:ListItem>
                                                                    <asp:ListItem Value="03">LHDN</asp:ListItem>
                                                                    <asp:ListItem Value="04">EA</asp:ListItem>
                                                                    <asp:ListItem Value="05">SIP</asp:ListItem>
                                                                    <asp:ListItem Value="06">Lain-Lain Potongan</asp:ListItem>
                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body" id="shw_jp" runat="server" visible="false">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="Label1" runat="server">  Jenis Potongan</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="sel_frmt" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">                                                                    
                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                            <%--  <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="Label1" runat="server">  Report Format</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="sel_frmt" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">                                                                    
                                                                    <asp:ListItem Value="01">PDF</asp:ListItem>
                                                                    <asp:ListItem Value="02">WORD</asp:ListItem>
                                                                    <asp:ListItem Value="03">EXCEL</asp:ListItem>
                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 
                                 </div>
                                </div>--%>

                            <div id="borangA" runat="server" visible="false">
                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl8_text" runat="server"> Jenis Caruman </label>
                                    <div class="col-sm-8">
                                        <asp:RadioButton ID="RB_JenisCaruman1" runat="server" GroupName="JenisCaruman"  Text=" Caruman Bulanan"/> <br />
                                                        <asp:RadioButton ID="RB_JenisCaruman2" runat="server" GroupName="JenisCaruman" Text=" Caruman Gaji Terkurang Bayar" />
                                        
                                    </div>
                                </div>
                            </div>
                                 
                                 </div>
                                </div>

                                </div>

                            <div id="EABUTTON" runat="server" visible="false">
                                <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Jenis Caruman </label>
                                    <div class="col-sm-8">
                                        <asp:Button ID="Button1" runat="server" class="btn btn-danger" Text="JANA" UseSubmitBehavior="false" OnClick="Button5_Click" />
                                        
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
                                 <asp:Button ID="Button4" runat="server" class="btn btn-warning" Text="Cetak" OnClick="Button4_Click" />
                                                                <asp:Button ID="Button2" runat="server" class="btn btn-default" Text="Set Semula" OnClick="clk_rset" />
                            </div>
                           </div>
                               </div>
                            <br />
                 <div class="dataTables_wrapper form-inline dt-bootstrap" id="shw_EA" visible="false" runat="server" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging" >
                    <PagerStyle CssClass="pager" />
                                        <Columns>

                                         <asp:TemplateField HeaderText="BIL" ItemStyle-HorizontalAlign="Center">  
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="150"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nama">  
                                                <ItemStyle HorizontalAlign="Left" Width="25%"></ItemStyle>
                                            <ItemTemplate>  
                                                 <asp:Label ID="Label2" runat="server"  Text='<%# Eval("s1") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No KP Baru"> 
                                                <ItemStyle HorizontalAlign="Center" Width="25%"></ItemStyle>  
                                            <ItemTemplate>  
                                                <asp:Label ID="Label3" runat="server"  Text='<%# Eval("s3") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No KWSP">  
                                                <ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle> 
                                            <ItemTemplate>  
                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("s4") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Gaji Kasar (RM)">   
                                                <ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("s5","{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PCB (RM)">   
                                                <ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="Label51" runat="server" Text='<%# Eval("s6","{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CB38 (RM)">   
                                                <ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="Label52" runat="server" Text='<%# Eval("s7","{0:n}") %>'></asp:Label>  
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
                              <div class="dataTables_wrapper form-inline dt-bootstrap" id="shw_jpotongan" visible="false" runat="server" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                <asp:GridView ID="GridView2" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging1" >
                    <PagerStyle CssClass="pager" />
                                        <Columns>

                                         <asp:TemplateField HeaderText="BIL" ItemStyle-HorizontalAlign="Center">  
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="150"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No. Kakitangan">  
                                                <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                 <asp:Label ID="Label2" runat="server"  Text='<%# Eval("v1") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nama"> 
                                                <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>  
                                            <ItemTemplate>  
                                                <asp:Label ID="Label3" runat="server"  Text='<%# Eval("v2") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No. Kad Pengenalan">  
                                                <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle> 
                                            <ItemTemplate>  
                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("v3") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Jenis Potongan">  
                                                <ItemStyle HorizontalAlign="Left" Width="25%"></ItemStyle> 
                                            <ItemTemplate>  
                                                <asp:Label ID="Label4_1" runat="server" Text='<%# Eval("v6") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No Rujukan">   
                                                <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("v4") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Jumlah (RM)">   
                                                <ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="Label51" runat="server" Text='<%# Eval("v5","{0:n}") %>'></asp:Label>  
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
                          
                              <div class="box-header with-border" id="show_htxt" runat="server" visible="false">
                            <h3 class="box-title">Maklumat Dari Hingga</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                             <div class="col-md-12 box-body">
               <div class="col-md-1 box-body"> &nbsp; </div>
                <div class="col-md-10 box-body" style="display:none;">
                                 <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="600">
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
               <asp:PostBackTrigger ControlID="Button4"  />               
           </Triggers>
    </asp:UpdatePanel>
        
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
   
</asp:Content>




