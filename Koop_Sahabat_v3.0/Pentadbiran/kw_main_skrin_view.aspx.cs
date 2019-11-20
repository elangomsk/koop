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


public partial class kw_main_skrin_view : System.Web.UI.Page
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
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('" + Session["alrt_msg"].ToString() + "',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                  
                }
                Session["validate_success"] = "";
                Session["alrt_msg"] = "";
                userid = Session["New"].ToString();
                BindData();
                
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    //protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    BindData();
    //    gv_refdata.PageIndex = e.NewPageIndex;
    //    gv_refdata.DataBind();
    //}

    //protected void srch_id_TextChanged(object sender, EventArgs e)
    //{
    //    BindData();

    //}

    protected void BindData()
    {
        string sqry = string.Empty;
        //if (srch_id.Text == "")
        //{
        //    sqry = "";
        //}
        //else
        //{
        //    sqry = "where kat_akuan like '%" + srch_id.Text + "%'";
        //}
        qry1 = "SELECT *,ROW_NUMBER() over (order by ID) as inc_id, case when Status = 'A' then 'AKTIF' else 'TIDAK AKTIF' end as sts FROM KK_PID_Skrin order by position";
        SqlCommand cmd2 = new SqlCommand("" + qry1 + "", conn);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            gv_refdata.DataSource = ds2;
            gv_refdata.DataBind();
            int columncount = gv_refdata.Rows[0].Cells.Count;
            gv_refdata.Rows[0].Cells.Clear();
            gv_refdata.Rows[0].Cells.Add(new TableCell());
            gv_refdata.Rows[0].Cells[0].ColumnSpan = columncount;
            gv_refdata.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
        }
        else
        {
            gv_refdata.DataSource = ds2;
            gv_refdata.DataBind();

        }
    }
      

    //protected void btn_search_Click(object sender, EventArgs e)
    //{
    //    BindData();

    //}

    protected void Add_profile(object sender, EventArgs e)
    {
        Response.Redirect("../Pentadbiran/kw_main_skrin.aspx");
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            //BindData();
          
            LinkButton btnButton = sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
            System.Web.UI.WebControls.Label og_genid = (System.Web.UI.WebControls.Label)gvRow.FindControl("og_genid");
            string ogid = og_genid.Text;
            DataTable ddokdicno = new DataTable();
            ddokdicno = Dblog.Ora_Execute_table("select * from KK_PID_Skrin where id = '" + ogid + "'");
            if (ddokdicno.Rows.Count != 0)
            {
                string name = HttpUtility.UrlEncode(service.Encrypt(ogid));
                //Response.Redirect("../kewengan/kw_profil_syarikat.aspx?edit={0}"+ og_genid.Text + "");
                Response.Redirect(string.Format("../Pentadbiran/kw_main_skrin.aspx?edit={0}", name));
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