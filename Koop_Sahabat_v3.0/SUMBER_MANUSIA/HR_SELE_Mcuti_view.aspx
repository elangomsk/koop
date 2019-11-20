<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_SELE_Mcuti_view.aspx.cs" Inherits="HR_SELE_Mcuti_view" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <asp:ScriptManager ID="ScriptManagerCalendar" ScriptMode="Release" runat="server">
    </asp:ScriptManager>

  
     <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper" >
                
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1 id="h1_tag" runat="server">  Selenggara Maklumat Cuti </h1>
                        <ol class="breadcrumb">
                            <li id="bb1_text" runat="server"><a href="#"><i class="fa fa-dashboard"></i>Sumber Manusia</a></li>
                            <li class="active" id="bb2_text" runat="server"> Selenggara Maklumat Cuti </li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div class="row">
               <!-- /.col -->
               
                <div class="col-md-12">

                    <div class="box">
                      
                        <div class="box-header">
                             <div class="box-body">&nbsp;</div>
                            <div class="box-body">
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl1_text" runat="server"> Jenis Cuti</label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="dd_jcuti" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" AutoPostBack="true" onselectedindexchanged="dd_jeniscuti"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                           
                                 </div>
                                </div>
                             <div class="box-header with-border">
                            <h3 class="box-title"><asp:Label ID="M_title" runat="server"></asp:Label></h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                             <div id="l1" runat="server">
                                 <div class="row">
                                    <div class="col-md-12">
                            <div class="col-md-6 box-body" style="display:none;">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Jawatan </label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="an_jawa" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl2_text" runat="server"> Jawatan</label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="an_katjawa" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl4_text" runat="server"> Tempoh Khidmat Minimum (Tahun) </label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="an_tk_min" runat="server" class="form-control validate[optional,custom[number]]" MaxLength="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl5_text" runat="server">  Tempoh Khidmat Maksimum (Tahun)</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="an_tk_mak" runat="server" class="form-control validate[optional,custom[number]]"  MaxLength="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                  <div class="row">
                                    <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl6_text" runat="server">  Kelayakan Cuti (Hari) </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="an_kel_cuti" runat="server" class="form-control validate[optional,custom[number]]"  MaxLength="2"></asp:TextBox>
                                                        <asp:TextBox ID="ann_rno" Visible="false" runat="server" class="form-control validate[optional,custom[number]]"  MaxLength="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                         <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="Label6" runat="server">  Tempoh Kelulusan</label>
                                    <div class="col-sm-8">
                                     <%--  <asp:DropDownList ID="act_yr" runat="server" CssClass="form-control select2">
                                           <asp:ListItem Value=""> --- PILIH ---</asp:ListItem>
                                           <asp:ListItem Value="01"> 2010 dan Sebelum</asp:ListItem>
                                           <asp:ListItem Value="02"> Selepas 2010</asp:ListItem>
                                       </asp:DropDownList>--%>
                                        <asp:TextBox ID="TextBox3" runat="server" class="form-control validate[optional,custom[number]]"  MaxLength="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                      </div>
                                   <div class="row">
                                    <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"></label>
                                    <div class="col-sm-8">
                                          <asp:CheckBox  ID="chk_man_ann" runat="server" Text="&nbsp;Mandatory Dokumen Lampiran"/>
                                    </div>
                                </div>
                            </div>
                                        
                                 </div>
                                      </div>

                                  <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                 <asp:Button ID="Button5" runat="server" class="btn btn-danger" Text="Simpan" Type="submit" UseSubmitBehavior="false" OnClick="Annual_insert_Click" OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;"/>
                                                        <asp:Button ID="Button7" runat="server" Text="Hapus" class="btn btn-warning" UseSubmitBehavior="false" OnClick="ann_hapus_click" OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;" />
                            </div>
                           </div>
                               </div>

                                <div class="box-body">&nbsp;
                                    </div>
                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                            
                                            <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="50" ShowFooter="false" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging" >
                                                <PagerStyle CssClass="pager" />
                                        <Columns>
                                        <asp:TemplateField HeaderText="BIL" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                   <asp:CheckBox ID="chkAll" runat="server" Text="&nbsp;BIL" AutoPostBack="true" onCheckedChanged="OnCheckedChanged_ann"
                                                        ItemStyle-Width="150" />
                                                </HeaderTemplate>
                                                <ItemStyle Width="5%"></ItemStyle> 
                                               <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="150"/>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JAWATAN" Visible="false"> 
                                                <ItemStyle HorizontalAlign="Left" Width="20%" Font-Underline="true" Font-Bold="true"></ItemStyle>  
                                            <ItemTemplate>  
                                           
                                                <asp:Label ID="Label3" runat="server"  Text='<%# Eval("hr_jaw_desc") %>'></asp:Label>   
                                                <asp:Label ID="pos_cd" Visible="false" runat="server"  Text='<%# Eval("ann_post_cd") %>'></asp:Label>   
                                                                                             
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JAWATAN">  
                                                <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle> 
                                            <ItemTemplate>  
                                                <asp:Label ID="gred" runat="server" Text='<%# Eval("hr_kate_desc") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tempoh Khidmat Minimum (Tahun)">   
                                                <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                 <asp:LinkButton runat="server" ID="lnkView" OnClick="lnkView_Click">
                                                <asp:Label ID="min_ser" runat="server" Text='<%# Eval("ann_min_service") %>'></asp:Label>  
                                                       <asp:Label ID="ser_year" Visible="false" runat="server"  Text='<%# Eval("ann_leave_duration") %>'></asp:Label>   
                                                <asp:Label ID="Label7" Visible="false" runat="server"  Text='<%# Eval("ann_leave_type_cd") %>'></asp:Label>    
                                                     </asp:LinkButton>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tempoh Khidmat Maksimum (Tahun)">   
                                                <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="max_ser" runat="server" Text='<%# Eval("ann_max_service") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Kelayakan Cuti (Hari)">   
                                                <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="org" runat="server" Text='<%# Eval("ann_leave_day") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Tempoh Kelulusan">   
                                                <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="org1" runat="server" Text='<%# Eval("ann_leave_duration") %>'></asp:Label>  
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


                             <div id="l2" runat="server">

                                   <div class="row">
                                    <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl7_text" runat="server">  Kategori Cuti </label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="com_cuti" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                          <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl8_text" runat="server">  Kelayakan Cuti (Hari) </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="com_kel_cuti" runat="server" class="form-control validate[optional,custom[number]]"  MaxLength="2"></asp:TextBox>
                                                        <asp:TextBox ID="com_rno" Visible="false" runat="server" class="form-control validate[optional,custom[number]]"  MaxLength="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                      </div>
                                  <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                <asp:Button ID="Button2" runat="server" class="btn btn-danger" Text="Simpan" Type="submit" UseSubmitBehavior="false" OnClick="Comp_insert_Click" OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;" />
                                                        <asp:Button ID="Button4" runat="server" Text="Hapus" class="btn btn-danger" UseSubmitBehavior="false" OnClick="com_hapus_click" OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;"  />
                            </div>
                           </div>
                               </div>
                                 <div class="box-body">&nbsp;
                                    </div>
                                 <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                            
                                             <asp:GridView ID="GridView2" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging_comp" >
                                                 <PagerStyle CssClass="pager" />
                                        <Columns>
                                         <asp:TemplateField HeaderText="BIL" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAll" runat="server" Text="&nbsp;BIL" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged_com"
                                                        ItemStyle-Width="150" />
                                                </HeaderTemplate>
                                                <ItemStyle Width="5%"></ItemStyle> 
                                               <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="150"/>
                                            
                                            </ItemTemplate>

                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Kategori Cuti"> 
                                                <ItemStyle HorizontalAlign="Left" Width="50%" Font-Underline></ItemStyle>  
                                            <ItemTemplate>  
                                            <asp:LinkButton runat="server" ID="lnkView" OnClick="lnkView_Click_comp">
                                                <asp:Label ID="cjen_desc" runat="server"  Text='<%# Eval("hr_jenis_desc") %>'></asp:Label>   
                                                <asp:Label ID="com_cd" Visible="false" runat="server"  Text='<%# Eval("com_cat_cd") %>'></asp:Label>   
                                                </asp:LinkButton> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Kelayakan Cuti (Hari)">   
                                                <ItemStyle HorizontalAlign="center" Width="30%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="cleave_day" runat="server" Text='<%# Eval("com_leave_day") %>'></asp:Label>  
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
                              <div id="l3" runat="server">
                             <div class="row">
                                    <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl9_text" runat="server">   Tempoh Khidmat Minimum (Tahun)</label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="out_tk_min" runat="server" class="form-control validate[optional,custom[number]]" MaxLength="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                          <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl10_text" runat="server">   Tempoh Khidmat Maksimum (Tahun)</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="out_tk_mak" runat="server" class="form-control validate[optional,custom[number]]"  MaxLength="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                      </div>

                            
                             <div class="row">
                                    <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl11_text" runat="server">   Kelayakan Cuti (Hari)</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="out_kel_cuti" runat="server" class="form-control validate[optional,custom[number]]"  MaxLength="2"></asp:TextBox>
                                                        <asp:TextBox ID="out_rno" Visible="false" runat="server" class="form-control validate[optional,custom[number]]"  MaxLength="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                      </div>

                                   <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                  <asp:Button ID="Button8" runat="server" class="btn btn-danger" Text="Simpan" Type="submit" UseSubmitBehavior="false" OnClick="out_insert_Click" OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;" />
                                                        <asp:Button ID="Button10" runat="server" Text="Hapus" class="btn btn-warning" UseSubmitBehavior="false" OnClick="out_hapus_click" OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;"  />
                            </div>
                           </div>
                               </div>
                             <div class="box-body">&nbsp;
                                    </div>
                                 <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                             <asp:GridView ID="GridView3" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging_out">
                                                 <PagerStyle CssClass="pager" />
                                        <Columns>

                                         <asp:TemplateField HeaderText="BIL" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAll" runat="server" Text="&nbsp;BIL" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged_out"
                                                        ItemStyle-Width="150" />
                                                </HeaderTemplate>
                                                <ItemStyle Width="5%"></ItemStyle> 
                                               <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="150"/>
                                            
                                            </ItemTemplate>

                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tempoh Khidmat Minimum (Tahun)">   
                                                <ItemStyle HorizontalAlign="center" Width="10%" Font-Underline></ItemStyle>
                                            <ItemTemplate>  
                                            <asp:LinkButton runat="server" ID="lnkView" OnClick="lnkView_Click_out">
                                                <asp:Label ID="omin_yr" runat="server" Text='<%# Eval("out_min_yr") %>'></asp:Label>  
                                                </asp:LinkButton>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tempoh Khidmat Maksimum (Tahun)">   
                                                <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="jabatan" runat="server" Text='<%# Eval("out_max_yr") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Kelayakan Cuti (Hari)">   
                                                <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="org" runat="server" Text='<%# Eval("out_leave_day") %>'></asp:Label>  
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
                                 <div id="l4" runat="server">

                                     <div class="row">
                                    <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl12_text" runat="server"> Kelayakan Cuti (Hari) <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="gen_kel_cuti" runat="server" class="form-control validate[optional,custom[number]]"  MaxLength="2"></asp:TextBox>
                                                        <asp:TextBox ID="gen_rno" Visible="false" runat="server" class="form-control validate[optional,custom[number]]"  MaxLength="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                          <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="Label8" runat="server"> Tempoh Kelulusan<span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="TextBox4" runat="server" class="form-control validate[optional,custom[number]]"  MaxLength="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                      </div>
                                      <div class="row">
                                    <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"></label>
                                    <div class="col-sm-8">
                                          <asp:CheckBox  ID="chk_man" runat="server" Text="&nbsp;Mandatory Dokumen Lampiran"/>
                                    </div>
                                </div>
                            </div>
                                        
                                 </div>
                                      </div>
                                      <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                 <asp:Button ID="Button11" runat="server" class="btn btn-danger" Text="Simpan" Type="submit" UseSubmitBehavior="false"  OnClick="gen_insert_Click" OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;"/>
                            </div>
                           </div>
                               </div>
                                     </div>

                                 <div id="l_umum" runat="server">


                                     <div class="row">
                                    <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl13_text" runat="server">    Organisasi</label>
                                    <div class="col-sm-8">
                                           <asp:DropDownList ID="DropDownList1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                                         <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl14_text" runat="server">   Negeri</label>
                                    <div class="col-sm-8">
                                           <asp:DropDownList ID="DD_NegriBind1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                      </div>

                                     <div class="row" id="v1" runat="server" visible="false">
                                    <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="Label1" runat="server">     Hari</label>
                                    <div class="col-sm-8">
                                           <div class="input-group">
                                                 
                                                        <asp:TextBox ID="TextBox2" runat="server" class="form-control " ></asp:TextBox>
                                                        </div>
                                    </div>
                                </div>
                            </div>

                                       
                                 </div>
                                      </div>

                                      <div class="row" id="v2" runat="server">
                                    <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl15_text" runat="server">     Dari</label>
                                    <div class="col-sm-8">
                                           <div class="input-group">
                                                     <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        <asp:TextBox ID="td_date" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                        </div>
                                    </div>
                                </div>
                            </div>

                                         <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl16_text" runat="server">   Sehingga</label>
                                    <div class="col-sm-8">
                                           <div class="input-group">
                                                     <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        <asp:TextBox ID="ts_date" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl17_text" runat="server">     Keterangan</label>
                                    <div class="col-sm-8">
                                            <asp:TextBox ID="TextBox1" runat="server" class="form-control uppercase"  MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   </div>
                                      </div>     
                                        <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                <asp:Button ID="Button1" runat="server" class="btn btn-danger" Text="Simpan" Type="submit" UseSubmitBehavior="false" OnClick="umum_insert_Click" OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;"/>
                                                        <asp:Button ID="Button3" runat="server" class="btn btn-default" Text="Batal" UseSubmitBehavior="false" OnClick="batal_Click" OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;"/>
                            </div>
                           </div>
                               </div>

                                      <div class="box-body">&nbsp;
                                    </div>                                
                                            <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                              <asp:GridView ID="GridView4" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging_umum" >
                                                  <PagerStyle CssClass="pager" />
                                        <Columns>

                                         <asp:TemplateField HeaderText="BIL">  
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="150"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ORGANISASI">  
                                                <ItemStyle HorizontalAlign="Left" Width="25%"></ItemStyle>
                                            <ItemTemplate>  
                                                 <asp:Label ID="Label2" runat="server"  Text='<%# Eval("org_name") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NEGERI"> 
                                                <ItemStyle HorizontalAlign="Left" Width="25%"></ItemStyle>  
                                            <ItemTemplate>  
                                                <asp:Label ID="Label3" runat="server"  Text='<%# Eval("hr_negeri_desc") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JENIS CUTI">  
                                                <ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle> 
                                            <ItemTemplate>  
                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("hr_jenis_desc") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TARIKH CUTI">   
                                                <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("hol_dt") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CATATAN">   
                                                <ItemStyle HorizontalAlign="Left" Width="25%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="Label51" runat="server" Text='<%# Eval("hol_remark") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BATAL">   
                                                <ItemStyle HorizontalAlign="center" Width="5%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:CheckBox ID="chkStatus" runat="server"/>
                                                <asp:Label ID="oid" runat="server" Visible="false" Text='<%# Eval("hol_org_id") %>'></asp:Label>
                                                <asp:Label ID="hid" runat="server" Visible="false" Text='<%# Eval("hol_holiday_cd") %>'></asp:Label>
                                                <asp:Label ID="sid" runat="server" Visible="false" Text='<%# Eval("hol_state_cd") %>'></asp:Label>
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
                                 </div>

                            <%--    </div>--%>
                           
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.col -->
            
            <!-- /.row -->
            </ContentTemplate>
    </asp:UpdatePanel>
        
        <!-- /.row -->
    </section>
                    <!-- /.content -->
               
                <!-- /.content-wrapper -->
   </div>
        </div>
</asp:Content>





