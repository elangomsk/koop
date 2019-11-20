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

public partial class Ast_penges_terimaan : System.Web.UI.Page
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
    string sdd = string.Empty;
    string qr1 = string.Empty;
    string CommandArgument1 = string.Empty, CommandArgument2 = string.Empty, CommandArgument3 = string.Empty, CommandArgument4 = string.Empty, CommandArgument5 = string.Empty, CommandArgument6 = string.Empty, CommandArgument7 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        //string script = " $(function () {$('.select2').select2()})";
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Button1);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;

                TextBox2.Text = DateTime.Now.ToString("dd/MM/yyyy");
                TextBox1.Text = "";
                TextBox3.Attributes.Add("readonly", "readonly");
                bind1();
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

    
    void view_details()
    {
        try
        {
            if (txtnopo.Text != "")
            {
                TextBox1.Text = "";
                CheckBox2.Checked = false;
                bind1();
            }
            else
            {
                bind1();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan No PO.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void bind1()
    {

        DataTable DDPO1 = new DataTable();
        DDPO1 = dbcon.Ora_Execute_table("select top(1) *,acp_verify_sts_cd,acp_verify_remark,FORMAT(isnull(acp_verify_dt,''),'dd/MM/yyyy') as verify_date,acp_verify_id from ast_acceptance where acp_po_no='" + txtnopo.Text + "'");
        if (DDPO1.Rows.Count != 0)
        {
            DD_down.SelectedValue = DDPO1.Rows[0]["acp_verify_sts_cd"].ToString().Trim();
            txtalamat.Value = DDPO1.Rows[0]["acp_verify_remark"].ToString();
            if (DDPO1.Rows[0]["verify_date"].ToString() != "01/01/1900")
            {
                TextBox2.Text = DDPO1.Rows[0]["verify_date"].ToString();
            }
            else
            {
                TextBox2.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
            DataTable dd2 = new DataTable();
            dd2 = dbcon.Ora_Execute_table("select stf_name from hr_staff_profile where stf_staff_no='" + DDPO1.Rows[0]["acp_verify_id"].ToString() + "'");
            DataTable dd1_1 = new DataTable();
            dd1_1 = dbcon.Ora_Execute_table("select stf_name from hr_staff_profile where stf_staff_no='" + Session["New"].ToString() + "'");
            if (dd2.Rows.Count != 0)
            {
                TextBox3.Text = dd2.Rows[0]["stf_name"].ToString();
            }
            else
            {
                TextBox3.Text = dd1_1.Rows[0]["stf_name"].ToString();
            }
            if (DDPO1.Rows[0]["acp_verify_sts_cd"].ToString().Trim() == "S")
            {
                DD_down.Attributes.Add("Readonly", "Readonly");
                txtalamat.Attributes.Add("Readonly", "Readonly");
                TextBox2.Attributes.Add("Readonly", "Readonly");
                DD_down.Attributes.Add("style", "pointer-events:None;");
                Btn_Kemaskini.Attributes.Add("style", "Display:none");
            }
            else
            {
                DD_down.Attributes.Remove("Readonly");
                DD_down.Attributes.Remove("style");
                txtalamat.Attributes.Remove("Readonly");
                Btn_Kemaskini.Attributes.Remove("style");
            }

            //if (DDPO1.Rows[0]["acp_complete_ind"].ToString() == "00")
            //{
            //    Btn_Kemaskini.Attributes.Add("style", "Display:none");
            //}
            //else
            //{
            //    Btn_Kemaskini.Attributes.Remove("style");
            //}

        }
        else
        {
            DataTable dd1 = new DataTable();
            dd1 = dbcon.Ora_Execute_table("select stf_name from hr_staff_profile where stf_staff_no='" + useid + "'");
            if (dd1.Rows.Count != 0)
            {
                TextBox3.Text = dd1.Rows[0]["stf_name"].ToString();
            }
            else
            {
                TextBox3.Text = "";
            }
        }

        //SqlCommand cmd2 = new SqlCommand("select acp_do_no,acp_receive_id,format(acp_receive_dt,'dd/MM/yyyy') as acp_receive_dt ,sup_name,acp_asset_cat_cd,raka.ast_kategori_desc,acp_asset_sub_cat_cd,rask.ast_subkateast_desc,acp_asset_type_cd,raja.ast_jeniaset_desc,acp_asset_cd,rak.cas_asset_desc,pur_asset_qty,acp_receive_qty,acp_receive_remark,case when format(ISNULL(acp_receive_upd_dt,''),'dd/MM/yyyy')  = '01/01/1900'  then  format(acp_receive_dt,'dd/MM/yyyy')  else  format(ISNULL(acp_receive_upd_dt,''),'dd/MM/yyyy') end as strTarikhTerimaan,acp_seq_no,sp.stf_name  from ast_acceptance aa Left join ast_supplier asu on asu.sup_id=aa.acp_supplier_id Left join  ast_purchase ap on  ap.pur_po_no=aa.acp_po_no and ap.pur_asset_id=aa.acp_seq_no left join  Ref_ast_kategori raka on raka.ast_kategori_Code=aa.acp_asset_cat_cd  left join Ref_ast_sub_kategri_Aset rask on rask.ast_subkateast_Code=aa.acp_asset_sub_cat_cd left join Ref_ast_jenis_aset raja on raja.ast_jeniaset_Code=aa.acp_asset_type_cd left join ast_cmn_asset rak on rak.cas_asset_cd=aa.acp_asset_cd and rak.cas_asset_type_cd=aa.acp_asset_type_cd and rak.cas_asset_cat_cd=aa.acp_asset_cat_cd left join hr_staff_profile sp on sp.stf_staff_no=aa.acp_receive_id where aa.acp_po_no='" + txtnopo.Text + "'", con);
        SqlCommand cmd2 = new SqlCommand("select acp_do_no,acp_po_no,acp_receive_dt,acp_receive_id,sp.stf_name,asu.sup_name from (select acp_po_no,acp_do_no,acp_receive_id,format(acp_receive_dt,'dd/MM/yyyy') as acp_receive_dt,acp_supplier_id from ast_acceptance where acp_po_no='" + txtnopo.Text + "' group by acp_po_no,acp_do_no,acp_receive_id,acp_receive_dt,acp_supplier_id) as a left join hr_staff_profile sp on sp.stf_staff_no=a.acp_receive_id left join ast_supplier asu on asu.sup_id=a.acp_supplier_id", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            GridView2.DataSource = ds2;
            GridView2.DataBind();
            int columncount1 = GridView2.Rows[0].Cells.Count;
            GridView2.Rows[0].Cells.Clear();
            GridView2.Rows[0].Cells.Add(new TableCell());
            GridView2.Rows[0].Cells[0].ColumnSpan = columncount1;
            GridView2.Rows[0].Cells[0].Text = "<strong><center>Maklumat Carian Tidak Dijumpai</center></strong>";
        }
        else
        {
            GridView2.DataSource = ds2;
            GridView2.DataBind();

        }

        bind2();
    }

    public void bind2()
    {
        cur_qury();
        SqlCommand cmd2 = new SqlCommand("" + qr1 + "", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            GridView1.DataSource = ds2;
            GridView1.DataBind();
            int columncount1 = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columncount1;
            GridView1.Rows[0].Cells[0].Text = "<strong><center>Maklumat Carian Tidak Dijumpai</center></strong>";

        }
        else
        {

            GridView1.DataSource = ds2;
            GridView1.DataBind();
        }


    }

    void cur_qury()
    {
        if (CheckBox2.Checked == true)
        {

            qr1 = "select acp_asset_cat_cd,raka.ast_kategori_desc,acp_asset_sub_cat_cd,rask.ast_subkateast_desc,acp_asset_type_cd,raja.ast_jeniaset_desc,acp_asset_cd,rak.cas_asset_desc,pur_verify_qty,acp_receive_qty,acp_receive_remark,case when format(ISNULL(acp_receive_upd_dt,''),'dd/MM/yyyy')  = '01/01/1900'  then  format(acp_receive_dt,'dd/MM/yyyy')  else  format(ISNULL(acp_receive_upd_dt,''),'dd/MM/yyyy') end as strTarikhTerimaan   from ast_acceptance aa left join ast_supplier asu on asu.sup_id=aa.acp_supplier_id left join  ast_purchase ap on  ap.pur_po_no=aa.acp_po_no and ap.pur_asset_id=aa.acp_seq_no left join  Ref_ast_kategori raka on raka.ast_kategori_Code=aa.acp_asset_cat_cd  left join Ref_ast_sub_kategri_Aset rask on rask.ast_subkateast_Code=aa.acp_asset_sub_cat_cd left join Ref_ast_jenis_aset raja on raja.ast_jeniaset_Code=aa.acp_asset_type_cd and raja.ast_sub_cat_Code=aa.acp_asset_sub_cat_cd and raja.ast_cat_Code=aa.acp_asset_cat_cd  left join ast_cmn_asset rak on rak.cas_asset_cd=aa.acp_asset_cd and rak.cas_asset_type_cd=aa.acp_asset_type_cd and rak.cas_asset_cat_cd=aa.acp_asset_cat_cd where aa.acp_po_no='" + txtnopo.Text + "'";

        }
        else if (CheckBox2.Checked == false)
        {
            if (TextBox1.Text != "")
            {
                qr1 = "select acp_asset_cat_cd,raka.ast_kategori_desc,acp_asset_sub_cat_cd,rask.ast_subkateast_desc,acp_asset_type_cd,raja.ast_jeniaset_desc,acp_asset_cd,rak.cas_asset_desc,pur_verify_qty,acp_receive_qty,acp_receive_remark,case when format(ISNULL(acp_receive_upd_dt,''),'dd/MM/yyyy')  = '01/01/1900'  then  format(acp_receive_dt,'dd/MM/yyyy')  else  format(ISNULL(acp_receive_upd_dt,''),'dd/MM/yyyy') end as strTarikhTerimaan   from ast_acceptance aa left join ast_supplier asu on asu.sup_id=aa.acp_supplier_id left join  ast_purchase ap on  ap.pur_po_no=aa.acp_po_no and ap.pur_asset_id=aa.acp_seq_no left join  Ref_ast_kategori raka on raka.ast_kategori_Code=aa.acp_asset_cat_cd  left join Ref_ast_sub_kategri_Aset rask on rask.ast_subkateast_Code=aa.acp_asset_sub_cat_cd left join Ref_ast_jenis_aset raja on raja.ast_jeniaset_Code=aa.acp_asset_type_cd and raja.ast_sub_cat_Code=aa.acp_asset_sub_cat_cd and raja.ast_cat_Code=aa.acp_asset_cat_cd left join ast_cmn_asset rak on rak.cas_asset_cd=aa.acp_asset_cd and rak.cas_asset_type_cd=aa.acp_asset_type_cd and rak.cas_asset_cat_cd=aa.acp_asset_cat_cd where aa.acp_po_no='" + txtnopo.Text + "' and aa.acp_do_no='" + TextBox1.Text + "'";
            }
            else
            {
                qr1 = "select acp_asset_cat_cd,raka.ast_kategori_desc,acp_asset_sub_cat_cd,rask.ast_subkateast_desc,acp_asset_type_cd,raja.ast_jeniaset_desc,acp_asset_cd,rak.cas_asset_desc,pur_verify_qty,acp_receive_qty,acp_receive_remark,case when format(ISNULL(acp_receive_upd_dt,''),'dd/MM/yyyy')  = '01/01/1900'  then  format(acp_receive_dt,'dd/MM/yyyy')  else  format(ISNULL(acp_receive_upd_dt,''),'dd/MM/yyyy') end as strTarikhTerimaan   from ast_acceptance aa left join ast_supplier asu on asu.sup_id=aa.acp_supplier_id left join  ast_purchase ap on  ap.pur_po_no=aa.acp_po_no and ap.pur_asset_id=aa.acp_seq_no left join  Ref_ast_kategori raka on raka.ast_kategori_Code=aa.acp_asset_cat_cd  left join Ref_ast_sub_kategri_Aset rask on rask.ast_subkateast_Code=aa.acp_asset_sub_cat_cd left join Ref_ast_jenis_aset raja on raja.ast_jeniaset_Code=aa.acp_asset_type_cd and raja.ast_sub_cat_Code=aa.acp_asset_sub_cat_cd and raja.ast_cat_Code=aa.acp_asset_cat_cd left join ast_cmn_asset rak on rak.cas_asset_cd=aa.acp_asset_cd and rak.cas_asset_type_cd=aa.acp_asset_type_cd and rak.cas_asset_cat_cd=aa.acp_asset_cat_cd where aa.acp_po_no=''";
            }
        }

    }

    //public void bind3()
    //{

    //    SqlCommand cmd2 = new SqlCommand("select acp_asset_cat_cd,raka.ast_kategori_desc,acp_asset_sub_cat_cd,rask.ast_subkateast_desc,acp_asset_type_cd,raja.ast_jeniaset_desc,acp_asset_cd,rak.cas_asset_desc,pur_asset_qty,acp_receive_qty,acp_receive_remark,case when format(ISNULL(acp_receive_upd_dt,''),'dd/MM/yyyy')  = '01/01/1900'  then  format(acp_receive_dt,'dd/MM/yyyy')  else  format(ISNULL(acp_receive_upd_dt,''),'dd/MM/yyyy') end as strTarikhTerimaan   from ast_acceptance aa left join ast_supplier asu on asu.sup_id=aa.acp_supplier_id left join  ast_purchase ap on  ap.pur_po_no=aa.acp_po_no and ap.pur_asset_id=aa.acp_seq_no left join  Ref_ast_kategori raka on raka.ast_kategori_Code=aa.acp_asset_cat_cd  left join Ref_ast_sub_kategri_Aset rask on rask.ast_subkateast_Code=aa.acp_asset_sub_cat_cd left join Ref_ast_jenis_aset raja on raja.ast_jeniaset_Code=aa.acp_asset_type_cd left join ast_cmn_asset rak on rak.cas_asset_cd=aa.acp_asset_cd and rak.cas_asset_type_cd=aa.acp_asset_type_cd and rak.cas_asset_cat_cd=aa.acp_asset_cat_cd where aa.acp_po_no='" + txtnopo.Text + "'", con);
    //    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
    //    DataSet ds2 = new DataSet();
    //    da2.Fill(ds2);
    //    if (ds2.Tables[0].Rows.Count == 0)
    //    {

    //        GridView1.DataSource = ds2;
    //        GridView1.DataBind();
    //        int columncount1 = GridView1.Rows[0].Cells.Count;
    //        GridView1.Rows[0].Cells.Clear();
    //        GridView1.Rows[0].Cells.Add(new TableCell());
    //        GridView1.Rows[0].Cells[0].ColumnSpan = columncount1;
    //        GridView1.Rows[0].Cells[0].Text = "<strong>Maklumat Carian Tidak Dijumpai</strong>";
    //    }
    //    else
    //    {

    //        GridView1.DataSource = ds2;
    //        GridView1.DataBind();
    //    }


    //}

    protected void lblSubItemName_Click(object sender, EventArgs e)
    {

        LinkButton btn = (LinkButton)sender;
        string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
        CommandArgument1 = commandArgs[0];
        //CommandArgument2 = commandArgs[4];
        oid = CommandArgument1;
        TextBox1.Text = oid;
        bind2();
    }

   
    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox2.Checked == true)
        {
            bind2();
            bind1();
        }
        else
        {
            bind1();
        }
    }

    protected void gv_refdata_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        bind1();
        bind2();
    }

    protected void gv_refdata_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bind1();
        bind2();
    }

    protected void Reset_btn(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void Btn_Kemaskini_Click(object sender, EventArgs e)
    {
        if (txtnopo.Text != "")
        {
            if (DD_down.SelectedValue != "")
            {
                useid = Session["New"].ToString();
                DataTable dd9 = new DataTable();

                DateTime today1 = DateTime.ParseExact(TextBox2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string ctdt = today1.ToString("yyyy-MM-dd hh:mm:ss");

                DataTable dd1 = new DataTable();
                dd1 = dbcon.Ora_Execute_table("select stf_staff_no from hr_staff_profile where stf_name='" + TextBox3.Text + "'");

                dd9 = dbcon.Ora_Execute_table("update ast_acceptance set acp_verify_sts_cd ='" + DD_down.SelectedValue + "',acp_verify_remark ='" + txtalamat.Value.Replace("'", "''") + "',acp_verify_id ='" + dd1.Rows[0]["stf_staff_no"].ToString() + "',acp_verify_dt ='" + ctdt + "' where acp_po_no='" + txtnopo.Text + "' ");
                bind1();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
            else
            {
                bind1();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
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
    void cur_qury1()
    {
        if (CheckBox2.Checked == true)
        {

            qr1 = "select acp_asset_cat_cd,raka.ast_kategori_desc,acp_asset_sub_cat_cd,rask.ast_subkateast_desc,acp_asset_type_cd,raja.ast_jeniaset_desc,acp_asset_cd,rak.cas_asset_desc,pur_verify_qty,acp_receive_qty,acp_receive_remark,case when format(ISNULL(acp_receive_upd_dt,''),'dd/MM/yyyy')  = '01/01/1900'  then  format(acp_receive_dt,'dd/MM/yyyy')  else  format(ISNULL(acp_receive_upd_dt,''),'dd/MM/yyyy') end as strTarikhTerimaan   from ast_acceptance aa left join ast_supplier asu on asu.sup_id=aa.acp_supplier_id left join  ast_purchase ap on  ap.pur_po_no=aa.acp_po_no and ap.pur_asset_id=aa.acp_seq_no left join  Ref_ast_kategori raka on raka.ast_kategori_Code=aa.acp_asset_cat_cd  left join Ref_ast_sub_kategri_Aset rask on rask.ast_subkateast_Code=aa.acp_asset_sub_cat_cd left join Ref_ast_jenis_aset raja on raja.ast_jeniaset_Code=aa.acp_asset_type_cd and raja.ast_sub_cat_Code=aa.acp_asset_sub_cat_cd and raja.ast_cat_Code=aa.acp_asset_cat_cd  left join ast_cmn_asset rak on rak.cas_asset_cd=aa.acp_asset_cd and rak.cas_asset_type_cd=aa.acp_asset_type_cd and rak.cas_asset_cat_cd=aa.acp_asset_cat_cd where aa.acp_po_no='" + txtnopo.Text + "'";

        }
        else if (CheckBox2.Checked == false)
        {
            if (TextBox1.Text != "")
            {
                qr1 = "select acp_asset_cat_cd,raka.ast_kategori_desc,acp_asset_sub_cat_cd,rask.ast_subkateast_desc,acp_asset_type_cd,raja.ast_jeniaset_desc,acp_asset_cd,rak.cas_asset_desc,pur_verify_qty,acp_receive_qty,acp_receive_remark,case when format(ISNULL(acp_receive_upd_dt,''),'dd/MM/yyyy')  = '01/01/1900'  then  format(acp_receive_dt,'dd/MM/yyyy')  else  format(ISNULL(acp_receive_upd_dt,''),'dd/MM/yyyy') end as strTarikhTerimaan   from ast_acceptance aa left join ast_supplier asu on asu.sup_id=aa.acp_supplier_id left join  ast_purchase ap on  ap.pur_po_no=aa.acp_po_no and ap.pur_asset_id=aa.acp_seq_no left join  Ref_ast_kategori raka on raka.ast_kategori_Code=aa.acp_asset_cat_cd  left join Ref_ast_sub_kategri_Aset rask on rask.ast_subkateast_Code=aa.acp_asset_sub_cat_cd left join Ref_ast_jenis_aset raja on raja.ast_jeniaset_Code=aa.acp_asset_type_cd and raja.ast_sub_cat_Code=aa.acp_asset_sub_cat_cd and raja.ast_cat_Code=aa.acp_asset_cat_cd left join ast_cmn_asset rak on rak.cas_asset_cd=aa.acp_asset_cd and rak.cas_asset_type_cd=aa.acp_asset_type_cd and rak.cas_asset_cat_cd=aa.acp_asset_cat_cd where aa.acp_po_no='" + txtnopo.Text + "' and aa.acp_do_no='" + TextBox1.Text + "'";
            }
            else
            {
                qr1 = "select acp_asset_cat_cd,raka.ast_kategori_desc,acp_asset_sub_cat_cd,rask.ast_subkateast_desc,acp_asset_type_cd,raja.ast_jeniaset_desc,acp_asset_cd,rak.cas_asset_desc,pur_verify_qty,acp_receive_qty,acp_receive_remark,case when format(ISNULL(acp_receive_upd_dt,''),'dd/MM/yyyy')  = '01/01/1900'  then  format(acp_receive_dt,'dd/MM/yyyy')  else  format(ISNULL(acp_receive_upd_dt,''),'dd/MM/yyyy') end as strTarikhTerimaan   from ast_acceptance aa left join ast_supplier asu on asu.sup_id=aa.acp_supplier_id left join  ast_purchase ap on  ap.pur_po_no=aa.acp_po_no and ap.pur_asset_id=aa.acp_seq_no left join  Ref_ast_kategori raka on raka.ast_kategori_Code=aa.acp_asset_cat_cd  left join Ref_ast_sub_kategri_Aset rask on rask.ast_subkateast_Code=aa.acp_asset_sub_cat_cd left join Ref_ast_jenis_aset raja on raja.ast_jeniaset_Code=aa.acp_asset_type_cd and raja.ast_sub_cat_Code=aa.acp_asset_sub_cat_cd and raja.ast_cat_Code=aa.acp_asset_cat_cd left join ast_cmn_asset rak on rak.cas_asset_cd=aa.acp_asset_cd and rak.cas_asset_type_cd=aa.acp_asset_type_cd and rak.cas_asset_cat_cd=aa.acp_asset_cat_cd where aa.acp_po_no='" + txtnopo.Text + "'";
            }
        }

    }
    protected void Cetak_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtnopo.Text != "")
            {
                DataTable dt = new DataTable();
                Page.Header.Title = "CARIAN MAKLUMAT BELIAN ASET";
                if ((DD_down.SelectedValue != "") && (txtalamat.Value != ""))
                {
                    cur_qury1();
                    DataSet dsCustomers = GetData("" + qr1 + "");
                    dt = dsCustomers.Tables[0];
                }
                DataSet ds1 = new DataSet();
                DataTable dt2 = new DataTable();
                dt2 = dbcon.Ora_Execute_table("select acp_do_no,acp_po_no,acp_receive_dt,acp_receive_id,sp.stf_name,asu.sup_name from (select acp_po_no,acp_do_no,acp_receive_id,format(acp_receive_dt,'dd/MM/yyyy') as acp_receive_dt,acp_supplier_id from ast_acceptance where acp_po_no='" + txtnopo.Text + "' group by acp_po_no,acp_do_no,acp_receive_id,acp_receive_dt,acp_supplier_id) as a left join hr_staff_profile sp on sp.stf_staff_no=a.acp_receive_id left join ast_supplier asu on asu.sup_id=a.acp_supplier_id");
                ds1.Tables.Add(dt2);

                ReportViewer1.Reset();

                List<DataRow> listResult = dt.AsEnumerable().ToList();
                listResult.Count();
                int countRow = 0;
                countRow = listResult.Count();

                //if (countRow != 0)
                //{
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("ast_pta", dt);
                ReportDataSource rds1 = new ReportDataSource("ast_pta1", dt2);
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.LocalReport.DataSources.Add(rds1);
                //Path
                ReportViewer1.LocalReport.ReportPath = "Aset/AST_pen_ast.rdlc";
                //Parameters
                ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("d1",DD_down.SelectedItem.Text ),
                     new ReportParameter("d2",txtalamat.Value ),
                     new ReportParameter("d3",TextBox3.Text ),
                     new ReportParameter("d4",TextBox2.Text ),
                     new ReportParameter("d5",txtnopo.Text ),
                };
                //     //new ReportParameter("toDate",ToDate .Text )
                //     //new ReportParameter("fromDate",datedari  ),
                //     //new ReportParameter("toDate",datehingga ),
                //          //new ReportParameter("caw",branch ),     
                //            //new ReportParameter("Cdate",DateTime.Now.ToString("dd/MM/yyyy") ),     
                //     //new ReportParameter("Date",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  )
                //     };


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
                string extfile = txtnopo.Text.Trim();
                Response.AddHeader("content-disposition", "attachment; filename=PENGESAHAN_TERIMAAN_ASET" + extfile + "." + extension);
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
                view_details();
                //}
                //else if (countRow == 0)
                //{
                //    bind2();
                //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Rekod tidak dijumpai. Sila pastikan semua maklumat dimasukkan dengan betul.');", true);
                //}
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan No PO.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
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
        Response.Redirect("../Aset/Ast_penges_terimaan.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_penges_terimaan_view.aspx");
    }

    
}