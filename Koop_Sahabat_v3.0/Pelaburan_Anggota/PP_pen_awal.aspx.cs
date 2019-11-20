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

public partial class PP_pen_awal : System.Web.UI.Page
{
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    DataTable dt = new DataTable();
    string Status = string.Empty;
    string level, userid;
    string cc_no = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Button3);
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
                TextBox6.Attributes.Add("Readonly", "Readonly");
                TextBox7.Attributes.Add("Readonly", "Readonly");
                TextBox8.Attributes.Add("Readonly", "Readonly");
                TextBox10.Attributes.Add("Readonly", "Readonly");
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
                com.CommandText = "select app_applcn_no from jpa_application where app_applcn_no like '%' + @Search + '%' and  app_sts_cd='Y' and applcn_clsed ='N'";
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
        try
        {

            if (Applcn_no.Text != "")
            {

                DataTable ddicno = new DataTable();
                ddicno = DBCon.Ora_Execute_table("select distinct app_applcn_no,app_new_icno from jpa_application JA where JA.app_sts_cd='Y' and '" + Applcn_no.Text + "' IN(JA.app_applcn_no, JA.app_new_icno)");

                if (ddicno.Rows.Count != 0)
                {
                    DataTable select_app = new DataTable();
                    select_app = DBCon.Ora_Execute_table("select * from (select JA.app_applcn_no,ja.app_current_jbb_ind,ja.app_end_pay_dt,JA.app_new_icno,JA.app_name,RW.Wilayah_Name,ISNULL(RB.branch_desc, '') AS branch_desc,JA.app_loan_amt,JA.appl_loan_dur,JA.app_permnt_address,ja.app_permnt_postcode,ja.app_permnt_state_cd,ja.app_phone_h,ja.app_phone_m,ja.app_phone_o,ja.app_mailing_address,JA.app_mailing_postcode,ja.app_mailing_state_cd,ISNULL(JA.app_cumm_installment_amt,'') as app_cumm_installment_amt,ISNULL(JA.app_cumm_pay_amt,'') as app_cumm_pay_amt,ISNULL(JA.app_backdated_amt,'') as app_backdated_amt,ISNULL(JA.app_cumm_profit_amt,'') as app_cumm_profit_amt,ISNULL(JA.app_cumm_saving_amt,'') as app_cumm_saving_amt,ISNULL(JA.app_bal_loan_amt,'') as app_bal_loan_amt from jpa_application as JA  Left Join jpa_calculate_fee as JC ON JC.cal_applcn_no=JA.app_applcn_no Left Join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd Left join ref_branch AS RB ON RB.branch_cd=JA.app_branch_cd where '" + ddicno.Rows[0]["app_applcn_no"] + "' IN(JA.app_applcn_no, JA.app_new_icno)  ) as a full outer join (select * from jpa_write_off  where '" + ddicno.Rows[0]["app_applcn_no"] + "' IN(wri_applcn_no) ) as b on a.app_applcn_no=b.wri_applcn_no");

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


                    string jbb_ind = string.Empty, jbb_indname = string.Empty;
                    if (select_app.Rows[0]["app_current_jbb_ind"].ToString() == "L")
                    {
                        jbb_ind = "writeoff";
                        jbb_indname = "jwo";
                    }
                    else if (select_app.Rows[0]["app_current_jbb_ind"].ToString() == "E")
                    {
                        jbb_ind = "extension";
                        jbb_indname = "ext";
                    }
                    else if (select_app.Rows[0]["app_current_jbb_ind"].ToString() == "P")
                    {
                        jbb_ind = "pjs";
                        jbb_indname = "pjs";
                    }
                    else if (select_app.Rows[0]["app_current_jbb_ind"].ToString() == "H")
                    {
                        jbb_ind = "holiday";
                        jbb_indname = "hol";
                    }
                    else if (select_app.Rows[0]["app_current_jbb_ind"].ToString() == "N")
                    {
                        jbb_ind = "normal";
                        jbb_indname = "jno";
                    }

                    string cday = DateTime.Now.ToString("dd");
                    string cmonth = DateTime.Now.ToString("MM");
                    string cyear = DateTime.Now.ToString("yyyy");

                    string a6 = string.Empty;
                    if (double.Parse(cday) >= 28)
                    {
                        DataTable jbb_sel = new DataTable();
                        jbb_sel = DBCon.Ora_Execute_table("select " + jbb_indname + "_pay_date,ISNULL(" + jbb_indname + "_late_excess_amt,'') as " + jbb_indname + "_late_excess_amt from jpa_jbb_" + jbb_ind + " where " + jbb_indname + "_applcn_no='" + select_app.Rows[0]["app_applcn_no"].ToString() + "' and MONTH(" + jbb_indname + "_pay_date) = '01' and Year(" + jbb_indname + "_pay_date) = '2017'");
                        if (jbb_sel.Rows.Count != 0)
                        {
                            a6 = (double.Parse(select_app.Rows[0]["app_bal_loan_amt"].ToString()) - double.Parse(jbb_sel.Rows[0]["" + jbb_indname + "_late_excess_amt"].ToString())).ToString("C").Replace("$", "").Replace("RM", "");
                            TextBox5.Text = a6;
                        }
                        else
                        {
                            TextBox5.Text = "0.00";
                        }
                }
                    else
                    {
                        if (cmonth == "01")
                        {
                            string pyear = (double.Parse(cyear) - 1).ToString();
                            DataTable jbb_sel = new DataTable();
                            jbb_sel = DBCon.Ora_Execute_table("select " + jbb_indname + "_pay_date,ISNULL(" + jbb_indname + "_late_excess_amt,'') as " + jbb_indname + "_late_excess_amt from jpa_jbb_" + jbb_ind + " where " + jbb_indname + "_applcn_no='" + select_app.Rows[0]["app_applcn_no"].ToString() + "' and MONTH(" + jbb_indname + "_pay_date) = '12' and Year(" + jbb_indname + "_pay_date) = '" + pyear + "'");
                            if (jbb_sel.Rows.Count != 0)
                            {
                                a6 = (double.Parse(select_app.Rows[0]["app_bal_loan_amt"].ToString()) - double.Parse(jbb_sel.Rows[0]["" + jbb_indname + "_late_excess_amt"].ToString())).ToString("C").Replace("$", "").Replace("RM", "");
                            TextBox5.Text = a6;
                            }
                            else
                            {
                                TextBox5.Text = "0.00";
                            }
                        }
                        else
                        {
                            string pmonth = (double.Parse(cmonth) - 1).ToString();
                            DataTable jbb_sel = new DataTable();
                            jbb_sel = DBCon.Ora_Execute_table("select " + jbb_indname + "_pay_date,ISNULL(" + jbb_indname + "_late_excess_amt,'') as " + jbb_indname + "_late_excess_amt from jpa_jbb_" + jbb_ind + " where " + jbb_indname + "_applcn_no='" + select_app.Rows[0]["app_applcn_no"].ToString() + "' and MONTH(" + jbb_indname + "_pay_date) = '" + pmonth + "' and Year(" + jbb_indname + "_pay_date) = '" + cyear + "'");
                            if (jbb_sel.Rows.Count != 0)
                            {
                                a6 = (double.Parse(select_app.Rows[0]["app_bal_loan_amt"].ToString()) - double.Parse(jbb_sel.Rows[0]["" + jbb_indname + "_late_excess_amt"].ToString())).ToString("C").Replace("$", "").Replace("RM", "");
                                TextBox5.Text = a6;
                            }
                            else
                            {
                                TextBox5.Text = "0.00";
                            }
                        }

                    }


                    if (select_app.Rows[0]["app_current_jbb_ind"].ToString() == "N")
                    {
                        TextBox6.Text = "NORMAL";
                    }
                    else if (select_app.Rows[0]["app_current_jbb_ind"].ToString() == "P")
                    {
                        TextBox6.Text = "PJS";
                    }
                    else if (select_app.Rows[0]["app_current_jbb_ind"].ToString() == "E")
                    {
                        TextBox6.Text = "PANJANG TEMPOH";
                    }
                    else if (select_app.Rows[0]["app_current_jbb_ind"].ToString() == "H")
                    {
                        TextBox6.Text = "TANGGUH";
                    }
                    else
                    {
                        TextBox6.Text = "HAPUS KIRA";
                    }

                    if (select_app.Rows[0]["app_end_pay_dt"].ToString() != "")
                    {
                        TextBox7.Text = Convert.ToDateTime(select_app.Rows[0]["app_end_pay_dt"]).ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        TextBox7.Text = "";
                    }



                }
                else
                {                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }

            }
            else
            {                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Ic OR NO KP Baru.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Semak Isu.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }

    }

    protected void clk_Bercagar(object sender, EventArgs e)
    {
        if (Applcn_no.Text != "")
        {
            var ic_count = Applcn_no.Text.Length;
            DataTable app_icno = new DataTable();
            app_icno = DBCon.Ora_Execute_table("select app_applcn_no from jpa_application JA where '" + Applcn_no.Text + "' IN(JA.app_applcn_no, JA.app_new_icno)");
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
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila mesti Masukkan Kata-kata.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }

    }

    protected void clk_Penjamin(object sender, EventArgs e)
    {
        if (Applcn_no.Text != "")
        {
            var ic_count = Applcn_no.Text.Length;
            DataTable app_icno = new DataTable();
            app_icno = DBCon.Ora_Execute_table("select app_applcn_no from jpa_application JA where '" + Applcn_no.Text + "' IN(JA.app_applcn_no, JA.app_new_icno)");
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
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Sila mesti Masukkan Kata-kata');", true);
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

   
    protected void Kira_Click(object sender, EventArgs e)
    {
        try
        {

            if (TextBox8.Text != "" && TextBox10.Text != "")
            {

                DataTable ddicno = new DataTable();
                ddicno = DBCon.Ora_Execute_table("select app_applcn_no,app_current_jbb_ind from jpa_application JA where JA.app_sts_cd='Y' and '" + Applcn_no.Text + "' IN(JA.app_applcn_no, JA.app_new_icno)");

                if (ddicno.Rows.Count != 0)
                {

                    DataTable select_app1p = new DataTable();
                    //select_app1p = DBCon.Ora_Execute_table("select *,ISNULL(d.lfe_legal_fee_amt,'') as leg_fee_amt from (select ja.app_applcn_no,JA.app_current_jbb_ind,ISNULL(JA.app_bal_loan_amt,'') as app_bal_loan_amt ,ISNULL(sum(JA.app_cumm_saving_amt),'') as app_cumm_saving_amt from jpa_application JA where ja.app_applcn_no='" + ddicno.Rows[0]["app_applcn_no"].ToString() + "' and ja.app_sts_cd ='Y' group by app_applcn_no,app_current_jbb_ind,app_bal_loan_amt) as a full outer join (select cal_applcn_no,ISNULL(sum (cal_deposit_amt),'') as cal_deposit_amt from jpa_calculate_fee where cal_applcn_no='" + ddicno.Rows[0]["app_applcn_no"].ToString() + "' group by cal_applcn_no) as b on b.cal_applcn_no=a.app_applcn_no full outer join(select caj_applcn_no,sum(caj_postage) as caj_postage,sum(caj_late_pay) as caj_late_pay from jpa_charge where caj_applcn_no='" + ddicno.Rows[0]["app_applcn_no"].ToString() + "' and (caj_postage_pay_dt != '1900-01-01' OR caj_late_pay_pay_dt != '1900-01-01') group by caj_applcn_no) as c on c.caj_applcn_no=b.cal_applcn_no full outer join(select lfe_applcn_no,sum(lfe_legal_fee_amt) as lfe_legal_fee_amt from jpa_lawyer_fee where lfe_applcn_no='" + ddicno.Rows[0]["app_applcn_no"].ToString() + "' and lfe_pay_dt!='1900-01-01' group by lfe_applcn_no) as d on d.lfe_applcn_no=c.caj_applcn_no");
                    select_app1p = DBCon.Ora_Execute_table("select a.app_applcn_no,a.app_current_jbb_ind,a.app_bal_loan_amt,a.app_cumm_saving_amt,b.cal_applcn_no,b.cal_deposit_amt,ISNULL(caj_applcn_no,'') as caj_applcn_no,ISNULL(caj_postage,'')as caj_postage,ISNULL(caj_late_pay,'') as caj_late_pay,ISNULL(lfe_applcn_no,'') as lfe_applcn_no,ISNULL(lfe_legal_fee_amt,'') as lfe_legal_fee_amt,ISNULL(d.lfe_legal_fee_amt,'') as leg_fee_amt from (select ja.app_applcn_no,JA.app_current_jbb_ind,ISNULL(JA.app_bal_loan_amt,'') as app_bal_loan_amt ,ISNULL(sum(JA.app_cumm_saving_amt),'') as app_cumm_saving_amt from jpa_application JA where ja.app_applcn_no='" + ddicno.Rows[0]["app_applcn_no"].ToString() + "' and ja.app_sts_cd ='Y' group by app_applcn_no,app_current_jbb_ind,app_bal_loan_amt) as a full outer join (select cal_applcn_no,ISNULL(sum (cal_deposit_amt),'') as cal_deposit_amt from jpa_calculate_fee where cal_applcn_no='" + ddicno.Rows[0]["app_applcn_no"].ToString() + "' group by cal_applcn_no) as b on b.cal_applcn_no=a.app_applcn_no full outer join(select caj_applcn_no,sum(caj_postage) as caj_postage,sum(caj_late_pay) as caj_late_pay from jpa_charge where caj_applcn_no='" + ddicno.Rows[0]["app_applcn_no"].ToString() + "' and (caj_postage_pay_dt != '1900-01-01' OR caj_late_pay_pay_dt != '1900-01-01') group by caj_applcn_no) as c on c.caj_applcn_no=b.cal_applcn_no full outer join(select lfe_applcn_no,sum(lfe_legal_fee_amt) as lfe_legal_fee_amt from jpa_lawyer_fee where lfe_applcn_no='" + ddicno.Rows[0]["app_applcn_no"].ToString() + "' and lfe_pay_dt!='1900-01-01' group by lfe_applcn_no) as d on d.lfe_applcn_no='" + ddicno.Rows[0]["app_applcn_no"].ToString() + "'");
                    decimal p7 = (decimal)select_app1p.Rows[0]["app_cumm_saving_amt"];
                    txttn16d.Text = p7.ToString("C").Replace("$", "").Replace("RM", "");
                    txttp.Text = p7.ToString("C").Replace("$", "").Replace("RM", "");
                    decimal p8 = (decimal)select_app1p.Rows[0]["cal_deposit_amt"];
                    TextBox15.Text = p8.ToString("C").Replace("$", "").Replace("RM", "");
                    TextBox16.Text = p8.ToString("C").Replace("$", "").Replace("RM", "");
                    decimal p9 = (decimal)select_app1p.Rows[0]["caj_postage"];
                    TextBox1.Text = p9.ToString("C").Replace("$", "").Replace("RM", "");
                    TextBox9.Text = p9.ToString("C").Replace("$", "").Replace("RM", "");
                    decimal p10 = (decimal)select_app1p.Rows[0]["caj_late_pay"];
                    TextBox21.Text = p10.ToString("C").Replace("$", "").Replace("RM", "");
                    TextBox22.Text = p10.ToString("C").Replace("$", "").Replace("RM", "");
                    decimal p11 = (decimal)select_app1p.Rows[0]["leg_fee_amt"];
                    TextBox17.Text = p11.ToString("C").Replace("$", "").Replace("RM", "");
                    TextBox18.Text = p11.ToString("C").Replace("$", "").Replace("RM", "");

                    decimal p14 = decimal.Parse(TextBox5.Text);

                    if (ddicno.Rows[0][1].ToString() == "P")
                    {
                        //Sequnce No Adding
                        string datep = TextBox8.Text;
                        DateTime pydtp = DateTime.ParseExact(datep, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                        DateTime pydt5 = DateTime.ParseExact(datep, "dd/mm/yyyy", CultureInfo.InvariantCulture);

                        string pdate5 = pydt5.ToString("mm");
                        string pdatey = pydt5.ToString("yyyy");
                        string sMonth = DateTime.Now.ToString("MM");
                        //if (sMonth >= pdate5)
                        //{

                        String paydt1 = pydtp.ToString("yyyy-mm-dd");
                        DataTable calculation1p = new DataTable();
                        calculation1p = DBCon.Ora_Execute_table("select APP.appl_loan_dur,JBB.pjs_seq_no,ISNULL(APP.app_cumm_profit_amt,'') as app_cumm_profit_amt,ISNULL(APP.app_cumm_saving_amt,'') as app_cumm_saving_amt from jpa_jbb_pjs as JBB Inner Join jpa_application as APP on APP.app_applcn_no=JBB.pjs_applcn_no where pjs_applcn_no='" + ddicno.Rows[0]["app_applcn_no"] + "' and MONTH(pjs_pay_date)='" + pdate5 + "' and YEAR(pjs_pay_date)='" + pdatey + "'");
                        if (calculation1p.Rows.Count != 0)
                        {
                            //Baki Tempoh Bayar 1 
                            int p12 = Convert.ToInt32(calculation1p.Rows[0]["appl_loan_dur"]);
                            int p13 = Convert.ToInt32(calculation1p.Rows[0]["pjs_seq_no"]);
                            float pvalue2, pvalue23, presult, presult_1, prabt;
                            pvalue2 = p13;
                            presult = p12 - pvalue2;
                            TextBox11.Text = presult.ToString();

                            //Rebat 1 
                            decimal value3 = (decimal)calculation1p.Rows[0]["app_cumm_profit_amt"];
                            decimal sav31_1 = (decimal)calculation1p.Rows[0]["app_cumm_saving_amt"];
                            float value31 = (float)value3;
                            float b = 1;
                            pvalue2 = presult * (presult + b);
                            pvalue23 = p12 * (p12 + b);
                            presult_1 = pvalue2 / pvalue23;
                            prabt = presult_1 * value31;
                            TextBox13.Text = prabt.ToString("C").Replace("$", "").Replace("RM", "");

                            //Baki Penyelesaian Awal 1 (RM) 
                            decimal pval3 = (p14 + p9 + p10 + p11) - (p8 + sav31_1);
                            float pba12 = (float)pval3;
                            float pv = prabt;
                            float pba1 = (float)pv;
                            float pba2 = pba12 - pba1;
                            TextBox23.Text = pba2.ToString("C").Replace("$", "").Replace("RM", "");
                        }
                        else
                        {
                            TextBox11.Text = "0";
                            TextBox13.Text = "0.00";
                            TextBox23.Text = "0.00";                            
                        }
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Sila Pilih Dengan di Bulan Ini.');", true);
                        //}
                        //Sequnce No Adding
                        string pdate2 = TextBox10.Text;
                        DateTime pydt2 = DateTime.ParseExact(pdate2, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                        DateTime pydt6 = DateTime.ParseExact(pdate2, "dd/mm/yyyy", CultureInfo.InvariantCulture);

                        string pdate6 = pydt6.ToString("mm");
                        string pdate6y = pydt6.ToString("yyyy");
                        string pMonth1 = DateTime.Now.ToString("MM");
                        //if (pMonth1 != pdate6)
                        //{
                        String paydt2 = pydt2.ToString("yyyy-mm-dd");
                        DataTable calculation2 = new DataTable();
                        calculation2 = DBCon.Ora_Execute_table("select APP.appl_loan_dur,JBB.pjs_seq_no,ISNULL(APP.app_cumm_profit_amt,'') as app_cumm_profit_amt,ISNULL(APP.app_cumm_saving_amt,'') as app_cumm_saving_amt from jpa_jbb_pjs as JBB Inner Join jpa_application as APP on APP.app_applcn_no=JBB.pjs_applcn_no where pjs_applcn_no='" + ddicno.Rows[0]["app_applcn_no"] + "' and MONTH(pjs_pay_date)='" + pdate6 + "' and YEAR(pjs_pay_date)='" + pdate6y + "'");

                        string sub1 = (Convert.ToDouble(pdate6) - Convert.ToDouble(pMonth1)).ToString();
                        if (calculation2.Rows.Count != 0)
                        {
                            //Baki Tempoh Bayar 1 
                            int p15 = Convert.ToInt32(calculation2.Rows[0]["appl_loan_dur"]);
                            int p16 = Convert.ToInt32(calculation2.Rows[0]["pjs_seq_no"]);
                            float pvalue_21, pvalue_22, pvalue_23, presult_11, presult_2, prabt1;
                            pvalue_21 = p16;
                            presult_11 = p15 - pvalue_21;
                            TextBox12.Text = presult_11.ToString();

                            //Rebat 1 
                            decimal pvalue31 = (decimal)calculation2.Rows[0]["app_cumm_profit_amt"];
                            decimal sav31 = (decimal)calculation2.Rows[0]["app_cumm_saving_amt"];
                            float value312 = (float)pvalue31;
                            float b1 = 1;
                            pvalue_22 = presult_11 * (presult_11 + b1);
                            pvalue_23 = p15 * (p15 + b1);
                            presult_2 = pvalue_22 / pvalue_23;
                            prabt1 = presult_2 * value312;
                            TextBox14.Text = prabt1.ToString("C").Replace("$", "").Replace("RM", "");

                            //Baki Penyelesaian Awal 1 (RM) 
                            decimal pval_3 = (p14 + p9 + p10 + p11) - (p8 + sav31);
                            float pba_12 = (float)pval_3;
                            float pval1 = prabt1;
                            float pba_1 = (float)pval1;
                            float pba_2 = pba_12 - pba_1;
                            TextBox24.Text = pba_2.ToString("C").Replace("$", "").Replace("RM", "");
                            //}
                            //else
                            //{
                            //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Kedua-dua Bulan Tidak Harus Sama.');", true);
                            //}
                        }
                        else
                        {
                            TextBox12.Text = "0";
                            TextBox14.Text = "0.00";
                            TextBox24.Text = "0.00";
                        }

                    }


                    else if (ddicno.Rows[0][1].ToString() == "H")
                    {
                        //Sequnce No Adding
                        string datep = TextBox8.Text;
                        DateTime pydtp = DateTime.ParseExact(datep, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                        DateTime pydt5 = DateTime.ParseExact(datep, "dd/mm/yyyy", CultureInfo.InvariantCulture);

                        string pdate5 = pydt5.ToString("mm");
                        string pdatey = pydt5.ToString("yyyy");
                        string sMonth = DateTime.Now.ToString("MM");
                        //if (sMonth == pdate5)
                        //{

                        String paydt1 = pydtp.ToString("yyyy-mm-dd");
                        DataTable calculation1p = new DataTable();
                        calculation1p = DBCon.Ora_Execute_table("select APP.appl_loan_dur,JBB.hol_seq_no,ISNULL(APP.app_cumm_profit_amt,'') as app_cumm_profit_amt,ISNULL(APP.app_cumm_saving_amt,'') as app_cumm_saving_amt from jpa_jbb_holiday as JBB Inner Join jpa_application as APP on APP.app_applcn_no=JBB.hol_applcn_no where hol_applcn_no='" + ddicno.Rows[0]["app_applcn_no"] + "' and MONTH(hol_pay_date)='" + pdate5 + "' and YEAR(hol_pay_date)='" + pdatey + "'");
                        if (calculation1p.Rows.Count != 0)
                        {
                            //Baki Tempoh Bayar 1 
                            int p12 = Convert.ToInt32(calculation1p.Rows[0]["appl_loan_dur"]);
                            int p13 = Convert.ToInt32(calculation1p.Rows[0]["hol_seq_no"]);
                            float pvalue2, pvalue23, presult, presult_1, prabt;
                            pvalue2 = p13;
                            presult = p12 - pvalue2;
                            TextBox11.Text = presult.ToString();

                            //Rebat 1 
                            decimal value3 = (decimal)calculation1p.Rows[0]["app_cumm_profit_amt"];
                            decimal sav31_1 = (decimal)calculation1p.Rows[0]["app_cumm_saving_amt"];
                            float value31 = (float)value3;
                            float b = 1;
                            pvalue2 = presult * (presult + b);
                            pvalue23 = p12 * (p12 + b);
                            presult_1 = pvalue2 / pvalue23;
                            prabt = presult_1 * value31;
                            TextBox13.Text = prabt.ToString("C").Replace("$", "").Replace("RM", "");

                            //Baki Penyelesaian Awal 1 (RM) 
                            decimal pval3 = (p14 + p9 + p10 + p11) - (p8 + sav31_1);
                            float pba12 = (float)pval3;
                            float pv = prabt;
                            float pba1 = (float)pv;
                            float pba2 = pba12 - pba1;
                            TextBox23.Text = pba2.ToString("C").Replace("$", "").Replace("RM", "");
                        }
                        else
                        {
                            TextBox11.Text = "0";
                            TextBox13.Text = "0.00";
                            TextBox23.Text = "0.00";
                        }
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Sila Pilih Dengan di Bulan Ini.');", true);
                        //}
                        //Sequnce No Adding
                        string pdate2 = TextBox10.Text;
                        DateTime pydt2 = DateTime.ParseExact(pdate2, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                        DateTime pydt6 = DateTime.ParseExact(pdate2, "dd/mm/yyyy", CultureInfo.InvariantCulture);

                        string pdate6 = pydt6.ToString("mm");
                        string pdate6y = pydt6.ToString("yyyy");
                        string pMonth1 = DateTime.Now.ToString("MM");
                        //if (pMonth1 != pdate6)
                        //{
                        String paydt2 = pydt2.ToString("yyyy-mm-dd");
                        DataTable calculation2 = new DataTable();
                        calculation2 = DBCon.Ora_Execute_table("select APP.appl_loan_dur,JBB.hol_seq_no,ISNULL(APP.app_cumm_profit_amt,'') as app_cumm_profit_amt,ISNULL(APP.app_cumm_saving_amt,'') as app_cumm_saving_amt from jpa_jbb_holiday as JBB Inner Join jpa_application as APP on APP.app_applcn_no=JBB.hol_applcn_no where hol_applcn_no='" + ddicno.Rows[0]["app_applcn_no"] + "' and MONTH(hol_pay_date)='" + pdate6 + "' and YEAR(hol_pay_date)='" + pdate6y + "'");

                        string sub1 = (Convert.ToDouble(pdate6) - Convert.ToDouble(pMonth1)).ToString();
                        if (calculation2.Rows.Count != 0)
                        {
                            //Baki Tempoh Bayar 1 
                            int p15 = Convert.ToInt32(calculation2.Rows[0]["appl_loan_dur"]);
                            int p16 = Convert.ToInt32(calculation2.Rows[0]["hol_seq_no"]);
                            float pvalue_21, pvalue_22, pvalue_23, presult_11, presult_2, prabt1;
                            pvalue_21 = p16;
                            presult_11 = p15 - pvalue_21;
                            TextBox12.Text = presult_11.ToString();

                            //Rebat 1 
                            decimal pvalue31 = (decimal)calculation2.Rows[0]["app_cumm_profit_amt"];
                            decimal sav31 = (decimal)calculation2.Rows[0]["app_cumm_saving_amt"];
                            float value312 = (float)pvalue31;
                            float b1 = 1;
                            pvalue_22 = presult_11 * (presult_11 + b1);
                            pvalue_23 = p15 * (p15 + b1);
                            presult_2 = pvalue_22 / pvalue_23;
                            prabt1 = presult_2 * value312;
                            TextBox14.Text = prabt1.ToString("C").Replace("$", "").Replace("RM", "");

                            //Baki Penyelesaian Awal 1 (RM) 
                            decimal pval_3 = (p14 + p9 + p10 + p11) - (p8 + sav31);
                            float pba_12 = (float)pval_3;
                            float pval1 = prabt1;
                            float pba_1 = (float)pval1;
                            float pba_2 = pba_12 - pba_1;
                            TextBox24.Text = pba_2.ToString("C").Replace("$", "").Replace("RM", "");
                            //}
                            //else
                            //{
                            //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Kedua-dua Bulan Tidak Harus Sama.');", true);
                            //}
                        }
                        else
                        {
                            TextBox12.Text = "0";
                            TextBox14.Text = "0.00";
                            TextBox24.Text = "0.00";
                        }

                    }

                    else if (ddicno.Rows[0][1].ToString() == "L")
                    {
                        //Sequnce No Adding
                        string datep = TextBox8.Text;
                        DateTime pydtp = DateTime.ParseExact(datep, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                        DateTime pydt5 = DateTime.ParseExact(datep, "dd/mm/yyyy", CultureInfo.InvariantCulture);

                        string pdate5 = pydt5.ToString("mm");
                        string pdatey = pydt5.ToString("yyyy");
                        string sMonth = DateTime.Now.ToString("MM");
                        //if (sMonth == pdate5)
                        //{

                        String paydt1 = pydtp.ToString("yyyy-mm-dd");
                        DataTable calculation1p = new DataTable();
                        calculation1p = DBCon.Ora_Execute_table("select APP.appl_loan_dur,JBB.jwo_seq_no,ISNULL(APP.app_cumm_profit_amt,'') as app_cumm_profit_amt,ISNULL(APP.app_cumm_saving_amt,'') as app_cumm_saving_amt from jpa_jbb_writeoff as JBB Inner Join jpa_application as APP on APP.app_applcn_no=JBB.jwo_applcn_no where JBB.jwo_applcn_no='" + ddicno.Rows[0]["app_applcn_no"] + "' and MONTH(JBB.jwo_pay_date)='" + pdate5 + "' and YEAR(jwo_pay_date)='" + pdatey + "'");
                        if (calculation1p.Rows.Count != 0)
                        {
                            //Baki Tempoh Bayar 1 
                            int p12 = Convert.ToInt32(calculation1p.Rows[0]["appl_loan_dur"]);
                            int p13 = Convert.ToInt32(calculation1p.Rows[0]["jwo_seq_no"]);
                            float pvalue2, pvalue23, presult, presult_1, prabt;
                            pvalue2 = p13;
                            presult = p12 - pvalue2;
                            TextBox11.Text = presult.ToString();

                            //Rebat 1 
                            decimal value3 = (decimal)calculation1p.Rows[0]["app_cumm_profit_amt"];
                            decimal sav31_1 = (decimal)calculation1p.Rows[0]["app_cumm_saving_amt"];
                            float value31 = (float)value3;
                            float b = 1;
                            pvalue2 = presult * (presult + b);
                            pvalue23 = p12 * (p12 + b);
                            presult_1 = pvalue2 / pvalue23;
                            prabt = presult_1 * value31;
                            TextBox13.Text = prabt.ToString("C").Replace("$", "").Replace("RM", "");

                            //Baki Penyelesaian Awal 1 (RM) 
                            decimal pval3 = (p14 + p9 + p10 + p11) - (p8 + sav31_1);
                            float pba12 = (float)pval3;
                            float pv = prabt;
                            float pba1 = (float)pv;
                            float pba2 = pba12 - pba1;
                            TextBox23.Text = pba2.ToString("C").Replace("$", "").Replace("RM", "");
                            //}
                            //else
                            //{
                            //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Sila Pilih Dengan di Bulan Ini.');", true);
                            //}
                        }
                        else
                        {
                            TextBox11.Text = "0";
                            TextBox13.Text = "0.00";
                            TextBox23.Text = "0.00";
                        }
                        //Sequnce No Adding
                        string pdate2 = TextBox10.Text;
                        DateTime pydt2 = DateTime.ParseExact(pdate2, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                        DateTime pydt6 = DateTime.ParseExact(pdate2, "dd/mm/yyyy", CultureInfo.InvariantCulture);

                        string pdate6 = pydt6.ToString("mm");
                        string pdate6y = pydt6.ToString("yyyy");
                        string pMonth1 = DateTime.Now.ToString("MM");
                        //if (pMonth1 != pdate6)
                        //{
                        String paydt2 = pydt2.ToString("yyyy-mm-dd");
                        DataTable calculation2 = new DataTable();
                        calculation2 = DBCon.Ora_Execute_table("select APP.appl_loan_dur,JBB.jwo_seq_no,ISNULL(APP.app_cumm_profit_amt,'') as app_cumm_profit_amt,ISNULL(APP.app_cumm_saving_amt,'') as app_cumm_saving_amt from jpa_jbb_writeoff as JBB Inner Join jpa_application as APP on APP.app_applcn_no=JBB.jwo_applcn_no where JBB.jwo_applcn_no='" + ddicno.Rows[0]["app_applcn_no"] + "' and MONTH(JBB.jwo_pay_date)='" + pdate6 + "' and YEAR(jwo_pay_date)='" + pdate6y + "'");

                        string sub1 = (Convert.ToDouble(pdate6) - Convert.ToDouble(pMonth1)).ToString();
                        if (calculation2.Rows.Count != 0)
                        {
                            //Baki Tempoh Bayar 1 
                            int p15 = Convert.ToInt32(calculation2.Rows[0]["appl_loan_dur"]);
                            int p16 = Convert.ToInt32(calculation2.Rows[0]["jwo_seq_no"]);
                            float pvalue_21, pvalue_22, pvalue_23, presult_11, presult_2, prabt1;
                            pvalue_21 = p16;
                            presult_11 = p15 - pvalue_21;
                            TextBox12.Text = presult_11.ToString();

                            //Rebat 1 
                            decimal pvalue31 = (decimal)calculation2.Rows[0]["app_cumm_profit_amt"];
                            decimal sav31 = (decimal)calculation2.Rows[0]["app_cumm_saving_amt"];
                            float value312 = (float)pvalue31;
                            float b1 = 1;
                            pvalue_22 = presult_11 * (presult_11 + b1);
                            pvalue_23 = p15 * (p15 + b1);
                            presult_2 = pvalue_22 / pvalue_23;
                            prabt1 = presult_2 * value312;
                            TextBox14.Text = prabt1.ToString("C").Replace("$", "").Replace("RM", "");

                            //Baki Penyelesaian Awal 1 (RM) 
                            decimal pval_3 = (p14 + p9 + p10 + p11) - (p8 + sav31);
                            float pba_12 = (float)pval_3;
                            float pval1 = prabt1;
                            float pba_1 = (float)pval1;
                            float pba_2 = pba_12 - pba_1;
                            TextBox24.Text = pba_2.ToString("C").Replace("$", "").Replace("RM", "");
                        }
                        else
                        {
                            TextBox12.Text = "0";
                            TextBox14.Text = "0.00";
                            TextBox24.Text = "0.00";
                        }
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Kedua-dua Bulan Tidak Harus Sama.');", true);
                        //}
                    }


                    else if (ddicno.Rows[0][1].ToString() == "E")
                    {
                        //Sequnce No Adding
                        string datep = TextBox8.Text;
                        DateTime pydtp = DateTime.ParseExact(datep, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                        DateTime pydt5 = DateTime.ParseExact(datep, "dd/mm/yyyy", CultureInfo.InvariantCulture);

                        string pdate5 = pydt5.ToString("mm");
                        string pdatey = pydt5.ToString("yyyy");
                        string sMonth = DateTime.Now.ToString("MM");
                        //if (sMonth == pdate5)
                        //{

                        String paydt1 = pydtp.ToString("yyyy-mm-dd");
                        DataTable calculation1p = new DataTable();
                        calculation1p = DBCon.Ora_Execute_table("select APP.appl_loan_dur,JBB.ext_seq_no,ISNULL(APP.app_cumm_profit_amt,'') as app_cumm_profit_amt,ISNULL(APP.app_cumm_saving_amt,'') as app_cumm_saving_amt from jpa_jbb_extension as JBB Inner Join jpa_application as APP on APP.app_applcn_no=JBB.ext_applcn_no where JBB.ext_applcn_no='" + ddicno.Rows[0]["app_applcn_no"] + "' and MONTH(JBB.ext_pay_date)='" + pdate5 + "' and YEAR(ext_pay_date)='" + pdatey + "'");
                        if (calculation1p.Rows.Count != 0)
                        {
                            //Baki Tempoh Bayar 1 
                            int p12 = Convert.ToInt32(calculation1p.Rows[0]["appl_loan_dur"]);
                            int p13 = Convert.ToInt32(calculation1p.Rows[0]["ext_seq_no"]);
                            float pvalue2, pvalue23, presult, presult_1, prabt;
                            pvalue2 = p13;
                            presult = p12 - pvalue2;
                            TextBox11.Text = presult.ToString();

                            //Rebat 1 
                            decimal value3 = (decimal)calculation1p.Rows[0]["app_cumm_profit_amt"];
                            decimal sav31_1 = (decimal)calculation1p.Rows[0]["app_cumm_saving_amt"];
                            float value31 = (float)value3;
                            float b = 1;
                            pvalue2 = presult * (presult + b);
                            pvalue23 = p12 * (p12 + b);
                            presult_1 = pvalue2 / pvalue23;
                            prabt = presult_1 * value31;
                            TextBox13.Text = prabt.ToString("C").Replace("$", "").Replace("RM", "");

                            //Baki Penyelesaian Awal 1 (RM) 
                            decimal pval3 = (p14 + p9 + p10 + p11) - (p8 + sav31_1);
                            float pba12 = (float)pval3;
                            float pv = prabt;
                            float pba1 = (float)pv;
                            float pba2 = pba12 - pba1;
                            TextBox23.Text = pba2.ToString("C").Replace("$", "").Replace("RM", "");
                        }
                        else
                        {
                            TextBox11.Text = "0";
                            TextBox13.Text = "0.00";
                            TextBox23.Text = "0.00";
                        }
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Sila Pilih Dengan di Bulan Ini.');", true);
                        //}
                        //Sequnce No Adding
                        string pdate2 = TextBox10.Text;
                        DateTime pydt2 = DateTime.ParseExact(pdate2, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                        DateTime pydt6 = DateTime.ParseExact(pdate2, "dd/mm/yyyy", CultureInfo.InvariantCulture);

                        string pdate6 = pydt6.ToString("mm");
                        string pdate6y = pydt6.ToString("yyyy");
                        string pMonth1 = DateTime.Now.ToString("MM");
                        //if (pMonth1 != pdate6)
                        //{
                        String paydt2 = pydt2.ToString("yyyy-mm-dd");
                        DataTable calculation2 = new DataTable();
                        calculation2 = DBCon.Ora_Execute_table("select APP.appl_loan_dur,JBB.ext_seq_no,ISNULL(APP.app_cumm_profit_amt,'') as app_cumm_profit_amt,ISNULL(APP.app_cumm_saving_amt,'') as app_cumm_saving_amt from jpa_jbb_extension as JBB Inner Join jpa_application as APP on APP.app_applcn_no=JBB.ext_applcn_no where JBB.ext_applcn_no='" + ddicno.Rows[0]["app_applcn_no"] + "' and MONTH(JBB.ext_pay_date)='" + pdate6 + "' and YEAR(ext_pay_date)='" + pdate6y + "'");

                        string sub1 = (Convert.ToDouble(pdate6) - Convert.ToDouble(pMonth1)).ToString();
                        if (calculation2.Rows.Count != 0)
                        {
                            //Baki Tempoh Bayar 1 
                            int p15 = Convert.ToInt32(calculation2.Rows[0]["appl_loan_dur"]);
                            int p16 = Convert.ToInt32(calculation2.Rows[0]["ext_seq_no"]);
                            float pvalue_21, pvalue_22, pvalue_23, presult_11, presult_2, prabt1;
                            pvalue_21 = p16;
                            presult_11 = p15 - pvalue_21;
                            TextBox12.Text = presult_11.ToString();

                            //Rebat 1 
                            decimal pvalue31 = (decimal)calculation2.Rows[0]["app_cumm_profit_amt"];
                            decimal sav31 = (decimal)calculation2.Rows[0]["app_cumm_saving_amt"];
                            float value312 = (float)pvalue31;
                            float b1 = 1;
                            pvalue_22 = presult_11 * (presult_11 + b1);
                            pvalue_23 = p15 * (p15 + b1);
                            presult_2 = pvalue_22 / pvalue_23;
                            prabt1 = presult_2 * value312;
                            TextBox14.Text = prabt1.ToString("C").Replace("$", "").Replace("RM", "");

                            //Baki Penyelesaian Awal 1 (RM) 
                            decimal pval_3 = (p14 + p9 + p10 + p11) - (p8 + sav31);
                            float pba_12 = (float)pval_3;
                            float pval1 = prabt1;
                            float pba_1 = (float)pval1;
                            float pba_2 = pba_12 - pba_1;
                            TextBox24.Text = pba_2.ToString("C").Replace("$", "").Replace("RM", "");
                        }
                        else
                        {
                            TextBox12.Text = "0";
                            TextBox14.Text = "0.00";
                            TextBox24.Text = "0.00";
                        }
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Kedua-dua Bulan Tidak Harus Sama.');", true);
                        //}

                    }
                    else
                    {
                        //Sequnce No Adding
                        string datep = TextBox8.Text;
                        DateTime pydtp = DateTime.ParseExact(datep, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                        DateTime pydt5 = DateTime.ParseExact(datep, "dd/mm/yyyy", CultureInfo.InvariantCulture);

                        string pdate5 = pydt5.ToString("mm");
                        string pdatey = pydt5.ToString("yyyy");
                        string sMonth = DateTime.Now.ToString("MM");
                        //if (sMonth == pdate5)
                        //{

                        String paydt1 = pydtp.ToString("yyyy-mm-dd");
                        DataTable calculation1p = new DataTable();
                        calculation1p = DBCon.Ora_Execute_table("select APP.appl_loan_dur,JBB.jno_seq_no,ISNULL(APP.app_cumm_profit_amt,'') as app_cumm_profit_amt,ISNULL(APP.app_cumm_saving_amt,'') as app_cumm_saving_amt from jpa_jbb_normal as JBB Inner Join jpa_application as APP on APP.app_applcn_no=JBB.jno_applcn_no where JBB.jno_applcn_no='" + ddicno.Rows[0]["app_applcn_no"] + "' and MONTH(JBB.jno_pay_date)='" + pdate5 + "' and YEAR(jno_pay_date)='" + pdatey + "'");
                        if (calculation1p.Rows.Count != 0)
                        {
                            //Baki Tempoh Bayar 1 
                            int p12 = Convert.ToInt32(calculation1p.Rows[0]["appl_loan_dur"]);
                            int p13 = Convert.ToInt32(calculation1p.Rows[0]["jno_seq_no"]);
                            float pvalue2, pvalue23, presult, presult_1, prabt;
                            pvalue2 = p13;
                            presult = p12 - pvalue2;
                            TextBox11.Text = presult.ToString();

                            //Rebat 1 
                            decimal value3 = (decimal)calculation1p.Rows[0]["app_cumm_profit_amt"];
                            decimal sav31_1 = (decimal)calculation1p.Rows[0]["app_cumm_saving_amt"];
                            float value31 = (float)value3;
                            float b = 1;
                            pvalue2 = presult * (presult + b);
                            pvalue23 = p12 * (p12 + b);
                            presult_1 = pvalue2 / pvalue23;
                            prabt = presult_1 * value31;
                            TextBox13.Text = prabt.ToString("C").Replace("$", "").Replace("RM", "");

                            //Baki Penyelesaian Awal 1 (RM) 
                            decimal pval3 = (p14 + p9 + p10 + p11) - (p8 + sav31_1);
                            float pba12 = (float)pval3;
                            float pv = prabt;
                            float pba1 = (float)pv;
                            float pba2 = pba12 - pba1;
                            TextBox23.Text = pba2.ToString("C").Replace("$", "").Replace("RM", "");
                        }
                        else
                        {
                            TextBox11.Text = "0";
                            TextBox13.Text = "0.00";
                            TextBox23.Text = "0.00";
                        }
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Sila Pilih Dengan di Bulan Ini.');", true);
                        //}
                        //Sequnce No Adding
                        string pdate2 = TextBox10.Text;
                        DateTime pydt2 = DateTime.ParseExact(pdate2, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                        DateTime pydt6 = DateTime.ParseExact(pdate2, "dd/mm/yyyy", CultureInfo.InvariantCulture);

                        string pdate6 = pydt6.ToString("mm");
                        string pdate6y = pydt6.ToString("yyyy");
                        string pMonth1 = DateTime.Now.ToString("MM");
                        //if (pMonth1 != pdate6)
                        //{
                        String paydt2 = pydt2.ToString("yyyy-mm-dd");
                        DataTable calculation2 = new DataTable();
                        calculation2 = DBCon.Ora_Execute_table("select APP.appl_loan_dur,JBB.jno_seq_no,ISNULL(APP.app_cumm_profit_amt,'') as app_cumm_profit_amt,ISNULL(APP.app_cumm_saving_amt,'') as app_cumm_saving_amt from jpa_jbb_normal as JBB Inner Join jpa_application as APP on APP.app_applcn_no=JBB.jno_applcn_no where JBB.jno_applcn_no='" + ddicno.Rows[0]["app_applcn_no"] + "' and MONTH(JBB.jno_pay_date)='" + pdate6 + "' and YEAR(jno_pay_date)='" + pdate6y + "'");

                        string sub1 = (Convert.ToDouble(pdate6) - Convert.ToDouble(pMonth1)).ToString();
                        if (calculation2.Rows.Count != 0)
                        {
                            //Baki Tempoh Bayar 1 
                            int p15 = Convert.ToInt32(calculation2.Rows[0]["appl_loan_dur"]);
                            int p16 = Convert.ToInt32(calculation2.Rows[0]["jno_seq_no"]);
                            float pvalue_21, pvalue_22, pvalue_23, presult_11, presult_2, prabt1;
                            pvalue_21 = p16;
                            presult_11 = p15 - pvalue_21;
                            TextBox12.Text = presult_11.ToString();

                            //Rebat 1 
                            decimal pvalue31 = (decimal)calculation2.Rows[0]["app_cumm_profit_amt"];
                            decimal sav31 = (decimal)calculation2.Rows[0]["app_cumm_saving_amt"];
                            float value312 = (float)pvalue31;
                            float b1 = 1;
                            pvalue_22 = presult_11 * (presult_11 + b1);
                            pvalue_23 = p15 * (p15 + b1);
                            presult_2 = pvalue_22 / pvalue_23;
                            prabt1 = presult_2 * value312;
                            TextBox14.Text = prabt1.ToString("C").Replace("$", "").Replace("RM", "");

                            //Baki Penyelesaian Awal 1 (RM) 
                            decimal pval_3 = (p14 + p9 + p10 + p11) - (p8 + sav31);
                            float pba_12 = (float)pval_3;
                            float pval1 = prabt1;
                            float pba_1 = (float)pval1;
                            float pba_2 = pba_12 - pba_1;
                            TextBox24.Text = pba_2.ToString("C").Replace("$", "").Replace("RM", "");
                        }
                        else
                        {
                            TextBox12.Text = "0";
                            TextBox14.Text = "0.00";
                            TextBox24.Text = "0.00";
                        }
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Kedua-dua Bulan Tidak Harus Sama.');", true);
                        //}

                    }

                }
                else
                {                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Medan Mandatori.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Isu.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
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
              
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Ic OR NO KP Baru.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
}