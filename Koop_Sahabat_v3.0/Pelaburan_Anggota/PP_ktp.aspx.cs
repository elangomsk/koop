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

using System.Threading.Tasks;



public partial class PP_ktp : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string str;
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    SqlCommand com;
    DataTable dt = new DataTable();
    string Status = string.Empty;
    string level, userid;
    string no, chklist = string.Empty;
    string val1, val2, val3, val4, val5, val6, val7, val8;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                level = Session["level"].ToString();
                userid = Session["New"].ToString();
                no = "10101111";
                splitno();
                txtnama.Attributes.Add("Readonly", "Readonly");
                //txtwil.Attributes.Add("Readonly", "Readonly");
                //Txtcaw.Attributes.Add("Readonly", "Readonly");
                Txttuj.Attributes.Add("Readonly", "Readonly");
                TxtAmaun.Attributes.Add("Readonly", "Readonly");
                TxtTem.Attributes.Add("Readonly", "Readonly");

                TxtTarikh.Attributes.Add("Readonly", "Readonly");
                TxtTarTan.Attributes.Add("Readonly", "Readonly");
                TxtTarTanper1.Attributes.Add("Readonly", "Readonly");
                TxtTarTanPer2.Attributes.Add("Readonly", "Readonly");
                TxtTarTanPer3.Attributes.Add("Readonly", "Readonly");
                TxtTarTanPerTam.Attributes.Add("Readonly", "Readonly");
                TxtTarDut.Attributes.Add("Readonly", "Readonly");
                tar_per.Attributes.Add("Readonly", "Readonly");
                var samp = Request.Url.Query;
                if (samp != "")
                {
                    txtnokp.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    srch_Click();

                    Button6.Visible = false;
                    Button4.Visible = false;
                    Button2.Text = "Kemaskini";
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
                com.CommandText = "select app_applcn_no from jpa_application where app_applcn_no like '%' + @Search + '%' and applcn_clsed ='N'";
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

    void splitno()
    {

        string[] numberArray = new string[no.Length];
        int counter = 0;

        for (int i = 0; i < no.Length; i++)
        {
            numberArray[i] = no.Substring(counter, 1); // 1 is split length
            counter++;
        }


        Console.WriteLine(string.Join(" ", numberArray));

        val1 = numberArray[0].ToString();
        val2 = numberArray[1].ToString();
        val3 = numberArray[2].ToString();
        val4 = numberArray[3].ToString();
        val5 = numberArray[4].ToString();
        val6 = numberArray[5].ToString();
        val7 = numberArray[6].ToString();
        val8 = numberArray[7].ToString();

    }

    void srch_Click()
    {
        if (txtnokp.Text != "")
        {

            DataTable ddokdicno = new DataTable();
            ddokdicno = DBCon.Ora_Execute_table("select JA.app_name,JA.app_loan_purpose_cd,JA.app_loan_amt,JA.appl_loan_dur,JJ.jkk_approve_amt,JJ.jkk_approve_dur,RW.Wilayah_Name,RB.branch_desc,RT.tujuan_desc from jpa_application JA Left join Ref_Wilayah as RW on RW.Wilayah_Code=JA.app_region_cd Left join ref_branch as RB on RB.branch_cd=ja.app_branch_cd left join ref_tujuan as RT on RT.tujuan_cd=JA.app_loan_purpose_cd inner join jpa_jkkpa_approval JJ on JJ.jkk_applcn_no=JA.app_applcn_no where JA.app_applcn_no ='" + txtnokp.Text + "'");
            if (ddokdicno.Rows.Count != 0)
            {

                DataTable checklist = new DataTable();
                checklist = DBCon.Ora_Execute_table("select ISNULL(case when CONVERT(DATE, JP.pre_agreement_dt) = '1900-01-01' then '' else CONVERT(CHAR(10), FORMAT(JP.pre_agreement_dt,'dd/MM/yyyy', 'en-us'), 103) END, '') as pre_agreement_dt,ISNULL(case when CONVERT(DATE, JP.pre_lo_date) = '1900-01-01' then '' else CONVERT(CHAR(10), FORMAT(JP.pre_lo_date,'dd/MM/yyyy', 'en-us'), 103) END, '') as pre_lo_date,ISNULL(case when CONVERT(DATE, JP.pre_sign_loan_dt) = '1900-01-01' then '' else CONVERT(CHAR(10), FORMAT(JP.pre_sign_loan_dt,'dd/MM/yyyy', 'en-us'), 103) END, '') as pre_sign_loan_dt,ISNULL(case when CONVERT(DATE, JP.pre_sign_guarantor_dt1) = '1900-01-01' then '' else CONVERT(CHAR(10), FORMAT(JP.pre_sign_guarantor_dt1,'dd/MM/yyyy', 'en-us'), 103) END, '') as pre_sign_guarantor_dt1,ISNULL(case when CONVERT(DATE, JP.pre_sign_guarantor_dt2) = '1900-01-01' then '' else CONVERT(CHAR(10), FORMAT(JP.pre_sign_guarantor_dt2,'dd/MM/yyyy', 'en-us'), 103) END, '') as pre_sign_guarantor_dt2,ISNULL(case when CONVERT(DATE, JP.pre_sign_guarantor_dt3) = '1900-01-01' then '' else CONVERT(CHAR(10), FORMAT(JP.pre_sign_guarantor_dt3,'dd/MM/yyyy', 'en-us'), 103) END, '') as pre_sign_guarantor_dt3,ISNULL(case when CONVERT(DATE, JP.pre_sign_add_agreement) = '1900-01-01' then '' else CONVERT(CHAR(10), FORMAT(JP.pre_sign_add_agreement,'dd/MM/yyyy', 'en-us'), 103) END, '') as pre_sign_add_agreement,ISNULL(case when CONVERT(DATE, JP.pre_stamp_duty_dt) = '1900-01-01' then '' else CONVERT(CHAR(10), FORMAT(JP.pre_stamp_duty_dt,'dd/MM/yyyy', 'en-us'), 103) END, '') as pre_stamp_duty_dt,JP.pre_checklist from jpa_pre_disburse_checklist JP where JP.pre_applcn_no='" + txtnokp.Text + "'");
                if (checklist.Rows.Count == 0)
                {

                    txtnama.Text = ddokdicno.Rows[0]["app_name"].ToString();
                    //txtwil.Text = ddokdicno.Rows[0]["Wilayah_Name"].ToString();
                    //Txtcaw.Text = ddokdicno.Rows[0]["branch_desc"].ToString();
                    Txttuj.Text = ddokdicno.Rows[0]["tujuan_desc"].ToString();
                    TxtAmaun.Text = double.Parse(ddokdicno.Rows[0]["app_loan_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                    TxtTem.Text = ddokdicno.Rows[0]["appl_loan_dur"].ToString();
                    //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
                else
                {

                    txtnama.Text = ddokdicno.Rows[0]["app_name"].ToString();
                    //txtwil.Text = ddokdicno.Rows[0]["Wilayah_Name"].ToString();
                    //Txtcaw.Text = ddokdicno.Rows[0]["branch_desc"].ToString();
                    Txttuj.Text = ddokdicno.Rows[0]["tujuan_desc"].ToString();
                    TxtAmaun.Text = double.Parse(ddokdicno.Rows[0]["app_loan_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                    TxtTem.Text = ddokdicno.Rows[0]["appl_loan_dur"].ToString();

                    //string j_dt1 = checklist.Rows[0]["pre_lo_date"].ToString();
                    TxtTarikh.Text = checklist.Rows[0]["pre_lo_date"].ToString();

                    //string j_dt2 = checklist.Rows[0]["pre_sign_loan_dt"].ToString();
                    TxtTarTan.Text = checklist.Rows[0]["pre_sign_loan_dt"].ToString();

                    //string j_dt3 = checklist.Rows[0]["pre_sign_guarantor_dt1"].ToString();
                    TxtTarTanper1.Text = checklist.Rows[0]["pre_sign_guarantor_dt1"].ToString();

                    if (checklist.Rows[0]["pre_agreement_dt"].ToString() != "")
                    {
                        //string j_dt4 = checklist.Rows[0]["pre_sign_guarantor_dt2"].ToString();
                        tar_per.Text = checklist.Rows[0]["pre_agreement_dt"].ToString();
                    }
                    else
                    {
                        tar_per.Text = "";
                    }

                    if (checklist.Rows[0]["pre_sign_guarantor_dt2"].ToString() != "")
                    {
                        //string j_dt4 = checklist.Rows[0]["pre_sign_guarantor_dt2"].ToString();
                        TxtTarTanPer2.Text = checklist.Rows[0]["pre_sign_guarantor_dt2"].ToString();
                    }
                    else
                    {
                        TxtTarTanPer2.Text = "";
                    }
                    if (checklist.Rows[0]["pre_sign_guarantor_dt3"].ToString() != "")
                    {
                        //string j_dt5 = checklist.Rows[0]["pre_sign_guarantor_dt3"].ToString();
                        TxtTarTanPer3.Text = checklist.Rows[0]["pre_sign_guarantor_dt3"].ToString();
                    }
                    else
                    {
                        TxtTarTanPer3.Text = "";
                    }
                    if (checklist.Rows[0]["pre_sign_add_agreement"].ToString() != "")
                    {
                        //string j_dt6 = checklist.Rows[0]["pre_sign_add_agreement"].ToString();
                        TxtTarTanPerTam.Text = checklist.Rows[0]["pre_sign_add_agreement"].ToString();
                    }
                    else
                    {
                        TxtTarTanPerTam.Text = "";
                    }
                    if (checklist.Rows[0]["pre_stamp_duty_dt"].ToString() != "")
                    {
                        //string j_dt7 = checklist.Rows[0]["pre_stamp_duty_dt"].ToString();
                        TxtTarDut.Text = checklist.Rows[0]["pre_stamp_duty_dt"].ToString();

                    }
                    else
                    {
                        TxtTarDut.Text = "";
                    }

                    no = checklist.Rows[0]["pre_checklist"].ToString();
                    splitno();
                    if (val1 == "1")
                    {
                        chk1.Checked = true;
                    }
                    else
                    {
                        chk1.Checked = false;
                    }
                    if (val2 == "1")
                    {
                        chk2.Checked = true;
                    }
                    else
                    {
                        chk2.Checked = false;
                    }
                    if (val3 == "1")
                    {
                        chk3.Checked = true;
                    }
                    else
                    {
                        chk3.Checked = false;
                    }
                    if (val4 == "1")
                    {
                        chk4.Checked = true;
                    }
                    else
                    {
                        chk4.Checked = false;
                    }
                    if (val5 == "1")
                    {
                        chk5.Checked = true;
                    }
                    else
                    {
                        chk5.Checked = false;
                    }
                    if (val6 == "1")
                    {
                        chk6.Checked = true;
                    }
                    else
                    {
                        chk6.Checked = false;
                    }
                    if (val7 == "1")
                    {
                        chk7.Checked = true;
                    }
                    else
                    {
                        chk7.Checked = false;
                    }
                    if (val8 == "1")
                    {
                        chk8.Checked = true;
                    }
                    else
                    {
                        chk8.Checked = false;
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Ic',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }
    protected void btnsrch_Click(object sender, EventArgs e)
    {
        try
        {
            srch_Click();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void btnsmmit_Click(object sender, EventArgs e)
    {
        if (tar_per.Text != "" && TxtTarikh.Text != "" && TxtTarTan.Text != "" && TxtTarTanper1.Text != "")
        {
            if (chk1.Checked == true)
            {
                chklist = "1";
            }
            else
            {
                chklist = "0";
            }
            if (chk2.Checked == true)
            {
                chklist += "1";
            }
            else
            {
                chklist += "0";
            }
            if (chk3.Checked == true)
            {
                chklist += "1";
            }
            else
            {
                chklist += "0";
            }
            if (chk4.Checked == true)
            {
                chklist += "1";
            }
            else
            {
                chklist += "0";
            }
            if (chk5.Checked == true)
            {
                chklist += "1";
            }
            else
            {
                chklist += "0";
            }
            if (chk6.Checked == true)
            {
                chklist += "1";
            }
            else
            {
                chklist += "0";
            }
            if (chk7.Checked == true)
            {
                chklist += "1";
            }
            else
            {
                chklist += "0";
            }
            if (chk8.Checked == true)
            {
                chklist += "1";
            }
            else
            {
                chklist += "0";
            }

            string date1 = TxtTarikh.Text;
            DateTime dt1 = DateTime.ParseExact(date1, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            String tk_dt1 = dt1.ToString("yyyy-mm-dd");

            string date2 = TxtTarTan.Text;
            DateTime dt2 = DateTime.ParseExact(date2, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            String tk_dt2 = dt2.ToString("yyyy-mm-dd");

            string date3 = TxtTarTanper1.Text;
            DateTime dt3 = DateTime.ParseExact(date3, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            String tk_dt3 = dt3.ToString("yyyy-mm-dd");

            String tk_dt4 = string.Empty, tk_dt5 = string.Empty, tk_dt6 = string.Empty, tk_dt7 = string.Empty, ag_dt = string.Empty;
            if (TxtTarTanPer2.Text != "")
            {
                string date4 = TxtTarTanPer2.Text;
                DateTime dt4 = DateTime.ParseExact(date4, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                tk_dt4 = dt4.ToString("yyyy-mm-dd");
            }

            if (TxtTarTanPer3.Text != "")
            {
                string date5 = TxtTarTanPer3.Text;
                DateTime dt5 = DateTime.ParseExact(date5, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                tk_dt5 = dt5.ToString("yyyy-mm-dd");
            }
            if (TxtTarTanPerTam.Text != "")
            {
                string date6 = TxtTarTanPerTam.Text;
                DateTime dt6 = DateTime.ParseExact(date6, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                tk_dt6 = dt6.ToString("yyyy-mm-dd");
            }
            if (TxtTarDut.Text != "")
            {
                string date7 = TxtTarDut.Text;
                DateTime dt7 = DateTime.ParseExact(date7, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                tk_dt7 = dt7.ToString("yyyy-mm-dd");
            }
            if (tar_per.Text != "")
            {
                string date8 = tar_per.Text;
                DateTime dt8 = DateTime.ParseExact(date8, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                ag_dt = dt8.ToString("yyyy-mm-dd");
            }

            DataTable checklist = new DataTable();
            checklist = DBCon.Ora_Execute_table("select pre_applcn_no from jpa_pre_disburse_checklist JP where JP.pre_applcn_no ='" + txtnokp.Text + "'");
            if (checklist.Rows.Count != 0)
            {
                DataTable ddupd = new DataTable();
                ddupd = DBCon.Ora_Execute_table("update jpa_pre_disburse_checklist set pre_lo_date='" + tk_dt1 + "',pre_sign_loan_dt='" + tk_dt2 + "',pre_sign_guarantor_dt1='" + tk_dt3 + "',pre_sign_guarantor_dt2='" + tk_dt4 + "',pre_sign_guarantor_dt3='" + tk_dt5 + "',pre_sign_add_agreement='" + tk_dt6 + "',pre_stamp_duty_dt='" + tk_dt7 + "',pre_checklist='" + chklist + "',pre_upd_id='" + Session["New"].ToString() + "',pre_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',pre_agreement_dt='" + ag_dt + "' where pre_applcn_no='" + txtnokp.Text + "'");
                Session["validate_success"] = "SUCCESS";
                Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                Response.Redirect("../Pelaburan_Anggota/PP_ktp_view.aspx");
            }
            else
            {
                DBCon.Execute_CommamdText("insert into jpa_pre_disburse_checklist (pre_applcn_no,pre_lo_date,pre_sign_loan_dt,pre_sign_guarantor_dt1,pre_sign_guarantor_dt2,pre_sign_guarantor_dt3,pre_sign_add_agreement,pre_stamp_duty_dt,pre_checklist,pre_crt_id,pre_crt_dt,pre_upd_id,pre_upd_dt,pre_agreement_dt) values ('" + txtnokp.Text + "','" + tk_dt1 + "','" + tk_dt2 + "','" + tk_dt3 + "','" + tk_dt4 + "','" + tk_dt5 + "','" + tk_dt6 + "','" + tk_dt7 + "','" + chklist + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','','','" + ag_dt + "')");
                Session["validate_success"] = "SUCCESS";
                Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                Response.Redirect("../Pelaburan_Anggota/PP_ktp_view.aspx");
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void clk_bak(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Pelaburan_Anggota/PP_ktp_view.aspx");
    }

}