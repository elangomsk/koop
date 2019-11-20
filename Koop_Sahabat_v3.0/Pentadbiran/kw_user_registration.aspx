<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Pentadbiran/kw_user_registration.aspx.cs" Inherits="kw_user_registration" %>
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
   
           <script type="text/javascript">
         function CloseEditStudentDialog() {
            $("#modal_large").hide();
            
         }

         function OnTreeClick(evt) {


             var src = window.event != window.undefined ? window.event.srcElement : evt.target;
             var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
             if (isChkBoxClick) {
                 var parentTable = GetParentByTagName("table", src);
                 var nxtSibling = parentTable.nextSibling;
                 //check if nxt sibling is not null & is an element node
                 if (nxtSibling && nxtSibling.nodeType == 1) {
                     if (nxtSibling.tagName.toLowerCase() == "div") //if node has children
                     {
                         //check or uncheck children at all levels
                         CheckUncheckChildren(parentTable.nextSibling, src.checked);
                     }

                 }
                 //check or uncheck parents at all levels
                 CheckUncheckParents(src, src.checked);

             }

         }
         function CheckUncheckChildren(childContainer, check) {
             var childChkBoxes = childContainer.getElementsByTagName("input");
             var childChkBoxCount = childChkBoxes.length;
             for (var i = 0; i < childChkBoxCount; i++) {
                 childChkBoxes[i].checked = check;
             }

         }
         function CheckUncheckParents(srcChild, check) {
             var parentDiv = GetParentByTagName("div", srcChild);
             var parentNodeTable = parentDiv.previousSibling;
             if (parentNodeTable) {
                 var checkUncheckSwitch;
                 if (check) //checkbox checked
                 {
                     var isAllSiblingsChecked = AreAllSiblingsChecked(srcChild);
                     if (isAllSiblingsChecked)
                         checkUncheckSwitch = true;
                     else
                         return; //do not need to check parent if any(one or more) child not checked
                 }
                 else //checkbox unchecked
                 {
                     checkUncheckSwitch = false;
                 }
                 var inpElemsInParentTable = parentNodeTable.getElementsByTagName("input");
                 if (inpElemsInParentTable.length > 0) {
                     var parentNodeChkBox = inpElemsInParentTable[0];
                     parentNodeChkBox.checked = checkUncheckSwitch;
                     //do the same recursively
                     CheckUncheckParents(parentNodeChkBox, checkUncheckSwitch);
                 }
             }
         } function AreAllSiblingsChecked(chkBox) {
             var parentDiv = GetParentByTagName("div", chkBox);
             var childCount = parentDiv.childNodes.length;

             for (var i = 0; i < childCount; i++) {
                 if (parentDiv.childNodes[i].nodeType == 1) {
                     //check if the child node is an element node
                     if (parentDiv.childNodes[i].tagName.toLowerCase() == "table") {
                         var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];

                         //if any of sibling nodes are not checked, return false
                         if (!prevChkBox.checked) {
                             return false;
                         }
                     }
                 }
             }

             return true;
         }

         function GetParentByTagName(parentTagName, childElementObj) {
             var parent = childElementObj.parentNode;

             while (parent.tagName.toLowerCase() != parentTagName.toLowerCase()) {
                 parent = parent.parentNode;
             }
             return parent;
         }
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server" ScriptMode="Release">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" style="height: 895px;">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>Pengguna</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Pentadbiran</a></li>
                            <li class="active">Pengguna</li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
     <asp:UpdatePanel ID="UpdatePanel3" runat="server"  UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title">Maklumat pengguna</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                         <div class="box-body">&nbsp;</div>
                        <div class="form-horizontal">
                             
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">Id pengguna <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                        <span class="input-group-addon">@</span>
                                            <asp:TextBox type="text" class="form-control text-uppercase" id="uname" runat="server" maxlength="100" ></asp:TextBox>
                                             <span class="input-group-addon"><i class="fa fa-pencil"></i></span>
                                            </div>
                                    </div>
                                </div>
                                  <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"> Nama pengguna <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                          <div class="input-group">
                                            <asp:TextBox type="text" class="form-control text-uppercase" id="pname" runat="server" maxlength="100" ></asp:TextBox>
                                              <span class="input-group-addon"><i class="fa fa-pencil"></i></span>
                                              </div>
                                    </div>
                                </div>
                                  <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">Email  </label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                            <i class="fa fa-envelope"></i>
                                            </span>
                                            <asp:TextBox type="text" class="form-control validate[optional] text-uppercase" id="email" runat="server" maxlength="100" ></asp:TextBox>
                                            <span class="input-group-addon"><i class="fa fa-pencil"></i></span>
                                            </div>
                                    </div>
                                </div>
                                 <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">Kata Laluan <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                            <asp:TextBox type="password" class="form-control" id="password" runat="server" maxlength="100" ></asp:TextBox>
                                             <span class="input-group-addon"><i class="fa fa-pencil"></i></span>
                                             </div>
                                    </div>
                                </div>
                                   <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">Sahkan Kata Laluan <span class="style1">*</span></label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                            <asp:TextBox type="password" class="form-control" id="cpassword" runat="server" AutoPostBack="true" OnTextChanged="chg_cpassword" maxlength="100" ></asp:TextBox>
                                             <span class="input-group-addon"><i class="fa fa-pencil"></i></span>
                                             </div>
                                    </div>
                                </div>
                                 <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">Status </label>
                                    <div class="col-sm-8">
                                         <div class="input-group">
                                        <asp:dropdownlist id="sts" style="width:100%; font-size:13px;" runat="server" class="form-control select2 validate[optional]">
                                                <asp:ListItem Value ="A">Aktif</asp:ListItem>
                                                <asp:ListItem Value ="T">Tidak Aktif</asp:ListItem>
                                            </asp:dropdownlist>
                                             <span class="input-group-addon"><i class="fa fa-search"></i></span>
                                             </div>
                                        <asp:TextBox runat="server" type="text" style="display:none;" id="txtName" CssClass="form-control text-uppercase" maxlength="5"  placeholder="Enter Here..."> </asp:TextBox>
                                            <asp:Label type="text" style="display:none;" id="txtrole" runat="server" class="form-control text-uppercase" maxlength="5"  placeholder="Enter Here..."></asp:Label>
                                          <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="edit_id" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label">Gambar Pengguna </label>
                                    <div class="col-sm-8">
                                            <asp:FileUpload ID="FileUpload1" CssClass="fileupload" type="file" runat="server" onchange="ShowImagePreview(this);"/>
                                        <br />
                                        <asp:Image ID="ImgPrv" runat="server" class="profile-user-img img-responsive img-circle" Width="150px" Height="150px" />
                                        <asp:TextBox ID="TextBox3" runat="server" class="form-control validate[optional] uppercase" Visible="false"></asp:TextBox>
                                        <br />
                                        <span style="color: Red;">(NOTA : Saiz Imej Maksimum Adalah - 100 KB)</span>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                            <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label"></label>
                                    <div class="col-sm-8">
                                         <button id="Button4" runat="server" type="button" class="btn btn-warning" data-toggle="modal" data-target="#modal-default1"><span class="glyphicon glyphicon-user"></span>&nbsp; Kemaskini Peranan</button> 
                                <%--<button id="rol_set" runat="server" type="button" class="btn btn-success" data-toggle="modal" data-target="#modal-default"><span class="fa fa-unlock-alt"></span>&nbsp; Kemaskini Akses</button>--%>
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
                             
                                <%--<asp:Button ID="rol_set" runat="server"  Text="Edit Permissions" class="btn btn-default" data-toggle="modal" data-target="#modal-default"/>--%>
                                <asp:Button ID="submit_btn" runat="server" OnClick="clk_submit" Text="Simpan" class="btn btn-primary" type="submit" />
                                <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Set Semula" UseSubmitBehavior="false" OnClick="Button5_Click" />
                                <asp:Button ID="Button2" runat="server" class="btn btn-default" Text="Kembali" UseSubmitBehavior="false" OnClick="Click_bck" />
                                
                            </div>
                           </div>
                               </div>
                           
                            <div class="box-body">&nbsp;
                                    </div>
                             <div id="shw_cnt1" runat="server" visible="false">
                                <div class="box-body">&nbsp;</div>
                              <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-6 box-body">
                                      &nbsp;
                                      </div>
                            <div class="col-md-6 box-body  text-right">
                                <div class="form-group">
                                    <div class="col-sm-4  text-right">
                                        &nbsp;
                                        </div>
                                    <div class="col-sm-8  text-right">
                                        <div class="col-sm-10  text-right">
                                           <div class="input-group">
                                                <span class="input-group-addon" style="background-color:#0090d9; color:#fff;" ><i class="fa fa-search"></i></span>
                                        <asp:TextBox ID="txtSearch" class="form-control" runat="server" OnTextChanged="txtSearch_TextChanged" AutoPostBack="True" placeholder="MASUKKAN NILAI DI SINI"></asp:TextBox>
                                             </div>
                                            </div>
                                          <div class="col-sm-2  text-right">
                                         <asp:Button ID="button3" runat="server" Text="Carian"  class="align-center btn btn-primary" UseSubmitBehavior="false" OnClick="btn_search_Click"></asp:Button>
                                              </div>
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                             <div class="dataTables_wrapper form-inline dt-bootstrap" style=" overflow-y:auto; height:420px;">
                                     <%-- <div class="row" >--%>
           <div class="col-md-12 box-body">
                                 <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover dataTable uppercase" AutoGenerateColumns="false" HeaderStyle-BackColor="#BDC4C7" HeaderStyle-ForeColor="black" CellPadding="3" CellSpacing="2" Font-Size="12px" Width="100%" AllowPaging="true" PageSize="500" ShowFooter="false" GridLines="None"  OnPageIndexChanging="gvSelected_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound">
                                     <PagerStyle CssClass="pager" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="BIL" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <%# Container.DataItemIndex + 1 %></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Main Skrin" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                                                    <ItemTemplate>                                                                        
                                                                            <asp:Label ID="gd_1" runat="server" Text='<%# Eval("KK_Skrin_name") %>'></asp:Label>                                                                            
                                                                        <asp:Label ID="vv1" runat="server" Visible="false" Text='<%# Eval("KK_Skrin_id") %>'></asp:Label>
                                                                        <asp:Label ID="vv2" runat="server" Visible="false" Text='<%# Eval("KK_Sskrin_id") %>'></asp:Label>
                                                                        <asp:Label ID="vv3" runat="server" Visible="false" Text='<%# Eval("KK_Spreskrin_id") %>'></asp:Label>
                                                                        <asp:Label ID="vv4" runat="server" Visible="false" Text='<%# Eval("KK_Spreskrin1_id") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Sub Skrin" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="gd_2" runat="server" Text='<%# Eval("KK_Sskrin_name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="KK_Spreskrin_name" HeaderText="Pre Sub Skrin" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%" />
                                                                <asp:BoundField DataField="KK_Spreskrin1_name" HeaderText="Pre Super Sub Skrin" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%" />
                                                                  <asp:TemplateField HeaderText="Permission" ItemStyle-HorizontalAlign="center" ItemStyle-Width="5%">
                                                                    <ItemTemplate>                                                                        
                                                                           <asp:CheckBox ID="chk1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" AutoPostBack="true" />                                                                          
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="center" ItemStyle-Width="5%">
                                                                    <ItemTemplate>                                                                        
                                                                           <asp:CheckBox ID="chk2" runat="server" />                                                                          
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="Add" ItemStyle-HorizontalAlign="center" ItemStyle-Width="5%">
                                                                    <ItemTemplate>                                                                        
                                                                           <asp:CheckBox ID="chk3" runat="server" />                                                                          
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="center" ItemStyle-Width="5%">
                                                                    <ItemTemplate>                                                                        
                                                                           <asp:CheckBox ID="chk4"  runat="server" />                                                                          
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
                                 </div>
                           <%--   <cc1:ModalPopupExtender BackgroundCssClass="modalBg" DropShadow="true" ID="ModalPopupExtender1"
                                                                PopupControlID="Panel3" runat="server" TargetControlID="btnBack" PopupDragHandleControlID="Panel2"
                                                                CancelControlID="btnBack">
                                                            </cc1:ModalPopupExtender>
                                <asp:Panel ID="Panel3" runat="server" Width="100%" CssClass="modalPanel" Style="display: none;">
                                    <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"></label>
                                    <div class="col-sm-8">
                                        <asp:TreeView ID="tvTables" runat="server" CssClass="sss1" style="line-height:25px; width:500px;" ForeColor="Black" NodeWrap="true"  TreeNodeSelectAction="None"    ShowLines="true"  ShowExpandCollapse="true" onclick="OnTreeClick(event)" ShowCheckBoxes="All">  
                                                 <ParentNodeStyle Font-Bold="False"/>
               <HoverNodeStyle Font-Underline="False" ForeColor="Purple" />
               <SelectedNodeStyle Font-Underline="False" HorizontalPadding="0px" VerticalPadding="0px" ForeColor="#663300" />
               <RootNodeStyle />
               <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="DarkBlue" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px"/>
               <LeafNodeStyle ForeColor="#CC0099"/></asp:TreeView>
                               
                                        
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                    <div class="row">
                             <div class="col-md-12">
                            <div class="col-md-6 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label"></label>
                                    <div class="col-sm-9">
                                       <asp:Button ID="Button3" runat="server" class="btn btn-danger sub_btn" Text="OK" OnClick="btnGetNode_Click"  UseSubmitBehavior="false" />
                                                                            &nbsp;
                                                                            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-default" Text="Keluar" />
                                    </div>
                                </div>
                            </div>
                                 </div>
                                </div>
                                    
                   </asp:Panel>--%>
                    
                           <%--  <div class="modal fade" id="modal-default">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Kemaskini Akses Peranan</h4>
              </div>
              <div class="modal-body" style="overflow-y: auto; height:350px;">
                 <p><strong>Akses</strong></p>
                 <asp:TreeView ID="tvTables" runat="server" CssClass="sss1" style="line-height:25px; padding-left:25px;" ForeColor="Black" NodeWrap="true"  TreeNodeSelectAction="None"    ShowLines="true"  ShowExpandCollapse="true" onclick="OnTreeClick(event)" ShowCheckBoxes="All">  
                                                 <ParentNodeStyle Font-Bold="False"/>
               <HoverNodeStyle Font-Underline="False" ForeColor="black" />
               <SelectedNodeStyle Font-Underline="False" HorizontalPadding="0px" VerticalPadding="0px" ForeColor="black" />
               <RootNodeStyle />
               <NodeStyle Font-Size="12px" ForeColor="black" HorizontalPadding="5px" NodeSpacing="1px" VerticalPadding="0px"/>
               <LeafNodeStyle ForeColor="black"/></asp:TreeView>
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Batal</button>
               <asp:Button ID="Button3" runat="server" class="btn btn-danger sub_btn" Text="Save Changes" data-dismiss="modal" OnClick="btnGetNode_Click"  UseSubmitBehavior="false" />
              </div>
            </div>
            <!-- /.modal-content -->
          </div>
          <!-- /.modal-dialog -->
        </div>--%>

                             <div class="modal fade" id="modal-default1">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Kemaskini Peranan</h4>
              </div>
              <div class="modal-body" style="overflow-y: auto; height:350px;">
                 <p><strong>Akses</strong></p>
                <div style="padding-left:40px; padding-top:10px;"><asp:CheckBoxList ID="skrin_list" CssClass="mycheckbox text-uppercase" runat="server"></asp:CheckBoxList></div>
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Batal</button>
               <asp:Button ID="Button5" runat="server" class="btn btn-danger sub_btn" Text="Save Changes" data-dismiss="modal" OnClick="btnGetNode_Click1"  UseSubmitBehavior="false" />
              </div>
            </div>
            <!-- /.modal-content -->
          </div>
          <!-- /.modal-dialog -->
        </div>
                    </div>
                </div>
            </div>
            <!-- /.row -->
          
         </ContentTemplate>
          <Triggers>
               <asp:PostBackTrigger ControlID="submit_btn"  />
               
           </Triggers>
    </asp:UpdatePanel>
        <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->
   
</asp:Content>

