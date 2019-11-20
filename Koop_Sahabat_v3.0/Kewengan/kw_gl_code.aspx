<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../Kewengan/kw_gl_code.aspx.cs" Inherits="kw_gl_code" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>
   <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>  Selenggara Integrasi    </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>SUMBER MANUSIA</a></li>
                            <li class="active"> Selenggara Integrasi </li>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl1_text" runat="server">Jadual Rujukan <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                          <asp:DropDownList ID="sel_rt" CssClass=" form-control select2 uppercase" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dd_SelectedIndexChanged">
                                                             
                                                            </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                                <div class="box-body">&nbsp;</div>
                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                   <%--   <div class="row" style="overflow:auto;">--%>
           <div class="col-md-12 box-body">
                                  <asp:GridView ID="gv_refdata" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="true" GridLines="None" OnRowCommand="gv_refdata_RowCommand"
                                                        OnRowDeleting="gv_refdata_RowDeleting" OnRowUpdating="gv_refdata_RowUpdating"
                                                        OnRowCancelingEdit="gv_refdata_RowCancelingEdit" OnRowEditing="gv_refdata_RowEditing" OnPageIndexChanging="gv_refdata_PageIndexChanging" OnRowDataBound="gv_refdata_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="BIL">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                        ItemStyle-Width="150" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="KETERANGAN">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEmpID" class="uppercase" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "C1") %>'  ItemStyle-Width="500"></asp:Label>
                                                                    <asp:TextBox ID="Id" Visible="false" class="uppercase" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "c5") %>'  ItemStyle-Width="500"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lblEditName" class="form-control uppercase" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "c1") %>'  ItemStyle-Width="500"></asp:TextBox>
                                                                    <asp:TextBox ID="Id" Visible="false" class="uppercase" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "c5") %>'  ItemStyle-Width="500"></asp:TextBox>
                                                                </EditItemTemplate>
                                                               
                                                            </asp:TemplateField>
                                                         
                                                             <asp:TemplateField HeaderText="KOD AKAUN DEBIT" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblkodID" class="uppercase" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "C2") %>'   ></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>           
                                                                <asp:DropDownList ID="lbleditkodID" runat="server" class="form-control select2 uppercase"  ></asp:DropDownList>
                                                            </EditItemTemplate>
                                                           
                                                        </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="KOD AKAUN KREDIT">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblkodID1" class="uppercase" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "C3") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>           
                                                                <asp:DropDownList ID="lbleditkodID1" runat="server" class="form-control select2 uppercase"></asp:DropDownList>
                                                            </EditItemTemplate>
                                                           
                                                        </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="STATUS">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="status" class="uppercase" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "C4").ToString() == "A" ? "Aktif" : DataBinder.Eval(Container.DataItem, "C4").ToString() == "T" ? "Tidak Aktif" : "" %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="editddlStatus" SelectedValue='<%#Eval("C4") %>' runat="server"
                                                                        class="form-control select2 uppercase">
                                                                        <asp:ListItem Text="--- PILIH ---" Value=""></asp:ListItem>
                                                                        <asp:ListItem Text="Aktif" Value="A"></asp:ListItem>
                                                                        <asp:ListItem Text="Tidak Aktif" Value="TA"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </EditItemTemplate>
                                                              
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
                                                                    <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="../Login_assets/img/icon-Cancel.jpg"
                                                                        Height="15px" Width="15px" />
                                                                </EditItemTemplate>
                                                               
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


