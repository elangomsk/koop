<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../Pelaburan_Anggota/Pengesahan_pemohon.aspx.cs" Inherits="Pengesahan_pemohon" %>

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
                        <h1>  Pengesahan Pemohonan </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>  Pelaburan Anggota </a></li>
                            <li class="active"> Pengesahan Pemohonan  </li>
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
                            <h3 class="box-title">Carian Maklumat Pemohonan </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Nama Kelompok <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="TextBox12" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                          <asp:Panel ID="autocompleteDropDownPanel" runat="server" ScrollBars="Auto" Height="150px"
                                                            Font-Size="Medium" HorizontalAlign="Left" Wrap="False" />
                                                        <cc1:AutoCompleteExtender ServiceMethod="GetCompletionList" MinimumPrefixLength="1"
                                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="TextBox12"
                                                            ID="AutoCompleteExtender1" CompletionListElementID="autocompleteDropDownPanel"
                                                            CompletionListCssClass="form-control uppercase" runat="server" FirstRowSelected="false">
                                                        </cc1:AutoCompleteExtender>
                                    </div>
                                </div>
                            </div>
                                <%-- <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Tarikh Kelulusan <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                       <div class="input-group">
                                                       <asp:TextBox ID="TextBox1" runat="server" class="form-control  datepicker mydatepickerclass" ></asp:TextBox>
                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                       </div>
                                        
                                    </div>
                                </div>
                            </div>--%>
                                 </div>
                                </div>
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                 <asp:Button ID="Button2" runat="server" class="btn btn-primary" Text="Carian" OnClick="BindGridview"/>
                                                                <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" usesubmitbehavior="false" OnClick="Reset_btn"/>
                                 
                            </div>
                           </div>
                               </div>
                           <div class="box-body">&nbsp;
                                    </div>


                            <div id="show_cnt1" runat="server">
                             <div class="box-header with-border">
                            <h3 class="box-title">Senarai Pemohonan Untuk Pengesahan Keahlian</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                              <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">                                   
         <div class="col-md-12 box-body">
                                     <asp:GridView ID="gvSelected" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="30" ShowFooter="false" GridLines="None"  OnPageIndexChanging="gvSelected_PageIndexChanging">
                                         <PagerStyle CssClass="pager" />
                                        <Columns>
                                        <asp:TemplateField HeaderText="BIL"  ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="NO PEMOHANAN">
                                            <ItemTemplate>  
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("app_applcn_no") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NO KP"> 
                                                <ItemStyle HorizontalAlign="Center" />    
                                            <ItemTemplate>  
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("app_new_icno") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="NAMA PEMOHANAN">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("app_name") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amaun Diluluskan (RM)">   
                                                    <ItemStyle HorizontalAlign="Right" />  
                                            <ItemTemplate>  
                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("jkk_approve_amt","{0:n}") %>'></asp:Label> 
                                                <asp:Label ID="lbl_kut" Visible="false" runat="server" Text='<%# Bind("cal_profit_amt") %>'></asp:Label> 
                                                <asp:Label ID="lbl_gua" Visible="false" runat="server" Text='<%# Bind("cal_no_guarantor") %>'></asp:Label> 
                                                <asp:Label ID="lbl_age" Visible="false" runat="server" Text='<%# Bind("app_age") %>'></asp:Label> 
                                                <asp:Label ID="lbl_ltype" Visible="false" runat="server" Text='<%# Bind("app_loan_type_cd") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Tempoh Kelulusan (Bulan)">   
                                                  <ItemStyle HorizontalAlign="Center" />  
                                            <ItemTemplate>  
                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("jkk_approve_dur") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Keputusan Permohonan">  
                                                <ItemStyle HorizontalAlign="Center" Font-Size="12px" /> 
                                            <ItemTemplate>  
                                                <asp:RadioButton ID="chkSelect_1" Checked="true" Text="&nbsp;Lulus" runat="server" GroupName="status" />
                                                &nbsp;&nbsp;
                                                <asp:RadioButton ID="chkSelect_2" Text="&nbsp;Tidak Lulus" runat="server" GroupName="status" />
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
                             <hr />
                       <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                  <asp:Button ID="Button3" runat="server" class="btn btn-danger" Visible="false" Text="Simpan" OnClick="submit_button"/>
                                            <asp:Button ID="Button4" runat="server" class="btn btn-default" Visible="false" Text="Batal" />
                                            <asp:Button ID="Button5" runat="server" class="btn btn-danger"  Text="Cetak" Visible="false" />
                                   <%--<asp:Button ID="Button6" runat="server" class="btn btn-danger"  Text="HANTAR JUNAL AKAUN" OnClick="submit_button"/>   --%>
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


