using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Text;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.Globalization;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Windows.Forms;

public partial class change_pwd : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    private static int PageSize = 20;
    string qry1 = string.Empty, qry2 = string.Empty;
    string sqry1 = string.Empty, sqry2 = string.Empty;
    string level;
    string Status = string.Empty;
    string userid;
    string CommandArgument1 = string.Empty, CommandArgument2 = string.Empty, CommandArgument3 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
              
            }
            else
            {
                Response.Redirect("KSAIMB_Login.aspx");
            }
        }
    }

    protected void clk_submit(object sender, EventArgs e)
    {
        if (old_pwd.Text != "" && new_pwd.Text != "" && cnf_pwd.Text != "")
        {
            DataTable dd1 = new DataTable();
            dd1 = DBCon.Ora_Execute_table("select * from kk_User_Login where KK_userid = '" + Session["userid"].ToString() + "'");
            if (dd1.Rows[0]["KK_password"].ToString() == old_pwd.Text)
            {
                if (new_pwd.Text == cnf_pwd.Text)
                {
                    string Inssql = "Update kk_User_Login SET KK_password='" + new_pwd.Text + "', Kk_upd_id='" + Session["userid"].ToString() + "',KK_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where KK_userid = '" + Session["userid"].ToString() + "'";
                    Status = DBCon.Ora_Execute_CommamdText(Inssql);
                    if (Status == "SUCCESS")
                    {
                            old_pwd.Text = "";
                            new_pwd.Text = "";
                            cnf_pwd.Text = "";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Password Changed.',{'type': 'confirmation','title': 'Success'});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Please Enter Correct Password',{'type': 'warning','title': 'Warning','auto_close': 2000}); ", true);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Password Mis Match',{'type': 'warning','title': 'Warning','auto_close': 2000}); ", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Please Enter Password',{'type': 'warning','title': 'Warning','auto_close': 2000}); ", true);
        }

    }
    protected void btn_reset(object sender, EventArgs e)
    {
        Response.Redirect("KSAIMB_Home.aspx");
    }


}