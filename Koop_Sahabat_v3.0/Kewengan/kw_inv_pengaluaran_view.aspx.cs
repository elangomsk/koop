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
using System.Text.RegularExpressions;
using System.Collections;
using System.Threading;

public partial class kw_inv_pengaluaran_view : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();

    StudentWebService service = new StudentWebService();
    string level;
    string Status = string.Empty;
    string userid;
    string ref_id;
    string confirmValue, am;
    string qry1 = string.Empty, qry2 = string.Empty;
    string tn1 = string.Empty, tc1 = string.Empty, tc2 = string.Empty, tc3 = string.Empty, tc4 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.btn_ctk);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                if (Session["validate_success"].ToString() == "SUCCESS")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('" + Session["alrt_msg"].ToString() + "',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    
                }
            
                Session["validate_success"] = "";
                Session["alrt_msg"] = "";
                userid = Session["New"].ToString();
                BindData();
                BindData1();
                BindData2();
                //bind_jenisbarangan();
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('808','705','14','39','809','810','811','1827')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            Button5.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            btn_ctk.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());

        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    //void bind_jenisbarangan()
    //{
    //    DataSet Ds = new DataSet();
    //    try
    //    {

    //        string com = "select DISTINCT jenis_barang from KW_INVENTORI_BARANG where status= 'A' order by jenis_barang";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
    //        DataTable dt = new DataTable();
    //        adpt.Fill(dt);
    //        dd_select2.DataSource = dt;
    //        dd_select2.DataBind();
    //        dd_select2.DataTextField = "jenis_barang";
    //        dd_select2.DataValueField = "jenis_barang";
    //        dd_select2.DataBind();
    //        dd_select2.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    protected void BindData()
    {
        string sqry = string.Empty;
        if (txtSearch.Text == "")
        {
            sqry = "";
        }
        else
        {
            sqry = "where do_no LIKE'%" + txtSearch.Text + "%' OR po_no LIKE'%" + txtSearch.Text + "%' OR s1.Ref_nama_syarikat LIKE'%" + txtSearch.Text + "%'";
        }
        con.Open();
        DataTable ddicno = new DataTable();
        SqlCommand cmd = new SqlCommand("select do_no,FORMAT(do_tarikh,'dd/MM/yyyy', 'en-us') crt_dt,po_no,s1.Ref_nama_syarikat,sum(cast(jum_kuantiti as int)) as qty from KW_pengeluaran_inventori left join KW_Ref_Pelanggan s1 on s1.Ref_no_syarikat=nama_pelengan "+ sqry + " group by do_no,do_tarikh,po_no,s1.Ref_nama_syarikat", con);
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
            gv_refdata.DataSource = ds;
            gv_refdata.DataBind();
        }

        con.Close();
      
    }

    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_refdata.PageIndex = e.NewPageIndex;
        gv_refdata.DataBind();
        BindData();
        tab1();
    }


    protected void BindData1()
    {
        string sqry = string.Empty;
        con.Open();
        DataTable ddicno = new DataTable();
        SqlCommand cmd = new SqlCommand("select m1.kod_barang,s1.nama_barang,FORMAT(m1.tarikh,'dd/MM/yyyy', 'en-us') as tarikh,m1.nama_syarikat,m1.qty_masuk,m1.qty_keluar,m1.qty_baki,m1.qty,m1.harga_kos,m1.jumlah_kos,m1.seq_no from KW_inv_lep_kad_stok m1 left join KW_INVENTORI_BARANG s1 on s1.kod_barang=m1.kod_barang order by m1.kod_barang,seq_no", con);
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
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

        con.Close();
       
    }

    protected void gvSelected_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
        BindData1();
        tab2();
    }



    protected void OnDataBound(object sender, EventArgs e)
    {
        for (int i = GridView1.Rows.Count - 1; i > 0; i--)
        {
            GridViewRow row = GridView1.Rows[i];
            GridViewRow previousRow = GridView1.Rows[i - 1];
            for (int j = 0; j < 2; j++)
            {
                //run this loop for the column which you thing the data will be similar
                if (((System.Web.UI.WebControls.Label)row.Cells[j].FindControl("lbl11")).Text == ((System.Web.UI.WebControls.Label)previousRow.Cells[j].FindControl("lbl11")).Text)
                {
                    if (previousRow.Cells[j].RowSpan == 0)
                    {
                        if (row.Cells[j].RowSpan == 0)
                        {
                            previousRow.Cells[j].RowSpan += 2;
                        }
                        else
                        {
                            previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                        }
                        row.Cells[j].Visible = false;
                    }
                }

            }
        }
    }

    protected void OnDataBound1(object sender, EventArgs e)
    {
        for (int i = GridView2.Rows.Count - 1; i > 0; i--)
        {
            GridViewRow row = GridView2.Rows[i];
            GridViewRow previousRow = GridView2.Rows[i - 1];
            for (int j = 0; j < 2; j++)
            {
                //run this loop for the column which you thing the data will be similar
                if (((System.Web.UI.WebControls.Label)row.Cells[j].FindControl("lbl11")).Text == ((System.Web.UI.WebControls.Label)previousRow.Cells[j].FindControl("lbl11")).Text)
                {
                    if (previousRow.Cells[j].RowSpan == 0)
                    {
                        if (row.Cells[j].RowSpan == 0)
                        {
                            previousRow.Cells[j].RowSpan += 2;
                        }
                        else
                        {
                            previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                        }
                        row.Cells[j].Visible = false;
                    }
                }

            }
        }
    }


    protected void BindData2()
    {
        string sqry = string.Empty;
       
        con.Open();
        DataTable ddicno = new DataTable();
        SqlCommand cmd = new SqlCommand("select s1.id,s1.kod_barang,s1.jenis_barang,s1.keterangan,FORMAT(s1.do_tarikh,'dd/MM/yyyy', 'en-us') as do_tarikh,s1.kuantiti,s1.unit,s1.gst_amaun,s1.gst_amaun_jum,s1.baki_kuantiti,s1.jumlah,ext_qty as out_qty,s1.jumlah_baki as jum_kes from KW_kemasukan_inventori s1 " + sqry + " order by s1.kod_barang,s1.do_tarikh asc", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView2.DataSource = ds;
            GridView2.DataBind();
            int columncount = GridView2.Rows[0].Cells.Count;
            GridView2.Rows[0].Cells.Clear();
            GridView2.Rows[0].Cells.Add(new TableCell());
            GridView2.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView2.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
        }
        else
        {
            GridView2.DataSource = ds;
            GridView2.DataBind();
        }

        con.Close();
       
    }

    protected void gvSelected_PageIndexChanging2(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        GridView2.DataBind();
        BindData2();
        tab3();
    }

    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {

        SearchText();
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        BindData();
    }


    public string Highlight(string InputTxt)
    {
        string Search_Str = txtSearch.Text.ToString();
        // Setup the regular expression and add the Or operator.
        Regex RegExp = new Regex(Search_Str.Replace(" ", "|").Trim(),
        RegexOptions.IgnoreCase);

        // Highlight keywords by calling the 
        //delegate each time a keyword is found.
        return RegExp.Replace(InputTxt,
        new MatchEvaluator(ReplaceKeyWords));

        // Set the RegExp to null.
        RegExp = null;

    }

    public string ReplaceKeyWords(Match m)
    {

        return "<span class=highlight>" + m.Value + "</span>";

    }
    private void SearchText()
    {
        BindData();

    }
    protected void Add_profile(object sender, EventArgs e)
    {
        Response.Redirect("../kewengan/kw_inv_pengaluaran.aspx");
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnButton = sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
            System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("lbl_id");
            string lblid = lblTitle.Text;
            DataTable ddokdicno = new DataTable();
            ddokdicno = DBCon.Ora_Execute_table("select * From KW_pengeluaran_inventori where do_no='" + lblid + "' ");
            if (ddokdicno.Rows.Count != 0)
            {
                string name = HttpUtility.UrlEncode(service.Encrypt(lblTitle.Text));
                Response.Redirect(string.Format("../kewengan/kw_inv_pengaluaran.aspx?edit={0}", name));
            }
            else
            {
                Session["validate_success"] = "";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void clk_cetak(object sender, EventArgs e)
    {
            tab3();
            string url = "KW_inv_stock_details.aspx";
            string s = "window.open('" + url + "', 'popup_window', 'width=11000,height=700,left=100,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

    }

    void tab3()
    {
        p6.Attributes.Add("class", "tab-pane");
        p1.Attributes.Add("class", "tab-pane");
        p2.Attributes.Add("class", "tab-pane active");
       

        pp6.Attributes.Remove("class");
        pp1.Attributes.Remove("class");
        pp2.Attributes.Add("class", "active");
      
    }

  
    void tab2()
    {
        p6.Attributes.Add("class", "tab-pane");
        p2.Attributes.Add("class", "tab-pane");
        p1.Attributes.Add("class", "tab-pane active");


        pp6.Attributes.Remove("class");
        pp2.Attributes.Remove("class");
        pp1.Attributes.Add("class", "active");

    }

    void tab1()
    {
        p1.Attributes.Add("class", "tab-pane");
        p2.Attributes.Add("class", "tab-pane");
        p6.Attributes.Add("class", "tab-pane active");


        pp1.Attributes.Remove("class");
        pp2.Attributes.Remove("class");
        pp6.Attributes.Add("class", "active");

    }

    //protected void clk_cetak(object sender, EventArgs e)
    //{
    //    DataSet ds = new DataSet();
    //    DataSet ds1 = new DataSet();
    //    DataTable dt = new DataTable();
    //    DataTable dt1 = new DataTable();
    //    string val1 = string.Empty, val2 = string.Empty, val3 = string.Empty, val4 = string.Empty, val5 = string.Empty, val6 = string.Empty;

    //    string fmdate = string.Empty, tmdate = string.Empty;
    //    //if (Tk_mula.Text != "")
    //    //{
    //    //    string fdate = Tk_mula.Text;
    //    //    DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
    //    //    fmdate = fd.ToString("yyyy-MM-dd");
    //    //}
    //    //if (Tk_akhir.Text != "")
    //    //{
    //    //    string tdate = Tk_akhir.Text;
    //    //    DateTime td = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
    //    //    tmdate = td.ToString("yyyy-MM-dd");
    //    //}

    //        val6 = "select s1.id,s1.kod_barang,s1.jenis_barang,s1.keterangan,FORMAT(s1.do_tarikh,'dd/MM/yyyy', 'en-us') as do_tarikh,s1.kuantiti,s1.unit,s1.gst_amaun,s1.gst_amaun_jum,s1.baki_kuantiti,s1.jumlah,(cast(s1.kuantiti as int) - cast(s1.baki_kuantiti as int)) as out_qty,(s1.unit * (cast(s1.baki_kuantiti as int))) as jum_kes from KW_kemasukan_inventori s1 order by s1.kod_barang,s1.do_tarikh desc";


    //    dt = DBCon.Ora_Execute_table(val6);
    //    //dt1 = DBCon.Ora_Execute_table(val5);
    //    Rptviwerlejar.Reset();
    //    ds.Tables.Add(dt);
    //    //ds1.Tables.Add(dt1);

    //    List<DataRow> listResult = dt.AsEnumerable().ToList();
    //    listResult.Count();
    //    int countRow = 0;
    //    countRow = listResult.Count();



    //    Rptviwerlejar.LocalReport.DataSources.Clear();
    //    if (countRow != 0)
    //    {


    //        Rptviwerlejar.LocalReport.ReportPath = "Kewengan/KW_inventori_stok.rdlc";
    //        ReportDataSource rds = new ReportDataSource("kwinvstk", dt);
    //        //ReportDataSource rds1 = new ReportDataSource("kwpl1", dt1);
    //        //ReportParameter[] rptParams = new ReportParameter[]{
    //        //         new ReportParameter("s1", ""),
    //        //         new ReportParameter("s2", ""),
    //        //         new ReportParameter("s3", ""),
    //        //         new ReportParameter("s4", ""),
    //        //         new ReportParameter("S5", "")
    //        //              };

    //        //Rptviwerlejar.LocalReport.SetParameters(rptParams);
    //        Rptviwerlejar.LocalReport.DataSources.Add(rds);
    //        //Rptviwerlejar.LocalReport.DataSources.Add(rds1);
    //        Rptviwerlejar.LocalReport.Refresh();
    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod tidak dijumpai. Sila Pastikan Semua Maklumat Dimasukkan Dengan Betul.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
    //    }

    //}
}