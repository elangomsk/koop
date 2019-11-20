<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../Pelaburan_Anggota/PP_Ringkasan_kelulusan.aspx.cs" Inherits="PP_Ringkasan_kelulusan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
            <script type="text/javascript">
 function chk_pro_amt() {            
     var amt1 = Number($("#<%=KJ_amaun.ClientID%>")[0].value);
   
          
            if (amt1 != "")
            {                
                $("#<%=KJ_amaun.ClientID%>").val(amt1.toFixed(2));
            }
     
            
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      
    <asp:ScriptManager ID="ScriptManagerCalendar" ScriptMode="Release" runat="server">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" style="height: 895px;">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>  Ringkasan Kelulusan</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>  Pelaburan Anggota </a></li>
                            <li class="active"> Ringkasan Kelulusan</li>
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
                                     <asp:TextBox ID="Icno" runat="server" class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase"
                                                            MaxLength="12"></asp:TextBox>
                                          <asp:Panel ID="autocompleteDropDownPanel" runat="server" ScrollBars="Auto" Height="150px"
                                                            Font-Size="Medium" HorizontalAlign="Left" Wrap="False" />
                                                        <cc1:AutoCompleteExtender ServiceMethod="GetCompletionList" MinimumPrefixLength="1"
                                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="Icno"
                                                            ID="AutoCompleteExtender1" CompletionListElementID="autocompleteDropDownPanel"
                                                            CompletionListCssClass="form-control uppercase" runat="server" FirstRowSelected="false">
                                                        </cc1:AutoCompleteExtender>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                   <div class="col-sm-8">
                                     <asp:Button ID="Button4" runat="server" class="btn btn-primary" Text="Carian" usesubmitbehavior="false" OnClick="btnsrch_Click" />
                                                    <asp:Button ID="Button6" runat="server" class="btn btn-default" Text="Set Semula" OnClick="btnreset_Click" />
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
                                        <asp:TextBox ID="MP_nama" runat="server" class="form-control validate[optional,custom[textSp]]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tujuan</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="MP_tujuan" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            

                              <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Amaun Dipohon (RM)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="MP_amaun" runat="server" style="text-align:right;" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tempoh (Bulan)</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="MP_tempoh" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>

                            
                        <!-- /.box-header -->
                        <!-- form start -->
                       
                            <div class="row">
                                 <div class="col-md-12">
                             <div class="box-header with-border">
                            <h3 class="box-title">Kelulusan Jawatankuasa</h3>
                        </div>
                                     </div>
                                </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                               <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Bil Mesyuarat <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="jkkbil" runat="server" class="form-control validate[optional,custom[rkel]] uppercase" MaxLength="20"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Amaun Diluluskan (RM) <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="KJ_amaun" onchange="chk_pro_amt()" style="text-align:right;" runat="server" class="form-control validate[optional,custom[number]]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>
                               <div class="row">
                             <div class="col-md-12">
                                 
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Mesyuarat <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                    <asp:TextBox ID="KJ_tarikh" runat="server" class="form-control validate[optional] datepicker mydatepickerclass"
                                                                    placeholder="PICK DATE"></asp:TextBox>
                                          <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tempoh (Bulan) <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="KJ_tempoh" runat="server" class="form-control validate[optional,custom[number]]"
                                                                    MaxLength="3"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                <%--  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Keputusan <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                          <div class="col-md-4 col-sm-4">
                                                                        <asp:RadioButton ID="RadioButton1" runat="server" Text=" Lulus" AutoPostBack="true"
                                                                            OnCheckedChanged="RadioButton1_CheckedChanged" />
                                                                       
                                                                    </div>
                                                                    <div class="col-md-4 col-sm-5">
                                                                        <asp:RadioButton ID="RadioButton2" runat="server" Text=" Lulus Bersyarat" AutoPostBack="true"
                                                                            OnCheckedChanged="RadioButton2_CheckedChanged" />
                                                                       
                                                                    </div>
                                                                    <div class="col-md-4 col-sm-6">
                                                                        <asp:RadioButton ID="RadioButton3" runat="server" Text=" Tolak" AutoPostBack="true"
                                                                            OnCheckedChanged="RadioButton3_CheckedChanged" />
                                                                       
                                                                    </div>
                                    </div>
                                </div>
                            </div>--%>
                                  </div>
                         </div>
                           
                              <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Catatan (Jika lulus bersyarat)</label>
                                    <div class="col-sm-8">
                                        <textarea id="KJ_catatan" runat="server" rows="3" class="form-control validate[optional,custom[onlyaddress]] uppercase" maxlength="250"></textarea>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Ulasan Keputusan <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                       <textarea id="KJ_ulasan" runat="server" rows="3" class="form-control validate[optional,custom[onlyaddress]] uppercase" maxlength="250"></textarea>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>
                           
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                               <asp:Button ID="Button7" runat="server" class="btn btn-warning" UseSubmitBehavior="false" Text="Cetak Templat" OnClick="click_template" />                                                                    
                                                                <asp:Button ID="Button2" runat="server" class="btn btn-danger" Text="Simpan" OnClick="btnsmmit_Click" />
                                                                <asp:Button ID="Button5" runat="server" class="btn btn-warning" UseSubmitBehavior="false" Text="Cetak" OnClick="click_print" />
                                                                <asp:Button ID="Button1" runat="server" class="btn btn-default" UseSubmitBehavior="false" Text="Set Semula" OnClick="btnrst_click" />                                                                
                                                                <asp:Button ID="Button3" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" OnClick="clk_bak" />
                                 
                            </div>
                           </div>
                               </div>
                             <div class="row">
                                                        <div class="col-md-12 col-sm-2" style="text-align: center">
                                                            <rsweb:ReportViewer ID="Rptviwer_kelulusan" runat="server">
                                                            </rsweb:ReportViewer>
                                                            <asp:Label runat="server" ID="ReportErrorMessage" Visible="false" CssClass="report-error-message"></asp:Label>
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



