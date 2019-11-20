<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ksaimb_forgot_pwd.aspx.cs" Inherits="ksaimb_forgot_pwd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" style="height:auto;">
<head>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Koperasi Sahabat | Login</title>

    <!-- CSS -->
    <link rel="stylesheet" href="http://fonts.googleapis.com/css?family=Roboto:400,100,300,500" />
    <link rel="stylesheet" href="../../Login_assets/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../Login_assets/font-awesome/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../Login_assets/css/form-elements.css" />
    <link rel="stylesheet" href="../../Login_assets/css/login.css" />




    <!-- Favicon and touch icons -->
    <link rel="shortcut icon" href="Login_assets/ico/favicon.png" />
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="Login_assets/ico/apple-touch-icon-144-precomposed.png" />
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="Login_assets/ico/apple-touch-icon-114-precomposed.png" />
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="Login_assets/ico/apple-touch-icon-72-precomposed.png" />
    <link rel="apple-touch-icon-precomposed" href="Login_assets/ico/apple-touch-icon-57-precomposed.png" />
     <link rel="stylesheet" href="kAIMb_components/zipra_dialogue/css/default/zebra_dialog.min.css" type="text/css" />

   <%-- <style type="text/css">
        .carousel-fade .carousel-inner .item {
            opacity: 0;
            transition-property: opacity;
        }

        .carousel-fade .carousel-inner .active {
            opacity: 1;
        }

            .carousel-fade .carousel-inner .active.left,
            .carousel-fade .carousel-inner .active.right {
                left: 0;
                opacity: 0;
                z-index: 1;
            }

        .carousel-fade .carousel-inner .next.left,
        .carousel-fade .carousel-inner .prev.right {
            opacity: 1;
        }

        .carousel-fade .carousel-control {
            z-index: 2;
        }

        @media all and (transform-3d), (-webkit-transform-3d) {
            .carousel-fade .carousel-inner > .item.next,
            .carousel-fade .carousel-inner > .item.active.right {
                opacity: 0;
                -webkit-transform: translate3d(0, 0, 0);
                transform: translate3d(0, 0, 0);
                -webkit-transition: all 0.5s;
                transition: all 0.5s;
            }

            .carousel-fade .carousel-inner > .item.prev,
            .carousel-fade .carousel-inner > .item.active.left {
                opacity: 0;
                -webkit-transform: translate3d(0, 0, 0);
                transform: translate3d(0, 0, 0);
                -webkit-transition: all 0.5s;
                transition: all 0.5s;
            }

                .carousel-fade .carousel-inner > .item.next.left,
                .carousel-fade .carousel-inner > .item.prev.right,
                .carousel-fade .carousel-inner > .item.active {
                    opacity: 1;
                    -webkit-transform: translate3d(0, 0, 0);
                    transform: translate3d(0, 0, 0);
                    -webkit-transition: all 0.5s;
                    transition: all 0.5s;
                }
        }

        .item:nth-child(1) {
            background: url(Login_assets/img/backgrounds/21.jpg) no-repeat center center fixed;
            -webkit-background-size: cover;
            -moz-background-size: cover;
            -o-background-size: cover;
            background-size: cover;
        }

        .item:nth-child(2) {
            background: url(Login_assets/img/backgrounds/22.jpg) no-repeat center center fixed;
            -webkit-background-size: cover;
            -moz-background-size: cover;
            -o-background-size: cover;
            background-size: cover;
        }

        .item:nth-child(3) {
            background: url(Login_assets/img/backgrounds/23.jpg) no-repeat center center fixed;
            -webkit-background-size: cover;
            -moz-background-size: cover;
            -o-background-size: cover;
            background-size: cover;
        }

        .carousel {
            z-index: -99;
        }

            .carousel .item {
                position: fixed;
                width: 100%;
                height: 100%;
            }

        .title {
            text-align: center;
            margin-top: 20px;
            padding: 10px;
            text-shadow: 2px 2px #000;
            color: #FFF;
        }
    </style>
    <script type="text/javascript">

        $('.carousel').carousel();
    </script>--%>
    <script type="text/javascript">
                function showpass(check_box) {
                    var spass = document.getElementById("Txtpwd");
                    if (spass != "") {
                        if (check_box.checked)
                            spass.setAttribute("type", "text");
                        else
                            spass.setAttribute("type", "password");
                    }
                }
           </script>


</head>

<body>
    <form id="form1" runat="server">


        <div class="carousel slide carousel-fade" data-ride="carousel">

            <!-- Wrapper for slides -->
            <div class="carousel-inner" role="listbox">
                <div class="item active">
                </div>
                <div class="item">
                </div>
                <div class="item">
                </div>
            </div>
        </div>

        <div class="top-content">
                <div class="container login-animate">
                         <div class="text-center">                         
                             <asp:Label ID="log_logo" runat="server"></asp:Label>
                         </div>
                         <div class="form-top">
                                <div class="text-center form-top-new">
                                    <asp:Label ID="log_tit" runat="server"></asp:Label>
                                </div>
                            </div>
                        <div class="col-sm-6 col-sm-offset-3 form-box">
                        <div class="form-box-padd">
                            <div class="col-md-12 form-row">
                                <div class="form-group col-md-12 form-group-new">
                                    <label class="text-style-new" for="inputEmail3"><i class="fa fa-user"></i>&nbsp Email <span class="style1" style="color:red;">*</span></label>
                                    <asp:Textbox type="text" class="form-control validate[optional]" runat="server" ID="email" placeholder="Enter your Email..."></asp:Textbox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="You can't leave this Empty" CssClass="requiredFieldValidateStyle" ForeColor="Red" ControlToValidate="email" ValidationGroup="vgSubmit_fpwd">
                                    </asp:RequiredFieldValidator>
                                </div>
                              
                                <div class="form-group col-md-12">
                                  <div class="col-md-6"></div>
                                    <div class="col-md-6 text-right"><a href="../KSAIMB_Login.aspx" style="font-weight:bold; color:#fff;"> <i class="fa fa-angle-left"></i> Back to Login</a></div>
                                </div>
                                <div class="form-group text-center form-group-new-btn">
                                    <label for="inputEmail3"></label>                                    
                                    <asp:button type="submit" runat="server" class="btn btn-primary" Text="Reset Kata Laluan" ValidationGroup="vgSubmit_fpwd" OnClientClick=" if ( Page_ClientValidate() ) { this.value='Please wait..';this.style.display='none';getElementById('bWait1').style.display = '';  }" OnClick="Button1_Click"></asp:button>
                                    <input type="button" id="bWait1" class="btn btn-primary" value="Please wait ..." disabled="disabled" style="display:none" />
                                </div>
                            </div>
                              </div>
                        </div>
                </div>
       </div>

        <!-- Javascript -->
        <script src="Login_assets/js/jquery-1.11.1.min.js"></script>
        <script src="Login_assets/bootstrap/js/bootstrap.min.js"></script>
        <script src="Login_assets/js/jquery.backstretch.min.js"></script>
        <script src="Login_assets/js/scripts.js"></script>
        <script type="text/javascript" src="kAIMb_components/zipra_dialogue/zebra_dialog.min.js"></script>   
        <!--[if lt IE 10]>
            <script src="assets/js/placeholder.js"></script>
        <![endif]-->

    </form>

</body>
</html>
