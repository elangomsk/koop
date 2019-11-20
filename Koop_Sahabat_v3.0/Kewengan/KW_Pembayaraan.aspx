<%@ Page Title="" Language="C#" MasterPageFile="~/DashMaster.master" AutoEventWireup="true" CodeFile="KW_Pembayaraan.aspx.cs" Inherits="KW_Pembayaraan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   <style type="text/css">
       
       
       fieldset 
	{
		border: 1px solid #ddd !important;
		margin: 0;
		xmin-width: 0;
		padding: 10px;       
		position: relative;
		border-radius:4px;
		
		padding-left:10px!important;
	}	
	
		legend
		{
			font-size:14px;
			font-weight:bold;
			margin-bottom: 0px; 
		width: 12%; 
			border: 0px solid #ddd;
			border-radius: 4px; 
			padding: 5px 5px 5px 10px; 
			background-color: #ffffff;
		}

        legend1
		{
			font-size:14px;
			font-weight:bold;
			margin-bottom: 0px; 
			width: 9%; 
			border: 0px solid #ddd;
			border-radius: 4px; 
			padding: 5px 5px 5px 10px; 
			background-color: #ffffff;
		}
		legend11
		{
			font-size:14px;
			font-weight:bold;
			margin-bottom: 0px; 
			width: 11%; 
			border: 0px solid #ddd;
			border-radius: 4px; 
			padding: 5px 5px 5px 10px; 
			background-color: #ffffff;
		}
    </style>


