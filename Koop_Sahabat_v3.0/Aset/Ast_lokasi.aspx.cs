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

public partial class Ast_lokasi : System.Web.UI.Page
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
                Orgnasi();
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

    void Orgnasi()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(cs);
            string com = "select org_name,org_gen_id From hr_organization order by org_name asc";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_down.DataSource = dt;
            DD_down.DataBind();
            DD_down.DataTextField = "org_name";
            DD_down.DataValueField = "org_gen_id";
            DD_down.DataBind();
            DD_down.Items.Insert(0, new ListItem("--- PILIH ---", ""));

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
            string abc = lbl_name.Text;
            DataTable ddokdicno1 = new DataTable();
            ddokdicno1 = DBCon.Ora_Execute_table("select lo.org_gen_id,loc_cd,loc_desc from ref_location as lo left join hr_organization as ho on ho.org_gen_id=lo.org_gen_id where ID='" + abc + "'");
            if (ddokdicno1.Rows.Count != 0)
            {
               

                DD_down.Attributes.Add("disabled", "disabled");
                //TextBox2.Attributes.Add("disabled", "disabled");
                DD_down.SelectedValue = ddokdicno1.Rows[0]["org_gen_id"].ToString().Trim();
                TextBox2.Text = ddokdicno1.Rows[0]["loc_cd"].ToString().Trim();
                txt_Lokasi.Text = ddokdicno1.Rows[0]["loc_desc"].ToString();
                TextBox3.Text = abc;
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
            if (DD_down.SelectedValue != "" && TextBox2.Text != "" && txt_Lokasi.Text != "")
            {
            if (ver_id.Text == "0")
            {
                DataTable sel_lok = new DataTable();
                sel_lok = DBCon.Ora_Execute_table("select * from ref_location where org_gen_id='" + DD_down.SelectedValue + "' and loc_cd='" + TextBox2.Text + "'");

                DataTable ddokdicno1 = new DataTable();
                ddokdicno1 = DBCon.Ora_Execute_table("select * from hr_organization where org_gen_id='" + DD_down.SelectedValue + "'");
                if (sel_lok.Rows.Count == 0)
                {
                    string Inssql1 = "insert into ref_location (org_gen_id,org_id,loc_cd,loc_desc,crt_id,crt_dt) values ('" + DD_down.SelectedValue + "','" + ddokdicno1.Rows[0]["org_id"].ToString() + "','" + TextBox2.Text + "','" + txt_Lokasi.Text + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status1 = DBCon.Ora_Execute_CommamdText(Inssql1);
                    if (Status1 == "SUCCESS")
                    {
                        Session["validate_success"] = "SUCCESS";
                        Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                        Response.Redirect("../Aset/Ast_lokasi_view.aspx");

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
                DataTable sel_lok = new DataTable();
                sel_lok = DBCon.Ora_Execute_table("select * from ref_location where org_gen_id='" + DD_down.SelectedValue + "' and loc_cd='" + TextBox2.Text + "' and ID != '" + TextBox3.Text + "'");
                if (sel_lok.Rows.Count == 0)
                {
                    string Inssql1 = "Update ref_location set loc_cd='" + TextBox2.Text + "',loc_desc='" + txt_Lokasi.Text + "',upd_id='" + Session["New"].ToString() + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where ID='" + TextBox3.Text + "'";
                    Status1 = DBCon.Ora_Execute_CommamdText(Inssql1);
                    if (Status1 == "SUCCESS")
                    {
                        Session["validate_success"] = "SUCCESS";
                        Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                        Response.Redirect("../Aset/Ast_lokasi_view.aspx");

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Bidang.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }
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
        Response.Redirect("../Aset/Ast_lokasi.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_lokasi_view.aspx");
    }

    
}