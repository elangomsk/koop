<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Kewengan/kw_Kelulusan_Jurnal.aspx.cs" Inherits="kw_Kelulusan_Jurnal" %>

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

             $('.select2').select2();

         });



     </script>

     
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



  

    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server" ScriptMode="Release">

    </asp:ScriptManager>



    

     <!-- Content Wrapper. Contains page content -->

                <div class="content-wrapper" >

                    <!-- Content Header (Page header) -->

                    <section class="content-header">

                        <h1><asp:Label ID="ps_lbl1" Visible="false" runat="server"></asp:Label>Kelulusan Jurnal</h1>

                        <ol class="breadcrumb">

                            <li><a href="#"><i class="fa fa-dashboard"></i><asp:Label ID="ps_lbl2" Visible="false" runat="server"></asp:Label></a> Kewangan</li>

                            <li class="active"><asp:Label ID="ps_lbl3" Visible="false" runat="server"></asp:Label>Kelulusan Jurnal</li>

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

                            <h3 class="box-title"><asp:Label ID="ps_lbl4" Visible="false" runat="server"></asp:Label> <asp:Label Visible="false" ID="hd_txt" runat="server"></asp:Label>Kelulusan Jurnal</h3>

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
                                                            </ul>

                                                            <!-- Tab panes -->

                                                            <div class="tab-content">

                                                            <div role="tabpanel" class="tab-pane active" runat="server" id="p6">

                                                                    <div class="col-md-12 table-responsive"  style="overflow:auto;">

                                                                     <div id="Div3"  runat="server">                                                                            

                                                                    <fieldset class="col-md-12">

                                                                     <legend style="width:4%"><asp:Label ID="ps_lbl11" runat="server"></asp:Label></legend>

                                                                        <div class="row">

                                                                         <div class="col-md-12">

                                                                       <div class="col-md-3 box-body">

                                                                            <div class="form-group">

                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="Label20" Visible="false" runat="server"></asp:Label>From</label>

                                                                                <div class="col-sm-7">

                                                                                      <div class="input-group">

                                                                                  <asp:TextBox ID="TextBox15" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox> 

                                                                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>

                                             </div>

                                                                                </div>

                                                                            </div>

                                                                        </div>

                                                                               <div class="col-md-3 box-body">

                                                                            <div class="form-group">

                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl13" Visible="false" runat="server"></asp:Label> To</label>

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

                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="ps_lbl14" Visible="false" runat="server"></asp:Label>Jenis Transaksi</label>

                                                                                <div class="col-sm-7">

                                                                                   <asp:DropDownList ID="jenis_trxn" class="form-control select2 uppercase" runat="server">

                                                                                   </asp:DropDownList>

                                                                                </div>

                                                                            </div>

                                                                        </div>

                                                                             <div class="col-md-3 box-body">

                                                                            <div class="form-group">

                                                                                <label for="inputEmail3" class="col-sm-5 control-label"><asp:Label ID="Label21" Visible="false" runat="server"></asp:Label>Status Kelulusan</label>

                                                                                <div class="col-sm-7">

                                                                                   <asp:DropDownList ID="DropDownList4" class="form-control select2 uppercase" runat="server">

                                                                                       <asp:ListItem Value="">PENDING</asp:ListItem>

                                                                                       <asp:ListItem Value="L">LULUS</asp:ListItem>

                                                                                       <asp:ListItem Value="T">TIDAK LULUS</asp:ListItem>

                                                                                   </asp:DropDownList>

                                                                                </div>

                                                                            </div>

                                                                        </div>

                                                                           

                                                                             </div>

                                                                     </div>

                                                                          <div class="row">

                                                                         <div class="col-md-12">

                                                                               <%--<div class="col-md-3 box-body">--%>

                                                                            <div class="form-group">

                                                                                <div class="col-sm-12 text-center">

                                                                                      <asp:CheckBox ID="mb_chk" Visible="false" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                                                                                   <asp:Button ID="but" runat="server" class="btn btn-danger" Text="Carian" 

                                                                    UseSubmitBehavior="false" onclick="but_Click"    />

                                                                                      <asp:Button ID="Button20" runat="server" class="btn btn-default" Text="Set Semula" 

                                                                    UseSubmitBehavior="false"  OnClick="grd2_reset"  />

                                                                                     <asp:Button ID="Button3" Visible="false" runat="server" class="btn btn-primary" Text=" Tambah" 

                                                                    UseSubmitBehavior="false" onclick="Button3_Click"

                                                                 />

                                                                                </div>

                                                                            </div>

                                                                        <%--</div>--%>

                                                                             </div>

                                                                              </div>

                                                                    </fieldset>

                                                                          <div class="box-body">&nbsp;</div>

                                                                         <div class="dataTables_wrapper form-inline dt-bootstrap" >

                                      <div class="row" style="overflow:auto;">

           <div class="col-md-12 box-body">

                                                                        <asp:gridview ID="Gridview2" runat="server"  class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" onrowdatabound="GridView1_RowDataBound1" OnPageIndexChanging="gvSelected_PageIndexChanging_g2" >

                                                                             <PagerStyle CssClass="pager" />

            <Columns>

                <asp:TemplateField HeaderText="BIL">  

                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>

                                            <ItemTemplate>  

                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 

                                            </ItemTemplate>  

                                                </asp:TemplateField>

                 <asp:TemplateField HeaderText="No Rujukan" ItemStyle-HorizontalAlign="Left">

                <ItemTemplate>

                  <asp:Label ID="rujukan" runat="server" Text='<%# Eval("rujukan") %>'></asp:Label>

                </ItemTemplate>

            </asp:TemplateField>
                    <asp:TemplateField HeaderText="No Invois / No Dokumen" ItemStyle-HorizontalAlign="Left">

                <ItemTemplate>

                  <asp:Label ID="noinvois" runat="server" Text='<%# Eval("noinvois") %>'></asp:Label>

                </ItemTemplate>

            </asp:TemplateField>

                <asp:BoundField DataField="tk_mohon" HeaderText="Tarkih Mohon" ItemStyle-HorizontalAlign="Center" />  

                   <asp:TemplateField HeaderText="Jenis Transaksi" ItemStyle-HorizontalAlign="Left">  

                    <ItemStyle HorizontalAlign="Left" />

                                            <ItemTemplate>  

                                                <asp:Label ID="jen_txi" runat="server" Text='<%# Eval("jen_txi") %>'></asp:Label>

                                            </ItemTemplate>  

                                                </asp:TemplateField>        

                  <asp:TemplateField HeaderText="Bayar Kepada" ItemStyle-HorizontalAlign="Left">  

                    <ItemStyle HorizontalAlign="Left" />

                                            <ItemTemplate>  

                                                <asp:Label ID="Bayar_kepada" runat="server" Text='<%# Eval("Bayar_kepada") %>'></asp:Label>

                                            </ItemTemplate>  

                                                </asp:TemplateField>

                

                <asp:BoundField DataField="keterengan" HeaderText="Keterangan" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

      <%--      <asp:BoundField DataField="nama" HeaderText="Nama" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />--%>

                <asp:BoundField DataField="jumlah" HeaderStyle-HorizontalAlign="Right" HeaderText="Amaun (RM)" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" />

                <%-- <asp:BoundField DataField="status" HeaderText="Status" ItemStyle-HorizontalAlign="Center" />--%>

                <asp:TemplateField HeaderText="Status Semakan" Visible="false" ItemStyle-HorizontalAlign="Center">  

                    <ItemStyle HorizontalAlign="Center" Font-Bold />

                                            <ItemTemplate>  

                                                <asp:Label ID="status" Width="100%" runat="server" Text='<%# Eval("status") %>'></asp:Label>

                                            </ItemTemplate>  

                                                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Status GL" ItemStyle-HorizontalAlign="Center">  

                    <ItemStyle HorizontalAlign="Center" Font-Bold />

                                            <ItemTemplate>  

                                                <asp:Label ID="kel_status" Width="100%" runat="server" Text='<%# Eval("glsts") %>'></asp:Label>

                                            </ItemTemplate>  

                                                </asp:TemplateField>

 <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" Visible="false">  

                    <ItemStyle HorizontalAlign="Center" />

                                            <ItemTemplate>  

