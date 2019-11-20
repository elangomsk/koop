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
using System.Text.RegularExpressions;


public partial class TKH_Sumbakan : System.Web.UI.Page
{

    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    string userid, level;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
                level = Session["level"].ToString();
                if (level == "5")
                {
                    Button2.Visible = true;
                  
                }
                else
                {
                    Button2.Visible = false;
                }
                wilayah();
                car.Visible = false;
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }


    void wilayah()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select wilayah_name,wilayah_code from Ref_Wilayah";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddwil.DataSource = dt;
            ddwil.DataTextField = "wilayah_name";
            ddwil.DataValueField = "wilayah_code";
            ddwil.DataBind();
            ddwil.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnButton = sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
            System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("lbl_rujukan");
            string lblid = lblTitle.Text;
           
            if  (lblTitle.Text != "")
            {
                string name = HttpUtility.UrlEncode(service.Encrypt(lblTitle.Text));
                Response.Redirect(string.Format("../TKH/Penerimaan_Butiran_TKH.aspx?edit={0}", name));
            }
            else
            {
                Session["validate_success"] = "";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void BindData()
    {
        string sqry = string.Empty;
        string fmdate = string.Empty, fmonth = string.Empty, fyear = string.Empty, stdate = string.Empty, tmdate = string.Empty, tmdate1 = string.Empty;
        if (Tk_mula.Text != "")
        {
            string fdate = Tk_mula.Text;
            DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            fmdate = fd.ToString("yyyy-MM-dd");
            fmonth = fd.ToString("MM");
            fyear = fd.ToString("yyyy");
        }
        if (Tk_akhir.Text != "")
        {
            string tdate = Tk_akhir.Text;
            DateTime td = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            tmdate = td.ToString("yyyy-MM-dd");
            tmdate1 = td.ToString("dd MMMM yyyy");
        }
        con.Open();
        DataTable ddicno = new DataTable();
        SqlCommand cmd = new SqlCommand("select tkh_tr_WP4_no,format(tkh_tr_WP4_dt,'dd/MM/yyyy')tkh_tr_WP4_dt,format(tkh_tr_WP4_lulus_dt,'dd/MM/yyyy') tkh_tr_WP4_lulus_dt,format(tkh_tr_bankin_dt,'dd/MM/yyyy')tkh_tr_bankin_dt,tkh_tr_WP4_loan_amt,tkh_tr_WP4_httks_amt,tkh_tr_WP4_tkh_amt,tkh_tr_WP4_baki_amt,tkh_tr_WP4_net_amt,tkh_tr_WP4_caj_amt,tkh_tr_WP4_stat,tkh_tr_WP4_stat_loc from tkh_terima where tkh_tr_region_cd='" + ddwil.SelectedItem.Text + "' and  tkh_tr_start_dt>=DATEADD(day,DATEDIFF(day,0,'" + fmdate + "'),0) and tkh_tr_end_dt<=DATEADD(day,DATEDIFF(day,0,'" + tmdate + "'),+1) and tkh_tr_wp4_stat in ('BARU','SEMAKAN-WILAYAH','BATAL-WILAYAH','BATAL-HQ')", con);
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
    protected void Button4_Click(object sender, EventArgs e)
    {
        try
        {
        
            BindData();
            car.Visible = true;
           
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btn_reset(object sender, EventArgs e)
    {
        Response.Redirect("../TKH/Sumbakan.aspx");
    }

    protected void btn_tutub(object sender, EventArgs e)
    {
        Response.Redirect("../KSAIMB_Home.aspx");
    }
}