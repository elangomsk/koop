using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Text;
using System.Net;
using System.Data.Common;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Net.Mail;
using System.Threading;
using System.Xml;
using System.Reflection;
using Microsoft.Reporting.WebForms;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Web.SessionState;
using System.Threading;

public partial class HR_REKOD_KEHADIRAN : System.Web.UI.Page
{
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    DataTable dt = new DataTable();
    string sqry1 = string.Empty, sqry2 = string.Empty, sqry3 = string.Empty;
    String fromdate = string.Empty, todate = string.Empty;
    string userid;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        app_language();
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
                //jaba();
                org();
                //txt_tmula.Attributes.Add("readonly", "readonly");
                //txt_takhir.Attributes.Add("readonly", "readonly");
                //txt_tmula.Text = "01/02/2015";
                //txt_takhir.Text = "15/02/2015";

                //DataTable ddicno = new DataTable();
                //ddicno = DBCon.Ora_Execute_table("select stf_staff_no,str_curr_org_cd,stf_curr_dept_cd From hr_staff_profile where stf_icno='" + Session["New"].ToString() + "' ");
                //if (ddicno.Rows.Count != 0)
                //{
                //    string stffno = ddicno.Rows[0]["stf_staff_no"].ToString();
                //    DD_Orgnsi.SelectedValue = ddicno.Rows[0]["str_curr_org_cd"].ToString();
                //    DD_JABAT.SelectedValue = ddicno.Rows[0]["stf_curr_dept_cd"].ToString();
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Rekod Tidak Dijumpai');", true);
                //}

            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    void app_language()
    {
        if (Session["New"] != null)
        {
            DataTable ste_set = new DataTable();
            ste_set = DBCon.Ora_Execute_table("select * from site_settings where ID IN ('1')");

            DataTable gt_lng = new DataTable();
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('558','448','559','64','1489','1491','1493','497','1288','1376','15')");

            //System.out.print(Arrays.toString(alg.id));
            //result_out = gt_lng.ToString();
            //System.Console.Out.WriteLine();
            CultureInfo culinfo = Thread.CurrentThread.CurrentUICulture;
            TextInfo txtinfo = culinfo.TextInfo;

            h1_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            bb1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            bb2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            h3_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            h3_tag2.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            lbl1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            lbl2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            lbl3_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            lbl4_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            lbl5_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            Btn_Senarai.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }

    protected void click_rst(object sender, EventArgs e)
    {
        Response.Redirect("HR_REKOD_KEHADIRAN.aspx");
    }

    protected void sel_orgbind(object sender, EventArgs e)
    {
        org_pern();
    }
    void org_pern()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select * from hr_organization_pern where op_org_id='" + DD_Orgnsi.SelectedValue + "' and Status = 'A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_org_pen.DataSource = dt;
            dd_org_pen.DataTextField = "op_perg_name";
            dd_org_pen.DataValueField = "op_perg_code";
            dd_org_pen.DataBind();
            dd_org_pen.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void sel_orgjaba(object sender, EventArgs e)
    {
        org_jaba();

    }
    void org_jaba()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select * from hr_organization_jaba where oj_perg_code='" + dd_org_pen.SelectedValue + "' and Status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_JABAT.DataSource = dt;
            DD_JABAT.DataTextField = "oj_jaba_desc";
            DD_JABAT.DataValueField = "oj_jaba_cd";
            DD_JABAT.DataBind();
            DD_JABAT.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void org()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select org_gen_id,UPPER(org_name) as org_name From hr_organization group by  org_gen_id,org_name order by org_name";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_Orgnsi.DataSource = dt;
            DD_Orgnsi.DataTextField = "org_name";
            DD_Orgnsi.DataValueField = "org_gen_id";
            DD_Orgnsi.DataBind();
            DD_Orgnsi.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //void jaba()
    //{
    //    DataSet Ds = new DataSet();
    //    try
    //    {
    //        string com = "select hr_jaba_Code,UPPER(hr_jaba_desc) as hr_jaba_desc from Ref_hr_jabatan where status='A'";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
    //        DataTable dt = new DataTable();
    //        adpt.Fill(dt);
    //        DD_JABAT.DataSource = dt;
    //        DD_JABAT.DataTextField = "hr_jaba_desc";
    //        DD_JABAT.DataValueField = "hr_jaba_Code";
    //        DD_JABAT.DataBind();
    //        DD_JABAT.Items.Insert(0, new ListItem("--- PILIH ---", ""));

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    protected void Btn_Senarai_Click(object sender, EventArgs e)
    {
        if (txt_tmula.Text != "" && txt_takhir.Text != "")
        {
            clk_print();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    //void grid()
    //{
    //    con.Open();
    //    DataTable ddicno = new DataTable();
    //    ddicno = DBCon.Ora_Execute_table("select stf_staff_no,str_curr_org_cd,stf_curr_dept_cd,OG.org_name,hr_jaba_desc From hr_staff_profile as ST left join hr_organization  as OG on OG.org_id=ST.str_curr_org_cd left join Ref_hr_jabatan as JB on JB.hr_jaba_Code=ST.stf_curr_dept_cd where stf_icno='" + Session["New"].ToString() + "' ");
    //    string stffno = ddicno.Rows[0]["stf_staff_no"].ToString();
    //    DD_Orgnsi.SelectedValue = ddicno.Rows[0]["str_curr_org_cd"].ToString();
    //    DD_JABAT.SelectedValue = ddicno.Rows[0]["stf_curr_dept_cd"].ToString();
    //    String fromdate = string.Empty, todate = string.Empty;
    //    if (txt_tmula.Text != "")
    //    {
    //        string datedari = txt_tmula.Text;
    //        DateTime dt = DateTime.ParseExact(datedari, "dd/MM/yyyy", CultureInfo.InvariantCulture);
    //        fromdate = dt.ToString("yyyy-MM-dd");
    //    }
    //    else
    //    {
    //        fromdate = "";
    //    }
    //    if (txt_takhir.Text != "")
    //    {
    //        string datedari1 = txt_takhir.Text;
    //        DateTime dt1 = DateTime.ParseExact(datedari1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
    //        todate = dt1.ToString("yyyy-MM-dd");
    //    }
    //    else
    //    {
    //        todate = "";
    //    }

    //    SqlCommand cmd = new SqlCommand("select stf_staff_no,stf_name,hr_jaw_desc,a.LIND,b.HIND from hr_staff_profile as SF left join hr_attendance as AT on AT.atd_staff_no=SF.stf_staff_no left join Ref_hr_Jawatan as JW on JW.hr_jaw_Code=stf_curr_post_cd full outer join (select atd_staff_no,COUNT(atd_hol_late_ind) as LIND From hr_attendance where ((atd_date) between ('" + fromdate + "') And ('" + todate + "')) and atd_staff_no='" + stffno + "' and atd_hol_late_ind='L' group by atd_staff_no,atd_hol_late_ind) a on a.atd_staff_no=SF.stf_staff_no full outer join (select atd_staff_no,COUNT(atd_hol_late_ind) as HIND From hr_attendance where ((atd_date) between ('" + fromdate + "') And ('" + todate + "')) and atd_staff_no='" + stffno + "' and atd_hol_late_ind='H' group by atd_staff_no,atd_hol_late_ind) b on b.atd_staff_no=SF.stf_staff_no where stf_staff_no='" + stffno + "' and ((atd_date) between ('" + fromdate + "') And ('" + todate + "')) group by a.LIND,b.HIND,stf_staff_no,stf_name,hr_jaw_desc", con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count == 0)
    //    {
    //        ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
    //        GridView1.DataSource = ds;
    //        GridView1.DataBind();
    //        int columncount = GridView1.Rows[0].Cells.Count;
    //        GridView1.Rows[0].Cells.Clear();
    //        GridView1.Rows[0].Cells.Add(new TableCell());
    //        GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
    //        GridView1.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
    //        //btn_hups.Visible = false;
    //    }
    //    else
    //    {
    //        GridView1.DataSource = ds;
    //        GridView1.DataBind();
    //    }
    //    con.Close();
    //}
    //protected void btn_cetak_Click(object sender, EventArgs e)
    //{

    //    try
    //    {
    //        Page.Header.Title = "Portal Laporan Pembiayaan CMCCS ";

    //        //string datedari = Convert.ToDateTime(txt_tmula.Text).ToString("yyyy/MM/dd");
    //        //string datehingga = Convert.ToDateTime(txt_takhir.Text).ToString("yyyy/MM/dd");
    //        string datefrom = txt_tmula.Text;
    //        DateTime dts = DateTime.ParseExact(datefrom, "dd/mm/yyyy", CultureInfo.InvariantCulture);
    //        String datedari = dts.ToString("yyyy-mm-dd");
    //        string dateto = txt_takhir.Text;
    //        DateTime dt1 = DateTime.ParseExact(dateto, "dd/mm/yyyy", CultureInfo.InvariantCulture);
    //        String datehingga = dt1.ToString("yyyy-mm-dd");

    //        Session["fformat"] = ddfformat.SelectedItem.Text;
    //        string ffile = Session["fformat"].ToString();
    //        if ((txt_tmula.Text != "") && (txt_takhir.Text != ""))
    //        {
    //            DataTable ddicno = new DataTable();
    //            ddicno = DBCon.Ora_Execute_table("select stf_staff_no From hr_staff_profile where stf_icno='" + Session["New"].ToString() + "' ");
    //            string stffno = ddicno.Rows[0]["stf_staff_no"].ToString();
    //            DataSet dsCustomers = GetData("select stf_staff_no,stf_name,hr_jaw_desc,a.LIND,b.HIND from hr_staff_profile as SF left join hr_attendance as AT on AT.atd_staff_no=SF.stf_staff_no left join Ref_hr_Jawatan as JW on JW.hr_jaw_Code=stf_curr_post_cd full outer join (select atd_staff_no,COUNT(atd_hol_late_ind) as LIND From hr_attendance where ((atd_date) between ('" + datedari + "') And ('" + datehingga + "')) and atd_staff_no='" + stffno + "' and atd_hol_late_ind='L' group by atd_staff_no,atd_hol_late_ind) a on a.atd_staff_no=SF.stf_staff_no full outer join (select atd_staff_no,COUNT(atd_hol_late_ind) as HIND From hr_attendance where ((atd_date) between ('" + datedari + "') And ('" + datehingga + "')) and atd_staff_no='" + stffno + "' and atd_hol_late_ind='H' group by atd_staff_no,atd_hol_late_ind) b on b.atd_staff_no=SF.stf_staff_no where stf_staff_no='" + stffno + "' and ((atd_date) between ('" + datedari + "') And ('" + datehingga + "')) group by a.LIND,b.HIND,stf_staff_no,stf_name,hr_jaw_desc");
    //            dt = dsCustomers.Tables[0];
    //        }
    //        ReportViewer1.Reset();

    //        List<DataRow> listResult = dt.AsEnumerable().ToList();
    //        listResult.Count();
    //        int countRow = 0;
    //        countRow = listResult.Count();

    //        if (countRow != 0)
    //        {
    //            //txtError.Text = "";
    //            //Display Report
    //            ReportViewer1.LocalReport.DataSources.Clear();

    //            ReportDataSource rds = new ReportDataSource("DataSet1", dt);

    //            ReportViewer1.LocalReport.DataSources.Add(rds);

    //            //Path
    //            ReportViewer1.LocalReport.ReportPath = "HR_REKOD_KEHADIRAN.rdlc";
    //            //Parameters
    //            ReportParameter[] rptParams = new ReportParameter[]{
    //                 //new ReportParameter("fromDate",FromDate .Text ),
    //                 //new ReportParameter("toDate",ToDate .Text )
    //                 //new ReportParameter("fromDate",datedari  ),
    //                 //new ReportParameter("toDate",datehingga ),
    //                      //new ReportParameter("caw",branch ),     
    //                        //new ReportParameter("Cdate",DateTime.Now.ToString("dd/MM/yyyy") ),     
    //                 //new ReportParameter("Date",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  )
    //                 };


    //            ReportViewer1.LocalReport.SetParameters(rptParams);
    //            //ReportViewer1.LocalReport.ExecuteReportInCurrentAppDomain(AppDomain.CurrentDomain.Evidence);
    //            //Refresh
    //            ReportViewer1.LocalReport.Refresh();
    //            if (ffile == "PDF")
    //            {
    //                Warning[] warnings;

    //                string[] streamids;

    //                string mimeType;

    //                string encoding;

    //                string extension;

    //          //      string devinfo = "<DeviceInfo>" +
    //          //"  <OutputFormat>PDF</OutputFormat>" +
    //          //"  <PageSize>A4</PageSize>" +
    //          //"  <PageWidth>8.5in</PageWidth>" +
    //          //"  <PageHeight>11in</PageHeight>" +
    //          //"  <MarginTop>0.25in</MarginTop>" +
    //          //"  <MarginLeft>0.5in</MarginLeft>" +
    //          //"  <MarginRight>0.5in</MarginRight>" +
    //          //"  <MarginBottom>0.5in</MarginBottom>" +
    //          //"</DeviceInfo>";

    //                byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);


    //                Response.Buffer = true;
    //                Response.Clear();
    //                Response.ContentType = mimeType;
    //                string extfile = DateTime.Now.ToString("dd_MM_yyyy.");
    //                Response.AddHeader("content-disposition", "inline; filename=REKOD_KEHADIRAN" + extfile + extension);
    //                Response.BinaryWrite(bytes);
    //                Response.Flush();
    //                Response.End();
    //            }
    //            else if (ffile == "Excel")
    //            {
    //                Warning[] warnings;
    //                string[] streamids;
    //                string mimeType;
    //                string encoding;
    //                string extension;
    //                string filename;

    //                byte[] bytes = ReportViewer1.LocalReport.Render(
    //                   "Excel", null, out mimeType, out encoding,
    //                    out extension,
    //                   out streamids, out warnings);

    //                filename = string.Format("{0}.{1}", "Laporan", "xls");
    //                Response.Buffer = true;
    //                Response.Clear();
    //                Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
    //                Response.ContentType = mimeType;
    //                Response.BinaryWrite(bytes);
    //                Response.Flush();
    //                Response.End();
    //            }
    //            else
    //            {
    //                Warning[] warnings;

    //                string[] streamids;

    //                string mimeType;

    //                string encoding;

    //                string extension;



    //                byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);


    //                Response.Buffer = true;
    //                Response.Clear();
    //                Response.ContentType = mimeType;
    //                Response.AddHeader("content-disposition", "inline; filename=myfile." + extension);
    //                Response.BinaryWrite(bytes);
    //                Response.Flush();
    //                Response.End();
    //            }
    //        }
    //        else if (countRow == 0)
    //        {
    //           ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Rekod tidak dijumpai. Sila pastikan semua maklumat dimasukkan dengan betul.');", true);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        //txtError.Text = ex.ToString();

    //    }

    //}
    void clk_print()
    {
        {
            try
            {
                if (DD_Orgnsi.SelectedValue != "" || dd_org_pen.SelectedValue != "" || DD_JABAT.SelectedValue != "")
                {
                    if (txt_tmula.Text != "")
                    {
                        string datedari = txt_tmula.Text;
                        DateTime dt2 = DateTime.ParseExact(datedari, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        fromdate = dt2.ToString("yyyy/MM/dd");
                    }
                    else
                    {
                        fromdate = "";
                    }
                    if (txt_takhir.Text != "")
                    {
                        string datedari1 = txt_takhir.Text;
                        DateTime dt1 = DateTime.ParseExact(datedari1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        todate = dt1.ToString("yyyy/MM/dd");
                    }
                    else
                    {
                        todate = "";
                    }
                    selqry();

                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    //dt = DBCon.Ora_Execute_table("select stf_staff_no,stf_name,hr_jaw_desc,a.LIND,b.HIND from hr_staff_profile as SF left join hr_attendance as AT on AT.atd_staff_no=SF.stf_staff_no left join Ref_hr_Jawatan as JW on JW.hr_jaw_Code=stf_curr_post_cd full outer join (select atd_staff_no,COUNT(atd_hol_late_ind) as LIND From hr_attendance where ((atd_date) between ('" + fromdate + "') And ('" + todate + "')) and atd_hol_late_ind='L' group by atd_staff_no,atd_hol_late_ind) a on a.atd_staff_no=SF.stf_staff_no full outer join (select atd_staff_no,COUNT(atd_hol_late_ind) as HIND From hr_attendance where ((atd_date) between ('" + fromdate + "') And ('" + todate + "')) and atd_hol_late_ind='H' group by atd_staff_no,atd_hol_late_ind) b on b.atd_staff_no=SF.stf_staff_no where ((atd_date) between ('" + fromdate + "') And ('" + todate + "')) group by a.LIND,b.HIND,stf_staff_no,stf_name,hr_jaw_desc");
                    dt = DBCon.Ora_Execute_table("select a.stf_staff_no,UPPER(a.stf_name) as stf_name,UPPER(ISNULL(jw.hr_jaw_desc,'')) as hr_jaw_desc,UPPER(ISNULL(op.op_perg_name,'')) as op_perg_name,UPPER(ISNULL(JB.oj_jaba_desc,'')) as oj_jaba_desc,ISNULL(b.LIND,'') LIND,ISNULL(c.HIND,'') HIND from (select * from hr_staff_profile as hp where " + sqry1 + ") as a full outer join (select hp.stf_staff_no,COUNT(atd_hol_late_ind) as LIND from hr_staff_profile as hp left join hr_attendance as ha on ha.atd_staff_no = hp.stf_staff_no and atd_date>=DATEADD(day, DATEDIFF(day, 0, '" + fromdate + "'), 0) and atd_date<=DATEADD(day, DATEDIFF(day, 0, '" + todate + "'), 0) where atd_hol_late_ind='L' and " + sqry1 + " group by stf_staff_no) as b on b.stf_staff_no=a.stf_staff_no full outer join (select hp.stf_staff_no,COUNT(atd_hol_late_ind) as HIND from hr_staff_profile as hp left join hr_attendance as ha on ha.atd_staff_no = hp.stf_staff_no and atd_date>=DATEADD(day, DATEDIFF(day, 0, '" + fromdate + "'), 0) and atd_date<=DATEADD(day, DATEDIFF(day, 0, '" + todate + "'), 0) where atd_hol_late_ind='H' and " + sqry1 + " group by stf_staff_no) as c on c.stf_staff_no=a.stf_staff_no left join hr_organization_jaba JB on jb.oj_jaba_cd=a.stf_curr_dept_cd and jb.oj_perg_code=a.stf_cur_sub_org left join hr_organization_pern OP on op.op_perg_code=a.stf_cur_sub_org left join Ref_hr_Jawatan as JW on JW.hr_jaw_Code=a.stf_curr_post_cd order by op.op_perg_code,jb.oj_jaba_cd");
                    RptviwerStudent.Reset();
                    ds.Tables.Add(dt);

                    List<DataRow> listResult = dt.AsEnumerable().ToList();
                    listResult.Count();
                    int countRow = 0;
                    countRow = listResult.Count();

                    if (DD_JABAT.SelectedValue != "")
                    {
                        sqry2 = DD_JABAT.SelectedItem.Text;
                    }
                    else
                    {
                        sqry2 = "SEMUA";
                    }

                    if (dd_org_pen.SelectedValue != "")
                    {
                        sqry3 = dd_org_pen.SelectedItem.Text;
                    }
                    else
                    {
                        sqry3 = "SEMUA";
                    }

                    RptviwerStudent.LocalReport.DataSources.Clear();
                    if (countRow != 0)
                    {
                        shw_hdr1.Visible = true;
                        RptviwerStudent.LocalReport.ReportPath = "SUMBER_MANUSIA/HR_REKOD_KEHADIRAN.rdlc";
                        ReportDataSource rds = new ReportDataSource("HR_rk", dt);
                        ReportParameter[] rptParams = new ReportParameter[]{
                        new ReportParameter("s1",DD_Orgnsi.SelectedItem.Text),
                        new ReportParameter("s2",sqry2),
                        new ReportParameter("s3",txt_tmula.Text),
                        new ReportParameter("s4",txt_takhir.Text),
                        new ReportParameter("s5",sqry3),
                     };


                        RptviwerStudent.LocalReport.SetParameters(rptParams);

                        RptviwerStudent.LocalReport.DisplayName = "Rekod_Kehadiran -" + DateTime.Now.ToString("ddMMyyyy");
                        RptviwerStudent.LocalReport.DataSources.Add(rds);

                        //Refresh
                        RptviwerStudent.LocalReport.Refresh();

                    }
                    else if (countRow == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod tidak dijumpai. Sila Pastikan Semua Maklumat Dimasukkan Dengan Betul.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    //grid();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    void selqry()
    {

        if (DD_Orgnsi.SelectedValue != "" && dd_org_pen.SelectedValue != "" && DD_JABAT.SelectedValue != "")
        {
            sqry1 = "hp.str_curr_org_cd='" + DD_Orgnsi.SelectedValue + "' and hp.stf_cur_sub_org='" + dd_org_pen.SelectedValue + "' and hp.stf_curr_dept_cd='" + DD_JABAT.SelectedValue + "'";
        }
        else if (DD_Orgnsi.SelectedValue != "" && dd_org_pen.SelectedValue == "" && DD_JABAT.SelectedValue == "")
        {
            sqry1 = "hp.str_curr_org_cd='" + DD_Orgnsi.SelectedValue + "'";
        }
        else if (DD_Orgnsi.SelectedValue != "" && dd_org_pen.SelectedValue != "" && DD_JABAT.SelectedValue == "")
        {
            sqry1 = "hp.str_curr_org_cd='" + DD_Orgnsi.SelectedValue + "' and hp.stf_cur_sub_org='" + dd_org_pen.SelectedValue + "'";
        }
        else
        {
            sqry1 = "hp.str_curr_org_cd=''";
        }
    }

    private DataSet GetData(string query)
    {
        string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
        SqlCommand cmd = new SqlCommand(query);
        using (SqlConnection con = new SqlConnection(conString))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                cmd.Connection = con;
                cmd.CommandTimeout = 200;

                sda.SelectCommand = cmd;
                using (DataSet dsCustomers = new DataSet())
                {
                    sda.Fill(dsCustomers, "Payment");
                    return dsCustomers;
                }
            }
        }
    }
}