<style type="text/css">
    .temp > span {
  background-color: white;
  font-family:Calibri;
  color:#19336a;
  font-weight:bold;
  font-size:14px;
  left: 2%;
  position: absolute;
  top: -10px;
}
.temp {
  border: 0.5px solid  #19336a;
  margin-top: 0px;
  padding: 20px;
  position: relative;
}
</style>
      <script type="text/javascript">

         $().ready(function () {

             $("#<%=pp6.ClientID %>").click(function () {
                 $("#<%=hd_txt.ClientID %>").text("MOHON BAYAR");
             });
             $("#<%=pp4.ClientID %>").click(function () {
                 $("#<%=hd_txt.ClientID %>").text("INVOIS / BIL");
             });
             $("#<%=pp1.ClientID %>").click(function () {
                 $("#<%=hd_txt.ClientID %>").text("PAYMENT VOUCHER");
             });
             $("#<%=pp2.ClientID %>").click(function () {
                 $("#<%=hd_txt.ClientID %>").text("NOTA KREDIT");
             });
             $("#<%=pp3.ClientID %>").click(function () {
                 $("#<%=hd_txt.ClientID %>").text("NOTA DEBIT");
             });
         });

     </script>
      <script type="text/javascript">

          function RadioCheck(rb) {

              var gv = document.getElementById("<%=grdbilinv1.ClientID%>");

              var rbs = gv.getElementsByTagName("input");

              var row = rb.parentNode.parentNode;

              for (var i = 0; i < rbs.length; i++) {

                  if (rbs[i].type == "radio") {

                      if (rbs[i].checked && rbs[i] != rb) {

                          rbs[i].checked = false;

                          break;

                      }

                  }

              }

          }    

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0;
                right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                <span style="border-width: 0px; position: fixed; font-weight: bold; padding: 50px;
                    background-color: #FFFFFF; font-size: 16px; left: 40%; top: 40%;">Sila Tunggu. Rekod
                    Sedang Diproses ...</span>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:ScriptManager ID="ScriptManagerCalendar" ScriptMode="Release" runat="server">
    </asp:ScriptManager>
    <div id="content" style="font-family: Calibri;">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div class="inner" style="min-height: 1200px;">
                    <div class="row head_bgcolor">
                        <div class="col-lg-12">
                            <h3>
                                <strong>PEMBAYARAAN</strong>
                            </h3>
                        </div>
                    </div>
                    <div class="row">
                        &nbsp;</div>
                    <div class="col-md-13 col-sm-13">
                        <div class="dashboard-block">
                            <div class="tabs profile-tabs">
                                <div>
                                    <div class="tab-content">
                                        <!-- PROFIE PERSONAL INFO -->
                                        <div id="personalinfo" class="tab-pane fade active in">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="chat-panel panel panel-primary">
                                                        <div class="panel-heading">
                                                            <h5>
                                                                <center>
                                                                    <strong>BUTIRAN <asp:Label ID="hd_txt" runat="server"></asp:Label></strong></center>
                                                            </h5>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="col-md-5 col-sm-1">
                                                                <label>
                                                                    Untuk akaun :
                                                                </label>
                                                            </div>
                                                            <div class="col-md-7 col-sm-4">
                                                                <asp:DropDownList ID="ddakaun" runat="server" class="form-control uppercase">
                                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6 col-sm-2">
                                                            <div class="col-md-5 col-sm-1">
                                                               <%-- <asp:Button ID="Button1" runat="server" class="btn btn-danger" Text="Carian" UseSubmitBehavior="false"
                                                                 />--%>
                                                               
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />
                                                   
                                                    <hr />
                                                    <%--  <div class="row">
                                                <div class="col-md-12 col-sm-2" style="text-align: center">
                                                    <div class="body collapse in">
                                                        <asp:Button ID="Button6" runat="server" class="btn btn-danger" Text="Simpan" Type="submit" OnClick="stf_update"/>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />--%>
                                                    <div class="panel" style="width: 100%;">
                                                        <div id="Div1" role="tabpanel" runat="server">
                                                            <!-- Nav tabs -->
                                                            <ul class="s1 nav nav-tabs" role="tablist">
                                                            <li id="pp6" runat="server" class="active"><a href="#ContentPlaceHolder1_p6" aria-controls="p6" role="tab" data-toggle="tab">Mohon Bayar</a>
                                                               
                                                            </li>
                                                                <li id="pp4" runat="server"><a href="#ContentPlaceHolder1_p4" aria-controls="p4" role="tab" data-toggle="tab">Invoice / Bil</a></li>
                                                                <li id="pp1" runat="server"><a href="#ContentPlaceHolder1_p1" aria-controls="p1" role="tab" data-toggle="tab">Payment Vocher</a></li>
                                                                <li id="pp2" runat="server"><a href="#ContentPlaceHolder1_p2" aria-controls="p2" role="tab" data-toggle="tab">Nota Kredit</a></li>
                                                                <li id="pp3" runat="server"><a href="#ContentPlaceHolder1_p3" aria-controls="p3" role="tab" data-toggle="tab">Nota Debit</a></li>
                                                               
                                                                
                                                            </ul>
                                                            <!-- Tab panes -->
                                                            <div class="tab-content" style="padding-top: 20px">
                                                          
                                                            
                                                            <div role="tabpanel" class="tab-pane active" runat="server" id="p6">
                                                                    
                                                                     <div id="Div3"  runat="server">
                                                                            
                                                                    <fieldset class="col-md-12">
                                                                     <legend>Filter</legend>
                                                                     <br />
                                                             <div class="row">
                                                                       
                                                                         <div class="col-md-3 col-sm-2">
                                                                            <div class="col-md-6 col-sm-1">
                                                                                <label>
                                                                                     No Permohonan Bayar 
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-6 col-sm-2">
                                                                               
                                                                                    <asp:TextBox ID="txtnoinvois" runat="server" class="form-control"></asp:TextBox> 
                                                                            </div>
                                                                        </div>
                                                                          <div class="col-md-3 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                  Tarikh Mohon  
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               <div class="input-group">
                                                                                    <span class="input-group-addon"><i class="icon-calendar"></i></span>
                                                                                    <asp:TextBox ID="txttarikhinvois" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox> 
                                                                                    </div>
                                                                               
                                                                            </div>
                                                                              
                                                                            </div>
                                                                       
                                                                         <div class="col-md-3 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                    Jenis Permohononan  
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               
                                                                                    <asp:TextBox ID="TextBox13" runat="server" class="form-control"></asp:TextBox> 
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-3 col-sm-2">
                                                                            <div class="col-md-7 col-sm-1">
                                                                               <asp:CheckBox ID="mb_chk" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                 <asp:Button ID="but" runat="server" class="btn btn-danger" Text="Carian" 
                                                                    UseSubmitBehavior="false" onclick="but_Click"    />
                                                                                <asp:Button ID="Button3" runat="server" class="btn btn-danger" Text="+Tambah" 
                                                                    UseSubmitBehavior="false" onclick="Button3_Click"
                                                                 />
                                                                            </div>
                                                                          
                                                                        </div>
                                                                    </div>
                                                                  
                                                                    </fieldset>
                                                                    <br />
                                                                    <br />
                                                                  
                                                                        <asp:gridview ID="Gridview2" runat="server"  Font-Size="12px" CssClass="table datatable dataTable no-footer uppercase" ShowFooter="false"  AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" onrowdatabound="GridView1_RowDataBound1" AllowPaging="true" PageSize="25" OnPageIndexChanging="gvSelected_PageIndexChanging_g2">
            <Columns>
             <asp:TemplateField HeaderText="BIL">  
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="No Permohonan" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                  <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("no_permohonan") %>'  CommandArgument='<%# Eval("no_permohonan")%>' CommandName="Add"  onclick="lblSubbind_Click" Font-Bold Font-Underline  ></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        <asp:BoundField DataField="tarkih_permohonan" HeaderText="Tarkih Permohonan" ItemStyle-HorizontalAlign="Center" />
          <asp:BoundField DataField="upd_dt" HeaderText="Tarkih Kelulusan" ItemStyle-HorizontalAlign="Center" />
              <asp:BoundField DataField="Bayar_kepada" HeaderText="Bayar Kepada" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
      <%--      <asp:BoundField DataField="nama" HeaderText="Nama" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />--%>
                <%-- <asp:BoundField DataField="status" HeaderText="Status" ItemStyle-HorizontalAlign="Center" />--%>
                <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">  
                    <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>  
                                                <asp:Label ID="status" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                  <asp:BoundField DataField="jumlah" HeaderStyle-HorizontalAlign="Right" HeaderText="Jumlah (RM)" ItemStyle-HorizontalAlign="Right" />
             
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="White" />
        </asp:gridview>
                                                                  
                                                                </div>
                                                                <br />
                                                                <br />
                                                                   <div id="Div2" role="tabpanel1" runat="server">
                                                            <!-- Nav tabs -->
                                                            <ul class="s1 nav nav-tabs" role="tablist1">
                                                            <li id="Li1" runat="server" class="active"><a href="#ContentPlaceHolder1_p61" aria-controls="p61" role="tab" data-toggle="tab">Mohon Bayar</a>

                                                            </li>
                                                              
                                                                <%--<li id="pp2" runat="server"><a href="#ContentPlaceHolder1_p2" aria-controls="p2" role="tab" data-toggle="tab">ELAUN TETAP</a></li>
                                                                <li id="pp3" runat="server"><a href="#ContentPlaceHolder1_p3" aria-controls="p3" role="tab" data-toggle="tab">LAIN-LAIN ELAUN</a></li>
                                                                <li id="pp4" runat="server"><a href="#ContentPlaceHolder1_p4" aria-controls="p4" role="tab" data-toggle="tab">KERJA LEBIH MASA</a></li>
                                                                <li id="pp5" runat="server"><a href="#ContentPlaceHolder1_p5" aria-controls="p5" role="tab" data-toggle="tab">BONUS</a></li>--%>
                                                                
                                                            </ul>
                                                            <!-- Tab panes -->
                                                            <div class="tab-content" style="padding-top: 20px">
                                                          
                                                            
                                                            <div role="tabpanel1" class="tab-pane active" runat="server" id="p61">
                                                                    <div class="chat-panel panel panel-primary">
                                                                      
                                                                    </div>
                                                                 <br />
                                                                   <div class="row">
                                                                        <asp:gridview ID="grdbind" runat="server" Font-Size="12px"  CssClass="table datatable dataTable no-footer uppercase" ShowFooter="True"  AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Visible="false"  >
            <Columns>
              <asp:TemplateField HeaderText="BIL">  
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="No Invois" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                  <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("no_Invois") %>'  CommandArgument='<%# Eval("no_Invois")%>' CommandName="Add"  onclick="lblSubItemName_Click"  ></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
         <asp:BoundField DataField="tarkih_invois" HeaderText="Tarkih Invois" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="jumlah" HeaderText="Jumlah (RM)" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
               
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="White" />
        </asp:gridview>
                                                                   </div>
                                                                    <br />
                                                                     <div class="row">
                                                                          <div class="col-md-6 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                    ID Pemohon <span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               
                                                                                    <asp:TextBox ID="txtid" runat="server" class="form-control"></asp:TextBox> 
                                                                            </div>
                                                                        </div>
                                                                           <div class="col-md-6 col-sm-2" id="mb_tab1" runat="server" visible="false">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                    No Permohonan <span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               
                                                                                    <asp:TextBox ID="txtnoper" runat="server" class="form-control uppercase"></asp:TextBox> 
                                                                                <asp:TextBox ID="txtnoper_1" runat="server" Visible="false" class="form-control uppercase"></asp:TextBox> 
                                                                                <asp:TextBox ID="TextBox1_2" runat="server" Visible="false" class="form-control uppercase"></asp:TextBox> 
                                                                            </div>
                                                                        </div>
                                                                          </div>
                                                                          <br />
                                                                          <br />
                                                                           <div class="row">
                                                                          <div class="col-md-6 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                  Tarikh Permohonan <span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               <div class="input-group">
                                                                                    <span class="input-group-addon"><i class="icon-calendar"></i></span>
                                                                                    <asp:TextBox ID="txttarkihmo" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox> 
                                                                                    </div>
                                                                               
                                                                            </div>
                                                                        </div>
                                                                               <div class="col-md-6 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                    Bayar Kepada <span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                                 <asp:DropDownList ID="ddbkepada" runat="server" class="form-control" OnSelectedIndexChanged="ddbkepada_SelectedIndexChanged" AutoPostBack="true">
                                                                                     <asp:ListItem>--- PILIH ---</asp:ListItem>
                                                                                     <asp:ListItem>KAKITANGAN</asp:ListItem>
                                                                                     <asp:ListItem>PEMBEKAL</asp:ListItem>
                                                                                     <asp:ListItem>KWSP</asp:ListItem>
                                                                                     <asp:ListItem>PERKESO</asp:ListItem>
                                                                                     <asp:ListItem>LHDN (PCB)</asp:ListItem>
                                                                                     <asp:ListItem>LHDN (CP 38)</asp:ListItem>
                                                                                     <asp:ListItem>GAJI KAKITANGAN</asp:ListItem>
                                                                                     <asp:ListItem>ANGKASA</asp:ListItem>
                                                                                     <asp:ListItem>KOOP / Bank</asp:ListItem>
                                                                                     <asp:ListItem>TABUNG HAJI</asp:ListItem>
                                                                                     <asp:ListItem>PEMBEKAL (PENGURUSAN ASET)</asp:ListItem>
                                                                                      <asp:ListItem>Keahlian</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                            </div>
                                                                    </div>
                                                                    <br />
                                                                    <br />
                                                                     <div class="row">
                                                                       
                                                                         <div class="col-md-6 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                    No Kakitangan <span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               
                                                                                    <asp:TextBox ID="txticno" runat="server" class="form-control" ></asp:TextBox> 
                                                                            </div>
                                                                        </div>
                                                                          <div class="col-md-3 col-sm-2">
                                                                          
                                                                            <div class="col-md-7 col-sm-2">
                                                                               
                                                                                  <asp:Button ID="btncarian" runat="server" class="btn btn-danger" Text="Carian" 
                                                                    UseSubmitBehavior="false" onclick="btncarian_Click"  />
                                                                            </div>
                                                                        </div>
                                                                          
                                                                    </div>
                                                                     <br />
                                                                    <br />
                                                                 <fieldset class="col-md-12">
                                                                   
                                                                     <br />
                                                                     <div class="row">
                                                                        <div class="col-md-4 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                   Nama <span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                                
                                                                                   <asp:TextBox ID="txtname" runat="server" class="form-control uppercase"></asp:TextBox> 
   
                                                                            </div>
                                                                        </div>
                                                                         <div class="col-md-4 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                     Nama Bank <span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               
                                                                                    <asp:TextBox ID="txtbname" runat="server" class="form-control uppercase"></asp:TextBox> 
                                                                            </div>
                                                                        </div>
                                                                          <div class="col-md-4 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                     No Akaun Bank <span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               
                                                                                    <asp:TextBox ID="txtbno" runat="server" class="form-control"></asp:TextBox> 
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                     </fieldset>
                                                                    <br />
                                                                     <br />
                                                                 <br />
                                                                <br /> <fieldset class="col-md-12">
                                                                   
                                                                     <br />
                                                                    <div class="row">
                                                                          <div class="col-md-4 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                     Terma <span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               
                                                                                     <asp:TextBox ID="txtterma" runat="server" class="form-control uppercase"></asp:TextBox> 
                                                                            </div>
                                                                        </div>
                                                                          <div class="col-md-4 col-sm-2" id="jurnal_show" runat="server" visible="false">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                     Jurnal No :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                                     <asp:DropDownList ID="jurnal_no" runat="server" class="form-control uppercase" OnSelectedIndexChanged="jurnal_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                          <div class="col-md-4 col-sm-2" id="jurnal_show2" runat="server" visible="false">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                     Jurnal No :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                                     <asp:TextBox ID="TextBox2" runat="server" class="form-control uppercase"></asp:TextBox> 
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    </fieldset>
                                                                    <br />
                                                                     <br />
                                                                <br />
                                                                <br />
                                                                     <div class="row">

                                                                         <div class="col-md-6 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                     Jenis Permohonan <span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               <%--<asp:TextBox ID="txtjenis" runat="server" class="form-control uppercase"  ></asp:TextBox>--%>
                                                                                <asp:DropDownList ID="txtjenis" runat="server" class="form-control uppercase">
                                                                                    <asp:ListItem Value="">--- PILIH ---</asp:ListItem>
                                                                                    <asp:ListItem Value="01">Tender</asp:ListItem>
                                                                                    <asp:ListItem Value="02">Sebut Harga</asp:ListItem>
                                                                                    <asp:ListItem Value="03">Pembelian Terus</asp:ListItem>
                                                                                    <asp:ListItem Value="04">Panjar Wang Runcit</asp:ListItem>
                                                                                    <asp:ListItem Value="05">Pembelian Secara Darurat / Kecemasan</asp:ListItem>
                                                                                    <asp:ListItem Value="06">Tuntutan Perubatan</asp:ListItem>
                                                                                    <asp:ListItem Value="07">Caruman KWSP </asp:ListItem>
                                                                                    <asp:ListItem Value="08">Caruman Perkeso</asp:ListItem>
                                                                                    <asp:ListItem Value="09">Bayaran Gaji Kakitangan</asp:ListItem>
                                                                                    <asp:ListItem Value="10">Bayaran Potongan Angkasa</asp:ListItem>
                                                                                    <asp:ListItem Value="11">Bayaran Potongan Koop/Bank</asp:ListItem>
                                                                                    <asp:ListItem Value="12">Bayaran Potongan Tabung Haji</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                   
                                                                            </div>
                                                                        </div>

                                                                           <div class="col-md-6 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                     Kaedah Pembayaran <span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               <%--<asp:TextBox ID="txtjenis" runat="server" class="form-control uppercase"  ></asp:TextBox>--%>
                                                                                <asp:DropDownList ID="dd_kplist" runat="server" class="form-control uppercase">
                                                                                    <asp:ListItem Value="">--- PILIH ---</asp:ListItem>
                                                                                    <asp:ListItem Value="01">Pindahan Akaun</asp:ListItem>
                                                                                    <asp:ListItem Value="02">Bank Deraf</asp:ListItem>
                                                                                    <asp:ListItem Value="03">Cek</asp:ListItem>
                                                                                    <asp:ListItem Value="04">Tunai</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                   
                                                                            </div>
                                                                        </div>

                                                                       
                                                                        
                                                                    </div>
                                                                    <br />
                                                                    <br />

                                                                  <div class="row">

                                                                       <div class="col-md-6 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                     Status <span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               <asp:DropDownList ID="ddstatus" runat="server" class="form-control">
                                                                                   <asp:ListItem>--- PILIH ---</asp:ListItem>
                                                                                     <asp:ListItem Value="B">BARU</asp:ListItem>
                                                                                     <asp:ListItem Value="P">PROSES</asp:ListItem>
                                                                                     <asp:ListItem Value="L">LULUS</asp:ListItem>
                                                                                     <asp:ListItem Value="G">GAGAL</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                   
                                                                            </div>
                                                                        </div>

                                                                       <div class="col-md-6 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                    Catatan <span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                                                                                                                 
                                                                                      <asp:TextBox ID="txtcatatan" runat="server" class="form-control uppercase" TextMode="MultiLine" ></asp:TextBox>
                                                                                  
                                                                            </div>
                                                                        </div>
                                                                      </div>
                                                                    <br />
                                                                    <br />
                                                                        <div class="col-md-12 table-responsive uppercase" style="overflow:auto;">
                                          


          <asp:gridview ID="grdmohon" runat="server"  CssClass="table datatable dataTable no-footer" Font-Size="12px"  ShowFooter="True"  AutoGenerateColumns="False" CellPadding="4"  ForeColor="#333333" GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting" onrowdatabound="grdmohon_RowDataBound" >
            <Columns>

            <asp:TemplateField HeaderText="Tarikh Dokumen" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox11" runat="server" Width="110px" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
                </ItemTemplate>
                 <FooterTemplate>
                 <asp:Button ID="ButtonAdd" runat="server" Text="Add New Row" 
                        onclick="ButtonAdd1_Click" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="No Invois" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox12" class="form-control uppercase" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Keterangan" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox13" runat="server" TextMode="MultiLine" class="form-control uppercase" Height="60px" Width="200px"  ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Jumlah (RM)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox14" Placeholder="0.00" CssClass="form-control" OnTextChanged="QtyChangedt1" AutoPostBack="true" runat="server" Style="text-align:right;"></asp:TextBox>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label11" CssClass="form-control"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Gst (RM)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox15" Placeholder="0.00" CssClass="form-control" OnTextChanged="QtyChangedt2" AutoPostBack="true"  runat="server" Style="text-align:right;"></asp:TextBox>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label12" CssClass="form-control"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Jumlah termasuk gst (RM)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox16" Placeholder="0.00"  CssClass="form-control"  runat="server" Style="text-align:right;"></asp:TextBox>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label13" CssClass="form-control"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>

            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="White" />
        </asp:gridview>


                      <asp:gridview ID="gridmohdup" runat="server" Font-Size="12px"  CssClass="table datatable dataTable no-footer" 
                                                                             ShowFooter="True"  AutoGenerateColumns="False" CellPadding="4" 
                                                                             ForeColor="#333333" GridLines="None" 
                                                                             OnRowDeleting="grvStudentDetails_RowDeleting" onrowdatabound="gridmohdup_RowDataBound" 
                                                                             >
            <Columns>
            <asp:BoundField DataField="tarkih_invois" HeaderText="Tarkih Invois" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="No_rujukan" HeaderText="No rujukan" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
             <asp:BoundField DataField="Keteragan" HeaderText="Keteragan" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
             <asp:BoundField DataField="Gjumlah" HeaderText="Jumlah (RM)" HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" />
              <asp:BoundField DataField="Gst" HeaderText="Gst (RM)" HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" />
               <asp:BoundField DataField="overall" HeaderText="Jumalah termasuk gst (RM)" HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" />
            
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="White" />
        </asp:gridview>

         <br />
                                                                    <br />
                                                                     <br />
                                                                     <div class="row">
                                                                    
                                                                        <div class="col-md-3 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                  
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               
                                                                                  
                                                                            </div>
                                                                        </div>
                                                                         <div class="col-md-3 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                  
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               
                                                                                  
                                                                            </div>
                                                                        </div>
                                                                          
                                                                        <div class="col-md-6 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                 Jumlah Keseluruhan  <span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                            <asp:TextBox ID="TextBox17" Placeholder="0.00"  runat="server" Style="text-align:right;" class="form-control"></asp:TextBox> 
                                                                            </div>
                                                                       
                                                                        </div>
                                                                    </div>
                                                 </div>
                                                 <br />
                                                                  
                                                                    <div class="row">
                                                                        <div class="col-md-12 col-sm-2" style="text-align: center">
                                                                            <div class="body collapse in">
                                                                                <asp:Button ID="Button2" runat="server" class="btn btn-danger" Text="Simpan" Type="submit"
                                                                                   OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;" onclick="Button2_Click" 
                                                                                    />

                                                                                  <asp:Button ID="btnprintmoh" runat="server" class="btn btn-danger" Text="Print" Type="submit"
                                                                                   onclick="btnprintmoh_Click" 
                                                                                    />
                                                                                 
                                                                                    <asp:Button ID="Button1" runat="server" class="btn btn-danger" Text="Tutup" Type="submit"
                                                                                   onclick="Button1_Click" 
                                                                                    />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                          <br />
                                                                     <br />
                                                                     <br />
                                                 <div class="row" id="sts" runat="server" visible="false"> 
                                                                    
                                                                        <div class="col-md-3 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                   Status <span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                                <asp:DropDownList ID="ddsts" runat="server" class="form-control" >
                                                                                <asp:ListItem>--- PILIH ---</asp:ListItem>
                                                                                     <asp:ListItem Value="P">PROSES</asp:ListItem>
                                                                                     <asp:ListItem Value="L">LULUS</asp:ListItem>
                                                                                     <asp:ListItem Value="G">GAGAL</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                  
                                                                            </div>
                                                                        </div>
                                                                         <div class="col-md-3 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                  Approved by
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                                 <asp:TextBox ID="txtApr" runat="server" class="form-control" ></asp:TextBox> 
                                                                                  
                                                                            </div>
                                                                        </div>
                                                                          
                                                                        <div class="col-md-6 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                 Catatan  :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                            <asp:TextBox ID="TextBox29" runat="server" class="form-control uppercase" TextMode="MultiLine" Height="140px"></asp:TextBox> 
                                                                            </div>
                                                                       
                                                                        </div>
                                                                    </div>
                                                                    <br />     
                                                                       <div class="row" id="Div10" runat="server" visible="false"> 
                                                                     <div class="col-md-3 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                 
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               
                                                                                  
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-2 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                 
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               
                                                                            </div>
                                                                        </div>
                                                                         <div class="col-md-4 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <asp:Button ID="btnkem" runat="server" class="btn btn-danger" Text="Kemaskini" Type="submit"
                                                                                   onclick="btnkem_Click" />
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               
                                                                                  
                                                                            </div>
                                                                        </div>
                                                                          
                                                                        <div class="col-md-3 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                               
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                           
                                                                            </div>
                                                                       
                                                                        </div>
                                                                    </div>
                                                                    <br />                                                               
                                                                </div>
                                                        
                                                                   </div>
                                                                    <br />
                                                                    <br />
                                                                    </div>
                                                                    </div> 

                                                                      <div role="tabpanel1" class="tab-pane" runat="server" id="p4">
                                                                     <div id="Div12"  runat="server">
                                                                    <fieldset class="col-md-12">
                                                                     <legend>Filter</legend>
                                                                     <br />
                                                             <div class="row">
                                                                       
                                                                         <div class="col-md-4 col-sm-2">
                                                                            <div class="col-md-6 col-sm-1">
                                                                                <label>
                                                                                     Nombor Permohonan 
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-6 col-sm-2">
                                                                               
                                                                                    <asp:TextBox ID="TextBox6" runat="server" class="form-control"></asp:TextBox> 
                                                                            </div>
                                                                        </div>
                                                                          <div class="col-md-4 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                  Tarikh Permohonan 
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               <div class="input-group">
                                                                                    <span class="input-group-addon"><i class="icon-calendar"></i></span>
                                                                                    <asp:TextBox ID="TextBox25" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox> 
                                                                                    </div>
                                                                               
                                                                            </div>
                                                                              
                                                                            </div>
                                                                       
                                                                         
                                                                        <div class="col-md-4 col-sm-2">
                                                                            <div class="col-md-6 col-sm-1">
                                                                               <asp:CheckBox ID="chk_invbill" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                 <asp:Button ID="Button6" runat="server" class="btn btn-danger" Text="Carian" 
                                                                    UseSubmitBehavior="false" onclick="buttab3_Click"    />
                                                                                 <asp:Button ID="Button4" runat="server" class="btn btn-danger" Text="+Tambah" 
                                                                    UseSubmitBehavior="false" onclick="tab4tam_Click"
                                                                 />
                                                                            </div>
                                                                          
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    </fieldset>
                                                                    <br />
                                                                  <%-- <div class="row">--%>
                                                                        <asp:gridview ID="grdBinv" runat="server" Font-Size="12px"  CssClass="table datatable dataTable no-footer uppercase" ShowFooter="false"  AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="true" PageSize="25" OnPageIndexChanging="gvSelected_PageIndexChanging_IB" >
                <Columns>
                 <asp:TemplateField HeaderText="BIL">  
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                     <asp:TemplateField HeaderText="No Permohonan" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                  <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("No_permohonan") %>'  CommandArgument='<%# Eval("No_permohonan")+ "," + Eval("Data") %>' CommandName="Add" OnClick="lblSubItemInv_Click" Font-Bold Font-Underline></asp:LinkButton>
             </ItemTemplate>
            </asp:TemplateField>
                      <asp:TemplateField HeaderText="Jenis Permohonan" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                  <asp:Label ID="Label2"  runat="server" Text='<%# Eval("Data") %>'  CommandArgument='<%# Eval("Data")%>'  ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                     <asp:BoundField DataField="Data" HeaderText="Data" ItemStyle-HorizontalAlign="Center" Visible="false" />
            <asp:BoundField DataField="tarkih_mohon" HeaderText="Tarkih Permohonan" ItemStyle-HorizontalAlign="Center" />
            
            <asp:BoundField DataField="tarkih_invois" HeaderText="Tarkih Invois" ItemStyle-HorizontalAlign="Center" />
             <%--<asp:BoundField DataField="no_invois" HeaderText="No Invois" ItemStyle-HorizontalAlign="Center" />--%>
                <asp:BoundField DataField="jumlah" HeaderText="Jumlah (RM)" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="Payamt" HeaderText="Paid Amount (RM)" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="Baki" HeaderText="BAKI (RM)" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n}"/>

                    <%-- <asp:TemplateField HeaderText="BAKI (RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="Label15" runat="server"  Text='<%# Convert.ToDecimal(Eval("jumlah")) -Convert.ToDecimal(Eval("Payamt")) %>'     ></asp:Label>
                    <asp:Label  runat="server" ID="LblHourRemaining"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>--%>
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="White" />
        </asp:gridview>
                                                                 <%--  </div>--%>
                                                                </div>
                                                                <br />
                                                                <br />
                                                                   <div id="Div13" role="tabpanel1" runat="server">
                                                            <!-- Nav tabs -->
                                                            <ul class="s1 nav nav-tabs" role="tablist1">
                                                            <li id="Li3" runat="server" class="active"><a href="#ContentPlaceHolder1_p65" aria-controls="p65" role="tab" data-toggle="tab">Butiran Invois</a></li>
                                                            </ul>
                                                            <!-- Tab panes -->
                                                            <div class="tab-content" style="padding-top: 20px">
                                                          
                                                            
                                                            <div role="tabpanel1" class="tab-pane active" runat="server" id="p65">
                                                                    <div class="chat-panel panel panel-primary">
                                                                      
                                                                    </div>
                                                                    <br />
                                                                      <br />
                                                                     <div class="row">
                                                                    
                                                                        <div class="col-md-3 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                   Jenis Permohonan <span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                                    <asp:DropDownList ID="dddata" runat="server" class="form-control" OnSelectedIndexChanged="dddata_SelectedIndexChanged" AutoPostBack="true">
                                                                                        <asp:ListItem>--- PILIH ---</asp:ListItem>
                                                                                        <asp:ListItem>MOHON BAYAR</asp:ListItem>
                                                                                        <asp:ListItem>BARU</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                         <div class="col-md-3 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                    No Permohonan <%--<span style="color: Red">*</span>--%>:
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               
                                                                                   <asp:DropDownList ID="ddnoper" runat="server" class="form-control uppercase" OnSelectedIndexChanged="ddnoper_SelectedIndexChanged" AutoPostBack="true">
                                                                                </asp:DropDownList> 

                                                                                <asp:TextBox ID="txtnoperbil" runat="server" class="form-control uppercase" Visible="false"></asp:TextBox> 
                                                                            </div>
                                                                        </div>
                                                                          <div class="col-md-3 col-sm-2" id="kataka" runat="server">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                  Kategori Akaun  <span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                              <asp:DropDownList ID="ddkataka" runat="server" class="form-control" OnSelectedIndexChanged="ddkataka_SelectedIndexChanged" AutoPostBack="true">
                                                                                  <asp:ListItem>--- PILIH ---</asp:ListItem>
                                                                                        <asp:ListItem>PEMBEKAL</asp:ListItem>
                                                                                        <asp:ListItem>SEMUA COA</asp:ListItem>
                                                                                </asp:DropDownList> 
                                                                            </div>
                                                                       
                                                                        </div>
                                                                        <div class="col-md-3 col-sm-2" id="kodaka" runat="server">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                Kod Akaun (Debit) <span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                             <asp:DropDownList ID="ddkodaka" runat="server" class="form-control uppercase">
                                                                                </asp:DropDownList> 
                                                                            </div>
                                                                       
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <br />
                                                                 <br />
                                                                    <br />
                                                                <div  id="invbil" runat="server" >
                                                                 <div class="row" >
                                                                    
                                                                        <div class="col-md-3 col-sm-2" id="tmb1" runat="server" >
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                   Tarikh Mohon<span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                                   <div class="input-group">
                                                                                    <span class="input-group-addon"><i class="icon-calendar"></i></span>
                                                                                    <asp:TextBox ID="txttmb" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox> 
                                                                                    </div>
                                                                            </div>
                                                                        </div>
                                                                         <div class="col-md-3 col-sm-2" id="tmb2" runat="server">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                    Tarikh Invois <span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               
                                                                                    <div class="input-group">
                                                                                    <span class="input-group-addon"><i class="icon-calendar"></i></span>
                                                                                    <asp:TextBox ID="txttinvbil" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox> 
                                                                                    </div>
                                                                            </div>
                                                                        </div>
                                                                          <div class="col-md-3 col-sm-2" id="tmb3" runat="server">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                 No Invois  <span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               <asp:TextBox ID="txrinvbil" runat="server" class="form-control uppercase"></asp:TextBox> 
                                                                            </div>
                                                                       
                                                                        </div>
                                                                        <div class="col-md-3 col-sm-2" id="tmb4" runat="server">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                               Terma  <span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                              <asp:TextBox ID="txtinterma" runat="server" class="form-control uppercase"></asp:TextBox> 
                                                                            </div>
                                                                       
                                                                        </div>
                                                                    </div>
                                                                  </div>
                                                                    <br />
                                                                    <br />
                                                               
                                                                     <div class="col-md-12 table-responsive uppercase" style="overflow:auto;">
                                          
                                            <asp:gridview ID="grdbilinv1" runat="server"  Font-Size="12px" CssClass="table datatable dataTable no-footer" 
                                                                             ShowFooter="false"  AutoGenerateColumns="False" CellPadding="4" 
                                                                             ForeColor="#333333" GridLines="None" 
                                                                            
                                                                             >
            <Columns>
           
                 <asp:TemplateField HeaderText="Tarkih Mohonan" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                  <asp:Label ID="Label1_1"  runat="server" Text='<%# Eval("tarkih_permohonan") %>'  ></asp:Label>
                    <asp:Label ID="lbl_jurn_no" Visible="false" runat="server" Text='<%# Eval("jornal_no") %>'  ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Tarkih Invois" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                  <asp:Label ID="Label2_1"  runat="server" Text='<%# Eval("tarkih_invois") %>'  ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="No Invois" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                  <asp:Label ID="Label3_1"  runat="server" Text='<%# Eval("no_invois") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
               <%-- <asp:TemplateField HeaderText="No Rujukan" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                  <asp:Label ID="Label4_1"  runat="server" Text='<%# Eval("No_rujukan") %>'   ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Keteragan" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                  <asp:Label ID="Label5_1" runat="server" Text='<%# Eval("Keteragan") %>'  ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                  <asp:TemplateField HeaderText="Jumlah (RM)" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="right">
                <ItemTemplate>
                  <asp:Label ID="Label6_1"  runat="server" Text='<%# Eval("Gjumlah","{0:n}") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Gst (RM)" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="right">
                <ItemTemplate>
                  <asp:Label ID="Label7_1"  runat="server" Text='<%# Eval("Gst","{0:n}") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Jumlah Termasuk GST (RM)" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="right">
                <ItemTemplate>
                  <asp:Label ID="Label8_1"  runat="server" Text='<%# Eval("overall","{0:n}") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:RadioButton ID="chkbind" runat="server" onclick="RadioCheck(this);"  />
                </ItemTemplate>
            </asp:TemplateField>
           
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="White" />
        </asp:gridview>

                                                      <asp:gridview ID="grdbilinv" runat="server"  Font-Size="12px" CssClass="table datatable dataTable no-footer uppercase" 
                                                                             ShowFooter="True"  AutoGenerateColumns="False" CellPadding="4" 
                                                                             ForeColor="#333333" GridLines="None" 
                                                                             OnRowDeleting="grvStudentDetails_RowDeleting"   OnRowDataBound="grdbilinv_RowDataBound"
                                                                             >
            <Columns>
           <asp:TemplateField HeaderText="Kategori Akaun"  ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>

                     <asp:DropDownList ID="ddkodKat" runat="server" class="form-control"  OnSelectedIndexChanged="ddkodKat_SelectedIndexChanged" AutoPostBack="true">   
                     <asp:ListItem>--- PILIH ---</asp:ListItem>
                     <asp:ListItem>PEMBEKAL</asp:ListItem>
                     <asp:ListItem>SEMUA COA</asp:ListItem>
                          </asp:DropDownList>
                </ItemTemplate>
                   <FooterTemplate>
                 <asp:Button ID="ButtonAdd" runat="server" Text="Add New Row" 
                        onclick="ButtonAddMohBil_Click" />
                </FooterTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Credit Kod Akaun" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>

                     <asp:DropDownList ID="ddkodcre" runat="server" Width="100px" class="form-control uppercase">    </asp:DropDownList>
                </ItemTemplate>
                 
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Debit Kod Akaun" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                     <asp:DropDownList ID="ddkodBil" runat="server" Width="100px" class="form-control uppercase">    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
               
                <asp:TemplateField HeaderText="Projek" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:DropDownList ID="ddprobil" runat="server" class="form-control uppercase"   >    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Harga/unit" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox55" runat="server"  Width="70px"   placeholder="0.00" OnTextChanged="QtyChangedMohBil_kty" AutoPostBack="true" style="text-align:right;" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Kuantiti" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox56" runat="server"  OnTextChanged="QtyChangedMohBil" AutoPostBack="true"  Width="70px"  placeholder="0" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
                    <asp:TemplateField HeaderText="Disk" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="Txtdis" runat="server"  Width="70px"  placeholder="0.00" style="text-align:right;"   CssClass="form-control"     OnTextChanged="disChangedMohBil" AutoPostBack="true"   ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Jumlah" ItemStyle-HorizontalAlign="right">
                <ItemTemplate>
                  <asp:Label ID="Label1" CssClass="form-control" runat="server" Text="0.00" Width="100px"  ></asp:Label>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
           <FooterTemplate>
                   <asp:Label ID="Label2" CssClass="form-control" runat="server" Text="0.00"  ></asp:Label>
         </FooterTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Tax" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="ChckedChangedMohBil" AutoPostBack="true"  />
                </ItemTemplate>
            </asp:TemplateField>

                <asp:TemplateField HeaderText="Caj Perkhidmatan (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:DropDownList ID="ddcukaioth" runat="server" class="form-control"  onselectedindexchanged="ddcukaiothMohBil_SelectedIndexChanged" AutoPostBack="true"  >    </asp:DropDownList>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label7" CssClass="form-control"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
                      <asp:TemplateField HeaderText="Caj Perkhidmatan (RM)" ItemStyle-HorizontalAlign="Right"  Visible="false">
                <ItemTemplate>
                    <asp:Label ID="Label10" CssClass="form-control" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label11" CssClass="form-control"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="GST (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:DropDownList ID="ddcukaiinv" runat="server" class="form-control"  onselectedindexchanged="ddcukaiinvMohBil_SelectedIndexChanged" AutoPostBack="true" >    </asp:DropDownList>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label4" CssClass="form-control"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
                       <asp:TemplateField HeaderText="AMAUN GST (RM)" ItemStyle-HorizontalAlign="Right" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="Label8" CssClass="form-control" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label9" CssClass="form-control"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Jumlah Inclu.GST (RM)" ItemStyle-HorizontalAlign="right">
                <ItemTemplate>
                     <asp:Label ID="Label5" CssClass="form-control" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                 <asp:Label ID="Label6" CssClass="form-control" runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
              
           
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="White" />
        </asp:gridview>

                                                                         <asp:gridview ID="grdbilinvdub" runat="server" Font-Size="12px"  CssClass="table datatable dataTable no-footer" 
                                                                             ShowFooter="True"  AutoGenerateColumns="False" CellPadding="4" 
                                                                             ForeColor="#333333" GridLines="None" 
                                                                             OnRowDeleting="grvStudentDetails_RowDeleting"   OnRowDataBound="grdbilinvdub_RowDataBound"
                                                                             >
            <Columns>
                  <asp:TemplateField HeaderText="Kategori Akaun" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                      <asp:Label ID="ddkodKat" runat="server" Text='<%# Eval("kat_akaun") %>' />
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Credit Kod Akaun" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                      <asp:Label ID="lblcre" runat="server" Text='<%# Eval("cre_kod_akaun") %>' Visible = "false" />
                      <asp:Label ID="ddkodcre" runat="server" Text='<%# Eval("cre_name") %>' />
                   <%--  <asp:DropDownList ID="ddkodcre" runat="server" class="form-control uppercase">    </asp:DropDownList>--%>
                </ItemTemplate>
                 
            </asp:TemplateField>

                  <asp:TemplateField HeaderText="Debit Kod Akaun" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                     <asp:Label ID="lbldeb" runat="server" Text='<%# Eval("deb_kod_akaun") %>' Visible = "false" />
                     <asp:Label ID="ddkodBil" runat="server" Text='<%# Eval("deb_name") %>' />
                     <%--<asp:DropDownList ID="ddkodBil" runat="server" class="form-control uppercase">    </asp:DropDownList>--%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="tarkih_mohon" HeaderText="Tarikh Mohon" ItemStyle-HorizontalAlign="Center" />
         <asp:BoundField DataField="tarkih_invois" HeaderText="Tarikh Invois" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="no_invois" HeaderText="No Invois" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="item" HeaderText="No Rujukan" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="keterangan" HeaderText="Keterangan" ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="Projek" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblprodub" runat="server" Text='<%# Eval("project_kod") %>' Visible = "false" />
                     <asp:Label ID="ddprobildub" runat="server" Text='<%# Eval("Ref_Projek_name") %>' />
                    <%--<asp:DropDownList ID="ddprobildub" runat="server" class="form-control"   >    </asp:DropDownList>--%>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:BoundField DataField="unit" HeaderText="HARAGA/Unit" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" />
            <asp:BoundField DataField="quantiti" HeaderText="Quantiti" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="jumlah" HeaderText="Jumlah (RM)" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n}" />
            <asp:BoundField DataField="gstjumlah" HeaderText="GST (RM)" ItemStyle-HorizontalAlign="Right" />
             <asp:BoundField DataField="Overall" HeaderText="Jumlah (RM)" ItemStyle-HorizontalAlign="Right" />
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="White" />
        </asp:gridview>

           <asp:gridview ID="Gridview5" Font-Size="12px" runat="server"  CssClass="table datatable dataTable no-footer uppercase" ShowFooter="True"  AutoGenerateColumns="False" CellPadding="4"  ForeColor="#333333" GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting" OnRowDataBound="Gridview5_RowDataBound" >
         <Columns>
           
             <asp:TemplateField HeaderText="Kod Akaun (Kredit)" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>

                     <asp:DropDownList ID="ddkodbil" runat="server" class="form-control uppercase" style=" width:125px">    </asp:DropDownList>
                </ItemTemplate>
                  <FooterTemplate>
                 <asp:Button ID="ButtonAdd" runat="server" Text="Add New Row" 
                        onclick="ButtonAddBil_Click" />
                </FooterTemplate>
            </asp:TemplateField>
             
            <asp:TemplateField HeaderText="Item" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox51" runat="server" class="form-control uppercase"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="No Po" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox50" runat="server" class="form-control uppercase"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Keterangan" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox52" class="form-control uppercase" TextMode="MultiLine" Height="40px" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Projek" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                     <asp:DropDownList ID="ddkodproj" style="width:100%;" runat="server" class="form-control select2 validate[optional]"> </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Harga/unit" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox53" runat="server" class="form-control uppercase"  Width="70px"   placeholder="0.00" OnTextChanged="QtyChangedBil_kty" AutoPostBack="true" style="text-align:right;" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Kuantiti" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox54" runat="server" class="form-control uppercase"   Width="70px"  placeholder="0" OnTextChanged="QtyChangedBil" AutoPostBack="true"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Disk" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="Txtdis" runat="server"  Width="70px"  placeholder="0.00" style="text-align:right;"   CssClass="form-control"     OnTextChanged="disChangedBil" AutoPostBack="true"   ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Jumlah" ItemStyle-HorizontalAlign="right">
                <ItemTemplate>
                  <asp:Label ID="Label1" CssClass="form-control" runat="server" Text="0.00" Width="100px"  ></asp:Label>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
           <FooterTemplate>
                   <asp:Label ID="Label2" CssClass="form-control" runat="server" Text="0.00"  ></asp:Label>
         </FooterTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Tax" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="ChckedChangedBil" AutoPostBack="true"  />
                </ItemTemplate>
            </asp:TemplateField>

                <asp:TemplateField HeaderText="Caj Perkhidmatan %" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:DropDownList ID="ddcukaioth" runat="server" class="form-control"  onselectedindexchanged="ddcukaiothBil_SelectedIndexChanged" AutoPostBack="true"  >    </asp:DropDownList>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label7" CssClass="form-control"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
                      <asp:TemplateField HeaderText="Caj Perkhidmatan (RM)" ItemStyle-HorizontalAlign="Right"  Visible="false">
                <ItemTemplate>
                    <asp:Label ID="Label10" CssClass="form-control" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label11" CssClass="form-control"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="GST %" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:DropDownList ID="ddcukaiinv" runat="server" class="form-control"  onselectedindexchanged="ddcukaiinvBil_SelectedIndexChanged" AutoPostBack="true" >    </asp:DropDownList>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label4" CssClass="form-control"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
                       <asp:TemplateField HeaderText="GST Amt" ItemStyle-HorizontalAlign="Right" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="Label8" CssClass="form-control" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label9" CssClass="form-control"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Jumlah Inclu.GST" ItemStyle-HorizontalAlign="right">
                <ItemTemplate>
                     <asp:Label ID="Label5" CssClass="form-control" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                 <asp:Label ID="Label6" CssClass="form-control" runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
           
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="White" />                                                                 
           
        </asp:gridview>

                                                                 <asp:gridview ID="grdvie5dup" runat="server"  CssClass="table datatable dataTable no-footer" ShowFooter="True"  AutoGenerateColumns="False" CellPadding="4"  ForeColor="#333333" GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting" onrowdatabound="grdvie5dup_RowDataBound" >
            <Columns>
           
             <asp:TemplateField HeaderText="Kod Akaun" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                 <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("deb_kod_akaun") %>' Visible = "false" />
                     <asp:DropDownList ID="ddkoddup" runat="server" Width="150px" class="form-control uppercase">    </asp:DropDownList>
                </ItemTemplate>
               
            </asp:TemplateField>
             <asp:BoundField DataField="item" HeaderText="Item" ItemStyle-HorizontalAlign="Center" />
              <asp:BoundField DataField="keterangan" HeaderText="Keterangan" ItemStyle-HorizontalAlign="Center"  />
               <asp:BoundField DataField="unit" HeaderText="Harga/unit" ItemStyle-HorizontalAlign="Center"  />
                <asp:BoundField DataField="quantiti" HeaderText="Kuantiti" ItemStyle-HorizontalAlign="Center" />
                  <asp:BoundField DataField="discount" HeaderText="Disk" ItemStyle-HorizontalAlign="Center" />
                 <asp:BoundField DataField="jumlah" HeaderText="Jumlah" ItemStyle-HorizontalAlign="Center" />
                  
          
             <asp:TemplateField HeaderText="Tax" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="c" runat="server" class="form-control" Checked='<%# bool.Parse(Eval("tax").ToString()) %>' Enable='<%# !bool.Parse(Eval("tax").ToString()) %>' />
                </ItemTemplate>
            </asp:TemplateField>
                
           <asp:TemplateField HeaderText="Caj Perkhidmatan" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                 <asp:Label ID="lblgstoth" runat="server" Text='<%# Eval("othgsttype") %>' Visible = "false" />
                     <asp:DropDownList ID="ddtaxoth" runat="server" class="form-control">    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="gst" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                 <asp:Label ID="lblgst" runat="server" Text='<%# Eval("gsttype") %>' Visible = "false" />
                     <asp:DropDownList ID="ddtax" runat="server" class="form-control">    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
                  <asp:BoundField DataField="Overall" HeaderText="Jumlah Termasuk CBP(RM)" ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="White" />                                                                 
           
        </asp:gridview>

         <br />
                                                                    <br />
                                                                     <br />
                                                                     <div class="row">
                                                                    
                                                                        <div class="col-md-3 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                   
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               
                                                                                  
                                                                            </div>
                                                                        </div>
                                                                         <div class="col-md-3 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                  
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               
                                                                                  
                                                                            </div>
                                                                        </div>
                                                                          
                                                                        <div class="col-md-6 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                 Jumlah Keseluruhan  <span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                            <asp:TextBox ID="TextBox37" runat="server" class="form-control" style="text-align:right;"></asp:TextBox> 
                                                                            </div>
                                                                       
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                 </div> 
                                                                    <br />
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-md-12 col-sm-2" style="text-align: center">
                                                                            <div class="body collapse in">
                                                                                <asp:Button ID="Button8" runat="server" class="btn btn-danger" Text="Simpan" Type="submit"
                                                                                   OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;" onclick="Button8_Click" 
                                                                                    /> 
                                                                                   
                                                                                    <asp:Button ID="Button11" runat="server" class="btn btn-danger" Text="Tutup" 
                                                                                    Type="submit" onclick="Button11_Click"
                                                                                 
                                                                                    />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                                                                                    
                                                                </div>
                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                   
                                                                </div>
                                                                  
                                                            <div role="tabpanel1" class="tab-pane" runat="server" id="p1">
                                                                     <div id="Div4"  runat="server">
                                                                    <fieldset class="col-md-12">
                                                                     <legend>Filter</legend>
                                                                     <br />
                                                             <div class="row">
                                                                       
                                                                         <div class="col-md-4 col-sm-2">
                                                                            <div class="col-md-6 col-sm-1">
                                                                                <label>
                                                                                     PV No
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-6 col-sm-2">
                                                                               
                                                                                    <asp:TextBox ID="TextBox8" runat="server" class="form-control"></asp:TextBox> 
                                                                            </div>
                                                                        </div>
                                                                          <div class="col-md-4 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                  Tarikh PV 
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               <div class="input-group">
                                                                                    <span class="input-group-addon"><i class="icon-calendar"></i></span>
                                                                                    <asp:TextBox ID="TextBox10" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox> 
                                                                                    </div>
                                                                               
                                                                            </div>
                                                                              
                                                                            </div>
                                                                       
                                                                        <div class="col-md-4 col-sm-2">
                                                                            <div class="col-md-6 col-sm-1">
                                                                                <asp:CheckBox ID="chk_pvouch" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                 <asp:Button ID="buttab2" runat="server" class="btn btn-danger" Text="Carian" 
                                                                    UseSubmitBehavior="false" onclick="buttab2_Click"    />
                                                                                <asp:Button ID="tab2tam" runat="server" class="btn btn-danger" Text="+Tambah" 
                                                                    UseSubmitBehavior="false" onclick="tab2tam_Click"
                                                                 />
                                                                            </div>
                                                                          
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    </fieldset>
                                                                    <br />
                                                                   <%--<div class="row">--%>
                                                                        <asp:gridview ID="grdinvoisview" runat="server" Font-Size="12px"  CssClass="table datatable dataTable no-footer uppercase"  AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging_t3"  >
            <Columns>
           <asp:TemplateField HeaderText="BIL">  
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="PV No" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                  <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("Pv_no") %>'  CommandArgument='<%# Eval("Pv_no")%>' CommandName="Add" OnClick ="lblSubItemPV_Click" Font-Bold Font-Underline ></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        
             <asp:BoundField DataField="tarkih_pv" HeaderText="Tarikh Payment" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="No_cek" HeaderText="No Cek" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Akaun_name" HeaderText="Beneficiary" ItemStyle-HorizontalAlign="Center" />
              <asp:BoundField DataField="PaidAmount" HeaderText="Paid Amount (RM)" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="status" HeaderText="Status" ItemStyle-HorizontalAlign="Center" />
              
             
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="White" />
        </asp:gridview>
                                                                  <%-- </div>--%>
                                                                </div>
                                                                <br />
                                                                <br />
                                                                   <div id="Div5" role="tabpanel1" runat="server">
                                                            <!-- Nav tabs -->
                                                            <ul class="s1 nav nav-tabs" role="tablist1">
                                                            <li id="Li2" runat="server" class="active"><a href="#ContentPlaceHolder1_p62" aria-controls="p62" role="tab" data-toggle="tab">Butiran Invois</a></li>
                                                            </ul>
                                                            <!-- Tab panes -->
                                                            <div class="tab-content" style="padding-top: 20px">
                                                          
                                                            
                                                            <div role="tabpanel1" class="tab-pane active" runat="server" id="p62">
                                                                    <div class="chat-panel panel panel-primary">
                                                                      
                                                                    </div>
                                                                <br />
                                                                <div class="row">
                                                                    <div class="col-md-4 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                   Jenis Permohonan <span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               
                                                                                     <asp:TextBox ID="txtdata" runat="server" class="form-control" Text="PAYMENT VOUCHER"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                <br />
                                                                    <br />
                                                                <div class="row">
                                                                    
                                                                         <div class="col-md-4 col-sm-2" id="pv_tab3" runat="server" visible="false">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                    PV No <span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               
                                                                                    <asp:TextBox ID="txtpvno" runat="server" class="form-control uppercase"></asp:TextBox> 
                                                                                <asp:TextBox ID="txtpvno_1" runat="server" Visible="false" class="form-control"></asp:TextBox> 
                                                                                <asp:TextBox ID="ddnoperpv_PV1" runat="server" Visible="false" class="form-control"></asp:TextBox> 
                                                                            </div>
                                                                        </div>

                                                                      <div class="col-md-4 col-sm-2" id="Div11" runat="server">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                  Kategori Akaun  <span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                              <asp:DropDownList ID="ddpvkat" runat="server" class="form-control" OnSelectedIndexChanged="ddpvkat_SelectedIndexChanged" AutoPostBack="true">
                                                                                        <asp:ListItem >--- PILIH ---</asp:ListItem>
                                                                                        <asp:ListItem >PEMBEKAL</asp:ListItem>
                                                                                        <asp:ListItem >SEMUA COA</asp:ListItem>
                                                                                      
                                                                                </asp:DropDownList> 
                                                                            </div>
                                                                       
                                                                        </div>
                                                                        <div class="col-md-4 col-sm-2" id="Div14" runat="server">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                Kod Akaun (Debit)  <span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                             <asp:DropDownList ID="ddpvkod" runat="server" class="form-control uppercase" OnSelectedIndexChanged="ddpvkod_SelectedIndexChanged" AutoPostBack="true">
                                                                                </asp:DropDownList> 
                                                                            </div>
                                                                       
                                                                        </div>
                                                                        
                                                                    </div>
                                                                  <br />
                                                                <br />
                                                                     <div class="row">
                                                                      <div class="col-md-4 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                  No Invois <span style="color: Red">*</span>:
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               <asp:DropDownList ID="ddnoperpv" runat="server" class="form-control" OnSelectedIndexChanged="ddnoperpv_SelectedIndexChanged" AutoPostBack="true">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                       
                                                                        </div>


                                                                        <div class="col-md-4 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                Tarikh PV  <span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                            <div class="input-group">
                                                                                <span class="input-group-addon"><i class="icon-calendar"></i></span>
                                                                                <asp:TextBox ID="txttarpv" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                                        placeholder="DD/MM/YYYY" ></asp:TextBox> 
                                                                                        </div>
                                                                            </div>
                                                                       
                                                                        </div>
                                                                       
                                                                         <div class="col-md-4 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                   Bayar Kepada <span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               
                                                                                     <asp:TextBox ID="TXTPVNAME" runat="server" class="form-control" ></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                <br />
                                                                <br />
                                                                <div class="row">
                                                                     
                                                                        <div class="col-md-4 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                 Cara Bayaran  <span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                                 <asp:DropDownList ID="ddpvcara" runat="server" class="form-control uppercase" OnSelectedIndexChanged="ddpvcara_SelectedIndexChanged" AutoPostBack="true"> </asp:DropDownList>                                                                        
                                                                        </div>
                                                                    </div>

                                                                     <div class="col-md-4 col-sm-2" id="Div15" runat="server">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                  Dibayar Oleh (Kredit) <span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                              <asp:DropDownList ID="DDbank" runat="server" class="form-control uppercase" > </asp:DropDownList> 
                                                                            </div>
                                                                       
                                                                        </div>

                                                                     <div class="col-md-4 col-sm-2" id="Div16" runat="server">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                               No Cek  <span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                             <asp:TextBox ID="txtpvnocek" runat="server" class="form-control uppercase"></asp:TextBox> 
                                                                            </div>
                                                                       
                                                                        </div>
                                                                </div>
                                                                    <br />
                                                                    <br />
                                                                        
                                                                         <div class="row">
                                                                    
                                                                        
                                                                       
                                                                          <div class="col-md-4 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                Terma <span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                              <asp:TextBox ID="txtpvterma" runat="server" class="form-control uppercase"></asp:TextBox> 
                                                                            </div>
                                                                       
                                                                        </div>
                                                                        <div class="col-md-4 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                Status  <span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                                 <asp:DropDownList ID="ddpvstatus" runat="server" class="form-control"  Visible="false">
                                                                                       <asp:ListItem Value="">--- PILIH ---</asp:ListItem>
                                                                                     <asp:ListItem Value="B">BARU</asp:ListItem>
                                                                                    
                                                                                </asp:DropDownList>  
                                                                                 <asp:DropDownList ID="ddpvstatus1" runat="server" class="form-control" Visible="false">
                                                                                     <asp:ListItem>--- PILIH ---</asp:ListItem>
                                                                                     <asp:ListItem Value="P">PROSES</asp:ListItem>
                                                                                     <asp:ListItem Value="L">LULUS</asp:ListItem>
                                                                                     <asp:ListItem Value="G">GAGAL</asp:ListItem>
                                                                                </asp:DropDownList>                                                                        
                                                                        </div>
                                                                    </div>
                                                                               </div>
                                                                         <br />
                                                                         <br />
                                                                <br />
                                                                         <br />
                                                                   <%--<div class="row">--%>
                                                                        <asp:gridview ID="grdpvinv" Font-Size="12px" runat="server"  CssClass="table datatable dataTable no-footer uppercase" ShowFooter="True"  AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"  >
                <Columns>
                  
          
           <asp:TemplateField HeaderText="Tarkih Permohonan" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label08" runat="server"     Text='<%# Eval("tarkih_mohon") %>' ></asp:Label>
                    <asp:Label ID="jurnal_po" runat="server" Visible="false"  Text='<%# Eval("no_po") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                      <asp:TemplateField HeaderText="No Permohonan" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label19" runat="server"     Text='<%# Eval("no_permohonan") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                     <asp:TemplateField HeaderText="Tarkih Invois" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label09" runat="server"     Text='<%# Eval("tarkih_invois") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="No Invois" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label10" runat="server"     Text='<%# Eval("no_invois") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
                <asp:TemplateField HeaderText="Jumlah (RM)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label11" runat="server"     Text='<%# Eval("jumlah") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                   
                   <asp:TemplateField HeaderText="Gst Jumlah (RM)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label12" runat="server"     Text='<%# Eval("gstjumlah") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                  <asp:TemplateField HeaderText="Service Charge Jumlah (RM)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label13" runat="server"     Text='<%# Eval("othgstjumlah") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jumlah Termasuk GST (RM)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label14" runat="server"     Text='<%# Eval("Overall") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                     <asp:TemplateField HeaderText="Pay AMAUN (RM)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server"     Text='<%# Eval("Baki") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                     <asp:TemplateField HeaderText="BAKI (RM)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label15" runat="server"  Text='<%#Convert.ToDecimal(Eval("Overall")) -Convert.ToDecimal(Eval("Baki")) %>'     ></asp:Label>
                    <asp:Label  runat="server" ID="LblHourRemaining"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                     
                     <asp:TemplateField HeaderText="Pay AMAUN (RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:TextBox ID="txtpay" runat="server" OnTextChanged="QtyChangedPV" AutoPostBack="true" Style="text-align:right;"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
                     <asp:TemplateField HeaderText="PILIH" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server"   />
                </ItemTemplate>
            </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="White" />
        </asp:gridview>

                                                                        <asp:gridview ID="Grdpvinvdup" Font-Size="12px" runat="server"  CssClass="table datatable dataTable no-footer uppercase" ShowFooter="True"  AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"  >
                <Columns>
                  
           <asp:BoundField DataField="no_invois" HeaderText="No Invois" ItemStyle-HorizontalAlign="Center" />
           <asp:BoundField DataField="tarikh_invois" HeaderText="Tarikh Invois" ItemStyle-HorizontalAlign="Center" />
           <asp:BoundField DataField="jumlah" HeaderText="Jumlah (RM)" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
           <asp:BoundField DataField="gstjumlah" HeaderText="Gst (RM)" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
           <asp:BoundField DataField="othgstjumlah" HeaderText="Service Charge (RM)" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
           <asp:BoundField DataField="Overall" HeaderText="Jumlah Termasuk GST (RM)" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
           <asp:BoundField DataField="Payamt" HeaderText="Paid Amount (RM)" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
         
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="White" />
        </asp:gridview>

                                                                   <%--</div>--%>
                                                                    <br />
                                                                    <br />

                                                                   
                                                                     <div class="col-md-12 table-responsive uppercase" style="overflow:auto;">
                                          
                                                                    <br />
                                                 </div> 
                                                                    <br />
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-md-12 col-sm-2" style="text-align: center">
                                                                            <div class="body collapse in">
                                                                                <asp:Button ID="Button7" runat="server" class="btn btn-danger" Text="Simpan" Type="submit"
                                                                                   OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;" onclick="Button7_Click" 
                                                                                    /> 

                                                                                 <asp:Button ID="Button17" runat="server" class="btn btn-danger" Text="Kemaskini" Type="submit"
                                                                                   OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;" onclick="Button17_Click" 
                                                                                    /> 
                                                                                   <asp:Button ID="Button18" runat="server" Visible="false" class="btn btn-danger" Text="Print" 
                                                                                    Type="submit" onclick="pv_cetak"/>
                                                                                    <asp:Button ID="Button9" runat="server" class="btn btn-danger" Text="Tutup" 
                                                                                    Type="submit" onclick="Button9_Click"/>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                                                                                    
                                                              
                                                        
                                                                </div>
                                                                    <br />
  
                                                                    <br />
                                                                   </div>

