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
using System.Net;
using System.Net.Mail;

public partial class HR_CUTI_LIST : System.Web.UI.Page
{
    Mailcoms ObjMail = new Mailcoms();
    SMS ObjSms = new SMS();
    CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["HRMS_ConString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMS_ConString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["HRMS_ConString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMS_ConString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    DataTable dt = new DataTable();
    StudentWebService service = new StudentWebService();
    string useid = string.Empty;
    string Status = string.Empty;
    string oid = string.Empty;
    string sdd = string.Empty;
    string sno = string.Empty;
    string v1 = string.Empty, v2 = string.Empty, v3 = string.Empty, v4 = string.Empty;
    string CommandArgument1 = string.Empty, CommandArgument2 = string.Empty, CommandArgument3 = string.Empty;
    string chk_sex = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;
                if (samp != "")
                {
                    Applcn_no1.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    view_details();                    

                    TextBox11.Attributes.Add("Readonly", "Readonly");
                    //TextBox12.Attributes.Add("Readonly", "Readonly");
                    txt_sebab.Attributes.Add("Readonly", "Readonly");
                    TextBox4.Attributes.Add("Readonly", "Readonly");
                    txt_cata.Attributes.Add("Readonly", "Readonly");
                    txt_peny.Attributes.Add("Readonly", "Readonly");
                    txt_kelu.Attributes.Add("Readonly", "Readonly");

                    txt_taricuti.Attributes.Add("Readonly", "Readonly");
                    txt_hingg.Attributes.Add("Readonly", "Readonly");

                }
                else
                {
                    TextBox1.Text = "";
                    TextBox3.Text = "";
                }
                useid = Session["New"].ToString();


            }
            else
            {
                Response.Redirect("../HRMS_Login.aspx");
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('1787','448','1788','484','1565','1789','473','516','1790','1791','1792','1528','283','1793','1342','53','1794','1789','71','1795','1574','77') order by ID ASC");

            CultureInfo culinfo = Thread.CurrentThread.CurrentUICulture;
            TextInfo txtinfo = culinfo.TextInfo;

            //h1_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
            bb1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            //bb2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());

            //h3_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
            //h3_tag2.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
            //h3_tag3.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            //h3_tag4.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[20][0].ToString().ToLower());

            //lbl1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            //lbl2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            //lbl3_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            //lbl4_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower());
            //lbl5_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[16][0].ToString().ToLower());
            //lbl6_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            lbl7_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[18][0].ToString().ToLower());
            lbl8_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            lbl9_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[19][0].ToString().ToLower());
            lbl10_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            //lbl11_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            //lbl12_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());

