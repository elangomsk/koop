<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/Hr_seleng_poton.aspx.cs" Inherits="Hr_seleng_poton" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       <script type="text/javascript">
    function RadioCheck(rb) {
        var gv = document.getElementById("<%=GridView1.ClientID%>");
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
  <%--  <script type="text/javascript">
        function addTotal_bk() {
          
                var amt1 = Number($("#<%=txtckm.ClientID %>").val());
                var amt2 = Number($("#<%=TextBox14.ClientID %>").val());
                //var amt3 = Number($("#<%=TextBox4.ClientID %>").val());
                var vv = "100";
                var t_amt = amt1 / vv;
                var t_amt1 = t_amt * (amt2);
                var t_amt3 = Math.ceil(t_amt1, 1);
                $(".txtacm1").val(t_amt3.toFixed(2));
           
        }

        function addTotal_bk11() {
           
                var amt1 = Number($("#<%=txtcka.ClientID %>").val());
                var amt2 = Number($("#<%=TextBox14.ClientID %>").val());
                //var amt3 = Number($("#<%=TextBox4.ClientID %>").val());
                var vv = "100";
                var t_amt = amt1 / vv;
                //var t_amt2 = amt2 + amt3;
                var t_amt1 = t_amt * (amt2);
                var t_amt3 = Math.ceil(t_amt1, 1);
               
                //var flamt = t_amt1 + amt3;
                //alert(amt2);
                $(".txtacm2").val(t_amt3.toFixed(2));
          
        }
</script>--%>
<script type="text/javascript">
    function addTotal_bk1() {
       
           <%-- var amt1 = Number($("#<%=TextBox8.ClientID %>").val());

            $(".txt8").val(amt1.toFixed(2));--%>
        
    }

    function addTotal_bk2() {
       
         <%--   var amt1 = Number($("#<%=TextBox17.ClientID %>").val());

            $(".txt9").val(amt1.toFixed(2));--%>
       
    }
    function addTotal_bk3() {
       
            var amt1 = Number($("#<%=TextBox30.ClientID %>").val());

            $(".txt30").val(amt1.toFixed(2));
        
    }
</script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <asp:UpdateProgress id="updateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
    <ProgressTemplate>
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
            <span style="border-width: 0px; position: fixed; font-weight:bold; padding: 50px; background-color: #FFFFFF; font-size: 16px; left: 40%; top: 40%;">Sila Tunggu. Rekod Sedang Diproses ...</span>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" >
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1> <asp:Label ID="ps_lbl1" runat="server"></asp:Label>  </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i><asp:Label ID="ps_lbl2" runat="server"></asp:Label></a></li>
                            <li class="active"> <asp:Label ID="ps_lbl3" runat="server"></asp:Label> </li>
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
                            <h3 class="box-title"> <asp:Label ID="ps_lbl4" runat="server"></asp:Label> </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl5" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                      <asp:label ID="txtsno" runat="server" class="uppercase" MaxLength="150"></asp:label>
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
                                    <label for="inputEmail3" class="col-sm-4 control-label">  <asp:Label ID="ps_lbl6" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                        <asp:label ID="txtname" runat="server" class="uppercase"></asp:label>
                                                        <asp:TextBox ID="TextBox1" runat="server" Visible="false" class="form-control validate[optional] uppercase"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox2" runat="server" style=" display:none;" class="form-control validate[optional] uppercase"></asp:TextBox>
                                        <asp:TextBox ID="TextBox17" runat="server" style=" display:none;" class="form-control validate[optional] uppercase"></asp:TextBox>
                                        <asp:TextBox ID="TextBox25" runat="server" style=" display:none;" class="form-control validate[optional] uppercase"></asp:TextBox>
                                        <asp:TextBox ID="TextBox26" runat="server" style=" display:none;" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">  <asp:Label ID="ps_lbl7" runat="server"></asp:Label>  </label>
                                    <div class="col-sm-8">
                                        <asp:label ID="txtgred" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                 
                                 </div>
                                 </div>
                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"> <asp:Label ID="ps_lbl8" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                         <asp:label ID="txt_org" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl9" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                        <asp:label ID="TextBox13" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                                 <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"> <asp:Label ID="ps_lbl10" runat="server"></asp:Label> </label>                                      
                                    <div class="col-sm-8">
                                        <asp:label ID="txtjab" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"> <asp:Label ID="ps_lbl11" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                        <asp:label ID="txtjaw" runat="server" class="uppercase"></asp:label>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                           <hr />
                               <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="lbl8_text" runat="server">  Pendapatan Bagi Bulan </label>
                                    <div class="col-sm-8">
                                       <asp:DropDownList ID="DD_PBB" style="width:100%; font-size:13px;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="sel_bagi_bulan" class="form-control select2 validate[optional]">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                
                                 
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" id="lbl9_text" runat="server">  Tahun </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txt_tahu" runat="server" class="form-control validate[optional] uppercase"
                                                            MaxLength="4"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <div class="box-header with-border">
                            <h3 class="box-title"><asp:Label ID="ps_lbl12" runat="server"></asp:Label> </h3>
                        </div>
                            <div class="box-body">&nbsp; </div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"> <asp:Label ID="ps_lbl13" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="DropDownList21" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"> <asp:Label ID="ps_lbl14" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox32" runat="server" class="form-control validate[optional] uppercase" MaxLength="15"></asp:TextBox>
                                                    <asp:TextBox ID="TextBox15" Visible="false" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <div class="row">
                            <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">  <asp:Label ID="ps_lbl15" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        <asp:TextBox ID="TextBox28" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                        </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"> <asp:Label ID="ps_lbl16" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                        <div class="input-group">

                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        <asp:TextBox ID="TextBox29" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-4 control-label">  <asp:Label ID="ps_lbl17" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox30" style="text-align:right;" runat="server" class="form-control validate[optional,custom[number]] txt30" onblur="addTotal_bk3(this)"></asp:TextBox>
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
                                 <asp:Button ID="Button8" runat="server" class="btn btn-danger" Text="Simpan" UseSubmitBehavior="false" Type="submit" onclick="Button8_Click" />
                                                        <asp:Button ID="Button9" Visible="false" runat="server" Text="Kemaskini" class="btn btn-danger" UseSubmitBehavior="false" onclick="Button9_Click"/>
                                                      <asp:Button ID="Button10" runat="server" Text="Hapus" class="btn btn-warning" UseSubmitBehavior="false" onclick="Button10_Click" OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;"/>
                                <asp:Button ID="Button2" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" Type="submit" onclick="Click_bck" />
                            </div>
                           </div>
                               </div>
                              
                             <div class="box-body">&nbsp;
                                    </div>
                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                   <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging_2">
                                       <PagerStyle CssClass="pager" />
                                      <Columns>
                                      <asp:TemplateField HeaderText="BIL" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber1" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="150"/> 
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Jenis Potongan" ItemStyle-HorizontalAlign="Left">  
                                            <ItemTemplate>  
                                               <asp:LinkButton ID="lblSubItemName"    runat="server" Text='<%# Eval("hr_poto_desc")%>' CommandArgument=' <%#Eval("Id")%>' CommandName="Add"  onclick="lblSubItem_Click" Font-Bold Font-Underline>
                                                <a  href="#"></a>
                                                  </asp:LinkButton>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                          <asp:BoundField DataField="ded_ref_no" ItemStyle-HorizontalAlign="Left" HeaderText="No Rujukan"  />
                                        <asp:BoundField DataField="ded_start_dt" HeaderText="Tarikh Dari"  />
                                         <asp:BoundField DataField="ded_end_dt" HeaderText="Tarikh Hingga"  />   
                                         <asp:BoundField DataField="ded_deduct_amt" ItemStyle-HorizontalAlign="Right" HeaderText="Amaun (RM)"  />   
                                               <asp:TemplateField HeaderText="Hapus" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:RadioButton ID="RadioButton1" runat="server" onclick = "RadioCheck(this);" />
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
                             <div class="box-body">&nbsp;</div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                &nbsp;
                                    </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"> <asp:Label ID="ps_lbl22" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox16" style="text-align:right; font-weight:bold;" runat="server" class="form-control validate[optional] uppercase"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox14" runat="server" style=" display:none;" class="form-control validate[optional] uppercase"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  </div>
                                   </div>
                                
                            <div class="box-header with-border">
                            <h3 class="box-title"> <asp:Label ID="ps_lbl23" runat="server"></asp:Label> </h3>
                        </div>
                            <div class="box-body">&nbsp; </div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">  <asp:Label ID="ps_lbl24" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtahli" runat="server" class="form-control validate[optional] uppercase" MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"> <asp:Label ID="ps_lbl25" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtmaji" runat="server" class="form-control validate[optional] uppercase" MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <div class="row">
                            <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">  <asp:Label ID="ps_lbl26" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                                        <asp:TextBox ID="txttmula" AutoComplete="off" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"> <asp:Label ID="ps_lbl27" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                                        <asp:TextBox ID="txttakhir" AutoComplete="off" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-4 control-label">  Caruman KWSP Ahli (%) </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtcaruahli" runat="server" style="text-align:right;" AutoPostBack="true" OnTextChanged="text_ahli" class="form-control "></asp:TextBox>
                                      
                                    </div>
                                </div>
                            </div>
                                    <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">  <asp:Label ID="ps_lbl29" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtacahli" runat="server" style="text-align:right;" class="form-control validate[optional, custom[number]] txtacm2"></asp:TextBox>
                                        <asp:TextBox ID="TextBox4" runat="server" class="form-control validate[optional]" style="display:none;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                            <div class="row">
                             <div class="col-md-12">
                      <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">  Caruman KWSP Majikan (%) </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtkmaji" runat="server" style="text-align:right;"  AutoPostBack="true" OnTextChanged="text_maji"  class="form-control "></asp:TextBox>
                                      
                                    </div>
                                </div>
                            </div>
                                  
                                    <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">   <asp:Label ID="ps_lbl31" runat="server"></asp:Label></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtacm" runat="server" style="text-align:right;" class="form-control validate[optional, custom[number]] txtacm1"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                           <%--  <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">   <asp:Label ID="ps_lbl30" runat="server"></asp:Label> (%)  </label>                                
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtckm" runat="server" class="form-control validate[optional, custom[number]]" onkeyup="addTotal_bk(this)"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                               
                                 </div>
                                </div>--%>
                            

                         <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                <asp:Button ID="Button12" runat="server" class="btn btn-danger" Text="Simpan" Type="submit" onclick="Button12_Click" />
                                                        <asp:Button ID="Button13" runat="server" Text="Kemaskini" class="btn btn-danger" Visible="false" onclick="Button13_Click" />
                                                        <asp:Button ID="Button14" runat="server" Text="Hapus" class="btn btn-warning" onclick="Button14_Click" OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;"/>
                            </div>
                           </div>
                               </div>
                              <div class="box-body">&nbsp;
                                    </div>

                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                   <asp:GridView ID="GridView2" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging_3">
                                       <PagerStyle CssClass="pager" />
                                      <Columns>
                                       <asp:TemplateField HeaderText="BIL" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="150"/> 
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                      <asp:TemplateField HeaderText="TARIKH MULA">  
                                            <ItemTemplate>  
                                               <asp:LinkButton ID="lblSubItem"    runat="server" Text='<%# Eval("epf_eff_dt")%>' CommandArgument=' <%#Eval("epf_eff_dt")+","+ Eval("epf_end_dt")%>' CommandName="Add"  onclick="lblSubItemName_Click" Font-Bold Font-Underline>
                                                <a  href="#"></a>
                                                  </asp:LinkButton>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                      
                                        <asp:BoundField DataField="epf_end_dt" HeaderText="TARIKH SEHINGGA"  />
                                         <%--<asp:BoundField DataField="epf_percentage" HeaderText="CARUMAN AHLI (%)"  />   --%>
                                           <asp:BoundField DataField="epf_percentage" ItemStyle-HorizontalAlign="Right" HeaderText="Caruman KWSP Ahli (%)"   />   
                                         
                                         <asp:BoundField DataField="epf_amt" ItemStyle-HorizontalAlign="Right" HeaderText="Amaun Caruman Ahli (RM)" DataFormatString="{0:n}"  />   
                                           <asp:BoundField DataField="epf_emp_kwsp_perc" ItemStyle-HorizontalAlign="Right" HeaderText="Caruman KWSP Majikan (%)"  />   
                                          <asp:BoundField DataField="epf_emp_kwsp_amt" ItemStyle-HorizontalAlign="Right" HeaderText="Amaun Caruman Ahli (Rm)" DataFormatString="{0:n}" />   

                                             <asp:TemplateField HeaderText="Hapus" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:RadioButton ID="RadioButton1" runat="server" onclick = "RadioCheck(this);" />
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
                      <div class="box-body">&nbsp;
                                    </div>

                            <div class="box-header with-border">
                            <h3 class="box-title"> Maklumat Potongan Statutori – Cukai Pendapatan </h3>
                        </div>
                            <div class="box-body">&nbsp; </div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">  No Cukai Pendapatan Pekerja  </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox10" runat="server" class="form-control validate[optional] uppercase" MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                    <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">  <a href="http://calcpcb.hasil.gov.my/" target="_blank" style="text-decoration:underline;">Kalkulator PCB</a> </label>
                                   
                                </div>
                            </div>
                                 </div>
                                </div>
                            <div class="row">
                            <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">  <asp:Label ID="Label4" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                                        <asp:TextBox ID="TextBox18" AutoComplete="off" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"> <asp:Label ID="Label5" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                                        <asp:TextBox ID="TextBox19" AutoComplete="off" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-4 control-label">  PCB Amaun (RM) </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox24" runat="server" style="text-align:right;" class="form-control validate[optional, custom[number]] txtacm1"></asp:TextBox>
                                      
                                    </div>
                                </div>
                            </div>
                                   <%-- <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  CP38 Amaun (RM) </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox25" runat="server" style="text-align:right;" class="form-control "></asp:TextBox>
                                        <asp:TextBox ID="TextBox26" runat="server" class="form-control validate[optional]" style="display:none;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>--%>
                                 </div>
                                </div>

                

                         <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                <asp:Button ID="Button15" runat="server" class="btn btn-danger" Text="Simpan (PCB)" Type="submit" onclick="Button15_Click" />
                                                        <asp:Button ID="Button16" runat="server" Text="Kemaskini (PCB)" class="btn btn-danger" Visible="false" onclick="Button16_Click" />
                                                        <asp:Button ID="Button17" runat="server" Text="Hapus (PCB)" class="btn btn-warning" onclick="Button17_Click" OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;"/>
                            </div>
                           </div>
                               </div>
                      <div class="box-body">&nbsp;
                                    </div>

                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                   <asp:GridView ID="GridView5" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging_3">
                                       <PagerStyle CssClass="pager" />
                                      <Columns>
                                       <asp:TemplateField HeaderText="BIL" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="150"/> 
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                      <asp:TemplateField HeaderText="TARIKH MULA">  
                                            <ItemTemplate>  
                                               <asp:LinkButton ID="lblSubItem"    runat="server" Text='<%# Eval("tax_pcb_start_dt")%>' CommandArgument=' <%#Eval("tax_pcb_start_dt")+","+ Eval("tax_pcb_end_dt")%>' CommandName="Add"  onclick="lblSubItemName_Click_cukai" Font-Bold Font-Underline>
                                                <a  href="#"></a>
                                                  </asp:LinkButton>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Tax No">  
                                            <ItemTemplate>  
                                               <asp:Label ID="lblSubno"    runat="server" Text='<%# Eval("tax_incometax_no")%>' > </asp:Label>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                      
                                      
                                        <asp:BoundField DataField="tax_pcb_end_dt" HeaderText="TARIKH SEHINGGA"  />
                                         <asp:BoundField DataField="tax_pcb_amt" ItemStyle-HorizontalAlign="Right" HeaderText="PCB Amaun (RM)" DataFormatString="{0:n}"  />   
                                             <asp:TemplateField HeaderText="Hapus" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:RadioButton ID="RadioButton1" runat="server" onclick = "RadioCheck(this);" />
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

                             <div class="box-body">&nbsp;
                                    </div>

                            <div class="box-header with-border">
                            <h3 class="box-title"> Maklumat Potongan Statutori – CP 38 </h3>
                        </div>
                            <div class="box-body">&nbsp; </div>
                          
                            <div class="row">
                            <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">  Dari </label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                                        <asp:TextBox ID="TextBox31" AutoComplete="off" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"> Hingga </label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                                        <asp:TextBox ID="TextBox33" AutoComplete="off" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-4 control-label">  CP 38 Amaun (RM) </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox34" runat="server" style="text-align:right;" class="form-control validate[optional, custom[number]] txtacm1"></asp:TextBox>
                                      
                                    </div>
                                </div>
                            </div>
                                   <%-- <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">  CP38 Amaun (RM) </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox25" runat="server" style="text-align:right;" class="form-control "></asp:TextBox>
                                        <asp:TextBox ID="TextBox26" runat="server" class="form-control validate[optional]" style="display:none;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>--%>
                                 </div>
                                </div>

                

                         <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                <asp:Button ID="Button18" runat="server" class="btn btn-danger" Text="Simpan (CP 38)" Type="submit" onclick="CP_Button15_Click" />
                                                        <asp:Button ID="Button19" runat="server" Text="Kemaskini (CP 38)" class="btn btn-danger" Visible="false" onclick="CP_Button16_Click" />
                                                        <asp:Button ID="Button20" runat="server" Text="Hapus (CP 38)" class="btn btn-warning" onclick="CP_Button17_Click" OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;"/>
                            </div>
                           </div>
                               </div>
                      <div class="box-body">&nbsp;
                                    </div>

                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                   <asp:GridView ID="GridView6" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging_3">
                                       <PagerStyle CssClass="pager" />
                                      <Columns>
                                       <asp:TemplateField HeaderText="BIL" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="150"/> 
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                      <asp:TemplateField HeaderText="TARIKH MULA">  
                                            <ItemTemplate>  
                                               <asp:LinkButton ID="lblSubItem"    runat="server" Text='<%# Eval("tax_pcb_start_dt")%>' CommandArgument=' <%#Eval("tax_pcb_start_dt")+","+ Eval("tax_pcb_end_dt")%>' CommandName="Add"  onclick="CP_lblSubItemName_Click_cukai" Font-Bold Font-Underline>
                                                <a  href="#"></a>
                                                  </asp:LinkButton>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                         
                                        <asp:BoundField DataField="tax_pcb_end_dt" HeaderText="TARIKH SEHINGGA"  />
                                         <asp:BoundField DataField="tax_cp38_amt1" ItemStyle-HorizontalAlign="Right" HeaderText="PCB Amaun (RM)" DataFormatString="{0:n}"  />   
                                             <asp:TemplateField HeaderText="Hapus" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:RadioButton ID="RadioButton1" runat="server" onclick = "RadioCheck(this);" />
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

                             <div class="box-header with-border">
                            <h3 class="box-title"><asp:Label ID="ps_lbl35" runat="server"></asp:Label> </h3>
                        </div>
                            <div class="box-body">&nbsp; </div>
                            <div class="row" style="display:none;">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">   Dari  </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                                        <asp:TextBox ID="TextBox12" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"> Hingga </label>
                                    <div class="col-sm-8">
                                       <div class="input-group">
                                                    <asp:TextBox ID="TextBox11" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-4 control-label">   <asp:Label ID="ps_lbl36" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox5" runat="server" class="form-control validate[optional] uppercase" MaxLength="12" readonly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"><asp:Label ID="ps_lbl37" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox6" runat="server" class="form-control validate[optional] " MaxLength="12" readonly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">  <asp:Label ID="ps_lbl38" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox7" style="text-align:right;" runat="server" class="form-control validate[optional, custom[number]] "></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"> <asp:Label ID="ps_lbl39" runat="server"></asp:Label> </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox3" style="text-align:right;" runat="server" class="form-control validate[optional, custom[number]] "></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                <div class="row" style="display:none;">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"> </label>
                                    <div class="col-sm-8">
                                     <label> <asp:CheckBox ID="perkeso_chk" runat="server" /> &nbsp;&nbsp;&nbsp; Assign Perkeso</label>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                               <div class="row" style="display:none;">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                <asp:Button ID="Button3" runat="server" class="btn btn-danger" Text="Simpan" Type="submit" onclick="perkeso_ins_Click" />
                                                        <asp:Button ID="Button4" runat="server" Text="Kemaskini" class="btn btn-danger" Visible="false" onclick="perkeso_upd_Click" />
                                                        <asp:Button ID="Button6" runat="server" Text="Hapus" class="btn btn-warning" onclick="perkeso_del_Click" OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;"/>
                            </div>
                           </div>
                               </div>
                             <div class="box-body">&nbsp;
                                    </div>
                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto; display:none;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                   <asp:GridView ID="GridView3" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging_3">
                                       <PagerStyle CssClass="pager" />
                                      <Columns>
                                       <asp:TemplateField HeaderText="BIL" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="150"/> 
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Dari">  
                                            <ItemTemplate>  
                                               <asp:LinkButton ID="lblSubItem1"    runat="server" Text='<%# Eval("st_dt")%>' CommandArgument=' <%#Eval("st_dt")+","+ Eval("ed_dt")%>' CommandName="Add"  onclick="lblSubItemName_Click_perkeso" Font-Bold Font-Underline>
                                                <a  href="#"></a>
                                                  </asp:LinkButton>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                      
                                        <asp:BoundField DataField="ed_dt" HeaderText="Hingga"  />
                                         <asp:BoundField DataField="perk_ahli_no" HeaderText="Perkeso Ahli No" ItemStyle-HorizontalAlign="Left" />   
                                          <asp:BoundField DataField="tax_cp38_amt1" HeaderText="Perkeso Ahli (Rm)" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" /> 
                                          <asp:BoundField DataField="perk_mjik_no" HeaderText="Perkeso Majikan No" ItemStyle-HorizontalAlign="Left" />     
                                         <asp:BoundField DataField="tax_cp38_amt2" ItemStyle-HorizontalAlign="Right" HeaderText="Perkeso Majikan (Rm)"  DataFormatString="{0:n}"  />   
                                             <asp:TemplateField HeaderText="Hapus" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:RadioButton ID="RadioButton1" runat="server" onclick = "RadioCheck(this);" />
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

                             <div class="box-header with-border">
                            <h3 class="box-title">Maklumat Potongan Statutori - SIP  </h3>
                        </div>
                            <div class="box-body">&nbsp; </div>
                             <div class="row" style="display:none;">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">   Dari  </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                                        <asp:TextBox ID="TextBox8" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"> Hingga </label>
                                    <div class="col-sm-8">
                                       <div class="input-group">
                                                    <asp:TextBox ID="TextBox9" runat="server" class="form-control validate[optional,custom[dtfmt]] datepicker mydatepickerclass" placeholder="DD/MM/YYYY"></asp:TextBox>
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
                                    <label for="inputEmail3" class="col-sm-4 control-label">  No SIP Ahli </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox20" runat="server" class="form-control validate[optional] uppercase" MaxLength="12" readonly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"> No SIP Majikan </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="TextBox21" runat="server" class="form-control validate[optional] " MaxLength="12" readonly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>

                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">  SIP Ahli (RM) </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox22" style="text-align:right;" runat="server" class="form-control validate[optional, custom[number]] "></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"> SIP Majikan (RM) </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox23" style="text-align:right;" runat="server" class="form-control validate[optional, custom[number]] "></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                           <div class="row" style="display:none;">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"> </label>
                                    <div class="col-sm-8">
                                     <label> <asp:CheckBox ID="sip_chk" runat="server" /> &nbsp;&nbsp;&nbsp; Assign SIP</label>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                 <hr />
                           <div class="row" style="display:none;">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                <asp:Button ID="Button5" runat="server" class="btn btn-danger" Text="Simpan" UseSubmitBehavior="false" Type="submit" onclick="sip_ins_Click" />
                                    <asp:Button ID="Button7" runat="server" Text="Kemaskini" class="btn btn-danger" Visible="false" onclick="sip_upd_Click" />
                                                        <asp:Button ID="Button11" runat="server" Text="Hapus" class="btn btn-warning" onclick="sip_del_Click" OnClientClick="if (!confirm('Adakah Anda pasti?')) return false;"/>
                                <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" Type="submit" onclick="Click_bck" />                    
                                </div>
                                 </div>
                               </div>
                             <div class="box-body">&nbsp;
                                    </div>
                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto; display:none;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                   <asp:GridView ID="GridView4" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="25" ShowFooter="false" GridLines="None" OnPageIndexChanging="gvSelected_PageIndexChanging_3">
                                       <PagerStyle CssClass="pager" />
                                      <Columns>
                                       <asp:TemplateField HeaderText="BIL" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="150"/> 
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Dari">  
                                            <ItemTemplate>  
                                               <asp:LinkButton ID="lblSubItem2"    runat="server" Text='<%# Eval("st_dt")%>' CommandArgument=' <%#Eval("st_dt")+","+ Eval("ed_dt")%>' CommandName="Add"  onclick="lblSubItemName_Click_sip" Font-Bold Font-Underline>
                                                <a  href="#"></a>
                                                  </asp:LinkButton>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                      
                                        <asp:BoundField DataField="ed_dt" HeaderText="Hingga"  />
                                              <asp:BoundField DataField="sip_ahli_no" HeaderText="Perkeso Ahli No" ItemStyle-HorizontalAlign="Left" />   
                                         <asp:BoundField DataField="tax_sip_amt1" HeaderText="SIP Ahli (Rm)" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" /> 
                                              <asp:BoundField DataField="sip_majikan_no" HeaderText="Perkeso Ahli No" ItemStyle-HorizontalAlign="Left" />     
                                         <asp:BoundField DataField="tax_sip_amt2" ItemStyle-HorizontalAlign="Right" HeaderText="SIP Majikan (Rm)"  DataFormatString="{0:n}"  />   
                                             <asp:TemplateField HeaderText="Hapus" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:RadioButton ID="RadioButton1" runat="server" onclick = "RadioCheck(this);" />
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

                           <div class="box-body">&nbsp;
                                    </div>

                        </div>

                    </div>
                </div>
            </div>
            <!-- /.row -->
              </ContentTemplate>
          <%--   <Triggers>
               <asp:PostBackTrigger ControlID="Button4"  />
               <asp:PostBackTrigger ControlID="btb_kmes"  />
           </Triggers>--%>
    </asp:UpdatePanel>
        
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
  
</asp:Content>


