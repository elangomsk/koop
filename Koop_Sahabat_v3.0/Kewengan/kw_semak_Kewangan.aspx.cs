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
using System.Threading;

public partial class kw_semak_Kewangan : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    DBConnection DBCon = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection Dbcon = new DBConnection();
    string qry1 = string.Empty, qry2 = string.Empty, tbl_name1 = string.Empty, col_name1 = string.Empty;
    string str_sdt1 = string.Empty, end_edt1 = string.Empty;
    string userid, level;
    int sum = 0;
    float total = 0;
    float total1 = 0;
    float total2 = 0;
    string uniqueId,Status;
    string jurnal_qry = string.Empty;
    string CommandArgument1 = string.Empty, CommandArgument2 = string.Empty, CommandArgument3 = string.Empty, CommandArgument4 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);

        scriptManager.RegisterPostBackControl(this.Button18);
        scriptManager.RegisterPostBackControl(this.Button22);
        scriptManager.RegisterPostBackControl(this.Button21);
        scriptManager.RegisterPostBackControl(this.btnprintmoh);

        string get_edt = string.Empty;
        DataTable sel_gst2 = new DataTable();
        sel_gst2 = Dbcon.Ora_Execute_table("select top(1) Format(tarikh_mula,'dd/MM/yyyy') as st_dt,Format(tarikh_akhir,'dd/MM/yyyy') as end_dt from kw_profile_syarikat where cur_sts='1' order by tarikh_akhir desc");
        if (sel_gst2.Rows.Count != 0)
        {
            get_edt = sel_gst2.Rows[0]["st_dt"].ToString();

            string fdate = get_edt;
            DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime fd1 = DateTime.ParseExact(sel_gst2.Rows[0]["end_dt"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string s1_dt = "" + fd.ToString("yyyy") + ", " + (double.Parse(fd.ToString("MM")) - 1) + ", " + fd.ToString("dd") + "";
            string s2_dt = "" + fd1.ToString("yyyy") + ", " + (double.Parse(fd1.ToString("MM")) - 1) + ", " + fd1.ToString("dd") + "";
            start_dt1.Text = s1_dt;
            end_dt1.Text = s2_dt;
            str_sdt1 = fd.ToString("yyyy-MM-dd");
            end_edt1 = fd1.ToString("yyyy-MM-dd");
            string script = "  $().ready(function () {  var today = new Date();   var preYear = today.getFullYear() - 1; var curYear = today.getFullYear() - 0; $('.datepicker2').datepicker({ format: 'dd/mm/yyyy',autoclose: true,inline: true,startDate: new Date(" + s1_dt + "),endDate: new Date(" + s2_dt + ")}).on('changeDate', function(ev) {(ev.viewMode == 'days') ? $(this).datepicker('hide') : '';}); $('.select2').select2(); $(" + pp6.ClientID + ").click(function() { $(" + hd_txt.ClientID + ").text('MOHON BAYAR'); }); $(" + pp4.ClientID + ").click(function() { $(" + hd_txt.ClientID + ").text('INVOIS / BIL');});$(" + pp1.ClientID + ").click(function() {$(" + hd_txt.ClientID + ").text('PAYMENT VOUCHER');});$(" + pp2.ClientID + ").click(function() {$(" + hd_txt.ClientID + ").text('NOTA KREDIT');});$(" + pp3.ClientID + ").click(function() {$(" + hd_txt.ClientID + ").text('NOTA DEBIT');});});";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        }
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                Session["bajt_det_cd"] = "";

                userid = Session["New"].ToString();
                txtid.Text = userid;
                txtid.Attributes.Add("disabled", "disabled");
                bind_doc_kod();
                negeri();
                negera();
                bind_kod_industry();
                jenis_cara();
                loadvis();
                invois_view();
                SetInitialRowBil();
                SetInitialRowMohBil();
                SetInitialRowmoh();
                SetInitialRowCre();
                SetInitialRowDeb();
                SetInitialRow_payment_vocher();
                akaun();
                Pembakal();
                cara();
                Bank();
                noper_pv();
                Bindbaucer();
                Bayaran();
                //pelanggan();
                BindInvois();
                project();
                BindMohon();
                Bindcredit();
                Binddedit();
                invbil.Visible = false;
                hd_txt.Text = "MOHON BAYAR";
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    void bind_doc_kod()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select Ref_doc_code,Ref_doc_name from KW_Ref_Doc_kod where Status = 'A' and Ref_doc_code IN ('01','03','05','06','09','10','11','12','13','14','15','16','18','19','20','21')";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            jenis_trxn.DataSource = dt;
            jenis_trxn.DataTextField = "Ref_doc_name";
            jenis_trxn.DataValueField = "Ref_doc_code";
            jenis_trxn.DataBind();
            jenis_trxn.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void negeri()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select DISTINCT hr_negeri_code,UPPER(hr_negeri_desc) as hr_negeri_desc from ref_hr_negeri";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            
            pel_ddnegeri.DataSource = dt;
            pel_ddnegeri.DataBind();
            pel_ddnegeri.DataTextField = "hr_negeri_desc";
            pel_ddnegeri.DataValueField = "hr_negeri_code";
            pel_ddnegeri.DataBind();
            pel_ddnegeri.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void negera()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select * from Country";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            pel_dd_negera.DataSource = dt;
            pel_dd_negera.DataBind();
            pel_dd_negera.DataTextField = "CountryName";
            pel_dd_negera.DataValueField = "ID";
            pel_dd_negera.DataBind();
            pel_dd_negera.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void bind_kod_industry()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select kod_industry,(kod_industry +' | ' + msic_desc) as name from Kw_kod_industry left join KW_Ref_Kod_Industri on msic_kod=kod_industry where kod_industry != ''";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            pel_dd_kodind.DataSource = dt;
            pel_dd_kodind.DataTextField = "name";
            pel_dd_kodind.DataValueField = "kod_industry";
            pel_dd_kodind.DataBind();
            pel_dd_kodind.Items.Insert(0, new ListItem("--- PILIH ---", ""));

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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('1776','705','1748','764','774','1777','776','767','768','1749','777','520','779','14','39','1778','324','1553','781','484','16','52','1037','1779','88','1486','1755', '61', '1756', '1757', '1780', '53', '35', '1781', '1553', '763', '88', '324', '711', '1765', '520', '770', '1782', '1779', '1755', '1757', '1783', '1784', '1765', '781', '1404','1785', '1786', '1779', '35', '1756', '1757', '1769', '1338', '1545', '769', '770', '1753', '1771', '169', '1772', '1748', '1755', '1773', '1774')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[50][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[21][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[50][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[53][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[18][0].ToString().ToLower()); 
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[20][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[46][0].ToString().ToLower());
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
            ps_lbl10.Text = txtinfo.ToTitleCase(gt_lng.Rows[23][0].ToString().ToLower());         
            ps_lbl11.Text = txtinfo.ToTitleCase(gt_lng.Rows[29][0].ToString().ToLower());
            //ps_lbl12.Text = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower());
            ps_lbl13.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            //ps_lbl14.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            but.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl17.Text = txtinfo.ToTitleCase(gt_lng.Rows[20][0].ToString().ToLower());
            ps_lbl18.Text = txtinfo.ToTitleCase(gt_lng.Rows[51][0].ToString().ToLower());
            ps_lbl19.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            ps_lbl20.Text = txtinfo.ToTitleCase(gt_lng.Rows[49][0].ToString().ToLower());               
            ps_lbl21.Text = txtinfo.ToTitleCase(gt_lng.Rows[25][0].ToString().ToLower());
            ps_lbl22.Text = txtinfo.ToTitleCase(gt_lng.Rows[17][0].ToString().ToLower());
            btncarian.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl24.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl25.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            ps_lbl26.Text = txtinfo.ToTitleCase(gt_lng.Rows[26][0].ToString().ToLower());
            ps_lbl27.Text = txtinfo.ToTitleCase(gt_lng.Rows[38][0].ToString().ToLower());
            ps_lbl28.Text = txtinfo.ToTitleCase(gt_lng.Rows[16][0].ToString().ToLower());
            ps_lbl29.Text = txtinfo.ToTitleCase(gt_lng.Rows[28][0].ToString().ToLower());
            ps_lbl30.Text = txtinfo.ToTitleCase(gt_lng.Rows[28][0].ToString().ToLower());            
            ps_lbl31.Text = txtinfo.ToTitleCase(gt_lng.Rows[31][0].ToString().ToLower());
            Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            btnprintmoh.Text = txtinfo.ToTitleCase(gt_lng.Rows[32][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[33][0].ToString().ToLower());
            //ps_lbl35.Text = txtinfo.ToTitleCase(gt_lng.Rows[28][0].ToString().ToLower());
            ps_lbl36.Text = txtinfo.ToTitleCase(gt_lng.Rows[39][0].ToString().ToLower());
            //ps_lbl37.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            btnkem.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            ps_lbl39.Text = txtinfo.ToTitleCase(gt_lng.Rows[29][0].ToString().ToLower());
            ps_lbl40.Text = txtinfo.ToTitleCase(gt_lng.Rows[40][0].ToString().ToLower());          
            ps_lbl41.Text = txtinfo.ToTitleCase(gt_lng.Rows[49][0].ToString().ToLower());
            Button6.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl44.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            ps_lbl45.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            ps_lbl46.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            ps_lbl47.Text = txtinfo.ToTitleCase(gt_lng.Rows[22][0].ToString().ToLower());
            ps_lbl48.Text = txtinfo.ToTitleCase(gt_lng.Rows[34][0].ToString().ToLower());
            ps_lbl49.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            ps_lbl50.Text = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());         
            ps_lbl51.Text = txtinfo.ToTitleCase(gt_lng.Rows[42][0].ToString().ToLower());
            ps_lbl52.Text = txtinfo.ToTitleCase(gt_lng.Rows[38][0].ToString().ToLower());
            ps_lbl53.Text = txtinfo.ToTitleCase(gt_lng.Rows[31][0].ToString().ToLower());
            Button8.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            Button11.Text = txtinfo.ToTitleCase(gt_lng.Rows[33][0].ToString().ToLower());
            ps_lbl56.Text = txtinfo.ToTitleCase(gt_lng.Rows[29][0].ToString().ToLower());
            ps_lbl57.Text = txtinfo.ToTitleCase(gt_lng.Rows[43][0].ToString().ToLower());
            ps_lbl58.Text = txtinfo.ToTitleCase(gt_lng.Rows[41][0].ToString().ToLower());
            buttab2.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            tab2tam.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());                
            ps_lbl61.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            ps_lbl62.Text = txtinfo.ToTitleCase(gt_lng.Rows[16][0].ToString().ToLower());
            ps_lbl63.Text = txtinfo.ToTitleCase(gt_lng.Rows[22][0].ToString().ToLower());
            ps_lbl64.Text = txtinfo.ToTitleCase(gt_lng.Rows[34][0].ToString().ToLower());
            ps_lbl65.Text = txtinfo.ToTitleCase(gt_lng.Rows[43][0].ToString().ToLower());
            ps_lbl66.Text = txtinfo.ToTitleCase(gt_lng.Rows[42][0].ToString().ToLower());
            ps_lbl67.Text = txtinfo.ToTitleCase(gt_lng.Rows[41][0].ToString().ToLower());
            ps_lbl68.Text = txtinfo.ToTitleCase(gt_lng.Rows[25][0].ToString().ToLower());
            ps_lbl69.Text = txtinfo.ToTitleCase(gt_lng.Rows[27][0].ToString().ToLower());
            ps_lbl70.Text = txtinfo.ToTitleCase(gt_lng.Rows[47][0].ToString().ToLower());            
            ps_lbl71.Text = txtinfo.ToTitleCase(gt_lng.Rows[52][0].ToString().ToLower());
            ps_lbl72.Text = txtinfo.ToTitleCase(gt_lng.Rows[38][0].ToString().ToLower());
            ps_lbl73.Text = txtinfo.ToTitleCase(gt_lng.Rows[28][0].ToString().ToLower());
            Button7.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            Button17.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            Button18.Text = txtinfo.ToTitleCase(gt_lng.Rows[32][0].ToString().ToLower());
            Button9.Text = txtinfo.ToTitleCase(gt_lng.Rows[33][0].ToString().ToLower());
            ps_lbl78.Text = txtinfo.ToTitleCase(gt_lng.Rows[29][0].ToString().ToLower());
            ps_lbl79.Text = txtinfo.ToTitleCase(gt_lng.Rows[35][0].ToString().ToLower());
            ps_lbl80.Text = txtinfo.ToTitleCase(gt_lng.Rows[24][0].ToString().ToLower());  
            ps_lbl81.Text = txtinfo.ToTitleCase(gt_lng.Rows[48][0].ToString().ToLower());
            Button12.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            tab3tam.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl84.Text = txtinfo.ToTitleCase(gt_lng.Rows[48][0].ToString().ToLower());
            ps_lbl85.Text = txtinfo.ToTitleCase(gt_lng.Rows[19][0].ToString().ToLower());
            ps_lbl86.Text = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());       
            ps_lbl87.Text = txtinfo.ToTitleCase(gt_lng.Rows[30][0].ToString().ToLower());
            ps_lbl88.Text = txtinfo.ToTitleCase(gt_lng.Rows[44][0].ToString().ToLower());
            ps_lbl89.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());          
            ps_lbl90.Text = txtinfo.ToTitleCase(gt_lng.Rows[45][0].ToString().ToLower());
            ps_lbl91.Text = txtinfo.ToTitleCase(gt_lng.Rows[53][0].ToString().ToLower());
            ps_lbl92.Text = txtinfo.ToTitleCase(gt_lng.Rows[31][0].ToString().ToLower());
            Button15.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            Button22.Text = txtinfo.ToTitleCase(gt_lng.Rows[32][0].ToString().ToLower());
            Button16.Text = txtinfo.ToTitleCase(gt_lng.Rows[33][0].ToString().ToLower());
            ps_lbl96.Text = txtinfo.ToTitleCase(gt_lng.Rows[29][0].ToString().ToLower());
            ps_lbl97.Text = txtinfo.ToTitleCase(gt_lng.Rows[36][0].ToString().ToLower());
            ps_lbl98.Text = txtinfo.ToTitleCase(gt_lng.Rows[24][0].ToString().ToLower());
            ps_lbl99.Text = txtinfo.ToTitleCase(gt_lng.Rows[48][0].ToString().ToLower());
            Button13.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            Button10.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());      
            ps_lbl102.Text = txtinfo.ToTitleCase(gt_lng.Rows[48][0].ToString().ToLower());
            ps_lbl103.Text = txtinfo.ToTitleCase(gt_lng.Rows[19][0].ToString().ToLower());
            ps_lbl104.Text = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
            ps_lbl105.Text = txtinfo.ToTitleCase(gt_lng.Rows[30][0].ToString().ToLower());
            ps_lbl106.Text = txtinfo.ToTitleCase(gt_lng.Rows[37][0].ToString().ToLower());
            ps_lbl107.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl108.Text = txtinfo.ToTitleCase(gt_lng.Rows[45][0].ToString().ToLower());
            ps_lbl109.Text = txtinfo.ToTitleCase(gt_lng.Rows[53][0].ToString().ToLower());
            ps_lbl110.Text = txtinfo.ToTitleCase(gt_lng.Rows[31][0].ToString().ToLower());             
            Button5.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            Button21.Text = txtinfo.ToTitleCase(gt_lng.Rows[32][0].ToString().ToLower());
            Button14.Text = txtinfo.ToTitleCase(gt_lng.Rows[33][0].ToString().ToLower());
           
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    protected void pel_Button5_Click(object sender, EventArgs e)
    {
        pel_clr_txt();
        ModalPopupExtender1.Show();
    }

    void pel_clr_txt()
    {
        pel_TextBox1.Text = "";
        pel_TextBox5.Text = "";
        pel_TextBox7.Text = "";
        pel_TextBox4.Text = "";
        pel_TextBox11.Text = "";
        pel_TextBox2.Text = "";
        pel_TextBox9.Text = "";
        pel_TextBox6.Text = "";
        pel_ddnegeri.SelectedValue = "";
        pel_TextBox8.Text = "";
        pel_TextBox14.Text = "";
        pel_dd_kodind.SelectedValue = "";
        pel_dd_negera.SelectedValue = "";
        pel_TextBox3.Text = "";
       
    }
    protected void pel_Click_bck(object sender, EventArgs e)
    {
        pel_clr_txt();
    }
    protected void pel_clk_submit(object sender, EventArgs e)
    {
        if (pel_TextBox1.Text != "" && pel_TextBox5.Text != "")
        {
            get_dd_value();
            string set_cnt = string.Empty, set_cnt1 = string.Empty, mcnt = string.Empty;
            string sno1 = string.Empty, sno2 = string.Empty, sno3 = string.Empty, cnt_value = string.Empty;
            DataTable cnt_no = new DataTable();
            //if (dd_akaun.SelectedValue != "")
            //{

            //    //cnt_no = DBCon.Ora_Execute_table("select Id as cnt,(jenis_akaun_type + 1) as rcnt,kod_akaun,under_jenis,jenis_akaun_type From KW_Ref_Carta_Akaun where kat_akaun='" + kat_akaun.SelectedValue + "' and kod_akaun='" + get_cd.Text + "'");
            //    cnt_no = DBCon.Ora_Execute_table("select Id as cnt,(jenis_akaun_type + 1) as rcnt,kod_akaun,under_jenis,jenis_akaun_type From KW_Ref_Carta_Akaun where kod_akaun='" + dd_akaun.SelectedValue + "'");

            //    set_cnt1 = cnt_no.Rows[0]["cnt"].ToString();
            //    mcnt = cnt_no.Rows[0]["rcnt"].ToString();
            //    set_cnt = dd_akaun.SelectedValue;
            //    //set_cnt1 = "1";
            //}
            //else
            //{
                //set_cnt1 = "0";
                //mcnt = "1";
                //set_cnt = "00";
            //}


            //DataTable cnt_no1 = new DataTable();
            //cnt_no1 = DBCon.Ora_Execute_table("select top(1) (ISNULL(kod_akaun_cnt,'') +1) as mcnt From KW_Ref_Carta_Akaun where jenis_akaun='" + dd_akaun.SelectedValue + "' order by kod_akaun desc");

            //if (cnt_no1.Rows.Count != 0)
            //{
            //    cnt_value = cnt_no1.Rows[0]["mcnt"].ToString();
            //}
            //else
            //{
                //cnt_value = "1";
            //}
            //string chk_role = string.Empty;
            //string[] sp_no = set_cnt.Split('.');
            //int sp_no1 = sp_no.Length;
            //string ss1 = string.Empty;
            //for (int i = 0; i <= sp_no1; i++)
            //{
            //    if (i == (Int32.Parse(mcnt) - 1))
            //    {
            //        sno1 = cnt_value.PadLeft(2, '0');
            //    }
            //    else
            //    {
            //        sno1 = sp_no[i].ToString();
            //    }
            //    if (i < (sp_no1))
            //    {
            //        ss1 = ".";
            //    }
            //    else
            //    {
            //        ss1 = "";
            //    }
            //    chk_role += (sno1).PadLeft(2, '0') + "" + ss1;
            //}

            //string sso1 = string.Empty, sso2 = string.Empty, sso3 = string.Empty;
            //if (set_cnt1 == "0")
            //{
            //    sso1 = "";
            //}
            //else
            //{
            //    if (cnt_no.Rows[0]["jenis_akaun_type"].ToString() == "1")
            //    {
            //        sso1 = chk_role + "," + cnt_no.Rows[0]["kod_akaun"].ToString();
            //    }
            //    else
            //    {
            //        sso1 = chk_role + "," + cnt_no.Rows[0]["under_jenis"].ToString();
            //    }
            //}
            if (pel_ver_id.Text == "0")
            {
                DataTable ddokdicno = new DataTable();
                ddokdicno = DBCon.Ora_Execute_table("select * From "+ tbl_name1 +" where Ref_nama_syarikat='" + pel_TextBox1.Text + "' and Ref_no_syarikat='" + pel_TextBox5.Text + "'");
                if (ddokdicno.Rows.Count == 0)
                {
                    //DataTable get_val = new DataTable();
                    //get_val = DBCon.Ora_Execute_table("select * From KW_Ref_Carta_Akaun where kod_akaun='" + dd_akaun.SelectedValue + "'");
                    //string Inssql1 = "insert into KW_Ref_Carta_Akaun(kat_akaun,nama_akaun,kod_akaun,jenis_akaun,under_parent,KW_Debit_amt,KW_kredit_amt,Kw_open_amt,jenis_akaun_type,under_jenis,Status,crt_id,cr_dt,Susu_nilai,kod_akaun_cnt,sts_kawalan,ct_kod_industry) values ('" + get_val.Rows[0]["kat_akaun"].ToString() + "','" + TextBox1.Text.Replace("'", "''") + "','" + chk_role + "','" + dd_akaun.SelectedValue + "','" + set_cnt1 + "','0.00','0.00','0.00','" + mcnt + "','','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','0','" + cnt_value + "','Y','" + dd_kodind.SelectedValue + "')";
                    //Status = DBCon.Ora_Execute_CommamdText(Inssql1);
                    //if (Status == "SUCCESS")
                    //{
                        string Inssql = "insert into " + tbl_name1 + " (Ref_nama_syarikat,Ref_no_syarikat,Ref_no_telefone,Ref_no_fax,Ref_email,Ref_gst_id,Ref_kod_akaun,Ref_jenis_akaun,Ref_alamat,Status,crt_id,cr_dt,Ref_kawalan,ref_alamat_ked," + col_name1 + "_bandar," + col_name1 + "_negeri," + col_name1 + "_poskod," + col_name1 + "_negera," + col_name1 + "_kod_industry) values ('" + pel_TextBox1.Text.Replace("'", "''") + "','" + pel_TextBox5.Text.Replace("'", "''") + "','" + pel_TextBox4.Text.Replace("'", "''") + "','" + pel_TextBox2.Text.Replace("'", "''") + "','" + pel_TextBox3.Text.Replace("'", "''") + "','" + pel_TextBox6.Text.Replace("'", "''") + "','','','" + pel_TextBox7.Text.Replace("'", "''") + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + pel_TextBox8.Text.Replace("'", "''") + "','" + pel_TextBox11.Text.Replace("'", "''") + "','" + pel_TextBox9.Text.Replace("'", "''") + "','" + pel_ddnegeri.SelectedValue + "','" + pel_TextBox14.Text.Replace("'", "''") + "','" + pel_dd_negera.SelectedValue + "','" + pel_dd_kodind.SelectedValue + "')";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql);
                    if (Status == "SUCCESS")
                    {
                        pel_clr_txt();
                        shw_grd_info();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('"+ Label16.Text +" Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success'});", true);
                    }
                    //Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                    //    Session["validate_success"] = "SUCCESS";
                    //    Response.Redirect("../kewengan/kw_pelanggan_view.aspx");
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Not Insert.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    //}
                }
                else
                {
                    ModalPopupExtender1.Show();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Telah Wujud.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else
            {
                DataTable ddokdicno = new DataTable();
                ddokdicno = DBCon.Ora_Execute_table("select * From KW_Ref_Pelanggan where Id = '" + pel_get_id.Text + "'");
                //if (ddokdicno.Rows[0]["Ref_jenis_akaun"].ToString() != dd_akaun.SelectedValue)
                if(ddokdicno.Rows.Count != 0)
                {
                //    DataTable get_val1 = new DataTable();
                //    get_val1 = DBCon.Ora_Execute_table("select * From KW_Ref_Carta_Akaun where kod_akaun='" + ddokdicno.Rows[0]["Ref_kod_akaun"].ToString() + "'");
                //    if (get_val1.Rows.Count != 0)
                //    {
                //        string Inssql2 = "delete from KW_Ref_Carta_Akaun where kod_akaun = '" + ddokdicno.Rows[0]["Ref_kod_akaun"].ToString() + "'";
                //        Status_del = DBCon.Ora_Execute_CommamdText(Inssql2);
                //        if (Status_del == "SUCCESS")
                //        {
                //            DataTable get_val = new DataTable();
                //            get_val = DBCon.Ora_Execute_table("select * From KW_Ref_Carta_Akaun where kod_akaun='" + dd_akaun.SelectedValue + "'");
                //            string Inssql1 = "insert into KW_Ref_Carta_Akaun(kat_akaun,nama_akaun,kod_akaun,jenis_akaun,under_parent,KW_Debit_amt,KW_kredit_amt,Kw_open_amt,jenis_akaun_type,under_jenis,Status,crt_id,cr_dt,Susu_nilai,kod_akaun_cnt,sts_kawalan,ct_kod_industry) values ('" + get_val.Rows[0]["kat_akaun"].ToString() + "','" + TextBox1.Text.Replace("'", "''") + "','" + chk_role + "','" + dd_akaun.SelectedValue + "','" + set_cnt1 + "','0.00','0.00','0.00','" + mcnt + "','','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','0','" + cnt_value + "','Y','" + dd_kodind.SelectedValue + "')";
                //            Status2 = DBCon.Ora_Execute_CommamdText(Inssql1);
                //        }
                //        else
                //        {
                //            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Dihapus Tidak Mungkin.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                //        }
                //    }

                //}
                //else
                //{
                //    string Inssql1 = "Update KW_Ref_Carta_Akaun set ct_kod_industry='" + dd_kodind.SelectedValue + "',nama_akaun='" + TextBox1.Text.Replace("'", "''") + "' where kod_akaun='" + ddokdicno.Rows[0]["Ref_kod_akaun"].ToString() + "'";
                //    Status2 = DBCon.Ora_Execute_CommamdText(Inssql1);
                //}

                //if (Status2 == "SUCCESS")
                //{
                //    string ckod = string.Empty;
                //    if (ddokdicno.Rows[0]["Ref_jenis_akaun"].ToString() != dd_akaun.SelectedValue)
                //    {
                //        ckod = "Ref_kod_akaun='" + chk_role + "',";
                //    }
                //    else
                //    {
                //        ckod = "";
                //    }

                    string Inssql = "UPDATE KW_Ref_Pelanggan set Ref_nama_syarikat='" + pel_TextBox1.Text.Replace("'", "''") + "',Ref_no_syarikat='" + pel_TextBox5.Text.Replace("'", "''") + "',Ref_no_telefone='" + pel_TextBox4.Text.Replace("'", "''") + "',Ref_no_fax='" + pel_TextBox2.Text.Replace("'", "''") + "',Ref_email='" + pel_TextBox3.Text.Replace("'", "''") + "',Ref_gst_id='" + pel_TextBox6.Text.Replace("'", "''") + "',Ref_alamat='" + pel_TextBox7.Text.Replace("'", "''") + "',Ref_kawalan='" + pel_TextBox8.Text.Replace("'", "''") + "',ref_alamat_ked='" + pel_TextBox11.Text.Replace("'", "''") + "',"+ col_name1 +"_bandar='" + pel_TextBox9.Text.Replace("'", "''") + "'," + col_name1 + "_negeri='" + pel_ddnegeri.SelectedValue + "'," + col_name1 + "_poskod='" + pel_TextBox14.Text.Replace("'", "''") + "'," + col_name1 + "_negera='" + pel_dd_negera.SelectedValue + "',upd_id='" + Session["New"].ToString() + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Id = '" + pel_get_id.Text + "'";
                    Status = DBCon.Ora_Execute_CommamdText(Inssql);
                    if (Status == "SUCCESS")
                    {
                        pel_clr_txt();
                        shw_grd_info();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success'});", true);
                    }
                }
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Telah Wujud.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                //}
            }

        }
        else
        {
            ModalPopupExtender1.Show();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }

    }
    void invois_view()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = " select no_invois from KW_Pembayaran_invoisBil_item group by no_invois";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);

            ddinv.DataSource = dt;
            ddinv.DataTextField = "no_invois";
            ddinv.DataValueField = "no_invois";
            ddinv.DataBind();
            ddinv.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            ddinv2.DataSource = dt;
            ddinv2.DataTextField = "no_invois";
            ddinv2.DataValueField = "no_invois";
            ddinv2.DataBind();
            ddinv2.Items.Insert(0, new ListItem("--- PILIH ---", ""));


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    void jenis_cara()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = " select Jenis_bayaran_cd,Jenis_bayaran from KW_Jenis_Cara_bayaran where Status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList2.DataSource = dt;
            DropDownList2.DataTextField = "Jenis_bayaran";
            DropDownList2.DataValueField = "Jenis_bayaran_cd";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    void loadvis()
    {
        Div2.Visible = false;
        Div5.Visible = false;
        fr2.Visible = false;
        th2.Visible = false;
        //pp12.Visible = false;

        Div13.Visible = false;
        Div12.Visible = true;
        btnprintmoh.Visible = false;
        Button17.Visible = false;
        TextBox17.Attributes.Add("disabled", "disabled");
        TextBox18.Attributes.Add("disabled", "disabled");
        TextBox1.Attributes.Add("disabled", "disabled");
        TextBox37.Attributes.Add("disabled", "disabled");
        txtname.Attributes.Add("disabled", "disabled");
        txtbname.Attributes.Add("disabled", "disabled");
        txtbno.Attributes.Add("disabled", "disabled");
        txtdata.Attributes.Add("disabled", "disabled");
        txtpvno.Attributes.Add("disabled", "disabled");

        userid = Session["New"].ToString();
        level = Session["level"].ToString();
        Button22.Visible = false;
        if (level == "6")
        {
            txtid.Text = userid;
            txtid.Attributes.Add("disabled", "disabled");
            sts.Visible = false;
            Div10.Visible = false;
            ddpvstatus.Visible = true;
            tab2tam.Attributes.Remove("disabled");
            ddpvstatus.SelectedIndex = 0;
            ddpvstatus.Attributes.Add("disabled", "disabled");
        }
        else if (level == "5")
        {
            txtApr.Text = userid;
            txtApr.Attributes.Add("disabled", "disabled");
            sts.Visible = true;
            ddpvstatus1.Visible = true;
            tab2tam.Attributes.Add("disabled", "disabled");

            Div10.Visible = true;
            ddpvstatus.Attributes.Remove("disabled");
        }
    }


    private void SetInitialRowBil()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;

        dt.Columns.Add(new DataColumn("Column1", typeof(string)));
        dt.Columns.Add(new DataColumn("Column2", typeof(string)));
        dt.Columns.Add(new DataColumn("Column3", typeof(string)));
        dt.Columns.Add(new DataColumn("Column4", typeof(string)));
        dt.Columns.Add(new DataColumn("Column5", typeof(string)));
        dt.Columns.Add(new DataColumn("Column6", typeof(string)));
        dt.Columns.Add(new DataColumn("Column7", typeof(string)));
        dt.Columns.Add(new DataColumn("Column8", typeof(string)));
        dt.Columns.Add(new DataColumn("Column9", typeof(string)));
        dt.Columns.Add(new DataColumn("Column10", typeof(string)));
        dt.Columns.Add(new DataColumn("Column11", typeof(string)));
        dt.Columns.Add(new DataColumn("Column12", typeof(string)));
        dt.Columns.Add(new DataColumn("Column13", typeof(string)));
        dt.Columns.Add(new DataColumn("Column14", typeof(string)));
        dt.Columns.Add(new DataColumn("Column15", typeof(string)));

        dr = dt.NewRow();

        dr["Column1"] = string.Empty;
        dr["Column2"] = string.Empty;
        dr["Column3"] = string.Empty;
        dr["Column4"] = string.Empty;
        dr["Column5"] = string.Empty;
        dr["Column6"] = string.Empty;
        dr["Column7"] = string.Empty;
        dr["Column8"] = string.Empty;
        dr["Column9"] = string.Empty;
        dr["Column10"] = string.Empty;
        dr["Column11"] = string.Empty;
        dr["Column12"] = string.Empty;
        dr["Column13"] = string.Empty;
        dr["Column14"] = string.Empty;
        dr["Column15"] = string.Empty;


        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentTable5"] = dt;



        Gridview5.DataSource = dt;
        Gridview5.DataBind();
    }


    private void SetInitialRowMohBil()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;

        dt.Columns.Add(new DataColumn("Column1", typeof(string)));
        dt.Columns.Add(new DataColumn("Column2", typeof(string)));
        dt.Columns.Add(new DataColumn("Column3", typeof(string)));
        dt.Columns.Add(new DataColumn("Column4", typeof(string)));
        dt.Columns.Add(new DataColumn("Column5", typeof(string)));
        dt.Columns.Add(new DataColumn("Column6", typeof(string)));
        dt.Columns.Add(new DataColumn("Column7", typeof(string)));
        dt.Columns.Add(new DataColumn("Column8", typeof(string)));
        dt.Columns.Add(new DataColumn("Column9", typeof(string)));
        dt.Columns.Add(new DataColumn("Column10", typeof(string)));
        dt.Columns.Add(new DataColumn("Column11", typeof(string)));
        dt.Columns.Add(new DataColumn("Column12", typeof(string)));
        dt.Columns.Add(new DataColumn("Column13", typeof(string)));
        dt.Columns.Add(new DataColumn("Column14", typeof(string)));


        dr = dt.NewRow();

        dr["Column1"] = string.Empty;
        dr["Column2"] = string.Empty;
        dr["Column3"] = string.Empty;
        dr["Column4"] = string.Empty;
        dr["Column5"] = string.Empty;
        dr["Column6"] = string.Empty;
        dr["Column7"] = string.Empty;
        dr["Column8"] = string.Empty;
        dr["Column9"] = string.Empty;
        dr["Column10"] = string.Empty;
        dr["Column11"] = string.Empty;
        dr["Column12"] = string.Empty;
        dr["Column13"] = string.Empty;
        dr["Column14"] = string.Empty;


        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentTable6"] = dt;



        grdbilinv.DataSource = dt;
        grdbilinv.DataBind();
    }

    protected void Mohon_grvStudentDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SetRowData();
        if (ViewState["CurrentTable2"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable2"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (dt.Rows.Count > 1)
            {
                dt.Rows.Remove(dt.Rows[rowIndex]);
                drCurrentRow = dt.NewRow();
                ViewState["CurrentTable2"] = dt;
                grdmohon.DataSource = dt;
                grdmohon.DataBind();

                for (int i = 0; i < grdmohon.Rows.Count - 1; i++)
                {
                    grdmohon.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                }
                SetPreviousData1();
                Mohon_GrandTotalBil();
            }
        }
    }

    private void SetRowData()
    {
        int rowIndex = 0;
        decimal total1 = 0;
        decimal total2 = 0;
        decimal total3 = 0;
        decimal total4 = 0;
        if (ViewState["CurrentTable2"] != null)
        {
            DataTable dtCurrentTable2 = (DataTable)ViewState["CurrentTable2"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable2.Rows.Count > 0)
            {
             
                for (int i = 1; i <= dtCurrentTable2.Rows.Count; i++)
                {
                    //extract the TextBox values

                    DropDownList box1 = (DropDownList)grdmohon.Rows[rowIndex].Cells[1].FindControl("TextBox11");
                    
                    TextBox box3 = (TextBox)grdmohon.Rows[rowIndex].Cells[2].FindControl("TextBox13");
                    DropDownList box1_2 = (DropDownList)grdmohon.Rows[rowIndex].Cells[3].FindControl("dd_projek");
                    DropDownList box1_1 = (DropDownList)grdmohon.Rows[rowIndex].Cells[4].FindControl("dd_bank");
                    TextBox box2 = (TextBox)grdmohon.Rows[rowIndex].Cells[5].FindControl("TextBox12");
                    //TextBox box4 = (TextBox)grdmohon.Rows[rowIndex].Cells[4].FindControl("TextBox14");
                    //TextBox box5 = (TextBox)grdmohon.Rows[rowIndex].Cells[5].FindControl("TextBox15");
                    //TextBox box6 = (TextBox)grdmohon.Rows[rowIndex].Cells[5].FindControl("TextBox16");

                    TextBox box7 = (TextBox)grdmohon.Rows[rowIndex].Cells[6].FindControl("TextBox53");
                    TextBox box8 = (TextBox)grdmohon.Rows[rowIndex].Cells[7].FindControl("TextBox54");
                    TextBox box9 = (TextBox)grdmohon.Rows[rowIndex].Cells[8].FindControl("Txtdis");
                    Label box10 = (Label)grdmohon.Rows[rowIndex].Cells[9].FindControl("Label1");
                    CheckBox chk = (CheckBox)grdmohon.Rows[rowIndex].Cells[10].FindControl("CheckBox1");
                    DropDownList box11 = (DropDownList)grdmohon.Rows[rowIndex].Cells[11].FindControl("ddcukaioth");
                    Label box12 = (Label)grdmohon.Rows[rowIndex].Cells[12].FindControl("Label10");
                    DropDownList box13 = (DropDownList)grdmohon.Rows[rowIndex].Cells[13].FindControl("ddcukaiinv");
                    Label box14 = (Label)grdmohon.Rows[rowIndex].Cells[14].FindControl("Label8");
                    Label box15 = (Label)grdmohon.Rows[rowIndex].Cells[14].FindControl("Label5");

                    drCurrentRow = dtCurrentTable2.NewRow();                    
                    dtCurrentTable2.Rows[i - 1]["Column1"] = box1.SelectedValue;
                    dtCurrentTable2.Rows[i - 1]["Column2"] = box3.Text;
                    dtCurrentTable2.Rows[i - 1]["Column3"] = box1_2.SelectedValue;
                    dtCurrentTable2.Rows[i - 1]["Column4"] = box1_1.SelectedValue;
                    dtCurrentTable2.Rows[i - 1]["Column5"] = box2.Text;
                    //dtCurrentTable2.Rows[i - 1]["Column4"] = box4.Text;
                    //dtCurrentTable2.Rows[i - 1]["Column5"] = box5.Text;
                    //dtCurrentTable2.Rows[i - 1]["Column6"] = box6.Text;
                    dtCurrentTable2.Rows[i - 1]["Column6"] = box7.Text;
                    dtCurrentTable2.Rows[i - 1]["Column7"] = box8.Text;
                    dtCurrentTable2.Rows[i - 1]["Column8"] = box9.Text;
                    dtCurrentTable2.Rows[i - 1]["Column9"] = chk.Checked.ToString();
                    dtCurrentTable2.Rows[i - 1]["Column10"] = box10.Text;
                    dtCurrentTable2.Rows[i - 1]["Column11"] = box11.SelectedValue;
                    dtCurrentTable2.Rows[i - 1]["Column12"] = box12.Text;
                    dtCurrentTable2.Rows[i - 1]["Column13"] = box13.SelectedValue;
                    dtCurrentTable2.Rows[i - 1]["Column14"] = box14.Text;
                    dtCurrentTable2.Rows[i - 1]["Column15"] = box15.Text;

                    rowIndex++;
                    if (box10.Text != "")
                    {
                        decimal tamt1 = Convert.ToDecimal(box10.Text);
                        total1 += tamt1;
                    }

                    if (box12.Text != "")
                    {

                        decimal tamt2 = Convert.ToDecimal(box12.Text);
                        total2 += tamt2;

                    }
                    if (box14.Text != "")
                    {
                        decimal tamt3 = Convert.ToDecimal(box14.Text);
                        total3 += tamt3;
                    }
                    if (box15.Text != "")
                    {
                        decimal tamt4 = Convert.ToDecimal(box15.Text);
                        total4 += tamt4;
                    }

                }
                //dtCurrentTable2.Rows.Add(drCurrentRow);
                ViewState["CurrentTable2"] = dtCurrentTable2;

                //grdmohon.DataSource = dtCurrentTable2;
                //grdmohon.DataBind();
                //tab1();
                //grdmohon.FooterRow.Cells[2].Text = "JUMLAH (RM) :";
                //grdmohon.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                //((Label)grdmohon.FooterRow.Cells[7].FindControl("Label2")).Text = total1.ToString("C").Replace("RM", "").Replace("$", "");
                //((Label)grdmohon.FooterRow.Cells[9].FindControl("Label7")).Text = total2.ToString("C").Replace("RM", "").Replace("$", "");
                //((Label)grdmohon.FooterRow.Cells[11].FindControl("Label4")).Text = total3.ToString("C").Replace("RM", "").Replace("$", "");
                //((Label)grdmohon.FooterRow.Cells[11].FindControl("Label9")).Text = total3.ToString("C").Replace("RM", "").Replace("$", "");
                //((Label)grdmohon.FooterRow.Cells[12].FindControl("Label6")).Text = total4.ToString("C").Replace("RM", "").Replace("$", "");

                //if (dtCurrentTable2.Rows.Count == 2)
                //{
                //    grdmohon.FooterRow.Cells[1].Enabled = false;
                //}
                //else
                //{
                //    grdmohon.FooterRow.Cells[1].Enabled = true;
                //}

               
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        //SetPreviousData();
    }

    //protected void LinkButton1_Click(object sender, EventArgs e)
    //{
    //    LinkButton lb = (LinkButton)sender;
    //    GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
    //    int rowID = gvRow.RowIndex;
    //    if (ViewState["CurrentTable2"] != null)
    //    {
    //        DataTable dt = (DataTable)ViewState["CurrentTable2"];
    //        if (dt.Rows.Count > 1)
    //        {
    //            if (gvRow.RowIndex < dt.Rows.Count)
    //            {
    //                //Remove the Selected Row data
    //                dt.Rows.Remove(dt.Rows[rowID]);
    //            }
    //        }
    //        //Store the current data in ViewState for future reference
    //        ViewState["CurrentTable"] = dt;
    //        //Re bind the GridView for the updated data
    //        grdmohon.DataSource = dt;
    //        grdmohon.DataBind();
    //    }

    //    //Set Previous Data on Postbacks
    //    SetPreviousData1();
    //    Mohon_GrandTotalBil();
    //}
    private void SetInitialRowmoh()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;


        dt.Columns.Add(new DataColumn("Column1", typeof(string)));
        dt.Columns.Add(new DataColumn("Column2", typeof(string)));
        dt.Columns.Add(new DataColumn("Column3", typeof(string)));
        dt.Columns.Add(new DataColumn("Column4", typeof(string)));
        dt.Columns.Add(new DataColumn("Column5", typeof(string)));
        dt.Columns.Add(new DataColumn("Column6", typeof(string)));
        dt.Columns.Add(new DataColumn("Column7", typeof(string)));
        dt.Columns.Add(new DataColumn("Column8", typeof(string)));
        dt.Columns.Add(new DataColumn("Column9", typeof(string)));
        dt.Columns.Add(new DataColumn("Column10", typeof(string)));
        dt.Columns.Add(new DataColumn("Column11", typeof(string)));
        dt.Columns.Add(new DataColumn("Column12", typeof(string)));
        dt.Columns.Add(new DataColumn("Column13", typeof(string)));
        dt.Columns.Add(new DataColumn("Column14", typeof(string)));
        dt.Columns.Add(new DataColumn("Column15", typeof(string)));
        dr = dt.NewRow();

        dr["Column1"] = string.Empty;
        dr["Column2"] = string.Empty;
        dr["Column3"] = string.Empty;
        dr["Column4"] = string.Empty;
        dr["Column5"] = string.Empty;
        dr["Column6"] = string.Empty;
        dr["Column7"] = string.Empty;
        dr["Column8"] = string.Empty;
        dr["Column9"] = string.Empty;
        dr["Column10"] = string.Empty;
        dr["Column11"] = string.Empty;
        dr["Column12"] = string.Empty;
        dr["Column13"] = string.Empty;
        dr["Column14"] = string.Empty;
        dr["Column15"] = string.Empty;
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentTable2"] = dt;
        
        grdmohon.DataSource = dt;
        grdmohon.DataBind();
    }


    private void GrandTotalt1()
    {
        float GTotal = 0f;
        float ITotal = 0f;
        float Gst = 0f;
        for (int i = 0; i < grdmohon.Rows.Count; i++)
        {
            String total = (grdmohon.Rows[i].FindControl("TextBox16") as TextBox).Text;
            String total1 = (grdmohon.Rows[i].FindControl("TextBox14") as TextBox).Text;
            Label jum = (Label)grdmohon.FooterRow.FindControl("Label11");
            Label jum1 = (Label)grdmohon.FooterRow.FindControl("Label13");
            decimal totval = Convert.ToDecimal(total);

            GTotal += (float)Convert.ToDecimal(total1);
            ITotal += (float)Convert.ToDecimal(totval);
            jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");
            jum1.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
            TextBox17.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");

        }

    }
    private void GrandTotalt2()
    {
        float GTotal = 0f;
        float ITotal = 0f;
        float Gst = 0f;
        for (int i = 0; i < grdmohon.Rows.Count; i++)
        {
            String total = (grdmohon.Rows[i].FindControl("TextBox14") as TextBox).Text;
            String gst = (grdmohon.Rows[i].FindControl("TextBox15") as TextBox).Text;
            Label jum = (Label)grdmohon.FooterRow.FindControl("Label12");
            Label tot = (Label)grdmohon.FooterRow.FindControl("Label13");
            decimal Gstval = Convert.ToDecimal(gst);
            decimal Totval = Convert.ToDecimal(total);
            GTotal += (float)Convert.ToDecimal(Totval + Gstval);
            ITotal += (float)Convert.ToDecimal(Gstval);
            jum.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
            tot.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");
            TextBox17.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");

        }

    }





    private void GrandTotalBil2()
    {
        float GTotal = 0f;
        float ITotal = 0f;
        float Gst = 0f;
        for (int i = 0; i < Gridview5.Rows.Count; i++)
        {
            String total = (Gridview5.Rows[i].FindControl("Label1") as Label).Text;
            String totalI = (Gridview5.Rows[i].FindControl("Label5") as Label).Text;
            String gmt = (Gridview5.Rows[i].FindControl("Label10") as Label).Text;
            String tgst = (Gridview5.Rows[i].FindControl("ddcukaioth") as DropDownList).SelectedItem.Value;
            Label jum1 = (Label)Gridview5.FooterRow.FindControl("Label7");

            Label jum = (Label)Gridview5.FooterRow.FindControl("Label2");
            Label gmt1 = (Label)Gridview5.FooterRow.FindControl("Label11");
            Label jumI = (Label)Gridview5.FooterRow.FindControl("Label6");
            string chk = (Gridview5.Rows[i].FindControl("CheckBox1") as CheckBox).Checked.ToString();
            GTotal += (float)Convert.ToDecimal(total);
            ITotal += (float)Convert.ToDecimal(totalI);

            if (chk == "True")
            {
                decimal Gstval = Convert.ToDecimal(total) + Convert.ToDecimal(jum1.Text);
                if (tgst != "--- PILIH ---")
                {

                    decimal cuk = Convert.ToDecimal(gmt);
                    //decimal cuk2 = cuk / 100;
                    //decimal cuk1 = Gstval / cuk2;
                    //decimal cuk4 = Gstval - cuk1;
                    string ggst = cuk.ToString("C").Replace("RM", "").Replace("$", "");
                    Gst += (float)Convert.ToDecimal(ggst);
                    jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");
                    jum1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    gmt1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    jumI.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                    TextBox37.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                }
                else
                {
                    gmt = "0";
                    decimal cuk = Convert.ToDecimal(gmt);

                    string ggst = cuk.ToString("C").Replace("RM", "").Replace("$", "");
                    Gst += (float)Convert.ToDecimal(ggst);
                    jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");
                    jum1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    gmt1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    jumI.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                    TextBox37.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                }
            }
            else
            {

                jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");
                jumI.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                if (gmt == "")
                {
                    gmt = "0";
                }
                Gst += (float)Convert.ToDecimal(gmt);
                jum1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                TextBox37.Text = (ITotal).ToString("C").Replace("RM", "").Replace("$", "");
            }
        }

    }

    private void Mohon_GrandTotalBil2()
    {
        float GTotal = 0f;
        float ITotal = 0f;
        float Gst = 0f;
        for (int i = 0; i < grdmohon.Rows.Count; i++)
        {
            String total = (grdmohon.Rows[i].FindControl("Label1") as Label).Text;
            String totalI = (grdmohon.Rows[i].FindControl("Label5") as Label).Text;
            String gmt = (grdmohon.Rows[i].FindControl("Label10") as Label).Text;
            String tgst = (grdmohon.Rows[i].FindControl("ddcukaioth") as DropDownList).SelectedValue;
            Label jum1 = (Label)grdmohon.FooterRow.FindControl("Label7");

            Label jum = (Label)grdmohon.FooterRow.FindControl("Label2");
            Label gmt1 = (Label)grdmohon.FooterRow.FindControl("Label11");
            Label jumI = (Label)grdmohon.FooterRow.FindControl("Label6");
            string chk = (grdmohon.Rows[i].FindControl("CheckBox1") as CheckBox).Checked.ToString();
            GTotal += (float)Convert.ToDecimal(total);
            ITotal += (float)Convert.ToDecimal(totalI);
            chk = "True";
            if (chk == "True")
            {
                decimal Gstval = Convert.ToDecimal(total) + Convert.ToDecimal(jum1.Text);
                if (tgst != "--- PILIH ---")
                {

                    decimal cuk = Convert.ToDecimal(gmt);
                    //decimal cuk2 = cuk / 100;
                    //decimal cuk1 = Gstval / cuk2;
                    //decimal cuk4 = Gstval - cuk1;
                    string ggst = cuk.ToString("C").Replace("RM", "").Replace("$", "");
                    Gst += (float)Convert.ToDecimal(ggst);
                    jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");
                    jum1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    gmt1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    jumI.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                    TextBox37.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                }
                else
                {
                    gmt = "0";
                    decimal cuk = Convert.ToDecimal(gmt);

                    string ggst = cuk.ToString("C").Replace("RM", "").Replace("$", "");
                    Gst += (float)Convert.ToDecimal(ggst);
                    jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");
                    jum1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    gmt1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    jumI.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                    TextBox37.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                }
            }
            else
            {

                jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");
                jumI.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                if (gmt == "")
                {
                    gmt = "0";
                }
                Gst += (float)Convert.ToDecimal(gmt);
                jum1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                TextBox37.Text = (ITotal).ToString("C").Replace("RM", "").Replace("$", "");
            }
        }

    }



    private void GrandTotalMohBil()
    {
        float GTotal = 0f;
        float ITotal = 0f;
        float Gst = 0f;
        for (int i = 0; i < grdbilinv.Rows.Count; i++)
        {
            String total = (grdbilinv.Rows[i].FindControl("Label1") as Label).Text;
            String totalI = (grdbilinv.Rows[i].FindControl("Label5") as Label).Text;
            String gamt = (grdbilinv.Rows[i].FindControl("Label8") as Label).Text;

            String tgst = (grdbilinv.Rows[i].FindControl("ddcukaiinv") as DropDownList).SelectedItem.Value;
            Label jum1 = (Label)grdbilinv.FooterRow.FindControl("Label4");
            Label gamt1 = (Label)grdbilinv.FooterRow.FindControl("Label9");
            Label jum = (Label)grdbilinv.FooterRow.FindControl("Label2");

            Label jumI = (Label)grdbilinv.FooterRow.FindControl("Label6");
            string chk = (grdbilinv.Rows[i].FindControl("CheckBox1") as CheckBox).Checked.ToString();
            GTotal += (float)Convert.ToDecimal(total);
            ITotal += (float)Convert.ToDecimal(totalI);

            if (chk == "True")
            {
                decimal Gstval = Convert.ToDecimal(totalI);
                if (tgst != "--- PILIH ---")
                {
                    decimal cuk = Convert.ToDecimal(gamt);
                    //decimal cuk2 = cuk / 100;
                    //decimal cuk1 = Gstval / cuk2 ;
                    //decimal cuk4 = Gstval - cuk1;
                    string ggst = cuk.ToString("C").Replace("RM", "").Replace("$", "");
                    Gst += (float)Convert.ToDecimal(ggst);
                    jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");
                    jum1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    gamt1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    jumI.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                    TextBox37.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                }
                else
                {
                    gamt = "0";
                    decimal cuk = Convert.ToDecimal(gamt);

                    string ggst = cuk.ToString("C").Replace("RM", "").Replace("$", "");
                    Gst += (float)Convert.ToDecimal(ggst);
                    jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");
                    jum1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    gamt1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    jumI.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                    TextBox37.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                }
            }
            else
            {
                jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");
                jumI.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                if (gamt == "")
                {
                    gamt = "0";
                }
                Gst += (float)Convert.ToDecimal(gamt);
                jum1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                TextBox37.Text = (ITotal).ToString("C").Replace("RM", "").Replace("$", "");
            }
        }

    }



    private void GrandTotalMohBil2()
    {
        float GTotal = 0f;
        float ITotal = 0f;
        float Gst = 0f;
        for (int i = 0; i < grdbilinv.Rows.Count; i++)
        {
            String total = (grdbilinv.Rows[i].FindControl("Label1") as Label).Text;
            String totalI = (grdbilinv.Rows[i].FindControl("Label5") as Label).Text;
            String gmt = (grdbilinv.Rows[i].FindControl("Label10") as Label).Text;
            String tgst = (grdbilinv.Rows[i].FindControl("ddcukaioth") as DropDownList).SelectedItem.Value;
            Label jum1 = (Label)grdbilinv.FooterRow.FindControl("Label7");

            Label jum = (Label)grdbilinv.FooterRow.FindControl("Label2");
            Label gmt1 = (Label)grdbilinv.FooterRow.FindControl("Label11");
            Label jumI = (Label)grdbilinv.FooterRow.FindControl("Label6");
            string chk = (grdbilinv.Rows[i].FindControl("CheckBox1") as CheckBox).Checked.ToString();
            GTotal += (float)Convert.ToDecimal(total);
            ITotal += (float)Convert.ToDecimal(totalI);

            if (chk == "True")
            {
                decimal Gstval = Convert.ToDecimal(total) + Convert.ToDecimal(jum1.Text);
                if (tgst != "--- PILIH ---")
                {

                    decimal cuk = Convert.ToDecimal(gmt);
                    //decimal cuk2 = cuk / 100;
                    //decimal cuk1 = Gstval / cuk2;
                    //decimal cuk4 = Gstval - cuk1;
                    string ggst = cuk.ToString("C").Replace("RM", "").Replace("$", "");
                    Gst += (float)Convert.ToDecimal(ggst);
                    jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");
                    jum1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    gmt1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    jumI.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                    TextBox37.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                }
                else
                {
                    gmt = "0";
                    decimal cuk = Convert.ToDecimal(gmt);

                    string ggst = cuk.ToString("C").Replace("RM", "").Replace("$", "");
                    Gst += (float)Convert.ToDecimal(ggst);
                    jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");
                    jum1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    gmt1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    jumI.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                    TextBox37.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                }
            }
            else
            {

                jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");
                jumI.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                if (gmt == "")
                {
                    gmt = "0";
                }
                Gst += (float)Convert.ToDecimal(gmt);
                jum1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                TextBox37.Text = (ITotal).ToString("C").Replace("RM", "").Replace("$", "");
            }
        }

    }

    private void GrandTotalBil()
    {
        float GTotal = 0f;
        float ITotal = 0f;
        float Gst = 0f;
        for (int i = 0; i < Gridview5.Rows.Count; i++)
        {
            String total = (Gridview5.Rows[i].FindControl("Label1") as Label).Text;
            String totalI = (Gridview5.Rows[i].FindControl("Label5") as Label).Text;
            String gamt = (Gridview5.Rows[i].FindControl("Label8") as Label).Text;

            String tgst = (Gridview5.Rows[i].FindControl("ddcukaiinv") as DropDownList).SelectedItem.Value;
            Label jum1 = (Label)Gridview5.FooterRow.FindControl("Label4");
            Label gamt1 = (Label)Gridview5.FooterRow.FindControl("Label9");
            Label jum = (Label)Gridview5.FooterRow.FindControl("Label2");

            Label jumI = (Label)Gridview5.FooterRow.FindControl("Label6");
            string chk = (Gridview5.Rows[i].FindControl("CheckBox1") as CheckBox).Checked.ToString();
            GTotal += (float)Convert.ToDecimal(total);
            ITotal += (float)Convert.ToDecimal(totalI);

            if (chk == "True")
            {
                decimal Gstval = Convert.ToDecimal(totalI);
                if (tgst != "--- PILIH ---")
                {
                    decimal cuk = Convert.ToDecimal(gamt);
                    //decimal cuk2 = cuk / 100;
                    //decimal cuk1 = Gstval / cuk2 ;
                    //decimal cuk4 = Gstval - cuk1;
                    string ggst = cuk.ToString("C").Replace("RM", "").Replace("$", "");
                    Gst += (float)Convert.ToDecimal(ggst);
                    jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");
                    jum1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    gamt1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    jumI.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                    TextBox37.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                }
                else
                {
                    gamt = "0";
                    decimal cuk = Convert.ToDecimal(gamt);

                    string ggst = cuk.ToString("C").Replace("RM", "").Replace("$", "");
                    Gst += (float)Convert.ToDecimal(ggst);
                    jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");
                    jum1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    gamt1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    jumI.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                    TextBox37.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                }
            }
            else
            {
                jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");
                jumI.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                if (gamt == "")
                {
                    gamt = "0";
                }
                Gst += (float)Convert.ToDecimal(gamt);
                jum1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                TextBox37.Text = (ITotal).ToString("C").Replace("RM", "").Replace("$", "");
            }
        }

    }

    private void Mohon_GrandTotalBil()
    {
        float GTotal = 0f;
        float ITotal = 0f;
        float Gst = 0f;
        for (int i = 0; i < grdmohon.Rows.Count; i++)
        {
            String total = (grdmohon.Rows[i].FindControl("Label1") as Label).Text;
            String totalI = (grdmohon.Rows[i].FindControl("Label5") as Label).Text;
            String gamt = (grdmohon.Rows[i].FindControl("Label8") as Label).Text;

            String tgst = (grdmohon.Rows[i].FindControl("ddcukaiinv") as DropDownList).SelectedValue;
            Label jum1 = (Label)grdmohon.FooterRow.FindControl("Label4");
            Label gamt1 = (Label)grdmohon.FooterRow.FindControl("Label9");
            Label jum = (Label)grdmohon.FooterRow.FindControl("Label2");

            Label jumI = (Label)grdmohon.FooterRow.FindControl("Label6");
            string chk = (grdmohon.Rows[i].FindControl("CheckBox1") as CheckBox).Checked.ToString();
            if (total != "")
            {
                GTotal += (float)Convert.ToDecimal(total);
            }
            else
            {
                GTotal += 0;
            }
            if (totalI != "")
            {
                ITotal += (float)Convert.ToDecimal(totalI);
            }
            else
            {
                ITotal += 0;
            }
            chk = "True";
            if (chk == "True")
            {
                decimal Gstval = 0;
                if (totalI != "")
                {
                    Gstval = Convert.ToDecimal(totalI);
                }
                
                if (tgst != "--- PILIH ---")
                {
                    decimal cuk = 0;
                    if(gamt != "")
                    { 
                    cuk = Convert.ToDecimal(gamt);
                    }
                    else
                    {
                        cuk = 0;
                    }
                    //decimal cuk2 = cuk / 100;
                    //decimal cuk1 = Gstval / cuk2 ;
                    //decimal cuk4 = Gstval - cuk1;
                    string ggst = cuk.ToString("C").Replace("RM", "").Replace("$", "");
                    Gst += (float)Convert.ToDecimal(ggst);
                    jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");
                    jum1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    gamt1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    jumI.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                    TextBox17.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                }
                else
                {
                    gamt = "0";
                    decimal cuk = Convert.ToDecimal(gamt);

                    string ggst = cuk.ToString("C").Replace("RM", "").Replace("$", "");
                    Gst += (float)Convert.ToDecimal(ggst);
                    jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");
                    jum1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    gamt1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    jumI.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                    TextBox17.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                }
            }
            else
            {
                jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");
                jumI.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                if (gamt == "")
                {
                    gamt = "0";
                }
                Gst += (float)Convert.ToDecimal(gamt);
                jum1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                TextBox17.Text = (ITotal).ToString("C").Replace("RM", "").Replace("$", "");
            }
        }

    }


    private void GrandTotalcre()
    {
        decimal GTotal = 0;
        decimal ITotal = 0;
        decimal Gst = 0;
        for (int i = 0; i < Gridview10.Rows.Count; i++)
        {
            String total = (Gridview10.Rows[i].FindControl("Txtdis") as TextBox).Text;
            String Itotal = (Gridview10.Rows[i].FindControl("Label5") as Label).Text;
            String tgst = (Gridview10.Rows[i].FindControl("ddkodgst") as DropDownList).SelectedItem.Value;
            String gamt = (Gridview10.Rows[i].FindControl("Label8") as Label).Text;
            Label jum = (Label)Gridview10.FooterRow.FindControl("Label3");

            Label gamt1 = (Label)Gridview10.FooterRow.FindControl("Label9");
            Label jumI = (Label)Gridview10.FooterRow.FindControl("Label6");
            string chk = (Gridview10.Rows[i].FindControl("Chkcre") as CheckBox).Checked.ToString();
            GTotal += (decimal)Convert.ToDecimal(total);
            ITotal += (decimal)Convert.ToDecimal(Itotal);
            if (chk == "True")
            {
                decimal Gstval = Convert.ToDecimal(Itotal);
                if (tgst != "--- PILIH ---")
                {
                    decimal cuk = Convert.ToDecimal(gamt);
                    //decimal cuk2 = cuk / 100;
                    //decimal cuk1 = Gstval / cuk2 ;
                    //decimal cuk4 = Gstval - cuk1;
                    string ggst = cuk.ToString("C").Replace("RM", "").Replace("$", "");
                    Gst += (decimal)Convert.ToDecimal(ggst);
                    jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");

                    gamt1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    jumI.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                    TextBox18.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                }
                else
                {
                    gamt = "0";
                    decimal cuk = Convert.ToDecimal(gamt);

                    string ggst = cuk.ToString("C").Replace("RM", "").Replace("$", "");
                    Gst += (decimal)Convert.ToDecimal(ggst);
                    jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");

                    gamt1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    jumI.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                    TextBox18.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                }
            }
            else
            {
                jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");
                jumI.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                if (gamt == "")
                {
                    gamt = "0";
                }
                Gst += (decimal)Convert.ToDecimal(gamt);
                gamt1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");

                TextBox18.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
            }
        }

    }

    private void GrandTotalcre2()
    {
        decimal GTotal = 0;
        decimal ITotal = 0;
        decimal Gst = 0;
        for (int i = 0; i < Gridview10.Rows.Count; i++)
        {
            String total = (Gridview10.Rows[i].FindControl("Txtdis") as TextBox).Text;
            String Itotal = (Gridview10.Rows[i].FindControl("Label5") as Label).Text;
            String tgst = (Gridview10.Rows[i].FindControl("ddkodgstoth") as DropDownList).SelectedItem.Value;
            String gmt = (Gridview10.Rows[i].FindControl("Label10") as Label).Text;
            Label jum = (Label)Gridview10.FooterRow.FindControl("Label3");

            Label gmt1 = (Label)Gridview10.FooterRow.FindControl("Label11");
            Label jumI = (Label)Gridview10.FooterRow.FindControl("Label6");
            string chk = (Gridview10.Rows[i].FindControl("Chkcre") as CheckBox).Checked.ToString();
            GTotal += (decimal)Convert.ToDecimal(total);
            ITotal += (decimal)Convert.ToDecimal(Itotal);
            if (chk == "True")
            {
                decimal Gstval = Convert.ToDecimal(total) + Convert.ToDecimal(gmt1.Text);
                if (tgst != "--- PILIH ---")
                {

                    decimal cuk = Convert.ToDecimal(gmt);
                    //decimal cuk2 = cuk / 100;
                    //decimal cuk1 = Gstval / cuk2;
                    //decimal cuk4 = Gstval - cuk1;
                    string ggst = cuk.ToString("C").Replace("RM", "").Replace("$", "");
                    Gst += (decimal)Convert.ToDecimal(ggst);
                    jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");

                    gmt1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    jumI.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                    TextBox18.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                }
                else
                {
                    gmt = "0";
                    decimal cuk = Convert.ToDecimal(gmt);

                    string ggst = cuk.ToString("C").Replace("RM", "").Replace("$", "");
                    Gst += (decimal)Convert.ToDecimal(ggst);
                    jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");

                    gmt1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    jumI.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                    TextBox18.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                }
            }
            else
            {
                jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");
                jumI.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                if (gmt == "")
                {
                    gmt = "0";
                }
                Gst += (decimal)Convert.ToDecimal(gmt);

                gmt1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                TextBox18.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
            }
        }

    }


    protected void disChangedBil(object sender, EventArgs eventArgs)
    {
        decimal numTotal = 0;
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox qty = (TextBox)row.FindControl("TextBox54");
        TextBox unit = (TextBox)row.FindControl("TextBox53");
        TextBox dis = (TextBox)row.FindControl("txtdis");
        Label jum = (Label)row.FindControl("Label1");

        Label JUMI = (Label)row.FindControl("Label5");
        if (qty.ToString() != "" && unit.ToString() !="")
        {
            decimal Jum1 = Convert.ToDecimal(jum.Text);
            decimal dis1 = Convert.ToDecimal(dis.Text);
            tab5();
            if (qty.ToString() != "")
            {
                numTotal = Jum1 - dis1;
                jum.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");
                JUMI.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");

                GrandTotalBil();
            }
        }
        //code to go here when pri is changed
    }

    protected void Mohon_disChangedBil(object sender, EventArgs eventArgs)
    {
        decimal numTotal = 0;
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox qty = (TextBox)row.FindControl("TextBox54");
        TextBox unit = (TextBox)row.FindControl("TextBox53");
        TextBox DIS = (TextBox)row.FindControl("txtdis");
        Label jum = (Label)row.FindControl("Label1");

        Label JUMI = (Label)row.FindControl("Label5");
        qty.Text = "1";
        decimal dis = 0;
        if (DIS.Text != "")
        {
            dis = Convert.ToDecimal(DIS.Text);
        }
        else
        {
            dis = 0;
        }
        if (qty.Text != "" && unit.Text != "")
        {
            decimal Jum1 = Convert.ToDecimal(unit.Text);
            decimal dis1 = Convert.ToDecimal(dis);
            tab1();
            if (qty.Text != "")
            {
                numTotal = Jum1 - dis1;
                jum.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");
                JUMI.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");

                Mohon_GrandTotalBil();
            }
        }
        //code to go here when pri is changed
    }

    protected void disChangedMohBil(object sender, EventArgs eventArgs)
    {
        decimal numTotal = 0;
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox qty = (TextBox)row.FindControl("TextBox56");
        TextBox unit = (TextBox)row.FindControl("TextBox55");
        TextBox dis = (TextBox)row.FindControl("txtdis");
        Label jum = (Label)row.FindControl("Label1");

        Label JUMI = (Label)row.FindControl("Label5");
        if (qty.ToString() != "" && unit.ToString() != "")
        {
            decimal Jum1 = Convert.ToDecimal(jum.Text);
            decimal dis1 = Convert.ToDecimal(dis.Text);
            tab5();
            if (qty.ToString() != "")
            {
                numTotal = Jum1 - dis1;
                jum.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");
                JUMI.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");

                GrandTotalMohBil();
            }
        }

        //code to go here when pri is changed
    }


    protected void disChanged1(object sender, EventArgs eventArgs)
    {
        decimal numTotal = 0;
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox qty = (TextBox)row.FindControl("TextBox24");
        TextBox unit = (TextBox)row.FindControl("TextBox23");
        TextBox dis = (TextBox)row.FindControl("txtdiscre");
        Label jum = (Label)row.FindControl("Label1");

        Label JUMI = (Label)row.FindControl("Label5");
        if (qty.ToString() != "" && unit.ToString() != "")
        {
            decimal Jum1 = Convert.ToDecimal(jum.Text);
            decimal dis1 = Convert.ToDecimal(dis.Text);
            tab3();
            if (qty.ToString() != "")
            {
                numTotal = Jum1 - dis1;
                jum.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");
                JUMI.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");

                GrandTotalcre();
            }
        }
        

        //code to go here when pri is changed
    }

    protected void ddkodgst_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal numTotal1 = 0;
        decimal numTotal2 = 0;
        decimal numTotal3 = 0;
        decimal numTotal4 = 0;
        int selRowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
        TextBox tOt = (TextBox)Gridview10.Rows[selRowIndex].FindControl("Txtdis");
        Label txt = (Label)Gridview10.FooterRow.FindControl("Label6");

        Label gamt = (Label)Gridview10.Rows[selRowIndex].FindControl("Label8");
        Label gmt = (Label)Gridview10.Rows[selRowIndex].FindControl("Label10");


        Label txt1 = (Label)Gridview10.Rows[selRowIndex].FindControl("Label5");
        DropDownList dd = (DropDownList)Gridview10.Rows[selRowIndex].FindControl("ddkodgstoth");
        String tgst_1 = (Gridview10.Rows[selRowIndex].FindControl("ddkodgst") as DropDownList).SelectedItem.Value;
        CheckBox chk = (CheckBox)Gridview10.Rows[selRowIndex].FindControl("Chkcre");
        Label tax1 = (Label)Gridview10.FooterRow.FindControl("Label9");
        if (tOt.ToString() != "")
        {
            string tgst = string.Empty;
            DataTable sel_gst = new DataTable();
            sel_gst = Dbcon.Ora_Execute_table("select Ref_nama_cukai,Ref_kadar,Ref_kod_cukai From KW_Ref_Tetapan_cukai where Ref_kod_cukai='" + tgst_1 + "'");
            if (sel_gst.Rows.Count != 0)
            {
                tgst = sel_gst.Rows[0]["Ref_kadar"].ToString();
            }
            else
            {
                tgst = "0";
            }

            if (tgst_1 != "--- PILIH ---")
            {
                if (chk.Checked == true)
                {

                    decimal tot1 = Convert.ToDecimal(tOt.Text);
                    decimal tgst1 = Convert.ToDecimal(tgst) + 100;
                    decimal fgst = tgst1 / 100;
                    numTotal1 = tot1 / fgst;
                    numTotal4 = tot1 - numTotal1;
                    txt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                    gamt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                    numTotal2 = Convert.ToDecimal(tOt.Text) - numTotal4;
                    numTotal3 = Convert.ToDecimal(txt1.Text);
                    tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                    txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                    tab3();
                    dd.Enabled = true;
                    //dd.Attributes.Remove("style");
                    dd.Attributes.Remove("readonly");
                    GrandTotalcre();
                }
                else
                {
                    decimal tot1 = Convert.ToDecimal(tOt.Text) + Convert.ToDecimal(gmt.Text);
                    int tgst1 = Convert.ToInt32(tgst);

                    numTotal1 = tot1 * tgst1 / 100;
                    txt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                    gamt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                    numTotal2 = Convert.ToDecimal(tOt.Text) + numTotal1 + Convert.ToDecimal(gmt.Text);
                    txt1.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                    tab3();
                    GrandTotalcre();
                }
            }
            else
            {

                numTotal1 = Convert.ToDecimal(txt1.Text);
                if (chk.Checked == true)
                {
                    numTotal2 = numTotal1 - Convert.ToDecimal(gmt.Text);
                    numTotal3 = numTotal1;
                    tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                    //txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                }
                else
                {
                    numTotal2 = numTotal1 + Convert.ToDecimal(gmt.Text);
                    numTotal3 = numTotal1;
                    tOt.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                    //txt1.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                }

                txt.Text = "0.00";
                gamt.Text = "0.00";
                tab3();
                GrandTotalcre();
            }
        }
    }

    protected void ddkodgstoth_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal numTotal1 = 0;
        decimal numTotal2 = 0;
        decimal numTotal3 = 0;
        decimal numTotal4 = 0;
        int selRowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
        TextBox tOt = (TextBox)Gridview10.Rows[selRowIndex].FindControl("Txtdis");
        Label txt = (Label)Gridview10.FooterRow.FindControl("Label6");
        Label txt1 = (Label)Gridview10.Rows[selRowIndex].FindControl("Label5");
        Label gmt = (Label)Gridview10.Rows[selRowIndex].FindControl("Label10");
        String tgst_1 = (Gridview10.Rows[selRowIndex].FindControl("ddkodgstoth") as DropDownList).SelectedItem.Value;
        CheckBox chk = (CheckBox)Gridview10.Rows[selRowIndex].FindControl("Chkcre");
        DropDownList dd = (DropDownList)Gridview10.Rows[selRowIndex].FindControl("ddkodgstoth");
        Label tax1 = (Label)Gridview10.FooterRow.FindControl("Label4");
        Label tax1_cur = (Label)Gridview10.Rows[selRowIndex].FindControl("Label8");
        if (tOt.ToString() != "")
        {
            string tgst = string.Empty;
            DataTable sel_gst = new DataTable();
            sel_gst = Dbcon.Ora_Execute_table("select Ref_nama_cukai,Ref_kadar,Ref_kod_cukai From KW_Ref_Tetapan_cukai where Ref_kod_cukai='" + tgst_1 + "'");
            if (sel_gst.Rows.Count != 0)
            {
                tgst = sel_gst.Rows[0]["Ref_kadar"].ToString();
            }
            else
            {
                tgst = "0";
            }

            if (tgst_1 != "--- PILIH ---")
            {
                if (chk.Checked == true)
                {
                    dd.Enabled = true;
                    //dd.Attributes.Remove("style");
                    dd.Attributes.Remove("readonly");
                    decimal tot1 = Convert.ToDecimal(tOt.Text);
                    decimal tgst1 = Convert.ToDecimal(tgst) + 100;
                    decimal fgst = tgst1 / 100;
                    numTotal1 = tot1 / fgst;
                    numTotal4 = tot1 - numTotal1;
                    txt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                    gmt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                    numTotal2 = tot1 - numTotal4;
                    numTotal3 = Convert.ToDecimal(txt1.Text);
                    tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                    txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                    tab3();
                    GrandTotalcre2();
                }
                else
                {
                    decimal tot1 = Convert.ToDecimal(tOt.Text);
                    int tgst1 = Convert.ToInt32(tgst);

                    numTotal1 = tot1 * tgst1 / 100;
                    txt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                    gmt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                    numTotal2 = Convert.ToDecimal(tOt.Text) + numTotal1;
                    txt1.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                    tab3();
                    GrandTotalcre2();
                }
            }
            else
            {

                numTotal1 = Convert.ToDecimal(txt1.Text);
                if (chk.Checked == true)
                {
                    dd.Enabled = false;
                    //dd.Attributes.Add("style", "pointer-events:none;");
                    dd.Attributes.Add("readonly", "readonly");
                    numTotal2 = numTotal1 - Convert.ToDecimal(tax1_cur.Text);
                    numTotal3 = numTotal1 - Convert.ToDecimal(tax1_cur.Text);
                    tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                    //txt1.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                }
                else
                {
                    numTotal2 = numTotal1 - Convert.ToDecimal(tax1_cur.Text);
                    numTotal3 = numTotal1 - Convert.ToDecimal(tax1_cur.Text);
                    tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                    //txt1.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                }

                txt.Text = "0.00";
                gmt.Text = "0.00";
                tab3();
                GrandTotalcre2();
            }
        }
    }





    protected void BindKoff()
    {

        qry1 = "select no_permohonan,Format(tarkih_mohon,'dd/MM/yyyy')tarikh_invois,Bayar_kepada,p.Ref_nama_syarikat,Format(jumlah,'#,##,###.00') jumlah,'' Status from kw_Pembayaran_invois  I  left join KW_Ref_Pelanggan P on p.Ref_no_syarikat=I.Bayar_kepada where I.status='B'";
        SqlCommand cmd2 = new SqlCommand("" + qry1 + "", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            Gridview10.DataSource = ds2;
            Gridview10.DataBind();
            int columncount = Gridview10.Rows[0].Cells.Count;
            Gridview10.Rows[0].Cells.Clear();
            Gridview10.Rows[0].Cells.Add(new TableCell());
            Gridview10.Rows[0].Cells[0].ColumnSpan = columncount;
            Gridview10.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
        }
        else
        {
            Gridview10.DataSource = ds2;
            Gridview10.DataBind();
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

            ddpro1.DataSource = dt;
            ddpro1.DataTextField = "Ref_Projek_name";
            ddpro1.DataValueField = "Ref_Projek_code";
            ddpro1.DataBind();
            ddpro1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            ddpro2.DataSource = dt;
            ddpro2.DataTextField = "Ref_Projek_name";
            ddpro2.DataValueField = "Ref_Projek_code";
            ddpro2.DataBind();
            ddpro2.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void pelanggan()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select Ref_jenis_akaun,Ref_nama_syarikat from  KW_Ref_Pelanggan";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);


            ddpela2.DataSource = dt;
            ddpela2.DataTextField = "Ref_nama_syarikat";
            ddpela2.DataValueField = "Ref_jenis_akaun";
            ddpela2.DataBind();
            ddpela2.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            ddpela3.DataSource = dt;
            ddpela3.DataTextField = "Ref_nama_syarikat";
            ddpela3.DataValueField = "Ref_jenis_akaun";
            ddpela3.DataBind();
            ddpela3.Items.Insert(0, new ListItem("--- PILIH ---", ""));


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void grvStudentDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (dt.Rows.Count > 1)
            {
                dt.Rows.Remove(dt.Rows[rowIndex]);
                drCurrentRow = dt.NewRow();
                ViewState["CurrentTable"] = dt;
                grdmohon.DataSource = dt;
                grdmohon.DataBind();

                for (int i = 0; i < grdmohon.Rows.Count - 1; i++)
                {
                    grdmohon.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                }

            }
        }
    }


    private void SetPreviousData1()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable2"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable2"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {



                    DropDownList box1 = (DropDownList)grdmohon.Rows[rowIndex].Cells[1].FindControl("TextBox11");
                    TextBox box3 = (TextBox)grdmohon.Rows[rowIndex].Cells[2].FindControl("TextBox13");
                    DropDownList box1_2 = (DropDownList)grdmohon.Rows[rowIndex].Cells[3].FindControl("dd_projek");
                    DropDownList box1_1 = (DropDownList)grdmohon.Rows[rowIndex].Cells[4].FindControl("dd_bank");
                    TextBox box2 = (TextBox)grdmohon.Rows[rowIndex].Cells[5].FindControl("TextBox12");
                    TextBox box5 = (TextBox)grdmohon.Rows[rowIndex].Cells[6].FindControl("TextBox53");
                    TextBox box6 = (TextBox)grdmohon.Rows[rowIndex].Cells[7].FindControl("TextBox54");
                    TextBox box7 = (TextBox)grdmohon.Rows[rowIndex].Cells[8].FindControl("Txtdis");
                    Label box8 = (Label)grdmohon.Rows[rowIndex].Cells[9].FindControl("Label1");
                    CheckBox chk = (CheckBox)grdmohon.Rows[rowIndex].Cells[10].FindControl("CheckBox1");
                    DropDownList box9 = (DropDownList)grdmohon.Rows[rowIndex].Cells[11].FindControl("ddcukaioth");
                    Label box10 = (Label)grdmohon.Rows[rowIndex].Cells[12].FindControl("Label10");
                    DropDownList box11 = (DropDownList)grdmohon.Rows[rowIndex].Cells[13].FindControl("ddcukaiinv");
                    Label box12 = (Label)grdmohon.Rows[rowIndex].Cells[14].FindControl("Label8");
                    Label box13 = (Label)grdmohon.Rows[rowIndex].Cells[14].FindControl("Label5");

                   
                        box1.SelectedValue = dt.Rows[i]["Column1"].ToString();
                        box3.Text = dt.Rows[i]["Column2"].ToString();
                    box1_2.SelectedValue = dt.Rows[i]["Column3"].ToString();
                    box1_1.SelectedValue = dt.Rows[i]["Column4"].ToString();
                    box2.Text = dt.Rows[i]["Column5"].ToString();
                    box5.Text = dt.Rows[i]["Column6"].ToString();
                        box6.Text = dt.Rows[i]["Column7"].ToString();
                        box7.Text = dt.Rows[i]["Column8"].ToString();
                        chk.Checked = true;
                        box8.Text = dt.Rows[i]["Column10"].ToString();
                        box9.Text = dt.Rows[i]["Column11"].ToString();
                        box10.Text = dt.Rows[i]["Column12"].ToString();
                        box11.Text = dt.Rows[i]["Column13"].ToString();
                        box12.Text = dt.Rows[i]["Column14"].ToString();
                        box13.Text = dt.Rows[i]["Column15"].ToString();
                        rowIndex++;
                   
                }
            }
        }
    }

    private void SetPreviousDataBil()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable5"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable5"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList box0 = (DropDownList)Gridview5.Rows[rowIndex].Cells[1].FindControl("ddkodbil");
                    TextBox box1 = (TextBox)Gridview5.Rows[rowIndex].Cells[2].FindControl("TextBox51");
                    TextBox box2 = (TextBox)Gridview5.Rows[rowIndex].Cells[3].FindControl("TextBox50");
                    TextBox box3 = (TextBox)Gridview5.Rows[rowIndex].Cells[4].FindControl("TextBox52");
                    DropDownList box4 = (DropDownList)Gridview5.Rows[rowIndex].Cells[5].FindControl("ddkodproj");
                    TextBox box5 = (TextBox)Gridview5.Rows[rowIndex].Cells[6].FindControl("TextBox53");
                    TextBox box6 = (TextBox)Gridview5.Rows[rowIndex].Cells[7].FindControl("TextBox54");
                    TextBox box7 = (TextBox)Gridview5.Rows[rowIndex].Cells[8].FindControl("Txtdis");
                    Label box8 = (Label)Gridview5.Rows[rowIndex].Cells[9].FindControl("Label1");
                    CheckBox chk = (CheckBox)Gridview5.Rows[rowIndex].Cells[10].FindControl("CheckBox1");
                    DropDownList box9 = (DropDownList)Gridview5.Rows[rowIndex].Cells[11].FindControl("ddcukaioth");
                    Label box10 = (Label)Gridview5.Rows[rowIndex].Cells[12].FindControl("Label10");
                    DropDownList box11 = (DropDownList)Gridview5.Rows[rowIndex].Cells[13].FindControl("ddcukaiinv");
                    Label box12 = (Label)Gridview5.Rows[rowIndex].Cells[14].FindControl("Label8");
                    Label box13 = (Label)Gridview5.Rows[rowIndex].Cells[14].FindControl("Label5");

                    box0.Text = dt.Rows[i]["Column1"].ToString();
                    box1.Text = dt.Rows[i]["Column2"].ToString();
                    box2.Text = dt.Rows[i]["Column3"].ToString();
                    box3.Text = dt.Rows[i]["Column4"].ToString();
                    box4.Text = dt.Rows[i]["Column5"].ToString();
                    box5.Text = dt.Rows[i]["Column6"].ToString();
                    box6.Text = dt.Rows[i]["Column7"].ToString();
                    box7.Text = dt.Rows[i]["Column8"].ToString();
                    chk.Checked = dt.Rows[i]["Column9"].ToString().ToUpperInvariant() == "TRUE";
                    box8.Text = dt.Rows[i]["Column10"].ToString();
                    box9.Text = dt.Rows[i]["Column11"].ToString();
                    box10.Text = dt.Rows[i]["Column12"].ToString();
                    box11.Text = dt.Rows[i]["Column13"].ToString();
                    box12.Text = dt.Rows[i]["Column14"].ToString();
                    box13.Text = dt.Rows[i]["Column15"].ToString();
                    rowIndex++;
                }
            }
        }
    }




    private void SetPreviousDataMohBil()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable6"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable6"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    DropDownList ddl1 = (DropDownList)grdbilinv.Rows[rowIndex].Cells[1].FindControl("ddkodKat");

                    DropDownList ddl2 = (DropDownList)grdbilinv.Rows[rowIndex].Cells[2].FindControl("ddkodcre");

                    DropDownList ddl3 = (DropDownList)grdbilinv.Rows[rowIndex].Cells[3].FindControl("ddkodBil");
                    DropDownList ddl4 = (DropDownList)grdbilinv.Rows[rowIndex].Cells[4].FindControl("ddprobil");
                    TextBox box1 = (TextBox)grdbilinv.Rows[rowIndex].Cells[5].FindControl("TextBox55");
                    TextBox box2 = (TextBox)grdbilinv.Rows[rowIndex].Cells[6].FindControl("TextBox56");
                    TextBox box3 = (TextBox)grdbilinv.Rows[rowIndex].Cells[7].FindControl("Txtdis");
                    Label box4 = (Label)grdbilinv.Rows[rowIndex].Cells[8].FindControl("Label1");
                    CheckBox chk = (CheckBox)grdbilinv.Rows[rowIndex].Cells[9].FindControl("CheckBox1");
                    DropDownList box5 = (DropDownList)grdbilinv.Rows[rowIndex].Cells[10].FindControl("ddcukaioth");
                    Label box6 = (Label)grdbilinv.Rows[rowIndex].Cells[11].FindControl("Label10");
                    DropDownList box7 = (DropDownList)grdbilinv.Rows[rowIndex].Cells[12].FindControl("ddcukaiinv");
                    Label box8 = (Label)grdbilinv.Rows[rowIndex].Cells[13].FindControl("Label8");
                    Label box9 = (Label)grdbilinv.Rows[rowIndex].Cells[13].FindControl("Label5");


                    ddl1.SelectedValue = dt.Rows[i]["Column1"].ToString();

                    if (ddl1.SelectedItem.Text == "PEMBEKAL")
                    {
                        tab5();
                        try
                        {
                            string com = "select Ref_kod_akaun,Ref_nama_syarikat from KW_Ref_Pembekal ";
                            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
                            DataTable dt1 = new DataTable();
                            adpt.Fill(dt1);


                            ddl2.DataSource = dt1;
                            ddl2.DataTextField = "Ref_nama_syarikat";
                            ddl2.DataValueField = "Ref_kod_akaun";
                            ddl2.DataBind();
                            ddl2.Items.Insert(0, new ListItem("--- PILIH ---", ""));

                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                    }
                    else if (ddl1.SelectedItem.Text == "SEMUA COA")
                    {
                        tab5();

                        try
                        {
                            string com = "select (kod_akaun + ' | '+ nama_akaun) nama_akaun,kod_akaun   from KW_Ref_Carta_Akaun where jenis_akaun_type != '1' and Status='A'";
                            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
                            DataTable dt1 = new DataTable();
                            adpt.Fill(dt1);


                            ddl2.DataSource = dt1;
                            ddl2.DataTextField = "nama_akaun";
                            ddl2.DataValueField = "kod_akaun";
                            ddl2.DataBind();
                            ddl2.Items.Insert(0, new ListItem("--- PILIH ---", ""));
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    ddl2.SelectedValue = dt.Rows[i]["Column2"].ToString();
                    ddl3.SelectedValue = dt.Rows[i]["Column3"].ToString();
                    ddl4.SelectedValue = dt.Rows[i]["Column4"].ToString();
                    box1.Text = dt.Rows[i]["Column5"].ToString();
                    box2.Text = dt.Rows[i]["Column6"].ToString();
                    box3.Text = dt.Rows[i]["Column7"].ToString();
                    box4.Text = dt.Rows[i]["Column8"].ToString();
                    chk.Checked = dt.Rows[i]["Column9"].ToString().ToUpperInvariant() == "TRUE";
                    box5.Text = dt.Rows[i]["Column10"].ToString();
                    box6.Text = dt.Rows[i]["Column11"].ToString();
                    box7.Text = dt.Rows[i]["Column12"].ToString();
                    box8.Text = dt.Rows[i]["Column13"].ToString();
                    box9.Text = dt.Rows[i]["Column14"].ToString();



                    rowIndex++;
                }
            }
        }
    }



    protected void ddcukaiinvBil_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal numTotal1 = 0;
        decimal numTotal2 = 0;
        decimal numTotal3 = 0;
        decimal numTotal4 = 0;
        int selRowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
        Label tOt = (Label)Gridview5.Rows[selRowIndex].FindControl("Label1");
        TextBox qty = (TextBox)Gridview5.Rows[selRowIndex].FindControl("TextBox54");
        TextBox unit = (TextBox)Gridview5.Rows[selRowIndex].FindControl("TextBox53");
        TextBox dis = (TextBox)Gridview5.Rows[selRowIndex].FindControl("Txtdis");
        Label txt = (Label)Gridview5.FooterRow.FindControl("Label4");
        Label gamt = (Label)Gridview5.Rows[selRowIndex].FindControl("Label8");
        Label gmt = (Label)Gridview5.Rows[selRowIndex].FindControl("Label10");
        Label txt1 = (Label)Gridview5.Rows[selRowIndex].FindControl("Label5");
        CheckBox chk = (CheckBox)Gridview5.Rows[selRowIndex].FindControl("CheckBox1");
        DropDownList dd = (DropDownList)Gridview5.Rows[selRowIndex].FindControl("ddcukaioth");
        String tgst_1 = (Gridview5.Rows[selRowIndex].FindControl("ddcukaiinv") as DropDownList).SelectedItem.Value;
        Label tax1 = (Label)Gridview5.FooterRow.FindControl("Label7");
        string gmt1;
        if (gmt.Text == "")
        {
            gmt1 = "0.00";
        }
        else
        {
            gmt1 = gmt.Text;
        }
        string tgst = string.Empty;
        DataTable sel_gst = new DataTable();
        sel_gst = Dbcon.Ora_Execute_table("select Ref_nama_cukai,Ref_kadar,Ref_kod_cukai From KW_Ref_Tetapan_cukai where Ref_kod_cukai='" + tgst_1 + "'");
        if (sel_gst.Rows.Count != 0)
        {
            tgst = sel_gst.Rows[0]["Ref_kadar"].ToString();
        }
        else
        {
            tgst = "0";
        }

        if (tgst_1 != "--- PILIH ---")
        {
            if (chk.Checked == true)
            {

                decimal tot1 = Convert.ToDecimal(txt1.Text);
                decimal tgst1 = Convert.ToDecimal(tgst) + 100;
                decimal fgst = tgst1 / 100;
                numTotal1 = tot1 / fgst;
                numTotal4 = tot1 - numTotal1;
                txt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                gamt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                numTotal2 = Convert.ToDecimal(txt1.Text) - numTotal4;
                numTotal3 = Convert.ToDecimal(txt1.Text);
                tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                tab5();
                dd.Enabled = true;
                //dd.Attributes.Remove("style");
                dd.Attributes.Remove("readonly");
                GrandTotalBil();
            }
            else
            {
                decimal tot1 = Convert.ToDecimal(tOt.Text) + Convert.ToDecimal(gmt1);
                int tgst1 = Convert.ToInt32(tgst);

                numTotal1 = tot1 * tgst1 / 100;
                txt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                gamt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                numTotal2 = Convert.ToDecimal(tOt.Text) + numTotal1 + Convert.ToDecimal(gmt1);
                txt1.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                tab5();
                GrandTotalBil();
            }
        }
        else
        {
            if (qty.Text != "" && unit.Text != "")
            {
                if (dis.Text == "")
                {
                    dis.Text = "0.00";
                }
                numTotal1 = Convert.ToDecimal(qty.Text) * Convert.ToDecimal(unit.Text);
                if (chk.Checked == true)
                {
                    decimal tot1 = Convert.ToDecimal(txt1.Text);
                    decimal tgst1 = Convert.ToDecimal(tgst) + 100;
                    decimal fgst = tgst1 / 100;
                    numTotal1 = tot1 / fgst;
                    numTotal4 = tot1 - numTotal1;
                    txt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                    gamt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                    numTotal2 = Convert.ToDecimal(txt1.Text) - numTotal4;
                    numTotal3 = Convert.ToDecimal(txt1.Text);
                    tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                    txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                }
                else
                {
                    decimal tot1 = Convert.ToDecimal(txt1.Text);
                    decimal tgst1 = Convert.ToDecimal(tgst) + 100;
                    decimal fgst = tgst1 / 100;
                    numTotal1 = tot1 / fgst;
                    numTotal4 = tot1 - numTotal1;
                    txt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                    gamt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                    numTotal2 = Convert.ToDecimal(txt1.Text) - numTotal4;
                    numTotal3 = Convert.ToDecimal(txt1.Text);
                    tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                    txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                }

                txt.Text = "0.00";
                gamt.Text = "0.00";
                tab5();
                GrandTotalBil();
            }
        }
    }


    protected void Mohon_ddcukaiinvBil_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal numTotal1 = 0;
        decimal numTotal2 = 0;
        decimal numTotal3 = 0;
        decimal numTotal4 = 0;
        int selRowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
        Label tOt = (Label)grdmohon.Rows[selRowIndex].FindControl("Label1");
        TextBox qty = (TextBox)grdmohon.Rows[selRowIndex].FindControl("TextBox54");
        TextBox unit = (TextBox)grdmohon.Rows[selRowIndex].FindControl("TextBox53");
        TextBox dis = (TextBox)grdmohon.Rows[selRowIndex].FindControl("Txtdis");
        Label txt = (Label)grdmohon.FooterRow.FindControl("Label4");
        Label gamt = (Label)grdmohon.Rows[selRowIndex].FindControl("Label8");
        Label gmt = (Label)grdmohon.Rows[selRowIndex].FindControl("Label10");
        Label txt1 = (Label)grdmohon.Rows[selRowIndex].FindControl("Label5");
        CheckBox chk = (CheckBox)grdmohon.Rows[selRowIndex].FindControl("CheckBox1");
        DropDownList dd = (DropDownList)grdmohon.Rows[selRowIndex].FindControl("ddcukaioth");
        String tgst_1 = (grdmohon.Rows[selRowIndex].FindControl("ddcukaiinv") as DropDownList).SelectedValue;
        Label tax1 = (Label)grdmohon.FooterRow.FindControl("Label7");
        string gmt1;
        if (gmt.Text == "")
        {
            gmt1 = "0.00";
        }
        else
        {
            gmt1 = gmt.Text;
        }
        qty.Text = "1";
        chk.Checked = true;
        string tgst = string.Empty;
        DataTable sel_gst = new DataTable();
        sel_gst = Dbcon.Ora_Execute_table("select Ref_nama_cukai,Ref_kadar,Ref_kod_cukai From KW_Ref_Tetapan_cukai where Ref_kod_cukai='" + tgst_1 + "'");
        if (sel_gst.Rows.Count != 0)
        {
            tgst = sel_gst.Rows[0]["Ref_kadar"].ToString();
        }
        else
        {
            tgst = "0";
        }

        if (tgst_1 != "")
        {
            if (chk.Checked == true)
            {

                decimal tot1 = Convert.ToDecimal(txt1.Text);
                decimal tgst1 = Convert.ToDecimal(tgst) + 100;
                decimal fgst = tgst1 / 100;
                numTotal1 = tot1 / fgst;
                numTotal4 = tot1 - numTotal1;
                txt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                gamt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                numTotal2 = Convert.ToDecimal(txt1.Text) + numTotal4;
                numTotal3 = Convert.ToDecimal(txt1.Text);
                tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                tab1();
                dd.Enabled = true;
                //dd.Attributes.Remove("style");
                dd.Attributes.Remove("readonly");
                Mohon_GrandTotalBil();
            }
            else
            {
                decimal tot1 = Convert.ToDecimal(tOt.Text) + Convert.ToDecimal(gmt1);
                int tgst1 = Convert.ToInt32(tgst);

                numTotal1 = tot1 * tgst1 / 100;
                txt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                gamt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                numTotal2 = Convert.ToDecimal(tOt.Text) + numTotal1 + Convert.ToDecimal(gmt1);
                txt1.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                tab1();
                Mohon_GrandTotalBil();
            }
        }
        else
        {
            if (qty.Text != "" && unit.Text != "")
            {
                if (dis.Text == "")
                {
                    dis.Text = "0.00";
                }
                numTotal1 = Convert.ToDecimal(qty.Text) * Convert.ToDecimal(unit.Text);
                if (chk.Checked == true)
                {
                    decimal tot1 = Convert.ToDecimal(txt1.Text);
                    decimal tgst1 = Convert.ToDecimal(tgst) + 100;
                    decimal fgst = tgst1 / 100;
                    numTotal1 = tot1 / fgst;
                    numTotal4 = tot1 - numTotal1;
                    txt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                    gamt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                    numTotal2 = Convert.ToDecimal(txt1.Text) + numTotal4;
                    numTotal3 = Convert.ToDecimal(txt1.Text);
                    tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                    txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                }
                else
                {
                    decimal tot1 = Convert.ToDecimal(txt1.Text);
                    decimal tgst1 = Convert.ToDecimal(tgst) + 100;
                    decimal fgst = tgst1 / 100;
                    numTotal1 = tot1 / fgst;
                    numTotal4 = tot1 - numTotal1;
                    txt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                    gamt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                    numTotal2 = Convert.ToDecimal(txt1.Text) + numTotal4;
                    numTotal3 = Convert.ToDecimal(txt1.Text);
                    tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                    txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                }

                txt.Text = "0.00";
                gamt.Text = "0.00";
                tab1();
                Mohon_GrandTotalBil();
            }
        }
    }

    protected void ddcukaiinvMohBil_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal numTotal1 = 0;
        decimal numTotal2 = 0;
        decimal numTotal3 = 0;
        decimal numTotal4 = 0;
        int selRowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
        Label tOt = (Label)grdbilinv.Rows[selRowIndex].FindControl("Label1");
         TextBox qty = (TextBox)grdbilinv.Rows[selRowIndex].FindControl("TextBox56");
        TextBox unit = (TextBox)grdbilinv.Rows[selRowIndex].FindControl("TextBox55");
        TextBox dis = (TextBox)grdbilinv.Rows[selRowIndex].FindControl("Txtdis");
        Label txt = (Label)grdbilinv.FooterRow.FindControl("Label4");
        Label gamt = (Label)grdbilinv.Rows[selRowIndex].FindControl("Label8");
        Label gmt = (Label)grdbilinv.Rows[selRowIndex].FindControl("Label10");
        Label txt1 = (Label)grdbilinv.Rows[selRowIndex].FindControl("Label5");
        CheckBox chk = (CheckBox)grdbilinv.Rows[selRowIndex].FindControl("CheckBox1");
        DropDownList dd = (DropDownList)grdbilinv.Rows[selRowIndex].FindControl("ddcukaioth");
        String tgst_1 = (grdbilinv.Rows[selRowIndex].FindControl("ddcukaiinv") as DropDownList).SelectedItem.Value;
        Label tax1 = (Label)grdbilinv.FooterRow.FindControl("Label7");
        
            string gmt1;
            if (gmt.Text == "")
            {
                gmt1 = "0.00";
            }
            else
            {
                gmt1 = gmt.Text;
            }
            string tgst = string.Empty;
            DataTable sel_gst = new DataTable();
            sel_gst = Dbcon.Ora_Execute_table("select Ref_nama_cukai,Ref_kadar,Ref_kod_cukai From KW_Ref_Tetapan_cukai where Ref_kod_cukai='" + tgst_1 + "'");
            if (sel_gst.Rows.Count != 0)
            {
                tgst = sel_gst.Rows[0]["Ref_kadar"].ToString();
            }
            else
            {
                tgst = "0";
            }

            if (tgst_1 != "--- PILIH ---")
            {
                if (chk.Checked == true)
                {

                    decimal tot1 = Convert.ToDecimal(txt1.Text);
                    decimal tgst1 = Convert.ToDecimal(tgst) + 100;
                    decimal fgst = tgst1 / 100;
                    numTotal1 = tot1 / fgst;
                    numTotal4 = tot1 - numTotal1;
                    txt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                    gamt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                    numTotal2 = Convert.ToDecimal(txt1.Text) - numTotal4;
                    numTotal3 = Convert.ToDecimal(txt1.Text);
                    tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                    txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                    tab5();
                    dd.Enabled = true;
                    //dd.Attributes.Remove("style");
                    dd.Attributes.Remove("readonly");
                    GrandTotalMohBil();
                }
                else
                {
                    decimal tot1 = Convert.ToDecimal(tOt.Text) + Convert.ToDecimal(gmt1);
                    int tgst1 = Convert.ToInt32(tgst);

                    numTotal1 = tot1 * tgst1 / 100;
                    txt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                    gamt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                    numTotal2 = Convert.ToDecimal(tOt.Text) + numTotal1 + Convert.ToDecimal(gmt1);
                    txt1.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                    tab5();
                    GrandTotalMohBil();
                }
            }
            else
            {
            if (qty.ToString() != "" && unit.ToString() != "")
            {
                if (dis.Text == "")
                {
                    dis.Text = "0.00";
                }
                numTotal1 = Convert.ToDecimal(qty.Text) * Convert.ToDecimal(unit.Text);
                if (chk.Checked == true)
                {

                    numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(gmt1);
                    numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text);
                    tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                    txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                }
                else
                {
                    numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) + Convert.ToDecimal(gmt1);
                    numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text);
                    tOt.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                    txt1.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");

                }

                txt.Text = "0.00";
                gamt.Text = "0.00";
                tab5();
                GrandTotalMohBil();
            }
            }
       
    }

    protected void ddcukaiothBil_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal numTotal1 = 0;
        decimal numTotal2 = 0;
        decimal numTotal3 = 0;
        decimal numTotal4 = 0;
        int selRowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
        Label tOt = (Label)Gridview5.Rows[selRowIndex].FindControl("Label1");
        TextBox qty = (TextBox)Gridview5.Rows[selRowIndex].FindControl("TextBox54");
        TextBox unit = (TextBox)Gridview5.Rows[selRowIndex].FindControl("TextBox53");
        TextBox dis = (TextBox)Gridview5.Rows[selRowIndex].FindControl("Txtdis");
        Label txt = (Label)Gridview5.FooterRow.FindControl("Label7");
        Label gmt = (Label)Gridview5.Rows[selRowIndex].FindControl("Label10");
        Label txt1 = (Label)Gridview5.Rows[selRowIndex].FindControl("Label5");
        CheckBox chk = (CheckBox)Gridview5.Rows[selRowIndex].FindControl("CheckBox1");
        DropDownList dd = (DropDownList)Gridview5.Rows[selRowIndex].FindControl("ddcukaioth");
        String tgst_1 = (Gridview5.Rows[selRowIndex].FindControl("ddcukaioth") as DropDownList).SelectedItem.Value;
        Label tax1 = (Label)Gridview5.FooterRow.FindControl("Label4");
        Label tax1_cur = (Label)Gridview5.Rows[selRowIndex].FindControl("Label8"); ;


        string tgst = string.Empty;
        DataTable sel_gst = new DataTable();
        sel_gst = Dbcon.Ora_Execute_table("select Ref_nama_cukai,Ref_kadar,Ref_kod_cukai From KW_Ref_Tetapan_cukai where Ref_kod_cukai='" + tgst_1 + "'");
        if (sel_gst.Rows.Count != 0)
        {
            tgst = sel_gst.Rows[0]["Ref_kadar"].ToString();
        }
        else
        {
            tgst = "0";
        }
        if (tgst_1 != "--- PILIH ---")
        {
            if (chk.Checked == true)
            {
                dd.Enabled = true;
                //dd.Attributes.Remove("style");
                dd.Attributes.Remove("readonly");
                decimal tot1 = Convert.ToDecimal(tOt.Text);
                decimal tgst1 = Convert.ToDecimal(tgst) + 100;
                decimal fgst = tgst1 / 100;
                numTotal1 = tot1 / fgst;
                numTotal4 = tot1 - numTotal1;
                txt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                gmt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                numTotal2 = tot1 - numTotal4;
                numTotal3 = Convert.ToDecimal(txt1.Text);
                tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                tab5();
                GrandTotalBil2();
            }
            else
            {
                decimal tot1 = Convert.ToDecimal(tOt.Text);
                int tgst1 = Convert.ToInt32(tgst);

                numTotal1 = tot1 * tgst1 / 100;
                txt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                gmt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                numTotal2 = Convert.ToDecimal(tOt.Text) + numTotal1;
                txt1.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                tab5();
                GrandTotalBil2();
            }
        }
        else
        {
            if (dis.Text == "")
            {
                dis.Text = "0.00";
            }

            numTotal1 = Convert.ToDecimal(qty.Text) * Convert.ToDecimal(unit.Text);
            if (chk.Checked == true)
            {
                dd.Enabled = false;
                //dd.Attributes.Add("style", "pointer-events:none;");
                dd.Attributes.Add("readonly", "readonly");

                numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1_cur.Text);
                numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1_cur.Text);
                tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                txt1.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
            }
            else
            {
                numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1_cur.Text);
                numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1_cur.Text);
                tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                txt1.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                //numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text);
                //numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text);
                //tOt.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                //txt1.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
            }

            txt.Text = "0.00";
            gmt.Text = "0.00";
            tab5();
            GrandTotalBil2();
        }
    }


    protected void Mohon_ddcukaiothBil_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal numTotal1 = 0;
        decimal numTotal2 = 0;
        decimal numTotal3 = 0;
        decimal numTotal4 = 0;
        int selRowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
        Label tOt = (Label)grdmohon.Rows[selRowIndex].FindControl("Label1");
        TextBox qty = (TextBox)grdmohon.Rows[selRowIndex].FindControl("TextBox54");
        TextBox unit = (TextBox)grdmohon.Rows[selRowIndex].FindControl("TextBox53");
        TextBox dis = (TextBox)grdmohon.Rows[selRowIndex].FindControl("Txtdis");
        Label txt = (Label)grdmohon.FooterRow.FindControl("Label7");
        Label gmt = (Label)grdmohon.Rows[selRowIndex].FindControl("Label10");
        Label txt1 = (Label)grdmohon.Rows[selRowIndex].FindControl("Label5");
        CheckBox chk = (CheckBox)grdmohon.Rows[selRowIndex].FindControl("CheckBox1");
        DropDownList dd = (DropDownList)grdmohon.Rows[selRowIndex].FindControl("ddcukaioth");
        String tgst_1 = (grdmohon.Rows[selRowIndex].FindControl("ddcukaioth") as DropDownList).SelectedItem.Value;
        Label tax1 = (Label)grdmohon.FooterRow.FindControl("Label4");
        Label tax1_cur = (Label)grdmohon.Rows[selRowIndex].FindControl("Label8"); ;


        string tgst = string.Empty;
        DataTable sel_gst = new DataTable();
        sel_gst = Dbcon.Ora_Execute_table("select Ref_nama_cukai,Ref_kadar,Ref_kod_cukai From KW_Ref_Tetapan_cukai where Ref_kod_cukai='" + tgst_1 + "'");
        if (sel_gst.Rows.Count != 0)
        {
            tgst = sel_gst.Rows[0]["Ref_kadar"].ToString();
        }
        else
        {
            tgst = "0";
        }
        if (tgst_1 != "--- PILIH ---")
        {
            if (chk.Checked == true)
            {
                dd.Enabled = true;
                //dd.Attributes.Remove("style");
                dd.Attributes.Remove("readonly");
                decimal tot1 = Convert.ToDecimal(tOt.Text);
                decimal tgst1 = Convert.ToDecimal(tgst) + 100;
                decimal fgst = tgst1 / 100;
                numTotal1 = tot1 / fgst;
                numTotal4 = tot1 - numTotal1;
                txt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                gmt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                numTotal2 = tot1 - numTotal4;
                numTotal3 = Convert.ToDecimal(txt1.Text);
                tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                tab1();
                Mohon_GrandTotalBil2();
            }
            else
            {
                decimal tot1 = Convert.ToDecimal(tOt.Text);
                int tgst1 = Convert.ToInt32(tgst);

                numTotal1 = tot1 * tgst1 / 100;
                txt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                gmt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                numTotal2 = Convert.ToDecimal(tOt.Text) + numTotal1;
                txt1.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                tab1();
                Mohon_GrandTotalBil2();
            }
        }
        else
        {
            if (dis.Text == "")
            {
                dis.Text = "0.00";
            }

            numTotal1 = Convert.ToDecimal(qty.Text) * Convert.ToDecimal(unit.Text);
            if (chk.Checked == true)
            {
                dd.Enabled = false;
                //dd.Attributes.Add("style", "pointer-events:none;");
                dd.Attributes.Add("readonly", "readonly");

                numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1_cur.Text);
                numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1_cur.Text);
                tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                txt1.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
            }
            else
            {
                numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1_cur.Text);
                numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1_cur.Text);
                tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                txt1.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                //numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text);
                //numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text);
                //tOt.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                //txt1.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
            }

            txt.Text = "0.00";
            gmt.Text = "0.00";
            tab1();
            Mohon_GrandTotalBil2();
        }
    }


    protected void ddcukaiothMohBil_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal numTotal1 = 0;
        decimal numTotal2 = 0;
        decimal numTotal3 = 0;
        decimal numTotal4 = 0;
        int selRowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
        Label tOt = (Label)grdbilinv.Rows[selRowIndex].FindControl("Label1");
        TextBox qty = (TextBox)grdbilinv.Rows[selRowIndex].FindControl("TextBox56");
        TextBox unit = (TextBox)grdbilinv.Rows[selRowIndex].FindControl("TextBox55");
        TextBox dis = (TextBox)grdbilinv.Rows[selRowIndex].FindControl("Txtdis");
        Label txt = (Label)grdbilinv.FooterRow.FindControl("Label7");
        Label gmt = (Label)grdbilinv.Rows[selRowIndex].FindControl("Label10");
        Label txt1 = (Label)grdbilinv.Rows[selRowIndex].FindControl("Label5");
        CheckBox chk = (CheckBox)grdbilinv.Rows[selRowIndex].FindControl("CheckBox1");
        DropDownList dd = (DropDownList)grdbilinv.Rows[selRowIndex].FindControl("ddcukaioth");
        String tgst_1 = (grdbilinv.Rows[selRowIndex].FindControl("ddcukaioth") as DropDownList).SelectedItem.Value;
        Label tax1 = (Label)grdbilinv.FooterRow.FindControl("Label4");
        Label tax1_cur = (Label)grdbilinv.Rows[selRowIndex].FindControl("Label8");


        string tgst = string.Empty;
        DataTable sel_gst = new DataTable();
        sel_gst = Dbcon.Ora_Execute_table("select Ref_nama_cukai,Ref_kadar,Ref_kod_cukai From KW_Ref_Tetapan_cukai where Ref_kod_cukai='" + tgst_1 + "'");
        if (sel_gst.Rows.Count != 0)
        {
            tgst = sel_gst.Rows[0]["Ref_kadar"].ToString();
        }
        else
        {
            tgst = "0";
        }
        if (tgst_1 != "--- PILIH ---")
        {
            if (chk.Checked == true)
            {
                dd.Enabled = true;
                //dd.Attributes.Remove("style");
                dd.Attributes.Remove("readonly");
                decimal tot1 = Convert.ToDecimal(tOt.Text);
                decimal tgst1 = Convert.ToDecimal(tgst) + 100;
                decimal fgst = tgst1 / 100;
                numTotal1 = tot1 / fgst;
                numTotal4 = tot1 - numTotal1;
                txt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                gmt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                numTotal2 = tot1 - numTotal4;
                numTotal3 = Convert.ToDecimal(txt1.Text);
                tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                tab5();
                GrandTotalMohBil2();
            }
            else
            {
                decimal tot1 = Convert.ToDecimal(tOt.Text);
                int tgst1 = Convert.ToInt32(tgst);

                numTotal1 = tot1 * tgst1 / 100;
                txt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                gmt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                numTotal2 = Convert.ToDecimal(tOt.Text) + numTotal1;
                txt1.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                tab5();
                GrandTotalMohBil2();
            }
        }
        else
        {
            if (dis.Text == "")
            {
                dis.Text = "0.00";
            }

            numTotal1 = Convert.ToDecimal(qty.Text) * Convert.ToDecimal(unit.Text);
            if (chk.Checked == true)
            {
                dd.Enabled = false;
                //dd.Attributes.Add("style", "pointer-events:none;");
                dd.Attributes.Add("readonly", "readonly");
                numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1_cur.Text);
                numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1_cur.Text);
                tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                txt1.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
            }
            else
            {
                numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1_cur.Text);
                numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1_cur.Text);
                tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                txt1.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                //numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text);
                //numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text);
                //tOt.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                //txt1.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
            }

            txt.Text = "0.00";
            gmt.Text = "0.00";
            tab5();
            GrandTotalMohBil2();
        }
    }

    private void SetPreviousDatacre()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable3"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable3"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList box0 = (DropDownList)Gridview10.Rows[rowIndex].Cells[1].FindControl("ddkodcre");
                    TextBox box1 = (TextBox)Gridview10.Rows[rowIndex].Cells[2].FindControl("txtket");
                    TextBox box2 = (TextBox)Gridview10.Rows[rowIndex].Cells[3].FindControl("Txtdis");
                    CheckBox chk = (CheckBox)Gridview10.Rows[rowIndex].Cells[4].FindControl("Chkcre");
                    DropDownList box3 = (DropDownList)Gridview10.Rows[rowIndex].Cells[5].FindControl("ddkodgst");
                    Label box4 = (Label)Gridview10.Rows[rowIndex].Cells[6].FindControl("Label8");
                    DropDownList box5 = (DropDownList)Gridview10.Rows[rowIndex].Cells[7].FindControl("ddkodgstoth");
                    Label box6 = (Label)Gridview10.Rows[rowIndex].Cells[8].FindControl("Label10");
                    Label box7 = (Label)Gridview10.Rows[rowIndex].Cells[8].FindControl("Label5");

                    box0.SelectedValue = dt.Rows[i]["Column1"].ToString();
                    box1.Text = dt.Rows[i]["Column2"].ToString();
                    box2.Text = dt.Rows[i]["Column3"].ToString();
                    chk.Checked = dt.Rows[i]["Column4"].ToString().ToUpperInvariant() == "TRUE";
                    box3.Text = dt.Rows[i]["Column5"].ToString();
                    box4.Text = dt.Rows[i]["Column6"].ToString();
                    box5.Text = dt.Rows[i]["Column7"].ToString();
                    box6.Text = dt.Rows[i]["Column8"].ToString();
                    box7.Text = dt.Rows[i]["Column9"].ToString();

                    rowIndex++;
                }
            }
        }
    }

    private void SetPreviousDatadeb()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable4"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable4"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList box0 = (DropDownList)Gridview11.Rows[rowIndex].Cells[1].FindControl("ddkoddeb");
                    TextBox box1 = (TextBox)Gridview11.Rows[rowIndex].Cells[2].FindControl("txtket");
                    TextBox box2 = (TextBox)Gridview11.Rows[rowIndex].Cells[3].FindControl("Txtdis");
                    CheckBox chk = (CheckBox)Gridview11.Rows[rowIndex].Cells[4].FindControl("Chkdeb");
                    DropDownList box3 = (DropDownList)Gridview11.Rows[rowIndex].Cells[5].FindControl("ddgstdeboth");
                    Label box4 = (Label)Gridview11.Rows[rowIndex].Cells[6].FindControl("Label10");
                    DropDownList box5 = (DropDownList)Gridview11.Rows[rowIndex].Cells[7].FindControl("ddgstdeb");
                    Label box6 = (Label)Gridview11.Rows[rowIndex].Cells[8].FindControl("Label8");
                    Label box7 = (Label)Gridview11.Rows[rowIndex].Cells[8].FindControl("Label5");

                    box0.SelectedValue = dt.Rows[i]["Column1"].ToString();
                    box1.Text = dt.Rows[i]["Column2"].ToString();
                    box2.Text = dt.Rows[i]["Column3"].ToString();
                    chk.Checked = dt.Rows[i]["Column4"].ToString().ToUpperInvariant() == "TRUE";
                    box3.Text = dt.Rows[i]["Column5"].ToString();
                    box4.Text = dt.Rows[i]["Column6"].ToString();
                    box5.Text = dt.Rows[i]["Column7"].ToString();
                    box6.Text = dt.Rows[i]["Column8"].ToString();
                    box7.Text = dt.Rows[i]["Column9"].ToString();

                    rowIndex++;
                }
            }
        }
    }

    protected void ddpela3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddpela3.SelectedItem.Text != "--- PILIH ---")
        {
            string com = "select no_invois from KW_Pembayaran_invoisBil_item where cre_kod_akaun='" + ddpela3.SelectedItem.Value + "' group by no_invois";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddinv2.DataSource = dt;
            ddinv2.DataTextField = "no_invois";
            ddinv2.DataValueField = "no_invois";
            ddinv2.DataBind();
            ddinv2.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            tab4();
        }
    }

    protected void ddinv2_SelectedIndexChanged(object sender, EventArgs e)
    {
        lnk_clk_Deb();
    }

    void lnk_clk_Deb()
    {
        string ssv1 = string.Empty;
        if (ddinv2.SelectedItem.Text != "--- PILIH ---")
        {
            ssv1 = ddinv2.SelectedItem.Text;
        }
        else
        {
            ssv1 = "";
        }

        string getsess_val = string.Empty;


        DataTable dt = new DataTable();
        dt = Dbcon.Ora_Execute_table("select untuk_akaun,cre_kod_akaun,Format(tarkih_invois,'dd/MM/yyyy') tarikh_invois,project_kod,Terma,deb_kod_akaun,item,keterangan,Format(unit,'#,##,###.00') unit,quantiti,gst,Format(Overall,'#,##,###.00') jumlah,tax from KW_Pembayaran_invoisBil_item where no_invois='" + ssv1 + "' and cre_kod_akaun='" + ddpela3.SelectedItem.Value + "'");

        if (dt.Rows.Count > 0)
        {

            ddakaun.Attributes.Add("disabled", "disabled");
            ddpela3.Attributes.Add("disabled", "disabled");
            txtnoruj2.Attributes.Add("disabled", "disabled");
            txtdtinvois.Attributes.Add("disabled", "disabled");
            ddpro2.Attributes.Add("disabled", "disabled");
            ddinvday2.Attributes.Add("disabled", "disabled");

            ddakaun.SelectedValue = dt.Rows[0][0].ToString();
            ddpela3.SelectedValue = dt.Rows[0][1].ToString().Trim();

            txtdtinvois.Text = dt.Rows[0][2].ToString();
            ddpro2.SelectedValue = dt.Rows[0][3].ToString();
            ddinvday2.SelectedItem.Text = dt.Rows[0][4].ToString();

            DataTable dt1 = new DataTable();
            Gridview7.Visible = true;

            dt1 = Dbcon.Ora_Execute_table("select m1.deb_kod_akaun as kod_akauan,s1.nama_akaun,s2.Ref_nama_cukai as othgstname,s3.Ref_nama_cukai as gstname,m1.no_invois as rujukan,m1.item,m1.keterangan,m1.unit as unit,m1.quantiti,m1.gst,case when m1.discount = '0.00' then '0.00' else m1.discount end as discount,m1.jumlah as jumlah,m1.Overall as Overall,m1.gstjumlah as gstjum,m1.othgstjumlah as othgst,CAST(CASE m1.tax WHEN 'Y' THEN 1 ELSE 0 END AS BIT) AS tax,m1.gsttype,m1.othgsttype from KW_Pembayaran_invoisBil_item  m1 left join KW_Ref_Carta_Akaun s1 on s1.kod_akaun=m1.deb_kod_akaun left join KW_Ref_Tetapan_cukai s2 on s2.Ref_kod_cukai=m1.othgsttype left join KW_Ref_Tetapan_cukai s3 on s3.Ref_kod_cukai=m1.gsttype where m1.no_invois='" + ssv1 + "' and m1.cre_kod_akaun='" + ddpela3.SelectedItem.Value + "' ");
            Gridview7.DataSource = dt1;
            Gridview7.DataBind();


            Button3.Visible = true;
            Button21.Visible = false;

            fr2.Visible = true;
            fr1.Visible = false;
            Bindcredit();

            Binddedit();
            tab4();



        }
        else
        {
            DataTable dt1 = new DataTable();
            dt1 = Dbcon.Ora_Execute_table("select m1.deb_kod_akaun as kod_akauan,s1.nama_akaun,s2.Ref_nama_cukai as othgstname,s3.Ref_nama_cukai as gstname,m1.no_invois as rujukan,m1.item,m1.keterangan,m1.unit as unit,m1.quantiti,m1.gst,case when m1.discount = '0.00' then '0.00' else m1.discount end as discount,m1.jumlah as jumlah,m1.Overall as Overall,m1.gstjumlah as gstjum,m1.othgstjumlah as othgst,CAST(CASE m1.tax WHEN 'Y' THEN 1 ELSE 0 END AS BIT) AS tax,m1.gsttype,m1.othgsttype from KW_Pembayaran_invoisBil_item  m1 left join KW_Ref_Carta_Akaun s1 on s1.kod_akaun=m1.deb_kod_akaun left join KW_Ref_Tetapan_cukai s2 on s2.Ref_kod_cukai=m1.othgsttype left join KW_Ref_Tetapan_cukai s3 on s3.Ref_kod_cukai=m1.gsttype where m1.no_invois='" + ssv1 + "' ");
            Gridview7.DataSource = dt1;
            Gridview7.DataBind();

            ddakaun.Attributes.Remove("disabled");
            ddpela3.Attributes.Remove("disabled");
            txtnoruj2.Attributes.Add("disabled", "disabled");
            txtdtinvois.Attributes.Remove("disabled");
            ddpro2.Attributes.Remove("disabled");
            ddinvday2.Attributes.Remove("disabled");

            ddakaun.SelectedValue = "";
            ddpela3.SelectedValue = "";

            txtdtinvois.Text = "";
            ddpro2.SelectedValue = "";
            ddinvday2.SelectedValue = "--- PILIH ---";



        }
    }


    protected void Gridview1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Find the DropDownList in the Row
            DropDownList ddlCountries = (e.Row.FindControl("ddkodcre") as DropDownList);
            ddlCountries.DataSource = GetData("select (kod_akaun + ' | '+ nama_akaun) nama_akaun,kod_akaun from KW_ref_carta_akaun where jenis_akaun_type != '1' and Status='A'");
            ddlCountries.DataTextField = "nama_akaun";
            ddlCountries.DataValueField = "kod_akaun";
            ddlCountries.DataBind();

            //Add Default Item in the DropDownList
            ddlCountries.Items.Insert(0, new ListItem("--- PILIH ---"));

            //Select the Country of Customer in DropDownList

        }
    }

    private DataSet GetData(string query)
    {
        string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
        SqlCommand cmd = new SqlCommand(query);
        using (SqlConnection con = new SqlConnection(conString))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                cmd.Connection = con;
                sda.SelectCommand = cmd;
                using (DataSet ds = new DataSet())
                {
                    sda.Fill(ds);
                    return ds;
                }
            }
        }
    }

    protected void ButtonAdd1_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid1();
    }


    protected void ButtonAddBil_Click(object sender, EventArgs e)
    {
        AddNewRowTobIL();
    }

    protected void ButtonAddMohBil_Click(object sender, EventArgs e)
    {
        AddNewRowToMohbIL();
    }
    protected void ButtonAddcre_Click(object sender, EventArgs e)
    {
        AddNewRowToGridcredit();
    }

    protected void ButtonAdddeb_Click(object sender, EventArgs e)
    {
        AddNewRowToGriddebit();
    }

    private void AddNewRowToGrid1()
    {
        int rowIndex = 0;
        decimal total1 = 0;
        decimal total2 = 0;
        decimal total3 = 0;
        decimal total4 = 0;
        if (ViewState["CurrentTable2"] != null)
        {
            DataTable dtCurrentTable2 = (DataTable)ViewState["CurrentTable2"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable2.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable2.Rows.Count; i++)
                {
                    //extract the TextBox values

                    DropDownList box1 = (DropDownList)grdmohon.Rows[rowIndex].Cells[1].FindControl("TextBox11");
                    
                   
                    TextBox box3 = (TextBox)grdmohon.Rows[rowIndex].Cells[2].FindControl("TextBox13");
                    DropDownList box1_2 = (DropDownList)grdmohon.Rows[rowIndex].Cells[3].FindControl("dd_projek");
                    DropDownList box1_1 = (DropDownList)grdmohon.Rows[rowIndex].Cells[4].FindControl("dd_bank");
                    TextBox box2 = (TextBox)grdmohon.Rows[rowIndex].Cells[5].FindControl("TextBox12");
                    //TextBox box4 = (TextBox)grdmohon.Rows[rowIndex].Cells[4].FindControl("TextBox14");
                    //TextBox box5 = (TextBox)grdmohon.Rows[rowIndex].Cells[5].FindControl("TextBox15");
                    //TextBox box6 = (TextBox)grdmohon.Rows[rowIndex].Cells[5].FindControl("TextBox16");

                    TextBox box7 = (TextBox)grdmohon.Rows[rowIndex].Cells[6].FindControl("TextBox53");
                    TextBox box8 = (TextBox)grdmohon.Rows[rowIndex].Cells[7].FindControl("TextBox54");
                    TextBox box9 = (TextBox)grdmohon.Rows[rowIndex].Cells[8].FindControl("Txtdis");
                    Label box10 = (Label)grdmohon.Rows[rowIndex].Cells[9].FindControl("Label1");
                    CheckBox chk = (CheckBox)grdmohon.Rows[rowIndex].Cells[10].FindControl("CheckBox1");
                    DropDownList box11 = (DropDownList)grdmohon.Rows[rowIndex].Cells[11].FindControl("ddcukaioth");
                    Label box12 = (Label)grdmohon.Rows[rowIndex].Cells[12].FindControl("Label10");
                    DropDownList box13 = (DropDownList)grdmohon.Rows[rowIndex].Cells[13].FindControl("ddcukaiinv");
                    Label box14 = (Label)grdmohon.Rows[rowIndex].Cells[14].FindControl("Label8");
                    Label box15 = (Label)grdmohon.Rows[rowIndex].Cells[14].FindControl("Label5");

                    drCurrentRow = dtCurrentTable2.NewRow();

                    dtCurrentTable2.Rows[i - 1]["Column1"] = box1.SelectedValue;
                   
                    dtCurrentTable2.Rows[i - 1]["Column2"] = box3.Text;
                    dtCurrentTable2.Rows[i - 1]["Column3"] = box1_2.SelectedValue;
                    dtCurrentTable2.Rows[i - 1]["Column4"] = box1_1.SelectedValue;
                    dtCurrentTable2.Rows[i - 1]["Column5"] = box2.Text;
                    //dtCurrentTable2.Rows[i - 1]["Column4"] = box4.Text;
                    //dtCurrentTable2.Rows[i - 1]["Column5"] = box5.Text;
                    //dtCurrentTable2.Rows[i - 1]["Column6"] = box6.Text;
                    dtCurrentTable2.Rows[i - 1]["Column6"] = box7.Text;
                    dtCurrentTable2.Rows[i - 1]["Column7"] = box8.Text;
                    dtCurrentTable2.Rows[i - 1]["Column8"] = box9.Text;
                    dtCurrentTable2.Rows[i - 1]["Column9"] = chk.Checked.ToString();
                    dtCurrentTable2.Rows[i - 1]["Column10"] = box10.Text;
                    dtCurrentTable2.Rows[i - 1]["Column11"] = box11.SelectedValue;
                    dtCurrentTable2.Rows[i - 1]["Column12"] = box12.Text;
                    dtCurrentTable2.Rows[i - 1]["Column13"] = box13.SelectedValue;
                    dtCurrentTable2.Rows[i - 1]["Column14"] = box14.Text;
                    dtCurrentTable2.Rows[i - 1]["Column15"] = box15.Text;

                    rowIndex++;
                    if (box10.Text != "")
                    {
                        decimal tamt1 = Convert.ToDecimal(box10.Text);
                        total1 += tamt1;
                    }

                    if (box12.Text != "")
                    {

                        decimal tamt2 = Convert.ToDecimal(box12.Text);
                        total2 += tamt2;

                    }
                    if (box14.Text != "")
                    {
                        decimal tamt3 = Convert.ToDecimal(box14.Text);
                        total3 += tamt3;
                    }
                    if (box15.Text != "")
                    {
                        decimal tamt4 = Convert.ToDecimal(box15.Text);
                        total4 += tamt4;
                    }

                }
                dtCurrentTable2.Rows.Add(drCurrentRow);
                ViewState["CurrentTable2"] = dtCurrentTable2;

                grdmohon.DataSource = dtCurrentTable2;
                grdmohon.DataBind();
                tab1();
                grdmohon.FooterRow.Cells[2].Text = "JUMLAH (RM) :";
                grdmohon.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                ((Label)grdmohon.FooterRow.Cells[7].FindControl("Label2")).Text = total1.ToString("C").Replace("RM", "").Replace("$", "");
                ((Label)grdmohon.FooterRow.Cells[9].FindControl("Label7")).Text = total2.ToString("C").Replace("RM", "").Replace("$", "");
                ((Label)grdmohon.FooterRow.Cells[11].FindControl("Label4")).Text = total3.ToString("C").Replace("RM", "").Replace("$", "");
                ((Label)grdmohon.FooterRow.Cells[11].FindControl("Label9")).Text = total3.ToString("C").Replace("RM", "").Replace("$", "");
                ((Label)grdmohon.FooterRow.Cells[12].FindControl("Label6")).Text = total4.ToString("C").Replace("RM", "").Replace("$", "");
                
                //if (dtCurrentTable2.Rows.Count == 2)
                //{
                //    grdmohon.FooterRow.Cells[1].Enabled = false;
                //}
                //else
                //{
                //    grdmohon.FooterRow.Cells[1].Enabled = true;
                //}
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousData1();
    }

    private void AddNewRowTobIL()
    {
        int rowIndex = 0;
        decimal total1 = 0;
        decimal total2 = 0;
        decimal total3 = 0;
        decimal total4 = 0;
        if (ViewState["CurrentTable5"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable5"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    DropDownList ddl1 = (DropDownList)Gridview5.Rows[rowIndex].Cells[1].FindControl("ddkodbil");
                    TextBox box1 = (TextBox)Gridview5.Rows[rowIndex].Cells[2].FindControl("TextBox51");
                    TextBox box2 = (TextBox)Gridview5.Rows[rowIndex].Cells[3].FindControl("TextBox50");
                    TextBox box3 = (TextBox)Gridview5.Rows[rowIndex].Cells[4].FindControl("TextBox52");
                    DropDownList ddl2 = (DropDownList)Gridview5.Rows[rowIndex].Cells[5].FindControl("ddkodproj");
                    TextBox box4 = (TextBox)Gridview5.Rows[rowIndex].Cells[6].FindControl("TextBox53");
                    TextBox box5 = (TextBox)Gridview5.Rows[rowIndex].Cells[7].FindControl("TextBox54");
                    TextBox box6 = (TextBox)Gridview5.Rows[rowIndex].Cells[8].FindControl("Txtdis");
                    Label box7 = (Label)Gridview5.Rows[rowIndex].Cells[9].FindControl("Label1");
                    CheckBox chk = (CheckBox)Gridview5.Rows[rowIndex].Cells[10].FindControl("CheckBox1");
                    DropDownList box8 = (DropDownList)Gridview5.Rows[rowIndex].Cells[11].FindControl("ddcukaioth");
                    Label box9 = (Label)Gridview5.Rows[rowIndex].Cells[12].FindControl("Label10");
                    DropDownList box10 = (DropDownList)Gridview5.Rows[rowIndex].Cells[13].FindControl("ddcukaiinv");
                    Label box11 = (Label)Gridview5.Rows[rowIndex].Cells[14].FindControl("Label8");
                    Label box12 = (Label)Gridview5.Rows[rowIndex].Cells[14].FindControl("Label5");

                    drCurrentRow = dtCurrentTable.NewRow();

                    dtCurrentTable.Rows[i - 1]["Column1"] = ddl1.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Column2"] = box1.Text;
                    dtCurrentTable.Rows[i - 1]["Column3"] = box2.Text;
                    dtCurrentTable.Rows[i - 1]["Column4"] = box3.Text;
                    dtCurrentTable.Rows[i - 1]["Column5"] = ddl2.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Column6"] = box4.Text;
                    dtCurrentTable.Rows[i - 1]["Column7"] = box5.Text;
                    dtCurrentTable.Rows[i - 1]["Column8"] = box6.Text;
                    dtCurrentTable.Rows[i - 1]["Column9"] = chk.Checked.ToString();
                    dtCurrentTable.Rows[i - 1]["Column10"] = box7.Text;
                    dtCurrentTable.Rows[i - 1]["Column11"] = box8.SelectedItem.Value;
                    dtCurrentTable.Rows[i - 1]["Column12"] = box9.Text;
                    dtCurrentTable.Rows[i - 1]["Column13"] = box10.SelectedItem.Value;
                    dtCurrentTable.Rows[i - 1]["Column14"] = box11.Text;
                    dtCurrentTable.Rows[i - 1]["Column15"] = box12.Text;

                    rowIndex++;
                    if (box7.Text != "")
                    {
                        decimal tamt1 = Convert.ToDecimal(box7.Text);
                        total1 += tamt1;
                    }

                    if (box9.Text != "")
                    {

                        decimal tamt2 = Convert.ToDecimal(box9.Text);
                        total2 += tamt2;

                    }
                    if (box11.Text != "")
                    {
                        decimal tamt3 = Convert.ToDecimal(box11.Text);
                        total3 += tamt3;
                    }
                    if (box12.Text != "")
                    {
                        decimal tamt4 = Convert.ToDecimal(box12.Text);
                        total4 += tamt4;
                    }

                }
                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable1"] = dtCurrentTable;

                Gridview5.DataSource = dtCurrentTable;
                Gridview5.DataBind();
                tab5();
                Gridview5.FooterRow.Cells[2].Text = "JUMLAH (RM) :";
                Gridview5.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                ((Label)Gridview5.FooterRow.Cells[7].FindControl("Label2")).Text = total1.ToString("C").Replace("RM", "").Replace("$", "");
                ((Label)Gridview5.FooterRow.Cells[9].FindControl("Label7")).Text = total2.ToString("C").Replace("RM", "").Replace("$", "");
                ((Label)Gridview5.FooterRow.Cells[11].FindControl("Label4")).Text = total3.ToString("C").Replace("RM", "").Replace("$", "");
                ((Label)Gridview5.FooterRow.Cells[12].FindControl("Label6")).Text = total4.ToString("C").Replace("RM", "").Replace("$", "");

                if (dtCurrentTable.Rows.Count == 2)
                {
                    Gridview5.FooterRow.Cells[1].Enabled = false;
                }
                else
                {
                    Gridview5.FooterRow.Cells[1].Enabled = true;
                }
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousDataBil();
    }

    private void AddNewRowToMohbIL()
    {
        int rowIndex = 0;
        decimal total1 = 0;
        decimal total2 = 0;
        decimal total3 = 0;
        decimal total4 = 0;
        if (ViewState["CurrentTable6"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable6"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    DropDownList ddl1 = (DropDownList)grdbilinv.Rows[rowIndex].Cells[1].FindControl("ddkodKat");
                    DropDownList ddl2 = (DropDownList)grdbilinv.Rows[rowIndex].Cells[2].FindControl("ddkodcre");
                    DropDownList ddl3 = (DropDownList)grdbilinv.Rows[rowIndex].Cells[3].FindControl("ddkodBil");
                    DropDownList ddl4 = (DropDownList)grdbilinv.Rows[rowIndex].Cells[4].FindControl("ddprobil");
                    TextBox box1 = (TextBox)grdbilinv.Rows[rowIndex].Cells[5].FindControl("TextBox55");
                    TextBox box2 = (TextBox)grdbilinv.Rows[rowIndex].Cells[6].FindControl("TextBox56");
                    TextBox box3 = (TextBox)grdbilinv.Rows[rowIndex].Cells[7].FindControl("Txtdis");
                    Label box4 = (Label)grdbilinv.Rows[rowIndex].Cells[8].FindControl("Label1");
                    CheckBox chk = (CheckBox)grdbilinv.Rows[rowIndex].Cells[9].FindControl("CheckBox1");
                    DropDownList box5 = (DropDownList)grdbilinv.Rows[rowIndex].Cells[10].FindControl("ddcukaioth");
                    Label box6 = (Label)grdbilinv.Rows[rowIndex].Cells[11].FindControl("Label10");
                    DropDownList box7 = (DropDownList)grdbilinv.Rows[rowIndex].Cells[12].FindControl("ddcukaiinv");
                    Label box8 = (Label)grdbilinv.Rows[rowIndex].Cells[13].FindControl("Label8");
                    Label box9 = (Label)grdbilinv.Rows[rowIndex].Cells[13].FindControl("Label5");
                    drCurrentRow = dtCurrentTable.NewRow();

                    dtCurrentTable.Rows[i - 1]["Column1"] = ddl1.SelectedItem.Value;
                    dtCurrentTable.Rows[i - 1]["Column2"] = ddl2.SelectedItem.Value;
                    dtCurrentTable.Rows[i - 1]["Column3"] = ddl3.SelectedItem.Value;
                    dtCurrentTable.Rows[i - 1]["Column4"] = ddl4.SelectedItem.Value;
                    dtCurrentTable.Rows[i - 1]["Column5"] = box1.Text;
                    dtCurrentTable.Rows[i - 1]["Column6"] = box2.Text;
                    dtCurrentTable.Rows[i - 1]["Column7"] = box3.Text;
                    dtCurrentTable.Rows[i - 1]["Column8"] = box4.Text;
                    dtCurrentTable.Rows[i - 1]["Column9"] = chk.Checked.ToString();
                    dtCurrentTable.Rows[i - 1]["Column10"] = box5.SelectedItem.Value;
                    dtCurrentTable.Rows[i - 1]["Column11"] = box6.Text;
                    dtCurrentTable.Rows[i - 1]["Column12"] = box7.SelectedItem.Value;
                    dtCurrentTable.Rows[i - 1]["Column13"] = box8.Text;
                    dtCurrentTable.Rows[i - 1]["Column14"] = box9.Text;
                    rowIndex++;
                    if (box4.Text != "")
                    {
                        decimal tamt1 = Convert.ToDecimal(box4.Text);
                        total1 += tamt1;
                    }

                    if (box6.Text != "")
                    {

                        decimal tamt2 = Convert.ToDecimal(box6.Text);
                        total2 += tamt2;

                    }
                    if (box8.Text != "")
                    {
                        decimal tamt3 = Convert.ToDecimal(box8.Text);
                        total3 += tamt3;
                    }
                    if (box9.Text != "")
                    {
                        decimal tamt4 = Convert.ToDecimal(box9.Text);
                        total4 += tamt4;
                    }

                }
                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable6"] = dtCurrentTable;

                grdbilinv.DataSource = dtCurrentTable;
                grdbilinv.DataBind();
                tab5();
                grdbilinv.FooterRow.Cells[2].Text = "JUMLAH (RM) :";
                Gridview5.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                ((Label)grdbilinv.FooterRow.Cells[7].FindControl("Label2")).Text = total1.ToString("C").Replace("RM", "").Replace("$", "");
                ((Label)grdbilinv.FooterRow.Cells[9].FindControl("Label7")).Text = total2.ToString("C").Replace("RM", "").Replace("$", "");
                ((Label)grdbilinv.FooterRow.Cells[11].FindControl("Label4")).Text = total3.ToString("C").Replace("RM", "").Replace("$", "");
                ((Label)grdbilinv.FooterRow.Cells[12].FindControl("Label6")).Text = total4.ToString("C").Replace("RM", "").Replace("$", "");

                if (dtCurrentTable.Rows.Count == 2)
                {
                    grdbilinv.FooterRow.Cells[1].Enabled = false;
                }
                else
                {
                    grdbilinv.FooterRow.Cells[1].Enabled = true;
                }
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousDataMohBil();
    }

    private void AddNewRowToGridcredit()
    {
        int rowIndex = 0;
        decimal total1 = 0;
        decimal total2 = 0;
        decimal total3 = 0;
        decimal total4 = 0;
        if (ViewState["CurrentTable3"] != null)
        {
            DataTable dtCurrentTable3 = (DataTable)ViewState["CurrentTable3"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable3.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable3.Rows.Count; i++)
                {
                    //extract the TextBox values
                    DropDownList ddl1 = (DropDownList)Gridview10.Rows[rowIndex].Cells[1].FindControl("ddkodcre");
                    TextBox box1 = (TextBox)Gridview10.Rows[rowIndex].Cells[2].FindControl("txtket");
                    TextBox box2 = (TextBox)Gridview10.Rows[rowIndex].Cells[3].FindControl("Txtdis");
                    CheckBox chk = (CheckBox)Gridview10.Rows[rowIndex].Cells[4].FindControl("Chkcre");
                    DropDownList box3 = (DropDownList)Gridview10.Rows[rowIndex].Cells[5].FindControl("ddkodgst");
                    Label box4 = (Label)Gridview10.Rows[rowIndex].Cells[6].FindControl("Label8");
                    DropDownList box5 = (DropDownList)Gridview10.Rows[rowIndex].Cells[7].FindControl("ddkodgstoth");
                    Label box6 = (Label)Gridview10.Rows[rowIndex].Cells[8].FindControl("Label10");
                    Label box7 = (Label)Gridview10.Rows[rowIndex].Cells[8].FindControl("Label5");

                    drCurrentRow = dtCurrentTable3.NewRow();

                    dtCurrentTable3.Rows[i - 1]["Column1"] = ddl1.SelectedValue;
                    dtCurrentTable3.Rows[i - 1]["Column2"] = box1.Text;
                    dtCurrentTable3.Rows[i - 1]["Column3"] = box2.Text;
                    dtCurrentTable3.Rows[i - 1]["Column4"] = chk.Checked.ToString();
                    dtCurrentTable3.Rows[i - 1]["Column5"] = box3.SelectedValue;
                    dtCurrentTable3.Rows[i - 1]["Column6"] = box4.Text;
                    dtCurrentTable3.Rows[i - 1]["Column7"] = box5.SelectedValue;
                    dtCurrentTable3.Rows[i - 1]["Column8"] = box6.Text;
                    dtCurrentTable3.Rows[i - 1]["Column9"] = box7.Text;
                    rowIndex++;
                    if (box2.Text != "")
                    {
                        decimal tamt1 = Convert.ToDecimal(box2.Text);
                        total1 += tamt1;
                    }

                    if (box4.Text != "")
                    {

                        decimal tamt2 = Convert.ToDecimal(box4.Text);
                        total2 += tamt2;

                    }
                    if (box6.Text != "")
                    {
                        decimal tamt3 = Convert.ToDecimal(box6.Text);
                        total3 += tamt3;
                    }
                    if (box7.Text != "")
                    {
                        decimal tamt4 = Convert.ToDecimal(box7.Text);
                        total4 += tamt4;
                    }

                }
                dtCurrentTable3.Rows.Add(drCurrentRow);
                ViewState["CurrentTable3"] = dtCurrentTable3;

                Gridview10.DataSource = dtCurrentTable3;
                Gridview10.DataBind();
                tab3();
                Gridview10.FooterRow.Cells[1].Text = "JUMLAH (RM) :";
                Gridview10.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                ((Label)Gridview10.FooterRow.Cells[2].FindControl("Label3")).Text = total1.ToString("C").Replace("RM", "").Replace("$", "");
                ((Label)Gridview10.FooterRow.Cells[5].FindControl("Label11")).Text = total3.ToString("C").Replace("RM", "").Replace("$", "");
                ((Label)Gridview10.FooterRow.Cells[7].FindControl("Label9")).Text = total2.ToString("C").Replace("RM", "").Replace("$", "");
                ((Label)Gridview10.FooterRow.Cells[8].FindControl("Label6")).Text = total4.ToString("C").Replace("RM", "").Replace("$", "");
                if (dtCurrentTable3.Rows.Count == 2)
                {
                    Gridview10.FooterRow.Cells[1].Enabled = false;
                }
                else
                {
                    Gridview10.FooterRow.Cells[1].Enabled = true;
                }
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousDatacre();
    }

    private void AddNewRowToGriddebit()
    {
        int rowIndex = 0;
        decimal total1 = 0;
        decimal total2 = 0;
        decimal total3 = 0;
        decimal total4 = 0;
        if (ViewState["CurrentTable4"] != null)
        {
            DataTable dtCurrentTable4 = (DataTable)ViewState["CurrentTable4"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable4.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable4.Rows.Count; i++)
                {
                    //extract the TextBox values
                    DropDownList ddl1 = (DropDownList)Gridview11.Rows[rowIndex].Cells[1].FindControl("ddkoddeb");
                    TextBox box1 = (TextBox)Gridview11.Rows[rowIndex].Cells[2].FindControl("txtket");
                    TextBox box2 = (TextBox)Gridview11.Rows[rowIndex].Cells[3].FindControl("Txtdis");
                    CheckBox chk = (CheckBox)Gridview11.Rows[rowIndex].Cells[4].FindControl("Chkdeb");
                    DropDownList box3 = (DropDownList)Gridview11.Rows[rowIndex].Cells[5].FindControl("ddgstdeboth");
                    Label box4 = (Label)Gridview11.Rows[rowIndex].Cells[6].FindControl("Label10");
                    DropDownList box5 = (DropDownList)Gridview11.Rows[rowIndex].Cells[7].FindControl("ddgstdeb");
                    Label box6 = (Label)Gridview11.Rows[rowIndex].Cells[8].FindControl("Label8");
                    Label box7 = (Label)Gridview11.Rows[rowIndex].Cells[8].FindControl("Label5");


                    drCurrentRow = dtCurrentTable4.NewRow();

                    dtCurrentTable4.Rows[i - 1]["Column1"] = ddl1.SelectedValue;
                    dtCurrentTable4.Rows[i - 1]["Column2"] = box1.Text;
                    dtCurrentTable4.Rows[i - 1]["Column3"] = box2.Text;
                    dtCurrentTable4.Rows[i - 1]["Column4"] = chk.Checked.ToString();
                    dtCurrentTable4.Rows[i - 1]["Column5"] = box3.SelectedValue;
                    dtCurrentTable4.Rows[i - 1]["Column6"] = box4.Text;
                    dtCurrentTable4.Rows[i - 1]["Column7"] = box5.SelectedValue;
                    dtCurrentTable4.Rows[i - 1]["Column8"] = box6.Text;
                    dtCurrentTable4.Rows[i - 1]["Column9"] = box7.Text;


                    rowIndex++;

                    if (box2.Text != "")
                    {
                        decimal tamt1 = Convert.ToDecimal(box2.Text);
                        total1 += tamt1;
                    }

                    if (box4.Text != "")
                    {

                        decimal tamt2 = Convert.ToDecimal(box4.Text);
                        total2 += tamt2;

                    }
                    if (box6.Text != "")
                    {
                        decimal tamt3 = Convert.ToDecimal(box6.Text);
                        total3 += tamt3;
                    }
                    if (box7.Text != "")
                    {
                        decimal tamt4 = Convert.ToDecimal(box7.Text);
                        total4 += tamt4;
                    }
                }
                dtCurrentTable4.Rows.Add(drCurrentRow);
                ViewState["CurrentTable4"] = dtCurrentTable4;

                Gridview11.DataSource = dtCurrentTable4;
                Gridview11.DataBind();
                tab4();
                Gridview11.FooterRow.Cells[1].Text = "JUMLAH (RM) :";
                Gridview11.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                ((Label)Gridview11.FooterRow.Cells[2].FindControl("Label3")).Text = total1.ToString("C").Replace("RM", "").Replace("$", "");
                ((Label)Gridview11.FooterRow.Cells[5].FindControl("Label11")).Text = total2.ToString("C").Replace("RM", "").Replace("$", "");
                ((Label)Gridview11.FooterRow.Cells[7].FindControl("Label9")).Text = total4.ToString("C").Replace("RM", "").Replace("$", "");
                ((Label)Gridview11.FooterRow.Cells[8].FindControl("Label6")).Text = total3.ToString("C").Replace("RM", "").Replace("$", "");
                if (dtCurrentTable4.Rows.Count == 2)
                {
                    Gridview11.FooterRow.Cells[1].Enabled = false;
                }
                else
                {
                    Gridview11.FooterRow.Cells[1].Enabled = true;
                }
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousDatadeb();
    }
    void akaun()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select kod_syarikat,nama_syarikat from KW_Profile_syarikat where cur_sts='1' and cur_sts='1'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddakaun.DataSource = dt;
            ddakaun.DataTextField = "nama_syarikat";
            ddakaun.DataValueField = "kod_syarikat";
            ddakaun.DataBind();
            ddakaun.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            string com1 = "select kod_bajet,nama_bajet from KW_Ref_kod_bajet inner join KW_Ref_Bajet s1 on s1.Ref_kod_bajet=kod_bajet where s1.Status='A'";
            SqlDataAdapter adpt1 = new SqlDataAdapter(com1, con);
            DataTable dt1 = new DataTable();
            adpt1.Fill(dt1);
            DropDownList3.DataSource = dt1;
            DropDownList3.DataTextField = "nama_bajet";
            DropDownList3.DataValueField = "kod_bajet";
            DropDownList3.DataBind();
            DropDownList3.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            string com2 = "select * from kw_kategory_kelulusan where status='A'";
            SqlDataAdapter adpt2 = new SqlDataAdapter(com2, con);
            DataTable dt2 = new DataTable();
            adpt2.Fill(dt2);
            DropDownList5.DataSource = dt2;
            DropDownList5.DataTextField = "kat_name";
            DropDownList5.DataValueField = "kat_cod";
            DropDownList5.DataBind();
            DropDownList5.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void cara()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select Jenis_bayaran,Jenis_bayaran_cd from KW_Jenis_Cara_bayaran";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddpvcara.DataSource = dt;
            ddpvcara.DataTextField = "Jenis_bayaran";
            ddpvcara.DataValueField = "Jenis_bayaran_cd";
            ddpvcara.DataBind();
            ddpvcara.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void Bank()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select Ref_Nama_akaun,Ref_kod_akaun from KW_Ref_Akaun_bank";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DDbank.DataSource = dt;
            DDbank.DataTextField = "Ref_Nama_akaun";
            DDbank.DataValueField = "Ref_kod_akaun";
            DDbank.DataBind();
            DDbank.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    void Pembakal()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select Ref_kod_akaun,ref_nama_syarikat from  kw_ref_pembekal";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            //ddbkepada.DataSource = dt;
            //ddbkepada.DataTextField = "ref_nama_syarikat";
            //ddbkepada.DataValueField = "Ref_jenis_akaun";
            //ddbkepada.DataBind();
            //ddbkepada.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            ddpela2.DataSource = dt;
            ddpela2.DataTextField = "Ref_nama_syarikat";
            ddpela2.DataValueField = "Ref_kod_akaun";
            ddpela2.DataBind();
            ddpela2.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            ddpela3.DataSource = dt;
            ddpela3.DataTextField = "Ref_nama_syarikat";
            ddpela3.DataValueField = "Ref_kod_akaun";
            ddpela3.DataBind();
            ddpela3.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void noper_pv()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select no_invois from KW_Pembayaran_invoisBil_item where status='B' group by no_invois";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            //ddbkepada.DataSource = dt;
            //ddbkepada.DataTextField = "ref_nama_syarikat";
            //ddbkepada.DataValueField = "Ref_jenis_akaun";
            //ddbkepada.DataBind();
            //ddbkepada.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            ddnoperpv.DataSource = dt;
            ddnoperpv.DataTextField = "no_invois";
            ddnoperpv.DataValueField = "no_invois";
            ddnoperpv.DataBind();
            ddnoperpv.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    void Bayaran()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select jenis_bayaran_cd,Jenis_bayaran from KW_Jenis_Cara_bayaran";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            //ddcbaya.DataSource = dt;
            //ddcbaya.DataTextField = "Jenis_bayaran";
            //ddcbaya.DataValueField = "jenis_bayaran_cd";
            //ddcbaya.DataBind();
            //ddcbaya.Items.Insert(0, new ListItem("--- PILIH ---", ""));


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
       
        Div2.Visible = true;
        Div3.Visible = false;
        kem_Button2.Visible = false;
        grdmohon.Visible = true;
        gridmohdup.Visible = false;
        grdbind.Visible = false;
        grdmohon.Enabled = true;
        sts.Visible = false;
        btnprintmoh.Visible = false;
        userid = Session["New"].ToString();
        level = Session["level"].ToString();

        if (level == "6")
        {
            txtid.Text = userid;
            txtid.Attributes.Add("disabled", "disabled");
        }
        else if (level == "5")
        {
            txtApr.Text = userid;
            txtApr.Attributes.Add("disabled", "disabled");
        }
        btnkem.Visible = false;
        Button2.Visible = true;
        Li2.Visible = false;
       
        reset();
        tab1();
        GetMohon();
        Bindbaucer();
        BindInvois();
        BindMohon();
        Bindcredit();
        Binddedit();
        DataTable get_pemo_info = new DataTable();
        get_pemo_info = Dbcon.Ora_Execute_table("SELECT Kk_username, KK_userid, ISNULL(s2.hr_unit_desc,'') as seksion,ISNULL(s1.hr_jaw_desc,'') as jawatan FROM KK_User_Login as table1 LEFT JOIN hr_staff_profile as table2 ON table1. KK_userid = table2.stf_staff_no left join Ref_hr_Jawatan s1 on s1.hr_jaw_Code=table2.stf_curr_post_cd left join Ref_hr_unit s2 on s2.hr_unit_Code = stf_curr_unit_cd where KK_userid='" + Session["New"].ToString() + "'");

        if (get_pemo_info.Rows.Count != 0)
        {
            txtid.Text = get_pemo_info.Rows[0]["Kk_username"].ToString();
            TextBox3.Text = get_pemo_info.Rows[0]["KK_userid"].ToString();
            TextBox4.Text = get_pemo_info.Rows[0]["seksion"].ToString();
            TextBox5.Text = get_pemo_info.Rows[0]["jawatan"].ToString();

        }

        txttarkihmo.Text = DateTime.Now.ToString("dd/MM/yyyy");
    }

    protected void new_entry(object sender, EventArgs e)
    {
        get_dd_value();
        ModalPopupExtender1.Show();
        
    }

    void get_dd_value()
    {
        pel_ver_id.Text = "0";
        if (DropDownList1.SelectedValue == "03")
        {
            tbl_name1 = "KW_Ref_Pelanggan";
            col_name1 = "pel";
            Label16.Text = "Pelenggan";
        }
        else if (DropDownList1.SelectedValue == "02")
        {
            tbl_name1 = "KW_Ref_Pembekal";
            col_name1 = "pem";
            Label16.Text = "Pembekal";
        }
    }
    //protected void sel_jenis(object sender, EventArgs e)
    //{
    //    TextBox10.Text = dd_akaun.SelectedValue;

    //}

    protected void pel_katcd_TextChanged(object sender, EventArgs e)
    {
        get_dd_value();
        ModalPopupExtender1.Show();
        DataTable sel_kat = new DataTable();
        sel_kat = DBCon.Ora_Execute_table("select * from "+ tbl_name1 + " where Ref_no_syarikat = '" + pel_TextBox5.Text + "'");
        if (sel_kat.Rows.Count != 0)
        {            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Pendaftaran Syarikat Kod Telah Wujud.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            pel_TextBox5.Text = "";
            pel_TextBox5.Focus();
        }
        else
        {
            pel_TextBox1.Focus();
        }
       
    }

  
    protected void btncarian_Click(object sender, EventArgs e)
    {
        if (txticno.Text != "")
        {

            DataTable dt = new DataTable();
            dt = Dbcon.Ora_Execute_table("select stf_name,stf_bank_acc_no,stf_bank_cd,R.Bank_Name from  hr_staff_profile S left join Ref_Nama_Bank R on S.stf_bank_cd=R.Bank_Code where stf_staff_no='" + txticno.Text + "'");
            if (dt.Rows.Count > 0)
            {
                txtname.Text = dt.Rows[0][0].ToString();
                txtbname.Text = dt.Rows[0][3].ToString();
                txtbno.Text = dt.Rows[0][1].ToString();
                txtname.Attributes.Add("disabled", "disabled");
                txtbname.Attributes.Add("disabled", "disabled");
                txtbname.Attributes.Add("disabled", "disabled");

            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Sila Masukkan No Kakitangan');", true);

        }


    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        //if (ddakaun.SelectedItem.Text != "--- PILIH ---")
        //{
       
            if (txtnoper.Text != "")
            {
                if (txttarkihmo.Text != "" && ddbkepada.SelectedValue != "" && DropDownList2.SelectedValue != "" && DropDownList1.SelectedValue != "")
                {
                string doc_dt = string.Empty;
                DateTime tDate = DateTime.ParseExact(txttarkihmo.Text, "dd/MM/yyyy", null);
                if (TextBox7.Text != "")
                {
                    DateTime tDate1 = DateTime.ParseExact(TextBox7.Text, "dd/MM/yyyy", null);
                    doc_dt = tDate1.ToString("yyyy-MM-dd");
                }

                userid = Session["New"].ToString();
                                        DataTable dt = new DataTable();

                                        DataTable gen_invkd = new DataTable();
                                        gen_invkd = Dbcon.Ora_Execute_table("select no_permohonan from KW_Pembayaran_mohon where no_permohonan='" + txtnoper.Text + "'");
                                        if (gen_invkd.Rows.Count > 0)
                                        {
                                            DataTable dtmb_db1 = Dbcon.Ora_Execute_table("select doc_type_cd,format as fmt,SUBSTRING(cur_format, 1, 10) as lfrmt1,SUBSTRING(cur_format, 11, 4) as lfrmt2,cur_format as cfmt from KW_Format_Nombor_rujukan where doc_type_cd='03' and Status='A'");
                                            if (dtmb_db1.Rows.Count != 0)
                                            {
                                                if (dtmb_db1.Rows[0]["cfmt"].ToString() == "")
                                                {
                                                    txtnoper_1.Text = dtmb_db1.Rows[0]["fmt"].ToString();                                                    
                                                }
                                                else
                                                {
                                                    int seqno = Convert.ToInt32(dtmb_db1.Rows[0]["lfrmt2"].ToString());
                                                    int newNumber = seqno + 1;
                                                    uniqueId = newNumber.ToString(dtmb_db1.Rows[0]["lfrmt1"].ToString() + "0000");
                                                    txtnoper_1.Text = uniqueId;
                                                }

                                            }
                                            else
                                            {
                                                DataTable dtmb_db2 = Dbcon.Ora_Execute_table("select   ISNULL(max(SUBSTRING(no_permohonan,13,3000)),'0') from KW_Pembayaran_mohon");
                                                if (dtmb_db2.Rows.Count > 0)
                                                {
                                                    int seqno = Convert.ToInt32(dtmb_db2.Rows[0][0].ToString());
                                                    int newNumber = seqno + 1;
                                                    uniqueId = newNumber.ToString("KTH-MHN" + " - " + DateTime.Now.ToString("yy") + " - " + "0000");
                                                    txtnoper_1.Text = uniqueId;                                                    

                                                }
                                                else
                                                {
                                                    int newNumber = Convert.ToInt32(uniqueId) + 1;
                                                    uniqueId = newNumber.ToString("KTH-MHN" + " - " + DateTime.Now.ToString("yy") + " - " + "0000");
                                                    txtnoper_1.Text = uniqueId;                                                    
                                                }
                                            }

                                        }
                                        else
                                        {
                                            txtnoper_1.Text = txtnoper.Text;
                                        }

                                    string Inssql_main = "insert into KW_Pembayaran_mohon (no_permohonan,ID_pemohon,tarkih_permohonan,Terma,Jenis_permohonan,jumlah,no_ruj,cara_bayaran,kat_penerima,tarkih_invois,PO_no,DO_no,crt_id,cr_dt,Status) values('" + txtnoper_1.Text + "','" + TextBox3.Text + "','" + tDate.ToString("yyyy-MM-dd") + "','" + dd_terma.SelectedValue + "','" + ddbkepada.SelectedValue + "','" + TextBox17.Text + "','" + txtname.Text + "','" + DropDownList2.SelectedValue + "','" + DropDownList1.SelectedValue + "','" + doc_dt + "','" + TextBox9.Text + "','" + TextBox14.Text + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','A')";
                                    Status = DBCon.Ora_Execute_CommamdText(Inssql_main);
                if (Status == "SUCCESS")
                {

                    foreach (GridViewRow g1 in grdmohon.Rows)
                    {
                        string kepda = (g1.FindControl("TextBox11") as DropDownList).SelectedValue;
                        string ket = (g1.FindControl("TextBox13") as TextBox).Text;
                        string projek = (g1.FindControl("dd_projek") as DropDownList).SelectedValue;
                        string bank = (g1.FindControl("dd_bank") as DropDownList).SelectedValue;
                        string no_akaun = (g1.FindControl("TextBox12") as TextBox).Text;
                        string jum = (g1.FindControl("TextBox53") as TextBox).Text;
                        string disk = (g1.FindControl("Txtdis") as TextBox).Text;
                        string jen_cukai = (g1.FindControl("ddcukaiinv") as DropDownList).SelectedValue;
                        string cukai_amt = (g1.FindControl("Label8") as Label).Text;
                        string ttotal = (g1.FindControl("Label5") as Label).Text;
                        
                        string Inssql = "insert into KW_Pembayaran_mohon_item (mhn_no_permohonan,mhn_byr_kepada,mhn_keterangan,mhn_bank,mhn_noacc_bank,mhn_amount,mhn_disc,mhn_jns_tax,mhn_tax,mhn_amount_bsr,mhn_projek_cd,crt_id,cr_dt,Status) values('" + txtnoper_1.Text + "','" + kepda + "','" + ket + "','" + bank + "','" + no_akaun + "','" + jum + "','" + disk + "','" + jen_cukai + "','" + cukai_amt + "','" + ttotal + "','" + projek + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','A')";
                        Status = Dbcon.Ora_Execute_CommamdText(Inssql);
                    }

                    if (Status == "SUCCESS")
                    {
                        DataTable dt_upd_format = new DataTable();
                        dt_upd_format = Dbcon.Ora_Execute_table("update KW_Format_Nombor_rujukan set cur_format='" + txtnoper_1.Text + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where doc_type_cd='03' and Status = 'A'");
                        BindMohon();
                        Div2.Visible = false;
                        Div3.Visible = true;
                        reset();
                        tab1();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Mohan Bayar Berjaya Dibuat.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    }
                }
                                   
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Mohon Bayar.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Untuk Akuan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        //}
    }

    protected void btnkem_Click(object sender, EventArgs e)
    {
        if (txtnoper.Text != "")
        {
            if (txttarkihmo.Text != "")
            {
                string semsts = string.Empty, Inssql_main =string.Empty, Inssql_item = string.Empty, tble_name = string.Empty;
                if (ddsts.SelectedValue != "")
                {
                    userid = Session["New"].ToString();
                    DataTable dt = new DataTable();
                    
                    if (ddsts.SelectedValue == "L")
                    {
                        semsts = ddsts.SelectedValue;
                    }
                    else
                    {
                        semsts = "";
                    }
                    int akaun_cnt = 0, j = 1;
                    foreach (GridViewRow g1 in gridmohdup.Rows)
                    {
                        string kod_akaun1 = (g1.FindControl("dd_bajet") as DropDownList).SelectedValue;
                        if (kod_akaun1 != "")
                        {
                            akaun_cnt = j;
                            j++;
                        }
                    }
                    if (akaun_cnt == gridmohdup.Rows.Count)
                    {
                        if (TextBox16.Text == "03")
                        {
                            Inssql_main = "update KW_Pembayaran_mohon  set kel_kategory='" + DropDownList5.SelectedValue + "',semak_id='" + userid + "',semak_dt='" + DateTime.Now.ToString("yyyy-MM-dd") + "',semak_sts='" + semsts + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd hh: mm:ss") + "' where no_permohonan='" + txtnoper.Text + "' and Status='A'";
                            tble_name = "KW_Pembayaran_mohon_item";
                        }
                        else if (TextBox16.Text == "01")
                        {
                            Inssql_main = "update KW_Pembayaran_invois  set kel_kategory='" + DropDownList5.SelectedValue + "',semak_id='" + userid + "',semak_dt='" + DateTime.Now.ToString("yyyy-MM-dd") + "',semak_sts='" + semsts + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd hh: mm:ss") + "' where no_invois='" + txtnoper.Text + "' and Status='A'";
                            tble_name = "KW_Pembayaran_invoisBil_item";
                        }
                        else if (TextBox16.Text == "05")
                        {
                            Inssql_main = "update KW_Pembayaran_Credit  set kel_kategory='" + DropDownList5.SelectedValue + "',semak_id='" + userid + "',semak_dt='" + DateTime.Now.ToString("yyyy-MM-dd") + "',semak_sts='" + semsts + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd hh: mm:ss") + "' where no_Rujukan='" + txtnoper.Text + "' and Status='A'";
                            tble_name = "KW_Pembayaran_Credit_item";
                        }
                        else if (TextBox16.Text == "06")
                        {
                            Inssql_main = "update KW_Pembayaran_Dedit  set kel_kategory='" + DropDownList5.SelectedValue + "',semak_id='" + userid + "',semak_dt='" + DateTime.Now.ToString("yyyy-MM-dd") + "',semak_sts='" + semsts + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd hh: mm:ss") + "' where no_Rujukan='" + txtnoper.Text + "' and Status='A'";
                            tble_name = "KW_Pembayaran_Dedit_item";
                        }
                        else if (TextBox16.Text == "09")
                        {
                            Inssql_main = "update kw_penerimaan_invois  set kel_kategory='" + DropDownList5.SelectedValue + "',semak_id='" + userid + "',semak_dt='" + DateTime.Now.ToString("yyyy-MM-dd") + "',semak_sts='" + semsts + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd hh: mm:ss") + "' where no_invois='" + txtnoper.Text + "' and Status='A'";
                            tble_name = "kw_penerimaan_invois_item";
                        }
                        else if (TextBox16.Text == "10")
                        {
                            Inssql_main = "update KW_Penerimaan_Credit  set kel_kategory='" + DropDownList5.SelectedValue + "',semak_id='" + userid + "',semak_dt='" + DateTime.Now.ToString("yyyy-MM-dd") + "',semak_sts='" + semsts + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd hh: mm:ss") + "' where no_notakredit='" + txtnoper.Text + "' and Status='A'";
                            tble_name = "KW_Penerimaan_Credit_item";
                        }
                        else if (TextBox16.Text == "11")
                        {
                            Inssql_main = "update KW_Penerimaan_Debit  set kel_kategory='" + DropDownList5.SelectedValue + "',semak_id='" + userid + "',semak_dt='" + DateTime.Now.ToString("yyyy-MM-dd") + "',semak_sts='" + semsts + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd hh: mm:ss") + "' where no_notadebit='" + txtnoper.Text + "' and Status='A'";
                            tble_name = "KW_Penerimaan_Debit_item";
                        }
                        else
                        {
                            Inssql_main = "update KW_jurnal_inter  set kel_kategory='" + DropDownList5.SelectedValue + "',semak_id='" + userid + "',semak_dt='" + DateTime.Now.ToString("yyyy-MM-dd") + "',semak_sts='" + semsts + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd hh: mm:ss") + "' where no_permohonan='" + txtnoper.Text + "' and Status='A'";
                            tble_name = "KW_jurnal_inter_items";
                        }
                        Status = DBCon.Ora_Execute_CommamdText(Inssql_main);
                        if (Status == "SUCCESS")
                        {
                            foreach (GridViewRow g1 in gridmohdup.Rows)
                            {
                                string get_Id = (g1.FindControl("Label24") as Label).Text;
                                string get_bajet_cd = (g1.FindControl("dd_bajet") as DropDownList).SelectedValue;

                                Inssql_item = "update " + tble_name + "  set kod_bajet='" + get_bajet_cd + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd hh: mm:ss") + "' where Id='" + get_Id + "' and Status='A'";
                                Status = DBCon.Ora_Execute_CommamdText(Inssql_item);
                            }

                            BindMohon();
                            Div2.Visible = false;
                            Div3.Visible = true;
                            reset();
                            tab1();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Semakan Kewangan Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Kod Bajet.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Permohonan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    void reset()
    {
        //tab1
        txtnoper_1.Text = "";
        //mb_tab1.Visible = false;
        DropDownList1.SelectedValue = "";
        DropDownList2.SelectedValue = "";
        DropDownList3.SelectedValue = "";
        DropDownList5.SelectedValue = "";
        ddsts.SelectedValue = "";
        dd_terma.SelectedValue = "";
        Button19.Visible = false;
        //mak_dok_shw.Visible = false;
        ddakaun.SelectedIndex = 0;
        ddakaun.Attributes.Remove("disabled");


        txttarkihmo.Text = DateTime.Now.ToString("yyyy-MM-dd");
        //txttarkihmo.Attributes.Remove("disabled");

        ddbkepada.SelectedIndex = 0;
        ddbkepada.Attributes.Remove("disabled");

        txticno.Text = "";
        txticno.Attributes.Remove("disabled");

        txtname.Text = "";
        txtname.Attributes.Remove("disabled");

        txtbname.Text = "";
        txtbname.Attributes.Remove("disabled");

        txtbno.Text = "";
        txtbno.Attributes.Remove("disabled");



        //txtterma.Text = "";
        //txtterma.Attributes.Remove("disabled");

        txtjenis.Text = "";
        txtjenis.Attributes.Remove("disabled");




        ddstatus.SelectedIndex = 0;
        ddstatus.Attributes.Remove("disabled");

        //txtcatatan.Text = "";
        //txtcatatan.Attributes.Remove("disabled");

        btnprintmoh.Visible = false;

        TextBox17.Text = "";
        Session["dbtno"] = "";
        jurnal_show.Visible = false;
        jurnal_show2.Visible = false;
        TextBox2.Text = "";
        SetInitialRowmoh();
        Session["permohon_no"] = "";
        //tab2


        Session["pvno"] = "";
        ddakaun.SelectedIndex = 0;
        ddakaun.Attributes.Remove("disabled");

        dddata.SelectedValue = "--- PILIH ---";
        ddnoper.SelectedValue = "";

        ddnoperpv.SelectedValue = "";
        ddnoperpv.Attributes.Remove("disabled");

        TXTPVNAME.Text = "";

        txttarpv.Text = "";
        txttarpv.Attributes.Remove("disabled");

        ddnoperpv.SelectedValue = "";
        ddnoperpv.Attributes.Remove("disabled");

        ddpvkat.SelectedIndex = 0;
        ddpvkat.Attributes.Remove("disabled");


        ddpvkod.Attributes.Remove("disabled");



        ddpvcara.SelectedIndex = 0;
        ddpvcara.Attributes.Remove("disabled");

        DDbank.SelectedIndex = 0;
        DDbank.Attributes.Remove("disabled");

        txtpvnocek.Text = "";
        txtpvnocek.Attributes.Remove("disabled");

        txtpvterma.Text = "";
        txtpvterma.Attributes.Remove("disabled");

        txttarpv.Text = "";
        txttarpv.Attributes.Remove("disabled");

        ddpvstatus.SelectedIndex = 0;
        ddpvstatus.Attributes.Remove("disabled");

        grdpvinv.Visible = false;

        Grdpvinvdup.Visible = false;
        grdbilinv.Visible = false;
        SetInitialRowMohBil();
        //tab3

        pv_tab3.Visible = false;
        txtpvno_1.Text = "";

        ddakaun.SelectedIndex = 0;
        ddakaun.Attributes.Remove("disabled");

        txttcre.Text = "";
        txttcre.Attributes.Remove("disabled");

        ddpela2.SelectedIndex = 0;
        ddpela2.Attributes.Remove("disabled");

        txttcinvois.Text = "";
        txttcinvois.Attributes.Remove("disabled");

        ddinv.SelectedValue = "";
        ddinv.Attributes.Remove("disabled");

        ddpro1.SelectedIndex = 0;
        ddpro1.Attributes.Remove("disabled");

        TextBox18.Text = "";
        ddinvday.SelectedIndex = 0; ;
        ddinvday.Attributes.Remove("disabled");
        ddpvkod.SelectedValue = "";

        ddnoperpv_PV1.Text = "";
        ddinvday.SelectedIndex = 0; ;
        ddinvday.Attributes.Remove("disabled");
        ddpvkod.SelectedValue = "";

        ddnoperpv_PV1.Text = "";
        ddpvstatus.Visible = true;
        ddpvstatus1.Visible = false;
        Button7.Visible = true;
        Button17.Visible = false;
        Button18.Visible = false;

        //SetInitialRow_payment_vocher();
        grdpvinv_new.Visible = false;
        noinv_new.Visible = true;
        SetInitialRowCre();

        //tab4 

        kredit_tab4.Visible = false;
        txtnoruju_1.Text = "";

        ddakaun.SelectedIndex = 0;
        ddakaun.Attributes.Remove("disabled");


        ddpela3.SelectedIndex = 0;
        ddpela3.Attributes.Remove("disabled");

        txtdtinvois.Text = "";
        txtdtinvois.Attributes.Remove("disabled");


        ddpro2.SelectedIndex = 0;
        ddpro2.Attributes.Remove("disabled");

        TextBox1.Text = "";


        SetInitialRowDeb();

        //Tab5
        txtnoruj2_2.Text = "";
        debit_tab5.Visible = false;
        ddakaun.SelectedIndex = 0;
        ddakaun.Attributes.Remove("disabled");

        txtnoperbil.Visible = false;
        ddnoper.Visible = true;

        ddinv2.SelectedValue = "";
        ddinv2.Attributes.Remove("disabled");

        ddinvday2.SelectedIndex = 0;
        ddinvday2.Attributes.Remove("disabled");


        txtdeb.Text = "";
        txtdeb.Attributes.Remove("disabled");

        dddata.Attributes.Remove("disabled");


        ddkataka.Attributes.Remove("disabled");
        //ddkataka.SelectedIndex = 0;
        ddkataka.SelectedValue = "--- PILIH ---";

        bind_carta();
        //ddkodaka.SelectedValue ="";
        ddkodaka.Attributes.Remove("disabled");


        txtnoperbil.Text = "";
        txtnoperbil.Attributes.Remove("disabled");

        txttmb.Text = "";
        txttmb.Attributes.Remove("disabled");

        txttinvbil.Text = "";
        txttinvbil.Attributes.Remove("disabled");

        txrinvbil.Text = "";
        txrinvbil.Attributes.Remove("disabled");

        txtinterma.Text = "";
        txtinterma.Attributes.Remove("disabled");

        kataka.Visible = false;
        kodaka.Visible = false;
        invbil.Visible = false;

        TextBox37.Text = "";

        grdbilinv.Visible = false;
        grdbilinv1.Visible = false;
        grdbilinvdub.Visible = false;
        Gridview5.Visible = false;
        grdvie5dup.Visible = false;

    }

    // tab 3 events

    private void SetInitialRowCre()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;


        dt.Columns.Add(new DataColumn("Column1", typeof(string)));
        dt.Columns.Add(new DataColumn("Column2", typeof(string)));
        dt.Columns.Add(new DataColumn("Column3", typeof(string)));
        dt.Columns.Add(new DataColumn("Column4", typeof(string)));
        dt.Columns.Add(new DataColumn("Column5", typeof(string)));
        dt.Columns.Add(new DataColumn("Column6", typeof(string)));
        dt.Columns.Add(new DataColumn("Column7", typeof(string)));
        dt.Columns.Add(new DataColumn("Column8", typeof(string)));
        dt.Columns.Add(new DataColumn("Column9", typeof(string)));


        dr = dt.NewRow();

        dr["Column1"] = string.Empty;
        dr["Column2"] = string.Empty;
        dr["Column3"] = string.Empty;
        dr["Column4"] = string.Empty;
        dr["Column5"] = string.Empty;
        dr["Column6"] = string.Empty;
        dr["Column7"] = string.Empty;
        dr["Column8"] = string.Empty;
        dr["Column9"] = string.Empty;

        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentTable3"] = dt;

        Gridview10.DataSource = dt;
        Gridview10.DataBind();

    }

    private void SetInitialRowDeb()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;


        dt.Columns.Add(new DataColumn("Column1", typeof(string)));
        dt.Columns.Add(new DataColumn("Column2", typeof(string)));
        dt.Columns.Add(new DataColumn("Column3", typeof(string)));
        dt.Columns.Add(new DataColumn("Column4", typeof(string)));
        dt.Columns.Add(new DataColumn("Column5", typeof(string)));
        dt.Columns.Add(new DataColumn("Column6", typeof(string)));
        dt.Columns.Add(new DataColumn("Column7", typeof(string)));
        dt.Columns.Add(new DataColumn("Column8", typeof(string)));
        dt.Columns.Add(new DataColumn("Column9", typeof(string)));

        dr = dt.NewRow();

        dr["Column1"] = string.Empty;
        dr["Column2"] = string.Empty;
        dr["Column3"] = string.Empty;
        dr["Column4"] = string.Empty;
        dr["Column5"] = string.Empty;
        dr["Column6"] = string.Empty;
        dr["Column7"] = string.Empty;
        dr["Column8"] = string.Empty;
        dr["Column9"] = string.Empty;


        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentTable4"] = dt;

        Gridview11.DataSource = dt;
        Gridview11.DataBind();

    }

    // new set initial row

    private void SetInitialRow_payment_vocher()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;

        dt.Columns.Add(new DataColumn("Column1", typeof(string)));
        dt.Columns.Add(new DataColumn("Column2", typeof(string)));
        dt.Columns.Add(new DataColumn("Column3", typeof(string)));
        dt.Columns.Add(new DataColumn("Column4", typeof(string)));
        dt.Columns.Add(new DataColumn("Column5", typeof(string)));
        dt.Columns.Add(new DataColumn("Column6", typeof(string)));
        dt.Columns.Add(new DataColumn("Column7", typeof(string)));
        dt.Columns.Add(new DataColumn("Column8", typeof(string)));
        dt.Columns.Add(new DataColumn("Column9", typeof(string)));
        dt.Columns.Add(new DataColumn("Column10", typeof(string)));

        dr = dt.NewRow();

        dr["Column1"] = string.Empty;
        dr["Column2"] = string.Empty;
        dr["Column3"] = string.Empty;
        dr["Column4"] = string.Empty;
        dr["Column5"] = string.Empty;
        dr["Column6"] = string.Empty;
        dr["Column7"] = string.Empty;
        dr["Column8"] = string.Empty;
        dr["Column9"] = string.Empty;
        dr["Column10"] = string.Empty;

        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentTable5"] = dt;
        grdpvinv_new.DataSource = dt;
        grdpvinv_new.DataBind();
    }

    protected void ButtonAdd_Click_pvnew(object sender, EventArgs e)
    {
        AddNewRowToGrid_pvnew();
    }

    private void AddNewRowToGrid_pvnew()
    {
        int rowIndex = 0;
        decimal total1 = 0;
        decimal total2 = 0;
        decimal total3 = 0;
        decimal total4 = 0;

        if (ViewState["CurrentTable5"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable5"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    TextBox bx1 = (TextBox)grdpvinv_new.Rows[rowIndex].Cells[1].FindControl("gn_Label08");
                    TextBox bx2 = (TextBox)grdpvinv_new.Rows[rowIndex].Cells[2].FindControl("gn_Label19");
                    TextBox bx3 = (TextBox)grdpvinv_new.Rows[rowIndex].Cells[3].FindControl("gn_Label09");
                    TextBox bx4 = (TextBox)grdpvinv_new.Rows[rowIndex].Cells[4].FindControl("gn_Label10");
                    TextBox bx5 = (TextBox)grdpvinv_new.Rows[rowIndex].Cells[5].FindControl("gn_Label11");
                    TextBox bx6 = (TextBox)grdpvinv_new.Rows[rowIndex].Cells[6].FindControl("gn_Label12");
                    TextBox bx7 = (TextBox)grdpvinv_new.Rows[rowIndex].Cells[7].FindControl("gn_Label13");
                    TextBox bx8 = (TextBox)grdpvinv_new.Rows[rowIndex].Cells[8].FindControl("gn_Label14");
                    TextBox bx9 = (TextBox)grdpvinv_new.Rows[rowIndex].Cells[9].FindControl("gn_Label15");
                    TextBox bx10 = (TextBox)grdpvinv_new.Rows[rowIndex].Cells[9].FindControl("gn_txtpay");

                    drCurrentRow = dtCurrentTable.NewRow();

                    dtCurrentTable.Rows[i - 1]["Column1"] = bx1.Text;
                    dtCurrentTable.Rows[i - 1]["Column2"] = bx2.Text;
                    dtCurrentTable.Rows[i - 1]["Column3"] = bx3.Text;
                    dtCurrentTable.Rows[i - 1]["Column4"] = bx4.Text;
                    dtCurrentTable.Rows[i - 1]["Column5"] = bx5.Text;
                    dtCurrentTable.Rows[i - 1]["Column6"] = bx6.Text;
                    dtCurrentTable.Rows[i - 1]["Column7"] = bx7.Text;
                    dtCurrentTable.Rows[i - 1]["Column8"] = bx8.Text;
                    dtCurrentTable.Rows[i - 1]["Column9"] = bx9.Text;
                    dtCurrentTable.Rows[i - 1]["Column10"] = bx10.Text;


                    rowIndex++;



                }
                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable5"] = dtCurrentTable;

                grdpvinv_new.DataSource = dtCurrentTable;
                grdpvinv_new.DataBind();
                tab2();

            }

        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousData_pvnew();
    }

    private void SetPreviousData_pvnew()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable5"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable5"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TextBox bx1 = (TextBox)grdpvinv_new.Rows[rowIndex].Cells[1].FindControl("gn_Label08");
                    TextBox bx2 = (TextBox)grdpvinv_new.Rows[rowIndex].Cells[2].FindControl("gn_Label19");
                    TextBox bx3 = (TextBox)grdpvinv_new.Rows[rowIndex].Cells[3].FindControl("gn_Label09");
                    TextBox bx4 = (TextBox)grdpvinv_new.Rows[rowIndex].Cells[4].FindControl("gn_Label10");
                    TextBox bx5 = (TextBox)grdpvinv_new.Rows[rowIndex].Cells[5].FindControl("gn_Label11");
                    TextBox bx6 = (TextBox)grdpvinv_new.Rows[rowIndex].Cells[6].FindControl("gn_Label12");
                    TextBox bx7 = (TextBox)grdpvinv_new.Rows[rowIndex].Cells[7].FindControl("gn_Label13");
                    TextBox bx8 = (TextBox)grdpvinv_new.Rows[rowIndex].Cells[8].FindControl("gn_Label14");
                    TextBox bx9 = (TextBox)grdpvinv_new.Rows[rowIndex].Cells[9].FindControl("gn_Label15");
                    TextBox bx10 = (TextBox)grdpvinv_new.Rows[rowIndex].Cells[9].FindControl("gn_txtpay");

                    bx1.Text = dt.Rows[i]["Column1"].ToString();
                    bx2.Text = dt.Rows[i]["Column2"].ToString();
                    bx3.Text = dt.Rows[i]["Column3"].ToString();
                    bx4.Text = dt.Rows[i]["Column4"].ToString();
                    bx5.Text = dt.Rows[i]["Column5"].ToString();
                    bx6.Text = dt.Rows[i]["Column6"].ToString();
                    bx7.Text = dt.Rows[i]["Column7"].ToString();
                    bx8.Text = dt.Rows[i]["Column8"].ToString();
                    bx9.Text = dt.Rows[i]["Column9"].ToString();
                    bx10.Text = dt.Rows[i]["Column10"].ToString();


                    rowIndex++;
                }
            }
        }
    }

    protected void load_bajet(object sender, EventArgs e)
    {
        lod_bajet();
    }
    void lod_bajet()
    {
        string ss1 = string.Empty, ss2 = string.Empty;
        //int selRowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
        //DropDownList ddbajet = (DropDownList)gridmohdup.Rows[selRowIndex].FindControl("dd_bajet");
        int count = 0, count1 = 0;
        for (int i = 0; i < gridmohdup.Rows.Count; i++)
        {
            String ddbajet = (gridmohdup.Rows[i].FindControl("dd_bajet") as DropDownList).SelectedValue;
            if (ddbajet != "")
            {
                count1 = 1;
                count += count1;
            }
        }
        for (int i = 0; i < gridmohdup.Rows.Count; i++)
        {
            String ddbajet = (gridmohdup.Rows[i].FindControl("dd_bajet") as DropDownList).SelectedValue;
            if (ddbajet != "")
            {
                if (i < (count - 1))
                {
                    ss1 = ddbajet + ",";
                }
                else
                {
                    ss1 = ddbajet;
                }
                ss2 += ss1;
            }

        }
        Session["bajt_det_cd"] = ss2;

        DataTable dt1 = new DataTable();
        dt1 = Dbcon.Ora_Execute_table("SELECT nama_bajet, Ref_jumlah_bajet,isnull(cast(Ref_used_bajet as money),'0.00') Ref_used_bajet, sum(isnull(cast(Ref_jumlah_bajet as money),'0.00') - isnull(cast(Ref_used_bajet as money),'0.00')) as baki FROM KW_Ref_kod_bajet as table1 LEFT JOIN  KW_Ref_Bajet as table2 ON table1. kod_bajet = table2.Ref_kod_bajet WHERE kod_bajet IN ('" + Session["bajt_det_cd"].ToString().Replace(",","','") + "') and '" + str_sdt1 + "' BETWEEN Ref_tk_mula AND Ref_tk_akhir group by nama_bajet, Ref_jumlah_bajet, Ref_used_bajet");
        Gridview1.DataSource = dt1;
        Gridview1.DataBind();
    }
    protected void lblSubbind_Click(object sender, EventArgs e)
    {
        string query1 = string.Empty;
        LinkButton btn = (LinkButton)sender;
        string[] CommadArgument = btn.CommandArgument.Split(',');
        CommandArgument1 = CommadArgument[0];
        Button2.Visible = false;
        //kem_Button2.Visible = true;
        //gridmohdup.Enabled = false;
        Session["bajt_det_cd"] = "";
        mb_tab1.Visible = true;
        sts.Visible = true;
        Div2.Visible = true;
        Div3.Visible = false;
        DataTable dt1 = new DataTable();
        grdmohon.Visible = false;
        DataTable dt = new DataTable();

        if (CommadArgument[1] == "01")
        {
            dt = Dbcon.Ora_Execute_table("select kel_kategory,kod_bajet,no_invois as no_permohonan,perkara Catatan,id_profile_syarikat,semak_sts,semak_id,Format(semak_dt,'dd/MM/yyyy') semakdt,'' ID_pemohon,PO_no,DO_no, Overall jumlah,Format(tarikh_invois,'dd/MM/yyyy') as tk_invois, Format(tarkih_mohon,'dd/MM/yyyy') as tk_mohon,'' Jenis_permohonan,PO_no as no_ruj,cara_bayaran,kat_penerima,Terma from KW_Pembayaran_invois where no_invois='" + CommandArgument1 + "' and Status='A'");
        }
        else if (CommadArgument[1] == "03")
        {
            dt = Dbcon.Ora_Execute_table("select kel_kategory,kod_bajet,no_permohonan,perkara Catatan,id_profile_syarikat,semak_sts,semak_id,Format(semak_dt,'dd/MM/yyyy') semakdt,ID_pemohon,PO_no,DO_no,jumlah,Format(tarkih_invois,'dd/MM/yyyy') as tk_invois,Format(tarkih_permohonan,'dd/MM/yyyy') as tk_mohon,Jenis_permohonan,no_ruj,cara_bayaran,kat_penerima,Terma from KW_Pembayaran_mohon where no_permohonan='" + CommandArgument1 + "' and Status='A'");
        }
        else if (CommadArgument[1] == "05")
        {
            dt = Dbcon.Ora_Execute_table("select kel_kategory,'' kod_bajet,'' PO_no,'' DO_no,'' no_ruj,'' Jenis_permohonan,'' cara_bayaran,'' ID_pemohon,no_Rujukan no_permohonan,perkera Catatan,id_profile_syarikat,semak_sts,semak_id,Format(semak_dt,'dd/MM/yyyy') semakdt,overall as jumlah,'' as tk_invois,Format(tarkih_mohon,'dd/MM/yyyy') as tk_mohon,'02' kat_penerima,Terma from KW_Pembayaran_Credit where no_Rujukan='" + CommandArgument1 + "' and Status='A'");
        }
        else if (CommadArgument[1] == "06")
        {
            dt = Dbcon.Ora_Execute_table("select kel_kategory,'' kod_bajet,'' PO_no,'' DO_no,'' no_ruj,'' Jenis_permohonan,'' cara_bayaran,'' ID_pemohon,no_Rujukan no_permohonan,perkera Catatan,id_profile_syarikat,semak_sts,semak_id,Format(semak_dt,'dd/MM/yyyy') semakdt,overall as jumlah,'' as tk_invois,Format(tarkih_mohon,'dd/MM/yyyy') as tk_mohon,'02' kat_penerima,Terma from KW_Pembayaran_Dedit where no_Rujukan='" + CommandArgument1 + "' and Status='A'");
        }
        else if (CommadArgument[1] == "09")
        {
            dt = Dbcon.Ora_Execute_table("select kel_kategory,'' kod_bajet,no_invois as no_permohonan,perkera Catatan,id_profile_syarikat,semak_sts,semak_id,Format(semak_dt,'dd/MM/yyyy') semakdt,'' ID_pemohon,PO_no,DO_no, Overall jumlah,Format(tarikh_invois,'dd/MM/yyyy') as tk_invois, Format(tarikh_invois,'dd/MM/yyyy') as tk_mohon,'' Jenis_permohonan,PO_no as no_ruj,'' cara_bayaran,'03' kat_penerima,Terma from kw_penerimaan_invois where no_invois='" + CommandArgument1 + "' and Status='A'");
        }
        else if (CommadArgument[1] == "10")
        {
            dt = Dbcon.Ora_Execute_table("select kel_kategory,'' kod_bajet,'' PO_no,'' DO_no,'' no_ruj,'' Jenis_permohonan,'' cara_bayaran,'' ID_pemohon,no_notakredit no_permohonan,perkara Catatan,id_profile_syarikat,semak_sts,semak_id,Format(semak_dt,'dd/MM/yyyy') semakdt,overall as jumlah,'' as tk_invois,Format(tarikh_kredit,'dd/MM/yyyy') as tk_mohon,'03' kat_penerima,Terma from KW_Penerimaan_Credit where no_notakredit='" + CommandArgument1 + "' and Status='A'");
        }
        else if (CommadArgument[1] == "11")
        {
            dt = Dbcon.Ora_Execute_table("select kel_kategory,'' kod_bajet,'' PO_no,'' DO_no,'' no_ruj,'' Jenis_permohonan,'' cara_bayaran,'' ID_pemohon,no_notadebit no_permohonan,perkara Catatan,id_profile_syarikat,semak_sts,semak_id,Format(semak_dt,'dd/MM/yyyy') semakdt,overall as jumlah,'' as tk_invois,Format(tarikh_debit,'dd/MM/yyyy') as tk_mohon,'03' kat_penerima,Terma from KW_Penerimaan_Debit where no_notadebit='" + CommandArgument1 + "' and Status='A'");
        }
        else
        {
            dt = Dbcon.Ora_Execute_table("select kel_kategory,'' kod_bajet,'' PO_no,'' DO_no,no_Rujukan no_ruj,'' Jenis_permohonan,'' cara_bayaran,'' ID_pemohon,no_permohonan,perkara Catatan,id_profile_syarikat,semak_sts,semak_id,Format(semak_dt,'dd/MM/yyyy') semakdt,overall as jumlah,'' as tk_invois,Format(tarikh_lulus,'dd/MM/yyyy') as tk_mohon,'03' kat_penerima,Terma from KW_jurnal_inter where no_permohonan='" + CommandArgument1 + "' and Status='A'");
        }
        //tab1();
        if (dt.Rows.Count != 0)
        {
            TextBox16.Text = CommadArgument[1];
            if (dt.Rows[0]["semak_sts"].ToString() == "L")
            {
                kem_Button2.Visible = false;
                ddsts.SelectedValue = dt.Rows[0]["semak_sts"].ToString();
            }
            else
            {
                kem_Button2.Visible = true;
            }
            DropDownList3.SelectedValue = dt.Rows[0]["kod_bajet"].ToString();
           
            //TextBox29.Text = dt.Rows[0]["Catatan"].ToString();


            TextBox3.Text = dt.Rows[0]["ID_pemohon"].ToString();
            txttarkihmo.Text = dt.Rows[0]["tk_mohon"].ToString();
            ddbkepada.SelectedValue = dt.Rows[0]["Jenis_permohonan"].ToString();
            txtname.Text = dt.Rows[0]["no_ruj"].ToString();
            txtnoper.Text = dt.Rows[0]["no_permohonan"].ToString();
            TextBox19.Text = dt.Rows[0]["Catatan"].ToString();
            DropDownList2.SelectedValue = dt.Rows[0]["cara_bayaran"].ToString();
            DropDownList1.SelectedValue = dt.Rows[0]["kat_penerima"].ToString();
            DropDownList5.SelectedValue = dt.Rows[0]["kel_kategory"].ToString();
            dd_terma.SelectedValue = dt.Rows[0]["Terma"].ToString();
            if(DropDownList1.SelectedValue != "01")
            {
                //mak_dok_shw.Visible = true;
                TextBox7.Text = dt.Rows[0]["tk_invois"].ToString();
                TextBox9.Text = dt.Rows[0]["PO_no"].ToString();
                TextBox14.Text = dt.Rows[0]["DO_no"].ToString();
            }
            else
            {
                //mak_dok_shw.Visible = false;
            }
            TextBox17.Text = double.Parse(dt.Rows[0]["jumlah"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
            if (CommadArgument[1] == "01")
            {
                query1 = "select s1.kod_bajet,s1.Id,m1.Bayar_kepada as kepada,UPPER(s1.keterangan) as Keteragan,s2.Ref_Projek_name as projek,s3.Bank_Name as bank,s1.nv_noacc_bank as acc_no, s1.Overall as jumlah, discount as Disk  , "
                           + " UPPER(ISNULL(s4.Ref_nama_cukai,'')) as jen_cukai, s1.gstjumlah as cukai_amt,jumlah as amount,unit as unit,m1.kat_penerima as kat,'1' as qty   from KW_Pembayaran_invoisBil_item s1  "
                           + " left join KW_Pembayaran_invois m1 on m1.no_invois=s1.no_invois left join KW_Ref_Projek s2 on s2.Ref_Projek_code=m1.project_kod  "
                           + " left join Ref_Nama_Bank s3 on s3.Bank_Code=s1.inv_bank  "
                           + " left join KW_Ref_Tetapan_cukai s4 on s4.Ref_kod_cukai=s1.tax where s1.no_invois='" + CommandArgument1 + "' and s1.Status='A'";
            }
            else if (CommadArgument[1] == "03")
            {
                query1 = "select s1.kod_bajet,s1.Id,mhn_byr_kepada as kepada,UPPER(mhn_keterangan) as Keteragan,s2.Ref_Projek_name as projek,s3.Bank_Name as bank,s1.mhn_noacc_bank as acc_no,mhn_amount_bsr as jumlah, mhn_disc as Disk "
                          + " ,UPPER(ISNULL(s4.Ref_nama_cukai,'')) as jen_cukai, s1.mhn_tax as cukai_amt, (mhn_amount - mhn_disc) as amount,s1.mhn_amount as unit,m1.kat_penerima as kat,'1' as qty "
                          + " from KW_Pembayaran_mohon_item s1 left join KW_Pembayaran_mohon m1 on m1.no_permohonan =s1.mhn_no_permohonan left join KW_Ref_Projek s2 on s2.Ref_Projek_code=s1.mhn_projek_cd "
                          + " left join Ref_Nama_Bank s3 on s3.Bank_Code=s1.mhn_bank "
                          + " left join KW_Ref_Tetapan_cukai s4 on s4.Ref_kod_cukai=s1.mhn_jns_tax  where mhn_no_permohonan='" + CommandArgument1 + "' and s1.Status='A'";
            }
            else if (CommadArgument[1] == "05")
            {
                query1 = "select s1.kod_bajet,s1.Id,m1.nama_pembekal_code as kepada,UPPER(perkera) as Keteragan,s2.Ref_Projek_name as projek,'' as bank,'' as acc_no,s1.overall as jumlah, discount as Disk "
                          + "   ,UPPER(ISNULL(s4.Ref_nama_cukai,'')) as jen_cukai, s1.gstjumlah cukai_amt, s1.jumlah as amount,s1.jumlah as unit,'02' as kat,'1' as qty "
                          + " from KW_Pembayaran_Credit_item s1 left join KW_Pembayaran_Credit m1 on m1.no_Rujukan =s1.no_Rujukan left join KW_Ref_Projek s2 on s2.Ref_Projek_code=m1.project_kod "
                          + " left join KW_Ref_Tetapan_cukai s4 on s4.Ref_kod_cukai=s1.tax  where m1.no_Rujukan='" + CommandArgument1 + "' and s1.Status='A'";
            }
            else if (CommadArgument[1] == "06")
            {
                query1 = "select s1.kod_bajet,s1.Id,m1.nama_pembekal_code as kepada,UPPER(perkera) as Keteragan,s2.Ref_Projek_name as projek,'' as bank,'' as acc_no,s1.overall as jumlah, discount as Disk "
                          + "   ,UPPER(ISNULL(s4.Ref_nama_cukai,'')) as jen_cukai, s1.gstjumlah cukai_amt,s1.jumlah  as amount,s1.jumlah as unit,'02' as kat,'1' as qty "
                          + " from KW_Pembayaran_Dedit_item s1 left join KW_Pembayaran_Dedit m1 on m1.no_Rujukan =s1.no_Rujukan left join KW_Ref_Projek s2 on s2.Ref_Projek_code=m1.project_kod "
                          + " left join KW_Ref_Tetapan_cukai s4 on s4.Ref_kod_cukai=s1.tax  where m1.no_Rujukan='" + CommandArgument1 + "' and s1.Status='A'";
            }
            else if (CommadArgument[1] == "09")
            {
                query1 = "select s1.kod_bajet,s1.Id,nama_pelanggan_code as kepada,UPPER(s1.keterangan) as Keteragan,m1.project_kod as projek,inv_bank as bank,inv_noacc_bank as acc_no, s1.Overall as jumlah, discount as Disk  , "
                           + " UPPER(ISNULL(s4.Ref_nama_cukai,'')) as jen_cukai, s1.gstjumlah as cukai_amt,jumlah as amount,unit as unit,'03' as kat,s1.quantiti as qty  from kw_penerimaan_invois_item s1  "
                           + " left join kw_penerimaan_invois m1 on m1.no_invois=s1.no_invois "
                           + " left join KW_Ref_Tetapan_cukai s4 on s4.Ref_kod_cukai=s1.tax where s1.no_invois='" + CommandArgument1 + "' and s1.Status='A'";
            }
            else if (CommadArgument[1] == "10")
            {
                query1 = "select s1.kod_bajet,s1.Id,m1.nama_pelanggan_code as kepada,UPPER(perkara) as Keteragan,'' as projek,'' as bank,'' as acc_no,s1.overall as jumlah, discount as Disk "
                          + "   ,UPPER(ISNULL(s4.Ref_nama_cukai,'')) as jen_cukai, s1.gstjumlah cukai_amt, s1.jumlah as amount,s1.unit as unit,'03' as kat,s1.quantiti as qty  "
                          + " from KW_Penerimaan_Credit_item s1 left join KW_Penerimaan_Credit m1 on m1.no_notakredit =s1.no_notakredit"
                          + " left join KW_Ref_Tetapan_cukai s4 on s4.Ref_kod_cukai=s1.tax  where m1.no_notakredit='" + CommandArgument1 + "' and s1.Status='A'";
            }
            else if (CommadArgument[1] == "11")
            {
                query1 = "select s1.kod_bajet,s1.Id,m1.nama_pelanggan_code as kepada,UPPER(perkara) as Keteragan,'' as projek,'' as bank,'' as acc_no,s1.overall as jumlah, discount as Disk "
                          + "   ,UPPER(ISNULL(s4.Ref_nama_cukai,'')) as jen_cukai, s1.gstjumlah cukai_amt, s1.jumlah as amount,s1.unit as unit,'03' as kat,s1.quantiti as qty  "
                          + " from KW_Penerimaan_Debit_item s1 left join KW_Penerimaan_Debit m1 on m1.no_notadebit =s1.no_notadebit"
                          + " left join KW_Ref_Tetapan_cukai s4 on s4.Ref_kod_cukai=s1.tax  where m1.no_notadebit='" + CommandArgument1 + "' and s1.Status='A'";
            }
            else
            {
                query1 = "select s1.kod_bajet kod_bajet,s1.Id,m1.nama_pelanggan_code as kepada,Keterangan as Keteragan,'' as projek,'' as bank,'' as acc_no,s1.overall as jumlah, '' as Disk "
                          + "   ,'' as jen_cukai, '' cukai_amt, s1.overall as amount,'' as unit,'11' as kat,Replace(s1.jumlah, '.00', '') as qty  "
                          + " from KW_jurnal_inter_items s1 left join KW_jurnal_inter m1 on m1.no_permohonan =s1.no_permohonan"
                          + " where Keterangan != 'PENDAPATAN BERSIH' and ISNULL(s1.overall,'0.00') != '0.00' and m1.no_permohonan='" + CommandArgument1 + "' and s1.Status='A'";
            }

            grdmohon.Visible = false;
            gridmohdup.Visible = true;
            dt1 = Dbcon.Ora_Execute_table(query1);
            gridmohdup.DataSource = dt1;
            gridmohdup.DataBind();
            double jum_unit = 0;
            foreach (GridViewRow g1 in gridmohdup.Rows)
            {
                string unit = (g1.FindControl("jumlah") as Label).Text;
                jum_unit += double.Parse(unit);

            }
            gridmohdup.FooterRow.Cells[2].Text = "JUMLAH (RM) :";
            gridmohdup.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
            ((Label)gridmohdup.FooterRow.Cells[12].FindControl("jumlah_ftr")).Text = jum_unit.ToString("C").Replace("RM", "").Replace("$", "");
            //double jum_unit = 0;
            //foreach (GridViewRow g1 in gridmohdup.Rows)
            //{
            //    string unit = (g1.FindControl("jumlah") as Label).Text;
            //    jum_unit += double.Parse(unit);

            //}
            //TextBox17.Text = double.Parse(Convert.ToString(jum_unit)).ToString("C").Replace("$", "").Replace("RM", "");
            lod_bajet();

            //TextBox17.Text = dt.Rows[0][13].ToString().Trim();
            //TextBox17.Attributes.Add("disabled", "disabled");

        }
        tab1();
        //Bindbaucer();
        //BindInvois();
        BindMohon();
        //Bindcredit();
        //Binddedit();
    }


    protected void lblSubItemName_Click(object sender, EventArgs e)
    {

        LinkButton btn = (LinkButton)sender;
        string[] CommadArgument = btn.CommandArgument.Split(',');
        CommandArgument3 = CommadArgument[0];
        //CommandArgument4 = CommadArgument[1];
        ss_frmgrid();

    }

    void ss_frmgrid()
    {
        string ssv2 = string.Empty, ssv3 = string.Empty;
        if (CommandArgument3 != "")
        {
            ssv2 = CommandArgument3;
            ssv3 = txtnoper.Text;
        }
        else
        {
            ssv2 = Session["Invoisno"].ToString();
            ssv3 = Session["permohon_no"].ToString();
        }
        Button2.Visible = false;
        btnprintmoh.Visible = true;
        gridmohdup.Enabled = false;
        sts.Visible = true;
        Div2.Visible = true;
        Div3.Visible = false;
        DataTable dt = new DataTable();
        dt = Dbcon.Ora_Execute_table("select '' as untuk_akaun,no_permohonan,ID_pemohon,Format(tarkih_permohonan,'dd/MM/yyyy') tarkih_permohonan,'' as  Bayar_kepada,'' as no_kakitangan,'' as nama,'' as nama_bank,'' as no_bank,'' as  tarkih_invois,'' as no_Invois,Terma,Jenis_permohonan, '' as  jumlah,status,Catatan,mohon_kp,jornal_no from KW_Pembayaran_mohon  where no_Invois='" + ssv2 + "' and no_permohonan='" + ssv3 + "' group by untuk_akaun,no_permohonan,ID_pemohon,tarkih_permohonan,Bayar_kepada,no_kakitangan,nama,nama_bank,no_bank,tarkih_invois,no_Invois,Terma,Jenis_permohonan,Overall,status,Catatan,mohon_kp,jornal_no");
        tab1();
        if (dt.Rows.Count > 0)
        {

            if (dt.Rows[0]["Bayar_kepada"].ToString() == "kakitangan")
            {
                jurnal_show2.Visible = false;
            }
            else if (dt.Rows[0]["jornal_no"].ToString() == "Pembekal")
            {
                jurnal_show2.Visible = false;
            }
            else
            {
                jurnal_show2.Visible = true;
                TextBox2.Text = dt.Rows[0]["jornal_no"].ToString();
                TextBox2.Attributes.Add("disabled", "disabled");
            }

            Session["Invoisno"] = ssv2;
            ddakaun.SelectedValue = dt.Rows[0][0].ToString();
            ddakaun.Attributes.Add("disabled", "disabled");

            txtnoper.Text = dt.Rows[0][1].ToString().Trim();
            Session["permohon_no"] = dt.Rows[0][1].ToString().Trim();
            txtnoper.Attributes.Add("disabled", "disabled");

            txtid.Text = dt.Rows[0][2].ToString().Trim();
            txtid.Attributes.Add("disabled", "disabled");

            txttarkihmo.Text = dt.Rows[0][3].ToString().Trim();
            txttarkihmo.Attributes.Add("disabled", "disabled");

            ddbkepada.SelectedValue = dt.Rows[0][4].ToString().Trim();
            ddbkepada.Attributes.Add("disabled", "disabled");

            txticno.Text = dt.Rows[0][5].ToString().Trim();
            txticno.Attributes.Add("disabled", "disabled");

            txtname.Text = dt.Rows[0][6].ToString().Trim();
            txtname.Attributes.Add("disabled", "disabled");

            txtbname.Text = dt.Rows[0][7].ToString().Trim();
            txtbname.Attributes.Add("disabled", "disabled");

            txtbno.Text = dt.Rows[0][8].ToString().Trim();
            txtbno.Attributes.Add("disabled", "disabled");


            //txtterma.Text = dt.Rows[0][11].ToString().Trim();
            //txtterma.Attributes.Add("disabled", "disabled");

            txtjenis.Text = dt.Rows[0][12].ToString().Trim();
            txtjenis.Attributes.Add("disabled", "disabled");



            ddstatus.SelectedValue = dt.Rows[0][14].ToString().Trim();
            ddstatus.Attributes.Add("disabled", "disabled");

            //txtcatatan.Text = dt.Rows[0][15].ToString().Trim();
            //txtcatatan.Attributes.Add("disabled", "disabled");

            userid = Session["New"].ToString();
            level = Session["level"].ToString();

            //if (level == "6")
            //{
            //    txtid.Text = userid;
            //    txtid.Attributes.Add("disabled", "disabled");
            //    sts.Visible = false;
            //    Div10.Visible = false;
            //}
            //else if (level == "5")
            //{
            txtApr.Text = userid;
            txtApr.Attributes.Add("disabled", "disabled");
            //sts.Visible = true;
            //Div10.Visible = true;
            if (dt.Rows[0]["status"].ToString().Trim() == "L")
            {
                sts.Visible = false;
                Div10.Visible = false;
            }
            else
            {
                sts.Visible = true;
                Div10.Visible = true;
            }
            //}


            DataTable dt1 = new DataTable();
            grdmohon.Visible = false;
            gridmohdup.Visible = true;
            dt1 = Dbcon.Ora_Execute_table("select  Format(tarkih_invois,'dd/MM/yyyy')tarkih_invois,No_rujukan,Keteragan,Gjumlah,Gst, overall from KW_Pembayaran_mohon where no_Invois='" + ssv2 + "'");
            gridmohdup.DataSource = dt1;
            gridmohdup.DataBind();


            TextBox17.Text = dt.Rows[0][13].ToString().Trim();
            TextBox17.Attributes.Add("disabled", "disabled");

        }
        Bindbaucer();
        BindInvois();
        BindMohon();
        Bindcredit();
        Binddedit();
    }
    protected void lblSubItemBil()
    {


        tab5();
        DataTable dt1 = new DataTable();
        dt1 = Dbcon.Ora_Execute_table("select   Format(sum(overall),'#,##,###.00') overall from KW_Pembayaran_mohon where no_permohonan='" + ddnoper.SelectedItem.Text + "' and Inv_status='N'");
        TextBox37.Text = dt1.Rows[0][0].ToString().Trim();
        TextBox37.Attributes.Add("disabled", "disabled");

    }
    protected void lblSubItemPV_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string[] CommadArgument = btn.CommandArgument.Split(',');
        CommandArgument1 = CommadArgument[0];
        crt_pvlink();
    }

    void crt_pvlink()
    {
        string ssv2 = string.Empty;
        if (CommandArgument1 != "")
        {
            ssv2 = CommandArgument1;
        }
        else
        {
            ssv2 = Session["pvno"].ToString();
        }
        Button2.Visible = false;
        //Button4.Visible = true;
        pv_tab3.Visible = true;
        level = Session["level"].ToString();



        Div5.Visible = true;
        Div4.Visible = false;
        tab2();
        Button7.Visible = false;
        grdpvinv_new.Visible = false;
        DataTable dt = new DataTable();
        dt = Dbcon.Ora_Execute_table("select untuk_akaun,Data,Pv_no,no_invois,Format(tarkih_pv,'dd/MM/yyyy') tarkih_pv,kat_akaun,Deb_kod_akaun,Bayar_kepada,Cara_Bayaran,Cre_kod_akaun,Terma,No_cek,status,Akaun_name from KW_Pembayaran_Pay_voucer where pv_no='" + ssv2 + "'");

        if (dt.Rows.Count > 0)
        {
            if (level == "6")
            {
                if (dt.Rows[0][12].ToString().Trim() != "B")
                {
                    grdpvinv.Visible = false;
                    Grdpvinvdup.Visible = true;
                    Button17.Visible = false;
                    ddakaun.SelectedValue = dt.Rows[0][0].ToString();
                    ddakaun.Attributes.Add("disabled", "disabled");

                    txtdata.Text = dt.Rows[0][1].ToString().Trim();
                    txtdata.Attributes.Add("disabled", "disabled");

                    txtpvno.Text = dt.Rows[0][2].ToString().Trim();
                    txtpvno.Attributes.Add("disabled", "disabled");
                    Session["pvno"] = txtpvno.Text;
                    if (dt.Rows[0][12].ToString().Trim() == "L")
                    {
                        ddnoperpv.SelectedItem.Text = dt.Rows[0][3].ToString().Trim();
                        ddnoperpv_PV1.Text = dt.Rows[0][3].ToString().Trim();
                        ddnoperpv.Attributes.Add("disabled", "disabled");
                        Button18.Visible = true;
                    }
                    else
                    {
                        ddnoperpv.SelectedValue = dt.Rows[0][3].ToString().Trim();
                        ddnoperpv_PV1.Text = dt.Rows[0][3].ToString().Trim();
                        ddnoperpv.Attributes.Add("disabled", "disabled");
                        Button18.Visible = false;
                    }

                    txttarpv.Text = dt.Rows[0][4].ToString().Trim();
                    txttarpv.Attributes.Add("disabled", "disabled");

                    ddpvkat.SelectedValue = dt.Rows[0][5].ToString().Trim();
                    ddpvkat.Attributes.Add("disabled", "disabled");
                    bind_grid1();
                    ddpvkod.SelectedValue = dt.Rows[0][6].ToString().Trim();
                    ddpvkod.Attributes.Add("disabled", "disabled");



                    ddpvcara.SelectedValue = dt.Rows[0][8].ToString().Trim();
                    ddpvcara.Attributes.Add("disabled", "disabled");

                    DDbank.SelectedValue = dt.Rows[0][9].ToString().Trim();
                    DDbank.Attributes.Add("disabled", "disabled");

                    txtpvnocek.Text = dt.Rows[0][11].ToString().Trim();
                    txtpvnocek.Attributes.Add("disabled", "disabled");

                    txtpvterma.Text = dt.Rows[0][10].ToString().Trim();
                    txtpvterma.Attributes.Add("disabled", "disabled");

                    TXTPVNAME.Text = dt.Rows[0][13].ToString().Trim();
                    TXTPVNAME.Attributes.Add("disabled", "disabled");

                    ddpvstatus.Visible = false;
                    ddpvstatus1.Visible = true;
                    ddpvstatus1.SelectedValue = dt.Rows[0][12].ToString().Trim();
                    ddpvstatus1.Attributes.Add("disabled", "disabled");


                    DataTable dt1 = new DataTable();
                    dt1 = Dbcon.Ora_Execute_table("select no_invois,Format(tarikh_invois,'dd/MM/yyyy')tarikh_invois,REPLACE(CONVERT(varchar(20), (CAST((jumlah) AS money)), 1), '.00', '.00') jumlah,REPLACE(CONVERT(varchar(20), (CAST((gstjumlah) AS money)), 1), '.00', '.00') gstjumlah,REPLACE(CONVERT(varchar(20), (CAST((othgstjumlah) AS money)), 1), '.00', '.00') othgstjumlah,REPLACE(CONVERT(varchar(20), (CAST((Overall) AS money)), 1), '.00', '.00') Overall,REPLACE(CONVERT(varchar(20), (CAST(sum(payamt) AS money)), 1), '.00', '.00') Payamt from KW_Pembayaran_Pay_voucer where pv_no='" + ssv2 + "' group by no_invois,tarikh_invois,jumlah,gstjumlah,othgstjumlah,Overall");
                    Grdpvinvdup.DataSource = dt1;
                    Grdpvinvdup.DataBind();

                }
                else
                {
                    Button17.Visible = true;
                    grdpvinv.Visible = true;
                    Grdpvinvdup.Visible = false;
                    ddakaun.SelectedValue = dt.Rows[0][0].ToString();


                    txtdata.Text = dt.Rows[0][1].ToString().Trim();


                    txtpvno.Text = dt.Rows[0][2].ToString().Trim();
                    Session["pvno"] = txtpvno.Text;

                    if (dt.Rows[0][12].ToString().Trim() == "L")
                    {
                        ddnoperpv.SelectedItem.Text = dt.Rows[0][3].ToString().Trim();
                        ddnoperpv_PV1.Text = dt.Rows[0][3].ToString().Trim();
                        Button18.Visible = true;

                    }
                    else
                    {
                        ddnoperpv.SelectedValue = dt.Rows[0][3].ToString().Trim();
                        ddnoperpv_PV1.Text = dt.Rows[0][3].ToString().Trim();
                        Button18.Visible = false;
                    }

                    txttarpv.Text = dt.Rows[0][4].ToString().Trim();


                    ddpvkat.SelectedValue = dt.Rows[0][5].ToString().Trim();

                    bind_grid1();
                    ddpvkod.SelectedValue = dt.Rows[0][6].ToString().Trim();
                    ddpvcara.SelectedValue = dt.Rows[0][8].ToString().Trim();
                    if (ddpvcara.SelectedValue != "Tunai")
                    {
                        txtpvnocek.Text = dt.Rows[0][11].ToString().Trim();

                    }
                    else
                    {

                        txtpvnocek.Attributes.Add("disabled", "disabled");
                    }

                    DDbank.SelectedValue = dt.Rows[0][9].ToString().Trim();





                    txtpvterma.Text = dt.Rows[0][10].ToString().Trim();

                    ddpvstatus.Visible = true;
                    ddpvstatus1.Visible = false;
                    ddpvstatus.SelectedValue = dt.Rows[0][12].ToString().Trim();

                    TXTPVNAME.Text = dt.Rows[0][13].ToString().Trim();


                    DataTable dt1 = new DataTable();
                    dt1 = Dbcon.Ora_Execute_table("select a.tarkih_mohon,a.tarkih_invois,a.no_invois,a.no_permohonan,a.jumlah,a.gstjumlah,a.othgstjumlah,a.Overall,isnull(b.Baki,0) as Baki,no_po from (select Format(tarkih_mohon,'dd/MM/yyyy')tarkih_mohon,Format(tarkih_invois,'dd/MM/yyyy')tarkih_invois,no_invois,no_permohonan,REPLACE(CONVERT(varchar(20), (CAST(SUM(jumlah) AS money)), 1), '.00', '.00') jumlah, REPLACE(CONVERT(varchar(20), (CAST(SUM(gstjumlah) AS money)), 1), '.00', '.00') gstjumlah, REPLACE(CONVERT(varchar(20), (CAST(SUM(othgstjumlah) AS money)), 1), '.00', '.00') othgstjumlah, REPLACE(CONVERT(varchar(20), (CAST(SUM(Overall) AS money)), 1), '.00', '.00') Overall from KW_Pembayaran_invoisBil_item where no_invois='" + dt.Rows[0][3].ToString() + "' and  Status='B' group by  tarkih_mohon,tarkih_invois,no_invois,no_permohonan) a left join (select  no_Permohonan,no_invois,format( sum(Payamt) ,'#,##,###.00') Baki   from KW_Pembayaran_Pay_voucer group by no_permohonan,no_invois) b on a.no_permohonan=b.no_Permohonan and a.no_invois=b.no_invois");
                    grdpvinv.DataSource = dt1;
                    grdpvinv.DataBind();

                }


            }
            else
            {
                if (dt.Rows[0][12].ToString().Trim() != "B")
                {

                    ddakaun.SelectedValue = dt.Rows[0][0].ToString();
                    ddakaun.Attributes.Add("disabled", "disabled");

                    txtdata.Text = dt.Rows[0][1].ToString().Trim();
                    txtdata.Attributes.Add("disabled", "disabled");

                    txtpvno.Text = dt.Rows[0][2].ToString().Trim();
                    txtpvno.Attributes.Add("disabled", "disabled");
                    Session["pvno"] = txtpvno.Text;
                    if (dt.Rows[0][12].ToString().Trim() == "L")
                    {
                        ddnoperpv.SelectedItem.Text = dt.Rows[0][3].ToString().Trim();
                        ddnoperpv_PV1.Text = dt.Rows[0][3].ToString().Trim();
                        ddnoperpv.Attributes.Add("disabled", "disabled");
                        Button18.Visible = true;
                    }
                    else
                    {
                        ddnoperpv.SelectedValue = dt.Rows[0][3].ToString().Trim();
                        ddnoperpv_PV1.Text = dt.Rows[0][3].ToString().Trim();
                        ddnoperpv.Attributes.Add("disabled", "disabled");
                        Button18.Visible = false;
                    }

                    txttarpv.Text = dt.Rows[0][4].ToString().Trim();
                    txttarpv.Attributes.Add("disabled", "disabled");

                    ddpvkat.SelectedValue = dt.Rows[0][5].ToString().Trim();
                    ddpvkat.Attributes.Add("disabled", "disabled");
                    bind_grid1();
                    ddpvkod.SelectedValue = dt.Rows[0][6].ToString().Trim();
                    ddpvkod.Attributes.Add("disabled", "disabled");



                    ddpvcara.SelectedValue = dt.Rows[0][8].ToString().Trim();
                    ddpvcara.Attributes.Add("disabled", "disabled");

                    DDbank.SelectedValue = dt.Rows[0][9].ToString().Trim();
                    DDbank.Attributes.Add("disabled", "disabled");

                    txtpvnocek.Text = dt.Rows[0][11].ToString().Trim();
                    txtpvnocek.Attributes.Add("disabled", "disabled");

                    txtpvterma.Text = dt.Rows[0][10].ToString().Trim();
                    txtpvterma.Attributes.Add("disabled", "disabled");

                    TXTPVNAME.Text = dt.Rows[0][13].ToString().Trim();
                    TXTPVNAME.Attributes.Add("disabled", "disabled");

                    ddpvstatus.Visible = false;
                    ddpvstatus1.Visible = true;
                    grdpvinv_new.Visible = false;
                    grdpvinv.Visible = false;
                    Button17.Visible = false;
                    Grdpvinvdup.Visible = true;
                    ddpvstatus1.SelectedValue = dt.Rows[0][12].ToString().Trim();
                    ddpvstatus1.Attributes.Add("disabled", "disabled");


                    DataTable dt1 = new DataTable();
                    dt1 = Dbcon.Ora_Execute_table("select no_invois,Format(tarikh_invois,'dd/MM/yyyy')tarikh_invois,REPLACE(CONVERT(varchar(20), (CAST((jumlah) AS money)), 1), '.00', '.00') jumlah,REPLACE(CONVERT(varchar(20), (CAST((gstjumlah) AS money)), 1), '.00', '.00') gstjumlah,REPLACE(CONVERT(varchar(20), (CAST((othgstjumlah) AS money)), 1), '.00', '.00') othgstjumlah,REPLACE(CONVERT(varchar(20), (CAST((Overall) AS money)), 1), '.00', '.00') Overall,REPLACE(CONVERT(varchar(20), (CAST(sum(payamt) AS money)), 1), '.00', '.00') Payamt from KW_Pembayaran_Pay_voucer where pv_no='" + ssv2 + "' group by no_invois,tarikh_invois,jumlah,gstjumlah,othgstjumlah,Overall");
                    Grdpvinvdup.DataSource = dt1;
                    Grdpvinvdup.DataBind();

                }
                else
                {

                    ddakaun.SelectedValue = dt.Rows[0][0].ToString();
                    ddakaun.Attributes.Add("disabled", "disabled");

                    txtdata.Text = dt.Rows[0][1].ToString().Trim();
                    txtdata.Attributes.Add("disabled", "disabled");

                    txtpvno.Text = dt.Rows[0][2].ToString().Trim();
                    txtpvno.Attributes.Add("disabled", "disabled");
                    Session["pvno"] = txtpvno.Text;
                    if (dt.Rows[0][12].ToString().Trim() == "L")
                    {
                        ddnoperpv.SelectedItem.Text = dt.Rows[0][3].ToString().Trim();
                        ddnoperpv_PV1.Text = dt.Rows[0][3].ToString().Trim();
                        ddnoperpv.Attributes.Add("disabled", "disabled");
                        Button18.Visible = true;
                    }
                    else
                    {
                        if (dt.Rows[0]["kat_akaun"].ToString() != "SEMUA COA")
                        {
                            ddnoperpv.SelectedValue = dt.Rows[0][3].ToString().Trim();
                            ddnoperpv_PV1.Text = dt.Rows[0][3].ToString().Trim();
                            ddnoperpv.Attributes.Add("disabled", "disabled");
                            Button18.Visible = false;
                        }
                    }

                    txttarpv.Text = dt.Rows[0][4].ToString().Trim();
                    txttarpv.Attributes.Add("disabled", "disabled");

                    ddpvkat.SelectedValue = dt.Rows[0][5].ToString().Trim();
                    ddpvkat.Attributes.Add("disabled", "disabled");
                    bind_grid1();
                    ddpvkod.SelectedValue = dt.Rows[0][6].ToString().Trim();
                    ddpvkod.Attributes.Add("disabled", "disabled");

                    ddpvcara.SelectedValue = dt.Rows[0][8].ToString().Trim();
                    ddpvcara.Attributes.Add("disabled", "disabled");

                    DDbank.SelectedValue = dt.Rows[0][9].ToString().Trim();
                    DDbank.Attributes.Add("disabled", "disabled");

                    txtpvnocek.Text = dt.Rows[0][11].ToString().Trim();
                    txtpvnocek.Attributes.Add("disabled", "disabled");

                    txtpvterma.Text = dt.Rows[0][10].ToString().Trim();
                    txtpvterma.Attributes.Add("disabled", "disabled");

                    TXTPVNAME.Text = dt.Rows[0][13].ToString().Trim();
                    TXTPVNAME.Attributes.Add("disabled", "disabled");

                    ddpvstatus.Visible = false;
                    ddpvstatus1.Visible = true;
                    ddpvstatus1.Attributes.Remove("disabled");
                    ddpvstatus1.SelectedIndex = 0;
                    grdpvinv.Visible = false;
                    grdpvinv_new.Visible = false;
                    Grdpvinvdup.Visible = true;
                    Button17.Visible = true;

                    DataTable dt1 = new DataTable();
                    dt1 = Dbcon.Ora_Execute_table("select no_invois,Format(tarikh_invois,'dd/MM/yyyy')tarikh_invois,REPLACE(CONVERT(varchar(20), (CAST((jumlah) AS money)), 1), '.00', '.00') jumlah,REPLACE(CONVERT(varchar(20), (CAST((gstjumlah) AS money)), 1), '.00', '.00') gstjumlah,REPLACE(CONVERT(varchar(20), (CAST((othgstjumlah) AS money)), 1), '.00', '.00') othgstjumlah,REPLACE(CONVERT(varchar(20), (CAST((Overall) AS money)), 1), '.00', '.00') Overall,REPLACE(CONVERT(varchar(20), (CAST(sum(payamt) AS money)), 1), '.00', '.00') Payamt from KW_Pembayaran_Pay_voucer where pv_no='" + ssv2 + "' group by no_invois,tarikh_invois,jumlah,gstjumlah,othgstjumlah,Overall");
                    Grdpvinvdup.DataSource = dt1;
                    Grdpvinvdup.DataBind();
                }
            }

        }
    }


    protected void lblSubItemInv_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;

        string[] CommadArgument = btn.CommandArgument.Split(',');

        CommandArgument1 = CommadArgument[0];
        CommandArgument2 = CommadArgument[1];

        Button8.Visible = false;

        grdbilinv.Visible = false;
        grdbilinv1.Visible = false;
        Gridview5.Visible = false;

        Div13.Visible = true;
        Div12.Visible = false;
        ddnoper.Visible = false;
        txtnoperbil.Visible = true;
        tab5();

        DataTable dt = new DataTable();
        dt = Dbcon.Ora_Execute_table("select untuk_akaun,Data,no_permohonan,kat_akaun,cre_kod_akaun,Format(tarkih_mohon,'dd/MM/yyyy') tarkih_mohon,Format(tarkih_invois,'dd/MM/yyyy') tarkih_invois,no_invois,Terma from KW_Pembayaran_invoisBil_item where no_permohonan='" + CommandArgument1 + "' and Data='" + CommandArgument2 + "'");

        if (dt.Rows.Count > 0)
        {
            if (CommandArgument2 == "MOHON BAYAR")
            {
                grdbilinvdub.Visible = true;
                grdbilinvdub.Enabled = false;
                grdvie5dup.Visible = false;
                kataka.Visible = false;
                kodaka.Visible = false;
                invbil.Visible = false;
                ddakaun.SelectedValue = dt.Rows[0][0].ToString();
                ddakaun.Attributes.Add("disabled", "disabled");

                dddata.SelectedValue = dt.Rows[0][1].ToString().Trim();
                dddata.Attributes.Add("disabled", "disabled");

                txtnoperbil.Text = dt.Rows[0][2].ToString().Trim();
                txtnoperbil.Attributes.Add("disabled", "disabled");

                DataTable dt1 = new DataTable();
                //dt1 = Dbcon.Ora_Execute_table("select kat_akaun,cre_kod_akaun,deb_kod_akaun,Format(tarkih_mohon,'dd/MM/yyyy') tarkih_mohon,Format(tarkih_invois,'dd/MM/yyyy') tarkih_invois,no_invois,item,keterangan,project_kod,unit,quantiti, REPLACE(CONVERT(varchar(20), (CAST((jumlah) AS money)), 1), '.00', '.00') jumlah, REPLACE(CONVERT(varchar(20), (CAST((gstjumlah) AS money)), 1), '.00', '.00') gstjumlah,REPLACE(CONVERT(varchar(20), (CAST((Overall) AS money)), 1), '.00', '.00') Overall from KW_Pembayaran_invoisBil_item where no_permohonan='" + CommandArgument1 + "' and data='MOHON BAYAR'");
                dt1 = Dbcon.Ora_Execute_table("select m1.kat_akaun,cre_kod_akaun,s2.nama_akaun as cre_name,deb_kod_akaun,s3.nama_akaun as deb_name,Format(tarkih_mohon,'dd/MM/yyyy') tarkih_mohon,Format(tarkih_invois,'dd/MM/yyyy') tarkih_invois,no_invois,item,keterangan,project_kod,s1.Ref_Projek_name,unit,quantiti, REPLACE(CONVERT(varchar(20), (CAST((jumlah) AS money)), 1), '.00', '.00') jumlah, REPLACE(CONVERT(varchar(20), (CAST((gstjumlah) AS money)), 1), '.00', '.00') gstjumlah,REPLACE(CONVERT(varchar(20), (CAST((Overall) AS money)), 1), '.00', '.00') Overall from KW_Pembayaran_invoisBil_item m1 left join KW_Ref_Projek s1 on s1.Ref_Projek_code=project_kod left join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=m1.cre_kod_akaun left join KW_Ref_Carta_Akaun s3 on s3.kod_akaun=m1.deb_kod_akaun where m1.no_permohonan='" + CommandArgument1 + "' and m1.data='MOHON BAYAR'");
                grdbilinvdub.DataSource = dt1;
                grdbilinvdub.DataBind();



                DataTable dt1_jum = new DataTable();
                dt1_jum = Dbcon.Ora_Execute_table("select SUM(Overall) as Overall from KW_Pembayaran_invoisBil_item where no_permohonan='" + CommandArgument1 + "' and data='MOHON BAYAR'");
                if (dt1_jum.Rows.Count != 0)
                {
                    TextBox37.Text = double.Parse(dt1_jum.Rows[0]["Overall"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                }
                else
                {
                    TextBox37.Text = "0.00";
                }

            }
            else
            {
                grdvie5dup.Visible = true;
                grdvie5dup.Enabled = false;
                grdbilinvdub.Visible = false;
                kataka.Visible = true;
                kodaka.Visible = true;
                invbil.Visible = true;
                ddakaun.SelectedValue = dt.Rows[0][0].ToString();
                ddakaun.Attributes.Add("disabled", "disabled");

                dddata.SelectedValue = dt.Rows[0][1].ToString().Trim();
                dddata.Attributes.Add("disabled", "disabled");

                txtnoperbil.Text = dt.Rows[0][2].ToString().Trim();
                txtnoperbil.Attributes.Add("disabled", "disabled");


                ddkataka.SelectedValue = dt.Rows[0][3].ToString().Trim();
                ddkataka.Attributes.Add("disabled", "disabled");
                bind_carta();

                ddkodaka.SelectedValue = dt.Rows[0][4].ToString().Trim();
                ddkodaka.Attributes.Add("disabled", "disabled");

                txttmb.Text = dt.Rows[0][5].ToString().Trim();
                txttmb.Attributes.Add("disabled", "disabled");


                txttinvbil.Text = dt.Rows[0][6].ToString().Trim();
                txttinvbil.Attributes.Add("disabled", "disabled");


                txrinvbil.Text = dt.Rows[0][7].ToString().Trim();
                txrinvbil.Attributes.Add("disabled", "disabled");


                txtinterma.Text = dt.Rows[0][8].ToString().Trim();
                txtinterma.Attributes.Add("disabled", "disabled");


                DataTable dt1 = new DataTable();
                dt1 = Dbcon.Ora_Execute_table("select deb_kod_akaun,s1.nama_akaun,item,keterangan,ISNULL(s2.Ref_Projek_name,'') as Ref_Projek_name,unit,quantiti,REPLACE(CONVERT(varchar(20), (CAST((discount) AS money)), 1), '.00', '.00') discount, REPLACE(CONVERT(varchar(20), (CAST((jumlah) AS money)), 1), '.00', '.00') jumlah,CAST(CASE tax WHEN 'Y' THEN 1 ELSE 0 END AS BIT) AS tax,othgsttype,ISNULL(s3.Ref_nama_cukai,'') oth_name,gsttype,ISNULL(s4.Ref_nama_cukai,'') th_name,REPLACE(CONVERT(varchar(20), (CAST((Overall) AS money)), 1), '.00', '.00') Overall from KW_Pembayaran_invoisBil_item m1 left join KW_Ref_Carta_Akaun s1 on s1.kod_akaun=m1.deb_kod_akaun left join KW_Ref_Projek s2 on s2.Ref_Projek_code=m1.project_kod left join KW_Ref_Tetapan_cukai s3 on s3.Ref_kod_cukai=m1.othgsttype left join KW_Ref_Tetapan_cukai s4 on s4.Ref_kod_cukai=m1.gsttype where no_permohonan='" + CommandArgument1 + "' and data='Baru'");
                grdvie5dup.DataSource = dt1;
                grdvie5dup.DataBind();

                DataTable dt1_jum1 = new DataTable();
                dt1_jum1 = Dbcon.Ora_Execute_table("select SUM(Overall) as Overall from KW_Pembayaran_invoisBil_item where no_permohonan='" + CommandArgument1 + "' and data='Baru'");
                if (dt1_jum1.Rows.Count != 0)
                {
                    TextBox37.Text = double.Parse(dt1_jum1.Rows[0]["Overall"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                }
                else
                {
                    TextBox37.Text = "0.00";
                }
            }

        }
        hd_txt.Text = "INVOIS / BIL";
        Bindbaucer();
        BindInvois();
        BindMohon();
        Bindcredit();
        Binddedit();
    }


    protected void ddpela2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddpela2.SelectedItem.Text != "--- PILIH ---")
        {
            string com = "select no_invois from KW_Pembayaran_invoisBil_item where cre_kod_akaun='" + ddpela2.SelectedItem.Value + "' group by no_invois";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddinv.DataSource = dt;
            ddinv.DataTextField = "no_invois";
            ddinv.DataValueField = "no_invois";
            ddinv.DataBind();
            ddinv.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            tab3();
        }
    }



    protected void lblSubItemcredit_Click(object sender, EventArgs e)
    {

        LinkButton btn = (LinkButton)sender;
        string[] CommadArgument = btn.CommandArgument.Split(',');
        CommandArgument1 = CommadArgument[0];
        crt_notelink();

    }


    void crt_notelink()
    {
        string ssv2 = string.Empty;
        if (CommandArgument1 != "")
        {
            ssv2 = CommandArgument1;
        }
        else
        {
            ssv2 = Session["dbtno"].ToString();
        }

        Button15.Visible = false;
        Button22.Visible = true;
        kredit_tab4.Visible = true;
        th2.Visible = true;
        th1.Visible = false;


        Binddedit();
        tab3();

        DataTable dt = new DataTable();
        dt = Dbcon.Ora_Execute_table("select untuk_akaun,nama_pelanggan,no_Rujukan,Format(tarikh_invois,'dd/MM/yyyy') tarikh_invois ,Format(tarikh_credit,'dd/MM/yyyy') tarikh_credit,project_kod,Format(jumlah,'#,##,###.00') jumlah,no_invois,inv_day from KW_Pembayaran_Credit_item  where no_Rujukan='" + ssv2 + "'");

        if (dt.Rows.Count > 0)
        {
            ddakaun.SelectedValue = dt.Rows[0][0].ToString();
            ddakaun.Attributes.Add("disabled", "disabled");

            ddpela2.SelectedValue = dt.Rows[0][1].ToString();
            ddpela2.Attributes.Add("disabled", "disabled");

            txtnoruju.Text = dt.Rows[0][2].ToString();
            txtnoruju.Attributes.Add("disabled", "disabled");

            txttcinvois.Text = dt.Rows[0][3].ToString();
            txttcinvois.Attributes.Add("disabled", "disabled");

            txttcre.Text = dt.Rows[0][4].ToString().Trim();
            txttcre.Attributes.Add("disabled", "disabled");

            ddpro1.SelectedValue = dt.Rows[0][5].ToString();
            ddpro1.Attributes.Add("disabled", "disabled");

            ddinv.SelectedValue = dt.Rows[0][7].ToString();
            ddinv.Attributes.Add("disabled", "disabled");

            if (dt.Rows[0][8].ToString().Trim() != "")
            {
                ddinvday.SelectedValue = dt.Rows[0][8].ToString();
            }
            else
            {
                ddinvday.SelectedValue = "";
            }
            ddinvday.Attributes.Add("disabled", "disabled");

            Session["dbtno"] = ssv2;

            DataTable dt2 = new DataTable();

            dt2 = Dbcon.Ora_Execute_table("select m1.deb_kod_akaun as kod_akauan,s1.nama_akaun,s2.Ref_nama_cukai as othgstname,s3.Ref_nama_cukai as gstname,m1.no_invois as rujukan,m1.item,m1.keterangan,m1.unit as unit,m1.quantiti,m1.gst,case when m1.discount = '0.00' then '0.00' else m1.discount end as discount,m1.jumlah as jumlah,m1.Overall as Overall,m1.gstjumlah as gstjum,m1.othgstjumlah as othgst,CAST(CASE m1.tax WHEN 'Y' THEN 1 ELSE 0 END AS BIT) AS tax,m1.gsttype,m1.othgsttype from KW_Pembayaran_invoisBil_item  m1 left join KW_Ref_Carta_Akaun s1 on s1.kod_akaun=m1.deb_kod_akaun left join KW_Ref_Tetapan_cukai s2 on s2.Ref_kod_cukai=m1.othgsttype left join KW_Ref_Tetapan_cukai s3 on s3.Ref_kod_cukai=m1.gsttype where m1.no_invois='" + dt.Rows[0][7].ToString().Trim() + "' and m1.cre_kod_akaun='" + dt.Rows[0][1].ToString() + "' ");
            Gridview9.DataSource = dt2;
            Gridview9.DataBind();

            Gridview10.Visible = false;

            DataTable dt1 = new DataTable();
            Gridview12.Visible = true;
            dt1 = Dbcon.Ora_Execute_table("select kod_akauan,s1.nama_akaun ,s2.Ref_nama_cukai as othgstname,s3.Ref_nama_cukai as gstname,keterangan,CAST(CASE c.tax WHEN 'Y' THEN 1 ELSE 0 END AS BIT) AS tax,gst,othgst,gsttype,othgsttype, case when c.gstjumlah = '0.00' then '0.00' else Format(c.gstjumlah,'#,##,###.00') end as gstjumlah , case when c.othgstjumlah = '0.00' then '0.00' else Format(c.othgstjumlah,'#,##,###.00') end as othgstjumlah , case when c.jumlah = '0.00' then '0.00' else Format(c.jumlah,'#,##,###.00') end as jumlah , case when c.overall = '0.00' then '0.00' else Format(c.overall,'#,##,###.00') end as overall   from KW_Pembayaran_Credit_item c left join KW_Ref_Carta_Akaun s1 on s1.kod_akaun=c.kod_akauan left join KW_Ref_Tetapan_cukai s2 on s2.Ref_kod_cukai=c.othgsttype left join KW_Ref_Tetapan_cukai s3 on s3.Ref_kod_cukai=c.gsttype where no_Rujukan='" + ssv2 + "'");
            Gridview12.DataSource = dt1;
            Gridview12.DataBind();


            DataTable dt1_jum = new DataTable();
            dt1_jum = Dbcon.Ora_Execute_table("select SUM(Overall) as Overall from KW_Pembayaran_Credit_item where no_Rujukan='" + ssv2 + "'");
            if (dt1_jum.Rows.Count != 0)
            {
                TextBox18.Text = double.Parse(dt1_jum.Rows[0]["Overall"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
            }
            else
            {
                TextBox18.Text = "0.00";
            }

        }
    }

    protected void ddinv_SelectedIndexChanged(object sender, EventArgs e)
    {
        lnk_clk_crt();
    }

    void lnk_clk_crt()
    {
        string ssv1 = string.Empty;
        if (ddinv.SelectedItem.Text != "--- PILIH ---")
        {
            ssv1 = ddinv.SelectedItem.Text;
        }
        else
        {
            ssv1 = "";
        }

        string getsess_val = string.Empty;


        DataTable dt = new DataTable();
        dt = Dbcon.Ora_Execute_table("select untuk_akaun,cre_kod_akaun,Format(tarkih_invois,'dd/MM/yyyy') tarikh_invois,project_kod,Terma,deb_kod_akaun,item,keterangan,Format(unit,'#,##,###.00') unit,quantiti,gst,Format(Overall,'#,##,###.00') jumlah,tax from KW_Pembayaran_invoisBil_item where no_invois='" + ssv1 + "' and cre_kod_akaun='" + ddpela2.SelectedValue + "' ");
        DataTable dt1 = new DataTable();
        if (dt.Rows.Count > 0)
        {

            ddakaun.Attributes.Add("disabled", "disabled");
            ddpela2.Attributes.Add("disabled", "disabled");
            txtnoruju.Attributes.Add("disabled", "disabled");
            txttcinvois.Attributes.Add("disabled", "disabled");
            ddpro1.Attributes.Add("disabled", "disabled");
            ddinvday.Attributes.Add("disabled", "disabled");

            ddakaun.SelectedValue = dt.Rows[0][0].ToString();
            ddpela2.SelectedValue = dt.Rows[0][1].ToString().Trim();

            txttcinvois.Text = dt.Rows[0][2].ToString();
            ddpro1.SelectedValue = dt.Rows[0][3].ToString();
            ddinvday.SelectedItem.Value = dt.Rows[0][4].ToString();


            Gridview9.Visible = true;
            Gridview12.Visible = false;
            dt1 = Dbcon.Ora_Execute_table("select m1.deb_kod_akaun as kod_akauan,s1.nama_akaun,s2.Ref_nama_cukai as othgstname,s3.Ref_nama_cukai as gstname,m1.no_invois as rujukan,m1.item,m1.keterangan,m1.unit as unit,m1.quantiti,m1.gst,case when m1.discount = '0.00' then '0.00' else m1.discount  end as discount,m1.jumlah as jumlah,m1.Overall as Overall,m1.gstjumlah as gstjum,m1.othgstjumlah as othgst,CAST(CASE m1.tax WHEN 'Y' THEN 1 ELSE 0 END AS BIT) AS tax,m1.gsttype,m1.othgsttype from KW_Pembayaran_invoisBil_item  m1 left join KW_Ref_Carta_Akaun s1 on s1.kod_akaun=m1.deb_kod_akaun left join KW_Ref_Tetapan_cukai s2 on s2.Ref_kod_cukai=m1.othgsttype left join KW_Ref_Tetapan_cukai s3 on s3.Ref_kod_cukai=m1.gsttype where m1.no_invois='" + ssv1 + "' and m1.cre_kod_akaun='" + ddpela2.SelectedItem.Value + "' ");
            Gridview9.DataSource = dt1;
            Gridview9.DataBind();

            Button15.Visible = true;
            Button22.Visible = false;

            th2.Visible = true;
            th1.Visible = false;


            Binddedit();
            tab3();



        }
        else
        {
            //dt1 = Dbcon.Ora_Execute_table("select m1.kod_akauan,s1.nama_akaun,s2.Ref_nama_cukai as othgstname,s3.Ref_nama_cukai as gstname,m1.rujukan,m1.item,m1.keterangan,Format(m1.unit,'#,##,###.00') unit,m1.quantiti,m1.gst,case when m1.discount = '0.00' then '0.00' else Format(m1.discount,'#,##,###.00') end as discount,Format(m1.jumlah,'#,##,###.00') jumlah,Format(m1.Overall,'#,##,###.00') Overall,Format(m1.gstjumlah,'#,##,###.00') gstjum,Format(m1.othgstjumlah,'#,##,###.00') othgst,CAST(CASE m1.tax WHEN 'Y' THEN 1 ELSE 0 END AS BIT) AS tax,m1.gsttype,m1.othgsttype from KW_Penerimaan_invois_item  m1 left join KW_Ref_Carta_Akaun s1 on s1.kod_akaun=m1.kod_akauan left join KW_Ref_Tetapan_cukai s2 on s2.Ref_kod_cukai=m1.othgsttype left join KW_Ref_Tetapan_cukai s3 on s3.Ref_kod_cukai=m1.gsttype where m1.no_invois='" + ssv1 + "' ");
            //Gridview9.DataSource = dt1;
            //Gridview9.DataBind();

            ddakaun.Attributes.Remove("disabled");
            ddpela2.Attributes.Remove("disabled");
            txtnoruju.Attributes.Add("disabled", "disabled");
            txttcinvois.Attributes.Remove("disabled");
            ddpro1.Attributes.Remove("disabled");
            ddinvday.Attributes.Remove("disabled");
            Gridview12.Visible = false;
            ddakaun.SelectedValue = "";
            ddpela2.SelectedValue = "";

            txttcinvois.Text = "";
            ddpro1.SelectedValue = "";
            ddinvday.SelectedValue = "--- PILIH ---";
            tab3();

        }
    }

    protected void Button22_Click(object sender, EventArgs e)
    {

        if (Session["dbtno"].ToString() != "")
        {
            crt_notelink();
            Session["dbtno"] = txtnoruju.Text;
            //Response.Redirect("KW_Penerimaan_invois_Rptview.aspx");

            string url = "KW_Pembeyaraan_credit_rptview.aspx";
            string s = "window.open('" + url + "', 'popup_window', 'width=11000,height=700,left=100,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

            Binddedit();
        }

        //if (txtnoruju.Text != "")
        //{
        //    Session["dbtno"] = txtnoruju.Text;
        //    Response.Redirect("KW_Penerimaan_credit_rptview.aspx");


        //}

    }

    protected void lblSubItemdebit_Click(object sender, EventArgs e)
    {

        LinkButton btn = (LinkButton)sender;
        string[] CommadArgument = btn.CommandArgument.Split(',');
        CommandArgument1 = CommadArgument[0];
        debit_link();
    }

    void debit_link()
    {
        string ssv3 = string.Empty;
        if (CommandArgument1 != "")
        {
            ssv3 = CommandArgument1;
        }
        else
        {
            ssv3 = Session["dbtno1"].ToString();
        }
        Button5.Visible = false;
        debit_tab5.Visible = true;
        //Button24.Visible = true;
        Button21.Visible = true;
        Gridview14.Enabled = false;
        fr2.Visible = true;
        fr1.Visible = false;
        //pp15.Visible = true;


        Bindcredit();
        tab4();
        DataTable dt = new DataTable();
        dt = Dbcon.Ora_Execute_table("select untuk_akaun,nama_pelanggan,no_Rujukan,Format(tarikh_invois,'dd/MM/yyyy') tarikh_invois,Format(tarikh_credit,'dd/MM/yyyy') tarikh_invois,project_kod,no_invois,inv_day from KW_Pembayaran_Dedit_item  where no_Rujukan='" + ssv3 + "'");

        if (dt.Rows.Count > 0)
        {
            ddakaun.SelectedValue = dt.Rows[0][0].ToString();
            ddakaun.Attributes.Add("disabled", "disabled");

            ddpela3.SelectedValue = dt.Rows[0][1].ToString();
            ddpela3.Attributes.Add("disabled", "disabled");


            txtnoruj2.Text = dt.Rows[0][2].ToString();
            txtnoruj2.Attributes.Add("disabled", "disabled");

            txtdtinvois.Text = dt.Rows[0][3].ToString();
            txtdtinvois.Attributes.Add("disabled", "disabled");

            txtdeb.Text = dt.Rows[0][4].ToString();
            txtdeb.Attributes.Add("disabled", "disabled");

            ddpro2.SelectedValue = dt.Rows[0][5].ToString();
            ddpro2.Attributes.Add("disabled", "disabled");

            ddinv2.SelectedValue = dt.Rows[0][6].ToString();
            ddinv2.Attributes.Add("disabled", "disabled");

            if (dt.Rows[0][7].ToString() != "")
            {
                ddinvday2.SelectedValue = dt.Rows[0][7].ToString();
            }

            ddinvday2.Attributes.Add("disabled", "disabled");



            Session["dbtno1"] = ssv3;


            DataTable dt2 = new DataTable();
            Gridview7.Visible = true;

            dt2 = Dbcon.Ora_Execute_table("select m1.deb_kod_akaun as kod_akauan,s1.nama_akaun,s2.Ref_nama_cukai as othgstname,s3.Ref_nama_cukai as gstname,m1.no_invois as rujukan,m1.item,m1.keterangan,m1.unit as unit,m1.quantiti,m1.gst,case when m1.discount = '0.00' then '0.00' else m1.discount end as discount,m1.jumlah as jumlah,m1.Overall as Overall,m1.gstjumlah as gstjum,m1.othgstjumlah as othgst,CAST(CASE m1.tax WHEN 'Y' THEN 1 ELSE 0 END AS BIT) AS tax,m1.gsttype,m1.othgsttype from KW_Pembayaran_invoisBil_item  m1 left join KW_Ref_Carta_Akaun s1 on s1.kod_akaun=m1.deb_kod_akaun left join KW_Ref_Tetapan_cukai s2 on s2.Ref_kod_cukai=m1.othgsttype left join KW_Ref_Tetapan_cukai s3 on s3.Ref_kod_cukai=m1.gsttype where m1.no_invois='" + dt.Rows[0][6].ToString().Trim() + "' and m1.cre_kod_akaun='" + dt.Rows[0][1].ToString() + "' ");
            Gridview7.DataSource = dt2;
            Gridview7.DataBind();

            Gridview11.Visible = false;

            DataTable dt1 = new DataTable();

            Gridview14.Visible = true;
            dt1 = Dbcon.Ora_Execute_table("select m1.kod_akauan,s1.nama_akaun,s2.Ref_nama_cukai as othgstname,s3.Ref_nama_cukai as gstname,m1.keterangan,case when m1.jumlah = '0.00' then '0.00' else m1.jumlah end as jumlah  , case when m1.Overall = '0.00' then '0.00' else m1.Overall end as Overall   , case when m1.gstjumlah = '0.00' then '0.00' else m1.gstjumlah end as gstjum, case when m1.othgstjumlah = '0.00' then '0.00' else m1.othgstjumlah end as othgst   ,CAST(CASE m1.tax WHEN 'Y' THEN 1 ELSE 0 END AS BIT) AS tax,m1.gsttype,m1.othgsttype    from KW_Pembayaran_Dedit_item m1 left join KW_Ref_Carta_Akaun s1 on s1.kod_akaun=m1.kod_akauan left join KW_Ref_Tetapan_cukai s2 on s2.Ref_kod_cukai=m1.othgsttype left join KW_Ref_Tetapan_cukai s3 on s3.Ref_kod_cukai=m1.gsttype where m1.no_Rujukan='" + ssv3 + "'");
            Gridview14.DataSource = dt1;
            Gridview14.DataBind();

            DataTable dt1_jum = new DataTable();
            dt1_jum = Dbcon.Ora_Execute_table("select SUM(Overall) as Overall from KW_Pembayaran_Dedit_item where no_Rujukan='" + ssv3 + "'");
            if (dt1_jum.Rows.Count != 0)
            {
                TextBox1.Text = double.Parse(dt1_jum.Rows[0]["Overall"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
            }
            else
            {
                TextBox1.Text = "0.00";
            }
        }
    }



    protected void Button21_Click(object sender, EventArgs e)
    {

        if (Session["dbtno1"].ToString() != "")
        {
            debit_link();
            Session["dbtno1"] = txtnoruj2.Text;
            //Response.Redirect("KW_Penerimaan_invois_Rptview.aspx");

            string url = "KW_pembayaraan_debit_rptview.aspx";
            string s = "window.open('" + url + "', 'popup_window', 'width=11000,height=700,left=100,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

            Bindcredit();
        }

        //if (txtnoruj2.Text != "")
        //{
        //    Session["dbtno"] = txtnoruj2.Text;
        //    Response.Redirect("KW_Penerimaan_debit_rptview.aspx");


        //}

    }

    protected void btnprintmoh_Click(object sender, EventArgs e)
    {
        if (Session["Invoisno"].ToString() != "")
        {
            CommandArgument3 = "";
            ss_frmgrid();
            Session["Invoisno"] = txtnoper.Text;
            //Response.Redirect("KW_Pembayaran_Mohon_prtview.aspx");
            string url = "KW_Pembayaran_Mohon_prtview.aspx";
            string s = "window.open('" + url + "', 'popup_window', 'width=11000,height=700,left=100,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
    }
    protected void grd2_reset(object sender, EventArgs e)
    {
        Response.Redirect("../kewengan/kw_semak_Kewangan.aspx");
    }
    protected void gvSelected_PageIndexChanging_g2(object sender, GridViewPageEventArgs e)
    {
        Gridview2.PageIndex = e.NewPageIndex;
        Gridview2.DataBind();
        Bindbaucer();
        BindInvois();
        BindMohon();
        Bindcredit();
        Binddedit();
        tab1();
    }
    protected void BindMohon()
    {

        string fmdate = string.Empty, tmdate = string.Empty, condtion = string.Empty;
        if (TextBox15.Text != "")
        {
            DateTime fd = DateTime.ParseExact(TextBox15.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            fmdate = fd.ToString("yyyy-MM-dd");
        }
        if (txttarikhinvois.Text != "")
        {            
            DateTime fd1 = DateTime.ParseExact(txttarikhinvois.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            tmdate = fd1.ToString("yyyy-MM-dd");
        }

      
        if (TextBox15.Text != "" && txttarikhinvois.Text != "" && jenis_trxn.SelectedValue != "" && DropDownList4.SelectedValue != "")
        {
            condtion = "where a.v1 >=DATEADD(day, DATEDIFF(day, 0, '"+ fmdate + "'), 0) and a.v1<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +0) and a.jen_txi='"+ jenis_trxn.SelectedValue + "' and a.sts_cd='"+ DropDownList4.SelectedValue + "'";
        }
        else if (TextBox15.Text != "" && txttarikhinvois.Text != "" && jenis_trxn.SelectedValue == "" && DropDownList4.SelectedValue != "")
        {
            condtion = "where a.v1 >=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and a.v1<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +0) and a.sts_cd='" + DropDownList4.SelectedValue + "'";
        }
        else if (TextBox15.Text != "" && txttarikhinvois.Text != "" && jenis_trxn.SelectedValue == "" && DropDownList4.SelectedValue == "")
        {
            condtion = "where a.v1 >=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and a.v1<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +0)";
        }
        else if (TextBox15.Text != "" && txttarikhinvois.Text != "" && jenis_trxn.SelectedValue != "" && DropDownList4.SelectedValue == "")
        {
            condtion = "where a.v1 >=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and a.v1<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +0)";
        }
        else if (TextBox15.Text == "" && txttarikhinvois.Text == "" && jenis_trxn.SelectedValue != "" && DropDownList4.SelectedValue != "")
        {
            condtion = "where a.jen_txi='" + jenis_trxn.SelectedValue + "' and a.sts_cd='" + DropDownList4.SelectedValue + "'";
        }
        else if (TextBox15.Text == "" && txttarikhinvois.Text == "" && jenis_trxn.SelectedValue != "" && DropDownList4.SelectedValue == "")
        {
            condtion = "where a.jen_txi='" + jenis_trxn.SelectedValue + "' and a.sts_cd='" + DropDownList4.SelectedValue + "'";
        }
        else if (TextBox15.Text == "" && txttarikhinvois.Text == "" && jenis_trxn.SelectedValue == "" && DropDownList4.SelectedValue != "")
        {
            condtion = "where a.sts_cd='" + DropDownList4.SelectedValue + "'";
        }
        else
        {
            condtion = "where a.sts_cd=''";
        }
        

        qry1 = "select * from (select kat_penerima kat,no_permohonan rujukan,tarkih_permohonan v1,Format(tarkih_permohonan,'dd/MM/yyyy') tk_mohon,'03' as jen_txi,b.mhn_byr_kepada as  Bayar_kepada,a.jumlah as  jumlah,b.mhn_keterangan as keterengan,ISNULL(a.semak_sts,'') sts_cd,CASE ISNULL(a.semak_sts,'')  WHEN '' THEN 'WAITING'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' END  as status,ISNULL(lulus_sts,'') lul_sts_cd,CASE ISNULL(lulus_sts,'')  WHEN '' THEN 'WAITING'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' END  as lul_status  from KW_Pembayaran_mohon a OUTER APPLY (select top(1)* from KW_Pembayaran_mohon_item s2 where s2.Status='A' and s2.mhn_no_permohonan=a.no_permohonan order by cr_dt asc) as b  where a.Status='A' and ISNULL(a.chk_sts,'')='L'"
        + " union all select kat_penerima kat,a.no_invois rujukan,tarkih_mohon v1,Format(tarkih_mohon,'dd/MM/yyyy') tk_mohon,'01' as jen_txi,Bayar_kepada as  Bayar_kepada,a.Overall as  jumlah,perkara as keterengan,ISNULL(a.semak_sts,'') sts_cd,CASE ISNULL(a.semak_sts,'')  WHEN '' THEN 'WAITING'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' END  as status,ISNULL(lulus_sts,'') lul_sts_cd,CASE ISNULL(lulus_sts,'')  WHEN '' THEN 'WAITING'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' END  as lul_status  from KW_Pembayaran_invois a OUTER APPLY (select top(1)* from KW_Penerimaan_invois_item s2 where s2.Status='A' and s2.no_invois=a.no_invois order by cr_dt asc) as b  where a.Status='A'"
        + " union all select '03' kat,a.no_invois rujukan,tarikh_invois v1,Format(tarikh_invois,'dd/MM/yyyy') tk_mohon,'09' as jen_txi,nama_pelanggan_code as  Bayar_kepada,a.Overall as  jumlah,perkera as keterengan,ISNULL(a.semak_sts,'') sts_cd,CASE ISNULL(a.semak_sts,'')  WHEN '' THEN 'WAITING'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' END  as status,ISNULL(lulus_sts,'') lul_sts_cd,CASE ISNULL(lulus_sts,'')  WHEN '' THEN 'WAITING'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' END  as lul_status  from KW_Penerimaan_invois a OUTER APPLY (select top(1)* from KW_Pembayaran_invoisBil_item s2 where s2.Status='A' and s2.no_invois=a.no_invois order by cr_dt asc) as b  where a.Status='A'"
        + " union all SELECT '02' kat,no_Rujukan rujukan,tarikh_debit v1, Format(tarikh_debit,'dd/MM/yyyy') tk_mohon,'06' as jen_txi, nama_pembekal_code as Bayar_kepada,Overall as  jumlah,perkera keterangan,ISNULL(semak_sts,'') sts_cd,CASE ISNULL(semak_sts,'')  WHEN '' THEN 'WAITING'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' END  as status,ISNULL(lulus_sts,'') lul_sts_cd,CASE ISNULL(lulus_sts,'')  WHEN '' THEN 'WAITING'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' END  as lul_status FROM KW_Pembayaran_Dedit "
        + " union all SELECT '02' kat,no_Rujukan rujukan,tarikh_kredit v1, Format(tarikh_kredit,'dd/MM/yyyy') tk_mohon,'05' as jen_txi, nama_pembekal_code as Bayar_kepada,Overall as  jumlah,perkera keterangan,ISNULL(semak_sts,'') sts_cd,CASE ISNULL(semak_sts,'')  WHEN '' THEN 'WAITING'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' END  as status,ISNULL(lulus_sts,'') lul_sts_cd,CASE ISNULL(lulus_sts,'')  WHEN '' THEN 'WAITING'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' END  as lul_status FROM KW_Pembayaran_Credit "
        + " union all SELECT '03' kat,no_notakredit rujukan,tarikh_kredit v1, Format(tarikh_kredit,'dd/MM/yyyy') tk_mohon,'10' as jen_txi, nama_pelanggan_code as Bayar_kepada,Overall as  jumlah,perkara keterangan,ISNULL(semak_sts,'') sts_cd,CASE ISNULL(semak_sts,'')  WHEN '' THEN 'WAITING'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' END  as status,ISNULL(lulus_sts,'') lul_sts_cd,CASE ISNULL(lulus_sts,'')  WHEN '' THEN 'WAITING'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' END  as lul_status FROM KW_Penerimaan_Credit "
        + " union all SELECT '03' kat,no_notadebit rujukan,tarikh_debit v1, Format(tarikh_debit,'dd/MM/yyyy') tk_mohon,'11' as jen_txi, nama_pelanggan_code as Bayar_kepada,Overall as  jumlah,perkara keterangan,ISNULL(semak_sts,'') sts_cd,CASE ISNULL(semak_sts,'')  WHEN '' THEN 'WAITING'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' END  as status,ISNULL(lulus_sts,'') lul_sts_cd,CASE ISNULL(lulus_sts,'')  WHEN '' THEN 'WAITING'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' END  as lul_status FROM KW_Penerimaan_Debit"
        + " union all SELECT '11' kat,no_permohonan rujukan,tarikh_lulus v1, Format(tarikh_lulus,'dd/MM/yyyy') tk_mohon,Jenis_permohonan as jen_txi, nama_pelanggan_code as Bayar_kepada,Overall as  jumlah,perkara keterangan,ISNULL(semak_sts,'') sts_cd,CASE ISNULL(semak_sts,'')  WHEN '' THEN 'WAITING'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' END  as status,ISNULL(lulus_sts,'') lul_sts_cd,CASE ISNULL(lulus_sts,'')  WHEN '' THEN 'WAITING'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' END  as lul_status FROM KW_jurnal_inter) as a "
        + " "+ condtion + " order by a.tk_mohon ASC";

        SqlCommand cmd2 = new SqlCommand(qry1, con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            Gridview2.DataSource = ds2;
            Gridview2.DataBind();
            int columncount = Gridview2.Rows[0].Cells.Count;
            Gridview2.Rows[0].Cells.Clear();
            Gridview2.Rows[0].Cells.Add(new TableCell());
            Gridview2.Rows[0].Cells[0].ColumnSpan = columncount;
            Gridview2.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
        }
        else
        {
            Gridview2.DataSource = ds2;
            Gridview2.DataBind();
        }

    }

  
    protected void gvSelected_PageIndexChanging_IB(object sender, GridViewPageEventArgs e)
    {
        grdBinv.PageIndex = e.NewPageIndex;
        grdBinv.DataBind();
        Bindbaucer();
        BindInvois();
        BindMohon();
        Bindcredit();
        Binddedit();
        tab5();
    }
    protected void BindInvois()
    {
        //string fmdate = string.Empty;
        //if (TextBox25.Text != "")
        //{
        //    string fdate = TextBox25.Text;
        //    DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //    fmdate = fd.ToString("yyyy-MM-dd") + " 12:00:00.000";
        //}

        //string smb_clk1 = string.Empty;
        //if (chk_invbill.Checked == true)
        //{

        //    smb_clk1 = "";

        //}
        //else
        //{
        //    if (TextBox6.Text == "" && TextBox25.Text == "")
        //    {
        //        smb_clk1 = "where tarkih_invois>=DATEADD(day, DATEDIFF(day, 0, '" + str_sdt1 + "'), 0) and tarkih_invois<=DATEADD(day, DATEDIFF(day, 0, '" + end_edt1 + "'), +1)";
        //    }
        //    else
        //    {
        //        smb_clk1 = "and tarkih_invois>=DATEADD(day, DATEDIFF(day, 0, '" + str_sdt1 + "'), 0) and tarkih_invois<=DATEADD(day, DATEDIFF(day, 0, '" + end_edt1 + "'), +1)";
        //    }
        //}

        //if (TextBox6.Text != "" && TextBox25.Text == "")
        //{
        //    qry1 = "select Data,tarkih_mohon,a.no_permohonan,a.tarkih_invois,jumlah,Payamt, Cast(jumlah as money )- Cast(Payamt as money ) Baki from (select Data,tarkih_mohon,a.no_permohonan,a.tarkih_invois,jumlah,isnull(Payamt,'0.00') Payamt from (select I.Data,Format(tarkih_mohon,'dd/MM/yyyy')tarkih_mohon,I.No_permohonan,Format(tarkih_invois,'dd/MM/yyyy')tarkih_invois,Format(sum(I.Overall),'#,##,###.00') jumlah from KW_Pembayaran_invoisBil_item I where I.no_permohonan='" + TextBox6.Text + "' "+ smb_clk1 + " group by I.Data,tarkih_mohon,I.No_permohonan,tarkih_invois) a left join (select no_permohonan,case when p.Status='L' Then Format(sum(P.payamt),'#,##,###.00')  else  '0.00'  End Payamt from KW_Pembayaran_Pay_voucer P where Status='L' group by no_permohonan,p.Status) b on  a.no_permohonan=b.no_Permohonan) a order by a.tarkih_invois";
        //}
        //else if (TextBox6.Text == "" && TextBox25.Text != "")
        //{
        //    qry1 = "select Data,tarkih_mohon,a.no_permohonan,a.tarkih_invois,jumlah,Payamt, Cast(jumlah as money )- Cast(Payamt as money ) Baki from (select Data,tarkih_mohon,a.no_permohonan,a.tarkih_invois,jumlah,isnull(Payamt,'0.00') Payamt from (select I.Data,Format(tarkih_mohon,'dd/MM/yyyy')tarkih_mohon,I.No_permohonan,Format(tarkih_invois,'dd/MM/yyyy')tarkih_invois,Format(sum(I.Overall),'#,##,###.00') jumlah from KW_Pembayaran_invoisBil_item I where I.tarkih_mohon='" + fmdate + "' " + smb_clk1 + " group by I.Data,tarkih_mohon,I.No_permohonan,tarkih_invois) a left join (select no_permohonan,case when p.Status='L' Then Format(sum(P.payamt),'#,##,###.00')  else  '0.00'  End Payamt from KW_Pembayaran_Pay_voucer P where Status='L' group by no_permohonan,p.Status) b on  a.no_permohonan=b.no_Permohonan) a order by a.tarkih_invois";
        //}
        //else if (TextBox6.Text != "" && TextBox25.Text != "")
        //{
        //    qry1 = "select Data,tarkih_mohon,a.no_permohonan,a.tarkih_invois,jumlah,Payamt, Cast(jumlah as money )- Cast(Payamt as money ) Baki from (select Data,tarkih_mohon,a.no_permohonan,a.tarkih_invois,jumlah,isnull(Payamt,'0.00') Payamt from (select I.Data,Format(tarkih_mohon,'dd/MM/yyyy')tarkih_mohon,I.No_permohonan,Format(tarkih_invois,'dd/MM/yyyy')tarkih_invois,Format(sum(I.Overall),'#,##,###.00') jumlah from KW_Pembayaran_invoisBil_item I where I.no_permohonan='" + TextBox6.Text + "' and a.tarkih_mohon='" + fmdate + "' " + smb_clk1 + " group by I.Data,tarkih_mohon,I.No_permohonan,tarkih_invois) a left join (select no_permohonan,case when p.Status='L' Then Format(sum(P.payamt),'#,##,###.00')  else  '0.00'  End Payamt from KW_Pembayaran_Pay_voucer P where Status='L' group by no_permohonan,p.Status) b on  a.no_permohonan=b.no_Permohonan) a order by a.tarkih_invois";
        //}
        //else
        //{
        //    qry1 = "select Data,tarkih_mohon,a.no_permohonan,a.tarkih_invois,jumlah,Payamt, Cast(jumlah as money )- Cast(Payamt as money ) Baki from (select Data,tarkih_mohon,a.no_permohonan,a.tarkih_invois,jumlah,isnull(Payamt,'0.00') Payamt from (select I.Data,Format(tarkih_mohon,'dd/MM/yyyy')tarkih_mohon,I.No_permohonan,Format(tarkih_invois,'dd/MM/yyyy')tarkih_invois,Format(sum(I.Overall),'#,##,###.00') jumlah from KW_Pembayaran_invoisBil_item I " + smb_clk1 + " group by I.Data,tarkih_mohon,I.No_permohonan,tarkih_invois) a left join (select no_permohonan,case when p.Status='L' Then Format(sum(P.payamt),'#,##,###.00')  else  '0.00'  End Payamt from KW_Pembayaran_Pay_voucer P where Status='L' group by no_permohonan,p.Status) b on  a.no_permohonan=b.no_Permohonan) a order by a.tarkih_invois";
        //}
        //SqlCommand cmd2 = new SqlCommand("" + qry1 + "", con);
        //SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        //DataSet ds2 = new DataSet();
        //da2.Fill(ds2);
        //if (ds2.Tables[0].Rows.Count == 0)
        //{
        //    ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
        //    grdBinv.DataSource = ds2;
        //    grdBinv.DataBind();
        //    int columncount = grdBinv.Rows[0].Cells.Count;
        //    grdBinv.Rows[0].Cells.Clear();
        //    grdBinv.Rows[0].Cells.Add(new TableCell());
        //    grdBinv.Rows[0].Cells[0].ColumnSpan = columncount;
        //    grdBinv.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
        //}
        //else
        //{
        //    grdBinv.DataSource = ds2;
        //    grdBinv.DataBind();
        //}

    }

    protected void gvSelected_PageIndexChanging_t3(object sender, GridViewPageEventArgs e)
    {
        grdinvoisview.PageIndex = e.NewPageIndex;
        grdinvoisview.DataBind();
        Bindbaucer();
        BindInvois();
        BindMohon();
        Bindcredit();
        Binddedit();
        tab2();
    }
    protected void Bindbaucer()
    {
        //string fmdate = string.Empty;
        //if (TextBox10.Text != "")
        //{
        //    string fdate = TextBox10.Text;
        //    DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //    fmdate = fd.ToString("yyyy-MM-dd") + " 12:00:00.000";
        //}

        //string smb_clk2 = string.Empty;
        //if (chk_pvouch.Checked == true)
        //{

        //    smb_clk2 = "";

        //}
        //else
        //{
        //    if (TextBox8.Text == "" && TextBox10.Text == "")
        //    {
        //        smb_clk2 = "where tarkih_pv>=DATEADD(day, DATEDIFF(day, 0, '" + str_sdt1 + "'), 0) and tarkih_pv<=DATEADD(day, DATEDIFF(day, 0, '" + end_edt1 + "'), +1)";
        //    }
        //    else
        //    {
        //        smb_clk2 = "and tarkih_pv>=DATEADD(day, DATEDIFF(day, 0, '" + str_sdt1 + "'), 0) and tarkih_pv<=DATEADD(day, DATEDIFF(day, 0, '" + end_edt1 + "'), +1)";
        //    }
        //}

        //if (TextBox8.Text != "" && TextBox10.Text == "")
        //{
        //    qry1 = "select Pv_no,Format(tarkih_pv,'dd/MM/yyyy') tarkih_pv,sum(payamt) PaidAmount,No_cek,Akaun_name,CASE Status  WHEN 'B' THEN 'BARU'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' WHEN 'G' THEN 'GAGAL' END  as status from KW_Pembayaran_Pay_voucer where Pv_no='" + TextBox8.Text + "' "+ smb_clk2 + " group by Pv_no,tarkih_pv,status,No_cek,Akaun_name";
        //}
        //else if (TextBox8.Text == "" && TextBox10.Text != "")
        //{
        //    qry1 = "select Pv_no,Format(tarkih_pv,'dd/MM/yyyy') tarkih_pv,sum(payamt) PaidAmount,No_cek,Akaun_name,CASE Status  WHEN 'B' THEN 'BARU'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' WHEN 'G' THEN 'GAGAL' END  as status from KW_Pembayaran_Pay_voucer where tarkih_pv='" + fmdate + "' " + smb_clk2 + "  group by Pv_no,tarkih_pv,status,No_cek,Akaun_name";
        //}
        //else if (TextBox8.Text != "" && TextBox10.Text != "")
        //{
        //    qry1 = "select Pv_no,Format(tarkih_pv,'dd/MM/yyyy') tarkih_pv,sum(payamt) PaidAmount,No_cek,Akaun_name,CASE Status  WHEN 'B' THEN 'BARU'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' WHEN 'G' THEN 'GAGAL' END  as status from KW_Pembayaran_Pay_voucer where Pv_no='" + TextBox8.Text + "' and tarkih_pv='" + fmdate + "' " + smb_clk2 + "  group by Pv_no,tarkih_pv,status,No_cek,Akaun_name";
        //}
        //else
        //{
        //    qry1 = "select Pv_no,Format(tarkih_pv,'dd/MM/yyyy') tarkih_pv,sum(payamt) PaidAmount,No_cek,Akaun_name,CASE Status  WHEN 'B' THEN 'BARU'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' WHEN 'G' THEN 'GAGAL' END  as status from KW_Pembayaran_Pay_voucer " + smb_clk2 + " group by Pv_no,tarkih_pv,status,No_cek,Akaun_name";
        //}

        //SqlCommand cmd2 = new SqlCommand("" + qry1 + "", con);
        //SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        //DataSet ds2 = new DataSet();
        //da2.Fill(ds2);
        //if (ds2.Tables[0].Rows.Count == 0)
        //{
        //    ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
        //    grdinvoisview.DataSource = ds2;
        //    grdinvoisview.DataBind();
        //    int columncount = grdinvoisview.Rows[0].Cells.Count;
        //    grdinvoisview.Rows[0].Cells.Clear();
        //    grdinvoisview.Rows[0].Cells.Add(new TableCell());
        //    grdinvoisview.Rows[0].Cells[0].ColumnSpan = columncount;
        //    grdinvoisview.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
        //}
        //else
        //{
        //    grdinvoisview.DataSource = ds2;
        //    grdinvoisview.DataBind();
        //}
    }


    protected void but_Click(object sender, EventArgs e)
    {
        Bindbaucer();
        BindInvois();
        BindMohon();
        Bindcredit();
        Binddedit();
        tab1();
    }

    protected void tab2tam_Click(object sender, EventArgs e)
    {
        Div4.Visible = false;
        Div5.Visible = true;

        Button7.Visible = true;
        GetUniqueInv();
        
        ddinv.SelectedItem.Text = "";

        reset();
        tab2();
        Bindbaucer();
        BindInvois();
        BindMohon();
        Bindcredit();
        Binddedit();
        //hd_txt.Text = "PAYMENT VOCHER";
    }

    protected void tab4tam_Click(object sender, EventArgs e)
    {


        Div12.Visible = false;
        Div13.Visible = true;
        grdbilinv.Visible = false;
        grdbilinv1.Visible = false;
        Gridview5.Visible = false;
        tab5();
        //hd_txt.Text = "INVOIS / BIL";
        Bindbaucer();
        BindInvois();
        BindMohon();
        Bindcredit();
        Binddedit();
        invois_view();
    }

    private void GetUniqueInv()
    {
        DataTable dt1 = Dbcon.Ora_Execute_table("select doc_type_cd,format as fmt,SUBSTRING(cur_format, 1, 10) as lfrmt1,SUBSTRING(cur_format, 11, 4) as lfrmt2,cur_format as cfmt from KW_Format_Nombor_rujukan where doc_type_cd='04' and Status='A'");
        if (dt1.Rows.Count != 0)
        {
            if (dt1.Rows[0]["cfmt"].ToString() == "")
            {
                txtpvno.Text = dt1.Rows[0]["fmt"].ToString();
                txtpvno.Attributes.Add("disabled", "disabled");
            }
            else
            {
                int seqno = Convert.ToInt32(dt1.Rows[0]["lfrmt2"].ToString());
                int newNumber = seqno + 1;
                uniqueId = newNumber.ToString(dt1.Rows[0]["lfrmt1"].ToString() + "0000");
                txtpvno.Text = uniqueId;
                txtpvno.Attributes.Add("disabled", "disabled");
            }

        }
        else
        {
            DataTable dt = Dbcon.Ora_Execute_table("select   ISNULL(max(SUBSTRING(Pv_no,13,2000)),'0')  from KW_Pembayaran_Pay_voucer");
            if (dt.Rows.Count > 0)
            {
                int seqno = Convert.ToInt32(dt.Rows[0][0].ToString());
                int newNumber = seqno + 1;
                uniqueId = newNumber.ToString("KTH-PVC" + "-" + DateTime.Now.ToString("yy") + "-" + "0000");
                txtpvno.Text = uniqueId;
                txtpvno.Attributes.Add("disabled", "disabled");

            }
            else
            {
                int newNumber = Convert.ToInt32(uniqueId) + 1;
                uniqueId = newNumber.ToString("KTH-PVC" + "-" + DateTime.Now.ToString("yy") + "-" + "0000");
                txtpvno.Text = uniqueId;
                txtpvno.Enabled = false;
                txtpvno.Attributes.Add("disabled", "disabled");
            }
        }



    }



    void tab1()
    {

        p6.Attributes.Add("class", "tab-pane active");
        p1.Attributes.Add("class", "tab-pane");
        p2.Attributes.Add("class", "tab-pane");
        p3.Attributes.Add("class", "tab-pane");
        p4.Attributes.Add("class", "tab-pane");

        pp6.Attributes.Add("class", "active");
        pp1.Attributes.Remove("class");
        pp2.Attributes.Remove("class");
        pp3.Attributes.Remove("class");
        pp4.Attributes.Remove("class");
        hd_txt.Text = "MOHON BAYAR";
    }

    void tab5()
    {

        p6.Attributes.Add("class", "tab-pane");
        p1.Attributes.Add("class", "tab-pane");
        p2.Attributes.Add("class", "tab-pane");
        p3.Attributes.Add("class", "tab-pane");
        p4.Attributes.Add("class", "tab-pane active");

        pp6.Attributes.Remove("class");
        pp1.Attributes.Remove("class");
        pp2.Attributes.Remove("class");
        pp3.Attributes.Remove("class");
        pp4.Attributes.Add("class", "active");
        hd_txt.Text = "INVOIS / BIL";
    }

    void tab2()
    {

        p6.Attributes.Add("class", "tab-pane");
        p1.Attributes.Add("class", "tab-pane active");
        p2.Attributes.Add("class", "tab-pane");
        p3.Attributes.Add("class", "tab-pane");
        p4.Attributes.Add("class", "tab-pane");

        pp6.Attributes.Remove("class");
        pp1.Attributes.Add("class", "active");
        pp2.Attributes.Remove("class");
        pp3.Attributes.Remove("class");
        pp4.Attributes.Remove("class");
        hd_txt.Text = "PAYMENT VOCHER";
    }


    protected void Button8_Click(object sender, EventArgs e)
    {
        if (ddakaun.SelectedItem.Text != "--- PILIH ---")
        {
            if (dddata.SelectedItem.Text == "MOHON BAYAR")
            {
                if (ddnoper.SelectedItem.Text != "--- PILIH ---")
                {
                    string rcount = string.Empty;
                    int count = 0;
                    foreach (GridViewRow gvrow in grdbilinv1.Rows)
                    {
                        var rb = gvrow.FindControl("chkbind") as System.Web.UI.WebControls.RadioButton;
                        if (rb.Checked)
                        {
                            count++;
                        }
                        rcount = count.ToString();
                    }

                    if (rcount != "0")
                    {
                        foreach (GridViewRow g2 in grdbilinv1.Rows)
                        {

                            string tarmoh1 = (g2.FindControl("Label1_1") as Label).Text;
                            string jurnal_no_pv = (g2.FindControl("lbl_jurn_no") as Label).Text;
                            string tarinv1 = (g2.FindControl("Label2_1") as Label).Text;
                            string noinv1 = (g2.FindControl("Label3_1") as Label).Text;

                            string ket1 = (g2.FindControl("Label5_1") as Label).Text;
                            string total1 = (g2.FindControl("Label6_1") as Label).Text;
                            string gst1 = (g2.FindControl("Label7_1") as Label).Text;
                            string gtotal1 = (g2.FindControl("Label8_1") as Label).Text;
                            DateTime tDate = DateTime.ParseExact(tarmoh1.ToString(), "dd/MM/yyyy", null);
                            DateTime tDate1 = DateTime.ParseExact(tarinv1.ToString(), "dd/MM/yyyy", null);
                            if ((g2.FindControl("chkbind") as RadioButton).Checked == true)
                            {
                                DataTable dt = new DataTable();
                                DataTable dtI = new DataTable();
                                DataTable dtGK = new DataTable();
                                DataTable dtGD = new DataTable();
                                DataTable dt2 = new DataTable();
                                DataTable dtak = new DataTable();


                                userid = Session["New"].ToString();



                                foreach (GridViewRow g1 in grdbilinv.Rows)
                                {
                                    string DD = (g1.FindControl("ddkodKat") as DropDownList).SelectedItem.Text;
                                    string DDcre = (g1.FindControl("ddkodcre") as DropDownList).SelectedItem.Value;
                                    string DDdeb = (g1.FindControl("ddkodBil") as DropDownList).SelectedItem.Value;
                                    string ddpro = (g1.FindControl("ddprobil") as DropDownList).SelectedItem.Value;
                                    string unit = (g1.FindControl("TextBox55") as TextBox).Text;
                                    string qty = (g1.FindControl("TextBox56") as TextBox).Text;
                                    string dis = (g1.FindControl("Txtdis") as TextBox).Text;
                                    string total = (g1.FindControl("Label1") as Label).Text;
                                    string Ogst = (g1.FindControl("Label10") as Label).Text;
                                    string gst = (g1.FindControl("Label8") as Label).Text;
                                    string gtotal = (g1.FindControl("Label5") as Label).Text;

                                    if (DD != "--- PILIH ---")
                                    {
                                        if (DDcre != "--- PILIH ---")
                                        {
                                            if (DDdeb != "--- PILIH ---")
                                            {
                                                string pro;
                                                if (DDdeb != "--- PILIH ---")
                                                {
                                                    pro = ddpro;
                                                }
                                                else
                                                {
                                                    pro = "";
                                                }
                                                string DDcukai = (g1.FindControl("ddcukaiinv") as DropDownList).SelectedItem.Text;
                                                DataTable dtcuk = new DataTable();
                                                dtcuk = Dbcon.Ora_Execute_table("select Ref_kadar,Ref_kod_akaun,kat_akaun from KW_Ref_Tetapan_cukai where  Ref_nama_cukai='" + DDcukai + "'");
                                                string DDcukaival = (g1.FindControl("ddcukaiinv") as DropDownList).SelectedItem.Value;
                                                string cuk, taxkod, katkod;
                                                if (dtcuk.Rows.Count > 0)
                                                {
                                                    cuk = dtcuk.Rows[0][0].ToString();
                                                    taxkod = dtcuk.Rows[0][1].ToString();
                                                    katkod = dtcuk.Rows[0][2].ToString();
                                                }
                                                else
                                                {
                                                    cuk = "0";
                                                    taxkod = "0";
                                                    katkod = "0";
                                                }
                                                string DDcukai1 = (g1.FindControl("ddcukaioth") as DropDownList).SelectedItem.Text;
                                                DataTable dtcuk1 = new DataTable();
                                                dtcuk1 = Dbcon.Ora_Execute_table("select Ref_kadar,Ref_kod_akaun,kat_akaun from KW_Ref_Tetapan_cukai where  Ref_nama_cukai='" + DDcukai1 + "'");
                                                string DDcukaival1 = (g1.FindControl("ddcukaioth") as DropDownList).SelectedItem.Value;
                                                string cuk1, taxkod1, katkod1;
                                                if (dtcuk1.Rows.Count > 0)
                                                {
                                                    cuk1 = dtcuk1.Rows[0][0].ToString();
                                                    taxkod1 = dtcuk1.Rows[0][1].ToString();
                                                    katkod1 = dtcuk1.Rows[0][2].ToString();
                                                }
                                                else
                                                {
                                                    cuk1 = "0";
                                                    taxkod1 = "0";
                                                    katkod1 = "0";
                                                }
                                                string fre;
                                                if (dis == "")
                                                {
                                                    fre = "0.00";
                                                }
                                                else
                                                {
                                                    fre = dis;
                                                }



                                                string chk, val, val1;
                                                decimal tgst = 0;
                                                decimal ogst = 0;
                                                decimal tgst1 = 0;

                                                if ((g1.FindControl("CheckBox1") as CheckBox).Checked == true)
                                                {
                                                    chk = "Y";
                                                    if (DDcukaival != "--- PILIH ---")
                                                    {
                                                        val = DDcukaival;
                                                        tgst = Convert.ToDecimal(gst);

                                                    }
                                                    else
                                                    {
                                                        val = "0";
                                                        tgst = Convert.ToDecimal("0.00");
                                                    }
                                                    if (DDcukaival1 != "--- PILIH ---")
                                                    {
                                                        val1 = DDcukaival1;

                                                        tgst1 = Convert.ToDecimal(Ogst);
                                                    }
                                                    else
                                                    {
                                                        val1 = "0";
                                                        tgst1 = Convert.ToDecimal("0.00");
                                                    }
                                                }
                                                else
                                                {
                                                    chk = "N";
                                                    if (DDcukaival != "--- PILIH ---")
                                                    {
                                                        val = DDcukaival;
                                                        tgst = Convert.ToDecimal(gst);
                                                    }
                                                    else
                                                    {
                                                        val = "0";
                                                        tgst = Convert.ToDecimal("0.00");
                                                    }
                                                    if (DDcukaival1 != "--- PILIH ---")
                                                    {
                                                        val1 = DDcukaival1;
                                                        tgst1 = Convert.ToDecimal(Ogst);
                                                    }
                                                    else
                                                    {
                                                        val1 = "0";
                                                        tgst1 = Convert.ToDecimal("0.00");
                                                    }
                                                }

                                                dt = Dbcon.Ora_Execute_table("insert into KW_Pembayaran_invoisBil_item values('" + ddakaun.SelectedItem.Value + "','" + dddata.SelectedItem.Text + "','" + ddnoper.SelectedItem.Text + "','" + DD + "','" + DDcre + "','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "','" + tDate1.ToString("yyyy/MM/dd hh:mm:ss") + "','" + noinv1 + "','','" + DDdeb + "','','" + ket1 + "','" + ddpro + "','" + unit + "','" + qty + "','" + fre + " ','" + total + "','" + chk + "','" + cuk + "','" + cuk1 + "','" + val + "','" + val1 + "','" + tgst + "','" + tgst1 + "','" + gtotal + "','B','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','','','" + jurnal_no_pv + "')");
                                                dt2 = Dbcon.Ora_Execute_table("update KW_Pembayaran_mohon set Inv_status='Y' ,upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where no_Invois='" + noinv1 + "' and no_permohonan='" + ddnoper.SelectedItem.Text + "' ");

                                                // update Aset integration
                                                DataTable chk_pur_asset = new DataTable();
                                                chk_pur_asset = DBCon.Ora_Execute_table("select * from ast_purchase where pur_approve_sts_cd='01' and pur_mp_sts='Proses' and pur_po_no='" + jurnal_no_pv + "' and ISNULL(pur_del_ind,'') != 'D'");
                                                if (chk_pur_asset.Rows.Count != 0)
                                                {
                                                    DataTable upd_purchase = new DataTable();
                                                    upd_purchase = Dbcon.Ora_Execute_table("update ast_purchase set pur_inv_no='" + noinv1 + "',pur_upd_id='" + userid + "',pur_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where pur_approve_sts_cd='01' and pur_mp_sts='Proses' and pur_po_no='" + jurnal_no_pv + "' and ISNULL(pur_del_ind,'') != 'D'");
                                                }

                                                DataTable dtkat1 = new DataTable();
                                                dtkat1 = Dbcon.Ora_Execute_table("select kat_akaun from KW_Ref_Carta_Akaun where kod_akaun='" + DDcre + "'");

                                                DataTable dtkat2 = new DataTable();
                                                dtkat2 = Dbcon.Ora_Execute_table("select kat_akaun from KW_Ref_Carta_Akaun where kod_akaun='" + DDdeb + "'");
                                                dtGK = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + DDcre + "','','" + gtotal + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + dtkat1.Rows[0][0].ToString() + "','" + ddnoper.SelectedItem.Text + "','" + noinv1 + "','','" + gtotal + "','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "','" + ket1 + "','' ,'" + DDcre + "','" + pro + "','P','A','')");

                                                //1
                                                DataTable chk_carta1 = new DataTable();
                                                chk_carta1 = Dbcon.Ora_Execute_table("select kat_akaun,kod_akaun,KW_Debit_amt,KW_kredit_amt,Kw_open_amt from KW_Ref_Carta_Akaun where kod_akaun='" + DDcre + "'");
                                                if (chk_carta1.Rows.Count != 0)
                                                {
                                                    double amt_ope2 = 0;
                                                    double amt_deb2 = 0;
                                                    double amt_deb3 = 0;
                                                    DataTable chk_kategory1 = new DataTable();
                                                    chk_kategory1 = Dbcon.Ora_Execute_table("select kat_cd,kat_akuan,bal_type from KW_Kategori_akaun where kat_cd='" + chk_carta1.Rows[0]["kat_akaun"].ToString() + "'");

                                                    amt_deb2 = (double.Parse(chk_carta1.Rows[0]["KW_kredit_amt"].ToString()) + double.Parse(gtotal));

                                                    if (chk_kategory1.Rows.Count != 0)
                                                    {
                                                        amt_deb3 = -(double.Parse(gtotal));
                                                        if (chk_kategory1.Rows[0]["bal_type"].ToString() == "D")
                                                        {
                                                           
                                                            amt_ope2 = (double.Parse(chk_carta1.Rows[0]["Kw_open_amt"].ToString()) + (amt_deb3));
                                                        }
                                                        else
                                                        {
                                                            amt_ope2 = (double.Parse(chk_carta1.Rows[0]["Kw_open_amt"].ToString()) + (amt_deb3));
                                                        }
                                                    }
                                                    DataTable upd_carta = new DataTable();
                                                    upd_carta = Dbcon.Ora_Execute_table("UPDATE KW_Ref_Carta_Akaun set KW_kredit_amt='" + amt_deb2 + "',Kw_open_amt='" + amt_ope2 + "' where  kod_akaun='" + DDcre + "'");
                                                }
                                                //2

                                                dtGK = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + DDdeb + "','" + total + "','','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + dtkat2.Rows[0][0].ToString() + "','" + ddnoper.SelectedItem.Text + "','" + noinv1 + "','','" + total + "','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "','" + ket1 + "','' ,'" + DDdeb + "','" + pro + "','P','A','')");
                                                //update certa Akaun
                                                DataTable chk_carta = new DataTable();
                                                chk_carta = Dbcon.Ora_Execute_table("select kat_akaun,kod_akaun,KW_Debit_amt,KW_kredit_amt,Kw_open_amt from KW_Ref_Carta_Akaun where kod_akaun='" + DDdeb + "'");
                                                if (chk_carta.Rows.Count != 0)
                                                {
                                                    double amt_ope1 = 0;
                                                    double amt_deb1 = 0;
                                                    double amt_deb2_1 = 0;

                                                    DataTable chk_kategory = new DataTable();
                                                    chk_kategory = Dbcon.Ora_Execute_table("select kat_cd,kat_akuan,bal_type from KW_Kategori_akaun where kat_cd='" + chk_carta.Rows[0]["kat_akaun"].ToString() + "'");

                                                    amt_deb1 = (double.Parse(chk_carta.Rows[0]["KW_Debit_amt"].ToString()) + double.Parse(total));

                                                    if (chk_kategory.Rows.Count != 0)
                                                    {
                                                        if (chk_kategory.Rows[0]["bal_type"].ToString() == "D")
                                                        {
                                                           
                                                            amt_ope1 = (double.Parse(chk_carta.Rows[0]["Kw_open_amt"].ToString()) + double.Parse(total));
                                                        }
                                                        else
                                                        {
                                                            amt_deb2_1 = (double.Parse(total));
                                                            amt_ope1 = (double.Parse(chk_carta.Rows[0]["Kw_open_amt"].ToString()) + (double.Parse(total)));
                                                        }
                                                    }
                                                    DataTable upd_carta = new DataTable();
                                                    upd_carta = Dbcon.Ora_Execute_table("UPDATE KW_Ref_Carta_Akaun set KW_Debit_amt='" + amt_deb1 + "',Kw_open_amt='" + amt_ope1 + "' where  kod_akaun='" + DDdeb + "'");
                                                }
                                                // end

                                                if (tgst != 0)
                                                {
                                                    dtGK = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + taxkod + "','" + tgst + "','','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + katkod + "','" + ddnoper.SelectedItem.Text + "','" + noinv1 + "','" + val + "','" + gtotal + "','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "' ,'" + ket1 + "','','" + taxkod + "','" + pro + "','P','A','')");
                                                }

                                                if (tgst1 != 0)
                                                {
                                                    dtGK = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + taxkod1 + "','" + tgst1 + "','','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + katkod1 + "','" + ddnoper.SelectedItem.Text + "','" + noinv1 + "','" + val1 + "','" + gtotal + "','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "' ,'" + ket1 + "','','" + taxkod1 + "','" + pro + "','P','A','')");
                                                }

                                                if (ddbkepada.SelectedItem.Text == "Keahlian")
                                                {
                                                    DataTable upd_div = new DataTable();
                                                    upd_div = Dbcon.Ora_Execute_table("update mem_divident set KE_EFT_Flag ='2' where KE_EFT_File_no='" + jurnal_no.Text + "'");
                                                }

                                            }
                                            else
                                            {
                                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Debit Kod Akaun.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                                            }
                                        }
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Credit Kod Akaun.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                                        }
                                    }
                                }

                            }

                        }
                        Div12.Visible = true;
                        Div13.Visible = false;
                        reset();
                        tab5();
                        Bindbaucer();
                        BindInvois();
                        BindMohon();
                        Bindcredit();
                        Binddedit();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Invois Berjaya Dibuat.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    }
                    else
                    {
                        tab5();
                        Bindbaucer();
                        BindInvois();
                        BindMohon();
                        Bindcredit();
                        Binddedit();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Nama Pembekal.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else if (dddata.SelectedItem.Text == "BARU")
            {

                //if (txtnoperbil.Text != "")
                //{
                if (ddkataka.SelectedItem.Text != "--- PILIH ---")
                {
                    if (ddkodaka.SelectedItem.Text != "--- PILIH ---")
                    {
                        if (txttmb.Text != "")
                        {
                            if (txttinvbil.Text != "")
                            {
                                if (txrinvbil.Text != "")
                                {
                                    string main_nopvtxt = string.Empty, main_nopvtxt_1 = string.Empty, nopvtxt = string.Empty;
                                    if (txtnoperbil.Text == "")
                                    {
                                       

                                        DataTable dt1_1 = Dbcon.Ora_Execute_table("select doc_type_cd,format as fmt,SUBSTRING(cur_format, 1, 10) as lfrmt1,SUBSTRING(cur_format, 11, 4) as lfrmt2,cur_format as cfmt from KW_Format_Nombor_rujukan where doc_type_cd='03' and Status='A'");
                                        if (dt1_1.Rows.Count != 0)
                                        {
                                            if (dt1_1.Rows[0]["cfmt"].ToString() == "")
                                            {
                                                nopvtxt = dt1_1.Rows[0]["fmt"].ToString();
                                            }
                                            else
                                            {
                                                int seqno = Convert.ToInt32(dt1_1.Rows[0]["lfrmt2"].ToString());
                                                int newNumber = seqno + 1;
                                                uniqueId = newNumber.ToString(dt1_1.Rows[0]["lfrmt1"].ToString() + "0000");
                                                nopvtxt = uniqueId;  
                                            }
                                        }
                                        else
                                        {
                                            DataTable dt2_2 = Dbcon.Ora_Execute_table("select   ISNULL(max(SUBSTRING(no_permohonan,13,3000)),'0') from KW_Pembayaran_mohon");
                                            if (dt2_2.Rows.Count > 0)
                                            {
                                                int seqno = Convert.ToInt32(dt2_2.Rows[0][0].ToString());
                                                int newNumber = seqno + 1;
                                                uniqueId = newNumber.ToString("KTH-MHN" + " - " + DateTime.Now.ToString("yy") + " - " + "0000");
                                                nopvtxt = uniqueId;
                                            }
                                            else
                                            {
                                                int newNumber = Convert.ToInt32(uniqueId) + 1;
                                                uniqueId = newNumber.ToString("KTH-MHN" + " - " + DateTime.Now.ToString("yy") + " - " + "0000");
                                                nopvtxt = uniqueId;
                                            }
                                        }
                                        main_nopvtxt = nopvtxt;

                                        DataTable dt_upd_format = new DataTable();
                                        dt_upd_format = Dbcon.Ora_Execute_table("update KW_Format_Nombor_rujukan set cur_format='" + main_nopvtxt + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where doc_type_cd='03' and Status = 'A'");
                                    }
                                    else
                                    {
                                        main_nopvtxt = txtnoperbil.Text;
                                    }

                                    DataTable gen_invkd = new DataTable();
                                    gen_invkd = Dbcon.Ora_Execute_table("select no_permohonan from KW_Pembayaran_invoisBil_item where no_permohonan='" + main_nopvtxt + "'");
                                    if (gen_invkd.Rows.Count > 0)
                                    {
                                        DataTable dt1iv_1 = Dbcon.Ora_Execute_table("select doc_type_cd,format as fmt,SUBSTRING(cur_format, 1, 10) as lfrmt1,SUBSTRING(cur_format, 11, 4) as lfrmt2,cur_format as cfmt from KW_Format_Nombor_rujukan where doc_type_cd='03' and Status='A'");
                                        if (dt1iv_1.Rows.Count != 0)
                                        {
                                            if (dt1iv_1.Rows[0]["cfmt"].ToString() == "")
                                            {
                                                main_nopvtxt_1 = dt1iv_1.Rows[0]["fmt"].ToString();
                                            }
                                            else
                                            {
                                                int seqno = Convert.ToInt32(dt1iv_1.Rows[0]["lfrmt2"].ToString());
                                                int newNumber = seqno + 1;
                                                uniqueId = newNumber.ToString(dt1iv_1.Rows[0]["lfrmt1"].ToString() + "0000");
                                                main_nopvtxt_1 = uniqueId;
                                            }
                                        }
                                        else
                                        {
                                            DataTable dt1iv_2 = Dbcon.Ora_Execute_table("select   ISNULL(max(SUBSTRING(no_permohonan,13,3000)),'0') from KW_Pembayaran_mohon");
                                            if (dt1iv_2.Rows.Count > 0)
                                            {
                                                int seqno = Convert.ToInt32(dt1iv_2.Rows[0][0].ToString());
                                                int newNumber = seqno + 1;
                                                uniqueId = newNumber.ToString("KTH-MHN" + " - " + DateTime.Now.ToString("yy") + " - " + "0000");
                                                main_nopvtxt_1 = uniqueId;
                                            }
                                            else
                                            {
                                                int newNumber = Convert.ToInt32(uniqueId) + 1;
                                                uniqueId = newNumber.ToString("KTH-MHN" + " - " + DateTime.Now.ToString("yy") + " - " + "0000");
                                                main_nopvtxt_1 = uniqueId;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        main_nopvtxt_1 = main_nopvtxt;
                                    }

                                    DataTable dt = new DataTable();
                                    DataTable dtI = new DataTable();
                                    DataTable dtGK = new DataTable();
                                    DataTable dtGD = new DataTable();
                                    DataTable dt2 = new DataTable();
                                    DataTable dtak = new DataTable();
                                    DateTime tDate = DateTime.ParseExact(txttmb.Text, "dd/MM/yyyy", null);
                                    DateTime tDate1 = DateTime.ParseExact(txttinvbil.Text, "dd/MM/yyyy", null);
                                    DataTable dtkat1 = new DataTable();
                                    dtkat1 = Dbcon.Ora_Execute_table("select kat_akaun from KW_Ref_Carta_Akaun where kod_akaun='" + ddkodaka.SelectedItem.Value + "'");

                                    int count = 0;
                                    string rcount = string.Empty;
                                    foreach (GridViewRow g1 in Gridview5.Rows)
                                    {
                                        count++;
                                        rcount = count.ToString();
                                        string DD = (g1.FindControl("ddkodbil") as DropDownList).SelectedItem.Value;
                                        string DDN = (g1.FindControl("ddkodbil") as DropDownList).SelectedItem.Text;
                                        string dd_pro = (g1.FindControl("ddkodproj") as DropDownList).SelectedValue;
                                        string po = (g1.FindControl("TextBox50") as TextBox).Text;
                                        string item = (g1.FindControl("TextBox51") as TextBox).Text;
                                        string keter = (g1.FindControl("TextBox52") as TextBox).Text;
                                        string unit = (g1.FindControl("TextBox53") as TextBox).Text;
                                        string qty = (g1.FindControl("TextBox54") as TextBox).Text;
                                        string dis = (g1.FindControl("Txtdis") as TextBox).Text;
                                        string total = (g1.FindControl("Label1") as Label).Text;
                                        string ttotal = (g1.FindControl("Label5") as Label).Text;
                                        string gamt = (g1.FindControl("Label8") as Label).Text;
                                        string ogamt = (g1.FindControl("Label10") as Label).Text;

                                        string DDcukai = (g1.FindControl("ddcukaiinv") as DropDownList).SelectedItem.Text;

                                        if (rcount == "1")
                                        {
                                            dtGK = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + ddkodaka.SelectedItem.Value + "','','" + TextBox37.Text + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + dtkat1.Rows[0][0].ToString() + "','" + main_nopvtxt_1 + "','" + txrinvbil.Text + "','','" + TextBox37.Text + "','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "','" + keter + "','','" + ddkodaka.SelectedItem.Value + "','','','A','')");

                                            DataTable chk_carta1 = new DataTable();
                                            chk_carta1 = Dbcon.Ora_Execute_table("select kat_akaun,kod_akaun,KW_Debit_amt,KW_kredit_amt,Kw_open_amt from KW_Ref_Carta_Akaun where kod_akaun='" + ddkodaka.SelectedItem.Value + "'");
                                            if (chk_carta1.Rows.Count != 0)
                                            {
                                                double amt_ope2 = 0;
                                                double amt_deb2 = 0;
                                                double amt_deb3 = 0;

                                                DataTable chk_kategory1 = new DataTable();
                                                chk_kategory1 = Dbcon.Ora_Execute_table("select kat_cd,kat_akuan,bal_type from KW_Kategori_akaun where kat_cd='" + chk_carta1.Rows[0]["kat_akaun"].ToString() + "'");

                                                amt_deb2 = (double.Parse(chk_carta1.Rows[0]["KW_kredit_amt"].ToString()) + double.Parse(TextBox37.Text));

                                                if (chk_kategory1.Rows.Count != 0)
                                                {
                                                    amt_deb3 = -(double.Parse(TextBox37.Text));
                                                    if (chk_kategory1.Rows[0]["bal_type"].ToString() == "D")
                                                    {

                                                        amt_ope2 = (double.Parse(chk_carta1.Rows[0]["Kw_open_amt"].ToString()) + (amt_deb3));

                                                    }
                                                    else
                                                    {
                                                        amt_ope2 = (double.Parse(chk_carta1.Rows[0]["Kw_open_amt"].ToString()) + (amt_deb3));
                                                    }
                                                }
                                                DataTable upd_carta = new DataTable();
                                                upd_carta = Dbcon.Ora_Execute_table("UPDATE KW_Ref_Carta_Akaun set KW_kredit_amt='" + amt_deb2 + "',Kw_open_amt='" + amt_ope2 + "' where   kod_akaun='" + ddkodaka.SelectedItem.Value + "'");
                                            }

                                        }

                                        DataTable dtcuk = new DataTable();
                                        dtcuk = Dbcon.Ora_Execute_table("select Ref_kadar,Ref_kod_akaun,kat_akaun from KW_Ref_Tetapan_cukai where  Ref_nama_cukai='" + DDcukai + "'");
                                        string DDcukaival = (g1.FindControl("ddcukaiinv") as DropDownList).SelectedItem.Value;
                                        string cuk, taxkod, katkod;
                                        if (dtcuk.Rows.Count > 0)
                                        {
                                            cuk = dtcuk.Rows[0][0].ToString();
                                            taxkod = dtcuk.Rows[0][1].ToString();
                                            katkod = dtcuk.Rows[0][2].ToString();
                                        }
                                        else
                                        {
                                            cuk = "0";
                                            taxkod = "0";
                                            katkod = "0";
                                        }
                                        string DDcukai1 = (g1.FindControl("ddcukaioth") as DropDownList).SelectedItem.Text;
                                        DataTable dtcuk1 = new DataTable();
                                        dtcuk1 = Dbcon.Ora_Execute_table("select Ref_kadar,Ref_kod_akaun,kat_akaun from KW_Ref_Tetapan_cukai where  Ref_nama_cukai='" + DDcukai1 + "'");
                                        string DDcukaival1 = (g1.FindControl("ddcukaioth") as DropDownList).SelectedItem.Value;
                                        string cuk1, taxkod1, katkod1;
                                        if (dtcuk1.Rows.Count > 0)
                                        {
                                            cuk1 = dtcuk1.Rows[0][0].ToString();
                                            taxkod1 = dtcuk.Rows[0][1].ToString();
                                            katkod1 = dtcuk.Rows[0][2].ToString();
                                        }
                                        else
                                        {
                                            cuk1 = "0";
                                            taxkod1 = "0";
                                            katkod1 = "0";
                                        }
                                        string fre;
                                        if (dis == "")
                                        {
                                            fre = "0.00";
                                        }
                                        else
                                        {
                                            fre = dis;
                                        }



                                        string chk, val, val1;
                                        decimal tgst = 0;
                                        decimal ogst = 0;
                                        decimal tgst1 = 0;

                                        if ((g1.FindControl("CheckBox1") as CheckBox).Checked == true)
                                        {
                                            chk = "Y";
                                            if (DDcukaival != "--- PILIH ---")
                                            {
                                                val = DDcukaival;
                                                tgst = Convert.ToDecimal(gamt);

                                            }
                                            else
                                            {
                                                val = "0";
                                                tgst = Convert.ToDecimal("0.00");
                                            }
                                            if (DDcukaival1 != "--- PILIH ---")
                                            {
                                                val1 = DDcukaival1;

                                                tgst1 = Convert.ToDecimal(ogamt);
                                            }
                                            else
                                            {
                                                val1 = "0";
                                                tgst1 = Convert.ToDecimal("0.00");
                                            }
                                        }
                                        else
                                        {
                                            chk = "N";
                                            if (DDcukaival != "--- PILIH ---")
                                            {
                                                val = DDcukaival;
                                                tgst = Convert.ToDecimal(gamt);
                                            }
                                            else
                                            {
                                                val = "0";
                                                tgst = Convert.ToDecimal("0.00");
                                            }
                                            if (DDcukaival1 != "--- PILIH ---")
                                            {
                                                val1 = DDcukaival1;
                                                tgst1 = Convert.ToDecimal(ogamt);
                                            }
                                            else
                                            {
                                                val1 = "0";
                                                tgst1 = Convert.ToDecimal("0.00");
                                            }
                                        }
                                        dt = Dbcon.Ora_Execute_table("insert into KW_Pembayaran_invoisBil_item values('" + ddakaun.SelectedItem.Value + "','" + dddata.SelectedItem.Text + "','" + main_nopvtxt_1 + "','" + ddkataka.SelectedItem.Text + "','" + ddkodaka.SelectedItem.Value + "','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "','" + tDate1.ToString("yyyy/MM/dd hh:mm:ss") + "','" + txrinvbil.Text + "','" + txtinterma.Text + "','" + DD + "','" + item + "','" + keter + "','" + dd_pro + "','" + unit + "','" + qty + "','" + fre + "','" + total + "','" + chk + "','" + cuk + "','" + cuk1 + "','" + val + "','" + val1 + "','" + tgst + "','" + tgst1 + "','" + ttotal + "','B','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','','','" + po + "')");
                                        DataTable dtkat2 = new DataTable();
                                        dtkat2 = Dbcon.Ora_Execute_table("select kat_akaun from KW_Ref_Carta_Akaun where kod_akaun='" + DD + "'");
                                        dtGK = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + DD + "','" + total + "','','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + dtkat2.Rows[0][0].ToString() + "','" + main_nopvtxt_1 + "','" + txrinvbil.Text + "','','" + total + "','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "' ,'" + keter + "','','" + DD + "','','','A','')");

                                        DataTable chk_carta = new DataTable();
                                        chk_carta = Dbcon.Ora_Execute_table("select kat_akaun,kod_akaun,KW_Debit_amt,KW_kredit_amt,Kw_open_amt from KW_Ref_Carta_Akaun where kod_akaun='" + DD + "'");
                                        if (chk_carta.Rows.Count != 0)
                                        {
                                            double amt_ope1 = 0;
                                            double amt_deb1 = 0;
                                            double amt_deb2_1 = 0;

                                            DataTable chk_kategory = new DataTable();
                                            chk_kategory = Dbcon.Ora_Execute_table("select kat_cd,kat_akuan,bal_type from KW_Kategori_akaun where kat_cd='" + chk_carta.Rows[0]["kat_akaun"].ToString() + "'");

                                            amt_deb1 = (double.Parse(chk_carta.Rows[0]["KW_Debit_amt"].ToString()) + double.Parse(total));

                                            if (chk_kategory.Rows.Count != 0)
                                            {
                                                if (chk_kategory.Rows[0]["bal_type"].ToString() == "D")
                                                {
                                                    amt_ope1 = (double.Parse(chk_carta.Rows[0]["Kw_open_amt"].ToString()) + double.Parse(total));
                                                }
                                                else
                                                {
                                                    amt_deb2_1 = (double.Parse(total));
                                                    amt_ope1 = (double.Parse(chk_carta.Rows[0]["Kw_open_amt"].ToString()) + (double.Parse(total)));
                                                }
                                            }
                                            DataTable upd_carta = new DataTable();
                                            upd_carta = Dbcon.Ora_Execute_table("UPDATE KW_Ref_Carta_Akaun set KW_Debit_amt='" + amt_deb1 + "',Kw_open_amt='" + amt_ope1 + "' where   kod_akaun='" + DD + "'");
                                        }

                                        // dt2 = Dbcon.Ora_Execute_table("update KW_Opening_Balance set ending_amt=ending_amt + " + ttotal + " ,upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where kod_akaun='" + DD + "' ");
                                        if (tgst != 0)
                                        {
                                            dtGK = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + taxkod + "','" + tgst + "','','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + katkod + "','" + main_nopvtxt_1 + "','" + txrinvbil.Text + "','" + val + "','" + ttotal + "','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "' ,'" + keter + "','','" + taxkod + "','','','A','')");
                                        }

                                        if (tgst1 != 0)
                                        {
                                            dtGK = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + taxkod1 + "','" + tgst1 + "','','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + katkod1 + "','" + main_nopvtxt_1 + "','" + txrinvbil.Text + "','" + val1 + "','" + ttotal + "','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "' ,'" + keter + "','','" + taxkod1 + "','','','A','')");
                                        }

                                    }

                                    Div12.Visible = true;
                                    Div13.Visible = false;
                                    reset();
                                    tab5();
                                    Bindbaucer();
                                    BindInvois();
                                    BindMohon();
                                    Bindcredit();
                                    Binddedit();
                                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Invois / Bil Dibuat Berjaya.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih No Invois.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Tarikh Invois.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Tarikh Mohon.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Kod Akaun.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Kategori Akaun.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Kod Akaun.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                //}
            }
            
        }
        else
        {
            tab5();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Untuk Akaun.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        if (ddakaun.SelectedItem.Text != "--- PILIH ---")
        {
            if (ddpvkat.SelectedItem.Text != "--- PILIH ---")
            {
                if (ddpvkat.SelectedItem.Text != "SEMUA COA")
                {
                    if (ddnoperpv.SelectedItem.Text != "--- PILIH ---")
                    {
                        if (txttarpv.Text != "")
                        {

                            if (ddpvkod.SelectedItem.Text != "--- PILIH ---")
                            {

                                if (ddpvcara.SelectedValue != "")
                                {
                                    if (DDbank.SelectedValue != "" || ddpvcara.SelectedValue == "001")
                                    {

                                        if (ddpvstatus.SelectedItem.Text != "--- PILIH ---")
                                        {
                                            DataTable dt = new DataTable();
                                            DataTable dtI = new DataTable();
                                            DataTable dtGK = new DataTable();
                                            DataTable dtGD = new DataTable();
                                            DataTable dt2 = new DataTable();
                                            DataTable dtak = new DataTable();


                                            DateTime tDate = DateTime.ParseExact(txttarpv.Text, "dd/MM/yyyy", null);

                                            string rcount = string.Empty;
                                            int count = 0;
                                            foreach (GridViewRow gvrow in grdpvinv.Rows)
                                            {
                                                var rb = gvrow.FindControl("CheckBox1") as System.Web.UI.WebControls.CheckBox;
                                                if (rb.Checked)
                                                {
                                                    count++;
                                                }
                                                rcount = count.ToString();
                                            }
                                            DataTable gen_invkd = new DataTable();
                                            gen_invkd = Dbcon.Ora_Execute_table("select Pv_no from KW_Pembayaran_Pay_voucer where Pv_no='" + txtpvno.Text + "'");
                                            if (gen_invkd.Rows.Count > 0)
                                            {
                                                DataTable dtpv_1 = Dbcon.Ora_Execute_table("select doc_type_cd,format as fmt,SUBSTRING(cur_format, 1, 10) as lfrmt1,SUBSTRING(cur_format, 11, 4) as lfrmt2,cur_format as cfmt from KW_Format_Nombor_rujukan where doc_type_cd='04' and Status='A'");
                                                if (dtpv_1.Rows.Count != 0)
                                                {
                                                    if (dtpv_1.Rows[0]["cfmt"].ToString() == "")
                                                    {
                                                        txtpvno_1.Text = dtpv_1.Rows[0]["fmt"].ToString();
                                                    }
                                                    else
                                                    {
                                                        int seqno = Convert.ToInt32(dtpv_1.Rows[0]["lfrmt2"].ToString());
                                                        int newNumber = seqno + 1;
                                                        uniqueId = newNumber.ToString(dtpv_1.Rows[0]["lfrmt1"].ToString() + "0000");
                                                        txtpvno_1.Text = uniqueId;
                                                    }

                                                }
                                                else
                                                {
                                                    DataTable dtpv_2 = Dbcon.Ora_Execute_table("select   ISNULL(max(SUBSTRING(Pv_no,13,2000)),'0')  from KW_Pembayaran_Pay_voucer");
                                                    if (dtpv_2.Rows.Count > 0)
                                                    {
                                                        int seqno = Convert.ToInt32(dtpv_2.Rows[0][0].ToString());
                                                        int newNumber = seqno + 1;
                                                        uniqueId = newNumber.ToString("KTH-PVC" + "-" + DateTime.Now.ToString("yy") + "-" + "0000");
                                                        txtpvno_1.Text = uniqueId;
                                                    }
                                                    else
                                                    {
                                                        int newNumber = Convert.ToInt32(uniqueId) + 1;
                                                        uniqueId = newNumber.ToString("KTH-PVC" + "-" + DateTime.Now.ToString("yy") + "-" + "0000");
                                                        txtpvno_1.Text = uniqueId;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                txtpvno_1.Text = txtpvno.Text;
                                            }

                                            string vv1 = string.Empty, vv2 = string.Empty, vv3 = string.Empty;
                                            string vv5;
                                            if (rcount != "0")
                                            {
                                                foreach (GridViewRow g1 in grdpvinv.Rows)
                                                {
                                                    string tarmoh = (g1.FindControl("Label08") as Label).Text;
                                                    string pv_no = (g1.FindControl("jurnal_po") as Label).Text;
                                                    string tarper = (g1.FindControl("Label19") as Label).Text;
                                                    string tarinv = (g1.FindControl("Label09") as Label).Text;
                                                    string noinv = (g1.FindControl("Label10") as Label).Text;
                                                    string jum = (g1.FindControl("Label11") as Label).Text;
                                                    string gst = (g1.FindControl("Label12") as Label).Text;
                                                    string othgst = (g1.FindControl("Label13") as Label).Text;
                                                    string total = (g1.FindControl("Label14") as Label).Text;
                                                    string dis = (g1.FindControl("txtpay") as TextBox).Text;
                                                    if (dis != "")
                                                    {
                                                        userid = Session["New"].ToString();
                                                        DateTime tDate1 = DateTime.ParseExact(tarinv.ToString(), "dd/MM/yyyy", null);
                                                        string chk, val, val1;
                                                        decimal tgst = 0;
                                                        decimal ogst = 0;
                                                        decimal tgst1 = 0;
                                                        if ((g1.FindControl("CheckBox1") as CheckBox).Checked == true)
                                                        {

                                                            chk = "Y";

                                                            dt2 = Dbcon.Ora_Execute_table("select Ref_No_akaun from KW_Ref_Akaun_bank where Ref_kod_akaun = '" + DDbank.SelectedItem.Value + "' and Ref_Nama_akaun = '" + DDbank.SelectedItem.Text + "'");

                                                            if (dt2.Rows.Count != 0)
                                                            {
                                                                vv1 = dt2.Rows[0][0].ToString();
                                                            }

                                                            dt = Dbcon.Ora_Execute_table("insert into KW_Pembayaran_Pay_voucer values('" + ddakaun.SelectedItem.Value + "','" + txtdata.Text + "','" + txtpvno_1.Text + "','" + tarper + "','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "','" + ddpvkat.SelectedItem.Text + "','" + ddpvkod.SelectedItem.Value + "','','" + ddpvcara.SelectedItem.Value + "','" + vv1 + "','" + DDbank.SelectedItem.Value + "','" + txtpvterma.Text + "','" + txtpvnocek.Text + "','" + ddnoperpv_PV1.Text + "','" + tDate1.ToString("yyyy/MM/dd hh:mm:ss") + "','" + jum + "','" + gst + "','" + othgst + "','" + total + "','" + dis + "','" + ddpvstatus.SelectedItem.Value + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','','','" + TXTPVNAME.Text + "','" + pv_no + "')");

                                                            DataTable dt_upd_format = new DataTable();
                                                            dt_upd_format = Dbcon.Ora_Execute_table("update KW_Format_Nombor_rujukan set cur_format='" + txtpvno_1.Text + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where doc_type_cd='04' and Status = 'A'");
                                                            // update Aset integration
                                                            string vv1_s1 = string.Empty, vv2_s2 = string.Empty;
                                                            if (pv_no.ToString() != "")
                                                            {
                                                                string[] sss_1 = pv_no.Split('_');

                                                                if (sss_1.Length > 1)
                                                                {
                                                                    vv1_s1 = sss_1[0].ToString();
                                                                    vv2_s2 = sss_1[1].ToString();
                                                                }
                                                                else
                                                                {
                                                                    vv1_s1 = sss_1[0].ToString();
                                                                }

                                                                DataTable chk_pur_asset = new DataTable();
                                                                chk_pur_asset = DBCon.Ora_Execute_table("select * from ast_purchase where pur_approve_sts_cd='01' and pur_mp_sts='Proses' and pur_po_no='" + vv1_s1 + "' and pur_supplier_id='" + vv2_s2 + "' and ISNULL(pur_del_ind,'') != 'D'");
                                                                if (chk_pur_asset.Rows.Count != 0)
                                                                {
                                                                    DataTable upd_purchase = new DataTable();
                                                                    upd_purchase = Dbcon.Ora_Execute_table("update ast_purchase set pur_inv_no='" + ddnoperpv.SelectedItem.Text + "',pur_tarikh_bayaran='" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "',pur_pv_no='" + txtpvno_1.Text + "',pur_cek_no='" + txtpvnocek.Text + "',pur_upd_id='" + userid + "',pur_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where pur_approve_sts_cd='01' and pur_mp_sts='Proses' and pur_po_no='" + vv1_s1 + "' and pur_supplier_id='" + vv2_s2 + "' and ISNULL(pur_del_ind,'') != 'D'");
                                                                }
                                                            }

                                                        }
                                                    }
                                                }
                                                Div4.Visible = true;
                                                Div5.Visible = false;
                                                reset();
                                                tab2();
                                                Bindbaucer();
                                                BindInvois();
                                                BindMohon();
                                                Bindcredit();
                                                Binddedit();
                                                //noper_pv();
                                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Vocher pembayaran Berjaya Dibuat.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                                            }
                                            else
                                            {
                                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                                            }

                                        }
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Status.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                                        }
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Dibayar Oleh.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Cara Bayaran.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                                }

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Kod Akaun.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                            }

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Tarikh.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih No Permohonan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }
                }
                else
                {

                    if (txttarpv.Text != "")
                    {

                        if (ddpvkod.SelectedItem.Text != "--- PILIH ---")
                        {

                            if (ddpvcara.SelectedValue != "")
                            {
                                if (DDbank.SelectedValue != "" || ddpvcara.SelectedValue == "001")
                                {

                                    if (ddpvstatus.SelectedItem.Text != "--- PILIH ---")
                                    {
                                        DataTable dt = new DataTable();
                                        DataTable dtI = new DataTable();
                                        DataTable dtGK = new DataTable();
                                        DataTable dtGD = new DataTable();
                                        DataTable dt2 = new DataTable();
                                        DataTable dtak = new DataTable();


                                        DateTime tDate = DateTime.ParseExact(txttarpv.Text, "dd/MM/yyyy", null);

                                        //string rcount = string.Empty;
                                        //int count = 0;
                                        //foreach (GridViewRow gvrow in grdpvinv_new.Rows)
                                        //{
                                        //    var rb = gvrow.FindControl("CheckBox1") as System.Web.UI.WebControls.CheckBox;
                                        //    if (rb.Checked)
                                        //    {
                                        //        count++;
                                        //    }
                                        //    rcount = count.ToString();
                                        //}
                                        DataTable gen_invkd = new DataTable();
                                        gen_invkd = Dbcon.Ora_Execute_table("select Pv_no from KW_Pembayaran_Pay_voucer where Pv_no='" + txtpvno.Text + "'");
                                        if (gen_invkd.Rows.Count > 0)
                                        {
                                            DataTable dtpv_1 = Dbcon.Ora_Execute_table("select doc_type_cd,format as fmt,SUBSTRING(cur_format, 1, 10) as lfrmt1,SUBSTRING(cur_format, 11, 4) as lfrmt2,cur_format as cfmt from KW_Format_Nombor_rujukan where doc_type_cd='04' and Status='A'");
                                            if (dtpv_1.Rows.Count != 0)
                                            {
                                                if (dtpv_1.Rows[0]["cfmt"].ToString() == "")
                                                {
                                                    txtpvno_1.Text = dtpv_1.Rows[0]["fmt"].ToString();
                                                }
                                                else
                                                {
                                                    int seqno = Convert.ToInt32(dtpv_1.Rows[0]["lfrmt2"].ToString());
                                                    int newNumber = seqno + 1;
                                                    uniqueId = newNumber.ToString(dtpv_1.Rows[0]["lfrmt1"].ToString() + "0000");
                                                    txtpvno_1.Text = uniqueId;
                                                }

                                            }
                                            else
                                            {
                                                DataTable dtpv_2 = Dbcon.Ora_Execute_table("select   ISNULL(max(SUBSTRING(Pv_no,13,2000)),'0')  from KW_Pembayaran_Pay_voucer");
                                                if (dtpv_2.Rows.Count > 0)
                                                {
                                                    int seqno = Convert.ToInt32(dtpv_2.Rows[0][0].ToString());
                                                    int newNumber = seqno + 1;
                                                    uniqueId = newNumber.ToString("KTH-PVC" + "-" + DateTime.Now.ToString("yy") + "-" + "0000");
                                                    txtpvno_1.Text = uniqueId;
                                                }
                                                else
                                                {
                                                    int newNumber = Convert.ToInt32(uniqueId) + 1;
                                                    uniqueId = newNumber.ToString("KTH-PVC" + "-" + DateTime.Now.ToString("yy") + "-" + "0000");
                                                    txtpvno_1.Text = uniqueId;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            txtpvno_1.Text = txtpvno.Text;
                                        }

                                        string vv1 = string.Empty, vv2 = string.Empty, vv3 = string.Empty;
                                        string vv5;
                                        //if (rcount != "0")
                                        //{
                                        foreach (GridViewRow g1 in grdpvinv_new.Rows)
                                        {
                                            string tarmoh = (g1.FindControl("gn_Label08") as TextBox).Text;
                                            //string pv_no = (g1.FindControl("gn_jurnal_po") as TextBox).Text;
                                            string tarper = (g1.FindControl("gn_Label19") as TextBox).Text;
                                            string tarinv = (g1.FindControl("gn_Label09") as TextBox).Text;
                                            string noinv = (g1.FindControl("gn_Label10") as TextBox).Text;
                                            string jum = (g1.FindControl("gn_Label11") as TextBox).Text;
                                            string gst = (g1.FindControl("gn_Label12") as TextBox).Text;
                                            string othgst = (g1.FindControl("gn_Label13") as TextBox).Text;
                                            string total = (g1.FindControl("gn_Label14") as TextBox).Text;
                                            string dis = (g1.FindControl("gn_txtpay") as TextBox).Text;
                                            if (dis != "")
                                            {
                                                userid = Session["New"].ToString();
                                                DateTime tDate1 = DateTime.ParseExact(tarinv.ToString(), "dd/MM/yyyy", null);
                                                string chk, val, val1;
                                                decimal tgst = 0;
                                                decimal ogst = 0;
                                                decimal tgst1 = 0;
                                                //if ((g1.FindControl("CheckBox1") as CheckBox).Checked == true)
                                                //{

                                                chk = "Y";

                                                dt2 = Dbcon.Ora_Execute_table("select Ref_No_akaun from KW_Ref_Akaun_bank where Ref_kod_akaun = '" + DDbank.SelectedItem.Value + "' and Ref_Nama_akaun = '" + DDbank.SelectedItem.Text + "'");

                                                if (dt2.Rows.Count != 0)
                                                {
                                                    vv1 = dt2.Rows[0][0].ToString();
                                                }

                                                dt = Dbcon.Ora_Execute_table("insert into KW_Pembayaran_Pay_voucer values('" + ddakaun.SelectedItem.Value + "','" + txtdata.Text + "','" + txtpvno_1.Text + "','" + tarper + "','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "','" + ddpvkat.SelectedItem.Text + "','" + ddpvkod.SelectedItem.Value + "','','" + ddpvcara.SelectedItem.Value + "','" + vv1 + "','" + DDbank.SelectedItem.Value + "','" + txtpvterma.Text + "','" + txtpvnocek.Text + "','" + noinv + "','" + tDate1.ToString("yyyy/MM/dd hh:mm:ss") + "','" + jum + "','" + gst + "','" + othgst + "','" + total + "','" + dis + "','" + ddpvstatus.SelectedItem.Value + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','','','" + TXTPVNAME.Text + "','')");

                                                DataTable dt_upd_format = new DataTable();
                                                dt_upd_format = Dbcon.Ora_Execute_table("update KW_Format_Nombor_rujukan set cur_format='" + txtpvno_1.Text + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where doc_type_cd='04' and Status = 'A'");

                                                //}
                                            }
                                        }
                                        Div4.Visible = true;
                                        Div5.Visible = false;
                                        reset();
                                        tab2();
                                        Bindbaucer();
                                        BindInvois();
                                        BindMohon();
                                        Bindcredit();
                                        Binddedit();
                                        //noper_pv();
                                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Vocher pembayaran Berjaya Dibuat.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                                        //}
                                        //else
                                        //{
                                        //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                                        //}

                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Status.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Dibayar Oleh.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Cara Bayaran.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                            }

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Kod Akaun.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Tarikh.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Kategori Akaun.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Untuk Akuan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
        tab2();
    }

    protected void Button17_Click(object sender, EventArgs e)
    {
        if (ddakaun.SelectedItem.Text != "--- PILIH ---")
        {
            //if (ddnoperpv.SelectedItem.Text != "--- PILIH ---")
            //{
            if (txttarpv.Text != "")
            {
                if (ddpvkat.SelectedItem.Text != "--- PILIH ---")
                {
                    if (ddpvkod.SelectedItem.Text != "--- PILIH ---")
                    {

                        if (ddpvcara.SelectedValue != "")
                        {
                            if (DDbank.SelectedValue != "" || ddpvcara.SelectedValue == "001")
                            {
                                DataTable dt = new DataTable();
                                DataTable dtI = new DataTable();
                                DataTable dtGK = new DataTable();
                                DataTable dtGD = new DataTable();
                                DataTable dt2 = new DataTable();
                                DataTable dtak = new DataTable();

                                string chk, val, val1;
                                decimal tgst = 0;
                                decimal ogst = 0;
                                decimal tgst1 = 0;
                                string vv1 = string.Empty, vv2 = string.Empty, vv3 = string.Empty;
                                string vv5;
                                userid = Session["New"].ToString();
                                level = Session["level"].ToString();
                                if (ddpvstatus1.SelectedItem.Text != "LULUS")
                                {

                                    if (ddpvstatus.SelectedItem.Text == "BARU")
                                    {
                                        dt = Dbcon.Ora_Execute_table("update KW_Pembayaran_Pay_voucer set Terma='" + txtpvterma.Text + "',No_cek='" + txtpvnocek.Text + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where  Pv_no='" + txtpvno.Text + "'");
                                        Div4.Visible = true;
                                        Div5.Visible = false;
                                        reset();
                                        tab2();
                                        Bindbaucer();
                                        BindInvois();
                                        BindMohon();
                                        Bindcredit();
                                        Binddedit();
                                        //noper_pv();
                                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Vocher pembayaran Berjaya Dibuat.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                                    }
                                    else
                                    {


                                        DateTime tDate = DateTime.ParseExact(txttarpv.Text, "dd/MM/yyyy", null);

                                        string rcount = string.Empty;
                                        int count = 0;
                                        foreach (GridViewRow gvrow in grdpvinv.Rows)
                                        {
                                            var rb = gvrow.FindControl("CheckBox1") as System.Web.UI.WebControls.CheckBox;
                                            if (rb.Checked)
                                            {
                                                count++;
                                            }
                                            rcount = count.ToString();
                                        }

                                        if (rcount != "0" || rcount != "")
                                        {
                                            foreach (GridViewRow g1 in grdpvinv.Rows)
                                            {
                                                string tarmoh = (g1.FindControl("Label08") as Label).Text;
                                                string tarper = (g1.FindControl("Label19") as Label).Text;
                                                string tarinv = (g1.FindControl("Label09") as Label).Text;
                                                string noinv = (g1.FindControl("Label10") as Label).Text;
                                                string jum = (g1.FindControl("Label11") as Label).Text;
                                                string gst = (g1.FindControl("Label12") as Label).Text;
                                                string othgst = (g1.FindControl("Label13") as Label).Text;
                                                string total = (g1.FindControl("Label14") as Label).Text;
                                                string dis = (g1.FindControl("txtpay") as TextBox).Text;
                                                if (dis != "")
                                                {

                                                    DateTime tDate1 = DateTime.ParseExact(tarinv.ToString(), "dd/MM/yyyy", null);

                                                    if ((g1.FindControl("CheckBox1") as CheckBox).Checked == true)
                                                    {

                                                        chk = "Y";

                                                        dt2 = Dbcon.Ora_Execute_table("select Ref_No_akaun from KW_Ref_Akaun_bank where Ref_kod_akaun = '" + DDbank.SelectedItem.Value + "' and Ref_Nama_akaun = '" + DDbank.SelectedItem.Text + "'");

                                                        if (dt2.Rows.Count != 0)
                                                        {
                                                            vv1 = dt2.Rows[0][0].ToString();
                                                        }

                                                        dt = Dbcon.Ora_Execute_table("update KW_Pembayaran_Pay_voucer set untuk_akaun='" + ddakaun.SelectedItem.Value + "',Data='" + txtdata.Text + "',Pv_no='" + txtpvno.Text + "',no_Permohonan='" + tarper + "',tarkih_pv='" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "',kat_akaun='" + ddpvkat.SelectedItem.Text + "',Deb_kod_akaun='" + ddpvkod.SelectedItem.Value + "',Cara_Bayaran='" + ddpvcara.SelectedItem.Value + "',Cre_Bank_no='" + vv1 + "',Cre_kod_akaun='" + DDbank.SelectedItem.Value + "',Terma='',No_cek='" + txtpvnocek.Text + "',no_invois='" + tarinv + "',tarikh_invois='" + tDate1.ToString("yyyy/MM/dd hh:mm:ss") + "',jumlah='" + jum + "',gstjumlah='" + gst + "',othgstjumlah='" + othgst + "',Overall='" + total + "',payamt='" + dis + "',Status='" + ddpvstatus.SelectedItem.Value + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "',Akaun_name='" + TXTPVNAME.Text + "' where  Pv_no='" + txtpvno.Text + "'");


                                                    }
                                                }
                                            }
                                            Div4.Visible = true;
                                            Div5.Visible = false;
                                            reset();
                                            tab2();
                                            Bindbaucer();
                                            BindInvois();
                                            BindMohon();
                                            Bindcredit();
                                            Binddedit();
                                            //noper_pv();
                                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Vocher pembayaran Berjaya Dibuat.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                                        }
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                                        }
                                    }
                                }
                                //else if (level == "5")
                                //{
                                if (ddpvstatus1.SelectedItem.Text == "LULUS")
                                {
                                    foreach (GridViewRow g1 in Grdpvinvdup.Rows)
                                    {
                                        string noinv = (g1.FindControl("no_invois") as Label).Text;
                                        DataTable Dtpv = new DataTable();
                                        Dtpv = Dbcon.Ora_Execute_table("select Deb_kod_akaun, Cre_kod_akaun, payamt,No_cek, Format(tarkih_pv,'dd/MM/yyyy') tarkih_pv from KW_Pembayaran_Pay_voucer where no_invois = '" + noinv + "'");

                                        for (int i = 0; i < Dtpv.Rows.Count; i++)
                                        {
                                            DataTable dtkat1 = new DataTable();
                                            dtkat1 = Dbcon.Ora_Execute_table("select kat_akaun from KW_Ref_Carta_Akaun where kod_akaun='" + Dtpv.Rows[i][0].ToString() + "'");
                                            DataTable dtkat2 = new DataTable();
                                            dtkat2 = Dbcon.Ora_Execute_table("select kat_akaun from KW_Ref_Carta_Akaun where kod_akaun='" + Dtpv.Rows[i][1].ToString() + "'");
                                            if (dtkat1.Rows.Count != 0)
                                            {
                                                vv2 = dtkat1.Rows[0][0].ToString();
                                            }
                                            if (dtkat2.Rows.Count != 0)
                                            {
                                                vv3 = dtkat2.Rows[0][0].ToString();
                                            }

                                            string pv_ket = string.Empty;
                                            DataTable get_ket = new DataTable();
                                            get_ket = Dbcon.Ora_Execute_table("select * from KW_Pembayaran_invoisBil_item where no_invois='" + noinv + "'");
                                            if (get_ket.Rows.Count != 0)
                                            {
                                                pv_ket = get_ket.Rows[0]["keterangan"].ToString();
                                            }

                                            DateTime tDate3 = DateTime.ParseExact(Dtpv.Rows[i][4].ToString(), "dd/MM/yyyy", null);
                                            dtGK = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + Dtpv.Rows[i][0].ToString() + "','" + Dtpv.Rows[i][2].ToString() + "','','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + vv2 + "','" + txtpvno.Text + "','" + noinv + "','','" + Dtpv.Rows[i][2].ToString() + "','" + tDate3.ToString("yyyy/MM/dd hh:mm:ss") + "','" + pv_ket + "','" + Dtpv.Rows[i][3].ToString() + "','" + Dtpv.Rows[i][0].ToString() + "','','','A','')");

                                            DataTable chk_carta = new DataTable();
                                            chk_carta = Dbcon.Ora_Execute_table("select kat_akaun,kod_akaun,KW_Debit_amt,KW_kredit_amt,Kw_open_amt from KW_Ref_Carta_Akaun where kod_akaun='" + Dtpv.Rows[i][0].ToString() + "'");
                                            if (chk_carta.Rows.Count != 0)
                                            {
                                                double amt_ope1 = 0;
                                                double amt_deb1 = 0;
                                                double amt_deb2_1 = 0;

                                                DataTable chk_kategory = new DataTable();
                                                chk_kategory = Dbcon.Ora_Execute_table("select kat_cd,kat_akuan,bal_type from KW_Kategori_akaun where kat_cd='" + chk_carta.Rows[0]["kat_akaun"].ToString() + "'");

                                                amt_deb1 = (double.Parse(chk_carta.Rows[0]["KW_Debit_amt"].ToString()) + double.Parse(Dtpv.Rows[i][2].ToString()));

                                                if (chk_kategory.Rows.Count != 0)
                                                {
                                                    if (chk_kategory.Rows[0]["bal_type"].ToString() == "D")
                                                    {
                                                        amt_ope1 = (double.Parse(chk_carta.Rows[0]["Kw_open_amt"].ToString()) + double.Parse(Dtpv.Rows[i][2].ToString()));
                                                    }
                                                    else
                                                    {
                                                        amt_deb2_1 = (double.Parse(Dtpv.Rows[i][2].ToString()));
                                                        amt_ope1 = (double.Parse(chk_carta.Rows[0]["Kw_open_amt"].ToString()) + (amt_deb2_1));
                                                    }
                                                }
                                                DataTable upd_carta = new DataTable();
                                                upd_carta = Dbcon.Ora_Execute_table("UPDATE KW_Ref_Carta_Akaun set KW_Debit_amt='" + amt_deb1 + "',Kw_open_amt='" + amt_ope1 + "' where   kod_akaun='" + Dtpv.Rows[i][0].ToString() + "'");
                                            }

                                            dtGK = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + Dtpv.Rows[i][1].ToString() + "','','" + Dtpv.Rows[i][2].ToString() + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + vv3 + "','" + txtpvno.Text + "','" + noinv + "','','" + Dtpv.Rows[i][2].ToString() + "','" + tDate3.ToString("yyyy/MM/dd hh:mm:ss") + "','" + pv_ket + "','" + Dtpv.Rows[i][3].ToString() + "','" + Dtpv.Rows[i][1].ToString() + "','','','A','')");

                                            //update carta akaun

                                            DataTable chk_carta1 = new DataTable();
                                            chk_carta1 = Dbcon.Ora_Execute_table("select kat_akaun,kod_akaun,KW_Debit_amt,KW_kredit_amt,Kw_open_amt from KW_Ref_Carta_Akaun where kod_akaun='" + Dtpv.Rows[i][1].ToString() + "'");
                                            if (chk_carta1.Rows.Count != 0)
                                            {
                                                double amt_ope2 = 0;
                                                double amt_deb2 = 0;
                                                double amt_deb3 = 0;

                                                DataTable chk_kategory1 = new DataTable();
                                                chk_kategory1 = Dbcon.Ora_Execute_table("select kat_cd,kat_akuan,bal_type from KW_Kategori_akaun where kat_cd='" + chk_carta1.Rows[0]["kat_akaun"].ToString() + "'");

                                                amt_deb2 = (double.Parse(chk_carta1.Rows[0]["KW_kredit_amt"].ToString()) + double.Parse(Dtpv.Rows[i][2].ToString()));

                                                if (chk_kategory1.Rows.Count != 0)
                                                {
                                                    amt_deb3 = -(double.Parse(Dtpv.Rows[i][2].ToString()));
                                                    if (chk_kategory1.Rows[0]["bal_type"].ToString() == "D")
                                                    {

                                                        amt_ope2 = (double.Parse(chk_carta1.Rows[0]["Kw_open_amt"].ToString()) + (amt_deb3));

                                                    }
                                                    else
                                                    {
                                                        amt_ope2 = (double.Parse(chk_carta1.Rows[0]["Kw_open_amt"].ToString()) + (amt_deb3));
                                                    }
                                                }
                                                DataTable upd_carta = new DataTable();
                                                upd_carta = Dbcon.Ora_Execute_table("UPDATE KW_Ref_Carta_Akaun set KW_kredit_amt='" + amt_deb2 + "',Kw_open_amt='" + amt_ope2 + "' where   kod_akaun='" + Dtpv.Rows[i][1].ToString() + "'");
                                            }
                                            // end

                                        }
                                        dtGK = Dbcon.Ora_Execute_table("update KW_Pembayaran_invoisBil_item set Status='L' where no_invois='" + noinv + "'");
                                        dtGK = Dbcon.Ora_Execute_table("update KW_Pembayaran_Pay_voucer set Status='L' where no_invois='" + noinv + "'");
                                        //
                                        DataTable get_ket1 = new DataTable();
                                        get_ket1 = Dbcon.Ora_Execute_table("select * from KW_Pembayaran_invoisBil_item where no_invois='" + noinv + "'");
                                        if (get_ket1.Rows.Count != 0)
                                        {
                                            if (get_ket1.Rows[0]["no_po"].ToString() != "")
                                            {

                                                string vv1_s1 = string.Empty, vv2_s2 = string.Empty;
                                                if (get_ket1.Rows[0]["no_po"].ToString() != "")
                                                {
                                                    string[] jurn_po = get_ket1.Rows[0]["no_po"].ToString().Split('_');
                                                    if (jurn_po.Length > 1)
                                                    {
                                                        vv1_s1 = jurn_po[0].ToString();
                                                        vv2_s2 = jurn_po[1].ToString();
                                                    }
                                                    else
                                                    {
                                                        vv1_s1 = jurn_po[0].ToString();
                                                    }
                                                    DataTable chk_pur_asset = new DataTable();
                                                    chk_pur_asset = DBCon.Ora_Execute_table("select * from ast_purchase where pur_approve_sts_cd='01' and pur_mp_sts='Proses' and pur_po_no='" + vv1_s1 + "' and pur_supplier_id='" + vv2_s2 + "' and ISNULL(pur_del_ind,'') != 'D'");
                                                    if (chk_pur_asset.Rows.Count != 0)
                                                    {
                                                        DataTable upd_purchase = new DataTable();
                                                        upd_purchase = Dbcon.Ora_Execute_table("update ast_purchase set pur_mp_sts='Telah Dibayar',pur_upd_id='" + userid + "',pur_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where pur_approve_sts_cd='01' and pur_mp_sts='Proses' and pur_po_no='" + vv1_s1 + "' and pur_supplier_id='" + vv2_s2 + "' and ISNULL(pur_del_ind,'') != 'D'");
                                                    }
                                                }
                                            }
                                        }

                                    }
                                    Div4.Visible = true;
                                    Div5.Visible = false;
                                    reset();
                                    tab2();
                                    Bindbaucer();
                                    BindInvois();
                                    BindMohon();
                                    Bindcredit();
                                    Binddedit();
                                    //noper_pv();
                                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Vocher pembayaran Berjaya Dibuat.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                                    //}
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Dibayar Oleh.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Cara Bayaran.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Kod Akaun.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog(''Sila Pilih Kategori Akaun.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Tarikh.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih No Permohonan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            //}
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Untuk Akuan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
        tab2();
    }

    protected void buttab2_Click(object sender, EventArgs e)
    {
        
        Bindbaucer();
        BindInvois();
        BindMohon();
        Bindcredit();
        Binddedit();
        tab2();
    }

    protected void buttab3_Click(object sender, EventArgs e)
    {

        Bindbaucer();
        BindInvois();
        BindMohon();
        Bindcredit();
        Binddedit();
        tab5();
    }
    protected void Button9_Click(object sender, EventArgs e)
    {
        Div4.Visible = true;
        Div5.Visible = false;
        noper_pv();
       
        reset();
        tab2();
        Bindbaucer();
        BindInvois();
        BindMohon();
        Bindcredit();
        Binddedit();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Div2.Visible = false;
        Div3.Visible = true;
        gridmohdup.Visible = false;
        reset();
        tab1();
        Bindbaucer();
        BindInvois();
        BindMohon();
        Bindcredit();
        Binddedit();

    }

    protected void Button11_Click(object sender, EventArgs e)
    {
        Div12.Visible = true;
        Div13.Visible = false;
        Button8.Visible = true;
      
        reset();
        tab5();
        Bindbaucer();
        BindInvois();
        BindMohon();
        Bindcredit();
        Binddedit();

    }
    protected void tab3tam_Click(object sender, EventArgs e)
    {
        th2.Visible = true;
        th1.Visible = false;
        Button15.Visible = true;
        Button22.Visible = false;
        Gridview9.Visible = false;
        Gridview10.Visible = true;
        Gridview12.Visible = false;
        Button15.Visible = true;
        Gridview9.Enabled = true;
        GetUniqueId();
        reset();
        tab3();

        Binddedit();
    }

    void tab3()
    {
        p6.Attributes.Add("class", "tab-pane");
        p1.Attributes.Add("class", "tab-pane");
        p2.Attributes.Add("class", "tab-pane active");
        p3.Attributes.Add("class", "tab-pane");
        p4.Attributes.Add("class", "tab-pane");

        pp6.Attributes.Remove("class");
        pp1.Attributes.Remove("class");
        pp2.Attributes.Add("class", "active");
        pp3.Attributes.Remove("class");
        pp4.Attributes.Remove("class");
        hd_txt.Text = "NOTA KREDIT";
    }
    protected void Button10_Click(object sender, EventArgs e)
    {
        fr2.Visible = true;
        fr1.Visible = false;
        Gridview7.Visible = false;
        Gridview14.Visible = false;
        Gridview11.Visible = true;
        Button5.Visible = true;
        Gridview7.Enabled = true;
        GetUniqueIddeb();
        reset();
        tab4();
        Bindbaucer();
        BindInvois();
        BindMohon();
        Bindcredit();
        Binddedit();
    }

    void tab4()
    {
        p6.Attributes.Add("class", "tab-pane");
        p1.Attributes.Add("class", "tab-pane");
        p2.Attributes.Add("class", "tab-pane");
        p3.Attributes.Add("class", "tab-pane active");
        p4.Attributes.Add("class", "tab-pane");

        pp6.Attributes.Remove("class");
        pp1.Attributes.Remove("class");
        pp2.Attributes.Remove("class");
        pp3.Attributes.Add("class", "active");
        pp4.Attributes.Remove("class");
        hd_txt.Text = "NOTA DEBIT";
    }

    protected void DISChanged(object sender, EventArgs eventArgs)
    {
        decimal numTotal = 0;

        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);

        TextBox qty = (TextBox)row.FindControl("TextBox4");
        TextBox DIS = (TextBox)row.FindControl("Txtdis");
        TextBox unit = (TextBox)row.FindControl("TextBox3");
        Label jum = (Label)row.FindControl("Label1");
        Label jumI = (Label)row.FindControl("Label5");
        tab2();

        decimal qty1 = Convert.ToDecimal(qty.Text);
        decimal JUM1 = Convert.ToDecimal(unit.Text);
        decimal dis = Convert.ToDecimal(DIS.Text);

        if (DIS.ToString() != "")
        {
            numTotal = qty1 * JUM1 - dis;
            jum.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");
            jumI.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");

        }
        //code to go here when pri is changed
    }

    protected void Button15_Click(object sender, EventArgs e)
    {

        if (ddakaun.SelectedItem.Text != "--- PILIH ---")
        {
            if (ddpela2.SelectedItem.Text != "--- PILIH ---")
            {
                if (txtnoruju.Text != "")
                {
                    if (txttcinvois.Text != "")
                    {
                        if (txttcre.Text != "")
                        {

                            DataTable dt = new DataTable();
                            DataTable dt1 = new DataTable();
                            DataTable dt2 = new DataTable();
                            DataTable dt3 = new DataTable();
                            userid = Session["New"].ToString();

                            DateTime tDate = DateTime.ParseExact(txttcinvois.Text, "dd/MM/yyyy", null);
                            DateTime tDate1 = DateTime.ParseExact(txttcre.Text, "dd/MM/yyyy", null);
                            DataTable dtnama = new DataTable();
                            dtnama = Dbcon.Ora_Execute_table("select Ref_no_syarikat,Ref_kod_akaun from  kw_ref_pembekal where Ref_nama_syarikat='" + ddpela2.SelectedItem.Text + "'");
                            DataTable dtkat = new DataTable();
                            dtkat = Dbcon.Ora_Execute_table("select kat_akaun from KW_Ref_Carta_Akaun where kod_akaun='" + dtnama.Rows[0][1].ToString() + "'");

                            DataTable gen_invkd = new DataTable();
                            gen_invkd = Dbcon.Ora_Execute_table("select no_Rujukan from KW_Pembayaran_Credit_item where no_Rujukan='" + txtnoruju.Text + "'");
                            if (gen_invkd.Rows.Count > 0)
                            {
                                DataTable dtkre_1 = Dbcon.Ora_Execute_table("select doc_type_cd,format as fmt,SUBSTRING(cur_format, 1, 10) as lfrmt1,SUBSTRING(cur_format, 11, 4) as lfrmt2,cur_format as cfmt from KW_Format_Nombor_rujukan where doc_type_cd='05' and Status='A'");
                                if (dtkre_1.Rows.Count != 0)
                                {
                                    if (dtkre_1.Rows[0]["cfmt"].ToString() == "")
                                    {
                                        txtnoruju_1.Text = dtkre_1.Rows[0]["fmt"].ToString();
                                        
                                    }
                                    else
                                    {
                                        int seqno = Convert.ToInt32(dtkre_1.Rows[0]["lfrmt2"].ToString());
                                        int newNumber = seqno + 1;
                                        uniqueId = newNumber.ToString(dtkre_1.Rows[0]["lfrmt1"].ToString() + "0000");
                                        txtnoruju_1.Text = uniqueId;                                        
                                    }

                                }
                                else
                                {
                                    DataTable dtkre_2 = Dbcon.Ora_Execute_table("select    ISNULL(max(SUBSTRING(no_Rujukan,13,2000)),'0') from KW_Pembayaran_Credit_item");
                                    if (dtkre_2.Rows.Count > 0)
                                    {
                                        int seqno = Convert.ToInt32(dtkre_2.Rows[0][0].ToString());
                                        int newNumber = seqno + 1;
                                        uniqueId = newNumber.ToString("KTH-KRD" + "-" + DateTime.Now.ToString("yy") + "-" + "0000");
                                        txtnoruju_1.Text = uniqueId;
                                    }
                                    else
                                    {
                                        int newNumber = Convert.ToInt32(uniqueId) + 1;
                                        uniqueId = newNumber.ToString("KTH-KRD" + "-" + DateTime.Now.ToString("yy") + "-" + "0000");
                                        txtnoruju_1.Text = uniqueId;
                                    }
                                }
                            }
                            else
                            {
                                txtnoruju_1.Text = txtnoruju.Text;
                            }

                            decimal Gtotal = 0;
                            int count = 0;
                            string rcount = string.Empty;
                            foreach (GridViewRow g1 in Gridview10.Rows)
                            {
                                count++;
                                rcount = count.ToString();

                                string DD = (g1.FindControl("ddkodcre") as DropDownList).SelectedItem.Value;
                                string KEt = (g1.FindControl("txtket") as TextBox).Text;
                                string total = (g1.FindControl("Txtdis") as TextBox).Text;
                                string DDcukai = (g1.FindControl("ddkodgstoth") as DropDownList).SelectedItem.Value;
                                string ogamt = (g1.FindControl("Label10") as Label).Text;
                                string DDN1 = (g1.FindControl("ddkodgst") as DropDownList).SelectedItem.Value;
                                string gamt = (g1.FindControl("Label8") as Label).Text;
                                string ttotal = (g1.FindControl("Label5") as Label).Text;

                                string DDcukaival_1 = DDcukai;
                                string DDcukaival = string.Empty;
                                string val = string.Empty;
                                string val1 = string.Empty;

                                if (rcount == "1")
                                {
                                    dt1 = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "','" + TextBox18.Text + "','','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + dtkat.Rows[0][0].ToString() + "','" + txtnoruju_1.Text + "','" + ddinv.SelectedItem.Text + "','','" + TextBox18.Text + "','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "','" + KEt + "','','" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "','','','A','')");

                                    DataTable chk_carta = new DataTable();
                                    chk_carta = Dbcon.Ora_Execute_table("select kat_akaun,kod_akaun,KW_Debit_amt,KW_kredit_amt,Kw_open_amt from KW_Ref_Carta_Akaun where kod_akaun='" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "'");
                                    if (chk_carta.Rows.Count != 0)
                                    {
                                        double amt_ope1 = 0;
                                        double amt_deb1 = 0;
                                        double amt_deb2_1 = 0;

                                        DataTable chk_kategory = new DataTable();
                                        chk_kategory = Dbcon.Ora_Execute_table("select kat_cd,kat_akuan,bal_type from KW_Kategori_akaun where kat_cd='" + chk_carta.Rows[0]["kat_akaun"].ToString() + "'");

                                        amt_deb1 = (double.Parse(chk_carta.Rows[0]["KW_Debit_amt"].ToString()) + double.Parse(TextBox18.Text));

                                        if (chk_kategory.Rows.Count != 0)
                                        {
                                            if (chk_kategory.Rows[0]["bal_type"].ToString() == "D")
                                            {
                                                amt_ope1 = (double.Parse(chk_carta.Rows[0]["Kw_open_amt"].ToString()) + double.Parse(TextBox18.Text));
                                            }
                                            else
                                            {
                                                amt_deb2_1 = (double.Parse(TextBox18.Text));
                                                amt_ope1 = (double.Parse(chk_carta.Rows[0]["Kw_open_amt"].ToString()) + (amt_deb2_1));
                                            }
                                        }
                                        DataTable upd_carta = new DataTable();
                                        upd_carta = Dbcon.Ora_Execute_table("UPDATE KW_Ref_Carta_Akaun set KW_Debit_amt='" + amt_deb1 + "',Kw_open_amt='" + amt_ope1 + "' where   kod_akaun='" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "'");
                                    }

                                }

                                DataTable sel_gst = new DataTable();
                                sel_gst = Dbcon.Ora_Execute_table("select Ref_kadar,Ref_kod_akaun From KW_Ref_Tetapan_cukai where Ref_kod_cukai='" + DDcukaival_1 + "'");
                                if (sel_gst.Rows.Count != 0)
                                {
                                    DDcukaival = sel_gst.Rows[0]["Ref_kadar"].ToString();
                                    val = sel_gst.Rows[0]["Ref_kod_akaun"].ToString();
                                }
                                else
                                {
                                    DDcukaival = "0";
                                    val = "";
                                }

                                string DDcukaival1_2 = DDN1;
                                string DDcukaival2 = string.Empty;
                                DataTable sel_gst1 = new DataTable();
                                sel_gst1 = Dbcon.Ora_Execute_table("select Ref_kadar,Ref_kod_akaun From KW_Ref_Tetapan_cukai where Ref_kod_cukai='" + DDcukaival1_2 + "'");
                                if (sel_gst1.Rows.Count != 0)
                                {
                                    val1 = sel_gst1.Rows[0]["Ref_kod_akaun"].ToString();
                                    DDcukaival2 = sel_gst1.Rows[0]["Ref_kadar"].ToString();
                                }
                                else
                                {
                                    val1 = "";
                                    DDcukaival2 = "0";
                                }

                                string chk;
                                decimal tgst = 0;
                                decimal ogst = 0;
                                decimal tgst1 = 0;

                                if ((g1.FindControl("chkcre") as CheckBox).Checked == true)
                                {
                                    chk = "Y";
                                    if (DDcukaival != "--- PILIH ---")
                                    {

                                        tgst = Convert.ToDecimal(gamt);

                                    }
                                    else
                                    {

                                        tgst = Convert.ToDecimal("0.00");
                                    }
                                    if (DDcukaival2 != "--- PILIH ---")
                                    {


                                        tgst1 = Convert.ToDecimal(ogamt);
                                    }
                                    else
                                    {

                                        tgst1 = Convert.ToDecimal("0.00");
                                    }
                                }
                                else
                                {
                                    chk = "N";
                                    if (DDcukaival != "--- PILIH ---")
                                    {

                                        if (gamt != "")
                                        {
                                            tgst = Convert.ToDecimal(gamt);
                                        }
                                        else
                                        {
                                            tgst = Convert.ToDecimal("0.00");
                                        }
                                    }
                                    else
                                    {

                                        tgst = Convert.ToDecimal("0.00");
                                    }
                                    if (DDcukaival2 != "--- PILIH ---")
                                    {

                                        if (ogamt != "")
                                        {
                                            tgst1 = Convert.ToDecimal(ogamt);
                                        }
                                        else
                                        {
                                            tgst1 = Convert.ToDecimal("0.00");
                                        }
                                    }
                                    else
                                    {

                                        tgst1 = Convert.ToDecimal("0.00");
                                    }
                                }
                                string ddcuk;
                                if (DDcukai == "--- PILIH ---")
                                {
                                    ddcuk = "";
                                }
                                else
                                {
                                    ddcuk = DDcukai;
                                }
                                string ddcuk1;
                                if (DDN1 == "--- PILIH ---")
                                {
                                    ddcuk1 = "";
                                }
                                else
                                {
                                    ddcuk1 = DDN1;
                                }
                                string kat1, kat2, kat3;
                                dt = Dbcon.Ora_Execute_table("insert into KW_Pembayaran_Credit_item values('" + ddakaun.SelectedItem.Value + "','" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "','" + ddinv.SelectedItem.Text + "','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "','" + txtnoruju_1.Text + "','" + tDate1.ToString("yyyy/MM/dd hh:mm:ss") + "','" + ddpro1.SelectedItem.Value + "','" + ddinvday.SelectedValue + "','" + DD + "','" + KEt + "','" + chk + "','" + DDcukaival2 + "','" + DDcukaival + "','" + ddcuk1 + "','" + ddcuk + "','" + tgst + "','" + tgst1 + "','" + total + "','" + ttotal + "','A','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','','')");
                                DataTable dtkat1 = new DataTable();
                                dtkat1 = Dbcon.Ora_Execute_table("select kat_akaun from KW_Ref_Carta_Akaun where kod_akaun='" + val + "'");
                                if (dtkat1.Rows.Count > 0)
                                {
                                    kat1 = dtkat1.Rows[0][0].ToString();
                                }
                                else
                                {
                                    kat1 = "0";
                                }
                                DataTable dtkat3 = new DataTable();
                                dtkat3 = Dbcon.Ora_Execute_table("select kat_akaun from KW_Ref_Carta_Akaun where kod_akaun='" + val1 + "'");
                                if (dtkat3.Rows.Count > 0)
                                {
                                    kat3 = dtkat3.Rows[0][0].ToString();
                                }
                                else
                                {
                                    kat3 = "0";
                                }

                                DataTable dtkat2 = new DataTable();
                                dtkat2 = Dbcon.Ora_Execute_table("select kat_akaun from KW_Ref_Carta_Akaun where kod_akaun='" + DD + "'");
                                if (dtkat2.Rows.Count > 0)
                                {
                                    kat2 = dtkat2.Rows[0][0].ToString();
                                }
                                else
                                {
                                    kat2 = "0";
                                }
                                dt1 = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + DD + "','','" + total + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + kat2 + "','" + txtnoruju_1.Text + "','" + ddinv.SelectedItem.Text + "','','" + TextBox18.Text + "','" + tDate1.ToString("yyyy/MM/dd hh:mm:ss") + "','" + KEt + "','','" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "','','','A','')");

                                //update certa Akaun

                                DataTable chk_carta1 = new DataTable();
                                chk_carta1 = Dbcon.Ora_Execute_table("select kat_akaun,kod_akaun,KW_Debit_amt,KW_kredit_amt,Kw_open_amt from KW_Ref_Carta_Akaun where kod_akaun='" + DD + "'");
                                if (chk_carta1.Rows.Count != 0)
                                {
                                    double amt_ope2 = 0;
                                    double amt_deb2 = 0;
                                    double amt_deb3 = 0;

                                    DataTable chk_kategory1 = new DataTable();
                                    chk_kategory1 = Dbcon.Ora_Execute_table("select kat_cd,kat_akuan,bal_type from KW_Kategori_akaun where kat_cd='" + chk_carta1.Rows[0]["kat_akaun"].ToString() + "'");

                                    amt_deb2 = (double.Parse(chk_carta1.Rows[0]["KW_kredit_amt"].ToString()) + double.Parse(total));

                                    if (chk_kategory1.Rows.Count != 0)
                                    {
                                        amt_deb3 = -(double.Parse(total));
                                        if (chk_kategory1.Rows[0]["bal_type"].ToString() == "D")
                                        {
                                            
                                            amt_ope2 = (double.Parse(chk_carta1.Rows[0]["Kw_open_amt"].ToString()) + (amt_deb3));

                                        }
                                        else
                                        {
                                            amt_ope2 = (double.Parse(chk_carta1.Rows[0]["Kw_open_amt"].ToString()) + (amt_deb3));
                                        }
                                    }
                                    DataTable upd_carta = new DataTable();
                                    upd_carta = Dbcon.Ora_Execute_table("UPDATE KW_Ref_Carta_Akaun set KW_kredit_amt='" + amt_deb2 + "',Kw_open_amt='" + amt_ope2 + "' where   kod_akaun='" + DD + "'");
                                }

                                dt2 = Dbcon.Ora_Execute_table("update KW_Opening_Balance set ending_amt=ending_amt + " + TextBox18.Text + " ,upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where kod_akaun='" + DD + "' and opening_year='" + DateTime.Now.ToString("yyyy") + "' ");
                                if (tgst1 != 0)
                                {
                                    dt3 = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + val + "','','" + tgst1 + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + kat1 + "','" + txtnoruju_1.Text + "','" + ddinv.SelectedItem.Text + "','" + ddcuk + "','" + TextBox18.Text + "','" + tDate1.ToString("yyyy/MM/dd hh:mm:ss") + "','" + KEt + "','','" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "','','','A','')");
                                }

                                if (tgst != 0)
                                {
                                    dt3 = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + val1 + "','','" + tgst + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + kat3 + "','" + txtnoruju_1.Text + "','" + ddinv.SelectedItem.Text + "','" + ddcuk1 + "','" + TextBox18.Text + "','" + tDate1.ToString("yyyy/MM/dd hh:mm:ss") + "','" + KEt + "','','" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "','','','A','')");
                                }

                                //  dt2 = Dbcon.Ora_Execute_table("update KW_Penerimaan_invois_item set Overall=Overall - '" + TextBox18.Text + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy / MM / dd hh: mm:ss") + "' where no_invois='" + ddinv.SelectedItem.Text + "' ");

                            }
                            dt2 = Dbcon.Ora_Execute_table("update KW_Pembayaran_invoisBil_item set jumlah=jumlah - " + TextBox18.Text + " , Overall= Overall - " + TextBox18.Text + "  ,upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where no_invois='" + ddinv.SelectedItem.Text + "' and  cre_kod_akaun='" + ddpela2.SelectedItem.Value + "'");
                            //  dt3 = Dbcon.Ora_Execute_table("update KW_Penerimaan_payment set jumlah_baki=jumlah_baki - " + TextBox18.Text + " ,upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "'  where no_invois='" + ddinv.SelectedItem.Text + "'");
                           
                            th2.Visible = false;
                            th1.Visible = true;
                            reset();
                            tab3();
                            Bindbaucer();
                            BindInvois();
                            BindMohon();
                            Bindcredit();
                            Binddedit();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Nota Kredit Dibuat Berjaya.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                            //if (txtnoruju.Text != "")
                            //{
                            //    Session["dbtno"] = txtnoruju.Text;
                            //    Response.Redirect("KW_Penerimaan_credit_rptview.aspx");


                            //}

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Tarikh Kredit.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Tarikh Invois.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Nombor Rujukan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Nama Pelanggan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila pilih Untuk Akuan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    protected void Button16_Click(object sender, EventArgs e)
    {
        th2.Visible = false;
        th1.Visible = true;
        reset();
        tab3();
        Bindbaucer();
        BindInvois();
        BindMohon();
        Bindcredit();
        Binddedit();
    }

    protected void gvSelected_PageIndexChanging_t4(object sender, GridViewPageEventArgs e)
    {
        Gridview6.PageIndex = e.NewPageIndex;
        Gridview6.DataBind();
        Bindbaucer();
        BindInvois();
        BindMohon();
        Bindcredit();
        Binddedit();
        tab3();
    }
    protected void Bindcredit()
    {
        //string fmdate = string.Empty;
        //if (txtctarikh.Text != "")
        //{
        //    string fdate = txtctarikh.Text;
        //    DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //    fmdate = fd.ToString("yyyy-MM-dd") + " 12:00:00.000";
        //}

        //string smb_clk3 = string.Empty;
        //if (chk_kredit.Checked == true)
        //{

        //    smb_clk3 = "";

        //}
        //else
        //{
        //    if (txtcno.Text == "" && txtctarikh.Text == "" && txtnpembe.Text == "")
        //    {
        //        smb_clk3 = "where tarikh_invois>=DATEADD(day, DATEDIFF(day, 0, '" + str_sdt1 + "'), 0) and tarikh_invois<=DATEADD(day, DATEDIFF(day, 0, '" + end_edt1 + "'), +1)";
        //    }
        //    else
        //    {
        //        smb_clk3 = "and tarikh_invois>=DATEADD(day, DATEDIFF(day, 0, '" + str_sdt1 + "'), 0) and tarikh_invois<=DATEADD(day, DATEDIFF(day, 0, '" + end_edt1 + "'), +1)";
        //    }
        //}

        //if (txtcno.Text != "" && txtctarikh.Text == "" && txtnpembe.Text == "")
        //{
        //    qry1 = "select no_Rujukan,Format(tarikh_invois,'dd/MM/yyyy') tarikh_invois,nama_pelanggan,Ref_nama_syarikat,Format(jumlah,'#,##,###.00') jumlah from KW_Pembayaran_Credit_item c left join KW_Ref_Pembekal p on p.Ref_kod_akaun=c.nama_pelanggan where no_Rujukan='" + txtcno.Text + "' "+ smb_clk3 + "";
        //}
        //else if (txtcno.Text == "" && txtctarikh.Text != "" && txtnpembe.Text == "")
        //{
        //    qry1 = "select no_Rujukan,Format(tarikh_invois,'dd/MM/yyyy') tarikh_invois,nama_pelanggan,Ref_nama_syarikat,Format(jumlah,'#,##,###.00') jumlah from KW_Pembayaran_Credit_item c left join KW_Ref_Pembekal p on p.Ref_kod_akaun=c.nama_pelanggan where tarikh_invois='" + fmdate + "' " + smb_clk3 + "";
        //}
        //else if (txtcno.Text == "" && txtctarikh.Text == "" && txtnpembe.Text != "")
        //{
        //    qry1 = "select no_Rujukan,Format(tarikh_invois,'dd/MM/yyyy') tarikh_invois,nama_pelanggan,Ref_nama_syarikat,Format(jumlah,'#,##,###.00') jumlah from KW_Pembayaran_Credit_item c left join KW_Ref_Pembekal p on p.Ref_kod_akaun=c.nama_pelanggan where Ref_nama_syarikat='" + txtnpembe.Text + "' " + smb_clk3 + "";
        //}
        //else if (txtcno.Text != "" && txtctarikh.Text != "" && txtnpembe.Text == "")
        //{
        //    qry1 = "select no_Rujukan,Format(tarikh_invois,'dd/MM/yyyy') tarikh_invois,nama_pelanggan,Ref_nama_syarikat,Format(jumlah,'#,##,###.00') jumlah from KW_Pembayaran_Credit_item c left join KW_Ref_Pembekal p on p.Ref_kod_akaun=c.nama_pelanggan where no_Rujukan='" + txtcno.Text + "' and tarikh_invois='" + fmdate + "' " + smb_clk3 + "";
        //}
        //else if (txtcno.Text == "" && txtctarikh.Text != "" && txtnpembe.Text != "")
        //{
        //    qry1 = "select no_Rujukan,Format(tarikh_invois,'dd/MM/yyyy') tarikh_invois,nama_pelanggan,Ref_nama_syarikat,Format(jumlah,'#,##,###.00') jumlah from KW_Pembayaran_Credit_item c left join KW_Ref_Pembekal p on p.Ref_kod_akaun=c.nama_pelanggan where tarikh_invois='" + fmdate + "' and Ref_nama_syarikat='" + txtnpembe.Text + "' " + smb_clk3 + "";
        //}
        //else if (txtcno.Text != "" && txtctarikh.Text == "" && txtnpembe.Text != "")
        //{
        //    qry1 = "select no_Rujukan,Format(tarikh_invois,'dd/MM/yyyy') tarikh_invois,nama_pelanggan,Ref_nama_syarikat,Format(jumlah,'#,##,###.00') jumlah from KW_Pembayaran_Credit_item c left join KW_Ref_Pembekal p on p.Ref_kod_akaun=c.nama_pelanggan where no_Rujukan='" + txtcno.Text + "' and Ref_nama_syarikat='" + txtnpembe.Text + "' " + smb_clk3 + "";
        //}
        //else if (txtcno.Text != "" && txtctarikh.Text != "" && txtnpembe.Text != "")
        //{
        //    qry1 = "select no_Rujukan,Format(tarikh_invois,'dd/MM/yyyy') tarikh_invois,nama_pelanggan,Ref_nama_syarikat,Format(jumlah,'#,##,###.00') jumlah from KW_Pembayaran_Credit_item c left join KW_Ref_Pembekal p on p.Ref_kod_akaun=c.nama_pelanggan where no_Rujukan='" + txtcno.Text + "' and tarikh_invois='" + fmdate + "' and Ref_nama_syarikat='" + txtnpembe.Text + "' " + smb_clk3 + "";
        //}
        //else
        //{
        //    qry1 = "select no_Rujukan,Format(tarikh_invois,'dd/MM/yyyy') tarikh_invois,nama_pelanggan,Ref_nama_syarikat,Format(jumlah,'#,##,###.00') jumlah from KW_Pembayaran_Credit_item c left join KW_Ref_Pembekal p on p.Ref_kod_akaun=c.nama_pelanggan " + smb_clk3 + "";

        //}
        //SqlCommand cmd2 = new SqlCommand("" + qry1 + "", con);
        //SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        //DataSet ds2 = new DataSet();
        //da2.Fill(ds2);
        //if (ds2.Tables[0].Rows.Count == 0)
        //{
        //    ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
        //    Gridview6.DataSource = ds2;
        //    Gridview6.DataBind();
        //    int columncount = Gridview6.Rows[0].Cells.Count;
        //    Gridview6.Rows[0].Cells.Clear();
        //    Gridview6.Rows[0].Cells.Add(new TableCell());
        //    Gridview6.Rows[0].Cells[0].ColumnSpan = columncount;
        //    Gridview6.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
        //}
        //else
        //{
        //    Gridview6.DataSource = ds2;
        //    Gridview6.DataBind();
        //}
    }

    protected void Button12_Click(object sender, EventArgs e)
    {
        Bindbaucer();
        BindInvois();
        BindMohon();
        Bindcredit();
        Binddedit();
        tab3();
    }

    protected void gvSelected_PageIndexChanging_t5(object sender, GridViewPageEventArgs e)
    {
        Gridview8.PageIndex = e.NewPageIndex;
        Gridview8.DataBind();
        Bindbaucer();
        BindInvois();
        BindMohon();
        Bindcredit();
        Binddedit();
        tab4();
    }

    protected void Binddedit()
    {
       // string fmdate = string.Empty;
       // if (TextBox20.Text != "")
       // {
       //     string fdate = TextBox20.Text;
       //     DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
       //     fmdate = fd.ToString("yyyy-MM-dd") + " 12:00:00.000";
       // }

       // string smb_clk4 = string.Empty;
       // if (chk_debit.Checked == true)
       // {

       //     smb_clk4 = "";

       // }
       // else
       // {
       //     if (TextBox15.Text == "" && txttarikhinvois.Text == "" && jenis_trxn.SelectedValue == "")
       //     {
       //         smb_clk4 = "where tarikh_invois>=DATEADD(day, DATEDIFF(day, 0, '" + str_sdt1 + "'), 0) and tarikh_invois<=DATEADD(day, DATEDIFF(day, 0, '" + end_edt1 + "'), +1)";
       //     }
       //     else
       //     {
       //         smb_clk4 = "and tarikh_invois>=DATEADD(day, DATEDIFF(day, 0, '" + str_sdt1 + "'), 0) and tarikh_invois<=DATEADD(day, DATEDIFF(day, 0, '" + end_edt1 + "'), +1)";
       //     }
       // }

       // if (TextBox12.Text != "" && TextBox20.Text == "" && TextBox21.Text == "")
       // {
       //     qry1 = "select no_rujukan,Format(tarikh_invois,'dd/MM/yyyy') tarikh_invois,nama_pelanggan,Ref_nama_syarikat,Format(jumlah,'#,##,###.00') jumlah  from KW_Pembayaran_Dedit_item d left join KW_Ref_Pembekal  p on p.Ref_kod_akaun=d.nama_pelanggan where no_Rujukan='" + TextBox12.Text + "' "+ smb_clk4 + "";
       // }
       // else if (TextBox12.Text == "" && TextBox20.Text != "" && TextBox21.Text == "")
       // {
       //     qry1 = "select no_rujukan,Format(tarikh_invois,'dd/MM/yyyy') tarikh_invois,nama_pelanggan,Ref_nama_syarikat,Format(jumlah,'#,##,###.00') jumlah  from KW_Pembayaran_Dedit_item d left join KW_Ref_Pembekal  p on p.Ref_kod_akaun=d.nama_pelanggan where tarikh_invois='" + fmdate + "' " + smb_clk4 + "";
       // }
       // else if (TextBox12.Text == "" && TextBox20.Text == "" && TextBox21.Text != "")
       // {
       //     qry1 = "select no_rujukan,Format(tarikh_invois,'dd/MM/yyyy') tarikh_invois,nama_pelanggan,Ref_nama_syarikat,Format(jumlah,'#,##,###.00') jumlah  from KW_Pembayaran_Dedit_item d left join KW_Ref_Pembekal  p on p.Ref_kod_akaun=d.nama_pelanggan where Ref_nama_syarikat='" + TextBox21.Text + "' " + smb_clk4 + "";
       // }
       // else if (TextBox12.Text != "" && TextBox20.Text != "" && TextBox21.Text == "")
       // {
       //     qry1 = "select no_rujukan,Format(tarikh_invois,'dd/MM/yyyy') tarikh_invois,nama_pelanggan,Ref_nama_syarikat,Format(jumlah,'#,##,###.00') jumlah  from KW_Pembayaran_Dedit_item d left join KW_Ref_Pembekal  p on p.Ref_kod_akaun=d.nama_pelanggan where no_Rujukan='" + TextBox12.Text + "' and tarikh_invois='" + fmdate + "' " + smb_clk4 + "";
       // }
       // else if (TextBox12.Text == "" && TextBox20.Text != "" && TextBox21.Text != "")
       // {
       //     qry1 = "select no_rujukan,Format(tarikh_invois,'dd/MM/yyyy') tarikh_invois,nama_pelanggan,Ref_nama_syarikat,Format(jumlah,'#,##,###.00') jumlah  from KW_Pembayaran_Dedit_item d left join KW_Ref_Pembekal  p on p.Ref_kod_akaun=d.nama_pelanggan where tarikh_invois='" + fmdate + "' and Ref_nama_syarikat='" + TextBox21.Text + "' " + smb_clk4 + "";
       // }
       // else if (TextBox12.Text != "" && TextBox20.Text == "" && TextBox21.Text != "")
       // {
       //     qry1 = "select no_rujukan,Format(tarikh_invois,'dd/MM/yyyy') tarikh_invois,nama_pelanggan,Ref_nama_syarikat,Format(jumlah,'#,##,###.00') jumlah  from KW_Pembayaran_Dedit_item d left join KW_Ref_Pembekal  p on p.Ref_kod_akaun=d.nama_pelanggan where no_Rujukan='" + TextBox12.Text + "' and Ref_nama_syarikat='" + TextBox21.Text + "' " + smb_clk4 + "";
       // }
       // else if (TextBox12.Text != "" && TextBox20.Text != "" && TextBox21.Text != "")
       // {
       //     qry1 = "select no_rujukan,Format(tarikh_invois,'dd/MM/yyyy') tarikh_invois,nama_pelanggan,Ref_nama_syarikat,Format(jumlah,'#,##,###.00') jumlah  from KW_Pembayaran_Dedit_item d left join KW_Ref_Pembekal  p on p.Ref_kod_akaun=d.nama_pelanggan where no_Rujukan='" + TextBox12.Text + "' and tarikh_invois='" + fmdate + "' and Ref_nama_syarikat='" + TextBox21.Text + "' " + smb_clk4 + "";
       // }
       //else
       // {
       //     qry1 = "select no_rujukan,Format(tarikh_invois,'dd/MM/yyyy') tarikh_invois,nama_pelanggan,Ref_nama_syarikat,Format(jumlah,'#,##,###.00') jumlah  from KW_Pembayaran_Dedit_item d left join KW_Ref_Pembekal  p on p.Ref_kod_akaun=d.nama_pelanggan "+ smb_clk4 + "";
       // }
       // SqlCommand cmd2 = new SqlCommand("" + qry1 + "", con);
       // SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
       // DataSet ds2 = new DataSet();
       // da2.Fill(ds2);
       // if (ds2.Tables[0].Rows.Count == 0)
       // {
       //     ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
       //     Gridview8.DataSource = ds2;
       //     Gridview8.DataBind();
       //     int columncount = Gridview8.Rows[0].Cells.Count;
       //     Gridview8.Rows[0].Cells.Clear();
       //     Gridview8.Rows[0].Cells.Add(new TableCell());
       //     Gridview8.Rows[0].Cells[0].ColumnSpan = columncount;
       //     Gridview8.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
       // }
       // else
       // {
       //     Gridview8.DataSource = ds2;
       //     Gridview8.DataBind();
       // }
    }

    protected void Button13_Click(object sender, EventArgs e)
    {
        Bindbaucer();
        BindInvois();
        BindMohon();
        Bindcredit();
        Binddedit();
        tab4();
    }

    protected void kemtab3_Click(object sender, EventArgs e)
    {
        if (ddakaun.SelectedItem.Text != "--- PILIH ---")
        {
            if (ddpela2.SelectedItem.Text != "--- PILIH ---")
            {
                if (txtnoruju.Text != "")
                {
                    if (txttcinvois.Text != "")
                    {

                        if (ddpro1.SelectedItem.Text != "--- PILIH ---")
                        {

                            th2.Visible = false;
                            th1.Visible = true;
                            DateTime tDate = DateTime.Parse(txttcinvois.Text);

                            DataTable dt = new DataTable();
                            foreach (GridViewRow g1 in Gridview9.Rows)
                            {
                                string item = (g1.FindControl("TextBox1") as TextBox).Text;
                                string keter = (g1.FindControl("TextBox2") as TextBox).Text;
                                string unit = (g1.FindControl("TextBox3") as TextBox).Text;
                                string qty = (g1.FindControl("TextBox4") as TextBox).Text;
                                string gst = (g1.FindControl("TextBox6") as TextBox).Text;
                                string total = (g1.FindControl("TextBox5") as TextBox).Text;
                                string DD = (g1.FindControl("ddkod") as DropDownList).SelectedItem.Value;
                                string chk;
                                if ((g1.FindControl("CheckBox1") as CheckBox).Checked == true)
                                {
                                    chk = "Y";
                                }
                                else
                                {
                                    chk = "N";
                                }
                                dt = Dbcon.Ora_Execute_table("update KW_Pembayaran_Credit set untuk_akaun='" + ddakaun.SelectedItem.Value + "',nama_pelanggan='" + ddpela2.SelectedItem.Value + "',tarikh_invois='" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "',project_kod='" + ddpro1.SelectedItem.Value + "',kod_akauan='" + DD + "',item='" + item + "',keterangan='" + keter + "',unit='" + unit + "',quantiti='" + qty + "',gst='" + gst + "',tax='" + chk + "',jumlah='" + total + "' where no_Rujukan='" + txtnoruju.Text + "'");
                            }
                            reset();
                            Bindbaucer();
                            BindInvois();
                            BindMohon();
                            Bindcredit();
                            Binddedit();

                        }
                    }
                }
            }
        }
    }
    protected void simtab4_Click(object sender, EventArgs e)
    {
        if (ddakaun.SelectedItem.Text != "--- PILIH ---")
        {
            if (ddpela3.SelectedItem.Text != "--- PILIH ---")
            {
                if (txtnoruj2.Text != "")
                {
                    if (txtdtinvois.Text != "")
                    {
                        if (txtdeb.Text != "")
                        {
                            DataTable dt = new DataTable();
                            DataTable dt1 = new DataTable();
                            DataTable dt2 = new DataTable();
                            DataTable dt3 = new DataTable();
                            userid = Session["New"].ToString();

                            DateTime tDate = DateTime.ParseExact(txtdtinvois.Text, "dd/MM/yyyy", null);
                            DateTime tDate1 = DateTime.ParseExact(txtdeb.Text, "dd/MM/yyyy", null);
                            DataTable dtnama = new DataTable();
                            dtnama = Dbcon.Ora_Execute_table("select Ref_no_syarikat,Ref_kod_akaun from  KW_Ref_Pembekal where Ref_nama_syarikat='" + ddpela3.SelectedItem.Text + "'");
                            DataTable dtkat = new DataTable();
                            dtkat = Dbcon.Ora_Execute_table("select kat_akaun from KW_Ref_Carta_Akaun where kod_akaun='" + dtnama.Rows[0][1].ToString() + "'");

                            DataTable gen_invkd = new DataTable();
                            gen_invkd = Dbcon.Ora_Execute_table("select no_invois from KW_Penerimaan_invois where no_invois='" + txtnoruj2.Text + "'");
                            if (gen_invkd.Rows.Count > 0)
                            {
                                DataTable dtdeb_1 = Dbcon.Ora_Execute_table("select doc_type_cd,format as fmt,SUBSTRING(cur_format, 1, 10) as lfrmt1,SUBSTRING(cur_format, 11, 4) as lfrmt2,cur_format as cfmt from KW_Format_Nombor_rujukan where doc_type_cd='06' and Status='A'");
                                if (dtdeb_1.Rows.Count != 0)
                                {
                                    if (dtdeb_1.Rows[0]["cfmt"].ToString() == "")
                                    {
                                        txtnoruj2_2.Text = dtdeb_1.Rows[0]["fmt"].ToString();
                                    }
                                    else
                                    {
                                        int seqno = Convert.ToInt32(dtdeb_1.Rows[0]["lfrmt2"].ToString());
                                        int newNumber = seqno + 1;
                                        uniqueId = newNumber.ToString(dtdeb_1.Rows[0]["lfrmt1"].ToString() + "0000");
                                        txtnoruj2_2.Text = uniqueId;                                        
                                    }

                                }
                                else
                                {
                                    DataTable dtdeb_2 = Dbcon.Ora_Execute_table("select   ISNULL(max(SUBSTRING(no_Rujukan,13,2000)),'0') from KW_Pembayaran_Dedit_item");
                                    if (dtdeb_2.Rows.Count > 0)
                                    {
                                        int seqno = Convert.ToInt32(dtdeb_2.Rows[0][0].ToString());
                                        int newNumber = seqno + 1;
                                        uniqueId = newNumber.ToString("KTH-DBT" + " - " + DateTime.Now.ToString("yy") + " - " + "0000");
                                        txtnoruj2_2.Text = uniqueId;                                        

                                    }
                                    else
                                    {
                                        int newNumber = Convert.ToInt32(uniqueId) + 1;
                                        uniqueId = newNumber.ToString("KTH-DBT" + " - " + DateTime.Now.ToString("yy") + " - " + "0000");
                                        txtnoruj2_2.Text = uniqueId;                                        
                                    }
                                }
                            }
                            else
                            {
                                txtnoruj2_2.Text = txtnoruj2.Text;
                            }

                            decimal Gtotal = 0;
                            int count = 0;
                            string rcount = string.Empty;
                            foreach (GridViewRow g1 in Gridview11.Rows)
                            {
                                count++;
                                rcount = count.ToString();
                                string DD = (g1.FindControl("ddkoddeb") as DropDownList).SelectedItem.Value;
                                string KEt = (g1.FindControl("txtket") as TextBox).Text;
                                string total = (g1.FindControl("Txtdis") as TextBox).Text;
                                string DDcukai = (g1.FindControl("ddgstdeboth") as DropDownList).SelectedItem.Value;
                                string ogamt = (g1.FindControl("Label10") as Label).Text;
                                string DDN1 = (g1.FindControl("ddgstdeb") as DropDownList).SelectedItem.Value;
                                string gamt = (g1.FindControl("Label8") as Label).Text;
                                string ttotal = (g1.FindControl("Label5") as Label).Text;

                                string DDcukaival_1 = DDcukai;
                                string DDcukaival = string.Empty;
                                string val = string.Empty;
                                string val1 = string.Empty;

                                if (rcount == "1")
                                {
                                    dt1 = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "','','" + TextBox1.Text + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + dtkat.Rows[0][0].ToString() + "','" + txtnoruj2.Text + "','" + ddinv2.SelectedItem.Text + "','','','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "','" + KEt + "','','" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "','','','A','')");

                                    DataTable chk_carta1 = new DataTable();
                                    chk_carta1 = Dbcon.Ora_Execute_table("select kat_akaun,kod_akaun,KW_Debit_amt,KW_kredit_amt,Kw_open_amt from KW_Ref_Carta_Akaun where kod_akaun='" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "'");
                                    if (chk_carta1.Rows.Count != 0)
                                    {
                                        double amt_ope2 = 0;
                                        double amt_deb2 = 0;
                                        double amt_deb3 = 0;

                                        DataTable chk_kategory1 = new DataTable();
                                        chk_kategory1 = Dbcon.Ora_Execute_table("select kat_cd,kat_akuan,bal_type from KW_Kategori_akaun where kat_cd='" + chk_carta1.Rows[0]["kat_akaun"].ToString() + "'");

                                        amt_deb2 = (double.Parse(chk_carta1.Rows[0]["KW_kredit_amt"].ToString()) + double.Parse(TextBox1.Text));

                                        if (chk_kategory1.Rows.Count != 0)
                                        {
                                            amt_deb3 = -(double.Parse(TextBox1.Text));
                                            if (chk_kategory1.Rows[0]["bal_type"].ToString() == "D")
                                            {
                                                
                                                amt_ope2 = (double.Parse(chk_carta1.Rows[0]["Kw_open_amt"].ToString()) + (amt_deb3));

                                            }
                                            else
                                            {
                                                amt_ope2 = (double.Parse(chk_carta1.Rows[0]["Kw_open_amt"].ToString()) + (amt_deb3));
                                            }
                                        }
                                        DataTable upd_carta = new DataTable();
                                        upd_carta = Dbcon.Ora_Execute_table("UPDATE KW_Ref_Carta_Akaun set KW_kredit_amt='" + amt_deb2 + "',Kw_open_amt='" + amt_ope2 + "' where   kod_akaun='" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "'");
                                    }

                                }

                                DataTable sel_gst = new DataTable();
                                sel_gst = Dbcon.Ora_Execute_table("select Ref_kadar,Ref_kod_akaun From KW_Ref_Tetapan_cukai where Ref_kod_cukai='" + DDcukaival_1 + "'");
                                if (sel_gst.Rows.Count != 0)
                                {
                                    DDcukaival = sel_gst.Rows[0]["Ref_kadar"].ToString();
                                    val = sel_gst.Rows[0]["Ref_kod_akaun"].ToString();
                                }
                                else
                                {
                                    DDcukaival = "0";
                                    val = "";
                                }

                                string DDcukaival1_2 = DDN1;
                                string DDcukaival2 = string.Empty;
                                DataTable sel_gst1 = new DataTable();
                                sel_gst1 = Dbcon.Ora_Execute_table("select Ref_kadar,Ref_kod_akaun From KW_Ref_Tetapan_cukai where Ref_kod_cukai='" + DDcukaival1_2 + "'");
                                if (sel_gst1.Rows.Count != 0)
                                {
                                    val1 = sel_gst1.Rows[0]["Ref_kod_akaun"].ToString();
                                    DDcukaival2 = sel_gst1.Rows[0]["Ref_kadar"].ToString();
                                }
                                else
                                {
                                    val1 = "";
                                    DDcukaival2 = "0";
                                }

                                string chk;
                                decimal tgst = 0;
                                decimal ogst = 0;
                                decimal tgst1 = 0;

                                if ((g1.FindControl("Chkdeb") as CheckBox).Checked == true)
                                {
                                    chk = "Y";
                                    if (DDcukaival != "--- PILIH ---")
                                    {

                                        tgst = Convert.ToDecimal(gamt);

                                    }
                                    else
                                    {

                                        tgst = Convert.ToDecimal("0.00");
                                    }
                                    if (DDcukaival2 != "--- PILIH ---")
                                    {


                                        tgst1 = Convert.ToDecimal(ogamt);
                                    }
                                    else
                                    {

                                        tgst1 = Convert.ToDecimal("0.00");
                                    }
                                }
                                else
                                {
                                    chk = "N";
                                    if (DDcukaival != "--- PILIH ---")
                                    {

                                        if (gamt != "")
                                        {
                                            tgst = Convert.ToDecimal(gamt);
                                        }
                                        else
                                        {
                                            tgst = Convert.ToDecimal("0.00");
                                        }
                                    }
                                    else
                                    {

                                        tgst = Convert.ToDecimal("0.00");
                                    }
                                    if (DDcukaival2 != "--- PILIH ---")
                                    {

                                        if (ogamt != "")
                                        {
                                            tgst1 = Convert.ToDecimal(ogamt);
                                        }
                                        else
                                        {
                                            tgst1 = Convert.ToDecimal("0.00");
                                        }
                                    }
                                    else
                                    {

                                        tgst1 = Convert.ToDecimal("0.00");
                                    }
                                }
                                string ddcuk;
                                if (DDcukai == "--- PILIH ---")
                                {
                                    ddcuk = "";
                                }
                                else
                                {
                                    ddcuk = DDcukai;
                                }
                                string ddcuk1;
                                if (DDN1 == "--- PILIH ---")
                                {
                                    ddcuk1 = "";
                                }
                                else
                                {
                                    ddcuk1 = DDN1;
                                }

                                dt = Dbcon.Ora_Execute_table("insert into KW_Pembayaran_Dedit_item values('" + ddakaun.SelectedItem.Value + "','" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "','" + ddinv2.SelectedItem.Text + "','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "','" + txtnoruj2_2.Text + "','" + tDate1.ToString("yyyy/MM/dd hh:mm:ss") + "','" + ddpro2.SelectedItem.Value + "','" + ddinvday2.SelectedValue + "','" + DD + "','" + KEt + "','" + chk + "','" + DDcukaival2 + "','" + DDcukaival + "','" + ddcuk1 + "','" + ddcuk + "','" + tgst + "','" + tgst1 + "','" + total + "','" + ttotal + "','A','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','','')");

                                DataTable dt_upd_format = new DataTable();
                                dt_upd_format = Dbcon.Ora_Execute_table("update KW_Format_Nombor_rujukan set cur_format='" + txtnoruj2_2.Text + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where doc_type_cd='06' and Status = 'A'");

                                string kat1, kat2, kat3;
                                DataTable dtkat1 = new DataTable();
                                dtkat1 = Dbcon.Ora_Execute_table("select kat_akaun from KW_Ref_Carta_Akaun where kod_akaun='" + val + "'");
                                if (dtkat1.Rows.Count > 0)
                                {
                                    kat1 = dtkat1.Rows[0][0].ToString();
                                }
                                else
                                {
                                    kat1 = "0";
                                }
                                DataTable dtkat3 = new DataTable();
                                dtkat3 = Dbcon.Ora_Execute_table("select kat_akaun from KW_Ref_Carta_Akaun where kod_akaun='" + val1 + "'");
                                if (dtkat1.Rows.Count > 0)
                                {
                                    kat3 = dtkat3.Rows[0][0].ToString();
                                }
                                else
                                {
                                    kat3 = "0";
                                }
                                DataTable dtkat2 = new DataTable();
                                dtkat2 = Dbcon.Ora_Execute_table("select kat_akaun from KW_Ref_Carta_Akaun where kod_akaun='" + DD + "'");
                                if (dtkat2.Rows.Count > 0)
                                {
                                    kat2 = dtkat2.Rows[0][0].ToString();
                                }
                                else
                                {
                                    kat2 = "0";
                                }
                                dt1 = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + DD + "','" + total + "','','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + kat2 + "','" + txtnoruj2_2.Text + "','" + ddinv2.SelectedItem.Text + "','','','" + tDate1.ToString("yyyy/MM/dd hh:mm:ss") + "','" + KEt + "','','" + DD + "','','','A','')");

                                // update certa Akaun

                                DataTable chk_carta = new DataTable();
                                chk_carta = Dbcon.Ora_Execute_table("select kat_akaun,kod_akaun,KW_Debit_amt,KW_kredit_amt,Kw_open_amt from KW_Ref_Carta_Akaun where kod_akaun='" + DD + "'");
                                if (chk_carta.Rows.Count != 0)
                                {
                                    double amt_ope1 = 0;
                                    double amt_deb1 = 0;
                                    double amt_deb2_1 = 0;
                                    DataTable chk_kategory = new DataTable();
                                    chk_kategory = Dbcon.Ora_Execute_table("select kat_cd,kat_akuan,bal_type from KW_Kategori_akaun where kat_cd='" + chk_carta.Rows[0]["kat_akaun"].ToString() + "'");

                                    amt_deb1 = (double.Parse(chk_carta.Rows[0]["KW_Debit_amt"].ToString()) + double.Parse(total));

                                    if (chk_kategory.Rows.Count != 0)
                                    {
                                        if (chk_kategory.Rows[0]["bal_type"].ToString() == "D")
                                        {
                                            amt_ope1 = (double.Parse(chk_carta.Rows[0]["Kw_open_amt"].ToString()) + double.Parse(total));
                                        }
                                        else
                                        {
                                            amt_deb2_1 = (double.Parse(total));
                                            amt_ope1 = (double.Parse(chk_carta.Rows[0]["Kw_open_amt"].ToString()) + (double.Parse(total)));
                                        }
                                    }
                                    DataTable upd_carta = new DataTable();
                                    upd_carta = Dbcon.Ora_Execute_table("UPDATE KW_Ref_Carta_Akaun set KW_Debit_amt='" + amt_deb1 + "',Kw_open_amt='" + amt_ope1 + "' where   kod_akaun='" + DD + "'");
                                }

                                // end

                                if (tgst1 != 0)
                                {
                                    dt3 = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + val + "','" + tgst1 + "','','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + kat1 + "','" + txtnoruj2_2.Text + "','" + ddinv2.SelectedItem.Text + "','" + ddcuk + "','" + TextBox1.Text + "','" + tDate1.ToString("yyyy/MM/dd hh:mm:ss") + "','" + KEt + "','','" + val + "','','','A','')");
                                }

                                if (tgst != 0)
                                {
                                    dt3 = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + val1 + "','" + tgst + "','','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + kat3 + "','" + txtnoruj2_2.Text + "','" + ddinv2.SelectedItem.Text + "','" + ddcuk1 + "','" + TextBox1.Text + "','" + tDate1.ToString("yyyy/MM/dd hh:mm:ss") + "','" + KEt + "','','" + val1 + "','','','A','')");
                                }

                            }
                            dt2 = Dbcon.Ora_Execute_table("update KW_Pembayaran_invoisBil_item set jumlah=jumlah + " + TextBox1.Text + " , Overall= Overall + " + TextBox1.Text + "  ,upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where no_invois='" + ddinv2.SelectedItem.Text + "' and  cre_kod_akaun='" + ddpela3.SelectedItem.Value + "'");
                            // dt3 = Dbcon.Ora_Execute_table("update KW_Penerimaan_payment set jumlah_baki=jumlah_baki + " + TextBox1.Text + " ,upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "'  where no_invois='" + ddinv2.SelectedItem.Text + "'");
                            fr2.Visible = false;
                            fr1.Visible = true;
                            reset();
                            tab4();
                            Bindbaucer();
                            BindInvois();
                            BindMohon();
                            Bindcredit();
                            Binddedit();

                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Nota Debit Dibuat Berjaya.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Tarikh Debit.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Tarikh Invois.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Nombor Rujukan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Nama Pelanggan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila pilih Untuk Akuan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }
    protected void kemtab4_Click(object sender, EventArgs e)
    {
        if (ddakaun.SelectedItem.Text != "--- PILIH ---")
        {
            if (ddpela3.SelectedItem.Text != "--- PILIH ---")
            {
                if (txtnoruj2.Text != "")
                {
                    if (txtdtinvois.Text != "")
                    {

                        if (ddpro2.SelectedItem.Text != "--- PILIH ---")
                        {


                            DateTime tDate = DateTime.Parse(txtdtinvois.Text);

                            DataTable dt = new DataTable();
                            foreach (GridViewRow g1 in Gridview7.Rows)
                            {
                                string item = (g1.FindControl("TextBox1") as TextBox).Text;
                                string keter = (g1.FindControl("TextBox2") as TextBox).Text;
                                string unit = (g1.FindControl("TextBox3") as TextBox).Text;
                                string qty = (g1.FindControl("TextBox4") as TextBox).Text;
                                string gst = (g1.FindControl("TextBox6") as TextBox).Text;
                                string total = (g1.FindControl("TextBox5") as TextBox).Text;
                                string DD = (g1.FindControl("ddkod") as DropDownList).SelectedItem.Value;
                                string chk;
                                if ((g1.FindControl("CheckBox1") as CheckBox).Checked == true)
                                {
                                    chk = "Y";
                                }
                                else
                                {
                                    chk = "N";
                                }
                                dt = Dbcon.Ora_Execute_table("update KW_Pembayaran_Dedit set untuk_akaun='" + ddakaun.SelectedItem.Value + "',nama_pelanggan='" + ddpela3.SelectedItem.Value + "',tarikh_invois='" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "',project_kod='" + ddpro2.SelectedItem.Value + "',kod_akauan='" + DD + "',item='" + item + "',keterangan='" + keter + "',unit='" + unit + "',quantiti='" + qty + "',gst='" + gst + "',tax='" + chk + "',jumlah='" + total + "' where no_Rujukan='" + txtnoruj2.Text + "'");
                            }
                            fr2.Visible = false;
                            fr1.Visible = true;
                            reset();
                            tab4();
                            Bindbaucer();
                            BindInvois();
                            BindMohon();
                            Bindcredit();
                            Binddedit();
                           

                        }
                    }
                }
            }
        }
    }
    protected void tubtab4_Click(object sender, EventArgs e)
    {
        fr1.Visible = true;
        fr2.Visible = false;
        reset();
        tab4();
        Bindbaucer();
        BindInvois();
        BindMohon();
        Bindcredit();
        Binddedit();
    }


    // textbox changed
    protected void QtyChangedt1(object sender, EventArgs eventArgs)
    {
        decimal numTotal = 0;
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox qty = (TextBox)row.FindControl("TextBox14");
        TextBox tot = (TextBox)row.FindControl("TextBox16");
        if (qty.ToString() != "")
        {
            tab1();
            decimal jum = Convert.ToDecimal(qty.Text);


            numTotal = jum;
            tot.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");
            GrandTotalt1();
        }

    }

    protected void QtyChangedt2(object sender, EventArgs eventArgs)
    {
        decimal numTotal = 0;
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox qty = (TextBox)row.FindControl("TextBox14");
        TextBox gst = (TextBox)row.FindControl("TextBox15");
        TextBox tot = (TextBox)row.FindControl("TextBox16");

        if (qty.ToString() != "")
        {
            tab1();
            decimal jum = Convert.ToDecimal(qty.Text);
            decimal jum1 = Convert.ToDecimal(gst.Text);

            numTotal = jum + jum1;
            tot.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");
            GrandTotalt2();
        }

    }


    protected void QtyChangedBil(object sender, EventArgs eventArgs)
    {
        decimal numTotal = 0;
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox qty = (TextBox)row.FindControl("TextBox54");
        TextBox unit = (TextBox)row.FindControl("TextBox53");
        Label jum = (Label)row.FindControl("Label1");
        Label jumI = (Label)row.FindControl("Label5");
        //int qty1 = Convert.ToInt32(qty.Text);
        decimal unit1 = Convert.ToDecimal(unit.Text);

        CheckBox chk1 = (CheckBox)row.FindControl("CheckBox1");
        DropDownList dd1 = (DropDownList)row.FindControl("ddcukaiinv");
        DropDownList dd2 = (DropDownList)row.FindControl("ddcukaioth");

        tab5();
        decimal qty1 = 1;
        if (qty.Text != "")
        {
            qty1 = Convert.ToInt32(qty.Text);
        }

        if (qty1.ToString() != "")
        {
            numTotal = qty1 * unit1;
            jum.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");
            jumI.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");
            GrandTotalBil();
        }

        //code to go here when pri is changed


        if (chk1.Checked == true)
        {
            if (dd1.SelectedValue != "")
            {
                decimal numTotal1 = 0;
                decimal numTotal2 = 0;
                decimal numTotal3 = 0;
                decimal numTotal4 = 0;
                //int selRowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
                Label tOt = (Label)row.FindControl("Label1");
                //TextBox qty = (TextBox)Gridview5.Rows[selRowIndex].FindControl("TextBox54");
                //TextBox unit = (TextBox)Gridview5.Rows[selRowIndex].FindControl("TextBox53");
                TextBox dis = (TextBox)row.FindControl("Txtdis");
                Label txt = (Label)Gridview5.FooterRow.FindControl("Label4");
                Label gamt = (Label)row.FindControl("Label8");
                Label gmt = (Label)row.FindControl("Label10");
                Label txt1 = (Label)row.FindControl("Label5");
                CheckBox chk = (CheckBox)row.FindControl("CheckBox1");
                DropDownList dd = (DropDownList)row.FindControl("ddcukaioth");
                String tgst_1 = (row.FindControl("ddcukaiinv") as DropDownList).SelectedItem.Value;
                Label tax1 = (Label)Gridview5.FooterRow.FindControl("Label7");
                string gmt1;
                if (gmt.Text == "")
                {
                    gmt1 = "0.00";
                }
                else
                {
                    gmt1 = gmt.Text;
                }
                string tgst = string.Empty;
                DataTable sel_gst = new DataTable();
                sel_gst = Dbcon.Ora_Execute_table("select Ref_nama_cukai,Ref_kadar,Ref_kod_cukai From KW_Ref_Tetapan_cukai where Ref_kod_cukai='" + tgst_1 + "'");
                if (sel_gst.Rows.Count != 0)
                {
                    tgst = sel_gst.Rows[0]["Ref_kadar"].ToString();
                }
                else
                {
                    tgst = "0";
                }

                if (tgst_1 != "--- PILIH ---")
                {
                    if (chk.Checked == true)
                    {

                        decimal tot1 = Convert.ToDecimal(txt1.Text);
                        decimal tgst1 = Convert.ToDecimal(tgst) + 100;
                        decimal fgst = tgst1 / 100;
                        numTotal1 = tot1 / fgst;
                        numTotal4 = tot1 - numTotal1;
                        txt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                        gamt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                        numTotal2 = Convert.ToDecimal(txt1.Text) - numTotal4;
                        numTotal3 = Convert.ToDecimal(txt1.Text);
                        tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                        txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                        tab5();
                        dd.Enabled = true;
                        //dd.Attributes.Remove("style");
                        dd.Attributes.Remove("readonly");
                        GrandTotalBil();
                    }
                    else
                    {
                        decimal tot1 = Convert.ToDecimal(tOt.Text) + Convert.ToDecimal(gmt1);
                        int tgst1 = Convert.ToInt32(tgst);

                        numTotal1 = tot1 * tgst1 / 100;
                        txt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                        gamt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                        numTotal2 = Convert.ToDecimal(tOt.Text) + numTotal1 + Convert.ToDecimal(gmt1);
                        txt1.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                        tab5();
                        GrandTotalBil();
                    }
                }
                else
                {
                    if (qty.Text != "" && unit.Text != "")
                    {
                        if (dis.Text == "")
                        {
                            dis.Text = "0.00";
                        }
                        numTotal1 = Convert.ToDecimal(qty.Text) * Convert.ToDecimal(unit.Text);
                        if (chk.Checked == true)
                        {
                            decimal tot1 = Convert.ToDecimal(txt1.Text);
                            decimal tgst1 = Convert.ToDecimal(tgst) + 100;
                            decimal fgst = tgst1 / 100;
                            numTotal1 = tot1 / fgst;
                            numTotal4 = tot1 - numTotal1;
                            txt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                            gamt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                            numTotal2 = Convert.ToDecimal(txt1.Text) - numTotal4;
                            numTotal3 = Convert.ToDecimal(txt1.Text);
                            tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                            txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                        }
                        else
                        {
                            decimal tot1 = Convert.ToDecimal(txt1.Text);
                            decimal tgst1 = Convert.ToDecimal(tgst) + 100;
                            decimal fgst = tgst1 / 100;
                            numTotal1 = tot1 / fgst;
                            numTotal4 = tot1 - numTotal1;
                            txt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                            gamt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                            numTotal2 = Convert.ToDecimal(txt1.Text) - numTotal4;
                            numTotal3 = Convert.ToDecimal(txt1.Text);
                            tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                            txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                        }

                        txt.Text = "0.00";
                        gamt.Text = "0.00";
                        tab5();
                        GrandTotalBil();
                    }
                }
            }
            if (dd2.SelectedValue != "")
            {
                decimal numTotal1 = 0;
                decimal numTotal2 = 0;
                decimal numTotal3 = 0;
                decimal numTotal4 = 0;
                //int selRowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
                Label tOt = (Label)row.FindControl("Label1");
                //TextBox qty = (TextBox)Gridview5.Rows[selRowIndex].FindControl("TextBox54");
                //TextBox unit = (TextBox)Gridview5.Rows[selRowIndex].FindControl("TextBox53");
                TextBox dis = (TextBox)row.FindControl("Txtdis");
                Label txt = (Label)Gridview5.FooterRow.FindControl("Label7");
                Label gmt = (Label)row.FindControl("Label10");
                Label txt1 = (Label)row.FindControl("Label5");
                CheckBox chk = (CheckBox)row.FindControl("CheckBox1");
                DropDownList dd = (DropDownList)row.FindControl("ddcukaioth");
                String tgst_1 = (row.FindControl("ddcukaioth") as DropDownList).SelectedItem.Value;
                Label tax1 = (Label)Gridview5.FooterRow.FindControl("Label4");
                Label tax1_cur = (Label)row.FindControl("Label8"); ;


                string tgst = string.Empty;
                DataTable sel_gst = new DataTable();
                sel_gst = Dbcon.Ora_Execute_table("select Ref_nama_cukai,Ref_kadar,Ref_kod_cukai From KW_Ref_Tetapan_cukai where Ref_kod_cukai='" + tgst_1 + "'");
                if (sel_gst.Rows.Count != 0)
                {
                    tgst = sel_gst.Rows[0]["Ref_kadar"].ToString();
                }
                else
                {
                    tgst = "0";
                }
                if (tgst_1 != "--- PILIH ---")
                {
                    if (chk.Checked == true)
                    {
                        dd.Enabled = true;
                        //dd.Attributes.Remove("style");
                        dd.Attributes.Remove("readonly");
                        decimal tot1 = Convert.ToDecimal(tOt.Text);
                        decimal tgst1 = Convert.ToDecimal(tgst) + 100;
                        decimal fgst = tgst1 / 100;
                        numTotal1 = tot1 / fgst;
                        numTotal4 = tot1 - numTotal1;
                        txt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                        gmt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                        numTotal2 = tot1 - numTotal4;
                        numTotal3 = Convert.ToDecimal(txt1.Text);
                        tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                        txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                        tab5();
                        GrandTotalBil2();
                    }
                    else
                    {
                        decimal tot1 = Convert.ToDecimal(tOt.Text);
                        int tgst1 = Convert.ToInt32(tgst);

                        numTotal1 = tot1 * tgst1 / 100;
                        txt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                        gmt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                        numTotal2 = Convert.ToDecimal(tOt.Text) + numTotal1;
                        txt1.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                        tab5();
                        GrandTotalBil2();
                    }
                }
                else
                {
                    if (dis.Text == "")
                    {
                        dis.Text = "0.00";
                    }

                    numTotal1 = Convert.ToDecimal(qty.Text) * Convert.ToDecimal(unit.Text);
                    if (chk.Checked == true)
                    {
                        dd.Enabled = false;
                        //dd.Attributes.Add("style", "pointer-events:none;");
                        dd.Attributes.Add("readonly", "readonly");

                        numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1_cur.Text);
                        numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1_cur.Text);
                        tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                        txt1.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                    }
                    else
                    {
                        numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1_cur.Text);
                        numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1_cur.Text);
                        tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                        txt1.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                        //numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text);
                        //numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text);
                        //tOt.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                        //txt1.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                    }

                    txt.Text = "0.00";
                    gmt.Text = "0.00";
                    tab5();
                    GrandTotalBil2();
                }
            }
        }
    }

    protected void Mohon_QtyChangedBil(object sender, EventArgs eventArgs)
    {
        decimal numTotal = 0;
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox qty = (TextBox)row.FindControl("TextBox54");
        TextBox unit = (TextBox)row.FindControl("TextBox53");
        Label jum = (Label)row.FindControl("Label1");
        Label jumI = (Label)row.FindControl("Label5");
        //int qty1 = Convert.ToInt32(qty.Text);
        decimal unit1 = Convert.ToDecimal(unit.Text);

        CheckBox chk1 = (CheckBox)row.FindControl("CheckBox1");
        DropDownList dd1 = (DropDownList)row.FindControl("ddcukaiinv");
        DropDownList dd2 = (DropDownList)row.FindControl("ddcukaioth");
        chk1.Checked = true;
        tab1();
        decimal qty1 = 1;
        if (qty.Text != "")
        {
            qty1 = Convert.ToInt32(qty.Text);
        }

        if (qty1.ToString() != "")
        {
            numTotal = qty1 * unit1;
            jum.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");
            jumI.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");
            Mohon_GrandTotalBil();
        }

        //code to go here when pri is changed


        if (chk1.Checked == true)
        {
            if (dd1.SelectedValue != "")
            {
                decimal numTotal1 = 0;
                decimal numTotal2 = 0;
                decimal numTotal3 = 0;
                decimal numTotal4 = 0;
                //int selRowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
                Label tOt = (Label)row.FindControl("Label1");
                //TextBox qty = (TextBox)Gridview5.Rows[selRowIndex].FindControl("TextBox54");
                //TextBox unit = (TextBox)Gridview5.Rows[selRowIndex].FindControl("TextBox53");
                TextBox dis = (TextBox)row.FindControl("Txtdis");
                Label txt = (Label)grdmohon.FooterRow.FindControl("Label4");
                Label gamt = (Label)row.FindControl("Label8");
                Label gmt = (Label)row.FindControl("Label10");
                Label txt1 = (Label)row.FindControl("Label5");
                CheckBox chk = (CheckBox)row.FindControl("CheckBox1");
                DropDownList dd = (DropDownList)row.FindControl("ddcukaioth");
                String tgst_1 = (row.FindControl("ddcukaiinv") as DropDownList).SelectedItem.Value;
                Label tax1 = (Label)grdmohon.FooterRow.FindControl("Label7");
                string gmt1;
                if (gmt.Text == "")
                {
                    gmt1 = "0.00";
                }
                else
                {
                    gmt1 = gmt.Text;
                }
                string tgst = string.Empty;
                DataTable sel_gst = new DataTable();
                sel_gst = Dbcon.Ora_Execute_table("select Ref_nama_cukai,Ref_kadar,Ref_kod_cukai From KW_Ref_Tetapan_cukai where Ref_kod_cukai='" + tgst_1 + "'");
                if (sel_gst.Rows.Count != 0)
                {
                    tgst = sel_gst.Rows[0]["Ref_kadar"].ToString();
                }
                else
                {
                    tgst = "0";
                }

                if (tgst_1 != "--- PILIH ---")
                {
                    if (chk.Checked == true)
                    {

                        decimal tot1 = Convert.ToDecimal(txt1.Text);
                        decimal tgst1 = Convert.ToDecimal(tgst) + 100;
                        decimal fgst = tgst1 / 100;
                        numTotal1 = tot1 / fgst;
                        numTotal4 = tot1 - numTotal1;
                        txt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                        gamt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                        numTotal2 = Convert.ToDecimal(txt1.Text) - numTotal4;
                        numTotal3 = Convert.ToDecimal(txt1.Text);
                        tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                        txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                        tab1();
                        dd.Enabled = true;
                        //dd.Attributes.Remove("style");
                        dd.Attributes.Remove("readonly");
                        Mohon_GrandTotalBil();
                    }
                    else
                    {
                        decimal tot1 = Convert.ToDecimal(tOt.Text) + Convert.ToDecimal(gmt1);
                        int tgst1 = Convert.ToInt32(tgst);

                        numTotal1 = tot1 * tgst1 / 100;
                        txt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                        gamt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                        numTotal2 = Convert.ToDecimal(tOt.Text) + numTotal1 + Convert.ToDecimal(gmt1);
                        txt1.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                        tab1();
                        Mohon_GrandTotalBil();
                    }
                }
                else
                {
                    if (qty.Text != "" && unit.Text != "")
                    {
                        if (dis.Text == "")
                        {
                            dis.Text = "0.00";
                        }
                        numTotal1 = Convert.ToDecimal(qty.Text) * Convert.ToDecimal(unit.Text);
                        if (chk.Checked == true)
                        {
                            decimal tot1 = Convert.ToDecimal(txt1.Text);
                            decimal tgst1 = Convert.ToDecimal(tgst) + 100;
                            decimal fgst = tgst1 / 100;
                            numTotal1 = tot1 / fgst;
                            numTotal4 = tot1 - numTotal1;
                            txt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                            gamt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                            numTotal2 = Convert.ToDecimal(txt1.Text) - numTotal4;
                            numTotal3 = Convert.ToDecimal(txt1.Text);
                            tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                            txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                        }
                        else
                        {
                            decimal tot1 = Convert.ToDecimal(txt1.Text);
                            decimal tgst1 = Convert.ToDecimal(tgst) + 100;
                            decimal fgst = tgst1 / 100;
                            numTotal1 = tot1 / fgst;
                            numTotal4 = tot1 - numTotal1;
                            txt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                            gamt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                            numTotal2 = Convert.ToDecimal(txt1.Text) - numTotal4;
                            numTotal3 = Convert.ToDecimal(txt1.Text);
                            tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                            txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                        }

                        txt.Text = "0.00";
                        gamt.Text = "0.00";
                        tab1();
                        Mohon_GrandTotalBil();
                    }
                }
            }
            if (dd2.SelectedValue != "")
            {
                decimal numTotal1 = 0;
                decimal numTotal2 = 0;
                decimal numTotal3 = 0;
                decimal numTotal4 = 0;
                //int selRowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
                Label tOt = (Label)row.FindControl("Label1");
                //TextBox qty = (TextBox)Gridview5.Rows[selRowIndex].FindControl("TextBox54");
                //TextBox unit = (TextBox)Gridview5.Rows[selRowIndex].FindControl("TextBox53");
                TextBox dis = (TextBox)row.FindControl("Txtdis");
                Label txt = (Label)grdmohon.FooterRow.FindControl("Label7");
                Label gmt = (Label)row.FindControl("Label10");
                Label txt1 = (Label)row.FindControl("Label5");
                CheckBox chk = (CheckBox)row.FindControl("CheckBox1");
                DropDownList dd = (DropDownList)row.FindControl("ddcukaioth");
                String tgst_1 = (row.FindControl("ddcukaioth") as DropDownList).SelectedItem.Value;
                Label tax1 = (Label)grdmohon.FooterRow.FindControl("Label4");
                Label tax1_cur = (Label)row.FindControl("Label8"); ;


                string tgst = string.Empty;
                DataTable sel_gst = new DataTable();
                sel_gst = Dbcon.Ora_Execute_table("select Ref_nama_cukai,Ref_kadar,Ref_kod_cukai From KW_Ref_Tetapan_cukai where Ref_kod_cukai='" + tgst_1 + "'");
                if (sel_gst.Rows.Count != 0)
                {
                    tgst = sel_gst.Rows[0]["Ref_kadar"].ToString();
                }
                else
                {
                    tgst = "0";
                }
                if (tgst_1 != "--- PILIH ---")
                {
                    if (chk.Checked == true)
                    {
                        dd.Enabled = true;
                        //dd.Attributes.Remove("style");
                        dd.Attributes.Remove("readonly");
                        decimal tot1 = Convert.ToDecimal(tOt.Text);
                        decimal tgst1 = Convert.ToDecimal(tgst) + 100;
                        decimal fgst = tgst1 / 100;
                        numTotal1 = tot1 / fgst;
                        numTotal4 = tot1 - numTotal1;
                        txt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                        gmt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                        numTotal2 = tot1 - numTotal4;
                        numTotal3 = Convert.ToDecimal(txt1.Text);
                        tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                        txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                        tab1();
                        Mohon_GrandTotalBil2();
                    }
                    else
                    {
                        decimal tot1 = Convert.ToDecimal(tOt.Text);
                        int tgst1 = Convert.ToInt32(tgst);

                        numTotal1 = tot1 * tgst1 / 100;
                        txt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                        gmt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                        numTotal2 = Convert.ToDecimal(tOt.Text) + numTotal1;
                        txt1.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                        tab1();
                        Mohon_GrandTotalBil2();
                    }
                }
                else
                {
                    if (dis.Text == "")
                    {
                        dis.Text = "0.00";
                    }

                    numTotal1 = Convert.ToDecimal(qty.Text) * Convert.ToDecimal(unit.Text);
                    if (chk.Checked == true)
                    {
                        dd.Enabled = false;
                        //dd.Attributes.Add("style", "pointer-events:none;");
                        dd.Attributes.Add("readonly", "readonly");

                        numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1_cur.Text);
                        numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1_cur.Text);
                        tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                        txt1.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                    }
                    else
                    {
                        numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1_cur.Text);
                        numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1_cur.Text);
                        tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                        txt1.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                        //numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text);
                        //numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text);
                        //tOt.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                        //txt1.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                    }

                    txt.Text = "0.00";
                    gmt.Text = "0.00";
                    tab1();
                    Mohon_GrandTotalBil2();
                }
            }
        }
    }

    protected void QtyChangedBil_kty(object sender, EventArgs eventArgs)
    {
        decimal numTotal = 0;
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox qty = (TextBox)row.FindControl("TextBox54");
        TextBox unit = (TextBox)row.FindControl("TextBox53");
        Label jum = (Label)row.FindControl("Label1");
        Label jumI = (Label)row.FindControl("Label5");
        //int qty1 = Convert.ToInt32(qty.Text);
        decimal unit1 = Convert.ToDecimal(unit.Text);

        CheckBox chk1 = (CheckBox)row.FindControl("CheckBox1");
        DropDownList dd1 = (DropDownList)row.FindControl("ddcukaiinv");
        DropDownList dd2 = (DropDownList)row.FindControl("ddcukaioth");

        tab5();
        decimal qty1 = 1;
        if (qty.Text != "")
        {
            qty1 = Convert.ToInt32(qty.Text);
        }

        if (qty1.ToString() != "")
        {
            numTotal = qty1 * unit1;
            jum.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");
            jumI.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");
            GrandTotalBil();


            //code to go here when pri is changed


            if (chk1.Checked == true)
            {
                if (dd1.SelectedValue != "")
                {
                    decimal numTotal1 = 0;
                    decimal numTotal2 = 0;
                    decimal numTotal3 = 0;
                    decimal numTotal4 = 0;
                    //int selRowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
                    Label tOt = (Label)row.FindControl("Label1");
                    //TextBox qty = (TextBox)Gridview5.Rows[selRowIndex].FindControl("TextBox54");
                    //TextBox unit = (TextBox)Gridview5.Rows[selRowIndex].FindControl("TextBox53");
                    TextBox dis = (TextBox)row.FindControl("Txtdis");
                    Label txt = (Label)Gridview5.FooterRow.FindControl("Label4");
                    Label gamt = (Label)row.FindControl("Label8");
                    Label gmt = (Label)row.FindControl("Label10");
                    Label txt1 = (Label)row.FindControl("Label5");
                    CheckBox chk = (CheckBox)row.FindControl("CheckBox1");
                    DropDownList dd = (DropDownList)row.FindControl("ddcukaioth");
                    String tgst_1 = (row.FindControl("ddcukaiinv") as DropDownList).SelectedItem.Value;
                    Label tax1 = (Label)Gridview5.FooterRow.FindControl("Label7");
                    string gmt1;
                    if (gmt.Text == "")
                    {
                        gmt1 = "0.00";
                    }
                    else
                    {
                        gmt1 = gmt.Text;
                    }
                    string tgst = string.Empty;
                    DataTable sel_gst = new DataTable();
                    sel_gst = Dbcon.Ora_Execute_table("select Ref_nama_cukai,Ref_kadar,Ref_kod_cukai From KW_Ref_Tetapan_cukai where Ref_kod_cukai='" + tgst_1 + "'");
                    if (sel_gst.Rows.Count != 0)
                    {
                        tgst = sel_gst.Rows[0]["Ref_kadar"].ToString();
                    }
                    else
                    {
                        tgst = "0";
                    }

                    if (tgst_1 != "--- PILIH ---")
                    {
                        if (chk.Checked == true)
                        {

                            decimal tot1 = Convert.ToDecimal(txt1.Text);
                            decimal tgst1 = Convert.ToDecimal(tgst) + 100;
                            decimal fgst = tgst1 / 100;
                            numTotal1 = tot1 / fgst;
                            numTotal4 = tot1 - numTotal1;
                            txt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                            gamt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                            numTotal2 = Convert.ToDecimal(txt1.Text) - numTotal4;
                            numTotal3 = Convert.ToDecimal(txt1.Text);
                            tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                            txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                            tab5();
                            dd.Enabled = true;
                            //dd.Attributes.Remove("style");
                            dd.Attributes.Remove("readonly");
                            GrandTotalBil();
                        }
                        else
                        {
                            decimal tot1 = Convert.ToDecimal(tOt.Text) + Convert.ToDecimal(gmt1);
                            int tgst1 = Convert.ToInt32(tgst);

                            numTotal1 = tot1 * tgst1 / 100;
                            txt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                            gamt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                            numTotal2 = Convert.ToDecimal(tOt.Text) + numTotal1 + Convert.ToDecimal(gmt1);
                            txt1.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                            tab5();
                            GrandTotalBil();
                        }
                    }
                    else
                    {
                        if (qty.Text != "" && unit.Text != "")
                        {
                            if (dis.Text == "")
                            {
                                dis.Text = "0.00";
                            }
                            numTotal1 = Convert.ToDecimal(qty.Text) * Convert.ToDecimal(unit.Text);
                            if (chk.Checked == true)
                            {
                                decimal tot1 = Convert.ToDecimal(txt1.Text);
                                decimal tgst1 = Convert.ToDecimal(tgst) + 100;
                                decimal fgst = tgst1 / 100;
                                numTotal1 = tot1 / fgst;
                                numTotal4 = tot1 - numTotal1;
                                txt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                                gamt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                                numTotal2 = Convert.ToDecimal(txt1.Text) - numTotal4;
                                numTotal3 = Convert.ToDecimal(txt1.Text);
                                tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                                txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                            }
                            else
                            {
                                decimal tot1 = Convert.ToDecimal(txt1.Text);
                                decimal tgst1 = Convert.ToDecimal(tgst) + 100;
                                decimal fgst = tgst1 / 100;
                                numTotal1 = tot1 / fgst;
                                numTotal4 = tot1 - numTotal1;
                                txt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                                gamt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                                numTotal2 = Convert.ToDecimal(txt1.Text) - numTotal4;
                                numTotal3 = Convert.ToDecimal(txt1.Text);
                                tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                                txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                            }

                            txt.Text = "0.00";
                            gamt.Text = "0.00";
                            tab5();
                            GrandTotalBil();
                        }
                    }
                }
                if (dd2.SelectedValue != "")
                {
                    decimal numTotal1 = 0;
                    decimal numTotal2 = 0;
                    decimal numTotal3 = 0;
                    decimal numTotal4 = 0;
                    //int selRowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
                    Label tOt = (Label)row.FindControl("Label1");
                    //TextBox qty = (TextBox)Gridview5.Rows[selRowIndex].FindControl("TextBox54");
                    //TextBox unit = (TextBox)Gridview5.Rows[selRowIndex].FindControl("TextBox53");
                    TextBox dis = (TextBox)row.FindControl("Txtdis");
                    Label txt = (Label)Gridview5.FooterRow.FindControl("Label7");
                    Label gmt = (Label)row.FindControl("Label10");
                    Label txt1 = (Label)row.FindControl("Label5");
                    CheckBox chk = (CheckBox)row.FindControl("CheckBox1");
                    DropDownList dd = (DropDownList)row.FindControl("ddcukaioth");
                    String tgst_1 = (row.FindControl("ddcukaioth") as DropDownList).SelectedItem.Value;
                    Label tax1 = (Label)Gridview5.FooterRow.FindControl("Label4");
                    Label tax1_cur = (Label)row.FindControl("Label8"); ;


                    string tgst = string.Empty;
                    DataTable sel_gst = new DataTable();
                    sel_gst = Dbcon.Ora_Execute_table("select Ref_nama_cukai,Ref_kadar,Ref_kod_cukai From KW_Ref_Tetapan_cukai where Ref_kod_cukai='" + tgst_1 + "'");
                    if (sel_gst.Rows.Count != 0)
                    {
                        tgst = sel_gst.Rows[0]["Ref_kadar"].ToString();
                    }
                    else
                    {
                        tgst = "0";
                    }
                    if (tgst_1 != "--- PILIH ---")
                    {
                        if (chk.Checked == true)
                        {
                            dd.Enabled = true;
                            //dd.Attributes.Remove("style");
                            dd.Attributes.Remove("readonly");
                            decimal tot1 = Convert.ToDecimal(tOt.Text);
                            decimal tgst1 = Convert.ToDecimal(tgst) + 100;
                            decimal fgst = tgst1 / 100;
                            numTotal1 = tot1 / fgst;
                            numTotal4 = tot1 - numTotal1;
                            txt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                            gmt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                            numTotal2 = tot1 - numTotal4;
                            numTotal3 = Convert.ToDecimal(txt1.Text);
                            tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                            txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                            tab5();
                            GrandTotalBil2();
                        }
                        else
                        {
                            decimal tot1 = Convert.ToDecimal(tOt.Text);
                            int tgst1 = Convert.ToInt32(tgst);

                            numTotal1 = tot1 * tgst1 / 100;
                            txt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                            gmt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                            numTotal2 = Convert.ToDecimal(tOt.Text) + numTotal1;
                            txt1.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                            tab5();
                            GrandTotalBil2();
                        }
                    }
                    else
                    {
                        if (dis.Text == "")
                        {
                            dis.Text = "0.00";
                        }

                        numTotal1 = Convert.ToDecimal(qty.Text) * Convert.ToDecimal(unit.Text);
                        if (chk.Checked == true)
                        {
                            dd.Enabled = false;
                            //dd.Attributes.Add("style", "pointer-events:none;");
                            dd.Attributes.Add("readonly", "readonly");

                            numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1_cur.Text);
                            numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1_cur.Text);
                            tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                            txt1.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                        }
                        else
                        {
                            numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1_cur.Text);
                            numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1_cur.Text);
                            tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                            txt1.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                            //numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text);
                            //numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text);
                            //tOt.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                            //txt1.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                        }

                        txt.Text = "0.00";
                        gmt.Text = "0.00";
                        tab5();
                        GrandTotalBil2();
                    }
                }
            }
        }
    }

    protected void Mohon_QtyChangedBil_kty(object sender, EventArgs eventArgs)
    {
        decimal numTotal = 0;
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox qty = (TextBox)row.FindControl("TextBox54");
        TextBox unit = (TextBox)row.FindControl("TextBox53");
        Label jum = (Label)row.FindControl("Label1");
        Label jumI = (Label)row.FindControl("Label5");
        //int qty1 = Convert.ToInt32(qty.Text);
        decimal unit1 = Convert.ToDecimal(unit.Text);
        
        CheckBox chk1 = (CheckBox)row.FindControl("CheckBox1");
        DropDownList dd1 = (DropDownList)row.FindControl("ddcukaiinv");
        DropDownList dd2 = (DropDownList)row.FindControl("ddcukaioth");
        chk1.Checked = true;
        tab1();
        decimal qty1 = 1;
        if (qty.Text != "")
        {
            qty1 = Convert.ToInt32(qty.Text);
        }

        if (qty1.ToString() != "")
        {
            numTotal = qty1 * unit1;
            jum.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");
            jumI.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");
            Mohon_GrandTotalBil();


            //code to go here when pri is changed


            if (chk1.Checked == true)
            {
                if (dd1.SelectedValue != "")
                {
                    decimal numTotal1 = 0;
                    decimal numTotal2 = 0;
                    decimal numTotal3 = 0;
                    decimal numTotal4 = 0;
                    //int selRowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
                    Label tOt = (Label)row.FindControl("Label1");
                    //TextBox qty = (TextBox)Gridview5.Rows[selRowIndex].FindControl("TextBox54");
                    //TextBox unit = (TextBox)Gridview5.Rows[selRowIndex].FindControl("TextBox53");
                    TextBox dis = (TextBox)row.FindControl("Txtdis");
                    Label txt = (Label)grdmohon.FooterRow.FindControl("Label4");
                    Label gamt = (Label)row.FindControl("Label8");
                    Label gmt = (Label)row.FindControl("Label10");
                    Label txt1 = (Label)row.FindControl("Label5");
                    CheckBox chk = (CheckBox)row.FindControl("CheckBox1");
                    DropDownList dd = (DropDownList)row.FindControl("ddcukaioth");
                    String tgst_1 = (row.FindControl("ddcukaiinv") as DropDownList).SelectedItem.Value;
                    Label tax1 = (Label)grdmohon.FooterRow.FindControl("Label7");
                    string gmt1;
                    if (gmt.Text == "")
                    {
                        gmt1 = "0.00";
                    }
                    else
                    {
                        gmt1 = gmt.Text;
                    }
                    string tgst = string.Empty;
                    DataTable sel_gst = new DataTable();
                    sel_gst = Dbcon.Ora_Execute_table("select Ref_nama_cukai,Ref_kadar,Ref_kod_cukai From KW_Ref_Tetapan_cukai where Ref_kod_cukai='" + tgst_1 + "'");
                    if (sel_gst.Rows.Count != 0)
                    {
                        tgst = sel_gst.Rows[0]["Ref_kadar"].ToString();
                    }
                    else
                    {
                        tgst = "0";
                    }

                    if (tgst_1 != "--- PILIH ---")
                    {
                        if (chk.Checked == true)
                        {

                            decimal tot1 = Convert.ToDecimal(txt1.Text);
                            decimal tgst1 = Convert.ToDecimal(tgst) + 100;
                            decimal fgst = tgst1 / 100;
                            numTotal1 = tot1 / fgst;
                            numTotal4 = tot1 - numTotal1;
                            txt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                            gamt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                            numTotal2 = Convert.ToDecimal(txt1.Text) - numTotal4;
                            numTotal3 = Convert.ToDecimal(txt1.Text);
                            tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                            txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                            tab1();
                            dd.Enabled = true;
                            //dd.Attributes.Remove("style");
                            dd.Attributes.Remove("readonly");
                            Mohon_GrandTotalBil();
                        }
                        else
                        {
                            decimal tot1 = Convert.ToDecimal(tOt.Text) + Convert.ToDecimal(gmt1);
                            int tgst1 = Convert.ToInt32(tgst);

                            numTotal1 = tot1 * tgst1 / 100;
                            txt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                            gamt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                            numTotal2 = Convert.ToDecimal(tOt.Text) + numTotal1 + Convert.ToDecimal(gmt1);
                            txt1.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                            tab1();
                            Mohon_GrandTotalBil();
                        }
                    }
                    else
                    {
                        if (qty.Text != "" && unit.Text != "")
                        {
                            if (dis.Text == "")
                            {
                                dis.Text = "0.00";
                            }
                            numTotal1 = Convert.ToDecimal(qty.Text) * Convert.ToDecimal(unit.Text);
                            if (chk.Checked == true)
                            {
                                decimal tot1 = Convert.ToDecimal(txt1.Text);
                                decimal tgst1 = Convert.ToDecimal(tgst) + 100;
                                decimal fgst = tgst1 / 100;
                                numTotal1 = tot1 / fgst;
                                numTotal4 = tot1 - numTotal1;
                                txt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                                gamt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                                numTotal2 = Convert.ToDecimal(txt1.Text) - numTotal4;
                                numTotal3 = Convert.ToDecimal(txt1.Text);
                                tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                                txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                            }
                            else
                            {
                                decimal tot1 = Convert.ToDecimal(txt1.Text);
                                decimal tgst1 = Convert.ToDecimal(tgst) + 100;
                                decimal fgst = tgst1 / 100;
                                numTotal1 = tot1 / fgst;
                                numTotal4 = tot1 - numTotal1;
                                txt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                                gamt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                                numTotal2 = Convert.ToDecimal(txt1.Text) - numTotal4;
                                numTotal3 = Convert.ToDecimal(txt1.Text);
                                tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                                txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                            }

                            txt.Text = "0.00";
                            gamt.Text = "0.00";
                            tab1();
                            Mohon_GrandTotalBil();
                        }
                    }
                }
                if (dd2.SelectedValue != "")
                {
                    decimal numTotal1 = 0;
                    decimal numTotal2 = 0;
                    decimal numTotal3 = 0;
                    decimal numTotal4 = 0;
                    //int selRowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
                    Label tOt = (Label)row.FindControl("Label1");
                    //TextBox qty = (TextBox)Gridview5.Rows[selRowIndex].FindControl("TextBox54");
                    //TextBox unit = (TextBox)Gridview5.Rows[selRowIndex].FindControl("TextBox53");
                    TextBox dis = (TextBox)row.FindControl("Txtdis");
                    Label txt = (Label)grdmohon.FooterRow.FindControl("Label7");
                    Label gmt = (Label)row.FindControl("Label10");
                    Label txt1 = (Label)row.FindControl("Label5");
                    CheckBox chk = (CheckBox)row.FindControl("CheckBox1");
                    DropDownList dd = (DropDownList)row.FindControl("ddcukaioth");
                    String tgst_1 = (row.FindControl("ddcukaioth") as DropDownList).SelectedItem.Value;
                    Label tax1 = (Label)grdmohon.FooterRow.FindControl("Label4");
                    Label tax1_cur = (Label)row.FindControl("Label8"); ;


                    string tgst = string.Empty;
                    DataTable sel_gst = new DataTable();
                    sel_gst = Dbcon.Ora_Execute_table("select Ref_nama_cukai,Ref_kadar,Ref_kod_cukai From KW_Ref_Tetapan_cukai where Ref_kod_cukai='" + tgst_1 + "'");
                    if (sel_gst.Rows.Count != 0)
                    {
                        tgst = sel_gst.Rows[0]["Ref_kadar"].ToString();
                    }
                    else
                    {
                        tgst = "0";
                    }
                    if (tgst_1 != "--- PILIH ---")
                    {
                        if (chk.Checked == true)
                        {
                            dd.Enabled = true;
                            //dd.Attributes.Remove("style");
                            dd.Attributes.Remove("readonly");
                            decimal tot1 = Convert.ToDecimal(tOt.Text);
                            decimal tgst1 = Convert.ToDecimal(tgst) + 100;
                            decimal fgst = tgst1 / 100;
                            numTotal1 = tot1 / fgst;
                            numTotal4 = tot1 - numTotal1;
                            txt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                            gmt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                            numTotal2 = tot1 - numTotal4;
                            numTotal3 = Convert.ToDecimal(txt1.Text);
                            tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                            txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                            tab1();
                            Mohon_GrandTotalBil2();
                        }
                        else
                        {
                            decimal tot1 = Convert.ToDecimal(tOt.Text);
                            int tgst1 = Convert.ToInt32(tgst);

                            numTotal1 = tot1 * tgst1 / 100;
                            txt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                            gmt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                            numTotal2 = Convert.ToDecimal(tOt.Text) + numTotal1;
                            txt1.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                            tab1();
                            Mohon_GrandTotalBil2();
                        }
                    }
                    else
                    {
                        if (dis.Text == "")
                        {
                            dis.Text = "0.00";
                        }

                        numTotal1 = Convert.ToDecimal(qty.Text) * Convert.ToDecimal(unit.Text);
                        if (chk.Checked == true)
                        {
                            dd.Enabled = false;
                            //dd.Attributes.Add("style", "pointer-events:none;");
                            dd.Attributes.Add("readonly", "readonly");

                            numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1_cur.Text);
                            numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1_cur.Text);
                            tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                            txt1.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                        }
                        else
                        {
                            numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1_cur.Text);
                            numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1_cur.Text);
                            tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                            txt1.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                            //numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text);
                            //numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text);
                            //tOt.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                            //txt1.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                        }

                        txt.Text = "0.00";
                        gmt.Text = "0.00";
                        tab1();
                        Mohon_GrandTotalBil2();
                    }
                }
            }
        }
    }
    protected void QtyChangedMohBil(object sender, EventArgs eventArgs)
    {
        decimal numTotal = 0;
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox qty = (TextBox)row.FindControl("TextBox56");
        TextBox unit = (TextBox)row.FindControl("TextBox55");
        Label jum = (Label)row.FindControl("Label1");
        Label jumI = (Label)row.FindControl("Label5");
        int qty1 = Convert.ToInt32(qty.Text);
        decimal unit1 = Convert.ToDecimal(unit.Text);

        CheckBox chk1 = (CheckBox)row.FindControl("CheckBox1");
        DropDownList dd1 = (DropDownList)row.FindControl("ddcukaiinv");
        DropDownList dd2 = (DropDownList)row.FindControl("ddcukaioth");

        tab5();
        if (qty1.ToString() != "")
        {
            numTotal = qty1 * unit1;
            jum.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");
            jumI.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");
            GrandTotalMohBil();
        }

        if (chk1.Checked == true)
        {
            if (dd1.SelectedValue != "")
            {
                decimal numTotal1 = 0;
                decimal numTotal2 = 0;
                decimal numTotal3 = 0;
                decimal numTotal4 = 0;
                //int selRowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
                Label tOt = (Label)row.FindControl("Label1");
                //TextBox qty = (TextBox)grdbilinv.Rows[selRowIndex].FindControl("TextBox56");
                //TextBox unit = (TextBox)grdbilinv.Rows[selRowIndex].FindControl("TextBox55");
                TextBox dis = (TextBox)row.FindControl("Txtdis");
                Label txt = (Label)grdbilinv.FooterRow.FindControl("Label4");
                Label gamt = (Label)row.FindControl("Label8");
                Label gmt = (Label)row.FindControl("Label10");
                Label txt1 = (Label)row.FindControl("Label5");
                CheckBox chk = (CheckBox)row.FindControl("CheckBox1");
                DropDownList dd = (DropDownList)row.FindControl("ddcukaioth");
                String tgst_1 = (row.FindControl("ddcukaiinv") as DropDownList).SelectedItem.Value;
                Label tax1 = (Label)grdbilinv.FooterRow.FindControl("Label7");

                string gmt1;
                if (gmt.Text == "")
                {
                    gmt1 = "0.00";
                }
                else
                {
                    gmt1 = gmt.Text;
                }
                string tgst = string.Empty;
                DataTable sel_gst = new DataTable();
                sel_gst = Dbcon.Ora_Execute_table("select Ref_nama_cukai,Ref_kadar,Ref_kod_cukai From KW_Ref_Tetapan_cukai where Ref_kod_cukai='" + tgst_1 + "'");
                if (sel_gst.Rows.Count != 0)
                {
                    tgst = sel_gst.Rows[0]["Ref_kadar"].ToString();
                }
                else
                {
                    tgst = "0";
                }

                if (tgst_1 != "--- PILIH ---")
                {
                    if (chk.Checked == true)
                    {

                        decimal tot1 = Convert.ToDecimal(txt1.Text);
                        decimal tgst1 = Convert.ToDecimal(tgst) + 100;
                        decimal fgst = tgst1 / 100;
                        numTotal1 = tot1 / fgst;
                        numTotal4 = tot1 - numTotal1;
                        txt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                        gamt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                        numTotal2 = Convert.ToDecimal(txt1.Text) - numTotal4;
                        numTotal3 = Convert.ToDecimal(txt1.Text);
                        tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                        txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                        tab5();
                        dd.Enabled = true;
                        //dd.Attributes.Remove("style");
                        dd.Attributes.Remove("readonly");
                        GrandTotalMohBil();
                    }
                    else
                    {
                        decimal tot1 = Convert.ToDecimal(tOt.Text) + Convert.ToDecimal(gmt1);
                        int tgst1 = Convert.ToInt32(tgst);

                        numTotal1 = tot1 * tgst1 / 100;
                        txt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                        gamt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                        numTotal2 = Convert.ToDecimal(tOt.Text) + numTotal1 + Convert.ToDecimal(gmt1);
                        txt1.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                        tab5();
                        GrandTotalMohBil();
                    }
                }
                else
                {
                    if (qty.ToString() != "" && unit.ToString() != "")
                    {
                        if (dis.Text == "")
                        {
                            dis.Text = "0.00";
                        }
                        numTotal1 = Convert.ToDecimal(qty.Text) * Convert.ToDecimal(unit.Text);
                        if (chk.Checked == true)
                        {

                            numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(gmt1);
                            numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text);
                            tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                            txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                        }
                        else
                        {
                            numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) + Convert.ToDecimal(gmt1);
                            numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text);
                            tOt.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                            txt1.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");

                        }

                        txt.Text = "0.00";
                        gamt.Text = "0.00";
                        tab5();
                        GrandTotalMohBil();
                    }
                }
            }
            if (dd2.SelectedValue != "")
            {
                decimal numTotal1 = 0;
                decimal numTotal2 = 0;
                decimal numTotal3 = 0;
                decimal numTotal4 = 0;
                //int selRowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
                Label tOt = (Label)row.FindControl("Label1");
                //TextBox qty = (TextBox)grdbilinv.Rows[selRowIndex].FindControl("TextBox56");
                //TextBox unit = (TextBox)grdbilinv.Rows[selRowIndex].FindControl("TextBox55");
                TextBox dis = (TextBox)row.FindControl("Txtdis");
                Label txt = (Label)grdbilinv.FooterRow.FindControl("Label7");
                Label gmt = (Label)row.FindControl("Label10");
                Label txt1 = (Label)row.FindControl("Label5");
                CheckBox chk = (CheckBox)row.FindControl("CheckBox1");
                DropDownList dd = (DropDownList)row.FindControl("ddcukaioth");
                String tgst_1 = (row.FindControl("ddcukaioth") as DropDownList).SelectedItem.Value;
                Label tax1 = (Label)grdbilinv.FooterRow.FindControl("Label4");
                Label tax1_cur = (Label)row.FindControl("Label8");


                string tgst = string.Empty;
                DataTable sel_gst = new DataTable();
                sel_gst = Dbcon.Ora_Execute_table("select Ref_nama_cukai,Ref_kadar,Ref_kod_cukai From KW_Ref_Tetapan_cukai where Ref_kod_cukai='" + tgst_1 + "'");
                if (sel_gst.Rows.Count != 0)
                {
                    tgst = sel_gst.Rows[0]["Ref_kadar"].ToString();
                }
                else
                {
                    tgst = "0";
                }
                if (tgst_1 != "--- PILIH ---")
                {
                    if (chk.Checked == true)
                    {
                        dd.Enabled = true;
                        //dd.Attributes.Remove("style");
                        dd.Attributes.Remove("readonly");
                        decimal tot1 = Convert.ToDecimal(tOt.Text);
                        decimal tgst1 = Convert.ToDecimal(tgst) + 100;
                        decimal fgst = tgst1 / 100;
                        numTotal1 = tot1 / fgst;
                        numTotal4 = tot1 - numTotal1;
                        txt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                        gmt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                        numTotal2 = tot1 - numTotal4;
                        numTotal3 = Convert.ToDecimal(txt1.Text);
                        tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                        txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                        tab5();
                        GrandTotalMohBil2();
                    }
                    else
                    {
                        decimal tot1 = Convert.ToDecimal(tOt.Text);
                        int tgst1 = Convert.ToInt32(tgst);

                        numTotal1 = tot1 * tgst1 / 100;
                        txt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                        gmt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                        numTotal2 = Convert.ToDecimal(tOt.Text) + numTotal1;
                        txt1.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                        tab5();
                        GrandTotalMohBil2();
                    }
                }
                else
                {
                    if (dis.Text == "")
                    {
                        dis.Text = "0.00";
                    }

                    numTotal1 = Convert.ToDecimal(qty.Text) * Convert.ToDecimal(unit.Text);
                    if (chk.Checked == true)
                    {
                        dd.Enabled = false;
                        //dd.Attributes.Add("style", "pointer-events:none;");
                        dd.Attributes.Add("readonly", "readonly");
                        numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1_cur.Text);
                        numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1_cur.Text);
                        tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                        txt1.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                    }
                    else
                    {
                        numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1_cur.Text);
                        numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1_cur.Text);
                        tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                        txt1.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                        //numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text);
                        //numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text);
                        //tOt.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                        //txt1.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                    }

                    txt.Text = "0.00";
                    gmt.Text = "0.00";
                    tab5();
                    GrandTotalMohBil2();
                }
            }
        }
        //code to go here when pri is changed
    }


    protected void QtyChangedMohBil_kty(object sender, EventArgs eventArgs)
    {
        decimal numTotal = 0;
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox qty = (TextBox)row.FindControl("TextBox56");
        TextBox unit = (TextBox)row.FindControl("TextBox55");
        Label jum = (Label)row.FindControl("Label1");
        Label jumI = (Label)row.FindControl("Label5");
        //int qty1 = Convert.ToInt32(qty.Text);
        decimal unit1 = Convert.ToDecimal(unit.Text);

        CheckBox chk1 = (CheckBox)row.FindControl("CheckBox1");
        DropDownList dd1 = (DropDownList)row.FindControl("ddcukaiinv");
        DropDownList dd2 = (DropDownList)row.FindControl("ddcukaioth");

        decimal qty1 = 1;
        if (qty.Text != "")
        {
            qty1 = Convert.ToInt32(qty.Text);
        }


        tab5();
        if (qty1.ToString() != "")
        {
            numTotal = qty1 * unit1;
            jum.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");
            jumI.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");
            GrandTotalMohBil();


            if (chk1.Checked == true)
            {
                if (dd1.SelectedValue != "")
                {
                    decimal numTotal1 = 0;
                    decimal numTotal2 = 0;
                    decimal numTotal3 = 0;
                    decimal numTotal4 = 0;
                    //int selRowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
                    Label tOt = (Label)row.FindControl("Label1");
                    //TextBox qty = (TextBox)grdbilinv.Rows[selRowIndex].FindControl("TextBox56");
                    //TextBox unit = (TextBox)grdbilinv.Rows[selRowIndex].FindControl("TextBox55");
                    TextBox dis = (TextBox)row.FindControl("Txtdis");
                    Label txt = (Label)grdbilinv.FooterRow.FindControl("Label4");
                    Label gamt = (Label)row.FindControl("Label8");
                    Label gmt = (Label)row.FindControl("Label10");
                    Label txt1 = (Label)row.FindControl("Label5");
                    CheckBox chk = (CheckBox)row.FindControl("CheckBox1");
                    DropDownList dd = (DropDownList)row.FindControl("ddcukaioth");
                    String tgst_1 = (row.FindControl("ddcukaiinv") as DropDownList).SelectedItem.Value;
                    Label tax1 = (Label)grdbilinv.FooterRow.FindControl("Label7");

                    string gmt1;
                    if (gmt.Text == "")
                    {
                        gmt1 = "0.00";
                    }
                    else
                    {
                        gmt1 = gmt.Text;
                    }
                    string tgst = string.Empty;
                    DataTable sel_gst = new DataTable();
                    sel_gst = Dbcon.Ora_Execute_table("select Ref_nama_cukai,Ref_kadar,Ref_kod_cukai From KW_Ref_Tetapan_cukai where Ref_kod_cukai='" + tgst_1 + "'");
                    if (sel_gst.Rows.Count != 0)
                    {
                        tgst = sel_gst.Rows[0]["Ref_kadar"].ToString();
                    }
                    else
                    {
                        tgst = "0";
                    }

                    if (tgst_1 != "--- PILIH ---")
                    {
                        if (chk.Checked == true)
                        {

                            decimal tot1 = Convert.ToDecimal(txt1.Text);
                            decimal tgst1 = Convert.ToDecimal(tgst) + 100;
                            decimal fgst = tgst1 / 100;
                            numTotal1 = tot1 / fgst;
                            numTotal4 = tot1 - numTotal1;
                            txt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                            gamt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                            numTotal2 = Convert.ToDecimal(txt1.Text) - numTotal4;
                            numTotal3 = Convert.ToDecimal(txt1.Text);
                            tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                            txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                            tab5();
                            dd.Enabled = true;
                            //dd.Attributes.Remove("style");
                            dd.Attributes.Remove("readonly");
                            GrandTotalMohBil();
                        }
                        else
                        {
                            decimal tot1 = Convert.ToDecimal(tOt.Text) + Convert.ToDecimal(gmt1);
                            int tgst1 = Convert.ToInt32(tgst);

                            numTotal1 = tot1 * tgst1 / 100;
                            txt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                            gamt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                            numTotal2 = Convert.ToDecimal(tOt.Text) + numTotal1 + Convert.ToDecimal(gmt1);
                            txt1.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                            tab5();
                            GrandTotalMohBil();
                        }
                    }
                    else
                    {
                        if (qty.ToString() != "" && unit.ToString() != "")
                        {
                            if (dis.Text == "")
                            {
                                dis.Text = "0.00";
                            }
                            numTotal1 = Convert.ToDecimal(qty.Text) * Convert.ToDecimal(unit.Text);
                            if (chk.Checked == true)
                            {

                                numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(gmt1);
                                numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text);
                                tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                                txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                            }
                            else
                            {
                                numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) + Convert.ToDecimal(gmt1);
                                numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text);
                                tOt.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                                txt1.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");

                            }

                            txt.Text = "0.00";
                            gamt.Text = "0.00";
                            tab5();
                            GrandTotalMohBil();
                        }
                    }
                }
                if (dd2.SelectedValue != "")
                {
                    decimal numTotal1 = 0;
                    decimal numTotal2 = 0;
                    decimal numTotal3 = 0;
                    decimal numTotal4 = 0;
                    //int selRowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
                    Label tOt = (Label)row.FindControl("Label1");
                    //TextBox qty = (TextBox)grdbilinv.Rows[selRowIndex].FindControl("TextBox56");
                    //TextBox unit = (TextBox)grdbilinv.Rows[selRowIndex].FindControl("TextBox55");
                    TextBox dis = (TextBox)row.FindControl("Txtdis");
                    Label txt = (Label)grdbilinv.FooterRow.FindControl("Label7");
                    Label gmt = (Label)row.FindControl("Label10");
                    Label txt1 = (Label)row.FindControl("Label5");
                    CheckBox chk = (CheckBox)row.FindControl("CheckBox1");
                    DropDownList dd = (DropDownList)row.FindControl("ddcukaioth");
                    String tgst_1 = (row.FindControl("ddcukaioth") as DropDownList).SelectedItem.Value;
                    Label tax1 = (Label)grdbilinv.FooterRow.FindControl("Label4");
                    Label tax1_cur = (Label)row.FindControl("Label8");


                    string tgst = string.Empty;
                    DataTable sel_gst = new DataTable();
                    sel_gst = Dbcon.Ora_Execute_table("select Ref_nama_cukai,Ref_kadar,Ref_kod_cukai From KW_Ref_Tetapan_cukai where Ref_kod_cukai='" + tgst_1 + "'");
                    if (sel_gst.Rows.Count != 0)
                    {
                        tgst = sel_gst.Rows[0]["Ref_kadar"].ToString();
                    }
                    else
                    {
                        tgst = "0";
                    }
                    if (tgst_1 != "--- PILIH ---")
                    {
                        if (chk.Checked == true)
                        {
                            dd.Enabled = true;
                            //dd.Attributes.Remove("style");
                            dd.Attributes.Remove("readonly");
                            decimal tot1 = Convert.ToDecimal(tOt.Text);
                            decimal tgst1 = Convert.ToDecimal(tgst) + 100;
                            decimal fgst = tgst1 / 100;
                            numTotal1 = tot1 / fgst;
                            numTotal4 = tot1 - numTotal1;
                            txt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                            gmt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                            numTotal2 = tot1 - numTotal4;
                            numTotal3 = Convert.ToDecimal(txt1.Text);
                            tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                            txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                            tab5();
                            GrandTotalMohBil2();
                        }
                        else
                        {
                            decimal tot1 = Convert.ToDecimal(tOt.Text);
                            int tgst1 = Convert.ToInt32(tgst);

                            numTotal1 = tot1 * tgst1 / 100;
                            txt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                            gmt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                            numTotal2 = Convert.ToDecimal(tOt.Text) + numTotal1;
                            txt1.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                            tab5();
                            GrandTotalMohBil2();
                        }
                    }
                    else
                    {
                        if (dis.Text == "")
                        {
                            dis.Text = "0.00";
                        }

                        numTotal1 = Convert.ToDecimal(qty.Text) * Convert.ToDecimal(unit.Text);
                        if (chk.Checked == true)
                        {
                            dd.Enabled = false;
                            //dd.Attributes.Add("style", "pointer-events:none;");
                            dd.Attributes.Add("readonly", "readonly");
                            numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1_cur.Text);
                            numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1_cur.Text);
                            tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                            txt1.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                        }
                        else
                        {
                            numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1_cur.Text);
                            numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1_cur.Text);
                            tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                            txt1.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                            //numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text);
                            //numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text);
                            //tOt.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                            //txt1.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                        }

                        txt.Text = "0.00";
                        gmt.Text = "0.00";
                        tab5();
                        GrandTotalMohBil2();
                    }
                }
            }
        }
        //code to go here when pri is changed
    }

    protected void QtyChangedInv(object sender, EventArgs eventArgs)
    {
        decimal numTotal = 0;
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox qty = (TextBox)row.FindControl("TextBox4");
        TextBox unit = (TextBox)row.FindControl("TextBox3");
        Label jum = (Label)row.FindControl("Label1");
        Label jumI = (Label)row.FindControl("Label5");
        int qty1 = Convert.ToInt32(qty.Text);
        decimal unit1 = Convert.ToDecimal(unit.Text);
        tab2();
        if (qty1.ToString() != "")
        {
            numTotal = qty1 * unit1;
            jum.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");
            jumI.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");

        }


        //code to go here when pri is changed
    }
    protected void QtyChanged1(object sender, EventArgs eventArgs)
    {
        decimal numTotal = 0;
        decimal GTotal = 0;
        decimal ITotal = 0;
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox qty = (TextBox)row.FindControl("Txtdis");

        Label jum = (Label)row.FindControl("Label5");
        Label jum1 = (Label)Gridview10.FooterRow.FindControl("Label6");
        Label jum2 = (Label)Gridview10.FooterRow.FindControl("Label3");

        tab3();
        if (qty.ToString() != "")
        {

            ITotal += (decimal)Convert.ToDecimal(qty.Text);
            qty.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
            jum.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
            GrandTotalcre();
        }




        //code to go here when pri is changed
    }

    protected void QtyChangedPV(object sender, EventArgs eventArgs)
    {
        decimal numTotal = 0;
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox qty = (TextBox)row.FindControl("txtpay");
        Label unit = (Label)row.FindControl("Label15");

        tab2();
        decimal chk = Convert.ToDecimal(qty.Text);
        decimal chk1 = Convert.ToDecimal(unit.Text);
        if (chk.ToString() != "")
        {
            if (chk1 < chk)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Please Enter correct the Amount');", true);
                qty.Text = "";
            }
        }


        //code to go here when pri is changed
    }



    // checkbox textchanged event


    protected void ChckedChangedBil(object sender, EventArgs e)
    {
        decimal numTotal1 = 0;
        int selRowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
        CheckBox cb = (CheckBox)Gridview5.Rows[selRowIndex].FindControl("CheckBox1");
        Label tOt = (Label)Gridview5.Rows[selRowIndex].FindControl("Label1");
        DropDownList db = (DropDownList)Gridview5.Rows[selRowIndex].FindControl("ddcukaioth");
        DropDownList db1 = (DropDownList)Gridview5.Rows[selRowIndex].FindControl("ddcukaiinv");

        tab5();
        if (cb.Checked)
        {
            //Perform your logic

            db.Enabled = false;
           db1.Enabled = true;

           // db.Attributes.Add("style", "pointer-events:none;");
            db.Attributes.Add("readonly", "readonly");
            //db1.Attributes.Remove("style");
            db1.Attributes.Remove("readonly");
        }
        else
        {
            db.Enabled = true;
            db1.Enabled = true;
            //db.Attributes.Remove("style");
            db.Attributes.Remove("readonly");
            //db1.Attributes.Remove("style");
            db1.Attributes.Remove("readonly");
        }

    }


    protected void ChckedChangedMohBil(object sender, EventArgs e)
    {
        decimal numTotal1 = 0;
        int selRowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
        CheckBox cb = (CheckBox)grdbilinv.Rows[selRowIndex].FindControl("CheckBox1");
        Label tOt = (Label)grdbilinv.Rows[selRowIndex].FindControl("Label1");
        DropDownList db = (DropDownList)grdbilinv.Rows[selRowIndex].FindControl("ddcukaioth");
        DropDownList db1 = (DropDownList)grdbilinv.Rows[selRowIndex].FindControl("ddcukaiinv");

        tab5();
        if (cb.Checked)
        {
            //Perform your logic

            db.Enabled = false;
            db1.Enabled = true;

            //db.Attributes.Add("style", "pointer-events:none;");
            db.Attributes.Add("readonly", "readonly");
            //db1.Attributes.Remove("style");
            db1.Attributes.Remove("readonly");
        }
        else
        {
            db.Enabled = true;
            db1.Enabled = true;
            //db.Attributes.Remove("style");
            db.Attributes.Remove("readonly");
            //db1.Attributes.Remove("style");
            db1.Attributes.Remove("readonly");
        }

    }

    protected void ChckedChanged1(object sender, EventArgs e)
    {
        decimal numTotal1 = 0;
        int selRowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
        CheckBox cb = (CheckBox)grdmohon.Rows[selRowIndex].FindControl("chkmoh");
        Label tOt = (Label)grdmohon.Rows[selRowIndex].FindControl("Label1");
        DropDownList txt = (DropDownList)grdmohon.Rows[selRowIndex].FindControl("ddcukaimoh");
        tab1();
        if (cb.Checked)
        {
            //Perform your logic

            txt.Enabled = true;

        }
        else
        {
            txt.Enabled = false;
        }

    }
    protected void QtyChangeddeb(object sender, EventArgs eventArgs)
    {
        var sum = 0;
        decimal numTotal = 0;
        decimal GTotal = 0;
        decimal ITotal = 0;
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox qty = (TextBox)row.FindControl("Txtdis");
        Label jum = (Label)row.FindControl("Label5");
        Label jum1 = (Label)Gridview10.FooterRow.FindControl("Label6");
        Label jum2 = (Label)Gridview10.FooterRow.FindControl("Label3");


        decimal unit1 = Convert.ToDecimal(qty.Text);
        tab4();
        if (unit1.ToString() != "")
        {
            ITotal += (decimal)Convert.ToDecimal(qty.Text);
            qty.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
            jum.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");

            GrandTotalded();
        }

        //code to go here when pri is changed
    }

    protected void ddgstdeb_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal numTotal1 = 0;
        decimal numTotal2 = 0;
        decimal numTotal3 = 0;
        decimal numTotal4 = 0;
        int selRowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
        TextBox tOt = (TextBox)Gridview11.Rows[selRowIndex].FindControl("Txtdis");
        Label txt = (Label)Gridview11.FooterRow.FindControl("Label3");
        Label gamt = (Label)Gridview11.Rows[selRowIndex].FindControl("Label8");
        Label gmt = (Label)Gridview11.Rows[selRowIndex].FindControl("Label10");

        Label txt1 = (Label)Gridview11.Rows[selRowIndex].FindControl("Label5");
        DropDownList dd = (DropDownList)Gridview11.Rows[selRowIndex].FindControl("ddgstdeboth");
        String tgst_1 = (Gridview11.Rows[selRowIndex].FindControl("ddgstdeb") as DropDownList).SelectedItem.Value;
        CheckBox chk = (CheckBox)Gridview11.Rows[selRowIndex].FindControl("Chkdeb");
        Label tax1 = (Label)Gridview11.FooterRow.FindControl("Label9");
        string tgst = string.Empty;
        DataTable sel_gst = new DataTable();
        sel_gst = Dbcon.Ora_Execute_table("select Ref_nama_cukai,Ref_kadar,Ref_kod_cukai From KW_Ref_Tetapan_cukai where Ref_kod_cukai='" + tgst_1 + "'");
        if (sel_gst.Rows.Count != 0)
        {
            tgst = sel_gst.Rows[0]["Ref_kadar"].ToString();
        }
        else
        {
            tgst = "0";
        }

        if (tgst_1 != "--- PILIH ---")
        {
            if (chk.Checked == true)
            {


                decimal tot1 = Convert.ToDecimal(txt1.Text);
                decimal tgst1 = Convert.ToDecimal(tgst) + 100;
                decimal fgst = tgst1 / 100;
                numTotal1 = tot1 / fgst;
                numTotal4 = tot1 - numTotal1;
                txt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                gamt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                numTotal2 = Convert.ToDecimal(txt1.Text) - numTotal4;
                numTotal3 = Convert.ToDecimal(txt1.Text);
                tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                tab4();

                dd.Enabled = true;
                //dd.Attributes.Remove("style");
                dd.Attributes.Remove("readonly");
                GrandTotalded();
            }
            else
            {
                decimal tot1 = Convert.ToDecimal(txt1.Text) + Convert.ToDecimal(gmt.Text);
                int tgst1 = Convert.ToInt32(tgst);

                numTotal1 = tot1 * tgst1 / 100;
                txt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                gamt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                numTotal2 = Convert.ToDecimal(txt1.Text) + numTotal1 + Convert.ToDecimal(gmt.Text);
                txt1.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                tab4();
                GrandTotalded();
            }
        }
        else
        {

            numTotal1 = Convert.ToDecimal(txt1.Text);
            if (chk.Checked == true)
            {
                numTotal2 = numTotal1 - Convert.ToDecimal(gmt.Text);
                numTotal3 = numTotal1;
                tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                //txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
            }
            else
            {
                numTotal2 = numTotal1 + Convert.ToDecimal(gmt.Text);
                numTotal3 = numTotal1;
                tOt.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                //txt1.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
            }

            txt.Text = "0.00";
            gamt.Text = "0.00";
            tab4();
            GrandTotalded();
        }
    }
    protected void ddgstdeboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal numTotal1 = 0;
        decimal numTotal2 = 0;
        decimal numTotal3 = 0;
        decimal numTotal4 = 0;
        int selRowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
        TextBox tOt = (TextBox)Gridview11.Rows[selRowIndex].FindControl("Txtdis");
        Label txt = (Label)Gridview11.FooterRow.FindControl("Label6");

        Label gmt = (Label)Gridview11.Rows[selRowIndex].FindControl("Label10");

        Label txt1 = (Label)Gridview11.Rows[selRowIndex].FindControl("Label5");
        String tgst_1 = (Gridview11.Rows[selRowIndex].FindControl("ddgstdeboth") as DropDownList).SelectedItem.Value;
        CheckBox chk = (CheckBox)Gridview11.Rows[selRowIndex].FindControl("Chkdeb");
        DropDownList dd = (DropDownList)Gridview11.Rows[selRowIndex].FindControl("ddgstdeboth");
        Label tax1 = (Label)Gridview11.FooterRow.FindControl("Label11");
        Label tax1_cur = (Label)Gridview11.Rows[selRowIndex].FindControl("Label8");

        string tgst = string.Empty;
        DataTable sel_gst = new DataTable();
        sel_gst = Dbcon.Ora_Execute_table("select Ref_nama_cukai,Ref_kadar,Ref_kod_cukai From KW_Ref_Tetapan_cukai where Ref_kod_cukai='" + tgst_1 + "'");
        if (sel_gst.Rows.Count != 0)
        {
            tgst = sel_gst.Rows[0]["Ref_kadar"].ToString();
        }
        else
        {
            tgst = "0";
        }

        if (tgst_1 != "--- PILIH ---")
        {
            if (chk.Checked == true)
            {
                dd.Enabled = true;
                //dd.Attributes.Remove("style");
                dd.Attributes.Remove("readonly");
                decimal tot1 = Convert.ToDecimal(tOt.Text);
                decimal tgst1 = Convert.ToDecimal(tgst) + 100;
                decimal fgst = tgst1 / 100;
                numTotal1 = tot1 / fgst;
                numTotal4 = tot1 - numTotal1;
                txt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                gmt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                numTotal2 = tot1 - numTotal4;
                numTotal3 = Convert.ToDecimal(txt1.Text);
                tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                tab4();
                GrandTotalded2();
            }
            else
            {
                decimal tot1 = Convert.ToDecimal(tOt.Text);
                int tgst1 = Convert.ToInt32(tgst);

                numTotal1 = tot1 * tgst1 / 100;
                txt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                gmt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                numTotal2 = Convert.ToDecimal(tOt.Text) + numTotal1;
                txt1.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                tab4();
                GrandTotalded2();
            }
        }
        else
        {



            numTotal1 = Convert.ToDecimal(txt1.Text);
            if (chk.Checked == true)
            {
                dd.Enabled = false;
                //dd.Attributes.Add("style", "pointer-events:none;");
                dd.Attributes.Add("readonly", "readonly");
                numTotal2 = numTotal1 - Convert.ToDecimal(tax1_cur.Text);
                numTotal3 = numTotal1 - Convert.ToDecimal(tax1_cur.Text);
                tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                //txt1.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
            }
            else
            {
                numTotal2 = numTotal1 - Convert.ToDecimal(tax1_cur.Text);
                numTotal3 = numTotal1 - Convert.ToDecimal(tax1_cur.Text);
                tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                //txt1.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
            }

            txt.Text = "0.00";
            gmt.Text = "0.00";
            tab4();
            GrandTotalded2();
        }
    }


    private void GrandTotalded()
    {
        decimal GTotal = 0;
        decimal ITotal = 0;
        decimal Gst = 0;
        for (int i = 0; i < Gridview11.Rows.Count; i++)
        {
            String total = (Gridview11.Rows[i].FindControl("Txtdis") as TextBox).Text;
            String Itotal = (Gridview11.Rows[i].FindControl("Label5") as Label).Text;
            String tgst = (Gridview11.Rows[i].FindControl("ddgstdeb") as DropDownList).SelectedItem.Value;
            String gamt = (Gridview11.Rows[i].FindControl("Label8") as Label).Text;
            Label jum = (Label)Gridview11.FooterRow.FindControl("Label3");

            Label gamt1 = (Label)Gridview11.FooterRow.FindControl("Label9");
            Label jumI = (Label)Gridview11.FooterRow.FindControl("Label6");
            string chk = (Gridview11.Rows[i].FindControl("Chkdeb") as CheckBox).Checked.ToString();
            GTotal += (decimal)Convert.ToDecimal(total);
            ITotal += (decimal)Convert.ToDecimal(Itotal);
            if (chk == "True")
            {
                decimal Gstval = Convert.ToDecimal(Itotal);
                if (tgst != "--- PILIH ---")
                {

                    decimal cuk = Convert.ToDecimal(gamt);
                    //decimal cuk2 = cuk / 100;
                    //decimal cuk1 = Gstval / cuk2 ;
                    //decimal cuk4 = Gstval - cuk1;
                    string ggst = cuk.ToString("C").Replace("RM", "").Replace("$", "");
                    Gst += (decimal)Convert.ToDecimal(ggst);
                    jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");

                    gamt1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    jumI.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                    TextBox1.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                }
                else
                {
                    gamt = "0";
                    decimal cuk = Convert.ToDecimal(gamt);

                    string ggst = cuk.ToString("C").Replace("RM", "").Replace("$", "");
                    Gst += (decimal)Convert.ToDecimal(ggst);
                    jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");

                    gamt1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    jumI.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                    TextBox1.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                }
            }
            else
            {
                jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");
                jumI.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                if (gamt == "")
                {
                    gamt = "0";
                }
                Gst += (decimal)Convert.ToDecimal(gamt);
                gamt1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                TextBox1.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
            }
        }

    }

    private void GrandTotalded2()
    {
        decimal GTotal = 0;
        decimal ITotal = 0;
        decimal Gst = 0;
        for (int i = 0; i < Gridview11.Rows.Count; i++)
        {
            String total = (Gridview11.Rows[i].FindControl("Txtdis") as TextBox).Text;
            String Itotal = (Gridview11.Rows[i].FindControl("Label5") as Label).Text;
            String tgst = (Gridview11.Rows[i].FindControl("ddgstdeboth") as DropDownList).SelectedItem.Value;
            String gmt = (Gridview11.Rows[i].FindControl("Label10") as Label).Text;
            Label jum = (Label)Gridview11.FooterRow.FindControl("Label3");
            Label jumI = (Label)Gridview11.FooterRow.FindControl("Label6");
            Label gmt1 = (Label)Gridview11.FooterRow.FindControl("Label11");
            string chk = (Gridview11.Rows[i].FindControl("Chkdeb") as CheckBox).Checked.ToString();
            GTotal += (decimal)Convert.ToDecimal(total);
            ITotal += (decimal)Convert.ToDecimal(Itotal);
            if (chk == "True")
            {
                decimal Gstval = Convert.ToDecimal(total) + Convert.ToDecimal(gmt1.Text);
                if (tgst != "--- PILIH ---")
                {


                    decimal cuk = Convert.ToDecimal(gmt);
                    //decimal cuk2 = cuk / 100;
                    //decimal cuk1 = Gstval / cuk2;
                    //decimal cuk4 = Gstval - cuk1;
                    string ggst = cuk.ToString("C").Replace("RM", "").Replace("$", "");
                    Gst += (decimal)Convert.ToDecimal(ggst);
                    jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");

                    gmt1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    jumI.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                    TextBox1.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                }
                else
                {
                    gmt = "0";
                    decimal cuk = Convert.ToDecimal(gmt);

                    string ggst = cuk.ToString("C").Replace("RM", "").Replace("$", "");
                    Gst += (decimal)Convert.ToDecimal(ggst);
                    jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");

                    gmt1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    jumI.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                    TextBox1.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                }
            }
            else
            {
                jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");
                jumI.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                if (gmt == "")
                {
                    gmt = "0";
                }
                Gst += (decimal)Convert.ToDecimal(gmt);
                gmt1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                TextBox1.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
            }
        }

    }

    protected void ChckedChangeddeb(object sender, EventArgs e)
    {

        int selRowIndex1 = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
        CheckBox cb1 = (CheckBox)Gridview11.Rows[selRowIndex1].FindControl("Chkdeb");
        Label tOt = (Label)Gridview11.Rows[selRowIndex1].FindControl("Label1");
        DropDownList db = (DropDownList)Gridview11.Rows[selRowIndex1].FindControl("ddgstdeboth");
        DropDownList db1 = (DropDownList)Gridview11.Rows[selRowIndex1].FindControl("ddgstdeb");
        tab4();
        if (cb1.Checked)
        {
            //Perform your logic

            db.Enabled = false;
            db1.Enabled = true;

            //db.Attributes.Add("style", "pointer-events:none;");
            db.Attributes.Add("readonly", "readonly");
            //db1.Attributes.Remove("style");
            db1.Attributes.Remove("readonly");
        }
        else
        {
            db.Enabled = true;
            db1.Enabled = true;
            //db.Attributes.Remove("style");
            db.Attributes.Remove("readonly");
            //db1.Attributes.Remove("style");
            db1.Attributes.Remove("readonly");
        }

    }

    protected void ChckedChangedcre(object sender, EventArgs e)
    {

        int selRowIndex1 = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
        CheckBox cb1 = (CheckBox)Gridview10.Rows[selRowIndex1].FindControl("Chkcre");
        Label tOt = (Label)Gridview10.Rows[selRowIndex1].FindControl("Label1");

        DropDownList db = (DropDownList)Gridview10.Rows[selRowIndex1].FindControl("ddkodgst");
        DropDownList db1 = (DropDownList)Gridview10.Rows[selRowIndex1].FindControl("ddkodgstoth");
        tab3();
        //if (cb1.Checked)
        //{
        //    //Perform your logic

        //    db.Enabled = true;
        //    db1.Enabled = false;
        //}
        //else
        //{
        //    db.Enabled = true;
        //    db1.Enabled = true;
        //}

        if (cb1.Checked)
        {
            //Perform your logic

            db1.Enabled = false;
            db.Enabled = true;

            //db1.Attributes.Add("style", "pointer-events:none;");
            db1.Attributes.Add("readonly", "readonly");
            //db.Attributes.Remove("style");
            db.Attributes.Remove("readonly");
        }
        else
        {
            db.Enabled = true;
            db1.Enabled = true;
            //db.Attributes.Remove("style");
            db.Attributes.Remove("readonly");
            //db1.Attributes.Remove("style");
            db1.Attributes.Remove("readonly");
        }

    }


    protected void Gridview12_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Find the DropDownList in the Row
            DropDownList ddlCountries = (e.Row.FindControl("ddkoddupcre") as DropDownList);
            ddlCountries.DataSource = GetData("select (kod_akaun + ' | '+ nama_akaun) nama_akaun,kod_akaun from KW_ref_carta_akaun where jenis_akaun_type != '1' and Status='A'");
            ddlCountries.DataTextField = "nama_akaun";
            ddlCountries.DataValueField = "kod_akaun";
            ddlCountries.DataBind();

            //Add Default Item in the DropDownList
            ddlCountries.Items.Insert(0, new ListItem("--- PILIH ---"));
            string country = (e.Row.FindControl("lblCountry") as Label).Text;
            if (country != "0")
            {
                ddlCountries.Items.FindByValue(country).Selected = true;
            }

            //Select the Country of Customer in DropDownList

            DropDownList ddlCountries1 = (e.Row.FindControl("ddgst") as DropDownList);
            ddlCountries1.DataSource = GetData("select Ref_nama_cukai,Ref_kod_cukai from KW_Ref_Tetapan_cukai where tc_jenis='PEM'");
            ddlCountries1.DataTextField = "Ref_nama_cukai";
            ddlCountries1.DataValueField = "Ref_kod_cukai";
            ddlCountries1.DataBind();

            ddlCountries1.Items.Insert(0, new ListItem("--- PILIH ---"));

            //Select the Country of Customer in DropDownList
            string country1 = (e.Row.FindControl("lblgst") as Label).Text;
            if (country1 != "0")
            {
                ddlCountries1.Items.FindByValue(country1).Selected = true;
            }

            DropDownList ddlCountries2 = (e.Row.FindControl("ddgstoth") as DropDownList);
            ddlCountries2.DataSource = GetData("select Ref_nama_cukai,Ref_kod_cukai from KW_Ref_Tetapan_cukai where tc_jenis='OTH'");
            ddlCountries2.DataTextField = "Ref_nama_cukai";
            ddlCountries2.DataValueField = "Ref_kod_cukai";
            ddlCountries2.DataBind();

            ddlCountries2.Items.Insert(0, new ListItem("--- PILIH ---"));

            //Select the Country of Customer in DropDownList
            string country2 = (e.Row.FindControl("lblgstoth") as Label).Text;
            if (country2 != "0")
            {
                ddlCountries2.Items.FindByValue(country2).Selected = true;
            }
        }

    }

    protected void Gridview14_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ////Find the DropDownList in the Row
            //DropDownList ddlCountries = (e.Row.FindControl("ddkoddupcre") as DropDownList);
            //ddlCountries.DataSource = GetData("select nama_akaun,kod_akaun from KW_ref_carta_akaun");
            //ddlCountries.DataTextField = "nama_akaun";
            //ddlCountries.DataValueField = "kod_akaun";
            //ddlCountries.DataBind();

            ////Add Default Item in the DropDownList
            //ddlCountries.Items.Insert(0, new ListItem("--- PILIH ---"));
            //string country = (e.Row.FindControl("lblCountry") as Label).Text;
            //if (country != "0")
            //{
            //    ddlCountries.Items.FindByValue(country).Selected = true;

            //}

            //DropDownList ddlCountries1 = (e.Row.FindControl("dddupgst") as DropDownList);
            //ddlCountries1.DataSource = GetData("select Ref_nama_cukai,Ref_kod_cukai from KW_Ref_Tetapan_cukai where tc_jenis='PEM'");
            //ddlCountries1.DataTextField = "Ref_nama_cukai";
            //ddlCountries1.DataValueField = "Ref_kod_cukai";
            //ddlCountries1.DataBind();

            //ddlCountries1.Items.Insert(0, new ListItem("--- PILIH ---"));

            ////Select the Country of Customer in DropDownList
            //string country1 = (e.Row.FindControl("lblgst") as Label).Text;
            //if (country1 != "0")
            //{
            //    ddlCountries1.Items.FindByValue(country1).Selected = true;
            //}



            //DropDownList ddlCountries2 = (e.Row.FindControl("dddupgstoth") as DropDownList);
            //ddlCountries2.DataSource = GetData("select Ref_nama_cukai,Ref_kod_cukai from KW_Ref_Tetapan_cukai where tc_jenis='OTH'");
            //ddlCountries2.DataTextField = "Ref_nama_cukai";
            //ddlCountries2.DataValueField = "Ref_kod_cukai";
            //ddlCountries2.DataBind();

            //ddlCountries2.Items.Insert(0, new ListItem("--- PILIH ---"));

            ////Select the Country of Customer in DropDownList
            //string country2 = (e.Row.FindControl("lblgstoth") as Label).Text;
            //if (country2 != "0")
            //{
            //    ddlCountries2.Items.FindByValue(country2).Selected = true;
            //}
            ////Select the Country of Customer in DropDownList

        }

    }

    private void GetUniqueId()
    {
        DataTable dt1 = Dbcon.Ora_Execute_table("select doc_type_cd,format as fmt,SUBSTRING(cur_format, 1, 10) as lfrmt1,SUBSTRING(cur_format, 11, 4) as lfrmt2,cur_format as cfmt from KW_Format_Nombor_rujukan where doc_type_cd='05' and Status='A'");
        if (dt1.Rows.Count != 0)
        {
            if (dt1.Rows[0]["cfmt"].ToString() == "")
            {
                txtnoruju.Text = dt1.Rows[0]["fmt"].ToString();
                txtnoruju.Attributes.Add("disabled", "disabled");
            }
            else
            {
                int seqno = Convert.ToInt32(dt1.Rows[0]["lfrmt2"].ToString());
                int newNumber = seqno + 1;
                uniqueId = newNumber.ToString(dt1.Rows[0]["lfrmt1"].ToString() + "0000");
                txtnoruju.Text = uniqueId;
                txtnoruju.Attributes.Add("disabled", "disabled");
            }

        }
        else
        {
            DataTable dt = Dbcon.Ora_Execute_table("select ISNULL(max(SUBSTRING(no_Rujukan,13,2000)),'0') from KW_Pembayaran_Credit_item");
            if (dt.Rows.Count > 0)
            {
                int seqno = Convert.ToInt32(dt.Rows[0][0].ToString());
                int newNumber = seqno + 1;
                uniqueId = newNumber.ToString("KTH-KRD" + "-" + DateTime.Now.ToString("yy") + "-" + "0000");
                txtnoruju.Text = uniqueId;
                txtnoruju.Attributes.Add("disabled", "disabled");
            }
            else
            {
                int newNumber = Convert.ToInt32(uniqueId) + 1;
                uniqueId = newNumber.ToString("KTH-KRD" + "-" + DateTime.Now.ToString("yy") + "-" + "0000");
                txtnoruju.Text = uniqueId;
                txtnoruju.Attributes.Add("disabled", "disabled");
            }
        }

    }

    private void GetMohon()
    {
        DataTable dt1 = Dbcon.Ora_Execute_table("select doc_type_cd,format as fmt,SUBSTRING(cur_format, 1, 12) as lfrmt1,SUBSTRING(cur_format, 13, 4) as lfrmt2,cur_format as cfmt from KW_Format_Nombor_rujukan where doc_type_cd='03' and Status='A'");
        if (dt1.Rows.Count != 0)
        {
            if (dt1.Rows[0]["cfmt"].ToString() == "")
            {
                txtnoper.Text = dt1.Rows[0]["fmt"].ToString();
                txtnoper.Attributes.Add("disabled", "disabled");
            }
            else
            {
                int seqno = Convert.ToInt32(dt1.Rows[0]["lfrmt2"].ToString());
                int newNumber = seqno + 1;
                uniqueId = newNumber.ToString(dt1.Rows[0]["lfrmt1"].ToString() + "0000");
                txtnoper.Text = uniqueId;
                txtnoper.Attributes.Add("disabled", "disabled");
            }

        }
        else
        {
            DataTable dt = Dbcon.Ora_Execute_table("select   ISNULL(max(SUBSTRING(no_permohonan,13,3000)),'0') from KW_Pembayaran_mohon");
            if (dt.Rows.Count > 0)
            {
                int seqno = Convert.ToInt32(dt.Rows[0][0].ToString());
                int newNumber = seqno + 1;
                uniqueId = newNumber.ToString("KS-MBY" + " - " + DateTime.Now.ToString("yyyy") + " - " + "0000");
                txtnoper.Text = uniqueId;
                txtnoper.Attributes.Add("disabled", "disabled");

            }
            else
            {
                int newNumber = Convert.ToInt32(uniqueId) + 1;
                uniqueId = newNumber.ToString("KS-MBY" + " - " + DateTime.Now.ToString("yyyy") + " - " + "0000");
                txtnoper.Text = uniqueId;
                txtnoper.Attributes.Add("disabled", "disabled");
            }
        }


    }



    private void GetUniqueIddeb()
    {
        DataTable dt1 = Dbcon.Ora_Execute_table("select doc_type_cd,format as fmt,SUBSTRING(cur_format, 1, 10) as lfrmt1,SUBSTRING(cur_format, 11, 4) as lfrmt2,cur_format as cfmt from KW_Format_Nombor_rujukan where doc_type_cd='06' and Status='A'");
        if (dt1.Rows.Count != 0)
        {
            if (dt1.Rows[0]["cfmt"].ToString() == "")
            {
                txtnoruj2.Text = dt1.Rows[0]["fmt"].ToString();
                txtnoruj2.Attributes.Add("disabled", "disabled");
            }
            else
            {
                int seqno = Convert.ToInt32(dt1.Rows[0]["lfrmt2"].ToString());
                int newNumber = seqno + 1;
                uniqueId = newNumber.ToString(dt1.Rows[0]["lfrmt1"].ToString() + "0000");
                txtnoruj2.Text = uniqueId;
                txtnoruj2.Attributes.Add("disabled", "disabled");
            }

        }
        else
        {
            DataTable dt = Dbcon.Ora_Execute_table("select   ISNULL(max(SUBSTRING(no_Rujukan,13,2000)),'0') from KW_Pembayaran_Dedit_item");
            if (dt.Rows.Count > 0)
            {
                int seqno = Convert.ToInt32(dt.Rows[0][0].ToString());
                int newNumber = seqno + 1;
                uniqueId = newNumber.ToString("KTH-DBT" + " - " + DateTime.Now.ToString("yy") + " - " + "0000");
                txtnoruj2.Text = uniqueId;
                txtnoruj2.Attributes.Add("disabled", "disabled");

            }
            else
            {
                int newNumber = Convert.ToInt32(uniqueId) + 1;
                uniqueId = newNumber.ToString("KTH-DBT" + " - " + DateTime.Now.ToString("yy") + " - " + "0000");
                txtnoruj2.Text = uniqueId;
                txtnoruj2.Attributes.Add("disabled", "disabled");
            }
        }


    }

    protected void Btnkoff_Click(object sender, EventArgs e)
    {



        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dt5 = new DataTable();
        DataTable dt6 = new DataTable();
        DataTable dt7 = new DataTable();
        DateTime tDate = DateTime.Parse(txttcinvois.Text);
        foreach (GridViewRow g1 in Gridview10.Rows)
        {

            CheckBox chk = (CheckBox)g1.FindControl("chkcreoff");
            string inv = (g1.FindControl("lblinv") as Label).Text;
            string tarikh = (g1.FindControl("lbltar") as Label).Text;
            DateTime tDate1 = DateTime.Parse(tarikh.ToString());
            string sya = (g1.FindControl("lblsya") as Label).Text;
            string total = (g1.FindControl("lbljum") as Label).Text;
            string dd = (g1.FindControl("txtkoffkre") as TextBox).Text;
            userid = Session["New"].ToString();
            if (chk.Checked == true)
            {
                if (dd != "")
                {
                    dt = Dbcon.Ora_Execute_table("select nama_pelanggan from KW_Pembayaran_Credit where no_Rujukan='" + txtnoruju.Text + "'");
                    dt1 = Dbcon.Ora_Execute_table("insert into KW_Pembayaran_Credit_reduce values('" + ddakaun.SelectedItem.Value + "','" + ddpela2.SelectedItem.Value + "','" + txtnoruju.Text + "','" + inv + "','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "','" + tDate1.ToString("yyyy/MM/dd hh:mm:ss") + "','" + ddpro1.SelectedItem.Value + "','" + total + "','" + dd + "','','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','','')");
                    DataTable dt_upd_format = new DataTable();
                    dt_upd_format = Dbcon.Ora_Execute_table("update KW_Format_Nombor_rujukan set cur_format='" + txtnoruju.Text + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where doc_type_cd='05' and Status = 'A'");
                    dt2 = Dbcon.Ora_Execute_table("update KW_Pembayaran_invois set jumlah=jumlah - " + dd + " , Overall= Overall - " + dd + "  ,upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where no_permohonan='" + inv + "'");
                    dt4 = Dbcon.Ora_Execute_table("update KW_Pembayaran_Credit set jumlah=jumlah - " + dd + " ,upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "'  where no_Rujukan='" + txtnoruju.Text + "'");
                    dt5 = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + dt.Rows[0][0].ToString() + "','" + dd + "','','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "','" + txtnoruju.Text + "','" + inv + "','" + dt.Rows[0][0].ToString() + "','','')");
                    dt6 = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + ddpela2.SelectedItem.Value + "','','" + dd + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "','" + txtnoruju.Text + "','" + inv + "','" + ddpela2.SelectedItem.Value + "','','')");

                    th2.Visible = false;
                    th1.Visible = true;
                    reset();
                    tab3();
                    fr2.Visible = false;
                    fr1.Visible = true;

                }
                else
                {
                }
            }
            else
            {
            }
        }



    }
    protected void Btnkoffclose_Click(object sender, EventArgs e)
    {
        th2.Visible = false;
        th1.Visible = true;
        reset();
        tab3();
    }


    protected void Btnkoffclose1_Click(object sender, EventArgs e)
    {
        fr1.Visible = true;
        fr2.Visible = false;
        reset();
        tab4();

    }

    protected void Gridview7_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Find the DropDownList in the Row
            DropDownList ddlCountries = (e.Row.FindControl("ddkoddeb") as DropDownList);
            ddlCountries.DataSource = GetData("select (kod_akaun + ' | '+ nama_akaun) nama_akaun,kod_akaun from KW_ref_carta_akaun where jenis_akaun_type != '1' and Status='A'");
            ddlCountries.DataTextField = "nama_akaun";
            ddlCountries.DataValueField = "kod_akaun";
            ddlCountries.DataBind();

            //Add Default Item in the DropDownList
            ddlCountries.Items.Insert(0, new ListItem("--- PILIH ---"));

            //Select the Country of Customer in DropDownList


            DropDownList ddlCountries1 = (e.Row.FindControl("ddgstdeb") as DropDownList);
            ddlCountries1.DataSource = GetData("select Ref_nama_cukai,Ref_kadar,Ref_kod_cukai from KW_Ref_Tetapan_cukai  where tc_jenis='PEM'");
            ddlCountries1.DataTextField = "Ref_nama_cukai";
            ddlCountries1.DataValueField = "Ref_kod_cukai";
            ddlCountries1.DataBind();

            //Add Default Item in the DropDownList
            ddlCountries1.Items.Insert(0, new ListItem("--- PILIH ---"));

            DropDownList ddlCountries2 = (e.Row.FindControl("ddgstdeboth") as DropDownList);
            ddlCountries2.DataSource = GetData("select Ref_nama_cukai,Ref_kadar,Ref_kod_cukai from KW_Ref_Tetapan_cukai where tc_jenis='OTH'");
            ddlCountries2.DataTextField = "Ref_nama_cukai";
            ddlCountries2.DataValueField = "Ref_kod_cukai";
            ddlCountries2.DataBind();

            //Add Default Item in the DropDownList
            ddlCountries2.Items.Insert(0, new ListItem("--- PILIH ---"));

        }

    }

    protected void Gridview9_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Find the DropDownList in the Row
            DropDownList ddlCountries = (e.Row.FindControl("ddkodcre") as DropDownList);
            ddlCountries.DataSource = GetData("select (kod_akaun + ' | '+ nama_akaun) nama_akaun,kod_akaun from KW_ref_carta_akaun where jenis_akaun_type != '1' and Status='A'");
            ddlCountries.DataTextField = "nama_akaun";
            ddlCountries.DataValueField = "kod_akaun";
            ddlCountries.DataBind();

            //Add Default Item in the DropDownList
            ddlCountries.Items.Insert(0, new ListItem("--- PILIH ---"));

            //Select the Country of Customer in DropDownList

            //Add Default Item in the DropDownList

            DropDownList ddlCountries1 = (e.Row.FindControl("ddkodgst") as DropDownList);
            ddlCountries1.DataSource = GetData("select Ref_nama_cukai,Ref_kod_cukai from KW_Ref_Tetapan_cukai where tc_jenis='PEM'");
            ddlCountries1.DataTextField = "Ref_nama_cukai";
            ddlCountries1.DataValueField = "Ref_kod_cukai";
            ddlCountries1.DataBind();

            //Add Default Item in the DropDownList
            ddlCountries1.Items.Insert(0, new ListItem("--- PILIH ---"));

            DropDownList ddlCountries2 = (e.Row.FindControl("ddkodgstoth") as DropDownList);
            ddlCountries2.DataSource = GetData("select Ref_nama_cukai,Ref_kod_cukai from KW_Ref_Tetapan_cukai where tc_jenis='OTH'");
            ddlCountries2.DataTextField = "Ref_nama_cukai";
            ddlCountries2.DataValueField = "Ref_kod_cukai";
            ddlCountries2.DataBind();

            //Add Default Item in the DropDownList
            ddlCountries2.Items.Insert(0, new ListItem("--- PILIH ---"));


        }

    }




    protected void grdbilinv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Find the DropDownList in the Row
            DropDownList ddlCountries = (e.Row.FindControl("ddkodBil") as DropDownList);
            ddlCountries.DataSource = GetData("select (kod_akaun + ' | '+ nama_akaun) nama_akaun,kod_akaun from KW_ref_carta_akaun where jenis_akaun_type != '1' and Status='A'");
            ddlCountries.DataTextField = "nama_akaun";
            ddlCountries.DataValueField = "kod_akaun";
            ddlCountries.DataBind();

            //Add Default Item in the DropDownList
            ddlCountries.Items.Insert(0, new ListItem("--- PILIH ---"));

            //Select the Country of Customer in DropDownList


            DropDownList ddlCountries1 = (e.Row.FindControl("ddprobil") as DropDownList);
            ddlCountries1.DataSource = GetData("select Ref_Projek_code,Ref_Projek_name from  KW_Ref_Projek");
            ddlCountries1.DataTextField = "Ref_Projek_name";
            ddlCountries1.DataValueField = "Ref_Projek_code";
            ddlCountries1.DataBind();

            //Add Default Item in the DropDownList
            ddlCountries1.Items.Insert(0, new ListItem("--- PILIH ---"));


            DropDownList ddlCountries2 = (e.Row.FindControl("ddcukaiinv") as DropDownList);
            ddlCountries2.DataSource = GetData("select Ref_nama_cukai,Ref_kod_cukai from KW_Ref_Tetapan_cukai where tc_jenis='PEM'");
            ddlCountries2.DataTextField = "Ref_nama_cukai";
            ddlCountries2.DataValueField = "Ref_kod_cukai";
            ddlCountries2.DataBind();

            //Add Default Item in the DropDownList
            ddlCountries2.Items.Insert(0, new ListItem("--- PILIH ---"));

            DropDownList ddlCountries3 = (e.Row.FindControl("ddcukaioth") as DropDownList);
            ddlCountries3.DataSource = GetData("select Ref_nama_cukai,Ref_kod_cukai from KW_Ref_Tetapan_cukai where tc_jenis='OTH'");
            ddlCountries3.DataTextField = "Ref_nama_cukai";
            ddlCountries3.DataValueField = "Ref_kod_cukai";
            ddlCountries3.DataBind();

            //Add Default Item in the DropDownList
            ddlCountries3.Items.Insert(0, new ListItem("--- PILIH ---"));


            DropDownList db1 = (DropDownList)e.Row.FindControl("ddkodKat");
            if (db1.SelectedValue == "SEMUA COA")
            {

                DropDownList ddlCountries5 = (e.Row.FindControl("ddkodcre") as DropDownList);


                ddlCountries5.DataSource = GetData("select (kod_akaun + ' | '+ nama_akaun) nama_akaun,kod_akaun from KW_ref_carta_akaun where jenis_akaun_type != '1' and Status='A'");
                ddlCountries5.DataTextField = "nama_akaun";
                ddlCountries5.DataValueField = "kod_akaun";
                ddlCountries5.DataBind();

                //Add Default Item in the DropDownList
                ddlCountries5.Items.Insert(0, new ListItem("--- PILIH ---"));
            }
            if (db1.SelectedValue == "PEMBEKAL")
            {
                DropDownList ddlCountries6 = (e.Row.FindControl("ddkodcre") as DropDownList);
                ddlCountries6.DataSource = GetData("select Ref_kod_akaun,Ref_nama_syarikat from KW_Ref_Pembekal ");
                ddlCountries6.DataTextField = "Ref_nama_syarikat";
                ddlCountries6.DataValueField = "Ref_kod_akaun";
                ddlCountries6.DataBind();

                //Add Default Item in the DropDownList
                ddlCountries6.Items.Insert(0, new ListItem("--- PILIH ---"));

            }
        }
    }

    protected void grdbilinvdub_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Find the DropDownList in the Row
            ////DropDownList ddlCountries = (e.Row.FindControl("ddkodBil") as DropDownList);
            ////ddlCountries.DataSource = GetData("select nama_akaun,kod_akaun from KW_ref_carta_akaun");
            ////ddlCountries.DataTextField = "nama_akaun";
            ////ddlCountries.DataValueField = "kod_akaun";
            ////ddlCountries.DataBind();

            ////Add Default Item in the DropDownList
            //string country = (e.Row.FindControl("lbldeb") as Label).Text;
            //if (country != "0")
            //{
            //    ddlCountries.Items.FindByValue(country).Selected = true;

            //}

            //Select the Country of Customer in DropDownList


            //DropDownList ddlCountries1 = (e.Row.FindControl("ddprobildub") as DropDownList);
            //ddlCountries1.DataSource = GetData("select Ref_Projek_code,Ref_Projek_name from  KW_Ref_Projek");
            //ddlCountries1.DataTextField = "Ref_Projek_name";
            //ddlCountries1.DataValueField = "Ref_Projek_code";
            //ddlCountries1.DataBind();

            ////Add Default Item in the DropDownList
            //string country1 = (e.Row.FindControl("lblprodub") as Label).Text;
            //if (country1 != "0")
            //{
            //    ddlCountries1.Items.FindByValue(country1).Selected = true;

            //}


            //DropDownList ddlkat = (e.Row.FindControl("ddkodKat") as DropDownList);
            //if (ddlkat.SelectedItem.Text == "PEMBEKAL")
            //{

            //    DropDownList ddlCountries2 = (e.Row.FindControl("ddkodcre") as DropDownList);
            //    ddlCountries2.DataSource = GetData("select Ref_kod_akaun, Ref_nama_syarikat from KW_Ref_Pembekal");
            //    ddlCountries2.DataTextField = "Ref_nama_syarikat";
            //    ddlCountries2.DataValueField = "Ref_kod_akaun";
            //    ddlCountries2.DataBind();

            //    //Add Default Item in the DropDownList
            //    string country2 = (e.Row.FindControl("lblcre") as Label).Text;
            //    if (country2 != "0")
            //    {
            //        ddlCountries2.Items.FindByValue(country2).Selected = true;

            //    }
            //}
            //else
            //{ 

            //DropDownList ddlCountries3 = (e.Row.FindControl("ddkodcre") as DropDownList);
            //ddlCountries3.DataSource = GetData(" select nama_akaun, kod_akaun   from KW_Ref_Carta_Akaun");
            //ddlCountries3.DataTextField = "nama_akaun";
            //ddlCountries3.DataValueField = "kod_akaun";
            //ddlCountries3.DataBind();

            ////Add Default Item in the DropDownList
            //string country3 = (e.Row.FindControl("lblcre") as Label).Text;
            //if (country3 != "0")
            //{
            //    ddlCountries3.Items.FindByValue(country3).Selected = true;

            //}

            //}
        }
    }


    protected void grdinvois_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Find the DropDownList in the Row
            DropDownList ddlCountries = (e.Row.FindControl("ddkodInv") as DropDownList);
            ddlCountries.DataSource = GetData("select (kod_akaun + ' | '+ nama_akaun) nama_akaun,kod_akaun from KW_ref_carta_akaun where jenis_akaun_type != '1' and Status='A'");
            ddlCountries.DataTextField = "nama_akaun";
            ddlCountries.DataValueField = "kod_akaun";
            ddlCountries.DataBind();

            //Add Default Item in the DropDownList
            ddlCountries.Items.Insert(0, new ListItem("--- PILIH ---"));

            //Select the Country of Customer in DropDownList


            DropDownList ddlCountries1 = (e.Row.FindControl("ddcukaiinv") as DropDownList);
            ddlCountries1.DataSource = GetData("select Ref_nama_cukai,Ref_kadar from KW_Ref_Tetapan_cukai where tc_jenis='PEM'");
            ddlCountries1.DataTextField = "Ref_nama_cukai";
            ddlCountries1.DataValueField = "Ref_kadar";
            ddlCountries1.DataBind();

            //Add Default Item in the DropDownList
            ddlCountries1.Items.Insert(0, new ListItem("--- PILIH ---"));

            DropDownList ddlCountries2 = (e.Row.FindControl("ddcukaioth") as DropDownList);
            ddlCountries2.DataSource = GetData("select Ref_nama_cukai,Ref_kadar from KW_Ref_Tetapan_cukai where tc_jenis='OTH'");
            ddlCountries2.DataTextField = "Ref_nama_cukai";
            ddlCountries2.DataValueField = "Ref_kadar";
            ddlCountries2.DataBind();

            //Add Default Item in the DropDownList
            ddlCountries2.Items.Insert(0, new ListItem("--- PILIH ---"));


        }
    }


    protected void Gridview5_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Find the DropDownList in the Row
            DropDownList ddlCountries = (e.Row.FindControl("ddkodbil") as DropDownList);
            ddlCountries.DataSource = GetData("select (kod_akaun + ' | '+ nama_akaun) nama_akaun,kod_akaun from KW_ref_carta_akaun where jenis_akaun_type != '1' and Status='A'");
            ddlCountries.DataTextField = "nama_akaun";
            ddlCountries.DataValueField = "kod_akaun";
            ddlCountries.DataBind();

            //Add Default Item in the DropDownList
            ddlCountries.Items.Insert(0, new ListItem("--- PILIH ---"));



            //Select the Country of Customer in DropDownList


            DropDownList ddlCountries1 = (e.Row.FindControl("ddcukaiinv") as DropDownList);
            ddlCountries1.DataSource = GetData("select Ref_nama_cukai,Ref_kod_cukai from KW_Ref_Tetapan_cukai where tc_jenis='PEM' ");
            ddlCountries1.DataTextField = "Ref_nama_cukai";
            ddlCountries1.DataValueField = "Ref_kod_cukai";
            ddlCountries1.DataBind();

            //Add Default Item in the DropDownList
            ddlCountries1.Items.Insert(0, new ListItem("--- PILIH ---"));

            DropDownList ddlCountries2 = (e.Row.FindControl("ddcukaioth") as DropDownList);
            ddlCountries2.DataSource = GetData("select Ref_nama_cukai,Ref_kod_cukai from KW_Ref_Tetapan_cukai where tc_jenis='OTH'");
            ddlCountries2.DataTextField = "Ref_nama_cukai";
            ddlCountries2.DataValueField = "Ref_kod_cukai";
            ddlCountries2.DataBind();

            //Add Default Item in the DropDownList
            ddlCountries2.Items.Insert(0, new ListItem("--- PILIH ---"));

            //Find the DropDownList in the Row
            DropDownList ddlCountries3 = (e.Row.FindControl("ddkodproj") as DropDownList);
            ddlCountries3.DataSource = GetData("select Ref_Projek_code,Ref_Projek_name from KW_Ref_Projek where Status='A'");
            ddlCountries3.DataTextField = "Ref_Projek_name";
            ddlCountries3.DataValueField = "Ref_Projek_code";
            ddlCountries3.DataBind();

            //Add Default Item in the DropDownList
            ddlCountries3.Items.Insert(0, new ListItem("--- PILIH ---"));
        }
    }

    protected void Gridview3_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // //Find the DropDownList in the Row
            // DropDownList ddlCountries = (e.Row.FindControl("ddkoddup") as DropDownList);
            // ddlCountries.DataSource = GetData("select nama_akaun,kod_akaun from KW_ref_carta_akaun");
            // ddlCountries.DataTextField = "nama_akaun";
            // ddlCountries.DataValueField = "kod_akaun";
            // ddlCountries.DataBind();

            // //Add Default Item in the DropDownList
            // ddlCountries.Items.Insert(0, new ListItem("--- PILIH ---"));
            // string country = (e.Row.FindControl("lblCountry") as Label).Text;
            //if (country != "0")
            //{
            //    ddlCountries.Items.FindByValue(country).Selected = true;
            //}

            ////Select the Country of Customer in DropDownList

            //DropDownList ddlCountries1 = (e.Row.FindControl("ddtax") as DropDownList);
            //ddlCountries1.DataSource = GetData("select Ref_nama_cukai,Ref_kod_cukai from KW_Ref_Tetapan_cukai where tc_jenis='PEM' ");
            //ddlCountries1.DataTextField = "Ref_nama_cukai";
            //ddlCountries1.DataValueField = "Ref_kod_cukai";
            //ddlCountries1.DataBind();

            //ddlCountries1.Items.Insert(0, new ListItem("--- PILIH ---"));

            ////Select the Country of Customer in DropDownList
            //string country1 = (e.Row.FindControl("lblgst") as Label).Text;
            //if (country1 != "0")
            //{
            //    ddlCountries1.Items.FindByValue(country1).Selected = true;
            //}

            //DropDownList ddlCountries2 = (e.Row.FindControl("ddtaxoth") as DropDownList);
            //ddlCountries2.DataSource = GetData("select Ref_nama_cukai,Ref_kod_cukai from KW_Ref_Tetapan_cukai where tc_jenis='OTH'");
            //ddlCountries2.DataTextField = "Ref_nama_cukai";
            //ddlCountries2.DataValueField = "Ref_kod_cukai";
            //ddlCountries2.DataBind();

            //ddlCountries2.Items.Insert(0, new ListItem("--- PILIH ---"));

            ////Select the Country of Customer in DropDownList
            //string country2 = (e.Row.FindControl("lblgstoth") as Label).Text;
            //if (country2 != "0")
            //{
            //    ddlCountries2.Items.FindByValue(country2).Selected = true;
            //}

        }

    }

    protected void grdvie5dup_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ////Find the DropDownList in the Row
            //DropDownList ddlCountries = (e.Row.FindControl("ddkoddup") as DropDownList);
            //ddlCountries.DataSource = GetData("select nama_akaun,kod_akaun from KW_ref_carta_akaun");
            //ddlCountries.DataTextField = "nama_akaun";
            //ddlCountries.DataValueField = "kod_akaun";
            //ddlCountries.DataBind();

            ////Add Default Item in the DropDownList
            //ddlCountries.Items.Insert(0, new ListItem("--- PILIH ---"));
            //string country = (e.Row.FindControl("lblCountry") as Label).Text;
            //if (country != "0")
            //{
            //    ddlCountries.Items.FindByValue(country).Selected = true;
            //}

            ////Select the Country of Customer in DropDownList

            //DropDownList ddlCountries1 = (e.Row.FindControl("ddtax") as DropDownList);
            //ddlCountries1.DataSource = GetData("select Ref_nama_cukai,Ref_kod_cukai from KW_Ref_Tetapan_cukai where tc_jenis='PEM'");
            //ddlCountries1.DataTextField = "Ref_nama_cukai";
            //ddlCountries1.DataValueField = "Ref_kod_cukai";
            //ddlCountries1.DataBind();

            //ddlCountries1.Items.Insert(0, new ListItem("--- PILIH ---"));

            ////Select the Country of Customer in DropDownList
            //string country1 = (e.Row.FindControl("lblgst") as Label).Text;
            //if (country1 != "0")
            //{
            //    ddlCountries1.Items.FindByValue(country1).Selected = true;
            //}

            //DropDownList ddlCountries2 = (e.Row.FindControl("ddtaxoth") as DropDownList);
            //ddlCountries2.DataSource = GetData("select Ref_nama_cukai,Ref_kod_cukai from KW_Ref_Tetapan_cukai where tc_jenis='OTH'");
            //ddlCountries2.DataTextField = "Ref_nama_cukai";
            //ddlCountries2.DataValueField = "Ref_kod_cukai";
            //ddlCountries2.DataBind();

            //ddlCountries2.Items.Insert(0, new ListItem("--- PILIH ---"));

            ////Select the Country of Customer in DropDownList
            //string country2 = (e.Row.FindControl("lblgstoth") as Label).Text;
            //if (country2 != "0")
            //{
            //    ddlCountries2.Items.FindByValue(country2).Selected = true;
            //}

        }

    }
    protected void grdmohon_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //CheckBox tax = (CheckBox)e.Row.FindControl("CheckBox1");

            //grdmohon.Columns[9].a;

            DropDownList dd_bayar = (e.Row.FindControl("TextBox11") as DropDownList);
            if (DropDownList1.SelectedValue == "01")
            {
                dd_bayar.DataSource = GetData("SELECT stf_name as val1,stf_staff_no as val2 FROM hr_staff_profile s1 left join hr_post_his s2 on s2.pos_staff_no=s1.stf_staff_no and pos_end_dt >='"+ DateTime.Now.ToString("yyyy-MM-dd") +"'");
            }
            else if (DropDownList1.SelectedValue == "02")
            {
                dd_bayar.DataSource = GetData("select Ref_nama_syarikat as val1,Ref_no_syarikat as val2 from KW_Ref_Pembekal where Status='A'");
            }
            else if (DropDownList1.SelectedValue == "03")
            {
                dd_bayar.DataSource = GetData("select Ref_nama_syarikat as val1,Ref_no_syarikat as val2 from KW_Ref_Pelanggan where Status='A'");
            }
            dd_bayar.DataTextField = "val1";
            dd_bayar.DataValueField = "val2";
            dd_bayar.DataBind();

            //Add Default Item in the DropDownList
            dd_bayar.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            DropDownList ddlCountries2 = (e.Row.FindControl("ddcukaiinv") as DropDownList);
            ddlCountries2.DataSource = GetData("select Ref_nama_cukai,Ref_kod_cukai from KW_Ref_Tetapan_cukai where tc_jenis='PEM'");
            ddlCountries2.DataTextField = "Ref_nama_cukai";
            ddlCountries2.DataValueField = "Ref_kod_cukai";
            ddlCountries2.DataBind();

            //Add Default Item in the DropDownList
            ddlCountries2.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            DropDownList ddbank = (e.Row.FindControl("dd_bank") as DropDownList);
            ddbank.DataSource = GetData("select Bank_Code,Bank_Name from Ref_Nama_Bank");
            ddbank.DataTextField = "Bank_Name";
            ddbank.DataValueField = "Bank_Code";
            ddbank.DataBind();

            //Add Default Item in the DropDownList
            ddbank.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            DropDownList ddprojek = (e.Row.FindControl("dd_projek") as DropDownList);
            ddprojek.DataSource = GetData("select Ref_Projek_code,Ref_Projek_name from KW_Ref_Projek");
            ddprojek.DataTextField = "Ref_Projek_name";
            ddprojek.DataValueField = "Ref_Projek_code";
            ddprojek.DataBind();

            //Add Default Item in the DropDownList
            ddprojek.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
    }

    protected void gridmohdup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            DataTable get_query = new DataTable();
            System.Web.UI.WebControls.Label dd_bayar = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label22");
            System.Web.UI.WebControls.Label ddbajet_cd1 = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label25");
            Label kepada = (e.Row.FindControl("kepada") as Label);

            if (dd_bayar.Text == "01")
            {
                get_query = Dbcon.Ora_Execute_table("SELECT stf_name as val1,stf_staff_no as val2,stf_kod_akaun val3 FROM hr_staff_profile s1 left join hr_post_his s2 on s2.pos_staff_no=s1.stf_staff_no and pos_end_dt >='" + DateTime.Now.ToString("yyyy-MM-dd") + "' where stf_staff_no='" + kepada.Text + "'");
            }
            else if (dd_bayar.Text == "02")
            {
                get_query = Dbcon.Ora_Execute_table("select Ref_nama_syarikat as val1,Ref_no_syarikat as val2,ISNULL(ref_kod_akaun,'') val3 from KW_Ref_Pembekal where Status='A' and Ref_no_syarikat='" + kepada.Text + "'");
            }
            else if (dd_bayar.Text == "03")
            {
                get_query = Dbcon.Ora_Execute_table("select Ref_nama_syarikat as val1,Ref_no_syarikat as val2,ISNULL(ref_kod_akaun,'') val3 from KW_Ref_Pelanggan where Status='A' and Ref_no_syarikat='" + kepada.Text + "'");
            }
            else if (dd_bayar.Text == "03")
            {
                get_query = Dbcon.Ora_Execute_table("select Ref_nama_syarikat as val1,Ref_no_syarikat as val2,ISNULL(ref_kod_akaun,'') val3 from KW_Ref_Pelanggan where Status='A' and Ref_no_syarikat='" + kepada.Text + "'");
            }
            else if (dd_bayar.Text == "11")
            {
                get_query = DBCon.Ora_Execute_table("select KK_Skrin_name as val1,KK_Skrin_id as val2 from KK_PID_Skrin where KK_Skrin_id='" + kepada.Text + "' and Status='A'");
            }
            kepada.Text = get_query.Rows[0]["val1"].ToString();

            DropDownList ddbajet = (e.Row.FindControl("dd_bajet") as DropDownList);
            ddbajet.DataSource = GetData("select kod_bajet,nama_bajet from KW_Ref_kod_bajet inner join KW_Ref_Bajet s1 on s1.Ref_kod_bajet=kod_bajet where s1.Status='A' and '"+ str_sdt1 + "' between Ref_tk_mula and Ref_tk_akhir");
            ddbajet.DataTextField = "nama_bajet";
            ddbajet.DataValueField = "kod_bajet";
            ddbajet.DataBind();

            //Add Default Item in the DropDownList
            ddbajet.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            ddbajet.SelectedValue = ddbajet_cd1.Text;
        }
    }
    protected void ddcbaya_SelectedIndexChanged(object sender, EventArgs e)
    {
        // if (ddcbaya.SelectedItem.Text == "Cek" || ddcbaya.SelectedItem.Text == "Bank Draft")
        // {

        //     string com = "select REf_nama_bank,ref_no_akaun from KW_Ref_Akaun_bank";
        //     SqlDataAdapter adpt = new SqlDataAdapter(com, con);
        //     DataTable dt = new DataTable();
        //     adpt.Fill(dt);
        //     ddabaya.DataSource = dt;
        //     ddabaya.DataTextField = "REf_nama_bank";
        //     ddabaya.DataValueField = "ref_no_akaun";
        //     ddabaya.DataBind();
        //     ddabaya.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        //    ddabaya.Attributes.Remove("disabled");
        //    txtnoruj.Attributes.Remove("disabled");
        //}
        // else
        // {
        //    ddabaya.Attributes.Add("disabled", "disabled");
        //    txtnoruj.Attributes.Add("disabled", "disabled");
        //}

    }

    protected void ddbkepada_SelectedIndexChanged(object sender, EventArgs e)
    {
        shw_grd_info();
    }

    void shw_grd_info()
    {
        if (DropDownList1.SelectedValue != "")
        {
            if (DropDownList1.SelectedValue != "01")
            {
                Button19.Visible = true;
                //mak_dok_shw.Visible = true;
            }
            else
            {
                //mak_dok_shw.Visible = false;
                Button19.Visible = false;
            }

            foreach (GridViewRow row in grdmohon.Rows)
            {
                DropDownList innerDropdown = (DropDownList)row.FindControl("TextBox11");

                if (DropDownList1.SelectedValue == "01")
                {
                    innerDropdown.DataSource = GetData("SELECT stf_name as val1,stf_staff_no as val2 FROM hr_staff_profile s1 left join hr_post_his s2 on s2.pos_staff_no=s1.stf_staff_no and pos_end_dt >='" + DateTime.Now.ToString("yyyy-MM-dd") + "'");
                }
                else if (DropDownList1.SelectedValue == "02")
                {
                    innerDropdown.DataSource = GetData("select Ref_nama_syarikat as val1,Ref_no_syarikat as val2 from KW_Ref_Pembekal where Status='A'");
                }
                else if (DropDownList1.SelectedValue == "03")
                {
                    innerDropdown.DataSource = GetData("select Ref_nama_syarikat as val1,Ref_no_syarikat as val2 from KW_Ref_Pelanggan where Status='A'");
                }
                innerDropdown.DataTextField = "val1";
                innerDropdown.DataValueField = "val2";
                innerDropdown.DataBind();

                //Add Default Item in the DropDownList
                innerDropdown.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            }
        }
    }

    void bind_jurnal()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = jurnal_qry;
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            jurnal_no.DataSource = dt;
            jurnal_no.DataTextField = "val2";
            jurnal_no.DataValueField = "val1";
            jurnal_no.DataBind();
            jurnal_no.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void jurnal_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddbkepada.SelectedItem.Text != "--- PILIH ---")
        {
            string[] split_val1 = jurnal_no.SelectedValue.Split('-');
            DataTable get_jumlah = new DataTable();
            TextBox keterangan = (TextBox)grdmohon.Rows[0].FindControl("TextBox13");
            TextBox jumlah = (TextBox)grdmohon.Rows[0].FindControl("TextBox14");
            if (ddbkepada.SelectedItem.Text == "KAKITANGAN")
            {
                jumlah.Text = "0.00";
                keterangan.Text = "";
                show_mohon();
            }
            else if (ddbkepada.SelectedItem.Text == "KWSP")
            {

                get_jumlah = Dbcon.Ora_Execute_table("select GL_invois_no,sum(KW_Debit_amt) as kwsp from KW_General_Ledger where kod_akaun IN ('12.06','12.07') and GL_invois_no='" + jurnal_no.SelectedValue + "' group by GL_invois_no");
                jumlah.Text = double.Parse(get_jumlah.Rows[0]["kwsp"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                keterangan.Text = "Caruman KWSP Bagi Bulan " + split_val1[3].ToString();
                show_mohon();

            }
            else if (ddbkepada.SelectedItem.Text == "PERKESO")
            {
                get_jumlah = Dbcon.Ora_Execute_table("select GL_invois_no,sum(KW_Debit_amt) as perkeso from KW_General_Ledger where kod_akaun IN ('12.08','12.09') and GL_invois_no='" + jurnal_no.SelectedValue + "' group by GL_invois_no");
                jumlah.Text = double.Parse(get_jumlah.Rows[0]["perkeso"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                keterangan.Text = "Caruman PERKESO Bagi Bulan " + split_val1[3].ToString();
                show_mohon();
            }
            else if (ddbkepada.SelectedItem.Text == "LHDN (PCB)")
            {
                get_jumlah = Dbcon.Ora_Execute_table("select GL_invois_no,sum(KW_Debit_amt) as pcb from KW_General_Ledger where kod_akaun IN ('12.12') and GL_invois_no='" + jurnal_no.SelectedValue + "' group by GL_invois_no");
                jumlah.Text = double.Parse(get_jumlah.Rows[0]["pcb"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                keterangan.Text = "Caruman LHDN (PCB) Bagi Bulan " + split_val1[3].ToString();
                show_mohon();
            }
            else if (ddbkepada.SelectedItem.Text == "LHDN (CP 38)")
            {
                get_jumlah = Dbcon.Ora_Execute_table("select GL_invois_no,sum(KW_Debit_amt) as cp_38 from KW_General_Ledger where kod_akaun IN ('12.13') and GL_invois_no='" + jurnal_no.SelectedValue + "' group by GL_invois_no");
                jumlah.Text = double.Parse(get_jumlah.Rows[0]["cp_38"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                keterangan.Text = "Caruman LHDN (CP 38) Bagi Bulan " + split_val1[3].ToString();
                show_mohon();
            }
            else if (ddbkepada.SelectedItem.Text == "GAJI KAKITANGAN")
            {
                get_jumlah = Dbcon.Ora_Execute_table("select GL_invois_no,sum(KW_kredit_amt) as gk_amt from KW_General_Ledger where kod_akaun='04.26' and GL_invois_no='" + jurnal_no.SelectedValue + "' group by GL_invois_no");
                jumlah.Text = double.Parse(get_jumlah.Rows[0]["gk_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                keterangan.Text = "Caruman GAJI KAKITANGAN Bagi Bulan " + split_val1[3].ToString();
                show_mohon();
            }
            else if (ddbkepada.SelectedItem.Text == "ANGKASA")
            {
                get_jumlah = Dbcon.Ora_Execute_table("select GL_invois_no,sum(KW_kredit_amt) as angkasa from KW_General_Ledger where kod_akaun='04.20' and GL_invois_no='" + jurnal_no.SelectedValue + "' group by GL_invois_no");
                jumlah.Text = double.Parse(get_jumlah.Rows[0]["angkasa"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                keterangan.Text = "Caruman ANGKASA Bagi Bulan " + split_val1[3].ToString();
                show_mohon();
            }
            else if (ddbkepada.SelectedItem.Text == "KOOP / Bank")
            {
                get_jumlah = Dbcon.Ora_Execute_table("select GL_invois_no,sum(KW_kredit_amt) as koop from KW_General_Ledger where kod_akaun='04.22' and GL_invois_no='" + jurnal_no.SelectedValue + "' group by GL_invois_no");
                jumlah.Text = double.Parse(get_jumlah.Rows[0]["koop"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                keterangan.Text = "Caruman KOOP / Bank Bagi Bulan " + split_val1[3].ToString();
                show_mohon();
            }
            else if (ddbkepada.SelectedItem.Text == "TABUNG HAJI")
            {
                get_jumlah = Dbcon.Ora_Execute_table("select GL_invois_no,sum(KW_kredit_amt) as tabung from KW_General_Ledger where kod_akaun='04.21' and GL_invois_no='" + jurnal_no.SelectedValue + "' group by GL_invois_no");
                jumlah.Text = double.Parse(get_jumlah.Rows[0]["tabung"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                keterangan.Text = "Caruman TABUNG HAJI Bagi Bulan " + split_val1[3].ToString();
                show_mohon();
            }
            else if (ddbkepada.SelectedItem.Text == "PEMBEKAL (PENGURUSAN ASET)")
            {
                string[] ss_sp1 = jurnal_no.SelectedValue.Split('_');
                get_jumlah = Dbcon.Ora_Execute_table("select pur_po_no,sum(pur_asset_tot_amt) po_amt from ast_purchase where pur_approve_sts_cd='01' and pur_mp_sts='Proses' and pur_po_no='" + ss_sp1[0].ToString() + "' and ISNULL(pur_del_ind,'') != 'D' and pur_supplier_id='" + ss_sp1[1].ToString() + "' group by pur_po_no");
                jumlah.Text = double.Parse(get_jumlah.Rows[0]["po_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                keterangan.Text = jurnal_no.SelectedItem.Text;

                DataTable ddokdicno = new DataTable();
                ddokdicno = Dbcon.Ora_Execute_table("select sup_id,sup_name from ast_supplier where sup_id='" + ss_sp1[1].ToString() + "'");
                if (ddokdicno.Rows.Count != 0)
                {
                    txtname.Text = ddokdicno.Rows[0]["sup_name"].ToString();
                }
                show_mohon();
            }
            else if (ddbkepada.SelectedItem.Text == "Keahlian")
            {
                get_jumlah = Dbcon.Ora_Execute_table("select KE_EFT_File_no,Sum(isnull(cast(div_debit_amt as float),0)) div_debit_amt,FORMAT(div_eft_dt,'yyyy/MM/dd'),'DIVIDEN BAGI TAHUN " + DateTime.Now.Year + " '  AS KET   from  mem_divident where KE_EFT_Flag='1' and KE_EFT_File_no='" + jurnal_no.SelectedValue + "' GROUP BY KE_EFT_File_no,FORMAT(div_eft_dt,'yyyy/MM/dd') ");
                jumlah.Text = double.Parse(get_jumlah.Rows[0]["div_debit_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                keterangan.Text = get_jumlah.Rows[0]["KET"].ToString();
                show_mohon();
            }
            else
            {
                txticno.Attributes.Add("disabled", "disabled");
                btncarian.Attributes.Add("disabled", "disabled");
                txtname.Attributes.Remove("disabled");
                txtbname.Attributes.Remove("disabled");
                txtbno.Attributes.Remove("disabled");
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Sila Pilih Bayar Kepada');", true);
        }
    }


    void show_mohon()
    {
        decimal numTotal = 0;

        TextBox qty = (TextBox)grdmohon.Rows[0].FindControl("TextBox14");
        TextBox tot = (TextBox)grdmohon.Rows[0].FindControl("TextBox16");
        if (qty.ToString() != "")
        {
            tab1();
            decimal jum = Convert.ToDecimal(qty.Text);


            numTotal = jum;
            tot.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");
            GrandTotalt1();
        }
    }

    protected void dddata_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dddata.SelectedItem.Text == "MOHON BAYAR")
        {

            Gridview5.Visible = false;
            txtnoperbil.Visible = false;
            kataka.Visible = false;
            kodaka.Visible = false;
            invbil.Visible = false;
            ddnoper.Visible = true;
            tab5();
            try
            {
                string com = "SELECT M.no_permohonan FROM KW_Pembayaran_mohon M    WHERE  M.status='L' and M.Inv_status='N'  group by  M.no_permohonan";
                SqlDataAdapter adpt = new SqlDataAdapter(com, con);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                ddnoper.DataSource = dt;
                ddnoper.DataTextField = "no_permohonan";
                ddnoper.DataValueField = "no_permohonan";
                ddnoper.DataBind();
                ddnoper.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        else if (dddata.SelectedItem.Text == "BARU")
        {
            SetInitialRowBil();
            Gridview5.Visible = true;
            grdbilinv.Visible = false;
            grdbilinv1.Visible = false;
            kataka.Visible = true;
            kodaka.Visible = true;
            txtnoperbil.Visible = true;
            ddnoper.Visible = false;
            invbil.Visible = true;
            tab5();
        }

    }



    protected void ddnoper_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddnoper.SelectedItem.Text != "--- PILIH ---")
        {

            try
            {


                //qry1 = "select  Format(tarkih_permohonan,'dd/MM/yyyy')tarkih_permohonan,Format(tarkih_invois,'dd/MM/yyyy')tarkih_invois,no_invois,No_rujukan,Keteragan,Format(Gjumlah,'#,##,###.00') Gjumlah,Format(Gst,'#,##,###.00') Gst,  Format(overall,'#,##,###.00') overall,format(jumlah,'#,##,###.00') jumlah from KW_Pembayaran_mohon where no_permohonan='" + ddnoper.SelectedItem.Text + "'";
                //SqlCommand cmd2 = new SqlCommand("" + qry1 + "", con);
                //SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                //DataSet ds2 = new DataSet();
                //da2.Fill(ds2);
                //if (ds2.Tables[0].Rows.Count == 0)
                //{
                //    ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
                //    grdbilinv.DataSource = ds2;
                //    grdbilinv.DataBind();
                //    int columncount = grdbilinv.Rows[0].Cells.Count;
                //    grdbilinv.Rows[0].Cells.Clear();
                //    grdbilinv.Rows[0].Cells.Add(new TableCell());
                //    grdbilinv.Rows[0].Cells[0].ColumnSpan = columncount;
                //    grdbilinv.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
                //}
                //else
                //{
                //    grdbilinv.DataSource = ds2;
                //    grdbilinv.DataBind();
                //    grdbilinv.Visible = true;
                //    lblSubItemBil();


                //}

                grdbilinv.Visible = true;
                qry2 = "select  Format(tarkih_permohonan,'dd/MM/yyyy')tarkih_permohonan,Format(M.tarkih_invois,'dd/MM/yyyy')tarkih_invois,M.no_invois,Keteragan,Gjumlah,M.Gst,M.overall,M.jumlah,jornal_no from KW_Pembayaran_mohon M     where M.no_permohonan='" + ddnoper.SelectedItem.Text + "' and M.Inv_status='N' and M.Status='L' ";
                SqlCommand cmd3 = new SqlCommand("" + qry2 + "", con);
                SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                DataSet ds3 = new DataSet();
                da3.Fill(ds3);
                if (ds3.Tables[0].Rows.Count == 0)
                {
                    ds3.Tables[0].Rows.Add(ds3.Tables[0].NewRow());
                    grdbilinv1.DataSource = ds3;
                    grdbilinv1.DataBind();
                    int columncount = grdbilinv1.Rows[0].Cells.Count;
                    grdbilinv1.Rows[0].Cells.Clear();
                    grdbilinv1.Rows[0].Cells.Add(new TableCell());
                    grdbilinv1.Rows[0].Cells[0].ColumnSpan = columncount;
                    grdbilinv1.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
                }
                else
                {
                    grdbilinv1.DataSource = ds3;
                    grdbilinv1.DataBind();
                    grdbilinv1.Visible = true;
                    lblSubItemBil();
                    tab5();

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }

    protected void ddnoperpv_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddnoperpv.SelectedItem.Text != "--- PILIH ---")
        {

            try
            {
                ddnoperpv_PV1.Text = ddnoperpv.SelectedItem.Text;
                DataTable ddokdicno = new DataTable();
                ddokdicno = Dbcon.Ora_Execute_table("select a.tarkih_mohon,a.tarkih_invois,a.no_invois,a.no_permohonan,a.jumlah,a.gstjumlah,a.othgstjumlah,a.Overall,isnull(b.Baki,0) as Baki,no_po from (select Format(tarkih_mohon,'dd/MM/yyyy')tarkih_mohon,Format(tarkih_invois,'dd/MM/yyyy')tarkih_invois,no_invois,no_permohonan,REPLACE(CONVERT(varchar(20), (CAST(SUM(jumlah) AS money)), 1), '.00', '.00') jumlah, REPLACE(CONVERT(varchar(20), (CAST(SUM(gstjumlah) AS money)), 1), '.00', '.00') gstjumlah, REPLACE(CONVERT(varchar(20), (CAST(SUM(othgstjumlah) AS money)), 1), '.00', '.00') othgstjumlah, REPLACE(CONVERT(varchar(20), (CAST(SUM(Overall) AS money)), 1), '.00', '.00') Overall,no_po from KW_Pembayaran_invoisBil_item where no_invois='" + ddnoperpv_PV1.Text + "' and  Status='B' group by  tarkih_mohon,tarkih_invois,no_invois,no_permohonan,no_po) a left join (select  no_Permohonan,no_invois,format( sum(Payamt) ,'#,##,###.00') Baki   from KW_Pembayaran_Pay_voucer group by no_permohonan,no_invois) b on a.no_permohonan=b.no_Permohonan and a.no_invois=b.no_invois");

                if (ddokdicno.Rows.Count != 0)
                {

                    qry1 = "select a.tarkih_mohon,a.tarkih_invois,a.no_invois,a.no_permohonan,a.jumlah,a.gstjumlah,a.othgstjumlah,a.Overall,isnull(b.Baki,0) as Baki,no_po from (select Format(tarkih_mohon,'dd/MM/yyyy')tarkih_mohon,Format(tarkih_invois,'dd/MM/yyyy')tarkih_invois,no_invois,no_permohonan,REPLACE(CONVERT(varchar(20), (CAST(SUM(jumlah) AS money)), 1), '.00', '.00') jumlah, REPLACE(CONVERT(varchar(20), (CAST(SUM(gstjumlah) AS money)), 1), '.00', '.00') gstjumlah, REPLACE(CONVERT(varchar(20), (CAST(SUM(othgstjumlah) AS money)), 1), '.00', '.00') othgstjumlah, REPLACE(CONVERT(varchar(20), (CAST(SUM(Overall) AS money)), 1), '.00', '.00') Overall,no_po from KW_Pembayaran_invoisBil_item where no_invois='" + ddnoperpv_PV1.Text + "' and  Status='B' group by  tarkih_mohon,tarkih_invois,no_invois,no_permohonan,no_po) a left join (select  no_Permohonan,no_invois,format( sum(Payamt) ,'#,##,###.00') Baki   from KW_Pembayaran_Pay_voucer group by no_permohonan,no_invois) b on a.no_permohonan=b.no_Permohonan and a.no_invois=b.no_invois";
                }
                else
                {
                    qry1 = "select a.tarkih_mohon,a.tarkih_invois,a.no_invois,(a.no_Rujukan) as no_permohonan,a.jumlah,a.gstjumlah,a.othgstjumlah,a.Overall,isnull(b.Baki,0) as Baki,no_po from (select Format(tarikh_credit,'dd/MM/yyyy')tarkih_mohon,Format(tarikh_invois,'dd/MM/yyyy')tarkih_invois,no_invois,no_Rujukan,REPLACE(CONVERT(varchar(20), (CAST(SUM(jumlah) AS money)), 1), '.00', '.00') jumlah, REPLACE(CONVERT(varchar(20), (CAST(SUM(gstjumlah) AS money)), 1), '.00', '.00') gstjumlah, REPLACE(CONVERT(varchar(20), (CAST(SUM(othgstjumlah) AS money)), 1), '.00', '.00') othgstjumlah, REPLACE(CONVERT(varchar(20), (CAST(SUM(Overall) AS money)), 1), '.00', '.00') Overall,'' as no_po from KW_Pembayaran_Credit_item where no_Rujukan='" + ddnoperpv_PV1.Text + "' group by  tarikh_credit,tarikh_invois,no_invois,no_Rujukan) a left join (select  no_Permohonan,no_invois,format( sum(Payamt) ,'#,##,###.00') Baki   from KW_Pembayaran_Pay_voucer group by no_permohonan,no_invois) b on a.no_Rujukan=b.no_Permohonan and a.no_invois=b.no_invois";
                }
                //qry1 = "select a.tarkih_mohon,a.tarkih_invois,a.no_invois,a.no_permohonan,a.jumlah,a.gstjumlah,a.othgstjumlah,a.Overall,isnull(b.Baki,0) as Baki from (select Format(tarkih_mohon,'dd/MM/yyyy')tarkih_mohon,Format(tarkih_invois,'dd/MM/yyyy')tarkih_invois,no_invois,no_permohonan,REPLACE(CONVERT(varchar(20), (CAST(SUM(jumlah) AS money)), 1), '.00', '.00') jumlah, REPLACE(CONVERT(varchar(20), (CAST(SUM(gstjumlah) AS money)), 1), '.00', '.00') gstjumlah, REPLACE(CONVERT(varchar(20), (CAST(SUM(othgstjumlah) AS money)), 1), '.00', '.00') othgstjumlah, REPLACE(CONVERT(varchar(20), (CAST(SUM(Overall) AS money)), 1), '.00', '.00') Overall from KW_Pembayaran_invoisBil_item where no_invois='" + ddnoperpv.SelectedItem.Text + "' and  Status='B' group by  tarkih_mohon,tarkih_invois,no_invois,no_permohonan) a left join (select  no_Permohonan,no_invois,sum(Payamt) Baki   from KW_Pembayaran_Pay_voucer group by no_permohonan,no_invois) b on a.no_permohonan=b.no_Permohonan and a.no_invois=b.no_invois";
                SqlCommand cmd2 = new SqlCommand("" + qry1 + "", con);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2);
                if (ds2.Tables[0].Rows.Count == 0)
                {
                    ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
                    grdpvinv.DataSource = ds2;
                    grdpvinv.DataBind();
                    int columncount = grdpvinv.Rows[0].Cells.Count;
                    grdpvinv.Rows[0].Cells.Clear();
                    grdpvinv.Rows[0].Cells.Add(new TableCell());
                    grdpvinv.Rows[0].Cells[0].ColumnSpan = columncount;
                    grdpvinv.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
                }
                else
                {
                    grdpvinv.DataSource = ds2;
                    grdpvinv.DataBind();
                    grdpvinv.Visible = true;


                    tab2();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }



    protected void ddkodKat_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selRowIndex1 = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
        DropDownList db1 = (DropDownList)grdbilinv.Rows[selRowIndex1].FindControl("ddkodKat");
        DropDownList db = (DropDownList)grdbilinv.Rows[selRowIndex1].FindControl("ddkodcre");
        if (db1.SelectedItem.Text == "PEMBEKAL")
        {
            tab5();
            try
            {
                string com = "select Ref_kod_akaun,Ref_nama_syarikat from KW_Ref_Pembekal ";
                SqlDataAdapter adpt = new SqlDataAdapter(com, con);
                DataTable dt = new DataTable();
                adpt.Fill(dt);


                db.DataSource = dt;
                db.DataTextField = "Ref_nama_syarikat";
                db.DataValueField = "Ref_kod_akaun";
                db.DataBind();
                db.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        else if (db1.SelectedItem.Text == "SEMUA COA")
        {
            tab5();

            try
            {
                string com = "select (kod_akaun + ' | '+ nama_akaun) nama_akaun,kod_akaun   from KW_Ref_Carta_Akaun where jenis_akaun_type != '1' and Status='A'";
                SqlDataAdapter adpt = new SqlDataAdapter(com, con);
                DataTable dt = new DataTable();
                adpt.Fill(dt);


                db.DataSource = dt;
                db.DataTextField = "nama_akaun";
                db.DataValueField = "kod_akaun";
                db.DataBind();
                db.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    protected void ddkataka_SelectedIndexChanged(object sender, EventArgs e)
    {
        bind_carta();
    }

    void bind_carta()
    {
        if (ddkataka.SelectedItem.Text == "PEMBEKAL")
        {
            tab5();
            try
            {
                string com = "select Ref_kod_akaun,Ref_nama_syarikat from KW_Ref_Pembekal ";
                SqlDataAdapter adpt = new SqlDataAdapter(com, con);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                ddkodaka.DataSource = dt;
                ddkodaka.DataTextField = "Ref_nama_syarikat";
                ddkodaka.DataValueField = "Ref_kod_akaun";
                ddkodaka.DataBind();
                ddkodaka.Items.Insert(0, new ListItem("--- PILIH ---", ""));


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        else if (ddkataka.SelectedItem.Text == "SEMUA COA")
        {
            tab5();
            try
            {
                string com = "select (kod_akaun + ' | '+ nama_akaun) nama_akaun,kod_akaun   from KW_Ref_Carta_Akaun where jenis_akaun_type != '1' and Status='A'";
                SqlDataAdapter adpt = new SqlDataAdapter(com, con);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                ddkodaka.DataSource = dt;
                ddkodaka.DataTextField = "nama_akaun";
                ddkodaka.DataValueField = "kod_akaun";
                ddkodaka.DataBind();
                ddkodaka.Items.Insert(0, new ListItem("--- PILIH ---", ""));


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        else
        {
            tab5();
            try
            {
                string com = "select (kod_akaun + ' | '+ nama_akaun) nama_akaun,kod_akaun   from KW_Ref_Carta_Akaun where jenis_akaun_type != '1' and Status='A'";
                SqlDataAdapter adpt = new SqlDataAdapter(com, con);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                ddkodaka.DataSource = dt;
                ddkodaka.DataTextField = "nama_akaun";
                ddkodaka.DataValueField = "kod_akaun";
                ddkodaka.DataBind();
                ddkodaka.Items.Insert(0, new ListItem("--- PILIH ---", ""));


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    protected void ddpvkod_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddpvkod.SelectedItem.Text != "--- PILIH ---")
        {
            string com = "select no_invois from KW_Pembayaran_invoisBil_item where cre_kod_akaun='" + ddpvkod.SelectedItem.Value + "' and  Status='B' group by no_invois";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddnoperpv.DataSource = dt;
            ddnoperpv.DataTextField = "no_invois";
            ddnoperpv.DataValueField = "no_invois";
            ddnoperpv.DataBind();
            ddnoperpv.Items.Insert(0, new ListItem("--- PILIH ---", ""));


            TXTPVNAME.Text = ddpvkod.SelectedItem.Text;

            tab2();
        }

    }


    protected void ddpvkat_SelectedIndexChanged(object sender, EventArgs e)
    {
        bind_grid1();
    }

    void bind_grid1()
    {
        if (ddpvkat.SelectedItem.Text == "PEMBEKAL")
        {
            tab2();
            try
            {
                string com = "select Ref_kod_akaun,Ref_nama_syarikat from KW_Ref_Pembekal ";
                SqlDataAdapter adpt = new SqlDataAdapter(com, con);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                ddpvkod.DataSource = dt;
                ddpvkod.DataTextField = "Ref_nama_syarikat";
                ddpvkod.DataValueField = "Ref_kod_akaun";
                ddpvkod.DataBind();
                ddpvkod.Items.Insert(0, new ListItem("--- PILIH ---", ""));


            }
            catch (Exception ex)
            {
                throw ex;
            }
            grdpvinv_new.Visible = false;
            noinv_new.Visible = true;
        }
        else if (ddpvkat.SelectedItem.Text == "SEMUA COA")
        {
            tab2();
            try
            {
                string com = "select (kod_akaun + ' | '+ nama_akaun) nama_akaun,kod_akaun   from KW_Ref_Carta_Akaun where jenis_akaun_type != '1' and Status='A'";
                SqlDataAdapter adpt = new SqlDataAdapter(com, con);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                ddpvkod.DataSource = dt;
                ddpvkod.DataTextField = "nama_akaun";
                ddpvkod.DataValueField = "kod_akaun";
                ddpvkod.DataBind();
                ddpvkod.Items.Insert(0, new ListItem("--- PILIH ---", ""));


            }
            catch (Exception ex)
            {
                throw ex;
            }
            noinv_new.Visible = false;
            grdpvinv_new.Visible = true;
            tab2();
        }
        else
        {
            grdpvinv_new.Visible = false;
            noinv_new.Visible = true;
        }
    }

    protected void ddpvcara_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddpvcara.SelectedValue != "002")
        {
            tab2();
            txtpvnocek.Attributes.Remove("disabled");
            DDbank.Attributes.Remove("disabled");
        }
        else
        {
            tab2();
            txtpvnocek.Attributes.Add("disabled", "disabled");
            txtpvnocek.Text = "";

        }
    }

   

    protected void Button14_Click(object sender, EventArgs e)
    {
        fr2.Visible = false;
        fr1.Visible = true;
        Bindbaucer();
        BindInvois();
        BindMohon();
        Bindcredit();
        Binddedit();
       
        reset();
        tab4();
        //lnk_clk_Deb();
        Session["dbtno1"] = "";
    }

    //protected void Button4_Click(object sender, EventArgs e)
    // {
    //     if (ddsts.SelectedItem.Text != "Baru")
    //     {
    //         if (TextBox29.Text != "")
    //         {
    //             DataTable dt = new DataTable();
    //             dt = Dbcon.Ora_Execute_table("update KW_Pembayaran_invois set status='L',catatan='" + TextBox29.Text + "' ,upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where no_permohonan='" + txtnoper.Text + "'");

    //             DataTable dt1 = new DataTable();
    //             dt1 = Dbcon.Ora_Execute_table("update KW_Pembayaran_invois_item set status='L',catatan='" + TextBox29.Text + "'  ,upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where no_permohonan='" + txtnoper.Text + "'");
    //             tab1();
    //             reset();
    //         }
    //         else
    //         {
    //             ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Please Enter the Catatan');", true);
    //         }
    //     }
    //     else
    //     {
    //         ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Please Select the Status');", true);
    //     }
    // }

    protected void GridView1_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            System.Web.UI.WebControls.Label lblctrl = (System.Web.UI.WebControls.Label)e.Row.FindControl("status");
            System.Web.UI.WebControls.Label lul_sts = (System.Web.UI.WebControls.Label)e.Row.FindControl("lul_status");
            System.Web.UI.WebControls.Label kat = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label17");
            System.Web.UI.WebControls.Label bay_cd = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label18");
            System.Web.UI.WebControls.Label jen_txi = (System.Web.UI.WebControls.Label)e.Row.FindControl("jen_txi");
            System.Web.UI.WebControls.Label bay_desc = (System.Web.UI.WebControls.Label)e.Row.FindControl("Bayar_kepada");
            DataTable ddokdicno = new DataTable();
            DataTable get_ino1 = new DataTable();
            get_ino1 = DBCon.Ora_Execute_table("select Ref_doc_code,Ref_doc_name from KW_Ref_Doc_kod where Ref_doc_code ='"+ jen_txi.Text + "'");
            if(get_ino1.Rows.Count != 0)
            {
                jen_txi.Text = get_ino1.Rows[0]["Ref_doc_name"].ToString();
            }
            if (kat.Text == "01")
            {
                ddokdicno = DBCon.Ora_Execute_table("SELECT stf_name as val1,stf_staff_no as val2 FROM hr_staff_profile s1 left join hr_post_his s2 on s2.pos_staff_no=s1.stf_staff_no and pos_end_dt >='" + DateTime.Now.ToString("yyyy-MM-dd") + "' where stf_staff_no='"+ bay_cd.Text + "'");
            }
            else if (kat.Text == "02")
            {
                ddokdicno = DBCon.Ora_Execute_table("select Ref_nama_syarikat as val1,Ref_no_syarikat as val2 from KW_Ref_Pembekal where Status='A' and Ref_no_syarikat='" + bay_cd.Text + "'");
            }
            else if (kat.Text == "03")
            {
                ddokdicno = DBCon.Ora_Execute_table("select Ref_nama_syarikat as val1,Ref_no_syarikat as val2 from KW_Ref_Pelanggan where Status='A' and Ref_no_syarikat='" + bay_cd.Text + "'");                
            }
            else if (kat.Text == "11")
            {
                ddokdicno = DBCon.Ora_Execute_table("select KK_Skrin_name as val1,KK_Skrin_id as val2 from KK_PID_Skrin where KK_Skrin_id='" + bay_cd.Text + "' and Status='A'");
            }
            if (ddokdicno.Rows.Count != 0)
            {
                bay_desc.Text = ddokdicno.Rows[0]["val1"].ToString();
            }
            if (lblctrl.Text == "WAITING")
            {
                lblctrl.Attributes.Add("Class", "label label-danger Uppercase");
                LinkButton Hlnk = e.Row.FindControl("LinkButton2") as LinkButton;
                Hlnk.Visible = true;
            }
            else
            {
                lblctrl.Attributes.Add("Class", "label label-primary Uppercase");
                LinkButton Hlnk = e.Row.FindControl("LinkButton2") as LinkButton;
                Hlnk.Visible = false;
            }

            if (lul_sts.Text == "WAITING")
            {
                lul_sts.Attributes.Add("Class", "label label-danger Uppercase");
            }
            else
            {
                lul_sts.Attributes.Add("Class", "label label-primary Uppercase");
            }
        }
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnButton = sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
            System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("lbl_mohon_no");
            string lblid1 = lblTitle.Text;
            DataTable ddokdicno = new DataTable();
            ddokdicno = DBCon.Ora_Execute_table("select * From KW_Pembayaran_mohon where no_permohonan='" + lblTitle.Text + "' and Status='A' ");
            if (ddokdicno.Rows.Count != 0)
            {
                string Inssql_main = "Update KW_Pembayaran_mohon set Status='T' where no_permohonan='" + lblTitle.Text + "' and Status='A'";
                Status = DBCon.Ora_Execute_CommamdText(Inssql_main);
                if(Status == "SUCCESS")
                {
                    string Inssql_item = "Update KW_Pembayaran_mohon_item set Status='T' where mhn_no_permohonan='" + lblTitle.Text + "' and Status='A'";
                    Status = DBCon.Ora_Execute_CommamdText(Inssql_item);
                    BindMohon();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Daftar Mohon Bayar Dihapuskan Berjaya',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
            }
        }
        catch
        {

        }
    }
    protected void pv_cetak(object sender, EventArgs e)
    {

        if (Session["pvno"].ToString() != "")
        {
            crt_pvlink();
            Session["pvno"] = txtpvno.Text;
            //Response.Redirect("KW_Penerimaan_invois_Rptview.aspx");

            string url = "KW_Pembeyaran_pv_rptview.aspx";
            string s = "window.open('" + url + "', 'popup_window', 'width=11000,height=700,left=100,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

        }



    }

}