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

public partial class Ast_maklumat_rujukan : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    string level;
    string Status = string.Empty;
    string userid;
    string ref_id;
    string qry1 = string.Empty, qry2 = string.Empty;
    string tn1 = string.Empty, tc1 = string.Empty, tc2 = string.Empty, tc3 = string.Empty, tc4 = string.Empty, tc5 = string.Empty, tc6 = string.Empty, tc7 = string.Empty, tc8 = string.Empty, tc9 = string.Empty, tc10 = string.Empty;
    string h1 = string.Empty, h2 = string.Empty, h3 = string.Empty, h4 = string.Empty, h5 = string.Empty, sq1 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
                sel_rt.SelectedValue = "01";
                sel_val();
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    protected void dd_SelectedIndexChanged(object sender, EventArgs e)
    {
        sel_val();
        string script1 = " $(function () {  $(" + gv_refdata.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);

    }
    void ref_table()
    {
       
        if (sel_rt.SelectedValue == "01")
        {
            tn1 = "Ref_ast_kategori";
            tc1 = "ast_kategori_code";
            tc2 = "ast_kategori_desc";
            tc3 = "Status";
            tc4 = "Id";
        }
        else if (sel_rt.SelectedValue == "02")
        {
            tn1 = "Ref_hr_Pengesahan_sts";
            tc1 = "hr_Pengesahan_Code";
            tc2 = "hr_Pengesahan_desc";
            tc3 = "Status";
            tc4 = "Id";
        }
        else if (sel_rt.SelectedValue == "03")
        {
            tn1 = "Ref_ast_sts_kelulusan";
            tc1 = "ast_stskelulusan_code";
            tc2 = "ast_stskelulusan_desc";
            tc3 = "Status";
            tc4 = "Id";
        }
        else if (sel_rt.SelectedValue == "04")
        {
            tn1 = "Ref_ast_Jenama";
            tc1 = "ast_Jenama_code";
            tc2 = "ast_Jenama_desc";
            tc3 = "Status";
            tc4 = "Id";
        }
        else if (sel_rt.SelectedValue == "05")
        {
            tn1 = "Ref_ast_Model";
            tc1 = "ast_Model_code";
            tc2 = "ast_Model_desc";
            tc3 = "Status";
            tc4 = "Id";
        }
        else if (sel_rt.SelectedValue == "06")
        {
            tn1 = "Ref_ast_Bahan_Bakar";
            tc1 = "ast_bahanbakar_code";
            tc2 = "ast_bahanbakar_desc";
            tc3 = "Status";
            tc4 = "Id";
        }
        else if (sel_rt.SelectedValue == "07")
        {
            tn1 = "Ref_ast_Jenis_Kenderaan";
            tc1 = "ast_Kenderaan_code";
            tc2 = "ast_Kenderaan_desc";
            tc3 = "Status";
            tc4 = "Id";
        }
        else if (sel_rt.SelectedValue == "08")
        {
            tn1 = "Ref_ast_Perolehan";
            tc1 = "ast_Perolehan_code";
            tc2 = "ast_Perolehan_desc";
            tc3 = "Status";
            tc4 = "Id";
        }
        else if (sel_rt.SelectedValue == "09")
        {
            tn1 = "Ref_ast_Penggunaan_Tanah";
            tc1 = "ast_Penggunaan_code";
            tc2 = "ast_Penggunaan_desc";
            tc3 = "Status";
            tc4 = "Id";
        }
        else if (sel_rt.SelectedValue == "10")
        {
            tn1 = "Ref_ast_Jenis_Pegangan";
            tc1 = "ast_Pegangan_code";
            tc2 = "ast_Pegangan_desc";
            tc3 = "Status";
            tc4 = "Id";
        }
        else if (sel_rt.SelectedValue == "11")
        {
            tn1 = "Ref_ast_Jenis_Milikan";
            tc1 = "ast_Milikan_code";
            tc2 = "ast_Milikan_desc";
            tc3 = "Status";
            tc4 = "Id";
        }
        else if (sel_rt.SelectedValue == "12")
        {
            tn1 = "Ref_ast_sts_pemerikksaan";
            tc1 = "ast_pemerikksaan_code";
            tc2 = "ast_pemerikksaan_desc";
            tc3 = "Status";
            tc4 = "Id";
        }
        else if (sel_rt.SelectedValue == "13")
        {
            tn1 = "Ref_ast_justifikasi";
            tc1 = "ast_justifikasi_code";
            tc2 = "ast_justifikasi_desc";
            tc3 = "Status";
            tc4 = "Id";
        }
        //else if (sel_rt.SelectedValue == "14")
        //{
        //    tn1 = "ref_complaint_sts";
        //    tc1 = "ast_complaintsts_code";
        //    tc2 = "ast_complaintsts_desc";
        //    tc3 = "Status";
        //    tc4 = "Id";
        //}
        else if (sel_rt.SelectedValue == "15")
        {
            tn1 = "Ref_ast_kaedah_palupusan";
            tc1 = "kaedah_id";
            tc2 = "kaedah_desc";
            tc3 = "Status";
            tc4 = "Id";
        }
        else if (sel_rt.SelectedValue == "16")
        {
            tn1 = "Ref_ast_Jenis_Geran";
            tc1 = "ast_Geran_code";
            tc2 = "ast_Geran_desc";
            tc3 = "Status";
            tc4 = "Id";
        }
    }

    void sel_val()
    {
        
        ref_table();
        //head_id.Text = sel_rt.SelectedItem.Text;
        BindData();
       
    }

    protected void BindData()

    {
        
        if (sel_rt.SelectedValue != "")
        {
            qry1 = "Select " + tc1 + " as c1," + tc2 + " as c2," + tc3 + " as c3," + tc4 + " as c4 from " + tn1 + " ORDER BY " + tc4 + " ASC";
        }
        else
        {
            qry1 = "select kavasan_name as c1,wilayah_code as c2,wilayah_name as c3,cawangan_code as c4 from ref_cawangan where kawasan_code = '00000'";

        }
        SqlCommand cmd2 = new SqlCommand("" + qry1 + "", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);

        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            gv_refdata.DataSource = ds2;
            gv_refdata.DataBind();
            int columncount = gv_refdata.Rows[0].Cells.Count;
            gv_refdata.Rows[0].Cells.Clear();
            gv_refdata.Rows[0].Cells.Add(new TableCell());
            gv_refdata.Rows[0].Cells[0].ColumnSpan = columncount;
            gv_refdata.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
        }
        else
        {
            gv_refdata.DataSource = ds2;
            gv_refdata.DataBind();
        }


    }



    protected void gv_refdata_RowDeleting(object sender, GridViewDeleteEventArgs e)

    {
        string confirmValue = Request.Form["confirm_value"].ToString();
        if (confirmValue == "Yes" || confirmValue == "Yes,Yes" || confirmValue == "Yes,Yes,Yes" || confirmValue == "Yes,Yes,Yes,Yes" || confirmValue == "Yes,Yes,Yes,Yes,Yes" || confirmValue == "Yes,Yes,Yes,Yes,Yes,Yes" || confirmValue == "Yes,Yes,Yes,Yes,Yes,Yes,Yes" || confirmValue == "Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes" || confirmValue == "Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes" || confirmValue == "Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes" || confirmValue == "Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes" || confirmValue == "Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes" || confirmValue == "Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes" || confirmValue == "Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes" || confirmValue == "Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes" || confirmValue == "Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes" || confirmValue == "Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes" || confirmValue == "Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes" || confirmValue == "Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes" || confirmValue == "Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes" || confirmValue == "Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes" || confirmValue == "Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes,Yes")
        {
            System.Web.UI.WebControls.TextBox Id = (System.Web.UI.WebControls.TextBox)gv_refdata.Rows[e.RowIndex].FindControl("Id");

            conn.Open();
            ref_table();
            string cmdstr = "delete from " + tn1 + " where " + tc4 + "=@Id";

            SqlCommand cmd = new SqlCommand(cmdstr, conn);

            cmd.Parameters.AddWithValue("@Id", Id.Text);

            cmd.ExecuteNonQuery();

            conn.Close();

            sel_val();
            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya dipadamkan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Anda Buang Lebih 20 Records Sila muat semula dan cuba lagi!!.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
        string script1 = " $(function () {  $(" + gv_refdata.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
    }

    protected void gv_refdata_RowCommand(object sender, GridViewCommandEventArgs e)

    {

        if (e.CommandName.Equals("ADD"))

        {

            System.Web.UI.WebControls.TextBox txtAddName = (System.Web.UI.WebControls.TextBox)gv_refdata.FooterRow.FindControl("txtAddName");

            System.Web.UI.WebControls.TextBox txtAddcode = (System.Web.UI.WebControls.TextBox)gv_refdata.FooterRow.FindControl("txtAddcode");

            DropDownList ddlStatus = (DropDownList)gv_refdata.FooterRow.FindControl("ddlStatus");

            if (txtAddName.Text != "" && txtAddcode.Text != "")
            {

                DataTable dtcenter = new DataTable();
                ref_table();
                dtcenter = Dblog.Ora_Execute_table("select * from " + tn1 + " where " + tc2 + "='" + txtAddName.Text + "'");
                if (dtcenter.Rows.Count == 0)
                {

                    conn.Open();


                    string cmdstr = "insert into " + tn1 + " (" + tc1 + "," + tc2 + "," + tc3 + ") values(@" + tc1 + ",@" + tc2 + ",@" + tc3 + ")";

                    SqlCommand cmd = new SqlCommand(cmdstr, conn);

                    cmd.Parameters.AddWithValue("@" + tc1 + "", txtAddName.Text);
                    cmd.Parameters.AddWithValue("@" + tc2 + "", txtAddcode.Text);
                    cmd.Parameters.AddWithValue("@" + tc3 + "", ddlStatus.SelectedValue);

                    cmd.ExecuteNonQuery();

                    conn.Close();

                    sel_val();

                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Simpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Sudah Wujud.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Nilai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                sel_val();
            }
            string script1 = " $(function () {  $(" + gv_refdata.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
        }

    }

    protected void gv_refdata_RowUpdating(object sender, GridViewUpdateEventArgs e)

    {
        //sel_val();
        System.Web.UI.WebControls.TextBox Id = (System.Web.UI.WebControls.TextBox)gv_refdata.Rows[e.RowIndex].FindControl("Id");
        System.Web.UI.WebControls.TextBox lblEditName = (System.Web.UI.WebControls.TextBox)gv_refdata.Rows[e.RowIndex].FindControl("lblEditName");
        System.Web.UI.WebControls.TextBox txtEditcode = (System.Web.UI.WebControls.TextBox)gv_refdata.Rows[e.RowIndex].FindControl("txtEditcode");
        DropDownList editddlStatus = (DropDownList)gv_refdata.Rows[e.RowIndex].FindControl("editddlStatus");

        DataTable dtcenter = new DataTable();
        ref_table();
        dtcenter = Dblog.Ora_Execute_table("select * from " + tn1 + " where " + tc2 + "='" + txtEditcode.Text + "' AND " + tc4 + " != '" + Id.Text + "'");
        if (dtcenter.Rows.Count == 0)
        {
            conn.Open();

            string cmdstr = "update " + tn1 + " set " + tc1 + "=@" + tc1 + "," + tc2 + "=@" + tc2 + "," + tc3 + "=@" + tc3 + " where " + tc4 + "=@" + tc4 + "";

            SqlCommand cmd = new SqlCommand(cmdstr, conn);
            cmd.Parameters.AddWithValue("@" + tc1 + "", lblEditName.Text);
            cmd.Parameters.AddWithValue("@" + tc2 + "", txtEditcode.Text);
            cmd.Parameters.AddWithValue("@" + tc3 + "", editddlStatus.SelectedValue);
            cmd.Parameters.AddWithValue("@" + tc4 + "", Id.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            gv_refdata.EditIndex = -1;
            sel_val();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Sudah Wujud.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
        string script1 = " $(function () {  $(" + gv_refdata.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
    }

    protected void gv_refdata_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)

    {
        gv_refdata.EditIndex = -1;
        sel_val();
        string script1 = " $(function () {  $(" + gv_refdata.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
    }

    protected void gv_refdata_RowEditing(object sender, GridViewEditEventArgs e)

    {

        gv_refdata.EditIndex = e.NewEditIndex;

        sel_val();
        string script1 = " $(function () {  $(" + gv_refdata.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
    }


    protected void btn_reset(object sender, EventArgs e)
    {
        Response.Redirect("../kewengan/kw_template_aliran.aspx");
    }


}