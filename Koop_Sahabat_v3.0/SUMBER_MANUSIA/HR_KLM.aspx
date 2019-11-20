<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_KLM.aspx.cs" Inherits="HR_KLM" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">   
     function ValidateEmail(button){

         var row = button.parentNode.parentNode;
         var valuestart = GetChildControl(row, "lbl2").value;
         var valuestop = GetChildControl(row, "lbl3").value;

         var date1 = new Date("01/01/2007 " + valuestart).getTime();
         var date2 = new Date("01/01/2007 " + valuestop).getTime();
        
         var msec = date2 - date1;
        
         var mins = Math.floor(msec / 60000);
         var hrs = Math.floor(mins / 60);
         var days = Math.floor(hrs / 24);
         var yrs = Math.floor(days / 365);         
         mins = mins % 60;         
         hrs = hrs % 24;         
         //alert(("0" + mins).slice(-2));
         GetChildControl(row, "lbl4").value = parseFloat(hrs + "." + ("0" + mins).slice(-2)).toFixed(2);
         GetChildControl(row, "hrs_id").value = hrs;
         GetChildControl(row, "mins_id").value = mins;

         Total_bk = 0;
         Total_bk1 = 0;
         var minutes = 0;

                         
         $(".txtAmount_bk1").each(function () {
            
             if ($(this).val() != '') {
                 Total_bk += parseInt($(this).val());
             }
         });
         $(".txtAmount_bk2").each(function () {

             if ($(this).val() != '') {
                 minutes += parseInt($(this).val());
             }
         });
         var hours = Math.floor(minutes / 60)
         
         var Total_bk1 = minutes % 60;
         var cur_min = (parseFloat(hours + "." + ("0" + Total_bk1).slice(-2)) + Total_bk).toFixed(2); 
       
         $("#<%=gv_refdata.ClientID %> [class*=TotalValue_bk]").val(cur_min);
       
         return false;
     };

     function GetChildControl(element, id) {
         var child_elements = element.getElementsByTagName("*");
         for (var i = 0; i < child_elements.length; i++) {
             if (child_elements[i].id.indexOf(id) != -1) {
                 return child_elements[i];
             }
         }
     };
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

   
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1 id="h1_tag" runat="server"> Jadual Kerja Lebih Masa </h1>
                        <ol class="breadcrumb">
                            <li id="bb1_text" runat="server"><a href="#"><i class="fa fa-dashboard"></i>Sumber Manusia</a></li>
                            <li class="active" id="bb2_text" runat="server"> Jadual KLM </li>
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
                            <h3 class="box-title" id="h3_tag" runat="server"> Carian Jadual Kerja Lebih Masa </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div style="display:none;">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl1_text" runat="server"> No Kakitangan</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txt_stfno" runat="server" class="form-control validate[optional] uppercase" MaxLength="150"></asp:TextBox>
                                                         <asp:TextBox ID="Applcn_no1" runat="server" class="form-control validate[optional] uppercase"
                                                                                MaxLength="150" Visible="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl2_text" runat="server">   Nama Kakitangan</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txt_nama" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl3_text" runat="server"> Gred </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txt_gred" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl4_text" runat="server"> Nama Syarikat / Organisasi</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txt_org" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl5_text" runat="server"> Perniagaan</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox3" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl6_text" runat="server"> Jabatan </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txt_jabat" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl7_text" runat="server"> Jawatan </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="txt_jawa" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                </div>
                          
                               <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body">
                                <div class="form-group">
                                     <div class="col-sm-3 col-xs-12">
                                       <asp:DropDownList id="Tahun_kew" runat="server" class="form-control select2 uppercase" ></asp:DropDownList>
                                    </div>
                                     <div class="col-sm-3 col-xs-12">
                                       <asp:DropDownList ID="DropDownList1" style="width:100%; font-size:13px;" runat="server" OnSelectedIndexChanged="sel_tahun" AutoPostBack="true" class="form-control select2 validate[optional]"></asp:DropDownList>
                                    </div>
                                     <div class="col-sm-2 col-xs-12"> 
                                           <asp:Button ID="btn_simp" runat="server" class="btn btn-danger" Text="Carian" Type="Submit" UseSubmitBehavior="false" OnClick="btn_cari_Click"/>
                                <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" UseSubmitBehavior="false"  OnClick="Button5_Click"   />
                                         </div>
                                     <div class="col-sm-2 col-xs-12"> 
                                         
                                              <asp:DropDownList runat="server" CssClass="form-control select2 uppercase" ID="sel_frmt">
                                                <asp:ListItem Value="01">PDF</asp:ListItem>
                                                <asp:ListItem Value="02">EXCEL</asp:ListItem>
                                               <%-- <asp:ListItem  Value="03">Word</asp:ListItem>--%>
                                                </asp:DropDownList>  
                                         </div>
                                     <div class="col-sm-2 col-xs-12">                                          
                                         <asp:TextBox ID="lbl_name" runat="server" class="form-control validate[optional] uppercase" Visible="false" ></asp:TextBox>
                                         <asp:Button ID="Button3" runat="server" class="btn btn-success" Text="Export" OnClick="ctk_values" UseSubmitBehavior="false"/>
                                    </div>
                                </div>
                            </div>
                                   
                                  
                                 </div>
                                </div>

                 
                            <hr />
                          
                          
                            <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                   <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                      &nbsp;
                                      </div>
                            <div class="col-md-6 box-body  text-right">
                                <div class="col-md-6 box-body">
                                      &nbsp;
                                      </div>
                                    <div class="col-sm-6  text-right">
                                        <div class="col-sm-6 text-right">
                                           <div class="input-group">                                               
                                               <asp:RadioButton ID="rr1_chk" GroupName="war11" runat="server" Text="&nbsp;KLM"  />
                                             </div>
                                            </div>
                                          <div class="col-sm-6  text-right">
                                         <asp:RadioButton ID="rr1_ch2" GroupName="war11" runat="server" Text="&nbsp;Cuti Ganti"  />
                                              </div>
                                    </div>
                                
                            </div>
                                 </div>
                                </div>
              
                                    <asp:GridView ID="gv_refdata" runat="server" CssClass="table datatable dataTable no-footer uppercase" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="31" ShowFooter="true" GridLines="None" AutoGenerateColumns="false" onrowdatabound="gvEmp_RowDataBound">
                                                    <Columns>
                                                    <asp:TemplateField HeaderText="BIL">  
                                                         <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TARIKH">
                                                             <ItemStyle HorizontalAlign="center" Width="5%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl1" runat="server" Text='<%# Eval("date") %>'></asp:Label>                                                              
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MASA MULA">
                                                                <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lbl2" runat="server" CssClass="form-control ss1 timepicker1 validate[optional]" placeholder="00:00 00"  ReadOnly="false" Text='<%# Eval("val1") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MASA AKHIR">
                                                             <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                            <ItemTemplate>
                                                               <asp:TextBox ID="lbl3" runat="server" placeholder="00:00 00" ReadOnly="false" CssClass="form-control ss2 timepicker1 validate[optional]" Text='<%# Eval("val2") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="KETERANGAN">
                                                             <ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
                                                            <ItemTemplate>
                                                               <asp:TextBox ID="lbl5" runat="server" CssClass="form-control uppercase validate[optional]" MaxLength="500" Text='<%# Eval("val3") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="JUMLAH JAM">
                                                             <ItemStyle HorizontalAlign="right" Width="5%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lbl4" runat="server" style="pointer-events:none;" CssClass="form-control txtAmount_bk validate[optional]" Text='<%# Eval("val4") %>'></asp:TextBox>
                                                                <asp:Label ID="hrs_id" CssClass="form-control txtAmount_bk1 validate[optional]" runat="server" style="display:none;" Text='<%# Eval("val5") %>'></asp:Label>
                                                                <asp:Label ID="mins_id" CssClass="form-control txtAmount_bk2 validate[optional]" runat="server" style="display:none;" Text='<%# Eval("val6") %>'></asp:Label>
                                                            </ItemTemplate>
                                                              <FooterStyle HorizontalAlign="Right" />
                                                                 <FooterTemplate>
                                                                    <asp:TextBox ID="lblTotal2" CssClass="form-control TotalValue_bk" style="font-weight:bold; pointer-events:none;" runat="server" ></asp:TextBox>
                                                                    </FooterTemplate>
                                                        </asp:TemplateField>    
                                                         <asp:TemplateField HeaderText="">
                                                                                            <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                  <asp:CheckBox ID="rbtnSelect2" onchange="ValidateEmail(this)" runat="server"/>
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
                             <div class="box-body">&nbsp;</div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                 <asp:Button ID="btn_submit" runat="server" class="btn btn-danger" Text="Simpan" UseSubmitBehavior="false" OnClick="btn_simp_Click" />                                
                            </div>
                           </div>
                               </div>
                           <div class="box-body">&nbsp;
                                    </div>
                             <cc1:ModalPopupExtender BackgroundCssClass="modalBg" DropShadow="true" ID="ModalPopupExtender1"
                                                                PopupControlID="Panel3" runat="server" TargetControlID="btnBack" PopupDragHandleControlID="Panel2"
                                                                CancelControlID="btnBack">
                                                            </cc1:ModalPopupExtender>
                                <asp:Panel ID="Panel3" runat="server" CssClass="modalPanel" Style="display: none; overflow-y:auto; height: 95vh;">
                                      <div class="box-header with-border">
                            <h3 class="box-title">Tetapan Tarikh Cuti Ganti</h3>
                        </div>                         
                        <div class="box-body">&nbsp;</div>
                                      <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="Label2" runat="server"> No Kakitangan</label>
                                    <div class="col-sm-8 text-right">
                                        <asp:label ID="TextBox2" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                    <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="Label6" runat="server"> Tahun </label>
                                    <div class="col-sm-8 text-right">
                                        <asp:label ID="TextBox7" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="Label5" runat="server">   Nama Kakitangan</label>
                                    <div class="col-sm-8 text-right">
                                        <asp:label ID="TextBox6" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="Label7" runat="server"> Bulan</label>
                                    <div class="col-sm-8 text-right">
                                        <asp:label ID="TextBox8" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                         
                                 </div>
                                </div>

                               
                                     <asp:GridView ID="GridView1" runat="server" CssClass="table datatable dataTable no-footer uppercase" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="31" ShowFooter="true" GridLines="None" AutoGenerateColumns="false">
                                                    <Columns>
                                                    <asp:TemplateField HeaderText="BIL">  
                                                         <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TARIKH">
                                                             <ItemStyle HorizontalAlign="center" Width="5%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl11" runat="server" Text='<%# Eval("tdt") %>'></asp:Label>                                                              
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MASA MULA">
                                                                <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:label ID="lbl12" runat="server" Text='<%# Eval("otd_time_start") %>'></asp:label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MASA AKHIR">
                                                             <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                            <ItemTemplate>
                                                               <asp:Label ID="lbl13" runat="server" Text='<%# Eval("otd_time_end") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="KETERANGAN">
                                                             <ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
                                                            <ItemTemplate>
                                                               <asp:Label ID="lbl15" runat="server" Text='<%# Eval("otd_desc") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="JUMLAH JAM">
                                                             <ItemStyle HorizontalAlign="right" Width="5%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl14" runat="server" Text='<%# Eval("otd_total_hour") %>'></asp:Label>
                                                            </ItemTemplate>
                                                              <FooterStyle HorizontalAlign="Right" />
                                                                 <FooterTemplate>
                                                                    <asp:Label ID="lblTotal12" runat="server" ></asp:Label>
                                                                    </FooterTemplate>
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
                                       <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Kelayakan Cuti Ganti</h3>
                        </div>                         
                        <div class="box-body">&nbsp;</div>
                                     <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="Label1" runat="server"> Kelayakan Hari Cuti </label>
                                    <div class="col-sm-8">
                                                          <asp:TextBox ID="hari_cuti" runat="server" class="form-control validate[optional"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                      <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="Label3" runat="server"> Tarikh Mula Layak Cuti </label>
                                    <div class="col-sm-8">
                                          <div class="input-group">
                                                          <asp:TextBox ID="TextBox1" runat="server" class="form-control datepicker mydatepickerclass" autocomplete="off"
                                                                                        placeholder="DD/MM/YYYY"></asp:TextBox>
                                               <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                    </div>
                                </div>
                            </div>
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="Label4" runat="server"> Tarikh Akhir Layak Cuti </label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                                           
                                         <asp:TextBox ID="TextBox4" runat="server" class="form-control validate[optional] datepicker mydatepickerclass" autocomplete="off" placeholder="DD/MM/YYYY"></asp:TextBox>
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            </div>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                    <hr />
                                     <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 text-center box-body">
                                <div class="form-group">
                                     <asp:TextBox ID="get_id" Visible="false" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                                                                        <asp:Button ID="Button4" runat="server" class="btn btn-danger sub_btn" Text="Simpan" OnClick="clk_submit"  UseSubmitBehavior="false" />
                                                                            &nbsp;
                                                                            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-default" Text="Keluar" />
                                </div>
                            </div>
                                
                          
                                 </div>
                                </div>
                                                               
                                    </asp:Panel>
                        </div>
                          <div class="row">
                                   <div class="col-md-12 col-sm-4" style="text-align:center; display:none; line-height:13px;">
                                     <rsweb:ReportViewer ID="RptviwerStudent" runat="server" Width="50%"></rsweb:ReportViewer>
                                    </div>
                                    </div>
                    </div>
                </div>
            </div>
            <!-- /.row -->
            
         </ContentTemplate>
             <Triggers>
                 <asp:PostBackTrigger ControlID="Button3" />                 
           </Triggers>
    </asp:UpdatePanel>
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
  
</asp:Content>





