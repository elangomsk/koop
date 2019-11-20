<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../Pelaburan_Anggota/PP_kiraan.aspx.cs" Inherits="PP_kiraan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
                <script type="text/javascript">
 function chk_pro_amt() {            
     var amt1 = Number($("#<%=Txtamaun.ClientID%>")[0].value);
   
          
            if (amt1 != "")
            {                
                $("#<%=Txtamaun.ClientID%>").val(amt1.toFixed(2));
            }
     
            
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
         <asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0;
                right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                <span style="border-width: 0px; position: fixed; font-weight: bold; padding: 50px;
                    background-color: #FFFFFF; font-size: 16px; left: 40%; top: 40%;">Sila Tunggu. Rekod
                    Sedang Diproses ...</span>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server" ScriptMode="Release">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>  Daftar Perkiraan Kelulusan</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>  Pelaburan Anggota </a></li>
                            <li class="active">  Daftar Perkiraan Kelulusan</li>
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
                            <h3 class="box-title">Maklumat Pemohon</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No KP Baru <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="Txtnokp" runat="server" class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase"
                                                            MaxLength="12"></asp:TextBox>
                                          <asp:Panel ID="autocompleteDropDownPanel" runat="server" ScrollBars="Auto" Height="150px"
                                                            Font-Size="Medium" HorizontalAlign="Left" Wrap="False" />
                                                        <cc1:AutoCompleteExtender ServiceMethod="GetCompletionList" MinimumPrefixLength="1"
                                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="Txtnokp"
                                                            ID="AutoCompleteExtender1" CompletionListElementID="autocompleteDropDownPanel"
                                                            CompletionListCssClass="form-control uppercase" runat="server" FirstRowSelected="false">
                                                        </cc1:AutoCompleteExtender>
                                        <asp:TextBox ID="txt_app_no" runat="server" class="form-control" Visible="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                   <div class="col-sm-8">                                        
                                     <asp:Button ID="Button4" runat="server" class="btn btn-primary" Text="Carian" usesubmitbehavior="false" OnClick="btnsrch_Click" />
                                                    <%--<asp:Button ID="Button6" runat="server" class="btn btn-default" Text="Set Semula" OnClick="Button1_Click" />--%>
                                       <asp:Button ID="Button8" runat="server" class="btn btn-default" Text="Kembali"  UseSubmitBehavior="false" OnClick="clk_bak" />
                                       </div>
                                    
                                </div>
                            </div>
                                  </div>
                                </div>
                               <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Permohonan <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="dd_permohon" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" AutoPostBack="true" OnSelectedIndexChanged="bind_permohon" ></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="Txtnama" runat="server" class="form-control validate[optional,custom[textSp]]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Umur</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="txtage" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <%-- <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Wilayah / Pejabat</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="Txtwil" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Cawangan / Jabatan</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="Txtcaw" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>--%>

                              <div class="box-header with-border">
                            <h3 class="box-title">Parameter Perkiraan</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                             <div class="row" id="pre_row1" runat="server" visible="false">
                             <div class="col-md-12">
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Amaun Pre Diluluskan (RM) <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txt_pre_bal" style="text-align:right;" onchange="chk_pro_amt()" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>                                  
                                 </div>
                                 </div>
                             <div class="row">
                             <div class="col-md-12">
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Amaun Diluluskan (RM) <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="Txtamaun" style="text-align:right;" onchange="chk_pro_amt()" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tempoh (Bulan) <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TxtTem" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">                                                                        
                                    <label for="inputEmail3" class="col-sm-3 control-label">Kadar Keuntungan (%) <span style="color: Red">*</span></label>
                                  <div class="col-sm-8">
                                      <asp:TextBox ID="Txtkadar" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">                                                                        
                                    <label for="inputEmail3" class="col-sm-3 control-label">Bilangan Penjamin</label>
                                  <div class="col-sm-8 text-left">
                                         <div class="col-sm-9 text-left">
                                      <asp:TextBox ID="TxtBilangan" runat="server" MaxLength="1" class="form-control validate[optional,custom[number]]"></asp:TextBox>
                                             </div>
                                       <div class="col-sm-3">
                                       <asp:Button ID="kira" runat="server" class="btn btn-warning" Text="Kira" 
                                                            onclick="kira_Click"/>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                             <div class="box-header with-border">
                            <h3 class="box-title">Janaan FI, CAJ Dan Bayaran Bulanan</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Keuntungan (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="Txtkeuntungan" runat="server"  Style=" text-align:right;"   class="form-control validate[optional,custom[number]] uppercase" MaxLength="12" ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Bayaran Bulanan (RM)</label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="TxtBay" runat="server"   Style=" text-align:right;"  class="form-control validate[optional,custom[number]] uppercase" MaxLength="12" ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                         </div>
                                   </div>
                               <%--<div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Caj Duti Setem (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TxtCaj" runat="server" style="text-align:right;" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                
                                  </div>
                         </div>
                                   </div>--%>
                             <%--  <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Fi Pemprosesan (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TxtFipem" runat="server" style="text-align:right;" class="form-control validate[optional,custom[number]]" MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Fi Semakan Kredit (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="Txtfisem" runat="server" style="text-align:right;" class="form-control validate[optional,custom[number]]" MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>--%>
                           <%--   <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Deposit Sekuriti (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TxtDeposit" runat="server" style="text-align:right;" class="form-control validate[optional,custom[number]]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Keuntungan (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="Txtkeuntungan" runat="server" style="text-align:right;"  class="form-control validate[optional,custom[number]] uppercase" MaxLength="12" ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>--%>
                          
                              <%-- <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Lain-lain Caj (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="Txtlain" runat="server" style="text-align:right;"  class="form-control validate[optional,custom[number]] uppercase" MaxLength="12" ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Bayaran Bulanan (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TxtBay" runat="server"  style="text-align:right;" class="form-control validate[optional,custom[number]] uppercase" MaxLength="12" ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>--%>
                               <div class="box-body">&nbsp;</div>
                              <div class="row">
                                     <div class="col-md-12">
                                  <div class="col-md-6 box-body" id="grd1" runat="server">
          
                                            <asp:GridView ID="grvStudentDetails" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="20" onrowdatabound="gvEmp_RowDataBound" OnRowDeleting="grvStudentDetails_RowDeleting" ShowFooter="True">
                                                        <Columns>
                                                             <asp:TemplateField Visible="false" HeaderText="BIL">
                                                                <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:label ID="RowNumber"  runat="server" Text='<%# Eval("RowNumber") %>'></asp:label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Jenis Caj">
                                                                 <ItemStyle Width="50%"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="Col1" style="width:100%; font-size:12px;" runat="server" CssClass="form-control select2 validate[optional]" onselectedindexchanged="ddgstdeboth_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                 <FooterStyle HorizontalAlign="Left" />
                                                                <FooterTemplate>
                                                                    <asp:Button ID="ButtonAdd" runat="server" Text="Tambah Baru" OnClick="ButtonAdd_Click" />
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Amaun (RM)">
                                                                  <ItemStyle Width="20%"></ItemStyle>
                                                                <ItemTemplate>
                                                                   <asp:TextBox ID="Col2" CssClass="form-control uppercase "  style="text-align:right;"  runat="server" Text='<%# Eval("col2","{0:n}") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                         
                                                            <asp:CommandField ItemStyle-Width="5%" ShowDeleteButton="True" />
                                                        </Columns>
                                                       <%-- <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="White" />--%>
                                                    </asp:GridView> 

                                                 </div> 
                                          <div class="col-md-6 box-body text-left">

                                                        <asp:Button ID="Button2" runat="server" Visible="false" class="btn btn-danger" Text="Simpan" OnClick="btnsmmit_Click" />
                                                        <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" usesubmitbehavior="false" OnClick="Button3_Click" />
                                                       <%-- <asp:Button ID="Button3" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" onclick="clk_bak" />--%>
                               
                                 
                            </div>
                                         </div>
                                  </div>
                        <%--    <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">

                                                        <asp:Button ID="Button2" runat="server" Visible="false" class="btn btn-danger" Text="Simpan" OnClick="btnsmmit_Click" />
                                                        <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" usesubmitbehavior="false" OnClick="Button3_Click" />
                                                        <asp:Button ID="Button3" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" onclick="clk_bak" />
                               
                                 
                            </div>
                           </div>
                               </div>--%>
                           <div class="box-body">&nbsp;</div>
                              
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



