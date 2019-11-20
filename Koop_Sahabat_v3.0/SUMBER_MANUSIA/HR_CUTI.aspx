<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_CUTI.aspx.cs" Inherits="HR_CUTI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        <script>
           
     $(function () {
         $('.select2').select2();
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
                        <h1 id="h1_tag" runat="server">   Selenggara Kalendar Cuti  </h1>
                        <ol class="breadcrumb">
                            <li id="bb1_text" runat="server"><a href="#"><i class="fa fa-dashboard"></i>Sumber Manusia</a></li>
                            <li class="active" id="bb2_text" runat="server"> Selenggara Kalendar Cuti </li>
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
                            <h3 class="box-title" id="h3_tag" runat="server">Kalendar Cuti </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl1_text" runat="server">  Nama Syarikat / Nama Organisasi</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="dd_org" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" AutoPostBack="true" OnSelectedIndexChanged="sel_negeri">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl2_text" runat="server">   Jenis Cuti  </label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="dd_jcuti" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="dd_jeniscuti">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl3_text" runat="server"> Negeri   </label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="DD_NegriBind1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                </asp:DropDownList>
                                                                <asp:TextBox ID="ver_id" Style="display: none;" runat="server" class="form-control"></asp:TextBox>
                                        <br /><br />
                                        <label><asp:CheckBox ID="check_sn" CssClass="mycheckbox" runat="server" />
                                                                <strong id="lbl4_text" runat="server">Semua Negeri </strong></label>
                                    </div>
                                </div>
                            </div>
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <asp:TextBox ID="lbl_name" runat="server" class="form-control validate[optional] uppercase" Visible="false"></asp:TextBox>
                                <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Papar" UseSubmitBehavior="false"
                                                                    OnClick="clk_paper" />
                                                                <asp:Button ID="Button4" runat="server" class="btn btn-default" Text="Set Semula"
                                                                    UseSubmitBehavior="false" OnClick="rset_click" />
                                                                
                                                                <asp:Button ID="Button5" runat="server" class="btn btn-danger" Text="Batal" UseSubmitBehavior="false"
                                                                    OnClick="batal_Click" OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;"/>
                                                                
                                                                <asp:Button ID="Button2" runat="server" class="btn btn-warning" UseSubmitBehavior="false"
                                                                    Text="Cetak" OnClick="click_pdf" />
                                </div>
                            </div>
                                 </div>
                                </div>

                            <div id="sow_cnt" runat="server" style="display: none;">
                                   <div class="box-header with-border">
                            <h3 class="box-title"><asp:Label ID="M_title" runat="server"></asp:Label></h3>
                        </div>
                                </div>
                            
                            <div id="hm" runat="server">
                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl5_text" runat="server">   Tahun </label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="dd_tahun" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional] uppercase">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">    </label>  
                                        <div class="col-md-2 col-sm-1">
                                                                    <label>
                                                                        <asp:CheckBox ID="CheckBox3" CssClass="mycheckbox" runat="server" />
                                                                        Ahad
                                                                    </label>
                                                                </div>
                                                                <div class="col-md-2 col-sm-2">
                                                                    <label>
                                                                        <asp:CheckBox ID="CheckBox4" CssClass="mycheckbox" runat="server" />
                                                                        Isnin
                                                                    </label>
                                                                </div>
                                                                <div class="col-md-2 col-sm-3">
                                                                    <label>
                                                                        <asp:CheckBox ID="CheckBox6" CssClass="mycheckbox" runat="server" />
                                                                        Selasa
                                                                    </label>
                                                                </div>
                                                                <div class="col-md-2 col-sm-4">
                                                                    <label>
                                                                        <asp:CheckBox ID="CheckBox8" CssClass="mycheckbox" runat="server" />
                                                                        Rabu
                                                                    </label>
                                                                </div> 
                                      

                                </div>
                            </div>
                                
                                 </div>
                                </div>
                                <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">    </label>  
                                          <div class="col-md-2 col-sm-1">
                                                                    <label>
                                                                        <asp:CheckBox ID="CheckBox2" CssClass="mycheckbox" runat="server" />
                                                                        Khamis
                                                                    </label>
                                                                </div>
                                                                <div class="col-md-2 col-sm-2">
                                                                    <label>
                                                                        <asp:CheckBox ID="CheckBox5" CssClass="mycheckbox" runat="server" />
                                                                        Jumaat
                                                                    </label>
                                                                </div>
                                                                <div class="col-md-2 col-sm-3">
                                                                    <label>
                                                                        <asp:CheckBox ID="CheckBox7" CssClass="mycheckbox" runat="server" />
                                                                        Sabtu
                                                                    </label>
                                                                </div>                                                           

                                </div>
                            </div>
                                
                                 </div>
                                </div>
                                </div>
                             <div id="cl" runat="server">

                                   <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Tarikh Dari   </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                                                        <asp:TextBox ID="td_date" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                            placeholder="DD/MM/YYYY"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                    </div>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Tarikh Sehingga   </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                                                        <asp:TextBox ID="ts_date" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                            placeholder="DD/MM/YYYY"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Catatan  </label>
                                    <div class="col-sm-8">
                                          <textarea id="TextBox7" runat="server" class="form-control uppercase" maxlength="100"></textarea>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                 </div>
                            <div id="cl_simp" runat="server" style="display:none;">
                                 <div class="row">
                             <div class="col-md-12">
                               <div class="col-md-12 box-body" style="text-align:center;">
                                                                <asp:Button ID="Button3" Visible="false" runat="server" class="btn btn-danger" Text="Simpan" UseSubmitBehavior="false"
                                                                    OnClick="clk_insert" />
                                
                            </div>
                                 </div>
                                </div>
                            </div>
                            <hr />
                             <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                 <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging">
                                      <PagerStyle CssClass="pager" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="BIL">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                                    ItemStyle-Width="150" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                       <%-- <asp:TemplateField HeaderText="genid" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                                                            <ItemStyle HorizontalAlign="Left" Width="25%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                               
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>--%>
                                                                        <asp:TemplateField HeaderText="ORGANISASI">
                                                                            <ItemStyle HorizontalAlign="Left" Width="25%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("org_name") %>'></asp:Label>
                                                                                 <asp:Label ID="Label21" runat="server" Visible="false" Text='<%# Eval("hol_gen_id") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="NEGERI">
                                                                            <ItemStyle HorizontalAlign="Left" Width="25%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("hr_negeri_desc") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="JENIS CUTI">
                                                                            <ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("hr_jenis_desc") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="TARIKH CUTI">
                                                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("hol_dt") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="CATATAN">
                                                                            <ItemStyle HorizontalAlign="Left" Width="25%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label51" runat="server" Text='<%# Eval("hol_remark") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="BATAL">
                                                                            <ItemStyle HorizontalAlign="center" Width="5%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkStatus" runat="server" />
                                                                                <asp:Label ID="oid" runat="server" Visible="false" Text='<%# Eval("hol_org_id") %>'></asp:Label>
                                                                                <asp:Label ID="hid" runat="server" Visible="false" Text='<%# Eval("hol_holiday_cd") %>'></asp:Label>
                                                                                <asp:Label ID="sid" runat="server" Visible="false" Text='<%# Eval("hol_state_cd") %>'></asp:Label>
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
                              <div class="row">
                                                        <div class="col-md-12 col-sm-2" style="text-align: center; display:none;">
                                                            <rsweb:ReportViewer ID="Rptviwer_kelulusan" runat="server">
                                                            </rsweb:ReportViewer>
                                                            <asp:Label runat="server" ID="ReportErrorMessage" Visible="false" CssClass="report-error-message"></asp:Label>
                                                        </div>
                                                    </div>
                           <div class="box-body">&nbsp;</div>

                        </div>

                    </div>
                </div>
            </div>
            <!-- /.row -->

         </ContentTemplate>
             <Triggers>
               <asp:PostBackTrigger ControlID="Button2"  />
           </Triggers>
    </asp:UpdatePanel>
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
   
</asp:Content>


