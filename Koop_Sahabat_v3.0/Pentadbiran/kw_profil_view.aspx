<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Pentadbiran/kw_profil_view.aspx.cs" Inherits="kw_profil_view" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server" ScriptMode="Release">
    </asp:ScriptManager>

    
     <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper" style="height: 895px;">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>Profile</h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Pentadbiran</a></li>
                            <li class="active">Profile</li>
                        </ol>
                    </section>

                    <!-- Main content -->
                   <section class="content">
     <asp:UpdatePanel ID="UpdatePanel3" runat="server"  UpdateMode="Conditional">
        <ContentTemplate>
              <div class="row">
        <div class="col-md-12">

          <!-- Profile Image -->
          <div class="box box-primary">
            <div class="box-body box-profile" style="padding:15px;">
              
                <asp:Image ID="ImgPrv" runat="server" class="profile-user-img img-responsive img-circle" alt="User profile picture" Width="150px" Height="150px" />

              <h3 class="profile-username text-center"><asp:label type="text" class="text-uppercase" id="pname" runat="server" maxlength="100" ></asp:label></h3>

              <p class="text-muted text-center">Software Engineer</p>
                <div class="col-md-3"></div>
                <div class="col-md-6">
              <ul class="list-group list-group-unbordered">
                <li class="list-group-item">
                  <b>User Name</b> <a class="pull-right"><asp:label type="text" class="text-uppercase" id="uname" runat="server" maxlength="100" ></asp:label></a>
                </li>
                <li class="list-group-item">
                  <b>Email</b> <a class="pull-right"><asp:label type="text" class="text-uppercase" id="email" runat="server" maxlength="100" ></asp:label></a>
                </li>
                <li class="list-group-item">
                  <b>Status</b> <a class="pull-right"><asp:label type="text" class="text-uppercase" id="sts" runat="server" maxlength="100" ></asp:label></a>
                </li>
              </ul>
                    </div>
                  <div class="col-md-3"></div>
            <%--  <a href="#" class="btn btn-primary btn-block"><b>Follow</b></a>--%>
            </div>
            <!-- /.box-body -->
          </div>
          <!-- /.box -->
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

