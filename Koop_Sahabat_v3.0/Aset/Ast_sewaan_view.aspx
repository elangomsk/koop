<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Aset/Ast_sewaan_view.aspx.cs" Inherits="Ast_sewaan_view" %>

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
                        <h1>Senarai Maklumat Sewaan</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Pengurusan Aset</a></li>
                            <li class="active">Senarai Maklumat Sewaan</li>
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
                             <div class="box-body">&nbsp;</div>
                            <div class="box-body">
                                 <div class="row">

          <%-- <div class="col-md-3 box-body">
                                <div class="form-group">
                                    <div class="col-sm-12">
                                         <div class="input-group">
                                                <span class="input-group-addon" style="background-color:#0090d9; color:#fff;" ><i class="fa fa-search"></i></span>
                                        <asp:TextBox ID="txtSearch" class="form-control" runat="server" OnTextChanged="txtSearch_TextChanged" AutoPostBack="True" placeholder="MASUKKAN NILAI DI SINI"></asp:TextBox>
                                             </div>
                                    </div>
                                   
                                </div>
                            </div>--%>
                                    <div class="col-md-12 box-body">
                                <div class="form-group">
                                     <div class="col-sm-12" style="text-align:right;">
                                    <%--    <asp:Button ID="button4" runat="server" Text="Carian"  class="align-center btn btn-primary" UseSubmitBehavior="false" OnClick="btn_search_Click"></asp:Button>
                                         <asp:Button ID="Button3" runat="server" class="btn btn-danger" Text="Hapus" UseSubmitBehavior="false" OnClick="btn_hups_Click" OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;" />--%>
                                         <asp:Button runat="server" Text="+ Tambah" OnClick="Add_profile"  class="align-center btn btn-primary"></asp:Button>
                                    </div>
                                </div>
                            </div>
      </div> 
                                <div class="box-body">&nbsp;
                                    </div>
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
                                                                        <%--<asp:LinkButton runat="server" ID="lnkView" OnClick="lnkView_Click" Font-Bold Font-Underline>--%>
                                                                            <asp:Label ID="gd_5" runat="server" Text='<%# Eval("ren_comp_reg_no") %>'></asp:Label>
                                                                      <%--  </asp:LinkButton>--%>
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
                            <%--    </div>--%>
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

