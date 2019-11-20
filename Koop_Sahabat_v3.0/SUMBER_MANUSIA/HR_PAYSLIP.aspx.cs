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

public partial class HR_PAYSLIP : System.Web.UI.Page
{

    DBConnection DBCon = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DataTable dt = new DataTable();
    string useid = string.Empty;
    string Status = string.Empty;
    string oid = string.Empty;
    string sdd = string.Empty;
    string act_dt = string.Empty;
    string CommandArgument1 = string.Empty, CommandArgument2 = string.Empty, CommandArgument3 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        app_language();
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                useid = Session["New"].ToString();
                if(Session["roles"].ToString() == "R0001")
                {
                    shw_admin.Visible = true;
                }
                else
                {
                    shw_admin.Visible = false;
                }
                TextBox1.Attributes.Add("Readonly", "Readonly");
                TextBox14.Attributes.Add("Readonly", "Readonly");
                TextBox15.Attributes.Add("Readonly", "Readonly");
                TextBox16.Attributes.Add("Readonly", "Readonly");
                TextBox3.Attributes.Add("Readonly", "Readonly");
                txt_org.Attributes.Add("Readonly", "Readonly");
                TextBox5.Attributes.Add("Readonly", "Readonly");
                TextBox4.Text = DateTime.Now.ToString("yyyy");
                gd_month();
                DropDownList1.SelectedValue = DateTime.Now.ToString("MM");
                act_dt = TextBox4.Text + "-" + DropDownList1.SelectedValue;

                bind();
                useid = Session["New"].ToString();


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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('504','448','505','484','13','496','497','1288','513','1150','1379','502','501','906','15')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentUICulture;
            TextInfo txtinfo = culinfo.TextInfo;


