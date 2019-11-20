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


public partial class PP_LOG_TUGASAN : System.Web.UI.Page
{
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string str;
    DBConnection DBCon = new DBConnection();
    SqlCommand com;
    DataTable dt = new DataTable();
    string Status = string.Empty;
    string level, userid;
    string seqno = string.Empty;
    string cc_no = string.Empty;
    string cc_no1 = string.Empty;
    string spiicno = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Button2);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                //if (Request.UrlReferrer.ToString() != null)
                //{
                //    ViewState["ReferrerUrl"] = Request.UrlReferrer.ToString();
                //}
                //else
                //{
                //    ViewState["ReferrerUrl"] = "";
                //}
                level = Session["level"].ToString();
                userid = Session["New"].ToString();
                MP_nama.Attributes.Add("Readonly", "Readonly");
                //MP_icno.Attributes.Add("Readonly", "Readonly");
                //MP_wilayah.Attributes.Add("Readonly", "Readonly");
                //MP_cawangan.Attributes.Add("Readonly", "Readonly");
                CAJ_amaun.Attributes.Add("Readonly", "Readonly");
                CAJ_tempoh.Attributes.Add("Readonly", "Readonly");
                TextBox1.Attributes.Add("Readonly", "Readonly");
                TextBox2.Attributes.Add("Readonly", "Readonly");
                TextBox4.Attributes.Add("Readonly", "Readonly");
                txtTime.Attributes.Add("Readonly", "Readonly");
                DataTable dd_name1 = new DataTable();
                dd_name1 = DBCon.Ora_Execute_table("select * from mem_member where mem_new_icno='" + Session["New"].ToString() + "'");
                DataTable dd_name2 = new DataTable();
                dd_name2 = DBCon.Ora_Execute_table("select * from hr_staff_profile where stf_icno='" + Session["New"].ToString() + "'");
                DataTable dd_name3 = new DataTable();
                dd_name3 = DBCon.Ora_Execute_table("select * from KK_User_Login where KK_userid='" + Session["New"].ToString() + "'");
                if (dd_name1.Rows.Count != 0)
                {
                    TextBox6.Text = dd_name1.Rows[0]["mem_name"].ToString();
                }
                else if (dd_name2.Rows.Count != 0)
                {
                    TextBox6.Text = dd_name1.Rows[0]["stf_name"].ToString();
                }
                else
                {
                    TextBox6.Text = dd_name3.Rows[0]["KK_username"].ToString();
                }
                TextBox1.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtTime.Text = DateTime.Now.ToString("HH:mm:ss tt");
                grid();
                var samp = Request.Url.Query;

                if (samp != "")
                {
                    Applcn_no.Text = Request.QueryString["spi_icno"].ToString();
                    srch();
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
                com.CommandText = "select app_applcn_no from jpa_application where app_applcn_no like '%' + @Search + '%' and app_sts_cd='Y' and applcn_clsed ='N'";
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
            grid();
            if (Applcn_no.Text != "")
            {

                DataTable ddicno = new DataTable();
                ddicno = DBCon.Ora_Execute_table("select distinct app_applcn_no,ISNULL(JJA.log_applcn_no,'') as log_applcn_no from jpa_application JA Left Join jpa_call_log as JJA ON JJA.log_applcn_no=JA.app_applcn_no where JA.app_sts_cd='Y' and '" + Applcn_no.Text + "' IN(JA.app_applcn_no, JA.app_new_icno)");
                DataTable select_app = new DataTable();
                select_app = DBCon.Ora_Execute_table("select JA.app_new_icno,JA.app_name,RW.Wilayah_Name,ISNULL(RB.branch_desc, '') AS branch_desc,JA.app_loan_amt,JA.appl_loan_dur from jpa_application as JA  Left Join jpa_calculate_fee as JC ON JC.cal_applcn_no=JA.app_applcn_no Left Join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd Left join ref_branch AS RB ON RB.branch_cd=JA.app_branch_cd where '" + Applcn_no.Text + "' IN(JA.app_applcn_no, JA.app_new_icno)");
                if (select_app.Rows.Count != 0)
                {

                    if (ddicno.Rows.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                    else
                    {
                        MP_nama.Text = select_app.Rows[0]["app_name"].ToString();
                        //MP_icno.Text = select_app.Rows[0]["app_new_icno"].ToString();
                        //MP_wilayah.Text = select_app.Rows[0]["Wilayah_Name"].ToString();
                        //MP_cawangan.Text = select_app.Rows[0]["branch_desc"].ToString();
                        decimal amt1 = (decimal)select_app.Rows[0]["app_loan_amt"];
                        CAJ_amaun.Text = amt1.ToString("C").Replace("$", "");
                        CAJ_tempoh.Text = select_app.Rows[0]["appl_loan_dur"].ToString();
                        Button1.Visible = true;
                        Button5.Visible = false;



                        //TextBox1.Text = "";
                        TextBox2.Text = "";
                        //txtTime.Text = "";
                        TextBox4.Text = "";
                        Textarea1.Value = "";

                        //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
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


    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {

            if (Applcn_no.Text != "")
            {
                if (TextBox1.Text != "" && TextBox2.Text != "" && txtTime.Text != "" && TextBox4.Text != "" && Textarea1.Value != "")
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

                    SqlCommand max_count = new SqlCommand("SELECT max(log_seq_no) FROM  jpa_call_log where log_applcn_no='" + cc_no + "'", con);
                    con.Open();
                    Object mcount = max_count.ExecuteScalar();
                    if (mcount.ToString() == "")
                    {
                        seqno = "1";
                    }
                    else
                    {
                        object count_seq = (Convert.ToInt32(max_count.ExecuteScalar()) + 1);
                        seqno = count_seq.ToString();
                    }


                    string date1 = TextBox1.Text;
                    DateTime pydt1 = DateTime.ParseExact(date1, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                    String paydt1 = pydt1.ToString("yyyy-mm-dd");

                    string date2 = TextBox2.Text;
                    DateTime pydt2 = DateTime.ParseExact(date2, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                    String paydt2 = pydt2.ToString("yyyy-mm-dd");

                    string date3 = TextBox4.Text;
                    DateTime pydt3 = DateTime.ParseExact(date3, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                    String paydt3 = pydt3.ToString("yyyy-mm-dd");

                    DBCon.Execute_CommamdText("insert into jpa_call_log (log_applcn_no,log_call_dt,log_call_time,log_followup_dt,log_promise_pay_dt,log_remark,log_crt_id,log_crt_dt,log_seq_no,log_call_npegwai) values ('" + cc_no + "','" + paydt1 + "','" + txtTime.Text + "','" + paydt2 + "','" + paydt3 + "','" + Textarea1.Value.Replace("'", "''") + "','" + Session["New"].ToString() + "','" + DateTime.Now + "','" + seqno + "','" + TextBox6.Text.Replace("'", "''") + "')");                    
                    con.Close();
                    TextBox2.Text = "";
                    TextBox4.Text = "";
                    Textarea1.Value = "";
                    grid();                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Red Mark Field Should be Mandatory',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }

            }
            else
            {
                grid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Please Enter No Permohonan',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Semak Isu',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }

    void grid()
    {
        //Button5.Visible = false;
        TextBox1.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtTime.Text = DateTime.Now.ToString("HH:mm:ss tt");
        var ic_count1 = Applcn_no.Text.Length;
        DataTable app_icno1 = new DataTable();
        app_icno1 = DBCon.Ora_Execute_table("select app_applcn_no from jpa_application JA where JA.app_new_icno= '" + Applcn_no.Text + "'");
        if (ic_count1 == 12)
        {
            cc_no1 = app_icno1.Rows[0]["app_applcn_no"].ToString();
        }
        else
        {
            cc_no1 = Applcn_no.Text;
        }
        con.Open();
        SqlCommand cmd = new SqlCommand("select * from jpa_call_log where log_applcn_no='" + cc_no1 + "' ORDER BY log_crt_dt ASC", con);
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
        }
        else
        {
            Button2.Visible = true;
            //Button3.Visible = true;
            Button1.Visible = true;
            gvSelected.DataSource = ds;
            gvSelected.DataBind();

        }
        con.Close();
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {

        LinkButton btnButton = sender as LinkButton;
        GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;

        System.Web.UI.WebControls.Label appicno = (System.Web.UI.WebControls.Label)gvRow.FindControl("app_no");
        System.Web.UI.WebControls.Label icseqno = (System.Web.UI.WebControls.Label)gvRow.FindControl("ic_seqno");


        DataTable ddokdicno = new DataTable();
        ddokdicno = DBCon.Ora_Execute_table("select distinct log_applcn_no from jpa_call_log where log_applcn_no='" + appicno.Text + "'");

        if (ddokdicno.Rows.Count != 0)
        {
            DataTable ddokdicno1 = new DataTable();
            ddokdicno1 = DBCon.Ora_Execute_table("select * from jpa_call_log where log_applcn_no='" + appicno.Text + "'  and log_seq_no='" + icseqno.Text + "'");
            if (ddokdicno1.Rows.Count != 0)
            {
                TextBox1.Text = Convert.ToDateTime(ddokdicno1.Rows[0]["log_call_dt"]).ToString("dd/MM/yyyy");
                if (ddokdicno1.Rows[0]["log_followup_dt"].ToString() != "")
                {
                    TextBox2.Text = Convert.ToDateTime(ddokdicno1.Rows[0]["log_followup_dt"]).ToString("dd/MM/yyyy");
                }
                else
                {
                    TextBox2.Text = "";
                }
                txtTime.Text = ddokdicno1.Rows[0]["log_call_time"].ToString();
                if (ddokdicno1.Rows[0]["log_promise_pay_dt"].ToString() != "")
                {
                    TextBox4.Text = Convert.ToDateTime(ddokdicno1.Rows[0]["log_promise_pay_dt"]).ToString("dd/MM/yyyy");
                }
                else
                {
                    TextBox4.Text = "";
                }
                Textarea1.Value = ddokdicno1.Rows[0]["log_remark"].ToString();
                if (ddokdicno1.Rows[0]["log_upd_id"].ToString() == "")
                {
                    DataTable ddname_1 = new DataTable();
                    ddname_1 = DBCon.Ora_Execute_table("select * from mem_member where mem_new_icno='" + ddokdicno1.Rows[0]["log_crt_id"].ToString() + "'");
                    DataTable ddname_2 = new DataTable();
                    ddname_2 = DBCon.Ora_Execute_table("select * from hr_staff_profile where stf_icno='" + ddokdicno1.Rows[0]["log_crt_id"].ToString() + "'");
                    DataTable ddname_3 = new DataTable();
                    ddname_3 = DBCon.Ora_Execute_table("select * from KK_User_Login where KK_userid='" + ddokdicno1.Rows[0]["log_crt_id"].ToString() + "'");
                    DataTable ddname_4 = new DataTable();
                    ddname_4 = DBCon.Ora_Execute_table("select * from KK_User_Login where KK_userid='" + Session["New"].ToString() + "'");
                    if (ddname_1.Rows.Count != 0)
                    {
                        TextBox6.Text = ddname_1.Rows[0]["mem_name"].ToString();
                    }
                    else if (ddname_2.Rows.Count != 0)
                    {
                        TextBox6.Text = ddname_2.Rows[0]["stf_name"].ToString();
                    }
                    else if (ddname_3.Rows.Count != 0)
                    {
                        TextBox6.Text = ddname_3.Rows[0]["KK_username"].ToString();
                    }
                    else
                    {
                        TextBox6.Text = ddname_4.Rows[0]["KK_username"].ToString();
                    }
                }
                else
                {
                    DataTable ddname_1 = new DataTable();
                    ddname_1 = DBCon.Ora_Execute_table("select * from mem_member where mem_new_icno='" + ddokdicno1.Rows[0]["log_upd_id"].ToString() + "'");
                    DataTable ddname_2 = new DataTable();
                    ddname_2 = DBCon.Ora_Execute_table("select * from hr_staff_profile where stf_icno='" + ddokdicno1.Rows[0]["log_upd_id"].ToString() + "'");
                    DataTable ddname_3 = new DataTable();
                    ddname_3 = DBCon.Ora_Execute_table("select * from KK_User_Login where KK_userid='" + ddokdicno1.Rows[0]["log_upd_id"].ToString() + "'");
                    DataTable ddname_4 = new DataTable();
                    ddname_4 = DBCon.Ora_Execute_table("select * from KK_User_Login where KK_userid='" + Session["New"].ToString() + "'");
                    if (ddname_1.Rows.Count != 0)
                    {
                        TextBox6.Text = ddname_1.Rows[0]["mem_name"].ToString();
                    }
                    else if (ddname_2.Rows.Count != 0)
                    {
                        TextBox6.Text = ddname_2.Rows[0]["stf_name"].ToString();
                    }
                    else if (ddname_3.Rows.Count != 0)
                    {
                        TextBox6.Text = ddname_3.Rows[0]["KK_username"].ToString();
                    }
                    else
                    {
                        TextBox6.Text = ddname_4.Rows[0]["KK_username"].ToString();
                    }
                }
                TextBox3.Text = ddokdicno1.Rows[0]["log_seq_no"].ToString();
                Button1.Visible = false;
                Button5.Visible = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }

    }

    protected void btnupdt_Click(object sender, EventArgs e)
    {
        try
        {

            if (Applcn_no.Text != "")
            {
                if (TextBox1.Text != "" && TextBox2.Text != "" && txtTime.Text != "" && TextBox4.Text != "" && Textarea1.Value != "")
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

                    string date1 = TextBox1.Text;
                    DateTime pydt1 = DateTime.ParseExact(date1, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                    String paydt1 = pydt1.ToString("yyyy-mm-dd");

                    string date2 = TextBox2.Text;
                    DateTime pydt2 = DateTime.ParseExact(date2, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                    String paydt2 = pydt2.ToString("yyyy-mm-dd");

                    string date3 = TextBox4.Text;
                    DateTime pydt3 = DateTime.ParseExact(date3, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                    String paydt3 = pydt3.ToString("yyyy-mm-dd");

                    DBCon.Execute_CommamdText("UPDATE jpa_call_log SET log_call_dt='" + paydt1 + "',log_call_time='" + txtTime.Text + "',log_followup_dt='" + paydt2 + "',log_promise_pay_dt='" + paydt3 + "',log_remark='" + Textarea1.Value.Replace("'", "''") + "',log_upd_id='" + Session["New"].ToString() + "',log_upd_dt='" + DateTime.Now + "',log_call_npegwai='" + TextBox6.Text.Replace("'", "''") + "' where log_applcn_no='" + cc_no + "' and log_seq_no='" + TextBox3.Text + "'");
                    TextBox2.Text = "";
                    TextBox4.Text = "";
                    Textarea1.Value = "";
                    Button5.Visible = false;
                    grid();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);

                    con.Close();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Red Mark Field Should be Mandatory',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Please Enter No Permohonan',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Semak Isu',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }

    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSelected.PageIndex = e.NewPageIndex;
        grid();
        gvSelected.DataBind();
    }

    protected void click_print(object sender, EventArgs e)
    {
        {
            try
            {
                grid();
                if (Applcn_no.Text != "")
                {
                    //Path
                    var ic_count1 = Applcn_no.Text.Length;
                    DataTable app_icno1 = new DataTable();
                    app_icno1 = DBCon.Ora_Execute_table("select app_applcn_no from jpa_application JA where JA.app_new_icno= '" + Applcn_no.Text + "'");
                    if (ic_count1 == 12)
                    {
                        cc_no1 = app_icno1.Rows[0]["app_applcn_no"].ToString();
                    }
                    else
                    {
                        cc_no1 = Applcn_no.Text;
                    }
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    dt = DBCon.Ora_Execute_table("select * from (select ja.app_applcn_no,JA.app_new_icno,JA.app_name,RW.Wilayah_Name,ISNULL(RB.branch_desc, '') AS branch_desc,JA.app_loan_amt,JA.appl_loan_dur from jpa_application as JA  Left Join jpa_calculate_fee as JC ON JC.cal_applcn_no=JA.app_applcn_no Left Join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd Left join ref_branch AS RB ON RB.branch_cd=JA.app_branch_cd where JA.app_applcn_no='" + cc_no1 + "') as a full outer join (select * from jpa_call_log where log_applcn_no='" + cc_no1 + "') as b on a.app_applcn_no = b.log_applcn_no order by b.log_seq_no");

                    Rptviwer_lt.Reset();
                    ds.Tables.Add(dt);

                    Rptviwer_lt.LocalReport.DataSources.Clear();

                    Rptviwer_lt.LocalReport.ReportPath = "Pelaburan_Anggota/log_tugasan.rdlc";
                    ReportDataSource rds = new ReportDataSource("l_tugasan", dt);

                    Rptviwer_lt.LocalReport.DataSources.Add(rds);

                    //Refresh
                    Rptviwer_lt.LocalReport.Refresh();
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

                    byte[] bytes = Rptviwer_lt.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);


                    Response.Buffer = true;

                    Response.Clear();

                    Response.ClearHeaders();

                    Response.ClearContent();

                    Response.ContentType = "application/pdf";


                    Response.AddHeader("content-disposition", "attachment; filename=LOG_TUGASAN_" + cc_no1 + "." + extension);

                    Response.BinaryWrite(bytes);

                    //Response.Write("<script>");
                    //Response.Write("window.open('', '_newtab');");
                    //Response.Write("</script>");
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
            }
        }
    }

    protected void btn_rstclick(object sender, EventArgs e)
    {
        Response.Redirect("PP_LOG_TUGASAN.aspx");
    }

    protected void btn_rstclick1(object sender, EventArgs e)
    {
        Response.Redirect("PP_LOG_TUGASAN.aspx");
    }

    protected void click_batal(object sender, EventArgs e)
    {
        Session["sess_no"] = Applcn_no.Text;
        //object referrer = ViewState["ReferrerUrl"];
        if (Session["sess_no"] != "")
            Response.Redirect("PP_SMPembiayaan.aspx");
        else
            grid();

    }

    protected void clk_bak(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Pelaburan_Anggota/PP_LOG_TUGASAN_view.aspx");
    }

}