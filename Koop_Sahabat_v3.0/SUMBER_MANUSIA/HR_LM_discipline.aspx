<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_LM_discipline.aspx.cs" Inherits="HR_LM_discipline" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1 id="h1_tag" runat="server">  Laporan Maklumat Disiplin</h1>
                        <ol class="breadcrumb">
                            <li id="bb1_text" runat="server"><a href="#"><i class="fa fa-dashboard"></i>Sumber Manusia</a></li>
                            <li class="active" id="bb2_text" runat="server">  Laporan Maklumat Disiplin  </li>
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
                            <h3 class="box-title" id="h3_tag" runat="server"> Maklumat Pilihan Janaan</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row"  style="display:none;">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl1_text" runat="server">  No Kakitangan</label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="Kaki_no" runat="server" class="form-control validate[optional] uppercase" MaxLength="1000"></asp:TextBox>
                                                         <asp:TextBox ID="Applcn_no1" runat="server" class="form-control validate[optional] uppercase"
                                                                                MaxLength="150" Visible="false"></asp:TextBox>
                                                                                  <asp:Panel ID="autocompleteDropDownPanel" runat="server" 
                                                                                    ScrollBars="Auto" Height="150px" Font-Size="Medium" 
                                                                                    HorizontalAlign="Left" Wrap="False" />
                                                                                 <cc1:AutoCompleteExtender ServiceMethod="GetCompletionList" MinimumPrefixLength="1"
                                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="Kaki_no"
                                                            ID="AutoCompleteExtender1" CompletionListElementID="autocompleteDropDownPanel" CompletionListCssClass="form-control uppercase" runat="server" FirstRowSelected="false">
                                                        </cc1:AutoCompleteExtender>
                                    </div>
                                </div>
                            </div>
                           
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl2_text" runat="server">    Organisasi  </label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="dd_org" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" AutoPostBack="true" OnSelectedIndexChanged="sel_orgbind">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl3_text" runat="server">Perniagaan</label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="dd_org_pen" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" AutoPostBack="true" OnSelectedIndexChanged="sel_orgjaba">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl4_text" runat="server">  Jabatan </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="dd_jabatan" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" AutoPostBack="true" onselectedindexchanged="UnitBind"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl5_text" runat="server">  Unit   </label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="dd_unit" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>


                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl6_text" runat="server"> Dari </label>
                                    <div class="col-sm-8">
                                        <div class="input-group">                                                                    
                                                        <asp:TextBox ID="tm_date" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl7_text" runat="server"> Hingga </label>
                                    <div class="col-sm-8">
                                        <div class="input-group">                                                                    
                                                        <asp:TextBox ID="ta_date" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                              <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl8_text" runat="server">   Jenis Tindakan  </label>
                                    <div class="col-sm-8">
                                      <asp:DropDownList ID="dd_kl" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body" style="display:none;">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl9_text" runat="server">Jenis Kursus </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="dd_jl" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                            

                 
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                <asp:Button ID="Button1" runat="server" class="btn btn-danger" Text="Senarai" OnClick="srch_click" UseSubmitBehavior="false" />
                                <asp:Button ID="Button2" runat="server" class="btn btn-warning" Text="Export Ke PDF" OnClick="pdf_Click" UseSubmitBehavior="false" />
                                <asp:Button ID="Button3" runat="server" class="btn btn-success" Text="Export Ke Excel" OnClick="ExportToEXCEL" UseSubmitBehavior="false" />
                                                        <asp:Button ID="Button4" runat="server" class="btn btn-default" Text="Set Semula" UseSubmitBehavior="false" OnClick="clk_rset"/>
                            </div>
                           </div>
                               </div>
                           <div class="box-body">&nbsp;
                                    </div>


                             <div class="box-header with-border">
                            <h3 class="box-title" id="h3_tag2" runat="server"> Senarai Maklumat Disiplin Kakitangan</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                             
                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                   <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="30" ShowFooter="false" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging" >
                                       <PagerStyle CssClass="pager" />
                                        <Columns>
                                         <asp:TemplateField HeaderText="BIL">  
                                         <ItemStyle HorizontalAlign="center" Width="2%"></ItemStyle> 
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" /> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NO KAKITANGAN">  
                                                <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                 <asp:Label ID="s_no" runat="server"  Text='<%# Eval("stf_staff_no") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                              <asp:TemplateField HeaderText="NAMA"> 
                                                <ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>  
                                            <ItemTemplate>  
                                                <asp:Label ID="s_name" runat="server"  Text='<%# Eval("stf_name") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField> 
                                             <asp:TemplateField HeaderText="Wilayah">  
                                                <ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
                                            <ItemTemplate>  
                                                 <asp:Label ID="op_perg_name" runat="server"  Text='<%# Eval("op_perg_name") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>                                           
                                             <asp:TemplateField HeaderText="Tajuk">   
                                                <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="s_gred" runat="server" Text='<%# Eval("dis_catatan") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="TINDAKAN">  
                                                <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                 <asp:Label ID="hr_discipline_desc" runat="server"  Text='<%# Eval("hr_discipline_desc") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                              
                                                <asp:TemplateField HeaderText="TARIKH">   
                                                <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="s_gred" runat="server" Text='<%# Eval("tarikh") %>'></asp:Label>  
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
                         <rsweb:ReportViewer ID="RptviwerStudent" runat="server" width="100%" Height="100%" ZoomMode="PageWidth" SizeToReportContent="True"></rsweb:ReportViewer>
                                                            <asp:Label runat="server" ID="Label1" Visible="false" CssClass="report-error-message"></asp:Label>
                    </div>
                </div>
            </div>
            <!-- /.row -->

         </ContentTemplate>
            <Triggers>
            <asp:PostBackTrigger ControlID="Button2"  />
             <asp:PostBackTrigger ControlID="Button3"  />
              
                 </Triggers>
    </asp:UpdatePanel>
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
   
</asp:Content>



