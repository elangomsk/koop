<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_tuntutan.aspx.cs" Inherits="HR_tuntutan" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
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
                        <h1 id="h1_tag" runat="server">  Tuntutan Kakitangan </h1>
                        <ol class="breadcrumb">
                            <li id="bb1_text" runat="server"><a href="#"><i class="fa fa-dashboard"></i>Sumber Manusia</a></li>
                            <li class="active" id="bb2_text" runat="server">  Tuntutan Kakitangan  </li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
       <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                       
                        <div class="form-horizontal">
                              <div style="display:none;">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl1_text" runat="server"> No Kakitangan</label>
                                    <div class="col-sm-8 text-bold text-right">
                                       <asp:label ID="Applcn_no1" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                           
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl2_text" runat="server"> No KP Baru / Pasport </label>
                                    <div class="col-sm-8 text-right">
                                        <asp:label ID="TextBox14" runat="server" class=" uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                  </div>
                                </div>

                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl3_text" runat="server"> Nama Syarikat / Organisasi</label>
                                    <div class="col-sm-8 text-right">
                                        <asp:label ID="txt_org" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                           
                                
                            
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl4_text" runat="server">   Perniagaan  </label>
                                    <div class="col-sm-8 text-right">
                                         <asp:label ID="TextBox5" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                  </div>
                                </div>
                                
                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl5_text" runat="server"> Jabatan</label>
                                    <div class="col-sm-8 text-right">
                                       <asp:label ID="TextBox16" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl6_text" runat="server">  Gred  </label>
                                    <div class="col-sm-8 text-right">
                                        <asp:label ID="TextBox15" runat="server" class="validate[optional] "></asp:label>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl7_text" runat="server"> Jawatan </label>
                                    <div class="col-sm-8 text-right">
                                        <asp:label ID="TextBox3" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                   <hr />
                                  </div>
                               
                            <div class="box-header with-border">
                            <h3 class="box-title" id="h3_tag2" runat="server">Tuntutan</h3>
                        </div>                         
                        <div class="box-body">&nbsp;</div>
                                                                       <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl14_text" runat="server">   Tarikh Reciept <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                      <div class="input-group">
                                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                          <asp:TextBox ID="ET_mdate" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" autocomplete="off"
                                                                                        placeholder="DD/MM/YYYY"></asp:TextBox>
                                                        </div>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl15_text" runat="server">   Tarikh Apply</label>
                                    <div class="col-sm-8">
                                      <div class="input-group">
                                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                          <asp:TextBox ID="ET_sdate" runat="server" class="form-control validate[optional" autocomplete="off"
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl16_text" runat="server">   Jenis Tuntutan</label>
                                    <div class="col-sm-8">
                                      <asp:DropDownList ID="ET_jelaun" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" OnSelectedIndexChanged="dd_sel_txtchanged1" AutoPostBack="true" >
                                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                           
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="Label7" runat="server">   Jumlah Terkini (RM)</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox2" style="text-align:right;" runat="server" ReadOnly="true" class="form-control validate[optionalcustom[number]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                                          
                                 </div>
                                </div>
                              <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl17_text" runat="server">   Amaun (RM)</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="ET_amaun" style="text-align:right;" runat="server" class="form-control validate[optionalcustom[number]" OnTextChanged="dd_sel_txtchanged"  autocomplete="off" AutoPostBack="true"></asp:TextBox>
                                        
                                        
                                                                                <asp:TextBox ID="ET_rno" Visible="false" runat="server" class="form-control validate[optional]"
                                                                                    MaxLength="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="Label8" runat="server"> Sebab </label>
                                    <div class="col-sm-8 text-right">
                                         <asp:TextBox ID="TextBox4" runat="server" autocomplete="off" class="form-control uppercase" MaxLength="250"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                                    
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                             <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="Label1" runat="server">   Dokumen <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                      <asp:FileUpload ID="FileUpload1"  runat="server"/> 
                                        <asp:TextBox ID="TextBox1" style="display:none;" runat="server" class="form-control"></asp:TextBox>
                                                                <%--<br />
                                                                <asp:Label ID="Label6" runat="server" Text="Makluman : Sila pilih fail format Excel (.xls) sahaja." ForeColor="Red" Font-Size="X-Small"></asp:Label> --%>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                                         <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="30" ShowFooter="false" GridLines="None">
                                                             <PagerStyle CssClass="pager" />
            <Columns>
                 <asp:TemplateField HeaderText="BIL" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="2%"/> 
                                                                        <%--<asp:RadioButton ID="RadioButton1" runat="server" onclick = "RadioCheck(this);" />--%>                                                                        
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                 <asp:TemplateField HeaderText="File Name">
                                                             <ItemStyle HorizontalAlign="center" Width="50%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl1" runat="server" Text='<%# Eval("td1_name") %>'></asp:Label>
                                                                       <asp:Label ID="lbl_id" Visible="false" runat="server" Text='<%# Bind("ID") %>' CssClass="uppercase"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
               <asp:TemplateField HeaderText="Download" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                    <ItemTemplate>
                      <asp:LinkButton runat="server" ID="lnkView11" OnClick="lnkView_Click11">
                                                                <asp:Label ID="lbl3" runat="server" Text='Download'></asp:Label>
                                                                </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" OnClick="DeleteFile"
                            CommandArgument='<%# Eval("Id") %>' OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;"></asp:LinkButton>
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
                                 <asp:GridView ID="GridView2" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging_2" OnRowDataBound="GridView1_RowDataBound">
                                      <PagerStyle CssClass="pager" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="BIL">
                                                                                            <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                                                    ItemStyle-Width="150" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Tarikh Reciept">
                                                                                            <ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton runat="server" ID="lnkView" OnClick="lnkView_Click1" Font-Bold Font-Underline>
                                                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("clm_rec_dt","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                                                    <asp:Label ID="est_no" Visible="false" runat="server" Text='<%# Eval("clm_staff_no") %>'></asp:Label>
                                                                                                    <asp:Label ID="eall_cd" Visible="false" runat="server" Text='<%# Eval("clm_claim_cd") %>'></asp:Label>
                                                                                                    <asp:Label ID="fxactdt" Visible="false" runat="server" Text='<%# Eval("clm_rec_dt") %>'></asp:Label>
                                                                                                    <asp:Label ID="app_sts" Visible="false" runat="server" Text='<%# Eval("clm_approve_sts_cd") %>'></asp:Label>
                                                                                                </asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Tarikh Apply">
                                                                                            <ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("clm_app_dt","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="JENIS TUNTUTAN">
                                                                                            <ItemStyle HorizontalAlign="Left" Width="25%"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("hr_tun_desc") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="SEBAB">
                                                                                            <ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label4_seb" runat="server" Text='<%# Eval("clm_sebap") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Amaun (RM)">
                                                                                            <ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("clm_claim_amt","{0:n}") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="Baki / Jumlah Terkini (RM)">
                                                                                            <ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label5_bamt" runat="server" Text='<%# Eval("clm_balance_amt","{0:n}") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                          <asp:TemplateField HeaderText="Status">
                                                                                            <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label5_sts" runat="server" Text='<%# Eval("sts_desc") %>'></asp:Label>
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
                                                             
                           <div class="box-body">&nbsp;
                                    </div>

                        </div>

                    </div>
                </div>
            </div>
            <!-- /.row -->
             </ContentTemplate>
             <Triggers>
            <asp:PostBackTrigger ControlID="Button2"  />
                 <asp:PostBackTrigger ControlID="GridView1"  />
                 </Triggers>
    </asp:UpdatePanel>
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
   
</asp:Content>



