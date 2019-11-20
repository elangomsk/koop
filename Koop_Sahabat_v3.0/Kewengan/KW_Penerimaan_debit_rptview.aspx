<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KW_Penerimaan_debit_rptview.aspx.cs" Inherits="KW_Penerimaan_debit_rptview" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <style type="text/css">
     .loader 
     {
         display:none;
	position: fixed;
	left: 0px;
	top: 0px;
	width: 100%;
	height: 100%;
	z-index: 9999;
	background: url('images/loader.gif') 50% 50% no-repeat rgb(249,249,249);
}

</style>

<script type="text/javascript">
    $(window).load(function () {
        $(".loader").fadeOut("slow");
    });
</script>
<style>
.loader 
{
 position: fixed;
 left: 0px;
 top: 0px;
 width: 100%;
 height: 100%;
 z-index: 9999;
 background: url('loader.gif') 50% 50% no-repeat rgb(249,249,249);
}
</style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
      <div class="loader">
   

    <div id="container">


     <rsweb:ReportViewer ID="rptdebit" runat="server" Height="400px">
    </rsweb:ReportViewer>
   
    </div>

  </div>
    <asp:TextBox ID="txtError" runat="server" BorderStyle="None" ForeColor="#CC0000" Width="910px" TextMode="MultiLine" BackColor="White" Enabled="False"></asp:TextBox>
    </div>
    </form>
</body>
</html>
