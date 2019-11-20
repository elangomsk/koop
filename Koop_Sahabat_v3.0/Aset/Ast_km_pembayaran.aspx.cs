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

public partial class Ast_km_pembayaran : System.Web.UI.Page
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
    string CommandArgument1 = string.Empty,
        CommandArgument2 = string.Empty,
        CommandArgument3 = string.Empty,
        CommandArgument4 = string.Empty,
        CommandArgument5 = string.Empty,
        CommandArgument6 = string.Empty,
        CommandArgument7 = string.Empty;
    string nama_Pembekal = string.Empty;
    string id_Pembekal = string.Empty;
    string Alamat_Pembekal = string.Empty;
    string accno_Pembekal = string.Empty;
    string namabank_Pembekal = string.Empty;
    string oid = string.Empty;
    string sdd = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2()})";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;
                bank_nama();
                bind3();
                TextBox5.Attributes.Add("Readonly", "Readonly");
                TextBox11.Attributes.Add("Readonly", "Readonly");
                DD_NAMABANK.Attributes.Add("Readonly", "Readonly");
                TextBox10.Attributes.Add("Readonly", "Readonly");
                TextBox3.Attributes.Add("Readonly", "Readonly");

                TextBox4.Attributes.Add("Readonly", "Readonly");
                TextBox6.Attributes.Add("Readonly", "Readonly");
                TextBox1.Attributes.Add("Readonly", "Readonly");
                TextBox8.Attributes.Add("Readonly", "Readonly");
                TextBox9.Attributes.Add("Readonly", "Readonly");
                TextBox12.Attributes.Add("Readonly", "Readonly");

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
                        get_supdet = DBCon.Ora_Execute_table("select * from ast_supplier where ID='" + varName1 + "'");
                        if (get_supdet.Rows.Count != 0)
                        {
                            TextBox3.Text = get_supdet.Rows[0]["sup_id"].ToString().Trim();
                            TextBox5.Text = get_supdet.Rows[0]["sup_name"].ToString().Trim();
                            TextBox10.Text = get_supdet.Rows[0]["sup_bank_acc_no"].ToString();
                            DD_NAMABANK.SelectedValue = get_supdet.Rows[0]["sup_bank_cd"].ToString().Trim();
                            TextBox11.Value = get_supdet.Rows[0]["sup_address"].ToString().Trim();
                        }
                    }
                }
            }
            catch
            {

            }
        }
    }

   

    protected void gv_refdata_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        bind3();
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
    void view_details()
    {

        try
        {
            DataTable dd_cmp = new DataTable();
            dd_cmp = DBCon.Ora_Execute_table("select *,sp1.stf_name as np,FORMAT(cmp_complaint_dt,'dd/MM/yyyy', 'en-us') cmp_action_dt1 from ast_complaint left join hr_staff_profile as sp1 on cmp_action_id=sp1.stf_staff_no where cmp_id='" + lbl_name.Text + "'");
            DataTable ddokdicno = new DataTable();
            ddokdicno = DBCon.Ora_Execute_table("select stf_staff_no,stf_name,org_name,hr_jaba_desc,hr_jaw_desc From hr_staff_profile as SP left join Ref_hr_jabatan as jb on jb.hr_jaba_Code=SP.stf_curr_dept_cd left join Ref_hr_Jawatan as JW on JW.hr_jaw_Code=SP.stf_curr_post_cd left join hr_organization as ho on ho.org_gen_id=SP.str_curr_org_cd  where stf_staff_no='" + dd_cmp.Rows[0]["cmp_staff_no"].ToString() + "'");
            if (ddokdicno.Rows.Count != 0)
            {
                TextBox4.Text = ddokdicno.Rows[0]["stf_staff_no"].ToString();
                TextBox6.Text = ddokdicno.Rows[0]["stf_name"].ToString();
                TextBox1.Text = ddokdicno.Rows[0]["org_name"].ToString();
                TextBox8.Text = ddokdicno.Rows[0]["hr_jaba_desc"].ToString();
                TextBox9.Text = lbl_name.Text;
                TextBox12.Text = dd_cmp.Rows[0]["cmp_action_dt1"].ToString();
            }
            DataTable ddicno_astid = new DataTable();
            ddicno_astid = DBCon.Ora_Execute_table("select cmp_invois_no,cmp_invois_amt,sup_id,sup_name,sup_address,sup_bank_cd,sup_bank_acc_no from ast_complaint left join ast_supplier c1 on c1.sup_id=cmp_supplier_id where cmp_id='" + lbl_name.Text + "' and cmp_invois_no !='' and cmp_supplier_id != ''");
            if (ddicno_astid.Rows.Count != 0)
            {
                TextBox2.Text = ddicno_astid.Rows[0]["cmp_invois_no"].ToString();
                TextBox3.Text = ddicno_astid.Rows[0]["sup_id"].ToString();
                TextBox7.Text = double.Parse(ddicno_astid.Rows[0]["cmp_invois_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                TextBox5.Text = ddicno_astid.Rows[0]["sup_name"].ToString();
                TextBox11.Value = ddicno_astid.Rows[0]["sup_address"].ToString();
                bank_nama();
                DD_NAMABANK.SelectedValue = ddicno_astid.Rows[0]["sup_bank_cd"].ToString().Trim();
                TextBox10.Text = ddicno_astid.Rows[0]["sup_bank_acc_no"].ToString();

            }
            else
            {
                clr_fld();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void bank_nama()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select Bank_Code,Bank_Name from Ref_Nama_Bank where Status='A' order by Bank_Name asc";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_NAMABANK.DataSource = dt;
            DD_NAMABANK.DataBind();
            DD_NAMABANK.DataTextField = "Bank_Name";
            DD_NAMABANK.DataValueField = "Bank_Code";
            DD_NAMABANK.DataBind();
            DD_NAMABANK.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

   
    protected void Button5_Click1(object sender, EventArgs e)
    {
        if (lbl_name.Text != "" && TextBox2.Text != "" && TextBox7.Text != "" && TextBox3.Text != "")
        {

            string Inssql = "update ast_complaint set cmp_invois_no='" + TextBox2.Text + "',cmp_invois_amt='" + TextBox7.Text + "',cmp_supplier_id='" + TextBox3.Text + "',cmp_upd_id='" + Session["New"].ToString() + "',cmp_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where cmp_id='" + lbl_name.Text + "'";
            Status = DBCon.Ora_Execute_CommamdText(Inssql);
            if (Status == "SUCCESS")
            {
                Session["validate_success"] = "SUCCESS";
                Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                Response.Redirect("../Aset/Ast_km_pembayaran_view.aspx");
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Tarikh.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    void clr_fld()
    {
        TextBox2.Text = "";
        TextBox7.Text = "";
        TextBox3.Text = "";
        TextBox5.Text = "";
        DD_NAMABANK.SelectedItem.Text = "";
        TextBox11.Value = "";
        TextBox10.Text = "";
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_km_pembayaran.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_km_pembayaran_view.aspx");
    }

    
}