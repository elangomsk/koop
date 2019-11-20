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
using System.Xml;


public partial class PP_kem_kelulusan : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string str;
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    SqlCommand com;
    DataTable dt = new DataTable();
    string val17;
    string Status = string.Empty;
    string level, userid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                level = Session["level"].ToString();
                userid = Session["New"].ToString();

                //Default value
                MP_nama.Attributes.Add("Readonly", "Readonly");
            
                MP_tujuan.Attributes.Add("Readonly", "Readonly");
                MP_amaun.Attributes.Add("Readonly", "Readonly");
                MP_tempoh.Attributes.Add("Readonly", "Readonly");
                MK_amaun.Attributes.Add("Readonly", "Readonly");
                MK_tempoh.Attributes.Add("Readonly", "Readonly");

                //JANAAN

                TextBox2.Attributes.Add("Readonly", "Readonly");
                TextBox6.Attributes.Add("Readonly", "Readonly");
                //TextBox9.Attributes.Add("Readonly", "Readonly");
                TextBox5.Attributes.Add("Readonly", "Readonly");
                TextBox13.Attributes.Add("Readonly", "Readonly");
                TextBox7.Attributes.Add("Readonly", "Readonly");

                var samp = Request.Url.Query;
                if (samp != "")
                {
                    Icno.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    srch_rfd();
                    Button4.Visible = false;
                    Button6.Visible = false;
                   
                }

            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    void srch_rfd()
    {
        if (Icno.Text != "")
        {
            DataTable ddicno = new DataTable();
            ddicno = DBCon.Ora_Execute_table("select JA.app_new_icno,JA.app_applcn_no from jpa_application as JA where JA.app_applcn_no='" + Icno.Text + "'");

            DataTable ddicno1 = new DataTable();
            ddicno1 = DBCon.Ora_Execute_table("select * from jpa_jkpa_approval where jkp_applcn_no='" + Icno.Text + "'");
            if (ddicno1.Rows.Count == 0)
            {
                DataTable ddicno2 = new DataTable();
                ddicno2 = DBCon.Ora_Execute_table("select * from jpa_jkkpa_approval where jkk_applcn_no='" + Icno.Text + "'");

                if (ddicno2.Rows.Count == 0)
                {
                    reset();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
                else
                {
                    string Select_App_query = "select JA.app_new_icno,JA.app_age,JA.app_name,rt.tujuan_desc,JA.app_apply_amt,JA.app_loan_amt,JA.appl_loan_dur,JA.app_apply_dur,RW.Wilayah_Name,ISNULL(RB.branch_desc, '') AS branch_desc, JJA.jkk_approve_amt,JJA.jkk_approve_dur,JJA.jkk_result_ind from jpa_application as JA left join ref_tujuan as rt on rt.tujuan_cd=JA.app_loan_purpose_cd Inner join jpa_jkkpa_approval as JJA ON JJA.jkk_applcn_no = JA.app_applcn_no Left Join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd Left join ref_branch AS RB ON RB.branch_cd=JA.app_branch_cd where JA.app_applcn_no='" + Icno.Text + "'";
                    con.Open();
                    var sqlCommand = new SqlCommand(Select_App_query, con);
                    var sqlReader = sqlCommand.ExecuteReader();
                    if (sqlReader.Read() == true)
                    {
                        MP_nama.Text = (string)sqlReader["app_name"];
                    
                        MP_tujuan.Text = (string)sqlReader["tujuan_desc"];

                        MP_amaun.Text = double.Parse(sqlReader["app_apply_amt"].ToString()).ToString("C").Replace("RM","").Replace("$", "");
                        MP_tempoh.Text = sqlReader["app_apply_dur"].ToString();                        
                        MK_amaun.Text = double.Parse(sqlReader["jkk_approve_amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                        MK_tempoh.Text = sqlReader["jkk_approve_dur"].ToString();

                        MPDP_amaun.Text = double.Parse(sqlReader["app_loan_amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                        MPDP_tempoh.Text = sqlReader["appl_loan_dur"].ToString();

                        if (sqlReader["jkk_result_ind"].ToString() == "L")
                        {
                            RadioButton1.Checked = true;
                            RadioButton2.Checked = false;
                        }
                        else if (sqlReader["jkk_result_ind"].ToString() == "T")
                        {
                            RadioButton1.Checked = false;
                            RadioButton2.Checked = true;
                        }
                        else
                        {
                            RadioButton1.Checked = false;
                            RadioButton2.Checked = false;
                        }

                        DataTable Select_calculate = new DataTable();
                        Select_calculate = DBCon.Ora_Execute_table("select JC.* from jpa_calculate_fee as JC Left join jpa_application as JA ON JA.app_applcn_no=JC.cal_applcn_no where JC.cal_applcn_no='" + Icno.Text + "'");
                      
                        if (Select_calculate.Rows.Count != 0)
                        {
                            TextBox2.Text = double.Parse(Select_calculate.Rows[0]["cal_stamp_duty_amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                            TextBox10.Text = double.Parse(Select_calculate.Rows[0]["cal_tkh_amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                            TextBox6.Text = double.Parse(Select_calculate.Rows[0]["cal_process_fee"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                            TextBox9.Text = double.Parse(Select_calculate.Rows[0]["cal_credit_fee"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                            TextBox5.Text = double.Parse(Select_calculate.Rows[0]["cal_deposit_amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                            TextBox13.Text = double.Parse(Select_calculate.Rows[0]["cal_profit_amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                            TextBox7.Text = double.Parse(Select_calculate.Rows[0]["cal_installment_amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                            TxtBilangan.Text = Select_calculate.Rows[0]["cal_no_guarantor"].ToString();

                        }
                        sqlReader.Close();
                        Button1.Visible = true;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }

            }
            else
            {
                string Select_App_query = "select JA.app_new_icno,JA.app_age,JA.app_name,rt.tujuan_desc,JA.app_apply_amt,JA.app_apply_dur,JA.app_loan_amt,JA.appl_loan_dur,RW.Wilayah_Name,ISNULL(RB.branch_desc, '') AS branch_desc, JJA.jkp_approve_amt,JJA.jkp_approve_dur from jpa_application as JA left join ref_tujuan as rt on rt.tujuan_cd=JA.app_loan_purpose_cd Inner join jpa_jkpa_approval as JJA ON JJA.jkp_applcn_no = JA.app_applcn_no Left Join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd Left join ref_branch AS RB ON RB.branch_cd=JA.app_branch_cd where JA.app_applcn_no='" + Icno.Text + "'";
                con.Open();
                var sqlCommand = new SqlCommand(Select_App_query, con);
                var sqlReader = sqlCommand.ExecuteReader();
                if (sqlReader.Read() == true)
                {
                    MP_nama.Text = (string)sqlReader["app_name"];
                    appage.Text = sqlReader["app_age"].ToString();
                   
                    MP_tujuan.Text = (string)sqlReader["tujuan_desc"].ToString();
                    MP_amaun.Text = double.Parse(sqlReader["app_apply_amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                    MP_tempoh.Text = sqlReader["app_apply_dur"].ToString();                    
                    MK_amaun.Text = double.Parse(sqlReader["jkp_approve_amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                    MK_tempoh.Text = sqlReader["jkp_approve_dur"].ToString();
                    MPDP_amaun.Text = double.Parse(sqlReader["app_loan_amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                    MPDP_tempoh.Text = sqlReader["appl_loan_dur"].ToString();

                    DataTable ddicno21 = new DataTable();
                    ddicno21 = DBCon.Ora_Execute_table("select * from jpa_jkkpa_approval where jkk_applcn_no='" + Icno.Text + "'");
                    if(ddicno21.Rows.Count != 0)
                    {
                        if (ddicno21.Rows[0]["jkk_result_ind"].ToString() == "L")
                        {
                            RadioButton1.Checked = true;
                            RadioButton2.Checked = false;
                        }
                        else if (ddicno21.Rows[0]["jkk_result_ind"].ToString() == "T")
                        {
                            RadioButton1.Checked = false;
                            RadioButton2.Checked = true;
                        }
                        else
                        {
                            RadioButton1.Checked = false;
                            RadioButton2.Checked = false;
                        }

                        KSP_catatan.Value = ddicno21.Rows[0]["jkk_condition_remark"].ToString();
                    }

                    DataTable Select_calculate = new DataTable();
                    Select_calculate = DBCon.Ora_Execute_table("select JC.* from jpa_calculate_fee as JC Left join jpa_application as JA ON JA.app_applcn_no=JC.cal_applcn_no where JC.cal_applcn_no='" + Icno.Text + "'");

                    if (Select_calculate.Rows.Count != 0)
                    {
                        TextBox2.Text = double.Parse(Select_calculate.Rows[0]["cal_stamp_duty_amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                        TextBox10.Text = double.Parse(Select_calculate.Rows[0]["cal_tkh_amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                        TextBox6.Text = double.Parse(Select_calculate.Rows[0]["cal_process_fee"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                        TextBox9.Text = double.Parse(Select_calculate.Rows[0]["cal_credit_fee"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                        TextBox5.Text = double.Parse(Select_calculate.Rows[0]["cal_deposit_amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                        TextBox13.Text = double.Parse(Select_calculate.Rows[0]["cal_profit_amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                        TextBox7.Text = double.Parse(Select_calculate.Rows[0]["cal_installment_amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                        TxtBilangan.Text = Select_calculate.Rows[0]["cal_no_guarantor"].ToString();
                     
                    }
                    
                    sqlReader.Close();
                    Button1.Visible = true;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
                
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Ic',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }
    protected void btnsrch_Click(object sender, EventArgs e)
    {
           srch_rfd();
       
    }


    protected void btnsmmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (MPDP_amaun.Text != "" && MPDP_tempoh.Text != "")
            {
                if (RadioButton1.Checked == true || RadioButton2.Checked == true)
                {
                    DataTable ddicno2 = new DataTable();
                    ddicno2 = DBCon.Ora_Execute_table("select JA.app_new_icno,JA.app_applcn_no from jpa_application as JA Inner Join jpa_jkkpa_approval as JKA ON JKA.jkk_applcn_no = JA.app_applcn_no  where JA.app_applcn_no='" + Icno.Text + "'");

                    if (ddicno2.Rows.Count != 0)
                    {
                        DataTable Check_status = new DataTable();
                        DBCon.Execute_CommamdText("Update jpa_application SET app_loan_amt = '" + MPDP_amaun.Text + "', appl_loan_dur = '" + MPDP_tempoh.Text + "' WHERE app_applcn_no = '" + Icno.Text + "'");
                       
                        DBCon.Execute_CommamdText("Update jpa_calculate_fee SET cal_stamp_duty_amt='" + TextBox2.Text + "',cal_no_guarantor='" + TxtBilangan.Text + "',cal_tkh_amt='" + TextBox10.Text + "',cal_process_fee='" + TextBox6.Text + "',cal_credit_fee='" + TextBox9.Text + "',cal_deposit_amt='" + TextBox5.Text + "',cal_profit_amt='" + TextBox13.Text + "',cal_installment_amt='" + TextBox7.Text + "', cal_approve_amt = '" + MPDP_amaun.Text + "', cal_approve_dur = '" + MPDP_tempoh.Text + "',cal_upd_id = '" + Session["New"].ToString() + "', cal_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE cal_applcn_no = '" + Icno.Text + "'");
                       
                        string sts = string.Empty;
                        string appsts = string.Empty;
                        if (RadioButton1.Checked == true)
                        {
                            sts = "L";
                            appsts = "Y";
                        }
                        else
                        {
                            sts = "T";
                            appsts = "N";
                        }
                        string txn_bamt = string.Empty;
                        txn_bamt = (double.Parse(MPDP_amaun.Text) + double.Parse(TextBox13.Text)).ToString("0.00");
                        DBCon.Execute_CommamdText("insert into jpa_transaction (txn_applcn_no,txn_cd,txn_debit_amt,txn_bal_amt,txn_crt_id,txn_crt_dt) values ('" + Icno.Text + "','01','0.00','" + txn_bamt + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");
                        DBCon.Execute_CommamdText("Update jpa_jkkpa_approval SET jkk_result_ind = '" + sts + "', jkk_condition_remark = '" + KSP_catatan.Value + "', jkk_upd_id = '" + Session["New"].ToString() + "', jkk_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE jkk_applcn_no = '" + Icno.Text + "'");
                        DBCon.Execute_CommamdText("UPDATE jpa_application SET app_sts_cd='" + appsts + "' WHERE app_applcn_no='" + Icno.Text + "'");
                        Session["validate_success"] = "SUCCESS";
                        Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                        Response.Redirect("../Pelaburan_Anggota/PP_kem_kelulusan_view.aspx");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Ic No wujud',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Kemaskini Status Permohonan',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Maklumat Pengeluaran Dipersetujui Pemohon',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }

    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton1.Checked == true)
        {
            RadioButton2.Checked = false;


        }
    }
    protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton2.Checked == true)
        {
            RadioButton1.Checked = false;

        }
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void reset()
    {

        MP_nama.Text = "";
       
        MP_tujuan.Text = "";
        MP_amaun.Text = "";
        MP_tempoh.Text = "";
        MK_amaun.Text = "";
        MK_tempoh.Text = "";
    }

  
    protected void kira_Click(object sender, EventArgs e)
    {
        if (MPDP_amaun.Text != "" && MPDP_tempoh.Text != "" && TxtBilangan.Text != "")
        {
            DataTable dd_calc = new DataTable();
            dd_calc = DBCon.Ora_Execute_table("select cal_profit_rate,cal_no_guarantor from jpa_calculate_fee where cal_applcn_no='" + Icno.Text + "'");
            if (dd_calc.Rows.Count != 0)
            {

                string kadar = dd_calc.Rows[0]["cal_profit_rate"].ToString();
                //string bilangan = dd_calc.Rows[0]["cal_no_guarantor"].ToString();
                string bilangan = TxtBilangan.Text;
                string val1 = (double.Parse(MPDP_amaun.Text) * double.Parse(MPDP_tempoh.Text)).ToString();
                //string val10 = (double.Parse(MPDP_amaun.Text)).ToString();
                string val2 = ((double.Parse(val1) * (double.Parse(kadar) / 100)) / 12).ToString();
                TextBox13.Text = (double.Parse(val2)).ToString("#,##.00");
                string c1 = "0.5";
                string t = "100";
                string c2 = "10";
                string val3 = ((((double.Parse(c1) / double.Parse(t)) * double.Parse(MPDP_amaun.Text)))).ToString();
                string val4 = (double.Parse(c2) * double.Parse(bilangan)).ToString();
                TextBox2.Text = (double.Parse(val3) + double.Parse(val4)).ToString("#,##.00");
                TextBox6.Text = "100.00";
                string val9 = ((double.Parse(MPDP_amaun.Text) + double.Parse(val2)) / double.Parse(MPDP_tempoh.Text)).ToString("#,##.00");
                string val7 = (double.Parse(val9) * 2).ToString();
                TextBox5.Text = (double.Parse(val7)).ToString("#,##.00");
                string val8 = (10 * (double.Parse(bilangan) + 1)).ToString("#,##.00");
                TextBox9.Text = (double.Parse(val8)).ToString("#,##.00");

                TextBox7.Text = (double.Parse(val9)).ToString("#,##.00");

                //string val11=txtage.Text.ToString();
                //string val12 = "60";
                string val11;

                //Premium TKH (RM) :
                DataTable cnt_tkh = new DataTable();
                cnt_tkh = DBCon.Ora_Execute_table("select * from jpa_khairat where tkh_applcn_no='" + Icno.Text + "' order by tkh_seq_no");
                string tkh1 = string.Empty, tkh2 = string.Empty;
                if (cnt_tkh.Rows.Count >= 1)
                {

                    if (double.Parse(appage.Text) < 60)
                    {
                        double total = 0;
                        int x = Int32.Parse(MPDP_tempoh.Text);
                        for (int i = 0; i <= x; i += 12)
                        {
                            string val13 = (double.Parse(MPDP_tempoh.Text) - i).ToString();
                            string val14 = double.Parse(val9).ToString();
                            string val15 = "1000";
                            string flval = (((double.Parse(val14) * double.Parse(val13)) / double.Parse(val15)) * 6).ToString();
                            val17 = (double.Parse(flval)).ToString();
                            total = total + Convert.ToDouble(val17);
                            tkh1 = total.ToString("#,##.00");
                        }
                    }
                    else
                    {
                        double total = 0;
                        int x = Int32.Parse(MPDP_tempoh.Text);
                        for (int i = 0; i <= x; i += 12)
                        {
                            string val13 = (double.Parse(MPDP_tempoh.Text) - i).ToString();
                            string val14 = double.Parse(val9).ToString();
                            string val15 = "1000";
                            string flval = (((double.Parse(val14) * double.Parse(val13)) / double.Parse(val15)) * 12).ToString();
                            val17 = (double.Parse(flval)).ToString();
                            total = total + Convert.ToDouble(val17);
                            tkh1 = total.ToString("#,##.00");
                        }
                    }
                }


                if (cnt_tkh.Rows.Count == 2)
                {

                    if (double.Parse(cnt_tkh.Rows[1]["tkh_age"].ToString()) < 60)
                    {
                        double total1 = 0;
                        int x1 = Int32.Parse(MPDP_tempoh.Text);
                        for (int i = 0; i <= x1; i += 12)
                        {
                            string val13 = (double.Parse(MPDP_tempoh.Text) - i).ToString();
                            string val14 = double.Parse(val9).ToString();
                            string val15 = "1000";
                            string flval = (((double.Parse(val14) * double.Parse(val13)) / double.Parse(val15)) * 6).ToString();
                            val17 = (double.Parse(flval)).ToString();
                            total1 = total1 + Convert.ToDouble(val17);
                            tkh2 = total1.ToString("#,##.00");
                        }
                    }
                    else
                    {
                        double total1 = 0;
                        int x1 = Int32.Parse(MPDP_tempoh.Text);
                        for (int i = 0; i <= x1; i += 12)
                        {
                            string val13 = (double.Parse(MPDP_tempoh.Text) - i).ToString();
                            string val14 = double.Parse(val9).ToString();
                            string val15 = "1000";
                            string flval = (((double.Parse(val14) * double.Parse(val13)) / double.Parse(val15)) * 12).ToString();
                            val17 = (double.Parse(flval)).ToString();
                            total1 = total1 + Convert.ToDouble(val17);
                            tkh2 = total1.ToString("#,##.00");
                        }
                    }
                }
                if (tkh2 != "")
                {
                    TextBox10.Text = (double.Parse(tkh1) + double.Parse(tkh2)).ToString("C").Replace("$", "");
                }
                else
                {
                    TextBox10.Text = (double.Parse(tkh1) + 0).ToString("C").Replace("$", "");
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Nilai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }

    }

    protected void clk_bak(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Pelaburan_Anggota/PP_kem_kelulusan_view.aspx");
    }
    protected void rst_clik(object sender, EventArgs e)
    {
        Response.Redirect("../Pelaburan_Anggota/PP_kem_kelulusan.aspx");
    }
}