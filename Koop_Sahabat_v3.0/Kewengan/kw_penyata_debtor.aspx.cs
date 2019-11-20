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

public partial class kw_penyata_debtor : System.Web.UI.Page
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
    string rcount = string.Empty, rcount1 = string.Empty;
    int count = 0, count1 = 1, pyr = 0, prdt = 0;
    string ss1 = string.Empty;
    string selectedValues = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        Button4.OnClientClick = @"if(this.value == 'Please wait...')
           return false;
           this.value = 'Please wait...';this.disabled=true";
        string script = " $(document).ready(function () { $('.select2').select2();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
                pelanggan();                
                
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
            //ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            
            Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
           
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    void pelanggan()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select Ref_no_syarikat,Ref_nama_syarikat from KW_Ref_Pelanggan where Status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddpro.DataSource = dt;
            ddpro.DataTextField = "Ref_nama_syarikat";
            ddpro.DataValueField = "Ref_no_syarikat";
            ddpro.DataBind();
            ddpro.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

  
    void get_value()
    {
       
       
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
            }
          
            
            py_fdate = fmdate;
            py_ldate = tmdate;
                        
                    qry1 = "select c.Ref_nama_syarikat,c.Ref_no_syarikat,c.Ref_no_telefone,Ref_alamat,pel_bandar,s4.Decription pel_negeri,Ref_email,s3.Bank_Name,pel_bank_accno,pel_credit_lmt,a.no_invois,a.perkera,Format(a.tarikh_invois, 'dd/MM/yyyy') tarikh_invois,case when (ISNULL(DATEDIFF(day, b.tarikh_resit, a.tarikh_invois),'0') - ISNULL(a.Terma,'0')) > '0' then (ISNULL(DATEDIFF(day, b.tarikh_resit, a.tarikh_invois),'0') - ISNULL(a.Terma,'0')) else '0' end as overdue , "
                    + " case when (ISNULL(b.Overall,'0.00') - a.Overall) = '0.00' then 'CLOSE' else 'OPEN' end as Status, A.overall amaun,ISNULL(b.Overall,'0.00') payment, (ISNULL(b.Overall,'0.00') - a.Overall) baki "
                    + " from KW_Penerimaan_invois  as a  outer apply (select * from kw_penerimaan_resit s1 where s1.no_invois=a.no_invois and s1.nama_pelanggan_code=a.nama_pelanggan_code) as b "
                    + " outer apply(SELECT *  FROM KW_Ref_Pelanggan s2 where s2.Ref_no_syarikat=a.nama_pelanggan_code and s2.Status='A') as c left join ref_nama_bank s3 on s3.Bank_Code=pel_bank_cd left join Ref_Negeri s4 on s4.Decription_Code=pel_negeri where c.Ref_no_syarikat='" + ddpro.SelectedValue + "' "
                    + " and b.tarikh_resit>=DATEADD(day, DATEDIFF(day, 0, '"+ fmdate + "'), 0) and b.tarikh_resit<=DATEADD(day, DATEDIFF(day, 0, '"+ tmdate + "'), +1) ";

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
                        
                        DataTable get_pfl = new DataTable();
                        get_pfl = DBCon.Ora_Execute_table("select syar_logo,fin_year from KW_Profile_syarikat m1 inner join kw_financial_year s1 on s1.fin_kod_syarikat=kod_syarikat and s1.Status='1'  where cur_sts='1' and m1.Status='A'");

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
                        Rptviwerlejar.LocalReport.ReportPath = "Kewengan/KW_penyata_deb.rdlc";
                        ReportDataSource rds = new ReportDataSource("penyata_deb", dt);


                        ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("d1",""),
                     new ReportParameter("d2", ""),
                     new ReportParameter("d3", Tk_mula.Text),
                     new ReportParameter("d4", Tk_akhir.Text),
                     new ReportParameter("d5", get_pfl.Rows[0]["fin_year"].ToString()),
                     new ReportParameter("d6", ""),
                     new ReportParameter("v1",imagePath)
                };


                        Rptviwerlejar.LocalReport.SetParameters(rptParams);
                        Rptviwerlejar.LocalReport.DataSources.Add(rds);
                        Rptviwerlejar.LocalReport.DisplayName = "PENYATA_DEBTOR_"+ ddpro.SelectedItem.Text +"_" + DateTime.Now.ToString("yyyyMMdd");
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
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

  

    protected void btn_reset(object sender, EventArgs e)
    {
        Response.Redirect("../kewengan/kw_lep_pl.aspx");
    }


}