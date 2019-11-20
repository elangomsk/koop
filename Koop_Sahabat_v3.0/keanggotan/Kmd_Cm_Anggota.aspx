<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../keanggotan/Kmd_Cm_Anggota.aspx.cs" Inherits="Kmd_Cm_Anggota" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       
   <style>
       .table label{
               font-size: 11px;
       }
   </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
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
                <div class="content-wrapper">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>    <asp:Label ID="ps_lbl1" runat="server"></asp:Label>         </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i> <asp:Label ID="ps_lbl2" runat="server"></asp:Label> </a></li>
                            <li class="active">     <asp:Label ID="ps_lbl3" runat="server"></asp:Label>      </li>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">  <asp:Label ID="ps_lbl5" runat="server"></asp:Label> <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="DropDownList1" class="form-control select2 validate[optional]" style="text-transform:uppercase;" runat="server">
                                                                    </asp:DropDownList>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="You can't leave this Empty" CssClass="requiredFieldValidateStyle" ForeColor="Red" ControlToValidate="DropDownList1" ValidationGroup="vgSubmit_cma_simp">
                                    </asp:RequiredFieldValidator>

                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                     
                                    <div class="col-sm-8">
                                           <asp:CheckBox ID="s_update" runat="server" CssClass="mycheckbox" Text=" Pilih Semua Permohonan"/>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">    <asp:Label ID="ps_lbl6" runat="server"></asp:Label>  </label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="TextBox18" runat="server"  class="form-control select2 uppercase" Width="100%"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  <asp:Label ID="ps_lbl7" runat="server"></asp:Label>  </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                                        <asp:TextBox ID="TextBox2" runat="server" class="form-control  datepicker mydatepickerclass"></asp:TextBox>
                                             <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl8" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="s_icno" runat="server" class="form-control uppercase" MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>  
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-8">
                                        <asp:Button ID="Button2" runat="server" class="btn btn-primary" Text="Carian" OnClick="BindGridview" ValidationGroup="vgSubmit_cma_simp"/>
                                        <asp:Button ID="Button3" runat="server" class="btn btn-default" Text="Set Semula" usesubmitbehavior="false" OnClick="btnreset_Click" />
                                    </div>
                                </div>
                            </div>  
                                 </div>
                                 </div>
                                 
                              <div class="box-body">&nbsp;</div>
                             <div class="box-header with-border">
                            <h3 class="box-title"><asp:Label ID="ps_lbl11" runat="server"></asp:Label> </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                             <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                    <%--  <div class="row" style="overflow:auto;">--%>
           <div class="col-md-12 box-body">
                <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
                 <div id="divImage" class="text-center" style="display:none; padding-top: 30px; font-weight:bold;">
                     <asp:Image ID="img1" runat="server" ImageUrl="../dist/img/LoaderIcon.gif" />&nbsp;&nbsp;&nbsp;Processing Please wait ... </div> 
                <asp:GridView ID="gvSelected" OnPageIndexChanging="gvSelected_PageIndexChanging" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" DataKeyNames="mem_new_icno" PageSize="50" ShowFooter="false" GridLines="None">
                    <PagerStyle CssClass="pager" />
                                      <Columns>
                                            <asp:TemplateField HeaderText="BIL">
                                               
                                               <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect"  Visible="false" Checked="true" runat="server" />
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="150"/>
                                            
                                            </ItemTemplate>

                                            </asp:TemplateField>
                                           <asp:TemplateField HeaderText="NO KP">  
                                            <ItemTemplate>  
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("mem_new_icno") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                          <asp:TemplateField HeaderText="KAWASAN">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label4_kaw" runat="server" Text='<%# Eval("kavasan_name").ToString() == "EMPTY" ? "" : Eval("kavasan_name").ToString() %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CAWANGAN">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("cawangan_name") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField HeaderText="NAMA">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("mem_name") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="PUSAT">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("mem_centre") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JUMLAH DIVIDEN (RM)">   
                                                <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>  
                                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("div_debit_amt","{0:n}") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JUMLAH SYER (RM)">   
                                                <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>  
                                                <asp:Label ID="Label10" runat="server" Text='<%# Bind("sha_amt","{0:n}") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NO AKAUN BANK">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label8" runat="server" Text='<%# Bind("div_bank_acc_no") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BANK">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label9" runat="server" Text='<%# Bind("Bank_Name") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="STATUS KELULUSAN">   
                                            <ItemTemplate>  
                                                <asp:RadioButton ID="chkSelect_1" Checked="true" Text="&nbsp;Lulus" runat="server" Font-Size="Smaller" GroupName="status" />
                                                &nbsp;&nbsp;
                                                <asp:RadioButton ID="chkSelect_2" Text="&nbsp;Tidak Lulus" runat="server"  Font-Size="XX-Small" GroupName="status" />
                                            </ItemTemplate>  
                                                </asp:TemplateField>

                                                                                       
                                        </Columns>
                      <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />                                                       
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />

                                    </asp:GridView>
               </div>
                                 </div>
                              <br />
                                     
                                                 <div class="row" id="d1" runat="server" visible="false">
                                                <div class="col-md-6">
                                                    <div class="col-md-5 col-sm-1">
                                                        <label>Jumlah Anggota Layak  :  </label>
                                                    </div>
                                                    <div class="col-md-7 col-sm-2">
                                                       <label>   <asp:Label ID="Label1" runat="server" Text=""></asp:Label></label>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 col-sm-2">
                                                    <div class="col-md-5 col-sm-1">
                                                        <label>Jumlah Anggota ada Akaun Bank :  </label>
                                                    </div>
                                                    <div class="col-md-7 col-sm-2">
                                                       <label> <asp:Label ID="Label6" runat="server" Text=""></asp:Label></label>
                                                    </div>
                                                </div>
                                            </div>
                                         
                                            
                                            <br />
                                              <div class="row" id="d2" runat="server" visible="false">
                                                 
                                                <div class="col-md-6">
                                                    <div class="col-md-5 col-sm-1">
                                                        <label>Jumlah Dividen (RM) :  </label>
                                                    </div>
                                                    <div class="col-md-7 col-sm-2">
                                                        <label> <asp:Label ID="Label12" runat="server" Text=""></asp:Label></label>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 col-sm-2">
                                                    <div class="col-md-5 col-sm-1">
                                                        <label>Jumlah Anggota Tiada Akaun Bank :  </label>
                                                    </div>
                                                    <div class="col-md-7 col-sm-2">
                                                      <label>  <asp:Label ID="Label11" runat="server" Text=""></asp:Label></label>
                                                    </div>
                                                </div>
                                                                                 
                                            </div>
                                            <br />
                                              <div class="row" id="d3" runat="server" visible="false">
                                               <div class="col-md-6">
                                                    <div class="col-md-5 col-sm-1">
                                                        <label>Jumlah Syer (RM) :  </label>
                                                    </div>
                                                    <div class="col-md-7 col-sm-2">
                                                        <label> <asp:Label ID="Label10" runat="server" Text=""></asp:Label></label>
                                                    </div>
                                                </div>
                                         
                                            </div>
                                            <br />
                              <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                  <asp:Button ID="Button1" runat="server" class="btn btn-danger" Visible="false" Text="Simpan" OnClick="submit_button" />
                                                
                                 
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












