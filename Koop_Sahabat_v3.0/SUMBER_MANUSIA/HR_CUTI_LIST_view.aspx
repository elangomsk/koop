<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_CUTI_LIST_view.aspx.cs" Inherits="HR_CUTI_LIST_view" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">    </asp:ScriptManager>

  
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1 id="h1_tag" runat="server">   Semakan Cuti  </h1>
                        <ol class="breadcrumb">
                            <li id="bb1_text" runat="server"><a href="#"><i class="fa fa-dashboard"></i>Sumber Manusia</a></li>
                            <li id="bb2_text" runat="server" class="active">  Semakan Cuti </li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div class="row">
               <!-- /.col -->
               
                <div class="col-md-12">

                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title">Senarai Semakan Cuti </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                           
                            <div class="col-md-12 box-body">
                                <div class="form-group">
                                     <div class="col-sm-2 col-xs-12">
                                         <div class="input-group">    
                                       <asp:TextBox ID="txt_tkcuti" runat="server" class="form-control validate[optional] datepicker mydatepickerclass" autocomplete="off" placeholder="DD/MM/YYYY"></asp:TextBox>
                                         <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="You can't leave this Empty" CssClass="requiredFieldValidateStyle" ForeColor="Red" ControlToValidate="txt_tkcuti" Display="Dynamic" ValidationGroup="vgSubmit1_clm_sts">
                                    </asp:RequiredFieldValidator>
                                    </div>
                                     <div class="col-sm-2 col-xs-12 mob-view-top-padd">
                                          <div class="input-group">   
                                                             <asp:TextBox ID="txt_hing" runat="server" class="form-control validate[optional] datepicker mydatepickerclass" autocomplete="off" placeholder="DD/MM/YYYY"></asp:TextBox>
                                               <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="You can't leave this Empty" CssClass="requiredFieldValidateStyle" ForeColor="Red" ControlToValidate="txt_hing" Display="Dynamic" ValidationGroup="vgSubmit1_clm_sts">
                                    </asp:RequiredFieldValidator>
                                    </div>
                                     <div class="col-sm-2 col-xs-12 mob-view-top-padd">
                                           <asp:DropDownList style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional] uppercase"
                                                                    ID="DropDownList1">  
                                               <asp:ListItem Value="">STATUS KELULUSAN</asp:ListItem>
                                               <asp:ListItem Value="00">MOHON</asp:ListItem>
                                               <asp:ListItem Value="01">SAH</asp:ListItem>
                                               <asp:ListItem Value="02">TIDAK SAH</asp:ListItem>
                                               <asp:ListItem Value="04">BATAL</asp:ListItem>
                                                                </asp:DropDownList>
                                    </div>
                                     
                                 <div class="col-sm-2 col-xs-12 mob-view-top-padd" style="text-align:center;">
                                 <asp:Button ID="Button2" runat="server" class="btn btn-primary" Text="Search" UseSubmitBehavior="false" OnClick="BindGridview" />
                                     <asp:Button ID="Button3" runat="server" class="btn btn-default" Text="Clear" usesubmitbehavior="false" OnClick="Reset_btn" />
                                     </div>
                                   <div class="col-sm-2 col-xs-12 mob-view-top-padd">
                                <asp:DropDownList runat="server" CssClass="form-control uppercase select2" ID="sel_frmt">
                                                <asp:ListItem Value="01">PDF</asp:ListItem>
                                                <asp:ListItem Value="02">EXCEL</asp:ListItem>
                                               <%-- <asp:ListItem  Value="03">Word</asp:ListItem>--%>
                                                </asp:DropDownList>
                            </div>
                                 <div class="col-sm-2 col-xs-12 mob-view-top-padd" style="text-align:center;">
                               <asp:Button ID="Btn_Cetak" runat="server" class="btn btn-warning" UseSubmitBehavior="false"  Text="Export" OnClick="ctk_values" />
                            </div>                                      
                                </div>
                            </div>
                                  </div>
                                </div>
                               <hr>
                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                   <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="15" ShowFooter="false" GridLines="None" OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="gvSelected_PageIndexChanging">
                                       <PagerStyle CssClass="pager" />
                                                    <Columns>
                                                    <asp:TemplateField HeaderText="Bil"> 
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle> 
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                           <asp:TemplateField HeaderText="Nama Kakitangan">
                                                        <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("stf_name") %>' CssClass="uppercase"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="No Kakitangan">
                                                            <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("stf_staff_no") %>' ></asp:Label>
                                                                <asp:Label ID="Label3" runat="server" Visible="false" Text='<%# Eval("stf_staff_no") %>' ></asp:Label>
                                                                     <asp:Label ID="lbl_id" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                                     <%--<asp:Label ID="lbl_sts_cd" runat="server" Text='<%# Eval("lap_approve_sts_cd") %>' Visible="false"></asp:Label>--%>                                                                     
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="No Rujukan">
                                                            <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label2_nr" runat="server" Text='<%# Eval("lap_ref_no") %>' CssClass="uppercase"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                     
                                                        <asp:TemplateField HeaderText="Tarikh Mohon">
                                                            <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("lap_application_dt","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Jenis Cuti">
                                                            <ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("hr_jenis_desc") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Hari Cuti">
                                                             <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label61" runat="server" Text='<%# Bind("lap_leave_day") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Tarikh Mula">
                                                          <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label11" runat="server" Text='<%# Bind("lap_leave_start_dt","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Tarikh Sehingga">
                                                               <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label12" runat="server" Text='<%# Bind("lap_leave_end_dt","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField Visible="false" HeaderText="Status Kelulusan">
                                                                <ItemStyle HorizontalAlign="Left" Font-Bold></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label12_sts" runat="server" Text='<%# Bind("app_stscd") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="LEWAT MOHON (HARI)">
                                                         <ItemStyle HorizontalAlign="center" ForeColor="Red" Font-Bold></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label63" runat="server" Text='<%# Bind("lap_late_apply") %>' CssClass="uppercase"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Status Kelulusan">
                                                                <ItemStyle HorizontalAlign="Left" Width="10%" Font-Bold></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label12_stsp1" runat="server" Text='<%# Bind("app_stscd") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="Status Pengesahan">
                                                                <ItemStyle HorizontalAlign="Left" Width="10%" Font-Bold></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label12_stsp" runat="server" Text='<%# Bind("psts") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Modify" Visible="false">
                                                                <ItemStyle HorizontalAlign="Left" Width="3%"></ItemStyle>
                                                                    <ItemTemplate>
                                                                          <asp:LinkButton runat="server" ID="lnkView" ToolTip="view" OnClick="lnkView_Click" Font-Bold Font-Underline>
                                                View
                                                  </asp:LinkButton>
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
                                              <div class="row" style="display:none;">
                                   <div class="col-md-12 col-sm-4" style="text-align:center; line-height:13px;">
                                     <rsweb:ReportViewer ID="RptviwerStudent" runat="server" Width="50%"></rsweb:ReportViewer>
                                    </div>
                                    </div>                
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.col -->
            </div>
            <!-- /.row -->
            </ContentTemplate>
                <Triggers>
                         <asp:PostBackTrigger ControlID="Btn_Cetak" />       
           </Triggers>
    </asp:UpdatePanel>
        
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
   
</asp:Content>






