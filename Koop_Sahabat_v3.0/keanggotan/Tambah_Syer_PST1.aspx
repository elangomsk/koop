<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../keanggotan/Tambah_Syer_PST1.aspx.cs" Inherits="Tambah_Syer_PST1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
         <script type = "text/javascript" >

function checkAll(objRef)

{

    var GridView = objRef.parentNode.parentNode.parentNode;

    var inputList = GridView.getElementsByTagName("input");

    for (var i=0;i<inputList.length;i++)

    {

        //Get the Cell To find out ColumnIndex

        var row = inputList[i].parentNode.parentNode;

        if(inputList[i].type == "checkbox"  && objRef != inputList[i])

        {

            if (objRef.checked)

            {

                //If the header checkbox is checked

                //check all checkboxes

                //and highlight all rows

                row.style.backgroundColor = "aqua";

                inputList[i].checked=true;

            }

            else

            {

                //If the header checkbox is checked

                //uncheck all checkboxes

                //and change rowcolor back to original

                if(row.rowIndex % 2 == 0)

                {

                   //Alternating Row Color

                   row.style.backgroundColor = "#C2D69B";

                }

                else

                {

                   row.style.backgroundColor = "white";

                }

                inputList[i].checked=false;

            }

        }

    }

}

</script> 
<script type = "text/javascript">

function Check_Click(objRef)

{

    //Get the Row based on checkbox

    var row = objRef.parentNode.parentNode;

    if(objRef.checked)

    {

        //If checked change color to Aqua

        row.style.backgroundColor = "aqua";

    }

    else

    {   

        //If not checked change back to original color

        if(row.rowIndex % 2 == 0)

        {

           //Alternating Row Color

           row.style.backgroundColor = "#C2D69B";

        }

        else

        {

           row.style.backgroundColor = "white";

        }

    }

   

    //Get the reference of GridView

    var GridView = row.parentNode;

   

    //Get all input elements in Gridview

    var inputList = GridView.getElementsByTagName("input");

   

    for (var i=0;i<inputList.length;i++)

    {

        //The First element is the Header Checkbox

        var headerCheckBox = inputList[0];

       

        //Based on all or none checkboxes

        //are checked check/uncheck Header Checkbox

        var checked = true;

        if(inputList[i].type == "checkbox" && inputList[i] != headerCheckBox)

        {

            if(!inputList[i].checked)

            {

                checked = false;

                break;

            }

        }

    }

    headerCheckBox.checked = checked;

   

}

</script>

    <script type = "text/javascript">

function MouseEvents(objRef, evt)

