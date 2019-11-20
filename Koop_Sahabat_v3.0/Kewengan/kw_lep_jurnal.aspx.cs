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

public partial class kw_lep_jurnal : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    private static int PageSize = 20;
    string qry1 = string.Empty, qry2 = string.Empty;
    string sqry1 = string.Empty, sqry2 = string.Empty;
    string level;
    string Status = string.Empty;
    string userid;
    string ss1 = string.Empty;
    string CommandArgument1 = string.Empty, CommandArgument2 = string.Empty, CommandArgument3 = string.Empty;
    string fmdate = string.Empty, fmonth = string.Empty, fyear = string.Empty, stdate = string.Empty, tmdate = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        Button4.OnClientClick = @"if(this.value == 'Please wait...')
           return false;
           this.value = 'Please wait...';this.disabled=true";
        string script1 = "$(function () { $('.select2').select2() });";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);

        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {

                level = Session["level"].ToString();
                userid = Session["New"].ToString();
                //bind_kod_akaun();
                bind_project();
                bind_invois_no();
                bind_jen_jurnal();
                BindData();
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('1844','705','1724','64','65','824','825','121','15')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());    
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());

        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }

    void bind_project()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select Ref_Projek_code,Ref_Projek_name from KW_Ref_Projek where Status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_projek.DataSource = dt;
            dd_projek.DataTextField = "Ref_Projek_name";
            dd_projek.DataValueField = "Ref_Projek_code";
            dd_projek.DataBind();
            dd_projek.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void bind_invois_no()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select GL_invois_no from KW_General_Ledger where GL_sts='L' group by GL_invois_no";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_invois.DataSource = dt;
            dd_invois.DataTextField = "GL_invois_no";
            dd_invois.DataValueField = "GL_invois_no";
            dd_invois.DataBind();
            dd_invois.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void bind_jen_jurnal()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select jur_type_cd,jur_desc from KW_ref_jurnal_type where Status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList1.DataSource = dt;
            DropDownList1.DataTextField = "jur_desc";
            DropDownList1.DataValueField = "jur_type_cd";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void bind_gview(object sender, EventArgs e)
    {

        bind_kod_akaun();
        string script = " $(document).ready(function () { $(" + chk_lst.ClientID + ").SumoSelect({ selectAll: true });});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
    }

    void bind_kod_akaun()
    {

        //string get_qry = string.Empty;

        //if(DropDownList1.SelectedValue =="1")
        //{
        //    get_qry = "select kod_akaun,(kod_akaun +' | '+ upper(nama_akaun)) as name from KW_Ref_Carta_Akaun where jenis_akaun_type = '1' order by kod_akaun asc";
        //}
        //else if (DropDownList1.SelectedValue == "2")
        //{
        //    get_qry = "select kod_akaun,(kod_akaun +' | '+ upper(nama_akaun)) as name from KW_Ref_Carta_Akaun inner join KW_Ref_Pelanggan on Ref_kod_akaun = kod_akaun where jenis_akaun_type != '1' order by kod_akaun asc";
        //}
        //else if (DropDownList1.SelectedValue == "3")
        //{
        //    get_qry = "select kod_akaun,(kod_akaun +' | '+ upper(nama_akaun)) as name from KW_Ref_Carta_Akaun inner join KW_Ref_Pembekal on Ref_kod_akaun = kod_akaun where jenis_akaun_type != '1' order by kod_akaun asc";
        //}
        //else
        //{

        //    get_qry = "select kod_akaun,(kod_akaun +' | '+ upper(nama_akaun)) as name from KW_Ref_Carta_Akaun where jenis_akaun_type != '1' order by kod_akaun asc";

        //}

        //DataSet ds = new DataSet();

        //string cmdstr = get_qry;

        //SqlDataAdapter adp = new SqlDataAdapter(cmdstr, con);

        //adp.Fill(ds);



        //if (ds.Tables[0].Rows.Count > 0)

        //{

        //    chk_lst.DataSource = ds.Tables[0];

        //    chk_lst.DataTextField = "name";

        //    chk_lst.DataValueField = "kod_akaun";

        //    chk_lst.DataBind();

        //}
    }
    protected void BindData()
    {

    }

    protected void get_values()
    {
        if (Tk_mula.Text != "" && Tk_akhir.Text != "")
        {
          
            if (Tk_mula.Text != "")
            {
                string fdate = Tk_mula.Text;
                DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                fmdate = fd.ToString("yyyy-MM-dd");
                fmonth = fd.ToString("MM");
                fyear = fd.ToString("yyyy");
                stdate = fyear + "-01-01";
            }
            if (Tk_akhir.Text != "")
            {
                string tdate = Tk_akhir.Text;
                DateTime td = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                tmdate = td.ToString("yyyy-MM-dd");
            }


            string tname1 = string.Empty;
            if (dd_projek.SelectedValue != "" && dd_invois.SelectedValue != "" && DropDownList1.SelectedValue != "" && txt_jno.Text != "")
            {
                ss1 = " and project_kod = '" + dd_projek.SelectedValue + "' and [GL_invois_no] ='" + dd_invois.SelectedValue + "' and GL_type='" + DropDownList1.SelectedValue + "' and GL_journal_no='" + txt_jno.Text + "'";
            }
            else if (dd_projek.SelectedValue != "" && dd_invois.SelectedValue == "" && DropDownList1.SelectedValue == "" && txt_jno.Text == "")
            {
                ss1 = " and project_kod = '" + dd_projek.SelectedValue + "'";
            }
            else if (dd_projek.SelectedValue == "" && dd_invois.SelectedValue != "" && DropDownList1.SelectedValue == "" && txt_jno.Text == "")
            {
                ss1 = " and [GL_invois_no] ='" + dd_invois.SelectedValue + "'";
            }
            else if (dd_projek.SelectedValue == "" && dd_invois.SelectedValue == "" && DropDownList1.SelectedValue != "" && txt_jno.Text == "")
            {
                ss1 = " and GL_type='" + DropDownList1.SelectedValue + "'";
            }
            else if (dd_projek.SelectedValue == "" && dd_invois.SelectedValue == "" && DropDownList1.SelectedValue == "" && txt_jno.Text != "")
            {
                ss1 = " and GL_journal_no='" + txt_jno.Text + "'";
            }
            else if (dd_projek.SelectedValue != "" && dd_invois.SelectedValue != "" && DropDownList1.SelectedValue == "" && txt_jno.Text == "")
            {
                ss1 = " and project_kod = '" + dd_projek.SelectedValue + "' and [GL_invois_no] ='" + dd_invois.SelectedValue + "' ";
            }
            else if (dd_projek.SelectedValue != "" && dd_invois.SelectedValue == "" && DropDownList1.SelectedValue != "" && txt_jno.Text == "")
            {
                ss1 = " and project_kod = '" + dd_projek.SelectedValue + "' and GL_type='" + DropDownList1.SelectedValue + "'";
            }
            else if (dd_projek.SelectedValue != "" && dd_invois.SelectedValue == "" && DropDownList1.SelectedValue == "" && txt_jno.Text != "")
            {
                ss1 = " and project_kod = '" + dd_projek.SelectedValue + "' and GL_journal_no='" + txt_jno.Text + "'";
            }
            else if (dd_projek.SelectedValue == "" && dd_invois.SelectedValue != "" && DropDownList1.SelectedValue != "" && txt_jno.Text == "")
            {
                ss1 = " and [GL_invois_no] ='" + dd_invois.SelectedValue + "' and GL_type='" + DropDownList1.SelectedValue + "'";
            }
            else if (dd_projek.SelectedValue == "" && dd_invois.SelectedValue == "" && DropDownList1.SelectedValue != "" && txt_jno.Text != "")
            {
                ss1 = " and GL_type='" + DropDownList1.SelectedValue + "' and GL_journal_no='" + txt_jno.Text + "'";
            }
            else if (dd_projek.SelectedValue != "" && dd_invois.SelectedValue != "" && DropDownList1.SelectedValue != "" && txt_jno.Text == "")
            {
                ss1 = " and project_kod = '" + dd_projek.SelectedValue + "' and [GL_invois_no] ='" + dd_invois.SelectedValue + "' and GL_type='" + DropDownList1.SelectedValue + "'";
            }
            else if (dd_projek.SelectedValue != "" && dd_invois.SelectedValue != "" && DropDownList1.SelectedValue == "" && txt_jno.Text != "")
            {
                ss1 = " and project_kod = '" + dd_projek.SelectedValue + "' and [GL_invois_no] ='" + dd_invois.SelectedValue + "' and GL_journal_no='" + txt_jno.Text + "'";
            }
            else if (dd_projek.SelectedValue != "" && dd_invois.SelectedValue == "" && DropDownList1.SelectedValue != "" && txt_jno.Text != "")
            {
                ss1 = " and project_kod = '" + dd_projek.SelectedValue + "' and GL_type='" + DropDownList1.SelectedValue + "' and GL_journal_no='" + txt_jno.Text + "'";
            }
            else if (dd_projek.SelectedValue == "" && dd_invois.SelectedValue != "" && DropDownList1.SelectedValue != "" && txt_jno.Text != "")
            {
                ss1 = " and [GL_invois_no] ='" + dd_invois.SelectedValue + "' and GL_type='" + DropDownList1.SelectedValue + "' and GL_journal_no='" + txt_jno.Text + "'";
            }
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
            get_values();
            sqry1 = "select c.*,b.*,a.* from (select project_kod,GL_type,Format(GL_post_dt, 'dd/MM/yyyy') post_dt,GL_post_dt, [kod_akaun], [GL_journal_no], [GL_desc1], [GL_invois_no],sum( [KW_Debit_amt]) as deb_amt, sum([KW_kredit_amt]) kre_amt from KW_General_Ledger where GL_sts='L' GROUP BY project_kod,GL_type,[GL_post_dt], [kod_akaun], [GL_journal_no], [GL_desc1], [GL_invois_no]) as a "
                + "outer apply(select jur_desc from KW_ref_jurnal_type where jur_type_cd=GL_type) as b "
                + "outer apply(select Ref_Projek_name from KW_Ref_Projek where Ref_Projek_code=project_kod) as c "
                + "where GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) " + ss1 +""
                + "order by project_kod,GL_type";


            dt = DBCon.Ora_Execute_table(sqry1);
            //dt1 = DBCon.Ora_Execute_table(sqry2);
            ds.Tables.Add(dt);
            //ds1.Tables.Add(dt1);

            string vs1 = string.Empty, vs2 = string.Empty, vs3 = string.Empty, vs4 = string.Empty, vs5 = string.Empty, vs6 = string.Empty;

            if(dd_projek.SelectedValue != "")
            {
                vs1 = dd_projek.SelectedItem.Text;
            }
            else
            {
                vs1 = "SEMUA";
            }

            if (dd_invois.SelectedValue != "")
            {
                vs2 = dd_invois.SelectedItem.Text;
            }
            else
            {
                vs2 = "SEMUA";
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

            if (DropDownList1.SelectedValue != "")
            {
                vs5 = DropDownList1.SelectedItem.Text;
            }
            else
            {
                vs5 = "SEMUA";
            }
            if (txt_jno.Text != "")
            {
                vs6 = txt_jno.Text;
            }
            else
            {
                vs6 = "SEMUA";
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
                Button2.Visible = true;
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

                Rptviwerlejar.LocalReport.ReportPath = "kewengan/KW_lep_journal.rdlc";
                Rptviwerlejar.LocalReport.EnableExternalImages = true;
                ReportDataSource rds = new ReportDataSource("kwjurnal_new", dt);
                //ReportDataSource rds1 = new ReportDataSource("kwlegar1", dt1);
                ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("s1", vs1),
                     new ReportParameter("s2", vs2),
                     new ReportParameter("s3", Tk_mula.Text),
                     new ReportParameter("s4", Tk_akhir.Text),
                     new ReportParameter("S5", imagePath),
                     new ReportParameter("S6", vs5),
                     new ReportParameter("S7", vs6)
                          };

                Rptviwerlejar.LocalReport.SetParameters(rptParams);
                Rptviwerlejar.LocalReport.DataSources.Add(rds);
                //Rptviwerlejar.LocalReport.DataSources.Add(rds1);
                Rptviwerlejar.LocalReport.DisplayName = "JURNAL_" + DateTime.Now.ToString("yyyyMMdd");
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


    protected void ExportToEXCEL(object sender, EventArgs e)
    {
        get_values();
        sqry1 = "select c.*,b.*,a.* from (select project_kod,GL_type,Format(GL_post_dt, 'dd/MM/yyyy') post_dt,GL_post_dt, [kod_akaun], [GL_journal_no],substring(ISNULL(REPLACE(REPLACE(GL_desc1, CHAR(13), ' '), CHAR(10), ' '),''),1,10) GL_desc1, [GL_invois_no],sum( [KW_Debit_amt]) as deb_amt, sum([KW_kredit_amt]) kre_amt from KW_General_Ledger where GL_sts='L' GROUP BY project_kod,GL_type,[GL_post_dt], [kod_akaun], [GL_journal_no], [GL_desc1], [GL_invois_no]) as a "
                + "outer apply(select jur_desc from KW_ref_jurnal_type where jur_type_cd=GL_type) as b "
                + "outer apply(select Ref_Projek_name from KW_Ref_Projek where Ref_Projek_code=project_kod) as c "
                + "where GL_post_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and GL_post_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) " + ss1 + ""
                + "order by project_kod,GL_type";

        DataTable dt = new DataTable();
        dt = DBCon.Ora_Execute_table(sqry1);


        if (dt.Rows.Count != 0)
        {


            StringBuilder builder = new StringBuilder();
            string strFileName = string.Format("{0}.{1}", "JURNAL_TRANSAKSI_" + DateTime.Now.ToString("ddMMyyyy") + "", "csv");
            builder.Append("Nama Projek ,Jenis Jurnal,Tarikh, No Akaun, Jurnal no, Keterangan, No Invois, Debit(RM),Kredit(RM)" + Environment.NewLine);
            for (int k = 0; k <= (dt.Rows.Count - 1); k++)
            {
                
                    builder.Append(dt.Rows[k]["Ref_Projek_name"].ToString() + " , " + dt.Rows[k]["jur_desc"].ToString() + "," + dt.Rows[k]["post_dt"].ToString() + "," + dt.Rows[k]["kod_akaun"].ToString() + "," + dt.Rows[k]["GL_journal_no"].ToString() + "," + dt.Rows[k]["GL_desc1"].ToString() + "," + dt.Rows[k]["GL_invois_no"].ToString() + "," + dt.Rows[k]["deb_amt"].ToString() + "," + dt.Rows[k]["kre_amt"].ToString()  + Environment.NewLine);
                
            }
            Response.Clear();
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + strFileName);
            Response.Write(builder.ToString());
            Response.End();

        }
        else
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod tidak dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }

        string script = " $(function () { $('.select2').select2() });";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
    }

    protected void btn_reset(object sender, EventArgs e)
    {
        Response.Redirect("../kewengan/kw_lep_jurnal.aspx");
    }


}