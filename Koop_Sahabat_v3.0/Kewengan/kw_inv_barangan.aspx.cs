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

public partial class kw_inv_barangan : System.Web.UI.Page
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
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                
                userid = Session["New"].ToString();
                btb_kmes.Visible = false;
                Applcn_no1.Text = "";
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

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]


    void app_language()

    {
        if (Session["New"] != null)
        {
            DataTable ste_set = new DataTable();
            ste_set = DBCon.Ora_Execute_table("select * from site_settings where ID IN ('1')");

            DataTable gt_lng = new DataTable();
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('814','705','1724','816','792','1834','1338','1835','1836','1486','61','35','15','77')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());          
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
            ps_lbl10.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
            ps_lbl11.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
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
    public static List<string> GetCompletionList(string prefixText, int count)
    {

        using (SqlConnection con = new SqlConnection())
        {

            con.ConnectionString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
            DBConnection dbcon1 = new DBConnection();
            DataTable qry1 = new DataTable();
            qry1 = dbcon1.Ora_Execute_table("select DISTINCT jenis_barang from KW_INVENTORI_BARANG where jenis_barang LIKE '%" + prefixText + "%'");
            //if (qry1.Rows.Count != 0)
            //{
                using (SqlCommand com = new SqlCommand())
                {
                    com.CommandText = "select DISTINCT jenis_barang from KW_INVENTORI_BARANG where jenis_barang LIKE '%' + @Search + '%'";
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
                            countryNames.Add(sdr["jenis_barang"].ToString());

                        }
                    }
                    else
                    {
                        countryNames.Add("Rekod Tidak Dijumpai ...");
                    }
                }

                    con.Close();
                    return countryNames;
                }
            //}
        }
    }


    void view_details()
    {
        try
        {
            
            btb_kmes.Visible = true;
            Button4.Visible = false;
            Button1.Visible = false;
            txt_jenis.Attributes.Add("Readonly", "Readonly");
            txt_kod_barang.Attributes.Add("Readonly", "Readonly");
            txt_name.Attributes.Add("Readonly", "Readonly");
            //tk_akhir.Attributes.Add("Readonly", "Readonly");
            string ogid = lbl_name.Text;
            gen_id.Text = lbl_name.Text;
            DataTable ddokdicno = new DataTable();
            ddokdicno = Dblog.Ora_Execute_table("select *,FORMAT(tarikh_barang,'dd/MM/yyyy', 'en-us') as tarikh from KW_INVENTORI_BARANG where id = '" + ogid + "'");
            if (ddokdicno.Rows.Count != 0)
            {
                txt_jenis.Text = ddokdicno.Rows[0]["jenis_barang"].ToString();
                txt_kod_barang.Text = ddokdicno.Rows[0]["kod_barang"].ToString();
                txt_name.Text = ddokdicno.Rows[0]["nama_barang"].ToString();
                tk_akhir.Text = ddokdicno.Rows[0]["tarikh"].ToString();
                txt_unit.Text = double.Parse(ddokdicno.Rows[0]["unit"].ToString()).ToString("C").Replace("RM","").Replace("RM", "").Replace("$", "");
                dd_list_sts.SelectedValue = ddokdicno.Rows[0]["Status"].ToString();
                TextBox1.Text = ddokdicno.Rows[0]["threshold"].ToString();
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
        string fmdate = string.Empty, tmdate = string.Empty;
        if (txt_jenis.Text != "" && txt_kod_barang.Text != "" && txt_name.Text != "" && txt_unit.Text != "" && tk_akhir.Text != "")
        {
            if (tk_akhir.Text != "")
            {
                string fdate = tk_akhir.Text;
                DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                fmdate = fd.ToString("yyyy-MM-dd");
            }

            DataTable ddokdicno = new DataTable();
            ddokdicno = Dblog.Ora_Execute_table("select * from KW_INVENTORI_BARANG where kod_barang='"+ txt_kod_barang.Text + "' and tarikh_barang >='"+ fmdate + "'");
            if (ddokdicno.Rows.Count == 0)
            {
                string kdakaun = string.Empty;
                string Inssql = "insert into KW_INVENTORI_BARANG (jenis_barang,kod_barang,nama_barang,unit,Status,crt_id,crt_dt,tarikh_barang,threshold)values('" + txt_jenis.Text.Replace("'", "''") + "','" + txt_kod_barang.Text.Replace("'", "''") + "','" + txt_name.Text.Replace("'", "''") + "','" + txt_unit.Text + "','" + dd_list_sts.SelectedValue + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "','" + fmdate + "','"+ TextBox1.Text + "')";
                Status = DBCon.Ora_Execute_CommamdText(Inssql);
                if (Status == "SUCCESS")
                {
                    Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                    Session["validate_success"] = "SUCCESS";
                    Response.Redirect("../kewengan/kw_inv_barangan_view.aspx");
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Sudah Ada.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Bidang.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    protected void btb_kmes_Click(object sender, EventArgs e)
    {
        string fmdate = string.Empty, tmdate = string.Empty;
        if (txt_jenis.Text != "" && txt_kod_barang.Text != "" && txt_name.Text != "" && txt_unit.Text != "" && tk_akhir.Text != "")
        {
            if (tk_akhir.Text != "")
            {
                string fdate = tk_akhir.Text;
                DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                fmdate = fd.ToString("yyyy-MM-dd");
            }
            DataTable ddokdicno = new DataTable();
            ddokdicno = Dblog.Ora_Execute_table("select * from KW_INVENTORI_BARANG where kod_barang='" + txt_kod_barang.Text + "' and tarikh_barang >='" + fmdate + "' and Id !='" + gen_id.Text + "'");
            if (ddokdicno.Rows.Count == 0)
            {
                string Inssql = "update KW_INVENTORI_BARANG set tarikh_barang='" + fmdate + "',unit='" + txt_unit.Text + "',Status='" + dd_list_sts.SelectedValue + "',threshold='"+ TextBox1.Text +"',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',upd_id='" + Session["New"].ToString() + "' where Id='" + gen_id.Text + "'";
                Status = DBCon.Ora_Execute_CommamdText(Inssql);
                if (Status == "SUCCESS")
                {
                    txt_jenis.Attributes.Remove("Readonly");
                    txt_kod_barang.Attributes.Remove("Readonly");
                    txt_name.Attributes.Remove("Readonly");
                    Session["validate_success"] = "SUCCESS";
                    Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                    Response.Redirect("../kewengan/kw_inv_barangan_view.aspx");
                }
            }
            
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Bidang.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
        Button4.Visible = true;
        btb_kmes.Visible = false;
    }


    //protected void katcd_TextChanged(object sender, EventArgs e)
    //{
    //    DataTable sel_kat = new DataTable();
    //    sel_kat = DBCon.Ora_Execute_table("select * from KW_Kategori_akaun where kat_cd = '" + txt_kat_cd.Text + "' order by kat_cd asc");
    //    if (sel_kat.Rows.Count != 0)
    //    {
    //        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Kategori Kod Telah Wujud.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
    //        txt_kat_cd.Text = "";
    //        txt_kat_cd.Focus();
    //    }
    //    else
    //    {
    //        txt_kat_akaun.Focus();
    //    }
    //}

    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../kewengan/kw_inv_barangan.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../kewengan/kw_inv_barangan_view.aspx");
    }

    
}