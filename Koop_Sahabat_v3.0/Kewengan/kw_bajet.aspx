<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Kewengan/kw_bajet.aspx.cs" Inherits="kw_bajet" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        <script type="text/javascript">
         function addTotal_bk1() {

             var amt1 = Number($("#<%=TextBox4.ClientID %>").val().replace(",", ""));

             $(".ss1").val(addCommas(amt1.toFixed(2)));

         }
        function addCommas(x) {
            var parts = x.toString().split(".");
            parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
            return parts.join(".");
        }

       
        $(document).ready(function () {
            $(<%=DropDownList1.ClientID%>).SumoSelect(
               { selectAll: true });
        });
    
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
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
                                       <asp:DropDownList id="dd_kumpulan" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                            <asp:ListItem value="0">--- PILIH ---</asp:ListItem>
                                                            <asp:ListItem value="01">JUMLAH PENDAPATAN</asp:ListItem>
                                                           <asp:ListItem value="02">JUMLAH PERBELANJAAN</asp:ListItem>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl6" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox1" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                                        <asp:TextBox ID="TextBox2" Visible="false" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl7" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="TextBox4" style="text-align:right;" runat="server" class="form-control validate[optional,custom[number]] ss1" onblur="addTotal_bk1(this)"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                           
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl8" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                         <asp:TextBox ID="tk_mula"  runat="server" class="form-control validate[optional] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl9" runat="server"></asp:Label><span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                    <asp:TextBox ID="tk_akhir"  runat="server" class="form-control validate[optional] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl10" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                           <asp:DropDownList id="DropDownList2" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" OnSelectedIndexChanged="bind_gview" AutoPostBack="true">
                                                            <asp:ListItem value="0">ALL COA</asp:ListItem>
                                                            <asp:ListItem value="1">PELANGGAN</asp:ListItem>
                                                           <asp:ListItem value="2">PEMBEKAL</asp:ListItem>
                                                       </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl11" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                         <asp:listbox runat="server" id="DropDownList1" class="form-control uppercase" selectionmode="Multiple">
                                                           </asp:listbox>
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
                                <asp:TextBox ID="ver_id" Visible="false" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                                <asp:TextBox ID="get_id" Visible="false" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                                <asp:Button ID="Button4" runat="server" class="btn btn-danger" Text="Simpan" OnClick="clk_submit" UseSubmitBehavior="false" />
                                <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" UseSubmitBehavior="false" OnClick="Button5_Click" />
                                <asp:Button ID="Button5" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" OnClick="Click_bck" />
                                <asp:Button ID="Button3" runat="server" Visible="false" class="btn btn-warning" Text="Hapus" UseSubmitBehavior="false" OnClick="btn_hups_Click" />
                                 
                            </div>
                           </div>
                               </div>
                             <div class="box-body">&nbsp;
                                    </div>
                           <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" style="overflow:auto;">--%>
           <div class="col-md-12 box-body">
                                 <asp:GridView  ID="gv_refdata" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="1000000" >
                                                    <Columns>
                                                    <asp:TemplateField HeaderText="BIL">  
                                                         <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="KOD AKAUN">
                                                                <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl2" runat="server" Text='<%# Eval("Ref_kod_akaun") %>'></asp:Label>
                                                                <asp:Label ID="Label1" Visible="false" runat="server" Text='<%# Eval("Ref_kat_bajet") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="NAMA AKAUN">
                                                                <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl2_1" runat="server" Text='<%# Eval("nama_akaun") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="Hapus" ItemStyle-Width="3%" ControlStyle-CssClass="panel-heading">
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="RadioButton1" runat="server" />
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

