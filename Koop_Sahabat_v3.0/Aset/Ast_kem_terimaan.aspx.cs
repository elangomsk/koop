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

public partial class Ast_kem_terimaan : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection dbcon = new DBConnection();
    StudentWebService service = new StudentWebService();

    private static int PageSize = 20;
    string qry1 = string.Empty, qry2 = string.Empty;
    string level;
    string Status = string.Empty, Status1 = string.Empty;
    string userid;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2()})";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;
                txttar.Attributes.Add("Readonly", "Readonly");
                txt_nama_pembe.Attributes.Add("Readonly", "Readonly");
                txt_notele.Attributes.Add("Readonly", "Readonly");
                txt_alamat.Attributes.Add("Readonly", "Readonly");
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
        try
        {
            if (txtnopo.Text != "")
            {
                DataTable dd = new DataTable();
                dd = dbcon.Ora_Execute_table(" select acp_po_no,acp_complete_ind from ast_acceptance where acp_po_no='" + txtnopo.Text + "'");
                if (dd.Rows.Count > 0)
                {

                    bind();
                    bind1();
                }
                else
                {
                    bind1();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Maklumat Carian Tidak Dijumpai');", true);
                }
            }
            else
            {
                bind1();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Sila Masukan No PO.');", true);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void gvEmp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (DataBinder.Eval(e.Row.DataItem, "acp_complete_ind") != DBNull.Value)
            {

                //System.Web.UI.WebControls.CheckBox lbl_chk = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("CheckBox1");
                System.Web.UI.WebControls.Label lbl_chk = (System.Web.UI.WebControls.Label)e.Row.FindControl("cmp_ind");
                System.Web.UI.WebControls.TextBox lb1 = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("txtCustomerId");
                System.Web.UI.WebControls.TextBox lb2 = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("txtName");

                System.Web.UI.WebControls.TextBox txt1 = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("txt_simp");

                System.Web.UI.WebControls.Label apo = (System.Web.UI.WebControls.Label)e.Row.FindControl("acppo");
                System.Web.UI.WebControls.Label sqn = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label4");
                System.Web.UI.WebControls.Label cdt = (System.Web.UI.WebControls.Label)e.Row.FindControl("acpctdt");
                if (lbl_chk.Text == "01")
                {
                    lb1.Attributes.Add("Readonly", "Readonly");
                    lb2.Attributes.Add("Readonly", "Readonly");
                    lbl_chk.Attributes.Add("Style", "pointer-events:None;");
                    txt1.Text = "0";
                    Btn_Kemaskini.Attributes.Add("style", "Display:none");
                }
                else
                {
                    DataTable DDPO1 = new DataTable();
                    DDPO1 = dbcon.Ora_Execute_table("select top(1) FORMAT(acp_crt_dt,'yyyy-MM-dd HH:mm:ss', 'en-us') as ct from ast_acceptance where acp_po_no='" + apo.Text.Trim() + "' and acp_seq_no='" + sqn.Text + "' order by acp_crt_dt desc");
                    if (DDPO1.Rows[0]["ct"].ToString() == cdt.Text)
                    {
                        lb1.Attributes.Remove("Readonly");
                        lb2.Attributes.Remove("Readonly");
                        lbl_chk.Attributes.Remove("Style");
                        txt1.Text = "1";
                        Btn_Kemaskini.Attributes.Remove("style");
                    }
                    else
                    {
                        lb1.Attributes.Add("Readonly", "Readonly");
                        lb2.Attributes.Add("Readonly", "Readonly");
                        lbl_chk.Attributes.Add("Style", "pointer-events:None;");
                        txt1.Text = "0";
                        Btn_Kemaskini.Attributes.Add("style", "Display:none");
                    }
                }
            }


        }
    }

  


    public void bind()
    {
        SqlConnection conn = new SqlConnection(cs);
        string query2 = "select aa.acp_po_no,ISNULL(asu.sup_name,'') as sup_name,format(aa.acp_receive_dt,'dd/MM/yyyy') acp_receive_dt  from ast_acceptance aa left join ast_supplier asu on  asu.sup_id=aa.acp_supplier_id where acp_po_no ='" + txtnopo.Text + "'";
        conn.Open();
        var sqlCommand2 = new SqlCommand(query2, conn);
        var sqlReader2 = sqlCommand2.ExecuteReader();
        while (sqlReader2.Read())
        {

            //txtnopo1.Text = (string)sqlReader2["acp_po_no"].ToString().Trim();
            txt_nama_pembe.Text = (string)sqlReader2["sup_name"].ToString().Trim();
            txttar.Text = (string)sqlReader2["acp_receive_dt"].ToString().Trim();

        }

        DataTable DDPO1 = new DataTable();
        DDPO1 = dbcon.Ora_Execute_table("select top(1) *,acp_verify_sts_cd,acp_verify_remark,isnull(acp_verify_dt,'') as verify_date from ast_acceptance where acp_po_no='" + txtnopo.Text + "'");
        if (DDPO1.Rows[0]["acp_verify_sts_cd"].ToString().Trim() == "S")
        {
            Btn_Kemaskini.Attributes.Add("style", "Display:none");
        }
        else
        {
            Btn_Kemaskini.Attributes.Remove("style");
        }

        sqlReader2.Close();

    }

    public void bind1()
    {

        SqlCommand cmd2 = new SqlCommand("select acp_po_no,acp_asset_cat_cd,raka.ast_kategori_desc,acp_asset_sub_cat_cd,rask.ast_subkateast_desc,acp_asset_type_cd,raja.ast_jeniaset_desc,acp_asset_cd,rak.cas_asset_desc,acp_tot_qty,acp_bal_qty,acp_receive_qty,acp_receive_remark,acp_do_no, case when acp_complete_ind = '01' then 'LENGKAP' else 'TIDAK LENGKAP' end as comp_desc,acp_seq_no,acp_complete_ind,FORMAT(acp_crt_dt,'yyyy-MM-dd HH:mm:ss', 'en-us') acpcdt  from ast_acceptance aa left join ast_supplier asu on  asu.sup_id=aa.acp_supplier_id left join Ref_ast_kategori raka on raka.ast_kategori_Code=aa.acp_asset_cat_cd  left join Ref_ast_sub_kategri_Aset rask on rask.ast_subkateast_Code=aa.acp_asset_sub_cat_cd left join Ref_ast_jenis_aset raja on raja.ast_jeniaset_Code=aa.acp_asset_type_cd and raja.ast_sub_cat_Code=aa.acp_asset_sub_cat_cd and raja.ast_cat_Code=aa.acp_asset_cat_cd left join ast_cmn_asset rak on rak.cas_asset_cd=aa.acp_asset_cd and rak.cas_asset_type_cd=aa.acp_asset_type_cd and rak.cas_asset_sub_cat_cd=aa.acp_asset_sub_cat_cd and rak.cas_asset_cat_cd=aa.acp_asset_cat_cd where acp_po_no ='" + txtnopo.Text + "' order by acp_crt_dt", con);
        //SqlCommand cmd2 = new SqlCommand("select *,(a.acp_tot_qty - b.rqty) as bqty from (select acp_po_no,acp_asset_cat_cd,raka.ast_kategori_desc,acp_asset_sub_cat_cd,rask.ast_subkateast_desc,acp_asset_type_cd,raja.ast_jeniaset_desc,acp_asset_cd,rak.cas_asset_desc,acp_tot_qty,acp_receive_qty,acp_receive_remark,acp_do_no, case when acp_complete_ind = '01' then 'LENGKAP' else 'TIDAK LENGKAP' end as comp_desc,acp_seq_no,acp_complete_ind,FORMAT(acp_crt_dt,'yyyy-MM-dd HH:mm:ss', 'en-us') acpcdt  from ast_acceptance aa left join ast_supplier asu on  asu.sup_id=aa.acp_supplier_id left join Ref_ast_kategori raka on raka.ast_kategori_Code=aa.acp_asset_cat_cd  left join Ref_ast_sub_kategri_Aset rask on rask.ast_subkateast_Code=aa.acp_asset_sub_cat_cd left join Ref_ast_jenis_aset raja on raja.ast_jeniaset_Code=aa.acp_asset_type_cd and raja.ast_sub_cat_Code=aa.acp_asset_sub_cat_cd and raja.ast_cat_Code=aa.acp_asset_cat_cd left join ast_cmn_asset rak on rak.cas_asset_cd=aa.acp_asset_cd and rak.cas_asset_type_cd=aa.acp_asset_type_cd and rak.cas_asset_cat_cd=aa.acp_asset_cat_cd where acp_po_no ='" + txtnopo.Text + "') as a left join (select acp_po_no,acp_seq_no,acp_asset_cat_cd,acp_asset_sub_cat_cd,acp_asset_type_cd,acp_asset_cd,sum(acp_receive_qty) as rqty from ast_acceptance where acp_po_no ='" + txtnopo.Text + "' group by acp_po_no,acp_seq_no,acp_asset_cat_cd,acp_asset_sub_cat_cd,acp_asset_type_cd,acp_asset_cd) as b on b.acp_po_no=a.acp_po_no and b.acp_seq_no=a.acp_seq_no order by a.acpcdt", con);
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
            GridView1.Rows[0].Cells[0].Text = "<strong>Maklumat Carian Tidak Dijumpai</strong>";
        }
        else
        {
            GridView1.DataSource = ds2;
            GridView1.DataBind();
        }


    }
    protected void Btn_Kemaskini_Click(object sender, EventArgs e)

    {
        if (txtnopo.Text != "")
        {
            DataTable dt = new DataTable();
            DataRow row = dt.NewRow();
            DataTable ddicno = new DataTable();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                TextBox txtUsrId = (TextBox)GridView1.Rows[i].FindControl("txtCustomerId");
                TextBox txtremark = (TextBox)GridView1.Rows[i].FindControl("txtName");
                Label acsqno = (Label)GridView1.Rows[i].FindControl("Label4");
                Label dono = (Label)GridView1.Rows[i].FindControl("txtnodo");

                CheckBox ckbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
                string EmployeeID = ((TextBox)GridView1.Rows[i].FindControl("TextBox4")).Text.ToString();
                string txt1 = ((TextBox)GridView1.Rows[i].FindControl("txt_simp")).Text.ToString();
                if (txt1 == "1")
                {
                    string ckvlue = string.Empty;
                    if (EmployeeID == "0")
                    {
                        ckvlue = "01";
                    }
                    else
                    {
                        ckvlue = "00";
                    }

                    userid = Session["New"].ToString();

                    int qty = Convert.ToInt32(txtUsrId.Text);

                    ddicno = dbcon.Ora_Execute_table("select pur_asset_cat_cd,pur_asset_sub_cat_cd,pur_asset_type_cd,pur_asset_cd,pur_asset_qty,pur_verify_remark,pur_verify_sts_cd,pur_verify_id,format(pur_verify_dt,'yyyy/MM/dd hh:mm:ss') pur_verify_dt,pur_verify_qty,pur_supplier_id from ast_purchase where pur_po_no='" + txtnopo.Text.Trim() + "' and pur_asset_id='" + acsqno.Text + "' ");


                    DataTable dd1 = new DataTable();
                    dd1 = dbcon.Ora_Execute_table(" update ast_acceptance set acp_bal_qty='" + EmployeeID + "', acp_receive_qty ='" + qty + "',acp_receive_remark ='" + txtremark.Text.Replace("'", "''") + "',acp_complete_ind ='" + ckvlue + "',acp_upd_id ='" + userid + "',acp_upd_dt ='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' WHERE acp_po_no ='" + txtnopo.Text + "' AND acp_do_no = '" + dono.Text + "' and acp_seq_no='" + acsqno.Text + "'");
                    if (ckvlue == "01")
                    {

                        DataTable DDPO1 = new DataTable();
                        DDPO1 = dbcon.Ora_Execute_table("select * from ast_acceptance where acp_po_no='" + txtnopo.Text.Trim() + "' and acp_asset_cat_cd='" + ddicno.Rows[0]["pur_asset_cat_cd"].ToString() + "' and acp_asset_sub_cat_cd='" + ddicno.Rows[0]["pur_asset_sub_cat_cd"].ToString() + "' and acp_asset_type_cd='" + ddicno.Rows[0]["pur_asset_type_cd"].ToString() + "' and acp_asset_cd='" + ddicno.Rows[0]["pur_asset_cd"].ToString() + "' and acp_seq_no='" + acsqno.Text + "'");
                        if (DDPO1.Rows.Count > 1)
                        {
                            dbcon.Execute_CommamdText("UPDATE ast_acceptance set acp_complete_ind='" + ckvlue + "' where acp_po_no='" + txtnopo.Text.Trim() + "' and acp_asset_cat_cd='" + ddicno.Rows[0]["pur_asset_cat_cd"].ToString() + "' and acp_asset_sub_cat_cd='" + ddicno.Rows[0]["pur_asset_sub_cat_cd"].ToString() + "' and acp_asset_type_cd='" + ddicno.Rows[0]["pur_asset_type_cd"].ToString() + "' and acp_asset_cd='" + ddicno.Rows[0]["pur_asset_cd"].ToString() + "' and acp_seq_no='" + acsqno.Text + "'");
                        }

                        dbcon.Execute_CommamdText("UPDATE ast_purchase set pur_complete_ind='" + ckvlue + "' where pur_po_no='" + txtnopo.Text + "' and pur_asset_id='" + acsqno.Text + "'");
                    }
                }
            }
            bind1();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
        }
        else
        {
            bind1();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan No PO.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

   
    protected void gv_refdata_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bind1();
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_kem_terimaan.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_kem_terimaan_view.aspx");
    }

    
}