<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/hr_gaji_kelompok.aspx.cs" Inherits="hr_gaji_kelompok" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
             <style type="text/css">
        /* A scrolable div */
        .GridViewContainer
        {         
            overflow: auto;
        }
        /* to freeze column cells and its respecitve header*/
        .FrozenCell
        {
            background-color:Gray;
            position: relative;
            cursor: default;
            left: expression(document.getElementById("GridViewContainer").scrollLeft-2);
        }
        /* for freezing column header*/
        .FrozenHeader
        {
         background-color:Gray;
            position: relative;
            cursor: default;          
            top: expression(document.getElementById("GridViewContainer").scrollTop-2);
            z-index: 10;
        }
        /*for the locked columns header to stay on top*/
        .FrozenHeader.locked
        {
            z-index: 99;
        }
      
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
     <script type="text/javascript">
             // Get the instance of PageRequestManager.
             var prm = Sys.WebForms.PageRequestManager.getInstance();
             // Add initializeRequest and endRequest
             prm.add_initializeRequest(prm_InitializeRequest);
             prm.add_endRequest(prm_EndRequest);

             // Called when async postback begins
             function prm_InitializeRequest(sender, args) {
                 // get the divImage and set it to visible
                 var panelProg = $get('divImage');
                 panelProg.style.display = '';
                 // reset label text
                 var lbl = $get('<%= this.lblText.ClientID %>');
                 lbl.innerHTML = '';

                 // Disable button that caused a postback
                 $get(args._postBackElement.id).disabled = true;
             }

             // Called when async postback ends
             function prm_EndRequest(sender, args) {
                 // get the divImage and hide it again
                 var panelProg = $get('divImage');
                 panelProg.style.display = 'none';

                 // Enable button that caused a postback
                 $get(sender._postBackSettings.sourceElement.id).disabled = false;
             }
         </script>
    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>  Janaan Pendapatan (Kelompok)</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>  Sumber Manusia </a></li>
                            <li class="active"> Janaan Pendapatan (Kelompok)  </li>
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
                            <h3 class="box-title">Carian Maklumat Pendapatan </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                                 <div class="col-md-12">
                            <div class="col-md-12 box-body">
                                <div class="form-group">
                                    <div class="col-sm-3 col-xs-12 ">
                                         <asp:DropDownList style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"
                                                                    ID="txt_tahun">                                                                    
                                                                </asp:DropDownList>
                                    </div>
                                      <div class="col-sm-3 col-xs-12 mob-view-top-padd">
                                           <asp:DropDownList style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"
                                                                    ID="DD_bulancaruman">
                                                                    <%--onselectedindexchanged="dd_kat_SelectedIndexChanged">--%>
                                                                </asp:DropDownList>
                                          </div>
                                    <div class="col-sm-6 col-xs-12 mob-view-top-padd">
                                            <label><asp:CheckBox ID="chk_assign_rkd" runat="server" /> &nbsp; Assigned</label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                          <asp:Button ID="Button2" runat="server" class="btn btn-primary" Text="Carian" OnClick="BindGridview"/>
                                                                <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" usesubmitbehavior="false" OnClick="Reset_btn"/>
                                 <asp:Button ID="Button6" runat="server" class="btn btn-warning" Text="Export To Excel" UseSubmitBehavior="false" Visible="false" OnClick="ctk_values" />
                               
                                 <asp:Button ID="Button7" runat="server" class="btn btn-danger" UseSubmitBehavior="false"
                                                                    Text="Export To PDF" OnClick="click_pdf" Visible="false" />
                                         <asp:Button ID="Button5" runat="server" class="btn btn-danger" UseSubmitBehavior="false"
                                                                    Text="Hapus" OnClick="click_Hapus" Visible="false" />
                                        </div>
                                  
                                </div>
                            </div>
                                 
                                  </div>
                                </div>
                            <hr />
                            <div class="row">
                                                        <div class="col-md-12 col-sm-2" style="text-align: center">
                                                            <rsweb:ReportViewer ID="Rptviwer_gaji" runat="server" Width="100%" style=" overflow:auto;" Height="100%" SizeToReportContent="True">
                                                            </rsweb:ReportViewer>
                                                            <asp:Label runat="server" ID="ReportErrorMessage" Visible="false" CssClass="report-error-message"></asp:Label>
                                                        </div>
                                                    </div>                          
                            
                            <div id="show_cnt1" runat="server" visible="false">
                         <%--    <div class="box-header with-border">
                            <h3 class="box-title">Senarai Janaan Pendapatan</h3>
                        </div>--%>
                        <!-- /.box-header -->
                        <!-- form start -->         
                                <div id="GridViewContainer" class="GridViewContainer" style="width:100%;height:500px;" >               
                              <div class="dataTables_wrapper form-inline dt-bootstrap">                                   
         <div class="col-md-12 box-body">
                                     <asp:GridView ID="gvSelected"  runat="server" class="GridView table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="50" ShowFooter="true" GridLines="None" OnRowDataBound="GridView1_DataBound" OnPageIndexChanging="gvSelected_PageIndexChanging" DataKeyNames="stf_staff_no" EnablePersistedSelection="true">
                                         <PagerStyle CssClass="pager" />
                                         <HeaderStyle CssClass="FrozenHeader" />
                                        <Columns>
                                              <asp:TemplateField HeaderText="Permission" ItemStyle-HorizontalAlign="center" ItemStyle-Width="2%" ItemStyle-CssClass="FrozenCell" HeaderStyle-CssClass="FrozenCell">
                                                                       <HeaderTemplate>
                                                                           Semua Pilih<br/>
                                            <asp:CheckBox ID="chkAll" runat="server" Text="" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged"
                                                        ItemStyle-Width="150" />
                                            </HeaderTemplate>  
                                                                    <ItemTemplate>                                                                        
                                                                           <asp:CheckBox ID="chkSelect"  runat="server" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                        <asp:TemplateField HeaderText="BIL"  ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Nama Kakitangan"> 
                                                <ItemStyle HorizontalAlign="Left" />    
                                            <ItemTemplate>  
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("stf_name") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                       <asp:TemplateField HeaderText="No Kakitangan">
                                            <ItemTemplate>  
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("stf_staff_no") %>'></asp:Label> 
                                                <asp:Label ID="Label4_mnth" Visible="false" runat="server" Text='<%# Bind("mnth") %>'></asp:Label>   
                                                <asp:Label ID="Label1_org_id" Visible="false" runat="server" Text='<%# Bind("yer") %>'></asp:Label> 
                                                <asp:Label ID="sub_org" Visible="false" runat="server" Text='<%# Bind("stf_cur_sub_org") %>'></asp:Label> 
                                                <asp:Label ID="org_cd" Visible="false" runat="server" Text='<%# Bind("str_curr_org_cd") %>'></asp:Label> 
                                                <asp:Label ID="ctg_amt" Visible="false" runat="server" Text='<%# Bind("ctg_amt") %>'></asp:Label> 
                                                <asp:Label ID="pcb_amt_l" Visible="false" runat="server" Text='<%# Bind("pcb_amt") %>'></asp:Label> 
                                                <asp:Label ID="cp38_amt_l" Visible="false" runat="server" Text='<%# Bind("cp38_amt") %>'></asp:Label>                                                 
                                                <asp:Label ID="emp_kwsp_perc" Visible="false" runat="server" Text='<%# Bind("emp_kwsp_perc") %>'></asp:Label> 
                                                <asp:Label ID="org_id" Visible="false" runat="server" Text='<%# Bind("org_id") %>'></asp:Label>  
                                               <%-- <asp:Label ID="tung_amt" runat="server" Visible="false" Text='<%# Bind("tunamt","{0:n}") %>'></asp:Label>   --%>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                              <asp:TemplateField HeaderText="IC No" Visible="false"> 
                                                <ItemStyle HorizontalAlign="Center" />    
                                            <ItemTemplate>  
                                                <asp:Label ID="stf_icno" runat="server" Text='<%# Bind("stf_icno") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tahun"> 
                                                <ItemStyle HorizontalAlign="Center" />    
                                            <ItemTemplate>  
                                                <asp:Label ID="yer" runat="server" Text='<%# Bind("yer") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bulan">   
                                                    <ItemStyle HorizontalAlign="Center" />  
                                            <ItemTemplate>  
                                                <asp:Label ID="mnth" runat="server" Text='<%# Bind("mnth") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Gaji Pokok (RM)" ItemStyle-HorizontalAlign="Right">   
                                            <ItemTemplate>  
                                                <asp:Label ID="salary" runat="server" Text='<%# Bind("salary","{0:n}") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                     <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                  <asp:Label ID="ftr_001" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Elaun Tetap (RM)" ItemStyle-HorizontalAlign="Right">   
                                            <ItemTemplate>  
                                                <asp:Label ID="fix_alwncce" runat="server" Text='<%# Bind("fix_alwncce","{0:n}") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                    <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                  <asp:Label ID="ftr_002" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Lain-Lain Elaun (RM)" ItemStyle-HorizontalAlign="Right">   
                                            <ItemTemplate>  
                                                <asp:Label ID="xta_alwnce" runat="server" Text='<%# Bind("xta_alwnce","{0:n}") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                   <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                  <asp:Label ID="ftr_003" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Bonus Tahunan (RM)" ItemStyle-HorizontalAlign="Right">   
                                            <ItemTemplate>  
                                                <asp:Label ID="bns_amt" runat="server" Text='<%# Bind("bns_amt","{0:n}") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                   <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                  <asp:Label ID="ftr_004" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Bonus KPI (RM)" ItemStyle-HorizontalAlign="Right">   
                                            <ItemTemplate>  
                                                <asp:Label ID="kpi_bns_amt" runat="server" Text='<%# Bind("kpi_bns_amt","{0:n}") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                  <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                  <asp:Label ID="ftr_005" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="KLM (RM)" ItemStyle-HorizontalAlign="Right">   
                                            <ItemTemplate>  
                                                <asp:Label ID="ot_amt" runat="server" Text='<%# Bind("ot_amt","{0:n}") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                  <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                  <asp:Label ID="ftr_006" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Lain-Lain (RM)" ItemStyle-HorizontalAlign="Right">   
                                            <ItemTemplate>  
                                                <asp:Label ID="tung_amt" runat="server" Text='<%# Bind("tunamt","{0:n}") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                  <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                  <asp:Label ID="ftr_006_1" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                             
                                             <asp:TemplateField HeaderText="Caruman KWSP (RM)" ItemStyle-HorizontalAlign="Right">   
                                            <ItemTemplate>  
                                                <asp:Label ID="kwsp_amt" runat="server" Text='<%# Bind("kwsp_amt","{0:n}") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                  <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                  <asp:Label ID="ftr_008" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                            
                                             <asp:TemplateField HeaderText="POTONGAN PERKESO (RM)" ItemStyle-HorizontalAlign="Right">   
                                            <ItemTemplate>  
                                                <asp:Label ID="perkeso_amt" runat="server" Text='<%# Bind("perkeso_amt","{0:n}") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                  <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                  <asp:Label ID="ftr_010" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                            
                                             <asp:TemplateField HeaderText="POTONGAN SIP (RM)" ItemStyle-HorizontalAlign="Right">   
                                            <ItemTemplate>  
                                                <asp:Label ID="sip_amt1" runat="server" Text='<%# Bind("sip_amt1","{0:n}") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                  <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                  <asp:Label ID="ftr_012" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                            
                                             <asp:TemplateField HeaderText="LAIN-LAIN POTONGAN (RM)" ItemStyle-HorizontalAlign="Right">   
                                            <ItemTemplate>  
                                                <asp:Label ID="ded_amt" runat="server" Text='<%# Bind("ded_amt","{0:n}") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                  <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                  <asp:Label ID="ftr_014" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="PCB (RM)" ItemStyle-HorizontalAlign="Right">   
                                            <ItemTemplate>  
                                                <asp:Label ID="PCB_amt" runat="server" Text='<%# Bind("pcb_amt","{0:n}") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                  <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                  <asp:Label ID="ftr_017" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                              <asp:TemplateField HeaderText="CP 38 (RM)" ItemStyle-HorizontalAlign="Right">   
                                            <ItemTemplate>  
                                                <asp:Label ID="cp38_amt" runat="server" Text='<%# Bind("cp38_amt","{0:n}") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                  <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                  <asp:Label ID="ftr_017_1" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Jumlah Potongan Pendapatan (RM)" ItemStyle-HorizontalAlign="Right">   
                                            <ItemTemplate>  
                                                <asp:Label ID="tot_ded_amt" runat="server" Text='<%# Bind("tot_ded_amt","{0:n}") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                   <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                  <asp:Label ID="ftr_007" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                              <asp:TemplateField HeaderText="PENDAPATAN KASAR (RM)" ItemStyle-HorizontalAlign="Right">   
                                            <ItemTemplate>  
                                                <asp:Label ID="gross_amt" runat="server" Text='<%# Bind("gross_amt","{0:n}") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                   <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                  <asp:Label ID="ftr_015" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="PENDAPATAN BERSIH (RM)" ItemStyle-HorizontalAlign="Right" ItemStyle-Font-Size="13px" ItemStyle-Font-Bold>   
                                            <ItemTemplate>  
                                                <asp:Label ID="nett_amt" runat="server" Text='<%# Bind("nett_amt","{0:n}") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                  <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                  <asp:Label ID="ftr_016" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Caruman KWSP Majikan (RM)" ItemStyle-HorizontalAlign="Right">   
                                            <ItemTemplate>  
                                                <asp:Label ID="kwsp_emp_amt" runat="server" Text='<%# Bind("kwsp_emp_amt","{0:n}") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                  <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                  <asp:Label ID="ftr_009" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="PERKESO MAJIKAN (RM)" ItemStyle-HorizontalAlign="Right">   
                                            <ItemTemplate>  
                                                <asp:Label ID="emp_perkeso_amt" runat="server" Text='<%# Bind("emp_perkeso_amt","{0:n}") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                  <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                  <asp:Label ID="ftr_011" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="SIP Majikan (RM)" ItemStyle-HorizontalAlign="Right">   
                                            <ItemTemplate>  
                                                <asp:Label ID="emp_sip_amt1" runat="server" Text='<%# Bind("sip_emp_amt1","{0:n}") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                  <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                  <asp:Label ID="ftr_013" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
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
                                    </div>
                            <div class="box-body">&nbsp;</div>
                                 <div class="row">
                             <div class="col-md-12">
                           
                            <div class="col-md-6 box-body">
                                 <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="Label4" runat="server"></label>
                                    <div class="col-sm-8">
                                       &nbsp;
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-5 control-label" id="Label5" runat="server">SEBELUM INI PENDAPATAN BERSIH (RM)</label>
                                    <div class="col-sm-7">
                                       <asp:TextBox ID="pre_mnth_jum" runat="server" CssClass="form-control" style="text-align:right;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                  
                                  </div>
                                </div>
                             <hr />
                                   
                       <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                  <asp:Button ID="Button3" runat="server" class="btn btn-danger" Visible="false" Text="Simpan" UseSubmitBehavior="false" OnClick="submit_button"/>
                                            <asp:Button ID="Button4" runat="server" class="btn btn-default" Visible="false" Text="Batal" />
                            </div>
                           </div>
                               </div>
                                </div>
                           <div class="box-body">&nbsp;
                                    </div> 
                              <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
                 <div id="divImage" class="text-center" style="display:none; padding-top: 30px; font-weight:bold;">
                     <asp:Image ID="img1" runat="server" ImageUrl="../dist/img/LoaderIcon.gif" />&nbsp;&nbsp;&nbsp;Processing Please wait ... </div> 
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.row -->
             </ContentTemplate>
             <Triggers>
               <asp:PostBackTrigger ControlID="Button6"  />   
                 <asp:PostBackTrigger ControlID="Button7"  />               
           </Triggers>
    </asp:UpdatePanel>
        
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
   
</asp:Content>