<asp:Label ID="lbl_mohon_no" runat="server" Text='<%# Eval("rujukan") %>' Visible="false"></asp:Label>

                                                <asp:Label ID="Label17" runat="server" Text='<%# Eval("kat") %>' Visible="false"></asp:Label>

                                                <asp:Label ID="Label18" runat="server" Text='<%# Eval("Bayar_kepada") %>' Visible="false"></asp:Label>

                                                <asp:LinkButton ID="LinkButton2" runat="server" ToolTip="Delete" CommandArgument='Cancel' OnClick="lnkView_Click"  OnClientClick="if (!confirm('Are you sure to Delete?')) return false;" Font-Bold>

                                                                                                    Hapus

                                                                                                </asp:LinkButton> 

                                            </ItemTemplate>  

                                                </asp:TemplateField>

                <asp:TemplateField HeaderText="Action">

                                                                <ItemStyle HorizontalAlign="center" Width="5%"></ItemStyle>

                                                                    <ItemTemplate>

                                                                       <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("rujukan")+","+ Eval("jen_txi")%>' CommandName="Add" ToolTip='<%# Eval("rujukan").ToString().ToUpper() %>'  onclick="lblSubbind_Click"><i class='fa fa-edit'></i></asp:LinkButton>                                                                    

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

                                                               

                                                                   <div id="Div2" visible="false" class="nav-tabs-custom" role="tabpanel1" runat="server">

                                                            <!-- Nav tabs -->

                                                          
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

                                                                      <%--   <div class="box-header with-border">

                            <h3 class="box-title">Butiran Pemohon</h3>

                        </div>

                                                                        --%>

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

                                                                       <legend style="width:10%">Butiran Pemohon</legend>

                                                                      

                                                                       <div class="row">

                                                                         <div class="col-md-12">

                                                                             <div class="col-md-6 box-body" id="mb_tab1" runat="server">

                                                                            <div class="form-group">

                                                                                <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl19" Visible="false" runat="server"></asp:Label> Jenis Permohonan</label>

                                                                                <div class="col-sm-8">

                                                                                 <asp:TextBox ID="txtnoper" runat="server" ReadOnly="true" class="form-control uppercase"></asp:TextBox> 

                                                                                     <asp:TextBox ID="TextBox31_jcd" runat="server" Visible="false" class="form-control uppercase"></asp:TextBox> 

                                                                                    <asp:TextBox ID="txtnoper_1" runat="server"  Visible="false" class="form-control uppercase"></asp:TextBox> 

                                                                                </div>

                                                                            </div>

                                                                        </div>

                                                                            

                                                                              <div class="col-md-6 box-body" id="Div17" runat="server">

                                                                            <div class="form-group">

                                                                                <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="Label24" Visible="false" runat="server"></asp:Label> GL NO <span class="style1">*</span></label>

                                                                                <div class="col-sm-8">

                                                                                 <asp:TextBox ID="TextBox23" runat="server" ReadOnly="true" class="form-control uppercase"></asp:TextBox> 

                                                                                </div>

                                                                            </div>

                                                                        </div>

                                                                             </div>

                                                                     </div>

                                                                      <div class="row">

                                                                         <div class="col-md-12">

                                                                               <div class="col-md-6 box-body">

                                                                            <div class="form-group">

                                                                                <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl24" Visible="false" runat="server"></asp:Label> No Rujukan </label>

                                                                                <div class="col-sm-8">

                                                                                 <asp:TextBox ID="txtname" runat="server" ReadOnly="true" class="form-control uppercase"></asp:TextBox> 

                                                                                </div>

                                                                                </div>

                                                                            </div>

                                                                              <div class="col-md-6 box-body">

                                                                            <div class="form-group">

                                                                                <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="Label25" Visible="false" runat="server"></asp:Label> Pembekal / Pelanggan </label>

                                                                                <div class="col-sm-8">

                                                                                 <asp:TextBox ID="TextBox26" runat="server" ReadOnly="true" class="form-control uppercase"></asp:TextBox> 

                                                                                </div>

                                                                                </div>

                                                                            </div>

                                                                             

                                                                             </div>

                                                                     </div>

                                                                       <div class="row">

                                                                         <div class="col-md-12">

                                                                               <div class="col-md-6 box-body">

                                                                            <div class="form-group">

                                                                                <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl20" Visible="false" runat="server"></asp:Label> Tarikh Mohon</label>     

                                                                                <div class="col-sm-8">

                                                                                 <div class="input-group">

                                                                                <asp:TextBox ID="txttarkihmo" runat="server" class="form-control validate[optional]" ReadOnly="true" placeholder="DD/MM/YYYY"></asp:TextBox> 

                                                                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>

                                             </div>

                                                                                </div>

                                                                                </div>

                                                                            </div>

                                                                               <div class="col-md-6 box-body">

                                                                            <div class="form-group">

                                                                                <label for="inputEmail3" class="col-sm-4 control-label">Projek </label>

                                                                                <div class="col-sm-8">

                                                                                  <asp:TextBox ID="TextBox22" runat="server" ReadOnly="true" class="form-control uppercase validate[optional]"></asp:TextBox> 

                                                                                  

                                                                                </div>

                                                                                </div>

                                                                            </div>

                                                                              <div class="col-md-6 box-body" style="display:none;">

                                                                            <div class="form-group">

                                                                                <label for="inputEmail3" class="col-sm-4 control-label"> Cara Bayaran</label>

                                                                                <div class="col-sm-8">

                                                                                  <asp:DropDownList ID="DropDownList6" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">

                                                                                     <asp:ListItem Value="">--- PILIH ---</asp:ListItem>

                                                                                       <asp:ListItem Value="01">KAKITANGAN</asp:ListItem>

                                                                                       <asp:ListItem Value="02">PEMBEKAL</asp:ListItem>

                                                                                       <asp:ListItem Value="03">PELANGGAN</asp:ListItem>

                                                                                       </asp:DropDownList>

                                                                                </div>

                                                                                </div>

                                                                            </div>

                                                                            <div class="col-md-6 box-body" style="display:none;">

                                                                            <div class="form-group">

                                                                                <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label Visible="false" ID="ps_lbl18" runat="server"></asp:Label>Nama Pemohon</label>

                                                                                <div class="col-sm-8">

                                                                                  <asp:TextBox ID="txtid" runat="server" class="form-control uppercase"></asp:TextBox> 

                                                                                    <asp:TextBox ID="TextBox16" runat="server" class="form-control uppercase" Visible="false"></asp:TextBox> 

                                                                                </div>

                                                                            </div>

                                                                        </div>

                                                                             </div>

                                                                     </div>

                                                                      <div class="row">

                                                                         <div class="col-md-12">

                                                                                <div class="col-md-6 box-body">

                                                                            <div class="form-group">

                                                                                <label for="inputEmail3" class="col-sm-4 control-label">Invois No / No Dokumen </label>

                                                                                <div class="col-sm-8">

                                                                                  <asp:TextBox ID="TextBox19" runat="server" ReadOnly="true" class="form-control uppercase validate[optional]"></asp:TextBox> 

                                                                                  

                                                                                </div>

                                                                                </div>

                                                                            </div>

                                                                            

                                                                              <div class="col-md-6 box-body">

                                                                            <div class="form-group">

                                                                                <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl27" runat="server"></asp:Label></label>

                                                                                <div class="col-sm-8">

                                                                                    <asp:TextBox ID="dd_terma" runat="server" ReadOnly="true" class="form-control uppercase validate[optional]"></asp:TextBox> 

                                                                                </div>

                                                                                </div>

                                                                            </div>

                                                                             

                                                                             </div>

                                                                     </div>

                                                                   <div class="row">

                                                                         <div class="col-md-12">

                                                                               <div class="col-md-6 box-body" style="display:none;">

                                                                            <div class="form-group">

                                                                                <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl21" Visible="false" runat="server"></asp:Label>Jenis Perolehan</label>                     

                                                                                <div class="col-sm-8">

                                                                                   <asp:DropDownList ID="ddbkepada" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" >

                                                                                     <asp:ListItem Value="">--- PILIH ---</asp:ListItem>

                                                                                       <asp:ListItem Value="01">Tender</asp:ListItem>

                                                                                       <asp:ListItem Value="02">Sebut Harga</asp:ListItem>

                                                                                       <asp:ListItem Value="03">Pembelian Terus</asp:ListItem>

                                                                                       <asp:ListItem Value="04">Panjar Wang Runcit</asp:ListItem>

                                                                                       <asp:ListItem Value="05">Pembelian Secara Darurat/Kecemasan</asp:ListItem>

                                                                                       <asp:ListItem Value="06">Tuntutan Perubatan</asp:ListItem>

                                                                                       <asp:ListItem Value="07">Lain-lain</asp:ListItem>

                                                                                  <%--   <asp:ListItem>KAKITANGAN</asp:ListItem>

                                                                                     <asp:ListItem>PEMBEKAL</asp:ListItem>

                                                                                        <asp:ListItem>KWSP</asp:ListItem>

                                                                                     <asp:ListItem>PERKESO</asp:ListItem>

                                                                                     <asp:ListItem>LHDN (PCB)</asp:ListItem>

                                                                                     <asp:ListItem>LHDN (CP 38)</asp:ListItem>

                                                                                     <asp:ListItem>GAJI KAKITANGAN</asp:ListItem>

                                                                                     <asp:ListItem>ANGKASA</asp:ListItem>

                                                                                     <asp:ListItem>KOOP / Bank</asp:ListItem>

                                                                                     <asp:ListItem>TABUNG HAJI</asp:ListItem>

                                                                                       <asp:ListItem>PEMBEKAL (PENGURUSAN ASET)</asp:ListItem>

                                                                                       <asp:ListItem>Keahlian</asp:ListItem>--%>

                                                                                </asp:DropDownList>

                                                                                </div>

                                                                            </div>

                                                                        </div>

                                                                             <div class="col-md-6 box-body">

                                                                            <div class="form-group">

                                                                                <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="Label26" Visible="false" runat="server"></asp:Label> Tarikh Invois</label>     

                                                                                <div class="col-sm-8">

                                                                                 <div class="input-group">

                                                                                <asp:TextBox ID="TextBox24" runat="server" class="form-control validate[optional]" ReadOnly="true" placeholder="DD/MM/YYYY"></asp:TextBox> 

                                                                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>

                                             </div>

                                                                                </div>

                                                                                </div>

                                                                            </div>

                                                                            <div class="col-md-6 box-body">

                                                                            <div class="form-group">

                                                                                <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl31" runat="server"></asp:Label></label>                                         

                                                                                <div class="col-sm-8">

                                                                                  <asp:TextBox ID="TextBox17" Placeholder="0.00"  runat="server" ReadOnly="true" Style="text-align:right;" class="form-control"></asp:TextBox> 

                                                                                </div>

                                                                            </div>

                                                                        </div>

                                                                              <div class="col-md-6 box-body" style="display:none;">

                                                                            <div class="form-group">

                                                                                <label for="inputEmail3" class="col-sm-4 control-label">Id Pemohon</label>

                                                                                <div class="col-sm-8">

                                                                                  <asp:TextBox ID="TextBox3" runat="server" class="form-control uppercase" ReadOnly="true"></asp:TextBox> 

                                                                                </div>

                                                                            </div>

                                                                        </div>

                                                                              

                                                                             </div>

                                                                     </div>
                                                                      <div class="row" style="pointer-events:none;">

                                                                         <div class="col-md-12">

                                                                                <div class="col-md-6 box-body">

                                                                            <div class="form-group">

                                                                                <label for="inputEmail3" class="col-sm-4 control-label">Cara Bayaran </label>

                                                                                <div class="col-sm-8">

                                                                                   <asp:DropDownList ID="DropDownList2" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                                  
                                                                                       </asp:DropDownList>

                                                                                  

                                                                                </div>

                                                                                </div>

                                                                            </div>

                                                                             </div>

                                                                     </div>
                                                                      <div class="row" style="display:none;">

                                                                         <div class="col-md-12">

                                                                              

                                                                             <div class="col-md-6 box-body">

                                                                            <div class="form-group">

                                                                                <label for="inputEmail3" class="col-sm-4 control-label">Syeksen</label>

                                                                                <div class="col-sm-8">

                                                                                  <asp:TextBox ID="TextBox4" runat="server" class="form-control uppercase" ReadOnly="true"></asp:TextBox> 

                                                                                </div>

                                                                            </div>

                                                                        </div>

                                                                             </div>

                                                                     </div>

                                                                         <div class="row" style="display:none;">

                                                                         <div class="col-md-12">

                                                                             

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

                                                                         <div class="col-md-12">

                                                                      

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

                                                                                <label for="inputEmail3" class="col-sm-4 control-label"> Kategori Penerima <span class="style1">*</span></label>

                                                                                <div class="col-sm-8">

                                                                                   <div class="col-sm-9">

                                                                                   <asp:DropDownList ID="DropDownList1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">

                                                                                     <asp:ListItem Value="">--- PILIH ---</asp:ListItem>

                                                                                       <asp:ListItem Value="01">KAKITANGAN</asp:ListItem>

                                                                                       <asp:ListItem Value="02">PEMBEKAL</asp:ListItem>

                                                                                       <asp:ListItem Value="03">PELANGGAN</asp:ListItem>

                                                                                       </asp:DropDownList>

                                                                                       </div>

                                                                                      <div class="col-sm-2">

                                                                                     <asp:Button ID="Button19" runat="server" visible="false" Text="New Entry" class="btn btn-danger" UseSubmitBehavior="false"

                                                                                   onclick="new_entry"  />

                                                                                          </div>

                                                                                </div>

                                                                                 

                                                                                </div>

                                                                            </div>

                                                                             

                                                                      </div>

                                                                             </div>

                                                                      <div class="row">

                                                                         <div class="col-md-12">

                                                                       

                                                                              <div class="col-md-6 box-body" style="display:none;">

                                                                                    <div class="form-group">

                                                                                <label for="inputEmail3" class="col-sm-4 control-label">Keterangan Bajet </label>

                                                                                <div class="col-sm-8">

                                                                                   <asp:DropDownList ID="DropDownList3" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" AutoPostBack="true" OnSelectedIndexChanged="load_bajet" >

                                                                             

                                                                                </asp:DropDownList>

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

                                                                                     <asp:DropDownList ID="jurnal_no" runat="server" class="form-control uppercase" ></asp:DropDownList>

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

                                                                     </fieldset>

                                                               

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

                                                                         <fieldset class="col-md-12">

                                                                    <%-- <legend style="width:12%">Maklumat Resit / Cek</legend>  --%>                                                                   

                                                                    

                                                                    <div class="row">

                                                                         <div class="col-md-12">

                                                                               <div class="col-md-6 box-body">

                                                                            <div class="form-group">

                                                                                <label for="inputEmail3" class="col-sm-4 control-label">Perkara </label>

                                                                                <div class="col-sm-8">

                                                                                  <textarea id="TextBox29" runat="server" rows="3" readonly="readonly" class="form-control uppercase validate[optional]"></textarea> 

                                                                                  

                                                                                </div>

                                                                                </div>

                                                                            </div>

                                                                             <div class="col-md-6 box-body" id="shw_01" runat="server" visible="false">

                                                                            <div class="form-group">

                                                                                <label for="inputEmail3" class="col-sm-4 control-label">Akaun Bank </label>

                                                                                <div class="col-sm-8">

                                                                                  <asp:TextBox ID="TextBox30" runat="server" ReadOnly="true" class="form-control uppercase validate[optional]"></asp:TextBox> 

                                                                                  

                                                                                </div>

                                                                                </div>

                                                                            </div>

                                                                     

                                                                             

                                                                      </div>

                                                                    </div>

                                                                              <div class="row">

                                                                         <div class="col-md-12">

                                                                      

                                                                      </div>

                                                                    </div>

                                                                             

                                                                    </fieldset>

                                                                            <div id="shw_02" runat="server">

                                                                         <fieldset class="col-md-12">

                                                                     <legend style="width:12%">Maklumat Kewangan</legend>                                                                     

                                                                    

                                                                    <div class="row">

                                                                         <div class="col-md-12">

                                                                        <div class="col-md-6 box-body">

                                                                            <div class="form-group">

                                                                                <label for="inputEmail3" class="col-sm-4 control-label">Tarikh Lulus Kewangan </label>

                                                                                <div class="col-sm-8">

                                                                                  <div class="input-group">

                                                                                <asp:TextBox ID="TextBox7" runat="server" class="form-control validate[optional]" ReadOnly="true" placeholder="DD/MM/YYYY"></asp:TextBox> 

                                                                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>

                                             </div>

                                                                                </div>

                                                                                </div>

                                                                            </div>

                                                                               <div class="col-md-6 box-body">

                                                                            <div class="form-group">

                                                                                <label for="inputEmail3" class="col-sm-4 control-label">Nota Kelulusan Kewangan </label>

                                                                                <div class="col-sm-8">

                                                                                  <asp:TextBox ID="TextBox27" runat="server" ReadOnly="true" class="form-control uppercase validate[optional]"></asp:TextBox> 

                                                                                  

                                                                                </div>

                                                                                </div>

                                                                            </div>

                                                                      </div>

                                                                    </div>

                                                                              <div class="row">

                                                                         <div class="col-md-12">

                                                                      

                                                                      </div>

                                                                    </div>

                                                                             

                                                                    </fieldset>

