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

public partial class kw_penerimaan : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    DBConnection DBCon = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection Dbcon = new DBConnection();
    string qry1 = string.Empty, qry2 = string.Empty;
    string str_sdt1 = string.Empty, end_edt1 = string.Empty;
    string userid;
    int sum = 0;
    decimal total = 0;
    decimal total1 = 0;
    decimal total2 = 0;
    decimal total3 = 0;
    string uniqueId;
    decimal Ftotal = 0;
    string CommandArgument1 = string.Empty;
    string get_grid = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Button14);
        scriptManager.RegisterPostBackControl(this.Button22);
        scriptManager.RegisterPostBackControl(this.Button21);
        scriptManager.RegisterPostBackControl(this.Button23);

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
            str_sdt1 = fd.ToString("yyyy-MM-dd");
            end_edt1 = fd1.ToString("yyyy-MM-dd");
            end_dt1.Text = s2_dt;

            string script = "  $().ready(function () {    var today = new Date();   var preYear = today.getFullYear() - 1; var curYear = today.getFullYear() - 0;$('.datepicker2').datepicker({ format: 'dd/mm/yyyy',autoclose: true,inline: true,startDate: new Date(" + s1_dt + "),endDate: new Date(" + s2_dt + ")}).on('changeDate', function(ev) {(ev.viewMode == 'days') ? $(this).datepicker('hide') : '';});  $('.select2').select2(); $(" + pp6.ClientID + ").click(function() { $(" + hd_txt.ClientID + ").text('INVOIS'); }); $(" + pp1.ClientID + ").click(function() { $(" + hd_txt.ClientID + ").text('RESIT');});$(" + pp2.ClientID + ").click(function() {$(" + hd_txt.ClientID + ").text('NOTA KREDIT');});$(" + pp3.ClientID + ").click(function() {$(" + hd_txt.ClientID + ").text('NOTA DEBIT');});});";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        }
        
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
                loadvis();
                SetInitialRow();
                SetInitialRowRes();
                SetInitialRowCre();
                SetInitialRowDeb();
                invois_view();
                pelanggan1();
                akaun();
                pelanggan();
                Bentuk();
                Bentuk1();
                project();
                //BindKoff();
                bank();
                Bindinvois();

                SetInitialRowPay();
                Bindresit();
                Bindcredit();
                Binddedit();

                hd_txt.Text = "INVOIS";
                
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('762','705','1748','764','765','766','767','768','1749','1750','769','770','14','39','1751','1752','1753','763','1754','1755','61','1756','1757','1758','1759', '1760', '1761', '110', '1762', '1763', '1764', '1766', '1402', '1767', '1769', '1338', '1753', '1771', '169', '1772', '1773', '1774', '1765', '1775')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[38][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());             
            ps_lbl10.Text = txtinfo.ToTitleCase(gt_lng.Rows[17][0].ToString().ToLower());     
            ps_lbl11.Text = txtinfo.ToTitleCase(gt_lng.Rows[33][0].ToString().ToLower());
            ps_lbl12.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            ps_lbl13.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            but.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            Button6.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl16.Text = txtinfo.ToTitleCase(gt_lng.Rows[18][0].ToString().ToLower());
            ps_lbl17.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl18.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            ps_lbl19.Text = txtinfo.ToTitleCase(gt_lng.Rows[19][0].ToString().ToLower());
            ps_lbl20.Text = txtinfo.ToTitleCase(gt_lng.Rows[20][0].ToString().ToLower());           
            ps_lbl21.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl22.Text = txtinfo.ToTitleCase(gt_lng.Rows[34][0].ToString().ToLower());
            ps_lbl23.Text = txtinfo.ToTitleCase(gt_lng.Rows[21][0].ToString().ToLower());
            Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            Button14.Text = txtinfo.ToTitleCase(gt_lng.Rows[22][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[23][0].ToString().ToLower());
            Button19.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            Button20.Text = txtinfo.ToTitleCase(gt_lng.Rows[23][0].ToString().ToLower());
            ps_lbl29.Text = txtinfo.ToTitleCase(gt_lng.Rows[39][0].ToString().ToLower());
            ps_lbl30.Text = txtinfo.ToTitleCase(gt_lng.Rows[17][0].ToString().ToLower());           
            ps_lbl31.Text = txtinfo.ToTitleCase(gt_lng.Rows[24][0].ToString().ToLower());
            ps_lbl32.Text = txtinfo.ToTitleCase(gt_lng.Rows[41][0].ToString().ToLower());
            Button11.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            Button8.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl35.Text = txtinfo.ToTitleCase(gt_lng.Rows[25][0].ToString().ToLower());
            ps_lbl36.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl37.Text = txtinfo.ToTitleCase(gt_lng.Rows[40][0].ToString().ToLower());
            ps_lbl38.Text = txtinfo.ToTitleCase(gt_lng.Rows[24][0].ToString().ToLower());
            ps_lbl39.Text = txtinfo.ToTitleCase(gt_lng.Rows[41][0].ToString().ToLower());
            ps_lbl40.Text = txtinfo.ToTitleCase(gt_lng.Rows[26][0].ToString().ToLower());            
            ps_lbl41.Text = txtinfo.ToTitleCase(gt_lng.Rows[42][0].ToString().ToLower());
            //ps_lbl42.Text = txtinfo.ToTitleCase(gt_lng.Rows[27][0].ToString().ToLower());
            ps_lbl43.Text = txtinfo.ToTitleCase(gt_lng.Rows[28][0].ToString().ToLower());
            ps_lbl44.Text = txtinfo.ToTitleCase(gt_lng.Rows[16][0].ToString().ToLower());
            ps_lbl45.Text = txtinfo.ToTitleCase(gt_lng.Rows[35][0].ToString().ToLower());
            ps_lbl46.Text = txtinfo.ToTitleCase(gt_lng.Rows[21][0].ToString().ToLower());
            Button5.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            Button23.Text = txtinfo.ToTitleCase(gt_lng.Rows[22][0].ToString().ToLower());
            Button7.Text = txtinfo.ToTitleCase(gt_lng.Rows[23][0].ToString().ToLower());                 
            ps_lbl50.Text = txtinfo.ToTitleCase(gt_lng.Rows[17][0].ToString().ToLower());
            ps_lbl51.Text = txtinfo.ToTitleCase(gt_lng.Rows[29][0].ToString().ToLower());
            ps_lbl52.Text = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());
            ps_lbl53.Text = txtinfo.ToTitleCase(gt_lng.Rows[33][0].ToString().ToLower());
            Button12.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            Button9.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl56.Text = txtinfo.ToTitleCase(gt_lng.Rows[33][0].ToString().ToLower());
            ps_lbl57.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            ps_lbl58.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl59.Text = txtinfo.ToTitleCase(gt_lng.Rows[20][0].ToString().ToLower());
            ps_lbl60.Text = txtinfo.ToTitleCase(gt_lng.Rows[36][0].ToString().ToLower());
            ps_lbl61.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());              
            ps_lbl62.Text = txtinfo.ToTitleCase(gt_lng.Rows[37][0].ToString().ToLower());
            ps_lbl63.Text = txtinfo.ToTitleCase(gt_lng.Rows[38][0].ToString().ToLower());
            ps_lbl64.Text = txtinfo.ToTitleCase(gt_lng.Rows[21][0].ToString().ToLower());
            Button15.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            Button22.Text = txtinfo.ToTitleCase(gt_lng.Rows[22][0].ToString().ToLower());
            Button16.Text = txtinfo.ToTitleCase(gt_lng.Rows[23][0].ToString().ToLower());
            ps_lbl68.Text = txtinfo.ToTitleCase(gt_lng.Rows[17][0].ToString().ToLower());
            ps_lbl69.Text = txtinfo.ToTitleCase(gt_lng.Rows[30][0].ToString().ToLower());          
            ps_lbl70.Text = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());
            ps_lbl71.Text = txtinfo.ToTitleCase(gt_lng.Rows[32][0].ToString().ToLower());
            Button13.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            Button10.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl74.Text = txtinfo.ToTitleCase(gt_lng.Rows[33][0].ToString().ToLower());
            ps_lbl75.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            ps_lbl76.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl77.Text = txtinfo.ToTitleCase(gt_lng.Rows[20][0].ToString().ToLower());
            ps_lbl78.Text = txtinfo.ToTitleCase(gt_lng.Rows[31][0].ToString().ToLower());
            ps_lbl79.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());            
            ps_lbl80.Text = txtinfo.ToTitleCase(gt_lng.Rows[37][0].ToString().ToLower());
            ps_lbl81.Text = txtinfo.ToTitleCase(gt_lng.Rows[38][0].ToString().ToLower());
            ps_lbl82.Text = txtinfo.ToTitleCase(gt_lng.Rows[21][0].ToString().ToLower());
            Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            Button21.Text = txtinfo.ToTitleCase(gt_lng.Rows[22][0].ToString().ToLower());
            Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[23][0].ToString().ToLower());
            ps_lbl86.Text = txtinfo.ToTitleCase(gt_lng.Rows[32][0].ToString().ToLower());
            Button17.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            Button18.Text = txtinfo.ToTitleCase(gt_lng.Rows[23][0].ToString().ToLower());
           

        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }

    void loadvis()
    {
        Div7.Visible = false;
        fr2.Visible = false;
        Div10.Visible = false;
        th2.Visible = false;
        Div3.Visible = true;
        Li2.Visible = false;

        // pp15.Visible = false;
        Button14.Visible = false;
        Button21.Visible = false;
        Button22.Visible = false;
        dddays.SelectedIndex = 0;
        TextBox14.Attributes.Add("disabled", "disabled");
        TextBox11.Attributes.Add("disabled", "disabled");
        TextBox18.Attributes.Add("disabled", "disabled");
        TextBox5.Attributes.Add("disabled", "disabled");
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
            DataTable dt = Dbcon.Ora_Execute_table("select    ISNULL(max(SUBSTRING(no_Rujukan,13,2000)),'0') from KW_Penerimaan_Credit_item");
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

    private void GetUniqueInv()
    {

        DataTable dt1 = Dbcon.Ora_Execute_table("select Id,doc_type_cd,format as fmt,SUBSTRING(cur_format, 1, 10) as lfrmt1,SUBSTRING(cur_format, 11, 4) as lfrmt2,cur_format as cfmt from KW_Format_Nombor_rujukan where doc_type_cd='01' and Status='A'");
        if (dt1.Rows.Count != 0)
        {
            if (dt1.Rows[0]["cfmt"].ToString() == "")
            {
                txtnoinvois1.Text = dt1.Rows[0]["fmt"].ToString();
                txtnoinvois1.Attributes.Add("disabled", "disabled");
            }
            else
            {
                int seqno = Convert.ToInt32(dt1.Rows[0]["lfrmt2"].ToString());
                int newNumber = seqno + 1;
                uniqueId = newNumber.ToString(dt1.Rows[0]["lfrmt1"].ToString() + "0000");
                txtnoinvois1.Text = uniqueId;
                txtnoinvois1.Attributes.Add("disabled", "disabled");
            }

        }
        else
        {
            DataTable dt = Dbcon.Ora_Execute_table("select   ISNULL(max(SUBSTRING(no_invois,13,2000)),'0')  from KW_Penerimaan_invois");
            if (dt.Rows.Count > 0)
            {
                int seqno = Convert.ToInt32(dt.Rows[0][0].ToString());
                int newNumber = seqno + 1;
                uniqueId = newNumber.ToString("KTH-INV" + "-" + DateTime.Now.ToString("yy") + "-" + "0000");
                txtnoinvois1.Text = uniqueId;
                txtnoinvois1.Attributes.Add("disabled", "disabled");

            }
            else
            {
                int newNumber = Convert.ToInt32(uniqueId) + 1;
                uniqueId = newNumber.ToString("KTH-INV" + "-" + DateTime.Now.ToString("yy") + "-" + "0000");
                txtnoinvois1.Text = uniqueId;
                txtnoinvois1.Enabled = false;
                txtnoinvois1.Attributes.Add("disabled", "disabled");
            }
        }

    }

    private void GetUniqueRES()
    {
        DataTable dt1 = Dbcon.Ora_Execute_table("select doc_type_cd,format as fmt,SUBSTRING(cur_format, 1, 10) as lfrmt1,SUBSTRING(cur_format, 11, 4) as lfrmt2,cur_format as cfmt from KW_Format_Nombor_rujukan where doc_type_cd='02' and Status='A'");
        if (dt1.Rows.Count != 0)
        {
            if (dt1.Rows[0]["cfmt"].ToString() == "")
            {
                txtnorst3.Text = dt1.Rows[0]["fmt"].ToString();
                txtnorst3.Attributes.Add("disabled", "disabled");
            }
            else
            {
                int seqno = Convert.ToInt32(dt1.Rows[0]["lfrmt2"].ToString());
                int newNumber = seqno + 1;
                uniqueId = newNumber.ToString(dt1.Rows[0]["lfrmt1"].ToString() + "0000");
                txtnorst3.Text = uniqueId;
                txtnorst3.Attributes.Add("disabled", "disabled");
            }

        }
        else
        {
            DataTable dt = Dbcon.Ora_Execute_table("select   ISNULL(max(SUBSTRING(no_resit,13,2000)),'0')  from KW_Penerimaan_resit");
            if (dt.Rows.Count > 0)
            {
                int seqno = Convert.ToInt32(dt.Rows[0][0].ToString());
                int newNumber = seqno + 1;
                uniqueId = newNumber.ToString("KTH-RES" + "-" + DateTime.Now.ToString("yy") + "-" + "0000");
                txtnorst3.Text = uniqueId;
                txtnorst3.Attributes.Add("disabled", "disabled");

            }
            else
            {
                int newNumber = Convert.ToInt32(uniqueId) + 1;
                uniqueId = newNumber.ToString("KTH-RES" + "-" + DateTime.Now.ToString("yy") + "-" + "0000");
                txtnorst3.Text = uniqueId;
                txtnorst3.Enabled = false;
                txtnorst3.Attributes.Add("disabled", "disabled");
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
            DataTable dt = Dbcon.Ora_Execute_table("select   ISNULL(max(SUBSTRING(no_Rujukan,13,2000)),'0') from KW_Penerimaan_Debit_item ");
            if (dt.Rows.Count > 0)
            {
                int seqno = Convert.ToInt32(dt.Rows[0][0].ToString());
                int newNumber = seqno + 1;
                uniqueId = newNumber.ToString("KTH-DBT" + "-" + DateTime.Now.ToString("yy") + "-" + "0000");
                txtnoruj2.Text = uniqueId;

                txtnoruj2.Attributes.Add("disabled", "disabled");
            }
            else
            {
                int newNumber = Convert.ToInt32(uniqueId) + 1;
                uniqueId = newNumber.ToString("KTH-DBT" + "-" + DateTime.Now.ToString("yy") + "-" + "0000");
                txtnoruj2.Text = uniqueId;

                txtnoruj2.Attributes.Add("disabled", "disabled");
            }
        }

    }
    private void SetInitialRow()
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
        ViewState["CurrentTable"] = dt;

        Gridview1.DataSource = dt;
        Gridview1.DataBind();

    }
    private void SetInitialRowRes()
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
        dr["Column10"] = 0.00;
        dr["Column11"] = string.Empty;
        dr["Column12"] = string.Empty;
        dr["Column13"] = string.Empty;
        dr["Column14"] = string.Empty;


        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentTable2"] = dt;

        Gridview4.DataSource = dt;
        Gridview4.DataBind();

    }

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
    private void SetInitialRowPay()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));

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
        dr["RowNumber"] = 1;
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
        ViewState["CurrentTable5"] = dt;

        grdpay.DataSource = dt;
        grdpay.DataBind();

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

    // add new row funtion

    private void AddNewRowToGrid()
    {
        int rowIndex = 0;
        decimal total1 = 0;
        decimal total2 = 0;
        decimal total3 = 0;
        decimal total4 = 0;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    DropDownList ddl1 = (DropDownList)Gridview1.Rows[rowIndex].Cells[1].FindControl("ddkod");
                    TextBox box_ruj = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txt_rujukan");
                    TextBox box_po = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txt_po");
                    TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("TextBox1");
                    TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("TextBox2");
                    TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[6].FindControl("TextBox4");
                    TextBox box4 = (TextBox)Gridview1.Rows[rowIndex].Cells[7].FindControl("TextBox3");
                    TextBox box5 = (TextBox)Gridview1.Rows[rowIndex].Cells[8].FindControl("Txtdis");
                    Label box6 = (Label)Gridview1.Rows[rowIndex].Cells[9].FindControl("Label1");
                    CheckBox chk = (CheckBox)Gridview1.Rows[rowIndex].Cells[10].FindControl("CheckBox1");
                    DropDownList box7 = (DropDownList)Gridview1.Rows[rowIndex].Cells[11].FindControl("ddcukaiinv");
                    Label box8 = (Label)Gridview1.Rows[rowIndex].Cells[12].FindControl("Label8");
                    DropDownList box9 = (DropDownList)Gridview1.Rows[rowIndex].Cells[13].FindControl("ddcukaioth");
                    Label box10 = (Label)Gridview1.Rows[rowIndex].Cells[14].FindControl("Label10");
                    Label box11 = (Label)Gridview1.Rows[rowIndex].Cells[14].FindControl("Label5");
                    drCurrentRow = dtCurrentTable.NewRow();

                    dtCurrentTable.Rows[i - 1]["Column1"] = ddl1.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Column2"] = box_ruj.Text;
                    dtCurrentTable.Rows[i - 1]["Column3"] = box_po.Text;
                    dtCurrentTable.Rows[i - 1]["Column4"] = box1.Text;
                    dtCurrentTable.Rows[i - 1]["Column5"] = box2.Text;
                    dtCurrentTable.Rows[i - 1]["Column6"] = box3.Text;
                    dtCurrentTable.Rows[i - 1]["Column7"] = box4.Text;
                    dtCurrentTable.Rows[i - 1]["Column8"] = box5.Text;
                    dtCurrentTable.Rows[i - 1]["Column9"] = box6.Text;
                    dtCurrentTable.Rows[i - 1]["Column10"] = chk.Checked.ToString();
                    dtCurrentTable.Rows[i - 1]["Column11"] = box7.SelectedItem.Value;
                    dtCurrentTable.Rows[i - 1]["Column12"] = box8.Text;
                    dtCurrentTable.Rows[i - 1]["Column13"] = box9.SelectedItem.Value;
                    dtCurrentTable.Rows[i - 1]["Column14"] = box10.Text;
                    dtCurrentTable.Rows[i - 1]["Column15"] = box11.Text;

                    rowIndex++;

                    if (box6.Text != "")
                    {
                        decimal tamt1 = Convert.ToDecimal(box6.Text);
                        total1 += tamt1;
                    }

                    if (box8.Text != "")
                    {

                        decimal tamt2 = Convert.ToDecimal(box8.Text);
                        total2 += tamt2;

                    }
                    if (box10.Text != "")
                    {
                        decimal tamt3 = Convert.ToDecimal(box10.Text);
                        total3 += tamt3;
                    }
                    if (box11.Text != "")
                    {
                        decimal tamt4 = Convert.ToDecimal(box11.Text);
                        total4 += tamt4;
                    }

                }
                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable2"] = dtCurrentTable;

                Gridview1.DataSource = dtCurrentTable;
                Gridview1.DataBind();
                tab1();
                Gridview1.FooterRow.Cells[2].Text = "JUMLAH (RM) :";
                Gridview1.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                ((Label)Gridview1.FooterRow.Cells[7].FindControl("Label3")).Text = total1.ToString("C").Replace("RM", "").Replace("$", "");
                ((Label)Gridview1.FooterRow.Cells[9].FindControl("Label4")).Text = total2.ToString("C").Replace("RM", "").Replace("$", "");
                ((Label)Gridview1.FooterRow.Cells[11].FindControl("Label7")).Text = total3.ToString("C").Replace("RM", "").Replace("$", "");
                ((Label)Gridview1.FooterRow.Cells[1].FindControl("Label6")).Text = total4.ToString("C").Replace("RM", "").Replace("$", "");

                if (dtCurrentTable.Rows.Count == 2)
                {
                    Gridview1.FooterRow.Cells[1].Enabled = false;
                }
                else
                {
                    Gridview1.FooterRow.Cells[1].Enabled = true;
                }
            }

        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousData();
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
                    DropDownList ddl1 = (DropDownList)Gridview4.Rows[rowIndex].Cells[1].FindControl("ddkodres");
                    TextBox res_ruj = (TextBox)Gridview4.Rows[rowIndex].Cells[2].FindControl("resit_rujukan");
                    TextBox box1 = (TextBox)Gridview4.Rows[rowIndex].Cells[3].FindControl("TextBox11");
                    TextBox box2 = (TextBox)Gridview4.Rows[rowIndex].Cells[4].FindControl("TextBox12");
                    TextBox box3 = (TextBox)Gridview4.Rows[rowIndex].Cells[5].FindControl("TextBox13");
                    TextBox box4 = (TextBox)Gridview4.Rows[rowIndex].Cells[6].FindControl("TextBox14");
                    TextBox box5 = (TextBox)Gridview4.Rows[rowIndex].Cells[7].FindControl("Txtdisres");
                    CheckBox chk = (CheckBox)Gridview4.Rows[rowIndex].Cells[8].FindControl("chkres");
                    DropDownList box6 = (DropDownList)Gridview4.Rows[rowIndex].Cells[9].FindControl("ddrescuk");
                    Label box7 = (Label)Gridview4.Rows[rowIndex].Cells[10].FindControl("Label8");
                    DropDownList box8 = (DropDownList)Gridview4.Rows[rowIndex].Cells[11].FindControl("ddresoth");
                    Label box9 = (Label)Gridview4.Rows[rowIndex].Cells[12].FindControl("Label10");
                    Label box10 = (Label)Gridview4.Rows[rowIndex].Cells[13].FindControl("Label3");
                    Label box11 = (Label)Gridview4.Rows[rowIndex].Cells[13].FindControl("Label5");

                    drCurrentRow = dtCurrentTable2.NewRow();

                    dtCurrentTable2.Rows[i - 1]["Column1"] = ddl1.SelectedValue;
                    dtCurrentTable2.Rows[i - 1]["Column2"] = res_ruj.Text;
                    dtCurrentTable2.Rows[i - 1]["Column3"] = box1.Text;
                    dtCurrentTable2.Rows[i - 1]["Column4"] = box2.Text;
                    dtCurrentTable2.Rows[i - 1]["Column5"] = box3.Text;
                    dtCurrentTable2.Rows[i - 1]["Column6"] = box4.Text;
                    dtCurrentTable2.Rows[i - 1]["Column7"] = box5.Text;
                    dtCurrentTable2.Rows[i - 1]["Column8"] = chk.Checked.ToString();
                    dtCurrentTable2.Rows[i - 1]["Column9"] = box6.SelectedValue;
                    dtCurrentTable2.Rows[i - 1]["Column10"] = box7.Text;
                    dtCurrentTable2.Rows[i - 1]["Column11"] = box8.SelectedValue;
                    dtCurrentTable2.Rows[i - 1]["Column12"] = box9.Text;
                    dtCurrentTable2.Rows[i - 1]["Column13"] = box10.Text;
                    dtCurrentTable2.Rows[i - 1]["Column14"] = box11.Text;
                    rowIndex++;

                    if (box7.Text != "--- PILIH ---")
                    {
                        if (box7.Text != "")
                        {
                            decimal tamt1 = Convert.ToDecimal(box7.Text);
                            total1 += tamt1;
                        }
                        else
                        {
                            decimal tamt1 = 0;
                            total1 += tamt1;
                        }
                    }
                    if (box9.Text != "")
                    {
                        decimal tamt2 = Convert.ToDecimal(box9.Text);
                        total2 += tamt2;
                    }
                    if (box10.Text != "")
                    {
                        decimal tamt3 = Convert.ToDecimal(box10.Text);
                        total3 += tamt3;
                    }
                    if (box11.Text != "")
                    {
                        decimal tamt4 = Convert.ToDecimal(box11.Text);
                        total4 += tamt4;
                    }

                }
                dtCurrentTable2.Rows.Add(drCurrentRow);
                ViewState["CurrentTable2"] = dtCurrentTable2;

                Gridview4.DataSource = dtCurrentTable2;
                Gridview4.DataBind();
                tab2();
                Gridview4.FooterRow.Cells[2].Text = "JUMLAH (RM) :";
                Gridview4.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                ((Label)Gridview4.FooterRow.Cells[8].FindControl("Label2")).Text = total1.ToString("C").Replace("RM", "").Replace("$", "");
                ((Label)Gridview4.FooterRow.Cells[10].FindControl("Label7")).Text = total2.ToString("C").Replace("RM", "").Replace("$", "");
                ((Label)Gridview4.FooterRow.Cells[12].FindControl("Label4")).Text = total3.ToString("C").Replace("RM", "").Replace("$", "");
                ((Label)Gridview4.FooterRow.Cells[12].FindControl("Label6")).Text = total4.ToString("C").Replace("RM", "").Replace("$", "");
                if (dtCurrentTable2.Rows.Count == 2)
                {
                    Gridview4.FooterRow.Cells[1].Enabled = false;
                }
                else
                {
                    Gridview4.FooterRow.Cells[1].Enabled = true;
                }
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousData1();
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

    private void AddNewRowToGridpay()
    {
        int rowIndex = 0;
        decimal total1 = 0;
        decimal total2 = 0;
        decimal total3 = 0;
        decimal total4 = 0;
        if (ViewState["CurrentTable5"] != null)
        {
            DataTable dtCurrentTable5 = (DataTable)ViewState["CurrentTable5"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable5.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable5.Rows.Count; i++)
                {
                    //extract the TextBox values
                    DropDownList ddl1 = (DropDownList)grdpay.Rows[rowIndex].Cells[1].FindControl("ddkoddeb");
                    TextBox box1 = (TextBox)grdpay.Rows[rowIndex].Cells[2].FindControl("TextBox1");
                    TextBox box2 = (TextBox)grdpay.Rows[rowIndex].Cells[3].FindControl("TextBox2");
                    TextBox box3 = (TextBox)grdpay.Rows[rowIndex].Cells[4].FindControl("Txtnor2");
                    DropDownList box4 = (DropDownList)grdpay.Rows[rowIndex].Cells[5].FindControl("ddBaya");
                    TextBox box5 = (TextBox)grdpay.Rows[rowIndex].Cells[6].FindControl("TextBox3");
                    TextBox box6 = (TextBox)grdpay.Rows[rowIndex].Cells[7].FindControl("TextBox4");



                    drCurrentRow = dtCurrentTable5.NewRow();

                    dtCurrentTable5.Rows[i - 1]["Column1"] = ddl1.SelectedValue;
                    dtCurrentTable5.Rows[i - 1]["Column2"] = box1.Text;
                    dtCurrentTable5.Rows[i - 1]["Column3"] = box2.Text;
                    dtCurrentTable5.Rows[i - 1]["Column4"] = box3.Text;
                    dtCurrentTable5.Rows[i - 1]["Column5"] = box4.Text;
                    dtCurrentTable5.Rows[i - 1]["Column6"] = box5.Text;
                    dtCurrentTable5.Rows[i - 1]["Column7"] = box6.Text;

                    rowIndex++;

                    if (box6.Text != "")
                    {
                        decimal tamt1 = Convert.ToDecimal(box6.Text);
                        total1 += tamt1;
                    }

                }
                dtCurrentTable5.Rows.Add(drCurrentRow);
                ViewState["CurrentTable5"] = dtCurrentTable5;

                grdpay.DataSource = dtCurrentTable5;
                grdpay.DataBind();
                tab1();
                grdpay.FooterRow.Cells[2].Text = "JUMLAH (RM) :";
                grdpay.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                ((Label)grdpay.FooterRow.Cells[7].FindControl("Label2")).Text = total1.ToString("C").Replace("RM", "").Replace("$", "");
                ((Label)grdpay.FooterRow.Cells[9].FindControl("Label4")).Text = total2.ToString("C").Replace("RM", "").Replace("$", "");
                ((Label)grdpay.FooterRow.Cells[11].FindControl("Label6")).Text = total3.ToString("C").Replace("RM", "").Replace("$", "");
                ((Label)grdpay.FooterRow.Cells[12].FindControl("Label7")).Text = total4.ToString("C").Replace("RM", "").Replace("$", "");
                if (dtCurrentTable5.Rows.Count == 2)
                {
                    grdpay.FooterRow.Cells[1].Enabled = false;
                }
                else
                {
                    grdpay.FooterRow.Cells[1].Enabled = true;
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
                Gridview1.DataSource = dt;
                Gridview1.DataBind();

                for (int i = 0; i < Gridview1.Rows.Count - 1; i++)
                {
                    Gridview1.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                }
                SetPreviousData();
                GrandTotal();
                //Gridview1.FooterRow.Cells[2].Text = "JUMLAH (RM) :";
                //Gridview1.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                //((TextBox)Gridview1.FooterRow.Cells[3].FindControl("txtTotal")).Text = total.ToString("C").Replace("RM", "").Replace("$", "");
                //((TextBox)Gridview1.FooterRow.Cells[4].FindControl("txtTotal1")).Text = total1.ToString("C").Replace("RM", "").Replace("$", "");
            }
        }
    }


    private void SetPreviousData()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList box0 = (DropDownList)Gridview1.Rows[rowIndex].Cells[1].FindControl("ddkod");
                    TextBox box_ruj = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txt_rujukan");
                    TextBox box_po = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txt_po");
                    TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("TextBox1");
                    TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("TextBox2");
                    TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[6].FindControl("TextBox4");
                    TextBox box4 = (TextBox)Gridview1.Rows[rowIndex].Cells[7].FindControl("TextBox3");
                    TextBox box5 = (TextBox)Gridview1.Rows[rowIndex].Cells[8].FindControl("Txtdis");
                    Label box6 = (Label)Gridview1.Rows[rowIndex].Cells[9].FindControl("Label1");
                    CheckBox chk = (CheckBox)Gridview1.Rows[rowIndex].Cells[10].FindControl("CheckBox1");
                    DropDownList box7 = (DropDownList)Gridview1.Rows[rowIndex].Cells[11].FindControl("ddcukaiinv");
                    Label box8 = (Label)Gridview1.Rows[rowIndex].Cells[12].FindControl("Label8");
                    DropDownList box9 = (DropDownList)Gridview1.Rows[rowIndex].Cells[13].FindControl("ddcukaioth");
                    Label box10 = (Label)Gridview1.Rows[rowIndex].Cells[14].FindControl("Label10");
                    Label box11 = (Label)Gridview1.Rows[rowIndex].Cells[14].FindControl("Label5");

                    box0.Text = dt.Rows[i]["Column1"].ToString();
                    box_ruj.Text = dt.Rows[i]["Column2"].ToString();
                    box_po.Text = dt.Rows[i]["Column3"].ToString();
                    box1.Text = dt.Rows[i]["Column4"].ToString();
                    box2.Text = dt.Rows[i]["Column5"].ToString();
                    box3.Text = dt.Rows[i]["Column6"].ToString();
                    box4.Text = dt.Rows[i]["Column7"].ToString();
                    box5.Text = dt.Rows[i]["Column8"].ToString();
                    box6.Text = dt.Rows[i]["Column9"].ToString();
                    chk.Checked = dt.Rows[i]["Column10"].ToString().ToUpperInvariant() == "TRUE";
                    box7.Text = dt.Rows[i]["Column11"].ToString();
                    box8.Text = dt.Rows[i]["Column12"].ToString();
                    box9.Text = dt.Rows[i]["Column13"].ToString();
                    box10.Text = dt.Rows[i]["Column14"].ToString();
                    box11.Text = dt.Rows[i]["Column15"].ToString();


                    rowIndex++;
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
                    DropDownList box0 = (DropDownList)Gridview4.Rows[rowIndex].Cells[1].FindControl("ddkodres");
                    TextBox res_ruj = (TextBox)Gridview4.Rows[rowIndex].Cells[2].FindControl("resit_rujukan");
                    TextBox box1 = (TextBox)Gridview4.Rows[rowIndex].Cells[3].FindControl("TextBox11");
                    TextBox box2 = (TextBox)Gridview4.Rows[rowIndex].Cells[4].FindControl("TextBox12");
                    TextBox box3 = (TextBox)Gridview4.Rows[rowIndex].Cells[5].FindControl("TextBox13");
                    TextBox box4 = (TextBox)Gridview4.Rows[rowIndex].Cells[6].FindControl("TextBox14");
                    TextBox box5 = (TextBox)Gridview4.Rows[rowIndex].Cells[7].FindControl("Txtdisres");
                    CheckBox chk = (CheckBox)Gridview4.Rows[rowIndex].Cells[8].FindControl("chkres");
                    DropDownList box6 = (DropDownList)Gridview4.Rows[rowIndex].Cells[9].FindControl("ddrescuk");
                    Label box7 = (Label)Gridview4.Rows[rowIndex].Cells[10].FindControl("Label8");
                    DropDownList box8 = (DropDownList)Gridview4.Rows[rowIndex].Cells[11].FindControl("ddresoth");
                    Label box9 = (Label)Gridview4.Rows[rowIndex].Cells[12].FindControl("Label10");
                    Label box10 = (Label)Gridview4.Rows[rowIndex].Cells[13].FindControl("Label3");
                    Label box11 = (Label)Gridview4.Rows[rowIndex].Cells[13].FindControl("Label5");

                    box0.SelectedValue = dt.Rows[i]["Column1"].ToString();
                    res_ruj.Text = dt.Rows[i]["Column2"].ToString();
                    box1.Text = dt.Rows[i]["Column3"].ToString();
                    box2.Text = dt.Rows[i]["Column4"].ToString();
                    box3.Text = dt.Rows[i]["Column5"].ToString();
                    box4.Text = dt.Rows[i]["Column6"].ToString();
                    box5.Text = dt.Rows[i]["Column7"].ToString();
                    chk.Checked = dt.Rows[i]["Column8"].ToString().ToUpperInvariant() == "TRUE";
                    box6.Text = dt.Rows[i]["Column9"].ToString();
                    box7.Text = dt.Rows[i]["Column10"].ToString();
                    box8.Text = dt.Rows[i]["Column11"].ToString();
                    box9.Text = dt.Rows[i]["Column12"].ToString();
                    box10.Text = dt.Rows[i]["Column13"].ToString();
                    box11.Text = dt.Rows[i]["Column14"].ToString();


                    rowIndex++;
                }
            }
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
    // add new row button click event
    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }
    protected void ButtonAdd_Click1(object sender, EventArgs e)
    {
        AddNewRowToGrid1();
    }
    protected void ButtonAddcre_Click(object sender, EventArgs e)
    {
        AddNewRowToGridcredit();
    }

    protected void ButtonAdddeb_Click(object sender, EventArgs e)
    {
        AddNewRowToGriddebit();
    }
    protected void ButtonAddpay_Click(object sender, EventArgs e)
    {
        AddNewRowToGridpay();
    }


    protected void Button6_Click(object sender, EventArgs e)
    {
        Div7.Visible = true;
        Div9.Visible = false;
        Button2.Visible = true;
        Button14.Visible = false;
        Gridview1.Visible = true;
        Gridview3.Visible = false;
        Gridview1.Enabled = true;
        GetUniqueInv();
        Li2.Visible = false;
        tab1();
        reset();
        Bindresit();
        Bindcredit();
        Binddedit();
    }

    protected void Button8_Click(object sender, EventArgs e)
    {
        Div10.Visible = true;
        Div11.Visible = false;
        Button5.Visible = true;

        Gridview4.Visible = true;
        Gridview15.Visible = false;
        Gridview4.Enabled = true;
        GetUniqueRES();
        tab2();
        Bindcredit();
        Binddedit();
    }
    protected void Button9_Click(object sender, EventArgs e)
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
        Bindresit();
        Binddedit();
    }
    protected void Button10_Click(object sender, EventArgs e)
    {
        fr2.Visible = true;
        fr1.Visible = false;
        Gridview7.Visible = false;
        Gridview11.Visible = true;
        Gridview14.Visible = false;
        Button3.Visible = true;
        Button21.Visible = false;
        Gridview7.Enabled = true;
        GetUniqueIddeb();
        tab4();
        reset();
        Bindresit();
        Bindcredit();
    }




    // checkbox textchanged event


    protected void ddcukaiinv_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal numTotal1 = 0;
        decimal numTotal2 = 0;
        decimal numTotal3 = 0;
        decimal numTotal4 = 0;
        int selRowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
        Label tOt = (Label)Gridview1.Rows[selRowIndex].FindControl("Label1");
        TextBox qty = (TextBox)Gridview1.Rows[selRowIndex].FindControl("TextBox4");
        TextBox unit = (TextBox)Gridview1.Rows[selRowIndex].FindControl("TextBox3");
        TextBox dis = (TextBox)Gridview1.Rows[selRowIndex].FindControl("Txtdis");
        Label txt = (Label)Gridview1.FooterRow.FindControl("Label4");
        Label gamt = (Label)Gridview1.Rows[selRowIndex].FindControl("Label8");
        Label gmt = (Label)Gridview1.Rows[selRowIndex].FindControl("Label10");
        Label txt1 = (Label)Gridview1.Rows[selRowIndex].FindControl("Label5");
        CheckBox chk = (CheckBox)Gridview1.Rows[selRowIndex].FindControl("CheckBox1");
        DropDownList dd = (DropDownList)Gridview1.Rows[selRowIndex].FindControl("ddcukaioth");
        String tgst_1 = (Gridview1.Rows[selRowIndex].FindControl("ddcukaiinv") as DropDownList).SelectedItem.Value;
        Label tax1 = (Label)Gridview1.FooterRow.FindControl("Label7");
        if (qty.Text != "" && unit.Text != "")
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
                    GrandTotal();
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
                    tab1();
                    GrandTotal();
                }
            }
            else
            {
                if (dis.Text == "")
                {
                    dis.Text = "0.00";
                }
                numTotal1 = Convert.ToDecimal(txt1.Text);
                if (chk.Checked == true)
                {
                    numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(gmt.Text);
                    numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text);
                    tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                    txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                }
                else
                {
                    numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) + Convert.ToDecimal(gmt.Text);
                    numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text);
                    tOt.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                    txt1.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                }

                txt.Text = "0.00";
                gamt.Text = "0.00";
                tab1();
                GrandTotal();
            }
        }
    }




    protected void ddcukaioth_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal numTotal1 = 0;
        decimal numTotal2 = 0;
        decimal numTotal3 = 0;
        decimal numTotal4 = 0;
        int selRowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
        Label tOt = (Label)Gridview1.Rows[selRowIndex].FindControl("Label1");
        TextBox qty = (TextBox)Gridview1.Rows[selRowIndex].FindControl("TextBox4");
        TextBox unit = (TextBox)Gridview1.Rows[selRowIndex].FindControl("TextBox3");
        TextBox dis = (TextBox)Gridview1.Rows[selRowIndex].FindControl("Txtdis");
        Label txt = (Label)Gridview1.FooterRow.FindControl("Label7");
        Label gmt = (Label)Gridview1.Rows[selRowIndex].FindControl("Label10");
        Label txt1 = (Label)Gridview1.Rows[selRowIndex].FindControl("Label5");
        CheckBox chk = (CheckBox)Gridview1.Rows[selRowIndex].FindControl("CheckBox1");
        DropDownList dd = (DropDownList)Gridview1.Rows[selRowIndex].FindControl("ddcukaioth");
        String tgst_1 = (Gridview1.Rows[selRowIndex].FindControl("ddcukaioth") as DropDownList).SelectedItem.Value;
        Label tax1 = (Label)Gridview1.FooterRow.FindControl("Label4");
        Label tax1_cur = (Label)Gridview1.Rows[selRowIndex].FindControl("Label8");

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
                GrandTotal2();
            }
            else
            {
                decimal tot1 = 0;
                if (tOt.Text != "")
                {
                    tot1 = Convert.ToDecimal(tOt.Text);
                }

                int tgst1 = Convert.ToInt32(tgst);

                numTotal1 = tot1 * tgst1 / 100;
                txt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                gmt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                numTotal2 = tot1 + numTotal1;
                txt1.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                tab1();
                GrandTotal2();
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
            }

            txt.Text = "0.00";
            gmt.Text = "0.00";
            tab1();
            GrandTotal2();
        }
    }



    protected void ddrescuk_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal numTotal1 = 0;
        decimal numTotal2 = 0;
        decimal numTotal3 = 0;
        decimal numTotal4 = 0;

        int selRowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
        Label tOt = (Label)Gridview4.Rows[selRowIndex].FindControl("Label3");
        TextBox qty = (TextBox)Gridview4.Rows[selRowIndex].FindControl("TextBox14");
        TextBox unit = (TextBox)Gridview4.Rows[selRowIndex].FindControl("TextBox13");
        TextBox dis = (TextBox)Gridview4.Rows[selRowIndex].FindControl("Txtdisres");
        Label gamt = (Label)Gridview4.Rows[selRowIndex].FindControl("Label8");
        Label gmt = (Label)Gridview4.Rows[selRowIndex].FindControl("Label10");
        Label txt = (Label)Gridview4.FooterRow.FindControl("Label2");
        Label txt1 = (Label)Gridview4.Rows[selRowIndex].FindControl("Label5");
        Label tax1 = (Label)Gridview4.FooterRow.FindControl("Label7");
        DropDownList dd = (DropDownList)Gridview4.Rows[selRowIndex].FindControl("ddresoth");
        CheckBox chk = (CheckBox)Gridview4.Rows[selRowIndex].FindControl("chkres");
        String tgst_1 = (Gridview4.Rows[selRowIndex].FindControl("ddrescuk") as DropDownList).SelectedItem.Value;
        if (qty.Text != "" && unit.Text != "")
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
                    txt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                    gamt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                    numTotal2 = Convert.ToDecimal(tOt.Text) - numTotal4;
                    numTotal3 = Convert.ToDecimal(txt1.Text);
                    tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                    txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                    tab2();
                    dd.Enabled = true;
                    //dd.Attributes.Remove("style");
                    dd.Attributes.Remove("readonly");
                    GrandTotalres();
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
                    tab2();
                    GrandTotalres();
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
                    numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(gmt.Text);
                    tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                    txt1.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                }
                else
                {
                    numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) + Convert.ToDecimal(gmt.Text);
                    numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text);
                    tOt.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                    txt1.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                }

                txt.Text = "0.00";
                gamt.Text = "0.00";
                tab2();
                GrandTotalres();
            }
        }
    }

    protected void ddrescukoth_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal numTotal1 = 0;
        decimal numTotal2 = 0;
        decimal numTotal3 = 0;
        decimal numTotal4 = 0;
        int selRowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
        Label tOt = (Label)Gridview4.Rows[selRowIndex].FindControl("Label3");
        Label txt = (Label)Gridview4.FooterRow.FindControl("Label7");
        TextBox qty = (TextBox)Gridview4.Rows[selRowIndex].FindControl("TextBox14");
        TextBox unit = (TextBox)Gridview4.Rows[selRowIndex].FindControl("TextBox13");
        TextBox dis = (TextBox)Gridview4.Rows[selRowIndex].FindControl("Txtdisres");
        Label gmt = (Label)Gridview4.Rows[selRowIndex].FindControl("Label10");
        Label txt1 = (Label)Gridview4.Rows[selRowIndex].FindControl("Label5");
        Label tax1 = (Label)Gridview4.FooterRow.FindControl("Label2");
        CheckBox chk = (CheckBox)Gridview4.Rows[selRowIndex].FindControl("chkres");
        DropDownList dd = (DropDownList)Gridview4.Rows[selRowIndex].FindControl("ddresoth");
        String tgst_1 = (Gridview4.Rows[selRowIndex].FindControl("ddresoth") as DropDownList).SelectedItem.Value;

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
                tab2();
                GrandTotalres2();
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
                tab2();
                GrandTotalres2();
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
                numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1.Text);
                numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1.Text);
                tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                //txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
            }
            else
            {
                numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1.Text);
                numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1.Text);
                tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                //txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
            }

            txt.Text = "0.00";
            gmt.Text = "0.00";
            tab2();
            GrandTotalres2();
        }
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
        if (tOt.Text != "")
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
        //else
        //{
        //    tOt.Focus();
        //}
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
        if (tOt.Text != "")
        {
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
                    tab4();

                    dd.Enabled = true;
                    //dd.Attributes.Remove("style");
                    dd.Attributes.Remove("readonly");
                    GrandTotalded();
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


    protected void ChckedChangedInv(object sender, EventArgs e)
    {

        int selRowIndex1 = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
        CheckBox cb1 = (CheckBox)Gridview1.Rows[selRowIndex1].FindControl("CheckBox1");
        DropDownList db = (DropDownList)Gridview1.Rows[selRowIndex1].FindControl("ddcukaioth");
        DropDownList db1 = (DropDownList)Gridview1.Rows[selRowIndex1].FindControl("ddcukaiinv");


        tab1();
        if (cb1.Checked)
        {
            //Perform your logic

            db.Enabled = false;
            db1.Enabled = true;
            //db1.Attributes.Remove("style");
            db1.Attributes.Remove("readonly");
            //db.Attributes.Add("style", "pointer-events:None;");
            db.Attributes.Add("readonly", "readonly");
        }
        else
        {
            //db.Attributes.Remove("style");
            db.Attributes.Remove("readonly");
            //db1.Attributes.Remove("style");
            db1.Attributes.Remove("readonly");
            db.Enabled = true;
            db1.Enabled = true;
        }

    }

    protected void ChckedChangedres(object sender, EventArgs e)
    {

        int selRowIndex1 = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
        CheckBox cb1 = (CheckBox)Gridview4.Rows[selRowIndex1].FindControl("chkres");
        DropDownList db = (DropDownList)Gridview4.Rows[selRowIndex1].FindControl("ddresoth");
        DropDownList db1 = (DropDownList)Gridview4.Rows[selRowIndex1].FindControl("ddrescuk");

        tab2();
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
            db.Attributes.Add("readonly", "readonly");
            //db.Attributes.Remove("style");
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

    void akaun()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select kod_syarikat,nama_syarikat from KW_Profile_syarikat Where Status='A' and cur_sts='1'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddakaun.DataSource = dt;
            ddakaun.DataTextField = "nama_syarikat";
            ddakaun.DataValueField = "kod_syarikat";
            ddakaun.DataBind();
            ddakaun.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void bank()
    {


        DataSet Ds = new DataSet();
        try
        {
            string com = "select (Ref_kod_akaun + ' | '+ REf_nama_akaun) nama_akaun,Ref_kod_akaun from KW_Ref_Akaun_bank";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddaka.DataSource = dt;
            ddaka.DataTextField = "nama_akaun";
            ddaka.DataValueField = "Ref_kod_akaun";
            ddaka.DataBind();
            ddaka.Items.Insert(0, new ListItem("--- PILIH ---", ""));

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
            string com = "select Ref_kod_akaun,Ref_nama_syarikat from  KW_Ref_Pelanggan";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddpela.DataSource = dt;
            ddpela.DataTextField = "Ref_nama_syarikat";
            ddpela.DataValueField = "Ref_kod_akaun";
            ddpela.DataBind();
            ddpela.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            ddpela1.DataSource = dt;
            ddpela1.DataTextField = "Ref_nama_syarikat";
            ddpela1.DataValueField = "Ref_kod_akaun";
            ddpela1.DataBind();
            ddpela1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
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
            ddnamapela3.DataSource = dt;
            ddnamapela3.DataTextField = "Ref_nama_syarikat";
            ddnamapela3.DataValueField = "Ref_kod_akaun";
            ddnamapela3.DataBind();
            ddnamapela3.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }


    void pelanggan1()
    {
        get_roleakaun();
        DataSet Ds = new DataSet();
        try
        {
            string com = get_grid;
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddnamapela3.DataSource = dt;
            ddnamapela3.DataTextField = "nama";
            ddnamapela3.DataValueField = "akaun";
            ddnamapela3.DataBind();
            ddnamapela3.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void sel_akauns(object sender, EventArgs e)
    {
        Bind_resit();
    }

    void Bind_resit()
    {
        if (dd_selvalue.SelectedItem.Text == "PELANGGAN")
        {
            tab2();
            txtname.Text = "";
            resit_tab21.Visible = true;
            Label12.Text = "";
            try
            {
                string com = "select Ref_kod_akaun,Ref_nama_syarikat from KW_Ref_Pelanggan ";
                SqlDataAdapter adpt = new SqlDataAdapter(com, con);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                ddnamapela3.DataSource = dt;
                ddnamapela3.DataTextField = "Ref_nama_syarikat";
                ddnamapela3.DataValueField = "Ref_kod_akaun";
                ddnamapela3.DataBind();
                ddnamapela3.Items.Insert(0, new ListItem("--- PILIH ---", ""));


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        else if (dd_selvalue.SelectedItem.Text == "SEMUA COA")
        {
            tab2();
            txtname.Text = "";
            resit_tab21.Visible = false;
            Label12.Text = "Terima Daripada";
            try
            {
                string com = "select (kod_akaun + ' | '+ nama_akaun) nama_akaun,kod_akaun   from KW_Ref_Carta_Akaun where jenis_akaun_type != '1' and Status='A'";
                SqlDataAdapter adpt = new SqlDataAdapter(com, con);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                ddnamapela3.DataSource = dt;
                ddnamapela3.DataTextField = "nama_akaun";
                ddnamapela3.DataValueField = "kod_akaun";
                ddnamapela3.DataBind();
                ddnamapela3.Items.Insert(0, new ListItem("--- PILIH ---", ""));


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    void get_roleakaun()
    {
        if (dd_selvalue.SelectedValue == "0")
        {
            get_grid = "select kod_akaun as akaun,nama_akaun as nama from KW_Ref_Carta_Akaun where jenis_akaun_type != '1' and Status='A' order by kod_akaun";
        }
        else if (dd_selvalue.SelectedValue == "1")
        {
            get_grid = "select Ref_kod_akaun as akaun,Ref_nama_syarikat as nama from  KW_Ref_Pelanggan";
        }
        else
        {
            get_grid = "select Ref_kod_akaun as akaun,Ref_nama_syarikat as nama from  KW_Ref_Pelanggan";
        }

    }

    void invois_view()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = " select no_invois from KW_Penerimaan_invois group by no_invois";
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

    void Bentuk1()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select Jenis_bayaran_cd,Jenis_bayaran from KW_Jenis_Cara_bayaran";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);

            dd_Betuk1.DataSource = dt;
            dd_Betuk1.DataTextField = "Jenis_bayaran";
            dd_Betuk1.DataValueField = "Jenis_bayaran_cd";
            dd_Betuk1.DataBind();
            dd_Betuk1.Items.Insert(0, new ListItem("--- PILIH ---", ""));





        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    void Bentuk()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = " select Jenis_bayaran_cd,Jenis_bayaran from KW_Jenis_Cara_bayaran";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);


            ddaka.DataSource = dt;
            ddaka.DataTextField = "Jenis_bayaran";
            ddaka.DataValueField = "Jenis_bayaran_cd";
            ddaka.DataBind();
            ddaka.Items.Insert(0, new ListItem("--- PILIH ---", ""));



        }
        catch (Exception ex)
        {
            throw ex;
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



    protected void Button1_Click(object sender, EventArgs e)
    {
        Div7.Visible = false;
        Div9.Visible = true;
        Bindinvois();
        Bindresit();
        Bindcredit();
        Binddedit();
        tab1();
        reset();
    }

    protected void Button7_Click(object sender, EventArgs e)
    {
        Div10.Visible = false;
        Div11.Visible = true;
        Bindinvois();
        Bindresit();
        Bindcredit();
        Binddedit();
        tab2();
        reset();
    }
    protected void Button16_Click(object sender, EventArgs e)
    {
        th2.Visible = false;
        th1.Visible = true;
        Bindinvois();
        Bindresit();
        Bindcredit();
        Binddedit();
        tab3();
        Gridview9.Visible = false;
        reset();
        lnk_clk_crt();
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        fr2.Visible = false;
        fr1.Visible = true;
        Bindinvois();
        Bindresit();
        Bindcredit();
        Binddedit();
        tab4();
        reset();
        //lnk_clk_Deb();
        Session["dbtno1"] = "";
    }
    void tab1()
    {

        p6.Attributes.Add("class", "tab-pane active");
        p1.Attributes.Add("class", "tab-pane");
        p2.Attributes.Add("class", "tab-pane");
        p3.Attributes.Add("class", "tab-pane");

        pp6.Attributes.Add("class", "active");
        pp1.Attributes.Remove("class");
        pp2.Attributes.Remove("class");
        pp3.Attributes.Remove("class");
        hd_txt.Text = "INVOIS";

    }
    void tab2()
    {

        p6.Attributes.Add("class", "tab-pane");
        p1.Attributes.Add("class", "tab-pane active");
        p2.Attributes.Add("class", "tab-pane");
        p3.Attributes.Add("class", "tab-pane");

        pp6.Attributes.Remove("class");
        pp1.Attributes.Add("class", "active");
        pp2.Attributes.Remove("class");
        pp3.Attributes.Remove("class");
        hd_txt.Text = "RESIT";
    }
    void tab3()
    {
        p6.Attributes.Add("class", "tab-pane");
        p1.Attributes.Add("class", "tab-pane");
        p2.Attributes.Add("class", "tab-pane active");
        p3.Attributes.Add("class", "tab-pane");

        pp6.Attributes.Remove("class");
        pp1.Attributes.Remove("class");
        pp2.Attributes.Add("class", "active");
        pp3.Attributes.Remove("class");
        hd_txt.Text = "NOTA KREDIT";
    }

    void tab4()
    {
        p6.Attributes.Add("class", "tab-pane");
        p1.Attributes.Add("class", "tab-pane");
        p2.Attributes.Add("class", "tab-pane");
        p3.Attributes.Add("class", "tab-pane active");

        pp6.Attributes.Remove("class");
        pp1.Attributes.Remove("class");
        pp2.Attributes.Remove("class");
        pp3.Attributes.Add("class", "active");
        hd_txt.Text = "NOTA DEBIT";
    }





    protected void BindKoff1()
    {

        qry1 = "select no_invois,Format(tarikh_invois,'dd/MM/yyyy')tarikh_invois,nama_pelanggan,p.Ref_nama_syarikat,Format(jumlah,'#,##,###.00') jumlah,'' Status from kw_penerimaan_invois  I  left join KW_Ref_Pelanggan P on p.Ref_no_syarikat=I.nama_pelanggan_code";
        SqlCommand cmd2 = new SqlCommand("" + qry1 + "", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            Gridview13.DataSource = ds2;
            Gridview13.DataBind();
            int columncount = Gridview13.Rows[0].Cells.Count;
            Gridview13.Rows[0].Cells.Clear();
            Gridview13.Rows[0].Cells.Add(new TableCell());
            Gridview13.Rows[0].Cells[0].ColumnSpan = columncount;
            Gridview13.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
        }
        else
        {
            Gridview13.DataSource = ds2;
            Gridview13.DataBind();
        }

    }


    protected void gvSelected_PageIndexChanging_g3(object sender, GridViewPageEventArgs e)
    {
        Gridview2.PageIndex = e.NewPageIndex;
        Gridview2.DataBind();
        Bindinvois();
        Bindresit();
        Binddedit();
        Bindcredit();
        tab1();
    }
    protected void Bindinvois()
    {
        string fmdate = string.Empty;
        if (TextBox10.Text != "")
        {
            string fdate = TextBox10.Text;
            DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            fmdate = fd.ToString("yyyy-MM-dd") + " 12:00:00.000";
        }

        string smb_clk1 = string.Empty;
        if (chk_invois.Checked == true)
        {

            smb_clk1 = "";

        }
        else
        {
            if (ddpela.SelectedItem.Text == "--- PILIH ---" && TextBox7.Text == "" && TextBox10.Text == "")
            {
                smb_clk1 = "where I.tarikh_invois>=DATEADD(day, DATEDIFF(day, 0, '"+ str_sdt1 + "'), 0) and I.tarikh_invois<=DATEADD(day, DATEDIFF(day, 0, '" + end_edt1 + "'), +1)";
            }
            else
            {
                smb_clk1 = "and I.tarikh_invois>=DATEADD(day, DATEDIFF(day, 0, '" + str_sdt1 + "'), 0) and I.tarikh_invois<=DATEADD(day, DATEDIFF(day, 0, '" + end_edt1 + "'), +1)";
            }
        }

        if (ddpela.SelectedItem.Text == "--- PILIH ---" && TextBox7.Text != "" && TextBox10.Text == "")
        {
            qry1 = "select I.no_invois,FORMAT(I.tarikh_invois,'dd/MM/yyyy') tarikh_invois,case when FORMAT(max(p.tarikh_Payment),'dd/MM/yyyy') = '01/01/1900' then '' else FORMAT(max(p.tarikh_Payment),'dd/MM/yyyy') end as tarikh,FORMAT(I.FINAL_DATE,'dd/MM/yyyy') FINAL_DATE,I.nama_pelanggan,k.Ref_nama_syarikat,I.jumlah as jumlah,ISNULL(sum(p.jumlah),'0.00') as  jumlah_bayaran, ISNULL((I.jumlah - sum(P.jumlah)),'0.00') as Baki from KW_Penerimaan_invois I left join  KW_Penerimaan_payment p on p.no_invois=I.no_invois left join KW_Ref_Pelanggan k on k.Ref_no_syarikat=I.nama_pelanggan_code  where I.no_invois='" + TextBox7.Text + "' "+ smb_clk1 + " group by I.no_invois,I.tarikh_invois,I.nama_pelanggan,Ref_nama_syarikat,I.jumlah,I.FINAL_DATE";
        }
        else if (ddpela.SelectedItem.Text == "--- PILIH ---" && TextBox7.Text == "" && TextBox10.Text != "")
        {
            qry1 = "select I.no_invois,FORMAT(I.tarikh_invois,'dd/MM/yyyy') tarikh_invois,case when FORMAT(max(p.tarikh_Payment),'dd/MM/yyyy') = '01/01/1900' then '' else FORMAT(max(p.tarikh_Payment),'dd/MM/yyyy') end as tarikh,FORMAT(I.FINAL_DATE,'dd/MM/yyyy') FINAL_DATE,I.nama_pelanggan,k.Ref_nama_syarikat,I.jumlah as jumlah,ISNULL(sum(p.jumlah),'0.00') as  jumlah_bayaran, ISNULL((I.jumlah - sum(P.jumlah)),'0.00') as Baki from KW_Penerimaan_invois I left join  KW_Penerimaan_payment p on p.no_invois=I.no_invois left join KW_Ref_Pelanggan k on k.Ref_no_syarikat=I.nama_pelanggan_code  where I.tarikh_invois='" + fmdate + "' " + smb_clk1 + " group by I.no_invois,I.tarikh_invois,I.nama_pelanggan,Ref_nama_syarikat,I.jumlah,I.FINAL_DATE";
        }
        else if (ddpela.SelectedItem.Text != "--- PILIH ---" && TextBox7.Text == "" && TextBox10.Text == "")
        {
            qry1 = "select I.no_invois,FORMAT(I.tarikh_invois,'dd/MM/yyyy') tarikh_invois,case when FORMAT(max(p.tarikh_Payment),'dd/MM/yyyy') = '01/01/1900' then '' else FORMAT(max(p.tarikh_Payment),'dd/MM/yyyy') end as tarikh,FORMAT(I.FINAL_DATE,'dd/MM/yyyy') FINAL_DATE,I.nama_pelanggan,k.Ref_nama_syarikat,I.jumlah as jumlah,ISNULL(sum(p.jumlah),'0.00') as  jumlah_bayaran, ISNULL((I.jumlah - sum(P.jumlah)),'0.00') as Baki from KW_Penerimaan_invois I left join  KW_Penerimaan_payment p on p.no_invois=I.no_invois left join KW_Ref_Pelanggan k on k.Ref_no_syarikat=I.nama_pelanggan_code  where k.Ref_nama_syarikat='" + ddpela.SelectedItem.Text + "' " + smb_clk1 + " group by I.no_invois,I.tarikh_invois,I.nama_pelanggan,Ref_nama_syarikat,I.jumlah,I.FINAL_DATE";
        }
        else if (ddpela.SelectedItem.Text != "--- PILIH ---" && TextBox7.Text != "" && TextBox10.Text == "")
        {
            qry1 = "select I.no_invois,FORMAT(I.tarikh_invois,'dd/MM/yyyy') tarikh_invois,case when FORMAT(max(p.tarikh_Payment),'dd/MM/yyyy') = '01/01/1900' then '' else FORMAT(max(p.tarikh_Payment),'dd/MM/yyyy') end as tarikh,FORMAT(I.FINAL_DATE,'dd/MM/yyyy') FINAL_DATE,I.nama_pelanggan,k.Ref_nama_syarikat,I.jumlah as jumlah,ISNULL(sum(p.jumlah),'0.00') as  jumlah_bayaran, ISNULL((I.jumlah - sum(P.jumlah)),'0.00') as Baki from KW_Penerimaan_invois I left join  KW_Penerimaan_payment p on p.no_invois=I.no_invois left join KW_Ref_Pelanggan k on k.Ref_no_syarikat=I.nama_pelanggan_code  where k.Ref_nama_syarikat='" + ddpela.SelectedItem.Text + "' and I.no_invois='" + TextBox7.Text + "' " + smb_clk1 + " group by I.no_invois,I.tarikh_invois,I.nama_pelanggan,Ref_nama_syarikat,I.jumlah,I.FINAL_DATE";
        }
        else if (ddpela.SelectedItem.Text != "--- PILIH ---" && TextBox7.Text != "" && TextBox10.Text != "")
        {
            qry1 = "select I.no_invois,FORMAT(I.tarikh_invois,'dd/MM/yyyy') tarikh_invois,case when FORMAT(max(p.tarikh_Payment),'dd/MM/yyyy') = '01/01/1900' then '' else FORMAT(max(p.tarikh_Payment),'dd/MM/yyyy') end as tarikh,FORMAT(I.FINAL_DATE,'dd/MM/yyyy') FINAL_DATE,I.nama_pelanggan,k.Ref_nama_syarikat,I.jumlah as jumlah,ISNULL(sum(p.jumlah),'0.00') as  jumlah_bayaran, ISNULL((I.jumlah - sum(P.jumlah)),'0.00') as Baki from KW_Penerimaan_invois I left join  KW_Penerimaan_payment p on p.no_invois=I.no_invois left join KW_Ref_Pelanggan k on k.Ref_no_syarikat=I.nama_pelanggan_code  where k.Ref_nama_syarikat='" + ddpela.SelectedItem.Text + "' and I.no_invois='" + TextBox7.Text + "' and  I.tarikh_invois='" + fmdate + "' " + smb_clk1 + " group by I.no_invois,I.tarikh_invois,I.nama_pelanggan,Ref_nama_syarikat,I.jumlah,I.FINAL_DATE";
        }
        else if (ddpela.SelectedItem.Text != "--- PILIH ---" && TextBox7.Text == "" && TextBox10.Text != "")
        {
            qry1 = "select I.no_invois,FORMAT(I.tarikh_invois,'dd/MM/yyyy') tarikh_invois,case when FORMAT(max(p.tarikh_Payment),'dd/MM/yyyy') = '01/01/1900' then '' else FORMAT(max(p.tarikh_Payment),'dd/MM/yyyy') end as tarikh,FORMAT(I.FINAL_DATE,'dd/MM/yyyy') FINAL_DATE,I.nama_pelanggan,k.Ref_nama_syarikat,I.jumlah as jumlah,ISNULL(sum(p.jumlah),'0.00') as  jumlah_bayaran, ISNULL((I.jumlah - sum(P.jumlah)),'0.00') as Baki from KW_Penerimaan_invois I left join  KW_Penerimaan_payment p on p.no_invois=I.no_invois left join KW_Ref_Pelanggan k on k.Ref_no_syarikat=I.nama_pelanggan_code  where k.Ref_nama_syarikat='" + ddpela.SelectedItem.Text + "' and  I.tarikh_invois='" + fmdate + "' " + smb_clk1 + " group by I.no_invois,I.tarikh_invois,I.nama_pelanggan,Ref_nama_syarikat,I.jumlah,I.FINAL_DATE";
        }
        else
        {
            qry1 = "select I.no_invois,FORMAT(I.tarikh_invois,'dd/MM/yyyy') tarikh_invois,case when FORMAT(max(p.tarikh_Payment),'dd/MM/yyyy') = '01/01/1900' then '' else FORMAT(max(p.tarikh_Payment),'dd/MM/yyyy') end as tarikh,FORMAT(I.FINAL_DATE,'dd/MM/yyyy') FINAL_DATE,I.nama_pelanggan,k.Ref_nama_syarikat,I.jumlah as jumlah,ISNULL(sum(p.jumlah),'0.00') as  jumlah_bayaran, ISNULL((I.jumlah - sum(P.jumlah)),'0.00') as Baki from KW_Penerimaan_invois I left join  KW_Penerimaan_payment p on p.no_invois=I.no_invois left join KW_Ref_Pelanggan k on k.Ref_no_syarikat=I.nama_pelanggan_code " + smb_clk1 + " group by I.no_invois,I.tarikh_invois,I.nama_pelanggan,Ref_nama_syarikat,I.jumlah,I.FINAL_DATE";
        }
        SqlCommand cmd2 = new SqlCommand("" + qry1 + "", con);
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
    protected void but_Click(object sender, EventArgs e)
    {
        Bindinvois();
        Bindresit();
        Bindcredit();
        Binddedit();
        tab1();
        string script = " $(function () {$('.select2').select2()})";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
    }

    protected void gvSelected_PageIndexChanging_g5(object sender, GridViewPageEventArgs e)
    {
        Gridview5.PageIndex = e.NewPageIndex;
        Gridview5.DataBind();
        Bindinvois();
        Bindresit();
        Bindcredit();
        Binddedit();
        tab2();
    }
    protected void Bindresit()
    {
        string fmdate = string.Empty;
        if (TextBox15.Text != "")
        {
            string fdate = TextBox15.Text;
            DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            fmdate = fd.ToString("yyyy-MM-dd") + " 12:00:00.000";
        }

        string smb_clk2 = string.Empty;
        if (chk_resit.Checked == true)
        {

            smb_clk2 = "";

        }
        else
        {
            if (TextBox17.Text == "" && TextBox15.Text == "")
            {
                smb_clk2 = "where tarikh_resit>=DATEADD(day, DATEDIFF(day, 0, '" + str_sdt1 + "'), 0) and tarikh_resit<=DATEADD(day, DATEDIFF(day, 0, '" + end_edt1 + "'), +1)";
            }
            else
            {
                smb_clk2 = "and tarikh_resit>=DATEADD(day, DATEDIFF(day, 0, '" + str_sdt1 + "'), 0) and tarikh_resit<=DATEADD(day, DATEDIFF(day, 0, '" + end_edt1 + "'), +1)";
            }
        }

        if (TextBox17.Text != "" && TextBox15.Text == "")
        {
            qry1 = "select no_resit,FORMAT(tarikh_resit,'dd/MM/yyyy') tarikh_resit,format(jumlah_bayaran,'#,##,###.00') jumlah_bayaran   from kw_penerimaan_resit j  where j.no_resit='" + TextBox17.Text + "' "+ smb_clk2 + "";
        }
        else if (TextBox17.Text != "" && TextBox15.Text != "")
        {
            qry1 = "select no_resit,FORMAT(tarikh_resit,'dd/MM/yyyy') tarikh_resit,format(jumlah_bayaran,'#,##,###.00') jumlah_bayaran   from kw_penerimaan_resit j  where j.no_resit='" + TextBox17.Text + "'  and j.tarikh_resit='" + fmdate + "' " + smb_clk2 + "";
        }
        else if (TextBox17.Text == "" && TextBox15.Text != "")
        {
            qry1 = "select no_resit,FORMAT(tarikh_resit,'dd/MM/yyyy') tarikh_resit,format(jumlah_bayaran,'#,##,###.00') jumlah_bayaran   from kw_penerimaan_resit j   where j.tarikh_resit='" + fmdate + "' " + smb_clk2 + "";
        }
        else
        {
            qry1 = "select no_resit,FORMAT(tarikh_resit,'dd/MM/yyyy') tarikh_resit,format(jumlah_bayaran,'#,##,###.00') jumlah_bayaran   from kw_penerimaan_resit j  " + smb_clk2 + "";
        }
        SqlCommand cmd2 = new SqlCommand("" + qry1 + "", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            Gridview5.DataSource = ds2;
            Gridview5.DataBind();
            int columncount = Gridview5.Rows[0].Cells.Count;
            Gridview5.Rows[0].Cells.Clear();
            Gridview5.Rows[0].Cells.Add(new TableCell());
            Gridview5.Rows[0].Cells[0].ColumnSpan = columncount;
            Gridview5.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
        }
        else
        {
            Gridview5.DataSource = ds2;
            Gridview5.DataBind();
        }
    }

    protected void Button11_Click(object sender, EventArgs e)
    {

        Bindinvois();
        Bindresit();
        Bindcredit();
        Binddedit();
        tab2();
    }

    protected void gvSelected_PageIndexChanging_g6(object sender, GridViewPageEventArgs e)
    {
        Gridview6.PageIndex = e.NewPageIndex;
        Gridview6.DataBind();
        Bindinvois();
        Bindresit();
        Bindcredit();
        Binddedit();
        tab3();
    }
    protected void Bindcredit()
    {
        string fmdate_kr = string.Empty;
        if (txtctarikh.Text != "")
        {
            string fdate_kr = txtctarikh.Text;
            DateTime fd = DateTime.ParseExact(fdate_kr, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            fmdate_kr = fd.ToString("yyyy-MM-dd") + " 12:00:00.000";
        }

        string smb_clk3 = string.Empty;
        if (chk_kredit.Checked == true)
        {

            smb_clk3 = "";

        }
        else
        {
            if (txtcno.Text == "" && txtctarikh.Text == "" && txtnpembe.Text == "")
            {
                smb_clk3 = "where tarikh_invois>=DATEADD(day, DATEDIFF(day, 0, '" + str_sdt1 + "'), 0) and tarikh_invois<=DATEADD(day, DATEDIFF(day, 0, '" + end_edt1 + "'), +1)";
            }
            else
            {
                smb_clk3 = "and tarikh_invois>=DATEADD(day, DATEDIFF(day, 0, '" + str_sdt1 + "'), 0) and tarikh_invois<=DATEADD(day, DATEDIFF(day, 0, '" + end_edt1 + "'), +1)";
            }
        }

        if (txtcno.Text != "" && txtctarikh.Text == "" && txtnpembe.Text == "")
        {
            qry1 = "select no_Rujukan,Format(tarikh_invois,'dd/MM/yyyy') tarikh_invois,nama_pelanggan,Ref_nama_syarikat,isnull(Format(sum(overall),'#,##,###.00'),0.00)  jumlah from KW_Penerimaan_Credit_item c left join KW_Ref_Pelanggan p on p.Ref_kod_akaun=c.nama_pelanggan where no_Rujukan='" + txtcno.Text + "' "+ smb_clk3 + " group by no_Rujukan,tarikh_invois,nama_pelanggan,Ref_nama_syarikat";
        }
        else if (txtcno.Text != "" && txtctarikh.Text != "" && txtnpembe.Text == "")
        {
            qry1 = "select no_Rujukan,Format(tarikh_invois,'dd/MM/yyyy') tarikh_invois,nama_pelanggan,Ref_nama_syarikat,isnull(Format(sum(overall),'#,##,###.00'),0.00)  jumlah from KW_Penerimaan_Credit_item c left join KW_Ref_Pelanggan p on p.Ref_kod_akaun=c.nama_pelanggan where no_Rujukan='" + txtcno.Text + "' and tarikh_invois='" + fmdate_kr + "' " + smb_clk3 + " group by no_Rujukan,tarikh_invois,nama_pelanggan,Ref_nama_syarikat";
        }
        else if (txtcno.Text == "" && txtctarikh.Text != "" && txtnpembe.Text != "")
        {
            qry1 = "select no_Rujukan,Format(tarikh_invois,'dd/MM/yyyy') tarikh_invois,nama_pelanggan,Ref_nama_syarikat,isnull(Format(sum(overall),'#,##,###.00'),0.00)  jumlah from KW_Penerimaan_Credit_item c left join KW_Ref_Pelanggan p on p.Ref_kod_akaun=c.nama_pelanggan where tarikh_invois='" + fmdate_kr + "' and Ref_nama_syarikat='" + txtnpembe.Text + "' " + smb_clk3 + " group by no_Rujukan,tarikh_invois,nama_pelanggan,Ref_nama_syarikat";
        }
        else if (txtcno.Text == "" && txtctarikh.Text != "" && txtnpembe.Text == "")
        {
            qry1 = "select no_Rujukan,Format(tarikh_invois,'dd/MM/yyyy') tarikh_invois,nama_pelanggan,Ref_nama_syarikat,isnull(Format(sum(overall),'#,##,###.00'),0.00)  jumlah from KW_Penerimaan_Credit_item c left join KW_Ref_Pelanggan p on p.Ref_kod_akaun=c.nama_pelanggan where tarikh_invois='" + fmdate_kr + "' " + smb_clk3 + " group by no_Rujukan,tarikh_invois,nama_pelanggan,Ref_nama_syarikat";
        }
        else if (txtcno.Text == "" && txtctarikh.Text == "" && txtnpembe.Text != "")
        {
            qry1 = "select no_Rujukan,Format(tarikh_invois,'dd/MM/yyyy') tarikh_invois,nama_pelanggan,Ref_nama_syarikat,isnull(Format(sum(overall),'#,##,###.00'),0.00)  jumlah from KW_Penerimaan_Credit_item c left join KW_Ref_Pelanggan p on p.Ref_kod_akaun=c.nama_pelanggan where Ref_nama_syarikat='" + txtnpembe.Text + "' " + smb_clk3 + " group by no_Rujukan,tarikh_invois,nama_pelanggan,Ref_nama_syarikat";
        }
        else if (txtcno.Text != "" && txtctarikh.Text != "" && txtnpembe.Text != "")
        {
            qry1 = "select no_Rujukan,Format(tarikh_invois,'dd/MM/yyyy') tarikh_invois,nama_pelanggan,Ref_nama_syarikat,isnull(Format(sum(overall),'#,##,###.00'),0.00)  jumlah from KW_Penerimaan_Credit_item c left join KW_Ref_Pelanggan p on p.Ref_kod_akaun=c.nama_pelanggan where no_Rujukan='" + txtcno.Text + "' and tarikh_invois='" + fmdate_kr + "' and Ref_nama_syarikat='" + txtnpembe.Text + "' " + smb_clk3 + " group by no_Rujukan,tarikh_invois,nama_pelanggan,Ref_nama_syarikat";
        }
        else
        {
            qry1 = "select no_Rujukan,Format(tarikh_invois,'dd/MM/yyyy') tarikh_invois,nama_pelanggan,Ref_nama_syarikat,isnull(Format(sum(overall),'#,##,###.00'),0.00)  jumlah from KW_Penerimaan_Credit_item c left join KW_Ref_Pelanggan p on p.Ref_kod_akaun=c.nama_pelanggan " + smb_clk3 + " group by no_Rujukan,tarikh_invois,nama_pelanggan,Ref_nama_syarikat";

        }
        SqlCommand cmd2 = new SqlCommand("" + qry1 + "", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            Gridview6.DataSource = ds2;
            Gridview6.DataBind();
            int columncount = Gridview6.Rows[0].Cells.Count;
            Gridview6.Rows[0].Cells.Clear();
            Gridview6.Rows[0].Cells.Add(new TableCell());
            Gridview6.Rows[0].Cells[0].ColumnSpan = columncount;
            Gridview6.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
        }
        else
        {
            Gridview6.DataSource = ds2;
            Gridview6.DataBind();
        }
    }

    protected void Button12_Click(object sender, EventArgs e)
    {
        Bindinvois();
        Bindresit();
        Bindcredit();
        Binddedit();
        tab3();
    }


    protected void gvSelected_PageIndexChanging_g8(object sender, GridViewPageEventArgs e)
    {
        Gridview8.PageIndex = e.NewPageIndex;
        Gridview8.DataBind();
        Bindinvois();
        Bindresit();
        Bindcredit();
        Binddedit();
        tab4();
    }
    protected void Binddedit()
    {
        string fmdate_de = string.Empty;
        if (TextBox20.Text != "")
        {
            string fdate_de = TextBox20.Text;
            DateTime fd = DateTime.ParseExact(fdate_de, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            fmdate_de = fd.ToString("yyyy-MM-dd") + " 12:00:00.000";
        }

        string smb_clk4 = string.Empty;
        if (chk_debit.Checked == true)
        {

            smb_clk4 = "";

        }
        else
        {
            if (TextBox13.Text == "" && TextBox20.Text == "" && TextBox21.Text == "")
            {
                smb_clk4 = "where tarikh_invois>=DATEADD(day, DATEDIFF(day, 0, '" + str_sdt1 + "'), 0) and tarikh_invois<=DATEADD(day, DATEDIFF(day, 0, '" + end_edt1 + "'), +1)";
            }
            else
            {
                smb_clk4 = "and tarikh_invois>=DATEADD(day, DATEDIFF(day, 0, '" + str_sdt1 + "'), 0) and tarikh_invois<=DATEADD(day, DATEDIFF(day, 0, '" + end_edt1 + "'), +1)";
            }
        }

        if (TextBox13.Text != "" && TextBox20.Text == "" && TextBox21.Text == "")
        {
            qry1 = "select no_rujukan,Format(tarikh_invois,'dd/MM/yyyy') tarikh_invois,nama_pelanggan,Ref_nama_syarikat,isnull(Format(sum(overall),'#,##,###.00'),0.00) jumlah  from KW_Penerimaan_Debit_item d left join KW_Ref_Pelanggan  p on p.Ref_kod_akaun=d.nama_pelanggan where no_Rujukan='" + TextBox13.Text + "' "+ smb_clk4 + " group by no_rujukan, tarikh_invois,nama_pelanggan,Ref_nama_syarikat ";
        }
        else if (TextBox13.Text == "" && TextBox20.Text != "" && TextBox21.Text == "")
        {
            qry1 = "select no_rujukan,Format(tarikh_invois,'dd/MM/yyyy') tarikh_invois,nama_pelanggan,Ref_nama_syarikat,isnull(Format(sum(overall),'#,##,###.00'),0.00) jumlah  from KW_Penerimaan_Debit_item d left join KW_Ref_Pelanggan  p on p.Ref_kod_akaun=d.nama_pelanggan where tarikh_invois='" + fmdate_de + "' " + smb_clk4 + " group by no_rujukan, tarikh_invois,nama_pelanggan,Ref_nama_syarikat";
        }
        else if (TextBox13.Text == "" && TextBox20.Text == "" && TextBox21.Text != "")
        {
            qry1 = "select no_rujukan,Format(tarikh_invois,'dd/MM/yyyy') tarikh_invois,nama_pelanggan,Ref_nama_syarikat,isnull(Format(sum(overall),'#,##,###.00'),0.00) jumlah  from KW_Penerimaan_Debit_item d left join KW_Ref_Pelanggan  p on p.Ref_kod_akaun=d.nama_pelanggan where Ref_nama_syarikat='" + TextBox21.Text + "' " + smb_clk4 + " group by no_rujukan, tarikh_invois,nama_pelanggan,Ref_nama_syarikat";
        }
        else if (TextBox13.Text != "" && TextBox20.Text != "" && TextBox21.Text == "")
        {
            qry1 = "select no_rujukan,Format(tarikh_invois,'dd/MM/yyyy') tarikh_invois,nama_pelanggan,Ref_nama_syarikat,isnull(Format(sum(overall),'#,##,###.00'),0.00) jumlah  from KW_Penerimaan_Debit_item d left join KW_Ref_Pelanggan  p on p.Ref_kod_akaun=d.nama_pelanggan where no_Rujukan='" + TextBox13.Text + "' and tarikh_invois='" + fmdate_de + "' " + smb_clk4 + " group by no_rujukan, tarikh_invois,nama_pelanggan,Ref_nama_syarikat";
        }
        else if (TextBox13.Text == "" && TextBox20.Text != "" && TextBox21.Text != "")
        {
            qry1 = "select no_rujukan,Format(tarikh_invois,'dd/MM/yyyy') tarikh_invois,nama_pelanggan,Ref_nama_syarikat,isnull(Format(sum(overall),'#,##,###.00'),0.00) jumlah  from KW_Penerimaan_Debit_item d left join KW_Ref_Pelanggan  p on p.Ref_kod_akaun=d.nama_pelanggan where tarikh_invois='" + fmdate_de + "' and Ref_nama_syarikat='" + TextBox21.Text + "' " + smb_clk4 + " group by no_rujukan, tarikh_invois,nama_pelanggan,Ref_nama_syarikat";
        }
        else if (TextBox13.Text != "" && TextBox20.Text == "" && TextBox21.Text != "")
        {
            qry1 = "select no_rujukan,Format(tarikh_invois,'dd/MM/yyyy') tarikh_invois,nama_pelanggan,Ref_nama_syarikat,isnull(Format(sum(overall),'#,##,###.00'),0.00) jumlah  from KW_Penerimaan_Debit_item d left join KW_Ref_Pelanggan  p on p.Ref_kod_akaun=d.nama_pelanggan where no_Rujukan='" + TextBox13.Text + "' and Ref_nama_syarikat='" + TextBox21.Text + "' " + smb_clk4 + " group by no_rujukan, tarikh_invois,nama_pelanggan,Ref_nama_syarikat";
        }
        else if (TextBox13.Text != "" && TextBox20.Text != "" && TextBox21.Text != "")
        {
            qry1 = "select no_rujukan,Format(tarikh_invois,'dd/MM/yyyy') tarikh_invois,nama_pelanggan,Ref_nama_syarikat,isnull(Format(sum(overall),'#,##,###.00'),0.00) jumlah  from KW_Penerimaan_Debit_item d left join KW_Ref_Pelanggan  p on p.Ref_kod_akaun=d.nama_pelanggan where no_Rujukan='" + TextBox13.Text + "' and tarikh_invois='" + fmdate_de + "' and Ref_nama_syarikat='" + TextBox21.Text + "' " + smb_clk4 + " group by no_rujukan, tarikh_invois,nama_pelanggan,Ref_nama_syarikat";
        }
       else
        {
            qry1 = "select no_rujukan,Format(tarikh_invois,'dd/MM/yyyy') tarikh_invois,nama_pelanggan,Ref_nama_syarikat,isnull(Format(sum(overall),'#,##,###.00'),0.00) jumlah  from KW_Penerimaan_Debit_item d left join KW_Ref_Pelanggan  p on p.Ref_kod_akaun=d.nama_pelanggan " + smb_clk4 + " group by no_rujukan, tarikh_invois,nama_pelanggan,Ref_nama_syarikat";
        }
        SqlCommand cmd2 = new SqlCommand("" + qry1 + "", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            Gridview8.DataSource = ds2;
            Gridview8.DataBind();
            int columncount = Gridview8.Rows[0].Cells.Count;
            Gridview8.Rows[0].Cells.Clear();
            Gridview8.Rows[0].Cells.Add(new TableCell());
            Gridview8.Rows[0].Cells[0].ColumnSpan = columncount;
            Gridview8.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
        }
        else
        {
            Gridview8.DataSource = ds2;
            Gridview8.DataBind();
        }
    }

    protected void Button13_Click(object sender, EventArgs e)
    {
        Bindinvois();
        Bindresit();
        Bindcredit();
        Binddedit();
        tab4();
    }





    void reset()
    {
        //tab1
        inv_tab1.Visible = false;
        txtnoinvois1_1.Text = "";
        ddakaun.SelectedIndex = 0;
        ddakaun.Attributes.Remove("disabled");

        ddpela.SelectedIndex = 0;


        TextBox7.Text = "";


        TextBox10.Text = "";


        GP_date.Text = "";

        ddpela1.SelectedIndex = 0;
        ddpela1.Attributes.Remove("disabled");


        txttarikhinvois1.Text = "";
        txttarikhinvois1.Attributes.Remove("disabled");

        ddpro.SelectedIndex = 0;
        ddpro.Attributes.Remove("disabled");

        dddays.SelectedValue = "";
        dddays.Attributes.Remove("disabled");

        TextBox14.Text = "";


        txtnorst3.Text = "";
        txtnorst3.Attributes.Remove("disabled");


        SetInitialRow();
        SetInitialRowPay();


        //tab2
        resit_tab2.Visible = false;
        txtnorst3_3.Text = "";
        ddakaun.SelectedIndex = 0;
        ddakaun.Attributes.Remove("disabled");

        dd_selvalue.SelectedValue = "0";
        dd_selvalue.Attributes.Remove("disabled");

        TextBox17.Text = "";
        TextBox15.Text = "";
        //TextBox16.Text = "";

        ddnamapela3.SelectedIndex = 0;
        ddnamapela3.Attributes.Remove("disabled");



        GP_date.Text = "";
        GP_date.Attributes.Remove("disabled");

        dd_Betuk1.SelectedValue = "";
        dd_Betuk1.Attributes.Remove("disabled");

        GP_rno.Text = "";
        GP_rno.Attributes.Remove("disabled");

        TextBox11.Text = "";

        txtname.Text = "";
        txtname.Attributes.Remove("disabled");

        ddaka.SelectedIndex = 0;
        ddaka.Attributes.Remove("disabled");

        txtjum.Text = "";
        txtjum.Attributes.Remove("disabled");

        SetInitialRowRes();
        Button23.Visible = false;

        //tab3
        txtnoruju_1.Text = "";
        kredit_tab3.Visible = false;

        ddakaun.SelectedIndex = 0;
        ddakaun.Attributes.Remove("disabled");




        ddpela2.SelectedIndex = 0;
        ddpela2.Attributes.Remove("disabled");

        txttcinvois.Text = "";
        txttcinvois.Attributes.Remove("disabled");

        txttcre.Text = "";
        txttcre.Attributes.Remove("disabled");


        ddpro1.SelectedIndex = 0;
        ddpro1.Attributes.Remove("disabled");

        //ddinv.SelectedIndex = 0;
        ddinv.SelectedValue = "";
        ddinv.Attributes.Remove("disabled");


        SetInitialRowCre();

        TextBox18.Text = "";

        Session["dbtno"] = "";

        //tab4 
        debit_tab4.Visible = false;
        txtnoruj2_2.Text = "";
        ddakaun.SelectedIndex = 0;
        ddakaun.Attributes.Remove("disabled");

        ddinv2.Attributes.Remove("disabled");


        ddpela3.SelectedIndex = 0;
        ddpela3.Attributes.Remove("disabled");

        txtdtinvois.Text = "";
        txtdtinvois.Attributes.Remove("disabled");

        txtdeb.Text = "";
        txtdeb.Attributes.Remove("disabled");

        ddpro2.SelectedIndex = 0;
        ddpro2.Attributes.Remove("disabled");

        ddinv2.SelectedIndex = 0;
        ddinvday2.SelectedIndex = 0; ;
        ddinvday2.Attributes.Remove("disabled");

        // SetInitialRowDeb();
    }

    // textbox changed
    protected void QtyChanged(object sender, EventArgs eventArgs)
    {
        decimal numTotal = 0;

        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox qty = (TextBox)row.FindControl("TextBox4");
        TextBox unit = (TextBox)row.FindControl("TextBox3");
        Label jum = (Label)row.FindControl("Label1");
        Label jumI = (Label)row.FindControl("Label5");
        //Label jumI2 = (Label)row.FindControl("Label6");
        CheckBox chk1 = (CheckBox)row.FindControl("CheckBox1");
        DropDownList dd1 = (DropDownList)row.FindControl("ddcukaiinv");
        DropDownList dd2 = (DropDownList)row.FindControl("ddcukaioth");

        tab1();

        int qty1 = Convert.ToInt32(qty.Text);
        decimal unit1 = Convert.ToDecimal(unit.Text);
        if (qty1.ToString() != "")
        {
            numTotal = qty1 * unit1;
            jum.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");
            jumI.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");

            GrandTotal();
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
                //TextBox qty = (TextBox)row.FindControl("TextBox4");
                //TextBox unit = (TextBox)row.FindControl("TextBox3");
                TextBox dis = (TextBox)row.FindControl("Txtdis");
                Label txt = (Label)Gridview1.FooterRow.FindControl("Label4");
                Label gamt = (Label)row.FindControl("Label8");
                Label gmt = (Label)row.FindControl("Label10");
                Label txt1 = (Label)row.FindControl("Label5");
                CheckBox chk = (CheckBox)row.FindControl("CheckBox1");
                DropDownList dd = (DropDownList)row.FindControl("ddcukaioth");
                String tgst_1 = (row.FindControl("ddcukaiinv") as DropDownList).SelectedItem.Value;
                Label tax1 = (Label)Gridview1.FooterRow.FindControl("Label7");
                if (qty.Text != "" && unit.Text != "")
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
                            GrandTotal();
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
                            tab1();
                            GrandTotal();
                        }
                    }
                    else
                    {
                        if (dis.Text == "")
                        {
                            dis.Text = "0.00";
                        }
                        numTotal1 = Convert.ToDecimal(txt1.Text);
                        if (chk.Checked == true)
                        {
                            numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(gmt.Text);
                            numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text);
                            tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                            txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                        }
                        else
                        {
                            numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) + Convert.ToDecimal(gmt.Text);
                            numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text);
                            tOt.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                            txt1.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                        }

                        txt.Text = "0.00";
                        gamt.Text = "0.00";
                        tab1();
                        GrandTotal();
                    }
                }
            }

            if(dd2.SelectedValue != "")
            {
                decimal numTotal1 = 0;
                decimal numTotal2 = 0;
                decimal numTotal3 = 0;
                decimal numTotal4 = 0;
                //int selRowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
                Label tOt = (Label)row.FindControl("Label1");
                //TextBox qty = (TextBox)Gridview1.Rows[selRowIndex].FindControl("TextBox4");
                //TextBox unit = (TextBox)Gridview1.Rows[selRowIndex].FindControl("TextBox3");
                TextBox dis = (TextBox)row.FindControl("Txtdis");
                Label txt = (Label)row.FindControl("Label7");
                Label gmt = (Label)row.FindControl("Label10");
                Label txt1 = (Label)row.FindControl("Label5");
                CheckBox chk = (CheckBox)row.FindControl("CheckBox1");
                DropDownList dd = (DropDownList)row.FindControl("ddcukaioth");
                String tgst_1 = (row.FindControl("ddcukaioth") as DropDownList).SelectedItem.Value;
                Label tax1 = (Label)Gridview1.FooterRow.FindControl("Label4");
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
                        GrandTotal2();
                    }
                    else
                    {
                        decimal tot1 = 0;
                        if (tOt.Text != "")
                        {
                            tot1 = Convert.ToDecimal(tOt.Text);
                        }

                        int tgst1 = Convert.ToInt32(tgst);

                        numTotal1 = tot1 * tgst1 / 100;
                        txt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                        gmt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                        numTotal2 = tot1 + numTotal1;
                        txt1.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                        tab1();
                        GrandTotal2();
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
                    }

                    txt.Text = "0.00";
                    gmt.Text = "0.00";
                    tab1();
                    GrandTotal2();
                }
            }
        }

    }

    protected void QtyChanged_kty(object sender, EventArgs eventArgs)
    {
        decimal numTotal = 0;

        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox qty = (TextBox)row.FindControl("TextBox4");
        TextBox unit = (TextBox)row.FindControl("TextBox3");
        Label jum = (Label)row.FindControl("Label1");
        Label jumI = (Label)row.FindControl("Label5");
        //Label jumI2 = (Label)row.FindControl("Label6");
        CheckBox chk1 = (CheckBox)row.FindControl("CheckBox1");
        DropDownList dd1 = (DropDownList)row.FindControl("ddcukaiinv");
        DropDownList dd2 = (DropDownList)row.FindControl("ddcukaioth");
        tab1();
        if (unit.Text != "")
        {
            int qty1 = Convert.ToInt32(qty.Text);
            decimal unit1 = Convert.ToDecimal(unit.Text);
            if (qty1.ToString() != "")
            {
                numTotal = qty1 * unit1;
                jum.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");
                jumI.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");

                GrandTotal();
            }
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
                //TextBox qty = (TextBox)row.FindControl("TextBox4");
                //TextBox unit = (TextBox)row.FindControl("TextBox3");
                TextBox dis = (TextBox)row.FindControl("Txtdis");
                Label txt = (Label)Gridview1.FooterRow.FindControl("Label4");
                Label gamt = (Label)row.FindControl("Label8");
                Label gmt = (Label)row.FindControl("Label10");
                Label txt1 = (Label)row.FindControl("Label5");
                CheckBox chk = (CheckBox)row.FindControl("CheckBox1");
                DropDownList dd = (DropDownList)row.FindControl("ddcukaioth");
                String tgst_1 = (row.FindControl("ddcukaiinv") as DropDownList).SelectedItem.Value;
                Label tax1 = (Label)Gridview1.FooterRow.FindControl("Label7");
                if (qty.Text != "" && unit.Text != "")
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
                            GrandTotal();
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
                            tab1();
                            GrandTotal();
                        }
                    }
                    else
                    {
                        if (dis.Text == "")
                        {
                            dis.Text = "0.00";
                        }
                        numTotal1 = Convert.ToDecimal(txt1.Text);
                        if (chk.Checked == true)
                        {
                            numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(gmt.Text);
                            numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text);
                            tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                            txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                        }
                        else
                        {
                            numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) + Convert.ToDecimal(gmt.Text);
                            numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text);
                            tOt.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                            txt1.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                        }

                        txt.Text = "0.00";
                        gamt.Text = "0.00";
                        tab1();
                        GrandTotal();
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
                //TextBox qty = (TextBox)Gridview1.Rows[selRowIndex].FindControl("TextBox4");
                //TextBox unit = (TextBox)Gridview1.Rows[selRowIndex].FindControl("TextBox3");
                TextBox dis = (TextBox)row.FindControl("Txtdis");
                Label txt = (Label)Gridview1.FooterRow.FindControl("Label7");
                Label gmt = (Label)row.FindControl("Label10");
                Label txt1 = (Label)row.FindControl("Label5");
                CheckBox chk = (CheckBox)row.FindControl("CheckBox1");
                DropDownList dd = (DropDownList)row.FindControl("ddcukaioth");
                String tgst_1 = (row.FindControl("ddcukaioth") as DropDownList).SelectedItem.Value;
                Label tax1 = (Label)Gridview1.FooterRow.FindControl("Label4");
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
                        GrandTotal2();
                    }
                    else
                    {
                        decimal tot1 = 0;
                        if (tOt.Text != "")
                        {
                            tot1 = Convert.ToDecimal(tOt.Text);
                        }

                        int tgst1 = Convert.ToInt32(tgst);

                        numTotal1 = tot1 * tgst1 / 100;
                        txt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                        gmt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                        numTotal2 = tot1 + numTotal1;
                        txt1.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                        tab1();
                        GrandTotal2();
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
                    }

                    txt.Text = "0.00";
                    gmt.Text = "0.00";
                    tab1();
                    GrandTotal2();
                }
            }
        }
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
        tab1();

        decimal qty1 = Convert.ToDecimal(qty.Text);
        decimal JUM1 = Convert.ToDecimal(unit.Text);
        decimal dis = 0;
        if (DIS.Text != "")
        {
            dis = Convert.ToDecimal(DIS.Text);
        }
        else
        {
            dis = 0;
        }

        if (DIS.ToString() != "")
        {
            numTotal = qty1 * JUM1 - dis;
            jum.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");
            jumI.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");
            GrandTotal();
        }
        //code to go here when pri is changed
    }



    protected void QtyChangedres(object sender, EventArgs eventArgs)
    {
        decimal numTotal = 0;
        //decimal numTotal1 = 0;
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox qty = (TextBox)row.FindControl("TextBox14");
        TextBox unit = (TextBox)row.FindControl("TextBox13");

        Label tgst2 = (Label)row.FindControl("Label2");
        Label jum = (Label)row.FindControl("Label3");
        Label JUMI = (Label)row.FindControl("Label5");

        CheckBox chk1 = (CheckBox)row.FindControl("chkres");
        DropDownList dd1 = (DropDownList)row.FindControl("ddrescuk");
        DropDownList dd2 = (DropDownList)row.FindControl("ddresoth");
        decimal qty1 = 1;
        if (qty.Text != "")
        {
            qty1 = Convert.ToInt32(qty.Text);
        }
      
        decimal unit1 = Convert.ToDecimal(unit.Text);

        tab2();
        if (qty1.ToString() != "")
        {
            numTotal = qty1 * unit1;
            jum.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");
            JUMI.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");
           
            GrandTotalres();
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
                Label tOt = (Label)row.FindControl("Label3");
                //TextBox qty = (TextBox)Gridview4.Rows[selRowIndex].FindControl("TextBox14");
                //TextBox unit = (TextBox)Gridview4.Rows[selRowIndex].FindControl("TextBox13");
                TextBox dis = (TextBox)row.FindControl("Txtdisres");
                Label gamt = (Label)row.FindControl("Label8");
                Label gmt = (Label)row.FindControl("Label10");
                Label txt = (Label)Gridview4.FooterRow.FindControl("Label2");
                Label txt1 = (Label)row.FindControl("Label5");
                Label tax1 = (Label)Gridview4.FooterRow.FindControl("Label7");
                DropDownList dd = (DropDownList)row.FindControl("ddresoth");
                CheckBox chk = (CheckBox)row.FindControl("chkres");
                String tgst_1 = (row.FindControl("ddrescuk") as DropDownList).SelectedItem.Value;
                if (qty.Text != "" && unit.Text != "")
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
                            txt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                            gamt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                            numTotal2 = Convert.ToDecimal(tOt.Text) - numTotal4;
                            numTotal3 = Convert.ToDecimal(txt1.Text);
                            tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                            txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                            tab2();
                            dd.Enabled = true;
                            //dd.Attributes.Remove("style");
                            dd.Attributes.Remove("readonly");
                            GrandTotalres();
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
                            tab2();
                            GrandTotalres();
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
                            numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(gmt.Text);
                            tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                            txt1.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                        }
                        else
                        {
                            numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) + Convert.ToDecimal(gmt.Text);
                            numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text);
                            tOt.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                            txt1.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                        }

                        txt.Text = "0.00";
                        gamt.Text = "0.00";
                        tab2();
                        GrandTotalres();
                    }
                }
            }

            if(dd2.SelectedValue != "")
            {
                decimal numTotal1 = 0;
                decimal numTotal2 = 0;
                decimal numTotal3 = 0;
                decimal numTotal4 = 0;
                //int selRowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
                Label tOt = (Label)row.FindControl("Label3");
                Label txt = (Label)Gridview4.FooterRow.FindControl("Label7");
                //TextBox qty = (TextBox)Gridview4.Rows[selRowIndex].FindControl("TextBox14");
                //TextBox unit = (TextBox)Gridview4.Rows[selRowIndex].FindControl("TextBox13");
                TextBox dis = (TextBox)row.FindControl("Txtdisres");
                Label gmt = (Label)row.FindControl("Label10");
                Label txt1 = (Label)row.FindControl("Label5");
                Label tax1 = (Label)Gridview4.FooterRow.FindControl("Label2");
                CheckBox chk = (CheckBox)row.FindControl("chkres");
                DropDownList dd = (DropDownList)row.FindControl("ddresoth");
                String tgst_1 = (row.FindControl("ddresoth") as DropDownList).SelectedItem.Value;

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
                        tab2();
                        GrandTotalres2();
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
                        tab2();
                        GrandTotalres2();
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
                        numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1.Text);
                        numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1.Text);
                        tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                        //txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                    }
                    else
                    {
                        numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1.Text);
                        numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1.Text);
                        tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                        //txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                    }

                    txt.Text = "0.00";
                    gmt.Text = "0.00";
                    tab2();
                    GrandTotalres2();
                }
            }
        }
    }

    protected void QtyChangedres_kty(object sender, EventArgs eventArgs)
    {
        decimal numTotal = 0;
        //decimal numTotal1 = 0;
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox qty = (TextBox)row.FindControl("TextBox14");
        TextBox unit = (TextBox)row.FindControl("TextBox13");

        Label tgst2 = (Label)row.FindControl("Label2");
        Label jum = (Label)row.FindControl("Label3");
        Label JUMI = (Label)row.FindControl("Label5");

        CheckBox chk1 = (CheckBox)row.FindControl("chkres");
        DropDownList dd1 = (DropDownList)row.FindControl("ddrescuk");
        DropDownList dd2 = (DropDownList)row.FindControl("ddresoth");
        decimal qty1 = 1;
        if (qty.Text != "")
        {
            qty1 = Convert.ToInt32(qty.Text);
        }

        decimal unit1 = Convert.ToDecimal(unit.Text);

        tab2();
        if (qty1.ToString() != "")
        {
            numTotal = qty1 * unit1;
            jum.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");
            JUMI.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");

            GrandTotalres();

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
                    Label tOt = (Label)row.FindControl("Label3");
                    //TextBox qty = (TextBox)Gridview4.Rows[selRowIndex].FindControl("TextBox14");
                    //TextBox unit = (TextBox)Gridview4.Rows[selRowIndex].FindControl("TextBox13");
                    TextBox dis = (TextBox)row.FindControl("Txtdisres");
                    Label gamt = (Label)row.FindControl("Label8");
                    Label gmt = (Label)row.FindControl("Label10");
                    Label txt = (Label)Gridview4.FooterRow.FindControl("Label2");
                    Label txt1 = (Label)row.FindControl("Label5");
                    Label tax1 = (Label)Gridview4.FooterRow.FindControl("Label7");
                    DropDownList dd = (DropDownList)row.FindControl("ddresoth");
                    CheckBox chk = (CheckBox)row.FindControl("chkres");
                    String tgst_1 = (row.FindControl("ddrescuk") as DropDownList).SelectedItem.Value;
                    if (qty.Text != "" && unit.Text != "")
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
                                txt.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                                gamt.Text = numTotal4.ToString("C").Replace("RM", "").Replace("$", "");
                                numTotal2 = Convert.ToDecimal(tOt.Text) - numTotal4;
                                numTotal3 = Convert.ToDecimal(txt1.Text);
                                tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                                txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                                tab2();
                                dd.Enabled = true;
                                //dd.Attributes.Remove("style");
                                dd.Attributes.Remove("readonly");
                                GrandTotalres();
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
                                tab2();
                                GrandTotalres();
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
                                numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(gmt.Text);
                                tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                                txt1.Text = numTotal1.ToString("C").Replace("RM", "").Replace("$", "");
                            }
                            else
                            {
                                numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) + Convert.ToDecimal(gmt.Text);
                                numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text);
                                tOt.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                                txt1.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                            }

                            txt.Text = "0.00";
                            gamt.Text = "0.00";
                            tab2();
                            GrandTotalres();
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
                    Label tOt = (Label)row.FindControl("Label3");
                    Label txt = (Label)Gridview4.FooterRow.FindControl("Label7");
                    //TextBox qty = (TextBox)Gridview4.Rows[selRowIndex].FindControl("TextBox14");
                    //TextBox unit = (TextBox)Gridview4.Rows[selRowIndex].FindControl("TextBox13");
                    TextBox dis = (TextBox)row.FindControl("Txtdisres");
                    Label gmt = (Label)row.FindControl("Label10");
                    Label txt1 = (Label)row.FindControl("Label5");
                    Label tax1 = (Label)Gridview4.FooterRow.FindControl("Label2");
                    CheckBox chk = (CheckBox)row.FindControl("chkres");
                    DropDownList dd = (DropDownList)row.FindControl("ddresoth");
                    String tgst_1 = (row.FindControl("ddresoth") as DropDownList).SelectedItem.Value;

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
                            tab2();
                            GrandTotalres2();
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
                            tab2();
                            GrandTotalres2();
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
                            numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1.Text);
                            numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1.Text);
                            tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                            //txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                        }
                        else
                        {
                            numTotal2 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1.Text);
                            numTotal3 = numTotal1 - Convert.ToDecimal(dis.Text) - Convert.ToDecimal(tax1.Text);
                            tOt.Text = numTotal2.ToString("C").Replace("RM", "").Replace("$", "");
                            //txt1.Text = numTotal3.ToString("C").Replace("RM", "").Replace("$", "");
                        }

                        txt.Text = "0.00";
                        gmt.Text = "0.00";
                        tab2();
                        GrandTotalres2();
                    }
                }
            }
        }
    }

    protected void disChangedres(object sender, EventArgs eventArgs)
    {
        decimal numTotal = 0;

        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox DIS = (TextBox)row.FindControl("Txtdisres");
        TextBox unit = (TextBox)row.FindControl("TextBox13");
        Label jum = (Label)row.FindControl("Label3");
        Label jumI = (Label)row.FindControl("Label5");
        tab2();

        decimal qty1 = Convert.ToDecimal(DIS.Text);
        decimal JUM1 = Convert.ToDecimal(jum.Text);

        if (qty1.ToString() != "")
        {
            numTotal = JUM1 - qty1;
            jum.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");
            jumI.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");
            GrandTotalres();
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

    protected void disChanged1(object sender, EventArgs eventArgs)
    {
        decimal numTotal = 0;
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox qty = (TextBox)row.FindControl("TextBox24");
        TextBox unit = (TextBox)row.FindControl("TextBox23");
        TextBox dis = (TextBox)row.FindControl("txtdiscre");
        Label jum = (Label)row.FindControl("Label1");

        Label JUMI = (Label)row.FindControl("Label5");

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




        //code to go here when pri is changed
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

    protected void disQtyChangeddeb(object sender, EventArgs eventArgs)
    {
        var sum = 0;
        decimal numTotal = 0;
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox qty = (TextBox)row.FindControl("TextBox34");
        TextBox unit = (TextBox)row.FindControl("TextBox33");
        TextBox dis = (TextBox)row.FindControl("txtdisdeb");
        Label jum = (Label)row.FindControl("Label1");
        Label jumI = (Label)row.FindControl("Label5");

        decimal Jum1 = Convert.ToDecimal(jum.Text);
        decimal dis1 = Convert.ToDecimal(dis.Text);
        tab4();
        if (Jum1.ToString() != "")
        {
            numTotal = Jum1 - dis1;
            jum.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");
            jumI.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");
            GrandTotalded();
        }

        //code to go here when pri is changed
    }
    // rowdatabound events
    protected void Gridview1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        decimal totalbill = 0;



        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Find the DropDownList in the Row
            DropDownList ddlCountries = (e.Row.FindControl("ddkod") as DropDownList);
            ddlCountries.DataSource = GetData("select (kod_akaun + ' | '+ nama_akaun) nama_akaun,kod_akaun from KW_ref_carta_akaun where jenis_akaun_type != '1' and Status='A'");
            ddlCountries.DataTextField = "nama_akaun";
            ddlCountries.DataValueField = "kod_akaun";
            ddlCountries.DataBind();

            //Add Default Item in the DropDownList
            ddlCountries.Items.Insert(0, new ListItem("--- PILIH ---"));


            DropDownList ddlCountries1 = (e.Row.FindControl("ddcukaiinv") as DropDownList);
            ddlCountries1.DataSource = GetData("select Ref_nama_cukai,Ref_kadar,Ref_kod_cukai from KW_Ref_Tetapan_cukai where tc_jenis='PEN'");
            ddlCountries1.DataTextField = "Ref_nama_cukai";
            ddlCountries1.DataValueField = "Ref_kod_cukai";
            ddlCountries1.DataBind();

            //Add Default Item in the DropDownList
            ddlCountries1.Items.Insert(0, new ListItem("--- PILIH ---"));


            DropDownList ddlCountries2 = (e.Row.FindControl("ddcukaioth") as DropDownList);
            ddlCountries2.DataSource = GetData("select Ref_nama_cukai,Ref_kadar,Ref_kod_cukai from KW_Ref_Tetapan_cukai where tc_jenis='OTH'");
            ddlCountries2.DataTextField = "Ref_nama_cukai";
            ddlCountries2.DataValueField = "Ref_kod_cukai";
            ddlCountries2.DataBind();

            //Add Default Item in the DropDownList
            ddlCountries2.Items.Insert(0, new ListItem("--- PILIH ---"));






            //Select the Country of Customer in DropDownList

        }

    }

    protected void Gridview4_RowDataBound(object sender, GridViewRowEventArgs e)
    {



        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Find the DropDownList in the Row
            DropDownList ddlCountries = (e.Row.FindControl("ddkodres") as DropDownList);
            ddlCountries.DataSource = GetData("select (kod_akaun + ' | '+ nama_akaun) nama_akaun,kod_akaun from KW_ref_carta_akaun where jenis_akaun_type != '1' and Status='A'");
            ddlCountries.DataTextField = "nama_akaun";
            ddlCountries.DataValueField = "kod_akaun";
            ddlCountries.DataBind();

            //Add Default Item in the DropDownList
            ddlCountries.Items.Insert(0, new ListItem("--- PILIH ---"));


            DropDownList ddlCountries1 = (e.Row.FindControl("ddrescuk") as DropDownList);
            ddlCountries1.DataSource = GetData("select Ref_nama_cukai,Ref_kadar,Ref_kod_cukai from KW_Ref_Tetapan_cukai where tc_jenis='PEN'");
            ddlCountries1.DataTextField = "Ref_nama_cukai";
            ddlCountries1.DataValueField = "Ref_kod_cukai";
            ddlCountries1.DataBind();

            //Add Default Item in the DropDownList
            ddlCountries1.Items.Insert(0, new ListItem("--- PILIH ---"));

            DropDownList ddlCountries2 = (e.Row.FindControl("ddresoth") as DropDownList);
            ddlCountries2.DataSource = GetData("select Ref_nama_cukai,Ref_kadar,Ref_kod_cukai from KW_Ref_Tetapan_cukai where tc_jenis='OTH'");
            ddlCountries2.DataTextField = "Ref_nama_cukai";
            ddlCountries2.DataValueField = "Ref_kod_cukai";
            ddlCountries2.DataBind();

            //Add Default Item in the DropDownList
            ddlCountries2.Items.Insert(0, new ListItem("--- PILIH ---"));

            //Select the Country of Customer in DropDownList

        }
    }

    private void GrandTotal()
    {
        decimal GTotal = 0;
        decimal ITotal = 0;
        decimal Gst = 0;
        for (int i = 0; i < Gridview1.Rows.Count; i++)
        {
            String total = (Gridview1.Rows[i].FindControl("Label1") as Label).Text;
            String totalI = (Gridview1.Rows[i].FindControl("Label5") as Label).Text;
            String gamt = (Gridview1.Rows[i].FindControl("Label8") as Label).Text;

            String tgst = (Gridview1.Rows[i].FindControl("ddcukaiinv") as DropDownList).SelectedItem.Value;
            Label jum1 = (Label)Gridview1.FooterRow.FindControl("Label4");
            Label gamt1 = (Label)Gridview1.FooterRow.FindControl("Label9");
            Label jum = (Label)Gridview1.FooterRow.FindControl("Label3");

            Label jumI = (Label)Gridview1.FooterRow.FindControl("Label6");
            string chk = (Gridview1.Rows[i].FindControl("CheckBox1") as CheckBox).Checked.ToString();
            GTotal += (decimal)Convert.ToDecimal(total);
            ITotal += (decimal)Convert.ToDecimal(totalI);

            if (chk == "True")
            {
                decimal Gstval = Convert.ToDecimal(totalI);
                if (tgst != "--- PILIH ---")
                {
                    decimal cuk = Convert.ToDecimal(gamt);
                    //decimal cuk2 = cuk / 100;
                    //decimal cuk1 = Gstval / cuk2 ;
                    //decimal cuk4 = Gstval - cuk1;
                    string ggst = cuk.ToString("C").Replace("RM", "").Replace("RM", "").Replace("$", "");
                    Gst += (decimal)Convert.ToDecimal(ggst);
                    jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("RM", "").Replace("$", "");
                    jum1.Text = Gst.ToString("C").Replace("RM", "").Replace("RM", "").Replace("$", "");
                    gamt1.Text = Gst.ToString("C").Replace("RM", "").Replace("RM", "").Replace("$", "");
                    jumI.Text = ITotal.ToString("C").Replace("RM", "").Replace("RM", "").Replace("$", "");
                    TextBox14.Text = ITotal.ToString("C").Replace("RM", "").Replace("RM", "").Replace("$", "");
                }
                else
                {
                    gamt = "0";
                    decimal cuk = Convert.ToDecimal(gamt);

                    string ggst = cuk.ToString("C").Replace("RM", "").Replace("$", "");
                    Gst += (decimal)Convert.ToDecimal(ggst);
                    jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");
                    jum1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    gamt1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    jumI.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                    TextBox14.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
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
                if (Gst != 0)
                {
                    jum1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                }
                else
                {
                    jum1.Text = "0.00";
                }
                gamt1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                TextBox14.Text = (ITotal).ToString("C").Replace("RM", "").Replace("$", "");
            }
        }

    }

    private void GrandTotal2()
    {
        decimal GTotal = 0;
        decimal ITotal = 0;
        decimal Gst = 0;
        for (int i = 0; i < Gridview1.Rows.Count; i++)
        {
            String total = (Gridview1.Rows[i].FindControl("Label1") as Label).Text;
            String totalI = (Gridview1.Rows[i].FindControl("Label5") as Label).Text;
            String gmt = (Gridview1.Rows[i].FindControl("Label10") as Label).Text;
            String tgst = (Gridview1.Rows[i].FindControl("ddcukaioth") as DropDownList).SelectedItem.Value;
            Label jum1 = (Label)Gridview1.FooterRow.FindControl("Label7");

            Label jum = (Label)Gridview1.FooterRow.FindControl("Label3");
            Label gmt1 = (Label)Gridview1.FooterRow.FindControl("Label11");
            Label jumI = (Label)Gridview1.FooterRow.FindControl("Label6");
            string chk = (Gridview1.Rows[i].FindControl("CheckBox1") as CheckBox).Checked.ToString();
            if (total != "")
            {
                GTotal += (decimal)Convert.ToDecimal(total);
            }
            else
            {
                GTotal += 0;
            }
            ITotal += (decimal)Convert.ToDecimal(totalI);

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
                    Gst += (decimal)Convert.ToDecimal(ggst);
                    jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");
                    jum1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    gmt1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    jumI.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                    TextBox14.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                }
                else
                {
                    gmt = "0";
                    decimal cuk = Convert.ToDecimal(gmt);

                    string ggst = cuk.ToString("C").Replace("RM", "").Replace("$", "");
                    Gst += (decimal)Convert.ToDecimal(ggst);
                    jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");
                    jum1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    gmt1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    jumI.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                    TextBox14.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
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
                if (Gst != 0)
                {
                    jum1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                }
                else
                {
                    jum1.Text = "0.00";
                }
                TextBox14.Text = (ITotal).ToString("C").Replace("RM", "").Replace("$", "");
            }
        }

    }

    private void GrandTotalres()
    {
        decimal GTotal = 0;
        decimal ITotal = 0;
        decimal Gst = 0;
        for (int i = 0; i < Gridview4.Rows.Count; i++)
        {
            String total = (Gridview4.Rows[i].FindControl("Label3") as Label).Text;
            String Itotal = (Gridview4.Rows[i].FindControl("Label5") as Label).Text;
            String gamt = (Gridview4.Rows[i].FindControl("Label8") as Label).Text;
            String tgst = (Gridview4.Rows[i].FindControl("ddrescuk") as DropDownList).SelectedItem.Value;
            Label jum = (Label)Gridview4.FooterRow.FindControl("Label4");
            Label gamt1 = (Label)Gridview4.FooterRow.FindControl("Label9");
            Label jum1 = (Label)Gridview4.FooterRow.FindControl("Label2");
            Label jumI = (Label)Gridview4.FooterRow.FindControl("Label6");
            string chk = (Gridview4.Rows[i].FindControl("chkres") as CheckBox).Checked.ToString();
            GTotal += (decimal)Convert.ToDecimal(total);
            ITotal += (decimal)Convert.ToDecimal(Itotal);
            if (chk == "True")
            {
                decimal Gstval = Convert.ToDecimal(Itotal);
                if (tgst != "--- PILIH ---")
                {
                    decimal cuk = Convert.ToDecimal(gamt);
                    string ggst = cuk.ToString("C").Replace("RM", "").Replace("$", "");
                    Gst += (decimal)Convert.ToDecimal(ggst);
                    jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");
                    jum1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    gamt1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    jumI.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                    TextBox11.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                }
                else
                {
                    gamt = "0";
                    decimal cuk = Convert.ToDecimal(gamt);
                    string ggst = cuk.ToString("C").Replace("RM", "").Replace("$", "");
                    Gst += (decimal)Convert.ToDecimal(ggst);
                    jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");
                    jum1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    gamt1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    jumI.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                    TextBox11.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
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
                jum1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                TextBox11.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
            }
        }

    }

    private void GrandTotalres2()
    {
        decimal GTotal = 0;
        decimal ITotal = 0;
        decimal Gst = 0;
        for (int i = 0; i < Gridview4.Rows.Count; i++)
        {
            String total = (Gridview4.Rows[i].FindControl("Label3") as Label).Text;
            String Itotal = (Gridview4.Rows[i].FindControl("Label5") as Label).Text;
            String tgst = (Gridview4.Rows[i].FindControl("ddresoth") as DropDownList).SelectedItem.Value;
            String gmt = (Gridview4.Rows[i].FindControl("Label10") as Label).Text;
            Label jum1 = (Label)Gridview4.FooterRow.FindControl("Label7");
            Label gmt1 = (Label)Gridview4.FooterRow.FindControl("Label11");
            Label jumI = (Label)Gridview4.FooterRow.FindControl("Label6");
            Label jum = (Label)Gridview4.FooterRow.FindControl("Label4");
            string chk = (Gridview4.Rows[i].FindControl("chkres") as CheckBox).Checked.ToString();
            GTotal += (decimal)Convert.ToDecimal(total);
            ITotal += (decimal)Convert.ToDecimal(Itotal);
            if (chk == "True")
            {
                decimal Gstval = Convert.ToDecimal(total) + Convert.ToDecimal(jum1.Text);
                if (tgst != "--- PILIH ---")
                {
                    decimal cuk = Convert.ToDecimal(gmt);

                    string ggst = cuk.ToString("C").Replace("RM", "").Replace("$", "");
                    Gst += (decimal)Convert.ToDecimal(ggst);
                    jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");
                    jum1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    gmt1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    jumI.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                    TextBox11.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                }
                else
                {
                    gmt = "0";
                    decimal cuk = Convert.ToDecimal(gmt);

                    string ggst = cuk.ToString("C").Replace("RM", "").Replace("$", "");
                    Gst += (decimal)Convert.ToDecimal(ggst);
                    jum.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");
                    jum1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    gmt1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                    jumI.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
                    TextBox11.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
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
                jum1.Text = Gst.ToString("C").Replace("RM", "").Replace("$", "");
                TextBox11.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
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
            if (total != "")
            {
                GTotal += (decimal)Convert.ToDecimal(total);
            }
            else
            {
                GTotal += 0;
            }
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
                    TextBox5.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
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
                    TextBox5.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
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
                TextBox5.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
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
                    TextBox5.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
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
                    TextBox5.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
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
                TextBox5.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
            }
        }

    }


    protected void Gridview3_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Find the DropDownList in the Row
            //DropDownList ddlCountries = (e.Row.FindControl("ddkoddup") as DropDownList);
            //ddlCountries.DataSource = GetData("select nama_akaun,kod_akaun from KW_ref_carta_akaun");
            //ddlCountries.DataTextField = "nama_akaun";
            //ddlCountries.DataValueField = "kod_akaun";
            //ddlCountries.DataBind();
            //ddlCountries.Items.Insert(0, new ListItem("--- PILIH ---"));

            ////Select the Country of Customer in DropDownList
            //string country = (e.Row.FindControl("lblCountry") as Label).Text;
            //if (country != "0")
            //{
            //    ddlCountries.Items.FindByValue(country).Selected = true;

            //}
            ////Add Default Item in the DropDownList

            //   DropDownList ddlCountries1 = (e.Row.FindControl("ddtax") as DropDownList);
            //   ddlCountries1.DataSource = GetData("select Ref_nama_cukai,Ref_kod_cukai from KW_Ref_Tetapan_cukai");
            //   ddlCountries1.DataTextField = "Ref_nama_cukai";
            //    ddlCountries1.DataValueField = "Ref_kod_cukai";
            //     ddlCountries1.DataBind();

            //  ddlCountries1.Items.Insert(0, new ListItem("--- PILIH ---"));

            ////Select the Country of Customer in DropDownList
            //  string country1 = (e.Row.FindControl("lblgst") as Label).Text;
            //if (country1 != "0")
            //{
            //    ddlCountries1.Items.FindByValue(country1).Selected = true;
            //}

            //DropDownList ddlCountries2 = (e.Row.FindControl("ddtaxoth") as DropDownList);
            //ddlCountries2.DataSource = GetData("select Ref_nama_cukai,Ref_kod_cukai from KW_Ref_Tetapan_cukai");
            //ddlCountries2.DataTextField = "Ref_nama_cukai";
            //ddlCountries2.DataValueField = "Ref_kod_cukai";
            //ddlCountries2.DataBind();

            //ddlCountries2.Items.Insert(0, new ListItem("--- PILIH ---"));

            ////Select the Country of Customer in DropDownList
            //string country2 = (e.Row.FindControl("lblgstoth") as Label).Text;
            //if(country2 != "0" )
            //{ 
            //ddlCountries2.Items.FindByValue(country2).Selected = true;
            //}
            //Select the Country of Customer in DropDownList

        }

    }

    protected void Gridview15_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Find the DropDownList in the Row
            //DropDownList ddlCountries = (e.Row.FindControl("ddkodres") as DropDownList);
            //ddlCountries.DataSource = GetData("select nama_akaun,kod_akaun from KW_ref_carta_akaun");
            //ddlCountries.DataTextField = "nama_akaun";
            //ddlCountries.DataValueField = "kod_akaun";
            //ddlCountries.DataBind();
            //ddlCountries.Items.Insert(0, new ListItem("--- PILIH ---"));

            //Select the Country of Customer in DropDownList
            //string country = (e.Row.FindControl("lblCountry") as Label).Text;
            //if (country != "0" )
            //{
            //    ddlCountries.Items.FindByValue(country).Selected = true;
            //}

            //Add Default Item in the DropDownList

            //DropDownList ddlCountries1 = (e.Row.FindControl("ddrescuk") as DropDownList);
            //ddlCountries1.DataSource = GetData("select Ref_nama_cukai,Ref_kod_cukai from KW_Ref_Tetapan_cukai");
            //ddlCountries1.DataTextField = "Ref_nama_cukai";
            //ddlCountries1.DataValueField = "Ref_kod_cukai";
            //ddlCountries1.DataBind();

            //ddlCountries1.Items.Insert(0, new ListItem("--- PILIH ---"));

            //Select the Country of Customer in DropDownList
            //string country1 = (e.Row.FindControl("lblgst") as Label).Text;
            //if (country1 != "0" )
            //{
            //    ddlCountries1.Items.FindByValue(country1).Selected = true;
            //}

            //DropDownList ddlCountries2 = (e.Row.FindControl("ddrescukoth") as DropDownList);
            //ddlCountries2.DataSource = GetData("select Ref_nama_cukai,Ref_kod_cukai from KW_Ref_Tetapan_cukai");
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

            //Select the Country of Customer in DropDownList

        }

    }

    protected void Gridview12_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Find the DropDownList in the Row
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

            //DropDownList ddlCountries1 = (e.Row.FindControl("ddgst") as DropDownList);
            //ddlCountries1.DataSource = GetData("select Ref_nama_cukai,Ref_kod_cukai from KW_Ref_Tetapan_cukai");
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


            //DropDownList ddlCountries2 = (e.Row.FindControl("ddgstoth") as DropDownList);
            //ddlCountries2.DataSource = GetData("select Ref_nama_cukai,Ref_kod_cukai from KW_Ref_Tetapan_cukai");
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

            //Select the Country of Customer in DropDownList

        }

    }

    protected void Gridview14_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Find the DropDownList in the Row
            //DropDownList ddlCountries = (e.Row.FindControl("ddkoddupdeb") as DropDownList);
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
            //ddlCountries1.DataSource = GetData("select Ref_nama_cukai,Ref_kod_cukai from KW_Ref_Tetapan_cukai");
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
            //ddlCountries2.DataSource = GetData("select Ref_nama_cukai,Ref_kod_cukai from KW_Ref_Tetapan_cukai");
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

            //Select the Country of Customer in DropDownList

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
            ddlCountries1.DataSource = GetData("select Ref_nama_cukai,Ref_kadar,Ref_kod_cukai from KW_Ref_Tetapan_cukai where tc_jenis='PEN' ");
            ddlCountries1.DataTextField = "Ref_nama_cukai";
            ddlCountries1.DataValueField = "Ref_kod_cukai";
            ddlCountries1.DataBind();

            //Add Default Item in the DropDownList
            ddlCountries1.Items.Insert(0, new ListItem("--- PILIH ---"));


            DropDownList ddlCountries2 = (e.Row.FindControl("ddkodgstoth") as DropDownList);
            ddlCountries2.DataSource = GetData("select Ref_nama_cukai,Ref_kadar,Ref_kod_cukai from KW_Ref_Tetapan_cukai where tc_jenis='OTH'");
            ddlCountries2.DataTextField = "Ref_nama_cukai";
            ddlCountries2.DataValueField = "Ref_kod_cukai";
            ddlCountries2.DataBind();

            //Add Default Item in the DropDownList
            ddlCountries2.Items.Insert(0, new ListItem("--- PILIH ---"));

        }

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
            ddlCountries1.DataSource = GetData("select Ref_nama_cukai,Ref_kadar,Ref_kod_cukai from KW_Ref_Tetapan_cukai  where tc_jenis='PEN'");
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

    protected void Button2_Click(object sender, EventArgs e)
    {


        if (ddakaun.SelectedItem.Text != "--- PILIH ---")
        {
            if (ddpela1.SelectedItem.Text != "--- PILIH ---")
            {
                if (txtnoinvois1.Text != "")
                {
                    if (txttarikhinvois1.Text != "")
                    {
                        DataTable dt = new DataTable();
                        DataTable dtI = new DataTable();
                        DataTable dtGK = new DataTable();
                        DataTable dtGD = new DataTable();
                        DataTable dt2 = new DataTable();

                        DataTable dt3 = new DataTable();
                        DataTable dtak = new DataTable();
                        DateTime tDate = DateTime.ParseExact(txttarikhinvois1.Text, "dd/MM/yyyy", null);
                        DateTime FDATE;
                        if (dddays.SelectedValue != "")
                        {
                            int addeddays = Convert.ToInt32(dddays.SelectedItem.Value);
                            FDATE = tDate.AddDays(addeddays);
                        }
                        else
                        {
                            int addeddays = Convert.ToInt32(0);
                            FDATE = tDate.AddDays(addeddays);
                        }
                        userid = Session["New"].ToString();
                        DataTable dtnama = new DataTable();
                        dtnama = Dbcon.Ora_Execute_table("select Ref_no_syarikat,Ref_kod_akaun,pel_kod_industry from  KW_Ref_Pelanggan where Ref_nama_syarikat='" + ddpela1.SelectedItem.Text + "'");
                        DataTable dtkat = new DataTable();
                        dtkat = Dbcon.Ora_Execute_table("select kat_akaun from KW_Ref_Carta_Akaun where kod_akaun='" + dtnama.Rows[0][1].ToString() + "'");
                        dtak = Dbcon.Ora_Execute_table("  select ref_kod_akaun from  KW_Ref_Pelanggan where Ref_nama_syarikat='" + ddpela1.SelectedItem.Text + "'");
                        string fmtcd = string.Empty;
                        DataTable gen_invkd = new DataTable();
                        gen_invkd = Dbcon.Ora_Execute_table("select no_invois from KW_Penerimaan_invois where no_invois='" + txtnoinvois1.Text + "'");
                        if(gen_invkd.Rows.Count > 0)
                        {
                            DataTable dt1_fmt = Dbcon.Ora_Execute_table("select Id,doc_type_cd,format as fmt,SUBSTRING(cur_format, 1, 10) as lfrmt1,SUBSTRING(cur_format, 11, 4) as lfrmt2,cur_format as cfmt from KW_Format_Nombor_rujukan where doc_type_cd='01' and Status='A'");
                            if (dt1_fmt.Rows.Count != 0)
                            {
                                fmtcd = dt1_fmt.Rows[0]["Id"].ToString();
                                if (dt1_fmt.Rows[0]["cfmt"].ToString() == "")
                                {
                                    txtnoinvois1_1.Text = dt1_fmt.Rows[0]["fmt"].ToString();                                    
                                }
                                else
                                {
                                    int seqno = Convert.ToInt32(dt1_fmt.Rows[0]["lfrmt2"].ToString());
                                    int newNumber = seqno + 1;
                                    uniqueId = newNumber.ToString(dt1_fmt.Rows[0]["lfrmt1"].ToString() + "0000");
                                    txtnoinvois1_1.Text = uniqueId;
                                }

                            }
                            else
                            {
                                DataTable dt1_fmt2 = Dbcon.Ora_Execute_table("select   ISNULL(max(SUBSTRING(no_invois,13,2000)),'0')  from KW_Penerimaan_invois");
                                if (dt1_fmt2.Rows.Count > 0)
                                {
                                    int seqno = Convert.ToInt32(dt1_fmt2.Rows[0][0].ToString());
                                    int newNumber = seqno + 1;
                                    uniqueId = newNumber.ToString("KTH-INV" + "-" + DateTime.Now.ToString("yy") + "-" + "0000");
                                    txtnoinvois1_1.Text = uniqueId;

                                }
                                else
                                {
                                    int newNumber = Convert.ToInt32(uniqueId) + 1;
                                    uniqueId = newNumber.ToString("KTH-INV" + "-" + DateTime.Now.ToString("yy") + "-" + "0000");
                                    txtnoinvois1_1.Text = uniqueId;
                                    
                                }
                            }
                        }
                        else
                        {
                            txtnoinvois1_1.Text = txtnoinvois1.Text;
                        }
                       
                        dt = Dbcon.Ora_Execute_table("insert into KW_Penerimaan_invois values('" + ddakaun.SelectedItem.Value + "','" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "','" + dtnama.Rows[0][0].ToString() + "','" + txtnoinvois1_1.Text + "','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "','" + ddpro.SelectedItem.Value + "','" + TextBox14.Text + "','" + TextBox14.Text + "','','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','','','" + dddays.SelectedValue + "','" + FDATE.ToString("yyyy/MM/dd hh:mm:ss") + "')");
                        DataTable dt_upd_format = new DataTable();
                        dt_upd_format = Dbcon.Ora_Execute_table("update KW_Format_Nombor_rujukan set cur_format='" + txtnoinvois1_1.Text + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where doc_type_cd='01' and Status = 'A'");
                        dt2 = Dbcon.Ora_Execute_table("update KW_Opening_Balance set ending_amt=ending_amt + " + TextBox14.Text + " ,upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where kod_akaun='" + dtak.Rows[0][0].ToString() + "' and opening_year='" + DateTime.Now.ToString("yyyy") + "' ");

                        dt3 = Dbcon.Ora_Execute_table("insert into KW_Penerimaan_payment values('" + ddakaun.SelectedItem.Value + "','" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "','" + txtnoinvois1_1.Text + "','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "','" + ddpro.SelectedItem.Value + "','','','','','','','" + TextBox14.Text + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','','','','')");
                        int count = 0;
                        string rcount = string.Empty;
                        foreach (GridViewRow g1 in Gridview1.Rows)
                        {

                            count++;
                            rcount = count.ToString();

                            string item = (g1.FindControl("TextBox1") as TextBox).Text;
                            string rujukan = (g1.FindControl("txt_rujukan") as TextBox).Text;
                            string po_no = (g1.FindControl("txt_po") as TextBox).Text;
                            string keter = (g1.FindControl("TextBox2") as TextBox).Text;
                            string unit = (g1.FindControl("TextBox3") as TextBox).Text;
                            string qty = (g1.FindControl("TextBox4") as TextBox).Text;
                            string dis = (g1.FindControl("Txtdis") as TextBox).Text;
                            string total = (g1.FindControl("Label1") as Label).Text;
                            string ttotal = (g1.FindControl("Label5") as Label).Text;
                            string DD = (g1.FindControl("ddkod") as DropDownList).SelectedItem.Value;
                            string DDN = (g1.FindControl("ddkod") as DropDownList).SelectedItem.Text;
                            string gamt = (g1.FindControl("Label8") as Label).Text;
                            string ogamt = (g1.FindControl("Label10") as Label).Text;
                            string DDcukai = (g1.FindControl("ddcukaiinv") as DropDownList).SelectedItem.Text;

                            if (rcount == "1")
                            {
                                dtGD = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "','" + TextBox14.Text + "','','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + dtkat.Rows[0][0].ToString() + "','" + txtnoinvois1_1.Text + "','" + rujukan + "','','','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "','" + keter + "','','" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "','" + ddpro.SelectedItem.Value + "','S','A','"+ dtnama.Rows[0]["pel_kod_industry"].ToString() + "')");

                                DataTable chk_carta = new DataTable();
                                chk_carta = Dbcon.Ora_Execute_table("select kat_akaun,kod_akaun,KW_Debit_amt,KW_kredit_amt,Kw_open_amt from KW_Ref_Carta_Akaun where kod_akaun='" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "'");
                                if(chk_carta.Rows.Count != 0)
                                {
                                    double amt_ope1 = 0;
                                    double amt_deb1 = 0;
                                    double amt_deb2_1 = 0;
                                    DataTable chk_kategory = new DataTable();
                                    chk_kategory = Dbcon.Ora_Execute_table("select kat_cd,kat_akuan,bal_type from KW_Kategori_akaun where kat_cd='"+ chk_carta.Rows[0]["kat_akaun"].ToString() + "'");

                                    amt_deb1 = ( double.Parse(chk_carta.Rows[0]["KW_Debit_amt"].ToString()) + double.Parse(TextBox14.Text));

                                    if (chk_kategory.Rows.Count != 0)
                                    {
                                        if (chk_kategory.Rows[0]["bal_type"].ToString() == "D")
                                        {
                                            amt_ope1 = (double.Parse(chk_carta.Rows[0]["Kw_open_amt"].ToString()) + double.Parse(TextBox14.Text));
                                        }
                                        else
                                        {
                                            amt_deb2_1 = (double.Parse(TextBox14.Text));
                                            amt_ope1 = (double.Parse(chk_carta.Rows[0]["Kw_open_amt"].ToString()) + (amt_deb2_1));
                                        }
                                    }
                                    DataTable upd_carta = new DataTable();
                                    upd_carta = Dbcon.Ora_Execute_table("UPDATE KW_Ref_Carta_Akaun set KW_Debit_amt='"+ amt_deb1 + "',Kw_open_amt='"+ amt_ope1 + "' where   kod_akaun='" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "'");
                                }


                            }

                            DataTable dtcuk = new DataTable();
                            dtcuk = Dbcon.Ora_Execute_table("select Ref_kod_cukai,Ref_kod_akaun,kat_akaun from KW_Ref_Tetapan_cukai where  Ref_nama_cukai='" + DDcukai + "'");
                            string DDcukaival_1 = (g1.FindControl("ddcukaiinv") as DropDownList).SelectedItem.Value;

                            string DDcukaival = string.Empty;
                            DataTable sel_gst = new DataTable();
                            sel_gst = Dbcon.Ora_Execute_table("select Ref_nama_cukai,Ref_kadar,Ref_kod_cukai From KW_Ref_Tetapan_cukai where Ref_kod_cukai='" + DDcukaival_1 + "'");
                            if (sel_gst.Rows.Count != 0)
                            {
                                DDcukaival = sel_gst.Rows[0]["Ref_kadar"].ToString();
                            }
                            else
                            {
                                DDcukaival = "0";
                            }

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
                            dtcuk1 = Dbcon.Ora_Execute_table("select Ref_kod_cukai,Ref_kod_akaun,kat_akaun from KW_Ref_Tetapan_cukai where  Ref_nama_cukai='" + DDcukai1 + "'");
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
                            string DDcukaival1_1 = (g1.FindControl("ddcukaioth") as DropDownList).SelectedItem.Value;
                            string DDcukaival1 = string.Empty;
                            DataTable sel_gst1 = new DataTable();
                            sel_gst1 = Dbcon.Ora_Execute_table("select Ref_nama_cukai,Ref_kadar,Ref_kod_cukai From KW_Ref_Tetapan_cukai where Ref_kod_cukai='" + DDcukaival1_1 + "'");
                            if (sel_gst1.Rows.Count != 0)
                            {
                                DDcukaival1 = sel_gst1.Rows[0]["Ref_kadar"].ToString();
                            }
                            else
                            {
                                DDcukaival1 = "0";
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
                                if (DDcukaival1 != "--- PILIH ---" && DDcukaival1 != "0")
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
                                if (DDcukaival != "--- PILIH ---" && DDcukaival != "0")
                                {
                                    val = DDcukaival;
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
                                    val = "0";
                                    tgst = Convert.ToDecimal("0.00");
                                }
                                if (DDcukaival1 != "--- PILIH ---")
                                {
                                    val1 = DDcukaival1;
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
                                    val1 = "0";
                                    tgst1 = Convert.ToDecimal("0.00");
                                }
                            }


                            dt = Dbcon.Ora_Execute_table("insert into KW_Penerimaan_invois_item values('" + ddakaun.SelectedItem.Value + "','" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "','" + txtnoinvois1_1.Text + "','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "','" + ddpro.SelectedItem.Value + "','" + DD + "','" + item + "','" + keter + "','" + unit + "','" + qty + "','" + val + "','" + val1 + "','" + chk + "','" + cuk + "','" + cuk1 + "','" + fre + "','" + total + "','" + tgst + "','" + tgst1 + "','" + ttotal + "','A','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','','','" + dddays.SelectedValue + "','" + FDATE.ToString("yyyy/MM/dd hh:mm:ss") + "','" + rujukan + "','" + po_no + "')");
                            dtI = Dbcon.Ora_Execute_table("update KW_Opening_Balance set ending_amt=ending_amt - " + ttotal + " ,upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where kod_akaun='" + DD + "'  and opening_year='" + DateTime.Now.ToString("yyyy") + "'");
                            DataTable dtkat1 = new DataTable();
                            dtkat1 = Dbcon.Ora_Execute_table("select kat_akaun,ct_kod_industry from KW_Ref_Carta_Akaun where kod_akaun='" + DD + "'");
                            string vv1 = string.Empty;
                            DataTable dtkat1_2 = new DataTable();
                            dtkat1_2 = Dbcon.Ora_Execute_table("select kat_akaun,ct_kod_industry from KW_Ref_Carta_Akaun where kod_akaun='" + taxkod + "'");
                            if(dtkat1_2.Rows.Count != 0)
                            {
                                vv1 = dtkat1_2.Rows[0][1].ToString();
                            }
                            

                            DataTable dtkat1_3 = new DataTable();
                            dtkat1_3 = Dbcon.Ora_Execute_table("select kat_akaun,ct_kod_industry from KW_Ref_Carta_Akaun where kod_akaun='" + taxkod1 + "'");

                            dtGK = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + DD + "','','" + total + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + dtkat1.Rows[0][0].ToString() + "','" + txtnoinvois1_1.Text + "','" + rujukan + "','','" + TextBox14.Text + "','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "','" + keter + "','','" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "','" + ddpro.SelectedItem.Value + "','S','A','"+ dtkat1.Rows[0][1].ToString() + "')");

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

                            if (tgst != 0)
                            {
                                dtGK = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + taxkod + "','','" + tgst + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + katkod + "','" + txtnoinvois1_1.Text + "','" + rujukan + "','" + cuk + "','" + TextBox14.Text + "','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "','" + keter + "','','" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "','" + ddpro.SelectedItem.Value + "','S','A','"+ vv1 + "')");
                            }
                            //else
                            //{
                            //    dtGK = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + taxkod + "','','" + tgst + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + katkod + "','" + txtnoinvois1_1.Text + "','" + rujukan + "','" + cuk + "','" + TextBox14.Text + "','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "','" + keter + "','','" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "','" + ddpro.SelectedItem.Value + "','S','A','" + vv1 + "')");
                            //}
                            if (tgst1 != 0)
                            {
                                dtGK = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + taxkod1 + "','','" + tgst1 + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + katkod1 + "','" + txtnoinvois1_1.Text + "','" + rujukan + "','" + cuk1 + "','" + TextBox14.Text + "','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "','" + keter + "','','" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "','" + ddpro.SelectedItem.Value + "','S','A','" + vv1 + "')");
                            }
                            //else
                            //{
                            //    dtGK = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + taxkod1 + "','','" + tgst1 + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + katkod1 + "','" + txtnoinvois1_1.Text + "','" + rujukan + "','" + cuk1 + "','" + TextBox14.Text + "','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "','" + keter + "','','" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "','" + ddpro.SelectedItem.Value + "','S','A','" + vv1 + "')");
                            //}
                        }


                        Div7.Visible = false;
                        Div9.Visible = true;
                        //if (txtnoinvois1.Text != "")
                        //{
                        //    Session["Invoisno"] = txtnoinvois1.Text;
                        //    //Response.Redirect("KW_Penerimaan_invois_Rptview.aspx");
                        //    string url = "KW_Penerimaan_invois_Rptview.aspx";
                        //    string s = "window.open('" + url + "', 'popup_window', 'width=11000,height=700,left=100,top=100,resizable=yes');";
                        //    ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

                        //}
                        Bindinvois();
                        Bindcredit();
                        Binddedit();
                        Bindresit();
                        reset();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Invois Dibuat Berjaya.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Tarikh Invois.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih No Invois.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila pilih Nama Pelanggan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila pilih Untuk Akuan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
        Bindinvois();
        Bindresit();
        Bindcredit();
        Binddedit();
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        if (ddakaun.SelectedItem.Text != "--- PILIH ---")
        {
            if (ddaka.SelectedItem.Text != "--- PILIH ---")
            {
                if (txtnorst3.Text != "")
                {
                    if (GP_date.Text != "")
                    {
                        if (dd_Betuk1.SelectedItem.Text != "--- PILIH ---")
                        {
                            if (txtjum.Text != "")
                            {


                                DataTable dt = new DataTable();
                                DataTable dt1 = new DataTable();
                                DataTable dt2 = new DataTable();
                                DataTable dt3 = new DataTable();
                                DataTable dt4 = new DataTable();

                                DateTime tDate = DateTime.ParseExact(GP_date.Text, "dd/MM/yyyy", null);
                                userid = Session["New"].ToString();
                                DataTable dtnama = new DataTable();

                                //if (dd_selvalue.SelectedItem.Text == "SEMUA COA")
                                //{
                                    dtnama = Dbcon.Ora_Execute_table("select nama_akaun,kod_akaun,ct_kod_industry from  KW_Ref_Carta_Akaun where kod_akaun='" + ddaka.SelectedValue + "'");
                                //}
                                //else if (dd_selvalue.SelectedItem.Text == "PELANGGAN")
                                //{
                                //    dtnama = Dbcon.Ora_Execute_table("select Ref_no_syarikat,Ref_kod_akaun,pel_kod_industry from  KW_Ref_Pelanggan where Ref_kod_akaun='" + ddnamapela3.SelectedValue + "'");
                                //}
                                DataTable dtkat = new DataTable();
                                dtkat = Dbcon.Ora_Execute_table("select kat_akaun from KW_Ref_Carta_Akaun where kod_akaun='" + dtnama.Rows[0][1].ToString() + "'");

                                //resit no

                                DataTable gen_invkd = new DataTable();
                                gen_invkd = Dbcon.Ora_Execute_table("select no_resit from kw_penerimaan_resit where no_resit='" + txtnorst3.Text + "'");
                                if (gen_invkd.Rows.Count > 0)
                                {
                                    DataTable dt1_fmt = Dbcon.Ora_Execute_table("select doc_type_cd,format as fmt,SUBSTRING(cur_format, 1, 10) as lfrmt1,SUBSTRING(cur_format, 11, 4) as lfrmt2,cur_format as cfmt from KW_Format_Nombor_rujukan where doc_type_cd='02' and Status='A'");
                                    if (dt1_fmt.Rows.Count != 0)
                                    {
                                        if (dt1_fmt.Rows[0]["cfmt"].ToString() == "")
                                        {
                                            txtnorst3_3.Text = dt1_fmt.Rows[0]["fmt"].ToString();
                                        }
                                        else
                                        {
                                            int seqno = Convert.ToInt32(dt1_fmt.Rows[0]["lfrmt2"].ToString());
                                            int newNumber = seqno + 1;
                                            uniqueId = newNumber.ToString(dt1_fmt.Rows[0]["lfrmt1"].ToString() + "0000");
                                            txtnorst3_3.Text = uniqueId;
                                        }

                                    }
                                    else
                                    {
                                        DataTable dt1_fmt2 = Dbcon.Ora_Execute_table("select   ISNULL(max(SUBSTRING(no_resit,13,2000)),'0')  from KW_Penerimaan_resit");
                                        if (dt1_fmt2.Rows.Count > 0)
                                        {
                                            int seqno = Convert.ToInt32(dt1_fmt2.Rows[0][0].ToString());
                                            int newNumber = seqno + 1;
                                            uniqueId = newNumber.ToString("KTH-RES" + "-" + DateTime.Now.ToString("yy") + "-" + "0000");
                                            txtnorst3_3.Text = uniqueId;

                                        }
                                        else
                                        {
                                            int newNumber = Convert.ToInt32(uniqueId) + 1;
                                            uniqueId = newNumber.ToString("KTH-RES" + "-" + DateTime.Now.ToString("yy") + "-" + "0000");
                                            txtnorst3_3.Text = uniqueId;
                                        }
                                    }
                                }
                                else
                                {
                                    txtnorst3_3.Text = txtnorst3.Text;
                                }

                                dt1 = Dbcon.Ora_Execute_table("insert into kw_penerimaan_resit values ('" + ddakaun.SelectedItem.Value + "','" + dtnama.Rows[0][1].ToString() + "','" + txtname.Text + "','" + txtnorst3_3.Text + "','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "','" + dd_Betuk1.SelectedItem.Value + "','" + ddnamapela3.SelectedItem.Value + "','" + GP_rno.Text + "','" + txtjum.Text + "','" + TextBox11.Text + "','" + TextBox11.Text + "','A','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','','','" + txtname.Text + "','" + dd_selvalue.SelectedValue + "')");
                                DataTable dt_upd_format = new DataTable();
                                dt_upd_format = Dbcon.Ora_Execute_table("update KW_Format_Nombor_rujukan set cur_format='" + txtnorst3_3.Text + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where doc_type_cd='02' and Status = 'A'");

                                int count = 0;
                                string rcount = string.Empty;
                                foreach (GridViewRow g1 in Gridview4.Rows)
                                {

                                    count++;
                                    rcount = count.ToString();

                                    string rujuk = (g1.FindControl("resit_rujukan") as TextBox).Text;
                                    string item = (g1.FindControl("TextBox11") as TextBox).Text;
                                    string keter = (g1.FindControl("TextBox12") as TextBox).Text;
                                    string unit = (g1.FindControl("TextBox13") as TextBox).Text;
                                    string qty = (g1.FindControl("TextBox14") as TextBox).Text;
                                    string dis = (g1.FindControl("Txtdisres") as TextBox).Text;
                                    string total = (g1.FindControl("Label3") as Label).Text;
                                    string ttotal = (g1.FindControl("Label5") as Label).Text;
                                    string gamt = (g1.FindControl("Label8") as Label).Text;
                                    string ogamt = (g1.FindControl("Label10") as Label).Text;

                                    string DDcukai = (g1.FindControl("ddrescuk") as DropDownList).SelectedItem.Text;

                                    if (rcount == "1")
                                    {
                                        dt4 = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + dtnama.Rows[0][1].ToString() + "','" + TextBox11.Text + "','','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + dtkat.Rows[0][0].ToString() + "','" + txtnorst3_3.Text + "','" + rujuk + "','','','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "','" + keter + "','','" + dtnama.Rows[0][1].ToString() + "','','','A','" + dtnama.Rows[0][2].ToString() + "')");

                                        DataTable chk_carta = new DataTable();
                                        chk_carta = Dbcon.Ora_Execute_table("select kat_akaun,kod_akaun,KW_Debit_amt,KW_kredit_amt,Kw_open_amt from KW_Ref_Carta_Akaun where kod_akaun='" + dtnama.Rows[0][1].ToString() + "'");
                                        if (chk_carta.Rows.Count != 0)
                                        {
                                            double amt_ope1 = 0;
                                            double amt_deb1 = 0;
                                            double amt_deb2_1 = 0;

                                            DataTable chk_kategory = new DataTable();
                                            chk_kategory = Dbcon.Ora_Execute_table("select kat_cd,kat_akuan,bal_type from KW_Kategori_akaun where kat_cd='" + chk_carta.Rows[0]["kat_akaun"].ToString() + "'");

                                            amt_deb1 = (double.Parse(chk_carta.Rows[0]["KW_Debit_amt"].ToString()) + double.Parse(TextBox11.Text));

                                            if (chk_kategory.Rows.Count != 0)
                                            {
                                                if (chk_kategory.Rows[0]["bal_type"].ToString() == "D")
                                                {
                                                    amt_ope1 = (double.Parse(chk_carta.Rows[0]["Kw_open_amt"].ToString()) + double.Parse(TextBox11.Text));
                                                }
                                                else
                                                {
                                                    amt_deb2_1 = (double.Parse(TextBox11.Text));
                                                    amt_ope1 = (double.Parse(chk_carta.Rows[0]["Kw_open_amt"].ToString()) + (amt_deb2_1));
                                                }
                                            }
                                            DataTable upd_carta = new DataTable();
                                            upd_carta = Dbcon.Ora_Execute_table("UPDATE KW_Ref_Carta_Akaun set KW_Debit_amt='" + amt_deb1 + "',Kw_open_amt='" + amt_ope1 + "' where   kod_akaun='" + dtnama.Rows[0][1].ToString() + "'");
                                        }

                                    }

                                    DataTable dtcuk = new DataTable();
                                    dtcuk = Dbcon.Ora_Execute_table("select Ref_kod_cukai,Ref_kod_akaun,kat_akaun from KW_Ref_Tetapan_cukai where  Ref_nama_cukai='" + DDcukai + "'");
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
                                    string DDcukaival_1 = (g1.FindControl("ddrescuk") as DropDownList).SelectedItem.Value;
                                    string DDcukaival = string.Empty;
                                    DataTable sel_gst = new DataTable();
                                    sel_gst = Dbcon.Ora_Execute_table("select Ref_nama_cukai,Ref_kadar,Ref_kod_cukai From KW_Ref_Tetapan_cukai where Ref_kod_cukai='" + DDcukaival_1 + "'");
                                    if (sel_gst.Rows.Count != 0)
                                    {
                                        DDcukaival = sel_gst.Rows[0]["Ref_kadar"].ToString();
                                    }
                                    else
                                    {
                                        DDcukaival = "0";
                                    }

                                    string DDcukai1 = (g1.FindControl("ddresoth") as DropDownList).SelectedItem.Text;
                                    DataTable dtcuk1 = new DataTable();
                                    dtcuk1 = Dbcon.Ora_Execute_table("select Ref_kod_cukai,Ref_kod_akaun,kat_akaun from KW_Ref_Tetapan_cukai where  Ref_nama_cukai='" + DDcukai1 + "'");
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
                                    string DDcukaival1_1 = (g1.FindControl("ddresoth") as DropDownList).SelectedItem.Value;
                                    string DDcukaival1 = string.Empty;
                                    DataTable sel_gst1 = new DataTable();
                                    sel_gst1 = Dbcon.Ora_Execute_table("select Ref_nama_cukai,Ref_kadar,Ref_kod_cukai From KW_Ref_Tetapan_cukai where Ref_kod_cukai='" + DDcukaival1_1 + "'");
                                    if (sel_gst1.Rows.Count != 0)
                                    {
                                        DDcukaival1 = sel_gst1.Rows[0]["Ref_kadar"].ToString();
                                    }
                                    else
                                    {
                                        DDcukaival1 = "0";
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
                                    if ((g1.FindControl("chkres") as CheckBox).Checked == true)
                                    {
                                        chk = "Y";
                                        if (DDcukaival != "--- PILIH ---")
                                        {
                                            val = DDcukaival;
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
                                            val = "0";
                                            tgst = Convert.ToDecimal("0.00");
                                        }
                                        if (DDcukaival1 != "--- PILIH ---")
                                        {
                                            val1 = DDcukaival1;
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
                                            if (gamt != "")
                                            {
                                                tgst = Convert.ToDecimal(gamt);
                                            }
                                            else
                                            {
                                                tgst = 0;
                                            }
                                        }
                                        else
                                        {
                                            val = "0";
                                            tgst = Convert.ToDecimal("0.00");
                                        }
                                        if (DDcukaival1 != "--- PILIH ---")
                                        {
                                            val1 = DDcukaival1;
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
                                            val1 = "0";
                                            tgst1 = Convert.ToDecimal("0.00");
                                        }
                                    }
                                    string DD = (g1.FindControl("ddkodres") as DropDownList).SelectedItem.Value;
                                    string DDN = (g1.FindControl("ddkodres") as DropDownList).SelectedItem.Text;
                                    DataTable dtkat1 = new DataTable();
                                    dtkat1 = Dbcon.Ora_Execute_table("select kat_akaun,ct_kod_industry from KW_Ref_Carta_Akaun where kod_akaun='" + DD + "'");

                                    string vv1 = string.Empty, vv2 = string.Empty;

                                    DataTable dtkat1_2 = new DataTable();
                                    dtkat1_2 = Dbcon.Ora_Execute_table("select kat_akaun,ct_kod_industry from KW_Ref_Carta_Akaun where kod_akaun='" + taxkod + "'");

                                    if(dtkat1_2.Rows.Count != 0)
                                    {
                                        vv1 = dtkat1_2.Rows[0][1].ToString();
                                    }

                                    DataTable dtkat1_3 = new DataTable();
                                    dtkat1_3 = Dbcon.Ora_Execute_table("select kat_akaun,ct_kod_industry from KW_Ref_Carta_Akaun where kod_akaun='" + taxkod1 + "'");

                                    if (dtkat1_3.Rows.Count != 0)
                                    {
                                        vv2 = dtkat1_3.Rows[0][1].ToString();
                                    }

                                    dt = Dbcon.Ora_Execute_table("insert into kw_penerimaan_resit_item values ('" + ddakaun.SelectedItem.Value + "','" + dtnama.Rows[0][1].ToString() + "','" + txtnorst3_3.Text + "','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "','" + dd_Betuk1.SelectedItem.Value + "','" + GP_rno.Text + "','" + DDN + "','" + ddnamapela3.SelectedItem.Value + "','" + txtjum.Text + "','" + DD + "','" + item + "','" + keter + "','" + unit + "','" + qty + "','" + fre + "','" + chk + "','" + val + "','" + val1 + "','" + cuk + "','" + cuk1 + "','" + tgst + "','" + tgst1 + "','" + total + "','" + ttotal + "','A','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','','','" + rujuk + "','" + txtname.Text + "','" + dd_selvalue.SelectedValue + "')");
                                    dt3 = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + DD + "','','" + total + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + dtkat1.Rows[0][0].ToString() + "','" + txtnorst3_3.Text + "','" + rujuk + "','','" + TextBox11.Text + "','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "','" + keter + "','','" + dtnama.Rows[0][1].ToString() + "','','','A','" + dtkat1.Rows[0][1].ToString() + "')");
                                    
                                    //update carta akaun

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
                                    // end
                                    dt2 = Dbcon.Ora_Execute_table("update KW_Opening_Balance set ending_amt=ending_amt + " + ttotal + " ,upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where kod_akaun='" + DD + "' and opening_year='" + DateTime.Now.ToString("yyyy") + "' ");
                                    if (tgst != 0)
                                    {
                                        dt3 = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + taxkod + "','','" + tgst + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + katkod + "','" + txtnorst3_3.Text + "','" + rujuk + "','" + cuk + "','" + TextBox11.Text + "','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "','" + keter + "','','" + dtnama.Rows[0][1].ToString() + "','','','A','"+ vv1 + "')");
                                    }

                                    if (tgst1 != 0)
                                    {
                                        dt3 = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + taxkod1 + "','','" + tgst1 + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + katkod1 + "','" + txtnorst3_3.Text + "','" + rujuk + "','" + cuk1 + "','" + TextBox11.Text + "','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "','" + keter + "','','" + dtnama.Rows[0][1].ToString() + "','','','A','" + vv1 + "')");
                                    }

                                }
                                Div10.Visible = false;
                                Div11.Visible = true;
                                //Bindinvois();
                                //Bindcredit();
                                //Binddedit();
                                //Bindresit();
                                tab2();
                                reset();
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Resit Dibuat Berjaya.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                            }

                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Amount.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Betuk Bayaran.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Tarikh Resit.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan No Resit.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Kod Akaun (Debit).',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }

        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila pilih Untuk Akuan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
        tab2();
        Bindinvois();
        Bindresit();
        Bindcredit();
        Binddedit();
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
                            dtnama = Dbcon.Ora_Execute_table("select Ref_no_syarikat,Ref_kod_akaun,pel_kod_industry from  KW_Ref_Pelanggan where Ref_nama_syarikat='" + ddpela2.SelectedItem.Text + "'");
                            DataTable dtkat = new DataTable();
                            dtkat = Dbcon.Ora_Execute_table("select kat_akaun from KW_Ref_Carta_Akaun where kod_akaun='" + dtnama.Rows[0][1].ToString() + "'");

                            decimal Gtotal = 0;
                            int count = 0;
                            string rcount = string.Empty;
                            DataTable gen_invkd = new DataTable();
                            gen_invkd = Dbcon.Ora_Execute_table("select no_Rujukan from KW_Penerimaan_Credit_item where no_Rujukan='" + txtnoruju.Text + "'");
                            if (gen_invkd.Rows.Count > 0)
                            {
                                DataTable dt_kfmt1 = Dbcon.Ora_Execute_table("select doc_type_cd,format as fmt,SUBSTRING(cur_format, 1, 10) as lfrmt1,SUBSTRING(cur_format, 11, 4) as lfrmt2,cur_format as cfmt from KW_Format_Nombor_rujukan where doc_type_cd='05' and Status='A'");
                                if (dt_kfmt1.Rows.Count != 0)
                                {
                                    if (dt_kfmt1.Rows[0]["cfmt"].ToString() == "")
                                    {
                                        txtnoruju_1.Text = dt1.Rows[0]["fmt"].ToString();
                                    }
                                    else
                                    {
                                        int seqno = Convert.ToInt32(dt_kfmt1.Rows[0]["lfrmt2"].ToString());
                                        int newNumber = seqno + 1;
                                        uniqueId = newNumber.ToString(dt_kfmt1.Rows[0]["lfrmt1"].ToString() + "0000");
                                        txtnoruju_1.Text = uniqueId;
                                    }

                                }
                                else
                                {
                                    DataTable dt_kfmt2 = Dbcon.Ora_Execute_table("select    ISNULL(max(SUBSTRING(no_Rujukan,13,2000)),'0') from KW_Penerimaan_Credit_item");
                                    if (dt_kfmt2.Rows.Count > 0)
                                    {
                                        int seqno = Convert.ToInt32(dt_kfmt2.Rows[0][0].ToString());
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
                                    dt1 = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "','','" + TextBox18.Text + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + dtkat.Rows[0][0].ToString() + "','" + txtnoruju_1.Text + "','" + ddinv.SelectedItem.Text + "','','" + TextBox18.Text + "','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "','" + KEt + "','','" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "','" + ddpro1.SelectedItem.Value + "','','A','"+ dtnama.Rows[0]["pel_kod_industry"].ToString() + "')");

                                    DataTable chk_carta1 = new DataTable();
                                    chk_carta1 = Dbcon.Ora_Execute_table("select kat_akaun,kod_akaun,KW_Debit_amt,KW_kredit_amt,Kw_open_amt from KW_Ref_Carta_Akaun where kod_akaun='" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "'");
                                    if (chk_carta1.Rows.Count != 0)
                                    {
                                        double amt_ope2 = 0;
                                        double amt_deb2 = 0;
                                        double amt_deb3 = 0;

                                        DataTable chk_kategory1 = new DataTable();
                                        chk_kategory1 = Dbcon.Ora_Execute_table("select kat_cd,kat_akuan,bal_type from KW_Kategori_akaun where kat_cd='" + chk_carta1.Rows[0]["kat_akaun"].ToString() + "'");

                                        amt_deb2 = (double.Parse(chk_carta1.Rows[0]["KW_kredit_amt"].ToString()) + double.Parse(TextBox18.Text));

                                        if (chk_kategory1.Rows.Count != 0)
                                        {
                                            amt_deb3 = -(double.Parse(TextBox18.Text));
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
                                string kat1, kat2, kat3, ind1, ind2, ind3;



                                dt = Dbcon.Ora_Execute_table("insert into KW_Penerimaan_Credit_item values('" + ddakaun.SelectedItem.Value + "','" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "','" + ddinv.SelectedItem.Text + "','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "','" + txtnoruju_1.Text + "','" + tDate1.ToString("yyyy/MM/dd hh:mm:ss") + "','" + ddpro1.SelectedItem.Value + "','" + ddinvday.SelectedItem.Text + "','" + DD + "','" + KEt + "','" + chk + "','" + DDcukaival + "','" + DDcukaival2 + "','" + DDcukai + "','" + DDN1 + "','" + tgst1 + "','" + tgst + "','" + total + "','" + ttotal + "','A','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','','')");
                                DataTable dt_upd_format = new DataTable();
                                dt_upd_format = Dbcon.Ora_Execute_table("update KW_Format_Nombor_rujukan set cur_format='" + txtnoruju_1.Text + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where doc_type_cd='05' and Status = 'A'");
                                DataTable dtkat1 = new DataTable();
                                dtkat1 = Dbcon.Ora_Execute_table("select kat_akaun,ct_kod_industry from KW_Ref_Carta_Akaun where kod_akaun='" + val + "'");
                                DataTable dtkat3 = new DataTable();
                                dtkat3 = Dbcon.Ora_Execute_table("select kat_akaun,ct_kod_industry from KW_Ref_Carta_Akaun where kod_akaun='" + val1 + "'");
                                DataTable dtkat2 = new DataTable();
                                dtkat2 = Dbcon.Ora_Execute_table("select kat_akaun,ct_kod_industry from KW_Ref_Carta_Akaun where kod_akaun='" + DD + "'");
                                if (dtkat1.Rows.Count > 0)
                                {
                                    kat1 = dtkat1.Rows[0][0].ToString();
                                    ind1 = dtkat1.Rows[0][1].ToString();
                                }
                                else
                                {
                                    kat1 = "0";
                                    ind1 = "";
                                }
                                if (dtkat3.Rows.Count > 0)
                                {
                                    kat3 = dtkat3.Rows[0][0].ToString();
                                    ind3 = dtkat3.Rows[0][1].ToString();
                                }
                                else
                                {
                                    kat3 = "0";
                                    ind3 = "";
                                }

                                if (dtkat2.Rows.Count > 0)
                                {
                                    kat2 = dtkat2.Rows[0][0].ToString();
                                    ind2 = dtkat2.Rows[0][1].ToString();
                                }
                                else
                                {
                                    kat2 = "0";
                                    ind2 = "";
                                }

                                dt2 = Dbcon.Ora_Execute_table("update KW_Opening_Balance set ending_amt=ending_amt + " + TextBox18.Text + " ,upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where kod_akaun='" + DD + "' and opening_year='" + DateTime.Now.ToString("yyyy") + "' ");
                                dt1 = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + DD + "','" + total + "','','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + kat2 + "','" + txtnoruju_1.Text + "','" + ddinv.SelectedItem.Text + "','','" + TextBox18.Text + "','" + tDate1.ToString("yyyy/MM/dd hh:mm:ss") + "','" + KEt + "','','" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "','" + ddpro1.SelectedItem.Value + "','','A','" + ind2 + "')");
                                //update certa Akaun

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
                                            amt_ope1 = (double.Parse(chk_carta.Rows[0]["Kw_open_amt"].ToString()) + (amt_deb2_1));
                                        }
                                    }
                                    DataTable upd_carta = new DataTable();
                                    upd_carta = Dbcon.Ora_Execute_table("UPDATE KW_Ref_Carta_Akaun set KW_Debit_amt='" + amt_deb1 + "',Kw_open_amt='" + amt_ope1 + "' where   kod_akaun='" + DD + "'");
                                }

                                //end

                                if (tgst1 != 0)
                                {
                                    dt3 = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + val + "','" + tgst1 + "','','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + kat1 + "','" + txtnoruju_1.Text + "','" + ddinv.SelectedItem.Text + "','" + ddcuk + "','" + TextBox18.Text + "','" + tDate1.ToString("yyyy/MM/dd hh:mm:ss") + "','" + KEt + "','','" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "','" + ddpro1.SelectedItem.Value + "','','A','"+ ind1 +"')");
                                }

                                if (tgst != 0)
                                {
                                    dt3 = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + val1 + "','" + tgst + "','','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + kat3 + "','" + txtnoruju_1.Text + "','" + ddinv.SelectedItem.Text + "','" + ddcuk1 + "','" + TextBox18.Text + "','" + tDate1.ToString("yyyy/MM/dd hh:mm:ss") + "','" + KEt + "','','" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "','" + ddpro1.SelectedItem.Value + "','','A','"+ ind3 +"')");
                                }

                                //  dt2 = Dbcon.Ora_Execute_table("update KW_Penerimaan_invois_item set Overall=Overall - '" + TextBox18.Text + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy / MM / dd hh: mm:ss") + "' where no_invois='" + ddinv.SelectedItem.Text + "' ");

                            }
                            dt2 = Dbcon.Ora_Execute_table("update KW_Penerimaan_invois set jumlah=jumlah - " + TextBox18.Text + " , Overall= Overall - " + TextBox18.Text + "  ,upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where no_invois='" + ddinv.SelectedItem.Text + "'");
                            dt3 = Dbcon.Ora_Execute_table("update KW_Penerimaan_payment set jumlah_baki=jumlah_baki - " + TextBox18.Text + " ,upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "'  where no_invois='" + ddinv.SelectedItem.Text + "'");
                            //Bindinvois();
                            //Bindcredit();
                            //Binddedit();
                            //Bindresit();
                            th2.Visible = false;
                            th1.Visible = true;
                            tab3();
                            reset();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Nota Kredit Dibuat Berjaya.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                            //if (txtnoruju.Text != "")
                            //{
                            //    Session["dbtno"] = txtnoruju.Text;
                            //    Response.Redirect("KW_Penerimaan_credit_rptview.aspx");


                            //}

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Please select the Tarikh Kredit.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Please select the Tarikh Invois.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Please Enter the Nombor Rujukan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Please select the Nama Pelanggan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila pilih Untuk Akuan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
        tab3();
        Bindinvois();
        Bindresit();
        Bindcredit();
        Binddedit();
    }


    protected void Button3_Click(object sender, EventArgs e)
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
                            dtnama = Dbcon.Ora_Execute_table("select Ref_no_syarikat,Ref_kod_akaun,pel_kod_industry from  KW_Ref_Pelanggan where Ref_nama_syarikat='" + ddpela3.SelectedItem.Text + "'");
                            DataTable dtkat = new DataTable();
                            dtkat = Dbcon.Ora_Execute_table("select kat_akaun from KW_Ref_Carta_Akaun where kod_akaun='" + dtnama.Rows[0][1].ToString() + "'");
                            
                            //debit kod

                            DataTable gen_invkd = new DataTable();
                            gen_invkd = Dbcon.Ora_Execute_table("select no_Rujukan from KW_Penerimaan_Debit_item where no_Rujukan='" + txtnoruj2.Text + "'");
                            if (gen_invkd.Rows.Count > 0)
                            {
                                DataTable dtdbt_1 = Dbcon.Ora_Execute_table("select doc_type_cd,format as fmt,SUBSTRING(cur_format, 1, 10) as lfrmt1,SUBSTRING(cur_format, 11, 4) as lfrmt2,cur_format as cfmt from KW_Format_Nombor_rujukan where doc_type_cd='06' and Status='A'");
                                if (dtdbt_1.Rows.Count != 0)
                                {
                                    if (dtdbt_1.Rows[0]["cfmt"].ToString() == "")
                                    {
                                        txtnoruj2_2.Text = dtdbt_1.Rows[0]["fmt"].ToString();                                        
                                    }
                                    else
                                    {
                                        int seqno = Convert.ToInt32(dtdbt_1.Rows[0]["lfrmt2"].ToString());
                                        int newNumber = seqno + 1;
                                        uniqueId = newNumber.ToString(dtdbt_1.Rows[0]["lfrmt1"].ToString() + "0000");
                                        txtnoruj2_2.Text = uniqueId;                                        
                                    }

                                }
                                else
                                {
                                    DataTable dtdbt_2 = Dbcon.Ora_Execute_table("select   ISNULL(max(SUBSTRING(no_Rujukan,13,2000)),'0') from KW_Penerimaan_Debit_item ");
                                    if (dtdbt_2.Rows.Count > 0)
                                    {
                                        int seqno = Convert.ToInt32(dtdbt_2.Rows[0][0].ToString());
                                        int newNumber = seqno + 1;
                                        uniqueId = newNumber.ToString("KTH-DBT" + "-" + DateTime.Now.ToString("yy") + "-" + "0000");
                                        txtnoruj2_2.Text = uniqueId;
                                    }
                                    else
                                    {
                                        int newNumber = Convert.ToInt32(uniqueId) + 1;
                                        uniqueId = newNumber.ToString("KTH-DBT" + "-" + DateTime.Now.ToString("yy") + "-" + "0000");
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
                                    dt1 = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "','" + TextBox5.Text + "','','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + dtkat.Rows[0][0].ToString() + "','" + txtnoruj2_2.Text + "','" + ddinv2.SelectedItem.Text + "','','" + TextBox5.Text + "','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "','" + KEt + "','','" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "','" + ddpro2.SelectedItem.Value + "','','A','" + dtnama.Rows[0]["pel_kod_industry"].ToString() + "')");

                                     DataTable chk_carta = new DataTable();
                                chk_carta = Dbcon.Ora_Execute_table("select kat_akaun,kod_akaun,KW_Debit_amt,KW_kredit_amt,Kw_open_amt from KW_Ref_Carta_Akaun where kod_akaun='" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "'");
                                if(chk_carta.Rows.Count != 0)
                                {
                                    double amt_ope1 = 0;
                                    double amt_deb1 = 0;
                                        double amt_deb2_1 = 0;

                                        DataTable chk_kategory = new DataTable();
                                    chk_kategory = Dbcon.Ora_Execute_table("select kat_cd,kat_akuan,bal_type from KW_Kategori_akaun where kat_cd='"+ chk_carta.Rows[0]["kat_akaun"].ToString() + "'");

                                    amt_deb1 = ( double.Parse(chk_carta.Rows[0]["KW_Debit_amt"].ToString()) + double.Parse(TextBox5.Text));

                                    if (chk_kategory.Rows.Count != 0)
                                    {
                                        if (chk_kategory.Rows[0]["bal_type"].ToString() == "D")
                                        {
                                            amt_ope1 = (double.Parse(chk_carta.Rows[0]["Kw_open_amt"].ToString()) + double.Parse(TextBox5.Text));
                                        }
                                        else
                                        {
                                                amt_deb2_1 = (double.Parse(TextBox5.Text));
                                                amt_ope1 = (double.Parse(chk_carta.Rows[0]["Kw_open_amt"].ToString()) + (amt_deb2_1));
                                            }
                                    }
                                    DataTable upd_carta = new DataTable();
                                    upd_carta = Dbcon.Ora_Execute_table("UPDATE KW_Ref_Carta_Akaun set KW_Debit_amt='"+ amt_deb1 + "',Kw_open_amt='"+ amt_ope1 + "' where   kod_akaun='" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "'");
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
                                string kat1, kat2, kat3, ind1, ind2, ind3;

                                dt = Dbcon.Ora_Execute_table("insert into KW_Penerimaan_Debit_item values('" + ddakaun.SelectedItem.Value + "','" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "','" + ddinv2.SelectedItem.Text + "','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "','" + txtnoruj2_2.Text + "','" + tDate1.ToString("yyyy/MM/dd hh:mm:ss") + "','" + ddpro2.SelectedItem.Value + "','" + ddinvday2.SelectedItem.Text + "','" + DD + "','" + KEt + "','" + chk + "','" + DDcukaival + "','" + DDcukaival2 + "','" + DDcukai + "','" + DDN1 + "','" + tgst1 + "','" + tgst + "','" + total + "','" + ttotal + "','A','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','','')");
                                DataTable dt_upd_format = new DataTable();
                                dt_upd_format = Dbcon.Ora_Execute_table("update KW_Format_Nombor_rujukan set cur_format='" + txtnoruj2_2.Text + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where doc_type_cd='06' and Status = 'A'");
                                                             
                                DataTable dtkat1 = new DataTable();
                                dtkat1 = Dbcon.Ora_Execute_table("select kat_akaun,ct_kod_industry from KW_Ref_Carta_Akaun where kod_akaun='" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "'");
                                DataTable dtkat3 = new DataTable();
                                dtkat3 = Dbcon.Ora_Execute_table("select kat_akaun,ct_kod_industry from KW_Ref_Carta_Akaun where kod_akaun='" + val1 + "'");
                                DataTable dtkat2 = new DataTable();
                                dtkat2 = Dbcon.Ora_Execute_table("select kat_akaun,ct_kod_industry from KW_Ref_Carta_Akaun where kod_akaun='" + DD + "'");
                                if (dtkat1.Rows.Count > 0)
                                {
                                    kat1 = dtkat1.Rows[0][0].ToString();
                                    ind1 = dtkat1.Rows[0][1].ToString();
                                }
                                else
                                {
                                    kat1 = "0";
                                    ind1 = "";
                                }
                                if (dtkat3.Rows.Count > 0)
                                {
                                    kat3 = dtkat3.Rows[0][0].ToString();
                                    ind3 = dtkat3.Rows[0][1].ToString();
                                }
                                else
                                {
                                    kat3 = "0";
                                    ind3 = "";
                                }

                                if (dtkat2.Rows.Count > 0)
                                {
                                    kat2 = dtkat2.Rows[0][0].ToString();
                                    ind2 = dtkat2.Rows[0][1].ToString();
                                }
                                else
                                {
                                    kat2 = "0";
                                    ind2 = "";
                                }
                                dt1 = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + DD + "','','" + TextBox5.Text + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + kat2 + "','" + txtnoruj2_2.Text + "','" + ddinv2.SelectedItem.Text + "','','','" + tDate1.ToString("yyyy/MM/dd hh:mm:ss") + "','" + KEt + "','','" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "','" + ddpro2.SelectedItem.Value + "','','A','"+ind2+"')");

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

                                    amt_deb2 = (double.Parse(chk_carta1.Rows[0]["KW_kredit_amt"].ToString()) + double.Parse(TextBox5.Text));

                                    if (chk_kategory1.Rows.Count != 0)
                                    {
                                        amt_deb3 = -(double.Parse(TextBox5.Text));
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

                                //end

                                if (tgst1 != 0)
                                {
                                    dt3 = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + val + "','','" + tgst1 + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + kat1 + "','" + txtnoruj2_2.Text + "','" + ddinv2.SelectedItem.Text + "','" + ddcuk + "','" + TextBox5.Text + "','" + tDate1.ToString("yyyy/MM/dd hh:mm:ss") + "','" + KEt + "','','" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "','" + ddpro2.SelectedItem.Value + "','','A','" + ind1 + "')");
                                }

                                if (tgst != 0)
                                {
                                    dt3 = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + val1 + "','','" + tgst + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + kat3 + "','" + txtnoruj2_2.Text + "','" + ddinv2.SelectedItem.Text + "','" + ddcuk1 + "','" + TextBox5.Text + "','" + tDate1.ToString("yyyy/MM/dd hh:mm:ss") + "','" + KEt + "','','" + dtnama.Rows[0]["Ref_kod_akaun"].ToString() + "','" + ddpro2.SelectedItem.Value + "','','A','" + ind3 + "')");
                                }

                            }
                            dt2 = Dbcon.Ora_Execute_table("update KW_Penerimaan_invois set jumlah=jumlah + " + TextBox5.Text + " , Overall= Overall + " + TextBox5.Text + "  ,upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where no_invois='" + ddinv2.SelectedItem.Text + "'");
                            dt3 = Dbcon.Ora_Execute_table("update KW_Penerimaan_payment set jumlah_baki=jumlah_baki + " + TextBox5.Text + " ,upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "'  where no_invois='" + ddinv2.SelectedItem.Text + "'");
                            fr2.Visible = false;
                            fr1.Visible = true;
                            //Bindinvois();
                            //Bindcredit();
                            //Binddedit();
                            //Bindresit();
                            tab4();
                            reset();
                            //if (txtnoruj2.Text != "")
                            //{
                            //    Session["dbtno"] = txtnoruj2.Text;
                            //    Response.Redirect("KW_Penerimaan_debit_rptview.aspx");


                            //}
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Nota Debit Dibuat Berjaya.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Please select the Tarikh Debit.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Please select the Tarikh Invois.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Please Enter the Nombor Rujukan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Please select the  Nama Pelanggan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila pilih Untuk Akuan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
        tab4();
        Bindinvois();
        Bindresit();
        Bindcredit();
        Binddedit();
    }

    //label click events
    protected void lblSubItemName_Click(object sender, EventArgs e)
    {

        LinkButton btn = (LinkButton)sender;
        string[] CommadArgument = btn.CommandArgument.Split(',');
        CommandArgument1 = CommadArgument[0];
        lnk_clk();

    }

    void lnk_clk()
    {
        string ssv1 = string.Empty;
        if (CommandArgument1 != "")
        {
            ssv1 = CommandArgument1;
        }
        else
        {
            ssv1 = Session["Invoisno"].ToString();
        }

        inv_tab1.Visible = true;
        string getsess_val = string.Empty;

        Button14.Visible = true;
        Button2.Visible = false;
        Div7.Visible = true;
        Div9.Visible = false;
        tab1();
        Bindresit();
        Bindcredit();
        Binddedit();
        Li2.Visible = true;
        Gridview3.Enabled = false;
        DataTable dt = new DataTable();
        dt = Dbcon.Ora_Execute_table("select untuk_akaun,nama_pelanggan,no_invois,Format(tarikh_invois,'dd/MM/yyyy') tarikh_invois,project_kod,days,kod_akauan,item,keterangan,Format(unit,'#,##,###.00') unit,quantiti,gst,Format(Overall,'#,##,###.00') jumlah,tax from KW_Penerimaan_invois_item where no_invois='" + ssv1 + "' ");

        if (dt.Rows.Count > 0)
        {

            ddakaun.Attributes.Add("disabled", "disabled");
            ddpela1.Attributes.Add("disabled", "disabled");
            txtnoinvois1.Attributes.Add("disabled", "disabled");
            txttarikhinvois1.Attributes.Add("disabled", "disabled");
            ddpro.Attributes.Add("disabled", "disabled");
            dddays.Attributes.Add("disabled", "disabled");

            ddakaun.SelectedValue = dt.Rows[0][0].ToString();
            ddpela1.SelectedValue = dt.Rows[0][1].ToString();
            //DataTable dtq = new DataTable();
            //dtq = Dbcon.Ora_Execute_table("select Ref_nama_syarikat from KW_Ref_Pelanggan where Ref_kod_akaun='" + + "'");
            TextBox qty = (TextBox)grdpay.Rows[0].FindControl("txtket");
            qty.Text = ddpela1.SelectedItem.Text;
            txtnoinvois1.Text = dt.Rows[0][2].ToString();
            Session["Invoisno"] = txtnoinvois1.Text;
            txttarikhinvois1.Text = dt.Rows[0][3].ToString();
            ddpro.SelectedValue = dt.Rows[0][4].ToString();
            dddays.SelectedValue = dt.Rows[0][5].ToString();



            DataTable dt1 = new DataTable();
            Gridview1.Visible = false;
            Gridview3.Visible = true;
            dt1 = Dbcon.Ora_Execute_table("select m1.kod_akauan,s1.nama_akaun,s2.Ref_nama_cukai as othgstname,s3.Ref_nama_cukai as gstname,m1.rujukan,m1.no_po,m1.item,m1.keterangan,m1.unit unit,m1.quantiti,m1.gst,case when m1.discount = '0.00' then '0.00' else m1.discount end as discount,m1.jumlah jumlah,m1.Overall Overall,CAST(CASE m1.tax WHEN 'Y' THEN 1 ELSE 0 END AS BIT) AS tax,m1.gsttype,m1.othgsttype,m1.gstjumlah,m1.othgstjumlah, ISNUll((I1.jumlah - sum(P1.jumlah)),'0.00') as Baki from KW_Penerimaan_invois_item  m1 left join KW_Ref_Carta_Akaun s1 on s1.kod_akaun=m1.kod_akauan left join KW_Ref_Tetapan_cukai s2 on s2.Ref_kod_cukai=m1.othgsttype left join KW_Ref_Tetapan_cukai s3 on s3.Ref_kod_cukai=m1.gsttype left join KW_Penerimaan_invois I1 on I1.no_invois=m1.no_invois left join KW_Penerimaan_payment P1 on p1.no_invois=m1.no_invois where m1.no_invois='" + ssv1 + "' group by m1.kod_akauan,s1.nama_akaun,s2.Ref_nama_cukai,s3.Ref_nama_cukai,m1.rujukan,m1.no_po,m1.item,m1.keterangan,m1.unit,m1.quantiti,m1.gst,m1.discount,m1.jumlah,m1.Overall,m1.tax,m1.gsttype,m1.othgsttype,m1.gstjumlah,m1.othgstjumlah,I1.jumlah");
            Gridview3.DataSource = dt1;
            Gridview3.DataBind();






            DataTable dt3 = new DataTable();
            Gridview1.Visible = false;
            Gridview3.Visible = true;
            grdpay.Visible = true;
            dt3 = Dbcon.Ora_Execute_table("select kod_akaun,FORMAT(tarikh_Payment,'dd/MM/yyyy') tarikh,no_rujukan,no_rujukan2,keteragan,Bentuk_penerimaan, jumlah jumlah from KW_Penerimaan_payment where no_invois='" + ssv1 + "' and tarikh_Payment !='1900-01-01 00:00:00.000' ");
            grdpayhis.DataSource = dt3;
            grdpayhis.DataBind();
            if (dt3.Rows.Count > 0)
            {
                his1.Visible = true;
            }
            else
            {
                his1.Visible = false;
            }
            DataTable dt2 = new DataTable();
            dt2 = Dbcon.Ora_Execute_table("select ISNULL((I.jumlah- sum(p.jumlah)),'0.00') as Baki,I.jumlah as jum from KW_Penerimaan_invois I left join KW_Penerimaan_payment p on I.no_invois=p.no_invois  where I.no_invois='" + ssv1 + "' group by I.jumlah ");
            if (dt2.Rows.Count > 0)
            {
                foreach (GridViewRow g2 in grdpay.Rows)
                {
                    (g2.FindControl("TextBox4") as TextBox).Text = double.Parse(dt2.Rows[0][0].ToString()).ToString("C").Replace("$","").Replace("RM", "");
                }

                TextBox14.Text = double.Parse(dt2.Rows[0][1].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                TextBox14.Attributes.Add("disabled", "disabled");
            }
        }
    }

    protected void lblSubItem_Click(object sender, EventArgs e)
    {

        LinkButton btn = (LinkButton)sender;
        string[] CommadArgument = btn.CommandArgument.Split(',');
        CommandArgument1 = CommadArgument[0];

        rst_crt();
    }

    void rst_crt()
    {
        string ssv1 = string.Empty;
        if (CommandArgument1 != "")
        {
            ssv1 = CommandArgument1;
        }
        else
        {
            ssv1 = Session["resitno"].ToString();
        }

        string getsess_val = string.Empty;
        resit_tab2.Visible = true;
        Button23.Visible = true;
        Button5.Visible = false;
        Button14.Visible = true;
        Gridview15.Enabled = false;
        Div10.Visible = true;
        Div11.Visible = false;
        Bindcredit();
        Binddedit();
        tab2();
        DataTable dt = new DataTable();
        dt = Dbcon.Ora_Execute_table("select untuk_akaun,nama_pelanggan,no_resit,Format(tarikh_resit,'dd/MM/yyyy') tarikh_resit,bentuk_bayaran,no_cek,Format(jumlah_bayaran,'#,##,###.00')jumlah_bayaran,bank_cod,kat_akaun,Akaun_name from kw_penerimaan_resit_item  where no_resit='" + ssv1 + "'");

        if (dt.Rows.Count > 0)
        {
            ddakaun.SelectedValue = dt.Rows[0][0].ToString().Trim();
            ddakaun.Attributes.Add("disabled", "disabled");

            dd_selvalue.SelectedValue = dt.Rows[0][8].ToString().Trim();
            dd_selvalue.Attributes.Add("disabled", "disabled");



            Bind_resit();
            if (dt.Rows[0][7].ToString().Trim() != "")
            {
                ddnamapela3.SelectedValue = dt.Rows[0][7].ToString().Trim();
            }
            ddnamapela3.Attributes.Add("disabled", "disabled");

            txtname.Text = dt.Rows[0][9].ToString().Trim();
            txtname.Attributes.Add("disabled", "disabled");
            DataTable chk_val1 = new DataTable();
            chk_val1 = Dbcon.Ora_Execute_table("select * from KW_Ref_Pelanggan where Ref_kod_akaun='" + dt.Rows[0][1].ToString().Trim() + "'");

            //if (chk_val1.Rows.Count != 0)
            //{
            //    dd_selvalue.SelectedValue = "1";
            //}
            //else
            //{
            //    dd_selvalue.SelectedValue = "0";
            //}
            //dd_selvalue.Attributes.Add("disabled", "disabled");

            txtnorst3.Attributes.Add("readonly", "readonly");
            txtnorst3.Attributes.Add("disabled", "disabled");

            txtnorst3.Text = dt.Rows[0][2].ToString().Trim();
            txtnorst3.Attributes.Add("disabled", "disabled");
            Session["resitno"] = txtnorst3.Text;
            GP_date.Text = dt.Rows[0][3].ToString().Trim();
            GP_date.Attributes.Add("disabled", "disabled");


            dd_Betuk1.SelectedValue = dt.Rows[0][4].ToString().Trim();
            dd_Betuk1.Attributes.Add("disabled", "disabled");

            GP_rno.Text = dt.Rows[0][5].ToString().Trim();
            GP_rno.Attributes.Add("disabled", "disabled");

            txtjum.Text = dt.Rows[0][6].ToString().Trim();
            txtjum.Attributes.Add("disabled", "disabled");
            if (dt.Rows[0][1].ToString().Trim() != "--- PILIH ---")
            {
                ddaka.SelectedValue = dt.Rows[0][1].ToString().Trim();
            }
            ddaka.Attributes.Add("disabled", "disabled");

            DataTable dt1 = new DataTable();
            Gridview4.Visible = false;
            Gridview15.Visible = true;
            dt1 = Dbcon.Ora_Execute_table("select m1.res_Kodakaun,s1.nama_akaun,ISNULL(m1.res_rujukan,'') as res_rujukan,m1.gsttype,s2.Ref_nama_cukai as othgstname,s3.Ref_nama_cukai as gstname,m1.res_item,m1.res_keterangan,Format(m1.res_unit,'#,##,###.00') res_unit,m1.res_quantiti,m1.res_gst,Format(m1.Overall,'#,##,###.00') res_jumlah,CAST(CASE m1.tax WHEN 'Y' THEN 1 ELSE 0 END AS BIT) AS tax,m1.gsttype,m1.othgsttype,m1.gstjumlah,m1.othgstjumlah  from kw_penerimaan_resit_item m1 left join KW_Ref_Carta_Akaun s1 on s1.kod_akaun=m1.res_Kodakaun left join KW_Ref_Tetapan_cukai s2 on s2.Ref_kod_cukai=m1.othgsttype left join KW_Ref_Tetapan_cukai s3 on s3.Ref_kod_cukai=m1.gsttype where m1.no_resit='" + ssv1 + "'");
            Gridview15.DataSource = dt1;
            Gridview15.DataBind();
            DataTable dt2 = new DataTable();
            dt2 = Dbcon.Ora_Execute_table("select Format(Overall,'#,##,###.00') Overall  from KW_Penerimaan_resit where  no_resit='" + ssv1 + "'");
            if (dt2.Rows.Count > 0)
            {
                TextBox11.Text = dt2.Rows[0][0].ToString().Trim();
                TextBox11.Attributes.Add("disabled", "disabled");


            }
        }
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
        dt = Dbcon.Ora_Execute_table("select untuk_akaun,nama_pelanggan,Format(tarikh_invois,'dd/MM/yyyy') tarikh_invois,project_kod,days,kod_akauan,item,keterangan,Format(unit,'#,##,###.00') unit,quantiti,gst,Format(Overall,'#,##,###.00') jumlah,tax from KW_Penerimaan_invois_item where no_invois='" + ssv1 + "' ");
        DataTable dt1 = new DataTable();
        if (dt.Rows.Count > 0)
        {

            ddakaun.Attributes.Add("disabled", "disabled");
            ddpela2.Attributes.Add("disabled", "disabled");
            txtnoruju.Attributes.Add("disabled", "disabled");
            txttcinvois.Attributes.Add("disabled", "disabled");
            ddpro1.Attributes.Add("disabled", "disabled");
            ddinvday.Attributes.Add("disabled", "disabled");

            ddakaun.SelectedValue = dt.Rows[0][0].ToString().Trim();
            ddpela2.SelectedValue = dt.Rows[0][1].ToString().Trim();

            txttcinvois.Text = dt.Rows[0][2].ToString();
            ddpro1.SelectedValue = dt.Rows[0][3].ToString();
            ddinvday.SelectedValue = dt.Rows[0][4].ToString();


            Gridview9.Visible = true;
            Gridview12.Visible = false;
            dt1 = Dbcon.Ora_Execute_table("select m1.kod_akauan,s1.nama_akaun,s2.Ref_nama_cukai as othgstname,s3.Ref_nama_cukai as gstname,m1.rujukan,m1.item,m1.keterangan,m1.unit as unit,m1.quantiti,m1.gst,case when Cast(m1.discount as money) = '0.00' then '0.00' else Cast(m1.discount as money) end as discount,ISNULL(Cast(m1.jumlah as money), '0.00') as jumlah,m1.Overall as Overall,isnull(m1.gstjumlah,'0.00') gstjum,isnull(m1.othgstjumlah,'0.00') othgst,CAST(CASE m1.tax WHEN 'Y' THEN 1 ELSE 0 END AS BIT) AS tax,m1.gsttype,m1.othgsttype,I.JUmlah - sum(p.jumlah) as jumlah_bayaran from KW_Penerimaan_invois_item  m1 left join  KW_Penerimaan_payment p on p.no_invois=m1.no_invois left join  KW_Penerimaan_invois I on I.no_invois=m1.no_invois left join KW_Ref_Carta_Akaun s1 on s1.kod_akaun=m1.kod_akauan left join KW_Ref_Tetapan_cukai s2 on s2.Ref_kod_cukai=m1.othgsttype left join KW_Ref_Tetapan_cukai s3 on s3.Ref_kod_cukai=m1.gsttype where m1.no_invois='" + ssv1 + "'  group by m1.kod_akauan,s1.nama_akaun,s2.Ref_nama_cukai ,s3.Ref_nama_cukai ,m1.rujukan,m1.item,m1.keterangan,m1.unit,m1.quantiti,m1.gst,m1.discount,m1.jumlah,m1.Overall,m1.gstjumlah,m1.othgstjumlah,m1.tax,m1.gsttype,m1.othgsttype,I.jumlah");
            Gridview9.DataSource = dt1;
            Gridview9.DataBind();

            Button15.Visible = true;
            Button22.Visible = false;

            th2.Visible = true;
            th1.Visible = false;

            Bindresit();
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
        dt = Dbcon.Ora_Execute_table("select untuk_akaun,nama_pelanggan,Format(tarikh_invois,'dd/MM/yyyy') tarikh_invois,project_kod,days,kod_akauan,item,keterangan,Format(unit,'#,##,###.00') unit,quantiti,gst,Format(Overall,'#,##,###.00') jumlah,tax from KW_Penerimaan_invois_item where no_invois='" + ssv1 + "' ");

        if (dt.Rows.Count > 0)
        {

            ddakaun.Attributes.Add("disabled", "disabled");
            ddpela3.Attributes.Add("disabled", "disabled");
            txtnoruj2.Attributes.Add("disabled", "disabled");
            txtdtinvois.Attributes.Add("disabled", "disabled");
            ddpro2.Attributes.Add("disabled", "disabled");
            ddinvday2.Attributes.Add("disabled", "disabled");

            ddakaun.SelectedValue = dt.Rows[0][0].ToString().Trim();
            ddpela3.SelectedValue = dt.Rows[0][1].ToString().Trim();

            txtdtinvois.Text = dt.Rows[0][2].ToString();
            ddpro2.SelectedValue = dt.Rows[0][3].ToString();
            ddinvday2.SelectedItem.Text = dt.Rows[0][4].ToString();

            DataTable dt1 = new DataTable();
            Gridview7.Visible = true;

            dt1 = Dbcon.Ora_Execute_table("select m1.kod_akauan,s1.nama_akaun,s2.Ref_nama_cukai as othgstname,s3.Ref_nama_cukai as gstname,m1.rujukan,m1.item,m1.keterangan,cast(m1.unit  as money) unit,m1.quantiti,m1.gst,case when m1.discount = '0.00' then '0.00' else cast(m1.discount  as money) end as discount,cast(m1.jumlah as money) jumlah,cast(m1.Overall as money) Overall,cast(m1.gstjumlah as money) gstjum,cast(m1.othgstjumlah as money) othgst,CAST(CASE m1.tax WHEN 'Y' THEN 1 ELSE 0 END AS BIT) AS tax,m1.gsttype,m1.othgsttype,cast((I.JUmlah - sum(p.jumlah)) as money)    as  jumlah_bayaran from KW_Penerimaan_invois_item  m1 left join  KW_Penerimaan_payment p on p.no_invois=m1.no_invois left join  KW_Penerimaan_invois I on I.no_invois=m1.no_invois left join KW_Ref_Carta_Akaun s1 on s1.kod_akaun=m1.kod_akauan  left join KW_Ref_Tetapan_cukai s2 on s2.Ref_kod_cukai=m1.othgsttype left join KW_Ref_Tetapan_cukai s3 on s3.Ref_kod_cukai=m1.gsttype where m1.no_invois='" + ssv1 + "'  group by m1.kod_akauan,s1.nama_akaun,s2.Ref_nama_cukai ,s3.Ref_nama_cukai ,m1.rujukan,m1.item,m1.keterangan,m1.unit,m1.quantiti,m1.gst,m1.discount,m1.jumlah,m1.Overall,m1.gstjumlah,m1.othgstjumlah,m1.tax,m1.gsttype,m1.othgsttype,I.jumlah ");
            Gridview7.DataSource = dt1;
            Gridview7.DataBind();


            Button3.Visible = true;
            Button21.Visible = false;



            tab4();



        }
        else
        {
            DataTable dt1 = new DataTable();
            dt1 = Dbcon.Ora_Execute_table("select m1.kod_akauan,s1.nama_akaun,s2.Ref_nama_cukai as othgstname,s3.Ref_nama_cukai as gstname,m1.rujukan,m1.item,m1.keterangan,cast(m1.unit as money) unit,m1.quantiti,m1.gst,case when m1.discount = '0.00' then '0.00' else cast(m1.discount as money) end as discount,cast(m1.jumlah as money) jumlah,cast(m1.Overall as money) Overall,cast(m1.gstjumlah as money) gstjum,cast(m1.othgstjumlah as money) othgst,CAST(CASE m1.tax WHEN 'Y' THEN 1 ELSE 0 END AS BIT) AS tax,m1.gsttype,m1.othgsttype from KW_Penerimaan_invois_item  m1 left join KW_Ref_Carta_Akaun s1 on s1.kod_akaun=m1.kod_akauan left join KW_Ref_Tetapan_cukai s2 on s2.Ref_kod_cukai=m1.othgsttype left join KW_Ref_Tetapan_cukai s3 on s3.Ref_kod_cukai=m1.gsttype where m1.no_invois='" + ssv1 + "' ");
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

        th2.Visible = true;
        th1.Visible = false;

        Bindresit();
        Binddedit();
        tab3();
        invois_view();
        DataTable dt = new DataTable();
        dt = Dbcon.Ora_Execute_table("select untuk_akaun,nama_pelanggan,no_Rujukan,Format(tarikh_invois,'dd/MM/yyyy') tarikh_invois ,Format(tarikh_credit,'dd/MM/yyyy') tarikh_credit,project_kod,Format(jumlah,'#,##,###.00') jumlah,no_invois,inv_day from KW_Penerimaan_Credit_item  where no_Rujukan='" + ssv2 + "'");

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

            txttcre.Text = dt.Rows[0][4].ToString();
            txttcre.Attributes.Add("disabled", "disabled");

            ddpro1.SelectedValue = dt.Rows[0][5].ToString();
            ddpro1.Attributes.Add("disabled", "disabled");

            ddinv.SelectedValue = dt.Rows[0][7].ToString();
            ddinv.Attributes.Add("disabled", "disabled");

            if (dt.Rows[0][8].ToString() != "")
            {
                ddinvday.SelectedValue = dt.Rows[0][8].ToString();
            }
            else
            {
                ddinvday.SelectedValue = "--- PILIH ---";
            }
            ddinvday.Attributes.Add("disabled", "disabled");

            Session["dbtno"] = ssv2;

            DataTable dt2 = new DataTable();
            Gridview9.Visible = true;
            dt2 = Dbcon.Ora_Execute_table("select m1.kod_akauan,s1.nama_akaun,s2.Ref_nama_cukai as othgstname,s3.Ref_nama_cukai as gstname,m1.rujukan,m1.item,m1.keterangan,cast(m1.unit as money) unit,m1.quantiti,m1.gst,case when m1.discount = '0.00' then '0.00' else cast(m1.discount as money) end as discount,cast(m1.jumlah as money) jumlah,cast(m1.Overall as money) Overall,cast(m1.gstjumlah as money) gstjum,cast(m1.othgstjumlah as money) othgst,CAST(CASE m1.tax WHEN 'Y' THEN 1 ELSE 0 END AS BIT) AS tax,m1.gsttype,m1.othgsttype,cast((I.JUmlah - sum(p.jumlah)) as money)    as  jumlah_bayaran from KW_Penerimaan_invois_item  m1 left join  KW_Penerimaan_payment p on p.no_invois=m1.no_invois left join  KW_Penerimaan_invois I on I.no_invois=m1.no_invois left join KW_Ref_Carta_Akaun s1 on s1.kod_akaun=m1.kod_akauan left join KW_Ref_Tetapan_cukai s2 on s2.Ref_kod_cukai=m1.othgsttype left join KW_Ref_Tetapan_cukai s3 on s3.Ref_kod_cukai=m1.gsttype where m1.no_invois='" + dt.Rows[0][7].ToString().Trim() + "'  group by m1.kod_akauan,s1.nama_akaun,s2.Ref_nama_cukai ,s3.Ref_nama_cukai ,m1.rujukan,m1.item,m1.keterangan,m1.unit,m1.quantiti,m1.gst,m1.discount,m1.jumlah,m1.Overall,m1.gstjumlah,m1.othgstjumlah,m1.tax,m1.gsttype,m1.othgsttype,I.jumlah");
            Gridview9.DataSource = dt2;
            Gridview9.DataBind();

            Gridview10.Visible = false;

            DataTable dt1 = new DataTable();
            Gridview12.Visible = true;
            dt1 = Dbcon.Ora_Execute_table("select kod_akauan,s1.nama_akaun ,s2.Ref_nama_cukai as othgstname,s3.Ref_nama_cukai as gstname,keterangan,CAST(CASE c.tax WHEN 'Y' THEN 1 ELSE 0 END AS BIT) AS tax,gst,othgst,gsttype,othgsttype, case when c.gstjumlah = '0.00' then '0.00' else Format(c.gstjumlah,'#,##,###.00') end as gstjumlah , case when c.othgstjumlah = '0.00' then '0.00' else Format(c.othgstjumlah,'#,##,###.00') end as othgstjumlah , case when c.jumlah = '0.00' then '0.00' else Format(c.jumlah,'#,##,###.00') end as jumlah , case when c.overall = '0.00' then '0.00' else Format(c.overall,'#,##,###.00') end as overall   from KW_Penerimaan_Credit_item c left join KW_Ref_Carta_Akaun s1 on s1.kod_akaun=c.kod_akauan left join KW_Ref_Tetapan_cukai s2 on s2.Ref_kod_cukai=c.othgsttype left join KW_Ref_Tetapan_cukai s3 on s3.Ref_kod_cukai=c.gsttype where no_Rujukan='" + ssv2 + "'");
            Gridview12.DataSource = dt1;
            Gridview12.DataBind();
            //GrandTotalcre();


            DataTable dt1_jum = new DataTable();
            dt1_jum = Dbcon.Ora_Execute_table("select SUM(Overall) as Overall from KW_Penerimaan_Credit_item where no_Rujukan='" + ssv2 + "'");
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
        debit_tab4.Visible = true;
        Button3.Visible = false;
        Button21.Visible = true;
        Gridview14.Enabled = false;
        fr2.Visible = true;
        fr1.Visible = false;
        //pp15.Visible = true;
        BindKoff1();
        Bindresit();
        Bindcredit();
        tab4();
        DataTable dt = new DataTable();
        dt = Dbcon.Ora_Execute_table("select untuk_akaun,nama_pelanggan,no_Rujukan,Format(tarikh_invois,'dd/MM/yyyy') tarikh_invois,Format(tarikh_credit,'dd/MM/yyyy') tarikh_invois,project_kod,no_invois,inv_day from KW_Penerimaan_Debit_item  where no_Rujukan='" + ssv3 + "'");

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

            ddinvday2.SelectedValue = dt.Rows[0][7].ToString();
            ddinvday2.Attributes.Add("disabled", "disabled");

            Session["dbtno1"] = ssv3;


            DataTable dt2 = new DataTable();
            Gridview7.Visible = true;

            dt2 = Dbcon.Ora_Execute_table("select m1.kod_akauan,s1.nama_akaun,s2.Ref_nama_cukai as othgstname,s3.Ref_nama_cukai as gstname,m1.rujukan,m1.item,m1.keterangan,cast(m1.unit as money) unit,m1.quantiti,m1.gst,case when m1.discount = '0.00' then '0.00' else cast(m1.discount as money) end as discount,cast(m1.jumlah as money) jumlah,cast(m1.Overall as money) Overall,cast(m1.gstjumlah as money) gstjum,cast(m1.othgstjumlah as money) othgst,CAST(CASE m1.tax WHEN 'Y' THEN 1 ELSE 0 END AS BIT) AS tax,m1.gsttype,m1.othgsttype,cast((I.JUmlah - sum(p.jumlah))  as money)    as  jumlah_bayaran from KW_Penerimaan_invois_item  m1 left join  KW_Penerimaan_payment p on p.no_invois=m1.no_invois left join  KW_Penerimaan_invois I on I.no_invois=m1.no_invois left join KW_Ref_Carta_Akaun s1 on s1.kod_akaun=m1.kod_akauan left join KW_Ref_Tetapan_cukai s2 on s2.Ref_kod_cukai=m1.othgsttype left join KW_Ref_Tetapan_cukai s3 on s3.Ref_kod_cukai=m1.gsttype where m1.no_invois='" + dt.Rows[0][6].ToString().Trim() + "'  group by m1.kod_akauan,s1.nama_akaun,s2.Ref_nama_cukai ,s3.Ref_nama_cukai ,m1.rujukan,m1.item,m1.keterangan,m1.unit,m1.quantiti,m1.gst,m1.discount,m1.jumlah,m1.Overall,m1.gstjumlah,m1.othgstjumlah,m1.tax,m1.gsttype,m1.othgsttype,I.jumlah");
            Gridview7.DataSource = dt2;
            Gridview7.DataBind();

            Gridview11.Visible = false;

            DataTable dt1 = new DataTable();

            Gridview14.Visible = true;
          
            dt1 = Dbcon.Ora_Execute_table("select m1.kod_akauan,s1.nama_akaun,s2.Ref_nama_cukai as othgstname,s3.Ref_nama_cukai as gstname,m1.keterangan,case when m1.jumlah = '0.00' then '0.00' else cast(m1.jumlah as money) end as jumlah  , case when m1.Overall = '0.00' then '0.00' else cast(m1.Overall as money) end as Overall   , case when m1.gstjumlah = '0.00' then '0.00' else cast(m1.gstjumlah as money) end as gstjum, case when m1.othgstjumlah = '0.00' then '0.00' else cast(m1.othgstjumlah as money) end as othgst   ,CAST(CASE m1.tax WHEN 'Y' THEN 1 ELSE 0 END AS BIT) AS tax,m1.gsttype,m1.othgsttype    from KW_Penerimaan_Debit_item m1 left join KW_Ref_Carta_Akaun s1 on s1.kod_akaun=m1.kod_akauan left join KW_Ref_Tetapan_cukai s2 on s2.Ref_kod_cukai=m1.othgsttype left join KW_Ref_Tetapan_cukai s3 on s3.Ref_kod_cukai=m1.gsttype where m1.no_Rujukan='" + ssv3 + "'");
            Gridview14.DataSource = dt1;
            Gridview14.DataBind();

            DataTable dt1_jum = new DataTable();
            dt1_jum = Dbcon.Ora_Execute_table("select SUM(Overall) as Overall from KW_Penerimaan_Debit_item where no_Rujukan='" + ssv3 + "'");
            if (dt1_jum.Rows.Count != 0)
            {
                TextBox5.Text = double.Parse(dt1_jum.Rows[0]["Overall"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
            }
            else
            {
                TextBox5.Text = "0.00";
            }
        }
    }

    protected void Button17_Click(object sender, EventArgs e)
    {
        if (ddakaun.SelectedItem.Text != "--- PILIH ---")
        {
            if (ddpela1.SelectedItem.Text != "--- PILIH ---")
            {
                if (txtnoinvois1.Text != "")
                {
                    if (txttarikhinvois1.Text != "")
                    {

                        DateTime tDate = DateTime.ParseExact(txttarikhinvois1.Text, "dd/MM/yyyy", null);
                        DataTable dt = new DataTable();
                        DataTable dtI = new DataTable();
                        DataTable dt2 = new DataTable();
                        DataTable dt3 = new DataTable();
                        userid = Session["New"].ToString();
                        DataTable dt4 = new DataTable();
                        dt3 = Dbcon.Ora_Execute_table("select Max(seq_no)+1 seqno from KW_Carta_Akaun_Payment");
                        foreach (GridViewRow g1 in grdpay.Rows)
                        {
                            string tarikh = (g1.FindControl("TextBox1") as TextBox).Text;

                            DateTime tDate1 = DateTime.ParseExact(tarikh, "dd/MM/yyyy", null);
                            string ruju = (g1.FindControl("TextBox2") as TextBox).Text;
                            string ruju2 = (g1.FindControl("Txtnor2") as TextBox).Text;
                            string ket = (g1.FindControl("txtket") as TextBox).Text;
                            string dd = (g1.FindControl("ddBaya") as DropDownList).SelectedValue;
                            string ddn = (g1.FindControl("ddBaya") as DropDownList).SelectedItem.Text;
                            string kod = (g1.FindControl("ddkodpay") as DropDownList).SelectedItem.Value;
                            string namakod = (g1.FindControl("ddkodpay") as DropDownList).SelectedItem.Text;
                            DataTable dtbank = new DataTable();
                            dtbank = Dbcon.Ora_Execute_table("select  Ref_No_akaun from KW_Ref_Akaun_bank where Ref_nama_akaun='" + namakod + "'");
                            string total = (g1.FindControl("TextBox3") as TextBox).Text;
                            string total1 = (g1.FindControl("TextBox4") as TextBox).Text;
                            DataTable dtkat = new DataTable();
                            dtkat = Dbcon.Ora_Execute_table("select kat_akaun,ct_kod_industry from KW_Ref_Carta_Akaun where kod_akaun='" + kod + "'");
                            DataTable dtkat1 = new DataTable();
                            dtkat1 = Dbcon.Ora_Execute_table("select kat_akaun,kod_akaun,ct_kod_industry from KW_Ref_Carta_Akaun where kod_akaun='" + ddpela1.SelectedValue + "'");

                            dt = Dbcon.Ora_Execute_table("insert into KW_Penerimaan_payment values('" + ddakaun.SelectedItem.Value + "','" + ddpela1.SelectedItem.Value + "','" + txtnoinvois1.Text + "','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "','" + ddpro.SelectedItem.Value + "','" + kod + "','" + dtbank.Rows[0][0].ToString() + "','" + tDate1.ToString("yyyy/MM/dd hh:mm:ss") + "','" + ruju + "','" + dd + "','" + total + "','" + total1 + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','','','" + ruju2 + "','" + ket + "')");
                            dt2 = Dbcon.Ora_Execute_table("select ref_kod_akaun from  KW_Ref_Pelanggan where Ref_nama_syarikat='" + ddpela1.SelectedItem.Text + "'");
                            dtI = Dbcon.Ora_Execute_table("update Kw_opening_balance set ending_amt=ending_amt - " + total + ",upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where kod_akaun='" + dt2.Rows[0][0].ToString() + "' ");
                            //dt2 = Dbcon.Ora_Execute_table("insert into KW_ref_carta_akaun(kat_akaun,nama_akaun,kod_akaun,status,KW_Debit_amt,KW_kredit_amt,crt_id,cr_dt) values('','" + ddpela1.SelectedItem.Text + "','" + ddpela1.SelectedItem.Value + "','D','" + TextBox14.Text + "','','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')");
                            dt3 = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + kod + "','" + total + "','','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + dtkat.Rows[0][0].ToString() + "','" + txtnoinvois1.Text + "','" + ruju + "','','0.00','" + tDate1.ToString("yyyy/MM/dd hh:mm:ss") + "','" + ket + "','" + ruju2 + "','" + ddpela1.SelectedItem.Value + "','" + ddpro.SelectedItem.Value + "','','A','" + dtkat.Rows[0][1].ToString() + "')");

                            DataTable chk_carta = new DataTable();
                            chk_carta = Dbcon.Ora_Execute_table("select kat_akaun,kod_akaun,KW_Debit_amt,KW_kredit_amt,Kw_open_amt from KW_Ref_Carta_Akaun where kod_akaun='" + kod + "'");
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
                                        amt_ope1 = (double.Parse(chk_carta.Rows[0]["Kw_open_amt"].ToString()) + (amt_deb2_1));
                                    }
                                }
                                DataTable upd_carta = new DataTable();
                                upd_carta = Dbcon.Ora_Execute_table("UPDATE KW_Ref_Carta_Akaun set KW_Debit_amt='" + amt_deb1 + "',Kw_open_amt='" + amt_ope1 + "' where   kod_akaun='" + kod + "'");
                            }

                            dt4 = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + dtkat1.Rows[0]["kod_akaun"].ToString() + "','','" + total + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + dtkat1.Rows[0][0].ToString() + "','" + txtnoinvois1.Text + "','" + ruju + "','','0.00','" + tDate1.ToString("yyyy/MM/dd hh:mm:ss") + "','" + ket + "','" + ruju2 + "','" + ddpela1.SelectedItem.Value + "','" + ddpro.SelectedItem.Value + "','','A','"+ dtkat1.Rows[0]["ct_kod_industry"].ToString() + "')");

                            DataTable chk_carta1 = new DataTable();
                            chk_carta1 = Dbcon.Ora_Execute_table("select kat_akaun,kod_akaun,KW_Debit_amt,KW_kredit_amt,Kw_open_amt from KW_Ref_Carta_Akaun where kod_akaun='" + dtkat1.Rows[0]["kod_akaun"].ToString() + "'");
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
                                upd_carta = Dbcon.Ora_Execute_table("UPDATE KW_Ref_Carta_Akaun set KW_kredit_amt='" + amt_deb2 + "',Kw_open_amt='" + amt_ope2 + "' where   kod_akaun='" + dtkat1.Rows[0]["kod_akaun"].ToString() + "'");
                            }


                        }

                        Div7.Visible = false;
                        Div9.Visible = true;
                        Bindinvois();
                        Bindcredit();
                        Binddedit();
                        Bindresit();
                        reset();
                        tab1();

                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Pembayaran Berhasil Dibuat.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);

                    }
                }
            }
        }
    }



    protected void grdpay_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Find the DropDownList in the Row
            DropDownList ddlCountries = (e.Row.FindControl("ddBaya") as DropDownList);
            ddlCountries.DataSource = GetData("select Jenis_bayaran_cd,Jenis_bayaran from KW_Jenis_Cara_bayaran");
            ddlCountries.DataTextField = "Jenis_bayaran";
            ddlCountries.DataValueField = "Jenis_bayaran_cd";
            ddlCountries.DataBind();

            //Add Default Item in the DropDownList
            ddlCountries.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            //Find the DropDownList in the Row
            DropDownList ddlCountries1 = (e.Row.FindControl("ddkodpay") as DropDownList);
            ddlCountries1.DataSource = GetData("select Ref_kod_akaun,Ref_nama_akaun from KW_Ref_Akaun_bank");
            ddlCountries1.DataTextField = "Ref_nama_akaun";
            ddlCountries1.DataValueField = "Ref_kod_akaun";
            ddlCountries1.DataBind();

            //Add Default Item in the DropDownList
            ddlCountries1.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            //Select the Country of Customer in DropDownList

        }
    }


    protected void grdpayhis_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Find the DropDownList in the Row
            DropDownList ddlCountries = (e.Row.FindControl("ddkodBen") as DropDownList);
            ddlCountries.DataSource = GetData("select Jenis_bayaran_cd,Jenis_bayaran from KW_Jenis_Cara_bayaran");
            ddlCountries.DataTextField = "Jenis_bayaran";
            ddlCountries.DataValueField = "Jenis_bayaran_cd";
            ddlCountries.DataBind();

            //Add Default Item in the DropDownList
            ddlCountries.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            string country1 = (e.Row.FindControl("lblBen") as Label).Text;
            if (country1 != "0")
            {
                ddlCountries.Items.FindByValue(country1).Selected = true;
            }

            //Find the DropDownList in the Row
            DropDownList ddlCountries1 = (e.Row.FindControl("ddkodpay") as DropDownList);
            ddlCountries1.DataSource = GetData("select Ref_kod_akaun,Ref_nama_akaun from KW_Ref_Akaun_bank");
            ddlCountries1.DataTextField = "Ref_nama_akaun";
            ddlCountries1.DataValueField = "Ref_kod_akaun";
            ddlCountries1.DataBind();

            //Add Default Item in the DropDownList
            ddlCountries1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            string country2 = (e.Row.FindControl("lblCountry") as Label).Text;
            if (country2 != "0")
            {
                ddlCountries1.Items.FindByValue(country2).Selected = true;
            }


            //Select the Country of Customer in DropDownList

        }
    }

    //protected void dd_Betuk_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (dd_Betuk1.SelectedItem.Text != "tunai" && dd_Betuk1.SelectedItem.Text != "Tunai" && dd_Betuk1.SelectedItem.Text != "TUNAI")
    //    {

    //        string com = "select REf_nama_akaun,ref_no_akaun from KW_Ref_Akaun_bank";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
    //        DataTable dt = new DataTable();
    //        adpt.Fill(dt);
    //        ddaka.DataSource = dt;
    //        ddaka.DataTextField = "REf_nama_akaun";
    //        ddaka.DataValueField = "ref_no_akaun";
    //        ddaka.DataBind();
    //        ddaka.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    //        ddaka.Attributes.Remove("disabled");
    //        GP_rno.Attributes.Remove("disabled");
    //        tab2();
    //    }
    //    else
    //    {

    //        ddaka.Attributes.Add("disabled", "disabled");
    //        GP_rno.Attributes.Add("disabled", "disabled");
    //        tab2();

    //    }
    //}

    protected void clk_txt_show(object sender, EventArgs e)
    {
        if (txtname.Text != "")
        {

            TextBox qty = (TextBox)Gridview4.Rows[0].FindControl("TextBox12");
            qty.Text = txtname.Text;
            tab2();
        }

    }
    protected void ddnamapela3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddnamapela3.SelectedItem.Text != "--- PILIH ---")
        {

            txtname.Text = ddnamapela3.SelectedItem.Text;
            TextBox qty = (TextBox)Gridview4.Rows[0].FindControl("TextBox12");
            qty.Text = ddnamapela3.SelectedItem.Text;
            tab2();
        }

    }

    protected void ddinv_SelectedIndexChanged(object sender, EventArgs e)
    {
        lnk_clk_crt();
    }

    protected void ddpela2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddpela2.SelectedItem.Text != "--- PILIH ---")
        {
            string com = "select no_invois from KW_Penerimaan_invois where nama_pelanggan='" + ddpela2.SelectedItem.Value + "'";
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


    protected void ddpela3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddpela3.SelectedItem.Text != "--- PILIH ---")
        {
            string com = "select no_invois from KW_Penerimaan_invois where nama_pelanggan='" + ddpela3.SelectedItem.Value + "'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddinv.DataSource = dt;
            ddinv.DataTextField = "no_invois";
            ddinv.DataValueField = "no_invois";
            ddinv.DataBind();
            ddinv.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            tab4();
        }
    }

    protected void ddinv2_SelectedIndexChanged(object sender, EventArgs e)
    {
        lnk_clk_Deb();
    }


    protected void Btnkoffclose_Click(object sender, EventArgs e)
    {
        tab3();
        reset();

    }
    protected void Btnkoff1_Click(object sender, EventArgs e)
    {



        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dt5 = new DataTable();
        DataTable dt6 = new DataTable();
        DataTable dt7 = new DataTable();
        string rcount1 = string.Empty;
        int count1 = 0;
        foreach (GridViewRow gvrow in Gridview13.Rows)
        {
            var rb1 = gvrow.FindControl("chkcreoff") as System.Web.UI.WebControls.CheckBox;
            if (rb1.Checked)
            {
                count1++;
            }
            rcount1 = count1.ToString();
        }

        if (rcount1 != "0")
        {

            DateTime tDate = DateTime.ParseExact(txtdtinvois.Text, "dd/MM/yyyy", null);
            foreach (GridViewRow g1 in Gridview13.Rows)
            {

                CheckBox chk = (CheckBox)g1.FindControl("chkcreoff");
                string inv = (g1.FindControl("lblinv") as Label).Text;
                string tarikh = (g1.FindControl("lbltar") as Label).Text;

                DateTime tDate1 = DateTime.ParseExact(tarikh.ToString(), "dd/MM/yyyy", null);
                string sya = (g1.FindControl("lblsya") as Label).Text;
                string total = (g1.FindControl("lbljum") as Label).Text;
                string dd = (g1.FindControl("txtkoffdeb") as TextBox).Text;
                userid = Session["New"].ToString();
                if (chk.Checked == true)
                {
                    if (dd != "")
                    {
                        dt = Dbcon.Ora_Execute_table("select nama_pelanggan from KW_Penerimaan_debit where no_Rujukan='" + txtnoruj2.Text + "'");
                        DataTable dtkat = new DataTable();
                        dtkat = Dbcon.Ora_Execute_table("select kat_akaun from KW_Ref_Carta_Akaun where kod_akaun='" + dt.Rows[0][0].ToString() + "'");
                        DataTable dtkat1 = new DataTable();
                        dtkat1 = Dbcon.Ora_Execute_table("select kat_akaun,kod_akaun from KW_Ref_Carta_Akaun where kod_akaun='" + ddpela3.SelectedValue + "'");
                        dt1 = Dbcon.Ora_Execute_table("insert into KW_Penerimaan_Debit_reduce values('" + ddakaun.SelectedItem.Value + "','" + ddpela3.SelectedItem.Value + "','" + txtnoruj2.Text + "','" + inv + "','" + tDate.ToString("yyyy/MM/dd hh:mm:ss") + "','" + tDate1.ToString("yyyy/MM/dd hh:mm:ss") + "','" + ddpro2.SelectedItem.Value + "','" + total + "','" + dd + "','','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','','')");
                        dt2 = Dbcon.Ora_Execute_table("update KW_Penerimaan_invois set jumlah=jumlah + " + dd + " , Overall= Overall + " + dd + "  ,upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where no_invois='" + inv + "'");
                        dt3 = Dbcon.Ora_Execute_table("update KW_Penerimaan_payment set jumlah_baki=jumlah_baki + " + dd + " ,upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "'  where no_invois='" + inv + "'");
                        dt4 = Dbcon.Ora_Execute_table("update KW_Penerimaan_debit set jumlah=jumlah - " + dd + " ,upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "'  where no_Rujukan='" + txtnoruj2.Text + "'");
                        dt5 = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + dt.Rows[0][0].ToString() + "','" + dd + "','','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + dtkat.Rows[0][0].ToString() + "','" + inv + "','" + txtnoruj2.Text + "','','0.00','" + tDate1.ToString("yyyy/MM/dd hh:mm:ss") + "','','','" + ddpela3.SelectedItem.Value + "','" + ddpro2.SelectedItem.Value + "','','A')");
                        dt6 = Dbcon.Ora_Execute_table("insert into KW_General_Ledger values('" + dtkat1.Rows[0]["kod_akaun"].ToString() + "','','" + dd + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + dtkat1.Rows[0][0].ToString() + "','" + inv + "','" + txtnoruj2.Text + "','','0.00','" + tDate1.ToString("yyyy/MM/dd hh:mm:ss") + "','','','" + ddpela3.SelectedItem.Value + "','" + ddpro2.SelectedItem.Value + "','','A')");
                        fr2.Visible = false;
                        fr1.Visible = true;
                        reset();
                        tab4();
                        Binddedit();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Mesti Nilai Debit Masuk.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }

                }

            }

        }
        else
        {
            tab4();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }


    }
    protected void Btnkoffclose1_Click(object sender, EventArgs e)
    {
        reset();

    }
    protected void Button14_Click(object sender, EventArgs e)
    {

        if (Session["Invoisno"].ToString() != "")
        {
            lnk_clk();
            Session["Invoisno"] = txtnoinvois1.Text;
            //Response.Redirect("KW_Penerimaan_invois_Rptview.aspx");

            string url = "KW_Penerimaan_invois_Rptview.aspx";
            string s = "window.open('" + url + "', 'popup_window', 'width=11000,height=700,left=100,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            Bindcredit();
            Binddedit();
        }

    }

    protected void Button21_Click(object sender, EventArgs e)
    {

        if (Session["dbtno1"].ToString() != "")
        {
            debit_link();
            Session["dbtno1"] = txtnoruj2.Text;
            //Response.Redirect("KW_Penerimaan_invois_Rptview.aspx");

            string url = "KW_Penerimaan_debit_rptview.aspx";
            string s = "window.open('" + url + "', 'popup_window', 'width=11000,height=700,left=100,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            Bindresit();
            Bindcredit();
        }

        //if (txtnoruj2.Text != "")
        //{
        //    Session["dbtno"] = txtnoruj2.Text;
        //    Response.Redirect("KW_Penerimaan_debit_rptview.aspx");


        //}

    }

    protected void Button22_Click(object sender, EventArgs e)
    {

        if (Session["dbtno"].ToString() != "")
        {
            crt_notelink();
            Session["dbtno"] = txtnoruju.Text;
            //Response.Redirect("KW_Penerimaan_invois_Rptview.aspx");

            string url = "KW_Penerimaan_credit_rptview.aspx";
            string s = "window.open('" + url + "', 'popup_window', 'width=11000,height=700,left=100,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            Bindresit();
            Binddedit();
        }

        //if (txtnoruju.Text != "")
        //{
        //    Session["dbtno"] = txtnoruju.Text;
        //    Response.Redirect("KW_Penerimaan_credit_rptview.aspx");


        //}

    }
    protected void resit_prt(object sender, EventArgs e)
    {

        if (Session["resitno"].ToString() != "")
        {
            rst_crt();
            Session["resitno"] = txtnorst3.Text;
            //Response.Redirect("KW_Penerimaan_invois_Rptview.aspx");

            string url = "KW_Penerimaan_resit_Rptview.aspx";
            string s = "window.open('" + url + "', 'popup_window', 'width=11000,height=700,left=100,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            Bindresit();
            Binddedit();
        }

    }
}