<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_KM_pen_prestasi1.aspx.cs" Inherits="HR_KM_pen_prestasi1" %>


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
                        <h1>   <asp:Label ID="ps_lbl1" runat="server"></asp:Label></h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i><asp:Label ID="ps_lbl2" runat="server"></asp:Label></a></li>
                            <li class="active">  <asp:Label ID="ps_lbl3" runat="server"></asp:Label></li>
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
                                        <asp:TextBox ID="txt_tahun" runat="server" ReadOnly="true" class="form-control validate[optional] uppercase"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">    <asp:Label ID="ps_lbl12" Visible="false" runat="server"></asp:Label>Wilayah  </label>
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
                            <h3 class="box-title"><asp:Label ID="ps_lbl13" Visible="false" runat="server"></asp:Label>Kriteria</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                               <div class="row">
                             <div class="col-md-12">
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" > Bahagian</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList id="dd_bah1" runat="server" CssClass="form-control uppercase select2"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>                          
                                 </div>
                                </div>
                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
              
                  <asp:GridView ID="grvStudentDetails" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" ShowFooter="True" AllowPaging="true" GridLines="None" PageSize="20" onrowdatabound="gvEmp_RowDataBound" >
                                                        <Columns>
                                                          
                                                            <asp:TemplateField HeaderText="SUBJEK KRITERIA">
                                                                <ItemTemplate>
                                                                   <asp:TextBox ID="Col1" CssClass="form-control uppercase" Width="100%" Rows="1" TextMode="MultiLine" runat="server" Text='<%# Eval("column1") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                                   <FooterStyle HorizontalAlign="Left" />
                                                                <FooterTemplate>
                                                                    <asp:Button ID="ButtonAdd" runat="server" Text="Tambah Baru" style="width:50%;" CssClass="btn btn-success" OnClick="ButtonAdd_Click" />
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                             
                                                            <asp:TemplateField HeaderText="MARKAH">
                                                                <ItemStyle HorizontalAlign="center" />
                                                                <ItemTemplate>
                                                                   <asp:TextBox ID="Col2" CssClass="form-control" Width="100%" Text='<%# Eval("column2") %>' runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                         
                                                        </Columns>
                                                      
                                                    </asp:GridView> 
                                    

               </div>
          </div>
                              <hr />
                                     <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 text-center box-body">
                                <div class="form-group">
                                    <asp:Button ID="Button1" runat="server" class="btn btn-danger sub_btn" Text="Post for Appraisal"  OnClick="click_insert_tmp" UseSubmitBehavior="false" />
                                                                         
                                </div>
                            </div>
                                
                          
                                 </div>
                                </div>
                               <div class="box-header with-border" id="grd1_shw" runat="server">
                            <h3 class="box-title">Not Assigned Screen</h3>
                                     <div class="box-body">&nbsp;</div>
                        </div>
                             <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging_out" onrowdatabound="gvEmp_RowDataBound">
                                         <PagerStyle CssClass="pager" />
                                        <Columns>

                                         <asp:TemplateField HeaderText="BIL">
                                                <ItemStyle Width="2%"></ItemStyle> 
                                               <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="2%"/>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Bahagian">   
                                                <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="sts" runat="server" Text='<%# Eval("cse_section_desc") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SUBJEK KRITERIA KOD">   
                                                <ItemStyle HorizontalAlign="center" Width="5%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lbl_bha" runat="server" Text='<%# Eval("sap_post_cat_cd") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SUBJEK KRITERIA">   
                                                <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lbl_sub" runat="server" Text='<%# Eval("sap_subject_cd") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MARKAH">   
                                                <ItemStyle HorizontalAlign="center" Width="5%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="jabatan" runat="server" Text='<%# Eval("sap_weightage") %>'></asp:Label>  
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
                            <br />
                              <div class="box-header with-border" id="grd2_shw" runat="server">
                            <h3 class="box-title">Assigned Screen</h3>
                                  <div class="box-body">&nbsp;</div>
                        </div>
                        
                            <asp:GridView ID="GridView2" runat="server" class="table table-bordered table-hover dataTable uppercase" OnSorting="GridView1_Sorting1" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging_out1" onrowdatabound="gvEmp_RowDataBound1">
                                         <PagerStyle CssClass="pager" />
                                        <Columns>
                                            <asp:BoundField DataField="bah_dec" HeaderText="bah_dec" SortExpression="bah_dec" />  
                                          
                                                <asp:TemplateField HeaderText="SUBJEK KRITERIA KOD">   
                                                <ItemStyle HorizontalAlign="center" Width="5%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lbl_bha1" runat="server" Text='<%# Eval("sub_cd") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SUBJEK KRITERIA">   
                                                <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lbl_sub1" runat="server" Text='<%# Eval("sub_desc") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                              <asp:BoundField DataField="weight" HeaderText="MARKAH" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%" SortExpression="weight" />  
                                              
                                        </Columns>
                                         <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                       <%-- <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />--%>
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                             <br />
                            <div class="box-header with-border">
                            <h3 class="box-title"><asp:Label ID="Label1" Visible="false" runat="server"></asp:Label>Kriteria Penilaian Lain</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                <asp:GridView ID="GridView3" runat="server" class="table table-bordered table-hover dataTable uppercase" OnSorting="GridView1_Sorting" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging_out" onrowdatabound="gvEmp_RowDataBound">
                                         <PagerStyle CssClass="pager" />
                                        <Columns>

                                        <asp:BoundField DataField="cse_section_desc" HeaderText="cse_section_desc" SortExpression="cse_section_desc" />  
                                                <asp:TemplateField HeaderText="KOD">   
                                                <ItemStyle HorizontalAlign="Left" Width="7%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lbl_bha" runat="server" Text='<%# Eval("cap_post_cat_cd") %>'></asp:Label>  
                                                <asp:Label ID="Label2" runat="server" Visible="false" Text='<%# Eval("cse_section_cd") %>'></asp:Label>  
                                                <asp:Label ID="Label3" runat="server" Visible="false" Text='<%# Eval("cap_weightage") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SUBJEK KRITERIA">   
                                                <ItemStyle HorizontalAlign="Left" Width="7%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lbl_sub" runat="server" Text='<%# Eval("cap_subject_cd") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                               <%-- <asp:TemplateField HeaderText="MARKAH">   
                                                <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="jabatan" runat="server" Text='<%# Eval("cap_weightage") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>--%>
                                            <asp:BoundField DataField="cap_weightage" HeaderText="MARKAH" ItemStyle-Width="7%" SortExpression="cap_weightage" ItemStyle-HorizontalAlign="Right" ItemStyle-Font-Bold="true"/>   
                                               <asp:TemplateField HeaderText="STATUS">   
                                                <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="sts" runat="server" Text='<%# Eval("sts") %>'></asp:Label>  
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
                            
                        </div>
                          <hr />
                                     <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 text-center box-body">
                                <div class="form-group">
                                     <asp:TextBox ID="get_id" Visible="false" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                                                                        <asp:Button ID="Button4" runat="server" class="btn btn-danger sub_btn" Text="Simpan" OnClick="click_insert"  UseSubmitBehavior="false" />
                                                                            &nbsp;
                                                                            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-default" Text="Kembali" OnClick="Click_bck" />
                                </div>
                            </div>
                                
                          
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






