<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Kewengan/kw_lep_pl.aspx.cs" Inherits="kw_lep_pl" %>
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
                        <h1><asp:Label ID="ps_lbl1" Visible="false" runat="server"></asp:Label> Penyata Pendapatan Dan Perbelanjaan</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i><asp:Label Visible="false" ID="ps_lbl2" runat="server"></asp:Label>Kewangan</a></li>
                            <li class="active"><asp:Label ID="ps_lbl3" Visible="false" runat="server"></asp:Label> Penyata Pendapatan Dan Perbelanjaan</li>
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
                            <h3 class="box-title"><asp:Label Visible="false" ID="ps_lbl4"  runat="server"></asp:Label>Kriteria Pilihan</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                         <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl5" runat="server"></asp:Label> <span class="style1">*</span> </label>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl6" runat="server"></asp:Label> <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                       <asp:TextBox ID="Tk_akhir"  runat="server" class="form-control validate[optional] datepicker mydatepickerclass" autocomplete="off" placeholder="DD/MM/YYYY"></asp:TextBox>
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                              <div class="row">
                             <div class="col-md-12">
                           <div class="col-md-6 box-body" >
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl8" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList id="ddpro" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"></label>
                                    <div class="col-sm-8">
                                        
                                       <label><asp:RadioButton ID="chk_var1" runat="server" GroupName="gg1" OnCheckedChanged="chk_variance" AutoPostBack="true" />&nbsp;&nbsp;&nbsp; Variance</label>                                        
                                        <label style="display:none;"><asp:RadioButton ID="chk_var2" runat="server" GroupName="gg1" OnCheckedChanged="chk_variance" AutoPostBack="true" />&nbsp;&nbsp;&nbsp;Monthly</label>
                                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <label><asp:RadioButton ID="chk_var3" runat="server" GroupName="gg1" OnCheckedChanged="chk_variance" AutoPostBack="true" />&nbsp;&nbsp;&nbsp;None</label>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body" style="display:none;">
                                        <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Kod Akaun </label>
                                    <div class="col-sm-8">
                                     <asp:listbox runat="server" class="form-control uppercase" id="chk_lst" selectionmode="Multiple"></asp:listbox>
                                    </div>
                                </div>
                              
                            </div>
                                 
                                 </div>
                                </div> 
                            
                             <div class="row" id="chk_variance1" runat="server" visible="false">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Mula Variance </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                         <asp:TextBox ID="TextBox1"  runat="server" class="form-control validate[optional] datepicker mydatepickerclass" autocomplete="off" placeholder="DD/MM/YYYY"></asp:TextBox>
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Akhir Variance </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                       <asp:TextBox ID="TextBox2"  runat="server" class="form-control validate[optional] datepicker mydatepickerclass" autocomplete="off" placeholder="DD/MM/YYYY"></asp:TextBox>
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                             <div class="row">
                             <div class="col-md-12">
                          <%--  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl7" runat="server"></asp:Label> <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                    <asp:DropDownList id="Tahun_kew" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>--%>
                                
                                   <div class="col-md-6 box-body" style="display:none;">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label Visible="false" ID="ps_lbl9" runat="server"></asp:Label>Aras COA</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList id="akaun_level" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                          <%--   <div class="row">
                             <div class="col-md-12">
                          
                              
                                 </div>
                                </div>--%>
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                <asp:Button ID="Button4" runat="server" class="btn btn-danger sub_btn" Text="Jana Laporan" OnClick="clk_srch"  UseSubmitBehavior="false" />
                                <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" OnClick="btn_reset"  UseSubmitBehavior="false" />
                                  <asp:Button ID="Button2" runat="server" class="btn btn-warning sub_btn" Visible="false" Text="Export PDF" OnClick="clk_submit"  UseSubmitBehavior="false" />
                                <asp:Button ID="Button3" runat="server" class="btn btn-warning sub_btn" Visible="false" Text="Export EXCEL" OnClick="ExportToEXCEL"  UseSubmitBehavior="false" />
                            </div>
                           </div>
                               </div>
                            <div class="box-body">&nbsp;
                                    </div>
                           <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
           <div class="col-md-12 box-body">
               <asp:GridView ID="GridView1" runat="server"  class="table table-bordered table-hover dataTable uppercase" OnSorting="GridView1_Sorting" AllowSorting="true" DataKeyNames="kat"  AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="5000" GridLines="None" OnRowDataBound="GridView1_RowDataBound"  ShowFooter="true">
                                                        <%--<HeaderStyle ForeColor="#ffffff" />--%>
                                                                               
                                                        <Columns>
                                                            <asp:BoundField DataField="sno" HeaderText="sno" Visible="false"  SortExpression="sno" />   
                                                         <asp:BoundField DataField="kat" HeaderText="kat" SortExpression="kat" ItemStyle-Font-Bold="true" />   
                                                               <asp:BoundField DataField="hdr" HeaderText="hdr" SortExpression="hdr" ItemStyle-Font-Bold="true"/>  
                                                               
                                                             <asp:TemplateField HeaderText= "Keterangan" ItemStyle-Width="25%" >
                                                                
                                                                <ItemTemplate>
                                                                     <asp:Label ID="bal_type" runat="server" Text='<%# Eval("tmp_kod_akaun").ToString() %>' Visible="false" CssClass="uppercase"></asp:Label>  
                                                                        <asp:Label ID="kat_cd" runat="server" Text='<%# Eval("nama_akaun").ToString() %>' CssClass="uppercase"></asp:Label>
                                                                 <%--   <asp:Label ID = "Label1" runat="server" Text='<%# Eval("curamt").ToString() %>' CssClass="uppercase"></asp:Label>
                                                                    <asp:Label ID = "Label2" runat="server" Text='<%# Eval("preamt").ToString() %>' CssClass="uppercase"></asp:Label>--%>

                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="cur_jum" HeaderText="2019 (RM)" DataFormatString="{0:n}" ItemStyle-Width="15%" SortExpression="cur_jum" ItemStyle-HorizontalAlign="Right" ItemStyle-Font-Bold="true"/>   
                                                            <asp:BoundField DataField="pre_jum" HeaderText="2018 (RM)" DataFormatString="{0:n}" ItemStyle-Width="15%" SortExpression="pre_jum" ItemStyle-HorizontalAlign="Right" ItemStyle-Font-Bold="true"/>   
     <%--  <asp:TemplateField HeaderText="Action">

                                                                <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:UpdatePanel ID="aa" runat="server">
                    <ContentTemplate>
                                                                         <asp:LinkButton ID="LinkButton1" runat="server" ToolTip='Download' CommandArgument='<%# Eval("tmp_kod_akaun")%>' OnClick="clk_update" >
                                                                                                    <i class='fa fa-file-pdf-o'></i>
                                                                                                </asp:LinkButton>  
                         </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger  ControlID="LinkButton1"/>
                    </Triggers>                                           
                                                                            </asp:UpdatePanel>                         
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
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
                           <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
           <div class="col-md-12 box-body">
               <div class="col-md-1 box-body"> &nbsp; </div>
                <div class="col-md-10 box-body">
                                 <rsweb:ReportViewer ID="Rptviwerlejar"  runat="server" width="100%" Height="100%" SizeToReportContent="True" ></rsweb:ReportViewer>
                    <rsweb:ReportViewer ID="ReportViewer1"  runat="server" width="100%" Height="100%" SizeToReportContent="True" ></rsweb:ReportViewer>
                                                            <asp:Label runat="server" ID="ReportErrorMessage" Visible="false" CssClass="report-error-message"></asp:Label>
               </div>
                <div class="col-md-1 box-body"> &nbsp; </div>
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
                 <asp:PostBackTrigger ControlID="Button3"  />
                 <asp:PostBackTrigger ControlID="Button2"  />
                  </Triggers>
    </asp:UpdatePanel>
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
   
</asp:Content>

