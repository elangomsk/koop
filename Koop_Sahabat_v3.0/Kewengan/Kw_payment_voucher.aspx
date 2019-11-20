<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Kewengan/Kw_payment_voucher.aspx.cs" Inherits="Kw_payment_voucher" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        <style type="text/css">
    .temp > span {
  background-color: white;
  font-family:Calibri;
  color:#19336a;
  font-weight:bold;
  font-size:14px;
  left: 2%;
  position: absolute;
  top: -10px;
}
.temp {
  border: 0.5px solid  #19336a;
  margin-top: 0px;
  padding: 20px;
  position: relative;
}
</style>
  <style>
        .ZebraDialog{
            z-index: 1000001 !important;
        }
    </style>
      <script type="text/javascript">

    $(document).ready(function () {
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

             $("#<%=pp6.ClientID %>").click(function () {
                 $("#<%=hd_txt.ClientID %>").text("MOHON BAYAR");
             });
             $("#<%=pp4.ClientID %>").click(function () {
                 $("#<%=hd_txt.ClientID %>").text("INVOIS / BIL");
             });
             $("#<%=pp1.ClientID %>").click(function () {
                 $("#<%=hd_txt.ClientID %>").text("PAYMENT VOUCHER");
             });
             $("#<%=pp2.ClientID %>").click(function () {
                 $("#<%=hd_txt.ClientID %>").text("NOTA KREDIT");
             });
             $("#<%=pp3.ClientID %>").click(function () {
                 $("#<%=hd_txt.ClientID %>").text("NOTA DEBIT");
             });

             $('.select2').select2();
         });

     </script>
     <script type="text/javascript">

          function RadioCheck(rb) {

              var gv = document.getElementById("<%=grdbilinv1.ClientID%>");

              var rbs = gv.getElementsByTagName("input");

              var row = rb.parentNode.parentNode;

              for (var i = 0; i < rbs.length; i++) {

                  if (rbs[i].type == "radio") {

                      if (rbs[i].checked && rbs[i] != rb) {

                          rbs[i].checked = false;

                          break;

                      }

                  }

              }

          }    

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

  
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server" ScriptMode="Release">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1><asp:Label ID="ps_lbl1" Visible="false" runat="server"></asp:Label>Pembayaran</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i><asp:Label ID="ps_lbl2" Visible="false" runat="server"></asp:Label></a> Kewangan</li>
                            <li class="active"><asp:Label ID="ps_lbl3" Visible="false" runat="server"></asp:Label>Pembayaran</li>
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
                            <h3 class="box-title"><asp:Label ID="ps_lbl4" Visible="false" runat="server"></asp:Label> <asp:Label Visible="false" ID="hd_txt" runat="server"></asp:Label>Pembayaran</h3>
                         <%--   <div class="type-1 ">
                              <div class="type-in">
                                <a href="../Kewengan/Fin_module.aspx?edit=P0201" class="btn btn-1">
                                  <span class="txt">Close</span>
                                  <span class="round"><i class="fa fa-times"></i></span>
                                </a>
                              </div>
                             </div>--%>
                        </div>
                       <%-- <div class="chat-panel panel panel-primary"></div>--%>
                        <!-- /.box-header -->
                        <!-- form start -->
                         
                        <div class="form-horizontal">
                           <div class="col-md-12">
                               
                                                    <%--  <div class="row">
                                                <div class="col-md-12 col-sm-2" style="text-align: center">
                                                    <div class="body collapse in">
                                                        <asp:Button ID="Button6" runat="server" class="btn btn-danger" Text="Simpan" Type="submit" OnClick="stf_update"/>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />--%>
                                                    <div class="panel" style="width: 100%;">
                                                        <div id="Div1" class="nav-tabs-custom" role="tabpanel" runat="server">
                                                            <!-- Nav tabs -->
                                                            <ul class="s1 nav nav-tabs" role="tablist" style="display:none;">
                                                            <li id="pp6" runat="server" class="active"><a href="#ContentPlaceHolder1_p6" aria-controls="p6" role="tab" data-toggle="tab"><strong><asp:Label ID="ps_lbl6" runat="server"></asp:Label></strong></a>
                                                               
                                                            </li>
                                                                <li id="pp4" runat="server"><a href="#ContentPlaceHolder1_p4" aria-controls="p4" role="tab" data-toggle="tab"><strong><asp:Label ID="ps_lbl7" runat="server"></asp:Label></strong></a></li>
                                                                <li id="pp1" runat="server"><a href="#ContentPlaceHolder1_p1" aria-controls="p1" role="tab" data-toggle="tab"><strong><asp:Label ID="ps_lbl8" runat="server"></asp:Label></strong></a></li>
                                                                <li id="pp2" runat="server"><a href="#ContentPlaceHolder1_p2" aria-controls="p2" role="tab" data-toggle="tab"><strong><asp:Label ID="ps_lbl9" runat="server"></asp:Label></strong></a></li>
                                                                <li id="pp3" runat="server"><a href="#ContentPlaceHolder1_p3" aria-controls="p3" role="tab" data-toggle="tab"><strong><asp:Label ID="ps_lbl10" runat="server"></asp:Label></strong></a></li>       
                                                               
                                                                
                                                            </ul>
                                                            <!-- Tab panes -->
                                                            <div class="tab-content">
                                                            <div role="tabpanel" class="tab-pane active" runat="server" id="p6">
                                                                    <div class="col-md-12 table-responsive"  style="overflow:auto;">
                                                                     <div id="Div3"  runat="server">
                                                                        <%-- <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-3 box-body">
                                <div class="form-group">
                                    <div class="col-sm-12">
                                         <asp:TextBox ID="TextBox9" runat="server" class="form-control" Placeholder="Carian"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-3 box-body">
                                <div class="form-group">
                                    <div class="col-sm-12">
                                     <asp:Button ID="Button3" runat="server" class="btn btn-danger" Text="+Tambah" 
                                                                    UseSubmitBehavior="false" onclick="Button3_Click"
                                                                 />
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>--%>
                                                                             
                                                                    <fieldset class="col-md-12">
                                                                     <legend style="width:4%"><asp:Label ID="ps_lbl11" runat="server"></asp:Label></legend>
                                                                        <div class="row">
                                                                         <div class="col-md-12">
                                                                        <div class="col-md-3 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label Visible="false" ID="ps_lbl12" runat="server"></asp:Label>No Baucer Bayaran</label>
                                                                                <div class="col-sm-7">
                                                                                     <asp:TextBox ID="txtnoinvois" runat="server" class="form-control uppercase"></asp:TextBox> 
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                               <div class="col-md-3 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl13" Visible="false" runat="server"></asp:Label> Tarikh Baucer</label>
                                                                                <div class="col-sm-7">
                                                                                      <div class="input-group">
                                                                                  <asp:TextBox ID="txttarikhinvois" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox> 
                                                                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-3 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl14" Visible="false"  runat="server"></asp:Label> No Invois</label>
                                                                                <div class="col-sm-7">
                                                                                   <asp:TextBox ID="TextBox13" runat="server" class="form-control uppercase"></asp:TextBox> 
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                           
                                                                             <div class="col-md-3 box-body">
                                                                            <div class="form-group">
                                                                                <div class="col-sm-12">
                                                                                      <asp:CheckBox ID="mb_chk" Visible="false" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                   <asp:Button ID="but" runat="server" class="btn btn-danger" Text="Carian" 
                                                                    UseSubmitBehavior="false" onclick="but_Click"    />
                                                                                     <asp:Button ID="Button3" runat="server" Visible="false" class="btn btn-primary" Text=" Tambah" 
                                                                    UseSubmitBehavior="false" onclick="Button3_Click"
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
                                                                        <asp:gridview ID="Gridview2" runat="server"  class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" onrowdatabound="GridView1_RowDataBound1" OnPageIndexChanging="gvSelected_PageIndexChanging_g2" >
            <Columns>
                <asp:TemplateField HeaderText="BIL">  
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                 <asp:TemplateField HeaderText="NO Rujukan" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="no_invois" runat="server" Text='<%# Eval("pv_no") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Jenis Permohonan" ItemStyle-HorizontalAlign="Left">  
                    <ItemStyle HorizontalAlign="Left" />
                                            <ItemTemplate>  
                                                <asp:Label ID="jenis_per" runat="server" Text='<%# Eval("jp_desc") %>'></asp:Label>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                <asp:BoundField DataField="tarkih_permohonan" HeaderText="Tarkih BAUCER" ItemStyle-HorizontalAlign="Center" />            
                <asp:TemplateField HeaderText="Bayar kepada" ItemStyle-HorizontalAlign="Left">  
                    <ItemStyle HorizontalAlign="Left" />
                                            <ItemTemplate>  
                                                <asp:Label ID="Bayar_kepada" runat="server" Text='<%# Eval("bk") %>'></asp:Label>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                <asp:BoundField DataField="no_inv" HeaderText="No INVOIS" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="perkara" HeaderText="Perkara" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
      <%--      <asp:BoundField DataField="nama" HeaderText="Nama" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />--%>                  <asp:TemplateField HeaderText="NO BAUCER BAYARAN" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="pv_no" runat="server" Text='<%# Eval("pvno") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                <asp:BoundField DataField="jumlah" HeaderStyle-HorizontalAlign="Right" HeaderText="Amaun (RM)" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" />
                <%-- <asp:BoundField DataField="status" HeaderText="Status" ItemStyle-HorizontalAlign="Center" />--%>
                <asp:TemplateField HeaderText="Status GL" ItemStyle-HorizontalAlign="Center">  
                    <ItemStyle HorizontalAlign="Center" Font-Bold />
                                            <ItemTemplate>  
                                                <asp:Label ID="status" Width="100%" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
              
 <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">  
                    <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>  
