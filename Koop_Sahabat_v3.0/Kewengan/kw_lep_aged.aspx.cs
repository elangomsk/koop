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

public partial class kw_lep_aged : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    string qry1 = string.Empty, qry2 = string.Empty;
    string sqry1 = string.Empty, sqry2 = string.Empty;
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
                Tk_mula.Text = DateTime.Now.ToString("dd/MM/yyyy");
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('1842','705','1724','64','1843','121','15')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());        
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    protected void clk_submit(object sender, EventArgs e)
    {
        if (dd_type.SelectedValue != "")
        {
            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            string val1 = string.Empty, val2 = string.Empty, val3 = string.Empty, val4 = string.Empty, val5 = string.Empty, val6 = string.Empty;

            string fmdate = string.Empty, tmdate = string.Empty;
            if (Tk_mula.Text != "")
            {
                string fdate = Tk_mula.Text;
                DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                fmdate = fd.ToString("yyyy-MM-dd");
            }

            //if (Tk_akhir.Text != "")
            //{
            //    string tdate = Tk_akhir.Text;
            //    DateTime td = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //    tmdate = td.ToString("yyyy-MM-dd");
            //}
            int min_val = 1;
            int curr_yr = Int32.Parse(DateTime.Now.Year.ToString());
            int prev_yr = (Int32.Parse(DateTime.Now.Year.ToString()) - min_val);
            if (dd_type.SelectedValue == "01")
            {
                val1 = "RECEIVABLE";
                //val6 = "select a.untuk_akaun as v1,s1.nama_syarikat as v2,a.no_invois as v3,FORMAT(a.tarikh_invois,'dd/MM/yyyy', 'en-us') as v4, FORMAT(ISNULL(c.tarikh_Payment,''),'dd/MM/yyyy', 'en-us') as v4_1,a.Overall as v5, case when FORMAT(ISNULL(c.tarikh_Payment,''),'dd/MM/yyyy', 'en-us') = '01/01/1900' then DATEDIFF(day,FORMAT(a.tarikh_invois,'yyyy-MM-dd', 'en-us'),'"+ fmdate +"') else DATEDIFF(day,FORMAT(c.tarikh_Payment,'yyyy-MM-dd', 'en-us'),'"+ fmdate +"') end as diff_day, case when FORMAT(ISNULL(c.tarikh_Payment,''),'dd/MM/yyyy', 'en-us') ='01/01/1900' then (case when DATEDIFF(day,FORMAT(a.tarikh_invois,'yyyy-MM-dd', 'en-us'),'"+ fmdate +"') < '31'  then (a.Overall - case when ISNULL(b.pay_amt,'') ='' then '0.00' else b.pay_amt end) else '' end) else (case when DATEDIFF(day,FORMAT(c.tarikh_Payment,'yyyy-MM-dd', 'en-us'),'"+ fmdate +"') < '31'  then (a.Overall - case when ISNULL(b.pay_amt,'') ='' then '0.00' else b.pay_amt end) else '' end )end as v6,case when FORMAT(ISNULL(c.tarikh_Payment,''),'dd/MM/yyyy', 'en-us') ='01/01/1900' then ( case when DATEDIFF(day,FORMAT(a.tarikh_invois,'yyyy-MM-dd', 'en-us'),'"+ fmdate +"') > '31' and DATEDIFF(day,FORMAT(a.tarikh_invois,'yyyy-MM-dd', 'en-us'),'"+ fmdate +"') < '61'  then (a.Overall - case when ISNULL(b.pay_amt,'') ='' then '0.00' else b.pay_amt end) else '' end) else ( case when DATEDIFF(day,FORMAT(a.tarikh_invois,'yyyy-MM-dd', 'en-us'),'"+ fmdate +"') > '31' and DATEDIFF(day,FORMAT(c.tarikh_Payment,'yyyy-MM-dd', 'en-us'),'"+ fmdate +"') < '61'  then (a.Overall - case when ISNULL(b.pay_amt,'') ='' then '0.00' else b.pay_amt end) else '' end) end as v7, case when FORMAT(ISNULL(c.tarikh_Payment,''),'dd/MM/yyyy', 'en-us') ='01/01/1900' then (case when DATEDIFF(day,FORMAT(a.tarikh_invois,'yyyy-MM-dd', 'en-us'),'"+ fmdate +"') > '61' and  DATEDIFF(day,FORMAT(a.tarikh_invois,'yyyy-MM-dd', 'en-us'),'"+ fmdate +"') < '90'  then (a.Overall - case when ISNULL(b.pay_amt,'') = '' then '0.00' else b.pay_amt end) else '' end) else (case when DATEDIFF(day,FORMAT(c.tarikh_Payment,'yyyy-MM-dd', 'en-us'),'"+ fmdate +"') > '61' and  DATEDIFF(day,FORMAT(a.tarikh_invois,'yyyy-MM-dd', 'en-us'),'"+ fmdate +"') < '90'  then (a.Overall - case when ISNULL(b.pay_amt,'') = '' then '0.00' else b.pay_amt end) else '' end) end as v8,case when FORMAT(ISNULL(c.tarikh_Payment,''),'dd/MM/yyyy', 'en-us') ='01/01/1900' then( case when DATEDIFF(day,FORMAT(a.tarikh_invois,'yyyy-MM-dd', 'en-us'),'"+ fmdate +"') >= '90'  then (a.Overall - case when ISNULL(b.pay_amt,'') = '' then '0.00' else b.pay_amt end ) else '' end) else ( case when DATEDIFF(day,FORMAT(c.tarikh_Payment,'yyyy-MM-dd', 'en-us'),'"+ fmdate +"') >= '90'  then (a.Overall - case when ISNULL(b.pay_amt,'') = '' then '0.00' else b.pay_amt end ) else '' end) end as v9 from (select untuk_akaun,no_invois,tarikh_invois,Overall from KW_Penerimaan_invois) as a left join (select p1.no_invois,SUM(ISNULL(p1.jumlah,'0.00')) as pay_amt from KW_Penerimaan_payment p1 where p1.tarikh_Payment>=DATEADD(day, DATEDIFF(day, 0, '"+ DateTime.Now.Year +"-01-01'), 0) and p1.tarikh_Payment<=DATEADD(day, DATEDIFF(day, 0, '"+ fmdate +"'), +1) group by p1.no_invois) as b on b.no_invois=a.no_invois left join (select top(1) p1.no_invois,jumlah,tarikh_Payment from KW_Penerimaan_payment p1 where p1.tarikh_Payment>=DATEADD(day, DATEDIFF(day, 0, '"+ DateTime.Now.Year +"-01-01'), 0) and p1.tarikh_Payment<=DATEADD(day, DATEDIFF(day, 0, '"+ fmdate +"'), +1) order by tarikh_Payment desc) as c on c.no_invois=a.no_invois left join KW_Profile_syarikat s1 on s1.kod_syarikat=a.untuk_akaun";
                val6 = "select a.untuk_akaun as v1,s1.nama_syarikat as v2,a.no_invois as v3,FORMAT(a.tarikh_invois,'dd/MM/yyyy', 'en-us') as v4,a.Overall as v5, case when a.Overall = ISNULL(b.pay_amt,'') then '0.00' else a.Overall end as v5_1,DATEDIFF(day,FORMAT(a.tarikh_invois,'yyyy-MM-dd', 'en-us'),'" + fmdate + "') as diff_day, case when DATEDIFF(day,FORMAT(a.tarikh_invois,'yyyy-MM-dd', 'en-us'),'" + fmdate + "') < '31'  then (a.Overall - case when ISNULL(b.pay_amt,'') ='' then '0.00' else b.pay_amt end) else '' end as v6, case when DATEDIFF(day,FORMAT(a.tarikh_invois,'yyyy-MM-dd', 'en-us'),'" + fmdate + "') > '31' and DATEDIFF(day,FORMAT(a.tarikh_invois,'yyyy-MM-dd', 'en-us'),'" + fmdate + "') < '61'  then (a.Overall - case when ISNULL(b.pay_amt,'') ='' then '0.00' else b.pay_amt end) else '' end as v7, case when DATEDIFF(day,FORMAT(a.tarikh_invois,'yyyy-MM-dd', 'en-us'),'" + fmdate + "') > '61' and  DATEDIFF(day,FORMAT(a.tarikh_invois,'yyyy-MM-dd', 'en-us'),'" + fmdate + "') < '90'  then (a.Overall - case when ISNULL(b.pay_amt,'') = '' then '0.00' else b.pay_amt end) else '' end as v8, case when DATEDIFF(day,FORMAT(a.tarikh_invois,'yyyy-MM-dd', 'en-us'),'" + fmdate + "') >= '90'  then (a.Overall - case when ISNULL(b.pay_amt,'') = '' then '0.00' else b.pay_amt end ) else '' end as v9 from (select untuk_akaun,no_invois,tarikh_invois,Overall from KW_Penerimaan_invois) as a left join (select p1.no_invois,SUM(ISNULL(p1.jumlah,'0.00')) as pay_amt from KW_Penerimaan_payment p1 where p1.tarikh_Payment>=DATEADD(day, DATEDIFF(day, 0, '" + DateTime.Now.Year + "-01-01'), 0) and p1.tarikh_Payment<=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), +1) group by p1.no_invois) as b on b.no_invois=a.no_invois left join KW_Profile_syarikat s1 on s1.kod_syarikat=a.untuk_akaun where a.Overall != ISNULL(b.pay_amt,'')";

            }
            else
            {
                val1 = "PAYABLE";
                //val6 = "select a.untuk_akaun as v1,s1.nama_syarikat as v2,a.no_invois as v3,FORMAT(a.tarikh_invois,'dd/MM/yyyy', 'en-us') as v4, FORMAT(ISNULL(c.tarikh_Payment,''),'dd/MM/yyyy', 'en-us') as v4_1,a.Overall as v5, case when FORMAT(ISNULL(c.tarikh_Payment,''),'dd/MM/yyyy', 'en-us') = '01/01/1900' then DATEDIFF(day,FORMAT(a.tarikh_invois,'yyyy-MM-dd', 'en-us'),'" + fmdate + "') else DATEDIFF(day,FORMAT(c.tarikh_Payment,'yyyy-MM-dd', 'en-us'),'" + fmdate + "') end as diff_day, case when FORMAT(ISNULL(c.tarikh_Payment,''),'dd/MM/yyyy', 'en-us') ='01/01/1900' then (case when DATEDIFF(day,FORMAT(a.tarikh_invois,'yyyy-MM-dd', 'en-us'),'" + fmdate + "') < '31'  then (a.Overall - case when ISNULL(b.pay_amt,'') ='' then '0.00' else b.pay_amt end) else '' end) else (case when DATEDIFF(day,FORMAT(c.tarikh_Payment,'yyyy-MM-dd', 'en-us'),'" + fmdate + "') < '31'  then (a.Overall - case when ISNULL(b.pay_amt,'') ='' then '0.00' else b.pay_amt end) else '' end )end as v6,case when FORMAT(ISNULL(c.tarikh_Payment,''),'dd/MM/yyyy', 'en-us') ='01/01/1900' then ( case when DATEDIFF(day,FORMAT(a.tarikh_invois,'yyyy-MM-dd', 'en-us'),'" + fmdate + "') > '31' and DATEDIFF(day,FORMAT(a.tarikh_invois,'yyyy-MM-dd', 'en-us'),'" + fmdate + "') < '61'  then (a.Overall - case when ISNULL(b.pay_amt,'') ='' then '0.00' else b.pay_amt end) else '' end) else ( case when DATEDIFF(day,FORMAT(a.tarikh_invois,'yyyy-MM-dd', 'en-us'),'" + fmdate + "') > '31' and DATEDIFF(day,FORMAT(c.tarikh_Payment,'yyyy-MM-dd', 'en-us'),'" + fmdate + "') < '61'  then (a.Overall - case when ISNULL(b.pay_amt,'') ='' then '0.00' else b.pay_amt end) else '' end) end as v7, case when FORMAT(ISNULL(c.tarikh_Payment,''),'dd/MM/yyyy', 'en-us') ='01/01/1900' then (case when DATEDIFF(day,FORMAT(a.tarikh_invois,'yyyy-MM-dd', 'en-us'),'" + fmdate + "') > '61' and  DATEDIFF(day,FORMAT(a.tarikh_invois,'yyyy-MM-dd', 'en-us'),'" + fmdate + "') < '90'  then (a.Overall - case when ISNULL(b.pay_amt,'') = '' then '0.00' else b.pay_amt end) else '' end) else (case when DATEDIFF(day,FORMAT(c.tarikh_Payment,'yyyy-MM-dd', 'en-us'),'" + fmdate + "') > '61' and  DATEDIFF(day,FORMAT(a.tarikh_invois,'yyyy-MM-dd', 'en-us'),'" + fmdate + "') < '90'  then (a.Overall - case when ISNULL(b.pay_amt,'') = '' then '0.00' else b.pay_amt end) else '' end) end as v8,case when FORMAT(ISNULL(c.tarikh_Payment,''),'dd/MM/yyyy', 'en-us') ='01/01/1900' then( case when DATEDIFF(day,FORMAT(a.tarikh_invois,'yyyy-MM-dd', 'en-us'),'" + fmdate + "') >= '90'  then (a.Overall - case when ISNULL(b.pay_amt,'') = '' then '0.00' else b.pay_amt end ) else '' end) else ( case when DATEDIFF(day,FORMAT(c.tarikh_Payment,'yyyy-MM-dd', 'en-us'),'" + fmdate + "') >= '90'  then (a.Overall - case when ISNULL(b.pay_amt,'') = '' then '0.00' else b.pay_amt end ) else '' end) end as v9 from (select untuk_akaun,no_invois,tarikh_invois,Overall from KW_Penerimaan_invois) as a left join (select p1.no_invois,SUM(ISNULL(p1.jumlah,'0.00')) as pay_amt from KW_Penerimaan_payment p1 where p1.tarikh_Payment>=DATEADD(day, DATEDIFF(day, 0, '" + DateTime.Now.Year + "-01-01'), 0) and p1.tarikh_Payment<=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), +1) group by p1.no_invois) as b on b.no_invois=a.no_invois left join (select top(1) p1.no_invois,jumlah,tarikh_Payment from KW_Penerimaan_payment p1 where p1.tarikh_Payment>=DATEADD(day, DATEDIFF(day, 0, '" + DateTime.Now.Year + "-01-01'), 0) and p1.tarikh_Payment<=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), +1) order by tarikh_Payment desc) as c on c.no_invois=a.no_invois left join KW_Profile_syarikat s1 on s1.kod_syarikat=a.untuk_akaun";
                val6 = "select a.untuk_akaun as v1,s1.nama_syarikat as v2,a.no_invois as v3,FORMAT(a.tarkih_invois,'dd/MM/yyyy', 'en-us') as v4,a.Overall as v5, case when a.Overall = ISNULL(b.pay_amt,'') then '0.00' else a.Overall end as v5_1,DATEDIFF(day,FORMAT(a.tarkih_invois,'yyyy-MM-dd', 'en-us'),'" + fmdate + "') as diff_day, case when DATEDIFF(day,FORMAT(a.tarkih_invois,'yyyy-MM-dd', 'en-us'),'" + fmdate + "') < '31'  then (a.Overall - case when ISNULL(b.pay_amt,'') ='' then '0.00' else b.pay_amt end) else '' end as v6, case when DATEDIFF(day,FORMAT(a.tarkih_invois,'yyyy-MM-dd', 'en-us'),'" + fmdate + "') > '31' and DATEDIFF(day,FORMAT(a.tarkih_invois,'yyyy-MM-dd', 'en-us'),'" + fmdate + "') < '61'  then (a.Overall - case when ISNULL(b.pay_amt,'') ='' then '0.00' else b.pay_amt end) else '' end as v7, case when DATEDIFF(day,FORMAT(a.tarkih_invois,'yyyy-MM-dd', 'en-us'),'" + fmdate + "') > '61' and  DATEDIFF(day,FORMAT(a.tarkih_invois,'yyyy-MM-dd', 'en-us'),'" + fmdate + "') < '90'  then (a.Overall - case when ISNULL(b.pay_amt,'') = '' then '0.00' else b.pay_amt end) else '' end as v8, case when DATEDIFF(day,FORMAT(a.tarkih_invois,'yyyy-MM-dd', 'en-us'),'" + fmdate + "') >= '90'  then (a.Overall - case when ISNULL(b.pay_amt,'') = '' then '0.00' else b.pay_amt end ) else '' end as v9 from (select untuk_akaun,no_invois,tarkih_invois,Overall from KW_Pembayaran_invoisBil_item) as a left join (select p1.no_invois,SUM(ISNULL(p1.jumlah,'0.00')) as pay_amt from KW_Pembayaran_Pay_voucer p1 where p1.tarkih_pv>=DATEADD(day, DATEDIFF(day, 0, '" + DateTime.Now.Year + "-01-01'), 0) and p1.tarkih_pv<=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), +1) group by p1.no_invois) as b on b.no_invois=a.no_invois left join KW_Profile_syarikat s1 on s1.kod_syarikat=a.untuk_akaun where a.Overall != ISNULL(b.pay_amt,'')";
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
                //DataTable sel_gst1 = new DataTable();
                //sel_gst1 = DBCon.Ora_Execute_table(val5);

                Rptviwerlejar.LocalReport.ReportPath = "Kewengan/KW_receivable.rdlc";
                ReportDataSource rds = new ReportDataSource("KWreceivable", dt);
                //ReportDataSource rds1 = new ReportDataSource("kwpl1", dt1);
                ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("d1", val1),
                     new ReportParameter("d2", ""),
                     new ReportParameter("d3", ""),
                     new ReportParameter("d4", ""),
                     new ReportParameter("d5", "")
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
        Response.Redirect("../kewengan/kw_lep_aged.aspx");
    }


}