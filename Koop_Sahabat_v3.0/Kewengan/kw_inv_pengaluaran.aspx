<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Kewengan/kw_inv_pengaluaran.aspx.cs" Inherits="kw_inv_pengaluaran" %>
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
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title"><asp:Label ID="ps_lbl4" runat="server"></asp:Label></h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl5" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox5" runat="server" class="form-control validate[optional] uppercase"  MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl6" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                         <asp:TextBox ID="tk_mula" runat="server" class="form-control validate[optional] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl7" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox7" runat="server" class="form-control validate[optional] uppercase" MaxLength="1000"></asp:TextBox>                                                        
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl8" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                          <div class="input-group">
                                       <asp:TextBox ID="tk_akhir" runat="server" class="form-control validate[optional] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
                                              <span class="input-group-addon" ><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl9" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="DropDownList3" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" OnSelectedIndexChanged="get_pelinfo" AutoPostBack="true">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl10" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="TextBox2" runat="server" class="form-control validate[optional] uppercase" TextMode="MultiLine"></asp:TextBox>                                                        
                                    </div>
                                </div>
                            </div>   
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl11" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox1" runat="server" class="form-control validate[optional] uppercase" TextMode="MultiLine"></asp:TextBox>      
                                    </div>
                                </div>
                            </div>                             
                                 </div>
                         </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl12" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                             <label>
                                                            <asp:RadioButton ID="Rdya" runat="server" AutoPostBack="true" GroupName="war" />&nbsp;<asp:Label ID="ps_lbl13" runat="server"></asp:Label>
                                                        </label>
                                        &nbsp;&nbsp;&nbsp;
                                         <label>
                                                            <asp:RadioButton ID="Rdtidak" runat="server" AutoPostBack="true" GroupName="war" />&nbsp;<asp:Label ID="ps_lbl14" runat="server"></asp:Label>
                                                        </label>                                                      
                                    </div>
                                </div>
                            </div>                            
                                 </div>
                         </div>
                            
                          
                                       <div class="box-body">&nbsp;</div>
           <div class="col-md-12 box-body" id="grd1" runat="server">
                                            <asp:GridView ID="grvStudentDetails" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="20" onrowdatabound="gvEmp_RowDataBound" OnRowDeleting="grvStudentDetails_RowDeleting" ShowFooter="True">
                                                        <Columns>
                                                            <asp:TemplateField Visible="false" HeaderText="BIL">
                                                                <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:label ID="RowNumber"  runat="server" Text='<%# Eval("RowNumber") %>'></asp:label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Kod Barang">
                                                                 <ItemStyle Width="20%"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="Col1" style="width:100%; font-size:13px;" runat="server" CssClass="form-control select2 validate[optional]" onselectedindexchanged="ddgstdeboth_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                 <FooterStyle HorizontalAlign="Left" />
                                                                <FooterTemplate>
                                                                    <asp:Button ID="ButtonAdd" runat="server" Text="Tambah Baru" OnClick="ButtonAdd_Click" />
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Jumlah Kuantiti">
                                                                <ItemTemplate>
                                                                   <asp:TextBox ID="Col2" CssClass="form-control uppercase " OnTextChanged="QtyChanged" AutoPostBack="true" runat="server" Text='<%# Eval("col2") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                           <%-- <asp:TemplateField HeaderText="Harga / Unit (RM)" HeaderStyle-HorizontalAlign="Right">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <ItemTemplate>
                                                                   <asp:TextBox ID="Col3" style="text-align:right;" CssClass="form-control" OnTextChanged="QtyChanged1" AutoPostBack="true" placeholder="0.00" Text='<%# Eval("col3","{0:n}") %>'  runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                              
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="Jumlah harga (KOS)" HeaderStyle-HorizontalAlign="Right">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <ItemTemplate>
                                                                   <asp:TextBox ID="Col3" ReadOnly="true" style="text-align:right;" CssClass="form-control " placeholder="0.00" Text='<%# Eval("col3","{0:n}") %>' runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:CommandField ShowDeleteButton="True" />
                                                        </Columns>
                                                       <%-- <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="White" />--%>
                                                    </asp:GridView> 

                                                 </div> 

                          
           <div class="col-md-12 box-body" id="grd2" runat="server">
                                   <asp:GridView ID="gv_refdata" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25"
                                       OnPageIndexChanging="gvSelected_PageIndexChanging1">
                                                    <Columns>
                                                    <asp:TemplateField HeaderText="BIL">  
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kod Barang">
                                                           <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl1" runat="server" Text='<%# Eval("kod_barang") %>'></asp:Label>
                                                                      <asp:Label ID="lbl_id" Visible="false" runat="server" Text='<%# Bind("do_no") %>' CssClass="uppercase"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Jenis Barangan">
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl2" runat="server" Text='<%# Eval("jenis_barang") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Keterangan Pengeluaran">
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl3" runat="server" Text='<%# Eval("keterangan_peng") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField> 
                                                         <asp:TemplateField HeaderText="Jumlah Kuantiti">
                                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl4" runat="server" Text='<%# Eval("jum_qty") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField> 
                                                         <asp:TemplateField HeaderText="Baki Kuantiti">
                                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl5" runat="server" Text='<%# Eval("bqty") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>   
                                                           <asp:TemplateField HeaderText="Jumlah (RM)">
                                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl6" runat="server" Text='<%# Eval("jumlah","{0:n}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>                                                     
                                                    </Columns>
                                                </asp:GridView>
               </div>                         
                            
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                 <asp:TextBox ID="lbl_name" runat="server" class="form-control validate[optional] uppercase" Visible="false"></asp:TextBox>
                                 <asp:TextBox ID="ver_id" Visible="false" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                                <asp:TextBox ID="get_id" Visible="false" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                                 <asp:Button ID="Button4" runat="server" class="btn btn-danger" Text="Simpan" OnClick="clk_submit" UseSubmitBehavior="false" />
                                <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" UseSubmitBehavior="false" OnClick="Button5_Click" />
                                <asp:Button ID="Button5" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" OnClick="Click_bck" />
                                 
                            </div>
                           </div>
                               </div>
                           <div class="box-body">&nbsp;
                                    </div>

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

