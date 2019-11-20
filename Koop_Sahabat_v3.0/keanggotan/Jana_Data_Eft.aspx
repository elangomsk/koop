<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../keanggotan/Jana_Data_Eft.aspx.cs" Inherits="Jana_Data_Eft" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server" ScriptMode="Release">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1><asp:Label ID="ps_lbl1" runat="server"></asp:Label>  </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i><asp:Label ID="ps_lbl2" runat="server"></asp:Label>   </a></li>
                            <li class="active"> <asp:Label ID="ps_lbl3" runat="server"></asp:Label>   </li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
       <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>--%>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title"> <asp:Label ID="ps_lbl4" runat="server"></asp:Label>    </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            


                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl7" runat="server"></asp:Label>   <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddbat" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl8" runat="server"></asp:Label>  </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddwil" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">  <asp:Label ID="ps_lbl9" runat="server"></asp:Label>   </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtbeftname" runat="server" class="form-control" style="text-transform:uppercase;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>


                             <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                 <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Jana Fail EFT" UseSubmitBehavior="false" onclick="ibtn_download_Click" />
                                                            <asp:Button ID="Button4" runat="server" class="btn btn-danger" Text="Cetak Fail Teks" Visible="false" />
                                                            <asp:Button ID="Button5" runat="server" class="btn btn-default" Text="Batal" />
                                 
                            </div>
                           </div>
                               </div>
                           <div class="box-body">&nbsp;
                                    </div>

                              <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                    <%--  <div class="row" style="overflow:auto;">--%>
           <div class="col-md-12 box-body">
                                    <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="100" ShowFooter="false" GridLines="None" EmptyDataText = "No files uploaded">
        <Columns>
                                            <asp:TemplateField HeaderText="BIL">  
                                            <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                            </asp:TemplateField>
                                                      
                                            <asp:TemplateField HeaderText="NAMA KELOMPOK"> <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                            <ItemTemplate>
                                            <asp:Label ID="lbl2" runat="server" Text='<%# Eval("Batch_name") %>'></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                                          
                                            <asp:TemplateField HeaderText="FAIL COUNT">
                                            <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                            <ItemTemplate>
                                            <asp:Label ID="lbl5" runat="server" Text='<%# Eval("File_count") %>'></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="TINDAKAN">
                                            <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                            <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkView1" OnClick="DownloadFiles">
                                            <asp:Label ID="lbl3" runat="server" Text='Download'></asp:Label>
                                            </asp:LinkButton>&nbsp; | &nbsp;
                                            <asp:LinkButton runat="server" ID="lnkView2" OnClick="lnkView_Click2" OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;">
                                            <asp:Label ID="lbl4" runat="server" Text='Delete'></asp:Label>
                                            </asp:LinkButton>
                                            </ItemTemplate>
                                            </asp:TemplateField> 
        </Columns>
                                        <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />                                                       
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
               </div>
          </div>

                            <div class="box-body">&nbsp;</div>
                                    </div>  
                        </div>

                    </div>
                </div>
            </div>
            <!-- /.row -->
           <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
        
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
   
</asp:Content>
