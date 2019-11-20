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

public partial class kw_penalysian_bank : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();

    string filename;
    float total = 0, total1 = 0;
    string level, userid;
    string Status = string.Empty;
    string qry1 = string.Empty, qry2 = string.Empty;
    string fmdate = string.Empty, fmonth = string.Empty, fyear = string.Empty, stdate = string.Empty, tmdate = string.Empty, sqry1 = string.Empty, sqry2 = string.Empty;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        //ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        //scriptManager.RegisterPostBackControl(this.Button3);
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;
                userid = Session["New"].ToString();
                //ver_id.Text = "0";
                bind_akaun();
                if (samp != "")
                {
                    txt_id.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    view_details();
                }
                BindData1();


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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('786','705','1806','824','52','787','1807','1808','1809','1810','818','1376','15','77','1812','1811')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());         
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());
            ps_lbl10.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl11.Text = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower());       
            ps_lbl12.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            Button5.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            //Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            //Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
           

        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    void view_details()
    {
        DataTable sel_kat_akn = new DataTable();
        sel_kat_akn = DBCon.Ora_Execute_table("select s2.*,Format(s2.tarikh_mtxn,'dd/MM/yyyy') as pen_mtxn,Format(s2.tarikh_atxn,'dd/MM/yyyy') as pen_atxn,s1.Ref_Nama_akaun from KW_Penyesuaian_bank as s2 left join KW_Ref_Akaun_bank s1 on s1.Ref_kod_akaun=Ref_bank_id where s2.Id='" + txt_id.Text + "'");
        if (sel_kat_akn.Rows.Count != 0)
        {
            edit_kakaun1.Attributes.Add("Style", "pointer-events:none;");
            edit_kakaun2.Attributes.Add("Style", "pointer-events:none;");
            TextBox2.Attributes.Add("Readonly", "Readonly");
            dd_bank.SelectedValue = sel_kat_akn.Rows[0]["Ref_bank_id"].ToString();
            TextBox3.Text = sel_kat_akn.Rows[0]["Ref_Nama_akaun"].ToString();
            TextBox7.Text = double.Parse(sel_kat_akn.Rows[0]["Ref_baki_penyata"].ToString()).ToString("C").Replace("$","").Replace("RM", "");
            TextBox11.Text = double.Parse(sel_kat_akn.Rows[0]["Ref_baki_akaun"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
            Tk_mula.Text = sel_kat_akn.Rows[0]["pen_mtxn"].ToString();
            Tk_akhir.Text = sel_kat_akn.Rows[0]["pen_atxn"].ToString();
            //zero_bal.Checked = true;
            //get_bankvalue();
            //get_totamt();
            BindData1();
        }
        
    }

    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {

        BindData1();
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        BindData1();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //System.Web.UI.WebControls.CheckBox tick_chk = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("drd_ckbox");
        //System.Web.UI.WebControls.Label lnk2 = (System.Web.UI.WebControls.Label)e.Row.FindControl("lbl_id");
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    DataTable get_value = new DataTable();
        //    get_value = DBCon.Ora_Execute_table("select * From KW_General_Ledger where Id='" + lnk2.Text + "'");
        //    LinkButton lnk_roe = (LinkButton)e.Row.FindControl("lnkView");
        //    if (get_value.Rows[0]["GL_sts"].ToString() == "T")
        //    {
        //        tick_chk.Checked = true;
        //    }
        //    else
        //    {
        //        tick_chk.Checked = false;
        //    }
        //}
    }

    protected void GridView1_RowDataBound_na(object sender, GridViewRowEventArgs e)
    {
        //System.Web.UI.WebControls.CheckBox tick_chk = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("drd_ckbox");
        //System.Web.UI.WebControls.Label lnk2 = (System.Web.UI.WebControls.Label)e.Row.FindControl("lbl_id");
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    DataTable get_value = new DataTable();
        //    get_value = DBCon.Ora_Execute_table("select * From KW_General_Ledger where Id='" + lnk2.Text + "'");
        //    LinkButton lnk_roe = (LinkButton)e.Row.FindControl("lnkView");
        //    if (get_value.Rows[0]["GL_sts"].ToString() == "T")
        //    {
        //        tick_chk.Checked = true;
        //    }
        //    else
        //    {
        //        tick_chk.Checked = false;
        //    }
        //}
    }
    protected void txt_bapbamt(object sender, EventArgs e)
    {
        get_totamt();
        BindData1();
    }

    void get_totamt()
    {
        if (TextBox4.Text != "" && TextBox5.Text != "")
        {
            decimal pel_amt = decimal.Parse(TextBox4.Text) - (decimal.Parse(TextBox1.Text.Replace("(", "-").Replace(")", "")));
            TextBox6.Text = double.Parse(Convert.ToString(pel_amt)).ToString("C").Replace("RM", "").Replace("$", "");
            TextBox6.Focus();
        }
    }

    protected void baki_txt_changed(object sender, EventArgs e)
    {
        BindData1();
        decimal perbazaan = decimal.Parse(TextBox7.Text) - decimal.Parse(TextBox8.Text.Replace("(", "-").Replace(")", ""));
        TextBox11.Text = perbazaan.ToString("C").Replace("RM","").Replace("$", "");
      
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)sender;
       
        if (chk.Checked == true)
        {
            int selRowIndex1 = ((GridViewRow)(((System.Web.UI.WebControls.CheckBox)sender).Parent.Parent)).RowIndex;
            System.Web.UI.WebControls.Label id = (System.Web.UI.WebControls.Label)GridView1.Rows[selRowIndex1].FindControl("lbl_id");

            string Updsql1 = "UPDATE KW_General_Ledger SET GL_bank_recon='Y' where kod_akaun='" + dd_bank.SelectedValue + "' and Id='"+ id.Text + "'";
            Status = DBCon.Ora_Execute_CommamdText(Updsql1);
            if (Status == "SUCCESS")
            {
                BindData1();
                BindData2();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Transaction Assigned Sucessfully.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
        }
       
    }

    protected void CheckBox1_CheckedChanged_na(object sender, EventArgs e)
    {
        System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)sender;

        if (chk.Checked == true)
        {
            int selRowIndex1 = ((GridViewRow)(((System.Web.UI.WebControls.CheckBox)sender).Parent.Parent)).RowIndex;
            System.Web.UI.WebControls.Label id = (System.Web.UI.WebControls.Label)GridView1.Rows[selRowIndex1].FindControl("lbl_id_na");

            string Updsql1 = "UPDATE KW_General_Ledger SET GL_bank_recon='' where kod_akaun='" + dd_bank.SelectedValue + "' and Id='" + id.Text + "'";
            Status = DBCon.Ora_Execute_CommamdText(Updsql1);
            if (Status == "SUCCESS")
            {
                BindData1();
                BindData2();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Transaction Assigned Sucessfully.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
        }

    }

    //protected void clk_prnt(object sender, EventArgs e)
    //{
    //    if (Tk_mula.Text != "" && Tk_akhir.Text != "" && TextBox4.Text != "")
    //    {
    //        DataSet ds = new DataSet();
    //        DataSet ds1 = new DataSet();
    //        DataTable dt = new DataTable();
    //        DataTable dt1 = new DataTable();
    //        string val1 = string.Empty, val2 = string.Empty, val3 = string.Empty, val4 = string.Empty, val5 = string.Empty, val6 = string.Empty;

    //        string fmdate = string.Empty, fmonth = string.Empty, fyear = string.Empty, stdate = string.Empty, tmdate = string.Empty;
    //        if (Tk_mula.Text != "")
    //        {
    //            string fdate = Tk_mula.Text;
    //            DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
    //            fmdate = fd.ToString("yyyy-MM-dd");
    //            fmonth = fd.ToString("MM");
    //            fyear = fd.ToString("yyyy");
    //        }
    //        if (Tk_akhir.Text != "")
    //        {
    //            string tdate = Tk_akhir.Text;
    //            DateTime td = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
    //            tmdate = td.ToString("yyyy-MM-dd");
    //        }
    //        int min_val = 1;
    //        int curr_yr = Int32.Parse(DateTime.Now.Year.ToString());
    //        int prev_yr = (Int32.Parse(fyear) - min_val);
    //        if (Tk_mula.Text != "" && Tk_akhir.Text != "")
    //        {
    //            if (zero_bal.Checked == false)
    //            {
    //                val6 = "select * from (select case when KW_Debit_amt = '0.00' then 'K' when KW_kredit_amt = '0.00' then 'D' end as type,KW_Debit_amt,KW_kredit_amt,GL_invois_no,ref2,Format(GL_process_dt,'dd/MM/yyyy') as inv_dt,GL_desc1,GL_desc2,GL_sts,kod_akaun,GL_process_dt from KW_General_Ledger) as a  where a.GL_sts='A' and a.kod_akaun='" + dd_bank.SelectedValue + "' and a.GL_process_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and a.GL_process_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) order by a.type";
    //            }
    //            else
    //            {
    //                val6 = "select * from (select case when KW_Debit_amt = '0.00' then 'K' when KW_kredit_amt = '0.00' then 'D' end as type,KW_Debit_amt,KW_kredit_amt,GL_invois_no,ref2,Format(GL_process_dt,'dd/MM/yyyy') as inv_dt,GL_desc1,GL_desc2,GL_sts,kod_akaun,GL_process_dt from KW_General_Ledger) as a  where a.GL_sts='A' and a.kod_akaun='" + dd_bank.SelectedValue + "' and a.GL_process_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and a.GL_process_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) order by a.type";
    //            }
    //        }
    //        else
    //        {
    //            if (zero_bal.Checked == false)
    //            {
    //                val6 = "select * from (select case when KW_Debit_amt = '0.00' then 'K' when KW_kredit_amt = '0.00' then 'D' end as type,KW_Debit_amt,KW_kredit_amt,GL_invois_no,ref2,Format(GL_process_dt,'dd/MM/yyyy') as inv_dt,GL_desc1,GL_desc2,GL_sts,kod_akaun,GL_process_dt from KW_General_Ledger) as a where a.GL_sts='A' and a.kod_akaun='" + dd_bank.SelectedValue + "' and a.GL_process_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and a.GL_process_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) order by a.type";
    //            }
    //            else
    //            {
    //                val6 = "select * from (select case when KW_Debit_amt = '0.00' then 'K' when KW_kredit_amt = '0.00' then 'D' end as type,KW_Debit_amt,KW_kredit_amt,GL_invois_no,ref2,Format(GL_process_dt,'dd/MM/yyyy') as inv_dt,GL_desc1,GL_desc2,GL_sts,kod_akaun,GL_process_dt from KW_General_Ledger) as a where a.GL_sts='A' and a.kod_akaun='" + dd_bank.SelectedValue + "' and a.GL_process_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and a.GL_process_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) order by a.type";
    //            }
    //        }


    //        dt = DBCon.Ora_Execute_table(val6);
    //        //dt1 = DBCon.Ora_Execute_table(val5);
    //        Rptviwerlejar.Reset();
    //        ds.Tables.Add(dt);
    //        //ds1.Tables.Add(dt1);

    //        List<DataRow> listResult = dt.AsEnumerable().ToList();
    //        listResult.Count();
    //        int countRow = 0;
    //        countRow = listResult.Count();



    //        Rptviwerlejar.LocalReport.DataSources.Clear();
    //        if (TextBox5.Text != "")
    //        {
    //            DataTable sel_gst1 = new DataTable();
    //            sel_gst1 = DBCon.Ora_Execute_table(val5);

    //            if (sel_gst1.Rows.Count != 0)
    //            {
    //                val2 = sel_gst1.Rows[0]["val1"].ToString();
    //                val3 = sel_gst1.Rows[0]["val2"].ToString();
    //            }
    //            else
    //            {
    //                val2 = "0.00";
    //                val3 = "0.00";
    //            }

    //            Rptviwerlejar.LocalReport.ReportPath = "kewengan/KW_lap_penyalisan_bank.rdlc";
    //            ReportDataSource rds = new ReportDataSource("kwbank", dt);
    //            //ReportDataSource rds1 = new ReportDataSource("kwpl1", dt1);
    //            ReportParameter[] rptParams = new ReportParameter[]{
    //                 new ReportParameter("d1", dd_bank.SelectedValue),
    //                 new ReportParameter("d2", TextBox3.Text),
    //                 new ReportParameter("d3", TextBox2.Text),
    //                 new ReportParameter("d4", val3),
    //                 new ReportParameter("d5", TextBox5.Text),
    //                 new ReportParameter("d6", countRow.ToString())
    //                      };

    //            Rptviwerlejar.LocalReport.SetParameters(rptParams);
    //            Rptviwerlejar.LocalReport.DataSources.Add(rds);
    //            //Rptviwerlejar.LocalReport.DataSources.Add(rds1);
    //            Rptviwerlejar.LocalReport.Refresh();
    //            filename = string.Format("{0}.{1}", "Reconcilation_Report_" + DateTime.Now.ToString("ddMMyyyy") + "", "pdf");
    //            //}
    //            Warning[] warnings;
    //            string[] streamids;
    //            string mimeType;
    //            string encoding;
    //            string extension;

    //            byte[] bytes = Rptviwerlejar.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
    //            Response.Buffer = true;
    //            Response.Clear();
    //            Response.ContentType = mimeType;
    //            Response.AddHeader("content-disposition", "attachment; filename=" + filename);
    //            Response.BinaryWrite(bytes);
    //            Response.Flush();
    //            Response.End();
    //        }
    //        else
    //        {
    //            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Rekod tidak dijumpai. Sila Pastikan Semua Maklumat Dimasukkan Dengan Betul');", true);
    //        }
    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Sila Masukan Input Carian.');", true);
    //    }
    //}
    protected void sel_bank(object sender, EventArgs e)
    {
        DataTable sel_kat = new DataTable();
        sel_kat = DBCon.Ora_Execute_table("select * from KW_Ref_Akaun_bank where Ref_kod_akaun = '" + dd_bank.SelectedValue + "' and status= 'A'");
        if (sel_kat.Rows.Count != 0)
        {          
            TextBox3.Text = sel_kat.Rows[0]["Ref_nama_bank"].ToString();
            TextBox6.Text = "";
            TextBox4.Text = "";
        }
        BindData1();
    }
    void bind_akaun()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select Id,Ref_kod_akaun,(Ref_kod_akaun + ' | ' + case when LEN(Ref_nama_akaun) >= '50' then SUBSTRING ( Ref_nama_akaun ,1 , 50)+ ' ...'  else  Ref_nama_akaun end) Ref_nama_akaun from KW_Ref_Akaun_bank where Status='A' and Ref_kod_akaun != ''";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_bank.DataSource = dt;
            dd_bank.DataTextField = "Ref_nama_akaun";
            dd_bank.DataValueField = "Ref_kod_akaun";
            dd_bank.DataBind();
            dd_bank.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void clk_submit(object sender, EventArgs e)
    {

        DataTable sel_kat = new DataTable();
        sel_kat = DBCon.Ora_Execute_table("select * from KW_Ref_Akaun_bank where Ref_kod_akaun = '" + dd_bank.SelectedValue + "'");
        if (sel_kat.Rows.Count != 0)
        {
            TextBox3.Text = sel_kat.Rows[0]["Ref_nama_bank"].ToString();
            get_bankvalue();
        }
        BindData1();
    }

    void get_bankvalue()
    {
        //if (Tk_mula.Text != "" && Tk_akhir.Text != "")
        //{
        //        string fdate = Tk_mula.Text;
        //        DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //        fmdate = fd.ToString("yyyy-MM-dd");
        //        fmonth = fd.ToString("MM");
        //        fyear = fd.ToString("yyyy");
        //        stdate = fyear + "-01-01";
        //        string tdate = Tk_akhir.Text;
        //        DateTime td = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //        tmdate = td.ToString("yyyy-MM-dd");


        //    if (zero_bal.Checked == false)
        //    {
        //        //sqry1 = "select a.kod_akaun,ISNULL(((ISNULL(a.opening_amt,'0.00') + ISNULL(b.deb_amt,'0.00')) - ISNULL(b.kre_amt,'0.00')) , '0.00') as BALB from  (select s1.kod_akaun,cast(opening_amt as money) as opening_amt from Kw_ref_carta_akaun s1 left join KW_Opening_Balance s2 on s2.kod_akaun=s1.kod_akaun and s2.set_sts='1' where s1.kod_akaun='" + dd_bank.SelectedValue + "') as a left join(select kod_akaun,cast(sum(KW_Debit_amt) as money) deb_amt,cast(sum(KW_kredit_amt) as money) kre_amt from KW_General_Ledger where GL_sts='A' and YEAR(GL_process_dt) = '" + fyear + "' and GL_process_dt>=DATEADD(day, DATEDIFF(day, 0, '" + stdate + "'), 0) and GL_process_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by kod_akaun) as b on b.kod_akaun=a.kod_akaun ";
        //        sqry1 = "select a.kod_akaun,ISNULL(((ISNULL(a.opening_amt,'0.00'))) , '0.00') as BALB from  (select s1.kod_akaun,cast(opening_amt as money) as opening_amt from Kw_ref_carta_akaun s1 left join KW_Opening_Balance s2 on s2.kod_akaun=s1.kod_akaun and s2.set_sts='1' where s1.kod_akaun='" + dd_bank.SelectedValue + "' and '" + tmdate + "' between start_dt and end_dt) as a left join(select kod_akaun,cast(sum(KW_Debit_amt) as money) deb_amt,cast(sum(KW_kredit_amt) as money) kre_amt from KW_General_Ledger where GL_sts='A' and YEAR(GL_process_dt) = '" + fyear + "' and GL_process_dt>=DATEADD(day, DATEDIFF(day, 0, '" + stdate + "'), 0) and GL_process_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by kod_akaun) as b on b.kod_akaun=a.kod_akaun ";
        //    }
        //    else
        //    {
        //        //sqry1 = "select a.kod_akaun,ISNULL(((ISNULL(a.opening_amt,'0.00') + ISNULL(b.deb_amt,'0.00')) - ISNULL(b.kre_amt,'0.00')) , '0.00') as BALB from  (select s1.kod_akaun,cast(opening_amt as money) as opening_amt from Kw_ref_carta_akaun s1 left join KW_Opening_Balance s2 on s2.kod_akaun=s1.kod_akaun and s2.set_sts='1' where s1.kod_akaun='" + dd_bank.SelectedValue + "' ) as a left join(select kod_akaun,cast(sum(KW_Debit_amt) as money) deb_amt,cast(sum(KW_kredit_amt) as money) kre_amt from KW_General_Ledger where GL_sts='T' and YEAR(GL_process_dt) = '" + fyear + "' and GL_process_dt>=DATEADD(day, DATEDIFF(day, 0, '" + stdate + "'), 0) and GL_process_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by kod_akaun) as b on b.kod_akaun=a.kod_akaun ";
        //        sqry1 = "select a.kod_akaun,ISNULL(((ISNULL(a.opening_amt,'0.00'))) , '0.00') as BALB from  (select s1.kod_akaun,cast(opening_amt as money) as opening_amt from Kw_ref_carta_akaun s1 left join KW_Opening_Balance s2 on s2.kod_akaun=s1.kod_akaun and s2.set_sts='1' where s1.kod_akaun='" + dd_bank.SelectedValue + "' and '" + tmdate + "' between start_dt and end_dt) as a left join(select kod_akaun,cast(sum(KW_Debit_amt) as money) deb_amt,cast(sum(KW_kredit_amt) as money) kre_amt from KW_General_Ledger where GL_sts='T' and YEAR(GL_process_dt) = '" + fyear + "' and GL_process_dt>=DATEADD(day, DATEDIFF(day, 0, '" + stdate + "'), 0) and GL_process_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by kod_akaun) as b on b.kod_akaun=a.kod_akaun ";
        //    }
        //}
        //else
        //{
        //    if (zero_bal.Checked == false)
        //    {
        //        //sqry1 = "select a.kod_akaun,ISNULL(((ISNULL(a.opening_amt,'0.00') + ISNULL(b.deb_amt,'0.00')) - ISNULL(b.kre_amt,'0.00')) , '0.00') as BALB from  (select s1.kod_akaun,cast(opening_amt as money) as opening_amt from Kw_ref_carta_akaun s1 left join KW_Opening_Balance s2 on s2.kod_akaun=s1.kod_akaun and s2.set_sts='1' where s1.kod_akaun='" + dd_bank.SelectedValue + "') as a left join(select kod_akaun,cast(sum(KW_Debit_amt) as money) deb_amt,cast(sum(KW_kredit_amt) as money) kre_amt from KW_General_Ledger where GL_sts='A' and YEAR(GL_process_dt) = '" + fyear + "' and GL_process_dt>=DATEADD(day, DATEDIFF(day, 0, '" + stdate + "'), 0) and GL_process_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by kod_akaun) as b on b.kod_akaun=a.kod_akaun ";
        //        sqry1 = "select a.kod_akaun,ISNULL(((ISNULL(a.opening_amt,'0.00'))) , '0.00') as BALB from  (select s1.kod_akaun,cast(opening_amt as money) as opening_amt from Kw_ref_carta_akaun s1 left join KW_Opening_Balance s2 on s2.kod_akaun=s1.kod_akaun and s2.set_sts='1' where s1.kod_akaun='" + dd_bank.SelectedValue + "' and '" + tmdate + "' between start_dt and end_dt) as a left join(select kod_akaun,cast(sum(KW_Debit_amt) as money) deb_amt,cast(sum(KW_kredit_amt) as money) kre_amt from KW_General_Ledger where GL_sts='A' and YEAR(GL_process_dt) = '" + fyear + "' and GL_process_dt>=DATEADD(day, DATEDIFF(day, 0, '" + stdate + "'), 0) and GL_process_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by kod_akaun) as b on b.kod_akaun=a.kod_akaun ";
        //    }
        //    else
        //    {
        //        //sqry1 = "select a.kod_akaun,ISNULL(((ISNULL(a.opening_amt,'0.00') + ISNULL(b.deb_amt,'0.00')) - ISNULL(b.kre_amt,'0.00')) , '0.00') as BALB from  (select s1.kod_akaun,cast(opening_amt as money) as opening_amt from Kw_ref_carta_akaun s1 left join KW_Opening_Balance s2 on s2.kod_akaun=s1.kod_akaun and s2.set_sts='1' where s1.kod_akaun='" + dd_bank.SelectedValue + "') as a left join(select kod_akaun,cast(sum(KW_Debit_amt) as money) deb_amt,cast(sum(KW_kredit_amt) as money) kre_amt from KW_General_Ledger where GL_sts='T' and YEAR(GL_process_dt) = '" + fyear + "' and GL_process_dt>=DATEADD(day, DATEDIFF(day, 0, '" + stdate + "'), 0) and GL_process_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by kod_akaun) as b on b.kod_akaun=a.kod_akaun ";
        //        sqry1 = "select a.kod_akaun,ISNULL(((ISNULL(a.opening_amt,'0.00'))) , '0.00') as BALB from  (select s1.kod_akaun,cast(opening_amt as money) as opening_amt from Kw_ref_carta_akaun s1 left join KW_Opening_Balance s2 on s2.kod_akaun=s1.kod_akaun and s2.set_sts='1' where s1.kod_akaun='" + dd_bank.SelectedValue + "' and '" + tmdate + "' between start_dt and end_dt) as a left join(select kod_akaun,cast(sum(KW_Debit_amt) as money) deb_amt,cast(sum(KW_kredit_amt) as money) kre_amt from KW_General_Ledger where GL_sts='T' and YEAR(GL_process_dt) = '" + fyear + "' and GL_process_dt>=DATEADD(day, DATEDIFF(day, 0, '" + stdate + "'), 0) and GL_process_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by kod_akaun) as b on b.kod_akaun=a.kod_akaun ";
        //    }
        //}
        //DataTable ss1_val = new DataTable();
        //ss1_val = DBCon.Ora_Execute_table(sqry1);
       
        ////TextBox6.Text = "";
        ////TextBox4.Text = "";
        //if (ss1_val.Rows.Count != 0)
        //{
        //    TextBox5.Text = double.Parse(ss1_val.Rows[0]["BALB"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
        //    TextBox1.Text = double.Parse(ss1_val.Rows[0]["BALB"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
        //}
        //else
        //{
        //    TextBox5.Text = "0.00";
        //    TextBox1.Text = "0.00";
        //}
    }

    protected void clk_sahkan(object sender, EventArgs e)
    {
        if (TextBox7.Text != "" && Tk_mula.Text != "" && Tk_akhir.Text != "" && dd_bank.SelectedValue != "")
        {
            string fdate = Tk_mula.Text;
            DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            fmdate = fd.ToString("yyyy-MM-dd");
            string tdate = Tk_akhir.Text;
            DateTime td = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            tmdate = td.ToString("yyyy-MM-dd");

            DataTable sel_kat = new DataTable();
            sel_kat = DBCon.Ora_Execute_table("select * from KW_Penyesuaian_bank where Ref_bank_id = '" + dd_bank.SelectedValue + "' and tarikh_mtxn='" + tmdate + "' and Status='A'");
            if (sel_kat.Rows.Count == 0)
            {

                string Inssql_ins = "insert into KW_Penyesuaian_bank (Ref_bank_id,tarikh_mtxn,tarikh_atxn,Ref_baki_penyata,Ref_baki_akaun,Status,crt_id,cr_dt) VALUES ('" + dd_bank.SelectedValue + "','" + fmdate + "','" + tmdate + "','" + TextBox7.Text.Replace("(","").Replace(")", "") + "','" + TextBox11.Text.Replace("(", "").Replace(")", "") + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                Status = DBCon.Ora_Execute_CommamdText(Inssql_ins);
                if (Status == "SUCCESS")
                {
                    Button4.Text = "Kemaskini";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                  
                }
            }
            else
            {
                string Inssql_upd = "Update KW_Penyesuaian_bank set Ref_baki_penyata='" + TextBox7.Text + "',Ref_baki_akaun='" + TextBox11.Text + "',upd_id='" + Session["New"].ToString() + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Id='" + txt_id.Text + "'";
                Status = DBCon.Ora_Execute_CommamdText(Inssql_upd);
                if (Status == "SUCCESS")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);                    
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
        BindData1();        
    }

    protected void BindData1()
    {
        string srch_txt1 = string.Empty, srch_txt2 = string.Empty;
        if(txtSearch.Text != "")
        {
            srch_txt1 = "where a.ket like '%"+ txtSearch.Text + "%'";
            srch_txt2 = "and GL_desc1 like '%" + txtSearch.Text + "%'";
        }

        if (Tk_mula.Text != "")
        {
            string fdate = Tk_mula.Text;
            DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            fmdate = fd.ToString("yyyy-MM-dd");
            fyear = fd.ToString("yyyy");
        }
        if (Tk_akhir.Text != "")
        {
            string tdate = Tk_akhir.Text;
            DateTime td = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            tmdate = td.ToString("yyyy-MM-dd");
        }

        if (Tk_mula.Text != "" && Tk_akhir.Text != "")
        {
           
                sqry1 = "select a.*,case when a.ref2 ='02' then c.no_cek when a.ref2 ='04' then b.no_cek end as cek  from( "
                + " select id,GL_invois_no,ref2,Format(gl_post_dt,'dd/MM/yyyy') pos_dt,GL_desc1 as ket,kw_debit_amt as resit,KW_kredit_amt as pv from KW_General_Ledger  "
                + " where kod_akaun='" + dd_bank.SelectedValue + "' and ISNULL(GL_bank_recon,'')='' and gl_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and gl_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) "
                + " ) as a outer apply (Select No_cek FROM KW_Pembayaran_Pay_voucer where no_invois= a.GL_invois_no) as b outer apply (Select no_cek FROM KW_Penerimaan_resit "
                + " where no_invois= a.GL_invois_no) as c "+ srch_txt1 + "";

            sqry2 = "select *,(a.baki_txi - b.jum_txi) per_amt from (select ISNULL((sum(ISNULL(KW_Debit_amt,'0.00')) - sum(ISNULL(KW_kredit_amt,'0.00'))),'0.00') as baki_txi from KW_General_Ledger  where kod_akaun='" + dd_bank.SelectedValue + "' and ISNULL(GL_bank_recon,'')='' and gl_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and gl_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) ) as a outer apply (select ISNULL((sum(ISNULL(KW_Debit_amt,'0.00')) - sum(ISNULL(KW_kredit_amt,'0.00'))) ,'0.00') as jum_txi from KW_General_Ledger  where kod_akaun='" + dd_bank.SelectedValue + "' and ISNULL(GL_bank_recon,'')='Y' and gl_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and gl_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) " + srch_txt2 + ") as b";


        }
        else
        {

            sqry1 = "select a.*,case when a.ref2 ='02' then c.no_cek when a.ref2 ='04' then b.no_cek end as cek  from( "
            + " select id,GL_invois_no,ref2,Format(gl_post_dt,'dd/MM/yyyy') pos_dt,GL_desc1 as ket,kw_debit_amt as resit,KW_kredit_amt as pv from KW_General_Ledger  "
            + " where kod_akaun='"+ dd_bank.SelectedValue + "' and ISNULL(GL_bank_recon,'')='') as a outer apply (Select No_cek FROM KW_Pembayaran_Pay_voucer where no_invois= a.GL_invois_no) as b outer apply (Select no_cek FROM KW_Penerimaan_resit "
            + " where no_invois= a.GL_invois_no) as c " + srch_txt1 + "";

            sqry2 = "select *,(a.baki_txi - b.jum_txi) per_amt from (select ISNULL((sum(ISNULL(KW_Debit_amt,'0.00')) - sum(ISNULL(KW_kredit_amt,'0.00'))),'0.00') as baki_txi from KW_General_Ledger  where kod_akaun='" + dd_bank.SelectedValue + "' and ISNULL(GL_bank_recon,'')='' ) as a outer apply (select ISNULL((sum(ISNULL(KW_Debit_amt,'0.00')) - sum(ISNULL(KW_kredit_amt,'0.00'))) ,'0.00') as jum_txi from KW_General_Ledger  where kod_akaun='" + dd_bank.SelectedValue + "' and ISNULL(GL_bank_recon,'')='Y' "+ srch_txt2 + " ) as b";

        }


        SqlCommand cmd2 = new SqlCommand("" + sqry1 + "", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            GridView1.DataSource = ds2;
            GridView1.DataBind();
            int columncount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView1.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
            //GridView1.Visible = false;
        }
        else
        {
            //GridView1.Visible = true;
            GridView1.DataSource = ds2;
            GridView1.DataBind();
          
        }
        BindData2();
    }


    protected void BindData2()
    {
       
        if (Tk_mula.Text != "")
        {
            string fdate = Tk_mula.Text;
            DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            fmdate = fd.ToString("yyyy-MM-dd");
            fyear = fd.ToString("yyyy");
        }
        if (Tk_akhir.Text != "")
        {
            string tdate = Tk_akhir.Text;
            DateTime td = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            tmdate = td.ToString("yyyy-MM-dd");
        }

        if (Tk_mula.Text != "" && Tk_akhir.Text != "")
        {

            sqry1 = "select a.*,case when a.ref2 ='02' then c.no_cek when a.ref2 ='04' then b.no_cek end as cek  from( "
            + " select id,GL_invois_no,ref2,Format(gl_post_dt,'dd/MM/yyyy') pos_dt,GL_desc1 as ket,kw_debit_amt as resit,KW_kredit_amt as pv from KW_General_Ledger  "
            + " where kod_akaun='" + dd_bank.SelectedValue + "' and ISNULL(GL_bank_recon,'')='Y' and gl_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and gl_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) "
            + " ) as a outer apply (Select No_cek FROM KW_Pembayaran_Pay_voucer where no_invois= a.GL_invois_no) as b outer apply (Select no_cek FROM KW_Penerimaan_resit "
            + " where no_invois= a.GL_invois_no) as c";

        }
        else
        {

            sqry1 = "select a.*,case when a.ref2 ='02' then c.no_cek when a.ref2 ='04' then b.no_cek end as cek  from( "
            + " select id,GL_invois_no,ref2,Format(gl_post_dt,'dd/MM/yyyy') pos_dt,GL_desc1 as ket,kw_debit_amt as resit,KW_kredit_amt as pv from KW_General_Ledger  "
            + " where kod_akaun='" + dd_bank.SelectedValue + "' and ISNULL(GL_bank_recon,'')='Y') as a outer apply (Select No_cek FROM KW_Pembayaran_Pay_voucer where no_invois= a.GL_invois_no) as b outer apply (Select no_cek FROM KW_Penerimaan_resit "
            + " where no_invois= a.GL_invois_no) as c";

        }


        SqlCommand cmd2 = new SqlCommand("" + sqry1 + "", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count == 0)
        {
            Button4.Visible = false;
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            GridView2.DataSource = ds2;
            GridView2.DataBind();
            int columncount = GridView2.Rows[0].Cells.Count;
            GridView2.Rows[0].Cells.Clear();
            GridView2.Rows[0].Cells.Add(new TableCell());
            GridView2.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView2.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
            //GridView1.Visible = false;
        }
        else
        {
            Button4.Visible = true;
            //GridView1.Visible = true;
            GridView2.DataSource = ds2;
            GridView2.DataBind();

        }
        get_jumlah();
    }

    void get_jumlah()
    {
        DataTable dd2 = new DataTable();
        dd2 = DBCon.Ora_Execute_table(sqry2);

        if (dd2.Rows.Count != 0)
        {
            TextBox8.Text = double.Parse(dd2.Rows[0]["jum_txi"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
            TextBox11.Text = double.Parse(dd2.Rows[0]["per_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
            TextBox9.Text = double.Parse(dd2.Rows[0]["baki_txi"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
        }
    }
    protected void OnCheckedChanged(object sender, EventArgs e)
    {
        
        System.Web.UI.WebControls.CheckBox chk = (sender as System.Web.UI.WebControls.CheckBox);
        if (chk.ID == "chkAll")
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    row.Cells[6].Controls.OfType<System.Web.UI.WebControls.CheckBox>().FirstOrDefault().Checked = chk.Checked;

                }
            }
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    System.Web.UI.WebControls.CheckBox rbn = (System.Web.UI.WebControls.CheckBox)row.FindControl("drd_ckbox");
                    if (rbn.Checked == true)
                    {
                            int RowIndex = row.RowIndex;
                            string varName1 = ((System.Web.UI.WebControls.Label)row.FindControl("lbl_id")).Text.ToString();
                          
                            string Updsql1 = "UPDATE KW_General_Ledger SET GL_bank_recon='Y' where kod_akaun='" + dd_bank.SelectedValue + "' and Id='" + varName1 + "'";
                            Status = DBCon.Ora_Execute_CommamdText(Updsql1);                      
                    }
                }
            }
            if (Status == "SUCCESS")
            {
                BindData1();                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Transaction Assigned Sucessfully.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
        }

    }

    protected void OnCheckedChanged_na(object sender, EventArgs e)
    {
        System.Web.UI.WebControls.CheckBox chk = (sender as System.Web.UI.WebControls.CheckBox);
        if (chk.ID == "chkAll_na")
        {
            foreach (GridViewRow row in GridView2.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    row.Cells[6].Controls.OfType<System.Web.UI.WebControls.CheckBox>().FirstOrDefault().Checked = chk.Checked;

                }
            }
            foreach (GridViewRow row in GridView2.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    System.Web.UI.WebControls.CheckBox rbn = (System.Web.UI.WebControls.CheckBox)row.FindControl("drd_ckbox_na");
                    if (rbn.Checked == true)
                    {
                        int RowIndex = row.RowIndex;
                        string varName1 = ((System.Web.UI.WebControls.Label)row.FindControl("lbl_id_na")).Text.ToString();

                        string Updsql1 = "UPDATE KW_General_Ledger SET GL_bank_recon='' where kod_akaun='" + dd_bank.SelectedValue + "' and Id='" + varName1 + "'";
                        Status = DBCon.Ora_Execute_CommamdText(Updsql1);
                    }
                }
            }
            if (Status == "SUCCESS")
            {
                BindData1();                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Transaction Cancelled Sucessfully.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
        }

    }
    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
        BindData1();
    }

    protected void gvSelected_PageIndexChanging_na(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        GridView2.DataBind();
        BindData2();
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../kewengan/kw_penalysian_bank.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../kewengan/kw_penalysian_bank_view.aspx");
    }

    
}