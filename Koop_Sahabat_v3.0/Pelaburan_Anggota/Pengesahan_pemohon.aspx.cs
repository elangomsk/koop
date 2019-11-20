using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Globalization;


public partial class Pengesahan_pemohon : System.Web.UI.Page
{
    DBConnection dbcon = new DBConnection();
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string level, userid;
    DBConnection DBCon = new DBConnection();
    string Status = string.Empty;
    String count_text;
    int incNumber = 1;
    int incNumber1 = 1;
    string val17;
    string str_textbox10;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
                //TextBox1.Text = DateTime.Now.ToString("dd/MM/yyyy");
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
                com.CommandText = "select jpa_batch_no from jpa_application  where ISNULL(jpa_batch_no,'') != '' group by jpa_batch_no";
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
                            countryNames.Add(sdr["jpa_batch_no"].ToString());

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

    protected void BindGridview(object sender, EventArgs e)
    {
        if (TextBox12.Text != "")
        {
            //show_cnt1.Visible = true;
            grid();
        }
        else
        {
            //show_cnt1.Visible = false;            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Nama Kelompok.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }
    protected void grid()
    {
        Button3.Visible = true;
        Button4.Visible = true;
        SqlConnection con = new SqlConnection(conString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select app_applcn_no,JA.app_loan_type_cd,JA.app_new_icno,JA.app_age,JA.app_name,rt.tujuan_desc,JA.app_apply_amt,JA.app_apply_dur,JJA.jkk_approve_amt,JJA.jkk_approve_dur,cal_stamp_duty_amt,cal_process_fee,cal_deposit_amt,cal_installment_amt,cal_tkh_amt,cal_credit_fee,cal_profit_amt,cal_no_guarantor from jpa_application as JA LEFT join jpa_calculate_fee cf on cf.cal_applcn_no = JA.app_applcn_no Left join ref_tujuan as rt on rt.tujuan_cd=JA.app_loan_purpose_cd  Inner join jpa_jkkpa_approval as JJA ON JJA.jkk_applcn_no = JA.app_applcn_no where JJA.pa_batch_no='" + TextBox12.Text + "' and ISNULL(JJA.jkk_result_ind,'') =''", con);
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
            gvSelected.Rows[0].Cells[0].Text = "<center><strong>Rekod Tidak Dijumpai. Sila Lakukan Semula Carian</strong></center>";
            Button3.Visible = false;
            Button4.Visible = false;
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

   

    protected void submit_button(object sender, EventArgs e)
    {

        using (SqlConnection con = new SqlConnection(conString))
        {

            //string strDate = DateTime.Now.ToString("yyyy-MM-dd");
            string datedari = DateTime.Now.ToString("dd/MM/yyyy");

            DateTime dt = DateTime.ParseExact(datedari, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            String datetime = dt.ToString("yyyy-mm-dd");
            foreach (GridViewRow gvrow in gvSelected.Rows)
            {
                var app_no = gvrow.FindControl("Label3") as Label;
                var app_icno = gvrow.FindControl("Label2") as Label;
                var MPDP_amaun = gvrow.FindControl("Label5") as Label;
                var MPDP_tempoh = gvrow.FindControl("Label6") as Label;
                var TextBox13 = gvrow.FindControl("lbl_kut") as Label;
                var TxtBilangan = gvrow.FindControl("lbl_gua") as Label;
                var appage = gvrow.FindControl("lbl_age") as Label;
                var loan_type = gvrow.FindControl("lbl_ltype") as Label;

                RadioButton chkbox = (RadioButton)gvrow.FindControl("chkSelect_1");
                RadioButton chkbox1 = (RadioButton)gvrow.FindControl("chkSelect_2");
                if (chkbox.Checked == true)
                {
                    //tkh amount
                    string kadar = string.Empty;
                    if (MPDP_amaun.Text != "" && MPDP_tempoh.Text != "" && TxtBilangan.Text != "")
                    {
                       
                        DataTable dd_calc = new DataTable();
                        dd_calc = DBCon.Ora_Execute_table("select cal_profit_rate,cal_no_guarantor from jpa_calculate_fee where cal_applcn_no='" + app_no.Text + "'");
                        if (dd_calc.Rows.Count != 0)
                        {

                            kadar = dd_calc.Rows[0]["cal_profit_rate"].ToString();
                            //string bilangan = dd_calc.Rows[0]["cal_no_guarantor"].ToString();
                            string bilangan = TxtBilangan.Text;
                            string val1 = (double.Parse(MPDP_amaun.Text) * double.Parse(MPDP_tempoh.Text)).ToString();
                            //string val10 = (double.Parse(MPDP_amaun.Text)).ToString();
                            string val2 = ((double.Parse(val1) * (double.Parse(kadar) / 100)) / 12).ToString();
                            TextBox13.Text = (double.Parse(val2)).ToString("C").Replace("RM", "").Replace("$", "");
                            string c1 = "0.5";
                            string t = "100";
                            string c2 = "10";
                            string val3 = ((((double.Parse(c1) / double.Parse(t)) * double.Parse(MPDP_amaun.Text)))).ToString();
                            string val4 = (double.Parse(c2) * double.Parse(bilangan)).ToString();
                            //TextBox2.Text = (double.Parse(val3) + double.Parse(val4)).ToString("#,##.00");
                            //TextBox6.Text = "100.00";
                            string val9 = ((double.Parse(MPDP_amaun.Text) + double.Parse(val2)) / double.Parse(MPDP_tempoh.Text)).ToString("C").Replace("RM", "").Replace("$", "");
                            string val7 = (double.Parse(val9) * 2).ToString();
                            //TextBox5.Text = (double.Parse(val7)).ToString("#,##.00");
                            string val8 = (10 * (double.Parse(bilangan) + 1)).ToString("C").Replace("RM", "").Replace("$", "");
                            //TextBox9.Text = (double.Parse(val8)).ToString("#,##.00");

                            //TextBox7.Text = (double.Parse(val9)).ToString("#,##.00");

                            //string val11=txtage.Text.ToString();
                            //string val12 = "60";
                            string val11;

                            //Premium TKH (RM) :
                            DataTable cnt_tkh = new DataTable();
                            cnt_tkh = DBCon.Ora_Execute_table("select * from jpa_khairat where tkh_applcn_no='" + app_no.Text + "' order by tkh_seq_no");
                            string tkh1 = string.Empty, tkh2 = string.Empty;
                            if (cnt_tkh.Rows.Count >= 1)
                            {

                                if (double.Parse(appage.Text) < 60)
                                {
                                    double total = 0;
                                    int x = Int32.Parse(MPDP_tempoh.Text);
                                    for (int i = 0; i <= x; i += 12)
                                    {
                                        string val13 = (double.Parse(MPDP_tempoh.Text) - i).ToString();
                                        string val14 = double.Parse(val9).ToString();
                                        string val15 = "1000";
                                        string flval = (((double.Parse(val14) * double.Parse(val13)) / double.Parse(val15)) * 6).ToString();
                                        val17 = (double.Parse(flval)).ToString();
                                        total = total + Convert.ToDouble(val17);
                                        tkh1 = total.ToString("C").Replace("RM", "").Replace("$", "");
                                    }
                                }
                                else
                                {
                                    double total = 0;
                                    int x = Int32.Parse(MPDP_tempoh.Text);
                                    for (int i = 0; i <= x; i += 12)
                                    {
                                        string val13 = (double.Parse(MPDP_tempoh.Text) - i).ToString();
                                        string val14 = double.Parse(val9).ToString();
                                        string val15 = "1000";
                                        string flval = (((double.Parse(val14) * double.Parse(val13)) / double.Parse(val15)) * 12).ToString();
                                        val17 = (double.Parse(flval)).ToString();
                                        total = total + Convert.ToDouble(val17);
                                        tkh1 = total.ToString("C").Replace("RM", "").Replace("$", "");
                                    }
                                }
                            }


                            if (cnt_tkh.Rows.Count == 2)
                            {

                                if (double.Parse(cnt_tkh.Rows[1]["tkh_age"].ToString()) < 60)
                                {
                                    double total1 = 0;
                                    int x1 = Int32.Parse(MPDP_tempoh.Text);
                                    for (int i = 0; i <= x1; i += 12)
                                    {
                                        string val13 = (double.Parse(MPDP_tempoh.Text) - i).ToString();
                                        string val14 = double.Parse(val9).ToString();
                                        string val15 = "1000";
                                        string flval = (((double.Parse(val14) * double.Parse(val13)) / double.Parse(val15)) * 6).ToString();
                                        val17 = (double.Parse(flval)).ToString();
                                        total1 = total1 + Convert.ToDouble(val17);
                                        tkh2 = total1.ToString("C").Replace("RM", "").Replace("$", "");
                                    }
                                }
                                else
                                {
                                    double total1 = 0;
                                    int x1 = Int32.Parse(MPDP_tempoh.Text);
                                    for (int i = 0; i <= x1; i += 12)
                                    {
                                        string val13 = (double.Parse(MPDP_tempoh.Text) - i).ToString();
                                        string val14 = double.Parse(val9).ToString();
                                        string val15 = "1000";
                                        string flval = (((double.Parse(val14) * double.Parse(val13)) / double.Parse(val15)) * 12).ToString();
                                        val17 = (double.Parse(flval)).ToString();
                                        total1 = total1 + Convert.ToDouble(val17);
                                        tkh2 = total1.ToString("C").Replace("RM", "").Replace("$", "");
                                    }
                                }
                            }
                            if (tkh2 != "")
                            {
                                str_textbox10 = (double.Parse(tkh1) + double.Parse(tkh2)).ToString("C").Replace("RM", "").Replace("$", "");
                            }
                            else
                            {
                                str_textbox10 = (double.Parse(tkh1) + 0).ToString("C").Replace("RM", "").Replace("$", "");
                            }
                        }
                    }




                 
                        DataTable ddicno2 = new DataTable();
                        ddicno2 = DBCon.Ora_Execute_table("select JA.app_new_icno,JA.app_applcn_no from jpa_application as JA Inner Join jpa_jkkpa_approval as JKA ON JKA.jkk_applcn_no = JA.app_applcn_no  where JA.app_applcn_no='" + app_no.Text + "'");

                        if (ddicno2.Rows.Count != 0)
                        {


                            DBCon.Execute_CommamdText("Update jpa_calculate_fee SET cal_tkh_amt='" + str_textbox10 + "',cal_profit_amt='" + TextBox13.Text + "', cal_approve_amt = '" + MPDP_amaun.Text + "', cal_approve_dur = '" + MPDP_tempoh.Text + "',cal_upd_id = '" + Session["New"].ToString() + "', cal_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE cal_applcn_no = '" + app_no.Text + "'");

                            string sts = string.Empty;
                            string appsts = string.Empty;
                            if (chkbox.Checked == true)
                            {
                                sts = "L";
                                appsts = "Y";
                            }
                            else
                            {
                                sts = "T";
                                appsts = "R";
                            }
                            string txn_bamt = string.Empty;
                           string txt_damt = string.Empty;
                        string txt_Camt = string.Empty;

                        DataTable Check_status = new DataTable();
                            DBCon.Execute_CommamdText("Update jpa_application SET app_loan_amt = '" + MPDP_amaun.Text + "',app_sts_cd='" + appsts + "', appl_loan_dur = '" + MPDP_tempoh.Text + "' WHERE app_applcn_no = '" + app_no.Text + "'");

                            txn_bamt = (double.Parse(MPDP_amaun.Text) + double.Parse(TextBox13.Text)).ToString("0.00");
                            txt_damt= (double.Parse(MPDP_amaun.Text) * (double.Parse(kadar) / 100) * (double.Parse(MPDP_tempoh.Text)/12) / double.Parse(MPDP_tempoh.Text)).ToString();
                            txt_Camt = (double.Parse(MPDP_amaun.Text)  / double.Parse(MPDP_tempoh.Text)).ToString();
                        DBCon.Execute_CommamdText("insert into jpa_transaction (txn_applcn_no,txn_cd,txn_debit_amt,txn_credit_amt,txn_bal_amt,txn_crt_id,txn_crt_dt) values ('" + app_no.Text + "','01','" + txt_damt + "','" + txt_Camt + "','" + txn_bamt + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");
                            DBCon.Execute_CommamdText("Update jpa_jkkpa_approval SET jkk_result_ind = '" + sts + "', jkk_upd_id = '" + Session["New"].ToString() + "', jkk_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE jkk_applcn_no = '" + app_no.Text + "'");
                            DataTable chng_sts = new DataTable();
                            chng_sts = DBCon.Ora_Execute_table("select * from jpa_application where app_new_icno='" + app_icno.Text + "' and applcn_clsed ='N'  order by Created_date ASC");
                            if (chng_sts.Rows.Count > 1)
                            {
                                DBCon.Execute_CommamdText("Update jpa_application SET applcn_clsed = 'Y' WHERE app_applcn_no = '" + chng_sts.Rows[0]["app_applcn_no"].ToString() + "'");

                                //DataTable get_pre_loan = new DataTable();
                                //get_pre_loan = DBCon.Ora_Execute_table("select top(1)txn_bal_amt from jpa_transaction where txn_applcn_no='"+ chng_sts.Rows[0]["app_applcn_no"].ToString() + "' order by txn_crt_dt desc");
                                //string pre_lamt = string.Empty;
                                //if(get_pre_loan.Rows.Count != 0)
                                //{
                                //    pre_lamt = get_pre_loan.Rows[0]["txn_bal_amt"].ToString();
                                //}
                                //else
                                //{
                                //    pre_lamt = "0.00";
                                //}

                                //if (loan_type.Text == "I")
                                //{
                                //    decimal lamt1 = decimal.Parse(txn_bamt) + decimal.Parse(pre_lamt);
                                //    DBCon.Execute_CommamdText("insert into jpa_transaction (txn_applcn_no,txn_cd,txn_debit_amt,txn_bal_amt,txn_crt_id,txn_crt_dt) values ('" + app_no.Text + "','01','"+ pre_lamt + "','" + lamt1 + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");
                                //}
                                //else
                                //{
                                //    decimal lamt1 = decimal.Parse(txn_bamt) - decimal.Parse(pre_lamt);
                                //    DBCon.Execute_CommamdText("insert into jpa_transaction (txn_applcn_no,txn_cd,txn_credit_amt,txn_bal_amt,txn_crt_id,txn_crt_dt) values ('" + app_no.Text + "','01','"+ pre_lamt + "','" + lamt1 + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");
                                //}

                            }


                            grid();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success'});", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Ic No wujud',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                        }

                   
                }

            }

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.Maklumat Login Pengguna Berjaya Diwujudkan.',{'type': 'confirmation','title': 'Success'});window.location ='../keanggotan/Pengesahan_Anggota1.aspx';", true);
        }
    }

    protected void Reset_btn(object sender, EventArgs e)
    {
        Response.Redirect("../Pelaburan_Anggota/Pengesahan_pemohon.aspx");
    }
}