<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_PENGALAMAN_view.aspx.cs" Inherits="HR_PENGALAMAN_view" %>

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
                        <h1 id="h1_tag" runat="server"> Senarai Pengalaman Bekerja </h1>
                        <ol class="breadcrumb">
                            <li id="bb1_text" runat="server"><a href="#"><i class="fa fa-dashboard"></i>Sumber Manusia</a></li>
                            <li class="active" id="bb2_text" runat="server"> Senarai Pengalaman Bekerja  </li>
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
                                
                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                   <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="1000000">
                                                                                    <Columns>
                                                                                     <asp:TemplateField HeaderText="BIL" ItemStyle-HorizontalAlign="center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                                    ItemStyle-Width="150" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                                        <%--<asp:BoundField DataField="pos_start_dt" HeaderText="Tarikh Mula" />--%>
                                                                                         <asp:TemplateField HeaderText="No Kakitangan" ItemStyle-HorizontalAlign="Left">
                                                                                            <ItemTemplate>
                                                                                               <asp:LinkButton runat="server" ID="lnkView" OnClick="lnkView_Click" Font-Bold Font-Underline>
                                                                                                <asp:Label ID="lb_sno" ToolTip='<%# Eval("stf_ss") %>' runat="server" Text='<%# Eval("stf_staff_no") %>'></asp:Label>
                                                                                                <asp:Label ID="lb_sdt" runat="server" Text='<%# Eval("stf_staff_no") %>' Visible="false"></asp:Label>
                                                                                                   </asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                         <asp:BoundField DataField="stf_name" ItemStyle-HorizontalAlign="Left" HeaderText="Nama Kakitangan" />
                                                                                         <asp:BoundField DataField="stf_icno" ItemStyle-HorizontalAlign="center" HeaderText="Ic No" />
                                                                                        <asp:BoundField DataField="pos_start_dt" ItemStyle-HorizontalAlign="center" HeaderText="Tarikh Mula" />
                                                                                        <asp:BoundField DataField="stf_age" ItemStyle-HorizontalAlign="center" HeaderText="Umur" />
                                                                                        <asp:BoundField DataField="hr_jaw_desc" ItemStyle-HorizontalAlign="Left" HeaderText="Jawatan" />                                                                                        
                                                                                        <asp:BoundField DataField="job_sts1" ItemStyle-HorizontalAlign="center" HeaderText="Status Penjawatan" />
                                                                                        <%-- <asp:TemplateField HeaderText="HAPUS">
                                                                <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="RadioButton1" runat="server"/>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
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








