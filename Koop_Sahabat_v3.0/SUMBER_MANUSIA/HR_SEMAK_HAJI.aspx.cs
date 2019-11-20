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
using ClosedXML.Excel;
using Microsoft.Reporting.WebForms;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Web.SessionState;
using System.Xml;

using Ionic.Zip;
using System.Text.RegularExpressions;

public partial class HR_SEMAK_HAJI : System.Web.UI.Page
{

    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    DataTable dt = new DataTable();
    StudentWebService service = new StudentWebService();
    SqlDataReader Dr;
    string Status = string.Empty, Status1 = string.Empty, Status2 = string.Empty;
    string level, userid, stscd, abc1;
    string val1 = string.Empty, val2 = string.Empty, val3 = string.Empty, upd_val2 = string.Empty;
    string phdate1 = string.Empty, phdate2 = string.Empty, chk_jurna = string.Empty, get_qry2 = string.Empty;
    string act_dt = string.Empty;
    Boolean[] notNulls;
    protected void Page_Load(object sender, EventArgs e)
    {
        //int colCount = gvCustomers.Columns.Count;
        //notNulls = new Boolean[colCount];

        //for (int i = 0; i < colCount; i++)
        //{
        //    notNulls[i] = false;
        //}

        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        app_language();
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                TextBox4.Text = DateTime.Now.ToString("yyyy");
                gd_month();
                DropDownList2.SelectedValue = DateTime.Now.ToString("MM");
                act_dt = TextBox4.Text + "" + DropDownList2.SelectedValue;

                OrgBind();
                get_jurnal();
                userid = Session["New"].ToString();


            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    void gd_month()
    {
        //DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);

        //for (int i = 1; i < 13; i++)
        //{
        //    DropDownList1.Items.Add(new ListItem(info.GetMonthName(i), i.ToString("00")));
        //}

        DateTimeFormatInfo info1 = DateTimeFormatInfo.GetInstance(null);
        int Month = DateTime.Now.Month - 4;
        for (int X = Month; X <= DateTime.Now.Month; X++)
        {
            DropDownList2.Items.Add(new ListItem(X.ToString("#0"), X.ToString("#0")));
        }
        string abc = DateTime.Now.Month.ToString("#0");
        //string abc = DD_bulancaruman.SelectedValue;

        DataSet Ds = new DataSet();
        try
        {

            string com = "select hr_month_Code,hr_month_desc from Ref_hr_month ORDER BY hr_month_Code";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList2.DataSource = dt;
            DropDownList2.DataBind();
            DropDownList2.DataTextField = "hr_month_desc";
            DropDownList2.DataValueField = "hr_month_Code";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
        DropDownList2.SelectedValue = abc.PadLeft(2, '0');
    }

    void app_language()
    {

        if (Session["New"] != null)
        {
            DataTable ste_set = new DataTable();
            ste_set = DBCon.Ora_Execute_table("select * from site_settings where ID IN ('1')");

            DataTable gt_lng = new DataTable();
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('494','448','495','496','497','1288','64','65','498','1376','15','36')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentUICulture;
            TextInfo txtinfo = culinfo.TextInfo;


            h1_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            bb1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            bb2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            h3_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            //lbl1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            lbl2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            lbl3_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            //lbl4_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            //lbl5_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            lbl6_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            //Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower()); 

        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    void OrgBind()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select org_gen_id, UPPER(org_name) as org_name from hr_organization  order by org_name";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_org.DataSource = dt;
            dd_org.DataTextField = "org_name";
            dd_org.DataValueField = "org_gen_id";
            dd_org.DataBind();
            dd_org.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void get_jurnal()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select inc_jurnal_no from hr_income where inc_sts='1' group by inc_jurnal_no";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_jurnal.DataSource = dt;
            dd_jurnal.DataTextField = "inc_jurnal_no";
            dd_jurnal.DataValueField = "inc_jurnal_no";
            dd_jurnal.DataBind();
            dd_jurnal.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
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

            string com = "select * from hr_organization_pern where op_org_id='" + dd_org.SelectedValue + "' and Status = 'A'";
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
            string com = "select * from hr_organization_jaba where oj_org_id='" + dd_org.SelectedValue + "' and oj_perg_code='" + dd_org_pen.SelectedValue + "' and Status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_jabatan.DataSource = dt;
            dd_jabatan.DataTextField = "oj_jaba_desc";
            dd_jabatan.DataValueField = "oj_jaba_cd";
            dd_jabatan.DataBind();
            dd_jabatan.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //void JabaBind()
    //{
    //    DataSet Ds = new DataSet();
    //    try
    //    {
    //        string com = "select hr_jaba_Code,UPPER(hr_jaba_desc) as hr_jaba_desc from Ref_hr_jabatan WHERE Status = 'A' ORDER BY hr_jaba_desc";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
    //        DataTable dt = new DataTable();
    //        adpt.Fill(dt);
    //        dd_jabatan.DataSource = dt;
    //        dd_jabatan.DataTextField = "hr_jaba_desc";
    //        dd_jabatan.DataValueField = "hr_jaba_Code";
    //        dd_jabatan.DataBind();
    //        dd_jabatan.Items.Insert(0, new ListItem("--- PILIH ---", ""));


    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}




    void grid()
    {
        gvCustomers.Visible = false;
        GridView1.Visible = false;
        Button5.Visible = false;
        Rptviwer_lt.Visible = true;

        act_dt = TextBox4.Text + "" + DropDownList2.SelectedValue;
        if (chk_assign_rkd.Checked == true)
        {
            chk_jurna = "inc_sts='1'";
        }
        else
        {
            chk_jurna = "inc_sts='0'";
        }
        if (dd_jurnal.SelectedValue == "")
        {
            if (dd_org.SelectedValue.Trim() != "" && dd_org_pen.SelectedValue == "" && dd_jabatan.SelectedValue.Trim() == "" && act_dt != "" )
            {
                val1 = "where " + chk_jurna + " and inc_gen_id='" + dd_org.SelectedValue.Trim() + "' and CONCAT(inc_year,inc_month) = '" + act_dt + "'";
            }
            else if (dd_org.SelectedValue.Trim() != "" && dd_org_pen.SelectedValue != "" && dd_jabatan.SelectedValue.Trim() == "" && act_dt != "")
            {
                val1 = "where " + chk_jurna + " and inc_gen_id='" + dd_org.SelectedValue.Trim() + "' and inc_sub_org_id='" + dd_org_pen.SelectedValue + "' and CONCAT(inc_year,inc_month) = '" + act_dt + "'";
            }
            else if (dd_org.SelectedValue.Trim() != "" && dd_org_pen.SelectedValue != "" && dd_jabatan.SelectedValue.Trim() != "" && act_dt != "")
            {
                val1 = "where " + chk_jurna + " and inc_gen_id='" + dd_org.SelectedValue.Trim() + "' and inc_sub_org_id='" + dd_org_pen.SelectedValue + "' and inc_dept_cd='" + dd_jabatan.SelectedValue.Trim() + "' and CONCAT(inc_year,inc_month) = '" + act_dt + "'";
            }
            else if (dd_org.SelectedValue.Trim() == "" && dd_org_pen.SelectedValue == "" && dd_jabatan.SelectedValue.Trim() == "" && act_dt != "")
            {
                val1 = "where " + chk_jurna + " and CONCAT(inc_year,inc_month) = '" + act_dt + "' ";
            }

            else
            {
                val1 = " where inc_org_id='00'";
            }
        }
        else
        {
            val1 = " where inc_jurnal_no='"+ dd_jurnal.SelectedValue +"'";
        }



        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        if (DropDownList1.SelectedItem.Text == "Laporan Ringkasan Butiran")
        {
            dt = DBCon.Ora_Execute_table("select a.inc_staff_no,hsp.stf_name,a.inc_grade_cd,a.inc_salary_amt,a.inc_cumm_fix_allwnce_amt,a.inc_cumm_xtra_allwnce_amt,a.inc_bonus_amt,a.inc_kpi_bonus_amt,a.inc_ot_amt,a.inc_kwsp_amt,a.inc_emp_kwsp_amt,a.inc_perkeso_amt,a.inc_SIP_amt,a.inc_emp_perkeso_amt,a.inc_emp_SIP_amt,a.inc_pcb_amt,a.inc_cp38_amt,a.inc_gross_amt,a.inc_nett_amt,a.inc_total_deduct_amt,a.inc_ded_amt,ISNULL(a.inc_tunggakan_amt,'0.00') as tung_amt from (select * from hr_income " + val1 + ") as a left join hr_staff_profile as hsp on hsp.stf_staff_no=a.inc_staff_no");
        }
        else if (DropDownList1.SelectedItem.Text == "Tetap Maklumat Elaun Laporan")
        {
            dt = DBCon.Ora_Execute_table("select a.inc_staff_no,hsp.stf_name,a.inc_grade_cd,a.inc_salary_amt,a.inc_cumm_fix_allwnce_amt,a.inc_cumm_xtra_allwnce_amt,a.inc_bonus_amt,a.inc_kpi_bonus_amt,a.inc_ot_amt,a.inc_kwsp_amt,a.inc_emp_kwsp_amt,a.inc_perkeso_amt,a.inc_SIP_amt,a.inc_emp_perkeso_amt,a.inc_emp_SIP_amt,a.inc_pcb_amt,a.inc_cp38_amt,a.inc_gross_amt,a.inc_nett_amt,a.inc_total_deduct_amt,a.inc_ded_amt,ISNULL(a.inc_tunggakan_amt,'0.00') as tung_amt from (select * from hr_income " + val1 + ") as a left join hr_staff_profile as hsp on hsp.stf_staff_no=a.inc_staff_no left join hr_fixed_allowance fa on fa.fxa_staff_no=a.inc_staff_no left join Ref_hr_jenis_elaun je on je.hr_elau_Code=fa.fxa_allowance_type_cd    ");
        }
        else if (DropDownList1.SelectedItem.Text == "Tambahan Elaun Laporan Butiran")
        {
            dt = DBCon.Ora_Execute_table("select a.inc_staff_no,hsp.stf_name,a.inc_grade_cd,a.inc_salary_amt,a.inc_cumm_fix_allwnce_amt,a.inc_cumm_xtra_allwnce_amt,a.inc_bonus_amt,a.inc_kpi_bonus_amt,a.inc_ot_amt,a.inc_kwsp_amt,a.inc_emp_kwsp_amt,a.inc_perkeso_amt,a.inc_SIP_amt,a.inc_emp_perkeso_amt,a.inc_emp_SIP_amt,a.inc_pcb_amt,a.inc_cp38_amt,a.inc_gross_amt,a.inc_nett_amt,a.inc_total_deduct_amt,a.inc_ded_amt,ISNULL(a.inc_tunggakan_amt,'0.00') as tung_amt from (select * from hr_income " + val1 + ") as a left join hr_staff_profile as hsp on hsp.stf_staff_no=a.inc_staff_no left join hr_extra_allowance fa on fa.xta_staff_no=a.inc_staff_no left join Ref_hr_jenis_elaun je on je.hr_elau_Code=fa.xta_allowance_type_cd   ");
        }
        else if (DropDownList1.SelectedItem.Text == "SENARAI PINDAHAN BANK")
        {
            dt = DBCon.Ora_Execute_table("select inc_staff_no,a1.stf_name,a1.stf_icno,a2.Bank_Name,a1.stf_bank_acc_no,sum(inc_nett_amt) as namt from hr_income left join hr_staff_profile as a1 on a1.stf_staff_no=inc_staff_no left join Ref_Nama_Bank a2 on a2.Bank_Code=a1.stf_bank_cd " + val1 + " group by inc_staff_no,a1.stf_name,a1.stf_icno,a2.Bank_Name,a1.stf_bank_acc_no");
        }

        ds.Tables.Add(dt);

        Rptviwer_lt.Reset();
        Rptviwer_lt.LocalReport.Refresh();
        List<DataRow> listResult = dt.AsEnumerable().ToList();
        listResult.Count();
        int countRow = 0;
        countRow = listResult.Count();

        if (countRow != 0)
        {
            //Button5.Visible = true;
            Rptviwer_lt.LocalReport.DataSources.Clear();

            string ss1 = string.Empty;
            DataTable tt_sum = new DataTable();
            tt_sum = DBCon.Ora_Execute_table("select sum(inc_nett_amt) jamt from hr_income " + val1 + "");
            if (tt_sum.Rows.Count != 0)
            {
                ss1 = tt_sum.Rows[0]["jamt"].ToString();
            }
            else
            {
                ss1 = "";
            }

            if (DropDownList1.SelectedItem.Text == "Laporan Ringkasan Butiran")
            {
                Rptviwer_lt.LocalReport.ReportPath = "SUMBER_MANUSIA/hrsha.rdlc";
                ReportDataSource rds = new ReportDataSource("hrsha", dt);
                Rptviwer_lt.LocalReport.DataSources.Add(rds);
            }
            else if (DropDownList1.SelectedItem.Text == "Tetap Maklumat Elaun Laporan")
            {
                Rptviwer_lt.LocalReport.ReportPath = "SUMBER_MANUSIA/hrfix.rdlc";
                ReportDataSource rds = new ReportDataSource("fixhr", dt);
                Rptviwer_lt.LocalReport.DataSources.Add(rds);
            }
            else if (DropDownList1.SelectedItem.Text == "Tambahan Elaun Laporan Butiran")
            {
                Rptviwer_lt.LocalReport.ReportPath = "SUMBER_MANUSIA/hrexa.rdlc";
                ReportDataSource rds = new ReportDataSource("exahr", dt);
                Rptviwer_lt.LocalReport.DataSources.Add(rds);
            }
            else if (DropDownList1.SelectedItem.Text == "SENARAI PINDAHAN BANK")
            {
                Rptviwer_lt.LocalReport.ReportPath = "SUMBER_MANUSIA/hr_trandetails.rdlc";
                ReportDataSource rds = new ReportDataSource("hrtran", dt);
                Rptviwer_lt.LocalReport.DataSources.Add(rds);
            }

            string oname = string.Empty, jabname = string.Empty;

            if (dd_org.SelectedValue != "")
            {
                oname = dd_org.SelectedItem.Text;
            }
            else
            {
                oname = "SEMUA";
            }

            if (dd_jabatan.SelectedValue != "")
            {
                jabname = dd_jabatan.SelectedItem.Text;
            }
            else
            {
                jabname = "SEMUA";
            }
            string fdt = string.Empty, tdt = string.Empty;
            if (TextBox4.Text != "")
            {
                fdt = TextBox4.Text;
            }
            else
            {
                fdt = "SEMUA";
            }

            if (DropDownList2.SelectedValue != "")
            {
                tdt = DropDownList2.SelectedItem.Text;
            }
            else
            {
                tdt = "SEMUA";
            }
            ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("oname", oname),
                     new ReportParameter("jname", jabname),
                     new ReportParameter("fdate", fdt),
                     new ReportParameter("tdate", tdt),
                      new ReportParameter("jamt", double.Parse(ss1).ToString("C").Replace("$",""))
               };

            Rptviwer_lt.LocalReport.SetParameters(rptParams);

            //Refresh
            Rptviwer_lt.LocalReport.DisplayName = "HR_" + DateTime.Now.ToString("ddMMyyyy");
            Rptviwer_lt.LocalReport.Refresh();
            Warning[] warnings;

            string[] streamids;

            string mimeType;

            string encoding;

            string extension;

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
            Response.AddHeader("content-disposition", "attachment; filename= HR_" + DateTime.Now.ToString("ddMMyyyy") + "." + extension);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Rekod tidak dijumpai. Sila Pastikan Semua Maklumat Dimasukkan Dengan Betul');", true);
        }
    }

    protected void Show_Hide_ProductsGrid(object sender, EventArgs e)
    {
        ImageButton imgShowHide = (sender as ImageButton);
        GridViewRow row = (imgShowHide.NamingContainer as GridViewRow);
        if (imgShowHide.CommandArgument == "Show")
        {
            row.FindControl("pnlProducts").Visible = true;
            imgShowHide.CommandArgument = "Hide";
            imgShowHide.ImageUrl = "~/images/minus.png";
            string orderId = Convert.ToString((row.NamingContainer as GridView).DataKeys[row.RowIndex].Value);
            GridView gvProducts = row.FindControl("gvProducts") as GridView;
            BindProducts(orderId, gvProducts);

            row.FindControl("pnlProducts1").Visible = true;
            imgShowHide.CommandArgument = "Hide";
            imgShowHide.ImageUrl = "~/images/minus.png";
            string orderId1 = Convert.ToString((row.NamingContainer as GridView).DataKeys[row.RowIndex].Value);
            GridView gvProducts1 = row.FindControl("gvProducts1") as GridView;
            BindProducts1(orderId, gvProducts1);

            row.FindControl("pnlProducts3").Visible = true;
            imgShowHide.CommandArgument = "Hide";
            imgShowHide.ImageUrl = "~/images/minus.png";
            string orderId3 = Convert.ToString((row.NamingContainer as GridView).DataKeys[row.RowIndex].Value);
            GridView gvProducts3 = row.FindControl("gvProducts3") as GridView;
            BindProducts2(orderId3, gvProducts3);
        }
        else
        {
            row.FindControl("pnlProducts").Visible = false;
            imgShowHide.CommandArgument = "Show";
            imgShowHide.ImageUrl = "~/images/plus.png";


            row.FindControl("pnlProducts1").Visible = false;
            imgShowHide.CommandArgument = "Show";
            imgShowHide.ImageUrl = "~/images/plus.png";

            row.FindControl("pnlProducts3").Visible = false;
            imgShowHide.CommandArgument = "Show";
            imgShowHide.ImageUrl = "~/images/plus.png";
        }



    }

    public void expand()
    {
        ImageButton imgShowHide = new ImageButton();
        GridViewRow row = (imgShowHide.NamingContainer as GridViewRow);
        if (imgShowHide.CommandArgument == "Show")
        {
            row.FindControl("pnlProducts").Visible = true;
            imgShowHide.CommandArgument = "Hide";
            imgShowHide.ImageUrl = "~/images/minus.png";
            string orderId = Convert.ToString((row.NamingContainer as GridView).DataKeys[row.RowIndex].Value);
            GridView gvProducts = row.FindControl("gvProducts") as GridView;
            BindProducts(orderId, gvProducts);

            row.FindControl("pnlProducts1").Visible = true;
            imgShowHide.CommandArgument = "Hide";
            imgShowHide.ImageUrl = "~/images/minus.png";
            string orderId1 = Convert.ToString((row.NamingContainer as GridView).DataKeys[row.RowIndex].Value);
            GridView gvProducts1 = row.FindControl("gvProducts1") as GridView;
            BindProducts1(orderId, gvProducts1);

            row.FindControl("pnlProducts3").Visible = true;
            imgShowHide.CommandArgument = "Hide";
            imgShowHide.ImageUrl = "~/images/minus.png";
            string orderId3 = Convert.ToString((row.NamingContainer as GridView).DataKeys[row.RowIndex].Value);
            GridView gvProducts3 = row.FindControl("gvProducts3") as GridView;
            BindProducts2(orderId3, gvProducts3);
        }
        else
        {
            row.FindControl("pnlProducts").Visible = false;
            imgShowHide.CommandArgument = "Show";
            imgShowHide.ImageUrl = "~/images/plus.png";


            row.FindControl("pnlProducts1").Visible = false;
            imgShowHide.CommandArgument = "Show";
            imgShowHide.ImageUrl = "~/images/plus.png";

            row.FindControl("pnlProducts3").Visible = false;
            imgShowHide.CommandArgument = "Show";
            imgShowHide.ImageUrl = "~/images/plus.png";
        }
    }

    private static DataTable GetData(string query)
    {
        string constr = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = query;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }


    public void BindProducts1(string orderId, GridView gvProducts1)
    {
        act_dt = TextBox4.Text + "-" + DropDownList2.SelectedValue;
        gvProducts1.ToolTip = orderId.ToString();
        gvProducts1.DataSource = GetData(string.Format("select je.hr_elau_desc,format(fa.xta_allowance_amt,'##,###.00') xta_allowance_amt from hr_extra_allowance fa inner join Ref_hr_jenis_elaun  je on je.hr_elau_Code=fa.xta_allowance_type_cd inner join hr_staff_profile hs on hs.stf_staff_no=fa.xta_staff_no where ('" + act_dt + "' between FORMAT(xta_eff_dt,'yyyy-MM') And FORMAT(xta_end_dt,'yyyy-MM')) and  hs.stf_name  = '{0}'", orderId));
        gvProducts1.DataBind();
    }

    public void BindProducts2(string orderId3, GridView gvProducts3)
    {
        act_dt = TextBox4.Text + "-" + DropDownList2.SelectedValue;
        gvProducts3.ToolTip = orderId3.ToString();
        gvProducts3.DataSource = GetData(string.Format("select je.hr_poto_desc,fa.ded_deduct_amt as xta_allowance_amt from hr_deduction fa inner join ref_hr_potongan  je on je.hr_poto_Code=fa.ded_deduct_type_cd inner join hr_staff_profile hs on hs.stf_staff_no=fa.ded_staff_no where ('" + act_dt + "' between FORMAT(ded_start_dt,'yyyy-MM') And FORMAT(ded_end_dt,'yyyy-MM')) and hs.stf_name  = '{0}'", orderId3));
        gvProducts3.DataBind();
    }
    public void BindProducts(string orderId, GridView gvProducts)
    {
        act_dt = TextBox4.Text + "-" + DropDownList2.SelectedValue;

        gvProducts.ToolTip = orderId.ToString();
        gvProducts.DataSource = GetData(string.Format("select je.hr_elau_desc,format(fa.fxa_allowance_amt,'##,###.00') fxa_allowance_amt from hr_fixed_allowance fa inner join Ref_hr_jenis_elaun  je on je.hr_elau_Code=fa.fxa_allowance_type_cd inner join hr_staff_profile hs on hs.stf_staff_no=fa.fxa_staff_no where ('" + act_dt + "' between FORMAT(fxa_eff_dt,'yyyy-MM') And FORMAT(fxa_end_dt,'yyyy-MM')) and hs.stf_name  = '{0}'", orderId));
        gvProducts.DataBind();
    }

    protected void gvProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void rset_click(object sender, EventArgs e)
    {
        Response.Redirect("HR_CUTI.aspx");
    }

    //protected void click_print(object sender, EventArgs e)
    //{
    //    {
    //        try
    //        {
    //            if (dd_org.SelectedValue.Trim() != "" && tm_date.Text != "" && ta_date.Text != "" || dd_jabatan.SelectedValue.Trim() != "")
    //            {
    //                if (tm_date.Text != "" && ta_date.Text != "")
    //                {
    //                    string d1 = tm_date.Text;
    //                    DateTime today1 = DateTime.ParseExact(d1, "dd/mm/yyyy", CultureInfo.InvariantCulture);
    //                    phdate1 = today1.ToString("yyyymm");

    //                    string d2 = ta_date.Text;
    //                    DateTime today2 = DateTime.ParseExact(d2, "dd/mm/yyyy", CultureInfo.InvariantCulture);
    //                    phdate2 = today2.ToString("yyyymm");
    //                }
    //                else
    //                {
    //                    phdate1 = "";
    //                    phdate2 = "";
    //                }
    //                if (dd_org.SelectedValue.Trim() != "" && dd_jabatan.SelectedValue.Trim() == "" && tm_date.Text == "" && ta_date.Text == "")
    //                {
    //                    val1 = " where inc_org_id='" + dd_org.SelectedValue.Trim() + "'";
    //                    val2 = "";
    //                    val3 = "";
    //                }
    //                else if (dd_org.SelectedValue.Trim() == "" && dd_jabatan.SelectedValue.Trim() != "" && tm_date.Text == "" && ta_date.Text == "")
    //                {
    //                    val2 = " where inc_dept_cd='" + dd_jabatan.SelectedValue.Trim() + "'";
    //                    val1 = "";
    //                    val3 = "";

    //                }
    //                else if (dd_org.SelectedValue.Trim() == "" && dd_jabatan.SelectedValue.Trim() == "" && tm_date.Text != "" && ta_date.Text != "")
    //                {
    //                    val3 = " where CONCAT(inc_year,inc_month) between '" + phdate1 + "' AND '" + phdate2 + "'";
    //                    val2 = "";
    //                    val1 = "";

    //                }
    //                else if (dd_org.SelectedValue.Trim() != "" && dd_jabatan.SelectedValue.Trim() != "" && tm_date.Text == "" && ta_date.Text == "")
    //                {
    //                    val1 = " where inc_org_id='" + dd_org.SelectedValue.Trim() + "'";
    //                    val2 = " and inc_dept_cd='" + dd_jabatan.SelectedValue.Trim() + "'";
    //                    val3 = "";

    //                }
    //                else if (dd_org.SelectedValue.Trim() == "" && dd_jabatan.SelectedValue.Trim() != "" && tm_date.Text != "" && ta_date.Text != "")
    //                {
    //                    val2 = " where inc_dept_cd='" + dd_jabatan.SelectedValue.Trim() + "'";
    //                    val3 = "and CONCAT(inc_year,inc_month) between '" + phdate1 + "' AND '" + phdate2 + "'";
    //                    val1 = "";

    //                }
    //                else if (dd_org.SelectedValue.Trim() != "" && dd_jabatan.SelectedValue.Trim() == "" && tm_date.Text != "" && ta_date.Text != "")
    //                {
    //                    val1 = " where inc_org_id='" + dd_org.SelectedValue.Trim() + "'";
    //                    val3 = "and CONCAT(inc_year,inc_month) between '" + phdate1 + "' AND '" + phdate2 + "'";
    //                    val2 = "";

    //                }
    //                else if (dd_org.SelectedValue.Trim() != "" && dd_jabatan.SelectedValue.Trim() != "" && tm_date.Text != "" && ta_date.Text != "")
    //                {
    //                    val1 = " where inc_org_id='" + dd_org.SelectedValue.Trim() + "'";
    //                    val2 = "and inc_dept_cd='" + dd_jabatan.SelectedValue.Trim() + "'";
    //                    val3 = "and CONCAT(inc_year,inc_month) between '" + phdate1 + "' AND '" + phdate2 + "'";

    //                }
    //                else
    //                {
    //                    val1 = " where inc_org_id='00'";
    //                }

    //                DataSet ds = new DataSet();
    //                DataTable dt = new DataTable();
    //                dt = DBCon.Ora_Execute_table("select a.inc_staff_no,hsp.stf_name,a.inc_grade_cd,a.inc_salary_amt,a.inc_cumm_fix_allwnce_amt,a.inc_cumm_xtra_allwnce_amt,a.inc_ot_amt,a.inc_kwsp_amt,a.inc_perkeso_amt,a.inc_total_deduct_amt from (select * from hr_income " + val1 + " " + val2 + " " + val3 + ") as a left join  hr_staff_profile as hsp on hsp.stf_staff_no=a.inc_staff_no");
    //                Rptviwer_lt.Reset();
    //                ds.Tables.Add(dt);

    //                Rptviwer_lt.LocalReport.DataSources.Clear();

    //                Rptviwer_lt.LocalReport.ReportPath = "hr_shaji.rdlc";
    //                ReportDataSource rds = new ReportDataSource("hr_shaji", dt);

    //                Rptviwer_lt.LocalReport.DataSources.Add(rds);

    //                //Refresh
    //                Rptviwer_lt.LocalReport.Refresh();
    //                Warning[] warnings;

    //                string[] streamids;

    //                string mimeType;

    //                string encoding;

    //                string extension;

    //                string filename;

    //                string fname = DateTime.Now.ToString("dd_MM_yyyy");

    //                //string devinfo = "<DeviceInfo><ColorDepth>32</ColorDepth><DpiX>350</DpiX><DpiY>350</DpiY><OutputFormat>PDF</OutputFormat>" +
    //                //       "  <PageWidth>12.20in</PageWidth>" +
    //                //        "  <PageHeight>8.27in</PageHeight>" +
    //                //        "  <MarginTop>0.1in</MarginTop>" +
    //                //        "  <MarginLeft>0.5in</MarginLeft>" +
    //                //         "  <MarginRight>0in</MarginRight>" +
    //                //         "  <MarginBottom>0in</MarginBottom>" +
    //                //       "</DeviceInfo>";

    //                //byte[] bytes = Rptviwer_lt.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);


    //                //Response.Buffer = true;

    //                //Response.Clear();

    //                //Response.ClearHeaders();

    //                //Response.ClearContent();

    //                //Response.ContentType = "application/pdf";


    //                //Response.AddHeader("content-disposition", "attachment; filename=SEMAK_MAKLUMAT_PENGGAJIAN_." + extension);

    //                //Response.BinaryWrite(bytes);

    //                ////Response.Write("<script>");
    //                ////Response.Write("window.open('', '_newtab');");
    //                ////Response.Write("</script>");
    //                //Response.Flush();

    //                //Response.End();


    //                if (sel_frmt.SelectedValue == "01")
    //                {
    //                    filename = string.Format("{0}.{1}", "SEMAK_MAKLUMAT_PENGGAJIAN_" + DateTime.Now.ToString("ddMMyyyy") + "", "pdf");
    //                    byte[] bytes = Rptviwer_lt.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
    //                    Response.Buffer = true;
    //                    Response.Clear();
    //                    Response.ContentType = mimeType;
    //                    Response.AddHeader("content-disposition", "attachment; filename=" + filename);
    //                    Response.BinaryWrite(bytes);
    //                    Response.Flush();
    //                    Response.End();
    //                }
    //                else if (sel_frmt.SelectedValue == "02")
    //                {
    //                    StringBuilder builder = new StringBuilder();
    //                    string strFileName = string.Format("{0}.{1}", "SEMAK_MAKLUMAT_PENGGAJIAN_" + DateTime.Now.ToString("ddMMyyyy") + "", "csv");
    //                    builder.Append("NO KAKITANGAN ,NAMA,GRED, Gaji Pokok (RM), Elaun Tetap (RM),Lain-lain Elaun (RM),OT (RM),Caruman KWSP (RM),Potongan SOCSO (RM)" + Environment.NewLine);
    //                    foreach (GridViewRow row in GridView1.Rows)
    //                    {
    //                        string sno = ((System.Web.UI.WebControls.Label)row.FindControl("s_no")).Text.ToString();
    //                        string stname = ((System.Web.UI.WebControls.Label)row.FindControl("s_name")).Text.ToString();
    //                        string sgred = ((System.Web.UI.WebControls.Label)row.FindControl("s_gred")).Text.ToString();
    //                        string skatjawa = ((System.Web.UI.WebControls.Label)row.FindControl("s_kat_jawa")).Text.ToString().Replace(",", "");
    //                        string s_stsjaw = ((System.Web.UI.WebControls.Label)row.FindControl("s_sts_jaw")).Text.ToString().Replace(",", "");
    //                        string ll = ((System.Web.UI.WebControls.Label)row.FindControl("Label51")).Text.ToString().Replace(",", "");
    //                        string ot = ((System.Web.UI.WebControls.Label)row.FindControl("Label512")).Text.ToString().Replace(",", "");
    //                        string ckwsp = ((System.Web.UI.WebControls.Label)row.FindControl("Label513")).Text.ToString().Replace(",", "");
    //                        string pso = ((System.Web.UI.WebControls.Label)row.FindControl("Label514")).Text.ToString().Replace(",", "");

    //                        builder.Append(sno + "," + stname + "," + sgred + "," + skatjawa + "," + s_stsjaw + "," + ll + "," + ot + "," + ckwsp + "," + pso + Environment.NewLine);
    //                    }
    //                    Response.Clear();
    //                    Response.ContentType = "text/csv";
    //                    Response.AddHeader("Content-Disposition", "attachment;filename=" + strFileName);
    //                    Response.Write(builder.ToString());
    //                    Response.End();
    //                }
    //                else if (sel_frmt.SelectedValue == "03")
    //                {
    //                    byte[] bytes = Rptviwer_lt.LocalReport.Render("Word", null, out mimeType, out encoding, out extension, out streamids, out warnings);
    //                    filename = string.Format("{0}.{1}", "SEMAK_MAKLUMAT_PENGGAJIAN_" + DateTime.Now.ToString("ddMMyyyy") + "", "doc");
    //                    Response.Buffer = true;
    //                    Response.Clear();
    //                    Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
    //                    Response.ContentType = mimeType;
    //                    Response.BinaryWrite(bytes);
    //                    Response.Flush();
    //                    Response.End();
    //                }
    //            }
    //            else
    //            {
    //                grid();
    //                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Sila Masukkan Input Carian');", true);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }
    //}

    void instaudt()
    {
        string audcd = "040101";
        string auddec = "SELENGGARA MAKLUMAT CUTI";
        string usrid = Session["New"].ToString();
        string curdt = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
        string Inssql = "insert into cmn_audit_trail(aud_crt_id,aud_crt_dt,aud_txn_cd,aud   _txn_desc) values ('" + usrid + "','" + curdt + "','" + audcd + "','" + auddec + "')";
        Status = DBCon.Ora_Execute_CommamdText(Inssql);
    }
    protected void srch_click(object sender, EventArgs e)
    {
        try
        {
            if (dd_org.SelectedValue.Trim() != "" && TextBox4.Text != "" && DropDownList2.SelectedValue != "" || dd_jurnal.SelectedValue != "")
            {
                
                grid_view();

            }
            else
            {                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Maklumat Penggajian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void gen_eft_file(object sender, EventArgs e)
    {
        int cut1 = 0;
        int cnt1 = 10;
        int counter = 0;
        string Sql = string.Empty;
        try
        {

            if (DropDownList1.SelectedItem.Text.ToUpper() == "SENARAI PINDAHAN BANK")
            {

                string LogFilePath = System.Configuration.ConfigurationManager.AppSettings["fpath_succcess"];
                string logFileName = "", gstrLogFile = "";
                if (!System.IO.Directory.Exists(LogFilePath))
                {
                    System.IO.Directory.CreateDirectory(LogFilePath);
                }
                logFileName = System.DateTime.Today.ToString("dd-MMM-yyyy") + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + ".txt";
                gstrLogFile = logFileName;
                System.IO.File.Delete(logFileName);
                StringBuilder newText = new StringBuilder();
                StringBuilder newText1 = new StringBuilder();

                StreamWriter LogFile = null;
                //HEAD
                string Batch_name = "";
                string stf_no = "";
                string stf_bank_cd = "";
                string stf_name = "";
                string stf_bank_Acc = "";
                string stf_newicno = "";
                string maddress = "";
                string poldicno = "";
                string mno = "";
                string sno = "";
                string remark = "";
                string bop = "";
                string org_name = "";
                double stf_sal = 0;


                //todate = DateTime.ParseExact(Txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //fromdate = DateTime.ParseExact(Txtfromdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                string cdate = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
                userid = Session["New"].ToString();
                grid_view();
                DataTable dt = DBCon.Ora_Execute_table(get_qry2);


                Sql = get_qry2;

                Dr = DBCon.Execute_Reader(Sql);


                if (Dr.HasRows)
                {

                    while (Dr.Read())
                    {
                        counter++;

                        if (newText.Length == 0 || cut1 == 1000)
                        {
                            cut1 = 0;
                            ++cnt1;
                        }

                        //ddbat.SelectedItem.Text = "S" + DateTime.Now.ToString("yyMMdd") + cnt1;
                        Batch_name = Dr["inc_batch_name"].ToString();
                        stf_no = Dr["inc_staff_no"].ToString();
                        stf_name = Dr["stf_name"].ToString();
                        stf_bank_Acc = Dr["stf_bank_acc_no"].ToString();
                        stf_newicno = Dr["stf_icno"].ToString();
                        stf_bank_cd = Dr["stf_bank_cd"].ToString();
                        remark = DropDownList1.SelectedItem.Text + "_" + DropDownList2.SelectedItem.Text + "_" + TextBox4.Text;
                        stf_sal = double.Parse(Dr["namt"].ToString());
                        org_name = dd_org.SelectedItem.Text;
                        bop = "1";
                        double tot_transaction_amt = 0;
                        tot_transaction_amt = tot_transaction_amt + Convert.ToDouble(stf_sal.ToString());
                        string[] tarytran = tot_transaction_amt.ToString("#.00").Split('.');

                        newText.AppendLine(Batch_name.Trim() + Space(10 - Batch_name.Trim().ToString().Length) + stf_newicno.Trim() + Space(14 - stf_newicno.Trim().ToString().Length) + Space(8) + Space(8) + Space(8) + Space(8) + Space(3) + RightJustified(13 - tarytran[0].Length) + tarytran[0] + tarytran[1] + stf_bank_Acc.Trim() + Space(20 - stf_bank_Acc.Trim().Length) + Space(14) + Space(40) + stf_bank_cd.Trim() + Space(11 - stf_bank_cd.Trim().Length) + bop + Space(1 - bop.Length) + Space(20) + stf_name.Trim() + Space(120 - stf_name.Trim().ToString().Length) + stf_bank_Acc.Trim() + Space(20 - stf_bank_Acc.Trim().Length) + stf_newicno.Trim() + Space(15 - stf_newicno.Trim().Length) + org_name + Space(40 - org_name.ToString().Length) + remark + Space(140 - remark.Length) + Space(14) + Space(5) + Space(5) + Space(1) + Space(40) + Space(12) + maddress + Space(105 - maddress.Length) + Space(12) + Space(40) + Space(40) + Space(2) + Space(40) + Space(20) + Space(10) + poldicno + Space(15 - poldicno.Length) + mno + Space(15 - mno.Length) + sno + Space(15 - sno.Length));
                        //newText.AppendLine(Batch_name + Space(10 - Batch_name.ToString().Length) + stf_newicno + Space(14 - stf_newicno.ToString().Length) + stf_bank_Acc + Space(20 - stf_bank_Acc.ToString().Length) + stf_bank_cd + Space(50 - stf_bank_cd.ToString().Length) + stf_name + Space(50 - stf_name.ToString().Length)   + stf_sal + Space(15 - stf_sal.ToString().Length));
                        ++cut1;


                        int count = Dr.VisibleFieldCount;
                        string baseName;
                        if (cut1 == 1000)
                        {


                            baseName = "SETGFILE" + "\\" + DateTime.Now.ToString("yyMMddHHmmss") + cnt1 + ".txt";

                            using (StreamWriter sw = new StreamWriter(Server.MapPath("~/FILES/" + baseName), true))
                            {

                                sw.WriteLine(newText.ToString());
                                newText.Clear();
                                sw.Close();
                            }
                        }

                        if (Convert.ToInt32(dt.Rows.Count) == counter)
                        {

                            baseName = "SETGFILE" + "\\" + DateTime.Now.ToString("yyMMddHHmmss") + cnt1 + ".txt";

                            using (StreamWriter sw1 = new StreamWriter(Server.MapPath("~/FILES/" + baseName), true))
                            {

                                sw1.WriteLine(newText.ToString());
                                newText.Clear();
                                sw1.Close();
                            }
                            Response.ContentType = "application/text";
                            Response.AppendHeader("Content-Disposition", "attachment; filename=" + DateTime.Now.ToString("yyMMddHHmmss") + cnt1 + ".txt");
                            Response.TransmitFile(Server.MapPath("~/FILES/" + baseName));
                            Response.End();
                        }
                    }


                    Dr.Close();

                    //Page.ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('Rekod Berjaya Dimuatnaik.'); window.location ='Jana_Data_Eft.aspx';</script>");

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Red Mark Field Harus Mandatory',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Please change Jenis Laporan',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('" + ex.Message + "');</script>");
        }
    }

    private string RemoveEmptyLines(string lines)
    {
        return Regex.Replace(lines, @"^\s+$", string.Empty, RegexOptions.Multiline).TrimEnd();
    }

    string RightJustified(int count)
    {
        if (count < 0)
            count = 0;
        return new string('0', count);
    }
    string Space(int count)
    {
        if (count < 0)
            count = 0;
        return new string(' ', count);
    }


    protected void OnCheckedChanged(object sender, EventArgs e)
    {
        bool isUpdateVisible = false;
        CheckBox chk = (sender as CheckBox);
        if (chk.ID == "chkAll")
        {
            foreach (GridViewRow row in gvCustomers.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked = chk.Checked;
                }
            }
        }
        CheckBox chkAll = (gvCustomers.HeaderRow.FindControl("chkAll") as CheckBox);
        chkAll.Checked = true;
        foreach (GridViewRow row in gvCustomers.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                bool isChecked = row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                for (int i = 1; i < row.Cells.Count; i++)
                {
                    if (isChecked && !isUpdateVisible)
                    {
                        isUpdateVisible = true;
                    }
                    if (!isChecked)
                    {
                        chkAll.Checked = false;
                    }
                }
            }
        }
        //btnUpdate.Visible = isUpdateVisible;
    }

    void grid_view()
    {
        //if (tm_date.Text != "" && ta_date.Text != "")
        //{
        //    string d1 = tm_date.Text;
        //    DateTime today1 = DateTime.ParseExact(d1, "dd/mm/yyyy", CultureInfo.InvariantCulture);
        //    phdate1 = today1.ToString("yyyymm");

        //    string d2 = ta_date.Text;
        //    DateTime today2 = DateTime.ParseExact(d2, "dd/mm/yyyy", CultureInfo.InvariantCulture);
        //    phdate2 = today2.ToString("yyyymm");
        //}
        //else
        //{
        //    phdate1 = "";
        //    phdate2 = "";
        //}
        act_dt = TextBox4.Text + "" + DropDownList2.SelectedValue;
        if (chk_kelulusan.Checked == true)
        {            
            chk_jurna = "and inc_app_sts='A'";
            Button6.Visible = false;
            Button7.Visible = true;
        }
        else
        {
            Button6.Visible = true;
            Button7.Visible = false;
            chk_jurna = "and (inc_app_sts='P' or ISNULL(inc_app_sts,'') = '')";
        }

        if (dd_jurnal.SelectedValue == "")
        {
            if (dd_org.SelectedValue.Trim() != "" && dd_org_pen.SelectedValue == "" && dd_jabatan.SelectedValue.Trim() == "" && act_dt != "")
            {
                val1 = "where inc_gen_id='" + dd_org.SelectedValue.Trim() + "' and CONCAT(inc_year,inc_month) = '" + act_dt + "' " + chk_jurna + "";
            }
            else if (dd_org.SelectedValue.Trim() != "" && dd_org_pen.SelectedValue != "" && dd_jabatan.SelectedValue.Trim() == "" && act_dt != "" )
            {
                val1 = "where inc_gen_id='" + dd_org.SelectedValue.Trim() + "' and inc_sub_org_id='" + dd_org_pen.SelectedValue + "' and CONCAT(inc_year,inc_month) = '" + act_dt + "' " + chk_jurna + "";
            }
            else if (dd_org.SelectedValue.Trim() != "" && dd_org_pen.SelectedValue != "" && dd_jabatan.SelectedValue.Trim() != "" && act_dt != "")
            {
                val1 = "where inc_gen_id='" + dd_org.SelectedValue.Trim() + "' and inc_sub_org_id='" + dd_org_pen.SelectedValue + "' and inc_dept_cd='" + dd_jabatan.SelectedValue.Trim() + "' and CONCAT(inc_year,inc_month) = '" + act_dt + "' " + chk_jurna + "";
            }
            else if (dd_org.SelectedValue.Trim() == "" && dd_org_pen.SelectedValue == "" && dd_jabatan.SelectedValue.Trim() == "" && act_dt != "")
            {
                val1 = "where CONCAT(inc_year,inc_month) = '" + act_dt + "' " + chk_jurna + "";
            }
            else
            {
                val1 = " where inc_org_id='00' " + chk_jurna + "";
            }
        }
        else
        {
            
            val1 = " where inc_jurnal_no='"+ dd_jurnal.SelectedValue +"'";
        }
     
        if (DropDownList1.SelectedItem.Text == "Laporan Ringkasan Butiran")
        {
            gvCustomers.Visible = true;
            GridView1.Visible = false;
            Rptviwer_lt.Visible = false;
            string query = "select rh.hr_month_desc,case when inc_app_sts='A' then 'LULUS' when inc_app_sts='P' then 'WAITING' else '' end as sts,a.inc_month,a.inc_year,a.inc_staff_no,hsp.stf_name,a.inc_grade_cd,a.inc_salary_amt as inc_salary_amt,a.inc_cumm_fix_allwnce_amt as inc_cumm_fix_allwnce_amt,a.inc_cumm_xtra_allwnce_amt as inc_cumm_xtra_allwnce_amt,inc_bonus_amt,inc_kpi_bonus_amt, inc_ot_amt,inc_total_deduct_amt,a.inc_kwsp_amt as inc_kwsp_amt,a.inc_emp_kwsp_amt as inc_emp_kwsp_amt,a.inc_perkeso_amt as inc_perkeso_amt,case when ISNULL(a.inc_SIP_amt,'') = '' then '0.00' else a.inc_SIP_amt end as inc_SIP_amt,a.inc_emp_perkeso_amt as inc_emp_perkeso_amt,case when ISNULL(a.inc_emp_SIP_amt,'') = '' then '0.00' else a.inc_emp_SIP_amt end as inc_emp_SIP_amt,a.inc_pcb_amt as inc_pcb_amt,inc_cp38_amt,a.inc_gross_amt as inc_gross_amt,a.inc_nett_amt as inc_nett_amt,a.inc_total_deduct_amt as inc_total_deduct_amt,ISNULL(inc_ded_amt,'0.00') as inc_ded_amt,ISNULL(inc_tunggakan_amt,'0.00') as inc_tunggakan_amt  from (select * from hr_income " + val1 + ") as a left join  hr_staff_profile as hsp on hsp.stf_staff_no=a.inc_staff_no left join Ref_hr_month rh on rh.hr_month_Code=a.inc_month order by a.inc_month";
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            DataSet ds = new DataSet();
                            sda.Fill(ds);
                            if (ds.Tables[0].Rows.Count != 0)
                            {
                                Button2.Visible = true;
                                gvCustomers.DataSource = dt;
                                gvCustomers.DataBind();
                                decimal salary = dt.AsEnumerable().Sum(row => row.Field<decimal>("inc_salary_amt"));
                                decimal ext_allow = dt.AsEnumerable().Sum(row => row.Field<decimal>("inc_cumm_fix_allwnce_amt"));
                                decimal ot_allow = dt.AsEnumerable().Sum(row => row.Field<decimal>("inc_cumm_xtra_allwnce_amt"));
                                decimal bonus_thn = (dt.AsEnumerable().Sum(row => row.Field<decimal>("inc_bonus_amt")));
                                decimal bonus_kpi = (dt.AsEnumerable().Sum(row => row.Field<decimal>("inc_kpi_bonus_amt")));
                                decimal ot_klm = dt.AsEnumerable().Sum(row => row.Field<decimal>("inc_ot_amt"));
                                decimal jp = dt.AsEnumerable().Sum(row => row.Field<decimal>("inc_total_deduct_amt"));
                                decimal car_kwsp = (dt.AsEnumerable().Sum(row => row.Field<decimal>("inc_kwsp_amt")));
                                decimal car_kwsp_emp = (dt.AsEnumerable().Sum(row => row.Field<decimal>("inc_emp_kwsp_amt")));
                                decimal car_perkeso = (dt.AsEnumerable().Sum(row => row.Field<decimal>("inc_perkeso_amt")));
                                decimal car_perkeso_emp = (dt.AsEnumerable().Sum(row => row.Field<decimal>("inc_emp_perkeso_amt")));
                                decimal sip = (dt.AsEnumerable().Sum(row => row.Field<decimal>("inc_SIP_amt")));
                                decimal sip_emp = (dt.AsEnumerable().Sum(row => row.Field<decimal>("inc_emp_SIP_amt")));
                                decimal pcb = dt.AsEnumerable().Sum(row => row.Field<decimal>("inc_pcb_amt"));
                                decimal cp_38 = dt.AsEnumerable().Sum(row => row.Field<decimal>("inc_cp38_amt"));
                                decimal pkrm = dt.AsEnumerable().Sum(row => row.Field<decimal>("inc_gross_amt"));
                                decimal pbrm = dt.AsEnumerable().Sum(row => row.Field<decimal>("inc_nett_amt"));
                                decimal ded_amt = dt.AsEnumerable().Sum(row => row.Field<decimal>("inc_ded_amt"));
                                decimal tun_amt = dt.AsEnumerable().Sum(row => row.Field<decimal>("inc_tunggakan_amt"));

                                Label ft_01 = (Label)gvCustomers.FooterRow.FindControl("ftr_001");
                                Label ft_02 = (Label)gvCustomers.FooterRow.FindControl("ftr_002");
                                Label ft_03 = (Label)gvCustomers.FooterRow.FindControl("ftr_003");
                                Label ft_04 = (Label)gvCustomers.FooterRow.FindControl("ftr_004");
                                Label ft_04_2 = (Label)gvCustomers.FooterRow.FindControl("ftr_004_2");
                                Label ft_05 = (Label)gvCustomers.FooterRow.FindControl("ftr_005");
                                Label ft_06 = (Label)gvCustomers.FooterRow.FindControl("ftr_006");
                                Label ft_06_1 = (Label)gvCustomers.FooterRow.FindControl("ftr_006_1");
                                Label ft_07 = (Label)gvCustomers.FooterRow.FindControl("ftr_007");
                                Label ft_07_2 = (Label)gvCustomers.FooterRow.FindControl("ftr_007_2");
                                Label ft_08 = (Label)gvCustomers.FooterRow.FindControl("ftr_008");
                                Label ft_08_2 = (Label)gvCustomers.FooterRow.FindControl("ftr_008_2");
                                Label ft_09 = (Label)gvCustomers.FooterRow.FindControl("ftr_009");
                                Label ft_09_2 = (Label)gvCustomers.FooterRow.FindControl("ftr_009_2");
                                Label ft_10 = (Label)gvCustomers.FooterRow.FindControl("ftr_010");
                                Label ft_11 = (Label)gvCustomers.FooterRow.FindControl("ftr_011");
                                Label ft_12 = (Label)gvCustomers.FooterRow.FindControl("ftr_012");
                                Label ft_13 = (Label)gvCustomers.FooterRow.FindControl("ftr_013");
                                Label ft_14 = (Label)gvCustomers.FooterRow.FindControl("ftr_014");

                                gvCustomers.FooterRow.Cells[2].Text = "<strong>KESELURUHAN (RM)</strong>";
                                gvCustomers.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;

                                ft_01.Text = salary.ToString("C").Replace("$", "").Replace("RM", "");
                                ft_02.Text = ext_allow.ToString("C").Replace("$", "").Replace("RM", "");
                                ft_03.Text = ot_allow.ToString("C").Replace("$", "").Replace("RM", "");
                                ft_04.Text = bonus_thn.ToString("C").Replace("$", "").Replace("RM", "");
                                ft_04_2.Text = bonus_kpi.ToString("C").Replace("$", "").Replace("RM", "");
                                ft_05.Text = ot_klm.ToString("C").Replace("$", "").Replace("RM", "");
                                ft_06.Text = jp.ToString("C").Replace("$", "").Replace("RM", "");
                                ft_06_1.Text = tun_amt.ToString("C").Replace("$", "").Replace("RM", "");
                                ft_07.Text = car_kwsp.ToString("C").Replace("$", "").Replace("RM", "");
                                ft_07_2.Text = car_kwsp_emp.ToString("C").Replace("$", "").Replace("RM", "");
                                ft_08.Text = car_perkeso.ToString("C").Replace("$", "").Replace("RM", "");
                                ft_08_2.Text = car_perkeso_emp.ToString("C").Replace("$", "").Replace("RM", "");
                                ft_09.Text = sip.ToString("C").Replace("$", "").Replace("RM", "");
                                ft_09_2.Text = sip.ToString("C").Replace("$", "").Replace("RM", "");
                                ft_10.Text = pcb.ToString("C").Replace("$", "").Replace("RM", "");
                                ft_11.Text = cp_38.ToString("C").Replace("$", "").Replace("RM", "");
                                ft_12.Text = pkrm.ToString("C").Replace("$", "").Replace("RM", "");
                                ft_13.Text = pbrm.ToString("C").Replace("$", "").Replace("RM", "");
                                ft_14.Text = ded_amt.ToString("C").Replace("$", "").Replace("RM", "");

                                DataTable chk_app_sts = new DataTable();
                                chk_app_sts = DBCon.Ora_Execute_table("select count(*) as cnt from hr_income " + val1 + " and inc_app_sts='A'");
                                if(gvCustomers.Rows.Count.ToString() == chk_app_sts.Rows[0]["cnt"].ToString())
                                {
                                    if (chk_assign_rkd.Checked == true)
                                    {
                                        Button3.Visible = false;
                                    }
                                    else
                                    {
                                        if (dd_jurnal.SelectedValue == "")
                                        {
                                            Button3.Visible = false;//modify
                                        }
                                        else
                                        {
                                            Button3.Visible = false;
                                        }
                                    }
                                }
                                else
                                {
                                    Button3.Visible = false;
                                }

                                Button5.Visible = true;
                                //if (dd_jurnal.SelectedValue == "")
                                //{
                                //    Button6.Visible = true;
                                //}
                                //else
                                //{
                                //    Button6.Visible = false;
                                //}
                            }
                            else
                            {
                                Button2.Visible = false;
                                Button5.Visible = false;
                                //Button6.Visible = false;
                                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                                gvCustomers.DataSource = ds;
                                gvCustomers.DataBind();
                                int columncount = gvCustomers.Rows[0].Cells.Count;
                                gvCustomers.Rows[0].Cells.Clear();
                                gvCustomers.Rows[0].Cells.Add(new TableCell());
                                gvCustomers.Rows[0].Cells[0].ColumnSpan = columncount;
                                gvCustomers.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
                            }

                        }
                    }
                }
            }

        }
        else
        {
            SqlCommand cmd;
            gvCustomers.Visible = false;
            GridView1.Visible = true;
            Rptviwer_lt.Visible = false;
            Button6.Visible = false;

            get_qry2 = "select inc_staff_no,inc_batch_name,a1.stf_name,a1.stf_icno,a2.Bank_Name,a1.stf_bank_acc_no,sum(inc_nett_amt) as namt,a1.stf_bank_cd from hr_income left join hr_staff_profile as a1 on a1.stf_staff_no=inc_staff_no left join Ref_Nama_Bank a2 on a2.Bank_Code=a1.stf_bank_cd " + val1 + " group by inc_staff_no,inc_batch_name,a1.stf_name,a1.stf_icno,a2.Bank_Name,a1.stf_bank_acc_no,a1.stf_bank_cd"; 
            //GridView1.DataSource = GetData(get_qry2);
            //GridView1.DataBind();

            cmd = new SqlCommand(get_qry2, conn);
            //}

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count == 0)
            {

                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                GridView1.DataSource = ds;
                GridView1.DataBind();
                int columncount = GridView1.Rows[0].Cells.Count;
                GridView1.Rows[0].Cells.Clear();
                GridView1.Rows[0].Cells.Add(new TableCell());
                GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
                GridView1.Rows[0].Cells[0].Text = "<center><strong>MAKLUMAT CARIAN TIDAK DIJUMPAI</strong></center>";

            }
            else
            {
                Button2.Visible = true;
                Button5.Visible = true;
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }

        }

    }

