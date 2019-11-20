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

public partial class Hbh_kiraan_hibah : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                batchbind();
                bind_year();
                f_date.Text = DateTime.Now.ToString("dd/MM/yyyy");                
                TextBox1.Text = DateTime.Now.ToString("dd/MM/yyyy");
                TextBox1.Attributes.Add("disabled", "disabled");                
                grid();
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    void bind_year()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(cs);
            string com = "select year(ast_end_date) ast_year from aim_st group by year(ast_end_date)";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList2.DataSource = dt;
            DropDownList2.DataBind();
            DropDownList2.DataTextField = "ast_year";
            DropDownList2.DataValueField = "ast_year";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    void batchbind()
    {
        DataTable dt1 = Dblog.Ora_Execute_table("select isnull(max(right(mem_hbp_batch_proses_id,1)),0) +1 mem_hbp_batch_proses_id from mem_hibah_proses");
        if (dt1.Rows[0][0].ToString() == "0")
        {
            txtnobat.Text = "KK-HBH-" + DateTime.Now.Year + "-" + "1";
        }
        else
        {
            txtnobat.Text = "KK-HBH-" + DateTime.Now.Year + "-" + dt1.Rows[0][0].ToString();
        }

        txtnobat.Attributes.Add("disabled", "disabled");
        TextBox1.Text = DateTime.Now.ToString("dd/MM/yyyy");
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

        dtgrid = Dblog.Ora_Execute_table("select I.mem_hbh_new_IC,I.mem_hbh_SAHABAT_name,I.mem_hbh_SAHABAT_no,I.mem_hbh_CAW_name,case when I.mem_hbh_PUSAT_name='null' then '' else I.mem_hbh_PUSAT_name end as  mem_hbh_PUSAT_name,P.mem_hbp_batch_proses_amt,p.mem_hbp_batch_proses_ST_amt,I.mem_hbh_batch_id,P.mem_hbh_crt_dt from mem_hibah_proses P inner join mem_hibah_st_item I on p.mem_hbp_kadar = I.mem_hbh_batch_id and p.mem_hbp_sahabat_no = I.mem_hbh_SAHABAT_no");
        GridView1.DataSource = dtgrid;


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
  
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (DropDownList2.SelectedValue != "" && txtph.Text != "")
        {
            DataTable dt1 = Dblog.Ora_Execute_table("select isnull(max(right(mem_hbp_batch_proses_id,1)),0) +1 mem_hbp_batch_proses_id from mem_hibah_proses");
            if (dt1.Rows[0][0].ToString() == "0")
            {
                txtnobat.Text = "KK-HBH-" + DateTime.Now.Year + "-" + "1";
            }
            else
            {
                txtnobat.Text = "KK-HBH-" + DateTime.Now.Year + "-" + dt1.Rows[0][0].ToString();
            }

            txtnobat.Attributes.Add("disabled", "disabled");




            TextBox1.Text = DateTime.Now.ToString("dd/MM/yyyy");
            TextBox1.Attributes.Add("disabled", "disabled");
            DateTime txt = DateTime.ParseExact(f_date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string test = txt.ToString("MMMM");

            DataTable dt = new DataTable();
            dt = Dblog.Ora_Execute_table("select top 1 mem_hbh_batch_bil,mem_hbh_batch_id  from mem_hibah_st where  mem_hbh_batch_yr_id='" + DropDownList2.SelectedValue + "'");
            if (dt.Rows.Count > 0)
            {
                DataTable dtcal = Dblog.Ora_Execute_table("select mem_hbh_new_IC,mem_hbh_SAHABAT_no,mem_hbh_BAKI_ST,mem_hbh_batch_id from mem_hibah_st_item where mem_hbh_batch_id='" + dt.Rows[0][1].ToString() + "'");
                if (dtcal.Rows.Count > 0)
                {
                    for (int i = 0; i < dtcal.Rows.Count; i++)
                    {
                        string bst = dtcal.Rows[i][2].ToString();
                        decimal amt = Convert.ToDecimal(bst.ToString());
                        decimal pamt = Convert.ToDecimal(txtph.Text);
                        decimal famt = amt * pamt / 100;

                        DataTable dtint = Dblog.Ora_Execute_table("insert into mem_hibah_proses values('" + txtnobat.Text + "','" + DropDownList2.SelectedValue + "','" + dtcal.Rows[i][1].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "','PROSES','" + famt + "' ,'" + amt + "','" + txtph.Text + "','" + dtcal.Rows[i][3].ToString() + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "','','','"+ dtcal.Rows[i][0].ToString() + "')");

                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dimuatnaik.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    }
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
        grid();
    }
}