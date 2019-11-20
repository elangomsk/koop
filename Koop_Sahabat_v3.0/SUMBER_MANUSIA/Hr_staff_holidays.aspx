<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/KSAIMB.master" CodeFile="../SUMBER_MANUSIA/Hr_staff_holidays.aspx.cs" Inherits="Hr_staff_holidays" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <asp:ScriptManager ID="ScriptManagerCalendar" runat="server">
    </asp:ScriptManager>

  
    <div class="content-wrapper">
    <!-- Content Header (Page header) -->
    <section class="content-header">
      <h1>
        Calendar Cuti
        <%--<small>Control panel</small>--%>
      </h1>
      <ol class="breadcrumb">
        <li><a href="#"><i class="fa fa-dashboard"></i> Sumber Manusia</a></li>
        <li class="active">Maklumat Cuti</li>
      </ol>
    </section>

    <!-- Main content -->
    <section class="content" >
      <div class="row">
        <div class="col-md-7">
          <div class="box box-primary">
            <div class="box-body no-padding">
              <!-- THE CALENDAR -->
              <div id="calendar1"></div>
            </div>
            <!-- /.box-body -->
          </div>
          <!-- /. box -->
        </div>
             <div class="col-md-5">
          <div class="box box-primary">
            <div class="box-body no-padding">
                   <div class="box-body">&nbsp;</div>
                  <div class="box-header with-border">
                            <h3 class="box-title">Petunjuk </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">&nbsp;</div>
              <!-- THE CALENDAR -->
                  <div class="form-horizontal">
            <div class="row">
                             <div class="col-md-12">
                           
                            <div class="col-md-8 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-9 control-label" id="lbl2_text" runat="server">  Cuti Umum </label>
                                    <div class="col-sm-3">
                                         <asp:TextBox ID="txt_tahun" runat="server" CssClass="form-control" BackColor="#ff6961" Width="20px"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 
                                  </div>
                                </div>
                  <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-8 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-9 control-label" id="Label2" runat="server"> Cuti Tahunan</label>
                                    <div class="col-sm-3">
                                         <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" BackColor="#2E62A7" Width="20px"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                  
                                  </div>
                                </div>
                    <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-8 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-9 control-label" id="Label1" runat="server"> Cuti Sakit</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" BackColor="#ffc000" Width="20px"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                  
                                  </div>
                                </div>
                    <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-8 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-9 control-label" id="Label3" runat="server"> Cuti Kecemasan</label>
                                    <div class="col-sm-3">
                                         <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" BackColor="#91AAE8" Width="20px"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                  
                                  </div>
                                </div>
                       <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-8 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-9 control-label" id="Label4" runat="server"> Cuti Bersalin</label>
                                    <div class="col-sm-3">
                                         <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" BackColor="#AFF7DB" Width="20px"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                  
                                  </div>
                                </div>
                       <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-8 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-9 control-label" id="Label5" runat="server"> Cuti Bawa Kadapan</label>
                                    <div class="col-sm-3">
                                         <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control" BackColor="#add8e6" Width="20px"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                  
                                  </div>
                                </div>
                      <div class="row">
                             <div class="col-md-12">
                                  <div class="col-md-8 box-body">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-9 control-label" id="Label6" runat="server"> Cuti Ganti</label>
                                    <div class="col-sm-3">
                                         <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control" BackColor="#800080" Width="20px"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                  
                                  </div>
                                </div>
                      </div>
            </div>
            <!-- /.box-body -->
          </div>
          <!-- /. box -->
        </div>
        <!-- /.col -->
      </div>
      <!-- /.row -->
    </section>
    <!-- /.content -->
  </div>
   
</asp:Content>








