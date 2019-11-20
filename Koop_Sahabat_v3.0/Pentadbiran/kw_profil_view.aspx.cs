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
using System.Data.OleDb;
using System.IO;
using System.Net;
public partial class kw_profil_view : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();

    string level;
    string Status = string.Empty;
    string userid;
    string ref_id;
    string confirmValue, am;
    string qry1 = string.Empty, qry2 = string.Empty;
    string tn1 = string.Empty, tc1 = string.Empty, tc2 = string.Empty, tc3 = string.Empty, tc4 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
                view_details();
            }
            else
            {
                service.audit_trail("", "Logout");
                Response.Redirect("~/KSAIMB_Login.aspx");
            }
        }
    }


    void view_details()
    {
        try
        {
           
             DataTable dd1 = new DataTable();
            dd1 = DBCon.Ora_Execute_table("select *,FORMAT(KK_tarikh_daftar,'dd/MM/yyyy', 'en-us') as daft_dt from kk_User_Login where KK_userid='" + userid + "'");
            if (dd1.Rows.Count != 0)
            {

                uname.Attributes.Add("Readonly", "Readonly");
                uname.Text = dd1.Rows[0]["KK_userid"].ToString();
                pname.Text = dd1.Rows[0]["KK_username"].ToString();
                email.Text = dd1.Rows[0]["KK_email"].ToString();
                if (dd1.Rows[0]["Status"].ToString() == "A")
                {
                    sts.Text = "AKTIF";
                }
                else
                {
                    sts.Text = "TIDAK AKTIF";
                }

                string checkimage = dd1.Rows[0]["user_img"].ToString();

                string fileName = Path.GetFileName(checkimage);
                if (fileName != "")
                {
                    ImgPrv.Attributes.Add("src", "../Files/Profile_syarikat/" + fileName);
                }
                else
                {
                    ImgPrv.Attributes.Add("src", "../Files/Profile_syarikat/user.gif");
                }


            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Kakitangan Tidak Berdaftar',{'type': 'warning','title': 'Warning','auto_close': 2000}); ", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    
    //protected void Click_bck(object sender, EventArgs e)
    //{
    //    Response.Redirect("../KSAIMB_Home.aspx");
    //}

    
}