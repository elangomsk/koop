<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_LR_kehadiran.aspx.cs" Inherits="HR_LR_kehadiran" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

     
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1 id="h1_tag" runat="server">Rekod Kehadiran (Individu)</h1>
                        <ol class="breadcrumb">
                            <li id="bb1_text" runat="server"><a href="#"><i class="fa fa-dashboard"></i>Sumber Manusia</a></li>
                            <li class="active" id="bb2_text" runat="server">Rekod Kehadiran (Individu)</li>
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
                            <h3 class="box-title" id="h3_tag" runat="server">Maklumat Peribadi</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">

                           


                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl1_text" runat="server"> Tarikh Mula <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                                        <asp:TextBox ID="tm_date" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                    </div>
                                </div>
                            </div>
                           
                               
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl2_text" runat="server">Tarikh Sahingga <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                       <div class="input-group">                                                                    
                                                        <asp:TextBox ID="ta_date" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                    </div>
                                </div>
                            </div>
                                 
                                <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl3_text" runat="server"> No Kakitangan / Nama Kakitangan <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="Kaki_no" runat="server" class="form-control validate[optional] uppercase" MaxLength="1000"></asp:TextBox>
                                                        <asp:TextBox ID="Applcn_no1" runat="server" class="form-control validate[optional] uppercase"
                                                                                MaxLength="150" Visible="false"></asp:TextBox>
                                                                                  <asp:Panel ID="autocompleteDropDownPanel" runat="server" 
                                                                                    ScrollBars="Auto" Height="150px" Font-Size="Medium" 
                                                                                    HorizontalAlign="Left" Wrap="False" />
                                                                                 <cc1:AutoCompleteExtender ServiceMethod="GetCompletionList" MinimumPrefixLength="1"
                                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="Kaki_no"
                                                            ID="AutoCompleteExtender1" CompletionListElementID="autocompleteDropDownPanel" CompletionListCssClass="form-control uppercase" runat="server" FirstRowSelected="false">
                                                        </cc1:AutoCompleteExtender>
                                    </div>
                                </div>
                            </div>
                             <div class="col-md-6 box-body">
                            <div class="col-md-5 col-sm-1">
                                 
                                               <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Carian" UseSubmitBehavior="false" OnClick="click_srch" />
                                                        <asp:Button ID="Button3" runat="server" class="btn btn-default" Text="Set Semula" UseSubmitBehavior="false" OnClick="click_rst" />
                                                &nbsp;
                                            </div>
                            </div>
                                
                                   </div>
                                </div>

                                 <div id="shw_txtgrid1" runat="server" visible="false">
                                  <div class="box-header with-border"> 
                            <h3 class="box-title">Maklumat Kehadiran Kakitangan</h3>
                        </div>
                                       
                                  <div class="box-body">&nbsp;</div>
                                         
                                  <div class="row">
                             <div class="col-md-12">
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Nama  Kakitangan  </label>
                                    <div class="col-sm-8 text-right">
                                        <asp:Label ID="s_nama" runat="server" class="uppercase"></asp:Label>
                                    </div>
                                </div>
                            </div>
                                
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Nama Organisasi  </label>
                                    <div class="col-sm-8 text-right">
                                        <asp:Label ID="txt_org" runat="server" class="uppercase"></asp:Label>
                                    </div>
                                </div>
                            </div>
                                </div>
                                      </div>

                                  <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Perniagaan </label>
                                    <div class="col-sm-8 text-right">
                                        <asp:Label ID="TextBox2" runat="server" class="uppercase"></asp:Label>
                                    </div>
                                </div>
                            </div>
                                
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Jabatan  </label>
                                    <div class="col-sm-8 text-right">
                                        <asp:Label ID="s_jab" runat="server" class="uppercase" MaxLength="15"></asp:Label>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                      </div>
                                
                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Jawatan </label>
                                    <div class="col-sm-8 text-right">
                                        <asp:Label ID="s_jaw" runat="server" class="uppercase" MaxLength="15"></asp:Label>
                                    </div>
                                </div>
                            </div>
                               

                            
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Gred </label>
                                    <div class="col-sm-8 text-right">
                                        <asp:Label ID="s_gred" runat="server" class="uppercase"></asp:Label>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                                  <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Jumlah Hari Lewat </label>
                                    <div class="col-sm-8 text-right">
                                          <asp:Label ID="jm_hl" runat="server" class="uppercase"></asp:Label>
                                    </div>
                                </div>
                            </div>
                           
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Jumlah Hari Cuti </label>
                                    <div class="col-sm-8 text-right">
                                        <asp:Label ID="jm_hc" runat="server" class="uppercase"></asp:Label>
                                    </div>
                                </div>
                            </div>
                           
                                 </div>
                                </div>
               
                            <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                  <div class="body collapse in">
                                                <asp:Button ID="Btn_Senarai" runat="server" class="btn btn-danger" Text="Simpan" UseSubmitBehavior="false" onClick="Btn_Senarai_Click" />
                                                &nbsp;
                                            </div>
                            </div>
                           </div>
                               </div>                                 
                                </div>

                                        <div class="box-body">&nbsp;</div>
                                   <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
               <div class="col-md-2 box-body"> &nbsp; </div>
                <div class="col-md-8 box-body">
                                <rsweb:ReportViewer ID="RptviwerStudent" runat="server" width="100%" Height="100%" SizeToReportContent="True"></rsweb:ReportViewer>
                                                            <asp:Label runat="server" ID="ReportErrorMessage" Visible="false" CssClass="report-error-message"></asp:Label>
               </div>
                <div class="col-md-2 box-body"> &nbsp; </div>
               </div>
          </div>                                                        
 <div class="box-body">&nbsp;</div>
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
