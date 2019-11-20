﻿using System;
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

public partial class Hr_sc_kakitangan_view : System.Web.UI.Page
{

    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
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
                if (Session["validate_success"].ToString() == "SUCCESS")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('" + Session["alrt_msg"].ToString() + "',{'type': 'confirmation','title': 'Success'});", true);

                }
                Session["validate_success"] = "";
                Session["alrt_msg"] = "";
                userid = Session["New"].ToString();
                BindData_Grid();
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    //protected void gvSelected_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    //{
    //    GridView1.PageIndex = e.NewPageIndex;
    //    GridView1.DataBind();
    //    BindData_jenis();
    //}
    protected void BindData_Grid()
    {

        string sqry = string.Empty;
        //if (txtSearch.Text == "")
        //{
        //    sqry = "";
        //}
        //else
        //{
        //    sqry = "where s2.Ref_Jenis_cukai LIKE'%" + txtSearch.Text + "%'";
        //}
        con.Open();
        DataTable ddicno = new DataTable();
        SqlCommand cmd = new SqlCommand("select a.stf_staff_no,a.str_curr_org_cd,a.stf_name,a.stf_curr_post_cd,a.stf_curr_job_cat_cd,a.hr_jaba_desc,a.hr_gred_desc,a.hr_kejaw_desc,a.hr_traf_desc,a.stf_carry_fwd_lv,a.tk_y,a.tk_m,a.d_yr,a.d_mth,CASE WHEN a.d_yr < 1 THEN convert(int,((select ann_leave_day from hr_cmn_annual_leave where ann_post_cd=a.stf_curr_post_cd and ann_post_cat_cd=a.stf_curr_job_cat_cd and ((a.d_yr) between (ann_min_service) And (ann_max_service))) * a.tk_m / 12)) ELSE (select ann_leave_day from hr_cmn_annual_leave where ann_post_cd=a.stf_curr_post_cd and ann_post_cat_cd=a.stf_curr_job_cat_cd and ((a.d_yr) between (ann_min_service) And (ann_max_service))) END AS ct_lday,convert(int, (select out_leave_day from hr_cmn_outpatient_leave where ((a.d_yr) between (out_min_yr) And (out_max_yr)))) cs_lday,(60 - convert(int, (select out_leave_day from hr_cmn_outpatient_leave where ((a.d_yr) between (out_min_yr) And (out_max_yr))))) as cs_hos from (select sp.stf_service_start_dt,sp.stf_staff_no,sp.str_curr_org_cd,sp.stf_name,rj.hr_jaba_desc,rg.hr_gred_desc,rkj.hr_kejaw_desc,rsj.hr_traf_desc,sp.stf_carry_fwd_lv,DATEDIFF( YEAR, stf_service_start_dt, GETDATE()) as tk_y,DATEDIFF( month, stf_service_start_dt, GETDATE()) as tk_m,round(datediff(m,stf_service_start_dt,GETDATE())/12,2)  as d_yr,round(datediff(m,stf_service_start_dt,GETDATE())%12,2) as d_mth,stf_curr_post_cd,stf_curr_job_cat_cd,stf_curr_dept_cd from hr_staff_profile as sp left join Ref_hr_jabatan as rj on rj.hr_jaba_Code=sp.stf_curr_dept_cd left join Ref_hr_gred as rg on rg.hr_gred_Code=sp.stf_curr_grade_cd left join Ref_hr_kategori_perjawatn as rkj on rkj.hr_kejaw_Code=sp.stf_curr_job_cat_cd left join Ref_hr_penj_traf as rsj on rsj.hr_traf_Code=sp.stf_curr_job_sts_cd where stf_service_end_dt = '' ) as a left join hr_cmn_annual_leave as hcal on hcal.ann_post_cd=a.stf_curr_post_cd and hcal.ann_post_cat_cd=a.stf_curr_job_cat_cd and hcal.ann_min_service <= a.d_yr and hcal.ann_max_service >= a.d_yr left join hr_cmn_outpatient_leave as hcol on hcol.out_min_yr <= a.d_yr and hcol.out_max_yr >= a.d_yr", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView1.DataSource = ds;
            GridView1.DataBind();
            int columncount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView1.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
        }
        else
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

        con.Close();
    }

    //protected void txtSearch_TextChanged(object sender, EventArgs e)
    //{

    //    SearchText();
    //}

    //protected void btn_search_Click(object sender, EventArgs e)
    //{
    //    BindData_jenis();
    //}



    //public string Highlight(string InputTxt)
    //{
    //    string Search_Str = txtSearch.Text.ToString();
    //    // Setup the regular expression and add the Or operator.
    //    Regex RegExp = new Regex(Search_Str.Replace(" ", "|").Trim(),
    //    RegexOptions.IgnoreCase);

    //    // Highlight keywords by calling the 
    //    //delegate each time a keyword is found.
    //    return RegExp.Replace(InputTxt,
    //    new MatchEvaluator(ReplaceKeyWords));

    //    // Set the RegExp to null.
    //    RegExp = null;

    //}

    //public string ReplaceKeyWords(Match m)
    //{

    //    return "<span class=highlight>" + m.Value + "</span>";

    //}
    //private void SearchText()
    //{
    //    BindData_jenis();

    //}

    protected void Add_profile(object sender, EventArgs e)
    {
        Response.Redirect("../SUMBER_MANUSIA/Hr_sc_kakitangan.aspx");
    }

    protected void btn_hups_Click(object sender, EventArgs e)
    {
        string rcount = string.Empty;
        int count = 0;
        foreach (GridViewRow gvrow in GridView1.Rows)
        {
            var rb = gvrow.FindControl("RadioButton1") as System.Web.UI.WebControls.CheckBox;
            if (rb.Checked)
            {
                count++;
            }
            rcount = count.ToString();
        }
        if (rcount != "0")
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                var rbn = row.FindControl("RadioButton1") as System.Web.UI.WebControls.CheckBox;
                if (rbn.Checked)
                {
                    int RowIndex = row.RowIndex;
                    string varName1 = ((System.Web.UI.WebControls.Label)row.FindControl("lbl1_id")).Text.ToString(); //this store the  value in varName1
                    SqlCommand ins_peng = new SqlCommand("Delete from hr_cmn_appr_section where id='" + varName1 + "'", conn);
                    conn.Open();
                    int i = ins_peng.ExecuteNonQuery();
                    conn.Close();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dihapuskan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    BindData_Grid();
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
        string script = " $(function () {$(" + GridView1.ClientID + ").prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        BindData_Grid();
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnButton = sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
            System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("lbl1_id");
            string lblid1 = lblTitle.Text;
            DataTable ddokdicno = new DataTable();
            ddokdicno = DBCon.Ora_Execute_table("select * From hr_cmn_appr_section where Id='" + lblTitle.Text + "' ");
            if (ddokdicno.Rows.Count != 0)
            {
                string name = HttpUtility.UrlEncode(service.Encrypt(lblTitle.Text));
                //Response.Redirect("../kewengan/kw_profil_syarikat.aspx?edit={0}"+ og_genid.Text + "");
                Response.Redirect(string.Format("../SUMBER_MANUSIA/Hr_sc_kakitangan.aspx?edit={0}", name));
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
}