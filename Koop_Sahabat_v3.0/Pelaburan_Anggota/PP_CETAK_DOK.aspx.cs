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


public partial class PP_CETAK_DOK : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string str;
    DBConnection DBCon = new DBConnection();
    DBConnection Dblog = new DBConnection();
    StudentWebService service = new StudentWebService();
    SqlCommand com;
    DataTable dt = new DataTable();
    string Status = string.Empty;
    string level, userid;
    string cc_no = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Button4);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
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
                var samp = Request.Url.Query;
                if (samp != "")
                {
                    Applcn_no.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    car1();
                    car2();
                }
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }
    //protected void Button9_Click(object sender, EventArgs e)
    //{
    //    car1();
    //    car2();
    //}

    protected void rset_click(object sender, EventArgs e)
    {
        Response.Redirect("PP_CETAK_DOK.aspx");
    }

    public void car1()
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
                    TextBox4.Text = a5.ToString("C").Replace("$", "");
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
    public void car2()
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
                    DataTable select_app1 = new DataTable();
                    select_app1 = DBCon.Ora_Execute_table("select ext_extension_dur,ext_new_pay_dur,FORMAT(ISNULL(ext_upd_perm,''),'dd/MM/yyyy', 'en-us') as ext_upd_perm,FORMAT(ISNULL(ext_upd_mes,''),'dd/MM/yyyy', 'en-us') as ext_upd_mes,FORMAT(ISNULL(ext_upd_tmula,''),'dd/MM/yyyy', 'en-us') as ext_upd_tmula,FORMAT(ISNULL(ext_upd_ttam,''),'dd/MM/yyyy', 'en-us') as ext_upd_ttam,ISNULL(ext_upd_bil,'') as ext_upd_bil from jpa_extension where ext_applcn_no='" + ddicno.Rows[0][0].ToString() + "'");
                    if (select_app1.Rows.Count != 0)
                    {
                        TextBox10.Text = select_app1.Rows[0]["ext_extension_dur"].ToString();
                        ahk.Text = select_app1.Rows[0]["ext_new_pay_dur"].ToString();
                        TextBox6.Text = select_app1.Rows[0]["ext_upd_bil"].ToString();
                        if (select_app1.Rows[0]["ext_upd_tmula"].ToString() == "01/01/1900")
                        {
                            txttarmula.Text = "";
                        }
                        else
                        {
                            txttarmula.Text = select_app1.Rows[0]["ext_upd_tmula"].ToString();
                        }

                        if (select_app1.Rows[0]["ext_upd_ttam"].ToString() == "01/01/1900")
                        {
                            txtTTamat.Text = "";
                        }
                        else
                        {
                            txtTTamat.Text = select_app1.Rows[0]["ext_upd_ttam"].ToString();
                        }

                        if (select_app1.Rows[0]["ext_upd_mes"].ToString() == "01/01/1900")
                        {
                            TextBox7.Text = "";
                        }
                        else
                        {
                            TextBox7.Text = select_app1.Rows[0]["ext_upd_mes"].ToString();
                        }

                        if (select_app1.Rows[0]["ext_upd_perm"].ToString() == "01/01/1900")
                        {
                            Txtfromdate.Text = "";
                        }
                        else
                        {
                            Txtfromdate.Text = select_app1.Rows[0]["ext_upd_perm"].ToString();
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
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Semak Isu',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }
    protected void Button3_Click(object sender, EventArgs e)
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
            string phdate1 = string.Empty, phdate2 = string.Empty, phdate3 = string.Empty, phdate4 = string.Empty;

            string d1 = TextBox7.Text;
            DateTime today1 = DateTime.ParseExact(d1, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            phdate1 = today1.ToString("yyyy-mm-dd");

            string d2 = Txtfromdate.Text;
            DateTime today2 = DateTime.ParseExact(d2, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            phdate2 = today2.ToString("yyyy-mm-dd");

            string d3 = txttarmula.Text;
            DateTime today3 = DateTime.ParseExact(d3, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            phdate3 = today3.ToString("yyyy-mm-dd");

            string d4 = txtTTamat.Text;
            DateTime today4 = DateTime.ParseExact(d4, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            phdate4 = today4.ToString("yyyy-mm-dd");

            DataTable dupd = new DataTable();
            dupd = DBCon.Ora_Execute_table("update jpa_extension set ext_upd_perm='" + phdate2 + "',ext_upd_mes='" + phdate1 + "',ext_upd_tmula='" + phdate3 + "',ext_upd_ttam='" + phdate4 + "',ext_upd_bil='" + TextBox6.Text + "',ext_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',ext_upd_id='" + Session["New"].ToString() + "' where ext_applcn_no='" + cc_no + "'");
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);

        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        try
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
                DataSet ds = new DataSet();
                DataTable dtn = new DataTable();

                dt = DBCon.Ora_Execute_table("select app_current_jbb_ind,month(getdate()) as cdate,year(getdate()) as cyear from jpa_application where app_applcn_no='" + cc_no + "'");
                if (dt.Rows[0][0].ToString().Trim() == "N")
                {
                    dtn = DBCon.Ora_Execute_table("select app_name,app_permnt_address,app_new_icno,app_loan_amt,appl_loan_dur,app_start_pay_dt,app_end_pay_dt,cal_profit_rate,cal_installment_amt,(jjn.jno_seq_no - 1)  as seq_no,jjn.jno_balance_amt as bal_amt,jjn.jno_pay_date as pdate from jpa_application ja inner join jpa_calculate_fee jcf on jcf.cal_applcn_no=ja.app_applcn_no inner join jpa_jbb_normal jjn on jjn.jno_applcn_no=ja.app_applcn_no  where ja.app_applcn_no='" + cc_no + "' and month(jjn.jno_pay_date)='" + dt.Rows[0][1].ToString() + "' and year(jjn.jno_pay_date)='" + dt.Rows[0][2].ToString() + "' and app_current_jbb_ind='" + dt.Rows[0][0].ToString() + "'");
                }
                else if (dt.Rows[0][0].ToString().Trim() == "E")
                {
                    dtn = DBCon.Ora_Execute_table("select app_name,app_permnt_address,app_new_icno,app_loan_amt,appl_loan_dur,app_start_pay_dt,app_end_pay_dt,cal_profit_rate,cal_installment_amt,(jjn.ext_seq_no - 1)  as seq_no,jjn.ext_balance_amt as bal_amt,jjn.ext_pay_date as pdate from jpa_application ja inner join jpa_calculate_fee jcf on jcf.cal_applcn_no=ja.app_applcn_no inner join jpa_jbb_extension jjn on jjn.ext_applcn_no=ja.app_applcn_no  where ja.app_applcn_no='" + cc_no + "' and month(jjn.ext_pay_date)='" + dt.Rows[0][1].ToString() + "' and year(jjn.ext_pay_date)='" + dt.Rows[0][2].ToString() + "' and app_current_jbb_ind='" + dt.Rows[0][0].ToString() + "'");
                }
                else if (dt.Rows[0][0].ToString().Trim() == "H")
                {
                    dtn = DBCon.Ora_Execute_table("select app_name,app_permnt_address,app_new_icno,app_loan_amt,appl_loan_dur,app_start_pay_dt,app_end_pay_dt,cal_profit_rate,cal_installment_amt,(jjn.hol_seq_no - 1)  as seq_no,jjn.hol_balance_amt as bal_amt,jjn.hol_pay_date as pdate from jpa_application ja inner join jpa_calculate_fee jcf on jcf.cal_applcn_no=ja.app_applcn_no inner join jpa_jbb_holiday jjn on jjn.hol_applcn_no=ja.app_applcn_no  where ja.app_applcn_no='" + cc_no + "' and month(jjn.hol_pay_date)='" + dt.Rows[0][1].ToString() + "' and year(jjn.hol_pay_date)='" + dt.Rows[0][2].ToString() + "' and app_current_jbb_ind='" + dt.Rows[0][0].ToString() + "'");
                }
                else if (dt.Rows[0][0].ToString().Trim() == "P")
                {
                    dtn = DBCon.Ora_Execute_table("select app_name,app_permnt_address,app_new_icno,app_loan_amt,appl_loan_dur,app_start_pay_dt,app_end_pay_dt,cal_profit_rate,cal_installment_amt,(jjn.pjs_seq_no - 1)  as seq_no,jjn.pjs_balance_amt as bal_amt,jjn.pjs_pay_date as pdate from jpa_application ja inner join jpa_calculate_fee jcf on jcf.cal_applcn_no=ja.app_applcn_no inner join jpa_jbb_pjs jjn on jjn.pjs_applcn_no=ja.app_applcn_no  where ja.app_applcn_no='" + cc_no + "' and month(jjn.pjs_pay_date)='" + dt.Rows[0][1].ToString() + "' and year(jjn.pjs_pay_date)='" + dt.Rows[0][2].ToString() + "' and app_current_jbb_ind='" + dt.Rows[0][0].ToString() + "'");
                }
                else if (dt.Rows[0][0].ToString().Trim() == "L")
                {
                    dtn = DBCon.Ora_Execute_table("select app_name,app_permnt_address,app_new_icno,app_loan_amt,appl_loan_dur,app_start_pay_dt,app_end_pay_dt,cal_profit_rate,cal_installment_amt,(jjn.jwo_seq_no - 1)  as seq_no,jjn.jwo_balance_amt as bal_amt,jjn.jwo_pay_date as pdate from jpa_application ja inner join jpa_calculate_fee jcf on jcf.cal_applcn_no=ja.app_applcn_no inner join jpa_jbb_writeoff jjn on jjn.jwo_applcn_no=ja.app_applcn_no  where ja.app_applcn_no='" + cc_no + "' and month(jjn.jwo_pay_date)='" + dt.Rows[0][1].ToString() + "' and year(jjn.jwo_pay_date)='" + dt.Rows[0][2].ToString() + "' and app_current_jbb_ind='" + dt.Rows[0][0].ToString() + "'");
                }

                DataTable dte = new DataTable();
                dte = DBCon.Ora_Execute_table("select ext_upd_perm,ext_upd_Mes,ext_upd_Tmula,ext_upd_TTam,ext_upd_Bil,ext_paid_dur,ext_extension_dur,ext_new_pay_dur from jpa_extension where ext_applcn_no='" + cc_no + "'");

                Rptviwer_cetakjbb.Reset();
                ds.Tables.Add(dtn);

                Rptviwer_cetakjbb.LocalReport.DataSources.Clear();

                Rptviwer_cetakjbb.LocalReport.ReportPath = "PELABURAN_ANGGOTA/Tambahan.rdlc";
                ReportDataSource rds = new ReportDataSource("DataSet1", dtn);
                ReportDataSource rds1 = new ReportDataSource("DataSet2", dte);
                Rptviwer_cetakjbb.LocalReport.DataSources.Add(rds);
                Rptviwer_cetakjbb.LocalReport.DataSources.Add(rds1);
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

                Response.AddHeader("content-disposition", "attachment; filename= Perjanjian_Tambahan_" + cc_no + "." + extension);

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

    protected void clk_bak(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Pelaburan_Anggota/PP_CETAK_DOK_view.aspx");
    }
}