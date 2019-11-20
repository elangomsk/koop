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
using System.Xml;
using System.Threading;

public partial class HR_STAFF_KEYIN : System.Web.UI.Page
{
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    DataTable bagiyan = new DataTable();
    DataTable subj = new DataTable();
    string apprisl_stffno = string.Empty;
    string sel_clmn = string.Empty, sel_clmn1 = string.Empty;
    string userid;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        string script = " $(function () {$('.select2').select2();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
                btn_simp_Click();
                bagian();
                TextBox1.Text = "";
                txt_org.Attributes.Add("Readonly", "Readonly");
                TextBox2.Text = (double.Parse(DateTime.Now.ToString("yyyy")) - 1).ToString();
                TextBox2.Attributes.Add("Readonly", "Readonly");
                grid();
                //subject();
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('526','448','1460','505','484','1405','16','513','1288','190','1397','795','1461','1463','1439','1441','1464','274','1376','61', '15', '77')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentUICulture;
            TextInfo txtinfo = culinfo.TextInfo;

            h1_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            bb1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            bb2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[17][0].ToString().ToLower());

            h3_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            h3_tag2.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[18][0].ToString().ToLower());
            h3_tag3.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[19][0].ToString().ToLower());

            lbl1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            lbl2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());
            lbl3_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());

            lbl4_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            lbl5_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
            lbl6_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());

            lbl7_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
            lbl8_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            lbl9_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[20][0].ToString().ToLower());

            lbl10_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower());
            lbl11_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[16][0].ToString().ToLower());
            lbl12_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());

            btn_senarai.Text = txtinfo.ToTitleCase(gt_lng.Rows[21][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            btn_simp.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }

    }

    protected void DD_Bahagi_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DD_Bahagi.SelectedItem.Value == "--- PILIH ---")
        {
            subj.Rows.Clear();
            DD_Subje.Items.Clear();
            DD_Subje.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        //-Pusat---------------------------------------------------------------------------------
        string cmd6 = "select csb_subject_cd,csb_subject_desc From hr_cmn_subject as SBJ left join hr_cmn_appr_section as SEC on SEC.cse_section_cd=SBJ.csb_section_cd where csb_section_cd='" + DD_Bahagi.SelectedItem.Value.Trim() + "' order by csb_subject_cd";
        subj.Rows.Clear();
        DD_Subje.Items.Clear();

        con.Open();
        SqlDataAdapter adapterP = new SqlDataAdapter(cmd6, con);
        adapterP.Fill(subj);

        DD_Subje.DataSource = subj;
        DD_Subje.DataTextField = "csb_subject_desc";
        DD_Subje.DataValueField = "csb_subject_cd";
        DD_Subje.DataBind();
        //ddPusat.Items.RemoveAt(0); //remove 'Semua Wilayah'
        con.Close();
        DD_Subje.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        grid();
    }

    void bagian()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(conString);
            string com = "select cse_section_cd,cse_section_desc from hr_cmn_appr_section order by cse_section_cd";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_Bahagi.DataSource = dt;
            DD_Bahagi.DataBind();
            DD_Bahagi.DataTextField = "cse_section_desc";
            DD_Bahagi.DataValueField = "cse_section_cd";
            DD_Bahagi.DataBind();
            DD_Bahagi.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void subject()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(conString);
            string com = "select csb_subject_cd,csb_subject_desc From hr_cmn_subject as SBJ left join hr_cmn_appr_section as SEC on SEC.cse_section_cd=SBJ.csb_section_cd where csb_subject_cd='" + DD_Bahagi.SelectedValue.Trim() + "' order by csb_subject_cd";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_Subje.DataSource = dt;
            DD_Subje.DataBind();
            DD_Subje.DataTextField = "kavasan_name";
            DD_Subje.DataValueField = "kawasan_code";
            DD_Subje.DataBind();
            DD_Bahagi.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    void btn_simp_Click()
    {
        DataTable ddicno = new DataTable();
        ddicno = DBCon.Ora_Execute_table("select stf_staff_no From hr_staff_profile where stf_staff_no='" + Session["New"].ToString() + "' ");
        if (ddicno.Rows.Count > 0)
        {
            string stffno = ddicno.Rows[0]["stf_staff_no"].ToString();
            DataTable ddbind = new DataTable();
            ddbind = DBCon.Ora_Execute_table("select stf_staff_no,stf_name,stf_curr_grade_cd,hr_jaba_desc,hr_jaw_desc,hr_kejaw_desc,hr_unit_desc,ho.org_name from hr_staff_profile as SF left join Ref_hr_jabatan as JB on JB.hr_jaba_Code=SF.stf_curr_dept_cd left join Ref_hr_Jawatan as JW on JW.hr_jaw_Code=SF.stf_curr_post_cd left join Ref_hr_kategori_perjawatn as KJ on KJ.hr_kejaw_Code=SF.stf_curr_job_cat_cd left join Ref_hr_unit as UI on UI.hr_unit_Code=SF.stf_curr_unit_cd left join hr_organization ho on ho.org_gen_id=sf.str_curr_org_cd where stf_staff_no='" + stffno + "'");
            txt_stffno.Text = ddbind.Rows[0]["stf_staff_no"].ToString();
            txt_nama.Text = ddbind.Rows[0]["stf_name"].ToString();
            txt_gred.Text = ddbind.Rows[0]["stf_curr_grade_cd"].ToString();
            txt_jaba.Text = ddbind.Rows[0]["hr_jaba_desc"].ToString();
            txt_jawa.Text = ddbind.Rows[0]["hr_jaw_desc"].ToString();
            txt_ketogjawa.Text = ddbind.Rows[0]["hr_kejaw_desc"].ToString();
            txt_unit.Text = ddbind.Rows[0]["hr_unit_desc"].ToString();
            txt_org.Text = ddbind.Rows[0]["org_name"].ToString().ToUpper();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    void grid()
    {


        DataTable ddicno = new DataTable();
        ddicno = DBCon.Ora_Execute_table("select stf_staff_no,stf_curr_post_cd,stf_curr_unit_cd From hr_staff_profile where stf_staff_no='" + Session["New"].ToString() + "' ");
        if (ddicno.Rows.Count > 0)
        {
            string stffno = ddicno.Rows[0]["stf_staff_no"].ToString();
            DataTable ddicno1 = new DataTable();
            ddicno1 = DBCon.Ora_Execute_table("select sap_staff_no From hr_staff_appraisal where sap_staff_no='" + stffno + "' and '" + TextBox2.Text + "' between FORMAT(sap_start_dt,'yyyy') And FORMAT(sap_end_dt,'yyyy') and sap_post_cat_cd='" + ddicno.Rows[0]["stf_curr_post_cd"].ToString() + "' and sap_unit_cd='" + ddicno.Rows[0]["stf_curr_unit_cd"].ToString() + "'");
            DataTable ddicno2 = new DataTable();
            ddicno2 = DBCon.Ora_Execute_table("select * From hr_cmn_appraisal where '" + TextBox2.Text + "' between FORMAT(cap_start_dt,'yyyy') And FORMAT(cap_end_dt,'yyyy')");
            if (ddicno1.Rows.Count > 0)
            {
                if (TextBox2.Text != "" && DD_Bahagi.SelectedValue != "" && DD_Subje.SelectedValue == "")
                {
                    sel_clmn = "and '" + TextBox2.Text + "' between FORMAT(sap_start_dt,'yyyy') And FORMAT(sap_end_dt,'yyyy') and  sap_section_cd='" + DD_Bahagi.SelectedItem.Value + "' and sap_post_cat_cd='" + ddicno.Rows[0]["stf_curr_post_cd"].ToString() + "' and sap_unit_cd='" + ddicno.Rows[0]["stf_curr_unit_cd"].ToString() + "'";
                }

                else if (TextBox2.Text != "" && DD_Bahagi.SelectedValue != "" && DD_Subje.SelectedValue != "")
                {
                    sel_clmn = "and '" + TextBox2.Text + "' between FORMAT(sap_start_dt,'yyyy') And FORMAT(sap_end_dt,'yyyy') and sap_section_cd='" + DD_Bahagi.SelectedItem.Value + "' and sap_subject_cd='" + DD_Subje.SelectedItem.Value + "' and sap_post_cat_cd='" + ddicno.Rows[0]["stf_curr_post_cd"].ToString() + "' and sap_unit_cd='" + ddicno.Rows[0]["stf_curr_unit_cd"].ToString() + "'";
                }
                else
                {
                    sel_clmn = "and '" + TextBox2.Text + "' between FORMAT(sap_start_dt,'yyyy') And FORMAT(sap_end_dt,'yyyy') and sap_post_cat_cd='" + ddicno.Rows[0]["stf_curr_post_cd"].ToString() + "' and sap_unit_cd='" + ddicno.Rows[0]["stf_curr_unit_cd"].ToString() + "'";
                }

                apprisl_stffno = ddicno1.Rows[0]["sap_staff_no"].ToString();
                sap_grid();

            }
            else if (ddicno2.Rows.Count > 0)
            {

                if (TextBox2.Text != "" && DD_Bahagi.SelectedValue != "" && DD_Subje.SelectedValue == "")
                {
                    sel_clmn1 = "'" + TextBox2.Text + "' between FORMAT(cap_start_dt,'yyyy') And FORMAT(cap_end_dt,'yyyy') and cap_section_cd='" + DD_Bahagi.SelectedItem.Value + "' and cap_post_cat_cd='" + ddicno.Rows[0]["stf_curr_post_cd"].ToString() + "' and cap_unit_cd='" + ddicno.Rows[0]["stf_curr_unit_cd"].ToString() + "'";
                }

                else if (TextBox2.Text != "" && DD_Bahagi.SelectedValue != "" && DD_Subje.SelectedValue != "")
                {
                    sel_clmn1 = "'" + TextBox2.Text + "' between FORMAT(cap_start_dt,'yyyy') And FORMAT(cap_end_dt,'yyyy') and cap_section_cd='" + DD_Bahagi.SelectedItem.Value + "' and cap_subject_cd='" + DD_Subje.SelectedItem.Value + "' and cap_post_cat_cd='" + ddicno.Rows[0]["stf_curr_post_cd"].ToString() + "' and cap_unit_cd='" + ddicno.Rows[0]["stf_curr_unit_cd"].ToString() + "'";
                }
                else
                {
                    sel_clmn1 = "'" + TextBox2.Text + "' between FORMAT(cap_start_dt,'yyyy') And FORMAT(cap_end_dt,'yyyy') and cap_post_cat_cd='" + ddicno.Rows[0]["stf_curr_post_cd"].ToString() + "' and cap_unit_cd='" + ddicno.Rows[0]["stf_curr_unit_cd"].ToString() + "'";
                }

                cap_grid();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Penilaian Prestasi Belum Sedia Untuk Diakses',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }


        }
        con.Close();
    }


    void sap_grid()
    {
        DataTable sp_grd = new DataTable();
        sp_grd = DBCon.Ora_Execute_table("select * From hr_staff_appraisal as SA left join hr_cmn_appr_section as BH on BH.cse_section_cd=SA.sap_section_cd left join hr_cmn_subject as SB on SB.csb_subject_cd=SA.sap_subject_cd where sap_staff_no='" + apprisl_stffno + "' " + sel_clmn.ToString() + "");
        SqlCommand cmd = new SqlCommand("select cse_section_desc as GRID1,csb_subject_desc as GRID2,'' as GRID3,sap_weightage as GRID4 From hr_staff_appraisal as SA left join hr_cmn_appr_section as BH on BH.cse_section_cd=SA.sap_section_cd left join hr_cmn_subject as SB on SB.csb_subject_cd=SA.sap_subject_cd where sap_staff_no='" + apprisl_stffno + "' " + sel_clmn.ToString() + "", con);
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
            GridView1.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
            //if (TextBox2.Text != "" || DD_Bahagi.SelectedValue != "" || DD_Subje.SelectedValue != "")
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            //}
            //btn_hups.Visible = false;
        }
        else
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
            TextBox1.Text = "1";
            txt_ulasan.Value = sp_grd.Rows[0]["sap_staff_remark"].ToString();
        }
    }

    void cap_grid()
    {


        SqlCommand cmd = new SqlCommand("select ISNULL(UPPER(cse_section_desc),'') as GRID1,ISNULL(UPPER(csb_subject_desc),'') as GRID2,'' as GRID3,cap_weightage as GRID4 From hr_cmn_appraisal as AP left join hr_cmn_appr_section as BH on BH.cse_section_cd=AP.cap_section_cd left join hr_cmn_subject as SB on SB.csb_subject_cd=AP.cap_subject_cd where " + sel_clmn1.ToString() + "", con);
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
            GridView1.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
            //if (TextBox2.Text != "" || DD_Bahagi.SelectedValue != "" || DD_Subje.SelectedValue != "")
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            //}
            //btn_hups.Visible = false;
        }
        else
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
            TextBox1.Text = "2";
            txt_ulasan.Value = "";
        }
    }


    protected void btn_senarai_Click(object sender, EventArgs e)
    {
        //btn_simp_Click();
        if (TextBox2.Text != "" || DD_Bahagi.SelectedValue != "" || DD_Subje.SelectedValue != "")
        {
            grid();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan yang Maklumat',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }
    protected void btn_simp_Click(object sender, EventArgs e)
    {
        if (txt_ulasan.Value != "")
        {
            string ss1 = string.Empty, ss2 = string.Empty;
            DataTable ddicno1 = new DataTable();
            ddicno1 = DBCon.Ora_Execute_table("select stf_staff_no,stf_curr_post_cd,stf_curr_unit_cd From hr_staff_profile where stf_staff_no='" + Session["New"].ToString() + "' ");
            string stffno1 = ddicno1.Rows[0]["stf_staff_no"].ToString();
            DataTable ddicno = new DataTable();
            if (TextBox1.Text != "1")
            {
                //if (DD_Bahagi.SelectedValue != "" && DD_Subje.SelectedValue == "")
                //{
                //    ss1 = "and cap_section_cd='" + DD_Bahagi.SelectedItem.Value + "'";
                //}

                //else if (DD_Bahagi.SelectedValue != "" && DD_Subje.SelectedValue != "")
                //{
                //    ss1 = "and cap_section_cd='" + DD_Bahagi.SelectedItem.Value + "' and cap_subject_cd='" + DD_Subje.SelectedItem.Value + "' ";
                //}
                //else
                //{
                //    ss1 = "";
                //}
                ddicno = DBCon.Ora_Execute_table("select FORMAT(cap_start_dt,'yyyy-MM-dd') as v1,FORMAT(cap_end_dt,'yyyy-MM-dd') as v2,cap_post_cat_cd as v3,cap_unit_cd as v4,cap_section_cd as v5,cap_subject_cd as v6,cap_seq_no as v7,cap_weightage as v8 from hr_cmn_appraisal where '" + TextBox2.Text + "' between FORMAT(cap_start_dt,'yyyy') And FORMAT(cap_end_dt,'yyyy') and cap_post_cat_cd='" + ddicno1.Rows[0]["stf_curr_post_cd"].ToString() + "' and cap_unit_cd='" + ddicno1.Rows[0]["stf_curr_unit_cd"].ToString() + "'");
            }
            else
            {
                //if (DD_Bahagi.SelectedValue != "" && DD_Subje.SelectedValue == "")
                //{
                //    ss2 = "and sap_section_cd='" + DD_Bahagi.SelectedItem.Value + "'";
                //}

                //else if (DD_Bahagi.SelectedValue != "" && DD_Subje.SelectedValue != "")
                //{
                //    ss2 = "and sap_section_cd='" + DD_Bahagi.SelectedItem.Value + "' and sap_subject_cd='" + DD_Subje.SelectedItem.Value + "' ";
                //}
                //else
                //{
                //    ss2 = "";
                //}
                ddicno = DBCon.Ora_Execute_table("select FORMAT(sap_start_dt,'yyyy-MM-dd') as v1,FORMAT(sap_end_dt,'yyyy-MM-dd') as v2,sap_post_cat_cd as v3,sap_unit_cd as v4,sap_section_cd as v5,sap_subject_cd as v6,sap_seq_no as v7,sap_weightage as v8 From hr_staff_appraisal as SA where '" + TextBox2.Text + "' between FORMAT(sap_start_dt,'yyyy') And FORMAT(sap_end_dt,'yyyy') and sap_staff_no='" + stffno1 + "' and sap_post_cat_cd='" + ddicno1.Rows[0]["stf_curr_post_cd"].ToString() + "' and sap_unit_cd='" + ddicno1.Rows[0]["stf_curr_unit_cd"].ToString() + "'");
            }
            if (ddicno.Rows.Count > 0)
            {

                if (ddicno1.Rows.Count > 0)
                {

                    DataTable ddicno2 = new DataTable();
                    ddicno2 = DBCon.Ora_Execute_table("select sap_staff_no From hr_staff_appraisal where '" + TextBox2.Text + "' between FORMAT(sap_start_dt,'yyyy') And FORMAT(sap_end_dt,'yyyy') and sap_staff_no='" + stffno1 + "' and sap_post_cat_cd='" + ddicno1.Rows[0]["stf_curr_post_cd"].ToString() + "' and sap_unit_cd='" + ddicno1.Rows[0]["stf_curr_unit_cd"].ToString() + "'");
                    if (ddicno2.Rows.Count == 0)
                    {
                        for (int k = 0; k <= ddicno.Rows.Count - 1; k++)
                        {
                            DBCon.Execute_CommamdText("insert into hr_staff_appraisal(sap_staff_no,sap_start_dt,sap_end_dt,sap_post_cat_cd,sap_unit_cd,sap_section_cd,sap_subject_cd,sap_seq_no,sap_weightage,sap_staff_remark,sap_remark_id,sap_remark_dt,sap_crt_id,sap_crt_dt ) values('" + stffno1 + "','" + ddicno.Rows[k]["v1"].ToString() + "','" + ddicno.Rows[k]["v2"].ToString() + "','" + ddicno.Rows[k]["v3"].ToString() + "','" + ddicno.Rows[k]["v4"].ToString() + "','" + ddicno.Rows[k]["v5"].ToString() + "','" + ddicno.Rows[k]["v6"].ToString() + "','" + ddicno.Rows[k]["v7"].ToString() + "','" + ddicno.Rows[k]["v8"].ToString() + "','" + txt_ulasan.Value.Replace("'", "''") + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "')");
                        }
                        Session["validate_success"] = "SUCCESS";
                        Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                        Response.Redirect("../SUMBER_MANUSIA/HR_STAFF_KEYIN_view.aspx");

                    }
                    else
                    {
                        //string stffno1 = ddicno1.Rows[0]["stf_staff_no"].ToString();
                        for (int k = 0; k <= ddicno.Rows.Count - 1; k++)
                        {
                            //DBCon.Execute_CommamdText("update hr_staff_appraisal set sap_section_cd='" + ddicno.Rows[k]["v5"].ToString() + "',sap_subject_cd='" + ddicno.Rows[k]["v6"].ToString() + "',sap_seq_no='" + ddicno.Rows[k]["v7"].ToString() + "',sap_weightage='" + ddicno.Rows[k]["v8"].ToString() + "',sap_staff_remark='" + txt_ulasan.Value.Replace("'", "''") + "',sap_remark_id='" + Session["New"].ToString() + "',sap_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd") + "',sap_upd_id='" + Session["New"].ToString() + "' where sap_staff_no='" + stffno1 + "' and sap_start_dt='" + ddicno.Rows[k]["v1"].ToString() + "' and sap_end_dt='" + ddicno.Rows[k]["v2"].ToString() + "' and sap_post_cat_cd='" + ddicno.Rows[k]["v3"].ToString() + "' and sap_unit_cd='" + ddicno.Rows[k]["v4"].ToString() + "' and sap_seq_no='" + ddicno.Rows[k]["v7"].ToString() + "'");
                            DBCon.Execute_CommamdText("update hr_staff_appraisal set sap_staff_remark='" + txt_ulasan.Value.Replace("'", "''") + "',sap_remark_id='" + Session["New"].ToString() + "',sap_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd") + "',sap_upd_id='" + Session["New"].ToString() + "' where sap_staff_no='" + stffno1 + "' and sap_start_dt='" + ddicno.Rows[k]["v1"].ToString() + "' and sap_end_dt='" + ddicno.Rows[k]["v2"].ToString() + "' and sap_post_cat_cd='" + ddicno.Rows[k]["v3"].ToString() + "' and sap_unit_cd='" + ddicno.Rows[k]["v4"].ToString() + "' and sap_seq_no='" + ddicno.Rows[k]["v7"].ToString() + "'");
                        }
                        Session["validate_success"] = "SUCCESS";
                        Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                        Response.Redirect("../SUMBER_MANUSIA/HR_STAFF_KEYIN_view.aspx");
                    }
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Tiada Rekod bagi Tahun Semasa',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan yang Maklumat.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    protected void rst_clk(object sender, EventArgs e)
    {
        DD_Bahagi.SelectedValue = "";
        DD_Subje.SelectedValue = "";
        TextBox2.Text = (double.Parse(DateTime.Now.ToString("yyyy")) - 1).ToString();
        grid();
    }

    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        grid();
        GridView1.DataBind();
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../SUMBER_MANUSIA/HR_KEMESKINI_KRITE_ins_view.aspx");
    }
}