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

public partial class kw_jenis_cukai : System.Web.UI.Page
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
        app_language();
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;
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
    void app_language()

    {
        if (Session["New"] != null)
        {
            DataTable ste_set = new DataTable();
            ste_set = DBCon.Ora_Execute_table("select * from site_settings where ID IN ('1')");

            DataTable gt_lng = new DataTable();
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('715','705','1626','1486','61','15','77')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
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
        Button1.Visible = false;
        try
        {
            string lblid1 = lbl_name.Text;
            DataTable ddokdicno = new DataTable();
            ddokdicno = DBCon.Ora_Execute_table("select * From KW_Ref_Jenis_Cukai where Id='" + lblid1 + "' ");
            if (ddokdicno.Rows.Count != 0)
            {
                Button2.Visible = true;
                
                Button4.Text = "Kemaskini";
                ver_id.Text = "1";
                get_id.Text = lblid1;
                TextBox8.Text = ddokdicno.Rows[0]["Ref_Jenis_cukai"].ToString();
                DropDownList2.SelectedValue = ddokdicno.Rows[0]["Status"].ToString();
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
            if (TextBox8.Text != "")
            {
                if (ver_id.Text == "0")
                {
                    DataTable ddokdicno1 = new DataTable();
                    ddokdicno1 = DBCon.Ora_Execute_table("select * From KW_Ref_Jenis_Cukai where Ref_Jenis_cukai='" + TextBox8.Text + "'");
                    if (ddokdicno1.Rows.Count == 0)
                    {
                        string Inssql1 = "insert into KW_Ref_Jenis_Cukai(Ref_Jenis_cukai,Status,crt_id,cr_dt) values ('" + TextBox8.Text.Replace("'", "''") + "','" + DropDownList2.SelectedValue + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        Status1 = DBCon.Ora_Execute_CommamdText(Inssql1);
                        if (Status1 == "SUCCESS")
                        {
                        Session["validate_success"] = "SUCCESS";
                        Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                        Response.Redirect("../kewengan/kw_jenis_cukai_view.aspx");
                        
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Rekod Not Insert.');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Rekod Sudah Ada.');", true);
                    }
                }
                else
                {
                    string Inssql1 = "UPDATE KW_Ref_Jenis_Cukai set Ref_Jenis_cukai='" + TextBox8.Text.Replace("'", "''") + "',Status='" + DropDownList2.SelectedValue + "',upd_id='" + Session["New"].ToString() + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Id = '" + get_id.Text + "'";
                    Status1 = DBCon.Ora_Execute_CommamdText(Inssql1);
                    if (Status1 == "SUCCESS")
                    {
                    Session["validate_success"] = "SUCCESS";
                    Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                    Response.Redirect("../kewengan/kw_jenis_cukai_view.aspx");
                    
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
        Response.Redirect("../kewengan/kw_jenis_cukai.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../kewengan/kw_jenis_cukai_view.aspx");
    }

    
}