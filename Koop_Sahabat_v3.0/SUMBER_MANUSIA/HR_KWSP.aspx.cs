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
using System.Threading;

public partial class HR_KWSP : System.Web.UI.Page
{

    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;

    DBConnection Con = new DBConnection();
    DataTable dt = new DataTable();
    string level, userid;
    string sdd = string.Empty, sqry1 = string.Empty, sqry2 = string.Empty, sqry3 = string.Empty;
    string sdd1 = string.Empty, sqry11 = string.Empty, sqry12 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        app_language();
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                Month();
                Org();
                Year();
                //grid();
                namapegavai();
                jenis_potongan();
                //DD_bulancaruman.Attributes.Add("Readonly", "Readonly");
                //txt_tahun.Attributes.Add("Readonly", "Readonly");
                userid = Session["New"].ToString();


            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    void app_language()
    {

        if (Session["New"] != null)
        {
            DataTable ste_set = new DataTable();
            ste_set = Con.Ora_Execute_table("select * from site_settings where ID IN ('1')");

            DataTable gt_lng = new DataTable();
            gt_lng = Con.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('499','448','500','501','502','496','497','1288','347','1104','15','36')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentUICulture;
            TextInfo txtinfo = culinfo.TextInfo;


            h1_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            bb1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            bb2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            h3_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            lbl1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            lbl2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            lbl3_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            lbl4_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            lbl5_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            lbl6_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            lbl7_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());

        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }

