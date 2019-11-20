<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../PELABURAN_ANGGOTA/PP_Guaman_cagaran.aspx.cs" Inherits="PP_Guaman_cagaran" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" style="height: 895px;">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1> Tindakan Litigasi(bercagar) </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Pelaburan Anggota</a></li>
                            <li class="active"> Tindakan Litigasi(bercagar)  </li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
      
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Pelanggan </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  No Permohonan <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="txtappno" runat="server" class="form-control validate[optional] uppercase"
                                                                    MaxLength="12"></asp:TextBox>
                                         <asp:Panel ID="autocompleteDropDownPanel" runat="server" ScrollBars="Auto" Height="150px"
                                                            Font-Size="Medium" HorizontalAlign="Left" Wrap="False" />
                                                        <cc1:AutoCompleteExtender ServiceMethod="GetCompletionList" MinimumPrefixLength="1"
                                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtappno"
                                                            ID="AutoCompleteExtender1" CompletionListElementID="autocompleteDropDownPanel"
                                                            CompletionListCssClass="form-control uppercase" runat="server" FirstRowSelected="false">
                                                        </cc1:AutoCompleteExtender>
                                    </div>
                                </div>
                            </div>
                             <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <div class="col-sm-8">
                                       <asp:Button ID="Button4" runat="server" class="btn btn-primary" Text="Carian" UseSubmitBehavior="false"
                                                                OnClick="btnsrch_Click" />                                                            
                                                            <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula"
                                                                UseSubmitBehavior="false" OnClick="Button1_Click" />
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="row">
                             <div class="col-md-12">
                            
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Nama  </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtname" runat="server" MaxLength="150" class="form-control validate[optional,custom[textSp]] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                
                                
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   No KP Baru</label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="txtnokp" runat="server" class="form-control validate[optional] uppercase"
                                                                    MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                                <%-- <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Wilayah / Pejabat </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtwila" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Cawangan / Jabatan  </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="txtcawa" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>--%>

                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Amaun Pengeluaran (RM) </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txtamaun" runat="server" class="form-control validate[optional,custom[number]]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Tempoh (Bulan)   </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="txttemp" runat="server" class="form-control uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>


                  <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Cagaran </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>


                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">    Jenis Cagaran <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                <ContentTemplate>
                                                                    <div class="col-md-6 col-sm-4">
                                                                        <asp:RadioButton ID="rb1" runat="server" Text=" Rumah" GroupName='Jenis_Cagaran' />
                                                                        <%--  <label>Warganegara</label>--%>
                                                                    </div>
                                                                    <div class="col-md-6 col-sm-5">
                                                                        <asp:RadioButton ID="rb2" runat="server" Text=" Tanah" GroupName='Jenis_Cagaran' />
                                                                        <%--  <label>Bukan Warganegara</label>--%>
                                                                    </div>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   No Geran <span style="color: Red">*</span>   </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="txtng" runat="server" class="form-control validate[required,custom[onlyLetterNumber]] uppercase" MaxLength="20"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Jenis Milikan Geran <span style="color: Red" >*</span> </label>
                                    <div class="col-sm-8">
                                       <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                <ContentTemplate>
                                                                    <div class="col-md-6 col-sm-4">
                                                                        <asp:RadioButton ID="rbjmg1" runat="server" Text=" Individu" GroupName='Jenis_Milikan' />
                                                                        <%--  <label>Warganegara</label>--%>
                                                                    </div>
                                                                    <div class="col-md-6 col-sm-5">
                                                                        <asp:RadioButton ID="rbjmg2" runat="server" Text=" Perkongsian" GroupName='Jenis_Milikan' />
                                                                        <%--  <label>Bukan Warganegara</label>--%>
                                                                    </div>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Nilai Hartanah (RM) <span style="color: Red">*</span>  </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtnh" runat="server" class="form-control validate[optional,custom[number]]" MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                              <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Panel Peguam <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txtpp" runat="server" class="form-control validate[optional,custom[textSp1]] uppercase" MaxLength="150"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Tarikh Permohonan Tindakan <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                          <div class="input-group">
                                                                   
                                                        <asp:TextBox ID="txttpt" runat="server" placeholder="DD/MM/YYYY" class="form-control validate[required] datepicker mydatepickerclass"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Tarikh Notis 16D <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                       <div class="input-group">
                                                                   
                                                        <asp:TextBox ID="txttn16d" runat="server" placeholder="DD/MM/YYYY" class="form-control datepicker validate[required]  mydatepickerclass"></asp:TextBox>
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                       </div>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Tarikh Perbicaraan <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                                                   
                                                       <asp:TextBox ID="txttp" runat="server" placeholder="DD/MM/YYYY" class="form-control validate[required] datepicker mydatepickerclass"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Tarikh Notis 16G <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                                                   
                                                       <asp:TextBox ID="txttn16g" runat="server" placeholder="DD/MM/YYYY" class="form-control validate[required] datepicker mydatepickerclass"></asp:TextBox>
                                             <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                       </div>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Keputusan Perbicaraan <span style="color: Red">*</span>  </label>
                                    <div class="col-sm-8">
                                         <textarea id="txtareakp" runat="server" rows="3" class="form-control validate[required] uppercase" maxlength="250"></textarea>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Alamat Hartanah <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                       <textarea id="txtareaalmt" runat="server" rows="3" class="form-control validate[optional,custom[onlyaddress]] uppercase" maxlength="250"></textarea>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Poskod <span style="color: Red">*</span>  </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="txtpk" runat="server" class="form-control validate[optional,custom[phone]]" MaxLength="5"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Negeri <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" ID="ddl1">
                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Gadaian </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>



                         <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">    Tarikh Lulusan Gadaian <span style="color: Red">*</span>   </label>
                                    <div class="col-sm-8">
                                          <div class="input-group">
                                                                  
                                                      <asp:TextBox ID="txttlg1" runat="server" class="form-control validate[required] datepicker  mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                       </div>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Tarikh Lepasan Gadaian <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                       <div class="input-group">
                                                                  
                                                        <asp:TextBox ID="txttlg2" runat="server" class="form-control validate[required] datepicker mydatepickerclass " placeholder="DD/MM/YYYY"></asp:TextBox>
                                             <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
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
                                <asp:Button ID="Button2" runat="server" class="btn btn-danger" Text="Simpan" UseSubmitBehavior="false" OnClick="btnsmmit_Click" />                                                                
                                                                
                            </div>
                           </div>
                               </div>


                             <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Ketuanpunyaan geran</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>


                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Nama <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtnama1" runat="server" MaxLength="150" class="form-control validate[optional,custom[textSp]] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">    No KP <span style="color: Red">*</span>   </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="txtnokp1" runat="server" class="form-control validate[optional,custom[number]]"
                                                                    MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Alamat <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                         <textarea id="txtareaalamt" runat="server" rows="3" class="form-control validate[optional,custom[onlyaddress]] uppercase" maxlength="250"></textarea>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Jenis Pengenalan <span style="color: Red">*</span>  </label>
                                    <div class="col-sm-8">
                                         <asp:DropDownList ID="ddljp" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Poskod </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtpk1" runat="server" class="form-control validate[optional,custom[number]]" MaxLength="5"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">    No Telefon <span style="color: Red">*</span>  </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txttp1" runat="server" class="form-control validate[optional,custom[phone]]" MaxLength="11"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>


                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Negeri</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]" ID="ddlneg">
                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Hubungan </label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="ddlhubu" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>


                             <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                               <asp:Button ID="Button5" runat="server" CssClass="btn btn-primary" Text="Tambah" UseSubmitBehavior="false" OnClick="btntmpa_Click"  />                                                                
                                                                <asp:Button ID="Button7" runat="server" CssClass="btn btn-danger" Text="Kemaskini" UseSubmitBehavior="false" OnClick="btnkems1_Click"  />
                            </div>
                           </div>
                               </div>
                           <div class="box-body">&nbsp;
                                    </div>
                             <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                    <%--  <div class="row" style="overflow:auto;">--%>
           <div class="col-md-12 box-body">
                                   <%--<asp:GridView Id="example1" runat="server" class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="Small" Width="100%" AllowPaging="true" PageSize="25" OnPageIndexChanging="gvSelected_PageIndexChanging">--%>
               <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None">
                   <PagerStyle CssClass="pager" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="BIL" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                                    ItemStyle-Width="150" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="NO KP">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton runat="server" ID="lnkView" OnClick="lnkView_Click" Font-Bold Font-Underline>
                                                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("pro_icno") %>'></asp:Label>
                                                                                </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="NAMA">
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("pro_name") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="JENIS PENGENALAN">
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="NO TELEFON">
                                                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("pro_phone") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="HUBUNGAN">
                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("Contact_Name") %>'></asp:Label>
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
                      
                         <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Lelongan</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>




                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> Tempat Lelongan <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txttl1" runat="server" class="form-control validate[optional,custom[onlyLetterNumberSp]] uppercase" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">    Tarikh Lelongan <span style="color: Red">*</span>  </label>
                                    <div class="col-sm-8">
                                        <div class="input-group">           
                                                        <asp:TextBox ID="txttl2" runat="server" class="form-control validate[optional] datepicker mydatepickerclass uppercase" placeholder="DD/MM/YYYY"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label">  Agensi Lelongan <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="txtal1" runat="server" class="form-control validate[optional,custom[textSp]] uppercase" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">    Nilai Lelongan (RM) <span style="color: Red">*</span> </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txtnl1" runat="server" class="form-control validate[optional,custom[number]]" style="text-align:right;"
                                                                    MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   Amaun Lelongan (RM) <span style="color: Red">*</span>  </label>
                                    <div class="col-sm-8">
                                         <asp:TextBox ID="txtal2" runat="server" class="form-control validate[optional,custom[number]]" style="text-align:right;" MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                   
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">    Keputusan Lelongan <span style="color: Red">*</span></label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="ddlkl" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                               <asp:TextBox ID="sqno1" runat="server" Visible="false" class="form-control" MaxLength="12"></asp:TextBox>
                                                                <asp:Button ID="Button6" runat="server" class="btn btn-primary" Text="Tambah" UseSubmitBehavior="false" OnClick="btntmpa1_Click" />
                                                            <asp:Button ID="Button8" runat="server" Visible="false" class="btn btn-danger" Text="Kemaskini" UseSubmitBehavior="false" OnClick="btnkem_Click" />
                            </div>
                           </div>
                               </div>
                           <div class="box-body">&nbsp;
                                    </div>


                         <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                    <%--  <div class="row" style="overflow:auto;">--%>
           <div class="col-md-12 box-body">
                                   <%--<asp:GridView Id="example1" runat="server" class="table table-bordered table-striped uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="Small" Width="100%" AllowPaging="true" PageSize="25" OnPageIndexChanging="gvSelected_PageIndexChanging">--%>
               <asp:GridView ID="GridView2" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None">
                   <PagerStyle CssClass="pager" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="BIL" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                                                                    ItemStyle-Width="150" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Tarikh Lelongan">
                                                                            <ItemTemplate>
                                                                            <asp:LinkButton runat="server" ID="lnkView_lel" OnClick="lnkView_Click2" Font-Bold Font-Underline>
                                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("auc_dt","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                                </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Tempat Lelongan">
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("auc_venue") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Nilai Lelongan (RM)">
                                                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("auc_value_amt","{0:n}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Keputusan Lelongan">
                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("keputusan_desc") %>'></asp:Label>
                                                                                <asp:Label ID="seqno" Visible="false" runat="server" Text='<%# Bind("auc_seq_no") %>'></asp:Label>
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
    </ContentTemplate>
            
    </asp:UpdatePanel>
</asp:Content>





