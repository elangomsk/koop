<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../Pelaburan_Anggota/PP_cetak_perjanjian.aspx.cs" Inherits="PP_cetak_perjanjian" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
      <script type="text/javascript">
 function chk_pro_amt() {            
   
     var amt2 = Number($("#<%=TextBox40.ClientID%>")[0].value);
     var amt3 = Number($("#<%=TextBox41.ClientID%>")[0].value);
     var amt4 = Number($("#<%=TextBox31.ClientID%>")[0].value);
     var amt5 = Number($("#<%=TextBox33.ClientID%>")[0].value);
          
          
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
      
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" style="height: 895px;">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1> Cetak Dokumen Perjanjian</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>  Pelaburan Anggota </a></li>
                            <li class="active">  Cetak Dokumen Perjanjian</li>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Permohonan <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TXTNOKP" runat="server"  class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase" maxlength="12"></asp:TextBox>
                                          <asp:Panel ID="autocompleteDropDownPanel" runat="server" ScrollBars="Auto" Height="150px"
                                                            Font-Size="Medium" HorizontalAlign="Left" Wrap="False" />
                                                        <cc1:AutoCompleteExtender ServiceMethod="GetCompletionList" MinimumPrefixLength="1"
                                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="TXTNOKP"
                                                            ID="AutoCompleteExtender1" CompletionListElementID="autocompleteDropDownPanel"
                                                            CompletionListCssClass="form-control uppercase" runat="server" FirstRowSelected="false">
                                                        </cc1:AutoCompleteExtender>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                   <div class="col-sm-8">
                                        <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Carian" UseSubmitBehavior="false" OnClick="Searchbtn_Click"/>
                                        <asp:Button ID="Button2" runat="server" class="btn btn-default"  Text="Set Semula" usesubmitbehavior="false" OnClick="Reset_btn"/>
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
                                    </div>
                                </div>
                            </div>
                                  <%-- <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Wilayah / Pejabat </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="TextBox3" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>--%>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tujuan</label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="TextBox8" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                           
                             <div class="row">
                             <div class="col-md-12">
                                  <%--<div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Cawangan / Jabatan</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox4" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>--%>
                               
                                  </div>
                         </div>

                              <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Amaun Pengeluaran (RM) </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox13" runat="server" class="form-control validate[optional,custom[number]]"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Tempoh (Bulan)</label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="TextBox14" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>

                            <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">No Rujukan <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="no_rujukan" runat="server" class="form-control validate[optional] uppercase" MaxLength="20"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-8">
                                    <asp:Button ID="Button12" runat="server" class="btn btn-danger"  Text="Simpan" usesubmitbehavior="false" onclick="Click_rujukan"/>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>

                                                   
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
                                      <asp:TextBox ID="TextBox31" runat="server" onchange="chk_pro_amt()" Style=" text-align:right;"   class="form-control validate[optional,custom[number]]" MaxLength="12"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Gaji Kasar (RM)</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox33" runat="server" onchange="chk_pro_amt()" Style=" text-align:right;"   class="form-control validate[optional,custom[number]]" MaxLength="12"></asp:TextBox> 
                                                                
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
                                      <asp:TextBox ID="TextBox40" runat="server"  onchange="chk_pro_amt()" Style=" text-align:right;"  class="form-control validate[optional,custom[number]]" MaxLength="12"></asp:TextBox> 
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
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                    <asp:Button ID="Button3" runat="server" class="btn btn-warning" UseSubmitBehavior="false" Text="Cetak Surat Tawaran" OnClick="print_offerletter"/>
                                                            <asp:Button ID="Button4" runat="server" class="btn btn-warning" UseSubmitBehavior="false"  Text="Cetak Kemudahan Perjanjian" OnClick="print_offerletter"/>                                                            
                                                          <%--  <asp:Button ID="Button5" runat="server" class="btn btn-warning" UseSubmitBehavior="false"  Text="Cetak Perjanjian Penjamin 1" OnClick="print_offerletter"/>                                                            
                                                            <asp:Button ID="Button6" runat="server" class="btn btn-warning" UseSubmitBehavior="false"  Text="Cetak Perjanjian Penjamin 2" OnClick="print_offerletter"/>                                                            
                                                            <asp:Button ID="Button7" runat="server" class="btn btn-warning" UseSubmitBehavior="false"  Text="Cetak Perjanjian Penjamin 3" OnClick="print_offerletter"/>--%>
                            </div>
                           </div>
                               </div>
                                     <div class="row">
                                                        <div class="col-md-12 col-sm-2" style="text-align: center">
                                                           <rsweb:ReportViewer ID="Rptviwer_cetak" runat="server"></rsweb:ReportViewer>
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



