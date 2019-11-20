<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="HR_Ref_Jenis_KLM.aspx.cs" Inherits="SUMBER_MANUSIA_HR_Ref_Jenis_KLM" %>

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
                        <h1 id="h1_tag" runat="server">  Selenggara Jenis KLM </h1>
                        <ol class="breadcrumb">
                            <li id="bb1_text" runat="server"><a href="#"><i class="fa fa-dashboard"></i>SUMBER MANUSIA</a></li>
                            <li class="active" id="bb2_text" runat="server"> Selenggara Jenis KLM  </li>
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
                                   <%--   <div class="row" style="overflow:auto;">--%>
           <div class="col-md-12 box-body">
                                   <asp:GridView ID="gv_refdata" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="1000000" ShowFooter="true" GridLines="None" OnRowCommand="gv_refdata_RowCommand"
                                                        OnRowDeleting="gv_refdata_RowDeleting" OnRowUpdating="gv_refdata_RowUpdating"
                                                        OnRowCancelingEdit="gv_refdata_RowCancelingEdit" OnRowEditing="gv_refdata_RowEditing">
                                                        <Columns>
                                                          <%--  <asp:TemplateField HeaderText="BIL" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                        ItemStyle-Width="150" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="KOD" ItemStyle-Width="15%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEmpID" class="uppercase" Width="100%" runat="server" MaxLength="2" Text='<%#DataBinder.Eval(Container.DataItem, "typeklm_cd") %>'></asp:Label>
                                                                    <asp:TextBox ID="Id" Visible="false" Width="100%"  class="uppercase" MaxLength="2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Id") %>'></asp:TextBox>
                                                                    <asp:TextBox ID="TextBox1" Visible="false" Width="100%"  class="uppercase" MaxLength="2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "typeklm_desc") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lblEditName" class="form-control uppercase" Width="100%" MaxLength="2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "typeklm_cd") %>'></asp:TextBox>
                                                                    <asp:TextBox ID="Id" Visible="false" MaxLength="2" Width="100%" class="uppercase" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Id") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txtAddName" MaxLength="2" Width="100%" placeholder="sila masukkan nilai" runat="server" class=" form-control uppercase"></asp:TextBox>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="KLM">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblName" class="uppercase" Width="100%" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "typeklm_desc") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtEditcode" class="form-control uppercase" Width="100%" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "typeklm_desc") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txtAddcode" placeholder="sila masukkan nilai" Width="100%" class="form-control uppercase"
                                                                        runat="server"></asp:TextBox>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="WEIGHT">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblwgt" class="uppercase" Width="100%" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "typeklm_weight") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtEditwgt" class="form-control uppercase" Width="100%" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "typeklm_weight") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txtAddwgt" placeholder="sila masukkan nilai" Width="100%" class="form-control uppercase"
                                                                        runat="server"></asp:TextBox>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="STATUS">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="status" class="uppercase" runat="server" Width="100%" Text='<%# DataBinder.Eval(Container.DataItem, "Status").ToString() == "A" ? "Aktif" : "Tidak Aktif" %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="editddlStatus" SelectedValue='<%#Eval("Status") %>' style="width:100%;" runat="server" class="form-control select2 validate[optional]">
                                                                        <asp:ListItem Text="Aktif" Value="A"></asp:ListItem>
                                                                        <asp:ListItem Text="Tidak Aktif" Value="TA"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:DropDownList ID="ddlStatus" style="width:100%;" runat="server" class="form-control select2 validate[optional]">
                                                                        <asp:ListItem Text="Aktif" Value="A"></asp:ListItem>
                                                                        <asp:ListItem Text="Tidak Aktif" Value="TA"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="TINDAKAN" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imgbtnEdit" runat="server" CommandName="Edit" ImageUrl="../Login_assets/img/icon-edit.png"
                                                                        Height="15px" Width="15px" />
                                                                    &nbsp;
                                                                    <asp:ImageButton ID="imgbtnDelete" OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;" runat="server" CommandName="Delete"
                                                                        ImageUrl="../Login_assets/img/Delete.jpg" Height="15px" Width="15px" />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" ImageUrl="../Login_assets/img/icon-update1.jpg"
                                                                        Height="15px" Width="15px" />
                                                                    &nbsp;
                                                                    <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="../Login_assets/img/icon-Cancel.jpg"
                                                                        Height="15px" Width="15px" />
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:LinkButton ID="lbtnAdd" runat="server" CommandName="ADD" Text="Tambah Lagi"
                                                                        Width="100px"></asp:LinkButton>
                                                                </FooterTemplate>
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
                               <%-- </div>--%>
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


