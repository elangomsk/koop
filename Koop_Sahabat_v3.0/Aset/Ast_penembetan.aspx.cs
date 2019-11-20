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

public partial class Ast_penembetan : System.Web.UI.Page
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
    string varName1 = string.Empty;
    string sqry = string.Empty;

    string CommandArgument1 = string.Empty, CommandArgument2 = string.Empty, CommandArgument3 = string.Empty, CommandArgument4 = string.Empty, CommandArgument5 = string.Empty, CommandArgument6 = string.Empty, CommandArgument7 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2()})";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Button2);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;
                kat();
                subkat();
                //kod();
                //jaset();

                sname();
                dd_qry();
                TextBox1.Text = DateTime.Now.ToString("dd/MM/yyyy");
                TextBox4.Text = "";
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

            string com = "select ast_kategori_code,UPPER(ast_kategori_desc) as ast_kategori_desc from Ref_ast_kategori";
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

    void subkat()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select loc_cd,UPPER(loc_desc) as loc_desc from ref_location";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList4.DataSource = dt;
            DropDownList4.DataBind();
            DropDownList4.DataTextField = "loc_desc";
            DropDownList4.DataValueField = "loc_cd";
            DropDownList4.DataBind();
            DropDownList4.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    void sname()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select stf_staff_no,UPPER(stf_name) as stf_name from hr_staff_profile";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList3.DataSource = dt;
            DropDownList3.DataBind();
            DropDownList3.DataTextField = "stf_name";
            DropDownList3.DataValueField = "stf_staff_no";
            DropDownList3.DataBind();
            DropDownList3.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void OnSelectedIndexChanged1(object sender, EventArgs e)
    {
        jenaset();
        dd_qry();

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
    //    dd_qry();
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

    //void kod()
    //{
    //    DataSet Ds = new DataSet();
    //    try
    //    {

    //        string com = "select ast_kodast_Code,UPPER(ast_kodast_desc) as ast_kodast_desc from Ref_ast_kod_aset";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
    //        DataTable dt = new DataTable();
    //        adpt.Fill(dt);
    //        DropDownList2.DataSource = dt;
    //        DropDownList2.DataBind();
    //        DropDownList2.DataTextField = "ast_kodast_desc";
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

            string com = "select ast_jeniaset_Code,UPPER(ast_jeniaset_desc) as ast_jeniaset_desc from Ref_ast_jenis_aset";
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

        if (DD_Kategori.SelectedValue == "01")
        {
            TextBox2.Attributes.Add("Readonly", "Readonly");
            TextBox2.Text = "1";
        }
        else if (DD_Kategori.SelectedValue == "02")
        {
            TextBox2.Attributes.Add("Readonly", "Readonly");
            TextBox2.Text = "1";
        }
        else if (DD_Kategori.SelectedValue == "03")
        {
            TextBox2.Attributes.Remove("Readonly");
            TextBox2.Text = "";
        }
        else if (DD_Kategori.SelectedValue == "04")
        {
            TextBox2.Attributes.Add("Readonly", "Readonly");
            TextBox2.Text = "1";
        }
        else
        {
            TextBox2.Attributes.Add("Readonly", "Readonly");
            TextBox2.Text = "0";
        }
        string com = "select ast_subkateast_Code,UPPER(ast_subkateast_desc) as ast_subkateast_desc from Ref_ast_sub_kategri_Aset where ast_kategori_code='" + DD_Kategori.SelectedValue + "'";
        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        DD_Sub.DataSource = dt;
        DD_Sub.DataBind();
        DD_Sub.DataTextField = "ast_subkateast_desc";
        DD_Sub.DataValueField = "ast_subkateast_Code";
        DD_Sub.DataBind();
        DD_Sub.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        dd_qry();
        DropDownList1.SelectedValue = "";

    }

    protected void gv_refdata_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        gvSelected.PageIndex = e.NewPageIndex;
        dd_qry();
    }

    protected void gv_refdata_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        dd_qry();
    }

    void bgrid2()
    {
        SqlCommand cmd2 = new SqlCommand("select raka.ast_kategori_desc,rask.ast_subkateast_desc,raja.ast_jeniaset_desc,rak.cas_asset_desc,aa.sas_asset_id,aa.sas_qty  from ast_staff_asset aa left join Ref_ast_kategori raka on raka.ast_kategori_code=aa.sas_asset_cat_cd  left join Ref_ast_sub_kategri_Aset rask on rask.ast_subkateast_Code=aa.sas_asset_sub_cat_cd left join Ref_ast_jenis_aset raja on raja.ast_jeniaset_Code=aa.sas_asset_type_cd and raja.ast_sub_cat_Code=aa.sas_asset_sub_cat_cd left join ast_cmn_asset rak on rak.cas_asset_cd=aa.sas_asset_cd and rak.cas_asset_cat_cd=aa.sas_asset_cat_cd and rak.cas_asset_sub_cat_cd=aa.sas_asset_sub_cat_cd and rak.cas_asset_type_cd=aa.sas_asset_type_cd where flag_set='0' and sas_staff_no='" + DropDownList3.SelectedValue + "'", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            GridView1.DataSource = ds2;
            GridView1.DataBind();
            int columncount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView1.Rows[0].Cells[0].Text = "<strong><center>Maklumat Carian Tidak Dijumpai</center></strong>";
        }
        else
        {
            GridView1.DataSource = ds2;
            GridView1.DataBind();
            Button2.Attributes.Remove("style");
        }
    }

    protected void Btn_Senarai_Click(object sender, EventArgs e)
    {
        if (DD_Kategori.SelectedValue != "" || DD_Sub.SelectedValue != "" || DropDownList1.SelectedValue != "")
        {
            dd_qry();
        }
        else
        {
            dd_qry();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }

    }

    protected void sel_namapag(object sender, EventArgs e)
    {
        DataTable ddokdicno1 = new DataTable();
        ddokdicno1 = DBCon.Ora_Execute_table("select rj.hr_jaw_desc,sp.stf_staff_no from hr_staff_profile sp left join Ref_hr_Jawatan rj on rj.hr_jaw_Code=sp.stf_curr_post_cd where stf_staff_no='" + DropDownList3.SelectedValue + "'");

        if (ddokdicno1.Rows.Count != 0)
        {
            TextBox3.Text = ddokdicno1.Rows[0]["hr_jaw_desc"].ToString();
        }
        else
        {
            TextBox3.Text = "";
        }

        string rcount = string.Empty;
        int count = 0;
        foreach (GridViewRow gvrow in gvSelected.Rows)
        {
            count++;
        }
        rcount = count.ToString();
        if (rcount == "1")
        {
            foreach (GridViewRow gvrow in gvSelected.Rows)
            {
                string kat = ((Label)gvrow.FindControl("Label1")).Text.ToString();
                if (kat == "")
                {
                    dd_qry();
                }
            }
        }
        bgrid2();
    }

  
    void dd_qry()
    {


        sel_clmflds();
        if (DD_Kategori.SelectedValue == "01")
        {
            sqry = "select com_asset_cat_cd,raka.ast_kategori_desc,com_asset_sub_cat_cd,rask.ast_subkateast_desc,com_asset_type_cd,raja.ast_jeniaset_desc,com_asset_id as aid,rak.cas_asset_desc,'1' as qty,'1' as bqty  from ast_component aa left join Ref_ast_kategori raka on raka.ast_kategori_code=aa.com_asset_cat_cd  left join Ref_ast_sub_kategri_Aset rask on rask.ast_subkateast_Code=aa.com_asset_sub_cat_cd left join Ref_ast_jenis_aset raja on raja.ast_jeniaset_Code=aa.com_asset_type_cd and raja.ast_sub_cat_Code=aa.com_asset_sub_cat_cd left join ast_cmn_asset rak on rak.cas_asset_cd=aa.com_asset_cd and rak.cas_asset_cat_cd=aa.com_asset_cat_cd and rak.cas_asset_sub_cat_cd=aa.com_asset_sub_cat_cd and rak.cas_asset_type_cd=aa.com_asset_type_cd where com_sas_id = '0' and " + clmfd + "";

        }
        else if (DD_Kategori.SelectedValue == "02")
        {
            sqry = "select car_asset_cat_cd,raka.ast_kategori_desc,car_asset_sub_cat_cd,rask.ast_subkateast_desc,car_asset_type_cd,raja.ast_jeniaset_desc,car_asset_id as aid,rak.cas_asset_desc,'1' as qty,'1' as bqty  from ast_car aa left join Ref_ast_kategori raka on raka.ast_kategori_code=aa.car_asset_cat_cd  left join Ref_ast_sub_kategri_Aset rask on rask.ast_subkateast_Code=aa.car_asset_sub_cat_cd left join Ref_ast_jenis_aset raja on raja.ast_jeniaset_Code=aa.car_asset_type_cd and raja.ast_sub_cat_Code=aa.car_asset_sub_cat_cd left join ast_cmn_asset rak on rak.cas_asset_cd=aa.car_asset_cd and rak.cas_asset_cat_cd=aa.car_asset_cat_cd and rak.cas_asset_sub_cat_cd=aa.car_asset_sub_cat_cd and rak.cas_asset_type_cd=aa.car_asset_type_cd  where car_sas_id = '0' and " + clmfd + "";

        }
        else if (DD_Kategori.SelectedValue == "03")
        {
            //sqry = "select inv_asset_cat_cd,raka.ast_kategori_desc,inv_asset_sub_cat_cd,rask.ast_subkateast_desc,inv_asset_type_cd,raja.ast_jeniaset_desc,inv_asset_id as aid,rak.cas_asset_desc,inv_qty as qty  from ast_inventory aa left join Ref_ast_kategori raka on raka.ast_kategori_code=aa.inv_asset_cat_cd  left join Ref_ast_sub_kategri_Aset rask on rask.ast_subkateast_Code=aa.inv_asset_sub_cat_cd left join Ref_ast_jenis_aset raja on raja.ast_jeniaset_Code=aa.inv_asset_type_cd and raja.ast_sub_cat_Code=aa.inv_asset_sub_cat_cd left join ast_cmn_asset rak on rak.cas_asset_cd=aa.inv_asset_cd and rak.cas_asset_cat_cd=aa.inv_asset_cat_cd and rak.cas_asset_sub_cat_cd=aa.inv_asset_sub_cat_cd and rak.cas_asset_type_cd=aa.inv_asset_type_cd  where inv_sas_id = '0' and " + clmfd + "";
            sqry = "select *,(ISNULL(a.qty,'0') - ISNULL(b.cty,'0')) as bqty from (select inv_asset_cat_cd,raka.ast_kategori_desc,inv_asset_sub_cat_cd,rask.ast_subkateast_desc,inv_asset_type_cd,raja.ast_jeniaset_desc,inv_asset_id as aid,rak.cas_asset_desc,inv_qty as qty  from ast_inventory aa left join Ref_ast_kategori raka on raka.ast_kategori_code=aa.inv_asset_cat_cd  left join Ref_ast_sub_kategri_Aset rask on rask.ast_subkateast_Code=aa.inv_asset_sub_cat_cd left join Ref_ast_jenis_aset raja on raja.ast_jeniaset_Code=aa.inv_asset_type_cd and raja.ast_sub_cat_Code=aa.inv_asset_sub_cat_cd left join ast_cmn_asset rak on rak.cas_asset_cd=aa.inv_asset_cd and rak.cas_asset_cat_cd=aa.inv_asset_cat_cd and rak.cas_asset_sub_cat_cd=aa.inv_asset_sub_cat_cd and rak.cas_asset_type_cd=aa.inv_asset_type_cd  where inv_sas_id = '0' and " + clmfd + ") as a left join(select ISNULL(sas_asset_id,'') sas_asset_id,ISNULL(sum(sas_qty),'') as cty from ast_staff_asset group by sas_asset_id) as b on b.sas_asset_id=a.aid";

        }
        else if (DD_Kategori.SelectedValue == "04")
        {
            sqry = "select pro_asset_cat_cd,raka.ast_kategori_desc,pro_asset_sub_cat_cd,rask.ast_subkateast_desc,pro_asset_type_cd,raja.ast_jeniaset_desc,pro_asset_id as aid,rak.cas_asset_desc,'1' as qty,'1' as bqty  from ast_property aa left join Ref_ast_kategori raka on raka.ast_kategori_code=aa.pro_asset_cat_cd  left join Ref_ast_sub_kategri_Aset rask on rask.ast_subkateast_Code=aa.pro_asset_sub_cat_cd left join Ref_ast_jenis_aset raja on raja.ast_jeniaset_Code=aa.pro_asset_type_cd and raja.ast_sub_cat_Code=aa.pro_asset_sub_cat_cd left join ast_cmn_asset rak on rak.cas_asset_cd=aa.pro_asset_cd and rak.cas_asset_cat_cd=aa.pro_asset_cat_cd and rak.cas_asset_sub_cat_cd=aa.pro_asset_sub_cat_cd and rak.cas_asset_type_cd=aa.pro_asset_type_cd  where pro_sas_id = '0' and " + clmfd + "";

        }
        else
        {
            sqry = "select com_asset_cat_cd,raka.ast_kategori_desc,com_asset_sub_cat_cd,rask.ast_subkateast_desc,com_asset_type_cd,raja.ast_jeniaset_desc,com_asset_id as aid,rak.cas_asset_desc,'' as qty,'1' as bqty  from ast_component aa left join Ref_ast_kategori raka on raka.ast_kategori_code=aa.com_asset_cat_cd  left join Ref_ast_sub_kategri_Aset rask on rask.ast_subkateast_Code=aa.com_asset_sub_cat_cd left join Ref_ast_jenis_aset raja on raja.ast_jeniaset_Code=aa.com_asset_type_cd and raja.ast_sub_cat_Code=aa.com_asset_sub_cat_cd left join ast_cmn_asset rak on rak.cas_asset_cd=aa.com_asset_cd and rak.cas_asset_cat_cd=aa.com_asset_cat_cd and rak.cas_asset_sub_cat_cd=aa.com_asset_sub_cat_cd and rak.cas_asset_type_cd=aa.com_asset_type_cd  where com_asset_cat_cd='' and com_asset_sub_cat_cd='' and com_asset_type_cd='' and com_asset_cd=''";

        }

        SqlCommand cmd2 = new SqlCommand("" + sqry + "", con);
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
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Maklumat Carian Tidak Dijumpai.');", true);
        }
        else
        {
            gvSelected.DataSource = ds2;
            gvSelected.DataBind();
        }
        bgrid2();
    }

    void sel_clmflds()
    {
        if (DD_Kategori.SelectedValue == "01")
        {
            clm_name = "com";
        }
        else if (DD_Kategori.SelectedValue == "02")
        {
            clm_name = "car";
        }
        else if (DD_Kategori.SelectedValue == "03")
        {
            clm_name = "inv";
        }
        else if (DD_Kategori.SelectedValue == "04")
        {
            clm_name = "pro";
        }
        else
        {
            clm_name = "com";
        }

        if (DD_Kategori.SelectedValue != "" && DD_Sub.SelectedValue != "" && DropDownList1.SelectedValue != "")
        {
            clmfd = "" + clm_name + "_asset_cat_cd='" + DD_Kategori.SelectedValue + "' and " + clm_name + "_asset_sub_cat_cd='" + DD_Sub.SelectedValue + "' and " + clm_name + "_asset_type_cd='" + DropDownList1.SelectedValue + "'";
        }
        else if (DD_Kategori.SelectedValue != "" && DD_Sub.SelectedValue == "" && DropDownList1.SelectedValue == "")
        {
            clmfd = "" + clm_name + "_asset_cat_cd='" + DD_Kategori.SelectedValue + "'";
        }
        else if (DD_Kategori.SelectedValue != "" && DD_Sub.SelectedValue != "" && DropDownList1.SelectedValue == "")
        {
            clmfd = "" + clm_name + "_asset_cat_cd='" + DD_Kategori.SelectedValue + "' and " + clm_name + "_asset_sub_cat_cd='" + DD_Sub.SelectedValue + "'";
        }
        else if (DD_Kategori.SelectedValue != "" && DD_Sub.SelectedValue != "" && DropDownList1.SelectedValue != "")
        {
            clmfd = "" + clm_name + "_asset_cat_cd='" + DD_Kategori.SelectedValue + "' and " + clm_name + "_asset_sub_cat_cd='" + DD_Sub.SelectedValue + "' and " + clm_name + "_asset_type_cd='" + DropDownList1.SelectedValue + "'";
        }
        else
        {
            clmfd = "" + clm_name + "_asset_cat_cd=''";
        }

    }


    protected void Button5_Click(object sender, EventArgs e)
    {
        if (DD_Kategori.SelectedValue != "" || DD_Sub.SelectedValue != "" || DropDownList1.SelectedValue != "")
        {

            string rcount = string.Empty;
            int count = 0;
            foreach (GridViewRow gvrow in gvSelected.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("CheckBox1");
                if (chk.Checked)
                {
                    count++;
                }
                rcount = count.ToString();
            }

            if (rcount != "0")
            {

                foreach (GridViewRow gvrow in gvSelected.Rows)
                {
                    CheckBox chk = (CheckBox)gvrow.FindControl("CheckBox1");

                    if (chk.Checked)
                    {
                        string kat = ((Label)gvrow.FindControl("Label1")).Text.ToString();
                        string skat = ((Label)gvrow.FindControl("Label2")).Text.ToString();
                        string jat = ((Label)gvrow.FindControl("Label3")).Text.ToString();
                        string aat = ((Label)gvrow.FindControl("Label4")).Text.ToString();
                        string ais = ((Label)gvrow.FindControl("Label5")).Text.ToString();
                        DataTable dd4 = new DataTable();
                        dd4 = DBCon.Ora_Execute_table("select  ast_kategori_code,ast_kategori_desc from Ref_ast_kategori where ast_kategori_desc='" + kat + "'");
                        DataTable dd5 = new DataTable();
                        dd5 = DBCon.Ora_Execute_table("select ast_subkateast_Code,ast_subkateast_desc from Ref_ast_sub_kategri_Aset where ast_subkateast_desc='" + skat + "'");
                        DataTable dd6 = new DataTable();
                        dd6 = DBCon.Ora_Execute_table("select ast_jeniaset_Code,ast_jeniaset_desc from Ref_ast_jenis_aset where ast_jeniaset_desc='" + jat + "'");
                        DataTable dd7 = new DataTable();
                        dd7 = DBCon.Ora_Execute_table("select cas_asset_cd,cas_asset_desc from ast_cmn_asset where cas_asset_cat_cd='" + dd4.Rows[0]["ast_kategori_code"].ToString() + "' and cas_asset_sub_cat_cd='" + dd5.Rows[0]["ast_subkateast_Code"].ToString() + "' and cas_asset_type_cd='" + dd6.Rows[0]["ast_jeniaset_Code"].ToString() + "' and cas_asset_desc='" + aat + "'");


                        using (SqlConnection con = new SqlConnection(cs))
                        {
                            con.Open();
                            DataTable sel_stf = new DataTable();
                            sel_stf = DBCon.Ora_Execute_table("select *  from ast_staff_asset where sas_staff_no='" + DropDownList3.SelectedValue + "' and sas_asset_id='" + ais + "'");

                            DataTable dd_org = new DataTable();
                            dd_org = DBCon.Ora_Execute_table("select str_curr_org_cd from hr_staff_profile  where stf_staff_no='" + DropDownList3.SelectedValue + "'");

                            if (sel_stf.Rows.Count == 0)
                            {
                                DateTime ft = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                string all_dt = ft.ToString("yyyy-MM-dd");
                                if (DD_Kategori.SelectedValue == "01")
                                {

                                    DataTable rt = new DataTable();
                                    //rt = DBCon.Ora_Execute_table("select  format(com_reg_dt,'yyyy/MM/dd hh:mm:ss') com_reg_dt  from ast_component   where com_asset_cat_cd='" + DD_Kategori.SelectedValue + "' and com_asset_sub_cat_cd='" + dd5.Rows[0][0].ToString() + "' and com_asset_type_cd='" + dd6.Rows[0][0].ToString() + "' and com_asset_cd='" + dd7.Rows[0][0].ToString() + "'");
                                    rt = DBCon.Ora_Execute_table("select  format(com_reg_dt,'yyyy/MM/dd hh:mm:ss') com_reg_dt  from ast_component   where com_asset_id='" + ais + "'");

                                    string Inssql1 = "insert into ast_staff_asset (sas_staff_no ,sas_asset_cat_cd ,sas_asset_sub_cat_cd ,sas_asset_type_cd ,sas_asset_cd ,sas_asset_id ,sas_reg_dt ,sas_staff_name ,sas_allocate_dt ,sas_location_cd ,sas_qty ,sas_crt_id ,sas_crt_dt,sas_org_id,flag_set)values('" + DropDownList3.SelectedValue + "','" + dd4.Rows[0][0].ToString() + "','" + dd5.Rows[0][0].ToString() + "','" + dd6.Rows[0][0].ToString() + "','" + dd7.Rows[0][0].ToString() + "','" + ais + "','" + rt.Rows[0][0].ToString() + "','" + DropDownList3.SelectedItem.Text.Replace("'", "''") + "','" + all_dt + "','" + DropDownList4.SelectedValue + "','" + TextBox2.Text + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + dd_org.Rows[0]["str_curr_org_cd"].ToString() + "','0')";
                                    Status = DBCon.Ora_Execute_CommamdText(Inssql1);

                                    //SqlCommand cmd = new SqlCommand("insert into ast_staff_asset (sas_staff_no ,sas_asset_cat_cd ,sas_asset_sub_cat_cd ,sas_asset_type_cd ,sas_asset_cd ,sas_asset_id ,sas_reg_dt ,sas_staff_name ,sas_allocate_dt ,sas_location_cd ,sas_qty ,sas_crt_id ,sas_crt_dt,sas_org_id,flag_set)values('" + DropDownList3.SelectedValue + "','" + dd4.Rows[0][0].ToString() + "','" + dd5.Rows[0][0].ToString() + "','" + dd6.Rows[0][0].ToString() + "','" + dd7.Rows[0][0].ToString() + "','" + ais + "','" + rt.Rows[0][0].ToString() + "','" + DropDownList3.SelectedItem.Text.Replace("'", "''") + "','" + all_dt + "','" + DropDownList4.SelectedValue + "','" + TextBox2.Text + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + dd_org.Rows[0]["str_curr_org_cd"].ToString() + "','0')", con);
                                    //cmd.ExecuteNonQuery();
                                    DataTable dtacp_com = new DataTable();
                                    dtacp_com = DBCon.Ora_Execute_table("update ast_component set com_sas_id='1',flag_set='0' where com_asset_id='" + ais + "'");
                                }
                                else if (DD_Kategori.SelectedValue == "02")
                                {
                                    DataTable rt = new DataTable();

                                    rt = DBCon.Ora_Execute_table("select  format(car_reg_dt,'yyyy/MM/dd hh:mm:ss') car_reg_dt  from ast_car   where car_asset_id='" + ais + "'");

                                    string Inssql2 = "insert into ast_staff_asset (sas_staff_no ,sas_asset_cat_cd ,sas_asset_sub_cat_cd ,sas_asset_type_cd ,sas_asset_cd ,sas_asset_id ,sas_reg_dt ,sas_staff_name ,sas_allocate_dt ,sas_location_cd ,sas_qty ,sas_crt_id ,sas_crt_dt,sas_org_id,flag_set)values('" + DropDownList3.SelectedValue + "','" + dd4.Rows[0][0].ToString() + "','" + dd5.Rows[0][0].ToString() + "','" + dd6.Rows[0][0].ToString() + "','" + dd7.Rows[0][0].ToString() + "','" + ais + "','" + rt.Rows[0][0].ToString() + "','" + DropDownList3.SelectedItem.Text.Replace("'", "''") + "','" + all_dt + "','" + DropDownList4.SelectedValue + "','" + TextBox2.Text + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + dd_org.Rows[0]["str_curr_org_cd"].ToString() + "','0')";
                                    Status = DBCon.Ora_Execute_CommamdText(Inssql2);

                                    //SqlCommand cmd = new SqlCommand("insert into ast_staff_asset (sas_staff_no ,sas_asset_cat_cd ,sas_asset_sub_cat_cd ,sas_asset_type_cd ,sas_asset_cd ,sas_asset_id ,sas_reg_dt ,sas_staff_name ,sas_allocate_dt ,sas_location_cd ,sas_qty ,sas_crt_id ,sas_crt_dt,sas_org_id,flag_set)values('" + DropDownList3.SelectedValue + "','" + dd4.Rows[0][0].ToString() + "','" + dd5.Rows[0][0].ToString() + "','" + dd6.Rows[0][0].ToString() + "','" + dd7.Rows[0][0].ToString() + "','" + ais + "','" + rt.Rows[0][0].ToString() + "','" + DropDownList3.SelectedItem.Text.Replace("'", "''") + "','" + all_dt + "','" + DropDownList4.SelectedValue + "','" + TextBox2.Text + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + dd_org.Rows[0]["str_curr_org_cd"].ToString() + "','0')", con);
                                    //cmd.ExecuteNonQuery();
                                    DataTable dtacp_car = new DataTable();
                                    dtacp_car = DBCon.Ora_Execute_table("update ast_car set car_sas_id='1',flag_set='0' where car_asset_id='" + ais + "'");
                                }
                                else if (DD_Kategori.SelectedValue == "03")
                                {
                                    if (rcount == "1")
                                    {
                                        DataTable rt = new DataTable();
                                        rt = DBCon.Ora_Execute_table("select  format(inv_reg_dt,'yyyy/MM/dd hh:mm:ss')  inv_reg_dt,inv_qty  from ast_inventory   where inv_asset_id='" + ais + "'");

                                        DataTable rt1 = new DataTable();
                                        rt1 = DBCon.Ora_Execute_table("select ISNULL(sum(sas_qty),'') as tqty from ast_staff_asset where sas_asset_id='" + ais + "'");

                                        int sqty = (Convert.ToInt32(rt1.Rows[0]["tqty"].ToString()) + Convert.ToInt32(TextBox2.Text));

                                        int sqty1 = Convert.ToInt32(rt.Rows[0]["inv_qty"].ToString());

                                        int sqty2 = (Convert.ToInt32(rt.Rows[0]["inv_qty"].ToString()) - Convert.ToInt32(rt1.Rows[0]["tqty"].ToString()));

                                        if (sqty1 >= sqty)
                                        {
                                            string Inssql3 = "insert into ast_staff_asset (sas_staff_no ,sas_asset_cat_cd ,sas_asset_sub_cat_cd ,sas_asset_type_cd ,sas_asset_cd ,sas_asset_id ,sas_reg_dt ,sas_staff_name ,sas_allocate_dt ,sas_location_cd ,sas_qty ,sas_crt_id ,sas_crt_dt,sas_org_id,flag_set)values('" + DropDownList3.SelectedValue + "','" + dd4.Rows[0][0].ToString() + "','" + dd5.Rows[0][0].ToString() + "','" + dd6.Rows[0][0].ToString() + "','" + dd7.Rows[0][0].ToString() + "','" + ais + "','" + rt.Rows[0][0].ToString() + "','" + DropDownList3.SelectedItem.Text.Replace("'", "''") + "','" + all_dt + "','" + DropDownList4.SelectedValue + "','" + TextBox2.Text + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + dd_org.Rows[0]["str_curr_org_cd"].ToString() + "','0')";
                                            Status = DBCon.Ora_Execute_CommamdText(Inssql3);
                                            //SqlCommand cmd = new SqlCommand("insert into ast_staff_asset (sas_staff_no ,sas_asset_cat_cd ,sas_asset_sub_cat_cd ,sas_asset_type_cd ,sas_asset_cd ,sas_asset_id ,sas_reg_dt ,sas_staff_name ,sas_allocate_dt ,sas_location_cd ,sas_qty ,sas_crt_id ,sas_crt_dt,sas_org_id,flag_set)values('" + DropDownList3.SelectedValue + "','" + dd4.Rows[0][0].ToString() + "','" + dd5.Rows[0][0].ToString() + "','" + dd6.Rows[0][0].ToString() + "','" + dd7.Rows[0][0].ToString() + "','" + ais + "','" + rt.Rows[0][0].ToString() + "','" + DropDownList3.SelectedItem.Text.Replace("'", "''") + "','" + all_dt + "','" + DropDownList4.SelectedValue + "','" + TextBox2.Text + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + dd_org.Rows[0]["str_curr_org_cd"].ToString() + "','0')", con);
                                            //cmd.ExecuteNonQuery();
                                            DataTable dtacp_inv = new DataTable();
                                            if (sqty1 == sqty)
                                            {
                                                dtacp_inv = DBCon.Ora_Execute_table("update ast_inventory set inv_sas_id='1' where inv_asset_id='" + ais + "'");
                                            }
                                            else
                                            {
                                                dtacp_inv = DBCon.Ora_Execute_table("update ast_inventory set flag_set='0' where inv_asset_id='" + ais + "'");
                                            }
                                        }
                                        else
                                        {
                                            bgrid2();
                                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Baki Kuantiti Aset ini hanya untuk " + sqty2 + ".',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                                            TextBox2.Text = Convert.ToString(sqty2);
                                        }
                                    }
                                    else
                                    {
                                        bgrid2();
                                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila mesti dipilih hanya satu Semak kotak.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                                    }
                                }
                                else if (DD_Kategori.SelectedValue == "04")
                                {
                                    DataTable rt = new DataTable();
                                    rt = DBCon.Ora_Execute_table("select  format(pro_reg_dt,'yyyy/MM/dd hh:mm:ss')  pro_reg_dt  from ast_property   where pro_asset_id='" + ais + "'");

                                    string Inssql4 = "insert into ast_staff_asset (sas_staff_no ,sas_asset_cat_cd ,sas_asset_sub_cat_cd ,sas_asset_type_cd ,sas_asset_cd ,sas_asset_id ,sas_reg_dt ,sas_staff_name ,sas_allocate_dt ,sas_location_cd ,sas_qty ,sas_crt_id ,sas_crt_dt,sas_org_id,flag_set)values('" + DropDownList3.SelectedValue + "','" + dd4.Rows[0][0].ToString() + "','" + dd5.Rows[0][0].ToString() + "','" + dd6.Rows[0][0].ToString() + "','" + dd7.Rows[0][0].ToString() + "','" + ais + "','" + rt.Rows[0][0].ToString() + "','" + DropDownList3.SelectedItem.Text.Replace("'", "''") + "','" + all_dt + "','" + DropDownList4.SelectedValue + "','" + TextBox2.Text + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + dd_org.Rows[0]["str_curr_org_cd"].ToString() + "','0')";
                                    Status = DBCon.Ora_Execute_CommamdText(Inssql4);

                                    //SqlCommand cmd = new SqlCommand("insert into ast_staff_asset (sas_staff_no ,sas_asset_cat_cd ,sas_asset_sub_cat_cd ,sas_asset_type_cd ,sas_asset_cd ,sas_asset_id ,sas_reg_dt ,sas_staff_name ,sas_allocate_dt ,sas_location_cd ,sas_qty ,sas_crt_id ,sas_crt_dt,sas_org_id,flag_set)values('" + DropDownList3.SelectedValue + "','" + dd4.Rows[0][0].ToString() + "','" + dd5.Rows[0][0].ToString() + "','" + dd6.Rows[0][0].ToString() + "','" + dd7.Rows[0][0].ToString() + "','" + ais + "','" + rt.Rows[0][0].ToString() + "','" + DropDownList3.SelectedItem.Text.Replace("'", "''") + "','" + all_dt + "','" + DropDownList4.SelectedValue + "','" + TextBox2.Text + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + dd_org.Rows[0]["str_curr_org_cd"].ToString() + "','0')", con);
                                    //cmd.ExecuteNonQuery();
                                    DataTable dtacp_pro = new DataTable();
                                    dtacp_pro = DBCon.Ora_Execute_table("update ast_property set pro_sas_id='1',flag_set='0' where pro_asset_id='" + ais + "'");
                                }

                            }
                            else
                            {
                                dd_qry();
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Sudah Wujud.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                            }
                            con.Close();
                        }
                    }
                }

                if (Status == "SUCCESS")
                {
                    clear();
                    dd_qry();
                    bgrid2();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }


                //clear();
            }
            else
            {
                dd_qry();
                bgrid2();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Minimum Satu Kotak.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }

        }
        else
        {
            dd_qry();
            bgrid2();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Bidang Adalah Wajib.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    void clear()
    {
        DropDownList4.SelectedValue = "";
        DropDownList3.SelectedValue = "";
        TextBox3.Text = "";
        TextBox2.Text = "";
    }


    protected void clk_cetak(object sender, EventArgs e)
    {
        string rcount = string.Empty;
        int count = 0;
        foreach (GridViewRow gvrow in GridView1.Rows)
        {
            var rb = gvrow.FindControl("RadioButton1") as System.Web.UI.WebControls.RadioButton;
            if (rb.Checked)
            {
                count++;
            }
            rcount = count.ToString();
        }
        if (rcount != "0")
        {
            try
            {
                foreach (GridViewRow row in GridView1.Rows)
                {

                    var rbn = row.FindControl("RadioButton1") as System.Web.UI.WebControls.RadioButton;
                    if (rbn.Checked)
                    {
                        int RowIndex = row.RowIndex;
                        varName1 = ((Label)row.FindControl("Label5_astid")).Text.ToString(); //this store the  value in varName1
                        gen_barcode();
                        dd_qry();
                    }
                }

            }
            catch
            {
                dd_qry();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod yang dipilih sekarang Digunakan Oleh Meja Lain Berkaitan jadi tidak Padam.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
            //}
        }
        else
        {
            dd_qry();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Sekurang-kurangnya Satu Kotak Semak.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
        //}
    }

    void gen_barcode()
    {
        try
        {

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            //dt = DBCon.Ora_Execute_table("select ho.org_name,rk.ast_kategori_desc,rja.ast_jeniaset_desc,aca.cas_asset_desc,a.sas_asset_id,a.sas_curr_price_amt,a.sas_asset_cat_cd,a.sas_asset_sub_cat_cd,a.sas_asset_type_cd,a.sas_asset_cd,a.sas_org_id, case a.sas_asset_cat_cd when '01' then (select FORMAT(com_reg_dt,'dd/MM/yyyy', 'en-us') as reg_dt from ast_component where com_asset_cat_cd=a.sas_asset_cat_cd and com_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and com_asset_type_cd=a.sas_asset_type_cd and com_asset_cd=a.sas_asset_cd) when '02' then (select FORMAT(car_reg_dt,'dd/MM/yyyy', 'en-us') as reg_dt from ast_car where car_asset_cat_cd=a.sas_asset_cat_cd and car_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and car_asset_type_cd=a.sas_asset_type_cd and car_asset_cd=a.sas_asset_cd) when '03' then (select FORMAT(inv_reg_dt,'dd/MM/yyyy', 'en-us') as reg_dt from ast_inventory where inv_asset_cat_cd=a.sas_asset_cat_cd and inv_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and inv_asset_type_cd=a.sas_asset_type_cd and inv_asset_cd=a.sas_asset_cd) end as a1, case a.sas_asset_cat_cd when '01' then (select DATEDIFF(day,com_reg_dt,GETDATE()) as u_dt from ast_component where com_asset_cat_cd=a.sas_asset_cat_cd and com_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and com_asset_type_cd=a.sas_asset_type_cd and com_asset_cd=a.sas_asset_cd) when '02' then (select DATEDIFF(day,car_reg_dt,GETDATE()) as u_dt from ast_car where car_asset_cat_cd=a.sas_asset_cat_cd and car_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and car_asset_type_cd=a.sas_asset_type_cd and car_asset_cd=a.sas_asset_cd) when '03' then (select  DATEDIFF(day,inv_reg_dt,GETDATE()) as u_dt from ast_inventory where inv_asset_cat_cd=a.sas_asset_cat_cd and inv_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and inv_asset_type_cd=a.sas_asset_type_cd and inv_asset_cd=a.sas_asset_cd) end as a2, case a.sas_asset_cat_cd when '01' then (select com_price_amt from ast_component where com_asset_cat_cd=a.sas_asset_cat_cd and com_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and com_asset_type_cd=a.sas_asset_type_cd and com_asset_cd=a.sas_asset_cd) when '02' then (select car_price_amt from ast_car where car_asset_cat_cd=a.sas_asset_cat_cd and car_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and car_asset_type_cd=a.sas_asset_type_cd and car_asset_cd=a.sas_asset_cd) when '03' then (select inv_price_amt from ast_inventory where inv_asset_cat_cd=a.sas_asset_cat_cd and inv_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and inv_asset_type_cd=a.sas_asset_type_cd and inv_asset_cd=a.sas_asset_cd) end as a3 from (select * from ast_staff_asset  where sas_cond_sts_cd = 'DI' and sas_dispose_cfm_ind !='Y') as a left join Ref_ast_kategori as rk on rk.ast_kategori_code=a.sas_asset_cat_cd left join Ref_ast_jenis_aset as rja on rja.ast_jeniaset_Code=a.sas_asset_type_cd left join ast_cmn_asset as aca on aca.cas_asset_cd=a.sas_asset_cd left join hr_organization as ho on ho.org_gen_id=a.sas_org_id");
            dt = DBCon.Ora_Execute_table("select sas_asset_id,sas_asset_cat_cd,sas_asset_sub_cat_cd,sas_asset_type_cd,sas_asset_cd,sas_location_cd,s1.ast_jeniaset_desc from ast_staff_asset left join Ref_ast_jenis_aset as s1 on s1.ast_jeniaset_Code=sas_asset_type_cd and s1.ast_sub_cat_Code=sas_asset_sub_cat_cd where sas_asset_id='" + varName1 + "' order by sas_crt_dt desc");
            RptviwerStudent.Reset();
            ds.Tables.Add(dt);

            List<DataRow> listResult = dt.AsEnumerable().ToList();
            listResult.Count();
            int countRow = 0;
            countRow = listResult.Count();

            RptviwerStudent.LocalReport.DataSources.Clear();
            if (countRow != 0)
            {
                RptviwerStudent.LocalReport.ReportPath = "Aset/Ast_Barcode.rdlc";
                ReportDataSource rds = new ReportDataSource("Bcode", dt);

                string bar_code = dt.Rows[0]["sas_asset_id"].ToString();
                string ast_type = dt.Rows[0]["ast_jeniaset_desc"].ToString();
                string ast_cd = dt.Rows[0]["sas_asset_cd"].ToString();

                ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("bcode", bar_code),
                     new ReportParameter("asttype", ast_type),
                     new ReportParameter("astcd", ast_cd)
                     };


                RptviwerStudent.LocalReport.SetParameters(rptParams);

                RptviwerStudent.LocalReport.DataSources.Add(rds);
                RptviwerStudent.LocalReport.Refresh();

                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;
                string filename;

                filename = string.Format("{0}.{1}", "Barcode_" + bar_code.ToUpper() + "_" + DateTime.Now.ToString("ddMMyyyy") + "", "pdf");
                byte[] bytes = RptviwerStudent.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=" + filename);
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();


            }
            else if (countRow == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod tidak dijumpai. Sila Pastikan Semua Maklumat Dimasukkan Dengan Betul.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Issue.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    protected void back_btn(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_penembetan_view.aspx");
    }

    protected void Reset_btn(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_penembetan.aspx");
    }

 
    
}