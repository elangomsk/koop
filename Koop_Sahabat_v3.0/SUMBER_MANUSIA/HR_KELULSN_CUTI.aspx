<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_KELULSN_CUTI.aspx.cs" Inherits="HR_KELULSN_CUTI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>  Kelulusan Cuti         </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i><asp:Label ID="ps_lbl2" runat="server"></asp:Label></a></li>
                            <li class="active">   Kelulusan Cuti    </li>
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
                            <h3 class="box-title"> <asp:Label ID="ps_lbl7" runat="server"></asp:Label></h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                              <div class="row">
                             <div class="col-md-12">
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  <asp:Label ID="ps_lbl5" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8  text-right">                                        
                                         <asp:label ID="Label4" runat="server"></asp:label>
                                                        
                                             
                                    </div>
                                </div>
                            </div>
                             <div class="col-md-6 box-body leave-application">
                                     <div class="" id="shw_blnce" runat="server">
                                        <asp:Label ID="get_blnce" runat="server"></asp:Label>
                                    </div>
                                  <a href="#" data-toggle="modal" data-target="#myModal" style="text-decoration:underline; font-weight:bold;">(Click Here for More Leave Information)</a>
                            </div>
                           
                                 </div>
                                </div>
                              <div class="row">
                             <div class="col-md-12">
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  <asp:Label ID="ps_lbl6" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8  text-right">                                        
                                         <asp:label ID="Label5" runat="server"></asp:label>
                                                        
                                             
                                    </div>
                                </div>
                            </div>
                                                       
                                 </div>
                                </div>
                           <div id="myModal" class="modal fade" role="dialog">
  <div class="modal-dialog animate" style="width:700px;">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title"><strong>Maklumat Cuti</strong></h4>
      </div>
      <div class="modal-body">
        <div class="box-cal box-primary">
               <div class="box-body no-padding">
                  <asp:GridView ID="GridView2" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging1">
                    <PagerStyle CssClass="pager" />
                                      <Columns>
                                       <asp:TemplateField HeaderText="BIL" >  
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                         <asp:TemplateField HeaderText="JENIS CUTI">
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("hr_jenis_desc") %>' CssClass="uppercase"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CUTI DIBAWA KEHADAPAN">
                                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label2_fl" runat="server" Text='<%# Eval("a","{0:n1}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CUTI LAYAK (TAHUNAN)">
                                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("c","{0:n1}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="KELAYAKAN CUTI TERKINI" Visible="false">
                                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Lab_kct" runat="server" Text='<%# Eval("b","{0:n1}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="JUMLAH LAYAK (TAHUNAN)">
                                                             <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Lab_jl" runat="server" Text='<%# Eval("ab","{0:n1}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="CUTI PROSES">
                                                             <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1_cp" runat="server" Text='<%# Bind("e","{0:n1}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CUTI DIAMBIL">
                                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("d","{0:n1}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="BAKI CUTI TERKINI">
                                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("res","{0:n1}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText="LAYAK CUTI PRORATE">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("pro_rate","{0:n1}") %>'></asp:Label>
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
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
      </div>
    </div>

  </div>
</div>

                            <div class="row">
                             <div class="col-md-12">
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl8" runat="server"></asp:Label></label>
                                    <div class="col-sm-8 text-right">
                                        <asp:label ID="TextBox11" runat="server" class="validate[optional] uppercase"></asp:label>
                                                        <asp:TextBox ID="TextBox13" Visible="false" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox14" runat="server" Visible="false" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                              <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  No of Days </label>
                                    <div class="col-sm-8  text-right">                                        
                                         <asp:label ID="Label8" runat="server"></asp:label>
                                                        
                                             
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  <asp:Label ID="ps_lbl9" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8  text-right">                                        
                                         <asp:label ID="TextBox7" runat="server" placeholder="DD/MM/YYYY"></asp:label>
                                                        
                                             
                                    </div>
                                </div>
                            </div>
                                <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl10" runat="server"></asp:Label> </label>
                                                                <div class="col-sm-8  text-right">        
                                                <asp:label ID="TextBox8" runat="server" placeholder="DD/MM/YYYY"></asp:label>
                                         
                                        <asp:TextBox ID="TextBox2" Visible="false" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                                <asp:TextBox ID="TextBox3" Visible="false" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>                                
                            </div>
                                 </div>
                               </div>
                                 </div>
                                 <div class="row" id="sc_id" runat="server">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl11" runat="server"></asp:Label>  </label>
                                    <div class="col-sm-8  text-right">
                                        <asp:label ID="TextBox5" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                                 <div class="row"  id="sbc_id" runat="server">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl12" runat="server"></asp:Label></label>
                                    <div class="col-sm-8   text-right">
                                       <asp:label ID="TextBox9" runat="server" class="uppercase"></asp:label> 
                                                        <asp:TextBox ID="TextBox4" runat="server" Visible="false" class="form-control validate[optional] uppercase"></asp:TextBox>   
                                                         <asp:TextBox ID="TextBox6" runat="server" Visible="false" class="form-control validate[optional] uppercase"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                                 <div class="row" id="dfile_id" style=" display:none;" runat="server">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl13" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8   text-right">
                                       <asp:LinkButton ID="lnkDownload" runat="server" Click = "DownloadFile"></asp:LinkButton> 
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                    <div id="hide_kelulusan" runat="server" visible="false">
                                   <div class="box-header with-border">
                            <h3 class="box-title"><asp:Label ID="ps_lbl14" runat="server"></asp:Label></h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
             

                                     <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl15" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="DropDownList2" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                        </asp:DropDownList> 
                                    </div>
                                </div>
                            </div>
                                    <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl16" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox1" runat="server" class="form-control validate[optional] uppercase" MaxLength="500"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div> 
                                        </div>
                                 
                                  <div class="box-header with-border">
                            <h3 class="box-title" id="h3_tag4" runat="server">Maklumat Kelulusan</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>

                                         <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl11_text" runat="server"> Status Kelulusan <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="DD_Penge" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl12_text" runat="server">  Catatan </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txt_catat" runat="server" class="form-control validate[optional] uppercase" MaxLength="500"></asp:TextBox>
                                                <asp:TextBox ID="TextBox15" runat="server" Visible="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>  
                                   <%-- <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl9_text" runat="server"> Nama Penyelia</label>
                                    <div class="col-sm-8  text-right">
                                         <asp:TextBox ID="txt_peny" runat="server" class="form-control validate[optional] uppercase" ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl10_text" runat="server"> Tarikh Kelulusan</label>
                                    <div class="col-sm-8 text-right">
                                                <asp:TextBox ID="txt_kelu" runat="server" class="form-control validate[optional] uppercase" ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>        --%>      
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                <asp:TextBox ID="TextBox10" Visible="false" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                <asp:TextBox ID="TextBox16" Visible="false" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                                     <asp:TextBox ID="TextBox12" Visible="false" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                 <asp:TextBox ID="Applcn_no1" runat="server" class="form-control validate[optional] uppercase"
                                                                                MaxLength="10" Visible="false"></asp:TextBox>
                                                        <asp:Button ID="Button2" runat="server" class="btn btn-danger" Text="Simpan" UseSubmitBehavior="false" onclick="Button2_Click" />
                                                        <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" onclick="Click_bck" />
                            </div>
                           </div>
                               </div>
                           <div class="box-body">&nbsp;
                                    </div>

                      

                    </div>
                </div>
            </div>
            <!-- /.row -->

           </ContentTemplate>
              <Triggers>
<asp:PostBackTrigger ControlID="lnkDownload" />
</Triggers>
    </asp:UpdatePanel>
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
 
</asp:Content>



