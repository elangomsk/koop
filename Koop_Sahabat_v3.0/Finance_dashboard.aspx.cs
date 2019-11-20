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
using System.Threading;
using System;
using System.Web;

public partial class Finance_dashboard : System.Web.UI.Page
{
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        view_fin_dashboard();
    }
    void view_fin_dashboard()
    {
        DataTable ddicno1 = new DataTable();
        ddicno1 = DBCon.Ora_Execute_table("select * from (select count(*) as pro_cnt from kw_profile_syarikat where status='A') as a"
            + " outer apply (select count(*) carta_cnt from kw_ref_carta_akaun where status='A') as b "
            + " outer apply (select count(*) bjt_cnt from KW_Ref_kod_bajet where bjt_Status='A') as c  "
            + " outer apply (select count(*) prj_cnt from KW_Ref_Projek where Status='A') as d "
            + " outer apply (select count(*) pel_cnt from KW_Ref_Pelanggan where Status='A') as e "
            + " outer apply (select count(*) pem_cnt from KW_Ref_Pembekal where Status='A') as f ");
        if(ddicno1.Rows.Count != 0)
        {
            lbl1.Text = ddicno1.Rows[0]["pro_cnt"].ToString();
            lbl2.Text = ddicno1.Rows[0]["carta_cnt"].ToString();
            lbl3.Text = ddicno1.Rows[0]["bjt_cnt"].ToString();
            lbl4.Text = ddicno1.Rows[0]["prj_cnt"].ToString();
            lbl5.Text = ddicno1.Rows[0]["pel_cnt"].ToString();
            lbl6.Text = ddicno1.Rows[0]["pem_cnt"].ToString();
        }
    }
}