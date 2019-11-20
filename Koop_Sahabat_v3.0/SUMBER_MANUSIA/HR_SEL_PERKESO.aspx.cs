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
using System.Threading;

public partial class HR_SEL_PERKESO : System.Web.UI.Page
{

    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    private static int PageSize = 20;
    string qry1 = string.Empty, qry2 = string.Empty;
    string level;
    string Status = string.Empty, Status1 = string.Empty;
    string userid;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
                grid();


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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('474','448','475','1531','1533','476','1534','1535','1536', '61', '15','35')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentUICulture;
            TextInfo txtinfo = culinfo.TextInfo;

            h1_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString());

            bb1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            bb2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString());

            h3_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString());
            h3_tag2.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString());

            lbl1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower() + "(RM)");
            lbl2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower() + "(RM)");
            lbl3_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower() + "(RM)");
            lbl4_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower() + "(RM)");
            lbl5_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower() + "(RM)");

            Button8.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }

    void view_details()
    {

    }


    void grid()
    {
        con.Open();

        SqlCommand cmd = new SqlCommand("SELECT id,per_min_income_amt,per_max_income_amt,per_employer_amt1,per_employee_amt,per_employer_amt2 FROM hr_comm_perkeso where Status='A' ", con);
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
            GridView1.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
        }
        else
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        con.Close();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string LocName = GridView1.DataKeys[e.RowIndex].Values["id"].ToString();
        System.Web.UI.WebControls.TextBox txt_min = (System.Web.UI.WebControls.TextBox)GridView1.Rows[e.RowIndex].FindControl("MINIMUM_(RM)");
        System.Web.UI.WebControls.TextBox txt_max = (System.Web.UI.WebControls.TextBox)GridView1.Rows[e.RowIndex].FindControl("MAKSIMUM_(RM)");
        System.Web.UI.WebControls.TextBox txt_empamt1 = (System.Web.UI.WebControls.TextBox)GridView1.Rows[e.RowIndex].FindControl("MAJIAKAN_(RM)");
        System.Web.UI.WebControls.TextBox txt_empamt = (System.Web.UI.WebControls.TextBox)GridView1.Rows[e.RowIndex].FindControl("KAKITANGAN_(RM)");
        System.Web.UI.WebControls.TextBox txt_empamt2 = (System.Web.UI.WebControls.TextBox)GridView1.Rows[e.RowIndex].FindControl("MAJIAKAN_(RM)");
        con.Open();
        SqlCommand cmd = new SqlCommand("UPDATE hr_comm_perkeso set per_min_income_amt='" + txt_min.Text + "',per_max_income_amt='" + txt_max.Text + "',per_employer_amt1='" + txt_empamt1.Text + "',per_employee_amt='" + txt_empamt.Text + "',per_employer_amt2='" + txt_empamt2.Text + "' where id='" + ID + "'", con);
        cmd.ExecuteNonQuery();
        con.Close();
        GridView1.EditIndex = -1;
        GridView1.DataBind();
    }
    protected void SubmitAppraisalGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grid();
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
    }



    protected void grvMergeHeader_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "<center><STRONG>JULAT GAJI</STRONG></center>";
            HeaderCell.ColumnSpan = 3;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "<center><STRONG>JADUAL 1 (UMUR < 60)</STRONG></center>";
            HeaderCell.ColumnSpan = 2;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "<center><STRONG>JADUAL 2 (UMUR >= 60)</STRONG></center>";
            HeaderCell.ColumnSpan = 1;
            HeaderGridRow.Cells.Add(HeaderCell);

            GridView1.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }


    protected void insert_Click3(object sender, EventArgs e)
    {
        if (jpp_min.Text != "")
        {
            DataTable ddicno_per = new DataTable();
            ddicno_per = DBCon.Ora_Execute_table("select * from hr_comm_perkeso where per_min_income_amt='" + jpp_min.Text + "'");

            if (ddicno_per.Rows.Count == 0)
            {
                DBCon.Ora_Execute_CommamdText("INSERT INTO hr_comm_perkeso (per_min_income_amt,per_max_income_amt,per_employer_amt1,per_employee_amt,per_employer_amt2,per_crt_id,per_crt_dt) values ('" + jpp_min.Text + "','" + jpp_mak.Text + "','" + jpp_maj1.Text + "','" + jpp_kak1.Text + "','" + jpp_maj2.Text + "','" + Session["New"].ToString() + "','" + DateTime.Now + "')");
                service.audit_trail("P0110", "Simpan","","");
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                clr_txt();
            }
            else
            {                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Sudah Wujud.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
        grid();
    }

    void clr_txt()
    {
        jpp_min.Text = "";
        jpp_mak.Text = "";
        jpp_maj1.Text = "";
        jpp_maj2.Text = "";
        jpp_kak1.Text = "";
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView1.Rows)
        {
            //var lblID = row.FindControl("TextBox1") as TextBox;
            //string id = lblID.ToString();
            System.Web.UI.WebControls.TextBox lblTitle = (System.Web.UI.WebControls.TextBox)row.FindControl("TextBox1");
            var txt_min = row.FindControl("txt_min") as System.Web.UI.WebControls.TextBox;
            //txt_min.Attributes.Add("readonly", "readonly");
            //txt_min.ReadOnly  = true;
            var txt_max = row.FindControl("txt_max") as System.Web.UI.WebControls.TextBox;
            var txt_empamt1 = row.FindControl("txt_empamt1") as System.Web.UI.WebControls.TextBox;
            var txt_empamt = row.FindControl("txt_empamt") as System.Web.UI.WebControls.TextBox;
            var txt_empamt2 = row.FindControl("txt_empamt2") as System.Web.UI.WebControls.TextBox;
            //Response.Write(lblFirstName.Text + "<br>");
            SqlCommand comm = new SqlCommand();
            comm.CommandText = "UPDATE hr_comm_perkeso set per_max_income_amt='" + txt_max.Text + "',per_employer_amt1='" + txt_empamt1.Text + "',per_employee_amt='" + txt_empamt.Text + "',per_employer_amt2='" + txt_empamt2.Text + "',per_upd_id='" + Session["New"].ToString() + "',per_upd_dt='" + DateTime.Now + "' where per_min_income_amt='" + txt_min.Text + "' and id='" + lblTitle.Text + "'";
            comm.Connection = con;
            comm.Parameters.AddWithValue("@id", lblTitle.Text.Trim());
            comm.Parameters.AddWithValue("@per_min_income_amt", txt_min.Text.Trim());
            comm.Parameters.AddWithValue("@per_max_income_amt", txt_max.Text.Trim());
            comm.Parameters.AddWithValue("@per_employer_amt1", txt_empamt1.Text.Trim());
            comm.Parameters.AddWithValue("@per_employee_amt", txt_empamt.Text.Trim());
            comm.Parameters.AddWithValue("@per_employer_amt2", txt_empamt2.Text.Trim());
            con.Open();
            comm.ExecuteNonQuery();
            con.Close();
        }
        service.audit_trail("P0110", "Kemaskini","", "");
        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
        grid();        
    }

  
    protected void rst_clk(object sender, EventArgs e)
    {
        clr_txt();
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../SUMBER_MANUSIA/HR_SEL_PERKESO_view.aspx");
    }


}