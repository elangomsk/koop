<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_MUK_view.aspx.cs" Inherits="HR_MUK_view" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

  
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>  MAKLUMAT KAKITANGAN  </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>SUMBER_MANUSIA</a></li>
                            <li class="active"> MAKLUMAT KAKITANGAN  </li>
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
                                 <div class="row">
                                     <div class="box-header with-border">
                            <h3 class="box-title"></h3>
                        </div>

          <%-- <div class="col-md-3 box-body">
                                <div class="form-group">
                                    <div class="col-sm-12">
                                         <div class="input-group">
                                                <span class="input-group-addon" style="background-color:#0090d9; color:#fff;" ><i class="fa fa-search"></i></span>
                                        <asp:TextBox ID="txtSearch" class="form-control" runat="server" OnTextChanged="txtSearch_TextChanged" AutoPostBack="True" placeholder="MASUKKAN NILAI DI SINI"></asp:TextBox>
                                             </div>
                                    </div>
                                   
                                </div>
                            </div>--%>
                                    <div class="col-md-12 box-body">
                                <div class="form-group">
                                     <div class="col-sm-12" style="text-align:right;">
                                       <%-- <asp:Button ID="button4" runat="server" Text="Carian"  class="align-center btn btn-primary" UseSubmitBehavior="false" OnClick="btn_search_Click"></asp:Button>--%>
                                         <asp:Button ID="Button3" runat="server" class="btn btn-danger" Text="Hapus" UseSubmitBehavior="false" OnClick="btn_hups_Click" />
                                         <asp:Button runat="server" Text="+ Tambah" OnClick="Add_profile"  class="align-center btn btn-primary"></asp:Button>
                                    </div>
                                </div>
                            </div>
      </div> 
                                <div class="box-body">&nbsp;
                                    </div>
                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                   <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="Small" Width="100%" AllowPaging="true" PageSize="1000000">

                                        <Columns>

                                         <asp:TemplateField HeaderText="BIL">  
                                         <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="150"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TARIKH MULA">  
                                                <ItemStyle HorizontalAlign="center" Width="7%" Font-Underline></ItemStyle>
                                            <ItemTemplate>  
                                             <asp:LinkButton runat="server" ID="lnkView" OnClick="lnkView_Click">
                                                 <asp:Label ID="TM" runat="server"  Text='<%# Eval("pos_start_dt","{0:dd/MM/yyyy}") %>'></asp:Label> 
                                                  </asp:LinkButton> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="TARIKH TAMAT">  
                                                <ItemStyle HorizontalAlign="center" Width="7%"></ItemStyle>
                                            <ItemTemplate>  
                                                 <asp:Label ID="TT" runat="server"  Text='<%# Eval("pos_end_dt","{0:dd/MM/yyyy}") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="SUBJEK">  
                                                <ItemStyle HorizontalAlign="center" Width="7%"></ItemStyle>
                                            <ItemTemplate>  
                                                 <asp:Label ID="lbl_sub" runat="server"  Text='<%# Eval("pos_subjek") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JAWATAN"> 
                                                <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>  
                                            <ItemTemplate>  
                                                <asp:Label ID="Label3" runat="server"  Text='<%# Eval("hr_jaw_desc") %>'></asp:Label>  
                                                <asp:Label ID="stafno" Visible="false" runat="server"  Text='<%# Eval("pos_staff_no") %>'></asp:Label>  
                                                <asp:Label ID="cdt" Visible="false" runat="server"  Text='<%# Eval("pos_start_dt") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="GRED">  
                                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle> 
                                            <ItemTemplate>  
                                                <asp:Label ID="gred" runat="server" Text='<%# Eval("pos_grade_cd") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UNIT">   
                                                <ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="unit" runat="server" Text='<%# Eval("hr_unit_desc") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JABATAN">   
                                                <ItemStyle HorizontalAlign="Left" Width="30%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="jabatan" runat="server" Text='<%# Eval("hr_jaba_desc") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                               <%-- <asp:TemplateField HeaderText="ORGANISASI">   
                                                <ItemStyle HorizontalAlign="Left" Width="35%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="org" runat="server" Text='<%# Eval("org_name") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>--%>
                                        </Columns>
                                    </asp:GridView>

               </div>
          </div>
                            <%--    </div>--%>
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










