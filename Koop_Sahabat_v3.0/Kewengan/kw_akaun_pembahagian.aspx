<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Kewengan/kw_akaun_pembahagian.aspx.cs" Inherits="kw_akaun_pembahagian" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server" ScriptMode="Release">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
         <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1><asp:Label ID="ps_lbl1" runat="server"></asp:Label></h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i><asp:Label ID="ps_lbl2" runat="server"></asp:Label></a></li>
                            <li class="active"><asp:Label ID="ps_lbl3" runat="server"></asp:Label></li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
  
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title"><asp:Label ID="ps_lbl4" runat="server"></asp:Label></h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                         <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl5" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                     <asp:DropDownList id="Tahun_kew" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                       <asp:Button ID="Button5" runat="server" class="btn btn-primary sub_btn" Text="Carian" OnClick="clk_cerian"  UseSubmitBehavior="false" />
                                <asp:Button ID="Button3" runat="server" class="btn btn-default" Text="Set Semula" OnClick="btn_reset"  UseSubmitBehavior="false" />
                                       <asp:Button ID="Button8" runat="server" class="btn btn-default" Text="Kempali" OnClick="Back_to_profile" UseSubmitBehavior="false" />
                                      </div>
                                 </div>
                                </div>
                            <div class="row" style="display:none;">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl6" runat="server"></asp:Label> <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                       <asp:TextBox ID="Tk_akhir"  runat="server" class="form-control validate[optional] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
                                             <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                        <asp:TextBox ID="TextBox1"  runat="server" class="form-control validate[optional]" style=" display:none;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                
                                 </div>
                         </div>
                               <div class="box-body">&nbsp;</div>
                             <div class="box-header with-border">
                            <h3 class="box-title">Untung / (Rugi) Bersih Tahun Semasa</h3>
                        </div>
                         
                             <div class="col-md-12 box-body">
                                   <asp:GridView ID="gv_refdata" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="20">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="KOD AKAUN">
                                                            <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_1" runat="server" Text='<%# Eval("a3") %>'></asp:Label>                                                              
                                                            </ItemTemplate>
                                                        </asp:TemplateField>                                                       
                                                        <asp:TemplateField HeaderText="Keterangan">
                                                            <ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_3" runat="server" Text='<%# Eval("a2") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>  
                                                        <asp:TemplateField HeaderText="AMAUN (RM)" HeaderStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_4" runat="server" Text='<%# Eval("a4","{0:C}").Replace("$","").Replace("RM","") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>                                                 
                                                    </Columns>
                                                </asp:GridView>
               </div>
          
                             
                            <div class="box-body">&nbsp;</div>
                           
                             <div class="col-md-12">
                                                <div id="Div1" class="nav-tabs-custom" role="tabpanel" runat="server">
                                                 <!-- Nav tabs -->
                                                            <ul class="s1 nav nav-tabs" role="tablist">
                                                            <li id="pp6" runat="server" class="active" style="font-weight:bold;"><a href="#ContentPlaceHolder1_p6" aria-controls="p6" role="tab" data-toggle="tab"><asp:Label Visible="false" ID="ps_lbl9" runat="server"></asp:Label> Pembahagian</a></li>
                                                            </ul>
                                                            <!-- Tab panes -->
                                                            <div class="tab-content">
                                                             <div role="tabpanel" class="tab-pane active" runat="server" id="p6">
                                           <%--  <div class="col-md-12 table-responsive uppercase" style="overflow:auto;">--%>
                                            <asp:GridView ID="grvStudentDetails" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="20" onrowdatabound="gvEmp_RowDataBound" OnRowDeleting="grvStudentDetails_RowDeleting" ShowFooter="True">
                                                        <Columns>
                                                           
                                                            <asp:TemplateField HeaderText="KOD AKAUN">
                                                                 <ItemStyle Width="20%"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="Col1" style="width:100%; font-size:13px;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="changed_kod" class="form-control select2 validate[optional]"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                 <FooterStyle HorizontalAlign="Left" />
                                                                <FooterTemplate>
                                                                    <asp:Button ID="ButtonAdd" runat="server" Text="Tambah Baru" style="width:50%;" CssClass="btn btn-success" OnClick="ButtonAdd_Click" />
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="KETERENGAN">
                                                                <ItemTemplate>
                                                                   <asp:TextBox ID="Col2" CssClass="form-control uppercase" ReadOnly="true" runat="server" Text='<%# Eval("col2") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="%">
                                                                <ItemTemplate>
                                                                   <asp:TextBox ID="Col3" CssClass="form-control uppercase " MaxLength="2" OnTextChanged="QtyChanged" AutoPostBack="true" runat="server" Text='<%# Eval("col3") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="AMAUN (RM)" HeaderStyle-HorizontalAlign="Right">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <ItemTemplate>
                                                                   <asp:TextBox ID="Col4" style="text-align:right;" CssClass="form-control" OnTextChanged="QtyChanged1" AutoPostBack="true" placeholder="0.00" Text='<%# Eval("col4","{0:n}") %>'  runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                              
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="AMAUN PEMBAHAGIAN (RM)" HeaderStyle-HorizontalAlign="Right">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <ItemTemplate>
                                                                   <asp:TextBox ID="Col5" ReadOnly="true" style="text-align:right;" CssClass="form-control " placeholder="0.00" Text='<%# Eval("col5","{0:n}") %>' runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                 <FooterTemplate>
                                                                    <asp:TextBox ID="lblTotal2" CssClass="form-control" style="text-align:right; font-weight:bold;" ReadOnly="true" runat="server" ></asp:TextBox>
                                                                    </FooterTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="NOTA JUSTIFIKASI">
                                                                <ItemTemplate>
                                                                   <asp:TextBox ID="Col6" CssClass="form-control uppercase" runat="server" Text='<%# Eval("col6") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                           <%-- <asp:CommandField ShowDeleteButton="True" />--%>
                                                        </Columns>
                                                       <%-- <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="White" />--%>
                                                    </asp:GridView> 
                                                <%-- </div> --%>
                                                                 <br />
                                                                  <div class="row">
                                                                         <div class="col-md-12">
                                                                       <div class="col-md-6 box-body">&nbsp;</div>
                                                                             
                                                                              <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-6 control-label">BAKI <asp:Label ID="cyp_txt" runat="server"></asp:Label> (RM)</label>                                         
                                                                                <div class="col-sm-6">
                                                                                  <asp:TextBox ID="TextBox17" Placeholder="0.00"  runat="server" Style="text-align:right; width:235px;" class="form-control"></asp:TextBox> 
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             <div class="col-md-2 box-body">&nbsp;</div>
                                                                             </div>
                                                                     </div> 
                                                  </div>
                                                               
                                                 </div>
                                                 
                                                 </div>
                                        
                                          <div class="row">
                                                <div class="col-md-12" style="text-align:center;">
                                                    <div class="body collapse in">
                                                        <asp:TextBox ID="ver_id" Visible="false" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                                                         <asp:TextBox ID="get_id" Visible="false" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                                                       <asp:Button ID="Button4" runat="server" class="btn btn-danger sub_btn" Text="Simpan" OnClick="clk_submit"  UseSubmitBehavior="false" />
                                                        <%--<asp:Button ID="Button1" runat="server" class="btn btn-danger" Text="JANA JURNAL & POST KE LEJER" OnClick="clk_pstledger"  UseSubmitBehavior="false" />--%>
                                                        <asp:Button ID="Button2" runat="server" class="btn btn-warning sub_btn" Text="JANA LAPORAN" OnClick="clk_prnt" UseSubmitBehavior="false" />
                                                        
                                                    </div>
                                                </div>
                                            </div>
                                        <br />
                                        <br />                                           
                                               <div class="row">
                                                        <div class="col-md-12 col-sm-2" style="text-align: center; line-height:10px; overflow: auto; line-height:13px;">
                                                            <rsweb:ReportViewer ID="Rptviwerlejar" runat="server" width="100%" Height="100%" SizeToReportContent="True"></rsweb:ReportViewer>
                                                            <asp:Label runat="server" ID="ReportErrorMessage" Visible="false" CssClass="report-error-message"></asp:Label>
                                                        </div>
                                                    </div>                                          
                                             </div>
                            <div class="box-body">&nbsp;</div>
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
                <Triggers>
               <asp:PostBackTrigger ControlID="Button2"  />               
           </Triggers>
    </asp:UpdatePanel>
</asp:Content>

