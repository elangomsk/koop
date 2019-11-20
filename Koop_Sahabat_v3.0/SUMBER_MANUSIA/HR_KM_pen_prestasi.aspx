<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_KM_pen_prestasi.aspx.cs" Inherits="HR_KM_pen_prestasi" %>

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
                        <h1> <asp:Label ID="ps_lbl1" runat="server"></asp:Label>    </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i><asp:Label ID="ps_lbl2" runat="server"></asp:Label></a></li>
                            <li class="active"> <asp:Label ID="ps_lbl3" runat="server"></asp:Label> </li>
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
                            <h3 class="box-title">  
                                        <asp:Label ID="ps_lbl4" runat="server"></asp:Label>
    </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                         <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl5" runat="server"></asp:Label></label>
                                    <div class="col-sm-8 text-bold text-right">
                                        <asp:label ID="Kaki_no" runat="server" class="uppercase" MaxLength="10"></asp:label>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl8" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8 text-right">
                                         <asp:label ID="s_jab" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl9" runat="server"></asp:Label>  </label>
                                    <div class="col-sm-8 text-right">
                                         <asp:label ID="s_jaw" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   <asp:Label ID="ps_lbl10" runat="server"></asp:Label>  </label>
                                    <div class="col-sm-8 text-right">
                                        <asp:label ID="s_kj" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  <asp:Label ID="ps_lbl11" runat="server"></asp:Label>   </label>
                                    <div class="col-sm-8 text-right">
                                           <asp:label ID="s_unit" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">    <asp:Label ID="ps_lbl12" runat="server"></asp:Label>  </label>
                                    <div class="col-sm-8 text-right">
                                        <asp:label ID="s_gred" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>

                             <div class="box-header with-border">
                            <h3 class="box-title">  
                                        <asp:Label ID="ps_lbl13" runat="server"></asp:Label>
    </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                         <div class="box-body">&nbsp;</div>
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl14" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                         <textarea id="ul_tarea" runat="server" rows="5" class="form-control uppercase"></textarea>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" OnClick="Click_bck"/></label>
                                    <div class="col-sm-8">
                                       
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="box-header with-border">
                            <h3 class="box-title">  
                                        <asp:Label ID="ps_lbl16" runat="server"></asp:Label>
    </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                         <div class="box-body">&nbsp;</div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl17" runat="server"></asp:Label>  </label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="dd_bahag" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" AutoPostBack="true" OnSelectedIndexChanged="sel_section"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                              <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl18" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                          <asp:DropDownList ID="dd_subject" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                <asp:Button ID="Button2" runat="server" class="btn btn-primary" Text="Senarai" UseSubmitBehavior="false" OnClick="srch_kpp"/>
                            </div>
                           </div>
                               </div>
                            <div class="box-body">&nbsp;
                                    </div>

                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                     <asp:GridView ID="GridView3" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging_out" onrowdatabound="gvEmp_RowDataBound"> 
                                        <PagerStyle CssClass="pager" />
                                        <Columns>
                                         <asp:TemplateField HeaderText="BIL">
                                                <ItemStyle Width="2%"></ItemStyle> 
                                               <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="2%"/>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BAHAGIAN">   
                                                <ItemStyle HorizontalAlign="Left" Width="7%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lbl_bha" runat="server" Text='<%# Eval("cse_section_desc") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SUBJEK">   
                                                <ItemStyle HorizontalAlign="Left" Width="7%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lbl_sub" runat="server" Text='<%# Eval("csb_subject_desc") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PEMBERAT (%)">   
                                                <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="jabatan" runat="server" Text='<%# Eval("sap_weightage") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PPP">   
                                                <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="s_ppp" runat="server" Text='<%# Eval("sap_ppp_score") %>' MaxLength="2"></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PPK">   
                                                <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Textbox ID="s_ppk" runat="server" CssClass="form-control" Text='<%# Eval("sap_ppk_score") %>' MaxLength="2"></asp:Textbox> 
                                                <asp:Label ID="s_ppk_1" runat="server" Text='<%# Eval("sap_ppk_score") %>' Visible="false"></asp:Label>  
                                                <asp:Label ID="sdt" Visible="false" runat="server" Text='<%# Eval("sap_start_dt", "{0:dd/MM/yyyy}") %>'></asp:Label>   
                                                <asp:Label ID="edt" Visible="false" runat="server" Text='<%# Eval("sap_end_dt", "{0:dd/MM/yyyy}") %>'></asp:Label>   
                                                <asp:Label ID="stno" Visible="false" runat="server" Text='<%# Eval("sap_staff_no") %>'></asp:Label>   
                                                <asp:Label ID="spcd" Visible="false" runat="server" Text='<%# Eval("sap_post_cat_cd") %>'></asp:Label>   
                                                <asp:Label ID="ssccd" Visible="false" runat="server" Text='<%# Eval("sap_section_cd") %>'></asp:Label>   
                                                <asp:Label ID="ssbcd" Visible="false" runat="server" Text='<%# Eval("sap_subject_cd") %>'></asp:Label>   
                                                <asp:Label ID="ssqno" Visible="false" runat="server" Text='<%# Eval("sap_seq_no") %>'></asp:Label>   
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

                            <div class="box-body">&nbsp;
                                    </div>

                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  <asp:Label ID="ps_lbl20" runat="server"></asp:Label>   </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox1" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                

                              <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl21" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="TextBox2" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>


                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  <asp:Label ID="ps_lbl22" runat="server"></asp:Label>  </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox3" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                

                              <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl23" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="TextBox4" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>



                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                <asp:Button ID="Button8" runat="server" class="btn btn-danger" Text="Simpan" Type="submit" UseSubmitBehavior="false" OnClick="click_insert" />
                            </div>
                           </div>
                               </div>
                           
                            <div class="box-body">&nbsp;
                                    </div>



                              <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                      <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="true" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging_jmk" onrowdatabound="gvEmp_RowDataBound_jmk">
                                          <PagerStyle CssClass="pager" />
                                        <Columns>
                                         <asp:TemplateField HeaderText="BIL">
                                                <ItemStyle Width="2%"></ItemStyle> 
                                               <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="2%"/>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BAHAGIAN">   
                                                <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="ss_bha" runat="server" Text='<%# Eval("cse_section_desc") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MARKAH PENUH">   
                                                <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="ss_mp" runat="server" Text='<%# Eval("wt") %>'></asp:Label>  
                                            </ItemTemplate>  
                                             <FooterTemplate>
                                                <asp:Label ID="lblTotal_wt" runat="server"/>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Center" Font-Bold="true" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PPP">   
                                                <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="ss_ppp" runat="server" Text='<%# Eval("ppp") %>'></asp:Label>  
                                            </ItemTemplate>  
                                             <FooterTemplate>
                                                <asp:Label ID="lblTotal_ppp" runat="server"/>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Center" Font-Bold="true" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PPK">   
                                                <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="ss_ppk" runat="server" Text='<%# Eval("ppk") %>'></asp:Label>  
                                            </ItemTemplate>  
                                             <FooterTemplate>
                                                <asp:Label ID="lblTotal_ppk" runat="server"/>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Center" Font-Bold="true" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PURATA">   
                                                <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="org" runat="server" Text='<%# Bind("pro") %>'></asp:Label>  
                                            </ItemTemplate>  
                                             <FooterTemplate>
                                                <asp:Label ID="lblTotal_pro" runat="server"/>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Center" Font-Bold="true" />
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

                            <div class="box-body">&nbsp;
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


