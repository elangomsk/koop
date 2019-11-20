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
using System.Xml;

public partial class hr_gaji : System.Web.UI.Page
{
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    DataTable dtt1 = new DataTable();
    string perk_tot = string.Empty, fxa_tot = string.Empty, xta_tot = string.Empty, tot_jenis = string.Empty, cb1 = string.Empty, cb2 = string.Empty, value = string.Empty;
    string pp_amt = string.Empty, pk_amt = string.Empty;
    string srch_dt = string.Empty;
    string act_dt = string.Empty;
    string tt_amt1, userid;
    string sdd = string.Empty, b_slry = string.Empty, cc1 = string.Empty, cc2 = string.Empty, cc3 = string.Empty, cc4 = string.Empty, cc5 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        app_language();
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;
                month();

                txt_org.Attributes.Add("Readonly", "Readonly");
                TextBox8.Attributes.Add("Readonly", "Readonly");
                TextBox20.Attributes.Add("Readonly", "Readonly");
                txt_pokok.Attributes.Add("Readonly", "Readonly");
                TextBox15.Attributes.Add("Readonly", "Readonly");
                TextBox3.Attributes.Add("Readonly", "Readonly");
                TextBox2.Attributes.Add("Readonly", "Readonly");
                txt_stffno.Attributes.Add("Readonly", "Readonly");

                //MAKLUMAT POTONGAN

                TextBox11.Attributes.Add("Readonly", "Readonly");
                TextBox12.Attributes.Add("Readonly", "Readonly");
                TextBox9.Attributes.Add("Readonly", "Readonly");
                TextBox10.Attributes.Add("Readonly", "Readonly");
                //TextBox13.Attributes.Add("Readonly", "Readonly");
                TextBox17.Attributes.Add("Readonly", "Readonly");
                //TextBox18.Attributes.Add("Readonly", "Readonly");
                TextBox23.Attributes.Add("Readonly", "Readonly");
                TextBox24.Attributes.Add("Readonly", "Readonly");


                DD_PBB.SelectedValue = DateTime.Now.ToString("MM");
                txt_tahu.Text = DateTime.Now.ToString("yyyy");
                act_dt = txt_tahu.Text + "-" + DD_PBB.SelectedValue;
                call_grd();
                if (samp != "")
                {
                    txt_stffno.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    Applcn_no1.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    view_details();

                }
                else
                {

                }
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
        ste_set = DBCon.Ora_Execute_table("select * from site_settings where ID IN ('1')");

        DataTable gt_lng = new DataTable();
        gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('455','448','505','484','77','1565','513','1675','497','1288','190','1710','502','1727','1708','1716','1730','1718','1719','1731','1732','1733','1735','1736','1737','1738','1741','1742','1743','1744','1745','1746','1747','61') order by ID ASC");

        CultureInfo culinfo = Thread.CurrentThread.CurrentUICulture;
        TextInfo txtinfo = culinfo.TextInfo;

