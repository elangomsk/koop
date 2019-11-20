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

public partial class Ast_dafter_aset : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();

    string useid = string.Empty;
    string Status, Status1;
    string tbl_name = string.Empty, sqclm_qry = string.Empty;
    string oid = string.Empty;
    string sdd = string.Empty;
    string qr1 = string.Empty;
    string CommandArgument1 = string.Empty, CommandArgument2 = string.Empty, CommandArgument3 = string.Empty, CommandArgument4 = string.Empty, CommandArgument5 = string.Empty, CommandArgument6 = string.Empty, CommandArgument7 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        string script = " $(function () {$('.select2').select2()})";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        //ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        //scriptManager.RegisterPostBackControl(this.Button1);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;
                if (samp != "")
                {
                    txt_nopo.Attributes.Add("Readonly", "Readonly");
                    //lbl_name.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    txt_nopo.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    grid();

                    Perolehan();
                    kate();
                    Org_view();
                    //jenisaset();
                    pemp_view();
                    sub_kategori();
                    Jenama();
                    Model();
                    Bahan_Bakar();
                    Jenis_Kenderaan();
                    negeri();
                    Penggunaan_Tanah();
                    bank_name();
                    Jenis_Pegangan();
                    Jenis_Milikan();
                    dd_staff();
                    Jenis_Geran();
                    TextBox6.Attributes.Add("Readonly", "Readonly");
                    TextBox7.Attributes.Add("Readonly", "Readonly");
                    TextBox8.Attributes.Add("Readonly", "Readonly");
                    TextBox9.Attributes.Add("Readonly", "Readonly");
                    DD_ketogri_Selection1.Attributes.Remove("style");
                    c001.Attributes.Remove("style");
                    hm001.Attributes.Remove("style");
                    iv001.Attributes.Remove("style");
                    TextBox6.Text = "0";
                    TextBox7.Text = "0";
                    TextBox8.Text = "0";
                    TextBox9.Text = "0";
                    view_details();
                    //ver_id.Text = "1";

                }
                else
                {
                    //ver_id.Text = "0";
                }
                useid = Session["New"].ToString();
               
             
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('1908','563','1557','1564','77','1909','1910','110','1405','1541','695','605','1911','1912','1913','1914','1915','1916','1917','1918','1919','740','1920','1541','695','605', '1921', '1912', '1913', '1922', '1923', '1924', '1925', '1926', '1927', '1928', '1929', '1930', '1931', '1917', '1932', '740', '1933', '1920', '1541', '695', '605', '1921', '1912', '1913', '1567', '1917', '53', '1920','1934', '695', '605', '1881', '1935', '1936', '1937', '1938', '29', '1939', '1882', '1799', '1940', '672', '683', '669', '684', '1889', '1941', '1263', '1264', '685', '686', '1924', '1920', '1943', '1944', '1428', '1945','1870', '1872', '1884', '52', '678', '680', '676', '1946', '688', '1947', '673', '1885', '677', '1886', '681', '1887', '1634', '1948', '1949', '1950', '1544', '496', '27', '1951', '28', '1141', '1420', '1036', '29', '61', '1942')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[37][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[37][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[73][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[76][0].ToString().ToLower());
            Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[87][0].ToString().ToLower());
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[37][0].ToString().ToLower());      
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[80][0].ToString().ToLower());
            ps_lbl10.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());            
            ps_lbl11.Text = txtinfo.ToTitleCase(gt_lng.Rows[29][0].ToString().ToLower());
            ps_lbl12.Text = txtinfo.ToTitleCase(gt_lng.Rows[71][0].ToString().ToLower());
            ps_lbl13.Text = txtinfo.ToTitleCase(gt_lng.Rows[19][0].ToString().ToLower());
            ps_lbl14.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            ps_lbl15.Text = txtinfo.ToTitleCase(gt_lng.Rows[75][0].ToString().ToLower());
            ps_lbl16.Text = txtinfo.ToTitleCase(gt_lng.Rows[38][0].ToString().ToLower());
            ps_lbl17.Text = txtinfo.ToTitleCase(gt_lng.Rows[39][0].ToString().ToLower());
            ps_lbl18.Text = txtinfo.ToTitleCase(gt_lng.Rows[40][0].ToString().ToLower());
            ps_lbl19.Text = txtinfo.ToTitleCase(gt_lng.Rows[81][0].ToString().ToLower());
            ps_lbl20.Text = txtinfo.ToTitleCase(gt_lng.Rows[84][0].ToString().ToLower());         
            ps_lbl21.Text = txtinfo.ToTitleCase(gt_lng.Rows[41][0].ToString().ToLower());
            ckbox_perlu.Text = txtinfo.ToTitleCase(gt_lng.Rows[88][0].ToString().ToLower());
            ps_lbl23.Text = txtinfo.ToTitleCase(gt_lng.Rows[42][0].ToString().ToLower());
            ps_lbl24.Text = txtinfo.ToTitleCase(gt_lng.Rows[16][0].ToString().ToLower());
            ps_lbl25.Text = txtinfo.ToTitleCase(gt_lng.Rows[43][0].ToString().ToLower());
            ps_lbl26.Text = txtinfo.ToTitleCase(gt_lng.Rows[71][0].ToString().ToLower());
            ps_lbl27.Text = txtinfo.ToTitleCase(gt_lng.Rows[19][0].ToString().ToLower());
            ps_lbl28.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            ps_lbl29.Text = txtinfo.ToTitleCase(gt_lng.Rows[44][0].ToString().ToLower());
            ps_lbl30.Text = txtinfo.ToTitleCase(gt_lng.Rows[38][0].ToString().ToLower());          
            ps_lbl31.Text = txtinfo.ToTitleCase(gt_lng.Rows[39][0].ToString().ToLower());
            ps_lbl32.Text = txtinfo.ToTitleCase(gt_lng.Rows[45][0].ToString().ToLower());
            ps_lbl33.Text = txtinfo.ToTitleCase(gt_lng.Rows[89][0].ToString().ToLower());
            ps_lbl34.Text = txtinfo.ToTitleCase(gt_lng.Rows[46][0].ToString().ToLower());
            ps_lbl35.Text = txtinfo.ToTitleCase(gt_lng.Rows[47][0].ToString().ToLower());
            ps_lbl36.Text = txtinfo.ToTitleCase(gt_lng.Rows[85][0].ToString().ToLower());
            ps_lbl37.Text = txtinfo.ToTitleCase(gt_lng.Rows[48][0].ToString().ToLower());
            ps_lbl38.Text = txtinfo.ToTitleCase(gt_lng.Rows[49][0].ToString().ToLower());
            ps_lbl39.Text = txtinfo.ToTitleCase(gt_lng.Rows[50][0].ToString().ToLower());
            ps_lbl40.Text = txtinfo.ToTitleCase(gt_lng.Rows[51][0].ToString().ToLower());   
            ps_lbl41.Text = txtinfo.ToTitleCase(gt_lng.Rows[52][0].ToString().ToLower());
            ps_lbl42.Text = txtinfo.ToTitleCase(gt_lng.Rows[41][0].ToString().ToLower());
            ps_lbl43.Text = txtinfo.ToTitleCase(gt_lng.Rows[53][0].ToString().ToLower());
            ps_lbl44.Text = txtinfo.ToTitleCase(gt_lng.Rows[16][0].ToString().ToLower());          
            CheckBox1.Text = txtinfo.ToTitleCase(gt_lng.Rows[54][0].ToString().ToLower());
            ps_lbl46.Text = txtinfo.ToTitleCase(gt_lng.Rows[43][0].ToString().ToLower());
            ps_lbl47.Text = txtinfo.ToTitleCase(gt_lng.Rows[71][0].ToString().ToLower());
            ps_lbl48.Text = txtinfo.ToTitleCase(gt_lng.Rows[19][0].ToString().ToLower());
            ps_lbl49.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            ps_lbl50.Text = txtinfo.ToTitleCase(gt_lng.Rows[44][0].ToString().ToLower());         
            ps_lbl51.Text = txtinfo.ToTitleCase(gt_lng.Rows[38][0].ToString().ToLower());
            ps_lbl52.Text = txtinfo.ToTitleCase(gt_lng.Rows[39][0].ToString().ToLower());
            ps_lbl53.Text = txtinfo.ToTitleCase(gt_lng.Rows[77][0].ToString().ToLower());
            ps_lbl54.Text = txtinfo.ToTitleCase(gt_lng.Rows[41][0].ToString().ToLower());
            ps_lbl55.Text = txtinfo.ToTitleCase(gt_lng.Rows[16][0].ToString().ToLower());
            ps_lbl56.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl57.Text = txtinfo.ToTitleCase(gt_lng.Rows[43][0].ToString().ToLower());
            ps_lbl58.Text = txtinfo.ToTitleCase(gt_lng.Rows[67][0].ToString().ToLower());
            ps_lbl59.Text = txtinfo.ToTitleCase(gt_lng.Rows[71][0].ToString().ToLower());          
            ps_lbl60.Text = txtinfo.ToTitleCase(gt_lng.Rows[19][0].ToString().ToLower());
            ps_lbl61.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            ps_lbl62.Text = txtinfo.ToTitleCase(gt_lng.Rows[79][0].ToString().ToLower());
            ps_lbl63.Text = txtinfo.ToTitleCase(gt_lng.Rows[55][0].ToString().ToLower());
            ps_lbl64.Text = txtinfo.ToTitleCase(gt_lng.Rows[90][0].ToString().ToLower());
            ps_lbl65.Text = txtinfo.ToTitleCase(gt_lng.Rows[86][0].ToString().ToLower());
            ps_lbl66.Text = txtinfo.ToTitleCase(gt_lng.Rows[56][0].ToString().ToLower());
            ps_lbl67.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            ps_lbl68.Text = txtinfo.ToTitleCase(gt_lng.Rows[57][0].ToString().ToLower());
            ps_lbl69.Text = txtinfo.ToTitleCase(gt_lng.Rows[33][0].ToString().ToLower());
            ps_lbl70.Text = txtinfo.ToTitleCase(gt_lng.Rows[65][0].ToString().ToLower());             
            ps_lbl71.Text = txtinfo.ToTitleCase(gt_lng.Rows[91][0].ToString().ToLower());
            ps_lbl72.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            ps_lbl73.Text = txtinfo.ToTitleCase(gt_lng.Rows[28][0].ToString().ToLower());
            ps_lbl74.Text = txtinfo.ToTitleCase(gt_lng.Rows[24][0].ToString().ToLower());
            ps_lbl75.Text = txtinfo.ToTitleCase(gt_lng.Rows[25][0].ToString().ToLower());
            ps_lbl76.Text = txtinfo.ToTitleCase(gt_lng.Rows[66][0].ToString().ToLower());
            ps_lbl77.Text = txtinfo.ToTitleCase(gt_lng.Rows[68][0].ToString().ToLower());
            RadioButton1.Text = txtinfo.ToTitleCase(gt_lng.Rows[26][0].ToString().ToLower());
            RadioButton2.Text = txtinfo.ToTitleCase(gt_lng.Rows[27][0].ToString().ToLower());
            ps_lbl80.Text = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower());         
            ps_lbl81.Text = txtinfo.ToTitleCase(gt_lng.Rows[22][0].ToString().ToLower());
            ps_lbl82.Text = txtinfo.ToTitleCase(gt_lng.Rows[70][0].ToString().ToLower());
            ps_lbl83.Text = txtinfo.ToTitleCase(gt_lng.Rows[43][0].ToString().ToLower());
            ps_lbl84.Text = txtinfo.ToTitleCase(gt_lng.Rows[58][0].ToString().ToLower());
            ps_lbl85.Text = txtinfo.ToTitleCase(gt_lng.Rows[92][0].ToString().ToLower());
            RadioButton3.Text = txtinfo.ToTitleCase(gt_lng.Rows[69][0].ToString().ToLower());        
            RadioButton4.Text = txtinfo.ToTitleCase(gt_lng.Rows[59][0].ToString().ToLower());
            ps_lbl88.Text = txtinfo.ToTitleCase(gt_lng.Rows[32][0].ToString().ToLower());
            ps_lbl89.Text = txtinfo.ToTitleCase(gt_lng.Rows[83][0].ToString().ToLower());
            ps_lbl90.Text = txtinfo.ToTitleCase(gt_lng.Rows[34][0].ToString().ToLower());              
            ps_lbl91.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            ps_lbl92.Text = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
            ps_lbl93.Text = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());
            ps_lbl94.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
            ps_lbl95.Text = txtinfo.ToTitleCase(gt_lng.Rows[60][0].ToString().ToLower());
            ps_lbl96.Text = txtinfo.ToTitleCase(gt_lng.Rows[25][0].ToString().ToLower());
            ps_lbl97.Text = txtinfo.ToTitleCase(gt_lng.Rows[61][0].ToString().ToLower());
            ps_lbl98.Text = txtinfo.ToTitleCase(gt_lng.Rows[17][0].ToString().ToLower());
            ps_lbl99.Text = txtinfo.ToTitleCase(gt_lng.Rows[74][0].ToString().ToLower());
            ps_lbl100.Text = txtinfo.ToTitleCase(gt_lng.Rows[18][0].ToString().ToLower());      
            ps_lbl101.Text = txtinfo.ToTitleCase(gt_lng.Rows[35][0].ToString().ToLower());
            ps_lbl102.Text = txtinfo.ToTitleCase(gt_lng.Rows[21][0].ToString().ToLower());
            ps_lbl103.Text = txtinfo.ToTitleCase(gt_lng.Rows[36][0].ToString().ToLower());
            ps_lbl104.Text = txtinfo.ToTitleCase(gt_lng.Rows[78][0].ToString().ToLower());
            ps_lbl105.Text = txtinfo.ToTitleCase(gt_lng.Rows[62][0].ToString().ToLower());
            ps_lbl106.Text = txtinfo.ToTitleCase(gt_lng.Rows[82][0].ToString().ToLower());
            ps_lbl107.Text = txtinfo.ToTitleCase(gt_lng.Rows[63][0].ToString().ToLower());
            ps_lbl108.Text = txtinfo.ToTitleCase(gt_lng.Rows[72][0].ToString().ToLower());
            ps_lbl109.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl110.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());        
            ps_lbl111.Text = txtinfo.ToTitleCase(gt_lng.Rows[64][0].ToString().ToLower());
            ps_lbl112.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl113.Text = txtinfo.ToTitleCase(gt_lng.Rows[20][0].ToString().ToLower());
            ps_lbl114.Text = txtinfo.ToTitleCase(gt_lng.Rows[30][0].ToString().ToLower());
            ps_lbl115.Text = txtinfo.ToTitleCase(gt_lng.Rows[31][0].ToString().ToLower());
            ps_lbl116.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            Simpan.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
          
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
            DD_ketogri_Selection1.SelectedValue = "";
            if (txt_nopo.Text != "")
            {
                DataTable ddokdicno1 = new DataTable();
                ddokdicno1 = DBCon.Ora_Execute_table("select KE.ast_kategori_desc,SKE.ast_subkateast_desc,JE.ast_jeniaset_desc,KO.cas_asset_desc,acp_receive_upd_dt,acp_asset_cat_cd,acp_asset_sub_cat_cd,acp_asset_type_cd,acp_asset_cd,acp_seq_no from (select acp_asset_cat_cd,acp_asset_sub_cat_cd,acp_asset_type_cd,acp_asset_cd,(CASE WHEN acp_receive_upd_dt IS NULL or acp_receive_upd_dt='1900-01-01' THEN  acp_receive_dt ELSE acp_receive_upd_dt END)  acp_receive_upd_dt,acp_seq_no from ast_acceptance where acp_po_no='" + txt_nopo.Text + "' and acp_verify_sts_cd = 'S' group by acp_asset_cat_cd,acp_asset_sub_cat_cd,acp_asset_type_cd,acp_asset_cd,acp_receive_upd_dt,acp_receive_dt,acp_seq_no) as a left join Ref_ast_kategori as KE on KE.ast_kategori_Code=a.acp_asset_cat_cd  left join Ref_ast_jenis_aset as JE on JE.ast_jeniaset_Code=a.acp_asset_type_cd and JE.ast_sub_cat_Code=a.acp_asset_sub_cat_cd and JE.ast_cat_Code=a.acp_asset_cat_cd left join ast_cmn_asset as KO on KO.cas_asset_cd=a.acp_asset_cd and ko.cas_asset_cat_cd=a.acp_asset_cat_cd and KO.cas_asset_sub_cat_cd=a.acp_asset_sub_cat_cd and ko.cas_asset_type_cd=a.acp_asset_type_cd left join Ref_ast_sub_kategri_Aset as SKE on SKE.ast_subkateast_Code=a.acp_asset_sub_cat_cd");
                if (ddokdicno1.Rows.Count != 0)
                {
                    //senariaset.Visible = true;
                    //mukulamataset.Visible = true;
                    grid();
                }
                else
                {
                    grid();
                    grid_bot();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Pengesahan Terimaan Aset Belum Dilakukan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else
            {
                grid();
                grid_bot();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan No PO.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void Jenis_Geran()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select ast_Geran_desc,ast_Geran_code from Ref_ast_Jenis_Geran where status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList3.DataSource = dt;
            DropDownList3.DataTextField = "ast_Geran_desc";
            DropDownList3.DataValueField = "ast_Geran_code";
            DropDownList3.DataBind();
            DropDownList3.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void Jenis_Milikan()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select ast_Milikan_desc,ast_Milikan_code From Ref_ast_Jenis_Milikan where status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_MILIKAN4.DataSource = dt;
            DD_MILIKAN4.DataTextField = "ast_Milikan_desc";
            DD_MILIKAN4.DataValueField = "ast_Milikan_code";
            DD_MILIKAN4.DataBind();
            DD_MILIKAN4.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void dd_staff()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select stf_staff_no,UPPER(stf_name) as stf_name from hr_staff_profile where stf_curr_dept_cd IN ('01','03')";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_PEGAWAI4.DataSource = dt;
            DD_PEGAWAI4.DataTextField = "stf_name";
            DD_PEGAWAI4.DataValueField = "stf_staff_no";
            DD_PEGAWAI4.DataBind();
            DD_PEGAWAI4.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            DD_PEGAWAIAST4.DataSource = dt;
            DD_PEGAWAIAST4.DataTextField = "stf_name";
            DD_PEGAWAIAST4.DataValueField = "stf_staff_no";
            DD_PEGAWAIAST4.DataBind();
            DD_PEGAWAIAST4.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void Perolehan()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select ast_Perolehan_code,ast_Perolehan_desc From Ref_ast_Perolehan where status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_Perolehan.DataSource = dt;
            DD_Perolehan.DataTextField = "ast_Perolehan_desc";
            DD_Perolehan.DataValueField = "ast_Perolehan_code";
            DD_Perolehan.DataBind();
            DD_Perolehan.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void negeri()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select hr_negeri_desc as Decription,hr_negeri_code as Decription_code From ref_hr_negeri where status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_Negeri4.DataSource = dt;
            DD_Negeri4.DataTextField = "Decription";
            DD_Negeri4.DataValueField = "Decription_code";
            DD_Negeri4.DataBind();
            DD_Negeri4.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            DD_Negiri5.DataSource = dt;
            DD_Negiri5.DataTextField = "Decription";
            DD_Negiri5.DataValueField = "Decription_code";
            DD_Negiri5.DataBind();
            DD_Negiri5.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void bank_name()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select Bank_Name,Bank_Code From Ref_Nama_Bank where status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_Namabank4.DataSource = dt;
            DD_Namabank4.DataTextField = "Bank_Name";
            DD_Namabank4.DataValueField = "Bank_Code";
            DD_Namabank4.DataBind();
            DD_Namabank4.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void Penggunaan_Tanah()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select ast_Penggunaan_desc,ast_Penggunaan_code from Ref_ast_Penggunaan_Tanah where status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_penggunaan4.DataSource = dt;
            DD_penggunaan4.DataTextField = "ast_Penggunaan_desc";
            DD_penggunaan4.DataValueField = "ast_Penggunaan_code";
            DD_penggunaan4.DataBind();
            DD_penggunaan4.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void Jenama()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select ast_jenama_desc,ast_jenama_code From [Ref_ast_Jenama] where status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_JENAMA2.DataSource = dt;
            DD_JENAMA2.DataTextField = "ast_jenama_desc";
            DD_JENAMA2.DataValueField = "ast_jenama_code";
            DD_JENAMA2.DataBind();
            DD_JENAMA2.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void Bahan_Bakar()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select ast_bahanbakar_code,ast_bahanbakar_desc From Ref_ast_Bahan_Bakar where status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_Bahan2.DataSource = dt;
            DD_Bahan2.DataTextField = "ast_bahanbakar_desc";
            DD_Bahan2.DataValueField = "ast_bahanbakar_code";
            DD_Bahan2.DataBind();
            DD_Bahan2.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void Jenis_Kenderaan()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select ast_Kenderaan_code,ast_Kenderaan_desc From Ref_ast_Jenis_Kenderaan where status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_jeniskend2.DataSource = dt;
            DD_jeniskend2.DataTextField = "ast_Kenderaan_desc";
            DD_jeniskend2.DataValueField = "ast_Kenderaan_code";
            DD_jeniskend2.DataBind();
            DD_jeniskend2.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void Jenis_Pegangan()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select ast_Pegangan_code,ast_Pegangan_desc from Ref_ast_Jenis_Pegangan where status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_pegangan4.DataSource = dt;
            DD_pegangan4.DataTextField = "ast_Pegangan_desc";
            DD_pegangan4.DataValueField = "ast_Pegangan_code";
            DD_pegangan4.DataBind();
            DD_pegangan4.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void Model()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select ast_Model_desc,ast_Model_code From Ref_ast_Model where status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_Model2.DataSource = dt;
            DD_Model2.DataTextField = "ast_Model_desc";
            DD_Model2.DataValueField = "ast_Model_code";
            DD_Model2.DataBind();
            DD_Model2.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void kate()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select ast_kategori_desc,ast_kategori_code from Ref_ast_kategori where status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_ketogri_Selection1.DataSource = dt;
            DD_ketogri_Selection1.DataTextField = "ast_kategori_desc";
            DD_ketogri_Selection1.DataValueField = "ast_kategori_code";
            DD_ketogri_Selection1.DataBind();
            DD_ketogri_Selection1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void Org_view()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select org_gen_id,UPPER(org_name) as org_name from hr_organization order by org_name";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList1.DataSource = dt;
            DropDownList1.DataTextField = "org_name";
            DropDownList1.DataValueField = "org_gen_id";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void pemp_view()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select sup_id,upper(sup_name) sup_name from ast_supplier order by sup_name";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_pemp1.DataSource = dt;
            dd_pemp1.DataTextField = "sup_name";
            dd_pemp1.DataValueField = "sup_id";
            dd_pemp1.DataBind();
            dd_pemp1.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            dd_pemp2.DataSource = dt;
            dd_pemp2.DataTextField = "sup_name";
            dd_pemp2.DataValueField = "sup_id";
            dd_pemp2.DataBind();
            dd_pemp2.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            dd_pemp3.DataSource = dt;
            dd_pemp3.DataTextField = "sup_name";
            dd_pemp3.DataValueField = "sup_id";
            dd_pemp3.DataBind();
            dd_pemp3.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            dd_ejen.DataSource = dt;
            dd_ejen.DataTextField = "sup_name";
            dd_ejen.DataValueField = "sup_id";
            dd_ejen.DataBind();
            dd_ejen.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {
        sel_mkat();
        dd_skat();

        if (DD_ketogri_Selection1.SelectedValue == "01")
        {
            GridView3.Visible = false;
            GridView2.Visible = true;
            GridView4.Visible = false;
            GridView5.Visible = false;
            grid_bot();
        }
        else if (DD_ketogri_Selection1.SelectedValue == "02")
        {
            GridView3.Visible = true;
            GridView2.Visible = false;
            GridView4.Visible = false;
            GridView5.Visible = false;
            grid_car();
        }
        else if (DD_ketogri_Selection1.SelectedValue == "03")

        {
            GridView3.Visible = false;
            GridView2.Visible = false;
            GridView4.Visible = true;
            GridView5.Visible = false;
            grid_inv();
        }
        else if (DD_ketogri_Selection1.SelectedValue == "04")
        {
            GridView3.Visible = false;
            GridView2.Visible = false;
            GridView4.Visible = false;
            GridView5.Visible = true;
            grid_pro();
        }
        grid();
    }

    void sel_mkat()
    {

        if (DD_ketogri_Selection1.SelectedValue == "01")
        {
            k1();
            sqclm_qry = "com";
            tbl_name = "ast_component";
        }
        else if (DD_ketogri_Selection1.SelectedValue == "02")
        {
            k2();
            sqclm_qry = "car";
            tbl_name = "ast_car";
        }
        else if (DD_ketogri_Selection1.SelectedValue == "03")
        {
            k3();
            sqclm_qry = "inv";
            tbl_name = "ast_inventory";
        }
        else if (DD_ketogri_Selection1.SelectedValue == "04")
        {
            k4();
            sqclm_qry = "pro";
            tbl_name = "ast_property";
        }
    }

    void dd_skat()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select ast_subkateast_Code,ast_subkateast_desc from Ref_ast_sub_kategri_Aset where Status='A' and ast_kategori_Code='" + DD_ketogri_Selection1.SelectedValue + "'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_SUBKATEG1.DataSource = dt;
            DD_SUBKATEG1.DataTextField = "ast_subkateast_desc";
            DD_SUBKATEG1.DataValueField = "ast_subkateast_Code";
            DD_SUBKATEG1.DataBind();
            DD_SUBKATEG1.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            DD_SUBKATEG2.DataSource = dt;
            DD_SUBKATEG2.DataTextField = "ast_subkateast_desc";
            DD_SUBKATEG2.DataValueField = "ast_subkateast_Code";
            DD_SUBKATEG2.DataBind();
            DD_SUBKATEG2.Items.Insert(0, new ListItem("--- PILIH ---", ""));


            DD_SUBKATEG3.DataSource = dt;
            DD_SUBKATEG3.DataTextField = "ast_subkateast_desc";
            DD_SUBKATEG3.DataValueField = "ast_subkateast_Code";
            DD_SUBKATEG3.DataBind();
            DD_SUBKATEG3.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            DD_SUBKATEG4.DataSource = dt;
            DD_SUBKATEG4.DataTextField = "ast_subkateast_desc";
            DD_SUBKATEG4.DataValueField = "ast_subkateast_Code";
            DD_SUBKATEG4.DataBind();
            DD_SUBKATEG4.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void sub_kategori()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select ast_subkateast_Code,ast_subkateast_desc from Ref_ast_sub_kategri_Aset where Status='A' ";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_SUBKATEG1.DataSource = dt;
            DD_SUBKATEG1.DataTextField = "ast_subkateast_desc";
            DD_SUBKATEG1.DataValueField = "ast_subkateast_Code";
            DD_SUBKATEG1.DataBind();
            DD_SUBKATEG1.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            DD_SUBKATEG2.DataSource = dt;
            DD_SUBKATEG2.DataTextField = "ast_subkateast_desc";
            DD_SUBKATEG2.DataValueField = "ast_subkateast_Code";
            DD_SUBKATEG2.DataBind();
            DD_SUBKATEG2.Items.Insert(0, new ListItem("--- PILIH ---", ""));


            DD_SUBKATEG3.DataSource = dt;
            DD_SUBKATEG3.DataTextField = "ast_subkateast_desc";
            DD_SUBKATEG3.DataValueField = "ast_subkateast_Code";
            DD_SUBKATEG3.DataBind();
            DD_SUBKATEG3.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            DD_SUBKATEG4.DataSource = dt;
            DD_SUBKATEG4.DataTextField = "ast_subkateast_desc";
            DD_SUBKATEG4.DataValueField = "ast_subkateast_Code";
            DD_SUBKATEG4.DataBind();
            DD_SUBKATEG4.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //void jenisaset()
    //{
    //    DataSet Ds = new DataSet();
    //    try
    //    {
    //        string com = "select ast_jeniaset_Code,ast_jeniaset_desc From Ref_ast_jenis_aset where Status='A'";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
    //        DataTable dt = new DataTable();
    //        adpt.Fill(dt);
    //        DD_JENISASET1.DataSource = dt;
    //        DD_JENISASET1.DataTextField = "ast_jeniaset_desc";
    //        DD_JENISASET1.DataValueField = "ast_jeniaset_Code";
    //        DD_JENISASET1.DataBind();
    //        DD_JENISASET1.Items.Insert(0, new ListItem("--- PILIH ---", ""));

    //        DD_JENISASET2.DataSource = dt;
    //        DD_JENISASET2.DataTextField = "ast_jeniaset_desc";
    //        DD_JENISASET2.DataValueField = "ast_jeniaset_Code";
    //        DD_JENISASET2.DataBind();
    //        DD_JENISASET2.Items.Insert(0, new ListItem("--- PILIH ---", ""));

    //        DD_JENISASET3.DataSource = dt;
    //        DD_JENISASET3.DataTextField = "ast_jeniaset_desc";
    //        DD_JENISASET3.DataValueField = "ast_jeniaset_Code";
    //        DD_JENISASET3.DataBind();
    //        DD_JENISASET3.Items.Insert(0, new ListItem("--- PILIH ---", ""));

    //        DD_JENISASET4.DataSource = dt;
    //        DD_JENISASET4.DataTextField = "ast_jeniaset_desc";
    //        DD_JENISASET4.DataValueField = "ast_jeniaset_Code";
    //        DD_JENISASET4.DataBind();
    //        DD_JENISASET4.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    protected void OnSelectedIndexChanged1(object sender, EventArgs e)
    {
        jenaset();
        grid();
        grid_bot();
    }

    protected void OnSelectedIndexChanged31(object sender, EventArgs e)
    {
        jenaset1();
        grid();
        grid_car();
    }

    protected void OnSelectedIndexChanged41(object sender, EventArgs e)
    {
        jenaset2();
        grid();
        grid_inv();
    }

    protected void OnSelectedIndexChanged51(object sender, EventArgs e)
    {
        jenaset3();
        grid();
        grid_pro();
    }

    void jenaset()
    {
        DataSet Ds1 = new DataSet();
        try
        {
            string com1 = "select * from Ref_ast_jenis_aset where Status='A' and ast_cat_Code='" + DD_ketogri_Selection1.SelectedValue + "' and ast_sub_cat_Code = '" + DD_SUBKATEG1.SelectedValue + "'";
            SqlDataAdapter adpt = new SqlDataAdapter(com1, con);
            DataTable dt1 = new DataTable();
            adpt.Fill(dt1);
            DD_JENISASET1.DataSource = dt1;
            DD_JENISASET1.DataTextField = "ast_jeniaset_desc";
            DD_JENISASET1.DataValueField = "ast_jeniaset_Code";
            DD_JENISASET1.DataBind();
            DD_JENISASET1.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void jenaset1()
    {
        DataSet Ds1 = new DataSet();
        try
        {
            string com1 = "select * from Ref_ast_jenis_aset where Status='A' and ast_cat_Code='" + DD_ketogri_Selection1.SelectedValue + "' and ast_sub_cat_Code = '" + DD_SUBKATEG2.SelectedValue + "'";
            SqlDataAdapter adpt = new SqlDataAdapter(com1, con);
            DataTable dt1 = new DataTable();
            adpt.Fill(dt1);
            DD_JENISASET2.DataSource = dt1;
            DD_JENISASET2.DataTextField = "ast_jeniaset_desc";
            DD_JENISASET2.DataValueField = "ast_jeniaset_Code";
            DD_JENISASET2.DataBind();
            DD_JENISASET2.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void jenaset2()
    {
        DataSet Ds1 = new DataSet();
        try
        {
            string com1 = "select * from Ref_ast_jenis_aset where Status='A' and ast_cat_Code='" + DD_ketogri_Selection1.SelectedValue + "' and ast_sub_cat_Code = '" + DD_SUBKATEG3.SelectedValue + "'";
            SqlDataAdapter adpt = new SqlDataAdapter(com1, con);
            DataTable dt1 = new DataTable();
            adpt.Fill(dt1);
            DD_JENISASET3.DataSource = dt1;
            DD_JENISASET3.DataTextField = "ast_jeniaset_desc";
            DD_JENISASET3.DataValueField = "ast_jeniaset_Code";
            DD_JENISASET3.DataBind();
            DD_JENISASET3.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void jenaset3()
    {
        DataSet Ds1 = new DataSet();
        try
        {
            string com1 = "select * from Ref_ast_jenis_aset where Status='A' and ast_cat_Code='" + DD_ketogri_Selection1.SelectedValue + "' and ast_sub_cat_Code = '" + DD_SUBKATEG4.SelectedValue + "'";
            SqlDataAdapter adpt = new SqlDataAdapter(com1, con);
            DataTable dt1 = new DataTable();
            adpt.Fill(dt1);
            DD_JENISASET4.DataSource = dt1;
            DD_JENISASET4.DataTextField = "ast_jeniaset_desc";
            DD_JENISASET4.DataValueField = "ast_jeniaset_Code";
            DD_JENISASET4.DataBind();
            DD_JENISASET4.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void OnSelectedIndexChanged2(object sender, EventArgs e)
    {
        cmnaset();
        grid();
        grid_bot();
    }

    protected void OnSelectedIndexChanged3(object sender, EventArgs e)
    {
        cmnaset1();
        grid();
        grid_car();
    }

    protected void OnSelectedIndexChanged4(object sender, EventArgs e)
    {
        cmnaset2();
        grid();
        grid_inv();
    }

    protected void OnSelectedIndexChanged5(object sender, EventArgs e)
    {
        cmnaset3();
        grid();
        grid_pro();
    }

    void cmnaset()
    {
        DataSet Ds1 = new DataSet();
        try
        {
            string com1 = "select * from ast_cmn_asset where cas_asset_cat_cd='" + DD_ketogri_Selection1.SelectedValue + "' and cas_asset_sub_cat_cd='" + DD_SUBKATEG1.SelectedValue + "' and cas_asset_type_cd='" + DD_JENISASET1.SelectedValue + "'";
            SqlDataAdapter adpt = new SqlDataAdapter(com1, con);
            DataTable dt1 = new DataTable();
            adpt.Fill(dt1);
            DropDownList2.DataSource = dt1;
            DropDownList2.DataTextField = "cas_asset_desc";
            DropDownList2.DataValueField = "cas_asset_cd";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void cmnaset1()
    {
        DataSet Ds1 = new DataSet();
        try
        {
            string com1 = "select * from ast_cmn_asset where cas_asset_cat_cd='" + DD_ketogri_Selection1.SelectedValue + "' and cas_asset_sub_cat_cd='" + DD_SUBKATEG2.SelectedValue + "' and cas_asset_type_cd='" + DD_JENISASET2.SelectedValue + "'";
            SqlDataAdapter adpt = new SqlDataAdapter(com1, con);
            DataTable dt1 = new DataTable();
            adpt.Fill(dt1);

            DropDownList21.DataSource = dt1;
            DropDownList21.DataTextField = "cas_asset_desc";
            DropDownList21.DataValueField = "cas_asset_cd";
            DropDownList21.DataBind();
            DropDownList21.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void cmnaset2()
    {
        DataSet Ds1 = new DataSet();
        try
        {
            string com1 = "select * from ast_cmn_asset where cas_asset_cat_cd='" + DD_ketogri_Selection1.SelectedValue + "' and cas_asset_sub_cat_cd='" + DD_SUBKATEG3.SelectedValue + "' and cas_asset_type_cd='" + DD_JENISASET3.SelectedValue + "'";
            SqlDataAdapter adpt = new SqlDataAdapter(com1, con);
            DataTable dt1 = new DataTable();
            adpt.Fill(dt1);
            DropDownList22.DataSource = dt1;
            DropDownList22.DataTextField = "cas_asset_desc";
            DropDownList22.DataValueField = "cas_asset_cd";
            DropDownList22.DataBind();
            DropDownList22.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void cmnaset3()
    {
        DataSet Ds1 = new DataSet();
        try
        {
            string com1 = "select * from ast_cmn_asset where cas_asset_cat_cd='" + DD_ketogri_Selection1.SelectedValue + "' and cas_asset_sub_cat_cd='" + DD_SUBKATEG4.SelectedValue + "' and cas_asset_type_cd='" + DD_JENISASET4.SelectedValue + "'";
            SqlDataAdapter adpt = new SqlDataAdapter(com1, con);
            DataTable dt1 = new DataTable();
            adpt.Fill(dt1);
            DropDownList4.DataSource = dt1;
            DropDownList4.DataTextField = "cas_asset_desc";
            DropDownList4.DataValueField = "cas_asset_cd";
            DropDownList4.DataBind();
            DropDownList4.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

  
    protected void rst_Click(object sender, EventArgs e)
    {
        Response.Redirect("AST_DAFTER_ASET.aspx");
    }



    void grid()
    {
        con.Open();
        //SqlCommand cmd = new SqlCommand("select AC.acp_po_no,AC.acp_do_no,AC.acp_seq_no,ast_kategori_desc,ast_subkateast_desc,ast_jeniaset_desc,cas_asset_desc,(CASE WHEN acp_receive_upd_dt IS NULL or acp_receive_upd_dt='1900-01-01' THEN  acp_receive_dt ELSE acp_receive_upd_dt END)  acp_receive_upd_dt from ast_acceptance as AC left join Ref_ast_kategori as KE on KE.ast_kategori_Code=AC.acp_asset_cat_cd  left join Ref_ast_jenis_aset as JE on JE.ast_jeniaset_Code=AC.acp_asset_type_cd left join ast_cmn_asset as KO on KO.cas_asset_cd=AC.acp_asset_cd and ko.cas_asset_cat_cd=ac.acp_asset_cat_cd and KO.cas_asset_sub_cat_cd=ac.acp_asset_sub_cat_cd and ko.cas_asset_type_cd=ac.acp_asset_type_cd left join Ref_ast_sub_kategri_Aset as SKE on SKE.ast_subkateast_Code=AC.acp_asset_sub_cat_cd where acp_po_no='" + txt_nopo.Text + "'", con);//06_04_17
        SqlCommand cmd = new SqlCommand("select KE.ast_kategori_desc,SKE.ast_subkateast_desc,JE.ast_jeniaset_desc,KO.cas_asset_desc,acp_receive_upd_dt,acp_asset_cat_cd,acp_asset_sub_cat_cd,acp_asset_type_cd,acp_asset_cd,acp_seq_no,acp_receive_qty,acp_all_qty,(acp_receive_qty - acp_all_qty) as q1,acp_do_no from (select acp_asset_cat_cd,acp_asset_sub_cat_cd,acp_asset_type_cd,acp_asset_cd,(CASE WHEN acp_receive_upd_dt IS NULL or acp_receive_upd_dt='1900-01-01' THEN  acp_receive_dt ELSE acp_receive_upd_dt END)  acp_receive_upd_dt,acp_seq_no,acp_receive_qty,acp_all_qty,acp_do_no from ast_acceptance where acp_po_no='" + txt_nopo.Text + "' and acp_verify_sts_cd = 'S' and acp_receive_qty!=acp_all_qty  group by acp_asset_cat_cd,acp_asset_sub_cat_cd,acp_asset_type_cd,acp_asset_cd,acp_receive_upd_dt,acp_receive_dt,acp_seq_no,acp_receive_qty,acp_all_qty,acp_do_no) as a left join Ref_ast_kategori as KE on KE.ast_kategori_Code=a.acp_asset_cat_cd  left join Ref_ast_jenis_aset as JE on JE.ast_jeniaset_Code=a.acp_asset_type_cd and JE.ast_sub_cat_Code=a.acp_asset_sub_cat_cd and JE.ast_cat_Code=a.acp_asset_cat_cd left join ast_cmn_asset as KO on KO.cas_asset_cd=a.acp_asset_cd and ko.cas_asset_cat_cd=a.acp_asset_cat_cd and KO.cas_asset_sub_cat_cd=a.acp_asset_sub_cat_cd and ko.cas_asset_type_cd=a.acp_asset_type_cd left join Ref_ast_sub_kategri_Aset as SKE on SKE.ast_subkateast_Code=a.acp_asset_sub_cat_cd", con);
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
        }
        else
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        con.Close();
        //rcorners1.Visible = false;
        //rcorners2.Visible = false;
        //rcorners3.Visible = false;
        //rcorners4.Visible = false;
        //rcorners5.Visible = false;

    }

    void k1()
    {
        rcorners1.Visible = true;
        rcorners2.Visible = false;
        rcorners3.Visible = false;
        rcorners4.Visible = false;
        //rcorners5.Visible = false;
        btnsimpan.Visible = true;
    }

    void k2()
    {
        rcorners1.Visible = false;
        rcorners2.Visible = true;
        btnsimpan.Visible = true;
        rcorners3.Visible = false;
        rcorners4.Visible = false;
        // rcorners5.Visible = false;
    }

    void k3()
    {
        rcorners1.Visible = false;
        rcorners2.Visible = false;
        rcorners3.Visible = true;
        btnsimpan.Visible = true;
        rcorners4.Visible = false;
        //rcorners5.Visible = false;
    }

    void k4()
    {
        rcorners1.Visible = false;
        rcorners2.Visible = false;
        rcorners3.Visible = false;
        rcorners4.Visible = true;
        btnsimpan.Visible = true;
        //rcorners5.Visible = true;
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {

        dd_pemp1.Attributes.Add("style", "pointer-events:None;");
        dd_pemp2.Attributes.Add("style", "pointer-events:None;");
        dd_pemp3.Attributes.Add("style", "pointer-events:None;");
        DD_ketogri_Selection1.Attributes.Add("style", "Pointer-events:none;");



        LinkButton btnButton = sender as LinkButton;
        GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
        System.Web.UI.WebControls.Label sq_no = (System.Web.UI.WebControls.Label)gvRow.FindControl("Label9");
        //System.Web.UI.WebControls.Label po_no = (System.Web.UI.WebControls.Label)gvRow.FindControl("Label2");
        System.Web.UI.WebControls.Label do_no = (System.Web.UI.WebControls.Label)gvRow.FindControl("Label4_do");
        //string abc = lblTitle.Text;
        DataTable ddokdicno1 = new DataTable();
        //ddokdicno1 = DBCon.Ora_Execute_table("select acp_asset_cat_cd,acp_asset_sub_cat_cd,acp_asset_type_cd,acp_asset_cd,acp_supplier_id,aca.cas_asset_desc From ast_acceptance as AA left join ast_cmn_asset aca on aca.cas_asset_cat_cd=AA.acp_asset_cat_cd and  aca.cas_asset_sub_cat_cd=AA.acp_asset_sub_cat_cd and aca.cas_asset_cd=AA.acp_asset_cd   where acp_po_no='" + txt_nopo.Text + "' and acp_do_no='"+ do_no.Text +"' and acp_seq_no='"+ sq_no.Text +"'");
        ddokdicno1 = DBCon.Ora_Execute_table("select acp_asset_cat_cd,acp_asset_sub_cat_cd,acp_asset_type_cd,acp_asset_cd,format(acp_receive_upd_dt,'dd/MM/yyyy')  as acp_receive_upd_dt,acp_seq_no,format(ap.pur_approve_dt,'dd/MM/yyyy') as pur_approve_dt,pur_asset_amt,pur_asset_tot_amt,KO.cas_asset_desc,as1.sup_name,acp_supplier_id,(acp_receive_qty - acp_all_qty) acp_receive_qty from (select acp_po_no,acp_asset_cat_cd,acp_asset_sub_cat_cd,acp_asset_type_cd,acp_asset_cd,(CASE WHEN acp_receive_upd_dt IS NULL or acp_receive_upd_dt='1900-01-01' THEN  acp_receive_dt ELSE acp_receive_upd_dt END)  acp_receive_upd_dt,acp_seq_no,acp_supplier_id,acp_receive_qty,acp_all_qty from ast_acceptance where acp_po_no='" + txt_nopo.Text + "' and acp_do_no='" + do_no.Text + "' and acp_seq_no='" + sq_no.Text + "' group by acp_po_no,acp_asset_cat_cd,acp_asset_sub_cat_cd,acp_asset_type_cd,acp_asset_cd,acp_receive_upd_dt,acp_receive_dt,acp_seq_no,acp_supplier_id,acp_receive_qty,acp_all_qty) as a left join ast_cmn_asset as KO on KO.cas_asset_cd=a.acp_asset_cd and ko.cas_asset_cat_cd=a.acp_asset_cat_cd and KO.cas_asset_sub_cat_cd=a.acp_asset_sub_cat_cd and ko.cas_asset_type_cd=a.acp_asset_type_cd left join ast_purchase ap on ap.pur_po_no=a.acp_po_no and ap.pur_asset_id=a.acp_seq_no left join ast_supplier as1 on as1.sup_id=a.acp_supplier_id");
        if (ddokdicno1.Rows.Count != 0)
        {
            DD_ketogri_Selection1.SelectedValue = ddokdicno1.Rows[0]["acp_asset_cat_cd"].ToString().Trim();
            if (DD_ketogri_Selection1.SelectedValue == "01")
            {
                c001.Attributes.Add("style", "Pointer-events:none;");
                k1();
                //rcorners3.Attributes.Remove("style");
                dd_skat();
                DD_SUBKATEG1.SelectedValue = ddokdicno1.Rows[0]["acp_asset_sub_cat_cd"].ToString().Trim();
                jenaset();
                DD_JENISASET1.SelectedValue = ddokdicno1.Rows[0]["acp_asset_type_cd"].ToString();
                pemp_view();
                dd_pemp1.SelectedValue = ddokdicno1.Rows[0]["acp_supplier_id"].ToString().Trim();
                cmnaset();
                DropDownList2.SelectedValue = ddokdicno1.Rows[0]["acp_asset_cd"].ToString();
                txt_hatga1.Text = double.Parse(ddokdicno1.Rows[0]["pur_asset_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                txt_tdibeli.Text = ddokdicno1.Rows[0]["pur_approve_dt"].ToString();
                txt_tditer.Text = ddokdicno1.Rows[0]["acp_receive_upd_dt"].ToString();
                TextBox6.Text = ddokdicno1.Rows[0]["acp_receive_qty"].ToString();
                GridView3.Visible = false;
                GridView4.Visible = false;
                GridView2.Visible = true;
                grid_bot();
            }
            else if (DD_ketogri_Selection1.SelectedValue == "02")
            {
                //rcorners3.Attributes.Remove("style");
                hm001.Attributes.Add("style", "Pointer-events:none;");
                k2();
                dd_skat();
                DD_SUBKATEG2.SelectedValue = ddokdicno1.Rows[0]["acp_asset_sub_cat_cd"].ToString().Trim();
                jenaset1();
                DD_JENISASET2.SelectedValue = ddokdicno1.Rows[0]["acp_asset_type_cd"].ToString();
                cmnaset1();
                DropDownList21.SelectedValue = ddokdicno1.Rows[0]["acp_asset_cd"].ToString();
                pemp_view();
                dd_pemp2.SelectedValue = ddokdicno1.Rows[0]["acp_supplier_id"].ToString().Trim();
                txt_haraga2.Text = double.Parse(ddokdicno1.Rows[0]["pur_asset_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                txt_debil2.Text = ddokdicno1.Rows[0]["pur_approve_dt"].ToString();
                txt_diterima2.Text = ddokdicno1.Rows[0]["acp_receive_upd_dt"].ToString();
                TextBox7.Text = ddokdicno1.Rows[0]["acp_receive_qty"].ToString();
                GridView3.Visible = true;
                GridView2.Visible = false;
                GridView4.Visible = false;
                grid_car();
            }
            else if (DD_ketogri_Selection1.SelectedValue == "03")
            {
                //rcorners3.Attributes.Remove("style");
                iv001.Attributes.Add("style", "Pointer-events:none;");
                k3();
                dd_skat();
                DD_SUBKATEG3.SelectedValue = ddokdicno1.Rows[0]["acp_asset_sub_cat_cd"].ToString().Trim();
                jenaset2();
                DD_JENISASET3.SelectedValue = ddokdicno1.Rows[0]["acp_asset_type_cd"].ToString();
                cmnaset2();
                DropDownList22.SelectedValue = ddokdicno1.Rows[0]["acp_asset_cd"].ToString();
                pemp_view();
                dd_pemp3.SelectedValue = ddokdicno1.Rows[0]["acp_supplier_id"].ToString().Trim();
                txt_hargaper3.Text = double.Parse(ddokdicno1.Rows[0]["pur_asset_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                txt_debeli3.Text = ddokdicno1.Rows[0]["pur_approve_dt"].ToString();
                txt_diterima3.Text = ddokdicno1.Rows[0]["acp_receive_upd_dt"].ToString();
                TextBox8.Text = ddokdicno1.Rows[0]["acp_receive_qty"].ToString();
                GridView3.Visible = false;
                GridView2.Visible = false;
                GridView4.Visible = true;
                grid_inv();
            }
            else if (DD_ketogri_Selection1.SelectedValue == "04")
            {
                //rcorners3.Attributes.Remove("style");
                //pp001.Attributes.Add("style", "Pointer-events:none;");
                k4();
                dd_skat();
                DD_SUBKATEG4.SelectedValue = ddokdicno1.Rows[0]["acp_asset_sub_cat_cd"].ToString().Trim();
                jenaset3();
                DD_JENISASET4.SelectedValue = ddokdicno1.Rows[0]["acp_asset_type_cd"].ToString().Trim();
                //txt_nama_aset.Text = ddokdicno1.Rows[0]["acp_asset_cd"].ToString().Trim();
                cmnaset3();
                DropDownList4.SelectedValue = ddokdicno1.Rows[0]["acp_asset_cd"].ToString();
                txt_pembekal4.Text = ddokdicno1.Rows[0]["sup_name"].ToString().Trim();
                TextBox9.Text = ddokdicno1.Rows[0]["acp_receive_qty"].ToString();
                grid_pro();
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }


    protected void Simpan_Click1(object sender, EventArgs e)
    {
        try
        {
            //if(txt_nopo.Text != "")
            //{
            string kategId = DD_ketogri_Selection1.SelectedValue;
            DataTable ddicno = new DataTable();
            ddicno = DBCon.Ora_Execute_table("select acp_do_no from ast_acceptance where acp_po_no='" + txt_nopo.Text.Trim() + "' ");
            //if (ddicno.Rows.Count > 0)
            //{
            string dono = string.Empty;
            if (ddicno.Rows.Count != 0)
            {
                dono = ddicno.Rows[0]["acp_do_no"].ToString();
            }
            if (kategId == "01")
            {
                if (DD_ketogri_Selection1.SelectedValue != "" && DD_SUBKATEG1.SelectedValue != "" && DD_JENISASET1.SelectedValue != "" && DropDownList2.SelectedValue != "")
                {
                    if (txt_hatga1.Text != "")
                    {
                        if (txt_tdibeli.Text != "")
                        {
                            if (txt_tditer.Text != "")
                            {
                                if (txt_nosiri.Text != "")
                                {
                                    if (txt_kodprod.Text != "")
                                    {
                                        if (txt_jenamodel.Text != "")
                                        {
                                            DataTable dtast = new DataTable();
                                            dtast = DBCon.Ora_Execute_table("select COUNT(*) from ast_component");
                                            string ccode = string.Empty;
                                            ccode = DD_ketogri_Selection1.SelectedValue.Trim() + "" + DD_SUBKATEG1.SelectedValue.Trim() + "" + DD_JENISASET1.SelectedValue.Trim();

                                            object count1 = (Convert.ToInt32(dtast.Rows[0][0].ToString()) + 1);
                                            string snp = count1.ToString();
                                            var count = ccode + snp.PadLeft(6, '0');
                                            string as_id = string.Empty;
                                            as_id = count;
                                            DataTable ddaset_katid = new DataTable();
                                            ddaset_katid = DBCon.Ora_Execute_table("select * from ast_component where com_asset_id='" + as_id + "'");
                                            if (ddaset_katid.Rows.Count == 0)
                                            {

                                                SqlCommand ins_prof = new SqlCommand("insert into ast_component(com_asset_id,com_asset_cat_cd,com_asset_sub_cat_cd,com_asset_type_cd,com_asset_cd,com_reg_dt,com_po_no,com_do_no,com_gain_type_cd,com_price_amt,com_buy_dt,com_receive_dt,com_serial_no,com_prod_no,com_brand,com_warranty,com_renewal_ind,com_renewal_amt,com_supplier_id,com_crt_id,com_crt_dt,com_org_id,com_sas_id,flag_set) values(@com_asset_id,@com_asset_cat_cd,@com_asset_sub_cat_cd,@com_asset_type_cd,@com_asset_cd,@com_reg_dt,@com_po_no,@com_do_no,@com_gain_type_cd,@com_price_amt,@com_buy_dt,@com_receive_dt,@com_serial_no,@com_prod_no,@com_brand,@com_warranty,@com_renewal_ind,@com_renewal_amt,@com_supplier_id,@com_crt_id,@com_crt_dt,@com_org_id,@com_sas_id,@flag_set)", con);
                                                string datedari = string.Empty, datedari1 = string.Empty, fromdate = string.Empty, todate = string.Empty;
                                                if (txt_tdibeli.Text != "")
                                                {
                                                    datedari = txt_tdibeli.Text;
                                                    DateTime dt = DateTime.ParseExact(datedari, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                                    fromdate = dt.ToString("yyyy-MM-dd");
                                                }
                                                ins_prof.Parameters.AddWithValue("@com_buy_dt", fromdate);
                                                if (txt_tditer.Text != "")
                                                {
                                                    datedari1 = txt_tditer.Text;
                                                    DateTime dt1 = DateTime.ParseExact(datedari1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                                    todate = dt1.ToString("yyyy-MM-dd");
                                                }


                                                ins_prof.Parameters.AddWithValue("@com_asset_id", as_id.ToString());
                                                ins_prof.Parameters.AddWithValue("@com_asset_cat_cd", DD_ketogri_Selection1.SelectedValue);
                                                ins_prof.Parameters.AddWithValue("@com_asset_sub_cat_cd", DD_SUBKATEG1.SelectedValue);
                                                ins_prof.Parameters.AddWithValue("@com_asset_type_cd", DD_JENISASET1.SelectedValue);
                                                string sb = null;
                                                if (ckbox_perlu.Checked)
                                                {
                                                    sb = "1";
                                                }
                                                else
                                                {
                                                    sb = "0";
                                                }
                                                string ckbox1 = sb.ToString();
                                                ins_prof.Parameters.AddWithValue("@com_renewal_ind", ckbox1.ToString());

                                                //DataTable ddicno2 = new DataTable();
                                                //ddicno2 = DBCon.Ora_Execute_table("select cas_asset_cd From ast_cmn_asset where cas_asset_desc like '%" + txt_namaast1.Text + "%'");
                                                //string def = ddicno2.Rows[0]["cas_asset_cd"].ToString();
                                                ins_prof.Parameters.AddWithValue("@com_asset_cd", DropDownList2.SelectedValue);//txt_namaast1
                                                ins_prof.Parameters.AddWithValue("@com_reg_dt", DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"));
                                                ins_prof.Parameters.AddWithValue("@com_po_no", txt_nopo.Text);
                                                ins_prof.Parameters.AddWithValue("@com_do_no", dono.ToString());
                                                ins_prof.Parameters.AddWithValue("@com_gain_type_cd", DD_Perolehan.SelectedValue);
                                                ins_prof.Parameters.AddWithValue("@com_price_amt", txt_hatga1.Text.ToString());

                                                ins_prof.Parameters.AddWithValue("@com_receive_dt", todate);
                                                ins_prof.Parameters.AddWithValue("@com_serial_no", txt_nosiri.Text);
                                                ins_prof.Parameters.AddWithValue("@com_prod_no", txt_kodprod.Text);
                                                ins_prof.Parameters.AddWithValue("@com_brand", txt_jenamodel.Text);
                                                ins_prof.Parameters.AddWithValue("@com_warranty", txt_jaminan.Text);
                                                ins_prof.Parameters.AddWithValue("@com_renewal_amt", txt_kosperb.Text.ToString());
                                                ins_prof.Parameters.AddWithValue("@com_supplier_id", dd_pemp1.SelectedValue);
                                                ins_prof.Parameters.AddWithValue("@com_crt_id", Session["New"].ToString());
                                                ins_prof.Parameters.AddWithValue("@com_crt_dt", DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"));
                                                ins_prof.Parameters.AddWithValue("@com_org_id", DropDownList1.SelectedValue);
                                                ins_prof.Parameters.AddWithValue("@com_sas_id", "0");
                                                ins_prof.Parameters.AddWithValue("@flag_set", "0");

                                                con.Open();
                                                int i = ins_prof.ExecuteNonQuery();
                                                con.Close();
                                                DataTable dtena = new DataTable();

                                                dtena = DBCon.Ora_Execute_table("select acp_all_qty from ast_acceptance where acp_asset_cat_cd='" + DD_ketogri_Selection1.SelectedValue + "' and acp_asset_sub_cat_cd='" + DD_SUBKATEG1.SelectedValue + "' and acp_asset_type_cd='" + DD_JENISASET1.SelectedValue + "' and acp_po_no='" + txt_nopo.Text + "' and acp_asset_cd='" + DropDownList2.SelectedValue + "' and acp_do_no='" + dono.ToString() + "'");
                                                if (dtena.Rows.Count != 0)
                                                {
                                                    int sno = (Convert.ToInt32(dtena.Rows[0][0].ToString()) + 1);
                                                    DataTable dtacp = new DataTable();
                                                    dtacp = DBCon.Ora_Execute_table("update ast_acceptance set acp_all_qty='" + sno + " ' where acp_asset_cat_cd='" + DD_ketogri_Selection1.SelectedValue + "' and acp_asset_sub_cat_cd='" + DD_SUBKATEG1.SelectedValue + "' and acp_asset_type_cd='" + DD_JENISASET1.SelectedValue + "' and acp_po_no='" + txt_nopo.Text + "' and acp_asset_cd='" + DropDownList2.SelectedValue + "' and acp_do_no='" + dono.ToString() + "'");
                                                }
                                                if (1 >= 0)
                                                {
                                                    //Simpan.Visible = false;
                                                    grid();
                                                    grid_bot();
                                                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                                                    clear();
                                                }
                                                
                                            }
                                            else
                                            {
                                                grid();
                                                grid_bot();
                                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekord Sudah Wujud.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                                            }
                                        }
                                        else
                                        {
                                            grid_bot();
                                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Jenama & Model.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                                        }
                                    }
                                    else
                                    {
                                        grid_bot();
                                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Kod Produk.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                                    }
                                }
                                else
                                {
                                    grid_bot();
                                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan No Siri.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                                }
                            }
                            else
                            {
                                grid_bot();
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Tarikh Diterima.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                            }
                        }
                        else
                        {
                            grid_bot();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Tarikh Dibeli.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                        }
                    }
                    else
                    {
                        grid_bot();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Harga Perolehan / Per unit (RM).',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    grid();
                    grid_bot();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }

            }
            else if (kategId == "02")
            {
                if (DD_ketogri_Selection1.Text != "" && DD_SUBKATEG2.SelectedValue != "" && DD_JENISASET2.SelectedValue != "" && DropDownList21.SelectedValue != "")
                {
                    if (txt_haraga2.Text != "")
                    {
                        if (txt_debil2.Text != "")
                        {
                            if (txt_diterima2.Text != "")
                            {
                                if (DD_Model2.SelectedValue != "")
                                {
                                    //if (txt_nocasis2.Text != "")
                                    //{
                                    //    if (txt_platno2.Text != "")
                                    //    {
                                            DataTable dtast = new DataTable();
                                            dtast = DBCon.Ora_Execute_table("select COUNT(*) from ast_car");

                                            string inv_id = string.Empty;
                                            inv_id = DD_ketogri_Selection1.SelectedValue.Trim() + "" + DD_SUBKATEG2.SelectedValue.Trim() + "" + DD_JENISASET2.SelectedValue.Trim();

                                            object count1 = (Convert.ToInt32(dtast.Rows[0][0].ToString()) + 1);
                                            string snp = count1.ToString();
                                            var count = inv_id + snp.PadLeft(6, '0');
                                            string as_id = string.Empty;
                                            as_id = count;

                                            DataTable ddicno_car = new DataTable();
                                            ddicno_car = DBCon.Ora_Execute_table("select * from ast_car where car_asset_id='" + as_id + "'");

                                            if (ddicno_car.Rows.Count == 0)
                                            {
                                                string datedari = string.Empty, datedari1 = string.Empty, fromdate = string.Empty, fromdate2 = string.Empty, su_id1 = string.Empty;
                                                //DataTable ddicno2 = new DataTable();
                                                //ddicno2 = DBCon.Ora_Execute_table("select cas_asset_cd From ast_cmn_asset where cas_asset_desc like '%" + txt_namaast2.Text + "%'");
                                                if (txt_jaminan2.Text == "")
                                                {
                                                    su_id1 = "0";
                                                }
                                                else
                                                {
                                                    su_id1 = txt_jaminan2.Text;
                                                }

                                                SqlCommand ins_prof = new SqlCommand("insert into ast_car(car_asset_id,car_asset_cat_cd,car_asset_sub_cat_cd,car_asset_type_cd,car_asset_cd,car_reg_dt,car_po_no,car_do_no,car_buy_dt,car_receive_dt,car_price_amt,car_brand_cd,car_model_cd,car_fuel_type_cd,car_engine_cpcty,car_usage_class,car_color,car_made_yr,car_vehicle_type_cd,car_casis_no,car_engine_no,car_warranty_yr ,car_plate_no,car_maintenance_ind,car_supplier_id,car_crt_id,car_crt_dt,car_org_id,car_sas_id,flag_set) values(@car_asset_id,@car_asset_cat_cd,@car_asset_sub_cat_cd,@car_asset_type_cd,@car_asset_cd,@car_reg_dt,@car_po_no,@car_do_no,@car_buy_dt,@car_receive_dt,@car_price_amt,@car_brand_cd,@car_model_cd,@car_fuel_type_cd,@car_engine_cpcty,@car_usage_class,@car_color,@car_made_yr,@car_vehicle_type_cd,@car_casis_no,@car_engine_no,@car_warranty_yr ,@car_plate_no,@car_maintenance_ind,@car_supplier_id,@car_crt_id,@car_crt_dt,@car_org_id,@car_sas_id,@flag_set)", con);
                                                ins_prof.Parameters.AddWithValue("@car_asset_id", as_id);
                                                ins_prof.Parameters.AddWithValue("@car_asset_cat_cd", DD_ketogri_Selection1.SelectedValue);
                                                ins_prof.Parameters.AddWithValue("@car_asset_sub_cat_cd", DD_SUBKATEG2.SelectedValue);
                                                ins_prof.Parameters.AddWithValue("@car_asset_type_cd", DD_JENISASET2.SelectedValue);
                                                ins_prof.Parameters.AddWithValue("@car_asset_cd", DropDownList21.SelectedValue);
                                                ins_prof.Parameters.AddWithValue("@car_reg_dt", DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"));
                                                ins_prof.Parameters.AddWithValue("@car_po_no", txt_nopo.Text);
                                                ins_prof.Parameters.AddWithValue("@car_do_no", dono.ToString());
                                                if (txt_debil2.Text != "")
                                                {
                                                    datedari = txt_debil2.Text;
                                                    DateTime dt = DateTime.ParseExact(datedari, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                                    fromdate = dt.ToString("yyyy-MM-dd");
                                                }
                                                ins_prof.Parameters.AddWithValue("@car_buy_dt", fromdate);
                                                if (txt_diterima2.Text != "")
                                                {
                                                    datedari1 = txt_diterima2.Text;
                                                    DateTime dt1 = DateTime.ParseExact(datedari, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                                    fromdate2 = dt1.ToString("yyyy-MM-dd");
                                                }
                                                ins_prof.Parameters.AddWithValue("@car_receive_dt", fromdate2);
                                                ins_prof.Parameters.AddWithValue("@car_price_amt", txt_haraga2.Text);
                                                ins_prof.Parameters.AddWithValue("@car_brand_cd", DD_JENAMA2.SelectedValue);
                                                ins_prof.Parameters.AddWithValue("@car_model_cd", DD_Model2.SelectedValue);
                                                ins_prof.Parameters.AddWithValue("@car_fuel_type_cd", DD_Bahan2.SelectedValue);
                                                ins_prof.Parameters.AddWithValue("@car_engine_cpcty", txt_keupayaan2.Text);
                                                ins_prof.Parameters.AddWithValue("@car_usage_class", txt_kelaskeg2.Text);
                                                ins_prof.Parameters.AddWithValue("@car_color", txt_warna2.Text);
                                                ins_prof.Parameters.AddWithValue("@car_made_yr", txt_tahun2.Text);
                                                ins_prof.Parameters.AddWithValue("@car_vehicle_type_cd", DD_jeniskend2.SelectedValue);
                                                ins_prof.Parameters.AddWithValue("@car_casis_no", txt_nocasis2.Text);
                                                ins_prof.Parameters.AddWithValue("@car_engine_no", txt_jenisdan2.Text);
                                                ins_prof.Parameters.AddWithValue("@car_warranty_yr", su_id1);
                                                ins_prof.Parameters.AddWithValue("@car_plate_no", txt_platno2.Text);
                                                string sb = null;
                                                if (CheckBox1.Checked)
                                                {
                                                    sb = "1";
                                                }
                                                else
                                                {
                                                    sb = "0";
                                                }
                                                string ckbox1 = sb.ToString();
                                                ins_prof.Parameters.AddWithValue("@car_maintenance_ind", ckbox1);
                                                ins_prof.Parameters.AddWithValue("@car_supplier_id", dd_pemp2.SelectedValue);

                                                ins_prof.Parameters.AddWithValue("@car_crt_id", Session["New"].ToString());
                                                ins_prof.Parameters.AddWithValue("@car_crt_dt", DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"));
                                                ins_prof.Parameters.AddWithValue("@car_org_id", DropDownList1.SelectedValue);
                                                ins_prof.Parameters.AddWithValue("@car_sas_id", "0");
                                                ins_prof.Parameters.AddWithValue("@flag_set", "0");

                                                con.Open();
                                                int i = ins_prof.ExecuteNonQuery();
                                                con.Close();
                                                DataTable dtena = new DataTable();
                                                dtena = DBCon.Ora_Execute_table("select acp_all_qty from ast_acceptance where acp_asset_cat_cd='" + DD_ketogri_Selection1.SelectedValue + "' and acp_asset_sub_cat_cd='" + DD_SUBKATEG2.SelectedValue + "' and acp_asset_type_cd='" + DD_JENISASET2.SelectedValue + "' and acp_po_no='" + txt_nopo.Text + "' and acp_asset_cd='" + DropDownList21.SelectedValue + "' and acp_do_no='" + dono.ToString() + "'");
                                                if (dtena.Rows.Count != 0)
                                                {
                                                    int sno = (Convert.ToInt32(dtena.Rows[0][0].ToString()) + 1);
                                                    DataTable dtacp = new DataTable();
                                                    dtacp = DBCon.Ora_Execute_table("update ast_acceptance set acp_all_qty='" + sno + " ' where acp_asset_cat_cd='" + DD_ketogri_Selection1.SelectedValue + "' and acp_asset_sub_cat_cd='" + DD_SUBKATEG2.SelectedValue + "' and acp_asset_type_cd='" + DD_JENISASET2.SelectedValue + "' and acp_po_no='" + txt_nopo.Text + "' and acp_asset_cd='" + DropDownList21.SelectedValue + "' and acp_do_no='" + dono.ToString() + "'");
                                                }
                                                if (1 >= 0)
                                                {
                                                    //Simpan.Visible = false;
                                                    grid();
                                                    grid_car();
                                                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                                                    clear();
                                                }
                                            }
                                            else
                                            {
                                                grid();
                                                grid_car();
                                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Sudah Wujud.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                                            }
                                    //    }
                                    //    else
                                    //    {
                                    //        grid_car();
                                    //        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan No Plat.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    grid_car();
                                    //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan No Casis.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                                    //}
                                }
                                else
                                {
                                    grid_car();
                                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Model.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                                }
                            }
                            else
                            {
                                grid_car();
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Tarikh Diterima.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                            }
                        }
                        else
                        {
                            grid_car();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Tarikh Dibeli.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                        }
                    }
                    else
                    {
                        grid_car();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Harga Perolehan (RM).',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    grid();
                    grid_car();                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else if (kategId == "03")
            {
                string datedari = string.Empty, datedari1 = string.Empty, fromdate = string.Empty, fromdate2 = string.Empty, su_id1 = string.Empty;
                if (DD_ketogri_Selection1.SelectedValue != "" && DD_SUBKATEG3.SelectedValue != "" && DD_JENISASET3.SelectedValue != "" && DropDownList22.SelectedValue != "")
                {
                    if (txt_hargaper3.Text != "")
                    {
                        if (txt_debeli3.Text != "")
                        {
                            if (txt_diterima3.Text != "")
                            {
                                if (txt_kuantiti3.Text != "")
                                {
                                    DataTable dtast = new DataTable();
                                    dtast = DBCon.Ora_Execute_table("select COUNT(*) from ast_inventory");

                                    string in_id = string.Empty;
                                    in_id = DD_ketogri_Selection1.SelectedValue.Trim() + "" + DD_SUBKATEG3.SelectedValue.Trim() + "" + DD_JENISASET3.SelectedValue.Trim();

                                    object count1 = (Convert.ToInt32(dtast.Rows[0][0].ToString()) + 1);
                                    string snp = count1.ToString();
                                    var count = in_id + snp.PadLeft(6, '0');
                                    string as_id = string.Empty;
                                    as_id = count;
                                    DataTable ddicno2_inv = new DataTable();
                                    ddicno2_inv = DBCon.Ora_Execute_table("select * from ast_inventory where inv_asset_id='" + as_id + "'");
                                    if (ddicno2_inv.Rows.Count == 0)
                                    {

                                        SqlCommand ins_prof = new SqlCommand("insert into ast_inventory (inv_asset_id,inv_asset_cat_cd,inv_asset_sub_cat_cd,inv_asset_type_cd,inv_asset_cd,inv_reg_dt,inv_price_amt,inv_qty,inv_warranty,inv_buy_dt,inv_receive_dt,inv_remark,inv_po_no,inv_do_no,inv_supplier_id,inv_crt_id,inv_crt_dt,inv_org_id,inv_sas_id,flag_set) values(@inv_asset_id,@inv_asset_cat_cd,@inv_asset_sub_cat_cd,@inv_asset_type_cd,@inv_asset_cd,@inv_reg_dt,@inv_price_amt,@inv_qty,@inv_warranty,@inv_buy_dt,@inv_receive_dt,@inv_remark,@inv_po_no,@inv_do_no,@inv_supplier_id,@inv_crt_id,@inv_crt_dt,@inv_org_id,@inv_sas_id,@flag_set)", con);
                                        ins_prof.Parameters.AddWithValue("@inv_asset_id", as_id);
                                        ins_prof.Parameters.AddWithValue("@inv_asset_cat_cd", DD_ketogri_Selection1.SelectedValue);
                                        ins_prof.Parameters.AddWithValue("@inv_asset_sub_cat_cd", DD_SUBKATEG3.SelectedValue);
                                        ins_prof.Parameters.AddWithValue("@inv_asset_type_cd", DD_JENISASET3.SelectedValue);
                                        ins_prof.Parameters.AddWithValue("@inv_asset_cd", DropDownList22.SelectedValue);
                                        ins_prof.Parameters.AddWithValue("@inv_reg_dt", DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"));
                                        ins_prof.Parameters.AddWithValue("@inv_price_amt", txt_hargaper3.Text);
                                        ins_prof.Parameters.AddWithValue("@inv_qty", txt_kuantiti3.Text);
                                        ins_prof.Parameters.AddWithValue("@inv_warranty", txt_jaminan3.Text);
                                        if (txt_debeli3.Text != "")
                                        {
                                            datedari = txt_debeli3.Text;
                                            DateTime dt = DateTime.ParseExact(datedari, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                            fromdate = dt.ToString("yyyy-MM-dd");
                                        }
                                        ins_prof.Parameters.AddWithValue("@inv_buy_dt", fromdate);
                                        if (txt_diterima3.Text != "")
                                        {
                                            datedari1 = txt_diterima3.Text;
                                            DateTime dt1 = DateTime.ParseExact(datedari1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                            fromdate2 = dt1.ToString("yyyy-MM-dd");
                                        }
                                        ins_prof.Parameters.AddWithValue("@inv_receive_dt", fromdate2);
                                        ins_prof.Parameters.AddWithValue("@inv_remark", txt_catan3.Text.Replace("'", "''"));
                                        ins_prof.Parameters.AddWithValue("@inv_po_no", txt_nopo.Text);
                                        ins_prof.Parameters.AddWithValue("@inv_do_no", dono.ToString());
                                        ins_prof.Parameters.AddWithValue("@inv_supplier_id", dd_pemp3.SelectedValue);
                                        ins_prof.Parameters.AddWithValue("@inv_crt_id", Session["New"].ToString());
                                        ins_prof.Parameters.AddWithValue("@inv_crt_dt", DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"));
                                        ins_prof.Parameters.AddWithValue("@inv_org_id", DropDownList1.SelectedValue);
                                        ins_prof.Parameters.AddWithValue("@inv_sas_id", "0");
                                        ins_prof.Parameters.AddWithValue("@flag_set", "0");
                                        con.Open();
                                        int i = ins_prof.ExecuteNonQuery();
                                        con.Close();
                                        DataTable dtena = new DataTable();
                                        dtena = DBCon.Ora_Execute_table("select acp_all_qty from ast_acceptance where acp_asset_cat_cd='" + DD_ketogri_Selection1.SelectedValue + "' and acp_asset_sub_cat_cd='" + DD_SUBKATEG3.SelectedValue + "' and acp_asset_type_cd='" + DD_JENISASET3.SelectedValue + "' and acp_po_no='" + txt_nopo.Text + "' and acp_asset_cd='" + DropDownList22.SelectedValue + "' and acp_do_no='" + dono.ToString() + "'");
                                        if (dtena.Rows.Count != 0)
                                        {
                                            int ss_qty = Convert.ToInt32(txt_kuantiti3.Text);
                                            int sno = (Convert.ToInt32(dtena.Rows[0][0].ToString()) + ss_qty);
                                            DataTable dtacp = new DataTable();
                                            dtacp = DBCon.Ora_Execute_table("update ast_acceptance set acp_all_qty='" + sno + " ' where acp_asset_cat_cd='" + DD_ketogri_Selection1.SelectedValue + "' and acp_asset_sub_cat_cd='" + DD_SUBKATEG3.SelectedValue + "' and acp_asset_type_cd='" + DD_JENISASET3.SelectedValue + "' and acp_po_no='" + txt_nopo.Text + "' and acp_do_no='" + dono.ToString() + "' and acp_asset_cd='" + DropDownList22.SelectedValue + "'");
                                        }
                                        if (1 >= 0)
                                        {
                                            grid();
                                            grid_inv();
                                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                                            clear();
                                        }
                                    }
                                    else
                                    {
                                        grid();
                                        grid_inv();
                                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Sudah Wujud.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                                    }
                                }
                                else
                                {
                                    grid_inv();
                                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Kuantiti.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                                }
                            }
                            else
                            {
                                grid_inv();
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Tarikh Diterima.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                            }
                        }
                        else
                        {
                            grid_inv();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Tarikh Dibeli.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                        }
                    }
                    else
                    {
                        grid_inv();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Harga Perolehan (RM).',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    grid();
                    grid_inv();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else if (kategId == "04")
            {


                if (DD_ketogri_Selection1.SelectedValue != "" && DD_SUBKATEG4.SelectedValue != "" && DD_JENISASET4.SelectedValue != "" && DropDownList4.SelectedValue != "")
                {
                    if (TextBox2.Text != "")
                    {
                        if (TextBox1.Text != "")
                        {
                            if (TextBox3.Text != "")
                            {
                                if (DD_Negeri4.SelectedValue != "")
                                {
                                    if (dd_ejen.SelectedValue != "")
                                    {
                                        DataTable dtast = new DataTable();
                                        dtast = DBCon.Ora_Execute_table("select COUNT(*) from ast_property");
                                        string pro_id = string.Empty;

                                        pro_id = DD_ketogri_Selection1.SelectedValue.Trim() + "" + DD_SUBKATEG4.SelectedValue.Trim() + "" + DD_JENISASET4.SelectedValue.Trim();

                                        object count1 = (Convert.ToInt32(dtast.Rows[0][0].ToString()) + 1);
                                        string snp = count1.ToString();
                                        var count = pro_id + snp.PadLeft(6, '0');
                                        string as_id = string.Empty;
                                        as_id = count;
                                        string su_id3 = string.Empty;
                                        DataTable ddicno2_pro = new DataTable();
                                        ddicno2_pro = DBCon.Ora_Execute_table("select * from ast_property where pro_asset_id='" + as_id + "'");

                                        if (ddicno2_pro.Rows.Count == 0)
                                        {

                                            string datedari = string.Empty, datedari1 = string.Empty, datedari2 = string.Empty, datedari3 = string.Empty, datedari4 = string.Empty, datedari5 = string.Empty;
                                            string fromdate = string.Empty, fromdate1 = string.Empty, fromdate2 = string.Empty, fromdate3 = string.Empty, fromdate4 = string.Empty, fromdate5 = string.Empty;
                                            if (txt_tarikh4.Text != "")
                                            {
                                                datedari = txt_tarikh4.Text;
                                                DateTime dt = DateTime.ParseExact(datedari, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                                fromdate = dt.ToString("yyyy-MM-dd");
                                            }
                                            if (txt_pintu4.Text != "")
                                            {
                                                datedari2 = txt_pintu4.Text;
                                                DateTime dt2 = DateTime.ParseExact(datedari2, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                                fromdate2 = dt2.ToString("yyyy-MM-dd");
                                            }
                                            if (txt_tariktanah4.Text != "")
                                            {
                                                datedari3 = txt_tariktanah4.Text;
                                                DateTime dt3 = DateTime.ParseExact(datedari3, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                                fromdate3 = dt3.ToString("yyyy-MM-dd");
                                            }
                                            if (txt_cukaibardar4.Text != "")
                                            {
                                                datedari4 = txt_cukaibardar4.Text;
                                                DateTime dt4 = DateTime.ParseExact(datedari4, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                                fromdate4 = dt4.ToString("yyyy-MM-dd");
                                            }
                                            if (txt_nopt4.Text != "")
                                            {
                                                datedari5 = txt_nopt4.Text;
                                                DateTime dt5 = DateTime.ParseExact(datedari5, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                                fromdate5 = dt5.ToString("yyyy-MM-dd");
                                            }

                                            string ss1 = string.Empty, ss2 = string.Empty;

                                            if (RadioButton1.Checked == true)
                                            {
                                                ss1 = "1";
                                            }
                                            else if (RadioButton2.Checked == true)
                                            {
                                                ss1 = "2";
                                            }
                                            else
                                            {
                                                ss1 = "0";
                                            }

                                            if (RadioButton3.Checked == true)
                                            {
                                                ss2 = "1";
                                            }
                                            else if (RadioButton4.Checked == true)
                                            {
                                                ss2 = "2";
                                            }
                                            else
                                            {
                                                ss2 = "0";
                                            }
                                            string Inssql = "insert into ast_property(pro_asset_id,pro_asset_cat_cd,pro_asset_sub_cat_cd,pro_asset_type_cd ,pro_asset_cd,pro_reg_dt ,pro_state_cd ,pro_grand_type_cd,pro_ownership_no,pro_lot_no,pro_mtr_sqft,pro_district,pro_city,pro_location,pro_address,pro_usage_cd,pro_hold_type_ind,pro_file_no,pro_ownership_cd,pro_lease_ind,pro_lease_due_dt,pro_lease_dur,pro_ownership_prcntg,pro_constraint,pro_buy_cash_ind,pro_buy_amt,pro_buy_dt,pro_loan_amt,pro_bank_cd,pro_loan_dur,pro_interest_rate,pro_installment_amt,pro_prev_owner,pro_agent_id,pro_assessment_due_dt,pro_assessment_amt,pro_land_tax_due_dt,pro_land_tax_amt,pro_city_tax_due_dt,pro_city_tax_amt,pro_asset_staff_id1,pro_asset_staff_id2,pro_po_no,pro_do_no,pro_org_id,pro_crt_id,pro_crt_dt,pro_pt,pro_sas_id,flag_set) VALUES ('" + as_id + "','" + DD_ketogri_Selection1.SelectedValue + "','" + DD_SUBKATEG4.SelectedValue + "','" + DD_JENISASET4.SelectedValue + "','" + DropDownList4.SelectedValue + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + DD_Negeri4.SelectedValue + "','" + DropDownList3.SelectedValue + "','" + TextBox1.Text + "','" + TextBox3.Text + "','" + txt_luas4.Text + "','" + txt_dearah4.Text.Replace("'", "''") + "','" + txt_bandar4.Text.Replace("'", "''") + "','" + TextBox4.Text.Replace("'", "''") + "','" + txt_ap.Value.Replace("'", "''") + "','" + DD_penggunaan4.SelectedValue + "','" + DD_pegangan4.SelectedValue + "','" + txt_fail4.Text + "','" + DD_MILIKAN4.SelectedValue + "','" + ss1 + "','" + fromdate + "','" + txt_tempoh4.Text + "','" + txt_milikan4.Text + "','" + Textarea1.Value.Replace("'", "''") + "','" + ss2 + "','" + txt_bel4.Text + "','" + fromdate5 + "','" + txt_pembiayaan4.Text + "','" + DD_Namabank4.SelectedValue + "','" + txt_pembiayan4.Text + "','" + txt_kadar4.Text + "','" + txt_bayaran4.Text + "','" + txt_BD.Text.Replace("'", "''") + "','" + dd_ejen.SelectedValue + "','" + fromdate2 + "','" + txt_cukaipintu4.Text + "','" + fromdate3 + "','" + txt_amauntanah4.Text + "','" + fromdate4 + "','" + txt_amaunbardar4.Text + "','" + DD_PEGAWAI4.SelectedValue + "','" + DD_PEGAWAIAST4.SelectedValue + "','" + txt_nopo.Text + "','" + dono.ToString() + "','" + DropDownList1.SelectedValue + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + TextBox2.Text + "','0','0')";
                                            Status = DBCon.Ora_Execute_CommamdText(Inssql);
                                            DataTable dt1 = new DataTable();
                                            //dt1 = DBCon.Ora_Execute_table("insert into ast_property(pro_asset_cat_cd,pro_asset_sub_cat_cd,pro_asset_type_cd ,pro_asset_cd,pro_reg_dt ,pro_state_cd ,pro_grand_type_cd,pro_ownership_no,pro_lot_no,pro_mtr_sqft,pro_state_cd,pro_district,pro_city,pro_location,pro_address,pro_usage_cd,pro_hold_type_ind,pro_file_no,pro_ownership_cd,pro_lease_ind,pro_lease_due_dt,pro_lease_dur,pro_ownership_prcntg,pro_constraint,pro_buy_cash_ind,pro_buy_amt,pro_buy_dt,pro_loan_amt,pro_bank_cd,pro_loan_dur,pro_interest_rate,pro_installment_amt,pro_prev_owner,pro_agent_id,pro_assessment_due_dt,pro_assessment_amt,pro_land_tax_due_dt,pro_land_tax_amt,pro_city_tax_due_dt,pro_city_tax_amt,pro_asset_staff_id1,pro_asset_staff_id2,pro_po_no,pro_do_no,pro_org_id,pro_crt_id,pro_crt_dt) VALUES ('" + DD_ketogri_Selection1.SelectedValue + "','" + DD_SUBKATEG4.SelectedValue + "','" + DD_JENISASET4.SelectedValue + "','" + txt_nama_aset.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + DropDownList3.SelectedValue + "','" + TextBox1.Text + "','" + TextBox3.Text + "','" + txt_luas4.Text + "','" + DD_Negeri4.SelectedValue + "','" + txt_dearah4.Text.Replace("'", "''") + "','" + txt_bandar4.Text.Replace("'", "''") + "','" + TextBox4.Text.Replace("'", "''") + "','" + txt_ap.Value.Replace("'", "''") + "','" + DD_penggunaan4.SelectedValue + "','" + DD_pegangan4.SelectedValue + "','" + txt_fail4.Text + "','" + DD_MILIKAN4.SelectedValue + "','" + ss1 + "','" + fromdate + "','" + txt_tempoh4.Text + "','" + txt_milikan4.Text + "','" + Textarea1.Value.Replace("'", "''") + "','" + ss2 + "','" + txt_bel4.Text + "','" + fromdate5 + "','" + txt_pembiayaan4.Text + "','" + DD_Namabank4.SelectedValue + "','" + txt_pembiayan4.Text + "','" + txt_kadar4.Text + "','" + txt_bayaran4.Text + "','" + txt_BD.Text.Replace("'", "''") + "','" + dd_ejen.SelectedValue + "','" + fromdate2 + "','" + txt_cukaipintu4.Text + "','" + fromdate3 + "','" + txt_amauntanah4.Text + "','" + fromdate4 + "','" + txt_amaunbardar4.Text + "','" + DD_PEGAWAI4.SelectedValue + "','" + DD_PEGAWAIAST4.SelectedValue + "','"+ txt_nopo.Text +"','"+ dono.ToString() +"','"+ Session["New"].ToString() +"','"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +"')");

                                            if (Status == "SUCCESS")
                                            {
                                                if (txt_nodafter5.Text != "")
                                                {

                                                    string Inssql1 = "insert into ast_partner(par_asset_cat_cd,par_asset_sub_cat_cd,par_asset_type_cd,par_asset_cd,par_reg_dt,par_comp_reg_no,par_comp_name,par_contact_prsn,par_phone_no,par_email,par_address,par_postcode,par_city,par_state_cd,pro_ownership_no,pro_state_cd,par_crt_id,par_crt_dt) VALUES ('" + DD_ketogri_Selection1.SelectedValue + "','" + DD_SUBKATEG4.SelectedValue + "','" + DD_JENISASET4.SelectedValue + "','" + DropDownList4.SelectedValue + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + txt_nodafter5.Text.Replace("'", "''") + "','" + txt_namasyarikat5.Text.Replace("'", "''") + "','" + txt_nama_hubu5.Text.Replace("'", "''") + "','" + txt_notele5.Text + "','" + txt_email5.Text + "','" + txt_alamat5.Text.Replace("'", "''") + "','" + txt_poskod5.Text + "','" + txt_bardar5.Text + "','" + DD_Negiri5.SelectedValue + "','" + TextBox1.Text + "','" + DD_Negeri4.SelectedValue + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                                                    Status1 = DBCon.Ora_Execute_CommamdText(Inssql1);
                                                }
                                                string sno = string.Empty;
                                                DataTable dtena = new DataTable();
                                                dtena = DBCon.Ora_Execute_table("select acp_all_qty from ast_acceptance where acp_asset_cat_cd='" + DD_ketogri_Selection1.SelectedValue + "' and acp_asset_sub_cat_cd='" + DD_SUBKATEG4.SelectedValue + "' and acp_asset_type_cd='" + DD_JENISASET4.SelectedValue + "' and acp_asset_cd='" + DropDownList4.SelectedValue + "' and acp_po_no='" + txt_nopo.Text + "' and acp_do_no='" + dono.ToString() + "'");
                                                if (dtena.Rows.Count != 0)
                                                {
                                                    sno = (double.Parse(dtena.Rows[0][0].ToString()) + 1).ToString();
                                                    DataTable dtacp = new DataTable();
                                                    dtacp = DBCon.Ora_Execute_table("update ast_acceptance set acp_all_qty='" + sno + " ' where acp_asset_cat_cd='" + DD_ketogri_Selection1.SelectedValue + "' and acp_asset_sub_cat_cd='" + DD_SUBKATEG4.SelectedValue + "' and acp_asset_type_cd='" + DD_JENISASET4.SelectedValue + "' and acp_po_no='" + txt_nopo.Text + "' and acp_asset_cd='" + DropDownList4.SelectedValue + "' and acp_do_no='" + dono.ToString() + "'");
                                                }
                                                DD_ketogri_Selection1.Attributes.Remove("style");
                                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                                                clear();
                                                grid();
                                                grid_pro();
                                            }

                                        }
                                        else
                                        {
                                            grid();
                                            grid_pro();
                                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Sudah Wujud.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                                        }
                                    }
                                    else
                                    {
                                        grid();
                                        grid_pro();
                                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Nama Ejen.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                                    }

                                }
                                else
                                {
                                    grid();
                                    grid_pro();
                                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Negeri.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                                }
                            }
                            else
                            {
                                grid();
                                grid_pro();
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan No Lot.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                            }
                        }
                        else
                        {
                            grid();
                            grid_pro();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan No Hak Milik.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                        }
                    }
                    else
                    {
                        grid();
                        grid_pro();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan  No PT.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    grid();
                    grid_pro();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }

            }

        }
        catch
        {
            con.Close();
            grid();
            grid_pro();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Issue.');", true);
        }
    }

    protected void gv_refdata_PageIndexChanging_com(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        grid();
        grid_bot();
    }

    protected void gv_refdata_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        grid();
        grid_bot();
    }

    protected void gv_refdata_PageIndexChanging_car(object sender, GridViewPageEventArgs e)
    {
        GridView3.PageIndex = e.NewPageIndex;
        grid();
        grid_car();
    }

    protected void gv_refdata_PageIndexChanging_pro(object sender, GridViewPageEventArgs e)
    {
        GridView5.PageIndex = e.NewPageIndex;
        grid();
        grid_pro();
    }


    protected void gv_refdata_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        grid();
    }


    void clear()
    {
        DD_ketogri_Selection1.Attributes.Remove("style");
        c001.Attributes.Remove("style");
        hm001.Attributes.Remove("style");
        iv001.Attributes.Remove("style");
        TextBox6.Text = "0";
        TextBox7.Text = "0";
        TextBox8.Text = "0";
        TextBox9.Text = "0";
        //btnsimpan.Visible = false;
        DD_JENISASET3.SelectedValue = "";
        DD_JENISASET4.SelectedValue = "";
        //DD_ketogri_Selection1.SelectedValue = "";
        DD_MILIKAN4.SelectedValue = "";
        DD_Namabank4.SelectedValue = "";
        DD_Negeri4.SelectedValue = "";
        DD_Negiri5.SelectedValue = "";
        DD_pegangan4.SelectedValue = "";
        dd_pemp3.SelectedValue = "";
        DD_penggunaan4.SelectedValue = "";
        DD_Perolehan.SelectedValue = "";
        DD_SUBKATEG3.SelectedValue = "";
        DD_SUBKATEG4.SelectedValue = "";
        DropDownList1.SelectedValue = "";
        DropDownList4.SelectedValue = "";
        txt_alamat5.Text = "";
        txt_amaunbardar4.Text = "";
        txt_bandar4.Text = "";
        txt_bardar5.Text = "";
        txt_bayaran4.Text = "";
        txt_catan3.Text = "";
        txt_cukaibardar4.Text = "";
        txt_cukaipintu4.Text = "";
        txt_dearah4.Text = "";
        txt_debeli3.Text = "";
        txt_diterima3.Text = "";
        txt_email5.Text = "";
        txt_hargaper3.Text = "";
        txt_jaminan3.Text = "";
        txt_kadar4.Text = "";
        txt_kuantiti3.Text = "";
        txt_luas4.Text = "";
        txt_milikan4.Text = "";
        txt_nama_aset.Text = "";
        txt_nama_hubu5.Text = "";
        txt_namasyarikat5.Text = "";
        txt_nodafter5.Text = "";
        txt_nopt4.Text = "";
        txt_notele5.Text = "";
        txt_pembekal4.Text = "";
        txt_pembiayaan4.Text = "";
        txt_pembiayan4.Text = "";
        txt_pintu4.Text = "";
        txt_poskod5.Text = "";
        txt_tarikh4.Text = "";
        txt_tempoh4.Text = "";
        DD_Perolehan.SelectedValue = "";
        TextBox2.Text = "";
        TextBox1.Text = "";
        DropDownList3.SelectedValue = "";
        TextBox3.Text = "";
        txt_luas4.Text = "";
        DD_Negeri4.SelectedValue = "";
        TextBox4.Text = "";
        txt_ap.Value = "";
        txt_fail4.Text = "";
        RadioButton1.Checked = false;
        RadioButton2.Checked = false;
        txt_tempoh4.Text = "";
        Textarea1.Value = "";
        RadioButton3.Checked = false;
        RadioButton4.Checked = false;
        txt_bel4.Text = "";
        DD_Namabank4.SelectedValue = "";
        txt_BD.Text = "";
        txt_tariktanah4.Text = "";
        txt_amauntanah4.Text = "";
        txt_pembiayaan4.Text = "";
        txt_pembiayan4.Text = "";
        dd_ejen.SelectedValue = "";
        DD_PEGAWAI4.SelectedValue = "";
        DD_PEGAWAIAST4.SelectedValue = "";
        DropDownList1.SelectedValue = "";
        DD_SUBKATEG1.SelectedValue = "";
        DropDownList2.SelectedValue = "";
        txt_tdibeli.Text = "";
        txt_nosiri.Text = "";
        txt_jenamodel.Text = "";
        ckbox_perlu.Checked = false;
        CheckBox1.Checked = false;
        dd_pemp1.SelectedValue = "";
        txt_kosperb.Text = "";
        txt_jaminan.Text = "";
        txt_kodprod.Text = "";
        txt_tditer.Text = "";
        txt_hatga1.Text = "";
        DD_JENISASET1.SelectedValue = "";
        dd_pemp2.SelectedValue = "";
        txt_nocasis2.Text = "";
        txt_tahun2.Text = "";
        txt_kelaskeg2.Text = "";
        DD_Bahan2.SelectedValue = "";
        DD_JENAMA2.SelectedValue = "";
        DD_JENISASET2.SelectedValue = "";
        DropDownList21.SelectedValue = "";
        DD_SUBKATEG2.SelectedValue = "";
        txt_haraga2.Text = "";
        txt_debil2.Text = "";
        txt_diterima2.Text = "";
        DD_Model2.SelectedValue = "";
        txt_keupayaan2.Text = "";
        txt_warna2.Text = "";
        DD_jeniskend2.Text = "";
        txt_jaminan2.Text = "";
        txt_platno2.Text = "";
        txt_jenisdan2.Text = "";
    }
    void grid_bot()
    {

        sel_mkat();
        string com_qry1 = string.Empty;
        if (txt_nopo.Text != "")
        {
            com_qry1 = "com_po_no='" + txt_nopo.Text + "' and com_asset_cat_cd='" + DD_ketogri_Selection1.SelectedValue + "' and com_asset_sub_cat_cd='" + DD_SUBKATEG1.SelectedValue + "' and com_asset_type_cd= '" + DD_JENISASET1.SelectedValue + "' and com_asset_cd='" + DropDownList2.SelectedValue + "'";
        }
        else
        {
            com_qry1 = "com_asset_cat_cd='" + DD_ketogri_Selection1.SelectedValue + "'";
        }

        //SqlCommand cmd = new SqlCommand("select com_asset_id,cas_asset_desc,com_serial_no,com_prod_no,com_price_amt from ast_component ac left join ast_cmn_asset aca on aca.cas_asset_cd=com_asset_cd where com_po_no='" + txt_nopo.Text + "' and com_asset_cat_cd='" + DD_ketogri_Selection1.SelectedValue + "' and com_asset_sub_cat_cd='" + DD_SUBKATEG1.SelectedValue + "' and com_asset_type_cd= '" + DD_JENISASET1.SelectedValue + "' and com_asset_cd='" + DropDownList2.SelectedValue + "'", con);
        SqlCommand cmd = new SqlCommand("select com_asset_id,cas_asset_desc,com_serial_no,com_prod_no,com_price_amt from ast_component ac left join ast_cmn_asset aca on aca.cas_asset_cd=com_asset_cd and aca.cas_asset_cat_cd=ac.com_asset_cat_cd and aca.cas_asset_sub_cat_cd=ac.com_asset_sub_cat_cd and aca.cas_asset_type_cd=ac.com_asset_type_cd where " + com_qry1 + "", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView2.DataSource = ds;
            GridView2.DataBind();
            int columncount = GridView2.Rows[0].Cells.Count;
            GridView2.Rows[0].Cells.Clear();
            GridView2.Rows[0].Cells.Add(new TableCell());
            GridView2.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView2.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
            //btn_hups.Visible = false;
        }
        else
        {
            GridView2.DataSource = ds;
            GridView2.DataBind();
        }
        //con.Close();
    }

    void grid_car()
    {

        sel_mkat();
        string hm_qry1 = string.Empty;
        if (txt_nopo.Text != "")
        {
            hm_qry1 = "car_po_no='" + txt_nopo.Text + "' and car_asset_cat_cd='" + DD_ketogri_Selection1.SelectedValue + "' and car_asset_sub_cat_cd='" + DD_SUBKATEG2.SelectedValue + "' and car_asset_type_cd= '" + DD_JENISASET2.SelectedValue + "' and car_asset_cd='" + DropDownList21.SelectedValue + "'";
        }
        else
        {
            hm_qry1 = "car_asset_cat_cd='" + DD_ketogri_Selection1.SelectedValue + "'";
        }

        SqlCommand cmd1 = new SqlCommand("select car_asset_id,cas_asset_desc,ast_Model_desc,car_engine_no,car_price_amt,car_plate_no from ast_car aa left join ast_cmn_asset ac on aa.car_asset_cd=ac.cas_asset_cd and aa.car_asset_cat_cd=ac.cas_asset_cat_cd and aa.car_asset_sub_cat_cd=ac.cas_asset_sub_cat_cd and aa.car_asset_type_cd=ac.cas_asset_type_cd left join Ref_ast_Model rm on rm.ast_Model_code=aa.car_model_cd where " + hm_qry1 + "", con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        da1.Fill(ds1);
        if (ds1.Tables[0].Rows.Count == 0)
        {
            ds1.Tables[0].Rows.Add(ds1.Tables[0].NewRow());
            GridView3.DataSource = ds1;
            GridView3.DataBind();
            int columncount = GridView3.Rows[0].Cells.Count;
            GridView3.Rows[0].Cells.Clear();
            GridView3.Rows[0].Cells.Add(new TableCell());
            GridView3.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView3.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
            //btn_hups.Visible = false;
        }
        else
        {
            GridView3.DataSource = ds1;
            GridView3.DataBind();
        }
        //DataTable dt = new DataTable();
        //dt = DBCon.Ora_Execute_table("select car_asset_id,cas_asset_desc,ast_Model_desc,car_engine_no,car_price_amt from ast_car aa left join ast_cmn_asset ac on aa.car_asset_cd=ac.cas_asset_cd left join Ref_ast_Model rm on rm.ast_Model_code=aa.car_model_cd where car_po_no='" + txt_nopo.Text + "'");

        //if (dt.Rows.Count == 0)
        //{

        //    GridView3.DataSource = dt;
        //    GridView3.DataBind();
        //    int columncount = GridView3.Rows[0].Cells.Count;
        //    GridView3.Rows[0].Cells.Clear();
        //    GridView3.Rows[0].Cells.Add(new TableCell());
        //    GridView3.Rows[0].Cells[0].ColumnSpan = columncount;
        //    GridView3.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
        //    //btn_hups.Visible = false;
        //}
        //else
        //{
        //    GridView3.DataSource = dt;
        //    GridView3.DataBind();
        //}
        //con.Close();
    }

    void grid_inv()
    {

        sel_mkat();
        string in_qry1 = string.Empty;
        if (txt_nopo.Text != "")
        {
            in_qry1 = "inv_po_no='" + txt_nopo.Text + "' and inv_asset_cat_cd='" + DD_ketogri_Selection1.SelectedValue + "' and inv_asset_sub_cat_cd='" + DD_SUBKATEG3.SelectedValue + "' and inv_asset_type_cd= '" + DD_JENISASET3.SelectedValue + "' and inv_asset_cd='" + DropDownList22.SelectedValue + "'";
        }
        else
        {
            in_qry1 = "inv_asset_cat_cd='" + DD_ketogri_Selection1.SelectedValue + "'";
        }

        SqlCommand cmd2 = new SqlCommand("select inv_asset_id,cas_asset_desc,inv_warranty,inv_remark,inv_qty,inv_price_amt from ast_inventory aa left join ast_cmn_asset ac on aa.inv_asset_cd=ac.cas_asset_cd where " + in_qry1 + "", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            GridView4.DataSource = ds2;
            GridView4.DataBind();
            int columncount = GridView4.Rows[0].Cells.Count;
            GridView4.Rows[0].Cells.Clear();
            GridView4.Rows[0].Cells.Add(new TableCell());
            GridView4.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView4.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
            //btn_hups.Visible = false;
        }
        else
        {
            GridView4.DataSource = ds2;
            GridView4.DataBind();
        }
    }


    void grid_pro()
    {

        sel_mkat();
        string pr_qry1 = string.Empty;
        if (txt_nopo.Text != "")
        {
            pr_qry1 = "pro_po_no='" + txt_nopo.Text + "' and pro_asset_cat_cd='" + DD_ketogri_Selection1.SelectedValue + "' and pro_asset_sub_cat_cd='" + DD_SUBKATEG4.SelectedValue + "' and pro_asset_type_cd= '" + DD_JENISASET4.SelectedValue + "' and pro_asset_cd='" + DropDownList4.SelectedValue + "'";
        }
        else
        {
            pr_qry1 = "pro_asset_cat_cd='" + DD_ketogri_Selection1.SelectedValue + "'";
        }

        SqlCommand cmd2 = new SqlCommand("select pro_asset_id,a3.cas_asset_desc,pro_reg_dt,pro_lot_no,a1.Decription,pro_ownership_no,pro_pt,a2.sup_name from ast_property left join Ref_Negeri a1 on a1.Decription_Code=pro_state_cd left join ast_supplier a2 on a2.sup_id=pro_agent_id left join ast_cmn_asset a3 on a3.cas_asset_cat_cd=pro_asset_cat_cd and a3.cas_asset_sub_cat_cd=pro_asset_sub_cat_cd and a3.cas_asset_type_cd=pro_asset_type_cd and a3.cas_asset_cd=pro_asset_cd where " + pr_qry1 + "", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            GridView5.DataSource = ds2;
            GridView5.DataBind();
            int columncount = GridView5.Rows[0].Cells.Count;
            GridView5.Rows[0].Cells.Clear();
            GridView5.Rows[0].Cells.Add(new TableCell());
            GridView5.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView5.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
            //btn_hups.Visible = false;
        }
        else
        {
            GridView5.DataSource = ds2;
            GridView5.DataBind();
        }
    }



    //protected void Button5_Click(object sender, EventArgs e)
    //{
    //    Session["validate_success"] = "";
    //    Session["alrt_msg"] = "";
    //    Response.Redirect("../Aset/Ast_dafter_aset.aspx");
    //}

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_dafter_aset_view.aspx");
    }

    
}