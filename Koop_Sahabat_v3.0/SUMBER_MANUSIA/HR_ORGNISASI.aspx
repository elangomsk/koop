<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_ORGNISASI.aspx.cs" Inherits="HR_ORGNISASI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        <script type="text/javascript">

        function ShowImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=ImgPrv.ClientID%>').prop('src', e.target.result)
                        .width(150)
                        .height(100);
                };
                reader.readAsDataURL(input.files[0]);
                }
            }
    </script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:ScriptManager ID="ScriptManagerCalendar" ScriptMode="Release" runat="server">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1 id="h1_tag" runat="server">  SELENGGARA           </h1>
                        <ol class="breadcrumb">
                            <li id="bb1_text" runat="server"><a href="#"><i class="fa fa-dashboard"></i>SUMBER_MANUSIA</a></li>
                            <li class="active" id="bb2_text" runat="server">  SELENGGARA MAKLUMAT ORGANISASI       </li>
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
                            <h3 class="box-title" id="h3_tag" runat="server"> MAKLUMAT ORGANISASI </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl1_text" runat="server">  No. Daftar Syarikat <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="org_no" runat="server" class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase" MaxLength="10"></asp:TextBox>
                                                        <asp:TextBox ID="refno" Visible="false" runat="server" class="form-control validate[optional]" MaxLength="10"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                           
                                 
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl2_text" runat="server">   Nama Syarikat <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="org_name" runat="server" class="form-control validate[optional,custom[textSp11]] uppercase" MaxLength="100"></asp:TextBox>
                                        <asp:Label ID="Label6" runat="server" Text="Label" Visible="false"></asp:Label>
                                    </div>
                                </div>
                            </div>
                                </div>
                                </div>
                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl3_text" runat="server"> Alamat</label>
                                    <div class="col-sm-8">
                                       <textarea id="org_address" runat="server" class="form-control validate[optional,custom[onlyaddress]] uppercase" maxlength="300"></textarea>
                                    </div>
                                </div>
                            </div>
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl4_text" runat="server">  No KWSP </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="org_epf_no" runat="server" class="form-control validate[optional,custom[onlyLetterNumber]] uppercase" MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                   </div>
                                     </div>
                           

                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl5_text" runat="server">   No PERKESO  </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="org_socso_no" runat="server" class="form-control validate[optional,custom[onlyLetterNumber]] uppercase" MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl6_text" runat="server">   Poskod   </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="org_postcd" runat="server" class="form-control validate[optional,custom[phone]] uppercase" MaxLength="5"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                                  <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl7_text" runat="server">    No Cukai Pendapatan  </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="org_income_tax_no" runat="server" class="form-control validate[optional,custom[onlyLetterNumber]] uppercase" MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl8_text" runat="server">   Bandar   </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="org_city" runat="server" class="form-control validate[optional] uppercase" MaxLength="40"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl9_text" runat="server">    No COID </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="org_co_id" runat="server" class="form-control validate[optional,custom[onlyLetterNumber]] uppercase" MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl10_text" runat="server">   Negeri   </label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="DD_NegriBind1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional] uppercase">
                                                        </asp:DropDownList>
                                         <asp:TextBox ID="txt_negeri" Visible="false" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>


                                  <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl11_text" runat="server">    Email</label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="org_email" runat="server" class="form-control validate[optional,custom[email]]" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl12_text" runat="server">   No Telefon   </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="org_phone_no" runat="server" class="form-control validate[optional,custom[phone]] uppercase" MaxLength="11"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                                  <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl13_text" runat="server">    No Fax   </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="org_fax_no" runat="server" class="form-control validate[optional,custom[phone]] uppercase" MaxLength="10"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body ">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl14_text" runat="server">   Sektor   </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="org_comp_sector" runat="server" class="form-control validate[optional] uppercase" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                      

                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl15_text" runat="server">Lampiran </label>
                                    <div class="col-sm-8">
                                            <asp:FileUpload ID="FileUpload1" CssClass="fileupload" type="file" runat="server" onchange="ShowImagePreview(this);"/>
                                        <br />
                                        <asp:Image ID="ImgPrv" runat="server" class="profile-user-img img-responsive img-square" Width="150px" Height="100px" />
                                        <asp:TextBox ID="TextBox3" runat="server" class="form-control validate[optional] uppercase" Visible="false"></asp:TextBox>
                                        <br />
                                        <span style="color: Red;">(NOTA : Saiz Imej Maksimum Adalah - 100 KB)</span>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body ">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">     </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="imname" Visible="false" runat="server" class="form-control" ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            

                                   <div class="box-header with-border">
                            <h3 class="box-title" id="h3_tag2" runat="server">MAKLUMAT ORANG HUBUNGAN </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>

                                  <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl16_text" runat="server">    No KP Baru  </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="org_contact_icno1" runat="server" class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase" MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body ">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl17_text" runat="server">   No KP Baru  </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="org_contact_icno2" runat="server" class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase" MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl18_text" runat="server">    Nama </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="org_contact_name1" runat="server" class="form-control validate[optional,custom[textSp]] uppercase" MaxLength="150"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body ">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl19_text" runat="server">   Nama  </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="org_contact_name2" runat="server" class="form-control validate[optional,custom[textSp]] uppercase" MaxLength="150"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl20_text" runat="server">    Jawatan </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="org_contact_post1" runat="server" class="form-control validate[optional,custom[jawa]] uppercase" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body ">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl21_text" runat="server">   Jawatan  </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="org_contact_post2" runat="server" class="form-control validate[optional,custom[jawa]] uppercase" MaxLength="100"></asp:TextBox>
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
                                 <asp:Button ID="Button8" runat="server" class="btn btn-danger" Text="Simpan" Type="submit" UseSubmitBehavior="false" OnClick="insert_Click"/>
                                                        <asp:Button ID="Button10" runat="server" Text="Set Semula" class="btn btn-default"  OnClick="Button5_Click"/>
                                <asp:Button ID="Button2" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" OnClick="Click_bck"  />
                            </div>
                           </div>
                               </div>
                           <div class="box-body">&nbsp;
                                    </div>

                        </div>

                    </div>
                </div>
            </ContentTemplate>
             <Triggers>
               <asp:PostBackTrigger ControlID="Button8"  />
               
           </Triggers>
    </asp:UpdatePanel>
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
   
</asp:Content>
