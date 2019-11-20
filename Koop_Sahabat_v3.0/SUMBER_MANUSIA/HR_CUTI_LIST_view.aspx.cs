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

public partial class HR_CUTI_LIST_view : System.Web.UI.Page
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
    string role_1 = string.Empty, role_2 = string.Empty;
    string sqry = string.Empty, fmdate = string.Empty, tmdate = string.Empty;
    string gt_val1 = string.Empty, gt_val2 = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        assgn_roles();
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {

            if (Session["New"] != null)
            {
                //txt_tkcuti.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //txt_hing.Text = DateTime.Now.ToString("dd/MM/yyyy");
                if (Session["validate_success"].ToString() == "SUCCESS")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('" + Session["alrt_msg"].ToString() + "',{'type': 'confirmation','title': 'Success'});", true);

                }
                Session["validate_success"] = "";
                Session["alrt_msg"] = "";
                userid = Session["New"].ToString();
                BindData_Grid();
                //gbind2();
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    void app_language()
    {

        //if (Session["New"] != null)
        //{
        //    DataTable ste_set = new DataTable();
        //    ste_set = DBCon.Ora_Execute_table("select * from site_settings where ID IN ('1')");

        //    DataTable gt_lng = new DataTable();
        //    gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('524','448')");

        //    CultureInfo culinfo = Thread.CurrentThread.CurrentUICulture;
        //    TextInfo txtinfo = culinfo.TextInfo;


        //    h1_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
        //    bb1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
        //    bb2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
        //}
        //else
        //{
        //    Response.Redirect("../HRMS_Login.aspx");
        //}
    }

    void assgn_roles()
    {
        if (Session["New"] != null)
        {
            DataTable ddokdicno = new DataTable();
            ddokdicno = DBCon.Ora_Execute_table("select * from KK_User_Login where KK_userid = '" + Session["userid"].ToString() + "'");

            if (ddokdicno.Rows.Count != 0)
            {
                DataTable ddokdicno_1 = new DataTable();
                ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0208' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

                if (ddokdicno_1.Rows.Count != 0)
                {
                    gt_val1 = ddokdicno_1.Rows[0]["ctrl_type"].ToString();
                }
            }

        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    protected void BindGridview(object sender, EventArgs e)
    {
            BindData_Grid();
    }
    void get_det()
    {
        
        string schk = string.Empty;
        string ssno = string.Empty;
        //DataTable ddokdicno = new DataTable();

        //ddokdicno = DBCon.Ora_Execute_table("select STUFF ((select ',''' + pos_staff_no + '''' from hr_post_his where pos_end_dt >='" + DateTime.Now.ToString("yyyy-MM-dd") +"' and '" + Session["New"].ToString() + "' IN (pos_spv_name1,pos_spv_name2)  FOR XML PATH ('')  ),1,1,'')  as scode");
        //if (Session["role_dep"].ToString() != "06")
        //{
        //    if (ddokdicno.Rows[0]["scode"].ToString() != "")
        //    {
        //        ssno = "and lap_staff_no IN(" +ddokdicno.Rows[0]["scode"].ToString() + ")";
        //    }
        //    else
        //    {
        //        ssno = "";
        //    }
        //}
        //else
        //{
        //    ssno = "";
        //}
        if(gt_val1 == "0")
        {
            ssno = "and lap_staff_no IN(" + Session["New"].ToString() + ")";
        }
        if (txt_tkcuti.Text != "")
        {
            string fdate = txt_tkcuti.Text;
            DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            fmdate = fd.ToString("yyyy-MM-dd");
        }
        if(txt_hing.Text != "")
        {
            string tdate = txt_hing.Text;
            DateTime td = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            tmdate = td.ToString("yyyy-MM-dd");
        }
       
        if (txt_tkcuti.Text != "" && txt_hing.Text != "" && DropDownList1.SelectedValue != "")
        {
            sqry = " "+ ssno + " and ISNULL(lap_approve_sts_cd,'')='" + DropDownList1.SelectedValue + "' and lap_leave_start_dt>=DATEADD(day, DATEDIFF(day, 0, '"+ fmdate + "'), 0) and lap_leave_start_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +0)";
        }
        else if (txt_tkcuti.Text == "" && txt_hing.Text == "" && DropDownList1.SelectedValue != "")
        {
            sqry = " " + ssno + " and ISNULL(lap_approve_sts_cd,'')='" + DropDownList1.SelectedValue + "'";
        }
        else if (txt_tkcuti.Text != "" && txt_hing.Text != "" && DropDownList1.SelectedValue == "00")
        {
            sqry = " " + ssno + " and lap_leave_start_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and lap_leave_start_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +0)";
        }
        else
        {
            sqry =ssno;
        }
    }
    protected void BindData_Grid()
    {

        get_det();
        con.Open();
        DataTable ddicno = new DataTable();
        SqlCommand cmd = new SqlCommand("select ph.Id,stf_staff_no,stf_name,FORMAT(lap_application_dt,'dd/MM/yyyy') lap_application_dt,lap_leave_type_cd,hjc.hr_jenis_desc,lap_leave_day,FORMAT(lap_leave_start_dt,'dd/MM/yyyy') as lap_leave_start_dt,FORMAT(lap_leave_end_dt, 'dd/MM/yyyy') as lap_leave_end_dt, lap_ref_no,case when lap_approve_sts_cd = '00' then 'MOHON' when lap_approve_sts_cd = '01' then 'SAH' when lap_approve_sts_cd = '02' then 'TIDAK SAH' when lap_approve_sts_cd = '03' then '' when lap_approve_sts_cd = '04' then 'BATAL' else '' end as app_stscd,case when lap_endorse_sts_cd = '01' then 'SAH' when lap_endorse_sts_cd = '02' then 'TIDAK SAH' when lap_endorse_sts_cd = '04' then 'BATAL' else '' end as psts,lap_late_apply from hr_leave_application ph left join hr_staff_profile sp on sp.stf_staff_no = ph.lap_staff_no Left join Ref_hr_jenis_cuti hjc on hjc.hr_jenis_Code = ph.lap_leave_type_cd where stf_service_end_dt>='" + DateTime.Now.ToString("yyyy-MM-dd") + "' and  stf_service_end_dt <= '9999-12-31' " + sqry + " order by lap_ref_no desc", con);
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
            GridView1.Rows[0].Cells[0].Text = "<center><strong>Rekod Tidak Dijumpai</strong></center>";
        }
        else
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

        con.Close();
    }

    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindData_Grid();
        GridView1.DataBind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        System.Web.UI.WebControls.Label sts_det = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label12_stsp");
        System.Web.UI.WebControls.Label app_sts_det = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label12_stsp1");
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (sts_det.Text.Trim() == "BATAL")
            {
                sts_det.Attributes.Add("Class", "label label-warning Uppercase");
            }
            else if (sts_det.Text.Trim() == "TIDAK SAH")
            {
                sts_det.Attributes.Add("Class", "label label-danger Uppercase");
            }
            else if (sts_det.Text.Trim() == "MOHON")
            {
                sts_det.Attributes.Add("Class", "label label-info Uppercase");
            }
            else if (sts_det.Text.Trim() == "SAH")
            {
                sts_det.Attributes.Add("Class", "label label-success Uppercase");
            }

            if (app_sts_det.Text.Trim() == "BATAL")
            {
                app_sts_det.Attributes.Add("Class", "label label-warning Uppercase");
            }
            else if (app_sts_det.Text.Trim() == "TIDAK SAH")
            {
                app_sts_det.Attributes.Add("Class", "label label-danger Uppercase");
            }
            else if (app_sts_det.Text.Trim() == "MOHON")
            {
                app_sts_det.Attributes.Add("Class", "label label-info Uppercase");
            }
            else if (app_sts_det.Text.Trim() == "SAH")
            {
                app_sts_det.Attributes.Add("Class", "label label-success Uppercase");
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
                string name = HttpUtility.UrlEncode(service.Encrypt(lblid1.ToString()));
                //Response.Redirect("../kewengan/kw_profil_syarikat.aspx?edit={0}"+ og_genid.Text + "");
                Response.Redirect(string.Format("../SUMBER_MANUSIA/HR_CUTI_LIST.aspx?edit={0}", name));
            }
            else
            {
                Session["validate_success"] = "";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void ctk_values(object sender, EventArgs e)
    {
        string rcount = string.Empty;
        int count1 = 0;
        get_det();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        dt = DBCon.Ora_Execute_table("select ph.Id,stf_staff_no,stf_name,FORMAT(lap_application_dt,'dd/MM/yyyy') lap_application_dt,lap_leave_type_cd,hjc.hr_jenis_desc,lap_leave_day,FORMAT(lap_leave_start_dt,'dd/MM/yyyy') as lap_leave_start_dt,FORMAT(lap_leave_end_dt, 'dd/MM/yyyy') as lap_leave_end_dt, lap_ref_no,case when lap_approve_sts_cd = '00' then 'MOHON' when lap_approve_sts_cd = '01' then 'SAH' when lap_approve_sts_cd = '02' then 'TIDAK SAH' when lap_approve_sts_cd = '03' then '' when lap_approve_sts_cd = '04' then 'BATAL' else '' end as app_stscd,case when lap_endorse_sts_cd = '01' then 'SAH' when lap_endorse_sts_cd = '02' then 'TIDAK SAH' else '' end as psts,lap_late_apply from hr_leave_application ph left join hr_staff_profile sp on sp.stf_staff_no = ph.lap_staff_no Left join Ref_hr_jenis_cuti hjc on hjc.hr_jenis_Code = ph.lap_leave_type_cd where stf_service_end_dt>='" + DateTime.Now.ToString("yyyy-MM-dd") + "' and  stf_service_end_dt <= '9999-12-31' and ph.lap_cancel_ind='N' " + sqry + " order by lap_ref_no desc");
        RptviwerStudent.Reset();
        ds.Tables.Add(dt);

        List<DataRow> listResult = dt.AsEnumerable().ToList();
        listResult.Count();
        int countRow = 0;
        countRow = listResult.Count();
        string sts_v1 = string.Empty, sts_v2 = string.Empty;
        if (DropDownList1.SelectedValue == "")
        {
            sts_v1 = "-";
        }
        else
        {
            sts_v1 = DropDownList1.SelectedItem.Text;
        }


        RptviwerStudent.LocalReport.DataSources.Clear();
        if (countRow != 0)
        {
            RptviwerStudent.LocalReport.ReportPath = "SUMBER_MANUSIA/cuti_list.rdlc";
            ReportDataSource rds = new ReportDataSource("hr_cuti_info", dt);
            ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("s1",txt_tkcuti.Text),
                     new ReportParameter("s2",txt_hing.Text),
                     new ReportParameter("s3",sts_v1)

                     };
            RptviwerStudent.LocalReport.SetParameters(rptParams);
            RptviwerStudent.LocalReport.DataSources.Add(rds);
            RptviwerStudent.LocalReport.Refresh();

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            string filename;
       
            if (sel_frmt.SelectedValue == "02")
            {
                StringBuilder builder = new StringBuilder();
                string strFileName = string.Format("{0}.{1}", "Semakan_Cuti_" + DateTime.Now.ToString("ddMMyyyy") + "", "csv");
                builder.Append("NAMA KAKITANGAN,NO KAKITANGAN, NO RUJUKAN,TARIKH MOHON, JENIS CUTI,HARI CUTI,TARIKH MULA, TARIKH SEHINGGA,LEWAT MOHON (HARI),STATUS KELULUSAN,STATUS PENGESAHAN" + Environment.NewLine);
                for (int k = 0; k <= (dt.Rows.Count - 1); k++)
                {
                    builder.Append(dt.Rows[k]["stf_name"].ToString() + " , " + dt.Rows[k]["stf_staff_no"].ToString() + "," + dt.Rows[k]["lap_ref_no"].ToString() + "," + dt.Rows[k]["lap_application_dt"].ToString() + "," + dt.Rows[k]["hr_jenis_desc"].ToString().ToUpper() + "," + dt.Rows[k]["lap_leave_day"].ToString() + "," + dt.Rows[k]["lap_leave_start_dt"].ToString() + "," + dt.Rows[k]["lap_leave_end_dt"].ToString() + "," + dt.Rows[k]["lap_late_apply"].ToString() + "," + dt.Rows[k]["app_stscd"].ToString() + "," + dt.Rows[k]["psts"].ToString() + Environment.NewLine);
                }
                Response.Clear();
                Response.ContentType = "text/csv";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + strFileName);
                Response.Write(builder.ToString());
                Response.End();
            }
            else if (sel_frmt.SelectedValue == "01")
            {
                filename = string.Format("{0}.{1}", "Semakan_Cuti_" + DateTime.Now.ToString("ddMMyyyy") + "", "pdf");
                byte[] bytes = RptviwerStudent.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=" + filename);
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }
        }
        else
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }

        BindData_Grid();
    }

    protected void Reset_btn(object sender, EventArgs e)
    {
        txt_tkcuti.Text = "";
        txt_hing.Text = "";
        DropDownList1.SelectedValue = "00";
        BindData_Grid();
    }
}