<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_DAF_FAMLI.aspx.cs" Inherits="HR_DAF_FAMLI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       
   <script type="text/javascript">
     function addTotal_bk() {
         var currentTime = new Date();
         var cyear = currentTime.getFullYear();
         var amt1 = $("#<%=txttarlah.ClientID %>").val().split('/');
         var t_amt = parseInt(amt1[1]);
         var yr = parseInt(amt1[2]);
         var age = cyear - yr;

         if (amt1 != "") {
             if (age >= '0') {
                 $(".gt_dt").val(age);
             }
             else {
                 $(".gt_dt").val('');
             }
         }

     }

     function addTotal_mo() {
         var currentTime = new Date();
         var cyear = currentTime.getFullYear();
         var amt1 = $("#<%=txttarlah.ClientID %>").val().split('/');
         var t_amt = parseInt(amt1[1]);
         var yr = parseInt(amt1[2]);
         var age = cyear - yr;

         if (amt1 != "") {
             if (age >= '0') {
                 $(".gt_dt").val(age);
             }
             else {
                 $(".gt_dt").val('');
             }
         }

     }

     function addTotal_bk1() {
         var currentTime = new Date();
         var cyear = currentTime.getFullYear();
         var amt1 = $("#<%=TextBox20.ClientID %>").val().split('/');
         var t_amt = parseInt(amt1[1]);
         var yr = parseInt(amt1[2]);
         var age = cyear - yr;

         if (amt1 != "") {
             if (age >= '0') {
               
                 $(".ss1").val(age);
             }
             else {
                 $(".ss1").val('');
             }
         }

     }

     function addTotal_mo1() {
         var currentTime = new Date();
         var cyear = currentTime.getFullYear();
         var amt1 = $("#<%=TextBox20.ClientID %>").val().split('/');
         var t_amt = parseInt(amt1[1]);
         var yr = parseInt(amt1[2]);
         var age = cyear - yr;

         if (amt1 != "") {
             if (age >= '0') {

                 $(".ss1").val(age);
             }
             else {
                 $(".ss1").val('');
             }
         }

     }

        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1 id="h1_tag" runat="server">   PENGURUSAN KAKITANGAN         </h1>
                        <ol class="breadcrumb">
                            <li id="bb1_text" runat="server"><a href="#"><i class="fa fa-dashboard"></i>SUMBER_MANUSIA</a></li>
                            <li class="active" id="bb2_text" runat="server">  PENDAFTARAN MAKLUMAT KELUARGA       </li>
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
                            <h3 class="box-title" id="h3_tag" runat="server"> MAKLUMAT PERIBADI </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl1_text" runat="server"> No Kakitangan</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtaplno" runat="server" class="form-control validate[optional] uppercase" MaxLength="150"></asp:TextBox>
                                                         <asp:TextBox ID="Applcn_no1" runat="server" class="form-control validate[optional] uppercase"
                                                                                MaxLength="150" Visible="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 box-body">
                                <div class="form-group">                                    
                                    <div class="col-sm-8">
                                        <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Kempali" UseSubmitBehavior="false" Type="submit" onclick="Click_bck" />
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl2_text" runat="server">   Nama Kakitangan</label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="txtnama" runat="server" class="form-control validate[optional] uppercase" MaxLength="150"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox1" style="display:none;" runat="server" class="form-control validate[optional]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl3_text" runat="server">  Gred </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtgred" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl4_text" runat="server"> Nama Syarikat / Organisasi </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txt_org" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl5_text" runat="server"> Perniagaan </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox2" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl6_text" runat="server">  Jabatan  </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtjab" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl7_text" runat="server"> Jawatan </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txtjaw" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                              <div class="box-body">&nbsp;</div>
                          <div class="row">
                             <div class="col-md-12">
                             <div id="Div1" class="nav-tabs-custom col-md-12 box-body" role="tabpanel" runat="server">
                                <ul class="s1 nav nav-tabs" role="tablist">
                                                                <li id="pp1" runat="server" class="active"><a href="#ContentPlaceHolder1_p1" aria-controls="p1" role="tab" data-toggle="tab" id="pt1" runat="server"><strong>Maklumat Pasangan</strong></a></li>
                                                                <li id="pp2" runat="server"><a href="#ContentPlaceHolder1_p2" aria-controls="p2" role="tab" data-toggle="tab" id="pt2" runat="server"><strong>Maklumat Anak</strong></a></li>
                                                                <li id="pp3" runat="server"><a href="#ContentPlaceHolder1_p3" aria-controls="p3" role="tab" data-toggle="tab" id="pt3" runat="server"><strong>Orang Hubungan Kecemasan</strong></a></li>
                                                            </ul>

                                <div class="tab-content" style="padding-top: 20px">
                                                            <div role="tabpanel" class="tab-pane active" runat="server" id="p1">

                                                                 <div class="box-header with-border">
                            <h3 class="box-title" id="h3_tag2" runat="server"> MAKLUMAT PASANGAN </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>

                                                                <div class="row">
                             <div class="col-md-12">                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl9_text" runat="server">  Nama <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txtnama1" runat="server" class="form-control validate[optional] uppercase" MaxLength="150"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl8_text" runat="server">  No KP / Pasport  <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtic" runat="server" class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase" MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl10_text" runat="server"> Tarikh Lahir </label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                                        <asp:TextBox ID="txttarlah" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" Onmouseover="addTotal_mo(this)" onkeyup="addTotal_bk(this)" placeholder="DD/MM/YYYY"></asp:TextBox>
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl11_text" runat="server">  Warganegara  </label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="ddwarga" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl12_text" runat="server">   Alamat  </label>
                                    <div class="col-sm-8">
                                        <textarea id="txtalamat" runat="server" class="form-control validate[optional,custom[onlyaddress1]] uppercase" maxlength="250"></textarea>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl13_text" runat="server">  Umur</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txtumur" runat="server" class="form-control validate[optional,custom[number]] uppercase gt_dt"  MaxLength="3"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                                                                <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl14_text" runat="server">   No Telefon </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txttel" runat="server" class="form-control validate[optional,custom[phone]] uppercase" MaxLength="11"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl15_text" runat="server">  Status Pekerjaan : </label>
                                    <div class="col-sm-8">
                                       <div class="col-md-3 col-sm-1">
                                                        <label>
                                                            <asp:RadioButton ID="Rdbwar" runat="server" AutoPostBack="true" 
                                                            GroupName="war11" oncheckedchanged="Rdbwar_CheckedChanged" />&nbsp;
                                                        </label>
                                                        <br />
                                                    </div>
                                        <div class="col-md-3 col-sm-1">
                                                        <label>
                                                            <asp:RadioButton ID="RdbBwar" runat="server" AutoPostBack="true" 
                                                            GroupName="war12" oncheckedchanged="RdbBwar_CheckedChanged" />&nbsp;
                                                        </label>
                                                        <br />
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl16_text" runat="server">   Jawatan & Majikan  </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtjawmaj" runat="server" class="form-control validate[optional,custom[textSp2]] uppercase" MaxLength="150"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl17_text" runat="server">   Status OKU:</label>
                                    <div class="col-sm-8">
                                        <div class="col-md-3 col-sm-1">
                                                        <label>
                                                            <asp:RadioButton ID="RadioButton6" runat="server" AutoPostBack="true"  
                                                            GroupName="war13" oncheckedchanged="RadioButton6_CheckedChanged" />&nbsp;
                                                        </label>
                                                        <br />
                                                    </div>
                                         <div class="col-md-3 col-sm-1">
                                                        <label>
                                                            <asp:RadioButton ID="RadioButton9" runat="server" AutoPostBack="true"  
                                                            GroupName="war14" oncheckedchanged="RadioButton9_CheckedChanged" />&nbsp;
                                                        </label>
                                                        <br />
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
                                <asp:Button ID="Button2" runat="server" class="btn btn-danger" Text="Simpan" UseSubmitBehavior="false" Type="submit" onclick="Button2_Click" />
                               



                                 </div>
                           </div>
                               </div>
                                                                <div class="box-body">&nbsp;</div>
                                          </div>
                                                                 <div role="tabpanel" class="tab-pane" runat="server" id="p2">
                                                                     
                                                                         <div class="box-header with-border">
                            <h3 class="box-title" id="h3_tag3" runat="server">MAKLUMAT ANAK </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>

                                                                         
                                                                      <div class="row">
                             <div class="col-md-12">
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl19_text" runat="server">   Nama <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox19" runat="server" class="form-control validate[optional] uppercase" MaxLength="150"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl18_text" runat="server">   No KP / Pasport  <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox18" runat="server" class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase" MaxLength="12"></asp:TextBox>
                                                        <asp:TextBox ID="txtsqno" runat="server" Visible="false" class="form-control validate[optional] uppercase" MaxLength="10"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 
                          
                                 </div>
                                </div>
                                                                     <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl20_text" runat="server">  Tarikh Lahir  </label>
                                    <div class="col-sm-8">
                                      <div class="input-group">
                                                        <asp:TextBox ID="TextBox20" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" onkeyup="addTotal_bk1(this)" Onmouseover="addTotal_mo1(this)" placeholder="DD/MM/YYYY"></asp:TextBox>
                                          <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl21_text" runat="server">   Umur</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox21" runat="server" class="form-control validate[optional,custom[number]] uppercase ss1" MaxLength="3"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                                                                     <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl22_text" runat="server">    Warganegara  </label>
                                    <div class="col-sm-8">
                                      <asp:DropDownList ID="DropDownList2" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl23_text" runat="server">   Jantina </label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="DropDownList1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl24_text" runat="server">    Status Pekerjaan </label>
                                    <div class="col-sm-9">
                                     <div class="col-lg-3 col-md-4 col-sm-1">
                                                        <label>
                                                            <asp:RadioButton ID="RadioButton1" runat="server" AutoPostBack="true" 
                                                            GroupName="war1" oncheckedchanged="RadioButton1_CheckedChanged" />&nbsp;
                                                        </label>
                                                        <br />
                                                    </div>
                                        <div class="col-lg-3 col-md-4 col-sm-1">
                                                        <label>
                                                            <asp:RadioButton ID="RadioButton2" runat="server" AutoPostBack="true" 
                                                            GroupName="war2" oncheckedchanged="RadioButton2_CheckedChanged" />&nbsp;
                                                        </label>
                                                        <br />
                                                    </div>
                                         <div class="col-lg-3 col-md-4 col-sm-1">
                                                        <label>
                                                            <asp:RadioButton ID="RadioButton3" runat="server" AutoPostBack="true" 
                                                            GroupName="war3" oncheckedchanged="RadioButton3_CheckedChanged" />&nbsp;
                                                            
                                                        </label>
                                                        <br />
                                                    </div>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl25_text" runat="server">    Hubungan </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="DropDownList3" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl26_text" runat="server">   Status Pendidikan </label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="DropDownList4" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl27_text" runat="server">   Status Perkahwinan :</label>
                                    <div class="col-sm-8">
                                       <div class="col-lg-4 col-md-4 col-sm-1">
                                                        <label >
                                                            <asp:RadioButton ID="RadioButton4" runat="server" AutoPostBack="true" 
                                                            GroupName="war4" oncheckedchanged="RadioButton4_CheckedChanged" />&nbsp;
                                                        </label>
                                                        <br />
                                                    </div>
                                                    <div class="col-lg-4 col-md-4 col-sm-1">
                                                        <label>
                                                            <asp:RadioButton ID="RadioButton5" runat="server" AutoPostBack="true" 
                                                            GroupName="war5" oncheckedchanged="RadioButton5_CheckedChanged" />&nbsp;
                                                        </label>
                                                        <br />
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl28_text" runat="server">   Status Tanggungan :  </label>
                                    <div class="col-sm-8">
                                       <div class="col-lg-4 col-md-4 col-sm-1">
                                                        <label>
                                                            <asp:RadioButton ID="RadioButton10" runat="server" AutoPostBack="true" 
                                                            GroupName="war6" oncheckedchanged="RadioButton10_CheckedChanged" />&nbsp;
                                                        </label>
                                                        <br />
                                                    </div>
                                                    <div class="col-lg-4 col-md-4 col-sm-1">
                                                        <label>
                                                            <asp:RadioButton ID="RadioButton11" runat="server" AutoPostBack="true" 
                                                            GroupName="war7" oncheckedchanged="RadioButton11_CheckedChanged" />&nbsp;
                                                        </label>
                                                        <br />
                                                    </div>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl29_text" runat="server">   Status OKU :</label>
                                    <div class="col-sm-8">
                                       <div class="col-lg-4 col-md-4 col-sm-1">
                                                        <label>
                                                            <asp:RadioButton ID="RadioButton7" runat="server" AutoPostBack="true" 
                                                            GroupName="war8" oncheckedchanged="RadioButton7_CheckedChanged" />&nbsp;
                                                        </label>
                                                        <br />
                                                    </div>
                                                    <div class="col-lg-4 col-md-4 col-sm-1">
                                                        <label>
                                                            <asp:RadioButton ID="RadioButton8" runat="server" AutoPostBack="true" 
                                                            GroupName="war9" oncheckedchanged="RadioButton8_CheckedChanged" />&nbsp;
                                                        </label>
                                                        <br />
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
                                <asp:Button ID="Button5" runat="server" class="btn btn-danger" Text="Simpan" UseSubmitBehavior="false" Type="submit" onclick="Button5_Click" />
                                                        <asp:Button ID="Button6" runat="server" Visible="false" Text="Kemaskini" class="btn btn-danger" UseSubmitBehavior="false" onclick="Button6_Click" />
                                                        <asp:Button ID="Button8" runat="server" Text="Set Semula" class="btn btn-default" UseSubmitBehavior="false" onclick="marst_Click" />
                                                        <asp:Button ID="Button7" runat="server" Text="Hapus" class="btn btn-warning" UseSubmitBehavior="false" onclick="Button7_Click" OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;"/>
                            </div>
                           </div>
                               </div>
                                                                      <div class="box-body">&nbsp;</div>
                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                 <asp:GridView ID="gvSelected" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging">
                                     <PagerStyle CssClass="pager" />
                                      <Columns>
                                      <asp:TemplateField HeaderText="BIL" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"/>     </ItemTemplate>
                                                                </asp:TemplateField>
                                       <asp:TemplateField HeaderText="NAMA" >  
                                       <ItemStyle HorizontalAlign="Left" Width="30%" />
                                            <ItemTemplate>  
                                               <asp:LinkButton ID="lblSubItemName"    runat="server" Text='<%# Eval("chl_name")%>' CommandArgument='<%# Eval("chl_seq_no")%>' CommandName="Add"  onclick="lblSubItemName_Click" Font-Bold Font-Underline>
                                                <a  href="#"></a>
                                                  </asp:LinkButton>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                       <asp:BoundField DataField="Contact_Name" HeaderText="HUBUNGAN"  ItemStyle-Width="10%"  />   
                                       <asp:BoundField DataField="chl_dependant_sts_ind" HeaderText="STATUS TANGGUNGAN"  ItemStyle-Width="10%" />
                                        <asp:TemplateField HeaderText="HAPUS">
                                                                <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                    <ItemTemplate>
                                                                      <asp:RadioButton ID="RadioButton1" runat="server"   onclick = "RadioCheck(this);"/> <asp:HiddenField ID="HiddenField1" runat="server"  />
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
                                                                     <div class="box-body">&nbsp;</div>
 </div>

                                                                <div role="tabpanel" class="tab-pane" runat="server" id="p3">
                                                                     <div class="box-header with-border">
                            <h3 class="box-title"  id="h3_tag4" runat="server"> ORANG HUBUNGAN KECEMASAN </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                                                                     <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"  id="lbl30_text" runat="server">   Nama  </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox22" runat="server" class="form-control validate[optional] uppercase" MaxLength="150"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"  id="lbl31_text" runat="server">   No Telefon (R/P)</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox23" runat="server" class="form-control validate[optional,custom[phone]]" MaxLength="11"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                                                                     <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"  id="lbl32_text" runat="server">   Hubungan </label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="DropDownList19" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"  id="lbl33_text" runat="server">    No Telefon (B)</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox9" runat="server" class="form-control validate[optional,custom[phone]]" MaxLength="11"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                                                    <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"  id="lbl34_text" runat="server">  Alamat </label>
                                    <div class="col-sm-8">
                                       <textarea id="txtAla" runat="server" class="form-control validate[optional,custom[onlyaddress1]] uppercase" maxlength="250"></textarea>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                                                        </div>
                                                                     <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                <asp:Button ID="Button3" runat="server" class="btn btn-danger" Text="Simpan" UseSubmitBehavior="false" Type="submit" onclick="Button3_Click"/>



                                 </div>
                           </div>
                               </div>
                                                                    <div class="box-body">&nbsp;</div>

                                                                    </div>

                                    </div>
                                </div>
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



