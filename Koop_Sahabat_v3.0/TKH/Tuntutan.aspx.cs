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

public partial class TKH_Tuntutan : System.Web.UI.Page
{
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    string userid, level,wil;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
                level = Session["level"].ToString();
                wil = Session["Wil"].ToString();
                wilayah();
                Branch();
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
            DataTable dt = new DataTable();
            dt = DBCon.Ora_Execute_table("select wilayah_name from Ref_Wilayah where wilayah_code='" + wil + "'");
            if(dt.Rows.Count>0)
            {
                txtula1.Text = dt.Rows[0][0].ToString();
                txtula1.Attributes.Add("disabled", "disabled");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void Branch()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select cawangan_name,cawangan_code from Ref_Cawangan where wilayah_code='" + wil + "'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddcaw.DataSource = dt;
            ddcaw.DataTextField = "cawangan_name";
            ddcaw.DataValueField = "cawangan_code";
            ddcaw.DataBind();
            ddcaw.Items.Insert(0, new ListItem("--- PILIH ---", ""));
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
        SqlCommand cmd = new SqlCommand();
        if (ddcaw.SelectedItem.Text != "--- PILIH ---" && DropDownList2.SelectedItem.Text == "Semua")
        {
            cmd = new SqlCommand("select c.cawangan_name,tkh_tn_region_cd,tkh_tn_center_cd,tkh_tn_penuntut,tkh_tn_waris,tkh_tn_type,tkh_tn_sakit_dt,tkh_tn_produk,tkh_tn_lindung_amt,tkh_tn_stat,tkh_tn_stat_loc,tkh_tn_lulus_stat,tkh_tn_WP4_no,tkh_tn_kemaskini_wil_dt  from tkh_tuntutan t left join Ref_Cawangan c on t.tkh_tn_area_cd=c.cawangan_code where  tkh_tn_kemaskini_wil_dt>=DATEADD(day,DATEDIFF(day,0,'2018/04/09'),0) and tkh_tn_kemaskini_wil_dt<=DATEADD(day,DATEDIFF(day,0,'2018/04/09'),+1) and tkh_tn_region_cd='" + wil + "' and tkh_tn_area_cd='" + ddcaw.SelectedItem.Value + "' ", con);
        }
        else if (ddcaw.SelectedItem.Text != "--- PILIH ---" && DropDownList2.SelectedItem.Text != "Semua")
        {
            cmd = new SqlCommand("select c.cawangan_name,tkh_tn_region_cd,tkh_tn_center_cd,tkh_tn_penuntut,tkh_tn_waris,tkh_tn_type,tkh_tn_sakit_dt,tkh_tn_produk,tkh_tn_lindung_amt,tkh_tn_stat,tkh_tn_stat_loc,tkh_tn_lulus_stat,tkh_tn_WP4_no,tkh_tn_kemaskini_wil_dt  from tkh_tuntutan t left join Ref_Cawangan c on t.tkh_tn_area_cd=c.cawangan_code where  tkh_tn_kemaskini_wil_dt>=DATEADD(day,DATEDIFF(day,0,'2018/04/09'),0) and tkh_tn_kemaskini_wil_dt<=DATEADD(day,DATEDIFF(day,0,'2018/04/09'),+1) and tkh_tn_region_cd='" + wil + "' and tkh_tn_area_cd='" + ddcaw.SelectedItem.Value + "' and  tkh_tn_type='" + DropDownList2.SelectedItem.Value + "' ", con);
        }
        else
        {
            cmd = new SqlCommand("select c.cawangan_name,tkh_tn_region_cd,tkh_tn_center_cd,tkh_tn_penuntut,tkh_tn_waris,tkh_tn_type,tkh_tn_sakit_dt,tkh_tn_produk,tkh_tn_lindung_amt,tkh_tn_stat,tkh_tn_stat_loc,tkh_tn_lulus_stat,tkh_tn_WP4_no,tkh_tn_kemaskini_wil_dt  from tkh_tuntutan t left join Ref_Cawangan c on t.tkh_tn_area_cd=c.cawangan_code where  tkh_tn_kemaskini_wil_dt>=DATEADD(day,DATEDIFF(day,0,'2018/04/09'),0) and tkh_tn_kemaskini_wil_dt<=DATEADD(day,DATEDIFF(day,0,'2018/04/09'),+1) and tkh_tn_region_cd='" + wil + "'  ", con);
        }
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
    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnButton = sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
            System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("lbl_rujukan");
            string lblid = lblTitle.Text;

            if (lblTitle.Text != "")
            {
                string name = HttpUtility.UrlEncode(service.Encrypt(lblTitle.Text));
                Response.Redirect(string.Format("../TKH/Butiran_Tuntutan.aspx?edit={0}", name));
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

    protected void Button4_Click(object sender, EventArgs e)
    {
        try
        {

            BindData();
           

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Redirect("Butiran_Tuntutan.aspx");


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}