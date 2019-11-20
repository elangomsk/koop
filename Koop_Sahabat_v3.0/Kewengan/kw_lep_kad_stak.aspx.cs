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

public partial class kw_lep_kad_stak : System.Web.UI.Page
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
        string script = " $(document).ready(function () { $(" + dd_select2.ClientID + ").SumoSelect({ selectAll: true }); $('.select2').select2();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Button4);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
                BindData();
                month();
                bind_jenisbarangan();
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('1845','705','1724','64','65','793','121','15')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());       
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());

        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    void month()
    {
        //DataSet Ds = new DataSet();
        //try
        //{
        //    DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);

        //    for (int i = 1; i < 13; i++)
        //    {
        //        Tahun_kew.Items.Add(new ListItem(info.GetMonthName(i), i.ToString("00")));
        //    }

        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
    }
    void bind_jenisbarangan()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select DISTINCT UPPER(jenis_barang) as name,jenis_barang as id from KW_INVENTORI_BARANG where status= 'A' order by jenis_barang";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_select2.DataSource = dt;
            dd_select2.DataBind();
            dd_select2.DataTextField = "name";
            dd_select2.DataValueField = "id";
            dd_select2.DataBind();
            //dd_select2.Items.Insert(0, new ListItem("--- PILIH SEMUA ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void BindData()
    {

    }

    protected void clk_submit(object sender, EventArgs e)
    {
        if (Tk_mula.Text != "" && Tk_akhir.Text != "")
        {
            string rcount = string.Empty, rcount1 = string.Empty;
            int count = 0, count1 = 1, pyr = 0;
            string ss1 = string.Empty;
            foreach (ListItem li in dd_select2.Items)
            {
                if (li.Selected == true)
                {
                    count++;
                }
                rcount = count.ToString();
            }
            string selectedValues = string.Empty;
            foreach (ListItem li in dd_select2.Items)
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
            string val1 = string.Empty, val2 = string.Empty, val3 = string.Empty, val4 = string.Empty, val5 = string.Empty, val6 = string.Empty;

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
            //int min_val = 1;
            //int curr_yr = Int32.Parse(DateTime.Now.Year.ToString());
            //int sel_mnth = Int32.Parse(Tk_mula.Text);
            //int prev_yr = (Int32.Parse(DateTime.Now.Year.ToString()) - min_val);
            //string srdate = curr_yr + "-01-01";
            //string crdate = DateTime.Now.Year.ToString() + "-" + Tk_mula.Text + "-" + DateTime.DaysInMonth(curr_yr, sel_mnth);
            //string crdate1 = DateTime.DaysInMonth(curr_yr, sel_mnth) + "/" + Tk_mula.Text + "/" + DateTime.Now.Year.ToString();


            if (selectedValues != "")
            {
                val6 = "select m1.kod_barang,s1.jenis_barang,s1.nama_barang,FORMAT(m1.tarikh,'dd/MM/yyyy', 'en-us') as tarikh,m1.nama_syarikat,cast(m1.qty_masuk as int) qty_masuk,cast(m1.qty_keluar as int) qty_keluar,cast(m1.qty_baki as int) qty_baki,cast(m1.qty as int) qty,m1.harga_kos,m1.jumlah_kos,m1.seq_no from KW_inv_lep_kad_stok m1 left join KW_INVENTORI_BARANG s1 on s1.kod_barang=m1.kod_barang where m1.tarikh>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and m1.tarikh<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and s1.jenis_barang IN ('" + selectedValues.Replace(",", "','") + "') order by m1.kod_barang,m1.seq_no Asc";
                val5 = "select sum(jumlah_baki) as jum from KW_kemasukan_inventori where do_tarikh>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and do_tarikh<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and jenis_barang IN ('"+ selectedValues.Replace(",", "','") + "')";
            }
            else if (selectedValues == "")
            {
                val6 = "select m1.kod_barang,s1.jenis_barang,s1.nama_barang,FORMAT(m1.tarikh,'dd/MM/yyyy', 'en-us') as tarikh,m1.nama_syarikat,cast(m1.qty_masuk as int) qty_masuk,cast(m1.qty_keluar as int) qty_keluar,cast(m1.qty_baki as int) qty_baki,cast(m1.qty as int) qty,m1.harga_kos,m1.jumlah_kos,m1.seq_no from KW_inv_lep_kad_stok m1 left join KW_INVENTORI_BARANG s1 on s1.kod_barang=m1.kod_barang where m1.tarikh>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and m1.tarikh<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) order by m1.kod_barang,m1.seq_no Asc";
                val5 = "select sum(jumlah_baki) as jum from KW_kemasukan_inventori where do_tarikh>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and do_tarikh<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1)";
            }
            //else if (Tahun_kew.SelectedValue == "" && dd_select2.SelectedValue != "")
            //{
            //    val6 = "select m1.kod_barang,s1.jenis_barang,s1.nama_barang,FORMAT(m1.tarikh,'dd/MM/yyyy', 'en-us') as tarikh,m1.nama_syarikat,cast(m1.qty_masuk as int) qty_masuk,cast(m1.qty_keluar as int) qty_keluar,cast(m1.qty_baki as int) qty_baki,cast(m1.qty as int) qty,m1.harga_kos,m1.jumlah_kos,m1.seq_no from KW_inv_lep_kad_stok m1 inner join KW_INVENTORI_BARANG s1 on s1.kod_barang=m1.kod_barang where s1.jenis_barang='" + dd_select2.SelectedValue + "' order by m1.kod_barang,m1.tarikh Asc";
            //}
            else
            {
                val6 = "select m1.kod_barang,s1.jenis_barang,s1.nama_barang,FORMAT(m1.tarikh,'dd/MM/yyyy', 'en-us') as tarikh,m1.nama_syarikat,cast(m1.qty_masuk as int) qty_masuk,cast(m1.qty_keluar as int) qty_keluar,cast(m1.qty_baki as int) qty_baki,cast(m1.qty as int) qty,m1.harga_kos,m1.jumlah_kos,m1.seq_no from KW_inv_lep_kad_stok m1 left join KW_INVENTORI_BARANG s1 on s1.kod_barang=m1.kod_barang where m1.tarikh>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and m1.tarikh<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) order by m1.kod_barang,m1.seq_no Asc";
                val5 = "select ISNULL(sum(jumlah_baki),'0.00') as jum from KW_kemasukan_inventori where do_tarikh>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and do_tarikh<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1)";
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

            string sem_val = string.Empty;

            if(dd_select2.SelectedValue != "")
            {
                sem_val = dd_select2.SelectedValue;
            }
            else
            {
                sem_val = "SEMUA";
            }

            Rptviwerlejar.LocalReport.DataSources.Clear();
            if (countRow != 0)
            {
                DataTable get_rec = new DataTable();
                get_rec = DBCon.Ora_Execute_table(val5);

                Rptviwerlejar.LocalReport.ReportPath = "Kewengan/KW_lep_inventori_stok.rdlc";
                ReportDataSource rds = new ReportDataSource("kwinvstk_lep", dt);
                //ReportDataSource rds1 = new ReportDataSource("kwpl1", dt1);
                ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("s1", Tk_mula.Text),
                     new ReportParameter("s2", sem_val),
                     new ReportParameter("s3", double.Parse(get_rec.Rows[0]["jum"].ToString()).ToString("C").Replace("$","").Replace("RM","")),
                     new ReportParameter("s4", Tk_akhir.Text),
                     new ReportParameter("s5", "")
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
        Response.Redirect("../kewengan/kw_lep_kad_stak.aspx");
    }


}