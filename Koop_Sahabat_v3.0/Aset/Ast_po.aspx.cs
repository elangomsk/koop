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

public partial class Ast_po : System.Web.UI.Page
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
    string userid = string.Empty;
    string Status = string.Empty;
    string oid = string.Empty;
    string sdd = string.Empty;
    string var_nam = string.Empty;
    string CommandArgument1 = string.Empty, CommandArgument2 = string.Empty, CommandArgument3 = string.Empty, CommandArgument4 = string.Empty, CommandArgument5 = string.Empty, CommandArgument6 = string.Empty, CommandArgument7 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;
                txtnokak.Attributes.Add("Readonly", "Readonly");
                txtnamekak.Attributes.Add("Readonly", "Readonly");
                txtorg.Attributes.Add("Readonly", "Readonly");
                txtjab.Attributes.Add("Readonly", "Readonly");
                txtunit.Attributes.Add("Readonly", "Readonly");
                TextBox2.Attributes.Add("Readonly", "Readonly");
                txtnotel.Attributes.Add("Readonly", "Readonly");
                txtalamat.Attributes.Add("Readonly", "Readonly");
                bind();
                bind3();
                kat();
                if (samp != "")
                {
                    lbl_name.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    txtnopo.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    view_details();
                    ver_id.Text = "1";

                }
                else
                {
                    Button2.Visible = false;
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


    protected void gv_refdata_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
            GridView2.PageIndex = e.NewPageIndex;
            bind3();
            bind1();

    }

    //protected void Carian_Click(object sender, EventArgs e)
    //{
    //    view_details();
    //}
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
                    txtnopo.Text = dd1.Rows[0]["pur_po_no"].ToString();
                    if (dd1.Rows[0]["pur_verify_sts_cd"].ToString() == "01" || dd1.Rows[0]["pur_approve_sts_cd"].ToString() == "01")
                    {
                        Button2.Visible = false;
                        Button1.Visible = false;
                        Button3.Visible = false;

                    }
                    else
                    {
                        Button2.Visible = false;
                        Button1.Visible = true;
                        Button3.Visible = true;
                    }

                    bind1();
                }
                else
                {
                    bind1();
                    Button2.Visible = false;
                    Button1.Visible = true;
                    Button3.Visible = true;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
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
    
  
    protected void lblSubItemName_Click(object sender, EventArgs e)
    {
        DataTable dd1 = new DataTable();
        dd1 = dbcon.Ora_Execute_table("select * from ast_purchase where pur_po_no='" + txtnopo.Text + "'");
        if (dd1.Rows[0]["pur_verify_sts_cd"].ToString() == "01" || dd1.Rows[0]["pur_approve_sts_cd"].ToString() == "01")
        {
            Button2.Visible = false;
            Button1.Visible = false;
            Button3.Visible = false;

        }
        else
        {
            Button1.Visible = false;
            Button2.Visible = true;
            Button3.Visible = true;
        }

        LinkButton btn = (LinkButton)sender;
        string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
        CommandArgument1 = commandArgs[0];
        CommandArgument2 = commandArgs[1];
        CommandArgument3 = commandArgs[2];
        CommandArgument4 = commandArgs[3];
        CommandArgument5 = commandArgs[4];
        CommandArgument6 = commandArgs[5];
        CommandArgument7 = commandArgs[6];
        subkat();
        oid = CommandArgument3;
        sdd = CommandArgument2;

        DataTable dtorg = new DataTable();
        dtorg = dbcon.Ora_Execute_table("select ast_kategori_code from Ref_ast_kategori where ast_kategori_code='" + sdd + "'");

        DropDownList5.SelectedValue = dtorg.Rows[0][0].ToString();
        DataTable dtsub = new DataTable();
        subkat();
        dtsub = dbcon.Ora_Execute_table("select ast_subkateast_Code from Ref_ast_sub_kategri_Aset where ast_kategori_code='" + sdd + "' and ast_subkateast_Code='" + oid + "'");
        DropDownList6.SelectedValue = dtsub.Rows[0][0].ToString();
        DataTable dtjen = new DataTable();
        dtjen = dbcon.Ora_Execute_table("select ast_jeniaset_Code from Ref_ast_jenis_aset where ast_cat_code='" + sdd + "' and ast_sub_cat_code='" + oid + "' and ast_jeniaset_Code='" + CommandArgument4 + "'");
        jenaset();
        DropDownList7.SelectedValue = dtjen.Rows[0][0].ToString();
        DataTable dtnam = new DataTable();
        dtnam = dbcon.Ora_Execute_table("select cas_asset_cd,cas_asset_desc  from ast_cmn_asset where cas_asset_cat_cd='" + sdd + "' and cas_asset_sub_cat_cd='" + oid + "' and cas_asset_type_cd='" + CommandArgument4 + "' and cas_asset_cd='" + CommandArgument1 + "'");
        //cmnaset();
        TextBox3.Text = dtnam.Rows[0]["cas_asset_desc"].ToString();
        txtkuan.Text = CommandArgument5;
        txtamt.Text = double.Parse(CommandArgument6).ToString("C").Replace("$", "");
        DataTable dd8 = new DataTable();
        dd8 = dbcon.Ora_Execute_table("select pur_supplier_id from ast_purchase where pur_po_no='" + txtnopo.Text + "' and pur_asset_id='" + CommandArgument7 + "'");
        DataTable dd9 = new DataTable();
        dd9 = dbcon.Ora_Execute_table("select sup_name,sup_phone_no,sup_address from ast_supplier where sup_id='" + dd8.Rows[0][0].ToString() + "'");
        TextBox1.Text = CommandArgument7;
        if (dd9.Rows.Count > 0)
        {
            TextBox2.Text = dd9.Rows[0][0].ToString();
            txtnotel.Text = dd9.Rows[0][1].ToString();
            txtalamat.Value = dd9.Rows[0][2].ToString();
        }
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
    //protected void lblSubItem_Click(object sender, EventArgs e)
    //{

    //    LinkButton btn = (LinkButton)sender;
    //    string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
    //    CommandArgument1 = commandArgs[0];
    //    CommandArgument2 = commandArgs[1];
    //    oid = CommandArgument1;
    //    sdd = CommandArgument2;
    //    bind4();
    //    bind1();
    //}
    protected void Reset_btn(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    public void bind()
    {
        SqlConnection conn = new SqlConnection(cs);
        string query2 = "select stf_staff_no,stf_name,str_curr_org_cd,ho.org_name,stf_curr_dept_cd,rhj.hr_jaba_desc,stf_curr_unit_cd,rhu.hr_unit_desc from hr_staff_profile hsp left join hr_organization ho on ho.org_gen_id=hsp.str_curr_org_cd left join Ref_hr_jabatan rhj on rhj.hr_jaba_Code=hsp.stf_curr_dept_cd left join Ref_hr_unit rhu on rhu.hr_unit_Code=hsp.stf_curr_unit_cd  where stf_staff_no='" + Session["new"].ToString() + "'";
        conn.Open();
        var sqlCommand2 = new SqlCommand(query2, conn);
        var sqlReader2 = sqlCommand2.ExecuteReader();
        while (sqlReader2.Read())
        {
            txtnokak.Text = (string)sqlReader2["stf_staff_no"].ToString().Trim();
            txtnamekak.Text = (string)sqlReader2["stf_name"].ToString().Trim();
            txtorg.Text = (string)sqlReader2["org_name"].ToString().Trim();
            txtjab.Text = (string)sqlReader2["hr_jaba_desc"].ToString().Trim();
            txtunit.Text = (string)sqlReader2["hr_unit_desc"].ToString().Trim();
            orgcd.Text = (string)sqlReader2["str_curr_org_cd"].ToString();
        }
        sqlReader2.Close();

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

            TextBox2.Text = (string)sqlReader2["sup_name"].ToString().Trim();
            txtnotel.Text = (string)sqlReader2["sup_phone_no"].ToString().Trim();
            txtalamat.Value = (string)sqlReader2["sup_address"].ToString().Trim();

        }
        sqlReader2.Close();

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
            DropDownList5.DataSource = dt;
            DropDownList5.DataTextField = "ast_kategori_desc";
            DropDownList5.DataValueField = "ast_kategori_code";
            DropDownList5.DataBind();
            DropDownList5.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            //DropDownList1.DataSource = dt;
            //DropDownList1.DataTextField = "ast_kategori_desc";
            //DropDownList1.DataValueField = "ast_kategori_code";
            //DropDownList1.DataBind();
            //DropDownList1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
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

            string com = "select ast_subkateast_Code,UPPER(ast_subkateast_desc) as ast_subkateast_desc from Ref_ast_sub_kategri_Aset where ast_kategori_Code='" + DropDownList5.SelectedItem.Value + "'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList6.DataSource = dt;
            DropDownList6.DataTextField = "ast_subkateast_desc";
            DropDownList6.DataValueField = "ast_subkateast_Code";
            DropDownList6.DataBind();
            DropDownList6.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
             
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
            string com1 = "select * from Ref_ast_jenis_aset where Status='A' and ast_cat_Code='" + DropDownList5.SelectedValue + "' and ast_sub_cat_Code = '" + DropDownList6.SelectedValue + "'";
            SqlDataAdapter adpt = new SqlDataAdapter(com1, con);
            DataTable dt1 = new DataTable();
            adpt.Fill(dt1);
            DropDownList7.DataSource = dt1;
            DropDownList7.DataTextField = "ast_jeniaset_desc";
            DropDownList7.DataValueField = "ast_jeniaset_Code";
            DropDownList7.DataBind();
            DropDownList7.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            bind1();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

  
    public void bind1()
    {

        //SqlCommand cmd2 = new SqlCommand("select pur_asset_cat_cd,raka.ast_kategori_desc,pur_asset_sub_cat_cd,rska.ast_subkateast_desc,pur_asset_type_cd,raja.ast_jeniaset_desc,pur_asset_cd,rsks.ast_kodast_desc,pur_asset_qty, format(pur_asset_amt,'###.00') as pur_asset_amt,format(pur_asset_tot_amt,'###.00') as pur_asset_tot_amt from ast_purchase ap left join Ref_ast_kategori raka on raka.ast_kategori_code=ap.pur_asset_cat_cd left join Ref_ast_sub_kategri_Aset rska on rska.ast_subkateast_Code=ap.pur_asset_sub_cat_cd left join Ref_ast_jenis_aset raja on raja.ast_jeniaset_Code=ap.pur_asset_type_cd left join Ref_ast_kod_aset rsks on rsks.ast_kodast_Code=ap.pur_asset_type_cd WHERE pur_po_no ='" + txtnopo.Text + "' and pur_del_ind is null", con);
        SqlCommand cmd2 = new SqlCommand("select pur_asset_cat_cd,pur_asset_cd,raka.ast_kategori_desc,pur_asset_sub_cat_cd,rska.ast_subkateast_desc,pur_asset_type_cd,raja.ast_jeniaset_desc,pur_asset_cd,rsks.cas_asset_desc,pur_asset_qty, pur_asset_amt,pur_asset_tot_amt,pur_asset_id from ast_purchase ap left join Ref_ast_kategori raka on raka.ast_kategori_code=ap.pur_asset_cat_cd left join Ref_ast_sub_kategri_Aset rska on rska.ast_subkateast_Code=ap.pur_asset_sub_cat_cd left join Ref_ast_jenis_aset raja on raja.ast_jeniaset_Code=ap.pur_asset_type_cd and raja.ast_sub_cat_Code=ap.pur_asset_sub_cat_cd and raja.ast_cat_Code=ap.pur_asset_cat_cd left join ast_cmn_asset rsks on rsks.cas_asset_cd=ap.pur_asset_cd and rsks.cas_asset_sub_cat_cd=ap.pur_asset_sub_cat_cd and rsks.cas_asset_type_cd=ap.pur_asset_type_cd  WHERE pur_po_no ='" + txtnopo.Text + "' and ISNULL(pur_del_ind,'') != 'D'", con);
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
            //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Rekod Tidak Wujud');", true);
        }
        else
        {
            GridView1.DataSource = ds2;
            GridView1.DataBind();
        }


    }

    protected void gv_refdata_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bind1();

    }

  
    public void bind3()
    {
       
        SqlCommand cmd4 = new SqlCommand("select asu.ID,sup_asset_cat_cd,raka.ast_kategori_desc,sup_asset_sub_cat_cd,rask.ast_subkateast_desc,sup_id,sup_name,sup_bumi_ind,sup_gst_ind from ast_supplier asu left join Ref_ast_kategori raka on raka.ast_kategori_code=asu.sup_asset_cat_cd left join Ref_ast_sub_kategri_Aset rask on rask.ast_subkateast_Code=asu.sup_asset_sub_cat_cd", con);
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtnopo.Text != "" && TextBox2.Text != "")
            {
                //DataTable dd = new DataTable();
                //dd = dbcon.Ora_Execute_table("select pur_po_no from ast_purchase where pur_po_no='" + txtnopo.Text + "'");
                //if (dd.Rows.Count == 0)
                //{
                string scnt = string.Empty, scnt1 = string.Empty;
                string sv1 = string.Empty, sv2 = string.Empty, sv3 = string.Empty, sv4 = string.Empty;
                DataTable dd1 = new DataTable();
                dd1 = dbcon.Ora_Execute_table("select org_gen_id from hr_organization where org_gen_id='" + orgcd.Text + "'");
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
                dd8 = dbcon.Ora_Execute_table("select sup_id,sup_name from ast_supplier where sup_name='" + TextBox2.Text.Replace("'", "''") + "'");
                if (dd8.Rows.Count > 0)
                {
                    decimal value = Convert.ToDecimal(txtkuan.Text, CultureInfo.InvariantCulture);

                    string samt1 = string.Empty;
                    if (txtamt.Text == "")
                    {
                        samt1 = "0.00";
                    }
                    else
                    {
                        samt1 = txtamt.Text;
                    }

                    decimal value1 = Convert.ToDecimal(samt1, CultureInfo.InvariantCulture);
                    decimal tamt = value * value1;

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

                    DataTable sel_cmn_ast = new DataTable();
                    sel_cmn_ast = dbcon.Ora_Execute_table("select top(1) * from ast_cmn_asset where cas_asset_cat_cd='" + DropDownList5.SelectedValue + "' order by cas_asset_cd desc");

                    if (sel_cmn_ast.Rows.Count != 0)
                    {
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
                    ddokdicno_kem = dbcon.Ora_Execute_table("select  cas_asset_desc,cas_asset_cd From ast_cmn_asset where cas_asset_cat_cd='" + DropDownList5.SelectedValue + "' and cas_asset_sub_cat_cd='" + DropDownList6.SelectedValue + "' and cas_asset_type_cd='" + DropDownList7.SelectedValue + "' and cas_asset_cd='" + scnt + "' ");
                    if (ddokdicno_kem.Rows.Count == 0)
                    {
                        string Inssql_cmn = "insert into ast_cmn_asset (cas_asset_cat_cd,cas_asset_sub_cat_cd,cas_asset_type_cd,cas_asset_cd,cas_asset_desc,cas_crt_id,cas_crt_dt) values('" + DropDownList5.SelectedValue + "','" + DropDownList6.SelectedValue + "','" + DropDownList7.SelectedValue + "','" + scnt + "','" + TextBox3.Text.Replace("'", "''") + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                        Status = dbcon.Ora_Execute_CommamdText(Inssql_cmn);
                    }
                    else
                    {
                        scnt = ddokdicno_kem.Rows[0][1].ToString();
                        Status = "SUCCESS";
                    }
                    if (Status == "SUCCESS")
                    {
                        DataTable dtt = new DataTable();
                        dtt = dbcon.Ora_Execute_table("insert into ast_purchase(pur_po_no,pur_apply_staff_no,pur_apply_staff_name,pur_apply_org_id,pur_dept_cd,pur_unit_cd,pur_asset_cat_cd,pur_asset_sub_cat_cd,pur_asset_type_cd,pur_asset_cd,pur_asset_qty,pur_asset_amt,pur_asset_tot_amt,pur_supplier_id,pur_crt_id,pur_crt_dt,pur_asset_id)values('" + txtnopo.Text + "','" + txtnokak.Text + "','" + txtnamekak.Text + "','" + sv1 + "','" + sv2 + "','" + sv3 + "','" + DropDownList5.SelectedItem.Value + "','" + DropDownList6.SelectedItem.Value + "','" + DropDownList7.SelectedItem.Value + "','" + scnt + "','" + txtkuan.Text + "','" + samt1 + "','" + tamt + "','" + dd8.Rows[0][0].ToString() + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + psno + "')");
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                        clr_tclmn();
                       
                        bind1();
                        bind3();
                    }
                    else
                    {
                        
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
                //}
                //else
                //{
                //    bind1();
                //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Rekod Sudah Wujud.');", true);
                //}

            }
            else
            {
                bind1();
                alt_popup();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('" + var_nam + ".',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        catch
        {
            bind1();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('ISSUE.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
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

    void clr_tclmn()
    {
        kat();
        subkat();
        //kod();
        //jaset();
        jenaset();
        DropDownList5.SelectedValue = "";
        DropDownList6.SelectedValue = "";
        DropDownList7.SelectedValue = "";
        //DropDownList8.SelectedValue = "";
        TextBox3.Text = "";
        txtkuan.Text = "";
        txtamt.Text = "";
        TextBox2.Text = "";
        txtnotel.Text = "";
        txtalamat.Value = "";
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (txtnopo.Text != "" && TextBox2.Text != "")
        {
            DataTable dd = new DataTable();
            dd = dbcon.Ora_Execute_table("select pur_po_no from ast_purchase where pur_po_no='" + txtnopo.Text + "' and pur_asset_id='" + TextBox1.Text + "'");

            if (dd.Rows.Count > 0)
            {
                decimal value = Convert.ToDecimal(txtkuan.Text, CultureInfo.InvariantCulture);
                decimal value1 = Convert.ToDecimal(txtamt.Text, CultureInfo.InvariantCulture);
                decimal tamt = value * value1;

                string sv1 = string.Empty, sv2 = string.Empty, sv3 = string.Empty, sv4 = string.Empty;
                DataTable dd1 = new DataTable();
                dd1 = dbcon.Ora_Execute_table("select org_gen_id from hr_organization where org_gen_id='" + orgcd.Text + "'");
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
                //DataTable dd4 = new DataTable();
                //dd4 = dbcon.Ora_Execute_table("select  ast_kategori_code,ast_kategori_desc from Ref_ast_kategori where ast_kategori_desc='" + DropDownList5.SelectedItem.Text + "'");
                //DataTable dd5 = new DataTable();
                //dd5 = dbcon.Ora_Execute_table("select ast_subkateast_Code,ast_subkateast_desc from Ref_ast_sub_kategri_Aset where ast_subkateast_desc='" + DropDownList6.SelectedItem.Text + "'");
                //DataTable dd6 = new DataTable();
                //dd6 = dbcon.Ora_Execute_table("select ast_jeniaset_Code,ast_jeniaset_desc from Ref_ast_jenis_aset where ast_jeniaset_desc='" + DropDownList7.SelectedItem.Text + "'");
                //DataTable dd7 = new DataTable();
                //dd7 = dbcon.Ora_Execute_table("select cas_asset_cd,cas_asset_desc from ast_cmn_asset where cas_asset_desc=''");

                DataTable dd8 = new DataTable();
                dd8 = dbcon.Ora_Execute_table("select sup_id,sup_name from ast_supplier where sup_name='" + TextBox2.Text.Replace("'", "''") + "'");

                DataTable dtt = new DataTable();
                //dtt = dbcon.Ora_Execute_table("update ast_purchase set pur_apply_staff_no='" + txtnokak.Text + "',pur_apply_staff_name='" + txtnamekak.Text + "',pur_apply_org_id='" + dd1.Rows[0][0].ToString() + "',pur_dept_cd='" + dd2.Rows[0][0].ToString() + "' ,pur_unit_cd='" + dd3.Rows[0][0].ToString() + "',pur_asset_cat_cd= '" + dd4.Rows[0][0].ToString() + "',pur_asset_sub_cat_cd='" + dd5.Rows[0][0].ToString() + "' ,pur_asset_type_cd='" + dd6.Rows[0][0].ToString() + "' ,pur_asset_cd='" + dd7.Rows[0][0].ToString() + "',pur_asset_qty='" + txtkuan.Text + "',pur_asset_amt='" + txtamt.Text + "',pur_asset_tot_amt ='" + tamt + "',pur_supplier_id ='" + dd8.Rows[0][0].ToString() + "',pur_upd_id='" + Session["New"].ToString() + "' ,pur_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where pur_po_no ='" + txtnopo.Text + "' and pur_asset_id='" + TextBox1.Text + "'");

                dtt = dbcon.Ora_Execute_table("update ast_purchase set pur_apply_staff_no='" + txtnokak.Text + "',pur_apply_staff_name='" + txtnamekak.Text + "',pur_apply_org_id='" + sv1 + "',pur_dept_cd='" + sv2 + "' ,pur_unit_cd='" + sv3 + "',pur_asset_qty='" + txtkuan.Text + "',pur_asset_amt='" + txtamt.Text + "',pur_asset_tot_amt ='" + tamt + "',pur_supplier_id ='" + dd8.Rows[0][0].ToString() + "',pur_upd_id='" + Session["New"].ToString() + "' ,pur_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where pur_po_no ='" + txtnopo.Text + "' and pur_asset_id='" + TextBox1.Text + "'");

                //string Inssql = "update ast_purchase set pur_po_no='" + txtnopo.Text + "',pur_apply_staff_no='" + txtnokak.Text + "',pur_apply_staff_name='" + txtnamekak.Text + "',pur_apply_org_id='" + dd1.Rows[0][0].ToString() + "',pur_dept_cd='" + dd2.Rows[0][0].ToString() + "' ,pur_unit_cd='" + dd3.Rows[0][0].ToString() + "',pur_asset_cat_cd= '" + dd4.Rows[0][0].ToString() + "',pur_asset_sub_cat_cd='" + dd5.Rows[0][0].ToString() + "' ,pur_asset_type_cd='" + dd6.Rows[0][0].ToString() + "' ,pur_asset_cd='" + dd7.Rows[0][0].ToString() + "',pur_asset_qty='" + txtkuan.Text + "',pur_asset_amt='" + txtamt.Text + "',pur_asset_tot_amt ='" + tamt + "',pur_supplier_id ='" + dd8.Rows[0][0].ToString() + "',pur_upd_id='" + Session["New"].ToString() + "' ,pur_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where pur_po_no ='" + txtnopo.Text + "'";
                //Status = dbcon.Ora_Execute_CommamdText(Inssql);
                //if (Status == "SUCCESS")
                //{
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog(' Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                Button1.Visible = true;
                Button2.Visible = false;
                TextBox1.Text = "";
                clr_tclmn();
                
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Please check Not Insert in Table');", true);
                //}
                bind1();
            }

        }
        else
        {
            alt_popup();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('" + var_nam + ".',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {

        string rcount = string.Empty;
        int count = 0;
        foreach (GridViewRow gvrow in GridView1.Rows)
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
            foreach (GridViewRow gvrow in GridView1.Rows)
            {
                //Finiding checkbox control in gridview for particular row
                System.Web.UI.WebControls.CheckBox chkdelete = (System.Web.UI.WebControls.CheckBox)gvrow.FindControl("CheckBox1");
                //Condition to check checkbox selected or not
                if (chkdelete.Checked)
                {
                    //Getting EmployeeID of particular row using datakey value
                    string EmployeeID = ((LinkButton)gvrow.FindControl("lblSubItemName")).Text.ToString();
                    string sno = ((System.Web.UI.WebControls.Label)gvrow.FindControl("Label1")).Text.ToString();
                    //Getting Connection String from Web.Config
                    userid = Session["New"].ToString();
                    using (SqlConnection con = new SqlConnection(cs))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("update ast_purchase set pur_del_ind='D',pur_del_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "',pur_del_id='" + userid + "' where pur_po_no='" + txtnopo.Text + "' and pur_asset_id='" + sno + "'", con);
                        cmd.ExecuteNonQuery();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
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
    protected void Button4_Click(object sender, EventArgs e)
    {

    }
    protected void btnModalPopup_Click(object sender, EventArgs e)
    {
        //ModalPopupExtender1.Show();
        bind3();
        bind1();
        //s1.Visible = true;
    }
    //protected void Button5_Click(object sender, EventArgs e)
    //{
    //    if (txtIcNo.Text != "" || DropDownList1.SelectedValue != "" || DropDownList2.SelectedValue != "")
    //    {
    //        //ModalPopupExtender1.Show();
    //        bind2();
    //        bind1();
    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Sila Masukan Maklumat Carian');", true);
    //        //ModalPopupExtender1.Show();
    //        bind3();
    //        bind1();
    //    }
    //}


    protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select ast_subkateast_Code,ast_subkateast_desc from Ref_ast_sub_kategri_Aset where ast_kategori_Code='" + DropDownList5.SelectedItem.Value + "'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList6.DataSource = dt;
            DropDownList6.DataBind();
            DropDownList6.DataTextField = "ast_subkateast_desc";
            DropDownList6.DataValueField = "ast_subkateast_Code";
            DropDownList6.DataBind();
            DropDownList6.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            bind1();
            DropDownList7.SelectedValue = "";
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

    //        string com = "select ast_subkateast_Code,ast_subkateast_desc from Ref_ast_sub_kategri_Aset where ast_kategori_Code='" + DropDownList1.SelectedItem.Value + "'";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
    //        DataTable dt = new DataTable();
    //        adpt.Fill(dt);

    //        DropDownList2.DataSource = dt;
    //        DropDownList2.DataBind();
    //        DropDownList2.DataTextField = "ast_subkateast_desc";
    //        DropDownList2.DataValueField = "ast_subkateast_Code";
    //        DropDownList2.DataBind();
    //        DropDownList2.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    //        //ModalPopupExtender1.Show();
    //        bind1();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

  

    protected void TextBox3_TextChanged(object sender, EventArgs e)
    {
        DataTable ddokdicno_kem = new DataTable();
        ddokdicno_kem = dbcon.Ora_Execute_table("select  cas_asset_desc,cas_asset_cd From ast_cmn_asset where cas_asset_cat_cd='" + DropDownList5.SelectedValue + "' and cas_asset_sub_cat_cd='" + DropDownList6.SelectedValue + "' and cas_asset_type_cd='" + DropDownList7.SelectedValue + "' and cas_asset_desc='" + TextBox3.Text.Replace("'", "''") + "' ");
        if (ddokdicno_kem.Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Aset Keterangan telah wujud. Sila masukkan Keterangan baru.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            TextBox3.Text = "";
        }
        bind1();
        
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_po.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_po_view.aspx");
    }

    
}