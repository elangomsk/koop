
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
using System.Threading;
using System.Net;
using System.Net.Mail;
using System.Collections;

public partial class hr_gaji_kelompok : System.Web.UI.Page
{
    DBConnection dbcon = new DBConnection();
    Mailcoms ObjMail = new Mailcoms();
    StudentWebService service = new StudentWebService();
    SMS ObjSms = new SMS();
    CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
    DBConnection Dblog = new DBConnection();
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string level, userid;
    DBConnection DBCon = new DBConnection();
    string Status = string.Empty, sqry = string.Empty;
    DataSet ds = new DataSet();
    String count_text;
    int incNumber = 1;
    int incNumber1 = 1;
    string role_1 = string.Empty, role_2 = string.Empty, query = string.Empty;
    Boolean[] notNulls;
    decimal grdTotal = 0;
    decimal salary = 0, ext_allow = 0, ot_allow = 0, bonus_thn = 0, bonus_kpi = 0, ot_klm = 0, jp = 0, car_kwsp = 0, car_kwsp_emp = 0, car_perkeso = 0, car_perkeso_emp = 0;
    decimal sip = 0, sip_emp = 0, pcb = 0, cp_38 = 0, pkrm = 0, pbrm = 0, ded_amt = 0, tun_amt = 0;
    private double dblRunningTotal;
    //class variable to store sum of order amounts of all orders on all pages
    private double dblTotalAmount;
    public object HtmlParser { get; private set; }

