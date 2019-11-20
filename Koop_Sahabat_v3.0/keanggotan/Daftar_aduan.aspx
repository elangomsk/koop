<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../keanggotan/Daftar_aduan.aspx.cs" Inherits="Daftar_aduan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server" ScriptMode="Release">
    </asp:ScriptManager>

     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>    <asp:Label ID="ps_lbl1" runat="server"></asp:Label>      </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i> <asp:Label ID="ps_lbl2" runat="server"></asp:Label> </a></li>
                            <li class="active">   <asp:Label ID="ps_lbl3" runat="server"></asp:Label>      </li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
      
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title"><asp:Label ID="ps_lbl4" runat="server"></asp:Label> </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  <asp:Label ID="ps_lbl5" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="Txtnokp" runat="server" class="form-control validate[optional] uppercase" maxlength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   <asp:Label ID="ps_lbl6" runat="server"></asp:Label>  </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="Txtnama" runat="server" class="form-control validate[optional,custom[textSp]]" style="text-transform:uppercase;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                
                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl7" runat="server"></asp:Label>  </label>
                                    <div class="col-sm-8">
                                        <%-- <asp:DropDownList ID="ddcaw" runat="server" class="form-control select2 selectpicker">
                                                                    </asp:DropDownList>--%>
                                         <asp:TextBox ID="TextBox1" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  <asp:Label ID="ps_lbl8" runat="server"></asp:Label>   </label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="Txtnotel" runat="server" class="form-control validate[optional,custom[phone]]" maxlength="11"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  <asp:Label ID="ps_lbl9" runat="server"></asp:Label>  </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TxtTaradu" runat="server" class="form-control datepicker mydatepickerclass"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  <asp:Label ID="ps_lbl10" runat="server"></asp:Label>   </label>
                                    <div class="col-sm-8">
                                           <asp:DropDownList ID="dd_kat" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                    <asp:ListItem Value="">--- PILIH ---</asp:ListItem>
                                                                    <asp:ListItem Value="01">NORMAL</asp:ListItem>
                                                                    <asp:ListItem Value="02">SEPARA KRITIKAL</asp:ListItem>
                                                                    <asp:ListItem Value="03">KRITIKAL</asp:ListItem>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">  <asp:Label ID="ps_lbl11" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                               <ContentTemplate>
                                                            <div class="col-md-4 col-sm-4">
                                                            <asp:RadioButton ID="RadioButton1" runat="server"  Text=" Status Anggota"  AutoPostBack="true"  OnCheckedChanged="RadioButton1_CheckedChanged"
                                                                   />
                                                          <%--  <label>Warganegara</label>--%>
                                                            </div>
                                                            <div class="col-md-4 col-sm-5"> 
                                                            <asp:RadioButton ID="RadioButton2" runat="server" Text=" Caruman Anggota"  AutoPostBack="true"  OnCheckedChanged="RadioButton2_CheckedChanged"
                                                                    />
                                                          <%--  <label>Bukan Warganegara</label>--%>
                                                            </div>
                                                            <div class="col-md-4 col-sm-6">
                                                            <asp:RadioButton ID="RadioButton3" runat="server" Text=" Hal-hal Lain"  AutoPostBack="true"  OnCheckedChanged="RadioButton3_CheckedChanged"
                                                                   /> 
                                                            <%--<label>Pemustautin Tetap</label>--%>
                                                            </div>
                                                            </ContentTemplate>
                                                             </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   <asp:Label ID="ps_lbl15" runat="server"></asp:Label>  </label>
                                    <div class="col-sm-8">
                                         <textarea ID="txtdesc" runat="server" rows="3" class="form-control uppercase"></textarea>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>



                 
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                              <asp:Button ID="btnsave" runat="server" class="btn btn-danger" Text="Simpan" UseSubmitBehavior="false" onclick="Button1_Click" />
                                                            <asp:Button ID="btn_reset" runat="server" class="btn btn-default" Text="Set Semula" usesubmitbehavior="false" onclick="btn_reset_Click" />
                                <asp:Button ID="Button1" runat="server" class="btn btn-default" Visible="false" Text="Kembali" usesubmitbehavior="false" onclick="Click_bck" />
                            </div>
                           </div>
                               </div>
                           <div class="box-body">&nbsp;
                                    </div>
                             <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="30" ShowFooter="false" GridLines="None" OnPageIndexChanging="grdView_PageIndexChanging">
                                            <Columns>
                                                <%--<asp:BoundField DataField="comp_id" HeaderText="BIL" ReadOnly="true" />--%>
                                                <asp:TemplateField HeaderText="BIL" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="NO KP BARU" ItemStyle-HorizontalAlign="Center">  
                                            <ItemTemplate>  
                                                <asp:Label ID="Label21" runat="server" Text='<%# Eval("com_new_icno") %>'></asp:Label>  
                                                <asp:Label ID="Label1" runat="server" Visible="false" Text='<%# Eval("comp_id") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NAMA">  
                                            <ItemTemplate>  
                                                <asp:Label ID="Label22" runat="server" Text='<%# Eval("mem_name") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TARIKH" ItemStyle-HorizontalAlign="Center">  
                                            <ItemTemplate>  
                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("com_dt", "{0:dd/MM/yyyy}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="KATEGORI ADUAN">  
                                            <ItemTemplate>  
                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("com_type_ind").ToString() == "SA" ? "Status Anggota" : Eval("com_type_ind").ToString() == "CA" ? "Caruman Anggota" : "Hal-hal Lain"%>'></asp:Label>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="cawangan_name" HeaderText="CAWANGAN" ReadOnly="true" />
                                                <asp:TemplateField HeaderText="STATUS">  
                                            <ItemTemplate>  
                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("KETERANGAN") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Action" ItemStyle-Width="5%">                                                     
                                            <ItemTemplate>  
                                                <asp:LinkButton ID="LinkButton1" runat="server" ToolTip="Delete" Text='' OnClick="hapus_click2" OnClientClick="if (!confirm('Are you Confirm to Cancel?')) return false;" Font-Bold>
                                                                                                     Batal
                                                                                                </asp:LinkButton>
                                                  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                            </Columns>
                                     <EmptyDataTemplate><center><strong>Rekod Dijumpai Dalam Julat Tarikh Yang Dimasukkan</strong></center></EmptyDataTemplate>
                                           <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />                                                       
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                       
                                        </asp:GridView>
               </div>
          </div>
                        </div>

                    </div>
                </div>
            </div>
            <!-- /.row -->

        
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
    </ContentTemplate>
            
    </asp:UpdatePanel>
</asp:Content>






