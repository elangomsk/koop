using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;
using System.Globalization;

public partial class HR_Ref_Table_allowance : System.Web.UI.Page
{

    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    StudentWebService service = new StudentWebService();

    DBConnection DBCon = new DBConnection();
    string level;
    string Status = string.Empty;
    string userid;
    string ref_id;
    string qry1 = string.Empty, qry2 = string.Empty;
    string tn1 = string.Empty, tc1 = string.Empty, tc2 = string.Empty, tc3 = string.Empty, tc4 = string.Empty, tc5 = string.Empty, tc6 = string.Empty, tc7 = string.Empty,tc8 = string.Empty;
    string kwsp, perkeso, pcb,sip;
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
                sel_rt.SelectedValue = "01";
                sel_val();
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
        gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('464','448')");

        CultureInfo culinfo = Thread.CurrentThread.CurrentUICulture;
        TextInfo txtinfo = culinfo.TextInfo;

        h1_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
        bb1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
        bb2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
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
        if (sel_rt.SelectedValue == "01")
        {
            tn1 = "Ref_hr_jenis_elaun";
            tc1 = "hr_elau_Code";
            tc2 = "hr_elau_desc";
            tc3 = "Status";
            tc4 = "Id";
            tc5 = "elau_kwsp";
            tc6 = "elau_perkeso";
            tc7 = "elau_pcb";
            tc8 = "elau_sip";
        }
        if (sel_rt.SelectedValue == "02")
        {
            tn1 = "Ref_hr_potongan";
            tc1 = "hr_poto_Code";
            tc2 = "hr_poto_desc";
            tc3 = "Status";
            tc4 = "Id";
            tc5 = "elau_kwsp";
            tc6 = "elau_perkeso";
            tc7 = "elau_pcb";
            tc8 = "elau_sip";
        }
        if (sel_rt.SelectedValue == "03")
        {
            tn1 = "Ref_hr_type_klm";
            tc1 = "typeklm_cd";
            tc2 = "typeklm_desc";
            tc3 = "Status";
            tc4 = "Id";
            tc5 = "elau_kwsp";
            tc6 = "elau_perkeso";
            tc7 = "elau_pcb";
            tc8 = "elau_sip";
        }

        if (sel_rt.SelectedValue == "04")
        {
            tn1 = "Ref_hr_tunggakan";
            tc1 = "hr_tung_code";
            tc2 = "hr_tung_desc";
            tc3 = "Status";
            tc4 = "Id";
            tc5 = "elau_kwsp";
            tc6 = "elau_perkeso";
            tc7 = "elau_pcb";
            tc8 = "elau_sip";
        }