            //h1_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            //bb1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            //bb2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            //h3_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            lbl1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            lbl2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            lbl3_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            lbl4_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            lbl5_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
            lbl6_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            lbl7_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());

            h3_tag2.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());

            lbl8_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            lbl9_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());

            Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());  

        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    void view_details()
    {

    }


    public void bind()
    {
        SqlConnection conn = new SqlConnection(cs);
        //string query2 = "select top(1) stf_staff_no,stf_icno,inc_dept_cd,rhj.hr_jaw_desc,inc_grade_cd,isnull(rhg.hr_gred_desc,'') hr_gred_desc,pos_post_cd,jb.hr_jaba_desc,ho.org_name from hr_income hi left join hr_staff_profile hsp on hsp.stf_staff_no=hi.inc_staff_no left join   hr_post_his hph on hph.pos_staff_no=hsp.stf_staff_no left join ref_hr_jawatan rhj on rhj.hr_jaw_Code=hph.pos_post_cd left join Ref_hr_gred rhg on rhg.hr_gred_Code=hi.inc_grade_cd left join Ref_hr_jabatan as jb on jb.hr_jaba_Code= hi.inc_dept_cd left join hr_organization ho on ho.org_gen_id=hsp.str_curr_org_cd where hsp.stf_staff_no='" + Session["New"].ToString() + "' and pos_end_dt = '9999/12/31' and hi.inc_month='" + DropDownList1.SelectedValue + "' and inc_year='" + TextBox4.Text + "'";
        //string query2 = "select top(1) stf_staff_no,stf_icno,inc_dept_cd,rhj.hr_jaw_desc,inc_grade_cd,isnull(rhg.hr_gred_desc,'') hr_gred_desc,pos_post_cd,jb.hr_jaba_desc,ho.org_name from hr_income hi left join hr_staff_profile hsp on hsp.stf_staff_no=hi.inc_staff_no left join   hr_post_his hph on hph.pos_staff_no=hsp.stf_staff_no left join ref_hr_jawatan rhj on rhj.hr_jaw_Code=hph.pos_post_cd left join Ref_hr_gred rhg on rhg.hr_gred_Code=hi.inc_grade_cd left join Ref_hr_jabatan as jb on jb.hr_jaba_Code= hi.inc_dept_cd left join hr_organization ho on ho.org_gen_id=hsp.str_curr_org_cd where hsp.stf_staff_no='" + Session["New"].ToString() + "' and pos_end_dt = '9999/12/31' and hi.inc_month='" + DropDownList1.SelectedValue + "' and inc_year='" + TextBox4.Text + "'";
        string query2 = "select *,o1.op_perg_name from hr_staff_profile as hsp left join   hr_post_his hph on hph.pos_staff_no=hsp.stf_staff_no left join ref_hr_jawatan rhj on rhj.hr_jaw_Code=hph.pos_post_cd left join Ref_hr_gred rhg on rhg.hr_gred_Code=hsp.stf_curr_grade_cd left join Ref_hr_jabatan as jb on jb.hr_jaba_Code= hsp.stf_curr_dept_cd left join hr_organization ho on ho.org_gen_id=hsp.str_curr_org_cd left join hr_organization_pern o1 on o1.op_perg_code=hsp.stf_cur_sub_org where hsp.stf_staff_no='" + Session["New"].ToString() + "' and pos_end_dt = '9999/12/31'";
        conn.Open();
        var sqlCommand2 = new SqlCommand(query2, conn);
        var sqlReader2 = sqlCommand2.ExecuteReader();
        while (sqlReader2.Read())
        {
            TextBox1.Text = useid;
            TextBox14.Text = (string)sqlReader2["stf_icno"].ToString().Trim();
            TextBox15.Text = (string)sqlReader2["hr_gred_desc"].ToString().Trim();
            TextBox16.Text = (string)sqlReader2["hr_jaba_desc"].ToString().Trim();
            TextBox3.Text = (string)sqlReader2["hr_jaw_desc"].ToString().Trim();
            txt_org.Text = (string)sqlReader2["org_name"].ToString().Trim();
            TextBox5.Text = (string)sqlReader2["op_perg_name"].ToString().Trim();

        }
        sqlReader2.Close();
    }


    void gd_month()
    {
        //DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);

        //for (int i = 1; i < 13; i++)
        //{
        //    DropDownList1.Items.Add(new ListItem(info.GetMonthName(i), i.ToString("00")));
        //}

        DateTimeFormatInfo info1 = DateTimeFormatInfo.GetInstance(null);
        int Month = DateTime.Now.Month - 4;
        for (int X = Month; X <= DateTime.Now.Month; X++)
        {
            DropDownList1.Items.Add(new ListItem(X.ToString("#0"), X.ToString("#0")));
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
            DropDownList1.DataSource = dt;
            DropDownList1.DataBind();
            DropDownList1.DataTextField = "hr_month_desc";
            DropDownList1.DataValueField = "hr_month_Code";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
        DropDownList1.SelectedValue = abc.PadLeft(2, '0');
    }

    void instaudt()
    {
        string audcd = "040407";
        string auddec = "SEMAK PENYATA GAJI";
        string usrid = Session["New"].ToString();
        string curdt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string Inssql = "insert into cmn_audit_trail(aud_crt_id,aud_crt_dt,aud_txn_cd,aud   _txn_desc) values ('" + usrid + "','" + curdt + "','" + audcd + "','" + auddec + "')";
        Status = DBCon.Ora_Execute_CommamdText(Inssql);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (TextBox4.Text != "" && DropDownList1.SelectedValue != "")
            {
                string chk_stf = string.Empty;
                act_dt = TextBox4.Text + "-" + DropDownList1.SelectedValue;
                DataTable dd_hrsal = new DataTable();

                if(Session["roles"].ToString() == "R0001")
                {
                    if(TextBox2.Text == "")
                    {
                        chk_stf = TextBox1.Text;
                    }
                    else
                    {
                        chk_stf = TextBox2.Text;
                    }

                }
                else
                {
                    chk_stf = TextBox1.Text;
                }

                dd_hrsal = DBCon.Ora_Execute_table("select * from hr_income where inc_staff_no='" + chk_stf + "' and inc_month='" + DropDownList1.SelectedValue + "' and inc_year='" + TextBox4.Text + "' and inc_app_sts='A'");



                string fmyr = DateTime.Now.ToString("yyyy") + "01";
                string cmyr = DateTime.Now.ToString("yyyy") + DateTime.Now.ToString("MM");
                DataTable dd_hrsal_dt = new DataTable();
                dd_hrsal_dt = DBCon.Ora_Execute_table("select sum(inc_kwsp_amt) as k1,sum(inc_emp_kwsp_amt) as k2,sum(inc_perkeso_amt) as k3,sum(inc_emp_perkeso_amt) as k4,sum(inc_SIP_amt) as k5,sum(inc_emp_SIP_amt) as k6 from hr_income where inc_staff_no='" + chk_stf + "' and inc_month between '01' and '" + DropDownList1.SelectedValue + "' and inc_year='" + TextBox4.Text + "'");

                if (dd_hrsal.Rows.Count != 0)
                {
                   
                    DataTable dd_incsum = new DataTable();
                    dd_incsum = DBCon.Ora_Execute_table("select (sum(inc_pcb_amt) + sum(inc_cp38_amt) + sum(inc_cp38_amt2)) as tt_amt1 from hr_income where inc_staff_no='" + chk_stf + "' and inc_month between '01' and '" + DropDownList1.SelectedValue + "' and inc_year='" + TextBox4.Text + "'");
                    string incamt = string.Empty;
                    if (dd_incsum.Rows[0]["tt_amt1"].ToString() != "")
                    {
                        incamt = dd_incsum.Rows[0]["tt_amt1"].ToString();
                    }
                    else
                    {

                        incamt = "0.00";
                    }

                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    dt = DBCon.Ora_Execute_table("select hsp.stf_name,hsp.stf_epf_no,hsp.stf_tax_no,hsp.stf_socso_no,hsp.str_curr_org_cd,FORMAT(hsp.stf_service_start_dt,'dd/MM/yyyy', 'en-us') as stf_service_start_dt,hsp.stf_staff_no,ISNULL(hsp.stf_bank_acc_no,'') as stf_bank_acc_no,ISNULL(nb.Bank_Name,'') as Bank_Name,hsp.stf_icno,inc_dept_cd,inc_grade_cd,pos_post_cd,ISNULL(jb.hr_jaw_desc,'') as hr_jaba_desc,ISNULL(pt.hr_traf_desc,'') as job_sts from hr_income hi left join hr_staff_profile hsp on hsp.stf_staff_no=hi.inc_staff_no left join   hr_post_his hph on hph.pos_staff_no=hsp.stf_staff_no left join Ref_hr_Jawatan as jb on jb.hr_jaw_Code= hsp.stf_curr_post_cd left join Ref_Nama_Bank as nb on nb.Bank_Code=hsp.stf_bank_cd left join Ref_hr_penj_traf pt on pt.hr_traf_Code=pos_job_sts_cd where hsp.stf_staff_no='" + chk_stf + "' and pos_end_dt = '9999-12-31' and hi.inc_month='" + DropDownList1.SelectedValue + "' and inc_year='" + TextBox4.Text + "'");
                    ds.Tables.Add(dt);

                    DataTable dt1 = new DataTable();
                    dt1 = DBCon.Ora_Execute_table("select hd.ded_staff_no,po.hr_poto_desc,ded_deduct_amt,hd.ded_deduct_type_cd from hr_deduction as hd inner join Ref_hr_potongan as PO on PO.hr_poto_Code=hd.ded_deduct_type_cd where hd.ded_staff_no='" + chk_stf + "' and (('" + act_dt.ToString() + "') between FORMAT(hd.ded_start_dt,'yyyy-MM') And FORMAT(hd.ded_end_dt,'yyyy-MM'))");
                    ds.Tables.Add(dt1);

                    DataTable dt2 = new DataTable();
                    dt2 = DBCon.Ora_Execute_table("select a.fxa_staff_no,el.hr_elau_desc as hr_elaun_desc,a.fxa_allowance_amt from (select * from hr_fixed_allowance as fx where ('" + act_dt.ToString() + "' between FORMAT(fx.fxa_eff_dt,'yyyy-MM') And FORMAT(fx.fxa_end_dt,'yyyy-MM')) and fx.fxa_staff_no='" + chk_stf + "') as a left join Ref_hr_jenis_elaun as EL on EL.hr_elau_Code=a.fxa_allowance_type_cd");
                    ds.Tables.Add(dt2);

                    DataTable dt3 = new DataTable();
                    dt3 = DBCon.Ora_Execute_table("select a.xta_staff_no,EL.hr_elau_desc as hr_elaun_desc,a.xta_allowance_amt from (select * from hr_extra_allowance as ea where ea.xta_staff_no='" + chk_stf + "' and ('" + act_dt.ToString() + "' between FORMAT(ea.xta_eff_dt,'yyyy-MM') And FORMAT(ea.xta_end_dt,'yyyy-MM'))) as a left join Ref_hr_jenis_elaun as EL on EL.hr_elau_Code=a.xta_allowance_type_cd");
                    ds.Tables.Add(dt3);

                    DataTable dt5 = new DataTable();
                    dt5 = DBCon.Ora_Execute_table("select *,hr_tung_desc from hr_tunggakan left join Ref_hr_tunggakan on hr_tung_Code=tun_type_cd where tun_staff_no='" + chk_stf + "' and  tun_year='" + TextBox4.Text + "' and tun_month='" + DropDownList1.SelectedValue + "'");
                    ds.Tables.Add(dt5);

                    DataTable dt6 = new DataTable();
                    dt6 = DBCon.Ora_Execute_table("select s2.typeklm_desc +'  ' + otl_remark as nm1,otl_remark,otl_ot_amt from hr_ot s1 left join Ref_hr_type_klm s2 on s2.typeklm_cd=s1.otl_ot_type_cd where otl_staff_no='" + chk_stf + "' and year(otl_work_dt) = '" + TextBox4.Text + "' and Month(otl_work_dt) = '" + DropDownList1.SelectedValue + "'");
                    ds.Tables.Add(dt6);

                    RptviwerStudent.Reset();
                    string filename;

                    DataTable dd_org = new DataTable();
                    dd_org = DBCon.Ora_Execute_table("select org_epf_no,org_socso_no,org_temp_ind,org_income_tax_no,org_name from hr_organization where org_gen_id='" + dt.Rows[0]["str_curr_org_cd"].ToString() + "'");


                    decimal sal_amt = decimal.Parse(dd_hrsal.Rows[0]["inc_salary_amt"].ToString());
                    decimal el_amt = decimal.Parse(dd_hrsal.Rows[0]["inc_cumm_fix_allwnce_amt"].ToString());
                    decimal ll_amt = decimal.Parse(dd_hrsal.Rows[0]["inc_cumm_xtra_allwnce_amt"].ToString());

                    DataTable dd_hrsal4 = new DataTable();
                    dd_hrsal4 = DBCon.Ora_Execute_table("select fx.ded_staff_no,sum(fx.ded_deduct_amt) as samt from hr_deduction as fx where ('" + act_dt.ToString() + "' between FORMAT(fx.ded_start_dt,'yyyy-MM') And FORMAT(fx.ded_end_dt,'yyyy-MM')) and fx.ded_staff_no='" + chk_stf + "' group by fx.ded_staff_no");

                    string hrs4_amt = string.Empty;
                    if (dd_hrsal4.Rows.Count != 0 && dd_hrsal4.Rows[0]["samt"].ToString() != "")
                    {
                        hrs4_amt = double.Parse(dd_hrsal4.Rows[0]["samt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                    }
                    else
                    {
                        hrs4_amt = "0.00";
                    }
                    DataTable dd_inc_tax = new DataTable();
                    dd_inc_tax = DBCon.Ora_Execute_table("select tax_pcb_amt   from hr_income_tax where ('" + act_dt.ToString() + "' between FORMAT(tax_pcb_start_dt ,'yyyy-MM') And FORMAT(tax_pcb_end_dt,'yyyy-MM')) and tax_staff_no='" + chk_stf + "'");
                 
                    string pcb4_amt = string.Empty;
                    if (dd_inc_tax.Rows.Count != 0 && dd_inc_tax.Rows[0]["tax_pcb_amt"].ToString() != "")
                    {
                        pcb4_amt = double.Parse(dd_inc_tax.Rows[0]["tax_pcb_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                    }
                    else
                    {
                        pcb4_amt = "0.00";
                    }

                    DataTable dd_inc_cp38 = new DataTable();
                    dd_inc_cp38 = DBCon.Ora_Execute_table("select tax_cp38_amt1   from hr_income_tax where ('" + act_dt.ToString() + "' between FORMAT(tax_cp38_start_dt1 ,'yyyy-MM') And FORMAT(tax_cp38_end_dt1,'yyyy-MM')) and tax_type ='2' and tax_staff_no='" + chk_stf + "'");
                    string hr_cpamt2 = string.Empty;
                    
                    if (dd_inc_cp38.Rows.Count != 0 && dd_inc_cp38.Rows[0]["tax_cp38_amt1"].ToString() != "")
                    {
                        double cp2 = double.Parse(dd_inc_cp38.Rows[0]["tax_cp38_amt1"].ToString());
                        hr_cpamt2 = cp2.ToString("C").Replace("$", "").Replace("RM", "");
                    }
                    else
                    {
                        hr_cpamt2 = "0.00";
                    }
                    DataTable dd_tun = new DataTable();
                    dd_tun = DBCon.Ora_Execute_table("select sum(clm_claim_amt) as camt from hr_claim where clm_staff_no='" + chk_stf + "' and clm_month='" + DropDownList1.SelectedValue + "' and clm_year='" + TextBox4.Text + "'");

                    string ded_at = string.Empty;
                    DataTable dd_deduct = new DataTable();
                    //dd_deduct = DBCon.Ora_Execute_table("select sum(ind_deduct_amt) as amt1,ind_ref_no from hr_income_deduct where ind_staff_no='" + TextBox1.Text + "' and ind_deduct_type_cd='04' and ind_month between '01' and '" + DropDownList1.SelectedValue + "' and ind_year='" + TextBox4.Text + "' group by ind_ref_no");
                    dd_deduct = DBCon.Ora_Execute_table("SELECT top(1) a.amt1,b.ind_ref_no FROM (select sum(ISNULL(ind_deduct_amt,'')) as amt1 from hr_income_deduct where ind_staff_no='" + chk_stf + "' and ind_deduct_type_cd='04' and ind_month between '01' and '" + DropDownList1.SelectedValue + "' and ind_year='" + TextBox4.Text + "') as a full outer join (select * from hr_income_deduct where ind_staff_no='" + chk_stf + "' and ind_deduct_type_cd='04' and ind_month between '01' and '" + DropDownList1.SelectedValue + "' and ind_year='" + TextBox4.Text + "') as b on b.ind_staff_no='" + chk_stf + "'");

                    if (dd_deduct.Rows.Count != 0 && dd_deduct.Rows[0]["amt1"].ToString() != "")
                    {
                        ded_at = dd_deduct.Rows[0]["amt1"].ToString();
                    }
                    else
                    {
                        ded_at = "0.00";
                    }

                    string tun_amt = string.Empty;
                    if (dd_tun.Rows[0]["camt"].ToString() != "")
                    {
                        tun_amt = dd_tun.Rows[0]["camt"].ToString();
                    }
                    else
                    {
                        tun_amt = "0.00";
                    }

                    string aq1 = string.Empty, aq2 = string.Empty, aq3 = string.Empty, aq4 = string.Empty, aq5 = string.Empty, aq6 = string.Empty, aq7 = string.Empty;

                    string kq1 = string.Empty, kq2 = string.Empty, kq3 = string.Empty, kq4 = string.Empty, kq5 = string.Empty, kq6 = string.Empty;

                    if (dd_hrsal.Rows[0]["inc_emp_kwsp_amt"].ToString() != "")
                    {
                        aq1 = dd_hrsal.Rows[0]["inc_emp_kwsp_amt"].ToString();
                    }
                    else
                    {
                        aq1 = "0.00";
                    }

                    if (dd_hrsal.Rows[0]["inc_emp_perkeso_amt"].ToString() != "")
                    {
                        aq2 = dd_hrsal.Rows[0]["inc_emp_perkeso_amt"].ToString();
                    }
                    else
                    {
                        aq2 = "0.00";
                    }

                    if (dd_hrsal.Rows[0]["inc_bonus_amt"].ToString() != "")
                    {
                        aq3 = dd_hrsal.Rows[0]["inc_bonus_amt"].ToString();
                    }
                    else
                    {
                        aq3 = "0.00";
                    }

                    if (dd_hrsal.Rows[0]["inc_kpi_bonus_amt"].ToString() != "")
                    {
                        aq4 = dd_hrsal.Rows[0]["inc_kpi_bonus_amt"].ToString();
                    }
                    else
                    {
                        aq4 = "0.00";
                    }

                    if (dd_hrsal.Rows[0]["inc_ot_amt"].ToString() != "")
                    {
                        aq5 = dd_hrsal.Rows[0]["inc_ot_amt"].ToString();
                    }
                    else
                    {
                        aq5 = "0.00";
                    }

                    if (dd_hrsal.Rows[0]["inc_tunggakan_amt"].ToString() != "")
                    {
                        aq6 = dd_hrsal.Rows[0]["inc_tunggakan_amt"].ToString();
                    }
                    else
                    {
                        aq6 = "0.00";
                    }

                    if (dd_hrsal.Rows[0]["inc_emp_sip_amt"].ToString() != "")
                    {
                        aq7 = dd_hrsal.Rows[0]["inc_emp_sip_amt"].ToString();
                    }
                    else
                    {
                        aq7 = "0.00";
                    }

                    if (dd_hrsal_dt.Rows[0]["k1"].ToString() != "")
                    {
                        kq1 = dd_hrsal_dt.Rows[0]["k1"].ToString();
                    }
                    else
                    {
                        kq1 = "0.00";
                    }

                    if (dd_hrsal_dt.Rows[0]["k2"].ToString() != "")
                    {
                        kq2 = dd_hrsal_dt.Rows[0]["k2"].ToString();
                    }
                    else
                    {
                        kq2 = "0.00";
                    }

                    if (dd_hrsal_dt.Rows[0]["k3"].ToString() != "")
                    {
                        kq3 = dd_hrsal_dt.Rows[0]["k3"].ToString();
                    }
                    else
                    {
                        kq3 = "0.00";
                    }

                    if (dd_hrsal_dt.Rows[0]["k4"].ToString() != "")
                    {
                        kq4 = dd_hrsal_dt.Rows[0]["k4"].ToString();
                    }
                    else
                    {
                        kq4 = "0.00";
                    }

                    if (dd_hrsal_dt.Rows[0]["k5"].ToString() != "")
                    {
                        kq5 = dd_hrsal_dt.Rows[0]["k5"].ToString();
                    }
                    else
                    {
                        kq5 = "0.00";
                    }

                    if (dd_hrsal_dt.Rows[0]["k6"].ToString() != "")
                    {
                        kq6 = dd_hrsal_dt.Rows[0]["k6"].ToString();
                    }
                    else
                    {
                        kq6 = "0.00";
                    }

                    string jum_pend = (double.Parse(sal_amt.ToString()) + double.Parse(el_amt.ToString()) + double.Parse(ll_amt.ToString()) + double.Parse(aq3.ToString()) + double.Parse(aq4.ToString()) + double.Parse(aq5.ToString()) + double.Parse(aq6.ToString())).ToString("C").Replace("$", "").Replace("RM", "");

                    //string jum_pote = (double.Parse(dd_hrsal.Rows[0]["inc_ctg_amt"].ToString()) + double.Parse(dd_hrsal.Rows[0]["inc_kwsp_amt"].ToString()) + double.Parse(dd_hrsal.Rows[0]["inc_perkeso_amt"].ToString()) + double.Parse(dd_hrsal.Rows[0]["inc_pcb_amt"].ToString()) + double.Parse(dd_hrsal.Rows[0]["inc_cp38_amt"].ToString()) + double.Parse(hr_cpamt2.ToString()) + double.Parse(hrs4_amt.ToString())).ToString("C").Replace("$", "").Replace("RM", "");
                    string jum_pote = (double.Parse(dd_hrsal.Rows[0]["inc_cp38_amt"].ToString()) + double.Parse(dd_hrsal.Rows[0]["inc_SIP_amt"].ToString()) + double.Parse(dd_hrsal.Rows[0]["inc_ctg_amt"].ToString()) + double.Parse(dd_hrsal.Rows[0]["inc_kwsp_amt"].ToString()) + double.Parse(dd_hrsal.Rows[0]["inc_perkeso_amt"].ToString()) + double.Parse(pcb4_amt.ToString())  /*double.Parse(dd_hrsal.Rows[0]["inc_pcb_amt"].ToString()) + double.Parse(dd_hrsal.Rows[0]["inc_cp38_amt"].ToString()) +*/ + double.Parse(hrs4_amt.ToString())).ToString("C").Replace("$", "").Replace("RM", "");

                    string tot_nettamt = (double.Parse(jum_pend) - double.Parse(jum_pote)).ToString("C").Replace("$", "").Replace("RM", "");
                    string org_typ = string.Empty;
                    string fname1 = string.Empty;
                    //if (dd_org.Rows[0]["org_temp_ind"].ToString() == "A")
                    //{
                    //    org_typ = "ar-rahnu.rdlc";
                    //    //org_typ = "koop_sahabat.rdlc";
                    //    //fname1 = "AR-RAHNU";
                    //}
                    //else if (dd_org.Rows[0]["org_temp_ind"].ToString() == "k")
                    //{
                    //    org_typ = "koop_sahabat.rdlc";
                    //    //fname1 = "KOOP_SAHABAT";
                    //}
                    //else if (dd_org.Rows[0]["org_temp_ind"].ToString() == "G")
                    //{
                    //    org_typ = "GEMALAI_PLANTATION.rdlc";
                    //    //fname1 = "GEMALAI_PLANTATION";
                    //}
                    //else
                    //{
                    //    org_typ = "koop_sahabat.rdlc";
                    //    //fname1 = "KOOP_SAHABAT";
                    //}

                    //string img_name = Server.MapPath("~/FILES/org_logo/logo4.png");

                    //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report.rdlc");




                   




                    RptviwerStudent.LocalReport.ReportPath = "SUMBER_MANUSIA/KTHB_PAYSLIP.rdlc";
                    RptviwerStudent.LocalReport.EnableExternalImages = true;
                    string imagePath = new Uri(Server.MapPath("~/FILES/org_logo/" + dd_org.Rows[0]["org_temp_ind"].ToString() + "")).AbsoluteUri;
                    ReportDataSource rds = new ReportDataSource("ks", dt);
                    ReportDataSource rds1 = new ReportDataSource("ks1", dt1);
                    ReportDataSource rds2 = new ReportDataSource("ks2", dt2);
                    ReportDataSource rds3 = new ReportDataSource("ks3", dt3);
                    ReportDataSource rds4 = new ReportDataSource("ks5", dt5);
                    ReportDataSource rds5 = new ReportDataSource("ks6", dt6);
                    ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("sal_amt", sal_amt.ToString("C").Replace("$","")),
                     new ReportParameter("et_amt", el_amt.ToString("C").Replace("$","")),
                     new ReportParameter("ll_amt", ll_amt.ToString("C").Replace("$","")),
                     new ReportParameter("tun_amt", double.Parse(tun_amt.ToString()).ToString("C").Replace("$","")),
                     new ReportParameter("jum_pen", jum_pend.ToString()),
                     new ReportParameter("pctg_amt", double.Parse(dd_hrsal.Rows[0]["inc_ctg_amt"].ToString()).ToString("C").Replace("$","")),
                     new ReportParameter("ckwsp_amt", double.Parse(dd_hrsal.Rows[0]["inc_kwsp_amt"].ToString()).ToString("C").Replace("$","")),
                     new ReportParameter("perk_amt", double.Parse(dd_hrsal.Rows[0]["inc_perkeso_amt"].ToString()).ToString("C").Replace("$","")),
                     new ReportParameter("ppcb_amt", double.Parse(dd_hrsal.Rows[0]["inc_pcb_amt"].ToString()).ToString("C").Replace("$","")),
                     new ReportParameter("cp38_amt1", double.Parse(dd_hrsal.Rows[0]["inc_cp38_amt"].ToString()).ToString("C").Replace("$","")),
                     new ReportParameter("cp38_amt2", hr_cpamt2.ToString()),
                     new ReportParameter("jum_pote", jum_pote.ToString()),
                     new ReportParameter("nett_amt", tot_nettamt),
                     new ReportParameter("kwsp_maj", double.Parse(aq1).ToString("C").Replace("$","")),
                     new ReportParameter("perk_maj", double.Parse(aq2).ToString("C").Replace("$","")),
                     new ReportParameter("sip_maj", double.Parse(aq7).ToString("C").Replace("$","")),
                     new ReportParameter("kwsp_pek", dt.Rows[0]["stf_epf_no"].ToString()),
                     new ReportParameter("kwsp_majno", dd_org.Rows[0]["org_epf_no"].ToString()),
                     new ReportParameter("per_pek", dt.Rows[0]["stf_socso_no"].ToString()),
                     new ReportParameter("per_majno", dd_org.Rows[0]["org_socso_no"].ToString()),
                     new ReportParameter("sip_pek", dt.Rows[0]["stf_tax_no"].ToString()),
                     new ReportParameter("sip_majno", dd_org.Rows[0]["org_income_tax_no"].ToString()),
                     new ReportParameter("cuk_pen", dt.Rows[0]["stf_tax_no"].ToString()),
                     new ReportParameter("k1", double.Parse(kq1).ToString("C").Replace("$","")),
                     new ReportParameter("k2", double.Parse(kq2).ToString("C").Replace("$","")),
                     new ReportParameter("k3", double.Parse(kq3).ToString("C").Replace("$","")),
                     new ReportParameter("k4", double.Parse(kq4).ToString("C").Replace("$","")),
                     new ReportParameter("k5", double.Parse(incamt.ToString()).ToString("C").Replace("$","")),
                      new ReportParameter("k6", dd_deduct.Rows[0]["ind_ref_no"].ToString()),
                     new ReportParameter("k7", double.Parse(ded_at.ToString()).ToString("C").Replace("$","")),
                     new ReportParameter("k8", DropDownList1.SelectedItem.Text.ToUpper()),
                     new ReportParameter("k9", TextBox4.Text),
                     new ReportParameter("imgname", imagePath),
                      new ReportParameter("org", dd_org.Rows[0]["org_name"].ToString()),
                      new ReportParameter("k10", double.Parse(aq3).ToString("C").Replace("$","")),
                      new ReportParameter("k11", double.Parse(aq4).ToString("C").Replace("$","")),
                      new ReportParameter("k12", double.Parse(aq5).ToString("C").Replace("$","")),
                      new ReportParameter("k13", double.Parse(dd_hrsal.Rows[0]["inc_SIP_amt"].ToString()).ToString("C").Replace("$","")),
                      new ReportParameter("k14", double.Parse(dd_hrsal.Rows[0]["inc_emp_SIP_amt"].ToString()).ToString("C").Replace("$","")),
                      new ReportParameter("k15", double.Parse(kq5).ToString("C").Replace("$","")),
                      new ReportParameter("k16", double.Parse(kq6).ToString("C").Replace("$","")),
                      new ReportParameter("k17", dt.Rows[0]["job_sts"].ToString()),
                      new ReportParameter("k18",double.Parse(pcb4_amt).ToString("C").Replace("$",""))
                     };

                    RptviwerStudent.LocalReport.SetParameters(rptParams);
                    RptviwerStudent.LocalReport.DataSources.Add(rds);
                    RptviwerStudent.LocalReport.DataSources.Add(rds1);
                    RptviwerStudent.LocalReport.DataSources.Add(rds2);
                    RptviwerStudent.LocalReport.DataSources.Add(rds3);
                    RptviwerStudent.LocalReport.DataSources.Add(rds4);
                    RptviwerStudent.LocalReport.DataSources.Add(rds5);
                    RptviwerStudent.LocalReport.DisplayName = "" + dt.Rows[0]["stf_staff_no"].ToString().ToUpper().Trim() + "_" + DropDownList1.SelectedValue + "" + TextBox4.Text + "";

                    RptviwerStudent.LocalReport.Refresh();
                    filename = string.Format("{0}.{1}", "" + dt.Rows[0]["stf_staff_no"].ToString().ToUpper().Trim() + "_" + DropDownList1.SelectedValue + "" + TextBox4.Text + "", "pdf");
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
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila masukan Input Carian',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Issue',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetCompletionList(string prefixText, int count)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "select stf_staff_no from hr_staff_profile left join hr_post_his s1 on s1.pos_staff_no=stf_staff_no where Status='A' and pos_end_dt >= '"+ DateTime.Now.ToString("yyyy-MM-dd") + "' and stf_staff_no like '%"+ prefixText + "%'";
                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> countryNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    if (sdr.HasRows == true)
                    {
                        while (sdr.Read())
                        {
                            countryNames.Add(sdr["stf_staff_no"].ToString());

                        }
                    }
                    else
                    {
                        countryNames.Add("Rekod Tidak Dijumpai.");
                    }
                }

                con.Close();
                return countryNames;
            }
        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../SUMBER_MANUSIA/HR_PAYSLIP.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../SUMBER_MANUSIA/HR_PAYSLIP_view.aspx");
    }


}