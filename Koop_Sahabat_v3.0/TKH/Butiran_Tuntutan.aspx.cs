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

public partial class TKH_Butiran_Tuntutan : System.Web.UI.Page
{

    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    string userid, level, wil, batch;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
                level = Session["level"].ToString();
                batch = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                BindData();
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }


    protected void BindData()
    {
        string sqry = string.Empty;
        string fmdate = string.Empty, fmonth = string.Empty, fyear = string.Empty, stdate = string.Empty, tmdate = string.Empty, tmdate1 = string.Empty;

        con.Open();
        DataTable ddicno = new DataTable();
        SqlCommand cmd = new SqlCommand("select tkh_tt_area_cd,tkh_tt_no_shbt,tkh_tt_name,tkh_tt_ic,thk_tt_age,tkh_tt_produk,tkh_tt_pinjaman_amt,tkh_tt_tempoh,tkh_tt_caj_amt,tkh_tt_lindung_amt,tkh_tt_manfaat_amt,tkh_tt_caruman_amt,format(tkh_tt_httks,'#,##,###.00')tkh_tt_httks,format(tkh_tt_mula_dt,'dd/MM/yyyy') tkh_tt_mula_dt,format(tkh_tt_akhir_dt,'dd/MM/yyyy') tkh_tt_akhir_dt,tkh_wp4_stat,tkh_tt_status_wil,tkh_tt_ulasan_wil,tkh_tt_kemaskini_wil,format(tkh_tt_kemaskini_wil_dt,'dd/MM/yyyy')tkh_tt_kemaskini_wil_dt,tkh_tt_status_hq,tkh_tt_ulasan_hq,format(tkh_tt_kemaskini_hq_dt,'dd/MM/yyyy') tkh_tt_kemaskini_hq_dt,tkh_chk_vr from tkh_terima_item where tkh_tt_batch_no='" + batch + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gv_refdata.DataSource = ds;
            gv_refdata.DataBind();
            int columncount = gv_refdata.Rows[0].Cells.Count;
            gv_refdata.Rows[0].Cells.Clear();
            gv_refdata.Rows[0].Cells.Add(new TableCell());
            gv_refdata.Rows[0].Cells[0].ColumnSpan = columncount;
            gv_refdata.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
        }
        else
        {
            gv_refdata.DataSource = ds;
            gv_refdata.DataBind();

        }

        con.Close();
    }

    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        System.Web.UI.WebControls.CheckBox tick_chk = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("CheckSin");
        System.Web.UI.WebControls.Label lnk2 = (System.Web.UI.WebControls.Label)e.Row.FindControl("lb_4");
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (level == "5")
            {
                tick_chk.Visible = true;
            }
            else
            {
                tick_chk.Visible = false;
            }
            DataTable get_value = new DataTable();
            get_value = DBCon.Ora_Execute_table("select tkh_chk_vr From tkh_terima_item where tkh_tt_WP4_no='" + batch + "'  and tkh_tt_ic='" + lnk2.Text + "'");
            LinkButton lnk_roe = (LinkButton)e.Row.FindControl("lnkView");
            if (get_value.Rows[0]["tkh_chk_vr"].ToString() == "Y")
            {
                tick_chk.Checked = true;
            }
            else
            {
                tick_chk.Checked = false;
            }
            e.Row.Attributes.Add("onmouseover", "MouseEvents(this, event)");

            e.Row.Attributes.Add("onmouseout", "MouseEvents(this, event)");

        }

    }
}