<asp:Label ID="lbl_mohon_no" runat="server" Text='<%# Eval("pv_no") %>' Visible="false"></asp:Label>
    <asp:Label ID="Label17" runat="server" Text='' Visible="false"></asp:Label>
                                                <asp:Label ID="Label18" runat="server" Text='<%# Eval("bk") %>' Visible="false"></asp:Label>
                                                <asp:LinkButton ID="LinkButton1" runat="server"  CommandArgument='<%# Eval("pv_no")+","+ Eval("jp_cd")+","+ Eval("perkara")%>' CommandName="Add"  onclick="lblSubbind_Click" ToolTip='<%# Eval("pv_no").ToString().ToUpper() %>' ><i class='fa fa-search'></i></asp:LinkButton>                                                                    
                                                                                         </ItemTemplate>  
                                                </asp:TemplateField>
               <asp:BoundField DataField="tarkih_permohonan" HeaderText="Tarkih Kelulusan" Visible="false" ItemStyle-HorizontalAlign="Center" />
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
                                                               
                                                                   <div id="Div2" class="nav-tabs-custom" role="tabpanel1" runat="server">
                                                            <!-- Nav tabs -->
                                                            <ul class="s1 nav nav-tabs" role="tablist1" style="display:none;">
                                                            <li id="Li1" runat="server" class="active"><a href="#ContentPlaceHolder1_p61" aria-controls="p61" role="tab" data-toggle="tab"><asp:Label ID="ps_lbl17" runat="server"></asp:Label></a>

                                                            </li>
                                                              
                                                                <%--<li id="pp2" runat="server"><a href="#ContentPlaceHolder1_p2" aria-controls="p2" role="tab" data-toggle="tab">ELAUN TETAP</a></li>
                                                                <li id="pp3" runat="server"><a href="#ContentPlaceHolder1_p3" aria-controls="p3" role="tab" data-toggle="tab">LAIN-LAIN ELAUN</a></li>
                                                                <li id="pp4" runat="server"><a href="#ContentPlaceHolder1_p4" aria-controls="p4" role="tab" data-toggle="tab">KERJA LEBIH MASA</a></li>
                                                                <li id="pp5" runat="server"><a href="#ContentPlaceHolder1_p5" aria-controls="p5" role="tab" data-toggle="tab">BONUS</a></li>--%>
                                                                
                                                            </ul>
                                                            <!-- Tab panes -->
                                                            <div class="tab-content">
                                                            <div role="tabpanel1" class="tab-pane active" runat="server" id="p61">
                                                                   
                                                                    <div class="col-md-12 box-body" style="overflow:auto;">
                                                                        <asp:gridview ID="grdbind" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="20" ShowFooter="true" GridLines="None" >
            <Columns>
                 <asp:TemplateField HeaderText="BIL">  
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="No Invois" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                  <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("mhn_no_permohonan") %>'  CommandArgument='<%# Eval("mhn_no_permohonan") %>' CommandName="Add"  onclick="lblSubItemName_Click" Font-Bold Font-Underline ></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
                  
                <asp:BoundField DataField="mhn_amount" HeaderText="Jumlah (RM)" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
               
            </Columns>
              <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                       
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />
        </asp:gridview>
                                                                       <%--  <div class="box-header with-border">
                            <h3 class="box-title">Butiran Pemohon</h3>
                        </div>--%>
                                                                        
                                                                    <div class="row" style="display:none;">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl5" runat="server"></asp:Label><span class="style1">*</span></label>
                                    <div class="col-sm-7">
                                            <asp:DropDownList ID="ddakaun" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                                </asp:DropDownList>
                                         <asp:TextBox ID="start_dt1" runat="server" Visible="false"></asp:TextBox>
                                        <asp:TextBox ID="end_dt1" runat="server" Visible="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>                      
 
                                                                 <fieldset class="col-md-12">
                                                                     <legend style="width:13%">Maklumat Baucer Bayaran</legend>     
 
                                                                   <div class="row" style="display:none;">
                                                                         <div class="col-md-12">
                                                                              <div class="col-md-6 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label Visible="false" ID="ps_lbl18" runat="server"></asp:Label>Nama Pemohon</label>
                                                                                <div class="col-sm-8">
                                                                                  <asp:TextBox ID="txtid" runat="server" class="form-control uppercase"></asp:TextBox> 
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-6 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-4 control-label">Id Pemohon</label>
                                                                                <div class="col-sm-8">
                                                                                  <asp:TextBox ID="TextBox3" runat="server" class="form-control uppercase" ReadOnly="true"></asp:TextBox> 
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              
                                                                             </div>
                                                                     </div>
                                                                         <div class="row"  style="display:none;">
                                                                         <div class="col-md-12">
                                                                              <div class="col-md-6 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-4 control-label">Syeksen</label>
                                                                                <div class="col-sm-8">
                                                                                  <asp:TextBox ID="TextBox4" runat="server" class="form-control uppercase" ReadOnly="true"></asp:TextBox> 
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-6 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-4 control-label">Jawatan</label>
                                                                                <div class="col-sm-8">
                                                                                  <asp:TextBox ID="TextBox5" runat="server" class="form-control uppercase" ReadOnly="true"></asp:TextBox> 
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              
                                                                             </div>
                                                                     </div>
                                                                      <div class="row">
                                                                         <div class="col-md-12">
                                                                              <div class="col-md-6 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-4 control-label">Jenis Permohonan</label>
                                                                                <div class="col-sm-8">
                                                                                  <asp:TextBox ID="TextBox23" runat="server" class="form-control uppercase" ReadOnly="true"></asp:TextBox>                                                                                     <asp:TextBox ID="TextBox33" Visible="false" runat="server" class="form-control uppercase"></asp:TextBox> 
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-6 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-4 control-label">No Dokumen</label>
                                                                                <div class="col-sm-8">
                                                                                  <asp:TextBox ID="TextBox35" runat="server" class="form-control uppercase" ReadOnly="true"></asp:TextBox>                                                                                     
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             </div>
                                                                     </div>
                                                                  <div class="row">
                                                                         <div class="col-md-12">
                                                                               <div class="col-md-6 box-body" style="display:none;">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-4 control-label">Akaun Syarikat <span class="style1">*</span></label>
                                                                                <div class="col-sm-8">
                                                                                 <asp:DropDownList ID="DropDownList3" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" >
                                                                                     </asp:DropDownList>
                                                                                </div>
                                                                                </div>
                                                                            </div>
                                                                              <div class="col-md-6 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl20" Visible="false" runat="server"></asp:Label> Tarikh Baucer <span class="style1">*</span></label>     
                                                                                <div class="col-sm-8">
                                                                                 <div class="input-group">
                                                                                      <asp:TextBox ID="TextBox7" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker2 mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox> 
                                                                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                                <asp:TextBox ID="txttarkihmo" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker2 mydatepickerclass" Visible="false" placeholder="DD/MM/YYYY"></asp:TextBox> 
                                                                                           
                                             </div>
                                                                                </div>
                                                                                </div>
                                                                            </div>
                                                                               <div class="col-md-6 box-body" id="mb_tab1" runat="server">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl19" Visible="false" runat="server"></asp:Label> No Baucer Bayaran <span class="style1">*</span></label>
                                                                                <div class="col-sm-8">
                                                                                     <div class="input-group">
                                                                                 <asp:TextBox ID="txtnoper" runat="server" class="form-control uppercase"></asp:TextBox> 
                                                                                    <asp:TextBox ID="txtnoper_1" runat="server" Visible="false" class="form-control uppercase"></asp:TextBox> 
                                                                                         <span class="input-group-addon"><i class="fa fa-pencil"></i></span>
                                             </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                                
                                                                              <div class="col-md-6 box-body" style="display:none;">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-4 control-label"> Pelenggan <span class="style1">*</span></label>
                                                                                <div class="col-sm-8">
                                                                                   <div class="col-sm-9">
                                                                                   <asp:DropDownList ID="DropDownList1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" OnSelectedIndexChanged="ddbkepada_SelectedIndexChanged" AutoPostBack="true">
                                                                                     <asp:ListItem Value="">--- PILIH ---</asp:ListItem>
                                                                                       <asp:ListItem Value="01">KAKITANGAN</asp:ListItem>
                                                                                     <%--  <asp:ListItem Value="02">PEMBEKAL</asp:ListItem>--%>
                                                                                       <asp:ListItem Value="03" >PELANGGAN</asp:ListItem>
                                                                                       </asp:DropDownList>
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
                                                                                <label for="inputEmail3" class="col-sm-4 control-label"> Bentuk Bayaran <span class="style1">*</span></label>
                                                                                <div class="col-sm-8">
                                                                                  <asp:DropDownList ID="DropDownList5" style="width:100%; font-size:13px;" runat="server" OnSelectedIndexChanged="shw_resit" AutoPostBack="true" class="form-control select2 validate[optional]">
                                                                                   </asp:DropDownList>
                                                                                </div>
                                                                                </div>
                                                                            </div>
                                                                             <div class="col-md-6 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="Label20" Visible="false" runat="server"></asp:Label>Nama Pembekal / Penerima</label>                     
                                                                                <div class="col-sm-8">                                                                                     
                                                                                    <div class="input-group">
                                                                                   <asp:TextBox ID="TextBox24" runat="server" class="form-control" ReadOnly="true" ></asp:TextBox> 
                                                                                         <span class="input-group-addon"><i class="fa fa-pencil"></i></span>
                                             </div>
                                                                                                                                                              
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              
                                                                      </div>
                                                                             </div>
                                                                      <div class="row" style="display:none;">
                                                                         <div class="col-md-12">
                                                                        <div class="col-md-6 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl22" runat="server"></asp:Label> <span class="style1">*</span></label>
                                                                                <div class="col-sm-8">
                                                                                <asp:TextBox ID="txticno" runat="server" class="form-control" ></asp:TextBox> 
                                                                                </div>
                                                                                </div>
                                                                            </div>
                                                                              <div class="col-md-6 box-body">
                                                                            <div class="form-group">
                                                                                <div class="col-sm-7">
                                                                                   <asp:Button ID="btncarian" runat="server" class="btn btn-danger" Text="Carian" 
                                                                    UseSubmitBehavior="false" onclick="btncarian_Click"  />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                      </div>
                                                                             </div>                                                                     
                                                                      <div class="row">
                                                                         <div class="col-md-12" >
                                                                               <div class="col-md-6 box-body" style="display:none">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-4 control-label">Tarikh Invois <span class="style1">*</span></label>
                                                                                <div class="col-sm-8">
                                                                                  <div class="input-group">
                                                                               
                                             </div>
                                                                                </div>
                                                                                </div>
                                                                            </div>
                                                                            
                                                                        <div class="col-md-6 box-body" style="display:none;">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl24" Visible="false" runat="server"></asp:Label> No Rujukan </label>
                                                                                <div class="col-sm-8">
                                                                                 <asp:TextBox ID="txtname" runat="server" class="form-control uppercase"></asp:TextBox> 
                                                                                </div>
                                                                                </div>
                                                                            </div>
                                                                             
                                                                              <div class="col-md-6 box-body" style="display:none;">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl25" Visible="false" runat="server"></asp:Label> <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                   <asp:TextBox ID="txtbname" runat="server" class="form-control uppercase"></asp:TextBox> 
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-4 box-body" style="display:none;">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl26" runat="server"></asp:Label><span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                  <asp:TextBox ID="txtbno" runat="server" class="form-control"></asp:TextBox> 
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                      </div>
                                                                             </div>
                                                                        <div class="row" style="display:none;">
                                                                         <div class="col-md-12">
                                                                             
                                                                            <div class="col-md-6 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-4 control-label">P.O No. </label>
                                                                                <div class="col-sm-8">
                                                                                      <div class="input-group">
                                                                                  <asp:TextBox ID="TextBox9" runat="server" class="form-control uppercase validate[optional]" MaxLength="20"></asp:TextBox> 
                                                                                           <span class="input-group-addon"><i class="fa fa-edit"></i></span>
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
                                                                                <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl21" Visible="false" runat="server"></asp:Label>Projek</label>                     
                                                                                <div class="col-sm-8">
                                                                                   <asp:DropDownList ID="ddbkepada" Visible="false" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" >
                                                                                </asp:DropDownList>
                                                                                       <div class="input-group">
                                                                                  <asp:TextBox ID="TextBox26" runat="server" class="form-control uppercase validate[optional]" ReadOnly="true"></asp:TextBox>                                                                                              <asp:TextBox ID="TextBox34" runat="server" class="form-control uppercase validate[optional]" Visible="false"></asp:TextBox> 
                                                                                           <span class="input-group-addon"><i class="fa fa-pencil"></i></span>
                                             </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-6 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label Visible="false" ID="ps_lbl27" runat="server"></asp:Label> No Invois</label>
                                                                                <div class="col-sm-8">
                                                                                  <asp:DropDownList Visible="false" ID="dd_terma" runat="server" class="form-control select2">
                                                                                </asp:DropDownList>
                                                                                       <div class="input-group">
                                                                                  <asp:TextBox ID="TextBox27" runat="server" class="form-control uppercase validate[optional]" ReadOnly="true"></asp:TextBox> 
                                                                                           <span class="input-group-addon"><i class="fa fa-pencil"></i></span>
                                             </div>
                                                                                </div>
                                                                                </div>
                                                                            </div>
                                                                        <div class="col-md-6 box-body" style="display:none;">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-4 control-label">D.O No. </label>
                                                                                <div class="col-sm-8">
                                                                                     <div class="input-group">
                                                                               <asp:TextBox ID="TextBox14" runat="server" class="form-control uppercase validate[optional]" MaxLength="20"></asp:TextBox> 
                                                                                           <span class="input-group-addon"><i class="fa fa-edit"></i></span>
                                             </div>
                                                                                  
                                                                                  
                                                                                </div>
                                                                                </div>
                                                                            </div>
                                                                              
                                                                                <div class="col-md-4 col-sm-2" id="jurnal_show" runat="server" visible="false">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                     Jurnal No :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                                     <asp:DropDownList ID="jurnal_no" runat="server" class="form-control uppercase" OnSelectedIndexChanged="jurnal_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                               <div class="col-md-4 col-sm-2" id="jurnal_show2" runat="server" visible="false">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                     Jurnal No :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                                     <asp:TextBox ID="TextBox2" runat="server" class="form-control uppercase"></asp:TextBox> 
                                                                            </div>
                                                                        </div>
                                                                      </div>
                                                                             </div>
                                                                      <div class="row">
                                                                         <div class="col-md-12">
                                                                             <div class="col-md-6 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-4 control-label"> Nama Bank Pembekal </label>
                                                                                <div class="col-sm-8">
                                                                              
                                                                                   <div class="input-group">
                                                                                      <asp:TextBox ID="TextBox19" runat="server" class="form-control validate[optional]" ReadOnly="true"></asp:TextBox> 
                                                                                           <span class="input-group-addon"><i class="fa fa-pencil"></i></span>
                                             </div>
                                                                                </div>
                                                                                </div>
                                                                            </div>
                                                                                <div class="col-md-6 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="Label21" Visible="false" runat="server"></asp:Label> No Akaun Bank Pembekal </label>
                                                                                <div class="col-sm-8">
                                                                                 <div class="input-group">
                                                                                      <asp:TextBox ID="TextBox22" runat="server" class="form-control validate[optional]" ReadOnly="true"></asp:TextBox> 
                                                                                           <span class="input-group-addon"><i class="fa fa-pencil"></i></span>
                                             </div>
                                                                                </div>
                                                                                </div>
                                                                            </div>
                                                                      </div>
                                                                    </div>
                                                                              <div class="row" style="display:none;">
                                                                         <div class="col-md-12">
                                                                              <div class="col-md-6 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-4 control-label">No Akaun Bank <span class="style1">*</span></label>
                                                                                <div class="col-sm-8">
                                                                                     <div class="input-group">
                                                                                  <asp:TextBox ID="TextBox15" runat="server" class="form-control uppercase" MaxLength="20"></asp:TextBox>
                                                                                           <span class="input-group-addon"><i class="fa fa-edit"></i></span>
                                             </div>
                                                                                
                                                                                </div>
                                                                                </div>
                                                                            </div>
                                                                               
                                                                     
                                                                            
                                                                      </div>
                                                                    </div>
                                                                   
                                                                     </fieldset>
                                                                <div class="box-body">&nbsp;</div>
                                                                        <fieldset class="col-md-12">                                                      
                                                                    <div class="row">
                                                                         <div class="col-md-12">
                                                                                <div class="col-md-6 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-4 control-label">Perkara </label>
                                                                                <div class="col-sm-8">
                                                                                  <textarea id="TextBox16" rows="3" runat="server" class="form-control uppercase validate[optional]" readonly="readonly"></textarea> 
                                                                                </div>
                                                                                </div>
                                                                            </div>
                                                                             <div class="col-md-6 box-body">
                                                                                  <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-4 control-label"> Terima </label>
                                                                                <div class="col-sm-8">
                                                                              
                                                                                   <div class="input-group">
                                                                                      <asp:TextBox ID="TextBox31" runat="server" class="form-control validate[optional]" ReadOnly="true"></asp:TextBox> 
                                                                                           <span class="input-group-addon"><i class="fa fa-pencil"></i></span>
                                             </div>
                                                                                </div>
                                                                                </div>
                                                                                  <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-4 control-label"> Tarikh Invois </label>
                                                                                <div class="col-sm-8">
                                                                              
                                                                                   <div class="input-group">
                                                                                      <asp:TextBox ID="TextBox32" runat="server" class="form-control validate[optional]" ReadOnly="true"></asp:TextBox>                                                                                        <asp:TextBox ID="TextBox38" runat="server" class="form-control validate[optional]" Visible="false"></asp:TextBox> 
                                                                                           <span class="input-group-addon"><i class="fa fa-pencil"></i></span>
                                             </div>
                                                                                </div>
                                                                                </div>
                                                                            </div>
                                                                      </div>
                                                                    </div>
                                                                              
                                                                    </fieldset>
                                                                         
                                                                        <div id="mak_resit" runat="server">
                                                                        <fieldset class="col-md-12">     
                                                                             <legend style="width:9%">Maklumat Cek</legend>                                                      
                                                                     <div class="row">
                                                                         <div class="col-md-12">
                                                                             <div class="col-md-4 box-body">
                                                                               <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-4 control-label"> Di Bayar Oleh (CR) <span class="style1">*</span></label>
                                                                                <div class="col-sm-8">
                                                                                  <asp:DropDownList ID="DropDownList4" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                                   </asp:DropDownList>
                                                                                </div>
                                                                                </div>
                                                                                 </div>
                                                                             <div class="col-md-4 box-body" runat="server" id="cek_det1">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-4 control-label"> No Cek </label>
                                                                                <div class="col-sm-8">
                                                                                   <div class="input-group">
                                                                                      <asp:TextBox ID="TextBox28" runat="server" class="form-control validate[optional]"></asp:TextBox> 
                                                                                           <span class="input-group-addon"><i class="fa fa-pencil"></i></span>
                                             </div>
                                                                                </div>
                                                                                </div>
                                                                            </div>
                                                                                <div class="col-md-4 box-body" runat="server" id="cek_det2">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="Label22" Visible="false" runat="server"></asp:Label> Terikh Cek </label>
                                                                                <div class="col-sm-8">
                                                                                 <div class="input-group">
                                                                                      <asp:TextBox ID="TextBox30" runat="server" class="form-control validate[optional] datepicker2 mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox> 
                                                                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                                                                </div>
                                                                                </div>
                                                                            </div>                                                                              <div class="col-md-4 box-body" runat="server" id="cek_det3">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-4 control-label"> No Rujukan </label>
                                                                                <div class="col-sm-8">
                                                                                   <div class="input-group">
                                                                                      <asp:TextBox ID="TextBox36" runat="server" class="form-control validate[optional]"></asp:TextBox> 
                                                                                           <span class="input-group-addon"><i class="fa fa-pencil"></i></span>
                                             </div>
                                                                                </div>
                                                                                </div>
                                                                            </div>
                                                                      </div>
                                                                    </div>
                                                                              
                                                                    </fieldset>
                                                                            </div>
                                                                 <div class="box-body">&nbsp;</div>
                                                                 <div class="row" style="display:none;">
                                                                         <div class="col-md-12">
                                                                        <div class="col-md-6 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl28" runat="server"></asp:Label> <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                 <asp:TextBox ID="txtjenis" runat="server" class="form-control uppercase"  ></asp:TextBox>
                                                                                </div>
                                                                                </div>
                                                                            </div>
                                                                      </div>
                                                                             </div>
                                                                 <div class="row" style="display:none;">
                                                                         <div class="col-md-12">
                                                                        <div class="col-md-6 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl29" runat="server"></asp:Label> <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                  <asp:DropDownList ID="ddstatus" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                                   <asp:ListItem>--- PILIH ---</asp:ListItem>
                                                                                     <asp:ListItem Value="B">BARU</asp:ListItem>
                                                                                     <asp:ListItem Value="P">SEMAKAN</asp:ListItem>
                                                                                     <asp:ListItem Value="L">LULUS</asp:ListItem>
                                                                                     <asp:ListItem Value="G">GAGAL</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                </div>
                                                                                </div>
                                                                            </div>
                                                                              <div class="col-md-6 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl30" runat="server"></asp:Label> <span class="style1">*</span></label>                  
                                                                                <div class="col-sm-7">
                                                                                   <asp:DropDownList ID="dd_terma1" runat="server" class="form-control">
                                                                                      <asp:ListItem>--- PILIH ---</asp:ListItem>
                                                                                 <asp:ListItem value="30">30</asp:ListItem>
                                                                                  <asp:ListItem value="60">60</asp:ListItem>
                                                                                  <asp:ListItem  value="90">90</asp:ListItem>
                                                                                  <asp:ListItem  value="COD">COD</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                </div>
                                                                                </div>
                                                                            </div>
                                                                      </div>
                                                                             </div>
                                                                            <div id="mak_dok_shw" runat="server" Visible="false"></div>
                                                                       
                                                                     <div class="box-header with-border">
                            <h3 class="box-title">Butiran</h3>
                        </div>
                                                                       <div class="dataTables_wrapper form-inline dt-bootstrap" >
                                      <div class="row" style="overflow:auto;">
           <div class="col-md-12 box-body">
          <asp:gridview ID="grdmohon" runat="server"  class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="20" ShowFooter="true" GridLines="None" OnRowDeleting="Mohon_grvStudentDetails_RowDeleting" onrowdatabound="grdmohon_RowDataBound" >
            <Columns>

            <asp:TemplateField HeaderText="Bayar Kepada" Visible="false" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:DropDownList ID="TextBox11" runat="server" Width="100%" class="form-control uppercase select2"></asp:DropDownList>
                </ItemTemplate>
               
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Keterangan" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox13" runat="server" TextMode="MultiLine" class="form-control uppercase" Height="60px" Width="100%"  ></asp:TextBox>
                </ItemTemplate>
                       <FooterTemplate>
                 <asp:Button ID="ButtonAdd" runat="server" style="width:100%;" CssClass="btn btn-success" Text="Add New Row" 
                        onclick="ButtonAdd1_Click" />
                </FooterTemplate>
            </asp:TemplateField>
                  <asp:TemplateField HeaderText="Projek" Visible="false" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:DropDownList ID="dd_projek" class="form-control uppercase select2" Width="100%" runat="server"></asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
                   <asp:TemplateField HeaderText="Nama Bank" Visible="false"  ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:DropDownList ID="dd_bank" class="form-control uppercase select2" Width="100%"  runat="server"></asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="No Akaun Bank" Visible="false"  ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox12" class="form-control uppercase" Width="100%" MaxLength="20" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            
             <asp:TemplateField HeaderText="Harga / Unit" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="right">
                <ItemTemplate>
                      
                    <asp:TextBox ID="TextBox53" runat="server" CssClass="form-control"  Width="100%"   placeholder="0.00" style="text-align:right;" OnTextChanged="Mohon_QtyChangedBil_kty" AutoPostBack="true" ></asp:TextBox>
                      
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Qty" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                      
                    <asp:TextBox ID="TextBox54" runat="server" CssClass="form-control"  Width="100%"    placeholder="0" OnTextChanged="Mohon_QtyChangedBil" AutoPostBack="true"></asp:TextBox>
                      
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Disk (RM)" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                                          <asp:TextBox ID="Txtdis" runat="server"  CssClass="form-control"  Width="100%"  placeholder="0.00" style="text-align:right;"  OnTextChanged="Mohon_disChangedBil" AutoPostBack="true"   ></asp:TextBox>
                      
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Jumlah Kecil (RM)" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                <ItemTemplate>
                     <asp:Label ID="Label5" CssClass="form-control"  Width="100%" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                 <asp:Label ID="Label6" CssClass="form-control"  Width="100%" runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Tax" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" Checked="true" style="pointer-events:none;" runat="server" Width="100%" OnCheckedChanged="ChckedChangedBil" AutoPostBack="true"  />
                </ItemTemplate>
            </asp:TemplateField>

                <asp:TemplateField HeaderText="Caj Perkhidmatan %" Visible="false" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:DropDownList ID="ddcukaioth" style="width:100%;" runat="server" class="form-control select2 validate[optional]" onselectedindexchanged="Mohon_ddcukaiothBil_SelectedIndexChanged" AutoPostBack="true"  >    </asp:DropDownList>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label7" CssClass="form-control"  Width="100%"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
                      <asp:TemplateField HeaderText="Caj Perkhidmatan (RM)" ItemStyle-HorizontalAlign="Right"  Visible="false">
                <ItemTemplate>
                    <asp:Label ID="Label10" CssClass="form-control"  Width="100%" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label11" CssClass="form-control"  Width="100%"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Jenis Cukai" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                     
                    <asp:DropDownList ID="ddcukaiinv" style="width:100%;" runat="server" class="form-control select2 validate[optional]"  onselectedindexchanged="Mohon_ddcukaiinvBil_SelectedIndexChanged" AutoPostBack="true" >    </asp:DropDownList>
                     
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label4" CssClass="form-control"  Width="100%"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
                       <asp:TemplateField HeaderText="Cukai (RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="Label8" CssClass="form-control"  Width="100%" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label9" CssClass="form-control"  Width="100%"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Amaun (RM)" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                <ItemTemplate>
                  <asp:Label ID="Label1" CssClass="form-control"  Width="100%" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
           <FooterTemplate>
                   <asp:Label ID="Label2" CssClass="form-control"  Width="100%" runat="server" Text="0.00"  ></asp:Label>
         </FooterTemplate>
            </asp:TemplateField>
                
               <%--  <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Remove</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>--%>
                    <%--<asp:CommandField ShowDeleteButton="True" />--%>
            </Columns>
           <%-- <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="White" />--%>
        </asp:gridview>

