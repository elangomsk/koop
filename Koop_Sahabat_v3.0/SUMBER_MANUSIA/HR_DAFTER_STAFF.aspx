<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_DAFTER_STAFF.aspx.cs" Inherits="HR_DAFTER_STAFF" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
         <script type="text/javascript">

               
             function ShowImagePreview(input) {
                 if (input.files && input.files[0]) {
                     var reader = new FileReader();
                     reader.onload = function (e) {
                         $('#<%=ImgPrv.ClientID%>').prop('src', e.target.result)
                             .width(100)
                             .height(100);
                     };
                     reader.readAsDataURL(input.files[0]);
                 }
             }
    </script>
    <script type="text/javascript">
        function addTotal_bk() {
            var currentTime = new Date();
            var cyear = currentTime.getFullYear();
            var amt1 = $("#<%=TxtTaradu.ClientID %>").val().split('/');
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

        function addTotal_mo1() {
            var currentTime = new Date();
            var cyear = currentTime.getFullYear();
            var amt1 = $("#<%=TxtTaradu.ClientID %>").val().split('/');
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
    </script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
       <asp:UpdateProgress id="updateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
    <ProgressTemplate>
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
            <span style="border-width: 0px; position: fixed; font-weight:bold; padding: 50px; background-color: #FFFFFF; font-size: 16px; left: 40%; top: 40%;">Sila Tunggu. Rekod Sedang Diproses ...</span>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server" ScriptMode="Release">
    </asp:ScriptManager>

     
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1 id="h1_tag" runat="server">  </h1>
                        <ol class="breadcrumb">
                            <li id="bb1_text" runat="server"><a href="#"><i class="fa fa-dashboard"></i>Sumber Manusia</a></li>
                            <li id="bb2_text" runat="server" class="active"> Maklumat Kakitangan</li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
      <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                      <%--  <div class="box-header with-border">
                            <h3 class="box-title" id="h3_tag" runat="server">MAKLUMAT PERIBADI </h3>
                        </div>--%>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                              <div class="row">
                             <div class="col-md-12">
                             <div id="Div1" class="nav-tabs-custom col-md-12 box-body" role="tabpanel" runat="server">
                                  <ul class="s1 nav nav-tabs" role="tablist">
                                                                <li class="active" id="pp1" runat="server"><a href="#ContentPlaceHolder1_p1" aria-controls="p1" role="tab" data-toggle="tab" id="pt1" runat="server"><strong>Maklumat Peribadi</strong></a></li>
                                                                <li id="pp2" runat="server"><a href="#ContentPlaceHolder1_p2" aria-controls="p2" role="tab" data-toggle="tab" id="pt2" runat="server"><strong>Maklumat Penjawatan</strong></a></li>
                                                                                                                               
                                                            </ul>
                                  <div class="box-body">&nbsp;</div>
                                     <div class="tab-content">
                                                             <div role="tabpanel" class="tab-pane active" runat="server" id="p1">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl1_text" runat="server"> No Kakitangan  <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="Applcn_no" runat="server" class="form-control validate[optional] uppercase"
                                                                                MaxLength="10"></asp:TextBox>
                                        <asp:TextBox ID="Applcn_no1" runat="server" class="form-control validate[optional] uppercase"
                                                                                MaxLength="10" Visible="false"></asp:TextBox>
                                                                                
                                    </div>
                                </div>
                            </div> 
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl2_text" runat="server">No KP Baru / Pasport </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtic" runat="server" class="form-control validate[optional] uppercase"
                                                                                MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>                                
                                 </div>
                                </div>
                             <div class="row" id="txt1" runat="server" visible="false" style="padding-top:25px;" >
                                  <div class="row">
                             <div class="col-md-12">
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" runat="server">  No Kakitangan</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox2" runat="server" class="form-control validate[optional] uppercase"
                                                                                MaxLength="12"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl3_text" runat="server"> Nama <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="txtname" runat="server" class="form-control validate[optional] uppercase"
                                                                                MaxLength="150"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl4_text" runat="server"> Nama Syarikat / Nama Organisasi </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddorganisasi" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" AutoPostBack="true" OnSelectedIndexChanged="sel_orgbind">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl5_text" runat="server"> Perniagaan  </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="dd_org_pen" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" AutoPostBack="true" OnSelectedIndexChanged="sel_orgjaba">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl6_text" runat="server"> Jantina </label>
                                    <div class="col-sm-8">
                                          <asp:DropDownList ID="ddjanita" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl7_text" runat="server"> Gelaran</label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="ddgel" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl8_text" runat="server">  Tarikh Lahir </label>
                                    <div class="col-sm-8">
                                          <div class="input-group">
                                                                                    <asp:TextBox ID="TxtTaradu" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                                        onkeyup="addTotal_bk(this)" Onmouseover="addTotal_mo1(this)" placeholder="DD/MM/YYYY"></asp:TextBox>
                                               <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                                </div>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl9_text" runat="server"> Pangkat</label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="ddpang" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl10_text" runat="server"> Bangsa </label>
                                    <div class="col-sm-8">
                                           <asp:DropDownList ID="ddBangsa" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl11_text" runat="server">Umur</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="txtumur" runat="server" class="form-control validate[optional,custom[number]] uppercase gt_dt"
                                                                                    MaxLength="3"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl12_text" runat="server"> Agama </label>
                                    <div class="col-sm-8">
                                            <asp:DropDownList ID="ddagama" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl13_text" runat="server"> Negeri Lahir</label>
                                    <div class="col-sm-8">
                                          <asp:DropDownList ID="ddnegeri" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl14_text" runat="server"> Warganegara </label>
                                    <div class="col-sm-8">
                                           <asp:DropDownList ID="ddwarg" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl15_text" runat="server">Status Perkahwinan</label>
                                    <div class="col-sm-8">
                                          <asp:DropDownList ID="ddstsper" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl16_text" runat="server"> Alamat Tetap</label>
                                    <div class="col-sm-8">
                                            <textarea id="txtalamat" runat="server" class="form-control validate[optional] uppercase"
                                                                                    maxlength="250"></textarea>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label id="lbl17_text" runat="server"> Alamat Surat-Menyurat </asp:Label> <asp:CheckBox ID="ck_address" runat="server" OnCheckedChanged="clk_chgaddress" AutoPostBack="true" /></label>                            
                                    <div class="col-sm-8">
                                         <textarea id="txtalamatsurat" runat="server" class="form-control validate[optional] uppercase"
                                                                                    maxlength="250"></textarea>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl18_text" runat="server">  Poskod </label>
                                    <div class="col-sm-8">
                                            <asp:TextBox ID="txtpstcd" runat="server" class="form-control validate[optional,custom[phone]]"
                                                                                    MaxLength="5"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl19_text" runat="server"> Poskod</label>
                                    <div class="col-sm-8">
                                           <asp:TextBox ID="txtmposkod" runat="server" class="form-control validate[optional,custom[phone]]"
                                                                                    MaxLength="5"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl20_text" runat="server">  Bandar  </label>
                                    <div class="col-sm-8">
                                           <asp:TextBox ID="txtbandar" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl21_text" runat="server"> Bandar</label>
                                    <div class="col-sm-8">
                                           <asp:TextBox ID="txtmbandar" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl22_text" runat="server">  Negeri  </label>
                                    <div class="col-sm-8">
                                            <asp:DropDownList ID="ddnegeri1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl23_text" runat="server">Negeri</label>
                                    <div class="col-sm-8">
                                           <asp:DropDownList ID="ddnegeri2" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl24_text" runat="server">  No Telefon (R) </label>
                                    <div class="col-sm-8">
                                            <asp:TextBox ID="Txtnotel_R" runat="server" class="form-control validate[optional,custom[phone]]"
                                                                                    MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl25_text" runat="server"> No Telefon (B)</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="Txtnotel_P" runat="server" class="form-control validate[optional,custom[phone]]"
                                                                                    MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl26_text" runat="server"> Email  </label>
                                    <div class="col-sm-8">
                                             <asp:TextBox ID="txtemail" runat="server" class="form-control validate[optional,custom[email]]"
                                                                                   ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl27_text" runat="server">Gambar Pengguna </label>
                                    <div class="col-sm-8">
                                            <asp:FileUpload ID="FileUpload1" CssClass="fileupload" type="file" runat="server" onchange="ShowImagePreview(this);"/>
                                        <br />
                                        <asp:Image ID="ImgPrv" runat="server" class="profile-user-img img-responsive img-circle" Width="100px" Height="100px" />
                                        <asp:TextBox ID="TextBox4" runat="server" class="form-control validate[optional] uppercase" Visible="false"></asp:TextBox>
                                        <br />
                                        <span style="color: Red;">(NOTA : Saiz Imej Maksimum Adalah - 100 KB)</span>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>


                 
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                <asp:TextBox ID="lbl_name" runat="server" class="form-control validate[optional] uppercase" Visible="false"></asp:TextBox>
                                   <asp:Button ID="Button3" runat="server" class="btn btn-danger" Text="Simpan" OnClick="Button3_Click"/>
                                                                                <asp:Button ID="Button2" Visible="false" runat="server" class="btn btn-warning" UseSubmitBehavior="false"
                                                                                    Text="Cetak" OnClick="cetak_Click" />
                                <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" UseSubmitBehavior="false"  />
                                <asp:Button ID="Button8" runat="server" class="btn btn-default" Text="Kembali" OnClick="Click_bck" UseSubmitBehavior="false"  />
                            </div>
                           </div>
                               </div>
                                                                   <div class="row">
                                <div class="col-md-12 col-sm-4" style="text-align: center; display:none; line-height: 13px;">
                                    <rsweb:ReportViewer ID="RptviwerStudent" runat="server" Width="50%">
                                    </rsweb:ReportViewer>
                                </div>
                            </div>
</div>
                                          <div role="tabpanel" class="tab-pane" runat="server" id="p2">                            
                          
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>

                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl28_text" runat="server">  Jawatan <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                            <asp:DropDownList ID="ddjawatan" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl29_text" runat="server"> Skim </label>
                                    <div class="col-sm-8">
                                          <asp:DropDownList ID="ddskim" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl30_text" runat="server">   Kategori </label>
                                    <div class="col-sm-8">
                                             <asp:DropDownList ID="ddKategori" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl31_text" runat="server"> Gred  </label>
                                    <div class="col-sm-8">
                                          <asp:DropDownList ID="ddgred" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl32_text" runat="server">    Jabatan  </label>
                                    <div class="col-sm-8">
                                             <asp:DropDownList ID="ddjabatan" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"
                                                                                    AutoPostBack="true" OnSelectedIndexChanged="ddjabatan_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl33_text" runat="server"> Status Penjawatan  </label>
                                    <div class="col-sm-8">
                                          <asp:DropDownList ID="ddstspenj" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl34_text" runat="server">    Unit  </label>
                                    <div class="col-sm-8">
                                              <asp:DropDownList ID="ddunit" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl35_text" runat="server"> Tarikh Mula Khidmat  </label>
                                    <div class="col-sm-8">
                                          <div class="input-group">
                                                                                    <asp:TextBox ID="txttmk" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                                        placeholder="DD/MM/YYYY"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl36_text" runat="server">    Tarikh Mula Lantikan <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                              <div class="input-group">
                                                                                    <asp:TextBox ID="txttlan" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"></asp:TextBox>
                                                  <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                                </div>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl37_text" runat="server">Tarikh Akhir Lantikan <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                          <div class="input-group">
                                                                                    <asp:TextBox ID="txthin" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                                        placeholder="DD/MM/YYYY"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl38_text" runat="server">   Penyelia 1 </label>
                                    <div class="col-sm-8">
                                             <asp:DropDownList ID="ddpen1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl39_text" runat="server"> Penyelia 2 </label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="ddpen2" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl40_text" runat="server">  Waktu Bekerja </label>
                                    <div class="col-sm-8">
                                             <asp:DropDownList ID="DropDownList1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl41_text" runat="server"> Sebab Pergerakan  </label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="ddSebab" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                                 <div class="box-header with-border" style="border-bottom: 1px solid red;">
                            <h3 class="box-title"> Maklumat Berhenti </h3>
                        </div>
                                <br />
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl42_text" runat="server">   Tarikh Berhenti  </label>
                                    <div class="col-sm-8">
                                             <div class="input-group">
                                                                                    <asp:TextBox ID="txttarber" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                                        placeholder="PICK DATE"></asp:TextBox>
                                                  <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                                </div>
                                    </div>
                                </div>
                            </div>
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl43_text" runat="server">Sebab Berhenti </label>
                                    <div class="col-sm-8">
                                          <asp:DropDownList ID="ddsebabber" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                                </asp:DropDownList>
                                        <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <hr style="border-top: 1px solid red;" />
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                <asp:Button ID="Button5" runat="server" class="btn btn-danger" Text="Simpan" Type="submit" UseSubmitBehavior="false" OnClick="Button5_Click" />
                                                                                <asp:Button ID="Button6" runat="server" Text="Kemaskini" Visible="false" class="btn btn-danger" UseSubmitBehavior="false"
                                                                                     OnClick="Button6_Click" />
                                                                                <asp:Button ID="Button7" runat="server" Text="Set Semula" class="btn btn-default"
                                                                                    OnClick="Button7_Click" />
                                                                                    <asp:Button ID="btn_hups" runat="server" Text="Hapus" class="btn btn-warning" UseSubmitBehavior="false" onclick="btn_hups_Click" OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;" />
                            </div>
                           </div>
                               </div>

                                 <div class="box-body">&nbsp;
                                    </div>
                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                   <asp:GridView ID="gvSelected" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging">
                                       <PagerStyle CssClass="pager" />
                                                                                    <Columns>
                                                                                     <asp:TemplateField HeaderText="BIL" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                                    ItemStyle-Width="150" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                                        <%--<asp:BoundField DataField="pos_start_dt" HeaderText="Tarikh Mula" />--%>
                                                                                         <asp:TemplateField HeaderText="Tarikh Mula" ItemStyle-HorizontalAlign="center" >
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="lblSubItemName" runat="server" Text='<%# Eval("pos_start_dt")%>'
                                                                                                    CommandArgument=' <%#Eval("hr_jaw_desc")+","+ Eval("pos_start_dt")%>'
                                                                                                    CommandName="Add" OnClick="lblSubItemName_Click" Font-Bold Font-Underline>
                                                <a  href="#"></a>
                                                                                                </asp:LinkButton>
                                                                                                <asp:Label ID="lb_sno" runat="server" Text='<%# Eval("pos_staff_no") %>' Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lb_sdt" runat="server" Text='<%# Eval("pos_start_dt") %>' Visible="false"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:BoundField DataField="pos_end_dt" HeaderText="Tarikh Tamat" />
                                                                                        <%--<asp:TemplateField HeaderText="JAWATAN" ItemStyle-HorizontalAlign="Left">
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="lblSubItemName" runat="server" Text='<%# Eval("hr_jaw_desc")%>'
                                                                                                    CommandArgument=' <%#Eval("hr_jaw_desc")+","+ Eval("pos_start_dt")%>'
                                                                                                    CommandName="Add" OnClick="lblSubItemName_Click">
                                                <a  href="#"></a>
                                                                                                </asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>--%>
                                                                                         <asp:BoundField DataField="pos_subjek" ItemStyle-HorizontalAlign="Left" HeaderText="SUBJEK" />
                                                                                         <asp:BoundField DataField="hr_jaw_desc" HeaderText="JAWATAN" />
                                                                                        <asp:BoundField DataField="hr_gred_desc" HeaderText="Gred" />
                                                                                        <asp:BoundField DataField="hr_unit_desc" ItemStyle-HorizontalAlign="Left" HeaderText="Seksyen" />
                                                                                        <asp:BoundField DataField="hr_jaba_desc" ItemStyle-HorizontalAlign="Left" HeaderText="Jabatan" />
                                                                                       <%-- <asp:BoundField DataField="org_name" ItemStyle-HorizontalAlign="Left" HeaderText="Organisasi" />--%>
                                                                                         <asp:TemplateField HeaderText="HAPUS">
                                                                <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="RadioButton1" runat="server"/>
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
                                </div>

                            
                           <div class="box-body">&nbsp;
                                    </div>

                        </div>
                                 
                            </div>
                                   </div>
                                  </div>
                    </div>
                </div>
            </div>
                       </div>
            <!-- /.row -->
             </ContentTemplate>
             <Triggers>
               <asp:PostBackTrigger ControlID="Button3"  />
               <asp:PostBackTrigger ControlID="Button2"  />
           </Triggers>
    </asp:UpdatePanel>
        
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
   
</asp:Content>





