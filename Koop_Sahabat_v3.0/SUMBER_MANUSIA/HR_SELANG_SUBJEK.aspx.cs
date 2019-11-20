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

public partial class HR_SELANG_SUBJEK : System.Web.UI.Page
{

    DBConnection dbcon = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DataTable dt = new DataTable();
    StudentWebService service = new StudentWebService();
    string useid = string.Empty;
    string Status = string.Empty;
    string oid = string.Empty;
    string sdd = string.Empty;
    string add = string.Empty;
    string CommandArgument1 = string.Empty, CommandArgument2 = string.Empty, CommandArgument3 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        string script = " $(function () {$('.select2').select2();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {

            if (Session["New"] != null)
            {
                useid = Session["New"].ToString();
                BAHA();
                var samp = Request.Url.Query;
                if (samp != "")
                {
                    lbl_id.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    view_details();
                }
                else
                {
                    btn_jenis_kmes.Visible = false;
                    btn_jenis_simp.Visible = true;
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
            gt_lng = dbcon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('532','448','1438','1439','1441','533','61', '15', '77')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentUICulture;
            TextInfo txtinfo = culinfo.TextInfo;

            h1_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            bb1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            bb2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());

            h3_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());

            lbl1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            lbl2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            lbl3_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());

            btn_jenis_simp.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }

    void view_details()
    {

        SqlConnection conn = new SqlConnection(cs);
        ss1_show.Attributes.Add("style","pointer-events:None;");
        Button1.Visible = false;
        string query2 = "select csb_section_cd,hs.cse_section_desc,csb_subject_cd,csb_subject_desc,csb_sub_weightage  from hr_cmn_subject hc left join hr_cmn_appr_section hs on hs.cse_section_cd = hc.csb_section_cd where Id='"+ lbl_id.Text + "'";
        conn.Open();
        var sqlCommand3 = new SqlCommand(query2, conn);
        var sqlReader3 = sqlCommand3.ExecuteReader();
        while (sqlReader3.Read())
        {
            btn_jenis_simp.Visible = false;
            btn_jenis_kmes.Visible = true;
            DataTable dd = new DataTable();
            dd = dbcon.Ora_Execute_table("select cse_section_cd,cse_section_desc from hr_cmn_appr_section where cse_section_cd='" + sqlReader3["csb_section_cd"].ToString() + "'");
            DDBAHA.SelectedValue = dd.Rows[0][0].ToString();

            TextBox1.Text = (string)sqlReader3["csb_subject_desc"].ToString().Trim();
            TextBox2.Text = (string)sqlReader3["csb_sub_weightage"].ToString().Trim();
        }
    }

   
    public void clear()
    {


        TextBox1.Text = "";
        TextBox2.Text = "";
    }

   

    void BAHA()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select cse_section_cd,UPPER(cse_section_desc) as cse_section_desc from hr_cmn_appr_section";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DDBAHA.DataSource = dt;
            DDBAHA.DataBind();
            DDBAHA.DataTextField = "cse_section_desc";
            DDBAHA.DataValueField = "cse_section_cd";
            DDBAHA.DataBind();
            DDBAHA.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btn_jenis_simp_Click(object sender, EventArgs e)
    {
        int scd;
        string len;
        if (DDBAHA.SelectedItem.Text != "--- PILIH ---")
        {

            if (TextBox2.Text != "")
            {
                dt = dbcon.Ora_Execute_table("select csb_section_cd,csb_subject_cd,csb_subject_desc,csb_sub_weightage from hr_cmn_subject where csb_section_cd='" + DDBAHA.SelectedItem.Value + "' and csb_subject_desc='" + TextBox1.Text + "' ");
                if (dt.Rows.Count == 0)
                {
                    dt = dbcon.Ora_Execute_table("SELECT max(RIGHT(csb_subject_cd, 2 )) csb_subject_cd from hr_cmn_subject where csb_section_cd='" + DDBAHA.SelectedItem.Value + "'  ");
                    if (dt.Rows[0][0].ToString() == "")
                    {
                        string scd1 = "01";
                        len = DDBAHA.SelectedItem.Value.Trim() + scd1;
                    }
                    else
                    {
                        scd = Convert.ToInt32(dt.Rows[0][0].ToString()) + 1;
                        len = DDBAHA.SelectedItem.Value + scd;
                    }

                    DataTable dtc = new DataTable();
                    dtc = dbcon.Ora_Execute_table("select  COALESCE(NULLIF(sum( convert( int, csb_sub_weightage) ),''),'0') csb_sub_weightage from hr_cmn_subject  where csb_section_cd='" + DDBAHA.SelectedItem.Value + "' ");
                    int ovt = Convert.ToInt32(dtc.Rows[0][0].ToString());
                    int total = 100 - ovt;
                    int total1 = 100 - ovt;
                    int txt = Convert.ToInt32(TextBox2.Text);
                    if (total >= txt)
                    {
                        useid = Session["New"].ToString();
                        string Inssql = "insert into hr_cmn_subject (csb_section_cd,csb_subject_cd,csb_subject_desc,csb_sub_weightage,csb_crt_id,csb_crt_dt)values ('" + DDBAHA.SelectedItem.Value + "','" + len + "','" + TextBox1.Text + "','" + TextBox2.Text + "','" + useid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                        Status = dbcon.Ora_Execute_CommamdText(Inssql);
                        if (Status == "SUCCESS")
                        {
                            Session["validate_success"] = "SUCCESS";
                            Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                            Response.Redirect("../SUMBER_MANUSIA/HR_SELANG_SUBJEK_view.aspx");
                        }
                    }

                    else
                    {
                        if (total == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Value Not More than 100%',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Only Allowed  " + total + "',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                        }
                    }
                }
            }
            else
            {                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Pemberat',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        else
        {            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Bahagian',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }
    protected void btn_jenis_kmes_Click(object sender, EventArgs e)
    {
        if (DDBAHA.SelectedItem.Text != "--- PILIH ---")
        {

            if (TextBox2.Text != "")
            {
                DataTable Check = new DataTable();
                Check = dbcon.Ora_Execute_table("select cse_section_cd,cse_section_desc from hr_cmn_appr_section where  cse_section_desc='" + DDBAHA.SelectedItem.Text + "'");
                dt = dbcon.Ora_Execute_table("select csb_section_cd,csb_subject_cd,csb_subject_desc,csb_sub_weightage from hr_cmn_subject where csb_section_cd='" + Check.Rows[0][0].ToString() + "'  ");
                if (dt.Rows.Count > 0)
                {
                    useid = Session["New"].ToString();
                    DataTable dtc = new DataTable();
                    dtc = dbcon.Ora_Execute_table("select  COALESCE(NULLIF(sum( convert( int, csb_sub_weightage) ),''),'0') csb_sub_weightage from hr_cmn_subject  where csb_section_cd='" + DDBAHA.SelectedItem.Value + "' ");
                    int ovt = Convert.ToInt32(dtc.Rows[0][0].ToString());
                    int total = 100 - ovt;
                    int total1 = 100 - ovt;
                    int txt = Convert.ToInt32(TextBox2.Text);
                    if (total >= txt)
                    {
                        string upssql = "update  hr_cmn_subject  set csb_section_cd='" + DDBAHA.SelectedItem.Value + "',csb_subject_desc='" + TextBox1.Text + "',csb_sub_weightage='" + TextBox2.Text + "',csb_upd_id='" + useid + "',csb_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where csb_section_cd='" + dt.Rows[0][0].ToString() + "' and csb_subject_cd='" + dt.Rows[0][1].ToString() + "'";
                        Status = dbcon.Ora_Execute_CommamdText(upssql);
                        if (Status == "SUCCESS")
                        {
                            Session["validate_success"] = "SUCCESS";
                            Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                            Response.Redirect("../SUMBER_MANUSIA/HR_SELANG_SUBJEK_view.aspx");
                        }
                    }
                    else
                    {
                        if (total == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Value Not More than 100%',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Only  Allowed " + total + "',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                        }
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Pemberat',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }

        }
        else
        {            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Bahagian',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["con_id"] = "";
        Response.Redirect("../SUMBER_MANUSIA/HR_SELANG_SUBJEK.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../SUMBER_MANUSIA/HR_SELANG_SUBJEK_view.aspx");
    }

}