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

public partial class kw_tutup_akaun : System.Web.UI.Page
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
                    Tk_mula.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    bind_data();
                }
                else
                {
                    Button8.Visible = false;
                }
                             
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
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());            
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());          
            Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());            
           
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }

    
   

    void bind_details()
    {
       
        DateTime fd, td;
        if (Tk_mula.Text != "")
        {

            DataTable get_fin_year = new DataTable();
            get_fin_year = DBCon.Ora_Execute_table("select FORMAT(fin_start_dt,'yyyy-MM-dd') st_dt,FORMAT(fin_end_dt,'yyyy-MM-dd') ed_dt from KW_financial_Year where fin_year='" + service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"])) + "' and Status='1'");

            qry1 = "with Recurse as ( select a.Id as DirectChildId, a.Id, a.kod_akaun from KW_Ref_Carta_Akaun a  where a.Status='A' and kkk_rep='1'    "
+ " union all select b.DirectChildId, a.Id, a.kod_akaun from KW_Ref_Carta_Akaun a  join Recurse b on b.Id = a.under_parent where a.Status='A' and a.kkk_rep='1') ,   "
+ " tree1 AS(select b.kat_akaun,b.nama_akaun,b.kod_akaun,b.kw_acc_header from (select a.DirectChildId from Recurse a left join KW_General_Ledger b on b.GL_sts='L' and a.kod_akaun = b.kod_akaun  "
+ " group by DirectChildId) as a    inner join  (select m1.Id,m1.jenis_akaun_type,m1.kat_akaun,m1.nama_akaun,m1.kod_akaun,m1.jenis_akaun,m1.under_parent,m1.kw_acc_header from KW_Ref_Carta_Akaun m1 where  "
+ " m1.Status='A' and m1.kkk_rep='1') as b on b.Id=a.DirectChildId)   "
+ " , tree2 AS(select b.kw_acc_header,b.jenis_akaun_type,b.kat_akaun,b.kod_akaun,b.nama_akaun,b.KW_Debit_amt,b.KW_kredit_amt,a.Amount from  "
+ " (select a.DirectChildId, (cast(replace(((((sum(ISNULL(cast(s2.opn_kredit_amt as money),'0.00')) - sum(ISNULL(cast(s2.opn_debit_amt as money),'0.00'))) + (isnull(sum(cast(s2_1.KW_kredit_amt as money)),'0.00') -  "
+ " isnull(sum(cast(s2_1.KW_Debit_amt as money)),'0.00'))) + isnull(sum(cast(b.KW_kredit_amt as money)),'0.00')) - isnull(sum(cast(b.KW_Debit_amt as money)),'0.00')),'-','') as money) "
+ " + sum(ISNULL(mm1.tmp_jum_amt,'0.00')))  as Amount from Recurse a  "
+ " left join KW_Ref_Pembahagian mm1 on mm1.tmp_kod_akaun=a.kod_akaun and tmp_tahun_kewangan='"+ service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"])) + "' "
+ " left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = a.kod_akaun and '" + service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"])) + "' between YEAR(s2.start_dt) and YEAR(s2.end_dt)   "
+ " left join KW_General_Ledger s2_1 on  s2_1.kod_akaun = a.kod_akaun and YEAR(s2_1.GL_post_dt)='" + service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"])) + "' and  s2_1.GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + get_fin_year.Rows[0]["st_dt"].ToString() + "'), 0)   "
+ " left join KW_General_Ledger b on b.kod_akaun = a.kod_akaun and b.GL_sts='L' and b.GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + get_fin_year.Rows[0]["st_dt"].ToString() + "'), 0) and  "
+ " b.GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + get_fin_year.Rows[0]["ed_dt"].ToString() + "'), +1) group by DirectChildId) as a    "
+ " inner join  (select m1.Id,m1.jenis_akaun_type,m1.kat_akaun,m1.nama_akaun,m1.kod_akaun,m1.jenis_akaun,m1.under_parent,m1.kw_acc_header, "
+ " (sum((ISNULL(cast(s2.opn_debit_amt as money),'0.00'))) + sum(ISNULL(s1.KW_Debit_amt,'0.00'))) as KW_Debit_amt , "
+ " (sum((ISNULL(cast(s2.opn_kredit_amt as money),'0.00'))) + sum(ISNULL(s1.KW_kredit_amt,'0.00'))) as KW_kredit_amt  from KW_Ref_Carta_Akaun m1  	   "
+ " left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = m1.kod_akaun and '" + service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"])) + "' between YEAR(s2.start_dt) and YEAR(s2.end_dt)   "
+ " left join KW_General_Ledger s1 on s1.kod_akaun=m1.kod_akaun and s1.GL_sts='L' and  "
+ " GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + get_fin_year.Rows[0]["st_dt"].ToString() + "'), 0) and GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + get_fin_year.Rows[0]["ed_dt"].ToString() + "'), +1) where m1.Status='A' and m1.kkk_rep='1'  "
+ " group by  m1.Id,m1.jenis_akaun_type,m1.kat_akaun,m1.nama_akaun,m1.kod_akaun,m1.jenis_akaun,m1.under_parent,m1.kw_acc_header) as b on b.Id=a.DirectChildId   "
+ " )     "
+ " select a1.kat_akaun,a1.kat_type,a1.deskripsi,a1.bal_type,a1.nama_akaun,a1.kod_akaun,a1.jenis_akaun_type,ISNULL(a1.kw_acc_header,'0')kw_acc_header,sum(a1.KW_Debit_amt) KW_Debit_amt,sum(a1.KW_kredit_amt) KW_kredit_amt "
+ " ,SUM(cast(a1.Amt1 as money)) Amt1,sum(ISNULL(a1.amt2,'0.00')) Amt2 from( select * from  "
+ " (select s1.kw_acc_header,s2.jenis_akaun_type,s1.kat_akaun,s5.kat_type,s4.deskripsi,s4.bal_type,s1.nama_akaun, "
+ " s1.kod_akaun,ISNULL(s2.KW_Debit_amt,'0.00') KW_Debit_amt,ISNULL(s2.KW_kredit_amt,'0.00') KW_kredit_amt, "
+ " replace(ISNULL(s2.Amount,'0.00'),'-','') Amt1,cast((tmp_jum_amt+ replace((ISNULL(s2.KW_kredit_amt,'0.00') - ISNULL(s2.KW_Debit_amt,'0.00')),'-','')) as money) as amt2 from  tree1 s1   "
+ " left join KW_Ref_Pembahagian mm1 on mm1.tmp_kod_akaun=s1.kod_akaun and tmp_tahun_kewangan='" + service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"])) + "' "
+ " left join tree2 s2 on s2.kod_akaun=s1.kod_akaun left join KW_Kategori_akaun s4 on s4.kat_cd=s1.kat_akaun  "
+ " and kat_type='01' inner join kw_ref_report_1 s5 on s5.kat_cd=s4.kat_rpt_kk and kat_rpt_cd='01') as a   "
+ " union all   "
+ " select a1.* from(select cyp.kw_acc_header,cyp.jenis_akaun_type,kk1.kat_cd kat_akaun, 'D' kat_type, kk1.deskripsi, Kk1.bal_type, cyp.nama_akaun,  "
+ " cyp.kod_akaun, cast(ISNULL('0.00', '0.00') as money) KW_Debit_amt, sum(cast(ISNULL(s2.fin_amt_re, '0.00') as money)) KW_kredit_amt "
+ " ,  isnull(sum(cast(s2.fin_amt_re as money)), '0.00') as amt1,isnull(sum(cast(s2.fin_amt_re as money)), '0.00') as amt2  from KW_Ref_Carta_Akaun cyp  "
+ " left join KW_financial_Year s2 on s2.fin_year='" + service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"])) + "' left join KW_Kategori_akaun kk1 on kk1.kat_cd=cyp.kat_akaun  where cyp.ca_re = '1' and cyp.Status = 'A'  "
+ " group by  cyp.kw_acc_header,cyp.jenis_akaun_type,kk1.kat_cd, kk1.deskripsi, Kk1.bal_type, cyp.nama_akaun, cyp.kod_akaun) as a1   "
+ " where a1.kod_akaun = a1.kod_akaun  ) as a1 group by a1.kat_akaun, a1.kat_type,a1.deskripsi,a1.bal_type,a1.nama_akaun,a1.kod_akaun,a1.jenis_akaun_type,a1.kw_acc_header order by a1.kod_akaun   ";
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
                            GridView1.FooterRow.Cells[1].Text = "<strong>JUMLAH KESELURUHAN (RM)</strong>";
                            GridView1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                            GridView1.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                            GridView1.FooterRow.Cells[2].Text = "0.00";
                        }
                        else
                        {
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
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

      

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
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
    protected void cls_financial_year(object sender, EventArgs e)
    {
        if (Tk_mula.Text != "")
        {
            string tk_m = string.Empty, tk_a = string.Empty;
            string rcount1 = string.Empty, rcount2 = string.Empty, rcount3 = string.Empty, rcount4 = string.Empty;
            int count1 = 0;
            foreach (GridViewRow gvrow in GridView1.Rows)
            {
                string val3 = ((System.Web.UI.WebControls.Label)gvrow.FindControl("Label2_nw")).Text.ToString();
                if (val3 == "0")
                {
                    count1++;
                    rcount1 = count1.ToString();
                }
            }

            if (rcount1 != "0")
            {
                foreach (GridViewRow row in GridView1.Rows)
                {                    
                    string val1 = ((System.Web.UI.WebControls.Label)row.FindControl("bal_type")).Text.ToString();
                    string val2 = ((System.Web.UI.WebControls.Label)row.FindControl("Label1_nw")).Text.ToString();
                    string val3 = ((System.Web.UI.WebControls.Label)row.FindControl("Label2_nw")).Text.ToString();
                    string val4 = ((System.Web.UI.WebControls.Label)row.FindControl("kat_cd")).Text.ToString();
                    string val5 = ((System.Web.UI.WebControls.Label)row.FindControl("Label3")).Text.ToString();
                    string val6 = ((System.Web.UI.WebControls.Label)row.FindControl("Label3_nw")).Text.ToString();
                    string val7 = ((System.Web.UI.WebControls.Label)row.FindControl("Label4_nw")).Text.ToString();
                    decimal Kamt = 0;
                    decimal Damt = 0;
                    string open_amt = string.Empty,st_dt=string.Empty, ed_dt = string.Empty;
                    if (val3 == "0")
                    {
                        //create opening Balance
                        int cyear = (Int32.Parse(Tk_mula.Text) + 1);
                        DataTable chk_fin_year = new DataTable();
                        chk_fin_year = DBCon.Ora_Execute_table("SELECT FORMAT(ISNULL(fin_start_dt,''),'yyyy-MM-dd')  as st_dt,FORMAT(ISNULL(fin_end_dt,''),'yyyy-MM-dd')  as ed_dt FROM  KW_financial_Year WHERE fin_year='" + cyear + "'");
                        if(chk_fin_year.Rows.Count != 0)
                        {
                            st_dt = chk_fin_year.Rows[0]["st_dt"].ToString();
                            ed_dt = chk_fin_year.Rows[0]["ed_dt"].ToString();
                        }

                        if (val6 == "D")
                        {
                            Damt = decimal.Parse(val5);
                            Kamt = 0;
                            open_amt = "-" + Damt.ToString();
                        }
                        else
                        {
                            Kamt = decimal.Parse(val5);
                            Damt = 0;
                            open_amt = Kamt.ToString();
                        }
                       

                            string Inssql1 = "insert into KW_Opening_Balance(kod_akaun,opeing_date,opening_year,Status,opening_amt,ending_amt,kat_akaun,set_sts,start_dt,end_dt,opn_kredit_amt,opn_debit_amt,crt_id,cr_dt) values ('" + val1 + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + cyear + "','A','"+ open_amt + "','" + open_amt + "','" + val7 + "','0','"+ st_dt + "','"+ ed_dt +"','"+Kamt+ "','" + Damt + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            Status = DBCon.Ora_Execute_CommamdText(Inssql1);
                        
                    }
                }
            }
            if (Status == "SUCCESS")
            {
                string Upd_fin_year1 = "Update KW_financial_Year set fin_close_id='" + Session["New"].ToString() + "',fin_close_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where  fin_year='" + Tk_mula.Text + "'";
                Status = DBCon.Ora_Execute_CommamdText(Upd_fin_year1);

                string Upd_fin_year2 = "Update KW_financial_Year set Status='1',fin_opening_balance='1' where  Status='2'";
                Status = DBCon.Ora_Execute_CommamdText(Upd_fin_year2);

                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);                
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
        bind_data();
    }
    protected void btn_reset(object sender, EventArgs e)
    {
        Response.Redirect("../kewengan/kw_lep_kunci_kira.aspx");
    }


}