    protected void sel_orgbind(object sender, EventArgs e)
    {
        org_pern();
        if (ddl_reporttye.SelectedValue == "04")
        {
            grid();
        }
    }
    void org_pern()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select * from hr_organization_pern where op_org_id='" + DropDownList1.SelectedValue + "' and Status = 'A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_org_pen.DataSource = dt;
            dd_org_pen.DataTextField = "op_perg_name";
            dd_org_pen.DataValueField = "op_perg_code";
            dd_org_pen.DataBind();
            dd_org_pen.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void jenis_potongan()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select hr_poto_Code,hr_poto_desc from Ref_hr_potongan where Status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            sel_frmt.DataSource = dt;
            sel_frmt.DataTextField = "hr_poto_desc";
            sel_frmt.DataValueField = "hr_poto_Code";
            sel_frmt.DataBind();
            sel_frmt.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void sel_orgjaba(object sender, EventArgs e)
    {
        org_jaba();
        if (ddl_reporttye.SelectedValue == "04")
        {
            grid();
        }
    }
    void org_jaba()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select * from hr_organization_jaba where oj_org_id='" + DropDownList1.SelectedValue + "' and oj_perg_code='" + dd_org_pen.SelectedValue + "' and Status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_jabatan.DataSource = dt;
            dd_jabatan.DataTextField = "oj_jaba_desc";
            dd_jabatan.DataValueField = "oj_jaba_cd";
            dd_jabatan.DataBind();
            dd_jabatan.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void Month()
    {
        DateTimeFormatInfo info1 = DateTimeFormatInfo.GetInstance(null);
        int Month = DateTime.Now.Month - 4;
        for (int X = Month; X <= DateTime.Now.Month; X++)
        {
            DD_bulancaruman.Items.Add(new ListItem(X.ToString("#0"), X.ToString("#0")));
        }
        string abc = DateTime.Now.Month.ToString("#0");
        //string abc = DD_bulancaruman.SelectedValue;

        DataSet Ds = new DataSet();
        try
        {

            string com = "select hr_month_Code,hr_month_desc from Ref_hr_month ORDER BY hr_month_Code";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_bulancaruman.DataSource = dt;
            DD_bulancaruman.DataBind();
            DD_bulancaruman.DataTextField = "hr_month_desc";
            DD_bulancaruman.DataValueField = "hr_month_Code";
            DD_bulancaruman.DataBind();
            DD_bulancaruman.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
        DD_bulancaruman.SelectedValue = abc.PadLeft(2, '0');
    }



    void Org()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select org_gen_id,org_name From hr_organization ORDER BY org_name";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList1.DataSource = dt;
            DropDownList1.DataBind();
            DropDownList1.DataTextField = "org_name";
            DropDownList1.DataValueField = "org_gen_id";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void namapegavai()
    {
        DataSet Ds = new DataSet();
        try
        {

            //string com = "select s.stf_staff_no,stf_name from hr_post_his p inner join hr_staff_profile s on s.stf_staff_no=p.pos_spv_name1 or s.stf_staff_no=pos_spv_name2 inner join Login l on l.userid=s.stf_staff_no where level='5' group by stf_staff_no,stf_name order by stf_name";
            string com = "select stf_staff_no,stf_name from hr_staff_profile order by stf_name";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DDL_NAMAPEGAWAI.DataSource = dt;
            DDL_NAMAPEGAWAI.DataBind();
            DDL_NAMAPEGAWAI.DataTextField = "stf_name";
            DDL_NAMAPEGAWAI.DataValueField = "stf_staff_no";
            DDL_NAMAPEGAWAI.DataBind();
            DDL_NAMAPEGAWAI.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void Year()
    {

        DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);
        int year = DateTime.Now.Year - 5;
        for (int Y = year; Y <= DateTime.Now.Year; Y++)
        {
            txt_tahun.Items.Add(new ListItem(Y.ToString(), Y.ToString()));
        }
        txt_tahun.SelectedValue = DateTime.Now.Year.ToString();

    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_reporttye.SelectedValue == "02")
        {
            borangA.Visible = false;
            RB_JenisCaruman1.Checked = true;
            EABUTTON.Visible = false;
            shw_jp.Visible = false;
            shw_jpotongan.Visible = false;
            shw_EA.Visible = false;
        }
        else if (ddl_reporttye.SelectedValue == "04")
        {
            borangA.Visible = false;
            EABUTTON.Visible = true;
            shw_jp.Visible = false;
            shw_jpotongan.Visible = false;
            shw_EA.Visible = true;
        }
        else if (ddl_reporttye.SelectedValue == "06")
        {
            borangA.Visible = false;
            EABUTTON.Visible = true;
            shw_jp.Visible = true;
            shw_jpotongan.Visible = true;
            shw_EA.Visible = false;
        }
        else
        {
            borangA.Visible = false;
            EABUTTON.Visible = false;
            shw_jp.Visible = false;
            shw_jpotongan.Visible = false;
            shw_EA.Visible = false;
        }
    }

    //void dd_selqry()
    //{
    //    if (DropDownList1.SelectedValue != "" && txt_tahun.SelectedValue == "" && DD_bulancaruman.SelectedValue == "")
    //    {
    //        sqry1 = "inc_gen_id = '" + DropDownList1.SelectedValue + "'";
    //    }
    //    else if (DropDownList1.SelectedValue != "" && txt_tahun.SelectedValue != "" && DD_bulancaruman.SelectedValue == "")
    //    {
    //        sqry1 = "inc_gen_id = '" + DropDownList1.SelectedValue + "' and inc_year ='" + txt_tahun.SelectedValue + "'";
    //    }
    //    else if (DropDownList1.SelectedValue == "" && txt_tahun.SelectedValue != "" && DD_bulancaruman.SelectedValue == "")
    //    {
    //        sqry1 = "inc_year ='" + txt_tahun.SelectedValue + "'";
    //        sqry2 = "where stf_staff_no='" + DDL_NAMAPEGAWAI.SelectedValue + "'"; 

    //    }
    //    else if (DropDownList1.SelectedValue != "" && txt_tahun.SelectedValue != "" && DD_bulancaruman.SelectedValue != "")
    //    {
    //        sqry1 = "inc_gen_id = '" + DropDownList1.SelectedValue + "' and inc_year ='" + txt_tahun.SelectedValue + "' ";
    //        sqry2 = "where stf_staff_no='" + DDL_NAMAPEGAWAI.SelectedValue + "'"; 
    //    }
    //    else
    //    {
    //        sqry1 = "inc_gen_id =''";
    //        sqry2 = "where stf_staff_no=''"; 
    //    }
    //}

    void dd_selqry()
    {
        if (DDL_NAMAPEGAWAI.SelectedValue == "")
        {
            if (ddl_reporttye.SelectedValue != "06")
            {
                if (DropDownList1.SelectedValue != "" && dd_org_pen.SelectedValue != "" && dd_jabatan.SelectedValue != "")
                {
                    sqry1 = " where inc_app_sts='A' and inc_gen_id = '" + DropDownList1.SelectedValue + "' and aa1.stf_cur_sub_org='" + dd_org_pen.SelectedValue + "' and aa1.stf_curr_dept_cd='" + dd_jabatan.SelectedValue + "' and inc_year ='" + txt_tahun.SelectedValue + "'";
                    sqry2 = "where sp.str_curr_org_cd='" + DropDownList1.SelectedValue + "' and sp.stf_cur_sub_org='" + dd_org_pen.SelectedValue + "' and sp.stf_curr_dept_cd='" + dd_jabatan.SelectedValue + "'";

                }
                else if (DropDownList1.SelectedValue != "" && dd_org_pen.SelectedValue == "" && dd_jabatan.SelectedValue == "")
                {
                    sqry1 = "where inc_app_sts='A' and inc_gen_id = '" + DropDownList1.SelectedValue + "' and inc_year ='" + txt_tahun.SelectedValue + "'";
                    sqry2 = "where sp.str_curr_org_cd='" + DropDownList1.SelectedValue + "'";
                }
                else if (DropDownList1.SelectedValue != "" && dd_org_pen.SelectedValue != "" && dd_jabatan.SelectedValue == "")
                {
                    sqry1 = "where inc_app_sts='A' and inc_gen_id = '" + DropDownList1.SelectedValue + "' and aa1.stf_cur_sub_org='" + dd_org_pen.SelectedValue + "' and inc_year ='" + txt_tahun.SelectedValue + "'";
                    sqry2 = "where sp.str_curr_org_cd='" + DropDownList1.SelectedValue + "' and sp.stf_cur_sub_org='" + dd_org_pen.SelectedValue + "' ";
                }
            }
            else
            {
                if (DropDownList1.SelectedValue != "" && dd_org_pen.SelectedValue != "" && dd_jabatan.SelectedValue != "" && sel_frmt.SelectedValue != "")
                {
                    sqry1 = " where ('"+ txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(ded_start_dt,'yyyy-MM') And FORMAT(ded_end_dt,'yyyy-MM')) and ded_deduct_type_cd='" + sel_frmt.SelectedValue + "' and str_curr_org_cd = '" + DropDownList1.SelectedValue + "' and s2.stf_cur_sub_org='" + dd_org_pen.SelectedValue + "' and s2.stf_curr_dept_cd='" + dd_jabatan.SelectedValue + "'";
                }
                else if (DropDownList1.SelectedValue != "" && dd_org_pen.SelectedValue == "" && dd_jabatan.SelectedValue == "" && sel_frmt.SelectedValue == "")
                {
                    sqry1 = " where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(ded_start_dt,'yyyy-MM') And FORMAT(ded_end_dt,'yyyy-MM')) and str_curr_org_cd = '" + DropDownList1.SelectedValue + "' ";

                }
                else if (DropDownList1.SelectedValue != "" && dd_org_pen.SelectedValue != "" && dd_jabatan.SelectedValue == "" && sel_frmt.SelectedValue == "")
                {
                    sqry1 = " where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(ded_start_dt,'yyyy-MM') And FORMAT(ded_end_dt,'yyyy-MM')) and str_curr_org_cd = '" + DropDownList1.SelectedValue + "' and s2.stf_cur_sub_org='" + dd_org_pen.SelectedValue + "'";

                }
                else if (DropDownList1.SelectedValue == "" && dd_org_pen.SelectedValue == "" && dd_jabatan.SelectedValue == "" && sel_frmt.SelectedValue != "")
                {
                    sqry1 = " where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(ded_start_dt,'yyyy-MM') And FORMAT(ded_end_dt,'yyyy-MM')) and ded_deduct_type_cd='" + sel_frmt.SelectedValue + "'";
                }
                else if (DropDownList1.SelectedValue != "" && dd_org_pen.SelectedValue != "" && dd_jabatan.SelectedValue != "" && sel_frmt.SelectedValue == "")
                {
                    sqry1 = " where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(ded_start_dt,'yyyy-MM') And FORMAT(ded_end_dt,'yyyy-MM')) and str_curr_org_cd = '" + DropDownList1.SelectedValue + "' and s2.stf_cur_sub_org='" + dd_org_pen.SelectedValue + "' and s2.stf_curr_dept_cd='" + dd_jabatan.SelectedValue + "'";

                }
                else
                {
                    sqry1 = " where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(ded_start_dt,'yyyy-MM') And FORMAT(ded_end_dt,'yyyy-MM')) ";

                }
            }
        }
        else
        {
            if (ddl_reporttye.SelectedValue != "06")
            {
                sqry1 = "where inc_app_sts='A' and inc_staff_no='" + DDL_NAMAPEGAWAI.SelectedValue + "' and inc_year ='" + txt_tahun.SelectedValue + "'";
                sqry2 = "where sp.stf_staff_no='" + DDL_NAMAPEGAWAI.SelectedValue + "' ";
            }
            else
            {
                if (sel_frmt.SelectedValue == "")
                {
                    sqry1 = "where ded_staff_no='" + DDL_NAMAPEGAWAI.SelectedValue + "' and ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(ded_start_dt,'yyyy-MM') And FORMAT(ded_end_dt,'yyyy-MM'))";
                }
                else
                {
                    sqry1 = "where ded_staff_no='" + DDL_NAMAPEGAWAI.SelectedValue + "' and ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(ded_start_dt,'yyyy-MM') And FORMAT(ded_end_dt,'yyyy-MM')) and ded_deduct_type_cd='" + sel_frmt.SelectedValue + "'";
                }
            }
        }

    }

