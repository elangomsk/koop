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
using System.Threading;

public partial class HR_ORGNISASI_PERNIGAAN : System.Web.UI.Page
{

    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();

    string level;
    string Status = string.Empty;
    string userid;
    string ref_id;
    string confirmValue, am;
    string qry1 = string.Empty, qry2 = string.Empty;
    string tn1 = string.Empty, tc1 = string.Empty, tc2 = string.Empty, tc3 = string.Empty, tc4 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                if (Session["validate_success"].ToString() == "SUCCESS")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('" + Session["alrt_msg"].ToString() + "',{'type': 'confirmation','title': 'Success'});", true);

                }
                Session["validate_success"] = "";
                Session["alrt_msg"] = "";
                userid = Session["New"].ToString();
                BindData();
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    void app_language()

    {
        if (Session["New"] != null)
        {
            DataTable ste_set = new DataTable();
            ste_set = DBCon.Ora_Execute_table("select * from site_settings where ID IN ('1')");

            DataTable gt_lng = new DataTable();
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('469','448')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
           
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }

    protected void gv_refdata_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                DropDownList ddList = (DropDownList)e.Row.FindControl("lbleditwilayah");
                DropDownList ddList_st = (DropDownList)e.Row.FindControl("lbleditwilayah_st");

                //return DataTable havinf department data
                DataTable dt = load_department();
                DataTable dt1 = load_state();

                ddList.DataSource = dt;
                ddList.DataTextField = "org_name";
                ddList.DataValueField = "org_gen_id";
                ddList.DataBind();
                ddList.Items.Insert(0, new ListItem("--- PILIH ---", ""));
                DataRowView dr = e.Row.DataItem as DataRowView;
                ddList.SelectedValue = dr["op_org_id"].ToString().Trim();

                ddList_st.DataSource = dt1;
                ddList_st.DataTextField = "hr_negeri_desc";
                ddList_st.DataValueField = "hr_negeri_Code";
                ddList_st.DataBind();
                ddList_st.Items.Insert(0, new ListItem("--- PILIH ---", ""));
                DataRowView dr1 = e.Row.DataItem as DataRowView;
                ddList_st.SelectedValue = dr1["op_state_cd"].ToString().Trim();

            }

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {

            DropDownList lbladdwilayah = (DropDownList)e.Row.FindControl("lbladdwilayah");
            DropDownList lbladdwilayah_st = (DropDownList)e.Row.FindControl("lbladdwilayah_st");

            DataTable dt = load_department();
            DataTable dt1 = load_state();

            lbladdwilayah.DataSource = dt;

            lbladdwilayah.DataTextField = "org_name";

            lbladdwilayah.DataValueField = "org_gen_id";

            lbladdwilayah.DataBind();

            lbladdwilayah.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            lbladdwilayah_st.DataSource = dt1;

            lbladdwilayah_st.DataTextField = "hr_negeri_desc";

            lbladdwilayah_st.DataValueField = "hr_negeri_Code";

            lbladdwilayah_st.DataBind();

            lbladdwilayah_st.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            conn.Close();

        }
    }

    public DataTable load_department()
    {
        DataTable dt = new DataTable();
        SqlConnection sqlcon = new SqlConnection(cs);
        sqlcon.Open();
        string sql = "select * from hr_organization ORDER BY org_id ASC";
        SqlCommand cmd = new SqlCommand(sql);
        cmd.CommandType = CommandType.Text;
        cmd.Connection = sqlcon;
        SqlDataAdapter sd = new SqlDataAdapter(cmd);
        sd.Fill(dt);
        return dt;
    }

    public DataTable load_state()
    {
        DataTable dt = new DataTable();
        SqlConnection sqlcon = new SqlConnection(cs);
        sqlcon.Open();
        string sql = "select DISTINCT hr_negeri_Code, UPPER(hr_negeri_desc) as hr_negeri_desc from Ref_hr_negeri  WHERE Status = 'A' order by hr_negeri_desc";
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

        string cmdstr = "Select * from hr_organization_pern as rb LEFT JOIN hr_organization AS rw ON rw.org_gen_id = rb.op_org_id LEFT JOIN Ref_hr_negeri AS rn ON rn.hr_negeri_Code = rb.op_state_cd ORDER BY rb.op_org_id,rn.hr_negeri_Code ASC";

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
        //string confirmValue = Request.Form["confirm_value"];
        //if (confirmValue == "Yes")
        //{

            TextBox Id = (TextBox)gv_refdata.Rows[e.RowIndex].FindControl("Id");
        System.Web.UI.WebControls.TextBox txtEditcode = (System.Web.UI.WebControls.TextBox)gv_refdata.Rows[e.RowIndex].FindControl("TextBox1");

        conn.Open();

            string cmdstr = "delete from hr_organization_pern where Id=@Id";

            SqlCommand cmd = new SqlCommand(cmdstr, conn);

            cmd.Parameters.AddWithValue("@Id", Id.Text);

            cmd.ExecuteNonQuery();

            conn.Close();

            BindData();
        service.audit_trail("P0107", "Hapus", "NAMA PERNIAGAAN", txtEditcode.Text);
        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dipadamkan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
        //}

    }

    protected void gv_refdata_RowCommand(object sender, GridViewCommandEventArgs e)

    {

        if (e.CommandName.Equals("ADD"))

        {
            TextBox txtAddCawangan = (TextBox)gv_refdata.FooterRow.FindControl("txtAddCawangan");
            DropDownList ddlStatus = (DropDownList)gv_refdata.FooterRow.FindControl("ddlStatus");
            DropDownList lbladdwilayah = (DropDownList)gv_refdata.FooterRow.FindControl("lbladdwilayah");
            DropDownList lbladdwilayah_st = (DropDownList)gv_refdata.FooterRow.FindControl("lbladdwilayah_st");
            TextBox txtAddcode = (TextBox)gv_refdata.FooterRow.FindControl("txtAddcode");

            if (txtAddCawangan.Text != "")
            {
                DataTable dtcenter = new DataTable();
                dtcenter = Dblog.Ora_Execute_table("select * from hr_organization_pern where op_org_id='" + lbladdwilayah.SelectedValue + "' and op_perg_name='" + txtAddCawangan.Text + "'");
                if (dtcenter.Rows.Count == 0)
                {
                    DataTable dtcenter1 = new DataTable();
                    dtcenter1 = Dblog.Ora_Execute_table("select * from hr_organization_pern where op_org_id='" + lbladdwilayah.SelectedValue + "' and op_perg_code='" + txtAddcode.Text + "'");
                    if (dtcenter1.Rows.Count == 0)
                    {

                        conn.Open();

                        string cmdstr = "insert into hr_organization_pern(op_org_id,op_perg_name,op_perg_code,Status,op_crt_id,op_crt_dt,op_state_cd) values(@op_org_id,@op_perg_name,@op_perg_code,@Status,@op_crt_id,@op_crt_dt,@op_state_cd)";

                        SqlCommand cmd = new SqlCommand(cmdstr, conn);

                        cmd.Parameters.AddWithValue("@op_org_id", lbladdwilayah.SelectedValue);

                        cmd.Parameters.AddWithValue("@op_perg_name", txtAddCawangan.Text);

                        cmd.Parameters.AddWithValue("@op_perg_code", txtAddcode.Text);

                        cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);

                        cmd.Parameters.AddWithValue("@op_crt_id", Session["new"].ToString());

                        cmd.Parameters.AddWithValue("@op_crt_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                        cmd.Parameters.AddWithValue("@op_state_cd", lbladdwilayah_st.SelectedValue);

                        cmd.ExecuteNonQuery();

                        conn.Close();

                        BindData();
                        service.audit_trail("P0107", "Simpan", "NAMA PERNIAGAAN", txtAddCawangan.Text);
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
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Sila Masukkan Nilai');", true);
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
        DropDownList lbleditwilayah_st = (DropDownList)gv_refdata.Rows[e.RowIndex].FindControl("lbleditwilayah_st");
        TextBox txtEditCawangan = (TextBox)gv_refdata.Rows[e.RowIndex].FindControl("txtEditCawangan");
        DropDownList editddlStatus = (DropDownList)gv_refdata.Rows[e.RowIndex].FindControl("editddlStatus");
        TextBox txtEditcode = (TextBox)gv_refdata.Rows[e.RowIndex].FindControl("txtEditcode");

        DataTable dtcenter = new DataTable();
        dtcenter = Dblog.Ora_Execute_table("select * from hr_organization_pern where op_org_id='" + lbleditwilayah.SelectedValue + "' and op_perg_name='" + txtEditCawangan.Text + "' AND Id != '" + Id.Text + "'");
        if (dtcenter.Rows.Count == 0)
        {
            DataTable dtcenter1 = new DataTable();
            dtcenter1 = Dblog.Ora_Execute_table("select * from hr_organization_pern where op_org_id='" + lbleditwilayah.SelectedValue + "' and op_perg_code='" + txtEditcode.Text + "' AND Id != '" + Id.Text + "'");
            if (dtcenter1.Rows.Count == 0)
            {
                conn.Open();

                string cmdstr = "update hr_organization_pern set op_org_id=@op_org_id, op_perg_name =@op_perg_name,op_perg_code=@op_perg_code, Status=@Status, op_upd_id=@op_upd_id,op_upd_dt=@op_upd_dt,op_state_cd=@op_state_cd  where Id=@Id";

                SqlCommand cmd = new SqlCommand(cmdstr, conn);

                cmd.Parameters.AddWithValue("@Id", Id.Text);

                cmd.Parameters.AddWithValue("@op_org_id", lbleditwilayah.SelectedValue);

                cmd.Parameters.AddWithValue("@op_perg_name", txtEditCawangan.Text);

                cmd.Parameters.AddWithValue("@op_perg_code", txtEditcode.Text);

                cmd.Parameters.AddWithValue("@Status", editddlStatus.SelectedValue);

                cmd.Parameters.AddWithValue("@op_upd_id", Session["new"].ToString());

                cmd.Parameters.AddWithValue("@op_upd_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                cmd.Parameters.AddWithValue("@op_state_cd", lbleditwilayah_st.SelectedValue);

                cmd.ExecuteNonQuery();

                conn.Close();

                gv_refdata.EditIndex = -1;

                BindData();
                service.audit_trail("P0107", "Kemaskini", "NAMA PERNIAGAAN", txtEditCawangan.Text);
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
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