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

public partial class HR_SELANG_SUBJEK_view : System.Web.UI.Page
{

    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    string level;
    string Status = string.Empty, Status1 = string.Empty, Status2 = string.Empty, Status_del = string.Empty;
    string userid;
    string ref_id;
    string qry1 = string.Empty, qry2 = string.Empty;
    string tn1 = string.Empty, tc1 = string.Empty, tc2 = string.Empty, tc3 = string.Empty, tc4 = string.Empty, tc5 = string.Empty, tc6 = string.Empty, tc7 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        //app_language();
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
                //sel_rt.SelectedValue = "23";
                //BAHA();
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

            h1_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            bb1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            bb2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
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
    
            tn1 = "hr_cmn_appraisal";
            tc1 = "cap_post_cat_cd";
            tc2 = "cap_subject_cd";
            tc3 = "cap_status";
            tc4 = "Id";
            tc5 = "";
            tc6 = "cap_section_cd";
            tc7 = "cap_weightage";
    
    

    }

    //void BAHA()
    //{
    //    DataSet Ds = new DataSet();
    //    try
    //    {

    //        string com = "SELECT cse_section_cd,UPPER(cse_section_desc) cse_section_desc FROM hr_cmn_appr_section WHERE cse_sec_type='01'";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
    //        DataTable dt = new DataTable();
    //        adpt.Fill(dt);
    //        sel_rt.DataSource = dt;
    //        sel_rt.DataBind();
    //        sel_rt.DataTextField = "cse_section_desc";
    //        sel_rt.DataValueField = "cse_section_cd";
    //        sel_rt.DataBind();
    //        sel_rt.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
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
                DropDownList ddList = (DropDownList)e.Row.FindControl("lbleditwilayah_1");

                //return DataTable havinf department data
                DataTable dt = load_department();

                ddList.DataSource = dt;
                ddList.DataTextField = "cse_section_desc";
                ddList.DataValueField = "cse_section_cd";
                ddList.DataBind();
                ddList.Items.Insert(0, new ListItem("--- PILIH ---", ""));
                DataRowView dr = e.Row.DataItem as DataRowView;
                ddList.SelectedValue= dr["c7"].ToString();

            }

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {

            DropDownList lbladdwilayah = (DropDownList)e.Row.FindControl("lbladdwilayah_1");

            DataTable dt = load_department();

            lbladdwilayah.DataSource = dt;

            lbladdwilayah.DataTextField = "cse_section_desc";

            lbladdwilayah.DataValueField = "cse_section_cd";

            lbladdwilayah.DataBind();

            lbladdwilayah.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            conn.Close();

        }
    }

    //public DataTable load_department()
    //{
    //    DataTable dt = new DataTable();
    //    SqlConnection sqlcon = new SqlConnection(cs);
    //    sqlcon.Open();
    //    string sql = "select kod_akaun,(kod_akaun +' | ' + nama_akaun) as nama_akaun from KW_Ref_Carta_Akaun where Status='A'";
    //    SqlCommand cmd = new SqlCommand(sql);
    //    cmd.CommandType = CommandType.Text;
    //    cmd.Connection = sqlcon;
    //    SqlDataAdapter sd = new SqlDataAdapter(cmd);
    //    sd.Fill(dt);
    //    return dt;
    //}
    public DataTable load_department()
    {
        DataTable dt = new DataTable();
        SqlConnection sqlcon = new SqlConnection(cs);
        sqlcon.Open();
        string sql = "SELECT cse_section_cd,UPPER(cse_section_desc) cse_section_desc FROM hr_cmn_appr_section WHERE cse_sec_type='01'";
        SqlCommand cmd = new SqlCommand(sql);
        cmd.CommandType = CommandType.Text;
        cmd.Connection = sqlcon;
        SqlDataAdapter sd = new SqlDataAdapter(cmd);
        sd.Fill(dt);
        return dt;
    }

    protected void gv_refdata_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        BindData();

        gv_refdata.PageIndex = e.NewPageIndex;

        gv_refdata.DataBind();

        string script1 = " $(function () {  $(" + gv_refdata.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
    }

    protected void BindData()
    {
        ref_table();
        //if (sel_rt.SelectedValue != "")
        //{
            qry1 = "Select " + tc1 + " as c1," + tc2 + " as c2,s2." + tc3 + " as c3,s2." + tc4 + " as c4,s1.cse_section_desc as c6,cap_section_cd as c7," + tc7 +" as c8 from " + tn1 + " s2 left join hr_cmn_appr_section s1 on s1.cse_section_cd=" + tc6 + " ORDER BY " + tc1 + " ASC";
        //}
        //else
        //{
        //    qry1 = "select kavasan_name as c1,wilayah_code as c2,wilayah_name as c3,cawangan_code as c4,'' as c5,'' as c6,'' as c7,'' as c8 from ref_cawangan where kawasan_code = '00000'";
        //}
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
        System.Web.UI.WebControls.TextBox txtEditcode = (System.Web.UI.WebControls.TextBox)gv_refdata.Rows[e.RowIndex].FindControl("TextBox1");

        ref_table();
        //DataTable dd1 = new DataTable();
        //dd1 = DBCon.Ora_Execute_table("select * from KW_ref_jurnal_inter where jur_module='M0001' and jur_item='" + sel_rt.SelectedItem.Text + "' and jur_desc_cd='" + cod.Text + "' and jur_dr_gl_cd != '' and jur_cr_gl_cd !='' and status='A'");
        //if (dd1.Rows.Count == 0)
        //{
            conn.Open();
            string cmdstr = "delete from " + tn1 + " where " + tc4 + "=@Id";
            SqlCommand cmd = new SqlCommand(cmdstr, conn);
            cmd.Parameters.AddWithValue("@Id", Id.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            //string del_jurn = "delete from KW_ref_jurnal_inter where jur_module='M0001' and jur_item='" + sel_rt.SelectedItem.Text + "' and jur_desc_cd='" + cod.Text + "'";
            //Status = DBCon.Ora_Execute_CommamdText(del_jurn);
            //service.audit_trail("P0202", "Hapus", "TUNTUTAN", txtEditcode.Text);
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dipadamkan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('COA HAVE BEEN SET, DELETING THIS INFO IS PROHIBITED.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        //}
        sel_val();     

        string script1 = " $(function () {  $(" + gv_refdata.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
    }

    protected void gv_refdata_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("ADD"))
        {
            System.Web.UI.WebControls.DropDownList bah = (System.Web.UI.WebControls.DropDownList)gv_refdata.FooterRow.FindControl("lbladdwilayah_1");
            System.Web.UI.WebControls.TextBox txtAddName = (System.Web.UI.WebControls.TextBox)gv_refdata.FooterRow.FindControl("txtAddName");
            System.Web.UI.WebControls.TextBox txtAddcode = (System.Web.UI.WebControls.TextBox)gv_refdata.FooterRow.FindControl("txtAddcode");
            System.Web.UI.WebControls.TextBox txtAddcode_amt = (System.Web.UI.WebControls.TextBox)gv_refdata.FooterRow.FindControl("txtAddcode_amt");
            DropDownList lbladdkodID = (DropDownList)gv_refdata.FooterRow.FindControl("lbladdkodID");
            DropDownList ddlStatus = (DropDownList)gv_refdata.FooterRow.FindControl("ddlStatus");
            if (txtAddName.Text != "" && txtAddcode.Text != "")
            {
                string set_cnt = string.Empty, set_cnt1 = string.Empty, mcnt = string.Empty;
                string sno1 = string.Empty, sno2 = string.Empty, sno3 = string.Empty, cnt_value = string.Empty;
                string sso1 = string.Empty, sso2 = string.Empty, sso3 = string.Empty, entry = string.Empty, pv = string.Empty; 
                string chk_role = string.Empty;
               
               
                DataTable dtcenter = new DataTable();
                ref_table();
                dtcenter = Dblog.Ora_Execute_table("select * from " + tn1 + " where "+ tc6 +" = '"+ bah.SelectedValue + "' and " + tc2 + "='" + txtAddName.Text + "'");

                if (dtcenter.Rows.Count == 0)
                {
                  
                    string Inssql = "insert into " + tn1 + " (" + tc1 + "," + tc2 + "," + tc3 + "," + tc6 + "," + tc7 + ") values('" + txtAddName.Text + "','" + txtAddcode.Text + "','" + ddlStatus.SelectedValue + "','" + bah.SelectedValue + "','"+ txtAddcode_amt.Text + "')";
                    Status = DBCon.Ora_Execute_CommamdText(Inssql);
                    if (Status == "SUCCESS")
                    {
                        //if (sel_rt.SelectedValue == "23")
                        //{
                        //    entry = "D";
                        //    pv = "K";

                        //    string Inssql_main = "Insert into KW_ref_jurnal_inter (jur_module,jur_item,jur_desc,jur_desc_cd,crt_id,crt_dt,status,jur_entry_type,jur_pv_type) values ('M0001','" + sel_rt.SelectedItem.Text + "','" + txtAddcode.Text + "','" + txtAddName.Text + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A','" + entry + "','" + pv + "')";
                        //    Status = DBCon.Ora_Execute_CommamdText(Inssql_main);
                        //}
                        sel_val();
                        //service.audit_trail("P0202", "Simpan", "TUNTUTAN", txtAddcode.Text);
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
        System.Web.UI.WebControls.DropDownList edit_bah = (System.Web.UI.WebControls.DropDownList)gv_refdata.Rows[e.RowIndex].FindControl("lbleditwilayah_1");
        System.Web.UI.WebControls.TextBox Id = (System.Web.UI.WebControls.TextBox)gv_refdata.Rows[e.RowIndex].FindControl("Id");
        System.Web.UI.WebControls.TextBox lblEditName = (System.Web.UI.WebControls.TextBox)gv_refdata.Rows[e.RowIndex].FindControl("lblEditName");
        System.Web.UI.WebControls.TextBox txtEditcode = (System.Web.UI.WebControls.TextBox)gv_refdata.Rows[e.RowIndex].FindControl("txtEditcode");
        System.Web.UI.WebControls.TextBox txtEditcode_amt = (System.Web.UI.WebControls.TextBox)gv_refdata.Rows[e.RowIndex].FindControl("txtEditcode_amt");
        DropDownList lbleditkodID = (DropDownList)gv_refdata.Rows[e.RowIndex].FindControl("lbleditkodID");
        DropDownList editddlStatus = (DropDownList)gv_refdata.Rows[e.RowIndex].FindControl("editddlStatus");
        DataTable dtcenter = new DataTable();
        ref_table();
        dtcenter = Dblog.Ora_Execute_table("select * from " + tn1 + " where " + tc6 + "='" + edit_bah.SelectedValue + "' and " + tc2 + "='" + txtEditcode.Text + "' AND " + tc4 + " != '" + Id.Text + "'");
        if (dtcenter.Rows.Count == 0)
        {
            string set_cnt = string.Empty, set_cnt1 = string.Empty, mcnt = string.Empty;
            string sno1 = string.Empty, sno2 = string.Empty, sno3 = string.Empty, cnt_value = string.Empty, entry = string.Empty, pv = string.Empty;
          
                string Inssql = "update " + tn1 + " set " + tc1 + "='" + lblEditName.Text + "'," + tc2 + "='" + txtEditcode.Text + "'," + tc3 + "='" + editddlStatus.SelectedValue + "'," + tc6 + "='" + edit_bah.SelectedValue + "'," + tc7 + "='" + txtEditcode_amt.Text + "' where " + tc4 + "='" + Id.Text + "'";
                Status = DBCon.Ora_Execute_CommamdText(Inssql);
                if (Status == "SUCCESS")
                {
                //if (sel_rt.SelectedValue == "23")
                //{
                //    entry = "D";
                //    pv = "K";

                //    string Inssql_main = string.Empty;
                //    DataTable dd1 = new DataTable();
                //    dd1 = DBCon.Ora_Execute_table("select * from KW_ref_jurnal_inter where jur_module='M0001' and jur_item='" + sel_rt.SelectedItem.Text + "' and jur_desc_cd='" + lblEditName.Text + "'");
                //    if (dd1.Rows.Count != 0)
                //    {
                //        Inssql_main = "update KW_ref_jurnal_inter set jur_desc='" + txtEditcode.Text + "',upd_id='" + Session["New"].ToString() + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',status='A',jur_entry_type='" + entry + "',jur_pv_type='" + pv + "' where jur_module='M0001' and jur_item='" + sel_rt.SelectedItem.Text + "' and jur_desc_cd='" + lblEditName.Text + "'";
                //        Status = DBCon.Ora_Execute_CommamdText(Inssql_main);
                //    }
                //    else
                //    {
                //        Inssql_main = "Insert into KW_ref_jurnal_inter (jur_module,jur_item,jur_desc,jur_desc_cd,crt_id,crt_dt,status,jur_entry_type,jur_pv_type) values ('M0001','" + sel_rt.SelectedItem.Text + "','" + txtEditcode.Text + "','" + lblEditName.Text + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A','" + entry + "','" + pv + "')";
                //        Status = DBCon.Ora_Execute_CommamdText(Inssql_main);
                //    }
                //}
                gv_refdata.EditIndex = -1;
                  sel_val();
                //service.audit_trail("P0202", "Kemaskini", "TUNTUTAN", txtEditcode.Text);
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