</div>

                                                                </div>
                                                                      <div role="tabpanel2" class="tab-pane" runat="server" id="p2">
                                                                    <div id="th1" runat="server">
                                                                          <fieldset class="col-md-12">
                                                                     <legend>Filter</legend>
                                                                     <br />
                                                                             <div class="row">
                                                                  
                                                                        <div class="col-md-3 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                    C/N No :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                                 <asp:TextBox ID="txtcno" runat="server" class="form-control validate[optional]"
                                                                                   ></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                   
                                                                        <div class="col-md-3">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                    Tarikh :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-4">
                                                                                <div class="input-group">
                                                                                    <span class="input-group-addon"><i class="icon-calendar"></i></span>
                                                                                 <asp:TextBox ID="txtctarikh" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                                        placeholder="DD/MM/YYYY"></asp:TextBox>

                                                                                </div>
                                                                               
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-3 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                    Nama Pembekal :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                                <asp:TextBox style="text-align:right;" ID="txtnpembe" runat="server" class="form-control validate[optional,custom[number]] au_amt"  onblur="addTotal_bk1(this)"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                         <div class="col-md-3 col-sm-2">
                                                                            <div class="col-md-7 col-sm-1">
                                                                                  <asp:CheckBox ID="chk_kredit" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                 <asp:Button ID="Button12" runat="server" class="btn btn-danger" Text="Carian" 
                                                                    UseSubmitBehavior="false" onclick="Button12_Click"    />
                                                                                <asp:Button ID="tab3tam" runat="server" class="btn btn-danger" Text="Tambah+" Type="submit"
                                                                                  onclick="tab3tam_Click" 
                                                                                    />
                                                                            </div>
                                                                          
                                                                        </div>
                                                                    </div>
                                                                    </fieldset>
                                                                  <br />
                                                                  <br />
                                                                
                                                                                 
                                                                        <asp:gridview ID="Gridview6" runat="server" Font-Size="12px"  CssClass="table datatable dataTable no-footer uppercase"  AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting" OnPageIndexChanging="gvSelected_PageIndexChanging_t4" >
            <Columns>
           <asp:TemplateField HeaderText="BIL">  
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="C/N No" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                  <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("no_Rujukan") %>'  CommandArgument='<%# Eval("no_Rujukan")%>' CommandName="Add"  onclick="lblSubItemcredit_Click" Font-Bold Font-Underline ></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:BoundField DataField="tarikh_invois" HeaderText="Tarikh" ItemStyle-HorizontalAlign="Center" />
              <asp:BoundField DataField="Ref_nama_syarikat" HeaderText="Nama Pembekal" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="jumlah" HeaderText="Jumlah (RM)" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                
             
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="White" />
        </asp:gridview>
                                                                 
                                                                  </div>
                                                                   <div id="th2" runat="server">
                                                                    <div class="row">

                                                                         <div class="col-md-4">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                    Nama Pembekal (Kredit) :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-4">
                                                                              
                                                                                          <asp:DropDownList ID="ddpela2" runat="server" class="form-control uppercase" OnSelectedIndexChanged="ddpela2_SelectedIndexChanged" AutoPostBack="true">
                                                                                </asp:DropDownList>
                                                                               
                                                                            </div>
                                                                        </div>

                                                                         <div class="col-md-4">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                   No Invois :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-4">
                                                                              
                                                                                          <asp:DropDownList ID="ddinv" runat="server" class="form-control uppercase" OnSelectedIndexChanged="ddinv_SelectedIndexChanged" AutoPostBack="true">
                                                                                </asp:DropDownList>
                                                                               
                                                                            </div>
                                                                        </div>

                                                                         
                                                                        <div class="col-md-4 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                    Tarikh Invois  :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                             <div class="input-group">
                                                                                    <span class="input-group-addon"><i class="icon-calendar"></i></span>
                                                                                 <asp:TextBox ID="txttcinvois" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                                        placeholder="DD/MM/YYYY"></asp:TextBox>

                                                                                </div>
                                                                            
                                                                            </div>

                                                                    </div>
                                                                        </div>
                                                                    <br />
                                                                       <br />
                                                                        <br />
                                                                        <div class="row">
                                                                         <div class="col-md-4" id="kredit_tab4" runat="server" visible="false">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                    Nombor Rujukan :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-4">
                                                                                   <asp:TextBox ID="txtnoruju" runat="server" class="form-control uppercase"></asp:TextBox>
                                                                               <asp:TextBox ID="txtnoruju_1" runat="server" Visible="false" class="form-control" ></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        
                                                                        <div class="col-md-4 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                    Tarikh Transaksi  :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                             <div class="input-group">
                                                                                    <span class="input-group-addon"><i class="icon-calendar"></i></span>
                                                                                 <asp:TextBox ID="txttcre" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                                        placeholder="DD/MM/YYYY"></asp:TextBox>

                                                                                </div>
                                                                            
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-4 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                    Projek :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               <asp:DropDownList ID="ddpro1" runat="server" class="form-control uppercase">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>

                                                                       
                                                                    </div>
                                                                    <br />
                                                                       <br />

                                                                          <br />
                                                                        <div class="row">
                                                                       
                                                                            
                                                                        <div class="col-md-4 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                    Invois Days :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               <asp:DropDownList ID="ddinvday" runat="server" class="form-control">
                                                                                    <asp:ListItem>--- PILIH ---</asp:ListItem>
                                                                                 <asp:ListItem>30</asp:ListItem>
                                                                                  <asp:ListItem>60</asp:ListItem>
                                                                                  <asp:ListItem>90</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                       <br />
                                                                        <br />
                                                                       <br />
                                                                    <div class="panel" style="width: 100%;">
                                                        <div id="Div6" role="tabpanel2" runat="server">
                                                            <!-- Nav tabs -->
                                                            <ul class="s1 nav nav-tabs" role="tablist">
                                                            <li id="pp11" runat="server" class="active"><a href="#ContentPlaceHolder1_p63" aria-controls="p63" role="tab" data-toggle="tab">Butiran </a>
                                                               
                                                            </li>
                                                               
                                                               
                                                            </ul>
                                                            <!-- Tab panes -->
                                                            <div class="tab-content" style="padding-top: 20px">
                                                          
                                                            
                                                            <div role="tabpanel2" class="tab-pane active" runat="server" id="p63">
                                                                    <div class="chat-panel panel panel-primary">
                                                                      
                                                                    </div>
                                                                    <br />
                                                                     <div id="Div7"  runat="server">
                                                                                <div class="col-md-12 table-responsive uppercase" style="overflow:auto;">
                                          

             
