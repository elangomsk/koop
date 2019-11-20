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

public partial class HR_KM_pen_prestasi : System.Web.UI.Page
{
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    SqlCommand com;
    DataTable dt = new DataTable();
    StudentWebService service = new StudentWebService();

    string Status = string.Empty;
    string level, userid, stscd, abc1;
    string phdate1 = string.Empty, phdate2 = string.Empty, phdate3 = string.Empty, phdate4 = string.Empty;
    string etdate1 = string.Empty, etdate2 = string.Empty, etdate3 = string.Empty, etdate4 = string.Empty;
    string val1 = string.Empty, val2 = string.Empty, val3 = string.Empty, val4 = string.Empty;
    string cyr1 = string.Empty, cyr2 = string.Empty;
    decimal total = 0M;
    decimal total1 = 0M;
    decimal total2 = 0M;
    decimal total_jmk1 = 0M, total_jmk2 = 0M, total_jmk3 = 0M, total_jmk4 = 0M;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        string script = " $(function () {$('.select2').select2();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                level = Session["level"].ToString();
                userid = Session["New"].ToString();

                var samp = Request.Url.Query;

                if (samp != "")
                {
                    Kaki_no.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    view_details();
                }

                s_nama.Attributes.Add("Readonly", "Readonly");
                s_jaw.Attributes.Add("Readonly", "Readonly");
                s_jab.Attributes.Add("Readonly", "Readonly");
                s_gred.Attributes.Add("Readonly", "Readonly");
                s_kj.Attributes.Add("Readonly", "Readonly");
                s_unit.Attributes.Add("Readonly", "Readonly");
                txt_org.Attributes.Add("Readonly", "Readonly");

                TextBox1.Attributes.Add("Readonly", "Readonly");
                TextBox2.Attributes.Add("Readonly", "Readonly");
                TextBox3.Attributes.Add("Readonly", "Readonly");
                TextBox4.Attributes.Add("Readonly", "Readonly");
                ul_tarea.Attributes.Add("Readonly", "Readonly");

