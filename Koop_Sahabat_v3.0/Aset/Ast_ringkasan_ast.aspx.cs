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

public partial class Ast_ringkasan_ast : System.Web.UI.Page
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
        string script = " $(function () {$('.select2').select2()})";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;
                kat();
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
        DropDownList1.SelectedValue = "";
    }


    protected void OnSelectedIndexChanged1(object sender, EventArgs e)
    {
        jenaset();
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
    void dd_bind()
    {
        if (DD_Kategori.SelectedValue != "" || DD_Sub.SelectedValue != "" || DropDownList1.SelectedValue != "")
        {
            sel_clmflds();
            if (DD_Kategori.SelectedValue == "01")
            {
                //st_qry = "select com_asset_cat_cd,raka.ast_kategori_desc,com_asset_sub_cat_cd,rask.ast_subkateast_desc,com_asset_type_cd,raja.ast_jeniaset_desc,com_asset_cd,rak.cas_asset_desc, count(ISNULL(com_allocate_staff_no,'')) as strqty,com_price_amt as pamt,(count(ISNULL(com_allocate_staff_no,'')) *  com_price_amt) as tamt    from ast_component aa left join ast_staff_asset asa on asa.sas_asset_cat_cd=aa.com_asset_cat_cd and asa.sas_asset_sub_cat_cd=aa.com_asset_sub_cat_cd and asa.sas_asset_type_cd=aa.com_asset_type_cd and asa.sas_asset_cd=aa.com_asset_cd left join Ref_ast_kategori raka on raka.ast_kategori_code=aa.com_asset_cat_cd  left join Ref_ast_sub_kategri_Aset rask on rask.ast_subkateast_Code=aa.com_asset_sub_cat_cd left join Ref_ast_jenis_aset raja on raja.ast_jeniaset_Code=aa.com_asset_type_cd left join ast_cmn_asset rak on rak.cas_asset_cd=aa.com_asset_cd and rak.cas_asset_cat_cd = aa.com_asset_cat_cd and rak.cas_asset_sub_cat_cd = aa.com_asset_sub_cat_cd and rak.cas_asset_type_cd = aa.com_asset_type_cd  where " + clmfd + " group by com_asset_cat_cd,raka.ast_kategori_desc,com_asset_sub_cat_cd,rask.ast_subkateast_desc,com_asset_type_cd,raja.ast_jeniaset_desc,com_asset_cd,rak.cas_asset_desc,com_price_amt,asa.sas_qty";
                st_qry = "select com_asset_cat_cd,raka.ast_kategori_desc,com_asset_sub_cat_cd,rask.ast_subkateast_desc,com_asset_type_cd,raja.ast_jeniaset_desc,com_asset_cd,rak.cas_asset_desc,aa.strqty,pamt,tamt from (select com_asset_cat_cd,com_asset_sub_cat_cd,com_asset_type_cd,com_asset_cd,count(ISNULL(com_allocate_staff_no,'')) as strqty,com_price_amt as pamt,(count(ISNULL(com_allocate_staff_no,'')) *  com_price_amt) as tamt from ast_component where flag_set ='0' and " + clmfd + " group by com_asset_cat_cd,com_asset_sub_cat_cd,com_asset_type_cd,com_asset_cd,com_price_amt) as aa left join ast_staff_asset asa on asa.sas_asset_cat_cd=aa.com_asset_cat_cd and asa.sas_asset_sub_cat_cd=aa.com_asset_sub_cat_cd and asa.sas_asset_type_cd=aa.com_asset_type_cd and asa.sas_asset_cd=aa.com_asset_cd left join Ref_ast_kategori raka on raka.ast_kategori_code=aa.com_asset_cat_cd  left join Ref_ast_sub_kategri_Aset rask on rask.ast_subkateast_Code=aa.com_asset_sub_cat_cd left join Ref_ast_jenis_aset raja on raja.ast_jeniaset_Code=aa.com_asset_type_cd and raja.ast_sub_cat_Code=aa.com_asset_sub_cat_cd and raja.ast_cat_Code=aa.com_asset_cat_cd left join ast_cmn_asset rak on rak.cas_asset_cd=aa.com_asset_cd and rak.cas_asset_cat_cd = aa.com_asset_cat_cd and rak.cas_asset_sub_cat_cd = aa.com_asset_sub_cat_cd and rak.cas_asset_type_cd = aa.com_asset_type_cd  group by com_asset_cat_cd,raka.ast_kategori_desc,com_asset_sub_cat_cd,rask.ast_subkateast_desc,com_asset_type_cd,raja.ast_jeniaset_desc,com_asset_cd,rak.cas_asset_desc,aa.strqty,aa.pamt,aa.tamt";
            }
            else if (DD_Kategori.SelectedValue == "02")
            {
                st_qry = "select car_asset_cat_cd,raka.ast_kategori_desc,car_asset_sub_cat_cd,rask.ast_subkateast_desc,car_asset_type_cd,raja.ast_jeniaset_desc,car_asset_cd,rak.cas_asset_desc,count(ISNULL(car_allocate_staff_no,'')) as strqty,car_price_amt as pamt,(count(ISNULL(car_allocate_staff_no,'')) *  car_price_amt) as tamt   from ast_car aa left join ast_staff_asset asa on asa.sas_asset_cat_cd=aa.car_asset_cat_cd and asa.sas_asset_sub_cat_cd=aa.car_asset_sub_cat_cd and asa.sas_asset_type_cd=aa.car_asset_type_cd and asa.sas_asset_cd=aa.car_asset_cd left join Ref_ast_kategori raka on raka.ast_kategori_code=aa.car_asset_cat_cd  left join Ref_ast_sub_kategri_Aset rask on rask.ast_subkateast_Code=aa.car_asset_sub_cat_cd left join Ref_ast_jenis_aset raja on raja.ast_jeniaset_Code=aa.car_asset_type_cd and raja.ast_sub_cat_Code=aa.car_asset_sub_cat_cd and raja.ast_cat_Code=aa.car_asset_cat_cd left join ast_cmn_asset rak on rak.cas_asset_cd=aa.car_asset_cd and rak.cas_asset_cat_cd = aa.car_asset_cat_cd and rak.cas_asset_sub_cat_cd = aa.car_asset_sub_cat_cd and rak.cas_asset_type_cd = aa.car_asset_type_cd where aa.flag_set ='0' and " + clmfd + " group by car_asset_cat_cd,raka.ast_kategori_desc,car_asset_sub_cat_cd,rask.ast_subkateast_desc,car_asset_type_cd,raja.ast_jeniaset_desc,car_asset_cd,rak.cas_asset_desc,car_price_amt,asa.sas_qty";
            }
            else if (DD_Kategori.SelectedValue == "03")
            {
                st_qry = "select inv_asset_cat_cd,raka.ast_kategori_desc,inv_asset_sub_cat_cd,rask.ast_subkateast_desc,inv_asset_type_cd,raja.ast_jeniaset_desc,inv_asset_cd,rak.cas_asset_desc ,inv_qty as strqty,inv_price_amt as pamt,(inv_qty *  inv_price_amt) as tamt from ast_inventory aa left join Ref_ast_kategori raka on raka.ast_kategori_code=aa.inv_asset_cat_cd  left join Ref_ast_sub_kategri_Aset rask on rask.ast_subkateast_Code=aa.inv_asset_sub_cat_cd left join Ref_ast_jenis_aset raja on raja.ast_jeniaset_Code=aa.inv_asset_type_cd and raja.ast_sub_cat_Code=aa.inv_asset_sub_cat_cd and raja.ast_cat_Code=aa.inv_asset_cat_cd left join ast_cmn_asset rak on rak.cas_asset_cd=aa.inv_asset_cd and rak.cas_asset_cat_cd = aa.inv_asset_cat_cd and rak.cas_asset_sub_cat_cd = aa.inv_asset_sub_cat_cd and rak.cas_asset_type_cd = aa.inv_asset_type_cd   where aa.flag_set ='0' and " + clmfd + "";
            }
            else if (DD_Kategori.SelectedValue == "04")
            {
                st_qry = "select pro_asset_cat_cd,raka.ast_kategori_desc,pro_asset_sub_cat_cd,rask.ast_subkateast_desc,pro_asset_type_cd,raja.ast_jeniaset_desc,pro_asset_cd,rak.cas_asset_desc,count(ISNULL(pro_allocate_staff_no,'')) as strqty,ISNULL(pro_loan_amt,'') as pamt,(count(ISNULL(pro_allocate_staff_no,'')) *  ISNULL(pro_loan_amt,'')) as tamt   from ast_property aa left join ast_staff_asset asa on asa.sas_asset_cat_cd=aa.pro_asset_cat_cd and asa.sas_asset_sub_cat_cd=aa.pro_asset_sub_cat_cd and asa.sas_asset_type_cd=aa.pro_asset_type_cd and asa.sas_asset_cd=aa.pro_asset_cd left join Ref_ast_kategori raka on raka.ast_kategori_code=aa.pro_asset_cat_cd  left join Ref_ast_sub_kategri_Aset rask on rask.ast_subkateast_Code=aa.pro_asset_sub_cat_cd left join Ref_ast_jenis_aset raja on raja.ast_jeniaset_Code=aa.pro_asset_type_cd and raja.ast_sub_cat_Code=aa.pro_asset_sub_cat_cd and raja.ast_cat_Code=aa.pro_asset_cat_cd left join ast_cmn_asset rak on rak.cas_asset_cd=aa.pro_asset_cd and rak.cas_asset_cat_cd = aa.pro_asset_cat_cd and rak.cas_asset_sub_cat_cd = aa.pro_asset_sub_cat_cd and rak.cas_asset_type_cd = aa.pro_asset_type_cd where aa.flag_set ='0' and " + clmfd + " group by pro_asset_cat_cd,raka.ast_kategori_desc,pro_asset_sub_cat_cd,rask.ast_subkateast_desc,pro_asset_type_cd,raja.ast_jeniaset_desc,pro_asset_cd,rak.cas_asset_desc,pro_loan_amt,asa.sas_qty";
            }
        }
        else
        {
            st_qry = "select com_asset_cat_cd,raka.ast_kategori_desc,com_asset_sub_cat_cd,rask.ast_subkateast_desc,com_asset_type_cd,raja.ast_jeniaset_desc,com_asset_cd,rak.cas_asset_desc, count(ISNULL(com_allocate_staff_no,'')) as strqty,com_price_amt as pamt,(count(ISNULL(com_allocate_staff_no,'')) *  com_price_amt) as tamt    from ast_component aa left join ast_staff_asset asa on asa.sas_asset_cat_cd=aa.com_asset_cat_cd and asa.sas_asset_sub_cat_cd=aa.com_asset_sub_cat_cd and asa.sas_asset_type_cd=aa.com_asset_type_cd and asa.sas_asset_cd=aa.com_asset_cd left join Ref_ast_kategori raka on raka.ast_kategori_code=aa.com_asset_cat_cd  left join Ref_ast_sub_kategri_Aset rask on rask.ast_subkateast_Code=aa.com_asset_sub_cat_cd left join Ref_ast_jenis_aset raja on raja.ast_jeniaset_Code=aa.com_asset_type_cd and raja.ast_sub_cat_Code=aa.com_asset_sub_cat_cd and raja.ast_cat_Code=aa.com_asset_cat_cd left join ast_cmn_asset rak on rak.cas_asset_cd=aa.com_asset_cd and rak.cas_asset_cat_cd = aa.com_asset_cat_cd and rak.cas_asset_sub_cat_cd = aa.com_asset_sub_cat_cd and rak.cas_asset_type_cd = aa.com_asset_type_cd  where com_asset_cd='' group by com_asset_cat_cd,raka.ast_kategori_desc,com_asset_sub_cat_cd,rask.ast_subkateast_desc,com_asset_type_cd,raja.ast_jeniaset_desc,com_asset_cd,rak.cas_asset_desc,com_price_amt,asa.sas_qty";
        }
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

    protected void Btn_Carian_Click(object sender, EventArgs e)
    {
        if (DD_Kategori.SelectedValue != "" || DD_Sub.SelectedValue != "" || DropDownList1.SelectedValue != "")
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
        dd_bind();
     
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        dt = DBCon.Ora_Execute_table("" + st_qry + "");
        ds.Tables.Add(dt);

        List<DataRow> listResult = dt.AsEnumerable().ToList();
        listResult.Count();
        int countRow = 0;
        countRow = listResult.Count();
        //ReportViewer1.LocalReport.Refresh();
        if (countRow != 0)
        {
            disp_hdr_txt.Visible = true;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("rma1", dt);

            //Path
            ReportViewer1.LocalReport.ReportPath = "Aset/AST_rma.rdlc";
            //Parameters

            string ss1 = string.Empty, ss2 = string.Empty, ss3 = string.Empty;

            if (DD_Sub.SelectedValue != "")
            {
                ss1 = DD_Sub.SelectedItem.Text;
            }
            else
            {
                ss1 = "SEMUA";
            }

            if (DropDownList1.SelectedValue != "")
            {
                ss2 = DropDownList1.SelectedItem.Text;
            }
            else
            {
                ss2 = "SEMUA";
            }

            //if (DropDownList2.SelectedValue != "")
            //{
            //    ss3 = DropDownList2.SelectedItem.Text;
            //}
            //else
            //{
            //    ss3 = "SEMUA";
            //}

            ReportParameter[] rptParams = new ReportParameter[]{
                         new ReportParameter("d1",DD_Kategori.SelectedItem.Text ),
                         new ReportParameter("d2",ss1),
                         new ReportParameter("d3",ss2),
                         new ReportParameter("d4",ss3),
                    };
            //     //new ReportParameter("toDate",ToDate .Text )
            //     //new ReportParameter("fromDate",datedari  ),
            //     //new ReportParameter("toDate",datehingga ),
            //          //new ReportParameter("caw",branch ),     
            //            //new ReportParameter("Cdate",DateTime.Now.ToString("dd/MM/yyyy") ),     
            //     //new ReportParameter("Date",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  )
            //     };
            //string extfile = DD_PEGA.SelectedValue;
            ReportViewer1.LocalReport.DisplayName = "RINGKASAN_MAKLUMAT_ASET_" + DateTime.Now.ToString("ddMMyyyy");
            ReportViewer1.LocalReport.SetParameters(rptParams);
            ReportViewer1.LocalReport.DataSources.Add(rds);
            //Refresh
            ReportViewer1.LocalReport.Refresh();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod tidak dijumpai. Sila pastikan semua maklumat dimasukkan dengan betul.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }

    }

    protected void Reset_btn(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_ringkasan_ast.aspx");
    }

 
    
}