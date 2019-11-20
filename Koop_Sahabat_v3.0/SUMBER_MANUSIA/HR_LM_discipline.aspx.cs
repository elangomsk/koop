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
using Microsoft.Reporting.WebForms;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Web.SessionState;
using System.Xml;
using System.Threading;

public partial class HR_LM_discipline : System.Web.UI.Page
{
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    SqlCommand com;
    DataTable dt = new DataTable();
    string Status = string.Empty;
    string level, userid, stscd, abc1;
    string val1 = string.Empty;
    string phdate1 = string.Empty, phdate2 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        app_language();
        //ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        //scriptManager.RegisterPostBackControl(this.Button3);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                level = Session["level"].ToString();
                userid = Session["New"].ToString();

                //tm_date.Attributes.Add("Readonly", "Readonly");
                //ta_date.Attributes.Add("Readonly", "Readonly");
                OrgBind();
                //JabaBind();
                Bind_jlati();
                Bind_klati();
                grid();
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
        gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('549','448','550','484','1405','497','1288', '795','1407','1468','551','552','1376','15','553')");

        CultureInfo culinfo = Thread.CurrentThread.CurrentUICulture;
        TextInfo txtinfo = culinfo.TextInfo;

        //h1_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
        //bb1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
        //bb2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());

        //h3_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
        //h3_tag2.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());

        //lbl1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
        //lbl2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
        //lbl3_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());

        //lbl4_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
        //lbl5_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
        //lbl6_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());

