using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Text;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.Globalization;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Data.Odbc;

public partial class kw_lep_pl : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    private static int PageSize = 20;
    string qry1 = string.Empty, qry2 = string.Empty, sqry1 = string.Empty, sqry2 = string.Empty, fyear = string.Empty;
    string level;
    string Status = string.Empty;
    string userid;
    string CommandArgument1 = string.Empty, CommandArgument2 = string.Empty, CommandArgument3 = string.Empty;
    string rcount = string.Empty, rcount1 = string.Empty;
    int count = 0, count1 = 1, pyr = 0, prdt = 0;
    string ss1 = string.Empty;
    string selectedValues = string.Empty;
    string fmdate = string.Empty, tmdate = string.Empty, tmdate1 = string.Empty, sqry = string.Empty, py_fdate = string.Empty, py_ldate = string.Empty, curr_yr = string.Empty, prev_yr = string.Empty;
    string var_fmdate = string.Empty, var_tmdate = string.Empty, var_tmdate1 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        Button4.OnClientClick = @"if(this.value == 'Please wait...')
           return false;
           this.value = 'Please wait...';this.disabled=true";
        string script = " $(document).ready(function () { $(" + chk_lst.ClientID + ").SumoSelect({ selectAll: true }); $('.select2').select2();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        //ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        //scriptManager.RegisterPostBackControl(this.Button4);

        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
                bind_kod_akaun();
                project();
                bind_acoa();
                chk_var3.Checked = true;
             
                //BindDropdown();

            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }


    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        GridView1.DataBind();
    }
    void app_language()

    {
        if (Session["New"] != null)
        {
            DataTable ste_set = new DataTable();
            ste_set = DBCon.Ora_Execute_table("select * from site_settings where ID IN ('1')");

            DataTable gt_lng = new DataTable();
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('828','705','1724','65','502','169','1841','121','15','64')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());          
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            //ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
           
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    void project()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select Ref_Projek_code,Ref_Projek_name from  KW_Ref_Projek";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddpro.DataSource = dt;
            ddpro.DataTextField = "Ref_Projek_name";
            ddpro.DataValueField = "Ref_Projek_code";
            ddpro.DataBind();
            ddpro.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void bind_kod_akaun()
    {

        string get_qry = string.Empty;

       
            get_qry = "select kod_akaun,(kod_akaun +' | '+ upper(nama_akaun)) as name from KW_Ref_Carta_Akaun where jenis_akaun_type='2' and Status='A' order by kod_akaun asc";
        
        DataSet ds = new DataSet();

        string cmdstr = get_qry;

        SqlDataAdapter adp = new SqlDataAdapter(cmdstr, con);

        adp.Fill(ds);



        if (ds.Tables[0].Rows.Count > 0)

        {

            chk_lst.DataSource = ds.Tables[0];

            chk_lst.DataTextField = "name";

            chk_lst.DataValueField = "kod_akaun";

            chk_lst.DataBind();

        }

    }
    void bind_acoa()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "SELECT ISNULL([sts_kawalan],'') sts FROM [KW_Ref_Carta_Akaun] WHERE [Status]='A' and ISNULL([sts_kawalan],'') !=''";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            akaun_level.DataSource = dt;
            akaun_level.DataTextField = "sts";
            akaun_level.DataValueField = "sts";
            akaun_level.DataBind();
            akaun_level.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

     protected void chk_variance(object sender, EventArgs e)
    {
        if(chk_var1.Checked== true)
        {
            chk_variance1.Visible = true;
        }
        else
        {
            chk_variance1.Visible = false;
        }
        TextBox1.Text = "";
        TextBox2.Text = "";
        bind_data();
    }

    void get_value()
    {
       
        foreach (ListItem li in chk_lst.Items)
        {
            if (li.Selected == true)
            {
                count++;
            }
            rcount = count.ToString();
        }
      
        foreach (ListItem li in chk_lst.Items)
        {
            if (li.Selected == true)
            {
                if (Int32.Parse(rcount) > count1)
                {
                    ss1 = ",";
                }
                else
                {
                    ss1 = "";
                }

                selectedValues += li.Value + ss1;

                count1++;
            }
            rcount1 = count1.ToString();
        }
        if (chk_var1.Checked == true)
        {
            if (selectedValues != "" && ddpro.SelectedValue != "")
            {
                qry2 = "a1.kod_akaun IN ('" + selectedValues.Replace(",", "','") + "') and";
            }
            else if (selectedValues != "" && ddpro.SelectedValue == "")
            {
                qry2 = "a1.kod_akaun IN ('" + selectedValues.Replace(",", "','") + "') and";
            }
            else if (selectedValues == "" && ddpro.SelectedValue != "")
            {
                qry2 = "";
            }
            else
            {
                qry2 = "";
            }
        }
        else
        {
            if (selectedValues != "" && ddpro.SelectedValue != "")
            {
                qry2 = "a1.kod_akaun IN ('" + selectedValues.Replace(",", "','") + "') and";
            }
            else if (selectedValues != "" && ddpro.SelectedValue == "")
            {
                qry2 = "a1.kod_akaun IN ('" + selectedValues.Replace(",", "','") + "') and";
            }
            else if (selectedValues == "" && ddpro.SelectedValue != "")
            {
                qry2 = "";
            }
            else
            {
                qry2 = "";
            }
        }
    }

    void bind_details()
    {

        DateTime fd, td;
        if (Tk_mula.Text != "")
        {
            string fdate = Tk_mula.Text;
            fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            fmdate = fd.ToString("yyyy-MM-dd");
            curr_yr = fd.ToString("yyyy");

        }
        if (Tk_akhir.Text != "")
        {
            string tdate = Tk_akhir.Text;
            td = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            tmdate = td.ToString("yyyy-MM-dd");
            curr_yr = td.ToString("yyyy");

            tmdate1 = td.ToString("dd MMMM yyyy");
        }
        get_value();

        py_fdate = fmdate;
        py_ldate = tmdate;
        if (chk_var1.Checked == true)
        {
            qry1 = " select * from (select c.ref_pnl_jenis sno,c.ref_pnl_nama kat,b.ref_pnl_nama hdr,a.tmp_kod_akaun,coa.nama_akaun,case when d.AMT1 < '0.00' then replace(d.AMT1,'-','') else ((-1) * d.AMT1) end as cur_jum,case when e.AMT1 < '0.00' then replace(e.AMT1,'-','') else ((-1) * e.AMT1) end as pre_jum from kw_template_pl as a  "
                    + " outer apply(select * from KW_Ref_pnl_item s1 where s1.ref_pnl_nama= a.ref_alrn_nama ) as b  "
                    + " outer apply(select * from KW_Ref_pnl_item s2 where s2.Id= b.under_parent) as c  "
                    + " outer apply (SELECT ISNULL(SUM(ISNULL(KW_Debit_amt,'0.00') - ISNULL(KW_kredit_amt,'0.00')) ,'0.00') AS AMT1 FROM KW_General_Ledger where kod_akaun=a.tmp_kod_akaun "
                    + " and GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1)) as d "
                    + " outer apply (SELECT ISNULL(SUM(ISNULL(KW_Debit_amt,'0.00') - ISNULL(KW_kredit_amt,'0.00')) ,'0.00') AS AMT1 FROM KW_General_Ledger where kod_akaun=a.tmp_kod_akaun "
                    + " and GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + var_fmdate + "'), 0) and GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + var_tmdate + "'), +1)) as e "
                    + " inner join KW_Ref_Carta_Akaun coa on coa.kod_akaun=a.tmp_kod_akaun and coa.Status='A' where a.tmp_Status='Y' and c.ref_pnl_jenis='R'  "
                    + " union all "
                    + " select c.ref_pnl_jenis sno,c.ref_pnl_nama kat,b.ref_pnl_nama hdr,a.tmp_kod_akaun,coa.nama_akaun,(ISNULL(opn1.opn_amt1,'0.00') + d.AMT1) cur_jum,(ISNULL(opn2.opn_amt1,'0.00') + e.AMT1) pre_jum from kw_template_pl as a  "
                    + " outer apply(select * from KW_Ref_pnl_item s1 where s1.ref_pnl_nama= a.ref_alrn_nama ) as b  "
                    + " outer apply(select * from KW_Ref_pnl_item s2 where s2.Id= b.under_parent) as c  "
                    + " outer apply (select ISNULL(case when opn_kredit_amt ='0.00' then opn_debit_amt else opn_kredit_amt end,'0.00') as opn_amt1 from KW_Opening_Balance where a.tmp_kod_akaun=kod_akaun and Status='A' and set_sts='1' and opening_year='" + curr_yr + "') as opn1 "
                    + " outer apply (SELECT ISNULL(SUM(ISNULL(KW_Debit_amt,'0.00') - ISNULL(KW_kredit_amt,'0.00')) ,'0.00') AS AMT1 FROM KW_General_Ledger where kod_akaun=a.tmp_kod_akaun "
                    + " and GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1)) as d "
                    + " outer apply (select ISNULL(case when opn_kredit_amt ='0.00' then opn_debit_amt else opn_kredit_amt end ,'0.00') as opn_amt1 from KW_Opening_Balance where a.tmp_kod_akaun=kod_akaun and Status='A' and set_sts='1' and opening_year='" + prev_yr + "') as opn2 "
                    + " outer apply (SELECT ISNULL(SUM(ISNULL(KW_Debit_amt,'0.00') - ISNULL(KW_kredit_amt,'0.00')) ,'0.00') AS AMT1 FROM KW_General_Ledger where kod_akaun=a.tmp_kod_akaun "
                    + " and GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + var_fmdate + "'), 0) and GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + var_tmdate + "'), +1)) as e "
                    + " inner join KW_Ref_Carta_Akaun coa on coa.kod_akaun=a.tmp_kod_akaun and coa.Status='A' where a.tmp_Status='Y' and c.ref_pnl_jenis='N')  as a1 outer apply( "

                    + " select sum(curamt)curamt, sum(preamt) preamt  from(select sum(cur_jum) curamt, sum(pre_jum) preamt from( "
                    + " select case when d.AMT1 < '0.00' then replace(d.AMT1, '-', '') else ((-1) * d.AMT1) end as cur_jum,case when e.AMT1 < '0.00' then replace(e.AMT1, '-', '') else ((-1) * e.AMT1) end as pre_jum from kw_template_pl as a "
                    + " outer apply(select * from KW_Ref_pnl_item s1 where s1.ref_pnl_nama = a.ref_alrn_nama ) as b "
                    + " outer apply(select * from KW_Ref_pnl_item s2 where s2.Id = b.under_parent) as c "
                    + " outer apply(SELECT ISNULL(SUM(ISNULL(KW_Debit_amt, '0.00') - ISNULL(KW_kredit_amt, '0.00')), '0.00') AS AMT1 FROM KW_General_Ledger where kod_akaun = a.tmp_kod_akaun "
                    + " and GL_post_dt >= DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1)) as d "
                    + " outer apply(SELECT ISNULL(SUM(ISNULL(KW_Debit_amt, '0.00') - ISNULL(KW_kredit_amt, '0.00')), '0.00') AS AMT1 FROM KW_General_Ledger where kod_akaun = a.tmp_kod_akaun "
                    + " and GL_post_dt >= DATEADD(day, DATEDIFF(day, 0, '" + var_fmdate + "'), 0) and GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + var_tmdate + "'), +1)) as e "
                    + " inner join KW_Ref_Carta_Akaun coa on coa.kod_akaun = a.tmp_kod_akaun and coa.Status = 'A' "
                    + " where a.tmp_Status = 'Y' and c.ref_pnl_jenis = 'R') as a union all "
                    + " select sum(cur_jum) curamt, sum(pre_jum) preamt from(select(ISNULL(opn1.opn_amt1, '0.00') + d.AMT1) cur_jum, (ISNULL(opn2.opn_amt1, '0.00') + e.AMT1) pre_jum from kw_template_pl as a "
                    + " outer apply(select * from KW_Ref_pnl_item s1 where s1.ref_pnl_nama = a.ref_alrn_nama ) as b outer apply(select * from KW_Ref_pnl_item s2 where s2.Id = b.under_parent) as c "
                    + " outer apply(select ISNULL(case when opn_kredit_amt = '0.00' then opn_debit_amt else opn_kredit_amt end, '0.00') as opn_amt1 from KW_Opening_Balance where a.tmp_kod_akaun = kod_akaun and Status = 'A' and set_sts = '1' and opening_year = '" + curr_yr + "') as opn1 "
                    + " outer apply(SELECT ISNULL(SUM(ISNULL(KW_Debit_amt, '0.00') - ISNULL(KW_kredit_amt, '0.00')), '0.00') AS AMT1 FROM KW_General_Ledger where kod_akaun = a.tmp_kod_akaun "
                    + " and GL_post_dt >= DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1)) as d "
                    + " outer apply(select ISNULL(case when opn_kredit_amt = '0.00' then opn_debit_amt else opn_kredit_amt end, '0.00') as opn_amt1 from KW_Opening_Balance where a.tmp_kod_akaun = kod_akaun and Status = 'A' and set_sts = '1' and opening_year = '" + prev_yr + "') as opn2 "
                    + " outer apply(SELECT ISNULL(SUM(ISNULL(KW_Debit_amt, '0.00') - ISNULL(KW_kredit_amt, '0.00')), '0.00') AS AMT1 FROM KW_General_Ledger where kod_akaun = a.tmp_kod_akaun "
                    + " and GL_post_dt >= DATEADD(day, DATEDIFF(day, 0, '" + var_fmdate + "'), 0) and GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + var_tmdate + "'), +1)) as e "
                    + " inner join KW_Ref_Carta_Akaun coa on coa.kod_akaun = a.tmp_kod_akaun and coa.Status = 'A' "
                    + " where a.tmp_Status = 'Y' and c.ref_pnl_jenis = 'N' "
                    + " ) as a) as a ) as b";


        }
        else if (chk_var3.Checked == true)
        {
            if (TextBox1.Text != "")
            {
                string var_fdate = TextBox1.Text;
                DateTime var_fd = DateTime.ParseExact(var_fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var_fmdate = var_fd.ToString("yyyy-MM-dd");
                prev_yr = var_fd.ToString("yyyy");
            }
            if (TextBox2.Text != "")
            {
                string var_tdate = TextBox2.Text;
                DateTime var_td = DateTime.ParseExact(var_tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var_tmdate = var_td.ToString("yyyy-MM-dd");

            }
            qry1 = " select * from (select c.ref_pnl_jenis sno,c.ref_pnl_nama kat,b.ref_pnl_nama hdr,a.tmp_kod_akaun,coa.nama_akaun,case when d.AMT1 < '0.00' then replace(d.AMT1,'-','') else ((-1) * d.AMT1) end as cur_jum,case when e.AMT1 < '0.00' then replace(e.AMT1,'-','') else ((-1) * e.AMT1) end as pre_jum from kw_template_pl as a  "
                     + " outer apply(select * from KW_Ref_pnl_item s1 where s1.ref_pnl_nama= a.ref_alrn_nama ) as b  "
                     + " outer apply(select * from KW_Ref_pnl_item s2 where s2.Id= b.under_parent) as c  "
                     + " outer apply (SELECT ISNULL(SUM(ISNULL(KW_Debit_amt,'0.00') - ISNULL(KW_kredit_amt,'0.00')) ,'0.00') AS AMT1 FROM KW_General_Ledger where kod_akaun=a.tmp_kod_akaun "
                     + " and GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1)) as d "
                     + " outer apply (SELECT ISNULL(SUM(ISNULL(KW_Debit_amt,'0.00') - ISNULL(KW_kredit_amt,'0.00')) ,'0.00') AS AMT1 FROM KW_General_Ledger where kod_akaun=a.tmp_kod_akaun "
                     + " and GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + var_fmdate + "'), 0) and GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + var_tmdate + "'), +1)) as e "
                     + " inner join KW_Ref_Carta_Akaun coa on coa.kod_akaun=a.tmp_kod_akaun and coa.Status='A' where a.tmp_Status='Y' and c.ref_pnl_jenis='R'  "
                     + " union all "
                     + " select c.ref_pnl_jenis sno,c.ref_pnl_nama kat,b.ref_pnl_nama hdr,a.tmp_kod_akaun,coa.nama_akaun,(ISNULL(opn1.opn_amt1,'0.00') + d.AMT1) cur_jum,(ISNULL(opn2.opn_amt1,'0.00') + e.AMT1) pre_jum from kw_template_pl as a  "
                     + " outer apply(select * from KW_Ref_pnl_item s1 where s1.ref_pnl_nama= a.ref_alrn_nama ) as b  "
                     + " outer apply(select * from KW_Ref_pnl_item s2 where s2.Id= b.under_parent) as c  "
                     + " outer apply (select ISNULL(case when opn_kredit_amt ='0.00' then opn_debit_amt else opn_kredit_amt end,'0.00') as opn_amt1 from KW_Opening_Balance where a.tmp_kod_akaun=kod_akaun and Status='A' and set_sts='1' and opening_year='" + curr_yr + "') as opn1 "
                     + " outer apply (SELECT ISNULL(SUM(ISNULL(KW_Debit_amt,'0.00') - ISNULL(KW_kredit_amt,'0.00')) ,'0.00') AS AMT1 FROM KW_General_Ledger where kod_akaun=a.tmp_kod_akaun "
                     + " and GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1)) as d "
                     + " outer apply (select ISNULL(case when opn_kredit_amt ='0.00' then opn_debit_amt else opn_kredit_amt end ,'0.00') as opn_amt1 from KW_Opening_Balance where a.tmp_kod_akaun=kod_akaun and Status='A' and set_sts='1' and opening_year='" + prev_yr + "') as opn2 "
                     + " outer apply (SELECT ISNULL(SUM(ISNULL(KW_Debit_amt,'0.00') - ISNULL(KW_kredit_amt,'0.00')) ,'0.00') AS AMT1 FROM KW_General_Ledger where kod_akaun=a.tmp_kod_akaun "
                     + " and GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + var_fmdate + "'), 0) and GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + var_tmdate + "'), +1)) as e "
                     + " inner join KW_Ref_Carta_Akaun coa on coa.kod_akaun=a.tmp_kod_akaun and coa.Status='A' where a.tmp_Status='Y' and c.ref_pnl_jenis='N')  as a1 outer apply( "

                     + " select sum(curamt)curamt, sum(preamt) preamt  from(select sum(cur_jum) curamt, sum(pre_jum) preamt from( "
                     + " select case when d.AMT1 < '0.00' then replace(d.AMT1, '-', '') else ((-1) * d.AMT1) end as cur_jum,case when e.AMT1 < '0.00' then replace(e.AMT1, '-', '') else ((-1) * e.AMT1) end as pre_jum from kw_template_pl as a "
                     + " outer apply(select * from KW_Ref_pnl_item s1 where s1.ref_pnl_nama = a.ref_alrn_nama ) as b "
                     + " outer apply(select * from KW_Ref_pnl_item s2 where s2.Id = b.under_parent) as c "
                     + " outer apply(SELECT ISNULL(SUM(ISNULL(KW_Debit_amt, '0.00') - ISNULL(KW_kredit_amt, '0.00')), '0.00') AS AMT1 FROM KW_General_Ledger where kod_akaun = a.tmp_kod_akaun "
                     + " and GL_post_dt >= DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1)) as d "
                     + " outer apply(SELECT ISNULL(SUM(ISNULL(KW_Debit_amt, '0.00') - ISNULL(KW_kredit_amt, '0.00')), '0.00') AS AMT1 FROM KW_General_Ledger where kod_akaun = a.tmp_kod_akaun "
                     + " and GL_post_dt >= DATEADD(day, DATEDIFF(day, 0, '" + var_fmdate + "'), 0) and GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + var_tmdate + "'), +1)) as e "
                     + " inner join KW_Ref_Carta_Akaun coa on coa.kod_akaun = a.tmp_kod_akaun and coa.Status = 'A' "
                     + " where a.tmp_Status = 'Y' and c.ref_pnl_jenis = 'R') as a union all "
                     + " select sum(cur_jum) curamt, sum(pre_jum) preamt from(select(ISNULL(opn1.opn_amt1, '0.00') + d.AMT1) cur_jum, (ISNULL(opn2.opn_amt1, '0.00') + e.AMT1) pre_jum from kw_template_pl as a "
                     + " outer apply(select * from KW_Ref_pnl_item s1 where s1.ref_pnl_nama = a.ref_alrn_nama ) as b outer apply(select * from KW_Ref_pnl_item s2 where s2.Id = b.under_parent) as c "
                     + " outer apply(select ISNULL(case when opn_kredit_amt = '0.00' then opn_debit_amt else opn_kredit_amt end, '0.00') as opn_amt1 from KW_Opening_Balance where a.tmp_kod_akaun = kod_akaun and Status = 'A' and set_sts = '1' and opening_year = '" + curr_yr + "') as opn1 "
                     + " outer apply(SELECT ISNULL(SUM(ISNULL(KW_Debit_amt, '0.00') - ISNULL(KW_kredit_amt, '0.00')), '0.00') AS AMT1 FROM KW_General_Ledger where kod_akaun = a.tmp_kod_akaun "
                     + " and GL_post_dt >= DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1)) as d "
                     + " outer apply(select ISNULL(case when opn_kredit_amt = '0.00' then opn_debit_amt else opn_kredit_amt end, '0.00') as opn_amt1 from KW_Opening_Balance where a.tmp_kod_akaun = kod_akaun and Status = 'A' and set_sts = '1' and opening_year = '" + prev_yr + "') as opn2 "
                     + " outer apply(SELECT ISNULL(SUM(ISNULL(KW_Debit_amt, '0.00') - ISNULL(KW_kredit_amt, '0.00')), '0.00') AS AMT1 FROM KW_General_Ledger where kod_akaun = a.tmp_kod_akaun "
                     + " and GL_post_dt >= DATEADD(day, DATEDIFF(day, 0, '" + var_fmdate + "'), 0) and GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + var_tmdate + "'), +1)) as e "
                     + " inner join KW_Ref_Carta_Akaun coa on coa.kod_akaun = a.tmp_kod_akaun and coa.Status = 'A' "
                     + " where a.tmp_Status = 'Y' and c.ref_pnl_jenis = 'N' "
                     + " ) as a) as a ) as b";
        }
        
    }

    protected void clk_srch(object sender, EventArgs e)
    {
        if (Tk_mula.Text != "" && Tk_akhir.Text != "")
        {
            bind_data();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Tarikh Mula and Tarikh Akhir.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }
    void bind_data()
    {
        bind_details();
        using (SqlConnection con = new SqlConnection(conString))
        {
            using (SqlCommand cmd = new SqlCommand(qry1))
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


                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                            GridView1.DataSource = ds;
                            GridView1.DataBind();
                            int columncount = GridView1.Rows[0].Cells.Count;
                            GridView1.Rows[0].Cells.Clear();
                            GridView1.Rows[0].Cells.Add(new TableCell());
                            GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
                            GridView1.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
                            Button2.Visible = false;
                            Button3.Visible = false;

                           GridView1.FooterRow.Cells[3].Text = "<strong>JUMLAH BERSIH (RM)</strong>";
                            GridView1.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                            GridView1.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                            GridView1.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                            GridView1.FooterRow.Cells[4].Text = "0.00";
                            GridView1.FooterRow.Cells[5].Text = "0.00";
                        }
                        else
                        {
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            Button2.Visible = true;
                            Button3.Visible = true;
                           
                            GridViewHelper helper = new GridViewHelper(this.GridView1);
                            helper.RegisterGroup("sno", true, true);
                            helper.RegisterGroup("kat", true, true);
                            helper.RegisterGroup("hdr", true, true);
                            helper.GroupHeader += new GroupEvent(helper_GroupHeader);
                            helper.RegisterSummary("cur_jum", SummaryOperation.Sum, "sno");
                            helper.RegisterSummary("pre_jum", SummaryOperation.Sum, "sno");
                            helper.GroupSummary += new GroupEvent(helper_GroupSummary);
                            helper.ApplyGroupSort();
                            double debit = double.Parse(dt.Rows[0]["curamt"].ToString());
                            double kredit = double.Parse(dt.Rows[0]["preamt"].ToString());
                            GridView1.FooterRow.Cells[3].Text = "<strong>JUMLAH BERSIH (RM)</strong>";
                            GridView1.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                            GridView1.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                            GridView1.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;

                            GridView1.FooterRow.Cells[4].Text = debit.ToString("C").Replace("$", "").Replace("RM", "");
                            GridView1.FooterRow.Cells[5].Text = kredit.ToString("C").Replace("$", "").Replace("RM", "");
                        }

                    }
                }
            }
         
        }
    }

    private void helper_GroupSummary(string groupName, object[] values, GridViewRow row)
    {
        if (values[0].ToString() == "R")
        {
            row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[0].Text = "Untung/Rugi Sebelum Cukai";
        }
        else
        {
            row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[0].Text = "Untung/Rugi Bersih";
        }
    }
    private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
    {
        if (groupName == "sno")
        {
            row.Attributes.Add("style","display:None;");
        }
        if (groupName == "kat")
        {
            row.BackColor = Color.LightGray;
            row.ForeColor = Color.DarkRed;
            row.Font.Bold = true;
            row.Cells[0].Text = "&nbsp;&nbsp;" + row.Cells[0].Text;
        }
        if (groupName == "hdr")
        {
            row.BackColor = Color.FromArgb(236, 236, 236);
            row.ForeColor = Color.DarkBlue;
            row.Font.Bold = true;
            row.Cells[0].Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + row.Cells[0].Text;
        }
    }
    protected void clk_submit(object sender, EventArgs e)
    {
        if (Tk_mula.Text != "" && Tk_akhir.Text != "")
        {
            string val1 = string.Empty, val2 = string.Empty, val3 = string.Empty, val4 = string.Empty, val5 = string.Empty, val6 = string.Empty, sqry = string.Empty, py_fdate = string.Empty, py_ldate = string.Empty;

            int min_val = 1;


            bind_details();


            if (chk_var1.Checked == true)
            {
                if (TextBox1.Text != "" && TextBox2.Text != "")
                {
                   
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    dt = DBCon.Ora_Execute_table(qry1);
                    Rptviwerlejar.Reset();
                    ds.Tables.Add(dt);

                    List<DataRow> listResult = dt.AsEnumerable().ToList();
                    listResult.Count();
                    int countRow = 0;
                    countRow = listResult.Count();

                    Rptviwerlejar.LocalReport.DataSources.Clear();
                    if (countRow != 0)
                    {
                        Button3.Visible = true;
                        DataTable get_pfl = new DataTable();
                        get_pfl = DBCon.Ora_Execute_table("select syar_logo from KW_Profile_syarikat where cur_sts='1' and Status='A'");

                        string imagePath = string.Empty;
                        if (get_pfl.Rows[0]["syar_logo"].ToString() != "")
                        {
                            imagePath = new Uri(Server.MapPath("~/FILES/Profile_syarikat/" + get_pfl.Rows[0]["syar_logo"].ToString() + "")).AbsoluteUri;

                        }
                        else
                        {
                            imagePath = new Uri(Server.MapPath("~/FILES/Profile_syarikat/user.png")).AbsoluteUri;
                        }
                        Rptviwerlejar.LocalReport.EnableExternalImages = true;
                        Rptviwerlejar.LocalReport.ReportPath = "Kewengan/kw_lep_pl_new_var.rdlc";
                        ReportDataSource rds = new ReportDataSource("kwpandl_new", dt);


                        ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("d1", Convert.ToString(curr_yr)),
                     new ReportParameter("d2", Convert.ToString(( Int32.Parse(curr_yr) - 1))),
                     new ReportParameter("d3", Tk_mula.Text),
                     new ReportParameter("d4", Tk_akhir.Text),
                     new ReportParameter("d5", TextBox1.Text),
                     new ReportParameter("d6", TextBox2.Text),
                     new ReportParameter("v1",imagePath)
                };


                        Rptviwerlejar.LocalReport.SetParameters(rptParams);
                        Rptviwerlejar.LocalReport.DataSources.Add(rds);
                        Rptviwerlejar.LocalReport.DisplayName = "Penyata_P&L_Variance_" + DateTime.Now.ToString("yyyyMMdd");
                        Rptviwerlejar.LocalReport.Refresh();
                        System.Threading.Thread.Sleep(1);                        
                        Warning[] warnings;

                        string[] streamids;

                        string mimeType;

                        string encoding;

                        string extension;

                        byte[] bytes = Rptviwerlejar.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);

                        Response.Buffer = true;

                        Response.Clear();

                        Response.ContentType = mimeType;

                        Response.AddHeader("content-disposition", "attatchment; filename=Penyata_P&L_Variance" + DateTime.Now.ToString("yyyyMMdd") + "." + extension);

                        Response.BinaryWrite(bytes);

                        Response.Flush();

                        Response.End();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Tarikh Variance.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else if (chk_var3.Checked == true)
            {

               
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt = DBCon.Ora_Execute_table(qry1);
                Rptviwerlejar.Reset();
                ds.Tables.Add(dt);

                List<DataRow> listResult = dt.AsEnumerable().ToList();
                listResult.Count();
                int countRow = 0;
                countRow = listResult.Count();

                Rptviwerlejar.LocalReport.DataSources.Clear();
                if (countRow != 0)
                {
                    Button3.Visible = true;
                    DataTable get_pfl = new DataTable();
                    get_pfl = DBCon.Ora_Execute_table("select syar_logo from KW_Profile_syarikat where cur_sts='1' and Status='A'");

                    string imagePath = string.Empty;
                    if (get_pfl.Rows[0]["syar_logo"].ToString() != "")
                    {
                        imagePath = new Uri(Server.MapPath("~/FILES/Profile_syarikat/" + get_pfl.Rows[0]["syar_logo"].ToString() + "")).AbsoluteUri;

                    }
                    else
                    {
                        imagePath = new Uri(Server.MapPath("~/FILES/Profile_syarikat/user.png")).AbsoluteUri;
                    }
                    Rptviwerlejar.LocalReport.EnableExternalImages = true;
                    Rptviwerlejar.LocalReport.ReportPath = "Kewengan/kw_lep_pl_new_none.rdlc";
                    ReportDataSource rds = new ReportDataSource("kwpandl_new", dt);


                    ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("d1", Convert.ToString(curr_yr)),
                     new ReportParameter("d2", ""),
                     new ReportParameter("d3", Tk_mula.Text),
                     new ReportParameter("d4", Tk_akhir.Text),
                     new ReportParameter("d5", TextBox1.Text),
                     new ReportParameter("d6", TextBox2.Text),
                     new ReportParameter("v1",imagePath)
                };


                    Rptviwerlejar.LocalReport.SetParameters(rptParams);
                    Rptviwerlejar.LocalReport.DataSources.Add(rds);
                    Rptviwerlejar.LocalReport.DisplayName = "Penyata_P&L_" + DateTime.Now.ToString("yyyyMMdd");
                    Rptviwerlejar.LocalReport.Refresh();
                    System.Threading.Thread.Sleep(1);
                    Warning[] warnings;

                    string[] streamids;

                    string mimeType;

                    string encoding;

                    string extension;

                    byte[] bytes = Rptviwerlejar.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);

                    Response.Buffer = true;

                    Response.Clear();

                    Response.ContentType = mimeType;

                    Response.AddHeader("content-disposition", "attatchment; filename=Penyata_P&L_" + DateTime.Now.ToString("yyyyMMdd") +"." + extension);

                    Response.BinaryWrite(bytes);

                    Response.Flush();

                    Response.End();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else if (chk_var2.Checked == true)
            {
                int month;
                int i = 0;
                string ss1 = string.Empty, ss21 = string.Empty, ss12 = string.Empty;
                string get_dt = string.Empty, fmt1 = string.Empty, fmt2 = string.Empty, fmt3 = string.Empty, fmt4 = string.Empty, fmt5 = string.Empty;
                DateTime sdate = DateTime.ParseExact(Tk_mula.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime edate = DateTime.ParseExact(Tk_akhir.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                TimeSpan ts = edate.Subtract(sdate);
                int months = Convert.ToInt16(Math.Round((double)(ts.Days) / 30.0));
                if (months <= 12)
                {
                    while (sdate <= edate)
                    {
                        string gtmnth = sdate.Month.ToString().PadLeft(2, '0');
                        string nemnth = edate.Month.ToString().PadLeft(2, '0');
                        DateTimeFormatInfo d = new DateTimeFormatInfo();
                        month = Int32.Parse(gtmnth) - 1;
                        if (Int32.Parse(gtmnth) != Int32.Parse(nemnth))
                        {
                            ss1 = ",";
                            ss21 = " OR ";
                            ss12 = "+";
                        }
                        else
                        {
                            ss1 = "";
                            ss12 = "";
                            ss21 = "";
                        }
                        string ss2 = sdate.Month.ToString().PadLeft(2, '0');
                        string ss3 = sdate.ToString("MMM");
                        var lastDayOfMonth = DateTime.DaysInMonth(sdate.Year, Int32.Parse(ss2));


                        if (Int32.Parse(gtmnth) != Int32.Parse(nemnth))
                        {
                            fmt1 += " left join tree" + i + " s" + i + " on s" + i + ".kod_akaun=sk1.kod_akaun";
                            fmt2 += ", tree" + i + " AS(select b.kat_akaun,b.kod_akaun,b.nama_akaun,b.KW_Debit_amt,b.KW_kredit_amt,a.Amount from (select a.DirectChildId, (isnull(sum(cast(b.KW_Debit_amt as money)),'0.00') - isnull(sum(cast(b.KW_kredit_amt as money)),'0.00'))  as Amount from Recurse a "
                                    + " left join KW_General_Ledger b on b.GL_sts='L' and b.kod_akaun = a.kod_akaun where GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/01'), 0) and GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/" + lastDayOfMonth + "'), +1) group by DirectChildId) as a  "
                                    + " left join  (select m1.Id,m1.jenis_akaun_type,m1.kat_akaun,m1.nama_akaun,m1.kod_akaun,m1.jenis_akaun,m1.under_parent,sum(ISNULL(s1.KW_Debit_amt,'0.00')) as KW_Debit_amt "
                                    + " ,sum(ISNULL(s1.KW_kredit_amt,'0.00')) as KW_kredit_amt  from KW_Ref_Carta_Akaun m1 "
                                    + " left join KW_General_Ledger s1 on s1.GL_sts='L' and s1.kod_akaun=m1.kod_akaun and GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/01'), 0) and GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/" + lastDayOfMonth + "'), +1) where m1.Status='A' and m1.PAL_rep='1' group by "
                                    + " m1.Id,m1.jenis_akaun_type,m1.kat_akaun,m1.nama_akaun,m1.kod_akaun,m1.jenis_akaun,m1.under_parent) as b on b.Id=a.DirectChildId  where (b.KW_Debit_amt !='0.00' or b.KW_kredit_amt !='0.00' or a.Amount !='0.00')) ";
                            fmt3 += "cast(ISNULL(s" + i + ".Amount,'0.00') as money) " + ss3 + "" + ss1;
                            fmt4 += "ISNULL(s" + i + ".Amount,'0.00') != '0.00'" + ss21 + "";
                        }
                        else
                        {
                            fmt1 += " left join tree" + i + " s" + i + " on s" + i + ".kod_akaun=sk1.kod_akaun";
                            fmt2 += ", tree" + i + " AS(select b.kat_akaun,b.kod_akaun,b.nama_akaun,b.KW_Debit_amt,b.KW_kredit_amt,a.Amount from (select a.DirectChildId, (isnull(sum(cast(b.KW_Debit_amt as money)),'0.00') - isnull(sum(cast(b.KW_kredit_amt as money)),'0.00'))  as Amount from Recurse a "
                                    + " left join KW_General_Ledger b on b.GL_sts='L' and b.kod_akaun = a.kod_akaun where GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/01'), 0) and GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/" + lastDayOfMonth + "'), +1) group by DirectChildId) as a  "
                                    + " left join  (select m1.Id,m1.jenis_akaun_type,m1.kat_akaun,m1.nama_akaun,m1.kod_akaun,m1.jenis_akaun,m1.under_parent,sum(ISNULL(s1.KW_Debit_amt,'0.00')) as KW_Debit_amt "
                                    + " ,sum(ISNULL(s1.KW_kredit_amt,'0.00')) as KW_kredit_amt  from KW_Ref_Carta_Akaun m1 "
                                    + " left join KW_General_Ledger s1 on s1.GL_sts='L' and s1.kod_akaun=m1.kod_akaun and GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/01'), 0) and GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/" + lastDayOfMonth + "'), +1) where m1.Status='A' and m1.PAL_rep='1' group by "
                                    + " m1.Id,m1.jenis_akaun_type,m1.kat_akaun,m1.nama_akaun,m1.kod_akaun,m1.jenis_akaun,m1.under_parent) as b on b.Id=a.DirectChildId  where (b.KW_Debit_amt !='0.00' or b.KW_kredit_amt !='0.00' or a.Amount !='0.00')) ";
                            fmt3 += "cast(ISNULL(s" + i + ".Amount,'0.00') as money) " + ss3 + "" + ss1;
                            fmt4 += "ISNULL(s" + i + ".Amount,'0.00') != '0.00'" + ss21 + "";
                        }


                        //drpdate is dropdownlist
                        sdate = sdate.AddMonths(1);
                        i++;
                    }
                    if (months < 12)
                    {
                        for (int r = 1; r <= (12 - months); r++)
                        {
                            //if (r != (12 - months))
                            //{
                                string ss3 = sdate.ToString("MMM");
                                fmt3 += ",cast('0.00' as money) " + ss3 + "";
                                sdate = sdate.AddMonths(1);
                            //}
                            //else
                            //{
                            //    string ss3 = sdate.ToString("MMM");
                            //    fmt3 += "'0.00' " + ss3 + "";
                            //    sdate = sdate.AddMonths(1);
                            //}
                        }
                    }



                    qry1 = "with Recurse as ( select a.Id as DirectChildId, a.Id, a.kod_akaun from KW_Ref_Carta_Akaun a  where a.Status='A' and PAL_rep='1' and jenis_akaun_type='2'  union all select b.DirectChildId, a.Id, a.kod_akaun from KW_Ref_Carta_Akaun a "
                                       + " join Recurse b on b.Id = a.under_parent where a.Status='A' and PAL_rep='1') , "
                                       + " tree_k AS(select b.kat_akaun,b.nama_akaun,b.kod_akaun from (select a.DirectChildId from Recurse a left join KW_General_Ledger b on b.GL_sts='L' and a.kod_akaun = b.kod_akaun group by DirectChildId) as a  "
                                       + " Left join  (select m1.Id,m1.jenis_akaun_type,m1.kat_akaun,m1.nama_akaun,m1.kod_akaun,m1.jenis_akaun,m1.under_parent from KW_Ref_Carta_Akaun m1 where m1.Status='A' and m1.PAL_rep='1') as b on b.Id=a.DirectChildId) "
                                       + "" + fmt2 + " "
                                       + "select skk1.deskripsi ,sk1.kat_akaun,sk1.nama_akaun,sk1.kod_akaun," + fmt3 + " from  tree_k sk1 " + fmt1 + " left join KW_Kategori_akaun skk1 on skk1.kat_cd=sk1.kat_akaun and skk1.kat_type='01' where " + qry2 + " (" + fmt4 + ") order by sk1.kod_akaun";

                    DataSet ds1 = new DataSet();
                    DataTable dt1 = new DataTable();
                    dt1 = DBCon.Ora_Execute_table(qry1);
                    Rptviwerlejar.Reset();
                    ds1.Tables.Add(dt1);

                    List<DataRow> listResult1 = dt1.AsEnumerable().ToList();
                    listResult1.Count();
                    int countRow1 = 0;
                    countRow1 = listResult1.Count();

                    Rptviwerlejar.LocalReport.DataSources.Clear();
                    if (countRow1 != 0)
                    {
                        Button3.Visible = true;
                        DataTable get_pfl = new DataTable();
                        get_pfl = DBCon.Ora_Execute_table("select syar_logo from KW_Profile_syarikat where cur_sts='1' and Status='A'");

                        string imagePath = string.Empty;
                        if (get_pfl.Rows[0]["syar_logo"].ToString() != "")
                        {
                            imagePath = new Uri(Server.MapPath("~/FILES/Profile_syarikat/" + get_pfl.Rows[0]["syar_logo"].ToString() + "")).AbsoluteUri;

                        }
                        else
                        {
                            imagePath = new Uri(Server.MapPath("~/FILES/Profile_syarikat/user.png")).AbsoluteUri;
                        }
                        Rptviwerlejar.LocalReport.EnableExternalImages = true;
                        Rptviwerlejar.LocalReport.ReportPath = "Kewengan/KW_pandl_new_mnth.rdlc";
                        ReportDataSource rds = new ReportDataSource("kwpandl1", dt1);


                       

                        ReportParameter[] rptParams = new ReportParameter[16];
                        rptParams[0] = new ReportParameter("d1", Tk_mula.Text);
                        rptParams[1] = new ReportParameter("d2", Tk_akhir.Text);
                        rptParams[2] = new ReportParameter("d3", imagePath);
                        rptParams[3] = new ReportParameter("d4", Convert.ToString(months));
                        int param_count = 4;
                        string param_val = "";
                        int m = 1;
                        DateTime sdate1 = DateTime.ParseExact(Tk_mula.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime edate1 = DateTime.ParseExact(Tk_akhir.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        while (sdate1 <= edate1)
                        {
                            param_val = "column" + m;
                            string ss3 = sdate1.ToString("MMM");
                            rptParams[param_count] = new ReportParameter(param_val, ss3);
                            sdate1 = sdate1.AddMonths(1);
                            param_count++;
                            m++;
                        }
                        if (months < 12)
                        {
                            for (int r = 0; r <= ((12 - months) -1); r++)
                            {
                                param_val = "column" + m;
                                rptParams[param_count + r] = new ReportParameter(param_val, "");
                                m++;
                            }
                        }
                       

                        Rptviwerlejar.LocalReport.SetParameters(rptParams);
                        Rptviwerlejar.LocalReport.DataSources.Add(rds);
                        Rptviwerlejar.LocalReport.DisplayName = "Penyata_P&L_Monthly_" + DateTime.Now.ToString("yyyyMMdd");
                        Rptviwerlejar.LocalReport.Refresh();
                        System.Threading.Thread.Sleep(1);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Please Select Maximum 12 Months.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
                      
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    protected void ExportToEXCEL(object sender, EventArgs e)
    {
        if (Tk_mula.Text != "" && Tk_akhir.Text != "")
        {
            string val1 = string.Empty, val2 = string.Empty, val3 = string.Empty, val4 = string.Empty, val5 = string.Empty, val6 = string.Empty, sqry = string.Empty, py_fdate = string.Empty, py_ldate = string.Empty;

            int min_val = 1;
            bind_details();

            if (chk_var1.Checked == true)
            {
                
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt = DBCon.Ora_Execute_table(qry1);
                Rptviwerlejar.Reset();
                ds.Tables.Add(dt);

                List<DataRow> listResult = dt.AsEnumerable().ToList();
                listResult.Count();
                int countRow = 0;
                countRow = listResult.Count();

                if (countRow != 0)
                {

                    StringBuilder builder = new StringBuilder();
                    string strFileName = string.Format("{0}.{1}", "Penyata_P&L_" + DateTime.Now.ToString("ddMMyyyy") + "", "csv");
                  DateTime fd1 = DateTime.ParseExact(Tk_mula.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime ed1 = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    builder.Append("NAMA KATEGORY,NAMA HEADER,KOD AKAUN,NAMA AKAUN," + curr_yr + ", " + Convert.ToString((Int32.Parse(curr_yr) - 1)) +  Environment.NewLine);
                        for (int k = 0; k <= (dt.Rows.Count - 1); k++)
                        {
                     
                            builder.Append(dt.Rows[k]["kat"].ToString() + " , " + dt.Rows[k]["hdr"].ToString() + ", " + dt.Rows[k]["tmp_kod_akaun"].ToString() + "," + dt.Rows[k]["nama_akaun"].ToString().Replace(",", "") + "," + dt.Rows[k]["cur_jum"].ToString() + "," + dt.Rows[k]["pre_jum"].ToString()+ Environment.NewLine);

                        }
                   
                    Response.Clear();
                    Response.ContentType = "text/csv";
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + strFileName);
                    Response.Write(builder.ToString());
                    Response.End();


                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else if (chk_var3.Checked == true)
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt = DBCon.Ora_Execute_table(qry1);
                Rptviwerlejar.Reset();
                ds.Tables.Add(dt);

                List<DataRow> listResult = dt.AsEnumerable().ToList();
                listResult.Count();
                int countRow = 0;
                countRow = listResult.Count();

                if (countRow != 0)
                {

                    StringBuilder builder = new StringBuilder();
                    string strFileName = string.Format("{0}.{1}", "Penyata_P&L_" + DateTime.Now.ToString("ddMMyyyy") + "", "csv");
                    DateTime fd1 = DateTime.ParseExact(Tk_mula.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    builder.Append("NAMA KATEGORY,NAMA HEADER,KOD AKAUN,NAMA AKAUN," + curr_yr + Environment.NewLine);
                    for (int k = 0; k <= (dt.Rows.Count - 1); k++)
                    {

                        builder.Append(dt.Rows[k]["kat"].ToString() + " , " + dt.Rows[k]["hdr"].ToString() + ", " + dt.Rows[k]["tmp_kod_akaun"].ToString() + "," + dt.Rows[k]["nama_akaun"].ToString().Replace(",", "") + "," + dt.Rows[k]["cur_jum"].ToString() + Environment.NewLine);

                    }

                    Response.Clear();
                    Response.ContentType = "text/csv";
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + strFileName);
                    Response.Write(builder.ToString());
                    Response.End();


                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else if (chk_var2.Checked == true)
            {
                int month;
                int i = 0;
                string ss1 = string.Empty, ss21 = string.Empty, ss12 = string.Empty;
                string get_dt = string.Empty, fmt1 = string.Empty, fmt2 = string.Empty, fmt3 = string.Empty, fmt4 = string.Empty, fmt5 = string.Empty;
                DateTime sdate = DateTime.ParseExact(Tk_mula.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime edate = DateTime.ParseExact(Tk_akhir.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                TimeSpan ts = edate.Subtract(sdate);
                int months = Convert.ToInt16(Math.Round((double)(ts.Days) / 30.0));
                if (months <= 12)
                {
                    while (sdate <= edate)
                    {
                        string gtmnth = sdate.Month.ToString().PadLeft(2, '0');
                        string nemnth = edate.Month.ToString().PadLeft(2, '0');
                        DateTimeFormatInfo d = new DateTimeFormatInfo();
                        month = Int32.Parse(gtmnth) - 1;
                        if (Int32.Parse(gtmnth) != Int32.Parse(nemnth))
                        {
                            ss1 = ",";
                            ss21 = " OR ";
                            ss12 = "+";
                        }
                        else
                        {
                            ss1 = "";
                            ss12 = "";
                            ss21 = "";
                        }
                        string ss2 = sdate.Month.ToString().PadLeft(2, '0');
                        string ss3 = sdate.ToString("MMM");
                        var lastDayOfMonth = DateTime.DaysInMonth(sdate.Year, Int32.Parse(ss2));


                        if (Int32.Parse(gtmnth) != Int32.Parse(nemnth))
                        {
                            fmt1 += " left join tree" + i + " s" + i + " on s" + i + ".kod_akaun=sk1.kod_akaun";
                            fmt2 += ", tree" + i + " AS(select b.kat_akaun,b.kod_akaun,b.nama_akaun,b.KW_Debit_amt,b.KW_kredit_amt,a.Amount from (select a.DirectChildId, (isnull(sum(cast(b.KW_Debit_amt as money)),'0.00') - isnull(sum(cast(b.KW_kredit_amt as money)),'0.00'))  as Amount from Recurse a "
                                    + " left join KW_General_Ledger b on b.GL_sts='L' and b.kod_akaun = a.kod_akaun where GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/01'), 0) and GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/" + lastDayOfMonth + "'), +1) group by DirectChildId) as a  "
                                    + " left join  (select m1.Id,m1.jenis_akaun_type,m1.kat_akaun,m1.nama_akaun,m1.kod_akaun,m1.jenis_akaun,m1.under_parent,sum(ISNULL(s1.KW_Debit_amt,'0.00')) as KW_Debit_amt "
                                    + " ,sum(ISNULL(s1.KW_kredit_amt,'0.00')) as KW_kredit_amt  from KW_Ref_Carta_Akaun m1 "
                                    + " left join KW_General_Ledger s1 on s1.GL_sts='L' and s1.kod_akaun=m1.kod_akaun and GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/01'), 0) and GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/" + lastDayOfMonth + "'), +1) where m1.Status='A' and m1.PAL_rep='1' group by "
                                    + " m1.Id,m1.jenis_akaun_type,m1.kat_akaun,m1.nama_akaun,m1.kod_akaun,m1.jenis_akaun,m1.under_parent) as b on b.Id=a.DirectChildId  where (b.KW_Debit_amt !='0.00' or b.KW_kredit_amt !='0.00' or a.Amount !='0.00')) ";
                            fmt3 += "cast(ISNULL(s" + i + ".Amount,'0.00') as money) " + ss3 + "" + ss1;
                            fmt4 += "ISNULL(s" + i + ".Amount,'0.00') != '0.00'" + ss21 + "";
                        }
                        else
                        {
                            fmt1 += " left join tree" + i + " s" + i + " on s" + i + ".kod_akaun=sk1.kod_akaun";
                            fmt2 += ", tree" + i + " AS(select b.kat_akaun,b.kod_akaun,b.nama_akaun,b.KW_Debit_amt,b.KW_kredit_amt,a.Amount from (select a.DirectChildId, (isnull(sum(cast(b.KW_Debit_amt as money)),'0.00') - isnull(sum(cast(b.KW_kredit_amt as money)),'0.00'))  as Amount from Recurse a "
                                    + " left join KW_General_Ledger b on b.GL_sts='L' and b.kod_akaun = a.kod_akaun where GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/01'), 0) and GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/" + lastDayOfMonth + "'), +1) group by DirectChildId) as a  "
                                    + " left join  (select m1.Id,m1.jenis_akaun_type,m1.kat_akaun,m1.nama_akaun,m1.kod_akaun,m1.jenis_akaun,m1.under_parent,sum(ISNULL(s1.KW_Debit_amt,'0.00')) as KW_Debit_amt "
                                    + " ,sum(ISNULL(s1.KW_kredit_amt,'0.00')) as KW_kredit_amt  from KW_Ref_Carta_Akaun m1 "
                                    + " left join KW_General_Ledger s1 on s1.GL_sts='L' and s1.kod_akaun=m1.kod_akaun and GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/01'), 0) and GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/" + lastDayOfMonth + "'), +1) where m1.Status='A' and m1.PAL_rep='1' group by "
                                    + " m1.Id,m1.jenis_akaun_type,m1.kat_akaun,m1.nama_akaun,m1.kod_akaun,m1.jenis_akaun,m1.under_parent) as b on b.Id=a.DirectChildId  where (b.KW_Debit_amt !='0.00' or b.KW_kredit_amt !='0.00' or a.Amount !='0.00')) ";
                            fmt3 += "cast(ISNULL(s" + i + ".Amount,'0.00') as money) " + ss3 + "" + ss1;
                            fmt4 += "ISNULL(s" + i + ".Amount,'0.00') != '0.00'" + ss21 + "";
                        }


                        //drpdate is dropdownlist
                        sdate = sdate.AddMonths(1);
                        i++;
                    }
                   



                    qry1 = "with Recurse as ( select a.Id as DirectChildId, a.Id, a.kod_akaun from KW_Ref_Carta_Akaun a  where a.Status='A' and PAL_rep='1' and jenis_akaun_type='2'  union all select b.DirectChildId, a.Id, a.kod_akaun from KW_Ref_Carta_Akaun a "
                                       + " join Recurse b on b.Id = a.under_parent where a.Status='A' and PAL_rep='1') , "
                                       + " tree_k AS(select b.kat_akaun,b.nama_akaun,b.kod_akaun from (select a.DirectChildId from Recurse a left join KW_General_Ledger b on b.GL_sts='L' and a.kod_akaun = b.kod_akaun group by DirectChildId) as a  "
                                       + " Left join  (select m1.Id,m1.jenis_akaun_type,m1.kat_akaun,m1.nama_akaun,m1.kod_akaun,m1.jenis_akaun,m1.under_parent from KW_Ref_Carta_Akaun m1 where m1.Status='A' and m1.PAL_rep='1') as b on b.Id=a.DirectChildId) "
                                       + "" + fmt2 + " "
                                       + "select skk1.deskripsi ,sk1.kat_akaun,sk1.nama_akaun,sk1.kod_akaun," + fmt3 + " from  tree_k sk1 " + fmt1 + " left join KW_Kategori_akaun skk1 on skk1.kat_cd=sk1.kat_akaun and skk1.kat_type='01' where " + qry2 + " (" + fmt4 + ") order by sk1.kod_akaun";

                    DataSet ds1 = new DataSet();
                    DataTable dt1 = new DataTable();
                    dt1 = DBCon.Ora_Execute_table(qry1);                    
                    ds1.Tables.Add(dt1);

                    List<DataRow> listResult1 = dt1.AsEnumerable().ToList();
                    listResult1.Count();
                    int countRow1 = 0;
                    countRow1 = listResult1.Count();
                    string ss1_1 = string.Empty, ss21_1 = string.Empty, ss12_1 = string.Empty;
                    string get_dt_1 = string.Empty, fmt1_1 = string.Empty, fmt2_1 = string.Empty, fmt3_1 = string.Empty, fmt4_1 = string.Empty, fmt_1 = string.Empty;
                    string ss1_2 = string.Empty, ss21_2 = string.Empty, ss12_2 = string.Empty;
                    string get_dt_3 = string.Empty, fmt1_2 = string.Empty, fmt2_2 = string.Empty, fmt3_2 = string.Empty, fmt4_2 = string.Empty, fmt_2 = string.Empty;

                    if (countRow1 != 0)
                    {
                        DateTime sdate1 = DateTime.ParseExact(Tk_mula.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime edate1 = DateTime.ParseExact(Tk_akhir.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        while (sdate1 <= edate1)
                        {
                            string gtmnth_1 = sdate1.Month.ToString().PadLeft(2, '0');
                            string nemnth_1 = edate1.Month.ToString().PadLeft(2, '0');
                            DateTimeFormatInfo d = new DateTimeFormatInfo();
                            month = Int32.Parse(gtmnth_1) - 1;
                            if (Int32.Parse(gtmnth_1) != Int32.Parse(nemnth_1))
                            {
                                ss1_1 = ",";                                

                            }
                            else
                            {
                                ss1_1 = "";                                
                            }
                            string ss2_1 = sdate1.Month.ToString().PadLeft(2, '0');
                            string ss3_1 = sdate1.ToString("MMM");
                            var lastDayOfMonth1 = DateTime.DaysInMonth(sdate1.Year, Int32.Parse(ss2_1));


                            if (Int32.Parse(gtmnth_1) != Int32.Parse(nemnth_1))
                            {
                                fmt3_1 +=  ss3_1 + "" + ss1_1;                                
                            }
                            else
                            {
                                fmt3_1 +=  ss3_1 + "" + ss1_1;                                
                            }


                            //drpdate is dropdownlist
                            sdate1 = sdate1.AddMonths(1);
                            i++;
                        }

                        StringBuilder builder = new StringBuilder();
                        string strFileName = string.Format("{0}.{1}", "Penyata_P&L_Monthly" + DateTime.Now.ToString("ddMMyyyy") + "", "csv");
                        builder.Append("KATEGORI KOD,NAMA KATEGORY,KOD AKAUN,NAMA AKAUN," + fmt3_1 + "" + Environment.NewLine);
                        for (int k = 0; k <= (dt1.Rows.Count - 1); k++)
                        {
                            DateTime sdate2 = DateTime.ParseExact(Tk_mula.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            DateTime edate2 = DateTime.ParseExact(Tk_akhir.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            while (sdate2 <= edate2)
                            {
                                string gtmnth_2 = sdate2.Month.ToString().PadLeft(2, '0');
                                string nemnth_2 = edate2.Month.ToString().PadLeft(2, '0');
                                DateTimeFormatInfo d = new DateTimeFormatInfo();
                                month = Int32.Parse(gtmnth_2) - 1;
                                if (Int32.Parse(gtmnth_2) != Int32.Parse(nemnth_2))
                                {                                    
                                    ss1_2 = " ,";

                                }
                                else
                                {                                    
                                    ss1_2 = " ";
                                }
                                string ss2_2 = sdate2.Month.ToString().PadLeft(2, '0');
                                string ss3_2 = sdate2.ToString("MMM");
                                var lastDayOfMonth1 = DateTime.DaysInMonth(sdate2.Year, Int32.Parse(ss2_2));


                                if (Int32.Parse(gtmnth_2) != Int32.Parse(nemnth_2))
                                {                                    
                                    fmt4_2 += dt1.Rows[k][ss3_2].ToString() +"" + ss1_2;
                                }
                                else
                                {
                                    fmt4_2 += dt1.Rows[k][ss3_2].ToString() + "" + ss1_2;
                                }


                                //drpdate is dropdownlist
                                sdate2 = sdate2.AddMonths(1);
                                i++;
                            }
                            fmt5 = fmt4_2;
                            builder.Append(dt1.Rows[k]["kat_akaun"].ToString() + " , " + dt1.Rows[k]["deskripsi"].ToString() + ", " + dt1.Rows[k]["kod_akaun"].ToString() + "," + dt1.Rows[k]["nama_akaun"].ToString().Replace(",", "") + "," + fmt5 + Environment.NewLine);
                            fmt4_2 = "";

                        }

                        Response.Clear();
                        Response.ContentType = "text/csv";
                        Response.AddHeader("Content-Disposition", "attachment;filename=" + strFileName);
                        Response.Write(builder.ToString());
                        Response.End();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Please Select Maximum 12 Months.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    //protected void clk_update(object sender, EventArgs e)
    //{
    //    if (Tk_mula.Text != "" && Tk_akhir.Text != "")
    //    {
    //        LinkButton btnButton = sender as LinkButton;
    //        GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;

            
            
    //        //System.Web.UI.WebControls.Label og_genid = (System.Web.UI.WebControls.Label)gvRow.FindControl("bal_type");
    //        System.Web.UI.WebControls.Label akaun_txt = (System.Web.UI.WebControls.Label)gvRow.FindControl("kat_cd");
    //        string og_genid = string.Empty;
    //        og_genid = gvRow.Cells[1].Text;
    //        DataTable get_carta_kod = new DataTable();
    //        get_carta_kod = DBCon.Ora_Execute_table("select kod_akaun from KW_Ref_Carta_Akaun where nama_akaun='"+ akaun_txt.Text + "' and Status='A' ");
    //        if(get_carta_kod.Rows.Count != 0)
    //        {
    //            og_genid = get_carta_kod.Rows[0]["kod_akaun"].ToString();
    //        }

    //        DataSet ds = new DataSet();
    //        DataSet ds1 = new DataSet();
    //        DataTable dt = new DataTable();
    //        DataTable dt1 = new DataTable();
    //        if (Tk_mula.Text != "")
    //        {
    //            string fdate = Tk_mula.Text;
    //            DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
    //            fmdate = fd.ToString("yyyy-MM-dd");
    //            fyear = fd.ToString("yyyy");

    //        }
    //        if (Tk_akhir.Text != "")
    //        {
    //            string tdate = Tk_akhir.Text;
    //            DateTime td = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
    //            tmdate = td.ToString("yyyy-MM-dd");
    //        }
    //        sqry1 = "select * from (select m1.project_kod,s1.kat_akaun kcd,s11.kat_akuan kname,s1.kod_akaun,s1.nama_akaun,case when ISNULL(s2.opn_debit_amt,'0.00')='0.00' then (ISNULL(s2.opn_kredit_amt,'0.00') + (ISNULL(m1_1.KW_kredit_amt,'0.00')-ISNULL(m1_1.KW_Debit_amt,'0.00'))) else (-1 * (ISNULL(s2.opn_debit_amt,'0.00') + (ISNULL(m1_1.KW_kredit_amt,'0.00')-ISNULL(m1_1.KW_Debit_amt,'0.00')))) end as opening_amt,m1.GL_invois_no as no_invois,m1.GL_post_dt,Format(m1.GL_post_dt, 'dd/MM/yyyy') tarikh_invois "
    //                                 + " ,ISNULL(m1.GL_desc1, '') as rjkn1,'' as rjkn2,ISNULL(m1.KW_Debit_amt, '') as KW_Debit_amt,ISNULL(m1.KW_kredit_amt, '') as KW_kredit_amt,m1.ref2,m1.GL_journal_no"
    //                                 + ", m1.Gl_jenis_no as no1"
    //                                 + " ,(case "
    //                                 + " when m1.ref2 = '01' then(select '' v1 from KW_Pembayaran_invois where no_invois = m1.GL_invois_no)"
    //                                 + " when m1.ref2 = '02' then(select no_cek v1 from KW_Penerimaan_resit where no_resit = m1.GL_invois_no)"
    //                                 + " when m1.ref2 = '03' then(select '' v1 from KW_Pembayaran_mohon where no_permohonan = m1.GL_invois_no)"
    //                                 + " when m1.ref2 = '04' then(select No_cek v1 from KW_Pembayaran_Pay_voucer where Pv_no = m1.GL_invois_no)"
    //                                 + " when m1.ref2 = '05' then(select '' v1 from KW_Pembayaran_Credit where no_Rujukan = m1.GL_invois_no)"
    //                                 + " when m1.ref2 = '06' then(select '' v1 from KW_Pembayaran_Dedit where no_Rujukan = m1.GL_invois_no)"
    //                                 + " when m1.ref2 = '09' then(select '' v1 from KW_Penerimaan_invois where no_invois = m1.GL_invois_no)"
    //                                 + " when m1.ref2 = '10' then(select '' v1 from KW_Penerimaan_Credit where no_notakredit = m1.GL_invois_no)"
    //                                 + " when m1.ref2 = '11' then(select '' v1 from KW_Penerimaan_Debit where no_notadebit = m1.GL_invois_no)"
    //                                 + " when m1.ref2 IN ('12', '13', '14', '15', '16', '17', '18', '19', '20', '21') then(select '' v1 from KW_jurnal_inter where no_permohonan = m1.GL_invois_no)"
    //                                 + " end) as no2, s3.Ref_doc_name dc_name from KW_Ref_Carta_Akaun s1"
    //                                 + " left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = s1.kod_akaun and '" + fmdate + "' between s2.start_dt and s2.end_dt "
    //                                 + " left join KW_General_Ledger m1_1 on s1.kod_akaun = m1_1.kod_akaun and m1_1.GL_sts='L' and YEAR(m1_1.GL_post_dt)='" + fyear + "' and  m1_1.GL_post_dt < DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) "
    //                                 + " left join KW_General_Ledger m1 on s1.kod_akaun = m1.kod_akaun and m1.GL_sts='L' and m1.GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and m1.GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) "
    //                                 + " left join KW_Kategori_akaun s11 on s11.kat_type='01' and s11.kat_cd=s1.kat_akaun "
    //                                 + " left join KW_Ref_Doc_kod s3 on s3.Ref_doc_code = m1.ref2"
    //                                 + " where s1.Status = 'A' and s1.jenis_akaun_type !='1' and ISNULL(ca_cyp,'') !='1' ) as a "
    //                      + " where a.kod_akaun ='" + og_genid + "' "
    //                     + " order by a.kod_akaun";

    //        sqry2 = " select  sum(a.opening_amt) open_amt from (select distinct s1.kat_akaun kcd,s11.kat_akuan kname,s1.kod_akaun,s1.nama_akaun, "
    //                              + " case when ISNULL(s2.opn_debit_amt,'0.00')='0.00' then (ISNULL(s2.opn_kredit_amt,'0.00'  ) + (ISNULL(m1_1.KW_kredit_amt,'0.00') - ISNULL(m1_1.KW_Debit_amt,'0.00'))) else ((ISNULL(s2.opn_debit_amt,'0.00') + 	(ISNULL(m1_1.KW_Debit_amt,'0.00') - ISNULL(m1_1.KW_kredit_amt,'0.00')))) end as opening_amt  "
    //                               + "  from KW_Ref_Carta_Akaun s1 left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = s1.kod_akaun and '" + fmdate + "' between s2.start_dt and s2.end_dt   "
    //                               + " outer apply(select ISNULL(sum(m1_1.KW_kredit_amt),'0.00') KW_kredit_amt,ISNULL(sum(m1_1.KW_kredit_amt),'0.00')  KW_Debit_amt  from KW_General_Ledger m1_1 where s1.kod_akaun = m1_1.kod_akaun and m1_1.GL_sts='L' and YEAR(m1_1.GL_post_dt)='" + prev_yr + "' and  m1_1.GL_post_dt < DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0)) as m1   "
    //                               + " outer apply(select sum(ISNULL(m1.KW_kredit_amt,'0.00')) KW_kredit_amt,sum(ISNULL(m1.KW_Debit_amt,'0.00')) KW_Debit_amt  from KW_General_Ledger m1 where s1.kod_akaun = m1.kod_akaun and m1.GL_sts='L' and YEAR(m1.GL_post_dt)='" + fyear + "' and  m1.GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and m1.GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1)) as m1_1   "
    //                               + " left join KW_Kategori_akaun s11 on s11.kat_type='01' and s11.kat_cd=s1.kat_akaun  left join KW_Ref_Doc_kod s3 on s3.Ref_doc_code = '' where s1.Status = 'A' and s1.kod_akaun ='" + og_genid + "'  "
    //                               + " and s1.jenis_akaun_type !='1' and ISNULL(ca_cyp,'') !='1' ) as a  ";


    //        dt = DBCon.Ora_Execute_table(sqry1);
    //        dt1 = DBCon.Ora_Execute_table(sqry2);
    //        //dt1 = DBCon.Ora_Execute_table(sqry2);
    //        ds.Tables.Add(dt);
    //        //ds1.Tables.Add(dt1);

    //        string vs1 = string.Empty, vs2 = string.Empty, vs3 = string.Empty, vs4 = string.Empty, vs5 = string.Empty, vs6 = string.Empty;

    //        ReportViewer1.Reset();
    //        ReportViewer1.LocalReport.Refresh();
    //        List<DataRow> listResult = dt.AsEnumerable().ToList();
    //        listResult.Count();
    //        int countRow = 0;
    //        countRow = listResult.Count();



    //        ReportViewer1.LocalReport.DataSources.Clear();
    //        if (countRow != 0)
    //        {
    //            DataTable get_pfl = new DataTable();
    //            get_pfl = DBCon.Ora_Execute_table("select syar_logo from KW_Profile_syarikat where cur_sts='1' and Status='A'");

    //            string imagePath = string.Empty;
    //            if (get_pfl.Rows[0]["syar_logo"].ToString() != "")
    //            {
    //                imagePath = new Uri(Server.MapPath("~/FILES/Profile_syarikat/" + get_pfl.Rows[0]["syar_logo"].ToString() + "")).AbsoluteUri;

    //            }
    //            else
    //            {
    //                imagePath = new Uri(Server.MapPath("~/FILES/Profile_syarikat/user.png")).AbsoluteUri;
    //            }

    //            DataTable cnt_open = new DataTable();

    //            cnt_open = DBCon.Ora_Execute_table(sqry2);
    //            ReportViewer1.LocalReport.EnableExternalImages = true;

    //            ReportViewer1.LocalReport.ReportPath = "Kewengan/KW_kunci_kira_new.rdlc";

    //            ReportDataSource rds = new ReportDataSource("kwlegar", dt);
    //            //ReportDataSource rds1 = new ReportDataSource("kwlegar1", dt1);
    //            ReportParameter[] rptParams = new ReportParameter[]{
    //                 new ReportParameter("s1", "SEMUA"),
    //                 new ReportParameter("s2", "NO"),
    //                 new ReportParameter("s3", Tk_mula.Text),
    //                 new ReportParameter("s4", Tk_akhir.Text),
    //                 new ReportParameter("S5", double.Parse(dt1.Rows[0]["open_amt"].ToString()).ToString("C").Replace("RM","").Replace("$","")),
    //                 new ReportParameter("v1",imagePath)
    //                      };

    //            ReportViewer1.LocalReport.SetParameters(rptParams);
    //            ReportViewer1.LocalReport.DataSources.Add(rds);
    //            //Rptviwerlejar.LocalReport.DataSources.Add(rds1);
    //            ReportViewer1.LocalReport.DisplayName = "JURNAL_TXN_" + akaun_txt.Text.ToUpper() + "_" + DateTime.Now.ToString("yyyyMMdd");

    //            ReportViewer1.LocalReport.Refresh();

    //            System.Threading.Thread.Sleep(1);
    //            Warning[] warnings;

    //            string[] streamids;

    //            string mimeType;

    //            string encoding;

    //            string extension;

    //            byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);

    //            Response.Buffer = true;

    //            Response.Clear();

    //            Response.ContentType = mimeType;

    //            Response.AddHeader("content-disposition", "attatchment; filename=\"JURNAL_TXN_" + akaun_txt.Text.ToUpper() + "_" + DateTime.Now.ToString("yyyyMMdd") + "." + extension + "\"");

    //            Response.BinaryWrite(bytes);

    //            Response.Flush();

    //            Response.End();


    //        }
    //        else
    //        {
    //            bind_data();
    //            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod tidak dijumpai. Sila Pastikan Semua Maklumat Dimasukkan Dengan Betul.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
    //        }

    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
    //    }
    //}

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //System.Web.UI.WebControls.Label lbl1 = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label1_nw");
        //System.Web.UI.WebControls.Label clmn1 = (System.Web.UI.WebControls.Label)e.Row.FindControl("bal_type");
        //System.Web.UI.WebControls.Label clmn2 = (System.Web.UI.WebControls.Label)e.Row.FindControl("kat_cd");
        //System.Web.UI.WebControls.Label clmn3 = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label3");
        //System.Web.UI.WebControls.Label clmn4 = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label4");

        //System.Web.UI.WebControls.Label hdr = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label2_nw");

        //LinkButton Button3 = e.Row.FindControl("LinkButton1") as LinkButton;

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    if (hdr.Text == "1")
        //    {
        //        Button3.Attributes.Add("style", "display:none;");
        //    }
        //    else
        //    {
        //        Button3.Attributes.Remove("style");
        //    }

        //    if (lbl1.Text == "1")
        //    {
        //        e.Row.BackColor = Color.FromName("#D4D4D4");
        //    }
        //    else if (lbl1.Text == "2")
        //    {
        //        e.Row.BackColor = Color.FromName("#D2D7D3");
        //        clmn1.Attributes.Add("style", "padding-left:10px;");
        //        clmn2.Attributes.Add("style", "padding-left:10px;");
        //    }
        //    else if (lbl1.Text == "3")
        //    {
        //        e.Row.BackColor = Color.FromName("#E7E7E7");
        //        clmn1.Attributes.Add("style", "padding-left:20px;");
        //        clmn2.Attributes.Add("style", "padding-left:20px;");
        //    }
        //    else if (lbl1.Text == "4")
        //    {
        //        e.Row.BackColor = Color.FromName("#F5F5F5");
        //        clmn1.Attributes.Add("style", "padding-left:30px;");
        //        clmn2.Attributes.Add("style", "padding-left:30px;");
        //    }
        //    else if (lbl1.Text == "5")
        //    {
        //        e.Row.BackColor = Color.FromName("#F5F5F5");
        //        clmn1.Attributes.Add("style", "padding-left:40px;");
        //        clmn2.Attributes.Add("style", "padding-left:40px;");
        //    }
        //    else if (lbl1.Text == "6")
        //    {
        //        e.Row.BackColor = Color.FromName("#F5F5F5");
        //        clmn1.Attributes.Add("style", "padding-left:50px;");
        //        clmn2.Attributes.Add("style", "padding-left:50px;");
        //    }
        //    else if (lbl1.Text == "7")
        //    {
        //        e.Row.BackColor = Color.FromName("#F5F5F5");
        //        clmn1.Attributes.Add("style", "padding-left:60px;");
        //        clmn2.Attributes.Add("style", "padding-left:60px;");
        //    }
        //    else if (lbl1.Text == "8")
        //    {
        //        e.Row.BackColor = Color.FromName("#F5F5F5");
        //        clmn1.Attributes.Add("style", "padding-left:70px;");
        //        clmn2.Attributes.Add("style", "padding-left:70px;");
        //    }
        //    if (hdr.Text == "0")
        //    {
        //        e.Row.BackColor = Color.FromName("#FFFFFF");
        //    }
        //}
    }
    protected void btn_reset(object sender, EventArgs e)
    {
        Response.Redirect("../kewengan/kw_lep_pl.aspx");
    }


}