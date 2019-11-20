<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Kewengan/kw_profil_syarikat.aspx.cs" Inherits="kw_profil_syarikat" %>
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
   <style>
       .ZebraDialog{
           z-index: 1000001;
       }
   </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

   
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1><asp:Label ID="ps_lbl1" runat="server"></asp:Label></h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i><asp:Label ID="ps_lbl2" runat="server"></asp:Label></a></li>
                            <li class="active"><asp:Label ID="ps_lbl3" runat="server"></asp:Label></li>
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
                            <h3 class="box-title"><asp:Label ID="ps_lbl4" runat="server"></asp:Label></h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                  <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl6" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txt_nombo" class="form-control validate[optional] uppercase" MaxLength="15" runat="server" AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl5" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txt_nama" class="form-control uppercase" runat="server" MaxLength="100" AutoComplete="off"></asp:TextBox>
                                         <asp:TextBox ID="txt_value" Visible="false" runat="server" class="form-control validate[optional] uppercase" MaxLength="99"></asp:TextBox>
                                    </div>
                                </div>
                               
                                 <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl7" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txt_teleph" runat="server" class="form-control validate[optional] uppercase" MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                                  <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl9" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txt_faxno" runat="server" class="form-control validate[optional] uppercase" MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                                  <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl11" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                        <asp:TextBox ID="txt_email" class="form-control validate[optional,custom[email]]" runat="server" placeholder="Email"></asp:TextBox>
                                           <span class="input-group-addon" > <i class="fa fa-envelope"></i></span>
                                            </div>
                                    </div>
                                </div>
                                 <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl16" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txt_alamat" class="form-control uppercase" runat="server" TextMode="MultiLine" MaxLength="1000"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputPassword3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl17" runat="server"></asp:Label></label>

                                    <div class="col-sm-8">
                                        <asp:FileUpload ID="FileUpload1" CssClass="fileupload" type="file" runat="server" onchange="ShowImagePreview(this);"/>
                                          <br />
                                        <asp:Image ID="ImgPrv" runat="server" class="profile-user-img img-responsive"  Width="150px" Height="150px" />
                                        <asp:TextBox ID="TextBox1" runat="server" class="form-control validate[optional] uppercase" Visible="false"></asp:TextBox>
                                        <br />
                                        <span style="color: Red;">(NOTA : <asp:Label ID="ps_lbl18" runat="server"></asp:Label>)</span>
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
                                 <asp:Button ID="Button4" runat="server" class="btn btn-danger" Text="Simpan" OnClick="Button4_Click" UseSubmitBehavior="false" />
                                <asp:Button ID="btb_kmes" runat="server" Text="Kemaskini" UseSubmitBehavior="false" class="btn btn-danger" OnClick="btb_kmes_Click" />
                                <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" UseSubmitBehavior="false" OnClick="Button5_Click" />
                                <asp:Button ID="Button2" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" OnClick="Click_bck" />
                                <asp:Button ID="Button3" runat="server" class="btn btn-success" Visible="false" Text="Maklumat Kewangan" OnClick="fin_year_new" UseSubmitBehavior="false" />
                            </div>
                           </div>
                               </div>
                           <div class="box-body">&nbsp;</div>
                              <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Kewangan Syarikat</h3>
                        </div>
                            <div class="box-body">&nbsp;</div>
                                <asp:gridview ID="Gridview2" runat="server"  class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="gvSelected_PageIndexChanging_g2" >
                                                                             <PagerStyle CssClass="pager" />
            <Columns>
                <asp:TemplateField HeaderText="BIL">  
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Tahun Kewangan" ItemStyle-HorizontalAlign="Center">
                       <ItemStyle HorizontalAlign="center" Font-Bold/>
                <ItemTemplate>
                  <asp:Label ID="kew_tahun" runat="server" Text='<%# Eval("fin_year") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                       <ItemStyle HorizontalAlign="center" Font-Bold/>
                <ItemTemplate>
                  <asp:Label ID="kew_status" runat="server" Text='<%# Eval("sts") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Tarikh Mula" ItemStyle-HorizontalAlign="Left">  
                    <ItemStyle HorizontalAlign="center" Font-Bold/>
                                            <ItemTemplate>  
                                                <asp:Label ID="kew_start_dt" runat="server" Text='<%# Eval("st_dt") %>'></asp:Label>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
               
                <asp:TemplateField HeaderText="Tarikh Akhir" ItemStyle-HorizontalAlign="Center">  
                    <ItemStyle HorizontalAlign="Center" Font-Bold />
                                            <ItemTemplate>  
                                                <asp:Label ID="kew_end_dt" Width="100%" runat="server" Text='<%# Eval("ed_dt") %>'></asp:Label>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                   <asp:TemplateField HeaderText="Bulan Tahun Kewangan" ItemStyle-HorizontalAlign="Center">  
                    <ItemStyle HorizontalAlign="Center" Font-Bold />
                                            <ItemTemplate>  
                                                <asp:Label ID="kew_bulan" Width="100%" runat="server" Text='<%# Eval("fin_months") %>'></asp:Label>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Transaksi Tempoh Kewangan" ItemStyle-HorizontalAlign="Center">  
                    <ItemStyle HorizontalAlign="Center" Font-Bold />
                                            <ItemTemplate>  
                                                <asp:Label ID="kew_tempoh" Width="100%" runat="server" Text='<%# Eval("fin_period") %>'></asp:Label>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Action">
                                                                <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                                                    <ItemTemplate>
                                                                         <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-success" ToolTip="Edit" CommandArgument='Edit' OnClick="fin_update"  Font-Bold>
                                                                                                    <i class='fa fa-pencil'></i> 
                                                                                                </asp:LinkButton>   
                                                                        
                                                                         <asp:LinkButton ID="LinkButton2" runat="server" class="btn btn-success" ToolTip="View" CommandArgument='View' OnClick="fin_open_balance" Font-Bold>
                                                                                                    View
                                                                                                </asp:LinkButton>                                                                      
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
 
              
            </Columns>
          <FooterStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                       <%-- <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />--%>
                                                        <HeaderStyle BackColor="#BDC4C7" Font-Bold="True" ForeColor="black" />
                                                        <AlternatingRowStyle BackColor="White" />
        </asp:gridview>
                            <cc1:ModalPopupExtender BackgroundCssClass="modalBg" DropShadow="true" ID="ModalPopupExtender1"
                                                                PopupControlID="Panel3" runat="server" TargetControlID="btnBack" PopupDragHandleControlID="Panel2"
                                                                CancelControlID="btnBack">
                                                            </cc1:ModalPopupExtender>
                                      <asp:Panel ID="Panel3" runat="server" CssClass="modalPanel" Style="display: none;  overflow-y:auto; height: 65vh;">
                                          <a class="popupCloseButton" id="btnBack" runat="server"></a>
                                                   <table border="0" cellpadding="6" style="width:100%" cellspacing="0" class="tblborder">
                                                                    <tr>
                                                                        <td colspan="3" align="left"><span class="leftpadding " style="font-weight:bold;">Maklumat Tahun Kewangan</td>                                                                       
                                                                        </tr>                                                       
                                                                    <tr id="tbl_rw1" runat="server">
                                                                        <td width="140px" align="left"><span class="leftpadding ">Tahun Kewangan <span class="style1">*</span></td>
                                                                        <td width="10%" align="center">:</td>
                                                                        <td width="270px" align="left">    <asp:TextBox id="fin_year" style="width:100%;" runat="server" class="form-control validate[optional]">
                                                       </asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    
                                                                     <tr id="tbl_rw2" runat="server">
                                                                        <td width="140px" align="left"><span class="leftpadding ">Status Kewangan <span class="style1">*</span></td>
                                                                        <td width="10%" align="center">:</td>
                                                                        <td width="270px" align="left">  <asp:DropDownList ID="dd_cursts" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                           <asp:ListItem Value="1">OPEN</asp:ListItem>
                                           <asp:ListItem Value="0">CLOSE</asp:ListItem>
                                           <asp:ListItem Value="2">TRIAL</asp:ListItem>
                                       </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    
                                                         <tr id="tbl_rw3" runat="server">
                                                                        <td width="140px" align="left"><span class="leftpadding ">Tarikh Mula <span class="style1">*</span></td>
                                                                        <td width="10%" align="center">:</td>
                                                                        <td width="270px" align="left">   <div class="input-group">
                                                        <asp:TextBox ID="txt_startdt" runat="server" class="form-control validate[optional] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
                                               <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                                        </td>
                                                                    </tr>
                                                         <tr>
                                                                        <td width="140px" align="left"><span class="leftpadding ">Tarikh Akhir <span class="style1">*</span></td>
                                                                        <td width="10%" align="center">:</td>
                                                                        <td width="270px" align="left">   <div class="input-group">
                                                         <asp:TextBox ID="txt_enddt" runat="server" class="form-control validate[optional] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
                                               <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                                        </td>
                                                                    </tr>
                                                        <tr>
                                                                        <td width="140px" align="left"><span class="leftpadding ">Bulan Tahun Kewangan</td>
                                                                        <td width="10%" align="center">:</td>
                                                                        <td width="270px" align="left">   
                                                                            <div class="input-group">
                                                                            <asp:TextBox ID="TextBox3" ReadOnly="true" class="form-control uppercase" runat="server"></asp:TextBox>
                                                                           <span class="input-group-addon"><asp:CheckBox ID="chk_tahun" runat="server" OnCheckedChanged="get_tahun_bulan" AutoPostBack="true" /></span>  
                                                                                </div>
                                                                        </td>

                                                                    </tr>
                                                        <tr>
                                                                        <td width="140px" align="left"><span class="leftpadding "><asp:Label ID="ps_lbl12" runat="server"></asp:Label></td>
                                                                        <td width="10%" align="center">:</td>
                                                                        <td width="270px" align="left">
                                                                             <asp:DropDownList ID="dd_tran_kew" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                       <asp:ListItem Value="">--- PILIH ---</asp:ListItem>
                                                       <asp:ListItem Value="12">12</asp:ListItem>
                                                       <asp:ListItem Value="13">13</asp:ListItem>
                                                       <asp:ListItem Value="14">14</asp:ListItem>
                                                       <asp:ListItem Value="15">15</asp:ListItem>
                                                       <asp:ListItem Value="16">16</asp:ListItem>
                                                       <asp:ListItem Value="17">17</asp:ListItem>
                                                       <asp:ListItem Value="18">18</asp:ListItem>
                                                       </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                     
                                                                     <tr>
                                                                        <td colspan="3" align="right"><hr /></td>
                                                                    </tr>
                                                                     <tr>
                                                                        <td colspan="3" align="center">
                                 <asp:Button ID="Button5" runat="server" class="btn btn-danger" Text="Simpan" OnClick="fin_clk_submit" UseSubmitBehavior="false" />
                                <asp:Button ID="Button6" runat="server" class="btn btn-default" Text="Set Semula" OnClick="fin_rst_Click" UseSubmitBehavior="false"  />
                                <asp:Button ID="Button8" runat="server" CssClass="btn btn-default" OnClick="fin_Button5_Click" Text="Keluar" />
                                                                        </td>
                                                                    </tr>  
                                                         <tr>
                                                                        <td colspan="3">&nbsp;</td>
                                                                    </tr>                                                     
                                                        <tr>
                                                                        <td colspan="3" align="left"><span class="leftpadding " style="font-weight:bold;">YEAR END CLOSING</td>                                                                       
                                                                        </tr> 
                                                       <tr>
                                                           <td colspan="3">
                                                       <div class="circle-icon">
            <div class="circle-out">
            <div class="circle-bg"><asp:LinkButton id="fin_data3" runat="server" onclick="load_akaun_kk"><img src="../dist/img/icon/icon1.svg" alt='PAPAR PENYATA KUNCI KIRA-KIRA' /></asp:LinkButton></div>
                <div class="text-cir"><p>PAPAR PENYATA <br /> KUNCI KIRA-KIRA</p></div>
            </div>
            <div class="circle-arrow">
                <p class="arrow-1">&#8594;</p>
                <p class="arrow-2">&#8592;</p>
            </div>
            <div class="circle-out">
            <div class="circle-bg"> <asp:LinkButton id="fin_data1" runat="server" onclick="load_akaun_pem"><img src="../dist/img/icon/icon2.svg" alt='AKAUN PEMBAHAGIAN' /> </asp:LinkButton>
            </div>
                <div class="text-cir"><p>AKAUN PEMBAHAGIAN</p></div>
            </div>
            <div class="circle-arrow">
                <p class="arrow-1">&#8594;</p>
            </div>
            <div class="circle-out">
            <div class="circle-bg"> <asp:LinkButton id="fin_data2" runat="server" onclick="load_akaun_tutup"><img src="../dist/img/icon/icon3.svg" alt='TUTUP AKAUN' /> </asp:LinkButton></div>
                <div class="text-cir text-danger"><p>TUTUP AKAUN</p></div>
        </div>
        </div>
                                                               </td>
                                                           </tr>
                                                                    </table>          
                                    </asp:Panel>
                        </div>

                    </div>
                </div>
            </div>
            <!-- /.row -->

         </ContentTemplate>
             <Triggers>
               <asp:PostBackTrigger ControlID="Button4"  />
               <asp:PostBackTrigger ControlID="btb_kmes"  />
           </Triggers>
    </asp:UpdatePanel>
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
   
</asp:Content>