    void grid_view1()
    {
        act_dt = TextBox4.Text + "" + DropDownList2.SelectedValue;

        if (dd_org.SelectedValue.Trim() != "" && dd_org_pen.SelectedValue == "" && dd_jabatan.SelectedValue.Trim() == "" && act_dt != "" )
        {
            val1 = "where inc_gen_id='" + dd_org.SelectedValue.Trim() + "' and CONCAT(inc_year,inc_month) = '" + act_dt + "' ";
        }
        else if (dd_org.SelectedValue.Trim() != "" && dd_org_pen.SelectedValue != "" && dd_jabatan.SelectedValue.Trim() == "" && act_dt != "")
        {
            val1 = "where inc_gen_id='" + dd_org.SelectedValue.Trim() + "' and inc_sub_org_id='" + dd_org_pen.SelectedValue + "' and CONCAT(inc_year,inc_month) = '" + act_dt + "'";
        }
        else if (dd_org.SelectedValue.Trim() != "" && dd_org_pen.SelectedValue != "" && dd_jabatan.SelectedValue.Trim() != "" && act_dt != "")
        {
            val1 = "where inc_gen_id='" + dd_org.SelectedValue.Trim() + "' and inc_sub_org_id='" + dd_org_pen.SelectedValue + "' and inc_dept_cd='" + dd_jabatan.SelectedValue.Trim() + "' and CONCAT(inc_year,inc_month) = '" + act_dt + "'";
        }
        else if (dd_org.SelectedValue.Trim() == "" && dd_org_pen.SelectedValue == "" && dd_jabatan.SelectedValue.Trim() == "" && act_dt != "")
        {
            val1 = "where CONCAT(inc_year,inc_month) = '" + act_dt + "'";
        }
        else
        {
            val1 = " where inc_org_id='00'";
        }
        //else if (dd_org.SelectedValue.Trim() != "" && dd_jabatan.SelectedValue.Trim() == "" && tm_date.Text != "" && ta_date.Text != "")
        //{
        //    val1 = " where inc_gen_id='" + dd_org.SelectedValue.Trim() + "'";
        //    val3 = "and CONCAT(inc_year,inc_month) between '" + phdate1 + "' AND '" + phdate2 + "'";
        //    val2 = "";

        //}
        //else if (dd_org.SelectedValue.Trim() != "" && dd_jabatan.SelectedValue.Trim() != "" && tm_date.Text != "" && ta_date.Text != "")
        //{
        //    val1 = " where inc_gen_id='" + dd_org.SelectedValue.Trim() + "'";
        //    val2 = "and inc_dept_cd='" + dd_jabatan.SelectedValue.Trim() + "'";
        //    val3 = "and CONCAT(inc_year,inc_month) between '" + phdate1 + "' AND '" + phdate2 + "'";

        //}


        gvCustomers.DataSource = GetData("select a.inc_staff_no,hsp.stf_name,a.inc_grade_cd,format(a.inc_salary_amt,'##,###.00') inc_salary_amt,format(a.inc_cumm_fix_allwnce_amt,'##,###.00') inc_cumm_fix_allwnce_amt,format(a.inc_cumm_xtra_allwnce_amt,'##,###.00')inc_cumm_xtra_allwnce_amt,inc_bonus_amt,inc_kpi_bonus_amt, inc_ot_amt,inc_total_deduct_amt,format(a.inc_kwsp_amt,'##,###.00') inc_kwsp_amt,format(a.inc_emp_kwsp_amt,'##,###.00') inc_emp_kwsp_amt,format(a.inc_perkeso_amt,'##,###.00') inc_perkeso_amt,format(a.inc_emp_perkeso_amt,'##,###.00') inc_emp_perkeso_amt,format(a.inc_pcb_amt,'##,###.00') inc_pcb_amt,inc_cp38_amt,format(a.inc_gross_amt,'##,###.00') inc_gross_amt,format(a.inc_nett_amt,'##,###.00') inc_nett_amt,format(a.inc_total_deduct_amt,'##,###.00') inc_total_deduct_amt from (select * from hr_income " + val1 + ") as a left join  hr_staff_profile as hsp on hsp.stf_staff_no=a.inc_staff_no");
        gvCustomers.DataBind();
        Button5.Visible = true;

    }

   
    protected void Button2_Click(object sender, EventArgs e)
    {

        grid();
    }
    protected void gvProducts1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        //Export("", gvCustomers);
        if (DropDownList1.SelectedItem.Text == "Laporan Ringkasan Butiran")
        {
            gvCustomers.Visible = true;

            string FileName = "SENARAI_MAKLUMAT_GAJI_(" + DateTime.Now.AddHours(5).ToString("yyyy-MM-dd") + ").xls";

            Response.Clear();
            Response.Buffer = true;

            Response.AddHeader("content-disposition",
             "attachment;filename=" + FileName);
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            gvCustomers.HeaderRow.Style.Add("color", "#FFFFFF");
            gvCustomers.HeaderRow.Style.Add("background-color", "#1F437D");

            for (int i = 0; i < gvCustomers.Rows.Count; i++)
            {
                GridViewRow row = gvCustomers.Rows[i];
                row.BackColor = System.Drawing.Color.White;
                row.Attributes.Add("class", "textmode");
                if (i % 2 != 0)
                {
                    for (int j = 0; j < row.Cells.Count; j++)
                    {
                        //row.Cells[j].Style.Add("background-color", "#eff3f8");
                    }
                }
            }

            gvCustomers.RenderControl(hw);
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());

