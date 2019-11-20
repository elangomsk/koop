<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_STAFF_KEYIN_view.aspx.cs" Inherits="HR_STAFF_KEYIN_view" %>


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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

  
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1 id="h1_tag" runat="server"> Senarai Penilaian Prestasi Kakitangan </h1>
                        <ol class="breadcrumb">
                            <li id="bb1_text" runat="server"><a href="#"><i class="fa fa-dashboard"></i>Sumber Manusia</a></li>
                            <li class="active" id="bb2_text" runat="server"> Senarai Penilaian Prestasi Kakitangan </li>
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
                                <%-- <div class="row">
                                   
                                    <div class="col-md-12 box-body">
                                <div class="form-group">
                                     <div class="col-sm-12" style="text-align:right;">
                                   
                                         <asp:Button id="Button1" runat="server" Text="+ Tambah" OnClick="Add_profile"  class="align-center btn btn-primary"></asp:Button>
                                    </div>
                                </div>
                            </div>
      </div> 
                                <div class="box-body">&nbsp;
                                    </div>--%>
                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                    <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="10000000" ShowFooter="false" GridLines="None" OnRowDataBound="gvEmp_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="BIL">
                                                                <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                    <ItemTemplate>
                                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="150"/>
                                                                        <%--<asp:RadioButton ID="RadioButton1" runat="server" onclick = "RadioCheck(this);" />--%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="KATEGORI JAWATAN">
                                                                <ItemStyle HorizontalAlign="Left" Width="7%"></ItemStyle>
                                                                    <ItemTemplate>
                                                                    <%--<asp:LinkButton runat="server" ID="lnkView" OnClick="lnkView_Click">--%>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("hr_kate_desc") %>'></asp:Label>
                                                                        <asp:Label ID="Label2" runat="server" Visible="false" Text='<%# Eval("cse_sec_jawatan") %>'></asp:Label>
                                                                        <asp:Label ID="Label3" runat="server" Visible="false" Text='<%# Eval("sap_year") %>'></asp:Label>
                                                                        <%--</asp:LinkButton>--%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="BAGI TAHUN">
                                                                <ItemStyle HorizontalAlign="center" Width="10%" Font-Bold="true" Font-Underline="true"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" ID="lnkView2" OnClick="lnkView_Click2"><%# Eval("sap_year") %></asp:LinkButton>
                                                                        
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="STATUS" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("sts") %>' CssClass="uppercase"></asp:Label>
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
            </ContentTemplate>
    </asp:UpdatePanel>
        
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
   
</asp:Content>





