<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="Penerimaan_Butiran_TKH.aspx.cs" Inherits="TKH_Penerimaan_Butiran_TKH" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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

    <script type="text/javascript" src="Scripts/jquery-1.8.2.js"></script> <!-- call jquery file or library into asp.net page --->
    <script>
        $(document).ready(function() { // this is a function which is called by browser when it loads a webpage
 
            var $Submitbtn = $('[id$="Button2"]'); // get the button control and assign into a variable
            var $checkbox = $('[id$="checkAll"]'); // get checkbox control, assign it into a variable

            var $dropdown = $('[id$="ddsts"]');
 
            // check on page load
            checkChecked($checkbox);
 
            $checkbox.click(function () { // when user clicks on checkbox an click event will be triggered
                checkChecked($checkbox); // call checkChecked() function, pass checkbox to the function as an argument
            });
 
            function checkChecked(chkBox) { // this function will be called, pass one parameter checkbox
                if (chkBox.is(":checked")) { // checks if that checkbox is checked using is() method
                    $Submitbtn.removeAttr('disabled'); // submit button control will be enabled using removeAttr() method
                    $dropdown.removeAttr('disabled');
                } else { // otherwise 
                    $Submitbtn.attr('disabled', 'disabled'); // if checkbox is not checked, disabled the submit button
                    $dropdown.attr('disabled', 'disabled'); // if checkbox is not checked, disabled the submit button
                }
            }
 
        });
 
    </script>

    <script type="text/javascript">
                
        function DisableButton()
        {
            var frm = document.forms[0];
            
            for (i=0;i<frm.elements.length;i++)
            {
                if (frm.elements[i].type =="checkbox")
                {
                    if (frm.elements[i].checked == false)
                    {
                        document.getElementById("<%= Button2.ClientID %>").disabled = true;
                    }
                    else
                    {
                        document.getElementById("<%= Button2.ClientID %>").disabled = false;
                        return;
                    }
                }
            }
        }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <asp:ScriptManager ID="ScriptManagerCalendar" runat="server" ScriptMode="Release">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
         <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
                <div class="content-wrapper" style="height: 895px;">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>PENERIMAAN BUTIRAN TKH</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>TKH</a></li>
                            <li class="active">SUMBAKAN TKH / PENERIMAAN BUTIRAN TKH</li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
                     
  
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title"></h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                         <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">

                               
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="lblbatch" class="col-sm-3 control-label">No Batch <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                      
                                             <asp:Label ID="lblbatchview" runat="server" Text=""></asp:Label>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="lbldis" class="col-sm-3 control-label">Disediakan <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                      
                                          <asp:Label ID="lbldisview" runat="server" Text=""></asp:Label>
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
                                    <label for="lbltardij" class="col-sm-3 control-label">Tarikh Dijina <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                      
                                       <asp:Label ID="lbltardijview" runat="server" Text=""></asp:Label>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="lbldisemak" class="col-sm-3 control-label">Disemak <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                       
                                        <asp:Label ID="lbldisemakview" runat="server" Text=""></asp:Label>
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
                                    <label for="lbldijole" class="col-sm-3 control-label">Dijina Oleh<span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                       
                                      <asp:Label ID="lbldijoleview" runat="server" Text=""></asp:Label>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="lbldisahkan" class="col-sm-3 control-label">Disahkan <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                         <asp:Label ID="lbldisahkanview" runat="server" Text=""></asp:Label>
                                      
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
                                    <label for="lbldijole" class="col-sm-3 control-label">Wilayah<span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                     
                                      <asp:Label ID="lblwilview" runat="server" Text=""></asp:Label>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="lblwil" class="col-sm-3 control-label"> <span class="style1"></span> </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                       
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
                                    <label for="lblsts" class="col-sm-3 control-label">Status<span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                    
                                        <asp:Label ID="lblstsview" runat="server" Text=""></asp:Label>
                                             </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="lblwil" class="col-sm-3 control-label"> <span class="style1"></span> </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                         <label for="lblwilview" class="col-sm-3 control-label"> <span class="style1"></span> </label>
                                      
                                             </div>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                       
                         <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                              
                            </div>
                           </div>
                               </div>
                              <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">
                                      <%--<div class="row" >--%>
           <div class="col-md-12 box-body">
                                   <asp:GridView ID="gv_refdata" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="1000000" OnRowDataBound = "RowDataBound" >
                                                    <Columns>
                                                    <asp:TemplateField HeaderText="BIL">  
                                                        <ItemStyle HorizontalAlign="center" Width="3%"></ItemStyle>
                                            <ItemTemplate>  
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ItemStyle-Width="3%"/> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cawangan" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                               
                                                                <asp:Label ID="lb_1" runat="server" Text='<%# Eval("tkh_tt_area_cd") %>'></asp:Label>
                                                                   
                                                                  
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="No Akaun P1">
                                                            <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_2" runat="server" Text='<%# Eval("tkh_tt_no_shbt") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Nama Pencarum" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_3" runat="server" Text='<%# Eval("tkh_tt_name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>  
                                                        <asp:TemplateField HeaderText="No.KP" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_4" runat="server" Text='<%# Eval("tkh_tt_ic") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Umur" HeaderStyle-HorizontalAlign="Left">
                                                              <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_5" runat="server" Text='<%# Eval("thk_tt_age") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>  
                                                         <asp:TemplateField HeaderText="Produk" HeaderStyle-HorizontalAlign="Left">
                                                              <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_6" runat="server" Text='<%# Eval("tkh_tt_produk") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>  
                                                        <asp:TemplateField HeaderText="Jumlah Pinjaman (RM)" HeaderStyle-HorizontalAlign="Right">
                                                              <ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_7" runat="server" Text='<%# Eval("tkh_tt_pinjaman_amt","{0:n}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>  
                                                        <asp:TemplateField HeaderText="Tempoh" HeaderStyle-HorizontalAlign="Left">
                                                              <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_8" runat="server" Text='<%# Eval("tkh_tt_tempoh") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>  
                                                        <asp:TemplateField HeaderText="Jumlah Caji (RM)" HeaderStyle-HorizontalAlign="Right">
                                                              <ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_9" runat="server" Text='<%# Eval("tkh_tt_caj_amt","{0:n}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>  
                                                        <asp:TemplateField HeaderText="Jumlah Perlindungan (RM)" HeaderStyle-HorizontalAlign="Right">
                                                              <ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_10" runat="server" Text='<%# Eval("tkh_tt_lindung_amt","{0:n}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Manafaat (RM)" HeaderStyle-HorizontalAlign="Right">
                                                              <ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_11" runat="server" Text='<%# Eval("tkh_tt_manfaat_amt","{0:n}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Jumlah Caruman (RM)" HeaderStyle-HorizontalAlign="Right">
                                                              <ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_12" runat="server" Text='<%# Eval("tkh_tt_caruman_amt","{0:n}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField> 
                                                         <asp:TemplateField HeaderText="HTTKS" HeaderStyle-HorizontalAlign="Left">
                                                              <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_13" runat="server" Text='<%# Eval("tkh_tt_httks") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField> 
                                                          <asp:TemplateField HeaderText="TKH Mula Takaful" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_14" runat="server" Text='<%# Eval("tkh_tt_mula_dt") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>  
                                                        <asp:TemplateField HeaderText="TKH Akhir Takaful" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_15" runat="server" Text='<%# Eval("tkh_tt_akhir_dt") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField> 
                                                        <asp:TemplateField>

    <HeaderTemplate>

      <asp:CheckBox ID="checkAll" runat="server" onclick = "checkAll(this) ;"   />

    </HeaderTemplate>

   <ItemTemplate>

     <asp:CheckBox ID="CheckSin"  runat="server"  onclick = "Check_Click(this)"    />

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
                           
                            <div class="box-body">&nbsp;</div>
                        </div>
                        
                        <div class="form-horizontal" id ="w1" runat="server">
                               <h1>KELULUSAN WILAYAH</h1>
                               
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="lblbatch" class="col-sm-3 control-label">Status <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                             <asp:DropDownList ID="ddsts" runat="server">
                                                 <asp:ListItem >Select</asp:ListItem>
                                                  <asp:ListItem Value="L">Lulus</asp:ListItem>
                                                  <asp:ListItem Value="D">DI PULANGKAN</asp:ListItem>
                                             </asp:DropDownList>
                                             
                                             </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="lbldis" class="col-sm-3 control-label">Dikemaskini oleh <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                         <asp:Label ID="lblDol" runat="server" Text=""></asp:Label>    
                                     
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
                                    <label for="lblbatch" class="col-sm-3 control-label">Ulasan <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                            <asp:TextBox ID="txtula" runat="server"></asp:TextBox>
                                             
                                             </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="lbldis" class="col-sm-3 control-label">Tarikh Kemaskini <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                           <asp:Label ID="lbltark" runat="server" Text=""></asp:Label>    
                                     
                                             </div>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                    </div>
                              <div class="form-horizontal" id ="H1" runat="server">
                               <h1>PENGESAHAN IBU PEJABAT</h1>
                               
                             <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="lblbatch" class="col-sm-3 control-label">Status <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                             <asp:DropDownList ID="DDSTS1" runat="server">
                                                 <asp:ListItem Value="L">Lulus</asp:ListItem>
                                             </asp:DropDownList>
                                             
                                             </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="lbldis" class="col-sm-3 control-label">Dikemaskini oleh <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                         <asp:Label ID="lblDol1" runat="server" Text=""></asp:Label>    
                                     
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
                                    <label for="lblbatch" class="col-sm-3 control-label">Ulasan <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                            <asp:TextBox ID="txtula1" runat="server"></asp:TextBox>
                                             
                                             </div>
                                    </div>
                                </div>
                            </div>
                                  <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="lbldis" class="col-sm-3 control-label">Tarikh Kemaskini <span class="style1">*</span> </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                           <asp:Label ID="lbltark1" runat="server" Text=""></asp:Label>    
                                     
                                             </div>
                                    </div>
                                </div>
                            </div>
                                 </div>
                         </div>
                    </div>
                         <hr />
                         <div class="row" id="car" runat="server">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                <asp:Button ID="Button1" runat="server" class="btn btn-danger sub_btn" Text="Kemaskini"  OnClick="btn_simpan"   UseSubmitBehavior="false" />
                                <asp:Button ID="Button2" runat="server" class="btn btn-danger sub_btn" Text="Hantar"  OnClick="btn_hantar"   UseSubmitBehavior="false" />
                                <asp:Button ID="Button3" runat="server" class="btn btn-default" Text="Tutub"   OnClick="btn_tutub"  UseSubmitBehavior="false" />
                            </div>
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

