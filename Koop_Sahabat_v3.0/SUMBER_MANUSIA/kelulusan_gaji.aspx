<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/kelulusan_gaji.aspx.cs" Inherits="kelulusan_gaji" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>  Kelulusan Gaji </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>  Sumber Manusia </a></li>
                            <li class="active"> Maklumat Penggajian  </li>
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
                            <h3 class="box-title">Carian Maklumat Gaji </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                              <div class="col-md-12">
                            <div class="col-md-10 box-body">
                                <div class="form-group">
                                    <div class="col-sm-4 col-xs-12 ">
                                         <%--<asp:TextBox ID="txt_tahun" runat="server" CssClass="form-control validate[optional,custom[number]] uppercase"></asp:TextBox>--%>
                                                                <asp:DropDownList style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"
                                                                    ID="txt_tahun">
                                                                    <%--onselectedindexchanged="dd_kat_SelectedIndexChanged">--%>
                                                                </asp:DropDownList>
                                    </div>
                                      <div class="col-sm-4 col-xs-12 mob-view-top-padd">
                                           <asp:DropDownList style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"
                                                                    ID="DD_bulancaruman">
                                                                    <%--onselectedindexchanged="dd_kat_SelectedIndexChanged">--%>
                                                                </asp:DropDownList>
                                          </div>
                                    <div class="col-sm-4 col-xs-12 mob-view-top-padd">
                                         <label><asp:CheckBox ID="chk_assign_rkd" runat="server" /> &nbsp;Verified</label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                           <asp:Button ID="Button2" runat="server" class="btn btn-primary" Text="Carian" OnClick="BindGridview"/>
                                                                <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" usesubmitbehavior="false" OnClick="Reset_btn"/>
                                        </div>
                                    <div class="col-sm-4 col-xs-12 mob-view-top-padd">
                                        </div>
                                </div>
                            </div>
                                 
                                  </div>
                                </div>
                           
                            <hr />
                          
                            <div id="show_cnt1" runat="server">
                           <%--  <div class="box-header with-border">
                            <h3 class="box-title">Senarai Kelulusan Gaji</h3>
                        </div>--%>
                        <!-- /.box-header -->
                        <!-- form start -->
                        
                               <div id="GridViewContainer" class="GridViewContainer" style="width:100%;height:500px;" >               
                              <div class="dataTables_wrapper form-inline dt-bootstrap">                                   
         <div class="col-md-12 box-body">
                                   <%--  <asp:GridView ID="gvSelected" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="30" ShowFooter="false" GridLines="None"  OnPageIndexChanging="gvSelected_PageIndexChanging">
                                         <PagerStyle CssClass="pager" />
                                        <Columns>
                                        <asp:TemplateField HeaderText="BIL"  ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Nama Organization"> 
                                                <ItemStyle HorizontalAlign="Left" />    
                                            <ItemTemplate>  
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("org_name") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                       <asp:TemplateField HeaderText="No Kakitangan">
                                            <ItemTemplate>  
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("inc_staff_no") %>'></asp:Label> 
                                                <asp:Label ID="Label4_mnth" Visible="false" runat="server" Text='<%# Bind("inc_month") %>'></asp:Label>   
                                                <asp:Label ID="Label1_org_id" Visible="false" runat="server" Text='<%# Bind("inc_org_id") %>'></asp:Label>   
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Nama Kakitangan"> 
                                                <ItemStyle HorizontalAlign="Center" />    
                                            <ItemTemplate>  
                                                <asp:Label ID="Label2_name" runat="server" Text='<%# Bind("stf_name") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tahun"> 
                                                <ItemStyle HorizontalAlign="Center" />    
                                            <ItemTemplate>  
                                                <asp:Label ID="Label2_yr" runat="server" Text='<%# Bind("inc_year") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Bulan">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("hr_month_desc") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Gaji Pokok" ItemStyle-HorizontalAlign="Right">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("inc_salary_amt","{0:n}") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="STATUS KELULUSAN">  
                                                <ItemStyle HorizontalAlign="Center" />  
                                            <ItemTemplate>  
                                                <asp:RadioButton ID="chkSelect_1" Checked="true" Text="&nbsp;Sah" runat="server" GroupName="status" />
                                                &nbsp;&nbsp;
                                                <asp:RadioButton ID="chkSelect_2" Text="&nbsp;Tidak Sah" runat="server" GroupName="status" />
                                            </ItemTemplate>  
                                                </asp:TemplateField>  
                                        </Columns> 
                                         <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />                                                       
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />
                              </asp:GridView>--%>
              <asp:GridView ID="gvCustomers" runat="server" class="GridView table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="50" ShowFooter="true" GridLines="None"
                                        DataKeyNames="inc_staff_no" onrowdatabound="gvCustomers_RowDataBound" OnDataBound="GridView1_DataBound" OnPageIndexChanging="gvSelected_PageIndexChanging">
                                     <PagerStyle CssClass="pager" />
                     <HeaderStyle CssClass="FrozenHeader" />
                                        <Columns>
                                             <asp:TemplateField HeaderText="Permission" ItemStyle-HorizontalAlign="center" ItemStyle-Width="3%" ItemStyle-CssClass="FrozenCell" HeaderStyle-CssClass="FrozenCell">
                                                                       <HeaderTemplate>
                                                                           STATUS KELULUSAN<br/>
                                            <asp:CheckBox ID="chkAll" runat="server" Text="" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged"
                                                        ItemStyle-Width="150" />
                                            </HeaderTemplate>  
                                                                    <ItemTemplate>                                                                        
                                                                           <asp:CheckBox ID="chkSelect"  runat="server" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                          <asp:TemplateField HeaderText="BIL">
                                         <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                            ItemStyle-Width="150" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText="NO KAKITANGAN" ItemStyle-Width="150px">  
                                            <ItemTemplate>  
                                                <asp:Label ID="labstf" runat="server" Text='<%# Bind("inc_staff_no") %>'></asp:Label>  
                                                   <asp:Label ID="ss_val1" Visible="false" runat="server" Text='<%# Bind("inc_month") %>'></asp:Label>  
                                                <asp:Label ID="ss_val2" Visible="false" runat="server" Text='<%# Bind("inc_year") %>'></asp:Label>  
                                                <asp:Label ID="Label1_org_id" Visible="false" runat="server" Text='<%# Bind("inc_org_id") %>'></asp:Label>   
                                                <asp:Label ID="inc_asts" Visible="false" runat="server" Text='<%# Bind("inc_app_sts") %>'></asp:Label>   
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                            <asp:BoundField ItemStyle-Width="150px" DataField="stf_name" HeaderText="NAMA" />
                                              
                                                
                                           <%--  <asp:BoundField ItemStyle-Width="150px" DataField="inc_staff_no" HeaderText="NO KAKITANGAN" />--%>
                                            
                                             <asp:TemplateField HeaderText="GRED" Visible="false" ItemStyle-Width="100px">  
                                            <ItemTemplate>  
                                                <asp:Label ID="lagred" runat="server" Text='<%# Bind("inc_grade_cd") %>'></asp:Label>  
                                             
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                          <asp:TemplateField HeaderText="GAJI POKOK (RM)" ItemStyle-Width="100px">  
                                          <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="labsal" runat="server" Text='<%# Eval("inc_salary_amt","{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                 <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                  <asp:Label ID="ftr_001" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="ELAUN TETAP (RM)" ItemStyle-Width="150px">
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                <ItemTemplate >
                                                    <asp:Panel ID="pnlProducts" runat="server" Visible="true" Style="position: relative" >
                                                        <asp:GridView ID="gvProducts" runat="server" AutoGenerateColumns="false" PageSize="10"
                                                            AllowPaging="true" CssClass="Nested_ChildGrid" OnPageIndexChanging="gvProducts_PageIndexChanging" >
                                                            <Columns>
                                                                <asp:BoundField ItemStyle-Width="300px" DataField="hr_elau_desc" HeaderText="Keterangan" />
                                                                <asp:BoundField ItemStyle-Width="300px" DataField="fxa_allowance_amt" ItemStyle-HorizontalAlign="Right" HeaderText="Amaun (RM)" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                     <asp:Label ID="txt_nama" runat="server" Text='<%# Eval("inc_cumm_fix_allwnce_amt" ,"{0:n}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                  <FooterTemplate>
                                                  <asp:Label ID="ftr_002" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="LAIN-LAIN ELAUN (RM)" ItemStyle-Width="150px">
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Panel ID="pnlProducts1" runat="server" Visible="true" Style="position: relative">
                                                        <asp:GridView ID="gvProducts1" runat="server" AutoGenerateColumns="false" PageSize="10"
                                                            AllowPaging="true" CssClass="Nested_ChildGrid" OnPageIndexChanging="gvProducts1_PageIndexChanging">
                                                            <Columns>
                                                                <asp:BoundField ItemStyle-Width="300px" DataField="hr_elau_desc" HeaderText="Keterangan" />
                                                                <asp:BoundField ItemStyle-Width="300px" DataField="xta_allowance_amt" ItemStyle-HorizontalAlign="Right" HeaderText="Amaun (RM)" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                     <asp:Label ID="txt_nama1" runat="server" Text='<%# Eval("inc_cumm_xtra_allwnce_amt" ,"{0:n}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                 <FooterTemplate>
                                                  <asp:Label ID="ftr_003" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="KLM (RM)" ItemStyle-Width="100px">  
                                              <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="labot" runat="server" Text='<%# Bind("inc_ot_amt","{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                  <FooterStyle HorizontalAlign="Right" />
                                                   <FooterTemplate>
                                                  <asp:Label ID="ftr_005" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Bonus Tahunan (RM)" ItemStyle-Width="100px">  
                                              <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="labot_bonus" runat="server" Text='<%# Bind("inc_bonus_amt","{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                 <FooterStyle HorizontalAlign="Right" />
                                                   <FooterTemplate>
                                                  <asp:Label ID="ftr_004" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Bonus KPI (RM)" ItemStyle-Width="100px">  
                                              <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="labot_kpi" runat="server" Text='<%# Bind("inc_kpi_bonus_amt","{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                     <FooterStyle HorizontalAlign="Right" />
                                              <FooterTemplate>
                                                  <asp:Label ID="ftr_004_2" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Lain-Lain (RM)" ItemStyle-HorizontalAlign="Right">   
                                            <ItemTemplate>  
                                                 <asp:Label ID="tung_amt" runat="server" Text='<%# Bind("inc_tunggakan_amt","{0:n}") %>'></asp:Label>
                                            </ItemTemplate>  
                                                  <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                  <asp:Label ID="ftr_006_1" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                              <asp:TemplateField HeaderText="PENDAPATAN KASAR (RM)" ItemStyle-Width="100px"> 
                                           <ItemStyle HorizontalAlign="Right"></ItemStyle> 
                                            <ItemTemplate>  
                                                <asp:Label ID="labgross" runat="server" Text='<%# Bind("inc_gross_amt" ,"{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                               <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                  <asp:Label ID="ftr_012" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                                
                                           <asp:TemplateField HeaderText="CARUMAN KWSP (RM)" ItemStyle-Width="100px"> 
                                           <ItemStyle HorizontalAlign="Right"></ItemStyle> 
                                            <ItemTemplate>  
                                                <asp:Label ID="lapkwap" runat="server" Text='<%# Bind("inc_kwsp_amt" ,"{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                               <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                  <asp:Label ID="ftr_007" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                              
                                          <asp:TemplateField HeaderText="POTONGAN PERKESO (RM)" ItemStyle-Width="100px">  
                                          <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="labper" runat="server" Text='<%# Bind("inc_perkeso_amt" ,"{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                              <FooterStyle HorizontalAlign="Right" />
                                               <FooterTemplate>
                                                  <asp:Label ID="ftr_008" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                                 
                                             <asp:TemplateField HeaderText="POTONGAN SIP (RM)" ItemStyle-Width="100px">  
                                          <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="labSIP" runat="server" Text='<%# Bind("inc_SIP_amt" ,"{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                 <FooterStyle HorizontalAlign="Right" />
                                                  <FooterTemplate>
                                                  <asp:Label ID="ftr_009" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                              <asp:TemplateField HeaderText="LAIN-LAIN POTONGAN (RM)" ItemStyle-Width="150px">
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Panel ID="pnlProducts3" runat="server" Visible="true" Style="position: relative">
                                                        <asp:GridView ID="gvProducts3" runat="server" AutoGenerateColumns="false" PageSize="10"
                                                            AllowPaging="true" CssClass="Nested_ChildGrid" OnPageIndexChanging="gvProducts3_PageIndexChanging">
                                                            <Columns>
                                                                <asp:BoundField ItemStyle-Width="300px" DataField="hr_poto_desc" HeaderText="Keterangan" />
                                                                <asp:BoundField ItemStyle-Width="300px" DataField="xta_allowance_amt" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" HeaderText="Amaun (RM)" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                     <asp:Label ID="txt_nama3" runat="server" Text='<%# Eval("inc_ded_amt" ,"{0:n}") %>'></asp:Label>
                                                </ItemTemplate>
                                                 <FooterStyle HorizontalAlign="Right" />
                                                 <FooterTemplate>
                                                  <asp:Label ID="ftr_014" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                          <asp:TemplateField HeaderText="PCB (RM)" ItemStyle-Width="100px">  
                                          <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="labpcb" runat="server" Text='<%# Bind("inc_pcb_amt" ,"{0:n}") %>'></asp:Label>  
                                            </ItemTemplate> 
                                              <FooterStyle HorizontalAlign="Right" />
                                               <FooterTemplate>
                                                  <asp:Label ID="ftr_010" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate> 
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="CP 38 (RM)" ItemStyle-Width="100px">  
                                              <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="labot_cp" runat="server" Text='<%# Bind("inc_cp38_amt","{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                     <FooterStyle HorizontalAlign="Right" />
                                                      <FooterTemplate>
                                                  <asp:Label ID="ftr_011" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                            
                                             <asp:TemplateField HeaderText="Jumlah Potongan Pendapatan (RM)" ItemStyle-Width="100px">  
                                              <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="labot_ded" runat="server" Text='<%# Bind("inc_total_deduct_amt","{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                     <FooterStyle HorizontalAlign="Right" />
                                                      <FooterTemplate>
                                                  <asp:Label ID="ftr_006" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                         
                                        
                                            
                                              <asp:TemplateField HeaderText="PENDAPATAN BERSIH (RM)" ItemStyle-Width="100px" ItemStyle-Font-Size="13px" ItemStyle-Font-Bold>  
                                          <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="labnet" runat="server" Text='<%# Bind("inc_nett_amt" ,"{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                              <FooterStyle HorizontalAlign="Right" />
                                               <FooterTemplate>
                                                  <asp:Label ID="ftr_013" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                           <asp:TemplateField HeaderText="CARUMAN KWSP MAJIKAN (RM)" ItemStyle-Width="100px">  
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lapkwap1" runat="server" Text='<%# Bind("inc_emp_kwsp_amt" ,"{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                    <FooterStyle HorizontalAlign="Right" />
                                             <FooterTemplate>
                                                  <asp:Label ID="ftr_007_2" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PERKESO MAJIKAN (RM)" ItemStyle-Width="100px">  
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="labper1" runat="server" Text='<%# Bind("inc_emp_perkeso_amt" ,"{0:n}") %>'></asp:Label>  
                                            </ItemTemplate> 
                                                <FooterStyle HorizontalAlign="Right" />
                                             <FooterTemplate>
                                                  <asp:Label ID="ftr_008_2" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate> 
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="SIP MAJIKAN (RM)" ItemStyle-Width="100px">  
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="labSIP1" runat="server" Text='<%# Bind("inc_emp_SIP_amt" ,"{0:n}") %>'></asp:Label>  
                                            </ItemTemplate> 
                                                     <FooterStyle HorizontalAlign="Right" />
                                             <FooterTemplate>
                                                  <asp:Label ID="ftr_009_2" runat="server" Text="0.00"  ></asp:Label>
                                                </FooterTemplate> 
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


