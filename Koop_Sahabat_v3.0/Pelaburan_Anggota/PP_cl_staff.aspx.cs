using System;
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


public partial class PP_cl_staff : System.Web.UI.Page
{
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    //string str;
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    //SqlCommand com;
    DataTable dt = new DataTable();
    string Status = string.Empty;
    string level, userid;
    string radiobtn1, radiobtn2, radiobtn3;
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Button7);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                level = Session["level"].ToString();
                userid = Session["New"].ToString();
                Button6.Visible = false;
                txtnama.Attributes.Add("Readonly", "Readonly");
                txtpeja.Attributes.Add("Readonly", "Readonly");
                txtjaba.Attributes.Add("Readonly", "Readonly");
                var samp = Request.Url.Query;
                if (samp != "")
                {
                    txticno.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    srch_Click();
                    Button4.Visible = false;
                    Button5.Visible = false;
                    Button1.Visible = false;
                    Button7.Visible = true;
                    Button6.Visible = true;
                    Button8.Visible = true;

                }
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    void reset()
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        reset();
    }

    void srch_Click()
    {
        SqlConnection conn = new SqlConnection(cs);
        if (txticno.Text != "")
        {
            DataTable Dt = new DataTable();
            Dt = DBCon.Ora_Execute_table("select mem_new_icno from mem_member where mem_new_icno='" + txticno.Text + "'");
            if (Dt.Rows.Count > 0)
            {
                SqlConnection con = new SqlConnection(cs);
                con.Open();
                string query1 = "select mem_name,ISNULL(rc.cawangan_name,'') as cawangan_name,mem_centre from mem_member as mm left join ref_cawangan as rc on rc.cawangan_code=mm.mem_branch_cd where mem_new_icno='" + txticno.Text + "' and mm.mem_staff_ind='Y'";
                var sqlCommand1 = new SqlCommand(query1, con);
                var sqlReader1 = sqlCommand1.ExecuteReader();
                if (sqlReader1.Read() == true)
                {
                    txtnama.Text = (string)sqlReader1["mem_name"];
                    txtpeja.Text = (string)sqlReader1["cawangan_name"];
                    txtjaba.Text = (string)sqlReader1["mem_centre"];
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
                con.Close();

                //cheklist table reader

                con.Open();
                string query2 = "select ac.che_staff_supp_doc,ac.che_muflis_ind,ac.che_muflis_remark,ac.che_legal_ind,ac.che_legal_remark,ac.che_active_status_ind  from jpa_application_checklist as ac  left join jpa_application as ap on ap.app_applcn_no=ac.che_applcn_no  left join mem_member as mm on mm.mem_new_icno=ap.app_new_icno where  ap.app_new_icno='" + txticno.Text + "' and mm.mem_staff_ind='Y' and ac.che_appl_sts_ind!='1'";
                var sqlCommand2 = new SqlCommand(query2, con);
                var sqlReader2 = sqlCommand2.ExecuteReader();
                if (sqlReader2.Read() == true)
                {
                    //string appno = (string)sqlReader2["che_muflis_remark"].ToString();
                    txtjmn.Text = (string)sqlReader2["che_muflis_remark"].ToString();
                    txtjyn.Text = (string)sqlReader2["che_legal_remark"].ToString();

                    string radio1 = (string)sqlReader2["che_muflis_ind"].ToString();

                    string jar1 = radio1.ToString();

                    if (jar1 == "True")
                    {
                        rbsm1.Checked = true;
                    }
                    else
                    {
                        rbsm2.Checked = true;
                    }

                    string radio2 = (string)sqlReader2["che_legal_ind"].ToString();

                    string bar2 = radio2.ToString();

                    if (bar2 == "True")
                    {
                        rbtuu1.Checked = true;
                    }
                    else
                    {
                        rbtuu2.Checked = true;
                    }

                    string radio3 = (string)sqlReader2["che_active_status_ind"].ToString();

                    string bar3 = radio3.ToString();

                    if (bar3 == "True")
                    {
                        rbsa1.Checked = true;
                    }
                    else
                    {
                        rbsa2.Checked = true;
                    }

                    string readText1 = (string)sqlReader2["che_staff_supp_doc"];

                    string bar1 = readText1.ToString();
                    for (int i = 0; i <= readText1.Length; i++)
                    {
                        if (i == 0)
                        {
                            if (bar1.Substring(0, 1) == "1")
                            {
                                cbskpp1.Checked = true;
                            }
                            else
                            {
                                cbskpp1.Checked = false;
                            }
                        }
                        else if (i == 1)
                        {
                            if (bar1.Substring(1, 1) == "1")
                            {
                                cbskpp2.Checked = true;
                            }
                            else
                            {
                                cbskpp2.Checked = false;
                            }
                        }
                        else if (i == 2)
                        {
                            if (bar1.Substring(2, 1) == "1")
                            {
                                cbssgp3.Checked = true;
                            }
                            else
                            {
                                cbssgp3.Checked = false;
                            }
                        }
                        else if (i == 3)
                        {
                            if (bar1.Substring(3, 1) == "1")
                            {
                                cbssgp4.Checked = true;
                            }
                            else
                            {
                                cbssgp4.Checked = false;
                            }
                        }
                        else if (i == 4)
                        {
                            if (bar1.Substring(4, 1) == "1")
                            {
                                cbpd5.Checked = true;
                            }
                            else
                            {
                                cbpd5.Checked = false;
                            }
                        }
                        else if (i == 5)
                        {
                            if (bar1.Substring(5, 1) == "1")
                            {
                                cbbtkh6.Checked = true;
                            }
                            else
                            {
                                cbbtkh6.Checked = false;
                            }
                        }

                    }
                    Button2.Visible = false;
                    Button6.Visible = true;
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }

        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Ic',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }
    protected void btnsrch_Click(object sender, EventArgs e)
    {
        srch_Click();
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }


    protected void btnkems_Click(object sender, EventArgs e)
    {
        if (txticno.Text != "")
        {
            DataTable Dt = new DataTable();
            Dt = DBCon.Ora_Execute_table("select mm. mem_new_icno,jp.app_applcn_no from mem_member as mm left join jpa_application as jp on jp.app_new_icno=mm.mem_new_icno  Inner join jpa_application_checklist as ac on ac.che_applcn_no=jp.app_applcn_no  where mm.mem_new_icno='" + txticno.Text + "' and mm.mem_staff_ind='Y' ");
            DataTable Dt2 = new DataTable();
            Dt2 = DBCon.Ora_Execute_table("select mm.mem_new_icno,jp.app_applcn_no from mem_member as mm left join jpa_application as jp on jp.app_new_icno=mm.mem_new_icno where mem_new_icno='" + txticno.Text + "'");
            string appno = Dt2.Rows[0][1].ToString();
            if (appno != "")
            {
                if (Dt.Rows.Count > 0)
                {
                    SqlConnection con = new SqlConnection(cs);
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "staff_cklist_update";
                    cmd.Parameters.Add("@che_applcn_no", SqlDbType.Char).Value = appno;

                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i <= sb.Length; i++)
                    {
                        if (i == 0)
                        {
                            if (cbskpp1.Checked == true)
                            {
                                sb.Append("1");
                            }
                            else
                            {
                                sb.Append("0");
                            }
                        }
                        else if (i == 1)
                        {
                            if (cbskpp2.Checked == true)
                            {
                                sb.Append("1");
                            }
                            else
                            {
                                sb.Append("0");
                            }
                        }
                        else if (i == 2)
                        {
                            if (cbssgp3.Checked == true)
                            {
                                sb.Append("1");
                            }
                            else
                            {
                                sb.Append("0");
                            }
                        }
                        else if (i == 3)
                        {
                            if (cbssgp4.Checked == true)
                            {
                                sb.Append("1");
                            }
                            else
                            {
                                sb.Append("0");
                            }
                        }
                        else if (i == 4)
                        {
                            if (cbpd5.Checked == true)
                            {
                                sb.Append("1");
                            }
                            else
                            {
                                sb.Append("0");
                            }
                        }
                        else if (i == 5)
                        {
                            if (cbbtkh6.Checked == true)
                            {
                                sb.Append("1");
                            }
                            else
                            {
                                sb.Append("0");
                            }
                        }

                    }



                    string values = sb.ToString();
                    cmd.Parameters.Add("@che_staff_supp_doc", SqlDbType.Char).Value = values.ToString();

                    if (rbsm1.Checked == true)
                    {
                        radiobtn1 = "1";
                    }
                    else
                    {
                        radiobtn1 = "0";
                    }
                    cmd.Parameters.Add("@che_muflis_ind", SqlDbType.Char).Value = radiobtn1;

                    if (rbtuu1.Checked == true)
                    {
                        radiobtn2 = "1";
                    }
                    else
                    {
                        radiobtn2 = "0";
                    }
                    cmd.Parameters.AddWithValue("@che_legal_ind", SqlDbType.Char).Value = radiobtn2;
                    if (rbsa1.Checked == true)
                    {
                        radiobtn3 = "1";
                    }
                    else
                    {
                        radiobtn3 = "0";
                    }
                    cmd.Parameters.AddWithValue("@che_active_status_ind", SqlDbType.Char).Value = radiobtn3;

                    cmd.Parameters.Add("@che_muflis_remark", SqlDbType.VarChar).Value = txtjmn.Text;
                    cmd.Parameters.Add("@che_legal_remark", SqlDbType.VarChar).Value = txtjyn.Text;
                    string userid = Session["New"].ToString();
                    cmd.Parameters.AddWithValue("@userid", SqlDbType.VarChar).Value = userid;
                    cmd.Parameters.AddWithValue("@curtdt", SqlDbType.VarChar).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    cmd.Parameters.AddWithValue("@che_appl_sts_ind", SqlDbType.VarChar).Value = "0";


                    String text1 = "030202";
                    String text2 = "SENARAI SEMAK PERMOHONAN (KAKITANGAN)";
                    cmd.Parameters.AddWithValue("aud_crt_id", Session["New"].ToString());
                    cmd.Parameters.AddWithValue("aud_crt_dt", DateTime.Now);
                    cmd.Parameters.AddWithValue("aud_txn_cd", text1);
                    cmd.Parameters.AddWithValue("aud_txn_desc", text2);


                    cmd.Connection = con;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                        Button2.Visible = false;
                        Button6.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        con.Close();
                        con.Dispose();
                        //clear();
                    }

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No KP Baru',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }


    protected void btnsmmit_Click(object sender, EventArgs e)
    {
        if (txticno.Text != "")
        {
            DataTable Dt = new DataTable();
            Dt = DBCon.Ora_Execute_table("select mm. mem_new_icno,jp.app_applcn_no from mem_member as mm left join jpa_application as jp on jp.app_new_icno=mm.mem_new_icno  Inner join jpa_application_checklist as ac on ac.che_applcn_no=jp.app_applcn_no  where mm.mem_new_icno='" + txticno.Text + "' and mm.mem_staff_ind='Y' ");
            DataTable Dt2 = new DataTable();
            Dt2 = DBCon.Ora_Execute_table("select mm.mem_new_icno,jp.app_applcn_no from mem_member as mm left join jpa_application as jp on jp.app_new_icno=mm.mem_new_icno where mem_new_icno='" + txticno.Text + "'");
            string appno = Dt2.Rows[0][1].ToString();
            if (appno != "")
            {
                if (Dt.Rows.Count == 0)
                {

                    SqlConnection con = new SqlConnection(cs);
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "staff_cklist";
                    cmd.Parameters.Add("@che_applcn_no", SqlDbType.Char).Value = appno;

                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i <= sb.Length; i++)
                    {
                        if (i == 0)
                        {
                            if (cbskpp1.Checked == true)
                            {
                                sb.Append("1");
                            }
                            else
                            {
                                sb.Append("0");
                            }
                        }
                        else if (i == 1)
                        {
                            if (cbskpp2.Checked == true)
                            {
                                sb.Append("1");
                            }
                            else
                            {
                                sb.Append("0");
                            }
                        }
                        else if (i == 2)
                        {
                            if (cbssgp3.Checked == true)
                            {
                                sb.Append("1");
                            }
                            else
                            {
                                sb.Append("0");
                            }
                        }
                        else if (i == 3)
                        {
                            if (cbssgp4.Checked == true)
                            {
                                sb.Append("1");
                            }
                            else
                            {
                                sb.Append("0");
                            }
                        }
                        else if (i == 4)
                        {
                            if (cbpd5.Checked == true)
                            {
                                sb.Append("1");
                            }
                            else
                            {
                                sb.Append("0");
                            }
                        }
                        else if (i == 5)
                        {
                            if (cbbtkh6.Checked == true)
                            {
                                sb.Append("1");
                            }
                            else
                            {
                                sb.Append("0");
                            }
                        }

                    }



                    string values = sb.ToString();
                    cmd.Parameters.Add("@che_staff_supp_doc", SqlDbType.Char).Value = values.ToString();

                    if (rbsm1.Checked == true)
                    {
                        radiobtn1 = "1";
                    }
                    else
                    {
                        radiobtn1 = "0";
                    }
                    cmd.Parameters.Add("@che_muflis_ind", SqlDbType.Char).Value = radiobtn1;

                    if (rbtuu1.Checked == true)
                    {
                        radiobtn2 = "1";
                    }
                    else
                    {
                        radiobtn2 = "0";
                    }
                    cmd.Parameters.AddWithValue("@che_legal_ind", SqlDbType.Char).Value = radiobtn2;
                    if (rbsa1.Checked == true)
                    {
                        radiobtn3 = "1";
                    }
                    else
                    {
                        radiobtn3 = "0";
                    }
                    cmd.Parameters.AddWithValue("@che_active_status_ind", SqlDbType.Char).Value = radiobtn3;

                    cmd.Parameters.Add("@che_muflis_remark", SqlDbType.VarChar).Value = txtjmn.Text;
                    cmd.Parameters.Add("@che_legal_remark", SqlDbType.VarChar).Value = txtjyn.Text;
                    string userid = Session["New"].ToString();
                    cmd.Parameters.AddWithValue("@userid", SqlDbType.VarChar).Value = userid;
                    cmd.Parameters.AddWithValue("@curtdt", SqlDbType.VarChar).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    cmd.Parameters.AddWithValue("@che_appl_sts_ind", SqlDbType.VarChar).Value = "0";


                    String text1 = "030202";
                    String text2 = "SENARAI SEMAK PERMOHONAN (KAKITANGAN)";
                    cmd.Parameters.AddWithValue("aud_crt_id", Session["New"].ToString());
                    cmd.Parameters.AddWithValue("aud_crt_dt", DateTime.Now);
                    cmd.Parameters.AddWithValue("aud_txn_cd", text1);
                    cmd.Parameters.AddWithValue("aud_txn_desc", text2);


                    cmd.Connection = con;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                        Button2.Visible = false;
                        Button6.Visible = true;

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        con.Close();
                        con.Dispose();
                        //clear();
                    }
                }
                else
                {
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan');window.location ='PP_cl_staff.aspx';", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No KP Baru',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }

    }

    protected void btnpt_Click(object sender, EventArgs e)
    {
        try
        {

            if (txticno.Text != "")
            {

                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                dt = DBCon.Ora_Execute_table("select app_applcn_no from jpa_application JA where JA.app_new_icno='" + txticno.Text + "'");

                Rptviwer_lt.Reset();
                ds.Tables.Add(dt);

                DataTable sel_mdet = new DataTable();
                sel_mdet = DBCon.Ora_Execute_table("select mem_name,ISNULL(rc.cawangan_name,'') as cawangan_name,mem_centre from mem_member as mm left join ref_cawangan as rc on rc.cawangan_code=mm.mem_branch_cd where mem_new_icno='" + txticno.Text + "' and mm.mem_staff_ind='Y'");

                DataTable sel_chlist = new DataTable();
                sel_chlist = DBCon.Ora_Execute_table("select ac.che_staff_supp_doc,ac.che_muflis_ind,ac.che_muflis_remark,ac.che_legal_ind,ac.che_legal_remark,ac.che_active_status_ind  from jpa_application_checklist as ac  left join jpa_application as ap on ap.app_applcn_no=ac.che_applcn_no  left join mem_member as mm on mm.mem_new_icno=ap.app_new_icno where  ap.app_new_icno='" + txticno.Text + "' and mm.mem_staff_ind='Y' and ac.che_appl_sts_ind!='1'");

                string tv1 = string.Empty, tv2 = string.Empty, tv3 = string.Empty, tv4 = string.Empty, tv5 = string.Empty, tv6 = string.Empty;
                string tt1 = string.Empty, tt2 = string.Empty, tt3 = string.Empty, tt4 = string.Empty, tt5 = string.Empty, tt6 = string.Empty, tt7 = string.Empty, tt8 = string.Empty, tt9 = string.Empty;
                if (sel_chlist.Rows.Count != 0)
                {
                    //string appno = (string)sqlReader2["che_muflis_remark"].ToString();
                    tt1 = sel_chlist.Rows[0]["che_muflis_remark"].ToString();
                    tt2 = sel_chlist.Rows[0]["che_legal_remark"].ToString();

                    string radio1 = sel_chlist.Rows[0]["che_muflis_ind"].ToString();

                    string jar1 = radio1.ToString();

                    if (jar1 == "True")
                    {
                        tt3 = "MUFLIS";
                    }
                    else
                    {
                        tt3 = "TIDAK MUFLIS";
                    }

                    string radio2 = sel_chlist.Rows[0]["che_legal_ind"].ToString();

                    string bar2 = radio2.ToString();

                    if (bar2 == "True")
                    {
                        tt4 = "YA";
                    }
                    else
                    {
                        tt4 = "TIDAK";
                    }

                    string radio3 = sel_chlist.Rows[0]["che_active_status_ind"].ToString();

                    string bar3 = radio3.ToString();

                    if (bar3 == "True")
                    {
                        tt5 = "AKTIF";
                    }
                    else
                    {
                        tt5 = "TIDAK AKTIF";
                    }

                    string readText1 = sel_chlist.Rows[0]["che_staff_supp_doc"].ToString();

                    string bar1 = readText1.ToString();
                    for (int i = 0; i <= readText1.Length; i++)
                    {
                        if (i == 0)
                        {
                            if (bar1.Substring(0, 1) == "1")
                            {
                                tv1 = "YA";
                            }
                            else
                            {
                                tv1 = "TIDAK";
                            }
                        }
                        else if (i == 1)
                        {
                            if (bar1.Substring(1, 1) == "1")
                            {
                                tv2 = "YA";
                            }
                            else
                            {
                                tv2 = "TIDAK";
                            }
                        }
                        else if (i == 2)
                        {
                            if (bar1.Substring(2, 1) == "1")
                            {
                                tv3 = "YA";
                            }
                            else
                            {
                                tv3 = "TIDAK";
                            }
                        }
                        else if (i == 3)
                        {
                            if (bar1.Substring(3, 1) == "1")
                            {
                                tv4 = "YA";
                            }
                            else
                            {
                                tv4 = "TIDAK";
                            }
                        }
                        else if (i == 4)
                        {
                            if (bar1.Substring(4, 1) == "1")
                            {
                                tv5 = "YA";
                            }
                            else
                            {
                                tv5 = "TIDAK";
                            }
                        }
                        else if (i == 5)
                        {
                            if (bar1.Substring(5, 1) == "1")
                            {
                                tv6 = "YA";
                            }
                            else
                            {
                                tv6 = "TIDAK";
                            }
                        }
                    }
                }
                Rptviwer_lt.LocalReport.DataSources.Clear();

                Rptviwer_lt.LocalReport.ReportPath = "Pelaburan_Anggota/pa_ssp_staff.rdlc";
                ReportDataSource rds = new ReportDataSource("pa_ssp_s", dt);

                ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("no_kp",txticno.Text),
                     new ReportParameter("appno",dt.Rows[0]["app_applcn_no"].ToString()),
                     new ReportParameter("nama",sel_mdet.Rows[0]["mem_name"].ToString()),
                     new ReportParameter("pejabat",sel_mdet.Rows[0]["cawangan_name"].ToString()),
                     new ReportParameter("pusat",sel_mdet.Rows[0]["mem_centre"].ToString()),

                     new ReportParameter("cc1",tv1),
                     new ReportParameter("cc2",tv2),
                     new ReportParameter("cc3",tv3),
                     new ReportParameter("cc4",tv4),
                     new ReportParameter("cc5",tv5),
                     new ReportParameter("cc6",tv6),

                     new ReportParameter("cc7",tt1),
                     new ReportParameter("cc8",tt2),
                     new ReportParameter("cc9",tt3),
                     new ReportParameter("cc10",tt4),
                     new ReportParameter("cc11",tt5),
                     new ReportParameter("cc12",tt6)


                     };


                Rptviwer_lt.LocalReport.SetParameters(rptParams);

                Rptviwer_lt.LocalReport.DataSources.Add(rds);

                //Refresh
                Rptviwer_lt.LocalReport.Refresh();
                Warning[] warnings;

                string[] streamids;

                string mimeType;

                string encoding;

                string extension;

                string fname = DateTime.Now.ToString("dd_MM_yyyy");

                string devinfo = "   <DeviceInfo><ColorDepth>32</ColorDepth><DpiX>350</DpiX><DpiY>350</DpiY><OutputFormat>PDF</OutputFormat>" +
                                "   <PageWidth>12.20in</PageWidth>" +
                                "   <PageHeight>8.27in</PageHeight>" +
                                "   <MarginTop>0.1in</MarginTop>" +
                                "   <MarginLeft>0.5in</MarginLeft>" +
                                "   <MarginRight>0in</MarginRight>" +
                                "   <MarginBottom>0in</MarginBottom>" +
                                "   </DeviceInfo>";

                byte[] bytes = Rptviwer_lt.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);

                Response.Buffer = true;

                Response.Clear();

                Response.ClearHeaders();

                Response.ClearContent();

                Response.ContentType = "application/pdf";

                Response.AddHeader("content-disposition", "attachment; filename=SENARAI_SEMAK_PERMOHONAN_KAKITANGAN_" + txticno.Text + "." + extension);

                Response.BinaryWrite(bytes);

                Response.Flush();

                //Response.End();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

   
    protected void clk_bak(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Pelaburan_Anggota/PP_cl_staff_view.aspx");
    }

}