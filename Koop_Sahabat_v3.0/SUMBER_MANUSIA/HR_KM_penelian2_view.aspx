<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_KM_penelian2_view.aspx.cs" Inherits="HR_KM_penelian2_view" %>

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
                        <h1 id="h1_tag" runat="server">Senarai Penilaian Prestasi </h1>
                        <ol class="breadcrumb">
                            <li id="bb1_text" runat="server"><a href="#"><i class="fa fa-dashboard"></i>Sumber Manusia</a></li>
                            <li class="active" id="bb2_text" runat="server">Senarai Penilaian Prestasi </li>
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
                          <div class="box-body">&nbsp;</div>
                         <div class="row">
                             <div class="col-md-12">
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl2_text" runat="server">   Tahun</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txt_tahun" runat="server" ReadOnly="true" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                
                          
                                 </div>
                                </div>
                        <div class="box-header">
                             <div class="box-body">&nbsp;</div>
                            <div class="box-body">
                                
                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                   <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="Small" Width="100%" AllowPaging="true" PageSize="10000000">
                                        <Columns>

                                         <asp:TemplateField HeaderText="BIL">
                                                <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                               <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="2%"/>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NO KAKITANGAN">   
                                                <ItemStyle HorizontalAlign="Left" Width="7%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lbl_bha" runat="server" Text='<%# Eval("stf_staff_no") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NAMA KAKITANGAN">   
                                                <ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lbl_sub" runat="server" Text='<%# Eval("stf_name") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="IC NO">   
                                                <ItemStyle HorizontalAlign="center" Width="5%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="jabatan" runat="server" Text='<%# Eval("stf_icno") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="JAWATAN">   
                                                <ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="jawatan" runat="server" Text='<%# Eval("hr_jaw_desc") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="PENILAIAN">   
                                                <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:LinkButton runat="server" ID="lnkView1" OnClick="lnkView_Click1"><i class='fa fa-edit'></i></asp:LinkButton>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="CETAK">   
                                                <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:LinkButton runat="server" ID="lnkView2" OnClick="lnkView_Click2"><i class='fa fa-edit'></i></asp:LinkButton>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                        </Columns>
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






