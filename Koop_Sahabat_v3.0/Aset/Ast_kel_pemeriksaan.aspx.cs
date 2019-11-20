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

public partial class Ast_kel_pemeriksaan : System.Web.UI.Page
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
    string level, userid;
    string val1 = string.Empty, val2 = string.Empty, val3 = string.Empty;

    string sqry_val1 = string.Empty, sqry_val2 = string.Empty, sqry_val3 = string.Empty;
    float total = 0, total1 = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2()})";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Button5);
        scriptManager.RegisterPostBackControl(this.Button3);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;
                sts_pemeri();
                if (samp != "")
                {
                    view_details(); 

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
    void sts_pemeri()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select ast_pemerikksaan_code,ast_pemerikksaan_desc from Ref_ast_sts_pemerikksaan where status='A' order by ast_pemerikksaan_desc asc";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_stspemeri.DataSource = dt;
            DD_stspemeri.DataBind();
            DD_stspemeri.DataTextField = "ast_pemerikksaan_desc";
            DD_stspemeri.DataValueField = "ast_pemerikksaan_code";
            DD_stspemeri.DataBind();
            DD_stspemeri.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void jawatan()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select hr_jaw_Code,hr_jaw_desc From Ref_hr_Jawatan where Status='A' order by hr_jaw_desc ASC";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_stspemeri.DataSource = dt;
            DD_stspemeri.DataBind();
            DD_stspemeri.DataTextField = "hr_jaw_desc";
            DD_stspemeri.DataValueField = "hr_jaw_Code";
            DD_stspemeri.DataBind();
            DD_stspemeri.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void clr()
    {
        TextBox11.Text = "";
        TextBox3.Text = "";
        TextBox10.Text = "";
        TextBox7.Text = "";

        TextBox2.Value = "";
        tb2.Text = "";

        TextBox6.Value = "";
        TextBox8.Text = "";

        TextBox9.Text = "";
        TextBox5.Text = "";
        TextBox12.Text = "";
        TextBox13.Text = "";
    }


    void grid()
    {

        clr();
        con.Open();
        //DataTable ddokdicno1 = new DataTable();
        //ddokdicno1 = DBCon.Ora_Execute_table("select sas_staff_no from ast_staff_asset SA Left join hr_staff_profile as SF on SF.stf_staff_no=SA.sas_staff_no  where stf_icno='" + Session["New"].ToString() + "'");
        //if (ddokdicno1.Rows.Count != 0)
        //{
        if (TextBox1.Text != "" && TextBox4.Text != "")
        {
            GridView2.Visible = false;
            GridView1.Visible = true;

            //string abc = ddokdicno1.Rows[0]["sas_staff_no"].ToString();
            string datedari = TextBox1.Text;
            DateTime dt = DateTime.ParseExact(datedari, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            String fromdate = dt.ToString("yyyy-MM-dd");

            string datedari1 = TextBox4.Text;
            DateTime dt1 = DateTime.ParseExact(datedari1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            String todate = dt1.ToString("yyyy-MM-dd");

            sel_qry();

            DataTable ddicno = new DataTable();
            SqlCommand cmd = new SqlCommand("select sas_asset_id,org_name,hr_jaba_desc,ast_kategori_desc,ast_subkateast_desc,ast_jeniaset_desc,ast_pemerikksaan_desc,ast_justifikasi_desc from ast_staff_asset as AST left join hr_staff_profile as HRS on HRS.stf_staff_no=AST.sas_staff_no  left join hr_organization ORG on ORG.org_gen_id=AST.sas_org_id left join Ref_hr_jabatan JB on JB.hr_jaba_Code=HRS.stf_curr_dept_cd  left join Ref_ast_kategori AK on  AK.ast_kategori_code=AST.sas_asset_cat_cd left join Ref_ast_sub_kategri_Aset SKA on  SKA.ast_subkateast_Code=AST.sas_asset_sub_cat_cd left join Ref_ast_jenis_aset AJA on AJA.ast_jeniaset_Code=AST.sas_asset_type_cd  left join Ref_ast_sts_pemerikksaan as PEM on PEM.ast_pemerikksaan_code=AST.sas_cond_sts_cd left join Ref_ast_justifikasi as js on js.ast_justifikasi_code=AST.sas_justify_cd where AST.flag_set ='0' and " + sqry_val1 + "", con);
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
                GridView1.FooterRow.Cells[0].Text = "<strong>JUMLAH (RM)</strong>";
                GridView1.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod carian Tidak Wujud.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);

            }
            else
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.FooterRow.Cells[0].Text = "<strong>JUMLAH (RM)</strong>";
                GridView1.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
            con.Close();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }


    }
    void grid1()
    {
        clr();
        con.Open();
        //DataTable ddokdicno1 = new DataTable();
        //ddokdicno1 = DBCon.Ora_Execute_table("select sas_staff_no from ast_staff_asset SA Left join hr_staff_profile as SF on SF.stf_staff_no=SA.sas_staff_no  where stf_icno='" + Session["New"].ToString() + "'");
        //if (ddokdicno1.Rows.Count != 0)
        //{
        if (TextBox1.Text != "" && TextBox4.Text != "")
        {
            GridView1.Visible = false;
            GridView2.Visible = true;
            //string abc = ddokdicno1.Rows[0]["sas_staff_no"].ToString();

            sel_qry();

            //GridView2.Visible = false;
            DataTable ddicno = new DataTable();
            SqlCommand cmd = new SqlCommand("select sas_asset_id,org_name,hr_jaba_desc,ast_kategori_desc,ast_subkateast_desc,ast_jeniaset_desc,ast_pemerikksaan_desc,ISNULL(sas_repair_amt,'') sas_repair_amt,sas_justify_cd,sas_repair_amt,ast_justifikasi_desc from ast_staff_asset as AST left join hr_staff_profile as HRS on HRS.stf_staff_no=AST.sas_staff_no  left join hr_organization ORG on ORG.org_gen_id=AST.sas_org_id left join Ref_hr_jabatan JB on JB.hr_jaba_Code=HRS.stf_curr_dept_cd  left join Ref_ast_kategori AK on  AK.ast_kategori_code=AST.sas_asset_cat_cd left join Ref_ast_sub_kategri_Aset SKA on  SKA.ast_subkateast_Code=AST.sas_asset_sub_cat_cd left join Ref_ast_jenis_aset AJA on AJA.ast_jeniaset_Code=AST.sas_asset_type_cd  left join Ref_ast_sts_pemerikksaan as PEM on PEM.ast_pemerikksaan_code=AST.sas_cond_sts_cd left join Ref_ast_justifikasi as js on js.ast_justifikasi_code=AST.sas_justify_cd where AST.flag_set ='0' and " + sqry_val1 + "", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count == 0)
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                GridView2.DataSource = ds;
                GridView2.DataBind();
                int columncount = GridView2.Rows[0].Cells.Count;
                GridView2.Rows[0].Cells.Clear();
                GridView2.Rows[0].Cells.Add(new TableCell());
                GridView2.Rows[0].Cells[0].ColumnSpan = columncount;
                GridView2.Rows[0].Cells[0].Text = "<strong><center>Maklumat Carian Tidak Dijumpai</center></strong>";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod carian Tidak Wujud.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                GridView2.FooterRow.Cells[8].Text = "<strong>JUMLAH (RM)</strong>";
                GridView2.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Center;

            }
            else
            {
                GridView2.DataSource = ds;
                GridView2.DataBind();
                GridView2.FooterRow.Cells[8].Text = "<strong>JUMLAH (RM)</strong>";
                GridView2.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Center;

            }
            con.Close();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }

        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Rekod Tidak Dijumpai.');", true);
        //}
    }

    void sel_qry()
    {
        string datedari = TextBox1.Text;
        DateTime dt = DateTime.ParseExact(datedari, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        String fromdate = dt.ToString("yyyy-MM-dd");

        string datedari1 = TextBox4.Text;
        DateTime dt1 = DateTime.ParseExact(datedari1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        String todate = dt1.ToString("yyyy-MM-dd");

        if (TextBox1.Text != "" && TextBox4.Text != "" && DD_stspemeri.SelectedValue != "")
        {
            sqry_val1 = "sas_inspect_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fromdate + "'), 0) and sas_inspect_dt<=DATEADD(day, DATEDIFF(day, 0, '" + todate + "'), +1) and sas_cond_sts_cd='" + DD_stspemeri.SelectedValue + "'";
        }
        else
        {
            sqry_val1 = "sas_inspect_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fromdate + "'), 0) and sas_inspect_dt<=DATEADD(day, DATEDIFF(day, 0, '" + todate + "'), +1)";
        }
       
    }

    protected void gv_refdata_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        grid1();
    }

    protected void gv_refdata_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        grid();
    }

    protected void gvEmp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            System.Web.UI.WebControls.Label rep_amt = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label6_4");
            if (rep_amt.Text != "")
            {
                decimal amt1 = Convert.ToDecimal(rep_amt.Text);
               total += (float)amt1;
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            System.Web.UI.WebControls.Label lblamount = (System.Web.UI.WebControls.Label)e.Row.FindControl("lbltotal");
            lblamount.Text = double.Parse(total.ToString()).ToString("C").Replace("RM","").Replace("$", "");
        }
    }
    protected void btn_reset(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void lnkView1_Click(object sender, EventArgs e)
    {
        LinkButton btnButton = sender as LinkButton;
        GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
        System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("Label11");
        string abc = lblTitle.Text;

        //string LabelText = ((Label)GridView1.FindControl("Label10")).Text;

        DataTable ddokdicno1 = new DataTable();
        ddokdicno1 = DBCon.Ora_Execute_table("select sas_spv_remark,sas_spv_id, case when format(sas_spv_dt,'dd/MM/yyyy') = '01/01/1900' then '' else  format(sas_spv_dt,'dd/MM/yyyy') end as sas_spv_dt, case when format(sas_approve_dt,'dd/MM/yyyy') = '01/01/1900' then '' else  format(sas_approve_dt,'dd/MM/yyyy') end as sas_apv_dt,sas_mgt_remark,sas_mgt_id,case when format(sas_mgt_dt,'dd/MM/yyyy') = '01/01/1900' then '' else  format(sas_mgt_dt,'dd/MM/yyyy') end as sas_mgt_dt,sas_approve_dt,sas_keyin_id,stf_curr_post_cd,sas_staff_no,stf_name,hr_jaw_desc from ast_staff_asset left join hr_staff_profile as hr on hr.stf_staff_no=sas_staff_no left join Ref_hr_Jawatan as jw on jw.hr_jaw_Code=hr.stf_curr_post_cd where sas_asset_id='" + abc + "'");
        if (ddokdicno1.Rows.Count != 0)
        {
            Button3.Visible = true;
            TextBox11.Text = ddokdicno1.Rows[0]["stf_name"].ToString();
            TextBox3.Text = ddokdicno1.Rows[0]["hr_jaw_desc"].ToString();
            TextBox10.Text = ddokdicno1.Rows[0]["stf_name"].ToString();
            TextBox7.Text = ddokdicno1.Rows[0]["hr_jaw_desc"].ToString();

            TextBox2.Value = ddokdicno1.Rows[0]["sas_spv_remark"].ToString();
            tb2.Text = ddokdicno1.Rows[0]["sas_spv_dt"].ToString();

            TextBox6.Value = ddokdicno1.Rows[0]["sas_mgt_remark"].ToString();
            TextBox8.Text = ddokdicno1.Rows[0]["sas_mgt_dt"].ToString();

            TextBox9.Text = DateTime.Now.ToString("dd/MM/yyyy");
            TextBox5.Text = ddokdicno1.Rows[0]["stf_name"].ToString();
            TextBox12.Text = abc;
            TextBox13.Text = ddokdicno1.Rows[0]["sas_staff_no"].ToString();
            TextBox9.Text = ddokdicno1.Rows[0]["sas_apv_dt"].ToString();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    protected void lnkView2_Click(object sender, EventArgs e)
    {
        LinkButton btnButton = sender as LinkButton;
        GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
        System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("Label10");
        string abc = lblTitle.Text;

        //string LabelText = ((Label)GridView1.FindControl("Label10")).Text;

        DataTable ddokdicno1 = new DataTable();
        ddokdicno1 = DBCon.Ora_Execute_table("select sas_spv_remark,sas_spv_id, case when format(sas_spv_dt,'dd/MM/yyyy') = '01/01/1900' then '' else  format(sas_spv_dt,'dd/MM/yyyy') end as sas_spv_dt, case when format(sas_approve_dt,'dd/MM/yyyy') = '01/01/1900' then '' else  format(sas_approve_dt,'dd/MM/yyyy') end as sas_apv_dt,sas_mgt_remark,sas_mgt_id,case when format(sas_mgt_dt,'dd/MM/yyyy') = '01/01/1900' then '' else  format(sas_mgt_dt,'dd/MM/yyyy') end as sas_mgt_dt,sas_approve_dt,sas_keyin_id,stf_curr_post_cd,sas_staff_no,stf_name,hr_jaw_desc from ast_staff_asset left join hr_staff_profile as hr on hr.stf_staff_no=sas_staff_no left join Ref_hr_Jawatan as jw on jw.hr_jaw_Code=hr.stf_curr_post_cd where sas_asset_id='" + abc + "'");
        if (ddokdicno1.Rows.Count != 0)
        {
            Button3.Visible = true;
            TextBox11.Text = ddokdicno1.Rows[0]["stf_name"].ToString();
            TextBox3.Text = ddokdicno1.Rows[0]["hr_jaw_desc"].ToString();
            TextBox10.Text = ddokdicno1.Rows[0]["stf_name"].ToString();
            TextBox7.Text = ddokdicno1.Rows[0]["hr_jaw_desc"].ToString();

            TextBox2.Value = ddokdicno1.Rows[0]["sas_spv_remark"].ToString();
            tb2.Text = ddokdicno1.Rows[0]["sas_spv_dt"].ToString();

            TextBox6.Value = ddokdicno1.Rows[0]["sas_mgt_remark"].ToString();
            TextBox8.Text = ddokdicno1.Rows[0]["sas_mgt_dt"].ToString();

            TextBox9.Text = DateTime.Now.ToString("dd/MM/yyyy");
            TextBox5.Text = ddokdicno1.Rows[0]["stf_name"].ToString();
            TextBox12.Text = abc;
            TextBox13.Text = ddokdicno1.Rows[0]["sas_staff_no"].ToString();
            TextBox9.Text = ddokdicno1.Rows[0]["sas_apv_dt"].ToString();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (DD_stspemeri.SelectedValue == "03")
        {
            grid1();
        }
        else
        {
            grid();

        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        //DataTable dd5 = new DataTable();
        //dd5 = DBCon.Ora_Execute_table("select sas_staff_no from ast_staff_asset SA Left join hr_staff_profile as SF on SF.stf_staff_no=SA.sas_staff_no  where stf_icno='" + Session["New"].ToString() + "'");
        if (TextBox12.Text != "")
        {
            DataTable dd1 = new DataTable();
            dd1 = DBCon.Ora_Execute_table("select sas_asset_id from ast_staff_asset where sas_asset_id='" + TextBox12.Text + "'");

            if (dd1.Rows.Count > 0)
            {
                string datedari = string.Empty, fromdate = string.Empty, datedari1 = string.Empty, datedari2 = string.Empty, todate = string.Empty, appr_dt = string.Empty;
                if (tb2.Text != "")
                {
                    datedari = tb2.Text;
                    DateTime dt = DateTime.ParseExact(datedari, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    fromdate = dt.ToString("yyyy-MM-dd");
                }

                if (TextBox8.Text != "")
                {
                    datedari1 = TextBox8.Text;
                    DateTime dt1 = DateTime.ParseExact(datedari1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    todate = dt1.ToString("yyyy-MM-dd");
                }

                if (TextBox9.Text != "")
                {
                    datedari2 = TextBox9.Text;
                    DateTime dt2 = DateTime.ParseExact(datedari2, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    appr_dt = dt2.ToString("yyyy-MM-dd");
                }

                string Inssql = "update ast_staff_asset set sas_spv_remark='" + TextBox2.Value.Replace("'", "''") + "',sas_spv_id='" + TextBox13.Text + "',sas_spv_dt='" + fromdate + "',sas_mgt_remark='" + TextBox6.Value.Replace("'", "''") + "',sas_mgt_id='" + TextBox13.Text + "',sas_mgt_dt='" + todate + "',sas_approve_dt='" + appr_dt + "',sas_keyin_id='" + Session["New"].ToString() + "',sas_upd_id='" + Session["New"].ToString() + "',sas_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where sas_asset_id='" + dd1.Rows[0]["sas_asset_id"].ToString() + "'";
                Status = DBCon.Ora_Execute_CommamdText(Inssql);
                Session["validate_success"] = "SUCCESS";
                Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                Response.Redirect("../Aset/Ast_kel_pemeriksaan_view.aspx");
                //clr();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    protected void Reset_btn(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void cetak_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt_2 = new DataTable();
            Page.Header.Title = "MAKLUMAT ADUAN PENYELENGGARAN ASET";
            if (TextBox1.Text != "" && TextBox4.Text != "" || DD_stspemeri.SelectedValue != "")
            {
              
                sel_qry();
                DataSet dsCustomers = GetData("select sas_asset_id,stf_name,js.ast_justifikasi_desc,org_name,hr_jaw_desc as hr_jaba_desc,ast_kategori_desc,ast_subkateast_desc,ast_jeniaset_desc,ast_pemerikksaan_desc,sas_repair_amt,sas_justify_cd,sas_repair_amt,sas_justify_cd ,ISNULL(sas_spv_remark,'') sas_spv_remark,ISNULL(sas_spv_id,'') sas_spv_id,case when format(ISNULL(sas_spv_dt,''),'dd/MM/yyyy') = '01/01/1900' then '' else  format(sas_spv_dt,'dd/MM/yyyy') end as sas_spv_dt,ISNULL(sas_mgt_remark,'') sas_mgt_remark,ISNULL(sas_mgt_id,'') sas_mgt_id,case when format(ISNULL(sas_mgt_dt,''),'dd/MM/yyyy') = '01/01/1900' then '' else  format(sas_mgt_dt,'dd/MM/yyyy') end as sas_mgt_dt,case when format(ISNULL(sas_approve_dt,''),'dd/MM/yyyy') = '01/01/1900' then '' else  format(sas_approve_dt,'dd/MM/yyyy') end as sas_approve_dt,ISNULL(sas_keyin_id,'') sas_keyin_id,sas_upd_id,sas_upd_dt from ast_staff_asset as AST left join hr_staff_profile as HRS on HRS.stf_staff_no=AST.sas_staff_no  left join hr_organization ORG on ORG.org_gen_id=AST.sas_org_id left join Ref_hr_Jawatan JB on JB.hr_jaw_Code=HRS.stf_curr_post_cd  left join Ref_ast_kategori AK on  AK.ast_kategori_code=AST.sas_asset_cat_cd left join Ref_ast_sub_kategri_Aset SKA on  SKA.ast_subkateast_Code=AST.sas_asset_sub_cat_cd left join Ref_ast_jenis_aset AJA on AJA.ast_jeniaset_Code=AST.sas_asset_type_cd  left join Ref_ast_sts_pemerikksaan as PEM on PEM.ast_pemerikksaan_code=AST.sas_cond_sts_cd left join Ref_ast_justifikasi as js on js.ast_justifikasi_code=AST.sas_justify_cd where AST.flag_set ='0' and " + sqry_val1 + "");
                dt = dsCustomers.Tables[0];


                DataSet dsCustomers1 = GetData("select sas_spv_remark,sas_spv_id, case when format(sas_spv_dt,'dd/MM/yyyy') = '01/01/1900' then '' else  format(sas_spv_dt,'dd/MM/yyyy') end as sas_spv_dt,sas_mgt_remark,sas_mgt_id,case when format(sas_mgt_dt,'dd/MM/yyyy') = '01/01/1900' then '' else  format(sas_mgt_dt,'dd/MM/yyyy') end as sas_mgt_dt,case when format(sas_approve_dt,'dd/MM/yyyy') = '01/01/1900' then '' else  format(sas_approve_dt,'dd/MM/yyyy') end as sas_approve_dt,sas_keyin_id,stf_curr_post_cd,sas_staff_no,stf_name,hr_jaw_desc as hr_jaba_desc from ast_staff_asset left join hr_staff_profile as hr on hr.stf_staff_no=sas_staff_no left join Ref_hr_Jawatan as jw on jw.hr_jaw_Code=hr.stf_curr_post_cd where flag_set ='0' and sas_asset_id='" + TextBox12.Text + "'");
                dt_2 = dsCustomers1.Tables[0];
                //}
            }

            ReportViewer1.Reset();

            List<DataRow> listResult = dt.AsEnumerable().ToList();
            listResult.Count();
            int countRow = 0;
            countRow = listResult.Count();

            if (countRow != 0)
            {
                string ss1 = string.Empty;
                if (DD_stspemeri.SelectedValue != "")
                {
                    ss1 = DD_stspemeri.SelectedItem.Text;
                }
                else
                {
                    ss1 = "SEMUA";
                }

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                ReportDataSource rds1 = new ReportDataSource("DataSet2", dt_2);
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.LocalReport.DataSources.Add(rds1);
                //Path
                ReportViewer1.LocalReport.ReportPath = "Aset/AST_sel_tindakan.rdlc";
                //Parameters
                ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("d1",TextBox1.Text),
                     new ReportParameter("d2",TextBox4.Text),
                     new ReportParameter("d3",ss1),
                     };


                ReportViewer1.LocalReport.SetParameters(rptParams);
                //Refresh
                ReportViewer1.LocalReport.Refresh();

                Warning[] warnings;

                string[] streamids;

                string mimeType;

                string encoding;

                string extension;

                string devinfo = "<DeviceInfo>" +
          "  <OutputFormat>PDF</OutputFormat>" +
          "  <PageSize>A4</PageSize>" +
          "  <PageWidth>8in</PageWidth>" +
          "  <PageHeight>11in</PageHeight>" +
          "  <MarginTop>0.25in</MarginTop>" +
          "  <MarginLeft>0.5in</MarginLeft>" +
          "  <MarginRight>0.5in</MarginRight>" +
          "  <MarginBottom>0.5in</MarginBottom>" +
          "</DeviceInfo>";

                byte[] bytes = ReportViewer1.LocalReport.Render("PDF", devinfo, out mimeType, out encoding, out extension, out streamids, out warnings);


                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                string extfile = DateTime.Now.ToString("dd_MM_yyyy.");
                Response.AddHeader("content-disposition", "attatchment; filename=AST_sel_tindakan_" + extfile + extension);
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
                clr();

            }
            else if (countRow == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod tidak dijumpai. Sila pastikan semua maklumat dimasukkan dengan betul.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            //txtError.Text = ex.ToString();

        }
    }

    protected void Button51_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt_2 = new DataTable();
            Page.Header.Title = "MAKLUMAT ADUAN PENYELENGGARAN ASET";
            if (TextBox1.Text != "" && TextBox4.Text != "" && DD_stspemeri.SelectedValue != "")
            {
                //DataTable ddokdicno1 = new DataTable();
                //ddokdicno1 = DBCon.Ora_Execute_table("select sas_staff_no from ast_staff_asset SA Left join hr_staff_profile as SF on SF.stf_staff_no=SA.sas_staff_no  where stf_icno='" + Session["New"].ToString() + "'");
                //if (ddokdicno1.Rows.Count != 0)
                //{
                string datedari = string.Empty, fromdate = string.Empty, datedari1 = string.Empty, todate = string.Empty;
                if (TextBox1.Text != "")
                {
                    datedari = TextBox1.Text;
                    DateTime dt2 = DateTime.ParseExact(datedari, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    fromdate = dt2.ToString("yyyy-MM-dd");
                }

                if (TextBox4.Text != "")
                {
                    datedari1 = TextBox4.Text;
                    DateTime dt1 = DateTime.ParseExact(datedari1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    todate = dt1.ToString("yyyy-MM-dd");
                }

                string abcd = TextBox13.Text;
                DataSet dsCustomers = GetData("select sas_asset_id,stf_name,js.ast_justifikasi_desc,org_name,hr_jaw_desc as hr_jaba_desc,ast_kategori_desc,ast_subkateast_desc,ast_jeniaset_desc,ast_pemerikksaan_desc,sas_repair_amt,sas_justify_cd,sas_repair_amt,sas_justify_cd ,ISNULL(sas_spv_remark,'') sas_spv_remark,ISNULL(sas_spv_id,'') sas_spv_id,case when format(ISNULL(sas_spv_dt,''),'dd/MM/yyyy') = '01/01/1900' then '' else  format(sas_spv_dt,'dd/MM/yyyy') end as sas_spv_dt,ISNULL(sas_mgt_remark,'') sas_mgt_remark,ISNULL(sas_mgt_id,'') sas_mgt_id,case when format(ISNULL(sas_mgt_dt,''),'dd/MM/yyyy') = '01/01/1900' then '' else  format(sas_mgt_dt,'dd/MM/yyyy') end as sas_mgt_dt,case when format(ISNULL(sas_approve_dt,''),'dd/MM/yyyy') = '01/01/1900' then '' else  format(sas_approve_dt,'dd/MM/yyyy') end as sas_approve_dt,ISNULL(sas_keyin_id,'') sas_keyin_id,sas_upd_id,sas_upd_dt from ast_staff_asset as AST left join hr_staff_profile as HRS on HRS.stf_staff_no=AST.sas_staff_no  left join hr_organization ORG on ORG.org_gen_id=AST.sas_org_id left join Ref_hr_Jawatan JB on JB.hr_jaw_Code=HRS.stf_curr_post_cd  left join Ref_ast_kategori AK on  AK.ast_kategori_code=AST.sas_asset_cat_cd left join Ref_ast_sub_kategri_Aset SKA on  SKA.ast_subkateast_Code=AST.sas_asset_sub_cat_cd left join Ref_ast_jenis_aset AJA on AJA.ast_jeniaset_Code=AST.sas_asset_type_cd  left join Ref_ast_sts_pemerikksaan as PEM on PEM.ast_pemerikksaan_code=AST.sas_cond_sts_cd left join Ref_ast_justifikasi as js on js.ast_justifikasi_code=AST.sas_justify_cd where sas_inspect_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fromdate + "'), 0) and sas_inspect_dt<=DATEADD(day, DATEDIFF(day, 0, '" + todate + "'), +1) and sas_cond_sts_cd='" + DD_stspemeri.SelectedValue + "'");
                dt = dsCustomers.Tables[0];


                DataSet dsCustomers1 = GetData("select sas_spv_remark,sas_spv_id, case when format(sas_spv_dt,'dd/MM/yyyy') = '01/01/1900' then '' else  format(sas_spv_dt,'dd/MM/yyyy') end as sas_spv_dt,sas_mgt_remark,sas_mgt_id,case when format(sas_mgt_dt,'dd/MM/yyyy') = '01/01/1900' then '' else  format(sas_mgt_dt,'dd/MM/yyyy') end as sas_mgt_dt,case when format(sas_approve_dt,'dd/MM/yyyy') = '01/01/1900' then '' else  format(sas_approve_dt,'dd/MM/yyyy') end as sas_approve_dt,sas_keyin_id,stf_curr_post_cd,sas_staff_no,stf_name,hr_jaw_desc as hr_jaba_desc from ast_staff_asset left join hr_staff_profile as hr on hr.stf_staff_no=sas_staff_no left join Ref_hr_Jawatan as jw on jw.hr_jaw_Code=hr.stf_curr_post_cd where sas_asset_id='" + TextBox12.Text + "'");
                dt_2 = dsCustomers1.Tables[0];
                //}
            }

            ReportViewer1.Reset();

            List<DataRow> listResult = dt.AsEnumerable().ToList();
            listResult.Count();
            int countRow = 0;
            countRow = listResult.Count();

            //if (countRow != 0)
            //{
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("DataSet1", dt);
            ReportDataSource rds1 = new ReportDataSource("DataSet2", dt_2);
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.DataSources.Add(rds1);
            //Path
            ReportViewer1.LocalReport.ReportPath = "Aset/AST_sel_tindakan.rdlc";
            //Parameters
            string ss1 = string.Empty;
            if (DD_stspemeri.SelectedValue == "")
            {
                ss1 = "";
            }
            else
            {
                ss1 = DD_stspemeri.SelectedItem.Text;
            }

            ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("d1",TextBox1.Text),
                     new ReportParameter("d2",TextBox4.Text),
                     new ReportParameter("d3",ss1 ),
                     };


            ReportViewer1.LocalReport.SetParameters(rptParams);
            //Refresh
            ReportViewer1.LocalReport.Refresh();

            Warning[] warnings;

            string[] streamids;

            string mimeType;

            string encoding;

            string extension;

            string devinfo = "<DeviceInfo>" +
      "  <OutputFormat>PDF</OutputFormat>" +
      "  <PageSize>A4</PageSize>" +
      "  <PageWidth>8in</PageWidth>" +
      "  <PageHeight>11in</PageHeight>" +
      "  <MarginTop>0.25in</MarginTop>" +
      "  <MarginLeft>0.5in</MarginLeft>" +
      "  <MarginRight>0.5in</MarginRight>" +
      "  <MarginBottom>0.5in</MarginBottom>" +
      "</DeviceInfo>";

            byte[] bytes = ReportViewer1.LocalReport.Render("PDF", devinfo, out mimeType, out encoding, out extension, out streamids, out warnings);


            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            string extfile = DateTime.Now.ToString("dd_MM_yyyy.");
            Response.AddHeader("content-disposition", "attatchment; filename=AST_sel_tindakan_" + extfile + extension);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
            clr();

            //}
            //else if (countRow == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Rekod tidak dijumpai. Sila pastikan semua maklumat dimasukkan dengan betul.');", true);
            //}
        }
        catch (Exception ex)
        {
            //txtError.Text = ex.ToString();

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
    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_kel_pemeriksaan.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_kel_pemeriksaan_view.aspx");
    }

    
}