<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Pentadbiran/kw_user_registration_view.aspx.cs" Inherits="kw_user_registration_view" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
      
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server" ScriptMode="Release">
    </asp:ScriptManager>

   
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" style="height: 895px;">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>Senarai Pengguna</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Pentadbiran</a></li>
                            <li class="active">Senarai Pengguna</li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
       <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div class="row">
               <!-- /.col -->
               
                <div class="col-md-12">

                    <div class="box">
                      
                        <div class="box-header">
                             <div class="box-body">&nbsp;</div>
                            <div class="box-body">
                              
                             <%--    <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                   
                                    <div class="col-sm-8">
                                        <div class="col-sm-9">
                                           <div class="input-group">
                                                <span class="input-group-addon" style="background-color:#0090d9; color:#fff;" ><i class="fa fa-search"></i></span>
                                        <asp:TextBox ID="txtSearch" class="form-control" runat="server" OnTextChanged="txtSearch_TextChanged" AutoPostBack="True" placeholder="MASUKKAN NILAI DI SINI"></asp:TextBox>
                                             </div>
                                            </div>
                                          <div class="col-sm-3  text-right">
                                         <asp:Button ID="button4" runat="server" Text="Carian"  class="align-center btn btn-primary" UseSubmitBehavior="false" OnClick="btn_search_Click"></asp:Button>
                                               <asp:Button runat="server" Text="+ Tambah" OnClick="Add_profile"  class="align-center btn btn-primary"></asp:Button>
                                              </div>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>--%>
                                 <div class="row">
                             <div class="col-md-12">
                           
                            <div class="col-md-12 box-body">
                                <div class="form-group">
                                      <div class="col-sm-3 col-xs-12 mob-view-top-padd">
                                           <asp:DropDownList style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional] uppercase" OnSelectedIndexChanged="txtSearch_TextChanged" AutoPostBack="true"
                                                                    ID="DropDownList1">                                                 
                                               <asp:ListItem Value="N">STAFF</asp:ListItem>
                                               <asp:ListItem Value="Y">MEMBER</asp:ListItem>                                               
                                                                </asp:DropDownList>
                                    </div>
                                     <div class="col-sm-3 col-xs-12">
                                       <asp:TextBox ID="txt_srch" runat="server" class="form-control validate[optional]" autocomplete="off" OnTextChanged="txtSearch_TextChanged" AutoPostBack="true" placeholder="Search"></asp:TextBox>
                                    </div>
                                  
                                 <div class="col-sm-3 col-xs-12 mob-view-top-padd" style="text-align:center;">
                                 <asp:Button ID="Button2" runat="server" class="btn btn-primary" UseSubmitBehavior="false" Text="Carian" OnClick="btn_search_Click" />
                                     <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" UseSubmitBehavior="false" OnClick="btn_clr_Click" />                                     
                                     <asp:Button runat="server" Text="+ Tambah" OnClick="Add_profile"  class="align-center btn btn-warning"></asp:Button>
                                     </div>                                   
                                </div>
                            </div>
                                  </div>
                                </div>
                              <hr />
                                <div class="dataTables_wrapper form-inline dt-bootstrap"  style="overflow:auto;">
                                    <%--  <div class="row">--%>
           <div class="col-md-12 box-body">
                                    <asp:GridView  ID="gv_refdata" runat="server" class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvSelected_PageIndexChanging">
                                                        <PagerStyle CssClass="pager" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Hapus" Visible="false" ItemStyle-Width="3%" ControlStyle-CssClass="panel-heading">
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="RadioButton1" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="BIL">
                                                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="User Image" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                                                <ItemTemplate>
                                                                          <asp:Image ID="ImgPrv" class="profile-user-img img-responsive img-circle" runat="server" ImageUrl='<%# "../Files/user/"+Eval("img1") %>' Width="30px" Height="30px" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="User name" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemStyle HorizontalAlign="Left" Width="20%" Font-Bold="true"></ItemStyle>
                                                                <ItemTemplate>
                                                                         <asp:Label ID="Label3" runat="server" Text='<%# Eval("Kk_userid") %>' CssClass="uppercase"></asp:Label>
                                                                         <asp:Label ID="og_genid" Visible="false" runat="server" Text='<%# Eval("Id") %>' CssClass="uppercase"></asp:Label>                                                                    
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Paparan Nama" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
                                                                <ItemTemplate>
                                                                        <asp:Label ID="pap_nam" runat="server" Text='<%# Eval("KK_username") %>' CssClass="uppercase"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Email" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemStyle HorizontalAlign="Left" Width="25%"></ItemStyle>
                                                                <ItemTemplate>
                                                                        <asp:Label ID="email" runat="server" Text='<%# Eval("KK_email") %>' CssClass="uppercase"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="STATUS">
                                                                <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                                                <ItemTemplate>
                                                                        <asp:Label ID="bal_sts" runat="server" Text='<%# Eval("sts") %>' CssClass="uppercase"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Modify">
                                                                                            <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                
                                                                                <asp:LinkButton ID="lnkView" runat="server" OnClick="lnkView_Click" Font-Bold Font-Underline>
                                                                                                   Edit 
                                                                                                </asp:LinkButton>                                                                                                
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
                              <%--  </div>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.col -->
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

