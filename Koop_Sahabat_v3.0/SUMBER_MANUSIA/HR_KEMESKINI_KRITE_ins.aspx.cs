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
using System.Data.OleDb;
using System.IO;
using System.Net;
using System.Threading;

public partial class HR_KEMESKINI_KRITE_ins : System.Web.UI.Page
{

    DBConnection dbcon = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DataTable dt = new DataTable();
    StudentWebService service = new StudentWebService();
    string useid = string.Empty;
    string Status = string.Empty, Status1 = string.Empty;
    string oid = string.Empty;
    string sdd = string.Empty;
    string add = string.Empty;
    string val1 = string.Empty, val2 = string.Empty, val3 = string.Empty, val4 = string.Empty, val5 = string.Empty;
    string v1 = string.Empty, v2 = string.Empty, v3 = string.Empty, v4 = string.Empty, v5 = string.Empty;
    string CommandArgument1 = string.Empty, CommandArgument2 = string.Empty, CommandArgument3 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        string script = " $(function () {$('.select2').select2();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Button2);
        if (!IsPostBack)
        {

            if (Session["New"] != null)
            {
                useid = Session["New"].ToString();
                //sectionBind();
                bind_Jaw();
                UnitBind();
                var samp = Request.Url.Query;
                if (samp != "")
                {
                    val5 = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    string[] k = val5.ToString().Split(',');
                    v1 = k[0].ToString();
                    v2 = k[1].ToString();
                    v3 = k[2].ToString();
                    v4 = k[3].ToString();
                    v5 = k[4].ToString();
                    lbl1_id2.Text = k[4].ToString();
                    lbl1_id.Text = "1";
                    view_details();
                }
                else
                {
                    grid_pkp();
                    lbl1_id.Text = "0";
                }

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
        ste_set = dbcon.Ora_Execute_table("select * from site_settings where ID IN ('1')");

        DataTable gt_lng = new DataTable();
        gt_lng = dbcon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('1454','448','1456','536','1408','190','795','1459','61','15','77','35','36','133')  order by ID ASC");

        CultureInfo culinfo = Thread.CurrentThread.CurrentUICulture;
        TextInfo txtinfo = culinfo.TextInfo;

        h1_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
        bb1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
        bb2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());

        h3_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());

