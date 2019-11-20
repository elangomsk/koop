<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_SELE_GAJI.aspx.cs" Inherits="HR_SELE_GAJI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       
    <script type="text/javascript">
          function addTotal_bk1() {
           
                  var amt1 = Number($("#<%=GP_amaun.ClientID %>").val());

                  $(".au_amt").val(amt1.toFixed(2));
            
          }

          function addTotal_bk2() {
             
                  var amt2 = Number($("#<%=ET_amaun.ClientID %>").val());

                  $(".au_amt1").val(amt2.toFixed(2));
              
          }

          function addTotal_bk3() {
              
                  var amt3 = Number($("#<%=LL_amaun.ClientID %>").val());

                  $(".au_amt2").val(amt3.toFixed(2));

              }
              function addTotal_bk4() {

                  var amt4 = Number($("#<%=TextBox5.ClientID %>").val());

                  $(".au_amt3").val(amt4.toFixed(2));

              }
              function addTotal_bk5() {

                  var amt5 = Number($("#<%=TextBox7.ClientID %>").val());

                  $(".au_amt4").val(amt5.toFixed(2));

              }
                function addTotal_bk6() {

                  var amt4 = Number($("#<%=TextBox10.ClientID %>").val());

                  $(".au_amt6").val(amt4.toFixed(2));

                }

          function addTotal_bk1_fp() {
           
                  var amt1_fp = Number($("#<%=TextBox15.ClientID %>").val());

              $(".au_amt1_fp").val(amt1_fp.toFixed(2));
            
          }
        function addTotal_bk1_xt() {
           
                  var amt1_xt = Number($("#<%=TextBox19.ClientID %>").val());
              $(".au_amt1_xt").val(amt1_xt.toFixed(2));
            
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
                        <h1 id="h1_tag" runat="server">  Maklumat Penggajian  </h1>
                        <ol class="breadcrumb">
                            <li id="bb1_text" runat="server"><a href="#"><i class="fa fa-dashboard"></i>Sumber Manusia</a></li>
                            <li class="active" id="bb2_text" runat="server">  Maklumat Penggajian </li>
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
                            <h3 class="box-title" id="h3_tag" runat="server"> Maklumat Peribadi </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl1_text" runat="server"> No Kakitangan</label>
                                    <div class="col-sm-8">
                                        <asp:label ID="Kaki_no" runat="server" class="uppercase"
                                                                    MaxLength="150"></asp:label>
                                         <asp:TextBox ID="Applcn_no1" runat="server" class="form-control validate[optional] uppercase"
                                                                                MaxLength="150" Visible="false"></asp:TextBox>
                                                                     
                                    </div>
                                </div>
                            </div>
                             <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-8">
                                      <asp:Button ID="Button19" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" OnClick="Click_bck"  />
                                                                     
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
                                        <asp:label ID="s_nama" runat="server" class="uppercase"></asp:label>
                                    </div>
                                     </div>
                            </div>
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl3_text" runat="server">  Gred</label>
                                    <div class="col-sm-8">
                                        <asp:label ID="s_gred" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl4_text" runat="server">Nama Syarikat / Organisasi </label>
                                    <div class="col-sm-8">
                                        <asp:label ID="txt_org" runat="server" class="uppercase"></asp:label>
                                        <asp:label ID="Label29" runat="server" Visible="false" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                 

                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl5_text" runat="server">Perniagaan </label>
                                    <div class="col-sm-8">
                                        <asp:label ID="TextBox2" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                    

                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl6_text" runat="server">Jabatan  </label>
                                    <div class="col-sm-8">
                                        <asp:label ID="s_jab" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                 
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl7_text" runat="server">Jawatan   </label>
                                    <div class="col-sm-8">
                                        <asp:label ID="s_jaw" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                     
                          <div class="row">
                             <div class="col-md-12">
                             <div id="Div1" class="nav-tabs-custom col-md-12 box-body" role="tabpanel" runat="server">
                                <ul class="s1 nav nav-tabs text-bold" role="tablist">
                                                               <li id="pp6" runat="server" class="active"><a href="#ContentPlaceHolder1_p6" aria-controls="p6" role="tab" data-toggle="tab" id="pt1" runat="server">Maklumat Bank</a></li>
                                                                <li id="pp1" runat="server"><a href="#ContentPlaceHolder1_p1" aria-controls="p1" role="tab" data-toggle="tab" id="pt2" runat="server">Gaji Pokok</a></li>
                                                                <li id="pp2" runat="server"><a href="#ContentPlaceHolder1_p2" aria-controls="p2" role="tab" data-toggle="tab" id="pt3" runat="server">Elaun Tetap</a></li>
                                                                <li id="pp3" runat="server"><a href="#ContentPlaceHolder1_p3" aria-controls="p3" role="tab" data-toggle="tab" id="pt4" runat="server">Lain-Lain Elaun</a></li>
                                                                <li id="pp4" runat="server"><a href="#ContentPlaceHolder1_p4" aria-controls="p4" role="tab" data-toggle="tab" id="pt5" runat="server">Kerja Lebih Masa</a></li>
                                                                <li id="pp5" runat="server"><a href="#ContentPlaceHolder1_p5" aria-controls="p5" role="tab" data-toggle="tab" id="pt6" runat="server">Bonus</a></li>
                                                                <li id="pp7" runat="server"><a href="#ContentPlaceHolder1_p7" aria-controls="p7" role="tab" data-toggle="tab" id="pt7" runat="server">Lain-Lain</a></li>
                                                            </ul>

                                <div class="tab-content" style="padding-top: 20px">
                                                            <div role="tabpanel" class="tab-pane active" runat="server" id="p6">
                        <div class="box-body">&nbsp;</div>

                                                                <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl8_text" runat="server">  No Akaun Bank <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="s_bno" runat="server" class="form-control validate[optional,custom[bank]] uppercase"
                                                                                    MaxLength="20"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl9_text" runat="server">  Nama Bank <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="dd_s_bank" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                                                 <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                <asp:Button ID="Button15" runat="server" class="btn btn-danger" Text="Simpan" Type="submit" OnClick="insert_bk1" />
                                 </div>
                           </div>
                               </div>
                                                                <div class="box-body">&nbsp;</div>
                                          </div>
                                                                 <div role="tabpanel" class="tab-pane" runat="server" id="p1">
                                                                         <div class="box-body">&nbsp;</div>                                                                     
                                                                      <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl10_text" runat="server">   Tarikh Mula <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                      <div class="input-group">
                                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                          <asp:TextBox ID="GP_date" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                                        placeholder="DD/MM/YYYY"></asp:TextBox>
                                                        </div>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl11_text" runat="server">   Tarikh Akhir <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                      <div class="input-group">
                                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                          <asp:TextBox ID="GE_date" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                                        placeholder="DD/MM/YYYY"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl12_text" runat="server">  Gaji Pokok (RM)   </label>
                                    <div class="col-sm-8">
                                   <asp:TextBox style="text-align:right;" ID="GP_amaun" runat="server" class="form-control validate[optional,custom[number]] au_amt"  onblur="addTotal_bk1(this)"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl13_text" runat="server">   Sebab</label>
                                    <div class="col-sm-8">
                                             <asp:DropDownList ID="dd_sebab" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                                </asp:DropDownList>
                                                                                <asp:TextBox ID="GP_rno" Visible="false" runat="server" class="form-control validate[optional]"
                                                                                    MaxLength="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>                              
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                 <asp:Button ID="Button5" runat="server" class="btn btn-danger" Text="Simpan" Type="submit"
                                                                                    OnClick="insert_Click1" />
                                                                                <asp:Button ID="Button7" runat="server" Text="Hapus" class="btn btn-warning" UseSubmitBehavior="false"
                                                                                    OnClick="hapus_click1" OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;" />
                                   <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" Type="submit"
                                                                                    OnClick="rset_Click1" />
                            </div>
                           </div>
                               </div>
                                                                      <div class="box-body">&nbsp;</div>
                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                 <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging_1">
                                      <PagerStyle CssClass="pager" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="BIL">
                                                                                            <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Tarikh Mula">
                                                                                            <ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton runat="server" ID="lnkView" OnClick="lnkView_Click" Font-Bold Font-Underline>
                                                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("slr_eff_dt","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                                                    <asp:Label ID="st_no" Visible="false" runat="server" Text='<%# Eval("slr_staff_no") %>'></asp:Label>
                                                                                                </asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Tarikh Sehingga">
                                                                                            <ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label3e" runat="server" Text='<%# Eval("slr_end_dt","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Gaji Pokok (RM)">
                                                                                            <ItemStyle HorizontalAlign="Right" Width="20%"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("slr_salary_amt","{0:n}") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Sebab">
                                                                                            <ItemStyle HorizontalAlign="Left" Width="40%"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("seb_desc") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Hapus">
                                                                                        <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                 <asp:CheckBox ID="rbtnSelect1" runat="server"/>
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
 </div>

                                                                <div role="tabpanel" class="tab-pane" runat="server" id="p2">
                                                                    
                        <div class="box-body">&nbsp;</div>
                                                                       <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl14_text" runat="server">   Tarikh Mula <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                      <div class="input-group">
                                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                          <asp:TextBox ID="ET_mdate" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                                        placeholder="DD/MM/YYYY"></asp:TextBox>
                                                        </div>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl15_text" runat="server">   Tarikh Akhir <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                      <div class="input-group">
                                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                          <asp:TextBox ID="ET_sdate" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                                        placeholder="DD/MM/YYYY"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl16_text" runat="server">   Jenis Elaun</label>
                                    <div class="col-sm-8">
                                      <asp:DropDownList ID="ET_jelaun" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl17_text" runat="server">   Amaun (RM)</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="ET_amaun" style="text-align:right;" runat="server" class="form-control validate[optionalcustom[number] au_amt1"  onblur="addTotal_bk2(this)"></asp:TextBox>
                                                                                <asp:TextBox ID="ET_rno" Visible="false" runat="server" class="form-control validate[optional]"
                                                                                    MaxLength="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                                          
                                 </div>
                                </div>
                                                                     <div class="row" id="fixpromo_en" runat="server" Visible="false">
                             <div class="col-md-12">
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="Label10" runat="server">   Promosi</label>
                                    <div class="col-sm-8">
                                     <asp:CheckBox ID="fix_promo" runat="server" AutoPostBack="true" OnCheckedChanged="fix_promo_changed"/>
                                    </div>
                                </div>
                            </div>
                                             
                                 </div>
                                </div>
                                                                    <div id="fixpromo_show" runat="server" style="display:none;">
                                                                      <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="Label11" runat="server">   Tarikh Mula <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                      <div class="input-group">
                                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                          <asp:TextBox ID="TextBox12" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                                        placeholder="DD/MM/YYYY"></asp:TextBox>
                                                        </div>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="Label12" runat="server">   Tarikh Akhir <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                      <div class="input-group">
                                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                          <asp:TextBox ID="TextBox13" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                                        placeholder="DD/MM/YYYY"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="Label13" runat="server">   Amaun (RM)</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox15" style="text-align:right;" runat="server" class="form-control validate[optionalcustom[number] au_amt1_fp"  onblur="addTotal_bk1_fp(this)"></asp:TextBox>
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
                                  <asp:Button ID="Button2" runat="server" class="btn btn-danger" Text="Simpan" Type="submit"
                                                                                    OnClick="insert_Click2" />
                                                                                <asp:Button ID="Button4" runat="server" Text="Hapus" class="btn btn-warning" UseSubmitBehavior="false"
                                                                                    OnClick="hapus_click2" OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;" />
                                  <asp:Button ID="Button3" runat="server" class="btn btn-default" Text="Set Semula" Type="submit"
                                                                                    OnClick="rset_Click2" />
                                 </div>
                           </div>
                               </div>
                                                                    <div class="box-body">&nbsp;</div>
                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                 <asp:GridView ID="GridView2" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnRowCreated="grvMergeHeader_RowCreated" OnPageIndexChanging="gvSelected_PageIndexChanging_2">
                                      <PagerStyle CssClass="pager" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="BIL">
                                                                                            <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                                                    ItemStyle-Width="150" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Tarikh Mula">
                                                                                            <ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton runat="server" ID="lnkView" OnClick="lnkView_Click1" Font-Bold Font-Underline>
                                                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("fxa_eff_dt","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                                                    <asp:Label ID="est_no" Visible="false" runat="server" Text='<%# Eval("fxa_staff_no") %>'></asp:Label>
                                                                                                    <asp:Label ID="eall_cd" Visible="false" runat="server" Text='<%# Eval("fxa_allowance_type_cd") %>'></asp:Label>
                                                                                                    <asp:Label ID="fxactdt" Visible="false" runat="server" Text='<%# Eval("fxacrt_dt") %>'></asp:Label>
                                                                                                </asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Tarikh Sehingga">
                                                                                            <ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("fxa_end_dt","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="JENIS Elaun">
                                                                                            <ItemStyle HorizontalAlign="Left" Width="25%"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("hr_elau_desc") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Amaun (RM)">
                                                                                            <ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("fxa_allowance_amt","{0:n}") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                          <asp:TemplateField HeaderText="PROMOSI" Visible="false">
                                                                                            <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label5_promo" runat="server" Text='<%# Eval("promo") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                          <asp:TemplateField HeaderText="Taikh Mula" Visible="false">
                                                                                            <ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label5_prsdt" runat="server" Text='<%# Eval("fxa_pst_dt") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                          <asp:TemplateField HeaderText="Tarikh Akhir" Visible="false">
                                                                                            <ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label5_predt" runat="server" Text='<%# Eval("fxa_ped_dt") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                          <asp:TemplateField HeaderText="Amaun (RM)" Visible="false">
                                                                                            <ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label5_pamt" runat="server" Text='<%# Eval("fxa_promo_amt","{0:n}") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="Hapus">
                                                                                            <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                  <asp:CheckBox ID="rbtnSelect2" runat="server"/>
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

                                                                    </div>
                                    <div role="tabpanel" class="tab-pane" runat="server" id="p3">
                        <div class="box-body">&nbsp;</div>
                                                                       <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl18_text" runat="server">   Tarikh Mula <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                      <div class="input-group">
                                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        <asp:TextBox ID="LL_mdate" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                                        placeholder="DD/MM/YYYY"></asp:TextBox>
                                                        </div>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl19_text" runat="server">   Tarikh Akhir <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                      <asp:TextBox ID="LL_sdate" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                                        placeholder="DD/MM/YYYY"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl20_text" runat="server">   Jenis Elaun</label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="LL_jelaun" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl21_text" runat="server">   Amaun (RM)</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="LL_amaun" style="text-align:right;" runat="server" class="form-control validate[optional,custom[number] au_amt2"  onblur="addTotal_bk3(this)"></asp:TextBox>
                                                                                <asp:TextBox ID="LLE_rno" Visible="false" runat="server" class="form-control validate[optional]"
                                                                                    MaxLength="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                                          
                                 </div>
                                </div>
                                         <div class="row" id="xta_promo_en" runat="server" visible="false">
                             <div class="col-md-12">
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="Label14" runat="server">   Promosi</label>
                                    <div class="col-sm-8">
                                     <asp:CheckBox ID="xta_promo" runat="server" AutoPostBack="true" OnCheckedChanged="xta_promo_changed"/>
                                    </div>
                                </div>
                            </div>
                                             
                                 </div>
                                </div>
                                                                    <div id="xtapromo_show" runat="server" style="display:none;">
                                                                      <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="Label15" runat="server">   Tarikh Mula <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                      <div class="input-group">
                                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                          <asp:TextBox ID="TextBox17" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                                        placeholder="DD/MM/YYYY"></asp:TextBox>
                                                        </div>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="Label16" runat="server">   Tarikh Akhir <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                      <div class="input-group">
                                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                          <asp:TextBox ID="TextBox18" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                                        placeholder="DD/MM/YYYY"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="Label17" runat="server">   Amaun (RM)</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox19" style="text-align:right;" runat="server" class="form-control validate[optionalcustom[number] au_amt1_xt"  onblur="addTotal_bk1_xt(this)"></asp:TextBox>
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
                                <asp:Button ID="Button8" runat="server" class="btn btn-danger" Text="Simpan" UseSubmitBehavior="false"
                                                                                    Type="submit" OnClick="insert_Click3"/>
                                                                                <asp:Button ID="Button10" runat="server" Text="Hapus" class="btn btn-warning" UseSubmitBehavior="false"
                                                                                    OnClick="hapus_click3" OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;"/>
                                 <asp:Button ID="Button16" runat="server" class="btn btn-default" Text="Set Semula" Type="submit"
                                                                                    OnClick="rset_Click3" />
                                 </div>
                           </div>
                               </div>
                                                                    <div class="box-body">&nbsp;</div>
                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                <asp:GridView ID="GridView3" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnRowCreated="grvMergeHeader_RowCreated1" OnPageIndexChanging="gvSelected_PageIndexChanging_3">
                                     <PagerStyle CssClass="pager" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="BIL">
                                                                                            <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                                                    ItemStyle-Width="150" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Tarikh Mula">
                                                                                            <ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton runat="server" ID="lnkView" OnClick="lnkView_Click2" Font-Bold Font-Underline>
                                                                                                    <asp:Label ID="Label21" runat="server" Text='<%# Eval("xta_eff_dt","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                                                    <asp:Label ID="lst_no" Visible="false" runat="server" Text='<%# Eval("xta_staff_no") %>'></asp:Label>
                                                                                                    <asp:Label ID="lall_cd" Visible="false" runat="server" Text='<%# Eval("xta_allowance_type_cd") %>'></asp:Label>
                                                                                                     <asp:Label ID="xta_crtdt" Visible="false" runat="server" Text='<%# Eval("crt_dt") %>'></asp:Label>
                                                                                                </asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Tarikh Sehingga">
                                                                                            <ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label31" runat="server" Text='<%# Eval("xta_end_dt","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="JENIS Elaun">
                                                                                            <ItemStyle HorizontalAlign="Left" Width="25%"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label41" runat="server" Text='<%# Eval("hr_elau_desc") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Amaun (RM)">
                                                                                            <ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label51" runat="server" Text='<%# Eval("xta_allowance_amt","{0:n}") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="PROMOSI" visible="false">
                                                                                            <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label51_promo" runat="server" Text='<%# Eval("promo") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                          <asp:TemplateField HeaderText="Taikh Mula" visible="false">
                                                                                            <ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label51_prsdt" runat="server" Text='<%# Eval("xta_pst_dt") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                          <asp:TemplateField HeaderText="Tarikh Akhir" visible="false">
                                                                                            <ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label51_predt" runat="server" Text='<%# Eval("xta_ped_dt") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                          <asp:TemplateField HeaderText="Amaun (RM)" visible="false">
                                                                                            <ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label51_pamt" runat="server" Text='<%# Eval("xta_promo_amt","{0:n}") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="Hapus" >
                                                                                            <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                 <asp:CheckBox ID="rbtnSelect3" runat="server" />
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

                                                                    </div>
                                    <div role="tabpanel" class="tab-pane" runat="server" id="p4">
                        <div class="box-body">&nbsp;</div>
                                       
                                                                       <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl22_text" runat="server">   Pendapatan Bagi Bulan</label>
                                    <div class="col-sm-8">
                                     <asp:DropDownList ID="DD_PBB" style="width:100%; font-size:13px;" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" runat="server" class="form-control select2 validate[optional]"> 
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl23_text" runat="server">   Tahun</label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="txt_tahu" runat="server" class="form-control validate[optional,custom[number]] uppercase"
                                                            MaxLength="4"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                                                     <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl24_text" runat="server">   Tarikh</label>
                                    <div class="col-sm-8">
                                      <div class="input-group">
                                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                         <asp:TextBox ID="TextBox3" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                placeholder="DD/MM/YYYY"></asp:TextBox>
                                                        </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="Label18" runat="server">   Jumlah Hari Bekerja</label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox20" runat="server" class="form-control validate[optional,custom[number]] uppercase"
                                                            MaxLength="4"></asp:TextBox>
                                           <asp:TextBox ID="TextBox25" Visible="false" runat="server" class="form-control validate[optional,custom[number]] uppercase"
                                                           ></asp:TextBox>
                                       
                                    </div>
                                </div>
                            </div>
                            
                                                          
                                 </div>
                                </div>
                                          <div class="row">
                             <div class="col-md-12">
                                        <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl25_text" runat="server">   Jenis KLM</label>
                                    <div class="col-sm-8">
                                      <asp:DropDownList ID="DropDownList1" style="width:100%; font-size:13px;" AutoPostBack="true" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" runat="server" class="form-control select2 validate[optional]">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="Label19" runat="server">   Jumlah Pemberat</label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox21" runat="server" class="form-control validate[optional"
                                                            MaxLength="4"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                              </div>
                                          <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl26_text" runat="server">   Jam / Unit</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox4" runat="server" class="form-control validate[optional,custom[number]]"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox14" runat="server" class="form-control validate[optional]"
                                                            Visible="false"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox16" runat="server" class="form-control validate[optional]"
                                                            Visible="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="Label20" runat="server">   Catatan</label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox22" runat="server" class="form-control validate[optional] uppercase"
                                                            MaxLength="255"></asp:TextBox>
                                      <%--  <br />
                                        <a href="#" data-toggle="modal" data-target="#myModal" style="text-decoration:underline; font-weight:bold;">(Semak Jadual Kerja Lebih Masa)</a>--%>
                                    </div>
                                </div>
                            </div>
                                                
                                 </div>
                                </div>
                                          
                                          <div id="myModal" class="modal fade" role="dialog">
  <div class="modal-dialog animate">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title"><strong>Tetapan Tarikh Cuti Ganti</strong></h4>
      </div>
      <div class="modal-body">
        <div class="box-cal box-primary">
               <div class="box-body no-padding">
                            <div class="box-body">&nbsp;</div>
                                      <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="Label2" runat="server"> No Kakitangan</label>
                                    <div class="col-sm-8 text-right">
                                        <asp:label ID="Label22" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                    <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="Label23" runat="server"> Tahun </label>
                                    <div class="col-sm-8 text-right">
                                        <asp:label ID="Label24" runat="server" class="uppercase"></asp:label>
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
                                        <asp:label ID="Label25" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="Label26" runat="server"> Bulan</label>
                                    <div class="col-sm-8 text-right">
                                        <asp:label ID="Label27" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                         
                                 </div>
                                </div>
                   </div>
                                <div class="row">
                             <div class="col-md-12">
                                     <asp:GridView ID="GridView7" runat="server" CssClass="table datatable dataTable no-footer uppercase" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="31" ShowFooter="true" GridLines="None" AutoGenerateColumns="false">
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
                                 </div>
                                    </div>
                                     <div class="box-body">&nbsp;</div>
                                       <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Kelayakan Cuti Ganti</h3>
                        </div>                         
                        <div class="box-body">&nbsp;</div>
                                     <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="Label28" runat="server"> Kelayakan Hari Cuti </label>
                                    <div class="col-sm-8">
                                                          <asp:TextBox ID="hari_cuti" runat="server" class="form-control validate[optional"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                      <div class="row" style="display:none;">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="Label3" runat="server"> Tarikh Mula Layak Cuti </label>
                                    <div class="col-sm-8">
                                          <div class="input-group">
                                                          <asp:TextBox ID="TextBox23" runat="server" class="form-control datepicker mydatepickerclass" autocomplete="off"
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
                                                           <asp:TextBox ID="get_id" Visible="false" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                                         <asp:TextBox ID="TextBox24" runat="server" class="form-control validate[optional] datepicker mydatepickerclass" autocomplete="off" placeholder="DD/MM/YYYY"></asp:TextBox>
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            </div>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>                                    
          </div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
      </div>
    </div>

  </div>
</div>
                                                                     
                                                                     <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                               <asp:Button ID="Button6" runat="server" class="btn btn-danger" Text="Simpan" Type="submit"
                                                            UseSubmitBehavior="false" OnClick="btn_mklm_simp_Click"/>
                                                        <asp:Button ID="Button9" runat="server" Text="Kemaskini" Visible="false" class="btn btn-danger"
                                                            UseSubmitBehavior="false" OnClick="btn_mklm_kem_Click" />
                                                        <asp:Button ID="Button11" runat="server" Text="Hapus" class="btn btn-warning" UseSubmitBehavior="false"
                                                            OnClick="btn_mklm_hapus_Click" OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;"/>
                                <asp:Button ID="Button17" runat="server" class="btn btn-default" Text="Set Semula" Type="submit"
                                                                                    OnClick="rset_Click4" />
                                 </div>
                           </div>
                               </div>
                                                                    <div class="box-body">&nbsp;</div>
                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                    
           <div class="col-md-12 box-body">
                                 <asp:GridView ID="GridView5" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging_4">
                                      <PagerStyle CssClass="pager" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="BIL">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                            ItemStyle-Width="150" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="TARIKH">
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" ID="lnkView" OnClick="lnkView_Click_ro" Font-Bold Font-Underline>
                                                                            <asp:Label ID="Label5_1" runat="server" Text='<%# Eval("otl_work_dt") %>' CssClass="uppercase"></asp:Label>
                                                                            <asp:Label ID="crt_dt" Visible="false" runat="server" Text='<%# Eval("otl_crt_dt") %>'
                                                                                CssClass="uppercase"></asp:Label>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="JENIS KLM">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label5_2" runat="server" Text='<%# Eval("typeklm_desc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="JAM / UNIT">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label5_3" runat="server" Text='<%# Eval("otl_work_hour") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="JUMLAH (RM)">
                                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label5_4" runat="server" Text='<%# Eval("otl_ot_amt","{0:n}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="STATUS">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label5_5" runat="server" Text='<%# Eval("stsname") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="CATATAN">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label5_6" runat="server" Text='<%# Eval("otl_remark") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                   <asp:TemplateField HeaderText="Hapus">
                                                                                            <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                 <asp:CheckBox ID="RadioButton5" runat="server"/>
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
                                                                  
                                                                    <div class="box-body">&nbsp;</div>

                                                                    </div>
                                    <div role="tabpanel" class="tab-pane" runat="server" id="p5">
                        <div class="box-body">&nbsp;</div>
                                                                       <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl27_text" runat="server">   Tarikh Mula <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                   <div class="input-group">
                                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                         <asp:TextBox ID="TextBox1" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                placeholder="DD/MM/YYYY"></asp:TextBox>
                                                        </div>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl28_text" runat="server">   Tarikh Akhir <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                      <div class="input-group">
                                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        <asp:TextBox ID="TextBox8" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                placeholder="DD/MM/YYYY"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl29_text" runat="server">   Bonus Tahunan (RM)</label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="TextBox5" style="text-align:right;" runat="server" class="form-control validate[optional,custom[number]] au_amt3"  onblur="addTotal_bk4(this)"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl30_text" runat="server">   Bonus KPI (RM)</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox7" style="text-align:right;" runat="server" class="form-control validate[optional,custom[number]] au_amt4"  onblur="addTotal_bk5(this)"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox6" Visible="false" runat="server" class="form-control validate[optional]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                                          
                                 </div>
                                </div>
                                         
                                                                     
                                                                     <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                             <asp:Button ID="Button12" runat="server" class="btn btn-danger" Text="Simpan" Type="submit"
                                                            UseSubmitBehavior="false" OnClick="simp_Click_bns" />
                                                        <asp:Button ID="Button13" runat="server" Text="Kemaskini" Visible="false" class="btn btn-danger"
                                                            UseSubmitBehavior="false" OnClick="kem_Click_bns" />
                                                        <asp:Button ID="Button14" runat="server" Text="Hapus" class="btn btn-warning" UseSubmitBehavior="false"
                                                            OnClick="hapus_Click_bns" OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;"/>
                                 <asp:Button ID="Button18" runat="server" class="btn btn-default" Text="Set Semula" Type="submit"
                                                                                    OnClick="rset_Click5" />
                                 </div>
                           </div>
                               </div>
                                                                    <div class="box-body">&nbsp;</div>
                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                 <asp:GridView ID="GridView4" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging_5">
                                      <PagerStyle CssClass="pager" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="BIL" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                            ItemStyle-Width="150" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="TARIKH MULA">
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" ID="lnkView1" OnClick="lnkView_Click_bonus" Font-Bold Font-Underline>
                                                                            <asp:Label ID="lb_51" runat="server" Text='<%# Eval("bns_eff_dt") %>' CssClass="uppercase"></asp:Label>
                                                                            <asp:Label ID="lb_52" Visible="false" runat="server" Text='<%# Eval("Id") %>'
                                                                                CssClass="uppercase"></asp:Label>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="Tarikh Akhir">
                                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lb_55" runat="server" Text='<%# Eval("bns_end_dt") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Bonus Tahunan (RM)">
                                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lb_53" runat="server" Text='<%# Eval("bns_amt","{0:n}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Bonus KPI (RM)">
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lb_54" runat="server" Text='<%# Eval("bns_kpi_amt","{0:n}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                               <asp:TemplateField HeaderText="Hapus">
                                                                                            <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                 <asp:CheckBox ID="rd_bonus" runat="server"/>
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
                                                                    </div>
                                     <div role="tabpanel" class="tab-pane" runat="server" id="p7">
                        <div class="box-body">&nbsp;</div>
                                                                       <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="Label1" runat="server">   Bagi Bulan <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                     <asp:DropDownList ID="DropDownList2" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="Label6" runat="server">   Tahun </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox9" runat="server" class="form-control validate[optional,custom[number]] uppercase"
                                                            MaxLength="4"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                                                     <div class="row">
                             <div class="col-md-12">
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="Label7" runat="server">   Jumlah (RM) <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox10" style="text-align:right;" runat="server" class="form-control validate[optional,custom[number]] au_amt6"  onblur="addTotal_bk6(this)" AutoComplete="off"></asp:TextBox>
                                        <asp:TextBox ID="TextBox11" runat="server" class="form-control" Visible="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="Label8" runat="server">   Sebab <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                      <asp:DropDownList ID="DropDownList3" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="Label9" runat="server">   Catatan</label>
                                    <div class="col-sm-8">
                                        <%--<asp:TextBox ID="TextBox11" runat="server" class="form-control validate[optional]" Rows="3" MaxLength="1500"></asp:TextBox>--%>
                                        <textarea id="tung_catatan" class="form-control validate[optional] uppercase" rows="4" runat="server" maxlength="1500"></textarea>
                                    </div>
                                </div>
                            </div>
                                                
                                 </div>
                                </div>

                                                                     
                                                                     <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                               <asp:Button ID="Button20" runat="server" class="btn btn-danger" Text="Simpan" Type="submit"
                                                            UseSubmitBehavior="false" OnClick="btn_tung_simp_Click"/>
                                                        <asp:Button ID="Button21" runat="server" Text="Kemaskini" Visible="false" class="btn btn-danger"
                                                            UseSubmitBehavior="false" OnClick="btn_tung_kem_Click" />
                                                        <asp:Button ID="Button22" runat="server" Text="Hapus" class="btn btn-warning" UseSubmitBehavior="false"
                                                            OnClick="btn_tung_hapus_Click" OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;"/>
                                <asp:Button ID="Button23" runat="server" class="btn btn-default" Text="Set Semula" Type="submit"
                                                                                    OnClick="rset_Click_tung" />
                                 </div>
                           </div>
                               </div>
                                                                    <div class="box-body">&nbsp;</div>
                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                 <asp:GridView ID="GridView6" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging_6">
                                      <PagerStyle CssClass="pager" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="BIL" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                            ItemStyle-Width="150" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="TAHUN">
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" ID="lnkView" OnClick="lnkView_Click_tung" Font-Bold Font-Underline>
                                                                            <asp:Label ID="tun_yr" runat="server" Text='<%# Eval("tun_year") %>' CssClass="uppercase"></asp:Label>
                                                                            <asp:Label ID="tun_crt_dt" Visible="false" runat="server" Text='<%# Eval("Id") %>'
                                                                                CssClass="uppercase"></asp:Label>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="Bulan">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="tun_5" runat="server" Text='<%# Eval("tun_mnth_desc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="SEBAB">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="tun_2" runat="server" Text='<%# Eval("hr_tung_desc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="JUMLAH (RM)">
                                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="tun_4" runat="server" Text='<%# Eval("tun_amt","{0:n}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                              <%--  <asp:TemplateField HeaderText="STATUS">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label5_5" runat="server" Text='<%# Eval("stsname") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                                   <asp:TemplateField HeaderText="Hapus">
                                                                                            <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                 <asp:CheckBox ID="RadioButton5" runat="server"/>
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


