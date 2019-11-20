<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../TKumbulan/Hbh_kiraan_hibah.aspx.cs" Inherits="Hbh_kiraan_hibah" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManagerCalendar" AsyncPostBackTimeOut="72000" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
     <script type="text/javascript">
             // Get the instance of PageRequestManager.
             var prm = Sys.WebForms.PageRequestManager.getInstance();
             // Add initializeRequest and endRequest
             prm.add_initializeRequest(prm_InitializeRequest);
             prm.add_endRequest(prm_EndRequest);

             // Called when async postback begins
             function prm_InitializeRequest(sender, args) {
                 // get the divImage and set it to visible
                 var panelProg = $get('divImage');
                 panelProg.style.display = '';
                 // reset label text
                 var lbl = $get('<%= this.lblText.ClientID %>');
                 lbl.innerHTML = '';

                 // Disable button that caused a postback
                 $get(args._postBackElement.id).disabled = true;
             }

             // Called when async postback ends
             function prm_EndRequest(sender, args) {
                 // get the divImage and hide it again
                 var panelProg = $get('divImage');
                 panelProg.style.display = 'none';

                 // Enable button that caused a postback
                 $get(sender._postBackSettings.sourceElement.id).disabled = false;
             }
         </script>
     <!-- Content Wrapper. Contains page content -->
       
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>Kemaskini Maklumat Hibah</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Tabung Kumpulan</a></li>
                            <li class="active">Kemaskini Maklumat Hibah</li>
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
                            <h3 class="box-title">Maklumat Hibah</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                         <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                             
                              <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Jana <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="f_date" runat="server" class="form-control datepicker mydatepickerclass"
                                                                    placeholder="PICK DATE"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Bagi Tahun <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                  <asp:DropDownList ID="DropDownList2" class="form-control select2 uppercase" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                              <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Peratusan Hibah (%) <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="txtph" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Batch Hibah <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="txtnobat" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                              <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Proses <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="TextBox1" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 
                                 </div>
                                </div>
                              <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-12 box-body text-center">
                                <div class="form-group">
                                    <div class="col-sm-12">
                                           <asp:Button ID="Button1" runat="server" class="btn btn-danger" Text="Proses" OnClick="Button1_Click"  />
                                                            <asp:Button ID="Button2" runat="server" class="btn btn-default" Text="Batal"/>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>  
                            
                                     <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Hibah Anggota</h3>
                        </div>                                 
                              <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                      <%--<div class="row" >--%>
                                    <div class="col-md-12 box-body">
               <div class="col-md-1 box-body"> &nbsp; </div>
                <div class="col-md-10 box-body">
                      <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
                 <div id="divImage" class="text-center" style="display:none; padding-top: 30px; font-weight:bold;">
                     <asp:Image ID="img1" runat="server" ImageUrl="../dist/img/LoaderIcon.gif" />&nbsp;&nbsp;&nbsp;Processing Please wait ... </div> 
               </div>
                <div class="col-md-1 box-body"> &nbsp; </div>
               </div>
           <div class="col-md-12 box-body">
                                 <asp:GridView ID="GridView1" CellPadding="8" CellSpacing="2" Width="100%" Height="100%" AllowPaging="true" runat="server" PageSize="20" AutoGenerateColumns="false" EmptyDataText = "No files uploaded" OnPageIndexChanging="gvSelected_PageIndexChanging">
                                     <PagerStyle CssClass="pager" />
        <Columns>
           
          <asp:TemplateField HeaderText="Bil">
    <ItemTemplate>
        <%# Container.DataItemIndex + 1 %>
    </ItemTemplate>
</asp:TemplateField>
              <asp:BoundField DataField="mem_hbh_new_IC" HeaderText="No KP"  ItemStyle-HorizontalAlign="Left" />
              <asp:BoundField DataField="mem_hbh_SAHABAT_name" HeaderText="Nama" ItemStyle-HorizontalAlign="Left" />
              <asp:BoundField DataField="mem_hbh_SAHABAT_no" HeaderText="No Anggota"  />
              <asp:BoundField DataField="mem_hbh_CAW_name" HeaderText="Cawangan" ItemStyle-HorizontalAlign="Left" />
              <asp:BoundField DataField="mem_hbh_PUSAT_name" HeaderText="Pusat" ItemStyle-HorizontalAlign="Left" />
              <asp:BoundField DataField="mem_hbp_batch_proses_amt" HeaderText="Amt HIBAH(RM)" ItemStyle-HorizontalAlign="right"  DataFormatString="{0:N}" />
              <asp:BoundField DataField="mem_hbp_batch_proses_ST_amt" HeaderText="Jumlah ST" ItemStyle-HorizontalAlign="right"  DataFormatString="{0:N}" />
            <asp:BoundField DataField="mem_hbh_batch_id" HeaderText="No Batch" ItemStyle-HorizontalAlign="center" />
              <asp:BoundField DataField="mem_hbh_crt_dt" HeaderText="Tarikh Kredit Bayaran" ItemStyle-HorizontalAlign="center" DataFormatString="{0:d}" />
      
          
        </Columns>
                                       <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />                                                       
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
               </div>
          </div>
                                <div class="box-body">&nbsp;
                                    </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-12 box-body text-center">
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <asp:Button ID="Button5" runat="server" class="btn btn-default" Text="Tutup"/>
                                    </div>
                                </div>
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

