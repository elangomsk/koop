<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/HR_KM_Latihan.aspx.cs" Inherits="HR_KM_Latihan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

   <script type="text/javascript">
          function addTotal_bk1() {
              var sno = $("#<%=Kaki_no.ClientID %>").val();
              
                  var amt1 = Number($("#<%=yuran_lati.ClientID %>").val());

                  $(".au_amt").val(amt1.toFixed(2));
             
          }
         </script>
      
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

     <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>--%>
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1 id="h1_tag" runat="server"> Kemasukan Maklumat Latihan </h1>
                        <ol class="breadcrumb">
                            <li id="bb1_text" runat="server"><a href="#"><i class="fa fa-dashboard"></i>Sumber Manusia</a></li>
                            <li class="active" id="bb2_text" runat="server"> Kemasukan Maklumat Latihan </li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
      
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="h3_tag" runat="server">MAKLUMAT PERIBADI</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl1_text" runat="server"> No Kakitangan </label>
                                    <div class="col-sm-8 text-right text-bold">
                                         <asp:Label ID="Kaki_no" runat="server" class="uppercase" MaxLength="1000"></asp:Label>
                                                         <asp:TextBox ID="Applcn_no1" runat="server" class="form-control validate[optional] uppercase"
                                                                                MaxLength="150" Visible="false"></asp:TextBox>
                                                                            
                                    </div>
                                </div>
                            </div>
                           
                            </div>
                                 </div>
                                
                             
                             <div class="row">
                             <div class="col-md-12">
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl2_text" runat="server">   Nama Kakitangan   </label>
                                    <div class="col-sm-8 text-right">
                                        <asp:Label ID="s_nama" runat="server" class="uppercase"></asp:Label>
                                    </div>
                                </div>
                            </div>
                                
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl3_text" runat="server">  Gred </label>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl4_text" runat="server">   Nama Syarikat / Organisasi  </label>
                                    <div class="col-sm-8 text-right">
                                        <asp:Label ID="txt_org" runat="server" class="uppercase"></asp:Label>
                                    </div>
                                </div>
                            </div>
                                
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl5_text" runat="server">  Perniagaan </label>
                                    <div class="col-sm-8 text-right">
                                        <asp:Label ID="TextBox2" runat="server" class="uppercase"></asp:Label>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="row">
                             <div class="col-md-12">
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl6_text" runat="server">    Jabatan  </label>
                                    <div class="col-sm-8 text-right">
                                        <asp:Label ID="s_jab" runat="server" class="uppercase"></asp:Label>
                                    </div>
                                </div>
                            </div>
                                
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl7_text" runat="server"> Jawatan </label>
                                    <div class="col-sm-8 text-right">
                                        <asp:Label ID="s_jaw" runat="server" class="uppercase"></asp:Label>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="box-header with-border">
                            <h3 class="box-title" id="h3_tag2" runat="server">Latihan Yang Dihadiri</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>

                             <div class="row">
                             <div class="col-md-12">
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl8_text" runat="server">     Kategori Latihan  </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="dd_kat_latihan" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl9_text" runat="server"> Jenis Latihan </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="dd_jen_latihan" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl10_text" runat="server">     Nama Latihan </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="nama_lat" runat="server" class="form-control validate[optional,custom[textSp]] uppercase" MaxLength="250"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl11_text" runat="server"> Tempat </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="tempat_lat" runat="server" class="form-control validate[optional] uppercase" MaxLength="250"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="row">
                             <div class="col-md-12">
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl12_text" runat="server">     Nama Penyedia</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="nama_peny" runat="server" class="form-control validate[optional] uppercase" MaxLength="150"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl13_text" runat="server">Tempoh </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="tempoh_lat" runat="server" class="form-control validate[optional,custom[number]]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="row">
                             <div class="col-md-12">
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl14_text" runat="server">      Dari</label>
                                    <div class="col-sm-8">
                                       <div class="input-group">
                                                       <asp:TextBox ID="tm_lati" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
                                           <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                       </div>
                                    </div>
                                </div>
                            </div>
                                
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl15_text" runat="server"> Hingga </label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                                       <asp:TextBox ID="ta_lati" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                       </div>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                            <div class="row">
                             <div class="col-md-12">
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl16_text" runat="server">      Yuran (RM)</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="yuran_lati" style="text-align:right;" runat="server" class="form-control validate[optional,custom[number]] au_amt"  onblur="addTotal_bk1(this)"></asp:TextBox>
                                </div>
                            </div>
                                 <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="lbl17_text" runat="server"> Lampiran </label>
                                    <div class="col-sm-8">
                                        <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="true" />
                                                        <br />
                                                      
                                                        <asp:Button ID="btnUpload" runat="server" class="btn btn-danger" Text="Upload" UseSubmitBehavior="false" OnClick="Upload"  />
                                                      
                                                       
                                    </div>
                                </div>
                                </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label" id="Label1" runat="server">   Catatan</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox1" runat="server" class="form-control uppercase" MaxLength="1000" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                </div>
                            </div>
                                </div>
                                  </div>
                                </div>
                              
                          <div class="box-body">&nbsp;
                                    </div>
                          <%--   </ContentTemplate>
         </asp:UpdatePanel>--%>
                              <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                                         <asp:GridView ID="GridView2" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="30" ShowFooter="false" GridLines="None">
                                                             <PagerStyle CssClass="pager" />
            <Columns>
                 <asp:TemplateField HeaderText="BIL" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="2%"/> 
                                                                        <%--<asp:RadioButton ID="RadioButton1" runat="server" onclick = "RadioCheck(this);" />--%>                                                                        
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                <asp:BoundField DataField="Name" HeaderText="File Name" />
               <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" OnClick="DownloadFile"
                            CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" OnClick="DeleteFile"
                            CommandArgument='<%# Eval("Id") %>' OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;"></asp:LinkButton>
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

                            <hr />
    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
                           <div class="row">
                               <div class="col-md-12">
                            
                            <div class="col-md-12 box-body" style="text-align:center;">

                                 
                                                    <asp:TextBox ID="updt_txt" runat="server" Visible="false" class="form-control validate[optional,custom[number]]"></asp:TextBox>
                                                          <%--<asp:Button ID="Button5" runat="server" class="btn btn-danger"   Text="Muatnaik" UseSubmitBehavior="false"  Type="submit"/>--%>
                                                       <asp:Button ID="Button6" runat="server"   Text="Simpan"  class="btn btn-danger" UseSubmitBehavior="false" OnClick="click_insert" />
                                                        <asp:Button ID="Button2" runat="server"   Text="Hapus"  class="btn btn-warning" UseSubmitBehavior="false" OnClick="btn_hups_Click" />
                                                                    <asp:Button ID="Button5" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" OnClick="Click_bck" />
                                                    </div>
                            </div>
                          
                               </div>
                            </div>

                        <div class="box-body">&nbsp; </div>
                      <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                    <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="30" ShowFooter="false" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging">
                                        <PagerStyle CssClass="pager" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="BIL" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="2%"/> 
                                                                        <%--<asp:RadioButton ID="RadioButton1" runat="server" onclick = "RadioCheck(this);" />--%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Dari">
                                                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="lnkView" OnClick="lnkView_Click" Font-Bold Font-Underline>
                                                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("d1") %>'></asp:Label>
                                                                         </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="center">
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label81" runat="server" Text='<%# Bind("d2") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Nama Latihan">
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("trn_training_name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Tempat">
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("trn_venue") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Nama Penyedia">
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("trn_organiser") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Tempoh">
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label82" runat="server" Text='<%# Bind("trn_dur") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Yuran (RM)">
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label83" runat="server" Text='<%# Bind("trn_fee_amt","{0:n}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Hapus">
                                                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                       <asp:CheckBox ID="RadioButton1" runat="server" />
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
                                                                   
                           <div class="box-body">&nbsp; </div>

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
               <asp:PostBackTrigger ControlID="Button6"  />
              <asp:PostBackTrigger ControlID="btnUpload"  />
              
           </Triggers>
             
    </asp:UpdatePanel>--%>
</asp:Content>