    protected void Page_Load(object sender, EventArgs e)
    {

        //int colCount = gvSelected.Columns.Count;
        //notNulls = new Boolean[colCount];

        //for (int i = 0; i < colCount; i++)
        //{
        //    notNulls[i] = false;
        //}        
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                Month();
                Year();
                userid = Session["New"].ToString();
                //TextBox1.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
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
    protected void BindGridview(object sender, EventArgs e)
    {
      
        if (txt_tahun.SelectedValue != "")
        {
            Session["chkditems"] = null;
            show_cnt1.Visible = true;
            grid();
        }
        else
        {
            show_cnt1.Visible = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Tahun.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }

    void get_stf_details()
    {
        DataTable get_stf_inc = new DataTable();
        get_stf_inc = DBCon.Ora_Execute_table("SELECT ISNULL(STUFF ((SELECT ',' + ISNULL(inc_staff_no ,'') FROM hr_income where inc_year='" + txt_tahun.SelectedValue + "' and inc_month='" + DD_bulancaruman.SelectedValue + "' FOR XML PATH ('')  ),1,1,''), '')  as scode");
        if(get_stf_inc.Rows[0]["scode"].ToString() != "")
        {
            if (chk_assign_rkd.Checked == true)
            {
                Button3.Visible = false;
                Button5.Visible = true;
                sqry = "where stf_staff_no IN ('" + get_stf_inc.Rows[0]["scode"].ToString().Replace(",", "','") + "')";
            }
            else
            {
                Button5.Visible = false;
                Button3.Visible = true;
                sqry = "where stf_staff_no NOT IN ('" + get_stf_inc.Rows[0]["scode"].ToString().Replace(",", "','") + "')";
            }
        }
        else
        {
            if (chk_assign_rkd.Checked == true)
            {
                Button3.Visible = false;
                Button5.Visible = true;
                sqry = "where stf_staff_no IN ('')";
            }
            else
            {
                Button5.Visible = false;
                Button3.Visible = true;
                sqry = "";
            }
          
        }

        query = " select stf_staff_no,stf_name,stf_icno,'" + DD_bulancaruman.SelectedValue + "' as mnth,'" + txt_tahun.SelectedValue + "' as yer,org_id,ISNULL(slr_salary_amt,'0.00') as salary,stf_cur_sub_org,str_curr_org_cd,((ISNULL(c.samt,'0.00') +  "
                          + " ISNULL(c1.fx_pamt,'0.00')) - ISNULL(c1.fx_amt,'0.00')) as fix_alwncce,((ISNULL(d.tot_xta,'0.00') + ISNULL(d1.xt_pamt,'0.00')) - ISNULL(d1.xt_amt,'0.00')) as xta_alwnce,ISNULL(i.samt, '0.00') "
                          + " as ded_amt,ISNULL(e.a1, '0.00') as bns_amt,ISNULL(e.a2, '0.00') as kpi_bns_amt, ISNULL((ISNULL(slr_salary_amt, '0.00') + ((ISNULL(c.samt, '0.00') + ISNULL(c1.fx_pamt, '0.00')) -  "
                          + " ISNULL(c1.fx_amt, '0.00')) + ((ISNULL(d.tot_xta, '0.00') + ISNULL(d1.xt_pamt, '0.00')) - ISNULL(d1.xt_amt, '0.00')) +  ISNULL(e.a1, '0.00') +   ISNULL(e.a2, '0.00') + ISNULL(n.tun_amt,  "
                          + " '0.00') + ISNULL(j.amt1, '0.00')), '0.00') as gross_amt,isnull(f.samt, '0.00') as ctg_amt,ISNULL(g.epf_amt, '0.00') as kwsp_amt, "
                          + " ISNULL(case when stf_age < '60' then (select top(1) per_employee_amt from hr_comm_perkeso where  "
                          + " ((slr_salary_amt + ((ISNULL(c_p1.samt, '0.00') + ISNULL(c1_p1.fx_pamt, '0.00')) - ISNULL(c1_p1.fx_amt, '0.00')) +  "
                          + " ((ISNULL(d_p1.tot_xta, '0.00') + ISNULL(d1_p1.xt_pamt, '0.00')) - ISNULL(d1_p1.xt_amt, '0.00')) + ISNULL(e_p1.tun_amt, '0.00') + ISNULL(f_p1.amt1, '0.00') "
                          + " ) - ISNULL(g_p1.samt, '0.00')) between per_min_income_amt and per_max_income_amt) else  "
                          + " (select top(1) per_employer_amt2 from hr_comm_perkeso where  "
                          + " ((slr_salary_amt + ((ISNULL(c_p1.samt, '0.00') + ISNULL(c1_p1.fx_pamt, '0.00')) - ISNULL(c1_p1.fx_amt, '0.00')) +  "
                          + " ((ISNULL(d_p1.tot_xta, '0.00') + ISNULL(d1_p1.xt_pamt, '0.00')) - ISNULL(d1_p1.xt_amt, '0.00')) + ISNULL(e_p1.tun_amt, '0.00') + ISNULL(f_p1.amt1, '0.00') "
                          + " ) - ISNULL(g_p1.samt, '0.00')) between per_min_income_amt and per_max_income_amt) end ,'0.00') as perkeso_amt, "
                          + " ISNULL(case when stf_age < '60' then (select top(1) per_employer_amt1 from hr_comm_perkeso where  "
                          + " ((slr_salary_amt + ((ISNULL(c_p1.samt, '0.00') + ISNULL(c1_p1.fx_pamt, '0.00')) - ISNULL(c1_p1.fx_amt, '0.00')) +  "
                          + " ((ISNULL(d_p1.tot_xta, '0.00') + ISNULL(d1_p1.xt_pamt, '0.00')) - ISNULL(d1_p1.xt_amt, '0.00')) + ISNULL(e_p1.tun_amt, '0.00') + ISNULL(f_p1.amt1, '0.00') "
                          + " ) - ISNULL(g_p1.samt, '0.00')) between per_min_income_amt and per_max_income_amt) else  "
                          + " (select top(1) per_employer_amt2 from hr_comm_perkeso where  "
                          + " ((slr_salary_amt + ((ISNULL(c_p1.samt, '0.00') + ISNULL(c1_p1.fx_pamt, '0.00')) - ISNULL(c1_p1.fx_amt, '0.00')) +  "
                          + " ((ISNULL(d_p1.tot_xta, '0.00') + ISNULL(d1_p1.xt_pamt, '0.00')) - ISNULL(d1_p1.xt_amt, '0.00')) + ISNULL(e_p1.tun_amt, '0.00') + ISNULL(f_p1.amt1, '0.00') "
                          + " ) - ISNULL(g_p1.samt, '0.00')) between per_min_income_amt and per_max_income_amt) end , '0.00')as emp_perkeso_amt, "
                          + " ISNULL(case when stf_age < '60' then (select top(1) SIP_employee_amt from hr_comm_SIP where  "
                          + " ((slr_salary_amt + ((ISNULL(c_s1.samt, '0.00') + ISNULL(c1_s1.fx_pamt, '0.00')) - ISNULL(c1_s1.fx_amt, '0.00')) +  "
                          + " ((ISNULL(d_s1.tot_xta, '0.00') + ISNULL(d1_s1.xt_pamt, '0.00')) - ISNULL(d1_s1.xt_amt, '0.00')) + ISNULL(e_s1.tun_amt, '0.00') + ISNULL(f_s1.amt1, '0.00') "
                          + " ) - ISNULL(g_s1.samt, '0.00')) between SIP_min_income_amt and SIP_max_income_amt) else  "
                          + " (select top(1) SIP_employee_amt from hr_comm_SIP where  "
                          + " ((slr_salary_amt + ((ISNULL(c_s1.samt, '0.00') + ISNULL(c1_s1.fx_pamt, '0.00')) - ISNULL(c1_s1.fx_amt, '0.00')) +  "
                          + " ((ISNULL(d_s1.tot_xta, '0.00') + ISNULL(d1_s1.xt_pamt, '0.00')) - ISNULL(d1_s1.xt_amt, '0.00')) + ISNULL(e_s1.tun_amt, '0.00') + ISNULL(f_s1.amt1, '0.00') "
                          + " ) - ISNULL(g_s1.samt, '0.00')) between SIP_min_income_amt and SIP_max_income_amt) end ,'0.00') as sip_amt1, "
                          + " ISNULL(case when stf_age < '60' then (select top(1) SIP_employer_amt1 from hr_comm_SIP where  "
                          + " ((slr_salary_amt + ((ISNULL(c_s1.samt, '0.00') + ISNULL(c1_s1.fx_pamt, '0.00')) - ISNULL(c1_s1.fx_amt, '0.00')) +  "
                          + " ((ISNULL(d_s1.tot_xta, '0.00') + ISNULL(d1_s1.xt_pamt, '0.00')) - ISNULL(d1_s1.xt_amt, '0.00')) + ISNULL(e_s1.tun_amt, '0.00') + ISNULL(f_s1.amt1, '0.00') "
                          + " ) - ISNULL(g_s1.samt, '0.00')) between SIP_min_income_amt and SIP_max_income_amt) else  "
                          + " (select top(1) SIP_employer_amt2 from hr_comm_SIP where  "
                          + " ((slr_salary_amt + ((ISNULL(c_s1.samt, '0.00') + ISNULL(c1_s1.fx_pamt, '0.00')) - ISNULL(c1_s1.fx_amt, '0.00')) +  "
                          + " ((ISNULL(d_s1.tot_xta, '0.00') + ISNULL(d1_s1.xt_pamt, '0.00')) - ISNULL(d1_s1.xt_amt, '0.00')) + ISNULL(e_s1.tun_amt, '0.00') + ISNULL(f_s1.amt1, '0.00') "
                          + " ) - ISNULL(g_s1.samt, '0.00')) between SIP_min_income_amt and SIP_max_income_amt) end, '0.00') as sip_emp_amt1, "
                          + "  '0.00' as sip_amt,cast(ISNULL(m1.pcb_amt,'0.00') as money) as pcb_amt,cast(ISNULL(m.tax_cp38_amt1,'0.00') as money) as cp38_amt, "
                          + "  ISNULL((ISNULL(g.epf_amt, '0.00') +   (case when stf_age < '60' then (select top(1) per_employee_amt from hr_comm_perkeso where  "
                          + " ((slr_salary_amt + ((ISNULL(c_p1.samt, '0.00') + ISNULL(c1_p1.fx_pamt, '0.00')) - ISNULL(c1_p1.fx_amt, '0.00')) +  "
                          + " ((ISNULL(d_p1.tot_xta, '0.00') + ISNULL(d1_p1.xt_pamt, '0.00')) - ISNULL(d1_p1.xt_amt, '0.00')) + ISNULL(e_p1.tun_amt, '0.00') + ISNULL(f_p1.amt1, '0.00') "
                          + " ) - ISNULL(g_p1.samt, '0.00')) between per_min_income_amt and per_max_income_amt) else  "
                          + " (select top(1) per_employer_amt2 from hr_comm_perkeso where  "
                          + " ((slr_salary_amt + ((ISNULL(c_p1.samt, '0.00') + ISNULL(c1_p1.fx_pamt, '0.00')) - ISNULL(c1_p1.fx_amt, '0.00')) +  "
                          + " ((ISNULL(d_p1.tot_xta, '0.00') + ISNULL(d1_p1.xt_pamt, '0.00')) - ISNULL(d1_p1.xt_amt, '0.00')) + ISNULL(e_p1.tun_amt, '0.00') + ISNULL(f_p1.amt1, '0.00') "
                          + " ) - ISNULL(g_p1.samt, '0.00')) between per_min_income_amt and per_max_income_amt) end) + (case when stf_age < '60' then (select top(1) SIP_employee_amt from hr_comm_SIP where  "
                          + " ((slr_salary_amt + ((ISNULL(c_s1.samt, '0.00') + ISNULL(c1_s1.fx_pamt, '0.00')) - ISNULL(c1_s1.fx_amt, '0.00')) +  "
                          + " ((ISNULL(d_s1.tot_xta, '0.00') + ISNULL(d1_s1.xt_pamt, '0.00')) - ISNULL(d1_s1.xt_amt, '0.00')) + ISNULL(e_s1.tun_amt, '0.00') + ISNULL(f_s1.amt1, '0.00') "
                          + " ) - ISNULL(g_s1.samt, '0.00')) between SIP_min_income_amt and SIP_max_income_amt) else  "
                          + " (select top(1) SIP_employee_amt from hr_comm_SIP where  "
                          + " ((slr_salary_amt + ((ISNULL(c_s1.samt, '0.00') + ISNULL(c1_s1.fx_pamt, '0.00')) - ISNULL(c1_s1.fx_amt, '0.00')) +  "
                          + " ((ISNULL(d_s1.tot_xta, '0.00') + ISNULL(d1_s1.xt_pamt, '0.00')) - ISNULL(d1_s1.xt_amt, '0.00')) + ISNULL(e_s1.tun_amt, '0.00') + ISNULL(f_s1.amt1, '0.00') "
                          + " ) - ISNULL(g_s1.samt, '0.00')) between SIP_min_income_amt and SIP_max_income_amt) end) + ISNULL(i.samt, '0.00') +  "
                          + " cast(ISNULL(m1.pcb_amt,'0.00') as money) + cast(ISNULL(m.tax_cp38_amt1,'0.00') as money)) , '0.00') as tot_ded_amt,  "
                          + "  ISNULL(((ISNULL(slr_salary_amt, '0.00') + ((ISNULL(c.samt, '0.00') + ISNULL(c1.fx_pamt, '0.00')) - ISNULL(c1.fx_amt, '0.00')) + "
                          + " ISNULL(j.amt1, '0.00') + ((ISNULL(d.tot_xta, '0.00') + ISNULL(d1.xt_pamt, '0.00')) - ISNULL(d1.xt_amt, '0.00')) +  ISNULL(n.tun_amt, '0.00') +   ISNULL(e.a1, '0.00') + ISNULL(e.a2, '0.00')) "
                          + " - (ISNULL(g.epf_amt, '0.00') +   (case when stf_age < '60' then (select top(1) per_employee_amt from hr_comm_perkeso where  "
                          + " ((slr_salary_amt + ((ISNULL(c_p1.samt, '0.00') + ISNULL(c1_p1.fx_pamt, '0.00')) - ISNULL(c1_p1.fx_amt, '0.00')) +  "
                          + " ((ISNULL(d_p1.tot_xta, '0.00') + ISNULL(d1_p1.xt_pamt, '0.00')) - ISNULL(d1_p1.xt_amt, '0.00')) + ISNULL(e_p1.tun_amt, '0.00') + ISNULL(f_p1.amt1, '0.00') "
                          + "  ) - ISNULL(g_p1.samt, '0.00')) between per_min_income_amt and per_max_income_amt) else  "
                          + " (select top(1) per_employer_amt2 from hr_comm_perkeso where  "
                          + " ((slr_salary_amt + ((ISNULL(c_p1.samt, '0.00') + ISNULL(c1_p1.fx_pamt, '0.00')) - ISNULL(c1_p1.fx_amt, '0.00')) +  "
                          + " ((ISNULL(d_p1.tot_xta, '0.00') + ISNULL(d1_p1.xt_pamt, '0.00')) - ISNULL(d1_p1.xt_amt, '0.00')) + ISNULL(e_p1.tun_amt, '0.00') + ISNULL(f_p1.amt1, '0.00') "
                          + " ) - ISNULL(g_p1.samt, '0.00')) between per_min_income_amt and per_max_income_amt) end) + (case when stf_age < '60' then (select top(1) SIP_employee_amt from hr_comm_SIP where  "
                          + " ((slr_salary_amt + ((ISNULL(c_s1.samt, '0.00') + ISNULL(c1_s1.fx_pamt, '0.00')) - ISNULL(c1_s1.fx_amt, '0.00')) +  "
                          + " ((ISNULL(d_s1.tot_xta, '0.00') + ISNULL(d1_s1.xt_pamt, '0.00')) - ISNULL(d1_s1.xt_amt, '0.00')) + ISNULL(e_s1.tun_amt, '0.00') + ISNULL(f_s1.amt1, '0.00') "
                          + " ) - ISNULL(g_s1.samt, '0.00')) between SIP_min_income_amt and SIP_max_income_amt) else  "
                          + " (select top(1) SIP_employee_amt from hr_comm_SIP where  "
                          + " ((slr_salary_amt + ((ISNULL(c_s1.samt, '0.00') + ISNULL(c1_s1.fx_pamt, '0.00')) - ISNULL(c1_s1.fx_amt, '0.00')) +  "
                          + " ((ISNULL(d_s1.tot_xta, '0.00') + ISNULL(d1_s1.xt_pamt, '0.00')) - ISNULL(d1_s1.xt_amt, '0.00')) + ISNULL(e_s1.tun_amt, '0.00') + ISNULL(f_s1.amt1, '0.00') "
                          + " ) - ISNULL(g_s1.samt, '0.00')) between SIP_min_income_amt and SIP_max_income_amt) end) + ISNULL(i.samt, '0.00') + cast(ISNULL(m1.pcb_amt,'0.00') as money) + cast(ISNULL(m.tax_cp38_amt1,'0.00') as money))),'0.00') as nett_amt,ISNULL(j.amt1, '0.00') as ot_amt,ISNULL(k.epf_emp_kwsp_perc, '') as emp_kwsp_perc,  "
                          + " ISNULL(k.epf_emp_kwsp_amt, '0.00') as kwsp_emp_amt,ISNULL(n.tun_amt, '0.00') as  "
                          + " tunamt from ( "
                          + "   select stf_staff_no,stf_age,stf_cur_sub_org,str_curr_org_cd,org_id,stf_name,stf_icno from hr_staff_profile left join hr_post_his hp  on hp.pos_staff_no =stf_staff_no  "
                          + " left join hr_organization as ho on ho.org_gen_id=str_curr_org_cd where pos_end_dt ='9999-12-31') as a  "
                          + "   left join (select slr_staff_no,slr_salary_amt from  hr_salary where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "') between  "
                          + " FORMAT(slr_eff_dt,'yyyy-MM') And FORMAT(slr_end_dt,'yyyy-MM')) as b on b.slr_staff_no=a.stf_staff_no  "
                          + "   left join (select fx.fxa_staff_no, sum(fx.fxa_allowance_amt) as samt from  hr_fixed_allowance as fx where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(fx.fxa_eff_dt,'yyyy-MM') And  "
                          + " FORMAT(fx.fxa_end_dt,'yyyy-MM')) group by fx.fxa_staff_no) as c on  c.fxa_staff_no=a.stf_staff_no  "
                          + "   left join (select fx.fxa_staff_no, sum(fx.fxa_promo_amt) as fx_pamt,sum(fx.fxa_allowance_amt) as fx_amt from hr_fixed_allowance as fx where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(fx.fxa_pst_dt, "
                          + " 'yyyy-MM') And FORMAT(fx.fxa_ped_dt,'yyyy-MM')) group by fx.fxa_staff_no) as c1 on  c1.fxa_staff_no=a.stf_staff_no  "
                          + "    left join (select fx.fxa_staff_no, sum(fx.fxa_allowance_amt) as samt from  hr_fixed_allowance as fx Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=fx.fxa_allowance_type_cd  "
                          + " and ss1.elau_perkeso='S' where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(fx.fxa_eff_dt,'yyyy-MM') And FORMAT(fx.fxa_end_dt,'yyyy-MM')) group by fx.fxa_staff_no) as c_p1  "
                          + " on  c_p1.fxa_staff_no=a.stf_staff_no  "
                          + "   left join (select fx.fxa_staff_no, sum(fx.fxa_promo_amt) as fx_pamt,sum(fx.fxa_allowance_amt) as fx_amt from hr_fixed_allowance as fx Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=fx.fxa_allowance_type_cd  "
                          + " and ss1.elau_perkeso='S' where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(fx.fxa_pst_dt, 'yyyy-MM') And FORMAT(fx.fxa_ped_dt,'yyyy-MM')) group by fx.fxa_staff_no) as c1_p1 "
                          + " on  c1_p1.fxa_staff_no=a.stf_staff_no  "
                          + " left join (select fx.xta_staff_no,sum(fx.xta_allowance_amt) as tot_xta from hr_extra_allowance as fx Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=fx.xta_allowance_type_cd  "
                          + " and ss1.elau_perkeso='S' where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(fx.xta_eff_dt,'yyyy-MM') And   FORMAT(fx.xta_end_dt,'yyyy-MM')) group by fx.xta_staff_no) as d_p1  "
                          + " on d_p1.xta_staff_no=a.stf_staff_no  "
                          + "    left join (select fx.xta_staff_no,sum(fx.xta_promo_amt) as xt_pamt,sum(fx.xta_allowance_amt) as xt_amt from hr_extra_allowance as fx Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=fx.xta_allowance_type_cd  "
                          + " and ss1.elau_perkeso='S' where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(fx.xta_pst_dt,'yyyy-MM') And  FORMAT(fx.xta_ped_dt,'yyyy-MM')) group by fx.xta_staff_no) as d1_p1  "
                          + " on d1_p1.xta_staff_no=a.stf_staff_no   "
                          + " 	 left join(select SUM(tun_amt) tun_amt,tun_staff_no "
                          + "  from hr_tunggakan left join Ref_hr_tunggakan s1 on s1.hr_tung_Code=tun_type_cd where s1.elau_perkeso ='S' and tun_year='" + txt_tahun.SelectedValue + "' and tun_month='" + DD_bulancaruman.SelectedValue + "' group by tun_staff_no)  e_p1  "
                          + " on e_p1.tun_staff_no=a.stf_staff_no  "
                          + " 	  left join(select  otl_staff_no, sum(otl_ot_amt) as amt1 from hr_ot as ot left join Ref_hr_type_klm s1 on s1.typeklm_cd=otl_ot_type_cd and s1.elau_perkeso ='S'   "
                          + " where FORMAT(otl_work_dt,'yyyy-MM') ='" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' group by otl_staff_no) as f_p1 on f_p1.otl_staff_no=a.stf_staff_no  "
                          + " 	   left join(select fx.ded_staff_no,sum( fx.ded_deduct_amt) as samt from hr_deduction as fx left join Ref_hr_potongan s1 on s1.hr_poto_Code=ded_deduct_type_cd and Status='A' "
                          + " where  s1.elau_perkeso='S' and ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(fx.ded_start_dt,'yyyy-MM') And FORMAT(fx.ded_end_dt,'yyyy-MM')) group by  fx.ded_staff_no) as g_p1 on g_p1.ded_staff_no=a.stf_staff_no  "
                          + "    left join (select fx.fxa_staff_no, sum(fx.fxa_allowance_amt) as samt from  hr_fixed_allowance as fx Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=fx.fxa_allowance_type_cd  "
                          + " and ss1.elau_sip='S' where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(fx.fxa_eff_dt,'yyyy-MM') And FORMAT(fx.fxa_end_dt,'yyyy-MM')) group by fx.fxa_staff_no) as c_s1  "
                          + " on  c_s1.fxa_staff_no=a.stf_staff_no  "
                          + "   left join (select fx.fxa_staff_no, sum(fx.fxa_promo_amt) as fx_pamt,sum(fx.fxa_allowance_amt) as fx_amt from hr_fixed_allowance as fx Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=fx.fxa_allowance_type_cd  "
                          + " and ss1.elau_sip='S' where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(fx.fxa_pst_dt, 'yyyy-MM') And FORMAT(fx.fxa_ped_dt,'yyyy-MM')) group by fx.fxa_staff_no) as c1_s1 "
                          + " on  c1_s1.fxa_staff_no=a.stf_staff_no  "
                          + " 	left join (select fx.xta_staff_no,sum(fx.xta_allowance_amt) as tot_xta from hr_extra_allowance as fx Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=fx.xta_allowance_type_cd  "
                          + " and ss1.elau_sip='S' where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(fx.xta_eff_dt,'yyyy-MM') And   FORMAT(fx.xta_end_dt,'yyyy-MM')) group by fx.xta_staff_no) as d_s1  "
                          + " on d_s1.xta_staff_no=a.stf_staff_no  "
                          + "    left join (select fx.xta_staff_no,sum(fx.xta_promo_amt) as xt_pamt,sum(fx.xta_allowance_amt) as xt_amt from hr_extra_allowance as fx Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=fx.xta_allowance_type_cd  "
                          + " and ss1.elau_sip='S' where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(fx.xta_pst_dt,'yyyy-MM') And  FORMAT(fx.xta_ped_dt,'yyyy-MM')) group by fx.xta_staff_no) as d1_s1  "
                          + " on d1_s1.xta_staff_no=a.stf_staff_no   "
                          + " 	 left join(select SUM(tun_amt) tun_amt,tun_staff_no "
                          + " from hr_tunggakan left join Ref_hr_tunggakan s1 on s1.hr_tung_Code=tun_type_cd  where s1.elau_sip ='S' and tun_year='" + txt_tahun.SelectedValue + "' and tun_month='" + DD_bulancaruman.SelectedValue + "' group by tun_staff_no)  e_s1  "
                          + " on e_s1.tun_staff_no=a.stf_staff_no  "
                          + " 	  left join(select  otl_staff_no, sum(otl_ot_amt) as amt1 from hr_ot as ot left join Ref_hr_type_klm s1 on s1.typeklm_cd=otl_ot_type_cd and s1.elau_sip ='S'   "
                          + " where FORMAT(otl_work_dt,'yyyy-MM') ='" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' group by otl_staff_no) as f_s1 on f_s1.otl_staff_no=a.stf_staff_no  "
                          + " 	   left join(select fx.ded_staff_no,sum( fx.ded_deduct_amt) as samt from hr_deduction as fx left join Ref_hr_potongan s1 on s1.hr_poto_Code=ded_deduct_type_cd and Status='A' "
                          + " where s1.elau_sip='S' and ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(fx.ded_start_dt,'yyyy-MM') And FORMAT(fx.ded_end_dt,'yyyy-MM')) group by  fx.ded_staff_no) as g_s1 on g_s1.ded_staff_no=a.stf_staff_no  "
                          + "   left join (select fx.xta_staff_no,sum(fx.xta_allowance_amt) as tot_xta from hr_extra_allowance as fx where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(fx.xta_eff_dt,'yyyy-MM') And   "
                          + " FORMAT(fx.xta_end_dt,'yyyy-MM')) group by fx.xta_staff_no) as d on d.xta_staff_no=a.stf_staff_no "
                          + "    left join (select fx.xta_staff_no,sum(fx.xta_promo_amt) as xt_pamt,sum(fx.xta_allowance_amt) as xt_amt from hr_extra_allowance as fx where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between  "
                          + " FORMAT(fx.xta_pst_dt,'yyyy-MM') And  FORMAT(fx.xta_ped_dt,'yyyy-MM')) group by fx.xta_staff_no) as d1 on d1.xta_staff_no=a.stf_staff_no   "
                          + "   left join (select bns_staff_no,sum(bns_amt) as a1,sum(bns_kpi_amt) as a2 from hr_Bonus where  (('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "') between FORMAT(bns_eff_dt,'yyyy-MM') And FORMAT(bns_end_dt,'yyyy-MM'))  "
                          + " group by bns_staff_no) as e on e.bns_staff_no= a.stf_staff_no  "
                          + "    left join(select fx.ded_staff_no,sum( fx.ded_deduct_amt) as samt from hr_deduction as fx where ded_deduct_type_cd='09' and ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(fx.ded_start_dt, "
                          + " 'yyyy-MM') And FORMAT(fx.ded_end_dt,'yyyy-MM')) group by  fx.ded_staff_no) as f on f.ded_staff_no=a.stf_staff_no  "
                          + "    left join (select epf_staff_no,SUM(epf_amt) epf_amt from hr_kwsp hk where  '" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(hk.epf_eff_dt,'yyyy-MM') And FORMAT (hk.epf_end_dt,'yyyy-MM') group by epf_staff_no)  "
                          + " as g on g.epf_staff_no=a.stf_staff_no  "
                          + "    left join(select * from hr_comm_perkeso) h on (ISNULL(slr_salary_amt,'0.00') + ISNULL(c.samt,'0.00') + ISNULL(d.tot_xta,'0.00') +  ISNULL(e.a1,'0.00') + ISNULL(e.a2,'0.00')) "
                          + " between h.per_min_income_amt AND h.per_max_income_amt  "
                          + " left join (select fx.ded_staff_no,sum(fx.ded_deduct_amt) as samt from hr_deduction as fx inner join Ref_hr_potongan as PO on PO.hr_poto_Code=fx.ded_deduct_type_cd where  ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(fx.ded_start_dt,'yyyy-MM') And  "
                          + " FORMAT(fx.ded_end_dt,'yyyy-MM'))  group by fx.ded_staff_no) i on i.ded_staff_no=a.stf_staff_no  "
                          + " 	left join(select  otl_staff_no, sum(otl_ot_amt) as amt1 from hr_ot as ot  where FORMAT(otl_work_dt,'yyyy-MM') ='" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' group by otl_staff_no) as j on j.otl_staff_no=a.stf_staff_no  "
                          + " 	left join(select epf_emp_kwsp_perc, sum(epf_emp_kwsp_amt) as epf_emp_kwsp_amt,epf_staff_no from hr_kwsp where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(epf_eff_dt,'yyyy-MM') And  "
                          + " FORMAT(epf_end_dt,'yyyy-MM')) group by epf_staff_no,epf_emp_kwsp_perc)as k on k.epf_staff_no= a.stf_staff_no  "
                          + " 	left join( select tax_sip_staff_no,SUM(tax_sip_amt1) tax_sip_amt1,SUM(tax_sip_amt2) tax_sip_amt2,sip_chk from hr_income_tax_sip where (('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "') between  "
                          + " FORMAT(tax_sip_start_dt1,'yyyy-MM') And FORMAT(tax_sip_end_dt1,'yyyy-MM')) group by tax_sip_staff_no,sip_chk) l on l.tax_sip_staff_no=a.stf_staff_no  "
                          + " left join(select SUM(tax_cp38_amt1) tax_cp38_amt1,tax_staff_no from hr_income_tax where tax_type ='2' and (('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "') between   "
                          + " FORMAT(tax_cp38_start_dt1,'yyyy-MM') And FORMAT(tax_cp38_end_dt1 ,'yyyy-MM')) group by tax_staff_no)  m on m.tax_staff_no=a.stf_staff_no  "
                          + " left join(select SUM(tax_pcb_amt) pcb_amt,tax_staff_no from hr_income_tax where tax_type ='1' and (('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "') between  "
                          + " FORMAT(tax_pcb_start_dt,'yyyy-MM') And FORMAT(tax_pcb_end_dt,'yyyy-MM')) group by tax_staff_no)  m1 on m1.tax_staff_no=a.stf_staff_no  "
                          + " 	 left join(select SUM(tun_amt) tun_amt,tun_staff_no "
                          + " from hr_tunggakan where tun_year='" + txt_tahun.SelectedValue + "' and tun_month='" + DD_bulancaruman.SelectedValue + "' group by tun_staff_no)  n on n.tun_staff_no=a.stf_staff_no  " + sqry + "";

    }
    //protected void grid()
    //{
    //    get_stf_details();
    //    SqlConnection con = new SqlConnection(conString);
    //    con.Open();

    //    //SqlCommand cmd = new SqlCommand("select stf_staff_no,stf_name,stf_icno,'" + DD_bulancaruman.SelectedValue + "' as mnth,'" + txt_tahun.SelectedValue + "' as yer,org_id,ISNULL(slr_salary_amt,'0.00') as salary,stf_cur_sub_org,str_curr_org_cd,ISNULL(c.samt,'0.00') as fix_alwncce,ISNULL(d.tot_xta,'0.00') as xta_alwnce,ISNULL(i.samt,'0.00') as ded_amt,ISNULL(e.a1,'0.00') as bns_amt,ISNULL(e.a2,'0.00') as kpi_bns_amt,ISNULL((ISNULL(slr_salary_amt,'0.00') + ISNULL(c.samt,'0.00') + ISNULL(d.tot_xta,'0.00') + ISNULL(e.a1,'0.00') + ISNULL(e.a2,'0.00')),'0.00') as gross_amt,isnull(f.samt, '0.00') as ctg_amt,ISNULL(g.epf_amt,'0.00') as kwsp_amt,case when m.perkeso_chk = '1' then ISNULL(m.tax_cp38_amt1,'0.00') else '0.00' end as  perkeso_amt,'0.00' as sip_amt,'0.00' as pcb_amt,'0.00' as cp38_amt,(ISNULL(g.epf_amt,'0.00') +  case when m.perkeso_chk = '1' then ISNULL(m.tax_cp38_amt1,'0.00') else '0.00' end + case when l.sip_chk = '1' then ISNULL(l.tax_sip_amt1,'0.00') else '0.00' end + ISNULL(i.samt,'0.00')) as tot_ded_amt,ISNULL(((ISNULL(slr_salary_amt,'0.00') + ISNULL(c.samt,'0.00') + ISNULL(d.tot_xta,'0.00') + ISNULL(e.a1,'0.00') + ISNULL(e.a2,'0.00')) - (ISNULL(g.epf_amt,'0.00') + ISNULL(i.samt,'0.00') + case when l.sip_chk = '1' then ISNULL(l.tax_sip_amt1,'0.00') else '0.00' end + case when m.perkeso_chk = '1' then ISNULL(m.tax_cp38_amt1,'0.00') else '0.00' end )),'0.00') as nett_amt,ISNULL(j.amt1,'0.00') as ot_amt,ISNULL(k.epf_emp_kwsp_perc,'') as  emp_kwsp_perc, case when m.perkeso_chk = '1' then ISNULL(m.tax_cp38_amt2,'0.00') else '0.00' end as  emp_perkeso_amt,case when l.sip_chk = '1' then ISNULL(l.tax_sip_amt1,'0.00') else '0.00' end as sip_amt1, case when l.sip_chk = '1' then ISNULL(l.tax_sip_amt2,'0.00') else '0.00' end as sip_emp_amt1,ISNULL(k.epf_emp_kwsp_amt,'0.00') as kwsp_emp_amt from (select stf_staff_no,stf_age,stf_cur_sub_org,str_curr_org_cd,org_id,stf_name,stf_icno from hr_staff_profile left join hr_post_his hp  on hp.pos_staff_no =stf_staff_no left join hr_organization as ho on ho.org_gen_id=str_curr_org_cd where pos_end_dt ='9999-12-31') as a left join (select slr_staff_no,slr_salary_amt from  hr_salary where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "') between FORMAT(slr_eff_dt,'yyyy-MM') And FORMAT(slr_end_dt,'yyyy-MM')) as b on b.slr_staff_no=a.stf_staff_no left join (select fx.fxa_staff_no, sum(fx.fxa_allowance_amt) as samt from hr_fixed_allowance as fx where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(fx.fxa_eff_dt,'yyyy-MM') And FORMAT(fx.fxa_end_dt,'yyyy-MM')) group by fx.fxa_staff_no) as c on  c.fxa_staff_no=a.stf_staff_no left join (select fx.xta_staff_no,sum(fx.xta_allowance_amt) as tot_xta from hr_extra_allowance as fx where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(fx.xta_eff_dt,'yyyy-MM') And  FORMAT(fx.xta_end_dt,'yyyy-MM')) group by fx.xta_staff_no) as d on d.xta_staff_no=a.stf_staff_no left join (select bns_staff_no,sum(bns_amt) as a1,sum(bns_kpi_amt) as a2 from hr_Bonus where  (('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "') between FORMAT(bns_eff_dt,'yyyy-MM') And FORMAT(bns_end_dt,'yyyy-MM')) group by bns_staff_no) as e on e.bns_staff_no=a.stf_staff_no left join(select fx.ded_staff_no,sum( fx.ded_deduct_amt) as samt from hr_deduction as fx where ded_deduct_type_cd='09' and ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(fx.ded_start_dt,'yyyy-MM') And FORMAT(fx.ded_end_dt,'yyyy-MM')) group by  fx.ded_staff_no) as f on f.ded_staff_no=a.stf_staff_no left join (select epf_staff_no,SUM(epf_amt) epf_amt from hr_kwsp hk where '" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(hk.epf_eff_dt,'yyyy-MM') And FORMAT (hk.epf_end_dt,'yyyy-MM') group by epf_staff_no) as g on g.epf_staff_no=a.stf_staff_no left join(select * from hr_comm_perkeso ) h on (ISNULL(slr_salary_amt,'0.00') + ISNULL(c.samt,'0.00') + ISNULL(d.tot_xta,'0.00') +  ISNULL(e.a1,'0.00') + ISNULL(e.a2,'0.00')) between h.per_min_income_amt AND h.per_max_income_amt left join (select fx.ded_staff_no,sum(fx.ded_deduct_amt) as samt from hr_deduction as fx where  ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(fx.ded_start_dt,'yyyy-MM') And FORMAT(fx.ded_end_dt,'yyyy-MM'))  group by fx.ded_staff_no) i on i.ded_staff_no=a.stf_staff_no left join(select  otl_staff_no, sum(otl_ot_amt) as amt1 from hr_ot as ot  where otl_month='" + DD_bulancaruman.SelectedValue + "' and otl_year ='" + txt_tahun.SelectedValue + "' group by otl_staff_no) as j on j.otl_staff_no=a.stf_staff_no left join(select epf_emp_kwsp_perc, sum(epf_emp_kwsp_amt) as epf_emp_kwsp_amt,epf_staff_no from hr_kwsp where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(epf_eff_dt,'yyyy-MM') And FORMAT(epf_end_dt,'yyyy-MM')) group by epf_staff_no,epf_emp_kwsp_perc)as k on k.epf_staff_no=a.stf_staff_no left join( select tax_sip_staff_no,SUM(tax_sip_amt1) tax_sip_amt1,SUM(tax_sip_amt2) tax_sip_amt2,sip_chk from hr_income_tax_sip where (('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "') between FORMAT(tax_sip_start_dt1,'yyyy-MM') And FORMAT(tax_sip_end_dt1,'yyyy-MM')) group by tax_sip_staff_no,sip_chk) l on l.tax_sip_staff_no=a.stf_staff_no left join(select SUM(tax_cp38_amt1) tax_cp38_amt1,SUM(tax_cp38_amt2) as tax_cp38_amt2,perkeso_chk,tax_staff_no from hr_income_tax where (('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "') between  FORMAT(tax_cp38_start_dt1,'yyyy-MM') And FORMAT(tax_cp38_end_dt1,'yyyy-MM')) group by perkeso_chk,tax_staff_no)  m on m.tax_staff_no=a.stf_staff_no " + sqry + "", con); //latest

    //    SqlCommand cmd = new SqlCommand("select stf_staff_no,stf_name,stf_icno,'" + DD_bulancaruman.SelectedValue + "' as mnth,'" + txt_tahun.SelectedValue + "' as yer,org_id,ISNULL(slr_salary_amt,'0.00') as salary,stf_cur_sub_org,str_curr_org_cd,ISNULL(c.samt,'0.00') as fix_alwncce,ISNULL(d.tot_xta,'0.00') as xta_alwnce,ISNULL(i.samt,'0.00') as ded_amt,ISNULL(e.a1,'0.00') as bns_amt,ISNULL(e.a2,'0.00') as kpi_bns_amt,ISNULL((ISNULL(slr_salary_amt,'0.00') + ISNULL(c.samt,'0.00') + ISNULL(d.tot_xta,'0.00') + ISNULL(e.a1,'0.00') + ISNULL(e.a2,'0.00') + ISNULL(n.tun_amt,'0.00') ),'0.00') as gross_amt,isnull(f.samt, '0.00') as ctg_amt,ISNULL(g.epf_amt,'0.00') as kwsp_amt,case when m.perkeso_chk = '1' then ISNULL(m.tax_cp38_amt1,'0.00') else '0.00' end as  perkeso_amt,'0.00' as sip_amt,'0.00' as pcb_amt,'0.00' as cp38_amt,(ISNULL(g.epf_amt,'0.00') +  case when m.perkeso_chk = '1' then ISNULL(m.tax_cp38_amt1,'0.00') else '0.00' end + case when l.sip_chk = '1' then ISNULL(l.tax_sip_amt1,'0.00') else '0.00' end + ISNULL(i.samt,'0.00')) as tot_ded_amt,ISNULL(((ISNULL(slr_salary_amt,'0.00') + ISNULL(c.samt,'0.00') + ISNULL(d.tot_xta,'0.00') + ISNULL(n.tun_amt,'0.00') + ISNULL(e.a1,'0.00') + ISNULL(e.a2,'0.00')) - (ISNULL(g.epf_amt,'0.00') + ISNULL(i.samt,'0.00') + case when l.sip_chk = '1' then ISNULL(l.tax_sip_amt1,'0.00') else '0.00' end + case when m.perkeso_chk = '1' then ISNULL(m.tax_cp38_amt1,'0.00') else '0.00' end )),'0.00') as nett_amt,ISNULL(j.amt1,'0.00') as ot_amt,ISNULL(k.epf_emp_kwsp_perc,'') as  emp_kwsp_perc, case when m.perkeso_chk = '1' then ISNULL(m.tax_cp38_amt2,'0.00') else '0.00' end as  emp_perkeso_amt,case when l.sip_chk = '1' then ISNULL(l.tax_sip_amt1,'0.00') else '0.00' end as sip_amt1, case when l.sip_chk = '1' then ISNULL(l.tax_sip_amt2,'0.00') else '0.00' end as sip_emp_amt1,ISNULL(k.epf_emp_kwsp_amt,'0.00') as kwsp_emp_amt,ISNULL(n.tun_amt,'0.00') as tunamt from (select stf_staff_no,stf_age,stf_cur_sub_org,str_curr_org_cd,org_id,stf_name,stf_icno from hr_staff_profile left join hr_post_his hp  on hp.pos_staff_no =stf_staff_no left join hr_organization as ho on ho.org_gen_id=str_curr_org_cd where pos_end_dt ='9999-12-31') as a left join (select slr_staff_no,slr_salary_amt from  hr_salary where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "') between FORMAT(slr_eff_dt,'yyyy-MM') And FORMAT(slr_end_dt,'yyyy-MM')) as b on b.slr_staff_no=a.stf_staff_no left join (select fx.fxa_staff_no, sum(fx.fxa_allowance_amt) as samt from hr_fixed_allowance as fx where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(fx.fxa_eff_dt,'yyyy-MM') And FORMAT(fx.fxa_end_dt,'yyyy-MM')) group by fx.fxa_staff_no) as c on  c.fxa_staff_no=a.stf_staff_no left join (select fx.xta_staff_no,sum(fx.xta_allowance_amt) as tot_xta from hr_extra_allowance as fx where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(fx.xta_eff_dt,'yyyy-MM') And  FORMAT(fx.xta_end_dt,'yyyy-MM')) group by fx.xta_staff_no) as d on d.xta_staff_no=a.stf_staff_no left join (select bns_staff_no,sum(bns_amt) as a1,sum(bns_kpi_amt) as a2 from hr_Bonus where  (('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "') between FORMAT(bns_eff_dt,'yyyy-MM') And FORMAT(bns_end_dt,'yyyy-MM')) group by bns_staff_no) as e on e.bns_staff_no=a.stf_staff_no left join(select fx.ded_staff_no,sum( fx.ded_deduct_amt) as samt from hr_deduction as fx where ded_deduct_type_cd='09' and ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(fx.ded_start_dt,'yyyy-MM') And FORMAT(fx.ded_end_dt,'yyyy-MM')) group by  fx.ded_staff_no) as f on f.ded_staff_no=a.stf_staff_no left join (select epf_staff_no,SUM(epf_amt) epf_amt from hr_kwsp hk where '" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(hk.epf_eff_dt,'yyyy-MM') And FORMAT (hk.epf_end_dt,'yyyy-MM') group by epf_staff_no) as g on g.epf_staff_no=a.stf_staff_no left join(select * from hr_comm_perkeso ) h on (ISNULL(slr_salary_amt,'0.00') + ISNULL(c.samt,'0.00') + ISNULL(d.tot_xta,'0.00') +  ISNULL(e.a1,'0.00') + ISNULL(e.a2,'0.00')) between h.per_min_income_amt AND h.per_max_income_amt left join (select fx.ded_staff_no,sum(fx.ded_deduct_amt) as samt from hr_deduction as fx where  ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(fx.ded_start_dt,'yyyy-MM') And FORMAT(fx.ded_end_dt,'yyyy-MM'))  group by fx.ded_staff_no) i on i.ded_staff_no=a.stf_staff_no left join(select  otl_staff_no, sum(otl_ot_amt) as amt1 from hr_ot as ot  where otl_month='" + DD_bulancaruman.SelectedValue + "' and otl_year ='" + txt_tahun.SelectedValue + "' group by otl_staff_no) as j on j.otl_staff_no=a.stf_staff_no left join(select epf_emp_kwsp_perc, sum(epf_emp_kwsp_amt) as epf_emp_kwsp_amt,epf_staff_no from hr_kwsp where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(epf_eff_dt,'yyyy-MM') And FORMAT(epf_end_dt,'yyyy-MM')) group by epf_staff_no,epf_emp_kwsp_perc)as k on k.epf_staff_no=a.stf_staff_no left join( select tax_sip_staff_no,SUM(tax_sip_amt1) tax_sip_amt1,SUM(tax_sip_amt2) tax_sip_amt2,sip_chk from hr_income_tax_sip where (('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "') between FORMAT(tax_sip_start_dt1,'yyyy-MM') And FORMAT(tax_sip_end_dt1,'yyyy-MM')) group by tax_sip_staff_no,sip_chk) l on l.tax_sip_staff_no=a.stf_staff_no left join(select SUM(tax_cp38_amt1) tax_cp38_amt1,SUM(tax_cp38_amt2) as tax_cp38_amt2,perkeso_chk,tax_staff_no from hr_income_tax where (('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "') between  FORMAT(tax_cp38_start_dt1,'yyyy-MM') And FORMAT(tax_cp38_end_dt1,'yyyy-MM')) group by perkeso_chk,tax_staff_no)  m on m.tax_staff_no=a.stf_staff_no left join(select SUM(tun_amt) tun_amt,tun_staff_no from hr_tunggakan where tun_year='"+ txt_tahun.SelectedValue + "' and tun_month='" + DD_bulancaruman.SelectedValue + "' group by tun_staff_no)  n on n.tun_staff_no=a.stf_staff_no " + sqry + "", con);

    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count == 0)
    //    {

