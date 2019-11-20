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

public partial class kw_lep_bajet : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    private static int PageSize = 20;
    string qry1 = string.Empty, qry2 = string.Empty;
    string level;
    string Status = string.Empty;
    string userid;
    string CommandArgument1 = string.Empty, CommandArgument2 = string.Empty, CommandArgument3 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        Button4.OnClientClick = @"if(this.value == 'Please wait...')
           return false;
           this.value = 'Please wait...';this.disabled=true";
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Button4);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
                BindData();
                bind_akaun();
                bind_tah_kew();
                tah_kewangan.SelectedValue = DateTime.Now.Year.ToString();
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }
    void app_language()

    {
        if (Session["New"] != null)
        {
            DataTable ste_set = new DataTable();
            ste_set = DBCon.Ora_Execute_table("select * from site_settings where ID IN ('1')");

            DataTable gt_lng = new DataTable();
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('741','705','1724','64','65','121','15')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());      
            //ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            //ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
          

        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }

    void bind_tah_kew()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select fin_year from KW_financial_Year group by fin_year";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            tah_kewangan.DataSource = dt;
            tah_kewangan.DataTextField = "fin_year";
            tah_kewangan.DataValueField = "fin_year";
            tah_kewangan.DataBind();
            tah_kewangan.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void bind_akaun()
    {
        //string get_qry = string.Empty;


        //DataSet Ds = new DataSet();
        //try
        //{
        //    string com = "select Ref_kat_bajet from KW_Ref_Bajet group by Ref_kat_bajet";
        //    SqlDataAdapter adpt = new SqlDataAdapter(com, con);
        //    DataTable dt = new DataTable();
        //    adpt.Fill(dt);
        //    dd_kategori.DataSource = dt;
        //    dd_kategori.DataTextField = "Ref_kat_bajet";
        //    dd_kategori.DataValueField = "Ref_kat_bajet";
        //    dd_kategori.DataBind();
        //    dd_kategori.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
    }
    protected void BindData()
    {

    }

    protected void clk_submit(object sender, EventArgs e)
    {
        if (tah_kewangan.SelectedValue != "")
        {
            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            string val1 = string.Empty, val2 = string.Empty, val3 = string.Empty, val4 = string.Empty, val5 = string.Empty, val6 = string.Empty, pre_day = string.Empty, pre_year = string.Empty;

            string fmdate = string.Empty, tmdate = string.Empty;
            //if (Tk_mula.Text != "")
            //{
                string fdate = "01/01/"+ tah_kewangan.SelectedValue;
                DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                fmdate = fd.ToString("yyyy-MM-dd");
                pre_day = fd.AddDays(-1).ToString("yyyy-MM-dd");
                pre_year = fd.AddYears(-1).ToString("yyyy");
            //}
            //if (Tk_akhir.Text != "")
            //{
            //    string tdate = Tk_akhir.Text;
            //    DateTime td = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //    tmdate = td.ToString("yyyy-MM-dd");
            //}
            int min_val = 1;
            int curr_yr = Int32.Parse(DateTime.Now.Year.ToString());
            int prev_yr = (Int32.Parse(DateTime.Now.Year.ToString()) - min_val);

            if (tah_kewangan.SelectedValue != "" )
            {
                val6 = "select a.ref_kumpulan,a.Ref_kat_bajet,cast(ISNULL(replace(a.Ref_jumlah_bajet, ',',''),'0.00') as money) Ref_jumlah_bajet,cast(ISNULL(replace(a1.Ref_jumlah_bajet, ',',''),'0.00') as money) as Ref_jumlah_bajet1,case when ISNULL(sum(b.kamt),'0.00') = '' then '0.00' else sum(b.kamt) end as kreamt,case when ISNULL(sum(b.damt),'0.00') = '' then '0.00' else sum(b.damt) end as debamt,((case when ISNULL(sum(b.kamt),'0.00') = '' then '0.00' else sum(b.kamt) end) -  case when ISNULL(sum(b.damt),'0.00') = '' then '0.00' else sum(b.damt) end) as jumlah,((case when ISNULL(sum(b1.kamt),'0.00') = '' then '0.00' else sum(b1.kamt) end) -  case when ISNULL(sum(b1.damt),'0.00') = '' then '0.00' else sum(b1.damt) end) as jumlah1 from (select * from KW_Ref_Bajet where ref_bjt_year = '"+ tah_kewangan.SelectedValue +"') as a left join (select * from KW_Ref_Bajet where ref_bjt_year = '" + pre_year + "') as a1 on a1.Ref_kat_bajet=a.Ref_kat_bajet left join (select kod_akaun,sum(cast(KW_Debit_amt as money)) as damt,sum(cast(KW_kredit_amt as money)) as kamt,GL_process_dt from KW_General_Ledger  group by kod_akaun,GL_process_dt) as b on b.kod_akaun=a.Ref_kod_akaun and year(b.GL_post_dt) = '"+ tah_kewangan.SelectedValue + "' left join(select * from KW_Opening_Balance where set_sts='1' and opening_year = '" + tah_kewangan.SelectedValue + "') as c on c.kod_akaun=a.Ref_kod_akaun left join (select kod_akaun,sum(cast(KW_Debit_amt as money)) as damt,sum(cast(KW_kredit_amt as money)) as kamt,GL_process_dt from KW_General_Ledger  group by kod_akaun,GL_process_dt) as b1 on b1.kod_akaun=a.Ref_kod_akaun and year(b1.GL_post_dt) = '"+ prev_yr +"' left join(select * from KW_Opening_Balance where set_sts='1' and opening_year= '" + pre_year + "') as c1 on c1.kod_akaun=a.Ref_kod_akaun left join KW_Ref_Carta_Akaun ss1 on ss1.kod_akaun=a.Ref_kod_akaun group by a.Ref_kat_bajet,a.Ref_jumlah_bajet,a1.Ref_jumlah_bajet,a.ref_kumpulan";
            }
            else
            {
                val6 = "select a.ref_kumpulan,a.Ref_kat_bajet,cast(ISNULL(replace(a.Ref_jumlah_bajet, ',',''),'0.00') as money) Ref_jumlah_bajet,cast(ISNULL(replace(a1.Ref_jumlah_bajet, ',',''),'0.00') as money) as Ref_jumlah_bajet1,case when ISNULL(sum(b.kamt),'0.00') = '' then '0.00' else sum(b.kamt) end as kreamt,case when ISNULL(sum(b.damt),'0.00') = '' then '0.00' else sum(b.damt) end as debamt,((case when ISNULL(sum(b.kamt),'0.00') = '' then '0.00' else sum(b.kamt) end) -  case when ISNULL(sum(b.damt),'0.00') = '' then '0.00' else sum(b.damt) end) as jumlah,((case when ISNULL(sum(b1.kamt),'0.00') = '' then '0.00' else sum(b1.kamt) end) -  case when ISNULL(sum(b1.damt),'0.00') = '' then '0.00' else sum(b1.damt) end) as jumlah1 from (select * from KW_Ref_Bajet where ref_bjt_year = '" + tah_kewangan.SelectedValue + "') as a left join (select * from KW_Ref_Bajet where ref_bjt_year = '" + pre_year + "') as a1 on a1.Ref_kat_bajet=a.Ref_kat_bajet left join (select kod_akaun,sum(cast(KW_Debit_amt as money)) as damt,sum(cast(KW_kredit_amt as money)) as kamt,GL_process_dt from KW_General_Ledger  group by kod_akaun,GL_process_dt) as b on b.kod_akaun=a.Ref_kod_akaun and year(b.GL_post_dt) = '" + tah_kewangan.SelectedValue + "' left join(select * from KW_Opening_Balance where set_sts='1' and opening_year = '" + tah_kewangan.SelectedValue + "') as c on c.kod_akaun=a.Ref_kod_akaun left join (select kod_akaun,sum(cast(KW_Debit_amt as money)) as damt,sum(cast(KW_kredit_amt as money)) as kamt,GL_process_dt from KW_General_Ledger  group by kod_akaun,GL_process_dt) as b1 on b1.kod_akaun=a.Ref_kod_akaun and year(b1.GL_post_dt) = '" + prev_yr + "' left join(select * from KW_Opening_Balance where set_sts='1' and opening_year='" + pre_year + "') as c1 on c1.kod_akaun=a.Ref_kod_akaun left join KW_Ref_Carta_Akaun ss1 on ss1.kod_akaun=a.Ref_kod_akaun group by a.Ref_kat_bajet,a.Ref_jumlah_bajet,a1.Ref_jumlah_bajet,a.ref_kumpulan";
            }
            dt = DBCon.Ora_Execute_table(val6);
            //dt1 = DBCon.Ora_Execute_table(val5);
            Rptviwerlejar.Reset();
            ds.Tables.Add(dt);
            //ds1.Tables.Add(dt1);

            List<DataRow> listResult = dt.AsEnumerable().ToList();
            listResult.Count();
            int countRow = 0;
            countRow = listResult.Count();



            Rptviwerlejar.LocalReport.DataSources.Clear();
            if (countRow != 0)
            {


                Rptviwerlejar.LocalReport.ReportPath = "Kewengan/KW_bajet.rdlc";
                ReportDataSource rds = new ReportDataSource("kwbajet", dt);
                //ReportDataSource rds1 = new ReportDataSource("kwpl1", dt1);
                ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("s1", ""),
                     new ReportParameter("s2", Convert.ToString(prev_yr)),
                     new ReportParameter("s3", ""),
                     new ReportParameter("s4", ""),
                     new ReportParameter("S5", "")
                          };

                Rptviwerlejar.LocalReport.SetParameters(rptParams);
                Rptviwerlejar.LocalReport.DataSources.Add(rds);
                //Rptviwerlejar.LocalReport.DataSources.Add(rds1);
                Rptviwerlejar.LocalReport.Refresh();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod tidak dijumpai. Sila Pastikan Semua Maklumat Dimasukkan Dengan Betul.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }


    protected void btn_reset(object sender, EventArgs e)
    {
        Response.Redirect("../kewengan/kw_lep_bajet.aspx");
    }


}