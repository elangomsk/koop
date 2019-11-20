﻿using System;
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
using System.Data.OleDb;
using System.IO;
using System.Net;

public partial class Ast_tindakan_aset : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection dbcon = new DBConnection();
    StudentWebService service = new StudentWebService();

    string useid = string.Empty;
    string Status = string.Empty;
    string oid = string.Empty;
    string sdd = string.Empty;
    string qr1 = string.Empty;
    string cde = string.Empty, qry1 = string.Empty;
    string CommandArgument1 = string.Empty, CommandArgument2 = string.Empty, CommandArgument3 = string.Empty, CommandArgument4 = string.Empty, CommandArgument5 = string.Empty, CommandArgument6 = string.Empty, CommandArgument7 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2()})";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;

                pg_load();

              
                TextBox1.Attributes.Add("Readonly", "Readonly");
                TextBox5.Text = DateTime.Now.ToString("hh:mm:ss");
                if (samp != "")
                {
                    lbl_name.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    view_details();
                    get_id.Text = "1";

                }
                else
                {
                    get_id.Text = "0";
                }
                useid = Session["New"].ToString();
               
             
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

   

    void view_details()
    {
        try
        {
            DataTable ddicno_astid = new DataTable();
            ddicno_astid = dbcon.Ora_Execute_table("select ss.sas_asset_cat_cd,ss.sas_asset_sub_cat_cd,ss.sas_asset_type_cd,ss.sas_asset_id,ss.sas_asset_cd,lst_cmplnt_id,lst_action_ind,ss.sas_curr_price_amt,FORMAT(ss.sas_reg_dt,'dd/MM/yyyy', 'en-us') as sas_reg_dt,UPPER(sp.stf_name) as stf_name,FORMAT(lst_cmplnt_dt,'dd/MM/yyyy', 'en-us') as lst_cmplnt_dt,FORMAT(lst_incident_dt,'dd/MM/yyyy', 'en-us') as lst_incident_dt,lst_spv_id,lst_incident_time,lst_incident_loc,lst_police_rpt_no,FORMAT(lst_police_rpt_dt,'dd/MM/yyyy', 'en-us') as lst_police_rpt_dt,lst_remark,lst_suspect,lst_spv_sts_cd,lst_spv_remark,lst_police_rpt_doc,FORMAT(lst_spv_dt,'dd/MM/yyyy', 'en-us') as lst_spv_dt,lst_fine_remark,lst_fine_amt from ast_lost left join hr_staff_profile as sp on sp.stf_staff_no=lst_spv_id left join ast_staff_asset as ss on ss.sas_staff_no=lst_staff_no and ss.sas_asset_id=lst_asset_id where lst_cmplnt_id='" + lbl_name.Text + "'");
            if (ddicno_astid.Rows.Count != 0)
            {
                Button4.Text = "Kemaskini";
                TextBox1.Text = ddicno_astid.Rows[0]["lst_cmplnt_id"].ToString();
                TextBox2.Text = ddicno_astid.Rows[0]["lst_cmplnt_dt"].ToString();
                TextBox3.Text = ddicno_astid.Rows[0]["lst_incident_dt"].ToString();
                TextBox5.Text = ddicno_astid.Rows[0]["lst_incident_time"].ToString();
                TextBox4.Text = ddicno_astid.Rows[0]["lst_incident_loc"].ToString();
                TextBox6.Text = ddicno_astid.Rows[0]["lst_police_rpt_no"].ToString();
                txt_ar1.Value = ddicno_astid.Rows[0]["lst_remark"].ToString();
                TextBox8.Text = ddicno_astid.Rows[0]["lst_police_rpt_dt"].ToString();
                TextBox7.Text = ddicno_astid.Rows[0]["lst_suspect"].ToString();
                TextBox13.Text = ddicno_astid.Rows[0]["lst_police_rpt_doc"].ToString();
                TextBox13.Text = ddicno_astid.Rows[0]["lst_police_rpt_doc"].ToString();

                dd_list.SelectedValue = ddicno_astid.Rows[0]["lst_spv_sts_cd"].ToString();
                Textarea1.Value = ddicno_astid.Rows[0]["lst_spv_remark"].ToString();
                TextBox10.Text = ddicno_astid.Rows[0]["lst_spv_dt"].ToString();
                TextBox9.Text = ddicno_astid.Rows[0]["stf_name"].ToString();
                if (ddicno_astid.Rows[0]["sas_curr_price_amt"].ToString() != "")
                {
                    double samt1 = double.Parse(ddicno_astid.Rows[0]["sas_curr_price_amt"].ToString());
                    TextBox11.Text = samt1.ToString("C").Replace("$", "").Replace("RM", "");
                }
                else
                {
                    TextBox11.Text = "0.00";
                }
                if (ddicno_astid.Rows[0]["sas_reg_dt"].ToString() != "")
                {
                    DateTime sst = DateTime.ParseExact(ddicno_astid.Rows[0]["sas_reg_dt"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    int age = (Int32.Parse(DateTime.Today.ToString("yyyyMMdd")) - Int32.Parse(sst.ToString("yyyyMMdd"))) / 10000;
                    TextBox15.Text = Convert.ToString(age);
                }
                else
                {
                    TextBox15.Text = "0";

                }
                Textarea2.Value = ddicno_astid.Rows[0]["lst_fine_remark"].ToString();
                if (ddicno_astid.Rows[0]["lst_fine_amt"].ToString() != "")
                {
                    double samt2 = double.Parse(ddicno_astid.Rows[0]["lst_fine_amt"].ToString());
                    TextBox17.Text = samt2.ToString("C").Replace("$", "").Replace("RM", "");
                }
                else
                {
                    TextBox17.Text = "0.00";
                }

                if (ddicno_astid.Rows[0]["lst_action_ind"].ToString() == "0")
                {
                    RB31.Checked = true;
                }
                else if (ddicno_astid.Rows[0]["lst_action_ind"].ToString() == "1")
                {
                    RB32.Checked = true;
                }
                else
                {
                    RB31.Checked = false;
                    RB32.Checked = false;
                }

                string tn = string.Empty, cn = string.Empty, cn1 = string.Empty;
                if (ddicno_astid.Rows[0]["sas_asset_cat_cd"].ToString().Trim() == "01")
                {
                    tn = "ast_component";
                    cn = "com";
                    cn1 = "and com_asset_id='" + ddicno_astid.Rows[0]["sas_asset_id"].ToString().Trim() + "'";
                }
                else if (ddicno_astid.Rows[0]["sas_asset_cat_cd"].ToString().Trim() == "02")
                {
                    tn = "ast_car";
                    cn = "car";
                    cn1 = "and car_asset_id='" + ddicno_astid.Rows[0]["sas_asset_id"].ToString().Trim() + "'";
                }

                if (ddicno_astid.Rows[0]["sas_asset_cat_cd"].ToString().Trim() == "01" || ddicno_astid.Rows[0]["sas_asset_cat_cd"].ToString().Trim() == "02")
                {
                    DataTable dd_cdsds = new DataTable();
                    dd_cdsds = dbcon.Ora_Execute_table("select " + cn + "_price_amt as v1,FORMAT(" + cn + "_buy_dt,'dd/MM/yyyy', 'en-us') as v2 from " + tn + " where " + cn + "_asset_cat_cd='" + ddicno_astid.Rows[0]["sas_asset_cat_cd"].ToString() + "' and " + cn + "_asset_sub_cat_cd='" + ddicno_astid.Rows[0]["sas_asset_sub_cat_cd"].ToString() + "' and " + cn + "_asset_type_cd='" + ddicno_astid.Rows[0]["sas_asset_type_cd"].ToString() + "' and " + cn + "_asset_cd='" + ddicno_astid.Rows[0]["sas_asset_cd"].ToString() + "' " + cn1 + "");

                    decimal amtm1 = decimal.Parse(dd_cdsds.Rows[0]["v1"].ToString());
                    TextBox26.Text = amtm1.ToString("C").Replace("$", "").Replace("RM", "");
                    TextBox14.Text = dd_cdsds.Rows[0]["v2"].ToString();
                }
            }
        }
        catch
        {

        }
    }
    void pg_load()
    {
       
    }

   

    
    protected void insert_values(object sender, EventArgs e)
    {

        if (TextBox1.Text != "" && TextBox26.Text != "" && TextBox17.Text != "" && Textarea2.Value != "")
        {
            string ss1 = string.Empty;
            if (RB31.Checked == true)
            {
                ss1 = "0";
            }
            else if (RB32.Checked == true)
            {
                ss1 = "1";
            }
            // insrt Query
            string Inssql1 = "UPDATE ast_lost set lst_purchase_amt='" + TextBox26.Text + "',lst_action_ind='" + ss1 + "',lst_fine_amt='" + TextBox17.Text + "',lst_fine_remark='" + Textarea2.Value + "',lst_upd_id='" + Session["New"].ToString() + "',lst_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where lst_cmplnt_id='" + lbl_name.Text + "'";
            Status = dbcon.Ora_Execute_CommamdText(Inssql1);
            if (Status == "SUCCESS")
            {
                Session["validate_success"] = "SUCCESS";
                Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                Response.Redirect("../Aset/Ast_tindakan_aset_view.aspx");
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Medan Input Adalah Mandatori.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }



    protected void DownloadFile(object sender, EventArgs e)
    {
        if (TextBox13.Text != "")
        {
            string filePath = Server.MapPath("~/FILES/AST_polis/" + TextBox13.Text);

            string[] commandArgs = TextBox13.Text.ToString().Split(new char[] { '.' });

            string val1 = commandArgs[0];
            string val2 = commandArgs[1];
            string ext = string.Empty;
            if (val2 == "pdf")
            {
                ext = "Application/pdf";
            }
            else if (val2 == "doc" || val2 == "docx" || val2 == "dotx" || val2 == "dot" || val2 == "dotm")
            {
                ext = "Application/msword";
            }
            else if (val2 == "txt" || val2 == "rtf")
            {
                ext = "Text/Plain";
            }
            //Response.ContentType = "application/doc";
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            //Response.WriteFile(filePath);
            //Response.End();

            byte[] bytesPDF = System.IO.File.ReadAllBytes(@"" + filePath + "");

            if (bytesPDF != null)
            {

                Response.AddHeader("content-disposition", "attachment;filename= " + TextBox13.Text + "");
                Response.ContentType = "" + ext + "";
                Response.BinaryWrite(bytesPDF);
                Response.End();
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Fail Tidak Turun.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    protected void RB1_CheckedChanged(object sender, EventArgs e)
    {
        if (RB31.Checked == true)
        {
            RB32.Checked = false;
        }
    }

    protected void RB2_CheckedChanged(object sender, EventArgs e)
    {
        if (RB32.Checked == true)
        {
            RB31.Checked = false;
        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_tindakan_aset.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_tindakan_aset_view.aspx");
    }

    
}