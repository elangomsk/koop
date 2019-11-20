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

public partial class kw_lep_gst_senerai : System.Web.UI.Page
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
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Button4);
        string script = " $(document).ready(function () { $(" + DropDownList2.ClientID + ").SumoSelect({ selectAll: true });});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
                bind_gst_type();
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('819','705','1724','64','65','820','121','15')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());        
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());

        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    void bind_gst_type()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select Ref_kod_cukai,Ref_nama_cukai from KW_Ref_Tetapan_cukai where Status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList2.DataSource = dt;
            DropDownList2.DataTextField = "Ref_nama_cukai";
            DropDownList2.DataValueField = "Ref_kod_cukai";
            DropDownList2.DataBind();
            //DropDownList2.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    protected void clk_submit(object sender, EventArgs e)
    {
        string rcount = string.Empty, rcount1 = string.Empty;
        int count = 0, count1 = 1;
        string ss1 = string.Empty;
        foreach (ListItem li in DropDownList2.Items)
        {
            if (li.Selected == true)
            {
                count++;
            }
            rcount = count.ToString();
        }
        string selectedValues = string.Empty;
        foreach (ListItem li in DropDownList2.Items)
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

        DataSet ds = new DataSet();
        DataSet ds1 = new DataSet();
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        string fmdate = string.Empty, tmdate = string.Empty;
        if (Tk_mula.Text != "")
        {
            string fdate = Tk_mula.Text;
            DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            fmdate = fd.ToString("yyyy-MM-dd");
        }
        if (Tk_akhir.Text != "")
        {
            string tdate = Tk_akhir.Text;
            DateTime td = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            tmdate = td.ToString("yyyy-MM-dd");
        }

        if (DropDownList2.SelectedValue != "" && fmdate == "" && tmdate == "")
        {
            sqry1 = "select a.GL_type as tname,FORMAT(ISNULL(a.GL_process_dt,''),'dd/MM/yyyy', 'en-us') as inv_dt,a.GL_invois_no as doc_no,s1.nama_akaun as com_name,s2.Ref_nama_cukai as descript,a.Tax_type as ttype,s2.Ref_kadar as trate,(a.tamt -a.gst_amt ) as Amt1,a.gst_amt as Amt2, a.tamt as Amt3 from (select Tax_type,GL_invois_no,case when KW_Debit_amt = '0.00' then KW_kredit_amt else KW_kredit_amt end as gst_amt,Tot_Amt as tamt,GL_process_dt,GL_type,GL_nama_kod from KW_General_Ledger  where Tax_type != '') as a left join KW_Ref_Carta_Akaun s1 on s1.kod_akaun=a.GL_nama_kod left join KW_Ref_Tetapan_cukai s2 on s2.Ref_kod_cukai=a.Tax_type where a.Tax_type='" + DropDownList2.SelectedValue + "' order by s2.Ref_kod_cukai";
        }
        else if (DropDownList2.SelectedValue == "" && fmdate != "" && tmdate != "")
        {
            sqry1 = "select a.GL_type as tname,FORMAT(ISNULL(a.GL_process_dt,''),'dd/MM/yyyy', 'en-us') as inv_dt,a.GL_invois_no as doc_no,s1.nama_akaun as com_name,s2.Ref_nama_cukai as descript,a.Tax_type as ttype,s2.Ref_kadar as trate,(a.tamt -a.gst_amt ) as Amt1,a.gst_amt as Amt2, a.tamt as Amt3 from (select Tax_type,GL_invois_no,case when KW_Debit_amt = '0.00' then KW_kredit_amt else KW_kredit_amt end as gst_amt,Tot_Amt as tamt,GL_process_dt,GL_type,GL_nama_kod from KW_General_Ledger  where Tax_type != '') as a left join KW_Ref_Carta_Akaun s1 on s1.kod_akaun=a.GL_nama_kod left join KW_Ref_Tetapan_cukai s2 on s2.Ref_kod_cukai=a.Tax_type where a.GL_process_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and a.GL_process_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) order by s2.Ref_kod_cukai";
        }
        else if (DropDownList2.SelectedValue != "" && fmdate != "" && tmdate != "")
        {

            sqry1 = "select a.GL_type as tname,FORMAT(ISNULL(a.GL_process_dt,''),'dd/MM/yyyy', 'en-us') as inv_dt,a.GL_invois_no as doc_no,s1.nama_akaun as com_name,s2.Ref_nama_cukai as descript,a.Tax_type as ttype,s2.Ref_kadar as trate,(a.tamt -a.gst_amt ) as Amt1,a.gst_amt as Amt2, a.tamt as Amt3 from (select Tax_type,GL_invois_no,case when KW_Debit_amt = '0.00' then KW_kredit_amt else KW_kredit_amt end as gst_amt,Tot_Amt as tamt,GL_process_dt,GL_type,GL_nama_kod from KW_General_Ledger  where Tax_type != '') as a left join KW_Ref_Carta_Akaun s1 on s1.kod_akaun=a.GL_nama_kod left join KW_Ref_Tetapan_cukai s2 on s2.Ref_kod_cukai=a.Tax_type where a.Tax_type='" + DropDownList2.SelectedValue + "' and a.GL_process_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and a.GL_process_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) order by s2.Ref_kod_cukai";
        }
        else
        {
            sqry1 = "select a.GL_type as tname,FORMAT(ISNULL(a.GL_process_dt,''),'dd/MM/yyyy', 'en-us') as inv_dt,a.GL_invois_no as doc_no,s1.nama_akaun as com_name,s2.Ref_nama_cukai as descript,a.Tax_type as ttype,s2.Ref_kadar as trate,(a.tamt -a.gst_amt ) as Amt1,a.gst_amt as Amt2, a.tamt as Amt3 from (select Tax_type,GL_invois_no,case when KW_Debit_amt = '0.00' then KW_kredit_amt else KW_kredit_amt end as gst_amt,Tot_Amt as tamt,GL_process_dt,GL_type,GL_nama_kod from KW_General_Ledger  where Tax_type != '') as a left join KW_Ref_Carta_Akaun s1 on s1.kod_akaun=a.GL_nama_kod left join KW_Ref_Tetapan_cukai s2 on s2.Ref_kod_cukai=a.Tax_type order by s2.Ref_kod_cukai";

        }

        dt = DBCon.Ora_Execute_table(sqry1);
        //dt1 = DBCon.Ora_Execute_table(sqry2);
        ds.Tables.Add(dt);
        //ds1.Tables.Add(dt1);

        string vs1 = string.Empty, vs2 = string.Empty, vs3 = string.Empty, vs4 = string.Empty, vs5 = string.Empty, vs6 = string.Empty;

        Rptviwerlejar.Reset();
        Rptviwerlejar.LocalReport.Refresh();
        List<DataRow> listResult = dt.AsEnumerable().ToList();
        listResult.Count();
        int countRow = 0;
        countRow = listResult.Count();



        Rptviwerlejar.LocalReport.DataSources.Clear();
        if (countRow != 0)
        {
            DataTable cnt_open = new DataTable();
            cnt_open = DBCon.Ora_Execute_table(sqry2);

            Rptviwerlejar.LocalReport.ReportPath = "Kewengan/KW_GST_sen.rdlc";
            ReportDataSource rds = new ReportDataSource("kwgstsen", dt);
            //ReportDataSource rds1 = new ReportDataSource("kwlegar1", dt1);
            ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("s1", Tk_mula.Text),
                     new ReportParameter("s2", Tk_akhir.Text),
                     new ReportParameter("s3", ""),
                     new ReportParameter("s4", ""),
                     new ReportParameter("S5", "")
                          };

            Rptviwerlejar.LocalReport.SetParameters(rptParams);
            Rptviwerlejar.LocalReport.DataSources.Add(rds);
            //Rptviwerlejar.LocalReport.DataSources.Add(rds1);
            Rptviwerlejar.LocalReport.DisplayName = "LEJAR_AM_" + DateTime.Now.ToString("yyyyMMdd");
            Rptviwerlejar.LocalReport.Refresh();

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod tidak dijumpai. Sila Pastikan Semua Maklumat Dimasukkan Dengan Betul.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }



    protected void btn_reset(object sender, EventArgs e)
    {
        Response.Redirect("../kewengan/kw_lep_gst_senerai.aspx");
    }


}