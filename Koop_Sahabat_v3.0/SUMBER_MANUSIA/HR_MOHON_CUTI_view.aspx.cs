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
using System.Text.RegularExpressions;
using System.Threading;

public partial class HR_MOHON_CUTI_view : System.Web.UI.Page
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
    string role_1 = string.Empty, role_2 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                assgn_roles();
                if (Session["validate_success"].ToString() == "SUCCESS")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('" + Session["alrt_msg"].ToString() + "',{'type': 'confirmation','title': 'Success'});", true);

                }
                Session["validate_success"] = "";
                Session["alrt_msg"] = "";
                userid = Session["New"].ToString();
                             
                grid1();
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('519','448','39')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentUICulture;
            TextInfo txtinfo = culinfo.TextInfo;

            h1_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            bb1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            bb2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            tmb_btn.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }

    void assgn_roles()
    {
        DataTable ddokdicno = new DataTable();
        ddokdicno = DBCon.Ora_Execute_table("select * from KK_User_Login where KK_userid = '" + Session["userid"].ToString() + "'");

        if (ddokdicno.Rows.Count != 0)
        {
            DataTable ddokdicno_1 = new DataTable();
            ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0059' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

            if (ddokdicno_1.Rows.Count != 0)
            {
                role_1 = ddokdicno_1.Rows[0]["ctrl_type"].ToString();
                if (ddokdicno_1.Rows[0]["Add_chk"].ToString() == "1")
                {
                    DataTable ddicno = new DataTable();
                    ddicno = DBCon.Ora_Execute_table("select stf_staff_no From hr_staff_profile where stf_staff_no='" + Session["userid"].ToString() + "' ");
                    if (ddicno.Rows.Count != 0)
                    {
                        tmb_btn.Visible = true;
                    }
                    else
                    {
                        tmb_btn.Visible = false;
                    }
                }
                else
                {
                    tmb_btn.Visible = false;
                //    Button3.Visible = false;
                }
            }
        }
    }
    void grid1()
    {
        con.Open();
        //DataTable ddicno = new DataTable();
        //ddicno = DBCon.Ora_Execute_table("select stf_staff_no From hr_staff_profile where stf_staff_no='" + Session["New"].ToString() + "' ");
        //string stffno = ddicno.Rows[0]["stf_staff_no"].ToString();
        string year = DateTime.Now.ToString("yyyy");
        SqlCommand cmd = new SqlCommand("select LP.Id,lap_application_dt,hr_jenis_desc,lap_leave_day,lap_leave_start_dt,lap_leave_end_dt,case when ISNULL(lap_approve_sts_cd,'') = '00' then 'MOHON' else hr_leave_desc end hr_leave_desc,lap_ref_no,lap_approve_sts_cd,lap_late_apply From hr_leave_application as LP left join Ref_hr_jenis_cuti as JC on JC.hr_jenis_Code=LP.lap_leave_type_cd left join Ref_hr_leave_sts as LS on LS.hr_leave_Code=LP.lap_approve_sts_cd where lap_staff_no='" + Session["New"].ToString() + "' order by lap_application_dt desc", con);
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
        }
        else
        {
            GridView2.DataSource = ds;
            GridView2.DataBind();
        }
        con.Close();
    }

   
    protected void Add_profile(object sender, EventArgs e)
    {
        Response.Redirect("../SUMBER_MANUSIA/HR_MOHON_CUTI.aspx");
    }

    protected void GridView1_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            System.Web.UI.WebControls.Label lblctrl = (System.Web.UI.WebControls.Label)e.Row.FindControl("lbl_sts_cd");
            if (lblctrl.Text == "04")
            {
                LinkButton Hlnk = e.Row.FindControl("lnkView") as LinkButton;
                Hlnk.Attributes.Add("style", "pointer-events:None; color:Black;");
                
            }
            else
            {
                LinkButton Hlnk = e.Row.FindControl("lnkView") as LinkButton;
                Hlnk.Visible = true;
            }
        }
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnButton = sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
            System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("lbl_id");
            string lblid1 = lblTitle.Text;
            DataTable ddokdicno = new DataTable();
            ddokdicno = DBCon.Ora_Execute_table("select * From hr_leave_application where Id='" + lblTitle.Text + "' ");
            if (ddokdicno.Rows.Count != 0)
            {
                string name = HttpUtility.UrlEncode(service.Encrypt(lblTitle.Text));
                //Response.Redirect("../kewengan/kw_profil_syarikat.aspx?edit={0}"+ og_genid.Text + "");
                Response.Redirect(string.Format("../SUMBER_MANUSIA/HR_MOHON_CUTI.aspx?edit={0}", name));
            }
            else
            {
                Session["validate_success"] = "";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }


}