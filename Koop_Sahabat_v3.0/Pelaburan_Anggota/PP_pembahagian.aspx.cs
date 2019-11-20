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


public partial class PP_pembahagian : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string str;
    string sel_button;
    DBConnection DBCon = new DBConnection();
    SqlCommand com;
    DataTable dt = new DataTable();
    StudentWebService service = new StudentWebService();
    string Status = string.Empty;
    string level, userid;
    //int total = 0;
    decimal total = 0M;
    string selphase_no;
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
                Banknama();
                Cara_bayaran();
                //Hide Textbox
                MP_Nama.Attributes.Add("Readonly", "Readonly");
                MP_kpbaru.Attributes.Add("Readonly", "Readonly");
                //MP_wilayah.Attributes.Add("Readonly", "Readonly");
                //MP_cawangan.Attributes.Add("Readonly", "Readonly");
                MP_amt.Attributes.Add("Readonly", "Readonly");
                MP_duration.Attributes.Add("Readonly", "Readonly");
                TextBox1.Attributes.Add("Readonly", "Readonly");
                TextBox7.Attributes.Add("Readonly", "Readonly");
                Button2.Visible = false;
                Button3.Visible = false;
                Button1.Visible = false;
                RadioButton1.Checked = true;
               
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

    void Banknama()
    {
        string com = "select Bank_Name,Bank_Code from Ref_Nama_Bank WHERE Status = 'A' order by Bank_Name ASC";
        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        DDL_bank.DataSource = dt;
        DDL_bank.DataBind();
        DDL_bank.DataTextField = "Bank_Name";
        DDL_bank.DataValueField = "Bank_Code";
        DDL_bank.DataBind();
        DDL_bank.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    }

    void Cara_bayaran()
    {
        string com = "select KETERANGAN,KETERANGAN_Code from Ref_Jenis_Bayaran WHERE Status = 'A' order by KETERANGAN ASC";
        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        DDL_Bayaran.DataSource = dt;
        DDL_Bayaran.DataBind();
        DDL_Bayaran.DataTextField = "KETERANGAN";
        DDL_Bayaran.DataValueField = "KETERANGAN_Code";
        DDL_Bayaran.DataBind();
        DDL_Bayaran.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    }

    void srch_Click()
    {
        if (Applcn_no.Text != "")
        {
            tab1();
            DataTable ddicno = new DataTable();
            ddicno = DBCon.Ora_Execute_table("select app_applcn_no from jpa_application JA Left Join jpa_jkkpa_approval as JJA ON JJA.jkk_applcn_no=JA.app_applcn_no where JA.app_applcn_no='" + Applcn_no.Text + "' and JJA.jkk_result_ind='L'");
            DataTable select_app = new DataTable();
            select_app = DBCon.Ora_Execute_table("select JA.app_new_icno,JA.app_loan_type_cd,JA.app_name,RW.Wilayah_Name,ISNULL(RB.branch_desc, '') AS branch_desc, JJA.jkk_approve_amt, JJA.jkk_approve_dur,JA.applcn_clsed from jpa_application as JA  Left Join jpa_jkkpa_approval as JJA ON JJA.jkk_applcn_no=JA.app_applcn_no Left Join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd Left join ref_branch AS RB ON RB.branch_cd=JA.app_branch_cd where JA.app_applcn_no='" + Applcn_no.Text + "'");
            if (select_app.Rows.Count != 0)
            {
                DataTable get_mem_details = new DataTable();
                get_mem_details = DBCon.Ora_Execute_table("select mem_new_icno,mem_bank_cd,mem_bank_acc_no from mem_member where mem_new_icno='"+ select_app.Rows[0]["app_new_icno"].ToString() + "'");

                TextBox6.Text = select_app.Rows[0]["app_name"].ToString();
                //TextBox9.Text = select_app.Rows[0]["app_name"].ToString();
                if (get_mem_details.Rows.Count != 0)
                {
                    TextBox5.Text = get_mem_details.Rows[0]["mem_bank_acc_no"].ToString();
                    DDL_bank.SelectedValue = get_mem_details.Rows[0]["mem_bank_cd"].ToString();
                }
            }

            DataTable select_penama = new DataTable();
            select_penama = DBCon.Ora_Execute_table("select count(*) as cc from jpa_khairat where tkh_applcn_no='" + Applcn_no.Text + "'");
            DataTable select_app1 = new DataTable();
            select_app1 = DBCon.Ora_Execute_table("select cal_approve_amt - (cal_stamp_duty_amt +cal_process_fee +cal_deposit_amt+(cal_tkh_amt * " + select_penama.Rows[0]["cc"].ToString() + ")+cal_other_fee + cal_credit_fee) as all_char from jpa_calculate_fee where cal_applcn_no='" + Applcn_no.Text + "'");
            if (ddicno.Rows.Count == 0)
            {
                
                //RadioButton1.Checked = false;
                //RadioButton2.Checked = false;
                //RadioButton3.Checked = false;
                //gvSelected.Visible = false;
                //Button2.Visible = false;
                //Button3.Visible = false;
                //Button1.Visible = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
            else
            {

                MP_Nama.Text = (string)select_app.Rows[0]["app_name"];
                MP_kpbaru.Text = (string)select_app.Rows[0]["app_new_icno"];
                //MP_wilayah.Text = (string)select_app.Rows[0]["Wilayah_Name"];
                //MP_cawangan.Text = (string)select_app.Rows[0]["branch_desc"];
                decimal amt = (decimal)select_app.Rows[0]["jkk_approve_amt"];
                MP_amt.Text = amt.ToString("C").Replace("RM","").Replace("$", "");
                MP_duration.Text = select_app.Rows[0]["jkk_approve_dur"].ToString();
                decimal amt6 = (decimal)select_app1.Rows[0]["all_char"];
                TextBox1.Text = amt6.ToString("C").Replace("RM", "").Replace("$", "");
             
                txt_sts.Text = select_app.Rows[0]["applcn_clsed"].ToString();
                if (select_app.Rows[0]["applcn_clsed"].ToString() == "N")
                {
                    DataTable chng_sts = new DataTable();
                    chng_sts = DBCon.Ora_Execute_table("select top(1)* from jpa_application where app_new_icno='" + MP_kpbaru.Text + "' and applcn_clsed ='Y'  order by Created_date ASC");
                    DataTable select_disbuse = new DataTable();
                    select_disbuse = DBCon.Ora_Execute_table("select ISNULL(sum(pha_pay_amt),'0.00') dis_amt from jpa_disburse where pha_applcn_no='" + Applcn_no.Text + "'");
                    if (chng_sts.Rows.Count != 0)
                    {
                        DataTable get_pre_loan = new DataTable();
                        get_pre_loan = DBCon.Ora_Execute_table("select top(1)txn_bal_amt from jpa_transaction where txn_applcn_no='" + chng_sts.Rows[0]["app_applcn_no"].ToString() + "' order by txn_crt_dt desc");

                      

                        string pre_lamt = string.Empty;
                        decimal lamt1 = 0;
                        if (get_pre_loan.Rows.Count != 0)
                        {
                            pre_lamt = get_pre_loan.Rows[0]["txn_bal_amt"].ToString();
                            if (select_app.Rows[0]["app_loan_type_cd"].ToString() == "I")
                            {
                                lamt1 = (amt - decimal.Parse(select_disbuse.Rows[0]["dis_amt"].ToString()));
                            }
                            else
                            {
                                lamt1 = (amt - decimal.Parse(pre_lamt)) - decimal.Parse(select_disbuse.Rows[0]["dis_amt"].ToString());
                            }
                            TextBox7.Text = lamt1.ToString("C").Replace("RM", "").Replace("$", "");
                        }

                    }
                    else
                    {
                        TextBox7.Text = amt6.ToString("C").Replace("RM", "").Replace("$", "");
                    }

                    Button2.Visible = true;
                    Button5.Visible = true;
                    Button1.Visible = true;
                }
                else
                {
                    Button2.Visible = false;
                    Button5.Visible = false;
                    Button1.Visible = false;
                }
                
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (Applcn_no.Text != "")
            {
                if (RadioButton1.Checked == true || RadioButton2.Checked == true || RadioButton3.Checked == true)
                {
                    if (TextBox10.Text != "" && TextBox6.Text != "" && TextBox9.Text != "" && TextBox5.Text != "" && DDL_bank.SelectedValue != "" && DDL_Bayaran.SelectedValue != "" && TextBox7.Text != "")
                    {

                        string seqno = string.Empty;
                        if (RadioButton1.Checked == true)
                        {
                            sel_button = "1";
                        }
                        else if (RadioButton2.Checked == true)
                        {
                            sel_button = "2";
                        }
                        else if (RadioButton3.Checked == true)
                        {
                            sel_button = "3";
                        }
                       
                        DataTable row_count = new DataTable();
                        row_count = DBCon.Ora_Execute_table("SELECT count(*) as c FROM  jpa_disburse where pha_applcn_no='" + Applcn_no.Text + "' and pha_phase_no='" + sel_button + "'");
                        if (row_count.Rows[0]["c"].ToString() == "0")
                        {
                            seqno = "1";
                        }
                        else
                        {
                            //object count_seq = (Convert.ToInt32(max_count.ExecuteScalar()) + 1);
                            string count_seq = (double.Parse(row_count.Rows[0]["c"].ToString()) + 1).ToString();
                            seqno = count_seq.ToString();
                        }

                        DataTable maxrow_count = new DataTable();
                        maxrow_count = DBCon.Ora_Execute_table("SELECT * FROM  jpa_disburse where pha_applcn_no='" + Applcn_no.Text + "' and pha_phase_no='" + sel_button + "' and pha_reg_no='" + TextBox9.Text + "'");

                        if (maxrow_count.Rows.Count == 0)
                        {
                            DBCon.Execute_CommamdText("insert into jpa_disburse (pha_applcn_no,pha_phase_no,pha_seqno,pha_pay_instruct_no,pha_phase_amt,pha_name,pha_reg_no,pha_bank_acc_no,pha_bank_cd,pha_pay_type_cd,pha_pay_amt,pha_crt_id,pha_crt_dt) values ('" + Applcn_no.Text + "','" + sel_button + "','" + seqno + "','','" + TextBox10.Text.Replace("RM", "") + "','" + TextBox6.Text.Replace("'", "''") + "','" + TextBox9.Text + "','" + TextBox5.Text + "','" + DDL_bank.SelectedValue + "','" + DDL_Bayaran.SelectedValue + "','" + TextBox7.Text + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('No Daftar Penerima Already exist this Phase No',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                        }                        
                        TextBox10.Text = "";
                        TextBox6.Text = "";
                        TextBox9.Text = "";
                        TextBox5.Text = "";
                        DDL_bank.SelectedValue = "";
                        DDL_Bayaran.SelectedValue = "";
                        TextBox7.Text = "";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);                        
                        con.Close();
                       
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Red Mark Field Harus Mandatori',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Red Mark Field Harus Mandatori',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Permohonan',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
            tab1();
            //tab2();
            //tab3();
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Coding Issue Sila semak',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }

    }


    void grid()
    {
        //Button5.Visible = false;
        con.Open();
        SqlCommand cmd = new SqlCommand("select pha_applcn_no,pha_seqno,pha_phase_no,pha_name,pha_reg_no,pha_bank_acc_no,pha_bank_cd,pha_pay_type_cd,pha_pay_amt,RNB.Bank_Name,RJB.KETERANGAN from jpa_disburse left join Ref_Nama_Bank as RNB ON RNB.Bank_Code=pha_bank_cd Left join Ref_Jenis_Bayaran as RJB ON RJB.KETERANGAN_Code = pha_pay_type_cd where pha_applcn_no = '" + Applcn_no.Text + "' and pha_phase_no ='" + sel_button + "' ORDER BY pha_seqno ASC", con);
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
            Button7.Visible = false;
            //TextBox10.Text = "";
            //TextBox10.Attributes.Remove("Readonly");
        }
        else
        {
            if (txt_sts.Text == "N")
            {
                Button2.Visible = true;
                Button3.Visible = true;
                Button1.Visible = true;
                Button7.Visible = true;
            }
            else
            {
                Button2.Visible = false;
                Button3.Visible = false;
                Button1.Visible = false;
                Button7.Visible = false;
            }
            gvSelected.DataSource = ds;
            gvSelected.DataBind();
            gvSelected.FooterRow.Cells[6].Text = "<center><strong>JUMLAH AMAUN PENGELUARAN (RM)</strong></center>";
            gvSelected.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;

           
        }
        con.Close();
    }
    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSelected.PageIndex = e.NewPageIndex;
        grid();
        gvSelected.DataBind();
    }

    //protected void gvEmp_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        int vv = 0;
    //        if (DataBinder.Eval(e.Row.DataItem, "pha_pay_amt") != DBNull.Value)
    //        {
    //            Decimal ss = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "pha_pay_amt"));
    //            total += ss;
    //            vv = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "pha_pay_amt"));
    //        }



    //    }

    //    //ecimal tot_val = 0;
    //    if (e.Row.RowType == DataControlRowType.Footer)
    //    {
    //        System.Web.UI.WebControls.Label lblamount = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblTotal");
    //        lblamount.Text = double.Parse(total.ToString()).ToString("C").Replace("$", "");
    //        TextBox10.Text = string.Format("{0:F}", Convert.ToDouble(total));

    //        TextBox10.Attributes.Add("Readonly", "Readonly");
    //    }
    //}

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


            var ddl_cb = (DropDownList)e.Row.FindControl("CB_details");
            string com1 = "select KETERANGAN,KETERANGAN_Code from Ref_Jenis_Bayaran WHERE Status = 'A' order by KETERANGAN ASC";
            SqlDataAdapter adpt1 = new SqlDataAdapter(com1, con);
            DataTable dt1 = new DataTable();
            adpt1.Fill(dt1);
            ddl_cb.DataSource = dt1;
            ddl_cb.DataBind();
            ddl_cb.DataTextField = "KETERANGAN";
            ddl_cb.DataValueField = "KETERANGAN_Code";
            ddl_cb.DataBind();
            ddl_cb.SelectedValue = ((DataRowView)e.Row.DataItem)["pha_pay_type_cd"].ToString();
            ddl_cb.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            System.Web.UI.WebControls.Label lblamount = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblTotal");
            lblamount.Text = double.Parse(total.ToString()).ToString("C").Replace("$", "");
            TextBox10.Text = double.Parse(total.ToString()).ToString("C").Replace("$", "");
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

                        System.Web.UI.WebControls.TextBox box1 = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("Label2");
                        System.Web.UI.WebControls.TextBox box2 = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("Label4");
                        System.Web.UI.WebControls.TextBox box3 = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("Label5");
                        DropDownList ddl = (DropDownList)gvrow.FindControl("Bank_details");
                        DropDownList ddl_cb = (DropDownList)gvrow.FindControl("CB_details");

                        DBCon.Execute_CommamdText("Update jpa_disburse SET pha_name='" + box1.Text.Replace("'", "''") + "', pha_reg_no='" + box2.Text.Trim() + "', pha_bank_acc_no='" + box3.Text + "',pha_bank_cd='" + ddl.SelectedValue + "',pha_pay_type_cd='" + ddl_cb.SelectedValue + "',pha_upd_id='" + Session["New"].ToString() + "',pha_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE pha_applcn_no='" + lblID.Text + "' and pha_phase_no = '" + ph_no1.Text + "' and pha_seqno='" + seqno.Text + "'");
                        //}

                    }
                    //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan'); window.location='PP_Kpp.aspx';", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
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
            tab1();
            //tab2();
            //tab3();
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }
    protected void OnCheckedChanged(object sender, EventArgs e)
    {
        bool isUpdateVisible = false;
        System.Web.UI.WebControls.CheckBox chk = (sender as System.Web.UI.WebControls.CheckBox);
        if (chk.ID == "chkAll")
        {
            foreach (GridViewRow row in gvSelected.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    row.Cells[0].Controls.OfType<System.Web.UI.WebControls.CheckBox>().FirstOrDefault().Checked = chk.Checked;
                }
            }
        }
        System.Web.UI.WebControls.CheckBox chkAll = (gvSelected.HeaderRow.FindControl("chkAll") as System.Web.UI.WebControls.CheckBox);
        chkAll.Checked = true;
        foreach (GridViewRow row in gvSelected.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                bool isChecked = row.Cells[0].Controls.OfType<System.Web.UI.WebControls.CheckBox>().FirstOrDefault().Checked;
                for (int i = 1; i < row.Cells.Count; i++)
                {
                    if (isChecked && !isUpdateVisible)
                    {
                        isUpdateVisible = true;
                    }
                    if (!isChecked)
                    {
                        chkAll.Checked = false;

                    }
                }
            }
        }

    }

    protected void hapus_click(object sender, EventArgs e)
    {


        string rcount = string.Empty;

        int count = 0;
        foreach (GridViewRow gvrow in gvSelected.Rows)
        {
            var checkbox = gvrow.FindControl("chkSelect") as System.Web.UI.WebControls.CheckBox;
            if (checkbox.Checked)
            {
                count++;
            }
            rcount = count.ToString();
        }
        if (rcount != "0")
        {
            foreach (GridViewRow gvrow in gvSelected.Rows)
            {
                var checkbox = gvrow.FindControl("chkSelect") as System.Web.UI.WebControls.CheckBox;
                if (checkbox.Checked == true)
                {
                    var lblID = gvrow.FindControl("app_no") as System.Web.UI.WebControls.Label;
                    var seqno = gvrow.FindControl("seq_no") as System.Web.UI.WebControls.Label;
                    var phaseno = gvrow.FindControl("Label3") as System.Web.UI.WebControls.Label;
                    DBCon.Execute_CommamdText("DELETE jpa_disburse WHERE pha_applcn_no='" + lblID.Text + "' and pha_phase_no= '" + phaseno.Text + "' and pha_seqno='" + seqno.Text + "'");
                }

            }            
            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dihapus.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            if (RadioButton1.Checked == true)
            {
                sel_button = "1";
            }
            else if (RadioButton2.Checked == true)
            {
                sel_button = "2";
            }
            else if (RadioButton3.Checked == true)
            {
                sel_button = "3";
            }
            grid();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Please Select Minimum One check box',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }

    }

    void tab1()
    {
        if (RadioButton1.Checked == true)
        {
            if (Applcn_no.Text != "")
            {
                sel_button = "1";
                grid();
                gvSelected.Visible = true;
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
        tab1();
    }

    void tab2()
    {
        if (RadioButton2.Checked == true)
        {
            if (Applcn_no.Text != "")
            {
                sel_button = "2";
                grid();
                gvSelected.Visible = true;
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
    protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
    {
        tab2();
    }

    void tab3()
    {
        if (RadioButton3.Checked == true)
        {
            if (Applcn_no.Text != "")
            {
                sel_button = "3";
                grid();
                gvSelected.Visible = true;
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
    protected void RadioButton3_CheckedChanged(object sender, EventArgs e)
    {
        tab3();
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("PP_pembahagian.aspx");
    }

    protected void Reset_grid_click(object sender, EventArgs e)
    {
        TextBox10.Text = "";
        TextBox6.Text = "";
        TextBox9.Text = "";
        TextBox5.Text = "";
        DDL_bank.SelectedValue = "";
        DDL_Bayaran.SelectedValue = "";
        TextBox7.Text = "";
        gvSelected.Visible = false;
        Button2.Visible = false;
        Button3.Visible = false;
        Button1.Visible = false;
        Button5.Visible = true;
        RadioButton1.Checked = true;
        RadioButton2.Checked = false;
        RadioButton3.Checked = false;

    }

    protected void clk_bak(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Pelaburan_Anggota/PP_pembahagian_view.aspx");
    }
}