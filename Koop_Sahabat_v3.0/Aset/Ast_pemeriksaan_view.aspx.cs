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

public partial class Ast_pemeriksaan_view : System.Web.UI.Page
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
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('"+ Session["alrt_msg"].ToString() + "',{'type': 'confirmation','title': 'Success'});", true);

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
        qry1 = "select s1.sas_asset_id,s2.stf_name,s1.sas_qty,s3.ast_kategori_desc,Format(s1.sas_allocate_dt,'dd/MM/yyyy') sas_allocate_dt,case when Format(ISNULL(s1.sas_inspect_dt,''),'dd/MM/yyyy')='01/01/1900' then '' else Format(ISNULL(s1.sas_inspect_dt,''),'dd/MM/yyyy') end as inspect_dt,ISNULL(s4.ast_pemerikksaan_desc,'') cond_desc,ISNULL(s5.ast_justifikasi_desc,'') just_txt from ast_staff_asset s1 left join hr_staff_profile s2 on s2.stf_staff_no=s1.sas_staff_no left join Ref_ast_kategori s3 on s3.ast_kategori_code=s1.sas_asset_cat_cd  left join Ref_ast_sts_pemerikksaan s4 on s4.ast_pemerikksaan_code=s1.sas_cond_sts_cd left join Ref_ast_justifikasi s5 on s5.ast_justifikasi_code=s1.sas_justify_cd";
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
            GridView1.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
            //btn_hups.Visible = false;
        }
        else
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        con.Close();
    }

   
    protected void Add_profile(object sender, EventArgs e)
    {
        Response.Redirect("../Aset/Ast_pemeriksaan.aspx");
    }

    //protected void btn_hups_Click(object sender, EventArgs e)
    //{

    //    string rcount = string.Empty;
    //    int count = 0;
    //    foreach (GridViewRow gvrow in GridView1.Rows)
    //    {
    //        var rb = gvrow.FindControl("chkRow") as System.Web.UI.WebControls.CheckBox;
    //        if (rb.Checked)
    //        {
    //            count++;
    //        }
    //        rcount = count.ToString();
    //    }
    //    if (rcount != "0")
    //    {

    //        try
    //        {
    //            foreach (GridViewRow row in GridView1.Rows)
    //            {
    //                System.Web.UI.WebControls.CheckBox rbn = new System.Web.UI.WebControls.CheckBox();
    //                rbn = (System.Web.UI.WebControls.CheckBox)row.FindControl("chkRow");
    //                if (rbn.Checked)
    //                {
    //                    int RowIndex = row.RowIndex;
    //                    string varName1 = ((System.Web.UI.WebControls.Label)row.FindControl("Label5")).Text.ToString(); //this store the  value in varName1
    //                    SqlCommand ins_peng = new SqlCommand("delete from ref_location where ID='" + varName1 + "'", con);
    //                    con.Open();
    //                    int i = ins_peng.ExecuteNonQuery();
    //                    con.Close();
    //                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dihapuskan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
    //                }
    //            }
    //            BindData_jenis();
    //        }
    //        catch
    //        {
    //            con.Close();
    //            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Sedang Digunakan Oleh Modul Lain.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
    //        }

    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
    //    }
    //    string script = " $(function () {$(" + GridView1.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
    //}
    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnButton = sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
            System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("Label5");
            string lblid1 = lblTitle.Text;
            DataTable ddokdicno = new DataTable();
            ddokdicno = DBCon.Ora_Execute_table("select * From ast_cmn_asset where Id='" + lblTitle.Text + "' ");
            if (ddokdicno.Rows.Count != 0)
            {
                string name = HttpUtility.UrlEncode(service.Encrypt(lblTitle.Text));
                //Response.Redirect("../kewengan/kw_profil_syarikat.aspx?edit={0}"+ og_genid.Text + "");
                Response.Redirect(string.Format("../Aset/Ast_lokasi.aspx?edit={0}", name));
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