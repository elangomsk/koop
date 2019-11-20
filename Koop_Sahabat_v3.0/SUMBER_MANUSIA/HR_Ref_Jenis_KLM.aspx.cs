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

public partial class SUMBER_MANUSIA_HR_Ref_Jenis_KLM : System.Web.UI.Page
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
    string qry1 = string.Empty, qry2 = string.Empty;
    string tn1 = string.Empty, tc1 = string.Empty, tc2 = string.Empty, tc3 = string.Empty, tc4 = string.Empty, tc5 = string.Empty, tc6 = string.Empty, tc7 = string.Empty, tc8 = string.Empty, tc9 = string.Empty, tc10 = string.Empty;
    string h1 = string.Empty, h2 = string.Empty, h3 = string.Empty, h4 = string.Empty, h5 = string.Empty, sq1 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
               
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
        //if (Session["New"] != null)
        //{
        //    DataTable ste_set = new DataTable();
        //    ste_set = DBCon.Ora_Execute_table("select * from site_settings where ID IN ('1')");

        //    DataTable gt_lng = new DataTable();
        //    gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('479','448','452')");

        //    CultureInfo culinfo = Thread.CurrentThread.CurrentUICulture;
        //    TextInfo txtinfo = culinfo.TextInfo;

        //    h1_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
        //    bb1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
        //    bb2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
        //    lbl1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
        //}
        //else
        //{
        //    Response.Redirect("../KSAIMB_Login.aspx");
        //}
    }

    protected void dd_SelectedIndexChanged(object sender, EventArgs e)
    {
        sel_val();
        string script1 = " $(function () {  $(" + gv_refdata.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);

    }
    //void ref_table()
    //{
    //    if (sel_rt.SelectedValue == "01")
    //    {
    //        tn1 = "Ref_hr_gelaran";
    //        tc1 = "hr_gelaran_Code";
    //        tc2 = "hr_gelaran_desc";
    //        tc3 = "Status";
    //        tc4 = "Id";
    //    }
    //    else if (sel_rt.SelectedValue == "02")
    //    {
    //        tn1 = "Ref_hr_gred";
    //        tc1 = "hr_gred_Code";
    //        tc2 = "hr_gred_desc";
    //        tc3 = "Status";
    //        tc4 = "Id";
    //    }
    //    else if (sel_rt.SelectedValue == "03")
    //    {
    //        tn1 = "Ref_hr_jabatan";
    //        tc1 = "hr_jaba_Code";
    //        tc2 = "hr_jaba_desc";
    //        tc3 = "Status";
    //        tc4 = "Id";
    //    }
    //    else if (sel_rt.SelectedValue == "04")
    //    {
    //        tn1 = "Ref_hr_Jad_kerj";
    //        tc1 = "hr_jad_Code";
    //        tc2 = "hr_jad_desc";
    //        tc3 = "Status";
    //        tc4 = "Id";
    //    }
    //    //else if (sel_rt.SelectedValue == "05")
    //    //{
    //    //    tn1 = "Ref_hr_jantina";
    //    //    tc1 = "hr_jantina_Code";
    //    //    tc2 = "hr_jantina_desc";
    //    //    tc3 = "Status";
    //    //    tc4 = "Id";
    //    //}
    //    else if (sel_rt.SelectedValue == "06")
    //    {
    //        tn1 = "Ref_hr_Jawatan";
    //        tc1 = "hr_jaw_Code";
    //        tc2 = "hr_jaw_desc";
    //        tc3 = "Status";
    //        tc4 = "Id";
    //    }
    //    else if (sel_rt.SelectedValue == "07")
    //    {
    //        tn1 = "Ref_hr_jenis_elaun";
    //        tc1 = "hr_elau_Code";
    //        tc2 = "hr_elau_desc";
    //        tc3 = "Status";
    //        tc4 = "Id";
    //    }
    //    else if (sel_rt.SelectedValue == "08")
    //    {
    //        tn1 = "Ref_hr_jenis_latihan";
    //        tc1 = "jen_latihan_cd";
    //        tc2 = "jen_latihan_desc";
    //        tc3 = "Status";
    //        tc4 = "Id";
    //    }
    //    else if (sel_rt.SelectedValue == "09")
    //    {
    //        tn1 = "Ref_hr_kat_latihan";
    //        tc1 = "kat_latihan_cd";
    //        tc2 = "kat_latihan_desc";
    //        tc3 = "Status";
    //        tc4 = "Id";
    //    }
    //    //else if (sel_rt.SelectedValue == "10")
    //    //{
    //    //    tn1 = "Ref_ast_Jenis_Pegangan";
    //    //    tc1 = "ast_Pegangan_code";
    //    //    tc2 = "ast_Pegangan_desc";
    //    //    tc3 = "Status";
    //    //    tc4 = "Id";
    //    //}
    //    else if (sel_rt.SelectedValue == "11")
    //    {
    //        tn1 = "Ref_hr_leave_sts";
    //        tc1 = "hr_leave_Code";
    //        tc2 = "hr_leave_desc";
    //        tc3 = "Status";
    //        tc4 = "Id";
    //    }
    //    else if (sel_rt.SelectedValue == "12")
    //    {
    //        tn1 = "Ref_hr_penamatan";
    //        tc1 = "hr_pen_Code";
    //        tc2 = "hr_pen_desc";
    //        tc3 = "Status";
    //        tc4 = "Id";
    //    }
    //    else if (sel_rt.SelectedValue == "13")
    //    {
    //        tn1 = "Ref_hr_pendidikan";
    //        tc1 = "hr_pendi_Code";
    //        tc2 = "hr_pendi_desc";
    //        tc3 = "Status";
    //        tc4 = "Id";
    //    }
    //    else if (sel_rt.SelectedValue == "14")
    //    {
    //        tn1 = "Ref_hr_Pengesahan_sts";
    //        tc1 = "hr_Pengesahan_Code";
    //        tc2 = "hr_Pengesahan_desc";
    //        tc3 = "Status";
    //        tc4 = "Id";
    //    }
    //    else if (sel_rt.SelectedValue == "15")
    //    {
    //        tn1 = "Ref_hr_penj_kategori";
    //        tc1 = "hr_kate_Code";
    //        tc2 = "hr_kate_desc";
    //        tc3 = "Status";
    //        tc4 = "Id";
    //    }

    //    else if (sel_rt.SelectedValue == "16")
    //    {
    //        tn1 = "Ref_hr_penj_traf";
    //        tc1 = "hr_traf_Code";
    //        tc2 = "hr_traf_desc";
    //        tc3 = "Status";
    //        tc4 = "Id";
    //    }

    //    else if (sel_rt.SelectedValue == "17")
    //    {
    //        tn1 = "Ref_hr_per_kaki";
    //        tc1 = "hr_perkaki_Code";
    //        tc2 = "hr_perkaki_desc";
    //        tc3 = "Status";
    //        tc4 = "Id";
    //    }
    //    else if (sel_rt.SelectedValue == "18")
    //    {
    //        tn1 = "Ref_hr_potongan";
    //        tc1 = "hr_poto_Code";
    //        tc2 = "hr_poto_desc";
    //        tc3 = "Status";
    //        tc4 = "Id";
    //    }
    //    else if (sel_rt.SelectedValue == "19")
    //    {
    //        tn1 = "Ref_hr_sebab";
    //        tc1 = "seb_cd";
    //        tc2 = "seb_desc";
    //        tc3 = "Status";
    //        tc4 = "Id";
    //    }
    //    else if (sel_rt.SelectedValue == "20")
    //    {
    //        tn1 = "Ref_hr_skim";
    //        tc1 = "hr_skim_Code";
    //        tc2 = "hr_skim_desc";
    //        tc3 = "Status";
    //        tc4 = "Id";
    //    }
    //    else if (sel_rt.SelectedValue == "21")
    //    {
    //        tn1 = "Ref_hr_sts_perkha";
    //        tc1 = "hr_perkha_Code";
    //        tc2 = "hr_perkha_desc";
    //        tc3 = "Status";
    //        tc4 = "Id";
    //    }
    //    else if (sel_rt.SelectedValue == "22")
    //    {
    //        tn1 = "Ref_hr_title";
    //        tc1 = "hr_titl_Code";
    //        tc2 = "hr_titl_desc";
    //        tc3 = "Status";
    //        tc4 = "Id";
    //    }
    //    else if (sel_rt.SelectedValue == "23")
    //    {
    //        tn1 = "Ref_hr_tuntutan";
    //        tc1 = "hr_tun_Code";
    //        tc2 = "hr_tun_desc";
    //        tc3 = "Status";
    //        tc4 = "Id";
    //    }
    //    else if (sel_rt.SelectedValue == "24")
    //    {
    //        tn1 = "Ref_hr_type_klm";
    //        tc1 = "typeklm_cd";
    //        tc2 = "typeklm_desc";
    //        tc3 = "Status";
    //        tc4 = "Id";
    //    }
    //    else if (sel_rt.SelectedValue == "25")
    //    {
    //        tn1 = "Ref_hr_wargan";
    //        tc1 = "hr_wargan_Code";
    //        tc2 = "hr_wargan_desc";
    //        tc3 = "Status";
    //        tc4 = "Id";
    //    }

    //    else if (sel_rt.SelectedValue == "26")
    //    {
    //        tn1 = "Ref_hr_jenis_cuti";
    //        tc1 = "hr_jenis_Code";
    //        tc2 = "hr_jenis_desc";
    //        tc3 = "Status";
    //        tc4 = "Id";
    //    }
    //    else if (sel_rt.SelectedValue == "27")
    //    {
    //        tn1 = "Ref_hr_universiti";
    //        tc1 = "hr_univ_Code";
    //        tc2 = "hr_univ_desc";
    //        tc3 = "Status";
    //        tc4 = "Id";
    //    }

    //    else if (sel_rt.SelectedValue == "28")
    //    {
    //        tn1 = "Ref_hr_tunggakan";
    //        tc1 = "hr_tung_Code";
    //        tc2 = "hr_tung_desc";
    //        tc3 = "Status";
    //        tc4 = "Id";
    //    }
    //}

    void sel_val()
    {

       // ref_table();
        //head_id.Text = sel_rt.SelectedItem.Text;
        BindData();

    }

    protected void BindData()

    {

       
        qry1 = "select Id,typeklm_cd,typeklm_desc,typeklm_weight,Status from Ref_hr_type_klm  ORDER BY Id ASC";
       
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
        System.Web.UI.WebControls.TextBox txtEditcode = (System.Web.UI.WebControls.TextBox)gv_refdata.Rows[e.RowIndex].FindControl("TextBox1");

        conn.Open();
     
        string cmdstr = "delete from Ref_hr_type_klm where Id=@Id";

        SqlCommand cmd = new SqlCommand(cmdstr, conn);

        cmd.Parameters.AddWithValue("@Id", Id.Text);

        cmd.ExecuteNonQuery();

        conn.Close();

        sel_val();
        service.audit_trail("P0214", "Hapus","KLM", txtEditcode.Text);
        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya dipadamkan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);


        string script1 = " $(function () {  $(" + gv_refdata.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
    }

    protected void gv_refdata_RowCommand(object sender, GridViewCommandEventArgs e)

    {

        if (e.CommandName.Equals("ADD"))

        {

            System.Web.UI.WebControls.TextBox txtAddName = (System.Web.UI.WebControls.TextBox)gv_refdata.FooterRow.FindControl("txtAddName");

            System.Web.UI.WebControls.TextBox txtAddcode = (System.Web.UI.WebControls.TextBox)gv_refdata.FooterRow.FindControl("txtAddcode");

            System.Web.UI.WebControls.TextBox txtAddwgt = (System.Web.UI.WebControls.TextBox)gv_refdata.FooterRow.FindControl("txtAddwgt");

            DropDownList ddlStatus = (DropDownList)gv_refdata.FooterRow.FindControl("ddlStatus");

            if (txtAddName.Text != "" && txtAddcode.Text != "")
            {

                DataTable dtcenter = new DataTable();
             
                dtcenter = Dblog.Ora_Execute_table("select * from Ref_hr_type_klm where typeklm_desc='" + txtAddName.Text + "'");
                if (dtcenter.Rows.Count == 0)
                {

                    conn.Open();


                    string cmdstr = "insert into Ref_hr_type_klm (typeklm_desc,typeklm_cd,typeklm_weight,Status) values(@typeklm_desc,@typeklm_cd,@typeklm_weight,@Status)";

                    SqlCommand cmd = new SqlCommand(cmdstr, conn);

                    cmd.Parameters.AddWithValue("@typeklm_desc", txtAddcode.Text );
                    cmd.Parameters.AddWithValue("@typeklm_cd", txtAddName.Text);
                    cmd.Parameters.AddWithValue("@typeklm_weight", txtAddwgt.Text);
                    cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);

                    cmd.ExecuteNonQuery();

                    conn.Close();

                    sel_val();
                    service.audit_trail("P0214", "Simpan", "KLM", txtAddcode.Text);
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
        System.Web.UI.WebControls.TextBox txtEditwgt = (System.Web.UI.WebControls.TextBox)gv_refdata.Rows[e.RowIndex].FindControl("txtEditwgt");
        DropDownList editddlStatus = (DropDownList)gv_refdata.Rows[e.RowIndex].FindControl("editddlStatus");

        DataTable dtcenter = new DataTable();
       
        dtcenter = Dblog.Ora_Execute_table("select * from Ref_hr_type_klm where typeklm_cd='" + txtEditcode.Text + "' AND Id != '" + Id.Text + "'");
        if (dtcenter.Rows.Count == 0)
        {
            conn.Open();

            string cmdstr = "update Ref_hr_type_klm set typeklm_desc=@typeklm_desc,typeklm_cd=@typeklm_cd,typeklm_weight=@typeklm_weight,Status=@Status where Id=@Id";

            SqlCommand cmd = new SqlCommand(cmdstr, conn);
            cmd.Parameters.AddWithValue("@typeklm_desc", txtEditcode.Text );
            cmd.Parameters.AddWithValue("@typeklm_cd", lblEditName.Text);
            cmd.Parameters.AddWithValue("@typeklm_weight", Convert.ToDecimal(txtEditwgt.Text));
            cmd.Parameters.AddWithValue("@Status", editddlStatus.SelectedValue);
            cmd.Parameters.AddWithValue("@Id", Id.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            gv_refdata.EditIndex = -1;
            sel_val();
            service.audit_trail("P0214", "Kemaskini", "KLM", txtEditcode.Text);
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