    //        ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
    //        gvSelected.DataSource = ds;
    //        gvSelected.DataBind();
    //        int columncount = gvSelected.Rows[0].Cells.Count;
    //        gvSelected.Rows[0].Cells.Clear();
    //        gvSelected.Rows[0].Cells.Add(new TableCell());
    //        gvSelected.Rows[0].Cells[0].ColumnSpan = columncount;
    //        gvSelected.Rows[0].Cells[0].Text = "<center><strong>Rekod Tidak Dijumpai. Sila Lakukan Semula Carian</strong></center>";
    //        Button4.Visible = false;
    //    }
    //    else
    //    {
    //        using (SqlDataAdapter sda = new SqlDataAdapter())
    //        {
    //            cmd.Connection = con;
    //            sda.SelectCommand = cmd;
    //            using (DataTable dt = new DataTable())
    //            {
    //                gvSelected.DataSource = dt;
    //                gvSelected.DataBind();

    //                decimal salary = dt.AsEnumerable().Sum(row => row.Field<decimal>("salary"));
    //                decimal ext_allow = dt.AsEnumerable().Sum(row => row.Field<decimal>("fix_alwnce"));
    //                decimal ot_allow = dt.AsEnumerable().Sum(row => row.Field<decimal>("xta_alwnce"));
    //                decimal bonus_thn = (dt.AsEnumerable().Sum(row => row.Field<decimal>("bns_amt")));
    //                decimal bonus_kpi = (dt.AsEnumerable().Sum(row => row.Field<decimal>("kpi_bns_amt")));
    //                decimal ot_klm = dt.AsEnumerable().Sum(row => row.Field<decimal>("ot_amt"));
    //                decimal jp = dt.AsEnumerable().Sum(row => row.Field<decimal>("tot_ded_amt"));
    //                decimal car_kwsp = (dt.AsEnumerable().Sum(row => row.Field<decimal>("kwsp_amt")));
    //                decimal car_kwsp_emp = (dt.AsEnumerable().Sum(row => row.Field<decimal>("kwsp_emp_amt")));
    //                decimal car_perkeso = (dt.AsEnumerable().Sum(row => row.Field<decimal>("perkeso_amt")));
    //                decimal car_perkeso_emp = (dt.AsEnumerable().Sum(row => row.Field<decimal>("emp_perkeso_amt")));
    //                decimal sip = (dt.AsEnumerable().Sum(row => row.Field<decimal>("SIP_amt1")));
    //                decimal sip_emp = (dt.AsEnumerable().Sum(row => row.Field<decimal>("SIP_emp_amt1")));
    //                decimal pcb = dt.AsEnumerable().Sum(row => row.Field<decimal>("pcb_amt"));
    //                decimal cp_38 = dt.AsEnumerable().Sum(row => row.Field<decimal>("cp38_amt"));
    //                decimal pkrm = dt.AsEnumerable().Sum(row => row.Field<decimal>("gross_amt"));
    //                decimal pbrm = dt.AsEnumerable().Sum(row => row.Field<decimal>("nett_amt"));
    //                decimal ded_amt = dt.AsEnumerable().Sum(row => row.Field<decimal>("ded_amt"));

