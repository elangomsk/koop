<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Kewengan/kw_diskaun_view.aspx.cs" Inherits="kw_diskaun_view" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <script>
         $(function () {


             $('#<%=gv_refdata.ClientID %>').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
                 "responsive": true,
                 "sPaginationType": "full_numbers",
                 "iDisplayLength": 15,
                 "aLengthMenu": [[15, 30, 50, 100], [15, 30, 50, 100]]
             });
         });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

   
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1><asp:Label ID="ps_lbl1" Visible="false" runat="server"></asp:Label> Diskaun</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i><asp:Label Visible="false" ID="ps_lbl2" runat="server"></asp:Label>Kewangan</a></li>
                            <li class="active"><asp:Label Visible="false" ID="ps_lbl3" runat="server"></asp:Label> Diskaun</li>
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
                                      <%--  <asp:Button ID="button4" runat="server" Text="Carian"  class="align-center btn btn-primary" UseSubmitBehavior="false" OnClick="btn_search_Click"></asp:Button>--%>
                                         <asp:Button ID="Button3" runat="server" class="btn btn-danger" Text="Hapus" UseSubmitBehavior="false" OnClick="btn_hups_Click" />
                                         <asp:Button ID="Button5" runat="server" Text="Tambah" OnClick="Add_profile"  class="align-center btn btn-primary"></asp:Button>
                                    </div>
                                </div>
                            </div>
      </div> 
                                <div class="box-body">&nbsp;
                                    </div>
                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                    <%--  <div class="row" >--%>
           <div class="col-md-12 box-body">
                                   <asp:GridView ID="gv_refdata" runat="server" class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="1000000">
                                                    <Columns>
                                                    <asp:TemplateField HeaderText="BIL">  
                                                         <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="NAMA Diskaun">
                                                         <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                  <asp:LinkButton runat="server" ID="lnkView" OnClick="lnkView_Click" Font-Underline Font-Bold>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("Ref_nama_diskaun") %>'></asp:Label>
                                                                        <asp:Label ID="lbl_id" Visible="false" runat="server" Text='<%# Bind("Id") %>' CssClass="uppercase"></asp:Label>
                                                                     </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="KOD Diskaun">
                                                             <ItemStyle HorizontalAlign="Left" Width="5%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label2_fl" runat="server" Text='<%# Eval("Ref_kod_diskaun") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="KADAR (%)" Visible="false">
                                                             <ItemStyle HorizontalAlign="center" Width="5%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_kadar" runat="server" Text='<%# Eval("Ref_kadar") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="NAMA AKAUN">
                                                             <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_akaunname" runat="server" Text='<%# Eval("nama_akaun") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="JENIS CUKAI" Visible="false">
                                                             <ItemStyle HorizontalAlign="center" Width="5%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_jeniscuki" runat="server" Text='<%# Eval("Ref_Jenis_cukai") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="JENIS OPERASI AKAUN">
                                                             <ItemStyle HorizontalAlign="center" Width="5%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_jenis" runat="server" Text='<%# Eval("jenis") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Deskripsi" Visible="false">
                                                              <ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_kakaun" runat="server" Text='<%# Eval("Ref_descripsi") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>                                                       
                                                         <asp:TemplateField HeaderText="Status" Visible="false">
                                                              <ItemStyle HorizontalAlign="Left" Width="5%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_sts" runat="server" Text='<%# Eval("sts") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Hapus" ItemStyle-Width="5%" ControlStyle-CssClass="panel-heading">
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="RadioButton1" runat="server" />
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
         <%-- </div>--%>
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

