<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Kewengan/kw_pelarasan.aspx.cs" Inherits="kw_pelarasan" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
         <script type="text/javascript">   
        function ValidateEmail(button) {

            var row = button.parentNode.parentNode;
            var lbl_amt1 = GetChildControl(row, "Col3").value.replace(",","");
            var lbl_amt2 = GetChildControl(row, "Col4").value.replace(",", "");
            var vv_amt1;
            var vv_amt2;
            if (lbl_amt1 != "")
            {
                vv_amt1 = lbl_amt1;
            }
            else {
                vv_amt1 = "0.00";
            }

            if (lbl_amt2 != "") {
                vv_amt2 = lbl_amt2;
            }
            else {
                vv_amt2 = "0.00";
            }
           
            Total_bk = 0.0;
            Total_bk1 = 0.0;
            $(".txtAmount_bk").each(function () {
                var vv1;
                
                if ($(this).val() != "")
                {
                    vv1 = $(this).val();
                }
                else
                {
                    vv1 = "0.00";
                }

               // if ($(this).val() != '') {
                Total_bk += parseFloat(vv1.replace(",", ""));
                GetChildControl(row, "Col3").value = addCommas(parseFloat(vv_amt1).toFixed(2));
                GetChildControl(row, "Col4").value = "";
                
                //}
               
                $("#<%=grvStudentDetails.ClientID %> [class*=TotalValue_bk]").val(addCommas(Total_bk.toFixed(2)));
            });
            $(".txtAmount_bk1").each(function () {
                var vv2;
              
                if ($(this).val() != "") {
                    vv2 = $(this).val();
                }
                else {
                    vv2 = "0.00";
                }
                //if ($(this).val() != '') {
                Total_bk1 += parseFloat(vv2.replace(",", ""));
                GetChildControl(row, "Col4").value = addCommas(parseFloat(vv_amt2).toFixed(2));
                //}
                 $("#<%=grvStudentDetails.ClientID %> [class*=TotalValue_bk1]").val(addCommas(Total_bk1.toFixed(2)));
            });
            //alert(Total_bk.toFixed(2));
                
               
         return false;
        };

        function addCommas(x) {
            var parts = x.toString().split(".");
            parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
            return parts.join(".");
        }

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
                                      <asp:TextBox ID="TextBox1" runat="server" class="form-control validate[optional] uppercase" MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl6" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="TextBox4" runat="server" class="form-control validate[optional] uppercase" MaxLength="500"></asp:TextBox>
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
                                        <asp:TextBox ID="TextBox2" runat="server" class="form-control validate[optional] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
                                             <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                             <div class="box-body">&nbsp;
                                    </div>
                                
           <div class="col-md-12 box-body">
                                   <asp:GridView ID="grvStudentDetails" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" ShowFooter="True" PageSize="20" onrowdatabound="gvEmp_RowDataBound" OnRowDeleting="grvStudentDetails_RowDeleting">
                                                        <Columns>
                                                            <asp:TemplateField Visible="false" HeaderText="BIL">
                                                                <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:label ID="RowNumber"  runat="server" Text='<%# Eval("RowNumber") %>'></asp:label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="KOD AKAUN">
                                                                 <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="Col1" style="width:100%; font-size:13px;" runat="server" CssClass="form-control select2 validate[optional]"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                 <FooterStyle HorizontalAlign="Left" />
                                                                <FooterTemplate>
                                                                    <asp:Button ID="ButtonAdd" runat="server" Text="Tambah Baru" OnClick="ButtonAdd_Click" />
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="KETERANGAN">
                                                                <ItemTemplate>
                                                                   <asp:TextBox ID="Col2" CssClass="form-control uppercase" runat="server" Text='<%# Eval("col2") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="DEBIT (RM)">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <ItemTemplate>
                                                                   <asp:TextBox ID="Col3" style="text-align:right;" CssClass="form-control txtAmount_bk" placeholder="0.00" Text='<%# Eval("col3","{0:n}") %>' OnTextChanged="QtyChanged_deb" AutoPostBack="true" onblur="ValidateEmail(this)" runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                                 <FooterStyle HorizontalAlign="Right" />
                                                                 <FooterTemplate>
                                                                    <asp:TextBox ID="lblTotal1" CssClass="form-control TotalValue_bk" style="text-align:right; font-weight:bold;" ReadOnly="true" runat="server" ></asp:TextBox>
                                                                    </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="KREDIT (RM)">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <ItemTemplate>
                                                                   <asp:TextBox ID="Col4" style="text-align:right;" CssClass="form-control txtAmount_bk1" placeholder="0.00" Text='<%# Eval("col4","{0:n}") %>' OnTextChanged="QtyChanged_kre" AutoPostBack="true" onblur="ValidateEmail(this)" runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                 <FooterTemplate>
                                                                    <asp:TextBox ID="lblTotal2" CssClass="form-control TotalValue_bk1" style="text-align:right; font-weight:bold;" ReadOnly="true" runat="server" ></asp:TextBox>
                                                                    </FooterTemplate>
                                                            </asp:TemplateField>
                                                           <%-- <asp:CommandField ShowDeleteButton="True" />--%>
                                                        </Columns>
                                                       <%-- <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="White" />--%>
                                                    </asp:GridView> 
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
                                 
                            </div>
                           </div>
                               </div>
                           
                            <div class="box-body">&nbsp;</div>
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

