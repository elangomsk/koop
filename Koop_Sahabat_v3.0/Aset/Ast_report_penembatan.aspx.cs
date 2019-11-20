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

public partial class Ast_report_penembatan : System.Web.UI.Page
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
    string sdd = string.Empty, st_qry = string.Empty;
    string clmfd = string.Empty, clm_name = string.Empty;
    string ss1 = string.Empty;
    string CommandArgument1 = string.Empty, CommandArgument2 = string.Empty, CommandArgument3 = string.Empty, CommandArgument4 = string.Empty, CommandArgument5 = string.Empty, CommandArgument6 = string.Empty, CommandArgument7 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Btn_Carian);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;
                kat();
                org();
                useid = Session["New"].ToString();
               
             
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }


    protected void DD_Kategori_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet Ds = new DataSet();
        try
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
            //Grid_bnd();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void OnSelectedIndexChanged1(object sender, EventArgs e)
    {
        jenaset();
        //Grid_bnd();
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

    protected void OnSelectedIndexChanged2(object sender, EventArgs e)
    {
        cmnaset();
        //Grid_bnd();

    }

    void cmnaset()
    {
        DataSet Ds1 = new DataSet();
        try
        {
            string com1 = "select * from ast_cmn_asset where cas_asset_cat_cd='" + DD_Kategori.SelectedValue + "' and cas_asset_sub_cat_cd='" + DD_Sub.SelectedValue + "' and cas_asset_type_cd='" + DropDownList1.SelectedValue + "'";
            SqlDataAdapter adpt = new SqlDataAdapter(com1, con);
            DataTable dt1 = new DataTable();
            adpt.Fill(dt1);
            DropDownList2.DataSource = dt1;
            DropDownList2.DataTextField = "cas_asset_desc";
            DropDownList2.DataValueField = "cas_asset_cd";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //void kod()
    //{
    //    DataSet Ds = new DataSet();
    //    try
    //    {

    //        string com = "select ast_kodast_Code,cas_asset_desc from Ref_ast_kod_aset";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
    //        DataTable dt = new DataTable();
    //        adpt.Fill(dt);
    //        DropDownList2.DataSource = dt;
    //        DropDownList2.DataBind();
    //        DropDownList2.DataTextField = "cas_asset_desc";
    //        DropDownList2.DataValueField = "ast_kodast_Code";
    //        DropDownList2.DataBind();
    //        DropDownList2.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

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



    void org()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select org_gen_id,org_name from hr_organization";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList5.DataSource = dt;
            DropDownList5.DataBind();
            DropDownList5.DataTextField = "org_name";
            DropDownList5.DataValueField = "org_gen_id";
            DropDownList5.DataBind();
            DropDownList5.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
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
    protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select loc_cd,loc_desc from ref_location where org_gen_id='" + DropDownList5.SelectedItem.Value + "'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList6.DataSource = dt;
            DropDownList6.DataBind();
            DropDownList6.DataTextField = "loc_desc";
            DropDownList6.DataValueField = "loc_cd";
            DropDownList6.DataBind();
            DropDownList6.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            //Grid_bnd();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void Btn_Carian_Click(object sender, EventArgs e)
    {
        if (txtdari.Text != "" && txtseh.Text != "")
        {
            Grid_bnd();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    void Grid_bnd()
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        string fdate = string.Empty, tmdate = string.Empty, tdate = string.Empty, tddate = string.Empty;

        if (txtdari.Text != "")
        {
            fdate = txtdari.Text;
            DateTime tt = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            tmdate = tt.ToString("yyyy/MM/dd");
        }

        if (txtseh.Text != "")
        {
            tdate = txtseh.Text;
            DateTime td = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            tddate = td.ToString("yyyy/MM/dd");
        }

        if (txtdari.Text != "" && txtseh.Text != "" && DD_Kategori.SelectedValue == "" && DD_Sub.Text == "" && DropDownList1.SelectedValue == "" && DropDownList2.SelectedValue == "" && DropDownList5.SelectedValue == "" && DropDownList6.Text == "")
        {

            dt = DBCon.Ora_Execute_table("select sas_org_id,org_name,sas_location_cd,loc_desc,sas_asset_cat_cd,ast_kategori_desc,sas_asset_sub_cat_cd,ast_subkateast_desc,sas_asset_type_cd,ast_jeniaset_desc,sas_asset_cd,cas_asset_desc,ISNULL(sas_purchase_amt,'') as sas_purchase_amt  from ast_staff_asset left join hr_organization on org_gen_id=sas_org_id left join Ref_ast_kategori on ast_kategori_code=sas_asset_cat_cd  left join ref_location on loc_cd=sas_location_cd left join Ref_ast_sub_kategri_Aset on ast_subkateast_Code=sas_asset_sub_cat_cd left join ast_cmn_asset  on cas_asset_cd=sas_asset_cd and cas_asset_cat_cd=sas_asset_cat_cd and cas_asset_sub_cat_cd=sas_asset_sub_cat_cd and cas_asset_type_cd=sas_asset_type_cd left join Ref_ast_jenis_aset on sas_asset_type_cd=ast_jeniaset_Code where  sas_reg_dt>=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), 0) and sas_reg_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tddate + "'), +1) ");
        }
        else if (txtdari.Text != "" && txtseh.Text != "" && DD_Kategori.SelectedValue != "" && DD_Sub.SelectedValue == "" && DropDownList1.SelectedValue == "" && DropDownList2.SelectedValue == "" && DropDownList5.SelectedValue == "" && DropDownList6.Text == "")
        {
            dt = DBCon.Ora_Execute_table("select sas_org_id,org_name,sas_location_cd,loc_desc,sas_asset_cat_cd,ast_kategori_desc,sas_asset_sub_cat_cd,ast_subkateast_desc,sas_asset_type_cd,ast_jeniaset_desc,sas_asset_cd,cas_asset_desc,ISNULL(sas_purchase_amt,'') as sas_purchase_amt  from ast_staff_asset left join hr_organization on org_gen_id=sas_org_id left join Ref_ast_kategori on ast_kategori_code=sas_asset_cat_cd  left join ref_location on loc_cd=sas_location_cd left join Ref_ast_sub_kategri_Aset on ast_subkateast_Code=sas_asset_sub_cat_cd left join ast_cmn_asset  on cas_asset_cd=sas_asset_cd and cas_asset_cat_cd=sas_asset_cat_cd and cas_asset_sub_cat_cd=sas_asset_sub_cat_cd and cas_asset_type_cd=sas_asset_type_cd left join Ref_ast_jenis_aset on sas_asset_type_cd=ast_jeniaset_Code where  sas_reg_dt>=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), 0) and sas_reg_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tddate + "'), +1) and sas_asset_cat_cd ='" + DD_Kategori.SelectedItem.Value + "' ");
        }
        else if (txtdari.Text != "" && txtseh.Text != "" && DD_Kategori.SelectedValue != "" && DD_Sub.SelectedValue != "" && DropDownList1.SelectedValue == "" && DropDownList2.SelectedValue == "" && DropDownList5.SelectedValue == "" && DropDownList6.Text == "")
        {
            dt = DBCon.Ora_Execute_table("select sas_org_id,org_name,sas_location_cd,loc_desc,sas_asset_cat_cd,ast_kategori_desc,sas_asset_sub_cat_cd,ast_subkateast_desc,sas_asset_type_cd,ast_jeniaset_desc,sas_asset_cd,cas_asset_desc,ISNULL(sas_purchase_amt,'') as sas_purchase_amt  from ast_staff_asset left join hr_organization on org_gen_id=sas_org_id left join Ref_ast_kategori on ast_kategori_code=sas_asset_cat_cd  left join ref_location on loc_cd=sas_location_cd left join Ref_ast_sub_kategri_Aset on ast_subkateast_Code=sas_asset_sub_cat_cd left join ast_cmn_asset  on cas_asset_cd=sas_asset_cd and cas_asset_cat_cd=sas_asset_cat_cd and cas_asset_sub_cat_cd=sas_asset_sub_cat_cd and cas_asset_type_cd=sas_asset_type_cd left join Ref_ast_jenis_aset on sas_asset_type_cd=ast_jeniaset_Code where  sas_reg_dt>=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), 0) and sas_reg_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tddate + "'), +1) and sas_asset_cat_cd ='" + DD_Kategori.SelectedItem.Value + "' and sas_asset_sub_cat_cd ='" + DD_Sub.SelectedItem.Value + "' ");
        }
        else if (txtdari.Text != "" && txtseh.Text != "" && DD_Kategori.SelectedValue != "" && DD_Sub.SelectedValue != "" && DropDownList1.SelectedValue != "" && DropDownList2.SelectedValue == "" && DropDownList5.SelectedValue == "" && DropDownList6.Text == "")
        {
            dt = DBCon.Ora_Execute_table("select sas_org_id,org_name,sas_location_cd,loc_desc,sas_asset_cat_cd,ast_kategori_desc,sas_asset_sub_cat_cd,ast_subkateast_desc,sas_asset_type_cd,ast_jeniaset_desc,sas_asset_cd,cas_asset_desc,ISNULL(sas_purchase_amt,'') as sas_purchase_amt  from ast_staff_asset left join hr_organization on org_gen_id=sas_org_id left join Ref_ast_kategori on ast_kategori_code=sas_asset_cat_cd  left join ref_location on loc_cd=sas_location_cd left join Ref_ast_sub_kategri_Aset on ast_subkateast_Code=sas_asset_sub_cat_cd left join ast_cmn_asset  on cas_asset_cd=sas_asset_cd and cas_asset_cat_cd=sas_asset_cat_cd and cas_asset_sub_cat_cd=sas_asset_sub_cat_cd and cas_asset_type_cd=sas_asset_type_cd left join Ref_ast_jenis_aset on sas_asset_type_cd=ast_jeniaset_Code where  sas_reg_dt>=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), 0) and sas_reg_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tddate + "'), +1) and sas_asset_cat_cd ='" + DD_Kategori.SelectedItem.Value + "' and sas_asset_sub_cat_cd ='" + DD_Sub.SelectedItem.Value + "'  and sas_asset_type_cd ='" + DropDownList1.SelectedItem.Value + "'");
        }
        else if (txtdari.Text != "" && txtseh.Text != "" && DD_Kategori.SelectedValue != "" && DD_Sub.SelectedValue != "" && DropDownList1.SelectedValue != "" && DropDownList2.SelectedValue != "" && DropDownList5.SelectedValue == "" && DropDownList6.Text == "")
        {
            dt = DBCon.Ora_Execute_table("select sas_org_id,org_name,sas_location_cd,loc_desc,sas_asset_cat_cd,ast_kategori_desc,sas_asset_sub_cat_cd,ast_subkateast_desc,sas_asset_type_cd,ast_jeniaset_desc,sas_asset_cd,cas_asset_desc,ISNULL(sas_purchase_amt,'') as sas_purchase_amt  from ast_staff_asset left join hr_organization on org_gen_id=sas_org_id left join Ref_ast_kategori on ast_kategori_code=sas_asset_cat_cd  left join ref_location on loc_cd=sas_location_cd left join Ref_ast_sub_kategri_Aset on ast_subkateast_Code=sas_asset_sub_cat_cd left join ast_cmn_asset  on cas_asset_cd=sas_asset_cd and cas_asset_cat_cd=sas_asset_cat_cd and cas_asset_sub_cat_cd=sas_asset_sub_cat_cd and cas_asset_type_cd=sas_asset_type_cd left join Ref_ast_jenis_aset on sas_asset_type_cd=ast_jeniaset_Code where  sas_reg_dt>=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), 0) and sas_reg_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tddate + "'), +1) and sas_asset_cat_cd ='" + DD_Kategori.SelectedItem.Value + "' and sas_asset_sub_cat_cd ='" + DD_Sub.SelectedItem.Value + "'  and sas_asset_type_cd ='" + DropDownList1.SelectedItem.Value + "' and sas_asset_cd ='" + DropDownList2.SelectedItem.Value + "' ");
        }
        else if (txtdari.Text != "" && txtseh.Text != "" && DD_Kategori.SelectedValue != "" && DD_Sub.SelectedValue != "" && DropDownList1.SelectedValue != "" && DropDownList2.SelectedValue != "" && DropDownList5.SelectedValue != "" && DropDownList6.Text == "")
        {
            dt = DBCon.Ora_Execute_table("select sas_org_id,org_name,sas_location_cd,loc_desc,sas_asset_cat_cd,ast_kategori_desc,sas_asset_sub_cat_cd,ast_subkateast_desc,sas_asset_type_cd,ast_jeniaset_desc,sas_asset_cd,cas_asset_desc,ISNULL(sas_purchase_amt,'') as sas_purchase_amt  from ast_staff_asset left join hr_organization on org_gen_id=sas_org_id left join Ref_ast_kategori on ast_kategori_code=sas_asset_cat_cd  left join ref_location on loc_cd=sas_location_cd left join Ref_ast_sub_kategri_Aset on ast_subkateast_Code=sas_asset_sub_cat_cd left join ast_cmn_asset  on cas_asset_cd=sas_asset_cd and cas_asset_cat_cd=sas_asset_cat_cd and cas_asset_sub_cat_cd=sas_asset_sub_cat_cd and cas_asset_type_cd=sas_asset_type_cd left join Ref_ast_jenis_aset on sas_asset_type_cd=ast_jeniaset_Code where  sas_reg_dt>=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), 0) and sas_reg_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tddate + "'), +1) and sas_asset_cat_cd ='" + DD_Kategori.SelectedItem.Value + "' and sas_asset_sub_cat_cd ='" + DD_Sub.SelectedItem.Value + "'  and sas_asset_type_cd ='" + DropDownList1.SelectedItem.Value + "' and sas_asset_cd ='" + DropDownList2.SelectedItem.Value + "' and sas_org_id  ='" + DropDownList5.SelectedItem.Value + "'");
        }
        else if (txtdari.Text != "" && txtseh.Text != "" && DD_Kategori.SelectedValue != "" && DD_Sub.SelectedValue != "" && DropDownList1.SelectedValue != "" && DropDownList2.SelectedValue != "" && DropDownList5.SelectedValue != "" && DropDownList6.SelectedValue != "")
        {
            dt = DBCon.Ora_Execute_table("select sas_org_id,org_name,sas_location_cd,loc_desc,sas_asset_cat_cd,ast_kategori_desc,sas_asset_sub_cat_cd,ast_subkateast_desc,sas_asset_type_cd,ast_jeniaset_desc,sas_asset_cd,cas_asset_desc,ISNULL(sas_purchase_amt,'') as sas_purchase_amt  from ast_staff_asset left join hr_organization on org_gen_id=sas_org_id left join Ref_ast_kategori on ast_kategori_code=sas_asset_cat_cd  left join ref_location on loc_cd=sas_location_cd left join Ref_ast_sub_kategri_Aset on ast_subkateast_Code=sas_asset_sub_cat_cd left join ast_cmn_asset  on cas_asset_cd=sas_asset_cd and cas_asset_cat_cd=sas_asset_cat_cd and cas_asset_sub_cat_cd=sas_asset_sub_cat_cd and cas_asset_type_cd=sas_asset_type_cd left join Ref_ast_jenis_aset on sas_asset_type_cd=ast_jeniaset_Code where  sas_reg_dt>=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), 0) and sas_reg_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tddate + "'), +1) and sas_asset_cat_cd ='" + DD_Kategori.SelectedItem.Value + "' and sas_asset_sub_cat_cd ='" + DD_Sub.SelectedItem.Value + "'  and sas_asset_type_cd ='" + DropDownList1.SelectedItem.Value + "' and sas_asset_cd ='" + DropDownList2.SelectedItem.Value + "' and sas_org_id  ='" + DropDownList5.SelectedItem.Value + "' and sas_location_cd ='" + DropDownList6.SelectedItem.Value + "'");
        }
        else if (txtdari.Text != "" && txtseh.Text != "" && DD_Kategori.SelectedValue == "" && DD_Sub.SelectedValue == "" && DropDownList1.SelectedValue == "" && DropDownList2.SelectedValue == "" && DropDownList5.SelectedValue != "" && DropDownList6.SelectedValue == "")
        {
            dt = DBCon.Ora_Execute_table("select sas_org_id,org_name,sas_location_cd,loc_desc,sas_asset_cat_cd,ast_kategori_desc,sas_asset_sub_cat_cd,ast_subkateast_desc,sas_asset_type_cd,ast_jeniaset_desc,sas_asset_cd,cas_asset_desc,ISNULL(sas_purchase_amt,'') as sas_purchase_amt  from ast_staff_asset left join hr_organization on org_gen_id=sas_org_id left join Ref_ast_kategori on ast_kategori_code=sas_asset_cat_cd  left join ref_location on loc_cd=sas_location_cd left join Ref_ast_sub_kategri_Aset on ast_subkateast_Code=sas_asset_sub_cat_cd left join ast_cmn_asset  on cas_asset_cd=sas_asset_cd and cas_asset_cat_cd=sas_asset_cat_cd and cas_asset_sub_cat_cd=sas_asset_sub_cat_cd and cas_asset_type_cd=sas_asset_type_cd left join Ref_ast_jenis_aset on sas_asset_type_cd=ast_jeniaset_Code where  sas_reg_dt>=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), 0) and sas_reg_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tddate + "'), +1) and sas_org_id  ='" + DropDownList5.SelectedItem.Value + "'");
        }
        else if (txtdari.Text != "" && txtseh.Text != "" && DD_Kategori.SelectedValue == "" && DD_Sub.SelectedValue == "" && DropDownList1.SelectedValue == "" && DropDownList2.SelectedValue == "" && DropDownList5.SelectedValue != "" && DropDownList6.SelectedValue != "")
        {
            dt = DBCon.Ora_Execute_table("select sas_org_id,org_name,sas_location_cd,loc_desc,sas_asset_cat_cd,ast_kategori_desc,sas_asset_sub_cat_cd,ast_subkateast_desc,sas_asset_type_cd,ast_jeniaset_desc,sas_asset_cd,cas_asset_desc,ISNULL(sas_purchase_amt,'') as sas_purchase_amt  from ast_staff_asset left join hr_organization on org_gen_id=sas_org_id left join Ref_ast_kategori on ast_kategori_code=sas_asset_cat_cd  left join ref_location on loc_cd=sas_location_cd left join Ref_ast_sub_kategri_Aset on ast_subkateast_Code=sas_asset_sub_cat_cd left join ast_cmn_asset  on cas_asset_cd=sas_asset_cd and cas_asset_cat_cd=sas_asset_cat_cd and cas_asset_sub_cat_cd=sas_asset_sub_cat_cd and cas_asset_type_cd=sas_asset_type_cd left join Ref_ast_jenis_aset on sas_asset_type_cd=ast_jeniaset_Code where  sas_reg_dt>=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), 0) and sas_reg_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tddate + "'), +1) and sas_org_id  ='" + DropDownList5.SelectedItem.Value + "' and sas_location_cd ='" + DropDownList6.SelectedItem.Value + "'");
        }
        else if (txtdari.Text != "" && txtseh.Text != "" && DD_Kategori.SelectedValue != "" && DD_Sub.SelectedValue == "" && DropDownList1.SelectedValue == "" && DropDownList2.SelectedValue == "" && DropDownList5.SelectedValue != "" && DropDownList6.SelectedValue == "")
        {
            dt = DBCon.Ora_Execute_table("select sas_org_id,org_name,sas_location_cd,loc_desc,sas_asset_cat_cd,ast_kategori_desc,sas_asset_sub_cat_cd,ast_subkateast_desc,sas_asset_type_cd,ast_jeniaset_desc,sas_asset_cd,cas_asset_desc,ISNULL(sas_purchase_amt,'') as sas_purchase_amt  from ast_staff_asset left join hr_organization on org_gen_id=sas_org_id left join Ref_ast_kategori on ast_kategori_code=sas_asset_cat_cd  left join ref_location on loc_cd=sas_location_cd left join Ref_ast_sub_kategri_Aset on ast_subkateast_Code=sas_asset_sub_cat_cd left join ast_cmn_asset  on cas_asset_cd=sas_asset_cd and cas_asset_cat_cd=sas_asset_cat_cd and cas_asset_sub_cat_cd=sas_asset_sub_cat_cd and cas_asset_type_cd=sas_asset_type_cd left join Ref_ast_jenis_aset on sas_asset_type_cd=ast_jeniaset_Code where  sas_reg_dt>=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), 0) and sas_reg_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tddate + "'), +1) and sas_asset_cat_cd ='" + DD_Kategori.SelectedItem.Value + "' and sas_org_id  ='" + DropDownList5.SelectedItem.Value + "'");
        }
        else if (txtdari.Text != "" && txtseh.Text != "" && DD_Kategori.SelectedValue != "" && DD_Sub.SelectedValue == "" && DropDownList1.SelectedValue == "" && DropDownList2.SelectedValue == "" && DropDownList5.SelectedValue != "" && DropDownList6.SelectedValue != "")
        {
            dt = DBCon.Ora_Execute_table("select sas_org_id,org_name,sas_location_cd,loc_desc,sas_asset_cat_cd,ast_kategori_desc,sas_asset_sub_cat_cd,ast_subkateast_desc,sas_asset_type_cd,ast_jeniaset_desc,sas_asset_cd,cas_asset_desc,ISNULL(sas_purchase_amt,'') as sas_purchase_amt  from ast_staff_asset left join hr_organization on org_gen_id=sas_org_id left join Ref_ast_kategori on ast_kategori_code=sas_asset_cat_cd  left join ref_location on loc_cd=sas_location_cd left join Ref_ast_sub_kategri_Aset on ast_subkateast_Code=sas_asset_sub_cat_cd left join ast_cmn_asset  on cas_asset_cd=sas_asset_cd and cas_asset_cat_cd=sas_asset_cat_cd and cas_asset_sub_cat_cd=sas_asset_sub_cat_cd and cas_asset_type_cd=sas_asset_type_cd left join Ref_ast_jenis_aset on sas_asset_type_cd=ast_jeniaset_Code where  sas_reg_dt>=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), 0) and sas_reg_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tddate + "'), +1) and sas_asset_cat_cd ='" + DD_Kategori.SelectedItem.Value + "' and sas_org_id  ='" + DropDownList5.SelectedItem.Value + "' and sas_location_cd ='" + DropDownList6.SelectedItem.Value + "'");
        }
        else
        {
            dt = DBCon.Ora_Execute_table("select sas_org_id,org_name,sas_location_cd,loc_desc,sas_asset_cat_cd,ast_kategori_desc,sas_asset_sub_cat_cd,ast_subkateast_desc,sas_asset_type_cd,ast_jeniaset_desc,sas_asset_cd,cas_asset_desc,ISNULL(sas_purchase_amt,'') as sas_purchase_amt  from ast_staff_asset left join hr_organization on org_gen_id=sas_org_id left join Ref_ast_kategori on ast_kategori_code=sas_asset_cat_cd  left join ref_location on loc_cd=sas_location_cd left join Ref_ast_sub_kategri_Aset on ast_subkateast_Code=sas_asset_sub_cat_cd left join ast_cmn_asset  on cas_asset_cd=sas_asset_cd and cas_asset_cat_cd=sas_asset_cat_cd and cas_asset_sub_cat_cd=sas_asset_sub_cat_cd and cas_asset_type_cd=sas_asset_type_cd left join Ref_ast_jenis_aset on sas_asset_type_cd=ast_jeniaset_Code where  sas_location_cd =''");
        }



        ds.Tables.Add(dt);

        Rptviwer_lt.Reset();
        Rptviwer_lt.LocalReport.Refresh();

        List<DataRow> listResult = dt.AsEnumerable().ToList();
        listResult.Count();
        int countRow = 0;
        countRow = listResult.Count();

        if (countRow != 0)
        {
            disp_hdr_txt.Visible = true;
            //Display Report
            //Rptviwer_lt.LocalReport.DataSources.Clear();
            Rptviwer_lt.LocalReport.ReportPath = "Aset/ast_pen.rdlc";
            ReportDataSource rds = new ReportDataSource("aspen", dt);

            //Path

            string branch;
            //ToDate.Text = today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            string ss1 = string.Empty, ss2 = string.Empty, ss3 = string.Empty, ss4 = string.Empty, ss5 = string.Empty, ss6 = string.Empty;
            if (DD_Kategori.SelectedValue != "")
            {
                ss1 = DD_Kategori.SelectedItem.Text.ToUpper();
            }
            else
            {
                ss1 = "SEMUA";
            }

            if (DD_Sub.SelectedValue != "")
            {
                ss2 = DD_Sub.SelectedItem.Text.ToUpper();
            }
            else
            {
                ss2 = "SEMUA";
            }

            if (DropDownList1.SelectedValue != "")
            {
                ss3 = DropDownList1.SelectedItem.Text.ToUpper();
            }
            else
            {
                ss3 = "SEMUA";
            }

            if (DropDownList2.SelectedValue != "")
            {
                ss4 = DropDownList2.SelectedItem.Text.ToUpper();
            }
            else
            {
                ss4 = "SEMUA";
            }

            if (DropDownList5.SelectedValue != "")
            {
                ss5 = DropDownList5.SelectedItem.Text.ToUpper();
            }
            else
            {
                ss5 = "SEMUA";
            }

            if (DropDownList6.SelectedValue != "")
            {
                ss6 = DropDownList6.SelectedItem.Text.ToUpper();
            }
            else
            {
                ss6 = "SEMUA";
            }

            //Parameters
            ReportParameter[] rptParams = new ReportParameter[]{
                     //new ReportParameter("fromDate",FromDate .Text ),
                     //new ReportParameter("toDate",ToDate .Text )
                     new ReportParameter("s7",txtdari.Text  ),
                     new ReportParameter("s8",txtseh.Text ),
                          new ReportParameter("s1",ss1 ),
                            new ReportParameter("s2",ss2 ),
                     new ReportParameter("s3",ss3 ),
                      new ReportParameter("s4",ss4 ),
                       new ReportParameter("s5",ss5 ),
                        new ReportParameter("s6",ss6 )
                     };


            Rptviwer_lt.LocalReport.SetParameters(rptParams);
            Rptviwer_lt.LocalReport.DataSources.Add(rds);
            Rptviwer_lt.LocalReport.DisplayName = "LAPORAN_PENEMPATAN_ASET_" + DateTime.Now.ToString("ddMMyyyy");
            //ReportViewer1.LocalReport.ExecuteReportInCurrentAppDomain(AppDomain.CurrentDomain.Evidence);
            //Refresh
            Rptviwer_lt.LocalReport.Refresh();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod tidak dijumpai. Sila Pastikan Semua Maklumat Dimasukkan Dengan Betul.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }


    protected void Reset_btn(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_report_penembatan.aspx");
    }

 
    
}