        if (sel_rt.SelectedValue == "05")
        {
            tn1 = "Ref_hr_Claim";
            tc1 = "hr_clm_code";
            tc2 = "hr_clm_desc";
            tc3 = "Status";
            tc4 = "Id";
            tc5 = "clm_kwsp";
            tc6 = "clm_perkeso";
            tc7 = "clm_pcb";
            tc8 = "clm_sip";
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
            qry1 = "Select " + tc1 + " as c1," + tc2 + " as c2," + tc3 + " as c3," + tc4 + " as c4," + tc5 + " as c5," + tc6 + " as c6," + tc7 + " as c7," + tc8 + " as c8 from " + tn1 + " ORDER BY " + tc4 + " ASC";
        }
        else
        {
            qry1 = "select kavasan_name as c1,wilayah_code as c2,wilayah_name as c3,cawangan_code as c4,cawangan_code as c5,cawangan_code as c6,cawangan_code as c7 from ref_cawangan where kawasan_code = '00000'";
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
        
            TextBox Id = (TextBox)gv_refdata.Rows[e.RowIndex].FindControl("Id");
        System.Web.UI.WebControls.TextBox txtEditcode = (System.Web.UI.WebControls.TextBox)gv_refdata.Rows[e.RowIndex].FindControl("TextBox1");
        conn.Open();
            ref_table();
            string cmdstr = "delete from " + tn1 + " where " + tc4 + "=@Id";
            SqlCommand cmd = new SqlCommand(cmdstr, conn);
            cmd.Parameters.AddWithValue("@Id", Id.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            sel_val();
        service.audit_trail("P0099", "Hapus", "KETERANGAN", txtEditcode.Text);
        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dipadamkan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
       
        string script1 = " $(function () {  $(" + gv_refdata.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
    }

    protected void gv_refdata_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("ADD"))
        {
            TextBox txtAddName = (TextBox)gv_refdata.FooterRow.FindControl("txtAddName");
            TextBox txtAddcode = (TextBox)gv_refdata.FooterRow.FindControl("txtAddcode");
            DropDownList ddlStatus = (DropDownList)gv_refdata.FooterRow.FindControl("ddlStatus");
            CheckBox chk_kwsp = ((CheckBox)gv_refdata.FooterRow.FindControl("chk_kwsp_upd1"));
            if (chk_kwsp != null && chk_kwsp.Checked)
            {
                kwsp = "S";
            }
            else
            {
                kwsp = "T";
            }
            CheckBox chk_perkeso = (CheckBox)gv_refdata.FooterRow.FindControl("chk_perkeso_upd1");
            if (chk_perkeso != null && chk_perkeso.Checked)
            {
                perkeso = "S";
            }
            else
            {
                perkeso = "T";
            }
            CheckBox chk_pcb = (CheckBox)gv_refdata.FooterRow.FindControl("chk_pcb_upd1");
            if (chk_pcb != null && chk_pcb.Checked)
            {
                pcb = "S";
            }
            else
            {
                pcb = "T";
            }
            CheckBox chk_sip = (CheckBox)gv_refdata.FooterRow.FindControl("chk_sip_upd1");
            if (chk_sip != null && chk_sip.Checked)
            {
                sip = "S";
            }
            else
            {
                sip = "T";
            }

            if (txtAddName.Text != "" && txtAddcode.Text != "")
            {

                DataTable dtcenter = new DataTable();
                ref_table();
                dtcenter = Dblog.Ora_Execute_table("select * from " + tn1 + " where " + tc1 + "='" + txtAddName.Text + "'");
                if (dtcenter.Rows.Count == 0)
                {
                    conn.Open();
                    string cmdstr = "insert into " + tn1 + " (" + tc1 + "," + tc2 + "," + tc3 + "," + tc5 + "," + tc6 + "," + tc7 + "," + tc8 + ") values(@" + tc1 + ",@" + tc2 + ",@" + tc3 + ",@" + tc5 + ",@" + tc6 + ",@" + tc7 + ",@" + tc8 + ")";
                    SqlCommand cmd = new SqlCommand(cmdstr, conn);
                    cmd.Parameters.AddWithValue("@" + tc1 + "", txtAddName.Text);
                    cmd.Parameters.AddWithValue("@" + tc2 + "", txtAddcode.Text);
                    cmd.Parameters.AddWithValue("@" + tc3 + "", ddlStatus.SelectedValue);
                    cmd.Parameters.AddWithValue("@" + tc5 + "", kwsp.ToString());
                    cmd.Parameters.AddWithValue("@" + tc6 + "", perkeso.ToString());
                    cmd.Parameters.AddWithValue("@" + tc7 + "", pcb.ToString());
                    cmd.Parameters.AddWithValue("@" + tc8 + "", sip.ToString());
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    sel_val();
                    service.audit_trail("P0099", "Simpan", "KETERANGAN", txtAddcode.Text);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Berjaya Masukkan rekod');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Rekod Sudah Wujud.');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Sila Masukkan Nilai');", true);
                sel_val();
            }
        }
    }

    protected void gv_refdata_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //sel_val();
        TextBox Id = (TextBox)gv_refdata.Rows[e.RowIndex].FindControl("Id");
        Label lblEditName = (Label)gv_refdata.Rows[e.RowIndex].FindControl("lblEditName");
        Label txtEditcode = (Label)gv_refdata.Rows[e.RowIndex].FindControl("txtEditcode");
        DropDownList editddlStatus = (DropDownList)gv_refdata.Rows[e.RowIndex].FindControl("editddlStatus");
        DropDownList ddlStatus = (DropDownList)gv_refdata.FooterRow.FindControl("ddlStatus");
        CheckBox chk_kwsp = (CheckBox)gv_refdata.Rows[e.RowIndex].FindControl("chk_kwsp_upd") as CheckBox;
        if (chk_kwsp != null && chk_kwsp.Checked)
        {
            kwsp = "S";
        }
        else
        {
            kwsp = "T";
        }
        CheckBox chk_perkeso = (CheckBox)gv_refdata.Rows[e.RowIndex].FindControl("chk_perkeso_upd") as CheckBox;
        if (chk_perkeso != null && chk_perkeso.Checked)
        {
            perkeso = "S";
        }
        else
        {
            perkeso = "T";
        }
        CheckBox chk_pcb = (CheckBox)gv_refdata.Rows[e.RowIndex].FindControl("chk_pcb_upd") as CheckBox;
        if (chk_pcb != null && chk_pcb.Checked)
        {
            pcb = "S";
        }
        else
        {
            pcb = "T";
        }
        CheckBox chk_sip = (CheckBox)gv_refdata.Rows[e.RowIndex].FindControl("chk_sip_upd") as CheckBox;
        if (chk_sip != null && chk_sip.Checked)
        {
            sip = "S";
        }
        else
        {
            sip = "T";
        }
        DataTable dtcenter = new DataTable();
        ref_table();
        dtcenter = Dblog.Ora_Execute_table("select * from " + tn1 + " where " + tc2 + "='" + txtEditcode.Text + "' AND " + tc4 + " != '" + Id.Text + "'");
        if (dtcenter.Rows.Count == 0)
        {
            conn.Open();
            string cmdstr = "update " + tn1 + " set " + tc1 + "=@" + tc1 + "," + tc2 + "=@" + tc2 + "," + tc3 + "=@" + tc3 + "," + tc5 + "=@" + tc5 + "," + tc6 + "=@" + tc6 + "," + tc7 + "=@" + tc7 + "," + tc8 + "=@" + tc8 + " where " + tc4 + "=@" + tc4 + "";
            SqlCommand cmd = new SqlCommand(cmdstr, conn);
            cmd.Parameters.AddWithValue("@" + tc1 + "", lblEditName.Text);
            cmd.Parameters.AddWithValue("@" + tc2 + "", txtEditcode.Text);
            cmd.Parameters.AddWithValue("@" + tc3 + "", editddlStatus.SelectedValue);
            cmd.Parameters.AddWithValue("@" + tc5 + "", kwsp.ToString());
            cmd.Parameters.AddWithValue("@" + tc6 + "", perkeso.ToString());
            cmd.Parameters.AddWithValue("@" + tc7 + "", pcb.ToString());
            cmd.Parameters.AddWithValue("@" + tc8 + "", sip.ToString());
            cmd.Parameters.AddWithValue("@" + tc4 + "", Id.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            gv_refdata.EditIndex = -1;
            sel_val();
            service.audit_trail("P0099", "Kemaskini", "KETERANGAN", txtEditcode.Text);
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
}