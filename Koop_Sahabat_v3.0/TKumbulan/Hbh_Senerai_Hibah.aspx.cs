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

public partial class Hbh_Senerai_Hibah : System.Web.UI.Page
{

    DBConnection Dblog = new DBConnection();
    string fileName;
    string Status = string.Empty;
    int i = 0;
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    StudentWebService service = new StudentWebService();
    string role_1 = string.Empty, role_view = string.Empty, role_add = string.Empty, role_edit = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string userid;
        string script = "  $().ready(function () {  $('.select2').select2();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {

                Batch();
                wilayah();
                grid();
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }


    void Batch()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(cs);
            string com = "select mem_hbh_batch_id from mem_hibah_st";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddbatch.DataSource = dt;
            ddbatch.DataBind();
            ddbatch.DataTextField = "mem_hbh_batch_id";

            ddbatch.DataBind();
            ddbatch.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }


    void wilayah()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(cs);
            string com = "select wilayah_code,wilayah_name from Ref_Cawangan group by wilayah_name,wilayah_code order by wilayah_code";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddwil.DataSource = dt;
            ddwil.DataBind();
            ddwil.DataTextField = "wilayah_name";

            ddwil.DataBind();
            ddwil.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void DD_wilayah_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select cawangan_code,cawangan_name From Ref_Cawangan where wilayah_name='" + ddwil.SelectedItem.Text + "' order by cawangan_name asc";
            SqlDataAdapter adpt = new SqlDataAdapter(com, cs);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_cawangan.DataSource = dt;
            DD_cawangan.DataTextField = "cawangan_name";
            DD_cawangan.DataValueField = "cawangan_code";
            DD_cawangan.DataBind();
            DD_cawangan.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
        grid();
    }
    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        grid();
        GridView1.DataBind();
        //BindGridview();
    }

    public void grid()
    {
        DataTable dtgrid = new DataTable();
        if (ddbatch.SelectedItem.Text != "" && ddwil.SelectedItem.Text == "--- PILIH ---")
        {
            dtgrid = Dblog.Ora_Execute_table("select mem_hbh_new_IC,mem_hbh_SAHABAT_name,mem_hbh_SAHABAT_no,mem_hbh_CAW_name,case when mem_hbh_PUSAT_name='null' then '' else mem_hbh_PUSAT_name end as mem_hbh_PUSAT_name,mem_hbh_BAKI_ST,[mem_hbh_BANK PENERIMA_no] bank_no,[mem_hbh_BANK PENERIMA] bank_name,case(mem_hbh_SAHABAT_st) when 'Selesai' Then 'SELESAI'  when 'Tidak Selesai' Then 'TIDAK SELESAI' else 'BARU' End AS Status from mem_hibah_st_item where mem_hbh_batch_id='" + ddbatch.SelectedItem.Text + "' ");
        }
        else if (ddbatch.SelectedItem.Text != "" && ddwil.SelectedItem.Text != "--- PILIH ---" && DD_cawangan.SelectedItem.Text == "--- PILIH ---")
        {
            dtgrid = Dblog.Ora_Execute_table("select mem_hbh_new_IC,mem_hbh_SAHABAT_name,mem_hbh_SAHABAT_no,mem_hbh_CAW_name,case when mem_hbh_PUSAT_name='null' then '' else mem_hbh_PUSAT_name end as mem_hbh_PUSAT_name,mem_hbh_BAKI_ST,[mem_hbh_BANK PENERIMA_no] bank_no,[mem_hbh_BANK PENERIMA] bank_name,case(mem_hbh_SAHABAT_st) when 'Selesai' Then 'SELESAI'  when 'Tidak Selesai' Then 'TIDAK SELESAI' else 'BARU' End AS Status from mem_hibah_st_item where mem_hbh_batch_id='" + ddbatch.SelectedItem.Text + "' and mem_hbh_WIL_name='" + ddwil.SelectedItem.Text + "'");
        }
        else if (ddbatch.SelectedItem.Text != "" && ddwil.SelectedItem.Text != "--- PILIH ---" && DD_cawangan.SelectedItem.Text != "--- PILIH ---")
        {
            dtgrid = Dblog.Ora_Execute_table("select mem_hbh_new_IC,mem_hbh_SAHABAT_name,mem_hbh_SAHABAT_no,mem_hbh_CAW_name,case when mem_hbh_PUSAT_name='null' then '' else mem_hbh_PUSAT_name end as mem_hbh_PUSAT_name,mem_hbh_BAKI_ST,[mem_hbh_BANK PENERIMA_no] bank_no,[mem_hbh_BANK PENERIMA] bank_name,case(mem_hbh_SAHABAT_st) when 'Selesai' Then 'SELESAI'  when 'Tidak Selesai' Then 'TIDAK SELESAI' else 'BARU' End AS Status from mem_hibah_st_item where mem_hbh_batch_id='" + ddbatch.SelectedItem.Text + "' and mem_hbh_WIL_name='" + ddwil.SelectedItem.Text + "' and mem_hbh_CAW_name='" + DD_cawangan.SelectedItem.Text + "'");
        }
        else
        {
            dtgrid = Dblog.Ora_Execute_table("select mem_hbh_new_IC,mem_hbh_SAHABAT_name,mem_hbh_SAHABAT_no,mem_hbh_CAW_name,mem_hbh_PUSAT_name,mem_hbh_BAKI_ST,[mem_hbh_BANK PENERIMA_no] bank_no,[mem_hbh_BANK PENERIMA] bank_name,case(mem_hbh_SAHABAT_st) when 'Selesai' Then 'SELESAI'  when 'Tidak Selesai' Then 'TIDAK SELESAI' else 'BARU' End AS Status from mem_hibah_st_item where mem_hbh_batch_id='' ");
        }
        GridView1.DataSource = dtgrid;


        if (dtgrid.Rows.Count == 0)
        {
            //Button7.Visible = false;
            dtgrid.Rows.Add(dtgrid.NewRow());
            GridView1.DataSource = dtgrid;
            GridView1.DataBind();
            int columncount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView1.Rows[0].Cells[0].Text = "<center>Rekod Tidak Dijumpai. Sila Lakukan Semula Carian</center>";

        }
        else
        {
            //Button7.Visible = true;
            GridView1.DataSource = dtgrid;
            GridView1.DataBind();
            //Button3.Visible = true;
            //Button4.Visible = true;

        }

    }

    //protected void Button3_Click(object sender, EventArgs e)
    //{
    //    foreach (GridViewRow gv in GridView1.Rows)
    //    {
    //        CheckBox deleteChkBxItem = (CheckBox)gv.FindControl("deleteRec");
    //        Label lbl = (Label)gv.FindControl("Label3");
    //        if (deleteChkBxItem.Checked == true)
    //        {


    //            DataTable dtupd = Dblog.Ora_Execute_table("update mem_hibah_st_item set mem_hbh_SAHABAT_st='" + ddsts.SelectedItem.Text + "' where mem_hbh_batch_id='" + ddbatch.SelectedItem.Text + "' and mem_hbh_new_IC='" + lbl.Text + "'");
    //            Page.ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('Rekod Berjaya Dimuatnaik.'); window.location ='Hbh_Kemaskini_Maklumat .aspx';</script>");
    //        }
    //    }
    //}
    protected void Button1_Click(object sender, EventArgs e)
    {

        grid();

    }
}