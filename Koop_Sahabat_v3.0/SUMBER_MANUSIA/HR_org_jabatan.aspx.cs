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
using System.Data.Common;
using System.Threading;

public partial class HR_org_jabatan : System.Web.UI.Page
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
        gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('1516','448')");

        CultureInfo culinfo = Thread.CurrentThread.CurrentUICulture;
        TextInfo txtinfo = culinfo.TextInfo;

        h1_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
        bb1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
        bb2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
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

                DropDownList ddList1 = (DropDownList)e.Row.FindControl("lbleditcawangan");

                DropDownList ddList2 = (DropDownList)e.Row.FindControl("lbleditwilayah_jab");
                //return DataTable havinf department data
                DataTable dt = load_department();

                ddList.DataSource = dt;
                ddList.DataTextField = "org_name";
                ddList.DataValueField = "org_gen_id";
                ddList.DataBind();
                ddList.Items.Insert(0, new ListItem("--- PILIH ---", ""));
                DataRowView dr = e.Row.DataItem as DataRowView;
                ddList.SelectedValue = dr["oj_org_id"].ToString().Trim();

                DataTable dt_j = load_department1();

                ddList2.DataSource = dt_j;
                ddList2.DataTextField = "hr_jaba_desc";
                ddList2.DataValueField = "hr_jaba_Code";
                ddList2.DataBind();
                ddList2.Items.Insert(0, new ListItem("--- PILIH ---", ""));
                DataRowView dr_j = e.Row.DataItem as DataRowView;
                ddList2.SelectedValue = dr_j["oj_jaba_cd"].ToString();

                DataTable dt1 = new DataTable();
                SqlConnection sqlcon = new SqlConnection(cs);
                sqlcon.Open();
                string sql = "select distinct * from hr_organization_pern WHERE op_org_id = '" + dr["op_org_id"].ToString() + "' AND Status = 'A' ORDER BY Id ASC";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlcon;
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                sd.Fill(dt1);


                ddList1.DataSource = dt1;
                ddList1.DataTextField = "op_perg_name";
                ddList1.DataValueField = "op_perg_code";
                ddList1.DataBind();
                ddList1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
                DataRowView dr1 = e.Row.DataItem as DataRowView;
                ddList1.SelectedValue = dr["oj_perg_code"].ToString();

            }

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {

            DropDownList lbladdwilayah = (DropDownList)e.Row.FindControl("lbladdwilayah");

            DataTable dt = load_department();

            lbladdwilayah.DataSource = dt;

            lbladdwilayah.DataTextField = "org_name";

            lbladdwilayah.DataValueField = "org_gen_id";

            lbladdwilayah.DataBind();

            lbladdwilayah.Items.Insert(0, new ListItem("--- PILIH ---", ""));


            DropDownList lbladdwilayah_jb = (DropDownList)e.Row.FindControl("lbladdwilayah_jab");

            DataTable dt1 = load_department1();

            lbladdwilayah_jb.DataSource = dt1;

            lbladdwilayah_jb.DataTextField = "hr_jaba_desc";

            lbladdwilayah_jb.DataValueField = "hr_jaba_code";

            lbladdwilayah_jb.DataBind();

            lbladdwilayah_jb.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            conn.Close();

        }
    }

    public DataTable load_department()
    {
        DataTable dt = new DataTable();
        SqlConnection sqlcon = new SqlConnection(cs);
        sqlcon.Open();
        string sql = "select distinct * from hr_organization ORDER BY org_gen_id ASC";
        SqlCommand cmd = new SqlCommand(sql);
        cmd.CommandType = CommandType.Text;
        cmd.Connection = sqlcon;
        SqlDataAdapter sd = new SqlDataAdapter(cmd);
        sd.Fill(dt);
        return dt;
    }

    public DataTable load_department1()
    {
        DataTable dt = new DataTable();
        SqlConnection sqlcon = new SqlConnection(cs);
        sqlcon.Open();
        string sql = "select distinct * from Ref_hr_jabatan ORDER BY Id ASC";
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

        string cmdstr = "Select * from hr_organization_jaba as rb LEFT JOIN hr_organization AS rw ON rw.org_gen_id = rb.oj_org_id LEFT JOIN hr_organization_pern AS rc ON rc.op_perg_code = rb.oj_perg_code ORDER BY rb.oj_org_id,rb.oj_perg_code,rb.Id ASC";

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

            gv_refdata.Rows[0].Cells[0].Text = "<center><strong>Rekod Tidak Dijumpai. Sila Lakukan Semula Carian..</strong></center>";

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

            string cmdstr = "delete from hr_organization_jaba where Id=@Id";

            SqlCommand cmd = new SqlCommand(cmdstr, conn);

            cmd.Parameters.AddWithValue("@Id", Id.Text);

            cmd.ExecuteNonQuery();

            conn.Close();

            BindData();
            service.audit_trail("P0108", "Hapus","NAMA JABATAN", txtEditcode.Text);
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dipadamkan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
        
    }

    protected void gv_refdata_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName.Equals("ADD"))
        {
            DropDownList txtAddjabatan = (DropDownList)gv_refdata.FooterRow.FindControl("lbladdwilayah_jab");
            DropDownList ddlStatus = (DropDownList)gv_refdata.FooterRow.FindControl("ddlStatus");
            DropDownList lbladdwilayah = (DropDownList)gv_refdata.FooterRow.FindControl("lbladdwilayah");
            DropDownList lbladdcawangan = (DropDownList)gv_refdata.FooterRow.FindControl("lbladdcawangan");
            //TextBox txtAddcode = (TextBox)gv_refdata.FooterRow.FindControl("txtAddcode");


            if (txtAddjabatan.SelectedValue != "")
            {
                DataTable dtcenter = new DataTable();
                dtcenter = Dblog.Ora_Execute_table("select * from hr_organization_jaba where oj_perg_code = '" + lbladdcawangan.SelectedValue + "' AND oj_jaba_desc = '" + txtAddjabatan.SelectedItem.Text + "' ");
                if (dtcenter.Rows.Count == 0)
                {
                    DataTable dtcenter_cd = new DataTable();
                    dtcenter_cd = Dblog.Ora_Execute_table("select * from hr_organization_jaba where oj_perg_code = '" + lbladdcawangan.SelectedValue + "' AND oj_jaba_cd = '" + txtAddjabatan.SelectedValue + "' ");
                    if (dtcenter_cd.Rows.Count == 0)
                    {
                        conn.Open();

                        string cmdstr = "insert into hr_organization_jaba(oj_org_id,oj_perg_code,oj_jaba_cd,oj_jaba_desc,Status,op_crt_id,op_crt_dt) values(@oj_org_id,@oj_perg_code,@oj_jaba_cd,@oj_jaba_desc,@Status,@op_crt_id,@op_crt_dt)";

                        SqlCommand cmd = new SqlCommand(cmdstr, conn);

                        cmd.Parameters.AddWithValue("@oj_org_id", lbladdwilayah.SelectedValue);

                        cmd.Parameters.AddWithValue("@oj_perg_code", lbladdcawangan.SelectedValue);

                        cmd.Parameters.AddWithValue("@oj_jaba_cd", txtAddjabatan.SelectedValue);

                        cmd.Parameters.AddWithValue("@oj_jaba_desc", txtAddjabatan.SelectedItem.Text);

                        cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);

                        cmd.Parameters.AddWithValue("@op_crt_id", Session["new"].ToString());

                        cmd.Parameters.AddWithValue("@op_crt_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                        cmd.ExecuteNonQuery();

                        conn.Close();

                        BindData();
                        service.audit_trail("P0108", "Simpan", "NAMA JABATAN", txtAddjabatan.SelectedItem.Text);
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
        string script1 = " $(function () {  $(" + gv_refdata.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
    }

    protected void gv_refdata_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        TextBox Id = (TextBox)gv_refdata.Rows[e.RowIndex].FindControl("Id");
        DropDownList lbleditwilayah = (DropDownList)gv_refdata.Rows[e.RowIndex].FindControl("lbleditwilayah");
        DropDownList lbleditcawangan = (DropDownList)gv_refdata.Rows[e.RowIndex].FindControl("lbleditcawangan");
        DropDownList txtEditkawasan = (DropDownList)gv_refdata.Rows[e.RowIndex].FindControl("lbleditwilayah_jab");
        DropDownList editddlStatus = (DropDownList)gv_refdata.Rows[e.RowIndex].FindControl("editddlStatus");

        DataTable dtcenter = new DataTable();
        dtcenter = Dblog.Ora_Execute_table("select * from hr_organization_jaba where oj_perg_code = '" + lbleditcawangan.SelectedValue + "' AND oj_perg_desc = '" + txtEditkawasan.SelectedItem.Text + "' AND Id != '" + Id.Text + "' ");
        if (dtcenter.Rows.Count == 0)
        {
            DataTable dtcenter_cd = new DataTable();
            dtcenter_cd = Dblog.Ora_Execute_table("select * from hr_organization_jaba where oj_perg_code = '" + lbleditcawangan.SelectedValue + "' AND oj_perg_code = '" + txtEditkawasan.SelectedValue + "' AND Id != '" + Id.Text + "' ");
            if (dtcenter_cd.Rows.Count == 0)
            {
                conn.Open();

                string cmdstr = "update hr_organization_jaba set oj_org_id=@oj_org_id, oj_perg_code =@oj_perg_code, oj_jaba_cd =@oj_jaba_cd, oj_jaba_desc=@oj_jaba_desc, Status=@Status , op_upd_id=@op_upd_id,op_upd_dt=@op_upd_dt where Id=@Id";

                SqlCommand cmd = new SqlCommand(cmdstr, conn);

                cmd.Parameters.AddWithValue("@Id", Id.Text);

                cmd.Parameters.AddWithValue("@oj_org_id", lbleditwilayah.SelectedValue);

                cmd.Parameters.AddWithValue("@oj_perg_code", lbleditcawangan.SelectedValue);

                cmd.Parameters.AddWithValue("@oj_jaba_cd", txtEditkawasan.SelectedValue);

                cmd.Parameters.AddWithValue("@oj_jaba_desc", txtEditkawasan.SelectedItem.Text);

                cmd.Parameters.AddWithValue("@Status", editddlStatus.SelectedValue);

                cmd.Parameters.AddWithValue("@op_upd_id", Session["new"].ToString());

                cmd.Parameters.AddWithValue("@op_upd_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                cmd.ExecuteNonQuery();

                conn.Close();

                gv_refdata.EditIndex = -1;

                BindData();
                service.audit_trail("P0108", "Kemaskini", "NAMA JABATAN", txtEditkawasan.SelectedItem.Text);
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


    private DataTable RetrieveSubCategories(string categoryID)
    {
        //fetch the connection string from web.config
        string connString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
        //SQL statement to fetch prodyct subcategories
        string sql = @"Select distinct * from hr_organization_pern where op_org_id = '" + categoryID + "'";
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
        lbladdcawangan.DataTextField = "op_perg_name";
        lbladdcawangan.DataValueField = "op_perg_code";
        lbladdcawangan.DataSource = RetrieveSubCategories(lbladdwilayah.SelectedValue);

        lbladdcawangan.DataBind();
        string script1 = " $(function () {  $(" + gv_refdata.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);

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
        lbleditcawangan.DataTextField = "op_perg_name";
        lbleditcawangan.DataValueField = "op_perg_code";
        lbleditcawangan.DataSource = RetrieveSubCategories(lbleditwilayah.SelectedValue);
        lbleditcawangan.DataBind();
        string script1 = " $(function () {  $(" + gv_refdata.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
    }
}