<div class="box-body">&nbsp;</div>
                    <asp:gridview ID="gridmohdup" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="5" CellSpacing="5" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="30" ShowFooter="true" GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting" onrowdatabound="gridmohdup_RowDataBound" >
            <Columns>
                  <asp:TemplateField HeaderText="BIL">  
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                
              <asp:TemplateField HeaderText="Kod kepada" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="Label23_kepada" runat="server" Visible="false" Text='<%# Eval("kepada") %>' class="uppercase" ></asp:Label>
                    <asp:Label ID="kepada" runat="server" Text='<%# Eval("kepada") %>' class="uppercase" ></asp:Label>
                    <asp:Label ID="Label23" runat="server" Text='<%# Eval("kepada") %>' Visible="false" class="uppercase" ></asp:Label>
                    <asp:Label ID="Label22" runat="server" Text='<%# Eval("kat") %>' Visible="false" class="uppercase" ></asp:Label>
                    <asp:Label ID="Label24" runat="server" Text='<%# Eval("Id") %>' Visible="false" class="uppercase" ></asp:Label>
                    <asp:Label ID="Label25" runat="server" Text='<%# Eval("kod_bajet") %>' Visible="false" class="uppercase" ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>                 <asp:TemplateField HeaderText="Kod Bajet"  HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center">
                <ItemTemplate>
                    <asp:Label ID="kd_bajet" runat="server" Text='<%# Eval("kod_bajet") %>' class="uppercase" ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Keteragan" HeaderText="Keterangan" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
             <asp:BoundField DataField="projek" Visible="false" HeaderText="Projek" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
<asp:BoundField DataField="bank" HeaderText="Nama Bank" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
<asp:BoundField DataField="acc_no" HeaderText="No Akaun Bank" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
             <asp:BoundField DataField="unit" HeaderText="Harga / Unit (RM)" HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="qty" HeaderText="QTY" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" />
              <asp:BoundField DataField="Disk" HeaderText="Disk (RM)" HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" />                  <asp:BoundField DataField="amount" HeaderText="Jumlah Kecil (RM)" HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" />
               <asp:BoundField DataField="jen_cukai" HeaderText="Jenis Cukai %" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
