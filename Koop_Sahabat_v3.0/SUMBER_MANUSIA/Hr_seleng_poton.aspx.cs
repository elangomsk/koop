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

public partial class Hr_seleng_poton : System.Web.UI.Page
{

    DBConnection Dblog = new DBConnection();
    DBConnection DBCon = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection dbcon = new DBConnection();
    StudentWebService service = new StudentWebService();
    DataTable dt = new DataTable();
    DataTable dt1 = new DataTable();
    DataTable dt2 = new DataTable();
    DataTable dt3 = new DataTable();
    DataTable dt4 = new DataTable();
    DataTable dtt1 = new DataTable();
    string useid = string.Empty;
    string Status = string.Empty;
    string oid = string.Empty;
    string sdd = string.Empty, b_slry = string.Empty, cc1 = string.Empty, cc2 = string.Empty, cc3 = string.Empty, cc4 = string.Empty, cc5 = string.Empty, cc6 = string.Empty, cc7 = string.Empty, cc8 = string.Empty, bns_cc1 = string.Empty, bns_cc2 = string.Empty;
    string v1 = string.Empty, v2 = string.Empty, v3 = string.Empty, v4 = string.Empty;
    string cka = string.Empty, ckm = string.Empty;
    DataTable dd_fallowence = new DataTable();
    DataTable dd_xallowence = new DataTable();
    DataTable per_fallowence = new DataTable();
    DataTable per_xallowence = new DataTable();
    string ss_kwspamt = string.Empty;
    string CommandArgument1 = string.Empty, CommandArgument2 = string.Empty, CommandArgument3 = string.Empty;
    string act_dt = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        string script = " $(function () {$('.select2').select2();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;
                month();
                pon();
                txtname.Attributes.Add("Readonly", "Readonly");
                txtgred.Attributes.Add("Readonly", "Readonly");
                txtjab.Attributes.Add("Readonly", "Readonly");
                txtjaw.Attributes.Add("Readonly", "Readonly");
                txt_org.Attributes.Add("Readonly", "Readonly");
                TextBox13.Attributes.Add("Readonly", "Readonly");
                Applcn_no1.Text = "";
                txttakhir.Text = "31/12/9999";
                TextBox29.Text = "31/12/9999";
                TextBox19.Text = "31/12/9999";
                TextBox33.Text = "31/12/9999";
                TextBox11.Text = "31/12/9999";
                TextBox9.Text = "31/12/9999";
                TextBox16.Attributes.Add("Readonly", "Readonly");

