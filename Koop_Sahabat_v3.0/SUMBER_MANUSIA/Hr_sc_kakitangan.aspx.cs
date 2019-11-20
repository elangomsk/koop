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

public partial class Hr_sc_kakitangan : System.Web.UI.Page
{

    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    DataTable dt = new DataTable();
    string Status = string.Empty;
    string level, userid, stscd, abc1;
    string val1 = string.Empty, val2 = string.Empty, val3 = string.Empty, val4 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        app_language();
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                KatjawBind();
                OrgBind();
                Year();
                //JabaBind();
                grid();
                userid = Session["New"].ToString();
               

            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
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
    void app_language()
    {

        if (Session["New"] != null)
        {
            DataTable ste_set = new DataTable();
            ste_set = DBCon.Ora_Execute_table("select * from site_settings where ID IN ('1')");

            DataTable gt_lng = new DataTable();
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('510','448','511','496','497','1288','1397','512','14','15','61')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentUICulture;
            TextInfo txtinfo = culinfo.TextInfo;


              //h1_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
              //bb1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
              //bb2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());

              //h3_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());  

              //lbl1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower()); 
              lbl2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower()); 
              lbl3_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower()); 
              lbl4_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
              lbl5_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());

              Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
              Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
              Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    void KatjawBind()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select hr_kejaw_Code, UPPER(hr_kejaw_desc) as hr_kejaw_desc from Ref_hr_kategori_perjawatn  WHERE Status = 'A' order by hr_kejaw_desc";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_katjaw.DataSource = dt;
            DD_katjaw.DataTextField = "hr_kejaw_desc";
            DD_katjaw.DataValueField = "hr_kejaw_Code";
            DD_katjaw.DataBind();
            DD_katjaw.Items.Insert(0, new ListItem("--- PILIH ---", ""));
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


    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {

        System.Web.UI.WebControls.Label stf_no = (System.Web.UI.WebControls.Label)e.Row.FindControl("s_no");
        System.Web.UI.WebControls.Label org_cd = (System.Web.UI.WebControls.Label)e.Row.FindControl("lbl_orgcd");
        System.Web.UI.WebControls.TextBox carry_fwd = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("stfcfwd");
        System.Web.UI.WebControls.CheckBox chk_bx = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("chkSelect");
        System.Web.UI.WebControls.Label ser_date = (System.Web.UI.WebControls.Label)e.Row.FindControl("service_dt");
        System.Web.UI.WebControls.TextBox hosp_1 = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("cslday");
        System.Web.UI.WebControls.TextBox hosp_2 = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("cshos");


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (chk_assign_rkd.Checked == false)
            {
                float yer1 = 2010;
                float cfl1 = 10;
                float cfl2 = 7;
                string cur_yr = txt_tahun.SelectedValue;
                int pre_yr1 = (Convert.ToInt32(txt_tahun.SelectedValue) - 1);
                int pre_yr2 = (Convert.ToInt32(txt_tahun.SelectedValue) - 2);
                int pre_yr3 = (Convert.ToInt32(txt_tahun.SelectedValue) - 3);

                DataTable chk_gen_leave = new DataTable();
                chk_gen_leave = DBCon.Ora_Execute_table("select gen_leave_type_cd,gen_leave_day from hr_cmn_general_leave where gen_leave_type_cd IN ('04','05')");

                if(chk_gen_leave.Rows.Count != 0)
                {
                    for(int k = 0; k < chk_gen_leave.Rows.Count; k++)
                    {
                        //if(chk_gen_leave.Rows[k]["gen_leave_type_cd"].ToString() == "04")
                        //{
                        //    hosp_1.Text = chk_gen_leave.Rows[k]["gen_leave_day"].ToString();
                        //}
                        if (chk_gen_leave.Rows[k]["gen_leave_type_cd"].ToString() == "05")
                        {
                            hosp_2.Text = chk_gen_leave.Rows[k]["gen_leave_day"].ToString();
                        }
                    }
                }
                else
                {
                    //hosp_1.Text = "0";
                    hosp_2.Text = "0";
                }

                if (Convert.ToInt32(cur_yr) == Convert.ToUInt32(DateTime.Now.ToString("yyyy")))
                {
                    if (stf_no.Text.Trim() != "")
                    {
                        DataTable chk_tmph = new DataTable();
                        chk_tmph = DBCon.Ora_Execute_table("select b.lev_staff_no,ISNULL(m.[" + pre_yr3 + "],'0') as val1,ISNULL(a.[" + pre_yr2 + "],'0') as val2,ISNULL(b.[" + pre_yr1 + "],'0') as val3,(ISNULL(b.lev_balance_day,'0') -(ISNULL(m.[" + pre_yr3 + "],'0') - (case when ISNULL(a.[" + pre_yr2 + "],'0') = '0' then '0' else ISNULL(b.[" + pre_yr1 + "],'0') end + (case when ISNULL(m.[lev_taken_day],'0')='0' then '0' else ISNULL(a.[" + pre_yr2 + "],'0') end +  ISNULL(m.[lev_taken_day],'0'))))) as cfd_lev from (select lev_staff_no,lev_year,convert(float ,lev_taken_day) lev_taken_day,convert(float ,lev_balance_day) lev_balance_day from hr_leave where lev_staff_no='" + stf_no.Text + "' and lev_year='" + pre_yr1 + "' and lev_leave_type_cd='01') as SourceTable PIVOT(AVG([lev_taken_day])  FOR [lev_year] IN ([" + pre_yr1 + "])) as b left join (select lev_staff_no,lev_year,convert(float ,lev_taken_day) lev_taken_day from hr_leave where lev_staff_no='" + stf_no.Text + "' and lev_year='" + pre_yr2 + "' and lev_leave_type_cd='01') as SourceTable PIVOT(AVG([lev_taken_day])  FOR [lev_year] IN ([" + pre_yr2 + "])) as a on b.lev_staff_no=a.lev_staff_no left join (select lev_staff_no,lev_year,convert(float ,lev_taken_day) lev_taken_day,convert(float ,lev_balance_day) lev_balance_day from hr_leave where lev_staff_no='" + stf_no.Text + "' and lev_year='" + pre_yr3 + "' and lev_leave_type_cd='01') as SourceTable PIVOT(AVG([lev_balance_day])  FOR [lev_year] IN ([" + pre_yr3 + "])) as m on m.lev_staff_no=a.lev_staff_no");
                        string ss1 = string.Empty;
                        if (chk_tmph.Rows.Count == 0)
                        {
                            carry_fwd.Text = "0";
                        }
                        else
                        {
                            //if(float.Parse(ser_date.Text) < yer1)
                            //{
                            //    if(float.Parse(chk_tmph.Rows[0]["cfd_lev"].ToString()) > cfl1)
                            //    {
                            //        carry_fwd.Text = "10";
                            //    }
                            //    else
                            //    {
                            //        carry_fwd.Text = chk_tmph.Rows[0]["cfd_lev"].ToString();
                            //    }
                            //}
                            //else if (float.Parse(ser_date.Text) > yer1)
                            //{
                            //    if (float.Parse(chk_tmph.Rows[0]["cfd_lev"].ToString()) > cfl2)
                            //    {
                            //        carry_fwd.Text = "7";
                            //    }
                            //    else
                            //    {
                                    carry_fwd.Text = chk_tmph.Rows[0]["cfd_lev"].ToString();
                            //    }
                            //}

                        }

                    }
                }
                DataTable chk_application = new DataTable();
                chk_application = DBCon.Ora_Execute_table("select * from hr_leave_application where lap_staff_no='"+ stf_no.Text + "' and year(lap_leave_start_dt) = '"+ DateTime.Now.Year.ToString() + "' and ISNULL(lap_endorse_sts_cd,'') != ''");
                if (chk_application.Rows.Count > 0)
                {
                    chk_bx.Visible = false;
                }
                else
                {
                    chk_bx.Visible = true;
                }
                //chk_bx.Visible = true;

            }
            else
            {
                //chk_bx.Visible = false;
                
            }
        }
    }

    void grid()
    {
        string cur_yr = txt_tahun.SelectedValue;
        string sval1 = string.Empty;
        DataTable sel_organ = new DataTable();
        sel_organ = DBCon.Ora_Execute_table("SELECT ISNULL(STUFF ((select ',' + lev_staff_no from hr_leave where lev_year='" + cur_yr + "' group by lev_staff_no FOR XML PATH ('')  ),1,1,''), '')  as scode");
        if( Convert.ToInt32(txt_tahun.SelectedValue) < Convert.ToUInt32(DateTime.Now.ToString("yyyy")))
        {
            chk_assign_rkd.Checked = true;
        }

        if (sel_organ.Rows.Count > 0)
        { 
        if (sel_organ.Rows[0]["scode"].ToString() != "")
            {
                if (chk_assign_rkd.Checked == false)
                {
                    sval1 = "and stf_staff_no Not IN ('" + sel_organ.Rows[0]["scode"].ToString().Replace(",", "','") + "')";
                }
                else
                {
                    sval1 = "and stf_staff_no IN ('" + sel_organ.Rows[0]["scode"].ToString().Replace(",", "','") + "')";
                }

            }
        }

        string sqry1 = string.Empty;
        if (dd_org.SelectedValue.Trim() != "" && dd_org_pen.SelectedValue.Trim() == "" && dd_jabatan.SelectedValue.Trim() == "" && DD_katjaw.SelectedValue.Trim() == "" && T_khidmat.Text == "")
        {
            val1 = "and str_curr_org_cd='" + dd_org.SelectedValue.Trim() + "' "+ sval1 + "";

        }
        else if (dd_org.SelectedValue.Trim() == "" && dd_org_pen.SelectedValue.Trim() == "" && dd_jabatan.SelectedValue.Trim() == "" && DD_katjaw.SelectedValue.Trim() != "" && T_khidmat.Text == "")
        {
            val1 = "and stf_curr_job_cat_cd='" + DD_katjaw.SelectedValue.Trim() + "' " + sval1 + "";

        }
        else if (dd_org.SelectedValue.Trim() == "" && dd_org_pen.SelectedValue.Trim() == "" && dd_jabatan.SelectedValue.Trim() == "" && DD_katjaw.SelectedValue.Trim() == "" && T_khidmat.Text != "")
        {
            val1 = "and round(datediff(m,stf_service_start_dt,GETDATE())/12,2)='" + T_khidmat.Text + "' " + sval1 + "";

        }
        else if (dd_org.SelectedValue.Trim() != "" && dd_org_pen.SelectedValue.Trim() != "" && dd_jabatan.SelectedValue.Trim() == "" && DD_katjaw.SelectedValue.Trim() == "" && T_khidmat.Text == "")
        {
            val1 = "and str_curr_org_cd='" + dd_org.SelectedValue.Trim() + "' and stf_cur_sub_org = '" + dd_org_pen.SelectedValue.Trim() + "' " + sval1 + "";

        }

        else if (dd_org.SelectedValue.Trim() != "" && dd_org_pen.SelectedValue.Trim() != "" && dd_jabatan.SelectedValue.Trim() != "" && DD_katjaw.SelectedValue.Trim() == "" && T_khidmat.Text == "")
        {
            val1 = "and str_curr_org_cd='" + dd_org.SelectedValue.Trim() + "' and stf_cur_sub_org = '" + dd_org_pen.SelectedValue.Trim() + "' and stf_curr_dept_cd='" + dd_jabatan.SelectedValue.Trim() + "' " + sval1 + "";

        }
        else if (dd_org.SelectedValue.Trim() != "" && dd_org_pen.SelectedValue.Trim() != "" && dd_jabatan.SelectedValue.Trim() != "" && DD_katjaw.SelectedValue.Trim() != "" && T_khidmat.Text == "")
        {
            val1 = "and str_curr_org_cd='" + dd_org.SelectedValue.Trim() + "' and stf_cur_sub_org = '" + dd_org_pen.SelectedValue.Trim() + "' and stf_curr_dept_cd='" + dd_jabatan.SelectedValue.Trim() + "' and stf_curr_job_cat_cd='" + DD_katjaw.SelectedValue.Trim() + "' " + sval1 + "";

        }
        else if (dd_org.SelectedValue.Trim() != "" && dd_org_pen.SelectedValue.Trim() != "" && dd_jabatan.SelectedValue.Trim() != "" && DD_katjaw.SelectedValue.Trim() != "" && T_khidmat.Text != "")
        {
            val1 = "and str_curr_org_cd='" + dd_org.SelectedValue.Trim() + "' and stf_cur_sub_org = '" + dd_org_pen.SelectedValue.Trim() + "' and stf_curr_dept_cd='" + dd_jabatan.SelectedValue.Trim() + "' and stf_curr_job_cat_cd='" + DD_katjaw.SelectedValue.Trim() + "' and round(datediff(m,stf_service_start_dt,GETDATE())/12,2)='" + T_khidmat.Text + "' " + sval1 + "";

        }
        else if (dd_org.SelectedValue.Trim() != "" && dd_org_pen.SelectedValue.Trim() == "" && dd_jabatan.SelectedValue.Trim() == "" && DD_katjaw.SelectedValue.Trim() != "" && T_khidmat.Text == "")
        {
            val1 = "and str_curr_org_cd='" + dd_org.SelectedValue.Trim() + "' and stf_curr_job_cat_cd='" + DD_katjaw.SelectedValue.Trim() + "' " + sval1 + "";

        }
        else if (dd_org.SelectedValue.Trim() != "" && dd_org_pen.SelectedValue.Trim() == "" && dd_jabatan.SelectedValue.Trim() == "" && DD_katjaw.SelectedValue.Trim() == "" && T_khidmat.Text != "")
        {
            val1 = "and str_curr_org_cd='" + dd_org.SelectedValue.Trim() + "' and round(datediff(m,stf_service_start_dt,GETDATE())/12,2)='" + T_khidmat.Text + "' " + sval1 + "";

        }
        else if (dd_org.SelectedValue.Trim() != "" && dd_org_pen.SelectedValue.Trim() != "" && dd_jabatan.SelectedValue.Trim() == "" && DD_katjaw.SelectedValue.Trim() != "" && T_khidmat.Text == "")
        {
            val1 = "and str_curr_org_cd='" + dd_org.SelectedValue.Trim() + "' and stf_cur_sub_org = '" + dd_org_pen.SelectedValue.Trim() + "' and stf_curr_job_cat_cd='" + DD_katjaw.SelectedValue.Trim() + "' " + sval1 + "";

        }
        else if (dd_org.SelectedValue.Trim() != "" && dd_org_pen.SelectedValue.Trim() != "" && dd_jabatan.SelectedValue.Trim() == "" && DD_katjaw.SelectedValue.Trim() == "" && T_khidmat.Text != "")
        {
            val1 = "and str_curr_org_cd='" + dd_org.SelectedValue.Trim() + "' and stf_cur_sub_org = '" + dd_org_pen.SelectedValue.Trim() + "' and round(datediff(m,stf_service_start_dt,GETDATE())/12,2)='" + T_khidmat.Text + "' " + sval1 + "";

        }
        else if (dd_org.SelectedValue.Trim() != "" && dd_org_pen.SelectedValue.Trim() == "" && dd_jabatan.SelectedValue.Trim() == "" && DD_katjaw.SelectedValue.Trim() != "" && T_khidmat.Text != "")
        {
            val1 = "and str_curr_org_cd='" + dd_org.SelectedValue.Trim() + "' and stf_curr_job_cat_cd='" + DD_katjaw.SelectedValue.Trim() + "' and round(datediff(m,stf_service_start_dt,GETDATE())/12,2)='" + T_khidmat.Text + "' " + sval1 + "";

        }
        else if (dd_org.SelectedValue.Trim() != "" && dd_org_pen.SelectedValue.Trim() != "" && dd_jabatan.SelectedValue.Trim() != "" && DD_katjaw.SelectedValue.Trim() == "" && T_khidmat.Text != "")
        {
            val1 = "and str_curr_org_cd='" + dd_org.SelectedValue.Trim() + "' and stf_cur_sub_org = '" + dd_org_pen.SelectedValue.Trim() + "' and stf_curr_dept_cd='" + dd_jabatan.SelectedValue.Trim() + "' and round(datediff(m,stf_service_start_dt,GETDATE())/12,2)='" + T_khidmat.Text + "' " + sval1 + "";

        }
        else
        {
            val1 = " and str_curr_org_cd='00' " + sval1 + "";
        }
     

       
        if(chk_assign_rkd.Checked == true)
        {
            if (DateTime.Now.Year.ToString() == txt_tahun.SelectedValue)
            {
                Button3.Text = "Kemaskini";
                Button3.Visible = true;
            }
            else
            {
                Button3.Text = "Simpan";
                Button3.Visible = false;
            }
            sqry1 = "select a.lev_staff_no as stf_staff_no,'' service_dt,hr_jaw_desc,a.str_curr_org_cd,a.stf_name,a.stf_curr_post_cd,a.stf_curr_job_cat_cd,a.hr_jaba_desc,a.hr_gred_desc,a.hr_kate_desc,a.hr_traf_desc,a.tk_y,a.tk_m,a.d_yr,a.d_mth,a.[01] as ct_lday,b.[04] as cs_lday,c.[05] as cs_hos,a.lev_carry_fwd_day as stf_carry_fwd_lv from (select lev_staff_no,sp.str_curr_org_cd,sp.stf_name,sp.stf_curr_post_cd,sp.stf_curr_job_cat_cd,rj.hr_jaba_desc,rg.hr_gred_desc,rkj.hr_kate_desc,rsj.hr_traf_desc,lev_year,lev_leave_type_cd as [val2],convert(float, lev_carry_fwd_day) as lev_carry_fwd_day,convert(float, lev_entitle_day) as [val3],DATEDIFF( YEAR, stf_service_start_dt, GETDATE()) as tk_y,DATEDIFF( month, stf_service_start_dt, GETDATE()) as tk_m,round(datediff(m,stf_service_start_dt,GETDATE())/12,2)  as d_yr,round(datediff(m,stf_service_start_dt,GETDATE())%12,2) as d_mth,hr_jaw_desc from hr_leave left join hr_staff_profile as sp on sp.stf_staff_no=lev_staff_no left join Ref_hr_jabatan as rj on rj.hr_jaba_Code=sp.stf_curr_dept_cd left join Ref_hr_gred as rg on rg.hr_gred_Code=sp.stf_curr_grade_cd left join Ref_hr_penj_kategori as rkj on rkj.hr_kate_Code=sp.stf_curr_job_cat_cd left join Ref_hr_penj_traf as rsj on rsj.hr_traf_Code=sp.stf_curr_job_sts_cd left join ref_hr_jawatan hj on hj.hr_jaw_Code=stf_curr_post_cd where lev_year='" + cur_yr+"' and lev_leave_type_cd='01' " + val1 + ") as SourceTable PIVOT(AVG([val3])  FOR [val2] IN ([01])) as a left join (select lev_staff_no,lev_year,lev_leave_type_cd as [val2],convert(float, lev_entitle_day) as [val3] from hr_leave where lev_year='" + cur_yr + "' and lev_leave_type_cd='04') as SourceTable PIVOT(AVG([val3])  FOR [val2] IN ([04])) as b on b.lev_staff_no=a.lev_staff_no left join (select lev_staff_no,lev_year,lev_leave_type_cd as [val2],convert(float, lev_entitle_day) as [val3] from hr_leave where lev_year='" + cur_yr + "' and lev_leave_type_cd='05') as SourceTable PIVOT(AVG([val3])  FOR [val2] IN ([05])) as c on c.lev_staff_no=a.lev_staff_no left join (select lev_staff_no,lev_year,lev_leave_type_cd as [val2],convert(float, lev_entitle_day) as [val3] from hr_leave where lev_year='"+ cur_yr +"' and lev_leave_type_cd='17') as SourceTable PIVOT(AVG([val3])  FOR [val2] IN ([17])) as d on d.lev_staff_no=a.lev_staff_no";
        }
        else
        {
            Button3.Text = "Simpan";
            Button3.Visible = true;
            //sqry1 = "select a.stf_staff_no,a.str_curr_org_cd,a.stf_name,a.stf_curr_post_cd,a.stf_curr_job_cat_cd,a.hr_jaba_desc,a.hr_gred_desc,a.hr_kejaw_desc,a.hr_traf_desc,ISNULL(a.stf_carry_fwd_lv,'0') stf_carry_fwd_lv,a.tk_y,a.tk_m,a.d_yr,a.d_mth,ISNULL(CASE WHEN a.d_yr < 1 THEN convert(int,((select ann_leave_day from hr_cmn_annual_leave where ann_post_cd=a.stf_curr_post_cd and ann_post_cat_cd=a.stf_curr_job_cat_cd and ((a.d_yr) between (ann_min_service) And (ann_max_service))) * a.tk_m / 12)) ELSE (select ann_leave_day from hr_cmn_annual_leave where ann_post_cd=a.stf_curr_post_cd and ann_post_cat_cd=a.stf_curr_job_cat_cd and ((a.d_yr) between (ann_min_service) And (ann_max_service))) END,'0') AS ct_lday,ISNULL(convert(int, (select out_leave_day from hr_cmn_outpatient_leave where ((a.d_yr) between (out_min_yr) And (out_max_yr)))),'0') cs_lday,ISNULL((60 - convert(int, (select out_leave_day from hr_cmn_outpatient_leave where ((a.d_yr) between (out_min_yr) And (out_max_yr))))),'0') as cs_hos from (select sp.stf_service_start_dt,sp.stf_staff_no,sp.str_curr_org_cd,sp.stf_name,rj.hr_jaba_desc,rg.hr_gred_desc,rkj.hr_kejaw_desc,rsj.hr_traf_desc,sp.stf_carry_fwd_lv,DATEDIFF( YEAR, stf_service_start_dt, GETDATE()) as tk_y,DATEDIFF( month, stf_service_start_dt, GETDATE()) as tk_m,round(datediff(m,stf_service_start_dt,GETDATE())/12,2)  as d_yr,round(datediff(m,stf_service_start_dt,GETDATE())%12,2) as d_mth,stf_curr_post_cd,stf_curr_job_cat_cd,stf_curr_dept_cd from hr_staff_profile as sp left join Ref_hr_jabatan as rj on rj.hr_jaba_Code=sp.stf_curr_dept_cd left join Ref_hr_gred as rg on rg.hr_gred_Code=sp.stf_curr_grade_cd left join Ref_hr_kategori_perjawatn as rkj on rkj.hr_kejaw_Code=sp.stf_curr_job_cat_cd left join Ref_hr_penj_traf as rsj on rsj.hr_traf_Code=sp.stf_curr_job_sts_cd where stf_service_end_dt = '9999-12-31' " + val1 + ") as a left join hr_cmn_annual_leave as hcal on hcal.ann_post_cd=a.stf_curr_post_cd and hcal.ann_post_cat_cd=a.stf_curr_job_cat_cd and hcal.ann_min_service <= a.d_yr and hcal.ann_max_service >= a.d_yr left join hr_cmn_outpatient_leave as hcol on hcol.out_min_yr <= a.d_yr and hcol.out_max_yr >= a.d_yr";
            sqry1 = "select distinct Year(stf_service_start_dt) service_dt,hj.hr_jaw_desc,a.stf_staff_no,a.str_curr_org_cd,a.stf_name,a.stf_curr_post_cd,a.stf_curr_job_cat_cd,a.hr_jaba_desc,a.hr_gred_desc,a.hr_kate_desc,a.hr_traf_desc,ISNULL(a.stf_carry_fwd_lv,'0') stf_carry_fwd_lv,a.tk_y,a.tk_m,a.d_yr, a.d_mth,ISNULL( (select ann_leave_day from hr_cmn_annual_leave where ann_leave_type_cd='01' and (ann_min_service <= a.d_yr and ann_max_service > a.d_yr)),'0') AS ct_lday,ISNULL( (select ann_leave_day from hr_cmn_annual_leave where ann_leave_type_cd='04' and (ann_min_service <= a.d_yr and ann_max_service > a.d_yr)),'0') cs_lday,ISNULL((60 - convert(int, (select out_leave_day from hr_cmn_outpatient_leave where ((a.d_yr) between  (out_min_yr) And (out_max_yr))))),'0') as cs_hos from (select sp.stf_service_start_dt,sp.stf_staff_no,sp.str_curr_org_cd,sp.stf_name,rj.hr_jaba_desc,rg.hr_gred_desc,rkj.hr_kate_desc,rsj.hr_traf_desc,sp.stf_carry_fwd_lv,DATEDIFF( YEAR, stf_service_start_dt, GETDATE()) as tk_y,DATEDIFF( month, stf_service_start_dt, GETDATE()) as tk_m,round(datediff(m,stf_service_start_dt,GETDATE())/12,2)  as d_yr,round(datediff(m,stf_service_start_dt,GETDATE())%12,2) as d_mth,stf_curr_post_cd,stf_curr_job_cat_cd,stf_curr_dept_cd from hr_staff_profile as sp left join Ref_hr_jabatan as rj on rj.hr_jaba_Code=sp.stf_curr_dept_cd left join Ref_hr_gred as rg on rg.hr_gred_Code=sp.stf_curr_grade_cd left join Ref_hr_penj_kategori as rkj on rkj.hr_kate_Code=sp.stf_curr_job_cat_cd left join Ref_hr_penj_traf as rsj on rsj.hr_traf_Code=sp.stf_curr_job_sts_cd where stf_service_end_dt>='" + DateTime.Now.ToString("yyyy-MM-dd") + "' and stf_service_end_dt <= '9999-12-31' " + val1 + " ) as a left join ref_hr_jawatan hj on hj.hr_jaw_Code=a.stf_curr_post_cd";
        }


        SqlCommand cmd2 = new SqlCommand(sqry1, con);
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
            GridView1.Rows[0].Cells[0].Text = "<strong><center>Maklumat Carian Tidak Dijumpai</center></strong>";
        }
        else
        {
            GridView1.DataSource = ds2;
            GridView1.DataBind();
        }
    }

  
    protected void OnCheckedChanged_ann(object sender, EventArgs e)
    {
        bool isUpdateVisible = false;
        System.Web.UI.WebControls.CheckBox chk = (sender as System.Web.UI.WebControls.CheckBox);
        if (chk.ID == "chkAll")
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    row.Cells[12].Controls.OfType<System.Web.UI.WebControls.CheckBox>().FirstOrDefault().Checked = chk.Checked;
                }
            }
        }
        System.Web.UI.WebControls.CheckBox chkAll = (GridView1.HeaderRow.FindControl("chkAll") as System.Web.UI.WebControls.CheckBox);
        chkAll.Checked = true;
        foreach (GridViewRow row in GridView1.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                bool isChecked = row.Cells[12].Controls.OfType<System.Web.UI.WebControls.CheckBox>().FirstOrDefault().Checked;
                for (int i = 1; i < row.Cells.Count; i++)
                {
                    if (isChecked && !isUpdateVisible)
                    {
                        isUpdateVisible = true;
                    }
                    if (!isChecked)
                    {
                        chkAll.Checked = false;

                    }
                }
            }
        }

    }
    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        grid();
    }


    protected void srch_click(object sender, EventArgs e)
    {
        try
        {
            if (dd_org.SelectedValue.Trim() != "" || dd_org_pen.SelectedValue.Trim() != "" || dd_jabatan.SelectedValue.Trim() != "" || DD_katjaw.SelectedValue.Trim() != "" || T_khidmat.Text != "")
            {
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

    protected void clk_insert(object sender, EventArgs e)
    {
        try
        {
            //int counter = 0;
            if (dd_org.SelectedValue.Trim() != "" || dd_org_pen.SelectedValue.Trim() != "" || dd_jabatan.SelectedValue.Trim() != "" || DD_katjaw.SelectedValue.Trim() != "" || T_khidmat.Text != "")
            {
                //foreach (GridViewRow gvrow1 in GridView1.Rows)
                //{
                //    counter = counter + 1;
                //}
                string rcount = string.Empty;
                int counter = 0;
                foreach (GridViewRow gvrow in GridView1.Rows)
                {
                    var checkbox = gvrow.FindControl("chkSelect") as System.Web.UI.WebControls.CheckBox;
                    if (checkbox.Checked)
                    {
                        counter++;
                    }
                    rcount = counter.ToString();
                }
                if (counter != 0)
                {
                    string d1 = string.Empty, d2 = string.Empty, d3 = string.Empty, d4 = string.Empty, d5 = string.Empty;
                    foreach (GridViewRow gvrow in GridView1.Rows)
                    {
                        var a_no = (System.Web.UI.WebControls.Label)gvrow.FindControl("s_no");
                        var d1_s = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("stfcfwd");
                        var d2_s = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("cslday");
                        var d3_s = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("cshos");
                        var d4_s = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("ctlday");
                        var ogcd = (System.Web.UI.WebControls.Label)gvrow.FindControl("org_cd");
                        var checkbox = gvrow.FindControl("chkSelect") as System.Web.UI.WebControls.CheckBox;
                        if (checkbox.Checked == true)
                        {
                            string cyr = DateTime.Now.ToString("yyyy");
                            if (d1_s.Text == "")
                            {
                                d1 = "0";
                            }
                            else
                            {
                                d1 = d1_s.Text;
                            }

                            if (d2_s.Text == "")
                            {
                                d2 = "0";
                            }
                            else
                            {
                                d2 = d2_s.Text;
                            }

                            if (d3_s.Text == "")
                            {
                                d3 = "0";
                            }
                            else
                            {
                                d3 = d3_s.Text;
                            }

                            if (d4_s.Text == "")
                            {
                                d4 = "0";
                            }
                            else
                            {
                                d4 = d4_s.Text;
                            }


                            DBCon.Execute_CommamdText("UPDATE hr_staff_profile set stf_carry_fwd_lv='" + d1 + "' ,stf_bal_annual_lv='" + d4 + "',stf_bal_outpatient_lv='" + d2 + "',stf_bal_inpatient_lv='" + d3 + "',stf_upd_id='" + Session["New"].ToString() + "',stf_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "' where stf_staff_no='" + a_no.Text + "'");

                            // cuti tahun

                            DataTable select_annleave = new DataTable();
                            select_annleave = DBCon.Ora_Execute_table("select * from hr_leave where lev_staff_no='" + a_no.Text + "' and lev_org_id='" + ogcd.Text + "' and lev_leave_type_cd='01' and lev_year='" + cyr + "'");

                            string ss1 = (double.Parse(d4) + double.Parse(d1)).ToString();
                            if (select_annleave.Rows.Count != 0)
                            {
                                DBCon.Execute_CommamdText("UPDATE hr_leave set lev_carry_fwd_day='" + d1 + "',lev_entitle_day='" + d4 + "',lev_balance_day='" + ss1 + "',lev_upd_id='" + Session["New"].ToString() + "',lev_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "' where lev_staff_no='" + a_no.Text + "' and lev_org_id='" + ogcd.Text + "' and lev_leave_type_cd='01' and lev_year='" + cyr + "'");
                            }
                            else
                            {
                                DBCon.Execute_CommamdText("INSERT INTO hr_leave (lev_org_id,lev_staff_no,lev_year,lev_leave_type_cd,lev_carry_fwd_day,lev_entitle_day,lev_taken_day,lev_balance_day,lev_crt_id,lev_crt_dt) VALUES ('" + ogcd.Text + "','" + a_no.Text + "','" + cyr + "','01','" + d1 + "','" + d4 + "','0','" + ss1 + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "')");
                            }

                            DataTable select_opleave = new DataTable();
                            select_opleave = DBCon.Ora_Execute_table("select * from hr_leave where lev_staff_no='" + a_no.Text + "' and lev_org_id='" + ogcd.Text + "' and lev_leave_type_cd='04' and lev_year='" + cyr + "'");
                            if (select_opleave.Rows.Count != 0)
                            {
                                DBCon.Execute_CommamdText("UPDATE hr_leave set lev_entitle_day='" + d2 + "',lev_upd_id='" + Session["New"].ToString() + "',lev_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "' where lev_staff_no='" + a_no.Text + "' and lev_org_id='" + ogcd.Text + "' and lev_leave_type_cd='04'");
                            }
                            else
                            {
                                DBCon.Execute_CommamdText("INSERT INTO hr_leave (lev_org_id,lev_staff_no,lev_year,lev_leave_type_cd,lev_carry_fwd_day,lev_entitle_day,lev_taken_day,lev_balance_day,lev_crt_id,lev_crt_dt) VALUES ('" + ogcd.Text + "','" + a_no.Text + "','" + cyr + "','04','0','" + d2 + "','0','" + d2 + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "')");
                            }

                            //DataTable select_ipleave = new DataTable();
                            //select_ipleave = DBCon.Ora_Execute_table("select * from hr_leave where lev_staff_no='" + a_no.Text + "' and lev_org_id='" + ogcd.Text + "' and lev_leave_type_cd='05' and lev_year='" + cyr + "'");
                            //if (select_ipleave.Rows.Count != 0)
                            //{
                            //    DBCon.Execute_CommamdText("UPDATE hr_leave set lev_entitle_day='" + d3 + "',lev_upd_id='" + Session["New"].ToString() + "',lev_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "' where lev_staff_no='" + a_no.Text + "' and lev_org_id='" + ogcd.Text + "' and lev_leave_type_cd='05' and lev_year='" + cyr + "'");
                            //}
                            //else
                            //{
                            //    DBCon.Execute_CommamdText("INSERT INTO hr_leave (lev_org_id,lev_staff_no,lev_year,lev_leave_type_cd,lev_carry_fwd_day,lev_entitle_day,lev_taken_day,lev_balance_day,lev_crt_id,lev_crt_dt) VALUES ('" + ogcd.Text + "','" + a_no.Text + "','" + cyr + "','05','0','" + d3 + "','0','" + d3 + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "')");
                            //}
                            
                        }
                    }
                    service.audit_trail("P0058", "Simpan", dd_org.SelectedItem.Text, txt_tahun.SelectedItem.Text);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    grid();
                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                grid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Issue',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }





    protected void Button5_Click(object sender, EventArgs e)
    {
        Response.Redirect("../SUMBER_MANUSIA/Hr_sc_kakitangan.aspx");
    }

  


}