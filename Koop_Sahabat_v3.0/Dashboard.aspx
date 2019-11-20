<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>
    <style>
        .fixed .content-wrapper, .fixed .right-side{padding-top: 0px;}
        .bg-blueviolet{background-color: blueviolet; color: #fff;}
        .bg-green-2{background-color: green; color: #fff;}
        .bg-red-new h4, .bg-red-new h3{display: inline;vertical-align: top; font-size: 38px; font-weight: bold;}
        .bg-red-new h4{margin-left: 20px;}
        .bg-red-new p{margin-top: 10px;}
    </style>
        <style>
    .content-header{
        padding: 6px 0px 0 0px;
    }
    .content-header > .breadcrumb{
        padding: 5px 0px;
    }
    .content{
        padding: 7px 0px 0px 0px;
    }
</style>
       <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper container-padding">
                    <!-- Content Header (Page header) -->
                  <%--  <section class="content-header">
                        <h1>Dashboard</h1>
                        <ol class="breadcrumb">
                            <li><a href="../KSAIMB_Home.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                            <li class="active">Dashboard</li>
                        </ol>
                    </section>--%>

                    <!-- Main content -->
                   <section class="content">
                        
         <!-- Small boxes (Stat box) -->
      <div class="row" id="module1" runat="server">
        <div class="col-lg-4 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-aqua bg-red-new">              
            <div class="inner">
              <h3><asp:Label ID="dash_txt" runat="server"></asp:Label></h3><br />
                 <h3><asp:Label ID="dash_txt_amt" runat="server"></asp:Label><sup style="font-size: 20px"> RM</sup></h3>
              <p>Jumlah Ahli <strong>Sah</strong></p>
            </div>
            <div class="icon">
              <i class="ion ion-person-add"></i>
            </div>
            <a href="#" class="small-box-footer"></a>
          </div>
        </div>
        <!-- ./col -->
       <div class="col-lg-4 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-green bg-red-new">
            <div class="inner">
              <h3><asp:Label ID="dash_txt2" runat="server"></asp:Label></h3><br />
                <h3><asp:Label ID="dash_txt2_amt" runat="server"></asp:Label><sup style="font-size: 20px"> RM</sup></h3>
              <p>Jumlah <strong>Permohonan Baru</strong></p>
            </div>
            <div class="icon">
              <i class="ion ion-person-add"></i>
            </div>
            <a href="#" class="small-box-footer"></a>
          </div>
        </div>
        <!-- ./col -->
        <div class="col-lg-4 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-yellow bg-red-new">
            <div class="inner">
              <h3><asp:Label ID="dash_txt3" runat="server"></asp:Label></h3><br />
                <h3><asp:Label ID="dash_txt3_amt" runat="server"></asp:Label><sup style="font-size: 20px"> RM</sup></h3>
              <p>Jumlah Ahli <strong>FI Masuk</strong></p>
            </div>
            <div class="icon">
              <i class="ion ion-person-add"></i>
            </div>
            <a href="#" class="small-box-footer"></a>
          </div>
        </div>
        <!-- ./col -->
        <div class="col-lg-4 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-red bg-red-new">
            <div class="inner">
              <h3><asp:Label ID="dash_txt4" runat="server"></asp:Label></h3> <br />
               <h3><asp:Label ID="dash_txt4_1" runat="server"></asp:Label><sup style="font-size: 20px"> RM</sup></h3>
              <p>Jumlah <strong>Tambah Syer</strong></p>
            </div>
            <div class="icon">
              
                <i class="ion ion-pie-graph"></i>
            </div>
            <a href="#" class="small-box-footer"></a>
          </div>
        </div>
          <div class="col-lg-4 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-blueviolet bg-red-new">
            <div class="inner">
              <h3><asp:Label ID="dash_txt5" runat="server"></asp:Label></h3> <br />
               <h3><asp:Label ID="dash_txt5_1" runat="server"></asp:Label><sup style="font-size: 20px"> RM</sup></h3>
              <p>Jumlah <strong>Penebusan Syer</strong></p>
            </div>
            <div class="icon">
              
                <i class="ion ion-pie-graph"></i>
            </div>
            <a href="#" class="small-box-footer"></a>
          </div>
        </div>
          <div class="col-lg-4 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-green-2 bg-red-new">
            <div class="inner">
              <h3><asp:Label ID="dash_txt6" runat="server"></asp:Label></h3> <br />
               <h3><asp:Label ID="dash_txt6_1" runat="server"></asp:Label><sup style="font-size: 20px"> RM</sup></h3>
              <p>Jumlah Ahli <strong>Tidak Sah</strong></p>
            </div>
            <div class="icon">
              
                <i class="ion ion-pie-graph"></i>
            </div>
            <a href="#" class="small-box-footer"></a>
          </div>
        </div>
        <!-- ./col -->
      </div>

                        <div class="row" id="module2" runat="server" visible="false">
        <div class="col-lg-4 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-aqua">
            <div class="inner">
              <h3><asp:Label ID="Label1" runat="server"></asp:Label></h3>

              <p><strong>Members</strong></p>
            </div>
            <div class="icon">
              <i class="ion ion-person-add"></i>
            </div>
            <a href="#" class="small-box-footer"></a>
          </div>
        </div>
        <!-- ./col -->
       <div class="col-lg-4 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-green">
            <div class="inner">
              <h3><asp:Label ID="Label2" runat="server"></asp:Label></h3>

              <p><strong>Staff</strong></p>
            </div>
            <div class="icon">
              <i class="ion ion-person-add"></i>
            </div>
            <a href="#" class="small-box-footer"></a>
          </div>
        </div>
        <!-- ./col -->
        <div class="col-lg-4 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-yellow">
            <div class="inner">
              <h3><asp:Label ID="Label3" runat="server"></asp:Label></h3>

              <p><strong>Peranan</strong></p>
            </div>
            <div class="icon">
              <i class="ion ion-person-add"></i>
            </div>
            <a href="#" class="small-box-footer"></a>
          </div>
        </div>
        <!-- ./col -->
      </div>

                           <div class="row" id="module3" runat="server" visible="false">
        <div class="col-lg-4 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-red bg-red-new">
            <div class="inner">
              <h3><asp:Label ID="Label4" runat="server"></asp:Label></h3><br />
                 <h3><asp:Label ID="Label4_1" runat="server"></asp:Label></h3>
              <p>Baki ST Semasa <strong>(keseluruhan)</strong></p>
            </div>
            <div class="icon">
              <i class="ion ion-person-add"></i>
            </div>
            <a href="#" class="small-box-footer"></a>
          </div>
        </div>
        <!-- ./col -->
       <div class="col-lg-4 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-green bg-red-new">
            <div class="inner">
              <h3><asp:Label ID="Label5" runat="server"></asp:Label></h3><br />
                 <h3><asp:Label ID="Label5_1" runat="server"></asp:Label></h3>
              <p>Baki ST Semasa <strong>(Anggota)</strong></p>
            </div>
            <div class="icon">
              <i class="ion ion-person-add"></i>
            </div>
            <a href="#" class="small-box-footer"></a>
          </div>
        </div>
        <!-- ./col -->
        <div class="col-lg-4 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-orange bg-red-new">
            <div class="inner">
              <h3><asp:Label ID="Label6" runat="server"></asp:Label></h3><br />
                 <h3><asp:Label ID="Label6_1" runat="server"></asp:Label></h3>
              <p>Baki ST Semasa <strong>(Bukan Anggota)</strong></p>
            </div>
            <div class="icon">
              <i class="ion ion-person-add"></i>
            </div>
            <a href="#" class="small-box-footer"></a>
          </div>
        </div>
        <!-- ./col -->
      </div>
      <!-- /.row -->
    </section>
                    <!-- /.content -->
                </div>
                <!-- /.content-wrapper -->

</asp:Content>

