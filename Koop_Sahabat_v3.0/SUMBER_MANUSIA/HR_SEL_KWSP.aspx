<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="HR_SEL_KWSP.aspx.cs" Inherits="SUMBER_MANUSIA_HR_SEL_KWSP" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  <script type="text/javascript">
     function ValidateEmail(button) {
         $(document).ready(function () {
             $('.txt_min').keypress(function (event) {
                 return isNumber(event, this)
             });
         });
         var prm = Sys.WebForms.PageRequestManager.getInstance();
         prm.add_endRequest(function () {
             $('.txt_min').keypress(function (event) {
                 return isNumber(event, this)
             });
         });
     };

     function isNumber(evt, element) {
         var charCode = (evt.which) ? evt.which : event.keyCode
         if (
             (charCode != 45 || $(element).val().indexOf('-') != -1) &&      // “-” CHECK MINUS, AND ONLY ONE.
             (charCode != 46 || $(element).val().indexOf('.') != -1) &&      // “.” CHECK DOT, AND ONLY ONE.
             (charCode != 44 || $(element).val().indexOf(',') != -1) &&      // "," ChECK COMMA
             (charCode < 48 || charCode > 57))
             return false;
         return true;
     };
    </script>
     <script type="text/javascript">
         function addTotal_bk1() {

             var amt1 = Number($("#<%=jpp_min.ClientID %>").val());

             $(".ss1").val(amt1.toFixed(2));

         }

         function addTotal_bk2() {

             var amt2 = Number($("#<%=jpp_mak.ClientID %>").val());

             $(".ss2").val(amt2.toFixed(2));

         }

       
</script>
       
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

     
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1 >   Selenggara Maklumat Jadual KWSP </h1>
                        <ol class="breadcrumb">
                            <li id="bb1_text" runat="server"><a href="#"><i class="fa fa-dashboard"></i>Sumber Manusia</a></li>
                            <li class="active" > Selenggara Maklumat Jadual KWSP </li>
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
                            <h3 class="box-title" >Jadual Gaji Potongan KWSP</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl1_text" runat="server"> Minimum (RM) <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="jpp_min" style="text-align:right;" runat="server"  class="form-control validate[optional,custom[number]] uppercase ss1" onblur="addTotal_bk1(this)"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                             <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl2_text" runat="server">  Maksimum (RM) </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="jpp_mak" runat="server" style="text-align:right;"  class="form-control validate[optional,custom[number]] uppercase ss2" onblur="addTotal_bk2(this)"></asp:TextBox>
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
                               <asp:Button ID="Button8" runat="server" class="btn btn-danger" Text="Simpan" Type="submit" OnClick="insert_Click3"/>
                                <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" UseSubmitBehavior="false"  OnClick="rst_clk"   />
                            </div>
                           </div>
                               </div>
                              <div class="box-header with-border">
                            <h3 class="box-title" id="h3_tag2" runat="server">Senarai KWSP</h3>
                        </div>
                             <div class="box-body">&nbsp;</div>
                            <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                  <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnPageIndexChanging="SubmitAppraisalGrid_PageIndexChanging" OnRowCreated="grvMergeHeader_RowCreated">
                                       <PagerStyle CssClass="pager" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="BIL" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                                    ItemStyle-Width="150" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                       <%-- <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%#Eval("id")%>' Visible="false" />
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                MINIMUM (RM)
                                                            </HeaderTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_min" style="text-align:right;" Width="100%" runat="server" ReadOnly="true" Text='<%#Eval("h1","{0:n}")%>' CssClass="form-control validate[optional,custom[number]] txt_min" />
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                MINIMUM (RM)
                                                            </HeaderTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_max" style="text-align:right;" Width="100%" runat="server" Text='<%#Eval("h2","{0:n}")%>' CssClass="form-control validate[optional,custom[number]] txt_max" />
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                MAKSIMUM (RM)
                                                            </HeaderTemplate>
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
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                               <asp:Button ID="Button3" runat="server" class="btn btn-danger" Text="Kemaskini" UseSubmitBehavior="false" OnClick="Button3_Click" />
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

