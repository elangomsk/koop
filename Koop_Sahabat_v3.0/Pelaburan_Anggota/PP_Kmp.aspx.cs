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
using System.Xml;


public partial class PP_Kmp : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string str;
    DBConnection DBCon = new DBConnection();
    SqlCommand com;
    DataTable dt = new DataTable();
    StudentWebService service = new StudentWebService();
    string Status = string.Empty;
    string level, userid;
    //int total = 0;
    decimal total = 0M;
    string selphase_no;
    string pay_date;
    string phdate;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
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
                MPP_jb.Attributes.Add("Readonly", "Readonly");
                RadioButton1.Checked = true;
                //gvSelected.Visible = false;
                var samp = Request.Url.Query;
                if (samp != "")
                {
                    Applcn_no.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    srch_Click();
                    Button4.Visible = false;
                    Button6.Visible = false;
                }
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
                com.CommandText = "select app_applcn_no from jpa_application JA Left Join jpa_jkkpa_approval as JJA ON JJA.jkk_applcn_no=JA.app_applcn_no where app_applcn_no like '%' + @Search + '%' and JJA.jkk_result_ind='L' and applcn_clsed ='N'";
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
    void srch_Click()
    {
        if (Applcn_no.Text != "")
        {
            tab1();
            DataTable ddicno = new DataTable();
            ddicno = DBCon.Ora_Execute_table("select app_applcn_no,applcn_clsed from jpa_application JA Left Join jpa_jkkpa_approval as JJA ON JJA.jkk_applcn_no=JA.app_applcn_no where JA.app_applcn_no='" + Applcn_no.Text + "' and JJA.jkk_result_ind='L'");
            DataTable select_app = new DataTable();
            select_app = DBCon.Ora_Execute_table("select JA.app_new_icno,JA.app_name,RW.Wilayah_Name,ISNULL(RB.branch_desc, '') AS branch_desc from jpa_application as JA Left Join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd Left join ref_branch AS RB ON RB.branch_cd=JA.app_branch_cd where JA.app_applcn_no='" + Applcn_no.Text + "'");
            if (ddicno.Rows.Count == 0)
            {
                //RadioButton1.Checked = false;
                //RadioButton2.Checked = false;
                //RadioButton3.Checked = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
            else
            {
                if (ddicno.Rows[0]["applcn_clsed"].ToString() == "Y")
                {
                    Button7.Visible = false;
                }
                else
                {
                    Button7.Visible = true;
                }

                MP_nama.Text = select_app.Rows[0]["app_name"].ToString();
                MP_icno.Text = select_app.Rows[0]["app_new_icno"].ToString();
                //MP_wilayah.Text = select_app.Rows[0]["Wilayah_Name"].ToString();
                //MP_cawangan.Text = select_app.Rows[0]["branch_desc"].ToString();

                //gvSelected.Visible = false;
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Ic',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }
    protected void btnsrch_Click(object sender, EventArgs e)
    {
        try
        {
            srch_Click();
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }

    }


    protected void BindGridview(object sender, EventArgs e)
    {
        grid();
    }

    protected void grid()
    {
        gvSelected.Visible = true;
        con.Open();
        SqlCommand cmd = new SqlCommand("select pha_applcn_no,pha_seqno,pha_phase_no,pha_name,pha_reg_no,pha_bank_acc_no,pha_bank_cd,pha_pay_type_cd,pha_pay_amt,RNB.Bank_Name,RJB.KETERANGAN,ISNULL(CASE WHEN CONVERT(DATE, pha_pay_dt) = '1900-01-01' THEN '' ELSE CONVERT(CHAR(10), pha_pay_dt, 103) END, '') AS pha_pay_dt from jpa_disburse left join Ref_Nama_Bank as RNB ON RNB.Bank_Code=pha_bank_cd left join Ref_Jenis_Bayaran as RJB ON RJB.KETERANGAN_Code=pha_pay_type_cd where pha_applcn_no = '" + Applcn_no.Text + "' and pha_phase_no = '" + selphase_no + "' ORDER BY pha_seqno ASC", con);
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
            gvSelected.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
            gvSelected.FooterRow.Cells[6].Text = "<strong>JUMLAH AMAUN PENGELUARAN (RM)</strong>";
            gvSelected.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
        }
        else
        {
            gvSelected.DataSource = ds;
            gvSelected.DataBind();
            gvSelected.FooterRow.Cells[6].Text = "<strong>JUMLAH AMAUN PENGELUARAN (RM)</strong>";
            gvSelected.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            Button7.Visible = true;
        }
        con.Close();
    }
    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSelected.PageIndex = e.NewPageIndex;
        grid();
        gvSelected.DataBind();
    }

    protected void gvEmp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (DataBinder.Eval(e.Row.DataItem, "pha_pay_amt") != DBNull.Value)
            {
                //total += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "pha_pay_amt"));
                Decimal ss = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "pha_pay_amt"));
                total += ss;
            }

            var ddl = (DropDownList)e.Row.FindControl("Bank_details");
            string com = "select Bank_Name,Bank_Code from Ref_Nama_Bank WHERE Status = 'A' order by Bank_Name ASC";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddl.DataSource = dt;
            ddl.DataBind();
            ddl.DataTextField = "Bank_Name";
            ddl.DataValueField = "Bank_Code";
            ddl.DataBind();
            ddl.SelectedValue = ((DataRowView)e.Row.DataItem)["pha_bank_cd"].ToString();
            ddl.Items.Insert(0, new ListItem("--- PILIH ---", ""));



        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            System.Web.UI.WebControls.Label lblamount = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblTotal");
            lblamount.Text = total.ToString("0.00");
            MPP_jb.Text = total.ToString("0.00");
        }
    }


    protected void update_click(object sender, EventArgs e)
    {
        try
        {
            if (Applcn_no.Text != "")
            {
                if (RadioButton1.Checked == true || RadioButton2.Checked == true || RadioButton3.Checked == true)
                {
                    foreach (GridViewRow gvrow in gvSelected.Rows)
                    {
                        //var checkbox = gvrow.FindControl("chkSelect") as System.Web.UI.WebControls.CheckBox;
                        //if (checkbox.Checked == true)
                        //{
                        var lblID = gvrow.FindControl("app_no") as System.Web.UI.WebControls.Label;
                        var seqno = gvrow.FindControl("seq_no") as System.Web.UI.WebControls.Label;
                        var ph_no1 = gvrow.FindControl("Label3") as System.Web.UI.WebControls.Label;

                        //System.Web.UI.WebControls.TextBox box1 = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("Label2");
                        System.Web.UI.WebControls.TextBox box2 = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("Label10");
                        System.Web.UI.WebControls.TextBox box3 = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("Label5");
                        DropDownList ddl = (DropDownList)gvrow.FindControl("Bank_details");
                        DropDownList ddl_cb = (DropDownList)gvrow.FindControl("CB_details");

                        string d2 = box2.Text;
                        if (d2 != "")
                        {
                            DateTime today1 = DateTime.ParseExact(d2, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                            phdate = today1.ToString("yyyy-mm-dd");
                            //Button7.Attributes.Add("Disabled", "Disabled");
                        }
                        else
                        {
                            phdate = "";
                        }

                        DBCon.Execute_CommamdText("Update jpa_disburse SET pha_bank_acc_no='" + box3.Text + "',pha_bank_cd='" + ddl.SelectedValue + "',pha_pay_dt='" + phdate + "' WHERE pha_applcn_no='" + lblID.Text + "' and pha_phase_no = '" + ph_no1.Text + "' and pha_seqno='" + seqno.Text + "'");

                    }

                    DataTable dt = new DataTable();
                    dt = DBCon.Ora_Execute_table("select jno_applcn_no from jpa_jbb_normal WHERE jno_applcn_no='" + Applcn_no.Text + "'");
                    DataTable dt_application = new DataTable();
                    dt_application = DBCon.Ora_Execute_table("select app_applcn_no,app_loan_amt,app_loan_type_cd from jpa_application WHERE app_applcn_no='" + Applcn_no.Text + "'");
                    DataTable dt_calculation = new DataTable();
                    dt_calculation = DBCon.Ora_Execute_table("select cal_applcn_no,cal_profit_amt,cal_installment_amt,cal_approve_amt,cal_pre_balance from jpa_calculate_fee WHERE cal_applcn_no='" + Applcn_no.Text + "'");
                    double a1 = 0;
                    if (dt_application.Rows[0]["app_loan_type_cd"].ToString() == "I")
                    {
                        a1 = (double.Parse(dt_application.Rows[0]["app_loan_amt"].ToString()) + double.Parse(dt_calculation.Rows[0]["cal_pre_balance"].ToString()));
                    }
                    else
                    {
                        a1 = double.Parse(dt_application.Rows[0]["app_loan_amt"].ToString());
                    }

                    //string a1 = dt_application.Rows[0]["app_loan_amt"].ToString();
                    string b1 = dt_calculation.Rows[0]["cal_profit_amt"].ToString();
                    string b2 = dt_calculation.Rows[0]["cal_installment_amt"].ToString();
                    string bal_amt = (a1 + double.Parse(b1)).ToString();
                    DataTable app_dur = new DataTable();
                    app_dur = DBCon.Ora_Execute_table("select jkk_approve_dur from jpa_jkkpa_approval WHERE jkk_applcn_no='" + Applcn_no.Text + "'");
                    string loan_dur = app_dur.Rows[0]["jkk_approve_dur"].ToString();

                    DataTable pay_dt = new DataTable();
                    pay_dt = DBCon.Ora_Execute_table("select pha_pay_dt from jpa_disburse WHERE pha_applcn_no='" + Applcn_no.Text + "' and pha_phase_no = '1' and pha_seqno='1'");

                    string sav_amt = string.Empty, emnth = string.Empty;
                    string s_1 = "51000.00";
                    string s_2 = "101000.00";
                    string s_3 = "201000.00";
                    string s_4 = "300000.00";
                    if (dt_application.Rows[0]["app_loan_type_cd"].ToString() != "P")
                    {
                        //sav_amt = ((0.1 / 100) * double.Parse(a1)).ToString("0.00");
                        emnth = "28";
                        if (a1 < double.Parse(s_1))
                        {
                            sav_amt = "50.00";
                        }
                        else if (a1 < double.Parse(s_2))
                        {
                            sav_amt = "100.00";
                        }
                        else if (a1 < double.Parse(s_3))
                        {
                            sav_amt = "200.00";
                        }
                        else if (a1 <= double.Parse(s_4))
                        {
                            sav_amt = "300.00";
                        }
                    }
                    else
                    {
                        sav_amt = "0.00";
                        emnth = "22";
                    }

                    string month = string.Empty;
                    string year = string.Empty;
                    DateTime thisDate = Convert.ToDateTime(pay_dt.Rows[0]["pha_pay_dt"].ToString());

                    month = thisDate.Month.ToString().PadLeft(2, '0');
                    year = thisDate.Year.ToString();
                    string d1 = "" + month + "/" + emnth + "/" + year;
                    //DateTime lastDayOfYear = date.AddDays(1).AddMonths(1).AddDays(-1);

                    if (dt.Rows.Count == 0)
                    {
                        DBCon.Execute_CommamdText("INSERT INTO jpa_jbb_normal (jno_applcn_no,jno_pay_date,jno_actual_pay_date,jno_seq_no,jno_approve_amt,jno_loan_amt,jno_profit_amt,jno_balance_amt,jno_crt_id,jno_crt_dt) VALUES ('" + Applcn_no.Text + "','" + pay_dt.Rows[0]["pha_pay_dt"].ToString() + "','1900-01-01','1','" + dt_calculation.Rows[0]["cal_approve_amt"].ToString() + "','" + a1 + "','" + dt_calculation.Rows[0]["cal_profit_amt"].ToString() + "','" + bal_amt + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");
                        DBCon.Execute_CommamdText("Update jpa_application SET app_start_pay_dt='" + pay_dt.Rows[0]["pha_pay_dt"].ToString() + "',app_current_jbb_ind='N',app_jbb_his='N',app_cumm_installment_amt='" + b2 + "',app_cumm_profit_amt='" + b1 + "' WHERE app_applcn_no='" + Applcn_no.Text + "'");
                        int i;
                        for (i = 1; i <= int.Parse(loan_dur); i++)
                        {
                            DateTime mm = Convert.ToDateTime(d1).AddMonths(i);
                            string pdt_month = mm.ToString("yyyy-MM-dd");

                            DBCon.Execute_CommamdText("INSERT INTO jpa_jbb_normal (jno_applcn_no,jno_pay_date,jno_actual_pay_date,jno_profit_amt,jno_seq_no,jno_approve_amt,jno_pay_amt,jno_saving_amt,jno_balance_amt,jno_day_late,jno_crt_id,jno_crt_dt) VALUES ('" + Applcn_no.Text + "','" + pdt_month + "','1900-01-01','" + dt_calculation.Rows[0]["cal_profit_amt"].ToString() + "','" + (i + 1) + "','" + dt_calculation.Rows[0]["cal_approve_amt"].ToString() + "','" + b2 + "','" + sav_amt + "','" + bal_amt + "','0','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");
                            //if (i == 1)
                            //{
                            //    DBCon.Execute_CommamdText("Update jpa_application SET app_start_pay_dt='" + pdt_month + "',app_sts_cd='N' WHERE jpa_applcn_no='" + Applcn_no.Text + "'");
                            //}
                            if (i == int.Parse(loan_dur))
                            {
                                DBCon.Execute_CommamdText("Update jpa_application SET app_end_pay_dt='" + pdt_month + "' WHERE app_applcn_no='" + Applcn_no.Text + "'");
                            }
                        }

                    }
                    string txn_bamt = (a1 + double.Parse(b1)).ToString("C").Replace("$","").Replace("RM", "");
                    DataTable ddokdicno_del = new DataTable();
                    ddokdicno_del = DBCon.Ora_Execute_table("select * from jpa_transaction where txn_applcn_no='" + Applcn_no.Text + "' and txn_cd = '00'");
                    if (ddokdicno_del.Rows.Count == 0)
                    {
                        DBCon.Execute_CommamdText("insert into jpa_transaction (txn_applcn_no,txn_cd,txn_debit_amt,txn_credit_amt,txn_gst_amt,txn_bal_amt,txn_crt_id,txn_crt_dt) values ('" + Applcn_no.Text + "','00','0.00','0.00','0.00','" + txn_bamt + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");
                    }

                    tab1();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila masukkan no ic',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila masukkan no ic',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }

    }

    public void tab1()
    {
        if (RadioButton1.Checked == true)
        {
            if (Applcn_no.Text != "")
            {
                selphase_no = "1";
                grid();
                RadioButton2.Checked = false;
                RadioButton3.Checked = false;
            }
            else
            {
                RadioButton2.Checked = false;
                RadioButton3.Checked = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Ic',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
    }

    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton1.Checked == true)
        {
            if (Applcn_no.Text != "")
            {
                selphase_no = "1";
                grid();
                RadioButton2.Checked = false;
                RadioButton3.Checked = false;
            }
            else
            {
                RadioButton2.Checked = false;
                RadioButton3.Checked = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Ic',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
    }
    protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton2.Checked == true)
        {
            if (Applcn_no.Text != "")
            {
                selphase_no = "2";
                grid();
                RadioButton1.Checked = false;
                RadioButton3.Checked = false;
            }
            else
            {
                RadioButton1.Checked = false;
                RadioButton3.Checked = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Ic',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
    }
    protected void RadioButton3_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton3.Checked == true)
        {
            if (Applcn_no.Text != "")
            {
                selphase_no = "3";
                grid();
                RadioButton1.Checked = false;
                RadioButton2.Checked = false;
            }
            else
            {
                RadioButton1.Checked = false;
                RadioButton2.Checked = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Ic',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void clk_bak(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Pelaburan_Anggota/PP_Kmp_view.aspx");
    }

}