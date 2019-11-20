<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/Hr_sc_kakitangan.aspx.cs" Inherits="Hr_sc_kakitangan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <asp:UpdateProgress id="updateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
    <ProgressTemplate>
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
            <span style="border-width: 0px; position: fixed; font-weight:bold; padding: 50px; background-color: #FFFFFF; font-size: 16px; left: 40%; top: 40%;">Sila Tunggu. Rekod Sedang Diproses ...</span>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1 id="h1_tag" runat="server"> Selenggara Cuti Kakitangan </h1>
                        <ol class="breadcrumb">
                            <li id="bb1_text" runat="server"><a href="#"><i class="fa fa-dashboard"></i>Sumber Manusia</a></li>
                            <li id="bb2_text" runat="server" class="active"> Selenggara Cuti Kakitangan  </li>
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
                            <h3 class="box-title" id="h3_tag" runat="server"> Maklumat Cuti Kakitangan </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl1_text" runat="server">Nama Syarikat / Organisasi <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="dd_org" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" AutoPostBack="true" OnSelectedIndexChanged="sel_orgbind">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                             <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl2_text" runat="server">  Perniagaan  </label>
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
                            <div class="col-md-6 box-body" style="pointer-events:none;">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl3_text" runat="server"> Jabatan </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="dd_jabatan" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl4_text" runat="server"> Kategori Jawatan </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="DD_katjaw" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="ver_id" style="display:none;"  runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl5_text" runat="server"> Tempoh Khidmat </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="T_khidmat" runat="server" class="form-control validate[optional]"></asp:TextBox>
                                        <br />
                                          <label><asp:CheckBox ID="chk_assign_rkd" runat="server" /> &nbsp;Assigned</label>
                                    </div>
                                </div>
                            </div>
                                    <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="Label1" runat="server">  Tahun </label>
                                    <div class="col-sm-8">
                                         <%--<asp:TextBox ID="txt_tahun" runat="server" CssClass="form-control validate[optional,custom[number]] uppercase"></asp:TextBox>--%>
                                                                <asp:DropDownList style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"
                                                                    ID="txt_tahun">
                                                                    <%--onselectedindexchanged="dd_kat_SelectedIndexChanged">--%>
                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Carian" UseSubmitBehavior="false" OnClick="srch_click"/>
                                                        <asp:Button ID="Button4" runat="server" class="btn btn-default" Text="Set Semula" UseSubmitBehavior="false" OnClick="Button5_Click" />
                            </div>
                           </div>
                               </div>
                            <hr />
                                    <div class="box-header with-border" id="show_htxt" runat="server" visible="false">
                            <h3 class="box-title" id="h3_tag2" runat="server">Senarai Cuti Kakitangan</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->                        
                                  <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
               <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="50" ShowFooter="false" GridLines="None" OnRowDataBound = "RowDataBound" OnPageIndexChanging="gvSelected_PageIndexChanging" >
                   <PagerStyle CssClass="pager" />
                                        <Columns>
                                         <asp:TemplateField HeaderText="BIL">  
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="150"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NO KAKITANGAN">  
                                                <ItemStyle HorizontalAlign="Center" Width="7%"></ItemStyle>
                                            <ItemTemplate>  
                                                 <asp:Label ID="s_no" runat="server"  Text='<%# Eval("stf_staff_no") %>'></asp:Label> 
                                                <asp:Label ID="lbl_orgcd" Visible="false" runat="server"  Text='<%# Eval("str_curr_org_cd") %>'></asp:Label> 
                                                <asp:Label ID="service_dt" Visible="false" runat="server"  Text='<%# Eval("service_dt") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NAMA"> 
                                                <ItemStyle HorizontalAlign="Left" Width="25%"></ItemStyle>  
                                            <ItemTemplate>  
                                                <asp:Label ID="s_name" runat="server"  Text='<%# Eval("stf_name") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JAWATAN">  
                                                <ItemStyle HorizontalAlign="Left" Width="25%"></ItemStyle> 
                                            <ItemTemplate>  
                                                <asp:Label ID="s_jabatan" runat="server" Text='<%# Eval("hr_jaw_desc") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="GRED">   
                                                <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="s_gred" runat="server" Text='<%# Eval("hr_gred_desc") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="KATEGORI JAWATAN">   
                                                <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="s_kat_jawa" runat="server" Text='<%# Eval("hr_kate_desc") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="STATUS JAWATAN">   
                                                <ItemStyle HorizontalAlign="Left" Width="5%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="s_sts_jaw" runat="server" Text='<%# Eval("hr_traf_desc") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TEMPOH KHIDMAT">   
                                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="Label51" runat="server" Text=' <%# Eval("d_yr") + " TAHUN "+  Eval("d_mth") + " BULAN" %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CUTI DIBAWA KEHADAPAN">   
                                                <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:TextBox ID="stfcfwd" runat="server" Width="100px" MaxLength="5" CssClass="form-control validate[optional,custom[number]] uppercase" Text='<%# Eval("stf_carry_fwd_lv") %>' ></asp:TextBox>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CUTI TAHUNAN">   
                                                <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:TextBox ID="ctlday" runat="server" Width="100px" MaxLength="5" CssClass="form-control validate[optional,custom[number]] uppercase" Text='<%# Eval("ct_lday") %>'></asp:TextBox>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CUTI SAKIT">   
                                                <ItemStyle HorizontalAlign="center" Width="15%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:TextBox ID="cslday" runat="server" Width="100px" MaxLength="5" CssClass="form-control validate[optional,custom[number]] uppercase" Text='<%# Eval("cs_lday") %>'></asp:TextBox>  
                                                <asp:Label ID="org_cd" runat="server"  Visible="false" Text='<%# Eval("str_curr_org_cd") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="cuti ehsan" Visible="false">   
                                                <ItemStyle HorizontalAlign="center" Width="15%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:TextBox ID="cshos" Width="100px" CssClass="form-control validate[optional,custom[number]] uppercase" runat="server" Text='<%# Eval("cs_hos") %>'></asp:TextBox>   
                                                 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                              <asp:TemplateField HeaderText="PILIH">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAll" runat="server" Text="&nbsp;PILIH" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged_ann"
                                                        ItemStyle-Width="150" />
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle> 
                                               <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" />
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
                               <asp:Button ID="Button3" runat="server" class="btn btn-danger" Text="Simpan" UseSubmitBehavior="false" OnClick="clk_insert" OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;"/>
                            </div>
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
             <%--<Triggers>
               <asp:PostBackTrigger ControlID="Button4"  />
               <asp:PostBackTrigger ControlID="btb_kmes"  />
           </Triggers>--%>
    </asp:UpdatePanel>
        
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
   
</asp:Content>