</div>

                                                                          <div id="shw_03" runat="server">

                                                                         <fieldset class="col-md-12">

                                                                     <legend style="width:12%">Maklumat Resit / Cek</legend>                                                                     

                                                                    

                                                                    <div class="row">

                                                                         <div class="col-md-12">

                                                                               <div class="col-md-6 box-body">

                                                                            <div class="form-group">

                                                                                <label for="inputEmail3" class="col-sm-4 control-label">No Resit / Cek </label>

                                                                                <div class="col-sm-8">

                                                                                  <asp:TextBox ID="TextBox14" runat="server" ReadOnly="true" class="form-control uppercase validate[optional]"></asp:TextBox> 

                                                                                  

                                                                                </div>

                                                                                </div>

                                                                            </div>

                                                                             <div class="col-md-6 box-body">

                                                                            <div class="form-group">

                                                                                <label for="inputEmail3" class="col-sm-4 control-label">Bank </label>

                                                                                <div class="col-sm-8">

                                                                                  <asp:TextBox ID="TextBox28" runat="server" ReadOnly="true" class="form-control uppercase validate[optional]"></asp:TextBox> 

                                                                                  

                                                                                </div>

                                                                                </div>

                                                                            </div>

                                                                        <div class="col-md-6 box-body">

                                                                            <div class="form-group">

                                                                                <label for="inputEmail3" class="col-sm-4 control-label">Tarikh Resit / Cek </label>

                                                                                <div class="col-sm-8">

                                                                                  <div class="input-group">

                                                                                <asp:TextBox ID="TextBox9" runat="server" class="form-control validate[optional]" ReadOnly="true" placeholder="DD/MM/YYYY"></asp:TextBox> 

                                                                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>

                                             </div>

                                                                                </div>

                                                                                </div>

                                                                            </div>

                                                                             

                                                                      </div>

                                                                    </div>

                                                                              <div class="row">

                                                                         <div class="col-md-12">

                                                                      

                                                                      </div>

                                                                    </div>

                                                                             

                                                                    </fieldset>