    //                Label ft_01 = (Label)gvSelected.FooterRow.FindControl("ftr_001");
    //                Label ft_02 = (Label)gvSelected.FooterRow.FindControl("ftr_002");
    //                Label ft_03 = (Label)gvSelected.FooterRow.FindControl("ftr_003");
    //                Label ft_04 = (Label)gvSelected.FooterRow.FindControl("ftr_004");
    //                Label ft_05 = (Label)gvSelected.FooterRow.FindControl("ftr_005");
    //                Label ft_06 = (Label)gvSelected.FooterRow.FindControl("ftr_006");
    //                Label ft_07 = (Label)gvSelected.FooterRow.FindControl("ftr_007");
    //                Label ft_08 = (Label)gvSelected.FooterRow.FindControl("ftr_008");
    //                Label ft_09 = (Label)gvSelected.FooterRow.FindControl("ftr_009");
    //                Label ft_10 = (Label)gvSelected.FooterRow.FindControl("ftr_010");
    //                Label ft_11 = (Label)gvSelected.FooterRow.FindControl("ftr_011");
    //                Label ft_12 = (Label)gvSelected.FooterRow.FindControl("ftr_012");
    //                Label ft_13 = (Label)gvSelected.FooterRow.FindControl("ftr_013");
    //                Label ft_14 = (Label)gvSelected.FooterRow.FindControl("ftr_014");
    //                Label ft_15 = (Label)gvSelected.FooterRow.FindControl("ftr_015");
    //                Label ft_16 = (Label)gvSelected.FooterRow.FindControl("ftr_016");

    //                gvSelected.FooterRow.Cells[2].Text = "<strong>KESELURUHAN (RM)</strong>";
    //                gvSelected.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;

    //                ft_01.Text = salary.ToString("C").Replace("$", "");
    //                ft_02.Text = ext_allow.ToString("C").Replace("$", "");
    //                ft_03.Text = ot_allow.ToString("C").Replace("$", "");
    //                ft_04.Text = bonus_thn.ToString("C").Replace("$", "");
    //                ft_05.Text = ot_klm.ToString("C").Replace("$", "");
    //                ft_06.Text = jp.ToString("C").Replace("$", "");
    //                ft_07.Text = car_kwsp.ToString("C").Replace("$", "");
    //                ft_08.Text = car_perkeso.ToString("C").Replace("$", "");
    //                ft_09.Text = sip.ToString("C").Replace("$", "");                    
    //                ft_10.Text = pcb.ToString("C").Replace("$", "");
    //                ft_11.Text = cp_38.ToString("C").Replace("$", "");
    //                ft_12.Text = pkrm.ToString("C").Replace("$", "");
    //                ft_13.Text = pbrm.ToString("C").Replace("$", "");
    //                ft_14.Text = ded_amt.ToString("C").Replace("$", "");
    //                ft_15.Text = ded_amt.ToString("C").Replace("$", "");
    //                ft_16.Text = ded_amt.ToString("C").Replace("$", "");
    //            }
    //        }


    //    }
    //    con.Close();
    //}

    protected void GridView1_DataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            var val2 = e.Row.FindControl("Label3") as Label;
            var val3 = e.Row.FindControl("Label4_mnth") as Label;
            var val4 = e.Row.FindControl("Label1_org_id") as Label;
            var val27 = e.Row.FindControl("org_id") as Label;
            var chk1 = e.Row.FindControl("chkSelect") as CheckBox;

