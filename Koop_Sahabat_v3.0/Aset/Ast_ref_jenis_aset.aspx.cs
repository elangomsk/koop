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

public partial class Ast_ref_jenis_aset : System.Web.UI.Page
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

                DropDownList ddList1 = (DropDownList)e.Row.FindControl("lbleditcawangan");
                //return DataTable havinf department data
                DataTable dt = load_department();

                ddList.DataSource = dt;
                ddList.DataTextField = "ast_kategori_desc";
                ddList.DataValueField = "ast_kategori_code";
                ddList.DataBind();
                ddList.Items.Insert(0, new ListItem("--- PILIH ---", ""));
                DataRowView dr = e.Row.DataItem as DataRowView;
                ddList.SelectedValue = dr["ast_kategori_code"].ToString();

                DataTable dt1 = new DataTable();
                SqlConnection sqlcon = new SqlConnection(cs);
                sqlcon.Open();
                string sql = "select * from Ref_ast_sub_kategri_Aset WHERE ast_kategori_code = '" + dr["ast_cat_Code"].ToString() + "' AND Status = 'A' ORDER BY Id DESC";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlcon;
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                sd.Fill(dt1);


                ddList1.DataSource = dt1;
                ddList1.DataTextField = "ast_subkateast_desc";
                ddList1.DataValueField = "ast_subkateast_Code";
                ddList1.DataBind();
                ddList1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
                DataRowView dr1 = e.Row.DataItem as DataRowView;
                ddList1.SelectedValue = dr["ast_subkateast_Code"].ToString();

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
        string sql = "select * from Ref_ast_kategori WHERE Status = 'A' ORDER BY Id DESC";
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

        string cmdstr = "Select * from Ref_ast_jenis_aset as rb LEFT JOIN Ref_ast_kategori AS rw ON rw.ast_kategori_code = rb.ast_cat_Code LEFT JOIN Ref_ast_sub_kategri_Aset AS rc ON rc.ast_subkateast_Code = rb.ast_sub_cat_Code ORDER BY rb.Id DESC";

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


    protected void gv_refdata_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        BindData();

        gv_refdata.PageIndex = e.NewPageIndex;

        gv_refdata.DataBind();
        string script = " $(function () {$(" + gv_refdata.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
    }


    protected void gv_refdata_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //string confirmValue = Request.Form["confirm_value"];
        //if (confirmValue == "Yes")
        //{

            System.Web.UI.WebControls.TextBox Id = (System.Web.UI.WebControls.TextBox)gv_refdata.Rows[e.RowIndex].FindControl("Id");

            conn.Open();

            string cmdstr = "delete from Ref_ast_jenis_aset where Id=@Id";

            SqlCommand cmd = new SqlCommand(cmdstr, conn);

            cmd.Parameters.AddWithValue("@Id", Id.Text);

            cmd.ExecuteNonQuery();

            conn.Close();

            BindData();

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dipadamkan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
        //}
        string script = " $(function () {$(" + gv_refdata.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
    }

    protected void gv_refdata_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName.Equals("ADD"))
        {
            System.Web.UI.WebControls.TextBox txtAddkawasan = (System.Web.UI.WebControls.TextBox)gv_refdata.FooterRow.FindControl("txtAddkawasan");
            DropDownList ddlStatus = (DropDownList)gv_refdata.FooterRow.FindControl("ddlStatus");
            DropDownList lbladdwilayah = (DropDownList)gv_refdata.FooterRow.FindControl("lbladdwilayah");
            DropDownList lbladdcawangan = (DropDownList)gv_refdata.FooterRow.FindControl("lbladdcawangan");
            System.Web.UI.WebControls.TextBox txtAddcode = (System.Web.UI.WebControls.TextBox)gv_refdata.FooterRow.FindControl("txtAddcode");


            if (txtAddkawasan.Text != "" && txtAddcode.Text != "")
            {
                DataTable dtcenter = new DataTable();
                dtcenter = Dblog.Ora_Execute_table("select * from Ref_ast_jenis_aset where ast_sub_cat_Code = '" + lbladdcawangan.SelectedValue + "' AND ast_jeniaset_desc = '" + txtAddkawasan.Text + "' ");
                if (dtcenter.Rows.Count == 0)
                {
                    DataTable dtcenter_cd = new DataTable();
                    dtcenter_cd = Dblog.Ora_Execute_table("select * from Ref_ast_jenis_aset where ast_sub_cat_Code = '" + lbladdcawangan.SelectedValue + "' AND ast_jeniaset_Code = '" + txtAddcode.Text + "' ");
                    if (dtcenter_cd.Rows.Count == 0)
                    {
                        conn.Open();

                        string cmdstr = "insert into Ref_ast_jenis_aset(ast_cat_Code,ast_sub_cat_Code,ast_jeniaset_desc,ast_jeniaset_Code,Status) values(@ast_cat_Code,@ast_sub_cat_Code,@ast_jeniaset_desc,@ast_jeniaset_Code,@Status)";

                        SqlCommand cmd = new SqlCommand(cmdstr, conn);

                        cmd.Parameters.AddWithValue("@ast_jeniaset_desc", txtAddkawasan.Text);

                        cmd.Parameters.AddWithValue("@ast_cat_Code", lbladdwilayah.SelectedValue);

                        cmd.Parameters.AddWithValue("@ast_sub_cat_Code", lbladdcawangan.SelectedValue);

                        cmd.Parameters.AddWithValue("@ast_jeniaset_Code", txtAddcode.Text);

                        cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);

                        cmd.ExecuteNonQuery();

                        conn.Close();

                        BindData();

                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Kod Sudah Wujud.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Sudah Wujud.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
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
        DropDownList lbleditcawangan = (DropDownList)gv_refdata.Rows[e.RowIndex].FindControl("lbleditcawangan");
        System.Web.UI.WebControls.TextBox txtEditkawasan = (System.Web.UI.WebControls.TextBox)gv_refdata.Rows[e.RowIndex].FindControl("txtEditkawasan");
        DropDownList editddlStatus = (DropDownList)gv_refdata.Rows[e.RowIndex].FindControl("editddlStatus");
        System.Web.UI.WebControls.TextBox txtEditcode = (System.Web.UI.WebControls.TextBox)gv_refdata.Rows[e.RowIndex].FindControl("txtEditcode");
        DataTable dtcenter = new DataTable();
        dtcenter = Dblog.Ora_Execute_table("select * from Ref_ast_jenis_aset where ast_sub_cat_Code = '" + lbleditcawangan.SelectedValue + "' AND ast_jeniaset_desc = '" + txtEditkawasan.Text + "' AND Id != '" + Id.Text + "' ");
        if (dtcenter.Rows.Count == 0)
        {
            DataTable dtcenter_cd = new DataTable();
            dtcenter_cd = Dblog.Ora_Execute_table("select * from Ref_ast_jenis_aset where ast_sub_cat_Code = '" + lbleditcawangan.SelectedValue + "' AND ast_jeniaset_Code = '" + txtEditcode.Text + "' AND Id != '" + Id.Text + "' ");
            if (dtcenter_cd.Rows.Count == 0)
            {
                conn.Open();

                string cmdstr = "update Ref_ast_jenis_aset set ast_cat_Code=@ast_cat_Code, ast_sub_cat_Code =@ast_sub_cat_Code, ast_jeniaset_desc =@ast_jeniaset_desc, ast_jeniaset_Code=@ast_jeniaset_Code, Status=@Status where Id=@Id";

                SqlCommand cmd = new SqlCommand(cmdstr, conn);

                cmd.Parameters.AddWithValue("@Id", Id.Text);

                cmd.Parameters.AddWithValue("@ast_jeniaset_desc", txtEditkawasan.Text);

                cmd.Parameters.AddWithValue("@ast_jeniaset_Code", txtEditcode.Text);

                cmd.Parameters.AddWithValue("@ast_cat_Code", lbleditwilayah.SelectedValue);

                cmd.Parameters.AddWithValue("@ast_sub_cat_Code", lbleditcawangan.SelectedValue);

                cmd.Parameters.AddWithValue("@Status", editddlStatus.SelectedValue);

                cmd.ExecuteNonQuery();

                conn.Close();

                gv_refdata.EditIndex = -1;

                BindData();

                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
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


    private DataTable RetrieveSubCategories(string categoryID)
    {
        //fetch the connection string from web.config
        string connString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
        //SQL statement to fetch prodyct subcategories
        string sql = @"Select * from Ref_ast_sub_kategri_Aset where ast_kategori_code = '" + categoryID + "'";
        DataTable dtSubCategories = new DataTable();
        //Open SQL Connection
        using (SqlConnection conn = new SqlConnection(connString))
        {
            conn.Open();
            //Initialize command object
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //Fill the result set

                adapter.Fill(dtSubCategories);

            }
        }
        return dtSubCategories;

    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //since this event is raised by the control in the gridview row
        //we could do some reverse engineerign to get reference to the gridview row
        GridViewRow gvrow = (GridViewRow)((DropDownList)sender).NamingContainer;

        //get reference to the categories & subcategories dropdownlist
        DropDownList lbladdwilayah = (DropDownList)gvrow.FindControl("lbladdwilayah");
        DropDownList lbladdcawangan = (DropDownList)gvrow.FindControl("lbladdcawangan");

        //Get subcategories based on category selected and bind the list to subcategories dropdownlist
        lbladdcawangan.DataTextField = "ast_subkateast_desc";
        lbladdcawangan.DataValueField = "ast_subkateast_Code";
        lbladdcawangan.DataSource = RetrieveSubCategories(lbladdwilayah.SelectedValue);

        if (gv_refdata.Rows.Count == 0)
        {

            int TotalColumns = gv_refdata.Rows[0].Cells.Count;

            gv_refdata.Rows[0].Cells.Clear();

            gv_refdata.Rows[0].Cells.Add(new TableCell());

            gv_refdata.Rows[0].Cells[0].ColumnSpan = TotalColumns;

            gv_refdata.Rows[0].Cells[0].Text = "<center>Rekod Tidak Dijumpai. Sila Lakukan Semula Carian</center>";
        }
        lbladdcawangan.DataBind();
        string script = " $(function () {$(" + gv_refdata.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
    }

    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        //since this event is raised by the control in the gridview row
        //we could do some reverse engineerign to get reference to the gridview row
        GridViewRow gvrow = (GridViewRow)((DropDownList)sender).NamingContainer;

        //get reference to the categories & subcategories dropdownlist
        DropDownList lbleditwilayah = (DropDownList)gvrow.FindControl("lbleditwilayah");
        DropDownList lbleditcawangan = (DropDownList)gvrow.FindControl("lbleditcawangan");

        //Get subcategories based on category selected and bind the list to subcategories dropdownlist
        lbleditcawangan.DataTextField = "ast_subkateast_desc";
        lbleditcawangan.DataValueField = "ast_subkateast_Code";
        lbleditcawangan.DataSource = RetrieveSubCategories(lbleditwilayah.SelectedValue);

        if (gv_refdata.Rows.Count == 0)
        {
            int TotalColumns = gv_refdata.Rows[0].Cells.Count;

            gv_refdata.Rows[0].Cells.Clear();

            gv_refdata.Rows[0].Cells.Add(new TableCell());

            gv_refdata.Rows[0].Cells[0].ColumnSpan = TotalColumns;

            gv_refdata.Rows[0].Cells[0].Text = "<center>Rekod Tidak Dijumpai. Sila Lakukan Semula Carian</center>";
        }
        lbleditcawangan.DataBind();
        string script = " $(function () {$(" + gv_refdata.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
    }


    protected void btn_reset(object sender, EventArgs e)
    {
        Response.Redirect("../Aset/Ast_ref_jenis_aset.aspx");
    }


}