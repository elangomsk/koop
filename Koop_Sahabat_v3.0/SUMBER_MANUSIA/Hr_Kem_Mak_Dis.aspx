<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/Hr_Kem_Mak_Dis.aspx.cs" Inherits="Hr_Kem_Mak_Dis" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       <script type="text/javascript">
        function ShowImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=btnUpload.ClientID%>').prop('src', e.target.result)
                        .width(100)
                        .height(100);
                };
                reader.readAsDataURL(input.files[0]);
                }
            }
    </script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

    <%-- <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>--%>
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1> <asp:Label ID="ps_lbl1" runat="server"></asp:Label>    </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i><asp:Label ID="ps_lbl2" runat="server"></asp:Label></a></li>
                            <li class="active"><asp:Label ID="ps_lbl3" runat="server"></asp:Label></li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
      
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl5" runat="server"></asp:Label></label>
                                    <div class="col-sm-8 text-right text-bold">
                                         <asp:Label ID="Kaki_no" runat="server" class="uppercase" MaxLength="1000"></asp:Label>
                                                         <asp:TextBox ID="Applcn_no1" runat="server" class="form-control validate[optional] uppercase"
                                                                                MaxLength="150" Visible="false"></asp:TextBox>
                                        
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">                                    
                                    <div class="col-sm-8">
                                        <asp:Button ID="Button3" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" OnClick="Click_bck" />
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl7" runat="server"></asp:Label></label>
                                    <div class="col-sm-8 text-right">
                                        <asp:Label ID="s_nama" runat="server" class="uppercase"></asp:Label>
                                    </div>
                                </div>
                            </div>
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl8" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8 text-right">
                                       <asp:Label ID="s_gred" runat="server" class="uppercase"></asp:Label>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                    <div class="row">
                             <div class="col-md-12">
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl9" runat="server"></asp:Label>  </label>
                                    <div class="col-sm-8 text-right">
                                        <asp:Label ID="txt_org" runat="server" class="uppercase" MaxLength="15"></asp:Label>
                                    </div>
                                </div>
                            </div>
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl10" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8 text-right">
                                        <asp:Label ID="TextBox2" runat="server" class="uppercase" MaxLength="15"></asp:Label>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                            <div class="row">
                             <div class="col-md-12">
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl11" runat="server"></asp:Label>  </label>
                                    <div class="col-sm-8 text-right">
                                        <asp:Label ID="s_jab" runat="server" class="uppercase" MaxLength="15"></asp:Label>
                                    </div>
                                </div>
                            </div>
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl12" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8 text-right">
                                        <asp:Label ID="s_jaw" runat="server" class="uppercase" MaxLength="15"></asp:Label>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                  <div class="box-header with-border">
                            <h3 class="box-title"><asp:Label ID="ps_lbl13" runat="server"></asp:Label></h3>
                        </div>
                             <div class="box-body">&nbsp;</div>
                            <div class="row">
                             <div class="col-md-12">
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl14" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="dd_sebab" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                        </asp:DropDownList>
                                        <asp:TextBox ID="GP_rno" Visible="false" runat="server" class="form-control validate[optional]"  MaxLength="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                          
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl15" runat="server"></asp:Label>  <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        <asp:TextBox ID="GP_date" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
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
                                    <label for="inputPassword3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl16" runat="server"></asp:Label> </label>

                                    <div class="col-sm-8">
                                       <asp:FileUpload ID="FileUpload1" runat="server" />
                                                       <br />
                                                          <asp:Button ID="btnUpload" runat="server" class="btn btn-warning" Text="Upload" UseSubmitBehavior="false" OnClick="Upload"  />
                                                       <br />
                                                       <asp:LinkButton ID="lnkDownload" runat="server" OnClick = "DownloadFile"><asp:Label ID="lab_fname" runat="server"></asp:Label></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="Label1" runat="server">   Catatan</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox1" runat="server" class="form-control uppercase" MaxLength="1000" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                </div>
                            </div>
                                </div>
                                 </div>
                                </div>

                             <div class="box-body">&nbsp;
                                    </div>
                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                   <asp:GridView ID="GridView2" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="30" ShowFooter="false" GridLines="None">
                                       <PagerStyle CssClass="pager" />
            <Columns>
                  <asp:TemplateField HeaderText="Bil" ItemStyle-Width="3%">
                                          <ItemTemplate>
                                             <%# Container.DataItemIndex + 1 %>
                                          </ItemTemplate>
                                       </asp:TemplateField>
                <asp:BoundField DataField="Name" HeaderText="File Name" />
               <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" OnClick="DownloadFile"
                            CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" OnClick="DeleteFile"
                            CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
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
                            
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                <asp:Button ID="Button5" runat="server" class="btn btn-danger" Text="Simpan" UseSubmitBehavior="false" Type="submit" onclick="Button5_Click"  />
                                                        <asp:Button ID="Button7" runat="server" Text="Kemaskini" Visible="false" class="btn btn-danger" UseSubmitBehavior="false" onclick="Button7_Click"  />
                                                       <%--   <asp:Button ID="Button2" runat="server" Text="Set Semula" class="btn btn-default" UseSubmitBehavior="false" OnClick="Button2_Click"  />--%>
                                  
                           </div>
                               </div>
                               </div>
                                  <div class="box-body">&nbsp;
                                    </div>

                            
                                
                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                               <asp:GridView ID="gvSelected" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="30" ShowFooter="false" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging">
                                   <PagerStyle CssClass="pager" />
                                      <Columns>
                                       <asp:TemplateField HeaderText="Bil" ItemStyle-Width="3%">
                                          <ItemTemplate>
                                             <%# Container.DataItemIndex + 1 %>
                                          </ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:BoundField DataField="dis_staff_no" ItemStyle-Width="8%" HeaderText="No Kakitangan"  />
                                        <asp:TemplateField HeaderText="TINDAKAN DISPILIN DIAMBIL" ItemStyle-HorizontalAlign="Left">  
                                            <ItemStyle  Font-Bold Font-Underline/>
                                            <ItemTemplate>  
                                               <asp:LinkButton ID="lblSubItemName"    runat="server" Text='<%# Eval("hr_discipline_desc")%>' CommandArgument=' <%#Eval("dis_discipline_type_cd")+","+ Eval("dis_eff_dt")+","+ Eval("dis_exp_dt")+","+ Eval("dis_file")+","+ Eval("dis_catatan")%>' CommandName="Add"  onclick="lblSubItemName_Click"      >
                                                <a  href="#"></a>
                                                  </asp:LinkButton>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                       <asp:BoundField DataField="dis_eff_dt"  HeaderText="TARIKH"  />
                                       
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
                

                    </div>
                </div>
            </div>
            <!-- /.row -->

        
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
    <%--</ContentTemplate>
          <Triggers>
               <asp:PostBackTrigger ControlID="btnUpload"  />
              <asp:PostBackTrigger ControlID="lnkDownload"  />
           </Triggers>
             
    </asp:UpdatePanel>--%>
</asp:Content>