            DataTable chk_income = new DataTable();
            chk_income = DBCon.Ora_Execute_table("select inc_staff_no from hr_income where inc_staff_no='" + val2.Text + "' and inc_month='" + val3.Text + "' and inc_year='" + val4.Text + "' and inc_org_id='" + val27.Text + "' and ISNULL(inc_app_sts,'') != 'A'");
            if (chk_income.Rows.Count != 0)
            {
                chk1.Attributes.Add("style","pointer-events:none;");
            }
            else
            {
                chk1.Attributes.Remove("style");
            }
            
        }

        //if (e.Row.RowType == DataControlRowType.Footer)

        //{


        //    Label ft_01 = (Label)e.Row.FindControl("ftr_001");
        //    Label ft_02 = (Label)e.Row.FindControl("ftr_002");
        //    Label ft_03 = (Label)e.Row.FindControl("ftr_003");
        //    Label ft_04 = (Label)e.Row.FindControl("ftr_004");
        //    Label ft_05 = (Label)e.Row.FindControl("ftr_005");
        //    Label ft_06 = (Label)e.Row.FindControl("ftr_006");
        //    Label ft_06_1 = (Label)e.Row.FindControl("ftr_006_1");
        //    Label ft_07 = (Label)e.Row.FindControl("ftr_007");
        //    Label ft_08 = (Label)e.Row.FindControl("ftr_008");
        //    Label ft_09 = (Label)e.Row.FindControl("ftr_009");
        //    Label ft_10 = (Label)e.Row.FindControl("ftr_010");
        //    Label ft_11 = (Label)e.Row.FindControl("ftr_011");
        //    Label ft_12 = (Label)e.Row.FindControl("ftr_012");
        //    Label ft_13 = (Label)e.Row.FindControl("ftr_013");
        //    Label ft_14 = (Label)e.Row.FindControl("ftr_014");
        //    Label ft_15 = (Label)e.Row.FindControl("ftr_015");
        //    Label ft_16 = (Label)e.Row.FindControl("ftr_016");
        //    Label ft_17 = (Label)e.Row.FindControl("ftr_017");
        //    Label ft_18 = (Label)e.Row.FindControl("ftr_017_1");

        //    gvSelected.FooterRow.Cells[2].Text = "<strong>KESELURUHAN (RM)</strong>";
        //    gvSelected.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;

        //    ft_01.Text = salary.ToString("C").Replace("$", "");
        //    ft_02.Text = ext_allow.ToString("C").Replace("$", "");
        //    ft_03.Text = ot_allow.ToString("C").Replace("$", "");
        //    ft_04.Text = bonus_thn.ToString("C").Replace("$", "");
        //    ft_05.Text = bonus_kpi.ToString("C").Replace("$", "");
        //    ft_06.Text = ot_klm.ToString("C").Replace("$", "");
        //    ft_06_1.Text = tun_amt.ToString("C").Replace("$", "");
        //    ft_07.Text = jp.ToString("C").Replace("$", "");
        //    ft_08.Text = car_kwsp.ToString("C").Replace("$", "");
        //    ft_09.Text = car_kwsp_emp.ToString("C").Replace("$", "");
        //    ft_10.Text = car_perkeso.ToString("C").Replace("$", "");
        //    ft_11.Text = car_perkeso_emp.ToString("C").Replace("$", "");
        //    ft_12.Text = sip.ToString("C").Replace("$", "");
        //    ft_13.Text = sip_emp.ToString("C").Replace("$", "");
        //    ft_14.Text = ded_amt.ToString("C").Replace("$", "");
        //    ft_15.Text = pkrm.ToString("C").Replace("$", "");
        //    ft_16.Text = pbrm.ToString("C").Replace("$", "");
        //    ft_17.Text = pcb.ToString("C").Replace("$", "");
        //    ft_18.Text = cp_38.ToString("C").Replace("$", "");

        //}
    }

  

    //protected void GridView1_OnRowCreated(object sender, GridViewRowEventArgs e)
    //{
    //    string vv1 = string.Empty;

    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        DataRowView drv = (DataRowView)e.Row.DataItem;
    //        if ((DataRowView)e.Row.DataItem != null)
    //        {
    //            for (int i = 0; i < gvSelected.Columns.Count; i++)
    //            {

    //                if (i == 7)
    //                {
    //                    if (drv[i + 2].ToString() != "")
    //                    {
    //                        vv1 = double.Parse(drv[i + 2].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
    //                    }
    //                    else
    //                    {
    //                        vv1 = "1";
    //                    }
    //                }
    //                else if (i == 8)
    //                {
    //                    if (drv[i + 2].ToString() != "")
    //                    {
    //                        vv1 = double.Parse(drv[i + 2].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
    //                    }
    //                    else
    //                    {
    //                        vv1 = "1";
    //                    }
    //                }
    //                else if (i == 9)
    //                {
    //                    if (drv[i + 3].ToString() != "")
    //                    {
    //                        vv1 = double.Parse(drv[i + 3].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
    //                    }
    //                    else
    //                    {
    //                        vv1 = "1";
    //                    }
    //                }
    //                else if (i == 10)
    //                {
    //                    if (drv[i + 3].ToString() != "")
    //                    {
    //                        vv1 = double.Parse(drv[i + 3].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
    //                    }
    //                    else
    //                    {
    //                        vv1 = "1";
    //                    }
    //                }
    //                else if (i == 11)
    //                {
    //                    if (drv[i + 12].ToString() != "")
    //                    {
    //                        vv1 = double.Parse(drv[i + 12].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
    //                    }
    //                    else
    //                    {
    //                        vv1 = "1";
    //                    }
    //                }
    //                else
    //                {
    //                    vv1 = "1";
    //                }

    //                    if (vv1 == "0.00" || vv1 == "0.0000" || vv1 == "0")
    //                    {
    //                        notNulls[i] = false;

    //                    }
    //                    else
    //                    {
    //                        notNulls[i] = true;
    //                    }

    //                }

    //        }
    //    }

    //}
    protected void grid()
    {
        get_stf_details();
        //string query = "select stf_staff_no,stf_name,stf_icno,'"+ DD_bulancaruman.SelectedValue + "' as mnth,'"+ txt_tahun.SelectedValue + "' as yer,org_id,ISNULL(slr_salary_amt,'0.00') as salary,stf_cur_sub_org,str_curr_org_cd,((ISNULL(c.samt,'0.00') + ISNULL(c1.fx_pamt,'0.00')) - ISNULL(c1.fx_amt,'0.00')) as fix_alwncce,((ISNULL(d.tot_xta,'0.00') + ISNULL(d1.xt_pamt,'0.00')) - ISNULL(d1.xt_amt,'0.00')) as xta_alwnce,ISNULL(i.samt, '0.00') as ded_amt,ISNULL(e.a1, '0.00') as bns_amt,ISNULL(e.a2, '0.00') as kpi_bns_amt, ISNULL((ISNULL(slr_salary_amt, '0.00') + ((ISNULL(c.samt, '0.00') + ISNULL(c1.fx_pamt, '0.00')) - ISNULL(c1.fx_amt, '0.00')) + ((ISNULL(d.tot_xta, '0.00') + ISNULL(d1.xt_pamt, '0.00')) - ISNULL(d1.xt_amt, '0.00')) +  ISNULL(e.a1, '0.00') +   ISNULL(e.a2, '0.00') + ISNULL(n.tun_amt, '0.00') + ISNULL(j.amt1, '0.00')), '0.00') as gross_amt,isnull(f.samt, '0.00') as ctg_amt,ISNULL(g.epf_amt, '0.00') as kwsp_amt,case when m.perkeso_chk = '1' then  ISNULL(m.tax_cp38_amt1, '0.00') else '0.00' end as       perkeso_amt,'0.00' as sip_amt,cast(ISNULL(m1.pcb_amt,'0.00') as money) as pcb_amt,cast('0.00' as money) as cp38_amt,(ISNULL(g.epf_amt, '0.00') +   case when m.perkeso_chk = '1' then ISNULL(m.tax_cp38_amt1, '0.00') else '0.00' end + case when l.sip_chk = '1' then ISNULL(l.tax_sip_amt1, '0.00') else '0.00' end + ISNULL(i.samt, '0.00') + cast(ISNULL(m1.pcb_amt,'0.00') as money)) as tot_ded_amt, ISNULL(((ISNULL(slr_salary_amt, '0.00') + ((ISNULL(c.samt, '0.00') + ISNULL(c1.fx_pamt, '0.00')) - ISNULL(c1.fx_amt, '0.00')) +ISNULL(j.amt1, '0.00') + ((ISNULL(d.tot_xta, '0.00') + ISNULL(d1.xt_pamt, '0.00')) - ISNULL(d1.xt_amt, '0.00')) +  ISNULL(n.tun_amt, '0.00') +   ISNULL(e.a1, '0.00') + ISNULL(e.a2, '0.00')) - (ISNULL(g.epf_amt, '0.00') +   case when m.perkeso_chk = '1' then ISNULL(m.tax_cp38_amt1, '0.00') else '0.00' end + case when l.sip_chk = '1' then ISNULL(l.tax_sip_amt1, '0.00') else '0.00' end + ISNULL(i.samt, '0.00') + cast(ISNULL(m1.pcb_amt,'0.00') as money))),'0.00') as nett_amt,ISNULL(j.amt1, '0.00') as ot_amt,ISNULL(k.epf_emp_kwsp_perc, '') as emp_kwsp_perc, case when m.perkeso_chk = '1' then  ISNULL(m.tax_cp38_amt2, '0.00') else '0.00' end as emp_perkeso_amt,case when l.sip_chk = '1' then ISNULL(l.tax_sip_amt1, '0.00') else '0.00' end as sip_amt1, case when l.sip_chk = '1' then ISNULL(l.tax_sip_amt2, '0.00')  else '0.00' end as sip_emp_amt1,ISNULL(k.epf_emp_kwsp_amt, '0.00') as kwsp_emp_amt,ISNULL(n.tun_amt, '0.00') as tunamt from (select stf_staff_no,stf_age,stf_cur_sub_org,str_curr_org_cd,org_id,stf_name,stf_icno from hr_staff_profile left join hr_post_his hp  on hp.pos_staff_no =stf_staff_no left join hr_organization as ho on ho.org_gen_id=str_curr_org_cd where pos_end_dt ='9999-12-31') as a left join (select slr_staff_no,slr_salary_amt from  hr_salary where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "') between FORMAT(slr_eff_dt,'yyyy-MM') And FORMAT(slr_end_dt,'yyyy-MM')) as b on b.slr_staff_no=a.stf_staff_no left join (select fx.fxa_staff_no, sum(fx.fxa_allowance_amt) as samt from hr_fixed_allowance as fx where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(fx.fxa_eff_dt,'yyyy-MM') And FORMAT(fx.fxa_end_dt,'yyyy-MM')) group by fx.fxa_staff_no) as c on  c.fxa_staff_no=a.stf_staff_no left join (select fx.fxa_staff_no, sum(fx.fxa_promo_amt) as fx_pamt,sum(fx.fxa_allowance_amt) as fx_amt from hr_fixed_allowance as fx where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(fx.fxa_pst_dt,'yyyy-MM') And FORMAT(fx.fxa_ped_dt,'yyyy-MM')) group by fx.fxa_staff_no) as c1 on  c1.fxa_staff_no=a.stf_staff_no left join (select fx.xta_staff_no,sum(fx.xta_allowance_amt) as tot_xta from hr_extra_allowance as fx where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(fx.xta_eff_dt,'yyyy-MM') And  FORMAT(fx.xta_end_dt,'yyyy-MM')) group by fx.xta_staff_no) as d on d.xta_staff_no=a.stf_staff_no left join (select fx.xta_staff_no,sum(fx.xta_promo_amt) as xt_pamt,sum(fx.xta_allowance_amt) as xt_amt from hr_extra_allowance as fx where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(fx.xta_pst_dt,'yyyy-MM') And  FORMAT(fx.xta_ped_dt,'yyyy-MM')) group by fx.xta_staff_no) as d1 on d1.xta_staff_no=a.stf_staff_no  left join (select bns_staff_no,sum(bns_amt) as a1,sum(bns_kpi_amt) as a2 from hr_Bonus where  (('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "') between FORMAT(bns_eff_dt,'yyyy-MM') And FORMAT(bns_end_dt,'yyyy-MM')) group by bns_staff_no) as e on e.bns_staff_no=a.stf_staff_no left join(select fx.ded_staff_no,sum( fx.ded_deduct_amt) as samt from hr_deduction as fx where ded_deduct_type_cd='09' and ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(fx.ded_start_dt,'yyyy-MM') And FORMAT(fx.ded_end_dt,'yyyy-MM')) group by  fx.ded_staff_no) as f on f.ded_staff_no=a.stf_staff_no left join (select epf_staff_no,SUM(epf_amt) epf_amt from hr_kwsp hk where '" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(hk.epf_eff_dt,'yyyy-MM') And FORMAT (hk.epf_end_dt,'yyyy-MM') group by epf_staff_no) as g on g.epf_staff_no=a.stf_staff_no left join(select * from hr_comm_perkeso ) h on (ISNULL(slr_salary_amt,'0.00') + ISNULL(c.samt,'0.00') + ISNULL(d.tot_xta,'0.00') +  ISNULL(e.a1,'0.00') + ISNULL(e.a2,'0.00')) between h.per_min_income_amt AND h.per_max_income_amt left join (select fx.ded_staff_no,sum(fx.ded_deduct_amt) as samt from hr_deduction as fx where  ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(fx.ded_start_dt,'yyyy-MM') And FORMAT(fx.ded_end_dt,'yyyy-MM'))  group by fx.ded_staff_no) i on i.ded_staff_no=a.stf_staff_no left join(select  otl_staff_no, sum(otl_ot_amt) as amt1 from hr_ot as ot  where FORMAT(otl_work_dt,'yyyy-MM') ='" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' group by otl_staff_no) as j on j.otl_staff_no=a.stf_staff_no left join(select epf_emp_kwsp_perc, sum(epf_emp_kwsp_amt) as epf_emp_kwsp_amt,epf_staff_no from hr_kwsp where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(epf_eff_dt,'yyyy-MM') And FORMAT(epf_end_dt,'yyyy-MM')) group by epf_staff_no,epf_emp_kwsp_perc)as k on k.epf_staff_no=a.stf_staff_no left join( select tax_sip_staff_no,SUM(tax_sip_amt1) tax_sip_amt1,SUM(tax_sip_amt2) tax_sip_amt2,sip_chk from hr_income_tax_sip where (('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "') between FORMAT(tax_sip_start_dt1,'yyyy-MM') And FORMAT(tax_sip_end_dt1,'yyyy-MM')) group by tax_sip_staff_no,sip_chk) l on l.tax_sip_staff_no=a.stf_staff_no left join(select SUM(tax_cp38_amt1) tax_cp38_amt1,SUM(tax_cp38_amt2) as tax_cp38_amt2,perkeso_chk,tax_staff_no from hr_income_tax where (('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "') between  FORMAT(tax_cp38_start_dt1,'yyyy-MM') And FORMAT(tax_cp38_end_dt1,'yyyy-MM')) group by perkeso_chk,tax_staff_no)  m on m.tax_staff_no=a.stf_staff_no left join(select SUM(tax_pcb_amt) pcb_amt,tax_staff_no from hr_income_tax where (('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "') between  FORMAT(tax_pcb_start_dt,'yyyy-MM') And FORMAT(tax_pcb_end_dt,'yyyy-MM')) group by tax_staff_no)  m1 on m1.tax_staff_no=a.stf_staff_no left join(select SUM(tun_amt) tun_amt,tun_staff_no from hr_tunggakan where tun_year='" + txt_tahun.SelectedValue + "' and tun_month='" + DD_bulancaruman.SelectedValue + "' group by tun_staff_no)  n on n.tun_staff_no=a.stf_staff_no " + sqry + "";

       

        using (SqlConnection con = new SqlConnection(conString))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        DataSet ds = new DataSet();
                        sda.Fill(ds);
                        if (ds.Tables[0].Rows.Count != 0)
                        {
                            Button6.Visible = true;
                            Button7.Visible = true;
                            gvSelected.DataSource = dt;
                            gvSelected.DataBind();

                            decimal salary = dt.AsEnumerable().Sum(row => row.Field<decimal>("salary"));
                            decimal ext_allow = dt.AsEnumerable().Sum(row => row.Field<decimal>("fix_alwncce"));
                            decimal ot_allow = dt.AsEnumerable().Sum(row => row.Field<decimal>("xta_alwnce"));
                            decimal bonus_thn = (dt.AsEnumerable().Sum(row => row.Field<decimal>("bns_amt")));
                            decimal bonus_kpi = (dt.AsEnumerable().Sum(row => row.Field<decimal>("kpi_bns_amt")));
                            decimal ot_klm = dt.AsEnumerable().Sum(row => row.Field<decimal>("ot_amt"));
                            decimal jp = dt.AsEnumerable().Sum(row => row.Field<decimal>("tot_ded_amt"));
                            decimal car_kwsp = (dt.AsEnumerable().Sum(row => row.Field<decimal>("kwsp_amt")));
                            decimal car_kwsp_emp = (dt.AsEnumerable().Sum(row => row.Field<decimal>("kwsp_emp_amt")));
                            decimal car_perkeso = (dt.AsEnumerable().Sum(row => row.Field<decimal>("perkeso_amt")));
                            decimal car_perkeso_emp = (dt.AsEnumerable().Sum(row => row.Field<decimal>("emp_perkeso_amt")));
                            decimal sip = (dt.AsEnumerable().Sum(row => row.Field<decimal>("SIP_amt1")));
                            decimal sip_emp = (dt.AsEnumerable().Sum(row => row.Field<decimal>("SIP_emp_amt1")));
                            decimal pcb = dt.AsEnumerable().Sum(row => row.Field<decimal>("pcb_amt"));
                            decimal cp_38 = dt.AsEnumerable().Sum(row => row.Field<decimal>("cp38_amt"));
                            decimal pkrm = dt.AsEnumerable().Sum(row => row.Field<decimal>("gross_amt"));
                            decimal pbrm = dt.AsEnumerable().Sum(row => row.Field<decimal>("nett_amt"));
                            decimal ded_amt = dt.AsEnumerable().Sum(row => row.Field<decimal>("ded_amt"));
                            decimal tun_amt = dt.AsEnumerable().Sum(row => row.Field<decimal>("tunamt"));

                            Label ft_01 = (Label)gvSelected.FooterRow.FindControl("ftr_001");
                            Label ft_02 = (Label)gvSelected.FooterRow.FindControl("ftr_002");
                            Label ft_03 = (Label)gvSelected.FooterRow.FindControl("ftr_003");
                            Label ft_04 = (Label)gvSelected.FooterRow.FindControl("ftr_004");
                            Label ft_05 = (Label)gvSelected.FooterRow.FindControl("ftr_005");
                            Label ft_06 = (Label)gvSelected.FooterRow.FindControl("ftr_006");
                            Label ft_06_1 = (Label)gvSelected.FooterRow.FindControl("ftr_006_1");
                            Label ft_07 = (Label)gvSelected.FooterRow.FindControl("ftr_007");
                            Label ft_08 = (Label)gvSelected.FooterRow.FindControl("ftr_008");
                            Label ft_09 = (Label)gvSelected.FooterRow.FindControl("ftr_009");
                            Label ft_10 = (Label)gvSelected.FooterRow.FindControl("ftr_010");
                            Label ft_11 = (Label)gvSelected.FooterRow.FindControl("ftr_011");
                            Label ft_12 = (Label)gvSelected.FooterRow.FindControl("ftr_012");
                            Label ft_13 = (Label)gvSelected.FooterRow.FindControl("ftr_013");
                            Label ft_14 = (Label)gvSelected.FooterRow.FindControl("ftr_014");
                            Label ft_15 = (Label)gvSelected.FooterRow.FindControl("ftr_015");
                            Label ft_16 = (Label)gvSelected.FooterRow.FindControl("ftr_016");
                            Label ft_17 = (Label)gvSelected.FooterRow.FindControl("ftr_017");
                            Label ft_18 = (Label)gvSelected.FooterRow.FindControl("ftr_017_1");

                            gvSelected.FooterRow.Cells[2].Text = "<strong>KESELURUHAN (RM)</strong>";
                            gvSelected.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;

                            ft_01.Text = salary.ToString("C").Replace("$", "");
                            ft_02.Text = ext_allow.ToString("C").Replace("$", "");
                            ft_03.Text = ot_allow.ToString("C").Replace("$", "");
                            ft_04.Text = bonus_thn.ToString("C").Replace("$", "");
                            ft_05.Text = bonus_kpi.ToString("C").Replace("$", "");
                            ft_06.Text = ot_klm.ToString("C").Replace("$", "");
                            ft_06_1.Text = tun_amt.ToString("C").Replace("$", "");
                            ft_07.Text = jp.ToString("C").Replace("$", "");
                            ft_08.Text = car_kwsp.ToString("C").Replace("$", "");
                            ft_09.Text = car_kwsp_emp.ToString("C").Replace("$", "");
                            ft_10.Text = car_perkeso.ToString("C").Replace("$", "");
                            ft_11.Text = car_perkeso_emp.ToString("C").Replace("$", "");
                            ft_12.Text = sip.ToString("C").Replace("$", "");
                            ft_13.Text = sip_emp.ToString("C").Replace("$", "");
                            ft_14.Text = ded_amt.ToString("C").Replace("$", "");
                            ft_15.Text = pkrm.ToString("C").Replace("$", "");
                            ft_16.Text = pbrm.ToString("C").Replace("$", "");
                            ft_17.Text = pcb.ToString("C").Replace("$", "");
                            ft_18.Text = cp_38.ToString("C").Replace("$", "");
                            Session["cr_mnth_slry"] = ft_16.Text;

                            String strDate = "01/"+ DD_bulancaruman.SelectedValue + "/"+ txt_tahun.SelectedValue + "";
                            DateTime dateTime = DateTime.ParseExact(strDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            string pre_year = dateTime.AddMonths(-1).ToString("yyyy");
                            string pre_mnth = dateTime.AddMonths(-1).ToString("MM");
                            DataTable gt_pmnth_jum = new DataTable();
                            gt_pmnth_jum = DBCon.Ora_Execute_table("select sum(ISNULL(slr_salary_amt,'0.00')) as salary,sum(ISNULL(c.samt,'0.00')) as fix_alwncce,SUM(ISNULL(d.tot_xta,'0.00')) as xta_alwnce,SUM(ISNULL(i.samt,'0.00')) as ded_amt,SUM(ISNULL(e.a1,'0.00')) as bns_amt,SUM(ISNULL(e.a2,'0.00')) as kpi_bns_amt,SUM(ISNULL((ISNULL(slr_salary_amt,'0.00') + ISNULL(c.samt,'0.00') + ISNULL(d.tot_xta,'0.00') + ISNULL(e.a1,'0.00') + ISNULL(e.a2,'0.00') + ISNULL(n.tun_amt,'0.00') ),'0.00')) as gross_amt,SUM(isnull(f.samt, '0.00')) as ctg_amt,SUM(ISNULL(g.epf_amt,'0.00')) as kwsp_amt,SUM(case when m.perkeso_chk = '1' then ISNULL(m.tax_cp38_amt1,'0.00') else '0.00' end) as  perkeso_amt,'0.00' as sip_amt,sum(ISNULL(PC.tax_pcb_amt,'0.00'))  as pcb_amt,cast('0.00' as decimal) as cp38_amt,SUM((ISNULL(g.epf_amt,'0.00') + ISNULL(PC.tax_pcb_amt,'0.00') +   case when m.perkeso_chk = '1' then ISNULL(m.tax_cp38_amt1,'0.00') else '0.00' end + case when l.sip_chk = '1' then ISNULL(l.tax_sip_amt1,'0.00') else '0.00' end + ISNULL(i.samt,'0.00'))) as tot_ded_amt,SUM(ISNULL(((ISNULL(slr_salary_amt,'0.00') + ISNULL(c.samt,'0.00') + ISNULL(d.tot_xta,'0.00') + ISNULL(n.tun_amt,'0.00') + ISNULL(e.a1,'0.00') + ISNULL(e.a2,'0.00')) - (ISNULL(g.epf_amt,'0.00') + ISNULL(i.samt,'0.00') + case when l.sip_chk = '1' then ISNULL(l.tax_sip_amt1,'0.00') else '0.00' end + ISNULL(PC.tax_pcb_amt,'0.00')  + case when m.perkeso_chk = '1' then ISNULL(m.tax_cp38_amt1,'0.00') else '0.00' end )),'0.00')) as nett_amt,SUM(ISNULL(j.amt1,'0.00')) as ot_amt,SUM( case when m.perkeso_chk = '1' then ISNULL(m.tax_cp38_amt2,'0.00') else '0.00' end) as  emp_perkeso_amt,SUM(case when l.sip_chk = '1' then ISNULL(l.tax_sip_amt1,'0.00') else '0.00' end) as sip_amt1, SUM(case when l.sip_chk = '1' then ISNULL(l.tax_sip_amt2,'0.00') else '0.00' end) as sip_emp_amt1,SUM(ISNULL(k.epf_emp_kwsp_amt,'0.00')) as kwsp_emp_amt,SUM(ISNULL(n.tun_amt,'0.00')) as tunamt from (select stf_staff_no,stf_age,stf_cur_sub_org,str_curr_org_cd,org_id,stf_name,stf_icno from hr_staff_profile left join hr_post_his hp  on hp.pos_staff_no =stf_staff_no left join hr_organization as ho on ho.org_gen_id=str_curr_org_cd where pos_end_dt ='9999-12-31') as a left join (select slr_staff_no,slr_salary_amt from  hr_salary where ('" + pre_year + "-" + pre_mnth + "') between FORMAT(slr_eff_dt,'yyyy-MM') And FORMAT(slr_end_dt,'yyyy-MM')) as b on b.slr_staff_no=a.stf_staff_no left join (select fx.fxa_staff_no, sum(fx.fxa_allowance_amt) as samt from hr_fixed_allowance as fx where ('" + pre_year + "-" + pre_mnth + "' between FORMAT(fx.fxa_eff_dt,'yyyy-MM') And FORMAT(fx.fxa_end_dt,'yyyy-MM')) group by fx.fxa_staff_no) as c on  c.fxa_staff_no=a.stf_staff_no left join (select fx.xta_staff_no,sum(fx.xta_allowance_amt) as tot_xta from hr_extra_allowance as fx where ('" + pre_year + "-" + pre_mnth + "' between FORMAT(fx.xta_eff_dt,'yyyy-MM') And  FORMAT(fx.xta_end_dt,'yyyy-MM')) group by fx.xta_staff_no) as d on d.xta_staff_no=a.stf_staff_no left join (select bns_staff_no,sum(bns_amt) as a1,sum(bns_kpi_amt) as a2 from hr_Bonus where  (('" + pre_year + "-" + pre_mnth + "') between FORMAT(bns_eff_dt,'yyyy-MM') And FORMAT(bns_end_dt,'yyyy-MM')) group by bns_staff_no) as e on e.bns_staff_no=a.stf_staff_no left join(select fx.ded_staff_no,sum( fx.ded_deduct_amt) as samt from hr_deduction as fx where ded_deduct_type_cd='09' and ('" + pre_year + "-" + pre_mnth + "' between FORMAT(fx.ded_start_dt,'yyyy-MM') And FORMAT(fx.ded_end_dt,'yyyy-MM')) group by  fx.ded_staff_no) as f on f.ded_staff_no=a.stf_staff_no left join (select epf_staff_no,SUM(epf_amt) epf_amt from hr_kwsp hk where '" + pre_year + "-" + pre_mnth + "' between FORMAT(hk.epf_eff_dt,'yyyy-MM') And FORMAT (hk.epf_end_dt,'yyyy-MM') group by epf_staff_no) as g on g.epf_staff_no=a.stf_staff_no left join(select * from hr_comm_perkeso ) h on (ISNULL(slr_salary_amt,'0.00') + ISNULL(c.samt,'0.00') + ISNULL(d.tot_xta,'0.00') +  ISNULL(e.a1,'0.00') + ISNULL(e.a2,'0.00')) between h.per_min_income_amt AND h.per_max_income_amt left join (select fx.ded_staff_no,sum(fx.ded_deduct_amt) as samt from hr_deduction as fx where  ('" + pre_year + "-" + pre_mnth + "' between FORMAT(fx.ded_start_dt,'yyyy-MM') And FORMAT(fx.ded_end_dt,'yyyy-MM'))  group by fx.ded_staff_no) i on i.ded_staff_no=a.stf_staff_no left join(select  otl_staff_no, sum(otl_ot_amt) as amt1 from hr_ot as ot  where otl_month='" + pre_mnth + "' and otl_year ='" + pre_year + "' group by otl_staff_no) as j on j.otl_staff_no=a.stf_staff_no left join(select epf_emp_kwsp_perc, sum(epf_emp_kwsp_amt) as epf_emp_kwsp_amt,epf_staff_no from hr_kwsp where ('" + pre_year + "-" + pre_mnth + "' between FORMAT(epf_eff_dt,'yyyy-MM') And FORMAT(epf_end_dt,'yyyy-MM')) group by epf_staff_no,epf_emp_kwsp_perc)as k on k.epf_staff_no=a.stf_staff_no left join( select tax_sip_staff_no,SUM(tax_sip_amt1) tax_sip_amt1,SUM(tax_sip_amt2) tax_sip_amt2,sip_chk from hr_income_tax_sip where (('" + pre_year + "-" + pre_mnth + "') between FORMAT(tax_sip_start_dt1,'yyyy-MM') And FORMAT(tax_sip_end_dt1,'yyyy-MM')) group by tax_sip_staff_no,sip_chk) l on l.tax_sip_staff_no=a.stf_staff_no left join(select SUM(tax_cp38_amt1) tax_cp38_amt1,SUM(tax_cp38_amt2) as tax_cp38_amt2,perkeso_chk,tax_staff_no from hr_income_tax where (('" + pre_year + "-" + pre_mnth + "') between  FORMAT(tax_cp38_start_dt1,'yyyy-MM') And FORMAT(tax_cp38_end_dt1,'yyyy-MM')) group by perkeso_chk,tax_staff_no)  m on m.tax_staff_no=a.stf_staff_no left join (select SUM(tax_pcb_amt ) tax_pcb_amt,tax_staff_no from hr_income_tax where (('" + pre_year + "-" + pre_mnth + "') between  FORMAT(tax_pcb_start_dt ,'yyyy-MM') And FORMAT(tax_pcb_end_dt,'yyyy-MM')) group by tax_staff_no)  PC on pc.tax_staff_no=a.stf_staff_no  left join(select SUM(tun_amt) tun_amt,tun_staff_no from hr_tunggakan where tun_year='" + pre_year + "' and tun_month='" + pre_mnth + "' group by tun_staff_no)  n on n.tun_staff_no=a.stf_staff_no");
                            if(gt_pmnth_jum.Rows.Count != 0)
                            {
                                pre_mnth_jum.Text = double.Parse(gt_pmnth_jum.Rows[0]["nett_amt"].ToString()).ToString("C").Replace("RM","").Replace("$", "");
                                Session["pre_mnth_slry"] = pre_mnth_jum.Text;
                            }


                        }
                        else
                        {
                            Button6.Visible = false;
                            Button7.Visible = false;
                            Button5.Visible = false;
                            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                            gvSelected.DataSource = ds;
                            gvSelected.DataBind();
                            int columncount = gvSelected.Rows[0].Cells.Count;
                            gvSelected.Rows[0].Cells.Clear();
                            gvSelected.Rows[0].Cells.Add(new TableCell());
                            gvSelected.Rows[0].Cells[0].ColumnSpan = columncount;
                            gvSelected.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
                        }
                    }
                }


            }

        }
    }

    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        savechkdvls();
        gvSelected.PageIndex = e.NewPageIndex;
        if (gvSelected.PageCount - 1 == gvSelected.PageIndex)
        {
            gvSelected.ShowFooter = true;
        }
        else
        {
            gvSelected.ShowFooter = false;
        }
        this.grid();
        //gvSelected.DataBind();
        chkdvaluesp();
    }

    private void chkdvaluesp()
    {

        ArrayList usercontent = (ArrayList)Session["chkditems"];

        if (usercontent != null && usercontent.Count > 0)
        {

            foreach (GridViewRow gvrow in gvSelected.Rows)
            {

                Int64 index;
                if (gvSelected.DataKeys[gvrow.RowIndex].Value.ToString() != "")
                {
                    index = Convert.ToInt64(gvSelected.DataKeys[gvrow.RowIndex].Value.ToString());
                }
                else
                {
                    index = 0;
                }

                if (usercontent.Contains(index))
                {

                    System.Web.UI.WebControls.CheckBox myCheckBox1 = (System.Web.UI.WebControls.CheckBox)gvrow.FindControl("chkSelect");

                    myCheckBox1.Checked = true;

                }

            }

        }

    }

    private void savechkdvls()
    {

        ArrayList usercontent = new ArrayList();

        Int64 index = -1;


        foreach (GridViewRow gvrow in gvSelected.Rows)
        {

            index = Convert.ToInt64(gvSelected.DataKeys[gvrow.RowIndex].Value);

            bool result = ((System.Web.UI.WebControls.CheckBox)gvrow.FindControl("chkSelect")).Checked;

            
            // Check in the Session

            if (Session["chkditems"] != null)

                usercontent = (ArrayList)Session["chkditems"];

            if (result)
            {

                if (!usercontent.Contains(index))

                    usercontent.Add(index);

            }

            else
            {
                usercontent.Remove(index);
            }

          
        }

        if (usercontent != null && usercontent.Count > 0)

            Session["chkditems"] = usercontent;

    }
    protected void OnCheckedChanged(object sender, EventArgs e)
    {
        bool isUpdateVisible = false;
        CheckBox chk = (sender as CheckBox);
        if (chk.ID == "chkAll")
        {
            foreach (GridViewRow row in gvSelected.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    //row.Cells[(gvSelected.Columns.Count - 1)].Controls.OfType<CheckBox>().FirstOrDefault().Checked = chk.Checked;
                    row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked = chk.Checked;
                }
            }
        }
        CheckBox chkAll = (gvSelected.HeaderRow.FindControl("chkAll") as CheckBox);
        chkAll.Checked = true;
        foreach (GridViewRow row in gvSelected.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                //bool isChecked = row.Cells[(gvSelected.Columns.Count - 1)].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                bool isChecked = row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
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

    protected void submit_button(object sender, EventArgs e)
    {
        string rcount = string.Empty;
        int count = 0;
        foreach (GridViewRow gvrow in gvSelected.Rows)
        {
            var rb = gvrow.FindControl("chkSelect") as System.Web.UI.WebControls.CheckBox;
            if (rb.Checked == true)
            {
                count++;
            }
            rcount = count.ToString();
        }
        if (rcount != "0")
        {            
            gvSelected.AllowPaging = false;
            savechkdvls();
            this.grid();
            gvSelected.DataBind();
            chkdvaluesp();
            int[] no = new int[gvSelected.Rows.Count];
            int i = 0;
            foreach (GridViewRow gvrow in gvSelected.Rows)
            {
                var checkbox = gvrow.FindControl("chkSelect") as CheckBox;
                if (checkbox.Checked == true)
                {
                    var val1 = gvrow.FindControl("Label2") as Label;
                    var stf_icno = gvrow.FindControl("stf_icno") as Label;
                    var val2 = gvrow.FindControl("Label3") as Label;
                    var val3 = gvrow.FindControl("Label4_mnth") as Label;
                    var val4 = gvrow.FindControl("Label1_org_id") as Label;
                    var val5 = gvrow.FindControl("sub_org") as Label;
                    var val6 = gvrow.FindControl("org_cd") as Label;
                    var val7 = gvrow.FindControl("ded_amt") as Label;
                    var val8 = gvrow.FindControl("bns_amt") as Label;
                    var val9 = gvrow.FindControl("kpi_bns_amt") as Label;
                    var val10 = gvrow.FindControl("ctg_amt") as Label;
                    var val11 = gvrow.FindControl("kwsp_amt") as Label;
                    var val12 = gvrow.FindControl("pcb_amt") as Label;
                    var val13 = gvrow.FindControl("cp38_amt") as Label;
                    var val14 = gvrow.FindControl("tot_ded_amt") as Label;
                    var val15 = gvrow.FindControl("ot_amt") as Label;
                    var val16 = gvrow.FindControl("emp_kwsp_perc") as Label;
                    var val17 = gvrow.FindControl("kwsp_emp_amt") as Label;
                    var val18 = gvrow.FindControl("emp_perkeso_amt") as Label;
                    var val19 = gvrow.FindControl("stf_icno") as Label;
                    var val20 = gvrow.FindControl("salary") as Label;
                    var val21 = gvrow.FindControl("fix_alwncce") as Label;
                    var val22 = gvrow.FindControl("xta_alwnce") as Label;
                    var val23 = gvrow.FindControl("perkeso_amt") as Label;
                    var val24 = gvrow.FindControl("tot_ded_amt") as Label;
                    var val25 = gvrow.FindControl("gross_amt") as Label;
                    var val26 = gvrow.FindControl("nett_amt") as Label;
                    var val27 = gvrow.FindControl("org_id") as Label;
                    var val28 = gvrow.FindControl("sip_amt1") as Label;
                    var val29 = gvrow.FindControl("emp_sip_amt1") as Label;
                    var val30 = gvrow.FindControl("tung_amt") as Label;

                    DataTable chk_income = new DataTable();
                    DataTable dtt1 = new DataTable();
                    string batch_name = string.Empty;
                    chk_income = DBCon.Ora_Execute_table("select inc_staff_no from hr_income where inc_staff_no='" + val2.Text + "' and inc_month='" + val3.Text + "' and inc_year='" + val4.Text + "' and inc_org_id='" + val27.Text + "'");
                    if (chk_income.Rows.Count == 0)
                    {
                        batch_name = "HR" + val3.Text + val4.Text;
                        string Inssql1_bon = "insert into hr_income (inc_staff_no,inc_month,inc_year,inc_grade_cd,inc_org_id,inc_dept_cd,inc_salary_amt,inc_cumm_fix_allwnce_amt,inc_cumm_xtra_allwnce_amt,inc_cumm_deduct_amt,inc_bonus_amt,inc_kpi_bonus_amt,inc_gross_amt,inc_ctg_amt,inc_kwsp_amt,inc_perkeso_amt,inc_pcb_amt,inc_cp38_amt,inc_total_deduct_amt,inc_nett_amt,inc_ot_amt,inc_crt_id,inc_crt_dt,inc_emp_perkeso_amt,inc_emp_kwsp_amt,inc_emp_kwsp_perc,inc_gen_id,inc_sub_org_id,inc_emp_SIP_amt,inc_SIP_amt,inc_ded_amt,inc_sts,inc_tunggakan_amt,inc_batch_name) values('" + val2.Text + "','" + val3.Text + "','" + val4.Text + "','','" + val27.Text + "','','" + val20.Text + "','" + val21.Text + "','" + val22.Text + "','" + val24.Text + "','" + val8.Text + "','" + val9.Text + "','" + val25.Text + "','" + val10.Text + "','" + val11.Text + "','" + val23.Text + "','" + val12.Text + "','" + val13.Text + "','" + val24.Text + "','" + val26.Text + "','" + val15.Text + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + val18.Text + "','" + val17.Text + "','" + val16.Text + "','" + val6.Text + "','" + val5.Text + "','" + val29.Text + "','" + val28.Text + "','" + val7.Text + "','0','" + val30.Text + "','"+ batch_name + "')";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql1_bon);

                        dtt1 = DBCon.Ora_Execute_table("UPDATE hr_staff_profile set stf_epf_no='" + stf_icno.Text + "',stf_tax_no='" + stf_icno.Text + "',stf_socso_no='" + stf_icno.Text + "' where stf_staff_no='" + val2.Text + "'");
                    }
                }

            }
            if (Status == "SUCCESS")
            {
                gvSelected.AllowPaging = true;
                send_email();
                savechkdvls();
                grid();
                chkdvaluesp();
                Session["chkditems"] = null;
                service.audit_trail("P0206", "Simpan", DD_bulancaruman.SelectedItem.Text +"_"+ txt_tahun.SelectedItem.Text, "");
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Hendak Dibatalkan',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }


    void send_email()
    {
        TextInfo txtinfo = culinfo.TextInfo;
        DataTable Ds = new DataTable();
        DataTable Ds1 = new DataTable();
        DataTable Ds2 = new DataTable();
        DataTable Ds3 = new DataTable();
        string Mailmsg = string.Empty, Mailmsg1 = string.Empty, Mail_id1 = string.Empty, Mail_id2 = string.Empty, bal_lve = string.Empty;
        try
        {
            Ds = Dblog.Ora_Execute_table("select * from KK_User_Login where KK_userid='admin'");
            Ds1 = Dblog.Ora_Execute_table("select * from KK_User_Login where KK_userid='C0019'");
            Ds2 = Dblog.Ora_Execute_table("select * from KK_User_Login where KK_userid='" + Session["new"].ToString() + "'");

            String strDate = "01/" + DD_bulancaruman.SelectedValue + "/" + txt_tahun.SelectedValue + "";
            DateTime dateTime = DateTime.ParseExact(strDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string pre_year = dateTime.AddMonths(-1).ToString("yyyy");
            string pre_mnth = dateTime.AddMonths(-1).ToString("MM");

            Ds3 = Dblog.Ora_Execute_table("select hr_month_Code,hr_month_desc from Ref_hr_month where hr_month_Code='"+ pre_mnth + "' ORDER BY hr_month_Code");

            if (Ds3.Rows.Count != 0)
            {
                bal_lve = Ds3.Rows[0]["hr_month_desc"].ToString();
            }
            else
            {
                bal_lve = "";
            }

            if (Ds.Rows[0]["KK_email"].ToString() != "")
            {
                Mail_id1 = Ds.Rows[0]["KK_email"].ToString();
            }

            if (Ds1.Rows[0]["KK_email"].ToString() != "")
            {
                Mail_id2 = Ds1.Rows[0]["KK_email"].ToString();
            }
         
            if (Mail_id1 != "")
            {

                var fromemail = new MailAddress("support@koopims.com", "KTHB Integrated Management System");
                //var toemail = new MailAddress("vengatit09@gmail.com");
                var toemail = new MailAddress(Mail_id1);
                var fromemailpassword = "P@ssw0rd";
                string subject = "KTHB - Salary Application";
                string body = "Hello " + txtinfo.ToTitleCase(Ds.Rows[0]["Kk_username"].ToString().ToLower()) + ",<br/>Pending Approval for " + DD_bulancaruman.SelectedItem.Text + ", " + txt_tahun.SelectedItem.Text + " salary amount of RM "+ Session["cr_mnth_slry"].ToString() + ".<br/><br/> Previous month total salary "+ bal_lve + ", "+ pre_year +" is RM " + Session["pre_mnth_slry"].ToString() + ".<br/><br/> If You require any clarifications about this application, please contact your HR Department. <br/><br/> Thank You,<br/><a><html><body><a href='http://www.koopims.com'> KTHB.koopims.com </a></body></html> . </a>";

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                {
                    Host = "webmail.koopims.com",
                    Port = 25,
                    EnableSsl = false,
                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromemail.Address, fromemailpassword)


                };
                using (var message = new MailMessage(fromemail, toemail)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                    smtp.Send(message);

            }

            if (Mail_id2 != "")
            {

                var fromemail = new MailAddress("support@koopims.com", "KTHB Integrated Management System");
                var toemail = new MailAddress(Mail_id2);
                //var toemail = new MailAddress("vengatit09@gmail.com");
                var fromemailpassword = "P@ssw0rd";
                string subject = "KTHB - Salary Application";
                string body = "Hello " + txtinfo.ToTitleCase(Ds1.Rows[0]["Kk_username"].ToString().ToLower()) + ",<br/>Pending Approval for " + DD_bulancaruman.SelectedItem.Text + ", " + txt_tahun.SelectedItem.Text + " salary amount of RM " + Session["cr_mnth_slry"].ToString() + ".<br/><br/> Previous month total salary " + bal_lve + ", " + pre_year + " is RM " + Session["pre_mnth_slry"].ToString() + ".<br/><br/> If You require any clarifications about this application, please contact your HR Department. <br/><br/> Thank You,<br/><a><html><body><a href='http://www.koopims.com'> KTHB.koopims.com </a></body></html> . </a>";

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                {
                    Host = "webmail.koopims.com",
                    Port = 25,
                    EnableSsl = false,
                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromemail.Address, fromemailpassword)


                };
                using (var message = new MailMessage(fromemail, toemail)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                    smtp.Send(message);

            }
            Session["cr_mnth_slry"] = "";
            Session["pre_mnth_slry"] = "";

        }
        catch (Exception ex)
        {
            service.LogError(ex.ToString());
        }
    }

    protected void ctk_values(object sender, EventArgs e)
    {
        get_stf_details();
        DataTable dt = new DataTable();

        dt = DBCon.Ora_Execute_table(query);


        if (dt.Rows.Count != 0)
        {


            StringBuilder builder = new StringBuilder();
            string strFileName = string.Format("{0}.{1}", "CUTI_KAITANGAN_" + DateTime.Now.ToString("ddMMyyyy") + "", "csv");
            builder.Append("No Kakitangan ,Nama Kakitangan,IC No,Tahun,Bulan,Gaji Pokok (RM),Elaun Tetap (RM),Lain-Lain Elaun (RM),Bonus Tahunan (RM),Bonus KPI (RM),Lain-Lain (RM),Pendapatan Potongan (RM),Caruman KWSP (RM),Caruman KWSP Majikan (RM),POTONGAN PERKESO (RM),PERKESO MAJIKAN (RM),POTONGAN SIP (RM),PCB (RM),CP 38 (RM),SIP Majikan (RM),LAIN-LAIN POTONGAN (RM),PENDAPATAN KASAR (RM),PENDAPATAN BERSIH (RM)" + Environment.NewLine);
            for (int k = 0; k <= (dt.Rows.Count - 1); k++)
            {
                builder.Append(dt.Rows[k]["stf_staff_no"].ToString() + " , " + dt.Rows[k]["stf_name"].ToString() + "," + dt.Rows[k]["stf_icno"].ToString() + "," + dt.Rows[k]["yer"].ToString() + "," + dt.Rows[k]["mnth"].ToString() + "," + dt.Rows[k]["salary"].ToString() + "," + dt.Rows[k]["fix_alwncce"].ToString() + "," + dt.Rows[k]["xta_alwnce"].ToString() + "," + dt.Rows[k]["bns_amt"].ToString() + "," + dt.Rows[k]["kpi_bns_amt"].ToString() + "," + dt.Rows[k]["tunamt"].ToString() + "," + dt.Rows[k]["tot_ded_amt"].ToString() + "," + dt.Rows[k]["kwsp_amt"].ToString() + "," + dt.Rows[k]["kwsp_emp_amt"].ToString() + "," + dt.Rows[k]["perkeso_amt"].ToString() + "," + dt.Rows[k]["emp_perkeso_amt"].ToString() + "," + dt.Rows[k]["sip_amt1"].ToString() + "," + dt.Rows[k]["pcb_amt"].ToString() + "," + dt.Rows[k]["cp38_amt"].ToString() + "," + dt.Rows[k]["sip_emp_amt1"].ToString() + "," + dt.Rows[k]["ded_amt"].ToString() + "," + dt.Rows[k]["gross_amt"].ToString() + "," + dt.Rows[k]["nett_amt"].ToString() + Environment.NewLine);
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

        string script = " $(function () {$(" + gvSelected.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
    }
    

    private DataTable GetData(SqlCommand cmd)
    {
        DataTable dt = new DataTable();
        String strConnString = System.Configuration.ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(strConnString);
        SqlDataAdapter sda = new SqlDataAdapter();
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        try
        {
            con.Open();
            sda.SelectCommand = cmd;
            sda.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            con.Close();
            sda.Dispose();
            con.Dispose();
        }
    }

  
    
    protected void click_pdf(object sender, EventArgs e)
    {
        {
            try
            {
                
                //Path
                DataSet ds = new DataSet();
                get_stf_details();
                DataTable dt = new DataTable();
               
                dt = DBCon.Ora_Execute_table(query);

                ds.Tables.Add(dt);

                Rptviwer_gaji.Reset();
                Rptviwer_gaji.LocalReport.Refresh();
                List<DataRow> listResult = dt.AsEnumerable().ToList();
                listResult.Count();
                int countRow = 0;
                countRow = listResult.Count();

               

                if (countRow != 0)
                {
                    Rptviwer_gaji.LocalReport.DataSources.Clear();

                    Rptviwer_gaji.LocalReport.ReportPath = "SUMBER_MANUSIA/gaji.rdlc";
                    ReportDataSource rds = new ReportDataSource("gaji", dt);
                   
                    Rptviwer_gaji.LocalReport.DataSources.Add(rds);

                    Rptviwer_gaji.LocalReport.DisplayName = "Janaan_Pendapatan_" + DateTime.Now.ToString("ddMMyyyy");
                    Rptviwer_gaji.LocalReport.Refresh();

                    //Refresh
                    Warning[] warnings;
                    string[] streamIds;
                    string contentType;
                    string encoding;
                    string extension;

                    //Export the RDLC Report to Byte Array.
                    byte[] bytes = Rptviwer_gaji.LocalReport.Render("PDF", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                    //Download the RDLC Report in Word, Excel, PDF and Image formats.
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = contentType;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=Janaan_Pendapatan_"+DateTime.Now.ToString("ddMMyyyy") +"." + extension);
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    protected void ctk_pdfvalues(object sender, EventArgs e)
    {
        get_stf_details();
        DataTable dt = new DataTable();

        dt = DBCon.Ora_Execute_table("select stf_staff_no as 'No_Kakitangan'  ,stf_name 'Nama_Kakitangan',stf_icno 'IC_No','" + txt_tahun.SelectedValue + "' as Tahun,'" + DD_bulancaruman.SelectedValue + "' as Bulan,ISNULL(slr_salary_amt,'0.00') as 'Gaji_Pokok',ISNULL(c.samt,'0.00') as 'Elaun_Tetap',ISNULL(d.tot_xta,'0.00') as 'Lain_Lain_Elaun',ISNULL(e.a1,'0.00') as 'Bonus_Tahunan',ISNULL(e.a2,'0.00') as 'Bonus_KPI',ISNULL(j.amt1,'0.00') as 'KLM',(ISNULL(g.epf_amt,'0.00') +  case when m.perkeso_chk = '1' then ISNULL(m.tax_cp38_amt1,'0.00') else '0.00' end + case when l.sip_chk = '1' then ISNULL(l.tax_sip_amt1,'0.00') else '0.00' end + ISNULL(i.samt,'0.00')) as 'Pendapatan_Potongan',ISNULL(g.epf_amt,'0.00') as 'Caruman_KWSP',ISNULL(k.epf_emp_kwsp_amt,'0.00') as 'Caruman_KWSP_Majikan',case when m.perkeso_chk = '1' then ISNULL(m.tax_cp38_amt1,'0.00') else '0.00' end as  'POTONGAN_PERKESO', case when m.perkeso_chk = '1' then ISNULL(m.tax_cp38_amt2,'0.00') else '0.00' end as  'PERKESO_MAJIKAN',case when l.sip_chk = '1' then ISNULL(l.tax_sip_amt1,'0.00') else '0.00' end as 'POTONGAN_SIP', case when l.sip_chk = '1' then ISNULL(l.tax_sip_amt2,'0.00') else '0.00' end as ' SIP_Majikan',ISNULL(i.samt,'0.00') as 'LAIN_LAIN_POTONGAN',ISNULL((ISNULL(slr_salary_amt,'0.00') + ISNULL(c.samt,'0.00') + ISNULL(d.tot_xta,'0.00') + ISNULL(e.a1,'0.00') + ISNULL(e.a2,'0.00') + ISNULL(n.tun_amt,'0.00') ),'0.00') as 'PENDAPATAN_KASAR',ISNULL(((ISNULL(slr_salary_amt,'0.00') + ISNULL(c.samt,'0.00') + ISNULL(d.tot_xta,'0.00') + ISNULL(n.tun_amt,'0.00') + ISNULL(e.a1,'0.00') + ISNULL(e.a2,'0.00')) - (ISNULL(g.epf_amt,'0.00') + ISNULL(i.samt,'0.00') + case when l.sip_chk = '1' then ISNULL(l.tax_sip_amt1,'0.00') else '0.00' end + case when m.perkeso_chk = '1' then ISNULL(m.tax_cp38_amt1,'0.00') else '0.00' end )),'0.00') as 'PENDAPATAN_BERSIH' from (select stf_staff_no,stf_age,stf_cur_sub_org,str_curr_org_cd,org_id,stf_name,stf_icno from hr_staff_profile left join hr_post_his hp  on hp.pos_staff_no =stf_staff_no left join hr_organization as ho on ho.org_gen_id=str_curr_org_cd where pos_end_dt ='9999-12-31') as a left join (select slr_staff_no,slr_salary_amt from  hr_salary where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "') between FORMAT(slr_eff_dt,'yyyy-MM') And FORMAT(slr_end_dt,'yyyy-MM')) as b on b.slr_staff_no=a.stf_staff_no left join (select fx.fxa_staff_no, sum(fx.fxa_allowance_amt) as samt from hr_fixed_allowance as fx where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(fx.fxa_eff_dt,'yyyy-MM') And FORMAT(fx.fxa_end_dt,'yyyy-MM')) group by fx.fxa_staff_no) as c on  c.fxa_staff_no=a.stf_staff_no left join (select fx.xta_staff_no,sum(fx.xta_allowance_amt) as tot_xta from hr_extra_allowance as fx where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(fx.xta_eff_dt,'yyyy-MM') And  FORMAT(fx.xta_end_dt,'yyyy-MM')) group by fx.xta_staff_no) as d on d.xta_staff_no=a.stf_staff_no left join (select bns_staff_no,sum(bns_amt) as a1,sum(bns_kpi_amt) as a2 from hr_Bonus where  (('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "') between FORMAT(bns_eff_dt,'yyyy-MM') And FORMAT(bns_end_dt,'yyyy-MM')) group by bns_staff_no) as e on e.bns_staff_no=a.stf_staff_no left join(select fx.ded_staff_no,sum( fx.ded_deduct_amt) as samt from hr_deduction as fx where ded_deduct_type_cd='09' and ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(fx.ded_start_dt,'yyyy-MM') And FORMAT(fx.ded_end_dt,'yyyy-MM')) group by  fx.ded_staff_no) as f on f.ded_staff_no=a.stf_staff_no left join (select epf_staff_no,SUM(epf_amt) epf_amt from hr_kwsp hk where '" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(hk.epf_eff_dt,'yyyy-MM') And FORMAT (hk.epf_end_dt,'yyyy-MM') group by epf_staff_no) as g on g.epf_staff_no=a.stf_staff_no left join(select * from hr_comm_perkeso ) h on (ISNULL(slr_salary_amt,'0.00') + ISNULL(c.samt,'0.00') + ISNULL(d.tot_xta,'0.00') +  ISNULL(e.a1,'0.00') + ISNULL(e.a2,'0.00')) between h.per_min_income_amt AND h.per_max_income_amt left join (select fx.ded_staff_no,sum(fx.ded_deduct_amt) as samt from hr_deduction as fx where  ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(fx.ded_start_dt,'yyyy-MM') And FORMAT(fx.ded_end_dt,'yyyy-MM'))  group by fx.ded_staff_no) i on i.ded_staff_no=a.stf_staff_no left join(select  otl_staff_no, sum(otl_ot_amt) as amt1 from hr_ot as ot  where otl_month='" + DD_bulancaruman.SelectedValue + "' and otl_year ='" + txt_tahun.SelectedValue + "' group by otl_staff_no) as j on j.otl_staff_no=a.stf_staff_no left join(select epf_emp_kwsp_perc, sum(epf_emp_kwsp_amt) as epf_emp_kwsp_amt,epf_staff_no from hr_kwsp where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(epf_eff_dt,'yyyy-MM') And FORMAT(epf_end_dt,'yyyy-MM')) group by epf_staff_no,epf_emp_kwsp_perc)as k on k.epf_staff_no=a.stf_staff_no left join( select tax_sip_staff_no,SUM(tax_sip_amt1) tax_sip_amt1,SUM(tax_sip_amt2) tax_sip_amt2,sip_chk from hr_income_tax_sip where (('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "') between FORMAT(tax_sip_start_dt1,'yyyy-MM') And FORMAT(tax_sip_end_dt1,'yyyy-MM')) group by tax_sip_staff_no,sip_chk) l on l.tax_sip_staff_no=a.stf_staff_no left join(select SUM(tax_cp38_amt1) tax_cp38_amt1,SUM(tax_cp38_amt2) as tax_cp38_amt2,perkeso_chk,tax_staff_no from hr_income_tax where (('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "') between  FORMAT(tax_cp38_start_dt1,'yyyy-MM') And FORMAT(tax_cp38_end_dt1,'yyyy-MM')) group by perkeso_chk,tax_staff_no)  m on m.tax_staff_no=a.stf_staff_no left join(select SUM(tun_amt) tun_amt,tun_staff_no from hr_tunggakan where tun_year='" + txt_tahun.SelectedValue + "' and tun_month='" + DD_bulancaruman.SelectedValue + "' group by tun_staff_no)  n on n.tun_staff_no=a.stf_staff_no " + sqry + "");
       

        if (dt.Rows.Count != 0)
        {
           
            ds.Tables.Add(dt);
            Rptviwer_gaji.Reset();
            Rptviwer_gaji.LocalReport.Refresh();
            List<DataRow> listResult = dt.AsEnumerable().ToList();

            listResult.Count();
            int countRow = 0;
            countRow = listResult.Count();

            if (countRow != 0)
            {

                Rptviwer_gaji.LocalReport.DataSources.Clear();
                Rptviwer_gaji.LocalReport.ReportPath = "SUMBER_MANUSIA/gaji.rdlc";
                ReportDataSource rds = new ReportDataSource("hrgaji", dt);
                Rptviwer_gaji.LocalReport.DataSources.Add(rds);

                Rptviwer_gaji.LocalReport.Refresh();
                Warning[] warnings;

                string[] streamids;

                string mimeType;

                string encoding;

                string extension;

                string devinfo = "<DeviceInfo><ColorDepth>32</ColorDepth><DpiX>350</DpiX><DpiY>350</DpiY><OutputFormat>PDF</OutputFormat>" +
                       "  <PageWidth>12.20in</PageWidth>" +
                        "  <PageHeight>8.27in</PageHeight>" +
                        "  <MarginTop>0.1in</MarginTop>" +
                        "  <MarginLeft>0.5in</MarginLeft>" +
                         "  <MarginRight>0in</MarginRight>" +
                         "  <MarginBottom>0in</MarginBottom>" +
                       "</DeviceInfo>";

                byte[] bytes = Rptviwer_gaji.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);


                Response.Buffer = true;

                Response.Clear();

                Response.ClearHeaders();

                Response.ClearContent();

                Response.ContentType = "application/pdf";


              
                Response.AddHeader("content-disposition", "attachment; filename= CUTI_KAITANGAN_" + DateTime.Now.ToString("dd_MM_yyyy") + "." + extension);
                Response.BinaryWrite(bytes);

              
                Response.Flush();

                Response.End();
            }

                //Document doc = new Document(PageSize.A4.Rotate(), 0, 0, 5, 0);

                //try
                //{

                //    PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/" + strFileName + "", FileMode.Create));
                //    doc.Open();
                //    PdfPTable tbl = new PdfPTable(21);
                //    tbl.TotalWidth = 800f;
                //    tbl.LockedWidth = true;
                //    float[] widths = new float[] { 20f, 60f, 60f, 30f, 50f, 80f, 50f, 50f, 50f, 50f };

                //    PdfPTable tbl1 = new PdfPTable(1);
                //    tbl1.AddCell(new Phrase("CARIAN MAKLUMAT PENDAPATAN"));
                //    tbl1.HorizontalAlignment = Element.ALIGN_CENTER;
                //    tbl1.TotalWidth = 220f;
                //    tbl1.LockedWidth = true;

                //    BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                //    var fnt = new iTextSharp.text.Font(bf, 7.0f, 0, BaseColor.BLACK);
                //    foreach (DataColumn c in dt.Columns)
                //    {
                //        tbl.AddCell(new Phrase(c.Caption, fnt));
                //    }
                //    foreach (DataRow row in dt.Rows)
                //    {

                //        tbl.AddCell(new Phrase(row[0].ToString(), fnt));
                //        tbl.AddCell(new Phrase(row[1].ToString(), fnt));
                //        tbl.AddCell(new Phrase(row[2].ToString(), fnt));
                //        tbl.AddCell(new Phrase(row[3].ToString(), fnt));
                //        tbl.AddCell(new Phrase(row[4].ToString(), fnt));
                //        tbl.AddCell(new Phrase(row[5].ToString(), fnt));
                //        tbl.AddCell(new Phrase(row[6].ToString(), fnt));
                //        tbl.AddCell(new Phrase(row[7].ToString(), fnt));
                //        tbl.AddCell(new Phrase(row[8].ToString(), fnt));
                //        tbl.AddCell(new Phrase(row[9].ToString(), fnt));
                //        tbl.AddCell(new Phrase(row[10].ToString(), fnt));
                //        tbl.AddCell(new Phrase(row[11].ToString(), fnt));
                //        tbl.AddCell(new Phrase(row[12].ToString(), fnt));
                //        tbl.AddCell(new Phrase(row[13].ToString(), fnt));
                //        tbl.AddCell(new Phrase(row[14].ToString(), fnt));
                //        tbl.AddCell(new Phrase(row[15].ToString(), fnt));
                //        tbl.AddCell(new Phrase(row[16].ToString(), fnt));
                //        tbl.AddCell(new Phrase(row[17].ToString(), fnt));
                //        tbl.AddCell(new Phrase(row[18].ToString(), fnt));
                //        tbl.AddCell(new Phrase(row[19].ToString(), fnt));
                //        tbl.AddCell(new Phrase(row[20].ToString(), fnt));
                //    }


                //    doc.Add(tbl1);
                //    doc.Add(tbl);



                //    System.Diagnostics.Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/" + strFileName + "");
                //}
                //catch (Exception ae)
                //{
                //    throw ae;
                //}

            }
            else
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod tidak dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }

        string script = " $(function () {$(" + gvSelected.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
    }

    protected void click_Hapus(object sender, EventArgs e)
    {
        string rcount = string.Empty;
        int count = 0;
        foreach (GridViewRow gvrow in gvSelected.Rows)
        {
            var rb = gvrow.FindControl("chkSelect") as System.Web.UI.WebControls.CheckBox;
            if (rb.Checked == true)
            {
                count++;
            }
            rcount = count.ToString();
        }
        if (rcount != "0")
        {
            gvSelected.AllowPaging = false;
            savechkdvls();
            this.grid();
            gvSelected.DataBind();
            chkdvaluesp();
            int[] no = new int[gvSelected.Rows.Count];
            int i = 0;
            foreach (GridViewRow gvrow in gvSelected.Rows)
            {
                var checkbox = gvrow.FindControl("chkSelect") as CheckBox;
                if (checkbox.Checked == true)
                {
                   
                    var val2 = gvrow.FindControl("Label3") as Label;
                    var val3 = gvrow.FindControl("Label4_mnth") as Label;
                    var val4 = gvrow.FindControl("Label1_org_id") as Label;
                    var val27 = gvrow.FindControl("org_id") as Label;
                 

                    DataTable chk_income = new DataTable();
                    DataTable dtt1 = new DataTable();
                    string batch_name = string.Empty;
                    chk_income = DBCon.Ora_Execute_table("select inc_staff_no from hr_income where inc_staff_no='" + val2.Text + "' and inc_month='" + val3.Text + "' and inc_year='" + val4.Text + "' and inc_org_id='" + val27.Text + "'");
                    if (chk_income.Rows.Count != 0)
                    {
                        string Inssql1_bon = "delete from hr_income where inc_staff_no='" + val2.Text + "' and inc_month='" + val3.Text + "' and inc_year='" + val4.Text + "' and inc_org_id='" + val27.Text + "'";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql1_bon);
                    }
                }

            }
            if (Status == "SUCCESS")
            {
                gvSelected.AllowPaging = true;                
                savechkdvls();
                grid();
                Session["chkditems"] = null;
                chkdvaluesp();
                service.audit_trail("P0206", "Hapus", DD_bulancaruman.SelectedItem.Text + "_" + txt_tahun.SelectedItem.Text, "");
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dihapuskan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Hendak Dibatalkan',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }
    protected void Reset_btn(object sender, EventArgs e)
    {
        Response.Redirect("../SUMBER_MANUSIA/hr_gaji_kelompok.aspx");
    }
}