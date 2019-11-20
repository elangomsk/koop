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


public partial class PP_pen_Senaraihitam : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string str;
    DBConnection DBCon = new DBConnection();
    SqlCommand com;
    DataTable dt = new DataTable();
    string Status = string.Empty;
    string level, userid;
    int total = 0;
    string selphase_no;
    string pay_date;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                level = Session["level"].ToString();
                userid = Session["New"].ToString();
                MP_nama.Attributes.Add("Readonly", "Readonly");
                MP_icno.Attributes.Add("Readonly", "Readonly");
                //MP_wilayah.Attributes.Add("Readonly", "Readonly");
                //MP_cawangan.Attributes.Add("Readonly", "Readonly");
                TextBox2.Attributes.Add("Readonly", "Readonly");
                TextBox1.Attributes.Add("Readonly", "Readonly");
                MPP_jb.Attributes.Add("Readonly", "Readonly");
                TextBox3.Attributes.Add("Readonly", "Readonly");
                TextBox9.Attributes.Add("Readonly", "Readonly");
                TextBox6.Attributes.Add("Readonly", "Readonly");
                TextBox7.Attributes.Add("Readonly", "Readonly");
                TextBox8.Attributes.Add("Readonly", "Readonly");
                TextBox10.Attributes.Add("Readonly", "Readonly");
                TextBox5.Attributes.Add("Readonly", "Readonly");
                TextBox4.Attributes.Add("Readonly", "Readonly");
                Pengenalan();
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }


    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetCompletionList(string prefixText, int count)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "select app_applcn_no from jpa_application JA Left Join jpa_jkkpa_approval as JJA ON JJA.jkk_applcn_no=JA.app_applcn_no where app_applcn_no like '%' + @Search + '%' and JJA.jkk_result_ind='L' and JA.applcn_clsed ='N'";
                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> countryNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    if (sdr.HasRows == true)
                    {
                        while (sdr.Read())
                        {
                            countryNames.Add(sdr["app_applcn_no"].ToString());

                        }
                    }
                    else
                    {
                        countryNames.Add("Rekod Tidak Dijumpai.");
                    }
                }

                con.Close();
                return countryNames;
            }
        }
    }
    void Pengenalan()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select Description,Description_Code from Ref_Jenis_Pengenalan WHERE Status = 'A' order by Description";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList1.DataSource = dt;
            DropDownList1.DataTextField = "Description";
            DropDownList1.DataValueField = "Description_Code";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            DropDownList2.DataSource = dt;
            DropDownList2.DataTextField = "Description";
            DropDownList2.DataValueField = "Description_Code";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            DropDownList3.DataSource = dt;
            DropDownList3.DataTextField = "Description";
            DropDownList3.DataValueField = "Description_Code";
            DropDownList3.DataBind();
            DropDownList3.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void btnsrch_Click(object sender, EventArgs e)
    {
        try
        {
            if (Applcn_no.Text != "")
            {
                DataTable ddicno = new DataTable();
                ddicno = DBCon.Ora_Execute_table("select JA.app_applcn_no,JA.app_new_icno,JA.app_name,RW.Wilayah_Name,ISNULL(RB.branch_desc, '') AS branch_desc,ja.app_sts_cd,ISNULL(ja.app_loan_amt,'') as app_loan_amt,ja.appl_loan_dur,ISNULL(ja.app_backdated_amt,'') as app_backdated_amt,ja.app_backdated_day,ISNULL(ja.app_bal_loan_amt,'') as app_bal_loan_amt,jg.gua_ic_type_cd,jg.gua_icno,jg.gua_name from jpa_application as JA  Left Join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd Left join ref_branch AS RB ON RB.branch_cd=JA.app_branch_cd left join jpa_guarantor as jg on jg.gua_applcn_no=ja.app_applcn_no where JA.app_applcn_no='" + Applcn_no.Text + "' and ja.app_sts_cd= 'Y' order by jg.gua_seq_no asc");
                //DataTable select_app = new DataTable();
                //select_app = DBCon.Ora_Execute_table("select JA.app_new_icno,JA.app_name,RW.Wilayah_Name,ISNULL(RB.branch_desc, '') AS branch_desc, JJA.jkk_approve_amt, JJA.jkk_approve_dur from jpa_application as JA  Left Join jpa_jkkpa_approval as JJA ON JJA.jkk_applcn_no=JA.app_applcn_no Left Join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd Left join ref_branch AS RB ON RB.branch_cd=JA.app_branch_cd where JA.app_applcn_no='" + Applcn_no.Text + "'");
                if (ddicno.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
                else
                {

                    MP_nama.Text = ddicno.Rows[0]["app_name"].ToString();
                    MP_icno.Text = ddicno.Rows[0]["app_new_icno"].ToString();
                    //MP_wilayah.Text = ddicno.Rows[0]["Wilayah_Name"].ToString();
                    //MP_cawangan.Text = ddicno.Rows[0]["branch_desc"].ToString();
                    decimal amt = (decimal)ddicno.Rows[0]["app_loan_amt"];
                    TextBox1.Text = amt.ToString("C").Replace("$", "");
                    TextBox2.Text = ddicno.Rows[0]["appl_loan_dur"].ToString();
                    decimal amt2 = (decimal)ddicno.Rows[0]["app_backdated_amt"];
                    TextBox6.Text = amt2.ToString("C").Replace("$", "");
                    TextBox7.Text = ddicno.Rows[0]["app_backdated_day"].ToString();
                    decimal amt3 = (decimal)ddicno.Rows[0]["app_bal_loan_amt"];
                    TextBox8.Text = amt3.ToString("C").Replace("$", "");

                    DataTable blist = new DataTable();
                    //blist = DBCon.Ora_Execute_table("select TOP 1 * from jpa_blacklist where blk_applcn_no='" + Applcn_no.Text + "' and blk_icno='" + ddicno.Rows[0]["app_new_icno"].ToString() + "' ORDER BY blk_crt_dt DESC");
                    blist = DBCon.Ora_Execute_table("SELECT * FROM jpa_blacklist WHERE blk_crt_dt IN (SELECT max(blk_crt_dt) FROM jpa_blacklist) and blk_applcn_no='" + Applcn_no.Text + "' and blk_icno='" + ddicno.Rows[0]["app_new_icno"].ToString() + "'");
                    if (blist.Rows.Count != 0)
                    {
                        chk1.Checked = true;
                    }
                    else
                    {
                        chk1.Checked = false;
                    }
                    if (ddicno.Rows.Count >= 1)
                    {
                        DropDownList3.SelectedValue = ddicno.Rows[0]["gua_ic_type_cd"].ToString();
                        MPP_jb.Text = ddicno.Rows[0]["gua_icno"].ToString();
                        TextBox4.Text = ddicno.Rows[0]["gua_name"].ToString();

                        DataTable dd_blist1 = new DataTable();
                        dd_blist1 = DBCon.Ora_Execute_table("SELECT * FROM jpa_blacklist WHERE blk_crt_dt IN (SELECT max(blk_crt_dt) FROM jpa_blacklist) and blk_applcn_no='" + Applcn_no.Text + "' and blk_icno='" + ddicno.Rows[0]["gua_icno"].ToString() + "'");
                        if (dd_blist1.Rows.Count != 0)
                        {
                            chk2.Checked = true;
                        }
                        else
                        {
                            chk2.Checked = false;
                        }

                    }
                    if (ddicno.Rows.Count >= 2)
                    {
                        DropDownList1.SelectedValue = ddicno.Rows[1]["gua_ic_type_cd"].ToString();
                        TextBox3.Text = ddicno.Rows[1]["gua_icno"].ToString();
                        TextBox5.Text = ddicno.Rows[1]["gua_name"].ToString();

                        DataTable dd_blist2 = new DataTable();
                        dd_blist2 = DBCon.Ora_Execute_table("SELECT * FROM jpa_blacklist WHERE blk_crt_dt IN (SELECT max(blk_crt_dt) FROM jpa_blacklist) and blk_applcn_no='" + Applcn_no.Text + "' and blk_icno='" + ddicno.Rows[1]["gua_icno"].ToString() + "'");
                        if (dd_blist2.Rows.Count != 0)
                        {
                            chk3.Checked = true;
                        }
                        else
                        {
                            chk3.Checked = false;
                        }
                    }
                    if (ddicno.Rows.Count == 3)
                    {
                        DropDownList2.SelectedValue = ddicno.Rows[2]["gua_ic_type_cd"].ToString();
                        TextBox9.Text = ddicno.Rows[2]["gua_icno"].ToString();
                        TextBox10.Text = ddicno.Rows[2]["gua_name"].ToString();

                        DataTable dd_blist3 = new DataTable();
                        dd_blist3 = DBCon.Ora_Execute_table("SELECT * FROM jpa_blacklist WHERE blk_crt_dt IN (SELECT max(blk_crt_dt) FROM jpa_blacklist) and blk_applcn_no='" + Applcn_no.Text + "' and blk_icno='" + ddicno.Rows[2]["gua_icno"].ToString() + "'");
                        if (dd_blist3.Rows.Count != 0)
                        {
                            chk4.Checked = true;
                        }
                        else
                        {
                            chk4.Checked = false;
                        }
                    }

                    

                }
            }
            else
            {
                

                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }

    }

    protected void submit_click(object sender, EventArgs e)
    {
        try
        {
            if (Applcn_no.Text != "")
            {
                if (chk1.Checked == true || chk2.Checked == true || chk3.Checked == true || chk4.Checked == true)
                {
                    if (chk1.Checked == true)
                    {

                        DataTable ddicno = new DataTable();
                        ddicno = DBCon.Ora_Execute_table("select * from jpa_blacklist where blk_applcn_no='" + Applcn_no.Text + "' and blk_icno='" + MP_icno.Text + "' and blk_crt_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                        if (ddicno.Rows.Count == 0)
                        {
                            DBCon.Execute_CommamdText("insert into jpa_blacklist (blk_applcn_no,blk_icno,blk_ic_type_cd,blk_name,blk_crt_id,blk_crt_dt) values ('" + Applcn_no.Text + "','" + MP_icno.Text + "','','" + MP_nama.Text.Replace("'", "''") + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");
                        }
                        else
                        {
                            DBCon.Execute_CommamdText("UPDATE jpa_blacklist set blk_upd_id='" + Session["New"].ToString() + "',blk_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where blk_applcn_no='" + Applcn_no.Text + "' and blk_icno='" + MPP_jb.Text + "' and blk_crt_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                        }
                    }

                    if (chk2.Checked == true)
                    {
                        DataTable ddicno = new DataTable();
                        ddicno = DBCon.Ora_Execute_table("select * from jpa_blacklist where blk_applcn_no='" + Applcn_no.Text + "' and blk_icno='" + MPP_jb.Text + "' and blk_crt_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                        if (ddicno.Rows.Count == 0)
                        {
                            DBCon.Execute_CommamdText("insert into jpa_blacklist (blk_applcn_no,blk_icno,blk_ic_type_cd,blk_name,blk_crt_id,blk_crt_dt) values ('" + Applcn_no.Text + "','" + MPP_jb.Text + "','" + DropDownList3.SelectedValue + "','" + TextBox4.Text.Replace("'", "''") + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");
                        }
                        else
                        {
                            DBCon.Execute_CommamdText("UPDATE jpa_blacklist set blk_upd_id='" + Session["New"].ToString() + "',blk_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where blk_applcn_no='" + Applcn_no.Text + "' and blk_icno='" + MPP_jb.Text + "' and blk_crt_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                        }
                    }
                    if (chk3.Checked == true)
                    {
                        DataTable ddicno = new DataTable();
                        ddicno = DBCon.Ora_Execute_table("select * from jpa_blacklist where blk_applcn_no='" + Applcn_no.Text + "' and blk_icno='" + TextBox3.Text + "' and blk_crt_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                        if (ddicno.Rows.Count == 0)
                        {
                            DBCon.Execute_CommamdText("insert into jpa_blacklist (blk_applcn_no,blk_icno,blk_ic_type_cd,blk_name,blk_crt_id,blk_crt_dt) values ('" + Applcn_no.Text + "','" + TextBox3.Text + "','" + DropDownList1.SelectedValue + "','" + TextBox5.Text.Replace("'", "''") + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");
                        }
                        else
                        {
                            DBCon.Execute_CommamdText("UPDATE jpa_blacklist set blk_upd_id='" + Session["New"].ToString() + "',blk_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where blk_applcn_no='" + Applcn_no.Text + "' and blk_icno='" + TextBox3.Text + "' and blk_crt_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                        }
                    }
                    if (chk4.Checked == true)
                    {
                        DataTable ddicno = new DataTable();
                        ddicno = DBCon.Ora_Execute_table("select * from jpa_blacklist where blk_applcn_no='" + Applcn_no.Text + "' and blk_icno='" + TextBox9.Text + "' and blk_crt_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                        if (ddicno.Rows.Count == 0)
                        {
                            DBCon.Execute_CommamdText("insert into jpa_blacklist (blk_applcn_no,blk_icno,blk_ic_type_cd,blk_name,blk_crt_id,blk_crt_dt) values ('" + Applcn_no.Text + "','" + TextBox9.Text + "','" + DropDownList2.SelectedValue + "','" + TextBox10.Text.Replace("'", "''") + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");
                        }
                        else
                        {
                            DBCon.Execute_CommamdText("UPDATE jpa_blacklist set blk_upd_id='" + Session["New"].ToString() + "',blk_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where blk_applcn_no='" + Applcn_no.Text + "' and blk_icno='" + TextBox9.Text + "' and blk_crt_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                        }
                    }


                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                   
                }
                else
                {
         
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Buat Pilihan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    
}