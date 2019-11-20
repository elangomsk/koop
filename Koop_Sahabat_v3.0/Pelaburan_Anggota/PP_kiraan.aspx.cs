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


public partial class PP_kiraan : System.Web.UI.Page
{
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    //string val13, val14, val15;
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    SqlCommand com;
    string val17;
    DataTable dt = new DataTable();
    string Status = string.Empty;
    string level, userid;
    decimal total = 0, total1 = 0;
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
                Txtnama.Attributes.Add("Readonly", "Readonly");
                txtage.Attributes.Add("Readonly", "Readonly");
                TxtBilangan.Attributes.Add("Readonly", "Readonly");
                txt_pre_bal.Attributes.Add("Readonly", "Readonly");
                Txtkeuntungan.Attributes.Add("Readonly", "Readonly");
                TxtBay.Attributes.Add("Readonly", "Readonly");
              
                //FirstGridViewRow();
                //Txtwil.Attributes.Add("Readonly", "Readonly");
                //Txtcaw.Attributes.Add("Readonly", "Readonly");
                //FirstGridViewRow();
                var samp = Request.Url.Query;
                if (samp != "")
                {
                    DataTable ddokdicno = new DataTable();
                    ddokdicno = DBCon.Ora_Execute_table("select app_new_icno,app_applcn_no from jpa_application where app_applcn_no='" + service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"])) + "'");

                    Txtnokp.Text = ddokdicno.Rows[0]["app_new_icno"].ToString();
                    load_permohon();
                    dd_permohon.SelectedValue = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    srch_rfd();
                    Button4.Visible = false;
                    //Button6.Visible = false;
                    //Button2.Visible = false;
                    Button1.Visible = false;
                    //Button5.Visible = true;

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
                com.CommandText = "select app_new_icno from jpa_application where app_new_icno like '%' + @Search + '%' group by app_new_icno";
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
                            countryNames.Add(sdr["app_new_icno"].ToString());

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
    private void FirstGridViewRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("Col1", typeof(string)));
        dt.Columns.Add(new DataColumn("Col2", typeof(string)));
        
        dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dr["Col1"] = string.Empty;
        dr["Col2"] = string.Empty;
        
        dt.Rows.Add(dr);

        ViewState["CurrentTable"] = dt;

        grvStudentDetails.DataSource = dt;
        grvStudentDetails.DataBind();
    }

    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        AddNewRow();
    }

    private void AddNewRow()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count < 5)
            {
                if (dtCurrentTable.Rows.Count > 0)
                {
                    foreach (System.Data.DataColumn col in dtCurrentTable.Columns) col.ReadOnly = false;
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        System.Web.UI.WebControls.DropDownList v1 =
                          (System.Web.UI.WebControls.DropDownList)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("Col1");
                        System.Web.UI.WebControls.TextBox v2 =
                          (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[rowIndex].Cells[2].FindControl("Col2");



                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;

                        dtCurrentTable.Rows[i - 1]["col1"] = v1.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["col2"] = v2.Text;
                        rowIndex++;
                    }
                    if (Convert.ToInt32(drCurrentRow.ItemArray[0].ToString()) == Convert.ToInt32(dtCurrentTable.Rows.Count + 1))
                    {
                        drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count + 1;
                        drCurrentRow["Col1"] = string.Empty;
                        drCurrentRow["Col2"] = 0;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;

                    grvStudentDetails.DataSource = dtCurrentTable;
                    grvStudentDetails.DataBind();



                }
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousData();
    }



    private void SetPreviousData()
    {
        
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList v1 =
                          (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("Col1");
                    System.Web.UI.WebControls.TextBox v2 =
                      (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[rowIndex].Cells[2].FindControl("Col2");
                   


                    v1.SelectedValue = dt.Rows[i]["Col1"].ToString();
                    v2.Text = dt.Rows[i]["Col2"].ToString();

                    rowIndex++;
                    if (v2.Text != "")
                    {
                        decimal amt1 = Convert.ToDecimal(v2.Text);
                        total += amt1;
                    }

                }
            }
        }
       
    }

    protected void grvStudentDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SetRowData();
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (dt.Rows.Count > 1)
            {
                dt.Rows.Remove(dt.Rows[rowIndex]);
                drCurrentRow = dt.NewRow();
                ViewState["CurrentTable"] = dt;
                grvStudentDetails.DataSource = dt;
                grvStudentDetails.DataBind();

                for (int i = 0; i < grvStudentDetails.Rows.Count - 1; i++)
                {
                    grvStudentDetails.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                }
                SetPreviousData();
            }
        }
    }

    private void SetRowData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                foreach (System.Data.DataColumn col in dtCurrentTable.Columns) col.ReadOnly = false;
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    DropDownList v1 =
                         (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("Col1");
                    System.Web.UI.WebControls.TextBox v2 =
                      (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[rowIndex].Cells[2].FindControl("Col2");
                   

                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;
                    dtCurrentTable.Rows[i - 1]["Col1"] = v1.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col2"] = v2.Text;                    

                    rowIndex++;

                }

                ViewState["CurrentTable"] = dtCurrentTable;
                //grvStudentDetails.DataSource = dtCurrentTable;
                //grvStudentDetails.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        //SetPreviousData();
    }

    protected void gvEmp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            var ddl = (DropDownList)e.Row.FindControl("Col1");
            //int CountryId = Convert.ToInt32(e.Row.Cells[0].Text);
            SqlCommand cmd = new SqlCommand("select * from pp_ref_caj where status='A' order by caj_cd", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            ddl.DataSource = ds;
            ddl.DataTextField = "caj_name";
            ddl.DataValueField = "caj_cd";
            ddl.DataBind();
            ddl.SelectedValue = ((DataRowView)e.Row.DataItem)["Col1"].ToString();
            ddl.Items.Insert(0, new ListItem("--- PILIH ---", "0"));

            
        }

    }

    protected void ddgstdeboth_SelectedIndexChanged(object sender, EventArgs e)
    {

        int selRowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
        DropDownList c1 = (DropDownList)grvStudentDetails.Rows[selRowIndex].FindControl("Col1");
        System.Web.UI.WebControls.TextBox c2 = (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[selRowIndex].FindControl("col2");

        decimal tgst;
        decimal tgst1;

        string ss_val = string.Empty;
        if (TxtBilangan.Text == "")
        {
            ss_val = "0";
            TxtBilangan.Text = "0";
        }
        else
        {
            ss_val = TxtBilangan.Text;
        }

        if (c1.SelectedValue != "")
        {
            if (c1.SelectedValue == "01")
            {
                //Caj Duti Setem (RM) 
                string a = "0.5";
                string b = "100";
                string val1 = (double.Parse(a) / double.Parse(b)).ToString();
                string vala = (double.Parse(val1) * double.Parse(Txtamaun.Text)).ToString();
                string val2 = (10 * double.Parse(ss_val)).ToString();
                c2.Text = (double.Parse(vala) + double.Parse(val2)).ToString("C").Replace("RM","").Replace("$", "");
                //TxtFipem.Text = "100.00";
            }
            else if (c1.SelectedValue == "02")
            {
                c2.Text = "100.00";
            }
            else if (c1.SelectedValue == "03")
            {
                //Deposit Sekuriti 
                string vv_dur = TxtTem.Text;
                string val7 = (double.Parse(Txtamaun.Text) * double.Parse(TxtTem.Text)).ToString();
                string val71 = (double.Parse(Txtkadar.Text) / 100).ToString();
                string val8 = (double.Parse(val7) * double.Parse(val71)).ToString();
                string val9 = (double.Parse(val8) / 12).ToString();
                string val91 = ((double.Parse(val9) + double.Parse(Txtamaun.Text)) / double.Parse(TxtTem.Text)).ToString("0.00");
                c2.Text = (double.Parse(val91) * 2).ToString("C").Replace("RM", "").Replace("$", "");
            }
            else if (c1.SelectedValue == "04")
            {
                c2.Text = "0.00";
            }
            else if(c1.SelectedValue == "05")
            {
                //Fi Semakan Kredit (RM) 
                string val10 = ((double.Parse(ss_val) + 1) * 10).ToString();
                c2.Text = (double.Parse(val10)).ToString("C").Replace("RM", "").Replace("$", "");
            }
        }
    }

    void load_permohon()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select app_new_icno,app_applcn_no from jpa_application where app_new_icno='" + Txtnokp.Text + "' order by Created_date ";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_permohon.DataSource = dt;
            dd_permohon.DataTextField = "app_applcn_no";
            dd_permohon.DataValueField = "app_applcn_no";
            dd_permohon.DataBind();
            //dd_permohon.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void bind_permohon(object sender, EventArgs e)
    {
        if (dd_permohon.SelectedValue != "")
        {
            srch_rfd();
        }
    }
    void srch_rfd()
    {
        if (Txtnokp.Text != "")
        {

            DataTable ddokdicno = new DataTable();
            //ddokdicno = DBCon.Ora_Execute_table("select app_name,app_age,wilayah_name,cawangan_name,app_applcn_no from jpa_application as ja left join ref_cawangan as rc on rc.cawangan_code=ja.app_branch_cd and rc.wilayah_code=ja.app_region_cd where app_new_icno='" + Txtnokp.Text + "' and app_sts_cd ='N'"); // 03_08_2018
            ddokdicno = DBCon.Ora_Execute_table("select app_name,app_age,wilayah_name,cawangan_name,app_applcn_no,app_sts_cd,applcn_clsed,app_loan_type_cd from jpa_application as ja left join ref_cawangan as rc on rc.cawangan_code=ja.app_branch_cd and rc.wilayah_code=ja.app_region_cd where app_applcn_no='" + dd_permohon.SelectedValue + "'");
            if (ddokdicno.Rows.Count != 0)
            {
                if(ddokdicno.Rows[0]["app_sts_cd"].ToString() == "N")
                {
                    Button2.Visible = true;
                }
                else
                {
                    Button2.Visible = false;
                }
                DataTable chk_member = new DataTable();
                chk_member = DBCon.Ora_Execute_table("select * from mem_member where mem_new_icno = '" + Txtnokp.Text + "' and mem_sts_cd = 'SA' and Acc_sts='Y'");
                if (chk_member.Rows.Count > 0)
                {
                    if (chk_member.Rows[0]["mem_applicant_type_cd"].ToString() != "LT")
                    {
                        TxtBilangan.Text = "2";
                    }
                    else
                    {
                        TxtBilangan.Text = "1";
                    }
                }
                txt_app_no.Text = ddokdicno.Rows[0]["app_applcn_no"].ToString();
                Txtnama.Text = ddokdicno.Rows[0]["app_name"].ToString();
                txtage.Text = ddokdicno.Rows[0]["app_age"].ToString();
                //Txtwil.Text = ddokdicno.Rows[0]["wilayah_name"].ToString();
                //Txtcaw.Text = ddokdicno.Rows[0]["cawangan_name"].ToString();
                DataTable chng_sts = new DataTable();
                chng_sts = DBCon.Ora_Execute_table("select top(1)* from jpa_application where app_new_icno='" + Txtnokp.Text + "' and app_applcn_no = '" + dd_permohon.SelectedValue +"'  order by Created_date desc");
                DataTable ddokdicno_kira = new DataTable();
                ddokdicno_kira = DBCon.Ora_Execute_table("select * from jpa_calculate_fee where cal_applcn_no='"+ ddokdicno.Rows[0]["app_applcn_no"].ToString() + "'");
                if(ddokdicno_kira.Rows.Count != 0)
                {
                    Txtamaun.Text = double.Parse(ddokdicno_kira.Rows[0]["cal_approve_amt"].ToString()).ToString("C").Replace("RM","").Replace("$", "");
                    TxtTem.Text = ddokdicno_kira.Rows[0]["cal_approve_dur"].ToString();
                    Txtkadar.Text = ddokdicno_kira.Rows[0]["cal_profit_rate"].ToString();
                    TxtBilangan.Text = ddokdicno_kira.Rows[0]["cal_no_guarantor"].ToString();
                    //TxtCaj.Text = double.Parse(ddokdicno_kira.Rows[0]["cal_stamp_duty_amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                   // Txtpre.Text = double.Parse(ddokdicno_kira.Rows[0]["cal_tkh_amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                    //TxtFipem.Text = double.Parse(ddokdicno_kira.Rows[0]["cal_process_fee"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                    //Txtfisem.Text = double.Parse(ddokdicno_kira.Rows[0]["cal_credit_fee"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                    //TxtDeposit.Text = double.Parse(ddokdicno_kira.Rows[0]["cal_deposit_amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                    Txtkeuntungan.Text = double.Parse(ddokdicno_kira.Rows[0]["cal_profit_amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                    //Txtlain.Text = double.Parse(ddokdicno_kira.Rows[0]["cal_other_fee"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                    TxtBay.Text = double.Parse(ddokdicno_kira.Rows[0]["cal_installment_amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                    if (chng_sts.Rows.Count == 0)
                    {
                        txt_pre_bal.Text = "0.00";
                        pre_row1.Visible = false;
                    }
                    else
                    {
                        txt_pre_bal.Text = double.Parse(ddokdicno_kira.Rows[0]["cal_pre_balance"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                        pre_row1.Visible = true;
                    }
                        BindData_show();
                }
                else
                {

                    //prevoius amount

                   

                    if (chng_sts.Rows.Count != 0)
                    {
                        pre_row1.Visible = true;
                        DataTable get_pre_loan = new DataTable();
                        get_pre_loan = DBCon.Ora_Execute_table("select top(1)txn_bal_amt from jpa_transaction where txn_applcn_no='" + chng_sts.Rows[0]["app_applcn_no"].ToString() + "' order by cast(txn_bal_amt as money),txn_crt_dt asc");



                        string pre_lamt = string.Empty;
                        decimal lamt1 = 0;
                        if (get_pre_loan.Rows.Count != 0)
                        {
                            pre_lamt = get_pre_loan.Rows[0]["txn_bal_amt"].ToString();
                            if (ddokdicno.Rows[0]["app_loan_type_cd"].ToString() == "I")
                            {
                                lamt1 = (decimal.Parse(pre_lamt));
                            }
                            else
                            {
                                lamt1 = (decimal.Parse(pre_lamt));
                            }
                            txt_pre_bal.Text = lamt1.ToString("C").Replace("RM", "").Replace("$", "");
                        }

                    }
                    else
                    {
                        txt_pre_bal.Text = "0.00";
                        pre_row1.Visible = false;
                    }

                    Txtamaun.Text = "";
                    TxtTem.Text = "";
                    Txtkadar.Text = "";
                    //TxtBilangan.Text = "";
                    Txtkeuntungan.Text = "";
                    TxtBay.Text = "";
                    FirstGridViewRow();
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Permohonan Telah Diluluskan. Pindaan Maklumat Permohonan Tidak Dibenarkan',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Ic',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }

    }

    protected void BindData_show()
    {
        int rowIndex = 0;
        con.Open();
        DataTable ddicno = new DataTable();
        SqlCommand cmd = new SqlCommand("select ROW_NUMBER() OVER (ORDER BY col1) AS RowNumber,* from (select  '01' as col1,cal_stamp_duty_amt as col2 from jpa_calculate_fee where cal_applcn_no='"+txt_app_no.Text+"' and cal_stamp_duty_amt != '0.00' union all select '02' as jenis_cd,cal_process_fee from jpa_calculate_fee where cal_applcn_no='"+txt_app_no.Text+"' and cal_process_fee != '0.00' union all select '03' as jenis_cd,cal_deposit_amt from jpa_calculate_fee where cal_applcn_no='"+txt_app_no.Text+"' and cal_deposit_amt != '0.00' union all  select '04' as jenis_cd,cal_other_fee from jpa_calculate_fee where cal_applcn_no='"+txt_app_no.Text+"' and cal_other_fee != '0.00' union all select '05' as jenis_cd,cal_credit_fee from jpa_calculate_fee where cal_applcn_no='"+txt_app_no.Text+"' and cal_credit_fee != '0.00') as a", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            FirstGridViewRow();
            //AddNewRow();
        }
        else
        {
            
            DataTable dt = new DataTable();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                dt.Load(reader);
            }
            ViewState["CurrentTable"] = dt;
            grvStudentDetails.DataSource = ds;
            grvStudentDetails.DataBind();
            //SetPreviousData();


        }
        con.Close();
    }
    protected void btnsrch_Click(object sender, EventArgs e)
    {
        try
        {
            load_permohon();
            srch_rfd();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        clear();

    }

    void clear()
    {
        Txtnokp.Text = "";
        Txtnama.Text = "";
        txtage.Text = "";
        //Txtwil.Text = "";
        //Txtcaw.Text = "";
        Txtamaun.Text = "";
        TxtTem.Text = "";
        Txtkadar.Text = "";
        TxtBilangan.Text = "";
        //TxtCaj.Text = "";
       
        //TxtFipem.Text = "";
        //Txtfisem.Text = "";
        //TxtDeposit.Text = "";
        Txtkeuntungan.Text = "";
        //Txtlain.Text = "";
        TxtBay.Text = "";
        txt_app_no.Text = "";
    }

    protected void btnsmmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Txtamaun.Text != "")
            {
                if (TxtTem.Text != "")
                {
                    if (Txtkadar.Text != "")
                    {
                        if (TxtBilangan.Text != "")
                        {
                           
                                            if (Txtkeuntungan.Text != "")
                                            {
                                                if (TxtBay.Text != "")
                                                {
                                                    DataTable ddokdicno1 = new DataTable();
                                                    ddokdicno1 = DBCon.Ora_Execute_table("select app_applcn_no from jpa_application where app_new_icno='" + Txtnokp.Text + "' and app_applcn_no='"+ dd_permohon.SelectedValue +"' and app_sts_cd='N'");
                                                    string appno = ddokdicno1.Rows[0][0].ToString();
                                                    DataTable dd_calapp_no = new DataTable();
                                                    dd_calapp_no = DBCon.Ora_Execute_table("select cal_applcn_no from jpa_calculate_fee where cal_applcn_no='" + appno + "'");
                                                    DataTable ddokdicno = new DataTable();
                                                    DataTable ddaudit = new DataTable();
                                    string sv1 = string.Empty, sv2 = string.Empty, sv3 = string.Empty, sv4 = string.Empty, sv5 = string.Empty;
                                    


                                                    if (dd_calapp_no.Rows.Count == 0)
                                                    {
                                                        ddokdicno = DBCon.Ora_Execute_table("insert into jpa_calculate_fee(cal_applcn_no,cal_approve_amt,cal_approve_dur,cal_profit_rate,cal_no_guarantor,cal_stamp_duty_amt,cal_process_fee,cal_deposit_amt,cal_tkh_amt,cal_credit_fee,cal_profit_amt,cal_other_fee,cal_installment_amt,cal_crt_id,cal_crt_dt,cal_pre_balance)values('" + appno + "','" + Txtamaun.Text + "','" + TxtTem.Text + "','" + Txtkadar.Text + "','" + TxtBilangan.Text + "','"+ sv1 + "','" + sv2 + "','" + sv3 + "','','" + sv5 + "','" + Txtkeuntungan.Text + "','" + sv4 + "','" + TxtBay.Text + "','" + userid + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','"+ txt_pre_bal.Text + "')");
                                                        Session["validate_success"] = "SUCCESS";
                                                        Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                                                    }
                                                    else
                                                    {
                                                        DBCon.Execute_CommamdText("DELETE jpa_calculate_fee WHERE cal_applcn_no='" + appno + "'");
                                                        ddokdicno = DBCon.Ora_Execute_table("insert into jpa_calculate_fee(cal_applcn_no,cal_approve_amt,cal_approve_dur,cal_profit_rate,cal_no_guarantor,cal_stamp_duty_amt,cal_process_fee,cal_deposit_amt,cal_tkh_amt,cal_credit_fee,cal_profit_amt,cal_other_fee,cal_installment_amt,cal_crt_id,cal_crt_dt,cal_pre_balance)values('" + appno + "','" + Txtamaun.Text + "','" + TxtTem.Text + "','" + Txtkadar.Text + "','" + TxtBilangan.Text + "','" + sv1 + "','" + sv2 + "','" + sv3 + "','','" + sv5 + "','" + Txtkeuntungan.Text + "','" + sv4 + "','" + TxtBay.Text + "','" + userid + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + txt_pre_bal.Text + "')");
                                                        Session["validate_success"] = "SUCCESS";
                                                        Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                                                       
                                                    }

                                    if (Session["validate_success"].ToString() == "SUCCESS")
                                    {

                                        if (grvStudentDetails.Rows.Count != 0)
                                        {
                                            foreach (GridViewRow row in grvStudentDetails.Rows)
                                            {
                                                string val1 = ((DropDownList)row.FindControl("Col1")).SelectedValue;
                                                string val2 = ((System.Web.UI.WebControls.TextBox)row.FindControl("Col2")).Text.ToString();

                                                if (val1 == "01")
                                                {
                                                    int rcount = 0;
                                                    int count = 0;
                                                    foreach (GridViewRow gvrow in grvStudentDetails.Rows)
                                                    {
                                                        string val1_1 = ((DropDownList)row.FindControl("Col1")).SelectedValue;
                                                        if (val1 == val1_1)
                                                        {
                                                            count++;
                                                        }
                                                        rcount = count;
                                                    }

                                                    if (rcount != 0)
                                                        {
                                                            string Inssql = "update jpa_calculate_fee set cal_stamp_duty_amt='" + val2 + "' WHERE cal_applcn_no='" + appno + "'";
                                                            Status = DBCon.Ora_Execute_CommamdText(Inssql);
                                                        }
                                                    else
                                                    {
                                                        string Inssql = "update jpa_calculate_fee set cal_stamp_duty_amt='0.00' WHERE cal_applcn_no='" + appno + "'";
                                                        Status = DBCon.Ora_Execute_CommamdText(Inssql);
                                                    }
                                                    
                                                }
                                               

                                                if (val1 == "02")
                                                {
                                                    int rcount1 = 0;
                                                    int count1 = 0;
                                                    foreach (GridViewRow gvrow in grvStudentDetails.Rows)
                                                    {
                                                        string val2_1 = ((DropDownList)row.FindControl("Col1")).SelectedValue;
                                                        if (val1 == val2_1)
                                                        {
                                                            count1++;
                                                        }
                                                        rcount1 = count1;
                                                    }
                                                    if (rcount1 != 0)
                                                    {
                                                        string Inssql = "update jpa_calculate_fee set cal_process_fee='" + val2 + "' WHERE cal_applcn_no='" + appno + "'";
                                                        Status = DBCon.Ora_Execute_CommamdText(Inssql);
                                                    }
                                                    else
                                                    {
                                                        string Inssql = "update jpa_calculate_fee set cal_process_fee='0.00' WHERE cal_applcn_no='" + appno + "'";
                                                        Status = DBCon.Ora_Execute_CommamdText(Inssql);
                                                    }
                                                }

                                                if (val1 == "03")
                                                {
                                                    int rcount2 = 0;
                                                    int count2 = 0;
                                                    foreach (GridViewRow gvrow in grvStudentDetails.Rows)
                                                    {
                                                        string val3_1 = ((DropDownList)row.FindControl("Col1")).SelectedValue;
                                                        if (val1 == val3_1)
                                                        {
                                                            count2++;
                                                        }
                                                        rcount2 = count2;
                                                    }
                                                    if (rcount2 != 0)
                                                    {
                                                        string Inssql = "update jpa_calculate_fee set cal_deposit_amt='" + val2 + "' WHERE cal_applcn_no='" + appno + "'";
                                                        Status = DBCon.Ora_Execute_CommamdText(Inssql);
                                                    }
                                                    else
                                                    {
                                                        string Inssql = "update jpa_calculate_fee set cal_deposit_amt='0.00' WHERE cal_applcn_no='" + appno + "'";
                                                        Status = DBCon.Ora_Execute_CommamdText(Inssql);
                                                    }
                                                }
                                                

                                                if (val1 == "04")
                                                {
                                                    int rcount3 = 0;
                                                    int count3 = 0;
                                                    foreach (GridViewRow gvrow in grvStudentDetails.Rows)
                                                    {
                                                        string val4_1 = ((DropDownList)row.FindControl("Col1")).SelectedValue;
                                                        if (val1 == val4_1)
                                                        {
                                                            count3++;
                                                        }
                                                        rcount3 = count3;
                                                    }
                                                    if (rcount3 != 0)
                                                    {
                                                        string Inssql = "update jpa_calculate_fee set cal_other_fee='" + val2 + "' WHERE cal_applcn_no='" + appno + "'";
                                                        Status = DBCon.Ora_Execute_CommamdText(Inssql);
                                                    }
                                                    else
                                                    {
                                                        string Inssql = "update jpa_calculate_fee set cal_other_fee='0.00' WHERE cal_applcn_no='" + appno + "'";
                                                        Status = DBCon.Ora_Execute_CommamdText(Inssql);
                                                    }
                                                }

                                                if (val1 == "05")
                                                {
                                                    int rcount4 = 0;
                                                    int count4 = 0;
                                                    foreach (GridViewRow gvrow in grvStudentDetails.Rows)
                                                    {
                                                        string val5_1 = ((DropDownList)row.FindControl("Col1")).SelectedValue;
                                                        if (val1 == val5_1)
                                                        {
                                                            count4++;
                                                        }
                                                        rcount4 = count4;
                                                    }
                                                    if (rcount4 != 0)
                                                    {
                                                        string Inssql = "update jpa_calculate_fee set cal_credit_fee='" + val2 + "' WHERE cal_applcn_no='" + appno + "'";
                                                        Status = DBCon.Ora_Execute_CommamdText(Inssql);
                                                    }
                                                    else
                                                    {
                                                        string Inssql = "update jpa_calculate_fee set cal_credit_fee='0.00' WHERE cal_applcn_no='" + appno + "'";
                                                        Status = DBCon.Ora_Execute_CommamdText(Inssql);
                                                    }
                                                }

                                            }
                                        }
                                        Response.Redirect("../Pelaburan_Anggota/PP_kiraan_view.aspx");
                                    }
                                                }
                                                else
                                                {
                                                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Bilangan Penjamin',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                                                }
                                              
                                            }
                                            else
                                            {
                                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Bilangan Penjaminn',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                                            }
                           
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Bilangan Penjamin',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Bilangan Penjamin',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Bilangan Penjamin',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Bilangan Penjamin',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
          
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Error On The Page',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }


    protected void kira_Click(object sender, EventArgs e)
    {
        if (Txtamaun.Text != "")
        {
            if (TxtTem.Text != "")
            {
                if (Txtkadar.Text != "")
                {
                    DataTable chng_sts = new DataTable();
                    chng_sts = DBCon.Ora_Execute_table("select * from jpa_application where app_new_icno='" + Txtnokp.Text + "' and app_applcn_no='"+ dd_permohon.SelectedValue +"'");
                    double exact_amt = 0;
                    if(chng_sts.Rows.Count != 0)
                    {
                        if(chng_sts.Rows[0]["app_loan_type_cd"].ToString() == "I")
                        {
                            exact_amt = double.Parse(Txtamaun.Text) + double.Parse(txt_pre_bal.Text);
                        }
                        else
                        {
                            exact_amt = double.Parse(Txtamaun.Text);
                        }

                    }

                    string ss_val = string.Empty;
                    if (TxtBilangan.Text == "")
                    {
                        ss_val = "0";
                        TxtBilangan.Text = "0";
                    }
                    else
                    {
                        ss_val = TxtBilangan.Text;
                    }

                    ////Deposit Sekuriti 
                    //string vv_dur = TxtTem.Text;
                    //string val7 = (double.Parse(Txtamaun.Text) * double.Parse(TxtTem.Text)).ToString();
                    //string val71 = (double.Parse(Txtkadar.Text) / 100).ToString();
                    //string val8 = (double.Parse(val7) * double.Parse(val71)).ToString();
                    //string val9 = (double.Parse(val8) / 12).ToString();
                    //string val91 = ((double.Parse(val9) + double.Parse(Txtamaun.Text)) / double.Parse(TxtTem.Text)).ToString("0.00");
                    ////TxtDeposit.Text = (double.Parse(val91) * 2).ToString("0.00");


                    ////Caj Duti Setem (RM) 
                    //string a = "0.5";
                    //string b = "100";
                    //string val1 = (double.Parse(a) / double.Parse(b)).ToString();
                    //string vala = (double.Parse(val1) * double.Parse(Txtamaun.Text)).ToString();
                    //string val2 = (10 * double.Parse(ss_val)).ToString();
                    ////TxtCaj.Text = (double.Parse(vala) + double.Parse(val2)).ToString("0.00");
                    ////TxtFipem.Text = "100.00";



                    //Keuntungan (RM) 
                    string val3 = (exact_amt * double.Parse(TxtTem.Text)).ToString();
                    string val6 = ((double.Parse(Txtkadar.Text) / 100) / 12).ToString();//whether the 100% should add or not
                    string val4 = (double.Parse(val3) * double.Parse(val6)).ToString();
                    string val5 = double.Parse(val4).ToString();
                    Txtkeuntungan.Text = (double.Parse(val5)).ToString("C").Replace("RM", "").Replace("$", "");





                    ////Fi Semakan Kredit (RM) 
                    //string val10 = ((double.Parse(ss_val) + 1) * 10).ToString();
                    ////Txtfisem.Text = (double.Parse(val10)).ToString("0.00");


                    //Bayaran Bulanan (RM) : 
                    string val11 = (exact_amt + (double.Parse(val5))).ToString();
                    string val12 = (double.Parse(val11) / double.Parse(TxtTem.Text)).ToString();
                    TxtBay.Text = (double.Parse(val12)).ToString("C").Replace("RM", "").Replace("$", "");


                    //Premium TKH (RM) :
                    DataTable ddokdicno1 = new DataTable();
                    ddokdicno1 = DBCon.Ora_Execute_table("select app_applcn_no,app_loan_type_cd from jpa_application where app_new_icno='" + Txtnokp.Text + "' and app_applcn_no='"+ dd_permohon.SelectedValue +"' and app_sts_cd='N'");

                    DataTable ddokdicno2 = new DataTable();
                    ddokdicno2 = DBCon.Ora_Execute_table("select app_applcn_no,app_loan_type_cd from jpa_application where app_new_icno='" + Txtnokp.Text + "' and app_applcn_no='" + dd_permohon.SelectedValue + "'");

                  


                    DataTable cnt_tkh = new DataTable();
                    if (ddokdicno1.Rows.Count > 0)
                    {

                        cnt_tkh = DBCon.Ora_Execute_table("select * from jpa_khairat where tkh_applcn_no='" + ddokdicno1.Rows[0]["app_applcn_no"].ToString() + "' order by tkh_seq_no");
                        string tkh1 = string.Empty, tkh2 = string.Empty;
                        if (cnt_tkh.Rows.Count >= 1)
                        {

                            double total = 0;
                            int x = Int32.Parse(TxtTem.Text);
                            for (int i = 0; i <= x; i += 12)
                            {
                                string val13 = (double.Parse(TxtTem.Text) - i).ToString();
                                string val14 = double.Parse(val12).ToString();
                                string val15 = "1000";
                                string flval = (((double.Parse(val14) * double.Parse(val13)) / double.Parse(val15)) * 6).ToString();
                                val17 = (double.Parse(flval)).ToString();
                                total = total + Convert.ToDouble(val17);
                                //Txtpre.Text = total.ToString("0.00");
                                tkh1 = total.ToString("C").Replace("RM", "").Replace("$", "");
                            }


                        }


                        if (cnt_tkh.Rows.Count == 2)
                        {

                            double total1 = 0;
                            int x1 = Int32.Parse(TxtTem.Text);
                            for (int i = 0; i <= x1; i += 12)
                            {
                                string val13 = (double.Parse(TxtTem.Text) - i).ToString();
                                string val14 = double.Parse(val12).ToString();
                                string val15 = "1000";
                                string flval = (((double.Parse(val14) * double.Parse(val13)) / double.Parse(val15)) * 6).ToString();
                                val17 = (double.Parse(flval)).ToString();
                                total1 = total1 + Convert.ToDouble(val17);
                                tkh2 = total1.ToString("C").Replace("RM", "").Replace("$", "");
                            }


                        }
                        if (tkh2 != "")
                        {
                            //Txtpre.Text = (double.Parse(tkh1) + double.Parse(tkh2)).ToString("C").Replace("$", "");
                        }
                        else
                        {
                            // Txtpre.Text = (double.Parse(tkh1) + 0).ToString("C").Replace("$", "");
                        }

                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Bilangan Penjamin',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                        //}
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Bilangan Penjamin',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Bilangan Penjamin',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Bilangan Penjamin',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Pelaburan_Anggota/PP_kiraan.aspx");
    }

    protected void clk_bak(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Pelaburan_Anggota/PP_kiraan_view.aspx");
    }
}