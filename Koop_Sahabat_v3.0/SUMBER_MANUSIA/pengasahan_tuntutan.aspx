<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/pengasahan_tuntutan.aspx.cs" Inherits="pengasahan_tuntutan" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server" ScriptMode="Release">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>  Status Tuntutan </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>  Sumber Manusia </a></li>
                            <li class="active"> Status Tuntutan  </li>
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
                            <h3 class="box-title">Carian Maklumat Tuntutan </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                             <div class="col-md-12 box-body">
                                <div class="form-group">
                                    <div class="col-sm-2 col-xs-12 ">
                                         <%--<asp:TextBox ID="txt_tahun" runat="server" CssClass="form-control validate[optional,custom[number]] uppercase"></asp:TextBox>--%>
                                                                <asp:DropDownList style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"
                                                                    ID="txt_tahun">
                                                                    <%--onselectedindexchanged="dd_kat_SelectedIndexChanged">--%>
                                                                </asp:DropDownList>
                                    </div>
                                      <div class="col-sm-2 col-xs-12 mob-view-top-padd">
                                           <asp:DropDownList style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"
                                                                    ID="DD_bulancaruman">
                                                                    <%--onselectedindexchanged="dd_kat_SelectedIndexChanged">--%>
                                                                </asp:DropDownList>
                                          </div>
                                  
                                     <div class="col-sm-2 col-xs-12 mob-view-top-padd">
                                           <asp:DropDownList style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional] uppercase"
                                                                    ID="DropDownList1">  
                                               <asp:ListItem Value="">STATUS KELULUSAN</asp:ListItem>
                                               <asp:ListItem Value="01">SAH</asp:ListItem>
                                               <asp:ListItem Value="02">TIDAK SAH</asp:ListItem>                                               
                                                                </asp:DropDownList>
                                    </div>
                                     
                                 <div class="col-sm-2 col-xs-12 mob-view-top-padd" style="text-align:center;">
                               <asp:Button ID="Button2" runat="server" class="btn btn-primary" Text="Carian" OnClick="BindGridview"/>
                                     <asp:Button ID="Button9" runat="server" class="btn btn-default" Text="Clear" usesubmitbehavior="false" OnClick="Reset_btn" />
                                     </div>
                                   <div class="col-sm-2 col-xs-12 mob-view-top-padd">
                                <asp:DropDownList runat="server" CssClass="form-control uppercase select2" ID="sel_frmt">
                                                <asp:ListItem Value="01">PDF</asp:ListItem>
                                                <asp:ListItem Value="02">EXCEL</asp:ListItem>
                                               <%-- <asp:ListItem  Value="03">Word</asp:ListItem>--%>
                                                </asp:DropDownList>
                            </div>
                                 <div class="col-sm-2 col-xs-12 mob-view-top-padd" style="text-align:center;">
                               <asp:Button ID="Btn_Cetak" runat="server" class="btn btn-warning" UseSubmitBehavior="false" OnClick="ctk_values" Text="Cetak" />
                                     <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" usesubmitbehavior="false" OnClick="Reset_btn"/>
                            </div>     
                                </div>
                            </div>
                  
                                  </div>
                                </div>
                           
                            <hr />
                          
                            <div id="show_cnt1" runat="server">
                             <div class="box-header with-border">
                            <h3 class="box-title">Senarai Status Tuntutan</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->                        
                              <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">                                   
         <div class="col-md-12 box-body">
                                     <asp:GridView ID="gvSelected" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="30" ShowFooter="false" GridLines="None"  OnPageIndexChanging="gvSelected_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound">
                                         <PagerStyle CssClass="pager" />
                                        <Columns>
                                        <asp:TemplateField HeaderText="BIL"  ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                             <asp:TemplateField HeaderText="No Kakitangan"> 
                                                <ItemStyle HorizontalAlign="center" />    
                                            <ItemTemplate>  
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("stf_staff_no") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                       <asp:TemplateField HeaderText="IC No">
                                            <ItemStyle HorizontalAlign="center" />   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("stf_icno") %>'></asp:Label> 
                                                <asp:Label ID="Label4_mnth" Visible="false" runat="server" Text='<%# Bind("clm_rec_dt") %>'></asp:Label>   
                                                <asp:Label ID="Label1_org_id" Visible="false" runat="server" Text='<%# Bind("clm_claim_cd") %>'></asp:Label>  
                                                <asp:Label ID="lbl_fil_name" Visible="false" runat="server" Text='<%# Eval("file_name") %>'></asp:Label> 
                                                <asp:Label ID="kod_akan" Visible="false" runat="server" Text='<%# Eval("stf_kod_akaun") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Nama Kakitangan"> 
                                                <ItemStyle HorizontalAlign="Left" />    
                                            <ItemTemplate>  
                                                <asp:Label ID="Label2_name" runat="server" Text='<%# Bind("stf_name") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Receipt Date"> 
                                                <ItemStyle HorizontalAlign="Center" />    
                                            <ItemTemplate>  
                                                <asp:Label ID="Label2_yr" runat="server" Text='<%# Bind("clm_rec_dt1") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Apply Date" ItemStyle-HorizontalAlign="center">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("clm_app_dt1") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Jenis Tuntutan" ItemStyle-HorizontalAlign="Left">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("hr_tun_desc") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                              <asp:TemplateField HeaderText="SEBAB">
                                                                                            <ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label4_seb" runat="server" Text='<%# Eval("clm_sebap") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Amuan (RM)" ItemStyle-HorizontalAlign="right">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label6_amt" runat="server" Text='<%# Bind("clm_claim_amt","{0:n}") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Baki / Jumlah Terkini (RM)" ItemStyle-HorizontalAlign="right">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label6_amt1" runat="server" Text='<%# Bind("clm_balance_amt","{0:n}") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="File Name" ItemStyle-HorizontalAlign="center">   
                                            <ItemTemplate>  
                                                 <asp:Panel ID="pnlProducts" runat="server" Visible="true" Style="position: relative" >
                                                        <asp:GridView ID="gvProducts" ShowHeader="False" GridLines="None" runat="server" AutoGenerateColumns="false" PageSize="10"
                                                            AllowPaging="true" CssClass="Nested_ChildGrid">
                                                            <Columns>
                                                                  <asp:TemplateField HeaderText="File Name">
                                                             <ItemStyle HorizontalAlign="center" Width="50%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl1" runat="server" Text='<%# Eval("td1_name") %>'></asp:Label>
                                                                       <asp:Label ID="lbl_id" Visible="false" runat="server" Text='<%# Bind("ID") %>' CssClass="uppercase"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Download" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                    <ItemTemplate>
                         <asp:UpdatePanel ID="aa" runat="server">
                    <ContentTemplate>
                      <asp:LinkButton runat="server" ID="lnkView11" OnClick="lnkView_Click11">
                                                                <asp:Label ID="lbl3" runat="server" Text='Download'></asp:Label>
                                                                </asp:LinkButton>
                          </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger  ControlID="lnkView11"/>
                    </Triggers>
              </asp:UpdatePanel>
                    </ItemTemplate>
                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Jurnal NO" ItemStyle-HorizontalAlign="center">   
                                            <ItemTemplate>  
                                                <asp:Label ID="jurn_no" runat="server" Text='<%# Bind("clm_jurnal_no") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Status Kelulusan">
                                                                                            <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label5_sts" runat="server" Text='<%# Eval("sts_desc1") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Status Pengesahan">
                                                                                            <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label5_sts1" runat="server" Text='<%# Eval("sts_desc") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Permission" ItemStyle-HorizontalAlign="center" Visible="false" ItemStyle-Width="3%">
                                                                       <HeaderTemplate>
                                                                           STATUS TUNTUTAN<br/>
                                            <asp:CheckBox ID="chkAll" runat="server" Text="" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged"
                                                        ItemStyle-Width="150" />
                                            </HeaderTemplate>  
                                                                    <ItemTemplate>                                                                        
                                                                           <asp:CheckBox ID="chkSelect"  runat="server" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged" />
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
                             <hr />
                       <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                  <asp:Button ID="Button3" runat="server" class="btn btn-danger" Visible="false" Text="Simpan" UseSubmitBehavior="false" OnClick="submit_button"/>
                                            <asp:Button ID="Button4" runat="server" class="btn btn-default" Visible="false" Text="Batal" />
                                            <asp:Button ID="Button5" runat="server" class="btn btn-danger"  Text="Cetak" Visible="false" />
                            </div>
                           </div>
                               </div>
                                </div>
                               <div class="row">
                                   <div class="col-md-12 col-sm-4" style="text-align:center; line-height:13px; display:none;">
                                     <rsweb:ReportViewer ID="RptviwerStudent" runat="server" Width="50%"></rsweb:ReportViewer>
                                    </div>
                                    </div>
                           <div class="box-body">&nbsp;
                                    </div> 
                        </div>
                    </div>
                </div>
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


