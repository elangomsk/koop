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

public partial class Ast_avail_ast : System.Web.UI.Page
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
                subkat();
                grid1();
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
        grid1();
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

    //protected void OnSelectedIndexChanged2(object sender, EventArgs e)
    //{
    //    cmnaset();
    //    grid1();
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

    void subkat()
    {
        DataSet Ds = new DataSet();
        try
        {



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
        grid1();
        DropDownList1.SelectedValue = "";
    }

  
    void get_qry()
    {
        if (DD_Kategori.SelectedValue != "" || DD_Sub.SelectedValue != "" || DropDownList1.SelectedValue != "" || TextBox3.Text != "")
        {
            sel_clmflds();
            if (ss1 == "01")
            {
                //str_qry = "select com_asset_cat_cd,raka.ast_kategori_desc,com_asset_sub_cat_cd,rask.ast_subkateast_desc,com_asset_type_cd,raja.ast_jeniaset_desc,com_asset_cd,rak.cas_asset_desc, count(ISNULL(com_allocate_staff_no,'')) as strqty from ast_component aa left join ast_staff_asset asa on asa.sas_asset_cat_cd=aa.com_asset_cat_cd and asa.sas_asset_sub_cat_cd=aa.com_asset_sub_cat_cd and asa.sas_asset_type_cd=aa.com_asset_type_cd and asa.sas_asset_cd=aa.com_asset_cd left join Ref_ast_kategori raka on raka.ast_kategori_code=aa.com_asset_cat_cd  left join Ref_ast_sub_kategri_Aset rask on rask.ast_subkateast_Code=aa.com_asset_sub_cat_cd left join Ref_ast_jenis_aset raja on raja.ast_jeniaset_Code=aa.com_asset_type_cd left join ast_cmn_asset rak on rak.cas_asset_cd=aa.com_asset_cd and rak.cas_asset_cat_cd = aa.com_asset_cat_cd and rak.cas_asset_sub_cat_cd = aa.com_asset_sub_cat_cd and rak.cas_asset_type_cd = aa.com_asset_type_cd  where " + clmfd + " group by com_asset_cat_cd,raka.ast_kategori_desc,com_asset_sub_cat_cd,rask.ast_subkateast_desc,com_asset_type_cd,raja.ast_jeniaset_desc,com_asset_cd,rak.cas_asset_desc,asa.sas_qty";
                str_qry = "select com_asset_cat_cd,raka.ast_kategori_desc,com_asset_sub_cat_cd,rask.ast_subkateast_desc,com_asset_type_cd,raja.ast_jeniaset_desc,com_asset_cd,rak.cas_asset_desc,aa.strqty from (select com_asset_cat_cd,com_asset_sub_cat_cd,com_asset_type_cd,com_asset_cd,count(*) as strqty from ast_component where flag_set ='0' and ISNULL(com_allocate_staff_no,'') ='' and " + clmfd + " group by com_asset_cat_cd,com_asset_sub_cat_cd,com_asset_type_cd,com_asset_cd) as aa left join ast_staff_asset asa on asa.sas_asset_cat_cd=aa.com_asset_cat_cd and asa.sas_asset_sub_cat_cd=aa.com_asset_sub_cat_cd and asa.sas_asset_type_cd=aa.com_asset_type_cd and asa.sas_asset_cd=aa.com_asset_cd left join Ref_ast_kategori raka on raka.ast_kategori_code=aa.com_asset_cat_cd  left join Ref_ast_sub_kategri_Aset rask on rask.ast_subkateast_Code=aa.com_asset_sub_cat_cd left join Ref_ast_jenis_aset raja on raja.ast_jeniaset_Code=aa.com_asset_type_cd and raja.ast_sub_cat_Code=aa.com_asset_sub_cat_cd and raja.ast_cat_Code=aa.com_asset_cat_cd left join ast_cmn_asset rak on rak.cas_asset_cd=aa.com_asset_cd and rak.cas_asset_cat_cd = aa.com_asset_cat_cd and rak.cas_asset_sub_cat_cd = aa.com_asset_sub_cat_cd and rak.cas_asset_type_cd = aa.com_asset_type_cd  group by com_asset_cat_cd,raka.ast_kategori_desc,com_asset_sub_cat_cd,rask.ast_subkateast_desc,com_asset_type_cd,raja.ast_jeniaset_desc,com_asset_cd,rak.cas_asset_desc,aa.strqty";
            }
            else if (ss1 == "02")
            {
                //str_qry = "select car_asset_cat_cd,raka.ast_kategori_desc,car_asset_sub_cat_cd,rask.ast_subkateast_desc,car_asset_type_cd,raja.ast_jeniaset_desc,car_asset_cd,rak.cas_asset_desc, count(ISNULL(car_allocate_staff_no,'')) as strqty from ast_car aa left join ast_staff_asset asa on asa.sas_asset_cat_cd=aa.car_asset_cat_cd and asa.sas_asset_sub_cat_cd=aa.car_asset_sub_cat_cd and asa.sas_asset_type_cd=aa.car_asset_type_cd and asa.sas_asset_cd=aa.car_asset_cd left join Ref_ast_kategori raka on raka.ast_kategori_code=aa.car_asset_cat_cd  left join Ref_ast_sub_kategri_Aset rask on rask.ast_subkateast_Code=aa.car_asset_sub_cat_cd left join Ref_ast_jenis_aset raja on raja.ast_jeniaset_Code=aa.car_asset_type_cd left join ast_cmn_asset rak on rak.cas_asset_cd=aa.car_asset_cd and rak.cas_asset_cat_cd = aa.car_asset_cat_cd and rak.cas_asset_sub_cat_cd = aa.car_asset_sub_cat_cd and rak.cas_asset_type_cd = aa.car_asset_type_cd where " + clmfd + " group by car_asset_cat_cd,raka.ast_kategori_desc,car_asset_sub_cat_cd,rask.ast_subkateast_desc,car_asset_type_cd,raja.ast_jeniaset_desc,car_asset_cd,rak.cas_asset_desc,asa.sas_qty";
                str_qry = "select car_asset_cat_cd,raka.ast_kategori_desc,car_asset_sub_cat_cd,rask.ast_subkateast_desc,car_asset_type_cd,raja.ast_jeniaset_desc,car_asset_cd,rak.cas_asset_desc, count(*) as strqty from ast_car aa left join ast_staff_asset asa on asa.sas_asset_cat_cd=aa.car_asset_cat_cd and asa.sas_asset_sub_cat_cd=aa.car_asset_sub_cat_cd and asa.sas_asset_type_cd=aa.car_asset_type_cd and asa.sas_asset_cd=aa.car_asset_cd left join Ref_ast_kategori raka on raka.ast_kategori_code=aa.car_asset_cat_cd  left join Ref_ast_sub_kategri_Aset rask on rask.ast_subkateast_Code=aa.car_asset_sub_cat_cd left join Ref_ast_jenis_aset raja on raja.ast_jeniaset_Code=aa.car_asset_type_cd and raja.ast_sub_cat_Code=aa.car_asset_sub_cat_cd and raja.ast_cat_Code=aa.car_asset_cat_cd left join ast_cmn_asset rak on rak.cas_asset_cd=aa.car_asset_cd and rak.cas_asset_cat_cd = aa.car_asset_cat_cd and rak.cas_asset_sub_cat_cd = aa.car_asset_sub_cat_cd and rak.cas_asset_type_cd = aa.car_asset_type_cd where aa.flag_set ='0' and ISNULL(car_allocate_staff_no,'') ='' and " + clmfd + " group by car_asset_cat_cd,raka.ast_kategori_desc,car_asset_sub_cat_cd,rask.ast_subkateast_desc,car_asset_type_cd,raja.ast_jeniaset_desc,car_asset_cd,rak.cas_asset_desc,asa.sas_qty";
            }
            else if (ss1 == "03")
            {
                //str_qry = "select inv_asset_cat_cd,raka.ast_kategori_desc,inv_asset_sub_cat_cd,rask.ast_subkateast_desc,inv_asset_type_cd,raja.ast_jeniaset_desc,inv_asset_cd,rak.cas_asset_desc ,sum(inv_qty) as strqty from ast_inventory aa left join Ref_ast_kategori raka on raka.ast_kategori_code=aa.inv_asset_cat_cd  left join Ref_ast_sub_kategri_Aset rask on rask.ast_subkateast_Code=aa.inv_asset_sub_cat_cd left join Ref_ast_jenis_aset raja on raja.ast_jeniaset_Code=aa.inv_asset_type_cd left join ast_cmn_asset rak on rak.cas_asset_cd=aa.inv_asset_cd and rak.cas_asset_cat_cd = aa.inv_asset_cat_cd and rak.cas_asset_sub_cat_cd = aa.inv_asset_sub_cat_cd and rak.cas_asset_type_cd = aa.inv_asset_type_cd where " + clmfd + " group by inv_asset_cat_cd,raka.ast_kategori_desc,inv_asset_sub_cat_cd,rask.ast_subkateast_desc,inv_asset_type_cd,raja.ast_jeniaset_desc,inv_asset_cd,rak.cas_asset_desc";
                str_qry = "select inv_asset_cat_cd,raka.ast_kategori_desc,inv_asset_sub_cat_cd,rask.ast_subkateast_desc,inv_asset_type_cd,raja.ast_jeniaset_desc,inv_asset_cd,rak.cas_asset_desc ,sum(inv_qty) as strqty from ast_inventory aa left join Ref_ast_kategori raka on raka.ast_kategori_code=aa.inv_asset_cat_cd  left join Ref_ast_sub_kategri_Aset rask on rask.ast_subkateast_Code=aa.inv_asset_sub_cat_cd left join Ref_ast_jenis_aset raja on raja.ast_jeniaset_Code=aa.inv_asset_type_cd and raja.ast_sub_cat_Code=aa.inv_asset_sub_cat_cd and raja.ast_cat_Code=aa.inv_asset_cat_cd left join ast_cmn_asset rak on rak.cas_asset_cd=aa.inv_asset_cd and rak.cas_asset_cat_cd = aa.inv_asset_cat_cd and rak.cas_asset_sub_cat_cd = aa.inv_asset_sub_cat_cd and rak.cas_asset_type_cd = aa.inv_asset_type_cd where aa.flag_set ='0' and " + clmfd + " group by inv_asset_cat_cd,raka.ast_kategori_desc,inv_asset_sub_cat_cd,rask.ast_subkateast_desc,inv_asset_type_cd,raja.ast_jeniaset_desc,inv_asset_cd,rak.cas_asset_desc";

            }
            else if (ss1 == "04")
            {
                //str_qry = "select pro_asset_cat_cd,raka.ast_kategori_desc,pro_asset_sub_cat_cd,rask.ast_subkateast_desc,pro_asset_type_cd,raja.ast_jeniaset_desc,pro_asset_cd,rak.cas_asset_desc,  case when ISNULL(asa.sas_qty,'') = '' then '0' else asa.sas_qty end as strqty from ast_property aa left join ast_staff_asset asa on asa.sas_asset_cat_cd=aa.pro_asset_cat_cd and asa.sas_asset_sub_cat_cd=aa.pro_asset_sub_cat_cd and asa.sas_asset_type_cd=aa.pro_asset_type_cd and asa.sas_asset_cd=aa.pro_asset_cd left join Ref_ast_kategori raka on raka.ast_kategori_code=aa.pro_asset_cat_cd  left join Ref_ast_sub_kategri_Aset rask on rask.ast_subkateast_Code=aa.pro_asset_sub_cat_cd left join Ref_ast_jenis_aset raja on raja.ast_jeniaset_Code=aa.pro_asset_type_cd left join ast_cmn_asset rak on rak.cas_asset_cd=aa.pro_asset_cd and rak.cas_asset_cat_cd = aa.pro_asset_cat_cd and rak.cas_asset_sub_cat_cd = aa.pro_asset_sub_cat_cd and rak.cas_asset_type_cd = aa.pro_asset_type_cd where " + clmfd + " group by pro_asset_cat_cd,raka.ast_kategori_desc,pro_asset_sub_cat_cd,rask.ast_subkateast_desc,pro_asset_type_cd,raja.ast_jeniaset_desc,pro_asset_cd,rak.cas_asset_desc,asa.sas_qty";
                str_qry = "select pro_asset_cat_cd,raka.ast_kategori_desc,pro_asset_sub_cat_cd,rask.ast_subkateast_desc,pro_asset_type_cd,raja.ast_jeniaset_desc,pro_asset_cd,rak.cas_asset_desc,  count(*) as strqty from ast_property aa left join ast_staff_asset asa on asa.sas_asset_cat_cd=aa.pro_asset_cat_cd and asa.sas_asset_sub_cat_cd=aa.pro_asset_sub_cat_cd and asa.sas_asset_type_cd=aa.pro_asset_type_cd and asa.sas_asset_cd=aa.pro_asset_cd left join Ref_ast_kategori raka on raka.ast_kategori_code=aa.pro_asset_cat_cd  left join Ref_ast_sub_kategri_Aset rask on rask.ast_subkateast_Code=aa.pro_asset_sub_cat_cd left join Ref_ast_jenis_aset raja on raja.ast_jeniaset_Code=aa.pro_asset_type_cd and raja.ast_sub_cat_Code=aa.pro_asset_sub_cat_cd and raja.ast_cat_Code=aa.pro_asset_cat_cd left join ast_cmn_asset rak on rak.cas_asset_cd=aa.pro_asset_cd and rak.cas_asset_cat_cd = aa.pro_asset_cat_cd and rak.cas_asset_sub_cat_cd = aa.pro_asset_sub_cat_cd and rak.cas_asset_type_cd = aa.pro_asset_type_cd  where aa.flag_set ='0' and ISNULL(pro_allocate_staff_no,'') ='' and " + clmfd + " group by pro_asset_cat_cd,raka.ast_kategori_desc,pro_asset_sub_cat_cd,rask.ast_subkateast_desc,pro_asset_type_cd,raja.ast_jeniaset_desc,pro_asset_cd,rak.cas_asset_desc,asa.sas_qty";
            }
            else
            {
                str_qry = "select com_asset_cat_cd,raka.ast_kategori_desc,com_asset_sub_cat_cd,rask.ast_subkateast_desc,com_asset_type_cd,raja.ast_jeniaset_desc,com_asset_cd,rak.cas_asset_desc, count(*) as strqty from ast_component aa left join ast_staff_asset asa on asa.sas_asset_cat_cd=aa.com_asset_cat_cd and asa.sas_asset_sub_cat_cd=aa.com_asset_sub_cat_cd and asa.sas_asset_type_cd=aa.com_asset_type_cd and asa.sas_asset_cd=aa.com_asset_cd left join Ref_ast_kategori raka on raka.ast_kategori_code=aa.com_asset_cat_cd  left join Ref_ast_sub_kategri_Aset rask on rask.ast_subkateast_Code=aa.com_asset_sub_cat_cd left join Ref_ast_jenis_aset raja on raja.ast_jeniaset_Code=aa.com_asset_type_cd and raja.ast_sub_cat_Code=aa.com_asset_sub_cat_cd and raja.ast_cat_Code=aa.com_asset_cat_cd left join ast_cmn_asset rak on rak.cas_asset_cd=aa.com_asset_cd and rak.cas_asset_cat_cd = aa.com_asset_cat_cd and rak.cas_asset_sub_cat_cd = aa.com_asset_sub_cat_cd and rak.cas_asset_type_cd = aa.com_asset_type_cd where com_asset_cd='' group by com_asset_cat_cd,raka.ast_kategori_desc,com_asset_sub_cat_cd,rask.ast_subkateast_desc,com_asset_type_cd,raja.ast_jeniaset_desc,com_asset_cd,rak.cas_asset_desc,asa.sas_qty";
            }
        }
        else
        {
            str_qry = "select com_asset_cat_cd,raka.ast_kategori_desc,com_asset_sub_cat_cd,rask.ast_subkateast_desc,com_asset_type_cd,raja.ast_jeniaset_desc,com_asset_cd,rak.cas_asset_desc, count(*) as strqty from ast_component aa left join ast_staff_asset asa on asa.sas_asset_cat_cd=aa.com_asset_cat_cd and asa.sas_asset_sub_cat_cd=aa.com_asset_sub_cat_cd and asa.sas_asset_type_cd=aa.com_asset_type_cd and asa.sas_asset_cd=aa.com_asset_cd left join Ref_ast_kategori raka on raka.ast_kategori_code=aa.com_asset_cat_cd  left join Ref_ast_sub_kategri_Aset rask on rask.ast_subkateast_Code=aa.com_asset_sub_cat_cd left join Ref_ast_jenis_aset raja on raja.ast_jeniaset_Code=aa.com_asset_type_cd and raja.ast_sub_cat_Code=aa.com_asset_sub_cat_cd and raja.ast_cat_Code=aa.com_asset_cat_cd left join ast_cmn_asset rak on rak.cas_asset_cd=aa.com_asset_cd and rak.cas_asset_cat_cd = aa.com_asset_cat_cd and rak.cas_asset_sub_cat_cd = aa.com_asset_sub_cat_cd and rak.cas_asset_type_cd = aa.com_asset_type_cd  where com_asset_cd='' group by com_asset_cat_cd,raka.ast_kategori_desc,com_asset_sub_cat_cd,rask.ast_subkateast_desc,com_asset_type_cd,raja.ast_jeniaset_desc,com_asset_cd,rak.cas_asset_desc,asa.sas_qty";
        }
    }

    void sel_clmflds()
    {

        if (DD_Kategori.SelectedValue == "" && DD_Sub.SelectedValue == "" && DropDownList1.SelectedValue == "" && TextBox3.Text != "")
        {
            ss1 = TextBox3.Text.Substring(0, 2);
        }
        else
        {
            if (DD_Kategori.SelectedValue == "01")
            {
                ss1 = DD_Kategori.SelectedValue;
            }
            else if (DD_Kategori.SelectedValue == "02")
            {
                ss1 = DD_Kategori.SelectedValue;
            }
            else if (DD_Kategori.SelectedValue == "03")
            {
                ss1 = DD_Kategori.SelectedValue;
            }
            else if (DD_Kategori.SelectedValue == "04")
            {
                ss1 = DD_Kategori.SelectedValue;
            }
            else
            {
                ss1 = "01";
            }
        }
        if (ss1 == "01")
        {
            clm_name = "com";
        }
        else if (ss1 == "02")
        {
            clm_name = "car";
        }
        else if (ss1 == "03")
        {
            clm_name = "inv";
        }
        else if (ss1 == "04")
        {
            clm_name = "pro";
        }
        else
        {
            clm_name = "com";
        }

        if (DD_Kategori.SelectedValue != "" && DD_Sub.SelectedValue != "" && DropDownList1.SelectedValue != "" && TextBox3.Text != "")
        {
            clmfd = "" + clm_name + "_asset_cat_cd='" + DD_Kategori.SelectedValue + "' and " + clm_name + "_asset_sub_cat_cd='" + DD_Sub.SelectedValue + "' and " + clm_name + "_asset_type_cd='" + DropDownList1.SelectedValue + "' and " + clm_name + "_asset_id='" + TextBox3.Text + "'";
        }
        else if (DD_Kategori.SelectedValue != "" && DD_Sub.SelectedValue == "" && DropDownList1.SelectedValue == "" && TextBox3.Text == "")
        {
            clmfd = "" + clm_name + "_asset_cat_cd='" + DD_Kategori.SelectedValue + "'";
        }
        else if (DD_Kategori.SelectedValue != "" && DD_Sub.SelectedValue != "" && DropDownList1.SelectedValue == "" && TextBox3.Text == "")
        {
            clmfd = "" + clm_name + "_asset_cat_cd='" + DD_Kategori.SelectedValue + "' and " + clm_name + "_asset_sub_cat_cd='" + DD_Sub.SelectedValue + "'";
        }
        else if (DD_Kategori.SelectedValue != "" && DD_Sub.SelectedValue != "" && DropDownList1.SelectedValue != "" && TextBox3.Text == "")
        {
            clmfd = "" + clm_name + "_asset_cat_cd='" + DD_Kategori.SelectedValue + "' and " + clm_name + "_asset_sub_cat_cd='" + DD_Sub.SelectedValue + "' and " + clm_name + "_asset_type_cd='" + DropDownList1.SelectedValue + "'";
        }
        else if (DD_Kategori.SelectedValue == "" && DD_Sub.SelectedValue == "" && DropDownList1.SelectedValue == "" && TextBox3.Text != "")
        {
            clmfd = "" + clm_name + "_asset_id='" + TextBox3.Text + "'";
        }
        else
        {
            clmfd = "" + clm_name + "_asset_cat_cd=''";
        }

    }

    protected void gv_refdata_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSelected.PageIndex = e.NewPageIndex;
        grid1();
    }

    protected void Btn_Carian_Click(object sender, EventArgs e)
    {
        if (DD_Kategori.SelectedValue != "" || DD_Sub.SelectedValue != "" || DropDownList1.SelectedValue != "" || TextBox3.Text != "")
        {
            grid1();
        }
        else
        {
            grid1();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    void grid1()
    {
        get_qry();
        SqlCommand cmd2 = new SqlCommand("" + str_qry + "", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            gvSelected.DataSource = ds2;
            gvSelected.DataBind();
            int columncount = gvSelected.Rows[0].Cells.Count;
            gvSelected.Rows[0].Cells.Clear();
            gvSelected.Rows[0].Cells.Add(new TableCell());
            gvSelected.Rows[0].Cells[0].ColumnSpan = columncount;
            gvSelected.Rows[0].Cells[0].Text = "<strong><center>Maklumat Carian Tidak Dijumpai</center></strong>";
        }
        else
        {
            gvSelected.DataSource = ds2;
            gvSelected.DataBind();
        }
    }

    protected void Reset_btn(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_avail_ast.aspx");
    }

 
    
}