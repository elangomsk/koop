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
using System.IO;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.Threading;
using System.Net;
using System.Net.Mail;


public partial class HR_KLM : System.Web.UI.Page
{

    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    private static int PageSize = 20;
    string qry1 = string.Empty, qry2 = string.Empty;
    string level;
    string Status = string.Empty, Status1 = string.Empty;
    string userid;
    string gt_val1 = string.Empty, gt_val2 = string.Empty;
    string act_dt = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        string script = " $(function () {$('.select2').select2();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        assgn_roles();
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;
                btn_simp.Visible = true;
                
                rr1_chk.Checked = true;
                BindDropdown();
                gd_month();
                DropDownList1.SelectedValue = DateTime.Now.ToString("MM");
                act_dt = Tahun_kew.SelectedItem.Text + "-" + DropDownList1.SelectedValue;
                txt_nama.Attributes.Add("readonly", "readonly");
                txt_gred.Attributes.Add("readonly", "readonly");
                txt_jabat.Attributes.Add("readonly", "readonly");
                txt_jawa.Attributes.Add("readonly", "readonly");
                txt_org.Attributes.Add("readonly", "readonly");
                TextBox3.Attributes.Add("readonly", "readonly");
                txt_stfno.Attributes.Add("readonly", "readonly");
                    txt_stfno.Text = Session["New"].ToString();
                    view_details();
                userid = Session["New"].ToString();


            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    void BindDropdown()
    {
        DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);

        int year = DateTime.Now.Year - 5;

        for (int Y = year; Y <= DateTime.Now.Year; Y++)

        {

            Tahun_kew.Items.Add(new ListItem(Y.ToString(), Y.ToString()));

        }

