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



public partial class PP_jkpa_alk : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string str;
    DBConnection DBCon = new DBConnection();
    SqlCommand com;
    DataTable dt = new DataTable();
    StudentWebService service = new StudentWebService();
    string Status = string.Empty;
    string level, userid;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Button5);
        //scriptManager.RegisterPostBackControl(this.Button7);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                level = Session["level"].ToString();
                userid = Session["New"].ToString();
                Jawatan();
                //Default value
                MP_nama.Attributes.Add("Readonly", "Readonly");
                MP_wilayah.Attributes.Add("Readonly", "Readonly");
                MP_cawangan.Attributes.Add("Readonly", "Readonly");
                MP_tujuan.Attributes.Add("Readonly", "Readonly");
                MP_amaun.Attributes.Add("Readonly", "Readonly");
                MP_tempoh.Attributes.Add("Readonly", "Readonly");
                UJ_tarikh1.Attributes.Add("Readonly", "Readonly");
                UJ_tarikh2.Attributes.Add("Readonly", "Readonly");
                UJ_tarikh3.Attributes.Add("Readonly", "Readonly");
                KJ_tarikh.Attributes.Add("Readonly", "Readonly");
                var samp = Request.Url.Query;
                if (samp != "")
                {
                    Icno.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    srch_rfd();
                    Button4.Visible = false;
                    Button6.Visible = false;
                    //Button2.Visible = false;
                    Button1.Visible = false;
                    //Button5.Visible = true;

                }

            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    void Jawatan()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select Jawatan_Code,Jawatan_desc from ref_jawatan order by Jawatan_desc ASC";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            UJ_jawatan1.DataSource = dt;
            UJ_jawatan1.DataTextField = "Jawatan_desc";
            UJ_jawatan1.DataValueField = "Jawatan_Code";
            UJ_jawatan1.DataBind();
            UJ_jawatan1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            UJ_jawatan2.DataSource = dt;
            UJ_jawatan2.DataTextField = "Jawatan_desc";
            UJ_jawatan2.DataValueField = "Jawatan_Code";
            UJ_jawatan2.DataBind();
            UJ_jawatan2.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            UJ_jawatan3.DataSource = dt;
            UJ_jawatan3.DataTextField = "Jawatan_desc";
            UJ_jawatan3.DataValueField = "Jawatan_Code";
            UJ_jawatan3.DataBind();
            UJ_jawatan3.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void srch_rfd()
    {
        if (Icno.Text != "")
        {
            DataTable ddicno = new DataTable();
            ddicno = DBCon.Ora_Execute_table("select app_applcn_no from jpa_application where app_applcn_no='" + Icno.Text + "' ");
            if (ddicno.Rows.Count == 0)
            {
                reset();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
            else
            {

                string Select_App_query = "select JA.app_new_icno,JA.app_name,RT.tujuan_desc,JA.app_apply_amt,JA.app_apply_dur,RW.Wilayah_Name,ISNULL(RB.branch_desc, '') AS branch_desc from jpa_application as JA Left Join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd left join ref_tujuan as RT ON RT.tujuan_cd=JA.app_loan_purpose_cd Left join ref_branch AS RB ON RB.branch_cd=JA.app_branch_cd where JA.app_applcn_no='" + Icno.Text + "' ";
                con.Open();
                var sqlCommand = new SqlCommand(Select_App_query, con);
                var sqlReader = sqlCommand.ExecuteReader();
                if (sqlReader.Read() == true)
                {
                    //Button7.Visible = true;
                  
                    MP_nama.Text = sqlReader["app_name"].ToString();
                    MP_wilayah.Text = sqlReader["Wilayah_Name"].ToString();
                    MP_cawangan.Text = sqlReader["branch_desc"].ToString();
                    MP_tujuan.Text = sqlReader["tujuan_desc"].ToString();
                    decimal amt = (decimal)sqlReader["app_apply_amt"];
                    MP_amaun.Text = amt.ToString("C").Replace("$", "").Replace("RM", "");
                    MP_tempoh.Text = sqlReader["app_apply_dur"].ToString();
                }
                sqlReader.Close();


                string Select_App_jkpa = "select *,ISNULL(jkp_bil,'') as bil,ISNULL(jkp_jenis,'') as jenis,ISNULL(jkp_mes_dt,'') as mesdt from jpa_jkpa_approval where jkp_applcn_no='" + Icno.Text + "'";
                var sqlCommand1 = new SqlCommand(Select_App_jkpa, con);
                var sqlReader1 = sqlCommand1.ExecuteReader();
                if (sqlReader1.Read() == true)
                {
                    Button5.Visible = true;
                    Button2.Text = "Kemaskini";
                    UJ_ulasan1.Value = sqlReader1["jkp_remark1"].ToString();
                    UJ_nama1.Text = sqlReader1["jkp_name1"].ToString();
                    UJ_jawatan1.SelectedValue = sqlReader1["jkp_post1"].ToString();
                    string j_dt1 = sqlReader1["jkp_dt1"].ToString();
                    UJ_tarikh1.Text = Convert.ToDateTime(j_dt1).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                    UJ_ulasan2.Value = sqlReader1["jkp_remark2"].ToString();
                    UJ_nama2.Text = sqlReader1["jkp_name2"].ToString();
                    UJ_jawatan2.SelectedValue = sqlReader1["jkp_post2"].ToString();
                    string j_dt2 = sqlReader1["jkp_dt2"].ToString();
                    if (Convert.ToDateTime(j_dt2).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) != "01/01/1900")
                    {
                        UJ_tarikh2.Text = Convert.ToDateTime(j_dt2).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        UJ_tarikh2.Text = "";
                    }

                    UJ_ulasan3.Value = (string)sqlReader1["jkp_remark3"];
                    UJ_nama3.Text = (string)sqlReader1["jkp_name3"];
                    UJ_jawatan3.SelectedValue = (string)sqlReader1["jkp_post3"];
                    string j_dt3 = sqlReader1["jkp_dt3"].ToString();
                    if (Convert.ToDateTime(j_dt3).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) != "01/01/1900")
                    {
                        UJ_tarikh3.Text = Convert.ToDateTime(j_dt3).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        UJ_tarikh3.Text ="";
                    }

                    string j_dt4 = sqlReader1["mesdt"].ToString();
                    var mes_date = Convert.ToDateTime(j_dt4).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (mes_date != "01/01/1900")
                    {
                        KJ_tarikh.Text = mes_date;
                    }
                    else
                    {
                        KJ_tarikh.Text = "";
                    }

                    decimal amaun = (decimal)sqlReader1["jkp_approve_amt"];
                    MK_amaun.Text = amaun.ToString("C").Replace("$", "");
                    MK_tempoh.Text = sqlReader1["jkp_approve_dur"].ToString();

                    jkkbil.Text = sqlReader1["bil"].ToString();
                    catatan.Value = sqlReader1["jkp_catatan"].ToString();

                    string resultind = (string)sqlReader1["jenis"];
                    if (resultind == "J")
                    {
                        RadioButton1.Checked = true;
                    }
                    else if (resultind == "A")
                    {
                        RadioButton2.Checked = true;
                    }
                    else if (resultind == "K")
                    {
                        RadioButton3.Checked = true;
                    }
                    else
                    {
                        RadioButton1.Checked = false;
                        RadioButton2.Checked = false;
                        RadioButton3.Checked = false;

                    }

                }
                sqlReader1.Close();

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
            srch_rfd();
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

    protected void cetak_Click(object sender, EventArgs e)
    {
        {
            try
            {

                if (Icno.Text != "")
                {
                    DataTable ddicno2 = new DataTable();
                    ddicno2 = DBCon.Ora_Execute_table("select app_new_icno,app_applcn_no from jpa_application where app_applcn_no='" + Icno.Text + "'");
                    //Path
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    //dt = DBCon.Ora_Execute_table("select JA.app_applcn_no,JA.app_new_icno,JA.app_name,JA.app_apply_amt,JA.app_apply_dur,RW.Wilayah_Name,RB.branch_desc,RT.tujuan_desc,RJ1.Jawatan_desc as desc1,RJ2.Jawatan_desc as desc2,RJ3.Jawatan_desc as desc3,JJA.* from jpa_application as JA Left join jpa_jkkpa_approval as JJA ON JJA.jkk_applcn_no=JA.app_applcn_no left join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd left join ref_branch as RB ON RB.branch_cd=JA.app_branch_cd left join ref_tujuan as RT ON RT.tujuan_cd = JA.app_loan_purpose_cd left join ref_jawatan as RJ1 on RJ1.Jawatan_Code=JJA.jkk_post1 left join ref_jawatan as RJ2 on RJ2.Jawatan_Code=JJA.jkk_post2 left join ref_jawatan as RJ3 on RJ3.Jawatan_Code=JJA.jkk_post3 where JA.app_new_icno='" + Icno.Text + "'");
                    dt = DBCon.Ora_Execute_table("");

                    Rptviwer_kelulusan.Reset();
                    ds.Tables.Add(dt);

                    string v1 = string.Empty, v2 = string.Empty, v3 = string.Empty, v4 = string.Empty, v5 = string.Empty, v6 = string.Empty;
                    string vv1 = string.Empty, vv2 = string.Empty, vv3 = string.Empty, vv4 = string.Empty, vv5 = string.Empty, vv6 = string.Empty, vv7 = string.Empty, vv8 = string.Empty, vv9 = string.Empty, vv10 = string.Empty, vv11 = string.Empty, vv12 = string.Empty, vv13 = string.Empty, vv14 = string.Empty, vv15 = string.Empty, vv16 = string.Empty, vv17 = string.Empty, vv18 = string.Empty, vv19 = string.Empty, vv20 = string.Empty, vv21 = string.Empty, vv22 = string.Empty, vv23 = string.Empty, vv24 = string.Empty, vv25 = string.Empty;

                    DataTable ddicno2_v1 = new DataTable();
                    ddicno2_v1 = DBCon.Ora_Execute_table("select JA.app_new_icno,JA.app_name,RT.tujuan_desc,JA.app_apply_amt,JA.app_apply_dur,RW.Wilayah_Name,ISNULL(RB.branch_desc, '') AS branch_desc from jpa_application as JA Left Join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd left join ref_tujuan as RT ON RT.tujuan_cd=JA.app_loan_purpose_cd Left join ref_branch AS RB ON RB.branch_cd=JA.app_branch_cd where JA.app_applcn_no='" + Icno.Text + "'");

                    if (ddicno2_v1.Rows.Count != 0)
                    {
                        v1 = ddicno2_v1.Rows[0]["app_name"].ToString();
                        v2 = ddicno2_v1.Rows[0]["Wilayah_Name"].ToString();
                        v3 = ddicno2_v1.Rows[0]["branch_desc"].ToString();
                        v4 = ddicno2_v1.Rows[0]["tujuan_desc"].ToString();
                        decimal amt = (decimal)ddicno2_v1.Rows[0]["app_apply_amt"];
                        v5 = amt.ToString("C").Replace("$", "");
                        v6 = ddicno2_v1.Rows[0]["app_apply_dur"].ToString();
                    }

                    DataTable ddicno2_v2 = new DataTable();
                    ddicno2_v2 = DBCon.Ora_Execute_table("select *,ISNULL(jkp_bil,'') as bil,ISNULL(jkp_jenis,'') as jenis,ISNULL(jkp_mes_dt,'') as mesdt from jpa_jkpa_approval where jkp_applcn_no='" + Icno.Text + "'");


                    if (ddicno2_v2.Rows.Count != '0')
                    {
                        vv1 = ddicno2_v2.Rows[0]["jkp_remark1"].ToString();
                        vv2 = ddicno2_v2.Rows[0]["jkp_name1"].ToString();
                        vv3 = UJ_jawatan1.SelectedItem.Text;
                        string j_dt1 = ddicno2_v2.Rows[0]["jkp_dt1"].ToString();
                        vv4 = Convert.ToDateTime(j_dt1).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                        vv5 = ddicno2_v2.Rows[0]["jkp_remark2"].ToString();
                        vv6 = ddicno2_v2.Rows[0]["jkp_name2"].ToString();
                        vv7 = UJ_jawatan2.SelectedItem.Text;
                        string j_dt2 = ddicno2_v2.Rows[0]["jkp_dt2"].ToString();
                        vv8 = Convert.ToDateTime(j_dt2).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                        vv9 = (string)ddicno2_v2.Rows[0]["jkp_remark3"];
                        vv10 = (string)ddicno2_v2.Rows[0]["jkp_name3"];
                        vv11 = UJ_jawatan3.SelectedItem.Text;
                        string j_dt3 = ddicno2_v2.Rows[0]["jkp_dt3"].ToString();
                        vv12 = Convert.ToDateTime(j_dt3).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                        string j_dt4 = ddicno2_v2.Rows[0]["mesdt"].ToString();
                        var mes_date = Convert.ToDateTime(j_dt4).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        if (mes_date != "01/01/1900")
                        {
                            vv13 = mes_date;
                        }
                        else
                        {
                            vv13 = "";
                        }

                        decimal amaun = (decimal)ddicno2_v2.Rows[0]["jkp_approve_amt"];
                        vv14 = amaun.ToString("C").Replace("$", "");
                        vv15 = ddicno2_v2.Rows[0]["jkp_approve_dur"].ToString();

                        vv16 = ddicno2_v2.Rows[0]["bil"].ToString();
                        vv17 = ddicno2_v2.Rows[0]["jkp_catatan"].ToString();

                        string resultind = (string)ddicno2_v2.Rows[0]["jenis"];
                        if (resultind == "J")
                        {
                            vv18 = "JKPA";
                        }
                        else if (resultind == "A")
                        {
                            vv18 = "ALK";
                        }
                        else if (resultind == "K")
                        {
                            vv18 = "JKKPA";
                        }

                        else
                        {
                            vv18 = "";

                        }

                    }

                    Rptviwer_kelulusan.LocalReport.DataSources.Clear();

                    Rptviwer_kelulusan.LocalReport.ReportPath = "Pelaburan_Anggota/pp_jkpa_alk.rdlc";
                    ReportDataSource rds = new ReportDataSource("jkpa_alk", dt);
                    ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("applcnno",ddicno2.Rows[0]["app_applcn_no"].ToString()),
                     new ReportParameter("kpbaru",ddicno2.Rows[0]["app_new_icno"].ToString()),
                     new ReportParameter("v1",v1),
                     new ReportParameter("v2",v2),
                     new ReportParameter("v3",v3),
                     new ReportParameter("v4",v4),
                     new ReportParameter("v5",v5),
                     new ReportParameter("v6",v6),

                     new ReportParameter("vv1",vv1),
                     new ReportParameter("vv2",vv2),
                     new ReportParameter("vv3",vv3),
                     new ReportParameter("vv4",vv4),
                     new ReportParameter("vv5",vv5),
                     new ReportParameter("vv6",vv6),
                     new ReportParameter("vv7",vv7),
                     new ReportParameter("vv8",vv8),
                     new ReportParameter("vv9",vv9),
                     new ReportParameter("vv10",vv10),
                     new ReportParameter("vv11",vv11),
                     new ReportParameter("vv12",vv12),
                     new ReportParameter("vv13",vv13),
                     new ReportParameter("vv14",vv14),
                     new ReportParameter("vv15",vv15),
                     new ReportParameter("vv16",vv16),
                     new ReportParameter("vv17",vv17),
                     new ReportParameter("vv18",vv18),

                     };


                    Rptviwer_kelulusan.LocalReport.SetParameters(rptParams);
                    Rptviwer_kelulusan.LocalReport.DataSources.Add(rds);

                    //Refresh
                    Rptviwer_kelulusan.LocalReport.Refresh();
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

                    byte[] bytes = Rptviwer_kelulusan.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);


                    Response.Buffer = true;

                    Response.Clear();

                    Response.ClearHeaders();

                    Response.ClearContent();

                    Response.ContentType = "application/pdf";


                    Response.AddHeader("content-disposition", "attachment; filename=MAKLUMAT_MESYUARAT_JKPA_ALK_" + ddicno2.Rows[0]["app_applcn_no"].ToString() + "." + extension);

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

    protected void btnsmmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (UJ_ulasan1.Value != "" && UJ_nama1.Text != "" && UJ_jawatan1.SelectedValue != "" && UJ_tarikh1.Text != "")
            {
                if (UJ_ulasan2.Value != "" && UJ_nama2.Text != "" && UJ_jawatan2.SelectedValue != "" && UJ_tarikh2.Text != "")
                {
                    if (jkkbil.Text != "" && KJ_tarikh.Text != "")
                    {
                        DataTable ddicno1 = new DataTable();
                        ddicno1 = DBCon.Ora_Execute_table("select jkp_applcn_no from jpa_jkpa_approval where jkp_applcn_no='" + Icno.Text + "'");
                        string datedari1 = string.Empty, datedari2 = string.Empty, datedari3 = string.Empty, datedari3_1 = string.Empty;
                        String tarikh1 = string.Empty, tarikh2 = string.Empty, tarikh3 = string.Empty, tarikh3_1 = string.Empty;
                        if (UJ_tarikh1.Text != "")
                        {
                            datedari1 = UJ_tarikh1.Text;
                            DateTime dt1 = DateTime.ParseExact(datedari1, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                            tarikh1 = dt1.ToString("yyyy-mm-dd");
                        }
                        if (UJ_tarikh2.Text != "")
                        {
                            datedari2 = UJ_tarikh2.Text;
                            DateTime dt2 = DateTime.ParseExact(datedari2, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                            tarikh2 = dt2.ToString("yyyy-mm-dd");
                        }

                        if (UJ_tarikh3.Text != "")
                        {
                            datedari3 = UJ_tarikh3.Text;
                            DateTime dt3 = DateTime.ParseExact(datedari3, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                            tarikh3 = dt3.ToString("yyyy-mm-dd");
                        }
                        if (KJ_tarikh.Text != "")
                        {
                            datedari3_1 = KJ_tarikh.Text;
                            DateTime dt3_1 = DateTime.ParseExact(datedari3_1, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                            tarikh3_1 = dt3_1.ToString("yyyy-mm-dd");
                        }

                        string strResultInd = string.Empty;
                        if (RadioButton1.Checked == true)
                        {
                            strResultInd = "J";
                        }
                        else if (RadioButton2.Checked == true)
                        {
                            strResultInd = "A";
                        }
                        else if (RadioButton3.Checked == true)
                        {
                            strResultInd = "K";
                        }

                        else
                        {
                            strResultInd = "";
                        }

                        if (ddicno1.Rows.Count == 0)
                        {

                            DataTable Check_status = new DataTable();
                            string mpval1 = UJ_nama1.Text;
                            string mval1 = mpval1.Replace("'", "''");
                            string mpval2 = UJ_nama2.Text;
                            string mval2 = mpval2.Replace("'", "''");
                            string mpval3 = UJ_nama3.Text;
                            string mval3 = mpval3.Replace("'", "''");
                            string Inssql = "insert into jpa_jkpa_approval(jkp_applcn_no,jkp_remark1,jkp_name1,jkp_post1,jkp_dt1,jkp_remark2,jkp_name2,jkp_post2,jkp_dt2,jkp_remark3,jkp_name3,jkp_post3,jkp_dt3,jkp_approve_amt,jkp_approve_dur,jkp_crt_id,jkp_crt_dt,jkp_upd_id,jkp_upd_dt,Created_date,jkp_bil,jkp_jenis,jkp_mes_dt,jkp_catatan) values ('" + Icno.Text + "','" + UJ_ulasan1.Value.Replace("'", "''") + "','" + mval1 + "','" + UJ_jawatan1.SelectedValue + "','" + tarikh1 + "','" + UJ_ulasan2.Value.Replace("'", "''") + "','" + mval2 + "','" + UJ_jawatan2.SelectedValue + "','" + tarikh2 + "','" + UJ_ulasan3.Value.Replace("'", "''") + "','" + mval3 + "','" + UJ_jawatan3.SelectedValue + "','" + tarikh3 + "','" + MK_amaun.Text + "','" + MK_tempoh.Text + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','','','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + jkkbil.Text + "','" + strResultInd + "','" + tarikh3_1 + "','" + catatan.Value.Replace("'", "''") + "')";
                            Status = DBCon.Ora_Execute_CommamdText(Inssql);

                            //DBCon.Execute_CommamdText("insert into jpa_jkpa_approval(jkp_applcn_no,jkp_remark1,jkp_name1,jkp_post1,jkp_dt1,jkp_remark2,jkp_name2,jkp_post2,jkp_dt2,jkp_remark3,jkp_name3,jkp_post3,jkp_dt3,jkp_approve_amt,jkp_approve_dur,jkp_crt_id,jkp_crt_dt,jkp_upd_id,jkp_upd_dt,Created_date,jkp_bil,jkp_jenis,jkp_mes_dt,jkp_catatan) values ('" + Icno.Text + "','" + UJ_ulasan1.Value.Replace("'", "''") + "','" + mval1.Replace("'", "''") + "','" + UJ_jawatan1.SelectedValue + "','" + tarikh1 + "','" + UJ_ulasan2.Value.Replace("'", "''") + "','" + mval2.Replace("'", "''") + "','" + UJ_jawatan2.SelectedValue + "','" + tarikh2 + "','" + UJ_ulasan3.Value.Replace("'", "''") + "','" + mval3.Replace("'", "''") + "','" + UJ_jawatan3.SelectedValue + "','" + tarikh3 + "','" + MK_amaun.Text + "','" + MK_tempoh.Text + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','','','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + jkkbil.Text + "','" + strResultInd + "','" + tarikh3_1 + "','" + catatan.Value.Replace("'", "''") + "')");
                            DBCon.Execute_CommamdText("UPDATE jpa_application SET app_sts_cd='N' WHERE app_applcn_no='" + Icno.Text + "'");

                            Session["validate_success"] = "SUCCESS";
                            Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                            Response.Redirect("../Pelaburan_Anggota/PP_jkpa_alk_view.aspx");

                        }
                        else
                        {
                            DataTable Check_status = new DataTable();

                            string mpval1 = UJ_nama1.Text;
                            string mval1 = mpval1.Replace("'", "''");
                            string mpval2 = UJ_nama2.Text;
                            string mval2 = mpval2.Replace("'", "''");
                            string mpval3 = UJ_nama3.Text;
                            string mval3 = mpval3.Replace("'", "''");

                            string Inssql = "Update jpa_jkpa_approval set jkp_remark1='" + UJ_ulasan1.Value.Replace("'", "''") + "',jkp_name1='" + mval1 + "',jkp_post1='" + UJ_jawatan1.SelectedValue + "',jkp_dt1='" + tarikh1 + "',jkp_remark2='" + UJ_ulasan2.Value.Replace("'", "''") + "',jkp_name2='" + mval2 + "',jkp_post2='" + UJ_jawatan2.SelectedValue + "',jkp_dt2='" + tarikh2 + "',jkp_remark3='" + UJ_ulasan3.Value.Replace("'", "''") + "',jkp_name3='" + mval3 + "',jkp_post3='" + UJ_jawatan3.SelectedValue + "',jkp_dt3='" + tarikh3 + "',jkp_approve_amt='" + MK_amaun.Text + "',jkp_approve_dur='" + MK_tempoh.Text + "',jkp_crt_id='" + Session["New"].ToString() + "',jkp_crt_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',Created_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',jkp_bil='" + jkkbil.Text + "',jkp_jenis='" + strResultInd + "',jkp_mes_dt='" + tarikh3_1 + "',jkp_catatan='" + catatan.Value.Replace("'", "''") + "' WHERE jkp_applcn_no='" + Icno.Text + "'";
                            Status = DBCon.Ora_Execute_CommamdText(Inssql);

                            //DBCon.Execute_CommamdText("Update jpa_jkpa_approval set jkp_remark1='" + UJ_ulasan1.Value.Replace("'", "''") + "',jkp_name1='" + mval1.Replace("'", "''") + "',jkp_post1='" + UJ_jawatan1.SelectedValue + "',jkp_dt1='" + tarikh1 + "',jkp_remark2='" + UJ_ulasan2.Value.Replace("'", "''") + "',jkp_name2='" + mval2.Replace("'", "''") + "',jkp_post2='" + UJ_jawatan2.SelectedValue + "',jkp_dt2='" + tarikh2 + "',jkp_remark3='" + UJ_ulasan3.Value.Replace("'", "''") + "',jkp_name3='" + mval3.Replace("'", "''") + "',jkp_post3='" + UJ_jawatan3.SelectedValue + "',jkp_dt3='" + tarikh3 + "',jkp_approve_amt='" + MK_amaun.Text + "',jkp_approve_dur='" + MK_tempoh.Text + "',jkp_crt_id='" + Session["New"].ToString() + "',jkp_crt_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',Created_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',jkp_bil='" + jkkbil.Text + "',jkp_jenis='" + strResultInd + "',jkp_mes_dt='" + tarikh3_1 + "',jkp_catatan='" + catatan.Value.Replace("'", "''") + "' WHERE jkp_applcn_no='" + Icno.Text + "'");
                            DBCon.Execute_CommamdText("UPDATE jpa_application SET app_sts_cd='Y' WHERE app_applcn_no='" + Icno.Text + "'");
                            Session["validate_success"] = "SUCCESS";
                            Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                            Response.Redirect("../Pelaburan_Anggota/PP_jkpa_alk_view.aspx");
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Maklumat Kelulusan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Ulasan JKKPA / JKPA / ALK 2.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Ulasan JKKPA / JKPA / ALK 1.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

            }
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }

    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton1.Checked == true)
        {
            RadioButton2.Checked = false;
            RadioButton3.Checked = false;

        }
    }
    protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton2.Checked == true)
        {
            RadioButton1.Checked = false;
            RadioButton3.Checked = false;

        }
    }

    protected void RadioButton3_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton3.Checked == true)
        {
            RadioButton1.Checked = false;
            RadioButton2.Checked = false;

        }
    }

    protected void btnrst_click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void reset()
    {

        MP_nama.Text = "";
        MP_wilayah.Text = "";
        MP_cawangan.Text = "";
        MP_tujuan.Text = "";
        MP_amaun.Text = "";
        MP_tempoh.Text = "";
    }

 
    //protected void click_template(object sender, EventArgs e)
    //{
    //    {
    //        try
    //        {

    //            if (Icno.Text != "")
    //            {
    //                string v1 = string.Empty, v2 = string.Empty, v3 = string.Empty, v4 = string.Empty, v5 = string.Empty, v6 = string.Empty;
    //                DataTable ddicno2 = new DataTable();
    //                ddicno2 = DBCon.Ora_Execute_table("select app_new_icno,app_applcn_no from jpa_application where app_applcn_no='" + Icno.Text + "'");
    //                //Path
    //                DataSet ds = new DataSet();
    //                DataTable dt = new DataTable();
    //                //dt = DBCon.Ora_Execute_table("select JA.app_applcn_no,JA.app_new_icno,JA.app_name,JA.app_apply_amt,JA.app_apply_dur,RW.Wilayah_Name,RB.branch_desc,RT.tujuan_desc,RJ1.Jawatan_desc as desc1,RJ2.Jawatan_desc as desc2,RJ3.Jawatan_desc as desc3,JJA.* from jpa_application as JA Left join jpa_jkkpa_approval as JJA ON JJA.jkk_applcn_no=JA.app_applcn_no left join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd left join ref_branch as RB ON RB.branch_cd=JA.app_branch_cd left join ref_tujuan as RT ON RT.tujuan_cd = JA.app_loan_purpose_cd left join ref_jawatan as RJ1 on RJ1.Jawatan_Code=JJA.jkk_post1 left join ref_jawatan as RJ2 on RJ2.Jawatan_Code=JJA.jkk_post2 left join ref_jawatan as RJ3 on RJ3.Jawatan_Code=JJA.jkk_post3 where JA.app_new_icno='" + Icno.Text + "'");
    //                dt = DBCon.Ora_Execute_table("select JA.app_applcn_no,JA.app_new_icno,JA.app_name,JA.app_apply_amt,JA.app_apply_dur,RW.Wilayah_Name,RB.branch_desc,RT.tujuan_desc,RJ1.Jawatan_desc as desc1,RJ2.Jawatan_desc as desc2,RJ3.Jawatan_desc as desc3,JJA.*,case when jja.jkk_result_ind = 'L' then 'LULUS' when JJA.jkk_result_ind = 'B' then 'LULUS BERSYARAT' else 'TOLAK' end as result from jpa_application as JA Left join jpa_jkkpa_approval as JJA ON JJA.jkk_applcn_no=JA.app_applcn_no left join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd left join ref_branch as RB ON RB.branch_cd=JA.app_branch_cd left join ref_tujuan as RT ON RT.tujuan_cd = JA.app_loan_purpose_cd left join ref_jawatan as RJ1 on RJ1.Jawatan_Code=JJA.jkk_post1 left join ref_jawatan as RJ2 on RJ2.Jawatan_Code=JJA.jkk_post2 left join ref_jawatan as RJ3 on RJ3.Jawatan_Code=JJA.jkk_post3 where JA.app_new_icno='" + Icno.Text + "'");

    //                DataTable ddicno2_v1 = new DataTable();
    //                ddicno2_v1 = DBCon.Ora_Execute_table("select JA.app_new_icno,JA.app_name,RT.tujuan_desc,JA.app_apply_amt,JA.app_apply_dur,RW.Wilayah_Name,ISNULL(RB.branch_desc, '') AS branch_desc from jpa_application as JA Left Join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd left join ref_tujuan as RT ON RT.tujuan_cd=JA.app_loan_purpose_cd Left join ref_branch AS RB ON RB.branch_cd=JA.app_branch_cd where JA.app_applcn_no='" + Icno.Text + "'");

    //                if (ddicno2_v1.Rows.Count != 0)
    //                {
    //                    v1 = ddicno2_v1.Rows[0]["app_name"].ToString();
    //                    v2 = ddicno2_v1.Rows[0]["Wilayah_Name"].ToString();
    //                    v3 = ddicno2_v1.Rows[0]["branch_desc"].ToString();
    //                    v4 = ddicno2_v1.Rows[0]["tujuan_desc"].ToString();
    //                    decimal amt = (decimal)ddicno2_v1.Rows[0]["app_apply_amt"];
    //                    v5 = amt.ToString("C").Replace("$", "");
    //                    v6 = ddicno2_v1.Rows[0]["app_apply_dur"].ToString();
    //                }

    //                Rptviwer_kelulusan.Reset();
    //                ds.Tables.Add(dt);

    //                Rptviwer_kelulusan.LocalReport.DataSources.Clear();

    //                Rptviwer_kelulusan.LocalReport.ReportPath = "Pelaburan_Anggota/jkpa_templat.rdlc";
    //                ReportDataSource rds = new ReportDataSource("r_kel", dt);
    //                ReportParameter[] rptParams = new ReportParameter[]{
    //                  new ReportParameter("applcnno",ddicno2.Rows[0]["app_applcn_no"].ToString()),
    //                 new ReportParameter("kpbaru",ddicno2.Rows[0]["app_new_icno"].ToString()),
    //                 new ReportParameter("v1",v1),
    //                 new ReportParameter("v2",v2),
    //                 new ReportParameter("v3",v3),
    //                 new ReportParameter("v4",v4),
    //                 new ReportParameter("v5",v5),
    //                 new ReportParameter("v6",v6),

    //                 };


    //                Rptviwer_kelulusan.LocalReport.SetParameters(rptParams);
    //                Rptviwer_kelulusan.LocalReport.DataSources.Add(rds);

    //                //Refresh
    //                Rptviwer_kelulusan.LocalReport.Refresh();
    //                Warning[] warnings;

    //                string[] streamids;

    //                string mimeType;

    //                string encoding;

    //                string extension;

    //                string devinfo = "<DeviceInfo><ColorDepth>32</ColorDepth><DpiX>350</DpiX><DpiY>350</DpiY><OutputFormat>PDF</OutputFormat>" +
    //                       "  <PageWidth>12.20in</PageWidth>" +
    //                        "  <PageHeight>8.27in</PageHeight>" +
    //                        "  <MarginTop>0.1in</MarginTop>" +
    //                        "  <MarginLeft>0.5in</MarginLeft>" +
    //                         "  <MarginRight>0in</MarginRight>" +
    //                         "  <MarginBottom>0in</MarginBottom>" +
    //                       "</DeviceInfo>";

    //                byte[] bytes = Rptviwer_kelulusan.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);


    //                Response.Buffer = true;

    //                Response.Clear();

    //                Response.ClearHeaders();

    //                Response.ClearContent();

    //                Response.ContentType = "application/pdf";


    //                Response.AddHeader("content-disposition", "attachment; filename=MAKLUMAT_MESYUARAT_JKPA_ALK_TEMPLAT_" + ddicno2.Rows[0]["app_applcn_no"].ToString() + "." + extension);

    //                Response.BinaryWrite(bytes);

    //                //Response.Write("<script>");
    //                //Response.Write("window.open('', '_newtab');");
    //                //Response.Write("</script>");
    //                Response.Flush();

    //                Response.End();
    //            }
    //            else
    //            {
    //                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Medan Input Adalah Mandatori')", true);
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }
    //}

    protected void clk_bak(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Pelaburan_Anggota/PP_jkpa_alk_view.aspx");
    }
}