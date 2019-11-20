<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="TK_Km_Bayaran.aspx.cs" Inherits="TKumbulan_TK_Km_Bayaran" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManagerCalendar" AsyncPostBackTimeOut="72000" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
   
     <!-- Content Wrapper. Contains page content -->
       
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>Kemaskini Maklumat Bayaran WP4</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Tabung Kumpulan</a></li>
                            <li class="active">Kemaskini Maklumat Bayaran WP4</li>
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
                                               <h3 class="box-title">Maklumat</h3>
                                               </div>                                     
                        <!-- /.box-header -->
                        <!-- form start -->
                         <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                             
                              <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama Kelompok <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                          <asp:DropDownList ID="ddljp" runat="server" class="form-control validate[optional] select2 uppercase"></asp:DropDownList>
                                       
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                          <asp:Button ID="btnsrch" runat="server" class="btn btn-success" Text="Carian" OnClick="BindGridview" />
                                    <asp:Button ID="Button1" runat="server" class="btn btn-success" Text="Set Semula" OnClick="clk_reset" />
                                </div>
                            </div>
                                 </div>
                                </div>
                                            
                              <div class="box-body">&nbsp;</div>
                              <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                      <%--<div class="row" >--%>
           <div class="col-md-12 box-body">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  
                                        Width="100%" Height="100%" BackColor="#ffffff"
                                        CellPadding="8" CellSpacing="2" Font-Size="12px" BorderStyle="None" PageSize="2" BorderColor="#fffff" OnRowDataBound="gvUserInfo_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="BIL">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAll" Checked="true" runat="server" Text="&nbsp;BIL" OnCheckedChanged="OnCheckedChanged" AutoPostBack="true" ItemStyle-Width="150" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" Checked="true" runat="server" />
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="150" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="NO KP">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" class="uppercase" runat="server" Text='<%# Bind("mem_new_icno") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="NAMA">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" class="uppercase" runat="server" Text='<%# Bind("mem_name") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="NO ANGGOTA">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label4" class="uppercase" runat="server" Text='<%# Bind("mem_member_no") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CAWANGAN">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label5" class="uppercase" runat="server" Text='<%# Bind("cawangan_name") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PUSAT">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label6" class="uppercase" runat="server" Text='<%# Bind("mem_centre") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="JUMLAH DIVIDEN(RM)" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("wp4_amt","{0:n}") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="NO AKAUN BANK">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="Label8" class="form-control uppercase" Width="100%" MaxLength="12" runat="server" Text='<%# Bind("wp4_bene_acc_no") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BANK">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="Bank_details" runat="server" Width="100%" class="form-control uppercase select2"></asp:DropDownList>
                                                    <%--<asp:TextBox ID="Label9" class="form-control uppercase" runat="server" Text='<%# Bind("wp4_bank_cd") %>' />--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TARIKH KREDIT BAYARAN">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TextBox2" class="form-control datepicker mydatepickerclass" Width="100%" runat="server" Text='<%# DateTime.Now.ToString("dd/MM/yyyy") %>' />
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
                               <hr />
                             <div class="row">
                             <div class="col-md-12">
                           
                                  <div class="col-md-12 box-body">
                                <div class="form-group text-center">
                           <asp:Button ID="btnupdt" runat="server" class="btn btn-danger" UseSubmitBehavior="false" Visible="false" Text="Simpan " OnClick="btnupdt_Click1" />
                                            <asp:Button ID="Button2" runat="server" class="btn btn-default" UseSubmitBehavior="false" Visible="false" Text="Batal" />
                                </div>
                            </div>
                                 </div>
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
                          
    </asp:UpdatePanel>
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
</asp:Content>

