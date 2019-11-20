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


public partial class PP_cetak_notis : System.Web.UI.Page
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
    public string DecimalToWords(decimal number)
    {
        if (number == 0)
            return "zero";

        if (number < 0)
            return "minus " + DecimalToWords(Math.Abs(number));

        string words = "";

        int intPortion = (int)number;
        decimal fraction = (number - intPortion) * 100;
        int decPortion = (int)fraction;

        words = NumberToWords(intPortion);
        if (decPortion > 0)
        {
            words += " and ";
            words += NumberToWords(decPortion);
        }
        return words;
    }

    public static string NumberToWords(int number)
    {
        if (number == 0)
            return "zero";

        if (number < 0)
            return "minus " + NumberToWords(Math.Abs(number));

        string words = "";

        if ((number / 1000000) > 0)
        {
            words += NumberToWords(number / 1000000) + " million ";
            number %= 1000000;
        }

        if ((number / 1000) > 0)
        {
            words += NumberToWords(number / 1000) + " thousand ";
            number %= 1000;
        }

        if ((number / 100) > 0)
        {
            words += NumberToWords(number / 100) + " hundred ";
            number %= 100;
        }

        if (number > 0)
        {
            if (words != "")
                words += "and ";

            var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
            var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += "-" + unitsMap[number % 10];
            }
        }

        return words;
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

    void grid()
    {

    }

    protected void btn_rstclick(object sender, EventArgs e)
    {
        Response.Redirect("PP_cetak_notis.aspx");
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
                app_icno = DBCon.Ora_Execute_table("select app_applcn_no,isnull(app_current_jbb_ind,'') AS app_current_jbb_ind from jpa_application JA where '" + Applcn_no.Text + "' IN(JA.app_applcn_no, JA.app_new_icno)");
                if (ic_count == 12)
                {
                    cc_no = app_icno.Rows[0]["app_applcn_no"].ToString();
                }
                else
                {
                    cc_no = Applcn_no.Text;
                }
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                //dt = Dblog.Ora_Execute_table("SELECT jn.jno_applcn_no as app_no,FORMAT(jn.jno_pay_date,'dd/MM/yyyy', 'en-us') as p_dt,ISNULL(jn.jno_loan_amt,'') as l_amt,jn.jno_seq_no as seq_no,ISNULL(jn.jno_approve_amt,'') as a_amt,ISNULL(jn.jno_profit_amt,'') as pr_amt,jn.jno_actual_pay_date as ap_dt,ISNULL(jn.jno_pay_amt,'') as p_amt,ISNULL(jn.jno_actual_pay_amt,'') as ap_amt,ISNULL(jn.jno_late_excess_amt,'') as le_amt,ISNULL(jn.jno_saving_amt,'') as sa_amt,ISNULL(jn.jno_actual_saving_amt,'') as as_amt,ISNULL(jn.jno_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.jno_total_pay_amt,'') as tp_amt,ISNULL(jn.jno_balance_amt,'') as bal_amt,ISNULL(jn.jno_total_saving_amt,'') as ts_amt,jn.jno_day_late as dlate,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno FROM jpa_jbb_normal as jn left join jpa_application as ja on ja.app_applcn_no = jn.jno_applcn_no where jn.jno_applcn_no='" + cc_no + "'");
                dt = Dblog.Ora_Execute_table("select app_applcn_no,convert(varchar,(CONVERT(date,GETDATE(),103)),103)  as cdate,app_loan_type_cd,app_name,app_permnt_address,app_permnt_state_cd,  case  when COALESCE(app_backdated_amt/nullif(app_cumm_installment_amt,0),0) < 1 and COALESCE(app_backdated_amt/nullif(app_cumm_installment_amt,0),0) > 0    Then '1 Bulan' when COALESCE(app_backdated_amt/nullif(app_cumm_installment_amt,0),0) < 2 and COALESCE(app_backdated_amt/nullif(app_cumm_installment_amt,0),0) > 1    Then '2 Bulan' when COALESCE(app_backdated_amt/nullif(app_cumm_installment_amt,0),0) < 3 and COALESCE(app_backdated_amt/nullif(app_cumm_installment_amt,0),0) > 2    Then '3 Bulan' when COALESCE(app_backdated_amt/nullif(app_cumm_installment_amt,0),0) < 4 and COALESCE(app_backdated_amt/nullif(app_cumm_installment_amt,0),0) > 3    Then '4 Bulan' when COALESCE(app_backdated_amt/nullif(app_cumm_installment_amt,0),0) < 5 and COALESCE(app_backdated_amt/nullif(app_cumm_installment_amt,0),0) > 4    Then '5 Bulan' when COALESCE(app_backdated_amt/nullif(app_cumm_installment_amt,0),0) < 6 and COALESCE(app_backdated_amt/nullif(app_cumm_installment_amt,0),0) > 5    Then '6 Bulan' else '0 Bulan'  end as Tempoh,app_bal_loan_amt,app_backdated_amt ,gua_name,rjp.Description as loan_type,rn.Decription,ISNULL(app_mailing_address,'') as app_mailing_address  from jpa_application ja left join jpa_guarantor jg on ja.app_applcn_no=jg.gua_applcn_no left join Ref_Jenis_Pelaburan rjp on rjp.Description_Code=ja.app_loan_type_cd left join Ref_Negeri rn on rn.Decription_Code=ja.app_permnt_state_cd  where app_applcn_no='" + cc_no + "' order by gua_name ");


                //dt = DBCon.Ora_Execute_table("select ja.app_name,ja.app_new_icno,ja.app_applcn_no,ja.appl_loan_dur,ISNULL(ja.app_installment_amt,'') as app_installment_amt,ja.app_loan_type_cd,tp.tpj_approve_amt,tp.tpj_profit_amt,tp.tpj_pay_date,tp.tpj_actual_pay_date,tp.tpj_approve_amt,tp.tpj_pay_amt,tp.tpj_actual_pay_amt,tp.tpj_late_excess_amt,tp.tpj_saving_amt,tp.tpj_actual_saving_amt,tp.tpj_saving_late_excess_amt,tp.tpj_total_pay_amt,tp.tpj_balance_amt,tp.tpj_total_saving_amt,tp.tpj_day_late from jpa_jbb_temp_pjs as tp left join jpa_application as ja on ja.app_applcn_no=tp.tpj_applcn_no and ja.app_sts_cd='Y' where tp.tpj_applcn_no='" + cc_no + "'");

                Rptviwer_cetakjbb.Reset();
                ds.Tables.Add(dt);

                Rptviwer_cetakjbb.LocalReport.DataSources.Clear();

                Rptviwer_cetakjbb.LocalReport.ReportPath = "PELABURAN_ANGGOTA/dd.rdlc";
                ReportDataSource rds = new ReportDataSource("Notis", dt);

                Rptviwer_cetakjbb.LocalReport.DataSources.Add(rds);
                Rptviwer_cetakjbb.LocalReport.DisplayName = "TANPA_PRASANGKA_" + DateTime.Now.ToString("ddMMyyyy");
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

                Response.AddHeader("content-disposition", "attachment; filename=Cetak_Notis_" + cc_no + "." + extension);

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

  
    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {
            if (Applcn_no.Text != "")
            {
                var ic_count = Applcn_no.Text.Length;
                //Path var ic_count = Applcn_no.Text.Length;
                DataTable app_icno = new DataTable();
                app_icno = DBCon.Ora_Execute_table("select app_applcn_no,isnull(app_current_jbb_ind,'') AS app_current_jbb_ind from jpa_application JA where '" + Applcn_no.Text + "' IN(JA.app_applcn_no, JA.app_new_icno)");
                if (ic_count == 12)
                {
                    cc_no = app_icno.Rows[0]["app_applcn_no"].ToString();
                }
                else
                {
                    cc_no = Applcn_no.Text;
                }

                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                //dt = Dblog.Ora_Execute_table("SELECT jn.jno_applcn_no as app_no,FORMAT(jn.jno_pay_date,'dd/MM/yyyy', 'en-us') as p_dt,ISNULL(jn.jno_loan_amt,'') as l_amt,jn.jno_seq_no as seq_no,ISNULL(jn.jno_approve_amt,'') as a_amt,ISNULL(jn.jno_profit_amt,'') as pr_amt,jn.jno_actual_pay_date as ap_dt,ISNULL(jn.jno_pay_amt,'') as p_amt,ISNULL(jn.jno_actual_pay_amt,'') as ap_amt,ISNULL(jn.jno_late_excess_amt,'') as le_amt,ISNULL(jn.jno_saving_amt,'') as sa_amt,ISNULL(jn.jno_actual_saving_amt,'') as as_amt,ISNULL(jn.jno_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.jno_total_pay_amt,'') as tp_amt,ISNULL(jn.jno_balance_amt,'') as bal_amt,ISNULL(jn.jno_total_saving_amt,'') as ts_amt,jn.jno_day_late as dlate,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno FROM jpa_jbb_normal as jn left join jpa_application as ja on ja.app_applcn_no = jn.jno_applcn_no where jn.jno_applcn_no='" + cc_no + "'");
                dt = Dblog.Ora_Execute_table("select app_applcn_no,convert(varchar,(CONVERT(date,GETDATE(),103)),103)  as cdate,app_loan_type_cd,app_name,app_permnt_address,app_permnt_state_cd,  case  when COALESCE(app_backdated_amt/nullif(app_cumm_installment_amt,0),0) < 1 and COALESCE(app_backdated_amt/nullif(app_cumm_installment_amt,0),0) > 0    Then '1 Bulan' when COALESCE(app_backdated_amt/nullif(app_cumm_installment_amt,0),0) < 2 and COALESCE(app_backdated_amt/nullif(app_cumm_installment_amt,0),0) > 1    Then '2 Bulan' when COALESCE(app_backdated_amt/nullif(app_cumm_installment_amt,0),0) < 3 and COALESCE(app_backdated_amt/nullif(app_cumm_installment_amt,0),0) > 2    Then '3 Bulan' when COALESCE(app_backdated_amt/nullif(app_cumm_installment_amt,0),0) < 4 and COALESCE(app_backdated_amt/nullif(app_cumm_installment_amt,0),0) > 3    Then '4 Bulan' when COALESCE(app_backdated_amt/nullif(app_cumm_installment_amt,0),0) < 5 and COALESCE(app_backdated_amt/nullif(app_cumm_installment_amt,0),0) > 4    Then '5 Bulan' when COALESCE(app_backdated_amt/nullif(app_cumm_installment_amt,0),0) < 6 and COALESCE(app_backdated_amt/nullif(app_cumm_installment_amt,0),0) > 5    Then '6 Bulan' else '0 Bulan'  end as Tempoh,app_bal_loan_amt,app_backdated_amt ,gua_name,rjp.Description as loan_type,rn.Decription,ISNULL(app_mailing_address,'') as app_mailing_address  from jpa_application ja left join jpa_guarantor jg on ja.app_applcn_no=jg.gua_applcn_no left join Ref_Jenis_Pelaburan rjp on rjp.Description_Code=ja.app_loan_type_cd left join Ref_Negeri rn on rn.Decription_Code=ja.app_permnt_state_cd  where app_applcn_no='" + cc_no + "' order by gua_name ");


                //dt = DBCon.Ora_Execute_table("select ja.app_name,ja.app_new_icno,ja.app_applcn_no,ja.appl_loan_dur,ISNULL(ja.app_installment_amt,'') as app_installment_amt,ja.app_loan_type_cd,tp.tpj_approve_amt,tp.tpj_profit_amt,tp.tpj_pay_date,tp.tpj_actual_pay_date,tp.tpj_approve_amt,tp.tpj_pay_amt,tp.tpj_actual_pay_amt,tp.tpj_late_excess_amt,tp.tpj_saving_amt,tp.tpj_actual_saving_amt,tp.tpj_saving_late_excess_amt,tp.tpj_total_pay_amt,tp.tpj_balance_amt,tp.tpj_total_saving_amt,tp.tpj_day_late from jpa_jbb_temp_pjs as tp left join jpa_application as ja on ja.app_applcn_no=tp.tpj_applcn_no and ja.app_sts_cd='Y' where tp.tpj_applcn_no='" + cc_no + "'");

                Rptviwer_cetakjbb.Reset();
                ds.Tables.Add(dt);

                Rptviwer_cetakjbb.LocalReport.DataSources.Clear();

                Rptviwer_cetakjbb.LocalReport.ReportPath = "PELABURAN_ANGGOTA/AMARAN TERAKHIR.rdlc";
                ReportDataSource rds = new ReportDataSource("Notis1", dt);

                Rptviwer_cetakjbb.LocalReport.DataSources.Add(rds);
                Rptviwer_cetakjbb.LocalReport.DisplayName = "AMARAN_TERAKHIR_" + DateTime.Now.ToString("ddMMyyyy");
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

                Response.AddHeader("content-disposition", "attachment; filename=Cetak_Notis_" + cc_no + "." + extension);

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