    void grid()
    {
        dd_selqry();

        //SqlCommand cmd2 = new SqlCommand("select UPPER(sp.stf_name) as s1,sp.stf_staff_no as s2,sp.stf_icno as s3,ISNULL(sp.stf_epf_no,'') s4,inc_salary_amt as s5,inc_pcb_amt as s6,(ISNULL(inc_cp38_amt,'') + ISNULL(inc_cp38_amt2,'')) as s7  from hr_income left join hr_staff_profile as sp on sp.stf_staff_no=inc_staff_no where " + sqry1 + "", con);
        SqlCommand cmd2 = new SqlCommand("select upper(sp.stf_name) as s1,sp.stf_staff_no as s2,sp.stf_icno as s3, ISNULL(sp.stf_epf_no,'') as s4,a.as5 as s5,a.as6 as s6,a.as7 as s7 from (select inc_staff_no, sum(inc_salary_amt) as as5,sum(inc_pcb_amt) as as6,(sum (ISNULL(inc_cp38_amt,'')) + sum(ISNULL(inc_cp38_amt2,''))) as as7  from hr_income left join hr_staff_profile as aa1 on aa1.stf_staff_no=inc_staff_no " + sqry1 + " group by inc_staff_no) as a left join hr_staff_profile as sp on sp.stf_staff_no=a.inc_staff_no " + sqry2 + " ", con);
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
        }
        else
        {
            GridView1.DataSource = ds2;
            GridView1.DataBind();
        }
    }

    void grid1()
    {
        dd_selqry();

        //SqlCommand cmd2 = new SqlCommand("select UPPER(sp.stf_name) as s1,sp.stf_staff_no as s2,sp.stf_icno as s3,ISNULL(sp.stf_epf_no,'') s4,inc_salary_amt as s5,inc_pcb_amt as s6,(ISNULL(inc_cp38_amt,'') + ISNULL(inc_cp38_amt2,'')) as s7  from hr_income left join hr_staff_profile as sp on sp.stf_staff_no=inc_staff_no where " + sqry1 + "", con);
        SqlCommand cmd2 = new SqlCommand("select s1.ded_staff_no v1,s2.stf_name v2,s2.stf_icno v3,s1.ded_ref_no v4,s1.ded_deduct_amt v5,s4.hr_poto_desc v6 from hr_deduction s1 left join hr_staff_profile s2 on s2.stf_staff_no=s1.ded_staff_no inner join hr_post_his s3 on s3.pos_staff_no=s2.stf_staff_no and s3.pos_end_dt >='" + DateTime.Now.ToString("yyyy-MM-dd")+ "' inner join Ref_hr_potongan s4 on s4.hr_poto_Code=s1.ded_deduct_type_cd and s4.Status='A' " + sqry1 + "", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            GridView2.DataSource = ds2;
            GridView2.DataBind();
            int columncount = GridView2.Rows[0].Cells.Count;
            GridView2.Rows[0].Cells.Clear();
            GridView2.Rows[0].Cells.Add(new TableCell());
            GridView2.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView2.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
        }
        else
        {
            GridView2.DataSource = ds2;
            GridView2.DataBind();
        }
    }
    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        grid();

    }
    protected void gvSelected_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        grid1();

    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedValue != "" || txt_tahun.SelectedValue != "" || DDL_NAMAPEGAWAI.SelectedValue != "")
        {
            if (ddl_reporttye.SelectedValue == "04")
            {
                grid();
            }
            else if (ddl_reporttye.SelectedValue == "06")
            {
                grid1();
            }
        }
    }

    void sql_qry()
    {
        if (DropDownList1.SelectedValue != "" && dd_org_pen.SelectedValue != "" && dd_jabatan.SelectedValue != "" && DDL_NAMAPEGAWAI.SelectedValue != "")
        {
            sqry11 = "inc_app_sts='A' and inc_month='" + DD_bulancaruman.SelectedValue + "' and inc_year='" + txt_tahun.SelectedValue + "' and '" + DDL_NAMAPEGAWAI.SelectedValue + "' in (pos_spv_name1,pos_spv_name2) and pos_end_dt='9999-12-31' and inc_gen_id='" + DropDownList1.SelectedValue + "' and inc_sub_org_id='" + dd_org_pen.SelectedValue + "' and stf_curr_dept_cd='" + dd_jabatan.SelectedValue + "' ";
        }
        else if (DropDownList1.SelectedValue != "" && dd_org_pen.SelectedValue == "" && dd_jabatan.SelectedValue == "" && DDL_NAMAPEGAWAI.SelectedValue == "")
        {
            sqry11 = "inc_app_sts='A' and inc_month='" + DD_bulancaruman.SelectedValue + "' and inc_year='" + txt_tahun.SelectedValue + "' and inc_gen_id='" + DropDownList1.SelectedValue + "'";
        }
        else if (DropDownList1.SelectedValue != "" && dd_org_pen.SelectedValue != "" && dd_jabatan.SelectedValue == "" && DDL_NAMAPEGAWAI.SelectedValue == "")
        {
            sqry11 = "inc_app_sts='A' and inc_month='" + DD_bulancaruman.SelectedValue + "' and inc_year='" + txt_tahun.SelectedValue + "' and inc_gen_id='" + DropDownList1.SelectedValue + "' and inc_sub_org_id='" + dd_org_pen.SelectedValue + "'";
        }
        else if (DropDownList1.SelectedValue != "" && dd_org_pen.SelectedValue != "" && dd_jabatan.SelectedValue != "" && DDL_NAMAPEGAWAI.SelectedValue == "")
        {
            sqry11 = "inc_app_sts='A' and inc_month='" + DD_bulancaruman.SelectedValue + "' and inc_year='" + txt_tahun.SelectedValue + "' and inc_gen_id='" + DropDownList1.SelectedValue + "' and inc_sub_org_id='" + dd_org_pen.SelectedValue + "' and stf_curr_dept_cd='" + dd_jabatan.SelectedValue + "' ";
        }
        else if (DropDownList1.SelectedValue != "" && dd_org_pen.SelectedValue == "" && dd_jabatan.SelectedValue == "" && DDL_NAMAPEGAWAI.SelectedValue != "")
        {
            sqry11 = "inc_app_sts='A' and inc_month='" + DD_bulancaruman.SelectedValue + "' and inc_year='" + txt_tahun.SelectedValue + "' and '" + DDL_NAMAPEGAWAI.SelectedValue + "' in (pos_spv_name1,pos_spv_name2) and pos_end_dt='9999-12-31' and inc_gen_id='" + DropDownList1.SelectedValue + "'";
        }
        else if (DropDownList1.SelectedValue == "" && dd_org_pen.SelectedValue == "" && dd_jabatan.SelectedValue == "" && DDL_NAMAPEGAWAI.SelectedValue != "")
        {
            sqry11 = "inc_app_sts='A' and inc_month='" + DD_bulancaruman.SelectedValue + "' and inc_year='" + txt_tahun.SelectedValue + "' and '" + DDL_NAMAPEGAWAI.SelectedValue + "' in (pos_spv_name1,pos_spv_name2) and pos_end_dt='9999-12-31'";
        }
        else if (DropDownList1.SelectedValue != "" && dd_org_pen.SelectedValue != "" && dd_jabatan.SelectedValue == "" && DDL_NAMAPEGAWAI.SelectedValue != "")
        {
            sqry11 = "inc_app_sts='A' and inc_month='" + DD_bulancaruman.SelectedValue + "' and inc_year='" + txt_tahun.SelectedValue + "' and '" + DDL_NAMAPEGAWAI.SelectedValue + "' in (pos_spv_name1,pos_spv_name2) and pos_end_dt='9999-12-31' and inc_gen_id='" + DropDownList1.SelectedValue + "' and inc_sub_org_id='" + dd_org_pen.SelectedValue + "'";
        }
        else
        {
            sqry11 = "inc_app_sts='A' and inc_month='" + DD_bulancaruman.SelectedValue + "' and inc_year='" + txt_tahun.SelectedValue + "' ";
        }

    }

    protected void clk_rset(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        try
        {
            string sel_qry,act_dt;
            sql_qry();
            if (ddl_reporttye.SelectedValue == "01")
            {
                act_dt = txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue;
                sel_qry = "select * from (select stf_email,stf_phone_m,hr_negeri_desc ,stf_staff_no,stf_icno,UPPER(stf_name) as stf_name,stf_curr_post_cd,org_id,stf_epf_no,org_name,org_address,org_epf_no,epf_staff_no,epf_amt,inc_staff_no,inc_month,inc_year,inc_org_id,inc_salary_amt,inc_kwsp_amt,inc_emp_kwsp_amt from hr_income I inner join hr_post_his P on P.pos_staff_no=I.inc_staff_no inner join hr_staff_profile as HR on HR.stf_staff_no=I.inc_staff_no inner join hr_organization o on o.org_id=i.inc_org_id and o.org_gen_id = HR.str_curr_org_cd left join Ref_hr_negeri as n on n.hr_negeri_Code=HR.stf_curr_post_cd left join hr_kwsp as k on k.epf_staff_no=i.inc_staff_no and epf_end_dt='9999-12-31' where " + sqry11 + " and (inc_kwsp_amt != '0.00' or inc_emp_kwsp_amt != '0.00') group by stf_epf_no,stf_staff_no,stf_icno,stf_name,stf_email,stf_phone_m,hr_negeri_desc,stf_curr_post_cd,org_id, org_name,org_address,org_epf_no,epf_staff_no,epf_amt,inc_staff_no,inc_month,inc_year,inc_org_id,inc_salary_amt,inc_kwsp_amt,inc_emp_kwsp_amt) as a "
                    + "OUTER APPLY  (select (a.inc_salary_amt + sum(amt1)) tot_amt from ( "
                + "select (sum(falw_amt) - sum(falw_amt1)) amt1 from (select ISNULL(sum(s1.fxa_allowance_amt),'') as falw_amt,'0.00' falw_amt1 from hr_fixed_allowance s1 Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=s1.fxa_allowance_type_cd and ss1.elau_kwsp='S' where ('" + act_dt + "' between FORMAT(fxa_eff_dt,'yyyy-MM') And FORMAT(fxa_end_dt,'yyyy-MM')) and fxa_staff_no=a.stf_staff_no union all select ISNULL(sum(s1.fxa_promo_amt),'') as falw_amt,ISNULL(sum(s1.fxa_allowance_amt),'') as falw_amt1 from hr_fixed_allowance s1 Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=s1.fxa_allowance_type_cd and ss1.elau_kwsp='S' where ('" + act_dt + "' between FORMAT(fxa_pst_dt,'yyyy-MM') And FORMAT(fxa_ped_dt,'yyyy-MM')) and fxa_staff_no=a.stf_staff_no)as a  union all "
                + "select (sum(xalw_amt) - sum(xalw_amt1)) amt1 from(select ISNULL(sum(s1.xta_allowance_amt),'') as xalw_amt,'0.00' xalw_amt1 from hr_extra_allowance s1 Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=s1.xta_allowance_type_cd and ss1.elau_kwsp='S' where s1.xta_staff_no=a.stf_staff_no and ('" + act_dt + "' between FORMAT(xta_eff_dt,'yyyy-MM') And FORMAT(xta_end_dt,'yyyy-MM')) union all select ISNULL(sum(s1.xta_promo_amt),'') as xalw_amt,ISNULL(sum(s1.xta_allowance_amt),'') as xalw_amt1 from hr_extra_allowance s1 Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=s1.xta_allowance_type_cd and ss1.elau_kwsp='S' where s1.xta_staff_no=a.stf_staff_no and ('" + act_dt + "' between FORMAT(xta_pst_dt,'yyyy-MM') And FORMAT(xta_ped_dt,'yyyy-MM'))) as a union all "
                + "select ISNULL(sum(ISNULL(otl_ot_amt,'0.00')),'0.00') as amt from hr_ot left join Ref_hr_type_klm s1 on s1.typeklm_cd=otl_ot_type_cd  where s1.elau_kwsp ='S' and otl_staff_no=a.stf_staff_no and FORMAT(otl_work_dt,'yyyy-MM') ='" + act_dt + "' union all "
                + "select ISNULL(sum(ded_deduct_amt),'') as s1 from hr_deduction left join Ref_hr_potongan s1 on s1.hr_poto_Code=ded_deduct_type_cd and Status='A' where s1.elau_kwsp='S' and ded_staff_no=a.stf_staff_no and ('" + act_dt + "' between FORMAT(ded_start_dt,'yyyy-MM') And FORMAT(ded_end_dt,'yyyy-MM'))  union all "
                + "select ISNULL(sum(ISNULL(tun_amt,'0.00')),'0.00') as amt from hr_tunggakan left join Ref_hr_tunggakan s1 on s1.hr_tung_Code=tun_type_cd  where s1.elau_kwsp ='S' and tun_staff_no=a.stf_staff_no and  tun_year='" + txt_tahun.SelectedValue + "' and tun_month='" + DD_bulancaruman.SelectedValue + "')  as a1) as b ";
                //DataSet dsCustomers = GetData("select stf_email,stf_phone_m,hr_negeri_desc ,stf_staff_no,stf_icno,UPPER(stf_name) as stf_name,stf_curr_post_cd,org_id,stf_epf_no,org_name,org_address,org_epf_no,epf_staff_no,epf_amt,inc_staff_no,inc_month,inc_year,inc_org_id,inc_salary_amt,inc_kwsp_amt,inc_emp_kwsp_amt from hr_income I inner join hr_post_his P on P.pos_staff_no=I.inc_staff_no inner join hr_staff_profile as HR on HR.stf_staff_no=I.inc_staff_no inner join hr_organization o on o.org_id=i.inc_org_id and o.org_gen_id = HR.str_curr_org_cd left join Ref_hr_negeri as n on n.hr_negeri_Code=HR.stf_curr_post_cd left join hr_kwsp as k on k.epf_staff_no=i.inc_staff_no and epf_end_dt='9999-12-31' where " + sqry11 + " and (inc_kwsp_amt != '0.00' or inc_emp_kwsp_amt != '0.00') group by stf_epf_no,stf_staff_no,stf_icno,stf_name,stf_email,stf_phone_m,hr_negeri_desc,stf_curr_post_cd,org_id, org_name,org_address,org_epf_no,epf_staff_no,epf_amt,inc_staff_no,inc_month,inc_year,inc_org_id,inc_salary_amt,inc_kwsp_amt,inc_emp_kwsp_amt");
                DataSet dsCustomers = GetData(sel_qry);
                dt = dsCustomers.Tables[0];

                if (dt.Rows.Count % 20 != 0)
                {
                    int addCount = 20 - dt.Rows.Count % 20;
                    for (int i = 0; i < addCount; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = "";
                        dt.Rows.Add(dr);
                    }

                }
                string vv1 = string.Empty, vv2 = string.Empty, vv3 = string.Empty, vv4 = string.Empty, vv5 = string.Empty;
                DataTable stf_info = new DataTable();
                stf_info = Dblog.Ora_Execute_table("select stf_name,stf_icno,stf_phone_m,stf_email,s1.hr_jaw_desc from hr_staff_profile left join Ref_hr_Jawatan s1 on s1.hr_jaw_Code=stf_curr_post_cd where stf_staff_no='" + Session["new"].ToString() +"'");
                if(stf_info.Rows.Count != 0)
                {
                    vv1 = stf_info.Rows[0]["stf_name"].ToString();
                    vv2 = stf_info.Rows[0]["stf_icno"].ToString();
                    vv3 = stf_info.Rows[0]["stf_phone_m"].ToString();
                    vv4 = stf_info.Rows[0]["stf_email"].ToString();
                    vv5 = stf_info.Rows[0]["hr_jaw_desc"].ToString();
                }
                else
                {
                    DataTable stf_info1 = new DataTable();
                    stf_info1 = Dblog.Ora_Execute_table("select Kk_username as stf_name,KK_userid as stf_icno,'' stf_phone_m,KK_email as stf_email,ISNULL(KK_Jawatan,'') as hr_jaw_desc from KK_User_Login where KK_userid='" + Session["new"].ToString() + "' and Status='A'");
                    if (stf_info1.Rows.Count != 0)
                    {
                        vv1 = stf_info1.Rows[0]["stf_name"].ToString();
                        vv2 = stf_info1.Rows[0]["stf_icno"].ToString();
                        vv3 = stf_info1.Rows[0]["stf_phone_m"].ToString();
                        vv4 = stf_info1.Rows[0]["stf_email"].ToString();
                        vv5 = stf_info1.Rows[0]["hr_jaw_desc"].ToString();
                    }
                }



                ReportViewer1.Reset();
                List<DataRow> listResult = dt.AsEnumerable().ToList();
                listResult.Count();
                int countRow = 0;
                countRow = listResult.Count();

                if (countRow != 0)
                {
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rds = new ReportDataSource("kw1", dt);
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                    //Path
                    ReportViewer1.LocalReport.ReportPath = "SUMBER_MANUSIA/HR_KWSP1.rdlc";
                    //Parameters
                    String strDate = "01/" + DD_bulancaruman.SelectedValue + "/" + txt_tahun.SelectedValue + "";
                    DateTime dateTime = DateTime.ParseExact(strDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DataTable dd1 = new DataTable();
                    dd1 = Dblog.Ora_Execute_table("select hr_month_Code,hr_month_desc from Ref_hr_month where hr_month_Code='"+ dateTime.AddMonths(+1).ToString("MM") + "'");

                    string nxt_mnth = dd1.Rows[0]["hr_month_desc"].ToString().ToUpper();

                    ReportParameter[] rptParams = new ReportParameter[]
                    {
                        new ReportParameter("Cdate1", nxt_mnth),
                        new ReportParameter("vv1", vv1),
                        new ReportParameter("vv2", vv2),
                        new ReportParameter("vv3", vv3),
                        new ReportParameter("vv4", vv4),
                        new ReportParameter("vv5", vv5),

                    };


                    ReportViewer1.LocalReport.SetParameters(rptParams);
                    //Refresh
                    ReportViewer1.LocalReport.Refresh();

                    Warning[] warnings;

                    string[] streamids;

                    string mimeType;

                    string encoding;

                    string extension;
                    string filename;

                  

                        filename = string.Format("{0}.{1}", "Laporan_Hartanah_" + DateTime.Now.ToString("ddMMyyyy") + "", "pdf");
                        byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);

                        Response.Buffer = true;
                        Response.Clear();
                        Response.ContentType = mimeType;
                        string extfile = DateTime.Now.ToString("dd_MM_yyyy.");
                        Response.AddHeader("content-disposition", "attachment; filename=KWSP_" + extfile + extension);

                        Response.BinaryWrite(bytes);
                        Response.Flush();
                        Response.End();
                   


                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else if (ddl_reporttye.SelectedValue == "02")
            {

                if (RB_JenisCaruman1.Checked == true)
                {

                    DataSet dsCustomers = GetData("select stf_staff_no,stf_icno,UPPER(stf_name) as stf_name,hr_negeri_desc,org_id,org_name,org_address,inc_staff_no,inc_month,inc_year,inc_org_id,org_socso_no,inc_cumm_deduct_amt,(inc_emp_perkeso_amt + inc_perkeso_amt) inc_perkeso_amt,aname,anumber,case FORMAT(stf_service_end_dt,'dd/MM/yyyy', 'en-us') when '31/12/9999' then FORMAT(stf_service_start_dt,'dd/MM/yyyy', 'en-us') else FORMAT(stf_service_end_dt,'dd/MM/yyyy', 'en-us') end as edate From hr_income i left join hr_kwsp as k on k.epf_staff_no=i.inc_staff_no and epf_end_dt='9999-12-31'  full outer join (select pos_staff_no,pos_spv_name1,pos_spv_name2 From hr_post_his where '" + DDL_NAMAPEGAWAI.SelectedValue + "' in (pos_spv_name1,pos_spv_name2) and pos_end_dt='9999-12-31') hp on hp.pos_staff_no=i.inc_staff_no left join hr_staff_profile as sf on sf.stf_staff_no=i.inc_staff_no left join Ref_hr_negeri as n on n.hr_negeri_Code=sf.stf_curr_post_cd left join hr_organization o on o.org_id=i.inc_org_id and sf.str_curr_org_cd=o.org_gen_id full outer join (select stf_staff_no as sfnumber,stf_name as aname,stf_phone_m as anumber from hr_post_his p inner join hr_staff_profile s on s.stf_staff_no=p.pos_spv_name1 or s.stf_staff_no=pos_spv_name2 where stf_staff_no='" + DDL_NAMAPEGAWAI.SelectedValue + "') as a1 on a1.sfnumber=sf.stf_staff_no where " + sqry11 + " and inc_perkeso_amt != '0.00' group by stf_staff_no,stf_icno,stf_name,hr_negeri_desc,org_id,org_name,org_address,inc_staff_no,inc_month,inc_year,inc_org_id,org_socso_no,inc_cumm_deduct_amt,inc_perkeso_amt,inc_emp_perkeso_amt,stf_service_end_dt,stf_service_start_dt,aname,anumber");

                    dt = dsCustomers.Tables[0];

                    if (dt.Rows.Count % 20 != 0)
                    {
                        int addCount = 20 - dt.Rows.Count % 20;
                        for (int i = 0; i < addCount; i++)
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = "";
                            dt.Rows.Add(dr);
                        }

                    }
                    //DataSet dsCustomers = GetData("select stf_email,stf_phone_m,hr_negeri_desc ,stf_staff_no,stf_icno,UPPER(stf_name) as stf_name,stf_curr_post_cd,org_id,stf_epf_no,org_name,org_address,org_epf_no,epf_staff_no,epf_amt,inc_staff_no,inc_month,inc_year,inc_org_id,inc_salary_amt,inc_kwsp_amt,inc_emp_kwsp_amt From hr_staff_profile s left join hr_organization o on o.org_gen_id=s.str_curr_org_cd left join hr_kwsp as k on k.epf_staff_no=s.stf_staff_no left join hr_income as i on i.inc_staff_no=s.stf_staff_no  left join Ref_hr_negeri as n on n.hr_negeri_Code=s.stf_curr_post_cd where inc_year=YEAR(getdate()) and inc_month='" + dd_kat.SelectedValue + "' and inc_org_id='" + DropDownList1.SelectedValue + "' and stf_staff_no='" + DropDownList2.SelectedValue + "' group by stf_email,stf_phone_m,hr_negeri_desc,stf_epf_no,stf_staff_no,stf_icno,stf_name,stf_curr_post_cd,org_id,org_name,org_address,org_epf_no,epf_staff_no,epf_amt,inc_staff_no,inc_month,inc_year,inc_org_id,inc_salary_amt,inc_kwsp_amt,inc_emp_kwsp_amt");
                    //dt = dsCustomers.Tables[0];


                    ReportViewer1.Reset();
                    List<DataRow> listResult = dt.AsEnumerable().ToList();
                    listResult.Count();
                    int countRow = 0;
                    countRow = listResult.Count();

                    if (countRow != 0)
                    {
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportDataSource rds = new ReportDataSource("PERKESO_BORANG_8A", dt);
                        ReportViewer1.LocalReport.DataSources.Add(rds);
                        //Path
                        ReportViewer1.LocalReport.ReportPath = "SUMBER_MANUSIA/HR_PERKESO_BORANG_8A.rdlc";
                        //Parameters

                        ReportParameter[] rptParams = new ReportParameter[0];
                        {
                            //new ReportParameter("param1", dd_kat.SelectedValue);
                            //new ReportParameter("toDate",ToDate .Text )
                            //new ReportParameter("fromDate",datedari  ),
                            //new ReportParameter("toDate",datehingga ),
                            //new ReportParameter("caw",branch ),     
                            // new ReportParameter("Cdate", DateTime.Now.AddMonths(1).ToString("MMMM"));   
                            //new ReportParameter("Date",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  )
                        };


                        ReportViewer1.LocalReport.SetParameters(rptParams);
                        //Refresh
                        ReportViewer1.LocalReport.Refresh();

                        Warning[] warnings;

                        string[] streamids;

                        string mimeType;

                        string encoding;

                        string extension;
                        string filename;

                        filename = string.Format("{0}.{1}", "Laporan_Hartanah_" + DateTime.Now.ToString("ddMMyyyy") + "", "pdf");
                        byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);


                        //byte[] bytes = ReportViewer1.LocalReport.Render("PDF", devinfo, out mimeType, out encoding, out extension, out streamids, out warnings);


                        Response.Buffer = true;
                        Response.Clear();
                        Response.ContentType = mimeType;
                        string extfile = DateTime.Now.ToString("dd_MM_yyyy.");
                        Response.AddHeader("content-disposition", "attachment; filename=BORANG_PERKESO_8A_" + extfile + extension);
                        Response.BinaryWrite(bytes);
                        Response.Flush();
                        Response.End();


                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                else if (RB_JenisCaruman2.Checked == true)
                {


                    DataSet dsCustomers = GetData("select stf_staff_no,stf_icno,UPPER(stf_name) as stf_name,hr_negeri_desc,org_id,org_name,org_address,inc_staff_no,inc_month,inc_year,inc_org_id,org_socso_no,inc_cumm_deduct_amt,inc_perkeso_amt,aname,anumber,case FORMAT(stf_service_end_dt,'dd/MM/yyyy', 'en-us') when '31/12/9999' then FORMAT(stf_service_start_dt,'dd/MM/yyyy', 'en-us') else FORMAT(stf_service_end_dt,'dd/MM/yyyy', 'en-us') end as edate From hr_income i left join hr_kwsp as k on k.epf_staff_no=i.inc_staff_no and epf_end_dt='9999-12-31'  full outer join (select pos_staff_no,pos_spv_name1,pos_spv_name2 From hr_post_his where '" + DDL_NAMAPEGAWAI.SelectedValue + "' in (pos_spv_name1,pos_spv_name2) and pos_end_dt='9999-12-31') hp on hp.pos_staff_no=i.inc_staff_no left join hr_staff_profile as sf on sf.stf_staff_no=i.inc_staff_no left join Ref_hr_negeri as n on n.hr_negeri_Code=sf.stf_curr_post_cd left join hr_organization o on o.org_id=i.inc_org_id and sf.str_curr_org_cd=o.org_gen_id full outer join (select stf_staff_no as sfnumber,stf_name as aname,stf_phone_m as anumber from hr_post_his p inner join hr_staff_profile s on s.stf_staff_no=p.pos_spv_name1 or s.stf_staff_no=pos_spv_name2 where stf_staff_no='" + DDL_NAMAPEGAWAI.SelectedValue + "') as a1 on a1.sfnumber=sf.stf_staff_no where " + sqry11 + " and inc_perkeso_amt != '0.00' group by stf_staff_no,stf_icno,stf_name,hr_negeri_desc,org_id,org_name,org_address,inc_staff_no,inc_month,inc_year,inc_org_id,org_socso_no,inc_cumm_deduct_amt,inc_perkeso_amt,stf_service_end_dt,stf_service_start_dt,aname,anumber");

                    dt = dsCustomers.Tables[0];

                    if (dt.Rows.Count % 20 != 0)
                    {
                        int addCount = 20 - dt.Rows.Count % 20;
                        for (int i = 0; i < addCount; i++)
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = "";
                            dt.Rows.Add(dr);
                        }

                    }



                    ReportViewer1.Reset();
                    List<DataRow> listResult = dt.AsEnumerable().ToList();
                    listResult.Count();
                    int countRow = 0;
                    countRow = listResult.Count();

                    if (countRow != 0)
                    {
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportDataSource rds = new ReportDataSource("PERKESO_BORANG_8A", dt);
                        ReportViewer1.LocalReport.DataSources.Add(rds);
                        //Path
                        ReportViewer1.LocalReport.ReportPath = "SUMBER_MANUSIA/HR_PERKESO_BORANG_8B.rdlc";
                        //Parameters

                        ReportParameter[] rptParams = new ReportParameter[0];
                        {
                            //new ReportParameter("param1", dd_kat.SelectedValue);
                            //new ReportParameter("toDate",ToDate .Text )
                            //new ReportParameter("fromDate",datedari  ),
                            //new ReportParameter("toDate",datehingga ),
                            //new ReportParameter("caw",branch ),     
                            //new ReportParameter("Cdate",DateTime.Now.ToString("dd/MM/yyyy") ),     
                            //new ReportParameter("Date",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  )
                        };


                        ReportViewer1.LocalReport.SetParameters(rptParams);
                        //Refresh
                        ReportViewer1.LocalReport.Refresh();

                        Warning[] warnings;

                        string[] streamids;

                        string mimeType;

                        string encoding;

                        string extension;
                        string filename;

                        filename = string.Format("{0}.{1}", "Laporan_Hartanah_" + DateTime.Now.ToString("ddMMyyyy") + "", "pdf");
                        byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);


                        //byte[] bytes = ReportViewer1.LocalReport.Render("PDF", devinfo, out mimeType, out encoding, out extension, out streamids, out warnings);


                        Response.Buffer = true;
                        Response.Clear();
                        Response.ContentType = mimeType;
                        string extfile = DateTime.Now.ToString("dd_MM_yyyy.");
                        Response.AddHeader("content-disposition", "attachment; filename=BORANG_PCB_8B_" + extfile + extension);
                        Response.BinaryWrite(bytes);
                        Response.Flush();
                        Response.End();


                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Please select the readio button',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else if (ddl_reporttye.SelectedValue == "03")
            {

                DataSet dsCustomers = GetData("select stf_tax_no,stf_icno,stf_staff_no,stf_nationality_cd,UPPER(stf_name) as stf_name,org_id,org_name,org_address,org_income_tax_no,inc_month,inc_year,inc_cumm_deduct_amt,inc_perkeso_amt,case FORMAT(stf_service_end_dt,'dd/MM/yyyy', 'en-us') when '31/12/9999' then FORMAT(stf_service_start_dt,'dd/MM/yyyy', 'en-us') else FORMAT(stf_service_end_dt,'dd/MM/yyyy', 'en-us') end as edate,inc_pcb_amt,inc_cp38_amt From hr_income i left join hr_kwsp as k on k.epf_staff_no=i.inc_staff_no and epf_end_dt='9999-12-31'  full outer join (select pos_staff_no,pos_spv_name1,pos_spv_name2 From hr_post_his where '" + DDL_NAMAPEGAWAI.SelectedValue + "' in (pos_spv_name1,pos_spv_name2) and pos_end_dt='9999-12-31') hp on hp.pos_staff_no=i.inc_staff_no left join hr_staff_profile as sf on sf.stf_staff_no=i.inc_staff_no left join Ref_hr_negeri as n on n.hr_negeri_Code=sf.stf_curr_post_cd left join hr_organization o on o.org_id=i.inc_org_id and sf.str_curr_org_cd=o.org_gen_id  where " + sqry11 + " and (inc_pcb_amt != '0.00' or inc_cp38_amt != '0.00') group by stf_tax_no,stf_icno,stf_staff_no,stf_nationality_cd,stf_name,org_id,org_name,org_address,org_income_tax_no,inc_month,inc_year,inc_cumm_deduct_amt,inc_perkeso_amt,stf_service_end_dt,stf_service_start_dt,inc_pcb_amt,inc_cp38_amt");

                dt = dsCustomers.Tables[0];

                if (dt.Rows.Count % 24 != 0)
                {
                    int addCount = 24 - dt.Rows.Count % 24;
                    for (int i = 0; i < addCount; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = "";
                        dt.Rows.Add(dr);
                    }

                }



                ReportViewer1.Reset();
                List<DataRow> listResult = dt.AsEnumerable().ToList();
                listResult.Count();
                int countRow = 0;
                countRow = listResult.Count();

                if (countRow != 0)
                {
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rds = new ReportDataSource("LHDN", dt);
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                    //Path
                    ReportViewer1.LocalReport.ReportPath = "SUMBER_MANUSIA/HR_LHDN_Report.rdlc";
                    //Parameters

                    ReportParameter[] rptParams = new ReportParameter[0];
                    {
                        //new ReportParameter("param1", dd_kat.SelectedValue);
                        //new ReportParameter("toDate",ToDate .Text )
                        //new ReportParameter("fromDate",datedari  ),
                        //new ReportParameter("toDate",datehingga ),
                        //new ReportParameter("caw",branch ),     
                        //new ReportParameter("Cdate",DateTime.Now.ToString("dd/MM/yyyy") ),     
                        //new ReportParameter("Date",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  )
                    };


                    ReportViewer1.LocalReport.SetParameters(rptParams);
                    //Refresh
                    ReportViewer1.LocalReport.Refresh();

                    Warning[] warnings;

                    string[] streamids;

                    string mimeType;

                    string encoding;

                    string extension;
                    string filename;

                    filename = string.Format("{0}.{1}", "Laporan_Hartanah_" + DateTime.Now.ToString("ddMMyyyy") + "", "pdf");
                    byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);


                    //byte[] bytes = ReportViewer1.LocalReport.Render("PDF", devinfo, out mimeType, out encoding, out extension, out streamids, out warnings);


                    Response.Buffer = true;
                    Response.Clear();
                    Response.ContentType = mimeType;
                    string extfile = DateTime.Now.ToString("dd_MM_yyyy.");
                    Response.AddHeader("content-disposition", "attachment; filename=LHDN_" + extfile + extension);
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();
                }
            }
            else if (ddl_reporttye.SelectedValue == "04")
            {
                dd_selqry();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt = Con.Ora_Execute_table("select * from (select FORMAT(stf_service_start_dt,'dd/MM/yyyy') ser_sdt,FORMAT(stf_service_end_dt,'dd/MM/yyyy') ser_edt,ISNULL(stf_socso_no,'') perk_no,ISNULL(ss1.tax_incometax_no,'') tax_no,UPPER(rj.hr_jaw_desc) as s1,sp.stf_name,sp.stf_staff_no as s2,sp.stf_icno as s3, ISNULL(sp.stf_epf_no,'') as s4,UPPER(sp.stf_permanent_address) as s5,a.as6 as s6,a.as7 as s7,as12 as s8,a.as5 as s9,a.as9 as s10,(a.as8) as s11,as10 s12,as11 s13 from (select inc_staff_no, sum(ISNULL(inc_salary_amt,'0.00')) as as5,sum(ISNULL(inc_pcb_amt,'0.00')) as as6,(sum (ISNULL(inc_cp38_amt,'0.00')) + sum(ISNULL(inc_cp38_amt2,'0.00'))) as as7,(sum(ISNULL(inc_salary_amt,'0.00')) + sum(ISNULL(inc_ot_amt,'0.00'))+ sum(ISNULL(inc_cumm_fix_allwnce_amt,'0.00'))) as as8,sum(ISNULL(inc_bonus_amt,'0.00')) as as9,sum(ISNULL(inc_cumm_xtra_allwnce_amt,'0.00')) as10,sum(ISNULL(inc_tunggakan_amt,'0.00')) as11,sum(ISNULL(inc_ded_amt,'0.00')) as12 from hr_income left join  hr_staff_profile as aa1 on aa1.stf_staff_no=inc_staff_no " + sqry1 + " group by inc_staff_no) as a left join hr_staff_profile as sp on sp.stf_staff_no=a.inc_staff_no left join hr_income_tax ss1 on ss1.tax_staff_no=a.inc_staff_no and ('"+ txt_tahun.SelectedValue +"-"+ DD_bulancaruman.SelectedValue +"' between FORMAT(tax_pcb_start_dt,'yyyy-MM') And FORMAT(tax_pcb_end_dt,'yyyy-MM')) and tax_type='1' left join Ref_hr_Jawatan rj on rj.hr_jaw_Code=sp.stf_curr_post_cd " + sqry2 + " ) as a outer apply(select count(*) cnt_chld from hr_children where chl_staff_no=a.s2) as b");
                ds.Tables.Add(dt);

                ReportViewer1.Reset();
                ReportViewer1.LocalReport.Refresh();
                List<DataRow> listResult = dt.AsEnumerable().ToList();
                listResult.Count();
                int countRow = 0;
                countRow = listResult.Count();

                if (countRow != 0)
                {
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.ReportPath = "SUMBER_MANUSIA/EA_Form1.rdlc";
                    //ReportViewer1.LocalReport.ReportPath = "KWSP.rdlc";
                    ReportDataSource rds = new ReportDataSource("EA1", dt);


                    ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("syear", txt_tahun.SelectedValue)
         };
                    ReportViewer1.LocalReport.SetParameters(rptParams);
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                    //Refresh
                    ReportViewer1.LocalReport.DisplayName = "EA_FORM_" + DateTime.Now.ToString("ddMMyyyy");
                    //Refresh
                    ReportViewer1.LocalReport.Refresh();
                    Warning[] warnings;

                    string[] streamids;

                    string mimeType;

                    string encoding;

                    string extension;

                    string filename;

                    string fname =
                    fname = string.Format("{0}.{1}", "EA_FORM_" + DateTime.Now.ToString("ddMMyyyy") + "", "pdf");
                    byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                    Response.Buffer = true;
                    Response.Clear();
                    Response.ContentType = mimeType;
                    Response.AddHeader("content-disposition", "attachment; filename=" + fname);
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();
                }
                else
                {
                    grid();                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod tidak dijumpai. Sila Pastikan Semua Maklumat Dimasukkan Dengan Betul.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else if (ddl_reporttye.SelectedValue == "05")
            {
                dd_selqry();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt = Con.Ora_Execute_table("select (cast(inc_month as varchar) +''+ cast(inc_year as varchar)) inc_month,stf_email,stf_phone_m,hr_negeri_desc ,stf_staff_no,stf_icno,UPPER(stf_name) as stf_name,stf_curr_post_cd,org_id,stf_epf_no,org_name,org_address,org_epf_no,epf_staff_no,epf_amt,inc_staff_no,inc_month,inc_year,inc_org_id,inc_salary_amt,inc_sip_amt,inc_emp_sip_amt from hr_income I inner join hr_post_his P on P.pos_staff_no=I.inc_staff_no inner join hr_staff_profile as HR on HR.stf_staff_no=I.inc_staff_no inner join hr_organization o on o.org_id=i.inc_org_id and o.org_gen_id = HR.str_curr_org_cd left join Ref_hr_negeri as n on n.hr_negeri_Code=HR.stf_curr_post_cd left join hr_kwsp as k on k.epf_staff_no=i.inc_staff_no and epf_end_dt='9999-12-31' where " + sqry11 + " and (inc_SIP_amt != '0.00' or inc_emp_SIP_amt != '0.00') group by stf_epf_no,stf_staff_no,stf_icno,stf_name,stf_email,stf_phone_m,hr_negeri_desc,stf_curr_post_cd,org_id, org_name,org_address,org_epf_no,epf_staff_no,epf_amt,inc_staff_no,inc_month,inc_year,inc_org_id,inc_salary_amt,inc_sip_amt,inc_emp_sip_amt");
                ds.Tables.Add(dt);

                ReportViewer1.Reset();
                ReportViewer1.LocalReport.Refresh();
                List<DataRow> listResult = dt.AsEnumerable().ToList();
                listResult.Count();
                int countRow = 0;
                countRow = listResult.Count();

                if (countRow != 0)
                {
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.ReportPath = "SUMBER_MANUSIA/SIP_Form1.rdlc";
                    //ReportViewer1.LocalReport.ReportPath = "KWSP.rdlc";
                    ReportDataSource rds = new ReportDataSource("sip1", dt);


         //           ReportParameter[] rptParams = new ReportParameter[]{
         //            new ReportParameter("syear", txt_tahun.SelectedValue)
         //};
         //           ReportViewer1.LocalReport.SetParameters(rptParams);
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                    //Refresh
                    ReportViewer1.LocalReport.DisplayName = "SIP_FORM_" + DateTime.Now.ToString("ddMMyyyy");
                    //Refresh
                    ReportViewer1.LocalReport.Refresh();
                    Warning[] warnings;

                    string[] streamids;

                    string mimeType;

                    string encoding;

                    string extension;

                    string filename;

                    string fname = string.Format("{0}.{1}", "SIP_FORM_" + DateTime.Now.ToString("ddMMyyyy") + "", "pdf");
                    byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                    Response.Buffer = true;
                    Response.Clear();
                    Response.ContentType = mimeType;
                    Response.AddHeader("content-disposition", "attachment; filename=" + fname);
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();
                }
                else
                {
                    grid();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod tidak dijumpai. Sila Pastikan Semua Maklumat Dimasukkan Dengan Betul.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else if (ddl_reporttye.SelectedValue == "06")
            {
                dd_selqry();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt = Con.Ora_Execute_table("select s1.ded_staff_no v1,s2.stf_name v2,s2.stf_icno v3,s1.ded_ref_no v4,s1.ded_deduct_amt v5,s4.hr_poto_desc v6 from hr_deduction s1 left join hr_staff_profile s2 on s2.stf_staff_no=s1.ded_staff_no inner join hr_post_his s3 on s3.pos_staff_no=s2.stf_staff_no and s3.pos_end_dt >='" + DateTime.Now.ToString("yyyy-MM-dd") + "' inner join Ref_hr_potongan s4 on s4.hr_poto_Code=s1.ded_deduct_type_cd and s4.Status='A' " + sqry1 + "");
                ds.Tables.Add(dt);

                ReportViewer1.Reset();
                ReportViewer1.LocalReport.Refresh();
                List<DataRow> listResult = dt.AsEnumerable().ToList();
                listResult.Count();
                int countRow = 0;
                countRow = listResult.Count();

                string ss_jen = string.Empty;
                if(sel_frmt.SelectedValue != "")
                {
                    ss_jen = sel_frmt.SelectedItem.Text.ToUpper();
                }
                else
                {
                    ss_jen = "SEMUA";
                }
               

                if (countRow != 0)
                {
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.ReportPath = "SUMBER_MANUSIA/potongan.rdlc";
                    //ReportViewer1.LocalReport.ReportPath = "KWSP.rdlc";
                    ReportDataSource rds = new ReportDataSource("pot1", dt);


                    ReportParameter[] rptParams = new ReportParameter[]{
                                new ReportParameter("s1", txt_tahun.SelectedValue),
                                new ReportParameter("s2", DD_bulancaruman.SelectedItem.Text),
                                new ReportParameter("s3", ss_jen)
                    };
                    ReportViewer1.LocalReport.SetParameters(rptParams);
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                    //Refresh
                    ReportViewer1.LocalReport.DisplayName = "LAIN-LAIN_POTONGAN_" + DateTime.Now.ToString("ddMMyyyy");
                    //Refresh
                    ReportViewer1.LocalReport.Refresh();
                    Warning[] warnings;

                    string[] streamids;

                    string mimeType;

                    string encoding;

                    string extension;

                    string filename;

                    string fname = string.Format("{0}.{1}", "LAIN-LAIN_POTONGAN_" + DateTime.Now.ToString("ddMMyyyy") + "", "pdf");
                    byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                    Response.Buffer = true;
                    Response.Clear();
                    Response.ContentType = mimeType;
                    Response.AddHeader("content-disposition", "attachment; filename=" + fname);
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();
                }
                else
                {
                    grid();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod tidak dijumpai. Sila Pastikan Semua Maklumat Dimasukkan Dengan Betul.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }


            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }

        catch
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }



    private DataSet GetData(string query)
    {
        string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;

        SqlCommand cmd = new SqlCommand(query);
        using (SqlConnection con = new SqlConnection(conString))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                cmd.Connection = con;

                sda.SelectCommand = cmd;
                using (DataSet dsCustomers = new DataSet())
                {
                    sda.Fill(dsCustomers, "DataTable1");
                    return dsCustomers;
                }
            }
        }
    }



    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../SUMBER_MANUSIA/HR_KWSP_view.aspx");
    }


}