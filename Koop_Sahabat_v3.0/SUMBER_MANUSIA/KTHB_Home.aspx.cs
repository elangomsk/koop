using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Globalization;
using System.Configuration;
using System.Data;  

public partial class KAIMB_Home : System.Web.UI.Page
{
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    DataTable get_ahli_sa = new DataTable();
    DataTable get_jum_syer = new DataTable();
    DataTable get_sts_pa = new DataTable();
    DataTable get_sts_ts = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                view_details();
            }
            else
            {
                Response.Redirect("~/KTHB_Logout.aspx", true);
            }
        }
        
    }
    
    void view_details()
    {
        
        get_ahli_sa = DBCon.Ora_Execute_table("select count(*) cnt_sa from mem_member where mem_sts_cd='SA'");
        get_sts_pa = DBCon.Ora_Execute_table("select count(*) cnt_pa from mem_member where mem_sts_cd=''");
        get_sts_ts = DBCon.Ora_Execute_table("select count(*) cnt_ts from mem_member where mem_sts_cd='TS'");
        hdr_txt_sa.InnerText = get_ahli_sa.Rows[0]["cnt_sa"].ToString();
        get_jum_syer = DBCon.Ora_Execute_table("select sum(ISNULL(sha_debit_amt,'0.00')) deb,sum(ISNULL(sha_credit_amt,'0.00')) kre,(sum(ISNULL(sha_debit_amt,'0.00')) - sum(ISNULL(sha_credit_amt,'0.00'))) as jum_syer from mem_share where sha_approve_sts_cd='SA'");
        hdr_txt_jum.Text = double.Parse(get_jum_syer.Rows[0]["jum_syer"].ToString()).ToString("C").Replace("$","").Replace("RM","");
        hdr_txt_pa.InnerText = get_sts_pa.Rows[0]["cnt_pa"].ToString();
        hdr_txt_ts.InnerText = get_sts_ts.Rows[0]["cnt_ts"].ToString();
    }

  
}