          h1_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
           bb1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
           bb2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());

           h3_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
           h3_tag3.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[18][0].ToString().ToLower());
           h3_tag4.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower());
           h3_tag5.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[19][0].ToString().ToLower());
           //h3_tag6.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[16][0].ToString().ToLower());
           h3_tag7.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[17][0].ToString().ToLower());
           h3_tag2.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[20][0].ToString().ToLower());

           lbl1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
           lbl2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
           lbl3_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
           lbl4_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
           lbl5_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
           lbl6_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
           lbl7_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
           lbl8_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());
           lbl9_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
           lbl10_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower() + " (RM)");
           lbl11_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[21][0].ToString().ToLower()+" (RM)");
           lbl12_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[22][0].ToString().ToLower());
           lbl13_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[23][0].ToString().ToLower()+ " (RM)");

           lbl14_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[24][0].ToString().ToLower());
           lbl15_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[25][0].ToString().ToLower()+ " (RM)");
           //lbl16_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[26][0].ToString().ToLower());
           //lbl17_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[27][0].ToString().ToLower()+ " (RM)");
           //lbl18_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[28][0].ToString().ToLower()+ " (RM)");
           lbl19_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[29][0].ToString().ToLower()+ " (RM)");
           lbl20_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[30][0].ToString().ToLower()+ " (RM)");
           lbl21_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[31][0].ToString().ToLower()+ " (RM)");
           lbl22_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[32][0].ToString().ToLower()+ " (RM)");
           lbl23_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[33][0].ToString().ToLower()+ " (RM)");

           Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
           btn_simp.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
           Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    void view_details()
    {
        DataSet Ds = new DataSet();
        try
        {
            if (txt_stffno.Text != "")
            {
                act_dt = txt_tahu.Text + "-" + DD_PBB.SelectedValue;
                DataTable ddicno = new DataTable();
                ddicno = DBCon.Ora_Execute_table("select stf_staff_no From hr_staff_profile where '" + txt_stffno.Text + "' IN (stf_staff_no)");
                string stffno = ddicno.Rows[0]["stf_staff_no"].ToString();
                if (ddicno.Rows.Count > 0)
                {
                    Applcn_no1.Text = ddicno.Rows[0]["stf_staff_no"].ToString();
                    DataTable ddbind = new DataTable();
                    //ddbind = DBCon.Ora_Execute_table("select a.stf_staff_no,a.stf_name,ISNULL(hj.hr_jaba_desc,'') as hr_jaba_desc,ISNULL(hg.hr_gred_desc,'') as hr_gred_desc,ISNULL(rhj.hr_jaw_desc,'') as hr_jaw_desc from (select * from hr_staff_profile as sp where sp.stf_staff_no='" + txt_stffno.Text + "') as a full outer join(select * from hr_post_his where pos_staff_no='" + txt_stffno.Text + "' and pos_end_dt='9999-12-31') as b left join Ref_hr_jabatan as hj on hj.hr_jaba_Code=b.pos_dept_cd left join Ref_hr_gred as hg on hg.hr_gred_Code=b.pos_grade_cd left join Ref_hr_Jawatan as rhj on rhj.hr_jaw_Code=b.pos_post_cd on b.pos_staff_no=a.stf_staff_no left join Ref_Nama_Bank as rnb on rnb.Bank_Code=a.stf_bank_cd");

                    ddbind = DBCon.Ora_Execute_table("select sp.stf_staff_no,sp.stf_name,ISNULL(hj.hr_jaba_desc,'') as hr_jaba_desc,ISNULL(hg.hr_gred_desc,'') as hr_gred_desc,ISNULL(rhj.hr_jaw_desc,'') as hr_jaw_desc,ho.org_name,o1.op_perg_name from hr_staff_profile as sp left join hr_post_his ph on ph.pos_staff_no=sp.stf_staff_no and pos_end_dt='9999-12-31' left join Ref_hr_jabatan as hj on hj.hr_jaba_Code=ph.pos_dept_cd left join Ref_hr_gred as hg on hg.hr_gred_Code=ph.pos_grade_cd left join Ref_hr_Jawatan as rhj on rhj.hr_jaw_Code=ph.pos_post_cd left join Ref_Nama_Bank as rnb on rnb.Bank_Code=sp.stf_bank_cd left join hr_organization ho on ho.org_gen_id=str_curr_org_cd left join hr_organization_pern o1 on o1.op_perg_code=stf_cur_sub_org where sp.stf_staff_no='" + Applcn_no1.Text + "'");

                    DataTable chk_income = new DataTable();
                    chk_income = DBCon.Ora_Execute_table("select * from hr_income where inc_staff_no='" + Applcn_no1.Text + "' and inc_month='" + DD_PBB.SelectedValue + "' and inc_year='" + txt_tahu.Text + "' ");
                    if(chk_income.Rows.Count == 0)
                    {
                        btn_simp.Text = "Simpan";
                    }
                    else
                    {
                        btn_simp.Text = "Kemaskini";
                    }
                    txt_org.Text = ddbind.Rows[0]["org_name"].ToString();
                    txt_gred.Text = ddbind.Rows[0]["hr_gred_desc"].ToString();
                    txt_nama.Text = ddbind.Rows[0]["stf_name"].ToString();
                    txt_jawa.Text = ddbind.Rows[0]["hr_jaw_desc"].ToString();
                    txt_jaba.Text = ddbind.Rows[0]["hr_jaba_desc"].ToString();
                    TextBox15.Text = ddbind.Rows[0]["op_perg_name"].ToString();
                    //TextBox22.Text = ddbind.Rows[0]["pos_post_cd"].ToString();
                    //srch_dt = txt_tahu.Text +"-"+ DD_PBB.SelectedValue + "-01";

                    DataTable dd_hrsal = new DataTable();
                    dd_hrsal = DBCon.Ora_Execute_table("select * from hr_salary where slr_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt + "') between FORMAT(slr_eff_dt,'yyyy-MM') And FORMAT(slr_end_dt,'yyyy-MM')");
                    if (dd_hrsal.Rows.Count != 0)
                    {
                        if (dd_hrsal.Rows.Count == 2)
                        {
                            DataTable dd_hrsal_tot = new DataTable();
                            dd_hrsal_tot = DBCon.Ora_Execute_table("select *,day(slr_eff_dt) d1,day(slr_end_dt) d2,datediff(day, slr_eff_dt, dateadd(month, 1, slr_eff_dt)) as tdays,datediff(day, DATEADD(mm, 1, slr_eff_dt), dateadd(month, 1, DATEADD(mm, 1, slr_eff_dt))) as tdays1,((day(slr_end_dt)) * slr_salary_amt) / datediff(day, DATEADD(mm, 1, slr_eff_dt), dateadd(month, 1, DATEADD(mm, 1, slr_eff_dt))) as a,(((datediff(day, slr_eff_dt, dateadd(month, 1, slr_eff_dt)) - day(slr_eff_dt)) + 1) * slr_salary_amt) / datediff(day, slr_eff_dt, dateadd(month, 1, slr_eff_dt)) as b,((datediff(day, slr_eff_dt, dateadd(month, 1, slr_eff_dt)) - day(slr_eff_dt)) * slr_salary_amt) / datediff(day, slr_eff_dt, dateadd(month, 1, slr_eff_dt)) as bsal from hr_salary where slr_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt + "') between FORMAT(slr_eff_dt,'yyyy-MM') And FORMAT(slr_end_dt,'yyyy-MM')");
                            string aa = (double.Parse(dd_hrsal_tot.Rows[0]["a"].ToString()) + double.Parse(dd_hrsal_tot.Rows[1]["b"].ToString())).ToString("C").Replace("$", "").Replace("RM", "");
                            txt_pokok.Text = aa;
                        }
                        else
                        {
                            DataTable dd_hrsal_1 = new DataTable();
                            dd_hrsal_1 = DBCon.Ora_Execute_table("select slr_salary_amt from hr_salary where slr_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt + "') between FORMAT(slr_eff_dt,'yyyy-MM') And FORMAT(slr_end_dt,'yyyy-MM')");
                            txt_pokok.Text = double.Parse(dd_hrsal_1.Rows[0]["slr_salary_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                        }
                    }
                    else
                    {
                        txt_pokok.Text = "0.00";
                    }
                    //decimal v1 = decimal.Parse(dd_hrsal.Rows[0]["slr_salary_amt"].ToString());
                    //txt_pokok.Text = v1.ToString("C").Replace("$", "").Replace("RM", "");
                    PERKESO();
                    call_grd();

                    tt();
                    //cumulative();
                }
                else
                {
                    call_grd();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                call_grd();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Sila Masukkan Input Carian',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            call_grd();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Carian Error',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }


    protected void sel_bagi_bulan(object sender, EventArgs e)
    {
        view_details();
    }
        void month()
    {
        DataSet Ds = new DataSet();
        try
        {
            DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);

            for (int i = 1; i < 13; i++)
            {
                DD_PBB.Items.Add(new ListItem(info.GetMonthName(i), i.ToString("00")));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    //void Type_klm()
    //{
    //    DataSet Ds = new DataSet();
    //    try
    //    {
    //        string com = "select typeklm_cd,UPPER(typeklm_desc) as typeklm_desc from Ref_hr_type_klm where Status = 'A' order by typeklm_cd";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
    //        DataTable dt = new DataTable();
    //        adpt.Fill(dt);
    //        DropDownList1.DataSource = dt;
    //        DropDownList1.DataTextField = "typeklm_desc";
    //        DropDownList1.DataValueField = "typeklm_cd";
    //        DropDownList1.DataBind();
    //        DropDownList1.Items.Insert(0, new ListItem("--- PILIH ---", ""));

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //void tunt()
    //{
    //    DataSet Ds = new DataSet();
    //    try
    //    {
    //        string com = "select UPPER(hr_tun_desc) as hr_tun_desc,hr_tun_code from Ref_hr_tuntutan where status='A'";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
    //        DataTable dt = new DataTable();
    //        adpt.Fill(dt);
    //        DD_TUNT.DataSource = dt;
    //        DD_TUNT.DataTextField = "hr_tun_desc";
    //        DD_TUNT.DataValueField = "hr_tun_Code";
    //        DD_TUNT.DataBind();
    //        DD_TUNT.Items.Insert(0, new ListItem("--- PILIH ---", ""));

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //void poton()
    //{
    //    DataSet Ds = new DataSet();
    //    try
    //    {
    //        string com = "select hr_poto_Code,hr_poto_desc from Ref_hr_potongan where Status='A'";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
    //        DataTable dt = new DataTable();
    //        adpt.Fill(dt);
    //        DD_POTON.DataSource = dt;
    //        DD_POTON.DataTextField = "hr_poto_desc";
    //        DD_POTON.DataValueField = "hr_poto_Code";
    //        DD_POTON.DataBind();
    //        DD_POTON.Items.Insert(0, new ListItem("--- PILIH ---", ""));

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}


   

    protected void bt_TextChanged(object sender, EventArgs e)
    {
        call_grd();
        tt();
    }

    protected void bk_TextChanged(object sender, EventArgs e)
    {
        call_grd();
        tt();
    }

    void tt()
    {
        //pp_amt = (double.Parse(TextBox11.Text) + double.Parse(TextBox9.Text) + double.Parse(TextBox13.Text) + double.Parse(TextBox18.Text) + double.Parse(TextBox23.Text) + double.Parse(TextBox24.Text) + double.Parse(TextBox2.Text)).ToString(); 
        pp_amt = (double.Parse(TextBox11.Text) + double.Parse(TextBox9.Text) +  double.Parse(TextBox19.Text) + /* +  double.Parse(TextBox13.Text) + double.Parse(TextBox18.Text) + */ double.Parse(TextBox2.Text) + double.Parse(Label4.Text) + double.Parse(TextBox18.Text)).ToString();
        pk_amt = (double.Parse(txt_pokok.Text) + double.Parse(et_amt.Text) + double.Parse(ll_amt.Text) + double.Parse(TextBox16.Text) + double.Parse(TextBox5.Text) + double.Parse(TextBox7.Text) + double.Parse(TextBox6.Text)).ToString("0.00");
        string tt_amt = (double.Parse(pk_amt) - double.Parse(pp_amt)).ToString("C").Replace("$", "").Replace("RM", "");
        tt_amt1 = (double.Parse(pk_amt) - double.Parse(pp_amt)).ToString("").Replace("$", "");
        TextBox8.Text = double.Parse(pk_amt).ToString("C").Replace("$", "").Replace("RM", "");
        TextBox3.Text = double.Parse(pp_amt).ToString("C").Replace("$", "").Replace("RM", "");
        TextBox20.Text = tt_amt;

        TextBox1.Text = pp_amt;
    }


    void PERKESO()
    {
        DataSet Ds = new DataSet();
        try
        {
            decimal a, b;
            //string com = "select top 1 stf_socso_no,org_socso_no,stf_age,slr_salary_amt,stf_epf_no,stf_tax_no,tax_pcb_amt,inc_ctg_amt,tax_cp38_amt1,tax_cp38_amt2,inc_bonus_amt,inc_kpi_bonus_amt from hr_staff_profile hsp left join hr_fixed_allowance hfa on hsp.stf_staff_no=hfa.fxa_staff_no left join hr_extra_allowance hea on hea.xta_staff_no=hsp.stf_staff_no left join hr_salary hs on hs.slr_staff_no=hsp.stf_staff_no  left join  hr_organization ho on ho.org_id=hsp.str_curr_org_cd left join hr_income_tax as ht on ht.tax_staff_no=hsp.stf_staff_no left join hr_income as hi on hi.inc_staff_no=hsp.stf_staff_no where hsp.stf_staff_no='" + Applcn_no1 + "' and hi.inc_month='" + DD_PBB.SelectedValue + "' and hi.inc_year='" + txt_tahu.Text +"'  order by slr_crt_dt desc";
            string com = "select top 1 stf_socso_no,org_socso_no,stf_age,slr_salary_amt,stf_epf_no,stf_tax_no,tax_pcb_amt,tax_cp38_amt1,tax_cp38_amt2,org_income_tax_no,org_epf_no from hr_staff_profile hsp left join hr_fixed_allowance hfa on hsp.stf_staff_no=hfa.fxa_staff_no left join hr_extra_allowance hea on hea.xta_staff_no=hsp.stf_staff_no left join hr_salary hs on hs.slr_staff_no=hsp.stf_staff_no  left join  hr_organization ho on ho.org_gen_id=hsp.str_curr_org_cd left join hr_income_tax as ht on ht.tax_staff_no=hsp.stf_staff_no where hsp.stf_staff_no='" + Applcn_no1.Text + "' order by slr_crt_dt desc";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                string v1 = string.Empty, v2 = string.Empty; string v3 = string.Empty, v4 = string.Empty;
                string act_dt = txt_tahu.Text + "-" + DD_PBB.SelectedValue;
                DataTable get_ca = new DataTable();
                get_ca = DBCon.Ora_Execute_table("select top(1) format(epf_amt,'#,##.00') epf_amt from hr_kwsp hk where '" + act_dt.ToString() + "' between FORMAT(hk.epf_eff_dt,'yyyy-MM') And FORMAT(hk.epf_end_dt,'yyyy-MM') and hk.epf_staff_no='" + Applcn_no1.Text + "' ORDER BY epf_end_dt DESC");

                DataTable get_cinc = new DataTable();
                get_cinc = DBCon.Ora_Execute_table("select * from hr_income where inc_staff_no='" + Applcn_no1.Text + "' and inc_month ='" + DD_PBB.SelectedValue + "' and inc_year='" + txt_tahu.Text + "'");

                //TextBox10.Text = dt.Rows[0][0].ToString();

                if (dt.Rows[0]["stf_socso_no"].ToString().Trim() != "")
                {
                    TextBox10.Text = dt.Rows[0]["stf_socso_no"].ToString();
                    DataTable dd_stf = new DataTable();
                    dd_stf = DBCon.Ora_Execute_table("select stf_staff_no,stf_icno from hr_staff_profile where stf_staff_no='" + Applcn_no1.Text + "'");
                    TextBox17.Text = dd_stf.Rows[0]["stf_icno"].ToString();
                }
                else
                {
                    DataTable dd_stf = new DataTable();
                    dd_stf = DBCon.Ora_Execute_table("select stf_staff_no,stf_icno from hr_staff_profile where stf_staff_no='" + Applcn_no1.Text + "'");
                    TextBox10.Text = dd_stf.Rows[0]["stf_icno"].ToString();
                    TextBox17.Text = dd_stf.Rows[0]["stf_icno"].ToString();
                }

                if (dt.Rows[0]["stf_epf_no"].ToString().Trim() != "")
                {
                    TextBox12.Text = dt.Rows[0]["stf_epf_no"].ToString();
                }
                else
                {
                    DataTable dd_stf = new DataTable();
                    dd_stf = DBCon.Ora_Execute_table("select stf_staff_no,stf_icno from hr_staff_profile where stf_staff_no='" + Applcn_no1.Text + "'");
                    TextBox12.Text = dd_stf.Rows[0]["stf_icno"].ToString();                    
                }

                //TextBox12.Text = dt.Rows[0]["stf_epf_no"].ToString();
                Label8.Text = dt.Rows[0]["org_socso_no"].ToString();
                Label10.Text = dt.Rows[0]["org_income_tax_no"].ToString();
                Label6.Text = dt.Rows[0]["org_epf_no"].ToString();
                if (get_ca.Rows.Count != 0)
                {
                    TextBox11.Text = get_ca.Rows[0]["epf_amt"].ToString();
                }
                else
                {
                    TextBox11.Text = "0.00";
                }
                //TextBox17.Text = dt.Rows[0]["stf_tax_no"].ToString();
             

                if (get_cinc.Rows.Count != 0)
                {
                    if (get_cinc.Rows[0]["inc_bonus_amt"].ToString() != "0.0000")
                    {
                        double amm1 = double.Parse(get_cinc.Rows[0]["inc_bonus_amt"].ToString());
                        TextBox5.Text = amm1.ToString("C").Replace("$", "").Replace("RM", "");
                        //CheckBox1.Checked = true;
                    }
                    else
                    {
                        TextBox5.Text = "0.00";
                        //CheckBox1.Checked = false;
                    }
                    if (get_cinc.Rows[0]["inc_kpi_bonus_amt"].ToString() != "0.0000")
                    {
                        double amm2 = double.Parse(get_cinc.Rows[0]["inc_kpi_bonus_amt"].ToString());
                        TextBox7.Text = amm2.ToString("C").Replace("$", "").Replace("RM", "");
                        //CheckBox2.Checked = true;
                    }
                    else
                    {
                        TextBox7.Text = "0.00";
                        //CheckBox2.Checked = false;

                    }


                }
                else
                {
                    TextBox5.Text = "0.00";
                    TextBox7.Text = "0.00";
                }
                int act_age = 60;
                int age = Convert.ToInt32(dt.Rows[0][2].ToString());
                if (age < act_age)
                {
                    v1 = "per_employee_amt";
                    v2 = "per_employer_amt1";
                    v3 = "SIP_employee_amt";
                    v4 = "SIP_employer_amt1";
                }
                else
                {
                    v1 = "per_employer_amt2";
                    v2 = "per_employer_amt2";
                    v3 = "SIP_employee_amt";
                    v4 = "SIP_employer_amt2";
                }
                DataTable dd_hrsal = new DataTable();
                dd_hrsal = DBCon.Ora_Execute_table("select * from hr_salary where slr_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt + "') between FORMAT(slr_eff_dt,'yyyy-MM') And FORMAT(slr_end_dt,'yyyy-MM')");
                if (dd_hrsal.Rows.Count != 0)
                {
                    if (dd_hrsal.Rows.Count == 2)
                    {
                        DataTable dd_hrsal_tot = new DataTable();
                        dd_hrsal_tot = DBCon.Ora_Execute_table("select *,day(slr_eff_dt) d1,day(slr_end_dt) d2,datediff(day, slr_eff_dt, dateadd(month, 1, slr_eff_dt)) as tdays,datediff(day, DATEADD(mm, 1, slr_eff_dt), dateadd(month, 1, DATEADD(mm, 1, slr_eff_dt))) as tdays1,((day(slr_end_dt)) * slr_salary_amt) / datediff(day, DATEADD(mm, 1, slr_eff_dt), dateadd(month, 1, DATEADD(mm, 1, slr_eff_dt))) as a,(((datediff(day, slr_eff_dt, dateadd(month, 1, slr_eff_dt)) - day(slr_eff_dt)) + 1) * slr_salary_amt) / datediff(day, slr_eff_dt, dateadd(month, 1, slr_eff_dt)) as b,((datediff(day, slr_eff_dt, dateadd(month, 1, slr_eff_dt)) - day(slr_eff_dt)) * slr_salary_amt) / datediff(day, slr_eff_dt, dateadd(month, 1, slr_eff_dt)) as bsal from hr_salary where slr_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt + "') between FORMAT(slr_eff_dt,'yyyy-MM') And FORMAT(slr_end_dt,'yyyy-MM')");
                        string aa = (double.Parse(dd_hrsal_tot.Rows[0]["a"].ToString()) + double.Parse(dd_hrsal_tot.Rows[1]["b"].ToString())).ToString();
                        TextBox2.Text = aa;
                    }
                    else
                    {
                        DataTable dd_hrsal_1 = new DataTable();
                        dd_hrsal_1 = DBCon.Ora_Execute_table("select slr_salary_amt from hr_salary where slr_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt + "') between FORMAT(slr_eff_dt,'yyyy-MM') And FORMAT(slr_end_dt,'yyyy-MM')");
                        TextBox2.Text = double.Parse(dd_hrsal_1.Rows[0]["slr_salary_amt"].ToString()).ToString();
                    }
                }
                else
                {
                    TextBox2.Text = "0.00";
                }

                
                DataTable Get_tot = new DataTable();
                
                //perkeso

                Get_tot = DBCon.Ora_Execute_table("select b.a1,ISNULL(c.b1,'0.00') b1,ISNULL((ISNULL(b.a1,'0.00') + ISNULL(c.b1,'0.00')),'') as tot from (select sum(fxa_allowance_amt) as a1,fxa_staff_no from hr_fixed_allowance Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=fxa_allowance_type_cd and ss1.elau_perkeso='S' where fxa_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt + "' between FORMAT(fxa_eff_dt,'yyyy-MM') And FORMAT(fxa_end_dt,'yyyy-MM')) group by fxa_staff_no) as b full outer join (select sum(xta_allowance_amt) as b1,xta_staff_no from hr_extra_allowance Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=xta_allowance_type_cd and ss1.elau_perkeso='S' where xta_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt + "' between FORMAT(xta_eff_dt,'yyyy-MM') And FORMAT(xta_end_dt,'yyyy-MM')) group by xta_staff_no) as c on c.xta_staff_no=b.fxa_staff_no order by b.fxa_staff_no Asc");
                string tt_amt = string.Empty, tt_amt1 = string.Empty;

                string allowence_amt = string.Empty, sip_amt = string.Empty, tung_perk_amt = string.Empty, ot_amt_maj = string.Empty, ot_amt_emp = string.Empty, ded_amt = string.Empty, ded_emp_amt = string.Empty;

                if (Get_tot.Rows.Count != 0)
                {
                    allowence_amt = Get_tot.Rows[0]["tot"].ToString();
                }
                else
                {
                    allowence_amt = "0.00";
                }


                DataTable dd_hrsal_tun1 = new DataTable();
                dd_hrsal_tun1 = DBCon.Ora_Execute_table("select ISNULL(sum(ISNULL(tun_amt,'0.00')),'0.00') as amt from hr_tunggakan left join Ref_hr_tunggakan s1 on s1.hr_tung_Code=tun_type_cd  where s1.elau_perkeso ='S' and tun_staff_no='" + Applcn_no1.Text + "' and  tun_year='" + txt_tahu.Text + "' and tun_month='" + DD_PBB.SelectedValue + "'");
                if (dd_hrsal_tun1.Rows.Count != 0)
                {
                    tung_perk_amt = double.Parse(dd_hrsal_tun1.Rows[0]["amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                }
                else
                {
                    tung_perk_amt = "0.00";
                }


                DataTable dd_hrsal_ot1 = new DataTable();
                dd_hrsal_ot1 = DBCon.Ora_Execute_table("select ISNULL(sum(ISNULL(otl_ot_amt,'0.00')),'0.00') as amt from hr_ot left join Ref_hr_type_klm s1 on s1.typeklm_cd=otl_ot_type_cd  where s1.elau_perkeso ='S' and otl_staff_no='" + Applcn_no1.Text + "' and FORMAT(otl_work_dt,'yyyy-MM') ='" + act_dt + "'");
                if (dd_hrsal_ot1.Rows.Count != 0)
                {
                    ot_amt_emp = double.Parse(dd_hrsal_ot1.Rows[0]["amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                }
                else
                {
                    ot_amt_emp = "0.00";
                }

                //deduction
                DataTable tot_upl = new DataTable();
                tot_upl = DBCon.Ora_Execute_table("select ISNULL(sum(ded_deduct_amt),'') as s1 from hr_deduction left join Ref_hr_potongan s1 on s1.hr_poto_Code=ded_deduct_type_cd and Status='A' where s1.elau_perkeso='S' and ded_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt + "' between FORMAT(ded_start_dt,'yyyy-MM') And FORMAT(ded_end_dt,'yyyy-MM'))");
                //Math.Round(b, 2)
                if (tot_upl.Rows.Count != 0)
                {
                    ded_amt = double.Parse(tot_upl.Rows[0]["s1"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                }
                else
                {
                    ded_amt = "0.00";
                }

                //SIP

                DataTable Get_tot_sip = new DataTable();
                Get_tot_sip = DBCon.Ora_Execute_table("select b.a1,ISNULL(c.b1,'0.00') b1,ISNULL((ISNULL(b.a1,'0.00') + ISNULL(c.b1,'0.00')),'') as tot from (select sum(fxa_allowance_amt) as a1,fxa_staff_no from hr_fixed_allowance Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=fxa_allowance_type_cd and ss1.elau_sip='S' where fxa_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt + "' between FORMAT(fxa_eff_dt,'yyyy-MM') And FORMAT(fxa_end_dt,'yyyy-MM')) group by fxa_staff_no) as b full outer join (select sum(xta_allowance_amt) as b1,xta_staff_no from hr_extra_allowance Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=xta_allowance_type_cd and ss1.elau_sip='S' where xta_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt + "' between FORMAT(xta_eff_dt,'yyyy-MM') And FORMAT(xta_end_dt,'yyyy-MM')) group by xta_staff_no) as c on c.xta_staff_no=b.fxa_staff_no order by b.fxa_staff_no Asc");

                if (Get_tot_sip.Rows.Count != 0)
                {
                    sip_amt = Get_tot_sip.Rows[0]["tot"].ToString();
                }
                else
                {
                    sip_amt = "0.00";
                }

                DataTable dd_hrsal_tun = new DataTable();
                dd_hrsal_tun = DBCon.Ora_Execute_table("select ISNULL(sum(ISNULL(tun_amt,'0.00')),'0.00') as amt from hr_tunggakan left join Ref_hr_tunggakan s1 on s1.hr_tung_Code=tun_type_cd  where s1.elau_sip ='S' and tun_staff_no='" + Applcn_no1.Text + "' and  tun_year='" + txt_tahu.Text + "' and tun_month='" + DD_PBB.SelectedValue + "'");
                if (dd_hrsal_tun.Rows.Count != 0)
                {
                    cc5 = double.Parse(dd_hrsal_tun.Rows[0]["amt"].ToString()).ToString();
                }
                else
                {
                    cc5 = "0.00";
                }

                DataTable dd_hrsal_ot = new DataTable();
                dd_hrsal_ot = DBCon.Ora_Execute_table("select ISNULL(sum(ISNULL(otl_ot_amt,'0.00')),'0.00') as amt from hr_ot left join Ref_hr_type_klm s1 on s1.typeklm_cd=otl_ot_type_cd  where s1.elau_sip ='S' and otl_staff_no='" + Applcn_no1.Text + "' and FORMAT(otl_work_dt,'yyyy-MM') ='" + act_dt + "'");
                if (dd_hrsal_ot.Rows.Count != 0)
                {
                    ot_amt_maj = double.Parse(dd_hrsal_ot.Rows[0]["amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                }
                else
                {
                    ot_amt_maj = "0.00";
                }

                //deduction
                DataTable tot_upl1 = new DataTable();
                tot_upl1 = DBCon.Ora_Execute_table("select ISNULL(sum(ded_deduct_amt),'') as s1 from hr_deduction left join Ref_hr_potongan s1 on s1.hr_poto_Code=ded_deduct_type_cd and Status='A' where s1.elau_sip='S' and ded_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt + "' between FORMAT(ded_start_dt,'yyyy-MM') And FORMAT(ded_end_dt,'yyyy-MM'))");
                //Math.Round(b, 2)
                if (tot_upl1.Rows.Count != 0)
                {
                    ded_emp_amt = double.Parse(tot_upl1.Rows[0]["s1"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                }
                else
                {
                    ded_emp_amt = "0.00";
                }

                //    if (Get_tot.Rows.Count != 0)
                //{
                //tt_amt = (double.Parse(cc1) +  double.Parse(cc2) + double.Parse(cc5) + double.Parse(TextBox2.Text)).ToString();
                tt_amt = ((double.Parse(allowence_amt) + double.Parse(tung_perk_amt) + double.Parse(ot_amt_emp) + double.Parse(TextBox2.Text)) - double.Parse(ded_amt)).ToString();
                //TextBox17.Text = tt_amt;
                tt_amt1 = ((double.Parse(sip_amt) + double.Parse(cc5) + double.Parse(ot_amt_maj) + double.Parse(TextBox2.Text)) - double.Parse(ded_emp_amt)).ToString();
                //TextBox25.Text = tt_amt1;
                DataTable Get_perk = new DataTable();
                Get_perk = DBCon.Ora_Execute_table("select top(1) " + v1 + " from hr_comm_perkeso where '" + tt_amt + "' between per_min_income_amt and per_max_income_amt");

                DataTable Get_perkmaj = new DataTable();
                Get_perkmaj = DBCon.Ora_Execute_table("select top(1) " + v2 + " from hr_comm_perkeso where '" + tt_amt + "' between per_min_income_amt and per_max_income_amt");

                DataTable Get_perk1 = new DataTable();
                Get_perk1 = DBCon.Ora_Execute_table("select top(1) " + v3 + " from hr_comm_SIP where '" + tt_amt1 + "' between SIP_min_income_amt and SIP_max_income_amt");

                DataTable Get_perkmaj1 = new DataTable();
                Get_perkmaj1 = DBCon.Ora_Execute_table("select top(1) " + v4 + " from hr_comm_SIP where '" + tt_amt1 + "' between SIP_min_income_amt and SIP_max_income_amt");

                double amnt1 = double.Parse(Get_perk.Rows[0]["" + v1 + ""].ToString());
                TextBox9.Text = amnt1.ToString("C").Replace("$", "").Replace("RM", "");


                double amnt2 = double.Parse(Get_perkmaj.Rows[0]["" + v2 + ""].ToString());
                TextBox24.Text = amnt2.ToString("C").Replace("$", "").Replace("RM", "");

                double amnt3 = double.Parse(Get_perk1.Rows[0]["" + v3 + ""].ToString());
                TextBox19.Text = amnt3.ToString("C").Replace("$", "").Replace("RM", "");

                double amnt4 = double.Parse(Get_perkmaj1.Rows[0]["" + v4 + ""].ToString());
                TextBox27.Text = amnt4.ToString("C").Replace("$", "").Replace("RM", "");

                DataTable Get_inc_tax_PCB = new DataTable();
                Get_inc_tax_PCB = DBCon.Ora_Execute_table("SELECT tax_pcb_amt FROM hr_income_tax WHERE tax_staff_no= '" + Applcn_no1.Text + "' and (('" + act_dt.ToString() + "') between FORMAT(tax_pcb_start_dt,'yyyy-MM') And FORMAT(tax_pcb_end_dt,'yyyy-MM'))");

                if (Get_inc_tax_PCB.Rows.Count == 0)
                {
                    Label4.Text = "0.00";
                }
                else
                {
                        //amnt3 = double.Parse(Get_inc_tax_PCB.Rows[0]["tax_pcb_amt"].ToString());
                        Label4.Text = double.Parse(Get_inc_tax_PCB.Rows[0]["tax_pcb_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                }

                DataTable Get_inc_tax_cp38 = new DataTable();
                Get_inc_tax_cp38 = DBCon.Ora_Execute_table("SELECT tax_cp38_amt1 FROM hr_income_tax WHERE tax_staff_no= '" + Applcn_no1.Text + "' and (('" + act_dt.ToString() + "') between FORMAT(tax_cp38_start_dt1,'yyyy-MM') And FORMAT(tax_cp38_end_dt1,'yyyy-MM'))");

                if (Get_inc_tax_cp38.Rows.Count == 0)
                {
                    TextBox18.Text = "0.00";
                }
                else
                {                    
                    TextBox18.Text = double.Parse(Get_inc_tax_cp38.Rows[0]["tax_cp38_amt1"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                }


                DataTable Get_kwsp = new DataTable();
                //Get_kwsp = DBCon.Ora_Execute_table("select epf_emp_kwsp_perc,epf_emp_kwsp_amt from hr_kwsp where epf_staff_no='" + Applcn_no1.Text + "' and epf_end_dt='9999-12-31' and ('" + act_dt.ToString() + "' between FORMAT(epf_eff_dt,'yyyy-MM') And FORMAT(epf_end_dt,'yyyy-MM'))");
                Get_kwsp = DBCon.Ora_Execute_table("select epf_emp_kwsp_perc,epf_emp_kwsp_amt from hr_kwsp where epf_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt.ToString() + "' between FORMAT(epf_eff_dt,'yyyy-MM') And FORMAT(epf_end_dt,'yyyy-MM'))");
                //TextBox23.Text = Get_kwsp.Rows[0]["epf_percentage"].ToString();
                if (Get_kwsp.Rows.Count != 0 && Get_kwsp.Rows[0]["epf_emp_kwsp_amt"].ToString() != "")
                {
                    double amt_kwsp = double.Parse(Get_kwsp.Rows[0]["epf_emp_kwsp_amt"].ToString());
                    TextBox23.Text = amt_kwsp.ToString("C").Replace("$", "").Replace("RM", "");
                    TextBox25.Text = Get_kwsp.Rows[0]["epf_emp_kwsp_perc"].ToString();
                }
                else
                {
                    TextBox23.Text = "0.00";
                    TextBox25.Text = "";
                }

                DataTable dd_hrsal4 = new DataTable();
                dd_hrsal4 = DBCon.Ora_Execute_table("select fx.ded_staff_no,sum(fx.ded_deduct_amt) as samt from hr_deduction as fx inner join Ref_hr_potongan as PO on PO.hr_poto_Code=fx.ded_deduct_type_cd where ('" + act_dt.ToString() + "' between FORMAT(fx.ded_start_dt,'yyyy-MM') And FORMAT(fx.ded_end_dt,'yyyy-MM')) and fx.ded_staff_no='" + Applcn_no1.Text + "' group by fx.ded_staff_no");
                if (dd_hrsal4.Rows.Count != 0)
                {
                    TextBox2.Text = double.Parse(dd_hrsal4.Rows[0]["samt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                }
                else
                {
                    TextBox2.Text = "0.00";
                }
                DataTable dd_hrsal5 = new DataTable();
                dd_hrsal5 = DBCon.Ora_Execute_table("select fx.ded_staff_no,sum(fx.ded_deduct_amt) as samt from hr_deduction as fx where ded_deduct_type_cd='09' and ('" + act_dt.ToString() + "' between FORMAT(fx.ded_start_dt,'yyyy-MM') And FORMAT(fx.ded_end_dt,'yyyy-MM')) and fx.ded_staff_no='" + Applcn_no1.Text + "' group by fx.ded_staff_no");
                if (dd_hrsal5.Rows.Count != 0)
                {
                    TextBox4.Text = double.Parse(dd_hrsal5.Rows[0]["samt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                }
                else
                {
                    TextBox4.Text = "0.00";
                }

            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

  

    void call_grd()
    {
        act_dt = txt_tahu.Text + "-" + DD_PBB.SelectedValue;
        grid1();
        grid2();
        //grid3();
        //grid4();
        grid5();
        grid6();
        grid7();
        if (txt_stffno.Text != "")
        {
            tt();
        }
    }

    protected void TextBox19_TextChanged(object sender, EventArgs e)
    {
        if (txt_stffno.Text != "")
        {

            call_grd();
            tt();
        }
    }

    //protected void TextBox13_TextChanged(object sender, EventArgs e)
    //{
    //    if (TextBox13.Text == "")
    //    {
    //        TextBox13.Text = "Enter Some values here";
    //    }
    //    else
    //    {
    //        double h, i, j;
    //        h = Convert.ToDouble(TextBox20.Text.Trim().ToString());
    //        i = Convert.ToDouble(TextBox13.Text.Trim().ToString());
    //        //f = Convert.ToDouble(TextBox11.Text.Trim().ToString());
    //        //g = Convert.ToDouble(TextBox9.Text.Trim().ToString());
    //        j = h - i;
    //        TextBox20.Text = Math.Round(decimal.Parse(j.ToString()), 2).ToString("0.00");
    //    }
    //}




    //void grid4()
    //{
    //    con.Open();
    //    SqlCommand cmd = new SqlCommand("select hd.ded_staff_no,po.hr_poto_desc,ded_ref_no,ded_deduct_amt,hd.ded_deduct_type_cd from hr_deduction as hd left join Ref_hr_potongan as PO on PO.hr_poto_Code=hd.ded_deduct_type_cd where hd.ded_staff_no='" + Applcn_no1.Text + "' and (('" + act_dt.ToString() + "') between FORMAT(hd.ded_start_dt,'yyyy-MM') And FORMAT(hd.ded_end_dt,'yyyy-MM'))", con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count == 0)
    //    {
    //        ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
    //        GridView4.DataSource = ds;
    //        GridView4.DataBind();
    //        int columncount = GridView4.Rows[0].Cells.Count;
    //        GridView4.Rows[0].Cells.Clear();
    //        GridView4.Rows[0].Cells.Add(new TableCell());
    //        GridView4.Rows[0].Cells[0].ColumnSpan = columncount;
    //        GridView4.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
    //        TextBox2.Text = "0.00";
    //    }
    //    else
    //    {
    //        GridView4.DataSource = ds;
    //        GridView4.DataBind();

    //    }
    //    con.Close();
    //    //cumulative();
    //    //TextBox20.Text = tot_jenis.ToString();
    //}

    void grid5()
    {
        TextBox16.Text = "0.00";
        string sq1 = string.Empty;
        if (txt_stffno.Text != "")
        {
            sq1 = "select ot.otl_staff_no,FORMAT(ot.otl_work_dt,'dd/MM/yyyy', 'en-us') as otl_work_dt,tk.typeklm_desc,ot.otl_work_hour,ot.otl_ot_amt, case when ot.otl_ot_sts_cd = '01' then 'BARU' else 'TELAH DIPROSES' end as stsname,FORMAT(ot.otl_crt_dt,'dd/MM/yyyy HH:mm:ss', 'en-us') as otl_crt_dt from hr_ot as ot left join Ref_hr_type_klm as tk on tk.typeklm_cd=ot.otl_ot_type_cd where otl_staff_no='" + Applcn_no1.Text + "' and FORMAT(otl_work_dt,'yyyy-MM') ='" + act_dt + "'";
        }
        else
        {
            sq1 = "select ot.otl_staff_no,FORMAT(ot.otl_work_dt,'dd/MM/yyyy', 'en-us') as otl_work_dt,tk.typeklm_desc,ot.otl_work_hour,ot.otl_ot_amt,case when ot.otl_ot_sts_cd = '01' then 'BARU' else 'TELAH DIPROSES' end as stsname,FORMAT(ot.otl_crt_dt,'dd/MM/yyyy  HH:mm:ss', 'en-us') as otl_crt_dt from hr_ot as ot left join Ref_hr_type_klm as tk on tk.typeklm_cd=ot.otl_ot_type_cd where otl_staff_no='" + Applcn_no1.Text + "'";
        }

        con.Open();
        SqlCommand cmd = new SqlCommand("" + sq1 + "", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView5.DataSource = ds;
            GridView5.DataBind();
            int columncount = GridView5.Rows[0].Cells.Count;
            GridView5.Rows[0].Cells.Clear();
            GridView5.Rows[0].Cells.Add(new TableCell());
            GridView5.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView5.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
            TextBox16.Text = "0.00";
        }
        else
        {
            GridView5.DataSource = ds;
            GridView5.DataBind();

            DataTable dd_hrsal5 = new DataTable();
            dd_hrsal5 = DBCon.Ora_Execute_table("select  sum(otl_ot_amt) as amt1 from hr_ot as ot  where otl_staff_no='" + Applcn_no1.Text + "' and FORMAT(otl_work_dt,'yyyy-MM') ='" + act_dt + "'");
            if (dd_hrsal5.Rows.Count != 0)
            {
                decimal vv4 = decimal.Parse(dd_hrsal5.Rows[0]["amt1"].ToString());
                TextBox16.Text = vv4.ToString();
            }
            else
            {
                TextBox16.Text = "0.00";
            }
        }
        con.Close();
        //cumulative();
        TextBox20.Text = tot_jenis.ToString();
    }


    void grid6()
    {
        string sq6 = string.Empty;
        if (txt_stffno.Text != "")
        {
            sq6 = "select Id,bns_staff_no,FORMAT(bns_eff_dt,'dd/MM/yyyy', 'en-us') as bns_eff_dt,FORMAT(bns_end_dt,'dd/MM/yyyy', 'en-us') as bns_end_dt,bns_amt,bns_kpi_amt,FORMAT(bns_crt_dt,'yyyy-MM-dd HH:mm:ss', 'en-us') as bns_crt_dt from hr_Bonus where bns_staff_no='" + Applcn_no1.Text + "' and (('" + act_dt.ToString() + "') between FORMAT(bns_eff_dt,'yyyy-MM') And FORMAT(bns_end_dt,'yyyy-MM'))";
        }
        else
        {
            sq6 = "select Id,bns_staff_no,FORMAT(bns_eff_dt,'dd/MM/yyyy', 'en-us') as bns_eff_dt,FORMAT(bns_end_dt,'dd/MM/yyyy', 'en-us') as bns_end_dt,bns_amt,bns_kpi_amt,FORMAT(bns_crt_dt,'yyyy-MM-dd HH:mm:ss', 'en-us') as bns_crt_dt from hr_Bonus where bns_staff_no='' ";
        }

        con.Open();
        SqlCommand cmd = new SqlCommand("" + sq6 + "", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView3.DataSource = ds;
            GridView3.DataBind();
            int columncount = GridView3.Rows[0].Cells.Count;
            GridView3.Rows[0].Cells.Clear();
            GridView3.Rows[0].Cells.Add(new TableCell());
            GridView3.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView3.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
            TextBox5.Text = "0.00";
            TextBox7.Text = "0.00";
        }
        else
        {
            GridView3.DataSource = ds;
            GridView3.DataBind();

            DataTable dd_hrsal_bns = new DataTable();
            dd_hrsal_bns = DBCon.Ora_Execute_table("select sum(bns_amt) as a1,sum(bns_kpi_amt) as a2 from hr_Bonus where bns_staff_no='" + Applcn_no1.Text + "' and (('" + act_dt.ToString() + "') between FORMAT(bns_eff_dt,'yyyy-MM') And FORMAT(bns_end_dt,'yyyy-MM'))");
            if (dd_hrsal_bns.Rows.Count != 0)
            {
                decimal vv4 = decimal.Parse(dd_hrsal_bns.Rows[0]["a1"].ToString());
                TextBox5.Text = vv4.ToString();
                decimal vv5 = decimal.Parse(dd_hrsal_bns.Rows[0]["a2"].ToString());
                TextBox7.Text = vv5.ToString();
            }
            else
            {
                TextBox5.Text = "0.00";
                TextBox7.Text = "0.00";
            }
        }
        con.Close();
    }

    void grid7()
    {
        string sq7 = string.Empty;
        if (txt_stffno.Text != "")
        {
            sq7 = "select *,hr_tung_desc from hr_tunggakan left join Ref_hr_tunggakan on hr_tung_Code=tun_type_cd where tun_staff_no='" + Applcn_no1.Text + "' and  tun_year='"+ txt_tahu.Text +"' and tun_month='"+ DD_PBB.SelectedValue + "'";
        }
        else
        {
            sq7 = "select *,hr_tung_desc from hr_tunggakan left join Ref_hr_tunggakan on hr_tung_Code=tun_type_cd where tun_staff_no='' ";
        }

        con.Open();
        SqlCommand cmd = new SqlCommand("" + sq7 + "", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView4.DataSource = ds;
            GridView4.DataBind();
            int columncount = GridView4.Rows[0].Cells.Count;
            GridView4.Rows[0].Cells.Clear();
            GridView4.Rows[0].Cells.Add(new TableCell());
            GridView4.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView4.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
            TextBox6.Text = "0.00";
        }
        else
        {
            GridView4.DataSource = ds;
            GridView4.DataBind();

            DataTable dd_hrsal_tun = new DataTable();
            dd_hrsal_tun = DBCon.Ora_Execute_table("select sum(ISNULL(tun_amt,'0.00')) as amt from hr_tunggakan where tun_staff_no='" + Applcn_no1.Text + "' and  tun_year='" + txt_tahu.Text + "' and tun_month='" + DD_PBB.SelectedValue + "'");
            if (dd_hrsal_tun.Rows.Count != 0)
            {
                TextBox6.Text = double.Parse(dd_hrsal_tun.Rows[0]["amt"].ToString()).ToString("C").Replace("RM","").Replace("$", "");
            }
            else
            {
                TextBox6.Text = "0.00";                
            }
        }
        con.Close();
    }
    void grid1()
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("select fxa_staff_no,hr_elau_desc as hr_elaun_desc,(sum(falw_amt) - sum(falw_amt1)) fxa_allowance_amt from (select fxa_staff_no,hr_elau_desc,ISNULL(sum(s1.fxa_allowance_amt),'') as falw_amt,'' falw_amt1 from hr_fixed_allowance s1 Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=s1.fxa_allowance_type_cd where ('" + act_dt.ToString() + "' between FORMAT(fxa_eff_dt,'yyyy-MM') And FORMAT(fxa_end_dt,'yyyy-MM')) and fxa_staff_no='" + Applcn_no1.Text + "' group by fxa_staff_no,hr_elau_desc union all select fxa_staff_no,hr_elau_desc,ISNULL(sum(s1.fxa_promo_amt),'') as falw_amt,ISNULL(sum(s1.fxa_allowance_amt),'') as falw_amt1 from hr_fixed_allowance s1 Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=s1.fxa_allowance_type_cd where ('" + act_dt.ToString() + "' between FORMAT(fxa_pst_dt,'yyyy-MM') And FORMAT(fxa_ped_dt,'yyyy-MM')) and fxa_staff_no='" + Applcn_no1.Text +"' group by fxa_staff_no,hr_elau_desc)as a group by fxa_staff_no,hr_elau_desc", con);
        //SqlCommand cmd = new SqlCommand("select a.fxa_staff_no,el.hr_elaun_desc,a.fxa_allowance_amt from (select * from hr_fixed_allowance as fx where fx.fxa_staff_no='" + txt_stffno.Text + "') as a left join Ref_hr_elaun as EL on EL.hr_elaun_Code=a.fxa_allowance_type_cd ", con);
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
            et_amt.Text = "0.00";
        }
        else
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
            DataTable dd_hrsal1 = new DataTable();
            dd_hrsal1 = DBCon.Ora_Execute_table("select (sum(falw_amt) - sum(falw_amt1)) samt from (select ISNULL(sum(s1.fxa_allowance_amt),'') as falw_amt,'' falw_amt1 from hr_fixed_allowance s1 Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=s1.fxa_allowance_type_cd where ('" + act_dt.ToString() + "' between FORMAT(fxa_eff_dt,'yyyy-MM') And FORMAT(fxa_end_dt,'yyyy-MM')) and fxa_staff_no='" + Applcn_no1.Text + "' union all select ISNULL(sum(s1.fxa_promo_amt),'') as falw_amt,ISNULL(sum(s1.fxa_allowance_amt),'') as falw_amt1 from hr_fixed_allowance s1 Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=s1.fxa_allowance_type_cd  where ('" + act_dt.ToString() + "' between FORMAT(fxa_pst_dt,'yyyy-MM') And FORMAT(fxa_ped_dt,'yyyy-MM')) and fxa_staff_no='" + Applcn_no1.Text +"')as a");
            //dd_hrsal1 = DBCon.Ora_Execute_table("select fx.fxa_staff_no,sum(fx.fxa_allowance_amt) as samt from hr_fixed_allowance as fx where fx.fxa_staff_no='" + Applcn_no1 + "' group by fx.fxa_staff_no");
            if (dd_hrsal1.Rows.Count != 0)
            {
                decimal vv1 = decimal.Parse(dd_hrsal1.Rows[0]["samt"].ToString());
                et_amt.Text = vv1.ToString();
            }
            else
            {
                et_amt.Text = "0.00";
            }
        }
        con.Close();
        //cumulative();


    }

    void grid2()
    {
        con.Open();
        //SqlCommand cmd = new SqlCommand("select a.xta_staff_no,EL.hr_elau_desc as hr_elaun_desc,a.xta_allowance_amt from (select * from hr_extra_allowance as ea where ea.xta_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt.ToString() + "' between FORMAT(ea.xta_eff_dt,'yyyy-MM') And FORMAT(ea.xta_end_dt,'yyyy-MM'))) as a left join Ref_hr_jenis_elaun as EL on EL.hr_elau_Code=a.xta_allowance_type_cd ", con);
        SqlCommand cmd = new SqlCommand("select xta_staff_no,hr_elau_desc as hr_elaun_desc,(sum(xalw_amt) - sum(xalw_amt1)) xta_allowance_amt from(select xta_staff_no,hr_elau_desc,ISNULL(sum(s1.xta_allowance_amt),'') as xalw_amt,'0.00' xalw_amt1 from hr_extra_allowance s1 Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=s1.xta_allowance_type_cd where s1.xta_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt.ToString() + "' between FORMAT(xta_eff_dt,'yyyy-MM') And FORMAT(xta_end_dt,'yyyy-MM') ) group by hr_elau_desc,xta_staff_no union all select xta_staff_no,hr_elau_desc,ISNULL(sum(s1.xta_promo_amt),'') as xalw_amt,ISNULL(sum(s1.xta_allowance_amt),'') as xalw_amt1 from hr_extra_allowance s1 Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=s1.xta_allowance_type_cd where s1.xta_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt.ToString() + "' between FORMAT(xta_pst_dt,'yyyy-MM') And FORMAT(xta_ped_dt,'yyyy-MM')) group by xta_staff_no,hr_elau_desc) as a group by xta_staff_no,hr_elau_desc", con);
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
            GridView2.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
            ll_amt.Text = "0.00";
        }
        else
        {
            GridView2.DataSource = ds;
            GridView2.DataBind();
            DataTable dd_hrsal2 = new DataTable();
            //dd_hrsal2 = DBCon.Ora_Execute_table("select fx.xta_staff_no,sum(fx.xta_allowance_amt) as tot_xta from hr_extra_allowance as fx where ('" + act_dt.ToString() + "' between FORMAT(fx.xta_eff_dt,'yyyy-MM') And FORMAT(fx.xta_end_dt,'yyyy-MM')) and fx.xta_staff_no='" + Applcn_no1.Text + "' group by fx.xta_staff_no");
            dd_hrsal2 = DBCon.Ora_Execute_table("select (sum(xalw_amt) - sum(xalw_amt1)) tot_xta from(select ISNULL(sum(s1.xta_allowance_amt),'') as xalw_amt,'0.00' xalw_amt1 from hr_extra_allowance s1 Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=s1.xta_allowance_type_cd where s1.xta_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt.ToString() + "' between FORMAT(xta_eff_dt,'yyyy-MM') And FORMAT(xta_end_dt,'yyyy-MM')) union all select ISNULL(sum(s1.xta_promo_amt),'') as xalw_amt,ISNULL(sum(s1.xta_allowance_amt),'') as xalw_amt1 from hr_extra_allowance s1 Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=s1.xta_allowance_type_cd where s1.xta_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt.ToString() + "' between FORMAT(xta_pst_dt,'yyyy-MM') And FORMAT(xta_ped_dt,'yyyy-MM'))) as a");
            if (dd_hrsal2.Rows.Count != 0)
            {
                decimal vv2 = decimal.Parse(dd_hrsal2.Rows[0]["tot_xta"].ToString());
                ll_amt.Text = vv2.ToString();
            }
            else
            {
                ll_amt.Text = "0.00";
            }
        }
        con.Close();

    }





    protected void btn_simp_Click(object sender, EventArgs e)
    {
        act_dt = txt_tahu.Text + "-" + DD_PBB.SelectedValue;
        string batch_name = string.Empty;
       
        DataTable ddicno = new DataTable();
        ddicno = DBCon.Ora_Execute_table("select * from hr_income where inc_staff_no='" + Applcn_no1.Text + "' and inc_month='" + DD_PBB.SelectedValue + "' and inc_year='" + txt_tahu.Text + "' ");
        DataTable ddicno1 = new DataTable();
        //ddicno1 = DBCon.Ora_Execute_table("select pos_org_id,pos_gen_ID,pos_dept_cd from hr_post_his where pos_staff_no='" + txt_stffno.Text + "' and pos_end_dt='9999-12-31'");
        ddicno1 = DBCon.Ora_Execute_table("select stf_staff_no,stf_curr_dept_cd,str_curr_org_cd,stf_cur_sub_org,ho.org_id from hr_staff_profile left join hr_organization ho on ho.org_gen_id=str_curr_org_cd where stf_staff_no='" + Applcn_no1.Text + "'");
        //string stffno = ddicno.Rows[0]["stf_staff_no"].ToString();
        batch_name = "HR" + DD_PBB.SelectedValue + txt_tahu.Text;
        if (ddicno.Rows.Count == 0)
        {

            if (TextBox11.Text != "" && TextBox9.Text != "" /*&& TextBox13.Text != ""*/)
            {
                //DBCon.Execute_CommamdText("insert into hr_income (inc_staff_no,inc_month,inc_year,inc_grade_cd,inc_org_id,inc_dept_cd,inc_salary_amt,inc_cumm_fix_allwnce_amt,inc_cumm_xtra_allwnce_amt,inc_cumm_deduct_amt,inc_bonus_amt,inc_kpi_bonus_amt,inc_gross_amt,inc_ctg_amt,inc_kwsp_amt,inc_perkeso_amt,inc_pcb_amt,inc_cp38_amt,inc_total_deduct_amt,inc_nett_amt,inc_ot_amt,inc_crt_id,inc_crt_dt,inc_cp38_amt2,inc_emp_perkeso_amt,inc_emp_kwsp_amt,inc_emp_kwsp_perc,inc_gen_id) values('" + txt_stffno.Text + "','" + DD_PBB.SelectedValue + "','" + txt_tahu.Text + "','" + txt_gred.Text + "','" + ddicno1.Rows[0]["pos_org_id"].ToString() + "','" + ddicno1.Rows[0]["pos_dept_cd"].ToString() + "','" + txt_pokok.Text + "','" + et_amt.Text + "','" + ll_amt.Text + "','" + TextBox1.Text + "','" + cb1 + "','" + cb2 + "','" + TextBox8.Text + "','" + TextBox19.Text + "','" + TextBox11.Text + "','" + TextBox9.Text + "','" + TextBox13.Text + "','" + TextBox18.Text + "','" + M + "','" + tt_amt1 + "','" + TextBox16.Text + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + TextBox15.Text + "','" + TextBox24.Text + "','" + TextBox23.Text + "','" + TextBox25.Text + "','" + ddicno1.Rows[0]["pos_gen_ID"].ToString() + "')");
                DBCon.Execute_CommamdText("insert into hr_income (inc_staff_no,inc_month,inc_year,inc_grade_cd,inc_org_id,inc_dept_cd,inc_salary_amt,inc_cumm_fix_allwnce_amt,inc_cumm_xtra_allwnce_amt,inc_cumm_deduct_amt,inc_bonus_amt,inc_kpi_bonus_amt,inc_gross_amt,inc_ctg_amt,inc_kwsp_amt,inc_perkeso_amt,inc_pcb_amt,inc_cp38_amt,inc_total_deduct_amt,inc_nett_amt,inc_ot_amt,inc_crt_id,inc_crt_dt,inc_emp_perkeso_amt,inc_emp_kwsp_amt,inc_emp_kwsp_perc,inc_gen_id,inc_sub_org_id,inc_emp_SIP_amt,inc_SIP_amt,inc_ded_amt,inc_sts,inc_tunggakan_amt,inc_batch_name) values('" + Applcn_no1.Text + "','" + DD_PBB.SelectedValue + "','" + txt_tahu.Text + "','" + txt_gred.Text + "','" + ddicno1.Rows[0]["org_id"].ToString() + "','" + ddicno1.Rows[0]["stf_curr_dept_cd"].ToString() + "','" + txt_pokok.Text + "','" + et_amt.Text + "','" + ll_amt.Text + "','0.00','" + TextBox5.Text + "','" + TextBox7.Text + "','" + TextBox8.Text + "','" + TextBox4.Text + "','" + TextBox11.Text + "','" + TextBox9.Text + "','"+ Label4.Text + "','" + TextBox18.Text + "','" + TextBox3.Text + "','" + TextBox20.Text + "','" + TextBox16.Text + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + TextBox24.Text + "','" + TextBox23.Text + "','" + TextBox25.Text + "','" + ddicno1.Rows[0]["str_curr_org_cd"].ToString() + "','" + ddicno1.Rows[0]["stf_cur_sub_org"].ToString() + "','" + TextBox27.Text + "','" + TextBox19.Text + "','" + TextBox2.Text + "','0','"+ TextBox6.Text + "','"+ batch_name +"')");

                dtt1 = DBCon.Ora_Execute_table("UPDATE hr_staff_profile set stf_epf_no='" + TextBox12.Text + "',stf_tax_no='" + TextBox17.Text + "',stf_socso_no='" + TextBox10.Text + "' where stf_staff_no='" + Applcn_no1.Text + "'");
                btn_simp.Text = "Kemaskini";
                service.audit_trail("P0069", "Simpan - "+ DD_PBB.SelectedItem.Text + "_" + txt_tahu.Text + "", "NO KAKITANGAN", Applcn_no1.Text);
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Gaji Kakitangan Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                //org ID,Pendapatan Bersih (RM)
                call_grd();

            }
            else
            {
                call_grd();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Maklumat yang.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }


        }
        else
        {
            if (TextBox11.Text != "" && TextBox9.Text != "" /*&& TextBox13.Text != ""*/)
            {

                DataTable ins_aud1 = new DataTable();
                DBCon.Execute_CommamdText("update hr_income set inc_emp_perkeso_amt='" + TextBox24.Text + "',inc_emp_SIP_amt='" + TextBox27.Text + "',inc_emp_kwsp_amt='" + TextBox23.Text + "',inc_emp_kwsp_perc='" + TextBox25.Text + "',inc_grade_cd='" + txt_gred.Text + "',inc_org_id='" + ddicno1.Rows[0]["org_id"].ToString() + "',inc_dept_cd='" + ddicno1.Rows[0]["stf_curr_dept_cd"].ToString() + "',inc_salary_amt='" + txt_pokok.Text + "',inc_cumm_fix_allwnce_amt='" + et_amt.Text + "',inc_cumm_xtra_allwnce_amt='" + ll_amt.Text + "',inc_cumm_deduct_amt ='" + TextBox1.Text + "',inc_ded_amt='" + TextBox2.Text + "',inc_bonus_amt ='" + TextBox5.Text + "',inc_kpi_bonus_amt ='" + TextBox7.Text + "',inc_gross_amt ='" + TextBox8.Text + "',inc_ctg_amt ='" + TextBox4.Text + "',inc_kwsp_amt ='" + TextBox11.Text + "',inc_perkeso_amt ='" + TextBox9.Text + "',inc_SIP_amt ='" + TextBox19.Text + "',inc_pcb_amt ='" + Label4.Text + "',inc_cp38_amt ='" + TextBox18.Text + "',inc_total_deduct_amt ='" + TextBox3.Text + "',inc_nett_amt ='" + TextBox20.Text + "',inc_ot_amt='" + TextBox16.Text + "',inc_gen_id='" + ddicno1.Rows[0]["str_curr_org_cd"].ToString() + "',inc_sub_org_id='" + ddicno1.Rows[0]["stf_cur_sub_org"].ToString() + "',inc_tunggakan_amt='"+ TextBox6.Text +"',inc_upd_id='"+ Session["New"].ToString() + "',inc_upd_dt='"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +"'  where inc_staff_no='" + Applcn_no1.Text + "' and inc_month='" + DD_PBB.SelectedValue + "' and inc_year='" + txt_tahu.Text + "'");
                dtt1 = DBCon.Ora_Execute_table("UPDATE hr_staff_profile set stf_epf_no='" + TextBox12.Text + "',stf_tax_no='" + TextBox17.Text + "',stf_socso_no='" + TextBox10.Text + "' where stf_staff_no='" + Applcn_no1.Text + "'");
                btn_simp.Text = "Kemaskini";
                call_grd();
                service.audit_trail("P0069", "Kemaskini - " + DD_PBB.SelectedItem.Text + "_" + txt_tahu.Text + "", "NO KAKITANGAN", Applcn_no1.Text);
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya DiKemskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
            else
            {
                call_grd();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Maklumat yang.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

            }

        }

    }


    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../SUMBER_MANUSIA/hr_gaji_view.aspx");
    }
}