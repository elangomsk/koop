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

public partial class kw_diskaun : System.Web.UI.Page
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
                jenis_cukai();
                bind_kod_akaun();
                var samp = Request.Url.Query;
                if (samp != "")
                {
                    lbl_name.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    view_details();
                }
                else
                {
                    ver_id.Text = "0";
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('732','705','1658','1659','64','733','65','824','1660','734','715','1661','1622','61','15','77','1486')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[16][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());    
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());            
            ps_lbl10.Text = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());
            ps_lbl11.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl12.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl13.Text = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower());
            ps_lbl14.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
            ps_lbl15.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            Button5.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
           

        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    void bind_kod_akaun()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select kod_akaun,(kod_akaun + ' | ' + nama_akaun) as name from KW_Ref_Carta_Akaun where jenis_akaun_type !='1' and Status='A' order by kod_akaun asc";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_akaun.DataSource = dt;
            dd_akaun.DataTextField = "name";
            dd_akaun.DataValueField = "kod_akaun";
            dd_akaun.DataBind();
            dd_akaun.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void jenis_cukai()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select Id,Ref_Jenis_cukai from KW_Ref_Jenis_Cukai where status='A' order by Ref_Jenis_cukai asc";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList1.DataSource = dt;
            DropDownList1.DataTextField = "Ref_Jenis_cukai";
            DropDownList1.DataValueField = "Id";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void view_details()
    {
        Button1.Visible = false;
        try
        {
            string lblid = lbl_name.Text;
            DataTable ddokdicno = new DataTable();
            ddokdicno = DBCon.Ora_Execute_table("select * From KW_Ref_diskaun where Id='" + lblid + "' ");
            if (ddokdicno.Rows.Count != 0)
            {
                Button4.Text = "Kemaskini";
                ver_id.Text = "1";
                get_id.Text = lblid;
                TextBox1.Text = ddokdicno.Rows[0]["Ref_nama_diskaun"].ToString();
                if (Convert.ToDateTime(ddokdicno.Rows[0]["Ref_tarikh_mula"]).ToString("dd/MM/yyyy") != "01/01/1900")
                {
                    TextBox5.Text = Convert.ToDateTime(ddokdicno.Rows[0]["Ref_tarikh_mula"]).ToString("dd/MM/yyyy");
                }
                TextBox2.Text = ddokdicno.Rows[0]["Ref_kod_diskaun"].ToString();
                if (Convert.ToDateTime(ddokdicno.Rows[0]["Ref_tarikh_akhir"]).ToString("dd/MM/yyyy") != "01/01/1900")
                {
                    TextBox6.Text = Convert.ToDateTime(ddokdicno.Rows[0]["Ref_tarikh_akhir"]).ToString("dd/MM/yyyy");
                }
                TextBox3.Text = ddokdicno.Rows[0]["Ref_kadar"].ToString();
                TextBox7.Text = ddokdicno.Rows[0]["Ref_klasi_akaun"].ToString();
                dd_akaun.SelectedValue = ddokdicno.Rows[0]["Ref_kod_akaun"].ToString();
                alamat_akn.Value = ddokdicno.Rows[0]["Ref_descripsi"].ToString();
                DropDownList1.SelectedValue = ddokdicno.Rows[0]["Ref_jenis_cukai"].ToString();
                sts.SelectedValue = ddokdicno.Rows[0]["Status"].ToString();
                DropDownList3.SelectedValue = ddokdicno.Rows[0]["tc_jenis"].ToString();
                TextBox2.Focus();

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
    protected void clk_submit(object sender, EventArgs e)
    {
       
            string tk_m = string.Empty, tk_a = string.Empty;
        if (TextBox1.Text != "" && TextBox2.Text != "" && dd_akaun.SelectedValue != "")
        {
            if (TextBox5.Text != "")
            {
                DateTime dt_1 = DateTime.ParseExact(TextBox5.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                tk_m = dt_1.ToString("yyyy-MM-dd");
            }

            if (TextBox6.Text != "")
            {
                DateTime dt_2 = DateTime.ParseExact(TextBox6.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                tk_a = dt_2.ToString("yyyy-MM-dd");
            }
            DataTable get_kat = new DataTable();
            get_kat = DBCon.Ora_Execute_table("select * From KW_Ref_Carta_Akaun where kod_akaun='" + dd_akaun.SelectedValue + "'");
            if (ver_id.Text == "0")
            {
                DataTable ddokdicno = new DataTable();
                ddokdicno = DBCon.Ora_Execute_table("select * From KW_Ref_diskaun where Ref_nama_diskaun='" + TextBox1.Text + "' and Ref_kod_diskaun='" + TextBox2.Text + "'");
                if (ddokdicno.Rows.Count == 0)
                {


                    string Inssql = "insert into KW_Ref_diskaun(Ref_nama_diskaun,Ref_kod_diskaun,Ref_kadar,Ref_kod_akaun,Ref_tarikh_mula,Ref_tarikh_akhir,Ref_klasi_akaun,Ref_descripsi,Ref_jenis_cukai,Status,crt_id,cr_dt,tc_jenis,kat_akaun) values ('" + TextBox1.Text.Replace("'", "''") + "','" + TextBox2.Text.Replace("'", "''") + "','" + TextBox3.Text.Replace("'", "''") + "','" + dd_akaun.SelectedValue + "','" + tk_m + "','" + tk_a + "','" + TextBox7.Text.Replace("'", "''") + "','" + alamat_akn.Value.Replace("'", "''") + "','" + DropDownList1.SelectedValue + "','" + sts.SelectedValue + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + DropDownList3.SelectedValue + "','" + get_kat.Rows[0]["kat_akaun"].ToString() + "')";
                    Status = DBCon.Ora_Execute_CommamdText(Inssql);
                    if (Status == "SUCCESS")
                    {
                        Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                        Session["validate_success"] = "SUCCESS";
                        Response.Redirect("../kewengan/kw_diskaun_view.aspx");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Not Insert.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Sudah Ada.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else
            {
                string Inssql = "UPDATE KW_Ref_diskaun set kat_akaun='" + get_kat.Rows[0]["kat_akaun"].ToString() + "',tc_jenis='" + DropDownList3.SelectedValue + "',Ref_nama_diskaun='" + TextBox1.Text.Replace("'", "''") + "',Ref_kod_diskaun='" + TextBox2.Text.Replace("'", "''") + "',Ref_kadar='" + TextBox3.Text.Replace("'", "''") + "',Ref_kod_akaun='" + dd_akaun.SelectedValue + "',Ref_tarikh_mula='" + tk_m + "',Ref_tarikh_akhir='" + tk_a + "',Ref_klasi_akaun='" + TextBox7.Text.Replace("'", "''") + "',Ref_descripsi='" + alamat_akn.Value.Replace("'", "''") + "',Ref_jenis_cukai='" + DropDownList1.SelectedValue + "',Status='" + sts.SelectedValue + "',upd_id='" + Session["New"].ToString() + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Id = '" + get_id.Text + "'";
                Status = DBCon.Ora_Execute_CommamdText(Inssql);
                if (Status == "SUCCESS")
                {
                    Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                    Session["validate_success"] = "SUCCESS";
                    Response.Redirect("../kewengan/kw_diskaun_view.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Not Insert.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);

        }
       
        
    }

    protected void katcd_TextChanged(object sender, EventArgs e)
    {
        DataTable sel_kat = new DataTable();
        sel_kat = DBCon.Ora_Execute_table("select * from KW_Ref_diskaun where Ref_kod_diskaun = '" + TextBox2.Text + "'");
        if (sel_kat.Rows.Count != 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Kod Diskaun Yang Sedia Ada.');", true);
            TextBox2.Text = "";
            TextBox2.Focus();
        }
        else
        {
            TextBox1.Focus();
        }
     
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Response.Redirect("../kewengan/kw_diskaun.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Response.Redirect("../kewengan/kw_diskaun_view.aspx");
    }

    
}