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


public partial class HR_Ref_hari : System.Web.UI.Page

{
   
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string level;
    string Status = string.Empty;
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
                DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);
                int year = DateTime.Now.Year;
                int year1 = DateTime.Now.Year + 1;
                for (int Y = year; Y <= year1; Y++)
                {
                    ddList.Items.Add(new ListItem(Y.ToString(), Y.ToString()));
                }
                DataRowView dr = e.Row.DataItem as DataRowView;
                ddList.SelectedValue = dr["hk_year"].ToString().Trim();



                DropDownList ddList1 = (DropDownList)e.Row.FindControl("txtEditcode");

                DataSet Ds = new DataSet();
                try
                {

                    string com = "select hr_month_Code,hr_month_desc from Ref_hr_month ORDER BY hr_month_Code";
                    SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
                    DataTable dt = new DataTable();
                    adpt.Fill(dt);
                    ddList1.DataSource = dt;
                    ddList1.DataBind();
                    ddList1.DataTextField = "hr_month_desc";
                    ddList1.DataValueField = "hr_month_Code";
                    ddList1.DataBind();
                    ddList1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                ddList1.SelectedValue = dr["hk_month"].ToString().Trim();


            }

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {


            DropDownList lbladdwilayah = (DropDownList)e.Row.FindControl("lbladdwilayah");


            DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);
            int year = DateTime.Now.Year;
            int year1 = DateTime.Now.Year + 1;
            for (int Y = year; Y <= year1; Y++)
            {
                lbladdwilayah.Items.Add(new ListItem(Y.ToString(), Y.ToString()));
            }
            lbladdwilayah.SelectedValue = DateTime.Now.Year.ToString();


            DropDownList txtAddcode = (DropDownList)e.Row.FindControl("txtAddcode");

        
            DataSet Ds = new DataSet();
            try
            {

                string com = "select hr_month_Code,hr_month_desc from Ref_hr_month ORDER BY hr_month_Code";
                SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                txtAddcode.DataSource = dt;
                txtAddcode.DataBind();
                txtAddcode.DataTextField = "hr_month_desc";
                txtAddcode.DataValueField = "hr_month_Code";
                txtAddcode.DataBind();
                txtAddcode.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            }
            catch (Exception ex)
            {
                throw ex;
            }
         

        }
    }

   
    protected void BindData()
    {

        DataSet ds = new DataSet();

        DataTable FromTable = new DataTable();

        conn.Open();

        string cmdstr = "select * from hr_hari_kerja LEFT JOIN Ref_hr_month on hr_month_Code=hk_month order by hk_year,hk_month desc";

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

            conn.Open();

            string cmdstr = "delete from hr_hari_kerja where Id=@Id";

            SqlCommand cmd = new SqlCommand(cmdstr, conn);

            cmd.Parameters.AddWithValue("@Id", Id.Text);

            cmd.ExecuteNonQuery();

            conn.Close();

            BindData();

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
            DropDownList txtAddcode = (DropDownList)gv_refdata.FooterRow.FindControl("txtAddcode");

            if (txtAddCawangan.Text != "")
            {
                DataTable dtcenter = new DataTable();
                dtcenter = Dblog.Ora_Execute_table("select * from hr_hari_kerja where hk_year='" + lbladdwilayah.SelectedValue + "' and hk_month='" + txtAddcode.Text + "' and hk_day='" + txtAddCawangan.Text + "'");
                if (dtcenter.Rows.Count == 0)
                {
                    DataTable dtcenter1 = new DataTable();
                    dtcenter1 = Dblog.Ora_Execute_table("select * from hr_hari_kerja where hk_year='" + lbladdwilayah.SelectedValue + "' and hk_month='" + txtAddcode.SelectedValue + "'");
                    if (dtcenter1.Rows.Count == 0)
                    {

                        conn.Open();

                        string cmdstr = "insert into hr_hari_kerja(hk_year,hk_day,hk_month,hk_Status,hk_crt_id,hk_crt_dt) values(@hk_year,@hk_day,@hk_month,@hk_Status,@hk_crt_id,@hk_crt_dt)";

                        SqlCommand cmd = new SqlCommand(cmdstr, conn);

                        cmd.Parameters.AddWithValue("@hk_year", lbladdwilayah.SelectedValue);

                        cmd.Parameters.AddWithValue("@hk_day", txtAddCawangan.Text);

                        cmd.Parameters.AddWithValue("@hk_month", txtAddcode.SelectedValue);

                        cmd.Parameters.AddWithValue("@hk_Status", ddlStatus.SelectedValue);

                        cmd.Parameters.AddWithValue("@hk_crt_id", Session["new"].ToString());

                        cmd.Parameters.AddWithValue("@hk_crt_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                        cmd.ExecuteNonQuery();

                        conn.Close();

                        BindData();

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
        DropDownList txtEditcode = (DropDownList)gv_refdata.Rows[e.RowIndex].FindControl("txtEditcode");

        DataTable dtcenter = new DataTable();
        dtcenter = Dblog.Ora_Execute_table("select * from hr_hari_kerja where hk_year='" + lbleditwilayah.SelectedValue + "' and hk_month='" + lbleditwilayah.SelectedValue + "' and hk_day='" + txtEditCawangan.Text + "' AND Id != '" + Id.Text + "'");
        if (dtcenter.Rows.Count == 0)
        {
            DataTable dtcenter1 = new DataTable();
            dtcenter1 = Dblog.Ora_Execute_table("select * from hr_hari_kerja where hk_year='" + lbleditwilayah.SelectedValue + "' and hk_month='" + txtEditcode.SelectedValue + "' AND Id != '" + Id.Text + "'");
            if (dtcenter1.Rows.Count == 0)
            {
                conn.Open();

                string cmdstr = "update hr_hari_kerja set hk_year=@hk_year, hk_day =@hk_day,hk_month=@hk_month, hk_Status=@hk_Status, hk_upd_id=@hk_upd_id,hk_upd_dt=@hk_upd_dt  where Id=@Id";

                SqlCommand cmd = new SqlCommand(cmdstr, conn);

                cmd.Parameters.AddWithValue("@Id", Id.Text);

                cmd.Parameters.AddWithValue("@hk_year", lbleditwilayah.SelectedValue);

                cmd.Parameters.AddWithValue("@hk_day", txtEditCawangan.Text);

                cmd.Parameters.AddWithValue("@hk_month", txtEditcode.SelectedValue);

                cmd.Parameters.AddWithValue("@hk_Status", editddlStatus.SelectedValue);

                cmd.Parameters.AddWithValue("@hk_upd_id", Session["new"].ToString());

                cmd.Parameters.AddWithValue("@hk_upd_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

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
