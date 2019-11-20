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

public partial class PP_cl_shahabat : System.Web.UI.Page
{
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    //SqlCommand com;
    DataTable dt = new DataTable();
    StudentWebService service = new StudentWebService();
    string Status = string.Empty;
    string level, userid;
    string radiobtn1, radiobtn2, radiobtn3, radiobtn4, radiobtn5;

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Button6);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                level = Session["level"].ToString();
                userid = Session["New"].ToString();
                Button1.Visible = false;
                txtname.Attributes.Add("Readonly", "Readonly");
                txtcaw.Attributes.Add("Readonly", "Readonly");
                txtpust.Attributes.Add("Readonly", "Readonly");
                var samp = Request.Url.Query;
                if (samp != "")
                {
                    txticno.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    srch_Click();
                    Button4.Visible = false;
                    Button5.Visible = false;
                    Button7.Visible = true;
                    btnreset.Visible = false;
                }
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
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
                string query1 = "select mm.mem_name,ISNULL(rc.cawangan_name,'') as cawangan_name,mm.mem_centre,mm.mem_sts_cd from mem_member as mm left join ref_cawangan as rc on rc.cawangan_code=mm.mem_branch_cd where mem_new_icno='" + txticno.Text + "' and mm.mem_staff_ind='N'";
                var sqlCommand1 = new SqlCommand(query1, con);
                var sqlReader1 = sqlCommand1.ExecuteReader();
                if (sqlReader1.Read() == true)
                {
                    txtname.Text = (string)sqlReader1["mem_name"];
                    txtcaw.Text = (string)sqlReader1["cawangan_name"];
                    txtpust.Text = (string)sqlReader1["mem_centre"];
                    if ((string)sqlReader1["mem_sts_cd"] == "SA")
                    {
                        cbaksya.Checked = true;
                    }
                    else
                    {
                        cbaksya.Checked = false;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
                con.Close();


                //check box tick condition


                con.Open();
                string query2 = "select ISNULL(ac.che_share_amt,'') as che_share_amt,ac.che_aim_atttendance,ac.che_meeting_bu,ISNULL(ac.che_income_amt,'') che_income_amt,ISNULL(ac.che_qualify,'') che_qualify,ISNULL(ac.che_supp_doc,'') che_supp_doc,ac.che_muflis_ind,ac.che_legal_ind,ap.app_applcn_no,ac.che_legal_remark,ac.che_muflis_remark,ac.che_active_status_ind,ac.che_aim_loan_ind,ac.che_koop_loan_ind,ac.che_inst_name1,ISNULL(ac.che_inst_loan_amt1,'') che_inst_loan_amt1,ISNULL(ac.che_inst_monthly_amt1,'') che_inst_monthly_amt1,ISNULL(ac.che_inst_name2,'') che_inst_name2,ISNULL(ac.che_inst_loan_amt2,'') che_inst_loan_amt2,ISNULL(ac.che_inst_monthly_amt2,'') che_inst_monthly_amt2,ISNULL(ac.che_inst_name3,'') che_inst_name3,ISNULL(ac.che_inst_loan_amt3,'') che_inst_loan_amt3,ISNULL(ac.che_inst_monthly_amt3,'') che_inst_monthly_amt3 from jpa_application_checklist as ac  left join jpa_application as ap on ap.app_applcn_no=ac.che_applcn_no  left join mem_member as mm on mm.mem_new_icno=ap.app_new_icno where  ap.app_new_icno='" + txticno.Text + "' and mm.mem_staff_ind='N' and ac.che_appl_sts_ind!='1'";
                var sqlCommand2 = new SqlCommand(query2, con);
                var sqlReader2 = sqlCommand2.ExecuteReader();
                if (sqlReader2.Read() == true)
                {
                    string appno = (string)sqlReader2["app_applcn_no"].ToString();
                    decimal t1 = (decimal)sqlReader2["che_share_amt"];
                    txttms.Text = t1.ToString("0.00");
                    txtatten.Text = (string)sqlReader2["che_aim_atttendance"].ToString();
                    txtmeet.Text = (string)sqlReader2["che_meeting_bu"].ToString();
                    decimal t2 = (decimal)sqlReader2["che_income_amt"];
                    txtincom.Text = t2.ToString("0.00");

                    txtjmn.Text = (string)sqlReader2["che_muflis_remark"].ToString();
                    txtjyn.Text = (string)sqlReader2["che_legal_remark"].ToString();

                    string radio1 = (string)sqlReader2["che_muflis_ind"].ToString();

                    string jar1 = radio1.ToString();

                    if (jar1 == "True")
                    {
                        radiostssmm.Checked = true;
                    }
                    else if (jar1 == "False")
                    {
                        radiostssmtm.Checked = true;
                    }


                    string radio2 = (string)sqlReader2["che_legal_ind"].ToString();

                    string bar2 = radio2.ToString();

                    if (bar2 == "True")
                    {
                        radiotuuy.Checked = true;
                    }
                    else if (bar2 == "False")
                    {
                        radiotuut.Checked = true;
                    }


                    string radio3 = (string)sqlReader2["che_active_status_ind"].ToString();

                    string bar3 = radio3.ToString();

                    if (bar3 == "True")
                    {
                        radiosaa.Checked = true;
                    }
                    else if (bar3 == "False")
                    {
                        radiostsata.Checked = true;
                    }


                    string radio4 = (string)sqlReader2["che_aim_loan_ind"].ToString();

                    string bar4 = radio4.ToString();

                    if (bar4 == "True")
                    {
                        radiosaimy.Checked = true;
                    }
                    else if (bar4 == "False")
                    {

                        radiosaimta1.Checked = true;
                    }


                    string radio5 = (string)sqlReader2["che_koop_loan_ind"].ToString();

                    string bar5 = radio5.ToString();

                    if (bar5 == "True")
                    {
                        radioaks.Checked = true;
                    }
                    else if (bar5 == "False")
                    {
                        radioakta.Checked = true;
                    }



                    string readText = (string)sqlReader2["che_qualify"];

                    string bar = readText.ToString();

                    for (int i = 0; i <= readText.Length; i++)
                    {
                        if (i == 0)
                        {
                            if (bar.Substring(0, 1) == "1")
                            {
                                cbbtk.Checked = true;
                            }
                            else
                            {
                                cbbtk.Checked = false;
                            }
                        }
                        else if (i == 1)
                        {
                            if (bar.Substring(1, 1) == "1")
                            {
                                cbtdm.Checked = true;
                            }
                            else
                            {
                                cbtdm.Checked = false;
                            }
                        }
                        else if (i == 2)
                        {
                            if (bar.Substring(2, 1) == "1")
                            {
                                cbaksya.Checked = true;
                            }
                            else
                            {
                                cbaksya.Checked = false;
                            }
                        }
                        else if (i == 3)
                        {
                            if (bar.Substring(3, 1) == "1")
                            {
                                cbmpac.Checked = true;
                            }
                            else
                            {
                                cbmpac.Checked = false;
                            }
                        }
                        else if (i == 4)
                        {
                            if (bar.Substring(4, 1) == "1")
                            {
                                cbmdp.Checked = true;
                            }
                            else
                            {
                                cbmdp.Checked = false;
                            }
                        }
                        else if (i == 5)
                        {
                            if (bar.Substring(5, 1) == "1")
                            {
                                cbtmpm.Checked = true;
                            }
                            else
                            {
                                cbtmpm.Checked = false;
                            }
                        }
                        else if (i == 6)
                        {
                            if (bar.Substring(6, 1) == "1")
                            {
                                cbmapy.Checked = true;
                            }
                            else
                            {
                                cbmapy.Checked = false;
                            }
                        }
                    }

                    string readText1 = (string)sqlReader2["che_supp_doc"];

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
                                cbskpjp2.Checked = true;
                            }
                            else
                            {
                                cbskpjp2.Checked = false;
                            }
                        }
                        else if (i == 2)
                        {
                            if (bar1.Substring(2, 1) == "1")
                            {
                                cbskppkt3.Checked = true;
                            }
                            else
                            {
                                cbskppkt3.Checked = false;
                            }
                        }
                        else if (i == 3)
                        {
                            if (bar1.Substring(3, 1) == "1")
                            {
                                cbgpbp4.Checked = true;
                            }
                            else
                            {
                                cbgpbp4.Checked = false;
                            }
                        }
                        else if (i == 4)
                        {
                            if (bar1.Substring(4, 1) == "1")
                            {
                                cbbtkh5.Checked = true;
                            }
                            else
                            {
                                cbbtkh5.Checked = false;
                            }
                        }
                        else if (i == 5)
                        {
                            if (bar1.Substring(5, 1) == "1")
                            {
                                cbspp6.Checked = true;
                            }
                            else
                            {
                                cbspp6.Checked = false;
                            }
                        }
                        else if (i == 6)
                        {
                            if (bar1.Substring(6, 1) == "1")
                            {
                                cbslpbt7.Checked = true;
                            }
                            else
                            {
                                cbslpbt7.Checked = false;
                            }
                        }
                        else if (i == 7)
                        {
                            if (bar1.Substring(7, 1) == "1")
                            {
                                cbsmdbb8.Checked = true;
                            }
                            else
                            {
                                cbsmdbb8.Checked = false;
                            }
                        }
                        else if (i == 8)
                        {
                            if (bar1.Substring(8, 1) == "1")
                            {
                                cbggltdk9.Checked = true;
                            }
                            else
                            {
                                cbggltdk9.Checked = false;
                            }
                        }
                        else if (i == 9)
                        {
                            if (bar1.Substring(9, 1) == "1")
                            {
                                cbpltdk10.Checked = true;
                            }
                            else
                            {
                                cbpltdk10.Checked = false;
                            }
                        }
                        else if (i == 10)
                        {
                            if (bar1.Substring(10, 1) == "1")
                            {
                                cbspsbk11.Checked = true;
                            }
                            else
                            {
                                cbspsbk11.Checked = false;
                            }
                        }
                        else if (i == 11)
                        {
                            if (bar1.Substring(11, 1) == "1")
                            {
                                cbsbuanp12.Checked = true;
                            }
                            else
                            {
                                cbsbuanp12.Checked = false;
                            }
                        }
                        else if (i == 12)
                        {
                            if (bar1.Substring(12, 1) == "1")
                            {
                                cbbpm13.Checked = true;
                            }
                            else
                            {
                                cbbpm13.Checked = false;
                            }
                        }
                        else if (i == 13)
                        {
                            if (bar1.Substring(13, 1) == "1")
                            {
                                cbshp14.Checked = true;
                            }
                            else
                            {
                                cbshp14.Checked = false;
                            }
                        }
                        else if (i == 14)
                        {
                            if (bar1.Substring(14, 1) == "1")
                            {
                                cbsgc15.Checked = true;
                            }
                            else
                            {
                                cbsgc15.Checked = false;
                            }
                        }
                        else if (i == 15)
                        {
                            if (bar1.Substring(15, 1) == "1")
                            {
                                cbbpspb16.Checked = true;
                            }
                            else
                            {
                                cbbpspb16.Checked = false;
                            }
                        }
                        else if (i == 16)
                        {
                            if (bar1.Substring(16, 1) == "1")
                            {
                                cbrkdpl17.Checked = true;
                            }
                            else
                            {
                                cbrkdpl17.Checked = false;
                            }
                        }
                        else if (i == 17)
                        {
                            if (bar1.Substring(17, 1) == "1")
                            {
                                cbrbbpta18.Checked = true;
                            }
                            else
                            {
                                cbrbbpta18.Checked = false;
                            }
                        }
                        else if (i == 18)
                        {
                            if (bar1.Substring(18, 1) == "1")
                            {
                                cbrpmfyd19.Checked = true;
                            }
                            else
                            {
                                cbrpmfyd19.Checked = false;
                            }
                        }
                        else if (i == 19)
                        {
                            if (bar1.Substring(19, 1) == "1")
                            {
                                cbtmakdjs20.Checked = true;
                            }
                            else
                            {
                                cbtmakdjs20.Checked = false;
                            }
                        }
                        else if (i == 20)
                        {
                            if (bar1.Substring(20, 1) == "1")
                            {
                                cbssgbp21.Checked = true;
                            }
                            else
                            {
                                cbssgbp21.Checked = false;
                            }
                        }
                    }
                    txtnik1.Text = (string)sqlReader2["che_inst_name1"].ToString();
                    //txtjp1.Text = (string)sqlReader2["che_inst_loan_amt1"].ToString();
                    decimal txtjp1amt = (decimal)sqlReader2["che_inst_loan_amt1"];
                    txtjp1.Text = txtjp1amt.ToString("0.00");
                    decimal txtab1amt = (decimal)sqlReader2["che_inst_monthly_amt1"];
                    txtab1.Text = txtab1amt.ToString("0.00");
                    //txtab1.Text = (string)sqlReader2["che_inst_monthly_amt1"].ToString();
                    txtnik2.Text = (string)sqlReader2["che_inst_name2"].ToString();
                    decimal txtnik2amt = (decimal)sqlReader2["che_inst_loan_amt2"];
                    txtjp2.Text = txtnik2amt.ToString("0.00");
                    decimal txtab2amt = (decimal)sqlReader2["che_inst_monthly_amt2"];
                    txtab2.Text = txtab2amt.ToString("0.00");
                    txtnik3.Text = (string)sqlReader2["che_inst_name3"].ToString();
                    decimal txtjp3amt = (decimal)sqlReader2["che_inst_loan_amt3"];
                    txtjp3.Text = txtjp3amt.ToString("0.00");
                    decimal txtab3amt = (decimal)sqlReader2["che_inst_monthly_amt3"];
                    txtab3.Text = txtab3amt.ToString("0.00");

                    Button2.Visible = false;
                    //btnreset.Visible = false;
                    //Button3.Visible = false;
                    Button1.Visible = true;

                }
                con.Close();
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
    void reset()
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        reset();
    }

    protected void btnkems_Click(object sender, EventArgs e)
    {
        if (txticno.Text != "")
        {
            DataTable Dt = new DataTable();
            Dt = DBCon.Ora_Execute_table("select mm. mem_new_icno,jp.app_applcn_no from mem_member as mm left join jpa_application as jp on jp.app_new_icno=mm.mem_new_icno  Inner join jpa_application_checklist as ac on ac.che_applcn_no=jp.app_applcn_no  where mem_new_icno='" + txticno.Text + "'");
            DataTable Dt2 = new DataTable();
            Dt2 = DBCon.Ora_Execute_table("select mm.mem_new_icno,jp.app_applcn_no from mem_member as mm left join jpa_application as jp on jp.app_new_icno=mm.mem_new_icno where mem_new_icno='" + txticno.Text + "'");
            string appno = Dt2.Rows[0][1].ToString();
            if (appno != "")
            {
                if (Dt.Rows.Count > 0)
                {
                    SqlCommand cmdup = new SqlCommand();
                    cmdup.CommandType = CommandType.StoredProcedure;
                    cmdup.CommandText = "sahabt_cklist_update";
                    cmdup.Parameters.Add("@che_applcn_no", SqlDbType.Char).Value = appno;

                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i <= sb.Length; i++)
                    {
                        if (i == 0)
                        {
                            if (cbbtk.Checked == true)
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
                            if (cbtdm.Checked == true)
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
                            if (cbaksya.Checked == true)
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
                            if (cbmpac.Checked == true)
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
                            if (cbmdp.Checked == true)
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
                            if (cbtmpm.Checked == true)
                            {
                                sb.Append("1");
                            }
                            else
                            {
                                sb.Append("0");
                            }
                        }
                        else if (i == 6)
                        {
                            if (cbmapy.Checked == true)
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
                    cmdup.Parameters.Add("@che_qualify", SqlDbType.Char).Value = values.ToString();
                    cmdup.Parameters.Add("@che_share_amt", SqlDbType.Money).Value = txttms.Text;
                    cmdup.Parameters.Add("@che_aim_atttendance", SqlDbType.Int).Value = txtatten.Text;
                    cmdup.Parameters.Add("@che_meeting_bu", SqlDbType.Int).Value = txtmeet.Text;
                    cmdup.Parameters.Add("@che_income_amt", SqlDbType.Money).Value = txtincom.Text;
                    cmdup.Parameters.Add("@che_muflis_remark", SqlDbType.VarChar).Value = txtjmn.Text;
                    cmdup.Parameters.Add("@che_legal_remark", SqlDbType.VarChar).Value = txtjyn.Text;
                    cmdup.Parameters.Add("@che_appl_sts_ind", SqlDbType.VarChar).Value = "0";

                    if (radiostssmm.Checked == true)
                    {
                        radiobtn1 = "1";
                    }
                    else if (radiostssmtm.Checked == true)
                    {
                        radiobtn1 = "0";
                    }
                    else
                    {
                        radiobtn1 = "";
                    }
                    cmdup.Parameters.Add("@che_muflis_ind", SqlDbType.Char).Value = radiobtn1;

                    if (radiotuuy.Checked == true)
                    {
                        radiobtn2 = "1";
                    }
                    else if (radiotuut.Checked == true)
                    {
                        radiobtn2 = "0";
                    }
                    else
                    {
                        radiobtn2 = "";
                    }
                    cmdup.Parameters.AddWithValue("@che_legal_ind", SqlDbType.Char).Value = radiobtn2;
                    if (radiosaa.Checked == true)
                    {
                        radiobtn3 = "1";
                    }
                    else if (radiostsata.Checked == true)
                    {
                        radiobtn3 = "0";
                    }
                    else
                    {
                        radiobtn3 = "";
                    }
                    cmdup.Parameters.AddWithValue("@che_active_status_ind", SqlDbType.Char).Value = radiobtn3;

                    if (radiosaimy.Checked == true)
                    {
                        radiobtn4 = "1";
                    }
                    else if (radiosaimta1.Checked == true)
                    {
                        radiobtn4 = "0";
                    }
                    else
                    {
                        radiobtn4 = "1";
                    }
                    cmdup.Parameters.AddWithValue("@che_aim_loan_ind", SqlDbType.Char).Value = radiobtn4;

                    if (radioaks.Checked == true)
                    {
                        radiobtn5 = "1";
                    }
                    else if (radioakta.Checked == true)
                    {
                        radiobtn5 = "0";
                    }
                    else
                    {
                        radiobtn5 = "";
                    }

                    cmdup.Parameters.AddWithValue("@che_koop_loan_ind", SqlDbType.Char).Value = radiobtn5;

                    cmdup.Parameters.AddWithValue("@che_inst_name1", SqlDbType.VarChar).Value = txtnik1.Text;
                    cmdup.Parameters.AddWithValue("@che_inst_loan_amt1", SqlDbType.Money).Value = txtjp1.Text.Trim();
                    cmdup.Parameters.AddWithValue("@che_inst_monthly_amt1", SqlDbType.Money).Value = txtab1.Text.Trim();
                    cmdup.Parameters.AddWithValue("@che_inst_name2", SqlDbType.VarChar).Value = txtnik2.Text;
                    cmdup.Parameters.AddWithValue("@che_inst_loan_amt2", SqlDbType.Money).Value = txtjp2.Text.Trim();
                    cmdup.Parameters.AddWithValue("@che_inst_monthly_amt2", SqlDbType.Money).Value = txtab2.Text.Trim();
                    cmdup.Parameters.AddWithValue("@che_inst_name3", SqlDbType.VarChar).Value = txtnik3.Text;
                    cmdup.Parameters.AddWithValue("@che_inst_loan_amt3", SqlDbType.Money).Value = txtjp3.Text.Trim();
                    cmdup.Parameters.AddWithValue("@che_inst_monthly_amt3", SqlDbType.Money).Value = txtab3.Text.Trim();





                    // Need to change the check box name

                    StringBuilder sb1 = new StringBuilder();
                    for (int i = 0; i <= sb1.Length; i++)
                    {
                        if (i == 0)
                        {
                            if (cbskpp1.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 1)
                        {
                            if (cbskpjp2.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 2)
                        {
                            if (cbskppkt3.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 3)
                        {
                            if (cbgpbp4.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 4)
                        {
                            if (cbbtkh5.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 5)
                        {
                            if (cbspp6.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 6)
                        {
                            if (cbslpbt7.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 7)
                        {
                            if (cbsmdbb8.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 8)
                        {
                            if (cbggltdk9.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 9)
                        {
                            if (cbpltdk10.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 10)
                        {
                            if (cbspsbk11.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 11)
                        {
                            if (cbsbuanp12.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 12)
                        {
                            if (cbbpm13.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 13)
                        {
                            if (cbshp14.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 14)
                        {
                            if (cbsgc15.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 15)
                        {
                            if (cbbpspb16.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 16)
                        {
                            if (cbrkdpl17.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 17)
                        {
                            if (cbrbbpta18.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 18)
                        {
                            if (cbrpmfyd19.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 19)
                        {
                            if (cbtmakdjs20.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 20)
                        {
                            if (cbssgbp21.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                    }
                    string values1 = sb1.ToString();
                    cmdup.Parameters.AddWithValue("@che_supp_doc", SqlDbType.Char).Value = values1.ToString();
                    string userid = Session["New"].ToString();
                    cmdup.Parameters.AddWithValue("@userid", SqlDbType.VarChar).Value = userid;
                    cmdup.Parameters.AddWithValue("@curtdt", SqlDbType.VarChar).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    String text1 = "030201";
                    String text2 = "SENARAI SEMAK PERMOHONAN (ANGGOTA)";
                    cmdup.Parameters.AddWithValue("aud_crt_id", Session["New"].ToString());
                    cmdup.Parameters.AddWithValue("aud_crt_dt", DateTime.Now);
                    cmdup.Parameters.AddWithValue("aud_txn_cd", text1);
                    cmdup.Parameters.AddWithValue("aud_txn_desc", text2);



                    cmdup.Connection = con1;
                    try
                    {
                        con1.Open();
                        cmdup.ExecuteNonQuery();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                        Button1.Visible = true;
                        Button2.Visible = false;

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        con1.Close();
                        con1.Dispose();
                        //clear();
                    }

                }
                else
                {
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    protected void btnsmmit_Click(object sender, EventArgs e)
    {
        if (txticno.Text != "")
        {
            DataTable Dt = new DataTable();
            Dt = DBCon.Ora_Execute_table("select mm. mem_new_icno,jp.app_applcn_no from mem_member as mm left join jpa_application as jp on jp.app_new_icno=mm.mem_new_icno  Inner join jpa_application_checklist as ac on ac.che_applcn_no=jp.app_applcn_no  where mem_new_icno='" + txticno.Text + "'");
            DataTable Dt2 = new DataTable();
            Dt2 = DBCon.Ora_Execute_table("select mm.mem_new_icno,jp.app_applcn_no from mem_member as mm left join jpa_application as jp on jp.app_new_icno=mm.mem_new_icno where mem_new_icno='" + txticno.Text + "'");
            string appno = Dt2.Rows[0][1].ToString();
            if (appno != "")
            {
                if (Dt.Rows.Count == 0)
                {

                    //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Record is already esisting);", true);
                    SqlConnection con = new SqlConnection(cs);
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sahabt_cklist";
                    cmd.Parameters.Add("@che_applcn_no", SqlDbType.Char).Value = appno;


                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i <= sb.Length; i++)
                    {
                        if (i == 0)
                        {
                            if (cbbtk.Checked == true)
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
                            if (cbtdm.Checked == true)
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
                            if (cbaksya.Checked == true)
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
                            if (cbmpac.Checked == true)
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
                            if (cbmdp.Checked == true)
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
                            if (cbtmpm.Checked == true)
                            {
                                sb.Append("1");
                            }
                            else
                            {
                                sb.Append("0");
                            }
                        }
                        else if (i == 6)
                        {
                            if (cbmapy.Checked == true)
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
                    cmd.Parameters.Add("@che_qualify", SqlDbType.Char).Value = values.ToString();
                    string tms;
                    if (txttms.Text == "")
                    {
                        tms = "0.00";
                    }
                    else
                    {
                        tms = txttms.Text;
                    }
                    string incom;
                    if (txtincom.Text == "")
                    {
                        incom = "0.00";
                    }
                    else
                    {
                        incom = txtincom.Text;
                    }
                    cmd.Parameters.Add("@che_share_amt", SqlDbType.Money).Value = tms;
                    string atten;
                    string meet;
                    if (txtatten.Text == "")
                    {
                        atten = "0";
                    }
                    else
                    {
                        atten = txtatten.Text;
                    }

                    if (txtmeet.Text == "")
                    {
                        meet = "0";
                    }
                    else
                    {
                        meet = txtmeet.Text;
                    }
                    cmd.Parameters.Add("@che_aim_atttendance", SqlDbType.Int).Value = atten;
                    cmd.Parameters.Add("@che_meeting_bu", SqlDbType.Int).Value = meet;
                    cmd.Parameters.Add("@che_income_amt", SqlDbType.Money).Value = incom;
                    cmd.Parameters.Add("@che_muflis_remark", SqlDbType.VarChar).Value = txtjmn.Text;
                    cmd.Parameters.Add("@che_legal_remark", SqlDbType.VarChar).Value = txtjyn.Text;
                    cmd.Parameters.Add("@che_appl_sts_ind", SqlDbType.VarChar).Value = "0";

                    if (radiostssmm.Checked == true)
                    {
                        radiobtn1 = "1";
                    }
                    else if (radiostssmtm.Checked == true)
                    {
                        radiobtn1 = "0";
                    }
                    else
                    {
                        radiobtn1 = "";
                    }
                    cmd.Parameters.Add("@che_muflis_ind", SqlDbType.VarChar).Value = radiobtn1;

                    if (radiotuuy.Checked == true)
                    {
                        radiobtn2 = "1";
                    }
                    else if (radiotuut.Checked == true)
                    {
                        radiobtn2 = "0";
                    }
                    else
                    {
                        radiobtn2 = "";
                    }
                    cmd.Parameters.AddWithValue("@che_legal_ind", SqlDbType.VarChar).Value = radiobtn2;
                    if (radiosaa.Checked == true)
                    {
                        radiobtn3 = "1";
                    }
                    else if (radiostsata.Checked == true)
                    {
                        radiobtn3 = "0";
                    }
                    else
                    {
                        radiobtn3 = "";
                    }
                    cmd.Parameters.AddWithValue("@che_active_status_ind", SqlDbType.VarChar).Value = radiobtn3;

                    if (radiosaimy.Checked == true)
                    {
                        radiobtn4 = "1";
                    }
                    else if (radiosaimta1.Checked == true)
                    {
                        radiobtn4 = "0";
                    }
                    else
                    {
                        radiobtn4 = "";
                    }
                    cmd.Parameters.AddWithValue("@che_aim_loan_ind", SqlDbType.VarChar).Value = radiobtn4;

                    if (radioaks.Checked == true)
                    {
                        radiobtn5 = "1";
                    }
                    else if (radioakta.Checked == true)
                    {
                        radiobtn5 = "0";
                    }
                    else
                    {
                        radiobtn5 = "";
                    }

                    cmd.Parameters.AddWithValue("@che_koop_loan_ind", SqlDbType.VarChar).Value = radiobtn5;

                    cmd.Parameters.AddWithValue("@che_inst_name1", SqlDbType.VarChar).Value = txtnik1.Text;
                    string jp1;
                    string jp2;
                    string jp3;
                    string tab1;
                    string tab2;
                    string tab3;
                    if (txtjp1.Text == "")
                    {
                        jp1 = "0.00";
                    }
                    else
                    {
                        jp1 = txtjp1.Text;
                    }

                    if (txtab1.Text == "")
                    {
                        tab1 = "0.00";
                    }
                    else
                    {
                        tab1 = txtab1.Text;
                    }

                    if (txtjp2.Text == "")
                    {
                        jp2 = "0.00";
                    }
                    else
                    {
                        jp2 = txtjp2.Text;
                    }

                    if (txtab2.Text == "")
                    {
                        tab2 = "0.00";
                    }
                    else
                    {
                        tab2 = txtab2.Text;
                    }
                    if (txtab3.Text == "")
                    {
                        tab3 = "0.00";
                    }
                    else
                    {
                        tab3 = txtab3.Text;
                    }

                    if (txtjp3.Text == "")
                    {
                        jp3 = "0.00";
                    }
                    else
                    {
                        jp3 = txtjp3.Text;
                    }

                    cmd.Parameters.AddWithValue("@che_inst_loan_amt1", SqlDbType.Money).Value = jp1;
                    cmd.Parameters.AddWithValue("@che_inst_monthly_amt1", SqlDbType.Money).Value = tab1;
                    cmd.Parameters.AddWithValue("@che_inst_name2", SqlDbType.VarChar).Value = txtnik2.Text;
                    cmd.Parameters.AddWithValue("@che_inst_loan_amt2", SqlDbType.Money).Value = jp2;
                    cmd.Parameters.AddWithValue("@che_inst_monthly_amt2", SqlDbType.Money).Value = tab2;
                    cmd.Parameters.AddWithValue("@che_inst_name3", SqlDbType.VarChar).Value = txtnik3.Text;
                    cmd.Parameters.AddWithValue("@che_inst_loan_amt3", SqlDbType.Money).Value = jp3;
                    cmd.Parameters.AddWithValue("@che_inst_monthly_amt3", SqlDbType.Money).Value = tab3;





                    // Need to change the check box name

                    StringBuilder sb1 = new StringBuilder();
                    for (int i = 0; i <= sb1.Length; i++)
                    {
                        if (i == 0)
                        {
                            if (cbskpp1.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 1)
                        {
                            if (cbskpjp2.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 2)
                        {
                            if (cbskppkt3.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 3)
                        {
                            if (cbgpbp4.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 4)
                        {
                            if (cbbtkh5.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 5)
                        {
                            if (cbspp6.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 6)
                        {
                            if (cbslpbt7.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 7)
                        {
                            if (cbsmdbb8.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 8)
                        {
                            if (cbggltdk9.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 9)
                        {
                            if (cbpltdk10.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 10)
                        {
                            if (cbspsbk11.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 11)
                        {
                            if (cbsbuanp12.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 12)
                        {
                            if (cbbpm13.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 13)
                        {
                            if (cbshp14.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 14)
                        {
                            if (cbsgc15.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 15)
                        {
                            if (cbbpspb16.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 16)
                        {
                            if (cbrkdpl17.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 17)
                        {
                            if (cbrbbpta18.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 18)
                        {
                            if (cbrpmfyd19.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 19)
                        {
                            if (cbtmakdjs20.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                        else if (i == 20)
                        {
                            if (cbssgbp21.Checked == true)
                            {
                                sb1.Append("1");
                            }
                            else
                            {
                                sb1.Append("0");
                            }
                        }
                    }
                    string values1 = sb1.ToString();
                    cmd.Parameters.AddWithValue("@che_supp_doc", SqlDbType.Char).Value = values1.ToString();
                    string userid = Session["New"].ToString();
                    cmd.Parameters.AddWithValue("@userid", SqlDbType.VarChar).Value = userid;
                    cmd.Parameters.AddWithValue("@curtdt", SqlDbType.VarChar).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    String text1 = "030201";
                    String text2 = "SENARAI SEMAK PERMOHONAN (ANGGOTA)";
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
                        Button1.Visible = true;
                        Button2.Visible = false;

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
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
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
                sel_mdet = DBCon.Ora_Execute_table("select mem_name,rc.cawangan_name,mem_centre from mem_member as mm left join ref_cawangan as rc on rc.cawangan_code=mm.mem_branch_cd where mem_new_icno='" + txticno.Text + "' and mm.mem_staff_ind='N'");

                DataTable sel_chlist = new DataTable();
                sel_chlist = DBCon.Ora_Execute_table("select ac.che_share_amt,ac.che_aim_atttendance,ac.che_meeting_bu,ac.che_income_amt,ac.che_qualify,ac.che_supp_doc,ac.che_muflis_ind,ac.che_legal_ind,ap.app_applcn_no,ac.che_legal_remark,ac.che_muflis_remark,ac.che_active_status_ind,ac.che_aim_loan_ind,ac.che_koop_loan_ind,ac.che_inst_name1,ac.che_inst_loan_amt1,ac.che_inst_monthly_amt1,ac.che_inst_name2,ac.che_inst_loan_amt2,ac.che_inst_monthly_amt2,ac.che_inst_name3,ac.che_inst_loan_amt3,ac.che_inst_monthly_amt3 from jpa_application_checklist as ac  left join jpa_application as ap on ap.app_applcn_no=ac.che_applcn_no  left join mem_member as mm on mm.mem_new_icno=ap.app_new_icno where  ap.app_new_icno='" + txticno.Text + "' and mm.mem_staff_ind='N' and ac.che_appl_sts_ind!='1'");

                string vc1 = string.Empty, vc2 = string.Empty, vc3 = string.Empty, vc4 = string.Empty, vc5 = string.Empty, vc6 = string.Empty, vc7 = string.Empty, vc8 = string.Empty, vc9 = string.Empty, vc10 = string.Empty, vc11 = string.Empty, vc12 = string.Empty, vc13 = string.Empty, vc14 = string.Empty, vc15 = string.Empty, vc16 = string.Empty, vc17 = string.Empty, vc18 = string.Empty, vc19 = string.Empty, vc20 = string.Empty, vc21 = string.Empty, vc22 = string.Empty, vc23 = string.Empty, vc24 = string.Empty, vc25 = string.Empty, vc26 = string.Empty, vc27 = string.Empty, vc28 = string.Empty, vc29 = string.Empty, vc30 = string.Empty;
                string clt1 = string.Empty, clt2 = string.Empty, clt3 = string.Empty, clt4 = string.Empty, clt5 = string.Empty, clt6 = string.Empty, clt7 = string.Empty;
                string cltt1 = string.Empty, cltt2 = string.Empty, cltt3 = string.Empty, cltt4 = string.Empty, cltt5 = string.Empty, cltt6 = string.Empty, cltt7 = string.Empty, cltt8 = string.Empty, cltt9 = string.Empty, cltt10 = string.Empty, cltt11 = string.Empty, cltt12 = string.Empty, cltt13 = string.Empty, cltt14 = string.Empty, cltt15 = string.Empty, cltt16 = string.Empty, cltt17 = string.Empty, cltt18 = string.Empty, cltt19 = string.Empty, cltt20 = string.Empty, cltt21 = string.Empty;

                //check_list start
                if (sel_chlist.Rows.Count != 0)
                {
                    string appno = sel_chlist.Rows[0]["app_applcn_no"].ToString();
                    decimal t1 = (decimal)sel_chlist.Rows[0]["che_share_amt"];
                    vc1 = t1.ToString("0.00");
                    vc2 = sel_chlist.Rows[0]["che_aim_atttendance"].ToString();
                    vc3 = sel_chlist.Rows[0]["che_meeting_bu"].ToString();
                    decimal t2 = (decimal)sel_chlist.Rows[0]["che_income_amt"];
                    vc4 = t2.ToString("0.00");

                    vc5 = sel_chlist.Rows[0]["che_muflis_remark"].ToString();
                    vc6 = sel_chlist.Rows[0]["che_legal_remark"].ToString();

                    string radio1 = sel_chlist.Rows[0]["che_muflis_ind"].ToString();

                    vc7 = sel_chlist.Rows[0]["che_inst_name1"].ToString();
                    //txtjp1.Text = (string)sqlReader2["che_inst_loan_amt1"].ToString();
                    decimal txtjp1amt = (decimal)sel_chlist.Rows[0]["che_inst_loan_amt1"];
                    vc8 = txtjp1amt.ToString("0.00");
                    decimal txtab1amt = (decimal)sel_chlist.Rows[0]["che_inst_monthly_amt1"];
                    vc9 = txtab1amt.ToString("0.00");
                    //txtab1.Text = (string)sqlReader2["che_inst_monthly_amt1"].ToString();
                    vc10 = sel_chlist.Rows[0]["che_inst_name2"].ToString();
                    decimal txtnik2amt = (decimal)sel_chlist.Rows[0]["che_inst_loan_amt2"];
                    vc11 = txtnik2amt.ToString("0.00");
                    decimal txtab2amt = (decimal)sel_chlist.Rows[0]["che_inst_monthly_amt2"];
                    vc12 = txtab2amt.ToString("0.00");
                    vc13 = sel_chlist.Rows[0]["che_inst_name3"].ToString();
                    decimal txtjp3amt = (decimal)sel_chlist.Rows[0]["che_inst_loan_amt3"];
                    vc14 = txtjp3amt.ToString("0.00");
                    decimal txtab3amt = (decimal)sel_chlist.Rows[0]["che_inst_monthly_amt3"];
                    vc15 = txtab3amt.ToString("0.00");

                    string jar1 = radio1.ToString();

                    if (jar1 == "True")
                    {
                        vc16 = "Muflis";
                    }
                    else if (jar1 == "False")
                    {
                        vc16 = "Tidak Muflis";
                    }


                    string radio2 = sel_chlist.Rows[0]["che_legal_ind"].ToString();

                    string bar2 = radio2.ToString();

                    if (bar2 == "True")
                    {
                        vc17 = "Ya";
                    }
                    else if (bar2 == "False")
                    {
                        vc17 = "Tidak";
                    }


                    string radio3 = sel_chlist.Rows[0]["che_active_status_ind"].ToString();

                    string bar3 = radio3.ToString();

                    if (bar3 == "True")
                    {
                        vc18 = "Aktif";
                    }
                    else if (bar3 == "False")
                    {
                        vc18 = "Tidak Aktif";
                    }


                    string radio4 = sel_chlist.Rows[0]["che_aim_loan_ind"].ToString();

                    string bar4 = radio4.ToString();

                    if (bar4 == "True")
                    {
                        vc19 = "Ya";
                    }
                    else if (bar4 == "False")
                    {

                        vc19 = "Tidak";
                    }


                    string radio5 = sel_chlist.Rows[0]["che_koop_loan_ind"].ToString();

                    string bar5 = radio5.ToString();

                    if (bar5 == "True")
                    {
                        vc20 = "Ya";
                    }
                    else if (bar5 == "False")
                    {
                        vc20 = "Tidak";
                    }



                    string readText = sel_chlist.Rows[0]["che_qualify"].ToString();

                    string bar = readText.ToString();

                    for (int i = 0; i <= readText.Length; i++)
                    {
                        if (i == 0)
                        {
                            if (bar.Substring(0, 1) == "1")
                            {
                                clt1 = "YA";
                            }
                            else
                            {
                                clt1 = "TIDAK";
                            }
                        }
                        else if (i == 1)
                        {
                            if (bar.Substring(1, 1) == "1")
                            {
                                clt2 = "YA";
                            }
                            else
                            {
                                clt2 = "TIDAK";
                            }
                        }
                        else if (i == 2)
                        {
                            if (bar.Substring(2, 1) == "1")
                            {
                                clt3 = "YA";
                            }
                            else
                            {
                                clt3 = "TIDAK";
                            }
                        }
                        else if (i == 3)
                        {
                            if (bar.Substring(3, 1) == "1")
                            {
                                clt4 = "YA";
                            }
                            else
                            {
                                clt4 = "TIDAK";
                            }
                        }
                        else if (i == 4)
                        {
                            if (bar.Substring(4, 1) == "1")
                            {
                                clt5 = "YA";
                            }
                            else
                            {
                                clt5 = "TIDAK";
                            }
                        }
                        else if (i == 5)
                        {
                            if (bar.Substring(5, 1) == "1")
                            {
                                clt6 = "YA";
                            }
                            else
                            {
                                clt6 = "TIDAK";
                            }
                        }
                        else if (i == 6)
                        {
                            if (bar.Substring(6, 1) == "1")
                            {
                                clt7 = "YA";
                            }
                            else
                            {
                                clt7 = "TIDAK";
                            }
                        }
                    }

                    string readText1 = sel_chlist.Rows[0]["che_supp_doc"].ToString();

                    string bar1 = readText1.ToString();
                    for (int i = 0; i <= readText1.Length; i++)
                    {
                        if (i == 0)
                        {
                            if (bar1.Substring(0, 1) == "1")
                            {
                                cltt1 = "YA";
                            }
                            else
                            {
                                cltt1 = "TIDAK";
                            }
                        }
                        else if (i == 1)
                        {
                            if (bar1.Substring(1, 1) == "1")
                            {
                                cltt2 = "YA";
                            }
                            else
                            {
                                cltt2 = "TIDAK";
                            }
                        }
                        else if (i == 2)
                        {
                            if (bar1.Substring(2, 1) == "1")
                            {
                                cltt3 = "YA";
                            }
                            else
                            {
                                cltt3 = "TIDAK";
                            }
                        }
                        else if (i == 3)
                        {
                            if (bar1.Substring(3, 1) == "1")
                            {
                                cltt4 = "YA";
                            }
                            else
                            {
                                cltt4 = "TIDAK";
                            }
                        }
                        else if (i == 4)
                        {
                            if (bar1.Substring(4, 1) == "1")
                            {
                                cltt5 = "YA";
                            }
                            else
                            {
                                cltt5 = "TIDAK";
                            }
                        }
                        else if (i == 5)
                        {
                            if (bar1.Substring(5, 1) == "1")
                            {
                                cltt6 = "YA";
                            }
                            else
                            {
                                cltt6 = "TIDAK";
                            }
                        }
                        else if (i == 6)
                        {
                            if (bar1.Substring(6, 1) == "1")
                            {
                                cltt7 = "YA";
                            }
                            else
                            {
                                cltt7 = "TIDAK";
                            }
                        }
                        else if (i == 7)
                        {
                            if (bar1.Substring(7, 1) == "1")
                            {
                                cltt8 = "YA";
                            }
                            else
                            {
                                cltt8 = "TIDAK";
                            }
                        }
                        else if (i == 8)
                        {
                            if (bar1.Substring(8, 1) == "1")
                            {
                                cltt9 = "YA";
                            }
                            else
                            {
                                cltt9 = "TIDAK";
                            }
                        }
                        else if (i == 9)
                        {
                            if (bar1.Substring(9, 1) == "1")
                            {
                                cltt10 = "YA";
                            }
                            else
                            {
                                cltt10 = "TIDAK";
                            }
                        }
                        else if (i == 10)
                        {
                            if (bar1.Substring(10, 1) == "1")
                            {
                                cltt11 = "YA";
                            }
                            else
                            {
                                cltt11 = "TIDAK";
                            }
                        }
                        else if (i == 11)
                        {
                            if (bar1.Substring(11, 1) == "1")
                            {
                                cltt12 = "YA";
                            }
                            else
                            {
                                cltt12 = "TIDAK";
                            }
                        }
                        else if (i == 12)
                        {
                            if (bar1.Substring(12, 1) == "1")
                            {
                                cltt13 = "YA";
                            }
                            else
                            {
                                cltt13 = "TIDAK";
                            }
                        }
                        else if (i == 13)
                        {
                            if (bar1.Substring(13, 1) == "1")
                            {
                                cltt14 = "YA";
                            }
                            else
                            {
                                cltt14 = "TIDAK";
                            }
                        }
                        else if (i == 14)
                        {
                            if (bar1.Substring(14, 1) == "1")
                            {
                                cltt15 = "YA";
                            }
                            else
                            {
                                cltt15 = "TIDAK";
                            }
                        }
                        else if (i == 15)
                        {
                            if (bar1.Substring(15, 1) == "1")
                            {
                                cltt16 = "YA";
                            }
                            else
                            {
                                cltt16 = "TIDAK";
                            }
                        }
                        else if (i == 16)
                        {
                            if (bar1.Substring(16, 1) == "1")
                            {
                                cltt17 = "YA";
                            }
                            else
                            {
                                cltt17 = "TIDAK";
                            }
                        }
                        else if (i == 17)
                        {
                            if (bar1.Substring(17, 1) == "1")
                            {
                                cltt18 = "YA";
                            }
                            else
                            {
                                cltt18 = "TIDAK";
                            }
                        }
                        else if (i == 18)
                        {
                            if (bar1.Substring(18, 1) == "1")
                            {
                                cltt19 = "YA";
                            }
                            else
                            {
                                cltt19 = "TIDAK";
                            }
                        }
                        else if (i == 19)
                        {
                            if (bar1.Substring(19, 1) == "1")
                            {
                                cltt20 = "YA";
                            }
                            else
                            {
                                cltt20 = "TIDAK";
                            }
                        }
                        else if (i == 20)
                        {
                            if (bar1.Substring(20, 1) == "1")
                            {
                                cltt21 = "YA";
                            }
                            else
                            {
                                cltt21 = "TIDAK";
                            }
                        }
                    }

                }

                ///end

                Rptviwer_lt.LocalReport.DataSources.Clear();

                Rptviwer_lt.LocalReport.ReportPath = "Pelaburan_Anggota/pa_ssp_anggota.rdlc";
                ReportDataSource rds = new ReportDataSource("pa_ssp_a", dt);

                ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("no_kp",txticno.Text),
                     new ReportParameter("appno",dt.Rows[0]["app_applcn_no"].ToString()),
                     new ReportParameter("nama",sel_mdet.Rows[0]["mem_name"].ToString()),
                     new ReportParameter("pejabat",sel_mdet.Rows[0]["cawangan_name"].ToString()),
                     new ReportParameter("pusat",sel_mdet.Rows[0]["mem_centre"].ToString()),

                     //check_list
                     new ReportParameter("cl1",vc1),
                     new ReportParameter("cl2",vc2),
                     new ReportParameter("cl3",vc3),
                     new ReportParameter("cl4",vc4),
                     new ReportParameter("cl5",vc5),
                     new ReportParameter("cl6",vc6),
                     new ReportParameter("cl7",vc7),
                     new ReportParameter("cl8",vc8),
                     new ReportParameter("cl9",vc9),
                     new ReportParameter("cl10",vc10),
                     new ReportParameter("cl11",vc11),
                     new ReportParameter("cl12",vc12),
                     new ReportParameter("cl13",vc13),
                     new ReportParameter("cl14",vc16),
                     new ReportParameter("cl15",vc17),
                     new ReportParameter("cl16",vc18),
                     new ReportParameter("cl17",vc19),
                     new ReportParameter("cl18",vc20),

                     new ReportParameter("cl46",vc14),
                     new ReportParameter("cl47",vc15),

                     new ReportParameter("cl19",clt1),
                     new ReportParameter("cl20",clt2),
                     new ReportParameter("cl21",clt3),
                     new ReportParameter("cl22",clt4),
                     new ReportParameter("cl23",clt5),
                     new ReportParameter("cl24",clt6),
                     new ReportParameter("cl25",clt7),

                     new ReportParameter("cl256",cltt1),
                     new ReportParameter("cl26",cltt2),
                     new ReportParameter("cl27",cltt3),
                     new ReportParameter("cl28",cltt4),
                     new ReportParameter("cl29",cltt5),
                     new ReportParameter("cl30",cltt6),
                     new ReportParameter("cl31",cltt7),
                     new ReportParameter("cl32",cltt8),
                     new ReportParameter("cl33",cltt9),
                     new ReportParameter("cl34",cltt10),
                     new ReportParameter("cl35",cltt11),
                     new ReportParameter("cl36",cltt12),
                     new ReportParameter("cl37",cltt13),
                     new ReportParameter("cl38",cltt14),
                     new ReportParameter("cl39",cltt15),
                     new ReportParameter("cl40",cltt16),
                     new ReportParameter("cl41",cltt17),
                     new ReportParameter("cl42",cltt18),
                     new ReportParameter("cl43",cltt19),
                     new ReportParameter("cl44",cltt20),
                     new ReportParameter("cl45",cltt21)



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

                Response.AddHeader("content-disposition", "attachment; filename=SENARAI_SEMAK_PERMOHONAN_ANGGOTA_" + txticno.Text + "." + extension);

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
        Response.Redirect("../Pelaburan_Anggota/PP_cl_shahabat_view.aspx");
    }
}