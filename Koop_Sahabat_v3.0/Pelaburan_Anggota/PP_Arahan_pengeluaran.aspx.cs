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


public partial class PP_Arahan_pengeluaran : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    string str;
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    SqlCommand com;
    DataTable dt = new DataTable();
    string Status = string.Empty;
    string level, userid;
    //int total = 0;
    decimal total = 0M;
    string sel_button;
    string selphase_no;
    string pay_date;
    string ctrno;
    string seq_eft;
    string m_count1;
    string m_count2;
    string m_count3;

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Button5);
        scriptManager.RegisterPostBackControl(this.Button1);
        scriptManager.RegisterPostBackControl(this.Button2);
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
                CAJ_amaun.Attributes.Add("Readonly", "Readonly");
                CAJ_bersih.Attributes.Add("Readonly", "Readonly");
                CAJ_deposit.Attributes.Add("Readonly", "Readonly");
                CAJ_ds.Attributes.Add("Readonly", "Readonly");
                CAJ_fi_p.Attributes.Add("Readonly", "Readonly");
                CAJ_fi_s.Attributes.Add("Readonly", "Readonly");
                CAJ_Keuntungan.Attributes.Add("Readonly", "Readonly");
                CAJ_ll.Attributes.Add("Readonly", "Readonly");
                CAJ_tempoh.Attributes.Add("Readonly", "Readonly");
                CAJ_tkh.Attributes.Add("Readonly", "Readonly");
                MPP_jb.Attributes.Add("Readonly", "Readonly");
                RadioButton1.Checked = true;
                var samp = Request.Url.Query;
                if (samp != "")
                {
                    Applcn_no.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    srch_Click();
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

    void srch_Click()
    {
        if (Applcn_no.Text != "")
        {
            tab1();
            DataTable ddicno = new DataTable();
            ddicno = DBCon.Ora_Execute_table("select app_applcn_no,applcn_clsed from jpa_application JA Left Join jpa_jkkpa_approval as JJA ON JJA.jkk_applcn_no=JA.app_applcn_no Left Join jpa_calculate_fee as JC ON JC.cal_applcn_no=JA.app_applcn_no where JA.app_applcn_no='" + Applcn_no.Text + "' and JJA.jkk_result_ind='L'");
            DataTable select_app = new DataTable();
            select_app = DBCon.Ora_Execute_table("select JA.app_new_icno,JA.app_name,RW.Wilayah_Name,ISNULL(RB.branch_desc, '') AS branch_desc,JC.cal_approve_amt,JC.cal_approve_dur,JC.cal_stamp_duty_amt,JC.cal_tkh_amt,JC.cal_process_fee,JC.cal_credit_fee,JC.cal_deposit_amt,JC.cal_profit_amt,JC.cal_other_fee,(JC.cal_approve_amt + JC.cal_profit_amt) as bersih from jpa_application as JA  Left Join jpa_calculate_fee as JC ON JC.cal_applcn_no=JA.app_applcn_no Left Join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd Left join ref_branch AS RB ON RB.branch_cd=JA.app_branch_cd where JA.app_applcn_no='" + Applcn_no.Text + "'");
            if (ddicno.Rows.Count == 0)
            {
                //RadioButton1.Checked = false;
                //RadioButton2.Checked = false;
                //RadioButton3.Checked = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Maklumat Carian Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
            else
            {
                //Button5.Visible = true;
                //RadioButton1.Checked = false;
                //RadioButton2.Checked = false;
                //RadioButton3.Checked = false;

                if(ddicno.Rows[0]["applcn_clsed"].ToString() == "Y")
                {
                    Button1.Visible = false;
                    Button2.Visible = false;
                    Button5.Visible = false;
                }
                else
                {
                    Button1.Visible = true;
                    Button2.Visible = true;
                    Button5.Visible = true;
                }

                MP_nama.Text = select_app.Rows[0]["app_name"].ToString();
                MP_icno.Text = select_app.Rows[0]["app_new_icno"].ToString();


                decimal amt1 = (decimal)select_app.Rows[0]["cal_approve_amt"];
                CAJ_amaun.Text = amt1.ToString("C").Replace("RM", "").Replace("$","");
                decimal amt2 = (decimal)select_app.Rows[0]["bersih"];         
                CAJ_bersih.Text = amt2.ToString("C").Replace("RM", "").Replace("$", "");
                decimal amt3 = (decimal)select_app.Rows[0]["cal_deposit_amt"];
                CAJ_deposit.Text = amt3.ToString("C").Replace("RM", "").Replace("$", "");
                decimal amt4 = (decimal)select_app.Rows[0]["cal_stamp_duty_amt"];
                CAJ_ds.Text = amt4.ToString("C").Replace("RM", "").Replace("$", "");
                decimal amt5 = (decimal)select_app.Rows[0]["cal_process_fee"];
                CAJ_fi_p.Text = amt5.ToString("C").Replace("RM", "").Replace("$", "");
                decimal amt6 = (decimal)select_app.Rows[0]["cal_credit_fee"];
                CAJ_fi_s.Text = amt6.ToString("C").Replace("RM", "").Replace("$", "");
                decimal amt7 = (decimal)select_app.Rows[0]["cal_profit_amt"];
                CAJ_Keuntungan.Text = amt7.ToString("C").Replace("RM", "").Replace("$", "");
                decimal amt8 = (decimal)select_app.Rows[0]["cal_other_fee"];
                CAJ_ll.Text = amt8.ToString("C").Replace("RM", "").Replace("$", "");
                CAJ_tempoh.Text = select_app.Rows[0]["cal_approve_dur"].ToString();
                //decimal amt9 = (decimal)select_app.Rows[0]["cal_tkh_amt"];
                //CAJ_tkh.Text = amt9.ToString("#,##.00");
                DataTable select_penama = new DataTable();
                select_penama = DBCon.Ora_Execute_table("select count(*) as cc from jpa_khairat where tkh_applcn_no='" + Applcn_no.Text + "'");
                decimal d1 = Convert.ToDecimal(select_app.Rows[0]["cal_tkh_amt"].ToString());
                decimal d2 = Convert.ToDecimal(select_penama.Rows[0]["cc"].ToString());
                //decimal d3 = d1 * d2;
                decimal amt_m = d1 * d2;
                //decimal amt9 = (decimal)select_app.Rows[0]["cal_tkh_amt"];
                CAJ_tkh.Text = amt_m.ToString("C").Replace("RM", "").Replace("$", "");


            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Sila Masukkan No Ic',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
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

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }

    }

    protected void genrate_eft(object sender, EventArgs e)
    {
        try
        {
            if (Applcn_no.Text != "")
            {
                if (RadioButton1.Checked == true)
                {
                    sel_button = "1";
                }
                else if (RadioButton2.Checked == true)
                {
                    sel_button = "2";
                }
                else if (RadioButton3.Checked == true)
                {
                    sel_button = "3";
                }
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand cmd = new SqlCommand("select pha_applcn_no,pha_seqno,pha_phase_no,pha_name,pha_reg_no,pha_bank_acc_no,pha_bank_cd,pha_pay_type_cd,pha_pay_amt,RNB.Bank_Name,pha_pay_dt,RJB.KETERANGAN from jpa_disburse left join Ref_Nama_Bank as RNB ON RNB.Bank_Code=pha_bank_cd Left join Ref_Jenis_Bayaran as RJB ON RJB.KETERANGAN_Code=pha_pay_type_cd where pha_applcn_no = '" + Applcn_no.Text + "' and pha_phase_no = '" + sel_button + "' ORDER BY pha_seqno ASC"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);

                                int[] maxLengths = new int[dt.Columns.Count];

                                for (int i = 0; i < dt.Columns.Count; i++)
                                {
                                    maxLengths[i] = dt.Columns[i].ColumnName.Length;

                                    foreach (DataRow row in dt.Rows)
                                    {
                                        if (!row.IsNull(i))
                                        {
                                            int length = row[i].ToString().Length;

                                            if (length > maxLengths[i])
                                            {
                                                maxLengths[i] = length;
                                            }
                                        }
                                    }
                                }

                                //Build the Text file data.
                                string txt = string.Empty;

                                foreach (DataRow row in dt.Rows)
                                {

                                    for (int i = 0; i < dt.Columns.Count; i++)
                                    {
                                        txt += row[dt.Columns[i]].ToString().ToUpper().PadRight(maxLengths[i] + 3);
                                    }
                                    DBCon.Execute_CommamdText("UPDATE jpa_disburse SET pha_eft_ind = '1' WHERE pha_applcn_no = '" + Applcn_no.Text + "' and pha_phase_no ='" + sel_button + "'");
                                    //Add new line.
                                    txt += "\r\n";
                                }

                                //Download the Text file.

                                //var dir = @"~/FILES/" + System.DateTime.Today.ToString("yyyyMMdd");  // folder location
                                var dir = Server.MapPath("~/FILES/EFT");  // folder location

                                if (!Directory.Exists(dir))  // if it doesn't exist, create
                                    Directory.CreateDirectory(dir);

                                var count = DateTime.Now.ToString("yyMMdd");
                                string strCurrentDate = count;
                                SqlCommand max_count = new SqlCommand("SELECT max(ctr_no) FROM  cmn_control", con);
                                con.Open();
                                Object mcount = max_count.ExecuteScalar();
                                if (mcount.ToString() == "")
                                {
                                    ctrno = "001";
                                    seq_eft = "1";
                                }
                                else
                                {
                                    object count_seq = (Convert.ToInt32(max_count.ExecuteScalar()) + 1);
                                    seq_eft = count_seq.ToString();
                                    ctrno = count_seq.ToString().PadLeft(3, '0');
                                }

                                string filename = "P" + strCurrentDate + "" + ctrno + "";
                                // use Path.Combine to combine 2 strings to a path
                                File.WriteAllText(Path.Combine(dir, "" + filename + ".txt"), txt);


                                string baseName = "Generatefile" + "\\" + filename + ".txt";


                                StreamWriter writer = null;
                                try
                                {
                                    string newpath = Server.MapPath("~/FILES/EFT/" + filename + ".txt");
                                    using (StreamReader inputfile = new System.IO.StreamReader(newpath))
                                    {
                                        int cut = 0;
                                        int cnt = 00000;
                                        string line;
                                        while ((line = inputfile.ReadLine()) != null)
                                        {

                                            if (writer == null || cut == 30)
                                            {
                                                cut = 0;
                                                if (writer != null)
                                                {
                                                    writer.Close();
                                                    writer = null;
                                                }
                                                ++cnt;
                                                baseName = "Generatefile" + "\\" + filename + ".txt";

                                                writer = new StreamWriter(Server.MapPath("~/FILES/" + baseName), true);

                                            }

                                            writer.WriteLine(line.ToLower());

                                            ++cut;
                                        }
                                    }
                                }
                                finally
                                {
                                    if (writer != null)
                                        writer.Close();
                                }

                                DataTable control_row = new DataTable();
                                control_row = DBCon.Ora_Execute_table("select eft_ind from cmn_control where eft_ind = 'P'");
                                if (control_row.Rows.Count == 0)
                                {
                                    DBCon.Execute_CommamdText("insert into cmn_control (current_dt,ctr_no,eft_ind) values ('" + count + "','" + ctrno + "','P')");
                                }
                                else
                                {
                                    DBCon.Execute_CommamdText("UPDATE cmn_control SET current_dt='" + count + "',ctr_no='" + ctrno + "' WHERE eft_ind = 'P'");
                                }
                                //DBCon.Execute_CommamdText("UPDATE cmn_control SET current_dt = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',ctr_no = strCtrNo  WHERE eft_ind='P'");
                                
                                string filePath = Server.MapPath("~/FILES/EFT/" + filename.ToUpper() + ".txt");
                                Response.ContentType = ContentType;
                                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                                Response.WriteFile(filePath);
                                Response.End();
                                tab1();
                                //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Berjaya Dimuat Turun');window.location ='PP_Arahan_pengeluaran.aspx';", true);
                            }

                        }
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Sila Masukkan No Ic',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }

    protected void BindGridview(object sender, EventArgs e)
    {
        gvSelected.Visible = true;
        grid();
    }

    protected void click_print(object sender, EventArgs e)
    {
        {
            try
            {
                if (Applcn_no.Text != "")
                {
                    //Path
                    con.Open();
                    DataTable ddicno_pha1 = new DataTable();
                    ddicno_pha1 = DBCon.Ora_Execute_table("SELECT * FROM  jpa_disburse WHERE pha_applcn_no = '" + Applcn_no.Text + "' and pha_phase_no = '1'");
                    if (ddicno_pha1.Rows.Count != 0)
                    {
                        SqlCommand max_count1 = new SqlCommand("select SUM(pha_pay_amt) from jpa_disburse where pha_applcn_no = '" + Applcn_no.Text + "' and pha_phase_no = '1'", con);
                        Decimal mcount1 = (decimal)max_count1.ExecuteScalar();
                        m_count1 = double.Parse( mcount1.ToString()).ToString("C").Replace("$","").Replace("RM", "");
                    }
                    else
                    {
                        m_count1 = "0.00";
                    }
                    if (RadioButton1.Checked == true)
                    {
                        sel_button = "1";
                    }
                    else if (RadioButton2.Checked == true)
                    {
                        sel_button = "2";
                    }
                    else if (RadioButton3.Checked == true)
                    {
                        sel_button = "3";
                    }
                    DataTable ddicno_pha2 = new DataTable();
                    ddicno_pha2 = DBCon.Ora_Execute_table("SELECT * FROM  jpa_disburse WHERE pha_applcn_no = '" + Applcn_no.Text + "' and pha_phase_no = '2'");
                    if (ddicno_pha2.Rows.Count != 0)
                    {
                        SqlCommand max_count2 = new SqlCommand("select SUM(pha_pay_amt) from jpa_disburse where pha_applcn_no = '" + Applcn_no.Text + "' and pha_phase_no = '2'", con);
                        Decimal mcount2 = (decimal)max_count2.ExecuteScalar();
                        m_count2 = double.Parse(mcount2.ToString()).ToString("C").Replace("$", "").Replace("RM", ""); 
                    }
                    else
                    {
                        m_count2 = "0.00";
                    }
                    DataTable ddicno_pha3 = new DataTable();
                    ddicno_pha3 = DBCon.Ora_Execute_table("SELECT * FROM  jpa_disburse WHERE pha_applcn_no = '" + Applcn_no.Text + "' and pha_phase_no = '3'");
                    if (ddicno_pha3.Rows.Count != 0)
                    {
                        SqlCommand max_count3 = new SqlCommand("select SUM(pha_pay_amt) from jpa_disburse where pha_applcn_no = '" + Applcn_no.Text + "' and pha_phase_no = '3'", con);
                        Decimal mcount3 = (decimal)max_count3.ExecuteScalar();
                        m_count3 = double.Parse(mcount3.ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                    }
                    else
                    {
                        m_count3 = "0.00";
                    }

                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    dt = DBCon.Ora_Execute_table("select JA.app_applcn_no,JA.app_bank_acc_no,JA.app_new_icno,JA.app_name,RW.Wilayah_Name,ISNULL(RB.branch_desc, '') AS branch_desc,JC.cal_approve_amt,JC.cal_approve_dur,JC.cal_stamp_duty_amt,JC.cal_tkh_amt,JC.cal_process_fee,JC.cal_credit_fee,JC.cal_deposit_amt,JC.cal_profit_amt,JC.cal_other_fee,(JC.cal_approve_amt + JC.cal_profit_amt) as bersih,RNB.Bank_Name,JJA.jkk_approve_amt from jpa_application as JA Left join jpa_jkkpa_approval as JJA ON JJA.jkk_applcn_no=JA.app_applcn_no  Left Join jpa_calculate_fee as JC ON JC.cal_applcn_no=JA.app_applcn_no Left Join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd Left join ref_branch AS RB ON RB.branch_cd=JA.app_branch_cd left join Ref_Nama_Bank as RNB ON RNB.Bank_Code=JA.app_bank_cd where JA.app_applcn_no='" + Applcn_no.Text + "'");
                    DataTable dt_phase = new DataTable();
                    dt_phase = DBCon.Ora_Execute_table("select jb.pha_name,jb.pha_phase_amt,jb.pha_bank_acc_no,rb.Bank_Name from jpa_disburse as jb left join Ref_Nama_Bank as rb on rb.Bank_Code=jb.pha_bank_cd WHERE pha_applcn_no='" + Applcn_no.Text + "' and pha_phase_no = '" + sel_button + "' ");

                    Rptviwer_Arahan.Reset();
                    ds.Tables.Add(dt);
                    ds.Tables.Add(dt_phase);

                    Rptviwer_Arahan.LocalReport.DataSources.Clear();

                    Rptviwer_Arahan.LocalReport.ReportPath = "Pelaburan_Anggota/Arahan.rdlc";
                    ReportDataSource rds = new ReportDataSource("Ara_peng", dt);
                    ReportDataSource rds1 = new ReportDataSource("Ara_peng_phase", dt_phase);

                    Rptviwer_Arahan.LocalReport.DataSources.Add(rds);
                    Rptviwer_Arahan.LocalReport.DataSources.Add(rds1);
                    string app_amt = dt.Rows[0]["jkk_approve_amt"].ToString();
                    string div_amt = (double.Parse(CAJ_ds.Text) + double.Parse(CAJ_fi_p.Text) + double.Parse(CAJ_deposit.Text) + double.Parse(CAJ_ll.Text) + double.Parse(CAJ_tkh.Text)).ToString();
                    decimal d1 = Convert.ToDecimal(app_amt);
                    decimal d2 = Convert.ToDecimal(div_amt);
                    decimal d3 = d1 - d2;
                    string jum_bersih = d3.ToString("C").Replace("$","").Replace("RM", "");

                    ReportParameter[] rptParams = new ReportParameter[]{
                    new ReportParameter("BPP", m_count1),
                    new ReportParameter("PK", m_count2),
                    new ReportParameter("BPK", m_count3),
                    new ReportParameter("CAJ_ds", CAJ_ds.Text),
                    new ReportParameter("CAJ_fi_p", CAJ_fi_p.Text),
                    new ReportParameter("CAJ_deposit", CAJ_deposit.Text),
                    new ReportParameter("CAJ_ll", CAJ_ll.Text),
                     new ReportParameter("CAJ_tkh", CAJ_tkh.Text),
                     new ReportParameter("Bersih", jum_bersih)
                     };
                    Rptviwer_Arahan.LocalReport.SetParameters(rptParams);

                    //Refresh
                    Rptviwer_Arahan.LocalReport.Refresh();
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

                    byte[] bytes = Rptviwer_Arahan.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);

                    Response.Clear();

                    Response.ClearHeaders();

                    Response.ClearContent();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=Arahan_Pengeluaran_" + Applcn_no.Text + "." + extension);
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.Clear();
                    Response.End();
                    tab1();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Medan Input Adalah Mandatori',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    protected void grid()
    {
        gvSelected.Visible = true;
        con.Open();
        SqlCommand cmd = new SqlCommand("select pha_applcn_no,pha_seqno,pha_phase_no,pha_name,pha_reg_no,pha_bank_acc_no,pha_bank_cd,pha_pay_type_cd,pha_pay_amt,RNB.Bank_Name,pha_pay_dt,RJB.KETERANGAN from jpa_disburse left join Ref_Nama_Bank as RNB ON RNB.Bank_Code=pha_bank_cd Left join Ref_Jenis_Bayaran as RJB ON RJB.KETERANGAN_Code=pha_pay_type_cd where pha_applcn_no = '" + Applcn_no.Text + "' and pha_phase_no = '" + selphase_no + "' ORDER BY pha_seqno ASC", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {

            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gvSelected.DataSource = ds;
            gvSelected.DataBind();
            int columncount = gvSelected.Rows[0].Cells.Count;
            gvSelected.Rows[0].Cells.Clear();
            gvSelected.Rows[0].Cells.Add(new TableCell());
            gvSelected.Rows[0].Cells[0].ColumnSpan = columncount;
            gvSelected.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
            gvSelected.FooterRow.Cells[6].Text = "<strong>JUMLAH AMAUN PENGELUARAN (RM)</strong>";
            gvSelected.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
        }
        else
        {
            gvSelected.DataSource = ds;
            gvSelected.DataBind();
            gvSelected.FooterRow.Cells[6].Text = "<strong>JUMLAH AMAUN PENGELUARAN (RM)</strong>";
            gvSelected.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            DataTable ddicno_eft = new DataTable();
            ddicno_eft = DBCon.Ora_Execute_table("SELECT Distinct pha_eft_ind FROM  jpa_disburse WHERE pha_applcn_no = '" + Applcn_no.Text + "' and pha_phase_no = '" + selphase_no + "' and pha_eft_ind = '1'");
            if (ddicno_eft.Rows.Count != 0)
            {
                Button1.Attributes.Add("disabled", "disabled");
            }
            else
            {
                Button1.Attributes.Remove("disabled");
            }
        }
        con.Close();
    }
    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSelected.PageIndex = e.NewPageIndex;
        grid();
        gvSelected.DataBind();
    }

    protected void gvEmp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (DataBinder.Eval(e.Row.DataItem, "pha_pay_amt") != DBNull.Value)
            {
                Decimal ss = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "pha_pay_amt"));
                total += ss;
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            System.Web.UI.WebControls.Label lblamount = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblTotal");
            lblamount.Text = double.Parse(total.ToString()).ToString("C").Replace("$","").Replace("RM", "");
            MPP_jb.Text = double.Parse(total.ToString()).ToString("C").Replace("$", "").Replace("RM", "");
        }
    }

    public void tab1()
    {
        if (RadioButton1.Checked == true)
        {
            if (Applcn_no.Text != "")
            {
                selphase_no = "1";
                grid();
                RadioButton2.Checked = false;
                RadioButton3.Checked = false;
                Button5.Visible = true;
                Button1.Visible = true;
                Button2.Visible = true;
            }
            else
            {
                RadioButton2.Checked = false;
                RadioButton3.Checked = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Ic',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
    }
    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton1.Checked == true)
        {
            if (Applcn_no.Text != "")
            {
                selphase_no = "1";
                grid();
                RadioButton2.Checked = false;
                RadioButton3.Checked = false;
                Button5.Visible = true;
                Button1.Visible = true;
                Button2.Visible = true;
            }
            else
            {
                RadioButton2.Checked = false;
                RadioButton3.Checked = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Ic',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
    }
    protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton2.Checked == true)
        {
            if (Applcn_no.Text != "")
            {
                selphase_no = "2";
                grid();
                RadioButton1.Checked = false;
                RadioButton3.Checked = false;
                Button5.Visible = true;
                Button1.Visible = true;
                Button2.Visible = true;
            }
            else
            {
                RadioButton1.Checked = false;
                RadioButton3.Checked = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Ic',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
    }
    protected void RadioButton3_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton3.Checked == true)
        {
            if (Applcn_no.Text != "")
            {
                selphase_no = "3";
                grid();
                RadioButton1.Checked = false;
                RadioButton2.Checked = false;
                Button5.Visible = true;
                Button1.Visible = true;
                Button2.Visible = true;
            }
            else
            {
                RadioButton1.Checked = false;
                RadioButton2.Checked = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Ic',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

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
        Response.Redirect("../Pelaburan_Anggota/PP_Arahan_pengeluaran_view.aspx");
    }

    protected void btncoa_Click(object sender, EventArgs e)
    {
        try
        {
            if (CAJ_amaun.Text != "" && CAJ_ds.Text != "" && CAJ_fi_p.Text != "" && CAJ_tkh.Text != "" && CAJ_deposit.Text != "")
            {
                userid = Session["New"].ToString();
                DataTable dtGL = new DataTable();
                string ket = Applcn_no.Text + MP_nama.Text;
                decimal plus = Convert.ToDecimal(CAJ_ds.Text) + Convert.ToDecimal(CAJ_fi_p.Text) + Convert.ToDecimal(CAJ_tkh.Text) + Convert.ToDecimal(CAJ_deposit.Text);
                decimal bamt = Convert.ToDecimal(CAJ_amaun.Text) - plus;
                dtGL = DBCon.Ora_Execute_table("insert into KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,crt_id,cr_dt,kat_akaun,GL_invois_no,ref2,GL_process_dt,GL_desc1,GL_nama_kod,GL_sts)values('03.03','" + CAJ_amaun.Text + "','0.00','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','03','" + Applcn_no.Text + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + ket + "  Arahan Pengeluaran','03.03','A')");
                dtGL = DBCon.Ora_Execute_table("insert into KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,crt_id,cr_dt,kat_akaun,GL_invois_no,ref2,GL_process_dt,GL_desc1,GL_nama_kod,GL_sts)values('11.02','0.00','" + CAJ_ds.Text + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','11','" + Applcn_no.Text + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + ket + "  Caj Duti Setem','11.02','A')");
                dtGL = DBCon.Ora_Execute_table("insert into KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,crt_id,cr_dt,kat_akaun,GL_invois_no,ref2,GL_process_dt,GL_desc1,GL_nama_kod,GL_sts)values('11.03','0.00','" + CAJ_fi_p.Text + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','11','" + Applcn_no.Text + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + ket + "  Caj Pemprosesan','11.03','A')");
                dtGL = DBCon.Ora_Execute_table("insert into KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,crt_id,cr_dt,kat_akaun,GL_invois_no,ref2,GL_process_dt,GL_desc1,GL_nama_kod,GL_sts)values('04.13.02','0.00','" + CAJ_tkh.Text + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','04','" + Applcn_no.Text + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + ket + "  Tabung Khairat Hutang','04.13.02','A')");
                dtGL = DBCon.Ora_Execute_table("insert into KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,crt_id,cr_dt,kat_akaun,GL_invois_no,ref2,GL_process_dt,GL_desc1,GL_nama_kod,GL_sts)values('04.11.03','0.00','" + CAJ_deposit.Text + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','04','" + Applcn_no.Text + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + ket + "  Deposit I-Usahawan 2 Bulan','04.11.03','A')");
                dtGL = DBCon.Ora_Execute_table("insert into KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,crt_id,cr_dt,kat_akaun,GL_invois_no,ref2,GL_process_dt,GL_desc1,GL_nama_kod,GL_sts)values('03.14.01','0.00','" + bamt + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','03','" + Applcn_no.Text + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + ket + "  Arahan Pengeluaran','03.14.01','A')");
                tab1();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}