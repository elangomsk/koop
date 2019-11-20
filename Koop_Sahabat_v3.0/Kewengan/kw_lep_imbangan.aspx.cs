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
using System.Data.OleDb;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;


public partial class kw_lep_imbangan : System.Web.UI.Page
{
    private GridViewHelper helper;
    private List<int> mQuantities = new List<int>();

    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    DBConnection Dbcon = new DBConnection();
    string query1 = string.Empty;
    string query2 = string.Empty;
    string query3 = string.Empty;
    string level;
    string Status = string.Empty;
    string sel = string.Empty;
    string userid;
    string ref_id;
    string confirmValue, am;
    string qry1 = string.Empty, qry2 = string.Empty, sqry = string.Empty;
    string tn1 = string.Empty, tc1 = string.Empty, tc2 = string.Empty, tc3 = string.Empty, tc4 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(document).ready(function () { $(" + dd_akaun.ClientID + ").SumoSelect({ selectAll: true }); $('.select2').select2();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        app_language();

        

        BindData();
        if (!IsPostBack)
        {
            get_profile_view();
            if (Session["New"] != null)
            {
                if (Session["validate_success"].ToString() == "SUCCESS")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('" + Session["alrt_msg"].ToString() + "',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    Session["validate_success"] = "";
                    Session["alrt_msg"] = "";
                    Session["pro_type"] = "";
                }
                //Button2.Attributes.Add("style", "Pointer-events:none; opacity: 0.5;");
                //Button3.Attributes.Add("style", "Pointer-events:none; opacity: 0.5;");
                kat_bajet.SelectedValue = "01";
                userid = Session["New"].ToString();
                //Button1.Visible = false;
                bind_kat_akaun();
                bind_kod_akaun();
                bind_tahun();
              
    
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

    void get_profile_view()
    {
        string get_sdt = string.Empty, get_edt = string.Empty;
        DataTable sel_gst2 = new DataTable();
        sel_gst2 = DBCon.Ora_Execute_table("select top(1) Format(tarikh_mula,'dd/MM/yyyy') as st_dt,Format(tarikh_akhir,'dd/MM/yyyy') as end_dt from kw_profile_syarikat where cur_sts='1' order by tarikh_akhir desc");
        if (sel_gst2.Rows.Count != 0)
        {
            get_sdt = sel_gst2.Rows[0]["st_dt"].ToString();
            get_edt = sel_gst2.Rows[0]["end_dt"].ToString();
            DateTime fd = DateTime.ParseExact(get_sdt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime fd1 = DateTime.ParseExact(get_edt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            TextBox5.Text = fd.ToString("dd/MM/yyyy");
            TextBox6.Text = fd1.ToString("dd/MM/yyyy");
            Tk_mula.Text = fd.ToString("dd/MM/yyyy");
            Tk_akhir.Text = fd1.ToString("dd/MM/yyyy");

        }
    }
    void bind_kod_akaun()
    {

        DataSet Ds = new DataSet();
        try
        {
            string get_qry = string.Empty;

            if (dd_kodind.SelectedValue == "1")
            {
                get_qry = "select kod_akaun,upper((kod_akaun + ' | ' + case when LEN(nama_akaun) >= '50' then SUBSTRING ( nama_akaun ,1 , 50)+ ' ...'  else  nama_akaun end)) as name from KW_Ref_Carta_Akaun where jenis_akaun_type != '1' and Status='A' order by kod_akaun asc";
            }
            else if (dd_kodind.SelectedValue == "2")
            {
                get_qry = "select kod_akaun,upper((kod_akaun + ' | ' + case when LEN(nama_akaun) >= '50' then SUBSTRING ( nama_akaun ,1 , 50)+ ' ...'  else  nama_akaun end)) as name from KW_Ref_Carta_Akaun s1 inner join KW_Ref_Pelanggan on Ref_kod_akaun = kod_akaun where jenis_akaun_type != '1' and s1.Status='A' order by kod_akaun asc";
            }
            else if (dd_kodind.SelectedValue == "3")
            {
                get_qry = "select kod_akaun,upper((kod_akaun + ' | ' + case when LEN(nama_akaun) >= '50' then SUBSTRING ( nama_akaun ,1 , 50)+ ' ...'  else  nama_akaun end)) as name from KW_Ref_Carta_Akaun s1 inner join KW_Ref_Pembekal on Ref_kod_akaun = kod_akaun where jenis_akaun_type != '1' and s1.Status='A' order by kod_akaun asc";
            }
            else if (dd_kodind.SelectedValue == "4")
            {
                get_qry = "select kod_akaun,upper((kod_akaun + ' | ' + case when LEN(nama_akaun) >= '50' then SUBSTRING ( nama_akaun ,1 , 50)+ ' ...'  else  nama_akaun end)) as name from KW_Ref_Carta_Akaun where jenis_akaun_type != '1' and jenis_akaun='12.01' order by kod_akaun asc";
            }
            else
            {

                get_qry = "select kod_akaun,upper((kod_akaun + ' | ' + case when LEN(nama_akaun) >= '50' then SUBSTRING ( nama_akaun ,1 , 50)+ ' ...'  else  nama_akaun end)) as name from KW_Ref_Carta_Akaun where jenis_akaun_type != '1' and Status='A' order by kod_akaun asc";

            }

            string com = get_qry;
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_akaun.DataSource = dt;
            dd_akaun.DataTextField = "name";
            dd_akaun.DataValueField = "kod_akaun";
            dd_akaun.DataBind();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    void bind_tahun()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "SELECT fin_year as post_dt FROM kw_financial_year";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList1.DataSource = dt;
            DropDownList1.DataTextField = "post_dt";
            DropDownList1.DataValueField = "post_dt";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void bind_kat_akaun()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select kat_cd,UPPER(kat_akuan) kat_akuan from KW_Kategori_akaun where kat_type='02' order by kat_cd asc";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            kat_akaun.DataSource = dt;
            kat_akaun.DataTextField = "kat_akuan";
            kat_akaun.DataValueField = "kat_cd";
            kat_akaun.DataBind();
            kat_akaun.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void app_language()

    {
        if (Session["New"] != null)
        {
            DataTable ste_set = new DataTable();
            ste_set = DBCon.Ora_Execute_table("select * from site_settings where ID IN ('1')");

            DataTable gt_lng = new DataTable();
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('731','705','36','1646','1647','133')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            

        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    protected void BindData()
    {
        //PopulateTreeview();
        BindGrid();
    }

    void bind_details()
    {
        if (DropDownList1.SelectedValue != "" && kat_bajet.SelectedValue != "")
        {
            if (kat_bajet.SelectedValue != "03")
            {
                sqry = "with Recurse as (select a.Id as DirectChildId, a.Id, a.kod_akaun from KW_Ref_Carta_Akaun a  where a.Status = 'A' "
                 + " union all select b.DirectChildId, a.Id, a.kod_akaun from KW_Ref_Carta_Akaun a  join Recurse b on b.Id = a.under_parent where a.Status = 'A' ) ,   "
                 + " tree1 AS(select b.kat_akaun,b.jenis_akaun_type, b.nama_akaun, b.kod_akaun,b.kw_acc_header from(select a.DirectChildId from Recurse a left join KW_General_Ledger b on a.kod_akaun = b.kod_akaun group by DirectChildId) as a    "
                 + " inner join(select m1.Id, m1.jenis_akaun_type, m1.kat_akaun, m1.nama_akaun, m1.kod_akaun, m1.jenis_akaun, m1.under_parent,m1.kw_acc_header from KW_Ref_Carta_Akaun m1 where m1.Status = 'A' ) as b on  "
                 + "  b.Id = a.DirectChildId)  , tree2 AS(select b.kat_akaun, b.kod_akaun, b.nama_akaun, b.KW_Debit_amt, b.KW_kredit_amt, a.Amount, a.Amount1 from(select a.DirectChildId,  "
                 + " (ISNULL(cast(replace(s2.opn_debit_amt,'-','') as money),'0.00') + (isnull(sum(cast(b.KW_Debit_amt as money)), '0.00'))) as Amount, (ISNULL(cast(replace(s2.opn_kredit_amt,'-','') as money),'0.00') + (isnull(sum(cast(b.KW_kredit_amt as money)), '0.00'))) as Amount1 from Recurse a left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = a.kod_akaun and '" + DropDownList1.SelectedValue + "' between YEAR(s2.start_dt) and YEAR(s2.end_dt) left join KW_General_Ledger b on  "
                 + " b.kod_akaun = a.kod_akaun and GL_post_dt >= DATEADD(day, DATEDIFF(day, 0, '" + DropDownList1.SelectedValue + "/01/01'), 0) and GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + DropDownList1.SelectedValue + "/12/31'), +1) and GL_sts='L' group by DirectChildId,s2.opn_kredit_amt,s2.opn_debit_amt) as "
                 + " a  inner join(select m1.Id, m1.jenis_akaun_type, m1.kat_akaun, m1.nama_akaun, m1.kod_akaun, m1.jenis_akaun, m1.under_parent,m1.kw_acc_header, (ISNULL(s2.opn_debit_amt,'0.00') + sum(ISNULL(s1.KW_Debit_amt, '0.00'))) as KW_Debit_amt  , "
                 + " (ISNULL(s2.opn_kredit_amt,'0.00') +  sum(ISNULL(s1.KW_kredit_amt, '0.00'))) as KW_kredit_amt  from KW_Ref_Carta_Akaun m1 left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = m1.kod_akaun and '" + DropDownList1.SelectedValue + "' between YEAR(s2.start_dt) and YEAR(s2.end_dt) "
                 + " left join KW_General_Ledger s1 on s1.kod_akaun = m1.kod_akaun and GL_post_dt >=  "
                 + " DATEADD(day, DATEDIFF(day, 0, '" + DropDownList1.SelectedValue + "/01/01'), 0) and GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + DropDownList1.SelectedValue + "/12/31'), +1) and GL_sts='L' where m1.Status = 'A' group by  m1.Id, m1.jenis_akaun_type, "
                 + "    m1.kat_akaun, m1.nama_akaun, m1.kod_akaun, m1.jenis_akaun, m1.under_parent,m1.kw_acc_header,s2.opn_kredit_amt,s2.opn_debit_amt) as b on b.Id = a.DirectChildId)     "
                 + " select s1.kat_akaun,kk.kat_akuan,s1.nama_akaun,s1.kod_akaun,s1.jenis_akaun_type,ISNULL(s1.kw_acc_header,'0') kw_acc_header,sum(ISNULL(s2.KW_Debit_amt, '0.00'))  KW_Debit_amt,sum(ISNULL(s2.KW_kredit_amt, '0.00')) KW_kredit_amt,sum(ISNULL(s2.Amount, '0.00')) Amt1, "
                 + " sum(ISNULL(s2.Amount1, '0.00')) Amt2 from  tree1 s1 left join tree2 s2 on s2.kod_akaun = s1.kod_akaun "
                 + " left join KW_Kategori_akaun kk on kk.kat_cd = s1.kat_akaun and kat_type = '01' where(ISNULL(s2.Amount, '0.00') != '0.00' or ISNULL(s2.Amount1, '0.00') != '0.00') "
                 + " group by s1.kat_akaun,kk.kat_akuan,s1.nama_akaun,s1.kod_akaun,s1.jenis_akaun_type,ISNULL(s1.kw_acc_header,'0') "
                 + " order by s1.kod_akaun  ";
            }
            else
            {
                sqry = "with Recurse as (select a.Id as DirectChildId, a.Id, a.kod_akaun from KW_Ref_Carta_Akaun a  where a.Status = 'A' "
                + " union all select b.DirectChildId, a.Id, a.kod_akaun from KW_Ref_Carta_Akaun a  join Recurse b on b.Id = a.under_parent where a.Status = 'A' ) ,   "
                + " tree1 AS(select b.kat_akaun,b.jenis_akaun_type, b.nama_akaun, b.kod_akaun,b.kw_acc_header,b.ca_cyp from(select a.DirectChildId from Recurse a left join KW_General_Ledger b on a.kod_akaun = b.kod_akaun group by DirectChildId) as a   "
                + " inner join(select m1.Id,m1.ca_cyp, m1.jenis_akaun_type, m1.kat_akaun, m1.nama_akaun, m1.kod_akaun, m1.jenis_akaun, m1.under_parent,m1.kw_acc_header from KW_Ref_Carta_Akaun m1 where m1.Status = 'A' ) as b on  "
                + "  b.Id = a.DirectChildId)  , tree2 AS(select b.kat_akaun, b.kod_akaun, b.nama_akaun, (ISNULL(b.deb_amt,'0.00') + b.KW_Debit_amt) KW_Debit_amt,(ISNULL(b.kre_amt,'0.00') + b.KW_kredit_amt) KW_kredit_amt, a.Amount, a.Amount1 from(select a.DirectChildId,  "
                + "   (SUM(ISNULL(b1.deb_amt,'0.00')) + (ISNULL(cast(replace(s2.opn_debit_amt,'-','') as money),'0.00') + (isnull(sum(cast(b.KW_Debit_amt as money)), '0.00')))) as Amount, (SUM(ISNULL(b1.kre_amt,'0.00')) + (ISNULL(cast(replace(s2.opn_kredit_amt,'-','') as money),'0.00') + (isnull(sum(cast(b.KW_kredit_amt as money)), '0.00')))) as Amount1 from Recurse a left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = a.kod_akaun and '" + DropDownList1.SelectedValue + "' between YEAR(s2.start_dt) and YEAR(s2.end_dt) left join KW_General_Ledger b on  "
                + " b.kod_akaun = a.kod_akaun and GL_post_dt >= DATEADD(day, DATEDIFF(day, 0, '" + DropDownList1.SelectedValue + "/01/01'), 0) and GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + DropDownList1.SelectedValue + "/12/31'), +1) and GL_sts='L' "
                + " Left join( "
                + " select cyp.kod_akaun,cast('0.00' as money) as deb_amt  ,  ((ISNULL(sum(s2.opn_kredit_amt),'0.00') +    sum(ISNULL(s1.KW_kredit_amt, '0.00'))) -(ISNULL(sum(s2.opn_debit_amt),'0.00') + sum(ISNULL(s1.KW_Debit_amt, '0.00')))) as kre_amt  from KW_Ref_Carta_Akaun m1 "
                + " left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = m1.kod_akaun and '" + DropDownList1.SelectedValue + "' between YEAR(s2.start_dt) and YEAR(s2.end_dt)  "
                + " left join KW_General_Ledger s1 on s1.kod_akaun = m1.kod_akaun and GL_post_dt >=   DATEADD(day, DATEDIFF(day, 0, '" + DropDownList1.SelectedValue + "/01/01'), 0) and GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + DropDownList1.SelectedValue + "/12/31'), +1) and GL_sts='L'"
                + " left join KW_Kategori_akaun kk on kk.kat_cd = m1.kat_akaun and kat_type = '01' inner join kw_ref_report_1 s5 on s5.kat_cd=kk.kat_rpt_kk and kat_rpt_cd='02' "
                + " left join KW_Ref_Carta_Akaun cyp on cyp.ca_cyp='1'	 where m1.jenis_akaun_type != '1' and m1.Status = 'A' group by  cyp.kod_akaun "
                + " ) as b1 on b1.kod_akaun=a.kod_akaun "
                + " group by DirectChildId,s2.opn_kredit_amt,s2.opn_debit_amt) as "
                + " a  inner join(select m1.Id,m1.ca_cyp, m1.jenis_akaun_type, m1.kat_akaun, m1.nama_akaun, m1.kod_akaun, m1.jenis_akaun, m1.under_parent,m1.kw_acc_header, (ISNULL(s2.opn_debit_amt,'0.00') + sum(ISNULL(s1.KW_Debit_amt, '0.00'))) as KW_Debit_amt  , "
                + " (ISNULL(s2.opn_kredit_amt,'0.00') +  sum(ISNULL(s1.KW_kredit_amt, '0.00'))) as KW_kredit_amt,sum(b2.deb_amt) deb_amt,sum(b2.kre_amt) kre_amt  from KW_Ref_Carta_Akaun m1 left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = m1.kod_akaun and '" + DropDownList1.SelectedValue + "' between YEAR(s2.start_dt) and YEAR(s2.end_dt) "
                + " left join KW_General_Ledger s1 on s1.kod_akaun = m1.kod_akaun and GL_post_dt >=  "
                + " DATEADD(day, DATEDIFF(day, 0, '" + DropDownList1.SelectedValue + "/01/01'), 0) and GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + DropDownList1.SelectedValue + "/12/31'), +1) and GL_sts='L' "
                 + " Left join( "
                + " select cyp.kod_akaun,cast('0.00' as money) as deb_amt  ,  ((ISNULL(sum(s2.opn_kredit_amt),'0.00') +  sum(ISNULL(s1.KW_kredit_amt, '0.00'))) - (ISNULL(sum(s2.opn_debit_amt),'0.00') + sum(ISNULL(s1.KW_Debit_amt, '0.00')))) as kre_amt  from KW_Ref_Carta_Akaun m1 "
                + " left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = m1.kod_akaun and '" + DropDownList1.SelectedValue + "' between YEAR(s2.start_dt) and YEAR(s2.end_dt)  "
                + " left join KW_General_Ledger s1 on s1.kod_akaun = m1.kod_akaun and GL_post_dt >=   DATEADD(day, DATEDIFF(day, 0, '" + DropDownList1.SelectedValue + "/01/01'), 0) and GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + DropDownList1.SelectedValue + "/12/31'), +1) and GL_sts='L'"
                + " left join KW_Kategori_akaun kk on kk.kat_cd = m1.kat_akaun and kat_type = '01' inner join kw_ref_report_1 s5 on s5.kat_cd=kk.kat_rpt_kk and kat_rpt_cd='02' "
                + " left join KW_Ref_Carta_Akaun cyp on cyp.ca_cyp='1'	 where m1.jenis_akaun_type != '1' and m1.Status = 'A' group by  cyp.kod_akaun "
                + " ) as b2 on b2.kod_akaun=m1.kod_akaun "
                + " where m1.Status = 'A' group by  m1.Id,m1.ca_cyp, m1.jenis_akaun_type, "
                + "    m1.kat_akaun, m1.nama_akaun, m1.kod_akaun, m1.jenis_akaun, m1.under_parent,m1.kw_acc_header,s2.opn_kredit_amt,s2.opn_debit_amt) as b on b.Id = a.DirectChildId)     "
                + " select a.kat_akaun,a.kat_akuan,a.nama_akaun,a.kod_akaun,a.jenis_akaun_type,ISNULL(a.kw_acc_header,'0') kw_acc_header, "
                + "  (sum(ISNULL(a.KW_Debit_amt, '0.00')))  KW_Debit_amt,(sum(ISNULL(a.KW_kredit_amt, '0.00'))) KW_kredit_amt,(sum(ISNULL(a.Amt1, '0.00'))) Amt1,(sum(ISNULL(a.Amt2, '0.00'))) Amt2 from ( "
                + " select s1.kat_akaun,kk.kat_akuan,ISNULL(s1.ca_cyp,'0') as ca_cyp,s1.nama_akaun,s1.kod_akaun,s1.jenis_akaun_type,ISNULL(s1.kw_acc_header,'0') kw_acc_header,"
                + " ISNULL(s2.KW_Debit_amt, '0.00')  KW_Debit_amt,ISNULL(s2.KW_kredit_amt, '0.00') KW_kredit_amt,ISNULL(s2.Amount, '0.00') Amt1,ISNULL(s2.Amount1, '0.00') Amt2 from  tree1 s1   "
                + " left join tree2 s2 on s2.kod_akaun = s1.kod_akaun  left join KW_Kategori_akaun kk on kk.kat_cd = s1.kat_akaun and kat_type = '01' "
                + " inner join kw_ref_report_1 s5 on s5.kat_cd=kk.kat_rpt_kk and kat_rpt_cd='01' where(ISNULL(s2.Amount, '0.00') != '0.00' or ISNULL(s2.Amount1, '0.00') != '0.00' or ISNULL(s1.ca_cyp,'0') ='1') ) as a "
                + " group by a.kat_akaun,a.kat_akuan,a.nama_akaun,a.kod_akaun,a.jenis_akaun_type,ISNULL(a.kw_acc_header,'0') "
                + " order by a.kod_akaun ";

            }
        }
        else
        {
            sqry = "select A.Rpt_tb_kod_akaun as kod_akaun,A.Rpt_tb_debit_amt as Amt1,A.Rpt_tb_kredit_amt as Amt2,B.nama_akaun as nama_akaun,B.kat_akaun,A.Rpt_tb_note as nota,C.kat_akuan,B.kw_acc_header,B.jenis_akaun_type  from KW_rpt_trialbalance A inner join KW_Ref_Carta_Akaun B on A.Rpt_tb_kod_akaun = B.kod_akaun inner join KW_Kategori_akaun C on B.kat_akaun = c.kat_cd and c.kat_type='01'  where A.Rpt_tb_year= '' and A .Rpt_tb_type='' and A.Rpt_tb_kod_akaun=''  ";
        }
    }
    protected void clk_srch(object sender, EventArgs e)
    {
        string st_dt = string.Empty;

        bind_details();
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand(sqry))
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
                                Button5.Visible = false;
                                Button6.Visible = false;
                                Button8.Visible = false;
                                GridView1.FooterRow.Cells[1].Text = "<strong>JUMLAH KESELURUHAN (RM)</strong>";
                                GridView1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                                GridView1.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                                GridView1.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                                GridView1.FooterRow.Cells[2].Text = "0.00";
                                GridView1.FooterRow.Cells[3].Text = "0.00";
                            }
                            else
                            {
                                GridView1.DataSource = dt;
                                GridView1.DataBind();
                                Button5.Visible = true;
                                Button6.Visible = true;
                                Button8.Visible = true;
                                decimal debit = dt.AsEnumerable().Sum(row => row.Field<decimal>("KW_Debit_amt"));
                                decimal kredit = dt.AsEnumerable().Sum(row => row.Field<decimal>("KW_kredit_amt"));
                                GridView1.FooterRow.Cells[1].Text = "<strong>JUMLAH KESELURUHAN (RM)</strong>";
                                GridView1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                                GridView1.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                                GridView1.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                                GridView1.FooterRow.Cells[2].Text = debit.ToString("C").Replace("RM", "").Replace("$", "");
                                GridView1.FooterRow.Cells[3].Text = kredit.ToString("C").Replace("RM", "").Replace("$", "");
                            }

                        }
                    }
                }
            }
    }
    protected void BindGrid()
    {

        string st_dt = string.Empty;
        bind_details();
        string query = sqry;

      
        using (SqlConnection con = new SqlConnection(conString))
        {
            using (SqlCommand cmd = new SqlCommand(query))
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
                            Button5.Visible = false;
                            Button6.Visible = false;
                            Button8.Visible = false;
                            GridView1.FooterRow.Cells[1].Text = "<strong>JUMLAH KESELURUHAN (RM)</strong>";
                            GridView1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                            GridView1.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                            GridView1.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                            GridView1.FooterRow.Cells[2].Text = "0.00";
                            GridView1.FooterRow.Cells[3].Text = "0.00";
                        }

                        else
                        {
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            Button5.Visible = true;
                            Button6.Visible = true;
                            Button8.Visible = true;
                            decimal debit = dt.AsEnumerable().Sum(row => row.Field<decimal>("KW_Debit_amt"));
                            decimal kredit = dt.AsEnumerable().Sum(row => row.Field<decimal>("KW_kredit_amt"));
                            GridView1.FooterRow.Cells[1].Text = "<strong>JUMLAH KESELURUHAN (RM)</strong>";
                            GridView1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                            GridView1.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                            GridView1.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                            GridView1.FooterRow.Cells[2].Text = debit.ToString("C").Replace("RM", "").Replace("$", "");
                            GridView1.FooterRow.Cells[3].Text = kredit.ToString("C").Replace("RM", "").Replace("$", "");
                        }

                    }
                }
            }
        }

    }
    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_refdata.PageIndex = e.NewPageIndex;
        gv_refdata.DataBind();
        // BindData();
    }

    protected void BindGrid1()
    {
        //SqlCommand cmd2 = new SqlCommand("select ISNULL(ho.org_name,'') as org_name,ISNULL(dd.dis_dispose_type_cd,'') as dis_dispose_type_cd,rk.ast_kategori_desc,rja.ast_jeniaset_desc,aca.cas_asset_desc,a.sas_asset_id,a.sas_curr_price_amt, a.sas_asset_cat_cd,a.sas_asset_sub_cat_cd,a.sas_asset_type_cd,a.sas_asset_cd,a.sas_org_id, case a.sas_asset_cat_cd when '01' then (select FORMAT(com_reg_dt,'dd/MM/yyyy', 'en-us') as reg_dt from ast_component where com_asset_cat_cd=a.sas_asset_cat_cd and com_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and com_asset_type_cd=a.sas_asset_type_cd and com_asset_cd=a.sas_asset_cd) when '02' then (select FORMAT(car_reg_dt,'dd/MM/yyyy', 'en-us') as reg_dt from ast_car where car_asset_cat_cd=a.sas_asset_cat_cd and car_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and car_asset_type_cd=a.sas_asset_type_cd and car_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') when '03' then (select FORMAT(inv_reg_dt,'dd/MM/yyyy', 'en-us') as reg_dt from ast_inventory where inv_asset_cat_cd=a.sas_asset_cat_cd and inv_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and inv_asset_type_cd=a.sas_asset_type_cd and inv_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') end as a1, case a.sas_asset_cat_cd when '01' then (select DATEDIFF(day,com_reg_dt,GETDATE()) as u_dt from ast_component where com_asset_cat_cd=a.sas_asset_cat_cd and com_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and com_asset_type_cd=a.sas_asset_type_cd and com_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') when '02' then (select DATEDIFF(day,car_reg_dt,GETDATE()) as u_dt from ast_car where car_asset_cat_cd=a.sas_asset_cat_cd and car_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and car_asset_type_cd=a.sas_asset_type_cd and car_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') when '03' then (select  DATEDIFF(day,inv_reg_dt,GETDATE()) as u_dt from ast_inventory where inv_asset_cat_cd=a.sas_asset_cat_cd and inv_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and inv_asset_type_cd=a.sas_asset_type_cd and inv_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') end as a2, case a.sas_asset_cat_cd when '01' then (select com_price_amt from ast_component where com_asset_cat_cd=a.sas_asset_cat_cd and com_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and com_asset_type_cd=a.sas_asset_type_cd and com_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') when '02' then (select car_price_amt from ast_car where car_asset_cat_cd=a.sas_asset_cat_cd and car_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and car_asset_type_cd=a.sas_asset_type_cd and car_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') when '03' then (select inv_price_amt from ast_inventory where inv_asset_cat_cd=a.sas_asset_cat_cd and inv_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and inv_asset_type_cd=a.sas_asset_type_cd and inv_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') end as a3 from (select * from ast_staff_asset  where sas_cond_sts_cd = '03' and ISNULL(sas_dispose_cfm_ind,'' ) !='Y' and sas_staff_no='" + Session["New"].ToString() + "') as a left join Ref_ast_kategori as rk on rk.ast_kategori_code=a.sas_asset_cat_cd left join Ref_ast_jenis_aset as rja on rja.ast_jeniaset_Code=a.sas_asset_type_cd left join ast_cmn_asset as aca on aca.cas_asset_cd=a.sas_asset_cd and aca.cas_asset_cat_cd=a.sas_asset_cat_cd and aca.cas_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and aca.cas_asset_type_cd=a.sas_asset_type_cd left join hr_organization as ho on ho.org_gen_id=a.sas_org_id left join ast_dispose as dd on dd.dis_asset_id=a.sas_asset_id", con);
        SqlCommand cmd2 = new SqlCommand("select *,case when jenis_bajet_type='1' then kat_bajet else kod_bajet end as kod_akaun1 from KW_Ref_kod_bajet", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        gv_refdata.DataSource = ds2;
        gv_refdata.DataBind();

    }

    protected void Add_profile(object sender, EventArgs e)
    {
        Response.Redirect("../kewengan/kw_carta_akaun.aspx");
    }





    void show_ddvalue()
    {
        
    }


    protected void clk_update(object sender, EventArgs e)
    {
        LinkButton btnButton = sender as LinkButton;
        GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
        System.Web.UI.WebControls.Label og_genid = (System.Web.UI.WebControls.Label)gvRow.FindControl("bal_type");
        string ogid = og_genid.Text;

        DataTable get_trial = new DataTable();
        get_trial = DBCon.Ora_Execute_table("select * from KW_rpt_trialbalance where Rpt_tb_kod_akaun='" + og_genid.Text + "' and Rpt_tb_year ='"+ DropDownList1.SelectedValue+ "' and Rpt_tb_type='"+ kat_bajet.SelectedValue +"' and status='A'");

       
         if (get_trial.Rows.Count != 0)
        {
           
            Session["trail"] = get_trial.Rows[0]["Rpt_tb_kod_akaun"].ToString();
            if (get_trial.Rows.Count != 0)
            {
                Button4.Text = "Kemaskini";
            }
            else
            {
                Button4.Text = "Simpan";
            }
            txt_nota.Value = get_trial.Rows[0]["rpt_tb_note"].ToString();
            hdr_txt.Text = "Imbungan Duga";
            BindGrid();
            ModalPopupExtender1.Show();

            ver_id.Text = "1";
            get_id.Text = og_genid.Text;
            show_ddvalue();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Pertama Anda Klik Yang Simpan Itu.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            BindGrid();
        }
    }
   

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        System.Web.UI.WebControls.Label lbl1 = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label1_nw");
        System.Web.UI.WebControls.Label clmn1 = (System.Web.UI.WebControls.Label)e.Row.FindControl("bal_type");
        System.Web.UI.WebControls.Label clmn2 = (System.Web.UI.WebControls.Label)e.Row.FindControl("kat_cd");
        System.Web.UI.WebControls.Label clmn3 = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label3");
        System.Web.UI.WebControls.Label clmn4 = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label4");
        System.Web.UI.WebControls.LinkButton clmn5 = (System.Web.UI.WebControls.LinkButton)e.Row.FindControl("LinkButton1");
        System.Web.UI.WebControls.Label hdr = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label2_nw");        

        LinkButton Button3 = e.Row.FindControl("LinkButton1") as LinkButton;

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
                Button3.Attributes.Remove("style");
                //clmn1.Attributes.Add("style", "padding-left:60px;");
                //clmn2.Attributes.Add("style", "padding-left:60px;");
            }
            else
            {
                Button3.Attributes.Add("style", "display:none;");
            }
        }
    }

    protected void sub_but(object sender, EventArgs e)
    {
       

        if (DropDownList1.SelectedValue != "" && kat_bajet.SelectedValue != "")
        {
            DataTable trl_val = new DataTable();
            trl_val = DBCon.Ora_Execute_table("select * from KW_rpt_trialbalance where Rpt_tb_type='" + kat_bajet.SelectedValue + "' and Rpt_tb_year ='" + DropDownList1.SelectedValue + "'");
            if (trl_val.Rows.Count != 0)
            {
                string Inssql1 = "delete from KW_rpt_trialbalance where Rpt_tb_type='" + kat_bajet.SelectedValue + "' and Rpt_tb_year ='" + DropDownList1.SelectedValue + "'";
                Status = DBCon.Ora_Execute_CommamdText(Inssql1);
            }
                
                foreach (GridViewRow g1 in GridView1.Rows)
                {
                    string str = (g1.FindControl("bal_type") as Label).Text;
                    string str1 = (g1.FindControl("kat_cd") as Label).Text;
                    string str2 = (g1.FindControl("Label3") as Label).Text;
                    string str3 = (g1.FindControl("Label4") as Label).Text;
                    string chld = (g1.FindControl("Label2_nw") as Label).Text;
                    if (chld == "0")
                    {
                        string Inssql = "insert into KW_rpt_trialbalance (Rpt_tb_year,Rpt_tb_type,Rpt_tb_kod_akaun,Rpt_tb_debit_amt,Rpt_tb_kredit_amt,Rpt_tb_note,Status) values ('" + DropDownList1.SelectedValue + "','" + kat_bajet.SelectedValue + "','" + str + "','" + str2 + "','" + str3 + "','','A')";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql);
                    }                        
                }

                if (Status == "SUCCESS")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
          

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            BindGrid();
        }
    }

    protected void rest_but(object sender, EventArgs e)
    {
        // clr_txt();
        DropDownList1.SelectedValue = "";
        kat_bajet.SelectedValue = "01";
        BindGrid();
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        clr_txt();
    }
    void clr_txt()
    {
        get_profile_view();
        bind_kod_akaun();
        BindGrid();
        Button4.Text = "Simpan";
        hdr_txt.Text = "";
        kat_akaun.SelectedValue = "";
        TextBox2.Text = "";
        TextBox1.Text = "";
        dd_kodind.SelectedValue = "";
        dd_akaun.ClearSelection();
        RadioButton1.Checked = false;
        //set_kakaun.Visible = false;
        ss1.Visible = false;
        TextBox4.Text = "";
        txt_nota.Value = "";
    }
    protected void clk_submit(object sender, EventArgs e)
    {
        string samt1 = string.Empty, samt2 = string.Empty, samt3 = string.Empty;
        
        DataTable trbl = new DataTable();
        trbl = DBCon.Ora_Execute_table("select * from KW_rpt_trialbalance where Rpt_tb_kod_akaun ='" + Session["trail"] + "' and Rpt_tb_type='" + kat_bajet.SelectedValue + "' and Rpt_tb_year ='" + DropDownList1.SelectedValue + "'");

         if (trbl.Rows.Count != 0)
        {
            if (kat_bajet.SelectedValue != "")
            {
                if (kat_bajet.SelectedValue != "")
                {
                    string Inssql = "update KW_rpt_trialbalance set Rpt_tb_note = '" + txt_nota.Value + "'  where Rpt_tb_kod_akaun = '" + Session["trail"] + "' and Rpt_tb_type='" + kat_bajet.SelectedValue + "' and Rpt_tb_year ='" + DropDownList1.SelectedValue + "'";
                    Status = DBCon.Ora_Execute_CommamdText(Inssql);
                    if (Status == "SUCCESS")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    }
                   
                }

            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);            
        }
        BindGrid();
    }


    


    protected void ctk_values(object sender, EventArgs e)
    {


        string ss1 = string.Empty, ss2 = string.Empty, ss3 = string.Empty, ss4 = string.Empty, ss5 = string.Empty, ss6 = string.Empty, ss7 = string.Empty, ss8 = string.Empty, ss9 = string.Empty, ss10 = string.Empty, ss11 = string.Empty;
        
        DataSet ds1 = new DataSet();
        DataTable dt = new DataTable();
        DataTable dh = new DataTable();
        DataTable dh1 = new DataTable();
        bind_details();
        dt = Dbcon.Ora_Execute_table(sqry);

                ds1.Tables.Add(dt);
         
            RptviwerStudent.Reset();
            RptviwerStudent.LocalReport.Refresh();
            List<DataRow> listResult = dt.AsEnumerable().ToList();
          
            listResult.Count();
           
            int countRow = 0;
            countRow = listResult.Count();

            if (countRow != 0)
            {
            //ss1_stap1.Visible = true;
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
            RptviwerStudent.LocalReport.EnableExternalImages = true;
            RptviwerStudent.LocalReport.DataSources.Clear();

                ReportDataSource rds = new ReportDataSource("Imbungan", dt);
             
                RptviwerStudent.LocalReport.DataSources.Add(rds);
             
                RptviwerStudent.LocalReport.ReportPath = "kewengan/Kw_Imbungan.rdlc";
            // string branch;


            ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("d1", DropDownList1.SelectedItem.Text),
                     new ReportParameter("d2", kat_bajet.SelectedItem.Text),
                     new ReportParameter("d3",imagePath)

                };

            RptviwerStudent.LocalReport.SetParameters(rptParams);

                //Refresh
                RptviwerStudent.LocalReport.Refresh();
                Warning[] warnings;

                string[] streamids;

                string mimeType;

                string encoding;

                string extension;



                byte[] bytes = RptviwerStudent.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);



                Response.Buffer = true;

                Response.Clear();

                Response.ContentType = mimeType;

                Response.AddHeader("content-disposition", "attatchment; filename=MAKLUMAT_PERIBADI_ANGGOTA." + extension);


                Response.BinaryWrite(bytes);


                Response.Flush();

                Response.End();
            }
        
    }

  
    protected void ExportToEXCEL(object sender, EventArgs e)
    {
        
        DataTable dt = new DataTable();
        bind_details();
        dt = DBCon.Ora_Execute_table(sqry);
        if(dt.Rows.Count!= 0)
        {
            RptviwerStudent.Reset();

            List<DataRow> listResult = dt.AsEnumerable().ToList();
            listResult.Count();
            int countRow = 0;
            countRow = listResult.Count();
            double deb = 0,krd = 0;
            if (countRow != 0)
            {
                StringBuilder builder = new StringBuilder();
                string strFileName = string.Format("{0}.{1}", "Imbangan_Duga_" + DateTime.Now.ToString("ddMMyyyy") + "", "csv");
                builder.Append("No Akaun,Keterangan,Debit (RM),Kredit (RM)" + Environment.NewLine);
                for (int k = 0; k <= (dt.Rows.Count - 1); k++)
                {

                    builder.Append(dt.Rows[k]["kod_akaun"].ToString() + " , " + dt.Rows[k]["nama_akaun"].ToString().Replace(",", "") + ", " + dt.Rows[k]["Amt1"].ToString() + "," + dt.Rows[k]["Amt2"].ToString() +  Environment.NewLine);
                    deb += double.Parse(dt.Rows[k]["kw_Debit_amt"].ToString());
                    krd += double.Parse(dt.Rows[k]["kw_kredit_amt"].ToString());
                    if (k == (dt.Rows.Count - 1))
                    {
                        string val1 = "JUMLAH";
                        string val2 = string.Empty;
                        builder.Append(val1+", "+ val2 + ", "+deb.ToString()+","+krd.ToString() + Environment.NewLine);
                    }
                }
               
                Response.Clear();
                Response.ContentType = "text/csv";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + strFileName);
                Response.Write(builder.ToString());
                Response.End();

            }
            else if (countRow == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Rekod tidak dijumpai. Sila pastikan semua maklumat dimasukkan dengan betul');", true);

                // txtError.Text = "Rekod tidak dijumpai. Sila pastikan semua maklumat dimasukkan dengan betul.";
            }
        }
        else
        {
            BindData();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }

    }

}







    