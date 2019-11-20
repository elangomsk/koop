<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Aset/Ast_avail_ast.aspx.cs" Inherits="Ast_avail_ast" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
          <script>
           
     $(function () {
         $('.select2').select2();
     });
</script> 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server" ScriptMode="Release">
    </asp:ScriptManager>

     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" style="height: 895px;">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>Maklumat Tiada Penempatan Aset</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Pengurusan Aset</a></li>
                            <li class="active">Maklumat Tiada Penempatan Aset</li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
      
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Carian Aset</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                         <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                               <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Kategori </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="DD_Kategori" AutoPostBack="true" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" onselectedindexchanged="DD_Kategori_SelectedIndexChanged" 
                                                   >
                                            </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Sub Kategori</label>
                                    <div class="col-sm-8">
                                    <asp:DropDownList ID="DD_Sub" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged1">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jenis Aset</label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="DropDownList1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Aset Id</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox3" runat="server" class="form-control uppercase" MaxLength="14"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                          
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                 <asp:Button ID="Btn_Carian" runat="server" class="btn btn-primary" Text="Carian"  UseSubmitBehavior="false"
                                                    onclick="Btn_Carian_Click" />
                                                <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" OnClick="Reset_btn" usesubmitbehavior="false"/>
                            </div>
                           </div>
                               </div>
                            <div class="box-header with-border">
                            <h3 class="box-title">Senarai Tiada Penempatan Aset</h3>
                        </div>
                             <div class="box-body">&nbsp;</div>
                              <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                 <asp:GridView ID="gvSelected" runat="server" class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" OnPageIndexChanging="gv_refdata_PageIndexChanging">
                                      <PagerStyle CssClass="pager" />
                                      <Columns>
                                       <asp:TemplateField HeaderText="BIL">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                    ItemStyle-Width="150" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                       <asp:BoundField DataField="ast_kategori_desc" ItemStyle-HorizontalAlign="Left" HeaderText="KATEGORI ASET"  />
                                       <asp:BoundField DataField="ast_subkateast_desc" ItemStyle-HorizontalAlign="Left" HeaderText="SUB KATEGORI ASET"  />                                      
                                         <asp:BoundField DataField="ast_jeniaset_desc" ItemStyle-HorizontalAlign="Left" HeaderText="JENIS ASET"  /> 
                                        <asp:BoundField DataField="cas_asset_desc" ItemStyle-HorizontalAlign="Left" HeaderText="KETERANGAN ASET"  />
                                         <asp:BoundField DataField="strqty" HeaderText="KUANTITI" ItemStyle-HorizontalAlign="center"   />   
                                        
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
                        </div>

                    </div>
                </div>
            </div>
            <!-- /.row -->

        
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

