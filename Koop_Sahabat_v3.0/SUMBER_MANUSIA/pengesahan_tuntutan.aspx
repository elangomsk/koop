<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/pengesahan_tuntutan.aspx.cs" Inherits="pengesahan_tuntutan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server" >
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
      
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server" ScriptMode="Release">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>  Kelulusan Tuntutan </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>  Sumber Manusia </a></li>
                            <li class="active"> Kelulusan Tuntutan  </li>
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
                            <h3 class="box-title">Carian Maklumat Tuntutan </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                            <div class="row">
                                  <div class="col-md-12">
                              <div class="col-md-10 box-body">
                                <div class="form-group">
                                    <div class="col-sm-4 col-xs-12 ">
                                         <%--<asp:TextBox ID="txt_tahun" runat="server" CssClass="form-control validate[optional,custom[number]] uppercase"></asp:TextBox>--%>
                                                                <asp:DropDownList style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"
                                                                    ID="txt_tahun">
                                                                    <%--onselectedindexchanged="dd_kat_SelectedIndexChanged">--%>
                                                                </asp:DropDownList>
                                    </div>
                                      <div class="col-sm-4 col-xs-12 mob-view-top-padd">
                                           <asp:DropDownList style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]"
                                                                    ID="DD_bulancaruman">
                                                                    <%--onselectedindexchanged="dd_kat_SelectedIndexChanged">--%>
                                                                </asp:DropDownList>
                                          </div>
                                    <div class="col-sm-4 col-xs-12 mob-view-top-padd">
                                         <label><asp:CheckBox ID="chk_assign_rkd" runat="server" /> &nbsp;Verified</label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                           <asp:Button ID="Button2" runat="server" class="btn btn-primary" Text="Carian" OnClick="BindGridview"/>
                                                                <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" usesubmitbehavior="false" OnClick="Reset_btn"/>
                                        </div>
                                    <div class="col-sm-4 col-xs-12 mob-view-top-padd">
                                        </div>
                                </div>
                            </div>
                                      </div>
                                </div>
                             
                            <hr />
                         
                         
                            <div id="show_cnt1" runat="server">
                             <div class="box-header with-border">
                            <h3 class="box-title">Senarai Kelulusan Tuntutan</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
                              <div class="dataTables_wrapper form-inline dt-bootstrap" style="overflow:auto;">                                   
         <div class="col-md-12 box-body">
                                     <asp:GridView ID="gvSelected" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="30" ShowFooter="false" GridLines="None" onrowdatabound="gvCustomers_RowDataBound" OnPageIndexChanging="gvSelected_PageIndexChanging">
                                         <PagerStyle CssClass="pager" />
                                        <Columns>
                                        <asp:TemplateField HeaderText="BIL"  ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                             <asp:TemplateField HeaderText="No Kakitangan"> 
                                                <ItemStyle HorizontalAlign="Left" />    
                                            <ItemTemplate>  
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("stf_staff_no") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                       <asp:TemplateField HeaderText="IC No">
                                            <ItemTemplate>  
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("stf_icno") %>'></asp:Label> 
                                                <asp:Label ID="Label4_mnth" Visible="false" runat="server" Text='<%# Bind("clm_rec_dt") %>'></asp:Label>   
                                                <asp:Label ID="Label1_org_id" Visible="false" runat="server" Text='<%# Bind("clm_claim_cd") %>'></asp:Label>  
                                                <asp:Label ID="lbl_fil_name" Visible="false" runat="server" Text='<%# Eval("file_name") %>'></asp:Label> 
                                                 <asp:Label ID="kod_akan" Visible="false" runat="server" Text='<%# Eval("stf_kod_akaun") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Nama Kakitangan"> 
                                                <ItemStyle HorizontalAlign="Center" />    
                                            <ItemTemplate>  
                                                <asp:Label ID="Label2_name" runat="server" Text='<%# Bind("stf_name") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Receipt Date"> 
                                                <ItemStyle HorizontalAlign="Center" />    
                                            <ItemTemplate>  
                                                <asp:Label ID="Label2_yr" runat="server" Text='<%# Bind("clm_rec_dt1") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Apply Date" ItemStyle-HorizontalAlign="center">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("clm_app_dt1") %>'></asp:Label>  
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Jenis Tuntutan" ItemStyle-HorizontalAlign="Left">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("hr_tun_desc") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="SEBAB">
                                                                                            <ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label4_seb" runat="server" Text='<%# Eval("clm_sebap") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Amuan (RM)" ItemStyle-HorizontalAlign="right">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label6_amt" runat="server" Text='<%# Bind("clm_claim_amt","{0:n}") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Baki / Jumlah Terkini (RM)" ItemStyle-HorizontalAlign="right">   
                                            <ItemTemplate>  
                                                <asp:Label ID="Label6_amt1" runat="server" Text='<%# Bind("clm_balance_amt","{0:n}") %>'></asp:Label> 
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="File Name" ItemStyle-HorizontalAlign="center">   
                                            <ItemTemplate>  
                                                 <asp:Panel ID="pnlProducts" runat="server" Visible="true" Style="position: relative" >
                                                        <asp:GridView ID="gvProducts" ShowHeader="False" GridLines="None" runat="server" AutoGenerateColumns="false" PageSize="10"
                                                            AllowPaging="true" CssClass="Nested_ChildGrid">
                                                            <Columns>
                                                                  <asp:TemplateField HeaderText="File Name">
                                                             <ItemStyle HorizontalAlign="center" Width="50%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl1" runat="server" Text='<%# Eval("td1_name") %>'></asp:Label>
                                                                       <asp:Label ID="lbl_id" Visible="false" runat="server" Text='<%# Bind("ID") %>' CssClass="uppercase"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Download" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                    <ItemTemplate>
                         <asp:UpdatePanel ID="aa" runat="server">
                    <ContentTemplate>
                      <asp:LinkButton runat="server" ID="lnkView11" OnClick="lnkView_Click11">
                                                                <asp:Label ID="lbl3" runat="server" Text='Download'></asp:Label>
                                                                </asp:LinkButton>
                          </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger  ControlID="lnkView11"/>
                    </Triggers>
              </asp:UpdatePanel>
                    </ItemTemplate>
                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                            </ItemTemplate>  
                                                </asp:TemplateField>
                                                <%-- <asp:TemplateField HeaderText="Permission" ItemStyle-HorizontalAlign="center" ItemStyle-Width="3%">
                                                                       <HeaderTemplate>
                                                                           LULUS<br/>
                                            <asp:CheckBox ID="chkAll" runat="server" Text="" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged"
                                                        ItemStyle-Width="150" />
                                            </HeaderTemplate>  
                                                                    <ItemTemplate>                                                                        
                                                                           <asp:CheckBox ID="chkSelect"  runat="server" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField> --%>
                                            <asp:TemplateField HeaderText="Permission" ItemStyle-HorizontalAlign="center" ItemStyle-Width="3%">

    <HeaderTemplate>
        Lulus<br/>
      <asp:CheckBox ID="chkAll" runat="server" onclick = "checkAll(this);" />

    </HeaderTemplate>

   <ItemTemplate>

     <asp:CheckBox ID="chkSelect" runat="server" onclick = "Check_Click(this)" />

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
                       <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-12 box-body" style="text-align:center;">
                                  <asp:Button ID="Button3" runat="server" class="btn btn-warning" Visible="false" Text="Lulus" UseSubmitBehavior="false" OnClick="submit_button"/>
                                <asp:Button ID="Button6" runat="server" class="btn btn-danger" Visible="false" Text="Tidak Lulus" UseSubmitBehavior="false" OnClick="submit_button_tl"/>
                                            <asp:Button ID="Button4" runat="server" class="btn btn-default" Visible="false" Text="Batal" />
                                            <asp:Button ID="Button5" runat="server" class="btn btn-danger"  Text="Cetak" Visible="false" />
                            </div>
                           </div>
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
           
    </asp:UpdatePanel>
        
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
   
</asp:Content>


