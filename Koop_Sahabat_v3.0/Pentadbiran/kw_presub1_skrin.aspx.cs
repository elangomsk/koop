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

public partial class kw_presub1_skrin : System.Web.UI.Page
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
        string script = " $(function () {$('.select2').select2()})";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                
                userid = Session["New"].ToString();
                Type_Skrins();
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


    void view_details()
    {
        try
        {
            
            btb_kmes.Visible = true;
            Button4.Visible = false;
            Button1.Visible = false;
            string ogid = lbl_name.Text;
            gen_id.Text = lbl_name.Text;
            DataTable ddokdicno = new DataTable();
            ddokdicno = Dblog.Ora_Execute_table("select * from KK_PID_presub1_Skrin where id = '" + ogid + "'");
            if (ddokdicno.Rows.Count != 0)
            {
                txtName.Text = ddokdicno.Rows[0]["KK_Spreskrin1_name"].ToString();
                dd_skrin.SelectedValue = ddokdicno.Rows[0]["KK_Skrin_id"].ToString();
                load_subskrin();
                dd_sskrin.SelectedValue = ddokdicno.Rows[0]["KK_Sskrin_id"].ToString();
                load_presubskrin();
                dd_presskrin.SelectedValue = ddokdicno.Rows[0]["KK_Spreskrin_id"].ToString();
                txtlink.Text = ddokdicno.Rows[0]["KK_preSkrin1_link"].ToString();
                txtlink_add.Text = ddokdicno.Rows[0]["KK_preSkrin1_link1"].ToString();
                txt_position.Text = ddokdicno.Rows[0]["position"].ToString();
                dd_sts.SelectedValue = ddokdicno.Rows[0]["Status"].ToString();
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

    protected void sel_mskrin(object sender, EventArgs e)
    {
        load_subskrin();
    }

    protected void sel_sskrin(object sender, EventArgs e)
    {
        load_presubskrin();
    }


    protected void sel_mskrin1(object sender, EventArgs e)
    {
        DataTable ddokdicno = new DataTable();
        ddokdicno = Dblog.Ora_Execute_table("select top(1) (position + 1) as pos from KK_PID_presub1_Skrin where Kk_Skrin_id='" + dd_skrin.SelectedValue + "' and Kk_Sskrin_id='" + dd_sskrin.SelectedValue + "' and Kk_Spreskrin_id='" + dd_presskrin.SelectedValue + "'  order by cast(position as int) desc");
        if (ddokdicno.Rows.Count != 0)
        {
            txt_position.Text = ddokdicno.Rows[0]["pos"].ToString();
        }
        else
        {
            txt_position.Text = "1";
        }
    }
    void load_subskrin()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select * from KK_PID_sub_Skrin where status='A' and KK_Skrin_id='" + dd_skrin.SelectedValue + "' order by KK_Skrin_id";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_sskrin.DataSource = dt;
            dd_sskrin.DataTextField = "KK_Sskrin_name";
            dd_sskrin.DataValueField = "KK_Sskrin_id";
            dd_sskrin.DataBind();
            dd_sskrin.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void load_presubskrin()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select * from KK_PID_presub_Skrin where status='A' and KK_Skrin_id='" + dd_skrin.SelectedValue + "' and KK_Sskrin_id='" + dd_sskrin.SelectedValue + "' order by KK_Skrin_id";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_presskrin.DataSource = dt;
            dd_presskrin.DataTextField = "KK_Spreskrin_name";
            dd_presskrin.DataValueField = "KK_Spreskrin_id";
            dd_presskrin.DataBind();
            dd_presskrin.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void Type_Skrins()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select * from KK_PID_Skrin where status='A' order by KK_Skrin_id";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_skrin.DataSource = dt;
            dd_skrin.DataTextField = "KK_Skrin_name";
            dd_skrin.DataValueField = "KK_Skrin_id";
            dd_skrin.DataBind();
            dd_skrin.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        if (dd_skrin.SelectedValue != "" && dd_sskrin.SelectedValue != "" && txtName.Text != "")
        {
            DataTable dd1 = new DataTable();
            dd1 = DBCon.Ora_Execute_table("select top(1) (RIGHT(KK_Spreskrin1_id, 4) + 1) cnt from KK_PID_presub1_Skrin order by KK_Spreskrin1_id desc");
            string ss1 = string.Empty;
            ss1 = dd1.Rows[0]["cnt"].ToString();
            var count = "K" + ss1.PadLeft(4, '0');

            string kdakaun = string.Empty;
            string Inssql = "INSERT INTO KK_PID_presub1_Skrin (KK_Skrin_id,KK_Sskrin_id,KK_Spreskrin_id,KK_Spreskrin1_id,KK_Spreskrin1_name,KK_preskrin1_link,KK_preSkrin1_link1,Status,crt_id,crt_dt,position) Values ('" + dd_skrin.SelectedValue + "','" + dd_sskrin.SelectedValue + "','" + dd_presskrin.SelectedValue + "','" + count + "','" + txtName.Text + "','"+ txtlink.Text + "','" + txtlink_add.Text + "','" + dd_sts.SelectedValue + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','"+ txt_position.Text +"')";
            Status = DBCon.Ora_Execute_CommamdText(Inssql);
            if (Status == "SUCCESS")
            {
                    Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                    Session["validate_success"] = "SUCCESS";
                    Response.Redirect("../Pentadbiran/kw_presub1_skrin_view.aspx");
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Bidang.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    protected void btb_kmes_Click(object sender, EventArgs e)
    {
        if (dd_skrin.SelectedValue != "" && dd_sskrin.SelectedValue != "" && txtName.Text != "")
        {
            if (dd_sts.SelectedValue != "T")
            {
                string Inssql = "UPDATE KK_PID_presub1_Skrin SET KK_Skrin_id = '" + dd_skrin.SelectedValue + "',KK_Sskrin_id = '" + dd_sskrin.SelectedValue + "',KK_Spreskrin_id = '" + dd_presskrin.SelectedValue + "',KK_Spreskrin1_name='" + txtName.Text + "',KK_preskrin1_link='" + txtlink.Text + "',KK_preskrin1_link1='" + txtlink_add.Text + "',position='" + txt_position.Text + "',Status= '" + dd_sts.SelectedValue + "', upd_id = '" + Session["New"].ToString() + "',upd_dt = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE ID = '" + lbl_name.Text + "'";
                Status = DBCon.Ora_Execute_CommamdText(Inssql);
                if (Status == "SUCCESS")
                {
                    Session["validate_success"] = "SUCCESS";
                    Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                    Response.Redirect("../Pentadbiran/kw_presub1_skrin_view.aspx");
                }
            }
            else
            {
                DataTable dd3 = new DataTable();
                dd3 = DBCon.Ora_Execute_table("select * from KK_PID_presub1_Skrin where ID = '" + lbl_name.Text + "'");
                DataTable dd3_chk = new DataTable();
                dd3_chk = DBCon.Ora_Execute_table("select * from KK_Role_skrins where psub1_skrin_id='" + dd3.Rows[0]["KK_Spreskrin1_id"].ToString() + "'");

                if (dd3_chk.Rows.Count == 0)
                {
                    string Inssql = "UPDATE KK_PID_presub1_Skrin SET KK_Skrin_id = '" + dd_skrin.SelectedValue + "',KK_Sskrin_id = '" + dd_sskrin.SelectedValue + "',KK_Spreskrin_id = '" + dd_presskrin.SelectedValue + "',KK_Spreskrin1_name='" + txtName.Text + "',KK_preskrin1_link='" + txtlink.Text + "',KK_preskrin1_link1='" + txtlink_add.Text + "',position='" + txt_position.Text + "',Status= '" + dd_sts.SelectedValue + "', upd_id = '" + Session["New"].ToString() + "',upd_dt = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE ID = '" + lbl_name.Text + "'";
                    Status = DBCon.Ora_Execute_CommamdText(Inssql);
                    if (Status == "SUCCESS")
                    {
                        Session["validate_success"] = "SUCCESS";
                        Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                        Response.Redirect("../Pentadbiran/kw_presub1_skrin_view.aspx");
                    }
                }
                else
                {
                    dd_sts.SelectedValue = "A";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Status Not Updated, Please Remove Role First.!',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Bidang.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
        Button4.Visible = false;
        btb_kmes.Visible = true;
    }


  
    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Pentadbiran/kw_presub1_skrin.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Pentadbiran/kw_presub1_skrin_view.aspx");
    }

    
}