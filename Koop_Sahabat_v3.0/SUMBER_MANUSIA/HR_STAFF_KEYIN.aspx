<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_STAFF_KEYIN.aspx.cs" Inherits="HR_STAFF_KEYIN" %>

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
                        <h1 id="h1_tag" runat="server"> Penilaian Prestasi  </h1>
                        <ol class="breadcrumb">
                            <li id="bb1_text" runat="server"><a href="#"><i class="fa fa-dashboard"></i>Sumber manusia</a></li>
                            <li class="active" id="bb2_text" runat="server">Kemasukan Maklumat Penilaian Prestasi</li>
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
                            <h3 class="box-title" id="h3_tag" runat="server">Maklumat Peribadi</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl1_text" runat="server">No Kakitangan </label>
                                    <div class="col-sm-8 text-right text-bold">
                                        <asp:label ID="txt_stffno" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                           
                                 
                             
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl2_text" runat="server">   Organisasi </label>
                                    <div class="col-sm-8 text-right">
                                        <asp:label ID="txt_org" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                            </div>
                                </div>
                                
                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl3_text" runat="server"> Nama </label>
                                    <div class="col-sm-8 text-right">
                                        <asp:label ID="txt_nama" runat="server" disabled="disabled" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                 

                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl4_text" runat="server"> Gred </label>
                                    <div class="col-sm-8 text-right">
                                        <asp:label ID="txt_gred" runat="server" disabled="disabled" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl5_text" runat="server"> Jabatan </label>
                                    <div class="col-sm-8 text-right">
                                         <asp:label ID="txt_jaba" runat="server" disabled="disabled" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl6_text" runat="server"> Jawatan </label>
                                    <div class="col-sm-8 text-right">
                                        <asp:label ID="txt_jawa" runat="server" disabled="disabled" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>


                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl7_text" runat="server">  Kategori Jawatan  </label>
                                    <div class="col-sm-8 text-right">
                                         <asp:label ID="txt_ketogjawa" runat="server" disabled="disabled" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                

                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl8_text" runat="server">  Unit </label>
                                    <div class="col-sm-8 text-right">
                                       <asp:label ID="txt_unit" runat="server" disabled="disabled" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                  </div>
                                </div>


                             <div class="box-header with-border">
                            <h3 class="box-title" id="h3_tag2" runat="server">Kriteria Pennilaian Prestasi</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>




                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl9_text" runat="server">  Penilaian Bagi Tahun</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox2" runat="server" class="form-control validate[optional,custom[number]] uppercase" MaxLength="4"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl10_text" runat="server">  Bahagian  <%--<span color:red>*</span>--%> </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="DD_Bahagi" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" AutoPostBack = "true"  onselectedindexchanged="DD_Bahagi_SelectedIndexChanged">
                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl11_text" runat="server"> Subjek<%--<span color:red> *</span>--%> </label>
                                    <div class="col-sm-8">
                                      <asp:DropDownList ID="DD_Subje" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                </asp:DropDownList>
                                                <asp:TextBox ID="TextBox1" runat="server" Visible="false" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">  <asp:Button ID="btn_senarai" runat="server" class="btn btn-primary" UseSubmitBehavior="false" Text="Senarai" onclick="btn_senarai_Click" />
                                                <asp:Button ID="Button3" runat="server" class="btn btn-default" Text="Set Semula" UseSubmitBehavior="false" onclick="rst_clk"  /> </label>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="box-body">&nbsp;
                                    </div>
                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                   <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="35" ShowFooter="false" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging">
                                       <PagerStyle CssClass="pager" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="BIL">
                                                                <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                    <ItemTemplate>
                                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="150"/>
                                                                        <%--<asp:RadioButton ID="RadioButton1" runat="server" onclick = "RadioCheck(this);" />--%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="BAHAGIAN">
                                                                <ItemStyle HorizontalAlign="Left" Width="7%"></ItemStyle>
                                                                    <ItemTemplate>
                                                                    <%--<asp:LinkButton runat="server" ID="lnkView" OnClick="lnkView_Click">--%>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("GRID1") %>'></asp:Label>
                                                                        <%--</asp:LinkButton>--%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="SUBJEK">
                                                                <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("GRID2") %>' CssClass="uppercase"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PEMBERAT (%)" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("GRID4") %>' CssClass="uppercase"></asp:Label>
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

                              <div class="box-header with-border">
                            <h3 class="box-title" id="h3_tag3" runat="server">Ulasan Kakitangan Yang Dinilai</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>

                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl12_text" runat="server">Ulasan </label>
                                    <div class="col-sm-8">
                                        <textarea id="txt_ulasan" cols="20" rows="3" runat="server" class="form-control validate[optional] uppercase" maxlength="1000"></textarea>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Button ID="btn_simp" runat="server" class="btn btn-danger" Text="Simpan" Type="submit" UseSubmitBehavior="false"  OnClick="btn_simp_Click"  /> 
                                        <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Kembali" OnClick="Click_bck" UseSubmitBehavior="false" /> 
                                    </label>
                                    <div class="col-sm-8">&nbsp;</div>
                                </div>
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





