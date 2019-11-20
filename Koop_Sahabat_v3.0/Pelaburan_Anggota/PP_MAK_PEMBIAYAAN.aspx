﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../Pelaburan_Anggota/PP_MAK_PEMBIAYAAN.aspx.cs" Inherits="PP_MAK_PEMBIAYAAN" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" style="height: 895px;">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1> Semakan Maklumat Pembiayaan</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i> Pelaburan Anggota </a></li>
                            <li class="active">Semakan Maklumat Pembiayaan</li>
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
                            <h3 class="box-title">Maklumat Pembiayaan</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Mula <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="f_date" runat="server" class="form-control datepicker mydatepickerclass"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                               <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Akhir <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="t_date" runat="server" class="form-control datepicker mydatepickerclass"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  </div>
                                </div>

                                
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jenis Pembiayaan <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                          <asp:DropDownList style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" ID="DD_Pelaburan">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Status Pembiayaan <%--<span style="color: Red">*</span>--%></label>
                                    <div class="col-sm-8">
                                           <asp:DropDownList style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" ID="ST_Pelaburan">
                                                        <asp:ListItem Value="" Text="--- PLIH ---"></asp:ListItem>
                                                        <asp:ListItem Text="TERTUNGGAK" Value="T"></asp:ListItem>
                                                        <asp:ListItem Text="NORMAL" Value="N"></asp:ListItem>
                                                        <asp:ListItem Text="SELESAI" Value="S"></asp:ListItem>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tempoh Tunggakan (Bulan)</label>
                                    <div class="col-sm-8">
                                           <div class="col-md-5 col-sm-2 text-left">
                                                        <asp:TextBox ID="tt_v1" runat="server" class="form-control validate[optional,custom[number]]" MaxLength="5"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2 col-sm-2" > 
                                                       <label>Hingga</label>
                                                    </div>
                                                    <div class="col-md-5 col-sm-2">
                                                        <asp:TextBox ID="tt_v2" runat="server" class="form-control validate[optional,custom[number]]" MaxLength="5"></asp:TextBox>
                                                    </div>
                                    </div>
                                </div>
                            </div>
                                                            
                                  </div>
                         </div>
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                             <asp:Button ID="Button4" runat="server" class="btn btn-primary" Text="Carian" UseSubmitBehavior="false" OnClick="btnsrch_Click"/>
                                                        <asp:Button ID="Button8" runat="server" class="btn btn-default" Text="Set Semula" usesubmitbehavior="false" OnClick="btn_rstclick"/>
                                 
                            </div>
                           </div>
                               </div>
                               <div class="box-header with-border">
                            <h3 class="box-title">Senarai Maklumat Pelanggan <asp:Label CssClass="uppercase" runat="server" ID="s_pel"></asp:Label></h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                             <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                              <asp:GridView ID="gvSelected" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None"  OnPageIndexChanging="gvSelected_PageIndexChanging">
                                  <PagerStyle CssClass="pager" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="BIL">
                                                
                                                <ItemStyle HorizontalAlign="Center" />  
                                               <ItemTemplate>                                           
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="150"/>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="NO PERMOHONAN">
                                            <ItemStyle HorizontalAlign="Center" />    
                                            <ItemTemplate>  
                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("app_applcn_no") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NO KP BARU">  
                                                <ItemStyle HorizontalAlign="Center" />   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("app_new_icno") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="NAMA">
                                                <ItemStyle HorizontalAlign="Left" />    
                                            <ItemTemplate>  
                                            <asp:LinkButton runat="server" ID="lnkView" OnClick="lnkView_Click">
                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("app_name") %>'></asp:Label>
                                                </asp:LinkButton>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JUMLAH BELIAN (RM)">  
                                                <ItemStyle HorizontalAlign="Right" /> 
                                            <ItemTemplate>  
                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("app_loan_amt","{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="ANSURAN BULANAN (RM)">   
                                                <ItemStyle HorizontalAlign="Right" /> 
                                            <ItemTemplate>  
                                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("app_installment_amt","{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="TEMPOH (BULAN)">  
                                                 <ItemStyle HorizontalAlign="Center" />  
                                            <ItemTemplate>  
                                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("appl_loan_dur") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BULAN TERTUNGGAK">   
                                                <ItemStyle HorizontalAlign="Right" /> 
                                            <ItemTemplate>  
                                                <asp:Label ID="Label8" runat="server" Text='<%# Eval("BT","{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JUMLAH TUNGGAKAN (RM)"> 
                                                <ItemStyle HorizontalAlign="Right" />   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label9" runat="server" Text='<%# Eval("app_backdated_amt","{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BAKI PELABURAN (RM)">   
                                                <ItemStyle HorizontalAlign="Right" /> 
                                            <ItemTemplate>  
                                                <asp:Label ID="Label10" runat="server" Text='<%# Eval("app_bal_loan_amt","{0:n}") %>'></asp:Label>  
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
             </ContentTemplate>
    </asp:UpdatePanel>
        
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
   
</asp:Content>


