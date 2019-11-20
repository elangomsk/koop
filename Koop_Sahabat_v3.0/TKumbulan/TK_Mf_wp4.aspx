﻿<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="TK_Mf_wp4.aspx.cs" Inherits="TKumbulan_TK_Mf_wp4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManagerCalendar" AsyncPostBackTimeOut="72000" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
   
     <!-- Content Wrapper. Contains page content -->
       
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>Urusan Bayaran WP4 (Arahan Pembayaran Tabung Kumpulan)</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Tabung Kumpulan</a></li>
                            <li class="active">Muatnaik Fail WP4</li>
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
                            <h3 class="box-title">Maklumat Muatnaik Fail WP4</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                         <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                             
                             <%-- <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jenis Produk <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                          <asp:DropDownList ID="DropDownList1" runat="server" class="form-control validate[optional] uppercase"></asp:DropDownList>
                                       
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                   
                                  
                                </div>
                            </div>
                                 </div>
                                </div>--%>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama Fail <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                     <asp:FileUpload ID="FileUpload1" runat="server" />
                                                     <asp:Label ID="Label1" runat="server" Text="(  Nota : Sila pilih fail format Excel (.xlsx) sahaja. )" ForeColor="Red" ></asp:Label>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                          <asp:Button ID="Button1" runat="server" class="btn btn-danger" Text="Muatnaik " OnClick="btnok_Click"/>
                                                            <asp:Button ID="Button2" runat="server" class="btn btn-danger" Text="Jana Baucer" Visible="false"/>
                                                            <asp:Button ID="Button3" runat="server" class="btn btn-danger" Text="Cetak" Visible="false"/>
                                                            <asp:Button ID="Button4" runat="server" class="btn btn-default" UseSubmitBehavior="false" Text="Batal" />
                                </div>
                            </div>
                                 </div>
                                </div>
                            <%-- <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama Kelompok </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txtbname" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 
                                 </div>
                                </div>--%>
                            
                          <%--    <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                  <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
                <div id="divImage" class="text-center" style="display:none; padding-top: 30px; font-weight:bold;">
                     <asp:Image ID="img1" runat="server" ImageUrl="../dist/img/LoaderIcon.gif" />&nbsp;&nbsp;&nbsp;Processing Please wait ... </div>  
                                </div>
                                 </div>
                                  </div>--%>
                           
                            
                              <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                      <%--<div class="row" >--%>
           <div class="col-md-12 box-body">
                                          <asp:GridView ID="GridView1" CellPadding="8" CellSpacing="2" Width="100%" Height="100%" runat="server" AutoGenerateColumns="false" EmptyDataText = "No files uploaded">
        <Columns>
             <asp:TemplateField HeaderText="BIL" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                                    ItemStyle-Width="150" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
            <asp:BoundField DataField="st_file_nm" ItemStyle-Width="80%" HeaderText="NAMA FAIL" />
            <asp:TemplateField HeaderText="TINDAKAN" ItemStyle-Width="17%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                       <asp:Label ID="lbl1" runat="server" Text='<%# Eval("st_file_nm") %>' Visible="false"></asp:Label>
                   <asp:LinkButton runat="server" ID="lnkView11" OnClick="lnkView_Click11" Font-Bold Font-Underline>
                                                                <asp:Label ID="lbl3" runat="server" Text='Download'></asp:Label>
                                                                </asp:LinkButton>
                    &nbsp;&nbsp;|&nbsp;&nbsp;
                       <asp:LinkButton runat="server" ID="LinkButton1" OnClick="lnkView_Click12" Font-Bold Font-Underline>
                                                                <asp:Label ID="Label2" runat="server" Text='Hapus'></asp:Label>
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
                                <div class="box-body">&nbsp;
                                    </div>
                      <%--   <div class="row" id="car" runat="server">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                
                                <asp:Button ID="Button3" runat="server" Visible="false" class="btn btn-default" Text="Tutub"   OnClick="btn_tutub"  UseSubmitBehavior="false" />
                            </div>
                           </div>
                               </div> 
                             <div class="box-body">&nbsp;
                                    </div>    --%>                     
                        </div>

                    </div>
                </div>
            </div>
            <!-- /.row -->
            
           </ContentTemplate>
                    <Triggers>            
                        <asp:PostBackTrigger ControlID="Button1"  />
                 <asp:PostBackTrigger ControlID="GridView1"  />
                 </Triggers>       
    </asp:UpdatePanel>
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
</asp:Content>
