<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../keanggotan/Daft.aspx.cs" Inherits="Daft" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server" ScriptMode="Release">
    </asp:ScriptManager>

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
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>  <asp:Label ID="ps_lbl1" runat="server"></asp:Label>  </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>  Keanggotaan </a></li>
                            <li class="active">  Pendaftaran Anggota Koperasi</li>
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
                            <h3 class="box-title"><asp:Label ID="ps_lbl2" runat="server"></asp:Label> </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl3" runat="server"></asp:Label>  <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="TXTNOKP" runat="server"  class="form-control validate[optional] uppercase" maxlength="12"></asp:TextBox>
                                                      <%--  <br />
                                                        <asp:Label ID="Label1" runat="server"  ForeColor="Red" Font-Size="Small"></asp:Label>--%>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                   <div class="col-sm-8">
                                         <asp:Button ID="Button1" runat="server" class="btn btn-primary" usesubmitbehavior="false"  onclick="Button1_Click" />
                                         <asp:Button ID="Button2" runat="server" class="btn btn-default" usesubmitbehavior="false"   onclick="Button2_Click" />
                                       </div>
                                    
                                </div>
                            </div>
                                  </div>
                                </div>

                                
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl7" runat="server"></asp:Label>  <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="Txtnama" runat="server"  class="form-control validate[optional,custom[textSp]]" MaxLength="150" style="text-transform:uppercase;"></asp:TextBox>                                                              
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2 control-label text-left" style="padding-right: 4px;"><asp:Label ID="ps_lbl8" runat="server"></asp:Label>  <span class="style1">*</span> </label>
                                    <div class="col-sm-10" style="padding-left: 25px;">
                                                    <label>
                                                        <asp:RadioButton ID="Rdbwar" runat="server" AutoPostBack="true" GroupName="war" />&nbsp&nbsp<asp:Label ID="ps_lbl9" runat="server"></asp:Label> 
                                                        </label>&nbsp&nbsp
                                                   
                                                    <label>
                                                        <asp:RadioButton ID="RdbBwar" runat="server" AutoPostBack="true" GroupName="war" />&nbsp&nbsp<asp:Label ID="ps_lbl10" runat="server"></asp:Label> 
                                                        </label>&nbsp&nbsp
                                                   
                                                    <label>
                                                        <asp:RadioButton ID="RdbPT" runat="server"   AutoPostBack="true" GroupName="war"  />&nbsp&nbsp<asp:Label ID="ps_lbl11" runat="server"></asp:Label> 
                                                        </label>
                                           
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>


                                  
                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl12" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="ddcat" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                        
                                                        </asp:DropDownList>

                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl13" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="Txtnokplama" runat="server"  class="form-control validate[optional] uppercase" maxlength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>

                              <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl14" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="ddJant" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                       
                                                        </asp:DropDownList>

                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl15" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                     <asp:DropDownList ID="ddBangsa" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>

                            <div class="row">
                             <div class="col-md-12">
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl17" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                     <div class="input-group">
                                                       <asp:TextBox ID="TxtTaradu" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" onkeyup="addTotal_bk(this)" Onmouseover="addTotal_mo1(this)" placeholder="DD/MM/YYYY"></asp:TextBox>
                                          <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                       </div>
                                    </div>
                                </div>
                            </div>
                                    <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl16" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="Txtage" runat="server"  class="form-control validate[optional,custom[number]] gt_dt" maxlength="3"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>

                              <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl18" runat="server"></asp:Label> <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="ddkaw" AutoPostBack="true" onselectedindexchanged="ddkaw_SelectedIndexChanged" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>

                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl19" runat="server"></asp:Label> : <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                     <asp:DropDownList ID="ddwil" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"  AutoPostBack="true" onselectedindexchanged="ddwil_SelectedIndexChanged" ></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  </div>
                         </div>

                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl20" runat="server"></asp:Label> <span class="style1">*</span>  </label>
                                    <div class="col-sm-8">
                                          <asp:DropDownList ID="ddcaw" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" ></asp:DropDownList>

                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl21" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                    <asp:TextBox ID="TxtPus" runat="server"  class="form-control validate[optional]" style="text-transform:uppercase;"></asp:TextBox>
                                </div>
                            </div>
                                  </div>
                         </div>
                                 </div>



                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl22" runat="server"></asp:Label>  </label>
                                    <div class="col-sm-8">
                                          <textarea id="txtAla" runat="server" class="form-control validate[optional]" rows="3" style="text-transform:uppercase;"></textarea>

                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl23" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                   <asp:TextBox ID="TextBox1" runat="server"  class="form-control validate[optional,custom[phone]]" maxlength="5"></asp:TextBox>
                                </div>
                            </div>
                                  </div>
                         </div>
                                 </div>



                                  <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl24" runat="server"></asp:Label>  </label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="ddlnegri" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>

                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl25" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                    <asp:TextBox ID="Txtnotel_R" runat="server"  class="form-control validate[optional,custom[phone]]" maxlength="15"></asp:TextBox>
                                </div>
                            </div>
                                  </div>
                         </div>
                                      </div>

                            <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl26" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="Txtnotel_P" runat="server"  class="form-control validate[optional,custom[phone]]" MaxLength="15"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl27" runat="server"></asp:Label>  </label>
                                    <div class="col-sm-8">
                                    <asp:TextBox ID="Txtnotel_B" runat="server"  class="form-control validate[optional,custom[phone]]" MaxLength="15"></asp:TextBox>
                                </div>
                            </div>
                                  </div>
                         </div>
                                      </div>

                            <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl28" runat="server"></asp:Label>  </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddlTxtPek" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>

                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl29" runat="server"></asp:Label>   </label>
                                    <div class="col-sm-8">
                                   <asp:TextBox ID="Txtemail" runat="server"  class="form-control validate[optional,custom[email]]" ></asp:TextBox>
                                </div>
                            </div>
                                  </div>
                         </div>
                                      </div>

                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl30" runat="server"></asp:Label>  </label>
                                    <div class="col-sm-8">
                                       <textarea id="TxtAlaPek" runat="server" class="form-control uppercase" rows="3"></textarea>

                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl31" runat="server"></asp:Label>   </label>
                                    <div class="col-sm-8">
                                  <asp:TextBox ID="txtpeke" runat="server"  class="form-control validate[optional,custom[phone]]" maxlength="5"></asp:TextBox>
                                </div>
                            </div>
                                  </div>
                         </div>
                                      </div>
                                 

                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl32" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddlpeke" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>

                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl33" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                 <asp:DropDownList ID="ddstt" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
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
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl34" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="Txtnobank" runat="server"  class="form-control validate[optional,custom[bank]]" MaxLength="20"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl35" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                <asp:DropDownList ID="ddbank" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>
                                </div>
                            </div>
                                  </div>
                         </div>
                                      </div>


                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl36" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                        <textarea id="Txtcat" runat="server" rows="3" class="form-control" readonly="readonly"></textarea>

                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl37" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                               <asp:TextBox ID="TextBox3" runat="server"  class="form-control validate[optional]" MaxLength="12"></asp:TextBox>
                                </div>
                            </div>
                                      <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl38" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                       <div class="input-group">
                                                     <asp:TextBox ID="TextBox4" runat="server"  class="form-control validate[optional] datepicker mydatepickerclass" AutoPostBack="true" OnTextChanged="TextBox1_TextChanged" placeholder="PICK DATE"></asp:TextBox>
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                       </div>

                                    </div>
                                </div>
                                  </div>
                         </div>
                                      </div>

                            <div class="box-header with-border">
                            <h3 class="box-title"><asp:Label ID="ps_lbl39" runat="server"></asp:Label></h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>

                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl40" runat="server"></asp:Label><span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="ddpay" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" onselectedindexchanged="ddpay_SelectedIndexChanged"  AutoPostBack="true"></asp:DropDownList>

                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-8">
                                         <asp:Button ID="Button6" runat="server" class="btn btn-primary" Text="Carian PST" onclick="Button6_Click" UseSubmitBehavior="false"  />

                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                            <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl42" runat="server"></asp:Label> <span class="style1">*</span>  </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txtamt1" runat="server"  class="form-control"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>

                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl43" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="Txtamt2" runat="server"  class="form-control validate[optional,custom[number]]"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl44" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox2" runat="server"  class="form-control  uppercase" ></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                             <div class="box-body">&nbsp;</div>
                            <div id="Div1" class="nav-tabs-custom col-md-12 box-body" role="tabpanel" runat="server">
                                  <ul class="s1 nav nav-tabs" role="tablist">
                                                                <li id="pp1" runat="server" class="active"><a href="#ContentPlaceHolder1_p1" aria-controls="p1" role="tab" data-toggle="tab"><strong><asp:Label ID="ps_lbl45" runat="server"></asp:Label></strong></a></li>
                                                                <li id="pp2" runat="server"><a href="#ContentPlaceHolder1_p2" aria-controls="p2" role="tab" data-toggle="tab"><strong><asp:Label ID="ps_lbl46" runat="server"></asp:Label></strong></a></li>
                                                                                                                               
                                                            </ul>
                                  <div class="box-body">&nbsp;</div>
                                     <div class="tab-content">
                                                             <div role="tabpanel" class="tab-pane active" runat="server" id="p1">
                                                                 <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl47" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TxtNoKPP1" runat="server"  class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase" maxlength="12"></asp:TextBox>
                                                      <%--  <br />
                                                        <asp:Label ID="Label2" runat="server" Text="(Makluman : Sila Masukkan Huruf dan Nombor Sahaja)" ForeColor="Red" Font-Size="Small"></asp:Label>--%>

                                    </div>
                                </div>
                            </div>

                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl49" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TxtnamaP1" runat="server"  class="form-control validate[optional]" MaxLength="150" style="text-transform:uppercase;"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>


                              <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl50" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="ddhun" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>

                                    </div>
                                </div>
                            </div>

                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl51" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="Txtnotelp1" runat="server"  class="form-control validate[optional,custom[phone]]" maxlength="15"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>

                            <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl52" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                       <textarea id="TxtAlamatp1" runat="server" class="form-control validate[optional]" rows="3" style="text-transform:uppercase;"></textarea>

                                    </div>
                                </div>
                            </div>

                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl53" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txtpost1" runat="server"  class="form-control validate[optional,custom[phone]]" maxlength="5"></asp:TextBox>

                                    </div>
                                </div>
                                       <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl54" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                      <asp:DropDownList ID="ddlnegri1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>

                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>

                                                                 </div>
                                         
                            <div role="tabpanel" class="tab-pane" runat="server" id="p2">
                                 <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl55" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TxtNoKPP2" runat="server" maxlength="12" class="form-control validate[optional] uppercase"></asp:TextBox>
                                                      <%--  <br />
                                                        <asp:Label ID="Label3" runat="server" Text="(Makluman : Sila Masukkan Huruf dan Nombor Sahaja)" ForeColor="Red" Font-Size="Small"></asp:Label>--%>

                                    </div>
                                </div>
                            </div>

                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl57" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TxtnamaP2" runat="server"  class="form-control validate[optional]" MaxLength="150" style="text-transform:uppercase;"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>

                            <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl58" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                      <asp:DropDownList ID="ddhun1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>

                                    </div>
                                </div>
                            </div>

                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl59" runat="server"></asp:Label>  </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="Txttelnop2" runat="server"  class="form-control validate[optional,custom[phone]]" maxlength="15"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>

                             <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl60" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                     <textarea id="TxtAlamatp2" runat="server" rows="3" class="form-control validate[optional]" style="text-transform:uppercase;"></textarea>

                                    </div>
                                </div>
                            </div>

                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl61" runat="server"></asp:Label>  </label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="txtpost2" runat="server"  class="form-control validate[optional,custom[phone]]" maxlength="5"></asp:TextBox>

                                    </div>
                                </div>
                                       <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl62" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                      <asp:DropDownList ID="ddlnegri2" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>

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
                                 <asp:Button ID="Button7" runat="server" class="btn btn-danger"   
                                                              Text="Simpan"   Type="submit"  onclick="Button3_Click" ValidationGroup="Group1" />
                                                              <asp:Button ID="Button3" runat="server" class="btn btn-danger" Visible="false" Text="Kemaskini" UseSubmitBehavior="false" OnClick="update"/>
                                                        <asp:Button ID="Button4" runat="server"   Text="Set Semula"  
                                                            class="btn btn-default" usesubmitbehavior="false" onclick="Button4_Click"/>
                                                        <asp:Button ID="Button5" runat="server" Text="Batal" Visible="false" OnClick="Click_bck" class="btn btn-default" />
                                 
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



