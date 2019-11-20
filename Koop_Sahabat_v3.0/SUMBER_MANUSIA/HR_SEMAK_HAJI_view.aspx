<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_SEMAK_HAJI_view.aspx.cs" Inherits="HR_SEMAK_HAJI_view" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <asp:ScriptManager ID="ScriptManagerCalendar" ScriptMode="Release" runat="server">
    </asp:ScriptManager>

  
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>  MAKLUMAT PENGGAJIAN      </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>SUMBER_MANUSIA</a></li>
                            <li class="active"> SENARAI MAKLUMAT GAJI  </li>
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
                                   <asp:GridView ID="GridView1" style="text-transform:uppercase;" runat="server" AutoGenerateColumns="false"  
                                        CssClass="Grid" CellSpacing="2" Height="100%" Width="100%"
                                        DataKeyNames="stf_name" >
                                        <Columns>
                                         <asp:TemplateField HeaderText="BIL">
                                         <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                            ItemStyle-Width="150" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                              <asp:TemplateField HeaderText="NO KAKITANGAN" ItemStyle-Width="200px">  
                                            <ItemTemplate>  
                                                <asp:Label ID="labstf" runat="server" Text='<%# Bind("inc_staff_no") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                
                                          <%--  <asp:BoundField ItemStyle-Width="150px" DataField="inc_staff_no" HeaderText="NO KAKITANGAN" />--%>
                                        
                                          
                                          <asp:TemplateField HeaderText="NAMA" ItemStyle-Width="200px">  
                                          <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="labsal" runat="server" Text='<%# Bind("stf_name") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>

                                             <asp:TemplateField HeaderText="NO. KAD PENGANALAN" ItemStyle-Width="200px">  
                                              <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="labot_bonus" runat="server" Text='<%# Bind("stf_icno") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="NAMA BANK" ItemStyle-Width="200px">  
                                              <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="labot_kpi" runat="server" Text='<%# Bind("Bank_Name") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                              <asp:TemplateField HeaderText="NO. AKAUN" ItemStyle-Width="200px">  
                                              <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="labot" runat="server" Text='<%# Bind("stf_bank_acc_no") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Jumlah(RM)" ItemStyle-Width="200px">  
                                              <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="labot_ded" runat="server" Text='<%# Bind("namt","{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                          
                                      
                                          <%-- <asp:TemplateField>
        <HeaderTemplate>
          <asp:CheckBox ID="checkAll" runat="server" onclick = "checkAll(this);" />
        </HeaderTemplate> 
       <ItemTemplate>
         <asp:CheckBox ID="CheckBox1" runat="server" onclick = "Check_Click(this)" />
       </ItemTemplate> 
    </asp:TemplateField> --%>
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








