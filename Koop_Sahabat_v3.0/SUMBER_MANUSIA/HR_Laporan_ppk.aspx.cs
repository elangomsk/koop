using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Text;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.Globalization;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Net;
using System.Data;
using System.Threading;

public partial class HR_Laporan_ppk : System.Web.UI.Page
{
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    SqlCommand com;
    SqlDataReader Dr;
    DataTable dt = new DataTable();
    string Sql = string.Empty;
    string Status = string.Empty;
    string level, userid, stscd, abc1;
    string val1 = string.Empty, val2 = string.Empty, val3 = string.Empty, val4 = string.Empty, val5 = string.Empty;
    string phdate1 = string.Empty, phdate2 = string.Empty;
    decimal total = 0M;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        app_language();
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                level = Session["level"].ToString();
                userid = Session["New"].ToString();

                tm_date.Attributes.Add("Readonly", "Readonly");
                ta_date.Attributes.Add("Readonly", "Readonly");
                OrgBind();
                JabaBind();
                ub();
                //grid();
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
        ste_set = DBCon.Ora_Execute_table("select * from site_settings where ID IN ('1')");

        DataTable gt_lng = new DataTable();
        gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('541','448','542','1405','1288','795','544','543','1376','1465')");

        CultureInfo culinfo = Thread.CurrentThread.CurrentUICulture;
        TextInfo txtinfo = culinfo.TextInfo;

