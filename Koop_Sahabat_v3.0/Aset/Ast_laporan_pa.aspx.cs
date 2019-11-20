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
using System.Data.OleDb;
using System.IO;
using System.Net;

public partial class Ast_laporan_pa : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection dbcon = new DBConnection();
    StudentWebService service = new StudentWebService();

    string useid = string.Empty, status = string.Empty;
    string dar_dt = string.Empty, seh_dt = string.Empty;
    string s_qry = string.Empty, fdate = string.Empty, tdate = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Btn_Carian);
        string script = " $(function () {$('.select2').select2();})";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;
                kat();
                useid = Session["New"].ToString();
               
             
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }



    void kat()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select ast_kategori_code,upper(ast_kategori_desc) as ast_kategori_desc from Ref_ast_kategori where status = 'A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList1.DataSource = dt;
            DropDownList1.DataTextField = "ast_kategori_desc";
            DropDownList1.DataValueField = "ast_kategori_code";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


  


    protected void clk_srch(object sender, EventArgs e)
    {
        try
        {
            if (txt_dar.Text != "" && txt_seh.Text != "")
            {
                //Path
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                if (txt_dar.Text != "" && txt_seh.Text != "")
                {
                    DateTime ft = DateTime.ParseExact(txt_dar.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    fdate = ft.ToString("yyyy-MM-dd");
                    DateTime td = DateTime.ParseExact(txt_seh.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    tdate = td.ToString("yyyy-MM-dd");
                }
                else
                {
                    fdate = "";
                    tdate = "";
                }
                DataTable s_det = new DataTable();
                if (txt_dar.Text != "" && txt_seh.Text != "" && DropDownList1.SelectedValue == "")
                {
                    dt = dbcon.Ora_Execute_table("select ak.ast_kategori_desc,ask.ast_subkateast_desc,ja.ast_jeniaset_desc,hj.hr_jaba_desc,ho.org_name,a.cmp_cost_amt,'' as tot_cst,a.cmp_asset_id from (select * from ast_complaint where cmp_complaint_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fdate + "'), 0) and cmp_complaint_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tdate + "'), +1)) as a left join hr_staff_profile as sp on sp.stf_staff_no=a.cmp_staff_no left join Ref_ast_kategori as ak on ak.ast_kategori_code=a.cmp_cat_cd left join Ref_ast_sub_kategri_Aset as ask on ask.ast_subkateast_Code=a.cmp_sub_cat_cd left join Ref_ast_jenis_aset as ja on ja.ast_jeniaset_code=a.cmp_type_cd left join hr_organization as ho on ho.org_gen_id=sp.str_curr_org_cd left join Ref_hr_jabatan as hj on hj.hr_jaba_Code=sp.stf_curr_dept_cd");
                    ds.Tables.Add(dt);

                    s_det = dbcon.Ora_Execute_table("select sum(cmp_cost_amt) as tot_cst1 from ast_complaint where cmp_complaint_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fdate + "'), 0) and cmp_complaint_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tdate + "'), +1)");

                }
                else if (txt_dar.Text != "" && txt_seh.Text != "" && DropDownList1.SelectedValue != "")
                {
                    dt = dbcon.Ora_Execute_table("select ak.ast_kategori_desc,ask.ast_subkateast_desc,ja.ast_jeniaset_desc,hj.hr_jaba_desc,ho.org_name,a.cmp_cost_amt,'' as tot_cst,a.cmp_asset_id from (select * from ast_complaint where cmp_complaint_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fdate + "'), 0) and cmp_complaint_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tdate + "'), +1) and cmp_cat_cd='" + DropDownList1.SelectedValue + "') as a left join hr_staff_profile as sp on sp.stf_staff_no=a.cmp_staff_no left join Ref_ast_kategori as ak on ak.ast_kategori_code=a.cmp_cat_cd left join Ref_ast_sub_kategri_Aset as ask on ask.ast_subkateast_Code=a.cmp_sub_cat_cd left join Ref_ast_jenis_aset as ja on ja.ast_jeniaset_code=a.cmp_type_cd left join hr_organization as ho on ho.org_gen_id=sp.str_curr_org_cd left join Ref_hr_jabatan as hj on hj.hr_jaba_Code=sp.stf_curr_dept_cd");
                    ds.Tables.Add(dt);

                    s_det = dbcon.Ora_Execute_table("select sum(cmp_cost_amt) as tot_cst1 from ast_complaint where cmp_complaint_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fdate + "'), 0) and cmp_complaint_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tdate + "'), +1) and cmp_cat_cd='" + DropDownList1.SelectedValue + "'");
                }
                else
                {
                    dt = dbcon.Ora_Execute_table("select ak.ast_kategori_desc,ask.ast_subkateast_desc,ja.ast_jeniaset_desc,hj.hr_jaba_desc,ho.org_name,a.cmp_cost_amt,'' as tot_cst,a.cmp_asset_id from (select * from ast_complaint where cmp_complaint_dt>=DATEADD(day, DATEDIFF(day, 0, ''), 0) and cmp_complaint_dt<=DATEADD(day, DATEDIFF(day, 0, ''), +1) and cmp_cat_cd='') as a left join hr_staff_profile as sp on sp.stf_staff_no=a.cmp_staff_no left join Ref_ast_kategori as ak on ak.ast_kategori_code=a.cmp_cat_cd left join Ref_ast_sub_kategri_Aset as ask on ask.ast_subkateast_Code=a.cmp_sub_cat_cd left join Ref_ast_jenis_aset as ja on ja.ast_jeniaset_code=a.cmp_type_cd left join hr_organization as ho on ho.org_gen_id=sp.str_curr_org_cd left join Ref_hr_jabatan as hj on hj.hr_jaba_Code=sp.stf_curr_dept_cd");
                    ds.Tables.Add(dt);

                    s_det = dbcon.Ora_Execute_table("select sum(cmp_cost_amt) as tot_cst1 from ast_complaint where cmp_complaint_dt>=DATEADD(day, DATEDIFF(day, 0, ''), 0) and cmp_complaint_dt<=DATEADD(day, DATEDIFF(day, 0, ''), +1)");
                }


                //RptviwerLKSENARI.Reset();
                //RptviwerLKSENARI.LocalReport.Refresh();
                List<DataRow> listResult = dt.AsEnumerable().ToList();
                listResult.Count();
                int countRow = 0;
                countRow = listResult.Count();

                string ss1 = string.Empty;
                if (DropDownList1.SelectedValue != "")
                {
                    ss1 = DropDownList1.SelectedItem.Text;
                }
                else
                {
                    ss1 = "SEMUA";
                }
                RptviwerLKSENARI.LocalReport.DataSources.Clear();
                if (countRow != 0)
                {
                    disp_hdr_txt.Visible = true;
                    RptviwerLKSENARI.LocalReport.ReportPath = "Aset/ast_lap.rdlc";
                    ReportDataSource rds = new ReportDataSource("astlap", dt);
                    ReportParameter[] rptParams = new ReportParameter[]{
                    new ReportParameter("d1", double.Parse(s_det.Rows[0]["tot_cst1"].ToString()).ToString("C").Replace("$","").Replace("RM","")),
                     new ReportParameter("d2", ss1),
                      new ReportParameter("d3", txt_dar.Text),
                       new ReportParameter("d4", txt_seh.Text)
                     };

                    RptviwerLKSENARI.LocalReport.SetParameters(rptParams);
                    RptviwerLKSENARI.LocalReport.DataSources.Add(rds);
                    //Refresh
                    //RptviwerLKSENARI.LocalReport.DisplayName = " ASET_UNTUK_DILUPUSKAN_" + DateTime.Now.ToString("ddMMyyyy");
                    RptviwerLKSENARI.LocalReport.Refresh();
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string extension;
                    string filename;

                   
                        filename = string.Format("{0}.{1}", "ASET_UNTUK_DILUPUSKAN_" + DateTime.Now.ToString("ddMMyyyy") + ".", "pdf");
                        byte[] bytes = RptviwerLKSENARI.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                        Response.Buffer = true;
                        Response.Clear();
                        Response.ContentType = mimeType;
                        Response.AddHeader("content-disposition", "attachment; filename=" + filename);
                        Response.BinaryWrite(bytes);
                        Response.Flush();
                        Response.End();
                    
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Medan Input Adalah Mandatori.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
            //Response.Redirect("LK_SENARI.aspx");
        }
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_laporan_pa.aspx");
    }

}