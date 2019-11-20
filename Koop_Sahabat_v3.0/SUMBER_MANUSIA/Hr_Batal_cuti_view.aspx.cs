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

public partial class SUMBER_MANUSIA_Hr_Batal_cuti_view : System.Web.UI.Page
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
            bb1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            //bb2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }

    protected void chk_view(object sender, EventArgs e)
    {
        BindData_Grid();
        string script1 = " $(function () {  $(" + GridView1.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
    }
    protected void BindData_Grid()
    {
       
        string sqry = string.Empty;
        if(chk_assign_rkd.Checked == true)
        {
            
            sqry = "select a.Id,p.stf_staff_no,a.lap_ref_no,p.stf_name,a.lap_application_dt,j.hr_jenis_desc,a.lap_leave_day,a.lap_leave_start_dt,a.lap_leave_end_dt, case when lap_endorse_sts_cd = '01' then 'SAH' when lap_endorse_sts_cd = '02' then 'TIDAK SAH' else 'PENDING' end as psts from hr_leave_application a left join hr_staff_profile p on p.stf_staff_no=a.lap_staff_no left join hr_leave  l on a.lap_staff_no=l.lev_staff_no left join Ref_hr_jenis_cuti J on j.hr_jenis_Code=a.lap_leave_type_cd    where '" + DateTime.Now.ToString("yyyy") + "' between year(lap_leave_start_dt) and year(lap_leave_end_dt) and lap_endorse_sts_cd='01' and lap_cancel_ind = 'N' and '" + DateTime.Now.ToString("MM") + "' >= cast(Month(lap_leave_end_dt) as int) group by a.Id,p.stf_staff_no,a.lap_ref_no,p.stf_name,a.lap_application_dt,j.hr_jenis_desc,lap_leave_day,a.lap_leave_start_dt,a.lap_leave_end_dt,lap_endorse_sts_cd";
        }
        else
        {
            sqry = "select a.Id,p.stf_staff_no,a.lap_ref_no,p.stf_name,a.lap_application_dt,j.hr_jenis_desc,a.lap_leave_day,a.lap_leave_start_dt,a.lap_leave_end_dt, case when lap_endorse_sts_cd = '01' then 'SAH' when lap_endorse_sts_cd = '02' then 'TIDAK SAH' else 'PENDING' end as psts from hr_leave_application a left join hr_staff_profile p on p.stf_staff_no=a.lap_staff_no left join hr_leave  l on a.lap_staff_no=l.lev_staff_no left join Ref_hr_jenis_cuti J on j.hr_jenis_Code=a.lap_leave_type_cd    where lap_endorse_sts_cd='01' and lap_cancel_ind = 'N' group by a.Id,p.stf_staff_no,a.lap_ref_no,p.stf_name,a.lap_application_dt,j.hr_jenis_desc,lap_leave_day,a.lap_leave_start_dt,a.lap_leave_end_dt,lap_endorse_sts_cd";
        }

        //SqlCommand cmd2 = new SqlCommand("select ph.Id,stf_staff_no,stf_name,FORMAT(lap_application_dt,'dd/MM/yyyy') lap_application_dt,lap_leave_type_cd,hjc.hr_jenis_desc,lap_leave_day,FORMAT(lap_leave_start_dt,'dd/MM/yyyy') as lap_leave_start_dt,FORMAT(lap_leave_end_dt,'dd/MM/yyyy') as lap_leave_end_dt,lap_ref_no,case when lap_approve_sts_cd='00' then 'MOHON' when lap_approve_sts_cd='01' then 'LULUS' when lap_approve_sts_cd='02' then 'LULUS BERSYARAT' when lap_approve_sts_cd='03' then 'TIDAK LULUS' when lap_approve_sts_cd='04' then 'BATAL' else '' end as app_stscd,case when lap_endorse_sts_cd='01' then 'SAH' when lap_endorse_sts_cd='02' then 'TIDAK SAH' else 'WAITING' end as psts from hr_staff_profile sp Left join hr_leave_application ph on sp.stf_staff_no=ph.lap_staff_no Left join Ref_hr_jenis_cuti hjc on hjc.hr_jenis_Code=ph.lap_leave_type_cd where stf_service_end_dt='9999-12-31' and lap_approve_sts_cd in ('01')  order by lap_ref_no", con); // 26_07_2018
        SqlCommand cmd2 = new SqlCommand(sqry, con);
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
                Response.Redirect(string.Format("../SUMBER_MANUSIA/Hr_Batal_Cuti.aspx?edit={0}", name));
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