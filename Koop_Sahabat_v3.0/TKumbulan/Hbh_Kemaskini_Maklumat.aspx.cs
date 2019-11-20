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

public partial class Hbh_Kemaskini_Maklumat : System.Web.UI.Page
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
        string script1 = "$(function () {$('.select2').select2()  });";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
        if (!this.IsPostBack)
        {
            if (Session["New"] != null)
            {
                Batch();
                Cawangan();
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

    void Cawangan()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(cs);
            string com = "select  branch_desc from ref_branch order by branch_desc";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddcaw.DataSource = dt;
            ddcaw.DataBind();
            ddcaw.DataTextField = "branch_desc";

            ddcaw.DataBind();
            ddcaw.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (ddbatch.SelectedValue == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Nama Batch.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
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
        if (ddbatch.SelectedValue != "" && ddcaw.SelectedValue == "" && TextBox1.Text == "")
        {
            dtgrid = Dblog.Ora_Execute_table("select mem_hbh_new_IC,mem_hbh_SAHABAT_name,mem_hbh_SAHABAT_no,mem_hbh_CAW_name,mem_hbh_PUSAT_name,mem_hbh_BAKI_ST,[mem_hbh_BANK PENERIMA_no] bank_no,[mem_hbh_BANK PENERIMA] bank_name,case(mem_hbh_SAHABAT_st) when 'Selesai' Then 'SELESAI'  when 'Tidak Selesai' Then 'TIDAK SELESAI' else 'BARU' End AS Status from mem_hibah_st_item where mem_hbh_batch_id='" + ddbatch.SelectedItem.Text + "' ");
            GridView1.DataSource = dtgrid;
            GridView1.DataBind();
        }
        else if (ddbatch.SelectedValue != "" && ddcaw.SelectedValue != "" && TextBox1.Text == "")
        {
            dtgrid = Dblog.Ora_Execute_table("select mem_hbh_new_IC,mem_hbh_SAHABAT_name,mem_hbh_SAHABAT_no,mem_hbh_CAW_name,mem_hbh_PUSAT_name,mem_hbh_BAKI_ST,[mem_hbh_BANK PENERIMA_no] bank_no,[mem_hbh_BANK PENERIMA] bank_name,case(mem_hbh_SAHABAT_st) when 'Selesai' Then 'SELESAI'  when 'Selesai' Then 'SELESAI' when 'Tidak Selesai' Then 'TIDAK SELESAI' else 'BARU' End AS Status from mem_hibah_st_item where mem_hbh_batch_id='" + ddbatch.SelectedItem.Text + "' and mem_hbh_CAW_name='" + ddcaw.SelectedItem.Text + "'  ");
            GridView1.DataSource = dtgrid;
            GridView1.DataBind();
        }
        else if (ddbatch.SelectedValue != "" && ddcaw.SelectedValue != "" && TextBox1.Text != "")
        {
            dtgrid = Dblog.Ora_Execute_table("select mem_hbh_new_IC,mem_hbh_SAHABAT_name,mem_hbh_SAHABAT_no,mem_hbh_CAW_name,mem_hbh_PUSAT_name,mem_hbh_BAKI_ST,[mem_hbh_BANK PENERIMA_no] bank_no,[mem_hbh_BANK PENERIMA] bank_name,case(mem_hbh_SAHABAT_st) when 'Selesai' Then 'SELESAI'  when 'Selesai' Then 'SELESAI' when 'Tidak Selesai' Then 'TIDAK SELESAI' else 'BARU' End AS Status from mem_hibah_st_item where mem_hbh_batch_id='" + ddbatch.SelectedItem.Text + "' and mem_hbh_CAW_name='" + ddcaw.SelectedItem.Text + "'  and mem_hbh_new_IC='" + TextBox1.Text + "'  ");

        }
        else if (ddbatch.SelectedItem.Text != "--- PILIH ---" && ddcaw.SelectedItem.Text != "--- PILIH ---" && TextBox1.Text != "")
        {
            dtgrid = Dblog.Ora_Execute_table("select mem_hbh_new_IC,mem_hbh_SAHABAT_name,mem_hbh_SAHABAT_no,mem_hbh_CAW_name,mem_hbh_PUSAT_name,mem_hbh_BAKI_ST,[mem_hbh_BANK PENERIMA_no] bank_no,[mem_hbh_BANK PENERIMA] bank_name,case(mem_hbh_SAHABAT_st) when 'Selesai' Then 'SELESAI'  when 'Selesai' Then 'SELESAI' when 'Tidak Selesai' Then 'TIDAK SELESAI' else 'BARU' End AS Status from mem_hibah_st_item where mem_hbh_batch_id='" + ddbatch.SelectedItem.Text + "' and mem_hbh_CAW_name='" + ddcaw.SelectedItem.Text + "'  and mem_hbh_new_IC='" + TextBox1.Text + "'  ");

        }
        else
        {
            dtgrid = Dblog.Ora_Execute_table("select mem_hbh_new_IC,mem_hbh_SAHABAT_name,mem_hbh_SAHABAT_no,mem_hbh_CAW_name,mem_hbh_PUSAT_name,mem_hbh_BAKI_ST,[mem_hbh_BANK PENERIMA_no] bank_no,[mem_hbh_BANK PENERIMA] bank_name,case(mem_hbh_SAHABAT_st) when 'Selesai' Then 'SELESAI'  when 'Selesai' Then 'SELESAI' when 'Tidak Selesai' Then 'TIDAK SELESAI' else 'BARU' End AS Status from mem_hibah_st_item where mem_hbh_batch_id='' ");
        }


        if (dtgrid.Rows.Count == 0)
        {

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
            GridView1.DataSource = dtgrid;
            GridView1.DataBind();

        }

    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        grid();
    }
}