<asp:BoundField DataField="cukai_amt" HeaderText="Cukai (RM)" HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" />                  <asp:TemplateField HeaderText="jumlah"  HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="jumlah" runat="server" Text='<%# Eval("jumlah","{0:n}") %>' class="uppercase" ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            </Columns>
           <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                      <%--  <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />--%>
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />      
        </asp:gridview>               <asp:gridview ID="Gridview1" runat="server"  class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="20" ShowFooter="true" GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting" onrowdatabound="Gridview3_RowDataBound" >
            <Columns>
                 <asp:TemplateField HeaderText="BIL">  
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Jenis Permohonan" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                 <asp:Label ID="docname" runat="server" Text='<%# Eval("Ref_doc_name") %>'  />
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Kod Akaun" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                 <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("kod_akauan") %>' Visible = "false" />
                    <asp:Label ID="ddkoddup" runat="server" Text='<%# Eval("nama_akaun") %>' />
                    <asp:Label ID="Label26" runat="server" Visible="false" Text='<%# Eval("txn_type") %>' />
                    <asp:Label ID="Label27" runat="server" Visible="false" Text='<%# Eval("inv_amt") %>' />
                     <%--<asp:DropDownList ID="ddkoddup" runat="server" Width="150px" class="form-control uppercase">    </asp:DropDownList>--%>
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Keterangan" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                 <asp:Label ID="lblket" runat="server" Text='<%# Eval("keterangan") %>'  />
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Jumlah Kecil (RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lbljum" runat="server" Text='<%# Eval("jumlah") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>  
             <asp:TemplateField HeaderText="Tax" Visible="false" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# bool.Parse(Eval("tax").ToString()) %>' Enable='<%# !bool.Parse(Eval("tax").ToString()) %>' />
                </ItemTemplate>
            </asp:TemplateField>
           <asp:TemplateField HeaderText="Caj Perkhidmatan (%)" Visible="false" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                 <asp:Label ID="lblgstoth" runat="server" Text='' Visible="false" />
                    <asp:Label ID="ddtaxoth" runat="server" Text='' />
                     <%--<asp:DropDownList ID="ddtaxoth" runat="server" class="form-control uppercase">    </asp:DropDownList>--%>
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Caj Perkhidmatan (RM)" Visible="false" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lblothgst" runat="server" Text='<%# Eval("othgstjumlah") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>  
                
             <asp:TemplateField HeaderText="Jenis Cukai" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                 <asp:Label ID="lblgst" runat="server" Text='<%# Eval("gsttype") %>'  />
                     <asp:Label ID="ddtax" runat="server" Text='<%# Eval("gstname") %>' />
                     <%--<asp:DropDownList ID="ddtax" runat="server" class="form-control uppercase">    </asp:DropDownList>--%>
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Cukai (RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lblgstjum" runat="server" Text='<%# Eval("gstjumlah") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>  
                 <asp:TemplateField HeaderText="Jumlah Termasuk CBP(RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lblover" runat="server" Text='<%# Eval("Overall") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                       
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />   
        </asp:gridview>
                     
                                                 </div>
                                          </div>
                                                                           </div>
                                                                <div class="box-body">&nbsp;</div>
                <div class="row">
                                                                         <div class="col-md-12">
                                                                       <div class="col-md-6 box-body">&nbsp;</div>
                                                                              <div class="col-md-6 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-6 control-label"><asp:Label ID="ps_lbl31" runat="server"></asp:Label></label>                                         
                                                                                <div class="col-sm-6">
                                                                                  <asp:TextBox ID="TextBox17" Placeholder="0.00"  runat="server" Style="text-align:right;" class="form-control"></asp:TextBox> 
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             </div>
                                                                     </div> 
                                                 <div class="box-body">&nbsp;</div>
                                                                   <div class="row">
                                                                                 <div class="col-md-12" style="text-align:center;">
                                                                                    <div class="form-group">
                                                                                        <div class="col-sm-12">
                                                                                           <asp:Button ID="Button2" runat="server" class="btn btn-danger" Text="Simpan" Type="submit"
                                                                                   OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;" onclick="Button2_Click" 
                                                                                    />
                                                                                    <asp:Button ID="kem_Button2" Visible="false" runat="server" class="btn btn-danger" Text="Simpan" Type="submit" onclick="btnkem_Click" />
                                                                                    <asp:Button ID="Button23" runat="server" class="btn btn-warning" onclick="Button22_Click" Text="Cetak" Type="submit" />
                                                                                             <asp:Button ID="Button24" runat="server" class="btn btn-warning" onclick="ENG_Button22_Click" Text="Cetak (E)" Type="submit"/>
                                                                                    <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Tutup" Type="submit"
                                                                                   onclick="Button1_Click" />
                                                                                          
                                                                                        </div>
                                                                                    </div>
                                                                                     </div>
                                </div>
                                                 <div class="row" id="sts" runat="server" visible="false"> 
                                                                    
                                                                        <div class="col-md-3 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                   <asp:Label ID="ps_lbl35" runat="server"></asp:Label> <span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                                <asp:DropDownList ID="ddsts" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" >
                                                                                <asp:ListItem>--- PILIH ---</asp:ListItem>
                                                                                     <asp:ListItem Value="P">PROSES</asp:ListItem>
                                                                                     <asp:ListItem Value="L">LULUS</asp:ListItem>
                                                                                     <asp:ListItem Value="G">GAGAL</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                  
                                                                            </div>
                                                                        </div>
                                                                         <div class="col-md-3 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                  <asp:Label ID="ps_lbl36" runat="server"></asp:Label>
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                                 <asp:TextBox ID="txtApr" runat="server" class="form-control" ></asp:TextBox> 
                                                                                  
                                                                            </div>
                                                                        </div>
                                                                          
                                                                        <div class="col-md-6 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                 <asp:Label ID="ps_lbl37" runat="server"></asp:Label>  :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                            <asp:TextBox ID="TextBox29" runat="server" class="form-control uppercase" TextMode="MultiLine" Height="140px"></asp:TextBox> 
                                                                            </div>
                                                                       
                                                                        </div>
                                                                    </div>
                                                                    <br />     
                                                                       <div class="row" id="Div10" runat="server" visible="false"> 
                                                                     <div class="col-md-3 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                 
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               
                                                                                  
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-2 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                 
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               
                                                                            </div>
                                                                        </div>
                                                                         <div class="col-md-4 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <asp:Button ID="btnkem" runat="server" class="btn btn-danger" Text="Kemaskini" Type="submit"
                                                                                   onclick="btnkem_Click" />
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
                                                                    </div>
                                                                </div>
                                                         </div>
                                                                   </div>
                                                                    </div>
                                                                        </div>
                                                                    </div> 

                                                                      <div role="tabpanel1" class="tab-pane" runat="server" id="p4">
                                                                          <div class="col-md-12 table-responsive uppercase"  style="overflow:auto; padding-top:20px;">
                                                                     <div id="Div12"  runat="server">
                                                                        <%-- <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-3 box-body">
                                <div class="form-group">
                                    <div class="col-sm-12">
                                          <asp:TextBox ID="TextBox5" runat="server" class="form-control" Placeholder="Carian"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-3 box-body">
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <asp:Button ID="Button4" runat="server" class="btn btn-danger" Text="+Tambah" UseSubmitBehavior="false" onclick="tab4tam_Click" />
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                                                          --%>                  
                                                                    <fieldset class="col-md-12">
                                                                     <legend><asp:Label ID="ps_lbl39" runat="server"></asp:Label></legend>
                                                                        <div class="row">
                                                                         <div class="col-md-12">
                                                                        <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl40" runat="server"></asp:Label></label>
                                                                                <div class="col-sm-7">
                                                                                   <asp:TextBox ID="TextBox6" runat="server" class="form-control"></asp:TextBox> 
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl41" runat="server"></asp:Label></label>                        
                                                                                <div class="col-sm-7">
                                                                                     <div class="input-group">
                                                                                   <asp:TextBox ID="TextBox25" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox> 
                                                                                          <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             
                                                                             <div class="col-md-3 box-body">
                                                                            <div class="form-group">
                                                                                <div class="col-sm-12">
                                                                                    <asp:CheckBox ID="chk_invbill" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                   <asp:Button ID="Button6" runat="server" class="btn btn-danger" Text="Carian"  UseSubmitBehavior="false" onclick="buttab3_Click"    />
                                                                                        <asp:Button ID="Button4" runat="server" class="btn btn-primary" Text="+ Tambah" UseSubmitBehavior="false" onclick="tab4tam_Click" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             </div>
                                                                     </div>
                                                                    </fieldset>
                                                                          <div class="box-body">&nbsp;</div>
                                                                  <%-- <div class="row">--%>
                                                                         
<div class="dataTables_wrapper form-inline dt-bootstrap" >
                                      <div class="row" style="overflow:auto;">
           <div class="col-md-12 box-body">
                                                                        <asp:gridview ID="grdBinv" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging_IB"  >
                <Columns>
                    <asp:TemplateField HeaderText="BIL">  
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                     <asp:TemplateField HeaderText="No Permohonan" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                  <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("No_permohonan") %>'  CommandArgument='<%# Eval("No_permohonan")+ "," + Eval("Data") %>' CommandName="Add" OnClick="lblSubItemInv_Click" Font-Bold Font-Underline></asp:LinkButton>
             </ItemTemplate>
            </asp:TemplateField>
                      <asp:TemplateField HeaderText="Jenis Permohonan" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                  <asp:Label ID="Label2"  runat="server" Text='<%# Eval("Data") %>'  CommandArgument='<%# Eval("Data")%>'  ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                     <asp:BoundField DataField="Data" HeaderText="Data" ItemStyle-HorizontalAlign="Center" Visible="false" />
            <asp:BoundField DataField="tarkih_mohon" HeaderText="Tarkih Permohonan" ItemStyle-HorizontalAlign="Center" />
            
            <asp:BoundField DataField="tarkih_invois" HeaderText="Tarkih Invois" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="jumlah" HeaderText="Jumlah (RM)" HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:n}"  ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="Payamt" HeaderText="Paid Amount (RM)" HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:n}"  ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="Baki" HeaderText="BAKI (RM)" HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:n}"  ItemStyle-HorizontalAlign="Right" />

                    <%-- <asp:TemplateField HeaderText="BAKI (RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="Label15" runat="server"  Text='<%# Convert.ToDecimal(Eval("jumlah")) -Convert.ToDecimal(Eval("Payamt")) %>'     ></asp:Label>
                    <asp:Label  runat="server" ID="LblHourRemaining"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>--%>
            </Columns>
           <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                       <%-- <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />--%>
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />
        </asp:gridview>
                                                                 <%--  </div>--%>
               </div>
                                          </div>
    </div>
                                                                </div>
                                                                   <div id="Div13" class="nav-tabs-custom" role="tabpanel1" runat="server">
                                                            <!-- Nav tabs -->
                                                            <ul class="s1 nav nav-tabs" role="tablist1">
                                                            <li id="Li3" runat="server" class="active"><a href="#ContentPlaceHolder1_p65" aria-controls="p65" role="tab" data-toggle="tab"><asp:Label ID="ps_lbl44" runat="server"></asp:Label></a></li>
                                                            </ul>
                                                            <!-- Tab panes -->
                                                            <div class="tab-content">
                                                            <div role="tabpanel1" class="tab-pane active" runat="server" id="p65">
                                                                <div class="col-md-12 table-responsive uppercase"  style="overflow:auto; padding-top:20px;">
                                                                <div class="row">
                                                                         <div class="col-md-12">
                                                                        <div class="col-md-3 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl45" runat="server"></asp:Label><span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                        <asp:DropDownList ID="dddata" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" OnSelectedIndexChanged="dddata_SelectedIndexChanged" AutoPostBack="true">
                                                                                        <asp:ListItem>--- PILIH ---</asp:ListItem>
                                                                                        <asp:ListItem>MOHON BAYAR</asp:ListItem>
                                                                                        <asp:ListItem>BARU</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-3 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl46" runat="server"></asp:Label> <%--<span class="style1">*</span>--%></label>
                                                                                <div class="col-sm-7">
                                                                                    <asp:DropDownList ID="ddnoper" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" OnSelectedIndexChanged="ddnoper_SelectedIndexChanged" AutoPostBack="true">
                                                                                </asp:DropDownList> 

                                                                                <asp:TextBox ID="txtnoperbil" runat="server" class="form-control uppercase" Visible="false"></asp:TextBox> 
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             <div class="col-md-3 box-body"  id="kataka" runat="server">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl47" runat="server"></asp:Label>  <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                        <asp:DropDownList ID="ddkataka" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" OnSelectedIndexChanged="ddkataka_SelectedIndexChanged" AutoPostBack="true">
                                                                                  <asp:ListItem>--- PILIH ---</asp:ListItem>
                                                                                        <asp:ListItem>PEMBEKAL</asp:ListItem>
                                                                                        <asp:ListItem>SEMUA COA</asp:ListItem>
                                                                                </asp:DropDownList> 
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             <div class="col-md-3 box-body" id="kodaka" runat="server">
                                                                            <div class="form-group">
                                                                                  <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl48" runat="server"></asp:Label>  <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                    <asp:DropDownList ID="ddkodaka" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                                </asp:DropDownList> 
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             </div>
                                                                     </div>
                                                                <div  id="invbil" runat="server" >
                                                                    <div class="row">
                                                                         <div class="col-md-12">
                                                                        <div class="col-md-3 box-body" id="tmb1" runat="server" >
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl49" runat="server"></asp:Label><span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                      <div class="input-group">
                                                                                      <asp:TextBox ID="txttmb" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker2 mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox> 
                                                                                          <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                                          </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-3 box-body" id="tmb2" runat="server">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl50" runat="server"></asp:Label> <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                     <div class="input-group">
                                                                                  <asp:TextBox ID="txttinvbil" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker2 mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox> 
                                                                                         <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                                          </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             <div class="col-md-3 box-body"  id="tmb3" runat="server">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl51" runat="server"></asp:Label>  <span class="style1">*</span></label>             
                                                                                <div class="col-sm-7">
                                                                                       <asp:TextBox ID="txrinvbil" runat="server" class="form-control uppercase"></asp:TextBox> 
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             <div class="col-md-3 box-body" id="tmb4" runat="server">
                                                                            <div class="form-group">
                                                                                  <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl52" runat="server"></asp:Label>  <span class="style1">*</span></label>               
                                                                                <div class="col-sm-7">
                                                                                    <asp:TextBox ID="txtinterma" runat="server" class="form-control uppercase"></asp:TextBox> 
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             </div>
                                                                     </div>
                                                                  </div>
                                                                 <div class="box-body">&nbsp;</div>
                                                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;" >
                                      
           <div class="col-md-12 box-body uppercase">                                          
                                            <asp:gridview ID="grdbilinv1" runat="server"  class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="20" ShowFooter="true" GridLines="None">
            <Columns>
           <asp:TemplateField HeaderText="BIL">  
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Tarkih Mohonan" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                  <asp:Label ID="Label1_1"  runat="server" Text='<%# Eval("tarkih_permohonan") %>'  ></asp:Label>
                    <asp:Label ID="lbl_jurn_no" Visible="false" runat="server" Text='<%# Eval("jornal_no") %>'  ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Tarkih Invois" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                  <asp:Label ID="Label2_1"  runat="server" Text='<%# Eval("tarkih_invois") %>'  ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="No Invois" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                  <asp:Label ID="Label3_1"  runat="server" Text='<%# Eval("no_invois") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
               <%-- <asp:TemplateField HeaderText="No Rujukan" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                  <asp:Label ID="Label4_1"  runat="server" Text='<%# Eval("No_rujukan") %>'   ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Keterengan" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                  <asp:Label ID="Label5_1" runat="server" Text='<%# Eval("Keteragan") %>'  ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                  <asp:TemplateField HeaderText="Jumlah (RM)" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="right">
                <ItemTemplate>
                  <asp:Label ID="Label6_1"  runat="server" Text='<%# Eval("Gjumlah","{0:n}") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Gst (RM)" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="right">
                <ItemTemplate>
                  <asp:Label ID="Label7_1"  runat="server" Text='<%# Eval("Gst","{0:n}") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Jumlah Termasuk GST (RM)" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="right">
                <ItemTemplate>
                  <asp:Label ID="Label8_1"  runat="server" Text='<%# Eval("overall","{0:n}") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="PILIH" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                   <asp:RadioButton ID="chkbind" runat="server" onclick="RadioCheck(this);"  />
                </ItemTemplate>
            </asp:TemplateField>
           
            </Columns>
            <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                      
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />
        </asp:gridview>

                                                      <asp:gridview ID="grdbilinv" runat="server"  class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="20" ShowFooter="true" GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting"   OnRowDataBound="grdbilinv_RowDataBound">
            <Columns>
           <asp:TemplateField HeaderText="Kategori Akaun" ItemStyle-Width="20%"  ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>

                     <asp:DropDownList ID="ddkodKat" style="width:100%;" runat="server" class="form-control select2 validate[optional]"  OnSelectedIndexChanged="ddkodKat_SelectedIndexChanged" AutoPostBack="true">   
                     <asp:ListItem>--- PILIH ---</asp:ListItem>
                     <asp:ListItem>PEMBEKAL</asp:ListItem>
                     <asp:ListItem>SEMUA COA</asp:ListItem>
                          </asp:DropDownList>
                </ItemTemplate>
                   <FooterTemplate>
                 <asp:Button ID="ButtonAdd" runat="server" Text="Add New Row" 
                        onclick="ButtonAddMohBil_Click" />
                </FooterTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Credit Kod Akaun" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>

                     <asp:DropDownList ID="ddkodcre" style="width:100%; " runat="server" class="form-control select2 validate[optional]">    </asp:DropDownList>
                </ItemTemplate>
                 
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Debit Kod Akaun" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                     <asp:DropDownList ID="ddkodBil" style="width:100%;" runat="server" class="form-control select2 validate[optional]">    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
               
                <asp:TemplateField HeaderText="Projek" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:DropDownList ID="ddprobil" style="width:100%;" runat="server" class="form-control select2 validate[optional]" >    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Harga/unit" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox55" runat="server"  class="form-control uppercase" OnTextChanged="QtyChangedMohBil_kty" AutoPostBack="true" Width="100%"  placeholder="0.00" style="text-align:right;" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Kuantiti" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox56" runat="server"  OnTextChanged="QtyChangedMohBil" AutoPostBack="true" class="form-control uppercase" Width="100%"  placeholder="0" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
                    <asp:TemplateField HeaderText="Disk" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="Txtdis" runat="server"   placeholder="0.00" style="text-align:right;"   class="form-control uppercase" Width="70px"     OnTextChanged="disChangedMohBil" AutoPostBack="true"   ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Jumlah (RM)" ItemStyle-HorizontalAlign="right">
                <ItemTemplate>
                  <asp:Label ID="Label1" runat="server" Text="0.00" class="form-control uppercase" Width="100%"  ></asp:Label>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
           <FooterTemplate>
                   <asp:Label ID="Label2" class="form-control uppercase" Width="100%" runat="server" Text="0.00"  ></asp:Label>
         </FooterTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Tax" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server"  OnCheckedChanged="ChckedChangedMohBil" AutoPostBack="true"  />
                </ItemTemplate>
            </asp:TemplateField>

                <asp:TemplateField HeaderText="Caj Perkhidmatan (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:DropDownList ID="ddcukaioth" style="width:100%;" runat="server" class="form-control select2 validate[optional]"  onselectedindexchanged="ddcukaiothMohBil_SelectedIndexChanged" AutoPostBack="true"  >    </asp:DropDownList>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label7" class="form-control uppercase" Width="100%"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
                      <asp:TemplateField HeaderText="Caj Perkhidmatan (RM)" ItemStyle-HorizontalAlign="Right"  Visible="false">
                <ItemTemplate>
                    <asp:Label ID="Label10" class="form-control uppercase" Width="100%" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label11" class="form-control uppercase" Width="100%"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="GST (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:DropDownList ID="ddcukaiinv" style="width:100%;" runat="server" class="form-control select2 validate[optional]"  onselectedindexchanged="ddcukaiinvMohBil_SelectedIndexChanged" AutoPostBack="true" >    </asp:DropDownList>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label4" class="form-control uppercase" Width="100%"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
                       <asp:TemplateField HeaderText="AMAUN GST (RM)" ItemStyle-HorizontalAlign="Right" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="Label8" class="form-control uppercase" Width="100%" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label9" class="form-control uppercase" Width="100%"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Jumlah Inclu.GST (RM)" ItemStyle-HorizontalAlign="right">
                <ItemTemplate>
                     <asp:Label ID="Label5" class="form-control uppercase" Width="100%" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                 <asp:Label ID="Label6" class="form-control uppercase" Width="100%" runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                       
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />
        </asp:gridview>

                                                                         <asp:gridview ID="grdbilinvdub" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="7" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="20" ShowFooter="true" GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting"   OnRowDataBound="grdbilinvdub_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="BIL">  
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Kategori Akaun" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                      <asp:Label ID="ddkodKat" runat="server" Text='<%# Eval("kat_akaun") %>' />
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Credit Kod Akaun" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                      <asp:Label ID="lblcre" runat="server" Text='<%# Eval("cre_kod_akaun") %>' Visible = "false" />
                      <asp:Label ID="ddkodcre" runat="server" Text='<%# Eval("cre_name") %>' />
                   <%--  <asp:DropDownList ID="ddkodcre" runat="server" class="form-control uppercase">    </asp:DropDownList>--%>
                </ItemTemplate>
                 
            </asp:TemplateField>

                  <asp:TemplateField HeaderText="Debit Kod Akaun" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                     <asp:Label ID="lbldeb" runat="server" Text='<%# Eval("deb_kod_akaun") %>' Visible = "false" />
                     <asp:Label ID="ddkodBil" runat="server" Text='<%# Eval("deb_name") %>' />
                     <%--<asp:DropDownList ID="ddkodBil" runat="server" class="form-control uppercase">    </asp:DropDownList>--%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="tarkih_mohon" HeaderText="Tarikh Mohon" ItemStyle-HorizontalAlign="Center" />
         <asp:BoundField DataField="tarkih_invois" HeaderText="Tarikh Invois" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="no_invois" HeaderText="No Invois" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="item" HeaderText="No Rujukan" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="keterangan" HeaderText="Keterangan" ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="Projek" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblprodub" runat="server" Text='<%# Eval("project_kod") %>' Visible = "false" />
                     <asp:Label ID="ddprobildub" runat="server" Text='<%# Eval("Ref_Projek_name") %>' />
                    <%--<asp:DropDownList ID="ddprobildub" runat="server" class="form-control"   >    </asp:DropDownList>--%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="unit" HeaderText="Unit" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n}"/>
            <asp:BoundField DataField="quantiti" HeaderText="Quantiti" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="jumlah" HeaderText="Jumlah (RM)" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n}" />
            <asp:BoundField DataField="gstjumlah" HeaderText="GST" ItemStyle-HorizontalAlign="Center" />
             <asp:BoundField DataField="Overall" HeaderText="Jumlah (RM)" ItemStyle-HorizontalAlign="Center" />
            </Columns>
           <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />
        </asp:gridview>

           <asp:gridview ID="Gridview5" runat="server"  class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="20" ShowFooter="true" GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting" OnRowDataBound="Gridview5_RowDataBound" >
         <Columns>
           
             <asp:TemplateField HeaderText="Kod Akaun (Kredit)" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>

                     <asp:DropDownList ID="ddkodbil" style="width:100%;" runat="server" class="form-control select2 validate[optional]">    </asp:DropDownList>
                </ItemTemplate>
                  <FooterTemplate>
                 <asp:Button ID="ButtonAdd" runat="server" Text="Add New Row" 
                        onclick="ButtonAddBil_Click" />
                </FooterTemplate>
            </asp:TemplateField>
             
            <asp:TemplateField HeaderText="Item" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox51" CssClass="form-control uppercase" Width="100px" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="No Po" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox50" CssClass="form-control uppercase" Width="100px"  runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Keterangan" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox52" TextMode="MultiLine" CssClass="form-control uppercase" Width="130px" Height="40px" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Projek" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                     <asp:DropDownList ID="ddkodproj" style="width:100%;" runat="server" class="form-control select2 validate[optional]"> </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Harga/unit" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox53" runat="server" CssClass="form-control"  Width="100%"   placeholder="0.00" style="text-align:right;" OnTextChanged="QtyChangedBil_kty" AutoPostBack="true" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Kuantiti" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox54" runat="server" CssClass="form-control"  Width="100%"    placeholder="0" OnTextChanged="QtyChangedBil" AutoPostBack="true"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Disk" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="Txtdis" runat="server"  CssClass="form-control"  Width="70px"  placeholder="0.00" style="text-align:right;"  OnTextChanged="disChangedBil" AutoPostBack="true"   ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Jumlah (RM)" ItemStyle-HorizontalAlign="right">
                <ItemTemplate>
                  <asp:Label ID="Label1" CssClass="form-control"  Width="100%" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
           <FooterTemplate>
                   <asp:Label ID="Label2" CssClass="form-control"  Width="100%" runat="server" Text="0.00"  ></asp:Label>
         </FooterTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Tax" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Width="100%" OnCheckedChanged="ChckedChangedBil" AutoPostBack="true"  />
                </ItemTemplate>
            </asp:TemplateField>

                <asp:TemplateField HeaderText="Caj Perkhidmatan %" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:DropDownList ID="ddcukaioth" style="width:100%;" runat="server" class="form-control select2 validate[optional]" onselectedindexchanged="ddcukaiothBil_SelectedIndexChanged" AutoPostBack="true"  >    </asp:DropDownList>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label7" CssClass="form-control"  Width="100%"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
                      <asp:TemplateField HeaderText="Caj Perkhidmatan (RM)" ItemStyle-HorizontalAlign="Right"  Visible="false">
                <ItemTemplate>
                    <asp:Label ID="Label10" CssClass="form-control"  Width="100%" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label11" CssClass="form-control"  Width="100%"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="GST %" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:DropDownList ID="ddcukaiinv" style="width:100%;" runat="server" class="form-control select2 validate[optional]"  onselectedindexchanged="ddcukaiinvBil_SelectedIndexChanged" AutoPostBack="true" >    </asp:DropDownList>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label4" CssClass="form-control"  Width="100%"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
                       <asp:TemplateField HeaderText="GST Amt" ItemStyle-HorizontalAlign="Right" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="Label8" CssClass="form-control"  Width="100%" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label9" CssClass="form-control"  Width="100%"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Jumlah Inclu.GST" ItemStyle-HorizontalAlign="right">
                <ItemTemplate>
                     <asp:Label ID="Label5" CssClass="form-control"  Width="100%" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                 <asp:Label ID="Label6" CssClass="form-control"  Width="100%" runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
           
            </Columns>
        <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />                                                       
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />                                                        
           
        </asp:gridview>

                                                                 <asp:gridview ID="grdvie5dup" runat="server"  CssClass="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="7" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="20" ShowFooter="true" GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting" onrowdatabound="grdvie5dup_RowDataBound" >
            <Columns>
            <asp:TemplateField HeaderText="BIL">  
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Kod Akaun" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                 <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("deb_kod_akaun") %>' Visible = "false" />
                    <%-- <asp:DropDownList ID="ddkoddup" runat="server" Width="150px" class="form-control uppercase">    </asp:DropDownList>--%>
                    <asp:Label ID="ddkoddup" runat="server" CssClass="uppercase" Text='<%# Eval("nama_akaun") %>' />
                </ItemTemplate>
               
            </asp:TemplateField>
             <asp:BoundField DataField="item" HeaderText="Item" ItemStyle-HorizontalAlign="Center" />
              <asp:BoundField DataField="keterangan" HeaderText="Keterangan" ItemStyle-HorizontalAlign="Center"  />
                <asp:BoundField DataField="Ref_Projek_name" HeaderText="Projek" ItemStyle-HorizontalAlign="Left"  />
               <asp:BoundField DataField="unit" HeaderText="Harga / unit (RM)" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n}" />
                <asp:BoundField DataField="quantiti" HeaderText="Kuantiti" ItemStyle-HorizontalAlign="Center" />
                  <asp:BoundField DataField="discount" HeaderText="Disk" ItemStyle-HorizontalAlign="Right" />
                 <asp:BoundField DataField="jumlah" HeaderText="Jumlah (RM)" ItemStyle-HorizontalAlign="Right" />
                  
          
             <asp:TemplateField HeaderText="Tax" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="c" runat="server" Checked='<%# bool.Parse(Eval("tax").ToString()) %>' Enable='<%# !bool.Parse(Eval("tax").ToString()) %>' />
                </ItemTemplate>
            </asp:TemplateField>
                
           <asp:TemplateField HeaderText="Caj Perkhidmatan" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                 <asp:Label ID="lblgstoth" runat="server" Text='<%# Eval("othgsttype") %>' Visible = "false" />
                    <%-- <asp:DropDownList ID="ddtaxoth" runat="server" class="form-control">    </asp:DropDownList>--%>
                    <asp:Label ID="ddtaxoth" runat="server" CssClass="uppercase" Text='<%# Eval("oth_name") %>' />
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="gst" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                 <asp:Label ID="lblgst" runat="server" Text='<%# Eval("gsttype") %>' Visible = "false" />
                   <%--  <asp:DropDownList ID="ddtax" runat="server" class="form-control">    </asp:DropDownList>--%>
                    <asp:Label ID="ddtax" runat="server" CssClass="uppercase" Text='<%# Eval("th_name") %>' />
                </ItemTemplate>
            </asp:TemplateField>
                  <asp:BoundField DataField="Overall" HeaderText="Jumlah Termasuk CBP(RM)" ItemStyle-HorizontalAlign="Right" />
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
                                                                   
                                          </div>
                                                                <div class="box-body">&nbsp;</div>
                                                            <div class="row">
                                                                                 <div class="col-md-12">
                                                                                <div class="col-md-8 box-body">&nbsp;</div>
                                                                                     <div class="col-md-4 box-body">
                                                                                    <div class="form-group">
                                                                                        <label for="inputEmail3" class="col-sm-6 control-label"><asp:Label ID="ps_lbl53" runat="server"></asp:Label></label>
                                                                                        <div class="col-sm-6">
                                                                                          <asp:TextBox ID="TextBox37" runat="server" class="form-control" style="text-align:right;"></asp:TextBox> 
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
                                                                                          <asp:Button ID="Button8" runat="server" class="btn btn-danger" Text="Simpan" Type="submit"
                                                                                   OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;" onclick="Button8_Click" 
                                                                                    /> 
                                                                                   
                                                                                    <asp:Button ID="Button11" runat="server" class="btn btn-default" Text="Tutup" UseSubmitBehavior="false"  Type="submit" onclick="Button11_Click"/>
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
                                                                                                                    

                                                            <div role="tabpanel1" class="tab-pane" runat="server" id="p1">
                                                                <div class="col-md-12 table-responsive uppercase"  style="overflow:auto; padding-top:20px;">
                                                                     <div id="Div4"  runat="server">
                                                                        <%-- <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-3 box-body">
                                <div class="form-group">
                                    <div class="col-sm-12">
                                          <asp:TextBox ID="TextBox7" runat="server" class="form-control" Placeholder="Carian"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-3 box-body">
                                <div class="form-group">
                                    <div class="col-sm-12">
                                      <asp:Button ID="tab2tam" runat="server" class="btn btn-danger" Text="+Tambah" 
                                                                    UseSubmitBehavior="false" onclick="tab2tam_Click"
                                                                 />
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>--%>
                                                                            
                                                                    <fieldset class="col-md-12">
                                                                     <legend><asp:Label ID="ps_lbl56" runat="server"></asp:Label></legend>
                                                                         <div class="row">
                                                                         <div class="col-md-12">
                                                                        <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl57" runat="server"></asp:Label></label>
                                                                                <div class="col-sm-7">
                                                                                   <asp:TextBox ID="TextBox8" runat="server" class="form-control"></asp:TextBox> 
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl58" runat="server"></asp:Label></label>
                                                                                <div class="col-sm-7">
                                                                                      <div class="input-group">
                                                                                  <asp:TextBox ID="TextBox10" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox> 
                                                                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             <div class="col-md-3 box-body">
                                                                            <div class="form-group">
                                                                                <div class="col-sm-12">
                                                                                    <asp:CheckBox ID="chk_pvouch" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                     <asp:Button ID="buttab2" runat="server" class="btn btn-danger" Text="Carian" 
                                                                    UseSubmitBehavior="false" onclick="buttab2_Click"    />
                                                                                 <asp:Button ID="tab2tam" runat="server" class="btn btn-primary" Text="+ Tambah" 
                                                                    UseSubmitBehavior="false" onclick="tab2tam_Click"
                                                                 />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             </div>
                                                                     </div>
                                                           
                                                                    </fieldset>
                                                                   <%--<div class="row">--%>
                                                                          <div class="box-body">&nbsp;</div>
                                                                         <div class="dataTables_wrapper form-inline dt-bootstrap" >
                                      <div class="row" style="overflow:auto;">
           <div class="col-md-12 box-body">
                                                                        <asp:gridview ID="grdinvoisview" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging_t3" >
            <Columns>
            <asp:TemplateField HeaderText="BIL">  
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="PV No" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                  <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("Pv_no") %>'  CommandArgument='<%# Eval("Pv_no")%>' CommandName="Add" OnClick ="lblSubItemPV_Click" Font-Bold Font-Underline ></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        
             <asp:BoundField DataField="tarkih_pv" HeaderText="Tarikh Payment" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="No_cek" HeaderText="No Cek" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Akaun_name" HeaderText="Beneficiary" ItemStyle-HorizontalAlign="Center" />
              <asp:BoundField DataField="PaidAmount" HeaderText="Paid Amount (RM)" HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="status" HeaderText="Status" ItemStyle-HorizontalAlign="Center" />
              
             
            </Columns>
     <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                       <%-- <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />--%>
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />
        </asp:gridview>
                                                                  <%-- </div>--%>
               </div>
                                          </div>
                                                                             </div>
                                                                </div>
                                                              
                                                                   <div id="Div5" class="nav-tabs-custom" role="tabpanel1" runat="server">
                                                            <!-- Nav tabs -->
                                                            <ul class="s1 nav nav-tabs" role="tablist1">
                                                            <li id="Li2" runat="server" class="active"><a href="#ContentPlaceHolder1_p62" aria-controls="p62" role="tab" data-toggle="tab"><asp:Label ID="ps_lbl61" runat="server"></asp:Label></a></li>              
                                                            </ul>
                                                            <!-- Tab panes -->
                                                            <div class="tab-content">
                                                            <div role="tabpanel1" class="tab-pane active" runat="server" id="p62">
                                                                <div class="col-md-12 table-responsive uppercase"  style="overflow:auto; padding-top:20px;">
                                                                 <div class="row">
                                                                         <div class="col-md-12">
                                                                        <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl62" runat="server"></asp:Label> <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                    <asp:TextBox ID="txtdata" runat="server" class="form-control" Text="PAYMENT VOUCHER"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>                                                                             
                                                                             </div>
                                                                     </div>
                                                                <div class="row">
                                                                         <div class="col-md-12">
                                                                              <div class="col-md-4 box-body" id="Div11" runat="server">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl63" runat="server"></asp:Label> <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                   <asp:DropDownList ID="ddpvkat" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" OnSelectedIndexChanged="ddpvkat_SelectedIndexChanged" AutoPostBack="true">
                                                                                        <asp:ListItem >--- PILIH ---</asp:ListItem>
                                                                                        <asp:ListItem >PEMBEKAL</asp:ListItem>
                                                                                        <asp:ListItem >SEMUA COA</asp:ListItem>
                                                                                      
                                                                                </asp:DropDownList> 
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             <div class="col-md-4 box-body" id="Div14" runat="server">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl64" runat="server"></asp:Label> <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                      <asp:DropDownList ID="ddpvkod" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" OnSelectedIndexChanged="ddpvkod_SelectedIndexChanged" AutoPostBack="true">
                                                                                </asp:DropDownList> 
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-4 box-body" id="pv_tab3" runat="server" visible="false">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl65" runat="server"></asp:Label><span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                    <asp:TextBox ID="txtpvno" runat="server" class="form-control"></asp:TextBox> 
                                                                                    <asp:TextBox ID="txtpvno_1" runat="server" Visible="false" class="form-control"></asp:TextBox> 
                                                                                    <asp:TextBox ID="ddnoperpv_PV1" runat="server" Visible="false" class="form-control"></asp:TextBox> 
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             </div>
                                                                     </div>
                                                               <div class="row">
                                                                         <div class="col-md-12">
                                                                        <div class="col-md-4 box-body" id="noinv_new" runat="server">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl66" runat="server"></asp:Label><span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                     <asp:DropDownList ID="ddnoperpv" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" OnSelectedIndexChanged="ddnoperpv_SelectedIndexChanged" AutoPostBack="true">
                                                                                </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl67" runat="server"></asp:Label><span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                     <div class="input-group">
                                                                                   <asp:TextBox ID="txttarpv" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker2 mydatepickerclass"
                                                                                        placeholder="DD/MM/YYYY" ></asp:TextBox> 
                                                                                         <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl68" runat="server"></asp:Label><span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                      <asp:TextBox ID="TXTPVNAME" runat="server" class="form-control" ></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             
                                                                             </div>
                                                                     </div>
                                                                  <div class="row">
                                                                         <div class="col-md-12">
                                                                        <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl69" runat="server"></asp:Label> <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                   <asp:DropDownList ID="ddpvcara" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" OnSelectedIndexChanged="ddpvcara_SelectedIndexChanged" AutoPostBack="true"> </asp:DropDownList>                                                                        
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-4 box-body"  id="Div15" runat="server">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl70" runat="server"></asp:Label> <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                      <asp:DropDownList ID="DDbank" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" > </asp:DropDownList> 
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             <div class="col-md-4 box-body" id="Div16" runat="server">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl71" runat="server"></asp:Label> <span class="style1">*</span></label>                           
                                                                                <div class="col-sm-7">
                                                                                      <asp:TextBox ID="txtpvnocek" runat="server" class="form-control uppercase"></asp:TextBox> 
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             
                                                                             </div>
                                                                     </div>
                                                               <div class="row">
                                                                         <div class="col-md-12">
                                                                        <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl72" runat="server"></asp:Label> <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                  <asp:TextBox ID="txtpvterma" runat="server" class="form-control uppercase"></asp:TextBox> 
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl73" runat="server"></asp:Label> <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                      <asp:DropDownList ID="ddpvstatus" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                                       <asp:ListItem Value="">--- PILIH ---</asp:ListItem>
                                                                                     <asp:ListItem Value="B">BARU</asp:ListItem>
                                                                                    
                                                                                </asp:DropDownList>  
                                                                                 <asp:DropDownList ID="ddpvstatus1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" Visible="false">
                                                                                     <asp:ListItem>--- PILIH ---</asp:ListItem>
                                                                                     <asp:ListItem Value="P">PROSES</asp:ListItem>
                                                                                     <asp:ListItem Value="L">LULUS</asp:ListItem>
                                                                                     <asp:ListItem Value="G">GAGAL</asp:ListItem>
                                                                                </asp:DropDownList> 
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                                                                                                         
                                                                             </div>
                                                                     </div>
                                                                        
                                                                   <%--<div class="row">--%>
                                                                 <div class="box-body">&nbsp;</div>
                                                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
           <div class="col-md-12 box-body">
                                                                        <asp:gridview ID="grdpvinv" runat="server"  class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="20" ShowFooter="true" GridLines="None" >
                <Columns>
                  
          <asp:TemplateField HeaderText="BIL">  
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
           <asp:TemplateField HeaderText="Tarkih Permohonan" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label08" runat="server" CssClass="form-control" Width="100%" Text='<%# Eval("tarkih_mohon") %>' ></asp:Label>
                    <asp:Label ID="jurnal_po" runat="server" Visible="false"  Text='<%# Eval("no_po") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                      <asp:TemplateField HeaderText="No Permohonan" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="Label19" runat="server" CssClass="form-control" Width="100%"    Text='<%# Eval("no_permohonan") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                     <asp:TemplateField HeaderText="Tarkih Invois" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label09" runat="server"  CssClass="form-control" Width="100%" Text='<%# Eval("tarkih_invois") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="No Invois" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label10" runat="server" CssClass="form-control" Width="100%"    Text='<%# Eval("no_invois") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
                <asp:TemplateField HeaderText="Jumlah (RM)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label11" runat="server"  CssClass="form-control" Width="100%"  style="text-align:right;"  Text='<%# Eval("jumlah") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                   
                   <asp:TemplateField HeaderText="Gst Jumlah (RM)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label12" runat="server"  CssClass="form-control" Width="100%" style="text-align:right;"  Text='<%# Eval("gstjumlah") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                  <asp:TemplateField HeaderText="Service Charge Jumlah (RM)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label13" runat="server"  CssClass="form-control" Width="100%"  style="text-align:right;" Text='<%# Eval("othgstjumlah") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jumlah Termasuk GST (RM)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label14" runat="server"  CssClass="form-control" Width="100%" style="text-align:right;"  Text='<%# Eval("Overall") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                     <asp:TemplateField HeaderText="Pay AMAUN (RM)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server"   CssClass="form-control" Width="100%" style="text-align:right;"  Text='<%# Eval("Baki","{0:n}") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                     <asp:TemplateField HeaderText="BAKI (RM)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label15" runat="server" CssClass="form-control" Width="100%" style="text-align:right;"  Text='<%#Convert.ToDecimal(Eval("Overall","{0:n}")) -Convert.ToDecimal(Eval("Baki","{0:n}")) %>'     ></asp:Label>
                    <asp:Label  runat="server" ID="LblHourRemaining"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                     
                     <asp:TemplateField HeaderText="Pay AMAUN (RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:TextBox ID="txtpay" runat="server" CssClass="form-control" Width="100%" OnTextChanged="QtyChangedPV" AutoPostBack="true" Style="text-align:right;"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
                     <asp:TemplateField HeaderText="PILIH" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" Width="100%" runat="server"   />
                </ItemTemplate>
            </asp:TemplateField>
            </Columns>
           <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />
        </asp:gridview>

                <%--new grid view Start--%>
                <asp:gridview ID="grdpvinv_new" runat="server"  class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="20" ShowFooter="true" GridLines="None" Visible="false" >
                <Columns>
           <asp:TemplateField HeaderText="Tarkih Permohonan" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="gn_Label08" runat="server" CssClass="form-control datepicker mydatepickerclass" placeholder="DD/MM/YYYY" Width="100%" ></asp:TextBox>                    
                </ItemTemplate>
                 <FooterTemplate>
                 <asp:Button ID="ButtonAdd" runat="server" Text="Add New Row" 
                        onclick="ButtonAdd_Click_pvnew" />
                </FooterTemplate>
            </asp:TemplateField>

                      <asp:TemplateField HeaderText="No Permohonan" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:TextBox ID="gn_Label19" runat="server" CssClass="form-control" Width="100%"    ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
                     <asp:TemplateField HeaderText="Tarkih Invois" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="gn_Label09" runat="server"  CssClass="form-control datepicker mydatepickerclass" placeholder="DD/MM/YYYY" Width="100%" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="No Invois" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="gn_Label10" runat="server" CssClass="form-control" Width="100%"    ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            
                <asp:TemplateField HeaderText="Jumlah (RM)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="gn_Label11" runat="server"  CssClass="form-control" Width="100%"  style="text-align:right;"  ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
                   
                   <asp:TemplateField HeaderText="Gst Jumlah (RM)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="gn_Label12" runat="server"  CssClass="form-control" Width="100%" style="text-align:right;"  ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
                  <asp:TemplateField HeaderText="Service Charge Jumlah (RM)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="gn_Label13" runat="server"  CssClass="form-control" Width="100%"  style="text-align:right;" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jumlah Termasuk GST (RM)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="gn_Label14" runat="server"  CssClass="form-control" Width="100%" style="text-align:right;"  ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
                   
                     <asp:TemplateField HeaderText="BAKI (RM)" ItemStyle-HorizontalAlign="Center" Visible="false">
                <ItemTemplate>
                    <asp:TextBox ID="gn_Label15" runat="server" CssClass="form-control" Width="100%" style="text-align:right;"  ></asp:TextBox>
                    <asp:Label  runat="server" ID="LblHourRemaining"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                     
                     <asp:TemplateField HeaderText="Pay AMAUN (RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <%--<asp:TextBox ID="gn_txtpay" runat="server" CssClass="form-control" Width="100%" OnTextChanged="QtyChangedPV" AutoPostBack="true" Style="text-align:right;"></asp:TextBox>--%>
                    <asp:TextBox ID="gn_txtpay" runat="server" CssClass="form-control" Width="100%" Style="text-align:right;"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
                    
            </Columns>
           <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />
        </asp:gridview>
               <%--new gridview end--%>

                                                                        <asp:gridview ID="Grdpvinvdup" runat="server"  class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="20" ShowFooter="true" GridLines="None">
                <Columns>
                    <asp:TemplateField HeaderText="BIL">  
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
           <asp:BoundField DataField="no_invois" HeaderText="No Invois" ItemStyle-HorizontalAlign="Center" />
           <asp:BoundField DataField="tarikh_invois" HeaderText="Tarikh Invois" ItemStyle-HorizontalAlign="Center" />
           <asp:BoundField DataField="jumlah" HeaderText="Jumlah (RM)" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
           <asp:BoundField DataField="gstjumlah" HeaderText="Gst (RM)" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
           <asp:BoundField DataField="othgstjumlah" HeaderText="Service Charge (RM)" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
           <asp:BoundField DataField="Overall" HeaderText="Jumlah Termasuk GST (RM)" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
           <asp:BoundField DataField="Payamt" HeaderText="Paid Amount (RM)" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
         
            </Columns>
           <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                      
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />  

        </asp:gridview>

                                                                   <%--</div>--%>
                                                                  </div>
                                                                    </div>
                                                                    <div class="box-body">&nbsp;</div>
                                                            <div class="row">
                                                                                 <div class="col-md-12" style="text-align: center">
                                                                                     <div class="col-md-12 box-body">
                                                                                    <div class="form-group">
                                                                                        <div class="col-sm-12">
                                                                                            <asp:Button ID="Button7" runat="server" class="btn btn-danger" Text="Simpan" Type="submit"
                                                                                   OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;" onclick="Button7_Click" 
                                                                                    /> 

                                                                                 <asp:Button ID="Button17" runat="server" class="btn btn-danger" Text="Kemaskini" Type="submit"
                                                                                   OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;" onclick="Button17_Click" 
                                                                                    /> 
                                                                                   <asp:Button ID="Button18" runat="server" Visible="false" class="btn btn-danger" Text="Print" 
                                                                                    Type="submit" onclick="pv_cetak"/>
                                                                                    <asp:Button ID="Button9" runat="server" class="btn btn-default" Text="Tutup" UseSubmitBehavior="false" 
                                                                                    Type="submit" onclick="Button9_Click"/>
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
                                                                          <div class="col-md-12 table-responsive uppercase"  style="overflow:auto; padding-top:20px;">
                                                                    <div id="th1" runat="server">
                                                                   <%-- <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-3 box-body">
                                <div class="form-group">
                                    <div class="col-sm-12">
                                         <asp:TextBox ID="TextBox26" runat="server" class="form-control" Placeholder="Carian"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-3 box-body">
                                <div class="form-group">
                                    <div class="col-sm-12">
                                       <asp:Button ID="tab3tam" runat="server" class="btn btn-danger" Text="+Tambah" Type="submit"
                                                                                  onclick="tab3tam_Click" 
                                                                                    />
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>--%>
                                                                    
                                                                          <fieldset class="col-md-12">
                                                                     <legend><asp:Label ID="ps_lbl78" runat="server"></asp:Label></legend>
                                                                               <div class="row">
                                                                         <div class="col-md-12">
                                                                        <div class="col-md-3 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl79" runat="server"></asp:Label></label>
                                                                                <div class="col-sm-7">
                                                                                      <asp:TextBox ID="txtcno" runat="server" class="form-control validate[optional]"
                                                                                   ></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             <div class="col-md-3 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl80" runat="server"></asp:Label></label>
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
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl81" runat="server"></asp:Label></label>                      
                                                                                <div class="col-sm-7">
                                                                                   <asp:TextBox style="text-align:right;" ID="txtnpembe" runat="server" class="form-control validate[optional,custom[number]] au_amt"  onblur="addTotal_bk1(this)"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             
                                                                             <div class="col-md-3 box-body">
                                                                            <div class="form-group">
                                                                                <div class="col-sm-12">
                                                                                    <asp:CheckBox ID="chk_kredit" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                     <asp:Button ID="Button12" runat="server" class="btn btn-danger" Text="Carian" 
                                                                    UseSubmitBehavior="false" onclick="Button12_Click"    />
                                                                                     <asp:Button ID="tab3tam" runat="server" class="btn btn-primary" Text="+ Tambah" Type="submit"
                                                                                  onclick="tab3tam_Click" 
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
                                                                        <asp:gridview ID="Gridview6" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting" OnPageIndexChanging="gvSelected_PageIndexChanging_t4" >
            <Columns>
            <asp:TemplateField HeaderText="BIL">  
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="C/N No" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                  <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("no_Rujukan") %>'  CommandArgument='<%# Eval("no_Rujukan")%>' CommandName="Add"  onclick="lblSubItemcredit_Click" Font-Bold Font-Underline></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:BoundField DataField="tarikh_invois" HeaderText="Tarikh" ItemStyle-HorizontalAlign="Center" />
              <asp:BoundField DataField="Ref_nama_syarikat" HeaderText="Nama Pembekal" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="jumlah" HeaderText="Jumlah (RM)" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
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
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl84" runat="server"></asp:Label> </label>
                                                                                <div class="col-sm-7">
                                                                                    <asp:DropDownList ID="ddpela2" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" OnSelectedIndexChanged="ddpela2_SelectedIndexChanged" AutoPostBack="true">
                                                                                </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl85" runat="server"></asp:Label></label>
                                                                                <div class="col-sm-7">
                                                                                     <asp:DropDownList ID="ddinv" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" OnSelectedIndexChanged="ddinv_SelectedIndexChanged" AutoPostBack="true">
                                                                                </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl86" runat="server"></asp:Label></label>
                                                                                <div class="col-sm-7">
                                                                                      <div class="input-group">
                                                                                 <asp:TextBox ID="txttcinvois" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
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
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl87" runat="server"></asp:Label> </label>
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
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl88" runat="server"></asp:Label> </label>
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
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl89" runat="server"></asp:Label></label>
                                                                                <div class="col-sm-7">
                                                                                 <asp:DropDownList ID="ddpro1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                                </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                            
                                                                           
                                                                             </div>
                                                                     </div>
                                                                      <div class="row" id="kredit_tab4" runat="server" visible="false">
                                                                         <div class="col-md-12">
                                                                       <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl90" runat="server"></asp:Label> </label>
                                                                                <div class="col-sm-7">
                                                                                      <asp:TextBox ID="txtnoruju" runat="server" class="form-control" ></asp:TextBox>
                                                                                    <asp:TextBox ID="txtnoruju_1" runat="server" Visible="false" class="form-control" ></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             </div>
                                                                     </div>
                                                                       <div class="box-body">&nbsp;</div>
                                                                    <div class="panel" style="width: 100%;">
                                                        <div id="Div6" class="nav-tabs-custom" role="tabpanel2" runat="server">
                                                            <!-- Nav tabs -->
                                                            <ul class="s1 nav nav-tabs" role="tablist">
                                                            <li id="pp11" runat="server" class="active"><a href="#ContentPlaceHolder1_p63" aria-controls="p63" role="tab" data-toggle="tab"><asp:Label ID="ps_lbl91" runat="server"></asp:Label> </a>           
                                                            </li>
                                                            </ul>
                                                            <!-- Tab panes -->
                                                            <div class="tab-content">  
                                                            <div role="tabpanel2" class="tab-pane active" runat="server" id="p63">
                                                                <div class="col-md-12 table-responsive uppercase"  style="overflow:auto;">
                                                                     <div id="Div7"  runat="server">
                                                                       <%--  <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">--%>
          <div class="col-md-12 box-body">
