<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../Pelaburan_Anggota/PP_Arahan_pengeluaran.aspx.cs" Inherits="PP_Arahan_pengeluaran" %>

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
                        <h1>  Arahan Pengeluaran</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>  Pelaburan Anggota </a></li>
                            <li class="active"> Arahan Pengeluaran</li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
     <%--  <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>--%>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Pemohon</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Permohonan <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="Applcn_no" runat="server" class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase"
                                                            MaxLength="12"></asp:TextBox>
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
                                     <asp:Button ID="Button4" runat="server" class="btn btn-primary" Text="Carian" usesubmitbehavior="false" OnClick="btnsrch_Click" />
                                                    <asp:Button ID="Button6" runat="server" class="btn btn-default" Text="Set Semula" OnClick="btnreset_Click" />
                                       <asp:Button ID="Button8" runat="server" class="btn btn-default" Text="Kembali"  UseSubmitBehavior="false" OnClick="clk_bak" />
                                       </div>
                                    
                                </div>
                            </div>
                                  </div>
                                </div>

                                
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="MP_nama" runat="server" class="form-control validate[optional,custom[textSp]]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No KP Baru</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="MP_icno" runat="server" class="form-control uppercase" ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <%-- <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Wilayah / Pejabat</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="MP_wilayah" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Cawangan / Jabatan</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="MP_cawangan" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>                                
                                  </div>
                         </div>--%>
                            <div class="box-header with-border">
                            <h3 class="box-title">Maklumat CAJ Dan FI</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                              <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Amaun Pengeluaran (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="CAJ_amaun" style="text-align:right;" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tempoh (Bulan)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="CAJ_tempoh" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>
                              <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Caj Duti Setem (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="CAJ_ds" runat="server" style="text-align:right;" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Premium Takaful (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="CAJ_tkh" runat="server" style="text-align:right;" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>
                            <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Fi Pemprosesan (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="CAJ_fi_p" runat="server" style="text-align:right;" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Fi Semakan kredit (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="CAJ_fi_s" runat="server" style="text-align:right;" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Deposit Sekuriti (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="CAJ_deposit" runat="server" style="text-align:right;" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Keuntungan (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="CAJ_Keuntungan" runat="server" style="text-align:right;" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Lain-lain Caj (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="CAJ_ll" runat="server" style="text-align:right;" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Pembiayaan Bersih (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="CAJ_bersih" runat="server" style="text-align:right;" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>
                              <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Pembahagian Pengeluaran</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                            <div class="row" style="display:none;">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Fasa Pengeluaran <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                        <div class="col-md-2 col-sm-4">
                                                            <asp:RadioButton ID="RadioButton1" runat="server"  Text=" 1"  AutoPostBack="true" 
                                                                    oncheckedchanged="RadioButton1_CheckedChanged"/>
                                                          <%--  <label>Warganegara</label>--%>
                                                            </div>
                                                            <div class="col-md-2 col-sm-5"> 
                                                            <asp:RadioButton ID="RadioButton2" runat="server" Text=" 2"  AutoPostBack="true" 
                                                                    oncheckedchanged="RadioButton2_CheckedChanged" />
                                                          <%--  <label>Bukan Warganegara</label>--%>
                                                            </div>
                                                            <div class="col-md-2 col-sm-6">
                                                            <asp:RadioButton ID="RadioButton3" runat="server" Text=" 3"  AutoPostBack="true" 
                                                                    oncheckedchanged="RadioButton3_CheckedChanged" /> 
                                                            <%--<label>Pemustautin Tetap</label>--%>
                                                            </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jumlah Bayaran (RM) <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="MPP_jb" runat="server" style="text-align:right;" class="form-control validate[optional,custom[number]]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>
                           
                        <div class="box-body">&nbsp;</div>
                                                           <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">                                     
           <div class="col-md-12 box-body">
                <asp:GridView ID="gvSelected" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="true" GridLines="None"  OnPageIndexChanging="gvSelected_PageIndexChanging"  onrowdatabound="gvEmp_RowDataBound">
                    <PagerStyle CssClass="pager" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="BIL">
                                               
                                                <ItemStyle HorizontalAlign="Center" />  
                                               <ItemTemplate>
                                            <%--<asp:CheckBox ID="chkSelect" runat="server" />--%>
                                            <asp:Label ID="app_no" Visible="false" runat="server" Text='<%# Bind("pha_applcn_no") %>'></asp:Label>
                                            <asp:Label ID="seq_no" Visible="false" runat="server" Text='<%# Bind("pha_seqno") %>'></asp:Label>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="150"/>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FASA PEMBAHAGIAN" Visible="false">
                                            <ItemStyle HorizontalAlign="Center" />    
                                            <ItemTemplate>  
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("pha_phase_no") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NAMA PENERIMA">  
                                            <ItemTemplate>  
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("pha_name") %>' ></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="NO DAFTAR PENERIMA">
                                                <ItemStyle HorizontalAlign="Left" />    
                                            <ItemTemplate>  
                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("pha_reg_no") %>' ></asp:Label>    
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NO AKAUN BANK">  
                                                <ItemStyle HorizontalAlign="Center" /> 
                                            <ItemTemplate>  
                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("pha_bank_acc_no") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="NAMA BANK">   
                                            <ItemTemplate>  
                                             <asp:Label ID="Label7" runat="server" Text='<%# Bind("Bank_Name") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CARA BAYARAN">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("KETERANGAN") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="AMAUN (RM)"> 
                                                <ItemStyle HorizontalAlign="Right" />  
                                            <ItemTemplate>  
                                               <asp:Label ID="Label8" runat="server" Text='<%# Bind("pha_pay_amt","{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                            <FooterTemplate>
                                                <asp:Label ID="lblTotal" runat="server"/>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
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
                                <asp:Button ID="Button5" runat="server" class="btn btn-warning" Visible="false" Text="Cetak Arahan Pengeluaran" OnClick="click_print" />
                                                        <asp:Button ID="Button1" runat="server" class="btn btn-warning" Visible="false" Text="Jana Fail EFT" OnClick="genrate_eft" />
                                                        <asp:Button ID="Button3" runat="server" class="btn btn-default" Text="Kembali"  UseSubmitBehavior="false" OnClick="clk_bak" />
                                                        <asp:Button ID="Button2" runat="server" class="btn btn-warning" Visible="false" Text="Jana Jurnal Akaun" OnClick="btncoa_Click" />
                                 
                            </div>
                           </div>
                               </div>
                            <div class="row">
                                   <div class="col-md-12 col-sm-2" style="text-align:center">
                                     <rsweb:ReportViewer ID="Rptviwer_Arahan" runat="server"></rsweb:ReportViewer>
                                     <asp:Label runat="server" ID="ReportErrorMessage" Visible="false" CssClass="report-error-message"></asp:Label>
                                    </div>
                                    </div>
                              </div>
                        </div>

                    </div>
                </div>
            </div>
            <!-- /.row -->
            <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
        
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
   
</asp:Content>



