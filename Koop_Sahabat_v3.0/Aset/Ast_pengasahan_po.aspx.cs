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

public partial class Ast_pengasahan_po : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    DBConnection dbcon = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();

    private static int PageSize = 20;
    DataTable dt = new DataTable();
    string useid = string.Empty;
    string Status = string.Empty;
    string oid = string.Empty;
    string sdd = string.Empty, var_nam = string.Empty;
    string CommandArgument1 = string.Empty, CommandArgument2 = string.Empty, CommandArgument3 = string.Empty, CommandArgument4 = string.Empty, CommandArgument5 = string.Empty, CommandArgument6 = string.Empty, CommandArgument7 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Cetak);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;
                kat();
                subkat();

                //jaset();
                //kod();
                bind();
                bind1();
                bind3();
                Pengesahan();
                txttper.Text = DateTime.Now.ToString("dd/MM/yyyy");
                Kemaskini.Visible = false;
                Simpan.Visible = true;
                txtalamat1.Attributes.Add("Readonly", "Readonly");
                txtnokak.Attributes.Add("Readonly", "Readonly");
                txtnama.Attributes.Add("Readonly", "Readonly");
                txtorg.Attributes.Add("Readonly", "Readonly");
                txtjab.Attributes.Add("Readonly", "Readonly");
                txtunit.Attributes.Add("Readonly", "Readonly");
                txttper.Attributes.Add("Readonly", "Readonly");
                txtnotel.Attributes.Add("Readonly", "Readonly");
                Cetak.Visible = false;

                if (samp != "")
                {
                    lbl_name.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    txtnopo.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
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

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetCompletionList(string prefixText, int count)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "select pur_po_no from ast_purchase where pur_po_no like '%' + @Search + '%' group by pur_po_no order by pur_po_no";
                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> countryNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    if (sdr.HasRows == true)
                    {
                        while (sdr.Read())
                        {
                            countryNames.Add(sdr["pur_po_no"].ToString());

                        }
                    }
                    else
                    {
                        countryNames.Add("Rekod Tidak Dijumpai.");
                    }
                }

                con.Close();
                return countryNames;
            }
        }
    }


    
    void view_details()
    {
        try
        {
            //Button7.Visible = false;
            if (txtnopo.Text != "")
            {
                DataTable dd1 = new DataTable();
                dd1 = dbcon.Ora_Execute_table("select * from ast_purchase where pur_po_no='" + txtnopo.Text + "'");
                if (dd1.Rows.Count != 0)
                {
                    app_det();
                    bind();
                    bind1();

                }
                else
                {
                    bind1();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    TextBox15.Attributes.Add("Readonly", "Readonly");
                }
            }
            else
            {
                bind1();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Maklumat No PO.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
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

            string com = "select ast_kategori_code,ast_kategori_desc from Ref_ast_kategori";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_down.DataSource = dt;
            DD_down.DataBind();
            DD_down.DataTextField = "ast_kategori_desc";
            DD_down.DataValueField = "ast_kategori_code";
            DD_down.DataBind();
            DD_down.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            //DropDownList3.DataSource = dt;
            //DropDownList3.DataBind();
            //DropDownList3.DataTextField = "ast_kategori_desc";
            //DropDownList3.DataValueField = "ast_kategori_code";
            //DropDownList3.DataBind();
            //DropDownList3.Items.Insert(0, new ListItem("--- PILIH ---", ""));

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

            string com = "select ast_subkateast_Code,ast_subkateast_desc From Ref_ast_sub_kategri_Aset where ast_kategori_Code='" + DD_down.SelectedValue + "' and status='A' order by ast_subkateast_Code ASC";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_down1.DataSource = dt;
            DD_down1.DataBind();
            DD_down1.DataTextField = "ast_subkateast_desc";
            DD_down1.DataValueField = "ast_subkateast_Code";
            DD_down1.DataBind();
            DD_down1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    DataSet Ds = new DataSet();
    //    try
    //    {

    //        string com = "select ast_subkateast_Code,ast_subkateast_desc from Ref_ast_sub_kategri_Aset where ast_kategori_Code='" + DropDownList3.SelectedItem.Value + "'";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
    //        DataTable dt = new DataTable();
    //        adpt.Fill(dt);

    //        DropDownList15.DataSource = dt;
    //        DropDownList15.DataBind();
    //        DropDownList15.DataTextField = "ast_subkateast_desc";
    //        DropDownList15.DataValueField = "ast_subkateast_Code";
    //        DropDownList15.DataBind();
    //        DropDownList15.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    //        ModalPopupExtender1.Show();
    //        bind1();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    protected void OnSelectedIndexChanged1(object sender, EventArgs e)
    {
        jenaset();
    }

    void jenaset()
    {
        DataSet Ds1 = new DataSet();
        try
        {
            string com1 = "select * from Ref_ast_jenis_aset where Status='A' and ast_cat_Code='" + DD_down.SelectedValue + "' and ast_sub_cat_Code = '" + DD_down1.SelectedValue + "'";
            SqlDataAdapter adpt = new SqlDataAdapter(com1, con);
            DataTable dt1 = new DataTable();
            adpt.Fill(dt1);
            DropDownList5.DataSource = dt1;
            DropDownList5.DataTextField = "ast_jeniaset_desc";
            DropDownList5.DataValueField = "ast_jeniaset_Code";
            DropDownList5.DataBind();
            DropDownList5.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            bind1();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //protected void OnSelectedIndexChanged2(object sender, EventArgs e)
    //{
    //    cmnaset();
    //}

    //void cmnaset()
    //{
    //    DataSet Ds1 = new DataSet();
    //    try
    //    {
    //        string com1 = "select * from ast_cmn_asset where cas_asset_cat_cd='" + DD_down.SelectedValue + "' and cas_asset_sub_cat_cd='" + DD_down1.SelectedValue + "' and cas_asset_type_cd='" + DropDownList5.SelectedValue + "'";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com1, con);
    //        DataTable dt1 = new DataTable();
    //        adpt.Fill(dt1);
    //        DropDownList1.DataSource = dt1;
    //        DropDownList1.DataTextField = "cas_asset_desc";
    //        DropDownList1.DataValueField = "cas_asset_cd";
    //        DropDownList1.DataBind();
    //        DropDownList1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    //        bind1();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    void Pengesahan()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select hr_Pengesahan_Code,UPPER(hr_Pengesahan_desc) as hr_Pengesahan_desc From Ref_hr_Pengesahan_sts where Status='A' order by hr_Pengesahan_desc ";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_pengsahan.DataSource = dt;
            dd_pengsahan.DataBind();
            dd_pengsahan.DataTextField = "hr_Pengesahan_desc";
            dd_pengsahan.DataValueField = "hr_Pengesahan_Code";
            dd_pengsahan.DataBind();
            dd_pengsahan.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    //void jaset()
    //{
    //    DataSet Ds = new DataSet();
    //    try
    //    {

    //        string com = "select ast_jeniaset_Code,ast_jeniaset_desc from Ref_ast_jenis_aset";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
    //        DataTable dt = new DataTable();
    //        adpt.Fill(dt);
    //        DropDownList5.DataSource = dt;
    //        DropDownList5.DataBind();
    //        DropDownList5.DataTextField = "ast_jeniaset_desc";
    //        DropDownList5.DataValueField = "ast_jeniaset_Code";
    //        DropDownList5.DataBind();
    //        DropDownList5.Items.Insert(0, new ListItem("--- PILIH ---", ""));
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

    //        string com = "select ast_kodast_Code,ast_kodast_desc from Ref_ast_kod_aset";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
    //        DataTable dt = new DataTable();
    //        adpt.Fill(dt);
    //        DropDownList1.DataSource = dt;
    //        DropDownList1.DataBind();
    //        DropDownList1.DataTextField = "ast_kodast_desc";
    //        DropDownList1.DataValueField = "ast_kodast_Code";
    //        DropDownList1.DataBind();
    //        DropDownList1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    protected void DD_down_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select ast_subkateast_Code,ast_subkateast_desc from Ref_ast_sub_kategri_Aset where ast_kategori_Code='" + DD_down.SelectedItem.Value + "'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_down1.DataSource = dt;
            DD_down1.DataTextField = "ast_subkateast_desc";
            DD_down1.DataValueField = "ast_subkateast_Code";
            DD_down1.DataBind();
            DD_down1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            bind1();
            DropDownList5.SelectedValue = "";
            //DropDownList15.DataSource = dt;
            //DropDownList15.DataBind();
            //DropDownList15.DataTextField = "ast_subkateast_desc";
            //DropDownList15.DataValueField = "ast_subkateast_Code";
            //DropDownList15.DataBind();
            //DropDownList15.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public void bind()
    {
        //if (txtnopo.Text != "")
        //{
        //    DataTable dd = new DataTable();
        //    dd = dbcon.Ora_Execute_table("select pur_po_no,pur_apply_staff_no from ast_purchase where pur_po_no='" + txtnopo.Text + "'");
        //    if (dd.Rows.Count > 0)
        //    {

        //        SqlConnection conn = new SqlConnection(cs);
        //        string query2 = "select pur_apply_staff_no,pur_apply_staff_name,pur_apply_org_id,ho.org_name,pur_dept_cd,rhj.hr_jaba_desc,pur_unit_cd,rhu.hr_unit_desc from ast_purchase hsp left join hr_organization ho on ho.org_id=hsp.pur_apply_org_id left join Ref_hr_jabatan rhj on rhj.hr_jaba_Code=hsp.pur_dept_cd left join Ref_hr_unit rhu on rhu.hr_unit_Code=hsp.pur_unit_cd  where pur_po_no='" + txtnopo.Text + "'";
        //        conn.Open();
        //        var sqlCommand2 = new SqlCommand(query2, conn);
        //        var sqlReader2 = sqlCommand2.ExecuteReader();
        //        while (sqlReader2.Read())
        //        {
        //            txtnokak.Text = (string)sqlReader2["pur_apply_staff_no"].ToString().Trim();
        //            txtnama.Text = (string)sqlReader2["pur_apply_staff_name"].ToString().Trim();
        //            txtorg.Text = (string)sqlReader2["org_name"].ToString().Trim();
        //            txtjab.Text = (string)sqlReader2["hr_jaba_desc"].ToString().Trim();
        //            txtunit.Text = (string)sqlReader2["hr_unit_desc"].ToString().Trim();


        //        }
        //        sqlReader2.Close();
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Maklumat Carian Tidak Dijumpai');", true);
        //    }
        //}
        SqlConnection conn = new SqlConnection(cs);
        string query2 = "select stf_staff_no,stf_name,str_curr_org_cd,ho.org_name,stf_curr_dept_cd,rhj.hr_jaba_desc,stf_curr_unit_cd,rhu.hr_unit_desc from hr_staff_profile hsp left join hr_organization ho on ho.org_gen_id=hsp.str_curr_org_cd left join Ref_hr_jabatan rhj on rhj.hr_jaba_Code=hsp.stf_curr_dept_cd left join Ref_hr_unit rhu on rhu.hr_unit_Code=hsp.stf_curr_unit_cd  where stf_staff_no='" + Session["new"].ToString() + "'";
        conn.Open();
        var sqlCommand2 = new SqlCommand(query2, conn);
        var sqlReader2 = sqlCommand2.ExecuteReader();
        while (sqlReader2.Read())
        {
            txtnokak.Text = (string)sqlReader2["stf_staff_no"].ToString().Trim();
            txtnama.Text = (string)sqlReader2["stf_name"].ToString().Trim();
            txtorg.Text = (string)sqlReader2["org_name"].ToString().Trim();
            txtjab.Text = (string)sqlReader2["hr_jaba_desc"].ToString().Trim();
            txtunit.Text = (string)sqlReader2["hr_unit_desc"].ToString().Trim();

        }
        sqlReader2.Close();
    }

    void app_det()
    {
        DataTable DDPO1 = new DataTable();
        DDPO1 = dbcon.Ora_Execute_table("select top(1) pur_verify_sts_cd,UPPER(pur_verify_remark) as pur_verify_remark from ast_purchase where pur_po_no='" + txtnopo.Text + "' and pur_verify_sts_cd='01'");
        if (DDPO1.Rows.Count != 0)
        {
            Cetak.Visible = true;
            dd_pengsahan.Attributes.Add("Readonly", "Readonly");
            dd_pengsahan.Attributes.Add("Style", "Pointer-events:None;");
            btn_Show.Attributes.Add("Style", "display:None;");
            txtalamat.Attributes.Add("Readonly", "Readonly");
            TextBox15.Attributes.Add("Readonly", "Readonly");
            Simpan.Attributes.Add("Style", "display:None;");
            Kemaskini.Attributes.Add("Style", "display:None;");
            Button4.Attributes.Add("Style", "display:None;");
            Btn_Kemaskini.Attributes.Add("Style", "display:None;");
            dd_pengsahan.SelectedValue = DDPO1.Rows[0]["pur_verify_sts_cd"].ToString();
            txtalamat.Value = DDPO1.Rows[0]["pur_verify_remark"].ToString();
        }
        else
        {
            dd_pengsahan.Attributes.Remove("Readonly");
            dd_pengsahan.Attributes.Remove("Style");
            btn_Show.Attributes.Remove("Style");
            txtalamat.Attributes.Remove("Readonly");
            Simpan.Attributes.Remove("Style");
            TextBox15.Attributes.Add("Readonly", "Readonly");
            Button4.Attributes.Remove("Style");
            Btn_Kemaskini.Attributes.Remove("Style");
            dd_pengsahan.SelectedValue = "";
            txtalamat.Value = "";
        }
    }

    protected void gv_refdata_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSelected.PageIndex = e.NewPageIndex;
        bind1();
    }

    protected void gv_refdata_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
            bind3();
            bind1();
    }

    public void bind1()
    {
        SqlCommand cmd2 = new SqlCommand("select pur_asset_tot_amt,pur_asset_cat_cd,raka.ast_kategori_desc,pur_asset_sub_cat_cd,rska.ast_subkateast_desc,pur_asset_type_cd,rajs.ast_jeniaset_desc,pur_asset_cd,raa.cas_asset_desc,pur_asset_qty,ISNULL(pur_verify_qty,'0') as pur_verify_qty,pur_asset_amt,pur_asset_tot_amt,pur_asset_id,'0' as a11 from ast_purchase ap left join Ref_ast_kategori raka on raka.ast_kategori_Code=ap.pur_asset_cat_cd left join Ref_ast_sub_kategri_Aset rska on rska.ast_subkateast_Code=ap.pur_asset_sub_cat_cd left join Ref_ast_jenis_aset rajs on rajs.ast_jeniaset_Code=ap.pur_asset_type_cd and rajs.ast_sub_cat_Code=ap.pur_asset_sub_cat_cd and rajs.ast_cat_Code=ap.pur_asset_cat_cd left join ast_cmn_asset raa on raa.cas_asset_cd=ap.pur_asset_cd and raa.cas_asset_sub_cat_cd=ap.pur_asset_sub_cat_cd and raa.cas_asset_type_cd=ap.pur_asset_type_cd where pur_po_no ='" + txtnopo.Text + "' and ISNULL(pur_del_ind,'') != 'D'", con);
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

   
    public void bind3()
    {

        SqlCommand cmd4 = new SqlCommand("select asu.ID,sup_asset_cat_cd,raka.ast_kategori_desc,sup_asset_sub_cat_cd,rask.ast_subkateast_desc,sup_id,sup_name,sup_bumi_ind,sup_gst_ind from ast_supplier asu left join Ref_ast_kategori raka on raka.ast_kategori_Code=asu.sup_asset_cat_cd left join Ref_ast_sub_kategri_Aset rask on rask.ast_subkateast_Code=asu.sup_asset_sub_cat_cd", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd4);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            GridView2.DataSource = ds2;
            GridView2.DataBind();
            int columncount = GridView2.Rows[0].Cells.Count;
            GridView2.Rows[0].Cells.Clear();
            GridView2.Rows[0].Cells.Add(new TableCell());
            GridView2.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView2.Rows[0].Cells[0].Text = "<strong>Maklumat Carian Tidak Dijumpai</strong>";
        }
        else
        {
            GridView2.DataSource = ds2;
            GridView2.DataBind();
        }


    }


    public void bind4()
    {
        SqlConnection conn = new SqlConnection(cs);
        string query2 = "select sup_name,sup_phone_no,sup_address from ast_supplier where sup_id='" + oid + "' and sup_name='" + sdd.Replace("'", "''") + "' ";
        conn.Open();
        var sqlCommand2 = new SqlCommand(query2, conn);
        var sqlReader2 = sqlCommand2.ExecuteReader();
        while (sqlReader2.Read())
        {

            TextBox15.Text = (string)sqlReader2["sup_name"].ToString().Trim();
            txtnotel.Text = (string)sqlReader2["sup_phone_no"].ToString().Trim();
            txtalamat1.Value = (string)sqlReader2["sup_address"].ToString().Trim();

        }
        sqlReader2.Close();

    }

    protected void Carian_Click(object sender, EventArgs e)
    {
        if (txtnopo.Text != "")
        {
            DataTable dd1 = new DataTable();
            dd1 = dbcon.Ora_Execute_table("select * from ast_purchase where pur_po_no='" + txtnopo.Text + "'");
            if (dd1.Rows.Count != 0)
            {
                app_det();
                bind();
                bind1();

            }
            else
            {
                bind1();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                TextBox15.Attributes.Add("Readonly", "Readonly");
            }
        }
        else
        {
            bind1();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Maklumat No PO.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    protected void lblSubItem_Click(object sender, EventArgs e)
    {

        LinkButton btn = (LinkButton)sender;
        string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
        CommandArgument1 = commandArgs[0];
        CommandArgument2 = commandArgs[1];
        oid = CommandArgument1;
        sdd = CommandArgument2;
        bind4();
        bind1();
    }


    protected void Simpan_Click(object sender, EventArgs e)
    {
        string scnt = string.Empty, scnt1 = string.Empty;
        if (txtnopo.Text != "" && TextBox15.Text != "")
        {
            //DataTable dd = new DataTable();
            //dd = dbcon.Ora_Execute_table("select pur_po_no from ast_purchase where pur_po_no='" + txtnopo.Text + "'");
            DataTable pur_cnt = new DataTable();
            pur_cnt = dbcon.Ora_Execute_table("select count(*) as pcnt from ast_purchase where pur_po_no='" + txtnopo.Text + "'");
            string psno = string.Empty;
            if (pur_cnt.Rows[0]["pcnt"].ToString() == "0")
            {
                psno = "1";
            }
            else
            {
                psno = (double.Parse(pur_cnt.Rows[0]["pcnt"].ToString()) + 1).ToString();
            }
            //if (dd.Rows.Count == 0)
            //{

            string sv1 = string.Empty, sv2 = string.Empty, sv3 = string.Empty, sv4 = string.Empty;
            DataTable dd1 = new DataTable();
            dd1 = dbcon.Ora_Execute_table("select org_gen_id from hr_organization where org_name ='" + txtorg.Text + "'");
            if (dd1.Rows.Count != 0)
            {
                sv1 = dd1.Rows[0]["org_gen_id"].ToString();
            }
            DataTable dd2 = new DataTable();
            dd2 = dbcon.Ora_Execute_table("select hr_jaba_Code from Ref_hr_jabatan  where hr_jaba_desc='" + txtjab.Text + "'");
            if (dd2.Rows.Count != 0)
            {
                sv2 = dd2.Rows[0]["hr_jaba_Code"].ToString();
            }

            DataTable dd3 = new DataTable();
            dd3 = dbcon.Ora_Execute_table("select hr_unit_Code from Ref_hr_unit where hr_unit_desc='" + txtunit.Text + "'");
            if (dd2.Rows.Count != 0)
            {
                sv3 = dd3.Rows[0]["hr_unit_Code"].ToString();
            }

            DataTable dd8 = new DataTable();
            dd8 = dbcon.Ora_Execute_table("select sup_id,sup_name from ast_supplier where sup_name='" + TextBox15.Text.Replace("'", "''") + "'");


            decimal value = Convert.ToDecimal(TextBox13.Text, CultureInfo.InvariantCulture);
            decimal value1 = Convert.ToDecimal(txtluan.Text, CultureInfo.InvariantCulture);
            decimal tamt = value * value1;

            useid = Session["New"].ToString();

            DataTable sel_cmn_ast = new DataTable();
            sel_cmn_ast = dbcon.Ora_Execute_table("select top(1) * from ast_cmn_asset where cas_asset_cat_cd='" + DD_down.SelectedValue + "' order by cas_asset_cd desc");

            if (sel_cmn_ast.Rows.Count != 0)
            {
                //scnt = (double.Parse(sel_cmn_ast.Rows[0]["cas_asset_cd"].ToString()) + 1).ToString();
                string sval1 = (double.Parse(sel_cmn_ast.Rows[0]["cas_asset_cd"].ToString()) + 1).ToString();
                //var count = sval1.PadLeft(6, '0');
                scnt = sval1.PadLeft(6, '0');
            }
            else
            {
                if (DropDownList5.SelectedValue == "01")
                {
                    scnt1 = "1";
                }
                else if (DropDownList5.SelectedValue == "02")
                {
                    scnt1 = "2";
                }
                else if (DropDownList5.SelectedValue == "03")
                {
                    scnt1 = "3";
                }
                else if (DropDownList5.SelectedValue == "01")
                {
                    scnt1 = "4";
                }
                scnt = scnt1 + "00001";
            }
            DataTable ddokdicno_kem = new DataTable();
            ddokdicno_kem = dbcon.Ora_Execute_table("select  cas_asset_desc,cas_asset_cd From ast_cmn_asset where cas_asset_cat_cd='" + DD_down.SelectedValue + "' and cas_asset_sub_cat_cd='" + DD_down1.SelectedValue + "' and cas_asset_type_cd='" + DropDownList5.SelectedValue + "' and cas_asset_cd='" + scnt + "' ");
            if (ddokdicno_kem.Rows.Count == 0)
            {
                string Inssql_cmn = "insert into ast_cmn_asset (cas_asset_cat_cd,cas_asset_sub_cat_cd,cas_asset_type_cd,cas_asset_cd,cas_asset_desc,cas_crt_id,cas_crt_dt) values('" + DD_down.SelectedValue + "','" + DD_down1.SelectedValue + "','" + DropDownList5.SelectedValue + "','" + scnt + "','" + TextBox4.Text.Replace("'", "''") + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                Status = dbcon.Ora_Execute_CommamdText(Inssql_cmn);
            }
            else
            {
                scnt = ddokdicno_kem.Rows[0][1].ToString();
                Status = "SUCCESS";
            }
            if (Status == "SUCCESS")
            {
                string Inssql = "insert into ast_purchase(pur_asset_id,pur_po_no,pur_apply_staff_no,pur_apply_staff_name,pur_apply_org_id,pur_dept_cd,pur_unit_cd ,pur_asset_cat_cd ,pur_asset_sub_cat_cd,pur_asset_type_cd,pur_asset_cd,pur_asset_qty,pur_asset_amt,pur_asset_tot_amt,pur_supplier_id,pur_crt_id,pur_crt_dt)values('" + psno + "','" + txtnopo.Text + "','" + txtnokak.Text + "','" + txtnama.Text + "','" + sv1 + "','" + sv2 + "','" + sv3 + "','" + DD_down.SelectedItem.Value + "','" + DD_down1.SelectedItem.Value + "','" + DropDownList5.SelectedItem.Value + "','" + scnt + "','" + txtluan.Text + "','" + TextBox13.Text + "','" + tamt + "','" + dd8.Rows[0][0].ToString() + "','" + useid + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "')";
                Status = dbcon.Ora_Execute_CommamdText(Inssql);
                clear_tp2();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Please check Not Insert in Table.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
            bind1();

        }
        else
        {
            bind1();
            alt_popup();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('" + var_nam + ".',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }


    }

    void clr_flds()
    {
        DD_down.SelectedValue = "";
        DD_down1.SelectedValue = "";
        DropDownList5.SelectedValue = "";
        //DropDownList1.SelectedValue = "";
        TextBox4.Text = "";
        txtluan.Text = "";
        TextBox13.Text = "";
        TextBox15.Text = "";
        txtnopo.Text = "";
    }

    void alt_popup()
    {

        if (txtnopo.Text == "")
        {
            var_nam = "Sila Masukan Maklumat No PO";
        }
        else
        {
            var_nam = "Sila Masukan Maklumat Nama Pembekal";
        }
    }
    protected void Kemaskini_Click(object sender, EventArgs e)
    {
        if (txtnopo.Text != "")
        {
            DataTable dd = new DataTable();
            dd = dbcon.Ora_Execute_table("select pur_po_no,pur_apply_staff_no from ast_purchase where pur_po_no='" + txtnopo.Text + "' and pur_asset_id='" + TextBox3.Text + "'");

            if (dd.Rows.Count > 0)
            {
                decimal value = Convert.ToDecimal(TextBox13.Text, CultureInfo.InvariantCulture);
                decimal value1 = Convert.ToDecimal(txtluan.Text, CultureInfo.InvariantCulture);
                decimal tamt = value * value1;
                useid = Session["New"].ToString();
                DataTable dd1 = new DataTable();
                dd1 = dbcon.Ora_Execute_table("select stf_name,str_curr_org_cd,stf_curr_dept_cd ,stf_curr_unit_cd   from hr_staff_profile where  stf_staff_no='" + dd.Rows[0][1].ToString() + "'");
                DataTable dd2 = new DataTable();
                dd2 = dbcon.Ora_Execute_table("select sup_id from ast_supplier where  sup_name='" + TextBox15.Text.Replace("'", "''") + "'");
                //string Inssql = "update ast_purchase set pur_apply_staff_no='" + dd.Rows[0][1].ToString() + "',pur_apply_staff_name='" + dd1.Rows[0][0].ToString() + "',pur_apply_org_id='" + dd1.Rows[0][1].ToString() + "',pur_dept_cd='" + dd1.Rows[0][2].ToString() + "',pur_unit_cd ='" + dd1.Rows[0][3].ToString() + "',pur_asset_sub_cat_cd='" + DD_down1.SelectedItem.Value + "',pur_asset_type_cd='" + DropDownList5.SelectedItem.Value + "',pur_asset_cd='',pur_asset_qty='" + txtluan.Text + "',pur_asset_amt='" + TextBox13.Text + "',pur_asset_tot_amt='" + tamt + "',pur_supplier_id='" + dd2.Rows[0][0].ToString() + "',pur_upd_id='" + useid + "',pur_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where pur_po_no ='" + txtnopo.Text + "' and pur_asset_id='" + TextBox3.Text + "'";
                string Inssql = "update ast_purchase set pur_apply_staff_no='" + dd.Rows[0][1].ToString() + "',pur_apply_staff_name='" + dd1.Rows[0][0].ToString() + "',pur_apply_org_id='" + dd1.Rows[0][1].ToString() + "',pur_dept_cd='" + dd1.Rows[0][2].ToString() + "',pur_unit_cd ='" + dd1.Rows[0][3].ToString() + "',pur_asset_qty='" + txtluan.Text + "',pur_asset_amt='" + TextBox13.Text + "',pur_asset_tot_amt='" + tamt + "',pur_supplier_id='" + dd2.Rows[0][0].ToString() + "',pur_upd_id='" + useid + "',pur_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where pur_po_no ='" + txtnopo.Text + "' and pur_asset_id='" + TextBox3.Text + "'";
                Status = dbcon.Ora_Execute_CommamdText(Inssql);
                bind1();
                if (Status == "SUCCESS")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    clear_tp2();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan No PO.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    protected void Reset_btn(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        DataTable dd1 = new DataTable();
        dd1 = dbcon.Ora_Execute_table("select * from ast_purchase where pur_po_no='" + txtnopo.Text + "'");
        if (dd1.Rows[0]["pur_approve_sts_cd"].ToString() == "01")
        {
            Kemaskini.Visible = false;
            Simpan.Visible = false;
            Button4.Visible = false;
        }
        else
        {
            Kemaskini.Visible = true;
            Simpan.Visible = false;
            Button4.Visible = true;
        }


        LinkButton btnButton = sender as LinkButton;
        GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
        System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("Label1");
        System.Web.UI.WebControls.Label asid = (System.Web.UI.WebControls.Label)gvRow.FindControl("Label4");
        string abc = lblTitle.Text;

        DataTable ddokdicno1 = new DataTable();
        ddokdicno1 = dbcon.Ora_Execute_table("select pur_asset_cat_cd,pur_asset_sub_cat_cd,pur_asset_type_cd,pur_asset_cd,pur_asset_qty,ISNULL(pur_verify_qty,'') as pur_verify_qty,pur_asset_amt,pur_asset_tot_amt,sup_name,sup_address,sup_phone_no,c1.cas_asset_desc From ast_purchase as p left join ast_supplier as s on s.sup_id=p.pur_supplier_id left join ast_cmn_asset as c1 on c1.cas_asset_cd=p.pur_asset_cd where pur_asset_cat_cd='" + abc + "' and pur_po_no='" + txtnopo.Text + "' and pur_asset_id='" + asid.Text + "' ");
        if (ddokdicno1.Rows.Count != 0)
        {
            DD_down.SelectedValue = ddokdicno1.Rows[0]["pur_asset_cat_cd"].ToString().Trim();
            subkat();
            DD_down1.SelectedValue = ddokdicno1.Rows[0]["pur_asset_sub_cat_cd"].ToString().Trim();
            jenaset();
            DropDownList5.SelectedValue = ddokdicno1.Rows[0]["pur_asset_type_cd"].ToString();
            TextBox4.Text = ddokdicno1.Rows[0]["cas_asset_desc"].ToString();
            txtluan.Text = ddokdicno1.Rows[0]["pur_asset_qty"].ToString().Trim();
            TextBox13.Text = double.Parse(ddokdicno1.Rows[0]["pur_asset_amt"].ToString()).ToString("C").Replace("$", "");
            TextBox15.Text = ddokdicno1.Rows[0]["sup_name"].ToString().Trim();
            txtnotel.Text = ddokdicno1.Rows[0]["sup_phone_no"].ToString().Trim();
            txtalamat1.Value = ddokdicno1.Rows[0]["sup_address"].ToString().Trim();
            //bind4();
            TextBox3.Text = asid.Text;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
        TextBox1.Text = abc.ToString();
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        string rcount = string.Empty;
        int count = 0;
        foreach (GridViewRow gvrow in gvSelected.Rows)
        {
            var rb = gvrow.FindControl("CheckBox1") as System.Web.UI.WebControls.CheckBox;
            if (rb.Checked)
            {
                count++;
            }
            rcount = count.ToString();
        }
        if (rcount != "0")
        {
            foreach (GridViewRow gvrow in gvSelected.Rows)
            {
                //Finiding checkbox control in gridview for particular row
                CheckBox chkdelete = (CheckBox)gvrow.FindControl("CheckBox1");
                //Condition to check checkbox selected or not
                if (chkdelete.Checked)
                {
                    //Getting EmployeeID of particular row using datakey value
                    string EmployeeID = ((Label)gvrow.FindControl("Label1")).Text.ToString();
                    string ast_id = ((Label)gvrow.FindControl("Label4")).Text.ToString();
                    //Getting Connection String from Web.Config
                    useid = Session["New"].ToString();
                    using (SqlConnection con = new SqlConnection(cs))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("update ast_purchase set pur_del_ind='D',pur_del_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "',pur_del_id='" + useid + "' where  pur_po_no='" + txtnopo.Text + "' and pur_asset_id='" + ast_id + "'", con);
                        cmd.ExecuteNonQuery();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dihapuskan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                        con.Close();
                    }
                }
            }
        }
        else
        {            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
        bind1();
    }
    protected void Btn_Kemaskini_Click(object sender, EventArgs e)
    {
        if (txtnopo.Text != "")
        {
            DataTable dd = new DataTable();
            dd = dbcon.Ora_Execute_table("select pur_po_no,pur_asset_tot_amt from ast_purchase where pur_po_no='" + txtnopo.Text + "'");
            useid = Session["New"].ToString();
            string rcount = string.Empty;
            int count = 0;
            foreach (GridViewRow gvrow in gvSelected.Rows)
            {
                var rb = (TextBox)gvrow.Cells[1].FindControl("txt_simp");
                if (rb.Text == "0")
                {
                    count++;
                }
                rcount = count.ToString();
            }
            if (rcount == "0")
            {
                foreach (GridViewRow grdRow in gvSelected.Rows)
                {
                    TextBox tamt = (TextBox)grdRow.Cells[1].FindControl("Label21_tamt");
                    TextBox pqty = (TextBox)grdRow.Cells[1].FindControl("txtCustomerId");
                    Label sqno = (Label)grdRow.Cells[1].FindControl("Label4");
                    CheckBox chk_cb = (CheckBox)grdRow.Cells[1].FindControl("CheckBox1");
                    string ind1 = string.Empty;
                    if (chk_cb.Checked == true)
                    {
                        ind1 = "D";
                    }
                    else
                    {
                        ind1 = "";
                    }
                    string Inssql = "update ast_purchase set pur_del_ind='" + ind1 + "',pur_asset_tot_amt='" + tamt.Text + "', pur_verify_qty='" + pqty.Text + "',pur_verify_sts_cd='" + dd_pengsahan.SelectedValue + "',pur_verify_remark ='" + txtalamat.Value.Replace("'", "''") + "',pur_verify_id ='" + useid + "',pur_verify_dt ='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "',pur_upd_id ='" + useid + "',pur_upd_dt ='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where pur_po_no ='" + txtnopo.Text + "' and pur_asset_id='" + sqno.Text + "'";
                    Status = dbcon.Ora_Execute_CommamdText(Inssql);

                }
                if (Status == "SUCCESS")
                {
                    app_det();
                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Please check Not Insert in Table.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
                bind1();
               
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Kuantiti Diterima.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        else
        {
            bind1();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Maklumat No PO.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }

    }

    void clear_tp2()
    {
        Kemaskini.Visible = false;
        Simpan.Visible = true;
        DD_down.SelectedValue = "";
        DD_down1.SelectedValue = "";
        DropDownList5.SelectedValue = "";
        TextBox4.Text = "";
        txtluan.Text = "";
        TextBox13.Text = "";
        TextBox15.Text = "";
        txtalamat1.Value = "";
        txtnotel.Text = "";
    }

    protected void btnGetNode_Click(object sender, EventArgs e)

    {
        string rcount = string.Empty;
        int count = 0;
        foreach (GridViewRow gvrow in GridView2.Rows)
        {
            var rb = gvrow.FindControl("chkRow") as System.Web.UI.WebControls.RadioButton;
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
                foreach (GridViewRow row in GridView2.Rows)
                {
                    System.Web.UI.WebControls.RadioButton rbn = new System.Web.UI.WebControls.RadioButton();
                    rbn = (System.Web.UI.WebControls.RadioButton)row.FindControl("chkRow");
                    if (rbn.Checked)
                    {
                        int RowIndex = row.RowIndex;
                        string varName1 = ((System.Web.UI.WebControls.Label)row.FindControl("Label2_id")).Text.ToString(); //this store the  value in varName1
                        DataTable get_supdet = new DataTable();
                        get_supdet = dbcon.Ora_Execute_table("select * from ast_supplier where ID='" + varName1 + "'");
                        CommandArgument1 = get_supdet.Rows[0]["sup_id"].ToString();
                        CommandArgument2 = get_supdet.Rows[0]["sup_name"].ToString();
                        oid = CommandArgument1;
                        sdd = CommandArgument2;
                        bind4();
                    }
                }
            }
            catch
            {

            }
        }

        bind1();
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

    protected void Btn_Cetak_Click(object sender, EventArgs e)
    {
        try
        {
            view_details();
            Page.Header.Title = "CARIAN MAKLUMAT BELIAN ASET";
            if (txtnopo.Text != "")
            {
                DataSet dsCustomers = GetData("select pur_apply_staff_no,UPPER(pur_apply_staff_name) pur_apply_staff_name,UPPER(ho.org_name) org_name,UPPER(rhj.hr_jaba_desc) hr_jaba_desc,UPPER(rhu.hr_unit_desc) hr_unit_desc,UPPER(raa.cas_asset_desc) ast_kodast_desc,UPPER(rska.ast_subkateast_desc) ast_subkateast_desc,UPPER(rajs.ast_jeniaset_desc) ast_jeniaset_desc,UPPER(raka.ast_kategori_desc) ast_kategori_desc,pur_asset_qty,pur_verify_qty,UPPER(pen.hr_Pengesahan_desc) hr_Pengesahan_desc,UPPER(pur_verify_remark) pur_verify_remark,format(pur_asset_amt,'#,##0.00') pur_asset_amt,format(pur_asset_tot_amt,'#,##0.00') pur_asset_tot_amt  from ast_purchase hsp Left join Ref_ast_kategori raka on raka.ast_kategori_Code=hsp.pur_asset_cat_cd left join ast_cmn_asset raa on raa.cas_asset_cd=hsp.pur_asset_cd and raa.cas_asset_sub_cat_cd=hsp.pur_asset_sub_cat_cd and raa.cas_asset_type_cd=hsp.pur_asset_type_cd left join Ref_ast_jenis_aset rajs on rajs.ast_jeniaset_Code=hsp.pur_asset_type_cd and rajs.ast_sub_cat_Code=hsp.pur_asset_sub_cat_cd and rajs.ast_cat_Code=hsp.pur_asset_cat_cd left join Ref_ast_sub_kategri_Aset rska on rska.ast_subkateast_Code=hsp.pur_asset_sub_cat_cd left join Ref_hr_Pengesahan_sts as pen on pen.hr_Pengesahan_Code=hsp.pur_verify_sts_cd left join hr_organization ho on ho.org_gen_id=hsp.pur_apply_org_id left join Ref_hr_jabatan rhj on rhj.hr_jaba_Code=hsp.pur_dept_cd left join Ref_hr_unit rhu on rhu.hr_unit_Code=hsp.pur_unit_cd where pur_po_no='" + txtnopo.Text + "' and ISNULL(pur_del_ind,'') != 'D'");
                dt = dsCustomers.Tables[0];
            }
            ReportViewer1.Reset();

            List<DataRow> listResult = dt.AsEnumerable().ToList();
            listResult.Count();
            int countRow = 0;
            countRow = listResult.Count();

            if (countRow != 0)
            {
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("AST_PENGESAHAN_PO", dt);
                ReportViewer1.LocalReport.DataSources.Add(rds);
                //Path
                ReportViewer1.LocalReport.ReportPath = "Aset/AST_PENGESAHAN_PO.rdlc";
                //Parameters
                //ReportParameter[] rptParams = new ReportParameter[]{
                //     //new ReportParameter("fromDate",FromDate .Text ),
                //     //new ReportParameter("toDate",ToDate .Text )
                //     //new ReportParameter("fromDate",datedari  ),
                //     //new ReportParameter("toDate",datehingga ),
                //          //new ReportParameter("caw",branch ),     
                //            //new ReportParameter("Cdate",DateTime.Now.ToString("dd/MM/yyyy") ),     
                //     //new ReportParameter("Date",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  )
                //     };


                //ReportViewer1.LocalReport.SetParameters(rptParams);
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
                string extfile = txtnopo.Text.Trim();
                Response.AddHeader("content-disposition", "attachment; filename=Pengesahan_Belian_" + extfile + "." + extension);
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();

            }
            else if (countRow == 0)
            {
                bind1();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod tidak dijumpai. Sila pastikan semua maklumat dimasukkan dengan betul.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
           
        }
        catch (Exception ex)
        {
            //txtError.Text = ex.ToString();

        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_pengasahan_po.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_pengasahan_po_view.aspx");
    }

    
}