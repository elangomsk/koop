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

public partial class kw_lep_kunci_kira : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    private static int PageSize = 20;
    string qry1 = string.Empty, qry2 = string.Empty, sqry1 = string.Empty, sqry2 = string.Empty, fyear = string.Empty;
    string level;
    string Status = string.Empty;
    string userid;
    string CommandArgument1 = string.Empty, CommandArgument2 = string.Empty, CommandArgument3 = string.Empty;
    string val1 = string.Empty, val2 = string.Empty, val3 = string.Empty, val4 = string.Empty, val5 = string.Empty, val6 = string.Empty, sqry = string.Empty, py_fdate = string.Empty, py_ldate = string.Empty, curr_yr = string.Empty, prev_yr = string.Empty;

    int min_val = 1;
    string fmdate = string.Empty, tmdate = string.Empty, tmdate1 = string.Empty;
    string var_year = string.Empty, var_fmdate = string.Empty, var_tmdate = string.Empty, var_tmdate1 = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        Button4.OnClientClick = @"if(this.value == 'Please wait...')
           return false;
           this.value = 'Please wait...';this.disabled=true";
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        //ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        //scriptManager.RegisterPostBackControl(this.Button4);
        
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
                var samp = Request.Url.Query;

                if (samp != "")
                {
                    Button8.Visible = true;
                    DataTable get_fin_year = new DataTable();
                    get_fin_year = DBCon.Ora_Execute_table("select FORMAT(fin_start_dt,'dd/MM/yyyy') st_dt,FORMAT(fin_end_dt,'dd/MM/yyyy') ed_dt from KW_financial_Year where fin_year='" + service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"])) + "' and Status='1'");
                    if (get_fin_year.Rows.Count != 0)
                    {
                        Tk_mula.Text = get_fin_year.Rows[0]["st_dt"].ToString();
                        Tk_akhir.Text = get_fin_year.Rows[0]["ed_dt"].ToString();
                        bind_data();
                    }
                }
                else
                {
                    Button8.Visible = false;
                }
                project();
                bind_acoa();
                //bind_level();                
                chk_var3.Checked = true;
                //BindDropdown();

            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    protected void Back_to_profile(object sender, EventArgs e)
    {
        string name = Request.QueryString["prfle_cd"];        
        Response.Redirect(string.Format("../kewengan/kw_profil_syarikat.aspx?edit={0}", name));
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

    //void bind_level()
    //{
    //    DataSet Ds = new DataSet();
    //    try
    //    {
    //        string com = "select jenis_akaun_type from KW_Ref_Carta_Akaun where jenis_akaun_type != '1' and Status='A' group by jenis_akaun_type";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
    //        DataTable dt = new DataTable();
    //        adpt.Fill(dt);
    //        dd_level.DataSource = dt;
    //        dd_level.DataTextField = "jenis_akaun_type";
    //        dd_level.DataValueField = "jenis_akaun_type";
    //        dd_level.DataBind();
    //        //dd_level.Items.Insert(0, new ListItem("--- PILIH ---", ""));

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
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

    protected void clk_update(object sender, EventArgs e)
    {
        if (Tk_mula.Text != "" && Tk_akhir.Text != "")
        {
            LinkButton btnButton = sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
            System.Web.UI.WebControls.Label og_genid = (System.Web.UI.WebControls.Label)gvRow.FindControl("bal_type");
            System.Web.UI.WebControls.Label akaun_txt = (System.Web.UI.WebControls.Label)gvRow.FindControl("kat_cd");
            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
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
            sqry1 = "select * from (select m1.project_kod,s1.kat_akaun kcd,s11.kat_akuan kname,s1.kod_akaun,s1.nama_akaun,case when ISNULL(s2.opn_debit_amt,'0.00')='0.00' then (ISNULL(s2.opn_kredit_amt,'0.00') + (ISNULL(m1_1.KW_kredit_amt,'0.00')-ISNULL(m1_1.KW_Debit_amt,'0.00'))) else (-1 * (ISNULL(s2.opn_debit_amt,'0.00') + (ISNULL(m1_1.KW_kredit_amt,'0.00')-ISNULL(m1_1.KW_Debit_amt,'0.00')))) end as opening_amt,m1.GL_invois_no as no_invois,m1.GL_post_dt,Format(m1.GL_post_dt, 'dd/MM/yyyy') tarikh_invois "
                                     + " ,ISNULL(m1.GL_desc1, '') as rjkn1,'' as rjkn2,ISNULL(m1.KW_Debit_amt, '') as KW_Debit_amt,ISNULL(m1.KW_kredit_amt, '') as KW_kredit_amt,m1.ref2,m1.GL_journal_no"
                                     + ", m1.Gl_jenis_no as no1"
                                     + " ,(case "
                                     + " when m1.ref2 = '01' then(select '' v1 from KW_Pembayaran_invois where no_invois = m1.GL_invois_no)"
                                     + " when m1.ref2 = '02' then(select no_cek v1 from KW_Penerimaan_resit where no_resit = m1.GL_invois_no)"
                                     + " when m1.ref2 = '03' then(select '' v1 from KW_Pembayaran_mohon where no_permohonan = m1.GL_invois_no)"
                                     + " when m1.ref2 = '04' then(select No_cek v1 from KW_Pembayaran_Pay_voucer where Pv_no = m1.GL_invois_no)"
                                     + " when m1.ref2 = '05' then(select '' v1 from KW_Pembayaran_Credit where no_Rujukan = m1.GL_invois_no)"
                                     + " when m1.ref2 = '06' then(select '' v1 from KW_Pembayaran_Dedit where no_Rujukan = m1.GL_invois_no)"
                                     + " when m1.ref2 = '09' then(select '' v1 from KW_Penerimaan_invois where no_invois = m1.GL_invois_no)"
                                     + " when m1.ref2 = '10' then(select '' v1 from KW_Penerimaan_Credit where no_notakredit = m1.GL_invois_no)"
                                     + " when m1.ref2 = '11' then(select '' v1 from KW_Penerimaan_Debit where no_notadebit = m1.GL_invois_no)"
                                     + " when m1.ref2 IN ('12', '13', '14', '15', '16', '17', '18', '19', '20', '21') then(select '' v1 from KW_jurnal_inter where no_permohonan = m1.GL_invois_no)"
                                     + " end) as no2, s3.Ref_doc_name dc_name from KW_Ref_Carta_Akaun s1"
                                     + " left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = s1.kod_akaun and '" + fmdate + "' between s2.start_dt and s2.end_dt "
                                     + " left join KW_General_Ledger m1_1 on s1.kod_akaun = m1_1.kod_akaun and m1_1.GL_sts='L' and YEAR(m1_1.GL_post_dt)='" + fyear + "' and  m1_1.GL_post_dt < DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) "
                                     + " left join KW_General_Ledger m1 on s1.kod_akaun = m1.kod_akaun and m1.GL_sts='L' and m1.GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and m1.GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) "
                                     + " left join KW_Kategori_akaun s11 on s11.kat_type='01' and s11.kat_cd=s1.kat_akaun "
                                     + " left join KW_Ref_Doc_kod s3 on s3.Ref_doc_code = m1.ref2"
                                     + " where s1.Status = 'A' and s1.jenis_akaun_type !='1' and ISNULL(ca_cyp,'') !='1' ) as a "
                          + " where a.kod_akaun ='"+ og_genid.Text + "' "
                         + " order by a.kod_akaun";

            sqry2 = " select  sum(a.opening_amt) open_amt from (select distinct s1.kat_akaun kcd,s11.kat_akuan kname,s1.kod_akaun,s1.nama_akaun, "
                                  + " case when ISNULL(s2.opn_debit_amt,'0.00')='0.00' then (ISNULL(s2.opn_kredit_amt,'0.00'  ) + (ISNULL(m1_1.KW_kredit_amt,'0.00') - ISNULL(m1_1.KW_Debit_amt,'0.00'))) else ((ISNULL(s2.opn_debit_amt,'0.00') + 	(ISNULL(m1_1.KW_Debit_amt,'0.00') - ISNULL(m1_1.KW_kredit_amt,'0.00')))) end as opening_amt  "
                                   + "  from KW_Ref_Carta_Akaun s1 left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = s1.kod_akaun and '" + fmdate + "' between s2.start_dt and s2.end_dt   "
                                   + " outer apply(select ISNULL(sum(m1_1.KW_kredit_amt),'0.00') KW_kredit_amt,ISNULL(sum(m1_1.KW_kredit_amt),'0.00')  KW_Debit_amt  from KW_General_Ledger m1_1 where s1.kod_akaun = m1_1.kod_akaun and m1_1.GL_sts='L' and YEAR(m1_1.GL_post_dt)='" + prev_yr + "' and  m1_1.GL_post_dt < DATEADD(day, DATEDIFF(day, 0, '" + fmdate +"'), 0)) as m1   "
                                   + " outer apply(select sum(ISNULL(m1.KW_kredit_amt,'0.00')) KW_kredit_amt,sum(ISNULL(m1.KW_Debit_amt,'0.00')) KW_Debit_amt  from KW_General_Ledger m1 where s1.kod_akaun = m1.kod_akaun and m1.GL_sts='L' and YEAR(m1.GL_post_dt)='" + fyear + "' and  m1.GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and m1.GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1)) as m1_1   "
                                   + " left join KW_Kategori_akaun s11 on s11.kat_type='01' and s11.kat_cd=s1.kat_akaun  left join KW_Ref_Doc_kod s3 on s3.Ref_doc_code = '' where s1.Status = 'A' and s1.kod_akaun ='" + og_genid.Text + "'  "
                                   + " and s1.jenis_akaun_type !='1' and ISNULL(ca_cyp,'') !='1' ) as a  ";


            dt = DBCon.Ora_Execute_table(sqry1);
            dt1 = DBCon.Ora_Execute_table(sqry2);
            //dt1 = DBCon.Ora_Execute_table(sqry2);
            ds.Tables.Add(dt);
            //ds1.Tables.Add(dt1);

            string vs1 = string.Empty, vs2 = string.Empty, vs3 = string.Empty, vs4 = string.Empty, vs5 = string.Empty, vs6 = string.Empty;

            ReportViewer1.Reset();
            ReportViewer1.LocalReport.Refresh();
            List<DataRow> listResult = dt.AsEnumerable().ToList();
            listResult.Count();
            int countRow = 0;
            countRow = listResult.Count();



            ReportViewer1.LocalReport.DataSources.Clear();
            if (countRow != 0)
            {
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

                DataTable cnt_open = new DataTable();

                cnt_open = DBCon.Ora_Execute_table(sqry2);
                ReportViewer1.LocalReport.EnableExternalImages = true;

                ReportViewer1.LocalReport.ReportPath = "Kewengan/KW_kunci_kira_new.rdlc";
              
                ReportDataSource rds = new ReportDataSource("kwlegar", dt);
                //ReportDataSource rds1 = new ReportDataSource("kwlegar1", dt1);
                ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("s1", "SEMUA"),
                     new ReportParameter("s2", "NO"),
                     new ReportParameter("s3", Tk_mula.Text),
                     new ReportParameter("s4", Tk_akhir.Text),
                     new ReportParameter("S5", double.Parse(dt1.Rows[0]["open_amt"].ToString()).ToString("C").Replace("RM","").Replace("$","")),
                     new ReportParameter("v1",imagePath)
                          };

                ReportViewer1.LocalReport.SetParameters(rptParams);
                ReportViewer1.LocalReport.DataSources.Add(rds);
                //Rptviwerlejar.LocalReport.DataSources.Add(rds1);
                ReportViewer1.LocalReport.DisplayName = "JURNAL_TXN_" + akaun_txt.Text.ToUpper() + "_" + DateTime.Now.ToString("yyyyMMdd");

                ReportViewer1.LocalReport.Refresh();

                System.Threading.Thread.Sleep(1);
                Warning[] warnings;

                string[] streamids;

                string mimeType;

                string encoding;

                string extension;

                byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);

                Response.Buffer = true;

                Response.Clear();

                Response.ContentType = mimeType;

                Response.AddHeader("content-disposition", "attatchment; filename=\"JURNAL_TXN_" + akaun_txt.Text.ToUpper() + "_" + DateTime.Now.ToString("yyyyMMdd") +"." + extension +"\"");

                Response.BinaryWrite(bytes);

                Response.Flush();

                Response.End();


            }
            else
            {
                bind_data();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod tidak dijumpai. Sila Pastikan Semua Maklumat Dimasukkan Dengan Betul.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);                
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
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

    void bind_details()
    {
       
        DateTime fd, td;
        if (Tk_mula.Text != "" && Tk_akhir.Text != "")
        {

            if (Tk_mula.Text != "")
            {
                string fdate = Tk_mula.Text;
                fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                fmdate = fd.ToString("yyyy-MM-dd");


            }
            if (Tk_akhir.Text != "")
            {
                string tdate = Tk_akhir.Text;
                td = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                tmdate = td.ToString("yyyy-MM-dd");
                curr_yr = td.ToString("yyyy");

                tmdate1 = td.ToString("dd MMMM yyyy");
            }


            py_fdate = fmdate;
            py_ldate = tmdate;
            if (TextBox1.Text != "")
            {
                string var_fdate = TextBox1.Text;
                DateTime var_fd = DateTime.ParseExact(var_fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var_fmdate = var_fd.ToString("yyyy-MM-dd");
                var_year = var_fd.ToString("yyyy");

            }
            if (TextBox2.Text != "")
            {
                string var_tdate = TextBox2.Text;
                DateTime var_td = DateTime.ParseExact(var_tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var_tmdate = var_td.ToString("yyyy-MM-dd");
                prev_yr = var_td.ToString("yyyy");
            }
            if (chk_var1.Checked == true)
            {
                qry1 = "with Recurse as ( select a.Id as DirectChildId, a.Id, a.kod_akaun from KW_Ref_Carta_Akaun a  where a.Status='A' and kkk_rep='1'   "
                + " union all select b.DirectChildId, a.Id, a.kod_akaun from KW_Ref_Carta_Akaun a  join Recurse b on b.Id = a.under_parent where a.Status='A' and a.kkk_rep='1') ,   "
                + " tree1 AS(select b.kat_akaun,b.nama_akaun,b.kod_akaun,b.kw_acc_header from (select a.DirectChildId from Recurse a left join KW_General_Ledger b on b.GL_sts='L' and a.kod_akaun = b.kod_akaun  "
                + " group by DirectChildId) as a    inner join  (select m1.Id,m1.jenis_akaun_type,m1.kat_akaun,m1.nama_akaun,m1.kod_akaun,m1.jenis_akaun,m1.under_parent,m1.kw_acc_header from KW_Ref_Carta_Akaun m1 where  "
                + " m1.Status='A' and m1.kkk_rep='1') as b on b.Id=a.DirectChildId) , tree2 AS(select b.kw_acc_header,b.jenis_akaun_type,b.kat_akaun,b.kod_akaun,b.nama_akaun,b.KW_Debit_amt,b.KW_kredit_amt,a.Amount from  "
                + " (select a.DirectChildId, (((ISNULL(cast(s2.opn_kredit_amt as money),'0.00') - ISNULL(cast(s2.opn_debit_amt as money),'0.00')) + (isnull(sum(cast(s2_1.KW_kredit_amt as money)),'0.00') -  "
                + " isnull(sum(cast(s2_1.KW_Debit_amt as money)),'0.00'))) + isnull(sum(cast(b.KW_kredit_amt as money)),'0.00')) - isnull(sum(cast(b.KW_Debit_amt as money)),'0.00')  as Amount  "
                + " from Recurse a "
                + " left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = a.kod_akaun and '" + curr_yr + "' between YEAR(s2.start_dt) and YEAR(s2.end_dt)   "
                + " left join KW_General_Ledger s2_1 on  s2_1.kod_akaun = a.kod_akaun and YEAR(s2_1.GL_post_dt)='" + curr_yr + "' and  s2_1.GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0)   "
                + " left join KW_General_Ledger b on b.kod_akaun = a.kod_akaun and b.GL_sts='L' and b.GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and  "
                + " b.GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by DirectChildId,s2.opn_debit_amt,s2.opn_kredit_amt) as a    "
                + " inner join  (select m1.Id,m1.jenis_akaun_type,m1.kat_akaun,m1.nama_akaun,m1.kod_akaun,m1.jenis_akaun,m1.under_parent,m1.kw_acc_header, "
                + " ((ISNULL(cast(s2.opn_debit_amt as money),'0.00')) + sum(ISNULL(s1.KW_Debit_amt,'0.00'))) as KW_Debit_amt , "
                + " ((ISNULL(cast(s2.opn_kredit_amt as money),'0.00')) + sum(ISNULL(s1.KW_kredit_amt,'0.00'))) as KW_kredit_amt  from KW_Ref_Carta_Akaun m1   "
                + " left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = m1.kod_akaun and '" + curr_yr + "' between YEAR(s2.start_dt) and YEAR(s2.end_dt)   "
                + " left join KW_General_Ledger s1 on s1.kod_akaun=m1.kod_akaun and s1.GL_sts='L' and  "
                + " GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) where m1.Status='A' and m1.kkk_rep='1'  "
                + " group by  m1.Id,m1.jenis_akaun_type,m1.kat_akaun,m1.nama_akaun,m1.kod_akaun,m1.jenis_akaun,m1.under_parent,m1.kw_acc_header,s2.opn_debit_amt,s2.opn_kredit_amt) as b on b.Id=a.DirectChildId   "
                + " where (b.KW_Debit_amt !='0.00' or b.KW_kredit_amt !='0.00' or a.Amount !='0.00'))   "
                + " , tree3 AS(select b.kw_acc_header,b.jenis_akaun_type,b.kat_akaun,b.kod_akaun,b.nama_akaun,b.KW_Debit_amt,b.KW_kredit_amt,a.Amount from  "
                + " (select a.DirectChildId, (((ISNULL(cast(s2.opn_kredit_amt as money),'0.00') - ISNULL(cast(s2.opn_debit_amt as money),'0.00')) + (isnull(sum(cast(s2_1.KW_kredit_amt as money)),'0.00') -  "
                + " isnull(sum(cast(s2_1.KW_Debit_amt as money)),'0.00'))) + isnull(sum(cast(b.KW_kredit_amt as money)),'0.00')) - isnull(sum(cast(b.KW_Debit_amt as money)),'0.00')  as Amount  "
                + " from Recurse a left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = a.kod_akaun and '" + prev_yr + "' between YEAR(s2.start_dt) and YEAR(s2.end_dt)   "
                + " left join KW_General_Ledger s2_1 on  s2_1.kod_akaun = a.kod_akaun and YEAR(s2_1.GL_post_dt)='" + prev_yr + "' and  s2_1.GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + var_fmdate + "'), 0)   "
                + " left join KW_General_Ledger b on b.kod_akaun = a.kod_akaun and b.GL_sts='L' and b.GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + var_fmdate + "'), 0) and  "
                + " b.GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + var_tmdate + "'), +1) group by DirectChildId,s2.opn_debit_amt,s2.opn_kredit_amt) as a    "
                + " inner join  (select m1.Id,m1.jenis_akaun_type,m1.kat_akaun,m1.nama_akaun,m1.kod_akaun,m1.jenis_akaun,m1.under_parent,m1.kw_acc_header, "
                + " ((ISNULL(cast(s2.opn_debit_amt as money),'0.00')) + sum(ISNULL(s1.KW_Debit_amt,'0.00'))) as KW_Debit_amt , "
                + " ((ISNULL(cast(s2.opn_kredit_amt as money),'0.00')) + sum(ISNULL(s1.KW_kredit_amt,'0.00'))) as KW_kredit_amt  from KW_Ref_Carta_Akaun m1   "
                + " left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = m1.kod_akaun and '" + prev_yr + "' between YEAR(s2.start_dt) and YEAR(s2.end_dt)   "
                + " left join KW_General_Ledger s1 on s1.kod_akaun=m1.kod_akaun and s1.GL_sts='L' and  "
                + " GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + var_fmdate + "'), 0) and GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + var_tmdate + "'), +1) where m1.Status='A' and m1.kkk_rep='1'  "
                + " group by  m1.Id,m1.jenis_akaun_type,m1.kat_akaun,m1.nama_akaun,m1.kod_akaun,m1.jenis_akaun,m1.under_parent,m1.kw_acc_header,s2.opn_debit_amt,s2.opn_kredit_amt) as b on b.Id=a.DirectChildId   "
                + " where (b.KW_Debit_amt !='0.00' or b.KW_kredit_amt !='0.00' or a.Amount !='0.00'))    "
                + " select a1.kat_akaun,a1.kat_type,a1.deskripsi,a1.bal_type,a1.nama_akaun,a1.kod_akaun,a1.jenis_akaun_type,ISNULL(a1.kw_acc_header,'0')kw_acc_header, "
                + " sum(a1.KW_Debit_amt) KW_Debit_amt,(sum(a1.KW_kredit_amt) + sum(ISNULL(a2.amt1,'0.00'))) KW_kredit_amt "
                + " ,(((sum(a1.KW_kredit_amt) - sum(a1.KW_Debit_amt))) +sum(ISNULL(a2.amt1,'0.00'))) act_amt1,((sum(a1.KW_kredit_amt1) - sum(a1.KW_Debit_amt1)) + sum(ISNULL(a2.amt2,'0.00'))) act_amt2, "
                + " (sum(a1.KW_kredit_amt1) + sum(ISNULL(a2.amt2,'0.00'))) KW_kredit_amt1,sum(a1.KW_Debit_amt1) KW_Debit_amt1, "
                + " (SUM(a1.Amt1) + sum(ISNULL(a2.amt1,'0.00'))) Amt1,(sum(a1.Amt2) + sum(ISNULL(a2.amt2,'0.00')))Amt2,(sum(a1.Amt3) + sum(ISNULL(a2.amt1,'0.00'))) Amt3,(sum(a1.Amt4) + sum(ISNULL(a2.amt2,'0.00'))) Amt4 from( select * from  "
                + " (select s1.kw_acc_header,s2.jenis_akaun_type,s1.kat_akaun,s5.kat_type,s4.deskripsi,s4.bal_type,s1.nama_akaun,s1.kod_akaun,ISNULL(s2.KW_Debit_amt,'0.00') KW_Debit_amt,ISNULL(s2.KW_kredit_amt,'0.00') KW_kredit_amt, "
                + " ISNULL(s3.KW_Debit_amt,'0.00') KW_Debit_amt1,ISNULL(s3.KW_kredit_amt,'0.00') KW_kredit_amt1,replace(ISNULL(s2.Amount,'0.00'),'-','') Amt1, "
                + " replace(ISNULL(s3.Amount,'0.00'),'-','') Amt2,ISNULL(s2.Amount,'0.00') Amt3,ISNULL(s3.Amount,'0.00') Amt4 from  tree1 s1   "
                + " left join tree2 s2 on s2.kod_akaun=s1.kod_akaun left join tree3 s3 on s3.kod_akaun=s1.kod_akaun left join KW_Kategori_akaun s4 on s4.kat_cd=s1.kat_akaun  "
                + " and kat_type='01' inner join kw_ref_report_1 s5 on s5.kat_cd=s4.kat_rpt_kk and kat_rpt_cd='01' where (s2.Amount !='0.00' or s3.Amount !='0.00')) as a   "
                + " union all   "
                + " select a1.kw_acc_header,a1.jenis_akaun_type,a1.kat_akaun, a1.kat_type, a1.deskripsi, a1.bal_type, a1.nama_akaun,  "
                + " a1.kod_akaun,a1.KW_Debit_amt,a1.KW_kredit_amt,(a1.KW_Debit_amt1 + b1.KW_Debit_amt1) KW_Debit_amt1,(a1.KW_kredit_amt1 + b1.KW_kredit_amt1) KW_kredit_amt1,a1.amt1, "
                + " b1.amt2 as amt2,a1.amt1 as amt3,b1.amt4 as amt4 from(select cyp.kw_acc_header,cyp.jenis_akaun_type,kk1.kat_cd kat_akaun, 'D' kat_type, kk1.deskripsi, Kk1.bal_type, cyp.nama_akaun,  "
                + " cyp.kod_akaun, sum(ISNULL(s1.KW_Debit_amt, '0.00')) KW_Debit_amt, sum(ISNULL(s1.KW_kredit_amt, '0.00')) KW_kredit_amt, '0.00' KW_Debit_amt1,'0.00' KW_kredit_amt1 "
                + " ,  (((ISNULL(cast(s2.opn_kredit_amt as money), '0.00') - ISNULL(cast(s2.opn_debit_amt as money), '0.00')) + (isnull(sum(cast(s2_1.KW_kredit_amt as money)), '0.00') - isnull(sum(cast(s2_1.KW_Debit_amt as money)), '0.00'))) +  "
                + " isnull(sum(cast(s1.KW_kredit_amt as money)), '0.00')) - isnull(sum(cast(s1.KW_Debit_amt as money)), '0.00') as amt1  from KW_Ref_Carta_Akaun m1  "
                + " left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = m1.kod_akaun and '" + curr_yr + "' between YEAR(s2.start_dt) and YEAR(s2.end_dt)   "
                + " left join KW_General_Ledger s2_1 on  s2_1.kod_akaun = m1.kod_akaun and YEAR(s2_1.GL_post_dt) = '" + curr_yr + "' and  s2_1.GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0)   "
                + " left join KW_General_Ledger s1 on s1.kod_akaun = m1.kod_akaun and s1.GL_post_dt >= DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and  "
                + " s1.GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and s1.GL_sts = 'L'  left join KW_Kategori_akaun kk on kk.kat_cd = m1.kat_akaun and kat_type = '01'  "
                + " inner join kw_ref_report_1 s5 on s5.kat_cd = kk.kat_rpt_kk and kat_rpt_cd = '02'  left join KW_Ref_Carta_Akaun cyp on cyp.ca_cyp = '1'   "
                + " left join KW_Kategori_akaun kk1 on kk1.kat_cd=cyp.kat_akaun and kk1.kat_type='01'  where m1.jenis_akaun_type != '1' and m1.Status = 'A'  "
                + " group by  cyp.kw_acc_header,cyp.jenis_akaun_type,kk1.kat_cd, kk1.deskripsi, Kk1.bal_type, cyp.nama_akaun, cyp.kod_akaun,s2.opn_debit_amt,s2.opn_kredit_amt) as a1   "
                + " outer apply (select cyp_m.kw_acc_header,cyp_m.jenis_akaun_type,cyp_m.kat_akaun,cyp_m.kat_type, cyp_m.deskripsi,cyp_m.bal_type, cyp_m.nama_akaun, cyp_m.kod_akaun, "
                + " sum(cast(cyp_m.KW_Debit_amt as money))KW_Debit_amt,SUM(cast(cyp_m.KW_kredit_amt as money)) KW_kredit_amt,(sum(cast(cyp_m.KW_kredit_amt as money)) - sum(cast(cyp_m.KW_Debit_amt as money))) as act_amt1,(sum(cast(cyp_m.KW_kredit_amt1 as money)) - sum(cast(cyp_m.KW_Debit_amt1 as money))) as act_amt2, "
                + " sum(cast(cyp_m.KW_kredit_amt1 as money)) KW_kredit_amt1,sum(cast(cyp_m.KW_Debit_amt1 as money)) KW_Debit_amt1,sum(cast(amt1 as money)) Amt1, sum(cast(amt2 as money)) Amt2,sum(cast(Amt3 as money)) Amt3,sum(cast(Amt4 as money)) Amt4 "
                + " from (select * from (select cyp.kw_acc_header,cyp.jenis_akaun_type,kk1.kat_cd kat_akaun, 'D' kat_type, kk1.deskripsi,  "
                + " Kk1.bal_type, cyp.nama_akaun, cyp.kod_akaun, sum(ISNULL(s1.KW_Debit_amt, '0.00')) KW_Debit_amt, sum(ISNULL(s1.KW_kredit_amt, '0.00')) KW_kredit_amt "
                + " , '0.00' KW_Debit_amt1,'0.00' KW_kredit_amt1,(sum(ISNULL(s1.kw_kredit_amt, '0.00')) - sum(ISNULL(s1.KW_Debit_amt, '0.00'))) act_amt1,	'0.00' act_amt2,     "
                + " ((((ISNULL(cast(s2.opn_kredit_amt as money), '0.00') - ISNULL(cast(s2.opn_debit_amt as money), '0.00')) + (isnull(sum(cast(s2_1.KW_kredit_amt as money)), '0.00') -  "
                + " isnull(sum(cast(s2_1.KW_Debit_amt as money)), '0.00'))) + isnull(sum(cast(s1.KW_kredit_amt as money)), '0.00')) - isnull(sum(cast(s1.KW_Debit_amt as money)), '0.00')) as amt1,'0.00' amt2, "
                + " ('0.00' - ((((ISNULL(cast(s2.opn_kredit_amt as money), '0.00') - ISNULL(cast(s2.opn_debit_amt as money), '0.00')) + (isnull(sum(cast(s2_1.KW_kredit_amt as money)), '0.00') -  "
                + " isnull(sum(cast(s2_1.KW_Debit_amt as money)), '0.00'))) + isnull(sum(cast(s1.KW_kredit_amt as money)), '0.00')) - isnull(sum(cast(s1.KW_Debit_amt as money)), '0.00'))) Amt3,'0.00' Amt4 "
                + " from KW_Ref_Carta_Akaun m1 left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = m1.kod_akaun and '" + curr_yr + "' between YEAR(s2.start_dt) and YEAR(s2.end_dt)   "
                + " left join KW_General_Ledger s2_1 on  s2_1.kod_akaun = m1.kod_akaun and YEAR(s2_1.GL_post_dt) = '" + curr_yr + "' and  s2_1.GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0)   "
                + " left join KW_General_Ledger s1 on s1.kod_akaun = m1.kod_akaun and s1.GL_post_dt >= DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and s1.GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1)  "
                + " and s1.GL_sts = 'L'  left join KW_Kategori_akaun kk on kk.kat_cd = m1.kat_akaun and kat_type = '01' inner join kw_ref_report_1 s5 on s5.kat_cd = kk.kat_rpt_kk and kat_rpt_cd = '02'   "
                + " left join KW_Ref_Carta_Akaun cyp on cyp.ca_cyp = '1'  left join KW_Kategori_akaun kk1 on kk1.kat_cd=cyp.kat_akaun and kk1.kat_type='01'  where m1.jenis_akaun_type != '1' and m1.Status = 'A' group by   "
                + " cyp.kw_acc_header,cyp.jenis_akaun_type,kk1.kat_cd, kk1.deskripsi, Kk1.bal_type, cyp.nama_akaun, cyp.kod_akaun ,s2.opn_debit_amt,s2.opn_kredit_amt) cyp_a "
                + " union all select * from (select cyp.kw_acc_header,cyp.jenis_akaun_type,kk1.kat_cd kat_akaun, 'D' kat_type, kk1.deskripsi, Kk1.bal_type, cyp.nama_akaun, cyp.kod_akaun, '0.00' KW_Debit_amt,'0.00' KW_kredit_amt "
                + " , sum(ISNULL(s1.KW_Debit_amt, '0.00')) KW_Debit_amt1, sum(ISNULL(s1.KW_kredit_amt, '0.00')) KW_kredit_amt1,'0.00' act_amt1,(sum(ISNULL(s1.kw_kredit_amt, '0.00')) - sum(ISNULL(s1.KW_Debit_amt, '0.00'))) act_amt2,'0.00' amt1,	     "
                + " ((((ISNULL(cast(s2.opn_kredit_amt as money), '0.00') - ISNULL(cast(s2.opn_debit_amt as money), '0.00')) + (isnull(sum(cast(s2_1.KW_kredit_amt as money)), '0.00') -  "
                + " isnull(sum(cast(s2_1.KW_Debit_amt as money)), '0.00'))) + isnull(sum(cast(s1.KW_kredit_amt as money)), '0.00')) - isnull(sum(cast(s1.KW_Debit_amt as money)), '0.00')) as amt2, "
                + " '0.00' Amt3,  ('0.00' - ((((ISNULL(cast(s2.opn_kredit_amt as money), '0.00') - ISNULL(cast(s2.opn_debit_amt as money), '0.00')) + (isnull(sum(cast(s2_1.KW_kredit_amt as money)), '0.00') -  "
                + " isnull(sum(cast(s2_1.KW_Debit_amt as money)), '0.00'))) + isnull(sum(cast(s1.KW_kredit_amt as money)), '0.00')) - isnull(sum(cast(s1.KW_Debit_amt as money)), '0.00'))) Amt4 "
                + " from KW_Ref_Carta_Akaun m1 left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = m1.kod_akaun and '' between YEAR(s2.start_dt) and YEAR(s2.end_dt)   "
                + " left join KW_General_Ledger s2_1 on  s2_1.kod_akaun = m1.kod_akaun and YEAR(s2_1.GL_post_dt) = '" + prev_yr + "' and  s2_1.GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + var_fmdate + "'), 0)   "
                + " left join KW_General_Ledger s1 on s1.kod_akaun = m1.kod_akaun and s1.GL_post_dt >= DATEADD(day, DATEDIFF(day, 0, '" + var_fmdate + "'), 0) and s1.GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + var_tmdate + "'), +1)  "
                + " and s1.GL_sts = 'L'  left join KW_Kategori_akaun kk on kk.kat_cd = m1.kat_akaun and kat_type = '01' inner join kw_ref_report_1 s5 on s5.kat_cd = kk.kat_rpt_kk and kat_rpt_cd = '02'   "
                + " left join KW_Ref_Carta_Akaun cyp on cyp.ca_cyp = '1'  left join KW_Kategori_akaun kk1 on kk1.kat_cd=cyp.kat_akaun and kk1.kat_type='01'  where m1.jenis_akaun_type != '1' and m1.Status = 'A' group by   "
                + " cyp.kw_acc_header,cyp.jenis_akaun_type,kk1.kat_cd, kk1.deskripsi, Kk1.bal_type, cyp.nama_akaun, cyp.kod_akaun,s2.opn_debit_amt,s2.opn_kredit_amt) as cyp_b ) as cyp_m group by cyp_m.kw_acc_header,cyp_m.jenis_akaun_type,cyp_m.kat_akaun,cyp_m.kat_type, cyp_m.deskripsi,cyp_m.bal_type, cyp_m.nama_akaun, cyp_m.kod_akaun "
                + " )  as b1 where b1.kod_akaun = a1.kod_akaun) as a1  "
                + " outer apply (select re_m.kw_acc_header,re_m.jenis_akaun_type,re_m.kat_akaun, re_m.kat_type, re_m.deskripsi, re_m.bal_type, re_m.nama_akaun,   "
                + " re_m.kod_akaun,sum(ISNULL(re_m.KW_Debit_amt, '0.00')) KW_Debit_amt,sum(ISNULL(re_m.KW_kredit_amt, '0.00')) KW_kredit_amt, "
                + " sum(ISNULL(re_m.KW_Debit_amt1, '0.00')) KW_Debit_amt1,sum(ISNULL(re_m.KW_kredit_amt1, '0.00')) KW_kredit_amt1,sum(ISNULL(re_m.amt1, '0.00')) amt1,sum(ISNULL(re_m.amt2, '0.00')) amt2 from( "
                + " select * from(select cyp.kw_acc_header,cyp.jenis_akaun_type,kk1.kat_cd kat_akaun, 'D' kat_type, kk1.deskripsi, Kk1.bal_type, cyp.nama_akaun,   "
                + " cyp.kod_akaun, cast(ISNULL('0.00', '0.00') as money) KW_Debit_amt, sum(cast(ISNULL(s2.fin_amt_re, '0.00') as money)) KW_kredit_amt  "
                + " ,'0.00' KW_Debit_amt1,'0.00' KW_kredit_amt1,  isnull(sum(cast(s2.fin_amt_re as money)), '0.00') as amt1,'0.00' amt2  from KW_Ref_Carta_Akaun cyp   "
                + " left join KW_financial_Year s2 on s2.fin_year='" + curr_yr + "' left join KW_Kategori_akaun kk1 on kk1.kat_cd=cyp.kat_akaun and kk1.kat_type='01'  where cyp.kod_akaun=a1.kod_akaun and cyp.ca_re = '1' and cyp.Status = 'A'   "
                + " group by  cyp.kw_acc_header,cyp.jenis_akaun_type,kk1.kat_cd, kk1.deskripsi, Kk1.bal_type, cyp.nama_akaun, cyp.kod_akaun) as re_1  "
                + " union all select * from(select cyp.kw_acc_header,cyp.jenis_akaun_type,kk1.kat_cd kat_akaun, 'D' kat_type, kk1.deskripsi, Kk1.bal_type, cyp.nama_akaun,   "
                + " cyp.kod_akaun,'0.00' KW_Debit_amt,'0.00' KW_kredit_amt,sum(cast(ISNULL(s2.fin_amt_re, '0.00') as money)) KW_kredit_amt1, cast(ISNULL('0.00', '0.00') as money) KW_Debit_amt1  "
                + " , '0.00' amt1 , isnull(sum(cast(s2.fin_amt_re as money)), '0.00') as amt2  from KW_Ref_Carta_Akaun cyp   "
                + " left join KW_financial_Year s2 on s2.fin_year='" + prev_yr + "' left join KW_Kategori_akaun kk1 on kk1.kat_cd=cyp.kat_akaun and kk1.kat_type='01'  where cyp.kod_akaun=a1.kod_akaun and cyp.ca_re = '1' and cyp.Status = 'A'   "
                + " group by  cyp.kw_acc_header,cyp.jenis_akaun_type,kk1.kat_cd, kk1.deskripsi, Kk1.bal_type, cyp.nama_akaun, cyp.kod_akaun) as re_2 "
                + " ) as re_m group by re_m.kw_acc_header,re_m.jenis_akaun_type,re_m.kat_akaun, re_m.kat_type, re_m.deskripsi, re_m.bal_type, re_m.nama_akaun,  re_m.kod_akaun) as a2 group by a1.kat_akaun, "
                + " a1.kat_type,a1.deskripsi,a1.bal_type,a1.nama_akaun,a1.kod_akaun,a1.jenis_akaun_type,a1.kw_acc_header order by a1.kod_akaun   ";
            }
            else
            {
                qry1 = "with Recurse as ( select a.Id as DirectChildId, a.Id, a.kod_akaun from KW_Ref_Carta_Akaun a  where a.Status='A' and kkk_rep='1'   "
               + " union all select b.DirectChildId, a.Id, a.kod_akaun from KW_Ref_Carta_Akaun a  join Recurse b on b.Id = a.under_parent where a.Status='A' and a.kkk_rep='1') ,   "
               + " tree1 AS(select b.kat_akaun,b.nama_akaun,b.kod_akaun,b.kw_acc_header from (select a.DirectChildId from Recurse a left join KW_General_Ledger b on b.GL_sts='L' and a.kod_akaun = b.kod_akaun  "
               + " group by DirectChildId) as a    inner join  (select m1.Id,m1.jenis_akaun_type,m1.kat_akaun,m1.nama_akaun,m1.kod_akaun,m1.jenis_akaun,m1.under_parent,m1.kw_acc_header from KW_Ref_Carta_Akaun m1 where  "
               + " m1.Status='A' and m1.kkk_rep='1') as b on b.Id=a.DirectChildId) , tree2 AS(select b.kw_acc_header,b.jenis_akaun_type,b.kat_akaun,b.kod_akaun,b.nama_akaun,b.KW_Debit_amt,b.KW_kredit_amt,a.Amount from  "
               + " (select a.DirectChildId, (((ISNULL(cast(s2.opn_kredit_amt as money),'0.00') - ISNULL(cast(s2.opn_debit_amt as money),'0.00')) + (isnull(sum(cast(s2_1.KW_kredit_amt as money)),'0.00') -  "
               + " isnull(sum(cast(s2_1.KW_Debit_amt as money)),'0.00'))) + isnull(sum(cast(b.KW_kredit_amt as money)),'0.00')) - isnull(sum(cast(b.KW_Debit_amt as money)),'0.00')  as Amount  "
               + " from Recurse a "
               + " left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = a.kod_akaun and '" + curr_yr + "' between YEAR(s2.start_dt) and YEAR(s2.end_dt)   "
               + " left join KW_General_Ledger s2_1 on  s2_1.kod_akaun = a.kod_akaun and YEAR(s2_1.GL_post_dt)='" + curr_yr + "' and  s2_1.GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0)   "
               + " left join KW_General_Ledger b on b.kod_akaun = a.kod_akaun and b.GL_sts='L' and b.GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and  "
               + " b.GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by DirectChildId,s2.opn_debit_amt,s2.opn_kredit_amt) as a    "
               + " inner join  (select m1.Id,m1.jenis_akaun_type,m1.kat_akaun,m1.nama_akaun,m1.kod_akaun,m1.jenis_akaun,m1.under_parent,m1.kw_acc_header, "
               + " ((ISNULL(cast(s2.opn_debit_amt as money),'0.00')) + sum(ISNULL(s1.KW_Debit_amt,'0.00'))) as KW_Debit_amt , "
               + " ((ISNULL(cast(s2.opn_kredit_amt as money),'0.00')) + sum(ISNULL(s1.KW_kredit_amt,'0.00'))) as KW_kredit_amt  from KW_Ref_Carta_Akaun m1   "
               + " left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = m1.kod_akaun and '" + curr_yr + "' between YEAR(s2.start_dt) and YEAR(s2.end_dt)   "
               + " left join KW_General_Ledger s1 on s1.kod_akaun=m1.kod_akaun and s1.GL_sts='L' and  "
               + " GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) where m1.Status='A' and m1.kkk_rep='1'  "
               + " group by  m1.Id,m1.jenis_akaun_type,m1.kat_akaun,m1.nama_akaun,m1.kod_akaun,m1.jenis_akaun,m1.under_parent,m1.kw_acc_header,s2.opn_debit_amt,s2.opn_kredit_amt) as b on b.Id=a.DirectChildId   "
               + " where (b.KW_Debit_amt !='0.00' or b.KW_kredit_amt !='0.00' or a.Amount !='0.00'))   "
               + " , tree3 AS(select b.kw_acc_header,b.jenis_akaun_type,b.kat_akaun,b.kod_akaun,b.nama_akaun,b.KW_Debit_amt,b.KW_kredit_amt,a.Amount from  "
               + " (select a.DirectChildId, (((ISNULL(cast(s2.opn_kredit_amt as money),'0.00') - ISNULL(cast(s2.opn_debit_amt as money),'0.00')) + (isnull(sum(cast(s2_1.KW_kredit_amt as money)),'0.00') -  "
               + " isnull(sum(cast(s2_1.KW_Debit_amt as money)),'0.00'))) + isnull(sum(cast(b.KW_kredit_amt as money)),'0.00')) - isnull(sum(cast(b.KW_Debit_amt as money)),'0.00')  as Amount  "
               + " from Recurse a left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = a.kod_akaun and '" + prev_yr + "' between YEAR(s2.start_dt) and YEAR(s2.end_dt)   "
               + " left join KW_General_Ledger s2_1 on  s2_1.kod_akaun = a.kod_akaun and YEAR(s2_1.GL_post_dt)='" + prev_yr + "' and  s2_1.GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + var_fmdate + "'), 0)   "
               + " left join KW_General_Ledger b on b.kod_akaun = a.kod_akaun and b.GL_sts='L' and b.GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + var_fmdate + "'), 0) and  "
               + " b.GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + var_tmdate + "'), +1) group by DirectChildId,s2.opn_debit_amt,s2.opn_kredit_amt) as a    "
               + " inner join  (select m1.Id,m1.jenis_akaun_type,m1.kat_akaun,m1.nama_akaun,m1.kod_akaun,m1.jenis_akaun,m1.under_parent,m1.kw_acc_header, "
               + " ((ISNULL(cast(s2.opn_debit_amt as money),'0.00')) + sum(ISNULL(s1.KW_Debit_amt,'0.00'))) as KW_Debit_amt , "
               + " ((ISNULL(cast(s2.opn_kredit_amt as money),'0.00')) + sum(ISNULL(s1.KW_kredit_amt,'0.00'))) as KW_kredit_amt  from KW_Ref_Carta_Akaun m1   "
               + " left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = m1.kod_akaun and '" + prev_yr + "' between YEAR(s2.start_dt) and YEAR(s2.end_dt)   "
               + " left join KW_General_Ledger s1 on s1.kod_akaun=m1.kod_akaun and s1.GL_sts='L' and  "
               + " GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + var_fmdate + "'), 0) and GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + var_tmdate + "'), +1) where m1.Status='A' and m1.kkk_rep='1'  "
               + " group by  m1.Id,m1.jenis_akaun_type,m1.kat_akaun,m1.nama_akaun,m1.kod_akaun,m1.jenis_akaun,m1.under_parent,m1.kw_acc_header,s2.opn_debit_amt,s2.opn_kredit_amt) as b on b.Id=a.DirectChildId   "
               + " where (b.KW_Debit_amt !='0.00' or b.KW_kredit_amt !='0.00' or a.Amount !='0.00'))    "
               + " select a1.kat_akaun,a1.kat_type,a1.deskripsi,a1.bal_type,a1.nama_akaun,a1.kod_akaun,a1.jenis_akaun_type,ISNULL(a1.kw_acc_header,'0')kw_acc_header, "
               + " sum(a1.KW_Debit_amt) KW_Debit_amt,(sum(a1.KW_kredit_amt) + sum(ISNULL(a2.amt1,'0.00'))) KW_kredit_amt "
               + " ,(((sum(a1.KW_kredit_amt) - sum(a1.KW_Debit_amt))) +sum(ISNULL(a2.amt1,'0.00'))) act_amt1,((sum(a1.KW_kredit_amt1) - sum(a1.KW_Debit_amt1)) + sum(ISNULL(a2.amt2,'0.00'))) act_amt2, "
               + " (sum(a1.KW_kredit_amt1) + sum(ISNULL(a2.amt2,'0.00'))) KW_kredit_amt1,sum(a1.KW_Debit_amt1) KW_Debit_amt1, "
               + " (SUM(a1.Amt1) + sum(ISNULL(a2.amt1,'0.00'))) Amt1,(sum(a1.Amt2) + sum(ISNULL(a2.amt2,'0.00')))Amt2,(sum(a1.Amt3) + sum(ISNULL(a2.amt1,'0.00'))) Amt3,(sum(a1.Amt4) + sum(ISNULL(a2.amt2,'0.00'))) Amt4 from( select * from  "
               + " (select s1.kw_acc_header,s2.jenis_akaun_type,s1.kat_akaun,s5.kat_type,s4.deskripsi,s4.bal_type,s1.nama_akaun,s1.kod_akaun,ISNULL(s2.KW_Debit_amt,'0.00') KW_Debit_amt,ISNULL(s2.KW_kredit_amt,'0.00') KW_kredit_amt, "
               + " ISNULL(s3.KW_Debit_amt,'0.00') KW_Debit_amt1,ISNULL(s3.KW_kredit_amt,'0.00') KW_kredit_amt1,replace(ISNULL(s2.Amount,'0.00'),'-','') Amt1, "
               + " replace(ISNULL(s3.Amount,'0.00'),'-','') Amt2,ISNULL(s2.Amount,'0.00') Amt3,ISNULL(s3.Amount,'0.00') Amt4 from  tree1 s1   "
               + " left join tree2 s2 on s2.kod_akaun=s1.kod_akaun left join tree3 s3 on s3.kod_akaun=s1.kod_akaun left join KW_Kategori_akaun s4 on s4.kat_cd=s1.kat_akaun  "
               + " and kat_type='01' inner join kw_ref_report_1 s5 on s5.kat_cd=s4.kat_rpt_kk and kat_rpt_cd='01' where (s2.Amount !='0.00' or s3.Amount !='0.00')) as a   "
               + " union all   "
               + " select a1.kw_acc_header,a1.jenis_akaun_type,a1.kat_akaun, a1.kat_type, a1.deskripsi, a1.bal_type, a1.nama_akaun,  "
               + " a1.kod_akaun,a1.KW_Debit_amt,a1.KW_kredit_amt,(a1.KW_Debit_amt1 + b1.KW_Debit_amt1) KW_Debit_amt1,(a1.KW_kredit_amt1 + b1.KW_kredit_amt1) KW_kredit_amt1,a1.amt1, "
               + " b1.amt2 as amt2,a1.amt1 as amt3,b1.amt4 as amt4 from(select cyp.kw_acc_header,cyp.jenis_akaun_type,kk1.kat_cd kat_akaun, 'D' kat_type, kk1.deskripsi, Kk1.bal_type, cyp.nama_akaun,  "
               + " cyp.kod_akaun, sum(ISNULL(s1.KW_Debit_amt, '0.00')) KW_Debit_amt, sum(ISNULL(s1.KW_kredit_amt, '0.00')) KW_kredit_amt, '0.00' KW_Debit_amt1,'0.00' KW_kredit_amt1 "
               + " ,  (((ISNULL(cast(s2.opn_kredit_amt as money), '0.00') - ISNULL(cast(s2.opn_debit_amt as money), '0.00')) + (isnull(sum(cast(s2_1.KW_kredit_amt as money)), '0.00') - isnull(sum(cast(s2_1.KW_Debit_amt as money)), '0.00'))) +  "
               + " isnull(sum(cast(s1.KW_kredit_amt as money)), '0.00')) - isnull(sum(cast(s1.KW_Debit_amt as money)), '0.00') as amt1  from KW_Ref_Carta_Akaun m1  "
               + " left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = m1.kod_akaun and '" + curr_yr + "' between YEAR(s2.start_dt) and YEAR(s2.end_dt)   "
               + " left join KW_General_Ledger s2_1 on  s2_1.kod_akaun = m1.kod_akaun and YEAR(s2_1.GL_post_dt) = '" + curr_yr + "' and  s2_1.GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0)   "
               + " left join KW_General_Ledger s1 on s1.kod_akaun = m1.kod_akaun and s1.GL_post_dt >= DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and  "
               + " s1.GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and s1.GL_sts = 'L'  left join KW_Kategori_akaun kk on kk.kat_cd = m1.kat_akaun and kat_type = '01'  "
               + " inner join kw_ref_report_1 s5 on s5.kat_cd = kk.kat_rpt_kk and kat_rpt_cd = '02'  left join KW_Ref_Carta_Akaun cyp on cyp.ca_cyp = '1'   "
               + " left join KW_Kategori_akaun kk1 on kk1.kat_cd=cyp.kat_akaun and kk1.kat_type='01'  where m1.jenis_akaun_type != '1' and m1.Status = 'A'  "
               + " group by  cyp.kw_acc_header,cyp.jenis_akaun_type,kk1.kat_cd, kk1.deskripsi, Kk1.bal_type, cyp.nama_akaun, cyp.kod_akaun,s2.opn_debit_amt,s2.opn_kredit_amt) as a1   "
               + " outer apply (select cyp_m.kw_acc_header,cyp_m.jenis_akaun_type,cyp_m.kat_akaun,cyp_m.kat_type, cyp_m.deskripsi,cyp_m.bal_type, cyp_m.nama_akaun, cyp_m.kod_akaun, "
               + " sum(cast(cyp_m.KW_Debit_amt as money))KW_Debit_amt,SUM(cast(cyp_m.KW_kredit_amt as money)) KW_kredit_amt,(sum(cast(cyp_m.KW_kredit_amt as money)) - sum(cast(cyp_m.KW_Debit_amt as money))) as act_amt1,(sum(cast(cyp_m.KW_kredit_amt1 as money)) - sum(cast(cyp_m.KW_Debit_amt1 as money))) as act_amt2, "
               + " sum(cast(cyp_m.KW_kredit_amt1 as money)) KW_kredit_amt1,sum(cast(cyp_m.KW_Debit_amt1 as money)) KW_Debit_amt1,sum(cast(amt1 as money)) Amt1, sum(cast(amt2 as money)) Amt2,sum(cast(Amt3 as money)) Amt3,sum(cast(Amt4 as money)) Amt4 "
               + " from (select * from (select cyp.kw_acc_header,cyp.jenis_akaun_type,kk1.kat_cd kat_akaun, 'D' kat_type, kk1.deskripsi,  "
               + " Kk1.bal_type, cyp.nama_akaun, cyp.kod_akaun, sum(ISNULL(s1.KW_Debit_amt, '0.00')) KW_Debit_amt, sum(ISNULL(s1.KW_kredit_amt, '0.00')) KW_kredit_amt "
               + " , '0.00' KW_Debit_amt1,'0.00' KW_kredit_amt1,(sum(ISNULL(s1.kw_kredit_amt, '0.00')) - sum(ISNULL(s1.KW_Debit_amt, '0.00'))) act_amt1,	'0.00' act_amt2,     "
               + " ((((ISNULL(cast(s2.opn_kredit_amt as money), '0.00') - ISNULL(cast(s2.opn_debit_amt as money), '0.00')) + (isnull(sum(cast(s2_1.KW_kredit_amt as money)), '0.00') -  "
               + " isnull(sum(cast(s2_1.KW_Debit_amt as money)), '0.00'))) + isnull(sum(cast(s1.KW_kredit_amt as money)), '0.00')) - isnull(sum(cast(s1.KW_Debit_amt as money)), '0.00')) as amt1,'0.00' amt2, "
               + " ('0.00' - ((((ISNULL(cast(s2.opn_kredit_amt as money), '0.00') - ISNULL(cast(s2.opn_debit_amt as money), '0.00')) + (isnull(sum(cast(s2_1.KW_kredit_amt as money)), '0.00') -  "
               + " isnull(sum(cast(s2_1.KW_Debit_amt as money)), '0.00'))) + isnull(sum(cast(s1.KW_kredit_amt as money)), '0.00')) - isnull(sum(cast(s1.KW_Debit_amt as money)), '0.00'))) Amt3,'0.00' Amt4 "
               + " from KW_Ref_Carta_Akaun m1 left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = m1.kod_akaun and '" + curr_yr + "' between YEAR(s2.start_dt) and YEAR(s2.end_dt)   "
               + " left join KW_General_Ledger s2_1 on  s2_1.kod_akaun = m1.kod_akaun and YEAR(s2_1.GL_post_dt) = '" + curr_yr + "' and  s2_1.GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0)   "
               + " left join KW_General_Ledger s1 on s1.kod_akaun = m1.kod_akaun and s1.GL_post_dt >= DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and s1.GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1)  "
               + " and s1.GL_sts = 'L'  left join KW_Kategori_akaun kk on kk.kat_cd = m1.kat_akaun and kat_type = '01' inner join kw_ref_report_1 s5 on s5.kat_cd = kk.kat_rpt_kk and kat_rpt_cd = '02'   "
               + " left join KW_Ref_Carta_Akaun cyp on cyp.ca_cyp = '1'  left join KW_Kategori_akaun kk1 on kk1.kat_cd=cyp.kat_akaun and kk1.kat_type='01'  where m1.jenis_akaun_type != '1' and m1.Status = 'A' group by   "
               + " cyp.kw_acc_header,cyp.jenis_akaun_type,kk1.kat_cd, kk1.deskripsi, Kk1.bal_type, cyp.nama_akaun, cyp.kod_akaun ,s2.opn_debit_amt,s2.opn_kredit_amt) cyp_a "
               + " union all select * from (select cyp.kw_acc_header,cyp.jenis_akaun_type,kk1.kat_cd kat_akaun, 'D' kat_type, kk1.deskripsi, Kk1.bal_type, cyp.nama_akaun, cyp.kod_akaun, '0.00' KW_Debit_amt,'0.00' KW_kredit_amt "
               + " , sum(ISNULL(s1.KW_Debit_amt, '0.00')) KW_Debit_amt1, sum(ISNULL(s1.KW_kredit_amt, '0.00')) KW_kredit_amt1,'0.00' act_amt1,(sum(ISNULL(s1.kw_kredit_amt, '0.00')) - sum(ISNULL(s1.KW_Debit_amt, '0.00'))) act_amt2,'0.00' amt1,	     "
               + " ((((ISNULL(cast(s2.opn_kredit_amt as money), '0.00') - ISNULL(cast(s2.opn_debit_amt as money), '0.00')) + (isnull(sum(cast(s2_1.KW_kredit_amt as money)), '0.00') -  "
               + " isnull(sum(cast(s2_1.KW_Debit_amt as money)), '0.00'))) + isnull(sum(cast(s1.KW_kredit_amt as money)), '0.00')) - isnull(sum(cast(s1.KW_Debit_amt as money)), '0.00')) as amt2, "
               + " '0.00' Amt3,  ('0.00' - ((((ISNULL(cast(s2.opn_kredit_amt as money), '0.00') - ISNULL(cast(s2.opn_debit_amt as money), '0.00')) + (isnull(sum(cast(s2_1.KW_kredit_amt as money)), '0.00') -  "
               + " isnull(sum(cast(s2_1.KW_Debit_amt as money)), '0.00'))) + isnull(sum(cast(s1.KW_kredit_amt as money)), '0.00')) - isnull(sum(cast(s1.KW_Debit_amt as money)), '0.00'))) Amt4 "
               + " from KW_Ref_Carta_Akaun m1 left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = m1.kod_akaun and '' between YEAR(s2.start_dt) and YEAR(s2.end_dt)   "
               + " left join KW_General_Ledger s2_1 on  s2_1.kod_akaun = m1.kod_akaun and YEAR(s2_1.GL_post_dt) = '" + prev_yr + "' and  s2_1.GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + var_fmdate + "'), 0)   "
               + " left join KW_General_Ledger s1 on s1.kod_akaun = m1.kod_akaun and s1.GL_post_dt >= DATEADD(day, DATEDIFF(day, 0, '" + var_fmdate + "'), 0) and s1.GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + var_tmdate + "'), +1)  "
               + " and s1.GL_sts = 'L'  left join KW_Kategori_akaun kk on kk.kat_cd = m1.kat_akaun and kat_type = '01' inner join kw_ref_report_1 s5 on s5.kat_cd = kk.kat_rpt_kk and kat_rpt_cd = '02'   "
               + " left join KW_Ref_Carta_Akaun cyp on cyp.ca_cyp = '1'  left join KW_Kategori_akaun kk1 on kk1.kat_cd=cyp.kat_akaun and kk1.kat_type='01'  where m1.jenis_akaun_type != '1' and m1.Status = 'A' group by   "
               + " cyp.kw_acc_header,cyp.jenis_akaun_type,kk1.kat_cd, kk1.deskripsi, Kk1.bal_type, cyp.nama_akaun, cyp.kod_akaun,s2.opn_debit_amt,s2.opn_kredit_amt) as cyp_b ) as cyp_m group by cyp_m.kw_acc_header,cyp_m.jenis_akaun_type,cyp_m.kat_akaun,cyp_m.kat_type, cyp_m.deskripsi,cyp_m.bal_type, cyp_m.nama_akaun, cyp_m.kod_akaun "
               + " )  as b1 where b1.kod_akaun = a1.kod_akaun) as a1  "
               + " outer apply (select re_m.kw_acc_header,re_m.jenis_akaun_type,re_m.kat_akaun, re_m.kat_type, re_m.deskripsi, re_m.bal_type, re_m.nama_akaun,   "
               + " re_m.kod_akaun,sum(ISNULL(re_m.KW_Debit_amt, '0.00')) KW_Debit_amt,sum(ISNULL(re_m.KW_kredit_amt, '0.00')) KW_kredit_amt, "
               + " sum(ISNULL(re_m.KW_Debit_amt1, '0.00')) KW_Debit_amt1,sum(ISNULL(re_m.KW_kredit_amt1, '0.00')) KW_kredit_amt1,sum(ISNULL(re_m.amt1, '0.00')) amt1,sum(ISNULL(re_m.amt2, '0.00')) amt2 from( "
               + " select * from(select cyp.kw_acc_header,cyp.jenis_akaun_type,kk1.kat_cd kat_akaun, 'D' kat_type, kk1.deskripsi, Kk1.bal_type, cyp.nama_akaun,   "
               + " cyp.kod_akaun, cast(ISNULL('0.00', '0.00') as money) KW_Debit_amt, sum(cast(ISNULL(s2.fin_amt_re, '0.00') as money)) KW_kredit_amt  "
               + " ,'0.00' KW_Debit_amt1,'0.00' KW_kredit_amt1,  isnull(sum(cast(s2.fin_amt_re as money)), '0.00') as amt1,'0.00' amt2  from KW_Ref_Carta_Akaun cyp   "
               + " left join KW_financial_Year s2 on s2.fin_year='" + curr_yr + "' left join KW_Kategori_akaun kk1 on kk1.kat_cd=cyp.kat_akaun and kk1.kat_type='01'  where cyp.kod_akaun=a1.kod_akaun and cyp.ca_re = '1' and cyp.Status = 'A'   "
               + " group by  cyp.kw_acc_header,cyp.jenis_akaun_type,kk1.kat_cd, kk1.deskripsi, Kk1.bal_type, cyp.nama_akaun, cyp.kod_akaun) as re_1  "
               + " union all select * from(select cyp.kw_acc_header,cyp.jenis_akaun_type,kk1.kat_cd kat_akaun, 'D' kat_type, kk1.deskripsi, Kk1.bal_type, cyp.nama_akaun,   "
               + " cyp.kod_akaun,'0.00' KW_Debit_amt,'0.00' KW_kredit_amt,sum(cast(ISNULL(s2.fin_amt_re, '0.00') as money)) KW_kredit_amt1, cast(ISNULL('0.00', '0.00') as money) KW_Debit_amt1  "
               + " , '0.00' amt1 , isnull(sum(cast(s2.fin_amt_re as money)), '0.00') as amt2  from KW_Ref_Carta_Akaun cyp   "
               + " left join KW_financial_Year s2 on s2.fin_year='" + prev_yr + "' left join KW_Kategori_akaun kk1 on kk1.kat_cd=cyp.kat_akaun and kk1.kat_type='01'  where cyp.kod_akaun=a1.kod_akaun and cyp.ca_re = '1' and cyp.Status = 'A'   "
               + " group by  cyp.kw_acc_header,cyp.jenis_akaun_type,kk1.kat_cd, kk1.deskripsi, Kk1.bal_type, cyp.nama_akaun, cyp.kod_akaun) as re_2 "
               + " ) as re_m group by re_m.kw_acc_header,re_m.jenis_akaun_type,re_m.kat_akaun, re_m.kat_type, re_m.deskripsi, re_m.bal_type, re_m.nama_akaun,  re_m.kod_akaun) as a2 group by a1.kat_akaun, "
               + " a1.kat_type,a1.deskripsi,a1.bal_type,a1.nama_akaun,a1.kod_akaun,a1.jenis_akaun_type,a1.kw_acc_header order by a1.kod_akaun   ";
            }
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

                            GridView1.FooterRow.Cells[1].Text = "<strong>JUMLAH KESELURUHAN (RM)</strong>";
                            GridView1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                            GridView1.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                            GridView1.FooterRow.Cells[2].Text = "0.00";
                        }
                        else
                        {
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            Button2.Visible = true;
                            Button3.Visible = true;
                            decimal debit = dt.AsEnumerable().Sum(row => row.Field<decimal>("KW_Debit_amt"));
                            decimal kredit = dt.AsEnumerable().Sum(row => row.Field<decimal>("KW_kredit_amt"));
                            GridView1.FooterRow.Cells[1].Text = "<strong>JUMLAH KESELURUHAN (RM)</strong>";
                            GridView1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                            GridView1.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                            decimal ftr_amt = kredit - debit;
                            GridView1.FooterRow.Cells[2].Text = ftr_amt.ToString("C").Replace("$", "").Replace("RM", "");

                        }

                    }
                }
            }
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        System.Web.UI.WebControls.Label lbl1 = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label1_nw");
        System.Web.UI.WebControls.Label clmn1 = (System.Web.UI.WebControls.Label)e.Row.FindControl("bal_type");
        System.Web.UI.WebControls.Label clmn2 = (System.Web.UI.WebControls.Label)e.Row.FindControl("kat_cd");
        System.Web.UI.WebControls.Label clmn3 = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label3");
        System.Web.UI.WebControls.Label clmn4 = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label4");
        
        System.Web.UI.WebControls.Label hdr = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label2_nw");

        LinkButton Button3 = e.Row.FindControl("LinkButton1") as LinkButton;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if(hdr.Text == "1")
            {
                Button3.Attributes.Add("style", "display:none;");
            }
            else
            {
                Button3.Attributes.Remove("style");
            }

            if (lbl1.Text == "1")
            {
                e.Row.BackColor = Color.FromName("#D4D4D4");
            }
            else if (lbl1.Text == "2")
            {
                e.Row.BackColor = Color.FromName("#D2D7D3");
                clmn1.Attributes.Add("style", "padding-left:10px;");
                clmn2.Attributes.Add("style", "padding-left:10px;");
            }
            else if (lbl1.Text == "3")
            {
                e.Row.BackColor = Color.FromName("#E7E7E7");
                clmn1.Attributes.Add("style", "padding-left:20px;");
                clmn2.Attributes.Add("style", "padding-left:20px;");
            }
            else if (lbl1.Text == "4")
            {
                e.Row.BackColor = Color.FromName("#F5F5F5");
                clmn1.Attributes.Add("style", "padding-left:30px;");
                clmn2.Attributes.Add("style", "padding-left:30px;");
            }
            else if (lbl1.Text == "5")
            {
                e.Row.BackColor = Color.FromName("#F5F5F5");
                clmn1.Attributes.Add("style", "padding-left:40px;");
                clmn2.Attributes.Add("style", "padding-left:40px;");
            }
            else if (lbl1.Text == "6")
            {
                e.Row.BackColor = Color.FromName("#F5F5F5");
                clmn1.Attributes.Add("style", "padding-left:50px;");
                clmn2.Attributes.Add("style", "padding-left:50px;");
            }
            else if (lbl1.Text == "7")
            {
                e.Row.BackColor = Color.FromName("#F5F5F5");
                clmn1.Attributes.Add("style", "padding-left:60px;");
                clmn2.Attributes.Add("style", "padding-left:60px;");
            }
            else if (lbl1.Text == "8")
            {
                e.Row.BackColor = Color.FromName("#F5F5F5");
                clmn1.Attributes.Add("style", "padding-left:70px;");
                clmn2.Attributes.Add("style", "padding-left:70px;");
            }
            if (hdr.Text == "0")
            {
                e.Row.BackColor = Color.FromName("#FFFFFF");
            }         
        }
    }
    protected void clk_submit(object sender, EventArgs e)
    {

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
                    Button2.Visible = true;
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
                    Rptviwerlejar.LocalReport.ReportPath = "Kewengan/KW_Kunci_kira.rdlc";
                    ReportDataSource rds = new ReportDataSource("kwkunci", dt);


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
                    Rptviwerlejar.LocalReport.DisplayName = "Kunci_Kira-Kira_Variance_" + DateTime.Now.ToString("yyyyMMdd");
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

                    Response.AddHeader("content-disposition", "attatchment; filename=Kunci_Kira-Kira_Variance_." + extension);

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
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Tarikh Variance.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
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
                get_pfl = DBCon.Ora_Execute_table("select syar_logo from KW_Profile_syarikat where cur_sts='1' and Status='A' ");

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
                Rptviwerlejar.LocalReport.ReportPath = "Kewengan/KW_Kunci_kira_none.rdlc";
                ReportDataSource rds = new ReportDataSource("kwkunci", dt);


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
                Rptviwerlejar.LocalReport.DisplayName = "Kunci_Kira-Kira_" + DateTime.Now.ToString("yyyyMMdd");
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

                Response.AddHeader("content-disposition", "attatchment; filename=Kunci_Kira-Kira_." + extension);

                Response.BinaryWrite(bytes);

                Response.Flush();

                Response.End();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
            //else if (chk_var2.Checked == true)
            //{
            //    int month;
            //    int i = 0;
            //    string ss1 = string.Empty, ss21 = string.Empty, ss12 = string.Empty;
            //    string get_dt = string.Empty, fmt1 = string.Empty, fmt2 = string.Empty, fmt3 = string.Empty, fmt4 = string.Empty, fmt5 = string.Empty;
            //    DateTime sdate = DateTime.ParseExact(Tk_mula.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //    DateTime edate = DateTime.ParseExact(Tk_akhir.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //    TimeSpan ts = edate.Subtract(sdate);
            //    int months = Convert.ToInt16(Math.Round((double)(ts.Days) / 30.0));
            //    if (months <= 12)
            //    {
            //        while (sdate <= edate)
            //        {
            //            string gtmnth = sdate.Month.ToString().PadLeft(2, '0');
            //            string nemnth = edate.Month.ToString().PadLeft(2, '0');
            //            DateTimeFormatInfo d = new DateTimeFormatInfo();
            //            month = Int32.Parse(gtmnth) - 1;
            //            if (Int32.Parse(gtmnth) != Int32.Parse(nemnth))
            //            {
            //                ss1 = ",";
            //                ss21 = " OR ";
            //                ss12 = "+";
            //            }
            //            else
            //            {
            //                ss1 = "";
            //                ss12 = "";
            //                ss21 = "";
            //            }
            //            string ss2 = sdate.Month.ToString().PadLeft(2, '0');
            //            string ss3 = sdate.ToString("MMM");
            //            var lastDayOfMonth = DateTime.DaysInMonth(sdate.Year, Int32.Parse(ss2));


            //            if (Int32.Parse(gtmnth) != Int32.Parse(nemnth))
            //            {
            //                fmt1 += " left join tree" + i + " s" + i + " on s" + i + ".kod_akaun=sk1.kod_akaun";
            //                fmt2 += ", tree" + i + " AS(select b.kat_akaun,b.kod_akaun,b.nama_akaun,b.KW_Debit_amt,b.KW_kredit_amt,a.Amount from (select a.DirectChildId, (isnull(sum(cast(b.KW_Debit_amt as money)),'0.00') - isnull(sum(cast(b.KW_kredit_amt as money)),'0.00'))  as Amount from Recurse a "
            //                        + " left join KW_General_Ledger b on b.GL_sts='L' and b.kod_akaun = a.kod_akaun where GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/01'), 0) and GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/" + lastDayOfMonth + "'), +1) group by DirectChildId) as a  "
            //                        + " left join  (select m1.Id,m1.jenis_akaun_type,m1.kat_akaun,m1.nama_akaun,m1.kod_akaun,m1.jenis_akaun,m1.under_parent,sum(ISNULL(s1.KW_Debit_amt,'0.00')) as KW_Debit_amt "
            //                        + " ,sum(ISNULL(s1.KW_kredit_amt,'0.00')) as KW_kredit_amt  from KW_Ref_Carta_Akaun m1 "
            //                        + " left join KW_General_Ledger s1 on s1.GL_sts='L' and s1.kod_akaun=m1.kod_akaun and GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/01'), 0) and GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/" + lastDayOfMonth + "'), +1) where m1.Status='A' and m1.kkk_rep='1' group by "
            //                        + " m1.Id,m1.jenis_akaun_type,m1.kat_akaun,m1.nama_akaun,m1.kod_akaun,m1.jenis_akaun,m1.under_parent) as b on b.Id=a.DirectChildId  where (b.KW_Debit_amt !='0.00' or b.KW_kredit_amt !='0.00' or a.Amount !='0.00')) ";
            //                fmt3 += "cast(ISNULL(s" + i + ".Amount,'0.00') as money) " + ss3 + "" + ss1;
            //                fmt4 += "ISNULL(s" + i + ".Amount,'0.00') != '0.00'" + ss21 + "";
            //            }
            //            else
            //            {
            //                fmt1 += " left join tree" + i + " s" + i + " on s" + i + ".kod_akaun=sk1.kod_akaun";
            //                fmt2 += ", tree" + i + " AS(select b.kat_akaun,b.kod_akaun,b.nama_akaun,b.KW_Debit_amt,b.KW_kredit_amt,a.Amount from (select a.DirectChildId, (isnull(sum(cast(b.KW_Debit_amt as money)),'0.00') - isnull(sum(cast(b.KW_kredit_amt as money)),'0.00'))  as Amount from Recurse a "
            //                        + " left join KW_General_Ledger b on b.GL_sts='L' and b.kod_akaun = a.kod_akaun where GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/01'), 0) and GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/" + lastDayOfMonth + "'), +1) group by DirectChildId) as a  "
            //                        + " left join  (select m1.Id,m1.jenis_akaun_type,m1.kat_akaun,m1.nama_akaun,m1.kod_akaun,m1.jenis_akaun,m1.under_parent,sum(ISNULL(s1.KW_Debit_amt,'0.00')) as KW_Debit_amt "
            //                        + " ,sum(ISNULL(s1.KW_kredit_amt,'0.00')) as KW_kredit_amt  from KW_Ref_Carta_Akaun m1 "
            //                        + " left join KW_General_Ledger s1 on s1.GL_sts='L' and s1.kod_akaun=m1.kod_akaun and GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/01'), 0) and GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/" + lastDayOfMonth + "'), +1) where m1.Status='A' and m1.kkk_rep='1' group by "
            //                        + " m1.Id,m1.jenis_akaun_type,m1.kat_akaun,m1.nama_akaun,m1.kod_akaun,m1.jenis_akaun,m1.under_parent) as b on b.Id=a.DirectChildId  where (b.KW_Debit_amt !='0.00' or b.KW_kredit_amt !='0.00' or a.Amount !='0.00')) ";
            //                fmt3 += "cast(ISNULL(s" + i + ".Amount,'0.00') as money) " + ss3 + "" + ss1;
            //                fmt4 += "ISNULL(s" + i + ".Amount,'0.00') != '0.00'" + ss21 + "";
            //            }


            //            //drpdate is dropdownlist
            //            sdate = sdate.AddMonths(1);
            //            i++;
            //        }
            //        if (months < 12)
            //        {
            //            for (int r = 1; r <= (12 - months); r++)
            //            {
            //                //if (r != (12 - months))
            //                //{
            //                    string ss3 = sdate.ToString("MMM");
            //                    fmt3 += ",cast('0.00' as money) " + ss3 + "";
            //                    sdate = sdate.AddMonths(1);
            //                //}
            //                //else
            //                //{
            //                //    string ss3 = sdate.ToString("MMM");
            //                //    fmt3 += "'0.00' " + ss3 + "";
            //                //    sdate = sdate.AddMonths(1);
            //                //}
            //            }
            //        }



            //        qry1 = "with Recurse as ( select a.Id as DirectChildId, a.Id, a.kod_akaun from KW_Ref_Carta_Akaun a  where a.Status='A' and kkk_rep='1' and jenis_akaun_type='2'  union all select b.DirectChildId, a.Id, a.kod_akaun from KW_Ref_Carta_Akaun a "
            //                           + " join Recurse b on b.Id = a.under_parent where a.Status='A' and kkk_rep='1') , "
            //                           + " tree_k AS(select b.kat_akaun,b.nama_akaun,b.kod_akaun from (select a.DirectChildId from Recurse a left join KW_General_Ledger b on b.GL_sts='L' and a.kod_akaun = b.kod_akaun group by DirectChildId) as a  "
            //                           + " Left join  (select m1.Id,m1.jenis_akaun_type,m1.kat_akaun,m1.nama_akaun,m1.kod_akaun,m1.jenis_akaun,m1.under_parent from KW_Ref_Carta_Akaun m1 where m1.Status='A' and m1.kkk_rep='1') as b on b.Id=a.DirectChildId) "
            //                           + "" + fmt2 + " "
            //                           + "select skk1.deskripsi ,sk1.kat_akaun,sk1.nama_akaun,sk1.kod_akaun," + fmt3 + " from  tree_k sk1 " + fmt1 + " left join KW_Kategori_akaun skk1 on skk1.kat_cd=sk1.kat_akaun and skk1.kat_type='01' where (" + fmt4 + ") order by sk1.kod_akaun";

            //        DataSet ds1 = new DataSet();
            //        DataTable dt1 = new DataTable();
            //        dt1 = DBCon.Ora_Execute_table(qry1);
            //        Rptviwerlejar.Reset();
            //        ds1.Tables.Add(dt1);

            //        List<DataRow> listResult1 = dt1.AsEnumerable().ToList();
            //        listResult1.Count();
            //        int countRow1 = 0;
            //        countRow1 = listResult1.Count();

            //        Rptviwerlejar.LocalReport.DataSources.Clear();
            //        if (countRow1 != 0)
            //        {
            //            Button3.Visible = true;
            //            DataTable get_pfl = new DataTable();
            //            get_pfl = DBCon.Ora_Execute_table("select syar_logo from KW_Profile_syarikat where cur_sts='1' and Status='A' and kod_syarikat='1234567-p'");

            //            string imagePath = string.Empty;
            //            if (get_pfl.Rows[0]["syar_logo"].ToString() != "")
            //            {
            //                imagePath = new Uri(Server.MapPath("~/FILES/Profile_syarikat/" + get_pfl.Rows[0]["syar_logo"].ToString() + "")).AbsoluteUri;

            //            }
            //            else
            //            {
            //                imagePath = new Uri(Server.MapPath("~/FILES/Profile_syarikat/user.png")).AbsoluteUri;
            //            }
            //            Rptviwerlejar.LocalReport.EnableExternalImages = true;
            //            Rptviwerlejar.LocalReport.ReportPath = "Kewengan/KW_Kunci_kira_mnth.rdlc";
            //            ReportDataSource rds = new ReportDataSource("kwkunci1", dt1);


                       

            //            ReportParameter[] rptParams = new ReportParameter[16];
            //            rptParams[0] = new ReportParameter("d1", Tk_mula.Text);
            //            rptParams[1] = new ReportParameter("d2", Tk_akhir.Text);
            //            rptParams[2] = new ReportParameter("d3", imagePath);
            //            rptParams[3] = new ReportParameter("d4", Convert.ToString(months));
            //            int param_count = 4;
            //            string param_val = "";
            //            int m = 1;
            //            DateTime sdate1 = DateTime.ParseExact(Tk_mula.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //            DateTime edate1 = DateTime.ParseExact(Tk_akhir.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //            while (sdate1 <= edate1)
            //            {
            //                param_val = "column" + m;
            //                string ss3 = sdate1.ToString("MMM");
            //                rptParams[param_count] = new ReportParameter(param_val, ss3);
            //                sdate1 = sdate1.AddMonths(1);
            //                param_count++;
            //                m++;
            //            }
            //            if (months < 12)
            //            {
            //                for (int r = 0; r <= ((12 - months) -1); r++)
            //                {
            //                    param_val = "column" + m;
            //                    rptParams[param_count + r] = new ReportParameter(param_val, "");
            //                    m++;
            //                }
            //            }
                       

            //            Rptviwerlejar.LocalReport.SetParameters(rptParams);
            //            Rptviwerlejar.LocalReport.DataSources.Add(rds);
            //            Rptviwerlejar.LocalReport.DisplayName = "Kunci_Kira-Kira_Monthly_" + DateTime.Now.ToString("yyyyMMdd");
            //            Rptviwerlejar.LocalReport.Refresh();
            //            System.Threading.Thread.Sleep(1);
            //        }
            //        else
            //        {
            //            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            //        }
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Please Select Maximum 12 Months.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            //    }
            //}
                      
       
    }

    protected void ExportToEXCEL(object sender, EventArgs e)
    {
        if (Tk_mula.Text != "" && Tk_akhir.Text != "")
        {
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

                    if (countRow != 0)
                    {

                        StringBuilder builder = new StringBuilder();
                        string strFileName = string.Format("{0}.{1}", "KUNCI_KIRA-KIRA_" + DateTime.Now.ToString("ddMMyyyy") + "", "csv");
                        DateTime fd1 = DateTime.ParseExact(Tk_mula.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime ed1 = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        builder.Append("KATEGORI KOD,NAMA KATEGORY,KOD AKAUN,NAMA AKAUN," + fd1.ToString("yyyy") + ", " + ed1.ToString("yyyy") + ",  VARIANCE (RM)" + Environment.NewLine);
                        for (int k = 0; k <= (dt.Rows.Count - 1); k++)
                        {
                            double var = (double.Parse(dt.Rows[k]["Amt2"].ToString()) - double.Parse(dt.Rows[k]["Amt1"].ToString()));
                            builder.Append(dt.Rows[k]["kat_akaun"].ToString() + " , " + dt.Rows[k]["deskripsi"].ToString() + ", " + dt.Rows[k]["kod_akaun"].ToString() + "," + dt.Rows[k]["nama_akaun"].ToString().Replace(",", "") + "," + dt.Rows[k]["Amt1"].ToString() + "," + dt.Rows[k]["Amt2"].ToString() + "," + var + Environment.NewLine);

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
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Variance Tarikh.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
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
                    string strFileName = string.Format("{0}.{1}", "KUNCI_KIRA-KIRA_" + DateTime.Now.ToString("ddMMyyyy") + "", "csv");
                    DateTime fd1 = DateTime.ParseExact(Tk_mula.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    builder.Append("KATEGORI KOD,NAMA KATEGORY,KOD AKAUN,NAMA AKAUN," + fd1.ToString("yyyy") + Environment.NewLine);
                    for (int k = 0; k <= (dt.Rows.Count - 1); k++)
                    {
                        double var = (double.Parse(dt.Rows[k]["Amt2"].ToString()) - double.Parse(dt.Rows[k]["Amt1"].ToString()));
                        builder.Append(dt.Rows[k]["kat_akaun"].ToString() + " , " + dt.Rows[k]["deskripsi"].ToString() + ", " + dt.Rows[k]["kod_akaun"].ToString() + "," + dt.Rows[k]["nama_akaun"].ToString().Replace(",", "") + "," + dt.Rows[k]["Amt1"].ToString() + Environment.NewLine);

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
                                    + " left join KW_General_Ledger s1 on s1.GL_sts='L' and s1.kod_akaun=m1.kod_akaun and GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/01'), 0) and GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/" + lastDayOfMonth + "'), +1) where m1.Status='A' and m1.kkk_rep='1' group by "
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
                                    + " left join KW_General_Ledger s1 on s1.GL_sts='L' and s1.kod_akaun=m1.kod_akaun and GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/01'), 0) and GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/" + lastDayOfMonth + "'), +1) where m1.Status='A' and m1.kkk_rep='1' group by "
                                    + " m1.Id,m1.jenis_akaun_type,m1.kat_akaun,m1.nama_akaun,m1.kod_akaun,m1.jenis_akaun,m1.under_parent) as b on b.Id=a.DirectChildId  where (b.KW_Debit_amt !='0.00' or b.KW_kredit_amt !='0.00' or a.Amount !='0.00')) ";
                            fmt3 += "cast(ISNULL(s" + i + ".Amount,'0.00') as money) " + ss3 + "" + ss1;
                            fmt4 += "ISNULL(s" + i + ".Amount,'0.00') != '0.00'" + ss21 + "";
                        }


                        //drpdate is dropdownlist
                        sdate = sdate.AddMonths(1);
                        i++;
                    }
                   



                    qry1 = "with Recurse as ( select a.Id as DirectChildId, a.Id, a.kod_akaun from KW_Ref_Carta_Akaun a  where a.Status='A' and kkk_rep='1' and jenis_akaun_type='2'  union all select b.DirectChildId, a.Id, a.kod_akaun from KW_Ref_Carta_Akaun a "
                                       + " join Recurse b on b.Id = a.under_parent where a.Status='A' and kkk_rep='1') , "
                                       + " tree_k AS(select b.kat_akaun,b.nama_akaun,b.kod_akaun from (select a.DirectChildId from Recurse a left join KW_General_Ledger b on b.GL_sts='L' and a.kod_akaun = b.kod_akaun group by DirectChildId) as a  "
                                       + " Left join  (select m1.Id,m1.jenis_akaun_type,m1.kat_akaun,m1.nama_akaun,m1.kod_akaun,m1.jenis_akaun,m1.under_parent from KW_Ref_Carta_Akaun m1 where m1.Status='A' and m1.kkk_rep='1') as b on b.Id=a.DirectChildId) "
                                       + "" + fmt2 + " "
                                       + "select skk1.deskripsi ,sk1.kat_akaun,sk1.nama_akaun,sk1.kod_akaun," + fmt3 + " from  tree_k sk1 " + fmt1 + " left join KW_Kategori_akaun skk1 on skk1.kat_cd=sk1.kat_akaun and skk1.kat_type='01' where (" + fmt4 + ") order by sk1.kod_akaun";

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
                        string strFileName = string.Format("{0}.{1}", "KUNCI_KIRA-KIRA_" + DateTime.Now.ToString("ddMMyyyy") + "", "csv");
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
                            builder.Append(dt1.Rows[k]["kat_akaun"].ToString() + " , " + dt1.Rows[k]["deskripsi"].ToString() + ", " + dt1.Rows[k]["kod_akaun"].ToString() + "," + dt1.Rows[k]["nama_akaun"].ToString().Replace(",","") + "," + fmt5 + Environment.NewLine);
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


    protected void btn_reset(object sender, EventArgs e)
    {
        Response.Redirect("../kewengan/kw_lep_kunci_kira.aspx");
    }


}