{

    var checkbox = objRef.getElementsByTagName("input")[0];

   if (evt.type == "mouseover")

   {

        objRef.style.backgroundColor = "orange";

   }

   else

   {

        if (checkbox.checked)

        {

            objRef.style.backgroundColor = "aqua";

        }

        else if(evt.type == "mouseout")

        {

            if(objRef.rowIndex % 2 == 0)

            {

               //Alternating Row Color

               objRef.style.backgroundColor = "#C2D69B";

            }

            else

            {

               objRef.style.backgroundColor = "white";

            }

        }

   }

}

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      
    <asp:ScriptManager ID="ScriptManagerCalendar" AsyncPostBackTimeOut="72000" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
      <script type="text/javascript">
             // Get the instance of PageRequestManager.
             var prm = Sys.WebForms.PageRequestManager.getInstance();
             // Add initializeRequest and endRequest
             prm.add_initializeRequest(prm_InitializeRequest);
             prm.add_endRequest(prm_EndRequest);

             // Called when async postback begins
             function prm_InitializeRequest(sender, args) {
                 // get the divImage and set it to visible
                 var panelProg = $get('divImage');
                 panelProg.style.display = '';
                 // reset label text
                 var lbl = $get('<%= this.lblText.ClientID %>');
                 lbl.innerHTML = '';

                 // Disable button that caused a postback
                 $get(args._postBackElement.id).disabled = true;
             }

             // Called when async postback ends
             function prm_EndRequest(sender, args) {
                 // get the divImage and hide it again
                 var panelProg = $get('divImage');
                 panelProg.style.display = 'none';

                 // Enable button that caused a postback
                 $get(sender._postBackSettings.sourceElement.id).disabled = false;
             }
         </script>

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1> <asp:Label ID="ps_lbl1" runat="server"></asp:Label></h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>  <asp:Label ID="ps_lbl2" runat="server"></asp:Label> </a></li>
                            <li class="active">    <asp:Label ID="ps_lbl3" runat="server"></asp:Label> </li>
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
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl5" runat="server"></asp:Label> <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                    <div class="input-group">
                                                       <asp:TextBox ID="TextBox12" runat="server" AutoComplete="off" class="form-control validate[optional] datepicker mydatepickerclass" placeholder="Pick Date"></asp:TextBox>
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                       </div>
                                    </div>
                                </div>
                            </div>
                                 <div class="col-md-6 box-body">
                                <div class="form-group">                                    
                                    <div class="col-sm-8">
                                       <asp:CheckBox ID="s_update" runat="server" CssClass="mycheckbox" Text=" Semua Rekod"/>
                                        
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"> <asp:Label ID="ps_lbl7" runat="server"></asp:Label> <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                                        <asp:TextBox ID="TextBox2" runat="server" class="form-control validate[optional] datepicker mydatepickerclass" AutoComplete="off" placeholder="Pick Date"></asp:TextBox>
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
                                <asp:Button ID="Button2" runat="server" class="btn btn-primary" Text="Carian" OnClick="BindGridview"/>
                                                                     <asp:Button ID="Button3" runat="server" class="btn btn-default" UseSubmitBehavior="false" OnClick="Reset_btn" Text="Set Semula" />
                                 
                            </div>
                           </div>
                               </div>
                           <div class="box-body">&nbsp;
                                    </div>
                             <div id="show_cnt1" runat="server" visible="false">
                             <div class="box-header with-border">
                            <h3 class="box-title">Senarai Maklumat Anggota Untuk Tambah Syer PST</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>

                                <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                     <%-- <div class="row" >--%>
            <div class="col-md-12 box-body">
                   <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
                 <div id="divImage" class="text-center" style="display:none; padding-top: 30px; font-weight:bold;">
                     <asp:Image ID="img1" runat="server" ImageUrl="../dist/img/LoaderIcon.gif" />&nbsp;&nbsp;&nbsp;Processing Please wait ... </div> 
                                    <asp:GridView ID="gvSelected" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="30" ShowFooter="false" GridLines="None" OnRowDataBound="gvUserInfo_RowDataBound"  OnPageIndexChanging="gvSelected_PageIndexChanging">
                                        <PagerStyle CssClass="pager" />
                                        <Columns>
                                        <asp:TemplateField HeaderText="BIL">   
                                            <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="150"/>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NO KP">  
                                            <ItemTemplate>  
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("mem_new_icno") %>'></asp:Label> 
                                                <asp:Label ID="lbl_id" Visible=false runat="server" Text='<%# Bind("Flag") %>'></asp:Label>  
                                                <asp:Label ID="lbl_cd" Visible=false runat="server" Text='<%# Bind("mem_sts_cd") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NAMA" ItemStyle-HorizontalAlign="Left">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("mem_name") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NO ANGGOTA">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label11" runat="server" Text='<%# Bind("mem_member_no") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CAWANGAN" ItemStyle-HorizontalAlign="Left">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("cawangan_name") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PUSAT" ItemStyle-HorizontalAlign="Left">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("mem_centre") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JUMLAH (RM)" ItemStyle-HorizontalAlign="Right">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("pst_balance_amt","{0:n}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TARIKH WP4">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("pst_post_dt", "{0:dd/MM/yyyy}") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NO WP4">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label8" runat="server" Text='<%# Bind("pst_wp4_no") %>'></asp:Label>   
                                                <asp:Label Visible="false" ID="Label9" runat="server" Text='UN0001'></asp:Label>  
                                                <asp:Label Visible="false" ID="Label10" runat="server" Text='V'></asp:Label>
                                                <asp:Label Visible="false" ID="pst_date" runat="server" Text='<%# Eval("pst_post_dt") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PILIH">  
                                                <HeaderTemplate>
                                            <asp:CheckBox ID="chkAll" runat="server" Text="&nbsp;" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged" ItemStyle-Width="150"/>
                                            </HeaderTemplate>  
                                            <ItemTemplate>  
                                                 <asp:CheckBox ID="chkSelect" runat="server" />
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
                             <div class="box-body">&nbsp;</div>
                             <hr />
                           <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                   <asp:Button ID="Button1" runat="server" class="btn btn-danger" Text="Simpan" OnClick="Save"/>
                            </div>
                           </div>
                               </div>
                           <div class="box-body">&nbsp;
                                    </div>
                                 </div>
                        </div>

                    </div>
                </div>
            </div>
            <!-- /.row -->
             </ContentTemplate>
    </asp:UpdatePanel>
        
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
   
</asp:Content>
