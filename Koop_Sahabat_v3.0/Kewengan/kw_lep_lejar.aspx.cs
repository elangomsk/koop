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

public partial class kw_lep_lejar : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    private static int PageSize = 20;
    string qry1 = string.Empty, qry2 = string.Empty, qry3 = string.Empty, qry4 = string.Empty;
    string sqry1 = string.Empty, sqry2 = string.Empty;
    string level;
    string Status = string.Empty;
    string userid;
    string CommandArgument1 = string.Empty, CommandArgument2 = string.Empty, CommandArgument3 = string.Empty;
    string rcount = string.Empty, rcount1 = string.Empty;
    int count = 0, count1 = 1, pyr = 0, prdt = 0;
    string ss1 = string.Empty;
    string selectedValues = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        //ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        //scriptManager.RegisterPostBackControl(this.Button4);
        Button4.OnClientClick = @"if(this.value == 'Please wait...')
           return false;
           this.value = 'Please wait...';this.disabled=true";
        string script = " $(document).ready(function () { $(" + chk_lst.ClientID + ").SumoSelect({ selectAll: true }); $('.select2').select2();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
       
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
                bind_kod_akaun();
                bind_inv();
                BindData();
                DropDownList1.SelectedValue = "0";
                project();
                get_jurnal_no();
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('822','705','1724','64','65','823','824','169','1840','825','121','15')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower()); 
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl10.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            ps_lbl11.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
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
            string com = "select Ref_Projek_code,Ref_Projek_name from  KW_Ref_Projek where Status='A'";
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

    void get_jurnal_no()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select GL_journal_no from KW_General_Ledger where GL_sts='L' group by GL_journal_no";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList2.DataSource = dt;
            DropDownList2.DataTextField = "GL_journal_no";
            DropDownList2.DataValueField = "GL_journal_no";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void bind_gview(object sender, EventArgs e)
    {
        bind_kod_akaun();
        string script = " $(document).ready(function () { $(" + chk_lst.ClientID + ").SumoSelect({ selectAll: true });$('.select2').select2();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
    }
    protected void select_project(object sender, EventArgs e)
    {
        bind_inv();
    }

    void bind_inv()
    {
        try
        {
            string com = string.Empty;
            DataSet Ds = new DataSet();
           
            SqlDataAdapter adpt = new SqlDataAdapter("select GL_invois_no from KW_General_Ledger where GL_sts='L' group by GL_invois_no", con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_invois_no.DataSource = dt;
            dd_invois_no.DataTextField = "GL_invois_no";
            dd_invois_no.DataValueField = "GL_invois_no";
            dd_invois_no.DataBind();
            dd_invois_no.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
        void bind_kod_akaun()
    {

        string get_qry = string.Empty;

        if (DropDownList1.SelectedValue == "1")
        {
            get_qry = "select kod_akaun,(kod_akaun +' | '+ upper(nama_akaun)) as name from KW_Ref_Carta_Akaun where jenis_akaun_type != '1' and Status='A' order by kod_akaun asc";
        }
        else if (DropDownList1.SelectedValue == "2")
        {
            get_qry = "SELECT s1.[kod_akaun],(s1.[kod_akaun] +' | '+ upper(s2.nama_akaun)) as name FROM [KW_Penerimaan_invois] m1 INNER JOIN [KW_General_Ledger] s1 ON m1.[no_invois] = s1.[GL_invois_no] INNER join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=s1.kod_akaun";
        }
        else if (DropDownList1.SelectedValue == "3")
        {
            get_qry = "SELECT s1.[kod_akaun],(s1.[kod_akaun] +' | '+ upper(s2.nama_akaun)) as name FROM [KW_Pembayaran_invois] m1 INNER JOIN [KW_General_Ledger] s1 ON m1.[no_invois] = s1.[GL_invois_no] INNER join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=s1.kod_akaun";
        }
        else if (DropDownList1.SelectedValue == "4")
        {
            get_qry = "select kod_akaun,(kod_akaun +' | '+ upper(nama_akaun)) as name from KW_Ref_Carta_Akaun where jenis_akaun_type != '1' and jenis_akaun='12.01' order by kod_akaun asc";
        }
        else
        {

            get_qry = "select kod_akaun,(kod_akaun +' | '+ upper(nama_akaun)) as name from KW_Ref_Carta_Akaun where jenis_akaun_type != '1' and Status='A' order by kod_akaun asc";

        }

        DataSet ds = new DataSet();

        string cmdstr = get_qry;

        SqlDataAdapter adp = new SqlDataAdapter(cmdstr, con);

        adp.Fill(ds);



        if (ds.Tables[0].Rows.Count > 0)

        {

            chk_lst.DataSource = ds.Tables[0];

            chk_lst.DataTextField = "name";

            chk_lst.DataValueField = "kod_akaun";

            chk_lst.DataBind();

        }

    }
    protected void BindData()
    {

    }

    void get_data()
    {
       
        foreach (ListItem li in chk_lst.Items)
        {
            if (li.Selected == true)
            {
                count++;
            }
            rcount = count.ToString();
        }
       
        foreach (ListItem li in chk_lst.Items)
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

        if (selectedValues != "" && ddpro.SelectedValue != "" && dd_invois_no.SelectedValue != "" && DropDownList2.SelectedValue != "")
        {
            qry2 = "where a.kod_akaun IN ('"+ selectedValues.Replace(",","','") + "') and project_kod ='"+ ddpro.SelectedValue + "' and no_invois='"+ dd_invois_no.SelectedValue + "' and GL_journal_no='"+ DropDownList2.SelectedValue + "'";

            qry3 = "and kod_akaun IN ('" + selectedValues.Replace(",", "','") + "') and project_kod ='" + ddpro.SelectedValue + "' and GL_invois_no='" + dd_invois_no.SelectedValue + "' and GL_journal_no='" + DropDownList2.SelectedValue + "'";

            qry4 = " and s1.kod_akaun IN ('" + selectedValues.Replace(",", "','") + "')";
        }
        else if (selectedValues != "" && ddpro.SelectedValue != "" && dd_invois_no.SelectedValue != "" && DropDownList2.SelectedValue == "")
        {
            qry2 = "where a.kod_akaun IN ('" + selectedValues.Replace(",", "','") + "') and project_kod ='" + ddpro.SelectedValue + "' and no_invois='" + dd_invois_no.SelectedValue + "' ";

            qry3 = "and kod_akaun IN ('" + selectedValues.Replace(",", "','") + "') and project_kod ='" + ddpro.SelectedValue + "' and GL_invois_no='" + dd_invois_no.SelectedValue + "'";
            qry4 = " and s1.kod_akaun IN ('" + selectedValues.Replace(",", "','") + "')";
        }
        else if (selectedValues != "" && ddpro.SelectedValue != "" && dd_invois_no.SelectedValue == "" && DropDownList2.SelectedValue == "")
        {
            qry2 = "where a.kod_akaun IN ('" + selectedValues.Replace(",", "','") + "') and project_kod ='" + ddpro.SelectedValue + "' ";

            qry3 = "and kod_akaun IN ('" + selectedValues.Replace(",", "','") + "') and project_kod ='" + ddpro.SelectedValue + "'";
            qry4 = " and s1.kod_akaun IN ('" + selectedValues.Replace(",", "','") + "')";
        }
        else if (selectedValues == "" && ddpro.SelectedValue != "" && dd_invois_no.SelectedValue != "" && DropDownList2.SelectedValue == "")
        {
            qry2 = "where project_kod ='" + ddpro.SelectedValue + "' and no_invois='" + dd_invois_no.SelectedValue + "' ";

            qry3 = "and project_kod ='" + ddpro.SelectedValue + "' and GL_invois_no='" + dd_invois_no.SelectedValue + "'";
        }
        else if (selectedValues == "" && ddpro.SelectedValue == "" && dd_invois_no.SelectedValue != "" && DropDownList2.SelectedValue != "")
        {
            qry2 = "where no_invois='" + dd_invois_no.SelectedValue + "' and GL_journal_no='" + DropDownList2.SelectedValue + "'";

            qry3 = "and GL_invois_no='" + dd_invois_no.SelectedValue + "' and GL_journal_no='" + DropDownList2.SelectedValue + "'";
        }
        else if (selectedValues == "" && ddpro.SelectedValue != "" && dd_invois_no.SelectedValue == "" && DropDownList2.SelectedValue != "")
        {
            qry2 = "where  project_kod ='" + ddpro.SelectedValue + "' and GL_journal_no='" + DropDownList2.SelectedValue + "'";
            qry3 = "and project_kod ='" + ddpro.SelectedValue + "' and GL_journal_no='" + DropDownList2.SelectedValue + "'";
        }
        else if (selectedValues != "" && ddpro.SelectedValue == "" && dd_invois_no.SelectedValue == "" && DropDownList2.SelectedValue == "")
        {
            qry2 = "where a.kod_akaun IN ('" + selectedValues.Replace(",", "','") + "')";
            qry3 = "and kod_akaun IN ('" + selectedValues.Replace(",", "','") + "')";
            qry4 = " and s1.kod_akaun IN ('" + selectedValues.Replace(",", "','") + "')";
        }
        else if (selectedValues == "" && ddpro.SelectedValue != "" && dd_invois_no.SelectedValue == "" && DropDownList2.SelectedValue == "")
        {
            qry2 = "where project_kod ='" + ddpro.SelectedValue + "'";

            qry3 = "and project_kod ='" + ddpro.SelectedValue + "' ";
        }
        else if (selectedValues == "" && ddpro.SelectedValue == "" && dd_invois_no.SelectedValue != "" && DropDownList2.SelectedValue == "")
        {
            qry2 = "where no_invois='" + dd_invois_no.SelectedValue + "'";
            qry3 = "and GL_invois_no='" + dd_invois_no.SelectedValue + "'";
        }
        else if (selectedValues == "" && ddpro.SelectedValue == "" && dd_invois_no.SelectedValue == "" && DropDownList2.SelectedValue != "")
        {
            qry2 = "where GL_journal_no='" + DropDownList2.SelectedValue + "'";
            qry3 = "and  GL_journal_no='" + DropDownList2.SelectedValue + "'";
        }
        else if (selectedValues != "" && ddpro.SelectedValue != "" && dd_invois_no.SelectedValue == "" && DropDownList2.SelectedValue != "")
        {
            qry2 = "where a.kod_akaun IN ('" + selectedValues.Replace(",", "','") + "') and project_kod ='" + ddpro.SelectedValue + "' and GL_journal_no='" + DropDownList2.SelectedValue + "'";
            qry3 = "and kod_akaun IN ('" + selectedValues.Replace(",", "','") + "') and project_kod ='" + ddpro.SelectedValue + "' and GL_journal_no='" + DropDownList2.SelectedValue + "'";
            qry4 = " and s1.kod_akaun IN ('" + selectedValues.Replace(",", "','") + "')";
        }
        else if (selectedValues == "" && ddpro.SelectedValue != "" && dd_invois_no.SelectedValue != "" && DropDownList2.SelectedValue != "")
        {
            qry2 = "where project_kod ='" + ddpro.SelectedValue + "' and no_invois='" + dd_invois_no.SelectedValue + "' and GL_journal_no='" + DropDownList2.SelectedValue + "'";
            qry3 = "and project_kod ='" + ddpro.SelectedValue + "' and GL_invois_no='" + dd_invois_no.SelectedValue + "' and GL_journal_no='" + DropDownList2.SelectedValue + "'";
        }
        else 
        {
            qry2 = "";
            qry3 = "";
        }

    }

    protected void clk_submit(object sender, EventArgs e)
    {
        if (Tk_mula.Text != "" && Tk_akhir.Text != "")
        {
           

            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            string fmdate = string.Empty, fmday = string.Empty, fmonth = string.Empty, pyear = string.Empty, fyear = string.Empty, stdate = string.Empty, tmdate = string.Empty, pre_day = string.Empty;
            string sqry2 = string.Empty;
            if (Tk_mula.Text != "")
            {
                string fdate = Tk_mula.Text;
                DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                fmdate = fd.ToString("yyyy-MM-dd");
                pre_day = fd.AddDays(-1).ToString("yyyy-MM-dd");
                fmonth = fd.ToString("MM");
                fyear = fd.ToString("yyyy");                
                pyr = (Convert.ToInt32(fyear) - 1);
                stdate = pyr + "-01-01";
            }
            if (Tk_akhir.Text != "")
            {
                string tdate = Tk_akhir.Text;
                DateTime td = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                tmdate = td.ToString("yyyy-MM-dd");
            }

            get_data();
                    if (zero_bal.Checked == true)
                        {
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
                                    + " left join KW_General_Ledger m1_1 on s1.kod_akaun = m1_1.kod_akaun and m1_1.GL_sts='L' and YEAR(m1_1.GL_post_dt)='"+ fyear + "' and  m1_1.GL_post_dt < DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) "
                                    + " left join KW_General_Ledger m1 on s1.kod_akaun = m1.kod_akaun and m1.GL_sts='L' and m1.GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and m1.GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) "
                                    + " left join KW_Kategori_akaun s11 on s11.kat_type='01' and s11.kat_cd=s1.kat_akaun "                                    
                                    + " left join KW_Ref_Doc_kod s3 on s3.Ref_doc_code = m1.ref2"
                                    + " where s1.Status = 'A' and s1.jenis_akaun_type !='1' and ISNULL(ca_cyp,'') !='1' ) as a "
                                    + " "+ qry2 + " "
                                    + " order by a.kod_akaun";

                sqry2 = " select  sum(a.opening_amt) open_amt from (select distinct s1.kat_akaun kcd,s11.kat_akuan kname,s1.kod_akaun,s1.nama_akaun, "
                      + " case when ISNULL(s2.opn_debit_amt,'0.00')='0.00' then (ISNULL(s2.opn_kredit_amt,'0.00'  ) + (ISNULL(m1_1.KW_kredit_amt,'0.00') - ISNULL(m1_1.KW_Debit_amt,'0.00'))) else ((ISNULL(s2.opn_debit_amt,'0.00') + 	(ISNULL(m1_1.KW_Debit_amt,'0.00') - ISNULL(m1_1.KW_kredit_amt,'0.00')))) end as opening_amt  "
                      + "  from KW_Ref_Carta_Akaun s1 left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = s1.kod_akaun and '" + fmdate + "' between s2.start_dt and s2.end_dt   "
                      + " outer apply(select ISNULL(sum(m1_1.KW_kredit_amt),'0.00') KW_kredit_amt,ISNULL(sum(m1_1.KW_kredit_amt),'0.00')  KW_Debit_amt  from KW_General_Ledger m1_1 where s1.kod_akaun = m1_1.kod_akaun and m1_1.GL_sts='L' and YEAR(m1_1.GL_post_dt)='" + fyear + "' and  m1_1.GL_post_dt < DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0)) as m1   "
                      + " outer apply(select sum(ISNULL(m1.KW_kredit_amt,'0.00')) KW_kredit_amt,sum(ISNULL(m1.KW_Debit_amt,'0.00')) KW_Debit_amt  from KW_General_Ledger m1 where s1.kod_akaun = m1.kod_akaun and m1.GL_sts='L' and  m1.GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and m1.GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) " + qry3 + ") as m1_1  "
                      + " left join KW_Kategori_akaun s11 on s11.kat_type='01' and s11.kat_cd=s1.kat_akaun  left join KW_Ref_Doc_kod s3 on s3.Ref_doc_code = '' where  s1.Status = 'A' " + qry4 + "  "
                      + " and s1.jenis_akaun_type !='1' and ISNULL(ca_cyp,'') !='1' ) as a  ";
                                 

            }
                        else
                        {
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
                        + " where s1.Status = 'A' and s1.jenis_akaun_type !='1' and ISNULL(ca_cyp,'') !='1'  and (opening_amt != '0.00' or m1.KW_Debit_amt != '0.00' or m1.KW_kredit_amt !='0.00')) as a "
                        + " " + qry2 + " "
                        + " order by a.kod_akaun";

                sqry2 = " select  sum(a.opening_amt) open_amt from (select distinct s1.kat_akaun kcd,s11.kat_akuan kname,s1.kod_akaun,s1.nama_akaun, "
                       + " case when ISNULL(s2.opn_debit_amt,'0.00')='0.00' then (ISNULL(s2.opn_kredit_amt,'0.00'  ) + (ISNULL(m1_1.KW_kredit_amt,'0.00') - ISNULL(m1_1.KW_Debit_amt,'0.00'))) else ((ISNULL(s2.opn_debit_amt,'0.00') + 	(ISNULL(m1_1.KW_Debit_amt,'0.00') - ISNULL(m1_1.KW_kredit_amt,'0.00')))) end as opening_amt  "
                       + "from KW_Ref_Carta_Akaun s1 left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = s1.kod_akaun and '" + fmdate + "' between s2.start_dt and s2.end_dt   "
                     + " outer apply(select ISNULL(sum(m1_1.KW_kredit_amt),'0.00') KW_kredit_amt,ISNULL(sum(m1_1.KW_kredit_amt),'0.00')  KW_Debit_amt  from KW_General_Ledger m1_1 where s1.kod_akaun = m1_1.kod_akaun and m1_1.GL_sts='L' and YEAR(m1_1.GL_post_dt)='" + fyear + "' and  m1_1.GL_post_dt < DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0)) as m1   "
                      + " outer apply(select sum(ISNULL(m1.KW_kredit_amt,'0.00')) KW_kredit_amt,sum(ISNULL(m1.KW_Debit_amt,'0.00')) KW_Debit_amt  from KW_General_Ledger m1 where s1.kod_akaun = m1.kod_akaun and m1.GL_sts='L' and  m1.GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and m1.GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) "+ qry3 + ") as m1_1  "
                       + " left join KW_Kategori_akaun s11 on s11.kat_type='01' and s11.kat_cd=s1.kat_akaun  left join KW_Ref_Doc_kod s3 on s3.Ref_doc_code = '' where s1.Status = 'A' " + qry4 + "  "
                       + " and s1.jenis_akaun_type !='1' and ISNULL(ca_cyp,'') !='1'  and (opening_amt != '0.00' or m1.KW_Debit_amt != '0.00' or m1.KW_kredit_amt !='0.00')) as a ";
                       
            }
                    
            dt = DBCon.Ora_Execute_table(sqry1);
            dt1 = DBCon.Ora_Execute_table(sqry2);
            ds.Tables.Add(dt);
            //ds1.Tables.Add(dt1);

            string vs1 = string.Empty, vs2 = string.Empty, vs3 = string.Empty, vs4 = string.Empty, vs5 = string.Empty, vs6 = string.Empty;

            if (DropDownList1.SelectedValue != "")
            {
                vs1 = DropDownList1.SelectedItem.Text;
            }
            else
            {
                vs1 = "SEMUA PILIH";
            }



            if (Tk_mula.Text != "")
            {
                vs3 = Tk_mula.Text;
            }
            else
            {
                vs3 = "-";
            }

            if (Tk_akhir.Text != "")
            {
                vs4 = Tk_akhir.Text;
            }
            else
            {
                vs4 = "-";
            }

            if (zero_bal.Checked == true)
            {
                vs6 = "YES";
            }
            else
            {
                vs6 = "NO";
            }
            Rptviwerlejar.Reset();
            Rptviwerlejar.LocalReport.Refresh();
            List<DataRow> listResult = dt.AsEnumerable().ToList();
            listResult.Count();
            int countRow = 0;
            countRow = listResult.Count();



            Rptviwerlejar.LocalReport.DataSources.Clear();
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
                Rptviwerlejar.LocalReport.EnableExternalImages = true;
                if (rpt_type.SelectedValue == "01")
                {
                    Rptviwerlejar.LocalReport.ReportPath = "Kewengan/KW_Leger.rdlc";
                }
                else
                {
                    Rptviwerlejar.LocalReport.ReportPath = "Kewengan/KW_Leger_ls.rdlc";
                }
                ReportDataSource rds = new ReportDataSource("kwlegar", dt);
                //ReportDataSource rds1 = new ReportDataSource("kwlegar1", dt1);
                ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("s1", vs1),
                     new ReportParameter("s2", vs6),
                     new ReportParameter("s3", vs3),
                     new ReportParameter("s4", vs4),
                     new ReportParameter("S5", double.Parse(dt1.Rows[0]["open_amt"].ToString()).ToString("C").Replace("RM","").Replace("$","")),
                     new ReportParameter("v1",imagePath)
                          };

                Rptviwerlejar.LocalReport.SetParameters(rptParams);
                Rptviwerlejar.LocalReport.DataSources.Add(rds);
                //Rptviwerlejar.LocalReport.DataSources.Add(rds1);
                Rptviwerlejar.LocalReport.DisplayName = "LEJER_AM_" + DateTime.Now.ToString("yyyyMMdd");
                
                Rptviwerlejar.LocalReport.Refresh();

                System.Threading.Thread.Sleep(1);

                //Warning[] warnings;

                //string[] streamids;

                //string mimeType;

                //string encoding;

                //string extension;

                //string fname = DateTime.Now.ToString("dd_MM_yyyy");

                //string devinfo = "<DeviceInfo><ColorDepth>32</ColorDepth><DpiX>350</DpiX><DpiY>350</DpiY><OutputFormat>PDF</OutputFormat>" +
                //       "  <PageWidth>12.20in</PageWidth>" +
                //        "  <PageHeight>8.27in</PageHeight>" +
                //        "  <MarginTop>0.1in</MarginTop>" +
                //        "  <MarginLeft>0.5in</MarginLeft>" +
                //         "  <MarginRight>0in</MarginRight>" +
                //         "  <MarginBottom>0in</MarginBottom>" +
                //       "</DeviceInfo>";

                //byte[] bytes = Rptviwerlejar.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);

                //Response.Buffer = true;
                //Response.Clear();
                //Response.ContentType = mimeType;
                //Response.AddHeader("content-disposition", "Attatchment; filename=Lejar_AM" + DateTime.Now.ToString("ddMMyyyy") +"." + extension);
                //Response.BinaryWrite(bytes);
                //Response.Flush();
                //Response.End();

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
        Response.Redirect("../kewengan/kw_lep_lejar.aspx");
    }


}