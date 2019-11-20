<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../keanggotan/Penyelesaian_Anggota1.aspx.cs" Inherits="Penyelesaian_Anggota1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server" ScriptMode="Release">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1> <asp:Label ID="ps_lbl1" runat="server"></asp:Label>  </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>  <asp:Label ID="ps_lbl2" runat="server"></asp:Label> </a></li>
                            <li class="active">   <asp:Label ID="ps_lbl3" runat="server"></asp:Label>  </li>
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
                                    <label for="inputEmail3" class="col-sm-4 control-label"> <asp:Label ID="ps_lbl5" runat="server"></asp:Label> <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="textbox1" runat="server" MaxLength="12"  CssClass="form-control validate[required,custom[onlyLetterNumberSp]] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-8">
                                        <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Carian" usesubmitbehavior="false" onclick="Searchbtn_Click"/>
                                                <asp:Button ID="Button4" runat="server" class="btn btn-default"  Text="Set Semula" usesubmitbehavior="false" OnClick="Reset_btn"/>
                                        <asp:Button ID="Button7" Visible="false" runat="server" class="btn btn-default"  Text="Kembali" usesubmitbehavior="false" OnClick="Click_bck"/>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                         
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"> <asp:Label ID="ps_lbl8" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="textbox2" runat="server"  class="form-control uppercase" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl9" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="textbox3" runat="server"  class="form-control validate[optional,custom[phone]] uppercase" MaxLength="12" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"> <asp:Label ID="ps_lbl10" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="textbox4" runat="server"  class="form-control uppercase" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl11" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="textbox5" runat="server"  class="form-control uppercase" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"> <asp:Label ID="ps_lbl12" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="textbox6" runat="server"  class="form-control uppercase" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl13" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="textbox7" runat="server"  class="form-control uppercase" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"> <asp:Label ID="ps_lbl14" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                      <div class="input-group">
                                                      <asp:TextBox ID="textbox8" runat="server"  class="form-control uppercase datepicker mydatepickerclass" ReadOnly="true"></asp:TextBox>
                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                       </div>

                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl15" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="textbox9" runat="server"  class="form-control uppercase" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>


                             <div class="box-header with-border">
                            <h3 class="box-title"><asp:Label ID="ps_lbl16" runat="server"></asp:Label> </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>


                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"> <asp:Label ID="ps_lbl17" runat="server"></asp:Label> <span class="style1">*</span>  </label>
                                    <div class="col-sm-8">
                                      <asp:DropDownList ID="textbox10" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>

                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl18" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="textbox11" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl19" runat="server"></asp:Label><span class="style1">*</span>  </label>
                                    <div class="col-sm-8">
                                      <asp:DropDownList ID="textbox13" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                        </asp:DropDownList>

                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl20" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="DropDownList1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
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
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl21" runat="server"></asp:Label><span class="style1">*</span>  </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="textbox14" runat="server" class="form-control validate[required,custom[number]] uppercase" AutoPostBack="true" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl22" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                                        <asp:TextBox ID="textbox12" runat="server" class="form-control validate[optional] uppercase datepicker mydatepickerclass"></asp:TextBox>
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                       </div>

                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>


                             <div class="box-header with-border">
                            <h3 class="box-title"><asp:Label ID="ps_lbl23" runat="server"></asp:Label></h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl24" runat="server"></asp:Label><span class="style1">*</span>  </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="textbox17" runat="server" MaxLength="12"  class="form-control validate[required,custom[onlyLetterNumberSp]] uppercase"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl25" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="textbox18" runat="server"  class="form-control validate[required,custom[textSp]] uppercase"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl26" runat="server"></asp:Label><span class="style1">*</span>  </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="textbox19" runat="server"  class="form-control validate[required,custom[number]]" MaxLength="15"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl27" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                       <textarea id="textbox20" rows="3" runat="server" class="form-control validate[optional] uppercase" maxlength="250"></textarea>

                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl28" runat="server"></asp:Label> <span class="style1">*</span>  </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="txtpostcd" runat="server" MaxLength="5"  class="form-control validate[required,custom[onlyLetterNumberSp]] uppercase"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl29" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                      <asp:DropDownList ID="ddlnegri" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>

                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl30" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="textbox15" runat="server"  class="form-control validate[optional] uppercase" MaxLength="20"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl31" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="Bank_details" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
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
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl32" runat="server"></asp:Label> <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                      <asp:DropDownList ID="DropDownList2" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                        </asp:DropDownList>
                                                         <asp:TextBox ID="txn_dt" runat="server"  class="form-control" type="hidden"></asp:TextBox>
                                                         <asp:TextBox ID="TextBox16" runat="server"  class="form-control" type="hidden"></asp:TextBox>
                                        <asp:TextBox ID="TextBox21" runat="server"  class="form-control" type="hidden"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>



                             <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                 <asp:TextBox ID="textbox22" runat="server"  class="form-control" value="010301" type="hidden"></asp:TextBox>
                                            <asp:TextBox ID="textbox23" runat="server"  class="form-control" value="PERMOHONAN PENYELESAIAN ANGGOTA" type="hidden"></asp:TextBox>
                                            <asp:TextBox ID="textbox24" runat="server"  class="form-control" value="N" type="hidden"></asp:TextBox>
                                              <asp:Button ID="Button3" runat="server" class="btn btn-danger" UseSubmitBehavior="false"  Text="Simpan" onclick="btnsubmit_Click" />
                                            <asp:Button ID="Button6" runat="server" class="btn btn-danger" UseSubmitBehavior="false" Visible="false" Text="Kemaskini" onclick="btnupdt_Click" />
                                <asp:Button ID="Button8" runat="server" class="btn btn-warning" UseSubmitBehavior="false" Visible="false" Text="Batal Permohonan" onclick="Btl_Click" />
                                            <asp:Button ID="Button2" runat="server" class="btn btn-default"  Text="Set Semula" usesubmitbehavior="false" OnClick="Reset_btn1"/>
                                <asp:Button ID="Button5" runat="server" class="btn btn-default"  Text="Kembali" Visible="false" usesubmitbehavior="false" OnClick="Click_bck"/>
                                 
                            </div>
                           </div>
                               </div>
                           <div class="box-body">&nbsp;
                                    </div>
                               <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
            <div class="col-md-12 box-body">
                                    <asp:GridView ID="gvSelected" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="30" ShowFooter="false" GridLines="None">
                                        <PagerStyle CssClass="pager" />
                                                    <Columns>
                                                     <asp:TemplateField HeaderText="BIL"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TARIKH">
                                                                <ItemStyle HorizontalAlign="CENTER" Width="10%" Font-Bold Font-Underline></ItemStyle>
                                                                    <ItemTemplate>
                                                                   <asp:LinkButton ID="lblSubItem" runat="server" Text='<%# Eval("set_txn_dt")%>' CommandArgument=' <%#Eval("set_new_icno")+","+ Eval("set_txn_dt1")%>' CommandName="Add"  onclick="lblSubItem_Click">
                                                <a  href="#"></a>
                                                  </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                        <asp:BoundField DataField="Application_name" HeaderText="JENIS PERMOHONON" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%"/>
                                                         <asp:TemplateField HeaderText="AMOUNT (RM)" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
                                            
                                            <ItemTemplate>  
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("set_apply_amt","{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
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
