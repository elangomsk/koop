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

public partial class kw_main_skrin : System.Web.UI.Page
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
                else
                {
                    view_details_add();
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
            ddokdicno = Dblog.Ora_Execute_table("select * from KK_PID_Skrin where id = '" + ogid + "'");
            if (ddokdicno.Rows.Count != 0)
            {
                txtName.Text = ddokdicno.Rows[0]["KK_Skrin_name"].ToString();
                txt_position.Text = ddokdicno.Rows[0]["position"].ToString();
                dd_sts.SelectedValue = ddokdicno.Rows[0]["Status"].ToString();
                TextBox1.Text = ddokdicno.Rows[0]["skrin_ikon"].ToString();
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

    void view_details_add()
    {
        DataTable ddokdicno = new DataTable();
        ddokdicno = Dblog.Ora_Execute_table("select top(1) (position + 1) as pos from KK_PID_Skrin order by position desc");
        if(ddokdicno.Rows.Count != 0)
        {
            txt_position.Text = ddokdicno.Rows[0]["pos"].ToString();
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        if (txtName.Text != "" )
        {
            DataTable dd1 = new DataTable();
            dd1 = DBCon.Ora_Execute_table("select count(*) + 1 as cnt from KK_PID_Skrin");
            string ss1 = string.Empty;
            ss1 = dd1.Rows[0]["cnt"].ToString();
            var count = "M" + ss1.PadLeft(4, '0');

            string kdakaun = string.Empty;
            string Inssql = "INSERT INTO KK_PID_Skrin (KK_Skrin_id,KK_Skrin_name,Status,crt_id,crt_dt,position,skrin_ikon) VALUES ('" + count + "','" + txtName.Text + "','" + dd_sts.SelectedValue + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','"+ txt_position.Text + "','"+ TextBox1.Text +"')";
            Status = DBCon.Ora_Execute_CommamdText(Inssql);
            if (Status == "SUCCESS")
            {
                    Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                    Session["validate_success"] = "SUCCESS";
                    Response.Redirect("../Pentadbiran/kw_main_skrin_view.aspx");
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Bidang.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    protected void btb_kmes_Click(object sender, EventArgs e)
    {
        if (txtName.Text != "")
        {
            DataTable dd1 = new DataTable();
            dd1 = DBCon.Ora_Execute_table("select * from KK_PID_Skrin WHERE ID = '" + lbl_name.Text + "'");
            string sval1 = string.Empty, sval2 = string.Empty;
            if(dd1.Rows[0]["position"].ToString() == txt_position.Text)
            {
                sval1 = txt_position.Text;
            }
            else
            {
                DataTable dd2 = new DataTable();
                dd2 = DBCon.Ora_Execute_table("select * from KK_PID_Skrin where position >= '"+ txt_position.Text + "' and ID != '" + lbl_name.Text + "'");
                if(dd2.Rows.Count > 0)
                {
                    for (int k = 0; k < dd2.Rows.Count; k++)
                    {

                        DataTable dd3 = new DataTable();
                        dd3 = DBCon.Ora_Execute_table("select * from KK_PID_Skrin where ID = '" + dd2.Rows[k]["ID"].ToString() + "'");
                        if(dd3.Rows.Count != 0)
                        {
                            double ss1 = (double.Parse(txt_position.Text) + k + 1);
                            sval2 = Convert.ToString(ss1);
                            string Inssql1 = "UPDATE KK_PID_Skrin SET position='" + sval2 + "', upd_id = '" + Session["New"].ToString() + "',upd_dt = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE ID = '" + dd2.Rows[k]["ID"].ToString() + "'";
                            Status = DBCon.Ora_Execute_CommamdText(Inssql1);
                        }
                    }
                }
                sval1 = txt_position.Text;
            }

            if (dd_sts.SelectedValue != "T")
            {
                string Inssql = "UPDATE KK_PID_Skrin SET skrin_ikon='" + TextBox1.Text + "',KK_Skrin_name = '" + txtName.Text.Replace("'", "''") + "',position='" + sval1 + "',Status= '" + dd_sts.SelectedValue + "', upd_id = '" + Session["New"].ToString() + "',upd_dt = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE ID = '" + lbl_name.Text + "'";
                Status = DBCon.Ora_Execute_CommamdText(Inssql);
                if (Status == "SUCCESS")
                {
                    Session["validate_success"] = "SUCCESS";
                    Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                    Response.Redirect("../Pentadbiran/kw_main_skrin_view.aspx");
                }
            }
            else
            {
                DataTable dd3 = new DataTable();
                dd3 = DBCon.Ora_Execute_table("select * from KK_PID_Skrin where ID = '" + lbl_name.Text + "'");
                DataTable dd3_chk = new DataTable();
                dd3_chk = DBCon.Ora_Execute_table("select * from KK_Role_skrins where skrin_id='" + dd3.Rows[0]["KK_Skrin_id"].ToString() + "'");

                if (dd3_chk.Rows.Count == 0)
                {
                    string Inssql = "UPDATE KK_PID_Skrin SET skrin_ikon='" + TextBox1.Text + "',KK_Skrin_name = '" + txtName.Text.Replace("'", "''") + "',position='" + sval1 + "',Status= '" + dd_sts.SelectedValue + "', upd_id = '" + Session["New"].ToString() + "',upd_dt = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE ID = '" + lbl_name.Text + "'";
                    Status = DBCon.Ora_Execute_CommamdText(Inssql);
                    if (Status == "SUCCESS")
                    {
                        Session["validate_success"] = "SUCCESS";
                        Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                        Response.Redirect("../Pentadbiran/kw_main_skrin_view.aspx");
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
        Response.Redirect("../Pentadbiran/kw_main_skrin.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Pentadbiran/kw_main_skrin_view.aspx");
    }

    
}