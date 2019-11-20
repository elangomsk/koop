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


public partial class PP_Cetak_Jbb : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string str;
    DBConnection DBCon = new DBConnection();
    DBConnection Dblog = new DBConnection();
    SqlCommand com;
    DataTable dt = new DataTable();
    string Status = string.Empty;
    string level, userid;
    string cc_no = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Button1);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {

                level = Session["level"].ToString();
                userid = Session["New"].ToString();
                txtname.Attributes.Add("Readonly", "Readonly");
                //txtwil.Attributes.Add("Readonly", "Readonly");
                txtjumla.Attributes.Add("Readonly", "Readonly");
                //txtcaw.Attributes.Add("Readonly", "Readonly");
                txttempoh.Attributes.Add("Readonly", "Readonly");
                txtamaun.Attributes.Add("Readonly", "Readonly");
                txttemp.Attributes.Add("Readonly", "Readonly");
                TextBox2.Attributes.Add("Readonly", "Readonly");
                TextBox3.Attributes.Add("Readonly", "Readonly");
                TextBox4.Attributes.Add("Readonly", "Readonly");
                TextBox5.Attributes.Add("Readonly", "Readonly");
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
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
                com.CommandText = "select app_applcn_no from jpa_application JA Left Join jpa_jkkpa_approval as JJA ON JJA.jkk_applcn_no=JA.app_applcn_no where app_applcn_no like '%' + @Search + '%' and JJA.jkk_result_ind='L' and JA.applcn_clsed ='N'";
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
                            countryNames.Add(sdr["app_applcn_no"].ToString());

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

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }



    protected void btnsrch_Click(object sender, EventArgs e)
    {
        try
        {
            Session.Remove("sess_no");
            if (Applcn_no.Text != "")
            {

                DataTable ddicno = new DataTable();
                ddicno = DBCon.Ora_Execute_table("select distinct app_applcn_no from jpa_application JA where JA.app_sts_cd='Y' and '" + Applcn_no.Text + "' IN(JA.app_applcn_no, JA.app_new_icno)");

                if (ddicno.Rows.Count != 0)
                {
                    DataTable select_app = new DataTable();
                    select_app = DBCon.Ora_Execute_table("select * from (select JA.app_applcn_no,JA.app_new_icno,JA.app_name,RW.Wilayah_Name,ISNULL(RB.branch_desc, '') AS branch_desc,JA.app_loan_amt,JA.appl_loan_dur,JA.app_permnt_address,ja.app_permnt_postcode,ja.app_permnt_state_cd,ja.app_phone_h,ja.app_phone_m,ja.app_phone_o,ja.app_mailing_address,JA.app_mailing_postcode,ja.app_mailing_state_cd,ISNULL(JA.app_cumm_installment_amt,'') as app_cumm_installment_amt,ISNULL(JA.app_cumm_pay_amt,'') as app_cumm_pay_amt,ISNULL(JA.app_backdated_amt,'') as app_backdated_amt,ISNULL(JA.app_cumm_profit_amt,'') as app_cumm_profit_amt,ISNULL(JA.app_cumm_saving_amt,'') as app_cumm_saving_amt,ISNULL(JA.app_bal_loan_amt,'') as app_bal_loan_amt from jpa_application as JA  Left Join jpa_calculate_fee as JC ON JC.cal_applcn_no=JA.app_applcn_no Left Join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd Left join ref_branch AS RB ON RB.branch_cd=JA.app_branch_cd where JA.app_applcn_no = '" + ddicno.Rows[0]["app_applcn_no"] + "') as a full outer join (select * from jpa_write_off  where wri_applcn_no='" + ddicno.Rows[0]["app_applcn_no"] + "') as b on a.app_applcn_no=b.wri_applcn_no");

                    txtname.Text = select_app.Rows[0]["app_name"].ToString();
                    //txtwil.Text = select_app.Rows[0]["Wilayah_Name"].ToString();
                    decimal amt1 = (decimal)select_app.Rows[0]["app_loan_amt"];
                    txtjumla.Text = amt1.ToString("C").Replace("$", "").Replace("RM", "");
                    //txtcaw.Text = select_app.Rows[0]["branch_desc"].ToString();
                    txttempoh.Text = select_app.Rows[0]["appl_loan_dur"].ToString();

                    decimal a1 = (decimal)select_app.Rows[0]["app_cumm_installment_amt"];
                    txtamaun.Text = a1.ToString("C").Replace("$", "").Replace("RM", "");
                    decimal a2 = (decimal)select_app.Rows[0]["app_cumm_pay_amt"];
                    txttemp.Text = a2.ToString("C").Replace("$", "").Replace("RM", "");
                    decimal a3 = (decimal)select_app.Rows[0]["app_backdated_amt"];
                    TextBox2.Text = a3.ToString("C").Replace("$", "").Replace("RM", "");
                    decimal a4 = (decimal)select_app.Rows[0]["app_cumm_profit_amt"];
                    TextBox3.Text = a4.ToString("C").Replace("$", "").Replace("RM", "");
                    decimal a5 = (decimal)select_app.Rows[0]["app_cumm_saving_amt"];
                    TextBox4.Text = a5.ToString("C").Replace("$", "").Replace("RM", "");
                    decimal a6 = (decimal)select_app.Rows[0]["app_bal_loan_amt"];
                    TextBox5.Text = a6.ToString("C").Replace("$", "").Replace("RM", "");

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Semak Isu',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }


    protected void Button5_Click(object sender, EventArgs e)
    {
        try
        {
            if (Applcn_no.Text != "")
            {
                if (txttempoh.Text != "" && txtamaun.Text != "")
                {


                    userid = Session["New"].ToString();
                    DataTable dtGL = new DataTable();
                    string ket = Applcn_no.Text + txtname.Text.Trim() + " " + "Ansuran bulanan bagi" + " " + DateTime.Now.ToString("MMMM") + " " + DateTime.Now.Year;

                    decimal amt = Convert.ToDecimal(txtjumla.Text) / Convert.ToDecimal(txttempoh.Text);
                    decimal amt1 = Convert.ToDecimal(txtamaun.Text) - amt;


                    dtGL = Dblog.Ora_Execute_table("insert into KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,crt_id,cr_dt,kat_akaun,GL_invois_no,ref2,GL_process_dt,GL_desc1,GL_nama_kod,GL_sts)values('03.03','0.00','" + amt + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','03','" + Applcn_no.Text + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + ket + " ','03.03','A')");
                    dtGL = Dblog.Ora_Execute_table("insert into KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,crt_id,cr_dt,kat_akaun,GL_invois_no,ref2,GL_process_dt,GL_desc1,GL_nama_kod,GL_sts)values('10.06','0.00','" + amt1 + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','10','" + Applcn_no.Text + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + ket + "','10.06','A')");
                    dtGL = Dblog.Ora_Execute_table("insert into KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,crt_id,cr_dt,kat_akaun,GL_invois_no,ref2,GL_process_dt,GL_desc1,GL_nama_kod,GL_sts)values('03.14.01','" + txtamaun.Text + "','0.00','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','03','" + Applcn_no.Text + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + ket + "','03.14.01','A')");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Inserted Successfully');", true);
                }
            }


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void grid()
    {

    }

    protected void btn_rstclick(object sender, EventArgs e)
    {
        Response.Redirect("PP_Cetak_Jbb.aspx");
    }
    protected void Cetakjbb_Click(object sender, EventArgs e)
    {

        try
        {
            if (Applcn_no.Text != "")
            {
                var ic_count = Applcn_no.Text.Length;
                //Path var ic_count = Applcn_no.Text.Length;
                DataTable app_icno = new DataTable();
                app_icno = DBCon.Ora_Execute_table("select app_applcn_no,isnull(app_current_jbb_ind,'') AS app_current_jbb_ind,app_loan_type_cd,app_loan_amt from jpa_application JA where '" + Applcn_no.Text + "' IN(JA.app_applcn_no, JA.app_new_icno)");
                if (ic_count == 12)
                {
                    cc_no = app_icno.Rows[0]["app_applcn_no"].ToString();
                }
                else
                {
                    cc_no = Applcn_no.Text;
                }

                string sav_amt = string.Empty, emnth = string.Empty;
                string s_1 = "51000.00";
                string s_2 = "101000.00";
                string s_3 = "201000.00";
                string s_4 = "300000.00";
                if (app_icno.Rows[0]["app_loan_type_cd"].ToString() != "P")
                {
                    //sav_amt = ((0.1 / 100) * double.Parse(a1)).ToString("0.00");
                    emnth = "28";
                    if (double.Parse(app_icno.Rows[0]["app_loan_amt"].ToString()) < double.Parse(s_1))
                    {
                        sav_amt = "50.00";
                    }
                    else if (double.Parse(app_icno.Rows[0]["app_loan_amt"].ToString()) < double.Parse(s_2))
                    {
                        sav_amt = "100.00";
                    }
                    else if (double.Parse(app_icno.Rows[0]["app_loan_amt"].ToString()) < double.Parse(s_3))
                    {
                        sav_amt = "200.00";
                    }
                    else if (double.Parse(app_icno.Rows[0]["app_loan_amt"].ToString()) <= double.Parse(s_4))
                    {
                        sav_amt = "300.00";
                    }
                }
                else
                {
                    sav_amt = "0.00";
                }

                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string c_jbb = string.Empty;
                if (app_icno.Rows[0]["app_current_jbb_ind"].ToString().Trim() != "")
                {
                    c_jbb = app_icno.Rows[0]["app_current_jbb_ind"].ToString();
                    if (c_jbb == "L")
                    {
                        //dt = Dblog.Ora_Execute_table("SELECT jn.tex_applcn_no as app_no,FORMAT(jn.tex_pay_date,'dd/MM/yyyy', 'en-us') as p_dt,ISNULL(jn.tex_loan_amt,'') as l_amt,jn.tex_seq_no as seq_no,ISNULL(jn.tex_approve_amt,'') as a_amt,ISNULL(jn.tex_profit_amt,'') as pr_amt,jn.tex_actual_pay_date as ap_dt,ISNULL(jn.tex_pay_amt,'') as p_amt,ISNULL(jn.tex_actual_pay_amt,'') as ap_amt,ISNULL(jn.tex_late_excess_amt,'') as le_amt,ISNULL(jn.tex_saving_amt,'') as sa_amt,ISNULL(jn.tex_actual_saving_amt,'') as as_amt,ISNULL(jn.tex_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.tex_total_pay_amt,'') as tp_amt,ISNULL(jn.tex_balance_amt,'') as bal_amt,ISNULL(jn.tex_total_saving_amt,'') as ts_amt,jn.tex_day_late as dlate,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno FROM jpa_jbb_temp_extension as jn left join jpa_application as ja on ja.app_applcn_no = jn.tex_applcn_no where jn.tex_applcn_no='" + cc_no + "'");
                        dt = Dblog.Ora_Execute_table("select * from (SELECT ja.app_cumm_saving_amt as acs_amt,jn.jwo_applcn_no as app_no,FORMAT(jn.jwo_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.jwo_loan_amt,'')) when '0.00' then '' else ISNULL(jn.jwo_loan_amt,'') end as l_amt,jn.jwo_seq_no as seq_no,ISNULL(jn.jwo_approve_amt,'') as a_amt,ISNULL(jn.jwo_profit_amt,'') as pr_amt,ISNULL(case when CONVERT(DATE, jn.jwo_actual_pay_date) = '1900-01-01' then '' else CONVERT(CHAR(10), FORMAT(jn.jwo_actual_pay_date,'dd/MM/yyyy', 'en-us'), 103) END, '') as ap_dt,ISNULL(jn.jwo_pay_amt,'') as p_amt,ISNULL(jn.jwo_actual_pay_amt,'') as ap_amt,ISNULL(jn.jwo_late_excess_amt,'') as le_amt,ISNULL(jn.jwo_saving_amt,'') as sa_amt,ISNULL(jn.jwo_actual_saving_amt,'') as as_amt,ISNULL(jn.jwo_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.jwo_total_pay_amt,'') as tp_amt,ISNULL(jn.jwo_balance_amt,'') as bal_amt,ISNULL(jn.jwo_total_saving_amt,'') as ts_amt,jn.jwo_day_late as dlate,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno,rjp.Description FROM jpa_jbb_writeoff  as jn left join jpa_application as ja on ja.app_applcn_no = jn.jwo_applcn_no left join Ref_Jenis_Pelaburan as rjp on rjp.Description_Code=ja.app_loan_type_cd where jn.jwo_applcn_no='" + cc_no + "' ) as a full outer join (select jwo_applcn_no,sum(jwo_pay_amt) as tot_pamt,sum(jwo_actual_pay_amt) as tot_apamt,sum(jwo_saving_amt) as tot_samt,sum(jwo_actual_saving_amt) as tot_asamt,sum(jwo_total_pay_amt) as tot_tpamt from jpa_jbb_writeoff where jwo_applcn_no='" + cc_no + "' group by jwo_applcn_no) as b on b.jwo_applcn_no=a.app_no full outer join (select * from jpa_calculate_fee where cal_applcn_no='" + cc_no + "') as c on c.cal_applcn_no=b.jwo_applcn_no");
                    }
                    else if (c_jbb == "H")
                    {
                        dt = Dblog.Ora_Execute_table("select * from (SELECT ja.app_cumm_saving_amt as acs_amt,jn.hol_applcn_no as app_no,FORMAT(jn.hol_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.hol_loan_amt,'')) when '0.00' then '' else ISNULL(jn.hol_loan_amt,'') end as l_amt,jn.hol_seq_no as seq_no,ISNULL(jn.hol_approve_amt,'') as a_amt,ISNULL(jn.hol_profit_amt,'') as pr_amt,ISNULL(case when CONVERT(DATE, jn.hol_actual_pay_date) = '1900-01-01' then '' else CONVERT(CHAR(10), FORMAT(jn.hol_actual_pay_date,'dd/MM/yyyy', 'en-us'), 103) END, '') as ap_dt,ISNULL(jn.hol_pay_amt,'') as p_amt,ISNULL(jn.hol_actual_pay_amt,'') as ap_amt,ISNULL(jn.hol_late_excess_amt,'') as le_amt,ISNULL(jn.hol_saving_amt,'') as sa_amt,ISNULL(jn.hol_actual_saving_amt,'') as as_amt,ISNULL(jn.hol_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.hol_total_pay_amt,'') as tp_amt,ISNULL(jn.hol_balance_amt,'') as bal_amt,ISNULL(jn.hol_total_saving_amt,'') as ts_amt,jn.hol_day_late as dlate,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno,rjp.Description FROM jpa_jbb_holiday  as jn left join jpa_application as ja on ja.app_applcn_no = jn.hol_applcn_no left join Ref_Jenis_Pelaburan as rjp on rjp.Description_Code=ja.app_loan_type_cd where jn.hol_applcn_no='" + cc_no + "' ) as a full outer join (select hol_applcn_no,sum(hol_pay_amt) as tot_pamt,sum(hol_actual_pay_amt) as tot_apamt,sum(hol_saving_amt) as tot_samt,sum(hol_actual_saving_amt) as tot_asamt,sum(hol_total_pay_amt) as tot_tpamt from jpa_jbb_holiday where hol_applcn_no='" + cc_no + "' group by hol_applcn_no) as b on b.hol_applcn_no=a.app_no full outer join (select * from jpa_calculate_fee where cal_applcn_no='" + cc_no + "') as c on c.cal_applcn_no=b.hol_applcn_no");
                    }
                    else if (c_jbb == "P")
                    {
                        dt = Dblog.Ora_Execute_table("select * from (SELECT ja.app_cumm_saving_amt as acs_amt,jn.pjs_applcn_no as app_no,FORMAT(jn.pjs_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.pjs_loan_amt,'')) when '0.00' then '' else ISNULL(jn.pjs_loan_amt,'') end as l_amt,jn.pjs_seq_no as seq_no,ISNULL(jn.pjs_approve_amt,'') as a_amt,ISNULL(jn.pjs_profit_amt,'') as pr_amt,ISNULL(case when CONVERT(DATE, jn.pjs_actual_pay_date) = '1900-01-01' then '' else CONVERT(CHAR(10), FORMAT(jn.pjs_actual_pay_date,'dd/MM/yyyy', 'en-us'), 103) END, '') as ap_dt,ISNULL(jn.pjs_pay_amt,'') as p_amt,ISNULL(jn.pjs_actual_pay_amt,'') as ap_amt,ISNULL(jn.pjs_late_excess_amt,'') as le_amt,ISNULL(jn.pjs_saving_amt,'') as sa_amt,ISNULL(jn.pjs_actual_saving_amt,'') as as_amt,ISNULL(jn.pjs_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.pjs_total_pay_amt,'') as tp_amt,ISNULL(jn.pjs_balance_amt,'') as bal_amt,ISNULL(jn.pjs_total_saving_amt,'') as ts_amt,jn.pjs_day_late as dlate,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno,rjp.Description FROM jpa_jbb_pjs  as jn left join jpa_application as ja on ja.app_applcn_no = jn.pjs_applcn_no left join Ref_Jenis_Pelaburan as rjp on rjp.Description_Code=ja.app_loan_type_cd where jn.pjs_applcn_no='" + cc_no + "' ) as a full outer join (select pjs_applcn_no,sum(pjs_pay_amt) as tot_pamt,sum(pjs_actual_pay_amt) as tot_apamt,sum(pjs_saving_amt) as tot_samt,sum(pjs_actual_saving_amt) as tot_asamt,sum(pjs_total_pay_amt) as tot_tpamt from jpa_jbb_pjs where pjs_applcn_no='" + cc_no + "' group by pjs_applcn_no) as b on b.pjs_applcn_no=a.app_no full outer join (select * from jpa_calculate_fee where cal_applcn_no='" + cc_no + "') as c on c.cal_applcn_no=b.pjs_applcn_no");
                    }
                    else if (c_jbb == "E")
                    {
                        dt = Dblog.Ora_Execute_table("select * from (SELECT ja.app_cumm_saving_amt as acs_amt,jn.ext_applcn_no as app_no,FORMAT(jn.ext_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.ext_loan_amt,'')) when '0.00' then '' else ISNULL(jn.ext_loan_amt,'') end as l_amt,jn.ext_seq_no as seq_no,ISNULL(jn.ext_approve_amt,'') as a_amt,ISNULL(jn.ext_profit_amt,'') as pr_amt,ISNULL(case when CONVERT(DATE, jn.ext_actual_pay_date) = '1900-01-01' then '' else CONVERT(CHAR(10), FORMAT(jn.ext_actual_pay_date,'dd/MM/yyyy', 'en-us'), 103) END, '') as ap_dt,ISNULL(jn.ext_pay_amt,'') as p_amt,ISNULL(jn.ext_actual_pay_amt,'') as ap_amt,ISNULL(jn.ext_late_excess_amt,'') as le_amt,ISNULL(jn.ext_saving_amt,'') as sa_amt,ISNULL(jn.ext_actual_saving_amt,'') as as_amt,ISNULL(jn.ext_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.ext_total_pay_amt,'') as tp_amt,ISNULL(jn.ext_balance_amt,'') as bal_amt,ISNULL(jn.ext_total_saving_amt,'') as ts_amt,jn.ext_day_late as dlate,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno,rjp.Description FROM jpa_jbb_extension as jn left join jpa_application as ja on ja.app_applcn_no = jn.ext_applcn_no left join Ref_Jenis_Pelaburan as rjp on rjp.Description_Code=ja.app_loan_type_cd where jn.ext_applcn_no='" + cc_no + "' ) as a full outer join (select ext_applcn_no,sum(ext_pay_amt) as tot_pamt,sum(ext_actual_pay_amt) as tot_apamt,sum(ext_saving_amt) as tot_samt,sum(ext_actual_saving_amt) as tot_asamt,sum(ext_total_pay_amt) as tot_tpamt from jpa_jbb_extension where ext_applcn_no='" + cc_no + "' group by ext_applcn_no) as b on b.ext_applcn_no=a.app_no full outer join (select * from jpa_calculate_fee where cal_applcn_no='" + cc_no + "') as c on c.cal_applcn_no=b.ext_applcn_no");
                    }
                    else
                    {
                        //dt = Dblog.Ora_Execute_table("SELECT jn.jno_applcn_no as app_no,FORMAT(jn.jno_pay_date,'dd/MM/yyyy', 'en-us') as p_dt,ISNULL(jn.jno_loan_amt,'') as l_amt,jn.jno_seq_no as seq_no,ISNULL(jn.jno_approve_amt,'') as a_amt,ISNULL(jn.jno_profit_amt,'') as pr_amt,jn.jno_actual_pay_date as ap_dt,ISNULL(jn.jno_pay_amt,'') as p_amt,ISNULL(jn.jno_actual_pay_amt,'') as ap_amt,ISNULL(jn.jno_late_excess_amt,'') as le_amt,ISNULL(jn.jno_saving_amt,'') as sa_amt,ISNULL(jn.jno_actual_saving_amt,'') as as_amt,ISNULL(jn.jno_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.jno_total_pay_amt,'') as tp_amt,ISNULL(jn.jno_balance_amt,'') as bal_amt,ISNULL(jn.jno_total_saving_amt,'') as ts_amt,jn.jno_day_late as dlate,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno FROM jpa_jbb_normal as jn left join jpa_application as ja on ja.app_applcn_no = jn.jno_applcn_no where jn.jno_applcn_no='" + cc_no + "'");
                        dt = Dblog.Ora_Execute_table("select * from (SELECT ja.app_cumm_saving_amt as acs_amt,jn.jno_applcn_no as app_no,FORMAT(jn.jno_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.jno_loan_amt,'')) when '0.00' then '' else ISNULL(jn.jno_loan_amt,'') end as l_amt,jn.jno_seq_no as seq_no,ISNULL(jn.jno_approve_amt,'') as a_amt,ISNULL(jn.jno_profit_amt,'') as pr_amt,ISNULL(case when CONVERT(DATE, jn.jno_actual_pay_date) = '1900-01-01' then '' else CONVERT(CHAR(10), FORMAT(jn.jno_actual_pay_date,'dd/MM/yyyy', 'en-us'), 103) END, '') as ap_dt,ISNULL(jn.jno_pay_amt,'') as p_amt,ISNULL(jn.jno_actual_pay_amt,'') as ap_amt,ISNULL(jn.jno_late_excess_amt,'') as le_amt,ISNULL(jn.jno_saving_amt,'') as sa_amt,ISNULL(jn.jno_actual_saving_amt,'') as as_amt,ISNULL(jn.jno_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.jno_total_pay_amt,'') as tp_amt,ISNULL(jn.jno_balance_amt,'') as bal_amt,ISNULL(jn.jno_total_saving_amt,'') as ts_amt,jn.jno_day_late as dlate,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno,rjp.Description FROM jpa_jbb_normal as jn left join jpa_application as ja on ja.app_applcn_no = jn.jno_applcn_no left join Ref_Jenis_Pelaburan as rjp on rjp.Description_Code=ja.app_loan_type_cd where jn.jno_applcn_no='" + cc_no + "' ) as a full outer join (select jno_applcn_no,sum(ISNULL(jno_pay_amt,'')) as tot_pamt,sum(ISNULL(jno_actual_pay_amt,'')) as tot_apamt,sum(ISNULL(jno_saving_amt,'')) as tot_samt,sum(ISNULL(jno_actual_saving_amt,'')) as tot_asamt,sum(ISNULL(jno_total_pay_amt,'')) as tot_tpamt from jpa_jbb_normal where jno_applcn_no='" + cc_no + "' group by jno_applcn_no) as b on b.jno_applcn_no=a.app_no full outer join (select * from jpa_calculate_fee where cal_applcn_no='" + cc_no + "') as c on c.cal_applcn_no=b.jno_applcn_no");
                    }
                }
                else
                {
                    //dt = Dblog.Ora_Execute_table("SELECT jn.jno_applcn_no as app_no,FORMAT(jn.jno_pay_date,'dd/MM/yyyy', 'en-us') as p_dt,ISNULL(jn.jno_loan_amt,'') as l_amt,jn.jno_seq_no as seq_no,ISNULL(jn.jno_approve_amt,'') as a_amt,ISNULL(jn.jno_profit_amt,'') as pr_amt,jn.jno_actual_pay_date as ap_dt,ISNULL(jn.jno_pay_amt,'') as p_amt,ISNULL(jn.jno_actual_pay_amt,'') as ap_amt,ISNULL(jn.jno_late_excess_amt,'') as le_amt,ISNULL(jn.jno_saving_amt,'') as sa_amt,ISNULL(jn.jno_actual_saving_amt,'') as as_amt,ISNULL(jn.jno_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.jno_total_pay_amt,'') as tp_amt,ISNULL(jn.jno_balance_amt,'') as bal_amt,ISNULL(jn.jno_total_saving_amt,'') as ts_amt,jn.jno_day_late as dlate,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno FROM jpa_jbb_normal as jn left join jpa_application as ja on ja.app_applcn_no = jn.jno_applcn_no where jn.jno_applcn_no='" + cc_no + "'");
                    dt = Dblog.Ora_Execute_table("select * from (SELECT ja.app_cumm_saving_amt as acs_amt,jn.jno_applcn_no as app_no,FORMAT(jn.jno_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.jno_loan_amt,'')) when '0.00' then '' else ISNULL(jn.jno_loan_amt,'') end as l_amt,jn.jno_seq_no as seq_no,ISNULL(jn.jno_approve_amt,'') as a_amt,ISNULL(jn.jno_profit_amt,'') as pr_amt,ISNULL(case when CONVERT(DATE, jn.jno_actual_pay_date) = '1900-01-01' then '' else CONVERT(CHAR(10), FORMAT(jn.jno_actual_pay_date,'dd/MM/yyyy', 'en-us'), 103) END, '') as ap_dt,ISNULL(jn.jno_pay_amt,'') as p_amt,ISNULL(jn.jno_actual_pay_amt,'') as ap_amt,ISNULL(jn.jno_late_excess_amt,'') as le_amt,ISNULL(jn.jno_saving_amt,'') as sa_amt,ISNULL(jn.jno_actual_saving_amt,'') as as_amt,ISNULL(jn.jno_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.jno_total_pay_amt,'') as tp_amt,ISNULL(jn.jno_balance_amt,'') as bal_amt,ISNULL(jn.jno_total_saving_amt,'') as ts_amt,jn.jno_day_late as dlate,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno,rjp.Description FROM jpa_jbb_normal as jn left join jpa_application as ja on ja.app_applcn_no = jn.jno_applcn_no left join Ref_Jenis_Pelaburan as rjp on rjp.Description_Code=ja.app_loan_type_cd where jn.jno_applcn_no='" + cc_no + "' ) as a full outer join (select jno_applcn_no,sum(ISNULL(jno_pay_amt,'')) as tot_pamt,sum(ISNULL(jno_actual_pay_amt,'')) as tot_apamt,sum(ISNULL(jno_saving_amt,'')) as tot_samt,sum(ISNULL(jno_actual_saving_amt,'')) as tot_asamt,sum(ISNULL(jno_total_pay_amt,'')) as tot_tpamt from jpa_jbb_normal where jno_applcn_no='" + cc_no + "' group by jno_applcn_no) as b on b.jno_applcn_no=a.app_no full outer join (select * from jpa_calculate_fee where cal_applcn_no='" + cc_no + "') as c on c.cal_applcn_no=b.jno_applcn_no");
                }

                //dt = DBCon.Ora_Execute_table("select ja.app_name,ja.app_new_icno,ja.app_applcn_no,ja.appl_loan_dur,ISNULL(ja.app_installment_amt,'') as app_installment_amt,ja.app_loan_type_cd,tp.tpj_approve_amt,tp.tpj_profit_amt,tp.tpj_pay_date,tp.tpj_actual_pay_date,tp.tpj_approve_amt,tp.tpj_pay_amt,tp.tpj_actual_pay_amt,tp.tpj_late_excess_amt,tp.tpj_saving_amt,tp.tpj_actual_saving_amt,tp.tpj_saving_late_excess_amt,tp.tpj_total_pay_amt,tp.tpj_balance_amt,tp.tpj_total_saving_amt,tp.tpj_day_late from jpa_jbb_temp_pjs as tp left join jpa_application as ja on ja.app_applcn_no=tp.tpj_applcn_no and ja.app_sts_cd='Y' where tp.tpj_applcn_no='" + cc_no + "'");

                Rptviwer_cetakjbb.Reset();
                ds.Tables.Add(dt);

                Rptviwer_cetakjbb.LocalReport.DataSources.Clear();

                Rptviwer_cetakjbb.LocalReport.ReportPath = "PELABURAN_ANGGOTA/cetakjbb.rdlc";
                ReportDataSource rds = new ReportDataSource("CJBB", dt);

                ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("samt",sav_amt)

                     };


                Rptviwer_cetakjbb.LocalReport.SetParameters(rptParams);

                Rptviwer_cetakjbb.LocalReport.DataSources.Add(rds);

                //Refresh
                Rptviwer_cetakjbb.LocalReport.Refresh();
                Warning[] warnings;

                string[] streamids;

                string mimeType;

                string encoding;

                string extension;

                string fname = DateTime.Now.ToString("dd_MM_yyyy");

                string devinfo = "<DeviceInfo><ColorDepth>32</ColorDepth><DpiX>350</DpiX><DpiY>350</DpiY><OutputFormat>PDF</OutputFormat>" +
                       "  <PageWidth>12.20in</PageWidth>" +
                        "  <PageHeight>8.27in</PageHeight>" +
                        "  <MarginTop>0.1in</MarginTop>" +
                        "  <MarginLeft>0.5in</MarginLeft>" +
                         "  <MarginRight>0in</MarginRight>" +
                         "  <MarginBottom>0in</MarginBottom>" +
                       "</DeviceInfo>";

                byte[] bytes = Rptviwer_cetakjbb.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);

                Response.Buffer = true;

                Response.Clear();

                Response.ClearHeaders();

                Response.ClearContent();

                Response.ContentType = "application/pdf";

                Response.AddHeader("content-disposition", "attachment; filename=CetakJBB_" + cc_no + "." + extension);

                Response.BinaryWrite(bytes);

                Response.Flush();

                Response.End();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Medan Input Adalah Mandatori',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
            //Request.Redirect(url, false);
        }

    }

   
}