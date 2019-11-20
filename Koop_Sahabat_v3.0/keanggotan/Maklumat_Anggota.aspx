<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../keanggotan/Maklumat_Anggota.aspx.cs" Inherits="Maklumat_Anggota" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server" ScriptMode="Release">
    </asp:ScriptManager>

    <%-- <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>--%>
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1> <asp:Label ID="ps_lbl1" runat="server"></asp:Label>   </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Keanggotaan</a></li>
                            <li class="active">  Dashboard Anggota   </li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
      
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl3" runat="server"></asp:Label><span class="style1">*</span>   </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox1" runat="server" MaxLength="12"  class="form-control validate[optional] uppercase"></asp:TextBox> 
                                           <asp:Panel ID="autocompleteDropDownPanel" runat="server" ScrollBars="Auto" Height="150px"
                                                            Font-Size="Medium" HorizontalAlign="Left" Wrap="False" />
                                                        <cc1:AutoCompleteExtender ServiceMethod="GetCompletionList" MinimumPrefixLength="1"
                                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="TextBox1"
                                                            ID="AutoCompleteExtender1" CompletionListElementID="autocompleteDropDownPanel"
                                                            CompletionListCssClass="form-control uppercase" runat="server" FirstRowSelected="false">
                                                        </cc1:AutoCompleteExtender>
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="You can't leave this Empty" CssClass="requiredFieldValidateStyle" ForeColor="Red" ControlToValidate="TextBox1" ValidationGroup="vgSubmit_dash_srch">
                                    </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-8">
                                         <asp:Button ID="Button2" runat="server" class="btn btn-primary" usesubmitbehavior="false" OnClick="Searchbtn_Click" ValidationGroup="vgSubmit_dash_srch"/>
                                         <asp:Button ID="Button4" runat="server" class="btn btn-default"  usesubmitbehavior="false" OnClick="Reset_btn"/>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                   </div>
                           
                            <div id="lev2" runat="server">
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   <asp:Label ID="ps_lbl6" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox2" runat="server"  class="form-control validate[optional] uppercase "></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl7" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox14" runat="server"  class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                     </div>


                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl8" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox7" runat="server"  class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase" MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl9" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="ddbangsa" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                     </div>


                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl10" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox27" runat="server"  class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase" MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl11" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="Txtsa" runat="server"  class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                     </div>

                            

                              <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>


                                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl12" runat="server"></asp:Label>  </label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="TextBox18" runat="server" style="width:100%; font-size:13px;"  class="form-control select2 uppercase" AutoPostBack="true" onselectedindexchanged="ddkaw_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl13" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="TextBox8" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" AutoPostBack="true" onselectedindexchanged="ddwil_SelectedIndexChanged" 
                                                            ></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                     </div>

                                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl14" runat="server"></asp:Label>   </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="TextBox9" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl15" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="TextBox19" runat="server"  class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>
                                 </div>
                                     </div>

                                             </ContentTemplate>
                                    </asp:UpdatePanel>


                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl16" runat="server"></asp:Label>  </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox5" runat="server"  class="form-control validate[optional,custom[phone]]" MaxLength="15"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl17" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="TextBox20" runat="server"  class="form-control validate[optional,custom[phone]]" MaxLength="15"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>
                                 </div>
                                     </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl18" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox22" runat="server"  class="form-control validate[optional,custom[phone]]" MaxLength="15"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl19" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                           <textarea id="TextArea1" runat="server"  rows="3" class="form-control validate[optional,custom[onlyaddress]] uppercase"></textarea>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                     </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl20" runat="server"></asp:Label>  </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox13" runat="server"  class="form-control validate[optional,custom[phone]]" maxlength="5"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl21" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                          <asp:DropDownList ID="ddlnegri" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                     </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl22" runat="server"></asp:Label>  </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox3" runat="server"  class="form-control validate[optional,custom[bank]]" MaxLength="20"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl23" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="Bank_details" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl24" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                       <div class="input-group">
                                                                    
                                                        <asp:TextBox ID="TextBox4" runat="server" AutoComplete="off"  class="form-control datepicker mydatepickerclass"></asp:TextBox> 
                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                       </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl25" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                                                   
                                                       <asp:TextBox ID="TextBox6" runat="server" AutoComplete="off" class="form-control datepicker mydatepickerclass"></asp:TextBox> 
                                             <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                       </div>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                     </div>
                               
                       <%--    <div class="box-body">&nbsp;</div>
                                  <div id="Div1" class="nav-tabs-custom col-md-12 box-body" role="tabpanel" runat="server">
                                  <ul class="s1 nav nav-tabs" role="tablist">
                                                                <li id="pp1" runat="server" class="active"><a href="#ContentPlaceHolder1_p1" aria-controls="p1" role="tab" data-toggle="tab"><strong>Maklumat Penama 1</strong></a></li>
                                                                <li id="pp2" runat="server"><a href="#ContentPlaceHolder1_p2" aria-controls="p2" role="tab" data-toggle="tab"><strong>Maklumat Penama 2</strong></a></li>
                                                                                                                               
                                                            </ul>
                                  <div class="box-body">&nbsp;</div>
                                     <div class="tab-content">
                                                             <div role="tabpanel" class="tab-pane active" runat="server" id="p1" >--%>
                                   <div class="box-header with-border" >
                            <h3 class="box-title"> <asp:Label ID="ps_lbl26" runat="server"></asp:Label> </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl27" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox11" runat="server" MaxLength="12"  class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase"></asp:TextBox>  
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl28" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="TextBox10" runat="server"  class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>
                                 </div>
                                     </div>


                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl29" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="DropDownList2" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                </asp:DropDownList> 
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl30" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox12" runat="server"  class="form-control validate[optional,custom[phone]]" MaxLength="15"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>
                                 </div>
                                     </div>

                              <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl31" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                        <textarea id="TextArea2" runat="server"  rows="3" class="form-control validate[optional,custom[onlyaddress]] uppercase"></textarea>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl32" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox21" runat="server"  class="form-control validate[optional,custom[phone]]" maxlength="5"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl33" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddlnegri1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                     </div>

                           
                                                                 <%--</div>
                                           <div role="tabpanel" class="tab-pane" runat="server" id="p2" >--%>
                           <div class="box-header with-border">
                            <h3 class="box-title"> <asp:Label ID="ps_lbl34" runat="server"></asp:Label> </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl35" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox15" runat="server" MaxLength="12"  class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl36" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="TextBox17" runat="server"  class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>
                                 </div>
                                     </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl37" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="DropDownList1" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl38" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="TextBox23" runat="server"  class="form-control validate[optional,custom[phone]]" MaxLength="15"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>
                                 </div>
                                     </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl39" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                        <textarea id="TextArea3" runat="server" rows="3" class="form-control validate[optional,custom[onlyaddress]] uppercase"></textarea>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl40" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="TextBox26" runat="server"  class="form-control validate[optional,custom[phone]]" maxlength="5"></asp:TextBox> 
                                    </div>
                                </div>
                                  <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl41" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddlnegri2" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" ></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                     </div>
                                               </div>
                                        <%-- </div>
                                      </div>--%>
                            <div id="style" runat="server">  
                             <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Caruman Anggota  </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                             <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
              <asp:GridView ID="gvSelected" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="30" ShowFooter="false" GridLines="None"  OnPageIndexChanging="gvSelected_PageIndexChanging">
                                        <Columns>
                                                <asp:TemplateField HeaderText="FI MASUK TUNAI (RM)"> 
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("ftunai","{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FI MASUK PST (RM)"> 
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>  
                                            <ItemTemplate>  
                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("fpst","{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MODAL SYER TUNAI (RM)">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("STUNAI","{0:n}")%>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MODAL SYER PST (RM)"> 
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>  
                                            <ItemTemplate>  
                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("SPST","{0:n}")%>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JUMLAH KESELURUHAN MODAL SYER (RM)"> 
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>  
                                            <ItemTemplate>  
                                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("jumlah","{0:n}") %>'></asp:Label>  
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
                    <div class="box-body">&nbsp;</div>
                                 <div class="col-md-12 box-body">
                                       <div class="text-left"><strong> Baki Simpanan Tetap</strong></div>
                                     </div>
                <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Tarikh Sehingga</label>
                                    <div class="col-sm-8">
                                             <asp:TextBox ID="TextBox25" runat="server" class="form-control"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>

                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Jumlah Amaun (RM) </label>
                                    <div class="col-sm-8">
                                             <asp:TextBox ID="TextBox24" runat="server"  class="form-control"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>
                                  </div>
                                </div>
                                       
                                       <div class="box-body">&nbsp;</div>
                                       <div class="text-center">  
                                            
                                           <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
               <div class="text-left" style="padding-bottom:7px;"><strong>Transaksi Kredit / Debit Modal Syer</strong></div>
                                            <asp:GridView ID="GridView2" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="30" ShowFooter="true" GridLines="None"  OnPageIndexChanging="gvSelected_PageIndexChanging"  onrowdatabound="gvEmp_RowDataBound">
                                        <Columns>
                                                <asp:TemplateField HeaderText="TARIKH">  
                                            <ItemTemplate>  
                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("sha_approve_Dt", "{0:dd/MM/yyyy}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PERKARA">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label3" runat="server"  Text='<%# Bind("sha_item") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="RUJUKAN">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("sha_reference_ind") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DEBIT (RM)">   
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("sha_debit_amt","{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="KREDIT (RM)">  
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle> 
                                            <ItemTemplate>  
                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("sha_credit_amt","{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JUMLAH (RM)"> 
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>  
                                            <ItemTemplate>  
                                                <asp:Label ID="Jumla" runat="server" Text='<%# Bind("Jumla","{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                            <FooterTemplate>
                                                <asp:Label ID="lblTotal" runat="server"/>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
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
                                       </div>

                                       <div class="box-body">&nbsp;</div>
                                       <div class="text-center">  
                                            
                                           <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
               <div class="text-left" style="padding-bottom:7px;"><strong>Maklumat Penyelesaian Anggota</strong></div>
                                            <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="30" ShowFooter="false" GridLines="None"  OnPageIndexChanging="gvSelected_PageIndexChanging" DataKeyNames="Id">
                                        <Columns>
                                                <asp:TemplateField HeaderText="TARIKH MOHON">  
                                            <ItemTemplate>  
                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("set_txn_dt", "{0:dd/MM/yyyy}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JENIS PERMOHONAN">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("Application_name") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SEBAB">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("DESCRRIPTION") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TARIKH LULUS">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("set_appprove_dt", "{0:dd/MM/yyyy}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TARIKH BAYARAN">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("set_pay_dt", "{0:dd/MM/yyyy}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NO AKAUN BANK">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("set_bank_acc_no") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="KOD BANK">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("set_bank_cd") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="KAEDAH PEMBAYARAN">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("Payment_Type") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="AMAUN (RM)">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("set_apply_amt","{0:n}") %>'></asp:Label>  
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
                                       </div>

                                      <div class="box-body">&nbsp;</div>
                                       <div class="text-center">  
                                           
                                           <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                <div class="text-left" style="padding-bottom:7px;"><strong>Maklumat Dividen Anggota</strong></div>
                                            <asp:GridView ID="GridView3" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="30" ShowFooter="false" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging" DataKeyNames="Id">
                                        <Columns>
                                                <asp:TemplateField HeaderText="TARIKH PEMBAYARAN">  
                                            <ItemTemplate>  
                                                <asp:Label ID="Label2" runat="server" Text=' <%# Eval("div_pay_dt", "{0:dd/MM/yyyy}") == "01/01/1900" ? "" : Eval("div_pay_dt", "{0:dd/MM/yyyy}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PERKARA">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("div_remark") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="RUJUKAN">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("Bank_Name") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NO RUJUKAN">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("div_bank_acc_no") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DEBIT (RM)">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("div_debit_amt","{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JUMLAH (RM)">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle> 
                                            <ItemTemplate>  
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("div_debit_amt","{0:n}") %>'></asp:Label>  
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
                                       </div>
               </div>
          </div>
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                                            <asp:Button ID="Button3" runat="server" class="btn btn-danger"  UseSubmitBehavior="false" OnClick="update"/>
                                                            <asp:Button ID="Button5" runat="server" class="btn btn-default" Visible="false" UseSubmitBehavior="false"  Text="Batal" />
                                                            <asp:Button ID="Button1" runat="server" class="btn btn-warning" UseSubmitBehavior="false" OnClientClick="document.forms[0].target ='_blank';"   onclick="Button4_Click" />
                            </div>
                           </div>
                               </div>
                        <div class="box-body">&nbsp;</div>
                          </div>
                                </div>
                            <div class="row">
                                   <div class="col-md-12 col-sm-2" style="text-align:center">
                                     <rsweb:ReportViewer ID="RptviwerStudent" runat="server"></rsweb:ReportViewer>
                                     <asp:Label runat="server" ID="ReportErrorMessage" Visible="false" CssClass="report-error-message"></asp:Label>
                                    </div>
                                    </div>
                        </div>

                    
        
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
   <%-- </ContentTemplate>
            
    </asp:UpdatePanel>--%>
</asp:Content>