        Tahun_kew.SelectedValue = DateTime.Now.Year.ToString();

    }
    void gd_month()
    {
        //DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);

        //for (int i = 1; i < 13; i++)
        //{
        //    DropDownList1.Items.Add(new ListItem(info.GetMonthName(i), i.ToString("00")));
        //}

        DateTimeFormatInfo info1 = DateTimeFormatInfo.GetInstance(null);
        int Month = DateTime.Now.Month - 4;
        for (int X = Month; X <= DateTime.Now.Month; X++)
        {
            DropDownList1.Items.Add(new ListItem(X.ToString("#0"), X.ToString("#0")));
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
            DropDownList1.DataSource = dt;
            DropDownList1.DataBind();
            DropDownList1.DataTextField = "hr_month_desc";
            DropDownList1.DataValueField = "hr_month_Code";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
        DropDownList1.SelectedValue = abc.PadLeft(2, '0');
    }
    void app_language()
    {
        if (Session["New"] != null)
        {
            DataTable ste_set = new DataTable();
            ste_set = DBCon.Ora_Execute_table("select * from site_settings where ID IN ('1')");

            DataTable gt_lng = new DataTable();
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('1697','448','505','484','77','1565','513','1675','497','1288','190','1698','461','1699','1700','1701','1702','1703','61','133','15') order by ID ASC");

            CultureInfo culinfo = Thread.CurrentThread.CurrentUICulture;
            TextInfo txtinfo = culinfo.TextInfo;

            //h1_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());
            //bb1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            //bb2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());

            //h3_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            //h3_tag2.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower());

            lbl1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            lbl2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
            lbl3_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            lbl4_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
            lbl5_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            lbl6_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            lbl7_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            //Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    void assgn_roles()
    {
        if (Session["New"] != null)
        {
            DataTable ddokdicno = new DataTable();
            ddokdicno = DBCon.Ora_Execute_table("select * from KK_User_Login where KK_userid = '" + Session["userid"].ToString() + "'");

            if (ddokdicno.Rows.Count != 0)
            {
                DataTable ddokdicno_1 = new DataTable();
                ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0089' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

                if (ddokdicno_1.Rows.Count != 0)
                {

                    gt_val1 = ddokdicno_1.Rows[0]["Edit_chk"].ToString();

                }
            }
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    void view_details()
    {
        if (txt_stfno.Text != "")
        {
            if (gt_val1 == "1")
            {
                btn_simp.Visible = true;                
            }
            else
            {                
                btn_simp.Visible = false;
            }            
            DataTable ddicno = new DataTable();
            ddicno = DBCon.Ora_Execute_table("select stf_staff_no From hr_staff_profile where '" + txt_stfno.Text + "' IN (stf_staff_no)");
            //string stffno = ddicno.Rows[0]["stf_staff_no"].ToString();
            if (ddicno.Rows.Count > 0)
            {
                Applcn_no1.Text = ddicno.Rows[0]["stf_staff_no"].ToString();
              
                DataTable ddbind = new DataTable();
                ddbind = DBCon.Ora_Execute_table("select stf_staff_no,hr_gred_desc,hr_jaba_desc,stf_name,hr_jaw_desc,ho.org_name,o1.op_perg_name,str_curr_org_cd From hr_staff_profile as SP left join Ref_hr_jabatan as JB on JB.hr_jaba_Code=SP.stf_curr_dept_cd left join Ref_hr_Jawatan as JW on JW.hr_jaw_Code=SP.stf_curr_post_cd left join Ref_hr_gred as GR on GR.hr_gred_Code=SP.stf_curr_grade_cd left join hr_organization ho on ho.org_gen_id=sp.str_curr_org_cd left join hr_organization_pern o1 on o1.op_perg_code=stf_cur_sub_org where stf_staff_no='" + Applcn_no1.Text + "'");
                //txt_stfno.Text = ddbind.Rows[0]["stf_staff_no"].ToString();
                txt_org.Text = ddbind.Rows[0]["org_name"].ToString();
                txt_jabat.Text = ddbind.Rows[0]["hr_jaba_desc"].ToString();
                txt_gred.Text = ddbind.Rows[0]["hr_gred_desc"].ToString();
                txt_nama.Text = ddbind.Rows[0]["stf_name"].ToString();
                TextBox2.Text = ddicno.Rows[0]["stf_staff_no"].ToString();
                TextBox6.Text = ddbind.Rows[0]["stf_name"].ToString();
                txt_jawa.Text = ddbind.Rows[0]["hr_jaw_desc"].ToString();
                TextBox3.Text = ddbind.Rows[0]["op_perg_name"].ToString();
                get_id.Text = ddbind.Rows[0]["str_curr_org_cd"].ToString();
                //txt_sipr.Text = "";
                //txt_noru.Text = "";
                //txt_tadi.Text = "";
                //txt_tata.Text = "";
                grid();
            }
            else
            {                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {
            grid();            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Kakitangan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }



    

  

    protected void sel_inst(object sender, EventArgs e)
    {        
        grid();
    }

  

    protected void btn_cari_Click(object sender, EventArgs e)
    {
        if (Tahun_kew.SelectedValue != "" && DropDownList1.SelectedValue != "")
        {
            grid();
            
        }
        else
        {
            grid();            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    protected void gvEmp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label otd_dt = (Label)e.Row.FindControl("lbl1");
            
            System.Web.UI.WebControls.TextBox val2 = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("lbl2");            
            System.Web.UI.WebControls.TextBox val3 = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("lbl3");
            System.Web.UI.WebControls.TextBox val4 = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("lbl5");
            System.Web.UI.WebControls.TextBox val5 = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("lbl4");
            System.Web.UI.WebControls.Label val6 = (System.Web.UI.WebControls.Label)e.Row.FindControl("hrs_id");
            System.Web.UI.WebControls.Label val7 = (System.Web.UI.WebControls.Label)e.Row.FindControl("mins_id");

            CheckBox chkinst = (CheckBox)e.Row.FindControl("rbtnSelect2");

            DateTime ot_dt = DateTime.ParseExact(otd_dt.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DataTable get_otd_info = new DataTable();
            get_otd_info = DBCon.Ora_Execute_table("select * from hr_daily_ot where otd_staff_no='"+ Session["New"].ToString() + "' and otd_month='"+ DropDownList1.SelectedValue +"' and otd_year='"+ Tahun_kew.SelectedItem.Text +"' and otd_work_dt='"+ ot_dt.ToString("yyyy-MM-dd") + "'");

            
            if (get_otd_info.Rows.Count != 0)
            {                
                val2.Text = get_otd_info.Rows[0]["otd_time_start"].ToString();
                val3.Text = get_otd_info.Rows[0]["otd_time_end"].ToString();
                val4.Text = get_otd_info.Rows[0]["otd_desc"].ToString();
                val5.Text = get_otd_info.Rows[0]["otd_total_hour"].ToString();
                string[] words = val5.Text.Split('.');
                //chkinst.Checked = true;
                val6.Text = words[0].ToString();
                val7.Text = words[1].ToString();
                if (get_otd_info.Rows[0]["otd_klm"].ToString() == "1")
                {
                    rr1_chk.Checked = true;                    
                }
                else
                {
                    rr1_chk.Checked = false;                    
                }
                if (get_otd_info.Rows[0]["otd_repl_leave"].ToString() == "1")
                {                    
                    rr1_ch2.Checked = true;
                }
                else
                {
                    rr1_ch2.Checked = false;
                }
            }
            else
            {
                //chkinst.Checked = false;
            }
                       

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
          
        }
    }
    protected void grid()
    {
        //grid1();
        //ModalPopupExtender1.Show();
        string sqry = string.Empty;
        int days = DateTime.DaysInMonth( Int32.Parse(Tahun_kew.SelectedItem.Text), Int32.Parse(DropDownList1.SelectedValue));
        if (Tahun_kew.SelectedValue != "")
        {
            sqry = "DECLARE @start_date DATETIME = '"+ Tahun_kew.SelectedItem.Text + "-"+ DropDownList1.SelectedValue + "-01';  DECLARE @end_date DATETIME = '"+ Tahun_kew.SelectedItem.Text + "-"+ DropDownList1.SelectedValue + "-"+ days + "';"
                            + " WITH    AllDays AS ( SELECT   @start_date AS [Date], 1 AS [level] , '' val1,'' val2,'' val3,'' val4,'' val5,'' val6"
                            + " UNION ALL SELECT   DATEADD(DAY, 1, [Date]), [level] + 1 , '' val1,'' val2,'' val3,'' val4,'' val5,'' val6 FROM  AllDays  WHERE    [Date] < @end_date )"
                            + " SELECT FORMAT([Date],'dd/MM/yyyy', 'en-us') [date], [level] , '' val1,'' val2,'' val3,'' val4,'' val5,'' val6"
                            + " FROM   AllDays OPTION (MAXRECURSION 0)";
        }
        else
        {
            sqry = "Declare @Start datetime Declare @End datetime Select @Start = '00000000' Select @End = '00000000' ;With CTE as (Select @Start  as Date,Case When DatePart(mm,@Start)<>DatePart(mm,@Start+1) then 1 else 0 end as [Last] UNION ALL Select Date+1,Case When DatePart(mm,Date+1)<>DatePart(mm,Date+2) then 1 else 0 end from CTE Where Date<@End)Select FORMAT(ISNULL(Date,''),'dd/MM', 'en-us') as s1, '' s2,'' s3,'' s4,'' s5,'' s6 from CTE where [Last]=1   OPTION ( MAXRECURSION 0 )";
        }


        con.Open();
        DataTable ddicno = new DataTable();
        SqlCommand cmd = new SqlCommand(sqry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gv_refdata.DataSource = ds;
            gv_refdata.DataBind();
            int columncount = gv_refdata.Rows[0].Cells.Count;
            gv_refdata.Rows[0].Cells.Clear();
            gv_refdata.Rows[0].Cells.Add(new TableCell());
            gv_refdata.Rows[0].Cells[0].ColumnSpan = columncount;
            gv_refdata.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
        }
        else
        {
            int min_val = 1;
            string fmdate = string.Empty, tmdate = string.Empty, py_fdate = string.Empty, py_ldate = string.Empty, vv_amt1 = string.Empty;
            int curr_yr = Int32.Parse(Tahun_kew.SelectedValue);
            int prev_yr = (Int32.Parse(Tahun_kew.SelectedValue) - min_val);
            py_fdate = prev_yr + "-01-01";
            py_ldate = prev_yr + "-12-31";
            fmdate = Tahun_kew.SelectedValue + "-01-01";
            tmdate = Tahun_kew.SelectedValue + "-12-31";


            DataTable get_otd_info2 = new DataTable();
            get_otd_info2 = DBCon.Ora_Execute_table("select * from hr_daily_ot where otd_staff_no='" + Session["New"].ToString() + "' and otd_month='" + DropDownList1.SelectedValue + "' and otd_year='" + Tahun_kew.SelectedItem.Text + "'");
            gv_refdata.DataSource = ds;
            gv_refdata.DataBind();
            gv_refdata.FooterRow.Cells[4].Text = "JUMLAH JAM KLM :";
            gv_refdata.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            if (get_otd_info2.Rows.Count != 0)
            {
                btn_submit.Text = "Kemaskini";
                ((TextBox)gv_refdata.FooterRow.Cells[4].FindControl("lblTotal2")).Text = float.Parse(get_otd_info2.Rows[0]["otd_jumlah_hour"].ToString()).ToString();
               

            }
            else
            {
                btn_submit.Text = "Simpan";
            }
        }
        
        con.Close();
    }


    protected void grid1()
    {        
        string sqry1 = string.Empty;
        TextBox7.Text = Tahun_kew.SelectedItem.Text;
        TextBox8.Text = DropDownList1.SelectedItem.Text;
        DateTime firstOfNextMonth = new DateTime( Int32.Parse(TextBox7.Text),Int32.Parse(DropDownList1.SelectedValue), 1).AddMonths(1);
        DateTime lastOfThisMonth = firstOfNextMonth.AddDays(-1);
        DateTime lastOftwoMonth = lastOfThisMonth.AddMonths(2);
        TextBox1.Text = lastOfThisMonth.ToString("dd/MM/yyyy");
        TextBox4.Text = lastOftwoMonth.ToString("dd/MM/yyyy");
        if (Tahun_kew.SelectedValue != "")
        {
            sqry1 = "select *,Format(otd_work_dt,'dd/MM/yyyy') as tdt from hr_daily_ot"
                            + " where"
                            + " otd_staff_no='" + Session["New"].ToString() + "' and otd_month='" + DropDownList1.SelectedValue + "' and otd_year='" + Tahun_kew.SelectedItem.Text + "'";
                            
        }
        else
        {
            sqry1 = "Declare @Start datetime Declare @End datetime Select @Start = '00000000' Select @End = '00000000' ;With CTE as (Select @Start  as Date,Case When DatePart(mm,@Start)<>DatePart(mm,@Start+1) then 1 else 0 end as [Last] UNION ALL Select Date+1,Case When DatePart(mm,Date+1)<>DatePart(mm,Date+2) then 1 else 0 end from CTE Where Date<@End)Select FORMAT(ISNULL(Date,''),'dd/MM', 'en-us') as s1, '' s2,'' s3,'' s4 from CTE where [Last]=1   OPTION ( MAXRECURSION 0 )";
        }


        con.Open();
        DataTable ddicno = new DataTable();
        SqlCommand cmd = new SqlCommand(sqry1, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView1.DataSource = ds;
            GridView1.DataBind();
            int columncount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView1.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
        }
        else
        {           
            DataTable get_otd_info2 = new DataTable();
            get_otd_info2 = DBCon.Ora_Execute_table("select * from hr_daily_ot where otd_staff_no='" + Session["New"].ToString() + "' and otd_month='" + DropDownList1.SelectedValue + "' and otd_year='" + Tahun_kew.SelectedItem.Text + "'");
            GridView1.DataSource = ds;
            GridView1.DataBind();
            GridView1.FooterRow.Cells[4].Text = "JUMLAH JAM KLM :";
            GridView1.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            if (get_otd_info2.Rows.Count != 0)
            {
                btn_submit.Text = "Kemaskini";
                ((Label)GridView1.FooterRow.Cells[4].FindControl("lblTotal12")).Text = float.Parse(get_otd_info2.Rows[0]["otd_jumlah_hour"].ToString()).ToString();
                double n = (double.Parse(get_otd_info2.Rows[0]["otd_jumlah_hour"].ToString()) / 8);
                double d = Math.Floor(n);
                double v = n - d;
                double h = 0;
                if (v < 0.5) { h = 0; }
                if (v >= 0.5) { h = 0.5; }
                double t = d + h;
                hari_cuti.Text = Convert.ToString(t);
            }
            else
            {
                btn_submit.Text = "Simpan";
            }
        }

        con.Close();
    }

    protected void clk_submit(object sender, EventArgs e)
    {
        if (TextBox1.Text != "" && TextBox4.Text != "")
        {
            DateTime st_dt = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime ed_dt = DateTime.ParseExact(TextBox4.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DataTable select_opleave = new DataTable();
            select_opleave = DBCon.Ora_Execute_table("select * from hr_com_leave where col_org_id= '" + get_id.Text + "' and col_staff_no='" + TextBox2.Text + "' and col_start_dt='" + st_dt.ToString("yyyy-MM-dd") + "' and col_end_dt='" + ed_dt.ToString("yyyy-MM-dd") + "' and col_cat_cd='12'");
            if (select_opleave.Rows.Count == 0)
            {                
                string Inssql = "Insert into hr_com_leave "
                                       + " (col_org_id,col_staff_no,col_start_dt,col_end_dt,col_cat_cd,col_status_cd,col_entitle_day,col_taken_day ,col_balance_day,col_crt_id,col_crt_dt,col_year)"
                                       + " Values ('"+ get_id.Text + "','" + TextBox2.Text + "','" + st_dt.ToString("yyyy-MM-dd") + "','" + ed_dt.ToString("yyyy-MM-dd") + "','12','A'"
                                       + " ,'" + hari_cuti.Text + "','0','" + hari_cuti.Text + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss") + "','" + Tahun_kew.SelectedItem.Text + "')";
                Status = DBCon.Ora_Execute_CommamdText(Inssql);
            }
            else
            {
                string Inssql = "Update hr_com_leave set "
                                      + " col_start_dt='" + st_dt.ToString("yyyy-MM-dd") + "',col_end_dt='" + ed_dt.ToString("yyyy-MM-dd") + "',col_entitle_day='" + hari_cuti.Text + "',col_taken_day='0' ,col_balance_day='" + hari_cuti.Text + "',col_upd_id='" + Session["New"].ToString() + "',col_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',col_year='" + Tahun_kew.SelectedItem.Text + "'"
                                      + " where col_org_id= '" + get_id.Text + "' and col_staff_no='" + TextBox2.Text + "' and col_start_dt='" + st_dt.ToString("yyyy-MM-dd") + "' and col_end_dt='" + ed_dt.ToString("yyyy-MM-dd") + "' and col_cat_cd='12'";
                Status = DBCon.Ora_Execute_CommamdText(Inssql);
            }
            if(Status == "SUCCESS")
            {
                btn_submit.Text = "Kemaskini";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
           
        }
        else
        {
            ModalPopupExtender1.Show();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }
    protected void btn_simp_Click(object sender, EventArgs e)
    {        
        if (Tahun_kew.SelectedValue != "" && DropDownList1.SelectedValue != "")
        {
            string rcount = string.Empty, jen_ot = string.Empty, jen_ot_3 = string.Empty, jen_ot1 = string.Empty, jen_ot2 = string.Empty;
            int count = 0;
            foreach (GridViewRow gvrow in gv_refdata.Rows)
            {
                var rb = gvrow.FindControl("rbtnSelect2") as System.Web.UI.WebControls.CheckBox;
                if (rb.Checked)
                {
                    count++;
                }
                rcount = count.ToString();
            }
            if (rcount != "0")
            {
                foreach (GridViewRow gvrow in gv_refdata.Rows)
                {
                    CheckBox chkinst = (CheckBox)gvrow.FindControl("rbtnSelect2");
                 
                    if (chkinst.Checked)
                    {                        
                        if(rr1_chk.Checked == true)
                        {
                            jen_ot = "01";                            
                        }
                        else if (rr1_ch2.Checked == true)
                        {
                            jen_ot = "02";
                        }
                        else
                        {
                            jen_ot = "";                            
                        }

                        if (rr1_chk.Checked == true)
                        {
                            jen_ot1 = "1";
                            jen_ot2 = "0";
                        }
                        else if (rr1_ch2.Checked == true)
                        {
                            jen_ot1 = "0";
                            jen_ot2 = "1";
                        }
                        else
                        {
                            jen_ot1 = "0";
                            jen_ot2 = "0";
                        }

                                                
                        string val1 = ((Label)gvrow.FindControl("lbl1")).Text.ToString();
                        DateTime ot_dt = DateTime.ParseExact(val1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        string val2 = ((TextBox)gvrow.FindControl("lbl2")).Text.ToString();
                        string val3 = ((TextBox)gvrow.FindControl("lbl3")).Text.ToString();
                        string val4 = ((TextBox)gvrow.FindControl("lbl5")).Text.ToString();
                        string val5 = ((TextBox)gvrow.FindControl("lbl4")).Text.ToString();
                        string val6 = ((TextBox)gv_refdata.FooterRow.FindControl("lblTotal2")).Text.ToString();
                        DataTable get_otd_info2 = new DataTable();
                        get_otd_info2 = DBCon.Ora_Execute_table("select * from hr_daily_ot where otd_staff_no='" + Session["New"].ToString() + "' and otd_month='" + DropDownList1.SelectedValue + "' and otd_year='" + Tahun_kew.SelectedItem.Text + "' and otd_work_dt='"+ ot_dt.ToString("yyyy-MM-dd") + "'");
                        if (get_otd_info2.Rows.Count == 0)
                        {
                            string Inssql = "Insert into hr_daily_ot "
                                            + " (otd_staff_no,otd_month,otd_year,otd_work_dt,otd_ot_type_cd,otd_time_start,otd_time_end,otd_total_hour,otd_klm,otd_repl_leave,otd_status_cd,otd_desc,otd_crt_id,otd_crt_dt,otd_jumlah_hour)"
                                            + " Values ('" + Session["New"].ToString() + "','" + DropDownList1.SelectedValue + "','" + Tahun_kew.SelectedItem.Text + "','" + ot_dt.ToString("yyyy-MM-dd") + "','" + jen_ot + "'"
                                            + " ,'" + val2 + "','" + val3 + "','" + val5 + "','" + jen_ot1 + "','" + jen_ot2 + "','A','" + val4 + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + val6 + "')";
                            Status = DBCon.Ora_Execute_CommamdText(Inssql);
                        }
                        else
                        {
                            string Inssql = "Update hr_daily_ot set"
                                            + " otd_time_start='" + val2 + "',otd_time_end='" + val3 + "',otd_total_hour='" + val5 + "',otd_desc='" + val4 + "',otd_upd_id='" + Session["New"].ToString() + "',otd_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',otd_jumlah_hour='" + val6 + "'"
                                            + " where otd_staff_no='" + Session["New"].ToString() + "' and otd_month='" + DropDownList1.SelectedValue + "' and otd_year='" + Tahun_kew.SelectedItem.Text + "' and otd_work_dt='" + ot_dt.ToString("yyyy-MM-dd") + "'";
                            Status = DBCon.Ora_Execute_CommamdText(Inssql);
                        }

                    }
                }
                if (Status == "SUCCESS")
                {
                  
                    btn_submit.Text = "Kemaskini";
                    if (rr1_ch2.Checked == true)
                    {
                        grid1();
                        ModalPopupExtender1.Show();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    }
                    
                }
                else
                {
                    btn_submit.Text = "Simpan";
                }                
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila pilih Rekod.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
 
  
       
    }


    protected void ctk_values(object sender, EventArgs e)
    {
        string rcount = string.Empty;
        int count1 = 0;
        
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        dt = DBCon.Ora_Execute_table("select *,s1.stf_name as nama,Format(otd_work_dt,'dd/MM/yyyy') as tdt from hr_daily_ot left join hr_staff_profile s1 on s1.stf_staff_no=otd_staff_no where otd_staff_no='" + Session["New"].ToString() + "' and otd_month='" + DropDownList1.SelectedValue + "' and otd_year='" + Tahun_kew.SelectedItem.Text + "'");
        RptviwerStudent.Reset();
        ds.Tables.Add(dt);

        List<DataRow> listResult = dt.AsEnumerable().ToList();
        listResult.Count();
        int countRow = 0;
        countRow = listResult.Count();

        RptviwerStudent.LocalReport.DataSources.Clear();
        if (countRow != 0)
        {
            if (sel_frmt.SelectedValue == "01")
            {
                RptviwerStudent.LocalReport.ReportPath = "SUMBER_MANUSIA/jadual_klm.rdlc";
                ReportDataSource rds = new ReportDataSource("jadualklm", dt);
                ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("s1",Tahun_kew.SelectedItem.Text ),
                     new ReportParameter("s2",DropDownList1.SelectedItem.Text )

                     };
                RptviwerStudent.LocalReport.SetParameters(rptParams);
                RptviwerStudent.LocalReport.DataSources.Add(rds);
                RptviwerStudent.LocalReport.Refresh();

                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;
                string filename;

                filename = string.Format("{0}.{1}", "Jadual_KLM_" + DateTime.Now.ToString("ddMMyyyy") + "", "pdf");
                byte[] bytes = RptviwerStudent.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=" + filename);
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }
            else if (sel_frmt.SelectedValue == "02")
            {
                System.Text.StringBuilder builder = new StringBuilder();
                string strFileName = string.Format("{0}.{1}", "Jadual_KLM_" + DateTime.Now.ToString("ddMMyyyy") + "", "csv");
                builder.Append("No Kakitangan ,Nama Kakitangan, Tarikh, Masa Mula, Masa Akhir, Keterangan, Jumlah Jam" + Environment.NewLine);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    builder.Append(dt.Rows[i]["otd_staff_no"].ToString() + "," + txt_nama.Text + "," + dt.Rows[i]["tdt"].ToString() + "," + dt.Rows[i]["otd_time_start"].ToString() + "," + dt.Rows[i]["otd_time_end"].ToString() + "," + dt.Rows[i]["otd_desc"].ToString() + "," + dt.Rows[i]["otd_total_hour"].ToString() + Environment.NewLine);
                }
                Response.Clear();
                Response.ContentType = "text/csv";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + strFileName);
                Response.Write(builder.ToString());
                Response.End();
            }
          
        }
        else if (countRow == 0)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod tidak dijumpai. Sila Pastikan Semua Maklumat Dimasukkan Dengan Betul.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }

        grid();

    }
    void txt_clr()
    {
      
    }

    protected void sel_tahun(object sender, EventArgs e)
    {
        grid();
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        Response.Redirect("../SUMBER_MANUSIA/HR_KLM.aspx");
    }

  
}