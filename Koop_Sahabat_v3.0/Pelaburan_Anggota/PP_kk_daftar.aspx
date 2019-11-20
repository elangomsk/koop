<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../Pelaburan_Anggota/PP_kk_daftar.aspx.cs" Inherits="PP_kk_daftar" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
                <script type="text/javascript">
 function chk_pro_amt() {            
     var amt1 = Number($("#<%=TextBox4.ClientID%>")[0].value);
     var amt2 = Number($("#<%=TextBox40.ClientID%>")[0].value);
     var amt3 = Number($("#<%=TextBox41.ClientID%>")[0].value);
     var amt4 = Number($("#<%=TextBox31.ClientID%>")[0].value);
     var amt5 = Number($("#<%=TextBox33.ClientID%>")[0].value);
          
            if (amt1 != "")
            {                
                $("#<%=TextBox4.ClientID%>").val(amt1.toFixed(2));
            }
     if (amt2 != "")
            {                
                $("#<%=TextBox40.ClientID%>").val(amt2.toFixed(2));
     }
     if (amt3 != "")
            {                
                $("#<%=TextBox41.ClientID%>").val(amt3.toFixed(2));
     }
     if (amt4 != "")
            {                
                $("#<%=TextBox31.ClientID%>").val(amt4.toFixed(2));
     }
     if (amt5 != "")
            {                
                $("#<%=TextBox33.ClientID%>").val(amt5.toFixed(2));
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
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" style="height: 895px;">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>  Kemaskini Pendaftaran Permohonan Pembiayaan</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>  Pelaburan Anggota </a></li>
                            <li class="active">  Kemaskini Pendaftaran Permohonan Pembiayaan</li>
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
                                      <asp:TextBox ID="TXTNOKP" runat="server"  class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase" maxlength="12"></asp:TextBox>
                                                        <br />
                                                        <asp:Label ID="Label1" runat="server" Text="(Makluman : Sila Masukkan Huruf dan Nombor Sahaja)" ForeColor="Red" Font-Size="Small"></asp:Label>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                   <div class="col-sm-8">
                                        <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Carian" UseSubmitBehavior="false" OnClick="Searchbtn_Click"/>
                                        <asp:Button ID="Button2" runat="server" class="btn btn-default"  Text="Set Semula" usesubmitbehavior="false" OnClick="Reset_btn"/>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox2" runat="server" class="form-control validate[optional,custom[textSp]] uppercase" MaxLength="150"></asp:TextBox>
                                        <asp:TextBox ID="app_age" runat="server" Visible="false" MaxLength="150"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Anggota / No Kakitangan </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox13" runat="server"  class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                           
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jenis Pembiayaan <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="DD_Pelaburan" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" ></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tujuan</label>
                                    <div class="col-sm-8">
                                     <asp:DropDownList ID="dd_tujuan" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>

                              <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Negeri </label>
                                    <div class="col-sm-8">
                                        <%--<asp:DropDownList ID="DD_wilayah" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" AutoPostBack="true" onselectedindexchanged="ddkaw_SelectedIndexChanged"></asp:DropDownList>--%>
                                        <asp:DropDownList ID="DD_wilayah" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>

                                    </div>
                                </div>
                            </div>
                              <%--  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Cawangan  </label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="DD_cawangan" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"  ></asp:DropDownList>

                                    </div>
                                </div>
                            </div>--%>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Kategori Permohon : </label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="ddkatper" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"  ></asp:DropDownList>

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
                                         <textarea id="TextArea1" runat="server" rows="3" class="form-control validate[optional,custom[onlyaddress]] uppercase" maxlength="250"></textarea>

                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Alamat Surat-Menyurat <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                     <textarea id="TextArea4" runat="server" rows="3" class="form-control validate[required,custom[onlyaddress]] uppercase" maxlength="250"></textarea>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>

                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Poskod </label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="At_pcode" runat="server"  class="form-control validate[optional,custom[phone]]" MaxLength="5"></asp:TextBox> 

                                    </div>
                                </div>
                            </div>
                                <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Poskod </label>
                                    <div class="col-sm-8">
                                    <asp:TextBox ID="As_pcode" runat="server"  class="form-control validate[optional,custom[phone]]" MaxLength="5"></asp:TextBox> 
                                </div>
                            </div>
                                  </div>
                         </div>
                                 </div>



                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Negeri  </label>
                                    <div class="col-sm-8">
                                          <asp:DropDownList style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" ID="DD_NegriBind1"></asp:DropDownList>                                                                

                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Negeri </label>
                                    <div class="col-sm-8">
                                   <asp:DropDownList style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" ID="DD_NegriBind2"></asp:DropDownList>      
                                </div>
                            </div>
                                  </div>
                         </div>
                                 </div>



                                  <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Telefon (R)  </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="telefon_h" runat="server"  class="form-control validate[optional,custom[phone]]" MaxLength="12"></asp:TextBox> 

                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Telefon (P)  </label>
                                    <div class="col-sm-8">
                                    <asp:TextBox ID="telefon_o" runat="server"  class="form-control validate[optional,custom[phone]]" MaxLength="12"></asp:TextBox> 
                                </div>
                            </div>
                                  </div>
                         </div>
                                      </div>

                            <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Telefon (B)  </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="telefon_m" runat="server"  class="form-control validate[optional,custom[phone]]" MaxLength="12"></asp:TextBox> 

                                    </div>
                                </div>
                            </div> 
                         </div>
                                      </div>

                            <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama Bank  </label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="Bank_details" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Akaun </label>
                                    <div class="col-sm-8">
                                    <asp:TextBox ID="app_bank_acc_no" runat="server"  class="form-control validate[optional,custom[bank]]" MaxLength="15"></asp:TextBox> 
                                </div>
                            </div>
                                  </div>
                         </div>
                                      </div>
                            <div class="box-header with-border">
                            <h3 class="box-title">Butiran Permohonan</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jumlah Permohonan (RM) <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox4" runat="server" onchange="chk_pro_amt()" Style=" text-align:right;"    class="form-control validate[required,custom[number]]" MaxLength="12"></asp:TextBox> 

                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tempoh (Bulan) </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox12" runat="server"  class="form-control validate[optional,custom[number]]" MaxLength="12"></asp:TextBox> 

                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                           
                              
                              <div class="row">
                             <div class="col-md-12">
                                      <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Pasangan</h3>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama</label>
                                    <div class="col-sm-8">
                                   <asp:TextBox ID="TextBox14" runat="server"  class="form-control validate[optional,custom[textSp]] uppercase" MaxLength="150"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No KP Baru</label>
                                    <div class="col-sm-8">
                                   <asp:TextBox ID="TextBox16" runat="server" MaxLength="12"  class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                                     <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Alamat</label>
                                    <div class="col-sm-8">
                                   <textarea id="TextArea2" runat="server" rows="3" class="form-control validate[optional,custom[onlyaddress]] uppercase" maxlength="250"></textarea>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Telefon</label>
                                    <div class="col-sm-8">
                                   <asp:TextBox ID="TextBox18" runat="server" MaxLength="12"  class="form-control validate[optional,custom[phone]] uppercase"></asp:TextBox> 
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
                                  <asp:TextBox ID="TextBox56" runat="server"  class="form-control validate[optional,custom[phone]]" MaxLength="5"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Negeri</label>
                                    <div class="col-sm-8">
                                  <asp:DropDownList style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" ID="DD_NegriBind3"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                                   
                          

                                    <div id="Div2_1" runat="server" class="box-header with-border">
                            <h3 class="box-title">Maklumat Waris Terdekat (Ahli Keluarga)</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                                     <div id="Div2" class="nav-tabs-custom col-md-12 box-body" role="tabpanel" runat="server">
                                  <ul class="s1 nav nav-tabs" role="tablist">
                                                                <li id="Li1" runat="server" class="active"><a href="#ContentPlaceHolder1_p3" aria-controls="p3" role="tab" data-toggle="tab"><strong>Penama 1</strong></a></li>
                                                                <li id="Li2" runat="server"><a href="#ContentPlaceHolder1_p4" aria-controls="p4" role="tab" data-toggle="tab"><strong>Penama 2</strong></a></li>
                                                                                                                               
                                                            </ul>
                                  <div class="box-body">&nbsp;</div>
                                     <div class="tab-content">
                                                             <div role="tabpanel" class="tab-pane active" runat="server" id="p3">
                                                                 <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox19" runat="server"  class="form-control validate[optional,custom[textSp]] uppercase" MaxLength="150"></asp:TextBox> 

                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No KP Baru</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox21" runat="server" MaxLength="12"  class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase"></asp:TextBox> 

                                    </div>
                                </div>
                            </div>
                               
                                 </div>
                                 </div>

                              <div class="row">
                             <div class="col-md-12">
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Telefon </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox23" runat="server" MaxLength="12"  class="form-control validate[optional,custom[phone]] uppercase"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>
                                <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Hubungan </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="DD_Hubungan2" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>

                                                                 </div>
                                         
                            <div role="tabpanel" class="tab-pane" runat="server" id="p4">
                                  <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Nama </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox24" runat="server"  class="form-control validate[optional,custom[textSp]] uppercase" MaxLength="150"></asp:TextBox> 

                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No KP Baru</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox25" runat="server" MaxLength="12"  class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase"></asp:TextBox> 

                                    </div>
                                </div>
                            </div>
                                
                                 </div>
                                 </div>
                              <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Telefon </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox26" runat="server" MaxLength="12"  class="form-control validate[optional,custom[phone]] uppercase"></asp:TextBox> 

                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Hubungan</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="DD_Hubungan3" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                                </div>
                            </div>
                            </div>
                           <div class="box-body">&nbsp;</div>
                        <!-- /.box-header -->
                        <!-- form start -->
                      <div class="box-header with-border">
                                                                        
                                     <div id="Div3_1" runat="server" class="box-header with-border">
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
                                    
                                                           
                                           <asp:DropDownList ID="ddcat" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"  ></asp:DropDownList>                
                                                            
                                                            
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
                                                            <div class="col-md-3 col-sm-3">
                                                            <asp:RadioButton ID="RB11" runat="server"  Text=" Tetap"  AutoPostBack="true"  GroupName="tb1_tj"
                                                                    oncheckedchanged="RB11_CheckedChanged"/>
                                                          <%--  <label>Warganegara</label>--%>
                                                                                                                            
                                                            </div>
                                                            <div class="col-md-3 col-sm-3">
                                                            <asp:RadioButton ID="RB12" runat="server" Text=" Kontrak" GroupName="tb1_tj"  AutoPostBack="true" 
                                                                    oncheckedchanged="RB12_CheckedChanged" />                                                                    
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Gaji Bersih (RM) </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox31" runat="server"  onchange="chk_pro_amt()" Style=" text-align:right;"  class="form-control validate[optional,custom[number]]" MaxLength="12"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Gaji Kasar (RM)</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox33" runat="server"  onchange="chk_pro_amt()" Style=" text-align:right;"  class="form-control validate[optional,custom[number]]" MaxLength="12"></asp:TextBox> 
                                                                
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
                                   
                                                       <asp:DropDownList ID="ddcat2" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"  ></asp:DropDownList>
                                                            
                                                         
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
                                 
                                                            <div class="col-md-3 col-sm-3">
                                                            <asp:RadioButton ID="TJ21" runat="server"  Text=" Tetap"  AutoPostBack="true" 
                                                                    oncheckedchanged="RB111_CheckedChanged"/>
                                                          <%--  <label>Warganegara</label>--%>
                                                                                                                            
                                                            </div>
                                                            <div class="col-md-3 col-sm-3">
                                                            <asp:RadioButton ID="TJ22" runat="server" Text=" Kontrak"  AutoPostBack="true" 
                                                                    oncheckedchanged="RB112_CheckedChanged" />                                                                    
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">Gaji Bersih (RM) </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TextBox40" runat="server" onchange="chk_pro_amt()" Style=" text-align:right;"  class="form-control validate[optional,custom[number]]" MaxLength="12"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Gaji Kasar (RM)</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox41" runat="server" onchange="chk_pro_amt()" Style=" text-align:right;"   class="form-control validate[optional,custom[number]]" MaxLength="12"></asp:TextBox> 
                                                                
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
                            <%--<hr />--%>
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                   <asp:Button ID="Button3" runat="server" class="btn btn-danger" OnClientClick="return userValid();" Text="Kemaskini" OnClick="update"/>
                                                            <asp:Button ID="Button6" runat="server" class="btn btn-warning" Text="Cetak" Visible="false" UseSubmitBehavior="false" OnClick="clk_print"/>                                                            
                                                            <asp:Button ID="Button5" runat="server" class="btn btn-default" UseSubmitBehavior="false" OnClick="click_Reset" Text="Set Semula" />                                                            
                                                            <asp:Button ID="Button4" runat="server" class="btn btn-default" UseSubmitBehavior="false"  Text="Batal" />
                                 
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



