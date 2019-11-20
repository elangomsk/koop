<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../keanggotan/Kelulusan_Penyelesaian1.aspx.cs" Inherits="Kelulusan_Penyelesaian1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        
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
                        <h1><asp:Label ID="ps_lbl1" runat="server"></asp:Label>  </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i><asp:Label ID="ps_lbl2" runat="server"></asp:Label>   </a></li>
                            <li class="active">  <asp:Label ID="ps_lbl3" runat="server"></asp:Label>   </li>
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
                            <h3 class="box-title"> <asp:Label ID="ps_lbl4" runat="server"></asp:Label>   </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                           
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl5" runat="server"></asp:Label>   <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                    <asp:DropDownList ID="DropDownList1" class="form-control select2 validate[optional]" style="text-transform:uppercase;" runat="server">
                                                                    </asp:DropDownList>
                                        
                                    </div>
                                </div>
                            </div>
                                     <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl6" runat="server"></asp:Label>   <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="janaan" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl7" runat="server"></asp:Label>   </label>
                                    <div class="col-sm-8">
                                     <div class="input-group">
                                                       <asp:TextBox ID="TextBox1" runat="server" class="form-control  datepicker mydatepickerclass" ReadOnly="false"></asp:TextBox>
                                         <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                       </div>

                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>


                             <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                 <asp:Button ID="Button2" runat="server" class="btn btn-primary" Text="Carian" UseSubmitBehavior="false" OnClick="BindGridview"/>
                                        <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" OnClick="Reset_btn" usesubmitbehavior="false"/>
                                 
                            </div>
                           </div>
                               </div>
                           <div class="box-body">&nbsp;
                                    </div>
 
                               <div id="disp_hdr_txt" runat="server" visible="true">
                            <div class="box-header with-border">
                            <h3 class="box-title"><asp:Label ID="ps_lbl10" runat="server"></asp:Label>   </h3>
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
             <asp:GridView ID="gvSelected" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="30" ShowFooter="false" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging">
                 <PagerStyle CssClass="pager" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="BIL" ItemStyle-HorizontalAlign="Center"> 
                                            <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%#Container.DataItemIndex+1 %>' runat="server" ItemStyle-Width="150"/>
                                            <asp:Label Visible="false" ID="Label1" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                            </ItemTemplate>   
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="NAMA">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("mem_name") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NO KP">  
                                            <ItemTemplate>  
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("mem_new_icno") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="CAWANGAN">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("cawangan_name") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PUSAT">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("mem_centre") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="JENIS PENYELESAIAN">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("Application_name") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="AMAUN DILULUSKAN (RM)">   
                                                <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>  
                                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("set_apply_amt","{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="STATUS KELULUSAN" ItemStyle-HorizontalAlign="Center">   
                                            <ItemTemplate>  
                                                <asp:RadioButton ID="chkSelect_1" Checked="true" Text="&nbsp;Lulus" runat="server" GroupName="status" />
                                                &nbsp;&nbsp;
                                                <asp:RadioButton ID="chkSelect_2" Text="&nbsp;Tidak Lulus" runat="server" GroupName="status" />
                                                <asp:Label Visible="false" ID="Label8" runat="server" Text='<%#Eval("set_bank_acc_no") %>'></asp:Label>
                                                <asp:Label Visible="false" ID="Label9" runat="server" Text='<%#Eval("set_batch_name") %>'></asp:Label>
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

                            <div class="box-body">&nbsp;</div>


                             <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                              <asp:Button ID="Button3" runat="server" class="btn btn-danger" Text="Simpan" OnClick="submit_button" />
                                        <asp:Button ID="Button4" runat="server" class="btn btn-default" Visible="false" Text="Batal" usesubmitbehavior="false" />
                                 
                            </div>
                           </div>
                               </div>
                           <div class="box-body">&nbsp;
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