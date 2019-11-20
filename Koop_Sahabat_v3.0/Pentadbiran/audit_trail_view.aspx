<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Pentadbiran/audit_trail_view.aspx.cs" Inherits="audit_trail_view" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <script type="text/javascript">
        $(function () {
            $('#gv_refdata').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
                "responsive": true,
                "sPaginationType": "full_numbers"
            });
        });
    </script> 
      
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server" ScriptMode="Release">
    </asp:ScriptManager>

   
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" style="height: 895px;">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>Audit Trail</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Pentadbiran</a></li>
                            <li class="active">Audit Trail</li>
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
                                                       <asp:TextBox ID="TextBox1" runat="server" AutoComplete="off" class="form-control uppercase" placeholder="User Id (or) Name (or) Description"></asp:TextBox>
                                    </div>
                                      <div class="col-sm-3 col-xs-12 mob-view-top-padd">
                                           <div class="input-group">                                                                    
                                                       <asp:TextBox ID="f_date" runat="server" AutoComplete="off" class="form-control uppercase datepicker mydatepickerclass" placeholder="Tarikh Mula"></asp:TextBox>
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                       </div>
                                    </div>
                                     <div class="col-sm-3 col-xs-12">
                                        <div class="input-group">                                                                    
                                                       <asp:TextBox ID="t_date" runat="server" AutoComplete="off" class="form-control uppercase datepicker mydatepickerclass" placeholder="Tarikh Akhir"></asp:TextBox>
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                       </div>
                                    </div>
                                  
                                 <div class="col-sm-3 col-xs-12 mob-view-top-padd" style="text-align:center;">
                                 <asp:Button ID="Button2" runat="server" class="btn btn-primary" UseSubmitBehavior="false" Text="Carian" OnClick="btn_search_Click" />
                                     <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" UseSubmitBehavior="false" OnClick="btn_clr_Click" />
                                     <asp:Button ID="Button3" runat="server" class="btn btn-warning" UseSubmitBehavior="false" Text="Jana" OnClick="btnExport_Click" />
                                     </div>                                   
                                </div>
                            </div>
                                  </div>
                                </div>
                              <hr />
                                <div class="dataTables_wrapper form-inline dt-bootstrap"  style="overflow:auto;">                                    
           <div class="col-md-12 box-body">
                                  
               <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>    
               </div>
          </div>                              
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.col -->
            </div>
            <!-- /.row -->
            </ContentTemplate>
           <Triggers>            
             <asp:PostBackTrigger ControlID="Button3"  />
                 </Triggers>
    </asp:UpdatePanel>
        
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
   
</asp:Content>

