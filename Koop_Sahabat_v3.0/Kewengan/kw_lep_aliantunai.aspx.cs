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

public partial class kw_lep_aliantunai : System.Web.UI.Page
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
        //ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        //scriptManager.RegisterPostBackControl(this.Button4);
        
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
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

    //void BindDropdown()
    //{
    //    DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);

    //    int year = DateTime.Now.Year - 6;

    //    for (int Y = year; Y <= DateTime.Now.Year; Y++)

    //    {

    //        Tahun_kew.Items.Add(new ListItem(Y.ToString(), Y.ToString()));

    //    }

    //    Tahun_kew.SelectedValue = DateTime.Now.Year.ToString();

    //}

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
    }

    protected void clk_submit(object sender, EventArgs e)
    {
        if (Tk_mula.Text != "" && Tk_akhir.Text != "")
        {
            string val1 = string.Empty, val2 = string.Empty, val3 = string.Empty, val4 = string.Empty, val5 = string.Empty, val6 = string.Empty, sqry = string.Empty, py_fdate = string.Empty, py_ldate = string.Empty, curr_yr = string.Empty, prev_yr = string.Empty;

            int min_val = 1;
            string fmdate = string.Empty, tmdate = string.Empty, tmdate1 = string.Empty;            
            string var_fmdate = string.Empty, var_tmdate = string.Empty, var_tmdate1 = string.Empty;
            DateTime fd,td;
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


            if (chk_var1.Checked == true)
            {
                if (TextBox1.Text != "" && TextBox2.Text != "")
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

                   
                    qry1 = "select * from(select '01' mcat_cd1,'01' mcat_cd,a.*,sum(ISNULL(b.Amount,'0.00')) cur_yr_amt,sum(ISNULL(b.Amount1,'0.00')) pre_yr_amt "
                     + " ,cast(replace(sum(ISNULL(b.Amount,'0.00')) ,'-','') as money) cur_yr_amt1,cast(replace(sum(ISNULL(b.Amount1,'0.00')) ,'-','') as money) pre_yr_amt1 "
                     + "  from (select a3.ref_alrn_kod kat_cd,a3.ref_alrn_nama kat_name,a2.ref_alrn_kod hdr_cd,a2.ref_alrn_nama hdr_name,a.ref_alrn_kod item_cd,a.ref_alrn_nama item_name,ISNULL(a1.tmp_kod_akaun,'') kod_Kaun "
                     + " from KW_Ref_aliran_item a  left join kw_template_aliran a1 on a1.ref_alrn_nama=a.ref_alrn_nama left join KW_Ref_aliran_item a2 on a2.Id=a.under_parent and a2.ref_alrn_header='Y' "
                     + " left join KW_Ref_aliran_item a3 on a3.Id=a2.under_parent and a3.ref_alrn_header='K'where a.ref_alrn_header='N') as a "
                     + " outer apply(  "
                     + " select a1.*,a2.Amount1 from (select m1.kod_akaun,(isnull(sum(cast(b.KW_kredit_amt as money)), '0.00') -isnull(sum(cast(b.KW_Debit_amt as money)), '0.00')) as Amount "
                     + " from  KW_Ref_Carta_Akaun m1 left join KW_General_Ledger b on b.kod_akaun = m1.kod_akaun and b.GL_sts = 'L' and b.GL_post_dt >= DATEADD(day, DATEDIFF(day, 0, '"+ fmdate +"'), 0) and  "
                     + " b.GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '"+ tmdate +"'), +1) where m1.kod_akaun=a.kod_Kaun group by m1.kod_akaun) as a1 "
                     + " outer apply(select m1.kod_akaun,(isnull(sum(cast(b.KW_kredit_amt as money)), '0.00') -isnull(sum(cast(b.KW_Debit_amt as money)), '0.00')) as Amount1 "
                     + " from  KW_Ref_Carta_Akaun m1 left join KW_General_Ledger b on b.kod_akaun = m1.kod_akaun and b.GL_sts = 'L' and b.GL_post_dt >= DATEADD(day, DATEDIFF(day, 0, '"+ var_fmdate +"'), 0) and  "
                     + " b.GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '"+ var_tmdate +"'), +1) where m1.kod_akaun=a.kod_Kaun group by m1.kod_akaun "
                     + " ) as a2 ) as b  group by a.kat_cd,a.kat_name,a.hdr_cd,a.hdr_name,a.item_cd,a.item_name,a.kod_Kaun  "
                     + " union all "
                     + " select '01' mcat_cd1,'02' mcat_cd,a.*,sum(ISNULL(b.Amount,'0.00')) cur_yr_amt,sum(ISNULL(b.Amount1,'0.00')) pre_yr_amt,cast(replace(sum(ISNULL(b.Amount,'0.00')) ,'-','') as money) cur_yr_amt1,cast(replace(sum(ISNULL(b.Amount1,'0.00')) ,'-','') as money) pre_yr_amt1 from ( "
                     + " select kat_akaun as kat_cd,s1.deskripsi+' opening' kat_name,'' hdr_cd,'' hdr_name,kod_akaun item_cd,nama_akaun item_name,kod_akaun kod_kaun from KW_Ref_Carta_Akaun  "
                     + " inner join KW_Kategori_akaun s1 on kat_cd=kat_akaun and kat_rpt_kk='01' and kat_type='01' where AT_rep='1') as a "
                     + " outer apply(select a1.*,a2.Amount1 from (select m1.kod_akaun,(sum(ISNULL(cast(s2.opn_kredit_amt as money),'0.00')) - sum(ISNULL(cast(s2.opn_debit_amt as money),'0.00'))) as Amount "
                     + " from  KW_Ref_Carta_Akaun m1 left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = m1.kod_akaun and '"+ curr_yr +"' between YEAR(s2.start_dt)and YEAR(s2.end_dt)   "
                     + " where m1.kod_akaun=a.kod_Kaun group by m1.kod_akaun) as a1 "
                     + " outer apply(select m1.kod_akaun,(sum(ISNULL(cast(s2.opn_kredit_amt as money),'0.00')) - sum(ISNULL(cast(s2.opn_debit_amt as money),'0.00'))) Amount1 "
                     + " from  KW_Ref_Carta_Akaun m1 left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = m1.kod_akaun and '"+ prev_yr +"' between YEAR(s2.start_dt)and YEAR(s2.end_dt)   "
                     + " where m1.kod_akaun=a.kod_Kaun group by m1.kod_akaun ) as a2 ) as b group by a.kat_cd,a.kat_name,a.hdr_cd,a.hdr_name,a.item_cd,a.item_name,a.kod_Kaun  "
                     + " union all "
                     + " select '02' mcat_cd1,'03' mcat_cd,a.*,sum(ISNULL(b.Amount,'0.00')) cur_yr_amt,sum(ISNULL(b.Amount1,'0.00')) pre_yr_amt,cast(replace(sum(ISNULL(b.Amount,'0.00')) ,'-','') as money) cur_yr_amt1,cast(replace(sum(ISNULL(b.Amount1,'0.00')) ,'-','') as money) pre_yr_amt1 from ( "
                     + " select kat_akaun as kat_cd,s1.deskripsi+' txn' kat_name,'' hdr_cd,'' hdr_name,kod_akaun item_cd,nama_akaun item_name,kod_akaun kod_kaun from KW_Ref_Carta_Akaun  "
                     + " inner join KW_Kategori_akaun s1 on kat_cd=kat_akaun and kat_rpt_kk='01' and kat_type='01' where AT_rep='1') as a "
                     + " outer apply( select a1.*,a2.Amount1 from (select m1.kod_akaun, ((("
                     //+ " (sum(ISNULL(cast(s2.opn_kredit_amt as money),'0.00')) - sum(ISNULL(cast(s2.opn_debit_amt as money),'0.00'))) + "
                     + " isnull(sum(cast(s2_1.KW_kredit_amt as money)),'0.00') -isnull(sum(cast(s2_1.KW_Debit_amt as money)), '0.00'))) + "
                     + " isnull(sum(cast(b.KW_kredit_amt as money)), '0.00')) -isnull(sum(cast(b.KW_Debit_amt as money)), '0.00') as Amount from  KW_Ref_Carta_Akaun m1 left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = m1.kod_akaun and '"+ curr_yr +"' between YEAR(s2.start_dt)and YEAR(s2.end_dt)   "
                     + " left join KW_General_Ledger s2_1 on  s2_1.kod_akaun = m1.kod_akaun and YEAR(s2_1.GL_post_dt) = '"+ curr_yr +"' and  s2_1.GL_post_dt < DATEADD(day, DATEDIFF(day, 0, '"+ fmdate +"'), 0)   "
                     + " left join KW_General_Ledger b on b.kod_akaun = m1.kod_akaun and b.GL_sts = 'L' and b.GL_post_dt >= DATEADD(day, DATEDIFF(day, 0, '"+ fmdate +"'), 0) and  "
                     + " b.GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '"+ tmdate +"'), +1) where m1.kod_akaun=a.kod_Kaun group by m1.kod_akaun "
                     + " ) as a1 "
                     + " outer apply(select m1.kod_akaun,((( "
                     //+ " sum(ISNULL(cast(s2.opn_kredit_amt as money),'0.00')) - sum(ISNULL(cast(s2.opn_debit_amt as money),'0.00'))) + ( "
                     + " isnull(sum(cast(s2_1.KW_kredit_amt as money)),'0.00') -isnull(sum(cast(s2_1.KW_Debit_amt as money)), '0.00'))) + "
                     + " isnull(sum(cast(b.KW_kredit_amt as money)), '0.00')) -isnull(sum(cast(b.KW_Debit_amt as money)), '0.00') as Amount1 "
                     + " from  KW_Ref_Carta_Akaun m1 left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = m1.kod_akaun and '"+ prev_yr +"' between YEAR(s2.start_dt)and YEAR(s2.end_dt)   "
                     + " left join KW_General_Ledger s2_1 on  s2_1.kod_akaun = m1.kod_akaun and YEAR(s2_1.GL_post_dt) = '"+ prev_yr +"' and  s2_1.GL_post_dt < DATEADD(day, DATEDIFF(day, 0, '"+ var_fmdate +"'), 0)   "
                     + " left join KW_General_Ledger b on b.kod_akaun = m1.kod_akaun and b.GL_sts = 'L' and b.GL_post_dt >= DATEADD(day, DATEDIFF(day, 0, '"+ var_fmdate +"'), 0) and  "
                     + " b.GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '"+ var_tmdate +"'), +1) where m1.kod_akaun=a.kod_Kaun group by m1.kod_akaun "
                     + " ) as a2 ) as b group by a.kat_cd,a.kat_name,a.hdr_cd,a.hdr_name,a.item_cd,a.item_name,a.kod_Kaun ) as m1 order by m1.mcat_cd,m1.kat_cd,m1.hdr_cd";
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
                        Rptviwerlejar.LocalReport.ReportPath = "Kewengan/kw_lep_ali_new.rdlc";
                        ReportDataSource rds = new ReportDataSource("kwpalnew", dt);


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
                        Rptviwerlejar.LocalReport.DisplayName = "PAT_Variance_" + DateTime.Now.ToString("yyyyMMdd");
                        Rptviwerlejar.LocalReport.Refresh();
                        System.Threading.Thread.Sleep(1);
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


                qry1 = "select * from(select '01' mcat_cd1,'01' mcat_cd,a.kat_cd,a.kat_name,a.hdr_cd,a.hdr_name,a.item_cd,a.item_name,sum(ISNULL(b.Amount,'0.00')) cur_yr_amt,sum(ISNULL(b.Amount1,'0.00')) pre_yr_amt "
                    + " ,cast(replace(sum(ISNULL(b.Amount,'0.00')) ,'-','') as money) cur_yr_amt1,cast(replace(sum(ISNULL(b.Amount1,'0.00')) ,'-','') as money) pre_yr_amt1 "
                    + "  from (select a3.ref_alrn_kod kat_cd,a3.ref_alrn_nama kat_name,a2.ref_alrn_kod hdr_cd,a2.ref_alrn_nama hdr_name,a.ref_alrn_kod item_cd,a.ref_alrn_nama item_name,ISNULL(a1.tmp_kod_akaun,'') kod_Kaun "
                    + " from KW_Ref_aliran_item a  left join kw_template_aliran a1 on a1.ref_alrn_nama=a.ref_alrn_nama left join KW_Ref_aliran_item a2 on a2.Id=a.under_parent and a2.ref_alrn_header='Y' "
                    + " left join KW_Ref_aliran_item a3 on a3.Id=a2.under_parent and a3.ref_alrn_header='K'where a.ref_alrn_header='N') as a "
                    + " outer apply(  "
                    + " select a1.*,a2.Amount1 from (select m1.kod_akaun,(isnull(sum(cast(b.KW_kredit_amt as money)), '0.00') -isnull(sum(cast(b.KW_Debit_amt as money)), '0.00')) as Amount "
                    + " from  KW_Ref_Carta_Akaun m1 left join KW_General_Ledger b on b.kod_akaun = m1.kod_akaun and b.GL_sts = 'L' and b.GL_post_dt >= DATEADD(day, DATEDIFF(day, 0, '"+ fmdate +"'), 0) and  "
                    + " b.GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '"+ tmdate +"'), +1) where m1.kod_akaun=a.kod_Kaun group by m1.kod_akaun) as a1 "
                    + " outer apply(select m1.kod_akaun,(isnull(sum(cast(b.KW_kredit_amt as money)), '0.00') -isnull(sum(cast(b.KW_Debit_amt as money)), '0.00')) as Amount1 "
                    + " from  KW_Ref_Carta_Akaun m1 left join KW_General_Ledger b on b.kod_akaun = m1.kod_akaun and b.GL_sts = 'L' and b.GL_post_dt >= DATEADD(day, DATEDIFF(day, 0, '"+ var_fmdate +"'), 0) and  "
                    + " b.GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '"+ var_tmdate +"'), +1) where m1.kod_akaun=a.kod_Kaun group by m1.kod_akaun "
                    + " ) as a2 ) as b  group by a.kat_cd,a.kat_name,a.hdr_cd,a.hdr_name,a.item_cd,a.item_name  "
                    + " union all "
                    + " select '01' mcat_cd1,'02' mcat_cd,a.kat_cd,a.kat_name,a.hdr_cd,a.hdr_name,a.item_cd,a.item_name,sum(ISNULL(b.Amount,'0.00')) cur_yr_amt,sum(ISNULL(b.Amount1,'0.00')) pre_yr_amt,cast(replace(sum(ISNULL(b.Amount,'0.00')) ,'-','') as money) cur_yr_amt1,cast(replace(sum(ISNULL(b.Amount1,'0.00')) ,'-','') as money) pre_yr_amt1 from ( "
                    + " select kat_akaun as kat_cd,s1.deskripsi+' opening' kat_name,'' hdr_cd,'' hdr_name,kod_akaun item_cd,nama_akaun item_name,kod_akaun kod_kaun from KW_Ref_Carta_Akaun  "
                    + " inner join KW_Kategori_akaun s1 on kat_cd=kat_akaun and kat_rpt_kk='01' and kat_type='01' where AT_rep='1') as a "
                    + " outer apply(select a1.*,a2.Amount1 from (select m1.kod_akaun,(sum(ISNULL(cast(s2.opn_kredit_amt as money),'0.00')) - sum(ISNULL(cast(s2.opn_debit_amt as money),'0.00'))) as Amount "
                    + " from  KW_Ref_Carta_Akaun m1 left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = m1.kod_akaun and '"+ curr_yr +"' between YEAR(s2.start_dt)and YEAR(s2.end_dt)   "
                    + " where m1.kod_akaun=a.kod_Kaun group by m1.kod_akaun) as a1 "
                    + " outer apply(select m1.kod_akaun,(sum(ISNULL(cast(s2.opn_kredit_amt as money),'0.00')) - sum(ISNULL(cast(s2.opn_debit_amt as money),'0.00'))) Amount1 "
                    + " from  KW_Ref_Carta_Akaun m1 left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = m1.kod_akaun and '"+ prev_yr +"' between YEAR(s2.start_dt)and YEAR(s2.end_dt)   "
                    + " where m1.kod_akaun=a.kod_Kaun group by m1.kod_akaun ) as a2 ) as b group by a.kat_cd,a.kat_name,a.hdr_cd,a.hdr_name,a.item_cd,a.item_name  "
                    + " union all "
                    + " select '02' mcat_cd1,'03' mcat_cd,a.kat_cd,a.kat_name,a.hdr_cd,a.hdr_name,a.item_cd,a.item_name,sum(ISNULL(b.Amount,'0.00')) cur_yr_amt,sum(ISNULL(b.Amount1,'0.00')) pre_yr_amt,cast(replace(sum(ISNULL(b.Amount,'0.00')) ,'-','') as money) cur_yr_amt1,cast(replace(sum(ISNULL(b.Amount1,'0.00')) ,'-','') as money) pre_yr_amt1 from ( "
                    + " select kat_akaun as kat_cd,s1.deskripsi+' txn' kat_name,'' hdr_cd,'' hdr_name,kod_akaun item_cd,nama_akaun item_name,kod_akaun kod_kaun from KW_Ref_Carta_Akaun  "
                    + " inner join KW_Kategori_akaun s1 on kat_cd=kat_akaun and kat_rpt_kk='01' and kat_type='01' where AT_rep='1') as a "
                    + " outer apply( select a1.*,a2.Amount1 from (select m1.kod_akaun,(( "
                    //+ " (sum(ISNULL(cast(s2.opn_kredit_amt as money),'0.00')) - sum(ISNULL(cast(s2.opn_debit_amt as money),'0.00'))) + "
                    + " (isnull(sum(cast(s2_1.KW_kredit_amt as money)),'0.00') -isnull(sum(cast(s2_1.KW_Debit_amt as money)), '0.00'))) + "
                    + " isnull(sum(cast(b.KW_kredit_amt as money)), '0.00')) -isnull(sum(cast(b.KW_Debit_amt as money)), '0.00') as Amount from  KW_Ref_Carta_Akaun m1 left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = m1.kod_akaun and '"+ curr_yr +"' between YEAR(s2.start_dt)and YEAR(s2.end_dt)   "
                    + " left join KW_General_Ledger s2_1 on  s2_1.kod_akaun = m1.kod_akaun and YEAR(s2_1.GL_post_dt) = '"+ curr_yr +"' and  s2_1.GL_post_dt < DATEADD(day, DATEDIFF(day, 0, '"+ fmdate +"'), 0)   "
                    + " left join KW_General_Ledger b on b.kod_akaun = m1.kod_akaun and b.GL_sts = 'L' and b.GL_post_dt >= DATEADD(day, DATEDIFF(day, 0, '"+ fmdate +"'), 0) and  "
                    + " b.GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '"+ tmdate +"'), +1) where m1.kod_akaun=a.kod_Kaun group by m1.kod_akaun "
                    + " ) as a1 "
                    + " outer apply(select m1.kod_akaun,((( "
                    //+ " sum(ISNULL(cast(s2.opn_kredit_amt as money),'0.00')) - sum(ISNULL(cast(s2.opn_debit_amt as money),'0.00'))) + ("
                    + " isnull(sum(cast(s2_1.KW_kredit_amt as money)),'0.00') -isnull(sum(cast(s2_1.KW_Debit_amt as money)), '0.00'))) + "
                    + " isnull(sum(cast(b.KW_kredit_amt as money)), '0.00')) -isnull(sum(cast(b.KW_Debit_amt as money)), '0.00') as Amount1 "
                    + " from  KW_Ref_Carta_Akaun m1 left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = m1.kod_akaun and '"+ prev_yr +"' between YEAR(s2.start_dt)and YEAR(s2.end_dt)   "
                    + " left join KW_General_Ledger s2_1 on  s2_1.kod_akaun = m1.kod_akaun and YEAR(s2_1.GL_post_dt) = '"+ prev_yr +"' and  s2_1.GL_post_dt < DATEADD(day, DATEDIFF(day, 0, '"+ var_fmdate +"'), 0)   "
                    + " left join KW_General_Ledger b on b.kod_akaun = m1.kod_akaun and b.GL_sts = 'L' and b.GL_post_dt >= DATEADD(day, DATEDIFF(day, 0, '"+ var_fmdate +"'), 0) and  "
                    + " b.GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '"+ var_tmdate +"'), +1) where m1.kod_akaun=a.kod_Kaun group by m1.kod_akaun "
                    + " ) as a2 ) as b group by a.kat_cd,a.kat_name,a.hdr_cd,a.hdr_name,a.item_cd,a.item_name ) as m1 order by m1.mcat_cd,m1.kat_cd,m1.hdr_cd";
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
                    Rptviwerlejar.LocalReport.ReportPath = "Kewengan/kw_lep_ali_new_none.rdlc";
                    ReportDataSource rds = new ReportDataSource("kwpalnew", dt);


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
                    Rptviwerlejar.LocalReport.DisplayName = "PAT_Variance_" + DateTime.Now.ToString("yyyyMMdd");
                    Rptviwerlejar.LocalReport.Refresh();
                    System.Threading.Thread.Sleep(1);
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
                            fmt1 += " left join tree" + i + " s" + i + " on s" + i + ".ref_alrn_kod=m_sk1.ref_alrn_kod";
                            fmt2 += ", tree" + i + " AS(select b.ref_alrn_kod,b.ref_alrn_nama,b.KW_Debit_amt,b.KW_kredit_amt,a.Amount from (select a.DirectChildId, (isnull(sum(cast(b.KW_Debit_amt as money)), '0.00') - isnull(sum(cast(b.KW_kredit_amt as money)), '0.00')) as Amount from Recurse a "
                                    + " inner join kw_template_aliran k1 on k1.ref_alrn_nama=a.ref_alrn_kod  inner join KW_General_Ledger b on b.kod_akaun = k1.tmp_kod_akaun where b.GL_sts = 'L' and GL_post_dt>= DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/01'), 0) and GL_post_dt<= DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/" + lastDayOfMonth + "'), +1) group by DirectChildId) as a "
                                    + " inner join  (select m1.Id,m1.ref_alrn_nama,m1.ref_alrn_kod,m1.under_parent,sum(ISNULL(s1.KW_Debit_amt,'0.00')) as KW_Debit_amt "
                                    + " ,sum(ISNULL(s1.KW_kredit_amt,'0.00')) as KW_kredit_amt  from KW_Ref_aliran_item m1 inner join kw_template_aliran k2 on k2.ref_alrn_nama=m1.ref_alrn_kod and k2.tmp_Status='Y' "
                                    + " inner join KW_General_Ledger s1 on s1.kod_akaun=k2.tmp_kod_akaun and s1.GL_sts='L' and GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/01'), 0) and GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/" + lastDayOfMonth + "'), +1) group by "
                                    + "  m1.Id,m1.ref_alrn_nama,m1.ref_alrn_kod,m1.under_parent) as b on b.Id=a.DirectChildId  where (b.KW_Debit_amt !='0.00' or b.KW_kredit_amt !='0.00' or a.Amount !='0.00')) ";
                            fmt3 += "cast(ISNULL(s" + i + ".Amount,'0.00') as money) " + ss3 + "" + ss1;
                            fmt4 += "ISNULL(s" + i + ".Amount,'0.00') != '0.00'" + ss21 + "";
                        }
                        else
                        {
                            fmt1 += " left join tree" + i + " s" + i + " on s" + i + ".ref_alrn_kod=m_sk1.ref_alrn_kod";
                            fmt2 += ", tree" + i + " AS(select b.ref_alrn_kod,b.ref_alrn_nama,b.KW_Debit_amt,b.KW_kredit_amt,a.Amount from (select a.DirectChildId, (isnull(sum(cast(b.KW_Debit_amt as money)), '0.00') - isnull(sum(cast(b.KW_kredit_amt as money)), '0.00')) as Amount from Recurse a "
                                    + " inner join kw_template_aliran k1 on k1.ref_alrn_nama=a.ref_alrn_kod  inner join KW_General_Ledger b on b.kod_akaun = k1.tmp_kod_akaun where b.GL_sts = 'L' and GL_post_dt>= DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/01'), 0) and GL_post_dt<= DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/" + lastDayOfMonth + "'), +1) group by DirectChildId) as a "
                                    + " inner join  (select m1.Id,m1.ref_alrn_nama,m1.ref_alrn_kod,m1.under_parent,sum(ISNULL(s1.KW_Debit_amt,'0.00')) as KW_Debit_amt "
                                    + " ,sum(ISNULL(s1.KW_kredit_amt,'0.00')) as KW_kredit_amt  from KW_Ref_aliran_item m1 inner join kw_template_aliran k2 on k2.ref_alrn_nama=m1.ref_alrn_kod and k2.tmp_Status='Y' "
                                    + " inner join KW_General_Ledger s1 on s1.kod_akaun=k2.tmp_kod_akaun and s1.GL_sts='L' and GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/01'), 0) and GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/" + lastDayOfMonth + "'), +1) group by "
                                    + "  m1.Id,m1.ref_alrn_nama,m1.ref_alrn_kod,m1.under_parent) as b on b.Id=a.DirectChildId  where (b.KW_Debit_amt !='0.00' or b.KW_kredit_amt !='0.00' or a.Amount !='0.00')) ";
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



                    qry1 = "with Recurse as ( select a.Id as DirectChildId, a.Id, a.ref_alrn_kod from KW_Ref_aliran_item a where ref_alrn_header='N' union all select b.DirectChildId, a.Id, a.ref_alrn_kod from KW_Ref_aliran_item a"
                                       + "  join Recurse b on b.Id = a.under_parent where a.ref_alrn_header='Y') , "
                                       + " tree_k AS(select b.ref_alrn_nama,b.ref_alrn_kod from (select a.DirectChildId from Recurse a inner join kw_template_aliran k1 on k1.ref_alrn_nama=a.ref_alrn_kod inner join KW_General_Ledger b on b.GL_sts='L' and k1.tmp_kod_akaun = b.kod_akaun group by DirectChildId) as a  "
                                       + " inner join  (select m1.Id,m1.ref_alrn_nama,m1.ref_alrn_kod,m1.under_parent from KW_Ref_aliran_item m1 left join kw_template_aliran k2 on k2.ref_alrn_nama=m1.ref_alrn_kod and k2.tmp_Status='Y' where m1.ref_alrn_header='N') as b on b.Id = a.DirectChildId) "
                                       + "" + fmt2 + " "
                                       + " select distinct sk2.ref_alrn_kod as kat_akaun,sk2.ref_alrn_nama as deskripsi,m_sk1.ref_alrn_nama as nama_akaun,m_sk1.ref_alrn_kod as kod_akaun," + fmt3 + " from  tree_k m_sk1 " + fmt1 + " left join KW_Ref_aliran_item sk1 on sk1.ref_alrn_kod=m_sk1.ref_alrn_kod left join KW_Ref_aliran_item sk2 on sk2.Id=sk1.under_parent where (" + fmt4 + ") and ISNULL(sk2.ref_alrn_kod,'') != '' order by m_sk1.ref_alrn_kod ";

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
                        Rptviwerlejar.LocalReport.ReportPath = "Kewengan/kw_lep_ali_new_mnth.rdlc";
                        ReportDataSource rds = new ReportDataSource("kwpalnew1", dt1);


                       

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
                        Rptviwerlejar.LocalReport.DisplayName = "PAT_Monthly_" + DateTime.Now.ToString("yyyyMMdd");
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
            string val1 = string.Empty, val2 = string.Empty, val3 = string.Empty, val4 = string.Empty, val5 = string.Empty, val6 = string.Empty, sqry = string.Empty, py_fdate = string.Empty, py_ldate = string.Empty, curr_yr = string.Empty, prev_yr = string.Empty;

            int min_val = 1;
            string fmdate = string.Empty, tmdate = string.Empty, tmdate1 = string.Empty;
            string var_fmdate = string.Empty, var_tmdate = string.Empty, var_tmdate1 = string.Empty;
            DateTime fd, td;
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

           

            if (chk_var1.Checked == true)
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

                qry1 = "with Recurse as ( select a.Id as DirectChildId, a.Id, a.ref_alrn_kod from KW_Ref_aliran_item a where ref_alrn_header='N' "
                    + " union all select b.DirectChildId, a.Id, a.ref_alrn_kod from KW_Ref_aliran_item a  join Recurse b on b.Id = a.under_parent where a.ref_alrn_header = 'Y') ,   "
                    + " tree1 AS(select b.ref_alrn_nama, b.ref_alrn_kod from(select a.DirectChildId from Recurse a left join kw_template_aliran k1 on k1.ref_alrn_nama = a.ref_alrn_kod left join KW_General_Ledger b on b.GL_sts = 'L' and k1.tmp_kod_akaun = b.kod_akaun "
                    + " group by DirectChildId) as a  "
                    + " inner join(select m1.Id, m1.ref_alrn_nama, m1.ref_alrn_kod, m1.under_parent from KW_Ref_aliran_item m1 left join kw_template_aliran k2 on k2.ref_alrn_nama = m1.ref_alrn_kod and k2.tmp_Status = 'Y' where m1.ref_alrn_header = 'N') as b "
                    + " on b.Id = a.DirectChildId)  , tree2 AS(select b.ref_alrn_kod, b.ref_alrn_nama, b.KW_Debit_amt, b.KW_kredit_amt, a.Amount from(select a.DirectChildId, "
                    + " (((sum(ISNULL(cast(s2.opn_kredit_amt as money),'0.00')) - sum(ISNULL(cast(s2.opn_debit_amt as money),'0.00'))) + (isnull(sum(cast(s2_1.KW_kredit_amt as money)),'0.00') -isnull(sum(cast(s2_1.KW_Debit_amt as money)), '0.00'))) +isnull(sum(cast(b.KW_kredit_amt as money)), '0.00')) -isnull(sum(cast(b.KW_Debit_amt as money)), '0.00') as Amount from Recurse a "
                    + " left join kw_template_aliran k1 on k1.ref_alrn_nama = a.ref_alrn_kod  "
                    + " left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = k1.tmp_kod_akaun and '" + prev_yr + "' between YEAR(s2.start_dt)and YEAR(s2.end_dt) "
                    + " left join KW_General_Ledger s2_1 on  s2_1.kod_akaun = k1.tmp_kod_akaun and YEAR(s2_1.GL_post_dt) = '" + prev_yr + "' and  s2_1.GL_post_dt < DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) "
                    + " left join KW_General_Ledger b on b.kod_akaun = k1.tmp_kod_akaun where b.GL_sts = 'L' and b.GL_post_dt >= DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and b.GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by DirectChildId) as "
                    + " a   inner join(select m1.Id, m1.ref_alrn_nama, m1.ref_alrn_kod, m1.under_parent, sum(ISNULL(s1.KW_Debit_amt, '0.00')) as KW_Debit_amt, "
                    + " sum(ISNULL(s1.KW_kredit_amt, '0.00')) as KW_kredit_amt  from KW_Ref_aliran_item m1 inner join kw_template_aliran k2 on k2.ref_alrn_nama = m1.ref_alrn_kod and k2.tmp_Status = 'Y' "
                    + " inner join KW_General_Ledger s1 on s1.kod_akaun = k2.tmp_kod_akaun and s1.GL_sts = 'L' and GL_post_dt >= DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) "
                    + " group by  m1.Id, m1.ref_alrn_nama, m1.ref_alrn_kod, m1.under_parent) as b on b.Id = a.DirectChildId  )  ,  "
                    + " tree3 AS(select b.ref_alrn_kod, b.ref_alrn_nama, b.KW_Debit_amt, b.KW_kredit_amt, a.Amount from(select a.DirectChildId, (((sum(ISNULL(cast(s2.opn_kredit_amt as money),'0.00')) - sum(ISNULL(cast(s2.opn_debit_amt as money),'0.00'))) + (isnull(sum(cast(s2_1.KW_kredit_amt as money)),'0.00') -isnull(sum(cast(s2_1.KW_Debit_amt as money)), '0.00'))) +isnull(sum(cast(b.KW_kredit_amt as money)), '0.00')) -isnull(sum(cast(b.KW_Debit_amt as money)), '0.00') as Amount from Recurse a "
                    + " left join kw_template_aliran k1 on k1.ref_alrn_nama = a.ref_alrn_kod "
                     + " left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = k1.tmp_kod_akaun and '" + prev_yr + "' between YEAR(s2.start_dt)and YEAR(s2.end_dt) "
                    + " left join KW_General_Ledger s2_1 on  s2_1.kod_akaun = k1.tmp_kod_akaun and YEAR(s2_1.GL_post_dt) = '" + prev_yr + "' and  s2_1.GL_post_dt < DATEADD(day, DATEDIFF(day, 0, '" + var_fmdate + "'), 0) "
                    + "  left join KW_General_Ledger b on k1.tmp_kod_akaun = b.kod_akaun where b.GL_sts = 'L' and b.GL_post_dt >= DATEADD(day, DATEDIFF(day, 0, '" + var_fmdate + "'), 0) and b.GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + var_tmdate + "'), +1) group by DirectChildId) as "
                    + " a   inner join(select m1.Id, m1.ref_alrn_nama, m1.ref_alrn_kod, m1.under_parent, sum(ISNULL(s1.KW_Debit_amt, '0.00')) as KW_Debit_amt, "
                    + " sum(ISNULL(s1.KW_kredit_amt, '0.00')) as KW_kredit_amt  from KW_Ref_aliran_item m1 inner join kw_template_aliran k2 on k2.ref_alrn_nama = m1.ref_alrn_kod and k2.tmp_Status = 'Y' "
                    + " inner join KW_General_Ledger s1 on s1.kod_akaun = k2.tmp_kod_akaun and s1.GL_sts = 'L' and GL_post_dt >= DATEADD(day, DATEDIFF(day, 0, '" + var_fmdate + "'), 0) and GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + var_tmdate + "'), +1)  group by "
                    + " m1.Id, m1.ref_alrn_nama, m1.ref_alrn_kod, m1.under_parent) as b on b.Id = a.DirectChildId )   "
                    + " select distinct sk2.ref_alrn_kod as kat_akaun,sk2.ref_alrn_nama as deskripsi,s1.ref_alrn_nama as nama_akaun,s1.ref_alrn_kod as kod_akaun, ISNULL(s2.Amount, '0.00') Amt1,ISNULL(s3.Amount, '0.00') Amt2 from  tree1 s1  left join tree2 s2 on s2.ref_alrn_kod = s1.ref_alrn_kod left join tree3 s3 on s3.ref_alrn_kod = s1.ref_alrn_kod "
                    + " left join KW_Ref_aliran_item sk1 on sk1.ref_alrn_kod = s1.ref_alrn_kod left join KW_Ref_aliran_item sk2 on sk2.Id = sk1.under_parent  where ISNULL(sk2.ref_alrn_kod,'') != '' order by s1.ref_alrn_kod ";

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
                    string strFileName = string.Format("{0}.{1}", "PAT_Variance_" + DateTime.Now.ToString("ddMMyyyy") + "", "csv");
                  DateTime fd1 = DateTime.ParseExact(Tk_mula.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime ed1 = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    builder.Append("KATEGORI KOD,NAMA KATEGORY,KOD AKAUN,NAMA AKAUN,"+ fd1.ToString("yyyy") + ", " + ed1.ToString("yyyy") + ",  VARIANCE (RM)" + Environment.NewLine);
                        for (int k = 0; k <= (dt.Rows.Count - 1); k++)
                        {
                        double var = (double.Parse(dt.Rows[k]["Amt1"].ToString()) - double.Parse(dt.Rows[k]["Amt2"].ToString()));
                            builder.Append(dt.Rows[k]["kat_akaun"].ToString() + " , " + dt.Rows[k]["deskripsi"].ToString() + ", " + dt.Rows[k]["kod_akaun"].ToString() + "," + dt.Rows[k]["nama_akaun"].ToString().Replace(",", "") + "," + dt.Rows[k]["Amt1"].ToString() + "," + dt.Rows[k]["Amt2"].ToString() + "," + var  + Environment.NewLine);

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

                qry1 = "with Recurse as ( select a.Id as DirectChildId, a.Id, a.ref_alrn_kod from KW_Ref_aliran_item a where ref_alrn_header='N' "
                  + " union all select b.DirectChildId, a.Id, a.ref_alrn_kod from KW_Ref_aliran_item a  join Recurse b on b.Id = a.under_parent where a.ref_alrn_header = 'Y') ,   "
                  + " tree1 AS(select b.ref_alrn_nama, b.ref_alrn_kod from(select a.DirectChildId from Recurse a left join kw_template_aliran k1 on k1.ref_alrn_nama = a.ref_alrn_kod left join KW_General_Ledger b on b.GL_sts = 'L' and k1.tmp_kod_akaun = b.kod_akaun "
                  + " group by DirectChildId) as a  "
                  + " inner join(select m1.Id, m1.ref_alrn_nama, m1.ref_alrn_kod, m1.under_parent from KW_Ref_aliran_item m1 left join kw_template_aliran k2 on k2.ref_alrn_nama = m1.ref_alrn_kod and k2.tmp_Status = 'Y' where m1.ref_alrn_header = 'N') as b "
                  + " on b.Id = a.DirectChildId)  , tree2 AS(select b.ref_alrn_kod, b.ref_alrn_nama, b.KW_Debit_amt, b.KW_kredit_amt, a.Amount from(select a.DirectChildId, "
                  + " (((sum(ISNULL(cast(s2.opn_kredit_amt as money),'0.00')) - sum(ISNULL(cast(s2.opn_debit_amt as money),'0.00'))) + (isnull(sum(cast(s2_1.KW_kredit_amt as money)),'0.00') -isnull(sum(cast(s2_1.KW_Debit_amt as money)), '0.00'))) +isnull(sum(cast(b.KW_kredit_amt as money)), '0.00')) -isnull(sum(cast(b.KW_Debit_amt as money)), '0.00') as Amount from Recurse a "
                  + " left join kw_template_aliran k1 on k1.ref_alrn_nama = a.ref_alrn_kod  "
                  + " left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = k1.tmp_kod_akaun and '" + prev_yr + "' between YEAR(s2.start_dt)and YEAR(s2.end_dt) "
                  + " left join KW_General_Ledger s2_1 on  s2_1.kod_akaun = k1.tmp_kod_akaun and YEAR(s2_1.GL_post_dt) = '" + prev_yr + "' and  s2_1.GL_post_dt < DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) "
                  + " left join KW_General_Ledger b on b.kod_akaun = k1.tmp_kod_akaun where b.GL_sts = 'L' and b.GL_post_dt >= DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and b.GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by DirectChildId) as "
                  + " a   inner join(select m1.Id, m1.ref_alrn_nama, m1.ref_alrn_kod, m1.under_parent, sum(ISNULL(s1.KW_Debit_amt, '0.00')) as KW_Debit_amt, "
                  + " sum(ISNULL(s1.KW_kredit_amt, '0.00')) as KW_kredit_amt  from KW_Ref_aliran_item m1 inner join kw_template_aliran k2 on k2.ref_alrn_nama = m1.ref_alrn_kod and k2.tmp_Status = 'Y' "
                  + " inner join KW_General_Ledger s1 on s1.kod_akaun = k2.tmp_kod_akaun and s1.GL_sts = 'L' and GL_post_dt >= DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) "
                  + " group by  m1.Id, m1.ref_alrn_nama, m1.ref_alrn_kod, m1.under_parent) as b on b.Id = a.DirectChildId  )  ,  "
                  + " tree3 AS(select b.ref_alrn_kod, b.ref_alrn_nama, b.KW_Debit_amt, b.KW_kredit_amt, a.Amount from(select a.DirectChildId, (((sum(ISNULL(cast(s2.opn_kredit_amt as money),'0.00')) - sum(ISNULL(cast(s2.opn_debit_amt as money),'0.00'))) + (isnull(sum(cast(s2_1.KW_kredit_amt as money)),'0.00') -isnull(sum(cast(s2_1.KW_Debit_amt as money)), '0.00'))) +isnull(sum(cast(b.KW_kredit_amt as money)), '0.00')) -isnull(sum(cast(b.KW_Debit_amt as money)), '0.00') as Amount from Recurse a "
                  + " left join kw_template_aliran k1 on k1.ref_alrn_nama = a.ref_alrn_kod "
                   + " left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = k1.tmp_kod_akaun and '" + prev_yr + "' between YEAR(s2.start_dt)and YEAR(s2.end_dt) "
                  + " left join KW_General_Ledger s2_1 on  s2_1.kod_akaun = k1.tmp_kod_akaun and YEAR(s2_1.GL_post_dt) = '" + prev_yr + "' and  s2_1.GL_post_dt < DATEADD(day, DATEDIFF(day, 0, '" + var_fmdate + "'), 0) "
                  + "  left join KW_General_Ledger b on k1.tmp_kod_akaun = b.kod_akaun where b.GL_sts = 'L' and b.GL_post_dt >= DATEADD(day, DATEDIFF(day, 0, '" + var_fmdate + "'), 0) and b.GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + var_tmdate + "'), +1) group by DirectChildId) as "
                  + " a   inner join(select m1.Id, m1.ref_alrn_nama, m1.ref_alrn_kod, m1.under_parent, sum(ISNULL(s1.KW_Debit_amt, '0.00')) as KW_Debit_amt, "
                  + " sum(ISNULL(s1.KW_kredit_amt, '0.00')) as KW_kredit_amt  from KW_Ref_aliran_item m1 inner join kw_template_aliran k2 on k2.ref_alrn_nama = m1.ref_alrn_kod and k2.tmp_Status = 'Y' "
                  + " inner join KW_General_Ledger s1 on s1.kod_akaun = k2.tmp_kod_akaun and s1.GL_sts = 'L' and GL_post_dt >= DATEADD(day, DATEDIFF(day, 0, '" + var_fmdate + "'), 0) and GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + var_tmdate + "'), +1)  group by "
                  + " m1.Id, m1.ref_alrn_nama, m1.ref_alrn_kod, m1.under_parent) as b on b.Id = a.DirectChildId )   "
                  + " select distinct sk2.ref_alrn_kod as kat_akaun,sk2.ref_alrn_nama as deskripsi,s1.ref_alrn_nama as nama_akaun,s1.ref_alrn_kod as kod_akaun, ISNULL(s2.Amount, '0.00') Amt1,ISNULL(s3.Amount, '0.00') Amt2 from  tree1 s1  left join tree2 s2 on s2.ref_alrn_kod = s1.ref_alrn_kod left join tree3 s3 on s3.ref_alrn_kod = s1.ref_alrn_kod "
                  + " left join KW_Ref_aliran_item sk1 on sk1.ref_alrn_kod = s1.ref_alrn_kod left join KW_Ref_aliran_item sk2 on sk2.Id = sk1.under_parent  where ISNULL(sk2.ref_alrn_kod,'') != '' order by s1.ref_alrn_kod ";
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
                    string strFileName = string.Format("{0}.{1}", "PAT_Variance_" + DateTime.Now.ToString("ddMMyyyy") + "", "csv");
                    DateTime fd1 = DateTime.ParseExact(Tk_mula.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);                    
                    builder.Append("KATEGORI KOD,NAMA KATEGORY,KOD AKAUN,NAMA AKAUN," + fd1.ToString("yyyy") + Environment.NewLine);
                    for (int k = 0; k <= (dt.Rows.Count - 1); k++)
                    {
                        double var = (double.Parse(dt.Rows[k]["Amt1"].ToString()) - double.Parse(dt.Rows[k]["Amt2"].ToString()));
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
                            fmt1 += " left join tree" + i + " s" + i + " on s" + i + ".ref_alrn_kod=m_sk1.ref_alrn_kod";
                            fmt2 += ", tree" + i + " AS(select b.ref_alrn_kod,b.ref_alrn_nama,b.KW_Debit_amt,b.KW_kredit_amt,a.Amount from (select a.DirectChildId, (isnull(sum(cast(b.KW_Debit_amt as money)), '0.00') - isnull(sum(cast(b.KW_kredit_amt as money)), '0.00')) as Amount from Recurse a "
                                    + " inner join kw_template_aliran k1 on k1.ref_alrn_nama=a.ref_alrn_kod  inner join KW_General_Ledger b on b.kod_akaun = k1.tmp_kod_akaun where b.GL_sts = 'L' and GL_post_dt>= DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/01'), 0) and GL_post_dt<= DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/" + lastDayOfMonth + "'), +1) group by DirectChildId) as a "
                                    + " inner join  (select m1.Id,m1.ref_alrn_nama,m1.ref_alrn_kod,m1.under_parent,sum(ISNULL(s1.KW_Debit_amt,'0.00')) as KW_Debit_amt "
                                    + " ,sum(ISNULL(s1.KW_kredit_amt,'0.00')) as KW_kredit_amt  from KW_Ref_aliran_item m1 inner join kw_template_aliran k2 on k2.ref_alrn_nama=m1.ref_alrn_kod and k2.tmp_Status='Y' "
                                    + " inner join KW_General_Ledger s1 on s1.kod_akaun=k2.tmp_kod_akaun and s1.GL_sts='L' and GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/01'), 0) and GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/" + lastDayOfMonth + "'), +1) group by "
                                    + "  m1.Id,m1.ref_alrn_nama,m1.ref_alrn_kod,m1.under_parent) as b on b.Id=a.DirectChildId  where (b.KW_Debit_amt !='0.00' or b.KW_kredit_amt !='0.00' or a.Amount !='0.00')) ";
                            fmt3 += "cast(ISNULL(s" + i + ".Amount,'0.00') as money) " + ss3 + "" + ss1;
                            fmt4 += "ISNULL(s" + i + ".Amount,'0.00') != '0.00'" + ss21 + "";
                        }
                        else
                        {
                            fmt1 += " left join tree" + i + " s" + i + " on s" + i + ".ref_alrn_kod=m_sk1.ref_alrn_kod";
                            fmt2 += ", tree" + i + " AS(select b.ref_alrn_kod,b.ref_alrn_nama,b.KW_Debit_amt,b.KW_kredit_amt,a.Amount from (select a.DirectChildId, (isnull(sum(cast(b.KW_Debit_amt as money)), '0.00') - isnull(sum(cast(b.KW_kredit_amt as money)), '0.00')) as Amount from Recurse a "
                                    + " inner join kw_template_aliran k1 on k1.ref_alrn_nama=a.ref_alrn_kod  inner join KW_General_Ledger b on b.kod_akaun = k1.tmp_kod_akaun where b.GL_sts = 'L' and GL_post_dt>= DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/01'), 0) and GL_post_dt<= DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/" + lastDayOfMonth + "'), +1) group by DirectChildId) as a "
                                    + " inner join  (select m1.Id,m1.ref_alrn_nama,m1.ref_alrn_kod,m1.under_parent,sum(ISNULL(s1.KW_Debit_amt,'0.00')) as KW_Debit_amt "
                                    + " ,sum(ISNULL(s1.KW_kredit_amt,'0.00')) as KW_kredit_amt  from KW_Ref_aliran_item m1 inner join kw_template_aliran k2 on k2.ref_alrn_nama=m1.ref_alrn_kod and k2.tmp_Status='Y' "
                                    + " inner join KW_General_Ledger s1 on s1.kod_akaun=k2.tmp_kod_akaun and s1.GL_sts='L' and GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/01'), 0) and GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + sdate.Year + "/" + ss2 + "/" + lastDayOfMonth + "'), +1) group by "
                                    + "  m1.Id,m1.ref_alrn_nama,m1.ref_alrn_kod,m1.under_parent) as b on b.Id=a.DirectChildId  where (b.KW_Debit_amt !='0.00' or b.KW_kredit_amt !='0.00' or a.Amount !='0.00')) ";
                            fmt3 += "cast(ISNULL(s" + i + ".Amount,'0.00') as money) " + ss3 + "" + ss1;
                            fmt4 += "ISNULL(s" + i + ".Amount,'0.00') != '0.00'" + ss21 + "";
                        }


                        //drpdate is dropdownlist
                        sdate = sdate.AddMonths(1);
                        i++;
                    }


                 

                    qry1 = "with Recurse as ( select a.Id as DirectChildId, a.Id, a.ref_alrn_kod from KW_Ref_aliran_item a where ref_alrn_header='N' union all select b.DirectChildId, a.Id, a.ref_alrn_kod from KW_Ref_aliran_item a"
                                       + "  join Recurse b on b.Id = a.under_parent where a.ref_alrn_header='Y') , "
                                       + " tree_k AS(select b.ref_alrn_nama,b.ref_alrn_kod from (select a.DirectChildId from Recurse a inner join kw_template_aliran k1 on k1.ref_alrn_nama=a.ref_alrn_kod inner join KW_General_Ledger b on b.GL_sts='L' and k1.tmp_kod_akaun = b.kod_akaun group by DirectChildId) as a  "
                                       + " inner join  (select m1.Id,m1.ref_alrn_nama,m1.ref_alrn_kod,m1.under_parent from KW_Ref_aliran_item m1 left join kw_template_aliran k2 on k2.ref_alrn_nama=m1.ref_alrn_kod and k2.tmp_Status='Y' where m1.ref_alrn_header='N') as b on b.Id = a.DirectChildId) "
                                       + "" + fmt2 + " "
                                       + " select distinct sk2.ref_alrn_kod as kat_akaun,sk2.ref_alrn_nama as deskripsi,m_sk1.ref_alrn_nama as nama_akaun,m_sk1.ref_alrn_kod as kod_akaun," + fmt3 + " from  tree_k m_sk1 " + fmt1 + " left join KW_Ref_aliran_item sk1 on sk1.ref_alrn_kod=m_sk1.ref_alrn_kod left join KW_Ref_aliran_item sk2 on sk2.Id=sk1.under_parent where (" + fmt4 + ") and ISNULL(sk2.ref_alrn_kod,'') != '' order by m_sk1.ref_alrn_kod ";


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
                        string strFileName = string.Format("{0}.{1}", "PAT_Monthly_" + DateTime.Now.ToString("ddMMyyyy") + "", "csv");
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


    protected void btn_reset(object sender, EventArgs e)
    {
        Response.Redirect("../kewengan/kw_lep_aliantunai.aspx");
    }


}