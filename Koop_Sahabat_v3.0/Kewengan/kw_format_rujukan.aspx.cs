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
using System.IO;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Threading;
public partial class kw_format_rujukan : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();


    string level;
    string Status = string.Empty;
    string userid;
    string ref_id;
    string confirmValue, am;
    string qry1 = string.Empty, qry2 = string.Empty;
    string tn1 = string.Empty, tc1 = string.Empty, tc2 = string.Empty, tc3 = string.Empty, tc4 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        string script = " $(function () {$('.select2').select2()})";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                TextBox1.Text = DateTime.Now.Year.ToString();
                bind_doc_kod();
                userid = Session["New"].ToString();
                btb_kmes.Visible = false;
                var samp = Request.Url.Query;
                if (samp != "")
                {
                    lbl_name.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    view_details();
                }
                else
                {
                    DataTable ste_set = new DataTable();
                    ste_set = DBCon.Ora_Execute_table("select ws_format from site_settings where ID IN ('1')");
                    txt_fomat.Text = ste_set.Rows[0]["ws_format"].ToString() +"-";
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
            

            DataTable gt_lng = new DataTable();
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('718','705','1630','1631','719','1091','1486','61','35','77','15')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());      
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            btb_kmes.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
                
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    protected void ss2_select(object sender, EventArgs e)
    {
        DataTable ste_set = new DataTable();
        ste_set = DBCon.Ora_Execute_table("select ws_format from site_settings where ID IN ('1')");
        DataTable ddokdicno1 = new DataTable();
        ddokdicno1 = Dblog.Ora_Execute_table("select * from KW_Format_Nombor_rujukan where doc_type_cd = '" + fmt_type.SelectedValue + "' and Status= 'A'");
        if (ddokdicno1.Rows.Count != 0)
        {
            txt_fomat.Text = ste_set.Rows[0]["ws_format"].ToString() + "-";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('The Selected Type Already Exist. So please change status in Previous one.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
        else
        {
            DataTable ste_set1 = new DataTable();
            ste_set1 = DBCon.Ora_Execute_table("select * from KW_Ref_Doc_kod where Ref_doc_code ='" + fmt_type.SelectedValue + "' and Status= 'A'");
            txt_fomat.Text = ste_set.Rows[0]["ws_format"].ToString() + "-" + ste_set1.Rows[0]["Ref_doc_descript"].ToString();
        }
       
    }

    void bind_doc_kod()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select Ref_doc_code,Ref_doc_name from KW_Ref_Doc_kod where Ref_doc_code NOT IN ('01','05','06') and Status = 'A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            fmt_type.DataSource = dt;
            fmt_type.DataTextField = "Ref_doc_name";
            fmt_type.DataValueField = "Ref_doc_code";
            fmt_type.DataBind();
            fmt_type.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void view_details()
    {
        Button1.Visible = false;
        btb_kmes.Visible = true;
        Button4.Visible = false;
        try
        {
            //BindData();
          
            string ogid = lbl_name.Text;
            gen_id.Text = lbl_name.Text;

            DataTable ddokdicno = new DataTable();
            ddokdicno = Dblog.Ora_Execute_table("select Id,Jenis_rujukan,format,status,doc_type_cd from KW_Format_Nombor_rujukan where id = '" + ogid + "'");
            if (ddokdicno.Rows.Count != 0)
            {
                TextBox1.Attributes.Add("Readonly", "Readonly");
                txt_rujukan.Attributes.Add("Readonly", "Readonly");
                txt_fomat.Attributes.Add("Readonly", "Readonly");
                dip_none.Attributes.Add("style", "pointer-events:none;");
                txt_rujukan.Text = ddokdicno.Rows[0]["Jenis_rujukan"].ToString();
                fmt_type.SelectedValue = ddokdicno.Rows[0]["doc_type_cd"].ToString();
                DataTable ste_set = new DataTable();
                ste_set = DBCon.Ora_Execute_table("select ws_format from site_settings where ID IN ('1')");
                DataTable ste_set1 = new DataTable();
                ste_set1 = DBCon.Ora_Execute_table("select * from KW_Ref_Doc_kod where Ref_doc_code ='" + fmt_type.SelectedValue + "' and Status= 'A'");
                txt_fomat.Text = ste_set.Rows[0]["ws_format"].ToString() + "-" + ste_set1.Rows[0]["Ref_doc_descript"].ToString();
                //txt_fomat.Text = ddokdicno.Rows[0]["format"].ToString();
                //string s = ddokdicno.Rows[0]["doc_type_cd"].ToString();
                string[] s = ddokdicno.Rows[0]["doc_type_cd"].ToString().Split('-');
                //txt_doc4dig.Text = s.Substring(0, 4);
                //txt_doc10dig.Text = s.Substring(5, 9);
              
                dd_list_sts.SelectedValue = ddokdicno.Rows[0]["status"].ToString();

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        DataTable ddokdicno = new DataTable();
        ddokdicno = Dblog.Ora_Execute_table("select * from KW_Format_Nombor_rujukan where doc_type_cd = '" + fmt_type.SelectedValue + "' and Status= 'A'");
        if (ddokdicno.Rows.Count == 0)
        {
            if (txt_rujukan.Text != "" && txt_fomat.Text != "" && fmt_type.SelectedValue != "")
            {
                string format = txt_fomat.Text + "-" + TextBox1.Text + "-" + txt_rujukan.Text;
                //var isValidNumber = Regex.IsMatch(txt_fomat.Text, @"^[a-zA-Z]{2}[-][a-zA-Z]{3}[-][0-9]{2}[-][0-9]{4}$");
                //if (isValidNumber == true)
                //{
                    SqlCommand ins_prof = new SqlCommand("insert into KW_Format_Nombor_rujukan (Jenis_rujukan,format,Status,doc_type_cd,crt_id,cr_dt,cur_year,strt_seqno) values(@Jenis_rujukan,@format,@Status,@doc_type_cd,@crt_id,@cr_dt,@cur_year,@strt_seqno)", conn);
                    ins_prof.Parameters.AddWithValue("@Jenis_rujukan", txt_rujukan.Text);
                    ins_prof.Parameters.AddWithValue("@format", format);
                    ins_prof.Parameters.AddWithValue("@Status", dd_list_sts.SelectedValue);
                    ins_prof.Parameters.AddWithValue("@doc_type_cd", fmt_type.SelectedValue);

                    ins_prof.Parameters.AddWithValue("@crt_id", Session["New"].ToString());
                    ins_prof.Parameters.AddWithValue("@cr_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") );
                ins_prof.Parameters.AddWithValue("@cur_year", TextBox1.Text);
                ins_prof.Parameters.AddWithValue("@strt_seqno", txt_rujukan.Text);
                conn.Open();
                    int i = ins_prof.ExecuteNonQuery();
                    conn.Close();
                    Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                    Session["validate_success"] = "SUCCESS";
                    Response.Redirect("../kewengan/kw_format_rujukan_view.aspx");
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Format Tidak Sah, mestilah dalam format INV-00-0000.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                //}
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Bidang.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('The Selected Type Already Exist. So please change status in Previous one.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }
    protected void btb_kmes_Click(object sender, EventArgs e)
    {
        if (txt_rujukan.Text != "" && txt_fomat.Text != "" && fmt_type.SelectedValue != "")
        {
            //var isValidNumber = Regex.IsMatch(txt_fomat.Text, @"^[a-zA-Z]{2}[-][a-zA-Z]{3}[-][0-9]{2}[-][0-9]{4}$");
            //if (isValidNumber == true)
            //{
                SqlCommand prof_upd = new SqlCommand("update KW_Format_Nombor_rujukan set Status=@Status,upd_id=@upd_id,upd_dt=@upd_dt where Id=@Id", conn);
                prof_upd.Parameters.AddWithValue("id", gen_id.Text);
                prof_upd.Parameters.AddWithValue("Status", dd_list_sts.SelectedValue);
                prof_upd.Parameters.AddWithValue("upd_id", Session["New"].ToString());
                prof_upd.Parameters.AddWithValue("upd_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") );
                conn.Open();
                int j = prof_upd.ExecuteNonQuery();
                conn.Close();
                Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                Session["validate_success"] = "SUCCESS";
                Response.Redirect("../kewengan/kw_format_rujukan_view.aspx");
            //}
            //else
            //{
            //    Button4.Visible = false;
            //    btb_kmes.Visible = true;
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Format Tidak Sah, mestilah dalam format INV-00-0000.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            //}
        }
        else
        {
            Button4.Visible = false;
            btb_kmes.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Bidang.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }

    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Response.Redirect("../kewengan/kw_format_rujukan.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Response.Redirect("../kewengan/kw_format_rujukan_view.aspx");
    }

    
}