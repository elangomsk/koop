<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_KEMESKINI_KRITE_view.aspx.cs" Inherits="HR_KEMESKINI_KRITE_view" %>

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
                        <h1>  PENILAIAN PRESTASI    </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>SUMBER_MANUSIA</a></li>
                            <li class="active"> PENILAIAN PRESTASI  </li>
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
                                     <div class="box-header with-border">
                            <h3 class="box-title"></h3>
                        </div>

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
                                       <%-- <asp:Button ID="button4" runat="server" Text="Carian"  class="align-center btn btn-primary" UseSubmitBehavior="false" OnClick="btn_search_Click"></asp:Button>--%>
                                         <asp:Button ID="Button3" runat="server" class="btn btn-danger" Text="Hapus" UseSubmitBehavior="false" OnClick="btn_hups_Click" />
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
                                    <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="Small" Width="100%" AllowPaging="true" PageSize="1000000">

                                        <Columns>

                                         <asp:TemplateField HeaderText="BIL">
                                                <ItemStyle Width="2%"></ItemStyle> 
                                               <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="2%"/>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="DARI">   
                                                <ItemStyle HorizontalAlign="Left" Width="2%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="ss_sdt" runat="server" Text='<%# Eval("dt1", "{0:dd/MM/yyyy}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="HINGGA">   
                                                <ItemStyle HorizontalAlign="Left" Width="2%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="ss_edt" runat="server" Text='<%# Eval("dt2", "{0:dd/MM/yyyy}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="JAWATAN">   
                                                <ItemStyle HorizontalAlign="Left" Width="7%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="ss_jaw" runat="server" Text='<%# Eval("hr_jaw_desc") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="UNIT">   
                                                <ItemStyle HorizontalAlign="Left" Width="7%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="ss_unit" runat="server" Text='<%# Eval("hr_unit_desc") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BAHAGIAN">   
                                                <ItemStyle HorizontalAlign="Left" Width="7%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="ss_bha" runat="server" Text='<%# Eval("cse_section_desc") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SUBJEK">   
                                                <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="ss_mp" runat="server" Text='<%# Eval("csb_subject_desc") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PEMBERAT (%)">   
                                                <ItemStyle HorizontalAlign="center" Width="5%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:TextBox ID="ss_pemberat" runat="server" CssClass="form-control validate[optional]" Text='<%# Eval("cap_weightage") %>' MaxLength="2"></asp:TextBox>
                                                 <asp:Label ID="Label1" runat="server" Visible="false" Text='<%# Eval("dt1") %>'></asp:Label>  
                                            <asp:Label ID="Label2" runat="server" Visible="false" Text='<%# Eval("dt2") %>'></asp:Label>  
                                            <asp:Label ID="Label3" runat="server" Visible="false" Text='<%# Eval("cap_post_cat_cd") %>'></asp:Label>  
                                            <asp:Label ID="Label4" runat="server" Visible="false" Text='<%# Eval("cap_unit_cd") %>'></asp:Label>  
                                            <asp:Label ID="Label5" runat="server" Visible="false" Text='<%# Eval("cap_section_cd") %>'></asp:Label>  
                                            <asp:Label ID="Label6" runat="server" Visible="false" Text='<%# Eval("cap_subject_cd") %>'></asp:Label>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                               <%-- <asp:TemplateField HeaderText="PILIH">   
                                                <ItemStyle HorizontalAlign="center" Width="5%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:CheckBox ID="s_pilih" runat="server" />
                                            </ItemTemplate>  
                                             
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="HAPUS">   
                                                <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:CheckBox ID="s_hapus" runat="server" />
                                                <asp:Label ID="ss_sqno" runat="server" Visible="false" Text='<%# Eval("cap_seq_no") %>'></asp:Label> 
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





