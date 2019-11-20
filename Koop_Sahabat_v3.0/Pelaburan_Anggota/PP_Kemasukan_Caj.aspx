<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../PELABURAN_ANGGOTA/PP_Kemasukan_Caj.aspx.cs" Inherits="PP_Kemasukan_Caj" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" style="height: 895px;">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>   Kemasukan Caj        </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i> Pelaburan Anggota </a></li>
                            <li class="active">  Kemasukan Caj       </li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
      
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title"> Maklumat Pelanggan </h3>

                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  No Permohonan <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="Applcn_no" runat="server"  class="form-control validate[optional] uppercase" MaxLength="11" ></asp:TextBox>
                                         <asp:Panel ID="autocompleteDropDownPanel" runat="server" ScrollBars="Auto" Height="150px"
                                                            Font-Size="Medium" HorizontalAlign="Left" Wrap="False" />
                                                        <cc1:AutoCompleteExtender ServiceMethod="GetCompletionList" MinimumPrefixLength="1"
                                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="Applcn_no"
                                                            ID="AutoCompleteExtender1" CompletionListElementID="autocompleteDropDownPanel"
                                                            CompletionListCssClass="form-control uppercase" runat="server" FirstRowSelected="false">
                                                        </cc1:AutoCompleteExtender>
                                    </div>
                                </div>
                            </div>
                             <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-8">
                                           <asp:Button ID="Button3" runat="server" class="btn btn-primary" Text="Carian" usesubmitbehavior="false" OnClick="btnsrch_Click"/>
                                                    <asp:Button ID="Button6" runat="server" class="btn btn-default" Text="Set Semula" usesubmitbehavior="false" OnClick="btnreset_Click" />
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Nama  </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="MP_nama" runat="server" class="form-control validate[optional,custom[textSp]] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  No KP Baru</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="MP_icno" runat="server" class="form-control validate[optional,custom[phone]]" MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                               <%--  <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Wilayah / Pejabat</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="MP_wilayah" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Cawangan / Jabatan  </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="MP_cawangan" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>--%>

                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Amaun Pengeluaran (RM) </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox1" runat="server" style="text-align:right;" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Tempoh (Bulan)    </label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="TextBox2" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                 


                             <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Caj </h3>

                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Caj Pengeposan (RM)  </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox3" runat="server" style="text-align:right;" OnTextChanged="vv1_changed" AutoPostBack="true" class="form-control validate[optional,custom[number]]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">    Caj Lewat (RM)    </label>
                                    <div class="col-sm-8">
                                          <div class="col-sm-8">
                                          <asp:TextBox ID="MPP_jb" runat="server" style="text-align:right;" OnTextChanged="vv2_changed" AutoPostBack="true" class="form-control validate[optional,custom[number]]"></asp:TextBox>
                                                        <asp:TextBox ID="hid_id" runat="server" class="form-control" Visible="false"></asp:TextBox>
                                              </div>
                                          <div class="col-sm-4">
                                                <asp:Button ID="Button2" runat="server" class="btn btn-danger" UseSubmitBehavior="false" Text="Tambah Baru" OnClick="tampah_click"/>
                                                        <asp:Button ID="Button5" Visible="false" runat="server" class="btn btn-danger" UseSubmitBehavior="false" Text="Kemaskini" OnClick="update_click" />
                                              </div>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                    <%--  <div class="row" style="overflow:auto;">--%>
           <div class="col-md-12 box-body">
                                   <%--<asp:GridView Id="example1" runat="server" class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="Small" Width="100%" AllowPaging="true" PageSize="25" OnPageIndexChanging="gvSelected_PageIndexChanging">--%>
               <asp:GridView ID="gvSelected" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="1000000" ShowFooter="false" GridLines="None"  OnPageIndexChanging="gvSelected_PageIndexChanging">
                   <PagerStyle CssClass="pager" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="BIL">
                                               
                                                <ItemStyle HorizontalAlign="Center" />  
                                               <ItemTemplate>
                                            <%--<asp:CheckBox ID="chkSelect" runat="server" />--%>
                                            <asp:Label ID="app_icno" Visible="false" runat="server" Text='<%# Eval("caj_applcn_no") %>'></asp:Label> 
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="150"/>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TARIKH REKOD">
                                            <ItemStyle HorizontalAlign="Center" />    
                                            <ItemTemplate>  
                                            <asp:LinkButton runat="server" ID="lnkView" OnClick="lnkView_Click" Font-Bold Font-Underline>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("caj_crt_dt","{0:dd/MM/yyyy}") %>'></asp:Label>  
                                                </asp:LinkButton>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CAJ PENGEPOSAN (RM)"> 
                                                <ItemStyle HorizontalAlign="Right" />     
                                            <ItemTemplate>  
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("caj_postage","{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="CAJ LEWAT (RM)">
                                                <ItemStyle HorizontalAlign="Right" />    
                                            <ItemTemplate>  
                                            <asp:label ID="Label4" runat="server" Text='<%# Bind("caj_late_pay","{0:n}") %>' ></asp:label>   
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NAMA PEGAWAI">  
                                                <ItemStyle HorizontalAlign="Center" /> 
                                            <ItemTemplate>  
                                                <asp:label ID="Label5" runat="server" Text='<%# Bind("caj_crt_id") %>'></asp:label> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                 <asp:TemplateField>
                                                 <ItemStyle HorizontalAlign="Center" /> 
                                                <ItemTemplate>
                                                    <asp:RadioButton ID="rbtnSelect" runat="server" AutoPostBack="true" onclick = "RadioCheck(this);"/>
                                                    <asp:Label ID="app_dt" Visible="false" runat="server" Text='<%# Eval("caj_crt_dt") %>'></asp:Label>
                                                   
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
                            <div class="col-md-12 box-body" style="text-align:center;">
                                <asp:Button ID="Button7" runat="server" class="btn btn-danger" Text="Hapus" UseSubmitBehavior="false" OnClick="hapus_click" />                                                    
                                                    <asp:Button ID="Button1" runat="server" class="btn btn-warning" UseSubmitBehavior="false" Text="Cetak" OnClick="click_print"/>                                                        
                            </div>
                           </div>
                               </div>
                           <div class="box-body">&nbsp;</div>
                             <div class="row">
                                   <div class="col-md-12 col-sm-2" style="text-align:center">
                                     <rsweb:ReportViewer ID="Rptviwer_lt" runat="server" SizeToReportContent="true" AsyncRendering="true"></rsweb:ReportViewer>
                                     <asp:Label runat="server" ID="ReportErrorMessage" Visible="false" CssClass="report-error-message"></asp:Label>
                                    </div>
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





