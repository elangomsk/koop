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

public partial class Ast_pemeriksaan : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();

    DataTable dt = new DataTable();
    string Status = string.Empty;
    string level, userid, stscd, abc1;
    string val1 = string.Empty, val2 = string.Empty, val3 = string.Empty;
    string clmfd = string.Empty, clm_name = string.Empty, cur_qry = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2()})";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;
                kategori();
                Orgnasi();
                grid();
                //LOKASI();
                this.TextBox3.Text = DateTime.Today.ToString("dd/MM/yyyy");
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
        try
        {
            
            
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void Orgnasi()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(cs);
            string com = "select org_name,org_gen_id From hr_organization order by org_name asc";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_ORGNSI.DataSource = dt;
            DD_ORGNSI.DataTextField = "org_name";
            DD_ORGNSI.DataValueField = "org_gen_id";
            DD_ORGNSI.DataBind();
            DD_ORGNSI.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void kategori()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select ast_kategori_code,ast_kategori_desc from Ref_ast_kategori";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_kat.DataSource = dt;
            dd_kat.DataTextField = "ast_kategori_desc";
            dd_kat.DataValueField = "ast_kategori_code";
            dd_kat.DataBind();
            dd_kat.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select ast_subkateast_Code,ast_subkateast_desc from Ref_ast_sub_kategri_Aset where Status='A' and ast_kategori_Code='" + dd_kat.SelectedValue + "'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_skat.DataSource = dt;
            dd_skat.DataTextField = "ast_subkateast_desc";
            dd_skat.DataValueField = "ast_subkateast_Code";
            dd_skat.DataBind();
            dd_skat.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            grid();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void sub_kategori()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select ast_subkateast_Code,ast_subkateast_desc from Ref_ast_sub_kategri_Aset where Status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_skat.DataSource = dt;
            dd_skat.DataTextField = "ast_subkateast_desc";
            dd_skat.DataValueField = "ast_subkateast_Code";
            dd_skat.DataBind();
            dd_skat.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //void asetcode()
    //{
    //    DataSet Ds = new DataSet();
    //    try
    //    {
    //        string com = "select ast_kodast_Code,ast_kodast_desc from Ref_ast_kod_aset where Status='A'";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
    //        DataTable dt = new DataTable();
    //        adpt.Fill(dt);
    //        dd_namaset.DataSource = dt;
    //        dd_namaset.DataTextField = "ast_kodast_desc";
    //        dd_namaset.DataValueField = "ast_kodast_Code";
    //        dd_namaset.DataBind();
    //        dd_namaset.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //void jenisaset()
    //{
    //    DataSet Ds = new DataSet();
    //    try
    //    {
    //        string com = "select ast_jeniaset_Code,ast_jeniaset_desc From Ref_ast_jenis_aset where Status='A'";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
    //        DataTable dt = new DataTable();
    //        adpt.Fill(dt);
    //        dd_jenis.DataSource = dt;
    //        dd_jenis.DataTextField = "ast_jeniaset_desc";
    //        dd_jenis.DataValueField = "ast_jeniaset_Code";
    //        dd_jenis.DataBind();
    //        dd_jenis.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}


    void clear()
    {
        dd_kat.SelectedValue = "";
        dd_skat.SelectedValue = "";
        dd_jenis.SelectedValue = "";
        TextBox1.Text = "";
        DD_ORGNSI.SelectedValue = "";
        DD_lokasi.SelectedValue = "";
        TextBox2.Text = "";
        tb1.Text = "";
    }
    protected void OnSelectedIndexChanged1(object sender, EventArgs e)
    {
        jenaset();
        grid();
    }

    void jenaset()
    {
        DataSet Ds1 = new DataSet();
        try
        {
            string com1 = "select * from Ref_ast_jenis_aset where Status='A' and ast_cat_Code='" + dd_kat.SelectedValue + "' and ast_sub_cat_Code = '" + dd_skat.SelectedValue + "'";
            SqlDataAdapter adpt = new SqlDataAdapter(com1, con);
            DataTable dt1 = new DataTable();
            adpt.Fill(dt1);
            dd_jenis.DataSource = dt1;
            dd_jenis.DataTextField = "ast_jeniaset_desc";
            dd_jenis.DataValueField = "ast_jeniaset_Code";
            dd_jenis.DataBind();
            dd_jenis.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
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
    //        string com1 = "select * from ast_cmn_asset where cas_asset_cat_cd='" + dd_kat.SelectedValue + "' and cas_asset_sub_cat_cd='" + dd_skat.SelectedValue + "' and cas_asset_type_cd='" + dd_jenis.SelectedValue + "'";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com1, con);
    //        DataTable dt1 = new DataTable();
    //        adpt.Fill(dt1);
    //        dd_namaset.DataSource = dt1;
    //        dd_namaset.DataTextField = "cas_asset_desc";
    //        dd_namaset.DataValueField = "cas_asset_cd";
    //        dd_namaset.DataBind();
    //        dd_namaset.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //void grid()
    //{
    //    con.Open();
    //    DataTable ddicno = new DataTable();
    //    SqlCommand cmd = new SqlCommand("select org_name,hr_jaba_desc,ast_kategori_desc,ast_subkateast_desc,ast_jeniaset_desc,isnull(sas_cond_sts_cd,'') as sas_cond_sts_cd,isnull(sas_justify_cd,'') sas_justify_cd,isnull(sas_curr_price_amt,'') sas_curr_price_amt,isnull(sas_repair_amt,'') sas_repair_amt from ast_staff_asset SA Left join hr_staff_profile as SF on SF.stf_staff_no=SA.sas_staff_no left join hr_organization ORG on ORG.org_id=SA.sas_org_id left join Ref_hr_jabatan JB on JB.hr_jaba_Code=SF.stf_curr_dept_cd left join Ref_ast_kod_aset AK on  AK.ast_kodast_Code=SA.sas_asset_cat_cd left join Ref_ast_sub_kategri_Aset SKA on SKA.ast_subkateast_Code=SA.sas_asset_sub_cat_cd left join Ref_ast_jenis_aset AJA on AJA.ast_jeniaset_Code=SA.sas_asset_type_cd where stf_icno='" + Session["New"].ToString() + "'", con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count == 0)
    //    {
    //        ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
    //        GridView1.DataSource = ds;
    //        GridView1.DataBind();
    //        int columncount = GridView1.Rows[0].Cells.Count;
    //        GridView1.Rows[0].Cells.Clear();
    //        GridView1.Rows[0].Cells.Add(new TableCell());
    //        GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
    //        GridView1.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
    //    }
    //    else
    //    {
    //        GridView1.DataSource = ds;
    //        GridView1.DataBind();
    //    }
    //    con.Close();
    //}
    protected void RB11_CheckedChanged(object sender, EventArgs e)
    {
        if (TJ31.Checked == true)
        {
            TJ32.Checked = false;
            TJ33.Checked = false;
            rcorners1.Visible = true;
            rcorners2.Visible = false;
            rcorners3.Visible = false;
        }
        clear();
        grid();
    }
    protected void RB12_CheckedChanged(object sender, EventArgs e)
    {
        if (TJ32.Checked == true)
        {
            TJ31.Checked = false;
            TJ33.Checked = false;
            rcorners1.Visible = false;
            rcorners2.Visible = true;
            rcorners3.Visible = false;
        }
        clear();
        grid();
    }
    protected void RB13_CheckedChanged(object sender, EventArgs e)
    {
        if (TJ33.Checked == true)
        {
            TJ31.Checked = false;
            TJ32.Checked = false;
            rcorners1.Visible = false;
            rcorners2.Visible = false;
            rcorners3.Visible = true;
        }
        clear();
        grid();
    }
    //protected void lnkView_Click(object sender, EventArgs e)
    //{        

    //}
    protected void btn_reset(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void cal_lokasi(object sender, EventArgs e)
    {
        LOKASI();
        grid();
    }

    void LOKASI()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(cs);
            string com = "SELECT loc_cd,loc_desc FROM ref_location where org_gen_id='" + DD_ORGNSI.SelectedValue + "' ORDER BY loc_desc ASC";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_lokasi.DataSource = dt;
            DD_lokasi.DataBind();
            DD_lokasi.DataTextField = "loc_desc";
            DD_lokasi.DataValueField = "loc_cd";
            DD_lokasi.DataBind();
            DD_lokasi.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
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
                sda.SelectCommand = cmd;
                using (DataSet ds = new DataSet())
                {
                    sda.Fill(ds);
                    return ds;
                }
            }
        }
    }
    protected void BindGrid()
    {
        GridView1.DataSource = ViewState["dt"] as DataTable;
        GridView1.DataBind();
    }
    protected void OnRowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        this.BindGrid();
    }
    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Find the DropDownList in the Row
            DropDownList ddl_sts = (e.Row.FindControl("ddl_sts") as DropDownList);

            ddl_sts.DataSource = GetData("select ast_pemerikksaan_desc,ast_pemerikksaan_code from Ref_ast_sts_pemerikksaan where status='A'");
            ddl_sts.DataTextField = "ast_pemerikksaan_desc";
            ddl_sts.DataValueField = "ast_pemerikksaan_code";
            ddl_sts.DataBind();

            //Add Default Item in the DropDownList
            ddl_sts.SelectedValue = ((DataRowView)e.Row.DataItem)["sas_cond_sts_cd"].ToString();
            ddl_sts.Items.Insert(0, new ListItem("--- PILIH ---"));
            TextBox4.Text = ddl_sts.SelectedValue;


            //Find the DropDownList in the Row
            DropDownList ddl_justy = (e.Row.FindControl("ddl_justy") as DropDownList);
            ddl_justy.DataSource = GetData("select ast_justifikasi_desc,ast_justifikasi_code from Ref_ast_justifikasi where status='A'");
            ddl_justy.DataTextField = "ast_justifikasi_desc";
            ddl_justy.DataValueField = "ast_justifikasi_code";
            ddl_justy.DataBind();

            //Add Default Item in the DropDownList
            ddl_justy.SelectedValue = ((DataRowView)e.Row.DataItem)["sas_justify_cd"].ToString();
            ddl_justy.Items.Insert(0, new ListItem("--- PILIH ---"));
            TextBox5.Text = ddl_justy.SelectedValue;

        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        userid = Session["New"].ToString();
        string d1 = string.Empty;
        using (SqlConnection con = new SqlConnection(cs))
        {
            con.Open();
            if (GridView1.Rows.Count != 0)
            {

                foreach (GridViewRow gvRow in GridView1.Rows)
                {
                    DropDownList GRID_DDL1 = (DropDownList)gvRow.FindControl("ddl_sts");
                    string cd3 = GRID_DDL1.SelectedValue;
                    DropDownList GRID_DDL2 = (DropDownList)gvRow.FindControl("ddl_justy");
                    string cd2 = GRID_DDL2.SelectedValue;
                    System.Web.UI.WebControls.TextBox box1 = (System.Web.UI.WebControls.TextBox)gvRow.FindControl("TXT_HS");
                    string tbox1 = box1.Text;
                    System.Web.UI.WebControls.TextBox box2 = (System.Web.UI.WebControls.TextBox)gvRow.FindControl("TXT_KB");
                    string tbox2 = box2.Text;
                    DateTime dt1 = DateTime.ParseExact(TextBox3.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    d1 = dt1.ToString("yyyy-MM-dd");
                    System.Web.UI.WebControls.Label st_no = (System.Web.UI.WebControls.Label)gvRow.FindControl("Label4");
                    System.Web.UI.WebControls.Label as_id = (System.Web.UI.WebControls.Label)gvRow.FindControl("Label5");

                    DBCon.Ora_Execute_table("update ast_staff_asset set sas_cond_sts_cd='" + cd3 + "' , sas_justify_cd='" + cd2 + "' , sas_curr_price_amt=' " + tbox1 + " ',sas_repair_amt='" + tbox2 + "',sas_inspect_dt='" + d1 + "',sas_inspect_id='" + Session["New"].ToString() + "',sas_upd_id='" + Session["New"].ToString() + "',sas_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where sas_staff_no='" + st_no.Text + "' and sas_asset_id='" + as_id.Text + "'");

                }

                Session["validate_success"] = "SUCCESS";
                Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                Response.Redirect("../Aset/Ast_pemeriksaan_view.aspx");
                //Response.Redirect(Request.RawUrl);
            }
            else
            {
                grid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
            con.Close();
        }
    }

    protected void clk_rst(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        if (TJ31.Checked == true && TJ32.Checked == false && TJ33.Checked == false)
        {
            grid();
        }
        else if (TJ31.Checked == false && TJ32.Checked == true && TJ33.Checked == false)
        {
            grid();
        }
        else if (TJ31.Checked == false && TJ32.Checked == false && TJ33.Checked == true)
        {
            grid();
        }
    }

    protected void gv_refdata_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        grid();
    }

    void grid()
    {
        sel_clmflds();
        if (TJ31.Checked == true)
        {
            cur_qry = "select org_gen_id,sas_staff_no,sas_asset_cat_cd,sas_asset_sub_cat_cd,sas_asset_type_cd,sas_asset_cd,sas_asset_id,org_name,hr_jaba_desc,ast_kategori_desc,ast_subkateast_desc,ast_jeniaset_desc, isnull(sas_cond_sts_cd,'') sas_cond_sts_cd , isnull(sas_justify_cd,'') sas_justify_cd,isnull(sas_curr_price_amt,'') sas_curr_price_amt,isnull(sas_repair_amt,'') sas_repair_amt,ISNULL(ast_pemerikksaan_desc,'') ast_pemerikksaan_desc,ISNULL(ast_justifikasi_desc,'') ast_justifikasi_desc from ast_staff_asset SA Left join hr_staff_profile as SF on SF.stf_staff_no=SA.sas_staff_no left join hr_organization ORG on ORG.org_gen_id=SA.sas_org_id left join Ref_hr_jabatan JB on JB.hr_jaba_Code=SF.stf_curr_dept_cd left join Ref_ast_kategori AK on  AK.ast_kategori_code=SA.sas_asset_cat_cd left join Ref_ast_sub_kategri_Aset SKA on SKA.ast_subkateast_Code=SA.sas_asset_sub_cat_cd left join Ref_ast_jenis_aset AJA on AJA.ast_jeniaset_Code=SA.sas_asset_type_cd and AJA.ast_sub_cat_Code=SA.sas_asset_sub_cat_cd left join Ref_ast_sts_pemerikksaan PE on PE.ast_pemerikksaan_code=SA.sas_cond_sts_cd Left join Ref_ast_justifikasi JU on JU.ast_justifikasi_code=SA.sas_justify_cd where SA.flag_set ='0' and " + clmfd + "";

        }
        else if (TJ32.Checked == true)
        {
            cur_qry = "select org_gen_id,sas_staff_no,sas_asset_cat_cd,sas_asset_sub_cat_cd,sas_asset_type_cd,sas_asset_cd,sas_asset_id,org_name,hr_jaba_desc,ast_kategori_desc,ast_subkateast_desc,ast_jeniaset_desc, isnull(sas_cond_sts_cd,'') sas_cond_sts_cd , isnull(sas_justify_cd,'') sas_justify_cd,isnull(sas_curr_price_amt,'') sas_curr_price_amt,isnull(sas_repair_amt,'') sas_repair_amt,ISNULL(ast_pemerikksaan_desc,'') ast_pemerikksaan_desc,ISNULL(ast_justifikasi_desc,'') ast_justifikasi_desc from ast_staff_asset SA Left join hr_staff_profile as SF on SF.stf_staff_no=SA.sas_staff_no left join hr_organization ORG on ORG.org_gen_id=SA.sas_org_id left join Ref_hr_jabatan JB on JB.hr_jaba_Code=SF.stf_curr_dept_cd left join Ref_ast_kategori AK on  AK.ast_kategori_code=SA.sas_asset_cat_cd left join Ref_ast_sub_kategri_Aset SKA on SKA.ast_subkateast_Code=SA.sas_asset_sub_cat_cd left join Ref_ast_jenis_aset AJA on AJA.ast_jeniaset_Code=SA.sas_asset_type_cd and AJA.ast_sub_cat_Code=SA.sas_asset_sub_cat_cd left join Ref_ast_sts_pemerikksaan PE on PE.ast_pemerikksaan_code=SA.sas_cond_sts_cd Left join Ref_ast_justifikasi JU on JU.ast_justifikasi_code=SA.sas_justify_cd where SA.flag_set ='0' and " + clmfd + "";
        }
        else if (TJ33.Checked == true)
        {
            cur_qry = "select org_gen_id,sas_staff_no,sas_asset_cat_cd,sas_asset_sub_cat_cd,sas_asset_type_cd,sas_asset_cd,sas_asset_id,org_name,hr_jaba_desc,ast_kategori_desc,ast_subkateast_desc,ast_jeniaset_desc, isnull(sas_cond_sts_cd,'') sas_cond_sts_cd , isnull(sas_justify_cd,'') sas_justify_cd,isnull(sas_curr_price_amt,'') sas_curr_price_amt,isnull(sas_repair_amt,'') sas_repair_amt,ISNULL(ast_pemerikksaan_desc,'') ast_pemerikksaan_desc,ISNULL(ast_justifikasi_desc,'') ast_justifikasi_desc from ast_staff_asset SA Left join hr_staff_profile as SF on SF.stf_staff_no=SA.sas_staff_no left join hr_organization ORG on ORG.org_gen_id=SA.sas_org_id left join Ref_hr_jabatan JB on JB.hr_jaba_Code=SF.stf_curr_dept_cd left join Ref_ast_kategori AK on  AK.ast_kategori_code=SA.sas_asset_cat_cd left join Ref_ast_sub_kategri_Aset SKA on SKA.ast_subkateast_Code=SA.sas_asset_sub_cat_cd left join Ref_ast_jenis_aset AJA on AJA.ast_jeniaset_Code=SA.sas_asset_type_cd and AJA.ast_sub_cat_Code=SA.sas_asset_sub_cat_cd left join Ref_ast_sts_pemerikksaan PE on PE.ast_pemerikksaan_code=SA.sas_cond_sts_cd Left join Ref_ast_justifikasi JU on JU.ast_justifikasi_code=SA.sas_justify_cd where SA.flag_set ='0' and " + clmfd + "";
        }
        else
        {
            cur_qry = "select org_gen_id,sas_staff_no,sas_asset_cat_cd,sas_asset_sub_cat_cd,sas_asset_type_cd,sas_asset_cd,sas_asset_id,org_name,hr_jaba_desc,ast_kategori_desc,ast_subkateast_desc,ast_jeniaset_desc, isnull(sas_cond_sts_cd,'') sas_cond_sts_cd , isnull(sas_justify_cd,'') sas_justify_cd,isnull(sas_curr_price_amt,'') sas_curr_price_amt,isnull(sas_repair_amt,'') sas_repair_amt,ISNULL(ast_pemerikksaan_desc,'') ast_pemerikksaan_desc,ISNULL(ast_justifikasi_desc,'') ast_justifikasi_desc from ast_staff_asset SA Left join hr_staff_profile as SF on SF.stf_staff_no=SA.sas_staff_no left join hr_organization ORG on ORG.org_gen_id=SA.sas_org_id left join Ref_hr_jabatan JB on JB.hr_jaba_Code=SF.stf_curr_dept_cd left join Ref_ast_kategori AK on  AK.ast_kategori_code=SA.sas_asset_cat_cd left join Ref_ast_sub_kategri_Aset SKA on SKA.ast_subkateast_Code=SA.sas_asset_sub_cat_cd left join Ref_ast_jenis_aset AJA on AJA.ast_jeniaset_Code=SA.sas_asset_type_cd and AJA.ast_sub_cat_Code=SA.sas_asset_sub_cat_cd left join Ref_ast_sts_pemerikksaan PE on PE.ast_pemerikksaan_code=SA.sas_cond_sts_cd Left join Ref_ast_justifikasi JU on JU.ast_justifikasi_code=SA.sas_justify_cd where SA.flag_set ='0' and sas_staff_name=''";
        }
        con.Open();
        DataTable ddicno = new DataTable();
        SqlCommand cmd = new SqlCommand("" + cur_qry + "", con);
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
        }
        else
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        con.Close();
    }

    void sel_clmflds()
    {
        clm_name = "sas";

        if (TJ31.Checked == true)
        {
            if (dd_kat.SelectedValue != "" && dd_skat.SelectedValue != "" && dd_jenis.SelectedValue != "" && TextBox1.Text != "")
            {
                clmfd = "" + clm_name + "_asset_cat_cd='" + dd_kat.SelectedValue + "' and " + clm_name + "_asset_sub_cat_cd='" + dd_skat.SelectedValue + "' and " + clm_name + "_asset_type_cd='" + dd_jenis.SelectedValue + "' and " + clm_name + "_asset_id='" + TextBox1.Text + "'";
            }
            else if (dd_kat.SelectedValue != "" && dd_skat.SelectedValue == "" && dd_jenis.SelectedValue == "" && TextBox1.Text == "")
            {
                clmfd = "" + clm_name + "_asset_cat_cd='" + dd_kat.SelectedValue + "'";
            }
            else if (dd_kat.SelectedValue != "" && dd_skat.SelectedValue != "" && dd_jenis.SelectedValue == "" && TextBox1.Text == "")
            {
                clmfd = "" + clm_name + "_asset_cat_cd='" + dd_kat.SelectedValue + "' and " + clm_name + "_asset_sub_cat_cd='" + dd_skat.SelectedValue + "'";
            }
            else if (dd_kat.SelectedValue != "" && dd_skat.SelectedValue != "" && dd_jenis.SelectedValue != "" && TextBox1.Text == "")
            {
                clmfd = "" + clm_name + "_asset_cat_cd='" + dd_kat.SelectedValue + "' and " + clm_name + "_asset_sub_cat_cd='" + dd_skat.SelectedValue + "' and " + clm_name + "_asset_type_cd='" + dd_jenis.SelectedValue + "'";
            }
            else if (dd_kat.SelectedValue != "" && dd_skat.SelectedValue != "" && dd_jenis.SelectedValue != "" && TextBox1.Text == "")
            {
                clmfd = "" + clm_name + "_asset_cat_cd='" + dd_kat.SelectedValue + "' and " + clm_name + "_asset_sub_cat_cd='" + dd_skat.SelectedValue + "' and " + clm_name + "_asset_type_cd='" + dd_jenis.SelectedValue + "'";
            }
            else if (dd_kat.SelectedValue != "" && dd_skat.SelectedValue == "" && dd_jenis.SelectedValue == "" && TextBox1.Text != "")
            {
                clmfd = "" + clm_name + "_asset_cat_cd='" + dd_kat.SelectedValue + "' and " + clm_name + "_asset_id='" + TextBox1.Text + "'";
            }
            else if (dd_kat.SelectedValue != "" && dd_skat.SelectedValue != "" && dd_jenis.SelectedValue == "" && TextBox1.Text != "")
            {
                clmfd = "" + clm_name + "_asset_cat_cd='" + dd_kat.SelectedValue + "' and " + clm_name + "_asset_sub_cat_cd='" + dd_skat.SelectedValue + "' and " + clm_name + "_asset_id='" + TextBox1.Text + "'";
            }
            else if (dd_kat.SelectedValue != "" && dd_skat.SelectedValue != "" && dd_jenis.SelectedValue != "" && TextBox1.Text != "")
            {
                clmfd = "" + clm_name + "_asset_cat_cd='" + dd_kat.SelectedValue + "' and " + clm_name + "_asset_sub_cat_cd='" + dd_skat.SelectedValue + "' and " + clm_name + "_asset_type_cd='" + dd_jenis.SelectedValue + "' and " + clm_name + "_asset_id='" + TextBox1.Text + "'";
            }
            else if (dd_kat.SelectedValue == "" && dd_skat.SelectedValue == "" && dd_jenis.SelectedValue == "" && TextBox1.Text != "")
            {
                clmfd = "" + clm_name + "_asset_id='" + TextBox1.Text + "'";
            }
            else
            {
                clmfd = "" + clm_name + "_asset_cat_cd=''";
            }
        }
        else if (TJ32.Checked == true)
        {
            if (DD_ORGNSI.SelectedValue != "" && DD_lokasi.SelectedValue != "")
            {
                clmfd = "sas_org_id ='" + DD_ORGNSI.SelectedValue + "' and sas_location_cd='" + DD_lokasi.SelectedValue + "'";
            }
            else if (DD_ORGNSI.SelectedValue != "" && DD_lokasi.SelectedValue == "")
            {
                clmfd = "sas_org_id ='" + DD_ORGNSI.SelectedValue + "'";
            }
            else if (DD_ORGNSI.SelectedValue == "" && DD_lokasi.SelectedValue != "")
            {
                clmfd = "sas_location_cd ='" + DD_lokasi.SelectedValue + "'";
            }
            else
            {
                clmfd = "sas_org_id =''";
            }

        }
        else if (TJ33.Checked == true)
        {
            if (TextBox2.Text != "" && tb1.Text != "")
            {
                clmfd = "sas_staff_no ='" + TextBox2.Text + "' and sas_staff_name ='" + tb1.Text + "'";
            }
            else if (TextBox2.Text != "" && tb1.Text == "")
            {
                clmfd = "sas_staff_no ='" + TextBox2.Text + "'";
            }
            else if (TextBox2.Text == "" && tb1.Text != "")
            {
                clmfd = "sas_staff_name ='" + tb1.Text + "'";
            }
            else
            {
                clmfd = "sas_staff_name =''";
            }
        }

    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_pemeriksaan.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_pemeriksaan_view.aspx");
    }

    
}