<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Pentadbiran/kw_presub_skrin_view.aspx.cs" Inherits="kw_presub_skrin_view" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <script>
  $(function () {
    
      $('#<%=gv_refdata.ClientID %>').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
          "responsive": true,
          "sPaginationType": "full_numbers",
          "iDisplayLength": 15,
          "aLengthMenu": [[15, 30, 50, 100], [15, 30, 50, 100]]
      });
  })
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

   
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" style="height: 895px;">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>Senarai Pre Sub Skrin</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Pentadbiran</a></li>
                            <li class="active">Senarai Pre Sub Skrin</li>
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

           <%--<div class="col-md-3 box-body">
                                <div class="form-group">
                                    <div class="col-sm-12">
                                         <div class="input-group">
                                                <span class="input-group-addon" style="background-color:#0090d9; color:#fff;" ><i class="fa fa-search"></i></span>
                                        <asp:TextBox ID="srch_id" class="form-control" runat="server" OnTextChanged="srch_id_TextChanged" AutoPostBack="True" placeholder="MASUKKAN NILAI DI SINI"></asp:TextBox>
                                             </div>
                                    </div>
                                   
                                </div>
                            </div>--%>
                                     <div class="col-md-12 box-body">
                                <div class="form-group">
                                     <div class="col-sm-12" style="text-align:right;">
                                        <%--<asp:Button ID="button4" runat="server" Text="Carian"  class="align-center btn btn-primary" UseSubmitBehavior="false" OnClick="btn_search_Click"></asp:Button>--%>
                                         <asp:Button runat="server" Text="+ Tambah" OnClick="Add_profile"  class="align-center btn btn-primary"></asp:Button>
                                    </div>
                                </div>
                            </div>
      </div> 
                                <div class="box-body">&nbsp;
                                    </div>
                                <div class="dataTables_wrapper form-inline dt-bootstrap"  style="overflow:auto;">
                                    <%--  <div class="row">--%>
           <div class="col-md-12 box-body">
                                    <asp:GridView  ID="gv_refdata" runat="server" class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="1000000">
                                                        <%--<HeaderStyle ForeColor="#ffffff" />--%>
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Hapus" Visible="false" ItemStyle-Width="3%" ControlStyle-CssClass="panel-heading">
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="RadioButton1" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="BIL" HeaderStyle-Width="3%">
                                                                <ItemStyle HorizontalAlign="Center" Width="2%"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Kod Pre Sub Skrin">
                                                                <ItemStyle HorizontalAlign="center" Width="5%" Font-Bold="true"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkView" runat="server" OnClick="lnkView_Click" Font-Bold Font-Underline>
                                                                        <asp:Label ID="kat_cd" runat="server" Text='<%# Eval("KK_Spreskrin_id") %>' CssClass="uppercase"></asp:Label>
                                                                        <asp:Label ID="og_genid" runat="server" Text='<%# Eval("Id") %>' Visible="false" CssClass="uppercase"></asp:Label>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Nama Sub Skrin" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                                <ItemTemplate>
                                                                        <asp:Label ID="lbl_mskrin" runat="server" Text='<%# Eval("sk_name") %>' CssClass="uppercase"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                            <asp:TemplateField HeaderText="Nama Pre Sub Skrin" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                                <ItemTemplate>
                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("KK_Spreskrin_name") %>' CssClass="uppercase"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                               <asp:TemplateField HeaderText="Position">
                                                                <ItemStyle HorizontalAlign="center" Width="5%" ></ItemStyle>
                                                                <ItemTemplate>
                                                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("position") %>' CssClass="uppercase"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="STATUS">
                                                                <ItemStyle HorizontalAlign="center" Width="5%"></ItemStyle>
                                                                <ItemTemplate>
                                                                        <asp:Label ID="bal_sts" runat="server" Text='<%# Eval("sts") %>' CssClass="uppercase"></asp:Label>
                                                                </ItemTemplate>
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
                              <%--  </div>--%>
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

