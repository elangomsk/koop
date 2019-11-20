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

public partial class Ast_Pembekal : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();

    
    string qry1 = string.Empty, qry2 = string.Empty;
    
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
                bank_nama();
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

    void bank_nama()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select Bank_Code,Bank_Name from Ref_Nama_Bank where Status='A' order by Bank_Name asc";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            sel_bank.DataSource = dt;
            sel_bank.DataBind();
            sel_bank.DataTextField = "Bank_Name";
            sel_bank.DataValueField = "Bank_Code";
            sel_bank.DataBind();
            sel_bank.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void kategori()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select ast_kategori_Code,ast_kategori_desc From Ref_ast_kategori where Status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_Kategori.DataSource = dt;
            DD_Kategori.DataTextField = "ast_kategori_desc";
            DD_Kategori.DataValueField = "ast_kategori_Code";
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
        DataSet Ds = new DataSet();
        try
        {
            get_katval();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void get_katval()
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

    void sub_kategori()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select ast_subkateast_Code,ast_subkateast_desc from Ref_ast_sub_kategri_Aset where Status='A'";
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
    void view_details()
    {
        Button1.Visible = false;
        try
        {
            Button4.Text = "Kemaskini";
            ver_id.Text = "1";
            txt_nodraft.Attributes.Add("Style", "pointer-events:None;");
            string abc = lbl_name.Text;
            DataTable ddokdicno1 = new DataTable();
            ddokdicno1 = DBCon.Ora_Execute_table("select sup_address,sup_asset_cat_cd,sup_phone_no,sup_asset_sub_cat_cd,sup_address,sup_id,sup_name,sup_bumi_ind,sup_gst_ind,sup_bank_acc_no,sup_bank_cd From ast_supplier where ID='" + abc + "'");

           
            if (ddokdicno1.Rows.Count != 0)
            {
                //txt_nodraft.Enabled = false;
                txt_nodraft.Text = ddokdicno1.Rows[0]["sup_id"].ToString();
                txtarea_alamat.Text = ddokdicno1.Rows[0]["sup_address"].ToString();
                DD_Kategori.SelectedValue = ddokdicno1.Rows[0]["sup_asset_cat_cd"].ToString().Trim();
                get_katval();
                DD_Sub_Kateg.SelectedValue = ddokdicno1.Rows[0]["sup_asset_sub_cat_cd"].ToString().Trim();
                txt_namapembe.Text = ddokdicno1.Rows[0]["sup_name"].ToString();
                txtarea_alamat.Text = ddokdicno1.Rows[0]["sup_address"].ToString();
                txt_notele.Text = ddokdicno1.Rows[0]["sup_phone_no"].ToString();
                acc_no.Text = ddokdicno1.Rows[0]["sup_bank_acc_no"].ToString();
                sel_bank.SelectedValue = ddokdicno1.Rows[0]["sup_bank_cd"].ToString().Trim();
                if (ddokdicno1.Rows[0]["sup_bumi_ind"].ToString().Trim() == "1")
                {
                    stsbumi1.Checked = true;
                    stsbumi2.Checked = false;
                }
                else if (ddokdicno1.Rows[0]["sup_bumi_ind"].ToString().Trim() == "2")
                {
                    stsbumi1.Checked = false;
                    stsbumi2.Checked = true;
                }
                else
                {
                    stsbumi1.Checked = false;
                    stsbumi2.Checked = false;
                }

                if (ddokdicno1.Rows[0]["sup_gst_ind"].ToString().Trim() == "1")
                {
                    sts_gst1.Checked = true;
                    sts_gst2.Checked = false;
                }
                else if (ddokdicno1.Rows[0]["sup_gst_ind"].ToString().Trim() == "2")
                {
                    sts_gst1.Checked = false;
                    sts_gst2.Checked = true;
                }
                else
                {
                    sts_gst1.Checked = false;
                    sts_gst2.Checked = false;
                }

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
            if (txt_nodraft.Text != "" && txt_namapembe.Text != "")
            {
                string sb = null;
                if (stsbumi1.Checked == true)
                {
                    sb = "1";
                }
                else if (stsbumi2.Checked == true)
                {
                    sb = "2";
                }
                else
                {
                    sb = "";
                }
                string Radio1 = sb.ToString();

                string sb1 = null;
                if (sts_gst1.Checked == true)
                {
                    sb1 = "1";
                }
                else if (sts_gst2.Checked == true)
                {
                    sb1 = "2";
                }
                else
                {
                    sb1 = "";
                }
                string Radio2 = sb1.ToString();
                string tel;
                if (txt_notele.Text == "")
                {
                    tel = "";
                }
                else
                {
                    tel = txt_notele.Text;
                }

            if (ver_id.Text == "0")
                {
                    DataTable ddokdicno1 = new DataTable();
                    ddokdicno1 = DBCon.Ora_Execute_table("select sup_id From ast_supplier where sup_id='" + txt_nodraft.Text.Trim() + "' ");
                    if (ddokdicno1.Rows.Count == 0)
                    {
                        string Inssql1 = "insert into ast_supplier (sup_id,sup_name,sup_phone_no,sup_bumi_ind,sup_gst_ind,sup_asset_cat_cd,sup_asset_sub_cat_cd,sup_address,sup_crt_id,sup_crt_dt,sup_bank_acc_no,sup_bank_cd) values('" + txt_nodraft.Text + "','" + txt_namapembe.Text.Replace("'", "''") + "','" + tel + "','" + Radio1 + "','" + Radio2 + "','" + DD_Kategori.SelectedValue + "','" + DD_Sub_Kateg.SelectedValue + "','" + txtarea_alamat.Text.Replace("'", "''") + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + acc_no.Text + "','" + sel_bank.SelectedValue + "')";
                        Status1 = DBCon.Ora_Execute_CommamdText(Inssql1);
                        if (Status1 == "SUCCESS")
                        {
                        Session["validate_success"] = "SUCCESS";
                        Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                        Response.Redirect("../Aset/Ast_Pembekal_view.aspx");
                        
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
                    string Inssql1 = "UPDATE ast_supplier SET sup_name='" + txt_namapembe.Text.Replace("'", "''") + "',sup_phone_no='" + txt_notele.Text + "',sup_bumi_ind='" + sb.ToString() + "',sup_gst_ind='" + sb1.ToString() + "',sup_asset_cat_cd='" + DD_Kategori.SelectedValue + "',sup_asset_sub_cat_cd='" + DD_Sub_Kateg.SelectedValue + "',sup_address='" + txtarea_alamat.Text.Replace("'", "''") + "',sup_bank_acc_no='" + acc_no.Text + "',sup_bank_cd='" + sel_bank.SelectedValue + "',sup_upd_id='" + Session["New"].ToString() + "',sup_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where  ID='" + TextBox1.Text + "'";
                    Status1 = DBCon.Ora_Execute_CommamdText(Inssql1);
                    if (Status1 == "SUCCESS")
                    {
                    Session["validate_success"] = "SUCCESS";
                    Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                    Response.Redirect("../Aset/Ast_Pembekal_view.aspx");
                    
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
        Response.Redirect("../Aset/Ast_Pembekal.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_Pembekal_view.aspx");
    }

    
}