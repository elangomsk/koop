<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../PELABURAN_ANGGOTA/PP_sel_jbb.aspx.cs" Inherits="PP_sel_jbb" %>
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
                        <h1> Selenggara Jadual Bayar Balik </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Pelaburan Anggota</a></li>
                            <li class="active"> Selenggara Jadual Bayar Balik </li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
      
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Kata Kunci Carian <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="Applcn_no" runat="server"  class="form-control validate[optional] uppercase" MaxLength="12" ></asp:TextBox>
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
                                          <asp:Button ID="Button9" runat="server" class="btn btn-primary" Text="Carian" usesubmitbehavior="false" OnClick="btnsrch_Click" />
                                                    <asp:Button ID="Button10" runat="server" class="btn btn-default" Text="Set Semula" usesubmitbehavior="false" OnClick="btn_rstclick"/>
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
                                        <asp:TextBox ID="txtname" runat="server" class="form-control validate[optional,custom[textSp]] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Jumlah Belian (RM)  </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txtjumla" runat="server" class="form-control" ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                   
                                
                           <%-- <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Wilayah / Pejabat </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txtwil" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>--%>
                                 </div>
                                </div>

                              <%--   <div class="row">
                             <div class="col-md-12">
                           
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Cawangan / Jabatan  </label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="txtcaw" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>--%>

                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Jumlah Kumulatif Kena (RM) </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtamaun" runat="server" class="form-control" style="text-align:right;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Tempoh (Bulan)    </label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="txttempoh" runat="server" class="form-control uppercase" MaxLength="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Jumlah Kumulatif Tunggakan (RM) </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox2" runat="server" class="form-control" style="text-align:right;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Jumlah Kumulatif Bayar (RM)  </label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="txttemp" runat="server" class="form-control" style="text-align:right;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>


                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Jumlah Kumulatif Simpanan (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox4" runat="server" class="form-control" style="text-align:right;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">     Jumlah Untung (RM) </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="TextBox3" runat="server" class="form-control" style="text-align:right;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Baki Kumulatif Pelaburan (RM) </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox5" runat="server" class="form-control" style="text-align:right;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                   </div>
                                 </div>

                             <div class="box-header with-border">
                            <h3 class="box-title">Kemaskini Maklumat Jadual Bayar Balik  </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                              <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                    <%--  <div class="row" style="overflow:auto;">--%>
           <div class="col-md-12 box-body">
                                   <%--<asp:GridView Id="example1" runat="server" class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="Small" Width="100%" AllowPaging="true" PageSize="25" OnPageIndexChanging="gvSelected_PageIndexChanging">--%>
               <asp:GridView ID="gvSelected" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="100" ShowFooter="false" GridLines="None"  OnPageIndexChanging="gvSelected_PageIndexChanging" OnRowDataBound="gvEmp_RowDataBound">
                   <PagerStyle CssClass="pager" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="TARIKH KENA">
                                            <ItemStyle HorizontalAlign="Center" />    
                                            <ItemTemplate>  
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("p_dt", "{0:dd/MM/yyyy}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TARIKH BAYAR">  
                                                <ItemStyle HorizontalAlign="Center" />   
                                            <ItemTemplate>  
                                                <asp:TextBox ID="Label2" CssClass="form-control datepicker mydatepickerclass" style="text-align:right;"  Text='<%# Bind("ap_dt", "{0:dd/MM/yyyy}") %>' runat="server" />
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BIAYA KENA (RM)">
                                                <ItemStyle HorizontalAlign="Right"/>    
                                            <ItemTemplate>  
                                                <asp:TextBox ID="bk" CssClass="form-control" style="text-align:right;"  Text='<%# Bind("p_amt","{0:n}") %>' runat="server" />
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BIAYA BAYAR (RM)">  
                                                <ItemStyle HorizontalAlign="Right" /> 
                                            <ItemTemplate>  
                                                <asp:TextBox ID="b_bayar" CssClass='form-control txtAmount_bk' runat="server" Text='<%# Bind("ap_amt","{0:n}") %>' OnTextChanged="txtW_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="TUNGGAKAN / LEBIH (RM)">  
                                                 <ItemStyle HorizontalAlign="Right" />     
                                            <ItemTemplate>  
                                                <asp:TextBox ID="Label6" CssClass='form-control' runat="server" Text='<%#  Bind("le_amt","{0:n}") %>'></asp:TextBox>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="SIMPANAN KENA (RM)">  
                                                 <ItemStyle HorizontalAlign="Right" />     
                                            <ItemTemplate>  
                                                <asp:TextBox ID="s_kena" CssClass='form-control ' runat="server" Text='<%# Bind("sa_amt","{0:n}") %>'></asp:TextBox>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="SIMPANAN BAYAR (RM)">  
                                                 <ItemStyle HorizontalAlign="Right" />     
                                            <ItemTemplate>  
                                                <asp:TextBox ID="s_bayar" CssClass='form-control' runat="server" Text='<%# Bind("as_amt","{0:n}") %>'></asp:TextBox>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="TUNGGAKAN / LEBIH (RM)">  
                                                 <ItemStyle HorizontalAlign="Right" />     
                                            <ItemTemplate>  
                                                <asp:TextBox ID="s_tun_lebih" CssClass='form-control ' runat="server" Text='<%# Bind("sle_amt","{0:n}") %>'></asp:TextBox>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="JUMLAH BAYARAN (RM)">  
                                                 <ItemStyle HorizontalAlign="Right" />     
                                            <ItemTemplate>  
                                                <asp:TextBox ID="j_bay" CssClass='form-control ' runat="server" Text='<%# Bind("tp_amt","{0:n}") %>' OnTextChanged="txtW_TextChanged1" AutoPostBack="true"></asp:TextBox>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="BAKI PEMBIAYAAN (RM)">  
                                                 <ItemStyle HorizontalAlign="Right" />     
                                            <ItemTemplate>  
                                                <asp:TextBox ID="b_pemb" CssClass='form-control' runat="server" Text='<%# Eval("bal_amt","{0:n}") %>'></asp:TextBox>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JUMLAH SIMPANAN (RM)">  
                                                 <ItemStyle HorizontalAlign="Right" />     
                                            <ItemTemplate>  
                                                <asp:TextBox ID="j_simpan" CssClass='form-control' runat="server" ></asp:TextBox>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="HARI LEWAT">   
                                                <ItemStyle HorizontalAlign="Right" />    
                                            <ItemTemplate>  
                                                <asp:TextBox ID="h_lewat" CssClass='form-control' runat="server" Text='<%# Eval("dlate") %>'></asp:TextBox>
                                                <asp:Label ID="seqno" Visible="false" runat="server" Text='<%# Eval("seq_no") %>'></asp:Label>
                                                <asp:Label ID="appno" Visible="false" runat="server" Text='<%# Eval("app_no") %>'></asp:Label>
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
                                <asp:Button ID="Button6" runat="server" class="btn btn-danger" Text="Simpan" UseSubmitBehavior="false" OnClick="btn_click"/>
                                                        <asp:Button ID="Button7" runat="server" class="btn btn-default" Text="Set Semula" UseSubmitBehavior="false" OnClick="btn_rstclick1"/>
                            </div>
                           </div>
                               </div>
                           <div class="box-body">&nbsp;
                                    </div>

                             <div class="row">
                                   <div class="col-md-12 col-sm-2" style="text-align:center">
                                     <rsweb:ReportViewer ID="Rptviwer_lt" runat="server"></rsweb:ReportViewer>
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





