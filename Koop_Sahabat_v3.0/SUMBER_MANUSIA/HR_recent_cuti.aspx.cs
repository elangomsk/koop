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

public partial class HR_recent_cuti : System.Web.UI.Page
{

    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    DataTable dt = new DataTable();
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
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                if (Session["validate_success"].ToString() == "SUCCESS")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('" + Session["alrt_msg"].ToString() + "',{'type': 'confirmation','title': 'Success'});", true);

                }
                Session["validate_success"] = "";
                Session["alrt_msg"] = "";
                userid = Session["New"].ToString();
                BindData_Grid();
               // grid1();
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('1410','448')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentUICulture;
            TextInfo txtinfo = culinfo.TextInfo;


            //h1_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            //bb1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            //bb2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }

    protected void BindData_Grid()
    {

        string sqry = string.Empty;
        //if(Session["clder_id"].ToString() == "1")
        //{
         sqry = "";
        //}
        //else
        //{
           // sqry = "where stf_staff_no='"+ Session["New"].ToString() + "'";
        //}
        DateTime now = DateTime.Now;
        var startDate = new DateTime(now.Year, now.Month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);

        //SqlCommand cmd2 = new SqlCommand("select ph.Id,stf_staff_no,stf_name,FORMAT(lap_application_dt,'dd/MM/yyyy') lap_application_dt,lap_leave_type_cd,hjc.hr_jenis_desc,lap_leave_day,FORMAT(lap_leave_start_dt,'dd/MM/yyyy') as lap_leave_start_dt,FORMAT(lap_leave_end_dt, 'dd/MM/yyyy') as lap_leave_end_dt, lap_ref_no,case when lap_approve_sts_cd = '00' then 'MOHON' when lap_approve_sts_cd = '01' then 'LULUS' when lap_approve_sts_cd = '02' then 'LULUS BERSYARAT' when lap_approve_sts_cd = '03' then 'TIDAK LULUS' when lap_approve_sts_cd = '04' then 'BATAL' else '' end as app_stscd,case when lap_endorse_sts_cd = '01' then 'LULUS' when lap_endorse_sts_cd = '02' then 'TIDAK LULUS' else 'WAITING' end as psts from hr_leave_application ph left join hr_staff_profile sp on sp.stf_staff_no = ph.lap_staff_no Left join Ref_hr_jenis_cuti hjc on hjc.hr_jenis_Code = ph.lap_leave_type_cd where stf_service_end_dt = '9999-12-31' and ph.lap_cancel_ind='N' and ISNULL(lap_endorse_sts_cd,'') ='' order by lap_ref_no", con);
        

        string sql_qry1_pub = string.Empty;
        string sql_qry = string.Empty;
        sql_qry1_pub = "select top 1 ctrl_type from KK_PID_Kumpulan where KK_kumpulan_id IN('" + Session["roles"].ToString().Replace(",", "','") + "')";
        System.Data.SqlClient.SqlCommand cmd_3 = new System.Data.SqlClient.SqlCommand(sql_qry1_pub, con);
        DataTable ddicno1 = new DataTable();
        ddicno1 = DBCon.Ora_Execute_table(sql_qry1_pub);
        if (ddicno1.Rows[0]["ctrl_type"].ToString() == "1") { 
            sql_qry = "select * from (select b.Id,stf_staff_no,stf_name,FORMAT(lap_application_dt,'dd/MM/yyyy') lap_application_dt,lap_leave_type_cd,hjc.hr_jenis_desc,lap_leave_day,FORMAT(lap_leave_start_dt,'dd/MM/yyyy') as lap_leave_start_dt,FORMAT(lap_leave_end_dt, 'dd/MM/yyyy') as lap_leave_end_dt, lap_ref_no,case when lap_approve_sts_cd = '00' then 'MOHON' when lap_approve_sts_cd = '01' then 'SAH' when lap_approve_sts_cd = '02' then 'TIDAK SAH' else 'PENDING' end as app_stscd,case when lap_endorse_sts_cd = '01' then 'SAH' when lap_endorse_sts_cd = '02' then 'TIDAK SAH' else 'PENDING' end as psts  from (select * from hr_staff_profile where stf_service_end_dt>='" + DateTime.Now.ToString("yyyy-MM-dd") + "' and  stf_service_end_dt <= '9999-12-31' ) as a inner join (select * from hr_leave_application where lap_cancel_ind='N' and  lap_leave_start_dt>=DATEADD(day, DATEDIFF(day, 0, '" + startDate.ToString("yyyy/MM/dd") + "'), 0) and lap_leave_start_dt<=DATEADD(day, DATEDIFF(day, 0, '" + endDate.ToString("yyyy/MM/dd") + "'), +1)) as b on b.lap_staff_no=a.stf_staff_no Left join Ref_hr_jenis_cuti hjc on hjc.hr_jenis_Code = b.lap_leave_type_cd ) b " + sqry + " order by lap_leave_start_dt desc";
        }
        else
        {
            sql_qry = "select * from (select b.Id,stf_staff_no,stf_name,FORMAT(lap_application_dt,'dd/MM/yyyy') lap_application_dt,lap_leave_type_cd,hjc.hr_jenis_desc,lap_leave_day,FORMAT(lap_leave_start_dt,'dd/MM/yyyy') as lap_leave_start_dt,FORMAT(lap_leave_end_dt, 'dd/MM/yyyy') as lap_leave_end_dt, lap_ref_no,case when lap_approve_sts_cd = '00' then 'MOHON' when lap_approve_sts_cd = '01' then 'SAH' when lap_approve_sts_cd = '02' then 'TIDAK SAH' else 'PENDING' end as app_stscd,case when lap_endorse_sts_cd = '01' then 'SAH' when lap_endorse_sts_cd = '02' then 'TIDAK SAH' else 'PENDING' end as psts  from (select * from hr_staff_profile where stf_service_end_dt>='" + DateTime.Now.ToString("yyyy-MM-dd") + "' and  stf_service_end_dt <= '9999-12-31' ) as a inner join (select * from hr_leave_application where lap_cancel_ind='N'  and lap_staff_no = '"+ Session["New"].ToString() + "' and  lap_leave_start_dt>=DATEADD(day, DATEDIFF(day, 0, '" + startDate.ToString("yyyy/MM/dd") + "'), 0) and lap_leave_start_dt<=DATEADD(day, DATEDIFF(day, 0, '" + endDate.ToString("yyyy/MM/dd") + "'), +1)) as b on b.lap_staff_no=a.stf_staff_no Left join Ref_hr_jenis_cuti hjc on hjc.hr_jenis_Code = b.lap_leave_type_cd ) b " + sqry + " order by lap_leave_start_dt desc";
        }
        // SqlCommand cmd2 = new SqlCommand("select * from (select b.Id,stf_staff_no,stf_name,FORMAT(lap_application_dt,'dd/MM/yyyy') lap_application_dt,lap_leave_type_cd,hjc.hr_jenis_desc,lap_leave_day,FORMAT(lap_leave_start_dt,'dd/MM/yyyy') as lap_leave_start_dt,FORMAT(lap_leave_end_dt, 'dd/MM/yyyy') as lap_leave_end_dt, lap_ref_no,case when lap_approve_sts_cd = '00' then 'MOHON' when lap_approve_sts_cd = '01' then 'SAH' when lap_approve_sts_cd = '02' then 'TIDAK SAH' else 'PENDING' end as app_stscd,case when lap_endorse_sts_cd = '01' then 'SAH' when lap_endorse_sts_cd = '02' then 'TIDAK SAH' else 'PENDING' end as psts  from (select * from hr_staff_profile where stf_service_end_dt>='" + DateTime.Now.ToString("yyyy-MM-dd") + "' and  stf_service_end_dt <= '9999-12-31' ) as a inner join (select * from hr_leave_application where lap_cancel_ind='N' and  lap_leave_start_dt>=DATEADD(day, DATEDIFF(day, 0, '" + startDate.ToString("yyyy/MM/dd") + "'), 0) and lap_leave_start_dt<=DATEADD(day, DATEDIFF(day, 0, '" + endDate.ToString("yyyy/MM/dd") + "'), +1)) as b on b.lap_staff_no=a.stf_staff_no Left join Ref_hr_jenis_cuti hjc on hjc.hr_jenis_Code = b.lap_leave_type_cd ) b " + sqry + " order by lap_leave_start_dt desc", con);
        SqlCommand cmd2 = new SqlCommand(sql_qry,con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            GridView1.DataSource = ds2;
            GridView1.DataBind();
            int columncount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView1.Rows[0].Cells[0].Text = "<strong><center>TIADA MAKLUMAT DIJUMPAI</center></strong>";
        }
        else
        {
            GridView1.DataSource = ds2;
            GridView1.DataBind();
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        System.Web.UI.WebControls.Label sts = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label12_sts");
        System.Web.UI.WebControls.Label p_sts = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label12_stsp");
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (sts.Text != "")
            {
                if (sts.Text == "LULUS")
                {
                    sts.ForeColor = Color.FromName("Green");
                }
                else
                {
                    sts.ForeColor = Color.FromName("Red");
                }
            }

            if (p_sts.Text != "")
            {
                if (p_sts.Text == "LULUS")
                {
                    p_sts.ForeColor = Color.FromName("Green");
                }
                else
                {
                    p_sts.ForeColor = Color.FromName("Red");
                }
            }
        }
    }
    //protected void lnkView_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        LinkButton btnButton = sender as LinkButton;
    //        GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
    //        System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("lbl_id");
    //        string lblid1 = lblTitle.Text;
    //        DataTable ddokdicno = new DataTable();
    //        ddokdicno = DBCon.Ora_Execute_table("select * From hr_leave_application where Id='" + lblTitle.Text + "' ");
    //        if (ddokdicno.Rows.Count != 0)
    //        {
    //            string name = HttpUtility.UrlEncode(service.Encrypt(lblTitle.Text));
    //            //Response.Redirect("../kewengan/kw_profil_syarikat.aspx?edit={0}"+ og_genid.Text + "");
    //            Response.Redirect(string.Format("../SUMBER_MANUSIA/HR_SEMAKAN.aspx?edit={0}", name));
    //        }
    //        else
    //        {
    //            Session["validate_success"] = "";
    //            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }

    //}
}