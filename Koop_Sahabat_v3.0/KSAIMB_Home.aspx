<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMaster.master" AutoEventWireup="true" CodeFile="KSAIMB_Home.aspx.cs" Inherits="KSAIMB_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container container-center">
       <div class="alert fade in alert-success-sec-new" >
               <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
               <p><strong> Welcome to KOOP SAHABAT!</strong></p>
        </div>
        <div class="center-section">
            <section class="main">
				<div class="ch-grid">
                     <asp:Repeater ID="bnd_modules" runat="server">
                    <ItemTemplate>
                        <div <%# Eval("mod_val9") %>>
					<div class="col-md-3 new-size animate_new fadeInUp">
                      
                         <asp:Label ID="ss1" runat="server" Text='<%# Eval("mod_val1") %>' style="display:none"></asp:Label>
                            <asp:Label ID="ss2" runat="server" Text='<%# Eval("mod_val8") %>' style="display:none"></asp:Label>
                        
                        <asp:LinkButton runat="server"  id="gt_lnk" onclick="get_mnulink">
                           
						<div class="ch-item" <%# Eval("mod_val6") %>>				
							<div class="ch-info">
								<div class="ch-info-front ch-img-<%# Eval("mod_val7") %>"></div>
								<div class="ch-info-back">
									<h3><%# Eval("mod_val2") %></h3>
								</div>	
							</div>
						</div>
                            </asp:LinkButton>
					</div>
                            </div>
                        </ItemTemplate>
                         </asp:Repeater>
				</div>
				
			</section>
        </div>
    </div>
</asp:Content>

