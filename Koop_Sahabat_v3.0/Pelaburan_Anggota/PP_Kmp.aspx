<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../Pelaburan_Anggota/PP_Kmp.aspx.cs" Inherits="PP_Kmp" %>

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
                        <h1>  Kemaskini Maklumat Pengeluaran</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i> Kemaskini Maklumat Pengeluaran </a></li>
                            <li class="active">Kemaskini Maklumat Pengeluaran</li>
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
                            <h3 class="box-title">Maklumat Pembahagian Pengeluaran</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        
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
                 <asp:GridView ID="gvSelected" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="true" GridLines="None"  OnPageIndexChanging="gvSelected_PageIndexChanging"
                  onrowdatabound="gvEmp_RowDataBound">
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
                                                <asp:Label ID="Label2" class="uppercase" runat="server" Text='<%# Bind("pha_name") %>' ></asp:Label>   
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
                                                <asp:TextBox ID="Label5" class="form-control" MaxLength="12" runat="server" Text='<%# Bind("pha_bank_acc_no") %>' /> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="NAMA BANK">   
                                            <ItemTemplate>  
                                            <asp:DropDownList ID="Bank_details" style="width:100%;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="TARIKH BAYARAN">  
                                                <ItemTemplate> 
                                                       <div class="input-group">
                                                <asp:TextBox ID="Label10" class="form-control datepicker mydatepickerclass" placeholder="DD/MM/YYYY" style="width:100%;" runat="server" Text='<%# Bind("pha_pay_dt", "{0:dd/MM/yyyy}") %>' /> 
                                                             <span class="input-group-addon" ><i class="fa fa-calendar"></i></span>
                                             </div>
                                            </ItemTemplate> 
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CARA BAYARAN">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label_cb" runat="server" Text='<%# Bind("KETERANGAN") %>'></asp:Label>
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
                              <asp:Button ID="Button7" runat="server" class="btn btn-danger" Text="Simpan" OnClick="update_click" />
                                                        <asp:Button ID="Button3" runat="server" class="btn btn-default" Text="Kembali"  UseSubmitBehavior="false" OnClick="clk_bak" />
                                 
                            </div>
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