                txtacahli.Attributes.Add("Readonly", "Readonly");
                txtacm.Attributes.Add("Readonly", "Readonly");
                TextBox7.Attributes.Add("Readonly", "Readonly");
                TextBox3.Attributes.Add("Readonly", "Readonly");
                //TextBox9.Attributes.Add("Readonly", "Readonly");
                txtsno.Attributes.Add("Readonly", "Readonly");
                TextBox22.Attributes.Add("Readonly", "Readonly");
                TextBox23.Attributes.Add("Readonly", "Readonly");
                DD_PBB.SelectedValue = DateTime.Now.ToString("MM");
                txt_tahu.Text = DateTime.Now.ToString("yyyy");
                act_dt = txt_tahu.Text + "-" + DD_PBB.SelectedValue;
                if (samp != "")
                {
                    Applcn_no1.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    txtsno.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    view_details();

                }
                else
                {

                }
                useid = Session["New"].ToString();


            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    void month()
    {
        DataSet Ds = new DataSet();
        try
        {
            DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);

            for (int i = 1; i < 13; i++)
            {
                DD_PBB.Items.Add(new ListItem(info.GetMonthName(i), i.ToString("00")));
            }

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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('1952','1953','505','484','1565','513','1675','1954','1288','190','1955','1956','326','1407','1468','1406','61','35','133','77','647','1957','1733','1959', '1407', '1468', '1735', '1960', '1745', '1961', '1962', '1736', '1963', '1964', '1965', '1966', '1967', '1738', '1968', '1406', '1969', '1970', '1468', '1406', '61', '77')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());   
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[26][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[36][0].ToString().ToLower());
            ps_lbl10.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());       
            ps_lbl11.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl12.Text = txtinfo.ToTitleCase(gt_lng.Rows[16][0].ToString().ToLower());
            ps_lbl13.Text = txtinfo.ToTitleCase(gt_lng.Rows[32][0].ToString().ToLower());
            ps_lbl14.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl15.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
            ps_lbl16.Text = txtinfo.ToTitleCase(gt_lng.Rows[24][0].ToString().ToLower());
            ps_lbl17.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString());
            Button8.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            Button9.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            Button10.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());         
            ps_lbl22.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl23.Text = txtinfo.ToTitleCase(gt_lng.Rows[25][0].ToString().ToLower());
            ps_lbl24.Text = txtinfo.ToTitleCase(gt_lng.Rows[27][0].ToString().ToLower());
            ps_lbl25.Text = txtinfo.ToTitleCase(gt_lng.Rows[38][0].ToString().ToLower());
            ps_lbl26.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
            ps_lbl27.Text = txtinfo.ToTitleCase(gt_lng.Rows[24][0].ToString().ToLower());
            Label4.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
            Label5.Text = txtinfo.ToTitleCase(gt_lng.Rows[24][0].ToString().ToLower());
            //ps_lbl28.Text = txtinfo.ToTitleCase(gt_lng.Rows[28][0].ToString().ToLower());
            ps_lbl29.Text = txtinfo.ToTitleCase(gt_lng.Rows[37][0].ToString());
            //ps_lbl30.Text = txtinfo.ToTitleCase(gt_lng.Rows[31][0].ToString().ToLower());     
            ps_lbl31.Text = txtinfo.ToTitleCase(gt_lng.Rows[17][0].ToString());
            Button12.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            Button13.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            Button14.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl35.Text = txtinfo.ToTitleCase(gt_lng.Rows[18][0].ToString());
            ps_lbl36.Text = txtinfo.ToTitleCase(gt_lng.Rows[29][0].ToString().ToLower());
            ps_lbl37.Text = txtinfo.ToTitleCase(gt_lng.Rows[19][0].ToString().ToLower());
            ps_lbl38.Text = txtinfo.ToTitleCase(gt_lng.Rows[35][0].ToString());
            ps_lbl39.Text = txtinfo.ToTitleCase(gt_lng.Rows[20][0].ToString());
            //ps_lbl40.Text = txtinfo.ToTitleCase(gt_lng.Rows[21][0].ToString().ToLower());      
            //ps_lbl41.Text = txtinfo.ToTitleCase(gt_lng.Rows[33][0].ToString().ToLower());
            //ps_lbl42.Text = txtinfo.ToTitleCase(gt_lng.Rows[30][0].ToString().ToLower());
            //ps_lbl43.Text = txtinfo.ToTitleCase(gt_lng.Rows[22][0].ToString().ToLower());
            //ps_lbl44.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            //ps_lbl45.Text = txtinfo.ToTitleCase(gt_lng.Rows[23][0].ToString().ToLower());
            //ps_lbl46.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
            //ps_lbl47.Text = txtinfo.ToTitleCase(gt_lng.Rows[24][0].ToString().ToLower());
            //ps_lbl48.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            Button5.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());

        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }

    protected void sel_bagi_bulan(object sender, EventArgs e)
    {
        view_details();
    }
    void view_details()
    {
        if (txtsno.Text != "")
        {

            bind();
            //KWSP();

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan kakitangan Tiada',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }
    protected void text_ahli(object sender, EventArgs e)
    {
        if (txtcaruahli.Text != "")
        {
            double max_amt = 20000.01;
            if (double.Parse(TextBox26.Text) < max_amt)
            {
                DataTable dt = new DataTable();
                dt = dbcon.Ora_Execute_table("SELECT top(1) h2 FROM kwsp_cal where '" + TextBox26.Text + "' between h1 and h2");
                if (dt.Rows.Count > 0)
                {

                    double ahli = Convert.ToDouble(dt.Rows[0][0].ToString()) * Convert.ToDouble(txtcaruahli.Text) / 100;

                    decimal value;
                    if (decimal.TryParse(ahli.ToString(), out value))
                    {
                        //value = Math.Round(value);
                        value = Math.Ceiling(value);
                        txtacahli.Text = value.ToString("C").Replace("$", "").Replace("RM", "");
                    }
                }
                else
                {
                    txtacahli.Text = "0.00";
                }
            }
            else
            {
                decimal ahli = Convert.ToDecimal(TextBox26.Text) * Convert.ToDecimal(txtcaruahli.Text) / 100;
                decimal value;
                if (decimal.TryParse(ahli.ToString(), out value))
                {
                    //value = Math.Round(value);
                    value = Math.Ceiling(value);
                    txtacahli.Text = value.ToString("C").Replace("$", "").Replace("RM", "");
                }
            }
        }
    }

    protected void text_maji(object sender, EventArgs e)
    {
        if (txtkmaji.Text != "")
        {
            double max_amt = 20000.01;
            if (double.Parse(TextBox26.Text) < max_amt)
            {
                DataTable dt = new DataTable();
                dt = dbcon.Ora_Execute_table("SELECT top(1) h2 FROM kwsp_cal where '" + TextBox26.Text + "' between h1 and h2");
                if (dt.Rows.Count > 0)
                {
                    decimal ahli = Convert.ToDecimal(dt.Rows[0][0].ToString()) * Convert.ToDecimal(txtkmaji.Text) / 100;                   
                    decimal value;
                    if (decimal.TryParse(ahli.ToString(), out value))
                    {
                        value = Math.Ceiling(value);
                        txtacm.Text = value.ToString("C").Replace("$", "").Replace("RM", "");
                    }
                }
                else
                {
                    txtacm.Text = "0.00";
                }
            }
            else
            {
                decimal ahli = Convert.ToDecimal(TextBox26.Text) * Convert.ToDecimal(txtkmaji.Text) / 100;
                decimal value;
                if (decimal.TryParse(ahli.ToString(), out value))
                {
                    value = Math.Ceiling(value);
                    txtacm.Text = value.ToString("C").Replace("$", "").Replace("RM", "");
                }
            }

        }
    }

    void bind()
    {
        DataSet Ds = new DataSet();
        try
        {
            act_dt = txt_tahu.Text + "-" + DD_PBB.SelectedValue;
            DataTable select_kaki1 = new DataTable();
            select_kaki1 = dbcon.Ora_Execute_table("select * from hr_staff_profile where '" + txtsno.Text + "' IN (stf_staff_no,stf_name)");
            if (select_kaki1.Rows.Count != 0)
            {
                Applcn_no1.Text = select_kaki1.Rows[0]["stf_staff_no"].ToString();

                string com = "select stf_name,pos_dept_cd,jhr.hr_jaba_desc,pos_post_cd,rhj.hr_jaw_desc,pos_grade_cd,rhg.hr_gred_desc,ISNULL(stf_age,'0') as stf_age,str_curr_org_cd,stf_epf_no,ho.org_name,o1.op_perg_name from hr_staff_profile hsp left join hr_post_his hph on hph.pos_staff_no=hsp.stf_staff_no left join ref_hr_jawatan rhj on  hph.pos_post_cd=rhj.hr_jaw_Code left join Ref_hr_jabatan jhr on hph.pos_dept_cd=jhr.hr_jaba_Code left join ref_hr_gred rhg on hph.pos_grade_cd=rhg.hr_gred_Code left join hr_organization ho on ho.org_gen_id=str_curr_org_cd left join hr_organization_pern o1 on o1.op_perg_code=stf_cur_sub_org where stf_staff_no ='" + Applcn_no1.Text + "' AND pos_end_dt = '9999-12-31'";
                SqlDataAdapter adpt = new SqlDataAdapter(com, con);
                DataTable dt = new DataTable();
                int age = 0;
                adpt.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    TextBox1.Text = dt.Rows[0]["str_curr_org_cd"].ToString();
                    txt_org.Text = dt.Rows[0]["org_name"].ToString();
                    txtname.Text = dt.Rows[0][0].ToString();
                    txtjab.Text = dt.Rows[0][2].ToString();
                    txtjaw.Text = dt.Rows[0][4].ToString();
                    txtgred.Text = dt.Rows[0][6].ToString();
                    txtahli.Text = dt.Rows[0]["stf_epf_no"].ToString();
                    TextBox13.Text = dt.Rows[0]["op_perg_name"].ToString();
                    if (dt.Rows[0]["stf_age"].ToString() != "")
                    {
                        age = Int32.Parse(dt.Rows[0]["stf_age"].ToString());
                    }
                    int act_age = 60;
                    if (age < act_age)
                    {
                        v1 = "per_employee_amt";
                        v2 = "per_employer_amt1";
                        v3 = "SIP_employee_amt";
                        v4 = "SIP_employer_amt1";
                    }
                    else
                    {
                        v1 = "per_employer_amt2";
                        v2 = "per_employer_amt2";
                        v3 = "SIP_employer_amt2";
                        v4 = "SIP_employer_amt2";
                    }

                    if (TextBox1.Text != "")
                    {
                        DataTable Get_org = new DataTable();
                        Get_org = dbcon.Ora_Execute_table("select org_epf_no from hr_organization where org_gen_id='" + dt.Rows[0]["str_curr_org_cd"].ToString() + "'");

                        txtmaji.Text = Get_org.Rows[0]["org_epf_no"].ToString();
                    }
                    else
                    {
                        txtmaji.Text = "";
                    }



                    bind1();
                    bind2();
                    bind_perkeso1();
                    bind_sip1();
                    PERKESO();
                    kwsp_allowences();
                    Potongan();
                    bind_cukai();
                    bind_cp38();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Wujud',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Wujud',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void kwsp_allowences()
    {
        b_slry = TextBox2.Text;
        //dd_fallowence = dbcon.Ora_Execute_table("select ISNULL(sum(s1.fxa_allowance_amt),'') as falw_amt from hr_fixed_allowance s1 Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=s1.fxa_allowance_type_cd and ss1.elau_kwsp='S' where ('" + act_dt + "' between FORMAT(fxa_eff_dt,'yyyy-MM') And FORMAT(fxa_end_dt,'yyyy-MM')) and fxa_staff_no='" + Applcn_no1.Text + "'");
        dd_fallowence = dbcon.Ora_Execute_table("select (sum(falw_amt) - sum(falw_amt1)) amt1 from (select ISNULL(sum(s1.fxa_allowance_amt),'') as falw_amt,'0.00' falw_amt1 from hr_fixed_allowance s1 Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=s1.fxa_allowance_type_cd and ss1.elau_kwsp='S' where ('" + act_dt + "' between FORMAT(fxa_eff_dt,'yyyy-MM') And FORMAT(fxa_end_dt,'yyyy-MM')) and fxa_staff_no='" + Applcn_no1.Text + "' union all select ISNULL(sum(s1.fxa_promo_amt),'') as falw_amt,ISNULL(sum(s1.fxa_allowance_amt),'') as falw_amt1 from hr_fixed_allowance s1 Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=s1.fxa_allowance_type_cd and ss1.elau_kwsp='S' where ('" + act_dt + "' between FORMAT(fxa_pst_dt,'yyyy-MM') And FORMAT(fxa_ped_dt,'yyyy-MM')) and fxa_staff_no='" + Applcn_no1.Text + "')as a");
        if (dd_fallowence.Rows.Count != 0)
        {
            cc1 = dd_fallowence.Rows[0]["amt1"].ToString();
        }
        else
        {
            cc1 = "0.00";
        }

        //dd_xallowence = dbcon.Ora_Execute_table("select ISNULL(sum(s1.xta_allowance_amt),'') as xalw_amt from hr_extra_allowance s1 Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=s1.xta_allowance_type_cd and ss1.elau_kwsp='S' where s1.xta_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt + "' between FORMAT(xta_eff_dt,'yyyy-MM') And FORMAT(xta_end_dt,'yyyy-MM'))");
        dd_xallowence = dbcon.Ora_Execute_table("select (sum(xalw_amt) - sum(xalw_amt1)) amt1 from(select ISNULL(sum(s1.xta_allowance_amt),'') as xalw_amt,'0.00' xalw_amt1 from hr_extra_allowance s1 Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=s1.xta_allowance_type_cd and ss1.elau_kwsp='S' where s1.xta_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt + "' between FORMAT(xta_eff_dt,'yyyy-MM') And FORMAT(xta_end_dt,'yyyy-MM')) union all select ISNULL(sum(s1.xta_promo_amt),'') as xalw_amt,ISNULL(sum(s1.xta_allowance_amt),'') as xalw_amt1 from hr_extra_allowance s1 Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=s1.xta_allowance_type_cd and ss1.elau_kwsp='S' where s1.xta_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt + "' between FORMAT(xta_pst_dt,'yyyy-MM') And FORMAT(xta_ped_dt,'yyyy-MM'))) as a");
        if (dd_xallowence.Rows.Count != 0)
        {
            cc2 = dd_xallowence.Rows[0]["amt1"].ToString();
        }
        else
        {
            cc2 = "0.00";
        }

        //ot
        DataTable dd_hrsal_ot = new DataTable();
        dd_hrsal_ot = DBCon.Ora_Execute_table("select ISNULL(sum(ISNULL(otl_ot_amt,'0.00')),'0.00') as amt from hr_ot left join Ref_hr_type_klm s1 on s1.typeklm_cd=otl_ot_type_cd  where s1.elau_kwsp ='S' and otl_staff_no='" + Applcn_no1.Text + "' and FORMAT(otl_work_dt,'yyyy-MM') ='" + act_dt + "'");
        if (dd_hrsal_ot.Rows.Count != 0)
        {
            cc6 = double.Parse(dd_hrsal_ot.Rows[0]["amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
        }
        else
        {
            cc6 = "0.00";
        }

        //deduction
        DataTable tot_upl = new DataTable();
        tot_upl = dbcon.Ora_Execute_table("select ISNULL(sum(ded_deduct_amt),'') as s1 from hr_deduction left join Ref_hr_potongan s1 on s1.hr_poto_Code=ded_deduct_type_cd and Status='A' where s1.elau_kwsp='S' and ded_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt + "' between FORMAT(ded_start_dt,'yyyy-MM') And FORMAT(ded_end_dt,'yyyy-MM'))");
        //Math.Round(b, 2)
        if (tot_upl.Rows.Count != 0)
        {
            cc7 = double.Parse(tot_upl.Rows[0]["s1"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
        }
        else
        {
            cc7 = "0.00";
        }

        DataTable dd_hrsal_tun = new DataTable();
        dd_hrsal_tun = DBCon.Ora_Execute_table("select ISNULL(sum(ISNULL(tun_amt,'0.00')),'0.00') as amt from hr_tunggakan left join Ref_hr_tunggakan s1 on s1.hr_tung_Code=tun_type_cd  where s1.elau_kwsp ='S' and tun_staff_no='" + Applcn_no1.Text + "' and  tun_year='" + txt_tahu.Text + "' and tun_month='" + DD_PBB.SelectedValue + "'");
        if (dd_hrsal_tun.Rows.Count != 0)
        {
            cc5 = double.Parse(dd_hrsal_tun.Rows[0]["amt"].ToString()).ToString();
        }
        else
        {
            cc5 = "0.00";
        }

        DataTable dd_hrsal_bns = new DataTable();
        dd_hrsal_bns = DBCon.Ora_Execute_table("	select a.amt1,b.amt2 from (select ISNULL(sum(ISNULL(bns_amt,'0.00')),'0.00') as amt1 from hr_bonus left join Ref_hr_Claim s1 on s1.hr_clm_Code='01'  "
+ " where s1.clm_kwsp = 'S' and bns_staff_no = '" + Applcn_no1.Text + "' and('" + act_dt + "' between FORMAT(bns_eff_dt, 'yyyy-MM') And FORMAT(bns_end_dt, 'yyyy-MM'))) as a "
+ " outer apply (select ISNULL(sum(ISNULL(bns_kpi_amt, '0.00')), '0.00') as amt2 from hr_bonus left join Ref_hr_Claim s1 on s1.hr_clm_Code = '02' "
+ " where s1.clm_kwsp = 'S' and bns_staff_no = '" + Applcn_no1.Text + "' and('" + act_dt + "' between FORMAT(bns_eff_dt, 'yyyy-MM') And FORMAT(bns_end_dt, 'yyyy-MM'))) as b");
        if (dd_hrsal_tun.Rows.Count != 0)
        {
            double bns1 = double.Parse(dd_hrsal_bns.Rows[0]["amt1"].ToString()) + double.Parse(dd_hrsal_bns.Rows[0]["amt2"].ToString());
            bns_cc1 = double.Parse(bns1.ToString()).ToString();
            
        }
        else
        {
            bns_cc1 = "0.00";
        }

        //TextBox4.Text = (double.Parse(cc1) + double.Parse(cc2)).ToString();
        TextBox4.Text = ((double.Parse(cc1) + double.Parse(cc2) + double.Parse(cc5) + double.Parse(cc6) + double.Parse(bns_cc1)) - double.Parse(cc7)).ToString();
        ss_kwspamt = (double.Parse(b_slry) + double.Parse(TextBox4.Text)).ToString();
        TextBox26.Text = ss_kwspamt;


        //kwsp Ahli & Majikan
        double max_amt = 20000.01;
        if (double.Parse(ss_kwspamt) < max_amt)
        {


            DataTable gt_kwsp_ahli = new DataTable();
            gt_kwsp_ahli = dbcon.Ora_Execute_table("select [KADAR CARUMAN BAGI BULAN ITU] as amt1 from kwsp_cal where '" + ss_kwspamt + "' between h1 and h2");
            if (gt_kwsp_ahli.Rows.Count != 0)
            {
                txtacahli.Text = double.Parse(gt_kwsp_ahli.Rows[0]["amt1"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
            }
            else
            {
                txtacahli.Text = "0.00";
            }

            DataTable gt_kwsp_maj = new DataTable();
            gt_kwsp_maj = dbcon.Ora_Execute_table("select [Oleh Majikan RM] as amt2 from kwsp_cal where '" + ss_kwspamt + "' between h1 and h2");
            if (gt_kwsp_maj.Rows.Count != 0)
            {
                txtacm.Text = double.Parse(gt_kwsp_maj.Rows[0]["amt2"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
            }
            else
            {
                txtacm.Text = "0.00";
            }
        }
        else
        {
            double ssv1 = double.Parse(ss_kwspamt) * 0.01;
            txtacahli.Text = (ssv1.ToString());
            txtacm.Text = (ssv1.ToString());


        }
        string st_dt1 = DateTime.Now.ToString("yyyy") + "-" + DateTime.Now.AddMonths(-1).Month + "-21";
        string st_dt2 = DateTime.Now.ToString("yyyy") + "-" + DateTime.Now.ToString("MM") + "-20";
        string stamt = string.Empty, allwnce_amt = string.Empty;

        //SIP

        DataTable Get_tot_sip = new DataTable();
        Get_tot_sip = dbcon.Ora_Execute_table("select b.a1,ISNULL(c.b1,'0.00') b1,ISNULL((ISNULL(b.a1,'0.00') + ISNULL(c.b1,'0.00')),'') as tot from (select sum(fxa_allowance_amt) as a1,fxa_staff_no from hr_fixed_allowance Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=fxa_allowance_type_cd where fxa_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt + "' between FORMAT(fxa_eff_dt,'yyyy-MM') And FORMAT(fxa_end_dt,'yyyy-MM')) group by fxa_staff_no) as b full outer join (select sum(xta_allowance_amt) as b1,xta_staff_no from hr_extra_allowance Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=xta_allowance_type_cd  where xta_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt + "' between FORMAT(xta_eff_dt,'yyyy-MM') And FORMAT(xta_end_dt,'yyyy-MM')) group by xta_staff_no) as c on c.xta_staff_no=b.fxa_staff_no order by b.fxa_staff_no Asc");

        if (Get_tot_sip.Rows.Count != 0)
        {
            allwnce_amt = Get_tot_sip.Rows[0]["tot"].ToString();
        }
        else
        {
            allwnce_amt = "0.00";
        }


        //DataTable tot_upl = new DataTable();
        //tot_upl = dbcon.Ora_Execute_table("select ISNULL(sum(ded_deduct_amt),'') as s1 from hr_deduction where ded_deduct_type_cd='09' and ded_staff_no='" + Applcn_no1.Text + "' and ded_start_dt>=DATEADD(day, DATEDIFF(day, 0, '" + st_dt1 + "'), 0) and ded_end_dt<=DATEADD(day, DATEDIFF(day, 0, '" + st_dt2 + "'), +1)");
        ////Math.Round(b, 2)
        //if (tot_upl.Rows.Count != 0)
        //{
        //    stamt = (double.Parse(allwnce_amt) - double.Parse(tot_upl.Rows[0]["s1"].ToString())).ToString();
        //}
        //else
        //{
        //    stamt = allwnce_amt;
        //}


        //decimal ssamt = Math.Round(decimal.Parse(stamt), 2);
        TextBox14.Text = (double.Parse(b_slry) + double.Parse(TextBox4.Text)).ToString("C").Replace("$", "").Replace("RM", "");
        TextBox16.Text = (double.Parse(b_slry) + double.Parse(TextBox4.Text)).ToString("C").Replace("$", "").Replace("RM", "");

        //TextBox9.Text = double.Parse(stamt).ToString("C").Replace("$", "").Replace("RM", ""); // 24_07_2018
        //DataTable tt_sum_kwsp = new DataTable();
        //tt_sum_kwsp = dbcon.Ora_Execute_table("select h2 from kwsp_cal where '" + ssamt + "' between h1 and h2");
        //if (tt_sum_kwsp.Rows.Count != 0)
        //{
        //    TextBox14.Text = tt_sum_kwsp.Rows[0]["h2"].ToString();
        //    TextBox16.Text = double.Parse(tt_sum_kwsp.Rows[0]["h2"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
        //}
        //else
        //{
        //    TextBox14.Text = "0.00";
        //    TextBox16.Text = "0.00";
        //}

        //DataTable dd_hrsal5 = new DataTable();
        //dd_hrsal5 = DBCon.Ora_Execute_table("select fx.ded_staff_no,sum(fx.ded_deduct_amt) as samt from hr_deduction as fx where ded_deduct_type_cd='09' and ('" + act_dt.ToString() + "' between FORMAT(fx.ded_start_dt,'yyyy-MM') And FORMAT(fx.ded_end_dt,'yyyy-MM')) and fx.ded_staff_no='" + Applcn_no1.Text + "' group by fx.ded_staff_no");
        //if (dd_hrsal5.Rows.Count != 0)
        //{
        //    TextBox4.Text = double.Parse(dd_hrsal5.Rows[0]["samt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
        //}
        //else
        //{
        //    TextBox4.Text = "0.00";
        //}

    }

    void perkeso_allowences()
    {

        per_fallowence = dbcon.Ora_Execute_table("select (sum(falw_amt) - sum(falw_amt1)) amt1 from (select ISNULL(sum(s1.fxa_allowance_amt),'') as falw_amt,'0.00' falw_amt1 from hr_fixed_allowance s1 Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=s1.fxa_allowance_type_cd and ss1.elau_kwsp='S' where ('" + act_dt + "' between FORMAT(fxa_eff_dt,'yyyy-MM') And FORMAT(fxa_end_dt,'yyyy-MM')) and fxa_staff_no='" + Applcn_no1.Text + "' union all select ISNULL(sum(s1.fxa_promo_amt),'') as falw_amt,ISNULL(sum(s1.fxa_allowance_amt),'') as falw_amt1 from hr_fixed_allowance s1 Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=s1.fxa_allowance_type_cd and ss1.elau_kwsp='S' where ('" + act_dt + "' between FORMAT(fxa_pst_dt,'yyyy-MM') And FORMAT(fxa_ped_dt,'yyyy-MM')) and fxa_staff_no='" + Applcn_no1.Text + "')as a");
        if (per_fallowence.Rows.Count != 0)
        {
            cc3 = per_fallowence.Rows[0]["amt1"].ToString();
        }
        else
        {
            cc3 = "0.00";
        }

        per_xallowence = dbcon.Ora_Execute_table("select (sum(xalw_amt) - sum(xalw_amt1)) amt1 from(select ISNULL(sum(s1.xta_allowance_amt),'') as xalw_amt,'0.00' xalw_amt1 from hr_extra_allowance s1 Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=s1.xta_allowance_type_cd and ss1.elau_kwsp='S' where s1.xta_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt + "' between FORMAT(xta_eff_dt,'yyyy-MM') And FORMAT(xta_end_dt,'yyyy-MM')) union all select ISNULL(sum(s1.xta_promo_amt),'') as xalw_amt,ISNULL(sum(s1.xta_allowance_amt),'') as xalw_amt1 from hr_extra_allowance s1 Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=s1.xta_allowance_type_cd and ss1.elau_kwsp='S' where s1.xta_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt + "' between FORMAT(xta_pst_dt,'yyyy-MM') And FORMAT(xta_ped_dt,'yyyy-MM'))) as a");
        if (per_xallowence.Rows.Count != 0)
        {
            cc4 = per_xallowence.Rows[0]["amt1"].ToString();
        }
        else
        {
            cc4 = "0.00";
        }


    }

    protected void gvSelected_PageIndexChanging_3(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        bind1();
        bind2();
    }
    protected void gvSelected_PageIndexChanging_2(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bind2();
        bind1();

    }


    void KWSP()
    {
        DataSet Ds = new DataSet();
        try
        {
            DateTime dt1 = DateTime.ParseExact(CommandArgument1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            String feedt = dt1.ToString("yyyy-MM-dd");

            DateTime dt2 = DateTime.ParseExact(CommandArgument2, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            String feedt1 = dt2.ToString("yyyy-MM-dd");

            Button12.Visible = false;
            Button13.Visible = true;

            //txtahli.Attributes.Add("Readonly", "Readonly");
            //txtmaji.Attributes.Add("Readonly", "Readonly");
            txttmula.Attributes.Add("Readonly", "Readonly");
            txttmula.Attributes.Add("style", "Pointer-events:none;");
            //txtckm.Attributes.Add("Readonly", "Readonly");
            //txtacm.Attributes.Add("Readonly", "Readonly");
            decimal a, b;
            string com = "select top(1) ISNULL(stf_epf_no,'') as stf_epf_no,ISNULL(org_epf_no,'') as org_epf_no,format(slr_salary_amt,'#,##.00')slr_salary_amt,epf_percentage,epf_amt,format(epf_eff_dt,'dd/MM/yyyy') epf_eff_dt,format(epf_end_dt,'dd/MM/yyyy')epf_end_dt,epf_emp_kwsp_perc,epf_emp_kwsp_amt from hr_staff_profile hsp left join hr_kwsp hk on hk.epf_staff_no=hsp.stf_staff_no left join hr_post_his hph on hph.pos_staff_no=hsp.stf_staff_no left join hr_organization ho on hsp.stf_epf_no=ho.org_epf_no left join hr_salary hs on hsp.stf_staff_no=hs.slr_staff_no where hsp.stf_staff_no='" + Applcn_no1.Text + "' and epf_eff_dt='" + feedt + "' order by slr_eff_dt desc";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            if (dt.Rows.Count > 0)
            {

                //txtcka.Text = dt.Rows[0]["epf_percentage"].ToString();
                //txtckm.Text = dt.Rows[0]["epf_emp_kwsp_perc"].ToString();
                txtacm.Text = double.Parse(dt.Rows[0]["epf_emp_kwsp_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");

                txtacahli.Text = double.Parse(dt.Rows[0]["epf_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                txttmula.Text = dt.Rows[0][5].ToString();
                txttakhir.Text = dt.Rows[0][6].ToString();
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void cukai()
    {
        DataSet Ds = new DataSet();
        try
        {
            DateTime dt1 = DateTime.ParseExact(CommandArgument1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            String feedt = dt1.ToString("yyyy-MM-dd");

            DateTime dt2 = DateTime.ParseExact(CommandArgument2, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            String feedt1 = dt2.ToString("yyyy-MM-dd");

            Button15.Visible = false;
            Button16.Visible = true;

            //txtahli.Attributes.Add("Readonly", "Readonly");
            //txtmaji.Attributes.Add("Readonly", "Readonly");
            TextBox18.Attributes.Add("Readonly", "Readonly");
            TextBox18.Attributes.Add("style", "Pointer-events:none;");
            //txtckm.Attributes.Add("Readonly", "Readonly");
            //txtacm.Attributes.Add("Readonly", "Readonly");
            decimal a, b;
            string com = "select format(tax_pcb_start_dt,'dd/MM/yyyy') tax_pcb_start_dt,format(tax_pcb_end_dt,'dd/MM/yyyy')tax_pcb_end_dt,tax_pcb_amt, tax_staff_no,tax_incometax_no from hr_staff_profile hsp inner join hr_income_tax it on hsp.stf_staff_no=it.tax_staff_no where hsp.stf_staff_no='" + Applcn_no1.Text + "' and tax_pcb_start_dt='" + feedt + "' and tax_type ='1' order by tax_pcb_start_dt desc";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                TextBox10.Text= dt.Rows[0][4].ToString();
                TextBox24.Text = double.Parse(dt.Rows[0]["tax_pcb_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                TextBox18.Text = dt.Rows[0][0].ToString();
                TextBox19.Text = dt.Rows[0][1].ToString();
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void cp38()
    {
        DataSet Ds = new DataSet();
        try
        {
            DateTime dt1 = DateTime.ParseExact(CommandArgument1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            String feedt = dt1.ToString("yyyy-MM-dd");

            DateTime dt2 = DateTime.ParseExact(CommandArgument2, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            String feedt1 = dt2.ToString("yyyy-MM-dd");

            Button18.Visible = false;
            Button19.Visible = true;

            //txtahli.Attributes.Add("Readonly", "Readonly");
            //txtmaji.Attributes.Add("Readonly", "Readonly");
            TextBox31.Attributes.Add("Readonly", "Readonly");
            TextBox31.Attributes.Add("style", "Pointer-events:none;");
            //txtckm.Attributes.Add("Readonly", "Readonly");
            //txtacm.Attributes.Add("Readonly", "Readonly");
            decimal a, b;
            string com = "select format(tax_cp38_start_dt1,'dd/MM/yyyy') tax_pcb_start_dt,format(tax_cp38_end_dt1,'dd/MM/yyyy')tax_pcb_end_dt,tax_cp38_amt1, tax_staff_no from hr_staff_profile hsp inner join hr_income_tax it on hsp.stf_staff_no=it.tax_staff_no where hsp.stf_staff_no='" + Applcn_no1.Text + "' and tax_cp38_start_dt1='" + feedt + "' and tax_type ='2' order by tax_cp38_start_dt1 desc";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
               
                TextBox34.Text = double.Parse(dt.Rows[0]["tax_cp38_amt1"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                TextBox31.Text = dt.Rows[0][0].ToString();
                TextBox33.Text = dt.Rows[0][1].ToString();
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    void get_perkeso()
    {
        DataSet Ds = new DataSet();
        try
        {
            DateTime dt1 = DateTime.ParseExact(CommandArgument1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            String feedt = dt1.ToString("yyyy-MM-dd");

            DateTime dt2 = DateTime.ParseExact(CommandArgument2, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            String feedt1 = dt2.ToString("yyyy-MM-dd");

            string com = "select * from hr_staff_profile hsp inner join hr_income_tax hit on hit.tax_staff_no=hsp.stf_staff_no where hsp.stf_staff_no='" + Applcn_no1.Text + "' and tax_cp38_start_dt1='"+ feedt + "'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Button3.Visible = false;
                Button4.Visible = true;
                TextBox7.Text = double.Parse(dt.Rows[0]["tax_cp38_amt1"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                TextBox3.Text = double.Parse(dt.Rows[0]["tax_cp38_amt2"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                TextBox5.Text = dt.Rows[0]["perk_ahli_no"].ToString();
                TextBox6.Text = dt.Rows[0]["perk_mjik_no"].ToString();
                TextBox12.Text = Convert.ToDateTime(dt.Rows[0]["tax_cp38_start_dt1"].ToString()).ToString("dd/MM/yyyy");
                TextBox11.Text = Convert.ToDateTime(dt.Rows[0]["tax_cp38_end_dt1"].ToString()).ToString("dd/MM/yyyy");
                if (dt.Rows[0]["perkeso_chk"].ToString() == "1")
                {
                    perkeso_chk.Checked = true;
                }
                else
                {
                    perkeso_chk.Checked = false;
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    void get_sip()
    {
        DataSet Ds = new DataSet();
        try
        {
            DateTime dt1 = DateTime.ParseExact(CommandArgument1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            String feedt = dt1.ToString("yyyy-MM-dd");

            DateTime dt2 = DateTime.ParseExact(CommandArgument2, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            String feedt1 = dt2.ToString("yyyy-MM-dd");

            string com = "select * from hr_staff_profile hsp inner join [hr_income_tax_sip] hit on hit.[tax_sip_staff_no]=hsp.stf_staff_no where hsp.stf_staff_no='" + Applcn_no1.Text + "' and [tax_sip_start_dt1]='" + feedt + "'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Button5.Visible = false;
                Button7.Visible = true;
                TextBox22.Text = double.Parse(dt.Rows[0]["tax_sip_amt1"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                TextBox23.Text = double.Parse(dt.Rows[0]["tax_sip_amt2"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                TextBox20.Text = dt.Rows[0]["sip_ahli_no"].ToString();
                TextBox21.Text = dt.Rows[0]["sip_majikan_no"].ToString();
                TextBox8.Text = Convert.ToDateTime(dt.Rows[0]["tax_sip_start_dt1"].ToString()).ToString("dd/MM/yyyy");
                TextBox9.Text = Convert.ToDateTime(dt.Rows[0]["tax_sip_end_dt1"].ToString()).ToString("dd/MM/yyyy");
                if (dt.Rows[0]["sip_chk"].ToString() == "1")
                {
                    sip_chk.Checked = true;
                }
                else
                {
                    sip_chk.Checked = false;
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void bind1()
    {
        dt = dbcon.Ora_Execute_table("select format(epf_eff_dt,'dd/MM/yyyy') epf_eff_dt,format(epf_end_dt,'dd/MM/yyyy')epf_end_dt,epf_percentage,epf_amt,epf_emp_kwsp_amt,epf_emp_kwsp_perc from hr_staff_profile hsp inner join hr_kwsp hk on hsp.stf_staff_no=hk.epf_staff_no where hsp.stf_staff_no='" + Applcn_no1.Text + "' ORDER BY hk.epf_eff_dt DESC");
        GridView2.DataSource = dt;
        GridView2.DataBind();

    }
    public void bind_cukai()
    {
        dt = dbcon.Ora_Execute_table("select format(tax_pcb_start_dt,'dd/MM/yyyy') tax_pcb_start_dt,format(tax_pcb_end_dt,'dd/MM/yyyy')tax_pcb_end_dt,tax_pcb_amt, tax_staff_no,tax_incometax_no from hr_staff_profile hsp inner join hr_income_tax it on hsp.stf_staff_no=it.tax_staff_no where hsp.stf_staff_no='" + Applcn_no1.Text + "' and ISNULL(tax_pcb_amt,'') != '' and tax_type ='1' ORDER BY it.tax_pcb_start_dt DESC");
        GridView5.DataSource = dt;
        GridView5.DataBind();

    }

    public void bind_cp38()
    {
        dt = dbcon.Ora_Execute_table("select format(tax_cp38_start_dt1,'dd/MM/yyyy') tax_pcb_start_dt,format(tax_cp38_end_dt1,'dd/MM/yyyy')tax_pcb_end_dt,tax_cp38_amt1, tax_staff_no from hr_staff_profile hsp inner join hr_income_tax it on hsp.stf_staff_no=it.tax_staff_no where hsp.stf_staff_no='" + Applcn_no1.Text + "' and ISNULL(tax_cp38_amt1,'') != '' and tax_type ='2' ORDER BY it.tax_cp38_start_dt1 DESC");
        GridView6.DataSource = dt;
        GridView6.DataBind();

    }

    public void bind_perkeso1()
    {
        dt = dbcon.Ora_Execute_table("select *,format(tax_cp38_start_dt1,'dd/MM/yyyy') as st_dt,format(tax_cp38_end_dt1,'dd/MM/yyyy') as ed_dt from hr_income_tax where tax_staff_no='" + Applcn_no1.Text + "' and ISNULL(tax_pcb_amt,'') = '' order by tax_cp38_start_dt1 desc");
        GridView3.DataSource = dt;
        GridView3.DataBind();

    }


    public void bind_sip1()
    {
        dt = dbcon.Ora_Execute_table("select *,format([tax_sip_start_dt1],'dd/MM/yyyy') as st_dt,format([tax_sip_end_dt1],'dd/MM/yyyy') as ed_dt from [hr_income_tax_sip] where [tax_sip_staff_no]='" + Applcn_no1.Text + "' order by tax_sip_start_dt1 desc");
        GridView4.DataSource = dt;
        GridView4.DataBind();

    }

    void PERKESO()
    {
        DataSet Ds = new DataSet();
        try
        {


            decimal a, b;
            //string com = "select top 1 stf_socso_no,org_socso_no,stf_age,format(slr_salary_amt,'#,##.00')slr_salary_amt from hr_staff_profile hsp left join hr_fixed_allowance hfa on hsp.stf_staff_no=hfa.fxa_staff_no left join hr_extra_allowance hea on hea.xta_staff_no=hsp.stf_staff_no left join hr_salary hs on hs.slr_staff_no=hsp.stf_staff_no  left join  hr_organization ho on ho.org_gen_id=hsp.str_curr_org_cd where hsp.stf_staff_no='" + Applcn_no1.Text + "'  order by slr_crt_dt desc"; //26/05/2017
            string com = "select  ISNULL(stf_socso_no,'') as stf_socso_no,org_socso_no,stf_age,stf_tax_no,org_income_tax_no from hr_staff_profile hsp left join  hr_organization ho on ho.org_gen_id=hsp.str_curr_org_cd where hsp.stf_staff_no='" + Applcn_no1.Text + "'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["stf_socso_no"].ToString().Trim() != "")
                {
                    TextBox5.Text = dt.Rows[0]["stf_socso_no"].ToString();
                    DataTable dd_stf = new DataTable();
                    dd_stf = dbcon.Ora_Execute_table("select stf_staff_no,stf_icno from hr_staff_profile where stf_staff_no='" + Applcn_no1.Text + "'");
                    TextBox20.Text = dd_stf.Rows[0]["stf_icno"].ToString();
                }
                else
                {
                    DataTable dd_stf = new DataTable();
                    dd_stf = dbcon.Ora_Execute_table("select stf_staff_no,stf_icno from hr_staff_profile where stf_staff_no='" + Applcn_no1.Text + "'");
                    TextBox5.Text = dd_stf.Rows[0]["stf_icno"].ToString();
                    TextBox20.Text = dd_stf.Rows[0]["stf_icno"].ToString();
                }
                TextBox6.Text = dt.Rows[0][1].ToString();
              

                if (dt.Rows[0]["stf_tax_no"].ToString().Trim() != "")
                {                    
                    TextBox20.Text = dt.Rows[0]["stf_tax_no"].ToString();
                }
                else
                {
                    DataTable dd_stf = new DataTable();
                    dd_stf = dbcon.Ora_Execute_table("select stf_staff_no,stf_icno from hr_staff_profile where stf_staff_no='" + Applcn_no1.Text + "'");
                    TextBox20.Text = dd_stf.Rows[0]["stf_icno"].ToString();
                }
                TextBox21.Text = dt.Rows[0]["org_income_tax_no"].ToString();
                DataTable dd_hrsal = new DataTable();
                dd_hrsal = dbcon.Ora_Execute_table("select * from hr_salary where slr_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt + "') between FORMAT(slr_eff_dt,'yyyy-MM') And FORMAT(slr_end_dt,'yyyy-MM')");
                if (dd_hrsal.Rows.Count != 0)
                {
                    if (dd_hrsal.Rows.Count == 2)
                    {
                        DataTable dd_hrsal_tot = new DataTable();
                        dd_hrsal_tot = dbcon.Ora_Execute_table("select *,day(slr_eff_dt) d1,day(slr_end_dt) d2,datediff(day, slr_eff_dt, dateadd(month, 1, slr_eff_dt)) as tdays,datediff(day, DATEADD(mm, 1, slr_eff_dt), dateadd(month, 1, DATEADD(mm, 1, slr_eff_dt))) as tdays1,((day(slr_end_dt)) * slr_salary_amt) / datediff(day, DATEADD(mm, 1, slr_eff_dt), dateadd(month, 1, DATEADD(mm, 1, slr_eff_dt))) as a,(((datediff(day, slr_eff_dt, dateadd(month, 1, slr_eff_dt)) - day(slr_eff_dt)) + 1) * slr_salary_amt) / datediff(day, slr_eff_dt, dateadd(month, 1, slr_eff_dt)) as b,((datediff(day, slr_eff_dt, dateadd(month, 1, slr_eff_dt)) - day(slr_eff_dt)) * slr_salary_amt) / datediff(day, slr_eff_dt, dateadd(month, 1, slr_eff_dt)) as bsal from hr_salary where slr_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt + "') between FORMAT(slr_eff_dt,'yyyy-MM') And FORMAT(slr_end_dt,'yyyy-MM')");
                        string aa = (double.Parse(dd_hrsal_tot.Rows[0]["a"].ToString()) + double.Parse(dd_hrsal_tot.Rows[1]["b"].ToString())).ToString();
                        TextBox2.Text = aa;
                    }
                    else
                    {
                        DataTable dd_hrsal_1 = new DataTable();
                        dd_hrsal_1 = dbcon.Ora_Execute_table("select slr_salary_amt from hr_salary where slr_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt + "') between FORMAT(slr_eff_dt,'yyyy-MM') And FORMAT(slr_end_dt,'yyyy-MM')");
                        TextBox2.Text = double.Parse(dd_hrsal_1.Rows[0]["slr_salary_amt"].ToString()).ToString();
                    }
                }
                else
                {
                    TextBox2.Text = "0.00";
                }

                perkeso_allowences();
                DataTable Get_tot = new DataTable();
                //Get_tot = dbcon.Ora_Execute_table("select a.s1,b.a1,(a.s1 + b.a1) as tot from (select top(1) slr_salary_amt as s1,slr_staff_no,slr_eff_dt from hr_salary where slr_staff_no='" + Applcn_no1.Text + "' order by slr_eff_dt DESC) as a full outer join (select sum(fxa_allowance_amt) as a1,fxa_staff_no from hr_fixed_allowance where fxa_staff_no='" + Applcn_no1.Text + "' group by fxa_staff_no) as b on b.fxa_staff_no=a.slr_staff_no order by a.slr_eff_dt Asc");

                //dd_fallowence = dbcon.Ora_Execute_table("select (sum(falw_amt) - sum(falw_amt1)) amt1 from (select ISNULL(sum(s1.fxa_allowance_amt),'') as falw_amt,'0.00' falw_amt1 from hr_fixed_allowance s1 Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=s1.fxa_allowance_type_cd and ss1.elau_kwsp='S' where ('" + act_dt + "' between FORMAT(fxa_eff_dt,'yyyy-MM') And FORMAT(fxa_end_dt,'yyyy-MM')) and fxa_staff_no='" + Applcn_no1.Text + "' union all select ISNULL(sum(s1.fxa_promo_amt),'') as falw_amt,ISNULL(sum(s1.fxa_allowance_amt),'') as falw_amt1 from hr_fixed_allowance s1 Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=s1.fxa_allowance_type_cd and ss1.elau_kwsp='S' where ('" + act_dt + "' between FORMAT(fxa_pst_dt,'yyyy-MM') And FORMAT(fxa_ped_dt,'yyyy-MM')) and fxa_staff_no='" + Applcn_no1.Text + "')as a");
                //if (dd_fallowence.Rows.Count != 0)
                //{
                //    cc1 = dd_fallowence.Rows[0]["amt1"].ToString();
                //}
                //else
                //{
                //    cc1 = "0.00";
                //}

                ////dd_xallowence = dbcon.Ora_Execute_table("select ISNULL(sum(s1.xta_allowance_amt),'') as xalw_amt from hr_extra_allowance s1 Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=s1.xta_allowance_type_cd and ss1.elau_kwsp='S' where s1.xta_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt + "' between FORMAT(xta_eff_dt,'yyyy-MM') And FORMAT(xta_end_dt,'yyyy-MM'))");
                //dd_xallowence = dbcon.Ora_Execute_table("select (sum(xalw_amt) - sum(xalw_amt1)) amt1 from(select ISNULL(sum(s1.xta_allowance_amt),'') as xalw_amt,'0.00' xalw_amt1 from hr_extra_allowance s1 Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=s1.xta_allowance_type_cd and ss1.elau_kwsp='S' where s1.xta_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt + "' between FORMAT(xta_eff_dt,'yyyy-MM') And FORMAT(xta_end_dt,'yyyy-MM')) union all select ISNULL(sum(s1.xta_promo_amt),'') as xalw_amt,ISNULL(sum(s1.xta_allowance_amt),'') as xalw_amt1 from hr_extra_allowance s1 Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=s1.xta_allowance_type_cd and ss1.elau_kwsp='S' where s1.xta_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt + "' between FORMAT(xta_pst_dt,'yyyy-MM') And FORMAT(xta_ped_dt,'yyyy-MM'))) as a");
                //if (dd_xallowence.Rows.Count != 0)
                //{
                //    cc2 = dd_xallowence.Rows[0]["amt1"].ToString();
                //}
                //else
                //{
                //    cc2 = "0.00";
                //}

                //perkeso

                Get_tot = dbcon.Ora_Execute_table("select b.a1,ISNULL(c.b1,'0.00') b1,ISNULL((ISNULL(b.a1,'0.00') + ISNULL(c.b1,'0.00')),'') as tot from (select sum(fxa_allowance_amt) as a1,fxa_staff_no from hr_fixed_allowance Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=fxa_allowance_type_cd and ss1.elau_perkeso='S' where fxa_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt + "' between FORMAT(fxa_eff_dt,'yyyy-MM') And FORMAT(fxa_end_dt,'yyyy-MM')) group by fxa_staff_no) as b full outer join (select sum(xta_allowance_amt) as b1,xta_staff_no from hr_extra_allowance Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=xta_allowance_type_cd and ss1.elau_perkeso='S' where xta_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt + "' between FORMAT(xta_eff_dt,'yyyy-MM') And FORMAT(xta_end_dt,'yyyy-MM')) group by xta_staff_no) as c on c.xta_staff_no=b.fxa_staff_no order by b.fxa_staff_no Asc");
                string tt_amt = string.Empty, tt_amt1 = string.Empty;

                string allowence_amt = string.Empty, sip_amt = string.Empty, tung_perk_amt = string.Empty, ot_amt_maj = string.Empty, ot_amt_emp = string.Empty, ded_amt = string.Empty, ded_emp_amt = string.Empty;

                if (Get_tot.Rows.Count != 0)
                {
                    allowence_amt = Get_tot.Rows[0]["tot"].ToString();
                }
                else
                {
                    allowence_amt = "0.00";
                }


                DataTable dd_hrsal_tun1 = new DataTable();
                dd_hrsal_tun1 = DBCon.Ora_Execute_table("select ISNULL(sum(ISNULL(tun_amt,'0.00')),'0.00') as amt from hr_tunggakan left join Ref_hr_tunggakan s1 on s1.hr_tung_Code=tun_type_cd  where s1.elau_perkeso ='S' and tun_staff_no='" + Applcn_no1.Text + "' and  tun_year='" + txt_tahu.Text + "' and tun_month='" + DD_PBB.SelectedValue + "'");
                if (dd_hrsal_tun1.Rows.Count != 0)
                {
                    tung_perk_amt = double.Parse(dd_hrsal_tun1.Rows[0]["amt"].ToString()).ToString();
                }
                else
                {
                    tung_perk_amt = "0.00";
                }


                DataTable dd_hrsal_ot1 = new DataTable();
                dd_hrsal_ot1 = DBCon.Ora_Execute_table("select ISNULL(sum(ISNULL(otl_ot_amt,'0.00')),'0.00') as amt from hr_ot left join Ref_hr_type_klm s1 on s1.typeklm_cd=otl_ot_type_cd  where s1.elau_perkeso ='S' and otl_staff_no='" + Applcn_no1.Text + "' and FORMAT(otl_work_dt,'yyyy-MM') ='" + act_dt + "'");
                if (dd_hrsal_ot1.Rows.Count != 0)
                {
                    ot_amt_emp = double.Parse(dd_hrsal_ot1.Rows[0]["amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                }
                else
                {
                    ot_amt_emp = "0.00";
                }


                //deduction
                DataTable tot_upl = new DataTable();
                tot_upl = dbcon.Ora_Execute_table("select ISNULL(sum(ded_deduct_amt),'') as s1 from hr_deduction left join Ref_hr_potongan s1 on s1.hr_poto_Code=ded_deduct_type_cd and Status='A' where s1.elau_perkeso='S' and ded_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt + "' between FORMAT(ded_start_dt,'yyyy-MM') And FORMAT(ded_end_dt,'yyyy-MM'))");
                //Math.Round(b, 2)
                if (tot_upl.Rows.Count != 0)
                {
                    ded_amt = double.Parse(tot_upl.Rows[0]["s1"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                }
                else
                {
                    ded_amt = "0.00";
                }

                DataTable dd_hrsal_bns1 = new DataTable();
                dd_hrsal_bns1 = DBCon.Ora_Execute_table("	select a.amt1,b.amt2 from (select ISNULL(sum(ISNULL(bns_amt,'0.00')),'0.00') as amt1 from hr_bonus left join Ref_hr_Claim s1 on s1.hr_clm_Code='01'  "
        + " where s1.clm_perkeso = 'S' and bns_staff_no = '" + Applcn_no1.Text + "' and('" + act_dt + "' between FORMAT(bns_eff_dt, 'yyyy-MM') And FORMAT(bns_end_dt, 'yyyy-MM'))) as a "
        + " outer apply (select ISNULL(sum(ISNULL(bns_kpi_amt, '0.00')), '0.00') as amt2 from hr_bonus left join Ref_hr_Claim s1 on s1.hr_clm_Code = '02' "
        + " where s1.clm_perkeso = 'S' and bns_staff_no = '" + Applcn_no1.Text + "' and('" + act_dt + "' between FORMAT(bns_eff_dt, 'yyyy-MM') And FORMAT(bns_end_dt, 'yyyy-MM'))) as b");
                if (dd_hrsal_bns1.Rows.Count != 0)
                {
                    bns_cc2 = double.Parse(dd_hrsal_bns1.Rows[0]["amt1"].ToString()).ToString() + double.Parse(dd_hrsal_bns1.Rows[0]["amt1"].ToString()).ToString();

                }
                else
                {
                    bns_cc2 = "0.00";
                }

                //SIP

                DataTable Get_tot_sip = new DataTable();
                Get_tot_sip = dbcon.Ora_Execute_table("select b.a1,ISNULL(c.b1,'0.00') b1,ISNULL((ISNULL(b.a1,'0.00') + ISNULL(c.b1,'0.00')),'') as tot from (select sum(fxa_allowance_amt) as a1,fxa_staff_no from hr_fixed_allowance Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=fxa_allowance_type_cd and ss1.elau_sip='S' where fxa_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt + "' between FORMAT(fxa_eff_dt,'yyyy-MM') And FORMAT(fxa_end_dt,'yyyy-MM')) group by fxa_staff_no) as b full outer join (select sum(xta_allowance_amt) as b1,xta_staff_no from hr_extra_allowance Inner join Ref_hr_jenis_elaun ss1 on ss1.hr_elau_Code=xta_allowance_type_cd and ss1.elau_sip='S' where xta_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt + "' between FORMAT(xta_eff_dt,'yyyy-MM') And FORMAT(xta_end_dt,'yyyy-MM')) group by xta_staff_no) as c on c.xta_staff_no=b.fxa_staff_no order by b.fxa_staff_no Asc");

                if (Get_tot_sip.Rows.Count != 0)
                {
                    sip_amt = Get_tot_sip.Rows[0]["tot"].ToString();
                }
                else
                {
                    sip_amt = "0.00";
                }

                DataTable dd_hrsal_tun = new DataTable();
                dd_hrsal_tun = DBCon.Ora_Execute_table("select ISNULL(sum(ISNULL(tun_amt,'0.00')),'0.00') as amt from hr_tunggakan left join Ref_hr_tunggakan s1 on s1.hr_tung_Code=tun_type_cd  where s1.elau_sip ='S' and tun_staff_no='" + Applcn_no1.Text + "' and  tun_year='" + txt_tahu.Text + "' and tun_month='" + DD_PBB.SelectedValue + "'");
                if (dd_hrsal_tun.Rows.Count != 0)
                {
                    cc5 = double.Parse(dd_hrsal_tun.Rows[0]["amt"].ToString()).ToString();
                }
                else
                {
                    cc5 = "0.00";
                }

                DataTable dd_hrsal_ot = new DataTable();
                dd_hrsal_ot = DBCon.Ora_Execute_table("select ISNULL(sum(ISNULL(otl_ot_amt,'0.00')),'0.00') as amt from hr_ot left join Ref_hr_type_klm s1 on s1.typeklm_cd=otl_ot_type_cd  where s1.elau_sip ='S' and otl_staff_no='" + Applcn_no1.Text + "' and FORMAT(otl_work_dt,'yyyy-MM') ='" + act_dt + "'");
                if (dd_hrsal_ot.Rows.Count != 0)
                {
                    ot_amt_maj = double.Parse(dd_hrsal_ot.Rows[0]["amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                }
                else
                {
                    ot_amt_maj = "0.00";
                }

                //deduction
                DataTable tot_upl1 = new DataTable();
                tot_upl1 = dbcon.Ora_Execute_table("select ISNULL(sum(ded_deduct_amt),'') as s1 from hr_deduction left join Ref_hr_potongan s1 on s1.hr_poto_Code=ded_deduct_type_cd and Status='A' where s1.elau_sip='S' and ded_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt + "' between FORMAT(ded_start_dt,'yyyy-MM') And FORMAT(ded_end_dt,'yyyy-MM'))");
                //Math.Round(b, 2)
                if (tot_upl1.Rows.Count != 0)
                {
                    ded_emp_amt = double.Parse(tot_upl1.Rows[0]["s1"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                }
                else
                {
                    ded_emp_amt = "0.00";
                }

                DataTable dd_hrsal_bns = new DataTable();
                dd_hrsal_bns = DBCon.Ora_Execute_table("	select a.amt1,b.amt2 from (select ISNULL(sum(ISNULL(bns_amt,'0.00')),'0.00') as amt1 from hr_bonus left join Ref_hr_Claim s1 on s1.hr_clm_Code='01'  "
        + " where s1.clm_sip = 'S' and bns_staff_no = '" + Applcn_no1.Text + "' and('" + act_dt + "' between FORMAT(bns_eff_dt, 'yyyy-MM') And FORMAT(bns_end_dt, 'yyyy-MM'))) as a "
        + " outer apply (select ISNULL(sum(ISNULL(bns_kpi_amt, '0.00')), '0.00') as amt2 from hr_bonus left join Ref_hr_Claim s1 on s1.hr_clm_Code = '02' "
        + " where s1.clm_sip = 'S' and bns_staff_no = '" + Applcn_no1.Text + "' and('" + act_dt + "' between FORMAT(bns_eff_dt, 'yyyy-MM') And FORMAT(bns_end_dt, 'yyyy-MM'))) as b");
                if (dd_hrsal_tun.Rows.Count != 0)
                {
                    bns_cc1 = double.Parse(dd_hrsal_bns.Rows[0]["amt1"].ToString()).ToString() + double.Parse(dd_hrsal_bns.Rows[0]["amt1"].ToString()).ToString();

                }
                else
                {
                    bns_cc1 = "0.00";
                }

                //    if (Get_tot.Rows.Count != 0)
                //{
                //tt_amt = (double.Parse(cc1) +  double.Parse(cc2) + double.Parse(cc5) + double.Parse(TextBox2.Text)).ToString();
                string st_amt_tot = (double.Parse(allowence_amt) + double.Parse(tung_perk_amt) + double.Parse(ot_amt_emp) + double.Parse(TextBox2.Text) + double.Parse(bns_cc2)).ToString();
                tt_amt = ( double.Parse(st_amt_tot) - double.Parse(ded_amt)).ToString();
                TextBox17.Text = tt_amt;
                tt_amt1 = ((double.Parse(sip_amt) + double.Parse(cc5) + double.Parse(ot_amt_maj) + double.Parse(TextBox2.Text) + double.Parse(bns_cc1)) - double.Parse(ded_emp_amt)).ToString();
                TextBox25.Text = tt_amt1;
                DataTable Get_perk = new DataTable();
                    Get_perk = dbcon.Ora_Execute_table("select top(1) " + v1 + " from hr_comm_perkeso where '" + tt_amt + "' between per_min_income_amt and per_max_income_amt");

                    DataTable Get_perkmaj = new DataTable();
                    Get_perkmaj = dbcon.Ora_Execute_table("select top(1) " + v2 + " from hr_comm_perkeso where '" + tt_amt + "' between per_min_income_amt and per_max_income_amt");

                    DataTable Get_perk1 = new DataTable();
                    Get_perk1 = dbcon.Ora_Execute_table("select top(1) " + v3 + " from hr_comm_SIP where '" + tt_amt1 + "' between SIP_min_income_amt and SIP_max_income_amt");

                    DataTable Get_perkmaj1 = new DataTable();
                    Get_perkmaj1 = dbcon.Ora_Execute_table("select top(1) " + v4 + " from hr_comm_SIP where '" + tt_amt1 + "' between SIP_min_income_amt and SIP_max_income_amt");

                    double amnt1 = double.Parse(Get_perk.Rows[0]["" + v1 + ""].ToString());
                    TextBox7.Text = amnt1.ToString("C").Replace("$", "").Replace("RM", "");


                    double amnt2 = double.Parse(Get_perkmaj.Rows[0]["" + v2 + ""].ToString());
                    TextBox3.Text = amnt2.ToString("C").Replace("$", "").Replace("RM", "");

                    double amnt3 = double.Parse(Get_perk1.Rows[0]["" + v3 + ""].ToString());
                    TextBox22.Text = amnt3.ToString("C").Replace("$", "").Replace("RM", "");

                    double amnt4 = double.Parse(Get_perkmaj1.Rows[0]["" + v4 + ""].ToString());
                    TextBox23.Text = amnt4.ToString("C").Replace("$", "").Replace("RM", "");
                //}
                //else
                //{
                //    TextBox7.Text = "0.00";
                //    TextBox3.Text = "0.00";
                //    TextBox22.Text = "0.00";
                //    TextBox23.Text = "0.00";
                //    tt_amt = "0.00";
                //}

            }

        }
        catch (Exception ex)
        {


            throw ex;
        }
    }

    void Potongan()
    {
        //DataSet Ds = new DataSet();
        //try
        //{
            
        //    string com = "select * from hr_staff_profile hsp inner join hr_income_tax hit on hit.tax_staff_no=hsp.stf_staff_no where hsp.stf_staff_no='" + Applcn_no1.Text + "'";
        //    SqlDataAdapter adpt = new SqlDataAdapter(com, con);
        //    DataTable dt = new DataTable();
        //    adpt.Fill(dt);
        //    if (dt.Rows.Count > 0)
        //    {
        //        TextBox7.Text = double.Parse(dt.Rows[0]["tax_cp38_amt1"].ToString()).ToString("C").Replace("$","").Replace("RM", "");
        //        TextBox3.Text = double.Parse(dt.Rows[0]["tax_cp38_amt2"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
        //        TextBox5.Text = dt.Rows[0]["perk_ahli_no"].ToString();
        //        TextBox6.Text = dt.Rows[0]["perk_mjik_no"].ToString();
        //        TextBox20.Text = dt.Rows[0]["sip_ahli_no"].ToString();
        //        TextBox21.Text = dt.Rows[0]["sip_mjik_no"].ToString();
        //        TextBox22.Text = double.Parse(dt.Rows[0]["sip_ahli_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
        //        TextBox23.Text = double.Parse(dt.Rows[0]["sip_mjik_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
        //        TextBox12.Text = Convert.ToDateTime(dt.Rows[0]["tax_cp38_start_dt1"].ToString()).ToString("dd/MM/yyyy");
        //        TextBox11.Text = Convert.ToDateTime(dt.Rows[0]["tax_cp38_end_dt1"].ToString()).ToString("dd/MM/yyyy");
        //        TextBox8.Text = Convert.ToDateTime(dt.Rows[0]["tax_cp38_start_dt2"].ToString()).ToString("dd/MM/yyyy");
        //        TextBox9.Text = Convert.ToDateTime(dt.Rows[0]["tax_cp38_end_dt2"].ToString()).ToString("dd/MM/yyyy");
        //        if(dt.Rows[0]["sip_chk"].ToString() == "1")
        //        {
        //            sip_chk.Checked = true;
        //        }
        //        else
        //        {
        //            sip_chk.Checked = false;
        //        }
        //        if (dt.Rows[0]["perkeso_chk"].ToString() == "1")
        //        {
        //            perkeso_chk.Checked = true;
        //        }
        //        else
        //        {
        //            perkeso_chk.Checked = false;
        //        }
        //    }

        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
    }

   

    public void bind2()
    {
        dt = dbcon.Ora_Execute_table("select hd.id,ded_deduct_type_cd,hjp.hr_poto_desc,ded_ref_no,ISNULL(CASE WHEN ded_start_dt = '1900-01-01 00:00:00.000' THEN '' ELSE FORMAT(ded_start_dt,'dd/MM/yyyy', 'en-us') END, '') AS ded_start_dt,ISNULL(CASE WHEN ded_end_dt = '1900-01-01 00:00:00.000' THEN '' ELSE FORMAT(ded_end_dt,'dd/MM/yyyy', 'en-us') END, '') AS ded_end_dt,cast(ded_deduct_amt as decimal(10,2)) as ded_deduct_amt from hr_staff_profile hsp inner join hr_deduction  hd on hd.ded_staff_no=hsp.stf_staff_no inner join Ref_hr_potongan hjp on hjp.hr_poto_Code=hd.ded_deduct_type_cd where hsp.stf_staff_no='" + Applcn_no1.Text + "' order by hd.ded_start_dt desc");
        GridView1.DataSource = dt;
        GridView1.DataBind();

    }

    protected void lblSubItemName_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
        CommandArgument1 = commandArgs[0];
        CommandArgument2 = commandArgs[1];
        KWSP();
    }
    protected void lblSubItemName_Click_cukai(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
        CommandArgument1 = commandArgs[0];
        CommandArgument2 = commandArgs[1];
        cukai();
    }

    protected void CP_lblSubItemName_Click_cukai(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
        CommandArgument1 = commandArgs[0];
        CommandArgument2 = commandArgs[1];
        cp38();
    }

    protected void lblSubItemName_Click_perkeso(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
        CommandArgument1 = commandArgs[0];
        CommandArgument2 = commandArgs[1];
        get_perkeso();
    }

    protected void lblSubItemName_Click_sip(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
        CommandArgument1 = commandArgs[0];
        CommandArgument2 = commandArgs[1];
        get_sip();
    }

    protected void lblSubItem_Click(object sender, EventArgs e)
    {

        LinkButton btn = (LinkButton)sender;
        string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
        CommandArgument1 = commandArgs[0];
        bindchl();
    }

    public void bindchl()
    {
        //DataTable ddps = new DataTable();
        //ddps = dbcon.Ora_Execute_table("select hr_poto_code,hr_poto_desc from Ref_hr_potongan where  hr_poto_desc='" + CommandArgument1 + "'");
        DataTable ddorg = new DataTable();

        SqlConnection conn = new SqlConnection(cs);

        string query2 = "select ded_deduct_type_cd,ded_ref_no,ISNULL(CASE WHEN ded_start_dt = '1900-01-01 00:00:00.000' THEN '' ELSE FORMAT(ded_start_dt,'dd/MM/yyyy', 'en-us') END, '') as ded_start_dt,ISNULL(CASE WHEN ded_end_dt = '1900-01-01 00:00:00.000' THEN '' ELSE FORMAT(ded_end_dt,'dd/MM/yyyy', 'en-us') END, '') as ded_end_dt,cast(ded_deduct_amt as decimal(10,2)) as ded_deduct_amt from hr_deduction where ded_staff_no='" + Applcn_no1.Text + "' and Id='" + CommandArgument1 + "'";
        conn.Open();
        var sqlCommand3 = new SqlCommand(query2, conn);
        var sqlReader3 = sqlCommand3.ExecuteReader();
        while (sqlReader3.Read())
        {
            Button8.Visible = false;
            Button9.Visible = true;
            //DropDownList21.Attributes.Add("style","Pointer-events:None;");
            DropDownList21.SelectedValue = (string)sqlReader3["ded_deduct_type_cd"].ToString().Trim();
            TextBox32.Text = (string)sqlReader3["ded_ref_no"].ToString().Trim();
            // var feedt = Convert.ToDateTime(sqlReader3["ded_start_dt"]).ToString("dd/MM/yyyy");
            TextBox28.Text = sqlReader3["ded_start_dt"].ToString();

            //var feedt1 = Convert.ToDateTime(sqlReader3["ded_end_dt"]).ToString("dd/MM/yyyy");
            TextBox29.Text = sqlReader3["ded_end_dt"].ToString();
            TextBox30.Text = (string)sqlReader3["ded_deduct_amt"].ToString().Trim();
            TextBox15.Text = CommandArgument1;

        }
    }

    protected void Button15_Click(object sender, EventArgs e)
    {
        if (txtsno.Text != "")
        {
            if (TextBox18.Text != "")
            {
                if (TextBox18.Text != "" && TextBox19.Text != "")
                {
                    useid = Session["New"].ToString();

                    string feedt = string.Empty, feedt1 = string.Empty, ssdt = string.Empty, ssdt1 = string.Empty, sd = string.Empty;


                    DateTime time1 = DateTime.ParseExact(TextBox18.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    feedt = time1.ToString("yyyy-MM-dd");

                    DateTime time2 = DateTime.ParseExact(TextBox19.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    feedt1 = time2.ToString("yyyy-MM-dd");
                    if ((time2 - time1).TotalDays > 0)
                    {

                        //ckmaj();
                        //ckahl();
                       
                        dt = dbcon.Ora_Execute_table("insert into hr_income_tax(tax_staff_no,tax_incometax_no,tax_pcb_amt,tax_pcb_start_dt,tax_pcb_end_dt,tax_crt_id,tax_crt_dt,tax_type)values('" + Applcn_no1.Text + "','" + TextBox10.Text + "','" + TextBox24.Text + "','" + feedt + "','" + feedt1 + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','1')");
                      //  dtt1 = dbcon.Ora_Execute_table("UPDATE hr_staff_profile set stf_epf_no='" + txtahli.Text + "' where stf_staff_no='" + Applcn_no1.Text + "'");
                      //  dtt1 = dbcon.Ora_Execute_table("UPDATE hr_organization set org_epf_no='" + txtmaji.Text + "' where org_gen_id='" + TextBox1.Text + "'");
                        //dtt1 = dbcon.Ora_Execute_table("UPDATE hr_income set inc_emp_kwsp_perc='" + txtckm.Text + "',inc_emp_kwsp_amt='" + txtacm.Text + "' where inc_staff_no='" + txtsno.Text + "' and inc_month='" + DateTime.Now.ToString("MM") + "' and inc_year='" + DateTime.Now.ToString("yyyy") + "'");
                        clr_cukai();
                        bind_cukai();
                        service.audit_trail("P0067", "Cukai Pendapatan Simpan", "NO KAKITANGAN", Applcn_no1.Text);
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Cukai Pendapatan Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    }
                    else
                    {
                        bind_cukai();
                        txttakhir.Text = "31/12/9999";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Tarikh Tamat Tidak Sah',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    bind_cukai();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila masukkan Field Tarikh',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih The Tarikh Mula',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan kakitangan Tiada',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    protected void CP_Button15_Click(object sender, EventArgs e)
    {
        if (txtsno.Text != "")
        {
           
                if (TextBox31.Text != "" && TextBox33.Text != "")
                {
                    useid = Session["New"].ToString();

                    string feedt = string.Empty, feedt1 = string.Empty, ssdt = string.Empty, ssdt1 = string.Empty, sd = string.Empty;


                    DateTime time1 = DateTime.ParseExact(TextBox31.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    feedt = time1.ToString("yyyy-MM-dd");

                    DateTime time2 = DateTime.ParseExact(TextBox33.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    feedt1 = time2.ToString("yyyy-MM-dd");
                if ((time2 - time1).TotalDays > 0)
                {
                    DataTable get_cp38 = new DataTable();
                    get_cp38 = DBCon.Ora_Execute_table("select tax_staff_no from hr_income_tax where tax_staff_no='" + Applcn_no1.Text + "' and tax_cp38_start_dt1 ='" + feedt + "' and tax_type ='2'");
                    if (get_cp38.Rows.Count == 0)
                    {
                        dt = dbcon.Ora_Execute_table("insert into hr_income_tax(tax_staff_no,tax_cp38_amt1,tax_cp38_start_dt1,tax_cp38_end_dt1,tax_crt_id,tax_crt_dt,tax_type)values('" + Applcn_no1.Text + "','" + TextBox34.Text + "','" + feedt + "','" + feedt1 + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','2')");
                        clr_cp38();
                        bind_cp38();
                        service.audit_trail("P0067", "CP 38 Simpan", "NO KAKITANGAN", Applcn_no1.Text);
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod CP 38 Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    bind_cp38();
                    TextBox33.Text = "31/12/9999";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Tarikh Tamat Tidak Sah',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
              
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih The Tarikh Mula',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan kakitangan Tiada',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }
    protected void Button12_Click(object sender, EventArgs e)
    {
        if (txtsno.Text != "")
        {
            if (txttmula.Text != "")
            {
                if (txttmula.Text != "" && txttakhir.Text != "")
                {
                    useid = Session["New"].ToString();

                    string feedt = string.Empty, feedt1 = string.Empty, ssdt = string.Empty, ssdt1 = string.Empty, sd = string.Empty;


                    DateTime time1 = DateTime.ParseExact(txttmula.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    feedt = time1.ToString("yyyy-MM-dd");

                    DateTime time2 = DateTime.ParseExact(txttakhir.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    feedt1 = time2.ToString("yyyy-MM-dd");
                    if ((time2 - time1).TotalDays > 0)
                    {

                        //ckmaj();
                        //ckahl();
                        dt = dbcon.Ora_Execute_table("insert into hr_kwsp(epf_staff_no,epf_eff_dt,epf_end_dt,epf_amt,epf_crt_id,epf_crt_dt,epf_emp_kwsp_amt,epf_percentage,epf_emp_kwsp_perc)values('" + Applcn_no1.Text + "','" + feedt + "','" + feedt1 + "','" + txtacahli.Text + "','" + useid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + txtacm.Text + "','" + txtcaruahli.Text + "','" + txtkmaji.Text + "')");
                        dtt1 = dbcon.Ora_Execute_table("UPDATE hr_staff_profile set stf_epf_no='" + txtahli.Text + "' where stf_staff_no='" + Applcn_no1.Text + "'");
                        dtt1 = dbcon.Ora_Execute_table("UPDATE hr_organization set org_epf_no='" + txtmaji.Text + "' where org_gen_id='" + TextBox1.Text + "'");
                        //dtt1 = dbcon.Ora_Execute_table("UPDATE hr_income set inc_emp_kwsp_perc='" + txtckm.Text + "',inc_emp_kwsp_amt='" + txtacm.Text + "' where inc_staff_no='" + txtsno.Text + "' and inc_month='" + DateTime.Now.ToString("MM") + "' and inc_year='" + DateTime.Now.ToString("yyyy") + "'");
                        clr_kwsp();
                        bind1();
                        service.audit_trail("P0067", "KWSP Simpan", "NO KAKITANGAN", Applcn_no1.Text);
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod KWSP Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    }
                    else
                    {
                        bind1();
                        txttakhir.Text = "31/12/9999";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Tarikh Tamat Tidak Sah',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    bind1();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila masukkan Field Tarikh',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih The Tarikh Mula',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan kakitangan Tiada',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    void clr_kwsp()
    {

        txttmula.Text = "";
        txttakhir.Text = "31/12/9999";
        //txtacahli.Text = "";
        //RdbBwar.Checked = false;
        //Rdbwar.Checked = true;
        //txtacm.Text = "";
        //txtckm.Text = "";
        //txtcka.Text = "";
    }
    void clr_cukai()
    {

        TextBox18.Text = "";
        TextBox19.Text = "31/12/9999";
        TextBox10.Text = "";
        //RdbBwar.Checked = false;
        //Rdbwar.Checked = true;
        TextBox24.Text = "";
        //txtckm.Text = "";
        //txtcka.Text = "";
    }

    void clr_cp38()
    {

        TextBox31.Text = "";
        TextBox33.Text = "31/12/9999";
        TextBox34.Text = "";
    }

    void pon()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select hr_poto_code,hr_poto_desc from Ref_hr_potongan where Status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList21.DataSource = dt;
            DropDownList21.DataTextField = "hr_poto_desc";
            DropDownList21.DataValueField = "hr_poto_code";
            DropDownList21.DataBind();
            DropDownList21.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //protected void Button2_Click(object sender, EventArgs e)
    //{
    //    if (txtsno.Text != "" && TextBox8.Text != "")
    //    {
    //        //if (TextBox12.Text != "" && TextBox11.Text != "")
    //        //{
    //            string tdt1 = string.Empty, tdt2 = string.Empty, tdt3 = string.Empty, tdt4 = string.Empty, tdt5 = string.Empty, schk = string.Empty, pchk = string.Empty;
    //            dt = dbcon.Ora_Execute_table("select tax_staff_no from hr_income_tax_sip where tax_staff_no='" + Applcn_no1.Text + "'");

    //        if (TextBox8.Text != "")
    //        {
    //            DateTime today3 = DateTime.ParseExact(TextBox8.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
    //            tdt3 = today3.ToString("yyyy-MM-dd");
    //        }
    //        if (TextBox9.Text != "")
    //        {
    //            DateTime today4 = DateTime.ParseExact(TextBox9.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
    //            tdt4 = today4.ToString("yyyy-MM-dd");
    //        }

    //        if(sip_chk.Checked == true)
    //        {
    //            schk = "1";
    //        }
    //        else
    //        {
    //            schk = "0";
    //        }


    //        if (dt.Rows.Count == 0)
    //                {

    //                dt1 = dbcon.Ora_Execute_table("insert into hr_income_tax(tax_staff_no,tax_cp38_start_dt1,tax_cp38_end_dt1,tax_cp38_start_dt2,tax_cp38_end_dt2,tax_cp38_amt1,tax_cp38_amt2,perk_ahli_no,perk_mjik_no,sip_ahli_no,sip_mjik_no,sip_ahli_amt,sip_mjik_amt,tax_crt_id,tax_crt_dt,tax_incometax_no,sip_chk,perkeso_chk)values('" + Applcn_no1.Text + "','" + tdt1 + "','" + tdt2 + "','" + tdt3 + "','" + tdt4 + "','" + TextBox7.Text + "','" + TextBox3.Text + "','" + TextBox5.Text + "','" + TextBox6.Text + "','" + TextBox20.Text + "','" + TextBox21.Text + "','" + TextBox22.Text + "','" + TextBox23.Text + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','','"+ schk +"','"+ pchk +"')");
    //                dt2 = dbcon.Ora_Execute_table("update hr_staff_profile set stf_socso_no='" + TextBox5.Text + "',stf_tax_no='"+ TextBox20.Text + "' where stf_staff_no='" + Applcn_no1.Text + "'");
    //                dt3 = dbcon.Ora_Execute_table("update hr_organization set org_socso_no='" + TextBox6.Text + "' where org_gen_id='" + TextBox1.Text + "'");
    //                dt4 = dbcon.Ora_Execute_table("update hr_income set inc_emp_perkeso_amt='" + TextBox3.Text + "' where inc_staff_no='" + Applcn_no1.Text + "' and inc_month='" + DateTime.Now.ToString("MM") + "' and inc_year='" + DateTime.Now.ToString("yyyy") + "'");

    //                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Cukai Pendapatan Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
    //                }
    //                else
    //                {
    //                //dt = dbcon.Ora_Execute_table("update hr_income_tax set tax_pcb_amt='" + TextBox9.Text + "',tax_cp38_start_dt1='" + tdt1 + "',tax_cp38_end_dt1='" + tdt2 + "',tax_cp38_amt1='" + TextBox8.Text + "',tax_cp38_start_dt2='" + tdt3 + "',tax_cp38_end_dt2='" + tdt4 + "',tax_cp38_amt2='" + TextBox27.Text + "',tax_upd_id='" + Session["New"].ToString() + "',tax_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "',tax_incometax_no='" + TextBox10.Text + "' where tax_staff_no='" + txtsno.Text + "'");
    //                //dt1 = dbcon.Ora_Execute_table("update hr_income_tax set tax_pcb_amt='" + TextBox17.Text + "',tax_cp38_start_dt1='" + tdt1 + "',tax_cp38_end_dt1='" + tdt2 + "',tax_cp38_amt1='" + TextBox8.Text + "',tax_upd_id='" + Session["New"].ToString() + "',tax_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "',tax_incometax_no='" + TextBox10.Text + "' where tax_staff_no='" + Applcn_no1.Text + "'"); // 24_07_2018
    //                dt1 = dbcon.Ora_Execute_table("update hr_income_tax set sip_chk='"+ schk + "',perkeso_chk='" + pchk + "',tax_cp38_amt1='" + TextBox7.Text + "',tax_cp38_amt2='" + TextBox3.Text + "',perk_ahli_no='" + TextBox5.Text + "',perk_mjik_no='" + TextBox6.Text + "',sip_ahli_no='" + TextBox20.Text + "',sip_mjik_no='" + TextBox21.Text + "',sip_ahli_amt='" + TextBox22.Text + "',sip_mjik_amt='" + TextBox23.Text + "',tax_cp38_start_dt1='" + tdt1 + "',tax_cp38_end_dt1='" + tdt2 + "',tax_cp38_start_dt2='" + tdt3 + "',tax_cp38_end_dt2='" + tdt4 + "',tax_upd_id='" + Session["New"].ToString() + "',tax_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "',tax_incometax_no='' where tax_staff_no='" + Applcn_no1.Text + "'");
    //                //dt2 = dbcon.Ora_Execute_table("update hr_staff_profile set stf_socso_no='" + TextBox5.Text + "',stf_tax_no='" + TextBox10.Text + "' where stf_staff_no='" + Applcn_no1.Text + "'");// 24_07_2018
    //                dt2 = dbcon.Ora_Execute_table("update hr_staff_profile set stf_socso_no='" + TextBox5.Text + "',stf_tax_no='" + TextBox20.Text + "' where stf_staff_no='" + Applcn_no1.Text + "'");
    //                dt3 = dbcon.Ora_Execute_table("update hr_organization set org_socso_no='" + TextBox6.Text + "' where org_gen_id='" + TextBox1.Text + "'");
    //                    dt4 = dbcon.Ora_Execute_table("update hr_income set inc_emp_perkeso_amt='" + TextBox3.Text + "' where inc_staff_no='" + Applcn_no1.Text + "' and inc_month='" + DateTime.Now.ToString("MM") + "' and inc_year='" + DateTime.Now.ToString("yyyy") + "'");
    //                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Cukai Pendapatan Berjaya Dikemaskini',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
    //                }

    //        //}
    //        //else
    //        //{

    //        //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila masukkan Field Tarikh',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
    //        //}
    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan kakitangan Tiada',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
    //    }

    //}


    protected void sip_ins_Click(object sender, EventArgs e)
    {
        if (txtsno.Text != "" && TextBox8.Text != "")
        {
            //if (TextBox12.Text != "" && TextBox11.Text != "")
            //{
            string tdt1 = string.Empty, tdt2 = string.Empty, tdt3 = string.Empty, tdt4 = string.Empty, tdt5 = string.Empty, schk = string.Empty, pchk = string.Empty;


            DataTable get_tax_no = new DataTable();
            get_tax_no = DBCon.Ora_Execute_table("select (count(*) + 1) as cnt from hr_income_tax_sip");

            if (TextBox8.Text != "")
            {
                DateTime today1 = DateTime.ParseExact(TextBox8.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                tdt1 = today1.ToString("yyyy-MM-dd");
            }
            if (TextBox9.Text != "")
            {
                DateTime today2 = DateTime.ParseExact(TextBox9.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                tdt2 = today2.ToString("yyyy-MM-dd");
            }


            if (sip_chk.Checked == true)
            {
                schk = "1";
            }
            else
            {
                schk = "0";
            }
            dt = dbcon.Ora_Execute_table("select tax_sip_staff_no from hr_income_tax_sip where tax_sip_staff_no='" + Applcn_no1.Text + "' and tax_sip_start_dt1='" + tdt1 + "' ");

            if (dt.Rows.Count == 0)
            {
                dt1 = dbcon.Ora_Execute_table("insert into hr_income_tax_sip(tax_sip_staff_no,tax_incometax_sip_no,[tax_sip_start_dt1],[tax_sip_end_dt1],[tax_sip_amt1],[tax_sip_amt2],sip_ahli_no,sip_majikan_no,[tax_sip_crt_id],[tax_sip_crt_dt],[sip_chk])values('" + Applcn_no1.Text + "','" + get_tax_no.Rows[0]["cnt"].ToString() + "','" + tdt1 + "','" + tdt2 + "','" + TextBox22.Text + "','" + TextBox23.Text + "','" + TextBox20.Text + "','" + TextBox21.Text + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + schk + "')");
                dt2 = dbcon.Ora_Execute_table("update hr_staff_profile set stf_tax_no='" + TextBox20.Text + "' where stf_staff_no='" + Applcn_no1.Text + "'");
                dt3 = dbcon.Ora_Execute_table("update hr_organization set org_income_tax_no='" + TextBox21.Text + "' where org_gen_id='" + TextBox1.Text + "'");
                bind_sip1();
                clr4();
                service.audit_trail("P0067", "Cukai Pendapatan Simpan", "NO KAKITANGAN", Applcn_no1.Text);
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Cukai Pendapatan Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

            //}
            //else
            //{

            //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila masukkan Field Tarikh',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            //}
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }

    }

    protected void sip_upd_Click(object sender, EventArgs e)
    {
        if (txtsno.Text != "" && TextBox8.Text != "")
        {
            //if (TextBox12.Text != "" && TextBox11.Text != "")
            //{
            string tdt1 = string.Empty, tdt2 = string.Empty, tdt3 = string.Empty, tdt4 = string.Empty, tdt5 = string.Empty, schk = string.Empty, pchk = string.Empty;


            DataTable get_tax_no = new DataTable();
            get_tax_no = DBCon.Ora_Execute_table("select (count(*) + 1) as cnt from [hr_income_tax_sip]");

            if (TextBox8.Text != "")
            {
                DateTime today1 = DateTime.ParseExact(TextBox8.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                tdt1 = today1.ToString("yyyy-MM-dd");
            }
            if (TextBox9.Text != "")
            {
                DateTime today2 = DateTime.ParseExact(TextBox9.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                tdt2 = today2.ToString("yyyy-MM-dd");
            }


            if (sip_chk.Checked == true)
            {
                schk = "1";
            }
            else
            {
                schk = "0";
            }
            dt = dbcon.Ora_Execute_table("select tax_sip_staff_no from hr_income_tax_sip where tax_sip_staff_no='" + Applcn_no1.Text + "' and tax_sip_start_dt1='" + tdt1 + "' ");
            if (dt.Rows.Count != 0)
            {
                dt1 = dbcon.Ora_Execute_table("update hr_income_tax_sip set sip_chk='" + schk + "',[tax_sip_amt1]='" + TextBox22.Text + "',[tax_sip_amt2]='" + TextBox23.Text + "',sip_ahli_no='" + TextBox20.Text + "',sip_majikan_no='" + TextBox21.Text + "',[tax_sip_start_dt1]='" + tdt1 + "',[tax_sip_end_dt1]='" + tdt2 + "',[tax_sip_upd_id]='" + Session["New"].ToString() + "',[tax_sip_upd_dt]='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where [tax_sip_staff_no]='" + Applcn_no1.Text + "' and [tax_sip_start_dt1]='" + tdt1 + "'");
                dt2 = dbcon.Ora_Execute_table("update hr_staff_profile set stf_tax_no='" + TextBox20.Text + "' where stf_staff_no='" + Applcn_no1.Text + "'");
                dt3 = dbcon.Ora_Execute_table("update hr_organization set org_income_tax_no='" + TextBox21.Text + "' where org_gen_id='" + TextBox1.Text + "'");
                bind_sip1();
                Button5.Visible = true;
                Button7.Visible = false;
                clr4();
                service.audit_trail("P0067", "Cukai Pendapatan Kemaskini", "NO KAKITANGAN", Applcn_no1.Text);
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Cukai Pendapatan Berjaya Dikemaskini',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan kakitangan Tiada',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }

    }
    protected void perkeso_ins_Click(object sender, EventArgs e)
    {
        if (txtsno.Text != "" && TextBox12.Text != "")
        {
            //if (TextBox12.Text != "" && TextBox11.Text != "")
            //{
            string tdt1 = string.Empty, tdt2 = string.Empty, tdt3 = string.Empty, tdt4 = string.Empty, tdt5 = string.Empty, schk = string.Empty, pchk = string.Empty;
          

            DataTable get_tax_no = new DataTable();
            get_tax_no = DBCon.Ora_Execute_table("select (count(*) + 1) as cnt from hr_income_tax");

            if (TextBox12.Text != "")
            {
                DateTime today1 = DateTime.ParseExact(TextBox12.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                tdt1 = today1.ToString("yyyy-MM-dd");
            }
            if (TextBox11.Text != "")
            {
                DateTime today2 = DateTime.ParseExact(TextBox11.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                tdt2 = today2.ToString("yyyy-MM-dd");
            }

       
            if (perkeso_chk.Checked == true)
            {
                pchk = "1";
            }
            else
            {
                pchk = "0";
            }
            dt = dbcon.Ora_Execute_table("select tax_staff_no from hr_income_tax where tax_staff_no='" + Applcn_no1.Text + "' and tax_cp38_start_dt1='"+ tdt1 + "' ");

            if (dt.Rows.Count == 0)
            {
                dt1 = dbcon.Ora_Execute_table("insert into hr_income_tax(tax_staff_no,tax_incometax_no,tax_cp38_start_dt1,tax_cp38_end_dt1,tax_cp38_amt1,tax_cp38_amt2,perk_ahli_no,perk_mjik_no,tax_crt_id,tax_crt_dt,perkeso_chk)values('" + Applcn_no1.Text + "','"+ get_tax_no.Rows[0]["cnt"].ToString() + "','" + tdt1 + "','" + tdt2 + "','" + TextBox7.Text + "','" + TextBox3.Text + "','" + TextBox5.Text + "','" + TextBox6.Text + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + pchk + "')");
                dt2 = dbcon.Ora_Execute_table("update hr_staff_profile set stf_socso_no='" + TextBox5.Text + "' where stf_staff_no='" + Applcn_no1.Text + "'");
                dt3 = dbcon.Ora_Execute_table("update hr_organization set org_socso_no='" + TextBox6.Text + "' where org_gen_id='" + TextBox1.Text + "'");
                dt4 = dbcon.Ora_Execute_table("update hr_income set inc_emp_perkeso_amt='" + TextBox3.Text + "' where inc_staff_no='" + Applcn_no1.Text + "' and inc_month='" + DD_PBB.SelectedValue + "' and inc_year='" + txt_tahu.Text + "'");
                bind_perkeso1();
                clr3();
                service.audit_trail("P0067", "Cukai Pendapatan Simpan", "NO KAKITANGAN", Applcn_no1.Text);
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Cukai Pendapatan Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

            //}
            //else
            //{

            //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila masukkan Field Tarikh',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            //}
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }

    }

    protected void perkeso_upd_Click(object sender, EventArgs e)
    {
        if (txtsno.Text != "" )
        {
            //if (TextBox12.Text != "" && TextBox11.Text != "")
            //{
            string tdt1 = string.Empty, tdt2 = string.Empty, tdt3 = string.Empty, tdt4 = string.Empty, tdt5 = string.Empty, schk = string.Empty, pchk = string.Empty;
          

            DataTable get_tax_no = new DataTable();
            get_tax_no = DBCon.Ora_Execute_table("select (count(*) + 1) as cnt from hr_income_tax");

            if (TextBox12.Text != "")
            {
                DateTime today1 = DateTime.ParseExact(TextBox12.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                tdt1 = today1.ToString("yyyy-MM-dd");
            }
            if (TextBox11.Text != "")
            {
                DateTime today2 = DateTime.ParseExact(TextBox11.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                tdt2 = today2.ToString("yyyy-MM-dd");
            }


            if (perkeso_chk.Checked == true)
            {
                pchk = "1";
            }
            else
            {
                pchk = "0";
            }
            dt = dbcon.Ora_Execute_table("select tax_staff_no from hr_income_tax where tax_staff_no='" + Applcn_no1.Text + "' and tax_cp38_start_dt1='" + tdt1 + "' ");
            if (dt.Rows.Count != 0)
            {
                dt1 = dbcon.Ora_Execute_table("update hr_income_tax set sip_chk='" + schk + "',perkeso_chk='" + pchk + "',tax_cp38_amt1='" + TextBox7.Text + "',tax_cp38_amt2='" + TextBox3.Text + "',perk_ahli_no='" + TextBox5.Text + "',perk_mjik_no='" + TextBox6.Text + "',tax_cp38_start_dt1='" + tdt1 + "',tax_cp38_end_dt1='" + tdt2 + "',tax_upd_id='" + Session["New"].ToString() + "',tax_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where tax_staff_no='" + Applcn_no1.Text + "' and tax_cp38_start_dt1='" + tdt1 + "'");
                dt2 = dbcon.Ora_Execute_table("update hr_staff_profile set stf_socso_no='" + TextBox5.Text + "' where stf_staff_no='" + Applcn_no1.Text + "'");
                dt3 = dbcon.Ora_Execute_table("update hr_organization set org_socso_no='" + TextBox6.Text + "' where org_gen_id='" + TextBox1.Text + "'");
                dt4 = dbcon.Ora_Execute_table("update hr_income set inc_emp_perkeso_amt='" + TextBox3.Text + "' where inc_staff_no='" + Applcn_no1.Text + "' and inc_month='" + DD_PBB.SelectedValue + "' and inc_year='" + txt_tahu.Text + "'");
                bind_perkeso1();
                Button3.Visible = true;
                Button4.Visible = false;
                clr3();
                service.audit_trail("P0067", "Cukai Pendapatan Kemaskini", "NO KAKITANGAN", Applcn_no1.Text);
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Cukai Pendapatan Berjaya Dikemaskini',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan kakitangan Tiada',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }

    }

    protected void Button8_Click(object sender, EventArgs e)
    {
        if (txtsno.Text != "" && TextBox28.Text != "")
        {
            string tdt1 = string.Empty, tdt2 = string.Empty;
            if (TextBox28.Text != "" && TextBox29.Text != "")
            {
                DateTime t1 = DateTime.ParseExact(TextBox28.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                tdt1 = t1.ToString("yyyy-MM-dd");


                DateTime t2 = DateTime.ParseExact(TextBox29.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                tdt2 = t2.ToString("yyyy-MM-dd");
                if ((t2 - t1).TotalDays >= 0)
                {

                    dt = dbcon.Ora_Execute_table("select ded_staff_no,ded_deduct_type_cd from hr_deduction where ded_staff_no='" + Applcn_no1.Text + "' and ded_deduct_type_cd='" + DropDownList21.SelectedValue + "' and ded_start_dt='" + tdt1 + "'");
                    if (dt.Rows.Count == 0)
                    {

                        dt = dbcon.Ora_Execute_table("insert into hr_deduction(ded_staff_no,ded_deduct_type_cd,ded_ref_no,ded_start_dt,ded_end_dt,ded_deduct_amt,ded_crt_id,ded_crt_dt)values('" + Applcn_no1.Text + "','" + DropDownList21.SelectedValue + "','" + TextBox32.Text + "','" + tdt1 + "','" + tdt2 + "','" + TextBox30.Text + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')");
                        bind2();
                        clr();
                        service.audit_trail("P0067", "Lain-lain Potongan Simpan", "NO KAKITANGAN", Applcn_no1.Text);
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Maklumat Lain-lain Potongan Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Sudah Wujud',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    bind2();
                    TextBox29.Text = "31/12/9999";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Tarikh Tamat Tidak Sah',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                bind2();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila masukkan Field Tarikh',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan kakitangan Tiada',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    protected void Button9_Click(object sender, EventArgs e)
    {
        if (txtsno.Text != "")
        {
            if (DropDownList21.SelectedItem.Text != "")
            {
                string tdt1 = string.Empty, tdt2 = string.Empty;
                if (TextBox28.Text != "" && TextBox29.Text != "")
                {
                    DateTime t1 = DateTime.ParseExact(TextBox28.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    tdt1 = t1.ToString("yyyy-MM-dd");


                    DateTime t2 = DateTime.ParseExact(TextBox29.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    tdt2 = t2.ToString("yyyy-MM-dd");


                    dt = dbcon.Ora_Execute_table("select ded_staff_no,ded_deduct_type_cd from hr_deduction where ded_staff_no='" + Applcn_no1.Text + "'");
                    if (dt.Rows.Count > 0)
                    {

                        dt = dbcon.Ora_Execute_table("update hr_deduction set ded_ref_no='" + TextBox32.Text + "',ded_start_dt='" + tdt1 + "',ded_end_dt='" + tdt2 + "',ded_deduct_amt='" + TextBox30.Text + "',ded_upd_id='" + Session["New"].ToString() + "',ded_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where ded_staff_no='" + Applcn_no1.Text + "' and id='" + TextBox15.Text + "'");
                        bind2();
                        Button8.Visible = true;
                        Button9.Visible = false;
                        clr();
                        service.audit_trail("P0067", "Lain-lain Potongan Kemaskini", "NO KAKITANGAN", Applcn_no1.Text);
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Maklumat Lain-lain Potongan Berjaya Dikemaskini',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    }
                }
                else
                {
                    bind2();
                    TextBox29.Text = "31/12/9999";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Tarikh Tamat Tidak Sah',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih The Jenis Potongan',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan kakitangan Tiada',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    void clr()
    {
        DropDownList21.SelectedValue = "";
        TextBox32.Text = "";
        TextBox28.Text = "";
        TextBox29.Text = "31/12/9999";
        TextBox30.Text = "";
        DropDownList21.Attributes.Remove("Style");
    }

    protected void Button10_Click(object sender, EventArgs e)
    {
        string rcount = string.Empty;
        int count = 0;
        foreach (GridViewRow gvrow in GridView1.Rows)
        {
            var rb = gvrow.FindControl("RadioButton1") as System.Web.UI.WebControls.RadioButton;
            if (rb.Checked)
            {
                count++;
            }
            rcount = count.ToString();
        }
        if (rcount != "0")
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                RadioButton rbn = new RadioButton();
                rbn = (RadioButton)row.FindControl("RadioButton1");
                if (rbn.Checked)
                {
                    int RowIndex = row.RowIndex;

                    string varName1 = ((LinkButton)row.FindControl("lblSubItemName")).Text.ToString(); //this store the  value in varName1
                    DataTable ddicno = new DataTable();
                    ddicno = dbcon.Ora_Execute_table("select hr_poto_code,hr_poto_desc from Ref_hr_potongan where hr_poto_desc='" + varName1 + "' ");
                    string stffno = ddicno.Rows[0]["hr_poto_code"].ToString();
                    SqlCommand ins_peng = new SqlCommand("delete from hr_deduction where ded_staff_no='" + Applcn_no1.Text + "' and ded_deduct_type_cd ='" + stffno + "'", con);
                    con.Open();
                    int i = ins_peng.ExecuteNonQuery();
                    con.Close();
                    service.audit_trail("P0067", "Lain-lain Potongan Hapus", "NO KAKITANGAN", Applcn_no1.Text);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Rekod Maklumat Lain-lain Potongan Berjaya Dihapuskan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    Button8.Visible = true;
                    Button9.Visible = false;
                    clr();
                    bind2();
                }

            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Sila Pilih Rekod Yang Ingin Dihapuskan',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }
    protected void TextBox5_TextChanged(object sender, EventArgs e)
    {

    }
    protected void Button13_Click(object sender, EventArgs e)
    {
        if (txtsno.Text != "")
        {
            if (txttmula.Text != "" && txttakhir.Text != "")
            {
                //ckahl();
                //ckmaj();
                DateTime dt4 = DateTime.ParseExact(txttmula.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                String feedt = dt4.ToString("yyyy-MM-dd");
                DateTime dt5 = DateTime.ParseExact(txttakhir.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                String feedt1 = dt5.ToString("yyyy-MM-dd");
                if ((dt5 - dt4).TotalDays > 0)
                {
                    useid = Session["New"].ToString();
                    dt = dbcon.Ora_Execute_table("update hr_kwsp set epf_end_dt='" + feedt1 + "',epf_maji_per='" + txtacm.Text + "',epf_ahli_per='" + txtcaruahli.Text + "',epf_amt='" + txtacahli.Text + "',epf_upd_id='" + useid + "',epf_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "',epf_emp_kwsp_amt='" + txtacm.Text + "' where epf_staff_no='" + Applcn_no1.Text + "' and epf_eff_dt='" + feedt + "'");
                    dtt1 = dbcon.Ora_Execute_table("UPDATE hr_staff_profile set stf_epf_no='" + txtahli.Text + "' where stf_staff_no='" + Applcn_no1.Text + "'");
                    dtt1 = dbcon.Ora_Execute_table("UPDATE hr_organization set org_epf_no='" + txtmaji.Text + "' where org_gen_id='" + TextBox1.Text + "'");
                    //dtt1 = dbcon.Ora_Execute_table("UPDATE hr_income set inc_emp_kwsp_perc='" + txtckm.Text + "',inc_emp_kwsp_amt='" + txtacm.Text + "' where inc_staff_no='" + txtsno.Text + "' and inc_month='" + DateTime.Now.ToString("MM") + "' and inc_year='" + DateTime.Now.ToString("yyyy") + "'");
                    bind1();
                    clr2();
                    Button13.Visible = false;
                    Button12.Visible = true;
                    service.audit_trail("P0067", "KWSP Kemaskini", "NO KAKITANGAN", Applcn_no1.Text);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod KWSP Berjaya Dikemaskini',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
                else
                {
                    bind1();
                    txttakhir.Text = "31/12/9999";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Tarikh Tamat Tidak Sah',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }

            }
            else
            {
                bind1();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila masukkan Field Tarikh',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan kakitangan Tiada',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }


    protected void Button16_Click(object sender, EventArgs e)
    {
        if (txtsno.Text != "")
        {
            if (TextBox18.Text != "" && TextBox19.Text != "")
            {
                //ckahl();
                //ckmaj();
                DateTime dt4 = DateTime.ParseExact(TextBox18.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                String feedt = dt4.ToString("yyyy-MM-dd");
                DateTime dt5 = DateTime.ParseExact(TextBox19.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                String feedt1 = dt5.ToString("yyyy-MM-dd");
                if ((dt5 - dt4).TotalDays > 0)
                {
                    useid = Session["New"].ToString();
                    dt = dbcon.Ora_Execute_table("update hr_income_tax set tax_incometax_no='" + TextBox10.Text + "', tax_pcb_end_dt='" + feedt1 + "',tax_pcb_amt='" + TextBox24.Text + "',tax_upd_id='" + useid + "',tax_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where tax_staff_no='" + Applcn_no1.Text + "' and tax_pcb_start_dt='" + feedt + "' and tax_type ='1'");
                   // dtt1 = dbcon.Ora_Execute_table("UPDATE hr_staff_profile set stf_epf_no='" + txtahli.Text + "' where stf_staff_no='" + Applcn_no1.Text + "'");
                   // dtt1 = dbcon.Ora_Execute_table("UPDATE hr_organization set org_epf_no='" + txtmaji.Text + "' where org_gen_id='" + TextBox1.Text + "'");
                    //dtt1 = dbcon.Ora_Execute_table("UPDATE hr_income set inc_emp_kwsp_perc='" + txtckm.Text + "',inc_emp_kwsp_amt='" + txtacm.Text + "' where inc_staff_no='" + txtsno.Text + "' and inc_month='" + DateTime.Now.ToString("MM") + "' and inc_year='" + DateTime.Now.ToString("yyyy") + "'");
                    bind_cukai();
                    clr_cukai();
                    Button16.Visible = false;
                    Button15.Visible = true;
                    service.audit_trail("P0067", "Cukai Pendapatan Kemaskini", "NO KAKITANGAN", Applcn_no1.Text);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Cukai Pendapatan Berjaya Dikemaskini',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
                else
                {
                    bind_cukai();
                    TextBox19.Text = "31/12/9999";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Tarikh Tamat Tidak Sah',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }

            }
            else
            {
                bind_cukai();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila masukkan Field Tarikh',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan kakitangan Tiada',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    protected void CP_Button16_Click(object sender, EventArgs e)
    {
        if (txtsno.Text != "")
        {
            if (TextBox31.Text != "" && TextBox33.Text != "")
            {
                //ckahl();
                //ckmaj();
                DateTime dt4 = DateTime.ParseExact(TextBox31.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                String feedt = dt4.ToString("yyyy-MM-dd");
                DateTime dt5 = DateTime.ParseExact(TextBox33.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                String feedt1 = dt5.ToString("yyyy-MM-dd");
                if ((dt5 - dt4).TotalDays > 0)
                {
                    useid = Session["New"].ToString();
                    dt = dbcon.Ora_Execute_table("update hr_income_tax set tax_cp38_end_dt1='" + feedt1 + "',tax_cp38_amt1='" + TextBox34.Text + "',tax_upd_id='" + useid + "',tax_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where tax_staff_no='" + Applcn_no1.Text + "' and tax_cp38_start_dt1='" + feedt + "' and tax_type ='2'");
                    // dtt1 = dbcon.Ora_Execute_table("UPDATE hr_staff_profile set stf_epf_no='" + txtahli.Text + "' where stf_staff_no='" + Applcn_no1.Text + "'");
                    // dtt1 = dbcon.Ora_Execute_table("UPDATE hr_organization set org_epf_no='" + txtmaji.Text + "' where org_gen_id='" + TextBox1.Text + "'");
                    //dtt1 = dbcon.Ora_Execute_table("UPDATE hr_income set inc_emp_kwsp_perc='" + txtckm.Text + "',inc_emp_kwsp_amt='" + txtacm.Text + "' where inc_staff_no='" + txtsno.Text + "' and inc_month='" + DateTime.Now.ToString("MM") + "' and inc_year='" + DateTime.Now.ToString("yyyy") + "'");
                    bind_cp38();
                    clr_cp38();
                    Button19.Visible = false;
                    Button18.Visible = true;
                    service.audit_trail("P0067", "CP 38 Kemaskini", "NO KAKITANGAN", Applcn_no1.Text);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod CP 38 Berjaya Dikemaskini',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
                else
                {
                    bind_cp38();
                    TextBox33.Text = "31/12/9999";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Tarikh Tamat Tidak Sah',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }

            }
            else
            {
                bind_cp38();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila masukkan Field Tarikh',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan kakitangan Tiada',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    void clr2()
    {
        txttmula.Text = "";
        txttmula.Attributes.Remove("style");
        txttakhir.Text = "31/12/9999";
        //RadioButton3.Checked = false;
        //RadioButton2.Checked = true;
        //Rdbwar.Checked = true;
        //RdbBwar.Checked = false;
        //txtacahli.Text = "";
        //txtckm.Text = "";
        //txtcka.Text = "";
        //txtacm.Text = "";
        view_details();
    }

    void clr3()
    {
        TextBox12.Text = "";
        TextBox11.Text = "31/12/9999";
        perkeso_chk.Checked = false;
        view_details();
    }

    void clr4()
    {
        TextBox8.Text = "";
        TextBox9.Text = "31/12/9999";
        sip_chk.Checked = false;
        view_details();
    }

    protected void rst_Click(object sender, EventArgs e)
    {
        Response.Redirect("HR_SELENG_POTON.aspx");
    }

    //protected void calc_kwsp1(object sender, EventArgs e)
    //{
    //    if (txtsno.Text != "")
    //    {
    //        kwsp_allowences();
    //        string AC_ahli = (((double.Parse(txtcka.Text) / 100) * double.Parse(b_slry)) + double.Parse(cc1) + double.Parse(cc2)).ToString();
    //        txtacahli.Text = double.Parse(AC_ahli).ToString("C").Replace("$","");
    //    }
    //}


    protected void Button14_Click(object sender, EventArgs e)
    {
        string rcount = string.Empty;
        int count = 0;
        foreach (GridViewRow gvrow in GridView2.Rows)
        {
            var rb = gvrow.FindControl("RadioButton1") as System.Web.UI.WebControls.RadioButton;
            if (rb.Checked)
            {
                count++;
            }
            rcount = count.ToString();
        }
        if (rcount != "0")
        {
            foreach (GridViewRow row in GridView2.Rows)
            {
                RadioButton rbn = new RadioButton();
                rbn = (RadioButton)row.FindControl("RadioButton1");
                if (rbn.Checked)
                {
                    int RowIndex = row.RowIndex;

                    string varName1 = ((LinkButton)row.FindControl("lblSubItem")).Text.ToString(); //this store the  value in varName1
                 
                    //var feedt = Convert.ToDateTime(varName1).ToString("yyyy/mm/dd");
                    DateTime time3 = DateTime.ParseExact(varName1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string feedt = time3.ToString("yyyy-MM-dd");
                    dbcon.Ora_Execute_CommamdText("delete from hr_kwsp where epf_staff_no='" + Applcn_no1.Text + "' and epf_eff_dt ='" + feedt + "'");
                    service.audit_trail("P0067", "KWSP Hapus", "NO KAKITANGAN", Applcn_no1.Text);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod KWSP Berjaya Dihapuskan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    bind1();
                    clr2();
                    Button13.Visible = false;
                    Button12.Visible = true;
                }

            }
        }
        else
        {            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    protected void Button17_Click(object sender, EventArgs e)
    {
        string rcount = string.Empty;
        int count = 0;
        foreach (GridViewRow gvrow in GridView5.Rows)
        {
            var rb = gvrow.FindControl("RadioButton1") as System.Web.UI.WebControls.RadioButton;
            if (rb.Checked)
            {
                count++;
            }
            rcount = count.ToString();
        }
        if (rcount != "0")
        {
            foreach (GridViewRow row in GridView5.Rows)
            {
                RadioButton rbn = new RadioButton();
                rbn = (RadioButton)row.FindControl("RadioButton1");
                if (rbn.Checked)
                {
                    int RowIndex = row.RowIndex;

                    string varName1 = ((LinkButton)row.FindControl("lblSubItem")).Text.ToString(); //this store the  value in varName1
                    string varName2 = ((Label)row.FindControl("lblSubno")).Text.ToString(); //this store the  value in varName1
                    //var feedt = Convert.ToDateTime(varName1).ToString("yyyy/mm/dd");
                    DateTime time3 = DateTime.ParseExact(varName1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string feedt = time3.ToString("yyyy-MM-dd");
                    dbcon.Ora_Execute_CommamdText("delete from hr_income_tax where tax_staff_no='" + Applcn_no1.Text + "' and tax_pcb_start_dt ='" + feedt + "' and tax_incometax_no='" + varName2 + "' and tax_type ='1'");
                    service.audit_trail("P0067", "Cukai Pendapatan Hapus", "NO KAKITANGAN", Applcn_no1.Text);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Cukai Pendapatan Berjaya Dihapuskan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    bind_cukai();
                    clr_cukai();
                    Button16.Visible = false;
                    Button15.Visible = true;
                }

            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    protected void CP_Button17_Click(object sender, EventArgs e)
    {
        string rcount = string.Empty;
        int count = 0;
        foreach (GridViewRow gvrow in GridView6.Rows)
        {
            var rb = gvrow.FindControl("RadioButton1") as System.Web.UI.WebControls.RadioButton;
            if (rb.Checked)
            {
                count++;
            }
            rcount = count.ToString();
        }
        if (rcount != "0")
        {
            foreach (GridViewRow row in GridView6.Rows)
            {
                RadioButton rbn = new RadioButton();
                rbn = (RadioButton)row.FindControl("RadioButton1");
                if (rbn.Checked)
                {
                    int RowIndex = row.RowIndex;

                    string varName1 = ((LinkButton)row.FindControl("lblSubItem")).Text.ToString(); //this store the  value in varName1
                    string varName2 = ((Label)row.FindControl("lblSubno")).Text.ToString(); //this store the  value in varName1
                    //var feedt = Convert.ToDateTime(varName1).ToString("yyyy/mm/dd");
                    DateTime time3 = DateTime.ParseExact(varName1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string feedt = time3.ToString("yyyy-MM-dd");
                    dbcon.Ora_Execute_CommamdText("delete from hr_income_tax where tax_staff_no='" + Applcn_no1.Text + "' and tax_cp38_start_dt1 ='" + feedt + "' and tax_type ='2'");
                    service.audit_trail("P0067", "CP 38 Hapus", "NO KAKITANGAN", Applcn_no1.Text);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod CP 38 Berjaya Dihapuskan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    bind_cp38();
                    clr_cp38();
                    Button19.Visible = false;
                    Button18.Visible = true;
                }

            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }


    protected void perkeso_del_Click(object sender, EventArgs e)
    {
        string rcount = string.Empty;
        int count = 0;
        foreach (GridViewRow gvrow in GridView3.Rows)
        {
            var rb = gvrow.FindControl("RadioButton1") as System.Web.UI.WebControls.RadioButton;
            if (rb.Checked)
            {
                count++;
            }
            rcount = count.ToString();
        }
        if (rcount != "0")
        {
            foreach (GridViewRow row in GridView3.Rows)
            {
                RadioButton rbn = new RadioButton();
                rbn = (RadioButton)row.FindControl("RadioButton1");
                if (rbn.Checked)
                {
                    int RowIndex = row.RowIndex;

                    string varName1 = ((LinkButton)row.FindControl("lblSubItem1")).Text.ToString(); //this store the  value in varName1
                    //var feedt = Convert.ToDateTime(varName1).ToString("yyyy/mm/dd");
                    DateTime time3 = DateTime.ParseExact(varName1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string feedt = time3.ToString("yyyy-MM-dd");
                    dbcon.Ora_Execute_CommamdText("delete from hr_income_tax where tax_staff_no='" + Applcn_no1.Text + "' and tax_cp38_start_dt1 ='" + feedt + "'");
                    bind_perkeso1();
                    clr3();
                    Button4.Visible = false;
                    Button3.Visible = true;
                    service.audit_trail("P0067", "Perkeso Hapus", "NO KAKITANGAN", Applcn_no1.Text);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Perkeso Berjaya Dihapuskan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                  
                }

            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    protected void sip_del_Click(object sender, EventArgs e)
    {
        string rcount = string.Empty;
        int count = 0;
        foreach (GridViewRow gvrow in GridView4.Rows)
        {
            var rb = gvrow.FindControl("RadioButton1") as System.Web.UI.WebControls.RadioButton;
            if (rb.Checked)
            {
                count++;
            }
            rcount = count.ToString();
        }
        if (rcount != "0")
        {
            foreach (GridViewRow row in GridView4.Rows)
            {
                RadioButton rbn = new RadioButton();
                rbn = (RadioButton)row.FindControl("RadioButton1");
                if (rbn.Checked)
                {
                    int RowIndex = row.RowIndex;

                    string varName1 = ((LinkButton)row.FindControl("lblSubItem2")).Text.ToString(); //this store the  value in varName1
                    //var feedt = Convert.ToDateTime(varName1).ToString("yyyy/mm/dd");
                    DateTime time3 = DateTime.ParseExact(varName1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string feedt = time3.ToString("yyyy-MM-dd");
                    dbcon.Ora_Execute_CommamdText("delete from hr_income_tax_sip where tax_sip_staff_no='" + Applcn_no1.Text + "' and tax_sip_start_dt1 ='" + feedt + "'");
                    bind_sip1();
                    clr4();
                    Button7.Visible = false;
                    Button5.Visible = true;
                    service.audit_trail("P0067", "SIP Hapus", "NO KAKITANGAN", Applcn_no1.Text);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod SIP Berjaya Dihapuskan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);

                }

            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }



    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../SUMBER_MANUSIA/Hr_seleng_poton.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../SUMBER_MANUSIA/Hr_seleng_poton_view.aspx");
    }


}