            TextBox11.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            TextBox4.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            txt_peny.Text = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());

            //CUTI.Text = txtinfo.ToTitleCase(gt_lng.Rows[17][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
        }
        else
        {
            Response.Redirect("../HRMS_Login.aspx");
        }
    }
    void view_details()
    {
        CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
        TextInfo txtinfo = culinfo.TextInfo;
        DataTable ddokdicno1 = new DataTable();
        ddokdicno1 = DBCon.Ora_Execute_table("select sp.stf_name,sp.emp_no,lap_staff_no,FORMAT(lap_application_dt,'dd/MM/yyyy') as lap_application_dt,lap_leave_type_cd,ISNULL(c1.hr_jenis_desc,'') hr_jenis_desc,ISNULL(c2.hr_kategori_desc,'') hr_kategori_desc,c3.hr_leave_desc,lap_leave_cat_cd,lap_leave_start_dt,lap_leave_end_dt,lap_reason,lap_cancel_ind,lap_endorse_sts_cd,lap_endorse_remark,lap_approve_sts_cd,lap_approve_remark,lap_endorse_remark,lap_approve_id,lap_approve_dt,lap_cancel_ind,ISNULL(UPPER(hsp1.stf_name),'') as stf_name,lap_doc,lap_ref_no,lap_leave_day,lap_pre_balance,sp.stf_sex_cd,case when lap_endorse_sts_cd = '01' then 'Approved' when lap_endorse_sts_cd = '02' then 'Rejected' else 'Pending' end as psts From hr_leave_application as LA  left join Ref_hr_jenis_cuti as JC on JC.hr_jenis_Code=LA.lap_leave_type_cd left join Ref_hr_kategori_cuti as KC on KC.hr_kategori_Code=LA.lap_leave_cat_cd left join hr_staff_profile as hsp1 on hsp1.stf_staff_no=LA.lap_approve_id left join Ref_hr_jenis_cuti c1 on c1.hr_jenis_Code=LA.lap_leave_type_cd left join Ref_hr_kategori_cuti c2 on c2.hr_jenis_code=LA.lap_leave_type_cd and c2.hr_kategori_Code=LA.lap_leave_cat_cd left join Ref_hr_leave_sts c3 on c3.hr_leave_Code=LA.lap_approve_sts_cd left join hr_staff_profile as sp on sp.stf_staff_no=LA.lap_staff_no where LA.Id='" + Applcn_no1.Text + "'");
        if (ddokdicno1.Rows.Count != 0)
        {
            chk_sex = ddokdicno1.Rows[0]["stf_sex_cd"].ToString();
            //DD_JCUTI.SelectedValue = ddokdicno1.Rows[0]["lap_leave_type_cd"].ToString().Trim();
            //DD_KCUTI.SelectedValue = ddokdicno1.Rows[0]["lap_leave_cat_cd"].ToString().Trim();
            TextBox9.Text = ddokdicno1.Rows[0]["lap_leave_day"].ToString();
            TextBox10.Text = ddokdicno1.Rows[0]["lap_pre_balance"].ToString();
            TextBox7.Text = ddokdicno1.Rows[0]["lap_ref_no"].ToString();
            TextBox11.Text = ddokdicno1.Rows[0]["hr_jenis_desc"].ToString().Trim();
            TextBox13.Text = ddokdicno1.Rows[0]["lap_leave_type_cd"].ToString().Trim();
            if (ddokdicno1.Rows[0]["lap_leave_type_cd"].ToString().Trim() == "03")
            {
                todate.Visible = false;
            }
            else
            {
                todate.Visible = true;
            }
            TextBox12.Text = ddokdicno1.Rows[0]["psts"].ToString();
            txt_catat.Value = ddokdicno1.Rows[0]["lap_endorse_remark"].ToString();
            //TextBox12.Text = ddokdicno1.Rows[0]["hr_kategori_desc"].ToString().Trim();
            TextBox14.Text = ddokdicno1.Rows[0]["lap_leave_cat_cd"].ToString().Trim();

            txt_taricuti.Text = Convert.ToDateTime(ddokdicno1.Rows[0]["lap_leave_start_dt"]).ToString("dd/MM/yyyy");
            TextBox8.Text = Convert.ToDateTime(ddokdicno1.Rows[0]["lap_leave_start_dt"]).ToString("dd/MM/yyyy");
            txt_hingg.Text = Convert.ToDateTime(ddokdicno1.Rows[0]["lap_leave_end_dt"]).ToString("dd/MM/yyyy");
            string Lev_cancel = ddokdicno1.Rows[0]["lap_cancel_ind"].ToString().Trim();
            if (Lev_cancel == "N")
            {
                //txt_sebab.Visible = false;
                //CUTI.Visible = false;
                seb_id.Attributes.Remove("style");
            }
            else
            {
                //txt_sebab.Visible = true;
                //CUTI.Visible = true;
                seb_id.Attributes.Add("style", "display:None;");
            }
            txt_sebab.Text = ddokdicno1.Rows[0]["lap_reason"].ToString();
            Label8.Text = ddokdicno1.Rows[0]["lap_leave_day"].ToString();
            //DD_LEVSTS.SelectedValue = ddokdicno1.Rows[0]["lap_approve_sts_cd"].ToString().Trim();
            TextBox4.Text = ddokdicno1.Rows[0]["hr_leave_desc"].ToString();
            TextBox5.Text = ddokdicno1.Rows[0]["lap_approve_sts_cd"].ToString().Trim();
            txt_cata.Text = ddokdicno1.Rows[0]["lap_approve_remark"].ToString();
            txt_peny.Text = txtinfo.ToTitleCase(ddokdicno1.Rows[0]["stf_name"].ToString().ToLower());
            if (ddokdicno1.Rows[0]["lap_approve_dt"].ToString() != "")
            {
                txt_kelu.Text = Convert.ToDateTime(ddokdicno1.Rows[0]["lap_approve_dt"]).ToString("dd/MM/yyyy");
            }
            else
            {
                txt_kelu.Text = "";
            }
          
            txt_catat.Value = ddokdicno1.Rows[0]["lap_endorse_remark"].ToString();
            TextBox1.Text = ddokdicno1.Rows[0]["lap_staff_no"].ToString();
            TextBox3.Text = ddokdicno1.Rows[0]["lap_staff_no"].ToString();
            Label4.Text = ddokdicno1.Rows[0]["emp_no"].ToString();
            Label5.Text = txtinfo.ToTitleCase(ddokdicno1.Rows[0]["stf_name"].ToString().ToLower());
            TextBox2.Text = ddokdicno1.Rows[0]["lap_application_dt"].ToString();
            if (ddokdicno1.Rows[0]["lap_doc"].ToString() != "")
            {
                TextBox6.Text = ddokdicno1.Rows[0]["lap_doc"].ToString();
                //dfile.Text = (string)sqlReader3["lap_doc"].ToString();
                lnkDownload.Text = ddokdicno1.Rows[0]["lap_doc"].ToString();
                dfile_id.Attributes.Remove("Style");
            }
            else
            {
                dfile_id.Attributes.Add("Style", "display:None;");
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('No Record Found.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
        //TextBox1.Text = abc;
        grid1();
    }


    //void Pengesahan()
    //{
    //    DataSet Ds = new DataSet();
    //    try
    //    {
    //        string com = "select hr_Pengesahan_desc,hr_Pengesahan_Code From Ref_hr_Pengesahan_sts where Status='A'";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
    //        DataTable dt = new DataTable();
    //        adpt.Fill(dt);
    //        DD_Penge.DataSource = dt;
    //        DD_Penge.DataTextField = "hr_Pengesahan_desc";
    //        DD_Penge.DataValueField = "hr_Pengesahan_Code";
    //        DD_Penge.DataBind();
    //        DD_Penge.Items.Insert(0, new ListItem("--- SELECT ---", ""));
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    void grid1()
    {
        con.Open();
        string year = DateTime.Now.ToString("yyyy");
        //DataTable ddicno = new DataTable();
        //ddicno = DBCon.Ora_Execute_table("select stf_staff_no From hr_staff_profile where stf_icno='" + Session["New"].ToString() + "' ");
        //string stffno = ddicno.Rows[0]["stf_staff_no"].ToString();
        string YR = DateTime.Now.ToString("yyyy");
        string chk2 = string.Empty;
        if (chk_sex.Trim() == "M")
        {
            chk2 = "('09')";
        }
        else if (chk_sex.Trim() == "F")
        {
            chk2 = "('12')";
        }
        else
        {
            chk2 = "('09','12')";
        }
        //SqlCommand cmd = new SqlCommand("select a1.lev_staff_no,a1.hr_jenis_desc,a1.a,cast(a1.c as float) as c,a1.b, a1.ab,(ISNULL(b1.e,'0') + ISNULL(c1.e,'0')) as e,a1.d,(((convert(float,(a1.c)) + a1.a) - (convert(float,(a1.d)) + ISNULL(b1.e,'0'))) - ISNULL(c1.e,'0')) as res from (select lev_staff_no,lev_leave_type_cd,hr_jenis_desc,lev_carry_fwd_day as a,lev_entitle_day as c,convert(float,(((convert(float,lev_entitle_day) / 12) * DATEPART(month, GETDATE())))) as b,(lev_carry_fwd_day + convert(float,(((convert(float,lev_entitle_day)))))) as ab,lev_taken_day as d,lev_balance_day from hr_staff_profile as SP left join hr_leave as LV on LV.lev_staff_no=SP.stf_staff_no and LV.lev_org_id=sp.str_curr_org_cd left join Ref_hr_jenis_cuti as JC on JC.hr_jenis_Code=LV.lev_leave_type_cd where stf_staff_no='" + TextBox3.Text + "' AND lev_year='" + YR + "') as a1 full outer join(select lap_staff_no,lap_leave_type_cd,sum(convert(float,lap_leave_day)) as e from hr_leave_application where lap_staff_no='" + TextBox3.Text + "' and lap_cancel_ind='N' and lap_leave_type_cd IN('01','04','05') and lap_approve_sts_cd NOT IN ('04','01') and ISNULL(lap_endorse_sts_cd,'') = '' group by lap_staff_no,lap_leave_type_cd) as b1 on b1.lap_staff_no=a1.lev_staff_no and b1.lap_leave_type_cd=a1.lev_leave_type_cd left join(select lap_staff_no,lap_leave_type_cd,sum(convert(float,lap_leave_day)) as e from hr_leave_application where lap_staff_no='"+ TextBox3.Text + "' and lap_cancel_ind='N' and lap_leave_type_cd IN('03') and lap_approve_sts_cd NOT IN ('04','01') and ISNULL(lap_endorse_sts_cd,'') = '' group by lap_staff_no,lap_leave_type_cd) as c1 on c1.lap_staff_no=a1.lev_staff_no and a1.lev_leave_type_cd='01' union all select a.lev_staff_no,a.hr_jenis_desc,a.a,cast(a.c as float) as c,a.b,a.ab,ISNULL(b1.e,'0') as e,ISNULL(b2.e,'0') as d,(a.ab - (ISNULL(b1.e,'0') + ISNULL(b2.e,'0'))) as res from (select '" + TextBox3.Text + "' as lev_staff_no,hr_jenis_desc,'0' a,gen_leave_day as c,'' as b,gen_leave_day as ab,'' e, '' d, '' res,s1.hr_jenis_Code from Ref_hr_jenis_cuti s1 inner join hr_cmn_general_leave as s2 on s2.gen_leave_type_cd=s1.hr_jenis_Code where hr_jenis_Code In ('09','10','12')) as a full outer join(select lap_staff_no,lap_leave_type_cd,sum(convert(float,lap_leave_day)) as e from hr_leave_application where lap_staff_no='" + TextBox3.Text + "' and lap_cancel_ind='N' and lap_leave_type_cd IN('09','10','12') and lap_approve_sts_cd NOT IN ('04','01') and ISNULL(lap_endorse_sts_cd,'') = '' group by lap_staff_no,lap_leave_type_cd) as b1 on b1.lap_staff_no=a.lev_staff_no and b1.lap_leave_type_cd=a.hr_jenis_Code full outer join(select lap_staff_no,lap_leave_type_cd,sum(convert(float,lap_leave_day)) as e from hr_leave_application where lap_staff_no='" + TextBox3.Text + "' and lap_cancel_ind='N' and lap_leave_type_cd IN('09','10','12') and lap_approve_sts_cd NOT IN ('04','01') and ISNULL(lap_endorse_sts_cd,'') = '01' group by lap_staff_no,lap_leave_type_cd) as b2 on b2.lap_staff_no=a.lev_staff_no and b2.lap_leave_type_cd=a.hr_jenis_Code", con);
        SqlCommand cmd = new SqlCommand("select a1.lev_staff_no,a1.hr_jenis_desc,case when a1.hr_jenis_desc = 'cuti tahunan' then '02' when a1.hr_jenis_desc = 'cuti sakit (pesakit luar)' then '03' when a1.hr_jenis_desc = 'cuti sakit (hospitalisasi)' then '04' when a1.hr_jenis_desc = 'CUTI BAWA HADAPAN' then '01' when a1.hr_jenis_desc = 'CUTI KOMPASIONAT' then '05' when a1.hr_jenis_desc = 'Cuti Bersalin' then '06' when a1.hr_jenis_desc = 'CUTI PATERNITY' then '07' end as sno,a1.a,(cast(a1.c as float) + ISNULL(c1_2.e,'0')) as c,a1.b,(a1.ab + ISNULL(c1_2.e,'0')) as ab,((ISNULL(b1.e,'0') + ISNULL(c1.e,'0') + ISNULL(c1_1.e,'0') + ISNULL(c1_2.e,'0')) + ISNULL(c2.pre_e,'0')) as e,a1.d,((((convert(float,(a1.c)) + a1.a) - (convert(float,(a1.d)) + ISNULL(b1.e, '0'))) -ISNULL(c1.e, '0') -ISNULL(c1_1.e, '0') -ISNULL(c1_2.e,'0')) -ISNULL(c2.pre_e, '0')) as res,ISNULL(c2.pre_e, '0') as pre_bal from (select lev_staff_no,lev_leave_type_cd,hr_jenis_desc,lev_carry_fwd_day as a,lev_entitle_day as c,convert(float,(((convert(float,lev_entitle_day) / 12) * DATEPART(month, GETDATE())))) as b,(lev_carry_fwd_day + convert(float,(((convert(float,lev_entitle_day)))))) as ab,lev_taken_day as d,lev_balance_day from hr_staff_profile as SP left join hr_leave as LV on LV.lev_staff_no=SP.stf_staff_no and LV.lev_org_id=sp.str_curr_org_cd left join Ref_hr_jenis_cuti as JC on JC.hr_jenis_Code=LV.lev_leave_type_cd where stf_staff_no='" + TextBox3.Text + "' AND lev_year='" + year + "') as a1 full outer join(select lap_staff_no,lap_leave_type_cd,sum(convert(float,lap_leave_day)) as e from hr_leave_application where lap_staff_no='" + TextBox3.Text + "' and lap_cancel_ind='N' and lap_leave_type_cd IN('01','05','17') and lap_approve_sts_cd NOT IN ('04','01') and ISNULL(lap_endorse_sts_cd,'') = '' group by lap_staff_no,lap_leave_type_cd) as b1 on b1.lap_staff_no=a1.lev_staff_no and b1.lap_leave_type_cd=a1.lev_leave_type_cd left join(select lap_staff_no,lap_leave_type_cd,sum(convert(float,lap_leave_day)) as e from hr_leave_application where lap_staff_no='" + TextBox3.Text + "' and lap_cancel_ind='N' and lap_leave_type_cd IN('03') and lap_approve_sts_cd NOT IN ('04','01') and ISNULL(lap_endorse_sts_cd,'') = '' group by lap_staff_no,lap_leave_type_cd) as c1 on c1.lap_staff_no=a1.lev_staff_no and a1.lev_leave_type_cd='01' left join(select lap_staff_no,lap_leave_type_cd,sum(convert(float,lap_leave_day)) as e from hr_leave_application where lap_staff_no='" + TextBox3.Text + "' and lap_cancel_ind='N' and lap_leave_type_cd IN('02') and lap_approve_sts_cd NOT IN ('04','01') and ISNULL(lap_endorse_sts_cd,'') = '' group by lap_staff_no,lap_leave_type_cd) as c1_1 on c1_1.lap_staff_no=a1.lev_staff_no and a1.lev_leave_type_cd='01' left join(select lap_staff_no,lap_leave_type_cd,sum(convert(float,lap_leave_day)) as e from hr_leave_application where lap_staff_no='" + TextBox3.Text + "' and lap_cancel_ind='N' and lap_leave_type_cd IN('18') and lap_approve_sts_cd NOT IN ('04','01') and ISNULL(lap_endorse_sts_cd,'') = '' group by lap_staff_no,lap_leave_type_cd) as c1_2 on c1_2.lap_staff_no=a1.lev_staff_no and a1.lev_leave_type_cd='01' left join(select lap_staff_no,lap_pre_type_cd,sum(convert(float,lap_pre_balance)) as pre_e from hr_leave_application where lap_staff_no ='" + TextBox3.Text + "' and lap_cancel_ind='N' and lap_leave_type_cd IN('05') and lap_approve_sts_cd NOT IN ('04','01') and ISNULL(lap_endorse_sts_cd,'') = '' group by lap_staff_no,lap_pre_type_cd) as c2 on  c2.lap_staff_no=a1.lev_staff_no and c2.lap_pre_type_cd = a1.lev_leave_type_cd union all select a.lev_staff_no,a.hr_jenis_desc,case when a.hr_jenis_desc = 'cuti tahunan' then '02' when a.hr_jenis_desc = 'cuti sakit (pesakit luar)' then '03' when a.hr_jenis_desc = 'cuti sakit (hospitalisasi)' then '04' when a.hr_jenis_desc = 'CUTI BAWA HADAPAN' then '01' when a.hr_jenis_desc = 'CUTI KOMPASIONAT' then '05' when a.hr_jenis_desc = 'Cuti Bersalin' then '06' when a.hr_jenis_desc = 'CUTI PATERNITY' then '07' end as sno,a.a,cast(a.c as float) as c,a.b,a.ab,ISNULL(b1.e,'0') as e,ISNULL(b2.e,'0') as d,(a.ab - (ISNULL(b1.e,'0') + ISNULL(b2.e,'0'))) as res,'0' pre_bal from (select hsp.stf_staff_no as lev_staff_no,hr_jenis_desc,'0' a,gen_leave_day as c,'' as b,gen_leave_day as ab,'' e, '' d, '' res,s1.hr_jenis_Code from Ref_hr_jenis_cuti s1 inner join hr_staff_profile as hsp on hsp.stf_staff_no='" + TextBox3.Text + "' inner join hr_cmn_general_leave as s2 on s2.gen_leave_type_cd=s1.hr_jenis_Code where hr_jenis_Code In " + chk2 + ") as a full outer join(select lap_staff_no,lap_leave_type_cd,sum(convert(float,lap_leave_day)) as e from hr_leave_application where lap_staff_no='" + TextBox3.Text + "' and lap_cancel_ind='N' and lap_leave_type_cd IN " + chk2 + " and lap_approve_sts_cd NOT IN ('04','01') and ISNULL(lap_endorse_sts_cd,'') = '' group by lap_staff_no,lap_leave_type_cd) as b1 on b1.lap_staff_no=a.lev_staff_no and b1.lap_leave_type_cd=a.hr_jenis_Code full outer join(select lap_staff_no,lap_leave_type_cd,sum(convert(float,lap_leave_day)) as e from hr_leave_application where lap_staff_no='" + TextBox3.Text + "' and lap_cancel_ind='N' and lap_leave_type_cd IN " + chk2 + " and lap_approve_sts_cd NOT IN ('04','01') and ISNULL(lap_endorse_sts_cd,'') = '01' and lap_cur_sts='Y' group by lap_staff_no,lap_leave_type_cd) as b2 on b2.lap_staff_no=a.lev_staff_no and b2.lap_leave_type_cd=a.hr_jenis_Code order by sno", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView2.DataSource = ds;
            GridView2.DataBind();
            int columncount = GridView2.Rows[0].Cells.Count;
            GridView2.Rows[0].Cells.Clear();
            GridView2.Rows[0].Cells.Add(new TableCell());
            GridView2.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView2.Rows[0].Cells[0].Text = "<center><strong>No Record Found</strong></center>";
            //btn_hups.Visible = false;
        }
        else
        {
            GridView2.DataSource = ds;
            GridView2.DataBind();
        }
        con.Close();
    }


    protected void gvSelected_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        grid1();
    }

    protected void DownloadFile(object sender, EventArgs e)
    {
        if (TextBox6.Text != "")
        {
            string filePath = Server.MapPath("~/FILES/Attendance/" + TextBox6.Text);
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + Path.GetFileName(filePath) + "\"");
            Response.WriteFile(filePath);
            Response.End();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('No Record Found',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }


    }

   
  

    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../SUMBER_MANUSIA/HR_SEMAKAN.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../SUMBER_MANUSIA/HR_CUTI_LIST_view.aspx");
    }


}