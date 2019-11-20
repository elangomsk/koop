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
using System.Threading;

public partial class kw_jenis_cara_bayaran : System.Web.UI.Page
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
                
                userid = Session["New"].ToString();
                btb_kmes.Visible = false;

                var samp = Request.Url.Query;
                if (samp != "")
                {
                    lbl_name.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    view_details();
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
            ste_set = DBCon.Ora_Execute_table("select * from site_settings where ID IN ('1')");

            DataTable gt_lng = new DataTable();
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('716','705','1627','1628','1629','1486','61','35','15','77')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());             
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
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
    void view_details()
    {
        try
        {
            //BindData();
            btb_kmes.Visible = true;
            Button4.Visible = false;
            Button1.Visible = false;
            string ogid = lbl_name.Text;
            gen_id.Text = lbl_name.Text;

            DataTable ddokdicno = new DataTable();
            ddokdicno = Dblog.Ora_Execute_table("select Jenis_bayaran_cd,Jenis_bayaran,status From KW_Jenis_Cara_bayaran where id = '" + ogid + "'");
            if (ddokdicno.Rows.Count != 0)
            {
                TextBox1.Attributes.Add("Readonly", "Readonly");
                txt_jenis.Text = ddokdicno.Rows[0]["Jenis_bayaran"].ToString();
                TextBox1.Text = ddokdicno.Rows[0]["Jenis_bayaran_cd"].ToString();
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
        if (txt_jenis.Text != "")
        {
            SqlCommand ins_prof = new SqlCommand("insert into KW_Jenis_Cara_bayaran (Jenis_bayaran_cd,Jenis_bayaran,Status,cr_dt,crt_id) values(@Jenis_bayaran_cd,@Jenis_bayaran,@Status,@cr_dt,@crt_id)", conn);
            ins_prof.Parameters.AddWithValue("@Jenis_bayaran_cd", TextBox1.Text.Replace("'", "''"));
            ins_prof.Parameters.AddWithValue("@Jenis_bayaran", txt_jenis.Text.Replace("'", "''"));
            ins_prof.Parameters.AddWithValue("@Status", dd_list_sts.SelectedValue);
            ins_prof.Parameters.AddWithValue("@crt_id", Session["New"].ToString());
            ins_prof.Parameters.AddWithValue("@cr_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") );
            conn.Open();
            int i = ins_prof.ExecuteNonQuery();
            conn.Close();
            txt_jenis.Text = "";
            dd_list_sts.SelectedValue = "";
            TextBox1.Text = "";
            Session["validate_success"] = "SUCCESS";
            Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
            Response.Redirect("../kewengan/kw_jenis_cara_bayaran_view.aspx");
            
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Bidang.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

   
    protected void btb_kmes_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text != "" && txt_jenis.Text != "" && dd_list_sts.SelectedValue != "")
        {

            SqlCommand prof_upd = new SqlCommand("update KW_Jenis_Cara_bayaran  set Jenis_bayaran_cd=@Jenis_bayaran_cd,Jenis_bayaran=@Jenis_bayaran,Status=@Status,upd_dt=@upd_dt,upd_id=@upd_id where Id=@Id", conn);
            prof_upd.Parameters.AddWithValue("id", gen_id.Text);
            prof_upd.Parameters.AddWithValue("Jenis_bayaran_cd", TextBox1.Text.Replace("'", "''"));
            prof_upd.Parameters.AddWithValue("Jenis_bayaran", txt_jenis.Text.Replace("'", "''"));
            prof_upd.Parameters.AddWithValue("Status", dd_list_sts.SelectedValue);
            prof_upd.Parameters.AddWithValue("upd_id", Session["New"].ToString());
            prof_upd.Parameters.AddWithValue("upd_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") );
            conn.Open();
            int j = prof_upd.ExecuteNonQuery();
            conn.Close();
            txt_jenis.Text = "";
            TextBox1.Text = "";
            dd_list_sts.SelectedValue = "";
            TextBox1.Attributes.Remove("Readonly");
            Session["validate_success"] = "SUCCESS";
            Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
            Response.Redirect("../kewengan/kw_jenis_cara_bayaran_view.aspx");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Bidang.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
        Button4.Visible = true;
        btb_kmes.Visible = false;
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../kewengan/kw_jenis_cara_bayaran.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../kewengan/kw_jenis_cara_bayaran_view.aspx");
    }

    
}