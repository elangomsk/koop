<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Kewengan/kw_penalysian_bank.aspx.cs" Inherits="kw_penalysian_bank" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       <script type="text/javascript">
         function addTotal_bk1() {

             var amt1 = Number($("#<%=TextBox4.ClientID %>").val().replace(",", ""));
             //var amt2 = Number($("#<%=TextBox1.ClientID %>").val().replace(",", "").replace("(", "-").replace(")", ""));
             //var amt3 = amt1 + amt2;
             $(".ss1").val(addCommas(amt1.toFixed(2)));
             //$(".ss2").val(addCommas(amt3.toFixed(2)));
         }

       
          function addCommas(x) {
              var parts = x.toString().split(".");
              parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
              return parts.join(".");
          }
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
                            <div class="col-md-6 box-body" id="edit_kakaun1" runat="server">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl5" runat="server"></asp:Label><span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="dd_bank" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" OnSelectedIndexChanged="sel_bank" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl6" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox3" runat="server" class="form-control validate[optional] uppercase" ReadOnly="true"></asp:TextBox>
                                        <asp:TextBox ID="txt_id" runat="server" class="form-control validate[optional] uppercase" Visible="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl9" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                        <asp:TextBox ID="Tk_mula"  runat="server" class="form-control validate[optional] datepicker mydatepickerclass" autocomplete="off" placeholder="DD/MM/YYYY"></asp:TextBox>
                                             <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl11" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                        <asp:TextBox ID="Tk_akhir"  runat="server" class="form-control validate[optional] datepicker mydatepickerclass" autocomplete="off" placeholder="DD/MM/YYYY"></asp:TextBox>
                                             <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                 
                                 </div>
                         </div>
                             <div class="row" style="display:none;">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body"  id="edit_kakaun2" runat="server">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl7" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                       <asp:TextBox ID="TextBox2" runat="server" class="form-control validate[optional] datepicker mydatepickerclass" autocomplete="off" placeholder="DD/MM/YYYY"></asp:TextBox>
                                             <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl8" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox4" runat="server" style="text-align:right;" class="form-control validate[optional] uppercase ss1" onblur="addTotal_bk1(this)" OnTextChanged="txt_bapbamt" AutoPostBack="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                            
                            <div class="row" style="display:none;">
                             <div class="col-md-12">
                          
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl12" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox6" runat="server" style="text-align:right; color:red;" class="form-control validate[optional] uppercase ss2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                         <div class="row" style="display:none;">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl10" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox5" runat="server" style="text-align:right;" class="form-control validate[optional]"></asp:TextBox>
                                        <asp:TextBox ID="TextBox1" runat="server" style="text-align:right; display:none;" class="form-control validate[optional]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"></label>
                                    <div class="col-sm-8">
                                       <label><asp:CheckBox ID="zero_bal" runat="server" /> Checked</label>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                  <asp:Button ID="Button2" runat="server" class="btn btn-danger" Text="Senarai" OnClick="clk_submit" UseSubmitBehavior="false" />
                                <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" UseSubmitBehavior="false" OnClick="Button5_Click" />
                                <asp:Button ID="Button5" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" OnClick="Click_bck" />
                              <%--  <asp:Button ID="Button3" runat="server" class="btn btn-primary" Text="Cetak" OnClick="clk_prnt" seSubmitBehavior="false" />--%>
                                 
                            </div>
                           </div>
                               </div>
                            
                            <div class="box-body">&nbsp;</div>   
                             <div class="box-header with-border">
                            <h3 class="box-title">Not Assigned Transactions</h3>
                        </div> 
                            <div class="box-body">&nbsp;</div>  
                             <div class="dataTables_wrapper form-inline dt-bootstrap" style=" overflow-y:auto; height:420px;">                            
           <div class="col-md-12 box-body">
                <div class="row">
                             <div class="col-md-12">
                                
                            <div class="col-md-8 box-body">
                                <div class="form-group">
                                   
                                    <div class="col-sm-12">
                                        <div class="col-sm-10">
                                           <div class="input-group">
                                                <span class="input-group-addon" style="background-color:#1157A5; color:#fff;" ><i class="fa fa-search"></i></span>
                                        <asp:TextBox ID="txtSearch" class="form-control" runat="server" OnTextChanged="txtSearch_TextChanged" AutoPostBack="True" placeholder="MASUKKAN NILAI DI SINI"></asp:TextBox>
                                             </div>
                                            </div>
                                          <div class="col-sm-2">
                                         <asp:Button ID="button7" runat="server" Text="Carian"  class="align-center btn btn-primary" UseSubmitBehavior="false" OnClick="btn_search_Click"></asp:Button>
                                              </div>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-4 box-body">
                                      &nbsp;
                                      </div>
                                 </div>
                                </div>
               <div class="box-body">&nbsp;</div> 
                                   <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="20" OnPageIndexChanging="gvSelected_PageIndexChanging"
                                       OnRowDataBound="GridView1_RowDataBound">
                                                    <Columns>
                                                    <asp:TemplateField HeaderText="BIL">  
                                                         <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" /> 
                                                <asp:Label ID="lbl_id" Visible="false" runat="server" Text='<%# Bind("Id") %>' CssClass="uppercase"></asp:Label>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                        <asp:BoundField DataField="pos_dt" HeaderText="Tarikh Transaksi" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150">
               <ItemStyle Width="100px" />
               </asp:BoundField>
                                                         <asp:BoundField DataField="cek" HeaderText="No Batch / Cek" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150">
               <ItemStyle Width="100px" />
               </asp:BoundField>
                                                        <asp:BoundField DataField="ket" HeaderText="KETERANGAN"
                   ItemStyle-Width="150">
               <ItemStyle Width="100px" />
               </asp:BoundField>
                
                                                         <asp:BoundField DataField="resit" HeaderText="TERIMAAN (RM)" DataFormatString="{0:n}"
                   ItemStyle-Width="150">
               <ItemStyle Width="100px" HorizontalAlign="Right" />
               </asp:BoundField>
                                                         <asp:BoundField DataField="pv" HeaderText="PEMBAYARAN (RM)" DataFormatString="{0:n}"
                   ItemStyle-Width="150">
               <ItemStyle Width="100px"  HorizontalAlign="Right" />
               </asp:BoundField>
                                                                                                           
                                                          <asp:TemplateField HeaderText="ASSIGNED">
                                                              <ItemStyle HorizontalAlign="center" Width="3"></ItemStyle>
                                                               <HeaderTemplate>
                                                                           ASSIGNED<br/>
                                            <asp:CheckBox ID="chkAll" runat="server" Text="" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged"
                                                        ItemStyle-Width="150" />
                                            </HeaderTemplate>  
                                                            <ItemTemplate>
                                                               <asp:CheckBox ID="drd_ckbox" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />                                                                 
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
                            <br />
                            <div class="row" >
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">&nbsp;</div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-5 control-label">Baki Akhir Penyata Bank (RM) <span class="style1">*</span> </label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="TextBox7" runat="server" style="text-align:right;" class="form-control validate[optional]" OnTextChanged="baki_txt_changed" AutoPostBack="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                           
                                 </div>
                         </div>
                             <div class="row" >
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">&nbsp;</div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-5 control-label">Baki Transaksi (RM) </label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="TextBox9" runat="server" style="text-align:right;" ReadOnly="true" class="form-control validate[optional]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                           
                                 </div>
                         </div>
                             <div class="row" >
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">&nbsp;</div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-5 control-label" style="color:red;">Perbazaan (RM) </label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="TextBox11" runat="server" style="text-align:right;" ReadOnly="true" class="form-control validate[optional]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                           
                                 </div>
                         </div>
                             <div class="box-body">&nbsp;</div>  
                             <div class="box-header with-border">
                            <h3 class="box-title">Assigned Transactions</h3>
                        </div>   
                            <div class="box-body">&nbsp;</div>  
                             <div class="dataTables_wrapper form-inline dt-bootstrap" style=" overflow-y:auto; height:420px;">                            
           <div class="col-md-12 box-body">
                                   <asp:GridView ID="GridView2" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="20" OnPageIndexChanging="gvSelected_PageIndexChanging_na"
                                       OnRowDataBound="GridView1_RowDataBound_na">
                                                    <Columns>
                                                    <asp:TemplateField HeaderText="BIL">  
                                                         <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" /> 
                                                <asp:Label ID="lbl_id_na" Visible="false" runat="server" Text='<%# Bind("Id") %>' CssClass="uppercase"></asp:Label>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                        <asp:BoundField DataField="pos_dt" HeaderText="Tarikh Transaksi" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150">
               <ItemStyle Width="100px" />
               </asp:BoundField>
                                                         <asp:BoundField DataField="cek" HeaderText="No Batch / Cek" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150">
               <ItemStyle Width="100px" />
               </asp:BoundField>
                                                        <asp:BoundField DataField="ket" HeaderText="KETERANGAN"
                   ItemStyle-Width="150">
               <ItemStyle Width="100px" />
               </asp:BoundField>
                
                                                         <asp:BoundField DataField="resit" HeaderText="TERIMAAN (RM)" DataFormatString="{0:n}"
                   ItemStyle-Width="150">
               <ItemStyle Width="100px" HorizontalAlign="Right" />
               </asp:BoundField>
                                                         <asp:BoundField DataField="pv" HeaderText="PEMBAYARAN (RM)" DataFormatString="{0:n}"
                   ItemStyle-Width="150">
               <ItemStyle Width="100px"  HorizontalAlign="Right" />
               </asp:BoundField>
                                                                                                           
                                                          <asp:TemplateField HeaderText="ASSIGNED">
                                                              <ItemStyle HorizontalAlign="center" Width="3"></ItemStyle>
                                                               <HeaderTemplate>
                                                                           ASSIGNED<br/>
                                            <asp:CheckBox ID="chkAll_na" runat="server" Text="" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged_na"
                                                        ItemStyle-Width="150" />
                                            </HeaderTemplate>  
                                                            <ItemTemplate>
                                                               <asp:CheckBox ID="drd_ckbox_na" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged_na" />                                                                 
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
                            <br />
                             <div class="row" >
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">&nbsp;</div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-5 control-label">Jumlah Penyesuaian (RM) </label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="TextBox8" runat="server" style="text-align:right;" ReadOnly="true" class="form-control validate[optional]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                           
                                 </div>
                         </div>
           <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                               <asp:Button ID="Button4" runat="server" class="btn btn-warning" OnClick="clk_sahkan" Visible="false" Text="Simpan" UseSubmitBehavior="false" />  
                                <asp:Button ID="Button6" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" OnClick="Click_bck" />
                            </div>
                           </div>
                               </div>
                            <div class="box-body">&nbsp;</div>
                             <div class="row">
                                                        <div class="col-md-12 col-sm-2" style="text-align: center; line-height:10px; overflow: auto; line-height:13px;">
                                                            <rsweb:ReportViewer ID="Rptviwerlejar" runat="server" width="100%" Height="100%" SizeToReportContent="True"></rsweb:ReportViewer>
                                                            <asp:Label runat="server" ID="ReportErrorMessage" Visible="false" CssClass="report-error-message"></asp:Label>
                                                        </div>
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

