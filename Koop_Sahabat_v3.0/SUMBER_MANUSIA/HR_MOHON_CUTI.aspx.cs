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
using System.Threading;
using System.Net;
using System.Net.Mail;

public partial class HR_MOHON_CUTI : System.Web.UI.Page
{
    Mailcoms ObjMail = new Mailcoms();
    SMS ObjSms = new SMS();
    CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    string rucnt = string.Empty;
    DataTable dt = new DataTable();
    private static int PageSize = 20;
    string qry1 = string.Empty, qry2 = string.Empty;
    string level;
    string Status = string.Empty, Status1 = string.Empty;
    string userid;
    string gt_val1 = string.Empty, gt_val2 = string.Empty;
    string chk_sex = string.Empty, strQuery = string.Empty;
    string gt_mnth = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        assgn_roles();

       
        if (!IsPostBack)
        {

            if (Session["New"] != null)
            {

                jeniscuti();
                var samp = Request.Url.Query;
                if (samp != "")
                {
                    Applcn_no1.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    view_details();
                }
                else
                {
                    txt_tkcuti.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txt_hing.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
             

               
                txt_norju.Attributes.Add("readonly", "readonly");
                grid();
                userid = Session["New"].ToString();
                txtsno.Text = userid;

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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('507','448','1971','1788','473','326','1790','1791','1528','1976','1792','61','35','883','15','77')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            //ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            //ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            //ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());
            //ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());     
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
            ps_lbl10.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());       
            //ps_lbl11.Text = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower());
            ps_lbl12.Text = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
            btn_submit.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            btn_kemask.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
           
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    //protected void lnkselect_Click(object sender, EventArgs e)
    //{
    //    if(DD_JENCUTI.SelectedItem.Text!= "CUTI BERSALIN")
    //    {
    //        v2.Visible = false;

    //    }
    //    else
    //    {
    //        v2.Visible = true;
    //    }
            
       
    //}
        void assgn_roles()
    {
        if (Session["New"] != null)
        {
            DataTable ddokdicno = new DataTable();
            ddokdicno = DBCon.Ora_Execute_table("select * from KK_User_Login where KK_userid = '" + Session["userid"].ToString() + "'");

            if (ddokdicno.Rows.Count != 0)
            {
                DataTable ddokdicno_1 = new DataTable();
                ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0059' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

                if (ddokdicno_1.Rows.Count != 0)
                {
                    gt_val1 = ddokdicno_1.Rows[0]["Edit_chk"].ToString();
                }
            }

            DataTable ddicno = new DataTable();
            ddicno = DBCon.Ora_Execute_table("select stf_staff_no,stf_sex_cd From hr_staff_profile where stf_staff_no='" + Session["New"].ToString() + "' ");
            if (ddicno.Rows.Count != 0)
            {
                chk_sex = ddicno.Rows[0]["stf_sex_cd"].ToString();
            }
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    void view_details()
    {
        DataTable ddicno = new DataTable();
        ddicno = DBCon.Ora_Execute_table("select stf_staff_no,stf_sex_cd From hr_staff_profile where stf_staff_no='" + Session["New"].ToString() + "' ");
        if (ddicno.Rows.Count != 0)
        {
            string stffno = ddicno.Rows[0]["stf_staff_no"].ToString();
            
            SqlConnection conn = new SqlConnection(cs);
            string query3 = "select FORMAT(lap_application_dt,'dd/MM/yyyy', 'en-us') as lap_application_dt,lap_leave_cat_cd,lap_reason,FORMAT(lap_leave_start_dt,'dd/MM/yyyy', 'en-us') as lap_leave_start_dt,FORMAT(lap_leave_end_dt,'dd/MM/yyyy', 'en-us') as lap_leave_end_dt,lap_leave_type_cd,ISNULL(lap_cancel_reason,'') as lap_cancel_reason,lap_ref_no,ISNULL(lap_endorse_sts_cd,'') lap_endorse_sts_cd,lap_sesi,lap_doc,lap_approve_sts_cd from hr_leave_application where lap_staff_no='" + stffno + "' and Id='"+ Applcn_no1.Text + "'";
            conn.Open();
            var sqlCommand3 = new SqlCommand(query3, conn);

            var sqlReader3 = sqlCommand3.ExecuteReader();
            while (sqlReader3.Read())
            {                
                if (gt_val1 == "1")
                {
                    if (sqlReader3["lap_approve_sts_cd"].ToString().Trim() == "00")
                    {
                        
                        btn_kemask.Visible = true;
                        Button3.Visible = true;
                        disp_id2.Visible = true;
                    }
                    else
                    {
                        disp_id2.Visible = false;
                        btn_kemask.Visible = false;
                        Button3.Visible = false;
                    }

                }
                else
                {
                    btn_kemask.Visible = false;
                    Button3.Visible = false;
                }
                btn_submit.Visible = false;
                Button1.Visible = false;
                
                TextBox3.Text = sqlReader3["lap_leave_type_cd"].ToString().Trim();
                //disp_id1.Visible = true;
                DD_JENCUTI.SelectedValue = TextBox3.Text;
                load_jenis();
               
                txt_sebab.Text = sqlReader3["lap_reason"].ToString();

                txt_tkcuti.Text = sqlReader3["lap_leave_start_dt"].ToString();
                TextBox2.Text = sqlReader3["lap_leave_start_dt"].ToString();
                txt_hing.Text = sqlReader3["lap_leave_end_dt"].ToString();
                txtsbcuti.Text = sqlReader3["lap_cancel_reason"].ToString();
                TextBox1.Text = sqlReader3["lap_application_dt"].ToString();
                TextBox4.Text = sqlReader3["lap_ref_no"].ToString();
                if (sqlReader3["lap_doc"].ToString() != "")
                {
                    TextBox8.Text = sqlReader3["lap_doc"].ToString();
                    //dfile.Text = (string)sqlReader3["lap_doc"].ToString();
                    lnkDownload.Text = sqlReader3["lap_doc"].ToString();
                    lnkDownload.Visible = true;
                }
                else
                {
                    lnkDownload.Visible = false;
                }
                
            }
            sqlReader3.Close();
        }
        else
        {            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (txtsbcuti.Text != "")
        {
            DataTable ddicno = new DataTable();
            ddicno = DBCon.Ora_Execute_table("select str_curr_org_cd,stf_staff_no From hr_staff_profile where stf_staff_no='" + Session["New"].ToString() + "'");
            if (ddicno.Rows.Count != 0)
            {
                string stffno = ddicno.Rows[0]["stf_staff_no"].ToString();
                string orgid = ddicno.Rows[0]["str_curr_org_cd"].ToString();
                DateTime feedt2 = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string ss1 = feedt2.ToString("yyyy-MM-dd");
                DateTime st_date2 = DateTime.ParseExact(TextBox2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string stdt2 = st_date2.ToString("yyyy-MM-dd");
                dt = DBCon.Ora_Execute_table("select FORMAT(lap_application_dt,'yyyy-MM-dd', 'en-us') as lap_application_dt,lap_leave_type_cd from hr_leave_application where Id= '" + Applcn_no1.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    userid = Session["New"].ToString();
                    DataTable dIns = new DataTable();
                    dIns = DBCon.Ora_Execute_table("update hr_leave_application set lap_cancel_reason ='" + txtsbcuti.Text.Replace("'", "''") + "',lap_cancel_ind = 'Y',lap_upd_id ='" + Session["New"].ToString() + "',lap_upd_dt ='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "',lap_approve_sts_cd='04'  where Id= '" + Applcn_no1.Text + "'");
                    Session["validate_success"] = "SUCCESS";
                    Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                    Response.Redirect("../SUMBER_MANUSIA/HR_MOHON_CUTI_view.aspx");
                }
            }
            else
            {
                grid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {
            grid();
            txtsbcuti.Focus();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Sebab Batal Cuti.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }
    protected void get_pro_rate(object sender, EventArgs e)
    {
        grid();
    }

    void bind_prorate()
    {
        DateTime feedt2 = DateTime.ParseExact(txt_tkcuti.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        gt_mnth = feedt2.ToString("MM");        
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (DD_JENCUTI.SelectedValue != "" && txt_tkcuti.Text != "" && txt_hing.Text != "" && txt_sebab.Text != "")
        {
            DataTable ddicno = new DataTable();
            ddicno = DBCon.Ora_Execute_table("select str_curr_org_cd,stf_staff_no From hr_staff_profile where stf_staff_no='" + Session["New"].ToString() + "'");
            if (ddicno.Rows.Count != 0)
            {
                string stffno = ddicno.Rows[0]["stf_staff_no"].ToString();
                string orgid = ddicno.Rows[0]["str_curr_org_cd"].ToString();
                DateTime feedt2 = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string ss2 = feedt2.ToString("yyyy-MM-dd");
                string fileName = string.Empty;
                DateTime st_date1 = DateTime.ParseExact(TextBox2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string stdt1 = st_date1.ToString("yyyy-MM-dd");
                if (FileUpload1.FileName != "")
                {
                    fileName = FileUpload1.PostedFile.FileName;
                }
                string fname = string.Empty;
                if (fileName != "")
                {
                    FileUpload1.PostedFile.SaveAs(Server.MapPath("~/FILES/Attendance/" + fileName));//Or code to save in the DataBase.
                    fname = fileName;
                }
                else
                {
                    fname = TextBox8.Text;
                }
                dt = DBCon.Ora_Execute_table("select FORMAT(lap_application_dt,'yyyy-MM-dd', 'en-us') as lap_application_dt,lap_leave_type_cd from hr_leave_application where lap_org_id='" + orgid + "' and lap_leave_type_cd='" + DD_JENCUTI.SelectedValue + "' and lap_application_dt='" + ss2 + "' and lap_staff_no='" + stffno + "' and lap_leave_start_dt='" + stdt1 + "' and lap_ref_no='" + TextBox4.Text + "' ");
                if (dt.Rows.Count > 0)
                {
                    string datedari = txt_tkcuti.Text;
                    DateTime dt1 = DateTime.ParseExact(datedari, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string fromdate = dt1.ToString("yyyy-MM-dd");
                    string datedari1 = txt_hing.Text;
                    DateTime dt2 = DateTime.ParseExact(datedari1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    String todate = dt2.ToString("yyyy-MM-dd");
                    TimeSpan t = dt2 - dt1;
                    var d = t.TotalDays;
                    var days = d + 1;
                    float formID = Convert.ToInt32(days);
                    DateTime cdt1 = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //if (dt1 >= cdt1)
                    //   {

                    DataTable ddicno_levchk = new DataTable();
                    ddicno_levchk = DBCon.Ora_Execute_table("select lev_balance_day,lev_leave_type_cd from hr_leave where lev_staff_no='" + stffno + "' and lev_year='" + DateTime.Now.ToString("yyyy") + "' and lev_leave_type_cd='" + DD_JENCUTI.SelectedValue + "'");
                    if (ddicno_levchk.Rows.Count != 0)
                    {
                        float sv1 = 0;
                        if (ddicno_levchk.Rows[0]["lev_leave_type_cd"].ToString() == "01" || ddicno_levchk.Rows[0]["lev_leave_type_cd"].ToString() == "04" || ddicno_levchk.Rows[0]["lev_leave_type_cd"].ToString() == "05")
                        {
                            if (ddicno_levchk.Rows[0]["lev_leave_type_cd"].ToString() == "01")
                            {
                                double sdt1 = (dt1 - cdt1).TotalDays;
                                if (ddicno_levchk.Rows[0]["lev_leave_type_cd"].ToString() == "01" && sdt1 < 0)
                                {
                                    sv1 = float.Parse(ddicno_levchk.Rows[0]["lev_balance_day"].ToString());
                                    if (sv1 >= formID)
                                    {
                                        if (sv1 != 0)
                                        {
                                            userid = Session["New"].ToString();
                                            DataTable dIns = new DataTable();

                                            dIns = DBCon.Ora_Execute_table("update hr_leave_application set lap_doc='" + fname + "',lap_leave_day='" + formID + "',lap_leave_start_dt ='" + fromdate + "',lap_leave_end_dt = '" + todate + "',lap_reason='" + txt_sebab.Text.Replace("'", "''") + "',lap_upd_id ='" + Session["New"].ToString() + "',lap_upd_dt ='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "'  WHERE lap_org_id='" + orgid + "' and lap_leave_type_cd='" + DD_JENCUTI.SelectedValue + "' and lap_application_dt='" + ss2 + "' and lap_staff_no='" + stffno + "' and lap_ref_no='" + TextBox4.Text + "' ");
                                            service.audit_trail("P0059", "Kemaskini",stffno, TextBox4.Text);
                                            Session["validate_success"] = "SUCCESS";
                                            Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                                            Response.Redirect("../SUMBER_MANUSIA/HR_MOHON_CUTI_view.aspx");
                                        }
                                        else
                                        {
                                            grid();
                                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Telah Wujud Rekod Tidak Dibenarkan Untuk Dikemaskini.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                                        }
                                    }
                                    else
                                    {
                                        grid();
                                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Meninggalkan Tidak Sesuai untuk '" + DD_JENCUTI.SelectedItem.Text + ".',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                                    }
                                }
                                else if (ddicno_levchk.Rows[0]["lev_leave_type_cd"].ToString() == "01" && sdt1 >= 3)
                                {
                                    sv1 = float.Parse(ddicno_levchk.Rows[0]["lev_balance_day"].ToString());
                                    if (sv1 >= formID)
                                    {
                                        if (sv1 != 0)
                                        {
                                            userid = Session["New"].ToString();
                                            DataTable dIns = new DataTable();
                                            dIns = DBCon.Ora_Execute_table("update hr_leave_application set lap_doc='" + fname + "',lap_leave_day='" + formID + "',lap_leave_start_dt ='" + fromdate + "',lap_leave_end_dt = '" + todate + "',lap_reason='" + txt_sebab.Text.Replace("'", "''") + "',lap_upd_id ='" + Session["New"].ToString() + "',lap_upd_dt ='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "'  WHERE lap_org_id='" + orgid + "' and lap_leave_type_cd='" + DD_JENCUTI.SelectedValue + "' and lap_application_dt='" + ss2 + "' and lap_staff_no='" + stffno + "' and lap_ref_no='" + TextBox4.Text + "' ");
                                            service.audit_trail("P0059", "Kemaskini", stffno, TextBox4.Text);
                                            Session["validate_success"] = "SUCCESS";
                                            Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                                            Response.Redirect("../SUMBER_MANUSIA/HR_MOHON_CUTI_view.aspx");
                                        }
                                        else
                                        {
                                            grid();
                                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Telah Wujud Rekod Tidak Dibenarkan Untuk Dikemaskini.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                                        }
                                    }
                                    else
                                    {
                                        grid();
                                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Meninggalkan Tidak Sesuai untuk '" + DD_JENCUTI.SelectedItem.Text + ".',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                                    }
                                }
                                else
                                {
                                    grid();
                                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila pohon 3 hari sebelum.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                                }
                            }
                            else
                            {
                                sv1 = float.Parse(ddicno_levchk.Rows[0]["lev_balance_day"].ToString());
                                if (sv1 >= formID)
                                {
                                    if (sv1 != 0)
                                    {
                                        userid = Session["New"].ToString();
                                        DataTable dIns = new DataTable();
                                        DateTime feedt21 = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                        string ss1 = feedt21.ToString("yyyy-MM-dd");
                                        dIns = DBCon.Ora_Execute_table("update hr_leave_application set lap_doc='" + fname + "',lap_leave_day='" + formID + "',lap_leave_start_dt ='" + fromdate + "',lap_leave_end_dt = '" + todate + "',lap_reason='" + txt_sebab.Text.Replace("'", "''") + "',lap_upd_id ='" + Session["New"].ToString() + "',lap_upd_dt ='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "'  WHERE lap_staff_no ='" + txtsno.Text + "'  AND lap_application_dt ='" + ss1 + "' and lap_leave_type_cd = '" + DD_JENCUTI.SelectedValue + "' and lap_ref_no='" + TextBox4.Text + "' ");
                                        service.audit_trail("P0059", "Kemaskini", txtsno.Text, TextBox4.Text);
                                        Session["validate_success"] = "SUCCESS";
                                        Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                                        Response.Redirect("../SUMBER_MANUSIA/HR_MOHON_CUTI_view.aspx");
                                    }
                                    else
                                    {
                                        grid();
                                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Telah Wujud Rekod Tidak Dibenarkan Untuk Dikemaskini.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                                    }
                                }
                                else
                                {
                                    grid();
                                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Meninggalkan Tidak Sesuai untuk '" + DD_JENCUTI.SelectedItem.Text + ".',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                                }
                            }
                        }
                        else
                        {
                            grid();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('" + DD_JENCUTI.SelectedItem.Text + " Meninggalkan hanya untuk " + sv1 + " hari.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                        }
                    }
                    else
                    {
                        userid = Session["New"].ToString();
                        DataTable dIns = new DataTable();
                        DateTime feedt21 = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        string ss1 = feedt21.ToString("yyyy-MM-dd");
                        dIns = DBCon.Ora_Execute_table("update hr_leave_application set lap_doc='" + fname + "',lap_leave_day='" + formID + "',lap_leave_start_dt ='" + fromdate + "',lap_leave_end_dt = '" + todate + "',lap_reason='" + txt_sebab.Text.Replace("'", "''") + "',lap_upd_id ='" + Session["New"].ToString() + "',lap_upd_dt ='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "'  WHERE lap_staff_no ='" + txtsno.Text + "'  AND lap_application_dt ='" + ss1 + "' and lap_leave_type_cd = '" + DD_JENCUTI.SelectedValue + "' and lap_ref_no='" + TextBox4.Text + "' ");
                        service.audit_trail("P0059", "Kemaskini", txtsno.Text, TextBox4.Text);
                        Session["validate_success"] = "SUCCESS";
                        Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                        Response.Redirect("../SUMBER_MANUSIA/HR_MOHON_CUTI_view.aspx");
                    }
                    //   }
                    //else
                    //{
                    //    bind1();
                    //    bind2();
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Sah Tarikh.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    //}
                }
            }
            else
            {
                grid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {
            grid();
            //txtsbcuti.Focus();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    

    void jeniscuti()
    {
        DataSet Ds = new DataSet();
        try
        {
            string chk1 = string.Empty;
            if(chk_sex.Trim() == "M")
            {
                chk1 = "('16','13','12')";
            }
            else if (chk_sex.Trim() == "F")
            {
                chk1 = "('16','13','09')";
            }
            else
            {
                chk1 = "('16','13')";
            }

            string com = "select hr_jenis_Code,hr_jenis_desc from Ref_hr_jenis_cuti where status='A' and hr_jenis_Code NOT IN "+ chk1 +"  order by hr_jenis_Code";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_JENCUTI.DataSource = dt;
            DD_JENCUTI.DataTextField = "hr_jenis_desc";
            DD_JENCUTI.DataValueField = "hr_jenis_Code";
            DD_JENCUTI.DataBind();
            DD_JENCUTI.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

   
    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        grid();        
    }

  
    void grid()
    {
        bind_prorate();
        con.Open();
        //DataTable ddicno = new DataTable();
        //ddicno = DBCon.Ora_Execute_table("select stf_staff_no From hr_staff_profile where stf_staff_no='" + Session["New"].ToString() + "' ");
        //string stffno = ddicno.Rows[0]["stf_staff_no"].ToString();
        string year = DateTime.Now.ToString("yyyy");

        string chk2 = string.Empty;
        if (chk_sex.Trim() == "M")
        {
            chk2 = "('10','09')";
        }
        else if (chk_sex.Trim() == "F")
        {
            chk2 = "('12','10')";
        }
        else
        {
            chk2 = "('09','10','12')";
        }
        strQuery = "select *,case when a.sno ='01' then convert(float,(a.a + ((a.c/12) * ("+ gt_mnth +")) - d)) else '0' end  as pro_rate from (select a1.lev_staff_no,a1.hr_jenis_desc,case when a1.hr_jenis_desc = 'cuti tahunan' then '01' when a1.hr_jenis_desc = 'cuti sakit' then '02' when a1.hr_jenis_desc = 'cuti ganti' then '04'  "
                    + " when a1.hr_jenis_desc = 'cuti bawa hadapan' then '04' end as sno, a1.a,cast(a1.c as float) as c,a1.b, a1.ab,((ISNULL(b1.e,'0')) + ISNULL(c2.pre_e,'0')) as e,a1.d,"
                    + " ((((convert(float,(a1.c)) + a1.a) - (convert(float,(a1.d)) + ISNULL(b1.e, '0')))) - ISNULL(c2.pre_e, '0')) as res,ISNULL(c2.pre_e, '0') as pre_bal from (select  "
                    + " lev_staff_no,lev_leave_type_cd,hr_jenis_desc,lev_carry_fwd_day as a,lev_entitle_day as c,convert(float,(((convert(float,lev_entitle_day) / 12) *  DATEPART(month, GETDATE())))) as b,"
                    + " (lev_carry_fwd_day + convert(float,(((convert(float,lev_entitle_day)))))) as ab,lev_taken_day as d,lev_balance_day from hr_staff_profile as SP left join hr_leave as LV on LV.lev_staff_no= "
                    + " SP.stf_staff_no and LV.lev_org_id=sp.str_curr_org_cd left join Ref_hr_jenis_cuti as JC on JC.hr_jenis_Code=LV.lev_leave_type_cd where stf_staff_no='" + Session["New"].ToString() + "' AND lev_year='" + year + "') as a1 "
                    + " full outer join(select lap_staff_no, lap_leave_type_cd,sum(convert(float,lap_leave_day)) as e from hr_leave_application where lap_staff_no='" + Session["New"].ToString() + "' and lap_cancel_ind='N' and "
                    + " lap_leave_type_cd IN('01','04') and lap_approve_sts_cd NOT IN ('04','01') and  ISNULL(lap_approve_sts_cd,'') = '00' group by lap_staff_no,lap_leave_type_cd) as b1 on b1.lap_staff_no="
                    + " a1.lev_staff_no and b1.lap_leave_type_cd=a1.lev_leave_type_cd left join(select lap_staff_no,lap_pre_type_cd,sum(convert(float,lap_pre_balance)) as pre_e from  "
                    + " hr_leave_application where lap_staff_no ='" + Session["New"].ToString() + "' and lap_cancel_ind='N' and lap_leave_type_cd IN('04','05') and lap_approve_sts_cd NOT IN ('04','01') and ISNULL(lap_approve_sts_cd,'') = '00' "
                    + "  group by lap_staff_no,lap_pre_type_cd ) as c2 on  c2.lap_staff_no=a1.lev_staff_no and c2.lap_pre_type_cd = a1.lev_leave_type_cd "

                    + "   union all select a1.col_staff_no,a1.hr_jenis_desc,case when a1.hr_jenis_desc = 'cuti tahunan' then '01' when a1.hr_jenis_desc = 'cuti sakit' then '02' when a1.hr_jenis_desc = 'cuti ganti' then '04' "
                    + "  when a1.hr_jenis_desc = 'cuti bawa hadapan' then '04' end as sno, a1.a,cast(a1.c as float) as c,a1.b, a1.ab,((ISNULL(b1.e, '0')) + ISNULL(c2.pre_e, '0')) as e,a1.d, "
                    + "  ((((convert(float, (a1.c)) + a1.a) - (convert(float, (a1.d)) + ISNULL(b1.e, '0')))) - ISNULL(c2.pre_e, '0')) as res,ISNULL(c2.pre_e, '0') as pre_bal from(select "
                    + "  col_staff_no, col_cat_cd, hr_jenis_desc, '0' as a, sum(convert(float,col_entitle_day)) as c, sum(convert(float, (((convert(float, col_entitle_day) / 12) * DATEPART(month, GETDATE()))))) as b,  "
                    + "   sum(('0' + convert(float, (((convert(float, col_entitle_day))))))) as ab, sum(convert(float, col_taken_day)) as d, sum(convert(float, col_balance_day)) col_balance_day from hr_staff_profile as SP left join hr_com_leave as LV on LV.col_staff_no = "
                    + "  SP.stf_staff_no and LV.col_org_id = sp.str_curr_org_cd left join Ref_hr_jenis_cuti as JC on JC.hr_jenis_Code = LV.col_cat_cd where stf_staff_no = '" + Session["New"].ToString() + "' AND col_year = '" + year + "' and col_end_dt >= '"+ DateTime.Now.ToString("yyyy-MM-dd") +"' group by col_staff_no,col_cat_cd,JC.hr_jenis_desc) as a1 "
                    + "  full outer join(select lap_staff_no, lap_leave_type_cd, sum(convert(float, lap_leave_day)) as e from hr_leave_application where lap_staff_no = '" + Session["New"].ToString() + "' and lap_cancel_ind = 'N' and "
                    + "  lap_leave_type_cd IN('12') and lap_approve_sts_cd NOT IN('04', '01') and  ISNULL(lap_approve_sts_cd, '') = '00' group by lap_staff_no, lap_leave_type_cd) as b1 on b1.lap_staff_no = "
                    + "  a1.col_staff_no and b1.lap_leave_type_cd = a1.col_cat_cd left join(select lap_staff_no, lap_pre_type_cd, sum(convert(float, lap_pre_balance)) as pre_e from "
                    + "  hr_leave_application where lap_staff_no = '" + Session["New"].ToString() + "' and lap_cancel_ind = 'N' and lap_leave_type_cd IN('12') and lap_approve_sts_cd NOT IN('04', '01') and ISNULL(lap_approve_sts_cd, '') = '00' "
                    + "  group by lap_staff_no, lap_pre_type_cd ) as c2 on c2.lap_staff_no = a1.col_staff_no and c2.lap_pre_type_cd = a1.col_cat_cd "

                    + "   union all select a.lev_staff_no,a.hr_jenis_desc,case when "
                    + "   a.hr_jenis_desc = 'cuti tahunan' then '01' when a.hr_jenis_desc = 'cuti sakit' then '02' when a.hr_jenis_desc = 'cuti ganti' then '04' when a.hr_jenis_desc = 'cuti bawa hadapan' then '03' "
                    + "   end as sno,a.a,cast(a.c as float) as c,a.b,a.ab,ISNULL(b1.e, '0') as e,ISNULL(b2.e, '0') as d,(a.ab - (ISNULL(b1.e, '0') + ISNULL(b2.e, '0'))) as res,  '0' pre_bal from (select "
                    + "   hsp.stf_staff_no as lev_staff_no, hr_jenis_desc, '0' a, gen_leave_day as c, '' as b, gen_leave_day as ab, '' e, '' d, '' res, s1.hr_jenis_Code from Ref_hr_jenis_cuti s1 inner "
                    + "   join hr_staff_profile as hsp on hsp.stf_staff_no = '" + Session["New"].ToString() + "' inner "
                    + "   join hr_cmn_general_leave as s2 on s2.gen_leave_type_cd = s1.hr_jenis_Code where hr_jenis_Code In('17')) as a full "
                    + "   outer join(select lap_staff_no, lap_leave_type_cd, sum(convert(float, lap_leave_day)) as e from hr_leave_application where lap_staff_no = '" + Session["New"].ToString() + "' and lap_cancel_ind = 'N' and "
                    + "   lap_leave_type_cd IN('17') and lap_approve_sts_cd NOT IN('04', '01') and ISNULL(lap_approve_sts_cd, '') = '00' group by   lap_staff_no, lap_leave_type_cd) as b1 on b1.lap_staff_no = "
                    + "   a.lev_staff_no and b1.lap_leave_type_cd = a.hr_jenis_Code full outer join(select lap_staff_no, lap_leave_type_cd, sum(convert(float, lap_leave_day)) as e from hr_leave_application where "
                    + "   lap_staff_no = '" + Session["New"].ToString() + "' and lap_cancel_ind = 'N' and lap_leave_type_cd IN('12', '17') and lap_approve_sts_cd NOT IN('04', '01') and ISNULL(lap_approve_sts_cd, '') = '01' and lap_cur_sts = 'Y' "
                    + "   group by   lap_staff_no, lap_leave_type_cd) as b2 on b2.lap_staff_no = a.lev_staff_no and b2.lap_leave_type_cd = a.hr_jenis_Code ) as a where a.lev_staff_no != '' order by sno ";


                                                                        //SqlCommand cmd = new SqlCommand("select a1.lev_staff_no,a1.hr_jenis_desc,a1.a,cast(a1.c as float) as c,a1.b, a1.ab,(ISNULL(b1.e,'0') + ISNULL(c1.e,'0')) as e,a1.d,(((convert(float,(a1.c)) + a1.a) - (convert(float,(a1.d)) + ISNULL(b1.e,'0'))) - ISNULL(c1.e,'0')) as res from (select lev_staff_no,lev_leave_type_cd,hr_jenis_desc,lev_carry_fwd_day as a,lev_entitle_day as c,convert(float,(((convert(float,lev_entitle_day) / 12) * DATEPART(month, GETDATE())))) as b,(lev_carry_fwd_day + convert(float,(((convert(float,lev_entitle_day)))))) as ab,lev_taken_day as d,lev_balance_day from hr_staff_profile as SP left join hr_leave as LV on LV.lev_staff_no=SP.stf_staff_no and LV.lev_org_id=sp.str_curr_org_cd left join Ref_hr_jenis_cuti as JC on JC.hr_jenis_Code=LV.lev_leave_type_cd where stf_staff_no='" + Session["New"].ToString() + "' AND lev_year='" + year + "') as a1 full outer join(select lap_staff_no,lap_leave_type_cd,sum(convert(float,lap_leave_day)) as e from hr_leave_application where lap_staff_no='" + Session["New"].ToString() + "' and lap_cancel_ind='N' and lap_leave_type_cd IN('01','04','05') and lap_approve_sts_cd NOT IN ('04','01') and ISNULL(lap_endorse_sts_cd,'') = '' group by lap_staff_no,lap_leave_type_cd) as b1 on b1.lap_staff_no=a1.lev_staff_no and b1.lap_leave_type_cd=a1.lev_leave_type_cd left join(select lap_staff_no,lap_leave_type_cd,sum(convert(float,lap_leave_day)) as e from hr_leave_application where lap_staff_no='" + Session["New"].ToString() + "' and lap_cancel_ind='N' and lap_leave_type_cd IN('03') and lap_approve_sts_cd NOT IN ('04','01') and ISNULL(lap_endorse_sts_cd,'') = '' group by lap_staff_no,lap_leave_type_cd) as c1 on c1.lap_staff_no=a1.lev_staff_no and a1.lev_leave_type_cd='01' union all select a.lev_staff_no,a.hr_jenis_desc,a.a,cast(a.c as float) as c,a.b,a.ab,ISNULL(b1.e,'0') as e,ISNULL(b2.e,'0') as d,(a.ab - (ISNULL(b1.e,'0') + ISNULL(b2.e,'0'))) as res from (select '" + Session["New"].ToString() + "' as lev_staff_no,hr_jenis_desc,'0' a,gen_leave_day as c,'' as b,gen_leave_day as ab,'' e, '' d, '' res,s1.hr_jenis_Code from Ref_hr_jenis_cuti s1 inner join hr_cmn_general_leave as s2 on s2.gen_leave_type_cd=s1.hr_jenis_Code where hr_jenis_Code In ('09','10','12')) as a full outer join(select lap_staff_no,lap_leave_type_cd,sum(convert(float,lap_leave_day)) as e from hr_leave_application where lap_staff_no='"+ Session["New"].ToString() + "' and lap_cancel_ind='N' and lap_leave_type_cd IN('09','10','12') and lap_approve_sts_cd NOT IN ('04','01') and ISNULL(lap_endorse_sts_cd,'') = '' group by lap_staff_no,lap_leave_type_cd) as b1 on b1.lap_staff_no=a.lev_staff_no and b1.lap_leave_type_cd=a.hr_jenis_Code full outer join(select lap_staff_no,lap_leave_type_cd,sum(convert(float,lap_leave_day)) as e from hr_leave_application where lap_staff_no='"+ Session["New"].ToString() + "' and lap_cancel_ind='N' and lap_leave_type_cd IN('09','10','12') and lap_approve_sts_cd NOT IN ('04','01') and ISNULL(lap_endorse_sts_cd,'') = '01' group by lap_staff_no,lap_leave_type_cd) as b2 on b2.lap_staff_no=a.lev_staff_no and b2.lap_leave_type_cd=a.hr_jenis_Code", con);
        SqlCommand cmd = new SqlCommand(strQuery, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView1.DataSource = ds;
            GridView1.DataBind();
            int columncount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView1.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
        }
        else
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        con.Close();
    }

    protected void DownloadFile(object sender, EventArgs e)
    {
        if (TextBox8.Text != "")
        {
            string filePath = Server.MapPath("~/FILES/Attendance/" + TextBox8.Text);
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + Path.GetFileName(filePath) + "\"");
            Response.WriteFile(filePath);
            Response.End();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('No Record Found',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }


    }
    protected void get_jenis_cuti(object sender, EventArgs e)
    {
        load_jenis();
    }

    void load_jenis()
    {

        if (DD_JENCUTI.SelectedValue == "03")
        {
            v2.Visible = false;
            Div1.Visible = true;
            txt_sebab.Text = "";
        }
        else if (DD_JENCUTI.SelectedValue == "12")
        {
            DataTable ddicno = new DataTable();
            ddicno = DBCon.Ora_Execute_table("select str_curr_org_cd,stf_staff_no From hr_staff_profile where stf_staff_no='" + Session["New"].ToString() + "' ");
            DataTable ddicno_chk = new DataTable();
            ddicno_chk = DBCon.Ora_Execute_table("select top(1)*,datename(month, col_start_dt) as Mnth from hr_com_leave where col_end_dt >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' and col_org_id='" + ddicno.Rows[0]["str_curr_org_cd"].ToString() + "' and col_staff_no='" + Session["New"].ToString() + "' and col_year='" + DateTime.Now.ToString("yyyy") + "' and col_cat_cd='12' order by col_start_dt");
            if (ddicno_chk.Rows.Count != 0)
            {
                txt_sebab.Text = DD_JENCUTI.SelectedItem.Text.ToUpper() + "_" + ddicno_chk.Rows[0]["Mnth"].ToString() + "_" + DateTime.Now.ToString("yyyy");
            }
            v2.Visible = true;
            Div1.Visible = false;
        }
        else
        {
            txt_sebab.Text = "";
            v2.Visible = true;
            Div1.Visible = false;
        }
        DataTable chk_limp = new DataTable();        
        
        if (DD_JENCUTI.SelectedValue == "01" || DD_JENCUTI.SelectedValue == "04")
        {            
            chk_limp = DBCon.Ora_Execute_table("Select top(1) chk_lampiran from hr_cmn_annual_leave where ann_leave_type_cd='" + DD_JENCUTI.SelectedValue + "'");
            if (chk_limp.Rows.Count != 0)
            {
                if (chk_limp.Rows[0]["chk_lampiran"].ToString() == "1")
                {
                    disp_id1.Visible = true;
                }
                else
                {
                    disp_id1.Visible = false;
                }
            }
            else
            {
                disp_id1.Visible = false;
            }
        }
        else
        {
            chk_limp = DBCon.Ora_Execute_table("Select top(1) chk_lampiran from hr_cmn_general_leave where gen_leave_type_cd='" + DD_JENCUTI.SelectedValue + "'");
            if (chk_limp.Rows.Count != 0)
            {
                if (chk_limp.Rows[0]["chk_lampiran"].ToString() == "1")
                {
                    disp_id1.Visible = true;
                }
                else
                {
                    disp_id1.Visible = false;
                }
            }
            else
            {
                disp_id1.Visible = false;
            }
        }
        string fdate = DateTime.Now.ToString("dd/MM/yyyy");
        DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        string s1_dt = "" + fd.AddDays(0).ToString("yyyy") + ", " + (double.Parse(fd.AddDays(0).ToString("MM")) - 1) + ", " + fd.AddDays(0).ToString("dd") + "";
        TextBox5.Text = s1_dt;
        string script = " $(function () { $('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);

    }
    private static int GetNumberOfWorkingDays(DateTime start, DateTime stop)
    {
        TimeSpan interval = stop - start;

        int totalWeek = interval.Days / 7;
        int totalWorkingDays = 5 * totalWeek;

        int remainingDays = interval.Days % 7;


        for (int i = 0; i <= remainingDays; i++)
        {
            DayOfWeek test = (DayOfWeek)(((int)start.DayOfWeek + i) % 7);
            if (test >= DayOfWeek.Monday && test <= DayOfWeek.Friday)
                totalWorkingDays++;
        }

        return totalWorkingDays;
    }

protected void btn_submit_Click(object sender, EventArgs e)
    {
        float dcnt = 0;
        double late_apply = 0;
        string chk_lev_qry = string.Empty, chk_lev_duration = string.Empty;
        DataTable ddicno = new DataTable();
        ddicno = DBCon.Ora_Execute_table("select str_curr_org_cd,org_state_cd,stf_staff_no From hr_staff_profile left join hr_organization s1 on org_gen_id=str_curr_org_cd where stf_staff_no='" + Session["New"].ToString() + "' ");

        if (ddicno.Rows.Count != 0)
        {
            string stffno = ddicno.Rows[0]["stf_staff_no"].ToString();
            string orgid = ddicno.Rows[0]["str_curr_org_cd"].ToString();
            string state_cd = ddicno.Rows[0]["org_state_cd"].ToString();
            string datedari = string.Empty,datedari1 = string.Empty, fromdate = string.Empty, todate = string.Empty;
            DateTime dt1;
           
                if (DD_JENCUTI.SelectedValue != "" && txt_tkcuti.Text != "" && txt_hing.Text != "" && txt_sebab.Text != "")
                {

                    datedari = txt_tkcuti.Text;
                    DateTime dt = DateTime.ParseExact(datedari, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    fromdate = dt.ToString("yyyy-MM-dd");
                    if (DD_JENCUTI.SelectedValue != "03")
                    {
                        datedari1 = txt_hing.Text;
                        dt1 = DateTime.ParseExact(datedari1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        todate = dt1.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        datedari1 = txt_tkcuti.Text;
                        dt1 = DateTime.ParseExact(datedari1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        todate = dt1.ToString("yyyy-MM-dd");
                    }

                    DateTime cur_dt = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    int cur_lev_dur = 0;
                if (DD_JENCUTI.SelectedValue == "01" || DD_JENCUTI.SelectedValue == "04")
                {
                    chk_lev_duration = "Select top(1) ISNULL(ann_leave_duration,'0') as lev_dur from hr_cmn_annual_leave where ann_leave_type_cd='" + DD_JENCUTI.SelectedValue + "'";
                }
                else
                {
                    chk_lev_duration = "Select top(1) ISNULL(gen_leave_duration,'0') as lev_dur from hr_cmn_general_leave where gen_leave_type_cd='" + DD_JENCUTI.SelectedValue + "'";
                }
                    DataTable get_duration = new DataTable();
                    get_duration = DBCon.Ora_Execute_table(chk_lev_duration);
                    if(get_duration.Rows.Count != 0)
                    {
                        cur_lev_dur = Int32.Parse(get_duration.Rows[0]["lev_dur"].ToString());
                    }
                    if ((dt - cur_dt).TotalDays <= cur_lev_dur)
                    {
                        late_apply = (dt - cur_dt).TotalDays;
                    }
                    

                        if ((dt1 - dt).TotalDays >= 0)
                    {
                    string fname = string.Empty;
                    if (FileUpload1.FileName != "")
                    {
                        int contentLength = FileUpload1.PostedFile.ContentLength;//You may need it for validation
                        string contentType = FileUpload1.PostedFile.ContentType;//You may need it for validation
                        string fileName = FileUpload1.PostedFile.FileName;
                        
                        if (fileName != "")
                        {
                            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/FILES/Attendance/" + fileName));//Or code to save in the DataBase.
                            fname = fileName;
                        }
                    }

                        DataTable get_hol_cnt = new DataTable();
                        get_hol_cnt = DBCon.Ora_Execute_table("select count(*) as cnt from hr_holiday where hol_dt>=DATEADD (day, DATEDIFF(day, 0, '" + fromdate + "'), 0) and hol_dt<=DATEADD(day, DATEDIFF(day, 0, '" + todate + "'), +0) and hol_state_cd='" + state_cd + "' and hol_cancel_ind='N' and hol_gen_id='" + orgid + "'");

                        //int totalWorkingDays = GetNumberOfWorkingDays(dt, dt1);
                        TimeSpan t = dt1 - dt;
                        var d = t.TotalDays;
                        var days = d + 1;
                        int formID = (Convert.ToInt32(days) - Convert.ToInt32(get_hol_cnt.Rows[0]["cnt"].ToString()));
                        if (formID != 0)
                        {
                            DataTable ins_aud = new DataTable();
                            string upssql, upssql1;
                            DataTable ddicno_lev = new DataTable();
                            ddicno_lev = DBCon.Ora_Execute_table("select * from hr_leave_application where lap_org_id='" + orgid + "' and lap_staff_no='" + stffno + "' and lap_leave_type_cd='" + DD_JENCUTI.SelectedValue + "' and  lap_leave_start_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fromdate + "'), 0) and lap_leave_end_dt<=DATEADD(day, DATEDIFF(day, 0, '" + fromdate + "'), +0) and lap_cancel_ind ='N'");

                            DateTime cdt1 = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            if (ddicno_lev.Rows.Count == 0)
                            {
                            //if (dt >= cdt1)
                            //{
                           
                                if(DD_JENCUTI.SelectedValue == "12")
                            {
                                chk_lev_qry = "select  sum(convert(float,col_balance_day)) as lev_balance_day, col_cat_cd as lev_leave_type_cd from hr_com_leave where col_end_dt >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' and col_org_id='" + orgid + "' and col_staff_no='" + stffno + "' and col_year='" + DateTime.Now.ToString("yyyy") + "' and col_cat_cd='" + DD_JENCUTI.SelectedValue + "' group by col_cat_cd";
                            }
                            else
                            {
                                chk_lev_qry = "select lev_balance_day,lev_leave_type_cd from hr_leave where lev_org_id='" + orgid + "' and lev_staff_no='" + stffno + "' and lev_year='" + DateTime.Now.ToString("yyyy") + "' and lev_leave_type_cd='" + DD_JENCUTI.SelectedValue + "'";
                            }

                                DataTable ddicno_levchk = new DataTable();
                                ddicno_levchk = DBCon.Ora_Execute_table(chk_lev_qry);
                                DataTable gen_rno = new DataTable();
                                gen_rno = DBCon.Ora_Execute_table("select count(*) as rcnt from hr_leave_application");
                              
                                if (gen_rno.Rows[0]["rcnt"].ToString() == "0")
                                {
                                    rucnt = "0000000001";
                                }
                                else
                                {
                                    rucnt = (double.Parse(gen_rno.Rows[0]["rcnt"].ToString()) + 1).ToString().PadLeft(10, '0');
                                }
                                if (ddicno_levchk.Rows.Count != 0)
                                {


                                    float sv1 = 0, sv2 = 0;
                                    if (ddicno_levchk.Rows[0]["lev_leave_type_cd"].ToString() == "01" || ddicno_levchk.Rows[0]["lev_leave_type_cd"].ToString() == "04" || ddicno_levchk.Rows[0]["lev_leave_type_cd"].ToString() == "12")
                                    {
                                        if (ddicno_levchk.Rows[0]["lev_leave_type_cd"].ToString() == "01")
                                        {
                                            double sdt1 = (dt - cdt1).TotalDays;

                                            if (ddicno_levchk.Rows[0]["lev_leave_type_cd"].ToString() == "01" && sdt1 < 0)
                                            {

                                                sv1 = float.Parse(ddicno_levchk.Rows[0]["lev_balance_day"].ToString());
                                                if (sv1 >= formID)
                                                {
                                                    if (sv1 != 0)
                                                    {
                                                        if (Session["level"].ToString() == "5" && Session["level_acc"].ToString() == "MHR")
                                                        {
                                                            upssql = "insert into hr_leave_application (lap_org_id,lap_staff_no,lap_application_dt,lap_leave_type_cd,lap_leave_cat_cd,lap_ref_no,lap_leave_start_dt,lap_leave_end_dt,lap_leave_day,lap_reason,lap_cancel_ind,lap_crt_id,lap_crt_dt,lap_approve_sts_cd,lap_doc,lap_approve_remark,lap_approve_id,lap_approve_dt,lap_cur_sts,lap_late_apply) values('" + orgid + "','" + stffno + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + DD_JENCUTI.SelectedValue + "','','" + rucnt + "','" + fromdate + "','" + todate + "','" + formID + "','" + txt_sebab.Text.Replace("'", "''") + "','N','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','01','" + fname + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','Y','" + late_apply + "')";
                                                        }
                                                        else
                                                        {
                                                            upssql = "insert into hr_leave_application (lap_org_id,lap_staff_no,lap_application_dt,lap_leave_type_cd,lap_leave_cat_cd,lap_ref_no,lap_leave_start_dt,lap_leave_end_dt,lap_leave_day,lap_reason,lap_cancel_ind,lap_crt_id,lap_crt_dt,lap_approve_sts_cd,lap_doc,lap_cur_sts,lap_late_apply) values('" + orgid + "','" + stffno + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + DD_JENCUTI.SelectedValue + "','','" + rucnt + "','" + fromdate + "','" + todate + "','" + formID + "','" + txt_sebab.Text.Replace("'", "''") + "','N','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','00','" + fname + "','Y','"+ late_apply + "','" + late_apply + "')";
                                                        }

                                                        Status = DBCon.Ora_Execute_CommamdText(upssql);
                                                        if (Status == "SUCCESS")
                                                        {
                                                            send_email();
                                                            Session["validate_success"] = "SUCCESS";
                                                            Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                                                            Response.Redirect("../SUMBER_MANUSIA/HR_MOHON_CUTI_view.aspx");
                                                        }
                                                        else
                                                        {
                                                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Telah Wujud Rekod Tidak Dibenarkan Untuk Dikemaskini.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Meninggalkan Tidak Sesuai untuk '" + DD_JENCUTI.SelectedItem.Text + ".',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                                                    }
                                                }
                                                else
                                                {
                                                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Baki Cuti Tidak Mencukupi.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                                                }
                                            }

                                            else if (ddicno_levchk.Rows[0]["lev_leave_type_cd"].ToString() == "01" && sdt1 >= 3)
                                            {

                                                sv1 = float.Parse(ddicno_levchk.Rows[0]["lev_balance_day"].ToString());
                                                if (sv1 >= formID)
                                                {
                                                    if (sv1 != 0)
                                                    {
                                                        if (Session["level"].ToString() == "5" && Session["level_acc"].ToString() == "MHR")
                                                        {
                                                            upssql = "insert into hr_leave_application (lap_org_id,lap_staff_no,lap_application_dt,lap_leave_type_cd,lap_leave_cat_cd,lap_ref_no,lap_leave_start_dt,lap_leave_end_dt,lap_leave_day,lap_reason,lap_cancel_ind,lap_crt_id,lap_crt_dt,lap_approve_sts_cd,lap_doc,lap_approve_remark,lap_approve_id,lap_approve_dt,lap_cur_sts,lap_late_apply) values('" + orgid + "','" + stffno + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + DD_JENCUTI.SelectedValue + "','','" + rucnt + "','" + fromdate + "','" + todate + "','" + formID + "','" + txt_sebab.Text.Replace("'", "''") + "','N','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','01','" + fname + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','Y','" + late_apply + "')";
                                                        }
                                                        else
                                                        {
                                                            upssql = "insert into hr_leave_application (lap_org_id,lap_staff_no,lap_application_dt,lap_leave_type_cd,lap_leave_cat_cd,lap_ref_no,lap_leave_start_dt,lap_leave_end_dt,lap_leave_day,lap_reason,lap_cancel_ind,lap_crt_id,lap_crt_dt,lap_approve_sts_cd,lap_doc,lap_cur_sts,lap_late_apply) values('" + orgid + "','" + stffno + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + DD_JENCUTI.SelectedValue + "','','" + rucnt + "','" + fromdate + "','" + todate + "','" + formID + "','" + txt_sebab.Text.Replace("'", "''") + "','N','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','00','" + fname + "','Y','" + late_apply + "')";
                                                        }

                                                        Status = DBCon.Ora_Execute_CommamdText(upssql);
                                                        if (Status == "SUCCESS")
                                                        {
                                                            send_email();
                                                            Session["validate_success"] = "SUCCESS";
                                                            Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                                                            Response.Redirect("../SUMBER_MANUSIA/HR_MOHON_CUTI_view.aspx");
                                                        }
                                                        else
                                                        {
                                                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Telah Wujud Rekod Tidak Dibenarkan Untuk Dikemaskini.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Meninggalkan Tidak Sesuai untuk '" + DD_JENCUTI.SelectedItem.Text + ".',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                                                    }
                                                }
                                                else
                                                {
                                                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Baki Cuti Tidak Mencukupi.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                                                }
                                            }
                                            else
                                            {
                                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila pohon 3 hari sebelum.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                                            }
                                        }
                                        else
                                        {
                                            if (ddicno_levchk.Rows[0]["lev_leave_type_cd"].ToString() == "04")
                                            {

                                                string gt_ltype = string.Empty, gt_fm_dt = string.Empty, gt_to_dt = string.Empty;
                                                sv1 = float.Parse(ddicno_levchk.Rows[0]["lev_balance_day"].ToString());
                                                float sv3 = 0;

                                                if (sv1 >= formID)
                                                {
                                                    DataTable ddicno_levchk1 = new DataTable();
                                                    ddicno_levchk1 = DBCon.Ora_Execute_table("select lev_balance_day,lev_leave_type_cd,lev_taken_day from hr_leave where lev_org_id='" + orgid + "' and lev_staff_no='" + stffno + "' and lev_year='" + DateTime.Now.ToString("yyyy") + "' and lev_leave_type_cd='05'");

                                                    sv2 = float.Parse(ddicno_levchk1.Rows[0]["lev_balance_day"].ToString());
                                                    if (sv2 >= formID)
                                                    {
                                                        sv3 = formID;
                                                    }
                                                    else
                                                    {
                                                        sv3 = sv2;
                                                    }

                                                        if (Session["level"].ToString() == "5" && Session["level_acc"].ToString() == "MHR")
                                                    {
                                                        upssql = "insert into hr_leave_application (lap_org_id,lap_staff_no,lap_application_dt,lap_leave_type_cd,lap_leave_cat_cd,lap_ref_no,lap_leave_start_dt,lap_leave_end_dt,lap_leave_day,lap_reason,lap_cancel_ind,lap_crt_id,lap_crt_dt,lap_approve_sts_cd,lap_doc,lap_approve_remark,lap_approve_id,lap_approve_dt,lap_pre_balance,lap_pre_type_cd,lap_cur_sts,lap_late_apply) values('" + orgid + "','" + stffno + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + DD_JENCUTI.SelectedValue + "','','" + rucnt + "','" + fromdate + "','" + todate + "','" + formID + "','" + txt_sebab.Text.Replace("'", "''") + "','N','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','01','" + fname + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','"+ sv3 + "','05','Y','" + late_apply + "')";
                                                        //upssql1 = "insert into hr_leave_application (lap_org_id,lap_staff_no,lap_application_dt,lap_leave_type_cd,lap_leave_cat_cd,lap_ref_no,lap_leave_start_dt,lap_leave_end_dt,lap_leave_day,lap_reason,lap_cancel_ind,lap_crt_id,lap_crt_dt,lap_approve_sts_cd,lap_doc,lap_approve_remark,lap_approve_id,lap_approve_dt) values('" + orgid + "','" + stffno + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','05','','" + rucnt + "','" + fromdate + "','" + todate + "','" + formID + "','" + txt_sebab.Text.Replace("'", "''") + "','N','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','01','" + fname + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                                                        
                                                    }
                                                    else
                                                    {
                                                        upssql = "insert into hr_leave_application (lap_org_id,lap_staff_no,lap_application_dt,lap_leave_type_cd,lap_leave_cat_cd,lap_ref_no,lap_leave_start_dt,lap_leave_end_dt,lap_leave_day,lap_reason,lap_cancel_ind,lap_crt_id,lap_crt_dt,lap_approve_sts_cd,lap_doc,lap_pre_balance,lap_pre_type_cd,lap_cur_sts,lap_late_apply) values('" + orgid + "','" + stffno + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + DD_JENCUTI.SelectedValue + "','','" + rucnt + "','" + fromdate + "','" + todate + "','" + formID + "','" + txt_sebab.Text.Replace("'", "''") + "','N','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','00','" + fname + "','" + sv3 + "','05','Y','" + late_apply + "')";
                                                        //upssql1 = "insert into hr_leave_application (lap_org_id,lap_staff_no,lap_application_dt,lap_leave_type_cd,lap_leave_cat_cd,lap_ref_no,lap_leave_start_dt,lap_leave_end_dt,lap_leave_day,lap_reason,lap_cancel_ind,lap_crt_id,lap_crt_dt,lap_approve_sts_cd,lap_doc) values('" + orgid + "','" + stffno + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','05','','" + rucnt + "','" + fromdate + "','" + todate + "','" + formID + "','" + txt_sebab.Text.Replace("'", "''") + "','N','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','00','" + fname + "')";
                                                    }

                                                    Status = DBCon.Ora_Execute_CommamdText(upssql);
                                                    //Status1 = DBCon.Ora_Execute_CommamdText(upssql1);
                                                    if (Status == "SUCCESS")
                                                    {
                                                        send_email();
                                                        Session["validate_success"] = "SUCCESS";
                                                        Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                                                        Response.Redirect("../SUMBER_MANUSIA/HR_MOHON_CUTI_view.aspx");
                                                    }
                                                    else
                                                    {
                                                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Telah Wujud Rekod Tidak Dibenarkan Untuk Dikemaskini.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                                                    }
                                                }
                                                else
                                                {
                                                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Baki Cuti Tidak Mencukupi.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                                                    //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('" + DD_JENCUTI.SelectedItem.Text + " Meninggalkan hanya untuk " + sv1 + " hari.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                                                }
                                            }
                                            else
                                            {
                                                sv1 = float.Parse(ddicno_levchk.Rows[0]["lev_balance_day"].ToString());
                                                if (sv1 >= formID)
                                                {
                                                    if (sv1 != 0)
                                                    {
                                                        if (Session["level"].ToString() == "5" && Session["level_acc"].ToString() == "MHR")
                                                        {
                                                            upssql = "insert into hr_leave_application (lap_org_id,lap_staff_no,lap_application_dt,lap_leave_type_cd,lap_leave_cat_cd,lap_ref_no,lap_leave_start_dt,lap_leave_end_dt,lap_leave_day,lap_reason,lap_cancel_ind,lap_crt_id,lap_crt_dt,lap_approve_sts_cd,lap_doc,lap_approve_remark,lap_approve_id,lap_approve_dt,lap_cur_sts,lap_late_apply) values('" + orgid + "','" + stffno + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + DD_JENCUTI.SelectedValue + "','','" + rucnt + "','" + fromdate + "','" + todate + "','" + formID + "','" + txt_sebab.Text.Replace("'", "''") + "','N','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','01','" + fname + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','Y','" + late_apply + "')";
                                                        }
                                                        else
                                                        {
                                                            upssql = "insert into hr_leave_application (lap_org_id,lap_staff_no,lap_application_dt,lap_leave_type_cd,lap_leave_cat_cd,lap_ref_no,lap_leave_start_dt,lap_leave_end_dt,lap_leave_day,lap_reason,lap_cancel_ind,lap_crt_id,lap_crt_dt,lap_approve_sts_cd,lap_doc,lap_cur_sts,lap_late_apply) values('" + orgid + "','" + stffno + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + DD_JENCUTI.SelectedValue + "','','" + rucnt + "','" + fromdate + "','" + todate + "','" + formID + "','" + txt_sebab.Text.Replace("'", "''") + "','N','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','00','" + fname + "','Y','" + late_apply + "')";
                                                        }

                                                        Status = DBCon.Ora_Execute_CommamdText(upssql);
                                                        if (Status == "SUCCESS")
                                                        {
                                                            send_email();
                                                            Session["validate_success"] = "SUCCESS";
                                                            Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                                                            Response.Redirect("../SUMBER_MANUSIA/HR_MOHON_CUTI_view.aspx");
                                                        }
                                                        else
                                                        {
                                                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Telah Wujud Rekod Tidak Dibenarkan Untuk Dikemaskini.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Meninggalkan Tidak Sesuai untuk '" + DD_JENCUTI.SelectedItem.Text + ".',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                                                    }
                                                }
                                                else
                                                {
                                                    //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('" + DD_JENCUTI.SelectedItem.Text + " Meninggalkan hanya untuk " + sv1 + " hari.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                                                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Baki Cuti Tidak Mencukupi.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        if (DD_JENCUTI.SelectedValue == "10" || DD_JENCUTI.SelectedValue == "09" || DD_JENCUTI.SelectedValue == "12")
                                        {
                                            float ss10_chk;
                                            DataTable ddicno_levchk1 = new DataTable();
                                            //ddicno_levchk1 = DBCon.Ora_Execute_table("select * from hr_cmn_general_leave where gen_leave_type_cd='10'");
                                            ddicno_levchk1 = DBCon.Ora_Execute_table("select a.lev_staff_no,a.hr_jenis_desc,a.a,cast(a.c as float) as c,a.b,a.ab,ISNULL(b1.e,'0') as e,ISNULL(b2.e,'0') as d,(a.ab - (ISNULL(b1.e,'0') + ISNULL(b2.e,'0'))) as res from (select '"+ Session["New"].ToString() + "' as lev_staff_no,hr_jenis_desc,'0' a,gen_leave_day as c,'' as b,gen_leave_day as ab,'' e, '' d, '' res,s1.hr_jenis_Code from Ref_hr_jenis_cuti s1 inner join hr_cmn_general_leave as s2 on s2.gen_leave_type_cd=s1.hr_jenis_Code where hr_jenis_Code In ('"+ DD_JENCUTI.SelectedValue + "')) as a full outer join(select lap_staff_no,lap_leave_type_cd,sum(convert(float,lap_leave_day)) as e from hr_leave_application where lap_staff_no='"+ Session["New"].ToString() + "' and lap_cancel_ind='N' and lap_leave_type_cd IN('"+ DD_JENCUTI.SelectedValue + "') and lap_approve_sts_cd NOT IN ('04','01') and ISNULL(lap_endorse_sts_cd,'') = '' group by lap_staff_no,lap_leave_type_cd) as b1 on b1.lap_staff_no=a.lev_staff_no and b1.lap_leave_type_cd=a.hr_jenis_Code full outer join(select lap_staff_no,lap_leave_type_cd,sum(convert(float,lap_leave_day)) as e from hr_leave_application where lap_staff_no='"+ Session["New"].ToString() + "' and lap_cancel_ind='N' and lap_leave_type_cd IN('"+ DD_JENCUTI.SelectedValue + "') and lap_approve_sts_cd NOT IN ('04','01') and ISNULL(lap_endorse_sts_cd,'') = '01' and lap_cur_sts='Y' group by lap_staff_no,lap_leave_type_cd) as b2 on b2.lap_staff_no=a.lev_staff_no and b2.lap_leave_type_cd=a.hr_jenis_Code");
                                            if (ddicno_levchk1.Rows.Count != 0)
                                            {
                                                ss10_chk = float.Parse(ddicno_levchk1.Rows[0]["res"].ToString());
                                            }
                                            else
                                            {
                                                ss10_chk = 60;
                                            }
                                            if (ss10_chk >= formID)
                                            {
                                                dcnt = formID;

                                                if (Session["level"].ToString() == "5" && Session["level_acc"].ToString() == "MHR")
                                                {
                                                    upssql = "insert into hr_leave_application (lap_org_id,lap_staff_no,lap_application_dt,lap_leave_type_cd,lap_leave_cat_cd,lap_ref_no,lap_leave_start_dt,lap_leave_end_dt,lap_leave_day,lap_reason,lap_cancel_ind,lap_crt_id,lap_crt_dt,lap_approve_sts_cd,lap_doc,lap_approve_remark,lap_approve_id,lap_approve_dt,lap_cur_sts,lap_late_apply) values('" + orgid + "','" + stffno + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + DD_JENCUTI.SelectedValue + "','','" + rucnt + "','" + fromdate + "','" + todate + "','" + dcnt + "','" + txt_sebab.Text.Replace("'", "''") + "','N','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','01','" + fname + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','Y','" + late_apply + "')";
                                                }
                                                else
                                                {
                                                    upssql = "insert into hr_leave_application (lap_org_id,lap_staff_no,lap_application_dt,lap_leave_type_cd,lap_leave_cat_cd,lap_ref_no,lap_leave_start_dt,lap_leave_end_dt,lap_leave_day,lap_reason,lap_cancel_ind,lap_crt_id,lap_crt_dt,lap_approve_sts_cd,lap_doc,lap_cur_sts,lap_late_apply) values('" + orgid + "','" + stffno + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + DD_JENCUTI.SelectedValue + "','','" + rucnt + "','" + fromdate + "','" + todate + "','" + dcnt + "','" + txt_sebab.Text.Replace("'", "''") + "','N','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','00','" + fname + "','Y','" + late_apply + "')";
                                                }

                                                Status = DBCon.Ora_Execute_CommamdText(upssql);
                                                if (Status == "SUCCESS")
                                                {
                                                    send_email();
                                                    Session["validate_success"] = "SUCCESS";
                                                    Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                                                    Response.Redirect("../SUMBER_MANUSIA/HR_MOHON_CUTI_view.aspx");
                                                }
                                                else
                                                {
                                                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Telah Wujud Rekod Tidak Dibenarkan Untuk Dikemaskini.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                                                }
                                            }
                                            else
                                            {
                                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Baki Cuti Tidak Mencukupi.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                                            }
                                        }
                                        else
                                        {
                                            if (DD_JENCUTI.SelectedValue == "03")
                                            {
                                                dcnt = float.Parse("0.5");
                                            }
                                            else
                                            {
                                                dcnt = formID;
                                            }
                                            if (Session["level"].ToString() == "5" && Session["level_acc"].ToString() == "MHR")
                                            {
                                                upssql = "insert into hr_leave_application (lap_org_id,lap_staff_no,lap_application_dt,lap_leave_type_cd,lap_leave_cat_cd,lap_ref_no,lap_leave_start_dt,lap_leave_end_dt,lap_leave_day,lap_reason,lap_cancel_ind,lap_crt_id,lap_crt_dt,lap_approve_sts_cd,lap_doc,lap_approve_remark,lap_approve_id,lap_approve_dt,lap_sesi,lap_cur_sts,lap_late_apply) values('" + orgid + "','" + stffno + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + DD_JENCUTI.SelectedValue + "','','" + rucnt + "','" + fromdate + "','" + todate + "','" + dcnt + "','" + txt_sebab.Text.Replace("'", "''") + "','N','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','01','" + fname + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','"+ sel_sesi.SelectedValue + "','Y','" + late_apply + "')";
                                            }
                                            else
                                            {
                                                upssql = "insert into hr_leave_application (lap_org_id,lap_staff_no,lap_application_dt,lap_leave_type_cd,lap_leave_cat_cd,lap_ref_no,lap_leave_start_dt,lap_leave_end_dt,lap_leave_day,lap_reason,lap_cancel_ind,lap_crt_id,lap_crt_dt,lap_approve_sts_cd,lap_doc,lap_sesi,lap_cur_sts,lap_late_apply) values('" + orgid + "','" + stffno + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + DD_JENCUTI.SelectedValue + "','','" + rucnt + "','" + fromdate + "','" + todate + "','" + dcnt + "','" + txt_sebab.Text.Replace("'", "''") + "','N','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','00','" + fname + "','"+ sel_sesi.SelectedValue + "','Y','" + late_apply + "')";
                                            }

                                            Status = DBCon.Ora_Execute_CommamdText(upssql);
                                            if (Status == "SUCCESS")
                                            {
                                                send_email();
                                                Session["validate_success"] = "SUCCESS";
                                                Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                                                Response.Redirect("../SUMBER_MANUSIA/HR_MOHON_CUTI_view.aspx");
                                            }
                                            else
                                            {
                                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Telah Wujud Rekod Tidak Dibenarkan Untuk Dikemaskini.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (DD_JENCUTI.SelectedValue == "10" || DD_JENCUTI.SelectedValue == "09" || DD_JENCUTI.SelectedValue == "12")
                                    {
                                        float ss10_chk;
                                        DataTable ddicno_levchk1 = new DataTable();
                                        //ddicno_levchk1 = DBCon.Ora_Execute_table("select * from hr_cmn_general_leave where gen_leave_type_cd='10'");
                                        ddicno_levchk1 = DBCon.Ora_Execute_table("select a.lev_staff_no,a.hr_jenis_desc,a.a,cast(a.c as float) as c,a.b,a.ab,ISNULL(b1.e,'0') as e,ISNULL(b2.e,'0') as d,(a.ab - (ISNULL(b1.e,'0') + ISNULL(b2.e,'0'))) as res from (select '" + Session["New"].ToString() + "' as lev_staff_no,hr_jenis_desc,'0' a,gen_leave_day as c,'' as b,gen_leave_day as ab,'' e, '' d, '' res,s1.hr_jenis_Code from Ref_hr_jenis_cuti s1 inner join hr_cmn_general_leave as s2 on s2.gen_leave_type_cd=s1.hr_jenis_Code where hr_jenis_Code In ('" + DD_JENCUTI.SelectedValue + "')) as a full outer join(select lap_staff_no,lap_leave_type_cd,sum(convert(float,lap_leave_day)) as e from hr_leave_application where lap_staff_no='" + Session["New"].ToString() + "' and lap_cancel_ind='N' and lap_leave_type_cd IN('" + DD_JENCUTI.SelectedValue + "') and lap_approve_sts_cd NOT IN ('04','01') and ISNULL(lap_endorse_sts_cd,'') = '' group by lap_staff_no,lap_leave_type_cd) as b1 on b1.lap_staff_no=a.lev_staff_no and b1.lap_leave_type_cd=a.hr_jenis_Code full outer join(select lap_staff_no,lap_leave_type_cd,sum(convert(float,lap_leave_day)) as e from hr_leave_application where lap_staff_no='" + Session["New"].ToString() + "' and lap_cancel_ind='N' and lap_leave_type_cd IN('" + DD_JENCUTI.SelectedValue + "') and lap_approve_sts_cd NOT IN ('04','01') and ISNULL(lap_endorse_sts_cd,'') = '01' and lap_cur_sts='Y' group by lap_staff_no,lap_leave_type_cd) as b2 on b2.lap_staff_no=a.lev_staff_no and b2.lap_leave_type_cd=a.hr_jenis_Code");
                                        if (ddicno_levchk1.Rows.Count != 0)
                                        {
                                            ss10_chk = float.Parse(ddicno_levchk1.Rows[0]["res"].ToString());
                                        }
                                        else
                                        {
                                            ss10_chk = 60;
                                        }
                                        if (ss10_chk >= formID)
                                        {
                                            dcnt = formID;

                                            if (Session["level"].ToString() == "5" && Session["level_acc"].ToString() == "MHR")
                                            {
                                                upssql = "insert into hr_leave_application (lap_org_id,lap_staff_no,lap_application_dt,lap_leave_type_cd,lap_leave_cat_cd,lap_ref_no,lap_leave_start_dt,lap_leave_end_dt,lap_leave_day,lap_reason,lap_cancel_ind,lap_crt_id,lap_crt_dt,lap_approve_sts_cd,lap_doc,lap_approve_remark,lap_approve_id,lap_approve_dt,lap_cur_sts,lap_late_apply) values('" + orgid + "','" + stffno + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + DD_JENCUTI.SelectedValue + "','','" + rucnt + "','" + fromdate + "','" + todate + "','" + dcnt + "','" + txt_sebab.Text.Replace("'", "''") + "','N','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','01','" + fname + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','Y','" + late_apply + "')";
                                            }
                                            else
                                            {
                                                upssql = "insert into hr_leave_application (lap_org_id,lap_staff_no,lap_application_dt,lap_leave_type_cd,lap_leave_cat_cd,lap_ref_no,lap_leave_start_dt,lap_leave_end_dt,lap_leave_day,lap_reason,lap_cancel_ind,lap_crt_id,lap_crt_dt,lap_approve_sts_cd,lap_doc,lap_cur_sts,lap_late_apply) values('" + orgid + "','" + stffno + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + DD_JENCUTI.SelectedValue + "','','" + rucnt + "','" + fromdate + "','" + todate + "','" + dcnt + "','" + txt_sebab.Text.Replace("'", "''") + "','N','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','00','" + fname + "','Y','" + late_apply + "')";
                                            }

                                            Status = DBCon.Ora_Execute_CommamdText(upssql);
                                            if (Status == "SUCCESS")
                                            {
                                                send_email();
                                                Session["validate_success"] = "SUCCESS";
                                                Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                                                Response.Redirect("../SUMBER_MANUSIA/HR_MOHON_CUTI_view.aspx");
                                            }
                                            else
                                            {
                                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Telah Wujud Rekod Tidak Dibenarkan Untuk Dikemaskini.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                                            }
                                        }
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Baki Cuti Tidak Mencukupi.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                                        }
                                    }
                                    else
                                    {
                                        if (DD_JENCUTI.SelectedValue == "03")
                                        {
                                            dcnt = float.Parse("0.5");
                                        }
                                        else
                                        {
                                            dcnt = formID;
                                        }
                                        if (Session["level"].ToString() == "5" && Session["level_acc"].ToString() == "MHR")
                                        {
                                            upssql = "insert into hr_leave_application (lap_org_id,lap_staff_no,lap_application_dt,lap_leave_type_cd,lap_leave_cat_cd,lap_ref_no,lap_leave_start_dt,lap_leave_end_dt,lap_leave_day,lap_reason,lap_cancel_ind,lap_crt_id,lap_crt_dt,lap_approve_sts_cd,lap_doc,lap_approve_remark,lap_approve_id,lap_approve_dt,lap_sesi,lap_cur_sts,lap_late_apply) values('" + orgid + "','" + stffno + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + DD_JENCUTI.SelectedValue + "','','" + rucnt + "','" + fromdate + "','" + todate + "','" + dcnt + "','" + txt_sebab.Text.Replace("'", "''") + "','N','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','01','" + fname + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + sel_sesi.SelectedValue + "','Y','" + late_apply + "')";
                                        }
                                        else
                                        {
                                            upssql = "insert into hr_leave_application (lap_org_id,lap_staff_no,lap_application_dt,lap_leave_type_cd,lap_leave_cat_cd,lap_ref_no,lap_leave_start_dt,lap_leave_end_dt,lap_leave_day,lap_reason,lap_cancel_ind,lap_crt_id,lap_crt_dt,lap_approve_sts_cd,lap_doc,lap_sesi,lap_cur_sts,lap_late_apply) values('" + orgid + "','" + stffno + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + DD_JENCUTI.SelectedValue + "','','" + rucnt + "','" + fromdate + "','" + todate + "','" + dcnt + "','" + txt_sebab.Text.Replace("'", "''") + "','N','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','00','" + fname + "','" + sel_sesi.SelectedValue + "','Y','" + late_apply + "')";
                                        }

                                        Status = DBCon.Ora_Execute_CommamdText(upssql);
                                        if (Status == "SUCCESS")
                                        {
                                            send_email();
                                            Session["validate_success"] = "SUCCESS";
                                            Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                                            Response.Redirect("../SUMBER_MANUSIA/HR_MOHON_CUTI_view.aspx");
                                        }
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Telah Wujud Rekod Tidak Dibenarkan Untuk Dikemaskini.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                                        }
                                    }
                                  
                                }
                               
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Tarikh Cuti Telah Diambil.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Tarikh permohonan cuti anda pada Hari Cuti.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Tarikh Tamat Tidak Sah',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Maklumat Cuti.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
           
        }
        else
        {            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
        grid();
    }

    void send_email()
    {
        TextInfo txtinfo = culinfo.TextInfo;
        DataTable Ds = new DataTable();
        DataTable Ds1 = new DataTable();
        DataTable Ds2 = new DataTable();
        DataTable Ds3 = new DataTable();
        DataTable email_settings = new DataTable();
        string Mailmsg = string.Empty, Mailmsg1 = string.Empty, Mail_id1 = string.Empty, Mail_id2 = string.Empty, bal_lve = string.Empty;
        try
        {
            email_settings = DBCon.Ora_Execute_table("select config_email_head,config_email_host,config_email_id,config_email_port,config_email_pwd,config_email_url,config_email_web from site_settings where ID='1'");
            Ds = Dblog.Ora_Execute_table("select * from KK_User_Login where KK_userid='admin'");
            Ds1 = Dblog.Ora_Execute_table("select * from KK_User_Login where KK_userid='C0019'");
            Ds2 = Dblog.Ora_Execute_table("select * from KK_User_Login where KK_userid='" + Session["new"].ToString() + "'");

            if (DD_JENCUTI.SelectedValue == "01" || DD_JENCUTI.SelectedValue == "04" || DD_JENCUTI.SelectedValue == "05")
            {
                Ds3 = Dblog.Ora_Execute_table("select *,lev_balance_day as res from hr_leave where lev_year='" + DateTime.Now.Year.ToString() + "' and lev_staff_no='" + Session["new"].ToString() + "' and lev_leave_type_cd='" + DD_JENCUTI.SelectedValue + "'");
            }
            else
            {
                Ds3 = DBCon.Ora_Execute_table("select a.lev_staff_no,a.hr_jenis_desc,a.a,cast(a.c as float) as c,a.b,a.ab,ISNULL(b1.e,'0') as e,ISNULL(b2.e,'0') as d,(a.ab - (ISNULL(b1.e,'0') + ISNULL(b2.e,'0'))) as res from (select '" + Session["New"].ToString() + "' as lev_staff_no,hr_jenis_desc,'0' a,gen_leave_day as c,'' as b,gen_leave_day as ab,'' e, '' d, '' res,s1.hr_jenis_Code from Ref_hr_jenis_cuti s1 inner join hr_cmn_general_leave as s2 on s2.gen_leave_type_cd=s1.hr_jenis_Code where hr_jenis_Code In ('" + DD_JENCUTI.SelectedValue + "')) as a full outer join(select lap_staff_no,lap_leave_type_cd,sum(convert(float,lap_leave_day)) as e from hr_leave_application where lap_staff_no='" + Session["New"].ToString() + "' and lap_cancel_ind='N' and lap_leave_type_cd IN('" + DD_JENCUTI.SelectedValue + "') and lap_approve_sts_cd NOT IN ('04','01') and ISNULL(lap_endorse_sts_cd,'') = '' group by lap_staff_no,lap_leave_type_cd) as b1 on b1.lap_staff_no=a.lev_staff_no and b1.lap_leave_type_cd=a.hr_jenis_Code full outer join(select lap_staff_no,lap_leave_type_cd,sum(convert(float,lap_leave_day)) as e from hr_leave_application where lap_staff_no='" + Session["New"].ToString() + "' and lap_cancel_ind='N' and lap_leave_type_cd IN('" + DD_JENCUTI.SelectedValue + "') and lap_approve_sts_cd NOT IN ('04','01') and ISNULL(lap_endorse_sts_cd,'') = '01' and lap_cur_sts='Y' group by lap_staff_no,lap_leave_type_cd) as b2 on b2.lap_staff_no=a.lev_staff_no and b2.lap_leave_type_cd=a.hr_jenis_Code");
            }


            if (Ds3.Rows.Count != 0)
            {
                bal_lve = Ds3.Rows[0]["res"].ToString();
            }
            else
            {
                bal_lve = "0";
            }

            if (Ds.Rows[0]["KK_email"].ToString() != "")
            {
                Mail_id1 = Ds.Rows[0]["KK_email"].ToString();
            }

            if (Ds1.Rows[0]["KK_email"].ToString() != "")
            {
                Mail_id2 = Ds1.Rows[0]["KK_email"].ToString();
            }

            var fromemail = new MailAddress(email_settings.Rows[0]["config_email_id"].ToString(), email_settings.Rows[0]["config_email_head"].ToString());
            var fromemailpassword = email_settings.Rows[0]["config_email_pwd"].ToString();
            string subject = email_settings.Rows[0]["config_email_web"].ToString()+ " - Leave Application";
            var verifyurl = email_settings.Rows[0]["config_email_url"].ToString();
            var link = verifyurl;
            DateTime dt1 = DateTime.ParseExact(txt_tkcuti.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dt2 = DateTime.ParseExact(txt_hing.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (Mail_id1 != "")
            {
                var toemail = new MailAddress(Mail_id1);
                string body = "Hello " + txtinfo.ToTitleCase(Ds.Rows[0]["Kk_username"].ToString().ToLower()) + ",<br/>Pending Approval for " + DD_JENCUTI.SelectedItem.Text + " on " + dt1.ToString("dd/MM/yyyy") + " to " + dt2.ToString("dd/MM/yyyy") + " from " + txtinfo.ToTitleCase(Ds2.Rows[0]["Kk_username"].ToString().ToLower()) + "<br/><br/> Remaining " + DD_JENCUTI.SelectedItem.Text + " is " + bal_lve + " days from this application.<br/><br/> If You require any clarifications about this application, please contact your HR Department. <br/><br/> Thank You,<br/><a href='" + link + "'> " + email_settings.Rows[0]["config_email_web"].ToString() + " </a></body></html> . </a>";
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                {
                    Host = email_settings.Rows[0]["config_email_host"].ToString(),
                    Port = Int32.Parse(email_settings.Rows[0]["config_email_port"].ToString()),
                    EnableSsl = false,
                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromemail.Address, fromemailpassword)


                };
                using (var message = new MailMessage(fromemail, toemail)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                    smtp.Send(message);

            }

            if (Mail_id2 != "")
            {
                var toemail = new MailAddress(Mail_id2);
                string body = "Hello " + txtinfo.ToTitleCase(Ds1.Rows[0]["Kk_username"].ToString().ToLower()) + ",<br/>" + DD_JENCUTI.SelectedItem.Text + " applied by " + txtinfo.ToTitleCase(Ds2.Rows[0]["Kk_username"].ToString().ToLower()) + " on " + dt1.ToString("dd/MM/yyyy") + " to " + dt2.ToString("dd/MM/yyyy") + " is pending for approval(" + txtinfo.ToTitleCase(Ds.Rows[0]["Kk_username"].ToString().ToLower()) + ")<br/><br/>Remaining " + DD_JENCUTI.SelectedItem.Text + " is " + bal_lve + " days from this application. <br/><br/> If You require any clarifications about this application, please contact your HR Department.<br/><br/> Thank You,<br/><a><html><body><a href='" + link + "'> " + email_settings.Rows[0]["config_email_web"].ToString() + " </a></body></html> . </a>";
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                {
                    Host = email_settings.Rows[0]["config_email_host"].ToString(),
                    Port = Int32.Parse(email_settings.Rows[0]["config_email_port"].ToString()),
                    EnableSsl = false,
                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromemail.Address, fromemailpassword)


                };
                using (var message = new MailMessage(fromemail, toemail)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                    smtp.Send(message);

            }
            service.audit_trail("P0059", "Simpan", Session["new"].ToString(), rucnt);

        }
        catch (Exception ex)
        {
            //throw ex;
            service.LogError(ex.ToString());
        }
    }

   

    void clr()
    {
        grid();
        DD_JENCUTI.SelectedValue = "";
        //DD_KETOG.SelectedValue = "";
        txt_norju.Text = "";
        txt_tkcuti.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txt_hing.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txt_sebab.Text = "";
        //rujno.Attributes.Remove("style");
        //subkcuti.Attributes.Remove("style");
    }



    protected void rst_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../SUMBER_MANUSIA/HR_MOHON_CUTI.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../SUMBER_MANUSIA/HR_MOHON_CUTI_view.aspx");
    }


}