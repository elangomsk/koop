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

public partial class PP_LP_Tempoh_view : System.Web.UI.Page
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
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('"+ Session["alrt_msg"].ToString() + "',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);

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
        qry1 = "select *,case when ext_apply_sts_cd ='01' then 'PERMOHONAN' else 'BATAL PERMOHONAN' end as app_sts,case when ext_approve_sts_cd ='A' then 'LULUS' when ext_approve_sts_cd ='NA' then 'TOLAK' else '' end as approve_sts from jpa_extension";
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
            GridView1.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
            //btn_hups.Visible = false;
        }
        else
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        con.Close();
    }

    //protected void Add_profile(object sender, EventArgs e)
    //{
    //    Response.Redirect("../Pelaburan_Anggota/PP_MP_Tempoh.aspx");
    //}
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //System.Web.UI.WebControls.Label sts = (System.Web.UI.WebControls.Label)e.Row.FindControl("lbl_sts1");
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    if (sts.Text != "")
        //    {
        //        if (sts.Text == "DALAM NOTIS" || sts.Text == "ANGGOTA TIDAK SAH")
        //        {
        //            LinkButton Hlnk = e.Row.FindControl("lnkView") as LinkButton;
        //            Hlnk.Visible = true;
        //        }
        //        else
        //        {
        //            LinkButton Hlnk = e.Row.FindControl("lnkView") as LinkButton;
        //            Hlnk.Attributes.Add("style", "pointer-events:None; color:Black;");
        //        }
        //    }
        //}
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnButton = sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
            System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("gd_1");
            string lblid1 = lblTitle.Text;
            DataTable ddokdicno = new DataTable();
            ddokdicno = DBCon.Ora_Execute_table("select * from jpa_extension where ext_applcn_no='" + lblTitle.Text + "' ");
            if (ddokdicno.Rows.Count != 0)
            {
                string name = HttpUtility.UrlEncode(service.Encrypt(lblTitle.Text));
                //Response.Redirect("../kewengan/kw_profil_syarikat.aspx?edit={0}"+ og_genid.Text + "");
                Response.Redirect(string.Format("../Pelaburan_Anggota/PP_LP_Tempoh.aspx?edit={0}", name));
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