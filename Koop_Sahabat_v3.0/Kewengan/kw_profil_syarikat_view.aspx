<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Kewengan/kw_profil_syarikat_view.aspx.cs" Inherits="kw_profil_syarikat_view" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
      <%--<script type = "text/javascript">
          function Confirm() {
              var confirm_value = document.createElement("INPUT");
              confirm_value.type = "hidden";
              confirm_value.name = "confirm_value";
              //if (confirm("Press OK for Update the Financial Year \nPress Cancel for Update the Genral Information?? !!")) {
              if (confirm("TEKAN BUTANG OK UNTUK KEMASKINI TAHUN KEWANGAN...")) {
                  confirm_value.value = "Yes";
              } else {
                  confirm_value.value = "No";
              }
              document.forms[0].appendChild(confirm_value);
          }
    </script>--%>
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
                        <h1><asp:Label ID="ps_lbl1" runat="server"></asp:Label></h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i><asp:Label ID="ps_lbl2" runat="server"></asp:Label></a></li>
                            <li class="active"><asp:Label ID="ps_lbl3" runat="server"></asp:Label></li>
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
                                       <%-- <asp:Button ID="button4" runat="server" Text="Carian"  class="align-center btn btn-primary" UseSubmitBehavior="false" OnClick="btn_search_Click"></asp:Button>--%>
                                         <asp:Button ID="Button3" runat="server" class="btn btn-danger" Text="Hapus" UseSubmitBehavior="false" OnClick="btn_hups_Click" />
                                         <asp:Button  ID="Button5" runat="server" Text=" Tambah" OnClick="Add_profile"  class="align-center btn btn-primary"></asp:Button>
                                    </div>
                                </div>
                            </div>
      </div> 
                                <div class="box-body">&nbsp;
                                    </div>
                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                    <asp:GridView ID="gv_refdata" runat="server" class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="1000000" >
                                                <Columns>
                                                    <asp:TemplateField HeaderText="BIL" HeaderStyle-Width="2%">
                                                        <ItemStyle HorizontalAlign="Center" Width="2%"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="NAMA SYARIKAT" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="Left" Width="15%" Font-Bold="true"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkView" runat="server" Font-Underline OnClick="lnkView_Click">
                                                                <i class='fa fa-edit'></i>&nbsp;&nbsp;<asp:Label ID="Label21" runat="server" Text='<%# Eval("nama_syarikat") %>'></asp:Label>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="NOMBOR PENDAFTARAN SYARIKAT">
                                                        <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("kod_syarikat") %>' CssClass="uppercase"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TAHUN KEWANGAN">
                                                        <ItemStyle HorizontalAlign="center" Width="5%"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2_fl" runat="server" Text='<%# Eval("fin_year") %>'></asp:Label>
                                                            <asp:Label ID="Label1" Visible="false" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                                                            <asp:TextBox ID="og_genid" Visible="false" runat="server" Text='<%# Eval("id") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Status">
                                                        <ItemStyle HorizontalAlign="center" Width="2%"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label5" runat="server" Text='<%# Eval("sts") %>' CssClass="uppercase"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="HAPUS" ItemStyle-Width="3%" ControlStyle-CssClass="panel-heading">
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="RadioButton1" runat="server" />
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
                                <%--</div>--%>
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