            Response.End();
            gvCustomers.Visible = false;
        }
        else
        {
            grid_view();
            dt = DBCon.Ora_Execute_table(get_qry2);
            List<DataRow> listResult = dt.AsEnumerable().ToList();
            listResult.Count();
            int countRow = 0;
            countRow = listResult.Count();

            if (countRow != 0)
            {
                StringBuilder builder = new StringBuilder();
                string strFileName = string.Format("{0}.{1}", "Senarai_Pindahan_Bank_" + DropDownList2.SelectedItem.Text +"_"+ TextBox4.Text + "", "csv");
                builder.Append("NO KAKITANGAN,NAMA,NO. KAD PENGANALAN,NAMA BANK, NO. AKAUN, JUMLAH (RM)" + Environment.NewLine);
                for (int k = 0; k <= (dt.Rows.Count - 1); k++)
                {

                    builder.Append(dt.Rows[k]["inc_staff_no"].ToString() + " , " + dt.Rows[k]["stf_name"].ToString() + ", " + dt.Rows[k]["stf_icno"].ToString() + "," + dt.Rows[k]["Bank_Name"].ToString() + "," + dt.Rows[k]["stf_bank_acc_no"].ToString() + "," + dt.Rows[k]["namt"].ToString().Replace(",", "") + Environment.NewLine);

                }
                Response.Clear();
                Response.ContentType = "text/csv";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + strFileName);
                Response.Write(builder.ToString());
                Response.End();

            }
        }
    }

    public static void Export(string fileName, GridView gv)
    {
        //HttpContext.Current.Response.Clear();
        //HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Unicode;
        //HttpContext.Current.Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
        //HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Customers.xls"));



        //using (StringWriter sw = new StringWriter())
        //{
        //    using (HtmlTextWriter htw = new HtmlTextWriter(sw))
        //    {
        //        //  Create a form to contain the grid
        //        Table table = new Table();

        //        //  add the header row to the table
        //        if (gv.HeaderRow != null)
        //        {
        //            PrepareControlForExport(gv.HeaderRow);

        //            table.Rows.Add(gv.HeaderRow);

        //        }

        //        //  add each of the data rows to the table
        //        foreach (GridViewRow row in gv.Rows)
        //        {




        //            if (row.RowType == DataControlRowType.DataRow)
        //            {
        //                for (int columnIndex = 0; columnIndex < row.Cells.Count; columnIndex++)
        //                {
        //                    PrepareControlForExport(row);
        //                    row.Cells[columnIndex].Attributes.Add("class", "text");
        //                    table.BorderColor = Color.Black;
        //                    table.Rows.Add(row);

        //                }
        //            }

        //        }

        //        //  add the footer row to the table
        //        if (gv.FooterRow != null)
        //        {
        //            PrepareControlForExport(gv.FooterRow);
        //            table.Rows.Add(gv.FooterRow);
        //        }

        //        //  render the table into the htmlwriter
        //        table.RenderControl(htw);

        //        //  render the htmlwriter into the response
        //        string style = @"<style> .textmode {mso-number-format:} </style>";
        //       HttpContext.Current.  Response.Write(style);
        //HttpContext.Current.   Response.ContentType = "application/text";
        //HttpContext.Current.   Response.Write(sw.ToString());
        //HttpContext.Current.Response.End();

        //    }
        //}


    }


    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    /// <summary>
    /// Replace any of the contained controls with literals
    /// </summary>
    /// <param name="control"></param>
    private static void PrepareControlForExport(Control control)
    {
        for (int i = 0; i < control.Controls.Count; i++)
        {
            Control current = control.Controls[i];
            if (current is LinkButton)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
            }
            else if (current is ImageButton)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
            }
            else if (current is HyperLink)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
            }
            else if (current is DropDownList)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
            }
            else if (current is CheckBox)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
            }

            if (current.HasControls())
            {
                PrepareControlForExport(current);
            }
        }
    }


    protected void gvCustomers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string gt_dt = TextBox4.Text + "-" + DropDownList2.SelectedValue;
                
                string customerId = gvCustomers.DataKeys[e.Row.RowIndex].Value.ToString().Replace("'","''");
                GridView gvOrders = e.Row.FindControl("gvProducts") as GridView;
                //gvOrders.DataSource = GetData(string.Format("select je.hr_elau_desc,format(fa.fxa_allowance_amt,'##,###.00') fxa_allowance_amt from hr_fixed_allowance fa inner join Ref_hr_jenis_elaun  je on je.hr_elau_Code=fa.fxa_allowance_type_cd inner join hr_staff_profile hs on hs.stf_staff_no=fa.fxa_staff_no where ('" + gt_dt + "' between FORMAT(fxa_eff_dt,'yyyy-MM') And FORMAT(fxa_end_dt,'yyyy-MM')) and hs.stf_name  = '{0}'", customerId));
                gvOrders.DataSource = GetData(string.Format("select hr_elau_desc,(sum(falw_amt) - sum(falw_amt1)) fxa_allowance_amt from (select hr_elau_desc,ISNULL(sum(s1.fxa_allowance_amt),'') as falw_amt,'' falw_amt1 from hr_fixed_allowance s1 Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=s1.fxa_allowance_type_cd inner join hr_staff_profile hs on hs.stf_staff_no=fxa_staff_no where ('" + gt_dt + "' between FORMAT(fxa_eff_dt,'yyyy-MM') And FORMAT(fxa_end_dt,'yyyy-MM')) and hs.stf_name='{0}' group by hr_elau_desc union all select hr_elau_desc,ISNULL(sum(s1.fxa_promo_amt),'') as falw_amt,ISNULL(sum(s1.fxa_allowance_amt),'') as falw_amt1 from hr_fixed_allowance s1 Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=s1.fxa_allowance_type_cd inner join hr_staff_profile hs on hs.stf_staff_no=fxa_staff_no where ('" + gt_dt + "' between FORMAT(fxa_pst_dt,'yyyy-MM') And FORMAT(fxa_ped_dt,'yyyy-MM')) and hs.stf_name='{0}' group by hr_elau_desc)as a group by hr_elau_desc", customerId));
                gvOrders.DataBind();


                string customerId1 = gvCustomers.DataKeys[e.Row.RowIndex].Value.ToString().Replace("'", "''");
                GridView gvOrders1 = e.Row.FindControl("gvProducts1") as GridView;
                //gvOrders1.DataSource = GetData(string.Format("select je.hr_elau_desc,format(fa.xta_allowance_amt,'##,###.00') xta_allowance_amt from hr_extra_allowance fa inner join Ref_hr_jenis_elaun  je on je.hr_elau_Code=fa.xta_allowance_type_cd inner join hr_staff_profile hs on hs.stf_staff_no=fa.xta_staff_no where ('" + gt_dt + "' between FORMAT(xta_eff_dt,'yyyy-MM') And FORMAT(xta_end_dt,'yyyy-MM')) and hs.stf_name  = '{0}'", customerId1));
                gvOrders1.DataSource = GetData(string.Format("select xta_staff_no,hr_elau_desc as hr_elau_desc,(sum(xalw_amt) - sum(xalw_amt1)) xta_allowance_amt from(select xta_staff_no,hr_elau_desc,ISNULL(sum(s1.xta_allowance_amt),'') as xalw_amt,'0.00' xalw_amt1 from hr_extra_allowance s1 Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=s1.xta_allowance_type_cd inner join hr_staff_profile hs on hs.stf_staff_no=xta_staff_no where hs.stf_name='{0}' and ('" + gt_dt + "' between FORMAT(xta_eff_dt,'yyyy-MM') And FORMAT(xta_end_dt,'yyyy-MM') ) group by hr_elau_desc,xta_staff_no union all select xta_staff_no,hr_elau_desc,ISNULL(sum(s1.xta_promo_amt),'') as xalw_amt,ISNULL(sum(s1.xta_allowance_amt),'') as xalw_amt1 from hr_extra_allowance s1 Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=s1.xta_allowance_type_cd inner join hr_staff_profile hs on hs.stf_staff_no=xta_staff_no where hs.stf_name='{0}' and ('" + gt_dt + "' between FORMAT(xta_pst_dt,'yyyy-MM') And FORMAT(xta_ped_dt,'yyyy-MM')) group by xta_staff_no,hr_elau_desc) as a group by xta_staff_no,hr_elau_desc", customerId1));
                gvOrders1.DataBind();
                e.Row.Cells[0].Attributes.Add("class", "text"); //Change to .cell[0]

                string customerId3 = gvCustomers.DataKeys[e.Row.RowIndex].Value.ToString().Replace("'", "''");
                GridView gvOrders3 = e.Row.FindControl("gvProducts3") as GridView;
                gvOrders3.DataSource = GetData(string.Format("select je.hr_poto_desc,fa.ded_deduct_amt as xta_allowance_amt from hr_deduction fa inner join ref_hr_potongan  je on je.hr_poto_Code=fa.ded_deduct_type_cd inner join hr_staff_profile hs on hs.stf_staff_no=fa.ded_staff_no where ('" + gt_dt + "' between FORMAT(ded_start_dt,'yyyy-MM') And FORMAT(ded_end_dt,'yyyy-MM')) and hs.stf_name  = '{0}'", customerId1));
                gvOrders3.DataBind();
            }


        }
    }

    protected void gvProducts3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void gvCustomers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCustomers.PageIndex = e.NewPageIndex;
        srch_click(null, null);
    }
    protected void upd_sts_clk(object sender, EventArgs e)
    {
        foreach (GridViewRow gvrow in gvCustomers.Rows)
        {
            var nokak = gvrow.FindControl("labstf") as Label;
            var mnth = gvrow.FindControl("ss_val1") as Label;
            var year = gvrow.FindControl("ss_val2") as Label;
            DataTable chk_income = new DataTable();
            chk_income = DBCon.Ora_Execute_table("select inc_staff_no from hr_income where inc_staff_no='" + nokak.Text + "' and inc_month='" + mnth.Text + "' and inc_year='" + year.Text + "' and ISNULL(inc_app_sts,'') =''");

            if (chk_income.Rows.Count != 0)
            {
                string Inssql1_bon = "UPDATE hr_income set inc_app_sts='P',inc_upd_id='" + Session["New"].ToString() + "',inc_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where inc_staff_no='" + nokak.Text + "' and inc_month='" + mnth.Text + "' and inc_year='" + year.Text + "'";
                Status1 = DBCon.Ora_Execute_CommamdText(Inssql1_bon);
            }

        }
        if(Status1 == "SUCCESS")
        {
            service.audit_trail("P0071", "Mohon Kelulusan Gaji", dd_org.SelectedItem.Text, DropDownList2.SelectedItem.Text + "_" + TextBox4.Text);
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
        }
        grid_view();
    }

    protected void jana_clk(object sender, EventArgs e)
    {
        string date1 = string.Empty, year1 = string.Empty, ruj1_1 = string.Empty, ruj1_2 = string.Empty, ruj1_3 = string.Empty, ruj1_4 = string.Empty, ruj1_5 = string.Empty, ruj1_6 = string.Empty, ruj2_1 = string.Empty, ruj2_2 = string.Empty, ruj2_3 = string.Empty, ruj2_4 = string.Empty, ruj2_5 = string.Empty, ruj2_6 = string.Empty;
        if (gvCustomers.Rows.Count != 0)
        {
            act_dt = TextBox4.Text + "-" + DropDownList2.SelectedValue;
            date1 = DropDownList1.SelectedItem.Text;
            year1 = TextBox4.Text;
            DataTable get_jn_jno = new DataTable();
            get_jn_jno = DBCon.Ora_Execute_table("select top(1) GL_invois_no,(RIGHT(GL_invois_no,1) + 1) invno from KW_General_Ledger where GL_invois_no like '%KTH-JV-HR-" + date1 + "" + year1 + "%' group by GL_invois_no order by GL_invois_no desc");
            if (get_jn_jno.Rows.Count == 0)
            {
                ruj1_1 = "KTH-JV-HR-" + date1 + "" + year1 + "-1";
                ruj1_2 = "KTH-JV-HR-" + date1 + "" + year1 + "-2";
                ruj1_3 = "KTH-JV-HR-" + date1 + "" + year1 + "-3";
                ruj1_4 = "KTH-JV-HR-" + date1 + "" + year1 + "-4";
                ruj1_5 = "KTH-JV-HR-" + date1 + "" + year1 + "-5";
                ruj1_6 = "KTH-JV-HR-" + date1 + "" + year1 + "-6";
            }
            else
            {
                ruj1_1 = "KTH-JV-HR-" + date1 + "" + year1 + "-" + get_jn_jno.Rows[0]["invno"].ToString();
                ruj1_2 = "KTH-JV-HR-" + date1 + "" + year1 + "-" + (Int32.Parse(get_jn_jno.Rows[0]["invno"].ToString()) + 1);
                ruj1_3 = "KTH-JV-HR-" + date1 + "" + year1 + "-" + (Int32.Parse(get_jn_jno.Rows[0]["invno"].ToString()) + 2);
                ruj1_4 = "KTH-JV-HR-" + date1 + "" + year1 + "-" + (Int32.Parse(get_jn_jno.Rows[0]["invno"].ToString()) + 3);
                ruj1_5 = "KTH-JV-HR-" + date1 + "" + year1 + "-" + (Int32.Parse(get_jn_jno.Rows[0]["invno"].ToString()) + 4);
                ruj1_6 = "KTH-JV-HR-" + date1 + "" + year1 + "-" + (Int32.Parse(get_jn_jno.Rows[0]["invno"].ToString()) + 5);
            }

            DataTable get_jn_inv = new DataTable();
            get_jn_inv = DBCon.Ora_Execute_table("select top(1) GL_invois_no,(RIGHT(GL_invois_no,1) + 1) invno from KW_General_Ledger where GL_invois_no like '%KTH-INV-HR-" + date1 + "" + year1 + "%' group by GL_invois_no order by GL_invois_no desc");
            if (get_jn_inv.Rows.Count == 0)
            {
                ruj2_1 = "KTH-INV-HR-" + date1 + "" + year1 + "-1";
                ruj2_2 = "KTH-INV-HR-" + date1 + "" + year1 + "-2";
                ruj2_3 = "KTH-INV-HR-" + date1 + "" + year1 + "-3";
                ruj2_4 = "KTH-INV-HR-" + date1 + "" + year1 + "-4";
                ruj2_5 = "KTH-INV-HR-" + date1 + "" + year1 + "-5";
                ruj2_6 = "KTH-INV-HR-" + date1 + "" + year1 + "-6";
            }
            else
            {
                ruj2_1 = "KTH-INV-HR-" + date1 + "" + year1 + "-" + get_jn_inv.Rows[0]["invno"].ToString();
                ruj2_2 = "KTH-INV-HR-" + date1 + "" + year1 + "-" + (Int32.Parse(get_jn_inv.Rows[0]["invno"].ToString()) + 1);
                ruj2_3 = "KTH-INV-HR-" + date1 + "" + year1 + "-" + (Int32.Parse(get_jn_inv.Rows[0]["invno"].ToString()) + 2);
                ruj2_4 = "KTH-INV-HR-" + date1 + "" + year1 + "-" + (Int32.Parse(get_jn_inv.Rows[0]["invno"].ToString()) + 3);
                ruj2_5 = "KTH-INV-HR-" + date1 + "" + year1 + "-" + (Int32.Parse(get_jn_inv.Rows[0]["invno"].ToString()) + 4);
                ruj2_6 = "KTH-INV-HR-" + date1 + "" + year1 + "-" + (Int32.Parse(get_jn_inv.Rows[0]["invno"].ToString()) + 5);
            }


            get_bind_details();

            DataTable get_jm_staff = new DataTable();
            get_jm_staff = DBCon.Ora_Execute_table("select sum(inc_cumm_fix_allwnce_amt) as ext_allowencw,sum(inc_kwsp_amt) as kwsp,sum(inc_emp_kwsp_amt) as emp_kwsp,sum(inc_perkeso_amt) as perk_amt,sum(inc_emp_perkeso_amt) as emp_perk_amt,sum(inc_SIP_amt) as sip_amt,sum(inc_emp_SIP_amt) as emp_sip_amt,sum(inc_pcb_amt) as pcb_amt,sum(inc_cp38_amt) as cp38_amt,sum(inc_nett_amt) as nett_amt,ISNULL(sum(inc_tunggakan_amt),'0.00') as tungg_amt from hr_income where " + upd_val2 + "  and inc_sts ='0' and inc_app_sts='A'");
            if (get_jm_staff.Rows.Count != 0)
            {
                string ext_alwnce = double.Parse(get_jm_staff.Rows[0]["ext_allowencw"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");

                if (ext_alwnce != "0.00")
                {
                    //invois bil item
                    string ea_desc = "" + date1 + " " + year1 + " - Elaun Teatap";
                    string Inssql_invois_bil_item = "INSERT INTO KW_Pembayaran_invoisBil_item (untuk_akaun,Data,no_permohonan,Kat_akaun,cre_kod_akaun,tarkih_mohon,tarkih_invois,no_invois,terma,deb_kod_akaun,item,keterangan,project_kod,unit,quantiti,discount,jumlah,tax,gst,Othgst,gsttype,othgsttype,gstjumlah,othgstjumlah,Overall,Status,crt_id,cr_dt,no_po) VALUES ('W-06030','BARU','" + ruj1_1 + "','SEMUA COA','04.26','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + ruj2_1 + "','','12.02','','" + ea_desc + "','--- PILIH ---','" + ext_alwnce + "','1','0.00','" + ext_alwnce + "','N','0','0','0','0','0.00','0.00','" + ext_alwnce + "','B','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','')";
                    Status1 = DBCon.Ora_Execute_CommamdText(Inssql_invois_bil_item);
                }


                string val5 = double.Parse(get_jm_staff.Rows[0]["kwsp"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                if (val5 != "0.00")
                {
                    string desc15 = "" + date1 + " " + year1 + " - KWSP Kakitangan Keseluruhan";
                    string Inssql_val5 = "INSERT INTO KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,kat_akaun,GL_invois_no,ref2,Tax_type,Tot_Amt,GL_process_dt,GL_desc1,GL_desc2,GL_nama_kod,crt_id,cr_dt,GL_sts) VALUES ('04.14','0.00','" + val5 + "','04','" + ruj1_2 + "','','','0.00','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + desc15 + "','','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A')";
                    Status1 = DBCon.Ora_Execute_CommamdText(Inssql_val5);
                }
                string val5_1 = double.Parse(get_jm_staff.Rows[0]["emp_kwsp"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                if (val5_1 != "0.00")
                {
                    string desc15 = "" + date1 + " " + year1 + " - KWSP Majikan Keseluruhan";
                    string Inssql_val5 = "INSERT INTO KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,kat_akaun,GL_invois_no,ref2,Tax_type,Tot_Amt,GL_process_dt,GL_desc1,GL_desc2,GL_nama_kod,crt_id,cr_dt,GL_sts) VALUES ('04.14','0.00','" + val5_1 + "','04','" + ruj1_2 + "','','','0.00','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + desc15 + "','','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A')";
                    Status1 = DBCon.Ora_Execute_CommamdText(Inssql_val5);
                }
                string val6 = double.Parse(get_jm_staff.Rows[0]["perk_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                if (val6 != "0.00")
                {
                    string desc16 = "" + date1 + " " + year1 + " - PERKESO Kakitangan Keseluruhan";
                    string Inssql_val6 = "INSERT INTO KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,kat_akaun,GL_invois_no,ref2,Tax_type,Tot_Amt,GL_process_dt,GL_desc1,GL_desc2,GL_nama_kod,crt_id,cr_dt,GL_sts) VALUES ('04.15','0.00','" + val6 + "','04','" + ruj1_3 + "','','','0.00','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + desc16 + "','','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A')";
                    Status1 = DBCon.Ora_Execute_CommamdText(Inssql_val6);
                }

                string val6_1 = double.Parse(get_jm_staff.Rows[0]["emp_perk_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                if (val6_1 != "0.00")
                {
                    string desc16 = "" + date1 + " " + year1 + " - PERKESO Majikan Keseluruhan";
                    string Inssql_val6 = "INSERT INTO KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,kat_akaun,GL_invois_no,ref2,Tax_type,Tot_Amt,GL_process_dt,GL_desc1,GL_desc2,GL_nama_kod,crt_id,cr_dt,GL_sts) VALUES ('04.15','0.00','" + val6_1 + "','04','" + ruj1_3 + "','','','0.00','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + desc16 + "','','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A')";
                    Status1 = DBCon.Ora_Execute_CommamdText(Inssql_val6);
                }

                string val7 = double.Parse(get_jm_staff.Rows[0]["sip_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                if (val7 != "0.00")
                {
                    string desc17 = "" + date1 + " " + year1 + " - PERKESO-SIP Kakitangan Keseluruhan";
                    string Inssql_val7 = "INSERT INTO KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,kat_akaun,GL_invois_no,ref2,Tax_type,Tot_Amt,GL_process_dt,GL_desc1,GL_desc2,GL_nama_kod,crt_id,cr_dt,GL_sts) VALUES ('04.16','0.00','" + val7 + "','04','" + ruj1_4 + "','','','0.00','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + desc17 + "','','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A')";
                    Status1 = DBCon.Ora_Execute_CommamdText(Inssql_val7);
                }

                string val7_2 = double.Parse(get_jm_staff.Rows[0]["emp_sip_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                if (val7_2 != "0.00")
                {
                    string desc17 = "" + date1 + " " + year1 + " - PERKESO-SIP Majikan Keseluruhan";
                    string Inssql_val7 = "INSERT INTO KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,kat_akaun,GL_invois_no,ref2,Tax_type,Tot_Amt,GL_process_dt,GL_desc1,GL_desc2,GL_nama_kod,crt_id,cr_dt,GL_sts) VALUES ('04.16','0.00','" + val7_2 + "','04','" + ruj1_4 + "','','','0.00','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + desc17 + "','','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A')";
                    Status1 = DBCon.Ora_Execute_CommamdText(Inssql_val7);
                }

                string val8 = double.Parse(get_jm_staff.Rows[0]["pcb_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                if (val8 != "0.00")
                {
                    string desc18 = "" + date1 + " " + year1 + " - PCB Keseluruhan";
                    string Inssql_val8 = "INSERT INTO KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,kat_akaun,GL_invois_no,ref2,Tax_type,Tot_Amt,GL_process_dt,GL_desc1,GL_desc2,GL_nama_kod,crt_id,cr_dt,GL_sts) VALUES ('04.24','0.00','" + val8 + "','04','" + ruj1_5 + "','','','0.00','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + desc18 + "','','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A')";
                    Status1 = DBCon.Ora_Execute_CommamdText(Inssql_val8);
                }

                string val9 = double.Parse(get_jm_staff.Rows[0]["cp38_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                if (val9 != "0.00")
                {
                    string desc19 = "" + date1 + " " + year1 + " - CP 38 Keseluruhan";
                    string Inssql_val9 = "INSERT INTO KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,kat_akaun,GL_invois_no,ref2,Tax_type,Tot_Amt,GL_process_dt,GL_desc1,GL_desc2,GL_nama_kod,crt_id,cr_dt,GL_sts) VALUES ('04.25','0.00','" + val9 + "','04','" + ruj1_6 + "','','','0.00','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + desc19 + "','','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A')";
                    Status1 = DBCon.Ora_Execute_CommamdText(Inssql_val9);
                }

                string val10 = get_jm_staff.Rows[0]["nett_amt"].ToString();
                string s = val10;



                val10 = double.Parse(s).ToString("C").Replace("$", "").Replace("RM", "");
                if (val10 != "0.00")
                {
                    string desc20 = "" + date1 + " " + year1 + " - GAJI KAKITANGAN (CLEARING)";
                    double ss1 = double.Parse(val10);
                    string Inssql_val10 = "INSERT INTO KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,kat_akaun,GL_invois_no,ref2,Tax_type,Tot_Amt,GL_process_dt,GL_desc1,GL_desc2,GL_nama_kod,crt_id,cr_dt,GL_sts) VALUES ('04.26','0.00','" + ss1.ToString() + "','04','" + ruj1_1 + "','','','0.00','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + desc20 + "','','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A')";
                    Status1 = DBCon.Ora_Execute_CommamdText(Inssql_val10);
                }


                string val11 = double.Parse(get_jm_staff.Rows[0]["tungg_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                if (val11 != "0.00")
                {
                    string desc21 = "" + date1 + " " + year1 + " - TUNGGAKAN Keseluruhan";
                    string Inssql_val11 = "INSERT INTO KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,kat_akaun,GL_invois_no,ref2,Tax_type,Tot_Amt,GL_process_dt,GL_desc1,GL_desc2,GL_nama_kod,crt_id,cr_dt,GL_sts) VALUES ('04.26','0.00','" + val11 + "','04','" + ruj1_1 + "','','','0.00','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + desc21 + "','','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A')";
                    Status1 = DBCon.Ora_Execute_CommamdText(Inssql_val11);
                }

                foreach (GridViewRow gvrow in gvCustomers.Rows)
                {

                    var nokak = gvrow.FindControl("labstf") as Label;
                    var mnth = gvrow.FindControl("ss_val1") as Label;
                    var year = gvrow.FindControl("ss_val2") as Label;
                    var gred = gvrow.FindControl("lagred") as Label;
                    var labsal = gvrow.FindControl("labsal") as Label;
                    var famt = gvrow.FindControl("txt_nama") as Label;
                    var eamt = gvrow.FindControl("txt_nama1") as Label;
                    var bonus = gvrow.FindControl("labot_bonus") as Label;
                    var bonus_kpi = gvrow.FindControl("labot_kpi") as Label;
                    var ot = gvrow.FindControl("labot") as Label;
                    var kwsp_ck = gvrow.FindControl("lapkwap") as Label;
                    var kwsp_ckm = gvrow.FindControl("lapkwap1") as Label;
                    var per_kak = gvrow.FindControl("labper") as Label;
                    var per_maj = gvrow.FindControl("labper1") as Label;
                    var pcb = gvrow.FindControl("labpcb") as Label;
                    var cp38 = gvrow.FindControl("labot_cp") as Label;
                    var gross = gvrow.FindControl("labgross") as Label;
                    var net = gvrow.FindControl("labnet") as Label;
                    var sip = gvrow.FindControl("labSIP") as Label;
                    var sip_maj = gvrow.FindControl("labSIP1") as Label;
                    var tung_amt = gvrow.FindControl("tung_amt") as Label;



                    DataTable dt_chk_sts = new DataTable();
                    dt_chk_sts = DBCon.Ora_Execute_table("select inc_staff_no from hr_income where inc_staff_no='" + nokak.Text + "' and inc_month='" + mnth.Text + "' and inc_year='" + year.Text + "' and inc_sts ='0' and inc_app_sts='A'");
                    if (dt_chk_sts.Rows.Count != 0)
                    {
                        DataTable sel_staff = new DataTable();
                        sel_staff = DBCon.Ora_Execute_table("select stf_staff_no,stf_kod_akaun,stf_name from hr_staff_profile where stf_staff_no='" + nokak.Text + "'");

                        //fixed

                        DataTable fix_all = new DataTable();
                        fix_all = DBCon.Ora_Execute_table("select * from hr_fixed_allowance where ('" + act_dt + "' between FORMAT(fxa_eff_dt,'yyyy-MM') And FORMAT(fxa_end_dt,'yyyy-MM')) and fxa_staff_no='" + nokak.Text + "'");

                        if (fix_all.Rows.Count != 0)
                        {
                            for (int k1 = 0; k1 < fix_all.Rows.Count; k1++)
                            {
                                DataTable sel_jenis = new DataTable();
                                sel_jenis = DBCon.Ora_Execute_table("select * from Ref_hr_jenis_elaun where hr_elau_Code='" + fix_all.Rows[k1]["fxa_allowance_type_cd"].ToString() + "'");

                                string Inssql_fix = "INSERT INTO KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,kat_akaun,GL_invois_no,ref2,Tax_type,Tot_Amt,GL_process_dt,GL_desc1,GL_desc2,GL_nama_kod,crt_id,cr_dt,GL_sts) VALUES ('12.02','" + fix_all.Rows[k1]["fxa_allowance_amt"].ToString() + "','0.00','12','" + ruj1_1 + "','" + sel_staff.Rows[0]["stf_name"].ToString() + "','','0.00','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + "" + date1 + " " + year1 + " - " + sel_jenis.Rows[0]["hr_elau_desc"].ToString() + "','','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A')";
                                Status1 = DBCon.Ora_Execute_CommamdText(Inssql_fix);
                            }
                        }

                        //xtra

                        DataTable ext_all = new DataTable();
                        ext_all = DBCon.Ora_Execute_table("select * from hr_extra_allowance where ('" + act_dt + "' between FORMAT(xta_eff_dt,'yyyy-MM') And FORMAT(xta_end_dt,'yyyy-MM')) and xta_staff_no='" + nokak.Text + "'");

                        if (ext_all.Rows.Count != 0)
                        {
                            for (int k2 = 0; k2 < ext_all.Rows.Count; k2++)
                            {
                                DataTable sel_jenis = new DataTable();
                                sel_jenis = DBCon.Ora_Execute_table("select * from Ref_hr_jenis_elaun where hr_elau_Code='" + ext_all.Rows[k2]["xta_allowance_type_cd"].ToString() + "'");
                                string Inssql_ext = "INSERT INTO KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,kat_akaun,GL_invois_no,ref2,Tax_type,Tot_Amt,GL_process_dt,GL_desc1,GL_desc2,GL_nama_kod,crt_id,cr_dt,GL_sts) VALUES ('12.03','" + ext_all.Rows[k2]["xta_allowance_amt"].ToString() + "','0.00','12','" + ruj1_1 + "','" + sel_staff.Rows[0]["stf_name"].ToString() + "','','0.00','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + "" + date1 + " " + year1 + " - " + sel_jenis.Rows[0]["hr_elau_desc"].ToString() + "','','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A')";
                                Status1 = DBCon.Ora_Execute_CommamdText(Inssql_ext);
                            }
                        }

                        //bonus tahunan

                        if (bonus.Text != "0.00")
                        {
                            string bon_desc = "" + date1 + " " + year1 + " - BONUS Tahunan";
                            string Inssql1_bon = "INSERT INTO KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,kat_akaun,GL_invois_no,ref2,Tax_type,Tot_Amt,GL_process_dt,GL_desc1,GL_desc2,GL_nama_kod,crt_id,cr_dt,GL_sts) VALUES ('12.04','" + bonus.Text + "','0.00','12','" + ruj1_1 + "','" + sel_staff.Rows[0]["stf_name"].ToString() + "','','0.00','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + bon_desc + "','','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A')";
                            Status1 = DBCon.Ora_Execute_CommamdText(Inssql1_bon);
                        }

                        //bonus tahunan

                        if (bonus_kpi.Text != "0.00")
                        {
                            string bon_desc1 = "" + date1 + " " + year1 + " - BONUS KPI";
                            string Inssql_bon_kpi = "INSERT INTO KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,kat_akaun,GL_invois_no,ref2,Tax_type,Tot_Amt,GL_process_dt,GL_desc1,GL_desc2,GL_nama_kod,crt_id,cr_dt,GL_sts) VALUES ('12.04','" + bonus_kpi.Text + "','0.00','12','" + ruj1_1 + "','" + sel_staff.Rows[0]["stf_name"].ToString() + "','','0.00','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + bon_desc1 + "','','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A')";
                            Status1 = DBCon.Ora_Execute_CommamdText(Inssql_bon_kpi);
                        }

                        //OT

                        if (ot.Text != "0.00")
                        {
                            string ot_desc1 = "" + date1 + " " + year1 + " - OT";
                            string Inssql_ot = "INSERT INTO KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,kat_akaun,GL_invois_no,ref2,Tax_type,Tot_Amt,GL_process_dt,GL_desc1,GL_desc2,GL_nama_kod,crt_id,cr_dt,GL_sts) VALUES ('12.05','" + ot.Text + "','0.00','12','" + ruj1_1 + "','" + sel_staff.Rows[0]["stf_name"].ToString() + "','','0.00','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + ot_desc1 + "','','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A')";
                            Status1 = DBCon.Ora_Execute_CommamdText(Inssql_ot);
                        }

                        //KWSP CK

                        if (kwsp_ck.Text != "0.00")
                        {
                            string ot_desc2 = "" + date1 + " " + year1 + " - CARUMAN KWSP";
                            string Inssql_kwsp_ck = "INSERT INTO KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,kat_akaun,GL_invois_no,ref2,Tax_type,Tot_Amt,GL_process_dt,GL_desc1,GL_desc2,GL_nama_kod,crt_id,cr_dt,GL_sts) VALUES ('12.06','" + kwsp_ck.Text + "','0.00','12','" + ruj1_2 + "','" + sel_staff.Rows[0]["stf_name"].ToString() + "','','0.00','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + ot_desc2 + "','','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A')";
                            Status1 = DBCon.Ora_Execute_CommamdText(Inssql_kwsp_ck);

                            //invois bil item
                            string Inssql_invois_bil_item_kwsp_ck = "INSERT INTO KW_Pembayaran_invoisBil_item (untuk_akaun,Data,no_permohonan,Kat_akaun,cre_kod_akaun,tarkih_mohon,tarkih_invois,no_invois,terma,deb_kod_akaun,item,keterangan,project_kod,unit,quantiti,discount,jumlah,tax,gst,Othgst,gsttype,othgsttype,gstjumlah,othgstjumlah,Overall,Status,crt_id,cr_dt,no_po) VALUES ('W-06030','BARU','" + ruj1_2 + "','SEMUA COA','04.14','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + ruj2_2 + "','','12.06','','" + ot_desc2 + "','--- PILIH ---','" + kwsp_ck.Text + "','1','0.00','" + kwsp_ck.Text + "','N','0','0','0','0','0.00','0.00','" + kwsp_ck.Text + "','B','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','')";
                            Status1 = DBCon.Ora_Execute_CommamdText(Inssql_invois_bil_item_kwsp_ck);
                        }

                        //KWSP CKM

                        if (kwsp_ckm.Text != "0.00")
                        {
                            string ot_desc3 = "" + date1 + " " + year1 + " - CARUMAN KWSP MAJIKAN";
                            string Inssql_kwsp_ckm = "INSERT INTO KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,kat_akaun,GL_invois_no,ref2,Tax_type,Tot_Amt,GL_process_dt,GL_desc1,GL_desc2,GL_nama_kod,crt_id,cr_dt,GL_sts) VALUES ('12.07','" + kwsp_ckm.Text + "','0.00','12','" + ruj1_2 + "','" + sel_staff.Rows[0]["stf_name"].ToString() + "','','0.00','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + ot_desc3 + "','','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A')";
                            Status1 = DBCon.Ora_Execute_CommamdText(Inssql_kwsp_ckm);

                            //invois bil item
                            string Inssql_invois_bil_item_kwsp_ckm = "INSERT INTO KW_Pembayaran_invoisBil_item (untuk_akaun,Data,no_permohonan,Kat_akaun,cre_kod_akaun,tarkih_mohon,tarkih_invois,no_invois,terma,deb_kod_akaun,item,keterangan,project_kod,unit,quantiti,discount,jumlah,tax,gst,Othgst,gsttype,othgsttype,gstjumlah,othgstjumlah,Overall,Status,crt_id,cr_dt,no_po) VALUES ('W-06030','BARU','" + ruj1_2 + "','SEMUA COA','04.14','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + ruj2_2 + "','','12.07','','" + ot_desc3 + "','--- PILIH ---','" + kwsp_ckm.Text + "','1','0.00','" + kwsp_ckm.Text + "','N','0','0','0','0','0.00','0.00','" + kwsp_ckm.Text + "','B','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','')";
                            Status1 = DBCon.Ora_Execute_CommamdText(Inssql_invois_bil_item_kwsp_ckm);
                        }

                        //PERKESO KAKITANGAN

                        if (per_kak.Text != "0.00")
                        {
                            string ot_desc4 = "" + date1 + " " + year1 + " - POTONGAN PERKESO";
                            string Inssql_kwsp_ckm = "INSERT INTO KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,kat_akaun,GL_invois_no,ref2,Tax_type,Tot_Amt,GL_process_dt,GL_desc1,GL_desc2,GL_nama_kod,crt_id,cr_dt,GL_sts) VALUES ('12.08','" + per_kak.Text + "','0.00','12','" + ruj1_3 + "','" + sel_staff.Rows[0]["stf_name"].ToString() + "','','0.00','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + ot_desc4 + "','','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A')";
                            Status1 = DBCon.Ora_Execute_CommamdText(Inssql_kwsp_ckm);

                            //invois bil item
                            string Inssql_invois_bil_item_per_kak = "INSERT INTO KW_Pembayaran_invoisBil_item (untuk_akaun,Data,no_permohonan,Kat_akaun,cre_kod_akaun,tarkih_mohon,tarkih_invois,no_invois,terma,deb_kod_akaun,item,keterangan,project_kod,unit,quantiti,discount,jumlah,tax,gst,Othgst,gsttype,othgsttype,gstjumlah,othgstjumlah,Overall,Status,crt_id,cr_dt,no_po) VALUES ('W-06030','BARU','" + ruj1_3 + "','SEMUA COA','04.15','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + ruj2_3 + "','','12.08','','" + ot_desc4 + "','--- PILIH ---','" + per_kak.Text + "','1','0.00','" + per_kak.Text + "','N','0','0','0','0','0.00','0.00','" + per_kak.Text + "','B','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','')";
                            Status1 = DBCon.Ora_Execute_CommamdText(Inssql_invois_bil_item_per_kak);

                        }

                        //PERKESO MAJIKAN

                        if (per_maj.Text != "0.00")
                        {
                            string ot_desc5 = "" + date1 + " " + year1 + " - PERKESO MAJIKAN";
                            string Inssql_kwsp_ckm = "INSERT INTO KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,kat_akaun,GL_invois_no,ref2,Tax_type,Tot_Amt,GL_process_dt,GL_desc1,GL_desc2,GL_nama_kod,crt_id,cr_dt,GL_sts) VALUES ('12.09','" + per_maj.Text + "','0.00','12','" + ruj1_3 + "','" + sel_staff.Rows[0]["stf_name"].ToString() + "','','0.00','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + ot_desc5 + "','','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A')";
                            Status1 = DBCon.Ora_Execute_CommamdText(Inssql_kwsp_ckm);

                            //invois bil item
                            string Inssql_invois_bil_item_per_maj = "INSERT INTO KW_Pembayaran_invoisBil_item (untuk_akaun,Data,no_permohonan,Kat_akaun,cre_kod_akaun,tarkih_mohon,tarkih_invois,no_invois,terma,deb_kod_akaun,item,keterangan,project_kod,unit,quantiti,discount,jumlah,tax,gst,Othgst,gsttype,othgsttype,gstjumlah,othgstjumlah,Overall,Status,crt_id,cr_dt,no_po) VALUES ('W-06030','BARU','" + ruj1_3 + "','SEMUA COA','04.15','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + ruj2_3 + "','','12.09','','" + ot_desc5 + "','--- PILIH ---','" + per_maj.Text + "','1','0.00','" + per_maj.Text + "','N','0','0','0','0','0.00','0.00','" + per_maj.Text + "','B','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','')";
                            Status1 = DBCon.Ora_Execute_CommamdText(Inssql_invois_bil_item_per_maj);

                        }

                        //PERKESO SIP

                        if (sip.Text != "0.00")
                        {
                            string ot_desc6 = "" + date1 + " " + year1 + " - PERKESO SIP";
                            string Inssql_kwsp_ckm = "INSERT INTO KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,kat_akaun,GL_invois_no,ref2,Tax_type,Tot_Amt,GL_process_dt,GL_desc1,GL_desc2,GL_nama_kod,crt_id,cr_dt,GL_sts) VALUES ('12.10','" + sip.Text + "','0.00','12','" + ruj1_4 + "','" + sel_staff.Rows[0]["stf_name"].ToString() + "','','0.00','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + ot_desc6 + "','','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A')";
                            Status1 = DBCon.Ora_Execute_CommamdText(Inssql_kwsp_ckm);

                            //invois bil item
                            string Inssql_invois_bil_item_sip = "INSERT INTO KW_Pembayaran_invoisBil_item (untuk_akaun,Data,no_permohonan,Kat_akaun,cre_kod_akaun,tarkih_mohon,tarkih_invois,no_invois,terma,deb_kod_akaun,item,keterangan,project_kod,unit,quantiti,discount,jumlah,tax,gst,Othgst,gsttype,othgsttype,gstjumlah,othgstjumlah,Overall,Status,crt_id,cr_dt,no_po) VALUES ('W-06030','BARU','" + ruj1_4 + "','SEMUA COA','04.16','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + ruj2_4 + "','','12.10','','" + ot_desc6 + "','--- PILIH ---','" + sip.Text + "','1','0.00','" + sip.Text + "','N','0','0','0','0','0.00','0.00','" + sip.Text + "','B','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','')";
                            Status1 = DBCon.Ora_Execute_CommamdText(Inssql_invois_bil_item_sip);
                        }

                        //PERKESO SIP MAJIKAN

                        if (sip_maj.Text != "0.00")
                        {
                            string ot_desc7 = "" + date1 + " " + year1 + " - PERKESO SIP MAJIKAN";
                            string Inssql_kwsp_ckm = "INSERT INTO KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,kat_akaun,GL_invois_no,ref2,Tax_type,Tot_Amt,GL_process_dt,GL_desc1,GL_desc2,GL_nama_kod,crt_id,cr_dt,GL_sts) VALUES ('12.11','" + sip_maj.Text + "','0.00','12','" + ruj1_4 + "','" + sel_staff.Rows[0]["stf_name"].ToString() + "','','0.00','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + ot_desc7 + "','','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A')";
                            Status1 = DBCon.Ora_Execute_CommamdText(Inssql_kwsp_ckm);

                            //invois bil item
                            string Inssql_invois_bil_item_sip_maj = "INSERT INTO KW_Pembayaran_invoisBil_item (untuk_akaun,Data,no_permohonan,Kat_akaun,cre_kod_akaun,tarkih_mohon,tarkih_invois,no_invois,terma,deb_kod_akaun,item,keterangan,project_kod,unit,quantiti,discount,jumlah,tax,gst,Othgst,gsttype,othgsttype,gstjumlah,othgstjumlah,Overall,Status,crt_id,cr_dt,no_po) VALUES ('W-06030','BARU','" + ruj1_4 + "','SEMUA COA','04.16','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + ruj2_4 + "','','12.11','','" + ot_desc7 + "','--- PILIH ---','" + sip_maj.Text + "','1','0.00','" + sip_maj.Text + "','N','0','0','0','0','0.00','0.00','" + sip_maj.Text + "','B','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','')";
                            Status1 = DBCon.Ora_Execute_CommamdText(Inssql_invois_bil_item_sip_maj);

                        }

                        //LHDN PCB

                        if (pcb.Text != "0.00")
                        {
                            string ot_desc8 = "" + date1 + " " + year1 + " - LHDN PCB";
                            string Inssql_kwsp_ckm = "INSERT INTO KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,kat_akaun,GL_invois_no,ref2,Tax_type,Tot_Amt,GL_process_dt,GL_desc1,GL_desc2,GL_nama_kod,crt_id,cr_dt,GL_sts) VALUES ('12.12','" + pcb.Text + "','0.00','12','" + ruj1_5 + "','" + sel_staff.Rows[0]["stf_name"].ToString() + "','','0.00','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + ot_desc8 + "','','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A')";
                            Status1 = DBCon.Ora_Execute_CommamdText(Inssql_kwsp_ckm);

                            //invois bil item
                            //string Inssql_invois_bil_item_pcb = "INSERT INTO KW_Pembayaran_invoisBil_item (untuk_akaun,Data,no_permohonan,Kat_akaun,cre_kod_akaun,tarkih_mohon,tarkih_invois,no_invois,terma,deb_kod_akaun,item,keterangan,project_kod,unit,quantiti,discount,jumlah,tax,gst,Othgst,gsttype,othgsttype,gstjumlah,othgstjumlah,Overall,Status,crt_id,cr_dt,no_po) VALUES ('W-06030','BARU','" + ruj1_5 + "','SEMUA COA','04.09','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + ruj2_5 + "','','12.12','','" + ot_desc8 + "','--- PILIH ---','" + pcb.Text + "','1','0.00','" + pcb.Text + "','N','0','0','0','0','0.00','0.00','" + pcb.Text + "','B','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','')";
                            //Status1 = DBCon.Ora_Execute_CommamdText(Inssql_invois_bil_item_pcb);

                        }

                        //LHDN CP 38

                        if (cp38.Text != "0.00")
                        {
                            string ot_desc9 = "" + date1 + " " + year1 + " - LHDN CP 38";
                            string Inssql_kwsp_ckm = "INSERT INTO KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,kat_akaun,GL_invois_no,ref2,Tax_type,Tot_Amt,GL_process_dt,GL_desc1,GL_desc2,GL_nama_kod,crt_id,cr_dt,GL_sts) VALUES ('12.13','" + cp38.Text + "','0.00','12','" + ruj1_6 + "','" + sel_staff.Rows[0]["stf_name"].ToString() + "','','0.00','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + ot_desc9 + "','','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A')";
                            Status1 = DBCon.Ora_Execute_CommamdText(Inssql_kwsp_ckm);

                            //invois bil item
                            //string Inssql_invois_bil_item_cp38 = "INSERT INTO KW_Pembayaran_invoisBil_item (untuk_akaun,Data,no_permohonan,Kat_akaun,cre_kod_akaun,tarkih_mohon,tarkih_invois,no_invois,terma,deb_kod_akaun,item,keterangan,project_kod,unit,quantiti,discount,jumlah,tax,gst,Othgst,gsttype,othgsttype,gstjumlah,othgstjumlah,Overall,Status,crt_id,cr_dt,no_po) VALUES ('W-06030','BARU','" + ruj1_6 + "','SEMUA COA','04.10','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + ruj2_6 + "','','12.13','','" + ot_desc9 + "','--- PILIH ---','" + cp38.Text + "','1','0.00','" + cp38.Text + "','N','0','0','0','0','0.00','0.00','" + cp38.Text + "','B','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','')";
                            //Status1 = DBCon.Ora_Execute_CommamdText(Inssql_invois_bil_item_cp38);
                        }

                        //Tunggakan

                        

                        if (tung_amt.Text != "0.00")
                        {
                            string ot_desc_tun = "" + date1 + " " + year1 + " - Tunggakan";
                            string Inssql_kwsp_ckm = "INSERT INTO KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,kat_akaun,GL_invois_no,ref2,Tax_type,Tot_Amt,GL_process_dt,GL_desc1,GL_desc2,GL_nama_kod,crt_id,cr_dt,GL_sts) VALUES ('" + sel_staff.Rows[0]["stf_kod_akaun"].ToString() + "','" + tung_amt.Text + "','0.00','12','" + ruj1_1 + "','" + sel_staff.Rows[0]["stf_name"].ToString() + "','','0.00','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + ot_desc_tun + "','','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A')";
                            Status1 = DBCon.Ora_Execute_CommamdText(Inssql_kwsp_ckm);
                        }


                        //Deduction
                        

                        DataTable deduct_all = new DataTable();
                        deduct_all = DBCon.Ora_Execute_table("select * from hr_deduction where ('" + act_dt + "' between FORMAT(ded_start_dt,'yyyy-MM') And FORMAT(ded_end_dt,'yyyy-MM')) and ded_staff_no='" + nokak.Text + "'");

                        DataTable deduct_all_sum = new DataTable();
                        deduct_all_sum = DBCon.Ora_Execute_table("select ISNULL(sum(ded_deduct_amt),'0.00') as amt1 from hr_deduction where ('" + act_dt + "' between FORMAT(ded_start_dt,'yyyy-MM') And FORMAT(ded_end_dt,'yyyy-MM')) and ded_staff_no='" + nokak.Text + "'");

                        if (deduct_all_sum.Rows[0]["amt1"].ToString() != "0.00")
                        {
                            string ot_desc12 = "" + date1 + " " + year1 + " - LAIN-LAIN POTONGAN";

                            string Inssql_deduct = "INSERT INTO KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,kat_akaun,GL_invois_no,ref2,Tax_type,Tot_Amt,GL_process_dt,GL_desc1,GL_desc2,GL_nama_kod,crt_id,cr_dt,GL_sts) VALUES ('" + sel_staff.Rows[0]["stf_kod_akaun"].ToString() + "','" + deduct_all_sum.Rows[0]["amt1"].ToString() + "','0.00','12','" + ruj1_1 + "','" + sel_staff.Rows[0]["stf_name"].ToString() + "','','0.00','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + ot_desc12 + "','','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A')";
                            Status1 = DBCon.Ora_Execute_CommamdText(Inssql_deduct);
                        }

                        if (deduct_all.Rows.Count != 0)
                        {
                            for (int k3 = 0; k3 < deduct_all.Rows.Count; k3++)
                            {
                                DataTable sel_poto = new DataTable();
                                sel_poto = DBCon.Ora_Execute_table("select * from Ref_hr_potongan where hr_poto_Code='" + deduct_all.Rows[k3]["ded_deduct_type_cd"].ToString() + "'");
                             

                                if (sel_poto.Rows[0]["elau_jenis_akaun"].ToString() != "")
                                {
                                    DataTable sel_kat_akaun = new DataTable();
                                    sel_kat_akaun = DBCon.Ora_Execute_table("select * from kw_ref_carta_akaun where kod_akaun='" + sel_poto.Rows[0]["elau_jenis_akaun"].ToString() + "'");

                                    string Inssql_deduct_kredit = "INSERT INTO KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,kat_akaun,GL_invois_no,ref2,Tax_type,Tot_Amt,GL_process_dt,GL_desc1,GL_desc2,GL_nama_kod,crt_id,cr_dt,GL_sts) VALUES ('" + sel_poto.Rows[0]["elau_jenis_akaun"].ToString() + "','0.00','" + deduct_all.Rows[k3]["ded_deduct_amt"].ToString() + "','" + sel_kat_akaun.Rows[0]["kat_akaun"].ToString() + "','" + ruj1_1 + "','" + sel_staff.Rows[0]["stf_name"].ToString() + "','','0.00','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + "" + date1 + " " + year1 + " - " + sel_poto.Rows[0]["hr_poto_desc"].ToString() + "','','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A')";
                                    Status2 = DBCon.Ora_Execute_CommamdText(Inssql_deduct_kredit);
                                }
                            }
                        }

                        //PENDAPATAN BERSIH

                        if (net.Text != "0.00")
                        {
                            decimal ssv1 = 0;
                            string ot_desc10 = "" + date1 + " " + year1 + " - PENDAPATAN BERSIH";
                            double vv_11 = (double.Parse(famt.Text) + double.Parse(eamt.Text) + double.Parse(bonus.Text) + double.Parse(bonus_kpi.Text) + double.Parse(ot.Text));
                            decimal ss_vv2 = Convert.ToDecimal(vv_11);
                            decimal ss_vv1 = decimal.Parse(net.Text);
                            ssv1 = (ss_vv1 - ss_vv2);
                            string Inssql_PB = "INSERT INTO KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,kat_akaun,GL_invois_no,ref2,Tax_type,Tot_Amt,GL_process_dt,GL_desc1,GL_desc2,GL_nama_kod,crt_id,cr_dt,GL_sts) VALUES ('"+ sel_staff.Rows[0]["stf_kod_akaun"].ToString() + "','" + ssv1 + "','0.00','12','" + ruj1_1 + "','" + sel_staff.Rows[0]["stf_name"].ToString() + "','','0.00','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + ot_desc10 + "','','" + sel_staff.Rows[0]["stf_kod_akaun"].ToString() + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A')";
                            Status1 = DBCon.Ora_Execute_CommamdText(Inssql_PB);

                            //invois bil item
                            string Inssql_invois_bil_item = "INSERT INTO KW_Pembayaran_invoisBil_item (untuk_akaun,Data,no_permohonan,Kat_akaun,cre_kod_akaun,tarkih_mohon,tarkih_invois,no_invois,terma,deb_kod_akaun,item,keterangan,project_kod,unit,quantiti,discount,jumlah,tax,gst,Othgst,gsttype,othgsttype,gstjumlah,othgstjumlah,Overall,Status,crt_id,cr_dt,no_po) VALUES ('W-06030','BARU','" + ruj1_1 + "','SEMUA COA','04.02','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + ruj2_1 + "','','" + sel_staff.Rows[0]["stf_kod_akaun"].ToString() + "','','" + ot_desc10 + "','--- PILIH ---','" + ssv1 + "','1','0.00','" + ssv1 + "','N','0','0','0','0','0.00','0.00','" + ssv1 + "','B','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','')";
                            Status1 = DBCon.Ora_Execute_CommamdText(Inssql_invois_bil_item);

                        }

                        if (Status1 == "SUCCESS")
                        {
                            string upd_sql = "UPdate hr_income set inc_jurnal_no='" + ruj1_1 + "',inc_sts='1' where inc_staff_no='" + nokak.Text + "' and inc_month='" + mnth.Text + "' and inc_year='" + year.Text + "'";
                            Status2 = DBCon.Ora_Execute_CommamdText(upd_sql);

                        }
                    }

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }

        if (Status1 == "SUCCESS")
        {
            service.audit_trail("P0071", "Jana Jurnal & Post Ke Lejer", dd_org.SelectedItem.Text, DropDownList2.SelectedItem.Text + "_" + TextBox4.Text);
            grid_view();            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
        }

    }

    void get_bind_details()
    {
        act_dt = TextBox4.Text + "-" + DropDownList2.SelectedValue;
        
        if (dd_org.SelectedValue.Trim() != "" && dd_org_pen.SelectedValue == "" && dd_jabatan.SelectedValue.Trim() == "" && act_dt != "")
        {
            upd_val2 = "inc_gen_id='" + dd_org.SelectedValue.Trim() + "' and CONCAT(inc_year,inc_month) = '" + act_dt + "'";
        }
        else if (dd_org.SelectedValue.Trim() != "" && dd_org_pen.SelectedValue != "" && dd_jabatan.SelectedValue.Trim() == "" && act_dt != "")
        {
            upd_val2 = "inc_gen_id='" + dd_org.SelectedValue.Trim() + "' and inc_sub_org_id='" + dd_org_pen.SelectedValue + "' and CONCAT(inc_year,inc_month) = '" + act_dt + "'";
        }
        else if (dd_org.SelectedValue.Trim() != "" && dd_org_pen.SelectedValue != "" && dd_jabatan.SelectedValue.Trim() != "" && act_dt != "")
        {
            upd_val2 = "inc_gen_id='" + dd_org.SelectedValue.Trim() + "' and inc_sub_org_id='" + dd_org_pen.SelectedValue + "' and inc_dept_cd='" + dd_jabatan.SelectedValue.Trim() + "' and CONCAT(inc_year,inc_month) = '" + act_dt + "'";
        }
        else if (dd_org.SelectedValue.Trim() == "" && dd_org_pen.SelectedValue == "" && dd_jabatan.SelectedValue.Trim() == "" && act_dt != "")
        {
            upd_val2 = "CONCAT(inc_year,inc_month) = '" + act_dt + "'";
        }
        else
        {
            upd_val2 = "inc_org_id='00'";
        }
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvrow in gvCustomers.Rows)
        {
            var checkbox = gvrow.FindControl("CheckBox1") as CheckBox;
            if (checkbox.Checked)
            {
                var nokak = gvrow.FindControl("labstf") as Label;
                var gred = gvrow.FindControl("lagred") as Label;
                var labsal = gvrow.FindControl("labsal") as Label;
                var famt = gvrow.FindControl("txt_nama") as Label;
                var eamt = gvrow.FindControl("txt_nama1") as Label;
                var ot = gvrow.FindControl("labot") as Label;
                var kwsp = gvrow.FindControl("lapkwap") as Label;
                var kwsp1 = gvrow.FindControl("lapkwap1") as Label;
                var per = gvrow.FindControl("labper") as Label;
                var per1 = gvrow.FindControl("labper1") as Label;
                var pcb = gvrow.FindControl("labpcb") as Label;
                var gross = gvrow.FindControl("labgross") as Label;
                var net = gvrow.FindControl("labnet") as Label;
                DataTable dt = new DataTable();
                dt = DBCon.Ora_Execute_table("select inc_staff_no from hr_income where inc_staff_no='" + nokak.Text + "'");

                DataTable ddicno1 = new DataTable();
                //ddicno1 = DBCon.Ora_Execute_table("select pos_org_id,pos_gen_ID,pos_dept_cd from hr_post_his where pos_staff_no='" + txt_stffno.Text + "' and pos_end_dt='9999-12-31'");
                ddicno1 = DBCon.Ora_Execute_table("select stf_staff_no,stf_curr_dept_cd,str_curr_org_cd,ho.org_id from hr_staff_profile left join hr_organization ho on ho.org_gen_id=str_curr_org_cd where stf_staff_no='" + nokak.Text + "'");
                DataTable Get_kwsp = new DataTable();
                Get_kwsp = DBCon.Ora_Execute_table("select epf_emp_kwsp_perc,epf_emp_kwsp_amt from hr_kwsp where epf_staff_no='" + nokak.Text + "' and epf_end_dt='9999-12-31' and ('" + DateTime.Now.Year.ToString() + " - " + DateTime.Now.Month.ToString("MM") + "' between FORMAT(epf_eff_dt,'yyyy-MM') And FORMAT(epf_end_dt,'yyyy-MM'))");
                //TextBox23.Text = Get_kwsp.Rows[0]["epf_percentage"].ToString();
                string kwsamt, kwsper;
                if (Get_kwsp.Rows.Count != 0 && Get_kwsp.Rows[0]["epf_emp_kwsp_amt"].ToString() != "")
                {
                    double amt_kwsp = double.Parse(Get_kwsp.Rows[0]["epf_emp_kwsp_amt"].ToString());
                    kwsamt = amt_kwsp.ToString("C").Replace("$", "").Replace("RM", "");
                    kwsper = Get_kwsp.Rows[0]["epf_emp_kwsp_perc"].ToString();
                }
                else
                {
                    kwsamt = "0.00";
                    kwsper = "";
                }
                DataTable cp38_amt1 = new DataTable();
                cp38_amt1 = DBCon.Ora_Execute_table("select tax_cp38_amt1 from hr_income_tax where tax_staff_no='" + nokak.Text + "' and (('" + DateTime.Now.Year.ToString() + " - " + DateTime.Now.Month.ToString("MM") + "') between FORMAT(tax_cp38_start_dt1,'yyyy-MM') And FORMAT(tax_cp38_end_dt1,'yyyy-MM'))");
                string cp;
                if (cp38_amt1.Rows.Count != 0)
                {
                    double amm4 = double.Parse(cp38_amt1.Rows[0]["tax_cp38_amt1"].ToString());
                    cp = amm4.ToString("C").Replace("$", "").Replace("RM", "");
                }
                else
                {
                    cp = "0.00";
                }
                double a, b, c, d, f, g;
                string ctg = "0.00";
                DataTable ddt = new DataTable();
                ddt = DBCon.Ora_Execute_table("select inc_ctg_amt+inc_kwsp_amt+inc_perkeso_amt+inc_pcb_amt+inc_cp38_amt+sum(hr_income_deduct.ind_deduct_amt) from hr_income inner join hr_income_deduct on hr_income.inc_staff_no=hr_income_deduct.ind_staff_no where inc_staff_no='" + nokak.Text + "' group by inc_ctg_amt,inc_kwsp_amt,inc_perkeso_amt,inc_pcb_amt,inc_cp38_amt");
                a = Convert.ToDouble(ctg.ToString());
                b = Convert.ToDouble(kwsp.Text.Trim().ToString());
                d = Convert.ToDouble(per.Text.Trim().ToString());
                f = Convert.ToDouble(pcb.Text.Trim().ToString());
                g = Convert.ToDouble(ddt.Rows[0][0].ToString());
                c = a + b + d + f + g;
                string M = Math.Round(decimal.Parse(c.ToString()), 2).ToString("0.00");

              
                string fdate = DropDownList1.SelectedValue;
                string tdate = TextBox4.Text;
                if (dt.Rows.Count == 0)
                {

                    DataTable dti = new DataTable();
                    dti = DBCon.Ora_Execute_table("insert into hr_income (inc_staff_no,inc_month,inc_year,inc_grade_cd,inc_org_id,inc_dept_cd,inc_salary_amt,inc_cumm_fix_allwnce_amt,inc_cumm_xtra_allwnce_amt,inc_cumm_deduct_amt,inc_bonus_amt,inc_kpi_bonus_amt,inc_gross_amt,inc_ctg_amt,inc_kwsp_amt,inc_perkeso_amt,inc_pcb_amt,inc_cp38_amt,inc_total_deduct_amt,inc_nett_amt,inc_ot_amt,inc_crt_id,inc_crt_dt,inc_emp_perkeso_amt,inc_emp_kwsp_amt,inc_emp_kwsp_perc,inc_gen_id)values('" + nokak.Text + "','" + fdate + "','" + tdate + "','" + gred.Text + "','" + dd_org.SelectedItem.Value + "','" + dd_jabatan.SelectedItem.Value + "','" + labsal.Text + "','" + famt.Text + "','" + eamt.Text + "','" + ddt.Rows[0][0].ToString() + "','0.00','0.00','" + gross.Text + "','" + ctg + "','" + kwsp.Text + "','" + per.Text + "','" + pcb.Text + "','" + cp + "','" + M + "','" + net.Text + "','" + ot.Text + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + per1.Text + "','" + kwsamt + "','" + kwsper + "','" + ddicno1.Rows[0]["str_curr_org_cd"].ToString() + "')");
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
                else
                {

                    DataTable ins_aud1 = new DataTable();
                    DBCon.Execute_CommamdText("update hr_income set inc_emp_perkeso_amt='" + per1.Text + "',inc_emp_kwsp_amt='" + kwsamt + "',inc_emp_kwsp_perc='" + kwsper + "',inc_grade_cd='" + gred.Text + "',inc_org_id='" + ddicno1.Rows[0]["org_id"].ToString() + "',inc_dept_cd='" + ddicno1.Rows[0]["stf_curr_dept_cd"].ToString() + "',inc_salary_amt='" + labsal.Text + "',inc_cumm_fix_allwnce_amt='" + famt.Text + "',inc_cumm_xtra_allwnce_amt='" + eamt.Text + "',inc_cumm_deduct_amt ='" + ddt.Rows[0][0].ToString() + "',inc_bonus_amt ='0.00',inc_kpi_bonus_amt ='0.00',inc_gross_amt ='" + gross.Text + "',inc_ctg_amt ='" + ctg + "',inc_kwsp_amt ='" + kwsp.Text + "',inc_perkeso_amt ='" + per.Text + "',inc_pcb_amt ='" + pcb.Text + "',inc_cp38_amt ='" + cp + "',inc_total_deduct_amt ='" + M + "',inc_nett_amt ='" + net.Text + "',inc_ot_amt='" + ot.Text + "',inc_gen_id='" + ddicno1.Rows[0]["str_curr_org_cd"].ToString() + "'  where inc_staff_no='" + nokak.Text + "' and inc_month='" + fdate + "' and inc_year='" + tdate + "'");                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya DiKemskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
            }
        }
    }

    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        //for (int cellNum = gvCustomers.Columns.Count - 1; cellNum >= 0; cellNum--)
        //{
        //    for (int i = 0; i < notNulls.Length; i++)
        //    {
        //        Boolean myLocalBool = notNulls[i];
        //        if (myLocalBool == false)
        //        {
        //            gvCustomers.Columns[i].Visible = false;

        //        }
        //    }

        //}
    }

    //protected void GridView1_OnRowCreated(object sender, GridViewRowEventArgs e)
    //{
    //    string vv1 = string.Empty;

    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        DataRowView drv = (DataRowView)e.Row.DataItem;
    //        if ((DataRowView)e.Row.DataItem != null)
    //        {
    //            for (int i = 0; i < gvCustomers.Columns.Count; i++)
    //            {

    //                if (i == 6)
    //                {
    //                    if (drv[i + 2].ToString() != "")
    //                    {
    //                        vv1 = double.Parse(drv[i + 2].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
    //                    }
    //                    else
    //                    {
    //                        vv1 = "1";
    //                    }
    //                }
    //                else if (i == 7)
    //                {
    //                    if (drv[i + 2].ToString() != "")
    //                    {
    //                        vv1 = double.Parse(drv[i + 2].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
    //                    }
    //                    else
    //                    {
    //                        vv1 = "1";
    //                    }
    //                }
    //                else if (i == 8)
    //                {
    //                    if (drv[i + 2].ToString() != "")
    //                    {
    //                        vv1 = double.Parse(drv[i + 2].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
    //                    }
    //                    else
    //                    {
    //                        vv1 = "1";
    //                    }
    //                }
    //                else if (i == 9)
    //                {
    //                    if (drv[i + 2].ToString() != "")
    //                    {
    //                        vv1 = double.Parse(drv[i + 2].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
    //                    }
    //                    else
    //                    {
    //                        vv1 = "1";
    //                    }
    //                }
    //                else if (i == 10)
    //                {
    //                    if (drv[i + 2].ToString() != "")
    //                    {
    //                        vv1 = double.Parse(drv[i + 2].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
    //                    }
    //                    else
    //                    {
    //                        vv1 = "1";
    //                    }
    //                }
    //                else
    //                {
    //                    vv1 = "1";
    //                }

    //                if (vv1 == "0.00" || vv1 == "0.0000" || vv1 == "0")
    //                {
    //                    notNulls[i] = false;

    //                }
    //                else
    //                {
    //                    notNulls[i] = true;
    //                }

    //            }

    //        }
    //    }

    //}
    protected void clk_rset(object sender, EventArgs e)
    {
        Response.Redirect("../SUMBER_MANUSIA/HR_SEMAK_HAJI.aspx");
    }


}