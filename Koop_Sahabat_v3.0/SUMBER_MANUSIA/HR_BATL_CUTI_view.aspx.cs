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

public partial class HR_BATL_CUTI_view : System.Web.UI.Page
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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                if (Session["validate_success"].ToString() == "SUCCESS")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('" + Session["alrt_msg"].ToString() + "',{'type': 'confirmation','title': 'Success'});", true);

                }
                Session["validate_success"] = "";
                Session["alrt_msg"] = "";
                userid = Session["New"].ToString();
               // BindData_Grid();
                bind2();
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
   // protected void BindData_Grid()
    //{

       // string sqry = string.Empty;
        //if (txtSearch.Text == "")
        //{
        //    sqry = "";
        //}
        //else
        //{
        //    sqry = "where s2.Ref_Jenis_cukai LIKE'%" + txtSearch.Text + "%'";
        //}
       // con.Open();
       // DataTable ddicno = new DataTable();
       // SqlCommand cmd = new SqlCommand("select a1.lev_staff_no,a1.hr_jenis_desc,a1.a,a1.c,a1.b, a1.ab,ISNULL(b1.e,'0') as e,a1.d,((convert(int,(a1.c)) + a1.a) - (convert(int,(a1.d)) + ISNULL(b1.e,'0'))) as res from (select lev_staff_no,lev_leave_type_cd,hr_jenis_desc,lev_carry_fwd_day as a,lev_entitle_day as c,convert(int,(((lev_entitle_day / 12) * DATEPART(month, GETDATE())))) as b,(lev_carry_fwd_day + convert(int,(((lev_entitle_day / 12) * DATEPART(month, GETDATE()))))) as ab,lev_taken_day as d,lev_balance_day from hr_staff_profile as SP left join hr_leave as LV on LV.lev_staff_no=SP.stf_staff_no and LV.lev_org_id=sp.str_curr_org_cd left join Ref_hr_jenis_cuti as JC on JC.hr_jenis_Code=LV.lev_leave_type_cd where stf_staff_no='" + Session["New"].ToString() + "' AND lev_year='" + year + "') as a1 full outer join(select lap_staff_no,lap_leave_type_cd,sum(lap_leave_day) as e from hr_leave_application where lap_staff_no='" + Session["New"].ToString() + "' and lap_cancel_ind='N' and lap_leave_type_cd IN('01','04','05') and lap_approve_sts_cd NOT IN ('04','01') group by lap_staff_no,lap_leave_type_cd) as b1 on b1.lap_staff_no=a1.lev_staff_no and b1.lap_leave_type_cd=a1.lev_leave_type_cd", con);
       // SqlDataAdapter da = new SqlDataAdapter(cmd);
       // DataSet ds = new DataSet();
       // da.Fill(ds);
       // if (ds.Tables[0].Rows.Count == 0)
       // {
          //  ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
           // gvSelected.DataSource = ds;
           // gvSelected.DataBind();
           // int columncount = gvSelected.Rows[0].Cells.Count;
           // gvSelected.Rows[0].Cells.Clear();
           // gvSelected.Rows[0].Cells.Add(new TableCell());
           // gvSelected.Rows[0].Cells[0].ColumnSpan = columncount;
           // gvSelected.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
       // }
       // else
       // {
          //  gvSelected.DataSource = ds;
          //  gvSelected.DataBind();
       // }

        //con.Close();
   // }
    public void bind2()
    {
    string sqry = string.Empty;
    //string stffno = Session["New"].ToString();
    //string year = DateTime.Now.ToString("yyyy");
    //dt = dbcon.Ora_Execute_table("select FORMAT(lap_application_dt,'dd/MM/yyyy', 'en-us') as lap_application_dt,hr_jenis_desc,lap_leave_day, FORMAT(lap_leave_start_dt,'dd/MM/yyyy', 'en-us') as lap_leave_start_dt, FORMAT(lap_leave_end_dt,'dd/MM/yyyy', 'en-us') as lap_leave_end_dt,hr_leave_desc,lap_approve_sts_cd,lap_leave_type_cd From hr_leave_application as LP left join Ref_hr_jenis_cuti as JC on JC.hr_jenis_Code=LP.lap_leave_type_cd left join Ref_hr_leave_sts as LS on LS.hr_leave_Code=LP.lap_approve_sts_cd where lap_staff_no='" + stffno + "' and year(lap_application_dt)='" + year + "' order by lap_application_dt desc");
    //GridView1.DataSource = dt;
    //GridView1.DataBind();

    SqlCommand cmd = new SqlCommand("select FORMAT(lap_application_dt,'dd/MM/yyyy', 'en-us') as lap_application_dt,hr_jenis_desc,lap_leave_day, FORMAT(lap_leave_start_dt,'dd/MM/yyyy', 'en-us') as lap_leave_start_dt, FORMAT(lap_leave_end_dt,'dd/MM/yyyy', 'en-us') as lap_leave_end_dt,hr_leave_desc,lap_approve_sts_cd,lap_leave_type_cd,lap_ref_no From hr_leave_application as LP left join Ref_hr_jenis_cuti as JC on JC.hr_jenis_Code=LP.lap_leave_type_cd left join Ref_hr_leave_sts as LS on LS.hr_leave_Code=LP.lap_approve_sts_cd where lap_staff_no='' and year(lap_application_dt)='' order by lap_application_dt desc", con);
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

    }

    //protected void txtSearch_TextChanged(object sender, EventArgs e)
    //{

    //    SearchText();
    //}

    //protected void btn_search_Click(object sender, EventArgs e)
    //{
    //    BindData_jenis();
    //}



    //public string Highlight(string InputTxt)
    //{
    //    string Search_Str = txtSearch.Text.ToString();
    //    // Setup the regular expression and add the Or operator.
    //    Regex RegExp = new Regex(Search_Str.Replace(" ", "|").Trim(),
    //    RegexOptions.IgnoreCase);

    //    // Highlight keywords by calling the 
    //    //delegate each time a keyword is found.
    //    return RegExp.Replace(InputTxt,
    //    new MatchEvaluator(ReplaceKeyWords));

    //    // Set the RegExp to null.
    //    RegExp = null;

    //}

    //public string ReplaceKeyWords(Match m)
    //{

    //    return "<span class=highlight>" + m.Value + "</span>";

    //}
    //private void SearchText()
    //{
    //    BindData_jenis();

    //}

    protected void Add_profile(object sender, EventArgs e)
    {
        Response.Redirect("../SUMBER_MANUSIA/HR_BATL_CUTI.aspx");
    }

    protected void btn_hups_Click(object sender, EventArgs e)
    {
        string rcount = string.Empty;
        int count = 0;
        foreach (GridViewRow gvrow in GridView1.Rows)
        {
            var rb = gvrow.FindControl("RadioButton1") as System.Web.UI.WebControls.CheckBox;
            if (rb.Checked)
            {
                count++;
            }
            rcount = count.ToString();
        }
        if (rcount != "0")
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                var rbn = row.FindControl("RadioButton1") as System.Web.UI.WebControls.CheckBox;
                if (rbn.Checked)
                {
                    int RowIndex = row.RowIndex;
                    string varName1 = ((System.Web.UI.WebControls.Label)row.FindControl("lbl1_id")).Text.ToString(); //this store the  value in varName1
                    SqlCommand ins_peng = new SqlCommand("Delete from hr_leave_application where id='" + varName1 + "'", conn);
                    conn.Open();
                    int i = ins_peng.ExecuteNonQuery();
                    conn.Close();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dihapuskan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                   // BindData_Grid();
                    bind2();
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
        string script = " $(function () {$(" + GridView1.ClientID + ").prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
       // BindData_Grid();
        bind2();
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnButton = sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
            System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("lbl1_id");
            string lblid1 = lblTitle.Text;
            DataTable ddokdicno = new DataTable();
            ddokdicno = DBCon.Ora_Execute_table("select * From hr_leave_application where Id='" + lblTitle.Text + "' ");
            if (ddokdicno.Rows.Count != 0)
            {
                string name = HttpUtility.UrlEncode(service.Encrypt(lblTitle.Text));
                //Response.Redirect("../kewengan/kw_profil_syarikat.aspx?edit={0}"+ og_genid.Text + "");
                Response.Redirect(string.Format("../SUMBER_MANUSIA/HR_BATL_CUTI.aspx?edit={0}", name));
            }
            else
            {
                Session["validate_success"] = "";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
}