using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Net;


public partial class HR_Ref_Unit : System.Web.UI.Page

{
   
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string level;
    string Status = string.Empty;
    StudentWebService service = new StudentWebService();
    string userid;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Session["New"] != null)
                {
                    level = Session["level"].ToString();
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
                ddList.DataTextField = "hr_jaba_desc";
                ddList.DataValueField = "hr_jaba_Code";
                ddList.DataBind();
                ddList.Items.Insert(0, new ListItem("--- PILIH ---", ""));
                DataRowView dr = e.Row.DataItem as DataRowView;
                ddList.SelectedValue = dr["hr_jaba_Code"].ToString().Trim();

            }

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {

            DropDownList lbladdwilayah = (DropDownList)e.Row.FindControl("lbladdwilayah");

            DataTable dt = load_department();

            lbladdwilayah.DataSource = dt;

            lbladdwilayah.DataTextField = "hr_jaba_desc";

            lbladdwilayah.DataValueField = "hr_jaba_Code";

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
        string sql = "select * from Ref_hr_jabatan ORDER BY hr_jaba_Code ASC";
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

        string cmdstr = "Select * from Ref_hr_unit as rb LEFT JOIN Ref_hr_jabatan AS rw ON rw.hr_jaba_Code = rb.hr_jaba_Code ORDER BY rb.hr_jaba_Code,rb.Id ASC";

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

    }


    protected void gv_refdata_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        

            TextBox Id = (TextBox)gv_refdata.Rows[e.RowIndex].FindControl("Id");
        System.Web.UI.WebControls.TextBox txtEditcode = (System.Web.UI.WebControls.TextBox)gv_refdata.Rows[e.RowIndex].FindControl("TextBox1");

        conn.Open();

            string cmdstr = "delete from Ref_hr_unit where Id=@Id";

            SqlCommand cmd = new SqlCommand(cmdstr, conn);

            cmd.Parameters.AddWithValue("@Id", Id.Text);

            cmd.ExecuteNonQuery();

            conn.Close();

            BindData();
        service.audit_trail("P0196", "Hapus","NAMA SEKSYEN", txtEditcode.Text);
        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dipadamkan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
     
        string script1 = " $(function () {  $(" + gv_refdata.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
    }

    protected void gv_refdata_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName.Equals("ADD"))
        {
            TextBox txtAddCawangan = (TextBox)gv_refdata.FooterRow.FindControl("txtAddCawangan");
            DropDownList ddlStatus = (DropDownList)gv_refdata.FooterRow.FindControl("ddlStatus");
            DropDownList lbladdwilayah = (DropDownList)gv_refdata.FooterRow.FindControl("lbladdwilayah");
            TextBox txtAddcode = (TextBox)gv_refdata.FooterRow.FindControl("txtAddcode");

            if (txtAddCawangan.Text != "")
            {
                DataTable dtcenter = new DataTable();
                dtcenter = Dblog.Ora_Execute_table("select * from Ref_hr_unit where hr_jaba_Code='" + lbladdwilayah.SelectedValue + "' and hr_unit_desc='" + txtAddCawangan.Text + "'");
                if (dtcenter.Rows.Count == 0)
                {
                    DataTable dtcenter1 = new DataTable();
                    dtcenter1 = Dblog.Ora_Execute_table("select * from Ref_hr_unit where hr_jaba_Code='" + lbladdwilayah.SelectedValue + "' and hr_unit_Code='" + txtAddcode.Text + "'");
                    if (dtcenter1.Rows.Count == 0)
                    {

                        conn.Open();

                        string cmdstr = "insert into Ref_hr_unit(hr_jaba_Code,hr_unit_desc,hr_unit_Code,Status,ins_crt_id,ins_crt_dt) values(@hr_jaba_Code,@hr_unit_desc,@hr_unit_Code,@Status,@ins_crt_id,@ins_crt_dt)";

                        SqlCommand cmd = new SqlCommand(cmdstr, conn);

                        cmd.Parameters.AddWithValue("@hr_jaba_Code", lbladdwilayah.SelectedValue);

                        cmd.Parameters.AddWithValue("@hr_unit_desc", txtAddCawangan.Text);

                        cmd.Parameters.AddWithValue("@hr_unit_Code", txtAddcode.Text);

                        cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);

                        cmd.Parameters.AddWithValue("@ins_crt_id", Session["new"].ToString());

                        cmd.Parameters.AddWithValue("@ins_crt_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                        cmd.ExecuteNonQuery();

                        conn.Close();

                        BindData();
                        service.audit_trail("P0196", "Simpan", "NAMA SEKSYEN", txtAddCawangan.Text);
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
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

            }

            else
            {                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Nilai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                BindData();
            }
        }
        string script1 = " $(function () {  $(" + gv_refdata.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
    }

    protected void gv_refdata_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        TextBox Id = (TextBox)gv_refdata.Rows[e.RowIndex].FindControl("Id");
        DropDownList lbleditwilayah = (DropDownList)gv_refdata.Rows[e.RowIndex].FindControl("lbleditwilayah");
        TextBox txtEditCawangan = (TextBox)gv_refdata.Rows[e.RowIndex].FindControl("txtEditCawangan");
        DropDownList editddlStatus = (DropDownList)gv_refdata.Rows[e.RowIndex].FindControl("editddlStatus");
        TextBox txtEditcode = (TextBox)gv_refdata.Rows[e.RowIndex].FindControl("txtEditcode");

        DataTable dtcenter = new DataTable();
        dtcenter = Dblog.Ora_Execute_table("select * from Ref_hr_unit where hr_unit_Code='" + lbleditwilayah.SelectedValue + "' and hr_unit_desc='" + txtEditCawangan.Text + "' AND Id != '" + Id.Text + "'");
        if (dtcenter.Rows.Count == 0)
        {
            DataTable dtcenter1 = new DataTable();
            dtcenter1 = Dblog.Ora_Execute_table("select * from Ref_hr_unit where hr_jaba_Code='" + lbleditwilayah.SelectedValue + "' and hr_unit_Code='" + txtEditcode.Text + "' AND Id != '" + Id.Text + "'");
            if (dtcenter1.Rows.Count == 0)
            {
                conn.Open();

                string cmdstr = "update Ref_hr_unit set hr_jaba_Code=@hr_jaba_Code, hr_unit_desc =@hr_unit_desc,hr_unit_Code=@hr_unit_Code, Status=@Status, ins_upd_id=@ins_upd_id,ins_upd_dt=@ins_upd_dt  where Id=@Id";

                SqlCommand cmd = new SqlCommand(cmdstr, conn);

                cmd.Parameters.AddWithValue("@Id", Id.Text);

                cmd.Parameters.AddWithValue("@hr_jaba_Code", lbleditwilayah.SelectedValue);

                cmd.Parameters.AddWithValue("@hr_unit_desc", txtEditCawangan.Text);

                cmd.Parameters.AddWithValue("@hr_unit_Code", txtEditcode.Text);

                cmd.Parameters.AddWithValue("@Status", editddlStatus.SelectedValue);

                cmd.Parameters.AddWithValue("@ins_upd_id", Session["new"].ToString());

                cmd.Parameters.AddWithValue("@ins_upd_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                cmd.ExecuteNonQuery();

                conn.Close();

                gv_refdata.EditIndex = -1;

                BindData();
                service.audit_trail("P0196", "Kemaskini", "NAMA SEKSYEN", txtEditCawangan.Text);
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
        string script1 = " $(function () {  $(" + gv_refdata.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
    }

    protected void gv_refdata_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

        gv_refdata.EditIndex = -1;

        BindData();
        string script1 = " $(function () {  $(" + gv_refdata.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
    }

    protected void gv_refdata_RowEditing(object sender, GridViewEditEventArgs e)
    {

        gv_refdata.EditIndex = e.NewEditIndex;

        BindData();
        string script1 = " $(function () {  $(" + gv_refdata.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
    }
}
