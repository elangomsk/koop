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

public partial class Ast_slupus : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection dbcon = new DBConnection();
    StudentWebService service = new StudentWebService();
    DataTable dt = new DataTable();

    string dt1 = string.Empty, dt2 = string.Empty;
    string useid = string.Empty;
    string Status = string.Empty;
    string oid = string.Empty;
    string sdd = string.Empty, str_qry = string.Empty;
    string clmfd = string.Empty, clm_name = string.Empty;
    string ss1 = string.Empty;
    string CommandArgument1 = string.Empty, CommandArgument2 = string.Empty, CommandArgument3 = string.Empty, CommandArgument4 = string.Empty, CommandArgument5 = string.Empty, CommandArgument6 = string.Empty, CommandArgument7 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Btn_Cetak);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;
                BindGrid();
                useid = Session["New"].ToString();
               
             
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    protected void gv_refdata_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSelected.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    protected void BindGrid()
    {
        SqlCommand cmd2 = new SqlCommand("select ho.org_name as org,UPPER(cs.cas_asset_desc) as naset,b.cnt as qty,dis_asset_age as uaset,dis_purchase_amt as hps,(dis_purchase_amt * b.cnt) as hpj,dis_curr_amt as nss,(dis_curr_amt * b.cnt) as nsj,kk.kaedah_desc as kp ,UPPER(ast_justifikasi_desc) as ast_justifikasi_desc from (select * from ast_dispose where dis_dispose_reg_dt>=DATEADD(day, DATEDIFF(day, 0, '" + dt1 + "'), 0) and dis_dispose_reg_dt<=DATEADD(day, DATEDIFF(day, 0, '" + dt2 + "'), +0)) as a left join ast_cmn_asset as cs on cs.cas_asset_cd=a.dis_asset_cd and cs.cas_asset_cat_cd=a.dis_asset_cat_cd and cs.cas_asset_sub_cat_cd=a.dis_asset_sub_cat_cd and cs.cas_asset_type_cd=a.dis_asset_type_cd left join Ref_ast_kaedah_palupusan as kk on kk.kaedah_id=a.dis_dispose_type_cd left join hr_organization as ho on ho.org_gen_id=a.dis_org_id left join ast_staff_asset as st on st.sas_asset_id=a.dis_asset_id and ISNULL(st.sas_justify_cd,'') != '' left join Ref_ast_justifikasi as J on J.ast_justifikasi_code=st.sas_justify_cd full outer join (select count(dis_asset_type_cd) as cnt,dis_asset_type_cd from ast_dispose where dis_dispose_reg_dt>=DATEADD(day, DATEDIFF(day, 0, '" + dt1 + "'), 0) and dis_dispose_reg_dt<=DATEADD(day, DATEDIFF(day, 0, '" + dt2 + "'), +0) group by dis_asset_type_cd) as b on b.dis_asset_type_cd=a.dis_asset_type_cd", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);

        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            gvSelected.DataSource = ds2;
            gvSelected.DataBind();
            int columncount = gvSelected.Rows[0].Cells.Count;
            gvSelected.Rows[0].Cells.Clear();
            gvSelected.Rows[0].Cells.Add(new TableCell());
            gvSelected.Rows[0].Cells[0].ColumnSpan = columncount;
            gvSelected.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
            if (txt_dar.Text != "" && txt_seh.Text != "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        else
        {
            gvSelected.DataSource = ds2;
            gvSelected.DataBind();
        }


    }

    protected void grdViewProducts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // Invisibling the first three columns of second row header (normally created on binding)
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false; // Invisibiling Year Header Cell
            e.Row.Cells[1].Visible = false; // Invisibiling Period Header Cell
            e.Row.Cells[2].Visible = false; // Invisibiling Audited By Header Cell
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
        }

        // This is for calculation of last column (Total = Direct + Referral)

    }

    protected void grdViewProducts_RowCreated(object sender, GridViewRowEventArgs e)
    {
        // Adding a column manually once the header created
        if (e.Row.RowType == DataControlRowType.Header) // If header created
        {
            GridView ProductGrid = (GridView)sender;

            // Creating a Row
            GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            //Adding Year Column
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "BIL";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2; // For merging first, second row cells to one
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            //Adding Period Column
            HeaderCell = new TableCell();
            HeaderCell.Text = "ORGANISASI";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            //Adding Audited By Column
            HeaderCell = new TableCell();
            HeaderCell.Text = "KETERANGAN ASET";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "KUANTITI";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "USIA ASET";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            //Adding Revenue Column
            HeaderCell = new TableCell();
            HeaderCell.Text = "HARGA PEROLEHAN";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.ColumnSpan = 2; // For merging three columns (Direct, Referral, Total)
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            //Adding Revenue Column
            HeaderCell = new TableCell();
            HeaderCell.Text = "NILAI SEMASA";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.ColumnSpan = 2; // For merging three columns (Direct, Referral, Total)
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "KAEDAH PELUPUSAN";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "JUSTIFIKASI PELUPUSAN";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            //Adding the Row at the 0th position (first row) in the Grid
            ProductGrid.Controls[0].Controls.AddAt(0, HeaderRow);
        }
    }

    //protected void click_rst(object sender, EventArgs e)
    //{
    //    txt_dar.Text = "";
    //    txt_seh.Text = "";
    //    BindGrid();
    //}
    protected void clk_srch(object sender, EventArgs e)
    {

        if (txt_dar.Text != "" && txt_seh.Text != "")
        {
            DateTime pd = DateTime.ParseExact(txt_dar.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            dt1 = pd.ToString("yyyy-MM-dd");
            DateTime pd1 = DateTime.ParseExact(txt_seh.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            dt2 = pd1.ToString("yyyy-MM-dd");
            BindGrid();
        }
        else
        {
            BindGrid();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Medan Input Adalah Mandatori.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    protected void ctk_values(object sender, EventArgs e)
    {
        if (txt_dar.Text != "" && txt_seh.Text != "")
        {
            DateTime pd = DateTime.ParseExact(txt_dar.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            dt1 = pd.ToString("yyyy-MM-dd");
            DateTime pd1 = DateTime.ParseExact(txt_seh.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            dt2 = pd1.ToString("yyyy-MM-dd");

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt = dbcon.Ora_Execute_table("select ho.org_name as org,UPPER(cs.cas_asset_desc) as naset,b.cnt as qty,dis_asset_age as uaset,dis_purchase_amt as hps,(dis_purchase_amt * b.cnt) as hpj,dis_curr_amt as nss,(dis_curr_amt * b.cnt) as nsj,kk.kaedah_desc as kp ,UPPER(ast_justifikasi_desc) as ast_justifikasi_desc from (select * from ast_dispose where dis_dispose_reg_dt>=DATEADD(day, DATEDIFF(day, 0, '" + dt1 + "'), 0) and dis_dispose_reg_dt<=DATEADD(day, DATEDIFF(day, 0, '" + dt2 + "'), +0)) as a left join ast_cmn_asset as cs on cs.cas_asset_cd=a.dis_asset_cd and cs.cas_asset_cat_cd=a.dis_asset_cat_cd and cs.cas_asset_sub_cat_cd=a.dis_asset_sub_cat_cd and cs.cas_asset_type_cd=a.dis_asset_type_cd left join Ref_ast_kaedah_palupusan as kk on kk.kaedah_id=a.dis_dispose_type_cd left join hr_organization as ho on ho.org_gen_id=a.dis_org_id left join ast_staff_asset as st on st.sas_asset_id=a.dis_asset_id and ISNULL(st.sas_justify_cd,'') != '' left join Ref_ast_justifikasi as J on J.ast_justifikasi_code=st.sas_justify_cd full outer join (select count(dis_asset_type_cd) as cnt,dis_asset_type_cd from ast_dispose where dis_dispose_reg_dt>=DATEADD(day, DATEDIFF(day, 0, '" + dt1 + "'), 0) and dis_dispose_reg_dt<=DATEADD(day, DATEDIFF(day, 0, '" + dt2 + "'), +0) group by dis_asset_type_cd) as b on b.dis_asset_type_cd=a.dis_asset_type_cd");
            RptviwerStudent.Reset();
            ds.Tables.Add(dt);

            List<DataRow> listResult = dt.AsEnumerable().ToList();
            listResult.Count();
            int countRow = 0;
            countRow = listResult.Count();

            RptviwerStudent.LocalReport.DataSources.Clear();
            if (countRow != 0)
            {
                RptviwerStudent.LocalReport.ReportPath = "Aset/ast_slupus.rdlc";
                ReportDataSource rds = new ReportDataSource("astslupus", dt);
                RptviwerStudent.LocalReport.DataSources.Add(rds);
                RptviwerStudent.LocalReport.Refresh();

                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;
                string filename;

                if (sel_frmt.SelectedValue == "01")
                {
                    filename = string.Format("{0}.{1}", "SEMAKAN_REKOD_PELUPUSAN_ASET_" + DateTime.Now.ToString("ddMMyyyy") + "", "pdf");
                    byte[] bytes = RptviwerStudent.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                    Response.Buffer = true;
                    Response.Clear();
                    Response.ContentType = mimeType;
                    Response.AddHeader("content-disposition", "attachment; filename=" + filename);
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();
                }
                else if (sel_frmt.SelectedValue == "02")
                {
                    StringBuilder builder = new StringBuilder();
                    string strFileName = string.Format("{0}.{1}", "SEMAKAN_REKOD_PELUPUSAN_ASET_" + DateTime.Now.ToString("ddMMyyyy") + "", "csv");
                    builder.Append("Organisation ,Nama Aset,Kuantiti, Usia Aset, Seunit (RM), Jumlah (RM), Seunit (RM), Jumlah (RM), Kaedah Pelupusan, Justifikasi Pelupusan" + Environment.NewLine);
                    foreach (GridViewRow row in gvSelected.Rows)
                    {
                        string oname = ((Label)row.FindControl("Label3")).Text.ToString();
                        string naset = ((Label)row.FindControl("Label2")).Text.ToString();
                        string qty = ((Label)row.FindControl("Label6")).Text.ToString();
                        string uaset = ((Label)row.FindControl("Label7")).Text.ToString();
                        string hss = ((Label)row.FindControl("Label8")).Text.ToString();
                        string hsj = ((Label)row.FindControl("Label9")).Text.ToString();
                        string nss = ((Label)row.FindControl("Label10")).Text.ToString();
                        string nsj = ((Label)row.FindControl("Label11")).Text.ToString();
                        string jp = ((Label)row.FindControl("Label12")).Text.ToString();
                        string kp = ((Label)row.FindControl("lbl_kp")).Text.ToString();
                        builder.Append(oname + "," + naset + "," + qty + "," + uaset + "," + hss + "," + hsj + "," + nss.Replace(",","") + "," + nsj.Replace(",", "") + "," + kp + ","+ jp + Environment.NewLine);
                    }
                    Response.Clear();
                    Response.ContentType = "text/csv";
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + strFileName);
                    Response.Write(builder.ToString());
                    Response.End();
                }
                else if (sel_frmt.SelectedValue == "03")
                {
                    byte[] bytes = RptviwerStudent.LocalReport.Render("Word", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                    filename = string.Format("{0}.{1}", "SEMAKAN_REKOD_PELUPUSAN_ASET_" + DateTime.Now.ToString("ddMMyyyy") + "", "doc");
                    Response.Buffer = true;
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
                    Response.ContentType = mimeType;
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod tidak dijumpai. Sila Pastikan Semua Maklumat Dimasukkan Dengan Betul.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        else
        {
            BindGrid();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Medan Input Adalah Mandatori.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }
    protected void click_rst(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_slupus.aspx");
    }

 
    
}