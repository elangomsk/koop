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

public partial class Ast_pen_terimaan : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
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
                txt_nopo.Attributes.Add("Readonly", "Readonly");
                txt_nama_pembe.Attributes.Add("Readonly", "Readonly");
                txt_notele.Attributes.Add("Readonly", "Readonly");
                txt_alamat.Attributes.Add("Readonly", "Readonly");
                grid();
                grid1();
                if (samp != "")
                {
                    lbl_name.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    txt_nopo.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
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
            if (txt_nopo.Text != "")
            {
                DataTable ddicno = new DataTable();
                ddicno = DBCon.Ora_Execute_table("select pur_po_no From ast_purchase where pur_po_no='" + txt_nopo.Text + "' ");
                if (ddicno.Rows.Count > 0)
                {
                    string nopo = ddicno.Rows[0]["pur_po_no"].ToString();
                    DataTable ddbind = new DataTable();
                    ddbind = DBCon.Ora_Execute_table("select sup_name,sup_phone_no,sup_address,ast_kategori_desc,ast_jeniaset_desc,cas_asset_desc,pur_asset_qty,pur_asset_amt,pur_asset_tot_amt from ast_purchase as PU left join ast_supplier as SU on SU.sup_id =PU.pur_supplier_id left join Ref_ast_kategori as KE on KE.ast_kategori_Code=PU.pur_asset_cat_cd left join Ref_ast_jenis_aset as JE on JE.ast_jeniaset_Code=PU.pur_asset_type_cd and JE.ast_sub_cat_Code=PU.pur_asset_sub_cat_cd and JE.ast_cat_Code=PU.pur_asset_cat_cd left join ast_cmn_asset as KO on KO.cas_asset_cd=PU.pur_asset_cd and KO.cas_asset_cat_cd=PU.pur_asset_cat_cd and KO.cas_asset_sub_cat_cd=PU.pur_asset_sub_cat_cd and KO.cas_asset_type_cd=PU.pur_asset_type_cd where PU.pur_po_no='" + nopo + "'");
                    txt_nama_pembe.Text = ddbind.Rows[0]["sup_name"].ToString();
                    txt_notele.Text = ddbind.Rows[0]["sup_phone_no"].ToString();
                    txt_alamat.Text = ddbind.Rows[0]["sup_address"].ToString();
                    grid();
                    grid1();
                }
                else
                {
                    grid();
                    grid1();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else
            {
                grid();
                grid1();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan No PO.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
   
    protected void OnSelectedIndexChanged1(object sender, EventArgs e)
    {
        grid();
        grid1();
    }

    protected void OnSelectedIndexChanged2(object sender, EventArgs e)
    {
        grid();
        grid1();

    }

    void grid()
    {
        //fgrd.Attributes.Remove("Style");
        con.Open();
        SqlCommand cmd = new SqlCommand("select ast_kategori_desc,ast_jeniaset_desc,cas_asset_desc,pur_asset_qty,pur_verify_qty,pur_asset_amt,pur_asset_tot_amt from ast_purchase as PU left join ast_supplier as SU on SU.sup_id =PU.pur_supplier_id left join Ref_ast_kategori as KE on KE.ast_kategori_Code=PU.pur_asset_cat_cd left join Ref_ast_jenis_aset as JE on JE.ast_jeniaset_Code=PU.pur_asset_type_cd and JE.ast_sub_cat_Code=PU.pur_asset_sub_cat_cd and JE.ast_cat_Code=PU.pur_asset_cat_cd left join ast_cmn_asset as KO on KO.cas_asset_cd=PU.pur_asset_cd and ko.cas_asset_type_cd=pu.pur_asset_type_cd and ko.cas_asset_sub_cat_cd=pu.pur_asset_sub_cat_cd and ko.cas_asset_cat_cd=pu.pur_asset_cat_cd where PU.pur_po_no='" + txt_nopo.Text.Trim() + "' and ISNULL(PU.pur_del_ind,'') != 'D'", con);
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

    protected void clk_showrecord(object sender, EventArgs e)
    {
        if (txt_nopo.Text != "")
        {
            grid();
            grid1();
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Sila Masukan No DO');", true);
            //}
        }
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Sila Masukan No PO');", true);
        //}
    }

    protected void gv_refdata_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        grid();
        grid1();
    }


    protected void gv_refdata_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        grid();
        grid1();

    }

    void grid1()
    {
        //string qry1 = "select ast_kategori_desc as a1,ast_subkateast_desc as a2,ast_jeniaset_desc as a3,cas_asset_desc as a4,'' as a5,'' as a6,'' as a7,'' as a8,pur_asset_id as a9 from ast_purchase as PU left join ast_supplier as SU on SU.sup_id =PU.pur_supplier_id left join Ref_ast_kategori as KE on KE.ast_kategori_Code=PU.pur_asset_cat_cd left join Ref_ast_jenis_aset as JE on JE.ast_jeniaset_Code=PU.pur_asset_type_cd left join Ref_ast_sub_kategri_Aset as SKE on SKE.ast_subkateast_Code=PU.pur_asset_sub_cat_cd left join ast_cmn_asset as KO on KO.cas_asset_cd=PU.pur_asset_cd and ko.cas_asset_type_cd=pu.pur_asset_type_cd and ko.cas_asset_cat_cd=pu.pur_asset_cat_cd and KO.cas_asset_sub_cat_cd=pu.pur_asset_sub_cat_cd and KO.cas_asset_type_cd=pu.pur_asset_type_cd where PU.pur_po_no='" + txt_nopo.Text.Trim() + "' and ISNULL(PU.pur_del_ind,'') != 'D' and ISNULL(pur_complete_ind,'') != '01'";
        string qry1 = "select ast_kategori_desc as a1,ast_subkateast_desc as a2,ast_jeniaset_desc as a3,cas_asset_desc as a4,'' as a5,'' as a6,'' as a7,'' as a8,pur_asset_id as a9,pur_verify_qty as a10,(pur_verify_qty - ISNULL(b.t_qty,'0')) a11,'0' as a12 from (select * from ast_purchase where pur_po_no='" + txt_nopo.Text.Trim() + "' and ISNULL(pur_del_ind,'') != 'D' and ISNULL(pur_complete_ind,'') != '01') as PU left join ast_supplier as SU on SU.sup_id =PU.pur_supplier_id left join Ref_ast_kategori as KE on KE.ast_kategori_Code=PU.pur_asset_cat_cd left join Ref_ast_jenis_aset as JE on JE.ast_jeniaset_Code=PU.pur_asset_type_cd and JE.ast_sub_cat_Code=PU.pur_asset_sub_cat_cd and JE.ast_cat_Code=PU.pur_asset_cat_cd left join Ref_ast_sub_kategri_Aset as SKE on SKE.ast_subkateast_Code=PU.pur_asset_sub_cat_cd left join ast_cmn_asset as KO on KO.cas_asset_cd=PU.pur_asset_cd and ko.cas_asset_type_cd=pu.pur_asset_type_cd and ko.cas_asset_cat_cd=pu.pur_asset_cat_cd and KO.cas_asset_sub_cat_cd=pu.pur_asset_sub_cat_cd and KO.cas_asset_type_cd=pu.pur_asset_type_cd Left join (select acp_po_no,acp_asset_cat_cd,acp_asset_sub_cat_cd,acp_asset_type_cd,acp_asset_cd,sum(acp_receive_qty) as t_qty from ast_acceptance group by acp_po_no,acp_asset_cat_cd,acp_asset_sub_cat_cd,acp_asset_type_cd,acp_asset_cd) as b on b.acp_po_no=pu.pur_po_no and b.acp_asset_cat_cd=pu.pur_asset_cat_cd and b.acp_asset_sub_cat_cd=pu.pur_asset_sub_cat_cd and b.acp_asset_type_cd=pu.pur_asset_type_cd and b.acp_asset_cd=pu.pur_asset_cd";
        con.Open();
        SqlCommand cmd = new SqlCommand("" + qry1 + "", con);
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
            GridView2.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
            Btn_Simpan.Attributes.Add("Style", "Display:None;");
        }
        else
        {
            GridView2.DataSource = ds;
            GridView2.DataBind();
            Btn_Simpan.Attributes.Remove("Style");
        }

        con.Close();
    }

    protected void Reset_btn(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void Btn_Simpan_Click(object sender, EventArgs e)
    {
        if (txt_nopo.Text != "")
        {
            if (txt_nodo.Text != "")
            {

                DataTable DDPO = new DataTable();
                DDPO = DBCon.Ora_Execute_table("select acp_po_no,acp_do_no from ast_acceptance where acp_po_no='" + txt_nopo.Text.Trim() + "' and acp_do_no='" + txt_nodo.Text + "'");

                if (DDPO.Rows.Count == 0)
                {
                    string rcount = string.Empty;
                    int count = 0;
                    foreach (GridViewRow gvrow in GridView2.Rows)
                    {
                        var rb = (System.Web.UI.WebControls.TextBox)gvrow.Cells[1].FindControl("txt_simp");
                        if (rb.Text == "0")
                        {
                            count++;
                        }
                        rcount = count.ToString();
                    }
                    if (rcount == "0")
                    {
                        DataTable ddicno = new DataTable();
                        foreach (GridViewRow grdRow in GridView2.Rows)
                        {
                            System.Web.UI.WebControls.TextBox abc = (System.Web.UI.WebControls.TextBox)grdRow.Cells[1].FindControl("TextBox2");
                            System.Web.UI.WebControls.TextBox def = (System.Web.UI.WebControls.TextBox)grdRow.Cells[1].FindControl("TextBox3");
                            System.Web.UI.WebControls.Label lab = (System.Web.UI.WebControls.Label)grdRow.Cells[1].FindControl("Label2");
                            System.Web.UI.WebControls.Label lab_sno = (System.Web.UI.WebControls.Label)grdRow.Cells[1].FindControl("Label4");
                            System.Web.UI.WebControls.CheckBox chk_cb = (System.Web.UI.WebControls.CheckBox)grdRow.Cells[1].FindControl("CheckBox1");
                            System.Web.UI.WebControls.TextBox IND = (System.Web.UI.WebControls.TextBox)grdRow.Cells[1].FindControl("TextBox4");
                            string EmployeeID = ((System.Web.UI.WebControls.TextBox)grdRow.FindControl("TextBox4")).Text.ToString();
                            string ind1 = string.Empty;
                            if (EmployeeID == "0")
                            {
                                ind1 = "01";
                            }
                            else
                            {
                                ind1 = "00";
                            }

                            ddicno = DBCon.Ora_Execute_table("select pur_asset_cat_cd,pur_asset_sub_cat_cd,pur_asset_type_cd,pur_asset_cd,pur_asset_qty,pur_verify_remark,pur_verify_sts_cd,pur_verify_id,format(pur_verify_dt,'yyyy/MM/dd hh:mm:ss') pur_verify_dt,pur_verify_qty,pur_supplier_id from ast_purchase where pur_po_no='" + txt_nopo.Text.Trim() + "' and pur_asset_id='" + lab_sno.Text + "' ");
                            DBCon.Execute_CommamdText("insert into ast_acceptance(acp_po_no,acp_do_no,acp_seq_no,acp_asset_cat_cd,acp_asset_sub_cat_cd,acp_asset_type_cd,acp_asset_cd,acp_receive_qty,acp_receive_remark,acp_complete_ind,acp_receive_id,acp_receive_dt,acp_tot_qty,acp_crt_id,acp_crt_dt,acp_supplier_id,acp_all_qty,acp_bal_qty) values('" + txt_nopo.Text + "','" + txt_nodo.Text + "','" + lab_sno.Text + "','" + ddicno.Rows[0]["pur_asset_cat_cd"].ToString() + "','" + ddicno.Rows[0]["pur_asset_sub_cat_cd"].ToString() + "','" + ddicno.Rows[0]["pur_asset_type_cd"].ToString() + "','" + ddicno.Rows[0]["pur_asset_cd"].ToString() + "','" + abc.Text + "','" + def.Text.Replace("'", "''") + "','" + ind1 + "','" + ddicno.Rows[0]["pur_verify_id"].ToString() + "','" + ddicno.Rows[0]["pur_verify_dt"].ToString() + "','" + ddicno.Rows[0]["pur_verify_qty"].ToString() + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + ddicno.Rows[0]["pur_supplier_id"].ToString() + "','0','" + EmployeeID + "')");
                            if (ind1 == "01")
                            {
                                DataTable DDPO1 = new DataTable();
                                DDPO1 = DBCon.Ora_Execute_table("select * from ast_acceptance where acp_po_no='" + txt_nopo.Text.Trim() + "' and acp_asset_cat_cd='" + ddicno.Rows[0]["pur_asset_cat_cd"].ToString() + "' and acp_asset_sub_cat_cd='" + ddicno.Rows[0]["pur_asset_sub_cat_cd"].ToString() + "' and acp_asset_type_cd='" + ddicno.Rows[0]["pur_asset_type_cd"].ToString() + "' and acp_asset_cd='" + ddicno.Rows[0]["pur_asset_cd"].ToString() + "' and acp_seq_no='" + lab_sno.Text + "'");
                                if (DDPO1.Rows.Count > 1)
                                {
                                    DBCon.Execute_CommamdText("UPDATE ast_acceptance set acp_complete_ind='" + ind1 + "' where acp_po_no='" + txt_nopo.Text.Trim() + "' and acp_asset_cat_cd='" + ddicno.Rows[0]["pur_asset_cat_cd"].ToString() + "' and acp_asset_sub_cat_cd='" + ddicno.Rows[0]["pur_asset_sub_cat_cd"].ToString() + "' and acp_asset_type_cd='" + ddicno.Rows[0]["pur_asset_type_cd"].ToString() + "' and acp_asset_cd='" + ddicno.Rows[0]["pur_asset_cd"].ToString() + "' and acp_seq_no='" + lab_sno.Text + "'");
                                }

                                DBCon.Execute_CommamdText("UPDATE ast_purchase set pur_complete_ind='" + ind1 + "' where pur_po_no='" + txt_nopo.Text.Trim() + "' and pur_asset_id='" + lab_sno.Text + "'");
                            }
                        }

                     
                        txt_nodo.Text = "";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                        grid();
                        grid1();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Kuantiti Diterima.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }
                }
                else
                {
//                    fgrd.Attributes.Remove("Style");
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('No DO Telah Wujud.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No DO.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);

            }

        }
        else
        {
            grid();
            grid1();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No PO.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }


    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_pen_terimaan.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_pen_terimaan_view.aspx");
    }

    
}