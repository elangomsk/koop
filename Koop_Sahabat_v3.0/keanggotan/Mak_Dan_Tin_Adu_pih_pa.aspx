<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="Mak_Dan_Tin_Adu_pih_pa.aspx.cs" Inherits="Mak_Dan_Tin_Adu_pih_pa" %>


 <%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
     <div class="content-wrapper">
            
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title">MAKLUMBALAS DAN TINDAKAN ADUAN PIHAK PENGURUSAN </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Tarikh Aduan <span class="style1">*</span> :</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="Txtdate" runat="server"  class="form-control validate[optional] uppercase "></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6 box-body">
                                <div class="form-group">
                             <label for="inputEmail3" class="col-sm-3 control-label">        Kategori Aduan :</label>
                                    <div class="col-sm-8">
                                          <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
                                        <asp:TextBox ID="Txtkat" runat="server"  class="form-control uppercase"></asp:TextBox>
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
                               <label for="inputEmail3" class="col-sm-3 control-label">      Status :</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txtsts" runat="server"  class="form-control validate[optional] uppercase "></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6 box-body">
                                <div class="form-group">
                               <label for="inputEmail3" class="col-sm-3 control-label">     Cawangan :</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtcaw" runat="server"  class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                     </div>


                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                  <label for="inputEmail3" class="col-sm-3 control-label">   Pengadu :</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="Txtpen" runat="server"  class="form-control validate[optional] uppercase" MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                <label for="inputEmail3" class="col-sm-3 control-label">    No Telefon :</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="Txt_telno" runat="server" class="form-control" style="text-transform:uppercase;" maxlength="15"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                     </div>


                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                  <label for="inputEmail3" class="col-sm-3 control-label">   Butiran :</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtdesc" runat="server"  class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase" MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                 <label for="inputEmail3" class="col-sm-3 control-label">  Kategori : </label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="dd_kat" runat="server" CssClass="form-control uppercase">
                                                                    <asp:ListItem Value="">--- PILIH ---</asp:ListItem>
                                                                    <asp:ListItem Value="01">NORMAL</asp:ListItem>
                                                                    <asp:ListItem Value="02">SEPARA KRITIKAL</asp:ListItem>
                                                                    <asp:ListItem Value="03">KRITIKAL</asp:ListItem>
                                                                    </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                     </div>

                            

                          
                                   <div class="box-header with-border" >
                            <h3 class="box-title"> MAKLUMBALAS & TINDAKAN PENERIMA ADUAN </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                  <label for="inputEmail3" class="col-sm-3 control-label">  Maklumbalas : <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                           <asp:TextBox ID="txtmak1" runat="server" TextMode="MultiLine" Height="100px" class="form-control" style="text-transform:uppercase;"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                   
                                    <div class="col-sm-8">
                                      
                                    </div>
                                </div>
                            </div>
                                 </div>
                                     </div>


                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                   <label for="inputEmail3" class="col-sm-3 control-label">  Tindakan : <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="Txttin1" runat="server" TextMode="MultiLine"  Height="100px" class="form-control" style="text-transform:uppercase;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                   
                                    <div class="col-sm-8">
                                     
                                    </div>
                                </div>
                            </div>
                                 </div>
                                     </div>

                              <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                               <label for="inputEmail3" class="col-sm-3 control-label">     Status : <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="DDSTS" runat="server" class="form-control select2 validate[optional]" style="text-transform:uppercase;"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6 box-body">
                                <div class="form-group">
                             <label for="inputEmail3" class="col-sm-3 control-label">        Tarikh :</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="Txtcdate" runat="server" class="form-control datepicker mydatepickerclass"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                     </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                <label for="inputEmail3" class="col-sm-3 control-label">    ID Pegawai :</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="Txtnama1" runat="server" class="form-control" style="text-transform:uppercase;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                     </div>
                                                                 <%--</div>
                                           <div role="tabpanel" class="tab-pane" runat="server" id="p2" >--%>
                           <div class="box-header with-border">
                            <h3 class="box-title"> MAKLUMBALAS & TINDAKAN KETUA JABATAN </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                <label for="inputEmail3" class="col-sm-3 control-label">     Maklumbalas :</label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="Txtmak2" runat="server" TextMode="MultiLine"  Height="100px" class="form-control" style="text-transform:uppercase;"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                 
                                    <div class="col-sm-8">
                                         
                                    </div>
                                </div>
                            </div>
                                 </div>
                                     </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                              <label for="inputEmail3" class="col-sm-3 control-label">      Tindakan :</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="Txttin2" runat="server" TextMode="MultiLine"  Height="100px" class="form-control" style="text-transform:uppercase;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                   
                                    <div class="col-sm-8">
                                       
                                    </div>
                                </div>
                            </div>
                                 </div>
                                     </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                              <label for="inputEmail3" class="col-sm-3 control-label">     Status :</label>
                                    <div class="col-sm-8">
                                          <asp:DropDownList ID="ddsts1" runat="server" class="form-control selectpicker" style="text-transform:uppercase;"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6 box-body">
                                <div class="form-group">
                               <label for="inputEmail3" class="col-sm-3 control-label">      Tarikh :</label>
                                    <div class="col-sm-8">
                                          <asp:TextBox ID="txtcdate1" runat="server" class="form-control datepicker mydatepickerclass"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                     </div>



                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                 <label for="inputEmail3" class="col-sm-3 control-label">    ID Pegawai :</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtnama2" runat="server" class="form-control" style="text-transform:uppercase;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>


                                
                                                                 <%--</div>
                                           <div role="tabpanel" class="tab-pane" runat="server" id="p2" >--%>
                                <div style="display:none;">
                           <div class="box-header with-border">
                            <h3 class="box-title"> ULASAN PIHAK PENGURUSAN </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                             <div id="Div4" runat="server" class="tab-pane fade active in">

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                           <label for="inputEmail3" class="col-sm-3 control-label">         Tindakan :</label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="Txttin3" runat="server" TextMode="MultiLine" Height="100px" class="form-control" style="text-transform:uppercase;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                   
                                    <div class="col-sm-8">
                                       
                                    </div>
                                </div>
                            </div>
                                 </div>
                                     </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                          <label for="inputEmail3" class="col-sm-3 control-label">         Status :</label>
                                    <div class="col-sm-8">
                                          <asp:CheckBox ID="chkb1" runat="server" />&nbsp;<asp:Label ID="Label1" runat="server" Text="Selesai"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                   <label for="inputEmail3" class="col-sm-3 control-label">  Tarikh :</label>
                                    <div class="col-sm-8">
                                             <asp:TextBox ID="Txtcdate3" runat="server" class="form-control datepicker mydatepickerclass"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                     </div>



                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                              <label for="inputEmail3" class="col-sm-3 control-label">       ID Pegawai :</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtnama3" runat="server" class="form-control" style="text-transform:uppercase;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                 </div>
                                 </div>
                                    </div>
                                <hr />
                               <div class="row">
                                        <div class="col-md-12 col-sm-4" style=" text-align:center;">
                                                <div class="body collapse in">
                                                            <asp:Button ID="Button1" runat="server" class="btn btn-danger" Text="Simpan" onclick="Button1_Click" />
                                                              <asp:Button ID="Button2" runat="server" class="btn btn-default" UseSubmitBehavior="false" Text="Set Semula" onclick="Button2_Click" />
                                                                <asp:Button ID="Button4" runat="server" class="btn btn-danger" UseSubmitBehavior="false" Text="Kembali" onclick="Button4_Click"  />
                                                                <asp:Button ID="Button3" runat="server" class="btn btn-success" UseSubmitBehavior="false" Text="Kembali" onclick="Button3_Click" />
                                                                 <asp:Button ID="Button5" runat="server" class="btn btn-warning" OnClientClick="document.forms[0].target ='_blank';"  UseSubmitBehavior="false" Text="Cetak" onclick="Button5_Click"  />
                                            </div>
                                            </div>
                                            </div>
                                             <div class="row">
                                   <div class="col-md-12 col-sm-2" style="text-align:center">
                                     <rsweb:ReportViewer ID="RptviwerStudent" runat="server"></rsweb:ReportViewer>
                                     <asp:Label runat="server" ID="ReportErrorMessage" Visible="false" CssClass="report-error-message"></asp:Label>
                                    </div>
                                    </div>
                                               </div>
                                        <%-- </div>
                                      </div>--%>
                            
          </div>
                            <hr />
                        
                          </div>
                                </div>
                          
                        </div>
            </div>
</asp:Content>

