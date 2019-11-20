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


public partial class Ast_hadiah : System.Web.UI.Page
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
                DD_staff();
                BindGrid1();
                TextBox3.Attributes.Add("Readonly", "Readonly");
                useid = Session["New"].ToString();             
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet Ds = new DataSet();
        try
        {
            DataTable dd_staff = new DataTable();
            dd_staff = dbcon.Ora_Execute_table("select b.hr_jaw_desc from hr_staff_profile left join Ref_hr_Jawatan as b on b.hr_jaw_Code=stf_curr_post_cd where stf_staff_no='" + DD_nama.SelectedValue + "'");
            TextBox3.Text = dd_staff.Rows[0]["hr_jaw_desc"].ToString();
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
            string com = "select KK_userid,UPPER(sp.stf_name) as sname from KK_User_Login left join hr_staff_profile as sp on sp.stf_staff_no=KK_userid where KK_userid IN (select stf_staff_no from hr_staff_profile)";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_nama.DataSource = dt;
            DD_nama.DataTextField = "sname";
            DD_nama.DataValueField = "KK_userid";
            DD_nama.DataBind();
            DD_nama.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void BindGrid1()
    {
        
        SqlCommand cmd2 = new SqlCommand("select dis_asset_id,ho.org_name as org,UPPER(cs.cas_asset_desc) as naset,b.cnt as qty,dis_curr_amt as nss from (select * from ast_dispose where dis_dispose_reg_dt>=DATEADD(day, DATEDIFF(day, 0, '" + dt1 + "'), 0) and dis_dispose_reg_dt<=DATEADD(day, DATEDIFF(day, 0, '" + dt2 + "'), 0)) as a left join ast_cmn_asset as cs on cs.cas_asset_cd=a.dis_asset_cd and cs.cas_asset_cat_cd=a.dis_asset_cat_cd and cs.cas_asset_sub_cat_cd=a.dis_asset_sub_cat_cd and cs.cas_asset_type_cd=a.dis_asset_type_cd left join Ref_ast_kaedah_palupusan as kk on kk.kaedah_id=a.dis_dispose_type_cd left join hr_organization as ho on ho.org_id=a.dis_org_id full outer join (select count(dis_asset_type_cd) as cnt,dis_asset_type_cd from ast_dispose where dis_dispose_reg_dt>=DATEADD(day, DATEDIFF(day, 0, '" + dt1 + "'), 0) and dis_dispose_reg_dt<=DATEADD(day, DATEDIFF(day, 0, '" + dt2 + "'), 0) group by dis_asset_type_cd) as b on b.dis_asset_type_cd=a.dis_asset_type_cd", con);
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
            GridView1.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";

        }
        else
        {
            GridView1.DataSource = ds2;
            GridView1.DataBind();
        }


    }

    protected void click_rst(object sender, EventArgs e)
    {
        //txt_dar.Text = "";
        //txt_seh.Text = "";
        //BindGrid2();
        Response.Redirect("AST_Hadiah.aspx");
    }
    protected void clk_srch(object sender, EventArgs e)
    {

        if (txt_dar.Text != "" && txt_seh.Text != "")
        {
            srk();
        }
        else
        {
            srk();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Medan Input Adalah Mandatori.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    void srk()
    {
        if (txt_dar.Text != "" && txt_seh.Text != "")
        {
            DateTime pd = DateTime.ParseExact(txt_dar.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            dt1 = pd.ToString("yyyy-MM-dd");
            DateTime pd1 = DateTime.ParseExact(txt_seh.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            dt2 = pd1.ToString("yyyy-MM-dd");
            
        }
        BindGrid1();
    }

    protected void gv_refdata_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindGrid1();
    }

  

    protected void ctk_values(object sender, EventArgs e)
    {
        if (txt_dar.Text != "" && txt_seh.Text != "")
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            string rcount = string.Empty, rcount1 = string.Empty;
            int count = 0;
            double sum = 0;

            dt.Columns.AddRange(new DataColumn[4] { new DataColumn("nast"), new DataColumn("qnty"), new DataColumn("ast_id"), new DataColumn("ns_rm") });
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[4].FindControl("chkRow") as CheckBox);
                    if (chkRow.Checked)
                    {
                        string naset = (row.Cells[1].FindControl("Label3") as Label).Text;
                        string qty = (row.Cells[2].FindControl("Label2") as Label).Text;
                        string aid = (row.Cells[3].FindControl("Label1") as Label).Text;
                        string ns = (row.Cells[4].FindControl("Label7") as Label).Text;
                        dt.Rows.Add(naset, qty, aid, ns);
                        count++;
                        double amount = Convert.ToDouble((row.Cells[4].FindControl("Label7") as Label).Text);
                        sum += amount;
                    }
                    rcount = count.ToString();
                    rcount1 = sum.ToString("0.00");
                }
            }

            string vv1 = string.Empty;
            if (DD_nama.SelectedValue != "")
            {
                vv1 = DD_nama.SelectedItem.Text;
            }

            //dt = dbcon.Ora_Execute_table("" + mqry + "");
            RptviwerStudent.Reset();
            ds.Tables.Add(dt);

            List<DataRow> listResult = dt.AsEnumerable().ToList();
            listResult.Count();
            int countRow = 0;
            countRow = listResult.Count();

            RptviwerStudent.LocalReport.DataSources.Clear();
            if (rcount != "0")
            {
                RptviwerStudent.LocalReport.ReportPath = "Aset/AST_Hadiah.rdlc";
                ReportDataSource rds = new ReportDataSource("asthadiah", dt);

                ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("tot_amt",rcount1 ),
                     new ReportParameter("h1",txt_dar.Text ),
                     new ReportParameter("h2",txt_seh.Text ),
                     new ReportParameter("h3",TextBox2.Text ),
                     new ReportParameter("h4",TextBox1.Text ),
                     new ReportParameter("h5",vv1 ),
                     new ReportParameter("h6",TextBox3.Text ),
                     new ReportParameter("h7",TextBox4.Text ),
                     //new ReportParameter("h8",TextBox4.Text ),
                     
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
                    filename = string.Format("{0}.{1}", "Pendaftaran_Maklumat_Hadiah_" + DateTime.Now.ToString("ddMMyyyy") + ".", "pdf");
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
                    string strFileName = string.Format("{0}.{1}", "LAPORAN_NILAI_ASET_YANG" + DateTime.Now.ToString("ddMMyyyy") + "", "csv");
                    builder.Append("Nama  Aset,Kuantiti,Nilai Semasa (RM)" + Environment.NewLine);
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkRow = (row.Cells[4].FindControl("chkRow") as CheckBox);
                            if (chkRow.Checked)
                            {
                                string nast = ((Label)row.FindControl("Label3")).Text.ToString();
                                string qty = ((Label)row.FindControl("Label2")).Text.ToString();
                                string kb = ((Label)row.FindControl("Label7")).Text.ToString();
                                builder.Append(nast + "," + qty + "," + kb + Environment.NewLine);
                            }
                        }
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
                    filename = string.Format("{0}.{1}", "LAPORAN_NILAI_ASET_" + DateTime.Now.ToString("ddMMyyyy") + "", "doc");
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
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Maklumat.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string rcount = string.Empty;
        int count1 = 0;
        foreach (GridViewRow row in GridView1.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[4].FindControl("chkRow") as CheckBox);
                if (chkRow.Checked)
                {
                    count1++;
                }

                rcount = count1.ToString();
            }
        }
        if (rcount != "0")
        {
            DateTime fd = DateTime.ParseExact(TextBox4.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            String ap_dt = fd.ToString("yyyy-MM-dd");
            DateTime fd1 = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            String pr_dt = fd1.ToString("yyyy-MM-dd");
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[4].FindControl("chkRow") as CheckBox);
                    if (chkRow.Checked)
                    {
                        string ast_id = ((Label)row.FindControl("Label1")).Text.ToString(); //this store the  value in varName1
                        dbcon.Execute_CommamdText("UPDATE ast_dispose set dis_approve_dt='" + ap_dt + "',dis_approve_id='" + DD_nama.SelectedValue + "',dis_present_name='" + TextBox2.Text.Replace("''", "'") + "',dis_present_dt='" + pr_dt + "',dis_upd_id='" + Session["new"].ToString() + "',dis_upd_dt='" + DateTime.Now + "' where dis_asset_id='" + ast_id + "'");
                    }
                }
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
        }
        else
        {
            srk();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
       
    }



}