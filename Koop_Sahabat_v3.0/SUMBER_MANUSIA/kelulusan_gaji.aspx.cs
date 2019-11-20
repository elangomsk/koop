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
using System.Threading;
using System.Net;
using System.Net.Mail;
using System.Collections;

public partial class kelulusan_gaji : System.Web.UI.Page
{
    DBConnection dbcon = new DBConnection();
    Mailcoms ObjMail = new Mailcoms();
    SMS ObjSms = new SMS();
    CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
    DBConnection Dblog = new DBConnection();
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string level, userid;
    string uniqueId, uniqueId2, uniqueId3, uniqueId4, unq_id1, unq_id2, unq_id3, unq_id4;
    string jurnal_qry = string.Empty;
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    string Status = string.Empty, sqry = string.Empty;
    String count_text;
    int incNumber = 1;
    int incNumber1 = 1;
    Boolean[] notNulls;
    protected void Page_Load(object sender, EventArgs e)
    {
        //int colCount = gvCustomers.Columns.Count;
        //notNulls = new Boolean[colCount];

        //for (int i = 0; i < colCount; i++)
        //{
        //    notNulls[i] = false;
        //}
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                Month();
                Year();
                userid = Session["New"].ToString();
                grid();
                //TextBox1.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }


    void Month()
    {
        DateTimeFormatInfo info1 = DateTimeFormatInfo.GetInstance(null);
        int Month = DateTime.Now.Month - 4;
        for (int X = Month; X <= DateTime.Now.Month; X++)
        {
            DD_bulancaruman.Items.Add(new ListItem(X.ToString("#0"), X.ToString("#0")));
        }
        string abc = DateTime.Now.Month.ToString("#0");
        //string abc = DD_bulancaruman.SelectedValue;

        DataSet Ds = new DataSet();
        try
        {

            string com = "select hr_month_Code,hr_month_desc from Ref_hr_month ORDER BY hr_month_Code";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_bulancaruman.DataSource = dt;
            DD_bulancaruman.DataBind();
            DD_bulancaruman.DataTextField = "hr_month_desc";
            DD_bulancaruman.DataValueField = "hr_month_Code";
            DD_bulancaruman.DataBind();
            DD_bulancaruman.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
        DD_bulancaruman.SelectedValue = abc.PadLeft(2, '0');
    }

    private void Year()
    {

        DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);
        int year = DateTime.Now.Year - 5;
        for (int Y = year; Y <= DateTime.Now.Year; Y++)
        {
            txt_tahun.Items.Add(new ListItem(Y.ToString(), Y.ToString()));
        }
        txt_tahun.SelectedValue = DateTime.Now.Year.ToString();

    }
    protected void BindGridview(object sender, EventArgs e)
    {
        if (txt_tahun.SelectedValue != "")
        {
            Session["chkditems"] = null;
            //show_cnt1.Visible = true;
            grid();
        }
        else
        {
            //grid();
            show_cnt1.Visible = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Tahun.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }
    protected void grid()
    {

        string schk = string.Empty;
        if (chk_assign_rkd.Checked == true)
        {
            schk = " and ISNULL(inc_app_sts,'')='A'";
            Button3.Visible = false;
        }
        else
        {
            schk = " and ISNULL(inc_app_sts,'')='P'";
            Button3.Visible = true;
        }

        if (txt_tahun.SelectedValue != "" && DD_bulancaruman.SelectedValue != "")
        {
            sqry = " where inc_month='" + DD_bulancaruman.SelectedValue + "' and inc_year='" + txt_tahun.SelectedValue + "' "+ schk + "";
        }
        else if (txt_tahun.SelectedValue != "" && DD_bulancaruman.SelectedValue == "")
        {
            sqry = "where inc_year='" + txt_tahun.SelectedValue + "' " + schk + "";
        }
        else
        {
            sqry = "where inc_month='' and inc_year='' " + schk + "";
        }


       // Button3.Visible = true;
        Button4.Visible = true;
        string query = "select a.inc_org_id,a.inc_month,a.inc_year,a.inc_staff_no,hsp.stf_name,a.inc_grade_cd,a.inc_salary_amt as inc_salary_amt,a.inc_cumm_fix_allwnce_amt as inc_cumm_fix_allwnce_amt,a.inc_cumm_xtra_allwnce_amt as inc_cumm_xtra_allwnce_amt,inc_bonus_amt,inc_kpi_bonus_amt, inc_ot_amt,inc_total_deduct_amt,a.inc_kwsp_amt as inc_kwsp_amt,a.inc_emp_kwsp_amt as inc_emp_kwsp_amt,a.inc_perkeso_amt as inc_perkeso_amt,case when ISNULL(a.inc_SIP_amt,'') = '' then '0.00' else a.inc_SIP_amt end as inc_SIP_amt,a.inc_emp_perkeso_amt as inc_emp_perkeso_amt,case when ISNULL(a.inc_emp_SIP_amt,'') = '' then '0.00' else a.inc_emp_SIP_amt end as inc_emp_SIP_amt,a.inc_pcb_amt as inc_pcb_amt,inc_cp38_amt,a.inc_gross_amt as inc_gross_amt,a.inc_nett_amt as inc_nett_amt,a.inc_total_deduct_amt as inc_total_deduct_amt,ISNULL(inc_ded_amt,'0.00') as inc_ded_amt,inc_app_sts,ISNULL(inc_tunggakan_amt,'0.00') as inc_tunggakan_amt  from (select * from hr_income " + sqry + ") as a left join  hr_staff_profile as hsp on hsp.stf_staff_no=a.inc_staff_no";
        using (SqlConnection con = new SqlConnection(conString))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        DataSet ds = new DataSet();
                        sda.Fill(ds);
                        if (ds.Tables[0].Rows.Count != 0)
                        {
                            gvCustomers.DataSource = dt;
                            gvCustomers.DataBind();
                            decimal salary = dt.AsEnumerable().Sum(row => row.Field<decimal>("inc_salary_amt"));
                            decimal ext_allow = dt.AsEnumerable().Sum(row => row.Field<decimal>("inc_cumm_fix_allwnce_amt"));
                            decimal ot_allow = dt.AsEnumerable().Sum(row => row.Field<decimal>("inc_cumm_xtra_allwnce_amt"));
                            decimal bonus_thn = (dt.AsEnumerable().Sum(row => row.Field<decimal>("inc_bonus_amt")));
                            decimal bonus_kpi = (dt.AsEnumerable().Sum(row => row.Field<decimal>("inc_kpi_bonus_amt")));
                            decimal ot_klm = dt.AsEnumerable().Sum(row => row.Field<decimal>("inc_ot_amt"));
                            decimal jp = dt.AsEnumerable().Sum(row => row.Field<decimal>("inc_total_deduct_amt"));
                            decimal car_kwsp = (dt.AsEnumerable().Sum(row => row.Field<decimal>("inc_kwsp_amt")));
                            decimal car_kwsp_emp = (dt.AsEnumerable().Sum(row => row.Field<decimal>("inc_emp_kwsp_amt")));
                            decimal car_perkeso = (dt.AsEnumerable().Sum(row => row.Field<decimal>("inc_perkeso_amt")));
                            decimal car_perkeso_emp = (dt.AsEnumerable().Sum(row => row.Field<decimal>("inc_emp_perkeso_amt")));
                            decimal sip = (dt.AsEnumerable().Sum(row => row.Field<decimal>("inc_SIP_amt")));
                            decimal sip_emp = (dt.AsEnumerable().Sum(row => row.Field<decimal>("inc_emp_SIP_amt")));
                            decimal pcb = dt.AsEnumerable().Sum(row => row.Field<decimal>("inc_pcb_amt"));
                            decimal cp_38 = dt.AsEnumerable().Sum(row => row.Field<decimal>("inc_cp38_amt"));
                            decimal pkrm = dt.AsEnumerable().Sum(row => row.Field<decimal>("inc_gross_amt"));
                            decimal pbrm = dt.AsEnumerable().Sum(row => row.Field<decimal>("inc_nett_amt"));
                            decimal ded_amt = dt.AsEnumerable().Sum(row => row.Field<decimal>("inc_ded_amt"));
                            decimal tun_amt = dt.AsEnumerable().Sum(row => row.Field<decimal>("inc_tunggakan_amt"));

                            Label ft_01 = (Label)gvCustomers.FooterRow.FindControl("ftr_001");
                            Label ft_02 = (Label)gvCustomers.FooterRow.FindControl("ftr_002");
                            Label ft_03 = (Label)gvCustomers.FooterRow.FindControl("ftr_003");
                            Label ft_04 = (Label)gvCustomers.FooterRow.FindControl("ftr_004");
                            Label ft_04_2 = (Label)gvCustomers.FooterRow.FindControl("ftr_004_2");
                            Label ft_05 = (Label)gvCustomers.FooterRow.FindControl("ftr_005");
                            Label ft_06 = (Label)gvCustomers.FooterRow.FindControl("ftr_006");                            
                            Label ft_06_1 = (Label)gvCustomers.FooterRow.FindControl("ftr_006_1");
                            Label ft_07 = (Label)gvCustomers.FooterRow.FindControl("ftr_007");
                            Label ft_07_2 = (Label)gvCustomers.FooterRow.FindControl("ftr_007_2");
                            Label ft_08 = (Label)gvCustomers.FooterRow.FindControl("ftr_008");
                            Label ft_08_2 = (Label)gvCustomers.FooterRow.FindControl("ftr_008_2");
                            Label ft_09 = (Label)gvCustomers.FooterRow.FindControl("ftr_009");
                            Label ft_09_2 = (Label)gvCustomers.FooterRow.FindControl("ftr_009_2");
                            Label ft_10 = (Label)gvCustomers.FooterRow.FindControl("ftr_010");
                            Label ft_11 = (Label)gvCustomers.FooterRow.FindControl("ftr_011");
                            Label ft_12 = (Label)gvCustomers.FooterRow.FindControl("ftr_012");
                            Label ft_13 = (Label)gvCustomers.FooterRow.FindControl("ftr_013");
                            Label ft_14 = (Label)gvCustomers.FooterRow.FindControl("ftr_014");

                            gvCustomers.FooterRow.Cells[2].Text = "<strong>KESELURUHAN (RM)</strong>";
                            gvCustomers.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;

                            ft_01.Text = salary.ToString("C").Replace("$", "");
                            ft_02.Text = ext_allow.ToString("C").Replace("$", "");
                            ft_03.Text = ot_allow.ToString("C").Replace("$", "");
                            ft_04.Text = bonus_thn.ToString("C").Replace("$", "");
                            ft_04_2.Text = bonus_kpi.ToString("C").Replace("$", "");
                            ft_05.Text = ot_klm.ToString("C").Replace("$", "");
                            ft_06.Text = jp.ToString("C").Replace("$", "");
                            ft_06_1.Text = tun_amt.ToString("C").Replace("$", "").Replace("RM", "");
                            ft_07.Text = car_kwsp.ToString("C").Replace("$", "");
                            ft_07_2.Text = car_kwsp_emp.ToString("C").Replace("$", "");
                            ft_08.Text = car_perkeso.ToString("C").Replace("$", "");
                            ft_08_2.Text = car_perkeso_emp.ToString("C").Replace("$", "");
                            ft_09.Text = sip.ToString("C").Replace("$", "");
                            ft_09_2.Text = sip.ToString("C").Replace("$", "");
                            ft_10.Text = pcb.ToString("C").Replace("$", "");
                            ft_11.Text = cp_38.ToString("C").Replace("$", "");
                            ft_12.Text = pkrm.ToString("C").Replace("$", "");
                            ft_13.Text = pbrm.ToString("C").Replace("$", "");
                            ft_14.Text = ded_amt.ToString("C").Replace("$", "");
                            Session["cr_mnth_slry"] = ft_13.Text;
                        }
                        else
                        {

                            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                            gvCustomers.DataSource = ds;
                            gvCustomers.DataBind();
                            int columncount = gvCustomers.Rows[0].Cells.Count;
                            gvCustomers.Rows[0].Cells.Clear();
                            gvCustomers.Rows[0].Cells.Add(new TableCell());
                            gvCustomers.Rows[0].Cells[0].ColumnSpan = columncount;
                            gvCustomers.Rows[0].Cells[0].Text = "<center><strong>Rekod Tidak Dijumpai. Sila Lakukan Semula Carian</strong></center>";
                            Button3.Visible = false;
                            Button4.Visible = false;

                        }

                    }
                }
            }
        }
        //SqlConnection con = new SqlConnection(conString);
        //con.Open();
        //SqlCommand cmd = new SqlCommand(sqry, con);
        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        //da.Fill(ds);
        //if (ds.Tables[0].Rows.Count == 0)
        //{

        //    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
        //    gvSelected.DataSource = ds;
        //    gvSelected.DataBind();
        //    int columncount = gvSelected.Rows[0].Cells.Count;
        //    gvSelected.Rows[0].Cells.Clear();
        //    gvSelected.Rows[0].Cells.Add(new TableCell());
        //    gvSelected.Rows[0].Cells[0].ColumnSpan = columncount;
        //    gvSelected.Rows[0].Cells[0].Text = "<center><strong>Rekod Tidak Dijumpai. Sila Lakukan Semula Carian</strong></center>";
        //    Button3.Visible = false;
        //    Button4.Visible = false;
        //}
        //else
        //{
        //    Button3.Visible = true;
        //    gvSelected.DataSource = ds;
        //    gvSelected.DataBind();

        //}
        //con.Close();
    }

    protected void gvProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void gvProducts1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void gvProducts3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void gvCustomers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        System.Web.UI.WebControls.Label sts = (System.Web.UI.WebControls.Label)e.Row.FindControl("inc_asts");
        System.Web.UI.WebControls.CheckBox cb = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("chkSelect");
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (sts.Text == "A")
            {
                cb.Enabled = false;
            }
            else
            {
                cb.Enabled = true;
            }

            string customerId = gvCustomers.DataKeys[e.Row.RowIndex].Value.ToString().Replace("'", "''");
                GridView gvOrders = e.Row.FindControl("gvProducts") as GridView;
                gvOrders.DataSource = GetData(string.Format("select je.hr_elau_desc,format(fa.fxa_allowance_amt,'##,###.00') fxa_allowance_amt from hr_fixed_allowance fa inner join Ref_hr_jenis_elaun  je on je.hr_elau_Code=fa.fxa_allowance_type_cd inner join hr_staff_profile hs on hs.stf_staff_no=fa.fxa_staff_no where ('" + txt_tahun.SelectedValue + "-"+ DD_bulancaruman.SelectedValue + "' between FORMAT(fxa_eff_dt,'yyyy-MM') And FORMAT(fxa_end_dt,'yyyy-MM')) and hs.stf_name  = '{0}'", customerId));
                gvOrders.DataBind();


                string customerId1 = gvCustomers.DataKeys[e.Row.RowIndex].Value.ToString().Replace("'", "''");
                GridView gvOrders1 = e.Row.FindControl("gvProducts1") as GridView;
                gvOrders1.DataSource = GetData(string.Format("select je.hr_elau_desc,format(fa.xta_allowance_amt,'##,###.00') xta_allowance_amt from hr_extra_allowance fa inner join Ref_hr_jenis_elaun  je on je.hr_elau_Code=fa.xta_allowance_type_cd inner join hr_staff_profile hs on hs.stf_staff_no=fa.xta_staff_no where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(xta_eff_dt,'yyyy-MM') And FORMAT(xta_end_dt,'yyyy-MM')) and hs.stf_name  = '{0}'", customerId1));
                gvOrders1.DataBind();
                e.Row.Cells[0].Attributes.Add("class", "text"); //Change to .cell[0]

                string customerId3 = gvCustomers.DataKeys[e.Row.RowIndex].Value.ToString().Replace("'", "''");
                GridView gvOrders3 = e.Row.FindControl("gvProducts3") as GridView;
                gvOrders3.DataSource = GetData(string.Format("select je.hr_poto_desc,fa.ded_deduct_amt as xta_allowance_amt from hr_deduction fa inner join ref_hr_potongan  je on je.hr_poto_Code=fa.ded_deduct_type_cd inner join hr_staff_profile hs on hs.stf_staff_no=fa.ded_staff_no where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(ded_start_dt,'yyyy-MM') And FORMAT(ded_end_dt,'yyyy-MM')) and hs.stf_name  = '{0}'", customerId1));
                gvOrders3.DataBind();
            
        }
    }

    protected void OnCheckedChanged(object sender, EventArgs e)
    {
        bool isUpdateVisible = false;
        CheckBox chk = (sender as CheckBox);
        if (chk.ID == "chkAll")
        {
            foreach (GridViewRow row in gvCustomers.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked = chk.Checked;
                }
            }
        }
        CheckBox chkAll = (gvCustomers.HeaderRow.FindControl("chkAll") as CheckBox);
        chkAll.Checked = true;
        foreach (GridViewRow row in gvCustomers.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                bool isChecked = row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
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

    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        savechkdvls();
        gvCustomers.PageIndex = e.NewPageIndex;
        if (gvCustomers.PageCount - 1 == gvCustomers.PageIndex)
        {
            gvCustomers.ShowFooter = true;
        }
        else
        {
            gvCustomers.ShowFooter = false;
        }
        this.grid();
        //gvSelected.DataBind();
        chkdvaluesp();
    }

    private void chkdvaluesp()
    {

        ArrayList usercontent = (ArrayList)Session["chkditems"];

        if (usercontent != null && usercontent.Count > 0)
        {

            foreach (GridViewRow gvrow in gvCustomers.Rows)
            {
                if (gvCustomers.Rows.Count != 0)
                {
                    Int64 index = Convert.ToInt64(gvCustomers.DataKeys[gvrow.RowIndex].Value);

                    if (usercontent.Contains(index))
                    {

                        System.Web.UI.WebControls.CheckBox myCheckBox1 = (System.Web.UI.WebControls.CheckBox)gvrow.FindControl("chkSelect");

                        myCheckBox1.Checked = true;

                    }
                }

            }

        }

    }

    private void savechkdvls()
    {

        ArrayList usercontent = new ArrayList();

        Int64 index = -1;


        foreach (GridViewRow gvrow in gvCustomers.Rows)
        {
            if (gvCustomers.Rows.Count != 0)
            {
                index = Convert.ToInt64(gvCustomers.DataKeys[gvrow.RowIndex].Value);

                bool result = ((System.Web.UI.WebControls.CheckBox)gvrow.FindControl("chkSelect")).Checked;


                // Check in the Session

                if (Session["chkditems"] != null)

                    usercontent = (ArrayList)Session["chkditems"];

                if (result)
                {

                    if (!usercontent.Contains(index))

                        usercontent.Add(index);

                }

                else
                {
                    usercontent.Remove(index);
                }
            }


        }

        if (usercontent != null && usercontent.Count > 0)

            Session["chkditems"] = usercontent;

    }
    private static DataTable GetData(string query)
    {
        string constr = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = query;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }


    public void BindProducts1(string orderId, GridView gvProducts1)
    {
        gvProducts1.ToolTip = orderId.ToString();
        gvProducts1.DataSource = GetData(string.Format("select je.hr_elau_desc,format(fa.xta_allowance_amt,'##,###.00') xta_allowance_amt from hr_extra_allowance fa inner join Ref_hr_jenis_elaun  je on je.hr_elau_Code=fa.xta_allowance_type_cd inner join hr_staff_profile hs on hs.stf_staff_no=fa.xta_staff_no where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(xta_eff_dt,'yyyy-MM') And FORMAT(xta_end_dt,'yyyy-MM')) and  hs.stf_name  = '{0}'", orderId));
        gvProducts1.DataBind();
    }

    public void BindProducts2(string orderId3, GridView gvProducts3)
    {
        gvProducts3.ToolTip = orderId3.ToString();
        gvProducts3.DataSource = GetData(string.Format("select je.hr_poto_desc,fa.ded_deduct_amt as xta_allowance_amt from hr_deduction fa inner join ref_hr_potongan  je on je.hr_poto_Code=fa.ded_deduct_type_cd inner join hr_staff_profile hs on hs.stf_staff_no=fa.ded_staff_no where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(ded_start_dt,'yyyy-MM') And FORMAT(ded_end_dt,'yyyy-MM')) and hs.stf_name  = '{0}'", orderId3));
        gvProducts3.DataBind();
    }
    public void BindProducts(string orderId, GridView gvProducts)
    {
        gvProducts.ToolTip = orderId.ToString();
        gvProducts.DataSource = GetData(string.Format("select je.hr_elau_desc,format(fa.fxa_allowance_amt,'##,###.00') fxa_allowance_amt from hr_fixed_allowance fa inner join Ref_hr_jenis_elaun  je on je.hr_elau_Code=fa.fxa_allowance_type_cd inner join hr_staff_profile hs on hs.stf_staff_no=fa.fxa_staff_no where ('" + txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue + "' between FORMAT(fxa_eff_dt,'yyyy-MM') And FORMAT(fxa_end_dt,'yyyy-MM')) and hs.stf_name  = '{0}'", orderId));
        gvProducts.DataBind();
    }

    //protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    gvCustomers.PageIndex = e.NewPageIndex;
    //    grid();
    //    gvCustomers.DataBind();        
    //}

    protected void submit_button(object sender, EventArgs e)
    {
        string rcount = string.Empty, btch_name = string.Empty, perkara = string.Empty;
        int count = 0;
        foreach (GridViewRow gvrow in gvCustomers.Rows)
        {
            var checkbox = gvrow.FindControl("chkSelect") as CheckBox;
            if (checkbox.Checked)
            {
                count++;
            }
            rcount = count.ToString();
        }
        if (rcount != "0")
        {
            string act_dt = txt_tahun.SelectedValue + "-" + DD_bulancaruman.SelectedValue;
            gvCustomers.AllowPaging = false;
            savechkdvls();
            this.grid();
            gvCustomers.DataBind();
            chkdvaluesp();
            int[] no = new int[gvCustomers.Rows.Count];
            int i = 0;
            foreach (GridViewRow gvrow in gvCustomers.Rows)
            {
                var checkbox = gvrow.FindControl("chkSelect") as CheckBox;
                if (checkbox.Checked == true)
                {
                    var nokak = gvrow.FindControl("labstf") as Label;
                    var mnth = gvrow.FindControl("ss_val1") as Label;
                    var year = gvrow.FindControl("ss_val2") as System.Web.UI.WebControls.Label;
                    var org_id = gvrow.FindControl("Label1_org_id") as System.Web.UI.WebControls.Label;

                    DataTable chk_income = new DataTable();
                    chk_income = DBCon.Ora_Execute_table("select inc_staff_no from hr_income where inc_staff_no='" + nokak.Text + "' and inc_month='" + mnth.Text + "' and inc_year='" + year.Text + "' and inc_org_id='" + org_id.Text + "' and inc_app_sts ='P'");
                    if (chk_income.Rows.Count != 0)
                    {

                        string Inssql1_bon = "UPDATE hr_income set inc_app_sts='A',inc_upd_id='" + Session["New"].ToString() + "',inc_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where inc_staff_no='" + nokak.Text + "' and inc_month='" + mnth.Text + "' and inc_year='" + year.Text + "' and inc_org_id='" + org_id.Text + "' and inc_app_sts ='P'";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql1_bon);
                        if (Status == "SUCCESS")
                        {
                           
                        }
                    }
                }
            }

            if (Status == "SUCCESS")
            {

                // Integration Part
                
                btch_name = "HR" + DD_bulancaruman.SelectedValue + txt_tahun.SelectedValue;
                DataTable dd_income = new DataTable();
                dd_income = DBCon.Ora_Execute_table("select inc_batch_name,inc_month,inc_year,Format(inc_upd_dt,'yyyy-MM-dd') upddt,count(inc_staff_no) cnt,sum(cast(inc_nett_amt as money)) nett_amt,sum(cast(inc_gross_amt as money)) gross_amt, "
                    + " sum(cast(inc_salary_amt as money)) sal_amt,sum(cast(inc_ot_amt as money)) ot_amt,sum(cast(inc_bonus_amt as money)) bns_amt,sum(cast(inc_kpi_bonus_amt as money)) kpi_bns_amt "
                    + " ,sum(cast(inc_kwsp_amt as money)) kwsp_amt,sum(cast(inc_perkeso_amt as money)) perk_amt,sum(cast(inc_pcb_amt as money)) pcb_amt,sum(cast(inc_SIP_amt as money)) sip_amt "
                    + " ,sum(cast(inc_tunggakan_amt as money)) tun_amt,sum(cast(inc_cumm_fix_allwnce_amt as money)) fix_amt,sum(cast(inc_cumm_xtra_allwnce_amt as money)) xta_amt,sum(cast(inc_cumm_deduct_amt as money)) ded_amt "
                    + " ,sum(cast(inc_emp_kwsp_amt as money)) emp_kwsp_amt,sum(cast(inc_emp_perkeso_amt as money)) emp_perk_amt,sum(cast(inc_emp_SIP_amt as money)) emp_sip_amt,sum(cast(inc_cp38_amt as money)) cp38_amt "
                    + " from hr_income WHERE inc_batch_name='" + btch_name + "' "
                    + " and inc_app_sts='A' and Format(inc_upd_dt,'yyyy-MM-dd')='" + DateTime.Now.ToString("yyyy-MM-dd") + "' group by inc_batch_name,inc_month,inc_year,Format(inc_upd_dt,'yyyy-MM-dd')");

                DataTable ddsha = new DataTable();
                

                if (dd_income.Rows.Count != 0)
                {

                    GetUniqueInv();
                    double gaji_sal = (double.Parse(dd_income.Rows[0]["gross_amt"].ToString()) + double.Parse(dd_income.Rows[0]["emp_kwsp_amt"].ToString()) + double.Parse(dd_income.Rows[0]["emp_perk_amt"].ToString()) + double.Parse(dd_income.Rows[0]["emp_sip_amt"].ToString()));
                    //GAJI KAKITANGAN
                    if (gaji_sal > 0)
                    {
                        
                        perkara = "GAJI KAKITANGAN-" + dd_income.Rows[0]["inc_month"].ToString() + "-" + dd_income.Rows[0]["inc_year"].ToString();
                        string Inssql_gk = "Insert into KW_jurnal_inter (no_permohonan,no_Rujukan,tarikh_lulus,Terma,Jenis_permohonan,perkara,nama_pelanggan_code,Overall,Status,crt_id,cr_dt) "
                            + " Values ('" + unq_id1 + "','" + dd_income.Rows[0]["inc_batch_name"].ToString() + "','" + dd_income.Rows[0]["upddt"].ToString() + "','30','16', "
                            + " '" + perkara + "','M0001','" + gaji_sal + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql_gk);
                    }

                    //PENDAPATAN BERSIH

                    if (dd_income.Rows[0]["nett_amt"].ToString().Trim() != "0.00")
                    {
                        string Inssql_items_gp = "Insert into KW_jurnal_inter_items (no_permohonan,keterangan,jumlah,Overall,Status,crt_id,cr_dt,pv_item) "
                            + " Values ('" + unq_id1 + "','PENDAPATAN BERSIH','" + dd_income.Rows[0]["cnt"].ToString() + "','" + dd_income.Rows[0]["nett_amt"].ToString() + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','1')";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql_items_gp);
                    }

                    //GAJI POKOK

                    if (dd_income.Rows[0]["sal_amt"].ToString().Trim() != "0.00")
                    {
                        string Inssql_items_gp = "Insert into KW_jurnal_inter_items (no_permohonan,keterangan,jumlah,Overall,Status,crt_id,cr_dt,pv_item) "
                            + " Values ('" + unq_id1 + "','GAJI POKOK','" + dd_income.Rows[0]["cnt"].ToString() + "','" + dd_income.Rows[0]["sal_amt"].ToString() + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','0')";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql_items_gp);
                    }

                    //KERJA LEBIH MASA

                    if (dd_income.Rows[0]["ot_amt"].ToString().Trim() != "0.00")
                    {
                        string Inssql_items_klm = "Insert into KW_jurnal_inter_items (no_permohonan,keterangan,jumlah,Overall,Status,crt_id,cr_dt,pv_item) "
                       + " Values ('" + unq_id1 + "','KERJA LEBIH MASA','" + dd_income.Rows[0]["cnt"].ToString() + "','" + dd_income.Rows[0]["ot_amt"].ToString() + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','0')";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql_items_klm);
                    }

                    //BONUS TAHUNAN

                    if (dd_income.Rows[0]["bns_amt"].ToString().Trim() != "0.00")
                    {
                        string Inssql_items_Bt = "Insert into KW_jurnal_inter_items (no_permohonan,keterangan,jumlah,Overall,Status,crt_id,cr_dt,pv_item) "
                       + " Values ('" + unq_id1 + "','BONUS TAHUNAN','" + dd_income.Rows[0]["cnt"].ToString() + "','" + dd_income.Rows[0]["bns_amt"].ToString() + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','0')";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql_items_Bt);
                    }

                    //BONUS KPI

                    if (dd_income.Rows[0]["kpi_bns_amt"].ToString().Trim() != "0.00")
                    {
                        string Inssql_items_BK = "Insert into KW_jurnal_inter_items (no_permohonan,keterangan,jumlah,Overall,Status,crt_id,cr_dt,pv_item) "
                       + " Values ('" + unq_id1 + "','BONUS KPI','" + dd_income.Rows[0]["cnt"].ToString() + "','" + dd_income.Rows[0]["kpi_bns_amt"].ToString() + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','0')";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql_items_BK);
                    }

                    //Lain-Lain

                    if (dd_income.Rows[0]["tun_amt"].ToString().Trim() != "0.00")
                    {
                        string Inssql_items_tun = "Insert into KW_jurnal_inter_items (no_permohonan,keterangan,jumlah,Overall,Status,crt_id,cr_dt,pv_item) "
                       + " Values ('" + unq_id1 + "','Lain-Lain','" + dd_income.Rows[0]["cnt"].ToString() + "','" + dd_income.Rows[0]["tun_amt"].ToString() + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','0')";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql_items_tun);
                    }

                    DataTable sel_organ = new DataTable();
                    sel_organ = DBCon.Ora_Execute_table("SELECT STUFF ((SELECT ',' + inc_staff_no  FROM hr_income where inc_batch_name='"+ btch_name + "' and inc_app_sts='A' and Format(inc_upd_dt,'yyyy-MM-dd')='" + DateTime.Now.ToString("yyyy-MM-dd") + "' FOR XML PATH ('')  ),1,1,'')  as scode");

                    //fixed
                
                    DataTable fix_all = new DataTable();
                    fix_all = DBCon.Ora_Execute_table("select fxa_allowance_type_cd,sum(fxa_allowance_amt) fx_amt,count(fxa_staff_no) cnt from hr_fixed_allowance where ('" + act_dt + "' between FORMAT(fxa_eff_dt,'yyyy-MM') And FORMAT(fxa_end_dt,'yyyy-MM')) and fxa_staff_no IN ('" + sel_organ.Rows[0]["scode"].ToString().Replace(",","','") + "') group by fxa_allowance_type_cd");

                    if (fix_all.Rows.Count != 0)
                    {
                        for (int k1 = 0; k1 < fix_all.Rows.Count; k1++)
                        {
                            DataTable sel_jenis = new DataTable();
                            sel_jenis = DBCon.Ora_Execute_table("select * from Ref_hr_jenis_elaun where hr_elau_Code='" + fix_all.Rows[k1]["fxa_allowance_type_cd"].ToString() + "'");
                            if (fix_all.Rows.Count != 0)
                            {

                                if (fix_all.Rows[k1]["fx_amt"].ToString().Trim() != "0.00")
                                {
                                    string Inssql_items_ET = "Insert into KW_jurnal_inter_items (no_permohonan,keterangan,jumlah,Overall,Status,crt_id,cr_dt,pv_item) "
                                   + " Values ('" + unq_id1 + "','" + sel_jenis.Rows[0]["hr_elau_desc"].ToString() + "','" + fix_all.Rows[k1]["cnt"].ToString() + "','" + fix_all.Rows[k1]["fx_amt"].ToString() + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','0')";
                                    Status = DBCon.Ora_Execute_CommamdText(Inssql_items_ET);
                                }
                            }
                        }
                    }

                    //xtra

                    DataTable ext_all = new DataTable();
                    ext_all = DBCon.Ora_Execute_table("select xta_allowance_type_cd,sum(xta_allowance_amt) as xta_amt,count(xta_staff_no) cnt from hr_extra_allowance where ('" + act_dt + "' between FORMAT(xta_eff_dt,'yyyy-MM') And FORMAT(xta_end_dt,'yyyy-MM')) and xta_staff_no IN ('" + sel_organ.Rows[0]["scode"].ToString().Replace(",", "','") + "') group by xta_allowance_type_cd");

                    if (ext_all.Rows.Count != 0)
                    {
                        for (int k2 = 0; k2 < ext_all.Rows.Count; k2++)
                        {
                            DataTable sel_jenis = new DataTable();
                            sel_jenis = DBCon.Ora_Execute_table("select * from Ref_hr_jenis_elaun where hr_elau_Code='" + ext_all.Rows[k2]["xta_allowance_type_cd"].ToString() + "'");
                            if (ext_all.Rows.Count != 0)
                            {
                                if (ext_all.Rows[k2]["xta_amt"].ToString().Trim() != "0.00")
                                {
                                    string Inssql_items_llep = "Insert into KW_jurnal_inter_items (no_permohonan,keterangan,jumlah,Overall,Status,crt_id,cr_dt,pv_item) "
                                   + " Values ('" + unq_id1 + "','" + sel_jenis.Rows[0]["hr_elau_desc"].ToString() + "','" + ext_all.Rows[k2]["cnt"].ToString() + "','" + ext_all.Rows[k2]["xta_amt"].ToString() + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','0')";
                                    Status = DBCon.Ora_Execute_CommamdText(Inssql_items_llep);
                                }
                            }
                        }
                    }

                    //Tunggakan

                    DataTable tun_all = new DataTable();
                    tun_all = DBCon.Ora_Execute_table("select tun_type_cd,sum(tun_amt) tun_amt,count(tun_staff_no) cnt from hr_tunggakan where tun_month='" + DD_bulancaruman.SelectedValue + "' and tun_year='" + txt_tahun.SelectedValue + "' and tun_staff_no IN ('" + sel_organ.Rows[0]["scode"].ToString().Replace(",", "','") + "') group by tun_type_cd");

                    if (tun_all.Rows.Count != 0)
                    {
                        for (int k3 = 0; k3 < tun_all.Rows.Count; k3++)
                        {
                            DataTable sel_jenis = new DataTable();
                            sel_jenis = DBCon.Ora_Execute_table("select * from Ref_hr_tunggakan where hr_tung_Code='" + tun_all.Rows[k3]["tun_type_cd"].ToString() + "'");
                            if (tun_all.Rows.Count != 0)
                            {
                                if (tun_all.Rows[k3]["tun_amt"].ToString().Trim() != "0.00")
                                {
                                    string Inssql_items_llep = "Insert into KW_jurnal_inter_items (no_permohonan,keterangan,jumlah,Overall,Status,crt_id,cr_dt,pv_item) "
                                   + " Values ('" + unq_id1 + "','" + sel_jenis.Rows[0]["hr_tung_desc"].ToString() + "','" + tun_all.Rows[k3]["cnt"].ToString() + "','" + tun_all.Rows[k3]["tun_amt"].ToString() + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','0')";
                                    Status = DBCon.Ora_Execute_CommamdText(Inssql_items_llep);
                                }
                            }
                        }
                    }


                    //Deduction

                    DataTable Ded_all = new DataTable();
                    Ded_all = DBCon.Ora_Execute_table("select ded_deduct_type_cd,sum(ded_deduct_amt) ded_amt,count(ded_staff_no) cnt from hr_deduction where ('" + act_dt + "' between FORMAT(ded_start_dt,'yyyy-MM') And FORMAT(ded_end_dt,'yyyy-MM')) and ded_staff_no IN ('" + sel_organ.Rows[0]["scode"].ToString().Replace(",", "','") + "') group by ded_deduct_type_cd");

                    if (Ded_all.Rows.Count != 0)
                    {
                        int pot_no = 6;
                        for (int k4 = 0; k4 < Ded_all.Rows.Count; k4++)
                        {
                            DataTable sel_jenis = new DataTable();
                            sel_jenis = DBCon.Ora_Execute_table("select * from Ref_hr_potongan where hr_poto_Code='" + Ded_all.Rows[k4]["ded_deduct_type_cd"].ToString() + "'");

                            if (sel_jenis.Rows.Count != 0)
                            {
                                if (Ded_all.Rows[k4]["ded_amt"].ToString().Trim() != "0.00")
                                {
                                   
                                    string Inssql_items_llep = "Insert into KW_jurnal_inter_items (no_permohonan,keterangan,jumlah,Overall,Status,crt_id,cr_dt,pv_item) "
                                   + " Values ('" + unq_id1 + "','" + sel_jenis.Rows[0]["hr_poto_desc"].ToString() + "','" + Ded_all.Rows[k4]["cnt"].ToString() + "','" + Ded_all.Rows[k4]["ded_amt"].ToString() + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','"+ pot_no + "')";
                                    Status = DBCon.Ora_Execute_CommamdText(Inssql_items_llep);
                                    ++pot_no;
                                }
                            }
                        }
                    }


                    //KUMPULAN WANG SIMPANAN PEKERJA (KWSP)

                    if (dd_income.Rows[0]["kwsp_amt"].ToString().Trim() != "0.00")
                    {
                        string Inssql_items_kwsp = "Insert into KW_jurnal_inter_items (no_permohonan,keterangan,jumlah,Overall,Status,crt_id,cr_dt,pv_item) "
                       + " Values ('" + unq_id1 + "','KUMPULAN WANG SIMPANAN PEKERJA (KWSP)','" + dd_income.Rows[0]["cnt"].ToString() + "','" + dd_income.Rows[0]["kwsp_amt"].ToString() + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','2')";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql_items_kwsp);
                    }

                    //PERKESO

                    if (dd_income.Rows[0]["perk_amt"].ToString().Trim() != "0.00")
                    {
                        string Inssql_items_perk = "Insert into KW_jurnal_inter_items (no_permohonan,keterangan,jumlah,Overall,Status,crt_id,cr_dt,pv_item) "
                       + " Values ('" + unq_id1 + "','PERKESO','" + dd_income.Rows[0]["cnt"].ToString() + "','" + dd_income.Rows[0]["perk_amt"].ToString() + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','3')";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql_items_perk);
                    }

                    //POTONGAN CUKAI BULANAN (PCB)

                    if (dd_income.Rows[0]["pcb_amt"].ToString().Trim() != "0.00")
                    {
                        string Inssql_items_PCB = "Insert into KW_jurnal_inter_items (no_permohonan,keterangan,jumlah,Overall,Status,crt_id,cr_dt,pv_item) "
                       + " Values ('" + unq_id1 + "','PCB','" + dd_income.Rows[0]["cnt"].ToString() + "','" + dd_income.Rows[0]["pcb_amt"].ToString() + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','4')";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql_items_PCB);
                    }

                    //CP 38

                    if (dd_income.Rows[0]["cp38_amt"].ToString().Trim() != "0.00")
                    {
                        string Inssql_items_cp_38 = "Insert into KW_jurnal_inter_items (no_permohonan,keterangan,jumlah,Overall,Status,crt_id,cr_dt,pv_item) "
                       + " Values ('" + unq_id1 + "','CP 38','" + dd_income.Rows[0]["cnt"].ToString() + "','" + dd_income.Rows[0]["cp38_amt"].ToString() + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','4')";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql_items_cp_38);
                    }


                    //SIP PERKESO

                    if (dd_income.Rows[0]["sip_amt"].ToString().Trim() != "0.00")
                    {
                        string Inssql_items_sip_per = "Insert into KW_jurnal_inter_items (no_permohonan,keterangan,jumlah,Overall,Status,crt_id,cr_dt,pv_item) "
                       + " Values ('" + unq_id1 + "','SIP','" + dd_income.Rows[0]["cnt"].ToString() + "','" + dd_income.Rows[0]["sip_amt"].ToString() + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','5')";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql_items_sip_per);
                    }

                  
                    //CARUMAN KWSP MAJIKAN ----- STATUTORI MAJIKAN --------------

                    if (dd_income.Rows[0]["emp_kwsp_amt"].ToString().Trim() != "0.00")
                    {
                        //perkara = "CARUMAN KWSP MAJIKAN-" + dd_income.Rows[0]["inc_month"].ToString() + "-" + dd_income.Rows[0]["inc_year"].ToString();
                        //string Inssql_SM = "Insert into KW_jurnal_inter (no_permohonan,no_Rujukan,tarikh_lulus,Terma,Jenis_permohonan,perkara,nama_pelanggan_code,Overall,Status,crt_id,cr_dt) "
                        //    + " Values ('" + unq_id1 + "','" + dd_income.Rows[0]["inc_batch_name"].ToString() + "','" + dd_income.Rows[0]["upddt"].ToString() + "','30','16', "
                        //    + " '" + perkara + "','M0001','" + dd_income.Rows[0]["emp_kwsp_amt"].ToString() + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        //Status = DBCon.Ora_Execute_CommamdText(Inssql_SM);


                        string Inssql_items_SM = "Insert into KW_jurnal_inter_items (no_permohonan,keterangan,jumlah,Overall,Status,crt_id,cr_dt,pv_item) "
                      + " Values ('" + unq_id1 + "','CARUMAN KWSP MAJIKAN','" + dd_income.Rows[0]["cnt"].ToString() + "','" + dd_income.Rows[0]["emp_kwsp_amt"].ToString() + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','2')";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql_items_SM);
                    }

                   

                  

                    //PERKESO MAJIKAN

                    if (dd_income.Rows[0]["emp_perk_amt"].ToString().Trim() != "0.00")
                    {
                        //perkara = "PERKESO MAJIKAN-" + dd_income.Rows[0]["inc_month"].ToString() + "-" + dd_income.Rows[0]["inc_year"].ToString();
                        //string Inssql_PM = "Insert into KW_jurnal_inter (no_permohonan,no_Rujukan,tarikh_lulus,Terma,Jenis_permohonan,perkara,nama_pelanggan_code,Overall,Status,crt_id,cr_dt) "
                        //    + " Values ('" + unq_id1 + "','" + dd_income.Rows[0]["inc_batch_name"].ToString() + "','" + dd_income.Rows[0]["upddt"].ToString() + "','30','16', "
                        //    + " '" + perkara + "','M0001','" + dd_income.Rows[0]["emp_perk_amt"].ToString() + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        //Status = DBCon.Ora_Execute_CommamdText(Inssql_PM);


                        string Inssql_items_PM = "Insert into KW_jurnal_inter_items (no_permohonan,keterangan,jumlah,Overall,Status,crt_id,cr_dt,pv_item) "
                            + " Values ('" + unq_id1 + "','PERKESO MAJIKAN','" + dd_income.Rows[0]["cnt"].ToString() + "','" + dd_income.Rows[0]["emp_perk_amt"].ToString() + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','3')";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql_items_PM);
                    }

                    //SIP MAJIKAN
                    if (dd_income.Rows[0]["emp_sip_amt"].ToString().Trim() != "0.00")
                    {
                        //perkara = "SIP MAJIKAN-" + dd_income.Rows[0]["inc_month"].ToString() + "-" + dd_income.Rows[0]["inc_year"].ToString();
                        //string Inssql_SIPM = "Insert into KW_jurnal_inter (no_permohonan,no_Rujukan,tarikh_lulus,Terma,Jenis_permohonan,perkara,nama_pelanggan_code,Overall,Status,crt_id,cr_dt) "
                        //    + " Values ('" + unq_id1 + "','" + dd_income.Rows[0]["inc_batch_name"].ToString() + "','" + dd_income.Rows[0]["upddt"].ToString() + "','30','16', "
                        //    + " '" + perkara + "','M0001','" + dd_income.Rows[0]["emp_sip_amt"].ToString() + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        //Status = DBCon.Ora_Execute_CommamdText(Inssql_SIPM);


                        string Inssql_items_SIPM = "Insert into KW_jurnal_inter_items (no_permohonan,keterangan,jumlah,Overall,Status,crt_id,cr_dt,pv_item) "
                            + " Values ('" + unq_id1 + "','SIP MAJIKAN','" + dd_income.Rows[0]["cnt"].ToString() + "','" + dd_income.Rows[0]["emp_sip_amt"].ToString() + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','5')";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql_items_SIPM);
                    }

                    DataTable dt_upd_format1 = dbcon.Ora_Execute_table("update KW_Format_Nombor_rujukan set cur_format='" + unq_id1 + "',upd_id='" + Session["New"].ToString() + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where doc_type_cd='16' and Status = 'A'");

                    //DataTable dt_upd_format2 = dbcon.Ora_Execute_table("update KW_Format_Nombor_rujukan set cur_format='" + unq_id2 + "',upd_id='" + Session["New"].ToString() + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where doc_type_cd='19' and Status = 'A'");

                    //DataTable dt_upd_format3 = dbcon.Ora_Execute_table("update KW_Format_Nombor_rujukan set cur_format='" + unq_id3 + "',upd_id='" + Session["New"].ToString() + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where doc_type_cd='20' and Status = 'A'");

                    //DataTable dt_upd_format4 = dbcon.Ora_Execute_table("update KW_Format_Nombor_rujukan set cur_format='" + unq_id4 + "',upd_id='" + Session["New"].ToString() + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where doc_type_cd='21' and Status = 'A'");

                }

                gvCustomers.AllowPaging = true;
                send_email();
                savechkdvls();
                grid();
                Session["chkditems"] = null;
                chkdvaluesp();
                
                service.audit_trail("P0207", "Simpan", "", DD_bulancaruman.SelectedItem.Text + "_" + txt_tahun.SelectedItem.Text);
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
        }
        else
        {
            grid();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dipilih.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    private void GetUniqueInv()
    {

        // Gaji Kakitangan
        //string btch_name1 = "GAJI KAKITANGAN-" + DD_bulancaruman.SelectedValue + "-" + txt_tahun.SelectedValue;
        DataTable dt1 = DBCon.Ora_Execute_table("select doc_type_cd,format as fmt,SUBSTRING(cur_format, 1, 12) as lfrmt1,SUBSTRING(cur_format, 13, 4) as lfrmt2,cur_format as cfmt from KW_Format_Nombor_rujukan where doc_type_cd='16' and Status='A'");
        if (dt1.Rows.Count != 0)
        {
            if (dt1.Rows[0]["cfmt"].ToString() == "")
            {
                unq_id1 = dt1.Rows[0]["fmt"].ToString();
            }
            else
            {
                int seqno = Convert.ToInt32(dt1.Rows[0]["lfrmt2"].ToString());
                int newNumber = seqno + 1;
                uniqueId = newNumber.ToString(dt1.Rows[0]["lfrmt1"].ToString() + "0000");
                unq_id1 = uniqueId;
            }

        }
        else
        {
            
            DataTable get_inter_info = dbcon.Ora_Execute_table("select jur_module,jur_item,jur_desc,jur_desc_cd from KW_ref_jurnal_inter where jur_module='M0007' and jur_item='CARUMAN ANGGOTA' and jur_desc_cd='01'");
            DataTable get_doc1 = new DataTable();
            get_doc1 = dbcon.Ora_Execute_table("select Ref_doc_descript as s1 from KW_Ref_Doc_kod where Ref_doc_code='16'");
            if (get_inter_info.Rows.Count != 0)
            {
                DataTable dt = dbcon.Ora_Execute_table("select ISNULL(max(SUBSTRING(no_permohonan,13,2000)),'0') from KW_jurnal_inter  where Jenis_permohonan='GAJI KAKITANGAN' and nama_pelanggan_code='M0001'");
                if (dt.Rows.Count > 0)
                {
                    int seqno = Convert.ToInt32(dt.Rows[0][0].ToString());
                    int newNumber = seqno + 1;
                    uniqueId = newNumber.ToString(get_doc1.Rows[0]["s1"].ToString() + "-" + DateTime.Now.ToString("yyyy") + "-" + "0000");
                    unq_id1 = uniqueId;

                }
                else
                {
                    int newNumber = Convert.ToInt32(uniqueId) + 1;
                    uniqueId = newNumber.ToString(get_doc1.Rows[0]["s1"].ToString() + "-" + DateTime.Now.ToString("yyyy") + "-" + "0000");
                    unq_id1 = uniqueId;
                }
            }
        }


        // CARUMAN KWSP MAJIKAN

        DataTable dt1_ms = DBCon.Ora_Execute_table("select doc_type_cd,format as fmt,SUBSTRING(cur_format, 1, 12) as lfrmt1,SUBSTRING(cur_format, 13, 4) as lfrmt2,cur_format as cfmt from KW_Format_Nombor_rujukan where doc_type_cd='19' and Status='A'");
        if (dt1_ms.Rows.Count != 0)
        {
            if (dt1_ms.Rows[0]["cfmt"].ToString() == "")
            {
                unq_id2 = dt1_ms.Rows[0]["fmt"].ToString();
            }
            else
            {
                int seqno = Convert.ToInt32(dt1_ms.Rows[0]["lfrmt2"].ToString());
                int newNumber = seqno + 1;
                uniqueId2 = newNumber.ToString(dt1_ms.Rows[0]["lfrmt1"].ToString() + "0000");
                unq_id2 = uniqueId2;
            }

        }
        else
        {
            DataTable get_inter_info_ms = dbcon.Ora_Execute_table("select jur_module,jur_item,jur_desc,jur_desc_cd from KW_ref_jurnal_inter where jur_module='M0007' and jur_item='CARUMAN ANGGOTA' and jur_desc_cd='02'");
            DataTable get_doc1_ms = new DataTable();
            get_doc1_ms = dbcon.Ora_Execute_table("select Ref_doc_descript as s1 from KW_Ref_Doc_kod where Ref_doc_code='19'");
            if (get_inter_info_ms.Rows.Count != 0)
            {
                DataTable dt_ms = dbcon.Ora_Execute_table("select ISNULL(max(SUBSTRING(no_permohonan,13,2000)),'0') from KW_jurnal_inter  where Jenis_permohonan='STATUTORI MAJIKAN' and perkara like '%CARUMAN KWSP MAJIKAN%' and nama_pelanggan_code='M0001'");
                if (dt_ms.Rows.Count > 0)
                {
                    int seqno = Convert.ToInt32(dt_ms.Rows[0][0].ToString());
                    int newNumber = seqno + 1;
                    uniqueId2 = newNumber.ToString(get_doc1_ms.Rows[0]["s1"].ToString() + "-" + DateTime.Now.ToString("yyyy") + "-" + "0000");
                    unq_id2 = uniqueId2;

                }
                else
                {
                    int newNumber = Convert.ToInt32(uniqueId2) + 1;
                    uniqueId2 = newNumber.ToString(get_doc1_ms.Rows[0]["s1"].ToString() + "-" + DateTime.Now.ToString("yyyy") + "-" + "0000");
                    unq_id2 = uniqueId2;
                }
            }
        }

        // PERKESO MAJIKAN

        DataTable dt1_pm = DBCon.Ora_Execute_table("select doc_type_cd,format as fmt,SUBSTRING(cur_format, 1, 12) as lfrmt1,SUBSTRING(cur_format, 13, 4) as lfrmt2,cur_format as cfmt from KW_Format_Nombor_rujukan where doc_type_cd='20' and Status='A'");
        if (dt1_pm.Rows.Count != 0)
        {
            if (dt1_pm.Rows[0]["cfmt"].ToString() == "")
            {
                unq_id3 = dt1_pm.Rows[0]["fmt"].ToString();
            }
            else
            {
                int seqno = Convert.ToInt32(dt1_pm.Rows[0]["lfrmt2"].ToString());
                int newNumber = seqno + 1;
                uniqueId3 = newNumber.ToString(dt1_pm.Rows[0]["lfrmt1"].ToString() + "0000");
                unq_id3 = uniqueId3;
            }

        }
        else
        {
            DataTable get_inter_info_ms = dbcon.Ora_Execute_table("select jur_module,jur_item,jur_desc,jur_desc_cd from KW_ref_jurnal_inter where jur_module='M0007' and jur_item='CARUMAN ANGGOTA' and jur_desc_cd='02'");
            DataTable get_doc1_ms = new DataTable();
            get_doc1_ms = dbcon.Ora_Execute_table("select Ref_doc_descript as s1 from KW_Ref_Doc_kod where Ref_doc_code='20'");
            if (get_inter_info_ms.Rows.Count != 0)
            {
                DataTable dt_ms = dbcon.Ora_Execute_table("select ISNULL(max(SUBSTRING(no_permohonan,13,2000)),'0') from KW_jurnal_inter  where Jenis_permohonan='STATUTORI MAJIKAN' and perkara like '%PERKESO MAJIKAN%' and nama_pelanggan_code='M0001'");
                if (dt_ms.Rows.Count > 0)
                {
                    int seqno = Convert.ToInt32(dt_ms.Rows[0][0].ToString());
                    int newNumber = seqno + 1;
                    uniqueId3 = newNumber.ToString(get_doc1_ms.Rows[0]["s1"].ToString() + "-" + DateTime.Now.ToString("yyyy") + "-" + "0000");
                    unq_id3 = uniqueId3;

                }
                else
                {
                    int newNumber = Convert.ToInt32(uniqueId2) + 1;
                    uniqueId3 = newNumber.ToString(get_doc1_ms.Rows[0]["s1"].ToString() + "-" + DateTime.Now.ToString("yyyy") + "-" + "0000");
                    unq_id3 = uniqueId3;
                }
            }
        }

        // SIP MAJIKAN

        DataTable dt1_sip = DBCon.Ora_Execute_table("select doc_type_cd,format as fmt,SUBSTRING(cur_format, 1, 12) as lfrmt1,SUBSTRING(cur_format, 13, 4) as lfrmt2,cur_format as cfmt from KW_Format_Nombor_rujukan where doc_type_cd='21' and Status='A'");
        if (dt1_sip.Rows.Count != 0)
        {
            if (dt1_sip.Rows[0]["cfmt"].ToString() == "")
            {
                unq_id4 = dt1_sip.Rows[0]["fmt"].ToString();
            }
            else
            {
                int seqno = Convert.ToInt32(dt1_sip.Rows[0]["lfrmt2"].ToString());
                int newNumber = seqno + 1;
                uniqueId4 = newNumber.ToString(dt1_sip.Rows[0]["lfrmt1"].ToString() + "0000");
                unq_id4 = uniqueId4;
            }

        }
        else
        {
            DataTable get_inter_info_ms = dbcon.Ora_Execute_table("select jur_module,jur_item,jur_desc,jur_desc_cd from KW_ref_jurnal_inter where jur_module='M0007' and jur_item='CARUMAN ANGGOTA' and jur_desc_cd='02'");
            DataTable get_doc1_ms = new DataTable();
            get_doc1_ms = dbcon.Ora_Execute_table("select Ref_doc_descript as s1 from KW_Ref_Doc_kod where Ref_doc_code='21'");
            if (get_inter_info_ms.Rows.Count != 0)
            {
                DataTable dt_ms = dbcon.Ora_Execute_table("select ISNULL(max(SUBSTRING(no_permohonan,13,2000)),'0') from KW_jurnal_inter  where Jenis_permohonan='STATUTORI MAJIKAN' and perkara like '%SIP MAJIKAN%' and nama_pelanggan_code='M0001'");
                if (dt_ms.Rows.Count > 0)
                {
                    int seqno = Convert.ToInt32(dt_ms.Rows[0][0].ToString());
                    int newNumber = seqno + 1;
                    uniqueId4 = newNumber.ToString(get_doc1_ms.Rows[0]["s1"].ToString() + "-" + DateTime.Now.ToString("yyyy") + "-" + "0000");
                    unq_id4 = uniqueId4;

                }
                else
                {
                    int newNumber = Convert.ToInt32(uniqueId2) + 1;
                    uniqueId4 = newNumber.ToString(get_doc1_ms.Rows[0]["s1"].ToString() + "-" + DateTime.Now.ToString("yyyy") + "-" + "0000");
                    unq_id4 = uniqueId4;
                }
            }
        }


    }
    void send_email()
    {
        TextInfo txtinfo = culinfo.TextInfo;
        DataTable Ds = new DataTable();
        DataTable Ds1 = new DataTable();
        DataTable Ds2 = new DataTable();
        DataTable Ds3 = new DataTable();
        string Mailmsg = string.Empty, Mailmsg1 = string.Empty, Mail_id1 = string.Empty, Mail_id2 = string.Empty, bal_lve = string.Empty;
        try
        {
            Ds = Dblog.Ora_Execute_table("select * from KK_User_Login where KK_userid='C0028'");
            Ds1 = Dblog.Ora_Execute_table("select * from KK_User_Login where KK_userid='C0019'");

            DataTable email_settings = new DataTable();
            email_settings = DBCon.Ora_Execute_table("select config_email_head,config_email_host,config_email_id,config_email_port,config_email_pwd,config_email_url,config_email_web from site_settings where ID='1'");
            var fromemail = new MailAddress(email_settings.Rows[0]["config_email_id"].ToString(), email_settings.Rows[0]["config_email_head"].ToString());
            var fromemailpassword = email_settings.Rows[0]["config_email_pwd"].ToString();
            string subject = email_settings.Rows[0]["config_email_web"].ToString() + " - Salary Application";
            var verifyurl = email_settings.Rows[0]["config_email_url"].ToString();
            var link = verifyurl;
            String strDate = "01/" + DD_bulancaruman.SelectedValue + "/" + txt_tahun.SelectedValue + "";
            DateTime dateTime = DateTime.ParseExact(strDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string pre_year = dateTime.AddMonths(-1).ToString("yyyy");
            string pre_mnth = dateTime.AddMonths(-1).ToString("MM");
            Ds2 = Dblog.Ora_Execute_table("select ISNULL(sum(inc_nett_amt),'0.00') as inc_nett_amt from hr_income where inc_month='" + pre_mnth + "' and inc_year='"+ pre_year + "'");
            Ds3 = Dblog.Ora_Execute_table("select hr_month_Code,hr_month_desc from Ref_hr_month where hr_month_Code='" + pre_mnth + "' ORDER BY hr_month_Code");

            if (Ds3.Rows.Count != 0)
            {
                bal_lve = Ds3.Rows[0]["hr_month_desc"].ToString();
            }
            else
            {
                bal_lve = "";
            }

            if (Ds.Rows[0]["KK_email"].ToString() != "")
            {
                Mail_id1 = Ds.Rows[0]["KK_email"].ToString();
            }

            if (Ds1.Rows[0]["KK_email"].ToString() != "")
            {
                Mail_id2 = Ds1.Rows[0]["KK_email"].ToString();
            }
            // HR Email
            if (Mail_id1 != "")
            {
                var toemail = new MailAddress(Mail_id1);
                string body = "Hello HR,<br/>Pending Approval for " + DD_bulancaruman.SelectedItem.Text + ", " + txt_tahun.SelectedItem.Text + " salary amount of RM " + Session["cr_mnth_slry"].ToString() + ".<br/><br/> Previous month total salary " + bal_lve + ", " + pre_year + " is RM " + double.Parse(Ds2.Rows[0]["inc_nett_amt"].ToString()).ToString("C").Replace("RM","").Replace("$", "") + ".<br/><br/> Thank You,<br/><a><html><body><a href='"+ link + "'> " + email_settings.Rows[0]["config_email_web"].ToString() + " </a></body></html> . </a>";

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
            // Azhari Email
            if (Mail_id2 != "")
            {
                var toemail = new MailAddress(Mail_id2);
                string body = "Hello " + txtinfo.ToTitleCase(Ds1.Rows[0]["Kk_username"].ToString().ToLower()) + ",<br/>Pending Approval for " + DD_bulancaruman.SelectedItem.Text + ", " + txt_tahun.SelectedItem.Text + " salary amount of RM " + Session["cr_mnth_slry"].ToString() + ".<br/><br/> Previous month total salary " + bal_lve + ", " + pre_year + " is RM " + double.Parse(Ds2.Rows[0]["inc_nett_amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "") + ".<br/><br/> Thank You,<br/><a><html><body><a href='" + link + "'> " + email_settings.Rows[0]["config_email_web"].ToString() + " </a></body></html> . </a>";

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
            Session["cr_mnth_slry"] = "";            

        }
        catch (Exception ex)
        {
            service.LogError(ex.ToString());
        }
    }

    protected void GridView1_DataBound(object sender, EventArgs e)
    {
    //    for (int cellNum = gvCustomers.Columns.Count - 1; cellNum >= 0; cellNum--)
    //    {
    //        for (int i = 0; i < notNulls.Length; i++)
    //        {
    //            Boolean myLocalBool = notNulls[i];
    //            if (myLocalBool == false)
    //            {
    //                gvCustomers.Columns[i].Visible = false;

    //            }
    //        }

    //    }
    }

    //protected void GridView1_OnRowCreated(object sender, GridViewRowEventArgs e)
    //{
    //    string vv1 = string.Empty;

    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        DataRowView drv = (DataRowView)e.Row.DataItem;
    //        if ((DataRowView)e.Row.DataItem != null)
    //        {
    //            for (int i = 0; i < gvCustomers.Columns.Count; i++)
    //            {

    //                if (i == 6)
    //                {
    //                    if (drv[i + 2].ToString() != "")
    //                    {
    //                        vv1 = double.Parse(drv[i + 2].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
    //                    }
    //                    else
    //                    {
    //                        vv1 = "1";
    //                    }
    //                }
    //                else if (i == 7)
    //                {
    //                    if (drv[i + 2].ToString() != "")
    //                    {
    //                        vv1 = double.Parse(drv[i + 2].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
    //                    }
    //                    else
    //                    {
    //                        vv1 = "1";
    //                    }
    //                }
    //                else if (i == 8)
    //                {
    //                    if (drv[i + 2].ToString() != "")
    //                    {
    //                        vv1 = double.Parse(drv[i + 2].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
    //                    }
    //                    else
    //                    {
    //                        vv1 = "1";
    //                    }
    //                }
    //                else if (i == 9)
    //                {
    //                    if (drv[i + 2].ToString() != "")
    //                    {
    //                        vv1 = double.Parse(drv[i + 2].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
    //                    }
    //                    else
    //                    {
    //                        vv1 = "1";
    //                    }
    //                }
    //                else if (i == 10)
    //                {
    //                    if (drv[i + 2].ToString() != "")
    //                    {
    //                        vv1 = double.Parse(drv[i + 2].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
    //                    }
    //                    else
    //                    {
    //                        vv1 = "1";
    //                    }
    //                }
    //                else
    //                {
    //                    vv1 = "1";
    //                }

    //                if (vv1 == "0.00" || vv1 == "0.0000" || vv1 == "0")
    //                {
    //                    notNulls[i] = false;

    //                }
    //                else
    //                {
    //                    notNulls[i] = true;
    //                }

    //            }

    //        }
    //    }

    //}
    protected void Reset_btn(object sender, EventArgs e)
    {
        Response.Redirect("../SUMBER_MANUSIA/kelulusan_gaji.aspx");
    }
}