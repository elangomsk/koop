<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../PELABURAN_ANGGOTA/PP_Guaman_penjamin.aspx.cs" Inherits="PP_Guaman_penjamin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" style="height: 895px;">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>  Tindakan Litigasi </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Pelaburan Anggota </a></li>
                            <li class="active"> Tindakan Litigasi </li>
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
                            <h3 class="box-title"> Maklumat Pelanggan </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> No Permohonan <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="txtappno" runat="server"  class="form-control validate[optional] uppercase" MaxLength="11" ></asp:TextBox>
                                         <asp:Panel ID="autocompleteDropDownPanel" runat="server" ScrollBars="Auto" Height="150px"
                                                            Font-Size="Medium" HorizontalAlign="Left" Wrap="False" />
                                                        <cc1:AutoCompleteExtender ServiceMethod="GetCompletionList" MinimumPrefixLength="1"
                                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtappno"
                                                            ID="AutoCompleteExtender1" CompletionListElementID="autocompleteDropDownPanel"
                                                            CompletionListCssClass="form-control uppercase" runat="server" FirstRowSelected="false">
                                                        </cc1:AutoCompleteExtender>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">                                    
                                    <div class="col-sm-8">
                                       <asp:Button ID="Button4" runat="server" class="btn btn-primary" Text="Carian" usesubmitbehavior="false" OnClick="btnsrch_Click"  />
                                                        <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" usesubmitbehavior="false" OnClick="Button1_Click" />
                                    </div>
                                </div>
                            </div>
                                  </div>
                                </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama</label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="txtname" runat="server" class="form-control validate[optional,custom[textSp]] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> No KP Baru <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                            <asp:TextBox ID="txtnokp" runat="server" class="form-control validate[optional] uppercase" MaxLength="12"></asp:TextBox>
                                </div>
                            </div>
                                </div>
                                 </div>
                            </div>


                            <%-- <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Wilayah / Pejabat </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtwila" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Cawangan / Jabatan</label>
                                    <div class="col-sm-8">
                                    <asp:TextBox ID="txtcawa" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>--%>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Amaun Pengeluaran (RM) </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txtamaun" runat="server" class="form-control" style="text-align:right;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tempoh (Bulan)</label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="txttemp" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>


                              <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Tindakan Mahkamah </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>

                                  <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Kategori Defendan <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="ddlkd1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Jenis Tindakan <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                    <asp:DropDownList ID="ddljt2" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>

                                  <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Panel Peguam <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="txtpp3" runat="server"  class="form-control validate[optional,custom[textSp1]] uppercase" MaxLength="150" ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Tarikh Permohonan Tindakan <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                   <div class="input-group">
                                                                   
                                                         <asp:TextBox ID="txttpt4" runat="server"  class="form-control validate[optional] datepicker  mydatepickerclass"  placeholder="DD/MM/YYYY"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Tarikh Perbicaraan <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                     <div class="input-group">
                                                                   
                                                       <asp:TextBox ID="txttp5" runat="server"  class="form-control validate[optional] datepicker  mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
                                          <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                       </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">    No Daftar Kes <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txtndk" runat="server"  class="form-control validate[optional,custom[nodaft1]] uppercase" MaxLength="50" ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>

                                  <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Keputusan Perbicaraan <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                      <textarea id="textarea1" runat="server" class="form-control validate[optional] uppercase" rows="3" maxlength="250"></textarea>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Penghukuman <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlpen" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" ></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>

                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                 <asp:Button ID="Button5" runat="server" Visible="false" class="btn btn-danger" Text="Kemaskini" OnClick="btnkmes_Click" />                                                        
                                                        <asp:Button ID="Button2" runat="server" class="btn btn-danger" Text="Simpan" OnClick="btnsmmit_Click" />                                                        
                                 
                            </div>
                           </div>
                               </div>
                            <div class="box-body">&nbsp;</div>
                             <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                    <%--  <div class="row" style="overflow:auto;">--%>
           <div class="col-md-12 box-body">
                                   <%--<asp:GridView Id="example1" runat="server" class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="Small" Width="100%" AllowPaging="true" PageSize="25" OnPageIndexChanging="gvSelected_PageIndexChanging">--%>
               <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="30" ShowFooter="false" GridLines="None" >
                   <PagerStyle CssClass="pager" />
                                        <Columns>
                                         <asp:TemplateField HeaderText="BIL" ItemStyle-HorizontalAlign="Center">  
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="150"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="KATEGORI DEFENDAN">  
                                            <ItemTemplate>  
                                                
                                                 <asp:LinkButton runat="server" ID="lnkView" OnClick="lnkView_Click" Font-Bold Font-Underline>
                                                 <asp:Label ID="Label2" runat="server"  Text='<%# Eval("defendan_desc") %>'></asp:Label>
                                                 </asp:LinkButton>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JENIS TINDAKAN">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label3" runat="server"  Text='<%# Bind("penghukuman_desc") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tarikh Perbicaraan" ItemStyle-HorizontalAlign="Center">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("leg_court_dt","{0:dd/MM/yyyy}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Keputusan Perbicaraan">   
                                                <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("leg_court_result") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Penghukuman">  
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle> 
                                            <ItemTemplate>  
                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("tindakan_desc") %>'></asp:Label>  
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




