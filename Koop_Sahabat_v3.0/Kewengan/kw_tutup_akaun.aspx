<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Kewengan/kw_tutup_akaun.aspx.cs" Inherits="kw_tutup_akaun" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        <style>
.progress-container{padding:0.01em 16px}
.progress-round{border-radius:4px}
.progress-container{padding:10px 16px}
.progress-blue{color:#fff!important;background-color:#2196F3!important}
.progress-light-grey{color:#000!important;background-color:#f1f1f1!important}
</style>
         <script type="text/javascript">
             $(document).ready(function () {
               $('.select2').select2();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:ScriptManager ID="ScriptManagerCalendar" AsyncPostBackTimeOut="72000" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
    
    

    
     <!-- Content Wrapper. Contains page content -->
       
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>Penutupan Akhir Tahun Kewangan</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i><asp:Label ID="ps_lbl2" runat="server"></asp:Label></a></li>
                            <li class="active">Penutupan Akhir Tahun Kewangan</li>
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
                            <h3 class="box-title"><asp:Label Visible="false" ID="ps_lbl4" runat="server"></asp:Label>Maklumat Penutupan Akhir Tahun Kewangan</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                         <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tahun Kewangan <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                         <asp:TextBox ID="Tk_mula"  runat="server" class="form-control validate[optional] datepicker mydatepickerclass" autocomplete="off" placeholder="DD/MM/YYYY"></asp:TextBox>
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">                                    
                                    <div class="col-sm-8">
                                        <asp:Button ID="Button8" runat="server" Visible="false" class="btn btn-default" Text="Kembali" OnClick="Back_to_profile" UseSubmitBehavior="false" />
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                             
                           
                            <div class="box-body">&nbsp;
                                    </div>
                           <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
           <div class="col-md-12 box-body">
               <asp:GridView ID="GridView1" runat="server"  class="table table-bordered table-hover dataTable uppercase" AllowSorting="true"  AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="5000" GridLines="None" OnRowDataBound="GridView1_RowDataBound"  ShowFooter="true">
                                                        <%--<HeaderStyle ForeColor="#ffffff" />--%>
                                                                               
                                                        <Columns>
                                                       
                                                            <asp:TemplateField HeaderText = "No Akaun" ItemStyle-Width="15%">
                                                                
                                                               <ItemTemplate>
                                                              <asp:Label ID = "bal_type" runat="server" Text='<%# Eval("kod_akaun").ToString() %>' CssClass="uppercase"></asp:Label>                                                                   
                                                                   <asp:Label ID="Label1_nw" runat="server" Visible="false" Text='<%# Eval("jenis_akaun_type") %>'></asp:Label>
                                                                   <asp:Label ID="Label2_nw" runat="server" Visible="false" Text='<%# Eval("kw_acc_header") %>'></asp:Label>
                                                                   <asp:Label ID="Label3_nw" runat="server" Visible="false" Text='<%# Eval("bal_type") %>'></asp:Label>
                                                                   <asp:Label ID="Label4_nw" runat="server" Visible="false" Text='<%# Eval("kat_akaun") %>'></asp:Label>
                                                          </ItemTemplate>

                                                       </asp:TemplateField>

                                                             <asp:TemplateField HeaderText = "Keterangan" ItemStyle-Width="25%" >
                                                                
                                                                <ItemTemplate>
                                                                        <asp:Label ID = "kat_cd" runat="server" Text='<%# Eval("nama_akaun").ToString() %>' CssClass="uppercase"></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                       <asp:TemplateField HeaderText = "JUMLAH (RM)" HeaderStyle-HorizontalAlign="right" ItemStyle-Width="15%">
                                                                <ItemStyle HorizontalAlign = "Right" Font-Bold Width="15%"></ItemStyle>
                                                                <ItemTemplate>
                                                                        <asp:Label ID = "Label3" runat="server" Text='<%#Eval("Amt1","{0:C}").Replace("$","").Replace("RM","")%>' CssClass="uppercase"></asp:Label>

                                                                </ItemTemplate>

                                                            </asp:TemplateField>
     
                                                        </Columns>
                                          <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <%-- <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />--%>
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                    </asp:GridView>
               
              
               </div>
          </div>
                             <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                <asp:Button ID="Button4" runat="server" class="btn btn-danger sub_btn" Text="Jana Laporan" UseSubmitBehavior="false" />
                                &nbsp;&nbsp;&nbsp;
                                <asp:LinkButton id="fin_data2" runat="server" OnClick="cls_financial_year"><img src="../dist/img/icon/close_button.svg" alt='TUTUP AKAUN' /> </asp:LinkButton>
                            </div>
                           </div>
                               </div>
                              <div class="col-md-12 box-body">
        <div class="table-new-style-scr"> 
               <div class="col-md-1"> &nbsp; </div>
                <div class="col-md-10 col-sm-12 col-xs-12 table-new-style-scr">
                             <rsweb:ReportViewer ID="Rptviwerlejar"  runat="server" width="100%" Height="100%" SizeToReportContent="True" ></rsweb:ReportViewer>
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

         </ContentTemplate>
             <Triggers>                                                 
                  </Triggers>
    </asp:UpdatePanel>
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
   
</asp:Content>

