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

public partial class Ast_penembetan_view : System.Web.UI.Page
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
    string lblid1 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                if (Session["validate_success"].ToString() == "SUCCESS")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('"+ Session["alrt_msg"].ToString() + "',{'type': 'confirmation','title': 'Success'});", true);

                }
                Session["validate_success"] = "";
                Session["alrt_msg"] = "";
                userid = Session["New"].ToString();
                BindData_jenis();
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    //protected void gvSelected_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    //{
    //    GridView1.PageIndex = e.NewPageIndex;
    //    GridView1.DataBind();
    //    BindData_jenis();
    //}
    protected void BindData_jenis()
    {
        con.Open();
        qry1 = "select s1.sas_asset_id,s2.stf_name,s1.sas_qty,s3.ast_kategori_desc,Format(s1.sas_allocate_dt,'dd/MM/yyyy') sas_allocate_dt,'Click' as clk from ast_staff_asset s1 left join hr_staff_profile s2 on s2.stf_staff_no=s1.sas_staff_no left join Ref_ast_kategori s3 on s3.ast_kategori_code=s1.sas_asset_cat_cd ";
        SqlCommand cmd = new SqlCommand("" + qry1 + "", con);
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
            GridView1.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
            //btn_hups.Visible = false;
        }
        else
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        con.Close();
    }


    protected void lblSubItem_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnButton = sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
            System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("gd_2");
            lblid1 = lblTitle.Text;
            if (lblid1 != "")
            {
                gen_barcode();
            }

        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod yang dipilih sekarang Digunakan Oleh Meja Lain Berkaitan jadi tidak Padam.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }


  
    void gen_barcode()
    {
        try
        {
          
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            //dt = DBCon.Ora_Execute_table("select ho.org_name,rk.ast_kategori_desc,rja.ast_jeniaset_desc,aca.cas_asset_desc,a.sas_asset_id,a.sas_curr_price_amt,a.sas_asset_cat_cd,a.sas_asset_sub_cat_cd,a.sas_asset_type_cd,a.sas_asset_cd,a.sas_org_id, case a.sas_asset_cat_cd when '01' then (select FORMAT(com_reg_dt,'dd/MM/yyyy', 'en-us') as reg_dt from ast_component where com_asset_cat_cd=a.sas_asset_cat_cd and com_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and com_asset_type_cd=a.sas_asset_type_cd and com_asset_cd=a.sas_asset_cd) when '02' then (select FORMAT(car_reg_dt,'dd/MM/yyyy', 'en-us') as reg_dt from ast_car where car_asset_cat_cd=a.sas_asset_cat_cd and car_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and car_asset_type_cd=a.sas_asset_type_cd and car_asset_cd=a.sas_asset_cd) when '03' then (select FORMAT(inv_reg_dt,'dd/MM/yyyy', 'en-us') as reg_dt from ast_inventory where inv_asset_cat_cd=a.sas_asset_cat_cd and inv_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and inv_asset_type_cd=a.sas_asset_type_cd and inv_asset_cd=a.sas_asset_cd) end as a1, case a.sas_asset_cat_cd when '01' then (select DATEDIFF(day,com_reg_dt,GETDATE()) as u_dt from ast_component where com_asset_cat_cd=a.sas_asset_cat_cd and com_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and com_asset_type_cd=a.sas_asset_type_cd and com_asset_cd=a.sas_asset_cd) when '02' then (select DATEDIFF(day,car_reg_dt,GETDATE()) as u_dt from ast_car where car_asset_cat_cd=a.sas_asset_cat_cd and car_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and car_asset_type_cd=a.sas_asset_type_cd and car_asset_cd=a.sas_asset_cd) when '03' then (select  DATEDIFF(day,inv_reg_dt,GETDATE()) as u_dt from ast_inventory where inv_asset_cat_cd=a.sas_asset_cat_cd and inv_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and inv_asset_type_cd=a.sas_asset_type_cd and inv_asset_cd=a.sas_asset_cd) end as a2, case a.sas_asset_cat_cd when '01' then (select com_price_amt from ast_component where com_asset_cat_cd=a.sas_asset_cat_cd and com_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and com_asset_type_cd=a.sas_asset_type_cd and com_asset_cd=a.sas_asset_cd) when '02' then (select car_price_amt from ast_car where car_asset_cat_cd=a.sas_asset_cat_cd and car_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and car_asset_type_cd=a.sas_asset_type_cd and car_asset_cd=a.sas_asset_cd) when '03' then (select inv_price_amt from ast_inventory where inv_asset_cat_cd=a.sas_asset_cat_cd and inv_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and inv_asset_type_cd=a.sas_asset_type_cd and inv_asset_cd=a.sas_asset_cd) end as a3 from (select * from ast_staff_asset  where sas_cond_sts_cd = 'DI' and sas_dispose_cfm_ind !='Y') as a left join Ref_ast_kategori as rk on rk.ast_kategori_code=a.sas_asset_cat_cd left join Ref_ast_jenis_aset as rja on rja.ast_jeniaset_Code=a.sas_asset_type_cd left join ast_cmn_asset as aca on aca.cas_asset_cd=a.sas_asset_cd left join hr_organization as ho on ho.org_gen_id=a.sas_org_id");
            dt = DBCon.Ora_Execute_table("select sas_asset_id,sas_asset_cat_cd,sas_asset_sub_cat_cd,sas_asset_type_cd,sas_asset_cd,sas_location_cd,s1.ast_jeniaset_desc from ast_staff_asset left join Ref_ast_jenis_aset as s1 on s1.ast_jeniaset_Code=sas_asset_type_cd and s1.ast_sub_cat_Code=sas_asset_sub_cat_cd where sas_asset_id='" + lblid1 + "' order by sas_crt_dt desc");
            RptviwerStudent.Reset();
            ds.Tables.Add(dt);

            List<DataRow> listResult = dt.AsEnumerable().ToList();
            listResult.Count();
            int countRow = 0;
            countRow = listResult.Count();

            RptviwerStudent.LocalReport.DataSources.Clear();
            if (countRow != 0)
            {
                RptviwerStudent.LocalReport.ReportPath = "Aset/Ast_Barcode.rdlc";
                ReportDataSource rds = new ReportDataSource("Bcode", dt);

                string bar_code = dt.Rows[0]["sas_asset_id"].ToString();
                string ast_type = dt.Rows[0]["ast_jeniaset_desc"].ToString();
                string ast_cd = dt.Rows[0]["sas_asset_cd"].ToString();

                ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("bcode", bar_code),
                     new ReportParameter("asttype", ast_type),
                     new ReportParameter("astcd", ast_cd)
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

                filename = string.Format("{0}.{1}", "Barcode_" + bar_code.ToUpper() + "_" + DateTime.Now.ToString("ddMMyyyy") + "", "pdf");
                byte[] bytes = RptviwerStudent.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=" + filename);
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
                //string script = " $(function () {$(" + GridView1.ClientID + ").prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);

            }
            else if (countRow == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod tidak dijumpai. Sila Pastikan Semua Maklumat Dimasukkan Dengan Betul.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Issue.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }


    protected void Add_profile(object sender, EventArgs e)
    {
        Response.Redirect("../Aset/Ast_penembetan.aspx");
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        System.Web.UI.WebControls.Label sts = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label2_sts");
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (sts.Text != "")
            {
                if (sts.Text == "Pending")
                {
                    sts.ForeColor = Color.FromName("Red");
                }
                else if (sts.Text == "Delivered")
                {
                    sts.ForeColor = Color.FromName("Green");
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
    //        System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("gd_4");
    //        string lblid1 = lblTitle.Text;
    //        DataTable ddokdicno = new DataTable();
    //        ddokdicno = DBCon.Ora_Execute_table("select * from ast_rental where Id='" + lblTitle.Text + "' ");
    //        if (ddokdicno.Rows.Count != 0)
    //        {
    //            string name = HttpUtility.UrlEncode(service.Encrypt(lblTitle.Text));
    //            //Response.Redirect("../kewengan/kw_profil_syarikat.aspx?edit={0}"+ og_genid.Text + "");
    //            Response.Redirect(string.Format("../Aset/Ast_sewaan.aspx?edit={0}", name));
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