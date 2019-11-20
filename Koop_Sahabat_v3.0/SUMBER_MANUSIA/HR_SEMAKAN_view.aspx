<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_SEMAKAN_view.aspx.cs" Inherits="HR_SEMAKAN_view" %>

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
                        <h1 > Senerai Pengesahan Cuti </h1>
                        <ol class="breadcrumb">
                            <li id="bb1_text" runat="server"><a href="#"><i class="fa fa-dashboard"></i>Sumber Manusia</a></li>
                            <li class="active" > Senerai Pengesahan Cuti   </li>
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
                                      <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="1000000" ShowFooter="false" GridLines="None" OnRowDataBound="GridView1_RowDataBound">
                                                    <Columns>
                                                    <asp:TemplateField HeaderText="BIL">  
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="2%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="No Kakitangan">
                                                            <ItemTemplate>
                                                                 <asp:LinkButton runat="server" ID="lnkView" OnClick="lnkView_Click" Font-Bold Font-Underline>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("stf_staff_no") %>' ></asp:Label>
                                                                     <asp:Label ID="lbl_id" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                                     <%--<asp:Label ID="lbl_sts_cd" runat="server" Text='<%# Eval("lap_approve_sts_cd") %>' Visible="false"></asp:Label>--%>
                                                                     </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="No Rujukan">
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label2_nr" runat="server" Text='<%# Eval("lap_ref_no") %>' CssClass="uppercase"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Nama Kakitangan">
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("stf_name") %>' CssClass="uppercase"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tarikh Mohon">
                                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("lap_application_dt","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Jenis Cuti">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("hr_jenis_desc") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Hari Cuti">
                                                             <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label61" runat="server" Text='<%# Bind("lap_leave_day") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Tarikh Mula">
                                                          <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label11" runat="server" Text='<%# Bind("lap_leave_start_dt","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Tarikh Sehingga">
                                                               <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label12" runat="server" Text='<%# Bind("lap_leave_end_dt","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="LEWAT MOHON (HARI)">
                                                         <ItemStyle HorizontalAlign="center" ForeColor="Red" Font-Bold></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label63" runat="server" Text='<%# Bind("lap_late_apply") %>' CssClass="uppercase"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Status Kelulusan">
                                                                <ItemStyle HorizontalAlign="center" Font-Bold></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label12_sts" runat="server" Text='<%# Bind("app_stscd") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%-- <asp:TemplateField HeaderText="Nama Penyelia">
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label12_name" runat="server" Text='<%# Bind("approve_name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                          <asp:TemplateField HeaderText="Status Pengesahan">
                                                                <ItemStyle HorizontalAlign="center" Font-Bold></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label12_stsp" runat="server" Text='<%# Bind("psts") %>'></asp:Label>
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






