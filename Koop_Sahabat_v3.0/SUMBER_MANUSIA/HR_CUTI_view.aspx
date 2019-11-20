<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_CUTI_view.aspx.cs" Inherits="HR_CUTI_view" %>

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
                        <h1 id="h1_tag" runat="server">   Senarai Kalendar Cuti </h1>
                        <ol class="breadcrumb">
                            <li id="bb1_text" runat="server"><a href="#"><i class="fa fa-dashboard"></i>Sumber Manusia</a></li>
                            <li class="active" id="bb2_text" runat="server">  Senarai Kalendar Cuti  </li>
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
                                       <%-- <asp:Button ID="button4" runat="server" Text="Carian"  class="align-center btn btn-primary" UseSubmitBehavior="false" OnClick="btn_search_Click"></asp:Button>--%>
                                         <%--<asp:Button ID="Button3" runat="server" class="btn btn-danger" Text="Hapus" UseSubmitBehavior="false" OnClick="btn_hups_Click" />--%>
                                         <asp:Button runat="server" ID="Button1" Text="+ Tambah" OnClick="Add_profile"  class="align-center btn btn-primary"></asp:Button>
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
                                                                        <asp:TemplateField HeaderText="BIL" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                                    ItemStyle-Width="150" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                      <%--  <asp:TemplateField HeaderText="genid" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                                                            <ItemStyle HorizontalAlign="Left" Width="25%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label21" runat="server" Text='<%# Eval("hol_gen_id") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>--%>
                                                                        <asp:TemplateField HeaderText="ORGANISASI">
                                                                            <ItemStyle HorizontalAlign="Left" Width="25%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("org_name") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="NEGERI">
                                                                            <ItemStyle HorizontalAlign="Left" Width="25%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("hr_negeri_desc") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="JENIS CUTI">
                                                                            <ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("hr_jenis_desc") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="TARIKH CUTI">
                                                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("hol_dt") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="CATATAN">
                                                                            <ItemStyle HorizontalAlign="Left" Width="25%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label51" runat="server" Text='<%# Eval("hol_remark") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                      <%--  <asp:TemplateField HeaderText="BATAL">
                                                                            <ItemStyle HorizontalAlign="center" Width="5%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkStatus" runat="server" />
                                                                                <asp:Label ID="oid" runat="server" Visible="false" Text='<%# Eval("hol_org_id") %>'></asp:Label>
                                                                                <asp:Label ID="hid" runat="server" Visible="false" Text='<%# Eval("hol_holiday_cd") %>'></asp:Label>
                                                                                <asp:Label ID="sid" runat="server" Visible="false" Text='<%# Eval("hol_state_cd") %>'></asp:Label>
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








