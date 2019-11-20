<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="Sumbakan.aspx.cs" Inherits="TKH_Sumbakan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManagerCalendar" runat="server" ScriptMode="Release">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
         <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
                <div class="content-wrapper" style="height: 895px;">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>SUMBAKAN TKH</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>TKH</a></li>
                            <li class="active">SUMBAKAN TKH</li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
                     
  
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title"></h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                         <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">

                                   <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Wilayah </label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList id="ddwil" runat="server" class="form-control uppercase"  AutoPostBack="true">
                                                            <asp:ListItem value="0">ALL COA</asp:ListItem>
                                                         
                                                       </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"></label>
                                    <div class="col-sm-8">
                                   
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Mula <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                         <asp:TextBox ID="Tk_mula"  runat="server" class="form-control validate[optional] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
                                          <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Akhir <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                       <asp:TextBox ID="Tk_akhir"  runat="server" class="form-control validate[optional] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
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
                                <asp:Button ID="Button4" runat="server" class="btn btn-danger sub_btn" Text="Carian"  OnClick="Button4_Click"   UseSubmitBehavior="false" />
                                <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Reset"   OnClick="btn_reset"  UseSubmitBehavior="false" />
                            </div>
                           </div>
                               </div>
                              <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                      <%--<div class="row" >--%>
           <div class="col-md-12 box-body">
                                   <asp:GridView ID="gv_refdata" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="1000000" >
                                                    <Columns>
                                                    <asp:TemplateField HeaderText="BIL">  
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="No WP4" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" OnClick="lnkView_Click" ID="lnkView" Font-Underline Font-Bold>
                                                                <asp:Label ID="lb_1" runat="server" Text='<%# Eval("tkh_tr_WP4_no") %>'></asp:Label>
                                                                    <asp:Label ID="lbl_rujukan" Visible="false" runat="server" Text='<%# Eval("tkh_tr_WP4_no") %>'></asp:Label>
                                                                    </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tarikh WP4">
                                                            <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_2" runat="server" Text='<%# Eval("tkh_tr_WP4_dt") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tarikh & Lulus" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_3" runat="server" Text='<%# Eval("tkh_tr_WP4_lulus_dt") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>  
                                                        <asp:TemplateField HeaderText="Tarikh Bank in/Post" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_4" runat="server" Text='<%# Eval("tkh_tr_bankin_dt") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Jumlah Pinijaman (RM)" HeaderStyle-HorizontalAlign="Right">
                                                              <ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_5" runat="server" Text='<%# Eval("tkh_tr_WP4_loan_amt","{0:n}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>  
                                                         <asp:TemplateField HeaderText="HTTKS (RM)" HeaderStyle-HorizontalAlign="Right">
                                                              <ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_6" runat="server" Text='<%# Eval("tkh_tr_WP4_httks_amt","{0:n}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>  
                                                        <asp:TemplateField HeaderText="TKH (RM)" HeaderStyle-HorizontalAlign="Right">
                                                              <ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_7" runat="server" Text='<%# Eval("tkh_tr_WP4_tkh_amt","{0:n}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>  
                                                        <asp:TemplateField HeaderText="Baki Hutang (RM)" HeaderStyle-HorizontalAlign="Right">
                                                              <ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_8" runat="server" Text='<%# Eval("tkh_tr_WP4_baki_amt","{0:n}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>  
                                                        <asp:TemplateField HeaderText="Jumlah Bersih (RM)" HeaderStyle-HorizontalAlign="Right">
                                                              <ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_9" runat="server" Text='<%# Eval("tkh_tr_WP4_net_amt","{0:n}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>  
                                                        <asp:TemplateField HeaderText="Jumlah Caj (RM)" HeaderStyle-HorizontalAlign="Right">
                                                              <ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_10" runat="server" Text='<%# Eval("tkh_tr_WP4_caj_amt","{0:n}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField> 
                                                          <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_11" runat="server" Text='<%# Eval("tkh_tr_WP4_stat") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>                                                     
                                                    </Columns>
                                       <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                       <%-- <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />--%>
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                </asp:GridView>
               </div>
          </div>
                               <hr />
                         <div class="row" id="car" runat="server">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                <asp:Button ID="Button2" runat="server" class="btn btn-danger sub_btn" Text="Tuntut Sumbangan"    UseSubmitBehavior="false" />
                                <asp:Button ID="Button3" runat="server" class="btn btn-default" Text="Tutub"   OnClick="btn_tutub"  UseSubmitBehavior="false" />
                            </div>
                           </div>
                               </div>
                            <div class="box-body">&nbsp;
                                    </div>
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align: center; line-height:10px; overflow: auto; line-height:13px; ">
                              
                            </div>
                                 </div>
                         </div>
                            <div class="box-body">&nbsp;</div>
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
   </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

