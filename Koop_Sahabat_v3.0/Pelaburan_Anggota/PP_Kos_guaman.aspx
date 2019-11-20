<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../PELABURAN_ANGGOTA/PP_Kos_guaman.aspx.cs" Inherits="PP_Kos_guaman" %>


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
                        <h1>  Kemaskini Kos Litigasi         </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i> Pelaburan Anggota</a></li>
                            <li class="active">  Kemaskini Kos Litigasi      </li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
      
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Pelanggan </h3>
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
                                       <asp:Button ID="Button3" runat="server" class="btn btn-primary" Text="Carian" usesubmitbehavior="false" OnClick="btnsrch_Click" />
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
                                        <asp:TextBox ID="MP_Nama" runat="server" class="form-control validate[optional,custom[textSp]] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  No KP Baru </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="MP_kpbaru" runat="server" class="form-control validate[optional] uppercase" MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                                <%-- <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Wilayah / Pejabat </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="MP_wilayah" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Cawangan / Jabatan   </label>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Amaun Pengeluaran (RM) </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="MP_amt" runat="server" class="form-control" style="text-align:right;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Tempoh (Bulan)   </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="MP_duration" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>


                             <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Kos Guaman </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                 

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Panel Peguam <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox6" runat="server" class="form-control validate[optional] uppercase" MaxLength="150"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox1_id" Visible="false" runat="server" class="form-control validate[required] uppercase" MaxLength="150"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Jenis Tindakan <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddl_tindakan" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">    Caj Guaman (RM) <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="TextBox5" runat="server" class="form-control validate[optional,custom[number]]" style="text-align:right;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Tarikh Kemaskini <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                      <div class="input-group">
                                                                   
                                                       <asp:TextBox ID="TextBox7" runat="server"  class="form-control validate[optional] datepicker mydatepickerclass" placeholder="DD/MM/YYYY" ></asp:TextBox>
                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
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
                                 <asp:Button ID="Button2" runat="server" Visible="false" class="btn btn-danger" Text="Kemaskini" OnClick="Click_update" />                                                        
                                                    <asp:Button ID="Button5" runat="server" class="btn btn-danger" Text="Tambah" usesubmitbehavior="false" OnClick="Click_insert" />
                                 <asp:Button ID="Button1" runat="server" CssClass="btn btn-default" Text="Set Semula" usesubmitbehavior="false" OnClick="btnrst_Click" />
                            </div>
                           </div>
                               </div>
                           <div class="box-body">&nbsp;</div>

                            <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                    <%--  <div class="row" style="overflow:auto;">--%>
           <div class="col-md-12 box-body">
                                   <%--<asp:GridView Id="example1" runat="server" class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="Small" Width="100%" AllowPaging="true" PageSize="25" OnPageIndexChanging="gvSelected_PageIndexChanging">--%>
               <asp:GridView ID="gvSelected" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None"  OnPageIndexChanging="gvSelected_PageIndexChanging" >
                   <PagerStyle CssClass="pager" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="BIL">
                                                <ItemStyle HorizontalAlign="Center" />  
                                               <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="150"/>
                                            <asp:Label ID="app_no" Visible="false" runat="server" Text='<%# Bind("lfe_applcn_no") %>'></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PANEL PEGUAM">
                                                <ItemStyle Font-Bold Font-Underline />   
                                            <ItemTemplate>  
                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="AddPizza_Click"
                                CommandArgument='<%# Eval("lfe_crt_dt") %>' Text='<%# Eval("lfe_lawyer_name") %>' OnClick="get_gridvalue"></asp:LinkButton>
                                            
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JENIS TINDAKAN">  
                                            <ItemTemplate>  
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("tindakan_desc") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="TARIKH BAYARAN">
                                                <ItemStyle HorizontalAlign="Center" />    
                                            <ItemTemplate>  
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("lfe_pay_dt", "{0:dd/MM/yyyy}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CAJ GUAMAN (RM)">  
                                                <ItemStyle HorizontalAlign="Center" /> 
                                            <ItemTemplate>  
                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("lfe_legal_fee_amt","{0:n}") %>'></asp:Label>  
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