        //lbl7_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());
        //lbl8_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
        //lbl9_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
          
        Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
        Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetCompletionList(string prefixText, int count)
    {

        using (SqlConnection con = new SqlConnection())
        {

            con.ConnectionString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
            DBConnection dbcon1 = new DBConnection();
            DataTable qry1 = new DataTable();
            qry1 = dbcon1.Ora_Execute_table("select stf_staff_no from hr_staff_profile where stf_staff_no LIKE '%" + prefixText + "%'");
            if (qry1.Rows.Count != 0)
            {
                using (SqlCommand com = new SqlCommand())
                {
                    com.CommandText = "select stf_staff_no from hr_staff_profile where stf_staff_no LIKE '%' + @Search + '%'";
                    com.Parameters.AddWithValue("@Search", prefixText);
                    com.Connection = con;
                    con.Open();
                    List<string> countryNames = new List<string>();
                    using (SqlDataReader sdr = com.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            countryNames.Add(sdr["stf_staff_no"].ToString());

                        }
                    }

                    con.Close();
                    return countryNames;
                }
            }
            else
            {
                using (SqlCommand com = new SqlCommand())
                {
                    com.CommandText = "select stf_staff_no,stf_name from hr_staff_profile where stf_name LIKE '%' + @Search + '%'";
                    com.Parameters.AddWithValue("@Search", prefixText);
                    com.Connection = con;
                    con.Open();
                    List<string> countryNames = new List<string>();
                    using (SqlDataReader sdr = com.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            countryNames.Add(sdr["stf_name"].ToString());

                        }
                    }

                    con.Close();
                    return countryNames;
                }
            }
        }
    }

    protected void sel_orgbind(object sender, EventArgs e)
    {
        org_pern();
        grid();
    }
    void org_pern()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select * from hr_organization_pern where op_org_id='" + dd_org.SelectedValue + "' and Status = 'A'";
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

    protected void sel_orgjaba(object sender, EventArgs e)
    {
        org_jaba();
        grid();
    }
    void org_jaba()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select * from hr_organization_jaba where oj_perg_code='" + dd_org_pen.SelectedValue + "' and Status='A'";
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

    void OrgBind()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select org_gen_id, UPPER(org_name) as org_name from hr_organization  order by org_name";
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

    void Bind_jlati()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select jen_latihan_cd,UPPER(jen_latihan_desc) as jen_latihan_desc from Ref_hr_jenis_latihan where status = 'A' order by jen_latihan_cd";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_jl.DataSource = dt;
            dd_jl.DataTextField = "jen_latihan_desc";
            dd_jl.DataValueField = "jen_latihan_cd";
            dd_jl.DataBind();
            dd_jl.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void Bind_klati()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select hr_discipline_Code,hr_discipline_desc  from Ref_hr_discipline";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_kl.DataSource = dt;
            dd_kl.DataTextField = "hr_discipline_desc";
            dd_kl.DataValueField = "hr_discipline_Code";
            dd_kl.DataBind();
            dd_kl.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //void JabaBind()
    //{
    //    DataSet Ds = new DataSet();
    //    try
    //    {
    //        string com = "select hr_jaba_Code,UPPER(hr_jaba_desc) as hr_jaba_desc from Ref_hr_jabatan WHERE Status = 'A' ORDER BY hr_jaba_desc";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
    //        DataTable dt = new DataTable();
    //        adpt.Fill(dt);
    //        dd_jabatan.DataSource = dt;
    //        dd_jabatan.DataTextField = "hr_jaba_desc";
    //        dd_jabatan.DataValueField = "hr_jaba_Code";
    //        dd_jabatan.DataBind();
    //        dd_jabatan.Items.Insert(0, new ListItem("--- PILIH ---", ""));


    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

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
                grid();
            }
            else
            {
                dd_unit.Items.Clear();
                dd_unit.Items.Insert(0, new ListItem("--- PILIH ---", ""));
                grid();
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void sql_info()
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
        if (Kaki_no.Text != "")
        {
            DataTable dd1 = new DataTable();
            dd1 = DBCon.Ora_Execute_table("select stf_staff_no from hr_staff_profile where '" + Kaki_no.Text + "' IN (stf_staff_no,stf_name)");
            if (dd1.Rows.Count != 0)
            {
                Applcn_no1.Text = dd1.Rows[0]["stf_staff_no"].ToString();
            }
        }
        if (dd_org.SelectedValue.Trim() != "" && dd_org_pen.SelectedValue == "" && dd_jabatan.SelectedValue.Trim() == "" && dd_unit.SelectedValue.Trim() == "" && tm_date.Text == "" && ta_date.Text == "" && Kaki_no.Text == "" && dd_kl.SelectedValue == "" && dd_jl.SelectedValue == "")
        {
            val1 = "select sp.stf_staff_no,upper(sp.stf_name) as stf_name,FORMAT(ht.dis_eff_dt,'dd/MM/yyyy', 'en-us')  as tarikh,hu.hr_discipline_desc,org.org_name,dis_catatan,ss1.op_perg_name from hr_discipline as ht  left join hr_post_his as ph on ht.dis_staff_no=ph.pos_staff_no  left join hr_staff_profile as sp on sp.stf_staff_no=ht.dis_staff_no left join Ref_hr_discipline as hu on hu.hr_discipline_Code=ht.dis_discipline_type_cd left join hr_organization org on org.org_gen_id=sp.str_curr_org_cd left join hr_organization_pern ss1 on ss1.op_perg_code=sp.stf_cur_sub_org where ph.pos_end_dt='9999-12-31' and sp.str_curr_org_cd='" + dd_org.SelectedValue.Trim() + "'";
        }
        else if (dd_org.SelectedValue.Trim() != "" && dd_org_pen.SelectedValue != "" && dd_jabatan.SelectedValue.Trim() == "" && dd_unit.SelectedValue.Trim() == "" && tm_date.Text == "" && ta_date.Text == "" && Kaki_no.Text == "" && dd_kl.SelectedValue == "" && dd_jl.SelectedValue == "")
        {
            val1 = "select sp.stf_staff_no,upper(sp.stf_name) as stf_name,FORMAT(ht.dis_eff_dt,'dd/MM/yyyy', 'en-us')  as tarikh,hu.hr_discipline_desc,org.org_name,dis_catatan,ss1.op_perg_name from hr_discipline as ht  left join hr_post_his as ph on ht.dis_staff_no=ph.pos_staff_no  left join hr_staff_profile as sp on sp.stf_staff_no=ht.dis_staff_no left join Ref_hr_discipline as hu on hu.hr_discipline_Code=ht.dis_discipline_type_cd left join hr_organization org on org.org_gen_id=sp.str_curr_org_cd left join hr_organization_pern ss1 on ss1.op_perg_code=sp.stf_cur_sub_org  where ph.pos_end_dt='9999-12-31' and sp.str_curr_org_cd='" + dd_org.SelectedValue.Trim() + "' and sp.stf_cur_sub_org='" + dd_org_pen.SelectedValue + "' ";
        }
        else if (dd_org.SelectedValue.Trim() != "" && dd_org_pen.SelectedValue != "" && dd_jabatan.SelectedValue.Trim() != "" && dd_unit.SelectedValue.Trim() == "" && tm_date.Text == "" && ta_date.Text == "" && Kaki_no.Text == "" && dd_kl.SelectedValue == "" && dd_jl.SelectedValue == "")
        {
            val1 = "select sp.stf_staff_no,upper(sp.stf_name) as stf_name,FORMAT(ht.dis_eff_dt,'dd/MM/yyyy', 'en-us')  as tarikh,hu.hr_discipline_desc,org.org_name,dis_catatan,ss1.op_perg_name from hr_discipline as ht  left join hr_post_his as ph on ht.dis_staff_no=ph.pos_staff_no  left join hr_staff_profile as sp on sp.stf_staff_no=ht.dis_staff_no left join Ref_hr_discipline as hu on hu.hr_discipline_Code=ht.dis_discipline_type_cd left join hr_organization org on org.org_gen_id=sp.str_curr_org_cd left join hr_organization_pern ss1 on ss1.op_perg_code=sp.stf_cur_sub_org  where ph.pos_end_dt='9999-12-31' and sp.str_curr_org_cd='" + dd_org.SelectedValue.Trim() + "' and sp.stf_cur_sub_org='" + dd_org_pen.SelectedValue + "' and ph.pos_dept_cd='" + dd_jabatan.SelectedValue.Trim() + "' ";
        }
        else if (dd_org.SelectedValue.Trim() != "" && dd_org_pen.SelectedValue != "" && dd_jabatan.SelectedValue.Trim() != "" && dd_unit.SelectedValue.Trim() != "" && tm_date.Text == "" && ta_date.Text == "" && Kaki_no.Text == "" && dd_kl.SelectedValue == "" && dd_jl.SelectedValue == "")
        {
            val1 = "select sp.stf_staff_no,upper(sp.stf_name) as stf_name,FORMAT(ht.dis_eff_dt,'dd/MM/yyyy', 'en-us')  as tarikh,hu.hr_discipline_desc,org.org_name,dis_catatan,ss1.op_perg_name from hr_discipline as ht  left join hr_post_his as ph on ht.dis_staff_no=ph.pos_staff_no  left join hr_staff_profile as sp on sp.stf_staff_no=ht.dis_staff_no left join Ref_hr_discipline as hu on hu.hr_discipline_Code=ht.dis_discipline_type_cd left join hr_organization org on org.org_gen_id=sp.str_curr_org_cd left join hr_organization_pern ss1 on ss1.op_perg_code=sp.stf_cur_sub_org where ph.pos_end_dt='9999-12-31' and sp.str_curr_org_cd='" + dd_org.SelectedValue.Trim() + "' and sp.stf_cur_sub_org='" + dd_org_pen.SelectedValue + "' and ph.pos_dept_cd='" + dd_jabatan.SelectedValue.Trim() + "' and ph.pos_unit_cd='" + dd_unit.SelectedValue.Trim() + "' ";
        }
        else if (dd_org.SelectedValue.Trim() == "" && dd_org_pen.SelectedValue == "" && dd_jabatan.SelectedValue.Trim() == "" && dd_unit.SelectedValue.Trim() == "" && tm_date.Text != "" && ta_date.Text != "" && Kaki_no.Text == "" && dd_kl.SelectedValue == "" && dd_jl.SelectedValue == "")
        {
            val1 = "select sp.stf_staff_no,upper(sp.stf_name) as stf_name,FORMAT(ht.dis_eff_dt,'dd/MM/yyyy', 'en-us')  as tarikh,hu.hr_discipline_desc,org.org_name,dis_catatan,ss1.op_perg_name from hr_discipline as ht  left join hr_post_his as ph on ht.dis_staff_no=ph.pos_staff_no  left join hr_staff_profile as sp on sp.stf_staff_no=ht.dis_staff_no left join Ref_hr_discipline as hu on hu.hr_discipline_Code=ht.dis_discipline_type_cd left join hr_organization org on org.org_gen_id=sp.str_curr_org_cd left join hr_organization_pern ss1 on ss1.op_perg_code=sp.stf_cur_sub_org where ph.pos_end_dt='9999-12-31' and ht.dis_eff_dt>=DATEADD(day, DATEDIFF(day, 0, '" + phdate1 + "'), 0) and ht.dis_eff_dt<=DATEADD(day, DATEDIFF(day, 0, '" + phdate2 + "'), 0) ";
        }
        else if (dd_org.SelectedValue.Trim() == "" && dd_org_pen.SelectedValue == "" && dd_jabatan.SelectedValue.Trim() == "" && dd_unit.SelectedValue.Trim() == "" && tm_date.Text == "" && ta_date.Text == "" && Kaki_no.Text != "" && dd_kl.SelectedValue == "" && dd_jl.SelectedValue == "")
        {
            val1 = "select sp.stf_staff_no,upper(sp.stf_name) as stf_name,FORMAT(ht.dis_eff_dt,'dd/MM/yyyy', 'en-us')  as tarikh,hu.hr_discipline_desc,org.org_name,dis_catatan,ss1.op_perg_name from hr_discipline as ht  left join hr_post_his as ph on ht.dis_staff_no=ph.pos_staff_no  left join hr_staff_profile as sp on sp.stf_staff_no=ht.dis_staff_no left join Ref_hr_discipline as hu on hu.hr_discipline_Code=ht.dis_discipline_type_cd left join hr_organization org on org.org_gen_id=sp.str_curr_org_cd left join hr_organization_pern ss1 on ss1.op_perg_code=sp.stf_cur_sub_org where ph.pos_end_dt='9999-12-31' and ht.dis_staff_no='" + Applcn_no1.Text + "' ";
        }
        else if (dd_org.SelectedValue.Trim() == "" && dd_org_pen.SelectedValue == "" && dd_jabatan.SelectedValue.Trim() == "" && dd_unit.SelectedValue.Trim() == "" && tm_date.Text == "" && ta_date.Text == "" && Kaki_no.Text == "" && dd_kl.SelectedValue != "" && dd_jl.SelectedValue == "")
        {
            val1 = "select sp.stf_staff_no,upper(sp.stf_name) as stf_name,FORMAT(ht.dis_eff_dt,'dd/MM/yyyy', 'en-us')  as tarikh,hu.hr_discipline_desc,org.org_name,dis_catatan,ss1.op_perg_name from hr_discipline as ht  left join hr_post_his as ph on ht.dis_staff_no=ph.pos_staff_no  left join hr_staff_profile as sp on sp.stf_staff_no=ht.dis_staff_no left join Ref_hr_discipline as hu on hu.hr_discipline_Code=ht.dis_discipline_type_cd left join hr_organization org on org.org_gen_id=sp.str_curr_org_cd left join hr_organization_pern ss1 on ss1.op_perg_code=sp.stf_cur_sub_org where ph.pos_end_dt='9999-12-31' and ht.dis_discipline_type_cd='" + dd_kl.SelectedValue + "' ";
        }
        else if (dd_org.SelectedValue.Trim() == "" && dd_org_pen.SelectedValue == "" && dd_jabatan.SelectedValue.Trim() == "" && dd_unit.SelectedValue.Trim() == "" && tm_date.Text == "" && ta_date.Text == "" && Kaki_no.Text == "" && dd_kl.SelectedValue == "" && dd_jl.SelectedValue != "")
        {
            val1 = "select sp.stf_staff_no,upper(sp.stf_name) as stf_name,FORMAT(ht.dis_eff_dt,'dd/MM/yyyy', 'en-us')  as tarikh,hu.hr_discipline_desc,org.org_name,dis_catatan,ss1.op_perg_name from hr_discipline as ht  left join hr_post_his as ph on ht.dis_staff_no=ph.pos_staff_no  left join hr_staff_profile as sp on sp.stf_staff_no=ht.dis_staff_no left join Ref_hr_discipline as hu on hu.hr_discipline_Code=ht.dis_discipline_type_cd left join hr_organization org on org.org_gen_id=sp.str_curr_org_cd left join hr_organization_pern ss1 on ss1.op_perg_code=sp.stf_cur_sub_org where ph.pos_end_dt='9999-12-31' and ht.trn_type_cd='" + dd_jl.SelectedValue + "' ";
        }

        else if (dd_org.SelectedValue.Trim() != "" && dd_org_pen.SelectedValue == "" && dd_jabatan.SelectedValue.Trim() == "" && dd_unit.SelectedValue.Trim() == "" && tm_date.Text != "" && ta_date.Text != "" && Kaki_no.Text == "" && dd_kl.SelectedValue == "" && dd_jl.SelectedValue == "")
        {
            val1 = "select sp.stf_staff_no,upper(sp.stf_name) as stf_name,FORMAT(ht.dis_eff_dt,'dd/MM/yyyy', 'en-us')  as tarikh,hu.hr_discipline_desc,org.org_name,dis_catatan,ss1.op_perg_name from hr_discipline as ht  left join hr_post_his as ph on ht.dis_staff_no=ph.pos_staff_no  left join hr_staff_profile as sp on sp.stf_staff_no=ht.dis_staff_no left join Ref_hr_discipline as hu on hu.hr_discipline_Code=ht.dis_discipline_type_cd left join hr_organization org on org.org_gen_id=sp.str_curr_org_cd left join hr_organization_pern ss1 on ss1.op_perg_code=sp.stf_cur_sub_org where ht.dis_eff_dt>=DATEADD(day, DATEDIFF(day, 0, '" + phdate1 + "'), 0) and ht.dis_eff_dt<=DATEADD(day, DATEDIFF(day, 0, '" + phdate2 + "'), 0) and  ph.pos_end_dt='9999-12-31' and sp.str_curr_org_cd='" + dd_org.SelectedValue + "' ";
        }
        else if (dd_org.SelectedValue.Trim() != "" && dd_org_pen.SelectedValue == "" && dd_jabatan.SelectedValue.Trim() == "" && dd_unit.SelectedValue.Trim() == "" && tm_date.Text == "" && ta_date.Text == "" && Kaki_no.Text != "" && dd_kl.SelectedValue == "" && dd_jl.SelectedValue == "")
        {
            val1 = "select sp.stf_staff_no,upper(sp.stf_name) as stf_name,FORMAT(ht.dis_eff_dt,'dd/MM/yyyy', 'en-us')  as tarikh,hu.hr_discipline_desc,org.org_name,dis_catatan,ss1.op_perg_name from hr_discipline as ht  left join hr_post_his as ph on ht.dis_staff_no=ph.pos_staff_no  left join hr_staff_profile as sp on sp.stf_staff_no=ht.dis_staff_no left join Ref_hr_discipline as hu on hu.hr_discipline_Code=ht.dis_discipline_type_cd left join hr_organization org on org.org_gen_id=sp.str_curr_org_cd left join hr_organization_pern ss1 on ss1.op_perg_code=sp.stf_cur_sub_org where ph.pos_end_dt='9999-12-31' and ht.dis_staff_no='" + Applcn_no1.Text + "' and sp.str_curr_org_cd='" + dd_org.SelectedValue + "'  ";
        }
        else if (dd_org.SelectedValue.Trim() != "" && dd_org_pen.SelectedValue == "" && dd_jabatan.SelectedValue.Trim() == "" && dd_unit.SelectedValue.Trim() == "" && tm_date.Text == "" && ta_date.Text == "" && Kaki_no.Text == "" && dd_kl.SelectedValue != "" && dd_jl.SelectedValue == "")
        {
            val1 = "select sp.stf_staff_no,upper(sp.stf_name) as stf_name,FORMAT(ht.dis_eff_dt,'dd/MM/yyyy', 'en-us')  as tarikh,hu.hr_discipline_desc,org.org_name,dis_catatan,ss1.op_perg_name from hr_discipline as ht  left join hr_post_his as ph on ht.dis_staff_no=ph.pos_staff_no  left join hr_staff_profile as sp on sp.stf_staff_no=ht.dis_staff_no left join Ref_hr_discipline as hu on hu.hr_discipline_Code=ht.dis_discipline_type_cd left join hr_organization org on org.org_gen_id=sp.str_curr_org_cd left join hr_organization_pern ss1 on ss1.op_perg_code=sp.stf_cur_sub_org where ph.pos_end_dt='9999-12-31' and ht.dis_discipline_type_cd='" + dd_kl.SelectedValue + "' and sp.str_curr_org_cd='" + dd_org.SelectedValue + "' ";
        }
        else if (dd_org.SelectedValue.Trim() != "" && dd_org_pen.SelectedValue == "" && dd_jabatan.SelectedValue.Trim() == "" && dd_unit.SelectedValue.Trim() == "" && tm_date.Text == "" && ta_date.Text == "" && Kaki_no.Text == "" && dd_kl.SelectedValue == "" && dd_jl.SelectedValue != "")
        {
            val1 = "select sp.stf_staff_no,upper(sp.stf_name) as stf_name,FORMAT(ht.dis_eff_dt,'dd/MM/yyyy', 'en-us')  as tarikh,hu.hr_discipline_desc,org.org_name,dis_catatan,ss1.op_perg_name from hr_discipline as ht  left join hr_post_his as ph on ht.dis_staff_no=ph.pos_staff_no  left join hr_staff_profile as sp on sp.stf_staff_no=ht.dis_staff_no left join Ref_hr_discipline as hu on hu.hr_discipline_Code=ht.dis_discipline_type_cd left join hr_organization org on org.org_gen_id=sp.str_curr_org_cd left join hr_organization_pern ss1 on ss1.op_perg_code=sp.stf_cur_sub_org where ph.pos_end_dt='9999-12-31' and ht.trn_type_cd='" + dd_jl.SelectedValue + "' and sp.str_curr_org_cd='" + dd_org.SelectedValue + "' ";
        }

        else if (dd_org.SelectedValue.Trim() != "" && dd_org_pen.SelectedValue != "" && dd_jabatan.SelectedValue.Trim() == "" && dd_unit.SelectedValue.Trim() == "" && tm_date.Text != "" && ta_date.Text != "" && Kaki_no.Text == "" && dd_kl.SelectedValue == "" && dd_jl.SelectedValue == "")
        {
            val1 = "select sp.stf_staff_no,upper(sp.stf_name) as stf_name,FORMAT(ht.dis_eff_dt,'dd/MM/yyyy', 'en-us')  as tarikh,hu.hr_discipline_desc,org.org_name,dis_catatan,ss1.op_perg_name from hr_discipline as ht  left join hr_post_his as ph on ht.dis_staff_no=ph.pos_staff_no  left join hr_staff_profile as sp on sp.stf_staff_no=ht.dis_staff_no left join Ref_hr_discipline as hu on hu.hr_discipline_Code=ht.dis_discipline_type_cd left join hr_organization org on org.org_gen_id=sp.str_curr_org_cd left join hr_organization_pern ss1 on ss1.op_perg_code=sp.stf_cur_sub_org where ht.dis_eff_dt>=DATEADD(day, DATEDIFF(day, 0, '" + phdate1 + "'), 0) and ht.dis_eff_dt<=DATEADD(day, DATEDIFF(day, 0, '" + phdate2 + "'), 0) and  ph.pos_end_dt='9999-12-31' and sp.str_curr_org_cd='" + dd_org.SelectedValue.Trim() + "' and sp.stf_cur_sub_org='" + dd_org_pen.SelectedValue + "'  ";
        }
        else if (dd_org.SelectedValue.Trim() != "" && dd_org_pen.SelectedValue != "" && dd_jabatan.SelectedValue.Trim() != "" && dd_unit.SelectedValue.Trim() != "" && tm_date.Text == "" && ta_date.Text == "" && Kaki_no.Text != "" && dd_kl.SelectedValue == "" && dd_jl.SelectedValue == "")
        {
            val1 = "select sp.stf_staff_no,upper(sp.stf_name) as stf_name,FORMAT(ht.dis_eff_dt,'dd/MM/yyyy', 'en-us')  as tarikh,hu.hr_discipline_desc,org.org_name,dis_catatan,ss1.op_perg_name from hr_discipline as ht  left join hr_post_his as ph on ht.dis_staff_no=ph.pos_staff_no  left join hr_staff_profile as sp on sp.stf_staff_no=ht.dis_staff_no left join Ref_hr_discipline as hu on hu.hr_discipline_Code=ht.dis_discipline_type_cd left join hr_organization org on org.org_gen_id=sp.str_curr_org_cd left join hr_organization_pern ss1 on ss1.op_perg_code=sp.stf_cur_sub_org where ph.pos_end_dt='9999-12-31' and sp.str_curr_org_cd='" + dd_org.SelectedValue.Trim() + "' and sp.stf_cur_sub_org='" + dd_org_pen.SelectedValue + "' and ht.dis_staff_no='" + Applcn_no1.Text + "'  and ph.pos_dept_cd='" + dd_jabatan.SelectedValue + "' and ph.pos_unit_cd='" + dd_unit.SelectedValue + "' ";
        }
        else if (dd_org.SelectedValue.Trim() != "" && dd_org_pen.SelectedValue != "" && dd_jabatan.SelectedValue.Trim() != "" && dd_unit.SelectedValue.Trim() != "" && tm_date.Text == "" && ta_date.Text == "" && Kaki_no.Text == "" && dd_kl.SelectedValue != "" && dd_jl.SelectedValue == "")
        {
            val1 = "select sp.stf_staff_no,upper(sp.stf_name) as stf_name,FORMAT(ht.dis_eff_dt,'dd/MM/yyyy', 'en-us')  as tarikh,hu.hr_discipline_desc,org.org_name,dis_catatan,ss1.op_perg_name from hr_discipline as ht  left join hr_post_his as ph on ht.dis_staff_no=ph.pos_staff_no  left join hr_staff_profile as sp on sp.stf_staff_no=ht.dis_staff_no left join Ref_hr_discipline as hu on hu.hr_discipline_Code=ht.dis_discipline_type_cd left join hr_organization org on org.org_gen_id=sp.str_curr_org_cd left join hr_organization_pern ss1 on ss1.op_perg_code=sp.stf_cur_sub_org where ph.pos_end_dt='9999-12-31' and sp.str_curr_org_cd='" + dd_org.SelectedValue.Trim() + "' and sp.stf_cur_sub_org='" + dd_org_pen.SelectedValue + "' and ht.dis_discipline_type_cd='" + dd_kl.SelectedValue + "' and ph.pos_dept_cd='" + dd_jabatan.SelectedValue + "' and ph.pos_unit_cd='" + dd_unit.SelectedValue + "' ";
        }
        else if (dd_org.SelectedValue.Trim() != "" && dd_org_pen.SelectedValue != "" && dd_jabatan.SelectedValue.Trim() != "" && dd_unit.SelectedValue.Trim() != "" && tm_date.Text == "" && ta_date.Text == "" && Kaki_no.Text == "" && dd_kl.SelectedValue == "" && dd_jl.SelectedValue != "")
        {
            val1 = "select sp.stf_staff_no,upper(sp.stf_name) as stf_name,FORMAT(ht.dis_eff_dt,'dd/MM/yyyy', 'en-us')  as tarikh,hu.hr_discipline_desc,org.org_name,dis_catatan,ss1.op_perg_name from hr_discipline as ht  left join hr_post_his as ph on ht.dis_staff_no=ph.pos_staff_no  left join hr_staff_profile as sp on sp.stf_staff_no=ht.dis_staff_no left join Ref_hr_discipline as hu on hu.hr_discipline_Code=ht.dis_discipline_type_cd left join hr_organization org on org.org_gen_id=sp.str_curr_org_cd left join hr_organization_pern ss1 on ss1.op_perg_code=sp.stf_cur_sub_org where ph.pos_end_dt='9999-12-31' and ht.trn_type_cd='" + dd_jl.SelectedValue + "' and sp.str_curr_org_cd='" + dd_org.SelectedValue.Trim() + "' and sp.stf_cur_sub_org='" + dd_org_pen.SelectedValue + "' and ph.pos_dept_cd='" + dd_jabatan.SelectedValue + "' and ph.pos_unit_cd='" + dd_unit.SelectedValue + "' ";
        }

        else if (dd_org.SelectedValue.Trim() == "" && dd_org_pen.SelectedValue == "" && dd_jabatan.SelectedValue.Trim() == "" && dd_unit.SelectedValue.Trim() == "" && tm_date.Text != "" && ta_date.Text != "" && Kaki_no.Text != "" && dd_kl.SelectedValue == "" && dd_jl.SelectedValue == "")
        {
            val1 = "select sp.stf_staff_no,upper(sp.stf_name) as stf_name,FORMAT(ht.dis_eff_dt,'dd/MM/yyyy', 'en-us')  as tarikh,hu.hr_discipline_desc,org.org_name,dis_catatan,ss1.op_perg_name from hr_discipline as ht  left join hr_post_his as ph on ht.dis_staff_no=ph.pos_staff_no  left join hr_staff_profile as sp on sp.stf_staff_no=ht.dis_staff_no left join Ref_hr_discipline as hu on hu.hr_discipline_Code=ht.dis_discipline_type_cd left join hr_organization org on org.org_gen_id=sp.str_curr_org_cd left join hr_organization_pern ss1 on ss1.op_perg_code=sp.stf_cur_sub_org where ht.dis_eff_dt>=DATEADD(day, DATEDIFF(day, 0, '" + phdate1 + "'), 0) and ht.dis_eff_dt<=DATEADD(day, DATEDIFF(day, 0, '" + phdate2 + "'), 0) and  ph.pos_end_dt='9999-12-31' and ht.dis_staff_no='" + Kaki_no.Text + "' ";
        }
        else if (dd_org.SelectedValue.Trim() == "" && dd_org_pen.SelectedValue == "" && dd_jabatan.SelectedValue.Trim() == "" && dd_unit.SelectedValue.Trim() == "" && tm_date.Text != "" && ta_date.Text != "" && Kaki_no.Text == "" && dd_kl.SelectedValue != "" && dd_jl.SelectedValue == "")
        {
            val1 = "select sp.stf_staff_no,upper(sp.stf_name) as stf_name,FORMAT(ht.dis_eff_dt,'dd/MM/yyyy', 'en-us')  as tarikh,hu.hr_discipline_desc,org.org_name,dis_catatan,ss1.op_perg_name from hr_discipline as ht  left join hr_post_his as ph on ht.dis_staff_no=ph.pos_staff_no  left join hr_staff_profile as sp on sp.stf_staff_no=ht.dis_staff_no left join Ref_hr_discipline as hu on hu.hr_discipline_Code=ht.dis_discipline_type_cd left join hr_organization org on org.org_gen_id=sp.str_curr_org_cd left join hr_organization_pern ss1 on ss1.op_perg_code=sp.stf_cur_sub_org where ht.dis_eff_dt>=DATEADD(day, DATEDIFF(day, 0, '" + phdate1 + "'), 0) and ht.dis_eff_dt<=DATEADD(day, DATEDIFF(day, 0, '" + phdate2 + "'), 0) and  ph.pos_end_dt='9999-12-31' and ht.dis_discipline_type_cd='" + dd_kl.SelectedValue + "' ";
        }
        else if (dd_org.SelectedValue.Trim() == "" && dd_org_pen.SelectedValue == "" && dd_jabatan.SelectedValue.Trim() == "" && dd_unit.SelectedValue.Trim() == "" && tm_date.Text != "" && ta_date.Text != "" && Kaki_no.Text == "" && dd_kl.SelectedValue == "" && dd_jl.SelectedValue != "")
        {
            val1 = "select sp.stf_staff_no,upper(sp.stf_name) as stf_name,FORMAT(ht.dis_eff_dt,'dd/MM/yyyy', 'en-us')  as tarikh,hu.hr_discipline_desc,org.org_name,dis_catatan,ss1.op_perg_name from hr_discipline as ht  left join hr_post_his as ph on ht.dis_staff_no=ph.pos_staff_no  left join hr_staff_profile as sp on sp.stf_staff_no=ht.dis_staff_no left join Ref_hr_discipline as hu on hu.hr_discipline_Code=ht.dis_discipline_type_cd left join hr_organization org on org.org_gen_id=sp.str_curr_org_cd left join hr_organization_pern ss1 on ss1.op_perg_code=sp.stf_cur_sub_org where ht.dis_eff_dt>=DATEADD(day, DATEDIFF(day, 0, '" + phdate1 + "'), 0) and ht.dis_eff_dt<=DATEADD(day, DATEDIFF(day, 0, '" + phdate2 + "'), 0) and  ph.pos_end_dt='9999-12-31' and ht.trn_type_cd='" + dd_jl.SelectedValue + "' ";
        }
        else if (dd_org.SelectedValue.Trim() == "" && dd_org_pen.SelectedValue == "" && dd_jabatan.SelectedValue.Trim() == "" && dd_unit.SelectedValue.Trim() == "" && tm_date.Text == "" && ta_date.Text == "" && Kaki_no.Text != "" && dd_kl.SelectedValue != "" && dd_jl.SelectedValue == "")
        {
            val1 = "select sp.stf_staff_no,upper(sp.stf_name) as stf_name,FORMAT(ht.dis_eff_dt,'dd/MM/yyyy', 'en-us')  as tarikh,hu.hr_discipline_desc,org.org_name,dis_catatan,ss1.op_perg_name from hr_discipline as ht  left join hr_post_his as ph on ht.dis_staff_no=ph.pos_staff_no  left join hr_staff_profile as sp on sp.stf_staff_no=ht.dis_staff_no left join Ref_hr_discipline as hu on hu.hr_discipline_Code=ht.dis_discipline_type_cd left join hr_organization org on org.org_gen_id=sp.str_curr_org_cd left join hr_organization_pern ss1 on ss1.op_perg_code=sp.stf_cur_sub_org where ph.pos_end_dt='9999-12-31' and ht.dis_staff_no='" + Applcn_no1.Text + "' and ht.dis_discipline_type_cd='" + dd_kl.SelectedValue + "' ";
        }
        else if (dd_org.SelectedValue.Trim() == "" && dd_org_pen.SelectedValue == "" && dd_jabatan.SelectedValue.Trim() == "" && dd_unit.SelectedValue.Trim() == "" && tm_date.Text == "" && ta_date.Text == "" && Kaki_no.Text != "" && dd_kl.SelectedValue == "" && dd_jl.SelectedValue != "")
        {
            val1 = "select sp.stf_staff_no,upper(sp.stf_name) as stf_name,FORMAT(ht.dis_eff_dt,'dd/MM/yyyy', 'en-us')  as tarikh,hu.hr_discipline_desc,org.org_name,dis_catatan,ss1.op_perg_name from hr_discipline as ht  left join hr_post_his as ph on ht.dis_staff_no=ph.pos_staff_no  left join hr_staff_profile as sp on sp.stf_staff_no=ht.dis_staff_no left join Ref_hr_discipline as hu on hu.hr_discipline_Code=ht.dis_discipline_type_cd left join hr_organization org on org.org_gen_id=sp.str_curr_org_cd left join hr_organization_pern ss1 on ss1.op_perg_code=sp.stf_cur_sub_org where ph.pos_end_dt='9999-12-31' and ht.dis_staff_no='" + Applcn_no1.Text + "' and ht.trn_type_cd='" + dd_jl.SelectedValue + "' ";
        }
        else if (dd_org.SelectedValue.Trim() == "" && dd_org_pen.SelectedValue == "" && dd_jabatan.SelectedValue.Trim() == "" && dd_unit.SelectedValue.Trim() == "" && tm_date.Text == "" && ta_date.Text == "" && Kaki_no.Text == "" && dd_kl.SelectedValue != "" && dd_jl.SelectedValue != "")
        {
            val1 = "select sp.stf_staff_no,upper(sp.stf_name) as stf_name,FORMAT(ht.dis_eff_dt,'dd/MM/yyyy', 'en-us')  as tarikh,hu.hr_discipline_desc,org.org_name,dis_catatan,ss1.op_perg_name from hr_discipline as ht  left join hr_post_his as ph on ht.dis_staff_no=ph.pos_staff_no  left join hr_staff_profile as sp on sp.stf_staff_no=ht.dis_staff_no left join Ref_hr_discipline as hu on hu.hr_discipline_Code=ht.dis_discipline_type_cd left join hr_organization org on org.org_gen_id=sp.str_curr_org_cd left join hr_organization_pern ss1 on ss1.op_perg_code=sp.stf_cur_sub_org where ph.pos_end_dt='9999-12-31' and ht.dis_discipline_type_cd='" + dd_kl.SelectedValue + "' and ht.trn_type_cd='" + dd_jl.SelectedValue + "' ";
        }
        else if (dd_org.SelectedValue.Trim() != "" && dd_org_pen.SelectedValue != "" && dd_jabatan.SelectedValue.Trim() != "" && dd_unit.SelectedValue.Trim() != "" && tm_date.Text != "" && ta_date.Text != "" && Kaki_no.Text != "" && dd_kl.SelectedValue != "" && dd_jl.SelectedValue != "")
        {
            val1 = "select sp.stf_staff_no,upper(sp.stf_name) as stf_name,FORMAT(ht.dis_eff_dt,'dd/MM/yyyy', 'en-us')  as tarikh,hu.hr_discipline_desc,org.org_name,dis_catatan,ss1.op_perg_name from hr_discipline as ht  left join hr_post_his as ph on ht.dis_staff_no=ph.pos_staff_no  left join hr_staff_profile as sp on sp.stf_staff_no=ht.dis_staff_no left join Ref_hr_discipline as hu on hu.hr_discipline_Code=ht.dis_discipline_type_cd left join hr_organization org on org.org_gen_id=sp.str_curr_org_cd left join hr_organization_pern ss1 on ss1.op_perg_code=sp.stf_cur_sub_org where ht.dis_eff_dt>=DATEADD(day, DATEDIFF(day, 0, '" + phdate1 + "'), 0) and ht.dis_eff_dt<=DATEADD(day, DATEDIFF(day, 0, '" + phdate2 + "'), 0) and  ph.pos_end_dt='9999-12-31' and ht.dis_staff_no='" + Kaki_no.Text + "' and ht.dis_discipline_type_cd='" + dd_kl.SelectedValue + "' and ht.trn_type_cd='" + dd_jl.SelectedValue + "' and p.str_curr_org_cd='" + dd_org.SelectedValue + "' and sp.stf_cur_sub_org='" + dd_org_pen.SelectedValue + "' and ph.pos_dept_cd='" + dd_jabatan.SelectedValue + "' and ph.pos_unit_cd='" + dd_unit.SelectedValue + "' ";
        }
        else
        {
            val1 = "select sp.stf_staff_no,upper(sp.stf_name) as stf_name,FORMAT(ht.dis_eff_dt,'dd/MM/yyyy', 'en-us')  as tarikh,hu.hr_discipline_desc,org.org_name,dis_catatan,ss1.op_perg_name from hr_discipline as ht  left join hr_post_his as ph on ht.dis_staff_no=ph.pos_staff_no  left join hr_staff_profile as sp on sp.stf_staff_no=ht.dis_staff_no left join Ref_hr_discipline as hu on hu.hr_discipline_Code=ht.dis_discipline_type_cd left join hr_organization org on org.org_gen_id=sp.str_curr_org_cd left join hr_organization_pern ss1 on ss1.op_perg_code=sp.stf_cur_sub_org where ph.pos_end_dt='9999-12-31' and ht.dis_staff_no='' ";
        }
    }

    void grid()
    {

        sql_info();
        //SqlCommand cmd2 = new SqlCommand("select ht.trn_dur,upper(sp.stf_name) as stf_name from hr_post_his as ph left join hr_training as ht on ht.dis_staff_no=ph.pos_staff_no  left join hr_staff_profile as sp on sp.stf_staff_no=ht.dis_staff_no where ht.dis_eff_dt>=DATEADD(day, DATEDIFF(day, 0, '2016-01-12'), 0) and ht.dis_eff_dt<=DATEADD(day, DATEDIFF(day, 0, '2016-01-16'), 0) and  ph.pos_end_dt='9999-12-31' and ht.dis_discipline_type_cd='' and ht.trn_type_cd='' and ht.dis_staff_no='0202020202' and ph.pos_gen_ID='s637456346' and ph.pos_dept_cd='01' and ph.pos_unit_cd='18'", con);
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
            GridView1.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
            if (dd_org.SelectedValue.Trim() != "" || dd_jabatan.SelectedValue.Trim() != "" || dd_unit.SelectedValue.Trim() != "" || tm_date.Text != "" || ta_date.Text != "" || Kaki_no.Text != "" || dd_kl.SelectedValue != "" || dd_jl.SelectedValue != "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
            Button2.Visible = false;
            Button3.Visible = false;
        }
        else
        {
            Button2.Visible = true;
            Button3.Visible = true;
            GridView1.DataSource = ds2;
            GridView1.DataBind();
        }
    }

    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        grid();
    }

    protected void rset_click(object sender, EventArgs e)
    {
        Response.Redirect("HR_LM_latihan.aspx");
    }

    protected void pdf_Click(object sender, EventArgs e)
    {
        try
        {
            sql_info();
            DataTable dt = new DataTable();
                dt = DBCon.Ora_Execute_table(val1);
                
                //Reset
                RptviwerStudent.Reset();

                List<DataRow> listResult = dt.AsEnumerable().ToList();
                listResult.Count();
                int countRow = 0;
                countRow = listResult.Count();

            if (countRow != 0)
            {
                // Label1.Text = "";
                //Label2.Text = "";
                ReportDataSource rds = new ReportDataSource("lapdisiplin", dt);

                RptviwerStudent.LocalReport.DataSources.Add(rds);

                //Path
                RptviwerStudent.LocalReport.ReportPath = "SUMBER_MANUSIA/lap_discipline.rdlc";
                //ToDate.Text = today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

                //Parameters
                ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("d1", tm_date.Text),
                     new ReportParameter("d2", ta_date.Text),
                     new ReportParameter("d3", ""),
                     new ReportParameter("d4", ""),
                     new ReportParameter("d5", "")
                     };


                RptviwerStudent.LocalReport.SetParameters(rptParams);
                RptviwerStudent.LocalReport.DisplayName = "LAPORAN_DISIPLIN_" + DateTime.Now.ToString("ddMMyyyy");
                //Refresh
                RptviwerStudent.LocalReport.Refresh();
                string filename = string.Format("{0}.{1}", "LAPORAN_DISIPLIN_" + DateTime.Now.ToString("ddMMyyyy") + "", "pdf");
                //}
                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;

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
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Tiada Rekod Dijumpai Dalam Julat Tarikh Yang Dimasukkan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
           
        }

        catch (Exception ex)
        {
            //Label2.Text = ex.ToString();
        }
        string script1 = "$(function () { $('[id*=GridView1]').prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({ 'responsive': true, 'sPaginationType': 'full_numbers'   }); });";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
    }

    protected void ExportToEXCEL(object sender, EventArgs e)
    {
        sql_info();

        DataTable dt = new DataTable();
        dt = DBCon.Ora_Execute_table(val1);
        
        if (dt.Rows.Count != 0)
        {


            StringBuilder builder = new StringBuilder();
            string strFileName = string.Format("{0}.{1}", "LAPORAN_KURSUS_" + DateTime.Now.ToString("ddMMyyyy") + "", "csv");
            builder.Append("ORGANISASI, WILAYAH,NO KAKITANGAN,NAMA KAKITANGAN,TAJUK, TINDAKAN, TARIKH" + Environment.NewLine);
            for (int k = 0; k <= (dt.Rows.Count - 1); k++)
            {

                builder.Append(dt.Rows[k]["org_name"].ToString() + " , " + dt.Rows[k]["op_perg_name"].ToString() + ", " + dt.Rows[k]["stf_staff_no"].ToString() + "," + dt.Rows[k]["stf_name"].ToString().Replace("'", "''") + ", " + dt.Rows[k]["dis_catatan"].ToString() + "," + dt.Rows[k]["hr_discipline_desc"].ToString() + "," + dt.Rows[k]["tarikh"].ToString() + Environment.NewLine);

            }
            Response.Clear();
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + strFileName);
            Response.Write(builder.ToString());
            Response.End();

        }
        else
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod tidak dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }

        string script1 = "$(function () { $('[id*=GridView1]').prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({ 'responsive': true, 'sPaginationType': 'full_numbers'   }); });";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
    }
    void instaudt()
    {
        string audcd = "040101";
        string auddec = "SELENGGARA MAKLUMAT CUTI";
        string usrid = Session["New"].ToString();
        string curdt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string Inssql = "insert into cmn_audit_trail(aud_crt_id,aud_crt_dt,aud_txn_cd,aud   _txn_desc) values ('" + usrid + "','" + curdt + "','" + audcd + "','" + auddec + "')";
        Status = DBCon.Ora_Execute_CommamdText(Inssql);
    }
    protected void srch_click(object sender, EventArgs e)
    {
        try
        {
            if (dd_org.SelectedValue.Trim() != "" || dd_jabatan.SelectedValue.Trim() != "" || dd_unit.SelectedValue.Trim() != "" || tm_date.Text != "" || ta_date.Text != "" || Kaki_no.Text != "" || dd_kl.SelectedValue != "" || dd_jl.SelectedValue != "")
            {
                grid();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                grid();
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