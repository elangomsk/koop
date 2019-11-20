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

public partial class PP_sel_jbb : System.Web.UI.Page
{
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);

    DBConnection DBCon = new DBConnection();
    SqlCommand grid_cmd;
    DataTable dt = new DataTable();
    string Status = string.Empty;
    string level, userid;
    string cc_no = string.Empty;
    string c_jbb = string.Empty;
    decimal total = 0M;
    decimal total1 = 0M;
    decimal total2 = 0M;
    decimal total3 = 0M;
    int counter = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Button10);

        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                level = Session["level"].ToString();
                userid = Session["New"].ToString();
                txtname.Attributes.Add("Readonly", "Readonly");
                //txtwil.Attributes.Add("Readonly", "Readonly");
                txtjumla.Attributes.Add("Readonly", "Readonly");
                //txtcaw.Attributes.Add("Readonly", "Readonly");
                txttempoh.Attributes.Add("Readonly", "Readonly");
                txtamaun.Attributes.Add("Readonly", "Readonly");
                txttemp.Attributes.Add("Readonly", "Readonly");
                TextBox2.Attributes.Add("Readonly", "Readonly");
                TextBox3.Attributes.Add("Readonly", "Readonly");
                TextBox4.Attributes.Add("Readonly", "Readonly");
                TextBox5.Attributes.Add("Readonly", "Readonly");
                grid();
                if (Session["sess_no"] != "" && Session["sess_no"] != null)
                {
                    Applcn_no.Text = Session["sess_no"].ToString();
                    srch();
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
        srch();

    }
    protected void srch()
    {
        try
        {
            Session.Remove("sess_no");
            if (Applcn_no.Text != "")
            {

                DataTable ddicno = new DataTable();
                ddicno = DBCon.Ora_Execute_table("select distinct app_applcn_no from jpa_application JA where JA.app_sts_cd='Y' and '" + Applcn_no.Text + "' IN(JA.app_applcn_no, JA.app_new_icno) and ISNULL(app_current_jbb_ind,'') not in ('N')");

                if (ddicno.Rows.Count != 0)
                {
                    DataTable select_app = new DataTable();
                    select_app = DBCon.Ora_Execute_table("select JA.app_applcn_no,JA.app_new_icno,JA.app_name,RW.Wilayah_Name,ISNULL(RB.branch_desc, '') AS branch_desc,JA.app_loan_amt,JA.appl_loan_dur,JA.app_permnt_address,ja.app_permnt_postcode,ja.app_permnt_state_cd,ja.app_phone_h,ja.app_phone_m,ja.app_phone_o,ja.app_mailing_address,JA.app_mailing_postcode,ja.app_mailing_state_cd,ISNULL(JA.app_cumm_installment_amt,'') as app_cumm_installment_amt,ISNULL(JA.app_cumm_pay_amt,'') as app_cumm_pay_amt,ISNULL(JA.app_backdated_amt,'') as app_backdated_amt,ISNULL(JA.app_cumm_profit_amt,'') as app_cumm_profit_amt,ISNULL(JA.app_cumm_saving_amt,'') as app_cumm_saving_amt,ISNULL(JA.app_bal_loan_amt,'') as app_bal_loan_amt,ISNULL(jp.apj_apply_sts_cd,'') as apj_apply_sts_cd,ISNULL(jp.apj_apply_remark,'') as apj_apply_remark,ISNULL(jc.cal_approve_amt,'') as cal_approve_amt,ISNULL(jc.cal_profit_amt,'') as cal_profit_amt  from jpa_application as JA Left join jpa_calculate_fee as jc on jc.cal_applcn_no=ja.app_applcn_no  Left Join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd Left join ref_branch AS RB ON RB.branch_cd=JA.app_branch_cd left join jpa_pjs_application as jp on jp.apj_applcn_no=ja.app_applcn_no where ja.app_applcn_no='" + ddicno.Rows[0]["app_applcn_no"] + "'");

                    txtname.Text = select_app.Rows[0]["app_name"].ToString();
                    //txtwil.Text = select_app.Rows[0]["Wilayah_Name"].ToString();
                    decimal amt1 = (decimal)select_app.Rows[0]["app_loan_amt"];
                    txtjumla.Text = amt1.ToString("C").Replace("$", "");
                    //txtcaw.Text = select_app.Rows[0]["branch_desc"].ToString();
                    txttempoh.Text = select_app.Rows[0]["appl_loan_dur"].ToString();

                    decimal a1 = (decimal)select_app.Rows[0]["app_cumm_installment_amt"];
                    txtamaun.Text = a1.ToString("C").Replace("$", "");
                    decimal a2 = (decimal)select_app.Rows[0]["app_cumm_pay_amt"];
                    txttemp.Text = a2.ToString("C").Replace("$", "");
                    decimal a3 = (decimal)select_app.Rows[0]["app_backdated_amt"];
                    TextBox2.Text = a3.ToString("C").Replace("$", "");
                    decimal a4 = (decimal)select_app.Rows[0]["app_cumm_profit_amt"];
                    TextBox3.Text = a4.ToString("C").Replace("$", "");
                    decimal a5 = (decimal)select_app.Rows[0]["app_cumm_saving_amt"];
                    TextBox4.Text = a5.ToString("C").Replace("$", "");
                    decimal a6 = (decimal)select_app.Rows[0]["app_bal_loan_amt"];
                    TextBox5.Text = a6.ToString("C").Replace("$", "");
                    grid();

                }
                else
                {
                    Applcn_no.Text = "";

                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    grid();

                }

            }
            else
            {
                grid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Ic OR NO KP Baru.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Semak Isu.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }

    protected void gvEmp_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            System.Web.UI.WebControls.Label lblctrl = (System.Web.UI.WebControls.Label)e.Row.FindControl("seqno");
            System.Web.UI.WebControls.TextBox l2 = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("Label2");


            //if (DataBinder.Eval(e.Row.DataItem, "seqno") != DBNull.Value)
            //{

            //    System.Web.UI.WebControls.TextBox l2 = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("Label2");
            //    System.Web.UI.WebControls.TextBox samt = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("s_bayar");

            //    //int lb_val = Int32.Parse(lblctrl.Text);

            //    for (int k = 0; k <= double.Parse(lblctrl.Text); k++)
            //    {
            //        //total += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "pha_pay_amt"));
            //        Decimal ss = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "s_bayar"));
            //        total += ss;
            //    }

            //    System.Web.UI.WebControls.TextBox jsimp = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("j_simpan");
            //    jsimp.Text = total.ToString("0.00");


            //}
            if (lblctrl.Text != "")
            {
                var ic_count1 = Applcn_no.Text.Length;
                DataTable app_icno1 = new DataTable();
                app_icno1 = DBCon.Ora_Execute_table("select app_applcn_no,isnull(app_current_jbb_ind,'') AS app_current_jbb_ind from jpa_application JA where '" + Applcn_no.Text + "' IN(JA.app_applcn_no, JA.app_new_icno)");
                if (ic_count1 == 12)
                {
                    cc_no = app_icno1.Rows[0]["app_applcn_no"].ToString();
                }
                else
                {
                    cc_no = Applcn_no.Text;
                }
                string tab_name = string.Empty;
                string col_name = string.Empty;

                if (app_icno1.Rows[0]["app_current_jbb_ind"].ToString() == "P")
                {
                    tab_name = "pjs";
                    col_name = "pjs";
                }
                else if (app_icno1.Rows[0]["app_current_jbb_ind"].ToString() == "E")
                {
                    tab_name = "extension";
                    col_name = "ext";
                }
                else if (app_icno1.Rows[0]["app_current_jbb_ind"].ToString() == "L")
                {
                    tab_name = "writeoff";
                    col_name = "jwo";
                }
                else if (app_icno1.Rows[0]["app_current_jbb_ind"].ToString() == "H")
                {
                    tab_name = "holiday";
                    col_name = "hol";
                }

                DataTable get_det = new DataTable();
                get_det = DBCon.Ora_Execute_table("select sum(" + col_name + "_actual_saving_amt) as as_amt from jpa_jbb_" + tab_name + " where " + col_name + "_applcn_no='" + cc_no + "' and " + col_name + "_seq_no <= '" + lblctrl.Text + "'");

                System.Web.UI.WebControls.TextBox jsimp = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("j_simpan");
                jsimp.Text = double.Parse(get_det.Rows[0]["as_amt"].ToString()).ToString("0.00");

            }

            l2.Attributes.Add("Readonly", "Readonly");
            if (lblctrl.Text == "1")
            {
                //Hide the Particular row
                e.Row.Visible = false;
            }
        }

    }

    protected void txtW_TextChanged(object sender, EventArgs e)
    {
        System.Web.UI.WebControls.TextBox ddl = (System.Web.UI.WebControls.TextBox)sender;
        GridViewRow row = (GridViewRow)ddl.NamingContainer;
        var ss = row.RowIndex - 1;
        System.Web.UI.WebControls.TextBox apayamt = (System.Web.UI.WebControls.TextBox)row.FindControl("b_bayar");

        GridViewRow gridRow = ((GridViewRow)((System.Web.UI.WebControls.TextBox)sender).NamingContainer);
        int index = gridRow.RowIndex - 1;
        int index1 = gridRow.RowIndex;

        System.Web.UI.WebControls.TextBox jum_bay1 = (System.Web.UI.WebControls.TextBox)gvSelected.Rows[index1].FindControl("j_bay");
        System.Web.UI.WebControls.TextBox bia_kena1 = (System.Web.UI.WebControls.TextBox)gvSelected.Rows[index1].FindControl("bk");
        System.Web.UI.WebControls.TextBox sakena = (System.Web.UI.WebControls.TextBox)gvSelected.Rows[index1].FindControl("s_kena");
        System.Web.UI.WebControls.TextBox sbayar1 = (System.Web.UI.WebControls.TextBox)gvSelected.Rows[index1].FindControl("s_bayar");



        System.Web.UI.WebControls.TextBox balamt = (System.Web.UI.WebControls.TextBox)gvSelected.Rows[index].FindControl("b_pemb");
        string test = (double.Parse(balamt.Text) - double.Parse(apayamt.Text)).ToString("0.00");

        System.Web.UI.WebControls.TextBox lblamount = (System.Web.UI.WebControls.TextBox)gvSelected.Rows[index1].FindControl("b_pemb");
        lblamount.Text = test;

        System.Web.UI.WebControls.TextBox lblamount2_lamt = (System.Web.UI.WebControls.TextBox)gvSelected.Rows[index1].FindControl("Label6");

        System.Web.UI.WebControls.TextBox sim_baya = (System.Web.UI.WebControls.TextBox)gvSelected.Rows[index1].FindControl("s_bayar");
        System.Web.UI.WebControls.TextBox jum_baya = (System.Web.UI.WebControls.TextBox)gvSelected.Rows[index1].FindControl("j_bay");
        System.Web.UI.WebControls.TextBox jsimp = (System.Web.UI.WebControls.TextBox)gvSelected.Rows[index].FindControl("j_simpan");

        if (double.Parse(apayamt.Text) >= double.Parse(bia_kena1.Text))
        {

            sim_baya.Text = sakena.Text;
            string jbay = (double.Parse(apayamt.Text) + double.Parse(sakena.Text)).ToString("0.00");
            jum_baya.Text = jbay;
            string test_le_amt = (double.Parse(jum_baya.Text) - double.Parse(bia_kena1.Text) - double.Parse(sim_baya.Text)).ToString("C").Replace("$", "");
            lblamount2_lamt.Text = test_le_amt;
        }
        else
        {
            sim_baya.Text = "0.00";
            string jbay = (double.Parse(apayamt.Text)).ToString("0.00");
            jum_baya.Text = jbay;
            string test_le_amt = (double.Parse(jum_baya.Text) - double.Parse(bia_kena1.Text) - double.Parse(sim_baya.Text)).ToString("C").Replace("$", "");
            lblamount2_lamt.Text = test_le_amt;
        }
        if (sim_baya.Text != "0.00")
        {

            string js = (double.Parse(jsimp.Text) + double.Parse(sim_baya.Text)).ToString("0.00");
            System.Web.UI.WebControls.TextBox jsimp1 = (System.Web.UI.WebControls.TextBox)gvSelected.Rows[index1].FindControl("j_simpan");
            jsimp1.Text = js;
        }
        for (int h = index1 + 1; h <= gvSelected.Rows.Count - 1; h++)
        {
            int index_se = h - 1;
            System.Web.UI.WebControls.TextBox balamt1 = (System.Web.UI.WebControls.TextBox)gvSelected.Rows[index_se].FindControl("b_pemb");
            System.Web.UI.WebControls.TextBox apayamt1 = (System.Web.UI.WebControls.TextBox)gvSelected.Rows[h].FindControl("b_bayar");
            System.Web.UI.WebControls.TextBox jum_bay = (System.Web.UI.WebControls.TextBox)gvSelected.Rows[h].FindControl("j_bay");
            System.Web.UI.WebControls.TextBox bia_kena = (System.Web.UI.WebControls.TextBox)gvSelected.Rows[h].FindControl("bk");
            System.Web.UI.WebControls.TextBox sbayar = (System.Web.UI.WebControls.TextBox)gvSelected.Rows[h].FindControl("s_bayar");

            string test1 = (double.Parse(balamt1.Text) - double.Parse(apayamt1.Text)).ToString("0.00");
            string test2 = (double.Parse(jum_bay.Text) - double.Parse(bia_kena.Text) - double.Parse(sbayar.Text)).ToString("C").Replace("$", "");
            System.Web.UI.WebControls.TextBox lblamount1 = (System.Web.UI.WebControls.TextBox)gvSelected.Rows[h].FindControl("b_pemb");
            lblamount1.Text = test1;
            if (apayamt1.Text != "0.00")
            {
                System.Web.UI.WebControls.TextBox lblamount2 = (System.Web.UI.WebControls.TextBox)gvSelected.Rows[h].FindControl("Label6");
                lblamount2.Text = test2;
            }

            if (sim_baya.Text != "0.00")
            {
                System.Web.UI.WebControls.TextBox jsimp_s = (System.Web.UI.WebControls.TextBox)gvSelected.Rows[h].FindControl("j_simpan");
                string js_s = (double.Parse(jsimp_s.Text) + double.Parse(sim_baya.Text)).ToString("0.00");
                System.Web.UI.WebControls.TextBox jsimp1_s = (System.Web.UI.WebControls.TextBox)gvSelected.Rows[h].FindControl("j_simpan");
                jsimp1_s.Text = js_s;
            }
        }



    }

    protected void txtW_TextChanged1(object sender, EventArgs e)
    {
        System.Web.UI.WebControls.TextBox ddl = (System.Web.UI.WebControls.TextBox)sender;
        GridViewRow row = (GridViewRow)ddl.NamingContainer;
        var ss = row.RowIndex - 1;
        System.Web.UI.WebControls.TextBox apayamt = (System.Web.UI.WebControls.TextBox)row.FindControl("b_bayar");

        GridViewRow gridRow = ((GridViewRow)((System.Web.UI.WebControls.TextBox)sender).NamingContainer);
        int index = gridRow.RowIndex - 1;
        int index1 = gridRow.RowIndex;

        System.Web.UI.WebControls.TextBox jum_bay1 = (System.Web.UI.WebControls.TextBox)gvSelected.Rows[index1].FindControl("j_bay");
        System.Web.UI.WebControls.TextBox bia_kena1 = (System.Web.UI.WebControls.TextBox)gvSelected.Rows[index1].FindControl("bk");
        System.Web.UI.WebControls.TextBox sakena = (System.Web.UI.WebControls.TextBox)gvSelected.Rows[index1].FindControl("s_kena");
        System.Web.UI.WebControls.TextBox sbayar1 = (System.Web.UI.WebControls.TextBox)gvSelected.Rows[index1].FindControl("s_bayar");



        System.Web.UI.WebControls.TextBox balamt = (System.Web.UI.WebControls.TextBox)gvSelected.Rows[index].FindControl("b_pemb");
        string test = (double.Parse(balamt.Text) - double.Parse(apayamt.Text)).ToString("0.00");

        System.Web.UI.WebControls.TextBox lblamount = (System.Web.UI.WebControls.TextBox)gvSelected.Rows[index1].FindControl("b_pemb");
        lblamount.Text = test;

        System.Web.UI.WebControls.TextBox lblamount2_lamt = (System.Web.UI.WebControls.TextBox)gvSelected.Rows[index1].FindControl("Label6");

        System.Web.UI.WebControls.TextBox sim_baya = (System.Web.UI.WebControls.TextBox)gvSelected.Rows[index1].FindControl("s_bayar");
        System.Web.UI.WebControls.TextBox jum_baya = (System.Web.UI.WebControls.TextBox)gvSelected.Rows[index1].FindControl("j_bay");
        System.Web.UI.WebControls.TextBox jsimp = (System.Web.UI.WebControls.TextBox)gvSelected.Rows[index].FindControl("j_simpan");

        if (double.Parse(apayamt.Text) >= double.Parse(bia_kena1.Text))
        {

            sim_baya.Text = sakena.Text;

            string test_le_amt = (double.Parse(jum_baya.Text) - double.Parse(bia_kena1.Text) - double.Parse(sim_baya.Text)).ToString("C").Replace("$", "");
            lblamount2_lamt.Text = test_le_amt;
        }
        else
        {
            sim_baya.Text = "0.00";
            string test_le_amt = (double.Parse(jum_baya.Text) - double.Parse(bia_kena1.Text) - double.Parse(sim_baya.Text)).ToString("C").Replace("$", "");
            lblamount2_lamt.Text = test_le_amt;
        }
        if (sim_baya.Text != "0.00")
        {

            string js = (double.Parse(jsimp.Text) + double.Parse(sim_baya.Text)).ToString("0.00");
            System.Web.UI.WebControls.TextBox jsimp1 = (System.Web.UI.WebControls.TextBox)gvSelected.Rows[index1].FindControl("j_simpan");
            jsimp1.Text = js;
        }




    }

    void grid()
    {

        var ic_count1 = Applcn_no.Text.Length;
        DataTable app_icno1 = new DataTable();
        app_icno1 = DBCon.Ora_Execute_table("select app_applcn_no,isnull(app_current_jbb_ind,'') AS app_current_jbb_ind from jpa_application JA where '" + Applcn_no.Text + "' IN(JA.app_applcn_no, JA.app_new_icno)");
        if (ic_count1 == 12)
        {
            cc_no = app_icno1.Rows[0]["app_applcn_no"].ToString();
        }
        else
        {
            cc_no = Applcn_no.Text;
        }
        con.Open();

        if (app_icno1.Rows.Count != 0)
        {
            c_jbb = app_icno1.Rows[0]["app_current_jbb_ind"].ToString();
            if (c_jbb == "P")
            {
                grid_cmd = new SqlCommand("select * from (SELECT jn.pjs_applcn_no as pjs_applcn_no,FORMAT(jn.pjs_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.pjs_loan_amt,'')) when '0.00' then '' else ISNULL(jn.pjs_loan_amt,'') end as l_amt,jn.pjs_seq_no as seq_no,ISNULL(jn.pjs_approve_amt,'') as a_amt,ISNULL(jn.pjs_profit_amt,'') as pr_amt,ISNULL(case when CONVERT(DATE, jn.pjs_actual_pay_date) = '1900-01-01' then '' else CONVERT(CHAR(10), FORMAT(jn.pjs_actual_pay_date,'dd/MM/yyyy', 'en-us'), 103) END, '') as ap_dt,ISNULL(jn.pjs_pay_amt,'') as p_amt,ISNULL(jn.pjs_actual_pay_amt,'') as ap_amt,ISNULL(jn.pjs_late_excess_amt,'') as le_amt,ISNULL(jn.pjs_saving_amt,'') as sa_amt,ISNULL(jn.pjs_actual_saving_amt,'') as as_amt,ISNULL(jn.pjs_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.pjs_total_pay_amt,'') as tp_amt,ISNULL(jn.pjs_balance_amt,'') as bal_amt,ISNULL(jn.pjs_total_saving_amt,'') as ts_amt,jn.pjs_day_late as dlate FROM jpa_jbb_pjs  as jn where jn.pjs_applcn_no='" + cc_no + "' ) as a full outer join (select pjs_applcn_no,sum(pjs_pay_amt) as tot_pamt,sum(pjs_actual_pay_amt) as tot_apamt,sum(pjs_saving_amt) as tot_samt,sum(pjs_actual_saving_amt) as tot_asamt,sum(pjs_total_pay_amt) as tot_tpamt from jpa_jbb_pjs where pjs_applcn_no='" + cc_no + "' group by pjs_applcn_no) as b on b.pjs_applcn_no=a.pjs_applcn_no full outer join (select * from jpa_calculate_fee as jc where jc.cal_applcn_no='" + cc_no + "') as c on c.cal_applcn_no=b.pjs_applcn_no full outer join (select ja.app_applcn_no as app_no,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno,rjp.Description from jpa_application as ja left join Ref_Jenis_Pelaburan as rjp on rjp.Description_Code=ja.app_loan_type_cd where ja.app_applcn_no = '" + cc_no + "') as d on d.app_no=c.cal_applcn_no", con);
            }
            else if (c_jbb == "L")
            {
                grid_cmd = new SqlCommand("select * from (SELECT jn.jwo_applcn_no as jwo_applcn_no,FORMAT(jn.jwo_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.jwo_loan_amt,'')) when '0.00' then '' else ISNULL(jn.jwo_loan_amt,'') end as l_amt,jn.jwo_seq_no as seq_no,ISNULL(jn.jwo_approve_amt,'') as a_amt,ISNULL(jn.jwo_profit_amt,'') as pr_amt,ISNULL(case when CONVERT(DATE, jn.jwo_actual_pay_date) = '1900-01-01' then '' else CONVERT(CHAR(10), FORMAT(jn.jwo_actual_pay_date,'dd/MM/yyyy', 'en-us'), 103) END, '') as ap_dt,ISNULL(jn.jwo_pay_amt,'') as p_amt,ISNULL(jn.jwo_actual_pay_amt,'') as ap_amt,ISNULL(jn.jwo_late_excess_amt,'') as le_amt,ISNULL(jn.jwo_saving_amt,'') as sa_amt,ISNULL(jn.jwo_actual_saving_amt,'') as as_amt,ISNULL(jn.jwo_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.jwo_total_pay_amt,'') as tp_amt,ISNULL(jn.jwo_balance_amt,'') as bal_amt,ISNULL(jn.jwo_total_saving_amt,'') as ts_amt,jn.jwo_day_late as dlate FROM jpa_jbb_writeoff  as jn where jn.jwo_applcn_no='" + cc_no + "' ) as a full outer join (select jwo_applcn_no,sum(jwo_pay_amt) as tot_pamt,sum(jwo_actual_pay_amt) as tot_apamt,sum(jwo_saving_amt) as tot_samt,sum(jwo_actual_saving_amt) as tot_asamt,sum(jwo_total_pay_amt) as tot_tpamt from jpa_jbb_writeoff where jwo_applcn_no='" + cc_no + "' group by jwo_applcn_no) as b on b.jwo_applcn_no=a.jwo_applcn_no full outer join (select * from jpa_calculate_fee as jc where jc.cal_applcn_no='" + cc_no + "') as c on c.cal_applcn_no=b.jwo_applcn_no full outer join (select ja.app_applcn_no as app_no,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno,rjp.Description from jpa_application as ja left join Ref_Jenis_Pelaburan as rjp on rjp.Description_Code=ja.app_loan_type_cd where ja.app_applcn_no = '" + cc_no + "') as d on d.app_no=c.cal_applcn_no", con);
            }
            else if (c_jbb == "H")
            {
                grid_cmd = new SqlCommand("select * from (SELECT jn.hol_applcn_no as hol_applcn_no,FORMAT(jn.hol_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.hol_loan_amt,'')) when '0.00' then '' else ISNULL(jn.hol_loan_amt,'') end as l_amt,jn.hol_seq_no as seq_no,ISNULL(jn.hol_approve_amt,'') as a_amt,ISNULL(jn.hol_profit_amt,'') as pr_amt,ISNULL(case when CONVERT(DATE, jn.hol_actual_pay_date) = '1900-01-01' then '' else CONVERT(CHAR(10), FORMAT(jn.hol_actual_pay_date,'dd/MM/yyyy', 'en-us'), 103) END, '') as ap_dt,ISNULL(jn.hol_pay_amt,'') as p_amt,ISNULL(jn.hol_actual_pay_amt,'') as ap_amt,ISNULL(jn.hol_late_excess_amt,'') as le_amt,ISNULL(jn.hol_saving_amt,'') as sa_amt,ISNULL(jn.hol_actual_saving_amt,'') as as_amt,ISNULL(jn.hol_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.hol_total_pay_amt,'') as tp_amt,ISNULL(jn.hol_balance_amt,'') as bal_amt,ISNULL(jn.hol_total_saving_amt,'') as ts_amt,jn.hol_day_late as dlate FROM jpa_jbb_holiday  as jn where jn.hol_applcn_no='" + cc_no + "' ) as a full outer join (select hol_applcn_no,sum(hol_pay_amt) as tot_pamt,sum(hol_actual_pay_amt) as tot_apamt,sum(hol_saving_amt) as tot_samt,sum(hol_actual_saving_amt) as tot_asamt,sum(hol_total_pay_amt) as tot_tpamt from jpa_jbb_holiday where hol_applcn_no='" + cc_no + "' group by hol_applcn_no) as b on b.hol_applcn_no=a.hol_applcn_no full outer join (select * from jpa_calculate_fee as jc where jc.cal_applcn_no='" + cc_no + "') as c on c.cal_applcn_no=b.hol_applcn_no full outer join (select ja.app_applcn_no as app_no,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno,rjp.Description from jpa_application as ja left join Ref_Jenis_Pelaburan as rjp on rjp.Description_Code=ja.app_loan_type_cd where ja.app_applcn_no = '" + cc_no + "') as d on d.app_no=c.cal_applcn_no", con);
            }
            else if (c_jbb == "E")
            {
                grid_cmd = new SqlCommand("select * from (SELECT jn.ext_applcn_no as ext_applcn_no,FORMAT(jn.ext_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.ext_loan_amt,'')) when '0.00' then '' else ISNULL(jn.ext_loan_amt,'') end as l_amt,jn.ext_seq_no as seq_no,ISNULL(jn.ext_approve_amt,'') as a_amt,ISNULL(jn.ext_profit_amt,'') as pr_amt,ISNULL(case when CONVERT(DATE, jn.ext_actual_pay_date) = '1900-01-01' then '' else CONVERT(CHAR(10), FORMAT(jn.ext_actual_pay_date,'dd/MM/yyyy', 'en-us'), 103) END, '') as ap_dt,ISNULL(jn.ext_pay_amt,'') as p_amt,ISNULL(jn.ext_actual_pay_amt,'') as ap_amt,ISNULL(jn.ext_late_excess_amt,'') as le_amt,ISNULL(jn.ext_saving_amt,'') as sa_amt,ISNULL(jn.ext_actual_saving_amt,'') as as_amt,ISNULL(jn.ext_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.ext_total_pay_amt,'') as tp_amt,ISNULL(jn.ext_balance_amt,'') as bal_amt,ISNULL(jn.ext_total_saving_amt,'') as ts_amt,jn.ext_day_late as dlate FROM jpa_jbb_extension  as jn where jn.ext_applcn_no='" + cc_no + "' ) as a full outer join (select ext_applcn_no,sum(ext_pay_amt) as tot_pamt,sum(ext_actual_pay_amt) as tot_apamt,sum(ext_saving_amt) as tot_samt,sum(ext_actual_saving_amt) as tot_asamt,sum(ext_total_pay_amt) as tot_tpamt from jpa_jbb_extension where ext_applcn_no='" + cc_no + "' group by ext_applcn_no) as b on b.ext_applcn_no=a.ext_applcn_no full outer join (select * from jpa_calculate_fee as jc where jc.cal_applcn_no='" + cc_no + "') as c on c.cal_applcn_no=b.ext_applcn_no full outer join (select ja.app_applcn_no as app_no,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno,rjp.Description from jpa_application as ja left join Ref_Jenis_Pelaburan as rjp on rjp.Description_Code=ja.app_loan_type_cd where ja.app_applcn_no = '" + cc_no + "') as d on d.app_no=c.cal_applcn_no", con);
            }
            else
            {
                grid_cmd = new SqlCommand("select * from (SELECT jn.jno_applcn_no as jno_applcn_no,FORMAT(jn.jno_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.jno_loan_amt,'')) when '0.00' then '' else ISNULL(jn.jno_loan_amt,'') end as l_amt,jn.jno_seq_no as seq_no,ISNULL(jn.jno_approve_amt,'') as a_amt,ISNULL(jn.jno_profit_amt,'') as pr_amt,ISNULL(case when CONVERT(DATE, jn.jno_actual_pay_date) = '1900-01-01' then '' else CONVERT(CHAR(10), FORMAT(jn.jno_actual_pay_date,'dd/MM/yyyy', 'en-us'), 103) END, '') as ap_dt,ISNULL(jn.jno_pay_amt,'') as p_amt,ISNULL(jn.jno_actual_pay_amt,'') as ap_amt,ISNULL(jn.jno_late_excess_amt,'') as le_amt,ISNULL(jn.jno_saving_amt,'') as sa_amt,ISNULL(jn.jno_actual_saving_amt,'') as as_amt,ISNULL(jn.jno_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.jno_total_pay_amt,'') as tp_amt,ISNULL(jn.jno_balance_amt,'') as bal_amt,ISNULL(jn.jno_total_saving_amt,'') as ts_amt,jn.jno_day_late as dlate FROM jpa_jbb_normal  as jn where jn.jno_applcn_no='" + cc_no + "' ) as a full outer join (select jno_applcn_no,sum(jno_pay_amt) as tot_pamt,sum(jno_actual_pay_amt) as tot_apamt,sum(jno_saving_amt) as tot_samt,sum(jno_actual_saving_amt) as tot_asamt,sum(jno_total_pay_amt) as tot_tpamt from jpa_jbb_normal where jno_applcn_no='" + cc_no + "' group by jno_applcn_no) as b on b.jno_applcn_no=a.jno_applcn_no full outer join (select * from jpa_calculate_fee as jc where jc.cal_applcn_no='" + cc_no + "') as c on c.cal_applcn_no=b.jno_applcn_no full outer join (select ja.app_applcn_no as app_no,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno,rjp.Description from jpa_application as ja left join Ref_Jenis_Pelaburan as rjp on rjp.Description_Code=ja.app_loan_type_cd where ja.app_applcn_no = '" + cc_no + "') as d on d.app_no=c.cal_applcn_no", con);
            }
        }
        else
        {
            grid_cmd = new SqlCommand("select * from (SELECT jn.jno_applcn_no as jno_applcn_no,FORMAT(jn.jno_pay_date,'dd/MM/yyyy', 'en-us') as p_dt, case(ISNULL(jn.jno_loan_amt,'')) when '0.00' then '' else ISNULL(jn.jno_loan_amt,'') end as l_amt,jn.jno_seq_no as seq_no,ISNULL(jn.jno_approve_amt,'') as a_amt,ISNULL(jn.jno_profit_amt,'') as pr_amt,ISNULL(case when CONVERT(DATE, jn.jno_actual_pay_date) = '1900-01-01' then '' else CONVERT(CHAR(10), FORMAT(jn.jno_actual_pay_date,'dd/MM/yyyy', 'en-us'), 103) END, '') as ap_dt,ISNULL(jn.jno_pay_amt,'') as p_amt,ISNULL(jn.jno_actual_pay_amt,'') as ap_amt,ISNULL(jn.jno_late_excess_amt,'') as le_amt,ISNULL(jn.jno_saving_amt,'') as sa_amt,ISNULL(jn.jno_actual_saving_amt,'') as as_amt,ISNULL(jn.jno_saving_late_excess_amt,'') as sle_amt,ISNULL(jn.jno_total_pay_amt,'') as tp_amt,ISNULL(jn.jno_balance_amt,'') as bal_amt,ISNULL(jn.jno_total_saving_amt,'') as ts_amt,jn.jno_day_late as dlate FROM jpa_jbb_normal  as jn where jn.jno_applcn_no='" + cc_no + "' ) as a full outer join (select jno_applcn_no,sum(jno_pay_amt) as tot_pamt,sum(jno_actual_pay_amt) as tot_apamt,sum(jno_saving_amt) as tot_samt,sum(jno_actual_saving_amt) as tot_asamt,sum(jno_total_pay_amt) as tot_tpamt from jpa_jbb_normal where jno_applcn_no='" + cc_no + "' group by jno_applcn_no) as b on b.jno_applcn_no=a.jno_applcn_no full outer join (select * from jpa_calculate_fee as jc where jc.cal_applcn_no='" + cc_no + "') as c on c.cal_applcn_no=b.jno_applcn_no full outer join (select ja.app_applcn_no as app_no,ja.appl_loan_dur as a_dur,ja.app_name as a_name,ja.app_new_icno as a_icno,rjp.Description from jpa_application as ja left join Ref_Jenis_Pelaburan as rjp on rjp.Description_Code=ja.app_loan_type_cd where ja.app_applcn_no = '" + cc_no + "') as d on d.app_no=c.cal_applcn_no", con);
        }


        SqlDataAdapter da = new SqlDataAdapter(grid_cmd);
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
            gvSelected.FooterRow.Cells[0].Text = "<strong>JUMLAH (RM)</strong>";
            gvSelected.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Center;
        }
        else
        {

            gvSelected.DataSource = ds;
            gvSelected.DataBind();
            gvSelected.FooterRow.Cells[0].Text = "<strong>JUMLAH (RM)</strong>";
            gvSelected.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Center;

        }
        con.Close();

    }
    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSelected.PageIndex = e.NewPageIndex;
        grid();
        gvSelected.DataBind();
    }



    protected void btn_click(object sender, EventArgs e)
    {
        try
        {
            if (Applcn_no.Text != "")
            {
                var ic_count = Applcn_no.Text.Length;
                DataTable app_icno = new DataTable();
                app_icno = DBCon.Ora_Execute_table("select app_applcn_no,isnull(app_current_jbb_ind,'') AS app_current_jbb_ind,appl_loan_dur from jpa_application JA where  '" + Applcn_no.Text + "' IN(JA.app_applcn_no, JA.app_new_icno)");
                if (ic_count == 12)
                {
                    cc_no = app_icno.Rows[0]["app_applcn_no"].ToString();
                }
                else
                {
                    cc_no = Applcn_no.Text;
                }

                foreach (GridViewRow gvrow1 in gvSelected.Rows)
                {
                    counter = counter + 1;

                }

                if (counter > 0)
                {
                    string pay_amt = string.Empty;
                    string apay_amt = string.Empty;
                    string pri_amt = string.Empty;
                    string dpro_amt = string.Empty;
                    string tab_name = string.Empty;
                    string col_name = string.Empty;
                    string apay_date = string.Empty;

                    if (app_icno.Rows[0]["app_current_jbb_ind"].ToString() == "P")
                    {
                        tab_name = "pjs";
                        col_name = "pjs";
                    }
                    else if (app_icno.Rows[0]["app_current_jbb_ind"].ToString() == "E")
                    {
                        tab_name = "extension";
                        col_name = "ext";
                    }
                    else if (app_icno.Rows[0]["app_current_jbb_ind"].ToString() == "L")
                    {
                        tab_name = "writeoff";
                        col_name = "jwo";
                    }
                    else if (app_icno.Rows[0]["app_current_jbb_ind"].ToString() == "H")
                    {
                        tab_name = "holiday";
                        col_name = "hol";
                    }

                    foreach (GridViewRow gvrow in gvSelected.Rows)
                    {

                        var v1_2 = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("Label2");
                        if (v1_2.Text != "")
                        {
                            DateTime dt_2 = DateTime.ParseExact(v1_2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            apay_date = dt_2.ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            apay_date = "";
                        }


                        var a_no = (System.Web.UI.WebControls.Label)gvrow.FindControl("appno");
                        var sno = (System.Web.UI.WebControls.Label)gvrow.FindControl("seqno");

                        var pamt = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("bk");
                        var sav_amt = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("s_kena");
                        var tsav_amt = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("tsa_amt");
                        var bamt = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("b_pemb");
                        var jsimp = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("j_simpan");
                        var dlate = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("h_lewat");

                        var ap_amt = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("b_bayar");
                        var le_amt = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("Label6");
                        var as_amt = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("s_bayar");
                        var sle_amt = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("s_tun_lebih");
                        var tp_amt = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("j_bay");
                        var j_samt = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("j_simpan");
                        Dblog.Execute_CommamdText("UPDATE jpa_jbb_" + tab_name + " SET " + col_name + "_actual_pay_date='" + apay_date + "'," + col_name + "_pay_amt='" + pamt.Text + "'," + col_name + "_actual_pay_amt='" + ap_amt.Text + "'," + col_name + "_late_excess_amt='" + le_amt.Text.Replace("(", "-").Replace(")", "") + "'," + col_name + "_saving_amt='" + sav_amt.Text + "'," + col_name + "_actual_saving_amt='" + as_amt.Text + "'," + col_name + "_saving_late_excess_amt='" + sle_amt.Text + "'," + col_name + "_total_pay_amt='" + tp_amt.Text + "'," + col_name + "_balance_amt='" + bamt.Text + "'," + col_name + "_total_saving_amt='" + j_samt.Text + "'," + col_name + "_day_late='" + dlate.Text + "'," + col_name + "_upd_id='" + Session["New"].ToString() + "'," + col_name + "_upd_dt='" + DateTime.Now + "' where " + col_name + "_applcn_no='" + a_no.Text + "' and " + col_name + "_seq_no='" + sno.Text + "'");

                        string ldur = (double.Parse(app_icno.Rows[0]["appl_loan_dur"].ToString()) + 1).ToString();
                        if (ldur == sno.Text)
                        {
                            DataTable get_jbb = new DataTable();
                            get_jbb = DBCon.Ora_Execute_table("select sum(" + col_name + "_actual_pay_amt) as tot_apamt from jpa_jbb_" + tab_name + " where " + col_name + "_applcn_no='" + a_no.Text + "'");
                            Dblog.Execute_CommamdText("update jpa_application set app_cumm_saving_amt='" + j_samt.Text + "',app_cumm_pay_amt='" + get_jbb.Rows[0]["tot_apamt"].ToString() + "',app_bal_loan_amt='" + bamt.Text + "',app_upd_id='" + Session["New"].ToString() + "',app_upd_dt='" + DateTime.Now + "'  where app_applcn_no='" + a_no.Text + "'");
                        }



                    }
                   
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Issue.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

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

   
}