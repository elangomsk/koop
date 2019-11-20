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

public partial class Ast_kerosakan_kelulusan : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();

    DataTable dt = new DataTable();
    string useid = string.Empty;
    string Status = string.Empty;
    string dar_dt = string.Empty, seh_dt = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2()})";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Button4);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;
                kat();
                comp_sts();
                //BindGrid();

                TextBox4.Attributes.Add("Readonly", "Readonly");
                TextBox5.Attributes.Add("Readonly", "Readonly");
                TextBox6.Attributes.Add("Readonly", "Readonly");
                TextBox7.Attributes.Add("Readonly", "Readonly");
                TextBox8.Attributes.Add("Readonly", "Readonly");
                TextBox9.Attributes.Add("Readonly", "Readonly");
                


                if (samp != "")
                {
                    lbl_name.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    TextBox2.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    view_details();
                    ver_id.Text = "1";

                }
                else
                {
                    ver_id.Text = "0";
                }
                useid = Session["New"].ToString();
               
             
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

   
    void view_details()
    {
        
        try
        {
            
                // View Record Details
                DataTable dd_cmp = new DataTable();
                dd_cmp = DBCon.Ora_Execute_table("select *,sp1.stf_name as np,FORMAT(cmp_action_dt,'dd/MM/yyyy', 'en-us') cmp_action_dt1 from ast_complaint left join hr_staff_profile as sp1 on cmp_action_id=sp1.stf_staff_no where cmp_id='" + lbl_name.Text + "'");

                DataTable ddokdicno = new DataTable();
                ddokdicno = DBCon.Ora_Execute_table("select stf_staff_no,stf_name,org_name,hr_jaba_desc,hr_jaw_desc From hr_staff_profile as SP left join Ref_hr_jabatan as jb on jb.hr_jaba_Code=SP.stf_curr_dept_cd left join Ref_hr_Jawatan as JW on JW.hr_jaw_Code=SP.stf_curr_post_cd left join hr_organization as ho on ho.org_gen_id=SP.str_curr_org_cd  where stf_staff_no='" + dd_cmp.Rows[0]["cmp_staff_no"].ToString() + "'");
                if (ddokdicno.Rows.Count != 0)
                {
                    TextBox4.Text = ddokdicno.Rows[0]["stf_staff_no"].ToString();
                    TextBox6.Text = ddokdicno.Rows[0]["stf_name"].ToString();
                    TextBox5.Text = ddokdicno.Rows[0]["org_name"].ToString();
                    TextBox7.Text = ddokdicno.Rows[0]["hr_jaba_desc"].ToString();
                    TextBox8.Text = ddokdicno.Rows[0]["hr_jaw_desc"].ToString();
                    TextBox9.Text = DateTime.Now.ToString("dd/MM/yyyy");

                 

                    if (dd_cmp.Rows.Count != 0)
                    {
                        DropDownList1.SelectedValue = dd_cmp.Rows[0]["cmp_cat_cd"].ToString().Trim();
                        ss_subkat();
                        DD_Sub.SelectedValue = dd_cmp.Rows[0]["cmp_sub_cat_cd"].ToString().Trim();
                        jenaset();
                        DropDownList2.SelectedValue = dd_cmp.Rows[0]["cmp_type_cd"].ToString();
                        Nama_aset();
                        DropDownList3.SelectedValue = dd_cmp.Rows[0]["cmp_asset_name"].ToString();
                        TextBox10.Text = dd_cmp.Rows[0]["cmp_asset_id"].ToString();
                        txt_area.Value = dd_cmp.Rows[0]["cmp_remark"].ToString();
                        Textarea1.Value = dd_cmp.Rows[0]["cmp_action_remark"].ToString();
                        TextBox1.Text = lbl_name.Text;
                        if (dd_cmp.Rows[0]["cmp_cost_amt"].ToString() != "")
                        {
                            decimal at1 = decimal.Parse(dd_cmp.Rows[0]["cmp_cost_amt"].ToString());
                            txt_bx1.Text = at1.ToString("0.00");
                        }
                        else
                        {
                            txt_bx1.Text = "0.00";
                        }
                        DropDownList5.SelectedValue = dd_cmp.Rows[0]["cmp_sts_cd"].ToString();
                        DropDownList4.SelectedValue = dd_cmp.Rows[0]["cmp_approval_sts_cd"].ToString().Trim();
                        Textarea3.Value = dd_cmp.Rows[0]["cmp_approval_remark"].ToString().Trim();
                        txtnp.Text = dd_cmp.Rows[0]["np"].ToString();
                        txttarikh.Text = dd_cmp.Rows[0]["cmp_action_dt1"].ToString();
                    }
                    //BindGrid();
                }
                else
                {
                    //BindGrid();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
           


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

            string com = "select ast_kategori_code,upper(ast_kategori_desc) as ast_kategori_desc from Ref_ast_kategori where status = 'A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList1.DataSource = dt;
            DropDownList1.DataTextField = "ast_kategori_desc";
            DropDownList1.DataValueField = "ast_kategori_code";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void OnSelectedIndexChanged1(object sender, EventArgs e)
    {
        jenaset();
        //BindGrid();
    }

    void jenaset()
    {
        DataSet Ds1 = new DataSet();
        try
        {
            string com1 = "select * from Ref_ast_jenis_aset where Status='A' and ast_cat_Code='" + DropDownList1.SelectedValue + "' and ast_sub_cat_Code = '" + DD_Sub.SelectedValue + "'";
            SqlDataAdapter adpt = new SqlDataAdapter(com1, con);
            DataTable dt1 = new DataTable();
            adpt.Fill(dt1);
            DropDownList2.DataSource = dt1;
            DropDownList2.DataTextField = "ast_jeniaset_desc";
            DropDownList2.DataValueField = "ast_jeniaset_Code";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void OnSelectedIndexChanged2(object sender, EventArgs e)
    {
        cmnaset();
        //BindGrid();
    }

    void cmnaset()
    {
        DataSet Ds1 = new DataSet();
        try
        {
            string com1 = "select * from ast_cmn_asset where cas_asset_cat_cd='" + DropDownList1.SelectedValue + "' and cas_asset_sub_cat_cd='" + DD_Sub.SelectedValue + "' and cas_asset_type_cd='" + DropDownList2.SelectedValue + "'";
            SqlDataAdapter adpt = new SqlDataAdapter(com1, con);
            DataTable dt1 = new DataTable();
            adpt.Fill(dt1);
            DropDownList3.DataSource = dt1;
            DropDownList3.DataTextField = "cas_asset_desc";
            DropDownList3.DataValueField = "cas_asset_cd";
            DropDownList3.DataBind();
            DropDownList3.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void jenis_aset()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select ast_jeniaset_Code,upper(ast_jeniaset_desc) as ast_jeniaset_desc from Ref_ast_jenis_aset where Status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList2.DataSource = dt;
            DropDownList2.DataTextField = "ast_jeniaset_desc";
            DropDownList2.DataValueField = "ast_jeniaset_Code";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void Nama_aset()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select cas_asset_cd,upper(cas_asset_desc) as cas_asset_desc from ast_cmn_asset order by cas_asset_cd";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList3.DataSource = dt;
            DropDownList3.DataTextField = "cas_asset_desc";
            DropDownList3.DataValueField = "cas_asset_cd";
            DropDownList3.DataBind();
            DropDownList3.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void sel_kat(object sender, EventArgs e)
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select ast_subkateast_Code,upper(ast_subkateast_desc) as ast_subkateast_desc from Ref_ast_sub_kategri_Aset where ast_kategori_Code='" + DropDownList1.SelectedValue + "'  and Status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_Sub.DataSource = dt;
            DD_Sub.DataTextField = "ast_subkateast_desc";
            DD_Sub.DataValueField = "ast_subkateast_Code";
            DD_Sub.DataBind();
            DD_Sub.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            //BindGrid();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void ss_subkat()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select ast_subkateast_Code,upper(ast_subkateast_desc) as ast_subkateast_desc from Ref_ast_sub_kategri_Aset where ast_kategori_Code='" + DropDownList1.SelectedValue + "'  and Status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_Sub.DataSource = dt;
            DD_Sub.DataTextField = "ast_subkateast_desc";
            DD_Sub.DataValueField = "ast_subkateast_Code";
            DD_Sub.DataBind();
            DD_Sub.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            // pg_load();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void pg_load()
    {
        DataTable ddicno = new DataTable();
        ddicno = DBCon.Ora_Execute_table("select stf_staff_no From hr_staff_profile where stf_staff_no='" + Session["New"].ToString() + "' ");
        if (ddicno.Rows.Count != 0)
        {
            string stffno = ddicno.Rows[0]["stf_staff_no"].ToString();
            if (ddicno.Rows.Count > 0)
            {
                DataTable ddbind = new DataTable();
                ddbind = DBCon.Ora_Execute_table("select stf_staff_no,stf_name,org_name,hr_jaba_desc,hr_jaw_desc From hr_staff_profile as SP left join Ref_hr_jabatan as jb on jb.hr_jaba_Code=SP.stf_curr_dept_cd left join Ref_hr_Jawatan as JW on JW.hr_jaw_Code=SP.stf_curr_post_cd left join hr_organization as ho on ho.org_id=SP.str_curr_org_cd  where stf_staff_no='" + stffno + "'");

                TextBox4.Text = ddbind.Rows[0]["stf_staff_no"].ToString();
                TextBox6.Text = ddbind.Rows[0]["stf_name"].ToString();
                TextBox5.Text = ddbind.Rows[0]["org_name"].ToString();
                TextBox7.Text = ddbind.Rows[0]["hr_jaba_desc"].ToString();
                TextBox8.Text = ddbind.Rows[0]["hr_jaw_desc"].ToString();
                TextBox9.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //BindGrid();
            }
            else
            {
                //BindGrid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        else
        {
            //BindGrid();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Pengguna Tidak Wujud.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    //protected void BindGrid()
    //{
    //    // From and To Date
    //    if (txt_dar.Text != "" && txt_seh.Text != "")
    //    {
    //        DateTime ft = DateTime.ParseExact(txt_dar.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
    //        dar_dt = ft.ToString("yyyy-MM-dd");
    //        DateTime td = DateTime.ParseExact(txt_seh.Text, "dd/mm/yyyy", CultureInfo.InvariantCulture);
    //        seh_dt = td.ToString("yyyy-mm-dd");
    //    }
    //    else
    //    {
    //        dar_dt = "";
    //        seh_dt = "";
    //    }
    //    SqlCommand cmd2 = new SqlCommand("select cmp_id,cmp_staff_no,FORMAT(cmp_complaint_dt,'dd/MM/yyyy', 'en-us') as cmp_complaint_dt,cmp_remark,FORMAT(cmp_action_dt,'dd/MM/yyyy', 'en-us') as cmp_action_dt,cmp_action_remark, case when cmp_sts_cd = '01'  then 'BARU' when cmp_sts_cd= '02' then 'SEDANG DISELENGGARA' when cmp_sts_cd= '03' then 'SELESAI' when cmp_sts_cd= '04' then 'RUJUK PENYELIA' end as ss1,cmp_asset_id from ast_complaint where cmp_complaint_dt>=DATEADD(day, DATEDIFF(day, 0, '" + dar_dt + "'), 0) and cmp_complaint_dt<=DATEADD(day, DATEDIFF(day, 0, '" + seh_dt + "'), 0)", con);
    //    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
    //    DataSet ds2 = new DataSet();
    //    da2.Fill(ds2);

    //    // Empty Records

    //    if (ds2.Tables[0].Rows.Count == 0)
    //    {
    //        ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
    //        gvSelected.DataSource = ds2;
    //        gvSelected.DataBind();
    //        int columncount = gvSelected.Rows[0].Cells.Count;
    //        gvSelected.Rows[0].Cells.Clear();
    //        gvSelected.Rows[0].Cells.Add(new TableCell());
    //        gvSelected.Rows[0].Cells[0].ColumnSpan = columncount;
    //        gvSelected.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
    //        if (txt_dar.Text != "" && txt_seh.Text != "")
    //        {
    //            clr2();
    //            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod carian Tidak Wujud.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
    //        }
    //    }
    //    // Show Records
    //    else
    //    {
    //        gvSelected.DataSource = ds2;
    //        gvSelected.DataBind();
    //    }
    //}

    protected void clk_cetak(object sender, EventArgs e)
    {
        try
        {
            if (lbl_name.Text != "")
            {
                //if (txt_dar.Text != "" && txt_seh.Text != "")
                //{
                //    DateTime ft = DateTime.ParseExact(txt_dar.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //    dar_dt = ft.ToString("yyyy-MM-dd");
                //    DateTime td = DateTime.ParseExact(txt_seh.Text, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                //    seh_dt = td.ToString("yyyy-mm-dd");
                //}
                //else
                //{
                //    dar_dt = "";
                //    seh_dt = "";
                //}
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                //dt = DBCon.Ora_Execute_table("select jt.txn_applcn_no,jt.txn_cd,ISNULL(jt.txn_credit_amt,'') as txn_credit_amt,ISNULL(jt.txn_bal_amt,'') as txn_bal_amt,ISNULL(jt.txn_debit_amt,'') as txn_debit_amt,ISNULL(jt.txn_gst_amt,'') as txn_gst_amt,jt.txn_crt_dt,rt.txn_description,sum(ISNULL(jt.txn_credit_amt,'')) as tc_amt,sum(ISNULL(jt.txn_bal_amt,'')) as tb_amt,sum(ISNULL(jt.txn_debit_amt,'')) as td_amt,sum(ISNULL(jt.txn_gst_amt,'')) as tg_amt from jpa_transaction as jt left join Ref_jpa_txn as rt on rt.txn_cd=jt.txn_cd where txn_applcn_no='"+Applcn_no.Text+"' group by jt.txn_applcn_no,jt.txn_bal_amt,jt.txn_credit_amt,jt.txn_debit_amt,jt.txn_gst_amt,rt.txn_description,jt.txn_cd,jt.txn_crt_dt");
                dt = DBCon.Ora_Execute_table("select cmp_id,FORMAT(cmp_complaint_dt,'dd/MM/yyyy', 'en-us') as cmp_complaint_dt,cmp_remark,FORMAT(cmp_action_dt,'dd/MM/yyyy', 'en-us') as cmp_action_dt,cmp_action_remark,case when cmp_sts_cd='04' then 'RUJUK PENYELIA' end as cmpsts from ast_complaint where cmp_sts_cd='04' and cmp_id='" + lbl_name.Text +"'");
                Rptviwer_lt.Reset();
                ds.Tables.Add(dt);

                Rptviwer_lt.LocalReport.DataSources.Clear();


                ReportDataSource rds = new ReportDataSource("ast_tinkero_kel", dt);
                Rptviwer_lt.LocalReport.DataSources.Add(rds);
                Rptviwer_lt.LocalReport.ReportPath = "Aset/AST_Tin_kero_kel.rdlc";
                //DataTable applcn = new DataTable();
                //applcn = dbcon.Ora_Execute_table("");
                string sv1 = string.Empty, sv2 = string.Empty;
                string dd1 = string.Empty, dd2 = string.Empty, dd3 = string.Empty, dd4 = string.Empty, dd5 = string.Empty, dd6 = string.Empty;
               
                if (txt_bx1.Text != "")
                {
                    sv2 = double.Parse(txt_bx1.Text).ToString("C").Replace("$", "");
                }
                else
                {
                    sv2 = "";
                }

                if (DropDownList1.SelectedValue != "")
                {
                    dd1 = DropDownList1.SelectedItem.Text;
                }

                if (DD_Sub.SelectedValue != "")
                {
                    dd2 = DD_Sub.SelectedItem.Text;
                }
                if (DropDownList2.SelectedValue != "")
                {
                    dd3 = DropDownList2.SelectedItem.Text;
                }
                if (DropDownList3.SelectedValue != "")
                {
                    dd4 = DropDownList3.SelectedItem.Text;
                }
                if (DropDownList5.SelectedValue != "")
                {
                    dd5 = DropDownList5.SelectedItem.Text;
                }

                if (DropDownList4.SelectedValue != "")
                {
                    dd6 = DropDownList4.SelectedItem.Text;
                }

                ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("s1", txtnp.Text),
                     new ReportParameter("s2", txttarikh.Text),
                     //1st
                     new ReportParameter("s3", TextBox4.Text),
                     new ReportParameter("s4", TextBox5.Text),
                     new ReportParameter("s5", TextBox6.Text),
                     new ReportParameter("s6", TextBox7.Text),
                     new ReportParameter("s7", TextBox8.Text),
                     new ReportParameter("s8", TextBox9.Text),
                     //2nd
                     new ReportParameter("s9", dd1),
                     new ReportParameter("s10", dd2),
                     new ReportParameter("s11", dd3),
                     new ReportParameter("s12", dd4),
                     new ReportParameter("s13", TextBox10.Text),
                     new ReportParameter("s14", dd6),
                     //3rd
                     new ReportParameter("s15", txt_area.Value),
                     //4th
                     new ReportParameter("s16", Textarea1.Value),
                     new ReportParameter("s17", sv2),
                     new ReportParameter("s18", dd5),
                       new ReportParameter("s19", Textarea3.Value),
                     };
                Rptviwer_lt.LocalReport.SetParameters(rptParams);

                //Refresh
                Rptviwer_lt.LocalReport.Refresh();
                Warning[] warnings;

                string[] streamids;

                string mimeType;

                string encoding;

                string extension;

                string fname = DateTime.Now.ToString("dd_MM_yyyy");

                string devinfo = "<DeviceInfo><ColorDepth>32</ColorDepth><DpiX>350</DpiX><DpiY>350</DpiY><OutputFormat>PDF</OutputFormat>" +
                       "  <PageWidth>12.20in</PageWidth>" +
                        "  <PageHeight>8.27in</PageHeight>" +
                        "  <MarginTop>0.1in</MarginTop>" +
                        "  <MarginLeft>0.5in</MarginLeft>" +
                         "  <MarginRight>0in</MarginRight>" +
                         "  <MarginBottom>0in</MarginBottom>" +
                       "</DeviceInfo>";

                byte[] bytes = Rptviwer_lt.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);


                Response.Buffer = true;

                Response.Clear();

                Response.ClearHeaders();

                Response.ClearContent();

                Response.ContentType = "application/pdf";


                Response.AddHeader("content-disposition", "attachment; filename=Tindakan_Kerosakan_" + DateTime.Now.ToString("ddMMyyyy") + "." + extension);

                Response.BinaryWrite(bytes);

                //Response.Write("<script>");
                //Response.Write("window.open('', '_newtab');");
                //Response.Write("</script>");
                Response.Flush();

                Response.End();
            }
            else
            {
                //BindGrid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Aduan No.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
//            BindGrid();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Issue.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);

        }
    }

    //protected void clk_srch(object sender, EventArgs e)
    //{
    //    if (txt_dar.Text != "" && txt_seh.Text != "")
    //    {
    //        if (txt_dar.Text != "" && txt_seh.Text != "")
    //        {

    //            BindGrid();

    //        }
    //        else
    //        {
    //            BindGrid();
    //            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Pengguna Tidak Wujud.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
    //        }
    //    }
    //    else
    //    {
    //        BindGrid();
    //        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Medan Input Adalah Mandatori.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
    //    }
    //}

    //protected void lnkView_Click(object sender, EventArgs e)
    //{
    //    try
    //    {

    //        LinkButton btnButton = sender as LinkButton;
    //        GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
    //        System.Web.UI.WebControls.Label clid = (System.Web.UI.WebControls.Label)gvRow.FindControl("cid");
    //        System.Web.UI.WebControls.Label stffno = (System.Web.UI.WebControls.Label)gvRow.FindControl("sno");

    //        // View Record Details

    //        DataTable ddokdicno = new DataTable();
    //        ddokdicno = DBCon.Ora_Execute_table("select stf_staff_no,stf_name,org_name,hr_jaba_desc,hr_jaw_desc From hr_staff_profile as SP left join Ref_hr_jabatan as jb on jb.hr_jaba_Code=SP.stf_curr_dept_cd left join Ref_hr_Jawatan as JW on JW.hr_jaw_Code=SP.stf_curr_post_cd left join hr_organization as ho on ho.org_gen_id=SP.str_curr_org_cd  where stf_staff_no='" + stffno.Text + "'");
    //        if (ddokdicno.Rows.Count != 0)
    //        {
    //            TextBox4.Text = ddokdicno.Rows[0]["stf_staff_no"].ToString();
    //            TextBox6.Text = ddokdicno.Rows[0]["stf_name"].ToString();
    //            TextBox5.Text = ddokdicno.Rows[0]["org_name"].ToString();
    //            TextBox7.Text = ddokdicno.Rows[0]["hr_jaba_desc"].ToString();
    //            TextBox8.Text = ddokdicno.Rows[0]["hr_jaw_desc"].ToString();
    //            TextBox9.Text = DateTime.Now.ToString("dd/MM/yyyy");

    //            DataTable dd_cmp = new DataTable();
    //            dd_cmp = DBCon.Ora_Execute_table("select * from ast_complaint where cmp_id='" + clid.Text + "'");

    //            if (dd_cmp.Rows.Count != 0)
    //            {
    //                DropDownList1.SelectedValue = dd_cmp.Rows[0]["cmp_cat_cd"].ToString().Trim();
    //                ss_subkat();
    //                DD_Sub.SelectedValue = dd_cmp.Rows[0]["cmp_sub_cat_cd"].ToString().Trim();
    //                jenaset();
    //                DropDownList2.SelectedValue = dd_cmp.Rows[0]["cmp_type_cd"].ToString();
    //                Nama_aset();
    //                DropDownList3.SelectedValue = dd_cmp.Rows[0]["cmp_asset_name"].ToString();
    //                TextBox10.Text = dd_cmp.Rows[0]["cmp_asset_id"].ToString();
    //                txt_area.Value = dd_cmp.Rows[0]["cmp_remark"].ToString();
    //                Textarea1.Value = dd_cmp.Rows[0]["cmp_action_remark"].ToString();
    //                TextBox1.Text = clid.Text;
    //                if (dd_cmp.Rows[0]["cmp_cost_amt"].ToString() != "")
    //                {
    //                    decimal at1 = decimal.Parse(dd_cmp.Rows[0]["cmp_cost_amt"].ToString());
    //                    txt_bx1.Text = at1.ToString("0.00");
    //                }
    //                else
    //                {
    //                    txt_bx1.Text = "0.00";
    //                }

    //                if (dd_cmp.Rows[0]["cmp_maintain_ind"].ToString().Trim() == "1")
    //                {
    //                    RB31.Checked = true;
    //                    RB32.Checked = false;
    //                }
    //                else if (dd_cmp.Rows[0]["cmp_maintain_ind"].ToString().Trim() == "2")
    //                {
    //                    RB32.Checked = true;
    //                    RB31.Checked = false;
    //                }
    //                else
    //                {
    //                    RB31.Checked = false;
    //                    RB32.Checked = false;
    //                }
    //                DropDownList5.SelectedValue = dd_cmp.Rows[0]["cmp_sts_cd"].ToString().Trim();
    //            }
    //            BindGrid();
    //        }
    //        else
    //        {
    //            BindGrid();
    //            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        BindGrid();
    //        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('ISSUE.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
    //    }
    //}

    //protected void RB1_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (RB31.Checked == true)
    //    {
    //        RB32.Checked = false;
    //    }
    //    if (TextBox10.Text != "")
    //    {
    //        string url = "AST_Sel_naiktaraf.aspx?ast_id=" + TextBox10.Text;
    //        Response.Redirect("" + url + "");
    //    }
    //    BindGrid();
    //}

  

    //protected void RB2_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (RB32.Checked == true)
    //    {
    //        RB31.Checked = false;
    //    }
    //    if (TextBox10.Text != "")
    //    {
    //        string url = "AST_Sel_pindahan.aspx?ast_id=" + TextBox1.Text;
    //        Response.Redirect("" + url + "");
    //    }
    //    BindGrid();
    //}
   

    //protected void gv_refdata_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    gvSelected.PageIndex = e.NewPageIndex;
    //    BindGrid();
    //}

    protected void clk_smpn(object sender, EventArgs e)
    {
        DataTable ddicno1 = new DataTable();
        ddicno1 = DBCon.Ora_Execute_table("select * from ast_complaint where cmp_id='" + TextBox1.Text + "'");
        if (ddicno1.Rows.Count != 0)
        {

            // Update Query
            string Inssql1 = "update ast_complaint set cmp_approval_sts_cd ='" + DropDownList4.SelectedValue + "',cmp_approval_remark ='" + Textarea3.Value.Replace("'", "''") + "',cmp_approval_id ='" + useid + "',cmp_approval_dt ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',cmp_upd_id ='" + useid + "',cmp_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'  where cmp_id ='" + lbl_name.Text + "'";
            Status = DBCon.Ora_Execute_CommamdText(Inssql1);
            if (Status == "SUCCESS")
            {
                Session["validate_success"] = "SUCCESS";
                Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                Response.Redirect("../Aset/Ast_kerosakan_kelulusan_view.aspx");
                //BindGrid();
            }
        }
        else
        {
            //BindGrid();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Pengguna Tidak Wujud.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void btnreset_Click1(object sender, EventArgs e)
    {
        clr2();
    }

    void clr2()
    {
        TextBox4.Text = "";
        TextBox5.Text = "";
        TextBox6.Text = "";
        TextBox7.Text = "";
        TextBox8.Text = "";
        TextBox9.Text = "";
        TextBox1.Text = "";
        DropDownList1.SelectedValue = "";
        DD_Sub.SelectedValue = "";
        DropDownList2.SelectedValue = "";
        DropDownList3.SelectedValue = "";
        TextBox10.Text = "";
        txt_area.Value = "";
        Textarea1.Value = "";
        txt_bx1.Text = "";
        DropDownList5.SelectedValue = "";
    }

    void comp_sts()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select RTRIM(ast_complaintsts_code) as ast_complaintsts_code,ast_complaintsts_desc From ref_complaint_sts where status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList5.DataSource = dt;
            DropDownList5.DataTextField = "ast_complaintsts_desc";
            DropDownList5.DataValueField = "ast_complaintsts_code";
            DropDownList5.DataBind();
            DropDownList5.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_kerosakan_kelulusan.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_kerosakan_kelulusan_view.aspx");
    }

    
}