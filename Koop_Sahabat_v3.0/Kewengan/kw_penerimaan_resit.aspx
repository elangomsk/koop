<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Kewengan/kw_terimaan_resit.aspx.cs" Inherits="kw_terimaan_resit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

     <script type="text/javascript">
         $().ready(function () {
             var today = new Date();
             var preYear = today.getFullYear() - 1;
             var curYear = today.getFullYear() - 0;
        
             $('.datepicker2').datepicker({
                 format: 'dd/mm/yyyy',
                 autoclose: true,
                 inline: true,
                 startDate: new Date($("#<%=start_dt1.ClientID %>").val()),
                 endDate: new Date($("#<%=end_dt1.ClientID %>").val())
             }).on('changeDate', function (ev) {
                 (ev.viewMode == 'days') ? $(this).datepicker('hide') : '';
             });
            

            <%-- $("#<%=pp6.ClientID %>").click(function () {
                 $("#<%=hd_txt.ClientID %>").text("INVOIS");
             });
             $("#<%=pp1.ClientID %>").click(function () {
                 $("#<%=hd_txt.ClientID %>").text("RESIT");
             });
             $("#<%=pp2.ClientID %>").click(function () {
                 $("#<%=hd_txt.ClientID %>").text("NOTA KREDIT");
             });
             $("#<%=pp3.ClientID %>").click(function () {
                 $("#<%=hd_txt.ClientID %>").text("NOTA DEBIT");
             });--%>
         });

     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <%-- <asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0;
                right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                <span style="border-width: 0px; position: fixed; font-weight: bold; padding: 50px;
                    background-color: #FFFFFF; font-size: 16px; left: 40%; top: 40%;">Sila Tunggu. Rekod
                    Sedang Diproses ...</span>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
    <asp:ScriptManager ID="ScriptManagerCalendar" ScriptMode="Release" runat="server" >
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>Buku Tunai</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Kewangan</a></li>
                            <li class="active">Buku Tunai</li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
       <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <asp:Label ID="hd_txt" Visible="false" runat="server"></asp:Label>
                        <div class="box-header with-border">
                            <h3 class="box-title"><asp:Label ID="ps_lbl4" runat="server">Terimaan (Resit)</asp:Label></h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                         
                        <div class="form-horizontal">
                         <div class="col-md-12">
                         
                             <div class="box-body">&nbsp;</div>
                                                  <%--  <div class="panel" style="width: 100%;">--%>
                                                        <div id="Div1" class="nav-tabs-custom" role="tabpanel" runat="server">
                                                            <!-- Nav tabs -->
                                                            <ul class="s1 nav nav-tabs" role="tablist" style="display:none;">
                                                            <li id="pp6" runat="server" ><a href="#ContentPlaceHolder1_p6" aria-controls="p6" role="tab" data-toggle="tab"><strong><asp:Label ID="ps_lbl6" runat="server"></asp:Label></strong></a>
                                                               
                                                            </li>
                                                                <li id="pp1" runat="server" class="active"><a href="#ContentPlaceHolder1_p1" aria-controls="p1" role="tab" data-toggle="tab"><strong><asp:Label ID="ps_lbl7" runat="server"></asp:Label></strong></a></li>
                                                                 <li id="pp2" runat="server"><a href="#ContentPlaceHolder1_p2" aria-controls="p2" role="tab" data-toggle="tab"><strong><asp:Label ID="ps_lbl8" runat="server"></asp:Label></strong></a></li>
                                                                  <li id="pp3" runat="server"><a href="#ContentPlaceHolder1_p3" aria-controls="p3" role="tab" data-toggle="tab"><strong><asp:Label ID="ps_lbl9" runat="server"></asp:Label></strong></a></li>
                                                            </ul>
                                                            <!-- Tab panes -->
                                                            <div class="tab-content">
                                                            <div role="tabpanel" class="tab-pane" runat="server" id="p6">
                                                                 <div class="col-md-12 table-responsive"  style="overflow:auto;">
                                                                     <div id="Div3"  runat="server">
                                                                    <div id="Div9"  runat="server">
                                                                      
                                                                     <fieldset class="col-md-12">
                                                                     <legend><asp:Label ID="ps_lbl10" runat="server"></asp:Label></legend>                                                                                                                      
                                                                          <div class="row">
                                                                         <div class="col-md-12">
                                                                        <div class="col-md-3 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl11" runat="server"></asp:Label> </label>
                                                                                <div class="col-sm-7">
                                                                                     <asp:DropDownList ID="ddpela" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                                                                            </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-3 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl12" runat="server"></asp:Label> </label>
                                                                                <div class="col-sm-7">
                                                                                   <asp:TextBox ID="TextBox7" runat="server" class="form-control uppercase"></asp:TextBox> 
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             <div class="col-md-3 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl13" runat="server"></asp:Label></label>
                                                                                <div class="col-sm-7">
                                                                                      <div class="input-group">
                                                                                  <asp:TextBox ID="TextBox10" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY" ></asp:TextBox> 
                                                                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                                          
                                             </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             <div class="col-md-3 box-body">
                                                                            <div class="form-group">
                                                                                <div class="col-sm-12">
                                                                                    <asp:CheckBox ID="chk_invois" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                   <asp:Button ID="but" runat="server" class="btn btn-danger" Text="Carian" UseSubmitBehavior="false" onclick="but_Click"    />
                                                                                    <asp:Button ID="Button6" runat="server" class="btn btn-primary" Text="Tambah" Type="submit" onclick="Button6_Click" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             </div>
                                                                     </div>
                                                            
                                                                       </fieldset>
                                                                
                                                                    <div class="box-body">&nbsp;</div>
                                                                          <div class="dataTables_wrapper form-inline dt-bootstrap" >
                                      <div class="row" style="overflow:auto;">
           <div class="col-md-12 box-body">
            <asp:gridview ID="Gridview2" runat="server"  class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting" OnPageIndexChanging="gvSelected_PageIndexChanging_g3"  >
            <Columns>
                  <asp:TemplateField HeaderText="BIL">  
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
            <asp:TemplateField HeaderText="No Invois" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                  <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("no_invois") %>'  CommandArgument='<%# Eval("no_invois")%>' CommandName="Add"  onclick="lblSubItemName_Click" Font-Bold Font-Underline></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:BoundField DataField="tarikh_invois" HeaderText="Tarikh Invois" ItemStyle-HorizontalAlign="Center" />
              <asp:BoundField DataField="tarikh" HeaderText="Tarikh Dibayar" ItemStyle-HorizontalAlign="Center" />
                 <asp:BoundField DataField="FINAL_DATE" HeaderText="Tarikh Matang" ItemStyle-HorizontalAlign="Center" />
               <asp:BoundField DataField="Ref_nama_syarikat" HeaderText="Nama Pelanggan" ItemStyle-HorizontalAlign="Center"  />
                <asp:BoundField DataField="jumlah" HeaderText="Jumlah (RM)" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:n}" />
                 <asp:BoundField DataField="jumlah_bayaran" HeaderText="Jumlah yang telah dibayar (RM)" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n}" />
                   <asp:BoundField DataField="baki" HeaderText="Baki (RM)" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n}" />
             
            </Columns>
        <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                       <%-- <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />--%>
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />
        </asp:gridview>
                                                                   </div>
                                                                   
                                          </div>
                                                                              </div>
                                                                        </div>
                                                                   <div id="Div7" role="tabpanel11" runat="server">
                                                                       
                                                                        <div class="row">
                                                                         <div class="col-md-12">
                                                                        <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl16" runat="server"></asp:Label> <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                   <asp:DropDownList ID="ddpela1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                                </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl17" runat="server"></asp:Label> <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                      <div class="input-group">
                                                                                  <asp:TextBox ID="txttarikhinvois1" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker2 mydatepickerclass2" placeholder="DD/MM/YYYY" ></asp:TextBox> 
                                                                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-4 box-body" id="inv_tab1" runat="server" visible="false">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl18" runat="server"></asp:Label> <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                   <asp:TextBox ID="txtnoinvois1" runat="server" class="form-control"></asp:TextBox> 
                                                                                    <asp:TextBox ID="txtnoinvois1_1" runat="server" Visible="false" class="form-control"></asp:TextBox> 
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             
                                                                            
                                                                             </div>
                                                                     </div>
                                                                  
                                                                     <div class="row">
                                                                         <div class="col-md-12">
                                                                        <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl19" runat="server"></asp:Label> <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                   <asp:DropDownList ID="ddpro" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                                </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"> <asp:Label ID="ps_lbl20" runat="server"></asp:Label> <span class="style1">*</span></label>                        
                                                                                <div class="col-sm-7">
                                                                                   <asp:DropDownList ID="dddays" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                                  <asp:ListItem Value="">--- PILIH ---</asp:ListItem>
                                                                                 <asp:ListItem Value="30">30</asp:ListItem>
                                                                                  <asp:ListItem Value="60">60</asp:ListItem>
                                                                                  <asp:ListItem Value="90">90</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             </div>
                                                                     </div>
                                                                            <div class="box-body">&nbsp;</div>
                                                                       <div class="nav-tabs-custom">
                                                                     <ul class="s1 nav nav-tabs" role="tablist">
                                                            <li id="Li4" runat="server" class="active" ><a href="#ContentPlaceHolder1_pf1" aria-controls="pf1" role="tab" data-toggle="tab"><asp:Label ID="ps_lbl21" runat="server"></asp:Label></a> </li>

                                                            <li id="Li2" runat="server" ><a href="#ContentPlaceHolder1_pf2" aria-controls="pf2" role="tab" data-toggle="tab"><asp:Label ID="ps_lbl22" runat="server"></asp:Label></a> </li>
                                                               
                                                                
                                                            </ul>
                                                          
                                                              
                                                               <div class="tab-content">
                                                            <div role="tabpanel11" class="tab-pane active uppercase" runat="server" id="pf1">
                                      
          <%-- <div class="col-md-12 table-responsive uppercase"  style="overflow:auto;">--%>
         <asp:gridview ID="Gridview1" runat="server"  class="table  table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="20" ShowFooter="true" GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting" onrowdatabound="Gridview1_RowDataBound" >
                 <Columns>
           
             <asp:TemplateField HeaderText="Kod Akaun (Kredit)" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                     <asp:DropDownList ID="ddkod" style="width:100%;" runat="server" class="form-control select2 validate[optional]">    </asp:DropDownList>
                </ItemTemplate>
                 <FooterTemplate>
                 <asp:Button ID="ButtonAdd" runat="server" Text="Add New Row" 
                        onclick="ButtonAdd_Click" />
                </FooterTemplate>
            </asp:TemplateField>
                      <asp:TemplateField HeaderText="Rujukan" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:TextBox ID="txt_rujukan" CssClass="form-control uppercase" MaxLength="100" Width="100px" runat="server" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
                      <asp:TemplateField HeaderText="NO PO" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:TextBox ID="txt_po" CssClass="form-control uppercase" MaxLength="100" Width="100px" runat="server" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox1" CssClass="form-control uppercase" Width="100px" runat="server" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Keterangan" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox2" CssClass="form-control uppercase" runat="server" TextMode="MultiLine" Width="180px" Height="40px"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
                      <asp:TemplateField HeaderText="Kuantiti" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server"  CssClass="form-control"  placeholder="0" OnTextChanged="QtyChanged_kty" AutoPostBack="true"  Width="70px"   ></asp:TextBox>
                </ItemTemplate>
              
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Harga/unit"  ItemStyle-HorizontalAlign="Right">
                <ItemTemplate >
                    <asp:TextBox ID="TextBox3" CssClass="form-control"  placeholder="0.00" runat="server" Width="80px" OnTextChanged="QtyChanged" AutoPostBack="true"  style="text-align:right;"  ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
                     <asp:TemplateField HeaderText="Disk" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:TextBox ID="Txtdis" runat="server"  CssClass="form-control"  placeholder="0.00" Width="80px"   OnTextChanged="DISChanged" AutoPostBack="true" style="text-align:right;"    ></asp:TextBox>
                </ItemTemplate>
              
            </asp:TemplateField>
                       <asp:TemplateField HeaderText="Jumlah tidak termasuk CBP(RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="Label1" CssClass="form-control" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label3" CssClass="form-control"  runat="server" Text="0.00"></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Tax" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server"  OnCheckedChanged="ChckedChangedInv"  AutoPostBack="true"  />
                </ItemTemplate>
            </asp:TemplateField>
                       <asp:TemplateField HeaderText="Caj Perkhidmatan (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:DropDownList ID="ddcukaioth" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"  onselectedindexchanged="ddcukaioth_SelectedIndexChanged" AutoPostBack="true"  >    </asp:DropDownList>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label7" CssClass="form-control"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
                      <asp:TemplateField HeaderText="Caj Perkhidmatan" ItemStyle-HorizontalAlign="Right"  Visible="false">
                <ItemTemplate>
                    <asp:Label ID="Label10" CssClass="form-control" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label11" CssClass="form-control"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="GST (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:DropDownList ID="ddcukaiinv" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" onselectedindexchanged="ddcukaiinv_SelectedIndexChanged" AutoPostBack="true" >    </asp:DropDownList>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label4" CssClass="form-control"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
                       <asp:TemplateField HeaderText="GST Amt" ItemStyle-HorizontalAlign="Right" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="Label8" CssClass="form-control" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label9" CssClass="form-control"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
                      <asp:TemplateField HeaderText="Jumlah Termasuk CBP" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label5" CssClass="form-control" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label6" CssClass="form-control"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            
            </Columns>
          <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                      <%--  <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />--%>
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />                                                            
        </asp:gridview>

        <asp:gridview ID="Gridview3" runat="server" CssClass="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="20" ShowFooter="true" GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting" onrowdatabound="Gridview3_RowDataBound" >
            <Columns>
             <asp:TemplateField HeaderText="Kod Akaun" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                 <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("kod_akauan") %>' Visible = "false" />
                    <asp:Label ID="ddkoddup" runat="server" CssClass="uppercase" Text='<%# Eval("nama_akaun") %>' />
                     <%--<asp:DropDownList ID="ddkoddup" runat="server" Width="150px" class="form-control uppercase">    </asp:DropDownList>--%>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:BoundField DataField="rujukan" HeaderText="Rujukan" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="no_po" HeaderText="NO PO" HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="center" />
                <asp:BoundField DataField="item" HeaderText="Item" ItemStyle-HorizontalAlign="Left" />
              <asp:BoundField DataField="keterangan" HeaderText="Keterangan" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="180px"  />
               <asp:BoundField DataField="unit" HeaderText="Harga/unit" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right"  />
                <asp:BoundField DataField="quantiti" HeaderText="Kuantiti" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" />
                  <asp:BoundField DataField="discount" HeaderText="Disk" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" />
                 <asp:BoundField DataField="jumlah" HeaderText="Jumlah tidak termasuk CBP(RM)" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" />
             <asp:TemplateField HeaderText="Tax" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# bool.Parse(Eval("tax").ToString()) %>' Enable='<%# !bool.Parse(Eval("tax").ToString()) %>' />
                </ItemTemplate>
            </asp:TemplateField>
           <asp:TemplateField HeaderText="Caj Perkhidmatan (%)" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                 <asp:Label ID="lblgstoth" runat="server" Text='<%# Eval("othgsttype") %>' Visible = "false" />
                    <asp:Label ID="ddtaxoth" runat="server" Text='<%# Eval("othgstname") %>' />
                     <%--<asp:DropDownList ID="ddtaxoth" runat="server" class="form-control uppercase">    </asp:DropDownList>--%>
                </ItemTemplate>
            </asp:TemplateField>
                <asp:BoundField DataField="othgstjumlah" HeaderText="Caj Perkhidmatan (RM)" ItemStyle-Width="5%" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" />
             <asp:TemplateField HeaderText="gst (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                 <asp:Label ID="lblgst" runat="server" Text='<%# Eval("gsttype") %>' Visible = "false" />
                     <asp:Label ID="ddtax" runat="server" Text='<%# Eval("gstname") %>' />
                     <%--<asp:DropDownList ID="ddtax" runat="server" class="form-control uppercase">    </asp:DropDownList>--%>
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:BoundField DataField="gstjumlah" HeaderText="gst (RM)" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" />
                  <asp:BoundField DataField="Overall" HeaderText="Jumlah Termasuk CBP" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" />
                 <asp:BoundField DataField="Baki" HeaderText="Baki Invois (RM)" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" />
            </Columns>
            <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                      <%--  <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />--%>
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />                                                             
           
        </asp:gridview>
               
              
                                         
                                                                <div class="box-body">&nbsp;</div>
                                                          <div class="row">
                                                                                 <div class="col-md-12">
                                                                                <div class="col-md-8 box-body">&nbsp;</div>
                                                                                     <div class="col-md-4 box-body">
                                                                                    <div class="form-group">
                                                                                        <label for="inputEmail3" class="col-sm-6 control-label"><asp:Label ID="ps_lbl23" runat="server"></asp:Label></label>
                                                                                        <div class="col-sm-6">
                                                                                          <asp:TextBox ID="TextBox14" runat="server" class="form-control" style="text-align:right;" ></asp:TextBox> 
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                     </div>
                              </div>
                                                                   
                                                  <div class="box-body">&nbsp;</div>
                                                                <div class="row">
                             <div class="col-md-12" style="text-align:center;">
                            <div class="col-md-12 box-body">
                                <div class="form-group">
                                    <div class="col-sm-12">
                                    <asp:Button ID="Button2" runat="server" class="btn btn-danger" Text="Simpan" Type="submit"  OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;" onclick="Button2_Click"  />

                                                                                 <asp:Button ID="Button14" runat="server" class="btn btn-danger" Text="Print"  Visible="false"
                                                                                    Type="submit" onclick="Button14_Click"   />
                                                                              
                                                                                <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Tutup"  Type="submit" onclick="Button1_Click"   />
                                    </div>
                                </div>
                            </div>
                                
                                 </div>
                                </div>
              <%--  </div>--%>
                                                     
                                                            </div>

                                                               
                                                            <div role="tabpanel12" class="tab-pane" runat="server" id="pf2">
                                                                   <%--  <div class="col-md-12 table-responsive uppercase" style="overflow:auto;">--%>
                                          

                                                      <asp:gridview ID="grdpay" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="20" ShowFooter="true" GridLines="None" onrowdatabound="grdpay_RowDataBound"   >
            <Columns>
                  <asp:TemplateField HeaderText="Kod Akaun" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                
                     <asp:DropDownList ID="ddkodpay" style="width:100%;" runat="server" class="form-control select2 validate[optional]">    </asp:DropDownList>
                </ItemTemplate>
           </asp:TemplateField>
             <asp:TemplateField HeaderText="Tarikh Dibayar" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="75px">
                <ItemTemplate>
                     <div class="input-group">
                   
                     <asp:TextBox ID="TextBox1" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker2 mydatepickerclass"  placeholder="DD/MM/YYYY" ></asp:TextBox> 
                     </div>
                </ItemTemplate>
              <%--  <FooterTemplate>
                <asp:Button ID="ButtonAdd" runat="server" Text="Add New Row" onclick="ButtonAddpay_Click"    />
                </FooterTemplate>--%>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="No Resit" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" class="form-control uppercase"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="No Cek" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                <ItemTemplate>
                    <asp:TextBox ID="Txtnor2" runat="server" class="form-control uppercase"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Keterangan" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px">
                <ItemTemplate>
                    <asp:TextBox ID="txtket" runat="server" Width="100%"   class="form-control uppercase" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Bentuk Penerimaan" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="125px">
                <ItemTemplate>
                   <asp:DropDownList ID="ddBaya" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" >    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Jumlah Penerimaan (RM)" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox3" style="text-align:right;" class="form-control uppercase"   placeholder="0.00" runat="server" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Jumalh Tunggakan Semasa (RM)"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                    <ItemTemplate>
                    <asp:TextBox ID="TextBox4" style="text-align:right;" class="form-control uppercase" DataFormatString="{0:n}"  placeholder="0.00"   runat="server" ></asp:TextBox>
                 </ItemTemplate>
                 
          
            </asp:TemplateField>
           
           
            </Columns>
            <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <%--<RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />--%>
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />
        </asp:gridview>

       

                                                                       
                                                            <div class="box-body">&nbsp;</div>
                                                            <div class="row">
                                                                                 <div class="col-md-12" style="text-align:center;">
                                                                                     <div class="col-md-12 box-body">
                                                                                    <div class="form-group">
                                                                                        <div class="col-sm-12">
                                                                                          <asp:Button ID="Button19" runat="server" class="btn btn-danger" Text="Simpan" Type="submit" onclick="Button17_Click"    />
                                                                                <asp:Button ID="Button20" runat="server" class="btn btn-default" Text="Tutup" Type="submit" onclick="Button1_Click"   />
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                     </div>

                                                                     </div>         
                                                                    <span id="his1" runat="server" visible="false"><strong> <asp:Label ID="ps_lbl29" runat="server"></asp:Label></strong></span>
                                                                          <div class="box-body">&nbsp;</div>
            <asp:gridview ID="grdpayhis" runat="server"  class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="20" ShowFooter="true" GridLines="None"  onrowdatabound="grdpayhis_RowDataBound"  >
            <Columns>
           
                  <asp:TemplateField HeaderText="Kod Akaun" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px">
                <ItemTemplate>
                 <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("kod_akaun") %>' Visible = "false" />
                     <asp:DropDownList ID="ddkodpay" style="width:100%;" disabled="disabled" runat="server" class="form-control select2 validate[optional]">    </asp:DropDownList>
                </ItemTemplate>
                      </asp:TemplateField>
             <asp:BoundField DataField="tarikh" HeaderText="Tarikh Dibayaran" ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Center" />
              <asp:BoundField DataField="no_rujukan" HeaderText="No Resit" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" />
                 <asp:BoundField DataField="no_rujukan2" HeaderText="No Cek" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" />
               <asp:BoundField DataField="keteragan" HeaderText="Keteragan" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Left"  />
                 <asp:TemplateField HeaderText="Bentuk Penerimaan " ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px">
                 <ItemTemplate>
                 <asp:Label ID="lblBen" runat="server" Text='<%# Eval("Bentuk_penerimaan") %>' Visible = "false" />
                     <asp:DropDownList ID="ddkodBen" style="width:100%;" disabled="disabled" runat="server" class="form-control select2 validate[optional]">    </asp:DropDownList>
                </ItemTemplate>
                      </asp:TemplateField>
                 <asp:BoundField DataField="jumlah" HeaderText="Jumlah yang telah dibayar (RM)" ItemStyle-Width="125px" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" />
                  
             
            </Columns>
             <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                       <%-- <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />--%>
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />
        </asp:gridview>
                                                <%-- </div> --%>
                                                                  
                                                            </div>
                                                           </div>
                                                            </div>
                                                                       
                                                                      
                                                                </div>                                                                     
                                                                </div>
                                                                     </div>
                                                                </div>
                                                                <div role="tabpanel12" class="tab-pane active" runat="server" id="p1">   
                                                                     <div class="col-md-12 table-responsive uppercase"  style="overflow:auto;">                                                                 
                                                                    <div id="Div11"  runat="server">
                                                                       <%-- <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-3 box-body">
                                <div class="form-group">
                                    <div class="col-sm-12">
                                         <asp:TextBox ID="TextBox25" runat="server" class="form-control" Placeholder="Carian"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-3 box-body">
                                <div class="form-group">
                                    <div class="col-sm-12">
                                       <asp:Button ID="Button8" runat="server" class="btn btn-danger" Text="Tambah+" Type="submit" onclick="Button8_Click" />
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>--%>
                                                                     
                                                                     <fieldset class="col-md-12">
                                                                     <legend><asp:Label ID="ps_lbl30" runat="server"></asp:Label></legend>                                                                      
                                                                    <div class="row">
                                                                         <div class="col-md-12">
                                                                        <div class="col-md-3 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl31" runat="server"></asp:Label></label>
                                                                                <div class="col-sm-7">
                                                                                    <asp:TextBox ID="TextBox17" runat="server" class="form-control validate[optional]"
                                                                                   ></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-3 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl32" runat="server"></asp:Label></label>
                                                                                <div class="col-sm-7">
                                                                                      <div class="input-group">
                                                                                  <asp:TextBox ID="TextBox15" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                                        placeholder="DD/MM/YYYY"></asp:TextBox> 
                                                                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             <%-- <div class="col-md-3 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label">Nama Pelanggan <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                   <asp:TextBox style="text-align:right;" ID="TextBox16" runat="server" class="form-control validate[optional,custom[number]] uppercase au_amt"  onblur="addTotal_bk1(this)"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>--%>
                                                                             <div class="col-md-3 box-body">
                                                                            <div class="form-group">
                                                                                <div class="col-sm-12">
                                                                                    <asp:CheckBox ID="chk_resit" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                   <asp:Button ID="Button11" runat="server" class="btn btn-danger" Text="Carian" 
                                                                    UseSubmitBehavior="false" onclick="Button11_Click"    />
                                                                                <asp:Button ID="Button8" runat="server" class="btn btn-primary" Text=" Tambah" Type="submit" onclick="Button8_Click" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             </div>
                                                                     </div>
                                                                   </fieldset>
                                                                    <div class="box-body">&nbsp;</div>
                                                                          <div class="dataTables_wrapper form-inline dt-bootstrap" >
                                      <div class="row" style="overflow:auto;">
           <div class="col-md-12 box-body">
                                                                        <asp:gridview ID="Gridview5" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting" OnPageIndexChanging="gvSelected_PageIndexChanging_g5"  >
            <Columns>
          <asp:TemplateField HeaderText="BIL">  
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
           <asp:TemplateField HeaderText="Nombor Resit" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                  <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("no_resit") %>'  CommandArgument='<%# Eval("no_resit")%>' CommandName="Add"  onclick="lblSubItem_Click" Font-Bold Font-Underline></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
         
          
             <asp:BoundField DataField="tarikh_resit" HeaderText="Tarikh Resit" ItemStyle-HorizontalAlign="Center" />
            
                <asp:BoundField DataField="jumlah_bayaran" HeaderText="Jumlah (RM)" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n}" />
                
             
            </Columns>
        <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                       <%-- <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />--%>
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />
        </asp:gridview>
                                                                   </div>
               </div>
                                          </div>
                                                                         

                                                                   </div> 
                                                                    <div id="Div10" class="nav-tabs-custom" role="tabpanel12" runat="server">
                                                                     <ul class="s1 nav nav-tabs" role="tablist">
                                                            <li id="Li1" runat="server" class="active"><a href="#ContentPlaceHolder1_ps1" aria-controls="ps1" role="tab" data-toggle="tab"><asp:Label ID="ps_lbl35" runat="server"></asp:Label></a> </li> 
                                                            </ul>
                                                             <div class="tab-content">
                                                            <div role="tabpanel12" class="tab-pane active" runat="server" id="ps1">
                                                                <div class="col-md-12 table-responsive uppercase"  style="overflow:auto; padding-top:20px;">
                                                                     <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-4 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl5" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-7">
                                           <asp:DropDownList ID="ddakaun" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>
                                        <asp:TextBox ID="start_dt1" runat="server" Visible="false"></asp:TextBox>
                                        <asp:TextBox ID="end_dt1" runat="server" Visible="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>                      
                                                                <div class="row">
                                                                         <div class="col-md-12">
                                                                        <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl36" runat="server"></asp:Label> <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                      <asp:DropDownList ID="dd_selvalue" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" AutoPostBack="true" OnSelectedIndexChanged="sel_akauns">
                                                                                          <asp:ListItem Value="0">--- PILIH ---</asp:ListItem>
                                                                                          <asp:ListItem Value="1">SEMUA COA</asp:ListItem>
                                                                                          <asp:ListItem Value="2">PELANGGAN</asp:ListItem>
                                                                                     
                                                                                </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-4 box-body" id="resit_tab21" runat="server">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl37" runat="server"></asp:Label> <%--<span class="style1">*</span>--%></label>
                                                                                <div class="col-sm-7">
                                                                                   <asp:DropDownList ID="ddnamapela3" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" AutoPostBack="true" OnSelectedIndexChanged="ddnamapela3_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             <div class="col-md-4 box-body" id="resit_tab2" runat="server" visible="false">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl38" runat="server"></asp:Label><span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                        <asp:TextBox ID="txtnorst3" runat="server" class="form-control uppercase"></asp:TextBox>
                                                                                    <asp:TextBox ID="txtnorst3_3" Visible="false" runat="server" class="form-control uppercase"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             </div>
                                                                     </div>
                                                                   <div class="row">
                                                                         <div class="col-md-12">
                                                                        <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl39" runat="server"></asp:Label><span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                      <div class="input-group">
                                                                                <asp:TextBox ID="GP_date" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker2 mydatepickerclass"
                                                                                        placeholder="DD/MM/YYYY"></asp:TextBox>
                                                                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="Label12" runat="server"></asp:Label></label>
                                                                                <div class="col-sm-7">
                                                                                         <asp:TextBox ID="txtname" runat="server" class="form-control uppercase" AutoPostBack="true" OnTextChanged="clk_txt_show"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             </div>
                                                                     </div>
                                                                     
                                                                       <fieldset class="col-md-12">
                                                                     <legend style="width:13%;"><asp:Label ID="ps_lbl40" runat="server"></asp:Label></legend>                                                     
                                                                           <div class="row">
                                                                         <div class="col-md-12">
                                                                        <div class="col-md-3 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl41" runat="server"></asp:Label> </label>
                                                                                <div class="col-sm-7">
                                                                                      <asp:updatepanel id="UpdatePanelMain" runat="server" updatemode="Always">
                           <contenttemplate>
                                                                                <%--  <asp:DropDownList ID="dd_Betuk1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"  AutoPostBack="True"  OnSelectedIndexChanged="dd_Betuk_SelectedIndexChanged" >--%>
                               <asp:DropDownList ID="dd_Betuk1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                                </asp:DropDownList>
                                                                                </contenttemplate>
