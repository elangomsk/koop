<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../keanggotan/Tambah_Syer_Tunai1.aspx.cs" Inherits="Tambah_Syer_Tunai1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>  <asp:Label ID="ps_lbl1" runat="server"></asp:Label> </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>  <asp:Label ID="ps_lbl2" runat="server"></asp:Label> </a></li>
                            <li class="active"> <asp:Label ID="ps_lbl3" runat="server"></asp:Label> </li>
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
                                    <label for="inputEmail3" class="col-sm-4 control-label"> <asp:Label ID="ps_lbl5" runat="server"></asp:Label> <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                    <asp:TextBox ID="TextBox12" runat="server"  class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase" MaxLength="12" ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-8">
                                    <asp:Button ID="Button4" runat="server" class="btn btn-primary" Text="Carian" usesubmitbehavior="false" OnClick="btnsrch_Click" />
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                            
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl7" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="TextBox1" runat="server" class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase" ReadOnly></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl8" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="TextBox2" runat="server" class="form-control" ReadOnly></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                              <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl9" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox14" runat="server" class="form-control validate[optional,custom[phone]]" MaxLength="12" ReadOnly></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl10" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="TextBox3" runat="server" class="form-control uppercase" ReadOnly></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><%--<asp:Label ID="ps_lbl11" runat="server"></asp:Label>--%>Cawangan  </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox4" runat="server" class="form-control uppercase" ReadOnly></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl12" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="TextBox5" runat="server" class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase" ReadOnly></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                            <div class="box-header with-border">
                            <h3 class="box-title"><asp:Label ID="ps_lbl13" runat="server"></asp:Label></h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>

                              <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"> <asp:Label ID="ps_lbl14" runat="server"></asp:Label>Jumlah Pembelian Syer (RM) <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox6" runat="server" class="form-control validate[required,custom[number]]" MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"> <asp:Label ID="ps_lbl15" runat="server"></asp:Label> <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="TextBox7" runat="server" class="form-control validate[required] uppercase"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox8" Visible="false" runat="server" class="form-control validate[optional]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"> <asp:Label ID="ps_lbl16" runat="server"></asp:Label> <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                          <textarea id="TextArea1" rows="3" runat="server" class="form-control validate[required] uppercase"></textarea>
                                    </div>
                                </div>
                            </div>

                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"> <asp:Label ID="ps_lbl17" runat="server"></asp:Label>  </label>
                                    <div class="col-sm-8">
                                           <asp:TextBox ID="TextBox9" runat="server" class="form-control validate[optional] uppercase datepicker mydatepickerclass"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox10" runat="server" type="hidden" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                    <asp:Button ID="Button2" runat="server" class="btn btn-danger" Text="Simpan" UseSubmitBehavior="false" OnClick="btnsmmit_Click" />
                                                        <asp:Button ID="Button5" runat="server" Visible="false" class="btn btn-danger" Text="Kemaskini" UseSubmitBehavior="false" OnClick="btnupdt_Click" />
                                                        <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" usesubmitbehavior="false" OnClick="Click_bck" />
                                <asp:Button ID="Button3" runat="server" class="btn btn-default" Visible="false" Text="Kembali" usesubmitbehavior="false" OnClick="Click_bck" />
                                 
                            </div>
                           </div>
                               </div>
                           <div class="box-body">&nbsp;
                                    </div>

                                 <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
         <div class="col-md-12 box-body">
                                     <asp:GridView ID="gvSelected" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="30" ShowFooter="false" GridLines="None" OnPageIndexChanging="GridView1_PageIndexChanging" onrowdatabound="gvEmp_RowDataBound">
                                         <PagerStyle CssClass="pager" />
                                                    <Columns>
                                                     <asp:TemplateField HeaderText="BIL"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TARIKH">
                                                                <ItemStyle HorizontalAlign="CENTER" Width="10%" Font-Bold Font-Underline></ItemStyle>
                                                                    <ItemTemplate>
                                                                    <asp:Label ID="stscd" Visible="false" runat="server" Text='<%# Eval("sha_approve_sts_cd") %>'></asp:Label>  
                                                                    <asp:Label ID="shitem" Visible="false" runat="server" Text='<%# Eval("sha_item") %>'></asp:Label>  
                                                                   <asp:LinkButton ID="lblSubItem" runat="server" Text='<%# Eval("sha_txn_dt")%>' CommandArgument=' <%#Eval("sha_new_icno")+","+ Eval("sha_txn_dt")+","+ Eval("sha_crt_dt")%>' CommandName="Add"  onclick="lblSubItem_Click">
                                                <a  href="#"></a>
                                                  </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                        <asp:BoundField DataField="sha_item" HeaderText="CATATAN" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%"/>
                                                        <asp:BoundField DataField="bname" HeaderText="BATCH ID" ItemStyle-HorizontalAlign="center" ItemStyle-Width="10%"/>
                                                         <asp:TemplateField HeaderText="AMOUNT (RM)" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
                                            
                                            <ItemTemplate>  
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("sha_debit_amt","{0:n}") %>'></asp:Label>  
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

