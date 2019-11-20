<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../Pelaburan_Anggota/PP_SMPembiayaan.aspx.cs" Inherits="PP_SMPembiayaan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
          <script type="text/javascript">
 function chk_pro_amt() {            
    
     var amt2 = Number($("#<%=TextBox31.ClientID%>")[0].value);
     var amt3 = Number($("#<%=TextBox33.ClientID%>")[0].value);
     var amt4 = Number($("#<%=TextBox40.ClientID%>")[0].value);
     var amt5 = Number($("#<%=TextBox41.ClientID%>")[0].value);
          
         
     if (amt2 != "")
            {                
                $("#<%=TextBox31.ClientID%>").val(amt2.toFixed(2));
     }
     if (amt3 != "")
            {                
                $("#<%=TextBox33.ClientID%>").val(amt3.toFixed(2));
     }
     if (amt4 != "")
            {                
                $("#<%=TextBox40.ClientID%>").val(amt4.toFixed(2));
     }
     if (amt5 != "")
            {                
                $("#<%=TextBox41.ClientID%>").val(amt5.toFixed(2));
            }
            
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" style="height: 895px;">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1> Semakan Maklumat Pembiayaan</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>  Pelaburan Anggota </a></li>
                            <li class="active">  Semakan Maklumat Pembiayaan</li>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Kata Kunci Carian <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="Applcn_no" runat="server"  class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase" maxlength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                   <div class="col-sm-8">
                                        <asp:Button ID="Button9" runat="server" class="btn btn-primary" Text="Carian" UseSubmitBehavior="false" OnClick="btnsrch_Click"/>
                                        <asp:Button ID="Button10" runat="server" class="btn btn-default"  Text="Set Semula" usesubmitbehavior="false" OnClick="btn_rstclick"/>
                                       <asp:Button ID="Button2" runat="server" class="btn btn-default"  Text="Kembali" usesubmitbehavior="false" OnClick="clk_bak"/>
                                       </div>
                                </div>
                            </div>
                                  </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtname" runat="server" class="form-control validate[optional,custom[textSp]] uppercase" MaxLength="150"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Wilayah / Pejabat </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="txtwil" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                           
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jumlah Belian (RM)</label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="txtjumla" runat="server" class="form-control validate[optional,custom[number]] uppercase" MaxLength="10"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Cawangan / Jabatan</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtcaw" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                
                                  </div>
                         </div>

                              <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jumlah Kumulatif Kena (RM) </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtamaun" runat="server" class="form-control validate[optional,custom[number]]"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tempoh (Bulan)</label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="txttempoh" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>

                            <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jumlah Kumulatif Tunggakan (RM) </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="TextBox2" runat="server" class="form-control validate[optional]"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jumlah Kumulatif Bayar (RM) </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="txttemp" runat="server" class="form-control"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>

                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jumlah Kumulatif Simpanan (RM) </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="TextBox4" runat="server" class="form-control validate[optional]"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jumlah Untung (RM) </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="TextBox3" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jenis Pemulihan </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="TextBox6" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Baki Kumulatif Pelaburan (RM) </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="TextBox5" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tarikh Akhir Bayar Balik </label>
                                    <div class="col-sm-8">
                                          <div class="input-group">
                                         <asp:TextBox ID="TextBox7" runat="server" class="form-control"></asp:TextBox>
                                               <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                             </div>

                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Bulan Tunggakan (RM) </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="TextBox1" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>
                              <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                  <asp:Button ID="Button3" runat="server" class="btn btn-warning" Text="Semak JBB" UseSubmitBehavior="false" OnClick="click_jbb"/>
                                                        <asp:Button ID="Button4" runat="server" class="btn btn-warning" Text="Litigasi Bercagar" OnClick="clk_Bercagar"/>                                                        
                                                        <asp:Button ID="Button5" runat="server" class="btn btn-warning" Text="Litigasi Penjamin" onclick="clk_Penjamin"/>
                            </div>
                           </div>
                               </div>
                                   <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Untuk Dihubungi</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                                                    <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Alamat Tetap <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                         <textarea id="Textarea1" runat="server" rows="3" class="form-control validate[optional,custom[onlyaddress]] uppercase" maxlength="150"></textarea>

                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Alamat Surat-Menyurat <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                         <textarea id="Textarea2" runat="server" rows="3" class="form-control validate[optional,custom[onlyaddress]] uppercase" maxlength="150"></textarea>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Poskod <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="txttn16d" runat="server" class="form-control validate[optional,custom[phone]]" MaxLength="5"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Poskod <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="txttp" runat="server" class="form-control validate[optional,custom[phone]]" MaxLength="5"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Negeri <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                            <asp:DropDownList style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" ID="DropDownList2">
                                                        </asp:DropDownList>

                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Negeri <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" ID="DropDownList1">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Telefon(R) </label>
                                    <div class="col-sm-8">
                                           <asp:TextBox ID="TextBox10" runat="server" class="form-control validate[optional,custom[phone]" MaxLength="12"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> No Telefon(B) </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox8" runat="server" class="form-control validate[optional,custom[phone]" MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>
                              <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Telefon(P) </label>
                                    <div class="col-sm-8">
                                           <asp:TextBox ID="TextBox11" runat="server" class="form-control validate[optional,custom[phone]" MaxLength="10"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>
                             <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Waris Terdekat (Ahli Keluarga) (Penama 1)</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                           <asp:TextBox ID="TextBox12" runat="server" class="form-control validate[optional,custom[textSp]] uppercase" MaxLength="150"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> No KP Baru </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox13" runat="server" class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase" MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Telefon <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                           <asp:TextBox ID="TextBox14" runat="server" class="form-control validate[optional,custom[phone]" MaxLength="12"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Hubungan </label>
                                    <div class="col-sm-8">
                                      <asp:DropDownList style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" ID="DropDownList4">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>
                              <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Waris Terdekat (Ahli Keluarga) (Penama 2)</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                           <asp:TextBox ID="TextBox15" runat="server" class="form-control validate[optional,custom[textSp]] uppercase" MaxLength="150"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> No KP Baru </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox16" runat="server" class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase" MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Telefon <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                           <asp:TextBox ID="TextBox17" runat="server" class="form-control validate[optional,custom[phone]" MaxLength="12"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Hubungan </label>
                                    <div class="col-sm-8">
                                      <asp:DropDownList style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" ID="DropDownList3">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>
                                     <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Penjamin</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                                    <div id="Div3" class="nav-tabs-custom col-md-12 box-body" role="tabpanel" runat="server">
                                  <ul class="s1 nav nav-tabs" role="tablist">
                                                                <li id="Li3" runat="server" class="active"><a href="#ContentPlaceHolder1_p5" aria-controls="p5" role="tab" data-toggle="tab"><strong>Penjamin 1</strong></a></li>
                                                                <li id="Li4" runat="server"><a href="#ContentPlaceHolder1_p6" aria-controls="p6" role="tab" data-toggle="tab"><strong>Penjamin 2</strong></a></li>
                                                            
                                                                                                                               
                                                            </ul>
                                  <div class="box-body">&nbsp;</div>
                                     <div class="tab-content">
                                                             <div role="tabpanel" class="tab-pane active" runat="server" id="p5">
                                                                 <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox27" runat="server" MaxLength="150"  class="form-control validate[optional,custom[textSp]] uppercase"></asp:TextBox> 

                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Telefon</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox35" runat="server"  class="form-control validate[optional,custom[phone]]" MaxLength="12"></asp:TextBox> 

                                    </div>
                                </div>
                            </div>
                               
                                 </div>
                                 </div>

                              <div class="row">
                             <div class="col-md-12">
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Alamat Tetap</label>
                                    <div class="col-sm-8">
                                       <textarea id="TextArea7" runat="server"  rows="3" class="form-control validate[optional,custom[onlyaddress]] uppercase" maxlength="250"></textarea>
                                    </div>
                                </div>
                            </div>
                                <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Alamat Surat-Menyurat </label>
                                    <div class="col-sm-8">
                                      <textarea id="TextArea8" runat="server" rows="3" class="form-control validate[optional,custom[onlyaddress]] uppercase" maxlength="250"></textarea>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                                                                  <div class="row">
                             <div class="col-md-12">
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Poskod</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox62" runat="server"  class="form-control validate[optional,custom[phone]]" MaxLength="5"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>
                                <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Poskod </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox63" runat="server"  class="form-control validate[optional,custom[phone]]" MaxLength="5"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                                                                  <div class="row">
                             <div class="col-md-12">
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Negeri</label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" ID="DD_NegriBind5"></asp:DropDownList>      
                                    </div>
                                </div>
                            </div>
                                <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Negeri </label>
                                    <div class="col-sm-8">
                                     <asp:DropDownList style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" ID="DD_NegriBind6"></asp:DropDownList>                                                                 
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                                                                  <div class="row">
                             <div class="col-md-12">
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jenis Pengenalan <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="DD_Pengenalan2" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>     
                                    </div>
                                </div>
                            </div>
                                <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Pengenalan <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="TextBox28" runat="server"  class="form-control validate[required] uppercase" MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                                                                  <div class="row">
                             <div class="col-md-12">
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jawatan</label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="TextBox29" runat="server"  class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase" MaxLength="40"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Sektor Pekerjaan</label>
                                    <div class="col-sm-8">
                                     <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                               <ContentTemplate>
                                                           
                                                            <asp:RadioButton ID="RB1" runat="server"  Text=" Agensi / Jabatan / Kementerian Kerajaan Malaysia"  AutoPostBack="true" 
                                                                    oncheckedchanged="RB1_CheckedChanged"/>
                                                          <%--  <label>Warganegara</label>--%>
                                                            <br />
                                                            <asp:RadioButton ID="RB2" runat="server" Text=" Syarikat Berkaitan Kerajaan (GLC)"  AutoPostBack="true" 
                                                                    oncheckedchanged="RB2_CheckedChanged" />
                                                            <br />
                                                            <asp:RadioButton ID="RB3" runat="server" Text=" Syarikat Pelaburan Berkaitan Kerajaan (GLIC)"  AutoPostBack="true" 
                                                                    oncheckedchanged="RB3_CheckedChanged" />
                                                                    <br />
                                                            <asp:RadioButton ID="RB4" runat="server" Text="Syarikat Berstatus Berhad"  AutoPostBack="true" 
                                                                    oncheckedchanged="RB4_CheckedChanged" />
                                                                     <br />
                                                            <%--<asp:RadioButton ID="RadioButton10" runat="server" Text=" Anak Syarikat"  AutoPostBack="true" 
                                                                    oncheckedchanged="RadioButton2_CheckedChanged" />--%>
                                                            <asp:CheckBox ID="cb1" runat="server" Text="Anak Syarikat" />
                                                            
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Taraf Jawatan</label>
                                    <div class="col-sm-8">
                                     <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                               <ContentTemplate>
                                                            <div class="col-md-3 col-sm-3">
                                                            <asp:RadioButton ID="RB11" runat="server"  Text=" Tetap"  AutoPostBack="true" 
                                                                    oncheckedchanged="RB11_CheckedChanged"/>
                                                          <%--  <label>Warganegara</label>--%>
                                                                                                                            
                                                            </div>
                                                            <div class="col-md-3 col-sm-3">
                                                            <asp:RadioButton ID="RB12" runat="server" Text=" Kontrak"  AutoPostBack="true" 
                                                                    oncheckedchanged="RB12_CheckedChanged" />                                                                    
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Gaji Bersih (RM) </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox31" runat="server" onchange="chk_pro_amt()" Style=" text-align:right;"  class="form-control validate[optional,custom[number]]" MaxLength="12"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Gaji Kasar (RM)</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox33" runat="server" onchange="chk_pro_amt()" Style=" text-align:right;"  class="form-control validate[optional,custom[number]]" MaxLength="12"></asp:TextBox> 
                                                                
                                    </div>
                                </div>
                            </div>
                                
                                 </div>
                                 </div>
                                                                  <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama Majikan</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox34" runat="server"  class="form-control validate[optional,custom[textSp]] uppercase" MaxLength="150"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Telefon Majikan</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox30" runat="server"  class="form-control validate[optional,custom[phone]] uppercase" MaxLength="12"></asp:TextBox> 
                                                                
                                    </div>
                                </div>
                            </div>
                                
                                 </div>
                                 </div>
                                                                  <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Samb</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox32" runat="server"  class="form-control validate[optional,custom[number]]" MaxLength="6"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>
                               
                                 </div>
                                 </div>

                                                                 </div>
                                         
                            <div role="tabpanel" class="tab-pane" runat="server" id="p6">
                                    <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox36" runat="server" MaxLength="150"  class="form-control validate[optional,custom[textSp]] uppercase"></asp:TextBox> 

                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Telefon</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox37" runat="server"  class="form-control validate[optional,custom[phone]]" MaxLength="12"></asp:TextBox> 

                                    </div>
                                </div>
                            </div>
                               
                                 </div>
                                 </div>

                              <div class="row">
                             <div class="col-md-12">
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Alamat Tetap</label>
                                    <div class="col-sm-8">
                                       <textarea id="TextArea5" runat="server"  rows="3" class="form-control validate[optional,custom[onlyaddress]] uppercase" maxlength="250"></textarea>
                                    </div>
                                </div>
                            </div>
                                <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Alamat Surat-Menyurat </label>
                                    <div class="col-sm-8">
                                      <textarea id="TextArea6" runat="server" rows="3" class="form-control validate[optional,custom[onlyaddress]] uppercase" maxlength="250"></textarea>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                                                                  <div class="row">
                             <div class="col-md-12">
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Poskod</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox60" runat="server"  class="form-control validate[optional,custom[phone]]" MaxLength="5"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>
                                <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Poskod </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox61" runat="server"  class="form-control validate[optional,custom[phone]]" MaxLength="5"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                                                                  <div class="row">
                             <div class="col-md-12">
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Negeri</label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" ID="DD_NegriBind7"></asp:DropDownList>      
                                    </div>
                                </div>
                            </div>
                                <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Negeri </label>
                                    <div class="col-sm-8">
                                     <asp:DropDownList style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" ID="DD_NegriBind8"></asp:DropDownList>                                                                 
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                                                                  <div class="row">
                             <div class="col-md-12">
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jenis Pengenalan <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="DD_Pengenalan3" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>     
                                    </div>
                                </div>
                            </div>
                                <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Pengenalan <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="TextBox38" runat="server"  class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase" MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                                                                  <div class="row">
                             <div class="col-md-12">
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jawatan</label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="TextBox39" runat="server"  class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase" MaxLength="40"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Sektor Pekerjaan</label>
                                    <div class="col-sm-8">
                                     <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                               <ContentTemplate>
                                                            
                                                            <asp:RadioButton ID="RB21" runat="server"  Text=" Agensi / Jabatan / Kementerian Kerajaan Malaysia"  AutoPostBack="true" 
                                                                    oncheckedchanged="RB1_1_CheckedChanged"/>
                                                          <%--  <label>Warganegara</label>--%>
                                                            <br />
                                                            <asp:RadioButton ID="RB22" runat="server" Text=" Syarikat Berkaitan Kerajaan (GLC)"  AutoPostBack="true" 
                                                                    oncheckedchanged="RB1_2_CheckedChanged" />
                                                            <br />
                                                            <asp:RadioButton ID="RB23" runat="server" Text=" Syarikat Pelaburan Berkaitan Kerajaan (GLIC)"  AutoPostBack="true" 
                                                                    oncheckedchanged="RB1_3_CheckedChanged" />
                                                                    <br />
                                                            <asp:RadioButton ID="RB24" runat="server" Text=" Syarikat Berstatus Berhad"  AutoPostBack="true" 
                                                                    oncheckedchanged="RB1_4_CheckedChanged" />
                                                                     <br />
                                                            <%--<asp:RadioButton ID="RadioButton17" runat="server" Text=" Anak Syarikat"  AutoPostBack="true" 
                                                                    oncheckedchanged="RadioButton2_CheckedChanged" />--%>
                                                                    <asp:CheckBox ID="CheckBox1" runat="server" Text="Anak Syarikat" />
                                                            
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Taraf Jawatan</label>
                                    <div class="col-sm-8">
                                     <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                               <ContentTemplate>
                                                            <div class="col-md-3 col-sm-3">
                                                            <asp:RadioButton ID="TJ21" runat="server"  Text=" Tetap"  AutoPostBack="true" 
                                                                    oncheckedchanged="RB111_CheckedChanged"/>
                                                          <%--  <label>Warganegara</label>--%>
                                                                                                                            
                                                            </div>
                                                            <div class="col-md-3 col-sm-3">
                                                            <asp:RadioButton ID="TJ22" runat="server" Text=" Kontrak"  AutoPostBack="true" 
                                                                    oncheckedchanged="RB112_CheckedChanged" />                                                                    
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Gaji Bersih (RM) </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox40" runat="server" onchange="chk_pro_amt()" Style=" text-align:right;" class="form-control validate[optional,custom[number]]" MaxLength="12"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Gaji Kasar (RM)</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox41" runat="server" onchange="chk_pro_amt()" Style=" text-align:right;"  class="form-control validate[optional,custom[number]]" MaxLength="12"></asp:TextBox> 
                                                                
                                    </div>
                                </div>
                            </div>
                                
                                 </div>
                                 </div>
                                                                  <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama Majikan</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox42" runat="server"  class="form-control validate[optional,custom[textSp]] uppercase" MaxLength="150"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Telefon Majikan</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox43" runat="server"  class="form-control validate[optional,custom[phone]] uppercase" MaxLength="12"></asp:TextBox> 
                                                                
                                    </div>
                                </div>
                            </div>
                                
                                 </div>
                                 </div>
                                                                  <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Samb</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox44" runat="server"  class="form-control validate[optional,custom[number]]" MaxLength="6"></asp:TextBox> 
                                    </div>
                                </div>
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
                                     <asp:Button ID="Button6" runat="server" class="btn btn-danger" Text="Kemaskini" UseSubmitBehavior="false" OnClick="clk_update" />                                                        
                                                        <asp:Button ID="Button7" runat="server" class="btn btn-warning" Text="Log Tugasan" OnClick="clk_log_tugasan" UseSubmitBehavior="false" />                                                        
                                                        <asp:Button ID="Button1" runat="server" class="btn btn-warning" Text="Cetak" OnClick="clk_cetak" UseSubmitBehavior="false" />                                                        
                                                        <asp:Button ID="Button8" runat="server" class="btn btn-default" Text="Kembali" usesubmitbehavior="false" OnClick="clk_bak"/>
                            </div>
                           </div>
                               </div>
                                     <div class="row">
                                                        <div class="col-md-12 col-sm-2" style="text-align: center">
                                                           <rsweb:ReportViewer ID="Rptviwer_kelulusan" runat="server"></rsweb:ReportViewer>
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
    </asp:UpdatePanel>
        
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
   
</asp:Content>