<asp:gridview ID="Gridview9" runat="server"  CssClass="table table-bordered table-hover dataTable uppercase" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="20" ShowFooter="true" GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting" onrowdatabound="Gridview3_RowDataBound"   >
            <Columns>
                  <asp:TemplateField HeaderText="BIL">  
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Kod Akaun" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                 <asp:Label ID="lblCountry" runat="server"  Text='<%# Eval("kod_akauan") %>' Visible = "false" />
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
                <asp:TemplateField HeaderText="Harga/unit" ItemStyle-HorizontalAlign="Right" >
                <ItemTemplate>
                 <asp:Label ID="lblunit" runat="server" Text='<%# Eval("unit","{0:n}") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Kuantiti" ItemStyle-HorizontalAlign="Center">
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
                 <asp:TemplateField HeaderText="Jumlah Termasuk CBP(RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lblover" runat="server" Text='<%# Eval("Overall","{0:n}") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>
               
               

            </Columns>
            <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />                                                                 
           
        </asp:gridview>

  <asp:gridview ID="Gridview10" runat="server"  class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="20" ShowFooter="true" GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting" onrowdatabound="Gridview9_RowDataBound" >
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
                    <asp:TextBox ID="txtket" class="form-control uppercase" Width="100%" runat="server" TextMode="MultiLine" Height="40px"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
                     
                       <asp:TemplateField HeaderText="Jumlah tidak termasuk CBP(RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                     <asp:TextBox ID="Txtdis" runat="server"  class="form-control uppercase" Width="100%"  placeholder="0.00"   OnTextChanged="QtyChanged1" AutoPostBack="true"   style="text-align:right;"    ></asp:TextBox>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label3" class="form-control uppercase" Width="100%"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Tax" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="Chkcre" runat="server" Width="100%"  OnCheckedChanged="ChckedChangedcre"  AutoPostBack="true"  />
                </ItemTemplate>
            </asp:TemplateField>
                       <asp:TemplateField HeaderText="Caj Perkhidmatan (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:DropDownList ID="ddkodgstoth" style="width:100%;" runat="server" class="form-control select2 validate[optional]"  onselectedindexchanged="ddkodgstoth_SelectedIndexChanged" AutoPostBack="true"  >    </asp:DropDownList>
                </ItemTemplate>
                
            </asp:TemplateField>
                      <asp:TemplateField HeaderText="Caj Perkhidmatan (RM)" ItemStyle-HorizontalAlign="Center" >
                <ItemTemplate>
                    <asp:Label ID="Label10" class="form-control uppercase" style="text-align:right;" Width="100%" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label11" class="form-control uppercase" style="text-align:right;" Width="100%"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="GST (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:DropDownList ID="ddkodgst" style="width:100%;" runat="server" class="form-control select2 validate[optional]"  onselectedindexchanged="ddkodgst_SelectedIndexChanged" AutoPostBack="true" >    </asp:DropDownList>
                </ItemTemplate>
               
            </asp:TemplateField>
                       <asp:TemplateField HeaderText="GST Amaun (RM)" ItemStyle-HorizontalAlign="Right" >
                <ItemTemplate>
                    <asp:Label ID="Label8" class="form-control uppercase" style="text-align:right;" Width="100%" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label9" class="form-control uppercase" Width="100%"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
                      <asp:TemplateField HeaderText="Jumlah Termasuk CBP(RM)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label5" class="form-control uppercase" Width="100%" style="text-align:right;" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label6" class="form-control uppercase" Width="100%"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            
            </Columns>
          <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />                                                                  
        </asp:gridview>

                                                                                              
<asp:gridview ID="Gridview12" runat="server"  class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="20" ShowFooter="true" GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting" onrowdatabound="Gridview3_RowDataBound" >
            <Columns>
                 <asp:TemplateField HeaderText="BIL">  
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
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
                 <asp:Label ID="lblgstoth" runat="server" Text='<%# Eval("othgsttype") %>' Visible="false" />
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
                 <asp:TemplateField HeaderText="Jumlah Termasuk CBP(RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lblover" runat="server" Text='<%# Eval("Overall") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                       
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />   
        </asp:gridview>
      
                                 <%--   </div>--%>
                                                                          <div class="box-body">&nbsp;</div>
                                                            <div class="row">
                                                                                 <div class="col-md-12">
                                                                                <div class="col-md-8 box-body">&nbsp;</div>
                                                                                     <div class="col-md-4 box-body">
                                                                                    <div class="form-group">
                                                                                        <label for="inputEmail3" class="col-sm-6 control-label"><asp:Label ID="ps_lbl92" runat="server"></asp:Label></label>
                                                                                        <div class="col-sm-6">
                                                                                          <asp:TextBox ID="TextBox18" runat="server" class="form-control" style="text-align:right;"></asp:TextBox> 
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                     </div>
                                </div>
               <div class="box-body">&nbsp;</div>
                                                                          <div class="row">
                                                                                 <div class="col-md-12" style="text-align: center">
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
                                </div>                                    </div>                              
                                                                </div>                                                             
                                                                </div>
                                                               </div>
                                                                   </div>
                                                                </div>                                                            
                                                        </div>
                                                                       </div>
                                                                   </div>
                                                                   </div>
                                                                    <div role="tabpanel3" class="tab-pane" runat="server" id="p3">
                                                                        <div class="col-md-12 table-responsive uppercase"  style="overflow:auto; padding-top:20px;">
                                                                    <div id="fr1" runat="server">
                                                                       <%-- <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-3 box-body">
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <asp:TextBox ID="TextBox11" runat="server" class="form-control" Placeholder="Carian"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-3 box-body">
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <asp:Button ID="Button10" runat="server" class="btn btn-danger" Text="+Tambah" Type="submit"
                                                                                  onclick="Button10_Click" 
                                                                                    />
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>--%>
                                                                   
                                                                     <fieldset class="col-md-12">
                                                                     <legend><asp:Label ID="ps_lbl96" runat="server"></asp:Label></legend>
                                                                  <div class="row">
                                                                         <div class="col-md-12">
                                                                        <div class="col-md-3 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl97" runat="server"></asp:Label></label>
                                                                                <div class="col-sm-7">
                                                                                    <asp:TextBox ID="TextBox12" runat="server" class="form-control validate[optional]"
                                                                                   ></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                               <div class="col-md-3 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl98" runat="server"></asp:Label></label>
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
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl99" runat="server"></asp:Label></label>
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
                                                                                <asp:Button ID="Button10" runat="server" class="btn btn-primary" Text="+ Tambah" Type="submit"                          
                                                                                  onclick="Button10_Click" 
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
                                                                        <asp:gridview ID="Gridview8" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting" OnPageIndexChanging="gvSelected_PageIndexChanging_t5">
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
              <asp:BoundField DataField="Ref_nama_syarikat" HeaderText="Nama Pembekal" ItemStyle-HorizontalAlign="Center" />
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
                                                                     <div id="fr2" runat="server">
                                                                         <div class="row">
                                                                         <div class="col-md-12">
                                                                        <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl102" runat="server"></asp:Label> </label>
                                                                                <div class="col-sm-7">
                                                                                 <asp:DropDownList ID="ddpela3" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" OnSelectedIndexChanged="ddpela3_SelectedIndexChanged" AutoPostBack="true">
                                                                                </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl103" runat="server"></asp:Label></label>
                                                                                <div class="col-sm-7">
                                                                                        <asp:DropDownList ID="ddinv2" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" OnSelectedIndexChanged="ddinv2_SelectedIndexChanged" AutoPostBack="true">
                                                                                </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl104" runat="server"></asp:Label></label>
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
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl105" runat="server"></asp:Label> </label>
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
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl106" runat="server"></asp:Label> </label>
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
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl107" runat="server"></asp:Label></label>
                                                                                <div class="col-sm-7">
                                                                                 <asp:DropDownList ID="ddpro2" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                                </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                            
                                                                           
                                                                             </div>
                                                                     </div>
                                                                      <div class="row" id="debit_tab5" runat="server" visible="false">
                                                                         <div class="col-md-12">
                                                                              <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl108" runat="server"></asp:Label> </label>
                                                                                <div class="col-sm-7">
                                                                                      <asp:TextBox ID="txtnoruj2" runat="server" class="form-control "
                                                                                      ></asp:TextBox>
                                                                                     <asp:TextBox ID="txtnoruj2_2" runat="server" Visible="false" class="form-control "
                                                                                      ></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                       
                                                                             </div>
                                                                     </div>
                                                                         <div class="box-body">&nbsp;</div>                                                                 
                                                           <div class="panel" style="width: 100%;">
                                                        <div id="Div8" class="nav-tabs-custom" role="tabpanel3" runat="server">
                                                            <!-- Nav tabs -->
                                                            <ul class="s1 nav nav-tabs" role="tablist">
                                                            <li id="pp14" runat="server" class="active"><a href="#ContentPlaceHolder1_p67" aria-controls="p67" role="tab" data-toggle="tab"><asp:Label ID="ps_lbl109" runat="server"></asp:Label> </a>
                                                               
                                                            </li>
                                                               <%-- <li id="pp15" runat="server"><a href="#ContentPlaceHolder1_p68" aria-controls="p68" role="tab" data-toggle="tab">Knock Off</a></li>--%>
                                                                <%--<li id="pp2" runat="server"><a href="#ContentPlaceHolder1_p2" aria-controls="p2" role="tab" data-toggle="tab">ELAUN TETAP</a></li>
                                                                <li id="pp3" runat="server"><a href="#ContentPlaceHolder1_p3" aria-controls="p3" role="tab" data-toggle="tab">LAIN-LAIN ELAUN</a></li>
                                                                <li id="pp4" runat="server"><a href="#ContentPlaceHolder1_p4" aria-controls="p4" role="tab" data-toggle="tab">KERJA LEBIH MASA</a></li>
                                                                <li id="pp5" runat="server"><a href="#ContentPlaceHolder1_p5" aria-controls="p5" role="tab" data-toggle="tab">BONUS</a></li>--%>
                                                                
                                                            </ul>
                                                            <!-- Tab panes -->
                                                            <div class="tab-content">
                                                            <div role="tabpanel3" class="tab-pane active" runat="server" id="p67">
                                                                <div class="col-md-12 table-responsive uppercase"  style="overflow:auto;">
                                                                     <div id="Div9"  runat="server">
                                                                              <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
           <div class="col-md-12 box-body">
                               
<asp:gridview ID="Gridview7" runat="server"  class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="20" ShowFooter="true" GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting" onrowdatabound="Gridview3_RowDataBound" >
            <Columns>
                  <asp:TemplateField HeaderText="BIL">  
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
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
                <asp:TemplateField HeaderText="Harga/unit" ItemStyle-HorizontalAlign="Right" >
                <ItemTemplate>
                 <asp:Label ID="lblunit" runat="server" Text='<%# Eval("unit","{0:n}") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Kuantiti" ItemStyle-HorizontalAlign="Center">
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
                 <asp:TemplateField HeaderText="Jumlah Termasuk CBP(RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lblover" runat="server" Text='<%# Eval("Overall","{0:n}") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>
               
                
            </Columns>
           <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                      
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
                    <asp:TextBox ID="txtket"  runat="server" TextMode="MultiLine" class="form-control uppercase" Width="100%" Height="40px"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
                     
                       <asp:TemplateField HeaderText="Jumlah tidak termasuk CBP(RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                     <asp:TextBox ID="Txtdis" runat="server"  class="form-control uppercase" Width="100%"  placeholder="0.00"   OnTextChanged="QtyChangeddeb" AutoPostBack="true"   style="text-align:right;"    ></asp:TextBox>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label3" class="form-control uppercase" Width="100%"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Tax" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="Chkdeb" runat="server"  OnCheckedChanged="ChckedChangeddeb"  AutoPostBack="true"  />
                </ItemTemplate>
            </asp:TemplateField>
                       <asp:TemplateField HeaderText="Caj Perkhidmatan (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:DropDownList ID="ddgstdeboth" style="width:100%;" runat="server" class="form-control select2 validate[optional]"  onselectedindexchanged="ddgstdeboth_SelectedIndexChanged" AutoPostBack="true"  >    </asp:DropDownList>
                </ItemTemplate>
                
            </asp:TemplateField>
                      <asp:TemplateField HeaderText="Caj Perkhidmatan (RM)" ItemStyle-HorizontalAlign="Right" >
                <ItemTemplate>
                    <asp:Label ID="Label10" class="form-control uppercase" Width="100%" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label11" class="form-control uppercase" Width="100%"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="GST (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:DropDownList ID="ddgstdeb" style="width:100%;" runat="server" class="form-control select2 validate[optional]"  onselectedindexchanged="ddgstdeb_SelectedIndexChanged" AutoPostBack="true" >    </asp:DropDownList>
                </ItemTemplate>
               
            </asp:TemplateField>
                       <asp:TemplateField HeaderText="GST Amt" ItemStyle-HorizontalAlign="Right" >
                <ItemTemplate>
                    <asp:Label ID="Label8" class="form-control uppercase" Width="100%" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label9" class="form-control uppercase" Width="100%"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
                      <asp:TemplateField HeaderText="Jumlah Termasuk CBP(RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="Label5" class="form-control uppercase" Width="100%" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label6" class="form-control uppercase" Width="100%"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            
            </Columns>
           <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />  
                                                              
        </asp:gridview>
            
  <asp:gridview ID="Gridview14" runat="server" CssClass="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="20" ShowFooter="true" GridLines="None" onrowdatabound="Gridview14_RowDataBound" >
         <Columns>
               <asp:TemplateField HeaderText="BIL">  
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Kod Akaun" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                 <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("kod_akauan") %>' Visible = "false" />
                    <asp:Label ID="ddkoddup" runat="server" Text='<%# Eval("nama_akaun") %>' />
                     <%--<asp:DropDownList ID="ddkoddup" runat="server" Width="150px" class="form-control uppercase">    </asp:DropDownList>--%>
                </ItemTemplate>
            </asp:TemplateField>
               
                 <asp:TemplateField HeaderText="Keterangan" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                 <asp:Label ID="lblket" runat="server" Text='<%# Eval("keterangan") %>' CssClass="uppercase"  />
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
                    <asp:Label ID="ddtaxoth" runat="server" Text='<%# Eval("othgstname") %>' CssClass="uppercase" />
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
                     <asp:Label ID="ddtax" runat="server" Text='<%# Eval("gstname") %>' CssClass="uppercase" />
                     <%--<asp:DropDownList ID="ddtax" runat="server" class="form-control uppercase">    </asp:DropDownList>--%>
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Gst (RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lblgstjum" runat="server" Text='<%# Eval("gstjum","{0:n}") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>  
                 <asp:TemplateField HeaderText="Jumlah Termasuk CBP(RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lblover" runat="server" Text='<%# Eval("Overall","{0:n}") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>
              

            </Columns>
            <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                      
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />  
                                                             
        </asp:gridview>
                                                 </div> 
                                                   </div>
          
                                                   <div class="box-body">&nbsp;</div>
                                                            <div class="row">
                                                                                 <div class="col-md-12">
                                                                                <div class="col-md-8 box-body">&nbsp;</div>
                                                                                     <div class="col-md-4 box-body">
                                                                                    <div class="form-group">
                                                                                        <label for="inputEmail3" class="col-sm-6 control-label"><asp:Label ID="ps_lbl110" runat="server"></asp:Label></label>
                                                                                        <div class="col-sm-6">
                                                                                          <asp:TextBox ID="TextBox1" runat="server" class="form-control" style="text-align:right;"></asp:TextBox> 
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                     </div>
                                </div>
                                                                  <div class="box-body">&nbsp;</div>
                                                                         <div class="row">
                                                                                 <div class="col-md-12" style="text-align: center">
                                                                                     <div class="col-md-12 box-body">
                                                                                    <div class="form-group">
                                                                                        <div class="col-sm-12">
                                                                                         <asp:Button ID="Button5" runat="server" class="btn btn-danger" Text="Simpan" Type="submit"               
                                                                                    OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;" 
                                                                                    onclick="simtab4_Click" />
                                                                                <asp:Button ID="Button14" runat="server" Text="Tutup" class="btn btn-default" UseSubmitBehavior="false"
                                                                                   onclick="Button14_Click"  />
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
                                                             
                                                               
                                                        </div>
                                                                            </div>
                                                                     </div>
                                                                   </div>
                                                                </div>

                                                               </div>
                                                               
                                                        </div>
                           <cc1:ModalPopupExtender BackgroundCssClass="modalBg" DropShadow="true" ID="ModalPopupExtender1"
                                                                PopupControlID="Panel3" runat="server" TargetControlID="btnBack" PopupDragHandleControlID="Panel2"
                                                                CancelControlID="btnBack">
                                                            </cc1:ModalPopupExtender>
                                      <asp:Panel ID="Panel3" runat="server" CssClass="modalPanel" Style="display: none;  overflow-y:auto; height: 60vh;">
                                          <a class="popupCloseButton" id="btnBack" runat="server"></a>
                                            <div class="box-header with-border">
                            <h3 class="box-title"><asp:Label ID="Label16" runat="server"></asp:Label></h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div id="div_shw1" runat="server">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">Nombor Pendaftaran Syarikat <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="pel_TextBox5" runat="server" class="form-control validate[optional] uppercase" OnTextChanged="pel_katcd_TextChanged" AutoPostBack="True" MaxLength="15"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">Nama Syarikat <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="pel_TextBox1" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">Alamat Pertama </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="pel_TextBox7" runat="server" class="form-control validate[optional] uppercase" MaxLength="1000"></asp:TextBox>                                                        

                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">Nombor Telefon </label>
                                    <div class="col-sm-8">
                                           <asp:TextBox ID="pel_TextBox4" runat="server" class="form-control validate[optional,custom[phone]] uppercase" MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">Alamat Kedua</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="pel_TextBox11" runat="server" class="form-control validate[optional] uppercase" MaxLength="1000"></asp:TextBox>                                                        
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">No Fax</label>
                                    <div class="col-sm-8">
                                    <asp:TextBox ID="pel_TextBox2" runat="server" class="form-control validate[optional,custom[phone]] uppercase" MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">Bandar </label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="pel_TextBox9" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>                                                        
                                    </div>
                                </div>
                            </div>
                           
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">Gst Id</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="pel_TextBox6" runat="server" class="form-control validate[optional] uppercase" MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">Negeri</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="pel_ddnegeri" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" >
                                                                                </asp:DropDownList>  
                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">Nama Pegawai</label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="pel_TextBox8" runat="server" class="form-control validate[optional] uppercase" MaxLength="30"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">Poskod</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="pel_TextBox14" runat="server" class="form-control validate[optional,custom[number]] uppercase" MaxLength="5"></asp:TextBox>                                                        
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">Kod Industri</label>
                                    <div class="col-sm-8 form-droupdown-index">
                                        <asp:DropDownList ID="pel_dd_kodind" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                <%--  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">Jenis Akaun <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                      <asp:DropDownList ID="dd_akaun" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" OnSelectedIndexChanged="sel_jenis" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>--%>
                                 </div>
                         </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">Negera </label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="pel_dd_negera" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                                </asp:DropDownList> 
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">Alamat Email</label>
                                    <div class="col-sm-8">
                                            <div class="input-group">
                                   <asp:TextBox ID="pel_TextBox3" runat="server" class="form-control validate[optional,custom[email]]"></asp:TextBox>
                                                <span class="input-group-addon" > <i class="fa fa-envelope"></i></span>
                                                </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="row">
                             <div class="col-md-12">
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">Nama Bank</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="pel_DropDownList1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">Bank Akaun No</label>
                                    <div class="col-sm-8">
                                   <asp:TextBox ID="pel_TextBox12" runat="server" class="form-control validate[optional]" MaxLength="20"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                                <%--  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">Kod Akaun</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox30" runat="server" class="form-control validate[optional] uppercase" MaxLength="20"></asp:TextBox>
                                    </div>
                                </div>
                            </div>--%>
                                 </div>
                         </div>
                          
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">

                               <asp:TextBox ID="pel_lbl_name" runat="server" class="form-control validate[optional] uppercase" Visible="false"></asp:TextBox>
                                 <asp:TextBox ID="pel_ver_id" Visible="false" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                                <asp:TextBox ID="pel_get_id" Visible="false" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                                 <asp:Button ID="pel_Button4" runat="server" class="btn btn-danger" Text="Simpan" OnClick="pel_clk_submit" UseSubmitBehavior="false" />
                                <asp:Button ID="pel_Button1" runat="server" class="btn btn-default" Text="Set Semula" UseSubmitBehavior="false" OnClick="pel_Button5_Click" />
                                <%--<asp:Button ID="btnBack" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" OnClick="pel_Click_bck" />--%>
                                <asp:Button ID="Button20" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" OnClick="pel_Click_bck" />
                                 
                            </div>
                           </div>
                               </div>
</div>
                                       
                            <div class="box-body">&nbsp;
                                    </div>
                        </div>        
                                    </asp:Panel>
 
                            <div class="box-body">&nbsp;
                                    </div>
                        </div>

                    </div>
                </div>
            </div>
            <!-- /.row -->
 

          </ContentTemplate>
              <Triggers>
           <asp:PostBackTrigger ControlID="Button23"  />
               <asp:PostBackTrigger ControlID="Button24"  />
                  </Triggers>
    </asp:UpdatePanel>
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
  
</asp:Content>

