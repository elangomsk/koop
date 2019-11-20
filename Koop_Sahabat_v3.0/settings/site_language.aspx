<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../settings/site_language.aspx.cs" Inherits="site_language" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                <span style="border-width: 0px; position: fixed; font-weight: bold; padding: 50px; background-color: #FFFFFF; font-size: 16px; left: 40%; top: 40%;">Sila Tunggu. Rekod
                    Sedang Diproses ...</span>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

   
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" style="height: 895px;">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>Langauage</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Kewangan</a></li>
                            <li class="active">Langauage</li>
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
                                        <div class="col-sm-6">
                                           <div class="input-group">
                                                <span class="input-group-addon" style="background-color:#84BD00; color:#fff;" ><i class="fa fa-search"></i></span>
                                        <asp:TextBox ID="txtSearch" class="form-control" runat="server" OnTextChanged="txtSearch_TextChanged" AutoPostBack="True" placeholder="MASUKKAN NILAI DI SINI"></asp:TextBox>
                                             </div>
                                            </div>
                                          <div class="col-sm-2">
                                         <asp:Button ID="button4" runat="server" Text="Carian"  class="align-center btn btn-primary" UseSubmitBehavior="false" OnClick="btn_search_Click"></asp:Button>
                                              </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                <div class="dataTables_wrapper form-inline dt-bootstrap" >
                                      <div class="row" style="overflow:auto;">
           <div class="col-md-12 box-body">
                                   <asp:GridView ID="gv_refdata" runat="server"  DataKeyNames="Id" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="20" ShowFooter="true" GridLines="None"
                                        OnRowCommand="gv_refdata_RowCommand"
                                        OnRowDeleting="gv_refdata_RowDeleting"
                                        OnRowUpdating="gv_refdata_RowUpdating"
                                        OnRowCancelingEdit="gv_refdata_RowCancelingEdit"
                                        OnRowEditing="gv_refdata_RowEditing"
                                        OnPageIndexChanging="gv_refdata_PageIndexChanging"
                                      >

                                        <Columns>

                                            <asp:TemplateField HeaderText="BIL">

                                                <ItemTemplate>

                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="150" />

                                                </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Default Word">

                                                <ItemTemplate>

                                                    <asp:Label ID="lblEmpID" class="uppercase" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "default") %>'></asp:Label>
                                                    <asp:TextBox ID="Id" Visible="false" class="uppercase" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Id") %>'></asp:TextBox>
                                                </ItemTemplate>

                                                <EditItemTemplate>
                                                     <asp:label ID="lbleditwilayah" class="uppercase" runat="server" Width="100%" MaxLength="100" Text='<%#DataBinder.Eval(Container.DataItem, "default") %>'></asp:label>
                                                    <asp:TextBox ID="Id" Visible="false" class="uppercase" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Id") %>'></asp:TextBox>
                                                </EditItemTemplate>

                                                <FooterTemplate>

                                                    <asp:TextBox ID="lbladdwilayah" placeholder="sila masukkan nilai" class="form-control uppercase" Width="100%" MaxLength="100" runat="server"></asp:TextBox>
                                                </FooterTemplate>

                                            </asp:TemplateField>




                                            <asp:TemplateField HeaderText="English">

                                                <ItemTemplate>

                                                    <asp:Label ID="lblcawangan" class="uppercase" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "eng") %>'></asp:Label>

                                                </ItemTemplate>

                                                <EditItemTemplate>

                                                    <asp:TextBox ID="txtEditCawangan" class="form-control uppercase" runat="server" Width="100%" MaxLength="100" Text='<%#DataBinder.Eval(Container.DataItem, "eng") %>'></asp:TextBox>

                                                </EditItemTemplate>

                                                <FooterTemplate>

                                                    <asp:TextBox ID="txtAddCawangan" placeholder="sila masukkan nilai" class="form-control uppercase" Width="100%" MaxLength="100" runat="server"></asp:TextBox>

                                                </FooterTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Malay">

                                                <ItemTemplate>

                                                    <asp:Label ID="lblName" class="uppercase" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "mal") %>'></asp:Label>

                                                </ItemTemplate>

                                                <EditItemTemplate>

                                                    <asp:TextBox ID="txtEditcode" class="form-control validate[optional] uppercase" MaxLength="100" Width="100%" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "mal") %>'></asp:TextBox>

                                                </EditItemTemplate>

                                                <FooterTemplate>

                                                    <asp:TextBox ID="txtAddcode" placeholder="sila masukkan nilai" class="form-control validate[optional] uppercase" Width="100%" MaxLength="100" runat="server"></asp:TextBox>

                                                </FooterTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="STATUS" Visible="false">

                                                <ItemTemplate>

                                                    <asp:Label ID="status" class="uppercase" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Status").ToString() == "A" ? "Aktif" : "Tidak Aktif" %>'></asp:Label>
                                                </ItemTemplate>

                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="editddlStatus" SelectedValue='<%#Eval("Status") %>' runat="server" class="form-control uppercase">
                                                        <asp:ListItem Text="Aktif" Value="A"></asp:ListItem>
                                                        <asp:ListItem Text="Tidak Aktif" Value="TA"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </EditItemTemplate>

                                                <FooterTemplate>

                                                    <asp:DropDownList ID="ddlStatus" runat="server" class="form-control uppercase">
                                                        <asp:ListItem Text="Aktif" Value="A"></asp:ListItem>
                                                        <asp:ListItem Text="Tidak Aktif" Value="TA"></asp:ListItem>
                                                    </asp:DropDownList>

                                                </FooterTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="TINDAKAN" ItemStyle-HorizontalAlign="Center">

                                                <ItemTemplate>

                                                    <%--<asp:ImageButton ID="imgbtnEdit" runat="server" CommandName="Edit" ImageUrl="assets/image/icon-edit.png" onclick="EditStudent(this);" Height="22px" Width="22px"/>--%>
                                                    <%--<img id="imgbtnEdit" style="cursor: pointer; height: 20px; width: 20px;" alt="Edit" src="img/icon-edit.png" onclick="EditStudent(this);" />--%>
                                                    <asp:LinkButton ID="imgbtnEdit" runat="server" CommandName="Edit"><img src="../Login_assets/img/icon-edit.png" alt="Edit" style="cursor: pointer; height: 18px; width: 18px;"/></asp:LinkButton>
                                                    &nbsp;
                           <%--<asp:ImageButton ID="imgbtnDelete" OnClientClick="Confirm()" runat="server" CommandName="Delete" ImageUrl="assets/image/Delete.jpg" Height="22px" Width="22px" />--%>
                                                    <%--<img id="imgbtnDelete" style="cursor: pointer; height: 20px; width: 20px;" onclientclick="Confirm()" alt="Edit" src="img/Delete.jpg" onclick="DeleteStudent(this);" />--%>
                                                    <asp:LinkButton Visible="false" ID="imgbtnDelete" runat="server" CommandName="Delete"><img src="../Login_assets/img/Delete.jpg" alt="Delete" style="cursor: pointer; height: 18px; width: 18px;"/></asp:LinkButton>
                                                </ItemTemplate>

                                                <EditItemTemplate>

                                                    <%--<asp:LinkButton ID="imgbtnUpdate" runat="server" CommandName="Update" ImageUrl="" Height="22px" Width="22px" />--%>
                                                    <asp:LinkButton ID="imgbtnUpdate" runat="server" CommandName="Update"><img src="../Login_assets/img/icon-update1.jpg" alt="Update" style="cursor: pointer; height: 18px; width: 18px;"/></asp:LinkButton>

                                                    <%--<asp:LinkButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="" Height="22px" Width="22px" />--%>
                                                    <asp:LinkButton ID="imgbtnCancel" runat="server" CommandName="Cancel"><img src="../Login_assets/img/icon-Cancel.jpg" alt="Cancel" style="cursor: pointer; height: 18px; width: 18px;"/></asp:LinkButton>

                                                </EditItemTemplate>

                                                <FooterTemplate>

                                                    <asp:LinkButton ID="lbtnAdd" runat="server" CommandName="ADD" Text="Tambah Lagi" Width="100px"></asp:LinkButton>
                                                    <%--<img id="lbtnAdd" CommandName="ADD" style="cursor: pointer; height: 20px; width: 20px;" alt="Edit" src="img/add-button-blue-hi.png" onclick="addStudent(this);" />--%>
                                                </FooterTemplate>

                                            </asp:TemplateField>

                                        </Columns>

                                    </asp:GridView>
               </div>
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

