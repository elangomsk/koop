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

public partial class PP_M_Tangguh : System.Web.UI.Page
{
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string str;
    DBConnection DBCon = new DBConnection();
    SqlCommand com;
    DataTable dt = new DataTable();
    StudentWebService service = new StudentWebService();
    string Status = string.Empty;
    string level, userid;
    string cc_no = string.Empty, dpay_4 = string.Empty, pdate_4ap = string.Empty;
    String date1 = String.Empty;
    String date2 = String.Empty;
    String date3 = String.Empty;
    String date4 = String.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        //scriptManager.RegisterPostBackControl(this.Button7);
        scriptManager.RegisterPostBackControl(this.Button1);
        scriptManager.RegisterPostBackControl(this.Button7);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                level = Session["level"].ToString();
                userid = Session["New"].ToString();
                TextBox7.Attributes.Add("Readonly", "Readonly");
                TextBox10.Attributes.Add("Readonly", "Readonly");
                TextBox11.Attributes.Add("Readonly", "Readonly");
                TextBox12.Attributes.Add("Readonly", "Readonly");
                Box7_edate.Attributes.Add("Readonly", "Readonly");
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

                TextBox1.Attributes.Add("Readonly", "Readonly");
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
                    Button2.Text = "Kemaskini";
                    Button9.Visible = false;
                    Button10.Visible = false;
                    Button3.Visible = false;
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
                    select_app = DBCon.Ora_Execute_table("select a.*,ISNULL(b.hol_applcn_no,'') as hol_applcn_no,b.hol_dur,b.hol_start_dt,b.hol_start_pay_dt,b.hol_end_dt,b.hol_end_pay_dt,b.hol_appl_sts_cd,b.hol_remark from (select JA.app_applcn_no,JA.app_new_icno,JA.app_end_pay_dt,JA.app_name,RW.Wilayah_Name,ISNULL(RB.branch_desc, '') AS branch_desc,JA.app_loan_amt,JA.appl_loan_dur,JA.app_permnt_address,ja.app_permnt_postcode,ja.app_permnt_state_cd,ja.app_phone_h,ja.app_phone_m,ja.app_phone_o,ja.app_mailing_address,JA.app_mailing_postcode,ja.app_mailing_state_cd,ISNULL(JA.app_cumm_installment_amt,'') as app_cumm_installment_amt,ISNULL(JA.app_cumm_pay_amt,'') as app_cumm_pay_amt,ISNULL(JA.app_backdated_amt,'') as app_backdated_amt,ISNULL(JA.app_cumm_profit_amt,'') as app_cumm_profit_amt,ISNULL(JA.app_cumm_saving_amt,'') as app_cumm_saving_amt,ISNULL(JA.app_bal_loan_amt,'') as app_bal_loan_amt from jpa_application as JA  Left Join jpa_calculate_fee as JC ON JC.cal_applcn_no=JA.app_applcn_no Left Join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd Left join ref_branch AS RB ON RB.branch_cd=JA.app_branch_cd where JA.app_applcn_no = '" + ddicno.Rows[0]["app_applcn_no"] + "') as a full outer join (select * from jpa_payment_holiday  where hol_applcn_no='" + ddicno.Rows[0]["app_applcn_no"] + "') as b on a.app_applcn_no=b.hol_applcn_no");

