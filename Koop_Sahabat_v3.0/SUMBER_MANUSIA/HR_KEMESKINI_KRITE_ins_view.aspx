<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_KEMESKINI_KRITE_ins_view.aspx.cs" Inherits="HR_KEMESKINI_KRITE_ins_view" %>

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
                        <h1 id="h1_tag" runat="server"> Senarai Jana Dokumen </h1>
                        <ol class="breadcrumb">
                            <li id="bb1_text" runat="server"><a href="#"><i class="fa fa-dashboard"></i>SUMBER_MANUSIA</a></li>
                            <li class="active" id="bb2_text" runat="server"> Senarai Jana Dokumen </li>
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
                                 
                                    <div class="col-md-12 box-body">
                                <div class="form-group">
                                     <div class="col-sm-12" style="text-align:right;">
                                       <%-- <asp:Button ID="button4" runat="server" Text="Carian"  class="align-center btn btn-primary" UseSubmitBehavior="false" OnClick="btn_search_Click"></asp:Button>--%>
                                         <asp:Button ID="Button3" runat="server" class="btn btn-danger" Text="Hapus" UseSubmitBehavior="false" OnClick="btn_hups_Click" />
                                         <asp:Button ID="Button1" runat="server" Text="+ Tambah" OnClick="Add_profile"  class="align-center btn btn-primary"></asp:Button>
                                    </div>
                                </div>
                            </div>
      </div> 
                                <div class="box-body">&nbsp;
                                    </div>
                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                    <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="1000000" ShowFooter="false" GridLines="None">

                                        <Columns>
                                         <asp:TemplateField HeaderText="BIL">
                                                <ItemStyle Width="2%"></ItemStyle> 
                                               <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="2%"/>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tempoh Penilaian Dari">   
                                                <ItemStyle HorizontalAlign="Left" Width="7%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:LinkButton runat="server" ID="lnkView" OnClick="lnkView_Click" Font-Bold Font-Underline>
                                                <asp:Label ID="ss_bha" runat="server" Text='<%# Eval("dt1") %>'></asp:Label>  
                                                <asp:Label ID="ss_seq_no" Visible="false" runat="server" Text='<%# Eval("cap_seq_no") %>'></asp:Label>  
                                                <asp:Label ID="ss_jaw_cd" Visible="false" runat="server" Text='<%# Eval("cap_post_cat_cd") %>'></asp:Label>  
                                                <asp:Label ID="ss_un_cd" Visible="false" runat="server" Text='<%# Eval("cap_unit_cd") %>'></asp:Label> 
                                                    </asp:LinkButton> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tempoh Penilaian Hingga">   
                                                <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="ss_mp" runat="server" Text='<%# Eval("dt2") %>'></asp:Label>                                                  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Jawatan">   
                                                <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="ss_jaw" runat="server" Text='<%# Eval("hr_jaw_desc") %>'></asp:Label>                                                  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit">   
                                                <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="ss_unit" runat="server" Text='<%# Eval("hr_unit_desc") %>'></asp:Label>                                                  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PEMBERAT (%)">   
                                                <ItemStyle HorizontalAlign="center" Width="5%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="ss_ppk" runat="server" Text='<%# Eval("percnt") %>'></asp:Label> 
                                               <%-- <asp:TextBox ID="ss_pemberat" runat="server" CssClass="form-control validate[optional]" MaxLength="2"></asp:TextBox>--%>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PILIH">   
                                                <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:CheckBox ID="s_pilih" runat="server" />
                                                <%--<asp:Label ID="ss_sqno" runat="server" Visible="false" Text='<%# Eval("cit_seq_no") %>'></asp:Label> --%>
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






