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

public partial class PP_SEMAKAN_PEMBIAYAAN : System.Web.UI.Page
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
    String fmdate = String.Empty;
    String tmdate = String.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Button2);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                level = Session["level"].ToString();
                userid = Session["New"].ToString();
                f_date.Attributes.Add("Readonly", "Readonly");
                t_date.Attributes.Add("Readonly", "Readonly");
                grid();


            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }
    protected void btnsrch_Click(object sender, EventArgs e)
    {
        try
        {
            if (f_date.Text != "" && t_date.Text != "")
            {
                string fdate = f_date.Text;
                DateTime ft = DateTime.ParseExact(fdate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                fmdate = ft.ToString("yyyy-mm-dd");

                string tdate = t_date.Text;
                DateTime tt = DateTime.ParseExact(tdate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                tmdate = tt.ToString("yyyy-mm-dd");

                DataTable ddokdicno_date = new DataTable();
                ddokdicno_date = DBCon.Ora_Execute_table("select * from jpa_write_off as jw left join jpa_application as ja on ja.app_applcn_no=jw.wri_applcn_no and app_sts_cd='Y' where jw.wri_crt_dt BETWEEN DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) AND DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and ja.applcn_clsed ='N'");
                if (ddokdicno_date.Rows.Count > 0)
                {
                    grid();
                }
                else
                {
                    grid();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Maklumat Carian Tidak Dijumpai');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Sila Masukkan Input Carian');", true);
                grid();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    void grid()
    {
        //Button5.Visible = false;
        if (f_date.Text != "")
        {
            string fdate = f_date.Text;
            DateTime ft = DateTime.ParseExact(fdate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            fmdate = ft.ToString("yyyy-mm-dd");
        }
        if (t_date.Text != "")
        {
            string tdate = t_date.Text;
            DateTime tt = DateTime.ParseExact(tdate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            tmdate = tt.ToString("yyyy-mm-dd");
        }
        con.Open();
        SqlCommand cmd = new SqlCommand("select * from jpa_application as ja Left join jpa_write_off as jw on jw.wri_applcn_no=ja.app_applcn_no where ja.app_sts_cd='Y' and ja.applcn_clsed ='N' and jw.wri_crt_dt BETWEEN DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) AND DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1)", con);
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
            gvSelected.DataSource = ds;
            gvSelected.DataBind();

        }
        con.Close();
    }

    protected void click_print(object sender, EventArgs e)
    {
        {
            try
            {
                grid();
                if (f_date.Text != "" && t_date.Text != "")
                {
                    //Path
                    if (f_date.Text != "")
                    {
                        string fdate = f_date.Text;
                        DateTime ft = DateTime.ParseExact(fdate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                        fmdate = ft.ToString("yyyy-mm-dd");
                    }
                    if (t_date.Text != "")
                    {
                        string tdate = t_date.Text;
                        DateTime tt = DateTime.ParseExact(tdate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                        tmdate = tt.ToString("yyyy-mm-dd");
                    }

                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    dt = DBCon.Ora_Execute_table("select * from jpa_application as ja Left join jpa_write_off as jw on jw.wri_applcn_no=ja.app_applcn_no where ja.app_sts_cd='Y' and jw.wri_crt_dt BETWEEN DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) AND DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1)");

                    Rptviwer_lt.Reset();
                    ds.Tables.Add(dt);

                    Rptviwer_lt.LocalReport.DataSources.Clear();

                    Rptviwer_lt.LocalReport.ReportPath = "Pelaburan_Anggota/pp_semak.rdlc";
                    ReportDataSource rds = new ReportDataSource("pp_shk", dt);

                    Rptviwer_lt.LocalReport.DataSources.Add(rds);

                    ReportParameter[] rptParams = new ReportParameter[]{
                    new ReportParameter("fromDate", f_date.Text),
                     new ReportParameter("toDate", t_date.Text)
                     };


                    Rptviwer_lt.LocalReport.SetParameters(rptParams);

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


                    Response.AddHeader("content-disposition", "attachment; filename= Semak_Hapus_Kira_" + f_date.Text + "_To_" + t_date.Text + "." + extension);

                    Response.BinaryWrite(bytes);

                    //Response.Write("<script>");
                    //Response.Write("window.open('', '_newtab');");
                    //Response.Write("</script>");
                    Response.Flush();

                    Response.End();
                    
                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Medan Input Adalah Mandatori');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSelected.PageIndex = e.NewPageIndex;
        grid();
        gvSelected.DataBind();
    }

    protected void btn_rstclick(object sender, EventArgs e)
    {
        Response.Redirect("PP_SEMAKAN_PEMBIAYAAN.aspx");
    }

   
}