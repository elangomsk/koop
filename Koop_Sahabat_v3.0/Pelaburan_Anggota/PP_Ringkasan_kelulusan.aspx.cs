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


public partial class PP_Ringkasan_kelulusan : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string str;
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    SqlCommand com;
    DataTable dt = new DataTable();
    string Status = string.Empty;
    string level, userid;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Button5);
        scriptManager.RegisterPostBackControl(this.Button7);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                level = Session["level"].ToString();
                userid = Session["New"].ToString();
                //Jawatan();
                //Default value
                MP_nama.Attributes.Add("Readonly", "Readonly");
              
                MP_tujuan.Attributes.Add("Readonly", "Readonly");
                MP_amaun.Attributes.Add("Readonly", "Readonly");
                MP_tempoh.Attributes.Add("Readonly", "Readonly");
                //UJ_tarikh1.Attributes.Add("Readonly", "Readonly");
                //UJ_tarikh2.Attributes.Add("Readonly", "Readonly");
                //UJ_tarikh3.Attributes.Add("Readonly", "Readonly");
                KJ_tarikh.Attributes.Add("Readonly", "Readonly");
                Button5.Visible = false;

                var samp = Request.Url.Query;
                if (samp != "")
                {
                    DataTable ddokdicno = new DataTable();
                    ddokdicno = DBCon.Ora_Execute_table("select app_new_icno,app_applcn_no from jpa_application where app_applcn_no='" + service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"])) + "'");
                    Icno.Text = ddokdicno.Rows[0]["app_new_icno"].ToString();
                    load_permohon();
                    dd_permohon.SelectedValue = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));                    
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

    void load_permohon()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select app_new_icno,app_applcn_no from jpa_application where app_new_icno='" + Icno.Text + "' order by Created_date ";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_permohon.DataSource = dt;
            dd_permohon.DataTextField = "app_applcn_no";
            dd_permohon.DataValueField = "app_applcn_no";
            dd_permohon.DataBind();
            //dd_permohon.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
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
                com.CommandText = "select app_new_icno from jpa_application where app_new_icno like '%' + @Search + '%'  group by app_new_icno";
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
                            countryNames.Add(sdr["app_new_icno"].ToString());

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


    protected void bind_permohon(object sender, EventArgs e)
    {
        if (dd_permohon.SelectedValue != "")
        {
            srch_rfd();
        }
    }

    //void Jawatan()
    //{
    //    DataSet Ds = new DataSet();
    //    try
    //    {
    //        string com = "select Jawatan_Code,Jawatan_desc from ref_jawatan order by Jawatan_desc ASC";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
    //        DataTable dt = new DataTable();
    //        adpt.Fill(dt);
    //        UJ_jawatan1.DataSource = dt;
    //        UJ_jawatan1.DataTextField = "Jawatan_desc";
    //        UJ_jawatan1.DataValueField = "Jawatan_Code";
    //        UJ_jawatan1.DataBind();
    //        UJ_jawatan1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    //        UJ_jawatan2.DataSource = dt;
    //        UJ_jawatan2.DataTextField = "Jawatan_desc";
    //        UJ_jawatan2.DataValueField = "Jawatan_Code";
    //        UJ_jawatan2.DataBind();
    //        UJ_jawatan2.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    //        UJ_jawatan3.DataSource = dt;
    //        UJ_jawatan3.DataTextField = "Jawatan_desc";
    //        UJ_jawatan3.DataValueField = "Jawatan_Code";
    //        UJ_jawatan3.DataBind();
    //        UJ_jawatan3.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}


    void srch_rfd()
    {
        if (Icno.Text != "")
        {
            DataTable ddicno = new DataTable();
            ddicno = DBCon.Ora_Execute_table("select app_new_icno from jpa_application where app_applcn_no='" + dd_permohon.SelectedValue + "'");
            if (ddicno.Rows.Count == 0)
            {
                reset();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Maklumat Carian Tidak Dijumpai');", true);
            }
            else
            {
                string Select_App_query = "select ISNULL(RT.tujuan_desc,'') as tujuan_desc,JA.app_new_icno,JA.app_applcn_no,JA.app_name,JA.app_loan_purpose_cd,JA.app_apply_amt,JA.app_apply_dur,RW.Wilayah_Name,ISNULL(RB.branch_desc, '') AS branch_desc,JPA.jkk_remark1,JPA.jkk_name1,JPA.jkk_post1,JPA.jkk_dt1,JPA.jkk_remark2,JPA.jkk_name2,JPA.jkk_post2,JPA.jkk_dt2,JPA.jkk_remark3,JPA.jkk_name3,JPA.jkk_post3,JPA.jkk_dt3,JPA.jkk_meeting_dt,JPA.jkk_result_ind,JPA.jkk_condition_remark,JPA.jkk_remark,JPA.jkk_approve_amt,JPA.jkk_approve_dur,ISNULL(JPA.jkk_bil,'') as jkk_bil from jpa_application as JA Left Join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd Left join ref_branch AS RB ON RB.branch_cd=JA.app_branch_cd Left Join ref_tujuan as RT ON RT.tujuan_cd=JA.app_loan_purpose_cd left join jpa_jkkpa_approval as JPA on JPA.jkk_applcn_no=JA.app_applcn_no where JA.app_applcn_no='" + dd_permohon.SelectedValue + "'";
                con.Open();
                var sqlCommand = new SqlCommand(Select_App_query, con);
                var sqlReader = sqlCommand.ExecuteReader();
                if (sqlReader.Read() == true)
                {
                    MP_nama.Text = sqlReader["app_name"].ToString();
                 
                    MP_tujuan.Text = sqlReader["tujuan_desc"].ToString();
                    //decimal amt = (decimal)sqlReader["app_apply_amt"];
                    MP_amaun.Text = double.Parse(sqlReader["app_apply_amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                    MP_tempoh.Text = sqlReader["app_apply_dur"].ToString();

                    DataTable ddicno1 = new DataTable();
                    ddicno1 = DBCon.Ora_Execute_table("select jkk_applcn_no from jpa_jkkpa_approval where jkk_applcn_no='" + (string)sqlReader["app_applcn_no"] + "'");
                    if (ddicno1.Rows.Count != 0)
                    {
                        Button2.Text = "Kemaskini";
                        //UJ_ulasan1.Value = sqlReader["jkk_remark1"].ToString();
                        //UJ_nama1.Text = sqlReader["jkk_name1"].ToString();
                        //UJ_jawatan1.Text = sqlReader["jkk_post1"].ToString();
                        //jkkbil.Text = sqlReader["jkk_bil"].ToString();
                        //UJ_tarikh1.Text = Convert.ToDateTime(sqlReader["jkk_dt1"]).ToString("dd/MM/yyyy");

                        //UJ_ulasan2.Value = sqlReader["jkk_remark2"].ToString();
                        //UJ_nama2.Text = sqlReader["jkk_name2"].ToString();
                        //UJ_jawatan2.Text = sqlReader["jkk_post2"].ToString();
                        //UJ_tarikh2.Text = Convert.ToDateTime(sqlReader["jkk_dt2"]).ToString("dd/MM/yyyy");

                        //UJ_ulasan3.Value = sqlReader["jkk_remark3"].ToString();
                        //UJ_nama3.Text = sqlReader["jkk_name3"].ToString();
                        //UJ_jawatan3.Text = sqlReader["jkk_post3"].ToString();
                        //string ujt3 = string.Empty;
                        //ujt3 = Convert.ToDateTime(sqlReader["jkk_dt3"]).ToString("dd/MM/yyyy");
                        //if (ujt3 != "01/01/1900")
                        //{
                        //    UJ_tarikh3.Text = ujt3;
                        //}
                        //else
                        //{
                        //    UJ_tarikh3.Text = "";
                        //}
                        jkkbil.Text = sqlReader["jkk_bil"].ToString();
                        KJ_tarikh.Text = Convert.ToDateTime(sqlReader["jkk_meeting_dt"]).ToString("dd/MM/yyyy");

                        string resultind = (string)sqlReader["jkk_result_ind"];
                        if (resultind == "L")
                        {
                            Button2.Visible = false;
                        }
                        else
                        {
                            Button2.Visible = true;
                        }
                        //else if (resultind == "B")
                        //{
                        //    RadioButton2.Checked = true;
                        //}
                        //else if (resultind == "T")
                        //{
                        //    RadioButton3.Checked = true;
                        //}
                        //else
                        //{
                        //    RadioButton1.Checked = false;
                        //    RadioButton2.Checked = false;
                        //    RadioButton3.Checked = false;
                        //}

                        KJ_catatan.Value = sqlReader["jkk_condition_remark"].ToString();
                        KJ_ulasan.Value = sqlReader["jkk_remark"].ToString();
                        //KJ_amaun.Text = (string)sqlReader["jkk_approve_amt"];                        
                        KJ_amaun.Text = double.Parse(sqlReader["jkk_approve_amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");

                        decimal dur1 = (decimal)sqlReader["jkk_approve_dur"];
                        KJ_tempoh.Text = dur1.ToString("00");
                        Button5.Visible = true;
                    }
                    sqlReader.Close();

                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan No KP Baru.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
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
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Isu.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }


    protected void btnsmmit_Click(object sender, EventArgs e)
    {
        try
        {
           
                
                    if (jkkbil.Text != "" && KJ_amaun.Text != "" && KJ_tarikh.Text != "" && KJ_ulasan.Value != "" && KJ_tempoh.Text != "")
                    {
                        DataTable ddicno2 = new DataTable();
                        ddicno2 = DBCon.Ora_Execute_table("select app_new_icno,app_applcn_no from jpa_application where app_applcn_no='" + dd_permohon.SelectedValue + "'");
                        DataTable ddicno1 = new DataTable();
                        ddicno1 = DBCon.Ora_Execute_table("select jkk_applcn_no from jpa_jkkpa_approval where jkk_applcn_no='" + ddicno2.Rows[0][1] + "'");
                        //string strResultInd;
                        //if (RadioButton1.Checked == true)
                        //{
                        //    strResultInd = "L";
                        //}
                        //else if (RadioButton2.Checked == true)
                        //{
                        //    strResultInd = "B";
                        //}
                        //else if (RadioButton3.Checked == true)
                        //{
                        //    strResultInd = "T";
                        //}
                        //else
                        //{
                        //    strResultInd = "";
                        //}
                        //string datedari1 = UJ_tarikh1.Text;
                        //DateTime dt1 = DateTime.ParseExact(datedari1, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                        //String tarikh1 = dt1.ToString("yyyy-mm-dd");

                        //string datedari2 = UJ_tarikh2.Text;
                        //DateTime dt2 = DateTime.ParseExact(datedari2, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                        //String tarikh2 = dt2.ToString("yyyy-mm-dd");
                        String tarikh3 = string.Empty;
                        //if (UJ_tarikh3.Text != "")
                        //{
                        //    string datedari3 = UJ_tarikh3.Text;
                        //    DateTime dt3 = DateTime.ParseExact(datedari3, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                        //    tarikh3 = dt3.ToString("yyyy-mm-dd");
                        //}
                        //else
                        //{
                            tarikh3 = "";
                        //}
                        string datedari4 = KJ_tarikh.Text;
                        DateTime dt4 = DateTime.ParseExact(datedari4, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                        String KJ_tarikh_dt = dt4.ToString("yyyy-mm-dd");

                        DataTable Check_status = new DataTable();
                        string mpval1 = "";
                        string mval1 = mpval1.Replace("'", "''");
                        string mpval2 = "";
                        string mval2 = mpval2.Replace("'", "''");
                        string mpval3 = "";
                        string mval3 = mpval3.Replace("'", "''");
                        if (ddicno1.Rows.Count == 0)
                        {
                            //if (strResultInd == "B" && KJ_catatan.Value == "")
                            //{
                            //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Sila Masukkan Catatan (jika lulus bersyarat)');", true);
                            //}
                            //else
                            //{

                                DBCon.Execute_CommamdText("insert into jpa_jkkpa_approval(jkk_applcn_no,jkk_remark1,jkk_name1,jkk_post1,jkk_dt1,jkk_remark2,jkk_name2,jkk_post2,jkk_dt2,jkk_remark3,jkk_name3,jkk_post3,jkk_dt3,jkk_meeting_dt,jkk_result_ind,jkk_condition_remark,jkk_remark,jkk_approve_amt,jkk_approve_dur,jkk_crt_id,jkk_crt_dt,jkk_upd_id,jkk_upd_dt,Created_date,jkk_bil) values ('" + ddicno2.Rows[0][1] + "','','" + mval1 + "','','','','" + mval2 + "','','','','" + mval3 + "','','" + tarikh3 + "','" + KJ_tarikh_dt + "','','" + KJ_catatan.Value + "','" + KJ_ulasan.Value + "','" + KJ_amaun.Text + "','" + KJ_tempoh.Text + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','','','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + jkkbil.Text + "')");
                                //if (strResultInd == "L" || strResultInd == "B")
                                //{
                                //    DBCon.Execute_CommamdText("UPDATE jpa_application SET app_sts_cd='Y' WHERE app_applcn_no='" + ddicno2.Rows[0][1] + "'");
                                //}
                                //else if (strResultInd == "T")
                                //{
                                //    DBCon.Execute_CommamdText("UPDATE jpa_application SET app_sts_cd='R' WHERE app_applcn_no='" + ddicno2.Rows[0][1] + "'");
                                //}

                                //DBCon.Execute_CommamdText("UPDATE jpa_application_checklist SET che_appl_sts_ind='1' WHERE che_applcn_no='" + ddicno2.Rows[0][1] + "'");
                                Session["validate_success"] = "SUCCESS";
                                Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                                Response.Redirect("../Pelaburan_Anggota/PP_Ringkasan_kelulusan_view.aspx");
                            //}
                        }
                        else
                        {
                            DBCon.Execute_CommamdText("UPDATE jpa_jkkpa_approval set jkk_name1='" + mval1 + "',jkk_name2='" + mval2 + "',jkk_name3='" + mval3 + "',jkk_dt3='" + tarikh3 + "',jkk_meeting_dt='" + KJ_tarikh_dt + "',jkk_condition_remark='" + KJ_catatan.Value + "',jkk_remark='" + KJ_ulasan.Value + "',jkk_approve_amt='" + KJ_amaun.Text + "',jkk_approve_dur='" + KJ_tempoh.Text + "',jkk_upd_id='" + Session["New"].ToString() + "',jkk_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',Created_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',jkk_bil='" + jkkbil.Text + "' where jkk_applcn_no='" + ddicno2.Rows[0][1] + "'");
                            //if (strResultInd == "L" || strResultInd == "B")
                            //{
                            //    DBCon.Execute_CommamdText("UPDATE jpa_application SET app_sts_cd='Y' WHERE app_applcn_no='" + ddicno2.Rows[0][1] + "'");
                            //}
                            //else if (strResultInd == "T")
                            //{
                            //    DBCon.Execute_CommamdText("UPDATE jpa_application SET app_sts_cd='R' WHERE app_applcn_no='" + ddicno2.Rows[0][1] + "'");
                            //}
                            Session["validate_success"] = "SUCCESS";
                            Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                            Response.Redirect("../Pelaburan_Anggota/PP_Ringkasan_kelulusan_view.aspx");
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Kelulusan Jawatankuasa.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
               
           
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }


    //protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (RadioButton1.Checked == true)
    //    {
    //        RadioButton2.Checked = false;
    //        RadioButton3.Checked = false;

    //    }
    //}
    //protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (RadioButton2.Checked == true)
    //    {
    //        RadioButton1.Checked = false;
    //        RadioButton3.Checked = false;

    //    }
    //}
    //protected void RadioButton3_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (RadioButton3.Checked == true)
    //    {
    //        RadioButton1.Checked = false;
    //        RadioButton2.Checked = false;

    //    }
    //}
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
     
        MP_tujuan.Text = "";
        MP_amaun.Text = "";
        MP_tempoh.Text = "";
    }

    protected void Ins_aud()
    {
        String a_text1 = "030501";
        String a_text2 = "RINGKASAN KELULUSAN";
        DataTable ins_aud = new DataTable();
        DBCon.Execute_CommamdText("insert into cmn_audit_trail (aud_crt_id,aud_crt_dt,aud_txn_cd,aud_txn_desc) values ('" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "','" + a_text1 + "','" + a_text2 + "')");
    }
    protected void click_print(object sender, EventArgs e)
    {
        {
            try
            {
                if (Icno.Text != "")
                {
                    DataTable ddicno2 = new DataTable();
                    ddicno2 = DBCon.Ora_Execute_table("select app_new_icno,app_applcn_no from jpa_application where app_applcn_no='" + dd_permohon.SelectedValue + "'");
                    //Path
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    //dt = DBCon.Ora_Execute_table("select JA.app_applcn_no,JA.app_new_icno,JA.app_name,JA.app_apply_amt,JA.app_apply_dur,RW.Wilayah_Name,RB.branch_desc,RT.tujuan_desc,RJ1.Jawatan_desc as desc1,RJ2.Jawatan_desc as desc2,RJ3.Jawatan_desc as desc3,JJA.* from jpa_application as JA Left join jpa_jkkpa_approval as JJA ON JJA.jkk_applcn_no=JA.app_applcn_no left join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd left join ref_branch as RB ON RB.branch_cd=JA.app_branch_cd left join ref_tujuan as RT ON RT.tujuan_cd = JA.app_loan_purpose_cd left join ref_jawatan as RJ1 on RJ1.Jawatan_Code=JJA.jkk_post1 left join ref_jawatan as RJ2 on RJ2.Jawatan_Code=JJA.jkk_post2 left join ref_jawatan as RJ3 on RJ3.Jawatan_Code=JJA.jkk_post3 where JA.app_new_icno='" + Icno.Text + "'");
                    dt = DBCon.Ora_Execute_table("select JA.app_applcn_no,JA.app_new_icno,JA.app_name,JA.app_apply_amt,JA.app_apply_dur,RW.Wilayah_Name,RB.branch_desc,RT.tujuan_desc,RJ1.Jawatan_desc as desc1,RJ2.Jawatan_desc as desc2,RJ3.Jawatan_desc as desc3,JJA.*,case when jja.jkk_result_ind = 'L' then 'LULUS' when JJA.jkk_result_ind = 'T' then 'REJECTED' else '' end as result from jpa_application as JA Left join jpa_jkkpa_approval as JJA ON JJA.jkk_applcn_no=JA.app_applcn_no left join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd left join ref_branch as RB ON RB.branch_cd=JA.app_branch_cd left join ref_tujuan as RT ON RT.tujuan_cd = JA.app_loan_purpose_cd left join ref_jawatan as RJ1 on RJ1.Jawatan_Code=JJA.jkk_post1 left join ref_jawatan as RJ2 on RJ2.Jawatan_Code=JJA.jkk_post2 left join ref_jawatan as RJ3 on RJ3.Jawatan_Code=JJA.jkk_post3 where JA.app_applcn_no='" + dd_permohon.SelectedValue + "'");

                    Rptviwer_kelulusan.Reset();
                    ds.Tables.Add(dt);

                    Rptviwer_kelulusan.LocalReport.DataSources.Clear();

                    Rptviwer_kelulusan.LocalReport.ReportPath = "Pelaburan_Anggota/R_kelulusan.rdlc";
                    ReportDataSource rds = new ReportDataSource("r_kel", dt);

                    ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("applcnno",ddicno2.Rows[0]["app_applcn_no"].ToString() )

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

                    string fname = DateTime.Now.ToString("dd_MM_yyyy");

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


                    Response.AddHeader("content-disposition", "attachment; filename=RINGKASAN_KELULUSAN_" + ddicno2.Rows[0]["app_applcn_no"].ToString() + "." + extension);

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

    protected void click_template(object sender, EventArgs e)
    {
        {
            try
            {

                if (Icno.Text != "")
                {
                    DataTable ddicno2 = new DataTable();
                    ddicno2 = DBCon.Ora_Execute_table("select app_new_icno,app_applcn_no from jpa_application where app_applcn_no='" + dd_permohon.SelectedValue + "'");
                    //Path
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    //dt = DBCon.Ora_Execute_table("select JA.app_applcn_no,JA.app_new_icno,JA.app_name,JA.app_apply_amt,JA.app_apply_dur,RW.Wilayah_Name,RB.branch_desc,RT.tujuan_desc,RJ1.Jawatan_desc as desc1,RJ2.Jawatan_desc as desc2,RJ3.Jawatan_desc as desc3,JJA.* from jpa_application as JA Left join jpa_jkkpa_approval as JJA ON JJA.jkk_applcn_no=JA.app_applcn_no left join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd left join ref_branch as RB ON RB.branch_cd=JA.app_branch_cd left join ref_tujuan as RT ON RT.tujuan_cd = JA.app_loan_purpose_cd left join ref_jawatan as RJ1 on RJ1.Jawatan_Code=JJA.jkk_post1 left join ref_jawatan as RJ2 on RJ2.Jawatan_Code=JJA.jkk_post2 left join ref_jawatan as RJ3 on RJ3.Jawatan_Code=JJA.jkk_post3 where JA.app_new_icno='" + Icno.Text + "'");
                    dt = DBCon.Ora_Execute_table("select JA.app_applcn_no,JA.app_new_icno,JA.app_name,JA.app_apply_amt,JA.app_apply_dur,RW.Wilayah_Name,RB.branch_desc,RT.tujuan_desc,RJ1.Jawatan_desc as desc1,RJ2.Jawatan_desc as desc2,RJ3.Jawatan_desc as desc3,JJA.*,case when jja.jkk_result_ind = 'L' then 'LULUS' when JJA.jkk_result_ind = 'T' then 'REJECTED' else '' end as result from jpa_application as JA Left join jpa_jkkpa_approval as JJA ON JJA.jkk_applcn_no=JA.app_applcn_no left join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd left join ref_branch as RB ON RB.branch_cd=JA.app_branch_cd left join ref_tujuan as RT ON RT.tujuan_cd = JA.app_loan_purpose_cd left join ref_jawatan as RJ1 on RJ1.Jawatan_Code=JJA.jkk_post1 left join ref_jawatan as RJ2 on RJ2.Jawatan_Code=JJA.jkk_post2 left join ref_jawatan as RJ3 on RJ3.Jawatan_Code=JJA.jkk_post3 where JA.app_applcn_no='" + dd_permohon.SelectedValue + "'");

                    Rptviwer_kelulusan.Reset();
                    ds.Tables.Add(dt);

                    Rptviwer_kelulusan.LocalReport.DataSources.Clear();

                    Rptviwer_kelulusan.LocalReport.ReportPath = "Pelaburan_Anggota/R_kelulusan_template.rdlc";
                    ReportDataSource rds = new ReportDataSource("r_kel", dt);
                    ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("applcnno",ddicno2.Rows[0]["app_applcn_no"].ToString() )

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


                    Response.AddHeader("content-disposition", "attachment; filename=RINGKASAN_KELULUSAN_TEMPLAT_" + ddicno2.Rows[0]["app_applcn_no"].ToString() + "." + extension);

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

    protected void clk_bak(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Pelaburan_Anggota/PP_Ringkasan_kelulusan_view.aspx");
    }
}