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

public partial class Ast_stok : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();

    private static int PageSize = 20;
    string qry1 = string.Empty, qry2 = string.Empty;
    string level;
    string Status = string.Empty, Status1 = string.Empty;
    string userid;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2()})";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;
                kategori();
                if (samp != "")
                {
                    lbl_name.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    view_details();
                    ver_id.Text = "1";

                }
                else
                {
                    ver_id.Text = "0";
                }
                userid = Session["New"].ToString();
               
             
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    void kategori()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select ast_kategori_code,ast_kategori_desc from Ref_ast_kategori";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_Kategori.DataSource = dt;
            DD_Kategori.DataTextField = "ast_kategori_desc";
            DD_Kategori.DataValueField = "ast_kategori_code";
            DD_Kategori.DataBind();
            DD_Kategori.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {
        skat();
        DD_Jenis_ast.SelectedValue = "";
    }

    void skat()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select ast_subkateast_Code,ast_subkateast_desc from Ref_ast_sub_kategri_Aset where Status='A' and ast_kategori_Code='" + DD_Kategori.SelectedValue + "'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_Sub_Kateg.DataSource = dt;
            DD_Sub_Kateg.DataTextField = "ast_subkateast_desc";
            DD_Sub_Kateg.DataValueField = "ast_subkateast_Code";
            DD_Sub_Kateg.DataBind();
            DD_Sub_Kateg.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void OnSelectedIndexChanged1(object sender, EventArgs e)
    {
        jenaset();
    }

    void jenaset()
    {
        DataSet Ds1 = new DataSet();
        try
        {
            string com1 = "select * from Ref_ast_jenis_aset where Status='A' and ast_cat_Code='" + DD_Kategori.SelectedValue + "' and ast_sub_cat_Code = '" + DD_Sub_Kateg.SelectedValue + "'";
            SqlDataAdapter adpt = new SqlDataAdapter(com1, con);
            DataTable dt1 = new DataTable();
            adpt.Fill(dt1);
            DD_Jenis_ast.DataSource = dt1;
            DD_Jenis_ast.DataTextField = "ast_jeniaset_desc";
            DD_Jenis_ast.DataValueField = "ast_jeniaset_Code";
            DD_Jenis_ast.DataBind();
            DD_Jenis_ast.Items.Insert(0, new ListItem("--- PILIH ---", ""));
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
            Button4.Text = "Kemaskini";
            ver_id.Text = "1";
            get_id.Text = lbl_name.Text;
            DD_Sub_Kateg.Attributes.Add("disabled", "disabled");
            DD_Jenis_ast.Attributes.Add("disabled", "disabled");
            DD_Kategori.Attributes.Add("disabled", "disabled");
            string abc = lbl_name.Text;
            DataTable ddokdicno1 = new DataTable();
            ddokdicno1 = DBCon.Ora_Execute_table("select cas_asset_cat_cd,cas_asset_sub_cat_cd,cas_asset_type_cd,cas_asset_cd,cas_asset_desc From ast_cmn_asset where ID='" + abc + "'");
            if (ddokdicno1.Rows.Count != 0)
            {
                DD_Kategori.SelectedValue = ddokdicno1.Rows[0]["cas_asset_cat_cd"].ToString().Trim();
                skat();
                DD_Sub_Kateg.SelectedValue = ddokdicno1.Rows[0]["cas_asset_sub_cat_cd"].ToString().Trim();
                jenaset();
                DD_Jenis_ast.SelectedValue = ddokdicno1.Rows[0]["cas_asset_type_cd"].ToString();
                DD_NAMAAST.Text = ddokdicno1.Rows[0]["cas_asset_cd"].ToString().Trim();
                txt_Keterangan.Text = ddokdicno1.Rows[0]["cas_asset_desc"].ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Maklumat Carian Tidak Dijumpai');", true);
            }
            TextBox1.Text = abc.ToString();
            
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void clk_submit(object sender, EventArgs e)
    {
            if (DD_Kategori.SelectedValue != "" && DD_Sub_Kateg.SelectedValue != "" && DD_Jenis_ast.SelectedValue != "" && DD_NAMAAST.Text != "" && txt_Keterangan.Text != "")
            {
                if (ver_id.Text == "0")
                {
                    DataTable ddokdicno1 = new DataTable();
                    ddokdicno1 = DBCon.Ora_Execute_table("select * From ast_cmn_asset where cas_asset_cat_cd='" + DD_Kategori.SelectedValue + "' and cas_asset_sub_cat_cd='" + DD_Sub_Kateg.SelectedValue + "' and cas_asset_type_cd='" + DD_Jenis_ast.SelectedValue + "' and cas_asset_cd='" + DD_NAMAAST.Text + "' ");
                    if (ddokdicno1.Rows.Count == 0)
                    {
                        string Inssql1 = "insert into ast_cmn_asset (cas_asset_cat_cd,cas_asset_sub_cat_cd,cas_asset_type_cd,cas_asset_cd,cas_asset_desc,cas_crt_id,cas_crt_dt) values('" + DD_Kategori.SelectedValue + "','" + DD_Sub_Kateg.SelectedValue + "','" + DD_Jenis_ast.SelectedValue + "','" + DD_NAMAAST.Text + "','" + txt_Keterangan.Text.Replace("'", "''") + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' )";
                        Status1 = DBCon.Ora_Execute_CommamdText(Inssql1);
                        if (Status1 == "SUCCESS")
                        {
                        Session["validate_success"] = "SUCCESS";
                        Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                        Response.Redirect("../Aset/Ast_stok_view.aspx");
                        
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
                    string Inssql1 = "Update ast_cmn_asset set cas_asset_cat_cd='" + DD_Kategori.SelectedValue + "',cas_asset_sub_cat_cd='" + DD_Sub_Kateg.SelectedValue + "',cas_asset_type_cd='" + DD_Jenis_ast.SelectedValue + "',cas_asset_cd='" + DD_NAMAAST.Text + "', cas_asset_desc='" + txt_Keterangan.Text.Replace("'", "''") + "',cas_upd_id='" + Session["New"].ToString() + "',cas_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where  ID='" + TextBox1.Text + "'";
                    Status1 = DBCon.Ora_Execute_CommamdText(Inssql1);
                    if (Status1 == "SUCCESS")
                    {
                    Session["validate_success"] = "SUCCESS";
                    Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                    Response.Redirect("../Aset/Ast_stok_view.aspx");
                    
                    }
                    else
                    {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Bidang.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }
                }
            }
            else
            {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
        
        

    }


    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_stok.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_stok_view.aspx");
    }

    
}