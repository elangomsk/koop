<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="Butiran_Tuntutan.aspx.cs" Inherits="TKH_Butiran_Tuntutan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server" >
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" style="height: 895px;">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>Penerimaan</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Kewangan</a></li>
                            <li class="active">Butiran Tuntutan  </li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
       <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                       
                        <!-- /.box-header -->
                        <!-- form start -->
                         <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                         <div class="col-md-12">
                             <fieldset class="col-md-12">
                                                    <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Wilayah <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                          
                                        <asp:TextBox ID="start_dt1" runat="server" class="form-control" ></asp:TextBox>
                                       
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <span class="style1"></span></label>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Cawangan <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                          
                                        <asp:TextBox ID="TextBox1" runat="server"  class="form-control"></asp:TextBox>
                                       
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <span class="style1"></span></label>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Pusat <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                          
                                        <asp:TextBox ID="TextBox2" runat="server" class="form-control" ></asp:TextBox>
                                       
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <span class="style1"></span></label>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">PA <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                          
                                        <asp:TextBox ID="TextBox3" runat="server" class="form-control" ></asp:TextBox>
                                       
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Terima <span class="style1"></span></label>
                                    <div class="col-sm-8">
                                           
                                          <asp:TextBox ID="TextBox4" runat="server" class="form-control" ></asp:TextBox>
                                      
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>    
                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">PC/PPC <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                          
                                        <asp:TextBox ID="TextBox6" runat="server" class="form-control" ></asp:TextBox>
                                       
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Terima <span class="style1"></span></label>
                                    <div class="col-sm-8">
                                           
                                          <asp:TextBox ID="TextBox7" runat="server" class="form-control" ></asp:TextBox>
                                      
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">PW/PPW <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                          
                                        <asp:TextBox ID="TextBox8" runat="server" class="form-control" ></asp:TextBox>
                                       
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Terima <span class="style1"></span></label>
                                    <div class="col-sm-8">
                                           
                                          <asp:TextBox ID="TextBox9" runat="server" class="form-control" ></asp:TextBox>
                                      
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>                              
                                                 </fieldset>
                             <div class="box-body">&nbsp;</div>
                                                  <%--  <div class="panel" style="width: 100%;">--%>
                                                        <div id="Div1" class="nav-tabs-custom" role="tabpanel" runat="server">
                                                            <!-- Nav tabs -->
                                                            <ul class="s1 nav nav-tabs" role="tablist">
                                                            <li id="pp6" runat="server" class="active"><a href="#ContentPlaceHolder1_p6" aria-controls="p6" role="tab" data-toggle="tab"><strong>Si Mati/Pesakit</strong></a>
                                                               
                                                            </li>
                                                                <li id="pp1" runat="server"><a href="#ContentPlaceHolder1_p1" aria-controls="p1" role="tab" data-toggle="tab"><strong>Penerima Manfaat</strong></a></li>
                                                                 <li id="pp2" runat="server"><a href="#ContentPlaceHolder1_p2" aria-controls="p2" role="tab" data-toggle="tab"><strong>Maklumat Akaun</strong></a></li>
                                                                  <li id="pp3" runat="server"><a href="#ContentPlaceHolder1_p3" aria-controls="p3" role="tab" data-toggle="tab"><strong>Maklumat Pembiayaan</strong></a></li>
                                                                 <li id="pp4" runat="server"><a href="#ContentPlaceHolder1_p4" aria-controls="p4" role="tab" data-toggle="tab"><strong>Kelulusan</strong></a></li>
                                                            </ul>
                                                            <!-- Tab panes -->
                                                            <div class="tab-content">
                                                            <div role="tabpanel" class="tab-pane active" runat="server" id="p6">
                                                                 <div class="col-md-12 table-responsive uppercase"  style="overflow:auto; padding-top:20px;">
                                                                     <div id="Div3"  runat="server">
                                                                    <div id="Div9"  runat="server">
                                                                      
                                                    
                                                                
                                                                    <div class="box-body">&nbsp;</div>
                                                                         
                                                                        </div>
                                                                   <div id="Div7" role="tabpanel11" runat="server">
                                                                        <div class="row">
                                                                         <div class="col-md-12">
                                                                        <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label">Nama  <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                     <asp:TextBox ID="TextBox10" runat="server" class="form-control" ></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label">No Ahli <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                   <asp:TextBox ID="txtnoinvois1" runat="server" class="form-control"></asp:TextBox> 
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                            
                                                                            
                                                                             </div>
                                                                     </div>
                                                                  
                                                                     <div class="row">
                                                                         <div class="col-md-12">
                                                                        <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label">Alamat <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                     <asp:TextBox ID="TextBox12" runat="server" class="form-control" ></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label">No IC <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                     <asp:TextBox ID="TextBox14" runat="server" class="form-control" ></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             </div>
                                                                     </div>
                                                                        <div class="row">
                                                                         <div class="col-md-12">
                                                                        <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label">No IC Lama <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                    <asp:TextBox ID="TextBox16" runat="server" class="form-control" ></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label"> Sebab Kematian<span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                      <asp:TextBox ID="TextBox19" runat="server" class="form-control" ></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             </div>
                                                                     </div>
                                                                       <div class="row">
                                                                         <div class="col-md-12">
                                                                        <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label">No Sijil Mati <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                    <asp:TextBox ID="TextBox22" runat="server" class="form-control" ></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             
                                                                             </div>
                                                                     </div>
                                                                            <div class="box-body">&nbsp;</div>
                                                                       
                                                                       
                                                                      
                                                                </div>                                                                     
                                                                </div>
                                                                     </div>
                                                                </div>
                                                                <div role="tabpanel12" class="tab-pane" runat="server" id="p1">   
                                                                     <div class="col-md-12 table-responsive uppercase"  style="overflow:auto; padding-top:20px;">                                                                 
                                                                    
                                                                    <div class="box-body">&nbsp;</div>
                                                                          <div class="dataTables_wrapper form-inline dt-bootstrap" >
                                      <div class="row" style="overflow:auto;">
           <div class="col-md-12 box-body">
                                                                        
                                                                   </div>
               </div>
                                          </div>
                                                                         

                                                                   </div> 
                                                                    <div id="Div10" class="nav-tabs-custom" role="tabpanel12" runat="server">
                                                                   <div class="row">
                                                                         <div class="col-md-12">
                                                                        <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label">Nama  <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                     <asp:TextBox ID="TextBox11" runat="server" class="form-control" ></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label">No Ahli <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                   <asp:TextBox ID="TextBox15" runat="server" class="form-control"></asp:TextBox> 
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                            
                                                                            
                                                                             </div>
                                                                     </div>
                                                                  
                                                                     <div class="row">
                                                                         <div class="col-md-12">
                                                                        <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label">Alamat <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                     <asp:TextBox ID="TextBox17" runat="server" class="form-control" ></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label">No IC <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                     <asp:TextBox ID="TextBox23" runat="server" class="form-control" ></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             </div>
                                                                     </div>
                                                                        <div class="row">
                                                                         <div class="col-md-12">
                                                                        <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label">No IC Lama <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                    <asp:TextBox ID="TextBox24" runat="server" class="form-control" ></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                               <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label">Hubungan Dengan  <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                    <asp:TextBox ID="TextBox26" runat="server" class="form-control" ></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             </div>
                                                                     </div>
                                                                       
                                                                            <div class="box-body">&nbsp;</div>
                                                            </div>
                                                                   </div>
                                                                  
                                                                     <div role="tabpanel2" class="tab-pane" runat="server" id="p2">
                                                                          <div class="col-md-12 table-responsive uppercase"  style="overflow:auto; padding-top:20px;">
                                                                    <div id="th1" runat="server">
                                                                                         
                                                                <div class="row">
                                                                         <div class="col-md-12">
                                                                        <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label">Nama Bank <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                     <asp:TextBox ID="TextBox18" runat="server" class="form-control" ></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label">Nama Bank <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                   <asp:TextBox ID="TextBox25" runat="server" class="form-control"></asp:TextBox> 
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                            
                                                                            
                                                                             </div>
                                                                     </div>
                                                                  
                                                                     <div class="row">
                                                                         <div class="col-md-12">
                                                                        <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label">No Akaun Bank <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                     <asp:TextBox ID="TextBox27" runat="server" class="form-control" ></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                              <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label">No Akaun Bank <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                     <asp:TextBox ID="TextBox29" runat="server" class="form-control" ></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             </div>
                                                                     </div>
                                                                        <div class="row">
                                                                         <div class="col-md-12">
                                                                        <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label">Nama <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                    <asp:TextBox ID="TextBox30" runat="server" class="form-control" ></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                               <div class="col-md-4 box-body">
                                                                            <div class="form-group">
                                                                                <label for="inputEmail3" class="col-sm-5 control-label">Nama  <span class="style1">*</span></label>
                                                                                <div class="col-sm-7">
                                                                                    <asp:TextBox ID="TextBox31" runat="server" class="form-control" ></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                             </div>
                                                                     </div>
                                                                       
                                                                            <div class="box-body">&nbsp;</div>
                                             
                                                                   
                                                                  </div>
                                                      
                                                                   </div>
                                                                         </div>
                                                                    <div role="tabpanel3" class="tab-pane" runat="server" id="p3">
                                                                         <div class="col-md-12 table-responsive uppercase"  style="overflow:auto; padding-top:20px;">
                                                                    <div id="fr1" runat="server">
                                                                         <asp:GridView ID="gv_refdata" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="1000000" OnRowDataBound = "RowDataBound" >
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="BIL">  
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                                        <ItemTemplate>  
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                                        </ItemTemplate>  
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cawangan" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                           <ItemTemplate>                                                           
                                                                <asp:Label ID="lb_1" runat="server" Text='<%# Eval("tkh_tt_area_cd") %>'></asp:Label>                                                            
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="No Akaun P1">
                                                            <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_2" runat="server" Text='<%# Eval("tkh_tt_no_shbt") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Nama Pencarum" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_3" runat="server" Text='<%# Eval("tkh_tt_name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>  
                                                        <asp:TemplateField HeaderText="No.KP" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_4" runat="server" Text='<%# Eval("tkh_tt_ic") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Umur" HeaderStyle-HorizontalAlign="Left">
                                                              <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_5" runat="server" Text='<%# Eval("thk_tt_age") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>  
                                                         <asp:TemplateField HeaderText="Produk" HeaderStyle-HorizontalAlign="Left">
                                                              <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_6" runat="server" Text='<%# Eval("tkh_tt_produk") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>  
                                                        <asp:TemplateField HeaderText="Jumlah Pinjaman (RM)" HeaderStyle-HorizontalAlign="Right">
                                                              <ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_7" runat="server" Text='<%# Eval("tkh_tt_pinjaman_amt","{0:n}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>  
                                                        <asp:TemplateField HeaderText="Tempoh" HeaderStyle-HorizontalAlign="Left">
                                                              <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_8" runat="server" Text='<%# Eval("tkh_tt_tempoh") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>  
                                                        <asp:TemplateField HeaderText="Jumlah Caji (RM)" HeaderStyle-HorizontalAlign="Right">
                                                              <ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_9" runat="server" Text='<%# Eval("tkh_tt_caj_amt","{0:n}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>  
                                                        <asp:TemplateField HeaderText="Jumlah Perlindungan (RM)" HeaderStyle-HorizontalAlign="Right">
                                                              <ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_10" runat="server" Text='<%# Eval("tkh_tt_lindung_amt","{0:n}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Manafaat (RM)" HeaderStyle-HorizontalAlign="Right">
                                                              <ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_11" runat="server" Text='<%# Eval("tkh_tt_manfaat_amt","{0:n}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Jumlah Caruman (RM)" HeaderStyle-HorizontalAlign="Right">
                                                              <ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_12" runat="server" Text='<%# Eval("tkh_tt_caruman_amt","{0:n}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField> 
                                                         <asp:TemplateField HeaderText="HTTKS" HeaderStyle-HorizontalAlign="Left">
                                                              <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_13" runat="server" Text='<%# Eval("tkh_tt_httks") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField> 
                                                          <asp:TemplateField HeaderText="TKH Mula Takaful" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_14" runat="server" Text='<%# Eval("tkh_tt_mula_dt") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>  
                                                        <asp:TemplateField HeaderText="TKH Akhir Takaful" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_15" runat="server" Text='<%# Eval("tkh_tt_akhir_dt") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField> 
                                                        <asp:TemplateField>

    <HeaderTemplate>

      <asp:CheckBox ID="checkAll" runat="server" onclick = "checkAll(this) ;"   />

    </HeaderTemplate>

   <ItemTemplate>

     <asp:CheckBox ID="CheckSin"  runat="server"  onclick = "Check_Click(this)"    />

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
                                                                    
                                                                          <div class="box-body">&nbsp;</div>
                                              
                                                                    </div>
                                                                  
                                                                   </div>
                                                                      </div>  
                                                                  <div role="tabpanel4" class="tab-pane" runat="server" id="p4">
                                                                         <div class="col-md-12 table-responsive uppercase"  style="overflow:auto; padding-top:20px;">
                                                                    <div id="Div4" runat="server">
                                                                     
                                                                    
                                                                          <div class="box-body">&nbsp;</div>
                                              
                                                                    </div>
                                                                  
                                                                   </div>
                                                                      </div>
                                                                   </div>
                                                                </div>  
                                                       <%-- </div>--%>
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

