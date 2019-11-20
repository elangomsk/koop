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

public partial class PP_MP_Tempoh : System.Web.UI.Page
{
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string str;
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    SqlCommand com;
    SqlCommand grid_cmd;

    string c_jbb = string.Empty, pdate_3 = string.Empty;
    DataTable dt = new DataTable();
    string Status = string.Empty, pdate_4 = string.Empty;
    string level, userid;
    string cc_no = string.Empty;
    String date1 = String.Empty;
    String date2 = String.Empty;
    String date3 = String.Empty;
    String date4 = String.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Button7);
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
                    Button3.Text = "Kemaskini";
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
                    select_app = DBCon.Ora_Execute_table("select * from (select JA.app_applcn_no,JA.app_new_icno,JA.app_name,RW.Wilayah_Name,ISNULL(RB.branch_desc, '') AS branch_desc,JA.app_loan_amt,JA.appl_loan_dur,JA.app_permnt_address,ja.app_permnt_postcode,ja.app_permnt_state_cd,ja.app_phone_h,ja.app_phone_m,ja.app_phone_o,ja.app_mailing_address,JA.app_mailing_postcode,ja.app_mailing_state_cd,ISNULL(JA.app_cumm_installment_amt,'') as app_cumm_installment_amt,ISNULL(JA.app_cumm_pay_amt,'') as app_cumm_pay_amt,ISNULL(JA.app_backdated_amt,'') as app_backdated_amt,ISNULL(JA.app_cumm_profit_amt,'') as app_cumm_profit_amt,ISNULL(JA.app_cumm_saving_amt,'') as app_cumm_saving_amt,ISNULL(JA.app_bal_loan_amt,'') as app_bal_loan_amt from jpa_application as JA  Left Join jpa_calculate_fee as JC ON JC.cal_applcn_no=JA.app_applcn_no Left Join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd Left join ref_branch AS RB ON RB.branch_cd=JA.app_branch_cd where JA.app_applcn_no = '" + ddicno.Rows[0]["app_applcn_no"] + "') as a full outer join (select * from jpa_extension  where ext_applcn_no='" + ddicno.Rows[0]["app_applcn_no"] + "') as b on a.app_applcn_no=b.ext_applcn_no");

                    txtname.Text = select_app.Rows[0]["app_name"].ToString();
                    //txtwil.Text = select_app.Rows[0]["Wilayah_Name"].ToString();
                    //decimal amt1 = (decimal)select_app.Rows[0]["app_loan_amt"];
                    string aamt = (double.Parse(select_app.Rows[0]["app_loan_amt"].ToString()) + double.Parse(select_app.Rows[0]["app_cumm_profit_amt"].ToString())).ToString("C").Replace("$", "").Replace("RM", "");
                    txtjumla.Text = aamt;
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

                    TextBox6.Text = select_app.Rows[0]["ext_paid_dur"].ToString();
                    if (select_app.Rows[0]["ext_paid_amt"].ToString() != "")
                    {
                        decimal t51 = (decimal)select_app.Rows[0]["ext_paid_amt"];
                        TextBox51.Text = t51.ToString("C").Replace("$", "").Replace("RM", "");
                    }
                    else
                    {
                        TextBox51.Text = "";
                    }
                    TextBox10.Text = select_app.Rows[0]["ext_extension_dur"].ToString();
                    if (select_app.Rows[0]["ext_pay_amt"].ToString() != "")
                    {
                        decimal t12 = (decimal)select_app.Rows[0]["ext_pay_amt"];
                        TextBox12.Text = t12.ToString("C").Replace("$", "").Replace("RM", "");
                    }
                    else
                    {
                        TextBox12.Text = "";
                    }
                    DropDownList1.SelectedValue = select_app.Rows[0]["ext_apply_sts_cd"].ToString();
                    Textarea2.Value = select_app.Rows[0]["ext_apply_remark"].ToString();
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
                if (TextBox6.Text != "" && TextBox51.Text != "" && TextBox10.Text != "" && TextBox12.Text != "" && DropDownList1.SelectedValue != "" && Textarea2.Value != "")
                {

                    var ic_count = Applcn_no.Text.Length;
                    DataTable app_icno = new DataTable();
                    app_icno = DBCon.Ora_Execute_table("select app_applcn_no,app_current_jbb_ind from jpa_application JA where '" + Applcn_no.Text + "' IN(JA.app_applcn_no, JA.app_new_icno)");
                    if (ic_count == 12)
                    {
                        cc_no = app_icno.Rows[0]["app_applcn_no"].ToString();
                    }
                    else
                    {
                        cc_no = Applcn_no.Text;
                    }

                    string new_dur = TextBox10.Text;

                    DataTable ddokdicno = new DataTable();
                    ddokdicno = Dblog.Ora_Execute_table("select * from jpa_extension where ext_applcn_no='" + cc_no + "'");
                    if (ddokdicno.Rows.Count > 0)
                    {
                        Dblog.Execute_CommamdText("Update jpa_extension SET ext_paid_dur='" + TextBox6.Text + "',ext_paid_amt='" + TextBox51.Text + "',ext_extension_dur='" + TextBox10.Text + "',ext_new_pay_dur='" + new_dur + "',ext_pay_amt='" + TextBox12.Text + "',ext_apply_sts_cd='" + DropDownList1.SelectedValue + "',ext_apply_remark='" + Textarea2.Value + "',ext_upd_id='" + Session["New"].ToString() + "',ext_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE ext_applcn_no='" + cc_no + "'");
                    }
                    else
                    {
                        Dblog.Execute_CommamdText("Insert into jpa_extension (ext_applcn_no,ext_paid_dur,ext_paid_amt,ext_extension_dur,ext_new_pay_dur,ext_pay_amt,ext_apply_sts_cd,ext_apply_remark,ext_crt_id,ext_crt_dt) values ('" + cc_no + "','" + TextBox6.Text + "','" + TextBox51.Text + "','" + TextBox10.Text + "','" + new_dur + "','" + TextBox12.Text + "','" + DropDownList1.SelectedValue + "','" + Textarea2.Value + "','" + Session["New"].ToString() + "','" + DateTime.Now + "')");
                    }



                    if (app_icno.Rows.Count != 0)
                    {
                        DataTable ddokdicno1 = new DataTable();
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

                        if (ddokdicno1.Rows.Count != 0)
                        {
                            //var v1 = Convert.ToDateTime(ddokdicno1.Rows[0]["p_dt"]).ToString("yyyy-MM-dd");
                            DataTable ch_query = new DataTable();
                            ch_query = Dblog.Ora_Execute_table("SELECT * FROM jpa_jbb_temp_extension where tex_applcn_no='" + cc_no + "'");
                            if (ch_query.Rows.Count > 0)
                            {
                                Dblog.Execute_CommamdText("Delete from jpa_jbb_temp_extension where tex_applcn_no='" + cc_no + "'");
                            }
                            for (int i = 0; i <= ddokdicno1.Rows.Count - 1; i++)
                            {


                                string pay_amt = string.Empty;
                                string apay_amt = string.Empty;
                                string val1 = "71.43";
                                string val2 = "28.57";

                                string lamt = string.Empty;
                                string d3 = ddokdicno1.Rows[i]["p_dt"].ToString();
                                DateTime d13 = DateTime.ParseExact(d3, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                pdate_3 = d13.ToString("yyyy-MM-dd");
                                string pdt = d13.ToString("yyyyMM");



                                //string d13_m = d13.ToString("MM");
                                string d13_y = d13.ToString("yyyyMM");

                                string d4 = ddokdicno1.Rows[i]["ap_dt"].ToString();
                                DateTime d14 = DateTime.ParseExact(d4, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                pdate_4 = d14.ToString("yyyy-MM-dd");

                                //String sDate_m = DateTime.Now.ToString("MM");
                                String sDate_y = DateTime.Now.ToString("yyyyMM");
                                string ppamt = string.Empty;
                                string pramt = string.Empty;

                                if (ddokdicno1.Rows[i]["seq_no"].ToString() == "1")
                                {
                                    ppamt = "0.00";
                                    lamt = txtjumla.Text;
                                    apay_amt = "0.00";
                                }
                                else
                                {
                                    lamt = "0.00";
                                    //pay_amt = ((double.Parse(txtjumla.Text) + double.Parse(ddokdicno1.Rows[i]["pr_amt"].ToString())) / double.Parse(ddokdicno1.Rows[i]["a_dur"].ToString())).ToString("0.00");
                                    apay_amt = ddokdicno1.Rows[i]["ap_amt"].ToString();
                                    if (pdate_4 != "1900-01-01" && double.Parse(d13_y) == double.Parse(sDate_y))
                                    {

                                        if (pdate_4 == "1900-01-01" && double.Parse(d13_y) > double.Parse(sDate_y))
                                        {
                                            ppamt = ((double.Parse(txtjumla.Text) + double.Parse(ddokdicno1.Rows[i]["pr_amt"].ToString())) / (double.Parse(ddokdicno1.Rows[i]["a_dur"].ToString()) + double.Parse(TextBox10.Text))).ToString("0.00");
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
                                        if (pdate_4 == "1900-01-01" && double.Parse(d13_y) > double.Parse(sDate_y))
                                        {
                                            ppamt = ((double.Parse(txtjumla.Text) + double.Parse(ddokdicno1.Rows[i]["pr_amt"].ToString())) / (double.Parse(ddokdicno1.Rows[i]["a_dur"].ToString()) + double.Parse(TextBox10.Text))).ToString("0.00");
                                            //pramt = ((double.Parse(jhk.Text) / 100) * double.Parse(pr_amt)).ToString();
                                        }
                                        else
                                        {
                                            ppamt = ddokdicno1.Rows[i]["p_amt"].ToString();
                                            //pramt = pr_amt;
                                        }
                                    }
                                }
                                string pri_amt = ((double.Parse(ppamt) * double.Parse(val1)) / 100).ToString("0.00");
                                string dpro_amt = ((double.Parse(ppamt) * double.Parse(val2)) / 100).ToString("0.00");
                                string cdt = DateTime.Now.ToString("yyyyMM");
                                string ndt = DateTime.Now.AddMonths(1).ToString("yyyyMM");

                                if (double.Parse(cdt) >= double.Parse(pdt))
                                {
                                    Dblog.Execute_CommamdText("INSERT INTO jpa_jbb_temp_extension (tex_applcn_no,tex_pay_date,tex_actual_pay_date,tex_loan_amt,tex_seq_no,tex_approve_amt,tex_profit_amt,tex_pay_amt,tex_actual_pay_amt,tex_late_excess_amt,tex_actual_saving_amt,tex_total_pay_amt,tex_balance_amt,tex_saving_amt,tex_saving_late_excess_amt,tex_total_saving_amt,tex_day_late,tex_principal_amt,tex_daily_profit_amt,tex_crt_id,tex_crt_dt,set_flag) VALUES ('" + cc_no + "','" + pdate_3 + "','" + pdate_4 + "','" + lamt + "','" + ddokdicno1.Rows[i]["seq_no"].ToString() + "','" + ddokdicno1.Rows[i]["a_amt"].ToString() + "','" + ddokdicno1.Rows[i]["pr_amt"].ToString() + "','" + ppamt + "','" + apay_amt + "','" + ddokdicno1.Rows[i]["le_amt"].ToString() + "','" + ddokdicno1.Rows[i]["as_amt"].ToString() + "','" + ddokdicno1.Rows[i]["tp_amt"].ToString() + "','" + ddokdicno1.Rows[i]["bal_amt"].ToString() + "','" + ddokdicno1.Rows[i]["sa_amt"].ToString() + "','" + ddokdicno1.Rows[i]["sle_amt"].ToString() + "','" + ddokdicno1.Rows[i]["ts_amt"].ToString() + "','0','" + pri_amt + "','" + dpro_amt + "','" + Session["New"].ToString() + "','" + DateTime.Now + "','1')");
                                }
                                else if (double.Parse(ndt) == double.Parse(pdt))
                                {
                                    string vv1 = "2";
                                    string vv2 = ddokdicno1.Rows[i]["seq_no"].ToString();
                                    string incno = (double.Parse(vv2) - double.Parse(vv1)).ToString();
                                    string sqn = (double.Parse(new_dur) - double.Parse(incno)).ToString();
                                    int sqn1 = Int32.Parse(ddokdicno1.Rows[i]["seq_no"].ToString());
                                    string spamt = (double.Parse(ddokdicno1.Rows[i]["bal_amt"].ToString()) / double.Parse(sqn)).ToString();
                                    //string emnth = "28";
                                    string month = string.Empty;
                                    string year = string.Empty;
                                    string day = string.Empty;
                                    DateTime thisDate = Convert.ToDateTime(pdate_3);
                                    day = thisDate.Day.ToString();
                                    month = thisDate.Month.ToString().PadLeft(2, '0');
                                    year = thisDate.Year.ToString();
                                    string d1 = "" + month + "/" + day + "/" + year;

                                    for (int k = 0; k <= double.Parse(sqn); k++)
                                    {
                                        DateTime mm = Convert.ToDateTime(d1).AddMonths(k);
                                        string pdt_month = mm.ToString("yyyy-MM-dd");
                                        int sss = k + sqn1;
                                        Dblog.Execute_CommamdText("INSERT INTO jpa_jbb_temp_extension (tex_applcn_no,tex_pay_date,tex_actual_pay_date,tex_loan_amt,tex_seq_no,tex_approve_amt,tex_profit_amt,tex_pay_amt,tex_actual_pay_amt,tex_late_excess_amt,tex_actual_saving_amt,tex_total_pay_amt,tex_balance_amt,tex_saving_amt,tex_saving_late_excess_amt,tex_total_saving_amt,tex_day_late,tex_principal_amt,tex_daily_profit_amt,tex_crt_id,tex_crt_dt,set_flag) VALUES ('" + cc_no + "','" + pdt_month + "','" + pdate_4 + "','" + lamt + "','" + sss + "','" + ddokdicno1.Rows[i]["a_amt"].ToString() + "','" + ddokdicno1.Rows[i]["pr_amt"].ToString() + "','" + spamt + "','" + apay_amt + "','" + ddokdicno1.Rows[i]["le_amt"].ToString() + "','" + ddokdicno1.Rows[i]["as_amt"].ToString() + "','" + ddokdicno1.Rows[i]["tp_amt"].ToString() + "','" + ddokdicno1.Rows[i]["bal_amt"].ToString() + "','" + ddokdicno1.Rows[i]["sa_amt"].ToString() + "','" + ddokdicno1.Rows[i]["sle_amt"].ToString() + "','" + ddokdicno1.Rows[i]["ts_amt"].ToString() + "','0','" + pri_amt + "','" + dpro_amt + "','" + Session["New"].ToString() + "','" + DateTime.Now + "','1')");
                                    }
                                }

                            }
                        }

                    }

                    Session["validate_success"] = "SUCCESS";
                    Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                    Response.Redirect("../Pelaburan_Anggota/PP_MP_Tempoh_view.aspx");
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

    
    protected void cetak_click(object sender, EventArgs e)
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
              
                dt = Dblog.Ora_Execute_table("select * from (SELECT jn.tex_applcn_no as app_no,FORMAT(jn.tex_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.tex_loan_amt,'')) when '0.00' then '' else ISNULL(jn.tex_loan_amt,'') end as l_amt,jn.tex_seq_no as seq_no,ISNULL(jn.tex_approve_amt,'') as a_amt,ISNULL(jn.tex_profit_amt,'') as pr_amt,FORMAT(jn.tex_actual_pay_date,'dd/MM/yyyy', 'en-us') as ap_dt,ISNULL(jn.tex_pay_amt,'') as p_amt,ISNULL(jn.tex_actual_pay_amt,'') as ap_amt,ISNULL(jn.tex_late_excess_amt,'') as le_amt,ISNULL(jn.tex_saving_amt,'') as sa_amt,ISNULL(jn.tex_actual_saving_amt,'') as as_amt,ISNULL(jn.tex_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.tex_total_pay_amt,'') as tp_amt,ISNULL(jn.tex_balance_amt,'') as bal_amt,ISNULL(jn.tex_total_saving_amt,'') as ts_amt,jn.tex_day_late as dlate,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno,rjp.Description,ISNULL(jn.tex_principal_amt,'') as priamt,ISNULL(jn.tex_daily_profit_amt,'') as dproamt,ja.app_cumm_saving_amt as acs_amt FROM jpa_jbb_temp_extension  as jn left join jpa_application as ja on ja.app_applcn_no = jn.tex_applcn_no left join Ref_Jenis_Pelaburan as rjp on rjp.Description_Code=ja.app_loan_type_cd where jn.tex_applcn_no='" + cc_no + "' ) as a full outer join (select tex_applcn_no,sum(tex_pay_amt) as tot_pamt,sum(tex_actual_pay_amt) as tot_apamt,sum(tex_saving_amt) as tot_samt,sum(tex_actual_saving_amt) as tot_asamt,sum(tex_total_pay_amt) as tot_tpamt,sum(tex_principal_amt) as tot_priamt,sum(tex_daily_profit_amt) as tot_dproamt from jpa_jbb_temp_extension where tex_applcn_no='" + cc_no + "' group by tex_applcn_no) as b on b.tex_applcn_no=a.app_no full outer join (select * from jpa_calculate_fee where cal_applcn_no='" + cc_no + "') as c on c.cal_applcn_no=b.tex_applcn_no");

                ReportViewer1.Reset();
                ds.Tables.Add(dt);

                ReportViewer1.LocalReport.DataSources.Clear();

                //ReportViewer1.LocalReport.ReportPath = "s_jbb.rdlc";
                //ReportDataSource rds = new ReportDataSource("sjbb", dt);

                ReportViewer1.LocalReport.ReportPath = "PELABURAN_ANGGOTA/hkira.rdlc";
                ReportDataSource rds = new ReportDataSource("hapus_kira", dt);

                ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("samt",sav_amt)

                     };


                ReportViewer1.LocalReport.SetParameters(rptParams);

                ReportViewer1.LocalReport.DataSources.Add(rds);

                //Refresh
                ReportViewer1.LocalReport.Refresh();
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

                byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);

                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                //Response.AddHeader("content-disposition", "inline; filename=JBB_" + fname + "." + extension);
                Response.AddHeader("content-disposition", "attachment; filename=  Permohonan_Panjang_Tempoh_" + cc_no + "." + extension);
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Medan Input Adalah Mandatori')", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
            //Request.Redirect(url, false);
        }

    }

    protected void clk_bak(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Pelaburan_Anggota/PP_MP_Tempoh_view.aspx");
    }
}