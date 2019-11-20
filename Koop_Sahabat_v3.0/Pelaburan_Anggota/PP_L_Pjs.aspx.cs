﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.ComponentModel;
using System.Text;
using System.Net;
using System.Data.Common;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Net.Mail;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Reporting.WebForms;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Web.SessionState;
using System.Xml;

public partial class PP_L_Pjs : System.Web.UI.Page
{
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string str;
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    SqlCommand com;
    DataTable dt = new DataTable();
    string Status = string.Empty;
    string level, userid;
    string cc_no = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.btnShow);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                level = Session["level"].ToString();
                userid = Session["New"].ToString();
                txtname.Attributes.Add("Readonly", "Readonly");
                txtwil.Attributes.Add("Readonly", "Readonly");
                txtjumla.Attributes.Add("Readonly", "Readonly");
                txtcaw.Attributes.Add("Readonly", "Readonly");
                txttempoh.Attributes.Add("Readonly", "Readonly");
                txtamaun.Attributes.Add("Readonly", "Readonly");
                txttemp.Attributes.Add("Readonly", "Readonly");
                TextBox2.Attributes.Add("Readonly", "Readonly");
                TextBox3.Attributes.Add("Readonly", "Readonly");
                TextBox4.Attributes.Add("Readonly", "Readonly");
                TextBox5.Attributes.Add("Readonly", "Readonly");
                Textarea1.Attributes.Add("Readonly", "Readonly");
                var samp = Request.Url.Query;
                if (samp != "")
                {
                    Applcn_no.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    srch();
                    Button7.Visible = false;
                }
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }

    }


    void srch()
    {
        if (Applcn_no.Text != "")
        {

            DataTable ddicno = new DataTable();
            ddicno = DBCon.Ora_Execute_table("select distinct app_applcn_no from jpa_application JA where JA.app_sts_cd='Y' and '" + Applcn_no.Text + "' IN(JA.app_applcn_no, JA.app_new_icno)");

            if (ddicno.Rows.Count != 0)
            {
                DataTable select_app = new DataTable();
                //select_app = DBCon.Ora_Execute_table("select * from (select JA.app_applcn_no,JA.app_new_icno,JA.app_name,RW.Wilayah_Name,ISNULL(RB.branch_desc, '') AS branch_desc,JA.app_loan_amt,JA.appl_loan_dur,JA.app_permnt_address,ja.app_permnt_postcode,ja.app_permnt_state_cd,ja.app_phone_h,ja.app_phone_m,ja.app_phone_o,ja.app_mailing_address,JA.app_mailing_postcode,ja.app_mailing_state_cd,ISNULL(JA.app_cumm_installment_amt,'') as app_cumm_installment_amt,ISNULL(JA.app_cumm_pay_amt,'') as app_cumm_pay_amt,ISNULL(JA.app_backdated_amt,'') as app_backdated_amt,ISNULL(JA.app_cumm_profit_amt,'') as app_cumm_profit_amt,ISNULL(JA.app_cumm_saving_amt,'') as app_cumm_saving_amt,ISNULL(JA.app_bal_loan_amt,'') as app_bal_loan_amt from jpa_application as JA  Left Join jpa_calculate_fee as JC ON JC.cal_applcn_no=JA.app_applcn_no Left Join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd Left join ref_branch AS RB ON RB.branch_cd=JA.app_branch_cd where JA.app_applcn_no = '" + ddicno.Rows[0]["app_applcn_no"] + "') as a full outer join (select * from jpa_write_off  where wri_applcn_no='" + ddicno.Rows[0]["app_applcn_no"] + "') as b on a.app_applcn_no=b.wri_applcn_no");
                select_app = DBCon.Ora_Execute_table("select JA.app_applcn_no,JA.app_new_icno,JA.app_name,RW.Wilayah_Name,ISNULL(RB.branch_desc, '') AS branch_desc,JA.app_loan_amt,JA.appl_loan_dur,JA.app_permnt_address,ja.app_permnt_postcode,ja.app_permnt_state_cd,ja.app_phone_h,ja.app_phone_m,ja.app_phone_o,ja.app_mailing_address,JA.app_mailing_postcode,ja.app_mailing_state_cd,ISNULL(JA.app_cumm_installment_amt,'') as app_cumm_installment_amt,ISNULL(JA.app_cumm_pay_amt,'') as app_cumm_pay_amt,ISNULL(JA.app_backdated_amt,'') as app_backdated_amt,ISNULL(JA.app_cumm_profit_amt,'') as app_cumm_profit_amt,ISNULL(JA.app_cumm_saving_amt,'') as app_cumm_saving_amt,ISNULL(JA.app_bal_loan_amt,'') as app_bal_loan_amt,ISNULL(jp.apj_apply_sts_cd,'') as apj_apply_sts_cd,ISNULL(jp.apj_apply_remark,'') as apj_apply_remark,ISNULL(jp.apj_approve_sts_cd,'') as apj_approve_sts_cd,ISNULL(jp.apj_approve_remark,'') as apj_approve_remark  from jpa_application as JA  Left Join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd Left join ref_branch AS RB ON RB.branch_cd=JA.app_branch_cd left join jpa_pjs_application as jp on jp.apj_applcn_no=ja.app_applcn_no where ja.app_applcn_no='" + ddicno.Rows[0]["app_applcn_no"] + "'");

                txtname.Text = select_app.Rows[0]["app_name"].ToString();
                txtwil.Text = select_app.Rows[0]["Wilayah_Name"].ToString();
                decimal amt1 = (decimal)select_app.Rows[0]["app_loan_amt"];
                txtjumla.Text = amt1.ToString("C").Replace("$", "").Replace("RM", "");
                txtcaw.Text = select_app.Rows[0]["branch_desc"].ToString();
                txttempoh.Text = select_app.Rows[0]["appl_loan_dur"].ToString();

                decimal a1 = (decimal)select_app.Rows[0]["app_cumm_installment_amt"];
                txtamaun.Text = a1.ToString("C").Replace("$", "").Replace("RM", "");
                decimal a2 = (decimal)select_app.Rows[0]["app_cumm_pay_amt"];
                txttemp.Text = a2.ToString("C").Replace("$", "").Replace("RM", "");
                decimal a3 = (decimal)select_app.Rows[0]["app_backdated_amt"];
                TextBox2.Text = a3.ToString("C").Replace("$", "").Replace("RM", "");
                decimal a4 = (decimal)select_app.Rows[0]["app_cumm_profit_amt"];
                TextBox3.Text = a4.ToString("C").Replace("$", "").Replace("RM", "");
                decimal a5 = (decimal)select_app.Rows[0]["app_cumm_saving_amt"];
                TextBox4.Text = a5.ToString("C").Replace("$", "").Replace("RM", "");
                decimal a6 = (decimal)select_app.Rows[0]["app_bal_loan_amt"];
                TextBox5.Text = a6.ToString("C").Replace("$", "").Replace("RM", "");

                DropDownList1.SelectedValue = select_app.Rows[0]["apj_apply_sts_cd"].ToString();
                Textarea1.Value = select_app.Rows[0]["apj_apply_remark"].ToString();

                k_status.SelectedValue = select_app.Rows[0]["apj_approve_sts_cd"].ToString().Trim();
                catatan.Value = select_app.Rows[0]["apj_approve_remark"].ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Ic OR NO KP Baru',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }

    }
    //protected void btnsrch_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        srch();
    //    }
    //    catch (Exception ex)
    //    {

    //        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Semak Isu',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

    //    }

    //}

    protected void btn_click(object sender, EventArgs e)
    {
        try
        {
            if (Applcn_no.Text != "")
            {
                if (k_status.SelectedValue != "" && catatan.Value != "")
                {

                    var ic_count = Applcn_no.Text.Length;
                    DataTable app_icno = new DataTable();
                    app_icno = DBCon.Ora_Execute_table("select app_applcn_no from jpa_application JA where JA.app_new_icno= '" + Applcn_no.Text + "'");
                    if (ic_count == 12)
                    {
                        cc_no = app_icno.Rows[0]["app_applcn_no"].ToString();
                    }
                    else
                    {
                        cc_no = Applcn_no.Text;
                    }
                    int res_value;
                    if (k_status.SelectedValue == "A")
                    {
                        res_value = 1;
                    }
                    else
                    {
                        res_value = 0;
                    }
                    DataTable ddokdicno = new DataTable();
                    ddokdicno = Dblog.Ora_Execute_table("SELECT * FROM jpa_pjs_application where apj_applcn_no='" + cc_no + "'");
                    if (ddokdicno.Rows.Count > 0)
                    {
                        if (k_status.SelectedValue == "A")
                        {
                            Dblog.Execute_CommamdText("UPDATE jpa_application set app_curr_temp_jbb_ind='' where app_applcn_no='" + cc_no + "'");
                        }
                        Dblog.Execute_CommamdText("Update jpa_pjs_application SET apj_approve_sts_cd='" + k_status.SelectedValue + "',apj_approve_remark='" + catatan.Value + "',apj_upd_id='" + Session["New"].ToString() + "',apj_upd_dt='" + DateTime.Now + "',set_flag='" + res_value + "' WHERE apj_applcn_no='" + cc_no + "'");

                    }

                    Session["validate_success"] = "SUCCESS";
                    Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                    Response.Redirect("../Pelaburan_Anggota/PP_L_Pjs_view.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Red Mark Field Should be Mandatory',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Sila Masukkan Input Carian',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Issue',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }

    }

    protected void clk_Bercagar(object sender, EventArgs e)
    {
        if (Applcn_no.Text != "")
        {
            var ic_count = Applcn_no.Text.Length;
            DataTable app_icno = new DataTable();
            app_icno = DBCon.Ora_Execute_table("select app_applcn_no from jpa_application JA where JA.app_new_icno= '" + Applcn_no.Text + "'");
            if (ic_count == 12)
            {
                cc_no = app_icno.Rows[0]["app_applcn_no"].ToString();
            }
            else
            {
                cc_no = Applcn_no.Text;
            }
            Response.Redirect("../Pelaburan_Anggota/PP_Guaman_cagaran.aspx?spi_icno=" + cc_no);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila mesti Masukkan Kata-kata',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }

    }

    protected void clk_Penjamin(object sender, EventArgs e)
    {
        if (Applcn_no.Text != "")
        {
            var ic_count = Applcn_no.Text.Length;
            DataTable app_icno = new DataTable();
            app_icno = DBCon.Ora_Execute_table("select app_applcn_no from jpa_application JA where JA.app_new_icno= '" + Applcn_no.Text + "'");
            if (ic_count == 12)
            {
                cc_no = app_icno.Rows[0]["app_applcn_no"].ToString();
            }
            else
            {
                cc_no = Applcn_no.Text;
            }
            Response.Redirect("../Pelaburan_Anggota/PP_Guaman_penjamin.aspx?spi_icno=" + cc_no);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila mesti Masukkan Kata-kata',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }

    }

    protected void click_jbb(object sender, EventArgs e)
    {

        try
        {
            if (Applcn_no.Text != "")
            {
                string url = "../Pelaburan_Anggota/semak_jbb.aspx?jbb_icno=" + Applcn_no.Text;

                string s = "window.open('" + url + "', 'popup_window', 'width=11000,height=700,left=100,top=100,resizable=yes');";

                ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Ic OR NO KP Baru',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void btn_rstclick(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void btn_rstclick1(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void clk_bak(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Pelaburan_Anggota/PP_L_Pjs_view.aspx");
    }
}