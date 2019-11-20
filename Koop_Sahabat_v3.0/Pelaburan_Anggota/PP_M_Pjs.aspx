<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../Pelaburan_Anggota/PP_M_Pjs.aspx.cs" Inherits="PP_M_Pjs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
         <script type="text/javascript">   
     function ValidateEmail(button){

         var row = button.parentNode.parentNode;
         var label = GetChildControl(row, "bk").value;
         var label1 = "71.43";
         var label2 = "28.57";
         var Multi_l1 = (parseFloat(label) * (parseFloat(label1) / 100));
         var Multi_l2 = (parseFloat(label) * (parseFloat(label2) / 100));
         //alert(Multi);
         GetChildControl(row, "Label6").value = Multi_l1.toFixed(2);
         GetChildControl(row, "Label7").value = Multi_l2.toFixed(2);

         Total_bk = 0.0;
         Total_pr = 0.0;
         Total_kt = 0.0;
         var Sample1;
         var amt1 = Number($("#<%=TextBox5.ClientID %>").val());
         var amt_jum = Number($("#<%=txtjumla.ClientID %>").val().replace(",",""));
         var amt_ju = Number($("#<%=TextBox3.ClientID %>").val().replace(",", ""));
         //var amt2 = Number($("#<%=cpro_amt.ClientID %>").val());

         var t_amt = (parseFloat(amt_jum) + parseFloat(amt_ju));
         
         //var t_amt = amt1;
         $(".txtAmount_bk").each(function () {

             if ($(this).val() != '') {
                 Total_bk += parseFloat($(this).val());
             }
         });
         $(".pri_amt").each(function () {

             if ($(this).val() != '') {
                 Total_pr += parseFloat($(this).val());

             }
         });
         $(".kt_amt").each(function () {

             if ($(this).val() != '') {
                 Total_kt += parseFloat($(this).val());

             }
         });
         //alert(t_amt + "," + Total_bk.toFixed(2));
         if (t_amt == Total_bk.toFixed(2)) {
             $("#<%=Button6.ClientID %>").removeAttr("disabled");
         }
         else {
             $("#<%=Button6.ClientID %>").attr("disabled", "disabled");
         }

         $("#<%=gvSelected.ClientID %> [class*=TotalValue_bk]").val(Total_bk.toFixed(2));
         $("#<%=gvSelected.ClientID %> [class*=TotalValue_pr]").val(Total_pr.toFixed(2));
         $("#<%=gvSelected.ClientID %> [class*=TotalValue_kt]").val(Total_kt.toFixed(2));
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
                <div class="content-wrapper" style="height: 895px;">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1> Permohonan Penjadualan Semula Pembiayaan</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i> Pelaburan Anggota </a></li>
                            <li class="active">Permohonan Penjadualan Semula Pembiayaan</li>
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
                            <h3 class="box-title">Maklumat Pembiayaan</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Kata Kunci Carian <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="Applcn_no" runat="server" class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase"
                                                            MaxLength="12"></asp:TextBox>
                                         <asp:Panel ID="autocompleteDropDownPanel" runat="server" ScrollBars="Auto" Height="150px"
                                                            Font-Size="Medium" HorizontalAlign="Left" Wrap="False" />
                                                        <cc1:AutoCompleteExtender ServiceMethod="GetCompletionList" MinimumPrefixLength="1"
                                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="Applcn_no"
                                                            ID="AutoCompleteExtender1" CompletionListElementID="autocompleteDropDownPanel"
                                                            CompletionListCssClass="form-control uppercase" runat="server" FirstRowSelected="false">
                                                        </cc1:AutoCompleteExtender>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                   <div class="col-sm-8">
                                    <asp:Button ID="Button9" runat="server" class="btn btn-primary" Text="Carian" usesubmitbehavior="false" OnClick="btnsrch_Click" />
                                                    <asp:Button ID="Button10" runat="server" class="btn btn-default" Text="Set Semula" usesubmitbehavior="false" OnClick="btn_rstclick"/>                                     
                                       
                                       </div>
                                    
                                </div>
                            </div>
                                  </div>
                                </div>

                                
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtname" runat="server" class="form-control validate[optional,custom[textSp]]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jumlah Belian (RM)</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="txtjumla" runat="server" style="text-align:right;" class="form-control "></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                <%--  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Wilayah / Pejabat</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="txtwil" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>--%>
                                 </div>
                                </div>
                          <%--   <div class="row">
                             <div class="col-md-12">
                                 
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Cawangan / Jabatan</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtcaw" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>                                
                                  </div>
                         </div>--%>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jumlah Kumulatif Kena (RM)</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="txtamaun" runat="server" style="text-align:right;" class="form-control "></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tempoh (Bulan)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txttempoh" runat="server" class="form-control uppercase" MaxLength="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>                                
                                  </div>
                         </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jumlah Kumulatif Tunggakan (RM)</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="TextBox2" runat="server" style="text-align:right;" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jumlah Kumulatif Bayar (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txttemp" runat="server" style="text-align:right;" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>                                
                                  </div>
                         </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jumlah Kumulatif Simpanan (RM)</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="TextBox4" runat="server" style="text-align:right;" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jumlah Untung (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox3" runat="server" style="text-align:right;" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>                                
                                  </div>
                         </div>
                             <div class="row">
                             <div class="col-md-12">
                                 <%-- <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jenis Pemulihan</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="TextBox6" runat="server" class="form-control validate[optional,custom[number]]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>--%>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Baki Kumulatif Pelaburan (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox5" runat="server" style="text-align:right;" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>                                
                                  </div>
                         </div>
                             
                              <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                  <asp:TextBox ID="capp_amt" runat="server" style="display:none;" class="form-control"></asp:TextBox>
                                                    <asp:TextBox ID="cpro_amt" runat="server" style="display:none;" class="form-control"></asp:TextBox>
                              <asp:Button ID="btnShow" runat="server" class="btn btn-warning" UseSubmitBehavior="false" Text="Semak JBB" OnClick="click_jbb"/>                                                        
                                                        <asp:Button ID="Button4" runat="server" class="btn btn-warning" UseSubmitBehavior="false" Text="Litigasi Bercagar" OnClick="clk_Bercagar"/>                                                        
                                                        <asp:Button ID="Button5" runat="server" class="btn btn-warning" UseSubmitBehavior="false" Text="Litigasi Penjamin" onclick="clk_Penjamin"/>
                                <asp:Button ID="Button2" runat="server" class="btn btn-default" Text="Kembali" usesubmitbehavior="false" OnClick="clk_bak"/>                                     
                            </div>
                           </div>
                               </div>
                              <div class="box-header with-border">
                            <h3 class="box-title">Permohonan Penjadualan Semula Pembiayaan </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                           
                              <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Status <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                           <asp:DropDownList runat="server" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" ID="DropDownList1">
                                                                 <asp:ListItem Value="">--- PILIH ---</asp:ListItem>
                                                                 <asp:ListItem Value="01">PERMOHONAN</asp:ListItem>
                                                                 <asp:ListItem Value="02">BATAL PERMOHONAN</asp:ListItem>
                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Catatan <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                        <textarea id="Textarea1" runat="server" rows="3" class="form-control uppercase" maxlength="500"></textarea>
                                    </div>
                                </div>
                            </div>                                
                                  </div>
                         </div>
                             <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
               <asp:GridView ID="gvSelected" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="100" ShowFooter="true" GridLines="None"  OnPageIndexChanging="gvSelected_PageIndexChanging" OnRowDataBound="gvEmp_RowDataBound">
                                        <Columns>
                                            <%--<asp:TemplateField HeaderText="BIL">
                                                
                                                <ItemStyle HorizontalAlign="Center" />  
                                               <ItemTemplate>
                                            
                                            <asp:Label ID="app_no" Visible="false" runat="server" Text='<%# Bind("log_applcn_no") %>'></asp:Label>
                                            <asp:Label ID="ic_seqno" Visible="false" runat="server" Text='<%# Bind("log_seq_no") %>'></asp:Label>
                                            
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="150"/>
                                            </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="TARIKH KENA">
                                            <ItemStyle HorizontalAlign="Center" />    
                                            <ItemTemplate>  
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("p_dt", "{0:dd/MM/yyyy}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TARIKH BAYAR">  
                                                <ItemStyle HorizontalAlign="Center" />   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("ap_dt", "{0:dd/MM/yyyy}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BIAYA KENA (RM)">
                                                <ItemStyle HorizontalAlign="Right" />    
                                            <ItemTemplate>  
                                                <%--<asp:Label ID="Label4" runat="server" Text='<%# Bind("p_amt","{0:n}") %>'></asp:Label>  --%>
                                                <asp:TextBox ID="bk" CssClass="form-control txtAmount_bk" style="text-align:right; width:100%;"   Text='<%# Bind("p_amt") %>' onblur="ValidateEmail(this)" runat="server" />
                                            </ItemTemplate>  
                                            <FooterTemplate>
                                               <%-- <asp:Label CssClass="TotalValue_bk" ID="lblTotal_bk" runat="server"/>--%>
                                               <asp:TextBox ID="total" CssClass="form-control TotalValue_bk" style="text-align:right; width:100%; font-weight:bold;" ReadOnly="true" runat="server"></asp:TextBox>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true"/>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BIAYA BAYAR (RM)">  
                                                <ItemStyle HorizontalAlign="Right" /> 
                                            <ItemTemplate>  
                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("ap_amt","{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                            <FooterTemplate>
                                                <asp:Label ID="lblTotal_bb" runat="server"/>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true"/>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="PRINSIPAL (RM)">  
                                                 <ItemStyle HorizontalAlign="Right" />     
                                            <ItemTemplate>  
                                                <asp:TextBox ID="Label6" CssClass='form-control pri_amt' style="width:100%; text-align:right;" runat="server" Text="0" disabled="disabled"></asp:TextBox>
                                            </ItemTemplate>  
                                            <FooterTemplate>
                                            <asp:TextBox ID="lblTotal_pr" CssClass="form-control TotalValue_pr" style="text-align:right; width:100%; font-weight:bold;" ReadOnly="true" runat="server"></asp:TextBox>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true"/>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="KEUNTUNGAN (RM)">   
                                                <ItemStyle HorizontalAlign="Right" />    
                                            <ItemTemplate>  
                                                <asp:TextBox ID="Label7" CssClass='form-control kt_amt' runat="server" Text="0.00" style="width:100%; text-align:right;" disabled="disabled"></asp:TextBox>
                                                <asp:Label ID="app_no" Visible="false" runat="server" Text='<%# Bind("app_no") %>'></asp:Label>
                                                <asp:Label ID="ll1" Visible="false" runat="server" Text='<%# Bind("a_amt","{0:n}") %>'></asp:Label>
                                                <asp:Label ID="ll2" Visible="false" runat="server" Text='<%# Bind("p_amt","{0:n}") %>'></asp:Label>
                                                <asp:Label ID="seqno" Visible="false" runat="server" Text='<%# Bind("seq_no") %>'></asp:Label>
                                                <asp:Label ID="sa_amt" Visible="false" runat="server" Text='<%# Bind("sa_amt","{0:n}") %>'></asp:Label>
                                                <asp:Label ID="tsa_amt" Visible="false" runat="server" Text='<%# Bind("ts_amt","{0:n}") %>'></asp:Label>
                                                <asp:Label ID="b_amt" Visible="false" runat="server" Text='<%# Bind("bal_amt","{0:n}") %>'></asp:Label>
                                                <asp:Label ID="apamt" Visible="false" runat="server" Text='<%# Bind("ap_amt","{0:n}") %>'></asp:Label>
                                                <asp:Label ID="leamt" Visible="false" runat="server" Text='<%# Bind("amt1","{0:n}") %>'></asp:Label>
                                                <asp:Label ID="asaamt" Visible="false" runat="server" Text='<%# Bind("amt3","{0:n}") %>'></asp:Label>
                                                <asp:Label ID="sleamt" Visible="false" runat="server" Text='<%# Bind("amt4","{0:n}") %>'></asp:Label>
                                                <asp:Label ID="tpayamt" Visible="false" runat="server" Text='<%# Bind("amt5","{0:n}") %>'></asp:Label>
                                            </ItemTemplate>  
                                            <FooterTemplate>
                                            <asp:TextBox ID="lblTotal_kt" CssClass="form-control TotalValue_kt" style="text-align:right; font-weight:bold;" ReadOnly="true" runat="server"></asp:TextBox>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true"/>
                                                </asp:TemplateField>
                                        </Columns>
                   <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />                                                       
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
               </div>
                                 </div>
                                      <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                               <asp:Button ID="Button6" disabled="disabled" runat="server" class="btn btn-danger" Text="Simpan" OnClick="btn_click"/>
                                                        <asp:Button ID="Button1" Visible="false" runat="server" class="btn btn-danger" OnClick="click_print" Text="Cetak"/>                                                     
                                                        <asp:Button ID="Button7" runat="server" class="btn btn-default" Text="Set Semula" UseSubmitBehavior="false" OnClick="btn_rstclick1"/>                                                        
                            </div>
                           </div>
                               </div>               
                          <div class="row">
                                   <div class="col-md-12 col-sm-2" style="text-align:center">
                                     <rsweb:ReportViewer ID="Rptviwer_lt" runat="server"></rsweb:ReportViewer>
                                     <asp:Label runat="server" ID="ReportErrorMessage" Visible="false" CssClass="report-error-message"></asp:Label>
                                    </div>
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