<asp:gridview ID="Gridview9" runat="server"  CssClass="table datatable dataTable no-footer uppercase" Font-Size="12px" ShowFooter="True"  AutoGenerateColumns="False" CellPadding="4"  ForeColor="#333333" GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting" onrowdatabound="Gridview3_RowDataBound"   >
            <Columns>
             <asp:TemplateField HeaderText="Kod Akaun" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                 <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("kod_akauan") %>' Visible = "false" />
                    <asp:Label ID="ddkoddup" runat="server" Text='<%# Eval("nama_akaun") %>' />
                     <%--<asp:DropDownList ID="ddkoddup" runat="server" Width="150px" class="form-control uppercase">    </asp:DropDownList>--%>
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Rujukan" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                 <asp:Label ID="lblruj" runat="server" Text='<%# Eval("rujukan") %>'  />
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Item" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                 <asp:Label ID="lblitem" runat="server" Text='<%# Eval("item") %>'  />
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Keterangan" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                 <asp:Label ID="lblket" runat="server" Text='<%# Eval("keterangan") %>'  />
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Harga/unit" ItemStyle-HorizontalAlign="Right" >
                <ItemTemplate>
                 <asp:Label ID="lblunit" runat="server" Text='<%# Eval("unit","{0:n}") %>'  />
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Kuantiti" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                 <asp:Label ID="lblqty" runat="server" Text='<%# Eval("quantiti") %>'  />
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Disk" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lbldis" runat="server" Text='<%# Eval("discount","{0:n}") %>'  />
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Jumlah tidak termasuk CBP(RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lbljum" runat="server" Text='<%# Eval("jumlah","{0:n}") %>'  />
                </ItemTemplate>
            </asp:TemplateField>  
             <asp:TemplateField HeaderText="Tax" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# bool.Parse(Eval("tax").ToString()) %>' Enable='<%# !bool.Parse(Eval("tax").ToString()) %>' />
                </ItemTemplate>
            </asp:TemplateField>
           <asp:TemplateField HeaderText="Caj Perkhidmatan (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                 <asp:Label ID="lblgstoth" runat="server" Text='<%# Eval("othgsttype") %>' Visible = "false" />
                    <asp:Label ID="ddtaxoth" runat="server" Text='<%# Eval("othgstname") %>' />
                     <%--<asp:DropDownList ID="ddtaxoth" runat="server" class="form-control uppercase">    </asp:DropDownList>--%>
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Caj Perkhidmatan (RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lblothgst" runat="server" Text='<%# Eval("othgst","{0:n}") %>'  />
                </ItemTemplate>
            </asp:TemplateField>  
                
             <asp:TemplateField HeaderText="Gst (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                 <asp:Label ID="lblgst" runat="server" Text='<%# Eval("gsttype") %>' Visible = "false" />
                     <asp:Label ID="ddtax" runat="server" Text='<%# Eval("gstname") %>' />
                     <%--<asp:DropDownList ID="ddtax" runat="server" class="form-control uppercase">    </asp:DropDownList>--%>
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Gst (RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lblgstjum" runat="server" Text='<%# Eval("gstjum","{0:n}") %>'  />
                </ItemTemplate>
            </asp:TemplateField>  
                 <asp:TemplateField HeaderText="Jumlah Termasuk CBP(RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lblover" runat="server" Text='<%# Eval("Overall","{0:n}") %>'  />
                </ItemTemplate>
            </asp:TemplateField>
               
               

            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="White" />                                                                 
           
        </asp:gridview>

  <asp:gridview ID="Gridview10" runat="server"  CssClass="table datatable dataTable no-footer" Font-Size="12px" ShowFooter="True"  AutoGenerateColumns="False" CellPadding="4"  ForeColor="#333333" GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting" onrowdatabound="Gridview9_RowDataBound" >
                 <Columns>
           
             <asp:TemplateField HeaderText="Kod Akaun (Debit)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                     <asp:DropDownList ID="ddkodcre" runat="server" class="form-control uppercase" Width="100px">    </asp:DropDownList>
                </ItemTemplate>
                 <FooterTemplate>
                 <asp:Button ID="ButtonAdd" runat="server" Text="Add New Row" 
                        onclick="ButtonAddcre_Click" />
                </FooterTemplate>
            </asp:TemplateField>
                    
            <asp:TemplateField HeaderText="Keterangan" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="txtket" CssClass="form-control uppercase" runat="server" TextMode="MultiLine" Width="180px" Height="40px"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
                     
                       <asp:TemplateField HeaderText="Jumlah tidak termasuk CBP(RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                     <asp:TextBox ID="Txtdis" runat="server"  CssClass="form-control"  placeholder="0.00"   OnTextChanged="QtyChanged1" AutoPostBack="true"   style="text-align:right;"    ></asp:TextBox>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label3" CssClass="form-control"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Tax" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="Chkcre" runat="server"  OnCheckedChanged="ChckedChangedcre"  AutoPostBack="true"  />
                </ItemTemplate>
            </asp:TemplateField>
                       <asp:TemplateField HeaderText="Caj Perkhidmatan (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:DropDownList ID="ddkodgstoth" runat="server" class="form-control uppercase"  onselectedindexchanged="ddkodgstoth_SelectedIndexChanged" AutoPostBack="true"  >    </asp:DropDownList>
                </ItemTemplate>
                
            </asp:TemplateField>
                      <asp:TemplateField HeaderText="Caj Perkhidmatan (RM)" ItemStyle-HorizontalAlign="Right" >
                <ItemTemplate>
                    <asp:Label ID="Label10" CssClass="form-control" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label11" CssClass="form-control"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="GST (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:DropDownList ID="ddkodgst" runat="server" class="form-control uppercase"  onselectedindexchanged="ddkodgst_SelectedIndexChanged" AutoPostBack="true" >    </asp:DropDownList>
                </ItemTemplate>
               
            </asp:TemplateField>
                       <asp:TemplateField HeaderText="GST Amaun (RM)" ItemStyle-HorizontalAlign="Right" >
                <ItemTemplate>
                    <asp:Label ID="Label8" CssClass="form-control" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label9" CssClass="form-control"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
                      <asp:TemplateField HeaderText="Jumlah Termasuk CBP(RM)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label5" CssClass="form-control" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label6" CssClass="form-control"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="White" />                                                                   
        </asp:gridview>

                                                                                              
<asp:gridview ID="Gridview12" runat="server"  CssClass="table datatable dataTable no-footer uppercase" Font-Size="12px" ShowFooter="True"  AutoGenerateColumns="False" CellPadding="4"  ForeColor="#333333" GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting" onrowdatabound="Gridview3_RowDataBound" >
            <Columns>
             <asp:TemplateField HeaderText="Kod Akaun" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                 <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("kod_akauan") %>' Visible = "false" />
                    <asp:Label ID="ddkoddup" runat="server" Text='<%# Eval("nama_akaun") %>' />
                     <%--<asp:DropDownList ID="ddkoddup" runat="server" Width="150px" class="form-control uppercase">    </asp:DropDownList>--%>
                </ItemTemplate>
            </asp:TemplateField>
               
           
                 <asp:TemplateField HeaderText="Keterangan" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                 <asp:Label ID="lblket" runat="server" Text='<%# Eval("keterangan") %>'  />
                </ItemTemplate>
            </asp:TemplateField>
               
            
              <asp:TemplateField HeaderText="Jumlah tidak termasuk CBP(RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lbljum" runat="server" Text='<%# Eval("jumlah") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>  
             <asp:TemplateField HeaderText="Tax" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# bool.Parse(Eval("tax").ToString()) %>' Enable='<%# !bool.Parse(Eval("tax").ToString()) %>' />
                </ItemTemplate>
            </asp:TemplateField>
           <asp:TemplateField HeaderText="Caj Perkhidmatan (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                 <asp:Label ID="lblgstoth" runat="server" Text='<%# Eval("othgsttype") %>' Visible="false" />
                    <asp:Label ID="ddtaxoth" runat="server" Text='<%# Eval("othgstname") %>' />
                     <%--<asp:DropDownList ID="ddtaxoth" runat="server" class="form-control uppercase">    </asp:DropDownList>--%>
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Caj Perkhidmatan (RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lblothgst" runat="server" Text='<%# Eval("othgstjumlah") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>  
                
             <asp:TemplateField HeaderText="Gst (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                 <asp:Label ID="lblgst" runat="server" Text='<%# Eval("gsttype") %>'  />
                     <asp:Label ID="ddtax" runat="server" Text='<%# Eval("gstname") %>' />
                     <%--<asp:DropDownList ID="ddtax" runat="server" class="form-control uppercase">    </asp:DropDownList>--%>
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Gst (RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lblgstjum" runat="server" Text='<%# Eval("gstjumlah") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>  
                 <asp:TemplateField HeaderText="Jumlah Termasuk CBP(RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lblover" runat="server" Text='<%# Eval("Overall") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>
               
            

            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="White" />                                                                 
           
        </asp:gridview>
        </div> 
                                                 <br />
                                                 <br />
               <div class="row">
                                                                    
                                                                        <div class="col-md-3 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                   
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               
                                                                                  
                                                                            </div>
                                                                        </div>
                                                                         <div class="col-md-3 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                  
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               
                                                                                  
                                                                            </div>
                                                                        </div>
                                                                          
                                                                        <div class="col-md-6 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                 Jumlah Keseluruhan  <span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                            <asp:TextBox ID="TextBox18" runat="server" class="form-control" style="text-align:right;"></asp:TextBox> 
                                                                            </div>
                                                                       
                                                                        </div>
                                                                    </div>
                                                      <br />
                                                      <br />
                                                                   
                                                                    <br />
                                                                    <br /><br />
                                                                      <div class="row">
                                                                        <div class="col-md-12 col-sm-2" style="text-align: center">
                                                                            <div class="body collapse in">
                                                                                <asp:Button ID="Button15" runat="server" class="btn btn-danger" Text="Simpan" Type="submit"
                                                                                    OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;" onclick="Button15_Click" 
                                                                                    />
                                                                                <asp:Button ID="Button22" runat="server" class="btn btn-danger" Text="Print" Type="submit"
                                                                                    onclick="Button22_Click" 
                                                                                    />

                                                                                <asp:Button ID="Button16" runat="server" Text="Tutup" class="btn btn-danger" UseSubmitBehavior="false"
                                                                                     onclick="Button16_Click"  />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                      <br />
                                                             
                                                                    <br />
                                                                  
                                                                </div>
                                                                <br />
                                                                <br />
                                                                <br />                                                               
                                                                </div>
                                                               
                                                                   </div>
                                                                </div>

                                                             
                                                               
                                                        </div>
                                                                      <br />
                                                                    <br />
                                                                   
                                                                            <br />

                                                       
                                                                   </div>
                                                                   </div>
                                                                    <div role="tabpanel3" class="tab-pane" runat="server" id="p3">
                                                                   
                                                                    <div id="fr1" runat="server">
                                                                   
                                                                     <fieldset class="col-md-12">
                                                                     <legend>Filter</legend>
                                                                     <br />
                                                                             <div class="row">
                                                                  
                                                                        <div class="col-md-3 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                    D/N No :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                                 <asp:TextBox ID="TextBox12" runat="server" class="form-control validate[optional]"
                                                                                   ></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                   
                                                                        <div class="col-md-3">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                    Tarikh :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-4">
                                                                                <div class="input-group">
                                                                                <span class="input-group-addon"><i class="icon-calendar"></i></span>
                                                                                <asp:TextBox ID="TextBox20" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                                        placeholder="DD/MM/YYYY"></asp:TextBox> 
                                                                                        
                                                                                        </div>
                                                                              
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-3 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                    Nama Pembekal :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                                <asp:TextBox style="text-align:right;" ID="TextBox21" runat="server" class="form-control validate[optional,custom[number]] au_amt"  onblur="addTotal_bk1(this)"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                         <div class="col-md-3 col-sm-2">
                                                                            <div class="col-md-7 col-sm-1">
                                                                                <asp:CheckBox ID="chk_debit" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                 <asp:Button ID="Button13" runat="server" class="btn btn-danger" Text="Carian" 
                                                                    UseSubmitBehavior="false" onclick="Button13_Click"    />
                                                                               <asp:Button ID="Button10" runat="server" class="btn btn-danger" Text="Tambah+" Type="submit"
                                                                                  onclick="Button10_Click" 
                                                                                    />
                                                                            </div>
                                                                          
                                                                        </div>
                                                                    </div>
                                                                 
                                                                    </fieldset>
                                                                  <br />
                                                                  <br />
                                                                
                                                                              
                                                                        <asp:gridview ID="Gridview8" runat="server" Font-Size="12px"  CssClass="table datatable dataTable no-footer uppercase" ShowFooter="false"  AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting" PageSize="25" AllowPaging="true" OnPageIndexChanging="gvSelected_PageIndexChanging_t5">
            <Columns>
           <asp:TemplateField HeaderText="BIL">  
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="D/N No" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                  <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("no_Rujukan") %>'  CommandArgument='<%# Eval("no_Rujukan")%>' CommandName="Add"  onclick="lblSubItemdebit_Click" Font-Bold Font-Underline ></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:BoundField DataField="tarikh_invois" HeaderText="Tarikh" ItemStyle-HorizontalAlign="Center" />
              <asp:BoundField DataField="Ref_nama_syarikat" HeaderText="Nama Pelanggan" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="jumlah" HeaderText="Jumlah (RM)" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                
             
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="White" />
        </asp:gridview>
                                                                                                                                   
                                                                   
                                                                    <br />
                                                                  
                                                                   
                                                                    <br />
                                                                    </div>
                                                                     <div id="fr2" runat="server">
                                                                      <div class="row">

                                                                           <div class="col-md-4">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                    Nama Pelanggan (Debit) :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-4">

                                                                                          <asp:DropDownList ID="ddpela3" runat="server" class="form-control uppercase" OnSelectedIndexChanged="ddpela3_SelectedIndexChanged" AutoPostBack="true">
                                                                                </asp:DropDownList>
                                                                               
                                                                            </div>
                                                                        </div>


                                                                           <div class="col-md-4">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                   No Invois :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-4">
                                                                              
                                                                                          <asp:DropDownList ID="ddinv2" runat="server" class="form-control uppercase" OnSelectedIndexChanged="ddinv2_SelectedIndexChanged" AutoPostBack="true">
                                                                                </asp:DropDownList>
                                                                               
                                                                            </div>
                                                                        </div>
                                                                           <div class="col-md-4 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                    Tarikh Invois :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                            <div class="input-group">
                                                                                    <span class="input-group-addon"><i class="icon-calendar"></i></span>
                                                                                 <asp:TextBox ID="txtdtinvois" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                                        placeholder="DD/MM/YYYY"></asp:TextBox>

                                                                                </div>
                                                                             
                                                                            </div>
                                                                        </div>
                                                                       
                                                                        
                                                                    </div>
                                                                        <br />
                                                                        <br />
                                                                        <div class="row">
                                                                              <div class="col-md-4" id="debit_tab5" runat="server" visible="false">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                    Nombor Rujukan :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-4">
                                                                                  <asp:TextBox ID="txtnoruj2" runat="server" class="form-control uppercase"></asp:TextBox>
                                                                                     <asp:TextBox ID="txtnoruj2_2" runat="server" Visible="false" class="form-control "></asp:TextBox>
                                                                               
                                                                            </div>
                                                                        </div>

                                                                            <div class="col-md-4 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                    Tarikh Transaksi :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                            <div class="input-group">
                                                                                    <span class="input-group-addon"><i class="icon-calendar"></i></span>
                                                                                 <asp:TextBox ID="txtdeb" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass"
                                                                                        placeholder="DD/MM/YYYY"></asp:TextBox>

                                                                                </div>
                                                                             
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-4 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                    Projek :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               <asp:DropDownList ID="ddpro2" runat="server" class="form-control uppercase">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                            
                                                                        </div>
                                                                    <br />
                                                                       <br />
                                                                          <div class="row">
                                                                            
                                                                             <div class="col-md-4 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                    Invois Days : 
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               <asp:DropDownList ID="ddinvday2" runat="server" class="form-control">
                                                                                      <asp:ListItem>--- PILIH ---</asp:ListItem>
                                                                                 <asp:ListItem>30</asp:ListItem>
                                                                                  <asp:ListItem>60</asp:ListItem>
                                                                                  <asp:ListItem>90</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>

                                                                        </div>
                                                                    <br />
                                                                       <br />

                                                                       <br />
                                                                    
                                                           <div class="panel" style="width: 100%;">
                                                        <div id="Div8" role="tabpanel3" runat="server">
                                                            <!-- Nav tabs -->
                                                            <ul class="s1 nav nav-tabs" role="tablist">
                                                            <li id="pp14" runat="server" class="active"><a href="#ContentPlaceHolder1_p67" aria-controls="p67" role="tab" data-toggle="tab">Butiran </a>
                                                               
                                                            </li>
                                                               <%-- <li id="pp15" runat="server"><a href="#ContentPlaceHolder1_p68" aria-controls="p68" role="tab" data-toggle="tab">Knock Off</a></li>--%>
                                                                <%--<li id="pp2" runat="server"><a href="#ContentPlaceHolder1_p2" aria-controls="p2" role="tab" data-toggle="tab">ELAUN TETAP</a></li>
                                                                <li id="pp3" runat="server"><a href="#ContentPlaceHolder1_p3" aria-controls="p3" role="tab" data-toggle="tab">LAIN-LAIN ELAUN</a></li>
                                                                <li id="pp4" runat="server"><a href="#ContentPlaceHolder1_p4" aria-controls="p4" role="tab" data-toggle="tab">KERJA LEBIH MASA</a></li>
                                                                <li id="pp5" runat="server"><a href="#ContentPlaceHolder1_p5" aria-controls="p5" role="tab" data-toggle="tab">BONUS</a></li>--%>
                                                                
                                                            </ul>
                                                            <!-- Tab panes -->
                                                            <div class="tab-content" style="padding-top: 20px">
                                                          
                                                            
                                                            <div role="tabpanel3" class="tab-pane active" runat="server" id="p67">
                                                                    <div class="chat-panel panel panel-primary">
                                                                      
                                                                    </div>
                                                                    <br />
                                                                     <div id="Div9"  runat="server">
                                                                                <div class="col-md-12 table-responsive uppercase" style="overflow:auto;">
                               
<asp:gridview ID="Gridview7" runat="server"  CssClass="table datatable dataTable no-footer uppercase" Font-Size="12px" ShowFooter="True"  AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting" onrowdatabound="Gridview3_RowDataBound" >
            <Columns>
             <asp:TemplateField HeaderText="Kod Akaun" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                 <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("kod_akauan") %>' Visible = "false" />
                    <asp:Label ID="ddkoddup" runat="server" Text='<%# Eval("nama_akaun") %>' />
                     <%--<asp:DropDownList ID="ddkoddup" runat="server" Width="150px" class="form-control uppercase">    </asp:DropDownList>--%>
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Rujukan" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                 <asp:Label ID="lblruj" runat="server" Text='<%# Eval("rujukan") %>'  />
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Item" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                 <asp:Label ID="lblitem" runat="server" Text='<%# Eval("item") %>'  />
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Keterangan" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                 <asp:Label ID="lblket" runat="server" Text='<%# Eval("keterangan") %>'  />
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Harga/unit" ItemStyle-HorizontalAlign="Right" >
                <ItemTemplate>
                 <asp:Label ID="lblunit" runat="server" Text='<%# Eval("unit","{0:n}") %>' />
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Kuantiti" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                 <asp:Label ID="lblqty" runat="server" Text='<%# Eval("quantiti") %>'  />
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Disk" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lbldis" runat="server" Text='<%# Eval("discount","{0:n}") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Jumlah tidak termasuk CBP (RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lbljum" runat="server" Text='<%# Eval("jumlah","{0:n}") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>  
             <asp:TemplateField HeaderText="Tax" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# bool.Parse(Eval("tax").ToString()) %>' Enable='<%# !bool.Parse(Eval("tax").ToString()) %>' />
                </ItemTemplate>
            </asp:TemplateField>
           <asp:TemplateField HeaderText="Caj Perkhidmatan (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                 <asp:Label ID="lblgstoth" runat="server" Text='<%# Eval("othgsttype") %>' Visible = "false" />
                    <asp:Label ID="ddtaxoth" runat="server" Text='<%# Eval("othgstname") %>' />
                     <%--<asp:DropDownList ID="ddtaxoth" runat="server" class="form-control uppercase">    </asp:DropDownList>--%>
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Caj Perkhidmatan (RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lblothgst" runat="server" Text='<%# Eval("othgst","{0:n}") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>  
                
             <asp:TemplateField HeaderText="Gst (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                 <asp:Label ID="lblgst" runat="server" Text='<%# Eval("gsttype") %>' Visible = "false" />
                     <asp:Label ID="ddtax" runat="server" Text='<%# Eval("gstname") %>' />
                     <%--<asp:DropDownList ID="ddtax" runat="server" class="form-control uppercase">    </asp:DropDownList>--%>
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Gst (RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lblgstjum" runat="server" Text='<%# Eval("gstjum","{0:n}") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>  
                 <asp:TemplateField HeaderText="Jumlah Termasuk CBP(RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lblover" runat="server" Text='<%# Eval("Overall","{0:n}") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>
               
                
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="White" />                                                                 
           
        </asp:gridview>


            <asp:gridview ID="Gridview11" runat="server"  CssClass="table datatable dataTable no-footer" Font-Size="12px" ShowFooter="True"  AutoGenerateColumns="False" CellPadding="4"  ForeColor="#333333" GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting" onrowdatabound="Gridview7_RowDataBound" >
                 <Columns>
           
             <asp:TemplateField HeaderText="Kod Akaun (Kredit)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                     <asp:DropDownList ID="ddkoddeb" runat="server" class="form-control uppercase" Width="100px">    </asp:DropDownList>
                </ItemTemplate>
                 <FooterTemplate>
                 <asp:Button ID="ButtonAdd" runat="server" Text="Add New Row" 
                        onclick="ButtonAdddeb_Click" />
                </FooterTemplate>
            </asp:TemplateField>
                    
            <asp:TemplateField HeaderText="Keterangan" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="txtket" CssClass="form-control uppercase" runat="server" TextMode="MultiLine" Width="180px" Height="40px"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
                     
                       <asp:TemplateField HeaderText="Jumlah tidak termasuk CBP(RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                     <asp:TextBox ID="Txtdis" runat="server"  CssClass="form-control"  placeholder="0.00"   OnTextChanged="QtyChangeddeb" AutoPostBack="true"   style="text-align:right;"    ></asp:TextBox>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label3" CssClass="form-control"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Tax" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="Chkdeb" runat="server"  OnCheckedChanged="ChckedChangeddeb"  AutoPostBack="true"  />
                </ItemTemplate>
            </asp:TemplateField>
                       <asp:TemplateField HeaderText="Caj Perkhidmatan (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:DropDownList ID="ddgstdeboth" runat="server" class="form-control uppercase"  onselectedindexchanged="ddgstdeboth_SelectedIndexChanged" AutoPostBack="true"  >    </asp:DropDownList>
                </ItemTemplate>
                
            </asp:TemplateField>
                      <asp:TemplateField HeaderText="Caj Perkhidmatan (RM)" ItemStyle-HorizontalAlign="Right" >
                <ItemTemplate>
                    <asp:Label ID="Label10" CssClass="form-control" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label11" CssClass="form-control"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="GST (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:DropDownList ID="ddgstdeb" runat="server" class="form-control uppercase"  onselectedindexchanged="ddgstdeb_SelectedIndexChanged" AutoPostBack="true" >    </asp:DropDownList>
                </ItemTemplate>
               
            </asp:TemplateField>
                       <asp:TemplateField HeaderText="GST Amt" ItemStyle-HorizontalAlign="Right" >
                <ItemTemplate>
                    <asp:Label ID="Label8" CssClass="form-control" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label9" CssClass="form-control"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
                      <asp:TemplateField HeaderText="Jumlah Termasuk CBP(RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="Label5" CssClass="form-control" runat="server" Text="0.00"  ></asp:Label>
                </ItemTemplate>
                 <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                  <asp:Label ID="Label6" CssClass="form-control"  runat="server" Text="0.00"  ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="White" />                                                                   
        </asp:gridview>
            



                                                 </div> 
                                                    <br />
                                                      <br />
            <asp:gridview ID="Gridview14" runat="server"  CssClass="table datatable dataTable no-footer uppercase" Font-Size="12px" ShowFooter="True"  AutoGenerateColumns="False" CellPadding="4"  ForeColor="#333333" GridLines="None"  onrowdatabound="Gridview14_RowDataBound" >
         <Columns>
             <asp:TemplateField HeaderText="Kod Akaun" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                 <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("kod_akauan") %>' Visible = "false" />
                    <asp:Label ID="ddkoddup" runat="server" Text='<%# Eval("nama_akaun") %>' />
                     <%--<asp:DropDownList ID="ddkoddup" runat="server" Width="150px" class="form-control uppercase">    </asp:DropDownList>--%>
                </ItemTemplate>
            </asp:TemplateField>
               
                 <asp:TemplateField HeaderText="Keterangan" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                 <asp:Label ID="lblket" runat="server" Text='<%# Eval("keterangan") %>'  />
                </ItemTemplate>
            </asp:TemplateField>
         
              <asp:TemplateField HeaderText="Jumlah tidak termasuk CBP(RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lbljum" runat="server" Text='<%# Eval("jumlah","{0:n}") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>  
             <asp:TemplateField HeaderText="Tax" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# bool.Parse(Eval("tax").ToString()) %>' Enable='<%# !bool.Parse(Eval("tax").ToString()) %>' />
                </ItemTemplate>
            </asp:TemplateField>
           <asp:TemplateField HeaderText="Caj Perkhidmatan (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                 <asp:Label ID="lblgstoth" runat="server" Text='<%# Eval("othgsttype") %>' Visible = "false" />
                    <asp:Label ID="ddtaxoth" runat="server" Text='<%# Eval("othgstname") %>' />
                     <%--<asp:DropDownList ID="ddtaxoth" runat="server" class="form-control uppercase">    </asp:DropDownList>--%>
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Caj Perkhidmatan (RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lblothgst" runat="server" Text='<%# Eval("othgst","{0:n}") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>  
                
             <asp:TemplateField HeaderText="Gst (%)" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                 <asp:Label ID="lblgst" runat="server" Text='<%# Eval("gsttype") %>' Visible = "false" />
                     <asp:Label ID="ddtax" runat="server" Text='<%# Eval("gstname") %>' />
                     <%--<asp:DropDownList ID="ddtax" runat="server" class="form-control uppercase">    </asp:DropDownList>--%>
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Gst (RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lblgstjum" runat="server" Text='<%# Eval("gstjum","{0:n}") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>  
                 <asp:TemplateField HeaderText="Jumlah Termasuk CBP(RM)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                 <asp:Label ID="lblover" runat="server" Text='<%# Eval("Overall","{0:n}") %>' DataFormatString="{0:n}"  />
                </ItemTemplate>
            </asp:TemplateField>
              

            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="White" />                                                                 
        </asp:gridview>
                                                      <br />
                                                      <br />
                                                                   <div class="row">
                                                                    
                                                                        <div class="col-md-3 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                   
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               
                                                                                  
                                                                            </div>
                                                                        </div>
                                                                         <div class="col-md-3 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                  
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               
                                                                                  
                                                                            </div>
                                                                        </div>
                                                                          
                                                                        <div class="col-md-6 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                 Jumlah Keseluruhan  <span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                            <asp:TextBox ID="TextBox1" runat="server" class="form-control" style="text-align:right;"></asp:TextBox> 
                                                                            </div>
                                                                       
                                                                        </div>
                                                                    </div>
                                                      <br />
                                                      <br />    
                                                                  
                                                                    <br /><br />
                                                                      <div class="row">
                                                                        <div class="col-md-12 col-sm-2" style="text-align: center">
                                                                            <div class="body collapse in">
                                                                                <asp:Button ID="Button5" runat="server" class="btn btn-danger" Text="Simpan" Type="submit"
                                                                                    OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;" 
                                                                                    onclick="simtab4_Click" />

                                                                                     <asp:Button ID="Button21" Visible=false runat="server" class="btn btn-danger" Text="Print" Type="submit"  onclick="Button21_Click"   />
                                                                                       
                                                                                <asp:Button ID="Button14" runat="server" Text="Tutup" class="btn btn-danger" UseSubmitBehavior="false"
                                                                                   onclick="Button14_Click"  />
                                                                            </div>
                                                                        </div>
                                                                  
                                                                  
                                                                  
                                                                    </div>
                                                                      <br />
                                                             
                                                                    <br />
                                                                  </div>
                                                                </div>
                                                              
                                                                <br />
                                                                <br />
                                                              
                                                                    <br />
                                                                  
                                                                    
                                                                                                                                    
                                                              
                                                                <div role="tabpanel3" class="tab-pane" runat="server" id="p68">
                                                                    <div class="chat-panel panel panel-primary">
                                                                        
                                                                    </div>
                                                                    <br />
                                                                    <br />
                                                                    <br />
                                                                    
                                                                    <br />
                                                                  <br />
                                                                  <div class="col-md-12 table-responsive uppercase" style="overflow:auto;">

                                                                               <div class="row">
                                                                    
                                                                        <div class="col-md-3 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                   
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               
                                                                                  
                                                                            </div>
                                                                        </div>
                                                                         <div class="col-md-3 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                  
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                               
                                                                                  
                                                                            </div>
                                                                        </div>
                                                                          
                                                                        <div class="col-md-6 col-sm-2">
                                                                            <div class="col-md-5 col-sm-1">
                                                                                <label>
                                                                                Balance  <span style="color: Red">*</span> :
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-7 col-sm-2">
                                                                            <asp:TextBox ID="TextBox28" runat="server" class="form-control"></asp:TextBox> 
                                                                            </div>
                                                                       
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <br /> 

                                                                                  <div class="row">
                                                                       
                                                                      <br />
                                                             
                                                                    <br />                 
                                                                   </div>
                                                                  
                                                                   </div>
                                                                    <br />
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
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
            </ContentTemplate>
        </asp:UpdatePanel>

         
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

