<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/Hr_sc_kakitangan_view.aspx.cs" Inherits="Hr_sc_kakitangan_view" %>

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
                        <h1> PENILAIAN PRESTASI </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>SUMBER_MANUSIA</a></li>
                            <li class="active"> PENILAIAN PRESTASI  </li>
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
                                     <div class="box-header with-border">
                            <h3 class="box-title"></h3>
                        </div>

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
                                         <asp:Button ID="Button3" runat="server" class="btn btn-danger" Text="Hapus" UseSubmitBehavior="false" OnClick="btn_hups_Click" />
                                         <asp:Button runat="server" Text="+ Tambah" OnClick="Add_profile"  class="align-center btn btn-primary"></asp:Button>
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
                                         <asp:TemplateField HeaderText="BIL">  
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="150"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NO KAKITANGAN">  
                                                <ItemStyle HorizontalAlign="Center" Width="7%"></ItemStyle>
                                            <ItemTemplate>  
                                                 <asp:Label ID="s_no" runat="server"  Text='<%# Eval("stf_staff_no") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NAMA"> 
                                                <ItemStyle HorizontalAlign="Left" Width="25%"></ItemStyle>  
                                            <ItemTemplate>  
                                                <asp:Label ID="s_name" runat="server"  Text='<%# Eval("stf_name") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JABATAN">  
                                                <ItemStyle HorizontalAlign="Left" Width="25%"></ItemStyle> 
                                            <ItemTemplate>  
                                                <asp:Label ID="s_jabatan" runat="server" Text='<%# Eval("hr_jaba_desc") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="GRED">   
                                                <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="s_gred" runat="server" Text='<%# Eval("hr_gred_desc") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="KATEGORI JAWATAN">   
                                                <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="s_kat_jawa" runat="server" Text='<%# Eval("hr_kejaw_desc") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="STATUS JAWATAN">   
                                                <ItemStyle HorizontalAlign="Left" Width="5%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="s_sts_jaw" runat="server" Text='<%# Eval("hr_traf_desc") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TEMPOH KHIDMAT">   
                                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="Label51" runat="server" Text=' <%# Eval("d_yr") + " TAHUN "+  Eval("d_mth") + " BULAN" %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CUTI DIBAWA KEHADAPAN">   
                                                <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:TextBox ID="stfcfwd" runat="server" CssClass="form-control validate[optional,custom[number]] uppercase" Text='<%# Eval("stf_carry_fwd_lv") %>'></asp:TextBox>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CUTI TAHUNAN">   
                                                <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:TextBox ID="ctlday" runat="server" CssClass="form-control validate[optional,custom[number]] uppercase" Text='<%# Eval("ct_lday") %>'></asp:TextBox>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CUTI SAKIT (PESAKIT LUAR) ">   
                                                <ItemStyle HorizontalAlign="center" Width="15%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:TextBox ID="cslday" runat="server" CssClass="form-control validate[optional,custom[number]] uppercase" Text='<%# Eval("cs_lday") %>'></asp:TextBox>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CUTI SAKIT (HOSPITALISASI)">   
                                                <ItemStyle HorizontalAlign="center" Width="15%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:TextBox ID="cshos" CssClass="form-control validate[optional,custom[number]] uppercase" runat="server" Text='<%# Eval("cs_hos") %>'></asp:TextBox>   
                                                <asp:Label ID="org_cd" runat="server" Visible="false" Text='<%# Eval("str_curr_org_cd") %>'></asp:Label>   
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






