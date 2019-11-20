<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_Ref_Table_allowance.aspx.cs" Inherits="HR_Ref_Table_allowance" %>

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
                        <h1 id="h1_tag" runat="server"> Potongan Statutori </h1>
                        <ol class="breadcrumb">
                            <li id="bb1_text" runat="server"><a href="#"><i class="fa fa-dashboard"></i>Sumber Manusia</a></li>
                            <li class="active" id="bb2_text" runat="server"> Potongan Statutori </li>
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
                                          <asp:DropDownList ID="sel_rt" CssClass=" form-control uppercase" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dd_SelectedIndexChanged">
                                                             
                                                               <asp:ListItem Value="01">Jenis Elaun</asp:ListItem>
                                                                <asp:ListItem Value="02">Potongan</asp:ListItem>
                                                                <asp:ListItem Value="03">Kerja Lebih Masa</asp:ListItem>
                                                                <asp:ListItem Value="04">Lain-lain</asp:ListItem>
                                               <asp:ListItem Value="05">Bonus</asp:ListItem>
                                                            </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                                <hr />
                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12" style="overflow:auto;">
                
                                                    <asp:GridView ID="gv_refdata" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="1000000" ShowFooter="false" GridLines="None" OnRowCommand="gv_refdata_RowCommand"
                                                        OnRowDeleting="gv_refdata_RowDeleting" OnRowUpdating="gv_refdata_RowUpdating"
                                                        OnRowCancelingEdit="gv_refdata_RowCancelingEdit" OnRowEditing="gv_refdata_RowEditing">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="BIL" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                        ItemStyle-Width="150" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="KOD" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEmpID" class="uppercase" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "c1") %>'></asp:Label>
                                                                    <asp:TextBox ID="Id" Visible="false" class="uppercase" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "c4") %>'></asp:TextBox>
                                                                    <asp:TextBox ID="TextBox1" Visible="false" class="uppercase" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "c2") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="lblEditName" class="form-control uppercase" Width="100%" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "c1") %>'></asp:Label>
                                                                    <asp:TextBox ID="Id" Visible="false" class="uppercase" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "c4") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txtAddName" placeholder="sila masukkan nilai"  Width="100%" runat="server" class=" form-control uppercase"></asp:TextBox>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="KETERANGAN">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblName" class="uppercase" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "c2") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="txtEditcode" class="form-control uppercase" Width="100%" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "c2") %>'></asp:Label>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txtAddcode" placeholder="sila masukkan nilai" Width="100%" class="form-control uppercase"
                                                                        runat="server"></asp:TextBox>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="KWSP" SortExpression="Visible">
                                                                <ItemTemplate>
                                                                    <center><asp:CheckBox ID="chk_kwsp" runat="server" Checked='<%#Eval("c5").ToString() == "S" ? true : false %>'>
                                                                    </asp:CheckBox></center>
                                                                    
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <center><asp:CheckBox ID="chk_kwsp_upd" runat="server" Checked='<%#Eval("c5").ToString() == "S" ? true : false %>'/></center>
                                                                </EditItemTemplate>
                                                                 <FooterTemplate>
                                                               <center> <asp:CheckBox ID="chk_kwsp_upd1" runat="server" Checked='<%#Convert.ToBoolean(Eval("c5"))%>'/></center>
                                                                 </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="PERKESO" SortExpression="Visible">
                                                                <ItemTemplate>
                                                                    <center><asp:CheckBox ID="chk_perkeso" runat="server" Checked='<%#Eval("c6").ToString() == "S" ? true : false %>'/></center>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <center><asp:CheckBox ID="chk_perkeso_upd" runat="server" Checked='<%#Eval("c6").ToString() == "S" ? true : false %>'/></center>
                                                                </EditItemTemplate>
                                                                 <FooterTemplate>
                                                                <center><asp:CheckBox ID="chk_perkeso_upd1" runat="server" Checked='<%#Convert.ToBoolean(Eval("c6"))%>'/></center>
                                                                 </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="PCB" SortExpression="Visible">
                                                                <ItemTemplate>
                                                                    <center><asp:CheckBox ID="chk_pcb" runat="server" Checked='<%#Eval("c7").ToString() == "S" ? true : false %>'/></center>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <center><asp:CheckBox ID="chk_pcb_upd" runat="server" Checked='<%#Eval("c7").ToString() == "S" ? true : false %>'/></center>
                                                                </EditItemTemplate>
                                                                 <FooterTemplate>
                                                                  <center><asp:CheckBox ID="chk_pcb_upd1" runat="server" Checked='<%#Convert.ToBoolean(Eval("c7"))%>'/></center>
                                                                 </FooterTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="SIP" SortExpression="Visible">
                                                                <ItemTemplate>
                                                                    <center><asp:CheckBox ID="chk_sip" runat="server" Checked='<%#Eval("c8").ToString() == "S" ? true : false %>'/></center>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <center><asp:CheckBox ID="chk_sip_upd" runat="server" Checked='<%#Eval("c8").ToString() == "S" ? true : false %>'/></center>
                                                                </EditItemTemplate>
                                                                 <FooterTemplate>
                                                                  <center><asp:CheckBox ID="chk_sip_upd1" runat="server" Checked='<%#Convert.ToBoolean(Eval("c8"))%>'/></center>
                                                                 </FooterTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="STATUS" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="status" class="uppercase" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "c3").ToString() == "A" ? "Aktif" : "Tidak Aktif" %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="editddlStatus" Width="100%" SelectedValue='<%#Eval("c3") %>' runat="server"
                                                                        class="form-control uppercase">
                                                                        <asp:ListItem Text="Aktif" Value="A"></asp:ListItem>
                                                                        <asp:ListItem Text="Tidak Aktif" Value="TA"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:DropDownList ID="ddlStatus" Width="100%" runat="server" class="form-control uppercase">
                                                                        <asp:ListItem Text="Aktif" Value="A"></asp:ListItem>
                                                                        <asp:ListItem Text="Tidak Aktif" Value="TA"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="TINDAKAN" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imgbtnEdit" runat="server" CommandName="Edit" ImageUrl="../Login_assets/img/icon-edit.png"
                                                                        Height="15px" Width="15px" />
                                                                    &nbsp;
                                                                    <asp:ImageButton ID="imgbtnDelete" OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;" runat="server" CommandName="Delete"
                                                                        ImageUrl="../Login_assets/img/Delete.jpg" Height="15px" Width="15px"/>
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