<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="MAK_POT_MAJI.aspx.cs" Inherits="PMajikan_MAK_POT_MAJI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script runat="server">
protected void UploadFile(object src, EventArgs e)
{
    if (ImageFile.HasFile)
    {
        string strFileName;
        strFileName = ImageFile.FileName;
        ImageFile.PostedFile.SaveAs(Server.MapPath(".") + "//" + strFileName);
    }
}
</script>
 
<script language="javascript" type="text/javascript">
function showWait()
{
    if ($get('ImageFile').value.length > 0)
    {
        $get('UpdateProgress1').style.display = 'block';
    }
}
</script>

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
   </asp:ScriptManager>
    <div class="content-wrapper" style="height: 895px;">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1> MUATNAIK DATA POTONGAN MAJIKAN </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>  Potongan Majikan </a></li>
                            <li class="active">  MUATNAIK DATA POTONGAN MAJIKAN </li>
                        </ol>
                    </section>

                    <!-- Main content -->
                 <div class="row">&nbsp;</div>
                <div class="col-md-13 col-sm-13">
                            
                            <div class="dashboard-block">
                                <div class="tabs profile-tabs">
                                    <div>
                                        <div class="tab-content">
                                            <!-- PROFIE PERSONAL INFO -->
                                            <div id="personalinfo" class="tab-pane fade active in">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                       <%--<div class="chat-panel panel panel-primary">
                                                       <div class="panel-heading">
                                                       
                                                                <h5><Strong>MUATNAIK DATA SIMPANAN TETAP</Strong></h5>
                                                       </div>
                                                       </div>--%>
                                                       
                                                        <br />
                                                        
                                                        <div class="row">
                                                        <div class="col-md-6 col-sm-6">
                                                            <div class="col-md-5 col-sm-1">
                                                            <label>Nama Fail <span class="style1">*</span> :  </label>
                                                            </div>
                                                            <div class="col-md-6 col-sm-2">

                                                                
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="Button2" /> 
        </Triggers>
        <ContentTemplate>
            <asp:FileUpload ID="ImageFile" runat="server" />
           <asp:Label ID="Label1" runat="server" Text="Makluman : Sila pilih fail format Excel (.xls) sahaja." ForeColor="Red" Font-Size="X-Small"></asp:Label>            
            <br />
                 
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <asp:Label ID="lblWait" runat="server" BackColor="#507CD1" 
                        Font-Bold="True" ForeColor="White" 
                        Text="Please wait ... Uploading file"></asp:Label>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
                                                                <br />
                                                              
                                                                </div>
                                                            
                                                        </div>

                                    <div class="col-md-4 col-sm-2">
                                                                <asp:Button ID="Button2" runat="server" class="btn btn-danger" Text="Muatnaik" onclick="Button2_Click" OnClientClick="javascript:showWait();"/>                                                            
                                                        </div>
                                                        </div>
                                                        <br />
     
	
                                         
                                                        <br />
                                                           <div class="row" style=" text-align:center; padding-top:40px;">
                                                    <div class="col-md-12 col-sm-4">
                                            <div class="body collapse in">
                                            <asp:GridView ID="GridView1" CellPadding="8" CellSpacing="2" Width="100%" Height="100%" runat="server" AutoGenerateColumns="false" EmptyDataText = "No files uploaded">
        <Columns>
            <asp:BoundField DataField="Text" HeaderText="NAMA FAIL" />
            <asp:TemplateField HeaderText="TINDAKAN">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDownload" Text = "Download" CommandArgument = '<%# Eval("Value") %>' runat="server" OnClick = "DownloadFile"></asp:LinkButton>&nbsp;|&nbsp; 
                    <asp:LinkButton ID = "lnkDelete" Text = "Delete" CommandArgument = '<%# Eval("Value") %>' runat = "server" OnClick = "DeleteFile" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
                                            </div>
                                            </div>
                                            </div>
                         
                                                  
                                                </div>
                                                </div>
                                         </div>
                                        </div>
                                        
                                </div>
                            </div>
                        </div>
                
                
                </div>
                                            
                                                    </div>
                    <!-- /.content -->
                </div>
</asp:Content>

