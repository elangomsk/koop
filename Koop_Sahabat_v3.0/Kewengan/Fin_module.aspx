<%@ Page Title="" Language="C#" MasterPageFile="~/KSAIMB.master" AutoEventWireup="true" CodeFile="../Kewengan/Fin_module.aspx.cs" Inherits="Fin_module" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <asp:ScriptManager ID="ScriptManagerCalendar" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
<style>
 body{
     background-color: #fff;
 }
.box-new{
  background-image: url("../dist/img/new/bg.png");
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;
  position: absolute;
  height: 22vh;
  width: 57vh;
  opacity: 0.5;
  bottom: 30vh;
  right: 60vh;
  -ms-transform: rotate(-20deg); /* IE 9 */
  -webkit-transform: rotate(-20deg); /* Safari 3-8 */
  transform: rotate(-20deg);
}
/*.bg-clr{
  margin-top: 15px;
}*/
.box-n{
border: 1px solid #b3013b;
-ms-border-radius: 8px;
-moz-border-radius: 8px;
-o-border-radius: 8px;
-webkit-border-radius: 8px;
border-radius: 8px;
}


</style>
<%--<div class="box-new"></div>--%>
<div class="content-wrapper" >
    <section class="content">
                    <div class="bg-clr box-n">
                    <%--<div class="box-new"></div>--%>
                    <div class="circular-menu">
                      <div class="circle1">
                             <asp:Repeater ID="bnd_mmenus" runat="server">
                    <ItemTemplate>
                        <a <%# Eval("mod_val6") %>><img class="port-image" src="<%# Eval("mod_val2") %>"/><strong><%# Eval("mod_val1") %></strong></a>
                        </ItemTemplate>
                                 </asp:Repeater>
                      </div>
                      <a href="#" class="menu-button1"><img class="port-image" src="../dist/img/new/bg1.png"/></a>
                    </div>
                </div>
    </section>
</div>
    <script>
        var items = document.querySelectorAll('.circle1 a');

        for (var i = 0, l = items.length; i < l; i++) {
            items[i].style.left = (50 - 35 * Math.cos(-0.5 * Math.PI - 2 * (1 / l) * i * Math.PI)).toFixed(4) + "%";

            items[i].style.top = (50 + 35 * Math.sin(-0.5 * Math.PI - 2 * (1 / l) * i * Math.PI)).toFixed(4) + "%";
        }

        //document.querySelector('.menu-button').onclick = function (e) {
        //    e.preventDefault(); document.querySelector('.circle').classList.toggle('open');
        //}
</script>
</asp:Content>

