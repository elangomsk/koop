<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_MOHON_CUTI.aspx.cs" Inherits="HR_MOHON_CUTI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        <script type="text/javascript">
         $().ready(function () {
             var today = new Date();
             var preYear = today.getFullYear() - 1;
             var curYear = today.getFullYear() - 0;
          
             $('.datepicker2').datepicker({
                 format: 'dd/mm/yyyy',
                 autoclose: true,
                 inline: true,
                 startDate: new Date($("#<%=TextBox5.ClientID %>").val()),
                <%-- endDate: new Date($("#<%=TextBox5.ClientID %>").val())--%>
             }).on('changeDate', function (ev) {
                 (ev.viewMode == 'days') ? $(this).datepicker('hide') : '';
             });
            
         });

     </script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:ScriptManager ID="ScriptManagerCalendar" ScriptMode="Release" runat="server">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1> Permohonan Cuti        </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i><asp:Label ID="ps_lbl2" runat="server"></asp:Label></a></li>
                            <li class="active"> Permohonan Cuti   </li>
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
                            <h3 class="box-title">  Maklumat Permohonan Cuti </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                              <div id="myModal" class="modal fade" role="dialog">
  <div class="modal-dialog animate" style="width:700px;">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title"><strong>Maklumat Cuti</strong></h4>
      </div>
      <div class="modal-body">
        <div class="box-cal box-primary">
               <div class="box-body no-padding">
                  <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging">
                    <PagerStyle CssClass="pager" />
                                                    <Columns>

                                                    <asp:TemplateField HeaderText="BIL">  
                                                          <ItemStyle HorizontalAlign="center"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="JENIS CUTI">
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("hr_jenis_desc") %>' CssClass="uppercase"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CUTI DIBAWA KEHADAPAN">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label2_fl" runat="server" Text='<%# Eval("a","{0:n1}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CUTI LAYAK (TAHUNAN)">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("c","{0:n1}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="KELAYAKAN CUTI TERKINI" Visible="false">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="Lab_kct" runat="server" Text='<%# Eval("b","{0:n1}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="JUMLAH LAYAK (TAHUNAN)">
                                                             <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="Lab_jl" runat="server" Text='<%# Eval("ab","{0:n1}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="CUTI PROSES">
                                                             <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1_cp" runat="server" Text='<%# Bind("e","{0:n1}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CUTI DIAMBIL">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("d","{0:n1}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="BAKI CUTI TERKINI">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("res","{0:n1}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="LAYAK CUTI PRORATE">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("pro_rate","{0:n1}") %>'></asp:Label>
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
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
      </div>
    </div>

  </div>
</div>
                          
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl6" runat="server"></asp:Label> <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                        <%--<asp:DropDownList ID="DD_JENCUTI" style="width:100%; font-size:13px;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="lnkselect_Click" class="form-control select2 validate[optional]">
                                                </asp:DropDownList>--%>
                                        <asp:DropDownList ID="DD_JENCUTI" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" AutoPostBack="true" OnSelectedIndexChanged="get_jenis_cuti">
                                                </asp:DropDownList>
                                         <asp:TextBox ID="TextBox5" runat="server" Visible="false" class="form-control uppercase"></asp:TextBox> 
                                        <asp:TextBox ID="TextBox3" runat="server" Visible="false" class="form-control uppercase"></asp:TextBox> 
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="You can't leave this Empty" CssClass="requiredFieldValidateStyle" ForeColor="Red" ControlToValidate="DD_JENCUTI" ValidationGroup="vgSubmit">
                                    </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body leave-application">
                                     <div class="" id="shw_blnce" runat="server">
                                        <asp:Label ID="get_blnce" runat="server"></asp:Label>
                                    </div>
                                  <a href="#"  data-toggle="modal" data-target="#myModal" style="text-decoration:underline; font-weight:bold;">(Click Here for More Leave Information)</a>
                            </div>
                            <div class="col-md-6 box-body" id="rujno" runat="server" style="display:none;">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl7" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txt_norju" runat="server" class="form-control  uppercase" MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body" id="v1" runat="server">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  <asp:Label ID="ps_lbl8" runat="server"></asp:Label>  <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                        <div class="input-group">                                                                    
                                                <asp:TextBox ID="txt_tkcuti" runat="server" class="form-control validate[optional] datepicker2 mydatepickerclass" autocomplete="off" placeholder="PICK DATE"></asp:TextBox>
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                </div>
                                        <br />
                                        <asp:checkbox ID="gt_pro" OnCheckedChanged="get_pro_rate" AutoPostBack="true" runat="server" Text="Prorate" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="You can't leave this Empty" CssClass="requiredFieldValidateStyle" ForeColor="Red" ControlToValidate="txt_tkcuti" ValidationGroup="vgSubmit">
                                    </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="TextBox2" style="display:none;" runat="server" class="form-control validate[optional,custom[dtfmt]]" placeholder="DD/MM/YYYY"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                
                            <div class="col-md-6 box-body" id="v2" runat="server" >
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl9" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                                <asp:TextBox ID="txt_hing" runat="server" class="form-control validate[optional] datepicker2 mydatepickerclass" autocomplete="off" placeholder="PICK DATE"></asp:TextBox>
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                </div>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body" id="Div1" runat="server" visible="false" >
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Sesi </label>
                                    <div class="col-sm-8">                                        
                                                <asp:DropDownList ID="sel_sesi" runat="server" class="form-control validate[optional] select2">
                                                    <asp:ListItem Value="">--- PILIH ---</asp:ListItem>
                                                    <asp:ListItem Value="Pagi">Pagi</asp:ListItem>
                                                    <asp:ListItem Value="Petang">Petang</asp:ListItem>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="ps_lbl12" runat="server"></asp:Label> <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txt_sebab" runat="server" class="form-control validate[optional] uppercase" autocomplete="off" MaxLength="500"></asp:TextBox>
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="You can't leave this Empty" CssClass="requiredFieldValidateStyle" ForeColor="Red" ControlToValidate="txt_sebab" ValidationGroup="vgSubmit">
                                    </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 box-body"  id="disp_id1" runat="server">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl10" runat="server"></asp:Label>  </label>
                                    <div class="col-sm-8">
                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                         <br />
                                          <asp:LinkButton ID="lnkDownload" runat="server" onclick = "DownloadFile"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                                
                           
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                                 <div class="col-md-6 box-body" id="disp_id2" runat="server" visible="false">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl11" runat="server"></asp:Label>Sebab Batal Cuti  <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="Applcn_no1" runat="server" class="form-control validate[optional] uppercase"
                                                                                MaxLength="10" Visible="false"></asp:TextBox>
                                         <asp:TextBox ID="txtsbcuti" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox1" runat="server" Visible="false" class="form-control validate[optional] uppercase"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox4" runat="server" Visible="false" class="form-control validate[optional] uppercase"></asp:TextBox>
                                        <asp:TextBox ID="txtsno" runat="server" Visible="false" class="form-control validate[optional] uppercase"></asp:TextBox>
                                         <asp:TextBox ID="TextBox8" runat="server" Visible="false" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            </div>
                                 </div>
                              <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                 <asp:Button ID="btn_submit" runat="server" class="btn btn-danger" Text="Simpan" ValidationGroup="vgSubmit" OnClientClick=" if ( Page_ClientValidate() ) { this.value='Please wait..';this.style.display='none';getElementById('bWait1').style.display = '';  }" UseSubmitBehavior="false" onclick="btn_submit_Click" />                                
                                
                                <asp:Button ID="btn_kemask" runat="server" Visible="false" class="btn btn-danger" Text="Kemaskini" UseSubmitBehavior="false" onclick="Button2_Click" ValidationGroup="vgSubmit" OnClientClick=" if ( Page_ClientValidate() ) { this.value='Please wait..';this.style.display='none';getElementById('bWait2').style.display = '';  }"/>
                                <input type="button" id="bWait1" class="btn btn-danger" value="Please wait ..." disabled="disabled" style="display:none" />
                                <input type="button" id="bWait2" class="btn btn-danger" value="Please wait ..." disabled="disabled" style="display:none" />
                                <input type="button" id="bWait3" class="btn btn-warning" value="Please wait ..." disabled="disabled" style="display:none" />
                                <asp:Button ID="Button3" runat="server" Visible="false" class="btn btn-warning" Text="Batal" UseSubmitBehavior="false" onclick="Button1_Click" ValidationGroup="vgSubmit" OnClientClick=" if ( Page_ClientValidate() ) { this.value='Please wait..';this.style.display='none';getElementById('bWait3').style.display = '';  }" />
                                                <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" UseSubmitBehavior="false" onclick="rst_Click" />
                               
                                <asp:Button ID="Button2" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" onclick="Click_bck" />
                            </div>
                           </div>
                               </div>
                       
                    </div>
                           <div class="box-body">&nbsp;
                                    </div>

                        </div>

                    </div>
                
          
            <!-- /.row -->
 </ContentTemplate>
             <Triggers>
               <asp:PostBackTrigger ControlID="btn_submit"  />  
                   <asp:PostBackTrigger ControlID="btn_kemask"  />
                 <asp:PostBackTrigger ControlID="lnkDownload"  />               
           </Triggers>
    </asp:UpdatePanel>
        
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
   
</asp:Content>