                grid_kpr();
                grid_jmk();
                sectionBind();

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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('526','448','1460','484','16','1405','1288','190','1397','1566','513','1977','274','77','1979','1441','1376','1980','1981','1982','1983','61','1439')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());                  
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl10.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());    
            ps_lbl11.Text = txtinfo.ToTitleCase(gt_lng.Rows[20][0].ToString().ToLower());
            ps_lbl12.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl13.Text = txtinfo.ToTitleCase(gt_lng.Rows[22][0].ToString().ToLower());
            ps_lbl14.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            ps_lbl16.Text = txtinfo.ToTitleCase(gt_lng.Rows[16][0].ToString().ToLower());
            ps_lbl17.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
            ps_lbl18.Text = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
            Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower());        
            ps_lbl20.Text = txtinfo.ToTitleCase(gt_lng.Rows[17][0].ToString().ToLower());
            ps_lbl21.Text = txtinfo.ToTitleCase(gt_lng.Rows[21][0].ToString().ToLower());
            ps_lbl22.Text = txtinfo.ToTitleCase(gt_lng.Rows[18][0].ToString().ToLower());
            ps_lbl23.Text = txtinfo.ToTitleCase(gt_lng.Rows[19][0].ToString().ToLower());
            Button8.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
           
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    void sectionBind()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select UPPER(cse_section_desc) as cse_section_desc,cse_section_cd from hr_cmn_appr_section ORDER by cse_section_cd";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_bahag.DataSource = dt;
            dd_bahag.DataTextField = "cse_section_desc";
            dd_bahag.DataValueField = "cse_section_cd";
            dd_bahag.DataBind();
            dd_bahag.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void sel_section(object sender, EventArgs e)
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select csb_section_cd,csb_subject_cd,UPPER(csb_subject_desc) as csb_subject_desc from hr_cmn_subject where csb_section_cd='" + dd_bahag.SelectedValue + "' order by csb_subject_cd";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_subject.DataSource = dt;
            dd_subject.DataTextField = "csb_subject_desc";
            dd_subject.DataValueField = "csb_subject_cd";
            dd_subject.DataBind();
            dd_subject.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            grid_kpr();
            grid_jmk();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    void view_details()
    {
        try
        {
            if (Kaki_no.Text != "")
            {
                //DataTable select_sup = new DataTable();
                //if (Session["level"].ToString() == "5" && Session["level_acc"].ToString() == "MHR" || Session["level"].ToString() == "6")
                //{
                //    select_sup = DBCon.Ora_Execute_table("select * from hr_post_his where pos_end_dt='9999-12-31' and pos_staff_no='" + Kaki_no.Text + "'");
                //    Button8.Attributes.Add("style", "display:none;");
                //}
                //else
                //{
                //    select_sup = DBCon.Ora_Execute_table("select * from hr_post_his where pos_spv_name2 = '" + Session["New"].ToString() + "' and pos_end_dt='9999-12-31' and pos_staff_no='" + Kaki_no.Text + "'");
                //    Button8.Attributes.Remove("style");
                //}


                //if (select_sup.Rows.Count != 0)
                //{
                    DataTable select_kaki = new DataTable();
                    select_kaki = DBCon.Ora_Execute_table("select sp.stf_staff_no,sp.stf_name,hg.hr_gred_desc,hj.hr_jaba_desc,rhj.hr_jaw_desc,pk.hr_kate_desc,hu.hr_unit_desc,ho.org_name from hr_staff_profile as sp left join Ref_hr_gred as hg on hg.hr_gred_Code=sp.stf_curr_grade_cd left join Ref_hr_jabatan as hj on hj.hr_jaba_Code=sp.stf_curr_dept_cd left join Ref_hr_Jawatan as rhj on rhj.hr_jaw_Code=sp.stf_curr_post_cd left join Ref_hr_penj_kategori as pk on pk.hr_kate_Code=sp.stf_curr_job_cat_cd left join Ref_hr_unit as hu on hu.hr_unit_Code=sp.stf_curr_unit_cd left join hr_organization ho on ho.org_gen_id=sp.str_curr_org_cd where sp.stf_staff_no='" + Kaki_no.Text + "'");
                    if (select_kaki.Rows.Count != 0)
                    {
                        s_nama.Text = select_kaki.Rows[0]["stf_name"].ToString();
                        s_gred.Text = select_kaki.Rows[0]["hr_gred_desc"].ToString();
                        s_jab.Text = select_kaki.Rows[0]["hr_jaba_desc"].ToString();
                        s_jaw.Text = select_kaki.Rows[0]["hr_jaw_desc"].ToString();
                        s_kj.Text = select_kaki.Rows[0]["hr_kate_desc"].ToString();
                        s_unit.Text = select_kaki.Rows[0]["hr_unit_desc"].ToString();
                        txt_org.Text = select_kaki.Rows[0]["org_name"].ToString();

                        string cyr1 = (double.Parse(DateTime.Now.ToString("yyyy")) - 1).ToString();

                        DataTable select_apr = new DataTable();
                        select_apr = DBCon.Ora_Execute_table("select sap_staff_no,ISNULL(sap_staff_remark,'') as sap_staff_remark from hr_staff_appraisal where sap_staff_no='" + select_kaki.Rows[0]["stf_staff_no"].ToString() + "' and YEAR(sap_start_dt) ='" + cyr1 + "'");

                        if (select_apr.Rows.Count != 0)
                        {
                            ul_tarea.Value = select_apr.Rows[0]["sap_staff_remark"].ToString();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Kakitangan Masih Belum Mengisi Borang Penilaian Prestasi',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                        }

                        grid_kpr();
                        grid_jmk();
                    }
                    else
                    {
                        grid_kpr();
                        grid_jmk();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                //}
                //else
                //{
                //    grid_kpr();
                //    grid_jmk();
                //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Transaksi Tidak Dibenarkan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                //}
            }
            else
            {
                grid_kpr();
                grid_jmk();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan yang Maklumat',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {
            grid_kpr();
            grid_jmk();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }

    }


    protected void srch_kpp(object sender, EventArgs e)
    {
        try
        {
            if (Kaki_no.Text != "")
            {
                if (dd_bahag.SelectedValue != "" || dd_subject.SelectedValue == "")
                {
                    grid_kpr();
                    grid_jmk();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan yang Maklumat',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    grid_kpr();
                    grid_jmk();
                }
            }
            else
            {
                grid_kpr();
                grid_jmk();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan yang Maklumat',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {
            grid_kpr();
            grid_jmk();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }

    }


    protected void click_insert(object sender, EventArgs e)
    {
        try
        {
            if (Kaki_no.Text != "")
            {
                cyr1 = (double.Parse(DateTime.Now.ToString("yyyy")) - 1).ToString();

                foreach (GridViewRow gvrow in GridView3.Rows)
                {
                    var res_1 = (System.Web.UI.WebControls.Label)gvrow.FindControl("stno");
                    var res_2 = (System.Web.UI.WebControls.Label)gvrow.FindControl("sdt");
                    DateTime dt_2 = DateTime.ParseExact(res_2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string stdt = dt_2.ToString("yyyy-MM-dd");
                    var res_3 = (System.Web.UI.WebControls.Label)gvrow.FindControl("edt");
                    DateTime et_2 = DateTime.ParseExact(res_3.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string etdt = et_2.ToString("yyyy-MM-dd");

                    var res_4 = (System.Web.UI.WebControls.Label)gvrow.FindControl("spcd");
                    var res_5 = (System.Web.UI.WebControls.Label)gvrow.FindControl("ssccd");
                    var res_6 = (System.Web.UI.WebControls.Label)gvrow.FindControl("ssbcd");
                    var res_7 = (System.Web.UI.WebControls.Label)gvrow.FindControl("ssqno");
                    var res_ppp = (System.Web.UI.WebControls.Label)gvrow.FindControl("s_ppp");
                    var res_ppk = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("s_ppk");

                    string tt_avg = ((double.Parse(res_ppp.Text) + double.Parse(res_ppk.Text)) / 2).ToString();

                    DataTable select_stapp = new DataTable();
                    select_stapp = DBCon.Ora_Execute_table("select * from hr_staff_appraisal where sap_staff_no='" + res_1.Text + "' and sap_post_cat_cd='" + res_4.Text + "' and sap_start_dt='" + stdt + "' and sap_end_dt='" + etdt + "' and sap_post_cat_cd='" + res_4.Text + "' and sap_section_cd='" + res_5.Text + "' and sap_subject_cd='" + res_6.Text + "' and sap_seq_no='" + res_7.Text + "' and year(sap_start_dt)='" + cyr1 + "'");
                    if (select_stapp.Rows.Count != 0)
                    {
                        DBCon.Execute_CommamdText("UPDATE hr_staff_appraisal SET sap_ppk_score='" + res_ppk.Text + "',sap_avg_score='" + tt_avg + "',sap_upd_id='" + Session["New"].ToString() + "',sap_upd_dt='" + DateTime.Now + "' where sap_staff_no='" + res_1.Text + "' and year(sap_start_dt)='" + cyr1 + "' and sap_post_cat_cd='" + res_4.Text + "' and sap_start_dt='" + stdt + "' and sap_end_dt='" + etdt + "' and sap_section_cd='" + res_5.Text + "' and sap_subject_cd='" + res_6.Text + "' and sap_seq_no='" + res_7.Text + "'");
                    }
                }
                DataTable select_stapp1 = new DataTable();
                select_stapp1 = DBCon.Ora_Execute_table("select DISTINCT sa.sap_staff_no,sa.sap_start_dt,sa.sap_end_dt,UPPER(sp.stf_name) as stf_name,sp.str_curr_org_cd,sp.stf_curr_dept_cd,sp.stf_curr_unit_cd,sp.stf_curr_post_cd,sp.stf_curr_job_cat_cd from hr_staff_appraisal as sa left join hr_staff_profile as sp on sp.stf_staff_no=sa.sap_staff_no where sap_staff_no='" + Kaki_no.Text + "' and year(sap_start_dt)='" + cyr1 + "'");
                DataTable select_staps = new DataTable();
                select_staps = DBCon.Ora_Execute_table("select * from hr_appraisal_summary where aps_staff_no='" + select_stapp1.Rows[0]["sap_staff_no"].ToString() + "' and aps_start_dt='" + select_stapp1.Rows[0]["sap_start_dt"].ToString() + "' and aps_end_dt='" + select_stapp1.Rows[0]["sap_end_dt"].ToString() + "'");
                DataTable select_sumapp = new DataTable();
                select_sumapp = DBCon.Ora_Execute_table("select ss.sap_section_cd,aca.cse_section_desc,sum(ss.sap_ppp_score) as ppp, sum(ISNULL(CONVERT(INT ,ss.sap_ppk_score),'')) as ppk,sum(ss.sap_weightage) as wt,cast(CONVERT(varchar ,((sum(ss.sap_ppp_score) + sum(ISNULL(CONVERT(INT ,ss.sap_ppk_score),''))) / 2)) as decimal(10,1)) as pro from hr_staff_appraisal as ss left join hr_cmn_appr_section as aca on aca.cse_section_cd=ss.sap_section_cd where sap_section_cd IN (select cse_section_cd from hr_cmn_appr_section) and sap_staff_no='" + Kaki_no.Text + "' and year(sap_start_dt)='" + cyr1 + "' group by ss.sap_section_cd,aca.cse_section_desc order by sap_section_cd");

                string vv1 = string.Empty, vv2 = string.Empty, vv3 = string.Empty, vv4 = string.Empty, tot_vv = string.Empty;
                if (select_sumapp.Rows.Count >= 1 && select_sumapp.Rows[0]["pro"].ToString() != "")
                {

                    vv1 = select_sumapp.Rows[0]["pro"].ToString();
                }
                else
                {
                    vv1 = "0";
                }

                if (select_sumapp.Rows.Count >= 2 && select_sumapp.Rows[1]["pro"].ToString() != "")
                {
                    vv2 = select_sumapp.Rows[1]["pro"].ToString();
                }
                else
                {
                    vv2 = "0";
                }

                if (select_sumapp.Rows.Count >= 3 && select_sumapp.Rows[2]["pro"].ToString() != "")
                {
                    vv3 = select_sumapp.Rows[2]["pro"].ToString();
                }
                else
                {
                    vv3 = "0";
                }
                if (select_sumapp.Rows.Count >= 4 && select_sumapp.Rows[3]["pro"].ToString() != "")
                {
                    vv4 = select_sumapp.Rows[3]["pro"].ToString();
                }
                else
                {
                    vv4 = "0";
                }

                tot_vv = (double.Parse(vv1) + double.Parse(vv2) + double.Parse(vv3) + double.Parse(vv4)).ToString();

                if (select_staps.Rows.Count == 0)
                {
                    DBCon.Execute_CommamdText("INSERT INTO hr_appraisal_summary (aps_staff_no,aps_start_dt,aps_end_dt,aps_staff_name,aps_org_id,aps_dept_cd,aps_unit_cd,aps_post_cd,aps_post_cat_cd,aps_section1_score,aps_section2_score,aps_section3_score,aps_section4_score,aps_total_score,aps_crt_id,aps_crt_dt) values ('" + select_stapp1.Rows[0]["sap_staff_no"].ToString() + "','" + select_stapp1.Rows[0]["sap_start_dt"].ToString() + "','" + select_stapp1.Rows[0]["sap_end_dt"].ToString() + "','" + select_stapp1.Rows[0]["stf_name"].ToString() + "','" + select_stapp1.Rows[0]["str_curr_org_cd"].ToString() + "','" + select_stapp1.Rows[0]["stf_curr_dept_cd"].ToString() + "','" + select_stapp1.Rows[0]["stf_curr_unit_cd"].ToString() + "','" + select_stapp1.Rows[0]["stf_curr_post_cd"].ToString() + "','" + select_stapp1.Rows[0]["stf_curr_job_cat_cd"].ToString() + "','" + vv1 + "','" + vv2 + "','" + vv3 + "','" + vv4 + "','" + tot_vv + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "')");
                }
                else
                {
                    DBCon.Execute_CommamdText("UPDATE hr_appraisal_summary SET aps_org_id='" + select_stapp1.Rows[0]["str_curr_org_cd"].ToString() + "',aps_dept_cd='" + select_stapp1.Rows[0]["stf_curr_dept_cd"].ToString() + "',aps_unit_cd='" + select_stapp1.Rows[0]["stf_curr_unit_cd"].ToString() + "',aps_post_cd='" + select_stapp1.Rows[0]["stf_curr_post_cd"].ToString() + "',aps_post_cat_cd='" + select_stapp1.Rows[0]["stf_curr_job_cat_cd"].ToString() + "',aps_section1_score='" + vv1 + "',aps_section2_score='" + vv2 + "',aps_section3_score='" + vv3 + "',aps_section4_score='" + vv4 + "',aps_total_score='" + tot_vv + "',aps_upd_id='" + Session["New"].ToString() + "',aps_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "' where aps_staff_no='" + select_stapp1.Rows[0]["sap_staff_no"].ToString() + "' and aps_start_dt='" + select_stapp1.Rows[0]["sap_start_dt"].ToString() + "' and aps_end_dt='" + select_stapp1.Rows[0]["sap_end_dt"].ToString() + "'");
                }                
                grid_kpr();
                grid_jmk();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
            else
            {
                grid_kpr();
                grid_jmk();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan yang Maklumat',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {
            grid_kpr();
            grid_jmk();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }

    }

    void grid_kpr()
    {

        DataTable select_sup = new DataTable();
        if (Session["level"].ToString() == "5" && Session["level_acc"].ToString() == "MHR" || Session["level"].ToString() == "6")
        {
            select_sup = DBCon.Ora_Execute_table("select * from hr_post_his where pos_end_dt='9999-12-31' and pos_staff_no='" + Kaki_no.Text + "'");
        }
        else
        {
            select_sup = DBCon.Ora_Execute_table("select * from hr_post_his where pos_spv_name2 = '" + Session["New"].ToString() + "' and pos_end_dt='9999-12-31' and pos_staff_no='" + Kaki_no.Text + "'");
        }


        DataTable ddicno_kaki = new DataTable();
        ddicno_kaki = DBCon.Ora_Execute_table("select stf_staff_no,stf_curr_post_cd,stf_curr_unit_cd From hr_staff_profile where stf_staff_no='" + Kaki_no.Text + "' ");
        string cyr1 = (double.Parse(DateTime.Now.ToString("yyyy")) - 1).ToString();

        if (Kaki_no.Text != "" && dd_bahag.SelectedValue != "" && dd_subject.SelectedValue == "")
        {
            val1 = "select BH.cse_section_desc,SB.csb_subject_desc,sa.sap_weightage,sa.sap_ppp_score,sa.sap_ppk_score,sa.sap_start_dt,sa.sap_end_dt,sa.sap_staff_no,sa.sap_post_cat_cd,sa.sap_section_cd,sa.sap_subject_cd,sa.sap_seq_no from hr_staff_appraisal as sa left join hr_cmn_appr_section as BH on BH.cse_section_cd=sa.sap_section_cd left join hr_cmn_subject as SB on SB.csb_subject_cd=sa.sap_subject_cd where sa.sap_staff_no='" + Kaki_no.Text + "' and YEAR(sa.sap_start_dt) ='" + cyr1 + "' and sa.sap_section_cd='" + dd_bahag.SelectedValue + "' and sap_post_cat_cd='" + ddicno_kaki.Rows[0]["stf_curr_post_cd"].ToString() + "' and sap_unit_cd='" + ddicno_kaki.Rows[0]["stf_curr_unit_cd"].ToString() + "'";
        }
        else if (Kaki_no.Text != "" && dd_bahag.SelectedValue == "" && dd_subject.SelectedValue != "")
        {
            val1 = "select BH.cse_section_desc,SB.csb_subject_desc,sa.sap_weightage,sa.sap_ppp_score,sa.sap_ppk_score,sa.sap_start_dt,sa.sap_end_dt,sa.sap_staff_no,sa.sap_post_cat_cd,sa.sap_section_cd,sa.sap_subject_cd,sa.sap_seq_no from hr_staff_appraisal as sa left join hr_cmn_appr_section as BH on BH.cse_section_cd=sa.sap_section_cd left join hr_cmn_subject as SB on SB.csb_subject_cd=sa.sap_subject_cd where sa.sap_staff_no='" + Kaki_no.Text + "' and YEAR(sa.sap_start_dt) ='" + cyr1 + "' and sa.sap_subject_cd='" + dd_subject.SelectedValue + "' and sap_post_cat_cd='" + ddicno_kaki.Rows[0]["stf_curr_post_cd"].ToString() + "' and sap_unit_cd='" + ddicno_kaki.Rows[0]["stf_curr_unit_cd"].ToString() + "'";
        }
        else if (Kaki_no.Text != "" && dd_bahag.SelectedValue != "" && dd_subject.SelectedValue != "")
        {
            val1 = "select BH.cse_section_desc,SB.csb_subject_desc,sa.sap_weightage,sa.sap_ppp_score,sa.sap_ppk_score,sa.sap_start_dt,sa.sap_end_dt,sa.sap_staff_no,sa.sap_post_cat_cd,sa.sap_section_cd,sa.sap_subject_cd,sa.sap_seq_no from hr_staff_appraisal as sa left join hr_cmn_appr_section as BH on BH.cse_section_cd=sa.sap_section_cd left join hr_cmn_subject as SB on SB.csb_subject_cd=sa.sap_subject_cd where sa.sap_staff_no='" + Kaki_no.Text + "' and YEAR(sa.sap_start_dt) ='" + cyr1 + "' and sa.sap_section_cd='" + dd_bahag.SelectedValue + "' and sa.sap_subject_cd='" + dd_subject.SelectedValue + "' and sap_post_cat_cd='" + ddicno_kaki.Rows[0]["stf_curr_post_cd"].ToString() + "' and sap_unit_cd='" + ddicno_kaki.Rows[0]["stf_curr_unit_cd"].ToString() + "'";
        }
        else if (Kaki_no.Text != "" && select_sup.Rows.Count != 0)
        {
            val1 = "select BH.cse_section_desc,SB.csb_subject_desc,sa.sap_weightage,sa.sap_ppp_score,sa.sap_ppk_score,sa.sap_start_dt,sa.sap_end_dt,sa.sap_staff_no,sa.sap_post_cat_cd,sa.sap_section_cd,sa.sap_subject_cd,sa.sap_seq_no from hr_staff_appraisal as sa left join hr_cmn_appr_section as BH on BH.cse_section_cd=sa.sap_section_cd left join hr_cmn_subject as SB on SB.csb_subject_cd=sa.sap_subject_cd where sa.sap_staff_no='" + Kaki_no.Text + "' and YEAR(sa.sap_start_dt) ='" + cyr1 + "' and sap_post_cat_cd='" + ddicno_kaki.Rows[0]["stf_curr_post_cd"].ToString() + "' and sap_unit_cd='" + ddicno_kaki.Rows[0]["stf_curr_unit_cd"].ToString() + "'";
        }
        else
        {
            val1 = "select BH.cse_section_desc,SB.csb_subject_desc,sa.sap_weightage,sa.sap_ppp_score,sa.sap_ppk_score,sa.sap_start_dt,sa.sap_end_dt,sa.sap_staff_no,sa.sap_post_cat_cd,sa.sap_section_cd,sa.sap_subject_cd,sa.sap_seq_no from hr_staff_appraisal as sa left join hr_cmn_appr_section as BH on BH.cse_section_cd=sa.sap_section_cd left join hr_cmn_subject as SB on SB.csb_subject_cd=sa.sap_subject_cd where sa.sap_staff_no=''";
        }

        cyr1 = (double.Parse(DateTime.Now.ToString("yyyy")) - 1).ToString();

        DataTable sum_vl = new DataTable();
        sum_vl = DBCon.Ora_Execute_table("select sum(sap_weightage) as sw,sum(ISNULL(CONVERT(INT ,sap_ppp_score),'')) as ppp,sum(ISNULL(CONVERT(INT ,sap_ppk_score),'')) as ppk from hr_staff_appraisal as sa where sa.sap_staff_no='" + Kaki_no.Text + "' and YEAR(sa.sap_start_dt) ='" + cyr1 + "'");
        if (select_sup.Rows.Count != 0)
        {
            if (sum_vl.Rows.Count != 0)
            {
                TextBox2.Text = sum_vl.Rows[0]["sw"].ToString();

                string s1 = string.Empty, s2 = string.Empty;
                if (sum_vl.Rows[0]["ppp"].ToString() != "")
                {
                    s1 = sum_vl.Rows[0]["ppp"].ToString();
                }
                else
                {
                    s1 = "0";
                }
                if (sum_vl.Rows[0]["ppk"].ToString() != "")
                {
                    s2 = sum_vl.Rows[0]["ppk"].ToString();
                }
                else
                {
                    s2 = "0";
                }
                //TextBox4.Text = ((double.Parse(s1) + double.Parse(s2)) / 2).ToString();
                TextBox4.Text = ((double.Parse(s1) + double.Parse(s2))).ToString();
            }
            else
            {
                TextBox2.Text = "0";
                TextBox4.Text = "0";
            }

        }
        else
        {
            TextBox2.Text = "0";
            TextBox4.Text = "0";
        }

        SqlCommand cmd2 = new SqlCommand("" + val1 + "", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            GridView3.DataSource = ds2;
            GridView3.DataBind();
            int columncount = GridView3.Rows[0].Cells.Count;
            GridView3.Rows[0].Cells.Clear();
            GridView3.Rows[0].Cells.Add(new TableCell());
            GridView3.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView3.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
        }
        else
        {
            GridView3.DataSource = ds2;
            GridView3.DataBind();
        }
    }
    protected void gvSelected_PageIndexChanging_out(object sender, GridViewPageEventArgs e)
    {
        GridView3.PageIndex = e.NewPageIndex;
        grid_kpr();

    }

    protected void gvEmp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (DataBinder.Eval(e.Row.DataItem, "sap_weightage") != DBNull.Value)
            {
                Decimal ss1 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "sap_weightage"));
                total += ss1;
            }

            if (DataBinder.Eval(e.Row.DataItem, "sap_ppp_score") != DBNull.Value)
            {
                Decimal ss2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "sap_ppp_score"));
                total1 += ss2;
            }
            if (DataBinder.Eval(e.Row.DataItem, "sap_ppk_score") != DBNull.Value)
            {
                Decimal ss3 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "sap_ppk_score"));
                total2 += ss3;


                System.Web.UI.WebControls.TextBox Label1_txt = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("s_ppk");
                System.Web.UI.WebControls.Label Label1_lbl = (System.Web.UI.WebControls.Label)e.Row.FindControl("s_ppk_1");
                if (Session["level"].ToString() == "2" && Session["level_acc"].ToString() == "PE2")
                {
                    Label1_txt.Visible = true;
                    Label1_lbl.Visible = false;
                }
                else
                {
                    Label1_txt.Visible = false;
                    Label1_lbl.Visible = true;
                }
            }


        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            //System.Web.UI.WebControls.Label lblamount = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblTotal");
            TextBox1.Text = total.ToString();
            //TextBox3.Text = ((double.Parse(total1.ToString()) + double.Parse(total2.ToString())) / 2).ToString();
            TextBox3.Text = ((double.Parse(total1.ToString()) + double.Parse(total2.ToString()))).ToString();

        }
    }

    protected void gvEmp_RowDataBound_jmk(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (DataBinder.Eval(e.Row.DataItem, "wt") != DBNull.Value)
            {
                Decimal ss = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "wt"));
                total_jmk1 += ss;
            }
            if (DataBinder.Eval(e.Row.DataItem, "ppp") != DBNull.Value)
            {
                Decimal ss1 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ppp"));
                total_jmk2 += ss1;
            }
            if (DataBinder.Eval(e.Row.DataItem, "ppk") != DBNull.Value)
            {
                Decimal ss2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ppk"));
                total_jmk3 += ss2;
            }
            if (DataBinder.Eval(e.Row.DataItem, "pro") != DBNull.Value)
            {
                Decimal ss3 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "pro"));
                total_jmk4 += ss3;
            }




        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            System.Web.UI.WebControls.Label lblamount_wt = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblTotal_wt");
            lblamount_wt.Text = total_jmk1.ToString();

            System.Web.UI.WebControls.Label lblamount_ppp = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblTotal_ppp");
            lblamount_ppp.Text = total_jmk2.ToString();

            //if (lblamount_ppp.Text == "0")
            //{
            //    Button8.Attributes.Add("style", "display:none;");
            //}
            //else
            //{
            //    Button8.Attributes.Remove("style");
            //}

            System.Web.UI.WebControls.Label lblamount_ppk = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblTotal_ppk");
            lblamount_ppk.Text = total_jmk3.ToString();

            System.Web.UI.WebControls.Label lblamount_pro = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblTotal_pro");
            lblamount_pro.Text = total_jmk4.ToString();

        }

    }

    void grid_jmk()
    {
        string gjmk1 = string.Empty, gjmk2 = string.Empty;
        DataTable select_sup = new DataTable();
        if (Session["level"].ToString() == "5" && Session["level_acc"].ToString() == "MHR" || Session["level"].ToString() == "6")
        {
            select_sup = DBCon.Ora_Execute_table("select * from hr_post_his where pos_end_dt='9999-12-31' and pos_staff_no='" + Kaki_no.Text + "'");
        }
        else
        {
            select_sup = DBCon.Ora_Execute_table("select * from hr_post_his where pos_spv_name2 = '" + Session["New"].ToString() + "' and pos_end_dt='9999-12-31' and pos_staff_no='" + Kaki_no.Text + "'");
        }
        if (select_sup.Rows.Count != 0)
        {
            gjmk1 = Kaki_no.Text;
        }
        string cyr1 = (double.Parse(DateTime.Now.ToString("yyyy")) - 1).ToString();
        SqlCommand cmd2 = new SqlCommand("select ss.sap_section_cd,aca.cse_section_desc,sum(ISNULL(CONVERT(INT ,ss.sap_ppp_score),'')) as ppp, sum(ISNULL(CONVERT(INT ,ss.sap_ppk_score),'')) as ppk,sum(ss.sap_weightage) as wt,cast(CONVERT(varchar ,((sum(ISNULL(CONVERT(INT ,ss.sap_ppp_score),'')) + sum(ISNULL(CONVERT(INT ,ss.sap_ppk_score),''))) / 2)) as decimal(10,1)) as pro from hr_staff_appraisal as ss left join hr_cmn_appr_section as aca on aca.cse_section_cd=ss.sap_section_cd where sap_section_cd IN (select cse_section_cd from hr_cmn_appr_section) and sap_staff_no='" + gjmk1 + "' and year(sap_start_dt)='" + cyr1 + "' group by ss.sap_section_cd,aca.cse_section_desc ORDER BY ss.sap_section_cd", con);
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
            GridView1.FooterRow.Cells[1].Text = "<strong>JUMLAH BESAR</strong>";
            GridView1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;
        }
        else
        {
            GridView1.DataSource = ds2;
            GridView1.DataBind();
            GridView1.FooterRow.Cells[1].Text = "<strong>JUMLAH BESAR</strong>";
            GridView1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;
        }
    }
    protected void gvSelected_PageIndexChanging_jmk(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        grid_jmk();

    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../SUMBER_MANUSIA/HR_KM_pen_prestasi_view.aspx");
    }

}