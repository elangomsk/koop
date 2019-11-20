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

public partial class semak_jbb : System.Web.UI.Page
{
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string str;
    DBConnection DBCon = new DBConnection();
    string level, userid;
    string cc_no = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        var samp = Request.Url.Query;
        string icno = string.Empty;
        if (samp != "")
        {
            icno = Request.QueryString["jbb_icno"].ToString();
           
        }
        try
        {
            if (icno != "")
            {
                var ic_count = icno.Length;
                //Path var ic_count = Applcn_no.Text.Length;
                DataTable app_icno = new DataTable();
                app_icno = DBCon.Ora_Execute_table("select app_applcn_no,isnull(app_current_jbb_ind,'') AS app_current_jbb_ind,app_loan_type_cd,app_loan_amt from jpa_application JA where '" + icno + "' IN(JA.app_applcn_no, JA.app_new_icno)");
                if (ic_count == 12)
                {
                    cc_no = app_icno.Rows[0]["app_applcn_no"].ToString();
                }
                else
                {
                    cc_no = icno;
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
                        //dt = Dblog.Ora_Execute_table("select * from (SELECT jn.jwo_applcn_no as app_no,FORMAT(jn.jwo_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.jwo_loan_amt,'')) when '0.00' then '' else ISNULL(jn.jwo_loan_amt,'') end as l_amt,jn.jwo_seq_no as seq_no,ISNULL(jn.jwo_approve_amt,'') as a_amt,ISNULL(jn.jwo_profit_amt,'') as pr_amt,ISNULL(case when CONVERT(DATE, jn.jwo_actual_pay_date) = '1900-01-01' then '' else CONVERT(CHAR(10), FORMAT(jn.jwo_actual_pay_date,'dd/MM/yyyy', 'en-us'), 103) END, '') as ap_dt,ISNULL(jn.jwo_pay_amt,'') as p_amt,ISNULL(jn.jwo_actual_pay_amt,'') as ap_amt,ISNULL(jn.jwo_late_excess_amt,'') as le_amt,ISNULL(jn.jwo_saving_amt,'') as sa_amt,ISNULL(jn.jwo_actual_saving_amt,'') as as_amt,ISNULL(jn.jwo_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.jwo_total_pay_amt,'') as tp_amt,ISNULL(jn.jwo_balance_amt,'') as bal_amt,ISNULL(jn.jwo_total_saving_amt,'') as ts_amt,jn.jwo_day_late as dlate,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno,rjp.Description FROM jpa_jbb_writeoff  as jn left join jpa_application as ja on ja.app_applcn_no = jn.jwo_applcn_no left join Ref_Jenis_Pelaburan as rjp on rjp.Description_Code=ja.app_loan_type_cd where jn.jwo_applcn_no='" + cc_no + "' ) as a full outer join (select jwo_applcn_no,sum(jwo_pay_amt) as tot_pamt,sum(jwo_actual_pay_amt) as tot_apamt,sum(jwo_saving_amt) as tot_samt,sum(jwo_actual_saving_amt) as tot_asamt,sum(jwo_total_pay_amt) as tot_tpamt from jpa_jbb_writeoff where jwo_applcn_no='" + cc_no + "' group by jwo_applcn_no) as b on b.jwo_applcn_no=a.app_no full outer join (select * from jpa_calculate_fee where cal_applcn_no='" + cc_no + "') as c on c.cal_applcn_no=b.jwo_applcn_no");
                        dt = Dblog.Ora_Execute_table("select * from (SELECT jn.jwo_applcn_no as jwo_applcn_no,FORMAT(jn.jwo_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.jwo_loan_amt,'')) when '0.00' then '' else ISNULL(jn.jwo_loan_amt,'') end as l_amt,jn.jwo_seq_no as seq_no,ISNULL(jn.jwo_approve_amt,'') as a_amt,ISNULL(jn.jwo_profit_amt,'') as pr_amt,ISNULL(case when CONVERT(DATE, jn.jwo_actual_pay_date) = '1900-01-01' then '' else CONVERT(CHAR(10), FORMAT(jn.jwo_actual_pay_date,'dd/MM/yyyy', 'en-us'), 103) END, '') as ap_dt,ISNULL(jn.jwo_pay_amt,'') as p_amt,ISNULL(jn.jwo_actual_pay_amt,'') as ap_amt,ISNULL(jn.jwo_late_excess_amt,'') as le_amt,ISNULL(jn.jwo_saving_amt,'') as sa_amt,ISNULL(jn.jwo_actual_saving_amt,'') as as_amt,ISNULL(jn.jwo_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.jwo_total_pay_amt,'') as tp_amt,ISNULL(jn.jwo_balance_amt,'') as bal_amt,ISNULL(jn.jwo_total_saving_amt,'') as ts_amt,jn.jwo_day_late as dlate FROM jpa_jbb_writeoff  as jn where jn.jwo_applcn_no='" + cc_no + "' ) as a full outer join (select jwo_applcn_no,sum(jwo_pay_amt) as tot_pamt,sum(jwo_actual_pay_amt) as tot_apamt,sum(jwo_saving_amt) as tot_samt,sum(jwo_actual_saving_amt) as tot_asamt,sum(jwo_total_pay_amt) as tot_tpamt from jpa_jbb_writeoff where jwo_applcn_no='" + cc_no + "' group by jwo_applcn_no) as b on b.jwo_applcn_no=a.jwo_applcn_no full outer join (select * from jpa_calculate_fee as jc where jc.cal_applcn_no='" + cc_no + "') as c on c.cal_applcn_no=b.jwo_applcn_no full outer join (select ja.app_cumm_saving_amt as acs_amt,ja.app_applcn_no as app_no,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno,rjp.Description from jpa_application as ja left join Ref_Jenis_Pelaburan as rjp on rjp.Description_Code=ja.app_loan_type_cd where ja.app_applcn_no = '" + cc_no + "') as d on d.app_no=c.cal_applcn_no");
                    }
                    else if (c_jbb == "H")
                    {
                        //dt = Dblog.Ora_Execute_table("select * from (SELECT jn.hol_applcn_no as app_no,FORMAT(jn.hol_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.hol_loan_amt,'')) when '0.00' then '' else ISNULL(jn.hol_loan_amt,'') end as l_amt,jn.hol_seq_no as seq_no,ISNULL(jn.hol_approve_amt,'') as a_amt,ISNULL(jn.hol_profit_amt,'') as pr_amt,ISNULL(case when CONVERT(DATE, jn.hol_actual_pay_date) = '1900-01-01' then '' else CONVERT(CHAR(10), FORMAT(jn.hol_actual_pay_date,'dd/MM/yyyy', 'en-us'), 103) END, '') as ap_dt,ISNULL(jn.hol_pay_amt,'') as p_amt,ISNULL(jn.hol_actual_pay_amt,'') as ap_amt,ISNULL(jn.hol_late_excess_amt,'') as le_amt,ISNULL(jn.hol_saving_amt,'') as sa_amt,ISNULL(jn.hol_actual_saving_amt,'') as as_amt,ISNULL(jn.hol_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.hol_total_pay_amt,'') as tp_amt,ISNULL(jn.hol_balance_amt,'') as bal_amt,ISNULL(jn.hol_total_saving_amt,'') as ts_amt,jn.hol_day_late as dlate,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno,rjp.Description FROM jpa_jbb_holiday  as jn left join jpa_application as ja on ja.app_applcn_no = jn.hol_applcn_no left join Ref_Jenis_Pelaburan as rjp on rjp.Description_Code=ja.app_loan_type_cd where jn.hol_applcn_no='" + cc_no + "' ) as a full outer join (select hol_applcn_no,sum(hol_pay_amt) as tot_pamt,sum(hol_actual_pay_amt) as tot_apamt,sum(hol_saving_amt) as tot_samt,sum(hol_actual_saving_amt) as tot_asamt,sum(hol_total_pay_amt) as tot_tpamt from jpa_jbb_holiday where hol_applcn_no='" + cc_no + "' group by hol_applcn_no) as b on b.hol_applcn_no=a.app_no full outer join (select * from jpa_calculate_fee where cal_applcn_no='" + cc_no + "') as c on c.cal_applcn_no=b.hol_applcn_no");
                        dt = Dblog.Ora_Execute_table("select * from (SELECT jn.hol_applcn_no as hol_applcn_no,FORMAT(jn.hol_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.hol_loan_amt,'')) when '0.00' then '' else ISNULL(jn.hol_loan_amt,'') end as l_amt,jn.hol_seq_no as seq_no,ISNULL(jn.hol_approve_amt,'') as a_amt,ISNULL(jn.hol_profit_amt,'') as pr_amt,ISNULL(case when CONVERT(DATE, jn.hol_actual_pay_date) = '1900-01-01' then '' else CONVERT(CHAR(10), FORMAT(jn.hol_actual_pay_date,'dd/MM/yyyy', 'en-us'), 103) END, '') as ap_dt,ISNULL(jn.hol_pay_amt,'') as p_amt,ISNULL(jn.hol_actual_pay_amt,'') as ap_amt,ISNULL(jn.hol_late_excess_amt,'') as le_amt,ISNULL(jn.hol_saving_amt,'') as sa_amt,ISNULL(jn.hol_actual_saving_amt,'') as as_amt,ISNULL(jn.hol_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.hol_total_pay_amt,'') as tp_amt,ISNULL(jn.hol_balance_amt,'') as bal_amt,ISNULL(jn.hol_total_saving_amt,'') as ts_amt,jn.hol_day_late as dlate FROM jpa_jbb_holiday  as jn where jn.hol_applcn_no='" + cc_no + "' ) as a full outer join (select hol_applcn_no,sum(hol_pay_amt) as tot_pamt,sum(hol_actual_pay_amt) as tot_apamt,sum(hol_saving_amt) as tot_samt,sum(hol_actual_saving_amt) as tot_asamt,sum(hol_total_pay_amt) as tot_tpamt from jpa_jbb_holiday where hol_applcn_no='" + cc_no + "' group by hol_applcn_no) as b on b.hol_applcn_no=a.hol_applcn_no full outer join (select * from jpa_calculate_fee as jc where jc.cal_applcn_no='" + cc_no + "') as c on c.cal_applcn_no=b.hol_applcn_no full outer join (select ja.app_cumm_saving_amt as acs_amt,ja.app_applcn_no as app_no,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno,rjp.Description from jpa_application as ja left join Ref_Jenis_Pelaburan as rjp on rjp.Description_Code=ja.app_loan_type_cd where ja.app_applcn_no = '" + cc_no + "') as d on d.app_no=c.cal_applcn_no");
                    }
                    else if (c_jbb == "P")
                    {
                        //dt = Dblog.Ora_Execute_table("select * from (SELECT jn.pjs_applcn_no as app_no,FORMAT(jn.pjs_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.pjs_loan_amt,'')) when '0.00' then '' else ISNULL(jn.pjs_loan_amt,'') end as l_amt,jn.pjs_seq_no as seq_no,ISNULL(jn.pjs_approve_amt,'') as a_amt,ISNULL(jn.pjs_profit_amt,'') as pr_amt,ISNULL(case when CONVERT(DATE, jn.pjs_actual_pay_date) = '1900-01-01' then '' else CONVERT(CHAR(10), FORMAT(jn.pjs_actual_pay_date,'dd/MM/yyyy', 'en-us'), 103) END, '') as ap_dt,ISNULL(jn.pjs_pay_amt,'') as p_amt,ISNULL(jn.pjs_actual_pay_amt,'') as ap_amt,ISNULL(jn.pjs_late_excess_amt,'') as le_amt,ISNULL(jn.pjs_saving_amt,'') as sa_amt,ISNULL(jn.pjs_actual_saving_amt,'') as as_amt,ISNULL(jn.pjs_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.pjs_total_pay_amt,'') as tp_amt,ISNULL(jn.pjs_balance_amt,'') as bal_amt,ISNULL(jn.pjs_total_saving_amt,'') as ts_amt,jn.pjs_day_late as dlate,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno,rjp.Description FROM jpa_jbb_pjs  as jn left join jpa_application as ja on ja.app_applcn_no = jn.pjs_applcn_no left join Ref_Jenis_Pelaburan as rjp on rjp.Description_Code=ja.app_loan_type_cd where jn.pjs_applcn_no='" + cc_no + "' ) as a full outer join (select pjs_applcn_no,sum(pjs_pay_amt) as tot_pamt,sum(pjs_actual_pay_amt) as tot_apamt,sum(pjs_saving_amt) as tot_samt,sum(pjs_actual_saving_amt) as tot_asamt,sum(pjs_total_pay_amt) as tot_tpamt from jpa_jbb_pjs where pjs_applcn_no='" + cc_no + "' group by pjs_applcn_no) as b on b.pjs_applcn_no=a.app_no full outer join (select * from jpa_calculate_fee where cal_applcn_no='" + cc_no + "') as c on c.cal_applcn_no=b.pjs_applcn_no");
                        dt = Dblog.Ora_Execute_table("select * from (SELECT jn.pjs_applcn_no as pjs_applcn_no,FORMAT(jn.pjs_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.pjs_loan_amt,'')) when '0.00' then '' else ISNULL(jn.pjs_loan_amt,'') end as l_amt,jn.pjs_seq_no as seq_no,ISNULL(jn.pjs_approve_amt,'') as a_amt,ISNULL(jn.pjs_profit_amt,'') as pr_amt,ISNULL(case when CONVERT(DATE, jn.pjs_actual_pay_date) = '1900-01-01' then '' else CONVERT(CHAR(10), FORMAT(jn.pjs_actual_pay_date,'dd/MM/yyyy', 'en-us'), 103) END, '') as ap_dt,ISNULL(jn.pjs_pay_amt,'') as p_amt,ISNULL(jn.pjs_actual_pay_amt,'') as ap_amt,ISNULL(jn.pjs_late_excess_amt,'') as le_amt,ISNULL(jn.pjs_saving_amt,'') as sa_amt,ISNULL(jn.pjs_actual_saving_amt,'') as as_amt,ISNULL(jn.pjs_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.pjs_total_pay_amt,'') as tp_amt,ISNULL(jn.pjs_balance_amt,'') as bal_amt,ISNULL(jn.pjs_total_saving_amt,'') as ts_amt,jn.pjs_day_late as dlate FROM jpa_jbb_pjs  as jn where jn.pjs_applcn_no='" + cc_no + "' ) as a full outer join (select pjs_applcn_no,sum(pjs_pay_amt) as tot_pamt,sum(pjs_actual_pay_amt) as tot_apamt,sum(pjs_saving_amt) as tot_samt,sum(pjs_actual_saving_amt) as tot_asamt,sum(pjs_total_pay_amt) as tot_tpamt from jpa_jbb_pjs where pjs_applcn_no='" + cc_no + "' group by pjs_applcn_no) as b on b.pjs_applcn_no=a.pjs_applcn_no full outer join (select * from jpa_calculate_fee as jc where jc.cal_applcn_no='" + cc_no + "') as c on c.cal_applcn_no=b.pjs_applcn_no full outer join (select ja.app_cumm_saving_amt as acs_amt,ja.app_applcn_no as app_no,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno,rjp.Description from jpa_application as ja left join Ref_Jenis_Pelaburan as rjp on rjp.Description_Code=ja.app_loan_type_cd where ja.app_applcn_no = '" + cc_no + "') as d on d.app_no=c.cal_applcn_no");
                    }
                    else if (c_jbb == "E")
                    {
                        //dt = Dblog.Ora_Execute_table("select * from (SELECT jn.ext_applcn_no as app_no,FORMAT(jn.ext_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.ext_loan_amt,'')) when '0.00' then '' else ISNULL(jn.ext_loan_amt,'') end as l_amt,jn.ext_seq_no as seq_no,ISNULL(jn.ext_approve_amt,'') as a_amt,ISNULL(jn.ext_profit_amt,'') as pr_amt,ISNULL(case when CONVERT(DATE, jn.ext_actual_pay_date) = '1900-01-01' then '' else CONVERT(CHAR(10), FORMAT(jn.ext_actual_pay_date,'dd/MM/yyyy', 'en-us'), 103) END, '') as ap_dt,ISNULL(jn.ext_pay_amt,'') as p_amt,ISNULL(jn.ext_actual_pay_amt,'') as ap_amt,ISNULL(jn.ext_late_excess_amt,'') as le_amt,ISNULL(jn.ext_saving_amt,'') as sa_amt,ISNULL(jn.ext_actual_saving_amt,'') as as_amt,ISNULL(jn.ext_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.ext_total_pay_amt,'') as tp_amt,ISNULL(jn.ext_balance_amt,'') as bal_amt,ISNULL(jn.ext_total_saving_amt,'') as ts_amt,jn.ext_day_late as dlate,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno,rjp.Description FROM jpa_jbb_extension as jn left join jpa_application as ja on ja.app_applcn_no = jn.ext_applcn_no left join Ref_Jenis_Pelaburan as rjp on rjp.Description_Code=ja.app_loan_type_cd where jn.ext_applcn_no='" + cc_no + "' ) as a full outer join (select ext_applcn_no,sum(ext_pay_amt) as tot_pamt,sum(ext_actual_pay_amt) as tot_apamt,sum(ext_saving_amt) as tot_samt,sum(ext_actual_saving_amt) as tot_asamt,sum(ext_total_pay_amt) as tot_tpamt from jpa_jbb_extension where ext_applcn_no='" + cc_no + "' group by ext_applcn_no) as b on b.ext_applcn_no=a.app_no full outer join (select * from jpa_calculate_fee where cal_applcn_no='" + cc_no + "') as c on c.cal_applcn_no=b.ext_applcn_no");
                        dt = Dblog.Ora_Execute_table("select * from (SELECT jn.ext_applcn_no as ext_applcn_no,FORMAT(jn.ext_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.ext_loan_amt,'')) when '0.00' then '' else ISNULL(jn.ext_loan_amt,'') end as l_amt,jn.ext_seq_no as seq_no,ISNULL(jn.ext_approve_amt,'') as a_amt,ISNULL(jn.ext_profit_amt,'') as pr_amt,ISNULL(case when CONVERT(DATE, jn.ext_actual_pay_date) = '1900-01-01' then '' else CONVERT(CHAR(10), FORMAT(jn.ext_actual_pay_date,'dd/MM/yyyy', 'en-us'), 103) END, '') as ap_dt,ISNULL(jn.ext_pay_amt,'') as p_amt,ISNULL(jn.ext_actual_pay_amt,'') as ap_amt,ISNULL(jn.ext_late_excess_amt,'') as le_amt,ISNULL(jn.ext_saving_amt,'') as sa_amt,ISNULL(jn.ext_actual_saving_amt,'') as as_amt,ISNULL(jn.ext_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.ext_total_pay_amt,'') as tp_amt,ISNULL(jn.ext_balance_amt,'') as bal_amt,ISNULL(jn.ext_total_saving_amt,'') as ts_amt,jn.ext_day_late as dlate FROM jpa_jbb_extension  as jn where jn.ext_applcn_no='" + cc_no + "' ) as a full outer join (select ext_applcn_no,sum(ext_pay_amt) as tot_pamt,sum(ext_actual_pay_amt) as tot_apamt,sum(ext_saving_amt) as tot_samt,sum(ext_actual_saving_amt) as tot_asamt,sum(ext_total_pay_amt) as tot_tpamt from jpa_jbb_extension where ext_applcn_no='" + cc_no + "' group by ext_applcn_no) as b on b.ext_applcn_no=a.ext_applcn_no full outer join (select * from jpa_calculate_fee as jc where jc.cal_applcn_no='" + cc_no + "') as c on c.cal_applcn_no=b.ext_applcn_no full outer join (select ja.app_cumm_saving_amt as acs_amt,ja.app_applcn_no as app_no,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno,rjp.Description from jpa_application as ja left join Ref_Jenis_Pelaburan as rjp on rjp.Description_Code=ja.app_loan_type_cd where ja.app_applcn_no = '" + cc_no + "') as d on d.app_no=c.cal_applcn_no");
                    }
                    else
                    {
                        //dt = Dblog.Ora_Execute_table("select * from (SELECT jn.jno_applcn_no as app_no,FORMAT(jn.jno_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.jno_loan_amt,'')) when '0.00' then '' else ISNULL(jn.jno_loan_amt,'') end as l_amt,jn.jno_seq_no as seq_no,ISNULL(jn.jno_approve_amt,'') as a_amt,ISNULL(jn.jno_profit_amt,'') as pr_amt,ISNULL(case when CONVERT(DATE, jn.jno_actual_pay_date) = '1900-01-01' then '' else CONVERT(CHAR(10), FORMAT(jn.jno_actual_pay_date,'dd/MM/yyyy', 'en-us'), 103) END, '') as ap_dt,ISNULL(jn.jno_pay_amt,'') as p_amt,ISNULL(jn.jno_actual_pay_amt,'') as ap_amt,ISNULL(jn.jno_late_excess_amt,'') as le_amt,ISNULL(jn.jno_saving_amt,'') as sa_amt,ISNULL(jn.jno_actual_saving_amt,'') as as_amt,ISNULL(jn.jno_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.jno_total_pay_amt,'') as tp_amt,ISNULL(jn.jno_balance_amt,'') as bal_amt,ISNULL(jn.jno_total_saving_amt,'') as ts_amt,jn.jno_day_late as dlate,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno,rjp.Description FROM jpa_jbb_normal as jn left join jpa_application as ja on ja.app_applcn_no = jn.jno_applcn_no left join Ref_Jenis_Pelaburan as rjp on rjp.Description_Code=ja.app_loan_type_cd where jn.jno_applcn_no='" + cc_no + "' ) as a full outer join (select jno_applcn_no,sum(jno_pay_amt) as tot_pamt,sum(jno_actual_pay_amt) as tot_apamt,sum(jno_saving_amt) as tot_samt,sum(jno_actual_saving_amt) as tot_asamt,sum(jno_total_pay_amt) as tot_tpamt from jpa_jbb_normal where jno_applcn_no='" + cc_no + "' group by jno_applcn_no) as b on b.jno_applcn_no=a.app_no full outer join (select * from jpa_calculate_fee where cal_applcn_no='" + cc_no + "') as c on c.cal_applcn_no=b.jno_applcn_no");
                        dt = Dblog.Ora_Execute_table("select * from (SELECT jn.jno_applcn_no as jno_applcn_no,FORMAT(jn.jno_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.jno_loan_amt,'')) when '0.00' then '' else ISNULL(jn.jno_loan_amt,'') end as l_amt,jn.jno_seq_no as seq_no,ISNULL(jn.jno_approve_amt,'') as a_amt,ISNULL(jn.jno_profit_amt,'') as pr_amt,ISNULL(case when CONVERT(DATE, jn.jno_actual_pay_date) = '1900-01-01' then '' else CONVERT(CHAR(10), FORMAT(jn.jno_actual_pay_date,'dd/MM/yyyy', 'en-us'), 103) END, '') as ap_dt,ISNULL(jn.jno_pay_amt,'') as p_amt,ISNULL(jn.jno_actual_pay_amt,'') as ap_amt,ISNULL(jn.jno_late_excess_amt,'') as le_amt,ISNULL(jn.jno_saving_amt,'') as sa_amt,ISNULL(jn.jno_actual_saving_amt,'') as as_amt,ISNULL(jn.jno_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.jno_total_pay_amt,'') as tp_amt,ISNULL(jn.jno_balance_amt,'') as bal_amt,ISNULL(jn.jno_total_saving_amt,'') as ts_amt,jn.jno_day_late as dlate FROM jpa_jbb_normal  as jn where jn.jno_applcn_no='" + cc_no + "' ) as a full outer join (select jno_applcn_no,sum(jno_pay_amt) as tot_pamt,sum(jno_actual_pay_amt) as tot_apamt,sum(jno_saving_amt) as tot_samt,sum(jno_actual_saving_amt) as tot_asamt,sum(jno_total_pay_amt) as tot_tpamt from jpa_jbb_normal where jno_applcn_no='" + cc_no + "' group by jno_applcn_no) as b on b.jno_applcn_no=a.jno_applcn_no full outer join (select * from jpa_calculate_fee as jc where jc.cal_applcn_no='" + cc_no + "') as c on c.cal_applcn_no=b.jno_applcn_no full outer join (select ja.app_cumm_saving_amt as acs_amt,ja.app_applcn_no as app_no,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno,rjp.Description from jpa_application as ja left join Ref_Jenis_Pelaburan as rjp on rjp.Description_Code=ja.app_loan_type_cd where ja.app_applcn_no = '" + cc_no + "') as d on d.app_no=c.cal_applcn_no");
                    }
                }
                else
                {
                    //dt = Dblog.Ora_Execute_table("select * from (SELECT jn.jno_applcn_no as app_no,FORMAT(jn.jno_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.jno_loan_amt,'')) when '0.00' then '' else ISNULL(jn.jno_loan_amt,'') end as l_amt,jn.jno_seq_no as seq_no,ISNULL(jn.jno_approve_amt,'') as a_amt,ISNULL(jn.jno_profit_amt,'') as pr_amt,ISNULL(case when CONVERT(DATE, jn.jno_actual_pay_date) = '1900-01-01' then '' else CONVERT(CHAR(10), FORMAT(jn.jno_actual_pay_date,'dd/MM/yyyy', 'en-us'), 103) END, '') as ap_dt,ISNULL(jn.jno_pay_amt,'') as p_amt,ISNULL(jn.jno_actual_pay_amt,'') as ap_amt,ISNULL(jn.jno_late_excess_amt,'') as le_amt,ISNULL(jn.jno_saving_amt,'') as sa_amt,ISNULL(jn.jno_actual_saving_amt,'') as as_amt,ISNULL(jn.jno_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.jno_total_pay_amt,'') as tp_amt,ISNULL(jn.jno_balance_amt,'') as bal_amt,ISNULL(jn.jno_total_saving_amt,'') as ts_amt,jn.jno_day_late as dlate,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno,rjp.Description FROM jpa_jbb_normal as jn left join jpa_application as ja on ja.app_applcn_no = jn.jno_applcn_no left join Ref_Jenis_Pelaburan as rjp on rjp.Description_Code=ja.app_loan_type_cd where jn.jno_applcn_no='" + cc_no + "' ) as a full outer join (select jno_applcn_no,sum(jno_pay_amt) as tot_pamt,sum(jno_actual_pay_amt) as tot_apamt,sum(jno_saving_amt) as tot_samt,sum(jno_actual_saving_amt) as tot_asamt,sum(jno_total_pay_amt) as tot_tpamt from jpa_jbb_normal where jno_applcn_no='" + cc_no + "' group by jno_applcn_no) as b on b.jno_applcn_no=a.app_no full outer join (select * from jpa_calculate_fee where cal_applcn_no='" + cc_no + "') as c on c.cal_applcn_no=b.jno_applcn_no");
                    dt = Dblog.Ora_Execute_table("select * from (SELECT jn.jno_applcn_no as jno_applcn_no,FORMAT(jn.jno_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.jno_loan_amt,'')) when '0.00' then '' else ISNULL(jn.jno_loan_amt,'') end as l_amt,jn.jno_seq_no as seq_no,ISNULL(jn.jno_approve_amt,'') as a_amt,ISNULL(jn.jno_profit_amt,'') as pr_amt,ISNULL(case when CONVERT(DATE, jn.jno_actual_pay_date) = '1900-01-01' then '' else CONVERT(CHAR(10), FORMAT(jn.jno_actual_pay_date,'dd/MM/yyyy', 'en-us'), 103) END, '') as ap_dt,ISNULL(jn.jno_pay_amt,'') as p_amt,ISNULL(jn.jno_actual_pay_amt,'') as ap_amt,ISNULL(jn.jno_late_excess_amt,'') as le_amt,ISNULL(jn.jno_saving_amt,'') as sa_amt,ISNULL(jn.jno_actual_saving_amt,'') as as_amt,ISNULL(jn.jno_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.jno_total_pay_amt,'') as tp_amt,ISNULL(jn.jno_balance_amt,'') as bal_amt,ISNULL(jn.jno_total_saving_amt,'') as ts_amt,jn.jno_day_late as dlate FROM jpa_jbb_normal  as jn where jn.jno_applcn_no='" + cc_no + "' ) as a full outer join (select jno_applcn_no,sum(jno_pay_amt) as tot_pamt,sum(jno_actual_pay_amt) as tot_apamt,sum(jno_saving_amt) as tot_samt,sum(jno_actual_saving_amt) as tot_asamt,sum(jno_total_pay_amt) as tot_tpamt from jpa_jbb_normal where jno_applcn_no='" + cc_no + "' group by jno_applcn_no) as b on b.jno_applcn_no=a.jno_applcn_no full outer join (select * from jpa_calculate_fee as jc where jc.cal_applcn_no='" + cc_no + "') as c on c.cal_applcn_no=b.jno_applcn_no full outer join (select ja.app_cumm_saving_amt as acs_amt,ja.app_applcn_no as app_no,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno,rjp.Description from jpa_application as ja left join Ref_Jenis_Pelaburan as rjp on rjp.Description_Code=ja.app_loan_type_cd where ja.app_applcn_no = '" + cc_no + "') as d on d.app_no=c.cal_applcn_no");
                }

                Rptviwer_cetakjbb.Reset();
                ds.Tables.Add(dt);

                Rptviwer_cetakjbb.LocalReport.DataSources.Clear();

                Rptviwer_cetakjbb.LocalReport.ReportPath = "Pelaburan_Anggota/s_jbb.rdlc";
                ReportDataSource rds = new ReportDataSource("sjbb", dt);

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
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "inline; filename=JBB_" + fname + "." + extension);
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Medan Input Adalah Mandatori.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
            //Request.Redirect(url, false);
        }
    }
}