<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_KEMESKINI_KRITE.aspx.cs" Inherits="HR_KEMESKINI_KRITE" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1> Penilalian prestasi   </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Sumber manusia </a></li>
                            <li class="active">  Kemaskini Dokumen Penilaian Prestasi  </li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
      
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Dokuman Penililain Prestasi</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Tempoh Penilaian Dari  </label>
                                    <div class="col-sm-8">
                                       <div class="input-group">
                                                                    
                                                        <asp:TextBox ID="TextBox1" runat="server" class="form-control validate[optional] datepicker mydatepickerclass" placeholder="PICK DATE"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Hingga  </label>
                                    <div class="col-sm-8">
                                       <div class="input-group">
                                                                    
                                                        <asp:TextBox ID="TextBox3" runat="server" class="form-control validate[optional] datepicker mydatepickerclass" placeholder="PICK DATE"></asp:TextBox>
                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                    </div>
                                </div>
                            </div>
                                
                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Jawatan </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddkat_jaw" runat="server" class="form-control selectpicker">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Unit </label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="dd_unit" runat="server" class="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                 
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                               <asp:Button ID="btn_Senar" runat="server" class="btn btn-danger" Text="Senarai"  UseSubmitBehavior="false" onclick="btn_Senar_Click"/>
                                                 <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" UseSubmitBehavior="false" onclick="rst_clk"  />
                            </div>
                           </div>
                               </div>
                           <div class="box-body">&nbsp;
                                    </div>


                                   <div class="box-header with-border">
                            <h3 class="box-title">Pilihan Kriteria Penilalian</h3>
                        </div>
                                  <div class="box-body">&nbsp;
                                    </div>

                                   <div class="box-body">&nbsp;
                                    </div>
                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                    <asp:GridView ID="GridView1" class="table table-bordered table-hover dataTable uppercase" CellPadding="5" runat="server"  AllowPaging="true" PageSize="25" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="Black" 
                                             AutoGenerateColumns="false" OnPageIndexChanging="gvSelected_PageIndexChanging_jmk" Height="75" Width="100%">

                                        <Columns>

                                         <asp:TemplateField HeaderText="BIL">
                                                <ItemStyle Width="2%"></ItemStyle> 
                                               <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="2%"/>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="DARI">   
                                                <ItemStyle HorizontalAlign="Left" Width="2%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="ss_sdt" runat="server" Text='<%# Eval("dt1", "{0:dd/MM/yyyy}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="HINGGA">   
                                                <ItemStyle HorizontalAlign="Left" Width="2%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="ss_edt" runat="server" Text='<%# Eval("dt2", "{0:dd/MM/yyyy}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="JAWATAN">   
                                                <ItemStyle HorizontalAlign="Left" Width="7%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="ss_jaw" runat="server" Text='<%# Eval("hr_jaw_desc") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="UNIT">   
                                                <ItemStyle HorizontalAlign="Left" Width="7%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="ss_unit" runat="server" Text='<%# Eval("hr_unit_desc") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BAHAGIAN">   
                                                <ItemStyle HorizontalAlign="Left" Width="7%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="ss_bha" runat="server" Text='<%# Eval("cse_section_desc") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SUBJEK">   
                                                <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="ss_mp" runat="server" Text='<%# Eval("csb_subject_desc") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PEMBERAT (%)">   
                                                <ItemStyle HorizontalAlign="center" Width="5%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:TextBox ID="ss_pemberat" runat="server" CssClass="form-control validate[optional]" Text='<%# Eval("cap_weightage") %>' MaxLength="2"></asp:TextBox>
                                                 <asp:Label ID="Label1" runat="server" Visible="false" Text='<%# Eval("dt1") %>'></asp:Label>  
                                            <asp:Label ID="Label2" runat="server" Visible="false" Text='<%# Eval("dt2") %>'></asp:Label>  
                                            <asp:Label ID="Label3" runat="server" Visible="false" Text='<%# Eval("cap_post_cat_cd") %>'></asp:Label>  
                                            <asp:Label ID="Label4" runat="server" Visible="false" Text='<%# Eval("cap_unit_cd") %>'></asp:Label>  
                                            <asp:Label ID="Label5" runat="server" Visible="false" Text='<%# Eval("cap_section_cd") %>'></asp:Label>  
                                            <asp:Label ID="Label6" runat="server" Visible="false" Text='<%# Eval("cap_subject_cd") %>'></asp:Label>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                               <%-- <asp:TemplateField HeaderText="PILIH">   
                                                <ItemStyle HorizontalAlign="center" Width="5%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:CheckBox ID="s_pilih" runat="server" />
                                            </ItemTemplate>  
                                             
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="HAPUS">   
                                                <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:CheckBox ID="s_hapus" runat="server" />
                                                <asp:Label ID="ss_sqno" runat="server" Visible="false" Text='<%# Eval("cap_seq_no") %>'></asp:Label> 
                                            </ItemTemplate>  
                                             
                                                </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

               </div>
          </div>


                                  <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                               <asp:Button ID="btn_jenis_simp" runat="server" class="btn btn-danger" Text="Kemaskini" onclick="btn_jenis_simp_Click" UseSubmitBehavior="false" />
                                            <asp:Button ID="Button3" style="display:none;" runat="server" class="btn btn-danger" Text="Cetak" onclick="ctk_values" UseSubmitBehavior="false" />
                                            <asp:Button ID="Button2" runat="server" class="btn btn-warning" Text="Hapus" onclick="btn_hapus_Click" OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;" UseSubmitBehavior="false" />
                            </div>
                           </div>
                               </div>
                           <div class="box-body">&nbsp;
                                    </div>


                                 <div class="row">
                                   <div class="col-md-12 col-sm-4" style="text-align:center; line-height:13px;">
                                     <rsweb:ReportViewer ID="RptviwerStudent" runat="server" Width="50%"></rsweb:ReportViewer>
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