                    DataTable select_normal = new DataTable();
                    select_normal = DBCon.Ora_Execute_table("select top(1) FORMAT(jn.jno_pay_date,'dd/MM/yyyy', 'en-us') as pdate from jpa_jbb_normal as jn where jno_applcn_no='" + ddicno.Rows[0]["app_applcn_no"] + "' order by jno_pay_date desc");

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
                    TextBox1.Text = a6.ToString("C").Replace("$", "").Replace("RM", "");
                    if (select_app.Rows[0]["app_end_pay_dt"].ToString() != "")
                    {
                        Box7_edate.Text = Convert.ToDateTime(select_app.Rows[0]["app_end_pay_dt"]).ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        Box7_edate.Text = "";
                    }
                    TextBox12.Text = select_normal.Rows[0]["pdate"].ToString();
                    if (select_app.Rows[0]["hol_applcn_no"].ToString().Trim() != "")
                    {
                        TextBox5.Text = select_app.Rows[0]["hol_dur"].ToString();
                        TextBox7.Text = Convert.ToDateTime(select_app.Rows[0]["hol_start_dt"]).ToString("dd/MM/yyyy");
                        TextBox10.Text = Convert.ToDateTime(select_app.Rows[0]["hol_end_dt"]).ToString("dd/MM/yyyy");
                        TextBox11.Text = Convert.ToDateTime(select_app.Rows[0]["hol_start_pay_dt"]).ToString("dd/MM/yyyy");
                        TextBox12.Text = Convert.ToDateTime(select_app.Rows[0]["hol_end_pay_dt"]).ToString("dd/MM/yyyy");
                        DropDownList1.SelectedValue = select_app.Rows[0]["hol_appl_sts_cd"].ToString();
                        Textarea1.Value = select_app.Rows[0]["hol_remark"].ToString();
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Ic OR NO KP Baru',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Semak Isu',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }

    }

    protected void btn_click(object sender, EventArgs e)
    {
        try
        {
            if (Applcn_no.Text != "")
            {
                if (TextBox5.Text != "" && TextBox7.Text != "" && TextBox10.Text != "" && TextBox11.Text != "" && TextBox12.Text != "" && DropDownList1.SelectedValue != "" && Textarea1.Value != "")
                {

                    var ic_count = Applcn_no.Text.Length;
                    DataTable app_icno = new DataTable();
                    app_icno = DBCon.Ora_Execute_table("select app_applcn_no,isnull(app_current_jbb_ind,'') as app_current_jbb_ind from jpa_application JA where '" + Applcn_no.Text + "' IN(JA.app_applcn_no, JA.app_new_icno)");
                    if (ic_count == 12)
                    {
                        cc_no = app_icno.Rows[0]["app_applcn_no"].ToString();
                    }
                    else
                    {
                        cc_no = Applcn_no.Text;
                    }

                    string d1 = TextBox7.Text;
                    DateTime d11 = DateTime.ParseExact(d1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    date1 = d11.ToString("yyyy-MM-dd");

                    string d2 = TextBox10.Text;
                    DateTime d12 = DateTime.ParseExact(d2, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    date2 = d12.ToString("yyyy-MM-dd");

                    string d3 = TextBox11.Text;
                    DateTime d13 = DateTime.ParseExact(d3, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    date3 = d13.ToString("yyyy-MM-dd");

                    string d4 = TextBox12.Text;
                    DateTime d14 = DateTime.ParseExact(d4, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    date4 = d14.ToString("yyyy-MM-dd");

                    DataTable ddokdicno = new DataTable();
                    ddokdicno = Dblog.Ora_Execute_table("select * from jpa_payment_holiday where hol_applcn_no='" + cc_no + "'");
                    if (ddokdicno.Rows.Count > 0)
                    {
                        Dblog.Execute_CommamdText("Update jpa_payment_holiday SET hol_dur='" + TextBox5.Text + "',hol_start_dt='" + date1 + "',hol_end_dt='" + date2 + "',hol_start_pay_dt='" + date3 + "',hol_end_pay_dt='" + date4 + "',hol_appl_sts_cd='" + DropDownList1.SelectedValue + "',hol_remark='" + Textarea1.Value + "',hol_upd_id='" + Session["New"].ToString() + "',hol_upd_dt='" + DateTime.Now + "' WHERE hol_applcn_no='" + cc_no + "'");
                    }
                    else
                    {
                        Dblog.Execute_CommamdText("Insert into jpa_payment_holiday (hol_applcn_no,hol_dur,hol_start_dt,hol_end_dt,hol_start_pay_dt,hol_end_pay_dt,hol_appl_sts_cd,hol_remark,hol_crt_id,ho_crt_dt) values ('" + cc_no + "','" + TextBox5.Text + "','" + date1 + "','" + date2 + "','" + date3 + "','" + date4 + "','" + DropDownList1.SelectedValue + "','" + Textarea1.Value + "','" + Session["New"].ToString() + "','" + DateTime.Now + "')");
                    }

                    //Temporary Table
                    string c_dt1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    DateTime billrunDate1 = Convert.ToDateTime(c_dt1);
                    string c_dt = billrunDate1.ToString("%M");

                    //DataTable ddokdicno1 = new DataTable();
                    //ddokdicno1 = Dblog.Ora_Execute_table("SELECT jn.jno_applcn_no,jn.jno_pay_date,ISNULL(jn.jno_loan_amt,'') as jno_loan_amt,jn.jno_seq_no,ISNULL(jn.jno_approve_amt,'') as jno_approve_amt,ISNULL(jn.jno_profit_amt,'') as jno_profit_amt,jn.jno_actual_pay_date,ISNULL(jn.jno_pay_amt,'') as jno_pay_amt,ISNULL(jn.jno_actual_pay_amt,'') as jno_actual_pay_amt,ISNULL(jn.jno_late_excess_amt,'') as jno_late_excess_amt,ISNULL(jn.jno_saving_amt,'') as jno_saving_amt,ISNULL(jn.jno_actual_saving_amt,'') as jno_actual_saving_amt,ISNULL(jn.jno_saving_late_excess_amt,'') as jno_saving_late_excess_amt,ISNULL(jn.jno_total_pay_amt,'') as jno_total_pay_amt,ISNULL(jn.jno_balance_amt,'') as jno_balance_amt,ISNULL(jn.jno_total_saving_amt,'') as jno_total_saving_amt,jn.jno_day_late,ja.appl_loan_dur FROM jpa_jbb_normal as jn left join jpa_application as ja on ja.app_applcn_no = jn.jno_applcn_no where jn.jno_applcn_no='" + cc_no + "' and month(jn.jno_pay_date)='" + c_dt + "'");

                    DataTable ddokdicno1 = new DataTable();
                    string c_jbb = string.Empty;
                    if (app_icno.Rows[0]["app_current_jbb_ind"].ToString().Trim() != "")
                    {
                        c_jbb = app_icno.Rows[0]["app_current_jbb_ind"].ToString();
                        if (c_jbb == "L")
                        {
                            //dt = Dblog.Ora_Execute_table("SELECT jn.tex_applcn_no as app_no,FORMAT(jn.tex_pay_date,'dd/MM/yyyy', 'en-us') as p_dt,ISNULL(jn.tex_loan_amt,'') as l_amt,jn.tex_seq_no as seq_no,ISNULL(jn.tex_approve_amt,'') as a_amt,ISNULL(jn.tex_profit_amt,'') as pr_amt,jn.tex_actual_pay_date as ap_dt,ISNULL(jn.tex_pay_amt,'') as p_amt,ISNULL(jn.tex_actual_pay_amt,'') as ap_amt,ISNULL(jn.tex_late_excess_amt,'') as le_amt,ISNULL(jn.tex_saving_amt,'') as sa_amt,ISNULL(jn.tex_actual_saving_amt,'') as as_amt,ISNULL(jn.tex_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.tex_total_pay_amt,'') as tp_amt,ISNULL(jn.tex_balance_amt,'') as bal_amt,ISNULL(jn.tex_total_saving_amt,'') as ts_amt,jn.tex_day_late as dlate,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno FROM jpa_jbb_temp_extension as jn left join jpa_application as ja on ja.app_applcn_no = jn.tex_applcn_no where jn.tex_applcn_no='" + cc_no + "'");
                            ddokdicno1 = Dblog.Ora_Execute_table("select * from (SELECT jn.jwo_applcn_no as app_no,FORMAT(jn.jwo_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.jwo_loan_amt,'')) when '0.00' then '' else ISNULL(jn.jwo_loan_amt,'') end as l_amt,jn.jwo_seq_no as seq_no,ISNULL(jn.jwo_approve_amt,'') as a_amt,ISNULL(jn.jwo_profit_amt,'') as pr_amt,FORMAT(jn.jwo_actual_pay_date,'dd/MM/yyyy', 'en-us') as ap_dt,ISNULL(jn.jwo_pay_amt,'') as p_amt,ISNULL(jn.jwo_actual_pay_amt,'') as ap_amt,ISNULL(jn.jwo_late_excess_amt,'') as le_amt,ISNULL(jn.jwo_saving_amt,'') as sa_amt,ISNULL(jn.jwo_actual_saving_amt,'') as as_amt,ISNULL(jn.jwo_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.jwo_total_pay_amt,'') as tp_amt,ISNULL(jn.jwo_balance_amt,'') as bal_amt,ISNULL(jn.jwo_total_saving_amt,'') as ts_amt,jn.jwo_day_late as dlate,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno,rjp.Description FROM jpa_jbb_writeoff  as jn left join jpa_application as ja on ja.app_applcn_no = jn.jwo_applcn_no left join Ref_Jenis_Pelaburan as rjp on rjp.Description_Code=ja.app_loan_type_cd where jn.jwo_applcn_no='" + cc_no + "' ) as a full outer join (select jwo_applcn_no,sum(jwo_pay_amt) as tot_pamt,sum(jwo_actual_pay_amt) as tot_apamt,sum(jwo_saving_amt) as tot_samt,sum(jwo_actual_saving_amt) as tot_asamt,sum(jwo_total_pay_amt) as tot_tpamt from jpa_jbb_writeoff where jwo_applcn_no='" + cc_no + "' group by jwo_applcn_no) as b on b.jwo_applcn_no=a.app_no full outer join (select * from jpa_calculate_fee where cal_applcn_no='" + cc_no + "') as c on c.cal_applcn_no=b.jwo_applcn_no");
                        }
                        else if (c_jbb == "H")
                        {
                            ddokdicno1 = Dblog.Ora_Execute_table("select * from (SELECT jn.hol_applcn_no as app_no,FORMAT(jn.hol_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.hol_loan_amt,'')) when '0.00' then '' else ISNULL(jn.hol_loan_amt,'') end as l_amt,jn.hol_seq_no as seq_no,ISNULL(jn.hol_approve_amt,'') as a_amt,ISNULL(jn.hol_profit_amt,'') as pr_amt,FORMAT(jn.hol_actual_pay_date,'dd/MM/yyyy', 'en-us') as ap_dt,ISNULL(jn.hol_pay_amt,'') as p_amt,ISNULL(jn.hol_actual_pay_amt,'') as ap_amt,ISNULL(jn.hol_late_excess_amt,'') as le_amt,ISNULL(jn.hol_saving_amt,'') as sa_amt,ISNULL(jn.hol_actual_saving_amt,'') as as_amt,ISNULL(jn.hol_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.hol_total_pay_amt,'') as tp_amt,ISNULL(jn.hol_balance_amt,'') as bal_amt,ISNULL(jn.hol_total_saving_amt,'') as ts_amt,jn.hol_day_late as dlate,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno,rjp.Description FROM jpa_jbb_holiday  as jn left join jpa_application as ja on ja.app_applcn_no = jn.hol_applcn_no left join Ref_Jenis_Pelaburan as rjp on rjp.Description_Code=ja.app_loan_type_cd where jn.hol_applcn_no='" + cc_no + "' ) as a full outer join (select hol_applcn_no,sum(hol_pay_amt) as tot_pamt,sum(hol_actual_pay_amt) as tot_apamt,sum(hol_saving_amt) as tot_samt,sum(hol_actual_saving_amt) as tot_asamt,sum(hol_total_pay_amt) as tot_tpamt from jpa_jbb_holiday where hol_applcn_no='" + cc_no + "' group by hol_applcn_no) as b on b.hol_applcn_no=a.app_no full outer join (select * from jpa_calculate_fee where cal_applcn_no='" + cc_no + "') as c on c.cal_applcn_no=b.hol_applcn_no");
                        }
                        else if (c_jbb == "P")
                        {
                            ddokdicno1 = Dblog.Ora_Execute_table("select * from (SELECT jn.pjs_applcn_no as app_no,FORMAT(jn.pjs_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.pjs_loan_amt,'')) when '0.00' then '' else ISNULL(jn.pjs_loan_amt,'') end as l_amt,jn.pjs_seq_no as seq_no,ISNULL(jn.pjs_approve_amt,'') as a_amt,ISNULL(jn.pjs_profit_amt,'') as pr_amt,FORMAT(jn.pjs_actual_pay_date,'dd/MM/yyyy', 'en-us') as ap_dt,ISNULL(jn.pjs_pay_amt,'') as p_amt,ISNULL(jn.pjs_actual_pay_amt,'') as ap_amt,ISNULL(jn.pjs_late_excess_amt,'') as le_amt,ISNULL(jn.pjs_saving_amt,'') as sa_amt,ISNULL(jn.pjs_actual_saving_amt,'') as as_amt,ISNULL(jn.pjs_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.pjs_total_pay_amt,'') as tp_amt,ISNULL(jn.pjs_balance_amt,'') as bal_amt,ISNULL(jn.pjs_total_saving_amt,'') as ts_amt,jn.pjs_day_late as dlate,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno,rjp.Description FROM jpa_jbb_pjs  as jn left join jpa_application as ja on ja.app_applcn_no = jn.pjs_applcn_no left join Ref_Jenis_Pelaburan as rjp on rjp.Description_Code=ja.app_loan_type_cd where jn.pjs_applcn_no='" + cc_no + "' ) as a full outer join (select pjs_applcn_no,sum(pjs_pay_amt) as tot_pamt,sum(pjs_actual_pay_amt) as tot_apamt,sum(pjs_saving_amt) as tot_samt,sum(pjs_actual_saving_amt) as tot_asamt,sum(pjs_total_pay_amt) as tot_tpamt from jpa_jbb_pjs where pjs_applcn_no='" + cc_no + "' group by pjs_applcn_no) as b on b.pjs_applcn_no=a.app_no full outer join (select * from jpa_calculate_fee where cal_applcn_no='" + cc_no + "') as c on c.cal_applcn_no=b.pjs_applcn_no");
                        }
                        else if (c_jbb == "E")
                        {
                            ddokdicno1 = Dblog.Ora_Execute_table("select * from (SELECT jn.ext_applcn_no as app_no,FORMAT(jn.ext_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.ext_loan_amt,'')) when '0.00' then '' else ISNULL(jn.ext_loan_amt,'') end as l_amt,jn.ext_seq_no as seq_no,ISNULL(jn.ext_approve_amt,'') as a_amt,ISNULL(jn.ext_profit_amt,'') as pr_amt,FORMAT(jn.ext_actual_pay_date,'dd/MM/yyyy', 'en-us') as ap_dt,ISNULL(jn.ext_pay_amt,'') as p_amt,ISNULL(jn.ext_actual_pay_amt,'') as ap_amt,ISNULL(jn.ext_late_excess_amt,'') as le_amt,ISNULL(jn.ext_saving_amt,'') as sa_amt,ISNULL(jn.ext_actual_saving_amt,'') as as_amt,ISNULL(jn.ext_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.ext_total_pay_amt,'') as tp_amt,ISNULL(jn.ext_balance_amt,'') as bal_amt,ISNULL(jn.ext_total_saving_amt,'') as ts_amt,jn.ext_day_late as dlate,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno,rjp.Description FROM jpa_jbb_extension as jn left join jpa_application as ja on ja.app_applcn_no = jn.ext_applcn_no left join Ref_Jenis_Pelaburan as rjp on rjp.Description_Code=ja.app_loan_type_cd where jn.ext_applcn_no='" + cc_no + "' ) as a full outer join (select ext_applcn_no,sum(ext_pay_amt) as tot_pamt,sum(ext_actual_pay_amt) as tot_apamt,sum(ext_saving_amt) as tot_samt,sum(ext_actual_saving_amt) as tot_asamt,sum(ext_total_pay_amt) as tot_tpamt from jpa_jbb_extension where ext_applcn_no='" + cc_no + "' group by ext_applcn_no) as b on b.ext_applcn_no=a.app_no full outer join (select * from jpa_calculate_fee where cal_applcn_no='" + cc_no + "') as c on c.cal_applcn_no=b.ext_applcn_no");
                        }
                        else
                        {
                            //dt = Dblog.Ora_Execute_table("SELECT jn.jno_applcn_no as app_no,FORMAT(jn.jno_pay_date,'dd/MM/yyyy', 'en-us') as p_dt,ISNULL(jn.jno_loan_amt,'') as l_amt,jn.jno_seq_no as seq_no,ISNULL(jn.jno_approve_amt,'') as a_amt,ISNULL(jn.jno_profit_amt,'') as pr_amt,jn.jno_actual_pay_date as ap_dt,ISNULL(jn.jno_pay_amt,'') as p_amt,ISNULL(jn.jno_actual_pay_amt,'') as ap_amt,ISNULL(jn.jno_late_excess_amt,'') as le_amt,ISNULL(jn.jno_saving_amt,'') as sa_amt,ISNULL(jn.jno_actual_saving_amt,'') as as_amt,ISNULL(jn.jno_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.jno_total_pay_amt,'') as tp_amt,ISNULL(jn.jno_balance_amt,'') as bal_amt,ISNULL(jn.jno_total_saving_amt,'') as ts_amt,jn.jno_day_late as dlate,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno FROM jpa_jbb_normal as jn left join jpa_application as ja on ja.app_applcn_no = jn.jno_applcn_no where jn.jno_applcn_no='" + cc_no + "'");
                            ddokdicno1 = Dblog.Ora_Execute_table("select * from (SELECT jn.jno_applcn_no as app_no,FORMAT(jn.jno_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.jno_loan_amt,'')) when '0.00' then '' else ISNULL(jn.jno_loan_amt,'') end as l_amt,jn.jno_seq_no as seq_no,ISNULL(jn.jno_approve_amt,'') as a_amt,ISNULL(jn.jno_profit_amt,'') as pr_amt,FORMAT(jn.jno_actual_pay_date,'dd/MM/yyyy', 'en-us') as ap_dt,ISNULL(jn.jno_pay_amt,'') as p_amt,ISNULL(jn.jno_actual_pay_amt,'') as ap_amt,ISNULL(jn.jno_late_excess_amt,'') as le_amt,ISNULL(jn.jno_saving_amt,'') as sa_amt,ISNULL(jn.jno_actual_saving_amt,'') as as_amt,ISNULL(jn.jno_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.jno_total_pay_amt,'') as tp_amt,ISNULL(jn.jno_balance_amt,'') as bal_amt,ISNULL(jn.jno_total_saving_amt,'') as ts_amt,jn.jno_day_late as dlate,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno,rjp.Description FROM jpa_jbb_normal as jn left join jpa_application as ja on ja.app_applcn_no = jn.jno_applcn_no left join Ref_Jenis_Pelaburan as rjp on rjp.Description_Code=ja.app_loan_type_cd where jn.jno_applcn_no='" + cc_no + "' ) as a full outer join (select jno_applcn_no,sum(jno_pay_amt) as tot_pamt,sum(jno_actual_pay_amt) as tot_apamt,sum(jno_saving_amt) as tot_samt,sum(jno_actual_saving_amt) as tot_asamt,sum(jno_total_pay_amt) as tot_tpamt from jpa_jbb_normal where jno_applcn_no='" + cc_no + "' group by jno_applcn_no) as b on b.jno_applcn_no=a.app_no full outer join (select * from jpa_calculate_fee where cal_applcn_no='" + cc_no + "') as c on c.cal_applcn_no=b.jno_applcn_no");
                        }
                    }
                    else
                    {
                        ddokdicno1 = Dblog.Ora_Execute_table("select * from (SELECT jn.jno_applcn_no as app_no,FORMAT(jn.jno_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.jno_loan_amt,'')) when '0.00' then '' else ISNULL(jn.jno_loan_amt,'') end as l_amt,jn.jno_seq_no as seq_no,ISNULL(jn.jno_approve_amt,'') as a_amt,ISNULL(jn.jno_profit_amt,'') as pr_amt,FORMAT(jn.jno_actual_pay_date,'dd/MM/yyyy', 'en-us') as ap_dt,ISNULL(jn.jno_pay_amt,'') as p_amt,ISNULL(jn.jno_actual_pay_amt,'') as ap_amt,ISNULL(jn.jno_late_excess_amt,'') as le_amt,ISNULL(jn.jno_saving_amt,'') as sa_amt,ISNULL(jn.jno_actual_saving_amt,'') as as_amt,ISNULL(jn.jno_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.jno_total_pay_amt,'') as tp_amt,ISNULL(jn.jno_balance_amt,'') as bal_amt,ISNULL(jn.jno_total_saving_amt,'') as ts_amt,jn.jno_day_late as dlate,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno,rjp.Description FROM jpa_jbb_normal as jn left join jpa_application as ja on ja.app_applcn_no = jn.jno_applcn_no left join Ref_Jenis_Pelaburan as rjp on rjp.Description_Code=ja.app_loan_type_cd where jn.jno_applcn_no='" + cc_no + "' ) as a full outer join (select jno_applcn_no,sum(jno_pay_amt) as tot_pamt,sum(jno_actual_pay_amt) as tot_apamt,sum(jno_saving_amt) as tot_samt,sum(jno_actual_saving_amt) as tot_asamt,sum(jno_total_pay_amt) as tot_tpamt from jpa_jbb_normal where jno_applcn_no='" + cc_no + "' group by jno_applcn_no) as b on b.jno_applcn_no=a.app_no full outer join (select * from jpa_calculate_fee where cal_applcn_no='" + cc_no + "') as c on c.cal_applcn_no=b.jno_applcn_no");
                    }

                    if (ddokdicno1.Rows.Count != 0)
                    {
                        //var v1 = Convert.ToDateTime(ddokdicno1.Rows[0]["p_dt"]).ToString("yyyy-MM-dd");
                        DataTable ch_query = new DataTable();
                        ch_query = Dblog.Ora_Execute_table("SELECT * FROM jpa_jbb_temp_holiday where tho_applcn_no='" + cc_no + "'");
                        if (ch_query.Rows.Count > 0)
                        {
                            Dblog.Execute_CommamdText("Delete from jpa_jbb_temp_holiday where tho_applcn_no='" + cc_no + "'");
                        }
                        for (int i = 0; i <= ddokdicno1.Rows.Count - 1; i++)
                        {


                            string pay_amt = string.Empty;
                            string apay_amt = string.Empty;
                            string ppamt = string.Empty, priamt2 = string.Empty, dproamt2 = string.Empty;
                            string pramt = string.Empty;
                            string val1 = "71.43";
                            string val2 = "28.57";

                            string lamt = string.Empty;
                            string fin_pamt = string.Empty, fin_priamt = string.Empty, fin_dproamt = string.Empty;
                            DateTime date = DateTime.Now;
                            string dd4 = ddokdicno1.Rows[i]["p_dt"].ToString();
                            DateTime dd_14 = DateTime.ParseExact(dd4, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            dpay_4 = dd_14.ToString("yyyy-MM-dd");
                            string d4_ap = ddokdicno1.Rows[i]["ap_dt"].ToString();
                            DateTime d14_ap = DateTime.ParseExact(d4_ap, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            pdate_4ap = d14_ap.ToString("yyyy-MM-dd");
                            if (ddokdicno1.Rows[i]["seq_no"].ToString() == "1")
                            {
                                ppamt = "0.00";
                                lamt = txtjumla.Text;
                                apay_amt = "0.00";
                                //priamt2 = pri_amt;
                                //dproamt2 = dpro_amt;
                            }
                            else
                            {
                                lamt = "0.00";

                                if (dd_14 >= d11 && dd_14 <= d12)
                                {
                                    ppamt = "0.00";
                                    //priamt2 = "0.00";
                                    //dproamt2 = "0.00";
                                }
                                else
                                {
                                    //pay_amt = ((double.Parse(txtjumla.Text) + double.Parse(ddokdicno1.Rows[i]["pr_amt"].ToString())) / double.Parse(ddokdicno1.Rows[i]["a_dur"].ToString())).ToString("0.00");

                                    string d13_y = dd_14.ToString("yyyyMM");

                                    //priamt2 = pri_amt;
                                    //dproamt2 = dpro_amt;

                                    //String sDate_m = DateTime.Now.ToString("MM");
                                    String sDate_y = DateTime.Now.ToString("yyyyMM");



                                    if (pdate_4ap != "1900-01-01" && double.Parse(d13_y) == double.Parse(sDate_y))
                                    {

                                        if (pdate_4ap == "1900-01-01" && double.Parse(d13_y) > double.Parse(sDate_y))
                                        {
                                            //ppamt = ((double.Parse(ddokdicno1.Rows[i]["l_amt"].ToString()) + double.Parse(ddokdicno1.Rows[i]["pr_amt"].ToString())) / double.Parse(ddokdicno1.Rows[i]["a_dur"].ToString())).ToString();
                                            ppamt = ddokdicno1.Rows[i]["p_amt"].ToString();
                                            //pramt = ((double.Parse(jhk.Text) / 100) * double.Parse(pr_amt)).ToString();
                                        }
                                        else
                                        {
                                            ppamt = ddokdicno1.Rows[i]["p_amt"].ToString();
                                            // pramt = pr_amt;
                                        }
                                    }
                                    else
                                    {
                                        if (pdate_4ap == "1900-01-01" && double.Parse(d13_y) > double.Parse(sDate_y))
                                        {
                                            ppamt = ddokdicno1.Rows[i]["p_amt"].ToString();
                                            //ppamt = ((double.Parse(ddokdicno1.Rows[i]["l_amt"].ToString()) + double.Parse(ddokdicno1.Rows[i]["pr_amt"].ToString())) / double.Parse(ddokdicno1.Rows[i]["a_dur"].ToString())).ToString();
                                            //pramt = ((double.Parse(jhk.Text) / 100) * double.Parse(pr_amt)).ToString();
                                        }
                                        else
                                        {
                                            ppamt = ddokdicno1.Rows[i]["p_amt"].ToString();
                                            //pramt = pr_amt;
                                        }
                                    }
                                }
                                string cal_mul = ((double.Parse(txttempoh.Text) + 1) - double.Parse(TextBox5.Text)).ToString();

                                string pri_amt = ((double.Parse(ppamt) * double.Parse(val1)) / 100).ToString("0.00");
                                string dpro_amt = ((double.Parse(ppamt) * double.Parse(val2)) / 100).ToString("0.00");

                                if (double.Parse(cal_mul) < double.Parse(ddokdicno1.Rows[i]["seq_no"].ToString()))
                                {
                                    fin_pamt = (double.Parse(ppamt) * 2).ToString("0,0.00");
                                    fin_priamt = (double.Parse(pri_amt) * 2).ToString("0,0.00");
                                    fin_dproamt = (double.Parse(dpro_amt) * 2).ToString("0,0.00");
                                }
                                else
                                {
                                    fin_pamt = ppamt;
                                    fin_priamt = pri_amt;
                                    fin_dproamt = dpro_amt;
                                }

                                apay_amt = ddokdicno1.Rows[i]["ap_amt"].ToString();
                            }

                          
                            Dblog.Execute_CommamdText("INSERT INTO jpa_jbb_temp_holiday (tho_applcn_no,tho_pay_date,tho_loan_amt,tho_seq_no,tho_approve_amt,tho_profit_amt,tho_actual_pay_date,tho_pay_amt,tho_actual_pay_amt,tho_late_excess_amt,tho_actual_saving_amt,tho_total_pay_amt,tho_balance_amt,tho_saving_amt,tho_saving_late_excess_amt,tho_total_saving_amt,tho_day_late,tho_principal_amt,tho_daily_profit_amt,tho_crt_id,tho_crt_dt,set_flag) VALUES ('" + cc_no + "','" + dpay_4 + "','" + lamt + "','" + ddokdicno1.Rows[i]["seq_no"].ToString() + "','" + ddokdicno1.Rows[i]["a_amt"].ToString() + "','" + ddokdicno1.Rows[i]["pr_amt"].ToString() + "','" + pdate_4ap + "','" + fin_pamt + "','" + apay_amt + "','" + ddokdicno1.Rows[i]["le_amt"].ToString() + "','" + ddokdicno1.Rows[i]["as_amt"].ToString() + "','" + ddokdicno1.Rows[i]["tp_amt"].ToString() + "','" + ddokdicno1.Rows[i]["bal_amt"].ToString() + "','" + ddokdicno1.Rows[i]["sa_amt"].ToString() + "','" + ddokdicno1.Rows[i]["sle_amt"].ToString() + "','" + ddokdicno1.Rows[i]["ts_amt"].ToString() + "','0','" + fin_priamt + "','" + fin_dproamt + "','" + Session["New"].ToString() + "','" + DateTime.Now + "','1')");
                        }                        
                    }
                    Button7.Visible = true;
                    Button2.Visible = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Red Mark Field Should be Mandatory',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
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
            Response.Redirect("../PELABURAN_ANGGOTA/PP_Guaman_cagaran.aspx?spi_icno=" + cc_no);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila mesti Masukkan Kata-kata',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }

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
            Response.Redirect("../PELABURAN_ANGGOTA/PP_Guaman_penjamin.aspx?spi_icno=" + cc_no);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila mesti Masukkan Kata-kata',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
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
                    //if (app_icno.Rows[0]["app_current_jbb_ind"].ToString().Trim() != "")
                    //{
                    //    c_jbb = app_icno.Rows[0]["app_current_jbb_ind"].ToString();
                    //    if (c_jbb == "H")
                    //    {
                    //        //dt = Dblog.Ora_Execute_table("SELECT jn.tex_applcn_no as app_no,FORMAT(jn.tex_pay_date,'dd/MM/yyyy', 'en-us') as p_dt,ISNULL(jn.tex_loan_amt,'') as l_amt,jn.tex_seq_no as seq_no,ISNULL(jn.tex_approve_amt,'') as a_amt,ISNULL(jn.tex_profit_amt,'') as pr_amt,jn.tex_actual_pay_date as ap_dt,ISNULL(jn.tex_pay_amt,'') as p_amt,ISNULL(jn.tex_actual_pay_amt,'') as ap_amt,ISNULL(jn.tex_late_excess_amt,'') as le_amt,ISNULL(jn.tex_saving_amt,'') as sa_amt,ISNULL(jn.tex_actual_saving_amt,'') as as_amt,ISNULL(jn.tex_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.tex_total_pay_amt,'') as tp_amt,ISNULL(jn.tex_balance_amt,'') as bal_amt,ISNULL(jn.tex_total_saving_amt,'') as ts_amt,jn.tex_day_late as dlate,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno FROM jpa_jbb_temp_extension as jn left join jpa_application as ja on ja.app_applcn_no = jn.tex_applcn_no where jn.tex_applcn_no='" + cc_no + "'");
                    //        dt = Dblog.Ora_Execute_table("select * from (SELECT jn.tho_applcn_no as app_no,FORMAT(jn.tho_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.tho_loan_amt,'')) when '0.00' then '' else ISNULL(jn.tho_loan_amt,'') end as l_amt,jn.tho_seq_no as seq_no,ISNULL(jn.tho_approve_amt,'') as a_amt,ISNULL(jn.tho_profit_amt,'') as pr_amt,jn.tho_actual_pay_date as ap_dt,ISNULL(jn.tho_pay_amt,'') as p_amt,ISNULL(jn.tho_actual_pay_amt,'') as ap_amt,ISNULL(jn.tho_late_excess_amt,'') as le_amt,ISNULL(jn.tho_saving_amt,'') as sa_amt,ISNULL(jn.tho_actual_saving_amt,'') as as_amt,ISNULL(jn.tho_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.tho_total_pay_amt,'') as tp_amt,ISNULL(jn.tho_balance_amt,'') as bal_amt,ISNULL(jn.tho_total_saving_amt,'') as ts_amt,jn.tho_day_late as dlate,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno FROM jpa_jbb_temp_holiday  as jn left join jpa_application as ja on ja.app_applcn_no = jn.tho_applcn_no where jn.tho_applcn_no='" + cc_no + "' ) as a full outer join (select tho_applcn_no,sum(tho_pay_amt) as tot_pamt,sum(tho_actual_pay_amt) as tot_apamt,sum(tho_saving_amt) as tot_samt,sum(tho_actual_saving_amt) as tot_asamt,sum(tho_total_pay_amt) as tot_tpamt from jpa_jbb_temp_holiday where tho_applcn_no='" + cc_no + "' group by tho_applcn_no) as b on b.tho_applcn_no=a.app_no");
                    //    }
                    //}
                    //else
                    //{
                    //    //dt = Dblog.Ora_Execute_table("SELECT jn.jno_applcn_no as app_no,FORMAT(jn.jno_pay_date,'dd/MM/yyyy', 'en-us') as p_dt,ISNULL(jn.jno_loan_amt,'') as l_amt,jn.jno_seq_no as seq_no,ISNULL(jn.jno_approve_amt,'') as a_amt,ISNULL(jn.jno_profit_amt,'') as pr_amt,jn.jno_actual_pay_date as ap_dt,ISNULL(jn.jno_pay_amt,'') as p_amt,ISNULL(jn.jno_actual_pay_amt,'') as ap_amt,ISNULL(jn.jno_late_excess_amt,'') as le_amt,ISNULL(jn.jno_saving_amt,'') as sa_amt,ISNULL(jn.jno_actual_saving_amt,'') as as_amt,ISNULL(jn.jno_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.jno_total_pay_amt,'') as tp_amt,ISNULL(jn.jno_balance_amt,'') as bal_amt,ISNULL(jn.jno_total_saving_amt,'') as ts_amt,jn.jno_day_late as dlate,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno FROM jpa_jbb_normal as jn left join jpa_application as ja on ja.app_applcn_no = jn.jno_applcn_no where jn.jno_applcn_no='" + cc_no + "'");
                    //    dt = Dblog.Ora_Execute_table("select * from (SELECT jn.jno_applcn_no as app_no,FORMAT(jn.jno_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.jno_loan_amt,'')) when '0.00' then '' else ISNULL(jn.jno_loan_amt,'') end as l_amt,jn.jno_seq_no as seq_no,ISNULL(jn.jno_approve_amt,'') as a_amt,ISNULL(jn.jno_profit_amt,'') as pr_amt,jn.jno_actual_pay_date as ap_dt,ISNULL(jn.jno_pay_amt,'') as p_amt,ISNULL(jn.jno_actual_pay_amt,'') as ap_amt,ISNULL(jn.jno_late_excess_amt,'') as le_amt,ISNULL(jn.jno_saving_amt,'') as sa_amt,ISNULL(jn.jno_actual_saving_amt,'') as as_amt,ISNULL(jn.jno_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.jno_total_pay_amt,'') as tp_amt,ISNULL(jn.jno_balance_amt,'') as bal_amt,ISNULL(jn.jno_total_saving_amt,'') as ts_amt,jn.jno_day_late as dlate,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno FROM jpa_jbb_normal as jn left join jpa_application as ja on ja.app_applcn_no = jn.jno_applcn_no where jn.jno_applcn_no='" + cc_no + "' ) as a full outer join (select jno_applcn_no,sum(jno_pay_amt) as tot_pamt,sum(jno_actual_pay_amt) as tot_apamt,sum(jno_saving_amt) as tot_samt,sum(jno_actual_saving_amt) as tot_asamt,sum(jno_total_pay_amt) as tot_tpamt from jpa_jbb_normal where jno_applcn_no='" + cc_no + "' group by jno_applcn_no) as b on b.jno_applcn_no=a.app_no");
                    //}

                    ////dt = DBCon.Ora_Execute_table("select ja.app_name,ja.app_new_icno,ja.app_applcn_no,ja.appl_loan_dur,ISNULL(ja.app_installment_amt,'') as app_installment_amt,ja.app_loan_type_cd,tp.tpj_approve_amt,tp.tpj_profit_amt,tp.tpj_pay_date,tp.tpj_actual_pay_date,tp.tpj_approve_amt,tp.tpj_pay_amt,tp.tpj_actual_pay_amt,tp.tpj_late_excess_amt,tp.tpj_saving_amt,tp.tpj_actual_saving_amt,tp.tpj_saving_late_excess_amt,tp.tpj_total_pay_amt,tp.tpj_balance_amt,tp.tpj_total_saving_amt,tp.tpj_day_late from jpa_jbb_temp_pjs as tp left join jpa_application as ja on ja.app_applcn_no=tp.tpj_applcn_no and ja.app_sts_cd='Y' where tp.tpj_applcn_no='" + cc_no + "'");

                    dt = Dblog.Ora_Execute_table("select * from (SELECT jn.tho_applcn_no as app_no,FORMAT(jn.tho_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.tho_loan_amt,'')) when '0.00' then '' else ISNULL(jn.tho_loan_amt,'') end as l_amt,jn.tho_seq_no as seq_no,ISNULL(jn.tho_approve_amt,'') as a_amt,ISNULL(jn.tho_profit_amt,'') as pr_amt,FORMAT(jn.tho_actual_pay_date,'dd/MM/yyyy', 'en-us') as ap_dt,ISNULL(jn.tho_pay_amt,'') as p_amt,ISNULL(jn.tho_actual_pay_amt,'') as ap_amt,ISNULL(jn.tho_late_excess_amt,'') as le_amt,ISNULL(jn.tho_saving_amt,'') as sa_amt,ISNULL(jn.tho_actual_saving_amt,'') as as_amt,ISNULL(jn.tho_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.tho_total_pay_amt,'') as tp_amt,ISNULL(jn.tho_balance_amt,'') as bal_amt,ISNULL(jn.tho_total_saving_amt,'') as ts_amt,jn.tho_day_late as dlate,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno,rjp.Description,ISNULL(jn.tho_principal_amt,'') as priamt,ISNULL(jn.tho_daily_profit_amt,'') as dproamt,ja.app_cumm_saving_amt as acs_amt FROM jpa_jbb_temp_holiday  as jn left join jpa_application as ja on ja.app_applcn_no = jn.tho_applcn_no left join Ref_Jenis_Pelaburan as rjp on rjp.Description_Code=ja.app_loan_type_cd where jn.tho_applcn_no='" + cc_no + "' ) as a full outer join (select tho_applcn_no,sum(tho_pay_amt) as tot_pamt,sum(tho_actual_pay_amt) as tot_apamt,sum(tho_saving_amt) as tot_samt,sum(tho_actual_saving_amt) as tot_asamt,sum(tho_total_pay_amt) as tot_tpamt,sum(tho_principal_amt) as tot_priamt,sum(tho_daily_profit_amt) as tot_dproamt from jpa_jbb_temp_holiday where tho_applcn_no='" + cc_no + "' group by tho_applcn_no) as b on b.tho_applcn_no=a.app_no full outer join (select * from jpa_calculate_fee where cal_applcn_no='" + cc_no + "') as c on c.cal_applcn_no=b.tho_applcn_no");

                    Rptviwer_lt.Reset();
                    ds.Tables.Add(dt);

                    Rptviwer_lt.LocalReport.DataSources.Clear();

                    //Rptviwer_lt.LocalReport.ReportPath = "m_tangguh.rdlc";
                    //ReportDataSource rds = new ReportDataSource("mt_dset", dt);

                    //Rptviwer_lt.LocalReport.ReportPath = "s_jbb.rdlc";
                    //ReportDataSource rds = new ReportDataSource("sjbb", dt);

                    Rptviwer_lt.LocalReport.ReportPath = "PELABURAN_ANGGOTA/hkira.rdlc";
                    ReportDataSource rds = new ReportDataSource("hapus_kira", dt);

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

                    Response.AddHeader("content-disposition", "attachment; filename= PERMOHONAN_TANGGUH_PEMBIAYAAN_" + cc_no + "." + extension);

                    Response.BinaryWrite(bytes);

                    Response.Flush();

                    //Response.End();
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
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
                string url = "../PELABURAN_ANGGOTA/semak_jbb.aspx?jbb_icno=" + Applcn_no.Text;

                string s = "window.open('" + url + "', 'popup_window', 'width=11000,height=700,left=100,top=100,resizable=yes');";

                ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Ic OR NO KP Baru',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void clk_bak(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Pelaburan_Anggota/PP_M_Tangguh_view.aspx");
    }
}