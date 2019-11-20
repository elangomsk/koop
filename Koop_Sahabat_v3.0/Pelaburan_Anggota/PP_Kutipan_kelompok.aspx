<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Pelaburan_Anggota/PP_Kutipan_kelompok.aspx.cs" Inherits="PP_Kutipan_kelompok" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
 <script>
     $(function () {

         $('#<%=GridView1.ClientID %>').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
             "responsive": true,
             "sPaginationType": "full_numbers",
             "iDisplayLength": 15,
             "aLengthMenu": [[15, 30, 50, 100], [15, 30, 50, 100]]
         });
     });
</script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server" ScriptMode="Release">
    
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

  
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" style="height: 895px;">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>Kutipan Berkelompok</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Pelaburan Anggota</a></li>
                            <li class="active">Kutipan Berkelompok</li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
      <%--  <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>--%>
            <div class="row">
               <!-- /.col -->
               
                <div class="col-md-12">

                    <div class="box">
                      
                        <div class="box-header">
                             <div class="box-body">&nbsp;</div>
                            <div class="box-body">
                                <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama Fail <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                          <asp:FileUpload ID="FileUpload1" runat="server" />
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                 <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                             <asp:Button ID="Button4" runat="server" class="btn btn-warning" Text="Papar" OnClick="btnok_Click"/>
                            <asp:Button ID="Button2" runat="server" class="btn btn-danger" Text="Muatnaik" OnClick="update_click"/>                                                        
                            <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" OnClick="rst_Click" usesubmitbehavior="false"/>                                                        
                            <asp:Button ID="Button3" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" OnClick="clk_bak" />
                           <asp:Button ID="Button5" runat="server" class="btn btn-danger"  Text="Jana Jurnal Akaun" onclick="Button5_Click"  />
                                 
                            </div>
                           </div>
                               </div>
                                <div class="box-header with-border" id="shw_htxt" runat="server" visible="false">
                            <h3 class="box-title">Senarai Pembayar</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                 <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="50" ShowFooter="false" GridLines="None">
                                     <PagerStyle CssClass="pager" />
                                        <Columns>
                                         <asp:TemplateField HeaderText="BIL">  
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="150"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NO RUJUKAN">  
                                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                                            <ItemTemplate>  
                                                 <asp:Label ID="Label2" runat="server"  Text='<%# Eval("no_rujukan") %>'></asp:Label>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NAMA PEMBAYAR">  
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle> 
                                            <ItemTemplate>  
                                                <asp:Label ID="Label3" runat="server"  Text='<%# Eval("nama_p") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="KOD TRANSAKSI">  
                                                <ItemStyle HorizontalAlign="center"></ItemStyle> 
                                            <ItemTemplate>  
                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("kod_t") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="RUJUKAN NO TRANSAKSI">   
                                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("rujukan_t") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TARIKH BAYARAN">  
                                                <ItemStyle HorizontalAlign="center"></ItemStyle> 
                                            <ItemTemplate>  
                                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("tarikh_b","{0:dd/MM/yyyy}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="AMAUN (RM)">  
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle> 
                                            <ItemTemplate>  
                                                <asp:Label ID="amt" runat="server" Text='<%# Eval("amaun") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                        </Columns>
                                       <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />                                                       
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
               </div>
          </div>
                            <%--    </div>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.col -->
            </div>
            <!-- /.row -->
            <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
        
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
   
</asp:Content>

