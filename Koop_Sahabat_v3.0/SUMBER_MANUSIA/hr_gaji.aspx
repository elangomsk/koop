<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/hr_gaji.aspx.cs" Inherits="hr_gaji" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0;
                right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                <span style="border-width: 0px; position: fixed; font-weight: bold; padding: 50px;
                    background-color: #FFFFFF; font-size: 16px; left: 40%; top: 40%;">Sila Tunggu. Rekod
                    Sedang Diproses ...</span>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>
    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1 id="h1_tag" runat="server">  Maklumat Penggajian </h1>
                        <ol class="breadcrumb">
                            <li id="bb1_text" runat="server"><a href="#"><i class="fa fa-dashboard"></i>Sumber Manusia</a></li>
                            <li class="active" id="bb2_text" runat="server">  Maklumat Penggajian </li>
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
                            <h3 class="box-title" id="h3_tag" runat="server"> Maklumat Peribedi </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="lbl1_text" runat="server">No Kakitangan</label>
                                    <div class="col-sm-8">
                                        <asp:label ID="txt_stffno" runat="server" class="uppercase"
                                                            MaxLength="150"></asp:label>
                                                             <asp:TextBox ID="Applcn_no1" runat="server" class="form-control validate[optional] uppercase"
                                                                                MaxLength="150" Visible="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="lbl2_text" runat="server">   Nama Kakitangan</label>
                                    <div class="col-sm-8">
                                        <asp:label ID="txt_nama" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="lbl3_text" runat="server">Gred</label>
                                    <div class="col-sm-8">
                                        <asp:label ID="txt_gred" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="lbl4_text" runat="server"> Nama Syarikat / Organisasi </label>
                                    <div class="col-sm-8">
                                         <asp:label ID="txt_org" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="lbl5_text" runat="server"> Perniagaan </label>
                                    <div class="col-sm-8">
                                        <asp:label ID="TextBox15" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="lbl6_text" runat="server">  Jabatan </label>
                                    <div class="col-sm-8">
                                        <asp:label ID="txt_jaba" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="lbl7_text" runat="server"> Jawatan </label>
                                    <div class="col-sm-8">
                                       <asp:label ID="txt_jawa" runat="server" class="uppercase"></asp:label>
                                                        <asp:TextBox ID="TextBox22" runat="server" Visible="false" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <hr />
                               <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="lbl8_text" runat="server">  Pendapatan Bagi Bulan </label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="DD_PBB" style="width:100%; font-size:13px;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="sel_bagi_bulan" class="form-control select2 validate[optional]">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="lbl9_text" runat="server">  Tahun </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txt_tahu" runat="server" class="form-control validate[optional] uppercase"
                                                            MaxLength="4"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                    <div class="box-header with-border">
                            <h3 class="box-title" id="h3_tag3" runat="server">Maklumat Pendapatan</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="lbl10_text" runat="server">  Gaji Pokok (RM) </label>
                                    <div class="col-sm-8 text-right text-bold">
                                        <asp:label ID="txt_pokok"  runat="server" ></asp:label>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-8">
                                    <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Kempali" UseSubmitBehavior="false" Type="submit" onclick="Click_bck" />
                                    </div>
                                </div>
                            </div>
                                </div>
                                </div>
                             <div class="box-header with-border">
                            <h3 class="box-title" id="h3_tag4" runat="server">Elaun Tetap</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        
                            <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                  <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None">
                                                            <PagerStyle CssClass="pager" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="BIL" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                            ItemStyle-Width="150" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Jenis Elaun">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label1_1" runat="server" Text='<%# Eval("hr_elaun_desc") %>' CssClass="uppercase"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Amaun (RM)">
                                                                    <ItemStyle HorizontalAlign="Right" Width="15%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label1_2" runat="server" Text='<%# Eval("fxa_allowance_amt","{0:N}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                          
                                                        </asp:GridView>
               </div>
          </div>
                            <div class="box-header with-border">
                            <h3 class="box-title" id="h3_tag5" runat="server">Lain-Lain Elaun / Bayaran</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        
                            <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                    <asp:GridView ID="GridView2" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None">
                                                            <PagerStyle CssClass="pager" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="BIL" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                            ItemStyle-Width="150" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Jenis Elaun">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label2_1" runat="server" Text='<%# Eval("hr_elaun_desc") %>' CssClass="uppercase"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Amaun (RM)">
                                                                    <ItemStyle HorizontalAlign="Right" Width="15%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label2_2" runat="server" Text='<%# Eval("xta_allowance_amt","{0:N}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            
                                                        </asp:GridView>
               </div>
          </div>
                            <div class="box-header with-border">
                            <h3 class="box-title" id="h3_tag6" runat="server">Kerja Lebih Masa</h3>
                        </div>
                            <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                   
           <div class="col-md-12 box-body">
                                   <asp:GridView ID="GridView5" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None">
                                                            <PagerStyle CssClass="pager" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="BIL" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                      
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                            ItemStyle-Width="150" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="TARIKH">
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        
                                                                            <asp:Label ID="Label5_1" runat="server" Text='<%# Eval("otl_work_dt") %>' CssClass="uppercase"></asp:Label>
                                                                            <asp:Label ID="crt_dt" Visible="false" runat="server" Text='<%# Eval("otl_crt_dt") %>'
                                                                                CssClass="uppercase"></asp:Label>
                                                                        
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="JENIS KLM">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label5_2" runat="server" Text='<%# Eval("typeklm_desc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="JAM / UNIT">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label5_3" runat="server" Text='<%# Eval("otl_work_hour") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="JUMLAH (RM)">
                                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label5_4" runat="server" Text='<%# Eval("otl_ot_amt","{0:n}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                           
                                                        </asp:GridView>
               </div>
          </div>
                            <div class="box-header with-border">
                            <h3 class="box-title" id="h3_tag7" runat="server">Bonus</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->                        
                            <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                   <asp:GridView ID="GridView3" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None">
                                                            <PagerStyle CssClass="pager" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="BIL" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                            ItemStyle-Width="150" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="TARIKH MULA">
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                            <asp:Label ID="lb_bon1" runat="server" Text='<%# Eval("bns_eff_dt") %>' CssClass="uppercase"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="TARIKH AKHIR">
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                            <asp:Label ID="lb_bon1" runat="server" Text='<%# Eval("bns_end_dt") %>' CssClass="uppercase"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText=" Bonus Tahunan (RM)">
                                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lb_bon2" runat="server" Text='<%# Eval("bns_amt","{0:n}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText=" Bonus KPI (RM)">
                                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lb_bon3" runat="server" Text='<%# Eval("bns_kpi_amt","{0:n}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                          
                                                        </asp:GridView>
               </div>
          </div>

                               <div class="box-header with-border">
                            <h3 class="box-title">Lain-Lain</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->                        
                            <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                   <asp:GridView ID="GridView4" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None">
                                                            <PagerStyle CssClass="pager" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="BIL" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                            ItemStyle-Width="150" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="TAHUN">
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                            <asp:Label ID="lb_thn1" runat="server" Text='<%# Eval("tun_year") %>' CssClass="uppercase"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Bulan">
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                            <asp:Label ID="lb_thn2" runat="server" Text='<%# Eval("tun_month") %>' CssClass="uppercase"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="Sebab">
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                            <asp:Label ID="lb_thn3" runat="server" Text='<%# Eval("hr_tung_desc") %>' CssClass="uppercase"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText=" Jumlah (RM)">
                                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lb_thn4" runat="server" Text='<%# Eval("tun_amt","{0:n}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                               
                                                            </Columns>
                                                          
                                                        </asp:GridView>
               </div>
          </div>

                           <div class="box-body">&nbsp;</div>
                            <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                &nbsp;
                                </div>
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="lbl11_text" runat="server">  Pendapatan Kasar (RM) </label>
                                    <div class="col-sm-8 text-right text-bold">
                                        <asp:label ID="TextBox8" runat="server" class="validate[optional,custom[number]]"></asp:label>
                                                         <asp:TextBox ID="TextBox14" runat="server" class="form-control validate[optional]"
                                                            Visible="false"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox16" runat="server" class="form-control validate[optional]"
                                                            Visible="false"></asp:TextBox>
                                                        <asp:TextBox ID="et_amt" runat="server" Visible="false" class="form-control validate[optional]"></asp:TextBox>
                                                        <asp:TextBox ID="ll_amt" runat="server" Visible="false" class="form-control validate[optional]"></asp:TextBox>
                                                        <%--<asp:TextBox ID="tun_amt" runat="server" Visible="false" class="form-control validate[optional]"></asp:TextBox>--%>
                                                        <asp:TextBox ID="tt_dt" runat="server" Visible="false" class="form-control validate[optional]"></asp:TextBox>
                                                        <asp:TextBox ID="tt_yr" runat="server" Visible="false" class="form-control validate[optional]"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox5" Visible="false" style="text-align:right;" runat="server"></asp:TextBox>
                                                         <asp:TextBox ID="TextBox7" Visible="false" style="text-align:right;" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="TextBox6" Visible="false" style="text-align:right;" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>


                             <div class="box-header with-border">
                            <h3 class="box-title" id="h3_tag2" runat="server">Maklumat Ptotongan</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>

                            <div class="row">
                             <div class="col-md-12">
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="lbl12_text" runat="server">   No KWSP Ahli </label>
                                    <div class="col-sm-8 text-right">
                                        <asp:label ID="TextBox12" runat="server" class="validate[optional] uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="lbl13_text" runat="server">Caruman KWSP Ahli (RM)</label>
                                    <div class="col-sm-8 text-right">
                                        <asp:label ID="TextBox11" runat="server" class="validate[optional] "></asp:label>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="row">
                             <div class="col-md-12">
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="lbl14_text" runat="server">   No PERKESO Ahli </label>
                                    <div class="col-sm-8 text-right">
                                        <asp:label ID="TextBox10" runat="server" class=" validate[optional] uppercase"
                                                            MaxLength="10"></asp:label>
                                    </div>
                                </div>
                            </div>
                                
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="lbl15_text" runat="server"> PERKESO Ahli (RM)</label>
                                    <div class="col-sm-8 text-right">
                                        <asp:label ID="TextBox9" runat="server" class=" validate[optional]"></asp:label>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                            <div class="row">
                             <div class="col-md-12">
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="lbl16_text" runat="server">   No SIP Ahli </label>
                                    <div class="col-sm-8 text-right">
                                         <asp:label ID="TextBox17" runat="server" class="validate[optional] uppercase"
                                                            MaxLength="10"></asp:label>

                                    </div>
                                </div>
                            </div>
                                
                                 
                          <%--  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="lbl17_text" runat="server"> Potongan PCB (RM) </label>
                                    <div class="col-sm-8 text-right">
                                        <asp:label ID="TextBox13" runat="server" class="validate[optional]"
                                                            ></asp:label>
                                    </div>
                                </div>
                            </div>--%>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="Label1" runat="server">   SIP (RM) </label>
                                    <div class="col-sm-8 text-right">
                                         <asp:label ID="TextBox19" runat="server" class="validate[optional]"></asp:label>                                                          
                                         
                                    </div>
                                </div>
                            </div>
                                  <%--  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="lbl19_text" runat="server">  Lain-Lain Potongan (RM)  </label>
                                    <div class="col-sm-8 text-right">
                                        <asp:label ID="TextBox2" runat="server" class="validate[optional]"></asp:label>
                                         <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>--%>
                                 </div>
                                </div>

                          <%--   <div class="row">
                             <div class="col-md-12">
                              <div class="col-md-6 box-body">
                               &nbsp;
                            </div>
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="lbl18_text" runat="server">   Potongan CP 38 (RM) </label>
                                    <div class="col-sm-8 text-right">
                                         <asp:label ID="TextBox18" runat="server" class="validate[optional]"></asp:label>
                                                          <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                
                                 </div>
                                </div>--%>
                            <%--  <div class="row">
                             <div class="col-md-12">
                              <div class="col-md-6 box-body">
                               &nbsp;
                            </div>
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="Label1" runat="server">   SIP (RM) </label>
                                    <div class="col-sm-8 text-right">
                                         <asp:label ID="TextBox19" runat="server" class="validate[optional]"></asp:label>                                                          
                                    </div>
                                </div>
                            </div>
                                
                                 </div>
                                </div>--%>
                              <div class="row">
                             <div class="col-md-12">
                              <div class="col-md-6 box-body">
                               &nbsp;
                            </div>
                        
                       <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="lbl19_text" runat="server">  Lain-Lain Potongan (RM)  </label>
                                    <div class="col-sm-8 text-right">
                                        <asp:label ID="TextBox2" runat="server" class="validate[optional]"></asp:label>
                                         <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="row">
                             <div class="col-md-12">
                              <div class="col-md-6 box-body">
                               &nbsp;
                            </div>
                        
                       <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="Label3" runat="server">  PCB (RM)  </label>
                                    <div class="col-sm-8 text-right">
                                        <asp:label ID="Label4" runat="server" class="validate[optional]"></asp:label>
                                         
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                 <div class="row">
                             <div class="col-md-12">
                              <div class="col-md-6 box-body">
                               &nbsp;
                            </div>
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="lbl18_text" runat="server">   CP 38 (RM) </label>
                                    <div class="col-sm-8 text-right">
                                         <asp:label ID="TextBox18" runat="server" class="validate[optional]"></asp:label>                                                          
                                    </div>
                                </div>
                            </div>
                                
                                 </div>
                                </div>
                            <div class="row">
                             <div class="col-md-12">
                               <div class="col-md-6 box-body">
                                &nbsp;
                                </div>
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="lbl20_text" runat="server">  Jumlah Potongan (RM)</label>
                                    <div class="col-sm-8 text-right">
                                        <asp:label ID="TextBox3" runat="server" class="validate[optional]"></asp:label>
                                                        <asp:TextBox ID="TextBox4" runat="server" Visible="false" class="validate[optional]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                <hr /> 
                                 <div class="row">
                             <div class="col-md-12">
                                    <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="Label5" runat="server">   No KWSP Majikan </label>
                                    <div class="col-sm-8 text-right">
                                         <asp:label ID="Label6" runat="server" class="validate[optional] uppercase"
                                                            ></asp:label>

                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="lbl21_text" runat="server">  Caruman KWSP Majikan(RM)  </label>
                                    <div class="col-sm-8 text-right">
                                        <asp:label ID="TextBox23" runat="server" class="validate[optional]"></asp:label>
                                                        <asp:TextBox ID="TextBox25" runat="server" Visible="false" class="form-control validate[optional]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                                 
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="Label7" runat="server">   No PERKESO Majikan </label>
                                    <div class="col-sm-8 text-right">
                                         <asp:label ID="Label8" runat="server" class="validate[optional] uppercase" MaxLength="12"
                                                            ></asp:label>

                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="lbl22_text" runat="server">   PERKESO Majikan (RM)  </label>
                                    <div class="col-sm-8 text-right">
                                       <asp:label ID="TextBox24" runat="server" class="validate[optional]"></asp:label>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                              <div class="row">
                             <div class="col-md-12">
                               <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="Label9" runat="server">   No SIP Majikan </label>
                                    <div class="col-sm-8 text-right">
                                         <asp:label ID="Label10" runat="server" class="validate[optional] uppercase" MaxLength="12"></asp:label>

                                    </div>
                                </div>
                            </div>
                       <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="Label2" runat="server">  SIP Majikan (RM)  </label>
                                    <div class="col-sm-8 text-right">
                                        <asp:label ID="TextBox27" runat="server" class="validate[optional]"></asp:label>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <hr />
                          
                            <div class="row">
                             <div class="col-md-12">
                                 <div class="col-md-6 box-body">
                                &nbsp;
                                </div>
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="lbl23_text" runat="server">  Pendapatan Bersih (RM)   </label>
                                    <div class="col-sm-8 text-right text-bold">
                                       <asp:label ID="TextBox20"  runat="server" class="validate[optional]"></asp:label>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                <asp:TextBox ID="lbl_name" runat="server" class="form-control validate[optional] uppercase" Visible="false"></asp:TextBox>
                                <asp:Button ID="btn_simp" runat="server" class="btn btn-danger" Text="Simpan" UseSubmitBehavior="false" OnClick="btn_simp_Click" />                                
                                <asp:Button ID="Button2" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" OnClick="Click_bck"  />
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
             
    </asp:UpdatePanel>
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
  
</asp:Content>



