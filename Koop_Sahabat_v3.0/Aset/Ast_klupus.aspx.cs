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

public partial class Ast_klupus : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection dbcon = new DBConnection();
    StudentWebService service = new StudentWebService();
    DataTable dt = new DataTable();
    string useid = string.Empty, Status = string.Empty;
    string dt1 = string.Empty, dt2 = string.Empty;
    string sqry = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2()})";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Button3);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;
                DD_Kaedah();
                DD_staff();
                BindGrid();
                TextBox2.Text = DateTime.Now.ToString("dd/MM/yyyy");
                useid = Session["New"].ToString();
               
             
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    void DD_Kaedah()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select * from Ref_ast_kaedah_palupusan where Status ='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList3.DataSource = dt;
            DropDownList3.DataTextField = "kaedah_desc";
            DropDownList3.DataValueField = "kaedah_id";
            DropDownList3.DataBind();
            DropDownList3.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    void DD_staff()
    {
        DataSet Ds = new DataSet();
        try
        {
            //string com = "select userid,UPPER(sp.stf_name) as sname from login left join hr_staff_profile as sp on sp.stf_staff_no=userid where userid IN (select stf_staff_no from hr_staff_profile) and level >='4' ";
            string com = "select KK_userid,UPPER(sp.stf_name) as sname from KK_User_Login left join hr_staff_profile as sp on sp.stf_staff_no=KK_userid where KK_userid IN (select stf_staff_no from hr_staff_profile)";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_pen.DataSource = dt;
            dd_pen.DataTextField = "sname";
            dd_pen.DataValueField = "KK_userid";
            dd_pen.DataBind();
            dd_pen.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            dd_ajk1.DataSource = dt;
            dd_ajk1.DataTextField = "sname";
            dd_ajk1.DataValueField = "KK_userid";
            dd_ajk1.DataBind();
            dd_ajk1.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            dd_ajk2.DataSource = dt;
            dd_ajk2.DataTextField = "sname";
            dd_ajk2.DataValueField = "KK_userid";
            dd_ajk2.DataBind();
            dd_ajk2.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void BindGrid()
    {

        select_qry();
        SqlCommand cmd2 = new SqlCommand("" + sqry + "", con);
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

    void select_qry()
    {
        if (txt_dar.Text != "" && txt_seh.Text != "")
        {
            DateTime pd = DateTime.ParseExact(txt_dar.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            dt1 = pd.ToString("yyyy-MM-dd");
            DateTime pd1 = DateTime.ParseExact(txt_seh.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            dt2 = pd1.ToString("yyyy-MM-dd");
        }

        if (txt_dar.Text != "" && txt_seh.Text != "" && DropDownList3.SelectedValue == "")
        {
            sqry = "select  a.dis_asset_id,a.dis_asset_type_cd,ISNULL(a.dis_reserve_amt,'') as res_amt,ISNULL(ho.org_name,'') as org,UPPER(cs.cas_asset_desc) as naset,b.cnt as qty,dis_asset_age as uaset,dis_purchase_amt as hps,(dis_purchase_amt * b.cnt) as hpj,dis_curr_amt as nss,(dis_curr_amt * b.cnt) as nsj,kk.kaedah_desc as kp from (select * from ast_dispose where dis_dispose_reg_dt>=DATEADD(day, DATEDIFF(day, 0, '" + dt1 + "'), 0) and dis_dispose_reg_dt<=DATEADD(day, DATEDIFF(day, 0, '" + dt2 + "'), 0)) as a left join ast_cmn_asset as cs on cs.cas_asset_cd=a.dis_asset_cd and cs.cas_asset_cat_cd=a.dis_asset_cat_cd and cs.cas_asset_sub_cat_cd=a.dis_asset_sub_cat_cd and cs.cas_asset_type_cd=a.dis_asset_type_cd left join Ref_ast_kaedah_palupusan as kk on kk.kaedah_id=a.dis_dispose_type_cd left join hr_organization as ho on ho.org_gen_id=a.dis_org_id full outer join (select count(dis_asset_type_cd) as cnt,dis_asset_type_cd from ast_dispose where dis_dispose_reg_dt>=DATEADD(day, DATEDIFF(day, 0, '" + dt1 + "'), 0) and dis_dispose_reg_dt<=DATEADD(day, DATEDIFF(day, 0, '" + dt2 + "'), 0) group by dis_asset_type_cd) as b on b.dis_asset_type_cd=a.dis_asset_type_cd";
        }
        else if (txt_dar.Text == "" && txt_seh.Text == "" && DropDownList3.SelectedValue != "")
        {
            sqry = "select  a.dis_asset_id,a.dis_asset_type_cd,ISNULL(a.dis_reserve_amt,'') as res_amt,ISNULL(ho.org_name,'') as org,UPPER(cs.cas_asset_desc) as naset,b.cnt as qty,dis_asset_age as uaset,dis_purchase_amt as hps,(dis_purchase_amt * b.cnt) as hpj,dis_curr_amt as nss,(dis_curr_amt * b.cnt) as nsj,kk.kaedah_desc as kp from (select * from ast_dispose where dis_dispose_type_cd ='" + DropDownList3.SelectedValue + "') as a left join ast_cmn_asset as cs on cs.cas_asset_cd=a.dis_asset_cd and cs.cas_asset_cat_cd=a.dis_asset_cat_cd and cs.cas_asset_sub_cat_cd=a.dis_asset_sub_cat_cd and cs.cas_asset_type_cd=a.dis_asset_type_cd left join Ref_ast_kaedah_palupusan as kk on kk.kaedah_id=a.dis_dispose_type_cd left join hr_organization as ho on ho.org_gen_id=a.dis_org_id full outer join (select count(dis_asset_type_cd) as cnt,dis_asset_type_cd from ast_dispose where dis_dispose_type_cd='" + DropDownList3.SelectedValue + "' group by dis_asset_type_cd) as b on b.dis_asset_type_cd=a.dis_asset_type_cd";
        }
        else if (txt_dar.Text != "" && txt_seh.Text != "" && DropDownList3.SelectedValue != "")
        {
            sqry = "select  a.dis_asset_id,a.dis_asset_type_cd,ISNULL(a.dis_reserve_amt,'') as res_amt,ISNULL(ho.org_name,'') as org,UPPER(cs.cas_asset_desc) as naset,b.cnt as qty,dis_asset_age as uaset,dis_purchase_amt as hps,(dis_purchase_amt * b.cnt) as hpj,dis_curr_amt as nss,(dis_curr_amt * b.cnt) as nsj,kk.kaedah_desc as kp from (select * from ast_dispose where dis_dispose_reg_dt>=DATEADD(day, DATEDIFF(day, 0, '" + dt1 + "'), 0) and dis_dispose_reg_dt<=DATEADD(day, DATEDIFF(day, 0, '" + dt2 + "'), 0) and dis_dispose_type_cd='" + DropDownList3.SelectedValue + "') as a left join ast_cmn_asset as cs on cs.cas_asset_cd=a.dis_asset_cd and cs.cas_asset_cat_cd=a.dis_asset_cat_cd and cs.cas_asset_sub_cat_cd=a.dis_asset_sub_cat_cd and cs.cas_asset_type_cd=a.dis_asset_type_cd left join Ref_ast_kaedah_palupusan as kk on kk.kaedah_id=a.dis_dispose_type_cd left join hr_organization as ho on ho.org_gen_id=a.dis_org_id full outer join (select count(dis_asset_type_cd) as cnt,dis_asset_type_cd from ast_dispose where dis_dispose_reg_dt>=DATEADD(day, DATEDIFF(day, 0, '" + dt1 + "'), 0) and dis_dispose_reg_dt<=DATEADD(day, DATEDIFF(day, 0, '" + dt2 + "'), 0) and dis_dispose_type_cd='" + DropDownList3.SelectedValue + "' group by dis_asset_type_cd) as b on b.dis_asset_type_cd=a.dis_asset_type_cd";
        }
        else
        {
            sqry = "select  a.dis_asset_id,a.dis_asset_type_cd,ISNULL(a.dis_reserve_amt,'') as res_amt,ISNULL(ho.org_name,'') as org,UPPER(cs.cas_asset_desc) as naset,b.cnt as qty,dis_asset_age as uaset,dis_purchase_amt as hps,(dis_purchase_amt * b.cnt) as hpj,dis_curr_amt as nss,(dis_curr_amt * b.cnt) as nsj,kk.kaedah_desc as kp from (select * from ast_dispose where dis_dispose_reg_dt>=DATEADD(day, DATEDIFF(day, 0, ''), 0) and dis_dispose_reg_dt<=DATEADD(day, DATEDIFF(day, 0, ''), +1)) as a left join ast_cmn_asset as cs on cs.cas_asset_cd=a.dis_asset_cd and cs.cas_asset_cat_cd=a.dis_asset_cat_cd and cs.cas_asset_sub_cat_cd=a.dis_asset_sub_cat_cd and cs.cas_asset_type_cd=a.dis_asset_type_cd left join Ref_ast_kaedah_palupusan as kk on kk.kaedah_id=a.dis_dispose_type_cd left join hr_organization as ho on ho.org_gen_id=a.dis_org_id full outer join (select count(dis_asset_type_cd) as cnt,dis_asset_type_cd from ast_dispose where dis_dispose_reg_dt>=DATEADD(day, DATEDIFF(day, 0, ''), 0) and dis_dispose_reg_dt<=DATEADD(day, DATEDIFF(day, 0, ''), +1) group by dis_asset_type_cd) as b on b.dis_asset_type_cd=a.dis_asset_type_cd";
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
            HeaderCell.Text = "NILAI REZAB (RM)";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            //Adding the Row at the 0th position (first row) in the Grid
            ProductGrid.Controls[0].Controls.AddAt(0, HeaderRow);
        }
    }

    protected void click_rst(object sender, EventArgs e)
    {
        txt_dar.Text = "";
        txt_seh.Text = "";
        DropDownList3.SelectedValue = "";
        dd_pen.SelectedValue = "";
        dd_ajk1.SelectedValue = "";
        dd_ajk2.SelectedValue = "";
        BindGrid();
    }

    protected void clk_srch(object sender, EventArgs e)
    {

        if (txt_dar.Text != "" && txt_seh.Text != "" || DropDownList3.SelectedValue != "")
        {

            BindGrid();
        }
        else
        {
            BindGrid();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Medan Input Adalah Mandatori.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    protected void gv_refdata_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSelected.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    protected void ctk_values(object sender, EventArgs e)
    {
        if (txt_dar.Text != "" && txt_seh.Text != "" || DropDownList3.SelectedValue != "")
        {
            select_qry();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt = dbcon.Ora_Execute_table("" + sqry + "");
            RptviwerStudent.Reset();
            ds.Tables.Add(dt);

            List<DataRow> listResult = dt.AsEnumerable().ToList();
            listResult.Count();
            int countRow = 0;
            countRow = listResult.Count();
            string ss1 = string.Empty, ss2 = string.Empty, ss3 = string.Empty, ss4 = string.Empty, ss5 = string.Empty;
            if (DropDownList3.SelectedValue != "")
            {
                ss1 = DropDownList3.SelectedItem.Text;
            }

            if (dd_pen.SelectedValue != "")
            {
                ss2 = dd_pen.SelectedItem.Text;
            }

            if (dd_ajk1.SelectedValue != "")
            {
                ss3 = dd_ajk1.SelectedItem.Text;
            }

            if (dd_ajk2.SelectedValue != "")
            {
                ss4 = dd_ajk2.SelectedItem.Text;
            }

            RptviwerStudent.LocalReport.DataSources.Clear();
            if (countRow != 0)
            {
                RptviwerStudent.LocalReport.ReportPath = "Aset/ast_klupus.rdlc";
                ReportDataSource rds = new ReportDataSource("astskupus", dt);

                ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("h1",txt_dar.Text ),
                     new ReportParameter("h2",txt_seh.Text ),
                     new ReportParameter("h3",ss1 ),
                     new ReportParameter("h4",TextBox2.Text ),
                     new ReportParameter("h5",ss2 ),
                     new ReportParameter("h6",ss3 ),
                     new ReportParameter("h7",ss4 ),

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

                if (sel_frmt.SelectedValue == "01")
                {
                    filename = string.Format("{0}.{1}", "Kelulusan_Rekod_Pelupusan_Aset_" + DateTime.Now.ToString("ddMMyyyy") + ".", "pdf");
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
                    builder.Append("Organisation ,Nama Aset,Kuantiti, Usia Aset, Seunit (RM), Jumlah (RM), Seunit (RM), Jumlah (RM), Kaedah Pelupusan, NILAI REZAB (RM)" + Environment.NewLine);
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
                        string res_amt = ((Label)row.FindControl("lbl_ramt")).Text.ToString();
                        string kp = ((DropDownList)row.FindControl("lbl_kp")).Text.ToString();
                        builder.Append(oname + "," + naset + "," + qty + "," + uaset + "," + hss + "," + hsj + "," + nss + "," + nsj + "," + res_amt + "," + kp + Environment.NewLine);
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


    protected void insert_values(object sender, EventArgs e)
    {
        string rcount = string.Empty;
        int count1 = 0;
        foreach (GridViewRow gvrow in gvSelected.Rows)
        {
            count1++;
        }
        rcount = count1.ToString();
        if (rcount != "0")
        {
            foreach (GridViewRow row in gvSelected.Rows)
            {
                string ast_cd = ((Label)row.FindControl("lbl_ascd")).Text.ToString(); //this store the  value in varName1
                string res_amt = ((Label)row.FindControl("lbl_ramt")).Text.ToString();
                dbcon.Execute_CommamdText("UPDATE ast_dispose SET dis_dispose_head='" + dd_pen.SelectedValue + "',dis_dispose_ajk1='" + dd_ajk1.SelectedValue + "',dis_dispose_ajk2='" + dd_ajk2.SelectedValue + "',dis_approve_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',dis_approve_id='" + Session["new"].ToString() + "',dis_upd_id='" + Session["new"].ToString() + "',dis_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where dis_asset_id='" + ast_cd + "'");
            }            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
        }
        else
        {
            BindGrid();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }
    //protected void click_rst(object sender, EventArgs e)
    //{
    //    Session["validate_success"] = "";
    //    Session["alrt_msg"] = "";
    //    Response.Redirect("../Aset/Ast_klupus.aspx");
    //}

 
    
}