        lbl1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
        lbl2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
        lbl3_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
        lbl4_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());

        h3_tag2.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
                
        btn_jenis_simp.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower()); //1,3
        Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
        Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
        Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
        Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }

    void view_details()
    {

        DataTable ddokdicno = new DataTable();
        ddokdicno = dbcon.Ora_Execute_table("select distinct format(cap_start_dt,'dd/MM/yyyy') as cap_start_dt, format(cap_end_dt,'dd/MM/yyyy') as cap_end_dt,cap_post_cat_cd,cap_unit_cd,cap_seq_no from hr_cmn_appraisal where cap_start_dt='" + v1 + "' and cap_end_dt = '" + v2 + "' and cap_post_cat_cd='" + v3 + "' and cap_unit_cd='" + v4 + "' and cap_seq_no ='" + v5 + "' ");
        if(ddokdicno.Rows.Count != 0)
        {
            shw_grid1.Visible = false;
            shw_grid2.Visible = true;
            btn_jenis_simp.Text = "Kemaskini";
            Button3.Visible = true;
            Button1.Visible = false;
            TextBox1.Text = ddokdicno.Rows[0]["cap_start_dt"].ToString();
            TextBox3.Text = ddokdicno.Rows[0]["cap_end_dt"].ToString();
            ddkat_jaw.SelectedValue = ddokdicno.Rows[0]["cap_post_cat_cd"].ToString();
            dd_unit.SelectedValue = ddokdicno.Rows[0]["cap_unit_cd"].ToString();
            grid_pkp2();
        }
    }


    protected void gvSelected_PageIndexChanging_jmk2(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        grid_pkp2();

    }

   
        void grid_pkp2()
    {
        string stdt = string.Empty, eddt = string.Empty;
        shw_grid1.Visible = false;
        shw_grid2.Visible = true;
        if (TextBox1.Text != "")
        {

            DateTime dt_1 = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            stdt = dt_1.ToString("yyyy-MM-dd");
        }
        if (TextBox3.Text != "")
        {
            DateTime dt_2 = DateTime.ParseExact(TextBox3.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            eddt = dt_2.ToString("yyyy-MM-dd");
        }
        SqlCommand cmd2 = new SqlCommand("select FORMAT(a.cap_start_dt,'yyyy-MM-dd') as dt1,FORMAT(a.cap_end_dt,'yyyy-MM-dd') as dt2,a.cap_post_cat_cd,a.cap_unit_cd,a.cap_section_cd,a.cap_subject_cd,a.cap_seq_no,cas.cse_section_desc,cs.csb_subject_desc,a.cap_weightage,c1.hr_jaw_desc,c2.hr_unit_desc from (select * from hr_cmn_appraisal as ca where ca.cap_start_dt>=DATEADD(day, DATEDIFF(day, 0, '" + stdt + "'), 0) and ca.cap_start_dt<=DATEADD(day, DATEDIFF(day, 0, '" + eddt + "'), +0) and ca.cap_post_cat_cd ='" + ddkat_jaw.SelectedValue + "' and ca.cap_unit_cd ='" + dd_unit.SelectedValue + "' and ca.cap_seq_no='"+ lbl1_id2.Text + "') a left join hr_cmn_appr_section as cas on cas.cse_section_cd=a.cap_section_cd left join hr_cmn_subject as cs on cs.csb_subject_cd=a.cap_subject_cd left join Ref_hr_Jawatan c1 on c1.hr_jaw_Code=a.cap_post_cat_cd left join Ref_hr_unit c2 on c2.hr_unit_Code=a.cap_unit_cd", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            GridView2.DataSource = ds2;
            GridView2.DataBind();
            int columncount = GridView2.Rows[0].Cells.Count;
            GridView2.Rows[0].Cells.Clear();
            GridView2.Rows[0].Cells.Add(new TableCell());
            GridView2.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView2.Rows[0].Cells[0].Text = "<strong>Maklumat Carian Tidak Dijumpai</strong>";
            if (TextBox1.Text != "" && TextBox3.Text != "" || ddkat_jaw.SelectedValue != "" || dd_unit.SelectedValue != "")
            {
                grid_pkp2();                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {
            GridView2.DataSource = ds2;
            GridView2.DataBind();
            Button2.Attributes.Remove("style");
        }
    }
    void bind_Jaw()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select hr_jaw_code,UPPER(hr_jaw_desc) as hr_jaw_desc from ref_hr_jawatan ORDER BY hr_jaw_code";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddkat_jaw.DataSource = dt;
            ddkat_jaw.DataTextField = "hr_jaw_desc";
            ddkat_jaw.DataValueField = "hr_jaw_code";
            ddkat_jaw.DataBind();
            ddkat_jaw.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    void UnitBind()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select hr_unit_Code,upper(hr_unit_desc) as hr_unit_desc from Ref_hr_unit where Status='A' order by hr_unit_Code";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_unit.DataSource = dt;
            dd_unit.DataTextField = "hr_unit_desc";
            dd_unit.DataValueField = "hr_unit_Code";
            dd_unit.DataBind();
            dd_unit.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

  
    void grid_pkp()
    {

        //SqlCommand cmd2 = new SqlCommand("select ci.cit_seq_no,cas.cse_section_desc,cs.csb_subject_desc,ci.cit_desc,ci.cit_item_weightage from hr_cmn_item as ci left join hr_cmn_appr_section as cas on cas.cse_section_cd=ci.cit_section_cd left join hr_cmn_subject as cs on cs.csb_subject_cd=ci.cit_subject_cd and cs.csb_section_cd=ci.cit_section_cd where cit_section_cd='" + dd_bahag.SelectedValue + "' and cit_subject_cd='" + dd_subject.SelectedValue + "'", con);
        SqlCommand cmd2 = new SqlCommand("select cas.cse_section_desc,csb_section_cd,csb_subject_cd,csb_subject_desc,csb_sub_weightage from hr_cmn_subject left join hr_cmn_appr_section as cas on cas.cse_section_cd=csb_section_cd", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            GridView1.DataSource = ds2;
            GridView1.DataBind();
            int columncount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView1.Rows[0].Cells[0].Text = "<strong>Maklumat Carian Tidak Dijumpai</strong>";
        }
        else
        {
            GridView1.DataSource = ds2;
            GridView1.DataBind();
        }
    }

    protected void gvSelected_PageIndexChanging_jmk(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        grid_pkp();

    }

    protected void btn_jenis_simp_Click(object sender, EventArgs e)
    {
        try
        {
            if (lbl1_id.Text == "0")
            {
                if (TextBox1.Text != "" && TextBox3.Text != "" && ddkat_jaw.SelectedValue != "" && dd_unit.SelectedValue != "")
                {

                    string stdt = string.Empty, eddt = string.Empty;
                    DateTime dt_1 = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    stdt = dt_1.ToString("yyyy-MM-dd");

                    DateTime dt_2 = DateTime.ParseExact(TextBox3.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    eddt = dt_2.ToString("yyyy-MM-dd");

                    string rcount = string.Empty;
                    int count = 0;
                    int num = 0;
                    foreach (GridViewRow gvrow in GridView1.Rows)
                    {
                        var checkbox = gvrow.FindControl("s_pilih") as System.Web.UI.WebControls.CheckBox;
                        if (checkbox.Checked)
                        {
                            count++;

                            rcount = count.ToString();
                            var v4 = gvrow.FindControl("ss_ppk") as System.Web.UI.WebControls.Label;
                            num += Convert.ToInt32(v4.Text);

                        }

                    }
                    if (num <= 100)
                    {
                        if (rcount != "")
                        {
                            foreach (GridViewRow gvrow in GridView1.Rows)
                            {
                                var checkbox = gvrow.FindControl("s_pilih") as System.Web.UI.WebControls.CheckBox;
                                if (checkbox.Checked == true)
                                {
                                    var v1 = gvrow.FindControl("ss_bha_cd") as System.Web.UI.WebControls.Label;
                                    var v2 = gvrow.FindControl("ss_mp_cd") as System.Web.UI.WebControls.Label;
                                    var v4 = gvrow.FindControl("ss_ppk") as System.Web.UI.WebControls.Label;



                                    dt = dbcon.Ora_Execute_table("select * from hr_cmn_appraisal where cap_start_dt = '" + stdt + "' and cap_end_dt ='" + eddt + "' and cap_post_cat_cd ='" + ddkat_jaw.SelectedValue + "' and cap_unit_cd ='" + dd_unit.SelectedValue + "' and cap_section_cd ='" + v1.Text + "' and cap_subject_cd ='" + v2.Text + "'");
                                    int updseq;
                                    if (dt.Rows.Count > 0)
                                    {

                                        updseq = Convert.ToInt32(dt.Rows.Count) + 1;
                                    }
                                    else
                                    {
                                        updseq = 1;
                                    }

                                    useid = Session["New"].ToString();
                                    string Inssql1 = "insert into hr_cmn_item (cit_section_cd,cit_subject_cd,cit_desc,cit_item_weightage,cit_crt_id,cit_crt_dt)values ('" + v1.Text + "','" + v2.Text + "','','" + v4.Text + "','" + useid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                                    Status1 = dbcon.Ora_Execute_CommamdText(Inssql1);
                                    if (Status1 == "SUCCESS")
                                    {
                                        string Inssql = "insert into hr_cmn_appraisal(cap_start_dt,cap_end_dt,cap_post_cat_cd,cap_unit_cd,cap_section_cd,cap_subject_cd,cap_seq_no,cap_weightage,cap_crt_id,cap_crt_dt)values('" + stdt + "','" + eddt + "','" + ddkat_jaw.SelectedItem.Value + "','" + dd_unit.SelectedItem.Value + "','" + v1.Text + "','" + v2.Text + "','" + updseq + "','" + v4.Text + "','" + useid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                                        Status = dbcon.Ora_Execute_CommamdText(Inssql);

                                    }
                                }
                            }
                            if (Status != "SUCCESS")
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('ISSUE.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                            }
                            else
                            {
                                Session["validate_success"] = "SUCCESS";
                                Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                                Response.Redirect("../SUMBER_MANUSIA/HR_KEMESKINI_KRITE_ins_view.aspx");
                            }

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Minimum Satu Kotak',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Only Allowed 100%',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Cerian',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                string rcount = string.Empty;
                int count = 0;
                int num = 0;
                foreach (GridViewRow gvrow in GridView2.Rows)
                {
                    count++;
                    rcount = count.ToString();
                    var v4 = gvrow.FindControl("ss_pemberat") as System.Web.UI.WebControls.TextBox;
                    num += Convert.ToInt32(v4.Text);
                }
                if (num <= 100)
                {
                    if (rcount != "0")
                    {
                        foreach (GridViewRow gvrow in GridView2.Rows)
                        {
                            var sqno = gvrow.FindControl("ss_sqno") as System.Web.UI.WebControls.Label;
                            var l1 = gvrow.FindControl("Label1") as System.Web.UI.WebControls.Label;
                            var l2 = gvrow.FindControl("Label2") as System.Web.UI.WebControls.Label;
                            var l3 = gvrow.FindControl("Label3") as System.Web.UI.WebControls.Label;
                            var l4 = gvrow.FindControl("Label4") as System.Web.UI.WebControls.Label;
                            var l5 = gvrow.FindControl("Label5") as System.Web.UI.WebControls.Label;
                            var l6 = gvrow.FindControl("Label6") as System.Web.UI.WebControls.Label;
                            var l7 = gvrow.FindControl("ss_pemberat") as System.Web.UI.WebControls.TextBox;
                            string Inssql = "UPDATE hr_cmn_appraisal set cap_weightage='" + l7.Text + "',cap_upd_id='" + Session["New"].ToString() + "',cap_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "' where cap_start_dt='" + l1.Text + "' and cap_end_dt='" + l2.Text + "' and cap_post_cat_cd='" + l3.Text + "' and cap_unit_cd='" + l4.Text + "' and cap_section_cd='" + l5.Text + "' and cap_subject_cd='" + l6.Text + "' and cap_seq_no='" + sqno.Text + "'";
                            Status = dbcon.Ora_Execute_CommamdText(Inssql);
                        }
                        if (Status == "SUCCESS")
                        {
                            Session["validate_success"] = "SUCCESS";
                            Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                            Response.Redirect("../SUMBER_MANUSIA/HR_KEMESKINI_KRITE_ins_view.aspx");
                        }
                        else
                        {
                            grid_pkp2();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Issue.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                        }
                    }
                    else
                    {
                        grid_pkp2();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Semak Isu',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

                }
            }

        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Semak Isu',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }
   
    protected void ctk_values(object sender, EventArgs e)
    {

        string stdt = string.Empty, eddt = string.Empty;
        if (TextBox1.Text != "")
        {
            DateTime dt_1 = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            stdt = dt_1.ToString("yyyy-MM-dd");
        }

        if (TextBox3.Text != "")
        {
            DateTime dt_2 = DateTime.ParseExact(TextBox3.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            eddt = dt_2.ToString("yyyy-MM-dd");
        }

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        dt = dbcon.Ora_Execute_table("select FORMAT(a.cap_start_dt,'dd/MM/yyyy') as dt1,FORMAT(a.cap_end_dt,'dd/MM/yyyy') as dt2,a.cap_post_cat_cd,a.cap_unit_cd,a.cap_section_cd,a.cap_subject_cd,a.cap_seq_no,cas.cse_section_desc,cs.csb_subject_desc,a.cap_weightage,c1.hr_jaw_desc,c2.hr_unit_desc from (select * from hr_cmn_appraisal as ca where ca.cap_start_dt>=DATEADD(day, DATEDIFF(day, 0, '" + stdt + "'), 0) and ca.cap_start_dt<=DATEADD(day, DATEDIFF(day, 0, '" + eddt + "'), +0) and ca.cap_post_cat_cd ='" + ddkat_jaw.SelectedValue + "' and ca.cap_unit_cd ='" + dd_unit.SelectedValue + "') a left join hr_cmn_appr_section as cas on cas.cse_section_cd=a.cap_section_cd left join hr_cmn_subject as cs on cs.csb_subject_cd=a.cap_subject_cd left join Ref_hr_Jawatan c1 on c1.hr_jaw_Code=a.cap_post_cat_cd left join Ref_hr_unit c2 on c2.hr_unit_Code=a.cap_unit_cd");
        RptviwerStudent.Reset();
        ds.Tables.Add(dt);

        List<DataRow> listResult = dt.AsEnumerable().ToList();
        listResult.Count();
        int countRow = 0;
        countRow = listResult.Count();

        RptviwerStudent.LocalReport.DataSources.Clear();
        if (countRow != 0)
        {
            RptviwerStudent.LocalReport.ReportPath = "SUMBER_MANUSIA/HR_pen_pertasi.rdlc";
            ReportDataSource rds = new ReportDataSource("HR_pen_pertasi", dt);
            RptviwerStudent.LocalReport.DataSources.Add(rds);
            RptviwerStudent.LocalReport.Refresh();


            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            string filename;


            filename = string.Format("{0}.{1}", "JANA_DOKUMEN_PENILAIAN_PRESTASI_" + DateTime.Now.ToString("ddMMyyyy") + "", "pdf");
            byte[] bytes = RptviwerStudent.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename=" + filename);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();

        }
        else if (countRow == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod tidak dijumpai. Sila Pastikan Semua Maklumat Dimasukkan Dengan Betul',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }

    }

    protected void btn_hapus_Click(object sender, EventArgs e)
    {
        try
        {

            string rcount = string.Empty;
            int count = 0;
            foreach (GridViewRow gvrow in GridView2.Rows)
            {
                var checkbox = gvrow.FindControl("s_hapus") as System.Web.UI.WebControls.CheckBox;
                if (checkbox.Checked)
                {
                    count++;
                }
                rcount = count.ToString();
            }

            if (rcount != "0")
            {
                foreach (GridViewRow gvrow in GridView2.Rows)
                {
                    var checkbox = gvrow.FindControl("s_hapus") as System.Web.UI.WebControls.CheckBox;
                    if (checkbox.Checked == true)
                    {
                        var sqno = gvrow.FindControl("ss_sqno") as System.Web.UI.WebControls.Label;
                        var l1 = gvrow.FindControl("Label1") as System.Web.UI.WebControls.Label;
                        var l2 = gvrow.FindControl("Label2") as System.Web.UI.WebControls.Label;
                        var l3 = gvrow.FindControl("Label3") as System.Web.UI.WebControls.Label;
                        var l4 = gvrow.FindControl("Label4") as System.Web.UI.WebControls.Label;
                        var l5 = gvrow.FindControl("Label5") as System.Web.UI.WebControls.Label;
                        var l6 = gvrow.FindControl("Label6") as System.Web.UI.WebControls.Label;

                        string Inssql = "delete hr_cmn_appraisal where cap_start_dt='" + l1.Text + "' and cap_end_dt='" + l2.Text + "' and cap_post_cat_cd='" + l3.Text + "' and cap_unit_cd='" + l4.Text + "' and cap_section_cd='" + l5.Text + "' and cap_subject_cd='" + l6.Text + "' and cap_seq_no='" + sqno.Text + "'";
                        Status = dbcon.Ora_Execute_CommamdText(Inssql);
                    }
                }
                grid_pkp2();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
            else
            {
                grid_pkp2();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Minimum Satu Kotak',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {
            grid_pkp2();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Semak Isu',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }
    protected void rst_clk(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["con_id"] = "";
        Response.Redirect("../SUMBER_MANUSIA/HR_KEMESKINI_KRITE_ins.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../SUMBER_MANUSIA/HR_KEMESKINI_KRITE_ins_view.aspx");
    }
}