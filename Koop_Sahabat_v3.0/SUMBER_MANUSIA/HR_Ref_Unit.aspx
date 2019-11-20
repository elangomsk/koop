<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_Ref_Unit.aspx.cs" Inherits="HR_Ref_Unit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
                        <h1>  Selenggara Maklumat Seksyen     </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>SUMBER MANUSIA</a></li>
                            <li class="active"> Selenggara Maklumat Seksyen  </li>
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
                                    <asp:GridView ID="gv_refdata" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="true" GridLines="None" DataKeyNames="Id"

                    onrowcommand="gv_refdata_RowCommand"

                    onrowdeleting="gv_refdata_RowDeleting"

                    onrowupdating="gv_refdata_RowUpdating"

                    onrowcancelingedit="gv_refdata_RowCancelingEdit"

                    onrowediting="gv_refdata_RowEditing"
                    
                    OnPageIndexChanging="gv_refdata_PageIndexChanging"
                    
                    OnRowDataBound="gv_refdata_RowDataBound"
                    >

                <Columns>           

                    <asp:TemplateField HeaderText="BIL">

                        <ItemTemplate>

                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="15%"/>

                        </ItemTemplate>

                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="JABATAN">

                        <ItemTemplate>

                            <asp:Label ID="lblEmpID" class="uppercase" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "hr_jaba_desc") %>'></asp:Label>
                            <asp:TextBox ID="Id" Visible="false" class="uppercase" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Id") %>'></asp:TextBox>
                            <asp:TextBox ID="TextBox1" Visible="false" class="uppercase" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "hr_unit_desc") %>'></asp:TextBox>
                        </ItemTemplate>

                        <EditItemTemplate>           

                            <asp:DropDownList ID="lbleditwilayah" runat="server" style="width:100%;" class="form-control uppercase"></asp:DropDownList>
                            <asp:TextBox ID="Id" Visible="false" class="uppercase" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Id") %>'></asp:TextBox>
                        </EditItemTemplate>

                        <FooterTemplate>

                            <asp:DropDownList ID="lbladdwilayah" runat="server" style="width:100%;" class="form-control uppercase"></asp:DropDownList>

                        </FooterTemplate>

                    </asp:TemplateField>


                        <asp:TemplateField HeaderText="KOD Seksyen" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">

                        <ItemTemplate>

                            <asp:Label ID="lblName" class="uppercase" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "hr_unit_Code") %>'></asp:Label>

                        </ItemTemplate>

                        <EditItemTemplate>           

                            <asp:TextBox ID="txtEditcode" class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase" style="width:100%;" MaxLength="4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "hr_unit_Code") %>'></asp:TextBox>           

                        </EditItemTemplate>

                        <FooterTemplate>

                            <asp:TextBox ID="txtAddcode" placeholder="sila masukkan nilai" style="width:100%;" class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase" MaxLength="4" runat="server" ></asp:TextBox>

                        </FooterTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="NAMA Seksyen">

                        <ItemTemplate>

                            <asp:Label ID="lblcawangan" class="uppercase" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "hr_unit_desc") %>'></asp:Label>

                        </ItemTemplate>

                        <EditItemTemplate>           

                            <asp:TextBox ID="txtEditCawangan" class="form-control uppercase" runat="server" style="width:100%;" MaxLength="100" Text='<%#DataBinder.Eval(Container.DataItem, "hr_unit_desc") %>'></asp:TextBox>           

                        </EditItemTemplate>

                        <FooterTemplate>

                            <asp:TextBox ID="txtAddCawangan" placeholder="sila masukkan nilai" style="width:100%;" class="form-control uppercase" MaxLength="100" runat="server" ></asp:TextBox>

                        </FooterTemplate>

                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="STATUS">

                        <ItemTemplate>

                            <asp:Label ID="status" class="uppercase" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Status").ToString() == "A" ? "Aktif" : "Tidak Aktif" %>'></asp:Label>
                        </ItemTemplate>

                        <EditItemTemplate>           
                            <asp:DropDownList ID="editddlStatus" style="width:100%;" SelectedValue='<%#Eval("Status") %>' runat="server" class="form-control uppercase">
                            <asp:ListItem Text="Aktif" Value="A"></asp:ListItem>
                            <asp:ListItem Text="Tidak Aktif" Value="TA"></asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>

                        <FooterTemplate>

                            <asp:DropDownList ID="ddlStatus" runat="server" style="width:100%;" class="form-control uppercase">
                            <asp:ListItem Text="Aktif" Value="A"></asp:ListItem>
                            <asp:ListItem Text="Tidak Aktif" Value="TA"></asp:ListItem>
                            </asp:DropDownList>

                        </FooterTemplate>

                    </asp:TemplateField>
 
                    <asp:TemplateField HeaderText="TINDAKAN" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center">

                        <ItemTemplate>

                           <asp:ImageButton ID="imgbtnEdit" runat="server" CommandName="Edit" ImageUrl="../Login_assets/img/icon-edit.png" Height="22px" Width="22px"/>
                           &nbsp;
                           <asp:ImageButton ID="imgbtnDelete" OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;" runat="server" CommandName="Delete" ImageUrl="../Login_assets/img/Delete.jpg" Height="22px" Width="22px"/>

                        </ItemTemplate>

                        <EditItemTemplate>

                           <asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" ImageUrl="../Login_assets/img/icon-update1.jpg" Height="22px" Width="22px"/>

                           <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="../Login_assets/img/icon-Cancel.jpg" Height="22px" Width="22px"/>

                        </EditItemTemplate>

                        <FooterTemplate>

                           <asp:LinkButton ID="lbtnAdd" runat="server" CommandName="ADD" Text="Tambah Lagi" Width="100px"></asp:LinkButton>

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
