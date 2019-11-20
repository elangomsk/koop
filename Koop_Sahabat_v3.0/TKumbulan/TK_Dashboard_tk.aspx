<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="TK_Dashboard_tk.aspx.cs" Inherits="TKumbulan_TK_Dashboard_tk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:ScriptManager ID="ScriptManagerCalendar" AsyncPostBackTimeOut="72000" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
   
     <!-- Content Wrapper. Contains page content -->
       
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>Dashboard Tabung Kumpulan</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Tabung Kumpulan</a></li>
                            <li class="active">Dashboard Tabung Kumpulan</li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
                
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate> 
              <div class="row">
                                    <div class="col-md-12">
                                        <div class="box box-info">
                        <div class="box-header with-border">
                                               <h3 class="box-title">Carian Maklumat Tabung Kumpulan Sahabat</h3>
                                               </div>
                                     <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                              <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No KP Baru <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                                       <asp:TextBox ID="txticno" runat="server" class="form-control validate[optional]" MaxLength="12" Style="text-transform: uppercase;"></asp:TextBox>                                                                                               
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Cawangan</label>
                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlcaw" runat="server" class="form-control validate[optional] select2 uppercase">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                             <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama Pusat</label>
                                    <div class="col-sm-8">
                                                       <asp:TextBox ID="txtbname" runat="server" class="form-control uppercase" Style="text-transform: uppercase;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <asp:Button ID="btnsrch" runat="server" class="btn btn-success" Text="Carian"  onclick="btnsrch_Click" />
                                                            <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula"  UseSubmitBehavior="false"
                                                            onclick="btnrst_Click" />
                                </div>
                            </div>
                                 </div>
                                </div>
                       
                                 <div class="box-header with-border">
                                               <h3 class="box-title">Maklumat Sahabat</h3>
                                               </div>
                                     <div class="box-body">&nbsp;</div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Sahabat</label>
                                    <div class="col-sm-8">
                                                      <asp:TextBox ID="txtsahabt" runat="server" class="form-control" Style="text-transform: uppercase;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No KP Baru</label>
                                    <div class="col-sm-8">
                                                     <asp:TextBox ID="txtnokb" runat="server" class="form-control validate[optional,custom[onlyLetterNumberSp]]" MaxLength="12" Style="text-transform: uppercase;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama</label>
                                    <div class="col-sm-8">
                                                     <asp:TextBox ID="txtname" runat="server" class="form-control validate[optional,custom[onlyLetterNumberSp]]" MaxLength="150"  Style="text-transform: uppercase;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Wilayah</label>
                                    <div class="col-sm-8">
                                                   <asp:TextBox ID="txtwila" runat="server" class="form-control" Style="text-transform: uppercase;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Cawangan</label>
                                    <div class="col-sm-8">
                                                     <asp:TextBox ID="txtcaw" runat="server" class="form-control" Style="text-transform: uppercase;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama Pusat</label>
                                    <div class="col-sm-8">
                                                    <asp:TextBox ID="txtpust" runat="server" class="form-control" Style="text-transform: uppercase;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                               
                                 <div class="box-header with-border">
                                               <h3 class="box-title">Maklumat Simpanan Tetap (ST) Sahabat</h3>
                                               </div>
                                     <div class="box-body">&nbsp;</div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Akhir Baki</label>
                                    <div class="col-sm-8">
                                                      <asp:TextBox ID="txttaric" runat="server" class="form-control" Style="text-transform: uppercase;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jumlah Baki (RM)</label>
                                    <div class="col-sm-8 text-right">
                                                    <asp:TextBox ID="txtjum" runat="server" class="form-control" Style="text-transform: uppercase; text-align:right;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                  <div class="box-body">&nbsp;</div>            
                             <div class="box-header with-border">
                                               <h3 class="box-title">Maklumat Pengeluaraan Simpanan Tetap (PST) Sahabat</h3>
                                               </div>
                                     <div class="box-body">&nbsp;</div>
                                 <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">                                    
           <div class="col-md-12 box-body">
                                            <div class="text-left" style="padding-bottom:7px;"><strong>Transaksi Kredit / Debit Modal Syer</strong></div>
                                            <asp:GridView ID="GridView2" class="col-md-12 col-sm-4" CellPadding="3" runat="server"  AllowPaging="true" PageSize="10" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black"  
                                             AutoGenerateColumns="false" >
                                        <Columns>
                                        <asp:TemplateField HeaderText="BIL">  
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="150"/>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TARIKH PERMOHONAN">  
                                            <ItemTemplate>  
                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("pst_post_dt", "{0:dd/MM/yyyy}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JENIS PST">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("Product_Code") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JUMLAH PST(RM)">   
                                                <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>  
                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("pst_apply_amt","{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JUMLAH KESELURUHAN PST(RM)">   
                                                <ItemStyle HorizontalAlign="Right" />

                                            <ItemTemplate>  
                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("total" , "{0:n}") %>'></asp:Label>  
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
                              <div class="box-body">&nbsp;
                                    </div> 
                               
                                  <div class="box-header with-border">
                                               <h3 class="box-title">Maklumat Pembayaran WP4-TK Sahabat</h3>
                                               </div>
                             <div class="box-body">&nbsp;</div>
                                 <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">                                    
           <div class="col-md-12 box-body">
                                    <asp:GridView ID="GridView1" class="col-md-12 col-sm-4" CellPadding="3" runat="server"  AllowPaging="true" PageSize="10" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black"  
                                             AutoGenerateColumns="false" >
                                        <Columns>
                                        <asp:TemplateField HeaderText="BIL">  
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="150"/>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NAMA KELOMPOK">  
                                            <ItemTemplate>  
                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("wp4_batch_name") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TARIKH BAYARAN">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("wp4_pay_txn_dt", "{0:dd/MM/yyyy}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JENIS BAYARAN">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("wp4_pay_detail") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JUMLAH (RM)">
                                                <ItemStyle HorizontalAlign="Right" />
                                                   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("wp4_amt","{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="JUMLAH BAYARAN (RM)">  
                                                <ItemStyle HorizontalAlign="Right" />
                                                
                                            <ItemTemplate>  
                                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("TOTAL" ,"{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                 <%--
                                                <asp:TemplateField HeaderText="JUMLAH (RM)">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("sha_credit_amt") %>'></asp:Label>  
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
                               <div class="box-body">&nbsp;
                                    </div> 
                               <%-- <div class="row">
                                    <div class="col-md-12 col-sm-4" style="text-align: center;">
                                        <div class="body collapse in">
                                            <asp:Button ID="btnrest" runat="server" class="btn btn-danger" Text="Batal" />
                                        </div>
                                    </div>
                                </div>--%>
                            </div>
                                        </div>
                                        </div>
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

