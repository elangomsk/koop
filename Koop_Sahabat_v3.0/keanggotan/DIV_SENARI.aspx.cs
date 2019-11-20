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
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Web.SessionState;
using System.Xml;
using Microsoft.Reporting.WinForms;
using Microsoft.Reporting.WebForms;

public partial class DIV_SENARI : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    DataTable wilayah = new DataTable();
    string level, userid;
    string role_1 = string.Empty, role_view = string.Empty, role_add = string.Empty, role_edit = string.Empty;
    string strJenisPerm = string.Empty;
    string strJenisLap = string.Empty;
    string strTarikhMula = string.Empty;
    string strTarikhAkhir = string.Empty;
    string strArea = string.Empty;
    string strRegion = string.Empty;
    string strBranch = string.Empty;
    string strSts = string.Empty;
    string strQuery = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();

        if (!this.IsPostBack)
        {
            if (Session["New"] != null)
            {
                f_date.Text = DateTime.Now.ToString("dd/MM/yyyy");
                t_date.Text = DateTime.Now.ToString("dd/MM/yyyy");
                level = Session["level"].ToString();
                userid = Session["New"].ToString();
                kawasan();
                bindWilayah();
                bindCawangan();
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('152','1052','1093','153','64','65','22','1034','148','25','14','1042','1094')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            //ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            //ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            //ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
            //ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            //ps_lbl10.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            //Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl13.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());

        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }

    void assgn_roles()
    {
        DataTable ddokdicno = new DataTable();
        ddokdicno = DBCon.Ora_Execute_table("select * from KK_User_Login where KK_userid = '" + Session["userid"].ToString() + "'");

        if (ddokdicno.Rows.Count != 0)
        {
            DataTable ddokdicno_1 = new DataTable();
            ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0144' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

            if (ddokdicno_1.Rows.Count != 0)
            {
                role_1 = ddokdicno_1.Rows[0]["ctrl_type"].ToString();
                role_view = ddokdicno_1.Rows[0]["view_chk"].ToString();
                role_add = ddokdicno_1.Rows[0]["Add_chk"].ToString();
                role_edit = ddokdicno_1.Rows[0]["edit_chk"].ToString();
                if (role_add == "1")
                {
                    Button2.Visible = true;
                }
                else
                {
                    Button2.Visible = false;
                }

            }
        }
    }

    
    void kawasan()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select DISTINCT Area_Code,Area_Name from Ref_Kawasan ORDER BY Area_Name";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            if (dt != null)
            {
                adpt.Fill(dt);
                DD_kaw.DataSource = dt;
                DD_kaw.DataTextField = "Area_Name";
                DD_kaw.DataValueField = "Area_Code";
                DD_kaw.DataBind();
                DD_kaw.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            }
            else
            {
                DD_kaw.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void DD_kaw_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindWilayah();
        bindCawangan();
    }
    private void bindWilayah()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select distinct wilayah_code,wilayah_name from Ref_Cawangan where kavasan_name='" + DD_kaw.SelectedItem.Text + "' order by wilayah_name asc ";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            if (dt != null)
            {
                DD_wilayah.DataSource = dt;
                DD_wilayah.DataTextField = "wilayah_name";
                DD_wilayah.DataValueField = "wilayah_code";
                DD_wilayah.DataBind();
                DD_wilayah.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            }
            else
            {
                DD_wilayah.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void DD_wilayah_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindCawangan();
    }
    private void bindCawangan()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select cawangan_code,cawangan_name From Ref_Cawangan where kavasan_name='" + DD_kaw.SelectedItem.Text + "' and wilayah_name='" + DD_wilayah.SelectedItem.Text + "' order by cawangan_name asc";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            if (dt != null)
            {
                DD_cawangan.DataSource = dt;
                DD_cawangan.DataTextField = "cawangan_name";
                DD_cawangan.DataValueField = "cawangan_code";
                DD_cawangan.DataBind();
                DD_cawangan.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            }
            else
            {
                DD_cawangan.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void rst_clk(object sender, EventArgs e)
    {
        Response.Redirect("../keanggotan/DIV_SENARI.aspx");
    }

    void get_filt_details()
    {
        strJenisPerm = DD_JenPerm.SelectedValue;
        strJenisLap = DD_JenLap.SelectedValue;
        strTarikhMula = f_date.Text;
        strTarikhAkhir = t_date.Text;
        strArea = DD_kaw.SelectedValue;
        strRegion = DD_wilayah.SelectedValue;
        strBranch = DD_cawangan.SelectedValue;
        if(DropDownList1.SelectedValue == "N")
        {
            strSts = "mem_sts_cd = 'DN' and ISNULL(set_batch_name,'') = '' and";
        }
        else
        {
            strSts = "mem_sts_cd = 'TS' and";
        }
        DateTime fd = DateTime.ParseExact(strTarikhMula, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        String fmdate = fd.ToString("yyyy-MM-dd");

        DateTime td = DateTime.ParseExact(strTarikhAkhir, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        String tmdate = td.ToString("yyyy-MM-dd");
        if (strArea != "" && strRegion == "" && strBranch == "")
        {
            strQuery = ""+strSts+" set_appl_type_cd='"+ strJenisPerm + "' and set_appprove_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and set_appprove_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate +"'), +1)  and  s1.kawasan_code='" + strArea + "'";
        }
        else if (strArea != "" && strRegion != "" && strBranch == "")
        {
            strQuery = " " + strSts + " set_appl_type_cd='" + strJenisPerm + "' and set_appprove_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and set_appprove_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and s1.kawasan_code='" + strArea + "' and s1.wilayah_code='" + strRegion + "'";
        }
        else if (strArea != "" && strRegion != "" && strBranch != "")
        {
            strQuery = " " + strSts + " set_appl_type_cd='" + strJenisPerm + "' and set_appprove_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and set_appprove_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and s1.kawasan_code='" + strArea + "' and s1.wilayah_code='" + strRegion + "' and s1.cawangan_code='" + strBranch + "'";
        }
        else if (strArea == "" && strRegion == "" && strBranch == "")
        {
            strQuery = " " + strSts + " set_appl_type_cd='" + strJenisPerm + "' and set_appprove_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and set_appprove_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1)";
        }
        else
        {
            strQuery = "";
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dtDataLaporan = new DataTable();
            get_filt_details();
            //string strPusat = txt_pusat.Text;

           
            if (strJenisLap == "1")
            {
                strQuery = "select s2.Applicant_Name,s1.kavasan_name,s1.wilayah_name,s1.cawangan_name,s3.DESCRRIPTION as set_reason_cd,count(mem_new_icno) BilAnggota,sum(set_apply_amt) set_apply_amt from mem_member mm "
                            + " inner join mem_settlement ms on ms.set_member_no=mm.mem_member_no and ms.set_new_icno=mm.mem_new_icno"
                            + " left join Ref_Cawangan s1 on s1.cawangan_code=mm.mem_branch_cd"
                            + " left join Ref_Applicant_Category s2 on s2.Applicant_Code=mm.mem_applicant_type_cd"
                            + " left join Ref_Sebab s3 on s3.DESCRRIPTION_CODE=ms.set_reason_cd"
                            + " where ms.Acc_sts='Y' and ms.set_approve_sts_cd='SA' and " + strQuery + " "
                            + " group by s2.Applicant_Name,s1.kavasan_name,s1.wilayah_name,s1.cawangan_name,s3.DESCRRIPTION"
                            + " order by s2.Applicant_Name,s1.kavasan_name,s1.wilayah_name,s1.cawangan_name";
                dtDataLaporan = DBCon.Ora_Execute_table(strQuery);
                ds.Tables.Add(dtDataLaporan);
                //Building an HTML string.
                StringBuilder htmlTable = new StringBuilder();
                htmlTable.Append("<table border='1' id='GridView1' cell style='width:100%;' class='table uppercase table-striped'>");
                htmlTable.Append("<tr style='background-color:#BDC4C7;'> <th>KATEGORI ANGGOTA</th><th> NAMA Kawasan </th><th> NAMA WILLAYA </th><th> NAMA CAWANGAN </th><th> BIL. ANGGOTA </th><th> Jenis Penyelesaian </th><th> Amaun (RM) </th></tr>");

                if (!object.Equals(ds.Tables[0], null))
                {
                    //Building the Data rows.
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            htmlTable.Append("<tr class='text-uppercase'>");
                            htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["Applicant_Name"] + "</td>");
                            htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["kavasan_name"] + "</td>");
                            htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["wilayah_name"] + "</td>");
                            htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["cawangan_name"] + "</td>");
                            htmlTable.Append("<td align='center'>" + ds.Tables[0].Rows[i]["BilAnggota"] + "</td>");
                            htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["set_reason_cd"] + "</td>");
                            htmlTable.Append("<td align='Right'>" + double.Parse(ds.Tables[0].Rows[i]["set_apply_amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "") + "</td>");
                            htmlTable.Append("</tr>");
                        }
                        btnExpPDF.Visible = true;
                        btnExpExcel.Visible = true;
                        htmlTable.Append("</table>");
                        PlaceHolder2.Controls.Add(new Literal { Text = htmlTable.ToString() });
                    }
                    else
                    {
                        btnExpPDF.Visible = false;
                        btnExpExcel.Visible = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

                    }

                }
                string script1 = "$(function () { $('#GridView1').prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({ 'responsive': true, 'sPaginationType': 'full_numbers'   }); $('.select2').select2() });";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
            }
            else if (strJenisLap == "2")
            {
                strQuery = "select s2.Applicant_Name,s1.kavasan_name,s1.wilayah_name,s1.cawangan_name,mem_name,mem_member_no,mem_new_icno,s3.DESCRRIPTION as set_reason_cd,set_apply_amt,Format(set_appprove_dt,'dd/MM/yyyy') set_appprove_dt from mem_member mm "
                            + " inner join mem_settlement ms on ms.Acc_sts='Y' and ms.set_new_icno=mm.mem_new_icno"
                            + " left join Ref_Cawangan s1 on s1.cawangan_code=mm.mem_branch_cd"
                            + " left join Ref_Applicant_Category s2 on s2.Applicant_Code=mm.mem_applicant_type_cd"
                            + " left join Ref_Sebab s3 on s3.DESCRRIPTION_CODE=ms.set_reason_cd"
                            + " where ms.Acc_sts='Y'  and ms.set_approve_sts_cd='SA' and " + strQuery + ""
                            + " order by s2.Applicant_Name,s1.kavasan_name,s1.wilayah_name,s1.cawangan_name,mem_name";

                dtDataLaporan = DBCon.Ora_Execute_table(strQuery);
                ds.Tables.Add(dtDataLaporan);
                //Building an HTML string.
                StringBuilder htmlTable = new StringBuilder();
                htmlTable.Append("<table border='1' id='GridView2' cell style='width:100%;' class='table uppercase table-striped'>");
                htmlTable.Append("<tr style='background-color:#BDC4C7;'> <th> Kategori Anggota </th><th> Kawasan </th><th> Wilayah </th><th> Cawangan </th><th>Nama Anggota</th> <th> No Anggota </th><th> No. IC Anggota </th><th> Jenis Penyelesaian </th><th> Amaun (RM) </th><th> Tarikh Lulus </th> </tr>");

                if (!object.Equals(ds.Tables[0], null))
                {
                    //Building the Data rows.
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            htmlTable.Append("<tr class='text-uppercase'>");
                            htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["Applicant_Name"] + "</td>");
                            htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["kavasan_name"] + "</td>");
                            htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["wilayah_name"] + "</td>");
                            htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["cawangan_name"] + "</td>");
                            htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["mem_name"] + "</td>");
                            htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["mem_member_no"] + "</td>");
                            htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["mem_new_icno"] + "</td>");
                            htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["set_reason_cd"] + "</td>");
                            htmlTable.Append("<td align='Right'>" + double.Parse(ds.Tables[0].Rows[i]["set_apply_amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "") + "</td>");
                            htmlTable.Append("<td align='center'>" + ds.Tables[0].Rows[i]["set_appprove_dt"] + "</td>");
                            htmlTable.Append("</tr>");
                        }
                        btnExpPDF.Visible = true;
                        btnExpExcel.Visible = true;
                        htmlTable.Append("</table>");
                        PlaceHolder1.Controls.Add(new Literal { Text = htmlTable.ToString() });
                    }
                    else
                    {
                        btnExpPDF.Visible = false;
                        btnExpExcel.Visible = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

                    }

                }
                string script1 = "$(function () { $('#GridView2').prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({ 'responsive': true, 'sPaginationType': 'full_numbers'   }); $('.select2').select2() });";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
            }

               
            Session["dtDataLaporan"] = dtDataLaporan;
        }
        catch (Exception ex)
        {
            throw ex;
            //Response.Redirect("LK_SENARI.aspx");
        }
        ClientScript.RegisterClientScriptBlock(this.GetType(), "", "window.onload=function(){window.scrollTo(0,document.body.scrollHeight)};", true);
    }

    protected void btnExpPDF_Click(object sender, EventArgs e)
    {
        try
        {
            if (f_date.Text != "" && t_date.Text != "" && DD_kaw.SelectedIndex >= 0 && DD_wilayah.SelectedIndex >= 0 && DD_cawangan.SelectedIndex >= 0)
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                string rcount = string.Empty, rcount1 = string.Empty;
                int count = 0;
                double sum = 0;

                string strkaw = "";
                if (DD_kaw.SelectedIndex == 0)
                {
                    strkaw = "SEMUA";
                }
                else
                {
                    strkaw = DD_kaw.SelectedItem.Text;
                }
                string strwilayah = "";
                if (DD_wilayah.SelectedIndex == 0)
                {
                    strwilayah = "SEMUA";
                }
                else
                {
                    strwilayah = DD_wilayah.SelectedItem.Text;
                }
                string strcawangan = "";
                if (DD_cawangan.SelectedIndex == 0)
                {
                    strcawangan = "SEMUA";
                }
                else {
                    strcawangan = DD_cawangan.SelectedItem.Text;
                }

               dt = (DataTable)Session["dtDataLaporan"];
                DataTable thisdt = dt.Copy();
                ds.Tables.Add(thisdt);
                DataTable dtdummy = new DataTable();
                DataTable dtItemTable = new DataTable();
                dtItemTable = dt.Copy();
                List<DataRow> listResult = dt.AsEnumerable().ToList();
                //listResult.Count();
                int countRow = 0;
                countRow = listResult.Count();
                rcount = "1";

                int countanggota = 0;
                if (DD_JenLap.SelectedValue == "1")
                {
                    if (dtItemTable != null)
                    {
                        for (int i = 0; i < dtItemTable.Rows.Count; i++)
                        {
                            if (dtItemTable.Rows[i]["BilAnggota"].ToString() != "")
                            {
                                countanggota = countanggota + int.Parse(dtItemTable.Rows[i]["BilAnggota"].ToString());
                            }
                        }
                    }
                }
                else if (DD_JenLap.SelectedValue == "2")
                {
                    if (dtItemTable != null)
                    {
                        countanggota = dtItemTable.Rows.Count;
                    }
                }

                decimal jumsyer = 0;
                if (dtItemTable != null)
                {
                    for (int i = 0; i < dtItemTable.Rows.Count; i++)
                    {
                        if (dtItemTable.Rows[i]["set_apply_amt"].ToString() != "")
                        {
                            jumsyer = jumsyer + decimal.Parse(dtItemTable.Rows[i]["set_apply_amt"].ToString());
                        }
                    }
                }

                ReportDataSource rds = null;
                RptviwerPenAng.LocalReport.DataSources.Clear();
                if (rcount != "0")
                {
                    if (DD_JenLap.SelectedValue == "1")
                    {
                        rds = new ReportDataSource("dsRingkasan", ds.Tables[0]);
                        RptviwerPenAng.LocalReport.DataSources.Add(rds);
                        rds = new ReportDataSource("dsSenarai", dtdummy);
                        RptviwerPenAng.LocalReport.DataSources.Add(rds);
                    }
                    else if (DD_JenLap.SelectedValue == "2")
                    {
                        rds = new ReportDataSource("dsRingkasan", dtdummy);
                        RptviwerPenAng.LocalReport.DataSources.Add(rds);
                        rds = new ReportDataSource("dsSenarai", ds.Tables[0]);
                        RptviwerPenAng.LocalReport.DataSources.Add(rds);
                    }

                    RptviwerPenAng.LocalReport.ReportPath = "keanggotan/rptPenyelesaianAnggota.rdlc";
                    ReportParameter[] rptParams = new ReportParameter[]{
                                        new ReportParameter("s1",f_date.Text),
                                        new ReportParameter("s2",t_date.Text),
                                        new ReportParameter("s3",strkaw),
                                        new ReportParameter("s4",strwilayah),
                                        new ReportParameter("s5",strcawangan),
                                        new ReportParameter("s6",DD_JenPerm.SelectedItem.Text),
                                        new ReportParameter("s7",DD_JenLap.SelectedValue),
                                        new ReportParameter("JenLap",DD_JenLap.SelectedValue),
                                        new ReportParameter("jumanggota",countanggota.ToString()),
                                        new ReportParameter("jumsyer",jumsyer.ToString())
                                    };

                    RptviwerPenAng.LocalReport.SetParameters(rptParams);
                    RptviwerPenAng.LocalReport.EnableExternalImages = true;
                    RptviwerPenAng.LocalReport.Refresh();

                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string extension;
                    string filename;

                    filename = string.Format("{0}.{1}", "IMS_"+ DD_JenPerm.SelectedValue + "_" + DateTime.Now.ToString("ddMMyyyy"), "pdf");
                    byte[] bytes = RptviwerPenAng.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                    Response.Buffer = true;
                    Response.Clear();
                    Response.ContentType = mimeType;
                    Response.AddHeader("content-disposition", "attachment; filename=" + filename);
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod tidak dijumpai. Sila Pastikan Semua Maklumat Dimasukkan Dengan Betul.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Maklumat.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            ReportErrorMessage.Text = ex.Message + ",,,,"+ ex.StackTrace;
            lblJenLap.Text = ex.Message + ",,,," + ex.StackTrace;
        }
    }
    protected void btnExpExcel_Click(object sender, EventArgs e)
    {
        try
        {
            if (f_date.Text != "" && t_date.Text != "" && DD_kaw.SelectedIndex >= 0 && DD_wilayah.SelectedIndex >= 0 && DD_cawangan.SelectedIndex >= 0)
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                string rcount = string.Empty, rcount1 = string.Empty;
                int count = 0;
                double sum = 0;

                string strkaw = "";
                if (DD_kaw.SelectedIndex == 0)
                {
                    strkaw = "SEMUA";
                }
                else
                {
                    strkaw = DD_kaw.SelectedItem.Text;
                }
                string strwilayah = "";
                if (DD_wilayah.SelectedIndex == 0)
                {
                    strwilayah = "SEMUA";
                }
                else
                {
                    strwilayah = DD_wilayah.SelectedItem.Text;
                }
                string strcawangan = "";
                if (DD_cawangan.SelectedIndex == 0)
                {
                    strcawangan = "SEMUA";
                }
                else
                {
                    strcawangan = DD_cawangan.SelectedItem.Text;
                }

                dt = (DataTable)Session["dtDataLaporan"];
                DataTable thisdt = dt.Copy();
                ds.Tables.Add(thisdt);
                DataTable dtdummy = new DataTable();
                DataTable dtItemTable = new DataTable();
                dtItemTable = dt.Copy();
                List<DataRow> listResult = dt.AsEnumerable().ToList();
                //listResult.Count();
                int countRow = 0;
                countRow = listResult.Count();
                rcount = "1";

                int countanggota = 0;
                if (DD_JenLap.SelectedValue == "1")
                {
                    if (dtItemTable != null)
                    {
                        for (int i = 0; i < dtItemTable.Rows.Count; i++)
                        {
                            if (dtItemTable.Rows[i]["BilAnggota"].ToString() != "")
                            {
                                countanggota = countanggota + int.Parse(dtItemTable.Rows[i]["BilAnggota"].ToString());
                            }
                        }
                    }
                }
                else if (DD_JenLap.SelectedValue == "2")
                {
                    if (dtItemTable != null)
                    {
                        countanggota = dtItemTable.Rows.Count;
                    }
                        
                }

                decimal jumsyer = 0;
                if (dtItemTable != null)
                {
                    for (int i = 0; i < dtItemTable.Rows.Count; i++)
                    {
                        if (dtItemTable.Rows[i]["set_apply_amt"].ToString() != "")
                        {
                            jumsyer = jumsyer + decimal.Parse(dtItemTable.Rows[i]["set_apply_amt"].ToString());
                        }
                    }
                }

                ReportDataSource rds = null;
                RptviwerPenAng.LocalReport.DataSources.Clear();
                if (rcount != "0")
                {
                    if (DD_JenLap.SelectedValue == "1")
                    {
                        rds = new ReportDataSource("dsRingkasan", ds.Tables[0]);
                        RptviwerPenAng.LocalReport.DataSources.Add(rds);
                        rds = new ReportDataSource("dsSenarai", dtdummy);
                        RptviwerPenAng.LocalReport.DataSources.Add(rds);
                    }
                    else if (DD_JenLap.SelectedValue == "2")
                    {
                        rds = new ReportDataSource("dsRingkasan", dtdummy);
                        RptviwerPenAng.LocalReport.DataSources.Add(rds);
                        rds = new ReportDataSource("dsSenarai", ds.Tables[0]);
                        RptviwerPenAng.LocalReport.DataSources.Add(rds);
                    }
                    RptviwerPenAng.LocalReport.ReportPath = "keanggotan/rptPenyelesaianAnggota.rdlc";
                    ReportParameter[] rptParams = new ReportParameter[]{
                                        new ReportParameter("s1",f_date.Text),
                                        new ReportParameter("s2",t_date.Text),
                                        new ReportParameter("s3",strkaw),
                                        new ReportParameter("s4",strwilayah),
                                        new ReportParameter("s5",strcawangan),
                                        new ReportParameter("s6",DD_JenPerm.SelectedItem.Text),
                                        new ReportParameter("s7",DD_JenLap.SelectedValue),
                                        new ReportParameter("JenLap",DD_JenLap.SelectedValue),
                                        new ReportParameter("jumanggota",countanggota.ToString()),
                                        new ReportParameter("jumsyer",jumsyer.ToString())
                                    };


                    RptviwerPenAng.LocalReport.SetParameters(rptParams);
                    RptviwerPenAng.LocalReport.EnableExternalImages = true;
                    RptviwerPenAng.LocalReport.Refresh();

                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string extension;
                    string filename;

                    filename = string.Format("{0}.{1}", "IMS_" + DD_JenPerm.SelectedValue + "_" + DateTime.Now.ToString("ddMMyyyy"), "xls");
                    byte[] bytes = RptviwerPenAng.LocalReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                    Response.Buffer = true;
                    Response.Clear();
                    Response.ContentType = mimeType;
                    Response.AddHeader("content-disposition", "attachment; filename=" + filename);
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod tidak dijumpai. Sila Pastikan Semua Maklumat Dimasukkan Dengan Betul.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Maklumat.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            ReportErrorMessage.Text = ex.Message + ",,,," + ex.StackTrace;
            lblJenLap.Text = ex.Message + ",,,," + ex.StackTrace;
        }
    }
}