<triggers>
                                <asp:asyncpostbacktrigger controlid="dd_Betuk1" eventname="SelectedIndexChanged" />
                                
                            </triggers>
                        </asp:updatepanel>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-3 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl42" runat="server"></asp:Label>KOD AKAUN (DEBIT)</label>
                                                                                <div class="col-sm-7">
                                                                                  <asp:DropDownList ID="ddaka" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" ></asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             <div class="col-md-3 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl43" runat="server"></asp:Label></label>
                                                                                <div class="col-sm-7">
                                                                                     <asp:TextBox ID="GP_rno" runat="server" class="form-control validate[optional] uppercase"
                                                                                  ></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                               <div class="col-md-3 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl44" runat="server"></asp:Label></label>
                                                                                <div class="col-sm-7">
                                                                                     <asp:TextBox style="text-align:right;" ID="txtjum" runat="server" class="form-control validate[optional,custom[number]] au_amt"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             </div>
                                                                     </div>
                                                                       
                                                                    </fieldset>
                                                                    <fieldset class="col-md-12">
                                                                     <legend><asp:Label ID="ps_lbl45" runat="server"></asp:Label></legend>
                                                                      <div class="col-md-12 table-responsive uppercase" style="overflow:auto;">
                                                      <asp:gridview ID="Gridview4" runat="server"  class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="20" ShowFooter="true" GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting"  onrowdatabound="Gridview4_RowDataBound" >
            <Columns>
             <asp:TemplateField HeaderText="Kod Akaun (Kredit)" HeaderStyle-Width="30%" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                     <asp:DropDownList ID="ddkodres" style="width:100%;" runat="server" class="form-control select2 validate[optional]">    </asp:DropDownList>
                </ItemTemplate>
                 <FooterTemplate>
                 <asp:Button ID="ButtonAdd" runat="server" Text="Add New Row" 
                        onclick="ButtonAdd_Click1" />
                </FooterTemplate>
            </asp:TemplateField>
                  <asp:TemplateField HeaderText="Rujukan" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:TextBox ID="resit_rujukan" class="form-control uppercase"  Width="100px" runat="server"></asp:TextBox>
                </ItemTemplate>
 
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox11" class="form-control uppercase" Width="100px"  runat="server"></asp:TextBox>
                </ItemTemplate>
 
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Keterangan" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox12" runat="server" class="form-control uppercase" TextMode="MultiLine" Width="180px" Height="40px"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Harga/unit" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox13" runat="server" class="form-control uppercase" placeholder="0.00" OnTextChanged="QtyChangedres_kty" AutoPostBack="true"  Width="70px"  style="text-align:right;" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Kuantiti" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox14" runat="server" class="form-control uppercase" OnTextChanged="QtyChangedres"  Width="70px" AutoPostBack="true" placeholder="0" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Discount" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="Txtdisres" runat="server" class="form-control uppercase" OnTextChanged="disChangedres"  Width="70px" style="text-align:right;" AutoPostBack="true" placeholder="0.00" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Tax" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="chkres" runat="server"  OnCheckedChanged="ChckedChangedres"  AutoPostBack="true"  />
                </ItemTemplate>
                     </asp:TemplateField>

                <asp:TemplateField HeaderText="Caj Perkhidmatan (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                  <asp:DropDownList ID="ddresoth" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"  onselectedindexchanged="ddrescukoth_SelectedIndexChanged" AutoPostBack="true"  >    </asp:DropDownList>

                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                   <asp:Label ID="Label7" CssClass="form-control" runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
                  <asp:TemplateField HeaderText="Caj Perkhidmatan" ItemStyle-HorizontalAlign="right" Visible="false">
                <ItemTemplate>
                       <asp:Label ID="Label10" CssClass="form-control" runat="server" Text="0.00"  Width="100px"  ></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                   <asp:Label ID="Label11" CssClass="form-control" runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="GST (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                  <asp:DropDownList ID="ddrescuk" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"  onselectedindexchanged="ddrescuk_SelectedIndexChanged" AutoPostBack="true"  >    </asp:DropDownList>

                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                   <asp:Label ID="Label2" CssClass="form-control" runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
                  <asp:TemplateField HeaderText="Gst (%)" ItemStyle-HorizontalAlign="right" Visible="false">
                <ItemTemplate>
                       <asp:Label ID="Label8" CssClass="form-control" runat="server" Text="0.00"  Width="100px"  ></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                   <asp:Label ID="Label9" CssClass="form-control" runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
                 
            
            <asp:TemplateField HeaderText="Jumlah (RM)" ItemStyle-HorizontalAlign="right">
                <ItemTemplate>
                       <asp:Label ID="Label3" CssClass="form-control" runat="server" Text="0.00"  Width="100px"  ></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                   <asp:Label ID="Label4" CssClass="form-control" runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>

                  <asp:TemplateField HeaderText="Jumlah Termasuk CBP (RM)" ItemStyle-HorizontalAlign="right">
                <ItemTemplate>
                    <asp:Label ID="Label5" CssClass="form-control" runat="server" Text="0.00"  Width="100px"  ></asp:Label>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="center" />
                <FooterTemplate>
                  <asp:Label ID="Label6" CssClass="form-control"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
       
            </Columns>
            <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <%--<RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />--%>
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />
        </asp:gridview>
                                                                          </div>
                                                                            
   
                                                                      <div class="col-md-12 table-responsive uppercase" style="overflow:auto;">
                           <asp:gridview ID="Gridview15" runat="server" CssClass="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="20" ShowFooter="true" GridLines="None"  onrowdatabound="Gridview15_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="BIL">  
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Kod Akaun" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                     <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("res_Kodakaun") %>' Visible = "false"></asp:Label>
                    <asp:Label ID="ddkodres" runat="server" Text='<%# Eval("nama_akaun") %>' class="uppercase" style="text-align:Left;" readonly="true"></asp:Label>
                     <%--<asp:DropDownList ID="ddkodres" runat="server" class="form-control uppercase"  Width="125">    </asp:DropDownList>--%>
                </ItemTemplate>
                 
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Rujukan" ItemStyle-HorizontalAlign="center">
                <ItemTemplate>
                    <asp:Label ID="resit_rujukan" runat="server" class="uppercase" Text='<%# Eval("res_rujukan") %>' Width="125"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="TextBox11" runat="server" Text='<%# Eval("res_item") %>'></asp:Label>
                </ItemTemplate>
 
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Keterangan" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="TextBox12" runat="server" Text='<%# Eval("res_keterangan") %>' class="uppercase" TextMode="MultiLine" Height="40px" Width="180px"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Harga/unit" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="TextBox13" runat="server" Text='<%# Eval("res_unit") %>' style="text-align:right;" Width="75"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Kuantiti" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="TextBox14" runat="server" Text='<%# Eval("res_quantiti") %>' Width="75"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Tax" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="chkres" runat="server"  Checked='<%# bool.Parse(Eval("tax").ToString()) %>' Enable='<%# !bool.Parse(Eval("tax").ToString()) %>'   />
                </ItemTemplate>
                     </asp:TemplateField>

                 <asp:TemplateField HeaderText="Caj Perkhidmatan (%)" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                     <asp:Label ID="lblgstoth" runat="server" Text='<%# Eval("othgsttype") %>' Visible = "false"></asp:Label>
                    <asp:Label ID="ddrescukoth" runat="server" Text='<%# Eval("othgstname") %>' class=" uppercase" readonly="true"></asp:Label>
                   <%-- <asp:DropDownList ID="ddrescukoth" runat="server" class="form-control"   Enabled="false" >    </asp:DropDownList>--%>
                </ItemTemplate>
                
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Caj Perkhidmatan (RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                  <asp:Label ID="Label3_Cajgst" runat="server"  Text='<%# Eval("othgstjumlah","{0:n}") %>'  style="text-align:right;"></asp:Label>
                </ItemTemplate>
              
            </asp:TemplateField>
             <asp:TemplateField HeaderText="GST (%)" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                     <asp:Label ID="lblgst" runat="server" Text='<%# Eval("gsttype") %>' Visible = "false"></asp:Label>
                     <asp:Label ID="ddrescuk" runat="server" Text='<%# Eval("gstname") %>' class="uppercase" readonly="true"></asp:Label>
                    <%--<asp:DropDownList ID="ddrescuk" runat="server" class="form-control"   Enabled="false" >    </asp:DropDownList>--%>
                </ItemTemplate>
                
            </asp:TemplateField>
            <asp:TemplateField HeaderText="GST (RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                  <asp:Label ID="Label3_gst" runat="server"  Text='<%# Eval("gstjumlah","{0:n}") %>'  style="text-align:right;"></asp:Label>
                </ItemTemplate>
              
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Jumlah" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                  <asp:Label ID="Label3" runat="server"  Text='<%# Eval("res_jumlah","{0:n}") %>'  style="text-align:right;"></asp:Label>
                </ItemTemplate>
              
            </asp:TemplateField>
            <%-- <asp:CommandField ShowDeleteButton="True" />--%>
            </Columns>
            <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                       <%-- <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />--%>
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />
        </asp:gridview>
     </div>
   
                                                                     <div class="box-body">&nbsp;</div>
                                                            <div class="row">
                                                                                 <div class="col-md-12">
                                                                                <div class="col-md-8 box-body">&nbsp;</div>
                                                                                     <div class="col-md-4 box-body">
                                                                                    <div class="form-group">
                                                                                        <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl46" runat="server"></asp:Label></label>
                                                                                        <div class="col-sm-7">
                                                                                          <asp:TextBox ID="TextBox11" runat="server" class="form-control" style="text-align:right;" ></asp:TextBox> 
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                     </div>
                                </div>
                                                 </fieldset>
                                                                                         <div class="box-body">&nbsp;</div>
                                                                <div class="row">
                             <div class="col-md-12" style="text-align:center;">
                            <div class="col-md-12 box-body">
                                <div class="form-group">
                                    <div class="col-sm-12">
                                     <asp:Button ID="Button5" runat="server" class="btn btn-danger" Text="Simpan" Type="submit"
                                                                                    OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;" 
                                                                                    onclick="Button5_Click" />
                                         <asp:Button ID="Button23" runat="server" Text="Print" class="btn btn-warning" Visible="false" UseSubmitBehavior="false"  onclick="resit_prt"
                                                                                     />
                                                                                <asp:Button ID="Button7" runat="server" Text="Tutup" class="btn btn-default" UseSubmitBehavior="false"  onclick="Button7_Click"
                                                                                     />
                                    </div>
                                </div>
                            </div>
                                
                                 </div>
                                </div>                                                     
                                                                          </div>                                                                          
                                                                    </div>
                                                                    </div>
                                                            </div>
                                                                   </div>
                                                                   </div>
                                                                     <div role="tabpanel2" class="tab-pane" runat="server" id="p2">
                                                                          <div class="col-md-12 table-responsive"  style="overflow:auto;">
                                                                    <div id="th1" runat="server">
                                                                                         
                                                                          <fieldset class="col-md-12">
                                                                     <legend><asp:Label ID="ps_lbl50" runat="server"></asp:Label></legend>                                                  
                                                                              <div class="row">
                                                                         <div class="col-md-12">
                                                                        <div class="col-md-3 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl51" runat="server"></asp:Label></label>
                                                                                <div class="col-sm-7">
                                                                                   <asp:TextBox ID="txtcno" runat="server" class="form-control validate[optional]"
                                                                                   ></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-3 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl52" runat="server"></asp:Label></label>
                                                                                <div class="col-sm-7">
                                                                                      <div class="input-group">
                                                                                 <asp:TextBox ID="txtctarikh" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                                        placeholder="DD/MM/YYYY"></asp:TextBox>
                                                                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-3 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl53" runat="server"></asp:Label></label>
                                                                                <div class="col-sm-7">
                                                                                   <asp:TextBox ID="txtnpembe" runat="server" class="form-control uppercase"></asp:TextBox> 
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                            
                                                                             <div class="col-md-3 box-body">
                                                                            <div class="form-group">
                                                                                <div class="col-sm-12">
                                                                                    <asp:CheckBox ID="chk_kredit" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                  <asp:Button ID="Button12" runat="server" class="btn btn-danger" Text="Carian" 
                                                                    UseSubmitBehavior="false" onclick="Button12_Click"    />
                                                                                <asp:Button ID="Button9" runat="server" class="btn btn-primary" Text=" Tambah" Type="submit"
                                                                                  onclick="Button9_Click" 
                                                                                    />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             </div>
                                                                     </div>
                                                                    </fieldset>
                                                                
                                                                 <div class="box-body">&nbsp;</div>
                                                                        <div class="dataTables_wrapper form-inline dt-bootstrap" >
                                      <div class="row" style="overflow:auto;">
           <div class="col-md-12 box-body">
                                                                        <asp:gridview ID="Gridview6" runat="server"  class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting" OnPageIndexChanging="gvSelected_PageIndexChanging_g6" >
            <Columns>
          <asp:TemplateField HeaderText="BIL">  
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="C/N No" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                  <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("no_Rujukan") %>'  CommandArgument='<%# Eval("no_Rujukan")%>' CommandName="Add"  onclick="lblSubItemcredit_Click" Font-Bold Font-Underline ></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:BoundField DataField="tarikh_invois" HeaderText="Tarikh" ItemStyle-HorizontalAlign="Center" />
              <asp:BoundField DataField="Ref_nama_syarikat" HeaderText="Nama Pelanggan" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="jumlah" HeaderText="Jumlah (RM)" ItemStyle-HorizontalAlign="Center" />
                
             
            </Columns>
            <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                       <%-- <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />--%>
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />
        </asp:gridview>
                                                                   </div>
               </div>
                                          </div>
                                                                   
                                                                  </div>
                                                                  <div id="th2" runat="server">
                                                                       
                                                                       <div class="row">
                                                                         <div class="col-md-12">
                                                                        <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl56" runat="server"></asp:Label><span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                          <asp:DropDownList ID="ddpela2" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" OnSelectedIndexChanged="ddpela2_SelectedIndexChanged" AutoPostBack="true">
                                                                                </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl57" runat="server"></asp:Label> <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                    <asp:DropDownList ID="ddinv" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" OnSelectedIndexChanged="ddinv_SelectedIndexChanged" AutoPostBack="true">
                                                                                </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl58" runat="server"></asp:Label> <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                      <div class="input-group">
                                                                                   <asp:TextBox ID="txttcinvois" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             </div>
                                                                     </div>
                                                                     <div class="row">
                                                                         <div class="col-md-12">
                                                                                <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl59" runat="server"></asp:Label></label>
                                                                                <div class="col-sm-7">
                                                                                         <asp:DropDownList ID="ddinvday" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                                    <asp:ListItem>--- PILIH ---</asp:ListItem>
                                                                                 <asp:ListItem>30</asp:ListItem>
                                                                                  <asp:ListItem>60</asp:ListItem>
                                                                                  <asp:ListItem>90</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl60" runat="server"></asp:Label> <span class="style1">*</span></label>                    
                                                                                <div class="col-sm-7">
                                                                                      <div class="input-group">
                                                                                  <asp:TextBox ID="txttcre" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker2 mydatepickerclass"
                                                                                        placeholder="DD/MM/YYYY"></asp:TextBox>
                                                                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl61" runat="server"></asp:Label> <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                   <asp:DropDownList ID="ddpro1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                                </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                            
                                                                             </div>
                                                                     </div>
                                                                        <div class="row" id="kredit_tab3" runat="server" visible="false">
                                                                         <div class="col-md-12">
                                                                       <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl62" runat="server"></asp:Label></label>
                                                                                <div class="col-sm-7">
                                                                                        <asp:TextBox ID="txtnoruju" runat="server" class="form-control "></asp:TextBox>
                                                                                    <asp:TextBox ID="txtnoruju_1" runat="server" Visible="false" class="form-control "></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             </div>
                                                                     </div>
                                                                        <div class="box-body">&nbsp;</div>
                                                                   <%-- <div class="panel" style="width: 100%;">--%>
                                                        <div id="Div4" class="nav-tabs-custom" role="tabpanel2" runat="server">
                                                            <!-- Nav tabs -->
                                                            <ul class="s1 nav nav-tabs" role="tablist">
                                                            <li id="pp11" runat="server" class="active"><a href="#ContentPlaceHolder1_p63" aria-controls="p63" role="tab" data-toggle="tab"><asp:Label ID="ps_lbl63" runat="server"></asp:Label> </a>
                                                            </li>  
                                                            </ul>
                                                            <!-- Tab panes -->
                                                            <div class="tab-content">  
                                                            <div role="tabpanel2" class="tab-pane active" runat="server" id="p63">                                                                   
                                                                  <%--   <div id="Div6"  runat="server">--%>
                                                                                <%--<div class="col-md-12 table-responsive uppercase" style="overflow:auto;">             --%>
<asp:gridview ID="Gridview9" runat="server"  CssClass="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="20" ShowFooter="false" GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting" onrowdatabound="Gridview3_RowDataBound" >
            <Columns>
             <asp:TemplateField HeaderText="Kod Akaun" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                 <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("kod_akauan") %>' Visible = "false" />
                    <asp:Label ID="ddkoddup" runat="server" Text='<%# Eval("nama_akaun") %>' />
                     <%--<asp:DropDownList ID="ddkoddup" runat="server" Width="150px" class="form-control uppercase">    </asp:DropDownList>--%>
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Rujukan" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                 <asp:Label ID="lblruj" runat="server" Text='<%# Eval("rujukan") %>'  />
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Item" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                 <asp:Label ID="lblitem" runat="server" Text='<%# Eval("item") %>'  />
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Keterangan" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                 <asp:Label ID="lblket" runat="server" Text='<%# Eval("keterangan") %>'  />
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Harga/unit" ItemStyle-HorizontalAlign="Right" Visible="false" >
                <ItemTemplate>
                 <asp:Label ID="lblunit" runat="server" Text='<%# Eval("unit") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Kuantiti" ItemStyle-HorizontalAlign="Center" Visible="false">
                <ItemTemplate>
                 <asp:Label ID="lblqty" runat="server" Text='<%# Eval("quantiti") %>'  />
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Disk" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lbldis" runat="server" Text='<%# Eval("discount","{0:n}") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Jumlah tidak termasuk CBP(RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lbljum" runat="server" Text='<%# Eval("jumlah","{0:n}") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>  
             <asp:TemplateField HeaderText="Tax" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# bool.Parse(Eval("tax").ToString()) %>' Enable='<%# !bool.Parse(Eval("tax").ToString()) %>' />
                </ItemTemplate>
            </asp:TemplateField>
           <asp:TemplateField HeaderText="Caj Perkhidmatan (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                 <asp:Label ID="lblgstoth" runat="server" Text='<%# Eval("othgsttype") %>' Visible = "false" />
                    <asp:Label ID="ddtaxoth" runat="server" Text='<%# Eval("othgstname") %>' />
                     <%--<asp:DropDownList ID="ddtaxoth" runat="server" class="form-control uppercase">    </asp:DropDownList>--%>
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Caj Perkhidmatan (RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lblothgst" runat="server" Text='<%# Eval("othgst","{0:n}") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>  
                
             <asp:TemplateField HeaderText="Gst (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                 <asp:Label ID="lblgst" runat="server" Text='<%# Eval("gsttype") %>' Visible = "false" />
                     <asp:Label ID="ddtax" runat="server" Text='<%# Eval("gstname") %>' />
                     <%--<asp:DropDownList ID="ddtax" runat="server" class="form-control uppercase">    </asp:DropDownList>--%>
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Gst (RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lblgstjum" runat="server" Text='<%# Eval("gstjum","{0:n}") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>  
                 <asp:TemplateField HeaderText="Jumlah Termasuk CBP (RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lblover" runat="server" Text='<%# Eval("Overall","{0:n}") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>
               
                <asp:TemplateField HeaderText="Baki Invois (RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lblover1" runat="server" Text='<%# Eval("jumlah_bayaran","{0:n}") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>

            </Columns>
           <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />                                                        
        </asp:gridview>

  <asp:gridview ID="Gridview10" runat="server"  CssClass="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="20" ShowFooter="true" GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting" onrowdatabound="Gridview9_RowDataBound" >
                 <Columns>
           
             <asp:TemplateField HeaderText="Kod Akaun" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                     <asp:DropDownList ID="ddkodcre" style="width:100%;" runat="server" class="form-control select2 validate[optional]">    </asp:DropDownList>
                </ItemTemplate>
                 <FooterTemplate>
                 <asp:Button ID="ButtonAdd" runat="server" Text="Add New Row" 
                        onclick="ButtonAddcre_Click" />
                </FooterTemplate>
            </asp:TemplateField>
                    
            <asp:TemplateField HeaderText="Keterangan" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="txtket" CssClass="form-control uppercase" runat="server" TextMode="MultiLine" Width="100%" Height="40px"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
                     
                       <asp:TemplateField HeaderText="Jumlah tidak termasuk CBP(RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                     <asp:TextBox ID="Txtdis" runat="server"  CssClass="form-control"  placeholder="0.00"   OnTextChanged="QtyChanged1" AutoPostBack="true"   style="text-align:right;"    ></asp:TextBox>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label3" CssClass="form-control"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Tax" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="Chkcre" runat="server"  OnCheckedChanged="ChckedChangedcre"  AutoPostBack="true"  />
                </ItemTemplate>
            </asp:TemplateField>
                       <asp:TemplateField HeaderText="Caj Perkhidmatan (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:DropDownList ID="ddkodgstoth" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"  onselectedindexchanged="ddkodgstoth_SelectedIndexChanged" AutoPostBack="true"  >    </asp:DropDownList>
                </ItemTemplate>
                
            </asp:TemplateField>
                      <asp:TemplateField HeaderText="Caj Perkhidmatan (RM)" ItemStyle-HorizontalAlign="Right" >
                <ItemTemplate>
                    <asp:Label ID="Label10" CssClass="form-control" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label11" CssClass="form-control"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="GST (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:DropDownList ID="ddkodgst" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" onselectedindexchanged="ddkodgst_SelectedIndexChanged" AutoPostBack="true" >    </asp:DropDownList>
                </ItemTemplate>
               
            </asp:TemplateField>
                       <asp:TemplateField HeaderText="GST (RM)" ItemStyle-HorizontalAlign="Right" >
                <ItemTemplate>
                    <asp:Label ID="Label8" CssClass="form-control" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label9" CssClass="form-control"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
                      <asp:TemplateField HeaderText="Jumlah Termasuk CBP (RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="Label5" CssClass="form-control" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label6" CssClass="form-control"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            
            </Columns>
           <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <%--<RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />--%>
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />                                                          
        </asp:gridview>
                           <div class="box-body">&nbsp;</div>                                                                   
<asp:gridview ID="Gridview12" runat="server"  CssClass="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="20" ShowFooter="false" GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting" onrowdatabound="Gridview3_RowDataBound" >
            <Columns>
             <asp:TemplateField HeaderText="Kod Akaun" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                 <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("kod_akauan") %>' Visible = "false" />
                    <asp:Label ID="ddkoddup" runat="server" Text='<%# Eval("nama_akaun") %>' />
                     <%--<asp:DropDownList ID="ddkoddup" runat="server" Width="150px" class="form-control uppercase">    </asp:DropDownList>--%>
                </ItemTemplate>
            </asp:TemplateField>
               
           
                 <asp:TemplateField HeaderText="Keterangan" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                 <asp:Label ID="lblket" runat="server" Text='<%# Eval("keterangan") %>'  />
                </ItemTemplate>
            </asp:TemplateField>
               
            
              <asp:TemplateField HeaderText="Jumlah tidak termasuk CBP(RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lbljum" runat="server" Text='<%# Eval("jumlah") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>  
             <asp:TemplateField HeaderText="Tax" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# bool.Parse(Eval("tax").ToString()) %>' Enable='<%# !bool.Parse(Eval("tax").ToString()) %>' />
                </ItemTemplate>
            </asp:TemplateField>
           <asp:TemplateField HeaderText="Caj Perkhidmatan (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                 <asp:Label ID="lblgstoth" runat="server" Text='<%# Eval("othgsttype") %>' />
                    <asp:Label ID="ddtaxoth" runat="server" Text='<%# Eval("othgstname") %>' />
                     <%--<asp:DropDownList ID="ddtaxoth" runat="server" class="form-control uppercase">    </asp:DropDownList>--%>
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Caj Perkhidmatan (RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lblothgst" runat="server" Text='<%# Eval("othgstjumlah") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>  
                
             <asp:TemplateField HeaderText="Gst (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                 <asp:Label ID="lblgst" runat="server" Text='<%# Eval("gsttype") %>'  />
                     <asp:Label ID="ddtax" runat="server" Text='<%# Eval("gstname") %>' />
                     <%--<asp:DropDownList ID="ddtax" runat="server" class="form-control uppercase">    </asp:DropDownList>--%>
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Gst (RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lblgstjum" runat="server" Text='<%# Eval("gstjumlah") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>  
                 <asp:TemplateField HeaderText="Jumlah Termasuk CBP" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lblover" runat="server" Text='<%# Eval("Overall") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>
            </Columns>
              <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />                                                            
           
        </asp:gridview>
        
                                            
                <div class="box-body">&nbsp;</div>
                                                            <div class="row">
                                                                                 <div class="col-md-12">
                                                                                <div class="col-md-8 box-body">&nbsp;</div>
                                                                                     <div class="col-md-4 box-body">
                                                                                    <div class="form-group">
                                                                                        <label for="inputEmail3" class="col-sm-6 control-label"><asp:Label ID="ps_lbl64" runat="server"></asp:Label></label>
                                                                                        <div class="col-sm-6">
                                                                                          <asp:TextBox ID="TextBox18" runat="server" class="form-control" style="text-align:right;" ></asp:TextBox> 
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                     </div>
                                </div>
                                                                           <div class="box-body">&nbsp;</div>
                                                            <div class="row">
                                                                                 <div class="col-md-12" style="text-align:center;">
                                                                                     <div class="col-md-12 box-body">
                                                                                    <div class="form-group">
                                                                                        <div class="col-sm-12">
                                                                                           <asp:Button ID="Button15" runat="server" class="btn btn-danger" Text="Simpan" Type="submit"
                                                                                    OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;" onclick="Button15_Click" 
                                                                                    />
                                                                                <asp:Button ID="Button22" runat="server" class="btn btn-danger" Text="Print" Type="submit"
                                                                                    onclick="Button22_Click" 
                                                                                    />

                                                                                <asp:Button ID="Button16" runat="server" Text="Tutup" class="btn btn-default" UseSubmitBehavior="false"
                                                                                     onclick="Button16_Click"  />
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                     </div>
                                </div>
                                                               <%-- </div> --%>                                                           
                                                               <%-- </div>--%>
                                                               
                                                                   </div>
                                                                </div>
                                                        </div>
                                                                  <%--</div> --%>
                                                                   </div>
                                                                   </div>
                                                                         </div>
                                                                    <div role="tabpanel3" class="tab-pane" runat="server" id="p3">
                                                                         <div class="col-md-12 table-responsive uppercase"  style="overflow:auto; padding-top:20px;">
                                                                    <div id="fr1" runat="server">
                                                                     
                                                                     <fieldset class="col-md-12">
                                                                     <legend><asp:Label ID="ps_lbl68" runat="server"></asp:Label></legend>
                                                                    <div class="row">
                                                                         <div class="col-md-12">
                                                                        <div class="col-md-3 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl69" runat="server"></asp:Label></label>
                                                                                <div class="col-sm-7">
                                                                                    <asp:TextBox ID="TextBox13" runat="server" class="form-control validate[optional]"
                                                                                   ></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-3 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl70" runat="server"></asp:Label></label>                                     
                                                                                <div class="col-sm-7">
                                                                                      <div class="input-group">
                                                                                  <asp:TextBox ID="TextBox20" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                                        placeholder="DD/MM/YYYY"></asp:TextBox> 
                                                                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-3 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl71" runat="server"></asp:Label></label>
                                                                                <div class="col-sm-7">
                                                                                   <asp:TextBox ID="TextBox21" runat="server" class="form-control uppercase"></asp:TextBox> 
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             <div class="col-md-3 box-body">
                                                                            <div class="form-group">
                                                                                <div class="col-sm-12">
                                                                                    <asp:CheckBox ID="chk_debit" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                    <asp:Button ID="Button13" runat="server" class="btn btn-danger" Text="Carian" 
                                                                    UseSubmitBehavior="false" onclick="Button13_Click"    />
                                                                               <asp:Button ID="Button10" runat="server" class="btn btn-primary" Text=" Tambah" Type="submit" onclick="Button10_Click" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             </div>
                                                                     </div>
                                                                    </fieldset>
                                                                          <div class="box-body">&nbsp;</div>
                                                                <div class="dataTables_wrapper form-inline dt-bootstrap" >
                                      <div class="row" style="overflow:auto;">
           <div class="col-md-12 box-body">
                                                                        <asp:gridview ID="Gridview8" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting" OnPageIndexChanging="gvSelected_PageIndexChanging_g8" >
            <Columns>
          <asp:TemplateField HeaderText="BIL">  
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="D/N No" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                  <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("no_Rujukan") %>'  CommandArgument='<%# Eval("no_Rujukan")%>' CommandName="Add"  onclick="lblSubItemdebit_Click" Font-Bold Font-Underline></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:BoundField DataField="tarikh_invois" HeaderText="Tarikh" ItemStyle-HorizontalAlign="Center" />
              <asp:BoundField DataField="Ref_nama_syarikat" HeaderText="Nama Pelanggan" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="jumlah" HeaderText="Jumlah (RM)" ItemStyle-HorizontalAlign="Right" />
                
             
            </Columns>
        <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                       <%-- <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />--%>
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />
        </asp:gridview>
                                                                   </div>
                                                                  
                                                                   
                                                               </div>
                                          </div>
                                                                    </div>
                                                                    <div id="fr2" runat="server">
                                                                        <div class="row">
                                                                         <div class="col-md-12">
                                                                        <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl74" runat="server"></asp:Label> <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                           <asp:DropDownList ID="ddpela3" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" OnSelectedIndexChanged="ddpela3_SelectedIndexChanged" AutoPostBack="true">
                                                                                </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl75" runat="server"></asp:Label> <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                          <asp:DropDownList ID="ddinv2" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" OnSelectedIndexChanged="ddinv2_SelectedIndexChanged" AutoPostBack="true">
                                                                                </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl76" runat="server"></asp:Label> <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                      <div class="input-group">
                                                                                    <asp:TextBox ID="txtdtinvois" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                                        placeholder="DD/MM/YYYY"></asp:TextBox>
                                                                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             </div>
                                                                     </div>
                                                                     <div class="row">
                                                                         <div class="col-md-12">
                                                                              <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl77" runat="server"></asp:Label></label>
                                                                                <div class="col-sm-7">
                                                                                         <asp:DropDownList ID="ddinvday2" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                                    <asp:ListItem>--- PILIH ---</asp:ListItem>
                                                                                 <asp:ListItem>30</asp:ListItem>
                                                                                  <asp:ListItem>60</asp:ListItem>
                                                                                  <asp:ListItem>90</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl78" runat="server"></asp:Label> <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                      <div class="input-group">
                                                                                <asp:TextBox ID="txtdeb" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker2 mydatepickerclass"
                                                                                        placeholder="DD/MM/YYYY"></asp:TextBox>
                                                                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl79" runat="server"></asp:Label> <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                   <asp:DropDownList ID="ddpro2" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                                </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                            
                                                                             </div>
                                                                     </div>
                                                                        <div class="row" id="debit_tab4" runat="server" visible="false">
                                                                         <div class="col-md-12">
                                                                         <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl80" runat="server"></asp:Label></label>                                        
                                                                                <div class="col-sm-7">
                                                                                         <asp:TextBox ID="txtnoruj2" runat="server" class="form-control "></asp:TextBox>
                                                                                    <asp:TextBox ID="txtnoruj2_2" runat="server" Visible="false" class="form-control "></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             </div>
                                                                     </div>
                                                                      <div class="box-body">&nbsp;
                                    </div>
                                                                                                                                 
                                                           <%--<div class="panel" style="width: 100%;">--%>
                                                        <div id="Div5" class="nav-tabs-custom" role="tabpanel3" runat="server">
                                                            <!-- Nav tabs -->
                                                            <ul class="s1 nav nav-tabs" role="tablist">
                                                            <li id="pp14" runat="server" class="active"><a href="#ContentPlaceHolder1_p67" aria-controls="p67" role="tab" data-toggle="tab"><asp:Label ID="ps_lbl81" runat="server"></asp:Label> </a>
                                                               
                                                            </li>
                                                               <%-- <li id="pp15" runat="server"><a href="#ContentPlaceHolder1_p68" aria-controls="p68" role="tab" data-toggle="tab">Knock Off</a></li>--%>
                                                                <%--<li id="pp2" runat="server"><a href="#ContentPlaceHolder1_p2" aria-controls="p2" role="tab" data-toggle="tab">ELAUN TETAP</a></li>
                                                                <li id="pp3" runat="server"><a href="#ContentPlaceHolder1_p3" aria-controls="p3" role="tab" data-toggle="tab">LAIN-LAIN ELAUN</a></li>
                                                                <li id="pp4" runat="server"><a href="#ContentPlaceHolder1_p4" aria-controls="p4" role="tab" data-toggle="tab">KERJA LEBIH MASA</a></li>
                                                                <li id="pp5" runat="server"><a href="#ContentPlaceHolder1_p5" aria-controls="p5" role="tab" data-toggle="tab">BONUS</a></li>--%>
                                                                
                                                            </ul>
                                                            <!-- Tab panes -->
                                                            <div class="tab-content" >
                                                            <div role="tabpanel3" class="tab-pane active" runat="server" id="p67">
                                                                   <%--  <div id="Div8"  runat="server">--%>
                                                                                <div class="col-md-12 table-responsive uppercase" style="overflow:auto;">
                               
<asp:gridview ID="Gridview7" runat="server"  class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="20" ShowFooter="true" GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting" onrowdatabound="Gridview3_RowDataBound" >
            <Columns>
             <asp:TemplateField HeaderText="Kod Akaun" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                 <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("kod_akauan") %>' Visible = "false" />
                    <asp:Label ID="ddkoddup" runat="server" Text='<%# Eval("nama_akaun") %>' />
                     <%--<asp:DropDownList ID="ddkoddup" runat="server" Width="150px" class="form-control uppercase">    </asp:DropDownList>--%>
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Rujukan" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                 <asp:Label ID="lblruj" runat="server" Text='<%# Eval("rujukan") %>'  />
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Item" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                 <asp:Label ID="lblitem" runat="server" Text='<%# Eval("item") %>'  />
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Keterangan" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                 <asp:Label ID="lblket" runat="server" Text='<%# Eval("keterangan") %>'  />
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Harga/unit" ItemStyle-HorizontalAlign="Right" Visible="false" >
                <ItemTemplate>
                 <asp:Label ID="lblunit" runat="server" Text='<%# Eval("unit") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Kuantiti" ItemStyle-HorizontalAlign="Center" Visible="false">
                <ItemTemplate>
                 <asp:Label ID="lblqty" runat="server" Text='<%# Eval("quantiti") %>'  />
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Disk" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lbldis" runat="server" Text='<%# Eval("discount","{0:n}") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Jumlah tidak termasuk CBP(RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lbljum" runat="server" Text='<%# Eval("jumlah","{0:n}") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>  
             <asp:TemplateField HeaderText="Tax" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# bool.Parse(Eval("tax").ToString()) %>' Enable='<%# !bool.Parse(Eval("tax").ToString()) %>' />
                </ItemTemplate>
            </asp:TemplateField>
           <asp:TemplateField HeaderText="Caj Perkhidmatan (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                 <asp:Label ID="lblgstoth" runat="server" Text='<%# Eval("othgsttype") %>' Visible = "false" />
                    <asp:Label ID="ddtaxoth" runat="server" Text='<%# Eval("othgstname") %>' />
                     <%--<asp:DropDownList ID="ddtaxoth" runat="server" class="form-control uppercase">    </asp:DropDownList>--%>
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Caj Perkhidmatan (RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lblothgst" runat="server" Text='<%# Eval("othgst","{0:n}") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>  
                
             <asp:TemplateField HeaderText="Gst (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                 <asp:Label ID="lblgst" runat="server" Text='<%# Eval("gsttype") %>' Visible = "false" />
                     <asp:Label ID="ddtax" runat="server" Text='<%# Eval("gstname") %>' />
                     <%--<asp:DropDownList ID="ddtax" runat="server" class="form-control uppercase">    </asp:DropDownList>--%>
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Gst (RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lblgstjum" runat="server" Text='<%# Eval("gstjum","{0:n}") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>  
                 <asp:TemplateField HeaderText="Jumlah Termasuk CBP" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lblover" runat="server" Text='<%# Eval("Overall","{0:n}") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>
               <asp:TemplateField HeaderText="Baki Invois (RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lblover1" runat="server" Text='<%# Eval("jumlah_bayaran","{0:n}") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>
                
            </Columns>
            <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />                                                               
           
        </asp:gridview>
                                                                                 
                                                                     

                <asp:gridview ID="Gridview11" runat="server"  class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="20" ShowFooter="true" GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting" onrowdatabound="Gridview7_RowDataBound" >
                 <Columns>
           
             <asp:TemplateField HeaderText="Kod Akaun" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                     <asp:DropDownList ID="ddkoddeb" style="width:100%;" runat="server" class="form-control select2 validate[optional]">    </asp:DropDownList>
                </ItemTemplate>
                 <FooterTemplate>
                 <asp:Button ID="ButtonAdd" runat="server" Text="Add New Row" 
                        onclick="ButtonAdddeb_Click" />
                </FooterTemplate>
            </asp:TemplateField>
                    
            <asp:TemplateField HeaderText="Keterangan" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="txtket" CssClass="form-control uppercase" runat="server" TextMode="MultiLine"  Width="100%" Height="40px"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
                     
                       <asp:TemplateField HeaderText="Jumlah tidak termasuk CBP(RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                     <asp:TextBox ID="Txtdis" runat="server"  CssClass="form-control"  placeholder="0.00"  Width="100%"   OnTextChanged="QtyChangeddeb" AutoPostBack="true"  style="text-align:right;"    ></asp:TextBox>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label3" CssClass="form-control"  runat="server" Text="0.00"  Width="100%"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Tax" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="Chkdeb" runat="server"  OnCheckedChanged="ChckedChangeddeb"  AutoPostBack="true"  />
                </ItemTemplate>
            </asp:TemplateField>
                       <asp:TemplateField HeaderText="Caj Perkhidmatan (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:DropDownList ID="ddgstdeboth" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"  onselectedindexchanged="ddgstdeboth_SelectedIndexChanged" AutoPostBack="true"  >    </asp:DropDownList>
                </ItemTemplate>
                
            </asp:TemplateField>
                      <asp:TemplateField HeaderText="Caj Perkhidmatan (RM)" ItemStyle-HorizontalAlign="Right" >
                <ItemTemplate>
                    <asp:Label ID="Label10" CssClass="form-control" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label11" CssClass="form-control"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="GST (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:DropDownList ID="ddgstdeb" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"  onselectedindexchanged="ddgstdeb_SelectedIndexChanged" AutoPostBack="true" >    </asp:DropDownList>
                </ItemTemplate>
               
            </asp:TemplateField>
                       <asp:TemplateField HeaderText="GST (RM)" ItemStyle-HorizontalAlign="Right" >
                <ItemTemplate>
                    <asp:Label ID="Label8" CssClass="form-control" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label9" CssClass="form-control"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
                      <asp:TemplateField HeaderText="Jumlah Termasuk CBP (RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="Label5" CssClass="form-control" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label6" CssClass="form-control"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            
            </Columns>
            <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                       
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />                                                             
        </asp:gridview>
            
      <asp:gridview ID="Gridview14" runat="server" CssClass="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="20" ShowFooter="true" GridLines="None"  onrowdatabound="Gridview14_RowDataBound" >
         <Columns>
             <asp:TemplateField HeaderText="Kod Akaun" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                 <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("kod_akauan") %>' Visible = "false" />
                    <asp:Label ID="ddkoddup" runat="server" Text='<%# Eval("nama_akaun") %>' />
                     <%--<asp:DropDownList ID="ddkoddup" runat="server" Width="150px" class="form-control uppercase">    </asp:DropDownList>--%>
                </ItemTemplate>
            </asp:TemplateField>
               
                 <asp:TemplateField HeaderText="Keterangan" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                 <asp:Label ID="lblket" runat="server" Text='<%# Eval("keterangan") %>'  />
                </ItemTemplate>
            </asp:TemplateField>
         
              <asp:TemplateField HeaderText="Jumlah tidak termasuk CBP(RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lbljum" runat="server" Text='<%# Eval("jumlah","{0:n}") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>  
             <asp:TemplateField HeaderText="Tax" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# bool.Parse(Eval("tax").ToString()) %>' Enable='<%# !bool.Parse(Eval("tax").ToString()) %>' />
                </ItemTemplate>
            </asp:TemplateField>
           <asp:TemplateField HeaderText="Caj Perkhidmatan (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                 <asp:Label ID="lblgstoth" runat="server" Text='<%# Eval("othgsttype") %>' Visible = "false" />
                    <asp:Label ID="ddtaxoth" runat="server" Text='<%# Eval("othgstname") %>' />
                     <%--<asp:DropDownList ID="ddtaxoth" runat="server" class="form-control uppercase">    </asp:DropDownList>--%>
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Caj Perkhidmatan (RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lblothgst" runat="server" Text='<%# Eval("othgst","{0:n}") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>  
                
             <asp:TemplateField HeaderText="Gst (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                 <asp:Label ID="lblgst" runat="server" Text='<%# Eval("gsttype") %>' Visible = "false" />
                     <asp:Label ID="ddtax" runat="server" Text='<%# Eval("gstname") %>' />
                     <%--<asp:DropDownList ID="ddtax" runat="server" class="form-control uppercase">    </asp:DropDownList>--%>
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Gst (RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lblgstjum" runat="server" Text='<%# Eval("gstjum","{0:n}") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>  
                 <asp:TemplateField HeaderText="Jumlah Termasuk CBP" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lblover" runat="server" Text='<%# Eval("Overall","{0:n}") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>
              

            </Columns>
          <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                       
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />                                                              
        </asp:gridview>
                                               <%--  </div> --%>
                                                   <div class="box-body">&nbsp;</div>
                                                            <div class="row">
                                                                                 <div class="col-md-12">
                                                                                <div class="col-md-8 box-body">&nbsp;</div>
                                                                                     <div class="col-md-4 box-body">
                                                                                    <div class="form-group">
                                                                                        <label for="inputEmail3" class="col-sm-6 control-label"><asp:Label ID="ps_lbl82" runat="server"></asp:Label></label>
                                                                                        <div class="col-sm-6">
                                                                                           <asp:TextBox ID="TextBox5" runat="server" class="form-control" style="text-align:right;"></asp:TextBox> 
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                     </div>
                                </div>
                                                                   
                                                                          <div class="row">
                                                                                 <div class="col-md-12" style="text-align: center">
                                                                                     <div class="col-md-12 box-body">
                                                                                    <div class="form-group">
                                                                                        <div class="col-sm-12">
                                                                                           <asp:Button ID="Button3" runat="server" class="btn btn-danger" Text="Simpan" Type="submit"
                                                                                    OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;" 
                                                                                    onclick="Button3_Click" />

                                                                                     <asp:Button ID="Button21" runat="server" class="btn btn-danger" Text="Print" Type="submit"  onclick="Button21_Click"   />
                                                                                       
                                                                                <asp:Button ID="Button4" runat="server" Text="Tutup" class="btn btn-default" UseSubmitBehavior="false"
                                                                                   onclick="Button4_Click"  /> 
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                     </div>
                                </div>
                                                                          <div class="box-body">&nbsp;</div>
                                                                  </div>
                                                                </div>
                                                                <div role="tabpanel3" class="tab-pane" runat="server" id="p68">
                                                                  <div class="col-md-12 table-responsive uppercase" style="overflow:auto;">

                                                                               <div class="row">
                                                                    
                                                                        <div class="col-md-3 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                   
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               
                                                                                  
                                                                            </div>
                                                                        </div>
                                                                         <div class="col-md-3 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                  
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               
                                                                                  
                                                                            </div>
                                                                        </div>
                                                                          
                                                                        <div class="col-md-6 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                <asp:Label ID="ps_lbl86" runat="server"></asp:Label>  <span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                            <asp:TextBox ID="TextBox28" runat="server" class="form-control"></asp:TextBox> 
                                                                            </div>
                                                                       
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <br /> 

                                                                                  <div class="row">
                                                                       <div class="row">
            <asp:gridview ID="Gridview13" runat="server"  CssClass="table datatable dataTable no-footer" ShowFooter="True"  AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"  >
            <Columns>
            <asp:TemplateField HeaderText="Pilih" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
            <asp:CheckBox ID="chkcreoff" runat="server" />
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="No Rujukan" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
            <asp:Label ID="lblinv" runat="server" Text='<%# Eval("no_invois") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Tarikh Invoice" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
            <asp:Label ID="lbltar" runat="server" Text='<%# Eval("tarikh_invois") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Nama Pelenggan" ItemStyle-HorizontalAlign="Left">
            <ItemTemplate>
            <asp:Label ID="lblsya" runat="server" Text='<%# Eval("Ref_nama_syarikat") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Jumlah" ItemStyle-HorizontalAlign="Right">
            <ItemTemplate>
            <asp:Label ID="lbljum" runat="server" Text='<%# Eval("jumlah") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Debit" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
             <asp:TextBox ID="txtkoffdeb" runat="server" style="text-align:right;" placeholder="0.00"></asp:TextBox>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <%--<RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />--%>
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="White" />
        </asp:gridview>
        </div>  
           <div class="row">
                                                                        <div class="col-md-12 col-sm-2" style="text-align: center">
                                                                            <div class="body collapse in">
                                                                                <asp:Button ID="Button17" runat="server" class="btn btn-danger" Text="Simpan" Type="submit"
                                                                                    OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;" onclick="Btnkoff1_Click" 
                                                                                    />
                                                                                <asp:Button ID="Button18" runat="server" Text="Tutup" 
                                                                                    class="btn btn-default" UseSubmitBehavior="false" onclick="Btnkoffclose1_Click"                                            
                                                                                      />
                                                                            </div>
                                                                        </div>
                                                                    </div>               
                                                                   </div>
                                                                   </div>
                                                                    <br />
                                                                   </div>
                                                                <%--   </div>--%>
                                                                </div>
                                                                  </div> 
                                                        </div>
                                                                   </div>
                                                                      </div>  
                                                                   </div>
                                                                </div>  
                                                       <%-- </div>--%>
                                                    </div>
                            <div class="box-body">&nbsp;</div>
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

