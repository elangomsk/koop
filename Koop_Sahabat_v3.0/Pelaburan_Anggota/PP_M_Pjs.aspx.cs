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

public partial class PP_M_Pjs : System.Web.UI.Page
{
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);

    DBConnection DBCon = new DBConnection();
    SqlCommand grid_cmd;
    DataTable dt = new DataTable();
    StudentWebService service = new StudentWebService();
    string Status = string.Empty;
    string level, userid;
    string cc_no = string.Empty;
    string c_jbb = string.Empty;
    decimal total = 0M;
    decimal total1 = 0M;
    decimal total2 = 0M;
    decimal total3 = 0M;
    int counter = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Button2);
        scriptManager.RegisterPostBackControl(this.Button10);
        scriptManager.RegisterPostBackControl(this.btnShow);

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
                grid();
                if (Session["sess_no"] != "" && Session["sess_no"] != null)
                {
                    Applcn_no.Text = Session["sess_no"].ToString();
                    srch();
                }

                var samp = Request.Url.Query;
                if (samp != "")
                {
                    Applcn_no.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    srch();
                    Button9.Visible = false;
                    Button10.Visible = false;
                }
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

    protected void btnsrch_Click(object sender, EventArgs e)
    {
        srch();

    }
    protected void srch()
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
                    select_app = DBCon.Ora_Execute_table("select JA.app_applcn_no,JA.app_new_icno,JA.app_name,RW.Wilayah_Name,ISNULL(RB.branch_desc, '') AS branch_desc,JA.app_loan_amt,JA.appl_loan_dur,JA.app_permnt_address,ja.app_permnt_postcode,ja.app_permnt_state_cd,ja.app_phone_h,ja.app_phone_m,ja.app_phone_o,ja.app_mailing_address,JA.app_mailing_postcode,ja.app_mailing_state_cd,ISNULL(JA.app_cumm_installment_amt,'') as app_cumm_installment_amt,ISNULL(JA.app_cumm_pay_amt,'') as app_cumm_pay_amt,ISNULL(JA.app_backdated_amt,'') as app_backdated_amt,ISNULL(JA.app_cumm_profit_amt,'') as app_cumm_profit_amt,ISNULL(JA.app_cumm_saving_amt,'') as app_cumm_saving_amt,ISNULL(JA.app_bal_loan_amt,'') as app_bal_loan_amt,ISNULL(jp.apj_apply_sts_cd,'') as apj_apply_sts_cd,ISNULL(jp.apj_apply_remark,'') as apj_apply_remark,ISNULL(jc.cal_approve_amt,'') as cal_approve_amt,ISNULL(jc.cal_profit_amt,'') as cal_profit_amt  from jpa_application as JA Left join jpa_calculate_fee as jc on jc.cal_applcn_no=ja.app_applcn_no  Left Join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd Left join ref_branch AS RB ON RB.branch_cd=JA.app_branch_cd left join jpa_pjs_application as jp on jp.apj_applcn_no=ja.app_applcn_no where ja.app_applcn_no='" + ddicno.Rows[0]["app_applcn_no"] + "'");

                    txtname.Text = select_app.Rows[0]["app_name"].ToString();
                    //txtwil.Text = select_app.Rows[0]["Wilayah_Name"].ToString();
                    decimal amt1 = (decimal)select_app.Rows[0]["app_loan_amt"];
                    txtjumla.Text = amt1.ToString("C").Replace("$","").Replace("RM", "");
                    //txtcaw.Text = select_app.Rows[0]["branch_desc"].ToString();
                    txttempoh.Text = select_app.Rows[0]["appl_loan_dur"].ToString();

                    decimal a1 = (decimal)select_app.Rows[0]["app_cumm_installment_amt"];
                    txtamaun.Text = a1.ToString("C").Replace("$", "").Replace("RM", "");
                    decimal a2 = (decimal)select_app.Rows[0]["app_cumm_pay_amt"];
                    txttemp.Text = a2.ToString("C").Replace("$", "").Replace("RM", "");
                    decimal a3 = (decimal)select_app.Rows[0]["app_backdated_amt"];
                    TextBox2.Text = a3.ToString("C").Replace("$", "").Replace("RM", "");
                    decimal a4 = (decimal)select_app.Rows[0]["app_cumm_profit_amt"];
                    TextBox3.Text = a4.ToString("C").Replace("$","").Replace("RM", "");
                    decimal a5 = (decimal)select_app.Rows[0]["app_cumm_saving_amt"];
                    TextBox4.Text = a5.ToString("C").Replace("$", "").Replace("RM", "");
                    decimal a6 = (decimal)select_app.Rows[0]["app_bal_loan_amt"];
                    //if (a6.ToString("C").Replace("$", "").Replace("RM", "") != "0.00")
                    //{
                        TextBox5.Text = a6.ToString("C").Replace("$", "").Replace("RM", "");
                    //}
                    //else
                    //{
                    //    double samt1 = ( Convert.ToDouble(TextBox3.Text) + Convert.ToDouble(txtjumla.Text));
                    //    TextBox5.Text = Convert.ToDouble(samt1).ToString("C").Replace("RM","").Replace("$", "").Replace(",", "");
                    //}
                    decimal a7 = (decimal)select_app.Rows[0]["cal_approve_amt"];
                    capp_amt.Text = a7.ToString("C").Replace("$","").Replace("RM", "");
                    decimal a8 = (decimal)select_app.Rows[0]["cal_profit_amt"];
                    cpro_amt.Text = a8.ToString("C").Replace("$","").Replace("RM", "");

                    DropDownList1.SelectedValue = select_app.Rows[0]["apj_apply_sts_cd"].ToString();
                    Textarea1.Value = select_app.Rows[0]["apj_apply_remark"].ToString();
                    grid();

                }
                else
                {
                    Applcn_no.Text = "";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    grid();

                }

            }
            else
            {
                grid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Ic OR NO KP Baru',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Semak Isu',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }

    void grid()
    {

        var ic_count1 = Applcn_no.Text.Length;
        DataTable app_icno1 = new DataTable();
        app_icno1 = DBCon.Ora_Execute_table("select app_applcn_no,isnull(app_curr_temp_jbb_ind,'') AS app_curr_temp_jbb_ind from jpa_application JA where '" + Applcn_no.Text + "' IN(JA.app_applcn_no, JA.app_new_icno)");
        if (ic_count1 == 12)
        {
            cc_no = app_icno1.Rows[0]["app_applcn_no"].ToString();
        }
        else
        {
            cc_no = Applcn_no.Text;
        }
        con.Open();

        if (app_icno1.Rows.Count != 0)
        {
            // if (app_icno1.Rows[0]["app_current_jbb_ind"].ToString().Trim() != "")
            // {
            c_jbb = app_icno1.Rows[0]["app_curr_temp_jbb_ind"].ToString();
            if (c_jbb == "P")
            {
                grid_cmd = new SqlCommand("select * from (SELECT jn.tpj_applcn_no as app_no,FORMAT(jn.tpj_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.tpj_loan_amt,'')) when '0.00' then '' else ISNULL(jn.tpj_loan_amt,'') end as l_amt,jn.tpj_seq_no as seq_no,ISNULL(jn.tpj_approve_amt,'') as a_amt,ISNULL(jn.tpj_profit_amt,'') as pr_amt,FORMAT(tpj_actual_pay_date,'dd/MM/yyyy', 'en-us') as ap_dt,ISNULL(jn.tpj_pay_amt,'') as p_amt,ISNULL(jn.tpj_actual_pay_amt,'') as ap_amt,ISNULL(jn.tpj_late_excess_amt,'') as le_amt,ISNULL(jn.tpj_saving_amt,'') as sa_amt,ISNULL(jn.tpj_actual_saving_amt,'') as as_amt,ISNULL(jn.tpj_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.tpj_total_pay_amt,'') as tp_amt,ISNULL(jn.tpj_balance_amt,'') as bal_amt,ISNULL(jn.tpj_total_saving_amt,'') as ts_amt,jn.tpj_day_late as dlate,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno,rjp.Description FROM jpa_jbb_temp_pjs  as jn left join jpa_application as ja on ja.app_applcn_no = jn.tpj_applcn_no left join Ref_Jenis_Pelaburan as rjp on rjp.Description_Code=ja.app_loan_type_cd where jn.tpj_applcn_no='" + cc_no + "' ) as a full outer join (select tpj_applcn_no,sum(tpj_pay_amt) as tot_pamt,sum(tpj_actual_pay_amt) as tot_apamt,sum(tpj_saving_amt) as tot_samt,sum(tpj_actual_saving_amt) as tot_asamt,sum(tpj_total_pay_amt) as tot_tpamt from jpa_jbb_temp_pjs where tpj_applcn_no='" + cc_no + "' group by tpj_applcn_no) as b on b.tpj_applcn_no=a.app_no full outer join (select * from jpa_calculate_fee where cal_applcn_no='" + cc_no + "') as c on c.cal_applcn_no=b.tpj_applcn_no", con);
            }
            else if (c_jbb == "L")
            {
                //dt = Dblog.Ora_Execute_table("SELECT jn.tex_applcn_no as app_no,FORMAT(jn.tex_pay_date,'dd/MM/yyyy', 'en-us') as p_dt,ISNULL(jn.tex_loan_amt,'') as l_amt,jn.tex_seq_no as seq_no,ISNULL(jn.tex_approve_amt,'') as a_amt,ISNULL(jn.tex_profit_amt,'') as pr_amt,jn.tex_actual_pay_date as ap_dt,ISNULL(jn.tex_pay_amt,'') as p_amt,ISNULL(jn.tex_actual_pay_amt,'') as ap_amt,ISNULL(jn.tex_late_excess_amt,'') as le_amt,ISNULL(jn.tex_saving_amt,'') as sa_amt,ISNULL(jn.tex_actual_saving_amt,'') as as_amt,ISNULL(jn.tex_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.tex_total_pay_amt,'') as tp_amt,ISNULL(jn.tex_balance_amt,'') as bal_amt,ISNULL(jn.tex_total_saving_amt,'') as ts_amt,jn.tex_day_late as dlate,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno FROM jpa_jbb_temp_extension as jn left join jpa_application as ja on ja.app_applcn_no = jn.tex_applcn_no where jn.tex_applcn_no='" + cc_no + "'");
                grid_cmd = new SqlCommand("select * from (SELECT jn.two_applcn_no as app_no,FORMAT(jn.two_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.two_loan_amt,'')) when '0.00' then '' else ISNULL(jn.two_loan_amt,'') end as l_amt,jn.two_seq_no as seq_no,ISNULL(jn.two_approve_amt,'') as a_amt,ISNULL(jn.two_profit_amt,'') as pr_amt,FORMAT(two_actual_pay_date,'dd/MM/yyyy', 'en-us') as ap_dt,ISNULL(jn.two_pay_amt,'') as p_amt,ISNULL(jn.two_actual_pay_amt,'') as ap_amt,ISNULL(jn.two_late_excess_amt,'') as le_amt,ISNULL(jn.two_saving_amt,'') as sa_amt,ISNULL(jn.two_actual_saving_amt,'') as as_amt,ISNULL(jn.two_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.two_total_pay_amt,'') as tp_amt,ISNULL(jn.two_balance_amt,'') as bal_amt,ISNULL(jn.two_total_saving_amt,'') as ts_amt,jn.two_day_late as dlate,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno,rjp.Description FROM jpa_jbb_temp_writeoff  as jn left join jpa_application as ja on ja.app_applcn_no = jn.two_applcn_no left join Ref_Jenis_Pelaburan as rjp on rjp.Description_Code=ja.app_loan_type_cd where jn.two_applcn_no='" + cc_no + "' ) as a full outer join (select two_applcn_no,sum(two_pay_amt) as tot_pamt,sum(two_actual_pay_amt) as tot_apamt,sum(two_saving_amt) as tot_samt,sum(two_actual_saving_amt) as tot_asamt,sum(two_total_pay_amt) as tot_tpamt from jpa_jbb_temp_writeoff where two_applcn_no='" + cc_no + "' group by two_applcn_no) as b on b.two_applcn_no=a.app_no full outer join (select * from jpa_calculate_fee where cal_applcn_no='" + cc_no + "') as c on c.cal_applcn_no=b.two_applcn_no", con);
            }
            else if (c_jbb == "H")
            {
                grid_cmd = new SqlCommand("select * from (SELECT jn.tho_applcn_no as app_no,FORMAT(jn.tho_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.tho_loan_amt,'')) when '0.00' then '' else ISNULL(jn.tho_loan_amt,'') end as l_amt,jn.tho_seq_no as seq_no,ISNULL(jn.tho_approve_amt,'') as a_amt,ISNULL(jn.tho_profit_amt,'') as pr_amt,FORMAT(tho_actual_pay_date,'dd/MM/yyyy', 'en-us') as ap_dt,ISNULL(jn.tho_pay_amt,'') as p_amt,ISNULL(jn.tho_actual_pay_amt,'') as ap_amt,ISNULL(jn.tho_late_excess_amt,'') as le_amt,ISNULL(jn.tho_saving_amt,'') as sa_amt,ISNULL(jn.tho_actual_saving_amt,'') as as_amt,ISNULL(jn.tho_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.tho_total_pay_amt,'') as tp_amt,ISNULL(jn.tho_balance_amt,'') as bal_amt,ISNULL(jn.tho_total_saving_amt,'') as ts_amt,jn.tho_day_late as dlate,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno,rjp.Description FROM jpa_jbb_temp_holiday  as jn left join jpa_application as ja on ja.app_applcn_no = jn.tho_applcn_no left join Ref_Jenis_Pelaburan as rjp on rjp.Description_Code=ja.app_loan_type_cd where jn.tho_applcn_no='" + cc_no + "' ) as a full outer join (select tho_applcn_no,sum(tho_pay_amt) as tot_pamt,sum(tho_actual_pay_amt) as tot_apamt,sum(tho_saving_amt) as tot_samt,sum(tho_actual_saving_amt) as tot_asamt,sum(tho_total_pay_amt) as tot_tpamt from jpa_jbb_temp_holiday where tho_applcn_no='" + cc_no + "' group by tho_applcn_no) as b on b.tho_applcn_no=a.app_no full outer join (select * from jpa_calculate_fee where cal_applcn_no='" + cc_no + "') as c on c.cal_applcn_no=b.tho_applcn_no", con);
            }
            else if (c_jbb == "E")
            {
                grid_cmd = new SqlCommand("select * from (SELECT jn.tex_applcn_no as app_no,FORMAT(jn.tex_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.tex_loan_amt,'')) when '0.00' then '' else ISNULL(jn.tex_loan_amt,'') end as l_amt,jn.tex_seq_no as seq_no,ISNULL(jn.tex_approve_amt,'') as a_amt,ISNULL(jn.tex_profit_amt,'') as pr_amt,FORMAT(tex_actual_pay_date,'dd/MM/yyyy', 'en-us') as ap_dt,ISNULL(jn.tex_pay_amt,'') as p_amt,ISNULL(jn.tex_actual_pay_amt,'') as ap_amt,ISNULL(jn.tex_late_excess_amt,'') as le_amt,ISNULL(jn.tex_saving_amt,'') as sa_amt,ISNULL(jn.tex_actual_saving_amt,'') as as_amt,ISNULL(jn.tex_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.tex_total_pay_amt,'') as tp_amt,ISNULL(jn.tex_balance_amt,'') as bal_amt,ISNULL(jn.tex_total_saving_amt,'') as ts_amt,jn.tex_day_late as dlate,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno,rjp.Description FROM jpa_jbb_temp_extension as jn left join jpa_application as ja on ja.app_applcn_no = jn.tex_applcn_no left join Ref_Jenis_Pelaburan as rjp on rjp.Description_Code=ja.app_loan_type_cd where jn.tex_applcn_no='" + cc_no + "' ) as a full outer join (select tex_applcn_no,sum(tex_pay_amt) as tot_pamt,sum(tex_actual_pay_amt) as tot_apamt,sum(tex_saving_amt) as tot_samt,sum(tex_actual_saving_amt) as tot_asamt,sum(tex_total_pay_amt) as tot_tpamt from jpa_jbb_temp_extension where tex_applcn_no='" + cc_no + "' group by tex_applcn_no) as b on b.tex_applcn_no=a.app_no full outer join (select * from jpa_calculate_fee where cal_applcn_no='" + cc_no + "') as c on c.cal_applcn_no=b.tex_applcn_no", con);
            }

            else
            {
                //grid_cmd = new SqlCommand("select ext_applcn_no as app_no,ext_pay_date as p_dt,ext_actual_pay_date as pa_dt,ext_pay_amt as p_amt,ext_actual_pay_amt as pa_amt, 0.00 as prinsipal, 0.00 as keuntungan,ext_approve_amt as app_amt,ext_profit_amt as pro_amt,ext_late_excess_amt as amt1, ext_saving_amt as amt2,ext_actual_saving_amt as amt3,ext_saving_late_excess_amt as amt4,ext_total_pay_amt as amt5,ext_balance_amt as amt6,ext_total_saving_amt as amt7,ext_day_late as d_late from jpa_jbb_extension where ext_applcn_no='" + cc_no + "'", con);
                grid_cmd = new SqlCommand("select jno_applcn_no as app_no,jno_pay_date as p_dt, FORMAT(jno_actual_pay_date,'dd/MM/yyyy', 'en-us') as ap_dt,cast(jno_pay_amt as decimal(10,2)) as p_amt,ISNULL(jno_actual_pay_amt,'') as ap_amt,0.00 as prinsipal,0.00 as keuntungan,ISNULL(jno_approve_amt,'') as a_amt,ISNULL(jno_profit_amt,'') as pro_amt,jno_seq_no as seq_no,jno_late_excess_amt as amt1, jno_saving_amt as sa_amt,jno_actual_saving_amt as amt3,jno_saving_late_excess_amt as amt4,jno_total_pay_amt as amt5,jno_balance_amt as bal_amt,jno_total_saving_amt as ts_amt,jno_day_late as d_late from jpa_jbb_normal where jno_applcn_no='" + cc_no + "'", con);
            }
        }
        else
        {
            //grid_cmd = new SqlCommand("select ext_applcn_no as app_no,ext_pay_date as p_dt,ext_actual_pay_date as pa_dt,ext_pay_amt as p_amt,ext_actual_pay_amt as pa_amt, 0.00 as prinsipal, 0.00 as keuntungan,ext_approve_amt as app_amt,ext_profit_amt as pro_amt,ext_late_excess_amt as amt1, ext_saving_amt as amt2,ext_actual_saving_amt as amt3,ext_saving_late_excess_amt as amt4,ext_total_pay_amt as amt5,ext_balance_amt as amt6,ext_total_saving_amt as amt7,ext_day_late as d_late from jpa_jbb_extension where ext_applcn_no='" + cc_no + "'", con);
            grid_cmd = new SqlCommand("select jno_applcn_no as app_no,jno_pay_date as p_dt, FORMAT(jno_actual_pay_date,'dd/MM/yyyy', 'en-us') as ap_dt,cast(jno_pay_amt as decimal(10,2)) as p_amt,ISNULL(jno_actual_pay_amt,'') as ap_amt,0.00 as prinsipal,0.00 as keuntungan,ISNULL(jno_approve_amt,'') as a_amt,ISNULL(jno_profit_amt,'') as pro_amt,jno_seq_no as seq_no,jno_late_excess_amt as amt1, jno_saving_amt as sa_amt,jno_actual_saving_amt as amt3,jno_saving_late_excess_amt as amt4,jno_total_pay_amt as amt5,jno_balance_amt as bal_amt,jno_total_saving_amt as ts_amt,jno_day_late as d_late from jpa_jbb_normal where jno_applcn_no='" + cc_no + "'", con);
        }


        SqlDataAdapter da = new SqlDataAdapter(grid_cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {

            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gvSelected.DataSource = ds;
            gvSelected.DataBind();
            int columncount = gvSelected.Rows[0].Cells.Count;
            gvSelected.Rows[0].Cells.Clear();
            gvSelected.Rows[0].Cells.Add(new TableCell());
            gvSelected.Rows[0].Cells[0].ColumnSpan = columncount;
            gvSelected.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
            gvSelected.FooterRow.Cells[0].Text = "<strong>JUMLAH (RM)</strong>";
            gvSelected.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Center;
        }
        else
        {

            gvSelected.DataSource = ds;
            gvSelected.DataBind();
            gvSelected.FooterRow.Cells[0].Text = "<strong>JUMLAH (RM)</strong>";
            gvSelected.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Center;

        }
        con.Close();

    }
    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSelected.PageIndex = e.NewPageIndex;
        grid();
        gvSelected.DataBind();
    }

    protected void gvEmp_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            System.Web.UI.WebControls.Label lblctrl = (System.Web.UI.WebControls.Label)e.Row.FindControl("seqno");
            if (lblctrl.Text == "1")
            {
                //Hide the Particular row
                e.Row.Visible = false;
            }


            System.Web.UI.WebControls.TextBox txt = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("bk");
            System.Web.UI.WebControls.TextBox lblamount_1 = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("Label6");
            System.Web.UI.WebControls.TextBox lblamount_2 = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("Label7");
            if (txt.Text != "")
            {
                string ss1 = "71.43";
                string ss2 = "28.57";
                lblamount_1.Text = (double.Parse(ss1) * (double.Parse(txt.Text) / 100)).ToString("C").Replace("$","").Replace("RM", "");
                lblamount_2.Text = (double.Parse(ss2) * (double.Parse(txt.Text) / 100)).ToString("C").Replace("$","").Replace("RM", "");
            }
            System.Web.UI.WebControls.Label txt_dt = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label2");
            System.Web.UI.WebControls.Label pdate_1 = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label3");
            string cdate = DateTime.Now.ToString("%M");
            decimal cdate1 = decimal.Parse(cdate);
            string apdt = string.Empty, pdate = string.Empty, pdt_1 = string.Empty;
            decimal apdt1 = 0M;


            if (pdate_1.Text != "")
            {
                DateTime dt2 = DateTime.ParseExact(txt_dt.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dt3 = DateTime.ParseExact(pdate_1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                apdt = dt2.ToString("dd/MM/yyyy");


                DateTime date = DateTime.Now;
                //DateTime txtMyDate = DateTime.Parse(pdate_1.Text);
                if (dt3 < date)
                {
                    txt.Attributes.Add("readonly", "readonly");
                }
                else
                {
                    txt.Attributes.Remove("readonly");
                }

            }

            if (apdt != "01/01/1900")
            {
                txt.Attributes.Add("readonly", "readonly");
            }




        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            
        }

    }

    protected void btn_click(object sender, EventArgs e)
    {
        try
        {
            if (Applcn_no.Text != "")
            {
                if (DropDownList1.SelectedValue != "" && Textarea1.Value != "")
                {

                    var ic_count = Applcn_no.Text.Length;
                    DataTable app_icno = new DataTable();
                    app_icno = DBCon.Ora_Execute_table("select app_applcn_no from jpa_application JA where  '" + Applcn_no.Text + "' IN(JA.app_applcn_no, JA.app_new_icno)");
                    if (ic_count == 12)
                    {
                        cc_no = app_icno.Rows[0]["app_applcn_no"].ToString();
                    }
                    else
                    {
                        cc_no = Applcn_no.Text;
                    }

                    DataTable ddokdicno = new DataTable();
                    ddokdicno = Dblog.Ora_Execute_table("SELECT * FROM jpa_pjs_application where apj_applcn_no='" + cc_no + "'");
                    if (ddokdicno.Rows.Count > 0)
                    {
                        Dblog.Execute_CommamdText("UPDATE jpa_pjs_application SET apj_apply_sts_cd='" + DropDownList1.SelectedValue + "',apj_apply_remark='" + Textarea1.Value + "',apj_upd_id='" + Session["New"].ToString() + "',apj_upd_dt='" + DateTime.Now + "' WHERE apj_applcn_no='" + cc_no + "'");
                    }
                    else
                    {
                        Dblog.Execute_CommamdText("INSERT INTO jpa_pjs_application (apj_applcn_no,apj_apply_sts_cd,apj_apply_remark,apj_crt_id,apj_crt_dt) VALUES ('" + cc_no + "','" + DropDownList1.SelectedValue + "','" + Textarea1.Value + "','" + Session["New"].ToString() + "','" + DateTime.Now + "')");
                    }
                    foreach (GridViewRow gvrow1 in gvSelected.Rows)
                    {
                        counter = counter + 1;

                    }

                    if (counter > 0)
                    {
                        DataTable ch_query = new DataTable();
                        ch_query = Dblog.Ora_Execute_table("SELECT * FROM jpa_jbb_temp_pjs where tpj_applcn_no='" + cc_no + "'");
                        if (ch_query.Rows.Count > 0)
                        {
                            Dblog.Execute_CommamdText("Delete from jpa_jbb_temp_pjs where tpj_applcn_no='" + cc_no + "'");
                        }
                        foreach (GridViewRow gvrow in gvSelected.Rows)
                        {
                            var v1 = (System.Web.UI.WebControls.Label)gvrow.FindControl("Label3");
                            DateTime dt2 = DateTime.ParseExact(v1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            DateTime billrunDate = Convert.ToDateTime(dt2);
                            string cpay_dt = billrunDate.ToString("%M");
                            string paydate = dt2.ToString("yyyy-MM-dd");

                            var v1_2 = (System.Web.UI.WebControls.Label)gvrow.FindControl("Label2");
                            DateTime dt_2 = DateTime.ParseExact(v1_2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            string apay_date = dt_2.ToString("yyyy-MM-dd");

                            string c_dt1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            DateTime billrunDate1 = Convert.ToDateTime(c_dt1);
                            string c_dt = billrunDate1.ToString("%M");
                            //if (cpay_dt.ToString() == c_dt.ToString())
                            //{
                            var a_no = (System.Web.UI.WebControls.Label)gvrow.FindControl("app_no");
                            var sno = (System.Web.UI.WebControls.Label)gvrow.FindControl("seqno");
                            var pamt = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("bk");
                            var sav_amt = (System.Web.UI.WebControls.Label)gvrow.FindControl("sa_amt");
                            var tsav_amt = (System.Web.UI.WebControls.Label)gvrow.FindControl("tsa_amt");
                            var bamt = (System.Web.UI.WebControls.Label)gvrow.FindControl("b_amt");
                            var v8 = (System.Web.UI.WebControls.Label)gvrow.FindControl("am1");

                            var amt_1 = (System.Web.UI.WebControls.Label)gvrow.FindControl("apamt");
                            var amt_2 = (System.Web.UI.WebControls.Label)gvrow.FindControl("leamt");
                            var amt_3 = (System.Web.UI.WebControls.Label)gvrow.FindControl("asaamt");
                            var amt_4 = (System.Web.UI.WebControls.Label)gvrow.FindControl("sleamt");
                            var amt_5 = (System.Web.UI.WebControls.Label)gvrow.FindControl("tpayamt");
                            string pay_amt = string.Empty;
                            string apay_amt = string.Empty;
                            string pri_amt = string.Empty;
                            string dpro_amt = string.Empty;
                            //string apay_date = string.Empty;
                            string val1 = "71.43";
                            string val2 = "28.57";

                            string lamt = string.Empty;
                            if (sno.Text == "1")
                            {
                                lamt = txtjumla.Text;
                                pay_amt = "0.00";
                                apay_amt = "0.00";
                                pri_amt = "0.00";
                                dpro_amt = "0.00";
                            }
                            else
                            {
                                lamt = "0.00";
                                pay_amt = pamt.Text;
                                apay_amt = amt_1.Text;
                                pri_amt = ((double.Parse(pay_amt) * double.Parse(val1)) / 100).ToString("C").Replace("$","").Replace("RM", "");
                                dpro_amt = ((double.Parse(pay_amt) * double.Parse(val2)) / 100).ToString("C").Replace("$","").Replace("RM", "");
                            }
                          
                            Dblog.Execute_CommamdText("INSERT INTO jpa_jbb_temp_pjs (tpj_applcn_no,tpj_pay_date,tpj_loan_amt,tpj_seq_no,tpj_approve_amt,tpj_profit_amt,tpj_actual_pay_date,tpj_pay_amt,tpj_saving_amt,tpj_actual_pay_amt,tpj_late_excess_amt,tpj_actual_saving_amt,tpj_saving_late_excess_amt,tpj_total_pay_amt,tpj_balance_amt,tpj_total_saving_amt,tpj_day_late,tpj_principal_amt,tpj_daily_profit_amt,tpj_crt_id,tpj_crt_dt,set_flag) VALUES ('" + a_no.Text + "','" + paydate + "','" + lamt + "','" + sno.Text + "','" + capp_amt.Text + "','" + cpro_amt.Text + "','" + apay_date + "','" + pay_amt + "','" + sav_amt.Text + "','" + apay_amt + "','" + amt_2.Text + "','" + amt_3.Text + "','" + amt_4.Text + "','" + amt_5.Text + "','" + bamt.Text + "','" + tsav_amt.Text + "','0','" + pri_amt + "','" + dpro_amt + "','" + Session["New"].ToString() + "','" + DateTime.Now + "','1')");
                          
                        }
                        Button6.Visible = false;
                        Button1.Visible = true;
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Issue',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
        
    }

    protected void clk_Bercagar(object sender, EventArgs e)
    {
        if (Applcn_no.Text != "")
        {
            var ic_count = Applcn_no.Text.Length;
            DataTable app_icno = new DataTable();
            app_icno = DBCon.Ora_Execute_table("select app_applcn_no from jpa_application JA where JA.app_new_icno= '" + Applcn_no.Text + "'");
            if (ic_count == 12)
            {
                cc_no = app_icno.Rows[0]["app_applcn_no"].ToString();
            }
            else
            {
                cc_no = Applcn_no.Text;
            }
            Response.Redirect("../Pelaburan_Anggota/PP_Guaman_cagaran.aspx?spi_icno=" + cc_no);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila mesti Masukkan Kata-kata',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
        grid();
    }

    protected void clk_Penjamin(object sender, EventArgs e)
    {
        if (Applcn_no.Text != "")
        {
            var ic_count = Applcn_no.Text.Length;
            DataTable app_icno = new DataTable();
            app_icno = DBCon.Ora_Execute_table("select app_applcn_no from jpa_application JA where JA.app_new_icno= '" + Applcn_no.Text + "'");
            if (ic_count == 12)
            {
                cc_no = app_icno.Rows[0]["app_applcn_no"].ToString();
            }
            else
            {
                cc_no = Applcn_no.Text;
            }
            Response.Redirect("../Pelaburan_Anggota/PP_Guaman_penjamin.aspx?spi_icno=" + cc_no);
        }
        else
        {
            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila mesti Masukkan Kata-kata',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
        grid();
    }

    protected void click_print(object sender, EventArgs e)
    {
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
                        //sav_amt = ((0.1 / 100) * double.Parse(a1)).ToString("C").Replace("$","").Replace("RM", "");
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
                   
                    dt = Dblog.Ora_Execute_table("select * from (SELECT jn.tpj_applcn_no as tpj_applcn_no,FORMAT(jn.tpj_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.tpj_loan_amt,'')) when '0.00' then '' else ISNULL(jn.tpj_loan_amt,'') end as l_amt,jn.tpj_seq_no as seq_no,ISNULL(jn.tpj_approve_amt,'') as a_amt,ISNULL(jn.tpj_profit_amt,'') as pr_amt,ISNULL(case when CONVERT(DATE, jn.tpj_actual_pay_date) = '1900-01-01' then '' else CONVERT(CHAR(10), FORMAT(jn.tpj_actual_pay_date,'dd/MM/yyyy', 'en-us'), 103) END, '') as ap_dt,ISNULL(jn.tpj_pay_amt,'') as p_amt,ISNULL(jn.tpj_actual_pay_amt,'') as ap_amt,ISNULL(jn.tpj_late_excess_amt,'') as le_amt,ISNULL(jn.tpj_saving_amt,'') as sa_amt,ISNULL(jn.tpj_actual_saving_amt,'') as as_amt,ISNULL(jn.tpj_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.tpj_total_pay_amt,'') as tp_amt,ISNULL(jn.tpj_balance_amt,'') as bal_amt,ISNULL(jn.tpj_total_saving_amt,'') as ts_amt,jn.tpj_day_late as dlate,ISNULL(jn.tpj_principal_amt,'') as priamt,ISNULL(jn.tpj_daily_profit_amt,'') as dproamt FROM jpa_jbb_temp_pjs  as jn where jn.tpj_applcn_no='" + cc_no + "' ) as a full outer join (select tpj_applcn_no,sum(tpj_pay_amt) as tot_pamt,sum(tpj_actual_pay_amt) as tot_apamt,sum(tpj_saving_amt) as tot_samt,sum(tpj_actual_saving_amt) as tot_asamt,sum(tpj_total_pay_amt) as tot_tpamt,sum(tpj_principal_amt) as tot_priamt,sum(tpj_daily_profit_amt) as tot_dproamt from jpa_jbb_temp_pjs where tpj_applcn_no='" + cc_no + "' group by tpj_applcn_no) as b on b.tpj_applcn_no=a.tpj_applcn_no full outer join (select * from jpa_calculate_fee as jc where jc.cal_applcn_no='" + cc_no + "') as c on c.cal_applcn_no=b.tpj_applcn_no full outer join (select ja.app_cumm_saving_amt as acs_amt,ja.app_applcn_no as app_no,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno,rjp.Description from jpa_application as ja left join Ref_Jenis_Pelaburan as rjp on rjp.Description_Code=ja.app_loan_type_cd where ja.app_applcn_no = '" + cc_no + "') as d on d.app_no=c.cal_applcn_no");
                   

                    Rptviwer_lt.Reset();
                    ds.Tables.Add(dt);

                    Rptviwer_lt.LocalReport.DataSources.Clear();

                    Rptviwer_lt.LocalReport.ReportPath = "Pelaburan_Anggota/permohonan_pjs.rdlc";
                    ReportDataSource rds = new ReportDataSource("p_pjs", dt);

                    ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("samt",sav_amt)

                     };


                    Rptviwer_lt.LocalReport.SetParameters(rptParams);

                    Rptviwer_lt.LocalReport.DataSources.Add(rds);

                    //Refresh
                    Rptviwer_lt.LocalReport.Refresh();
                    Warning[] warnings;

                    string[] streamids;

                    string mimeType;

                    string encoding;

                    string extension;

                    string fname = DateTime.Now.ToString("dd_MM_yyyy");

                    string devinfo = "   <DeviceInfo><ColorDepth>32</ColorDepth><DpiX>350</DpiX><DpiY>350</DpiY><OutputFormat>PDF</OutputFormat>" +
                                    "   <PageWidth>12.20in</PageWidth>" +
                                    "   <PageHeight>8.27in</PageHeight>" +
                                    "   <MarginTop>0.1in</MarginTop>" +
                                    "   <MarginLeft>0.5in</MarginLeft>" +
                                    "   <MarginRight>0in</MarginRight>" +
                                    "   <MarginBottom>0in</MarginBottom>" +
                                    "   </DeviceInfo>";

                    byte[] bytes = Rptviwer_lt.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);

                    Response.Buffer = true;

                    Response.Clear();

                    Response.ClearHeaders();

                    Response.ClearContent();

                    Response.ContentType = "application/pdf";

                    Response.AddHeader("content-disposition", "attachment; filename=PERMOHONAN_PJS_" + cc_no + "." + extension);

                    Response.BinaryWrite(bytes);

                    Response.Flush();

                    //Response.End();
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
                grid();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    protected void click_jbb(object sender, EventArgs e)
    {

        try
        {
            if (Applcn_no.Text != "")
            {
                string url = "../Pelaburan_Anggota/semak_jbb.aspx?jbb_icno=" + Applcn_no.Text;

                string s = "window.open('" + url + "', 'popup_window', 'width=11000,height=700,left=100,top=100,resizable=yes');";

                ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Ic OR NO KP Baru',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
            grid();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void btn_rstclick(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void btn_rstclick1(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void clk_bak(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Pelaburan_Anggota/PP_M_Pjs_view.aspx");
    }


}