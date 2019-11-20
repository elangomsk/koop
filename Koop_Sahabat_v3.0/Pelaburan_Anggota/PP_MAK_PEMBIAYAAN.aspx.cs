using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.ComponentModel;
using System.Text;
using System.Net;
using System.Data.Common;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Net.Mail;
using System.Threading;
using System.Windows.Forms;


public partial class PP_MAK_PEMBIAYAAN : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    DataTable wilayah = new DataTable();
    StudentWebService service = new StudentWebService();

    string level, userid;
    String status, JM_status, strSektorPekerjaan, strSektorPekerjaan1, strSektorPekerjaan2, strAnakSyarikat, strAnakSyarikat1, strAnakSyarikat2, strTarafJawatan, strTarafJawatan1, strTarafJawatan2;
    string Status = string.Empty;
    String seq_count;
    String fmdate = String.Empty;
    String tmdate = String.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!this.IsPostBack)
        {
            if (Session["New"] != null)
            {

                level = Session["level"].ToString();
                userid = Session["New"].ToString();
                f_date.Attributes.Add("Readonly", "Readonly");
                t_date.Attributes.Add("Readonly", "Readonly");
                Pelaburan();
                if (Session["sess_fdt"] != "" && Session["sess_tdt"] != "" && Session["sess_jp"] != "" && Session["sess_sp"] != "" && Session["sess_fdt"] != null && Session["sess_tdt"] != null && Session["sess_jp"] != null && Session["sess_sp"] != null)
                {
                    f_date.Text = Session["sess_fdt"].ToString();
                    t_date.Text = Session["sess_tdt"].ToString();
                    DD_Pelaburan.SelectedValue = Session["sess_jp"].ToString();
                    ST_Pelaburan.SelectedValue = Session["sess_sp"].ToString();
                    srch();
                }
                grid();
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }

    }

    void Pelaburan()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select * from Ref_Jenis_Pelaburan WHERE Status = 'A' order by Description ASC";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_Pelaburan.DataSource = dt;
            DD_Pelaburan.DataTextField = "Description";
            DD_Pelaburan.DataValueField = "Description_Code";
            DD_Pelaburan.DataBind();
            DD_Pelaburan.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnsrch_Click(object sender, EventArgs e)
    {
        srch();
    }
    protected void srch()
    {
        try
        {
            Session.Remove("sess_fdt");
            Session.Remove("sess_tdt");
            Session.Remove("sess_jp");
            Session.Remove("sess_sp");
            if (f_date.Text != "" && t_date.Text != "" && DD_Pelaburan.SelectedValue != "")
            {
                string fdate = f_date.Text;
                DateTime ft = DateTime.ParseExact(fdate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                fmdate = ft.ToString("yyyy-mm-dd");

                string tdate = t_date.Text;
                DateTime tt = DateTime.ParseExact(tdate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                tmdate = tt.ToString("yyyy-mm-dd");

                grid();
            }
            else
            {
                grid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void grid()
    {
        //Button5.Visible = false;
        if (f_date.Text != "")
        {
            string fdate = f_date.Text;
            DateTime ft = DateTime.ParseExact(fdate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            fmdate = ft.ToString("yyyy-mm-dd");
        }
        if (t_date.Text != "")
        {
            string tdate = t_date.Text;
            DateTime tt = DateTime.ParseExact(tdate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            tmdate = tt.ToString("yyyy-mm-dd");
        }
        string var_id = string.Empty, var_id1 = string.Empty;
        if (ST_Pelaburan.SelectedValue == "T")
        {
            //var_id = "Y";
            var_id = "and jw.app_backdated_amt < '0.00'";
        }
        else if (ST_Pelaburan.SelectedValue == "N")
        {
            var_id = "and jw.app_backdated_amt > '0.00'";
        }
        else if (ST_Pelaburan.SelectedValue == "S")
        {
            var_id = "and app_sts_cd = 'S'";
        }
        else
        {
            var_id = "";
        }


        if (tt_v1.Text != "" && tt_v2.Text != "")
        {
            var_id1 = "where a.BT>='" + tt_v1.Text + "' and a.BT<='" + tt_v2.Text + "'";
        }
        else
        {
            var_id1 = "";
        }
        string sqry = string.Empty;
        if (f_date.Text != "" && t_date.Text != "" && DD_Pelaburan.SelectedValue != "")
        {
            sqry = "select * from (select app_applcn_no,app_name,app_new_icno,app_loan_amt,app_installment_amt,appl_loan_dur,app_backdated_amt,app_bal_loan_amt,case WHEN (app_backdated_amt / app_cumm_installment_amt) > 0 THEN 0 ELSE abs(app_backdated_amt / app_cumm_installment_amt)  end as BT from jpa_application as jw where jw.applcn_clsed ='N' and jw.app_batch_last_upd_dt BETWEEN DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) AND DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), 0) " + var_id + " and jw.app_loan_type_cd='" + DD_Pelaburan.SelectedValue + "') a " + var_id1 + " ORDER BY a.BT";
        }
        else
        {
            sqry = "select * from (select app_applcn_no,app_name,app_new_icno,app_loan_amt,app_installment_amt,appl_loan_dur,app_backdated_amt,app_bal_loan_amt,case WHEN (app_backdated_amt / app_cumm_installment_amt) > 0 THEN 0 ELSE abs(app_backdated_amt / app_cumm_installment_amt)  end as BT from jpa_application as jw where jw.applcn_clsed ='N' and jw.app_batch_last_upd_dt BETWEEN DATEADD(day, DATEDIFF(day, 0, ''), 0) AND DATEADD(day, DATEDIFF(day, 0, ''), 0) " + var_id + " and jw.app_loan_type_cd='" + DD_Pelaburan.SelectedValue + "') a " + var_id1 + " ORDER BY a.BT";
        }

            con.Open();
        SqlCommand cmd = new SqlCommand(sqry, con);
        //SqlCommand cmd = new SqlCommand("select app_applcn_no,app_name,app_new_icno,app_loan_amt,app_installment_amt,appl_loan_dur,app_backdated_amt,app_bal_loan_amt,case WHEN (app_backdated_amt / app_cumm_installment_amt) > 0 THEN 0 ELSE abs(app_backdated_amt / app_cumm_installment_amt)  end as BT from jpa_application as jw where jw.app_batch_last_upd_dt BETWEEN DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) AND DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), 0) " + var_id + " and jw.app_loan_type_cd='" + DD_Pelaburan.SelectedValue + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {

            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gvSelected.DataSource = ds;
            gvSelected.DataBind();
            int columncount = gvSelected.Rows[0].Cells.Count;
            gvSelected.Rows[0].Cells.Clear();
            gvSelected.Rows[0].Cells.Add(new TableCell());
            gvSelected.Rows[0].Cells[0].ColumnSpan = columncount;
            gvSelected.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
            if (ST_Pelaburan.SelectedValue != "")
            {
                s_pel.Text = ST_Pelaburan.SelectedItem.Text ;
            }
            else
            {
                s_pel.Text = "";
            }
        }
        else
        {
            s_pel.Text = ST_Pelaburan.SelectedItem.Text;
            gvSelected.DataSource = ds;
            gvSelected.DataBind();

        }
        con.Close();
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        Session["sess_fdt"] = f_date.Text;
        Session["sess_tdt"] = t_date.Text;
        Session["sess_jp"] = DD_Pelaburan.SelectedValue;
        Session["sess_sp"] = ST_Pelaburan.SelectedValue;
        LinkButton btnButton = sender as LinkButton;
        GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
        System.Web.UI.WebControls.Label appicno = (System.Web.UI.WebControls.Label)gvRow.FindControl("Label3");
        string name = HttpUtility.UrlEncode(service.Encrypt(appicno.Text));
        Response.Redirect("../Pelaburan_Anggota/PP_SMPembiayaan.aspx?spi_icno=" + name);

    }

    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSelected.PageIndex = e.NewPageIndex;
        grid();
        gvSelected.DataBind();
    }

    protected void btn_rstclick(object sender, EventArgs e)
    {
        Response.Redirect("../Pelaburan_Anggota/PP_MAK_PEMBIAYAAN.aspx");
    }

   
}