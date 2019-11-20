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


public partial class PP_Guaman_cagaran_view : System.Web.UI.Page
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
                Session["con_id"] = "";
                userid = Session["New"].ToString();
                BindData();
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }


    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {

            LinkButton btnButton = sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
            System.Web.UI.WebControls.Label txtappno = (System.Web.UI.WebControls.Label)gvRow.FindControl("txtappno");
            string appno = txtappno.Text;
            DataTable ddokdicno = new DataTable();
            ddokdicno = Dblog.Ora_Execute_table("select * from jpa_application where app_applcn_no = '" + appno + "'");
            if (ddokdicno.Rows.Count != 0)
            {
                string name = HttpUtility.UrlEncode(service.Encrypt(appno));
                //Response.Redirect("../kewengan/kw_profil_syarikat.aspx?edit={0}"+ og_genid.Text + "");
                Response.Redirect(string.Format("../PELABURAN_ANGGOTA/PP_Guaman_cagaran.aspx?edit={0}", name));
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


    protected void BindData()
    {

        string sqry = string.Empty;
        //if (txtappno.Text
        //    == "")
        //{
        //    sqry = "";
        //}
        //else
        //{
        //    sqry = "where app_applcn_no='" + txtappno.Text + "' and jkk_result_ind='L'";
        //}

        qry1 = "select ja.app_name,ja.app_new_icno,rc.branch_desc,rw.Wilayah_Name,ja.app_loan_amt,ja.appl_loan_dur,ja.app_applcn_no from jpa_application as ja left join jpa_jkkpa_approval as jk on jk.jkk_applcn_no=ja.app_applcn_no left join ref_branch as rc on rc.branch_cd=ja.app_branch_cd left join ref_wilayah as rw on rw.Wilayah_Code=ja.app_region_cd  where  jkk_result_ind='L'";
        SqlCommand cmd2 = new SqlCommand("" + qry1 + "", con);
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
    //protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    gv_refdata.PageIndex = e.NewPageIndex;
    //    BindData();

    //}
    //protected void srch_id_TextChanged(object sender, EventArgs e)
    //{
    //    BindData();

    //}

    //protected void btn_search_Click(object sender, EventArgs e)
    //{
    //    BindData();

    //}
    //string Form2;

    protected void Add_profile(object sender, EventArgs e)
    {
        Response.Redirect("../PELABURAN_ANGGOTA/PP_Guaman_cagaran.aspx");
    }

    //protected void btn_hups_Click(object sender, EventArgs e)
    //{
    //    string rcount = string.Empty;
    //    int count = 0;
    //    foreach (GridViewRow gvrow in gv_refdata.Rows)
    //    {
    //        var rb = gvrow.FindControl("RadioButton1") as System.Web.UI.WebControls.CheckBox;
    //        if (rb.Checked)
    //        {
    //            count++;
    //        }
    //        rcount = count.ToString();
    //    }
    //    if (rcount != "0")
    //    {
    //        foreach (GridViewRow row in gv_refdata.Rows)
    //        {
    //            System.Web.UI.WebControls.CheckBox rbn = new System.Web.UI.WebControls.CheckBox();
    //            rbn = (System.Web.UI.WebControls.CheckBox)row.FindControl("RadioButton1");
    //            if (rbn.Checked)
    //            {
    //                int RowIndex = row.RowIndex;
    //                string varName1 = ((System.Web.UI.WebControls.Label)row.FindControl("Label1")).Text.ToString(); //this store the  value in varName1

    //                DataTable ddokdicno01 = new DataTable();
    //                ddokdicno01 = DBCon.Ora_Execute_table("select syar_logo,tarikh_mula,tarikh_akhir,syar_Cd from KW_Profile_syarikat where id = '" + varName1 + "'");
    //                string checkimage01 = ddokdicno01.Rows[0]["syar_logo"].ToString();
    //                DateTime smp1 = DateTime.Parse(ddokdicno01.Rows[0]["tarikh_mula"].ToString());
    //                //SqlCommand prof_del = new SqlCommand("update KW_financial_Year set Status='T' where fin_start_dt>'" + smp1.Date.ToString("MM/dd/yyyy") + "' and fin_Syar_Cd='" + ddokdicno01.Rows[0]["syar_Cd"] + "'", con);
    //                //SqlCommand ins_peng = new SqlCommand("Delete from KW_Profile_syarikat where id='" + varName1 + "'", con);
    //                SqlCommand ins_peng = new SqlCommand("update KW_Profile_syarikat set Status='T' where id='" + varName1 + "'", con);

    //                con.Open();
    //                int i = ins_peng.ExecuteNonQuery();
    //                con.Close();

    //                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dihapuskan.',{'type': 'confirmation','title': 'success','auto_close': 2000}); ", true);
    //                //BindData();

    //            }
    //        }
    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan.',{'type': 'warning','title': 'warning','auto_close': 2000}); ", true);
    //    }
    //    string script = " $(function () {$(" + gv_refdata.ClientID + ").prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
    //    BindData();
    //}
}