using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class KSAIMB_Logout : System.Web.UI.Page
{
    StudentWebService service = new StudentWebService();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["New"] != null)
        {
            service.audit_trail("02","","", "Logout");
        }
        Session.Clear();
        Session.RemoveAll();
        Session.Abandon();
        Response.Redirect("~/KSAIMB_Login.aspx", true);
    }
}