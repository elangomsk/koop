<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Aset/Ast_maklumat_rujukan.aspx.cs" Inherits="Ast_maklumat_rujukan" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
      
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server" ScriptMode="Release">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
        
                <div class="content-wrapper" style="height: 895px;">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>Selenggara Maklumat Rujukan</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Pengurusan Aset</a></li>
                            <li class="active">Selenggara Maklumat Rujukan</li>
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
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jadual Rujukan <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                          <asp:DropDownList ID="sel_rt" CssClass=" form-control uppercase" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dd_SelectedIndexChanged">
                                                                <asp:ListItem Value="">--- PILIH ---</asp:ListItem>
                                                                <asp:ListItem Value="01">Aset Kategori</asp:ListItem>
                                                                <asp:ListItem Value="02">Pengesahan Status</asp:ListItem>
                                                                <asp:ListItem Value="03">Status Kelulusan</asp:ListItem>
                                                                <asp:ListItem Value="04">Jenama</asp:ListItem>
                                                                <asp:ListItem Value="05">Aset Model</asp:ListItem>
                                                                <asp:ListItem Value="06">Bahan Bakar</asp:ListItem>
                                                                <asp:ListItem Value="07">Jenis Kenderaan</asp:ListItem>
                                                                <asp:ListItem Value="08">Aset Perolehan</asp:ListItem>
                                                                <asp:ListItem Value="09">Penggunaan Tanah</asp:ListItem>
                                                                <asp:ListItem Value="10">Jenis Pegangan</asp:ListItem>
                                                                <asp:ListItem Value="11">Jenis Milikan</asp:ListItem>
                                                                <asp:ListItem Value="12">status Pemeriksaan</asp:ListItem>
                                                                <asp:ListItem Value="13">Justifikasi</asp:ListItem>
                                                                <asp:ListItem Enabled="false" Value="14">Status Aduan</asp:ListItem>
                                                                <asp:ListItem Value="15">Kaedah Pelupusan</asp:ListItem>
                                                                <asp:ListItem Value="16">Jenis Geran</asp:ListItem>
                                                            </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                                <div class="box-body">&nbsp;
                                    </div>
                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                   <%--   <div class="row" style="overflow:auto;">--%>
           <div class="col-md-12 box-body">
                                   <asp:GridView ID="gv_refdata" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="1000000" ShowFooter="true" GridLines="None" OnRowCommand="gv_refdata_RowCommand"
                                                        OnRowDeleting="gv_refdata_RowDeleting" OnRowUpdating="gv_refdata_RowUpdating"
                                                        OnRowCancelingEdit="gv_refdata_RowCancelingEdit" OnRowEditing="gv_refdata_RowEditing">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="BIL" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                        ItemStyle-Width="150" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="KOD">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEmpID" class="uppercase" Width="100%" runat="server" MaxLength="2" Text='<%#DataBinder.Eval(Container.DataItem, "c1") %>'></asp:Label>
                                                                    <asp:TextBox ID="Id" Visible="false" Width="100%"  class="uppercase" MaxLength="2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "c4") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lblEditName" class="form-control uppercase" Width="100%" MaxLength="2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "c1") %>'></asp:TextBox>
                                                                    <asp:TextBox ID="Id" Visible="false" MaxLength="2" Width="100%" class="uppercase" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "c4") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txtAddName" MaxLength="2" Width="100%" placeholder="sila masukkan nilai" runat="server" class=" form-control uppercase"></asp:TextBox>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="KATEGORI">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblName" class="uppercase" Width="100%" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "c2") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtEditcode" class="form-control uppercase" Width="100%" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "c2") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txtAddcode" placeholder="sila masukkan nilai" Width="100%" class="form-control uppercase"
                                                                        runat="server"></asp:TextBox>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="STATUS">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="status" class="uppercase" runat="server" Width="100%" Text='<%# DataBinder.Eval(Container.DataItem, "c3").ToString() == "A" ? "Aktif" : "Tidak Aktif" %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="editddlStatus" SelectedValue='<%#Eval("c3") %>' style="width:100%;" runat="server" class="form-control select2 validate[optional]">
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
                                                                    <asp:ImageButton ID="imgbtnDelete" OnClientClick="Confirm()" runat="server" CommandName="Delete"
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

