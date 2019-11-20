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

public partial class Ast_sub_kategory : System.Web.UI.Page
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
                BindData();
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    protected void gv_refdata_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                DropDownList ddList = (DropDownList)e.Row.FindControl("lbleditwilayah");

                //return DataTable havinf department data
                DataTable dt = load_department();

                ddList.DataSource = dt;
                ddList.DataTextField = "ast_kategori_desc";
                ddList.DataValueField = "ast_kategori_code";
                ddList.DataBind();
                ddList.Items.Insert(0, new ListItem("--- PILIH ---", ""));
                DataRowView dr = e.Row.DataItem as DataRowView;
                ddList.SelectedValue = dr["ast_kategori_Code"].ToString();

            }

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {

            DropDownList lbladdwilayah = (DropDownList)e.Row.FindControl("lbladdwilayah");

            DataTable dt = load_department();

            lbladdwilayah.DataSource = dt;

            lbladdwilayah.DataTextField = "ast_kategori_desc";

            lbladdwilayah.DataValueField = "ast_kategori_code";

            lbladdwilayah.DataBind();

            lbladdwilayah.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            conn.Close();

        }
    }

    public DataTable load_department()
    {
        DataTable dt = new DataTable();
        SqlConnection sqlcon = new SqlConnection(cs);
        sqlcon.Open();
        string sql = "select * from Ref_ast_kategori WHERE Status = 'A' ORDER BY cast(Id as int) DESC";
        SqlCommand cmd = new SqlCommand(sql);
        cmd.CommandType = CommandType.Text;
        cmd.Connection = sqlcon;
        SqlDataAdapter sd = new SqlDataAdapter(cmd);
        sd.Fill(dt);
        return dt;
    }



    protected void BindData()

    {

        DataSet ds = new DataSet();

        DataTable FromTable = new DataTable();

        conn.Open();

        string cmdstr = "Select * from Ref_ast_sub_kategri_Aset as rb LEFT JOIN Ref_ast_kategori AS rw ON rw.ast_kategori_code = rb.ast_kategori_Code ORDER BY rb.ast_kategori_code,rb.ast_subkateast_Code Asc";

        SqlCommand cmd = new SqlCommand(cmdstr, conn);

        SqlDataAdapter adp = new SqlDataAdapter(cmd);

        adp.Fill(ds);

        cmd.ExecuteNonQuery();

        FromTable = ds.Tables[0];

        if (FromTable.Rows.Count > 0)

        {

            gv_refdata.DataSource = ds;

            gv_refdata.DataBind();

        }

        else

        {

            FromTable.Rows.Add(FromTable.NewRow());

            gv_refdata.DataSource = ds;

            gv_refdata.DataBind();

            int TotalColumns = gv_refdata.Rows[0].Cells.Count;

            gv_refdata.Rows[0].Cells.Clear();

            gv_refdata.Rows[0].Cells.Add(new TableCell());

            gv_refdata.Rows[0].Cells[0].ColumnSpan = TotalColumns;

            gv_refdata.Rows[0].Cells[0].Text = "<center>Rekod Tidak Dijumpai. Sila Lakukan Semula Carian</center>";

        }

        ds.Dispose();

        conn.Close();

    }


    //protected void gv_refdata_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{

    //    BindData();

    //    gv_refdata.PageIndex = e.NewPageIndex;

    //    gv_refdata.DataBind();

    //}


    protected void gv_refdata_RowDeleting(object sender, GridViewDeleteEventArgs e)

    {
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue == "Yes")
        {

            System.Web.UI.WebControls.TextBox Id = (System.Web.UI.WebControls.TextBox)gv_refdata.Rows[e.RowIndex].FindControl("Id");

            conn.Open();

            string cmdstr = "delete from Ref_ast_sub_kategri_Aset where Id=@Id";

            SqlCommand cmd = new SqlCommand(cmdstr, conn);

            cmd.Parameters.AddWithValue("@Id", Id.Text);

            cmd.ExecuteNonQuery();

            conn.Close();

            BindData();

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dipadamkan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
        }

    }

    protected void gv_refdata_RowCommand(object sender, GridViewCommandEventArgs e)

    {

        if (e.CommandName.Equals("ADD"))

        {
            System.Web.UI.WebControls.TextBox txtAddCawangan = (System.Web.UI.WebControls.TextBox)gv_refdata.FooterRow.FindControl("txtAddCawangan");
            DropDownList ddlStatus = (DropDownList)gv_refdata.FooterRow.FindControl("ddlStatus");
            DropDownList lbladdwilayah = (DropDownList)gv_refdata.FooterRow.FindControl("lbladdwilayah");
            System.Web.UI.WebControls.TextBox txtAddcode = (System.Web.UI.WebControls.TextBox)gv_refdata.FooterRow.FindControl("txtAddcode");

            if (txtAddCawangan.Text != "")
            {
                DataTable dtcenter = new DataTable();
                dtcenter = Dblog.Ora_Execute_table("select * from Ref_ast_sub_kategri_Aset where ast_subkateast_desc='" + txtAddCawangan.Text + "'");
                if (dtcenter.Rows.Count == 0)
                {
                    DataTable dtcenter1 = new DataTable();
                    dtcenter1 = Dblog.Ora_Execute_table("select * from Ref_ast_sub_kategri_Aset where ast_subkateast_Code='" + txtAddcode.Text + "'");
                    if (dtcenter1.Rows.Count == 0)
                    {

                        conn.Open();

                        string cmdstr = "insert into Ref_ast_sub_kategri_Aset(ast_kategori_Code,ast_subkateast_desc,ast_subkateast_Code,Status) values(@ast_kategori_Code,@ast_subkateast_desc,@ast_subkateast_Code,@Status)";

                        SqlCommand cmd = new SqlCommand(cmdstr, conn);

                        cmd.Parameters.AddWithValue("@ast_kategori_Code", lbladdwilayah.SelectedValue);

                        cmd.Parameters.AddWithValue("@ast_subkateast_desc", txtAddCawangan.Text);

                        cmd.Parameters.AddWithValue("@ast_subkateast_Code", txtAddcode.Text);

                        cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);

                        cmd.ExecuteNonQuery();

                        conn.Close();

                        BindData();

                        
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Kod Sudah Wujud.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Kod Sudah Wujud.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }

            }

            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Nilai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                BindData();
            }
        }
        string script = " $(function () {$(" + gv_refdata.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
    }

    protected void gv_refdata_RowUpdating(object sender, GridViewUpdateEventArgs e)

    {

        System.Web.UI.WebControls.TextBox Id = (System.Web.UI.WebControls.TextBox)gv_refdata.Rows[e.RowIndex].FindControl("Id");
        DropDownList lbleditwilayah = (DropDownList)gv_refdata.Rows[e.RowIndex].FindControl("lbleditwilayah");
        System.Web.UI.WebControls.TextBox txtEditCawangan = (System.Web.UI.WebControls.TextBox)gv_refdata.Rows[e.RowIndex].FindControl("txtEditCawangan");
        DropDownList editddlStatus = (DropDownList)gv_refdata.Rows[e.RowIndex].FindControl("editddlStatus");
        System.Web.UI.WebControls.TextBox txtEditcode = (System.Web.UI.WebControls.TextBox)gv_refdata.Rows[e.RowIndex].FindControl("txtEditcode");

        DataTable dtcenter = new DataTable();
        dtcenter = Dblog.Ora_Execute_table("select * from Ref_ast_sub_kategri_Aset where ast_subkateast_desc='" + txtEditCawangan.Text + "' AND Id != '" + Id.Text + "'");
        if (dtcenter.Rows.Count == 0)
        {
            DataTable dtcenter1 = new DataTable();
            dtcenter1 = Dblog.Ora_Execute_table("select * from Ref_ast_sub_kategri_Aset where ast_subkateast_Code='" + txtEditcode.Text + "' AND Id != '" + Id.Text + "'");
            if (dtcenter1.Rows.Count == 0)
            {
                conn.Open();

                string cmdstr = "update Ref_ast_sub_kategri_Aset set ast_kategori_Code=@ast_kategori_Code, ast_subkateast_desc =@ast_subkateast_desc,ast_subkateast_Code=@ast_subkateast_Code, Status=@Status where Id=@Id";

                SqlCommand cmd = new SqlCommand(cmdstr, conn);

                cmd.Parameters.AddWithValue("@Id", Id.Text);

                cmd.Parameters.AddWithValue("@ast_kategori_Code", lbleditwilayah.SelectedValue);

                cmd.Parameters.AddWithValue("@ast_subkateast_desc", txtEditCawangan.Text);

                cmd.Parameters.AddWithValue("@ast_subkateast_Code", txtEditcode.Text);

                cmd.Parameters.AddWithValue("@Status", editddlStatus.SelectedValue);

                cmd.ExecuteNonQuery();

                conn.Close();

                gv_refdata.EditIndex = -1;

                BindData();

                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Sudah Wujud.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Sudah Wujud.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
        string script = " $(function () {$(" + gv_refdata.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
    }

    protected void gv_refdata_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)

    {
        gv_refdata.EditIndex = -1;
        BindData();
        string script = " $(function () {$(" + gv_refdata.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
    }

    protected void gv_refdata_RowEditing(object sender, GridViewEditEventArgs e)

    {

        gv_refdata.EditIndex = e.NewEditIndex;
        BindData();
        string script = " $(function () {$(" + gv_refdata.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
    }


    protected void btn_reset(object sender, EventArgs e)
    {
        Response.Redirect("../kewengan/kw_template_aliran.aspx");
    }


}