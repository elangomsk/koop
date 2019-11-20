<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Kewengan/kw_sebut_harga.aspx.cs" Inherits="kw_sebut_harga" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
         <script type="text/javascript">
         $().ready(function () {
             var today = new Date();
             var preYear = today.getFullYear() - 1;
             var curYear = today.getFullYear() - 0;
          
             $('.datepicker2').datepicker({
                 format: 'dd/mm/yyyy',
                 autoclose: true,
                 inline: true,
                 startDate: new Date($("#<%=TextBox1.ClientID %>").val()),
                 endDate: new Date($("#<%=TextBox6.ClientID %>").val())
             }).on('changeDate', function (ev) {
                 (ev.viewMode == 'days') ? $(this).datepicker('hide') : '';
             });
            
         });

     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server" ScriptMode="Release">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1><asp:Label ID="ps_lbl1" runat="server"></asp:Label></h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i><asp:Label ID="ps_lbl2" runat="server"></asp:Label></a></li>
                            <li class="active"><asp:Label ID="ps_lbl3" runat="server"></asp:Label></li>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl5" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="ddpela" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl6" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="txtnsh" runat="server" class="form-control uppercase"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                           
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl7" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                        <asp:TextBox ID="txtdate" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY" ></asp:TextBox> 
                                             <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            <asp:TextBox ID="TextBox1" runat="server" Visible="false" class="form-control uppercase"></asp:TextBox> 
                                            <asp:TextBox ID="TextBox6" runat="server" Visible="false" class="form-control uppercase"></asp:TextBox> 
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl8" runat="server"></asp:Label><span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                         <asp:TextBox ID="txtdate1" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker2 mydatepickerclass" placeholder="DD/MM/YYYY" ></asp:TextBox> 
                                             <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                           <asp:CheckBox ID="set_hingga" runat="server" AutoPostBack="true"/>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl9" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="txttajuk" runat="server" class="form-control uppercase"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                             <div class="box-body">&nbsp;
                                    </div>
                                
              <div class="col-md-12 table-responsive uppercase" style="overflow:auto;"> 
                                   <asp:gridview ID="Gridview2" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="20" ShowFooter="true" onrowdatabound="Gridview2_RowDataBound" >
            <Columns>
            
            <asp:TemplateField HeaderText="Produk / Servis" ItemStyle-Width="25%" >
                <ItemTemplate>
                   <%-- <asp:TextBox ID="TextBox1" runat="server" class="form-control uppercase"></asp:TextBox>--%>
                     <asp:DropDownList ID="ddkod1" Width="100%" style="font-size:13px;" runat="server" class="form-control select2 validate[optional]" onselectedindexchanged="ddgstdeboth_SelectedIndexChanged" AutoPostBack="true" >    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Deskripsi" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" class="form-control uppercase"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Kuantiti/Jam" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox3"  class="form-control uppercase" OnTextChanged="QtyChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Harga/Kadar (RM)" ItemStyle-HorizontalAlign="Center" >
                <ItemTemplate >
                    <asp:TextBox ID="TextBox4" style="text-align:right;" runat="server"  class="form-control uppercase"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Diskaun (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox5" runat="server" OnTextChanged="QtyChanged2" AutoPostBack="true" class="form-control uppercase"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Kod Akaun" ItemStyle-Width="25%">
                 <ItemTemplate>
                     <asp:DropDownList ID="ddkod" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" >    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Cukai (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox7" style="text-align:right;" OnTextChanged="QtyChanged1" AutoPostBack="true"  CssClass="form-control" placeholder="0"   runat="server"></asp:TextBox>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:TextBox ID="txtTotalr1"   CssClass="form-control"  placeholder="0" style="text-align:right; font-weight:bold;" ReadOnly="true" runat="server" ></asp:TextBox>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Jumlah (RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                     <asp:TextBox ID="TextBox8" style="text-align:right;" CssClass="form-control" placeholder="0.00"  runat="server"></asp:TextBox>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                 <asp:TextBox ID="txtTotal1"  CssClass="form-control"  placeholder="0" style="text-align:right; font-weight:bold;" ReadOnly="true" runat="server" ></asp:TextBox>
                </FooterTemplate>
            </asp:TemplateField>
           
            </Columns>
         <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                       <%-- <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />--%>
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />
        </asp:gridview>
              
                                    
            <div class="box-body">&nbsp;</div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">&nbsp;</div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl10" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox14" style="text-align:right;" runat="server" class="form-control"></asp:TextBox> 
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
                                 <asp:TextBox ID="lbl_name" runat="server" class="form-control validate[optional] uppercase" Visible="false"></asp:TextBox>
                                     <asp:Button ID="Button8" runat="server" class="btn btn-danger" Text="Simpan" Type="submit" OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;" onclick="Button2_Click" />
                                     <asp:Button ID="Button1" runat="server" class="btn btn-danger" Text="Kemaskini" Type="submit" Visible="false" OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;" onclick="Button1_Click" />
                                     <asp:Button ID="Button9" runat="server" class="btn btn-default" Text="Set Semula" Type="submit" onclick="Button5_Click"/>
                                 <asp:Button ID="Button2" runat="server" class="btn btn-default" Type="submit" Text="Kembali" UseSubmitBehavior="false" OnClick="Click_bck"/>
                                 
                            </div>
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

