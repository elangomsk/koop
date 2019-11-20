<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_kem_mak_Latihan.aspx.cs" Inherits="HR_kem_mak_Latihan" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

    <%-- <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>--%>
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1 id="h1_tag" runat="server">Import Rekod Kehaderan</h1>
                        <ol class="breadcrumb">
                            <li id="bb1_text" runat="server"><a href="#"><i class="fa fa-dashboard"></i>Sumber Manusia</a></li>
                            <li class="active" id="bb2_text" runat="server">Import Rekod Kehaderan</li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
      
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title"></h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl1_text" runat="server"> Nama Fail <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                          <div class="col-md-8 col-sm-2">
                                                                <asp:FileUpload ID="FileUpload1"  runat="server"/> 
                                                                <br />
                                                                <asp:Label ID="Label1" runat="server" Text="Makluman : Sila pilih fail format Excel (.xls) sahaja." ForeColor="Red" Font-Size="X-Small"></asp:Label> 
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

                                <asp:Button ID="Button2" runat="server" class="btn btn-danger" Text="Muatnaik" onclick="Button2_Click"/> 
                                  <asp:Button ID="Button5" runat="server" class="btn btn-default" Text="Kembali" OnClick="Click_bck" UseSubmitBehavior="false"  />
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

        
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
   <%-- </ContentTemplate>
         <Triggers>
<asp:PostBackTrigger ControlID="Button2" />
             
</Triggers>
             
    </asp:UpdatePanel>--%>
</asp:Content>
