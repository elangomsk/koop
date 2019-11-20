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

public partial class HR_Ref_table_mgmt_kod : System.Web.UI.Page
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
    string tn1 = string.Empty, tc1 = string.Empty, tc2 = string.Empty, tc3 = string.Empty, tc4 = string.Empty, tc5 = string.Empty, tc6 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
                sel_rt.SelectedValue = "01";
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
            lbl1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
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
            tn1 = "Ref_hr_gelaran";
            tc1 = "hr_gelaran_Code";
            tc2 = "hr_gelaran_desc";
            tc3 = "Status";
            tc4 = "Id";
        }
        else if (sel_rt.SelectedValue == "02")
        {
            tn1 = "Ref_hr_gred";
            tc1 = "hr_gred_Code";
            tc2 = "hr_gred_desc";
            tc3 = "Status";
            tc4 = "Id";
        }
        else if (sel_rt.SelectedValue == "03")
        {
            tn1 = "Ref_hr_jabatan";
            tc1 = "hr_jaba_Code";
            tc2 = "hr_jaba_desc";
            tc3 = "Status";
            tc4 = "Id";
        }
        else if (sel_rt.SelectedValue == "04")
        {
            tn1 = "Ref_hr_Jad_kerj";
            tc1 = "hr_jad_Code";
            tc2 = "hr_jad_desc";
            tc3 = "Status";
            tc4 = "Id";
        }
        //else if (sel_rt.SelectedValue == "05")
        //{
        //    tn1 = "Ref_hr_jantina";
        //    tc1 = "hr_jantina_Code";
        //    tc2 = "hr_jantina_desc";
        //    tc3 = "Status";
        //    tc4 = "Id";
        //}
        else if (sel_rt.SelectedValue == "06")
        {
            tn1 = "Ref_hr_Jawatan";
            tc1 = "hr_jaw_Code";
            tc2 = "hr_jaw_desc";
            tc3 = "Status";
            tc4 = "Id";
        }
        else if (sel_rt.SelectedValue == "07")
        {
            tn1 = "Ref_hr_jenis_elaun";
            tc1 = "hr_elau_Code";
            tc2 = "hr_elau_desc";
            tc3 = "Status";
            tc4 = "Id";
            //tc5 = "elau_kod_akaun";
            //tc6 = "elau_jenis_akaun";
        }
        else if (sel_rt.SelectedValue == "08")
        {
            tn1 = "Ref_hr_jenis_latihan";
            tc1 = "jen_latihan_cd";
            tc2 = "jen_latihan_desc";
            tc3 = "Status";
            tc4 = "Id";
        }
        else if (sel_rt.SelectedValue == "09")
        {
            tn1 = "Ref_hr_kat_latihan";
            tc1 = "kat_latihan_cd";
            tc2 = "kat_latihan_desc";
            tc3 = "Status";
            tc4 = "Id";
        }
        //else if (sel_rt.SelectedValue == "10")
        //{
        //    tn1 = "Ref_ast_Jenis_Pegangan";
        //    tc1 = "ast_Pegangan_code";
        //    tc2 = "ast_Pegangan_desc";
        //    tc3 = "Status";
        //    tc4 = "Id";
        //}
        else if (sel_rt.SelectedValue == "11")
        {
            tn1 = "Ref_hr_leave_sts";
            tc1 = "hr_leave_Code";
            tc2 = "hr_leave_desc";
            tc3 = "Status";
            tc4 = "Id";
        }
        else if (sel_rt.SelectedValue == "12")
        {
            tn1 = "Ref_hr_penamatan";
            tc1 = "hr_pen_Code";
            tc2 = "hr_pen_desc";
            tc3 = "Status";
            tc4 = "Id";
        }
        else if (sel_rt.SelectedValue == "13")
        {
            tn1 = "Ref_hr_pendidikan";
            tc1 = "hr_pendi_Code";
            tc2 = "hr_pendi_desc";
            tc3 = "Status";
            tc4 = "Id";
        }
        else if (sel_rt.SelectedValue == "14")
        {
            tn1 = "Ref_hr_Pengesahan_sts";
            tc1 = "hr_Pengesahan_Code";
            tc2 = "hr_Pengesahan_desc";
            tc3 = "Status";
            tc4 = "Id";
        }
        else if (sel_rt.SelectedValue == "15")
        {
            tn1 = "Ref_hr_penj_kategori";
            tc1 = "hr_kate_Code";
            tc2 = "hr_kate_desc";
            tc3 = "Status";
            tc4 = "Id";
        }

        else if (sel_rt.SelectedValue == "16")
        {
            tn1 = "Ref_hr_penj_traf";
            tc1 = "hr_traf_Code";
            tc2 = "hr_traf_desc";
            tc3 = "Status";
            tc4 = "Id";
        }

        else if (sel_rt.SelectedValue == "17")
        {
            tn1 = "Ref_hr_per_kaki";
            tc1 = "hr_perkaki_Code";
            tc2 = "hr_perkaki_desc";
            tc3 = "Status";
            tc4 = "Id";
        }
        else if (sel_rt.SelectedValue == "18")
        {
            tn1 = "Ref_hr_potongan";
            tc1 = "hr_poto_Code";
            tc2 = "hr_poto_desc";
            tc3 = "Status";
            tc4 = "Id";
            //tc5 = "elau_kod_akaun";
            //tc6 = "elau_jenis_akaun";
        }
        else if (sel_rt.SelectedValue == "19")
        {
            tn1 = "Ref_hr_sebab";
            tc1 = "seb_cd";
            tc2 = "seb_desc";
            tc3 = "Status";
            tc4 = "Id";
        }
        else if (sel_rt.SelectedValue == "20")
        {
            tn1 = "Ref_hr_skim";
            tc1 = "hr_skim_Code";
            tc2 = "hr_skim_desc";
            tc3 = "Status";
            tc4 = "Id";
        }
        else if (sel_rt.SelectedValue == "21")
        {
            tn1 = "Ref_hr_sts_perkha";
            tc1 = "hr_perkha_Code";
            tc2 = "hr_perkha_desc";
            tc3 = "Status";
            tc4 = "Id";
        }
        else if (sel_rt.SelectedValue == "22")
        {
            tn1 = "Ref_hr_title";
            tc1 = "hr_titl_Code";
            tc2 = "hr_titl_desc";
            tc3 = "Status";
            tc4 = "Id";
        }
        //else if (sel_rt.SelectedValue == "23")
        //{
        //    tn1 = "Ref_hr_tuntutan";
        //    tc1 = "hr_tun_Code";
        //    tc2 = "hr_tun_desc";
        //    tc3 = "Status";
        //    tc4 = "Id";
        //    tc5 = "elau_kod_akaun";
        //    tc6 = "elau_jenis_akaun";
        //}
        else if (sel_rt.SelectedValue == "24")
        {
            tn1 = "Ref_hr_type_klm";
            tc1 = "typeklm_cd";
            tc2 = "typeklm_desc";
            tc3 = "Status";
            tc4 = "Id";
        }
        else if (sel_rt.SelectedValue == "25")
        {
            tn1 = "Ref_hr_wargan";
            tc1 = "hr_wargan_Code";
            tc2 = "hr_wargan_desc";
            tc3 = "Status";
            tc4 = "Id";
        }

        else if (sel_rt.SelectedValue == "26")
        {
            tn1 = "Ref_hr_jenis_cuti";
            tc1 = "hr_jenis_Code";
            tc2 = "hr_jenis_desc";
            tc3 = "Status";
            tc4 = "Id";
        }
        else if (sel_rt.SelectedValue == "27")
        {
            tn1 = "Ref_hr_universiti";
            tc1 = "hr_univ_Code";
            tc2 = "hr_univ_desc";
            tc3 = "Status";
            tc4 = "Id";
        }
        else if (sel_rt.SelectedValue == "28")
        {
            tn1 = "Ref_hr_tunggakan";
            tc1 = "hr_tung_Code";
            tc2 = "hr_tung_desc";
            tc3 = "Status";
            tc4 = "Id";
        }
    }

    void sel_val()
    {
        ref_table();
        BindData();
    }


    protected void gv_refdata_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{

        //    if ((e.Row.RowState & DataControlRowState.Edit) > 0)
        //    {
        //        DropDownList ddList = (DropDownList)e.Row.FindControl("lbleditkodID");

        //        //return DataTable havinf department data
        //        DataTable dt = load_department();

        //        ddList.DataSource = dt;
        //        ddList.DataTextField = "nama_akaun";
        //        ddList.DataValueField = "kod_akaun";
        //        ddList.DataBind();
        //        ddList.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        //        DataRowView dr = e.Row.DataItem as DataRowView;
        //        ddList.SelectedValue = dr["c6"].ToString();

        //    }

        //}

        //if (e.Row.RowType == DataControlRowType.Footer)
        //{

        //    DropDownList lbladdwilayah = (DropDownList)e.Row.FindControl("lbladdkodID");

        //    DataTable dt = load_department();

        //    lbladdwilayah.DataSource = dt;

        //    lbladdwilayah.DataTextField = "nama_akaun";

        //    lbladdwilayah.DataValueField = "kod_akaun";

        //    lbladdwilayah.DataBind();

        //    lbladdwilayah.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        //    conn.Close();

        //}
    }

    public DataTable load_department()
    {
        DataTable dt = new DataTable();
        SqlConnection sqlcon = new SqlConnection(cs);
        sqlcon.Open();
        string sql = "select kod_akaun,(kod_akaun +' | ' + nama_akaun) as nama_akaun from KW_Ref_Carta_Akaun where Status='A'";
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
        if (sel_rt.SelectedValue != "")
        {
            qry1 = "Select " + tc1 + " as c1," + tc2 + " as c2,s2." + tc3 + " as c3,s2." + tc4 + " as c4 from " + tn1 + " s2  ORDER BY " + tc1 + " ASC";
        }
        else
        {
            qry1 = "select kavasan_name as c1,wilayah_code as c2,wilayah_name as c3,cawangan_code as c4,'' as c5,'' as c6,'' as c7 from ref_cawangan where kawasan_code = '00000'";
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
        System.Web.UI.WebControls.TextBox txtEditcode = (System.Web.UI.WebControls.TextBox)gv_refdata.Rows[e.RowIndex].FindControl("TextBox1");

        ref_table();
        DataTable chk_dd1 = new DataTable();
        if (sel_rt.SelectedValue == "07")
        {
            chk_dd1 = DBCon.Ora_Execute_table("select fxa_allowance_type_cd from hr_fixed_allowance where fxa_allowance_type_cd='"+ cod.Text + "' group by fxa_allowance_type_cd union all select xta_allowance_type_cd from hr_extra_allowance where xta_allowance_type_cd = '" + cod.Text + "' group by xta_allowance_type_cd");
        }
        else if (sel_rt.SelectedValue == "18")
        {
            chk_dd1 = DBCon.Ora_Execute_table("select ded_deduct_type_cd from hr_deduction where ded_deduct_type_cd='" + cod.Text + "' group by ded_deduct_type_cd");
        }
        else if (sel_rt.SelectedValue == "28")
        {
            chk_dd1 = DBCon.Ora_Execute_table("select tun_type_cd from hr_tunggakan where tun_type_cd='" + cod.Text + "' group by tun_type_cd");
        }
        if (chk_dd1.Rows.Count == 0)
        {
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

                string del_jurn = "delete from KW_ref_jurnal_inter where jur_module='M0001' and jur_item='" + sel_rt.SelectedItem.Text + "' and jur_desc_cd='" + cod.Text + "'";
                Status = DBCon.Ora_Execute_CommamdText(del_jurn);

                sel_val();
                service.audit_trail("P0117", "Hapus", "KATEGORI", txtEditcode.Text);
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dipadamkan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('COA HAVE BEEN SET, DELETING THIS INFO IS PROHIBITED.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('DELETING THIS INFO IS PROHIBITED.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
        string script1 = " $(function () {  $(" + gv_refdata.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
    }

    protected void gv_refdata_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("ADD"))
        {
            System.Web.UI.WebControls.TextBox txtAddName = (System.Web.UI.WebControls.TextBox)gv_refdata.FooterRow.FindControl("txtAddName");
            System.Web.UI.WebControls.TextBox txtAddcode = (System.Web.UI.WebControls.TextBox)gv_refdata.FooterRow.FindControl("txtAddcode");
            DropDownList lbladdkodID = (DropDownList)gv_refdata.FooterRow.FindControl("lbladdkodID");
            DropDownList ddlStatus = (DropDownList)gv_refdata.FooterRow.FindControl("ddlStatus");
            if (txtAddName.Text != "" && txtAddcode.Text != "")
            {
                string set_cnt = string.Empty, set_cnt1 = string.Empty, mcnt = string.Empty;
                string sno1 = string.Empty, sno2 = string.Empty, sno3 = string.Empty, cnt_value = string.Empty;
                string sso1 = string.Empty, sso2 = string.Empty, sso3 = string.Empty,entry=string.Empty,pv =string.Empty;
                string chk_role = string.Empty;
                DataTable dtcenter = new DataTable();
                ref_table();
                dtcenter = Dblog.Ora_Execute_table("select * from " + tn1 + " where " + tc2 + "='" + txtAddName.Text + "'");

                if (dtcenter.Rows.Count == 0)
                {

                    string Inssql = "insert into " + tn1 + " (" + tc1 + "," + tc2 + "," + tc3 + ") values('" + txtAddName.Text + "','" + txtAddcode.Text + "','" + ddlStatus.SelectedValue + "')";
                    Status = DBCon.Ora_Execute_CommamdText(Inssql);
                    if (Status == "SUCCESS")
                    {
                        if(sel_rt.SelectedValue == "18" || sel_rt.SelectedValue == "07" || sel_rt.SelectedValue == "28")
                        {
                            if(sel_rt.SelectedValue == "07")
                            {
                                entry = "D";
                                pv = "K";
                            }
                            else if (sel_rt.SelectedValue == "18")
                            {
                                entry = "K";
                                pv = "D";
                            }
                            else if (sel_rt.SelectedValue == "28")
                            {
                                entry = "K";
                                pv = "D";
                            }

                            string Inssql_main = "Insert into KW_ref_jurnal_inter (jur_module,jur_item,jur_desc,jur_desc_cd,crt_id,crt_dt,status,jur_entry_type,jur_pv_type) values ('M0001','" + sel_rt.SelectedItem.Text + "','" + txtAddcode.Text + "','" + txtAddName.Text + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A','"+ entry + "','" + pv + "')";
                            Status = DBCon.Ora_Execute_CommamdText(Inssql_main);
                        }

                        sel_val();
                        service.audit_trail("P0117", "Simpan","KATEGORI", txtAddcode.Text);
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
        DropDownList lbleditkodID = (DropDownList)gv_refdata.Rows[e.RowIndex].FindControl("lbleditkodID");
        DropDownList editddlStatus = (DropDownList)gv_refdata.Rows[e.RowIndex].FindControl("editddlStatus");
        DataTable dtcenter = new DataTable();
        ref_table();
        dtcenter = Dblog.Ora_Execute_table("select * from " + tn1 + " where " + tc2 + "='" + txtEditcode.Text + "' AND " + tc4 + " != '" + Id.Text + "'");
        if (dtcenter.Rows.Count == 0)
        {
            string set_cnt = string.Empty, set_cnt1 = string.Empty, mcnt = string.Empty;
            string sno1 = string.Empty, sno2 = string.Empty, sno3 = string.Empty, cnt_value = string.Empty, entry = string.Empty, pv = string.Empty;
            DataTable cnt_no = new DataTable();

            string Inssql = "update " + tn1 + " set " + tc1 + "='" + lblEditName.Text + "'," + tc2 + "='" + txtEditcode.Text + "'," + tc3 + "='" + editddlStatus.SelectedValue + "' where " + tc4 + "='" + Id.Text + "'";
            Status = DBCon.Ora_Execute_CommamdText(Inssql);
            if (Status == "SUCCESS")
            {
                if (sel_rt.SelectedValue == "18" || sel_rt.SelectedValue == "07" || sel_rt.SelectedValue == "28")
                {
                    if (sel_rt.SelectedValue == "07")
                    {
                        entry = "D";
                        pv = "K";
                    }
                    else if (sel_rt.SelectedValue == "18")
                    {
                        entry = "K";
                        pv = "D";
                    }
                    else if (sel_rt.SelectedValue == "28")
                    {
                        entry = "K";
                        pv = "D";
                    }

                    string Inssql_main = string.Empty;
                    DataTable dd1 = new DataTable();
                    dd1 = DBCon.Ora_Execute_table("select * from KW_ref_jurnal_inter where jur_module='M0001' and jur_item='" + sel_rt.SelectedItem.Text + "' and jur_desc_cd='" + lblEditName.Text + "'");
                    if (dd1.Rows.Count != 0)
                    {
                        Inssql_main = "update KW_ref_jurnal_inter set jur_desc='" + txtEditcode.Text + "',upd_id='" + Session["New"].ToString() + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',status='A',jur_entry_type='"+entry+"',jur_pv_type='"+ pv +"' where  jur_module='M0001' and jur_item='" + sel_rt.SelectedItem.Text + "' and jur_desc_cd='" + lblEditName.Text + "'";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql_main);
                    }
                    else
                    {
                        Inssql_main = "Insert into KW_ref_jurnal_inter (jur_module,jur_item,jur_desc,jur_desc_cd,crt_id,crt_dt,status,jur_entry_type,jur_pv_type) values ('M0001','" + sel_rt.SelectedItem.Text + "','" + txtEditcode.Text + "','" + lblEditName.Text + "','"+ Session["New"].ToString() +"','"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A','" + entry + "','" + pv + "')";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql_main);
                    }
                }
                gv_refdata.EditIndex = -1;
                sel_val();
                service.audit_trail("P0117", "Kemaskini", "KATEGORI", txtEditcode.Text);
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
