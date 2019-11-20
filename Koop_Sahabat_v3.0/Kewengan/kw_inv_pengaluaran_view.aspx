<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Kewengan/kw_inv_pengaluaran_view.aspx.cs" Inherits="kw_inv_pengaluaran_view" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
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
                            <div class="box-body">&nbsp;</div>
                            <div class="box-body">

                                 <div class="row">

           <div class="col-md-3 box-body">
                                <div class="form-group">
                                    <div class="col-sm-12">
                                         <div class="input-group">
                                                <span class="input-group-addon" style="background-color:#0090d9; color:#fff;" ><i class="fa fa-search"></i></span>
                                        <asp:TextBox ID="txtSearch" class="form-control" runat="server" OnTextChanged="txtSearch_TextChanged" AutoPostBack="True" placeholder="MASUKKAN NILAI DI SINI"></asp:TextBox>
                                             </div>
                                    </div>
                                   
                                </div>
                            </div>
                                     <div class="col-md-6 box-body">
                                <div class="form-group">
                                     <div class="col-sm-6">
                                        <asp:Button ID="button4" runat="server" Text="Carian"  class="align-center btn btn-primary" UseSubmitBehavior="false" OnClick="btn_search_Click"></asp:Button>
                                         <%--<asp:Button ID="Button3" runat="server" class="btn btn-danger" Text="Hapus" UseSubmitBehavior="false" OnClick="btn_hups_Click" />--%>
                                         <asp:Button ID="Button5" runat="server" Text=" Tambah" OnClick="Add_profile"  class="align-center btn btn-default"></asp:Button>
                                    </div>
                                </div>
                            </div>
      </div> 
                                <div class="box-body">&nbsp;
                                    </div>
                                 <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                    <%--  <div class="row" style="overflow:auto;">--%>
           <div class="col-md-12 box-body">
          
                                                <div id="Div1" class="nav-tabs-custom" role="tabpanel" runat="server">
                                                 <!-- Nav tabs -->
                                                            <ul class="s1 nav nav-tabs" role="tablist">
                                                            <li id="pp6" runat="server" class="active"><a href="#ContentPlaceHolder1_p6" aria-controls="p6" role="tab" data-toggle="tab"><strong><asp:Label ID="ps_lbl6" runat="server"></asp:Label></strong></a></li>
                                                                <li id="pp1" runat="server"><a href="#ContentPlaceHolder1_p1" aria-controls="p1" role="tab" data-toggle="tab"><strong><asp:Label ID="ps_lbl7" runat="server"></asp:Label></strong></a></li>
                                                                <li id="pp2" runat="server"><a href="#ContentPlaceHolder1_p2" aria-controls="p2" role="tab" data-toggle="tab"><strong><asp:Label ID="ps_lbl8" runat="server"></asp:Label></strong></a></li>
                                                                                                                               
                                                            </ul>
                                                            <!-- Tab panes -->
                                                            <div class="tab-content">
                                                             <div role="tabpanel" class="tab-pane active" runat="server" id="p6">
                                             <div class="col-md-12 table-responsive uppercase" style="overflow:auto;">
                                             <asp:GridView ID="gv_refdata" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25"
                                       OnPageIndexChanging="gvSelected_PageIndexChanging">
                                                    <Columns>
                                                    <asp:TemplateField HeaderText="BIL">  
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="No. DO">
                                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                            <ItemTemplate>
                                                                  <asp:LinkButton runat="server" ID="lnkView" OnClick="lnkView_Click" Font-Underline Font-Bold>
                                                                <asp:Label ID="lbl3" runat="server" Text='<%#  Highlight(Eval("do_no").ToString()) %>'></asp:Label>
                                                                         </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField> 
                                                        <asp:TemplateField HeaderText="Tarikh">
                                                           <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl1" runat="server" Text='<%# Eval("crt_dt") %>'></asp:Label>
                                                                      <asp:Label ID="lbl_id" Visible="false" runat="server" Text='<%# Bind("do_no") %>' CssClass="uppercase"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="No. Dokumen">
                                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl2" runat="server" Text='<%# Highlight(Eval("po_no").ToString()) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Nama Syarikat">
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl4" runat="server" Text='<%#  Highlight(Eval("Ref_nama_syarikat").ToString()) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField> 
                                                         <asp:TemplateField HeaderText="Jumlah Kuantiti">
                                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl5" runat="server" Text='<%# Eval("qty") %>'></asp:Label>
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
                                                                <div role="tabpanel" class="tab-pane" runat="server" id="p1">
                                                  <div class="col-md-12 table-responsive uppercase" style="overflow:auto;">
                                             <div class="dataTables_wrapper form-inline dt-bootstrap" >
                                      <div class="row" style="overflow:auto;">
           <div class="col-md-12 box-body">
                                   <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25"
                                       OnPageIndexChanging="gvSelected_PageIndexChanging1" OnDataBound="OnDataBound">
                                                    <Columns>
                                                  
                                                        <asp:TemplateField HeaderText="Kod Barang">
                                                           <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                 <%--<asp:LinkButton runat="server" ID="lnkView" OnClick="lnkView_Click" Font-Underline Font-Bold>--%>
                                                                <asp:Label ID="lbl11" runat="server" Text='<%# Highlight(Eval("kod_barang").ToString()) %>'></asp:Label>
                                                                      <asp:Label ID="lbl_id" Visible="false" runat="server" Text='<%# Bind("seq_no") %>' CssClass="uppercase"></asp:Label>
                                                                     <%-- </asp:LinkButton>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                           <asp:TemplateField HeaderText="Nama Barangan">
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl121" runat="server" Text='<%# Highlight(Eval("nama_barang").ToString()) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="">  
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tarikh">
                                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl12" runat="server" Text='<%# Eval("tarikh") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Nama Syarikat">
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl13" runat="server" Text='<%# Eval("nama_syarikat") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField> 
                                                         <asp:TemplateField HeaderText="Kuantiti Masuk">
                                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl14" runat="server" Text='<%# Eval("qty_masuk") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField> 
                                                         <asp:TemplateField HeaderText="Kuantiti Keluar">
                                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl15" runat="server" Text='<%# Eval("qty_keluar") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField> 
                                                         <%-- <asp:TemplateField HeaderText="Kuantiti Keluar">
                                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl15" runat="server" Text='<%# Eval("out_qty") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField> --%>
                                                         <asp:TemplateField HeaderText="Baki Kuantiti">
                                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl19" runat="server" Text='<%# Eval("qty_baki") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField> 
                                                         <asp:TemplateField HeaderText="Kuantiti">
                                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl20" runat="server" Text='<%# Eval("qty") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>  
                                                        <asp:TemplateField HeaderText="Harga KOS (RM)">
                                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl21" runat="server" Text='<%# Eval("harga_kos","{0:n}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField> 
                                                          <asp:TemplateField HeaderText="Jumlah KOS (RM)">
                                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl22" runat="server" Text='<%# Eval("jumlah_kos","{0:n}") %>'></asp:Label>
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
                                </div>
                                                 </div> 
                                                  </div>
                                                                <div role="tabpanel" class="tab-pane" runat="server" id="p2">
                                                                   
                                                 <div class="col-md-12 table-responsive uppercase" style="overflow:auto;">
                                              <asp:GridView ID="GridView2" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25"
                                       OnPageIndexChanging="gvSelected_PageIndexChanging2" OnDataBound="OnDataBound1">
                                                    <Columns>
                                                   
                                                        <asp:TemplateField HeaderText="Kod Barang">
                                                           <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                 <%--<asp:LinkButton runat="server" ID="lnkView" OnClick="lnkView_Click" Font-Underline Font-Bold>--%>
                                                                <asp:Label ID="lbl11" runat="server" Text='<%# Highlight(Eval("kod_barang").ToString()) %>'></asp:Label>
                                                                      <asp:Label ID="lbl_id" Visible="false" runat="server" Text='<%# Bind("Id") %>' CssClass="uppercase"></asp:Label>
                                                                     <%-- </asp:LinkButton>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Jenis Barangan">
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl12" runat="server" Text='<%# Highlight(Eval("jenis_barang").ToString()) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="">  
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Keterangan Barangan">
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl13" runat="server" Text='<%# Eval("keterangan") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField> 
                                                       <asp:TemplateField HeaderText="Baki Keseluruhan">
                                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl19" runat="server" Text='<%# Eval("baki_kuantiti") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField> 
                                                         <asp:TemplateField HeaderText="Kos / Unit (RM)" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl15" runat="server" Text='<%# Eval("unit","{0:n}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField> 
                                                          <asp:TemplateField HeaderText="Jumlah Keseluruhan (RM)" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl16" runat="server" Text='<%# Eval("jum_kes","{0:n}") %>'></asp:Label>
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
                                                                
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                 <asp:Button ID="btn_ctk" runat="server" class="btn btn-warning" Text="Cetak Laporan" OnClick="clk_cetak" UseSubmitBehavior="false" />
                            </div>
                           </div>
                                                                   
                                                  </div>
                                                             
                                                 </div>                                                                        
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

