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

public partial class ast_report_mengikut : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection dbcon = new DBConnection();
    StudentWebService service = new StudentWebService();

    string useid = string.Empty;
    string Status = string.Empty;
    string oid = string.Empty;
    string sdd = string.Empty, st_qry = string.Empty;
    string clmfd = string.Empty, clm_name = string.Empty;
    string dt1 = string.Empty, dt2 = string.Empty;
    string vv1 = string.Empty, vv2 = string.Empty;
    string mqry = string.Empty;
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
            DD_Sub_Kateg.DataSource = dt;
            DD_Sub_Kateg.DataBind();
            DD_Sub_Kateg.DataTextField = "ast_subkateast_desc";
            DD_Sub_Kateg.DataValueField = "ast_subkateast_Code";
            DD_Sub_Kateg.DataBind();
            DD_Sub_Kateg.Items.Insert(0, new ListItem("--- PILIH ---", ""));
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
            string com1 = "select * from Ref_ast_jenis_aset where Status='A' and ast_cat_Code='" + DD_Kategori.SelectedValue + "' and ast_sub_cat_Code = '" + DD_Sub_Kateg.SelectedValue + "'";
            SqlDataAdapter adpt = new SqlDataAdapter(com1, con);
            DataTable dt1 = new DataTable();
            adpt.Fill(dt1);
            DD_Jenis_ast.DataSource = dt1;
            DD_Jenis_ast.DataTextField = "ast_jeniaset_desc";
            DD_Jenis_ast.DataValueField = "ast_jeniaset_Code";
            DD_Jenis_ast.DataBind();
            DD_Jenis_ast.Items.Insert(0, new ListItem("--- PILIH ---", ""));

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
            string com1 = "select * from ast_cmn_asset where cas_asset_cat_cd='" + DD_Kategori.SelectedValue + "' and cas_asset_sub_cat_cd='" + DD_Sub_Kateg.SelectedValue + "' and cas_asset_type_cd='" + DD_Jenis_ast.SelectedValue + "'";
            SqlDataAdapter adpt = new SqlDataAdapter(com1, con);
            DataTable dt1 = new DataTable();
            adpt.Fill(dt1);
            DD_NAMAAST.DataSource = dt1;
            DD_NAMAAST.DataTextField = "cas_asset_desc";
            DD_NAMAAST.DataValueField = "cas_asset_cd";
            DD_NAMAAST.DataBind();
            DD_NAMAAST.Items.Insert(0, new ListItem("--- PILIH ---", ""));
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
            DD_Jenis_ast.DataSource = dt;
            DD_Jenis_ast.DataBind();
            DD_Jenis_ast.DataTextField = "ast_jeniaset_desc";
            DD_Jenis_ast.DataValueField = "ast_jeniaset_Code";
            DD_Jenis_ast.DataBind();
            DD_Jenis_ast.Items.Insert(0, new ListItem("--- PILIH ---", ""));
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
            DD_organ.DataSource = dt;
            DD_organ.DataBind();
            DD_organ.DataTextField = "org_name";
            DD_organ.DataValueField = "org_gen_id";
            DD_organ.DataBind();
            DD_organ.Items.Insert(0, new ListItem("--- PILIH ---", ""));

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
   
    protected void Btn_Carian_Click(object sender, EventArgs e)
    {
        if (txt_dar.Text != "" && txt_seh.Text != "")
        {
            grid();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    void clk_jana()
    {
        if (txt_dar.Text != "" && txt_seh.Text != "")
        {
            DateTime pd = DateTime.ParseExact(txt_dar.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            dt1 = pd.ToString("yyyy-MM-dd");
            DateTime pd1 = DateTime.ParseExact(txt_seh.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            dt2 = pd1.ToString("yyyy-MM-dd");
        }

        if (txt_dar.Text != "" && txt_seh.Text != "" && DD_Kategori.SelectedValue != "" && DD_Sub_Kateg.SelectedValue != "" && DD_Jenis_ast.SelectedValue != "" && DD_NAMAAST.SelectedValue != "" && DD_organ.SelectedValue != "")
        {
            vv1 = "sas_reg_dt>=DATEADD(day, DATEDIFF(day, 0, '" + dt1 + "'), 0) and sas_reg_dt<=DATEADD(day, DATEDIFF(day, 0, '" + dt2 + "'), 0) and sas_asset_cat_cd='" + DD_Kategori.SelectedValue + "' and sas_asset_sub_cat_cd='" + DD_Sub_Kateg.SelectedValue + "' and sas_asset_type_cd='" + DD_Jenis_ast.SelectedValue + "' and sas_asset_cd ='" + DD_NAMAAST.SelectedValue + "' and sas_org_id='" + DD_organ.SelectedValue + "'";
        }
        else if (txt_dar.Text != "" && txt_seh.Text != "" && DD_Kategori.SelectedValue == "" && DD_Sub_Kateg.SelectedValue == "" && DD_Jenis_ast.SelectedValue == "" && DD_NAMAAST.SelectedValue == "" && DD_organ.SelectedValue != "")
        {
            vv1 = "sas_reg_dt>=DATEADD(day, DATEDIFF(day, 0, '" + dt1 + "'), 0) and sas_reg_dt<=DATEADD(day, DATEDIFF(day, 0, '" + dt2 + "'), 0) and sas_org_id='" + DD_organ.SelectedValue + "'";
        }
        else if (txt_dar.Text == "" && txt_seh.Text == "" && DD_Kategori.SelectedValue != "" && DD_Sub_Kateg.SelectedValue == "" && DD_Jenis_ast.SelectedValue == "" && DD_NAMAAST.SelectedValue == "" && DD_organ.SelectedValue != "")
        {
            vv1 = "sas_asset_cat_cd='" + DD_Kategori.SelectedValue + "' and sas_org_id='" + DD_organ.SelectedValue + "'";
        }
        else if (txt_dar.Text != "" && txt_seh.Text != "" && DD_Kategori.SelectedValue == "" && DD_Sub_Kateg.SelectedValue == "" && DD_Jenis_ast.SelectedValue == "" && DD_NAMAAST.SelectedValue == "" && DD_organ.SelectedValue == "")
        {
            vv1 = "sas_reg_dt>=DATEADD(day, DATEDIFF(day, 0, '" + dt1 + "'), 0) and sas_reg_dt<=DATEADD(day, DATEDIFF(day, 0, '" + dt2 + "'), 0)";
        }
        else if (txt_dar.Text == "" && txt_seh.Text == "" && DD_Kategori.SelectedValue != "" && DD_Sub_Kateg.SelectedValue == "" && DD_Jenis_ast.SelectedValue == "" && DD_NAMAAST.SelectedValue == "" && DD_organ.SelectedValue == "")
        {
            vv1 = "sas_asset_cat_cd='" + DD_Kategori.SelectedValue + "'";
        }
        else if (txt_dar.Text == "" && txt_seh.Text == "" && DD_Kategori.SelectedValue != "" && DD_Sub_Kateg.SelectedValue != "" && DD_Jenis_ast.SelectedValue == "" && DD_NAMAAST.SelectedValue == "" && DD_organ.SelectedValue == "")
        {
            vv1 = "sas_asset_cat_cd='" + DD_Kategori.SelectedValue + "' and sas_asset_sub_cat_cd='" + DD_Sub_Kateg.SelectedValue + "'";
        }
        else if (txt_dar.Text == "" && txt_seh.Text == "" && DD_Kategori.SelectedValue == "" && DD_Sub_Kateg.SelectedValue == "" && DD_Jenis_ast.SelectedValue != "" && DD_NAMAAST.SelectedValue == "" && DD_organ.SelectedValue == "")
        {
            vv1 = "sas_asset_type_cd='" + DD_Jenis_ast.SelectedValue + "'";
        }
        else if (txt_dar.Text == "" && txt_seh.Text == "" && DD_Kategori.SelectedValue == "" && DD_Sub_Kateg.SelectedValue == "" && DD_Jenis_ast.SelectedValue == "" && DD_NAMAAST.SelectedValue != "" && DD_organ.SelectedValue == "")
        {
            vv1 = "sas_asset_cd ='" + DD_NAMAAST.SelectedValue + "'";
        }
        else if (txt_dar.Text == "" && txt_seh.Text == "" && DD_Kategori.SelectedValue == "" && DD_Sub_Kateg.SelectedValue == "" && DD_Jenis_ast.SelectedValue == "" && DD_NAMAAST.SelectedValue == "" && DD_organ.SelectedValue != "")
        {
            vv1 = "sas_org_id='" + DD_organ.SelectedValue + "'";
        }
        else if (txt_dar.Text != "" && txt_seh.Text != "" && DD_Kategori.SelectedValue != "" && DD_Sub_Kateg.SelectedValue == "" && DD_Jenis_ast.SelectedValue == "" && DD_NAMAAST.SelectedValue == "" && DD_organ.SelectedValue == "")
        {
            vv1 = "sas_reg_dt>=DATEADD(day, DATEDIFF(day, 0, '" + dt1 + "'), 0) and sas_reg_dt<=DATEADD(day, DATEDIFF(day, 0, '" + dt2 + "'), +1) and sas_asset_cat_cd='" + DD_Kategori.SelectedValue + "'";
        }
        else if (txt_dar.Text != "" && txt_seh.Text != "" && DD_Kategori.SelectedValue != "" && DD_Sub_Kateg.SelectedValue != "" && DD_Jenis_ast.SelectedValue == "" && DD_NAMAAST.SelectedValue == "" && DD_organ.SelectedValue == "")
        {
            vv1 = "sas_reg_dt>=DATEADD(day, DATEDIFF(day, 0, '" + dt1 + "'), 0) and sas_reg_dt<=DATEADD(day, DATEDIFF(day, 0, '" + dt2 + "'), +1) and sas_asset_cat_cd='" + DD_Kategori.SelectedValue + "' and sas_asset_sub_cat_cd='" + DD_Sub_Kateg.SelectedValue + "'";
        }
        else if (txt_dar.Text == "" && txt_seh.Text == "" && DD_Kategori.SelectedValue != "" && DD_Sub_Kateg.SelectedValue != "" && DD_Jenis_ast.SelectedValue != "" && DD_NAMAAST.SelectedValue == "" && DD_organ.SelectedValue == "")
        {
            vv1 = "sas_asset_cat_cd='" + DD_Kategori.SelectedValue + "' and sas_asset_sub_cat_cd='" + DD_Sub_Kateg.SelectedValue + "' and sas_asset_type_cd='" + DD_Jenis_ast.SelectedValue + "'";
        }
        else if (txt_dar.Text == "" && txt_seh.Text == "" && DD_Kategori.SelectedValue == "" && DD_Sub_Kateg.SelectedValue == "" && DD_Jenis_ast.SelectedValue != "" && DD_NAMAAST.SelectedValue != "" && DD_organ.SelectedValue == "")
        {
            vv1 = "sas_asset_type_cd='" + DD_Jenis_ast.SelectedValue + "' and sas_asset_cd ='" + DD_NAMAAST.SelectedValue + "'";
        }
        else if (txt_dar.Text == "" && txt_seh.Text == "" && DD_Kategori.SelectedValue == "" && DD_Sub_Kateg.SelectedValue == "" && DD_Jenis_ast.SelectedValue == "" && DD_NAMAAST.SelectedValue != "" && DD_organ.SelectedValue != "")
        {
            vv1 = "sas_asset_cd ='" + DD_NAMAAST.SelectedValue + "' and sas_org_id='" + DD_organ.SelectedValue + "'";
        }
        else if (txt_dar.Text != "" && txt_seh.Text != "" && DD_Kategori.SelectedValue != "" && DD_Sub_Kateg.SelectedValue != "" && DD_Jenis_ast.SelectedValue != "" && DD_NAMAAST.SelectedValue == "" && DD_organ.SelectedValue == "")
        {
            vv1 = "sas_reg_dt>=DATEADD(day, DATEDIFF(day, 0, '" + dt1 + "'), 0) and sas_reg_dt<=DATEADD(day, DATEDIFF(day, 0, '" + dt2 + "'), +1) and sas_asset_cat_cd='" + DD_Kategori.SelectedValue + "' and sas_asset_sub_cat_cd='" + DD_Sub_Kateg.SelectedValue + "' and sas_asset_type_cd='" + DD_Jenis_ast.SelectedValue + "'";
        }
        else if (txt_dar.Text == "" && txt_seh.Text == "" && DD_Kategori.SelectedValue != "" && DD_Sub_Kateg.SelectedValue != "" && DD_Jenis_ast.SelectedValue != "" && DD_NAMAAST.SelectedValue != "" && DD_organ.SelectedValue == "")
        {
            vv1 = "sas_asset_cat_cd='" + DD_Kategori.SelectedValue + "' and sas_asset_sub_cat_cd='" + DD_Sub_Kateg.SelectedValue + "' and sas_asset_type_cd='" + DD_Jenis_ast.SelectedValue + "' and sas_asset_cd ='" + DD_NAMAAST.SelectedValue + "'";
        }
        else if (txt_dar.Text == "" && txt_seh.Text == "" && DD_Kategori.SelectedValue == "" && DD_Sub_Kateg.SelectedValue == "" && DD_Jenis_ast.SelectedValue != "" && DD_NAMAAST.SelectedValue != "" && DD_organ.SelectedValue != "")
        {
            vv1 = "sas_asset_type_cd='" + DD_Jenis_ast.SelectedValue + "' and sas_asset_cd ='" + DD_NAMAAST.SelectedValue + "' and sas_org_id='" + DD_organ.SelectedValue + "'";
        }
        else if (txt_dar.Text != "" && txt_seh.Text != "" && DD_Kategori.SelectedValue != "" && DD_Sub_Kateg.SelectedValue != "" && DD_Jenis_ast.SelectedValue != "" && DD_NAMAAST.SelectedValue != "" && DD_organ.SelectedValue == "")
        {
            vv1 = "sas_reg_dt>=DATEADD(day, DATEDIFF(day, 0, '" + dt1 + "'), 0) and sas_reg_dt<=DATEADD(day, DATEDIFF(day, 0, '" + dt2 + "'), +1) and sas_asset_cat_cd='" + DD_Kategori.SelectedValue + "' and sas_asset_sub_cat_cd='" + DD_Sub_Kateg.SelectedValue + "' and sas_asset_type_cd='" + DD_Jenis_ast.SelectedValue + "' and sas_asset_cd ='" + DD_NAMAAST.SelectedValue + "'";
        }
        else if (txt_dar.Text == "" && txt_seh.Text == "" && DD_Kategori.SelectedValue != "" && DD_Sub_Kateg.SelectedValue != "" && DD_Jenis_ast.SelectedValue != "" && DD_NAMAAST.SelectedValue != "" && DD_organ.SelectedValue != "")
        {
            vv1 = "sas_asset_cat_cd='" + DD_Kategori.SelectedValue + "' and sas_asset_sub_cat_cd='" + DD_Sub_Kateg.SelectedValue + "' and sas_asset_type_cd='" + DD_Jenis_ast.SelectedValue + "' and sas_asset_cd ='" + DD_NAMAAST.SelectedValue + "' and sas_org_id='" + DD_organ.SelectedValue + "'";
        }
        else if (txt_dar.Text != "" && txt_seh.Text != "" && DD_Kategori.SelectedValue != "" && DD_Sub_Kateg.SelectedValue == "" && DD_Jenis_ast.SelectedValue == "" && DD_NAMAAST.SelectedValue == "" && DD_organ.SelectedValue != "")
        {
            vv1 = "sas_reg_dt>=DATEADD(day, DATEDIFF(day, 0, '" + dt1 + "'), 0) and sas_reg_dt<=DATEADD(day, DATEDIFF(day, 0, '" + dt2 + "'), +1) and sas_asset_cat_cd='" + DD_Kategori.SelectedValue + "' and sas_org_id='" + DD_organ.SelectedValue + "'";
        }
        else
        {
            vv1 = "sas_asset_cat_cd='' and sas_asset_sub_cat_cd='' and sas_asset_type_cd='' and sas_asset_cd ='' and sas_org_id='' ";
        }
    }
    void grid()
    {
        clk_jana();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        dt = dbcon.Ora_Execute_table("select ISNULL(ho.org_name,'') as org_name,rak.ast_kategori_desc,rsk.ast_subkateast_desc,ja.ast_jeniaset_desc,ISNULL(b.amt1,'') as amt1 from (select sas_org_id,sas_asset_cat_cd,sas_asset_sub_cat_cd,sas_asset_type_cd from ast_staff_asset where " + vv1 + " group by sas_org_id,sas_asset_cat_cd,sas_asset_sub_cat_cd,sas_asset_type_cd) as asa left join Ref_ast_kategori as rak on rak.ast_kategori_code=asa.sas_asset_cat_cd left join Ref_ast_sub_kategri_Aset as rsk on rsk.ast_subkateast_Code=asa.sas_asset_sub_cat_cd left join Ref_ast_jenis_aset as ja on ja.ast_jeniaset_Code=asa.sas_asset_type_cd left join hr_organization as ho on ho.org_gen_id=asa.sas_org_id full outer join (select sas_org_id,sas_asset_cat_cd,sas_asset_sub_cat_cd,sas_asset_type_cd,sum(sas_purchase_amt) as amt1 from ast_staff_asset where " + vv1 + " group by sas_asset_cat_cd,sas_asset_sub_cat_cd,sas_asset_type_cd,sas_org_id) as b on b.sas_org_id=asa.sas_org_id and b.sas_asset_cat_cd=asa.sas_asset_cat_cd and b.sas_asset_sub_cat_cd=asa.sas_asset_sub_cat_cd and b.sas_asset_type_cd=asa.sas_asset_type_cd");
        RptviwerStudent.Reset();
        ds.Tables.Add(dt);

        List<DataRow> listResult = dt.AsEnumerable().ToList();
        listResult.Count();
        int countRow = 0;
        countRow = listResult.Count();

        string ss1 = string.Empty, ss2 = string.Empty, ss3 = string.Empty, ss4 = string.Empty, ss5 = string.Empty, ss6 = string.Empty;
        if (DD_Kategori.SelectedValue != "")
        {
            ss1 = DD_Kategori.SelectedItem.Text.ToUpper();
        }
        else
        {
            ss1 = "SEMUA";
        }

        if (DD_Sub_Kateg.SelectedValue != "")
        {
            ss2 = DD_Sub_Kateg.SelectedItem.Text.ToUpper();
        }
        else
        {
            ss2 = "SEMUA";
        }

        if (DD_Jenis_ast.SelectedValue != "")
        {
            ss3 = DD_Jenis_ast.SelectedItem.Text.ToUpper();
        }
        else
        {
            ss3 = "SEMUA";
        }

        if (DD_NAMAAST.SelectedValue != "")
        {
            ss4 = DD_NAMAAST.SelectedItem.Text.ToUpper();
        }
        else
        {
            ss4 = "SEMUA";
        }

        if (DD_organ.SelectedValue != "")
        {
            ss5 = DD_organ.SelectedItem.Text.ToUpper();
        }
        else
        {
            ss5 = "SEMUA";
        }

        RptviwerStudent.LocalReport.DataSources.Clear();
        if (countRow != 0)
        {
            disp_hdr_txt.Visible = true;

            RptviwerStudent.LocalReport.ReportPath = "Aset/ast_lapnamo.rdlc";
            ReportDataSource rds = new ReportDataSource("astlapnamo", dt);
            ReportParameter[] rptParams = new ReportParameter[]{
                     //new ReportParameter("fromDate",FromDate .Text ),
                     //new ReportParameter("toDate",ToDate .Text )
                     new ReportParameter("s7",txt_dar.Text  ),
                     new ReportParameter("s8",txt_seh.Text ),
                          new ReportParameter("s1",ss1 ),
                            new ReportParameter("s2",ss2 ),
                     new ReportParameter("s3",ss3 ),
                      new ReportParameter("s4",ss4 ),
                       new ReportParameter("s5",ss5 ),
                        new ReportParameter("s6",ss6 )
                     };


            RptviwerStudent.LocalReport.SetParameters(rptParams);
            RptviwerStudent.LocalReport.DataSources.Add(rds);
            RptviwerStudent.LocalReport.DisplayName = "LAPORAN_NILAI_ASET_MENGIKUT_ORGANISASI_" + DateTime.Now.ToString("ddMMyyyy");
            RptviwerStudent.LocalReport.Refresh();
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
        Response.Redirect("../Aset/ast_report_mengikut.aspx");
    }

 
    
}