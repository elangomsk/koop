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

public partial class Ast_pergerakan : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();

    string Status = string.Empty;
    string level, userid, stscd, abc1;
    string val1 = string.Empty, val2 = string.Empty, val3 = string.Empty;
    string c1 = string.Empty, c2 = string.Empty, c3 = string.Empty, c4 = string.Empty, c5 = string.Empty, c6 = string.Empty, c7 = string.Empty, c8 = string.Empty, c9 = string.Empty, c10 = string.Empty, c11 = string.Empty;
    string kd1 = string.Empty;
    string result_kat, result_subkat, result_jeniskat, result_asset_cd;
    string clmfd = string.Empty, clm_name = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2()})";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;
                gd_loc();
                grid();
                res();
                Bind_staff();
                TextBox10.Attributes.Add("Readonly", "Readonly");
                
                //TextBox18.Text = DateTime.Now.ToString("HH:mm tt");
                //TextBox19.Text = DateTime.Now.ToString("HH:mm tt");

                DataTable ddokdicno = new DataTable();
                ddokdicno = DBCon.Ora_Execute_table("select stf_staff_no,stf_name,ISNULL(rj.Jawatan_desc,'') as Jawatan_desc From hr_staff_profile left join ref_jawatan as rj on rj.Jawatan_Code=stf_curr_post_cd where stf_staff_no='" + Session["New"].ToString() + "' ");
                if (ddokdicno.Rows.Count != 0)
                {
                    TextBox21.Text = ddokdicno.Rows[0]["stf_name"].ToString();
                    TextBox22.Text = ddokdicno.Rows[0]["Jawatan_desc"].ToString();
                    TextBox23.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
                if (samp != "")
                {
                    lbl_name.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    view_details();
                    ver_id.Text = "1";

                }
                else
                {
                    ver_id.Text = "0";
                }
                userid = Session["New"].ToString();
               
             
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

   
    void view_details()
    {
        Button1.Visible = false;
        btn_Show.Visible = false;
        DropDownList2.Items[1].Attributes.Add("disabled", "disabled");
        try
        {
            TextBox1.Text = lbl_name.Text;
            show_details();


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void gd_loc()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select loc_cd,UPPER(loc_desc) as loc_desc from ref_location";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList1.DataSource = dt;
            DropDownList1.DataBind();
            DropDownList1.DataTextField = "loc_desc";
            DropDownList1.DataValueField = "loc_cd";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void RB11_CheckedChanged(object sender, EventArgs e)
    {
        if (ch1.Checked == true)
        {
            ch2.Checked = false;
            ch3.Checked = false;
            ch1.Checked = true;
        }


    }

    protected void RB12_CheckedChanged(object sender, EventArgs e)
    {
        if (ch2.Checked == true)
        {
            ch1.Checked = false;
            ch3.Checked = false;
            ch2.Checked = true;
        }

    }

    protected void RB13_CheckedChanged(object sender, EventArgs e)
    {
        if (ch3.Checked == true)
        {
            ch1.Checked = false;
            ch2.Checked = false;
            ch3.Checked = true;
        }

    }

    //void Bind_Kat()
    //{
    //    DataSet Ds = new DataSet();
    //    try
    //    {
    //        string com = "select ast_kategori_code,UPPER(ast_kategori_desc) as as_desc from Ref_ast_kategori order by ast_kategori_code";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
    //        DataTable dt = new DataTable();
    //        adpt.Fill(dt);
    //        dd_kat.DataSource = dt;
    //        dd_kat.DataTextField = "as_desc";
    //        dd_kat.DataValueField = "ast_kategori_code";
    //        dd_kat.DataBind();
    //        dd_kat.Items.Insert(0, new ListItem("--- PILIH ---", ""));


    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //protected void Bind_SubKat(object sender, EventArgs e)
    //{
    //    DataSet Ds = new DataSet();
    //    try
    //    {
    //        string com = "select ast_subkateast_Code,UPPER(ast_subkateast_desc) as sub_desc,ast_kategori_Code from Ref_ast_sub_kategri_Aset where ast_kategori_Code='"+dd_kat.SelectedValue+"' order by ast_subkateast_desc";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
    //        DataTable dt = new DataTable();
    //        adpt.Fill(dt);
    //        dd_skat.DataSource = dt;
    //        dd_skat.DataTextField = "sub_desc";
    //        dd_skat.DataValueField = "ast_subkateast_Code";
    //        dd_skat.DataBind();
    //        dd_skat.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    //        //grid();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    protected void OnSelectedIndexChanged1(object sender, EventArgs e)
    {
        //jenaset();
        grid();
    }

    //void jenaset()
    //{
    //    DataSet Ds1 = new DataSet();
    //    try
    //    {
    //        string com1 = "select * from Ref_ast_jenis_aset where Status='A' and ast_cat_Code='" + dd_kat.SelectedValue + "' and ast_sub_cat_Code = '" + dd_skat.SelectedValue + "'";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com1, con);
    //        DataTable dt1 = new DataTable();
    //        adpt.Fill(dt1);
    //        dd_jaset.DataSource = dt1;
    //        dd_jaset.DataTextField = "ast_jeniaset_desc";
    //        dd_jaset.DataValueField = "ast_jeniaset_Code";
    //        dd_jaset.DataBind();
    //        dd_jaset.Items.Insert(0, new ListItem("--- PILIH ---", ""));

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    protected void OnSelectedIndexChanged2(object sender, EventArgs e)
    {
        //cmnaset();
        grid();
    }

    //void cmnaset()
    //{
    //    DataSet Ds1 = new DataSet();
    //    try
    //    {
    //        string com1 = "select * from ast_cmn_asset where cas_asset_cat_cd='" + dd_kat.SelectedValue + "' and cas_asset_sub_cat_cd='" + dd_skat.SelectedValue + "' and cas_asset_type_cd='" + dd_jaset.SelectedValue + "'";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com1, con);
    //        DataTable dt1 = new DataTable();
    //        adpt.Fill(dt1);
    //        DropDownList7.DataSource = dt1;
    //        DropDownList7.DataTextField = "cas_asset_desc";
    //        DropDownList7.DataValueField = "cas_asset_cd";
    //        DropDownList7.DataBind();
    //        DropDownList7.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //void Bind_jenisaset()
    //{
    //    DataSet Ds = new DataSet();
    //    try
    //    {
    //        string com = "select ast_jeniaset_Code,UPPER(ast_jeniaset_desc) as jen_desc from Ref_ast_jenis_aset order by ast_jeniaset_desc";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
    //        DataTable dt = new DataTable();
    //        adpt.Fill(dt);
    //        dd_jaset.DataSource = dt;
    //        dd_jaset.DataTextField = "jen_desc";
    //        dd_jaset.DataValueField = "ast_jeniaset_Code";
    //        dd_jaset.DataBind();
    //        dd_jaset.Items.Insert(0, new ListItem("--- PILIH ---", ""));

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}


    //void Bind_aset()
    //{
    //    DataSet Ds = new DataSet();
    //    try
    //    {
    //        string com = "select cas_asset_cd,UPPER(cas_asset_desc) as sa_desc from ast_cmn_asset order by cas_asset_desc";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
    //        DataTable dt = new DataTable();
    //        adpt.Fill(dt);
    //        DropDownList7.DataSource = dt;
    //        DropDownList7.DataTextField = "sa_desc";
    //        DropDownList7.DataValueField = "cas_asset_cd";
    //        DropDownList7.DataBind();
    //        DropDownList7.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    void Bind_staff()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select stf_staff_no,UPPER(stf_name) as stf_name from hr_staff_profile group by stf_staff_no,stf_name order by stf_name ";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList5.DataSource = dt;
            DropDownList5.DataTextField = "stf_name";
            DropDownList5.DataValueField = "stf_staff_no";
            DropDownList5.DataBind();
            DropDownList5.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //void Bind_staff()
    //{
    //    DataSet Ds = new DataSet();
    //    try
    //    {
    //        string com = "select distinct sas_staff_no,sas_staff_name from ast_staff_asset";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
    //        DataTable dt = new DataTable();
    //        adpt.Fill(dt);
    //        DropDownList5.DataSource = dt;
    //        DropDownList5.DataTextField = "sas_staff_name";
    //        DropDownList5.DataValueField = "sas_staff_no";
    //        DropDownList5.DataBind();
    //        DropDownList5.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}




    void grid()
    {

        //qry_det();
        //con.Open();
        //DataTable ddicno = new DataTable();
        //SqlCommand cmd = new SqlCommand(""+ val1 +"", con);
        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        //da.Fill(ds);
        //if (ds.Tables[0].Rows.Count == 0)
        //{
        //    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
        //    GridView1.DataSource = ds;
        //    GridView1.DataBind();
        //    int columncount = GridView1.Rows[0].Cells.Count;
        //    GridView1.Rows[0].Cells.Clear();
        //    GridView1.Rows[0].Cells.Add(new TableCell());
        //    GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
        //    GridView1.Rows[0].Cells[0].Text = "<center><strong> Maklumat Carian Tidak Dijumpai </strong></center>";

        //}
        //else
        //{
        //    GridView1.DataSource = ds;
        //    GridView1.DataBind();

        //}
        //con.Close();
    }


    //protected void bt_TextChanged(object sender, EventArgs e)
    //{
    //    TextBox5.Text = (double.Parse(TextBox6.Text) - double.Parse(TextBox4.Text)).ToString();
    //}

    void qry_det()
    {
        sel_clmflds();

        //res();


        if (result_kat == "01")
        {

            if (result_kat == "01" && TextBox1.Text == "")
            {
                val1 = "select com_asset_id as s1,FORMAT(com_reg_dt,'yyyy-MM-dd HH:mm:ss', 'en-us') as reg_dt,ISNULL(st.sas_staff_no,'') as sno,com_asset_cat_cd as mccd,com_asset_sub_cat_cd as mscd,com_asset_type_cd as atcd,ak.ast_kategori_desc,ask.ast_subkateast_desc,ja.ast_jeniaset_desc,ca.cas_asset_desc,com_asset_cd as cd from ast_component as c1 left join ast_staff_asset as st on st.sas_asset_id=com_asset_id left join Ref_ast_kategori as ak on ak.ast_kategori_code=com_asset_cat_cd left join Ref_ast_sub_kategri_Aset as ask on ask.ast_subkateast_Code = com_asset_sub_cat_cd and ask.ast_kategori_Code=com_asset_cat_cd left join Ref_ast_jenis_aset as ja on ja.ast_jeniaset_Code = com_asset_type_cd and ja.ast_sub_cat_Code=com_asset_sub_cat_cd and ja.ast_cat_Code=com_asset_cat_cd left join ast_cmn_asset as ca on ca.cas_asset_cd=com_asset_cd and ca.cas_asset_cat_cd=com_asset_cat_cd and ca.cas_asset_sub_cat_cd=com_asset_sub_cat_cd and ca.cas_asset_type_cd=com_asset_type_cd where st.flag_set ='0' and " + clmfd + "";
            }
            else
            {
                val1 = "select com_asset_id as s1,FORMAT(com_reg_dt,'yyyy-MM-dd HH:mm:ss', 'en-us') as reg_dt,ISNULL(st.sas_staff_no,'') as sno,com_asset_cat_cd as mccd,com_asset_sub_cat_cd as mscd,com_asset_type_cd as atcd,ak.ast_kategori_desc,ask.ast_subkateast_desc,ja.ast_jeniaset_desc,ca.cas_asset_desc,com_asset_cd as cd from ast_component as c1 left join ast_staff_asset as st on st.sas_asset_id=com_asset_id left join Ref_ast_kategori as ak on ak.ast_kategori_code=com_asset_cat_cd left join Ref_ast_sub_kategri_Aset as ask on ask.ast_subkateast_Code = com_asset_sub_cat_cd and ask.ast_kategori_Code=com_asset_cat_cd left join Ref_ast_jenis_aset as ja on ja.ast_jeniaset_Code = com_asset_type_cd and ja.ast_sub_cat_Code=com_asset_sub_cat_cd and ja.ast_cat_Code=com_asset_cat_cd left join ast_cmn_asset as ca on ca.cas_asset_cd=com_asset_cd and ca.cas_asset_cat_cd=com_asset_cat_cd and ca.cas_asset_sub_cat_cd=com_asset_sub_cat_cd and ca.cas_asset_type_cd=com_asset_type_cd where st.flag_set ='0' and com_asset_id = '" + TextBox1.Text + "'";
            }
        }

        else if (result_kat == "02")
        {
            val1 = "select car_asset_id as s1,FORMAT(car_reg_dt,'yyyy-MM-dd HH:mm:ss', 'en-us') as reg_dt,car_allocate_staff_no as sno,car_asset_cat_cd as mccd,car_asset_sub_cat_cd as mscd,car_asset_type_cd as atcd,ak.ast_kategori_desc,ask.ast_subkateast_desc,ja.ast_jeniaset_desc,ca.cas_asset_desc,car_asset_cd as cd from ast_car as c1 left join Ref_ast_kategori as ak on ak.ast_kategori_code=car_asset_cat_cd left join Ref_ast_sub_kategri_Aset as ask on ask.ast_subkateast_Code = car_asset_sub_cat_cd and ask.ast_kategori_Code=car_asset_cat_cd left join Ref_ast_jenis_aset as ja on ja.ast_jeniaset_Code = car_asset_type_cd and ja.ast_sub_cat_Code=car_asset_sub_cat_cd and ja.ast_cat_Code=car_asset_cat_cd left join ast_cmn_asset as ca on ca.cas_asset_cd=car_asset_cd and ca.cas_asset_cat_cd=car_asset_cat_cd and ca.cas_asset_sub_cat_cd=car_asset_sub_cat_cd and ca.cas_asset_type_cd=car_asset_type_cd  where c1.flag_set ='0' and " + clmfd + "";
        }
        else if (result_kat == "03")
        {
            val1 = "select inv_asset_id as s1,FORMAT(inv_reg_dt,'yyyy-MM-dd HH:mm:ss', 'en-us') as reg_dt,inv_supplier_id as sno,inv_asset_cat_cd as mccd,inv_asset_sub_cat_cd as mscd,inv_asset_type_cd as atcd,ak.ast_kategori_desc,ask.ast_subkateast_desc,ja.ast_jeniaset_desc,ca.cas_asset_desc,inv_asset_cd as cd from ast_inventory as c1 left join Ref_ast_kategori as ak on ak.ast_kategori_code=inv_asset_cat_cd left join Ref_ast_sub_kategri_Aset as ask on ask.ast_subkateast_Code = inv_asset_sub_cat_cd and ask.ast_kategori_Code=inv_asset_cat_cd left join Ref_ast_jenis_aset as ja on ja.ast_jeniaset_Code = inv_asset_type_cd and ja.ast_sub_cat_Code=inv_asset_sub_cat_cd and ja.ast_cat_Code=inv_asset_cat_cd left join ast_cmn_asset as ca on ca.cas_asset_cd=inv_asset_cd and ca.cas_asset_cat_cd=inv_asset_cat_cd and ca.cas_asset_sub_cat_cd=inv_asset_sub_cat_cd and ca.cas_asset_type_cd=inv_asset_type_cd where c1.flag_set ='0' and " + clmfd + "";
        }
        else if (result_kat == "04")
        {
            val1 = "select pro_asset_id as s1,FORMAT(pro_reg_dt,'yyyy-MM-dd HH:mm:ss', 'en-us') as reg_dt,pro_allocate_staff_no as sno,pro_asset_cat_cd as mccd,pro_asset_sub_cat_cd as mscd,pro_asset_type_cd as atcd,ak.ast_kategori_desc,ask.ast_subkateast_desc,ja.ast_jeniaset_desc,ca.cas_asset_desc,pro_asset_cd as cd from ast_property as c1 left join Ref_ast_kategori as ak on ak.ast_kategori_code=pro_asset_cat_cd left join Ref_ast_sub_kategri_Aset as ask on ask.ast_subkateast_Code = pro_asset_sub_cat_cd and ask.ast_kategori_Code=pro_asset_cat_cd left join Ref_ast_jenis_aset as ja on ja.ast_jeniaset_Code = pro_asset_type_cd and ja.pro_sub_cat_Code=car_asset_sub_cat_cd and ja.ast_cat_Code=car_asset_cat_cd left join ast_cmn_asset as ca on ca.cas_asset_cd=pro_asset_cd and ca.cas_asset_cat_cd=pro_asset_cat_cd and ca.cas_asset_sub_cat_cd=pro_asset_sub_cat_cd and ca.cas_asset_type_cd=pro_asset_type_cd where c1.flag_set ='0' and " + clmfd + "";
        }
        else
        {
            val1 = "select com_asset_id as s1,FORMAT(com_reg_dt,'yyyy-MM-dd HH:mm:ss', 'en-us') as reg_dt,com_allocate_staff_no as sno,com_asset_cat_cd as mccd,com_asset_sub_cat_cd as mscd,com_asset_type_cd as atcd,ak.ast_kategori_desc,ask.ast_subkateast_desc,ja.ast_jeniaset_desc,ca.cas_asset_desc,com_asset_cd as cd from ast_component left join Ref_ast_kategori as ak on ak.ast_kategori_code=com_asset_cat_cd left join Ref_ast_sub_kategri_Aset as ask on ask.ast_subkateast_Code = com_asset_sub_cat_cd and ask.ast_kategori_Code=com_asset_cat_cd left join Ref_ast_jenis_aset as ja on ja.ast_jeniaset_Code = com_asset_type_cd and ja.ast_sub_cat_Code=com_asset_sub_cat_cd and ja.ast_cat_Code=com_asset_cat_cd left join ast_cmn_asset as ca on ca.cas_asset_cd=com_asset_cd and ca.cas_asset_cat_cd=com_asset_cat_cd and ca.cas_asset_sub_cat_cd=com_asset_sub_cat_cd and ca.cas_asset_type_cd=com_asset_type_cd where flag_set ='0' and com_asset_id='" + TextBox1.Text + "'";
        }

    }

    void res()
    {

        if (TextBox1.Text == "")
        {
            result_kat = "";
            result_subkat = "";
            result_jeniskat = "";
            result_asset_cd = "";
        }
        else
        {
            result_kat = TextBox1.Text.Substring(0, 2);
            result_subkat = TextBox1.Text.Substring(2, 2);
            result_jeniskat = TextBox1.Text.Substring(4, 2);
            result_asset_cd = TextBox1.Text.Substring(6, 6);
        }
    }

    void sel_clmflds()
    {
        res();
        if (result_kat == "01")
        {
            clm_name = "com";
            clmfd = "" + clm_name + "_asset_id='" + TextBox1.Text + "'";
        }
        else if (result_kat == "02")
        {
            clm_name = "car";
            clmfd = "" + clm_name + "_asset_id='" + TextBox1.Text + "'";
        }
        else if (result_kat == "03")
        {
            clm_name = "inv";
            clmfd = "" + clm_name + "_asset_id='" + TextBox1.Text + "'";
        }
        else if (result_kat == "04")
        {
            clm_name = "pro";
            clmfd = "" + clm_name + "_asset_id='" + TextBox1.Text + "'";
        }
        else
        {
            clm_name = "com";
            clmfd = "" + clm_name + "_asset_cat_id=''";
        }

        //if (dd_kat.SelectedValue != "" && dd_skat.SelectedValue != "" && dd_jaset.SelectedValue != "" && DropDownList7.SelectedValue != "" && TextBox1.Text != "")
        //{

        //}
        //else if (dd_kat.SelectedValue != "" && dd_skat.SelectedValue == "" && dd_jaset.SelectedValue == "" && DropDownList7.SelectedValue == "" && TextBox1.Text == "")
        //{
        //    clmfd = "" + clm_name + "_asset_cat_cd='" + dd_kat.SelectedValue + "'";
        //}
        //else if (dd_kat.SelectedValue != "" && dd_skat.SelectedValue != "" && dd_jaset.SelectedValue == "" && DropDownList7.SelectedValue == "" && TextBox1.Text == "")
        //{
        //    clmfd = "" + clm_name + "_asset_cat_cd='" + dd_kat.SelectedValue + "' and " + clm_name + "_asset_sub_cat_cd='" + dd_skat.SelectedValue + "'";
        //}
        //else if (dd_kat.SelectedValue != "" && dd_skat.SelectedValue != "" && dd_jaset.SelectedValue != "" && DropDownList7.SelectedValue == "" && TextBox1.Text == "")
        //{
        //    clmfd = "" + clm_name + "_asset_cat_cd='" + dd_kat.SelectedValue + "' and " + clm_name + "_asset_sub_cat_cd='" + dd_skat.SelectedValue + "' and " + clm_name + "_asset_type_cd='" + dd_jaset.SelectedValue + "'";
        //}
        //else if (dd_kat.SelectedValue != "" && dd_skat.SelectedValue != "" && dd_jaset.SelectedValue != "" && DropDownList7.SelectedValue != "" && TextBox1.Text == "")
        //{
        //    clmfd = "" + clm_name + "_asset_cat_cd='" + dd_kat.SelectedValue + "' and " + clm_name + "_asset_sub_cat_cd='" + dd_skat.SelectedValue + "' and " + clm_name + "_asset_type_cd='" + dd_jaset.SelectedValue + "' and " + clm_name + "_asset_cd='" + DropDownList7.SelectedValue + "'";
        //}
        //else if (dd_kat.SelectedValue != "" && dd_skat.SelectedValue == "" && dd_jaset.SelectedValue == "" && DropDownList7.SelectedValue == "" && TextBox1.Text != "")
        //{
        //    clmfd = "" + clm_name + "_asset_cat_cd='" + dd_kat.SelectedValue + "' and " + clm_name + "_asset_cd='" + TextBox1.Text + "'";
        //}
        //else if (dd_kat.SelectedValue != "" && dd_skat.SelectedValue != "" && dd_jaset.SelectedValue == "" && DropDownList7.SelectedValue == "" && TextBox1.Text != "")
        //{
        //    clmfd = "" + clm_name + "_asset_cat_cd='" + dd_kat.SelectedValue + "' and " + clm_name + "_asset_sub_cat_cd='" + dd_skat.SelectedValue + "' and " + clm_name + "_asset_cd='" + TextBox1.Text + "'";
        //}
        //else if (dd_kat.SelectedValue != "" && dd_skat.SelectedValue != "" && dd_jaset.SelectedValue != "" && DropDownList7.SelectedValue == "" && TextBox1.Text != "")
        //{
        //    clmfd = "" + clm_name + "_asset_cat_cd='" + dd_kat.SelectedValue + "' and " + clm_name + "_asset_sub_cat_cd='" + dd_skat.SelectedValue + "' and " + clm_name + "_asset_type_cd='" + dd_jaset.SelectedValue + "' and " + clm_name + "_asset_cd='" + TextBox1.Text + "'";
        //}
        //else if (dd_kat.SelectedValue == "" && dd_skat.SelectedValue == "" && dd_jaset.SelectedValue == "" && DropDownList7.SelectedValue == "" && TextBox1.Text != "")
        //{
        //    clmfd = "" + clm_name + "_asset_id='" + TextBox1.Text + "'";
        //}
        //else
        //{
        //    clmfd = "" + clm_name + "_asset_cat_id=''";
        //}

    }

    protected void clk_carian(object sender, EventArgs e)
    {
        if (TextBox1.Text != "")
        {
            //ModalPopupExtender1.Show();
            //grid();
            show_details();
            //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Sila Masukkan Kategory.');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Sila Masukan Aset ID..');", true);
        }
    }

    //protected void lblSubItem_Click(object sender, EventArgs e)
    //{

    //    LinkButton btn = (LinkButton)sender;
    //    string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
    //    c1 = commandArgs[0];
    //    c2 = commandArgs[1];
    //    c3 = commandArgs[2];
    //    c4 = commandArgs[3];
    //    c5 = commandArgs[4];
    //    c6 = commandArgs[5];
    //    c7 = commandArgs[6];
    //    c8 = commandArgs[7];
    //    c9 = commandArgs[8];
    //    c10 = commandArgs[9];
    //    c11 = commandArgs[10];
    //    show_details();
    //}

    void show_details()
    {
        try
        {
            qry_det();
            if (result_kat == "01" || result_kat == "02")
            {
                DataTable cnt_qry = new DataTable();
                cnt_qry = DBCon.Ora_Execute_table("" + val1 + "");
                //if (TextBox4.Text == "")
                //{
                //    kd1 = "0";
                //}
                //else
                //{
                //    kd1 = TextBox4.Text;
                //}
                //TextBox5.Text = (double.Parse(cnt_qry.Rows.Count.ToString()) - double.Parse(kd1)).ToString();
                //TextBox6.Text = cnt_qry.Rows.Count.ToString();

                TextBox20.Text = cnt_qry.Rows[0]["ast_kategori_desc"].ToString();
                TextBox25.Text = cnt_qry.Rows[0]["ast_subkateast_desc"].ToString();
                TextBox26.Text = cnt_qry.Rows[0]["ast_jeniaset_desc"].ToString();
                TextBox2.Text = cnt_qry.Rows[0]["cas_asset_desc"].ToString();
                TextBox3.Text = cnt_qry.Rows[0]["s1"].ToString();
                TextBox24.Text = cnt_qry.Rows[0]["reg_dt"].ToString();
                TextBox27.Text = cnt_qry.Rows[0]["s1"].ToString();
                TextBox28.Text = cnt_qry.Rows[0]["sno"].ToString().Trim();
                TextBox11.Text = cnt_qry.Rows[0]["cd"].ToString();
                TextBox32.Text = cnt_qry.Rows[0]["mccd"].ToString();
                TextBox33.Text = cnt_qry.Rows[0]["mscd"].ToString();
                TextBox34.Text = cnt_qry.Rows[0]["atcd"].ToString();

                DataTable s_det = new DataTable();
                s_det = DBCon.Ora_Execute_table("select stf_staff_no,UPPER(stf_name) as stf_name,ISNULL(UPPER(rj.Jawatan_desc),'') as Jawatan_desc,UPPER(c2.loc_desc) loc_desc From hr_staff_profile left join ref_jawatan as rj on rj.Jawatan_Code=stf_curr_post_cd left join ast_staff_asset c1 on c1.sas_staff_no= stf_staff_no and c1.sas_asset_id='" + TextBox3.Text + "' left join ref_location c2 on c2.loc_cd=c1.sas_location_cd where stf_staff_no='" + TextBox28.Text + "' ");
                if (s_det.Rows.Count != 0)
                {
                    TextBox7.Text = s_det.Rows[0]["stf_name"].ToString();
                    TextBox8.Text = s_det.Rows[0]["Jawatan_desc"].ToString();
                    TextBox10.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    TextBox9.Text = s_det.Rows[0]["loc_desc"].ToString();
                }

                DataTable s_det1 = new DataTable();
                s_det1 = DBCon.Ora_Execute_table("select * from ast_borrow_txn where mov_asset_id='"+ TextBox1.Text + "' and mov_reg_dt='"+ TextBox24.Text + "'");
                if(s_det1.Rows.Count != 0)
                {
                    Button1.Visible = false;
                    btn_Show.Visible = false;
                    DropDownList2.Items[1].Attributes.Add("disabled", "disabled");
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Hanya Dibenarkan untuk Komponen dan Harta Modal.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila berikan semua input dengan betul.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    protected void slct_jp(object sender, EventArgs e)
    {
        if (DropDownList2.SelectedValue == "02" || DropDownList2.SelectedValue == "03")
        {
            Btn_Kemaskini.Text = "Kemaskini";
            DataTable s_det = new DataTable();
            s_det = DBCon.Ora_Execute_table("select stf_staff_no,UPPER(stf_name) as stf_name,ISNULL(UPPER(rj.Jawatan_desc),'') as Jawatan_desc From hr_staff_profile left join ref_jawatan as rj on rj.Jawatan_Code=stf_curr_post_cd where stf_staff_no='" + TextBox28.Text + "' ");
            if (s_det.Rows.Count != 0)
            {
                TextBox7.Text = s_det.Rows[0]["stf_name"].ToString();
                TextBox8.Text = s_det.Rows[0]["Jawatan_desc"].ToString();
                TextBox10.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
            DataTable s_det1 = new DataTable();

            s_det1 = DBCon.Ora_Execute_table("select mov_brw_staff_id,mov_brw_location_cd,mov_brw_purpose,format(mov_brw_start_dt,'dd/MM/yyyy') mov_brw_start_dt,mov_brw_start_time,mov_brw_remark,mov_verify_cond_ind,mov_verify_remark,mov_brw_qty from ast_borrow_txn where mov_asset_cat_cd='" + TextBox32.Text + "' and mov_asset_sub_cat_cd='" + TextBox33.Text + "' and mov_asset_type_cd='" + TextBox34.Text + "' and mov_asset_cd='" + TextBox11.Text + "' and mov_reg_dt='" + TextBox24.Text + "'");
            if (s_det1.Rows.Count != 0)
            {
                DropDownList5.SelectedValue = s_det1.Rows[0]["mov_brw_staff_id"].ToString();
                disp_pegawai();
                //gd_loc();

                DropDownList1.SelectedValue = s_det1.Rows[0]["mov_brw_location_cd"].ToString();
                TextBox15.Text = s_det1.Rows[0]["mov_brw_purpose"].ToString();

                TextBox16.Text = s_det1.Rows[0]["mov_brw_start_dt"].ToString();
                TextBox18.Text = s_det1.Rows[0]["mov_brw_start_time"].ToString();
                txtarea1.Value = s_det1.Rows[0]["mov_brw_remark"].ToString();


                if (s_det1.Rows[0]["mov_verify_cond_ind"].ToString() == "1")
                {
                    ch1.Checked = true;
                }
                else if (s_det1.Rows[0]["mov_verify_cond_ind"].ToString() == "2")
                {
                    ch2.Checked = true;
                }
                else if (s_det1.Rows[0]["mov_verify_cond_ind"].ToString() == "3")
                {
                    ch3.Checked = true;
                }
                Textarea1.Value = s_det1.Rows[0]["mov_verify_remark"].ToString();
                TextBox9.Text = DropDownList1.SelectedItem.Text;
                //TextBox4.Text = s_det1.Rows[0]["mov_brw_qty"].ToString();

                //TextBox5.Text = (double.Parse(TextBox4.Text) - double.Parse(TextBox6.Text)).ToString();
            }
        }
    }

    protected void nam_peg(object sender, EventArgs e)
    {
        disp_pegawai();
    }

    void disp_pegawai()
    {
        DataTable s_det1 = new DataTable();
        s_det1 = DBCon.Ora_Execute_table("select stf_staff_no,stf_name,stf_curr_post_cd,stf_curr_dept_cd,str_curr_org_cd,rj.hr_jaw_desc,og.org_name,jb.hr_jaba_desc,lo.loc_cd from hr_staff_profile as sp left join Ref_hr_Jawatan rj on rj.hr_jaw_Code=sp.stf_curr_post_cd left join hr_organization og on og.org_gen_id=sp.str_curr_org_cd left join ref_location as lo on lo.org_id=og.org_id left join Ref_hr_jabatan as jb on jb.hr_jaba_Code=sp.stf_curr_dept_cd where stf_staff_no='" + DropDownList5.SelectedValue + "'");
        if (s_det1.Rows.Count != 0)
        {
            TextBox12.Text = s_det1.Rows[0]["org_name"].ToString().ToUpper();
            TextBox13.Text = s_det1.Rows[0]["hr_jaw_desc"].ToString().ToUpper();
            TextBox14.Text = s_det1.Rows[0]["hr_jaba_desc"].ToString().ToUpper();
            TextBox29.Text = s_det1.Rows[0]["str_curr_org_cd"].ToString();
            TextBox30.Text = s_det1.Rows[0]["stf_curr_post_cd"].ToString();
            TextBox31.Text = s_det1.Rows[0]["stf_curr_dept_cd"].ToString();


            if (s_det1.Rows[0]["str_curr_org_cd"].ToString() != "")
            {
                string com = "select loc_cd,UPPER(loc_desc) as loc_desc from ref_location where org_gen_id='" + s_det1.Rows[0]["str_curr_org_cd"].ToString() + "'";
                SqlDataAdapter adpt = new SqlDataAdapter(com, con);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                DropDownList1.DataSource = dt;
                DropDownList1.DataBind();
                DropDownList1.DataTextField = "loc_desc";
                DropDownList1.DataValueField = "loc_cd";
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            }
            DropDownList1.SelectedValue = s_det1.Rows[0]["loc_cd"].ToString();
        }
    }

    protected void clk_simpan(object sender, EventArgs e)
    {
        try
        {
            if (DropDownList2.SelectedValue != "")
            {
                string c_v1 = string.Empty, c_v2 = string.Empty;
                if (result_kat == "01")
                {
                    c_v1 = TextBox27.Text;
                }
                else
                {
                    c_v1 = "";
                }
                if (ch1.Checked == true)
                {
                    c_v2 = "1";
                }
                else if (ch2.Checked == true)
                {
                    c_v2 = "2";
                }
                else if (ch3.Checked == true)
                {
                    c_v2 = "3";
                }
                else
                {
                    c_v2 = "";
                }
                res();
                DataTable ddokdicno = new DataTable();
                ddokdicno = DBCon.Ora_Execute_table("select stf_staff_no From hr_staff_profile where stf_icno='" + Session["New"].ToString() + "' ");
                string d1 = string.Empty, d2 = string.Empty;
                if (TextBox16.Text != "")
                {
                    DateTime dt1 = DateTime.ParseExact(TextBox16.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    d1 = dt1.ToString("yyyy-MM-dd");
                }

                if (TextBox17.Text != "")
                {
                    DateTime dt2 = DateTime.ParseExact(TextBox17.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    d2 = dt2.ToString("yyyy-MM-dd");
                }

                if (DropDownList2.SelectedValue == "01")
                {
                    //DataTable rt = new DataTable();
                    //if (dd_kat.SelectedValue == "01")
                    //{
                    //    rt = DBCon.Ora_Execute_table("select  format(com_reg_dt,'yyyy-MM-dd hh:mm:ss') reg_dt  from ast_component   where com_asset_cat_cd='" + TextBox32.Text + "' and com_asset_sub_cat_cd='" + TextBox33.Text + "' and com_asset_type_cd='" + TextBox34.Text + "' and com_asset_cd='" + TextBox11.Text + "' and com_asset_id='" + TextBox27.Text + "'");
                    //}
                    //else if (dd_kat.SelectedValue == "02")
                    //{
                    //    rt = DBCon.Ora_Execute_table("select  format(car_reg_dt,'yyyy/MM/dd hh:mm:ss') reg_dt  from ast_car   where car_asset_cat_cd='" + TextBox32.Text + "' and car_asset_sub_cat_cd='" + TextBox33.Text + "' and car_asset_type_cd='" + TextBox34.Text + "' and car_asset_cd='" + TextBox11.Text + "' and car_plate_no='" + TextBox27.Text + "'");     
                    //}
                    //else if (dd_kat.SelectedValue == "03")
                    //{
                    //    rt = DBCon.Ora_Execute_table("select  format(inv_reg_dt,'yyyy/MM/dd hh:mm:ss')  reg_dt  from ast_inventory   where inv_asset_cat_cd='" + TextBox32.Text + "' and inv_asset_sub_cat_cd='" + TextBox33.Text + "' and inv_asset_type_cd='" + TextBox34.Text + "' and inv_asset_cd='" + TextBox11.Text + "'");     
                    //}
                    //else if (dd_kat.SelectedValue == "04")
                    //{
                    //    rt = DBCon.Ora_Execute_table("select  format(pro_reg_dt,'yyyy/MM/dd hh:mm:ss')  reg_dt  from ast_property   where pro_asset_cat_cd='" + TextBox32.Text + "' and pro_asset_sub_cat_cd='" + TextBox33.Text + "' and pro_asset_type_cd='" + TextBox34.Text + "' and pro_asset_cd='" + TextBox11.Text + "' and pro_ownership_no='" + TextBox27.Text + "'");
                    //}
                    DBCon.Ora_Execute_table("INSERT INTO ast_borrow_txn (mov_asset_cat_cd,mov_asset_sub_cat_cd,mov_asset_type_cd,mov_asset_cd,mov_reg_dt,mov_asset_id,mov_type_cd,mov_brw_qty,mov_brw_staff_id,mov_brw_location_cd,mov_brw_purpose,mov_brw_start_dt,mov_brw_start_time,mov_brw_remark,mov_verify_id,mov_verify_dt,mov_verify_cond_ind,mov_verify_remark,mov_crt_id,mov_crt_dt) VALUES ('" + TextBox32.Text + "','" + TextBox33.Text + "','" + TextBox34.Text + "','" + TextBox11.Text + "','" + TextBox24.Text + "','" + TextBox3.Text + "','" + DropDownList2.SelectedValue + "','1','" + DropDownList5.SelectedValue + "','" + DropDownList1.SelectedValue + "','" + TextBox15.Text + "','" + d1 + "','" + TextBox18.Text + "','" + txtarea1.Value.Replace("'", "''") + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + c_v2 + "','" + Textarea1.Value.Replace("'", "''") + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");

                    if (result_kat == "01")
                    {
                        //DBCon.Ora_Execute_table("UPDATE ast_component set com_borrow_qty='" + TextBox4.Text + "',com_allocate_staff_no='" + ddokdicno.Rows[0]["stf_staff_no"].ToString() + "',com_upd_id='" + Session["New"].ToString() + "',com_upd_dt='" + DateTime.Now + "' where com_asset_id = '" + TextBox27.Text + "'");
                        DBCon.Ora_Execute_table("UPDATE ast_component set com_borrow_qty='1',com_allocate_staff_no='" + DropDownList5.SelectedValue + "',com_upd_id='" + Session["New"].ToString() + "',com_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where com_asset_id = '" + TextBox27.Text + "'");
                    }
                    else if (result_kat == "02")
                    {
                        DBCon.Ora_Execute_table("UPDATE ast_car set car_allocate_staff_no='" + DropDownList5.SelectedValue + "',car_upd_id='" + Session["New"].ToString() + "',car_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where car_asset_id = '" + TextBox27.Text + "'");
                    }

                }
                else if (DropDownList2.SelectedValue == "02")
                {
                    DataTable aims = new DataTable();
                    aims = DBCon.Ora_Execute_table("select * From ast_borrow_txn where mov_asset_cat_cd = '" + TextBox32.Text + "' and mov_asset_sub_cat_cd='" + TextBox33.Text + "' and mov_asset_type_cd='" + TextBox34.Text + "' and mov_asset_cd='" + TextBox11.Text + "' and mov_reg_dt='" + TextBox24.Text + "'");
                    if (aims.Rows.Count != 0)
                    {
                        DBCon.Ora_Execute_table("UPDATE ast_borrow_txn set mov_type_cd='"+ DropDownList2.SelectedValue +"',mov_brw_end_dt='" + d2 + "',mov_brw_end_time='" + TextBox19.Text + "',mov_brw_remark='" + txtarea1.Value.Replace("'", "''") + "',mov_brw_actual_return_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',mov_return_id='" + Session["New"].ToString() + "',mov_return_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',mov_return_cond_ind='" + c_v2 + "',mov_return_remark='" + Textarea1.Value.Replace("'", "''") + "',mov_upd_id='" + Session["New"].ToString() + "',mov_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where mov_asset_cat_cd = '" + TextBox32.Text + "' and mov_asset_sub_cat_cd='" + TextBox33.Text + "' and mov_asset_type_cd='" + TextBox34.Text + "' and mov_asset_cd='" + TextBox11.Text + "' and mov_reg_dt='" + TextBox24.Text + "'");
                        if (result_kat == "01")
                        {
                            DBCon.Ora_Execute_table("UPDATE ast_component set com_borrow_qty='0',com_allocate_staff_no='',com_upd_id='" + Session["New"].ToString() + "',com_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where com_asset_id = '" + TextBox27.Text + "'");
                        }
                        else if (result_kat == "02")
                        {
                            DBCon.Ora_Execute_table("UPDATE ast_car set car_allocate_staff_no='',car_upd_id='" + Session["New"].ToString() + "',car_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where car_asset_id = '" + TextBox27.Text + "'");
                        }
                    }
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Please Select Jenis Pergerakan : Peminjaman.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    //}
                }
                else if (DropDownList2.SelectedValue == "03")
                {
                    DBCon.Ora_Execute_table("UPDATE ast_staff_asset set sas_staff_no='" + DropDownList5.SelectedValue + "',sas_staff_name='" + DropDownList5.SelectedItem.Text + "',sas_allocate_dt='" + d1 + "',sas_location_cd='" + DropDownList1.SelectedValue + "',sas_upd_id='" + Session["New"].ToString() + "',sas_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where sas_asset_id='" + TextBox27.Text + "'");
                }

                Session["validate_success"] = "SUCCESS";
                Session["alrt_msg"] = "Rekod " + DropDownList2.SelectedItem.Text + " Berjaya Disimpan.";
                Response.Redirect("../Aset/Ast_pergerakan_view.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Jenis Pergerakan : Peminjaman.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        catch
        {

        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_pergerakan.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_pergerakan_view.aspx");
    }

    
}