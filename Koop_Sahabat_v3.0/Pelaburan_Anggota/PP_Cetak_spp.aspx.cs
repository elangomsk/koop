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


public partial class PP_Cetak_spp : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string str;
    DBConnection DBCon = new DBConnection();
    SqlCommand com;
    DataTable dt = new DataTable();
    string Status = string.Empty;
    string level, userid;
    int total = 0;
    string selphase_no;
    string pay_date;

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Button1);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                level = Session["level"].ToString();
                userid = Session["New"].ToString();
                MP_nama.Attributes.Add("Readonly", "Readonly");
                MP_icno.Attributes.Add("Readonly", "Readonly");
                //MP_wilayah.Attributes.Add("Readonly", "Readonly");
                //MP_cawangan.Attributes.Add("Readonly", "Readonly");
                TextBox2.Attributes.Add("Readonly", "Readonly");
                TextBox1.Attributes.Add("Readonly", "Readonly");
                TextBox3.Attributes.Add("Readonly", "Readonly");

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
    protected void Click_rujukan(object sender, EventArgs e)
    {
        try
        {

            if (Applcn_no.Text != "" && no_rujukan.Text != "")
            {
                DataTable app_rujno = new DataTable();
                app_rujno = DBCon.Ora_Execute_table("select cmn_applcn_no from cmn_ref_no where cmn_applcn_no= '" + Applcn_no.Text + "' and cmn_ref_no='" + no_rujukan.Text + "'");

                if (app_rujno.Rows.Count == 0)
                {

                    DBCon.Execute_CommamdText("INSERT into cmn_ref_no (cmn_applcn_no,cmn_ref_no,cmn_txn_dt,cmn_txn_cd,cmn_crt_id,cmn_crt_dt) values ('" + Applcn_no.Text + "','" + no_rujukan.Text + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','032505','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')");
                    DBCon.Execute_CommamdText("UPDATE  jpa_application SET app_sts_cd = 'S' WHERE app_applcn_no= '" + Applcn_no.Text + "'");
                   
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
                else
                {
                  

                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('No Rujukan Already exist.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);


            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }


    protected void btnsrch_Click(object sender, EventArgs e)
    {
        try
        {
            if (Applcn_no.Text != "")
            {
                DataTable ddicno = new DataTable();
                ddicno = DBCon.Ora_Execute_table("select app_applcn_no from jpa_application JA Left Join jpa_jkkpa_approval as JJA ON JJA.jkk_applcn_no=JA.app_applcn_no where JA.app_applcn_no='" + Applcn_no.Text + "' and JJA.jkk_result_ind='L'");
                DataTable select_app = new DataTable();
                select_app = DBCon.Ora_Execute_table("select JA.app_new_icno,JA.app_name,JA.app_loan_amt,ISNULL(JA.app_bal_loan_amt,'') as app_bal_loan_amt,RW.Wilayah_Name,ISNULL(RB.branch_desc, '') AS branch_desc, JJA.jkk_approve_amt, JJA.jkk_approve_dur from jpa_application as JA  Left Join jpa_jkkpa_approval as JJA ON JJA.jkk_applcn_no=JA.app_applcn_no Left Join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd Left join ref_branch AS RB ON RB.branch_cd=JA.app_branch_cd where JA.app_applcn_no='" + Applcn_no.Text + "'");
                if (ddicno.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
                else
                {

                    MP_nama.Text = select_app.Rows[0]["app_name"].ToString();
                    MP_icno.Text = select_app.Rows[0]["app_new_icno"].ToString();
                    //MP_wilayah.Text = select_app.Rows[0]["Wilayah_Name"].ToString();
                    //MP_cawangan.Text = select_app.Rows[0]["branch_desc"].ToString();
                    decimal amt = (decimal)select_app.Rows[0]["app_loan_amt"];
                    TextBox1.Text = amt.ToString("C").Replace("$", "").Replace("RM", "");
                    TextBox2.Text = select_app.Rows[0]["jkk_approve_dur"].ToString();
                    decimal amt_bak = (decimal)select_app.Rows[0]["app_bal_loan_amt"];
                    TextBox3.Text = amt_bak.ToString("C").Replace("$", "").Replace("RM", "");

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Ic.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }

    }


    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    
    protected void Button1_Click(object sender, EventArgs e)
    {

        try
        {
            if (Applcn_no.Text != "")
            {
                string xxx_tname = string.Empty, xxx_fname = string.Empty;
                DataTable ddt_jbb_ind = new DataTable();
                ddt_jbb_ind = DBCon.Ora_Execute_table("select app_current_jbb_ind from jpa_application where app_applcn_no='" + Applcn_no.Text + "'");

                if (ddt_jbb_ind.Rows.Count != 0)
                {
                    string jbbind = ddt_jbb_ind.Rows[0]["app_current_jbb_ind"].ToString();
                    if (jbbind == "P")
                    {
                        xxx_tname = "pjs";
                        xxx_fname = "pjs";
                    }
                    else if (jbbind == "H")
                    {
                        xxx_tname = "holiday";
                        xxx_fname = "hol";
                    }
                    else if (jbbind == "E")
                    {
                        xxx_tname = "extension";
                        xxx_fname = "ext";
                    }
                    else if (jbbind == "L")
                    {
                        xxx_tname = "writeoff";
                        xxx_fname = "jwo";
                    }
                    else
                    {
                        xxx_tname = "normal";
                        xxx_fname = "jno";
                    }
                }
                //Path
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt = DBCon.Ora_Execute_table("select * from (select app_new_icno,app_name,app_applcn_no,ISNULL(app_mailing_address,'') app_mailing_address,app_mailing_postcode,ISNULL(KA.Area_Name, '') AS Area_Name,app_end_pay_dt,RJP.Description,jg.gua_name from jpa_application JA Left join jpa_guarantor as jg on jg.gua_applcn_no=ja.app_applcn_no Left Join jpa_jkkpa_approval as JJA ON JJA.jkk_applcn_no=JA.app_applcn_no Left Join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd Left join ref_branch AS RB ON RB.branch_cd=JA.app_branch_cd left join Ref_Kawasan as KA ON KA.Area_Code=JA.app_mailing_state_cd left join Ref_Jenis_Pelaburan as RJP ON RJP.Description_Code=JA.app_loan_type_cd where JA.app_applcn_no='" + Applcn_no.Text + "' and JJA.jkk_result_ind='L') as a full outer join (select cmn_applcn_no,cmn_ref_no from cmn_ref_no where cmn_applcn_no='" + Applcn_no.Text + "' and cmn_crt_dt IN (SELECT max(cmn_crt_dt) FROM cmn_ref_no where cmn_applcn_no='" + Applcn_no.Text + "')) as b on b.cmn_applcn_no='" + Applcn_no.Text + "'");

                Reportviwer.Reset();
                ds.Tables.Add(dt);
                DataTable jbb_lpdt = new DataTable();
                jbb_lpdt = DBCon.Ora_Execute_table("select FORMAT(" + xxx_fname + "_pay_date,'dd/MM/yyyy', 'en-us') as pdt from jpa_jbb_" + xxx_tname + " where " + xxx_fname + "_applcn_no='" + Applcn_no.Text + "' and " + xxx_fname + "_actual_pay_date != '1900-01-01' and " + xxx_fname + "_upd_dt =  (select top (1) " + xxx_fname + "_upd_dt from jpa_jbb_" + xxx_tname + " where " + xxx_fname + "_applcn_no='" + Applcn_no.Text + "' and " + xxx_fname + "_actual_pay_date != '1900-01-01' order by " + xxx_fname + "_pay_date DESC)");
                Reportviwer.LocalReport.DataSources.Clear();
                if (jbb_lpdt.Rows.Count != 0)
                {
                    Reportviwer.LocalReport.ReportPath = "PELABURAN_ANGGOTA/Penyelesaian_Pembiayaan.rdlc";
                    ReportDataSource rds = new ReportDataSource("CSPP", dt);
                    ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("lpdt",jbb_lpdt.Rows[0]["pdt"].ToString() )

                     };


                    Reportviwer.LocalReport.SetParameters(rptParams);
                    Reportviwer.LocalReport.DataSources.Add(rds);

                    //Refresh
                    Reportviwer.LocalReport.Refresh();
                    Warning[] warnings;

                    string[] streamids;

                    string mimeType;

                    string encoding;

                    string extension;

                    string fname = DateTime.Now.ToString("dd_MM_yyyy");

                    string devinfo;
                    devinfo = "<DeviceInfo>" + "<OutputFormat>EMF</OutputFormat>" + "<PageWidth>8.5in</PageWidth>" + "<PageHeight>11in</PageHeight>" + "<MarginTop>0.25in</MarginTop>" +
                        "<MarginLeft>0.25in</MarginLeft>" + "<MarginRight>0.25in</MarginRight>" + "<MarginBottom>0.25in</MarginBottom>" + "</DeviceInfo>";

                    byte[] bytes = Reportviwer.LocalReport.Render("PDF", devinfo, out mimeType, out encoding, out extension, out streamids, out warnings);
                    Response.Buffer = true;
                    Response.Clear();
                    Response.ClearHeaders();
                    Response.ClearContent();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment; filename=Pelepasan_Pembiayaan_" + Applcn_no.Text + "." + extension);
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
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