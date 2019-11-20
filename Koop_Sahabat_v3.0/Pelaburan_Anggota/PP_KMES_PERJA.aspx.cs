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

public partial class PP_KMES_PERJA : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    DBConnection Dblog = new DBConnection();
    StudentWebService service = new StudentWebService();
    DataTable dt = new DataTable();
    string Status = string.Empty;
    string level, userid;
    string updsts;
    string cc_no = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {

                level = Session["level"].ToString();
                userid = Session["New"].ToString();
                txtname.Attributes.Add("Readonly", "Readonly");
                txtwil.Attributes.Add("Readonly", "Readonly");
                txtjumla.Attributes.Add("Readonly", "Readonly");
                txtcaw.Attributes.Add("Readonly", "Readonly");
                txttempoh.Attributes.Add("Readonly", "Readonly");
                txtamaun.Attributes.Add("Readonly", "Readonly");
                txttemp.Attributes.Add("Readonly", "Readonly");
                TextBox2.Attributes.Add("Readonly", "Readonly");
                TextBox3.Attributes.Add("Readonly", "Readonly");
                TextBox4.Attributes.Add("Readonly", "Readonly");
                TextBox5.Attributes.Add("Readonly", "Readonly");
                ahk.Attributes.Add("Readonly", "Readonly");
                TextBox10.Attributes.Add("Readonly", "Readonly");
                var samp = Request.Url.Query;
                if (samp != "")
                {
                    Applcn_no.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    srch();
                }

            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }

    }

    void srch()
    {
        if (Applcn_no.Text != "")
        {

            DataTable ddicno = new DataTable();
            ddicno = DBCon.Ora_Execute_table("select distinct ISNULL(je.ext_applcn_no,'') as ext_applcn_no from jpa_application JA inner join jpa_extension as je on je.ext_applcn_no=ja.app_applcn_no where JA.app_sts_cd='Y' and '" + Applcn_no.Text + "' IN(JA.app_applcn_no, JA.app_new_icno)");
            if (ddicno.Rows.Count != 0)
            {
                if (ddicno.Rows[0]["ext_applcn_no"] != "")
                {
                    DataTable select_app = new DataTable();
                    select_app = DBCon.Ora_Execute_table("select * from (select JA.app_applcn_no,JA.app_new_icno,JA.app_name,RW.Wilayah_Name,ISNULL(RB.branch_desc, '') AS branch_desc,JA.app_loan_amt,JA.appl_loan_dur,JA.app_permnt_address,ja.app_permnt_postcode,ja.app_permnt_state_cd,ja.app_phone_h,ja.app_phone_m,ja.app_phone_o,ja.app_mailing_address,JA.app_mailing_postcode,ja.app_mailing_state_cd,ISNULL(JA.app_cumm_installment_amt,'') as app_cumm_installment_amt,ISNULL(JA.app_cumm_pay_amt,'') as app_cumm_pay_amt,ISNULL(JA.app_backdated_amt,'') as app_backdated_amt,ISNULL(JA.app_cumm_profit_amt,'') as app_cumm_profit_amt,ISNULL(JA.app_cumm_saving_amt,'') as app_cumm_saving_amt,ISNULL(JA.app_bal_loan_amt,'') as app_bal_loan_amt ,FORMAT(EX.ext_agreemnt_dt,'dd/MM/yyyy', 'en-us') as  ext_agreemnt_dt,FORMAT(EX.ext_stmpduty_dt,'dd/MM/yyyy', 'en-us') as ext_stmpduty_dt from jpa_application as JA  Left Join jpa_calculate_fee as JC ON JC.cal_applcn_no=JA.app_applcn_no  left join jpa_extension as EX on EX.ext_applcn_no=JA.app_applcn_no Left Join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd Left join ref_branch AS RB ON RB.branch_cd=JA.app_branch_cd where JA.app_applcn_no = '" + ddicno.Rows[0]["ext_applcn_no"] + "') as a full outer join (select * from jpa_write_off  where wri_applcn_no='" + ddicno.Rows[0]["ext_applcn_no"] + "') as b on a.app_applcn_no=b.wri_applcn_no");

                    txtname.Text = select_app.Rows[0]["app_name"].ToString();
                    txtwil.Text = select_app.Rows[0]["Wilayah_Name"].ToString();
                    decimal amt1 = (decimal)select_app.Rows[0]["app_loan_amt"];
                    txtjumla.Text = amt1.ToString("C").Replace("$", "").Replace("RM", "");
                    txtcaw.Text = select_app.Rows[0]["branch_desc"].ToString();
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

                    if (TextBox10.Text != "01/01/1900" && ahk.Text != "01/01/1900")
                    {
                        TextBox10.Text = select_app.Rows[0]["ext_agreemnt_dt"].ToString();
                        ahk.Text = select_app.Rows[0]["ext_stmpduty_dt"].ToString();
                    }
                    else
                    {
                        TextBox10.Text = "";
                        ahk.Text = "";
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Permohonan Panjang Tempoh Tidak Dijumpai. Sila Lakukan Permohonan Panjang Tempoh Dahulu',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }

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

    //protected void Button9_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        srch();
    //    }
    //    catch (Exception ex)
    //    {

    //        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Semak Isu',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

    //    }
    //}

    protected void btn_rstclick(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        if (Applcn_no.Text != "")
        {
            if (TextBox10.Text != "" || ahk.Text != "")
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
                DataTable ddicno = new DataTable();
                ddicno = DBCon.Ora_Execute_table("select ext_applcn_no,ext_approve_sts_cd From jpa_extension where ext_applcn_no='" + cc_no + "'");
                string applnno = ddicno.Rows[0]["ext_applcn_no"].ToString();
                string sts = ddicno.Rows[0]["ext_approve_sts_cd"].ToString().Trim();

                if (ddicno.Rows.Count > 0)
                {
                    string datedari = TextBox10.Text;
                    DateTime dt = DateTime.ParseExact(datedari, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                    String fromdate = dt.ToString("yyyy-mm-dd");
                    string datedari1 = ahk.Text;
                    DateTime dt1 = DateTime.ParseExact(datedari1, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                    String todate = dt1.ToString("yyyy-mm-dd");
                    if (sts == "E")
                    {
                        updsts = "A";
                    }
                    else
                    {
                        updsts = sts;
                    }
                    SqlCommand up_status = new SqlCommand("update jpa_extension set ext_agreemnt_dt='" + fromdate + "',ext_stmpduty_dt='" + todate + "',ext_approve_sts_cd='" + updsts + "' where ext_applcn_no='" + Applcn_no.Text + "'", con);
                    con.Open();
                    int i = up_status.ExecuteNonQuery();
                    con.Close();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});window.location='../PELABURAN_ANGGOTA/PP_KMES_PERJA.aspx';", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Bidang.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila mesti Masukkan Kata-kata.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    protected void clk_bak(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Pelaburan_Anggota/PP_KMES_PERJA_view.aspx");
    }
}