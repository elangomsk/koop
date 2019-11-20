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
using System.Threading;
using System;
using System.Web;

public partial class HR_Dashboard : System.Web.UI.Page
{
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    DataTable get_lv_det = new DataTable();
    StudentWebService service = new StudentWebService();
    DataTable ddicno1_stf = new DataTable();
    DataTable ddicno1 = new DataTable();
    DataTable tot_salary_wtng = new DataTable();
    DataTable ddicno1_role = new DataTable();
    DataTable get_role_no = new DataTable();
    DataTable get_role_no1 = new DataTable();
    string checkimage = string.Empty;
    string fileName = string.Empty;
    string strQuery = string.Empty;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                view_details();
                view_dashboard();
            }
            else
            {
                Response.Redirect("KSAIMB_Login.aspx");
            }
        }

    }

    void view_details()
    {

        CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
        TextInfo txtinfo = culinfo.TextInfo;
        string user = Session["New"].ToString();
        ddicno1 = DBCon.Ora_Execute_table("select * from KK_User_Login  where KK_userid='" + Session["New"].ToString() + "'");
        if (ddicno1.Rows[0]["KK_userid"].ToString() == "")
        {
            //lbluname.Text = Session["New"].ToString();
            lbluname1.Text = Session["New"].ToString().ToUpper();
        }
        else
        {
            //lbluname.Text = ddicno1.Rows[0]["KK_username"].ToString();
            lbluname1.Text = ddicno1.Rows[0]["KK_username"].ToString().ToUpper();
        }

        checkimage = ddicno1.Rows[0]["user_img"].ToString();


        string fullPath = Request.Url.AbsolutePath;
        fileName = System.IO.Path.GetFileName(fullPath);
    }



    void view_dashboard()
    {
        string year = DateTime.Now.ToString("yyyy");
        CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
        TextInfo txtinfo = culinfo.TextInfo;
        ddicno1_stf = DBCon.Ora_Execute_table("select * from KK_User_Login where KK_userid='" + Session["New"].ToString() + "'");
        tot_salary_wtng = DBCon.Ora_Execute_table("select a.wait_sal,b.wait_clm,c.wait_lap from (select '0' as sv1,count(*) wait_sal from hr_income where inc_app_sts='P' and inc_sts='0' ) as a full outer join (select '0' as sv1,count(*) wait_clm from hr_claim_new where clm_app_sts='P') as b on b.sv1=a.sv1 full outer join (select '0' as sv1,count(*) wait_lap from hr_leave_application where lap_approve_sts_cd='00' and ISNULL(lap_endorse_sts_cd,'') ='') as c on c.sv1=b.sv1");
        ddicno1_role = DBCon.Ora_Execute_table("select * from KK_Role_skrins where Role_id IN ('" + ddicno1_stf.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "') and psub_skrin_id IN ('P0071','P0207','P0200','P0211','P0062')");
        get_role_no = DBCon.Ora_Execute_table("select * from (select * from KK_PID_Kumpulan where KK_kumpulan_id IN ('') ) as a where KK_kumpulan_id IN ('" + ddicno1_stf.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");
        if (get_role_no.Rows.Count == 0)
        {
            strQuery = "select a1.lev_leave_type_cd lev_leave_type_cd,a1.lev_staff_no,a1.hr_jenis_desc,case when a1.hr_jenis_desc = 'cuti tahunan' then '01' when a1.hr_jenis_desc = 'cuti sakit' then '02' when a1.hr_jenis_desc = 'cuti ganti' then '04'  "
                 + " when a1.hr_jenis_desc = 'cuti bawa hadapan' then '04' end as sno, a1.a,cast(a1.c as float) as c,a1.b, a1.ab,((ISNULL(b1.e,'0')) + ISNULL(c2.pre_e,'0')) as e,a1.d,"
                 + " ((((convert(float,(a1.c)) + a1.a) - (convert(float,(a1.d)) + ISNULL(b1.e, '0')))) - ISNULL(c2.pre_e, '0')) as res,ISNULL(c2.pre_e, '0') as pre_bal from (select  "
                 + " lev_staff_no,lev_leave_type_cd,hr_jenis_desc,lev_carry_fwd_day as a,lev_entitle_day as c,convert(float,(((convert(float,lev_entitle_day) / 12) *  DATEPART(month, GETDATE())))) as b,"
                 + " (lev_carry_fwd_day + convert(float,(((convert(float,lev_entitle_day)))))) as ab,lev_taken_day as d,lev_balance_day from hr_staff_profile as SP left join hr_leave as LV on LV.lev_staff_no= "
                 + " SP.stf_staff_no and LV.lev_org_id=sp.str_curr_org_cd left join Ref_hr_jenis_cuti as JC on JC.hr_jenis_Code=LV.lev_leave_type_cd where stf_staff_no='" + Session["New"].ToString() + "' AND lev_year='" + year + "') as a1 "
                 + " full outer join(select lap_staff_no, lap_leave_type_cd,sum(convert(float,lap_leave_day)) as e from hr_leave_application where lap_staff_no='" + Session["New"].ToString() + "' and lap_cancel_ind='N' and "
                 + " lap_leave_type_cd IN('01','04') and lap_approve_sts_cd NOT IN ('04','01') and  ISNULL(lap_endorse_sts_cd,'') = '' group by lap_staff_no,lap_leave_type_cd) as b1 on b1.lap_staff_no="
                 + " a1.lev_staff_no and b1.lap_leave_type_cd=a1.lev_leave_type_cd left join(select lap_staff_no,lap_pre_type_cd,sum(convert(float,lap_pre_balance)) as pre_e from  "
                 + " hr_leave_application where lap_staff_no ='" + Session["New"].ToString() + "' and lap_cancel_ind='N' and lap_leave_type_cd IN('04','05') and lap_approve_sts_cd NOT IN ('04','01') and ISNULL(lap_endorse_sts_cd,'') = '' "
                 + "  group by lap_staff_no,lap_pre_type_cd ) as c2 on  c2.lap_staff_no=a1.lev_staff_no and c2.lap_pre_type_cd = a1.lev_leave_type_cd "

                 + "   union all select a1.col_cat_cd lev_leave_type_cd,a1.col_staff_no,a1.hr_jenis_desc,case when a1.hr_jenis_desc = 'cuti tahunan' then '01' when a1.hr_jenis_desc = 'cuti sakit' then '02' when a1.hr_jenis_desc = 'cuti ganti' then '04' "
                 + "  when a1.hr_jenis_desc = 'cuti bawa hadapan' then '04' end as sno, a1.a,cast(a1.c as float) as c,a1.b, a1.ab,((ISNULL(b1.e, '0')) + ISNULL(c2.pre_e, '0')) as e,a1.d, "
                 + "  ((((convert(float, (a1.c)) + a1.a) - (convert(float, (a1.d)) + ISNULL(b1.e, '0')))) - ISNULL(c2.pre_e, '0')) as res,ISNULL(c2.pre_e, '0') as pre_bal from(select "
                 + " col_staff_no, col_cat_cd, hr_jenis_desc, '0' as a, sum(convert(float,col_entitle_day)) as c, sum(convert(float, (((convert(float, col_entitle_day) / 12) * DATEPART(month, GETDATE()))))) as b,  "
                 + "  sum(('0' + convert(float, (((convert(float, col_entitle_day))))))) as ab, sum(convert(float, col_taken_day)) as d, sum(convert(float, col_balance_day)) col_balance_day from hr_staff_profile as SP left join hr_com_leave as LV on LV.col_staff_no = "
                 + "  SP.stf_staff_no and LV.col_org_id = sp.str_curr_org_cd left join Ref_hr_jenis_cuti as JC on JC.hr_jenis_Code = LV.col_cat_cd where stf_staff_no = '" + Session["New"].ToString() + "' AND col_year = '" + year + "'  and col_end_dt >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' group by col_staff_no,col_cat_cd,JC.hr_jenis_desc) as a1 "
                 + "  full outer join(select lap_staff_no, lap_leave_type_cd, sum(convert(float, lap_leave_day)) as e from hr_leave_application where lap_staff_no = '" + Session["New"].ToString() + "' and lap_cancel_ind = 'N' and "
                 + "  lap_leave_type_cd IN('12') and lap_approve_sts_cd NOT IN('04', '01') and  ISNULL(lap_endorse_sts_cd, '') = '' group by lap_staff_no, lap_leave_type_cd) as b1 on b1.lap_staff_no = "
                 + "  a1.col_staff_no and b1.lap_leave_type_cd = a1.col_cat_cd left join(select lap_staff_no, lap_pre_type_cd, sum(convert(float, lap_pre_balance)) as pre_e from "
                 + "  hr_leave_application where lap_staff_no = '" + Session["New"].ToString() + "' and lap_cancel_ind = 'N' and lap_leave_type_cd IN('12') and lap_approve_sts_cd NOT IN('04', '01') and ISNULL(lap_endorse_sts_cd, '') = '' "
                 + "  group by lap_staff_no, lap_pre_type_cd ) as c2 on c2.lap_staff_no = a1.col_staff_no and c2.lap_pre_type_cd = a1.col_cat_cd "

                 + "   union all select a.hr_jenis_Code lev_leave_type_cd,a.lev_staff_no,a.hr_jenis_desc,case when "
                 + "   a.hr_jenis_desc = 'cuti tahunan' then '01' when a.hr_jenis_desc = 'cuti sakit' then '02' when a.hr_jenis_desc = 'cuti ganti' then '04' when a.hr_jenis_desc = 'cuti bawa hadapan' then '03' "
                 + "   end as sno,a.a,cast(a.c as float) as c,a.b,a.ab,ISNULL(b1.e, '0') as e,ISNULL(b2.e, '0') as d,(a.ab - (ISNULL(b1.e, '0') + ISNULL(b2.e, '0'))) as res,  '0' pre_bal from (select "
                 + "   hsp.stf_staff_no as lev_staff_no, hr_jenis_desc, '0' a, gen_leave_day as c, '' as b, gen_leave_day as ab, '' e, '' d, '' res, s1.hr_jenis_Code from Ref_hr_jenis_cuti s1 inner "
                 + "   join hr_staff_profile as hsp on hsp.stf_staff_no = '" + Session["New"].ToString() + "' inner "
                 + "   join hr_cmn_general_leave as s2 on s2.gen_leave_type_cd = s1.hr_jenis_Code where hr_jenis_Code In('17')) as a full "
                 + "   outer join(select lap_staff_no, lap_leave_type_cd, sum(convert(float, lap_leave_day)) as e from hr_leave_application where lap_staff_no = '" + Session["New"].ToString() + "' and lap_cancel_ind = 'N' and "
                 + "   lap_leave_type_cd IN('17') and lap_approve_sts_cd NOT IN('04', '01') and ISNULL(lap_endorse_sts_cd, '') = '' group by   lap_staff_no, lap_leave_type_cd) as b1 on b1.lap_staff_no = "
                 + "   a.lev_staff_no and b1.lap_leave_type_cd = a.hr_jenis_Code full outer join(select lap_staff_no, lap_leave_type_cd, sum(convert(float, lap_leave_day)) as e from hr_leave_application where "
                 + "   lap_staff_no = '" + Session["New"].ToString() + "' and lap_cancel_ind = 'N' and lap_leave_type_cd IN('12', '17') and lap_approve_sts_cd NOT IN('04', '01') and ISNULL(lap_endorse_sts_cd, '') = '01' and lap_cur_sts = 'Y' "
                 + "   group by   lap_staff_no, lap_leave_type_cd) as b2 on b2.lap_staff_no = a.lev_staff_no and b2.lap_leave_type_cd = a.hr_jenis_Code order by sno ";
            get_lv_det = DBCon.Ora_Execute_table(strQuery);
            if (get_lv_det.Rows.Count != 0)
            {
                for (int i = 0; i < get_lv_det.Rows.Count; i++)
                {
                    if (get_lv_det.Rows[i]["lev_leave_type_cd"].ToString() == "01")
                    {
                        sv1.Text = get_lv_det.Rows[i]["d"].ToString() + " dari " + get_lv_det.Rows[i]["ab"].ToString();

                        float gt1 = ((float.Parse(get_lv_det.Rows[i]["d"].ToString()) / float.Parse(get_lv_det.Rows[i]["ab"].ToString())) * 100);

                        cu_avg.Attributes.Add("style", "width:" + gt1 + "%");
                    }

                    if (get_lv_det.Rows[i]["lev_leave_type_cd"].ToString() == "04")
                    {
                        sv2.Text = get_lv_det.Rows[i]["d"].ToString() + " dari " + get_lv_det.Rows[i]["ab"].ToString();
                        float gt2 = ((float.Parse(get_lv_det.Rows[i]["d"].ToString()) / float.Parse(get_lv_det.Rows[i]["ab"].ToString())) * 100);
                        ct_avg.Attributes.Add("style", "width:" + gt2 + "%");
                    }

                    if (get_lv_det.Rows[i]["lev_leave_type_cd"].ToString() == "17")
                    {
                        sv3.Text = get_lv_det.Rows[i]["d"].ToString() + " dari " + get_lv_det.Rows[i]["ab"].ToString();
                        float gt3 = ((float.Parse(get_lv_det.Rows[i]["d"].ToString()) / float.Parse(get_lv_det.Rows[i]["ab"].ToString())) * 100);
                        cs_avg.Attributes.Add("style", "width:" + gt3 + "%");
                    }

                    if (get_lv_det.Rows[i]["lev_leave_type_cd"].ToString() == "12")
                    {
                        sv4.Text = get_lv_det.Rows[i]["d"].ToString() + " dari " + get_lv_det.Rows[i]["ab"].ToString();
                        float gt4 = ((float.Parse(get_lv_det.Rows[i]["d"].ToString()) / float.Parse(get_lv_det.Rows[i]["ab"].ToString())) * 100);
                        cu_samp.Attributes.Add("style", "width:" + gt4 + "%");
                    }

                    if (sv1.Text == "")
                    {
                        sv1.Text = "0 dari 0";
                        cu_avg.Attributes.Add("style", "width:0%");
                    }
                    if (sv2.Text == "")
                    {
                        sv2.Text = "0 dari 0";
                        ct_avg.Attributes.Add("style", "width:0%");
                    }
                    if (sv3.Text == "")
                    {
                        sv3.Text = "0 dari 0";
                        cs_avg.Attributes.Add("style", "width:0%");
                    }
                    if (sv4.Text == "")
                    {
                        sv4.Text = "0 dari 0";
                        cu_samp.Attributes.Add("style", "width:0%");
                    }
                }

            }
            else
            {
                sv1.Text = "0";
                sv2.Text = "0";
                sv3.Text = "0";
                sv4.Text = "0";
                cu_avg.Attributes.Add("style", "width:0%");
                ct_avg.Attributes.Add("style", "width:0%");
                cs_avg.Attributes.Add("style", "width:0%");
                cu_samp.Attributes.Add("style", "width:0%");

            }

            DataTable ddicno1 = new DataTable();
            ddicno1 = DBCon.Ora_Execute_table("select *,FORMAT(stf_service_start_dt,'dd/MM/yyyy', 'en-us') as st_dt,r1.hr_jaw_desc from hr_staff_profile left join ref_hr_jawatan r1 on r1.hr_jaw_Code=stf_curr_post_cd where stf_staff_no='" + Session["New"].ToString() + "'");
            if (ddicno1.Rows.Count == 0)
            {
                Session["clder_id"] = "1";
                //shw_calendar.Attributes.Remove("style");
                LinkButton1_btn2.Visible = false;
                LinkButton1_btn3.Visible = false;
                LinkButton1_btn4.Visible = false;
                LinkButton1_btn5.Visible = false;
                LinkButton1_btn6.Visible = false;
                LinkButton1_btn1.Visible = false;
                if (tot_salary_wtng.Rows[0]["wait_sal"].ToString() != "0")
                {
                    kg1.Text = tot_salary_wtng.Rows[0]["wait_sal"].ToString();
                    LinkButton1_btn7.Visible = true;
                }
                else
                {
                    LinkButton1_btn7.Visible = false;
                }
                if (tot_salary_wtng.Rows[0]["wait_clm"].ToString() != "0")
                {
                    kg2.Text = tot_salary_wtng.Rows[0]["wait_clm"].ToString();
                    LinkButton1_btn8.Visible = true;
                }
                else
                {
                    LinkButton1_btn8.Visible = false;
                }
                if (tot_salary_wtng.Rows[0]["wait_lap"].ToString() != "0")
                {
                    kg3.Text = tot_salary_wtng.Rows[0]["wait_lap"].ToString();
                    LinkButton1_btn10.Visible = true;
                }
                else
                {
                    LinkButton1_btn10.Visible = false;
                }
                Label3.Text = "-";


                string fileimg = Path.GetFileName(checkimage);
                if (fileimg != "")
                {
                    //ImgPrv_top.Attributes.Add("src", "../Files/user/" + fileimg);
                    ImgPrv_top_small.Attributes.Add("src", "../Files/user/" + fileimg);
                }
                else
                {
                    //ImgPrv_top.Attributes.Add("src", "../Files/user/user.gif");
                    ImgPrv_top_small.Attributes.Add("src", "../Files/user/user.gif");
                }
            }
            else
            {
                Session["clder_id"] = "0";
                string checkimage1 = ddicno1_stf.Rows[0]["user_img"].ToString();
                string checkimage = ddicno1.Rows[0]["Stf_image"].ToString();

                string fileimg1 = Path.GetFileName(checkimage1);
                string fileimg = Path.GetFileName(checkimage);
                if (fileimg1 != "")
                {                    
                    ImgPrv_top_small.Attributes.Add("src", "../Files/user/" + fileimg1);
                }
                else if (fileimg != "")
                {
                    ImgPrv_top_small.Attributes.Add("src", "../Files/user/" + fileimg);
                }
                else
                {
                    //ImgPrv_top.Attributes.Add("src", "../Files/user/user.gif");
                    ImgPrv_top_small.Attributes.Add("src", "../Files/user/user.gif");
                }
                Label1.Text = ddicno1.Rows[0]["stf_email"].ToString();
                Label2.Text = txtinfo.ToTitleCase(ddicno1.Rows[0]["stf_permanent_city"].ToString().ToLower());
                if (ddicno1.Rows[0]["st_dt"].ToString() != "")
                {
                    DateTime dt1 = DateTime.ParseExact(ddicno1.Rows[0]["st_dt"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime dt2 = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    TimeSpan tspan = dt2.Subtract(dt1);
                }
                Label3.Text = txtinfo.ToTitleCase(ddicno1.Rows[0]["hr_jaw_desc"].ToString().ToLower());

                LinkButton1_btn2.Visible = true;
                LinkButton1_btn3.Visible = true;
                LinkButton1_btn4.Visible = true;
                LinkButton1_btn5.Visible = false;
                LinkButton1_btn6.Visible = false;
                LinkButton1_btn7.Visible = false;
                LinkButton1_btn8.Visible = false;
                LinkButton1_btn10.Visible = false;
                LinkButton1_btn11.Visible = false;
                LinkButton1_btn12.Visible = false;
            }

        }
        else
        {
            get_role_no1 = DBCon.Ora_Execute_table("select * from (select * from KK_PID_Kumpulan where KK_kumpulan_id IN ('R0016') ) as a where KK_kumpulan_id IN ('" + ddicno1_stf.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

            if (get_role_no1.Rows.Count == 0)
            {
                Session["clder_id"] = "1";
                //shw_calendar.Attributes.Remove("style");
                strQuery = "select count(*) lev_cnt1 from hr_leave_application where lap_leave_type_cd='01' and ISNULL(lap_endorse_sts_cd,'') =''  "
                           + " union all select count(*) lev_cnt1 from hr_leave_application where lap_leave_type_cd='04' and ISNULL(lap_endorse_sts_cd,'') ='' "
                           + " union all select count(*) lev_cnt1 from hr_leave_application where lap_leave_type_cd='17' and ISNULL(lap_endorse_sts_cd,'') ='' "
                           + " union all select count(*) lev_cnt1 from hr_leave_application where lap_leave_type_cd='12' and ISNULL(lap_endorse_sts_cd,'') =''";
                get_lv_det = DBCon.Ora_Execute_table(strQuery);
                if (get_lv_det.Rows.Count != 0)
                {
                    for (int i = 0; i < get_lv_det.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            sv1.Text = get_lv_det.Rows[i]["lev_cnt1"].ToString() + " Permohonan";

                            float gt1 = float.Parse(get_lv_det.Rows[i]["lev_cnt1"].ToString());

                            cu_avg.Attributes.Add("style", "width:" + gt1 + "%");
                        }
                        if (i == 1)
                        {
                            sv2.Text = get_lv_det.Rows[i]["lev_cnt1"].ToString() + " Permohonan";

                            float gt2 = float.Parse(get_lv_det.Rows[i]["lev_cnt1"].ToString());

                            ct_avg.Attributes.Add("style", "width:" + gt2 + "%");
                        }

                        if (i == 2)
                        {
                            sv3.Text = get_lv_det.Rows[i]["lev_cnt1"].ToString() + " Permohonan";

                            float gt3 = float.Parse(get_lv_det.Rows[i]["lev_cnt1"].ToString());

                            cs_avg.Attributes.Add("style", "width:" + gt3 + "%");
                        }

                        if (i == 3)
                        {
                            sv4.Text = get_lv_det.Rows[i]["lev_cnt1"].ToString() + " Permohonan";

                            float gt4 = float.Parse(get_lv_det.Rows[i]["lev_cnt1"].ToString());

                            cu_samp.Attributes.Add("style", "width:" + gt4 + "%");
                        }

                    }
                }

                ddicno1 = DBCon.Ora_Execute_table("select *,FORMAT(stf_service_start_dt,'dd/MM/yyyy', 'en-us') as st_dt,r1.hr_jaw_desc from hr_staff_profile left join ref_hr_jawatan r1 on r1.hr_jaw_Code=stf_curr_post_cd where stf_staff_no='" + Session["New"].ToString() + "'");
                if (ddicno1.Rows.Count == 0)
                {
                    LinkButton1_btn1.Visible = false;
                    Label3.Text = "-";
                    Label1.Text = ddicno1_stf.Rows[0]["KK_email"].ToString();

                    string fileimg = Path.GetFileName(checkimage);
                    if (fileimg != "")
                    {
                        ImgPrv_top_small.Attributes.Add("src", "../Files/user/" + fileimg);
                    }
                    else
                    {
                        ImgPrv_top_small.Attributes.Add("src", "../Files/user/user.gif");
                    }
                }
                else
                {
                    string checkimage1 = ddicno1_stf.Rows[0]["user_img"].ToString();
                    string checkimage = ddicno1.Rows[0]["Stf_image"].ToString();

                    string fileimg1 = Path.GetFileName(checkimage1);
                    string fileimg = Path.GetFileName(checkimage);
                    if (fileimg1 != "")
                    {
                        ImgPrv_top_small.Attributes.Add("src", "../Files/user/" + fileimg1);
                    }
                    else if (fileimg != "")
                    {
                        ImgPrv_top_small.Attributes.Add("src", "../Files/user/" + fileimg);
                    }
                    else
                    {
                        //ImgPrv_top.Attributes.Add("src", "../Files/user/user.gif");
                        ImgPrv_top_small.Attributes.Add("src", "../Files/user/user.gif");
                    }
                    Label1.Text = ddicno1.Rows[0]["stf_email"].ToString();
                    Label2.Text = txtinfo.ToTitleCase(ddicno1.Rows[0]["stf_permanent_city"].ToString().ToLower());
                    if (ddicno1.Rows[0]["st_dt"].ToString() != "")
                    {
                        DateTime dt1 = DateTime.ParseExact(ddicno1.Rows[0]["st_dt"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime dt2 = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        TimeSpan tspan = dt2.Subtract(dt1);
                    }
                    Label3.Text = txtinfo.ToTitleCase(ddicno1.Rows[0]["hr_jaw_desc"].ToString().ToLower());
                }
               
                LinkButton1_btn2.Visible = false;
                LinkButton1_btn3.Visible = false;
                LinkButton1_btn4.Visible = false;
                LinkButton1_btn5.Visible = false;
                LinkButton1_btn6.Visible = false;
                LinkButton1_btn11.Visible = false;
                LinkButton1_btn12.Visible = false;
                if (tot_salary_wtng.Rows[0]["wait_sal"].ToString() != "0")
                {
                    kg1.Text = tot_salary_wtng.Rows[0]["wait_sal"].ToString();
                    LinkButton1_btn7.Visible = true;
                }
                else
                {
                    LinkButton1_btn7.Attributes.Add("style", "pointer-events:None;");
                    LinkButton1_btn7.Visible = true;
                }
                if (tot_salary_wtng.Rows[0]["wait_clm"].ToString() != "0")
                {
                    kg2.Text = tot_salary_wtng.Rows[0]["wait_clm"].ToString();
                    LinkButton1_btn8.Visible = true;
                }
                else
                {
                    LinkButton1_btn8.Attributes.Add("style", "pointer-events:None;");
                    LinkButton1_btn8.Visible = true;
                }
                if (tot_salary_wtng.Rows[0]["wait_lap"].ToString() != "0")
                {
                    kg3.Text = tot_salary_wtng.Rows[0]["wait_lap"].ToString();
                    LinkButton1_btn10.Visible = true;
                }
                else
                {
                    LinkButton1_btn10.Attributes.Add("style", "pointer-events:None;");
                    LinkButton1_btn10.Visible = true;
                }
            }
            else
            {
                Session["clder_id"] = "0";
                strQuery = "select a1.lev_leave_type_cd lev_leave_type_cd,a1.lev_staff_no,a1.hr_jenis_desc,case when a1.hr_jenis_desc = 'cuti tahunan' then '01' when a1.hr_jenis_desc = 'cuti sakit' then '02' when a1.hr_jenis_desc = 'cuti ganti' then '04'  "
                  + " when a1.hr_jenis_desc = 'cuti bawa hadapan' then '04' end as sno, a1.a,cast(a1.c as float) as c,a1.b, a1.ab,((ISNULL(b1.e,'0')) + ISNULL(c2.pre_e,'0')) as e,a1.d,"
                  + " ((((convert(float,(a1.c)) + a1.a) - (convert(float,(a1.d)) + ISNULL(b1.e, '0')))) - ISNULL(c2.pre_e, '0')) as res,ISNULL(c2.pre_e, '0') as pre_bal from (select  "
                  + " lev_staff_no,lev_leave_type_cd,hr_jenis_desc,lev_carry_fwd_day as a,lev_entitle_day as c,convert(float,(((convert(float,lev_entitle_day) / 12) *  DATEPART(month, GETDATE())))) as b,"
                  + " (lev_carry_fwd_day + convert(float,(((convert(float,lev_entitle_day)))))) as ab,lev_taken_day as d,lev_balance_day from hr_staff_profile as SP left join hr_leave as LV on LV.lev_staff_no= "
                  + " SP.stf_staff_no and LV.lev_org_id=sp.str_curr_org_cd left join Ref_hr_jenis_cuti as JC on JC.hr_jenis_Code=LV.lev_leave_type_cd where stf_staff_no='" + Session["New"].ToString() + "' AND lev_year='" + year + "') as a1 "
                  + " full outer join(select lap_staff_no, lap_leave_type_cd,sum(convert(float,lap_leave_day)) as e from hr_leave_application where lap_staff_no='" + Session["New"].ToString() + "' and lap_cancel_ind='N' and "
                  + " lap_leave_type_cd IN('01','04') and lap_approve_sts_cd NOT IN ('04','01') and  ISNULL(lap_endorse_sts_cd,'') = '' group by lap_staff_no,lap_leave_type_cd) as b1 on b1.lap_staff_no="
                  + " a1.lev_staff_no and b1.lap_leave_type_cd=a1.lev_leave_type_cd left join(select lap_staff_no,lap_pre_type_cd,sum(convert(float,lap_pre_balance)) as pre_e from  "
                  + " hr_leave_application where lap_staff_no ='" + Session["New"].ToString() + "' and lap_cancel_ind='N' and lap_leave_type_cd IN('04','05') and lap_approve_sts_cd NOT IN ('04','01') and ISNULL(lap_endorse_sts_cd,'') = '' "
                  + "  group by lap_staff_no,lap_pre_type_cd ) as c2 on  c2.lap_staff_no=a1.lev_staff_no and c2.lap_pre_type_cd = a1.lev_leave_type_cd "

                  + "   union all select a1.col_cat_cd lev_leave_type_cd,a1.col_staff_no,a1.hr_jenis_desc,case when a1.hr_jenis_desc = 'cuti tahunan' then '01' when a1.hr_jenis_desc = 'cuti sakit' then '02' when a1.hr_jenis_desc = 'cuti ganti' then '04' "
                  + "  when a1.hr_jenis_desc = 'cuti bawa hadapan' then '04' end as sno, a1.a,cast(a1.c as float) as c,a1.b, a1.ab,((ISNULL(b1.e, '0')) + ISNULL(c2.pre_e, '0')) as e,a1.d, "
                  + "  ((((convert(float, (a1.c)) + a1.a) - (convert(float, (a1.d)) + ISNULL(b1.e, '0')))) - ISNULL(c2.pre_e, '0')) as res,ISNULL(c2.pre_e, '0') as pre_bal from(select "
                  + "  col_staff_no, col_cat_cd, hr_jenis_desc, '0' as a, col_entitle_day as c, convert(float, (((convert(float, col_entitle_day) / 12) * DATEPART(month, GETDATE())))) as b, "
                  + "  ('0' + convert(float, (((convert(float, col_entitle_day)))))) as ab, col_taken_day as d, col_balance_day from hr_staff_profile as SP left join hr_com_leave as LV on LV.col_staff_no = "
                  + "  SP.stf_staff_no and LV.col_org_id = sp.str_curr_org_cd left join Ref_hr_jenis_cuti as JC on JC.hr_jenis_Code = LV.col_cat_cd where stf_staff_no = '" + Session["New"].ToString() + "' AND col_year = '" + year + "') as a1 "
                  + "  full outer join(select lap_staff_no, lap_leave_type_cd, sum(convert(float, lap_leave_day)) as e from hr_leave_application where lap_staff_no = '" + Session["New"].ToString() + "' and lap_cancel_ind = 'N' and "
                  + "  lap_leave_type_cd IN('12') and lap_approve_sts_cd NOT IN('04', '01') and  ISNULL(lap_endorse_sts_cd, '') = '' group by lap_staff_no, lap_leave_type_cd) as b1 on b1.lap_staff_no = "
                  + "  a1.col_staff_no and b1.lap_leave_type_cd = a1.col_cat_cd left join(select lap_staff_no, lap_pre_type_cd, sum(convert(float, lap_pre_balance)) as pre_e from "
                  + "  hr_leave_application where lap_staff_no = '" + Session["New"].ToString() + "' and lap_cancel_ind = 'N' and lap_leave_type_cd IN('12') and lap_approve_sts_cd NOT IN('04', '01') and ISNULL(lap_endorse_sts_cd, '') = '' "
                  + "  group by lap_staff_no, lap_pre_type_cd ) as c2 on c2.lap_staff_no = a1.col_staff_no and c2.lap_pre_type_cd = a1.col_cat_cd "

                  + "   union all select a.hr_jenis_Code lev_leave_type_cd,a.lev_staff_no,a.hr_jenis_desc,case when "
                  + "   a.hr_jenis_desc = 'cuti tahunan' then '01' when a.hr_jenis_desc = 'cuti sakit' then '02' when a.hr_jenis_desc = 'cuti ganti' then '04' when a.hr_jenis_desc = 'cuti bawa hadapan' then '03' "
                  + "   end as sno,a.a,cast(a.c as float) as c,a.b,a.ab,ISNULL(b1.e, '0') as e,ISNULL(b2.e, '0') as d,(a.ab - (ISNULL(b1.e, '0') + ISNULL(b2.e, '0'))) as res,  '0' pre_bal from (select "
                  + "   hsp.stf_staff_no as lev_staff_no, hr_jenis_desc, '0' a, gen_leave_day as c, '' as b, gen_leave_day as ab, '' e, '' d, '' res, s1.hr_jenis_Code from Ref_hr_jenis_cuti s1 inner "
                  + "   join hr_staff_profile as hsp on hsp.stf_staff_no = '" + Session["New"].ToString() + "' inner "
                  + "   join hr_cmn_general_leave as s2 on s2.gen_leave_type_cd = s1.hr_jenis_Code where hr_jenis_Code In('17')) as a full "
                  + "   outer join(select lap_staff_no, lap_leave_type_cd, sum(convert(float, lap_leave_day)) as e from hr_leave_application where lap_staff_no = '" + Session["New"].ToString() + "' and lap_cancel_ind = 'N' and "
                  + "   lap_leave_type_cd IN('17') and lap_approve_sts_cd NOT IN('04', '01') and ISNULL(lap_endorse_sts_cd, '') = '' group by   lap_staff_no, lap_leave_type_cd) as b1 on b1.lap_staff_no = "
                  + "   a.lev_staff_no and b1.lap_leave_type_cd = a.hr_jenis_Code full outer join(select lap_staff_no, lap_leave_type_cd, sum(convert(float, lap_leave_day)) as e from hr_leave_application where "
                  + "   lap_staff_no = '" + Session["New"].ToString() + "' and lap_cancel_ind = 'N' and lap_leave_type_cd IN('12', '17') and lap_approve_sts_cd NOT IN('04', '01') and ISNULL(lap_endorse_sts_cd, '') = '01' and lap_cur_sts = 'Y' "
                  + "   group by   lap_staff_no, lap_leave_type_cd) as b2 on b2.lap_staff_no = a.lev_staff_no and b2.lap_leave_type_cd = a.hr_jenis_Code order by sno ";
                get_lv_det = DBCon.Ora_Execute_table(strQuery);
                if (get_lv_det.Rows.Count != 0)
                {
                    for (int i = 0; i < get_lv_det.Rows.Count; i++)
                    {
                        if (get_lv_det.Rows[i]["lev_leave_type_cd"].ToString() == "01")
                        {
                            sv1.Text = get_lv_det.Rows[i]["d"].ToString() + " dari " + get_lv_det.Rows[i]["ab"].ToString();

                            float gt1 = ((float.Parse(get_lv_det.Rows[i]["d"].ToString()) / float.Parse(get_lv_det.Rows[i]["ab"].ToString())) * 100);

                            cu_avg.Attributes.Add("style", "width:" + gt1 + "%");
                        }

                        if (get_lv_det.Rows[i]["lev_leave_type_cd"].ToString() == "04")
                        {
                            sv2.Text = get_lv_det.Rows[i]["d"].ToString() + " dari " + get_lv_det.Rows[i]["ab"].ToString();
                            float gt2 = ((float.Parse(get_lv_det.Rows[i]["d"].ToString()) / float.Parse(get_lv_det.Rows[i]["ab"].ToString())) * 100);
                            ct_avg.Attributes.Add("style", "width:" + gt2 + "%");
                        }

                        if (get_lv_det.Rows[i]["lev_leave_type_cd"].ToString() == "17")
                        {
                            sv3.Text = get_lv_det.Rows[i]["d"].ToString() + " dari " + get_lv_det.Rows[i]["ab"].ToString();
                            float gt3 = ((float.Parse(get_lv_det.Rows[i]["d"].ToString()) / float.Parse(get_lv_det.Rows[i]["ab"].ToString())) * 100);
                            cs_avg.Attributes.Add("style", "width:" + gt3 + "%");
                        }

                        if (get_lv_det.Rows[i]["lev_leave_type_cd"].ToString() == "12")
                        {
                            sv4.Text = get_lv_det.Rows[i]["d"].ToString() + " dari " + get_lv_det.Rows[i]["ab"].ToString();
                            float gt4 = ((float.Parse(get_lv_det.Rows[i]["d"].ToString()) / float.Parse(get_lv_det.Rows[i]["ab"].ToString())) * 100);
                            cu_samp.Attributes.Add("style", "width:" + gt4 + "%");
                        }

                    }

                    if(sv1.Text == "")
                    {
                        sv1.Text = "0 dari 0";
                        cu_avg.Attributes.Add("style", "width:0%");
                    }
                    if (sv2.Text == "")
                    {
                        sv2.Text = "0 dari 0";
                        ct_avg.Attributes.Add("style", "width:0%");
                    }
                    if (sv3.Text == "")
                    {
                        sv3.Text = "0 dari 0";
                        cs_avg.Attributes.Add("style", "width:0%");
                    }
                    if (sv4.Text == "")
                    {
                        sv4.Text = "0 dari 0";
                        cu_samp.Attributes.Add("style", "width:0%");
                    }


                

                }
                ddicno1 = DBCon.Ora_Execute_table("select *,FORMAT(stf_service_start_dt,'dd/MM/yyyy', 'en-us') as st_dt,r1.hr_jaw_desc from hr_staff_profile left join ref_hr_jawatan r1 on r1.hr_jaw_Code=stf_curr_post_cd where stf_staff_no='" + Session["New"].ToString() + "'");
                if (ddicno1.Rows.Count == 0)
                {
                    
                    Label3.Text = "-";
                    Label1.Text = ddicno1_stf.Rows[0]["KK_email"].ToString();

                    string fileimg = Path.GetFileName(checkimage);
                    if (fileimg != "")
                    {
                        ImgPrv_top_small.Attributes.Add("src", "../Files/user/" + fileimg);
                    }
                    else
                    {
                        ImgPrv_top_small.Attributes.Add("src", "../Files/user/user.gif");
                    }
                }
                else
                {
                    //shw_calendar.Attributes.Remove("style");
                    Session["clder_id"] = "1";
                    string checkimage1 = ddicno1_stf.Rows[0]["user_img"].ToString();
                    string checkimage = ddicno1.Rows[0]["Stf_image"].ToString();

                    string fileimg1 = Path.GetFileName(checkimage1);
                    string fileimg = Path.GetFileName(checkimage);
                    if (fileimg1 != "")
                    {
                        ImgPrv_top_small.Attributes.Add("src", "../Files/user/" + fileimg1);
                    }
                    else if (fileimg != "")
                    {
                        ImgPrv_top_small.Attributes.Add("src", "../Files/user/" + fileimg);
                    }
                    else
                    {
                        //ImgPrv_top.Attributes.Add("src", "../Files/user/user.gif");
                        ImgPrv_top_small.Attributes.Add("src", "../Files/user/user.gif");
                    }
                    Label1.Text = ddicno1.Rows[0]["stf_email"].ToString();
                    Label2.Text = txtinfo.ToTitleCase(ddicno1.Rows[0]["stf_permanent_city"].ToString().ToLower());
                    if (ddicno1.Rows[0]["st_dt"].ToString() != "")
                    {
                        DateTime dt1 = DateTime.ParseExact(ddicno1.Rows[0]["st_dt"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime dt2 = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        TimeSpan tspan = dt2.Subtract(dt1);
                    }
                    Label3.Text = txtinfo.ToTitleCase(ddicno1.Rows[0]["hr_jaw_desc"].ToString().ToLower());
                }

                LinkButton1_btn2.Visible = true;
                LinkButton1_btn3.Visible = true;
                LinkButton1_btn4.Visible = true;
                LinkButton1_btn5.Visible = false;
                LinkButton1_btn6.Visible = false;
                LinkButton1_btn11.Visible = true;
                LinkButton1_btn12.Visible = true;
                LinkButton1_btn7.Visible = false;
                LinkButton1_btn8.Visible = false;
                LinkButton1_btn10.Visible = false;
              
            }
        }
        if (ddicno1_role.Rows.Count != 0)
        {
            for (int k = 0; k < ddicno1_role.Rows.Count; k++)
            {
                if (ddicno1_role.Rows[k]["psub_skrin_id"].ToString() == "P0071")
                {
                    
                    LinkButton1_btn5.Visible = true;
                }
                if (ddicno1_role.Rows[k]["psub_skrin_id"].ToString() == "P0207")
                {
                    LinkButton1_btn6.Visible = true;
                }
                if (ddicno1_role.Rows[k]["psub_skrin_id"].ToString() == "P0200")
                {
                    if (tot_salary_wtng.Rows[0]["wait_sal"].ToString() != "0")
                    {
                        kg1.Text = tot_salary_wtng.Rows[0]["wait_sal"].ToString();
                        LinkButton1_btn7.Visible = true;
                    }
                    else
                    {
                        kg1.Text = "0";
                        LinkButton1_btn7.Attributes.Add("style", "pointer-events:None;");
                        LinkButton1_btn7.Visible = true;
                    }
                }
                if (ddicno1_role.Rows[k]["psub_skrin_id"].ToString() == "P0211")
                {
                    if (tot_salary_wtng.Rows[0]["wait_clm"].ToString() != "0")
                    {
                        kg2.Text = tot_salary_wtng.Rows[0]["wait_clm"].ToString();
                        LinkButton1_btn8.Visible = true;
                    }
                    else
                    {
                        kg2.Text = "0";
                        LinkButton1_btn8.Attributes.Add("style", "pointer-events:None;");
                        LinkButton1_btn8.Visible = true;
                    }
                }
                if (ddicno1_role.Rows[k]["psub_skrin_id"].ToString() == "P0062")
                {
                    if (tot_salary_wtng.Rows[0]["wait_lap"].ToString() != "0")
                    {
                        kg3.Text = tot_salary_wtng.Rows[0]["wait_lap"].ToString();
                        LinkButton1_btn10.Visible = true;
                    }
                    else
                    {
                        kg3.Text = "0";
                        LinkButton1_btn10.Attributes.Add("style", "pointer-events:None;");
                        LinkButton1_btn10.Visible = true;
                    }
                }

            }
        }

    }

    protected void clk_profile(object sender, EventArgs e)
    {
        string name = HttpUtility.UrlEncode(service.Encrypt(Session["New"].ToString()));        
        Response.Redirect(string.Format("/SUMBER_MANUSIA/HR_DAFTER_STAFF.aspx?edit={0}", name));
    }

    protected void clk_cuti(object sender, EventArgs e)
    {
        Response.Redirect("/SUMBER_MANUSIA/HR_MOHON_CUTI.aspx");
    }

    protected void clk_payslip(object sender, EventArgs e)
    {
        Response.Redirect("/SUMBER_MANUSIA/HR_PAYSLIP.aspx");
    }

    protected void clk_claim(object sender, EventArgs e)
    {
        Response.Redirect("/SUMBER_MANUSIA/HR_tuntutan.aspx");
    }

    protected void clk_settings(object sender, EventArgs e)
    {
        //string name = HttpUtility.UrlEncode(service.Encrypt(Session["New"].ToString()));
        Response.Redirect("/change_pwd.aspx");
    }

    protected void clk_semak_pen(object sender, EventArgs e)
    {
        Response.Redirect("/SUMBER_MANUSIA/HR_SEMAK_HAJI.aspx");
    }

    protected void clk_sck(object sender, EventArgs e)
    {
        Response.Redirect("/SUMBER_MANUSIA/HR_CUTI_LIST_view.aspx");
    }

    protected void clk_kg(object sender, EventArgs e)
    {
        Response.Redirect("/SUMBER_MANUSIA/kelulusan_gaji.aspx");
    }

    protected void clk_kt(object sender, EventArgs e)
    {
        Response.Redirect("/SUMBER_MANUSIA/kelulusan_tuntutan.aspx");
    }

    protected void clk_kc(object sender, EventArgs e)
    {
        Response.Redirect("/SUMBER_MANUSIA/HR_SEMAKAN_view.aspx");
    }

    protected void clk_stun(object sender, EventArgs e)
    {
        Response.Redirect("/SUMBER_MANUSIA/pengasahan_tuntutan.aspx");
    }
    protected void clk_jpen(object sender, EventArgs e)
    {
        Response.Redirect("/SUMBER_MANUSIA/hr_gaji_kelompok.aspx");
    }
}