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
using System.Text.RegularExpressions;
using System.Threading;

public partial class kw_template_pandl : System.Web.UI.Page
{

    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    string level;
    string Status = string.Empty, Status1 = string.Empty, Status2 = string.Empty, Status_del = string.Empty;
    string userid;
    string ref_id;
    string qry1 = string.Empty, qry2 = string.Empty;
    string tn1 = string.Empty, tc1 = string.Empty, tc2 = string.Empty, tc3 = string.Empty, tc4 = string.Empty, tc5 = string.Empty, tc6 = string.Empty, tc7 = string.Empty, tc8 = string.Empty, tc9 = string.Empty, tc10 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
                sel_rt.SelectedValue = "00";
                sel_val();
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('455','448','452')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentUICulture;
            TextInfo txtinfo = culinfo.TextInfo;

            //h1_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            //bb1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            //bb2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            //lbl1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
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
        if (sel_rt.SelectedValue == "00")
        {
            tn1 = "KW_Ref_pnl_item";
            tc1 = "ref_pnl_kod";
            tc2 = "ref_pnl_nama";
            tc5 = "under_parent";
            tc6 = "ref_pnl_header";
            tc3 = "ref_pnl_status";
            tc4 = "Id";
            tc7 = "K";
            tc8 = "Y";
            tc9 = "K";
            tc10 = "ref_pnl_jenis";
            this.gv_refdata.Columns[1].Visible = false;
            this.gv_refdata.Columns[2].Visible = false;
            this.gv_refdata.Columns[5].Visible = true;
        }
        else if (sel_rt.SelectedValue == "01")
        {
            tn1 = "KW_Ref_pnl_item";
            tc1 = "ref_pnl_kod";
            tc2 = "ref_pnl_nama";
            tc5 = "under_parent";
            tc6 = "ref_pnl_header";
            tc3 = "ref_pnl_status";
            tc4 = "Id";
            tc7 = "Y";
            tc8 = "K";
            tc9 = "K";
            tc10 = "ref_pnl_jenis";
            this.gv_refdata.Columns[1].Visible = true;
            this.gv_refdata.Columns[2].Visible = false;
            this.gv_refdata.Columns[5].Visible = false;
        }
        else if (sel_rt.SelectedValue == "02")
        {
            tn1 = "KW_Ref_pnl_item";
            tc1 = "ref_pnl_kod";
            tc2 = "ref_pnl_nama";
            tc5 = "under_parent";
            tc6 = "ref_pnl_header";
            tc3 = "ref_pnl_status";
            tc4 = "Id";
            tc7 = "N";
            tc8 = "Y";
            tc9 = "K";
            tc10 = "ref_pnl_jenis";
            this.gv_refdata.Columns[1].Visible = true;
            this.gv_refdata.Columns[2].Visible = true;
            this.gv_refdata.Columns[5].Visible = false;
        }
       
    }

    void sel_val()
    {
        ref_table();
        BindData();
    }


    protected void gv_refdata_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                DropDownList ddList = (DropDownList)e.Row.FindControl("lbledit_hdr");
                DropDownList ddList1 = (DropDownList)e.Row.FindControl("lbledit_hdr1");
              

                //return DataTable havinf department data
                DataTable dt = load_department();
              

                ddList.DataSource = dt;
                ddList.DataTextField = "ref_pnl_nama";
                ddList.DataValueField = "Id";
                ddList.DataBind();
                ddList.Items.Insert(0, new ListItem("--- PILIH ---", ""));
                DataRowView dr = e.Row.DataItem as DataRowView;
                ddList.SelectedValue = dr["c9"].ToString();
                Session["get_cd"] = dr["c9"].ToString();
                DataTable dt1 = load_department1();
                ddList1.DataSource = dt1;
                ddList1.DataTextField = "ref_pnl_nama";
                ddList1.DataValueField = "Id";
                ddList1.DataBind();
                ddList1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
                DataRowView dr1 = e.Row.DataItem as DataRowView;
                ddList1.SelectedValue = dr1["c7"].ToString();

            }

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {

            DropDownList lbladdwilayah = (DropDownList)e.Row.FindControl("lbladd_hdr");

          
            DataTable dt = load_department();

            lbladdwilayah.DataSource = dt;

            lbladdwilayah.DataTextField = "ref_pnl_nama";

            lbladdwilayah.DataValueField = "Id";

            lbladdwilayah.DataBind();

            lbladdwilayah.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            lbladdwilayah.SelectedValue = Session["get_cd"].ToString();

            DropDownList lbladdwilayah1 = (DropDownList)e.Row.FindControl("lbladd_hdr1");

            DataTable dt1 = load_department1();

            lbladdwilayah1.DataSource = dt1;

            lbladdwilayah1.DataTextField = "ref_pnl_nama";

            lbladdwilayah1.DataValueField = "Id";

            lbladdwilayah1.DataBind();

            lbladdwilayah1.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            conn.Close();
           
        }
    }

    public DataTable load_department()
    {
        DataTable dt = new DataTable();
        SqlConnection sqlcon = new SqlConnection(cs);
        sqlcon.Open();
        string sql = "SELECT Id,UPPER(ref_pnl_nama) ref_pnl_nama FROM KW_Ref_pnl_item WHERE ref_pnl_header='K'";
        SqlCommand cmd = new SqlCommand(sql);
        cmd.CommandType = CommandType.Text;
        cmd.Connection = sqlcon;
        SqlDataAdapter sd = new SqlDataAdapter(cmd);
        sd.Fill(dt);
        return dt;
    }

    public DataTable load_department1()
    {
        DataTable dt1 = new DataTable();
        SqlConnection sqlcon = new SqlConnection(cs);
        sqlcon.Open();
        string sql = "SELECT Id,UPPER(ref_pnl_nama) ref_pnl_nama FROM KW_Ref_pnl_item WHERE under_parent='" + Session["get_cd"].ToString() + "' and ref_pnl_header='Y'";
        SqlCommand cmd = new SqlCommand(sql);
        cmd.CommandType = CommandType.Text;
        cmd.Connection = sqlcon;
        SqlDataAdapter sd = new SqlDataAdapter(cmd);
        sd.Fill(dt1);
        return dt1;
    }

   


    protected void gv_refdata_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        BindData();

        gv_refdata.PageIndex = e.NewPageIndex;

        gv_refdata.DataBind();

        string script1 = " $(function () {  $(" + gv_refdata.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
    }

    protected void bind_header(object sender, EventArgs e)
    {
        int selRowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
        String get_cd = (gv_refdata.FooterRow.FindControl("lbladd_hdr") as DropDownList).SelectedValue;
        DropDownList ddList = (DropDownList)gv_refdata.FooterRow.FindControl("lbladd_hdr1");
        Session["get_cd"] = get_cd;
        if (sel_rt.SelectedValue == "02")
        {
            DataTable dt = new DataTable();
            SqlConnection sqlcon = new SqlConnection(cs);
            sqlcon.Open();
            string sql = "SELECT Id,UPPER(ref_pnl_nama) ref_pnl_nama FROM KW_Ref_pnl_item WHERE under_parent='" + Session["get_cd"].ToString() + "' and ref_pnl_header='Y'";
            SqlCommand cmd = new SqlCommand(sql);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlcon;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            sd.Fill(dt);
            ddList.DataSource = dt;
            ddList.DataTextField = "ref_pnl_nama";
            ddList.DataValueField = "Id";
            ddList.DataBind();
            ddList.Items.Insert(0, new ListItem("--- PILIH ---", ""));
           
        }
        BindData();
        string script1 = " $(function () {  $(" + gv_refdata.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
    }
    protected void BindData()
    {
        ref_table();
        if (sel_rt.SelectedValue != "")
        {
            qry1 = "Select s2." + tc1 + " as c1,s2." + tc2 + " as c2,s2." + tc3 + " as c3,s3.ref_pnl_nama as c5,s2." + tc6 + " as c6,s2." + tc4 + " as c4,s3.Id as c7,case when s2.ref_pnl_header='Y' then s3.ref_pnl_nama else s4.ref_pnl_nama end as c8,case when s2.ref_pnl_header='Y' then s3.Id else s4.Id end as c9,s2." + tc10 + " as c31 from " + tn1 + " s2 left join KW_Ref_pnl_item s3 on s3.Id=s2.under_parent and s3.ref_pnl_header = '" + tc8 + "' left join KW_Ref_pnl_item s4 on s4.Id=s3.under_parent and s4.ref_pnl_header = '" + tc9 + "'  where s2." + tc6+" = '"+ tc7 + "'  ORDER BY s2." + tc1 + " ASC";
        }
        else
        {
            qry1 = "select kavasan_name as c1,wilayah_code as c2,wilayah_name as c3,cawangan_code as c4,'' as c5,'' as c6,'' as c7,'' as c8,'' as c9,'' as c31 from ref_cawangan where kawasan_code = '00000'";
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

        System.Web.UI.WebControls.TextBox Id = (System.Web.UI.WebControls.TextBox)gv_refdata.Rows[e.RowIndex].FindControl("Id");
        System.Web.UI.WebControls.Label cod = (System.Web.UI.WebControls.Label)gv_refdata.Rows[e.RowIndex].FindControl("lblEmpID");

        ref_table();
        DataTable dd1 = new DataTable();
        dd1 = DBCon.Ora_Execute_table("select * from KW_ref_jurnal_inter where jur_module='M0001' and jur_item='" + sel_rt.SelectedItem.Text + "' and jur_desc_cd='" + cod.Text + "' and jur_dr_gl_cd != '' and jur_cr_gl_cd !='' and status='A'");
        if (dd1.Rows.Count == 0)
        {
            conn.Open();
            string cmdstr = "delete from " + tn1 + " where " + tc4 + "=@Id";
            SqlCommand cmd = new SqlCommand(cmdstr, conn);
            cmd.Parameters.AddWithValue("@Id", Id.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('COA HAVE BEEN SET, DELETING THIS INFO IS PROHIBITED.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
        sel_val();
        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dipadamkan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);


        string script1 = " $(function () {  $(" + gv_refdata.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
    }

    protected void gv_refdata_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("ADD"))
        {
            System.Web.UI.WebControls.TextBox txtAddName = (System.Web.UI.WebControls.TextBox)gv_refdata.FooterRow.FindControl("txtAddName");
            System.Web.UI.WebControls.TextBox txtAddcode = (System.Web.UI.WebControls.TextBox)gv_refdata.FooterRow.FindControl("txtAddcode");
            System.Web.UI.WebControls.DropDownList lbladdhdr = (System.Web.UI.WebControls.DropDownList)gv_refdata.FooterRow.FindControl("lbladd_hdr");
            System.Web.UI.WebControls.DropDownList lbladdhdr1 = (System.Web.UI.WebControls.DropDownList)gv_refdata.FooterRow.FindControl("lbladd_hdr1");
            DropDownList lbladdkodID = (DropDownList)gv_refdata.FooterRow.FindControl("lbladdkodID");
            DropDownList ddlStatus = (DropDownList)gv_refdata.FooterRow.FindControl("ddlStatus");
            DropDownList ddljen_txi = (DropDownList)gv_refdata.FooterRow.FindControl("ddljen_txi");
            if (txtAddName.Text != "" && txtAddcode.Text != "")
            {
                string set_cnt = string.Empty, set_cnt1 = string.Empty, mcnt = string.Empty;
                string sno1 = string.Empty, sno2 = string.Empty, sno3 = string.Empty, cnt_value = string.Empty;
                string sso1 = string.Empty, sso2 = string.Empty, sso3 = string.Empty;
                string chk_role = string.Empty;
                DataTable dtcenter = new DataTable();
                ref_table();
                dtcenter = Dblog.Ora_Execute_table("select * from " + tn1 + " where " + tc1 + "='" + txtAddName.Text + "'");

                if (dtcenter.Rows.Count == 0)
                {
                    string up_val = string.Empty;
                    if(sel_rt.SelectedValue == "02")
                    {
                        up_val = lbladdhdr1.SelectedValue;
                    }
                    else
                    {
                        up_val = lbladdhdr.SelectedValue;
                    }

                    string Inssql = "insert into " + tn1 + " (" + tc1 + "," + tc2 + "," + tc3 + "," + tc5 + "," + tc6 + "," + tc10 + ",crt_id,cr_dt) values('" + txtAddName.Text + "','" + txtAddcode.Text + "','" + ddlStatus.SelectedValue + "','"+ up_val + "','"+ tc7 + "','" + ddljen_txi.SelectedValue + "','" + Session["new"].ToString() +"','"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +"')";
                    Status = DBCon.Ora_Execute_CommamdText(Inssql);
                    if (Status == "SUCCESS")
                    {
                        Session["get_cd"] = "";
                        sel_val();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
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
                sel_val();
            }
        }

        string script1 = " $(function () {  $(" + gv_refdata.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
    }

    protected void gv_refdata_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //sel_val();
        System.Web.UI.WebControls.TextBox Id = (System.Web.UI.WebControls.TextBox)gv_refdata.Rows[e.RowIndex].FindControl("Id");
        System.Web.UI.WebControls.TextBox lblEditName = (System.Web.UI.WebControls.TextBox)gv_refdata.Rows[e.RowIndex].FindControl("lblEditName");
        System.Web.UI.WebControls.TextBox txtEditcode = (System.Web.UI.WebControls.TextBox)gv_refdata.Rows[e.RowIndex].FindControl("txtEditcode");
        System.Web.UI.WebControls.DropDownList lbledithdr = (System.Web.UI.WebControls.DropDownList)gv_refdata.Rows[e.RowIndex].FindControl("lbledit_hdr");
        System.Web.UI.WebControls.DropDownList lbledithdr1 = (System.Web.UI.WebControls.DropDownList)gv_refdata.Rows[e.RowIndex].FindControl("lbledit_hdr1");
        DropDownList lbleditkodID = (DropDownList)gv_refdata.Rows[e.RowIndex].FindControl("lbleditkodID");
        DropDownList editddlStatus = (DropDownList)gv_refdata.Rows[e.RowIndex].FindControl("editddlStatus");
        DropDownList editddljen_txi = (DropDownList)gv_refdata.Rows[e.RowIndex].FindControl("editddljen_txi");
        DataTable dtcenter = new DataTable();
        ref_table();
        dtcenter = Dblog.Ora_Execute_table("select * from " + tn1 + " where " + tc2 + "='" + txtEditcode.Text + "' AND " + tc4 + " != '" + Id.Text + "'");
        if (dtcenter.Rows.Count == 0)
        {
            string set_cnt = string.Empty, set_cnt1 = string.Empty, mcnt = string.Empty;
            string sno1 = string.Empty, sno2 = string.Empty, sno3 = string.Empty, cnt_value = string.Empty;
            DataTable cnt_no = new DataTable();
            string up_val = string.Empty;
            if (sel_rt.SelectedValue == "02")
            {
                up_val = lbledithdr1.SelectedValue;
            }
            else
            {
                up_val = lbledithdr.SelectedValue;
            }
            string Inssql = "update " + tn1 + " set " + tc10 + "='" + editddljen_txi.SelectedValue + "'," + tc1 + "='" + lblEditName.Text + "'," + tc2 + "='" + txtEditcode.Text + "'," + tc3 + "='" + editddlStatus.SelectedValue + "'," + tc5 + "='" + up_val + "',upd_id='" + Session["new"].ToString() + "',upd_dt='"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +"' where " + tc4 + "='" + Id.Text + "'";
            Status = DBCon.Ora_Execute_CommamdText(Inssql);
            if (Status == "SUCCESS")
            {
                Session["get_cd"] = "";
                gv_refdata.EditIndex = -1;
                sel_val();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
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

}
