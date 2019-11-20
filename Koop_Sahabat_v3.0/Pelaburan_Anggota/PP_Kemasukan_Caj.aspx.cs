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


public partial class PP_Kemasukan_Caj : System.Web.UI.Page
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
    string icno = string.Empty;
    string phdate;
    string txn_code = string.Empty;
    string post, late;
    string tcode, ct;
    string tamt = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Button1);
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
                grid();


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

    protected void btnsrch_Click(object sender, EventArgs e)
    {
        try
        {
            if (Applcn_no.Text != "")
            {

                DataTable select_app = new DataTable();
                select_app = DBCon.Ora_Execute_table("select JA.app_applcn_no,JA.app_new_icno,JA.app_name,RW.Wilayah_Name,ISNULL(RB.branch_desc, '') AS branch_desc,ja.app_sts_cd,ISNULL(ja.app_loan_amt,'') as app_loan_amt,ja.appl_loan_dur from jpa_application as JA  Left Join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd Left join ref_branch AS RB ON RB.branch_cd=JA.app_branch_cd where JA.app_applcn_no='" + Applcn_no.Text + "' and ja.app_sts_cd= 'Y'");
                if (select_app.Rows.Count == 0)
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
                else
                {

                    MP_nama.Text = select_app.Rows[0]["app_name"].ToString();
                    MP_icno.Text = select_app.Rows[0]["app_new_icno"].ToString();
                    //MP_wilayah.Text = select_app.Rows[0]["Wilayah_Name"].ToString();
                    //MP_cawangan.Text = select_app.Rows[0]["branch_desc"].ToString();
                    decimal amt = (decimal)select_app.Rows[0]["app_loan_amt"];
                    TextBox1.Text = amt.ToString("C").Replace("$","").Replace("RM", "");
                    TextBox2.Text = select_app.Rows[0]["appl_loan_dur"].ToString();

                }
                grid();
            }
            else
            {
                grid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }

    }

    protected void tampah_click(object sender, EventArgs e)
    {
        try
        {

            if (Applcn_no.Text != "")
            {
                if (TextBox3.Text != "" || MPP_jb.Text != "")
                {
                    DataTable select_ic_amt = new DataTable();
                    //select_ic_amt = DBCon.Ora_Execute_table("select caj_applcn_no,sum(caj_postage) + sum(caj_late_pay) as tb_amt from jpa_charge where caj_applcn_no='"+ Applcn_no.Text +"' group by caj_applcn_no");
                    select_ic_amt = DBCon.Ora_Execute_table("select txn_bal_amt from jpa_transaction where txn_applcn_no='" + Applcn_no.Text + "' and txn_crt_dt = (select max(txn_crt_dt) from jpa_transaction where txn_applcn_no='" + Applcn_no.Text + "')");

                    string val_amt = string.Empty;
                    if (TextBox3.Text != "0.00" && TextBox3.Text != "")
                    {
                        txn_code = "11";
                        post = "11";
                        val_amt = TextBox3.Text;
                    }
                    else
                    {
                        post = "";
                    }
                    if (MPP_jb.Text != "0.00" && MPP_jb.Text != "")
                    {
                        txn_code = "02";
                        late = "02";
                        val_amt = MPP_jb.Text;
                    }
                    else
                    {
                        late = "";
                    }
                    string bamt = string.Empty;
                    string b_amt = string.Empty;
                    if (select_ic_amt.Rows.Count != 0)
                    {
                        bamt = select_ic_amt.Rows[0]["txn_bal_amt"].ToString();
                        b_amt = (double.Parse(bamt) + double.Parse(val_amt)).ToString("0.00");
                    }
                    else
                    {
                        //bamt = "0.00";
                        b_amt = val_amt;
                    }

                    DBCon.Execute_CommamdText("insert into jpa_charge (caj_applcn_no,caj_postage,caj_postage_pay_id,caj_late_pay,caj_late_pay_pay_id,caj_crt_id,caj_crt_dt) values ('" + Applcn_no.Text + "','" + TextBox3.Text + "','" + post + "','" + MPP_jb.Text + "','" + late + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");
                    DBCon.Execute_CommamdText("insert into jpa_transaction (txn_applcn_no,txn_cd,txn_debit_amt,txn_bal_amt,txn_credit_amt,txn_gst_amt,txn_crt_id,txn_crt_dt) values ('" + Applcn_no.Text + "','" + txn_code + "','" + val_amt + "','" + b_amt + "','0.00','0.00','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");
                    grid();
                    TextBox3.Text = "";
                    MPP_jb.Text = "";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }

            }
            else
            {
                grid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }


    protected void lnkView_Click(object sender, EventArgs e)
    {

        LinkButton btnButton = sender as LinkButton;
        GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;


        var a_no = (System.Web.UI.WebControls.Label)gvRow.FindControl("app_icno");
        var a_id = (System.Web.UI.WebControls.Label)gvRow.FindControl("app_dt");

        DataTable ddokdicno = new DataTable();
        ddokdicno = DBCon.Ora_Execute_table("select * from jpa_charge where caj_applcn_no='" + a_no.Text + "' and caj_crt_dt='" + a_id.Text + "'");

        if (ddokdicno.Rows.Count != 0)
        {
            decimal amt11 = (decimal)ddokdicno.Rows[0]["caj_postage"];
            TextBox3.Text = amt11.ToString("0.00");
            decimal amt12 = (decimal)ddokdicno.Rows[0]["caj_late_pay"];
            MPP_jb.Text = amt12.ToString("0.00");
            hid_id.Text = ddokdicno.Rows[0]["caj_crt_dt"].ToString();
            Button5.Visible = true;
            Button2.Visible = false;

            DataTable ddokdicno_del = new DataTable();
            ddokdicno_del = DBCon.Ora_Execute_table("select * from jpa_transaction where txn_applcn_no='" + Applcn_no.Text + "' and txn_crt_dt >= '" + a_id.Text + "' and txn_cd IN('02','11')");
            for (int i = 0; i <= ddokdicno_del.Rows.Count - 1; i++)
            {
                string v1 = string.Empty;
                string aa1 = ddokdicno_del.Rows[i]["txn_bal_amt"].ToString();
                if (TextBox3.Text != "0.00")
                {
                    v1 = (double.Parse(aa1) - double.Parse(TextBox3.Text)).ToString("0.00");
                }
                else if (MPP_jb.Text != "0.00")
                {
                    v1 = (double.Parse(aa1) - double.Parse(MPP_jb.Text)).ToString("0.00");
                }

                DBCon.Execute_CommamdText("UPDATE jpa_transaction set txn_bal_amt='" + v1 + "' where txn_applcn_no='" + ddokdicno_del.Rows[i]["txn_applcn_no"].ToString() + "' and txn_crt_dt='" + ddokdicno_del.Rows[i]["txn_crt_dt"].ToString() + "'");
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }

    }

    protected void hapus_click(object sender, EventArgs e)
    {
        try
        {
            if (Applcn_no.Text != "")
            {
                foreach (GridViewRow gvrow in gvSelected.Rows)
                {
                    var a_no = (System.Web.UI.WebControls.Label)gvrow.FindControl("app_icno");
                    var a_id = (System.Web.UI.WebControls.Label)gvrow.FindControl("app_dt");
                    var amt1 = (System.Web.UI.WebControls.Label)gvrow.FindControl("Label2");
                    var amt2 = (System.Web.UI.WebControls.Label)gvrow.FindControl("Label4");

                    //if (c_dte.Text != "")
                    //{
                    var checkbox = gvrow.FindControl("rbtnSelect") as System.Web.UI.WebControls.RadioButton;
                    if (checkbox.Checked == true)
                    {
                        DataTable ddokdicno_del = new DataTable();
                        ddokdicno_del = DBCon.Ora_Execute_table("select * from jpa_transaction where txn_applcn_no='" + a_no.Text + "' and txn_crt_dt > '" + a_id.Text + "' and txn_cd IN('02','11')");
                        for (int i = 0; i <= ddokdicno_del.Rows.Count - 1; i++)
                        {
                            string v1 = string.Empty;
                            string aa1 = ddokdicno_del.Rows[i]["txn_bal_amt"].ToString();
                            if (amt1.Text != "0.00")
                            {
                                v1 = (double.Parse(aa1) - double.Parse(amt1.Text)).ToString("0.00");
                            }
                            else if (amt2.Text != "0.00")
                            {
                                v1 = (double.Parse(aa1) - double.Parse(amt2.Text)).ToString("0.00");
                            }
                            DBCon.Execute_CommamdText("UPDATE jpa_transaction set txn_bal_amt='" + v1 + "' where txn_applcn_no='" + ddokdicno_del.Rows[i]["txn_applcn_no"].ToString() + "' and txn_crt_dt='" + ddokdicno_del.Rows[i]["txn_crt_dt"].ToString() + "'");
                        }
                        DBCon.Execute_CommamdText("DELETE from jpa_charge where caj_applcn_no='" + a_no.Text + "' and caj_crt_dt='" + a_id.Text + "'");
                        DBCon.Execute_CommamdText("DELETE from jpa_transaction where txn_applcn_no='" + a_no.Text + "' and txn_crt_dt='" + a_id.Text + "'");
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dihapus',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                        grid();
                    }

                }

            }
            else
            {
                grid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }


    protected void BindGridview(object sender, EventArgs e)
    {
        gvSelected.Visible = true;
        grid();
    }

    protected void grid()
    {
        if (Applcn_no.Text != "")
        {
            icno = Applcn_no.Text;
        }
        else
        {
            icno = "";
        }
        gvSelected.Visible = true;
        con.Open();
        SqlCommand cmd = new SqlCommand("select * from jpa_charge where caj_applcn_no='" + icno + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {

            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gvSelected.DataSource = ds;
            gvSelected.DataBind();
            int columncount = gvSelected.Rows[0].Cells.Count;
            gvSelected.Rows[0].Cells.Clear();
            gvSelected.Rows[0].Cells.Add(new TableCell());
            gvSelected.Rows[0].Cells[0].ColumnSpan = columncount;
            gvSelected.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";

        }
        else
        {
            gvSelected.DataSource = ds;
            gvSelected.DataBind();


        }
        con.Close();
    }
    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSelected.PageIndex = e.NewPageIndex;
        grid();
        gvSelected.DataBind();
    }

    protected void update_click(object sender, EventArgs e)
    {
        try
        {
            if (Applcn_no.Text != "")
            {
                if (TextBox3.Text != "" || MPP_jb.Text != "")
                {
                    string val_amt = string.Empty;
                    //DataTable select_ic_amt = new DataTable();
                    //select_ic_amt = DBCon.Ora_Execute_table("select caj_applcn_no,sum(caj_postage) + sum(caj_late_pay) as tb_amt from jpa_charge where caj_applcn_no='" + Applcn_no.Text + "' and caj_crt_dt!= '" + hid_id.Text + "' group by caj_applcn_no");
                    if (TextBox3.Text != "0.00")
                    {
                        txn_code = "11";
                        post = "11";
                        val_amt = TextBox3.Text;
                    }
                    else
                    {
                        post = "";
                    }
                    if (MPP_jb.Text != "0.00")
                    {
                        txn_code = "02";
                        late = "02";
                        val_amt = MPP_jb.Text;
                    }
                    else
                    {
                        late = "";
                    }

                    string bamt = string.Empty;
                    string b_amt = string.Empty;
                    //if (select_ic_amt.Rows.Count != 0)
                    //{
                    //    bamt = select_ic_amt.Rows[0]["tb_amt"].ToString();
                    //    b_amt = (double.Parse(bamt) + double.Parse(val_amt)).ToString("0.00");
                    //}
                    //else
                    //{
                    //    //bamt = "0.00";
                    //    b_amt = val_amt;
                    //}
                    DBCon.Execute_CommamdText("UPDATE jpa_charge SET caj_postage='" + TextBox3.Text + "',caj_postage_pay_id='" + post + "',caj_late_pay='" + MPP_jb.Text + "',caj_late_pay_pay_id='" + late + "',caj_upd_id='" + Session["New"].ToString() + "',caj_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE caj_applcn_no='" + Applcn_no.Text + "' and caj_crt_dt='" + hid_id.Text + "'");
                    //DBCon.Execute_CommamdText("UPDATE jpa_transaction SET txn_cd='" + txn_code + "',txn_debit_amt='"+val_amt+"',txn_bal_amt='"+ b_amt +"',txn_upd_id='" + Session["New"].ToString() + "',txn_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE txn_applcn_no='" + Applcn_no.Text + "' and txn_crt_dt='" + hid_id.Text + "'");
                    DataTable select_ic_amt = new DataTable();
                    select_ic_amt = DBCon.Ora_Execute_table("select caj_applcn_no,sum(caj_postage) + sum(caj_late_pay) as tb_amt from jpa_charge where caj_applcn_no='" + Applcn_no.Text + "' and caj_crt_dt < '" + hid_id.Text + "' group by caj_applcn_no");
                    DataTable max_dt = new DataTable();
                    max_dt = DBCon.Ora_Execute_table("select MAX(txn_crt_dt) as crt_dt from jpa_transaction where txn_applcn_no='" + Applcn_no.Text + "' and txn_crt_dt < '" + hid_id.Text + "'");

                    DataTable max_dt_txn = new DataTable();
                    max_dt_txn = DBCon.Ora_Execute_table("select * from jpa_transaction where txn_applcn_no='" + Applcn_no.Text + "' and txn_crt_dt = '" + max_dt.Rows[0]["crt_dt"].ToString() + "'");

                    string sn = string.Empty;
                    if (max_dt_txn.Rows.Count == 0)
                    {
                        sn = "0.00";
                    }
                    else
                    {
                        sn = max_dt_txn.Rows[0]["txn_bal_amt"].ToString();
                    }

                    DataTable ddokdicno_del = new DataTable();
                    ddokdicno_del = DBCon.Ora_Execute_table("select * from jpa_transaction where txn_applcn_no='" + Applcn_no.Text + "' and txn_crt_dt >= '" + hid_id.Text + "' order by txn_crt_dt");
                    for (int i = 0; i <= ddokdicno_del.Rows.Count - 1; i++)
                    {
                        if (i == 0)
                        {
                            bamt = sn;
                            b_amt = (double.Parse(bamt) + double.Parse(val_amt)).ToString("0.00");
                        }
                        else
                        {
                            //bamt = "0.00";
                            DataTable max_dt1 = new DataTable();
                            max_dt1 = DBCon.Ora_Execute_table("select MAX(txn_crt_dt) as crt_dt from jpa_transaction where txn_applcn_no='" + ddokdicno_del.Rows[i]["txn_applcn_no"].ToString() + "' and txn_crt_dt < '" + ddokdicno_del.Rows[i]["txn_crt_dt"].ToString() + "'");

                            DataTable max_dt_txn1 = new DataTable();
                            max_dt_txn1 = DBCon.Ora_Execute_table("select * from jpa_transaction where txn_applcn_no='" + ddokdicno_del.Rows[i]["txn_applcn_no"].ToString() + "' and txn_crt_dt = '" + max_dt1.Rows[0]["crt_dt"].ToString() + "'");

                            b_amt = (double.Parse(max_dt_txn1.Rows[0]["txn_bal_amt"].ToString()) + double.Parse(ddokdicno_del.Rows[i]["txn_debit_amt"].ToString())).ToString("0.00");
                        }
                        if (Applcn_no.Text == ddokdicno_del.Rows[i]["txn_applcn_no"].ToString() && hid_id.Text == ddokdicno_del.Rows[i]["txn_crt_dt"].ToString())
                        {
                            DBCon.Execute_CommamdText("UPDATE jpa_transaction SET txn_cd='" + txn_code + "',txn_debit_amt='" + val_amt + "',txn_bal_amt='" + b_amt + "',txn_upd_id='" + Session["New"].ToString() + "',txn_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE txn_applcn_no='" + Applcn_no.Text + "' and txn_crt_dt='" + ddokdicno_del.Rows[i]["txn_crt_dt"].ToString() + "'");
                        }
                        else
                        {
                            DBCon.Execute_CommamdText("UPDATE jpa_transaction SET txn_bal_amt='" + b_amt + "',txn_upd_id='" + Session["New"].ToString() + "',txn_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE txn_applcn_no='" + Applcn_no.Text + "' and txn_crt_dt='" + ddokdicno_del.Rows[i]["txn_crt_dt"].ToString() + "'");
                        }
                    }                   

                    TextBox3.Text = "";
                    MPP_jb.Text = "";
                    Button5.Visible = false;
                    Button2.Visible = true;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    grid();

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Sila Masukkan Input Carian',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }

    protected void vv1_changed(object sender, EventArgs e)
    {
        if(TextBox3.Text != "")
        {
            MPP_jb.Text = "0.00";
            TextBox3.Text = double.Parse(TextBox3.Text).ToString("C").Replace("$","").Replace("RM", "");
        }
        grid();
    }

    protected void vv2_changed(object sender, EventArgs e)
    {
        if (MPP_jb.Text != "")
        {
            TextBox3.Text = "0.00";
            MPP_jb.Text = double.Parse(MPP_jb.Text).ToString("C").Replace("$", "").Replace("RM", "");
        }
        grid();
    }
    protected void click_print(object sender, EventArgs e)
    {
        {
            try
            {
                int counter = 0;

                if (Applcn_no.Text != "")
                {
                    //Path
                    foreach (GridViewRow gvrow1 in gvSelected.Rows)
                    {
                        var cb = gvrow1.FindControl("rbtnSelect") as System.Web.UI.WebControls.RadioButton;
                        if (cb.Checked)
                        {
                            counter = counter + 1;
                        }
                    }
                    if (counter != 0)
                    {
                        foreach (GridViewRow gvrow in gvSelected.Rows)
                        {
                            var a_no = (System.Web.UI.WebControls.Label)gvrow.FindControl("app_icno");
                            var a_id = (System.Web.UI.WebControls.Label)gvrow.FindControl("app_id");
                            //var l1 = (System.Web.UI.WebControls.Label)gvrow.FindControl("Label1");
                            //var l2 = (System.Web.UI.WebControls.Label)gvrow.FindControl("Label6");
                            var l_amt = (System.Web.UI.WebControls.Label)gvrow.FindControl("Label2");
                            var l_amt2 = (System.Web.UI.WebControls.Label)gvrow.FindControl("Label4");

                            //if (c_dte.Text != "")
                            //{
                            var checkbox = gvrow.FindControl("rbtnSelect") as System.Web.UI.WebControls.RadioButton;
                            if (checkbox.Checked == true)
                            {
                                if (l_amt.Text.Trim() != "0.00" && l_amt2.Text.Trim() == "0.00")
                                {
                                    tcode = "Caj Pengeposan";
                                    tamt = l_amt.Text;
                                    ct = "01";
                                }
                                if (l_amt2.Text.Trim() != "0.00" && l_amt.Text.Trim() == "0.00")
                                {
                                    tcode = "Caj Lewat";
                                    tamt = l_amt2.Text;
                                    ct = "11";
                                }
                                DataSet ds = new DataSet();
                                DataTable dt = new DataTable();
                                // dt = DBCon.Ora_Execute_table("select * from jpa_charge where id='" + a_id.Text + "' and caj_applcn_no='" + a_no.Text + "'");

                                Rptviwer_lt.Reset();
                                //ds.Tables.Add(dt);

                                //Rptviwer_lt.LocalReport.DataSources.Clear();

                                Rptviwer_lt.LocalReport.ReportPath = "PELABURAN_ANGGOTA/kemaskini_caj.rdlc";
                                //ReportDataSource rds = new ReportDataSource("pp_transaksi", dt);

                                //Rptviwer_lt.LocalReport.DataSources.Add(rds);
                                DataTable applcn = new DataTable();
                                applcn = DBCon.Ora_Execute_table("select ja.app_bank_acc_no,ja.app_applcn_no,ja.app_name,ja.app_permnt_address,ja.app_permnt_postcode,rn.Decription from jpa_application ja left join Ref_Negeri rn on rn.Decription_Code=ja.app_permnt_state_cd where app_applcn_no='" + a_no.Text + "'");
                                ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("no_akaun", applcn.Rows[0]["app_bank_acc_no"].ToString()),
                     new ReportParameter("no_applcn", applcn.Rows[0]["app_applcn_no"].ToString()),
                     new ReportParameter("tarikh", DateTime.Now.ToString("dd/MM/yyyy")),
                     new ReportParameter("name", applcn.Rows[0]["app_name"].ToString()),
                     new ReportParameter("address", applcn.Rows[0]["app_permnt_address"].ToString()),
                     new ReportParameter("s_code", applcn.Rows[0]["app_permnt_postcode"].ToString()),
                     new ReportParameter("state", applcn.Rows[0]["Decription"].ToString()),
                     new ReportParameter("t_code", tcode),
                     new ReportParameter("kt", ct),
                     new ReportParameter("t_amt", tamt)
                     };


                                Rptviwer_lt.LocalReport.SetParameters(rptParams);

                                //Refresh
                                Rptviwer_lt.LocalReport.Refresh();
                                Warning[] warnings;

                                string[] streamids;

                                string mimeType;

                                string encoding;

                                string extension;

                                string fname = DateTime.Now.ToString("dd_MM_yyyy");

                                string devinfo = "<DeviceInfo><ColorDepth>32</ColorDepth><DpiX>350</DpiX><DpiY>350</DpiY><OutputFormat>PDF</OutputFormat>" +
                                       "  <PageWidth>12.20in</PageWidth>" +
                                        "  <PageHeight>8.27in</PageHeight>" +
                                        "  <MarginTop>0.1in</MarginTop>" +
                                        "  <MarginLeft>0.5in</MarginLeft>" +
                                         "  <MarginRight>0in</MarginRight>" +
                                         "  <MarginBottom>0in</MarginBottom>" +
                                       "</DeviceInfo>";

                                byte[] bytes = Rptviwer_lt.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);


                                Response.Buffer = true;

                                Response.Clear();

                                Response.ClearHeaders();

                                Response.ClearContent();

                                Response.ContentType = "application/pdf";

                                Response.AddHeader("content-disposition", "attachment; filename= Kemasukan_Caj_" + a_no.Text + "." + extension);

                                Response.BinaryWrite(bytes);

                                Response.Flush();

                                Response.End();
                            }
                        }

                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Please Select Min One Check box',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    grid();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("PP_Kemasukan_Caj.aspx");
    }

  
}