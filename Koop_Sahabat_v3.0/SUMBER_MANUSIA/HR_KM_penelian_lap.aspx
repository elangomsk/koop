<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_KM_penelian_lap.aspx.cs" Inherits="HR_KM_penelian_lap" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       <style>
           .Grid tr.normal {
    background-color: #F7F6F3;
}

.Grid tr.alternate {
    background-color: #FFFFFF;
}
       </style>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>   <asp:Label ID="ps_lbl1" Visible="false" runat="server"></asp:Label> Maklumat Penilaian Prestasi</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i><asp:Label ID="ps_lbl2" runat="server"></asp:Label></a></li>
                            <li class="active">  <asp:Label ID="ps_lbl3" Visible="false" runat="server"></asp:Label> Maklumat Penilaian Prestasi</li>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl2_text" runat="server"> Penilaian Bagi Tahun</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txt_tahun" style="text-align:right;" runat="server" ReadOnly="true" class="form-control validate[optional] uppercase"></asp:TextBox>
                                        <asp:TextBox ID="txt_kat_jaw" runat="server" Visible="false" class="form-control validate[optional] uppercase"></asp:TextBox>
                                        
                                    </div>
                                </div>
                            </div>
                                
                          
                                 </div>
                                </div>
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl5" runat="server"></asp:Label></label>
                                    <div class="col-sm-8 text-bold text-right">
                                        <asp:label ID="Kaki_no" runat="server" class="uppercase" MaxLength="10"></asp:label>
                                    </div>
                                </div>
                            </div>
                              <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  <asp:Label ID="ps_lbl7" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8 text-right">
                                        <asp:label ID="txt_org" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   <asp:Label ID="ps_lbl6" runat="server"></asp:Label>  </label>
                                    <div class="col-sm-8 text-right">
                                       <asp:label ID="s_nama" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">    <asp:Label ID="ps_lbl12" Visible="false" runat="server"></asp:Label> Wilayah  </label>
                                    <div class="col-sm-8 text-right">
                                        <asp:label ID="s_gred" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>




                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl8" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8 text-right">
                                         <asp:label ID="s_jab" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                     <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   <asp:Label ID="ps_lbl10" runat="server"></asp:Label>  </label>
                                    <div class="col-sm-8 text-right">
                                        <asp:label ID="s_kj" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                          
                                 </div>
                                </div>



                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label Visible="false" ID="ps_lbl9" runat="server"></asp:Label> Penyelia 1 </label>
                                    <div class="col-sm-8 text-right">
                                         <asp:label ID="s_jaw" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  <asp:Label Visible="false" ID="ps_lbl11" runat="server"></asp:Label>  Penyelia 2 </label>
                                    <div class="col-sm-8 text-right">
                                           <asp:label ID="s_unit" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                           <br />
                              <div class="box-header with-border">
                            <h3 class="box-title">Kursus, Latihan Dan Seminar</h3>
                        </div>
                        <div class="box-body">&nbsp;</div>
                               <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
           <div class="col-md-12 box-body">
                            <asp:GridView ID="GridView4" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging_out" onrowdatabound="gvEmp_RowDataBound1">
                                         <PagerStyle CssClass="pager" />
                                        <Columns>

                                         <asp:TemplateField HeaderText="BIL">
                                                <ItemStyle Width="2%"></ItemStyle> 
                                               <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="2%"/>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Tajuk">   
                                                <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="gv4_1" runat="server" Text='<%# Eval("trn_catatan") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tempat">   
                                                <ItemStyle HorizontalAlign="center" Width="5%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="gv4_2" runat="server" Text='<%# Eval("trn_dur") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tarikh">   
                                                <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="gv4_3" runat="server" Text='<%# Eval("dari") %>'></asp:Label>  
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
                             <br />
                              <div class="box-header with-border">
                            <h3 class="box-title">Disiplin</h3>
                        </div>
                        <div class="box-body">&nbsp;</div>
                               <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
           <div class="col-md-12 box-body">
                            <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging_out1" onrowdatabound="gvEmp_RowDataBound11">
                                         <PagerStyle CssClass="pager" />
                                        <Columns>

                                         <asp:TemplateField HeaderText="BIL">
                                                <ItemStyle Width="2%"></ItemStyle> 
                                               <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="2%"/>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Tajuk">   
                                                <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="gv1_1" runat="server" Text='<%# Eval("dis_catatan") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tindakan">   
                                                <ItemStyle HorizontalAlign="center" Width="5%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="gv1_2" runat="server" Text='<%# Eval("hr_discipline_desc") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tarikh">   
                                                <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="gv1_3" runat="server" Text='<%# Eval("eff_dt") %>'></asp:Label>  
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
                            <br />
                           
                            <div class="box-header with-border">
                            <h3 class="box-title"><asp:Label ID="Label1" Visible="false" runat="server"></asp:Label>Kriteria Penilaian</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                <asp:GridView ID="GridView3" runat="server" class="table table-bordered table-hover uppercase Grid" RowStyle-BorderWidth="3" RowStyle-BorderColor="#BDC4C7" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" RowStyle-CssClass="alternate"   OnPageIndexChanging="gvSelected_PageIndexChanging_out"  onrowdatabound="gvEmp_RowDataBound" OnDataBound="OnDataBound">
                                         <PagerStyle CssClass="pager" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Bahagian">   
                                                <ItemStyle HorizontalAlign="Left" Width="15%" Font-Bold="true"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lbl_bha_desc" runat="server" Text='<%# Eval("cse_section_desc") %>'></asp:Label>  
                                               
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                        <%--<asp:BoundField DataField="cse_section_desc" HeaderText="cse_section_desc" SortExpression="cse_section_desc" />  --%>
                                                <asp:TemplateField HeaderText="KOD">   
                                                <ItemStyle HorizontalAlign="center" Width="7%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lbl_bha" runat="server" Text='<%# Eval("sap_post_cat_cd") %>'></asp:Label>  
                                               
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SUBJEK KRITERIA">   
                                                <ItemStyle HorizontalAlign="Left" Width="25%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lbl_sub" runat="server" Text='<%# Eval("sap_subject_cd") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ulasan Kakitangan">   
                                                <ItemStyle HorizontalAlign="center" Width="20%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lbl_ula" runat="server" Text='<%# Eval("sap_staff_remark") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MARKAH WAJARAN">   
                                                <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lbl_ula1" runat="server" Text='<%# Eval("sap_weightage").ToString() + " / "+ Eval("cnt") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="MARKAH PENYELIA 1">   
                                                <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lbl_ula2" runat="server" Text='<%# Eval("sap_ppp_score").ToString()  + " / "+ Eval("cnt1") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                              <asp:TemplateField HeaderText="MARKAH PENYELIA 2">   
                                                <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lbl_markah" runat="server" Text='<%# Eval("sap_ppk_score").ToString()  + " / "+ Eval("cnt2") %>'></asp:Label>  
                                                 <asp:Textbox ID="Label2" runat="server" style="display:none;" Text='<%# Eval("sap_section_cd") %>'></asp:Textbox>  
                                                <asp:Textbox ID="Label3" runat="server" style="display:none;" Text='<%# Eval("sap_weightage") %>'></asp:Textbox>  
                                                <asp:Textbox ID="Label4" runat="server" style="display:none;" Text='<%# Eval("cse_sec_type") %>'></asp:Textbox>  
                                                <asp:Textbox ID="Label5" runat="server" style="display:none;" Text='<%# Eval("sap_post_cat_cd") %>'></asp:Textbox>  
                                                <asp:Textbox ID="lbl_Textbox1" runat="server" style="display:none;" Text='<%# Eval("sap_ppp_score") %>'></asp:Textbox>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                        </Columns>
                                         <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <RowStyle BackColor="#EFF3FB" BorderStyle="Solid" />
                                                        
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                       
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
               </div>
                                   
          </div>
                            <br />
                              <div class="box-header with-border">
                            <h3 class="box-title">Markah Keseluruhan</h3>
                        </div>
                        <div class="box-body">&nbsp;</div>
                               <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
           <div class="col-md-12 box-body">
                            <asp:GridView ID="GridView2" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="true" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging_out1" onrowdatabound="gvEmp_RowDataBound12">
                                         <PagerStyle CssClass="pager" />
                                        <Columns>

                                         <asp:TemplateField HeaderText="BIL">
                                                <ItemStyle Width="2%"></ItemStyle> 
                                               <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="2%"/>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Bahagian">   
                                                <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="gv2_1" runat="server" Text='<%# Eval("cse_section_desc") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                  <FooterStyle HorizontalAlign="right" />
                <FooterTemplate>
                 <asp:Label ID="ftr_v8"  Width="100%" runat="server" Text="JUMLAH MARKAH"  ></asp:Label>
                </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Jumlah Markah Penuh">   
                                                <ItemStyle HorizontalAlign="center" Width="5%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="gv2_2" runat="server" Text='<%# Eval("c1") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                    <FooterStyle HorizontalAlign="center" />
                <FooterTemplate>
                 <asp:Label ID="ftr_v1" CssClass="form-control"  Width="100%" runat="server" Text="0"  ></asp:Label>
                </FooterTemplate>
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Jumlah Markah Wajaran">   
                                                <ItemStyle HorizontalAlign="center" Width="5%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="gv2_3" runat="server" Text='<%# Eval("c2") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                  <FooterStyle HorizontalAlign="center" />
                <FooterTemplate>
                 <asp:Label ID="ftr_v2" CssClass="form-control"  Width="100%" runat="server" Text="0"  ></asp:Label>
                </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Jumlah Markah Penyelia 1">   
                                                <ItemStyle HorizontalAlign="center" Width="5%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="gv2_4" runat="server" Text='<%# Eval("c3") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                     <FooterStyle HorizontalAlign="center" />
                <FooterTemplate>
                 <asp:Label ID="ftr_v3" CssClass="form-control"  Width="100%" runat="server" Text="0"  ></asp:Label>
                </FooterTemplate>
                                                </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Jumlah Markah Penyelia 2">   
                                                <ItemStyle HorizontalAlign="center" Width="5%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="gv2_5" runat="server" Text='<%# Eval("c4") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                   <FooterStyle HorizontalAlign="center" />
                <FooterTemplate>
                 <asp:Label ID="ftr_v4" CssClass="form-control"  Width="100%" runat="server" Text="0"  ></asp:Label>
                </FooterTemplate>
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="PURATA (%)">   
                                                <ItemStyle HorizontalAlign="center" Width="5%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="gv2_6" runat="server" Text='<%# Eval("purta") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                  <FooterStyle HorizontalAlign="center" />
                <FooterTemplate>
                 <asp:Label ID="ftr_v5" CssClass="form-control"  Width="100%" runat="server" Text="0"  ></asp:Label>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Status</label>
                                    <div class="col-sm-8">
                                        <asp:Dropdownlist ID="Dropdownlist1" runat="server" class="form-control select2 validate[optional] uppercase" >
                                            <asp:ListItem Value="A">AKTIF</asp:ListItem>
                                            <asp:ListItem Value="T">TIDAK AKTIF</asp:ListItem>
                                        </asp:Dropdownlist>
                                    </div>
                                </div>
                            </div>
                                
                          
                                 </div>
                                </div>
                          <hr />
                                     <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 text-center box-body">
                                <div class="form-group">
                                     <asp:TextBox ID="get_id" Visible="false" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                                                                        <asp:Button ID="Button4" runat="server" class="btn btn-danger sub_btn" Text="Kemaskini" OnClick="click_insert"  UseSubmitBehavior="false" />
                                    &nbsp;
                                    <asp:Button ID="Button1" runat="server" class="btn btn-warning sub_btn" Text="Cetak" OnClick="clk_cetak"  UseSubmitBehavior="false" />
                                                                            &nbsp;
                                                                            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-default" Text="Kembali" OnClick="Click_bck" />
                                </div>
                            </div>
                                
                          
                                 </div>
                                </div>
                           <div class="row">
                                <div class="col-md-12 col-sm-4" style="text-align: center; display:none; line-height: 13px;">
                                    <rsweb:ReportViewer ID="RptviwerStudent" runat="server" Width="50%">
                                    </rsweb:ReportViewer>
                                </div>
                            </div>
                    </div>
                </div>
            </div>
            <!-- /.row -->
              </ContentTemplate>
            <Triggers>
               <asp:PostBackTrigger ControlID="Button1"  />
           </Triggers>
    </asp:UpdatePanel>
        
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
  
</asp:Content>






