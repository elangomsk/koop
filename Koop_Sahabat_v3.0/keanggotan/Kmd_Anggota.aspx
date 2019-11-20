<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../keanggotan/Kmd_Anggota.aspx.cs" Inherits="Kmd_Anggota" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server" ScriptMode="Release">
    </asp:ScriptManager>

    <%-- <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>--%>
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1> <asp:Label ID="ps_lbl1" runat="server"></asp:Label> </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i><asp:Label ID="ps_lbl2" runat="server"></asp:Label>   </a></li>
                            <li class="active">   <asp:Label ID="ps_lbl3" runat="server"></asp:Label></li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
      
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title"> <asp:Label ID="ps_lbl4" runat="server"></asp:Label> </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  <asp:Label ID="ps_lbl5" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                           <asp:DropDownList ID="DropDownList1" class="form-control validate[optional]" style="text-transform:uppercase;" runat="server">
                                                                    </asp:DropDownList>
                                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="You can't leave this Empty" CssClass="requiredFieldValidateStyle" ForeColor="Red" ControlToValidate="DropDownList1" ValidationGroup="vgSubmit_kd_simp">
                                    </asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl6" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                           <asp:DropDownList ID="TextBox18" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  </div>
                                </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl7" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                           <asp:TextBox ID="s_icno" runat="server" class="form-control uppercase" MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                     <label for="inputEmail3" class="col-sm-3 control-label"> </label>
                                    <div class="col-sm-8">
                                           <asp:CheckBox ID="s_update" runat="server" CssClass="mycheckbox" Text=" Semua Rekod"/>
                                    </div>
                                </div>
                            </div>
                                  </div>
                                </div>

                              <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  <asp:Label ID="ps_lbl9" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                           
                                         <div class="input-group">
                                                         <asp:TextBox ID="TextBox2" runat="server" class="form-control"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl10" runat="server"></asp:Label> <span class="style1"></span> </label>
                                    <div class="col-sm-8">                                           
                                                                <asp:FileUpload ID="FileUpload1" CssClass="form-control" runat="server" />
                                      <br />
                                          <asp:LinkButton style="text-decoration:underline;" ID="lnkDownload" runat="server" onclick = "DownloadFile" Text="Download Sampel Fail"></asp:LinkButton>
                                                          
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                              <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                <asp:Button ID="Button2" runat="server" class="btn btn-primary" Text="Carian" OnClick="btnserch_Click" />
                                                        <asp:Button ID="Button4" runat="server" class="btn btn-default"  Text="Set Semula" usesubmitbehavior="false" OnClick="Reset_btn"/>
                            </div>
                           </div>
                               </div>
                            <div class="box-body">&nbsp;</div>
                            <div id="shw_cnt1" runat="server" visible ="false">
                                  <div class="box-header with-border">
                            <h3 class="box-title"><asp:Label ID="ps_lbl13" runat="server"></asp:Label>  </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                             <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                    <%--  <div class="row" style="overflow:auto;">--%>
           <div class="col-md-12 box-body">
              <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="50" ShowFooter="false" GridLines="None"  OnRowDataBound="gvUserInfo_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging">
                  <PagerStyle CssClass="pager" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="BIL" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAll" Checked="true" Visible="false" runat="server" Text="&nbsp;BIL" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged"
                                                        ItemStyle-Width="150" />
                                                         <asp:Label ID="bil" Text='BIL' runat="server" ItemStyle-Width="150"/>
                                                </HeaderTemplate>

                                               <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" Visible="false" Checked="true" runat="server" />
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="150"/>
                                            
                                            </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="NO KP" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("div_new_icno") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="NAMA" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("mem_name") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="NO ANGGOTA" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("mem_member_no") %>'>
                                                    </asp:Label>
                                                    <asp:Label ID="Label1" Visible="false" runat="server" Text='<%# Bind("ID") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CAWANGAN" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("mem_branch_cd") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PUSAT" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("mem_centre") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="JUMLAH DIVIDEN(RM)" ItemStyle-Width="10%">
                                            <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("div_debit_amt","{0:n}") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="NO AKAUN BANK" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                <asp:TextBox ID="Label8" class="form-control" MaxLength="20" runat="server" Text='<%# Bind("div_bank_acc_no") %>' /> 
                                            </ItemTemplate>   
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BANK" ItemStyle-Width="10%">   
                                            <ItemTemplate>  
                                                <asp:DropDownList ID="Bank_details" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TARIKH KREDIT BAYARAN" ItemStyle-Width="7%">  
                                                <ItemTemplate> 
                                                    <%--<div class="input-group">--%>
                                                <asp:TextBox ID="Label10" class="form-control datepicker mydatepickerclass" style="width:100%;" placeholder="DD/MM/YYYY" runat="server"  Text='<%# Eval("div_pay_dt", "{0:dd/MM/yyyy}") %>' /> 
                                                          <%--<span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                       </div>--%>
                                                <%--<asp:TextBox ID="Label8" class="form-control" runat="server" Text='<%# Eval("set_txn_dt", "{0:dd/MM/yyyy}") %>' />--%>
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
                                 <asp:Button ID="Button1" runat="server" class="btn btn-danger"  usesubmitbehavior="false" Text="Simpan" onclick="Button1_Click" />
                                                <asp:Button ID="Button3" runat="server" class="btn btn-default"  usesubmitbehavior="false" Text="Batal" />
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

        
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
   <%-- </ContentTemplate>
            
    </asp:UpdatePanel>--%>
</asp:Content>



