<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="Butiran_pembiayaraan.aspx.cs" Inherits="TKH_Butiran_pembiayaraan" %>

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
                        <h1>Butiran WP4 Pembiayaan</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>TKH</a></li>
                            <li class="active">Butiran WP4 Pembiayaan</li>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Batch No </label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList id="DropDownList1" runat="server" class="form-control uppercase"  AutoPostBack="true">
                                                            <asp:ListItem value="0">ALL COA</asp:ListItem>
                                                            <asp:ListItem value="1">KATEGORY AKAUN</asp:ListItem>
                                                            <asp:ListItem value="2">PELANGGAN</asp:ListItem>
                                                           <asp:ListItem value="3">PEMBEKAL</asp:ListItem>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Dijina <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                         <asp:TextBox ID="Tk_mula"  runat="server" class="form-control validate[optional] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Dijina Oleh <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList id="DropDownList2" runat="server" class="form-control uppercase"  AutoPostBack="true">
                                                            <asp:ListItem value="0">ALL COA</asp:ListItem>
                                                            <asp:ListItem value="1">KATEGORY AKAUN</asp:ListItem>
                                                            <asp:ListItem value="2">PELANGGAN</asp:ListItem>
                                                           <asp:ListItem value="3">PEMBEKAL</asp:ListItem>
                                                       </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 
                                 </div>
                         </div>
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Wilayah </label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList id="DropDownList3" runat="server" class="form-control uppercase"  AutoPostBack="true">
                                                            <asp:ListItem value="0">ALL COA</asp:ListItem>
                                                            <asp:ListItem value="1">KATEGORY AKAUN</asp:ListItem>
                                                            <asp:ListItem value="2">PELANGGAN</asp:ListItem>
                                                           <asp:ListItem value="3">PEMBEKAL</asp:ListItem>
                                                       </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            
                           
                          
                         <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                <asp:Button ID="Button4" runat="server" class="btn btn-danger sub_btn" Text="Carian"   UseSubmitBehavior="false" />
                                <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Reset"  UseSubmitBehavior="false" />
                            </div>
                           </div>
                               </div>
                            <br />
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" style="text-align:center;">GRID VIEW </label>
                                    <div class="col-sm-8">
                                      
                                    </div>
                                </div>
                            </div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Status <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList id="DropDownList4" runat="server" class="form-control uppercase"  AutoPostBack="true">
                                                            <asp:ListItem value="0">Lulus</asp:ListItem>
                                                            <asp:ListItem value="1">Gagam</asp:ListItem>
                                                         
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Ulasan <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="TextBox1"  runat="server" class="form-control validate[optional] " ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 
                                 </div>
                         </div>
                              <hr />
                        
                              <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                <asp:Button ID="Button2" runat="server" class="btn btn-danger sub_btn" Text="Hantar"   UseSubmitBehavior="false" />
                                <asp:Button ID="Button3" runat="server" class="btn btn-default" Text="Tutup"  UseSubmitBehavior="false" />
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

