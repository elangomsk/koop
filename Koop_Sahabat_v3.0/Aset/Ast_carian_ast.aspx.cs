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

public partial class Ast_carian_ast : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();

    string useid = string.Empty;
    string Status = string.Empty;
    string oid = string.Empty;
    string sdd = string.Empty, str_qry = string.Empty;
    string clmfd = string.Empty, clm_name = string.Empty;
    string ss1 = string.Empty;
    string CommandArgument1 = string.Empty, CommandArgument2 = string.Empty, CommandArgument3 = string.Empty, CommandArgument4 = string.Empty, CommandArgument5 = string.Empty, CommandArgument6 = string.Empty, CommandArgument7 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2()})";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;
                kat();
                kod();
                useid = Session["New"].ToString();
               
             
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }



    void kat()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select ast_kategori_code,ast_kategori_desc from Ref_ast_kategori";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_Kategori.DataSource = dt;
            DD_Kategori.DataBind();
            DD_Kategori.DataTextField = "ast_kategori_desc";
            DD_Kategori.DataValueField = "ast_kategori_code";
            DD_Kategori.DataBind();
            DD_Kategori.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void OnSelectedIndexChanged1(object sender, EventArgs e)
    {
        jenaset();
        //grid();
    }

    void jenaset()
    {
        DataSet Ds1 = new DataSet();
        try
        {
            string com1 = "select * from Ref_ast_jenis_aset where Status='A' and ast_cat_Code='" + DD_Kategori.SelectedValue + "' and ast_sub_cat_Code = '" + DD_Sub.SelectedValue + "'";
            SqlDataAdapter adpt = new SqlDataAdapter(com1, con);
            DataTable dt1 = new DataTable();
            adpt.Fill(dt1);
            DropDownList1.DataSource = dt1;
            DropDownList1.DataTextField = "ast_jeniaset_desc";
            DropDownList1.DataValueField = "ast_jeniaset_Code";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void gv_refdata_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSelected.PageIndex = e.NewPageIndex;
        //grid();
    }

    //protected void OnSelectedIndexChanged2(object sender, EventArgs e)
    //{
    //    cmnaset();
    //    grid();
    //}

    //void cmnaset()
    //{
    //    DataSet Ds1 = new DataSet();
    //    try
    //    {
    //        string com1 = "select * from ast_cmn_asset where cas_asset_cat_cd='" + DD_Kategori.SelectedValue + "' and cas_asset_sub_cat_cd='" + DD_Sub.SelectedValue + "' and cas_asset_type_cd='" + DropDownList1.SelectedValue + "'";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com1, con);
    //        DataTable dt1 = new DataTable();
    //        adpt.Fill(dt1);
    //        DropDownList2.DataSource = dt1;
    //        DropDownList2.DataTextField = "cas_asset_desc";
    //        DropDownList2.DataValueField = "cas_asset_cd";
    //        DropDownList2.DataBind();
    //        DropDownList2.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    void kod()
    {
        DataSet Ds = new DataSet();
        try
        {
            //string com = "select stf_staff_no,stf_name from hr_staff_profile";
            string com = "select DISTINCT sas_staff_no as stf_staff_no,sas_staff_name as stf_name from ast_staff_asset";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList2.DataSource = dt;
            DropDownList2.DataBind();
            DropDownList2.DataTextField = "stf_name";
            DropDownList2.DataValueField = "stf_staff_no";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void jaset()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select ast_jeniaset_Code,ast_jeniaset_desc from Ref_ast_jenis_aset";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList1.DataSource = dt;
            DropDownList1.DataBind();
            DropDownList1.DataTextField = "ast_jeniaset_desc";
            DropDownList1.DataValueField = "ast_jeniaset_Code";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void DD_Kategori_SelectedIndexChanged(object sender, EventArgs e)
    {
        string com = "select ast_subkateast_Code,ast_subkateast_desc from Ref_ast_sub_kategri_Aset where ast_kategori_Code='" + DD_Kategori.SelectedItem.Value + "'";
        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        DD_Sub.DataSource = dt;
        DD_Sub.DataBind();
        DD_Sub.DataTextField = "ast_subkateast_desc";
        DD_Sub.DataValueField = "ast_subkateast_Code";
        DD_Sub.DataBind();
        DD_Sub.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        //grid();
        DropDownList1.SelectedValue = "";
    }


    protected void lblSubItem_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
        CommandArgument1 = commandArgs[0];
        CommandArgument2 = commandArgs[1];
        CommandArgument3 = commandArgs[2];
        CommandArgument4 = commandArgs[3];

        DataTable dd1_stf = new DataTable();
        dd1_stf = DBCon.Ora_Execute_table("select sas_staff_no,sas_asset_id from ast_staff_asset where sas_asset_id='" + CommandArgument1 + "'");
        if (dd1_stf.Rows.Count != 0)
        {
            string url = "AST_STAFF.aspx?sas_stno=" + dd1_stf.Rows[0]["sas_staff_no"].ToString();

            Session["ast_id"] = CommandArgument1;
            Response.Redirect("" + url + "");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    protected void Reset_btn(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_carian_ast.aspx");
    }
    void ck_bd()
    {
        if (DD_Kategori.SelectedValue != "" || DD_Sub.SelectedValue != "" || DropDownList1.SelectedValue != "" || DropDownList2.SelectedValue != "" || txt_asetid.Text != "")
        {
            sel_clmflds();

            str_qry = "select sas_asset_id,sas_asset_cat_cd,raka.ast_kategori_desc,sas_asset_sub_cat_cd,rska.ast_subkateast_desc,sas_asset_type_cd,raja.ast_jeniaset_desc,sas_asset_cd,rak.cas_asset_desc,sas_qty,FORMAT(sas_allocate_dt,'dd/MM/yyyy', 'en-us') as sas_allocate_dt from ast_staff_asset ap left join Ref_ast_kategori raka on raka.ast_kategori_code=ap.sas_asset_cat_cd left join Ref_ast_sub_kategri_Aset rska on rska.ast_subkateast_Code=ap.sas_asset_sub_cat_cd left join Ref_ast_jenis_aset raja on raja.ast_jeniaset_Code=ap.sas_asset_type_cd and raja.ast_sub_cat_Code=ap.sas_asset_sub_cat_cd and raja.ast_cat_Code=ap.sas_asset_cat_cd left join ast_cmn_asset rak on rak.cas_asset_cd=ap.sas_asset_cd and rak.cas_asset_cat_cd = ap.sas_asset_cat_cd and rak.cas_asset_sub_cat_cd = ap.sas_asset_sub_cat_cd and rak.cas_asset_type_cd = ap.sas_asset_type_cd WHERE flag_set ='0' and " + clmfd + "";
        }
        else
        {
            str_qry = "select sas_asset_cat_cd as a,raka.ast_kategori_desc,sas_asset_sub_cat_cd as b,rask.ast_subkateast_desc,sas_asset_type_cd as c,raja.ast_jeniaset_desc,sas_asset_id as aid,rak.cas_asset_desc  from ast_staff_asset aa left join Ref_ast_kategori raka on raka.ast_kategori_code=aa.sas_asset_cat_cd  left join Ref_ast_sub_kategri_Aset rask on rask.ast_subkateast_Code=aa.sas_asset_sub_cat_cd left join Ref_ast_jenis_aset raja on raja.ast_jeniaset_Code=aa.sas_asset_type_cd and raja.ast_sub_cat_Code=aa.sas_asset_sub_cat_cd and raja.ast_cat_Code=aa.sas_asset_cat_cd left join ast_cmn_asset rak on rak.cas_asset_cd=aa.sas_asset_cd and rak.cas_asset_cat_cd = aa.sas_asset_cat_cd and rak.cas_asset_sub_cat_cd = aa.sas_asset_sub_cat_cd and rak.cas_asset_type_cd = aa.sas_asset_type_cd  where aa.sas_asset_cd='' group by sas_asset_cat_cd,raka.ast_kategori_desc,sas_asset_sub_cat_cd,rask.ast_subkateast_desc,sas_asset_type_cd,raja.ast_jeniaset_desc,sas_asset_id,rak.cas_asset_desc";
        }
    }

    void sel_clmflds()
    {
        clm_name = "sas";

        if (DD_Kategori.SelectedValue != "" && DD_Sub.SelectedValue != "" && DropDownList1.SelectedValue != "" && DropDownList2.SelectedValue != "" && txt_asetid.Text != "")
        {
            clmfd = "" + clm_name + "_asset_cat_cd='" + DD_Kategori.SelectedValue + "' and " + clm_name + "_asset_sub_cat_cd='" + DD_Sub.SelectedValue + "' and " + clm_name + "_asset_type_cd='" + DropDownList1.SelectedValue + "' and " + clm_name + "_staff_no='" + DropDownList2.SelectedValue + "' and " + clm_name + "_asset_id='" + txt_asetid.Text + "'";
        }
        else if (DD_Kategori.SelectedValue != "" && DD_Sub.SelectedValue == "" && DropDownList1.SelectedValue == "" && DropDownList2.SelectedValue != "" && txt_asetid.Text != "")
        {
            clmfd = "" + clm_name + "_asset_cat_cd='" + DD_Kategori.SelectedValue + "' and " + clm_name + "_staff_no='" + DropDownList2.SelectedValue + "' and " + clm_name + "_asset_id='" + txt_asetid.Text + "'";
        }
        else if (DD_Kategori.SelectedValue == "" && DD_Sub.SelectedValue == "" && DropDownList1.SelectedValue != "" && DropDownList2.SelectedValue != "" && txt_asetid.Text != "")
        {
            clmfd = "" + clm_name + "_staff_no='" + DropDownList2.SelectedValue + "' and " + clm_name + "_asset_id='" + txt_asetid.Text + "'";
        }
        else if (DD_Kategori.SelectedValue != "" && DD_Sub.SelectedValue == "" && DropDownList1.SelectedValue == "" && DropDownList2.SelectedValue == "" && txt_asetid.Text == "")
        {
            clmfd = "" + clm_name + "_asset_cat_cd='" + DD_Kategori.SelectedValue + "'";
        }
        else if (DD_Kategori.SelectedValue != "" && DD_Sub.SelectedValue != "" && DropDownList1.SelectedValue == "" && DropDownList2.SelectedValue == "" && txt_asetid.Text == "")
        {
            clmfd = "" + clm_name + "_asset_cat_cd='" + DD_Kategori.SelectedValue + "' and " + clm_name + "_asset_sub_cat_cd='" + DD_Sub.SelectedValue + "'";
        }
        else if (DD_Kategori.SelectedValue != "" && DD_Sub.SelectedValue != "" && DropDownList1.SelectedValue != "" && DropDownList2.SelectedValue == "" && txt_asetid.Text == "")
        {
            clmfd = "" + clm_name + "_asset_cat_cd='" + DD_Kategori.SelectedValue + "' and " + clm_name + "_asset_sub_cat_cd='" + DD_Sub.SelectedValue + "' and " + clm_name + "_asset_type_cd='" + DropDownList1.SelectedValue + "'";
        }
        else if (DD_Kategori.SelectedValue != "" && DD_Sub.SelectedValue != "" && DropDownList1.SelectedValue != "" && DropDownList2.SelectedValue != "" && txt_asetid.Text == "")
        {
            clmfd = "" + clm_name + "_asset_cat_cd='" + DD_Kategori.SelectedValue + "' and " + clm_name + "_asset_sub_cat_cd='" + DD_Sub.SelectedValue + "' and " + clm_name + "_asset_type_cd='" + DropDownList1.SelectedValue + "' and " + clm_name + "_staff_no='" + DropDownList2.SelectedValue + "'";
        }
        else if (DD_Kategori.SelectedValue != "" && DD_Sub.SelectedValue == "" && DropDownList1.SelectedValue == "" && DropDownList2.SelectedValue != "" && txt_asetid.Text == "")
        {
            clmfd = "" + clm_name + "_asset_cat_cd='" + DD_Kategori.SelectedValue + "' and " + clm_name + "_staff_no='" + DropDownList2.SelectedValue + "'";
        }
        else if (DD_Kategori.SelectedValue == "" && DD_Sub.SelectedValue == "" && DropDownList1.SelectedValue == "" && DropDownList2.SelectedValue != "" && txt_asetid.Text == "")
        {
            clmfd = "" + clm_name + "_staff_no='" + DropDownList2.SelectedValue + "'";
        }
        else if (DD_Kategori.SelectedValue == "" && DD_Sub.SelectedValue == "" && DropDownList1.SelectedValue == "" && DropDownList2.SelectedValue != "" && txt_asetid.Text != "")
        {
            clmfd = "" + clm_name + "_staff_no='" + DropDownList2.SelectedValue + "' and " + clm_name + "_asset_id='" + txt_asetid.Text + "'";
        }
        else if (DD_Kategori.SelectedValue != "" && DD_Sub.SelectedValue == "" && DropDownList1.SelectedValue == "" && DropDownList2.SelectedValue == "" && txt_asetid.Text != "")
        {
            clmfd = "" + clm_name + "_asset_cat_cd='" + DD_Kategori.SelectedValue + "' and " + clm_name + "_asset_id='" + txt_asetid.Text + "'";
        }
        else if (DD_Kategori.SelectedValue != "" && DD_Sub.SelectedValue != "" && DropDownList1.SelectedValue == "" && DropDownList2.SelectedValue == "" && txt_asetid.Text != "")
        {
            clmfd = "" + clm_name + "_asset_cat_cd='" + DD_Kategori.SelectedValue + "' and " + clm_name + "_asset_sub_cat_cd='" + DD_Sub.SelectedValue + "' and " + clm_name + "_asset_id='" + txt_asetid.Text + "'";
        }
        else if (DD_Kategori.SelectedValue != "" && DD_Sub.SelectedValue != "" && DropDownList1.SelectedValue != "" && DropDownList2.SelectedValue == "" && txt_asetid.Text != "")
        {
            clmfd = "" + clm_name + "_asset_cat_cd='" + DD_Kategori.SelectedValue + "' and " + clm_name + "_asset_sub_cat_cd='" + DD_Sub.SelectedValue + "' and " + clm_name + "_asset_type_cd='" + DropDownList1.SelectedValue + "' and " + clm_name + "_asset_id='" + txt_asetid.Text + "'";
        }
        else if (DD_Kategori.SelectedValue == "" && DD_Sub.SelectedValue == "" && DropDownList1.SelectedValue == "" && DropDownList2.SelectedValue == "" && txt_asetid.Text != "")
        {
            clmfd = "" + clm_name + "_asset_id='" + txt_asetid.Text + "'";
        }
        else
        {
            clmfd = "" + clm_name + "_asset_cat_cd=''";
        }

    }

    protected void Btn_Carian_Click(object sender, EventArgs e)
    {
        if (DD_Kategori.SelectedValue != "" || DD_Sub.SelectedValue != "" || DropDownList1.SelectedValue != "" || DropDownList2.SelectedValue != "" || txt_asetid.Text != "")
        {
            grid();
        }
        else
        {
            grid();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }

    }

    void grid()
    {
        ck_bd();


        DataTable dt = new DataTable();

        DataSet dsCustomers = GetData("" + str_qry + "");
        dt = dsCustomers.Tables[0];

        DataSet ds1 = new DataSet();
        ReportViewer1.Reset();

        List<DataRow> listResult = dt.AsEnumerable().ToList();
        listResult.Count();
        int countRow = 0;
        countRow = listResult.Count();

        if (countRow != 0)
        {
            disp_hdr_txt.Visible = true;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("sas1", dt);
            ReportViewer1.LocalReport.DataSources.Add(rds);
            //Path
            ReportViewer1.LocalReport.ReportPath = "Aset/AST_staff_list.rdlc";
            //Parameters
            string v1 = string.Empty, v2 = string.Empty, v3 = string.Empty, v4 = string.Empty, v5 = string.Empty;
            if (DD_Kategori.SelectedValue != "")
            {
                v1 = DD_Kategori.SelectedItem.Text;
            }
            else
            {
                v1 = "SEMUA";
            }

            if (DD_Sub.SelectedValue != "")
            {
                v2 = DD_Sub.SelectedItem.Text;
            }
            else
            {
                v2 = "SEMUA";
            }

            if (DropDownList1.SelectedValue != "")
            {
                v3 = DropDownList1.SelectedItem.Text;
            }
            else
            {
                v3 = "SEMUA";
            }

            if (DropDownList2.SelectedValue != "")
            {
                v4 = DropDownList2.SelectedItem.Text;
            }
            else
            {
                v4 = "SEMUA";
            }

            if (txt_asetid.Text != "")
            {
                v5 = txt_asetid.Text;
            }
            else
            {
                v5 = "SEMUA";
            }

            ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("d1",v1),
                     new ReportParameter("d2",v2),
                     new ReportParameter("d3",v3),
                     new ReportParameter("d4",v4),
                     new ReportParameter("d5",v5),
                };

            string extfile = DropDownList2.SelectedValue;
            ReportViewer1.LocalReport.DisplayName = "SEMAKAN_PENEMPATAN_ASET_" + extfile + "_" + DateTime.Now.ToString("ddMMyyyy");
            ReportViewer1.LocalReport.SetParameters(rptParams);
            //Refresh
            ReportViewer1.LocalReport.Refresh();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod tidak dijumpai. Sila pastikan semua maklumat dimasukkan dengan betul.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    private DataSet GetData(string query)
    {
        string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
        SqlCommand cmd = new SqlCommand(query);
        using (SqlConnection con = new SqlConnection(conString))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                cmd.Connection = con;
                cmd.CommandTimeout = 200;

                sda.SelectCommand = cmd;
                using (DataSet dsCustomers = new DataSet())
                {
                    sda.Fill(dsCustomers, "Payment");
                    return dsCustomers;
                }
            }
        }
    }

    
    
}