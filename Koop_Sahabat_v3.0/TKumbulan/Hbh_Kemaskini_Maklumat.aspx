<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../TKumbulan/Hbh_Kemaskini_Maklumat.aspx.cs" Inherits="Hbh_Kemaskini_Maklumat" %>

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
                        <h1>Senarai Maklumat Hibah</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Tabung Kumpulan</a></li>
                            <li class="active">Senarai Maklumat Hibah</li>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama Batch <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                     <asp:DropDownList ID="ddbatch" class="form-control select2 uppercase" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama Cawangan <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                     <asp:DropDownList ID="ddcaw" class="form-control validate[optional] select2 uppercase" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                              <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No KP Baru <span class="style1">*</span></label>
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
                                          <asp:Button ID="Button1" runat="server" class="btn btn-danger" Text="Carian " OnClick="Button1_Click" />
                                                            <asp:Button ID="Button2" runat="server" class="btn btn-default" Text="Set Semula"/>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>  
                            <hr />
                                     <div class="box-header with-border">
                            <h3 class="box-title">Senarai Pemerima Hibah</h3>
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
                                   <asp:GridView ID="GridView1" CssClass="uppercase" CellPadding="8" CellSpacing="2" Width="100%" Height="100%" AllowPaging="true" runat="server" PageSize="10" AutoGenerateColumns="false" EmptyDataText = "No files uploaded" OnPageIndexChanging="gvSelected_PageIndexChanging">
                                       <PagerStyle CssClass="pager" />
        <Columns>
            <asp:TemplateField HeaderText="BIL" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                                    ItemStyle-Width="150" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
            <asp:TemplateField HeaderText="No KP" ItemStyle-HorizontalAlign="Left" >
                                            
                                            <ItemTemplate>  
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("mem_hbh_new_IC") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
          
              <asp:BoundField DataField="mem_hbh_SAHABAT_name" HeaderText="Nama"  ItemStyle-HorizontalAlign="Left" />
              <asp:BoundField DataField="mem_hbh_SAHABAT_no" HeaderText="No Anggota" />
              <asp:BoundField DataField="mem_hbh_CAW_name" HeaderText="Cawangan" ItemStyle-HorizontalAlign="Left" />
              <asp:BoundField DataField="mem_hbh_PUSAT_name" HeaderText="Pusat" ItemStyle-HorizontalAlign="Left" />
              <asp:BoundField DataField="mem_hbh_BAKI_ST" HeaderText="Baki ST(RM)" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right"  />
              <asp:BoundField DataField="bank_no" HeaderText="No Akaun Bank" ItemStyle-HorizontalAlign="Left" />
              <asp:BoundField DataField="bank_name" HeaderText="Nama Bank" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-HorizontalAlign="Left" />
            
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
                                        <asp:Button ID="Button5" runat="server" class="btn btn-default" Text="Tutub"/>
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