</div>

                                                                       <div class="box-body">&nbsp;</div>

                                                                     <div class="box-header with-border">

                            <h3 class="box-title">Butiran</h3>

                        </div>

                                                                       <div class="dataTables_wrapper form-inline dt-bootstrap" >

                                      <div class="row" style="overflow:auto;">

           <div class="col-md-12 box-body">

                      <asp:gridview ID="gridmohdup" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="5" CellSpacing="5" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="30" ShowFooter="true" GridLines="None" onrowdatabound="gridmohdup_RowDataBound" >

            <Columns>

                 <%-- <asp:TemplateField HeaderText="BIL">  
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>--%>
          
                   <asp:TemplateField HeaderText="Kod Akaun" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left">

                <ItemTemplate>

                     <asp:Label ID="Label22" runat="server" Text='<%# Eval("sno") %>' Visible="false" class="uppercase" ></asp:Label>
                    <asp:Label ID="Label27" runat="server" Text='<%# Eval("sno1") %>' Visible="false" class="uppercase" ></asp:Label>
                    <asp:Label ID="Label28" runat="server" Text='<%# Eval("kd_akaun") %>' Visible="false" class="uppercase" ></asp:Label>
                    <asp:Label ID="Label23" runat="server" Text='<%# Eval("jen_txi") %>' Visible="false" class="uppercase" ></asp:Label>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("kd_bjt") %>' Visible="false" class="uppercase" ></asp:Label>
                    <asp:Label ID="Label29" runat="server"  Visible="false" class="uppercase" ></asp:Label>

                    <asp:DropDownList ID="gridmohdup_ddakaun" class="form-control uppercase select2" Width="80%" onselectedindexchanged="sel_Akaun_desc" AutoPostBack="true" runat="server"></asp:DropDownList>
                   
                      <asp:LinkButton runat="server" ID="gridmohdup_lnkView" Visible="false" OnClick="gridmohdup_lnkView_Click">

                         <img src="../dist/img/gl_icon.png" width="22px" height="22px" />
                        </asp:LinkButton>
                </ItemTemplate>
                          <FooterTemplate>
                 <asp:Button ID="ButtonAdd" runat="server" style="width:50%;" CssClass="btn btn-success" Text="Tambah Baru" 
                        onclick="ButtonAdd1_Click" />
                </FooterTemplate>
            </asp:TemplateField>
                
                  <asp:TemplateField HeaderText="Keterangan" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Left">

                <ItemTemplate>

                  <asp:TextBox ID="ket" runat="server" Text='<%# Eval("ket") %>' class="uppercase form-control" Width="100%" ></asp:TextBox>

                </ItemTemplate>
                          <FooterStyle HorizontalAlign="Right" />
           <FooterTemplate>
                   <asp:Label ID="Label21_ket"  Width="100%" runat="server" Text="JUMLAH (RM)"  ></asp:Label>
         </FooterTemplate>
            </asp:TemplateField>
            
                      <asp:TemplateField HeaderText="Debit (RM)" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Right">

                <ItemTemplate>

                  <asp:TextBox ID="deb_amount" runat="server" style="text-align:right;" Width="100%" Text='<%# Eval("jum_deb","{0:n}") %>' OnTextChanged="changed_amt1" AutoPostBack="true" class="uppercase form-control" ></asp:TextBox>

                </ItemTemplate>
                           <FooterStyle HorizontalAlign="Right" />
           <FooterTemplate>
                   <asp:Label ID="Label1_deb" CssClass="form-control"  Width="100%" runat="server" Text="0.00"  ></asp:Label>
         </FooterTemplate>
            </asp:TemplateField>
                  <asp:TemplateField HeaderText="Kredit (RM)" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Right">

                <ItemTemplate>

                  <asp:TextBox ID="kre_amount" runat="server" style="text-align:right;" Width="100%" Text='<%# Eval("jum_kre","{0:n}") %>' OnTextChanged="changed_amt1" AutoPostBack="true" class="uppercase form-control" ></asp:TextBox>

                </ItemTemplate>
                       <FooterStyle HorizontalAlign="Right" />
           <FooterTemplate>
                   <asp:Label ID="Label2_kre" CssClass="form-control"  Width="100%" runat="server" Text="0.00"  ></asp:Label>
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

                <asp:gridview ID="Gridview1_shw" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="5" CellSpacing="5" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="30" ShowFooter="true" GridLines="None" onrowdatabound="RowDataBound_view">

            <Columns>

                  <asp:TemplateField HeaderText="BIL">  

                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>

                                            <ItemTemplate>  

                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 

                                            </ItemTemplate>  

                                                </asp:TemplateField>

        
                   <asp:TemplateField HeaderText="Kod Akaun" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left">

                <ItemTemplate>

                   <asp:Label ID="kod_akaun" runat="server" Text='<%# Eval("kod_akaun") %>' class="uppercase" ></asp:Label>

                </ItemTemplate>

            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Kod Bajet" Visible="false" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left">

                <ItemTemplate>

                  <asp:Label ID="kd_bjt" runat="server" Text='<%# Eval("kod_bajet") %>' class="uppercase" ></asp:Label>

                </ItemTemplate>

            </asp:TemplateField>
                  <asp:TemplateField HeaderText="Keterangan" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Left">

                <ItemTemplate>

                  <asp:Label ID="ket" runat="server" Text='<%# Eval("GL_desc1") %>' class="uppercase" ></asp:Label>

                </ItemTemplate>
                        <FooterStyle HorizontalAlign="Right" />
           <FooterTemplate>
                   <asp:Label ID="Label21_ket"  Width="100%" runat="server" Text="JUMLAH (RM)"  ></asp:Label>
         </FooterTemplate>
            </asp:TemplateField>
            
             <asp:TemplateField HeaderText="Jenis Transaksi" Visible="false" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left">

                <ItemTemplate>

                     <asp:Label ID="jen_txi" runat="server" Text='<%# Eval("gl_txi") %>' class="uppercase" ></asp:Label>
                </ItemTemplate>

            </asp:TemplateField>

                      <asp:TemplateField HeaderText="Debit (RM)" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Right">

                <ItemTemplate>

                  <asp:Label ID="amount_deb" runat="server" Text='<%# Eval("Amaun_deb","{0:n}") %>' class="uppercase" ></asp:Label>

                </ItemTemplate>
                           <FooterStyle HorizontalAlign="Right" />
           <FooterTemplate>
                   <asp:Label ID="Label21_deb" CssClass="form-control"  Width="100%" runat="server" Text="0.00"  ></asp:Label>
         </FooterTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Kredit (RM)" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Right">

                <ItemTemplate>

                  <asp:Label ID="amount_kre" runat="server" Text='<%# Eval("Amaun_kre","{0:n}") %>' class="uppercase" ></asp:Label>

                </ItemTemplate>
                      <FooterStyle HorizontalAlign="Right" />
           <FooterTemplate>
                   <asp:Label ID="Label21_kre" CssClass="form-control"  Width="100%" runat="server" Text="0.00"  ></asp:Label>
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

                     

                                                 </div>

                                          </div>

                                                                           </div>

                                                                <div class="box-body">&nbsp;</div>

                                                                        <div id="sts" runat="server" visible="false">

                                                                              <fieldset class="col-md-12">

                                                                     <legend style="width:12%">Kelulusan Jurnal</legend> 

                                                                                   <div class="row">

                                                                         <div class="col-md-12">

                                                                               <div class="col-md-6 box-body">

                                                                          

                                                                            <div class="form-group">

                                                                                <label for="inputEmail3" class="col-sm-4 control-label">Nota Kelulusan <%--<span class="style1">*</span>--%></label>

                                                                                <div class="col-sm-8">

                                                                                  <textarea id="jurnal_txt" runat="server" rows="3" class="form-control uppercase"></textarea>

                                                                                  

                                                                                </div>

                                                                                </div>

                                                                            </div>

                                                                        <div class="col-md-6 box-body">

                                                                          

                                                                            <div class="form-group">

                                                                                <label for="inputEmail3" class="col-sm-4 control-label">Status Lulus <span class="style1">*</span></label>

                                                                                <div class="col-sm-8">

                                                                                   <asp:DropDownList ID="ddsts" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" >

                                                                                <asp:ListItem Value="">--- PILIH ---</asp:ListItem>

                                                                                     <asp:ListItem Value="L">LULUS</asp:ListItem>

                                                                                     <asp:ListItem Value="T">Tidak LULUS</asp:ListItem>

                                                                                </asp:DropDownList>

                                                                                  

                                                                                </div>

                                                                                </div>

                                                                              <div class="form-group">

                                                                                <label for="inputEmail3" class="col-sm-4 control-label">Jenis Jurnal <span class="style1">*</span></label>

                                                                                <div class="col-sm-8">

                                                                                   <asp:DropDownList ID="DropDownList5" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" >

                                                                                </asp:DropDownList>

                                                                                  

                                                                                </div>

                                                                                </div>

                                                                            </div>

                                                                           

                                                                      </div>

                                                                    </div>

                                                                                   <div class="row">

                                                                         <div class="col-md-12">

                                                                        <div class="col-md-6 box-body">

                                                                            

                                                                            </div>

                                                                                <div class="" style="display:none;">

                                                                            <div class="col-md-5 col-sm-1">

                                                                                <label>

                                                                                  <asp:Label ID="ps_lbl36" runat="server"></asp:Label>

                                                                                </label>

                                                                            </div>

                                                                            <div class="col-md-7 col-sm-2">

                                                                                 <asp:TextBox ID="txtApr" runat="server" class="form-control" ></asp:TextBox> 

                                                                                  

                                                                            </div>

                                                                        </div>

                                                                              

                                                                      </div>

                                                                    </div> 

                                                                                  

                                                                                  </fieldset>

                                                                            </div>

                                                                     <div class="box-body">&nbsp;</div>

                                                                   <div class="row">

                                                                                 <div class="col-md-12" style="text-align:center;">

                                                                                    <div class="form-group">

                                                                                        <div class="col-sm-12">

                                                                                    <asp:Button ID="kem_Button2" Visible="false" runat="server" class="btn btn-danger" Text="HANTAR KE GL" Type="submit" onclick="btnkem_Click" />

                                                                                    <asp:Button ID="Button23" runat="server" class="btn btn-default" Text="Kembali" Type="submit"/>

                                                                                    <asp:Button ID="btnprintmoh" runat="server" class="btn btn-warning" Visible="false" Text="Print" Type="submit" />

                                                                                    <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Tutup" Type="submit" onclick="Button1_Click"/>

                                                                                        </div>

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
                                                                
                                                                   </div>

                                                                </div>



                                                               </div>

                                                               

                                                        </div>

                           <cc1:ModalPopupExtender BackgroundCssClass="modalBg" DropShadow="true" ID="ModalPopupExtender1"

                                                                PopupControlID="Panel3" runat="server" TargetControlID="btnBack" PopupDragHandleControlID="Panel2"

                                                                CancelControlID="btnBack">

                                                            </cc1:ModalPopupExtender>

                                      <asp:Panel ID="Panel3" runat="server" CssClass="modalPanel" Style="display: none;  overflow-y:auto; height: 70vh;">

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

                            <div class="row">

                             <div class="col-md-12">

                                     <div class="col-md-6 box-body">

                                <div class="form-group">

                                    <label for="inputEmail3" class="col-sm-4 control-label">Jenis Akaun <span class="style1">*</span></label>

                                    <div class="col-sm-8">

                                      <asp:DropDownList ID="pel_dd_akaun" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" OnSelectedIndexChanged="sel_jenis" AutoPostBack="true"></asp:DropDownList>

                                    </div>

                                </div>

                            </div>

                                    <div class="col-md-6 box-body">

                                <div class="form-group">

                                    <label for="inputEmail3" class="col-sm-4 control-label">Kod Akaun</label>

                                    <div class="col-sm-8">

                                       <asp:TextBox ID="pel_TextBox10" runat="server" class="form-control validate[optional] uppercase" ReadOnly="true" MaxLength="20"></asp:TextBox>

                                    </div>

                                </div>

                            </div>

                                 </div>

                                </div>

                              <div class="row">

                             <div class="col-md-12">

                                     <div class="col-md-6 box-body">

                                <div class="form-group">

                                    <label for="inputEmail3" class="col-sm-4 control-label">Pelbagai</label>

                                    <div class="col-sm-8">

                                     <asp:CheckBox ID="chk_pel" runat="server" />

                                    </div>

                                </div>

                            </div>

                                 

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

                                <asp:Button ID="Button24" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" OnClick="pel_Click_bck" />

                                 

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

    </asp:UpdatePanel>

        <!-- /.row -->

    </section>

                    <!-- /.content -->

                </div>

                <!-- /.content-wrapper -->

  

</asp:Content>



