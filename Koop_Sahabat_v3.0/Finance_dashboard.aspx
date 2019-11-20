<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="Finance_dashboard.aspx.cs" Inherits="Finance_dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:ScriptManager ID="ScriptManagerCalendar" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
     <script>
window.onload = function () {

var chart = new CanvasJS.Chart("chartContainer", {
	animationEnabled: true,
	theme: "light2",
	title:{
		text: ""
	},
	axisY:{
		includeZero: false
	},
	data: [{        
		type: "line",       
		dataPoints: [
			{ y: 450 },
			{ y: 414},
			{ y: 520, indexLabel: "highest",markerColor: "red", markerType: "triangle" },
			{ y: 460 },
			{ y: 450 },
			{ y: 500 },
			{ y: 480 },
			{ y: 480 },
			{ y: 410 , indexLabel: "lowest",markerColor: "DarkSlateGrey", markerType: "cross" },
			{ y: 500 },
			{ y: 480 },
			{ y: 510 }
		]
	},
    {
        type: "line",
        dataPoints: [
			{ y: 550 },
			{ y: 514 },
			{ y: 520, indexLabel: "highest", markerColor: "red", markerType: "triangle" },
			{ y: 560 },
			{ y: 550 },
			{ y: 500 },
			{ y: 580 },
			{ y: 580 },
			{ y: 410, indexLabel: "lowest", markerColor: "DarkSlateGrey", markerType: "cross" },
			{ y: 500 },
			{ y: 580 },
			{ y: 510 }
        ]
    }, {
        type: "line",
        dataPoints: [
			{ y: 350 },
			{ y: 414 },
			{ y: 320, indexLabel: "highest", markerColor: "red", markerType: "triangle" },
			{ y: 260 },
			{ y: 550 },
			{ y: 400 },
			{ y: 380 },
			{ y: 380 },
			{ y: 310, indexLabel: "lowest", markerColor: "DarkSlateGrey", markerType: "cross" },
			{ y: 300 },
			{ y: 380 },
			{ y: 410 }
        ]
    }]
});
chart.render();

}
</script>
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
    <div class="content-wrapper">
                    <!-- Content Header (Page header) -->
     <%--   <section class="content-header">
          <h1>Dashboard</h1>
             <ol class="breadcrumb">
                  <li><a href="../KSAIMB_Home.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                  <li class="active">Dashboard</li>
            </ol>
        </section>--%>

        <section class="content">
         <!-- Small boxes (Stat box) -->
      <div class="container-scroll-sty well-1">
          <div class="row">
              <div class="col-md-12 col-sm-12">
                  <div class="col-md-2">
                       <div class="card card-1">
                          <div class="card-body card-new">
                            <h4 class="card-text text-primary"><asp:Label ID="lbl1" runat="server"></asp:Label></h4>
                            <p>Profile Akaun Syrikat</p>
                          </div>
                        </div>
                  </div>
                   <div class="col-md-2">
                       <div class="card card-2">
                          <div class="card-body card-new text-default">
                            <h4 class="card-text text-success"><asp:Label ID="lbl2" runat="server"></asp:Label></h4>
                            <p>Carta Akaun</p>
                          </div>
                        </div>
                  </div>
                  <div class="col-md-2">
                       <div class="card card-4">
                          <div class="card-body card-new text-default">
                            <h4 class="card-text"><asp:Label ID="lbl3" runat="server"></asp:Label></h4>
                            <p>Bajet Akaun</p>
                          </div>
                        </div>
                  </div>
                   <div class="col-md-2">
                       <div class="card card-3">
                          <div class="card-body card-new text-default">
                           <h4 class="card-text text-info"><asp:Label ID="lbl4" runat="server"></asp:Label></h4>
                            <p>Projek</p>
                          </div>
                        </div>
                  </div>
                  <div class="col-md-2">
                       <div class="card card-5">
                          <div class="card-body card-new text-default">
                            <h4 class="card-text text-danger"><asp:Label ID="lbl5" runat="server"></asp:Label></h4>
                            <p>Pelenggan</p>
                          </div>
                        </div>
                  </div>
                  <div class="col-md-2">
                       <div class="card card-6">
                          <div class="card-body card-new text-default">
                            <h4 class="card-text text-muted"><asp:Label ID="lbl6" runat="server"></asp:Label></h4>
                            <p>Pembekal</p>
                          </div>
                        </div>
                  </div>
        </div>
                     <div class="col-md-12">
                      <div class="col-md-8">
                          <div class="">
                            <div class="col-md-12 card weel-2 text-center">
                                <div id="chartContainer" style="height: 368px; width: 100%;"></div>
                            </div>
                          </div>
                      </div>
                      <div class="col-md-4">
                            <div class="col-md-12 card weel-2 text-center">
                               <div class="content-header-fin"><h1>Dashboard</h1></div>
                                <div class="col-md-6 col-xs-6">
                                    <div class="c100 p25 green">
                                    <span>25%</span>
                                    <div class="slice">
                                        <div class="bar"></div>
                                        <div class="fill"></div>
                                    </div>
                                </div>
                                <div class="cir-text">
                                  <p>Title here</p>
                                </div>
                            </div>
                                <div class="col-md-6 col-xs-6">
                                    <div class="c100 p45">
                                    <span>45%</span>
                                    <div class="slice">
                                        <div class="bar"></div>
                                        <div class="fill"></div>
                                    </div>
                                </div>
                                    <div class="cir-text">
                                  <p>Title here</p>
                                </div>
                            </div>
                                <div class="col-md-6 col-xs-6">
                                    <div class="c100 p85 orange">
                                    <span>85%</span>
                                    <div class="slice">
                                        <div class="bar"></div>
                                        <div class="fill"></div>
                                    </div>
                                </div>
                                    <div class="cir-text">
                                  <p>Title here</p>
                                </div>
                            </div>
                                <div class="col-md-6 col-xs-6">
                                    <div class="c100 p70 green">
                                    <span>70%</span>
                                    <div class="slice">
                                        <div class="bar"></div>
                                        <div class="fill"></div>
                                    </div>
                                </div>
                                    <div class="cir-text">
                                  <p>Title here</p>
                                </div>
                            </div>
                            </div>
       
                      </div>
                    </div>
          </div>
      </div>
      </section>
     </div>
    <script src="https://canvasjs.com/assets/script/canvasjs.min.js"></script>
</asp:Content>