        h1_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
        bb1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
        bb2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());

        h3_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
        h3_tag2.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());

        lbl1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
        lbl2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
        lbl3_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
        lbl4_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
        lbl5_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());        
        Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }

    string RightJustified(int count)
    {
        if (count < 0)
            count = 0;
        return new string('0', count);
    }
    string Space(int count)
    {
        if (count < 0)
            count = 0;
        return new string(' ', count);
    }

    void OrgBind()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select DISTINCT org_gen_id, UPPER(org_name) as org_name from hr_organization  order by org_name";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_org.DataSource = dt;
            dd_org.DataTextField = "org_name";
            dd_org.DataValueField = "org_gen_id";
            dd_org.DataBind();
            dd_org.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void JabaBind()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select hr_jaba_Code,UPPER(hr_jaba_desc) as hr_jaba_desc from Ref_hr_jabatan WHERE Status = 'A' ORDER BY hr_jaba_desc";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_jabatan.DataSource = dt;
            dd_jabatan.DataTextField = "hr_jaba_desc";
            dd_jabatan.DataValueField = "hr_jaba_Code";
            dd_jabatan.DataBind();
            dd_jabatan.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void UnitBind(object sender, EventArgs e)
    {

        ub();
    }

    void ub()
    {
        DataSet Ds = new DataSet();
        try
        {
            if (dd_jabatan.SelectedValue != "")
            {
                string com = "select hr_unit_Code,UPPER(hr_unit_desc) as hr_unit_desc from Ref_hr_unit WHERE hr_jaba_code='" + dd_jabatan.SelectedValue + "' and Status = 'A' ORDER BY hr_unit_desc";
                SqlDataAdapter adpt = new SqlDataAdapter(com, con);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                dd_unit.DataSource = dt;
                dd_unit.DataTextField = "hr_unit_desc";
                dd_unit.DataValueField = "hr_unit_Code";
                dd_unit.DataBind();
                dd_unit.Items.Insert(0, new ListItem("--- PILIH ---", ""));
                //grid();
            }
            else
            {
                dd_unit.Items.Clear();
                dd_unit.Items.Insert(0, new ListItem("--- PILIH ---", ""));
                //grid();
            }


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void rset_click(object sender, EventArgs e)
    {
        Response.Redirect("HR_CUTI.aspx");
    }

    void clk_print()
    {
        {
            try
            {
                if (dd_org.SelectedValue != "" || dd_jabatan.SelectedValue.Trim() != "" || dd_unit.SelectedValue != "" || tm_date.Text != "" || ta_date.Text != "")
                {
                    if (tm_date.Text != "" && ta_date.Text != "")
                    {
                        string d1 = tm_date.Text;
                        DateTime today1 = DateTime.ParseExact(d1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        phdate1 = today1.ToString("yyyy-MM-dd");

                        string d2 = ta_date.Text;
                        DateTime today2 = DateTime.ParseExact(d2, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        phdate2 = today2.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        phdate1 = "";
                        phdate2 = "";
                    }
                    if (dd_org.SelectedValue != "" && dd_jabatan.SelectedValue.Trim() == "" && dd_unit.SelectedValue == "" && tm_date.Text == "" && ta_date.Text == "")
                    {
                        val1 = "select sa.aps_staff_no,sa.aps_staff_name,rj.hr_jaw_desc,pk.hr_kate_desc,sa.aps_section1_score,sa.aps_section2_score,sa.aps_section3_score,sa.aps_section4_score,sa.aps_total_score from hr_appraisal_summary as sa left join Ref_hr_Jawatan as rj on rj.hr_jaw_Code=sa.aps_post_cd left join Ref_hr_penj_kategori as pk on pk.hr_kate_Code=sa.aps_post_cat_cd  where aps_org_id='" + dd_org.SelectedValue + "'";
                    }
                    else if (dd_org.SelectedValue == "" && dd_jabatan.SelectedValue.Trim() != "" && dd_unit.SelectedValue == "" && tm_date.Text == "" && ta_date.Text == "")
                    {
                        val1 = "select sa.aps_staff_no,sa.aps_staff_name,rj.hr_jaw_desc,pk.hr_kate_desc,sa.aps_section1_score,sa.aps_section2_score,sa.aps_section3_score,sa.aps_section4_score,sa.aps_total_score from hr_appraisal_summary as sa left join Ref_hr_Jawatan as rj on rj.hr_jaw_Code=sa.aps_post_cd left join Ref_hr_penj_kategori as pk on pk.hr_kate_Code=sa.aps_post_cat_cd  where aps_dept_cd='" + dd_jabatan.SelectedValue.Trim() + "'";
                    }
                    else if (dd_org.SelectedValue == "" && dd_jabatan.SelectedValue.Trim() == "" && dd_unit.SelectedValue != "" && tm_date.Text == "" && ta_date.Text == "")
                    {
                        val1 = "select sa.aps_staff_no,sa.aps_staff_name,rj.hr_jaw_desc,pk.hr_kate_desc,sa.aps_section1_score,sa.aps_section2_score,sa.aps_section3_score,sa.aps_section4_score,sa.aps_total_score from hr_appraisal_summary as sa left join Ref_hr_Jawatan as rj on rj.hr_jaw_Code=sa.aps_post_cd left join Ref_hr_penj_kategori as pk on pk.hr_kate_Code=sa.aps_post_cat_cd  where aps_unit_cd='" + dd_unit.SelectedValue.Trim() + "'";
                    }

                    else if (dd_org.SelectedValue != "" && dd_jabatan.SelectedValue.Trim() != "" && dd_unit.SelectedValue == "" && tm_date.Text == "" && ta_date.Text == "")
                    {
                        val1 = "select sa.aps_staff_no,sa.aps_staff_name,rj.hr_jaw_desc,pk.hr_kate_desc,sa.aps_section1_score,sa.aps_section2_score,sa.aps_section3_score,sa.aps_section4_score,sa.aps_total_score from hr_appraisal_summary as sa left join Ref_hr_Jawatan as rj on rj.hr_jaw_Code=sa.aps_post_cd left join Ref_hr_penj_kategori as pk on pk.hr_kate_Code=sa.aps_post_cat_cd  where aps_org_id='" + dd_org.SelectedValue + "' and aps_dept_cd='" + dd_jabatan.SelectedValue.Trim() + "'";
                    }
                    else if (dd_org.SelectedValue != "" && dd_jabatan.SelectedValue.Trim() == "" && dd_unit.SelectedValue != "" && tm_date.Text == "" && ta_date.Text == "")
                    {
                        val1 = "select sa.aps_staff_no,sa.aps_staff_name,rj.hr_jaw_desc,pk.hr_kate_desc,sa.aps_section1_score,sa.aps_section2_score,sa.aps_section3_score,sa.aps_section4_score,sa.aps_total_score from hr_appraisal_summary as sa left join Ref_hr_Jawatan as rj on rj.hr_jaw_Code=sa.aps_post_cd left join Ref_hr_penj_kategori as pk on pk.hr_kate_Code=sa.aps_post_cat_cd  where aps_org_id='" + dd_org.SelectedValue + "' and aps_unit_cd='" + dd_unit.SelectedValue.Trim() + "'";
                    }
                    else if (dd_org.SelectedValue != "" && dd_jabatan.SelectedValue.Trim() == "" && dd_unit.SelectedValue == "" && tm_date.Text != "" && ta_date.Text == "")
                    {
                        val1 = "select sa.aps_staff_no,sa.aps_staff_name,rj.hr_jaw_desc,pk.hr_kate_desc,sa.aps_section1_score,sa.aps_section2_score,sa.aps_section3_score,sa.aps_section4_score,sa.aps_total_score from hr_appraisal_summary as sa left join Ref_hr_Jawatan as rj on rj.hr_jaw_Code=sa.aps_post_cd left join Ref_hr_penj_kategori as pk on pk.hr_kate_Code=sa.aps_post_cat_cd  where aps_org_id='" + dd_org.SelectedValue + "' and aps_start_dt='" + phdate1 + "'";
                    }
                    else if (dd_org.SelectedValue != "" && dd_jabatan.SelectedValue.Trim() == "" && dd_unit.SelectedValue == "" && tm_date.Text == "" && ta_date.Text != "")
                    {
                        val1 = "select sa.aps_staff_no,sa.aps_staff_name,rj.hr_jaw_desc,pk.hr_kate_desc,sa.aps_section1_score,sa.aps_section2_score,sa.aps_section3_score,sa.aps_section4_score,sa.aps_total_score from hr_appraisal_summary as sa left join Ref_hr_Jawatan as rj on rj.hr_jaw_Code=sa.aps_post_cd left join Ref_hr_penj_kategori as pk on pk.hr_kate_Code=sa.aps_post_cat_cd  where aps_org_id='" + dd_org.SelectedValue + "' and aps_end_dt='" + phdate2 + "'";
                    }
                    else if (dd_org.SelectedValue == "" && dd_jabatan.SelectedValue.Trim() != "" && dd_unit.SelectedValue != "" && tm_date.Text == "" && ta_date.Text == "")
                    {
                        val1 = "select sa.aps_staff_no,sa.aps_staff_name,rj.hr_jaw_desc,pk.hr_kate_desc,sa.aps_section1_score,sa.aps_section2_score,sa.aps_section3_score,sa.aps_section4_score,sa.aps_total_score from hr_appraisal_summary as sa left join Ref_hr_Jawatan as rj on rj.hr_jaw_Code=sa.aps_post_cd left join Ref_hr_penj_kategori as pk on pk.hr_kate_Code=sa.aps_post_cat_cd where aps_dept_cd='" + dd_jabatan.SelectedValue.Trim() + "' and aps_unit_cd='" + dd_unit.SelectedValue.Trim() + "'";
                    }

                    else if (dd_org.SelectedValue == "" && dd_jabatan.SelectedValue.Trim() != "" && dd_unit.SelectedValue == "" && tm_date.Text != "" && ta_date.Text != "")
                    {
                        val1 = "select sa.aps_staff_no,sa.aps_staff_name,rj.hr_jaw_desc,pk.hr_kate_desc,sa.aps_section1_score,sa.aps_section2_score,sa.aps_section3_score,sa.aps_section4_score,sa.aps_total_score from hr_appraisal_summary as sa left join Ref_hr_Jawatan as rj on rj.hr_jaw_Code=sa.aps_post_cd left join Ref_hr_penj_kategori as pk on pk.hr_kate_Code=sa.aps_post_cat_cd  where aps_dept_cd='" + dd_jabatan.SelectedValue.Trim() + "' and aps_start_dt>=DATEADD(day, DATEDIFF(day, 0, '" + phdate1 + "'), 0) and aps_start_dt<=DATEADD(day, DATEDIFF(day, 0, '" + phdate2 + "'), +1)";
                    }
                    else if (dd_org.SelectedValue == "" && dd_jabatan.SelectedValue.Trim() == "" && dd_unit.SelectedValue != "" && tm_date.Text != "" && ta_date.Text != "")
                    {
                        val1 = "select sa.aps_staff_no,sa.aps_staff_name,rj.hr_jaw_desc,pk.hr_kate_desc,sa.aps_section1_score,sa.aps_section2_score,sa.aps_section3_score,sa.aps_section4_score,sa.aps_total_score from hr_appraisal_summary as sa left join Ref_hr_Jawatan as rj on rj.hr_jaw_Code=sa.aps_post_cd left join Ref_hr_penj_kategori as pk on pk.hr_kate_Code=sa.aps_post_cat_cd  where aps_unit_cd='" + dd_unit.SelectedValue.Trim() + "' and aps_start_dt>=DATEADD(day, DATEDIFF(day, 0, '" + phdate1 + "'), 0) and aps_start_dt<=DATEADD(day, DATEDIFF(day, 0, '" + phdate2 + "'), +1)";
                    }

                    else if (dd_org.SelectedValue == "" && dd_jabatan.SelectedValue.Trim() == "" && dd_unit.SelectedValue == "" && tm_date.Text != "" && ta_date.Text != "")
                    {
                        val1 = "select sa.aps_staff_no,sa.aps_staff_name,rj.hr_jaw_desc,pk.hr_kate_desc,sa.aps_section1_score,sa.aps_section2_score,sa.aps_section3_score,sa.aps_section4_score,sa.aps_total_score from hr_appraisal_summary as sa left join Ref_hr_Jawatan as rj on rj.hr_jaw_Code=sa.aps_post_cd left join Ref_hr_penj_kategori as pk on pk.hr_kate_Code=sa.aps_post_cat_cd  where aps_start_dt>=DATEADD(day, DATEDIFF(day, 0, '" + phdate1 + "'), 0) and aps_start_dt<=DATEADD(day, DATEDIFF(day, 0, '" + phdate2 + "'), +1)";
                    }
                    else if (dd_org.SelectedValue != "" && dd_jabatan.SelectedValue.Trim() != "" && dd_unit.SelectedValue != "" && tm_date.Text != "" && ta_date.Text != "")
                    {
                        val1 = "select sa.aps_staff_no,sa.aps_staff_name,rj.hr_jaw_desc,pk.hr_kate_desc,sa.aps_section1_score,sa.aps_section2_score,sa.aps_section3_score,sa.aps_section4_score,sa.aps_total_score from hr_appraisal_summary as sa left join Ref_hr_Jawatan as rj on rj.hr_jaw_Code=sa.aps_post_cd left join Ref_hr_penj_kategori as pk on pk.hr_kate_Code=sa.aps_post_cat_cd  where aps_org_id='" + dd_org.SelectedValue + "' and aps_dept_cd='" + dd_jabatan.SelectedValue.Trim() + "' and aps_unit_cd='" + dd_unit.SelectedValue.Trim() + "' and  aps_start_dt>=DATEADD(day, DATEDIFF(day, 0, '" + phdate1 + "'), 0) and aps_start_dt<=DATEADD(day, DATEDIFF(day, 0, '" + phdate2 + "'), +1)";
                    }
                    else if (dd_org.SelectedValue != "" && dd_jabatan.SelectedValue.Trim() == "" && dd_unit.SelectedValue == "" && tm_date.Text != "" && ta_date.Text != "")
                    {
                        val1 = "select sa.aps_staff_no,sa.aps_staff_name,rj.hr_jaw_desc,pk.hr_kate_desc,sa.aps_section1_score,sa.aps_section2_score,sa.aps_section3_score,sa.aps_section4_score,sa.aps_total_score from hr_appraisal_summary as sa left join Ref_hr_Jawatan as rj on rj.hr_jaw_Code=sa.aps_post_cd left join Ref_hr_penj_kategori as pk on pk.hr_kate_Code=sa.aps_post_cat_cd  where aps_org_id='" + dd_org.SelectedValue + "' and  aps_start_dt>=DATEADD(day, DATEDIFF(day, 0, '" + phdate1 + "'), 0) and aps_start_dt<=DATEADD(day, DATEDIFF(day, 0, '" + phdate2 + "'), +1)";
                    }
                    else
                    {
                        val1 = "select sa.aps_staff_no,sa.aps_staff_name,rj.hr_jaw_desc,pk.hr_kate_desc,sa.aps_section1_score,sa.aps_section2_score,sa.aps_section3_score,sa.aps_section4_score,sa.aps_total_score from hr_appraisal_summary as sa left join Ref_hr_Jawatan as rj on rj.hr_jaw_Code=sa.aps_post_cd left join Ref_hr_penj_kategori as pk on pk.hr_kate_Code=sa.aps_post_cat_cd  where aps_org_id=''";
                    }

                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    dt = DBCon.Ora_Execute_table("" + val1 + "");
                    RptviwerStudent.Reset();
                    ds.Tables.Add(dt);

                    List<DataRow> listResult = dt.AsEnumerable().ToList();
                    listResult.Count();
                    int countRow = 0;
                    countRow = listResult.Count();

                    RptviwerStudent.LocalReport.DataSources.Clear();
                    if (countRow != 0)
                    {
                        hdr_txt.Visible = true;
                        RptviwerStudent.LocalReport.ReportPath = "SUMBER_MANUSIA/hr_lppk.rdlc";
                        ReportDataSource rds = new ReportDataSource("hr_lppk", dt);
                        RptviwerStudent.LocalReport.DataSources.Add(rds);
                        RptviwerStudent.LocalReport.DisplayName = " PENILAIAN_PRESTASI_" + DateTime.Now.ToString("ddMMyyyy");
                        //Refresh
                        RptviwerStudent.LocalReport.Refresh();

                    }
                    else if (countRow == 0)
                    {                        
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod tidak dijumpai. Sila Pastikan Semua Maklumat Dimasukkan Dengan Betul.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    //grid();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }




    void instaudt()
    {
        string audcd = "040406";
        string auddec = "JANA MAKLUMAT PENGGAJIAN";
        string usrid = Session["New"].ToString();
        string curdt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string Inssql = "insert into cmn_audit_trail(aud_crt_id,aud_crt_dt,aud_txn_cd,aud   _txn_desc) values ('" + usrid + "','" + curdt + "','" + audcd + "','" + auddec + "')";
        Status = DBCon.Ora_Execute_CommamdText(Inssql);
    }
    protected void srch_click(object sender, EventArgs e)
    {
        try
        {
            if (dd_org.SelectedValue.Trim() != "" || dd_jabatan.SelectedValue.Trim() != "" || dd_unit.SelectedValue.Trim() != "" || tm_date.Text != "" && ta_date.Text != "")
            {
                clk_print();
            }
            else
            {                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                clk_print();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void clk_rset(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }
}