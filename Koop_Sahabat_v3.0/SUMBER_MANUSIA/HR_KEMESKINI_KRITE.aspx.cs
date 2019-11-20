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

public partial class HR_KEMESKINI_KRITE : System.Web.UI.Page
{

    DBConnection dbcon = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DataTable dt = new DataTable();
    string useid = string.Empty;
    string Status = string.Empty;
    string oid = string.Empty;
    string sdd = string.Empty;
    string add = string.Empty;
    string val1 = string.Empty, val2 = string.Empty, val3 = string.Empty, val4 = string.Empty;
    string CommandArgument1 = string.Empty, CommandArgument2 = string.Empty, CommandArgument3 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Button3);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                useid = Session["New"].ToString();
                //sectionBind();
                bind_Jaw();
                UnitBind();
                grid_pkp();

                TextBox1.Attributes.Add("Readonly", "Readonly");
                TextBox3.Attributes.Add("Readonly", "Readonly");
            }
        }

    }


    void bind_Jaw()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select hr_jaw_code,UPPER(hr_jaw_desc) as hr_jaw_desc from ref_hr_jawatan ORDER BY hr_jaw_code";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddkat_jaw.DataSource = dt;
            ddkat_jaw.DataTextField = "hr_jaw_desc";
            ddkat_jaw.DataValueField = "hr_jaw_code";
            ddkat_jaw.DataBind();
            ddkat_jaw.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //void Jaw()
    //{
    //    DataSet Ds = new DataSet();
    //    try
    //    {

    //        string com = "select hr_kejaw_Code,upper(hr_kejaw_desc) as hr_kejaw_desc from Ref_hr_kategori_perjawatn";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
    //        DataTable dt = new DataTable();
    //        adpt.Fill(dt);
    //        ddkat_jaw.DataSource = dt;
    //        ddkat_jaw.DataBind();
    //        ddkat_jaw.DataTextField = "hr_kejaw_desc";
    //        ddkat_jaw.DataValueField = "hr_kejaw_Code";
    //        ddkat_jaw.DataBind();
    //        ddkat_jaw.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //void sectionBind()
    //{
    //    DataSet Ds = new DataSet();
    //    try
    //    {
    //        string com = "select UPPER(cse_section_desc) as cse_section_desc,cse_section_cd from hr_cmn_appr_section ORDER by cse_section_cd";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
    //        DataTable dt = new DataTable();
    //        adpt.Fill(dt);
    //        dd_bahag.DataSource = dt;
    //        dd_bahag.DataTextField = "cse_section_desc";
    //        dd_bahag.DataValueField = "cse_section_cd";
    //        dd_bahag.DataBind();
    //        dd_bahag.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    void UnitBind()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select hr_unit_Code,upper(hr_unit_desc) as hr_unit_desc from Ref_hr_unit where Status='A' order by hr_unit_Code";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_unit.DataSource = dt;
            dd_unit.DataTextField = "hr_unit_desc";
            dd_unit.DataValueField = "hr_unit_Code";
            dd_unit.DataBind();
            dd_unit.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //protected void sel_section(object sender, EventArgs e)
    //{
    //    DataSet Ds = new DataSet();
    //    try
    //    {
    //        string com = "select csb_section_cd,csb_subject_cd,UPPER(csb_subject_desc) as csb_subject_desc from hr_cmn_subject where csb_section_cd='" + dd_bahag.SelectedValue + "' order by csb_subject_cd";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
    //        DataTable dt = new DataTable();
    //        adpt.Fill(dt);
    //        dd_subject.DataSource = dt;
    //        dd_subject.DataTextField = "csb_subject_desc";
    //        dd_subject.DataValueField = "csb_subject_cd";
    //        dd_subject.DataBind();
    //        dd_subject.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    //        grid_pkp();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}


    protected void btn_Senar_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text != "" && TextBox3.Text != "" || ddkat_jaw.SelectedValue != "" || dd_unit.SelectedValue != "")
        {
            grid_pkp();
        }
        else
        {
            grid_pkp();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Sila Masukkan yang Maklumat');", true);
        }
    }


    void grid_pkp()
    {
        sel_qry();
        SqlCommand cmd2 = new SqlCommand("" + val1 + "", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            GridView1.DataSource = ds2;
            GridView1.DataBind();
            int columncount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView1.Rows[0].Cells[0].Text = "<strong>Maklumat Carian Tidak Dijumpai</strong>";
            if (TextBox1.Text != "" && TextBox3.Text != "" || ddkat_jaw.SelectedValue != "" || dd_unit.SelectedValue != "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Maklumat Carian Tidak Dijumpai');", true);
            }
        }
        else
        {
            GridView1.DataSource = ds2;
            GridView1.DataBind();
            Button3.Attributes.Remove("style");
        }
    }

    void sel_qry()
    {
        string stdt = string.Empty, eddt = string.Empty;
        if (TextBox1.Text != "")
        {

            DateTime dt_1 = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            stdt = dt_1.ToString("yyyy-MM-dd");
        }
        if (TextBox3.Text != "")
        {
            DateTime dt_2 = DateTime.ParseExact(TextBox3.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            eddt = dt_2.ToString("yyyy-MM-dd");
        }
        if (TextBox1.Text != "" && TextBox3.Text != "" && ddkat_jaw.SelectedValue == "" && dd_unit.SelectedValue == "")
        {
            //val1 = "select a.cap_seq_no,cas.cse_section_desc,cs.csb_subject_desc,ci.cit_desc,ci.cit_item_weightage from (select * from hr_cmn_appraisal as ca where ca.cap_start_dt = '" + stdt + "' and ca.cap_end_dt ='" + eddt + "' and ca.cap_post_cat_cd ='" + ddkat_jaw.SelectedValue + "' and ca.cap_unit_cd ='" + dd_unit.SelectedValue + "' and ca.cap_section_cd ='" + dd_bahag.SelectedValue + "' and ca.cap_subject_cd ='" + dd_subject.SelectedValue + "') a left join hr_cmn_appr_section as cas on cas.cse_section_cd=a.cap_section_cd left join hr_cmn_subject as cs on cs.csb_subject_cd=a.cap_subject_cd left join hr_cmn_item as ci on ci.cit_seq_no = a.cap_seq_no and ci.cit_section_cd=a.cap_section_cd and ci.cit_subject_cd=a.cap_subject_cd";
            val1 = "select FORMAT(a.cap_start_dt,'yyyy-MM-dd') as dt1,FORMAT(a.cap_end_dt,'yyyy-MM-dd') as dt2,a.cap_post_cat_cd,a.cap_unit_cd,a.cap_section_cd,a.cap_subject_cd,a.cap_seq_no,cas.cse_section_desc,cs.csb_subject_desc,a.cap_weightage,c1.hr_jaw_desc,c2.hr_unit_desc from (select * from hr_cmn_appraisal as ca where ca.cap_start_dt>=DATEADD(day, DATEDIFF(day, 0, '" + stdt + "'), 0) and ca.cap_start_dt<=DATEADD(day, DATEDIFF(day, 0, '" + eddt + "'), +0)) a left join hr_cmn_appr_section as cas on cas.cse_section_cd=a.cap_section_cd left join hr_cmn_subject as cs on cs.csb_subject_cd=a.cap_subject_cd left join Ref_hr_Jawatan c1 on c1.hr_jaw_Code=a.cap_post_cat_cd left join Ref_hr_unit c2 on c2.hr_unit_Code=a.cap_unit_cd";
        }
        else if (TextBox1.Text != "" && TextBox3.Text != "" && ddkat_jaw.SelectedValue != "" && dd_unit.SelectedValue == "")
        {
            //val1 = "select a.cap_seq_no,cas.cse_section_desc,cs.csb_subject_desc,ci.cit_desc,ci.cit_item_weightage from (select * from hr_cmn_appraisal as ca where ca.cap_start_dt = '" + stdt + "' and ca.cap_end_dt ='" + eddt + "' and ca.cap_post_cat_cd ='" + ddkat_jaw.SelectedValue + "' and ca.cap_unit_cd ='" + dd_unit.SelectedValue + "' and ca.cap_section_cd ='" + dd_bahag.SelectedValue + "' and ca.cap_subject_cd ='" + dd_subject.SelectedValue + "') a left join hr_cmn_appr_section as cas on cas.cse_section_cd=a.cap_section_cd left join hr_cmn_subject as cs on cs.csb_subject_cd=a.cap_subject_cd left join hr_cmn_item as ci on ci.cit_seq_no = a.cap_seq_no and ci.cit_section_cd=a.cap_section_cd and ci.cit_subject_cd=a.cap_subject_cd";
            val1 = "select FORMAT(a.cap_start_dt,'yyyy-MM-dd') as dt1,FORMAT(a.cap_end_dt,'yyyy-MM-dd') as dt2,a.cap_post_cat_cd,a.cap_unit_cd,a.cap_section_cd,a.cap_subject_cd,a.cap_seq_no,cas.cse_section_desc,cs.csb_subject_desc,a.cap_weightage,c1.hr_jaw_desc,c2.hr_unit_desc from (select * from hr_cmn_appraisal as ca where ca.cap_start_dt>=DATEADD(day, DATEDIFF(day, 0, '" + stdt + "'), 0) and ca.cap_start_dt<=DATEADD(day, DATEDIFF(day, 0, '" + eddt + "'), +0) and ca.cap_post_cat_cd ='" + ddkat_jaw.SelectedValue + "') a left join hr_cmn_appr_section as cas on cas.cse_section_cd=a.cap_section_cd left join hr_cmn_subject as cs on cs.csb_subject_cd=a.cap_subject_cd left join Ref_hr_Jawatan c1 on c1.hr_jaw_Code=a.cap_post_cat_cd left join Ref_hr_unit c2 on c2.hr_unit_Code=a.cap_unit_cd";
        }
        else if (TextBox1.Text != "" && TextBox3.Text != "" && ddkat_jaw.SelectedValue != "" && dd_unit.SelectedValue != "")
        {
            //val1 = "select a.cap_seq_no,cas.cse_section_desc,cs.csb_subject_desc,ci.cit_desc,ci.cit_item_weightage from (select * from hr_cmn_appraisal as ca where ca.cap_start_dt = '" + stdt + "' and ca.cap_end_dt ='" + eddt + "' and ca.cap_post_cat_cd ='" + ddkat_jaw.SelectedValue + "' and ca.cap_unit_cd ='" + dd_unit.SelectedValue + "' and ca.cap_section_cd ='" + dd_bahag.SelectedValue + "' and ca.cap_subject_cd ='" + dd_subject.SelectedValue + "') a left join hr_cmn_appr_section as cas on cas.cse_section_cd=a.cap_section_cd left join hr_cmn_subject as cs on cs.csb_subject_cd=a.cap_subject_cd left join hr_cmn_item as ci on ci.cit_seq_no = a.cap_seq_no and ci.cit_section_cd=a.cap_section_cd and ci.cit_subject_cd=a.cap_subject_cd";
            val1 = "select FORMAT(a.cap_start_dt,'yyyy-MM-dd') as dt1,FORMAT(a.cap_end_dt,'yyyy-MM-dd') as dt2,a.cap_post_cat_cd,a.cap_unit_cd,a.cap_section_cd,a.cap_subject_cd,a.cap_seq_no,cas.cse_section_desc,cs.csb_subject_desc,a.cap_weightage,c1.hr_jaw_desc,c2.hr_unit_desc from (select * from hr_cmn_appraisal as ca where ca.cap_start_dt>=DATEADD(day, DATEDIFF(day, 0, '" + stdt + "'), 0) and ca.cap_start_dt<=DATEADD(day, DATEDIFF(day, 0, '" + eddt + "'), +0) and ca.cap_post_cat_cd ='" + ddkat_jaw.SelectedValue + "' and ca.cap_unit_cd ='" + dd_unit.SelectedValue + "') a left join hr_cmn_appr_section as cas on cas.cse_section_cd=a.cap_section_cd left join hr_cmn_subject as cs on cs.csb_subject_cd=a.cap_subject_cd left join Ref_hr_Jawatan c1 on c1.hr_jaw_Code=a.cap_post_cat_cd left join Ref_hr_unit c2 on c2.hr_unit_Code=a.cap_unit_cd";
        }
        else if (TextBox1.Text != "" && TextBox3.Text != "" && ddkat_jaw.SelectedValue == "" && dd_unit.SelectedValue != "")
        {
            //val1 = "select a.cap_seq_no,cas.cse_section_desc,cs.csb_subject_desc,ci.cit_desc,ci.cit_item_weightage from (select * from hr_cmn_appraisal as ca where ca.cap_start_dt = '" + stdt + "' and ca.cap_end_dt ='" + eddt + "' and ca.cap_post_cat_cd ='" + ddkat_jaw.SelectedValue + "' and ca.cap_unit_cd ='" + dd_unit.SelectedValue + "' and ca.cap_section_cd ='" + dd_bahag.SelectedValue + "' and ca.cap_subject_cd ='" + dd_subject.SelectedValue + "') a left join hr_cmn_appr_section as cas on cas.cse_section_cd=a.cap_section_cd left join hr_cmn_subject as cs on cs.csb_subject_cd=a.cap_subject_cd left join hr_cmn_item as ci on ci.cit_seq_no = a.cap_seq_no and ci.cit_section_cd=a.cap_section_cd and ci.cit_subject_cd=a.cap_subject_cd";
            val1 = "select FORMAT(a.cap_start_dt,'yyyy-MM-dd') as dt1,FORMAT(a.cap_end_dt,'yyyy-MM-dd') as dt2,a.cap_post_cat_cd,a.cap_unit_cd,a.cap_section_cd,a.cap_subject_cd,a.cap_seq_no,cas.cse_section_desc,cs.csb_subject_desc,a.cap_weightage,c1.hr_jaw_desc,c2.hr_unit_desc from (select * from hr_cmn_appraisal as ca where ca.cap_start_dt>=DATEADD(day, DATEDIFF(day, 0, '" + stdt + "'), 0) and ca.cap_start_dt<=DATEADD(day, DATEDIFF(day, 0, '" + eddt + "'), +0) and ca.cap_unit_cd ='" + dd_unit.SelectedValue + "') a left join hr_cmn_appr_section as cas on cas.cse_section_cd=a.cap_section_cd left join hr_cmn_subject as cs on cs.csb_subject_cd=a.cap_subject_cd left join Ref_hr_Jawatan c1 on c1.hr_jaw_Code=a.cap_post_cat_cd left join Ref_hr_unit c2 on c2.hr_unit_Code=a.cap_unit_cd";
        }
        else if (TextBox1.Text == "" && TextBox3.Text == "" && ddkat_jaw.SelectedValue != "" && dd_unit.SelectedValue == "")
        {
            //val1 = "select a.cap_seq_no,cas.cse_section_desc,cs.csb_subject_desc,ci.cit_desc,ci.cit_item_weightage from (select * from hr_cmn_appraisal as ca where ca.cap_start_dt = '" + stdt + "' and ca.cap_end_dt ='" + eddt + "' and ca.cap_post_cat_cd ='" + ddkat_jaw.SelectedValue + "' and ca.cap_unit_cd ='" + dd_unit.SelectedValue + "' and ca.cap_section_cd ='" + dd_bahag.SelectedValue + "' and ca.cap_subject_cd ='" + dd_subject.SelectedValue + "') a left join hr_cmn_appr_section as cas on cas.cse_section_cd=a.cap_section_cd left join hr_cmn_subject as cs on cs.csb_subject_cd=a.cap_subject_cd left join hr_cmn_item as ci on ci.cit_seq_no = a.cap_seq_no and ci.cit_section_cd=a.cap_section_cd and ci.cit_subject_cd=a.cap_subject_cd";
            val1 = "select FORMAT(a.cap_start_dt,'yyyy-MM-dd') as dt1,FORMAT(a.cap_end_dt,'yyyy-MM-dd') as dt2,a.cap_post_cat_cd,a.cap_unit_cd,a.cap_section_cd,a.cap_subject_cd,a.cap_seq_no,cas.cse_section_desc,cs.csb_subject_desc,a.cap_weightage,c1.hr_jaw_desc,c2.hr_unit_desc from (select * from hr_cmn_appraisal as ca where ca.cap_post_cat_cd ='" + ddkat_jaw.SelectedValue + "') a left join hr_cmn_appr_section as cas on cas.cse_section_cd=a.cap_section_cd left join hr_cmn_subject as cs on cs.csb_subject_cd=a.cap_subject_cd left join Ref_hr_Jawatan c1 on c1.hr_jaw_Code=a.cap_post_cat_cd left join Ref_hr_unit c2 on c2.hr_unit_Code=a.cap_unit_cd";
        }
        else if (TextBox1.Text == "" && TextBox3.Text == "" && ddkat_jaw.SelectedValue == "" && dd_unit.SelectedValue != "")
        {
            //val1 = "select a.cap_seq_no,cas.cse_section_desc,cs.csb_subject_desc,ci.cit_desc,ci.cit_item_weightage from (select * from hr_cmn_appraisal as ca where ca.cap_start_dt = '" + stdt + "' and ca.cap_end_dt ='" + eddt + "' and ca.cap_post_cat_cd ='" + ddkat_jaw.SelectedValue + "' and ca.cap_unit_cd ='" + dd_unit.SelectedValue + "' and ca.cap_section_cd ='" + dd_bahag.SelectedValue + "' and ca.cap_subject_cd ='" + dd_subject.SelectedValue + "') a left join hr_cmn_appr_section as cas on cas.cse_section_cd=a.cap_section_cd left join hr_cmn_subject as cs on cs.csb_subject_cd=a.cap_subject_cd left join hr_cmn_item as ci on ci.cit_seq_no = a.cap_seq_no and ci.cit_section_cd=a.cap_section_cd and ci.cit_subject_cd=a.cap_subject_cd";
            val1 = "select FORMAT(a.cap_start_dt,'yyyy-MM-dd') as dt1,FORMAT(a.cap_end_dt,'yyyy-MM-dd') as dt2,a.cap_post_cat_cd,a.cap_unit_cd,a.cap_section_cd,a.cap_subject_cd,a.cap_seq_no,cas.cse_section_desc,cs.csb_subject_desc,a.cap_weightage,c1.hr_jaw_desc,c2.hr_unit_desc from (select * from hr_cmn_appraisal as ca where ca.cap_unit_cd ='" + dd_unit.SelectedValue + "') a left join hr_cmn_appr_section as cas on cas.cse_section_cd=a.cap_section_cd left join hr_cmn_subject as cs on cs.csb_subject_cd=a.cap_subject_cd left join Ref_hr_Jawatan c1 on c1.hr_jaw_Code=a.cap_post_cat_cd left join Ref_hr_unit c2 on c2.hr_unit_Code=a.cap_unit_cd";
        }
        else if (TextBox1.Text == "" && TextBox3.Text == "" && ddkat_jaw.SelectedValue != "" && dd_unit.SelectedValue != "")
        {
            //val1 = "select a.cap_seq_no,cas.cse_section_desc,cs.csb_subject_desc,ci.cit_desc,ci.cit_item_weightage from (select * from hr_cmn_appraisal as ca where ca.cap_start_dt = '" + stdt + "' and ca.cap_end_dt ='" + eddt + "' and ca.cap_post_cat_cd ='" + ddkat_jaw.SelectedValue + "' and ca.cap_unit_cd ='" + dd_unit.SelectedValue + "' and ca.cap_section_cd ='" + dd_bahag.SelectedValue + "' and ca.cap_subject_cd ='" + dd_subject.SelectedValue + "') a left join hr_cmn_appr_section as cas on cas.cse_section_cd=a.cap_section_cd left join hr_cmn_subject as cs on cs.csb_subject_cd=a.cap_subject_cd left join hr_cmn_item as ci on ci.cit_seq_no = a.cap_seq_no and ci.cit_section_cd=a.cap_section_cd and ci.cit_subject_cd=a.cap_subject_cd";
            val1 = "select FORMAT(a.cap_start_dt,'yyyy-MM-dd') as dt1,FORMAT(a.cap_end_dt,'yyyy-MM-dd') as dt2,a.cap_post_cat_cd,a.cap_unit_cd,a.cap_section_cd,a.cap_subject_cd,a.cap_seq_no,cas.cse_section_desc,cs.csb_subject_desc,a.cap_weightage,c1.hr_jaw_desc,c2.hr_unit_desc from (select * from hr_cmn_appraisal as ca where ca.cap_post_cat_cd ='" + ddkat_jaw.SelectedValue + "' and ca.cap_unit_cd ='" + dd_unit.SelectedValue + "') a left join hr_cmn_appr_section as cas on cas.cse_section_cd=a.cap_section_cd left join hr_cmn_subject as cs on cs.csb_subject_cd=a.cap_subject_cd left join Ref_hr_Jawatan c1 on c1.hr_jaw_Code=a.cap_post_cat_cd left join Ref_hr_unit c2 on c2.hr_unit_Code=a.cap_unit_cd";
        }
        else
        {
            val1 = "select FORMAT(a.cap_start_dt,'yyyy-MM-dd') as dt1,FORMAT(a.cap_end_dt,'yyyy-MM-dd') as dt2,a.cap_post_cat_cd,a.cap_unit_cd,a.cap_section_cd,a.cap_subject_cd,a.cap_seq_no,cas.cse_section_desc,cs.csb_subject_desc,a.cap_weightage,c1.hr_jaw_desc,c2.hr_unit_desc from (select * from hr_cmn_appraisal as ca where ca.cap_post_cat_cd ='' and ca.cap_unit_cd ='' and ca.cap_section_cd ='' and ca.cap_subject_cd ='') a left join hr_cmn_appr_section as cas on cas.cse_section_cd=a.cap_section_cd left join hr_cmn_subject as cs on cs.csb_subject_cd=a.cap_subject_cd left join Ref_hr_Jawatan c1 on c1.hr_jaw_Code=a.cap_post_cat_cd left join Ref_hr_unit c2 on c2.hr_unit_Code=a.cap_unit_cd";
        }
    }

    protected void gvSelected_PageIndexChanging_jmk(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        grid_pkp();

    }

    protected void btn_jenis_simp_Click(object sender, EventArgs e)
    {
        try
        {
            string rcount = string.Empty;
            int count = 0;
            int num = 0;
            foreach (GridViewRow gvrow in GridView1.Rows)
            {
                count++;
                rcount = count.ToString();
                var v4 = gvrow.FindControl("ss_pemberat") as System.Web.UI.WebControls.TextBox;
                num += Convert.ToInt32(v4.Text);
            }
            if (num <= 100)
            {
                if (rcount != "0")
                {
                    foreach (GridViewRow gvrow in GridView1.Rows)
                    {
                        var sqno = gvrow.FindControl("ss_sqno") as System.Web.UI.WebControls.Label;
                        var l1 = gvrow.FindControl("Label1") as System.Web.UI.WebControls.Label;
                        var l2 = gvrow.FindControl("Label2") as System.Web.UI.WebControls.Label;
                        var l3 = gvrow.FindControl("Label3") as System.Web.UI.WebControls.Label;
                        var l4 = gvrow.FindControl("Label4") as System.Web.UI.WebControls.Label;
                        var l5 = gvrow.FindControl("Label5") as System.Web.UI.WebControls.Label;
                        var l6 = gvrow.FindControl("Label6") as System.Web.UI.WebControls.Label;
                        var l7 = gvrow.FindControl("ss_pemberat") as System.Web.UI.WebControls.TextBox;
                        string Inssql = "UPDATE hr_cmn_appraisal set cap_weightage='" + l7.Text + "',cap_upd_id='" + Session["New"].ToString() + "',cap_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "' where cap_start_dt='" + l1.Text + "' and cap_end_dt='" + l2.Text + "' and cap_post_cat_cd='" + l3.Text + "' and cap_unit_cd='" + l4.Text + "' and cap_section_cd='" + l5.Text + "' and cap_subject_cd='" + l6.Text + "' and cap_seq_no='" + sqno.Text + "'";
                        Status = dbcon.Ora_Execute_CommamdText(Inssql);
                    }
                    if (Status == "SUCCESS")
                    {
                        grid_pkp();
                        Ins_aud();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Rekod Berjaya Dikemaskini.');", true);
                    }
                    else
                    {
                        grid_pkp();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Issue.');", true);
                    }
                }
                else
                {
                    grid_pkp();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Maklumat Carian Tidak Dijumpai');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Sila Semak Isu');", true);

            }

        }
        catch (Exception ex)
        {
            grid_pkp();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Sila Semak Isu');", true);

        }
    }


    protected void btn_hapus_Click(object sender, EventArgs e)
    {
        try
        {

            string rcount = string.Empty;
            int count = 0;
            foreach (GridViewRow gvrow in GridView1.Rows)
            {
                var checkbox = gvrow.FindControl("s_hapus") as System.Web.UI.WebControls.CheckBox;
                if (checkbox.Checked)
                {
                    count++;
                }
                rcount = count.ToString();
            }

            if (rcount != "0")
            {
                foreach (GridViewRow gvrow in GridView1.Rows)
                {
                    var checkbox = gvrow.FindControl("s_hapus") as System.Web.UI.WebControls.CheckBox;
                    if (checkbox.Checked == true)
                    {
                        var sqno = gvrow.FindControl("ss_sqno") as System.Web.UI.WebControls.Label;
                        var l1 = gvrow.FindControl("Label1") as System.Web.UI.WebControls.Label;
                        var l2 = gvrow.FindControl("Label2") as System.Web.UI.WebControls.Label;
                        var l3 = gvrow.FindControl("Label3") as System.Web.UI.WebControls.Label;
                        var l4 = gvrow.FindControl("Label4") as System.Web.UI.WebControls.Label;
                        var l5 = gvrow.FindControl("Label5") as System.Web.UI.WebControls.Label;
                        var l6 = gvrow.FindControl("Label6") as System.Web.UI.WebControls.Label;

                        string Inssql = "delete hr_cmn_appraisal where cap_start_dt='" + l1.Text + "' and cap_end_dt='" + l2.Text + "' and cap_post_cat_cd='" + l3.Text + "' and cap_unit_cd='" + l4.Text + "' and cap_section_cd='" + l5.Text + "' and cap_subject_cd='" + l6.Text + "' and cap_seq_no='" + sqno.Text + "'";
                        Status = dbcon.Ora_Execute_CommamdText(Inssql);
                    }
                }
                grid_pkp();
                Ins_aud();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Rekod Berjaya Dikemaskini');", true);
            }
            else
            {
                grid_pkp();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Sila Pilih Minimum Satu Kotak');", true);
            }

        }
        catch (Exception ex)
        {
            grid_pkp();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Sila Semak Isu');", true);

        }
    }

    protected void ctk_values(object sender, EventArgs e)
    {

        sel_qry();

        string stdt1 = string.Empty, eddt1 = string.Empty, vv1 = string.Empty, vv2 = string.Empty;
        if (TextBox1.Text != "")
        {

            DateTime dt_1 = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            stdt1 = dt_1.ToString("yyyy-MM-dd");
        }
        else
        {
            stdt1 = "SEMUA";
        }
        if (TextBox3.Text != "")
        {
            DateTime dt_2 = DateTime.ParseExact(TextBox3.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            eddt1 = dt_2.ToString("yyyy-MM-dd");
        }
        else
        {
            eddt1 = "SEMUA";
        }
        if (ddkat_jaw.SelectedValue != "")
        {
            vv1 = ddkat_jaw.SelectedItem.Text;
        }
        else
        {
            vv1 = "SEMUA";
        }

        if (dd_unit.SelectedValue != "")
        {
            vv2 = dd_unit.SelectedItem.Text;
        }
        else
        {
            vv2 = "SEMUA";
        }

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        dt = dbcon.Ora_Execute_table("" + val1 + "");
        RptviwerStudent.Reset();
        ds.Tables.Add(dt);

        List<DataRow> listResult = dt.AsEnumerable().ToList();
        listResult.Count();
        int countRow = 0;
        countRow = listResult.Count();

        RptviwerStudent.LocalReport.DataSources.Clear();
        if (countRow != 0)
        {
            RptviwerStudent.LocalReport.ReportPath = "HR_pen_pertasi1.rdlc";
            ReportDataSource rds = new ReportDataSource("HR_pen_pertasi1", dt);
            RptviwerStudent.LocalReport.DataSources.Add(rds);

            ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("s1", stdt1),
                     new ReportParameter("s2", eddt1),
                     new ReportParameter("s3", vv1),
                     new ReportParameter("s4", vv2)
                     };
            RptviwerStudent.LocalReport.SetParameters(rptParams);

            RptviwerStudent.LocalReport.Refresh();


            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            string filename;


            filename = string.Format("{0}.{1}", "Kemaskini_Dokumen_Penilaian_Prestasi_" + DateTime.Now.ToString("ddMMyyyy") + "", "pdf");
            byte[] bytes = RptviwerStudent.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename=" + filename);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();

        }
        else if (countRow == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Rekod tidak dijumpai. Sila Pastikan Semua Maklumat Dimasukkan Dengan Betul');", true);
        }

    }

    protected void rst_clk(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void Ins_aud()
    {
        String a_text1 = "040605";
        String a_text2 = "KEMASUKAN MAKLUMAT PENILAIAN PRESTASI";
        DataTable ins_aud = new DataTable();
        dbcon.Execute_CommamdText("insert into cmn_audit_trail (aud_crt_id,aud_crt_dt,aud_txn_cd,aud_txn_desc) values ('" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + a_text1 + "','" + a_text2 + "')");
    }
}