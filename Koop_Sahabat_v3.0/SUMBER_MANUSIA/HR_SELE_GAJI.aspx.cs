using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.ComponentModel;
using System.Text;
using System.Net;
using System.Data.Common;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Net.Mail;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Reporting.WebForms;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Web.SessionState;
using System.Xml;
public partial class HR_SELE_GAJI : System.Web.UI.Page
{

    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    private static int PageSize = 20;
    DataTable dt = new DataTable();
    string Status = string.Empty;
    string level, userid;
    string act_dt = string.Empty, et_amt = string.Empty;
    string phdate1 = string.Empty, phdate2 = string.Empty, phdate3 = string.Empty, phdate4 = string.Empty;
    string etdate1 = string.Empty, etdate2 = string.Empty, etdate3 = string.Empty, etdate4 = string.Empty;
    string gt_val1 = string.Empty, gt_val2 = string.Empty, gt_val3 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        assgn_roles();
        string script = " $(function () {$('.select2').select2();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;
                GP_rno.Text = "0";
                ET_rno.Text = "0";
                LLE_rno.Text = "0";
                BankBind();
                SebabBind();
                JelaunBind();
                Type_klm();
                Type_tunggakan();
                all_grid();
                month();
                month1();
                TextBox22.Attributes.Add("Readonly", "Readonly");
                Kaki_no.Attributes.Add("Readonly", "Readonly");
                s_nama.Attributes.Add("Readonly", "Readonly");
                s_jaw.Attributes.Add("Readonly", "Readonly");
                s_jab.Attributes.Add("Readonly", "Readonly");
                s_gred.Attributes.Add("Readonly", "Readonly");
                txt_org.Attributes.Add("Readonly", "Readonly");
                TextBox2.Attributes.Add("Readonly", "Readonly");
                TextBox20.Attributes.Add("Readonly", "Readonly");
                TextBox21.Attributes.Add("Readonly", "Readonly");
               
                Applcn_no1.Text = "";

                GE_date.Text = "31/12/9999";
                ET_sdate.Text = "31/12/9999";
                LL_sdate.Text = "31/12/9999";
                //TextBox8.Text = "31/12/9999";

                DD_PBB.SelectedValue = DateTime.Now.ToString("MM");//24_01
                txt_tahu.Text = DateTime.Now.ToString("yyyy");//24_01
                TextBox22.Text = "KLM Bagi " + DD_PBB.SelectedItem.Text + " " + txt_tahu.Text;
               
                DropDownList2.SelectedValue = DateTime.Now.ToString("MM");
               
                TextBox9.Text = DateTime.Now.ToString("yyyy");
                if (samp != "")
                {
                    Kaki_no.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    view_details();

                }
                else
                {

                }
                Wday();
                userid = Session["New"].ToString();
                grid1();

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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('455','448','505','484','77','1565','513','1675','497','1288','190','1707','1037','52','61','64','65','1708','89','133','15','1709','1406','1710','502','1300','1712','1713','1714','1715','1716','1717','1718','1719') order by ID ASC");

            CultureInfo culinfo = Thread.CurrentThread.CurrentUICulture;
            TextInfo txtinfo = culinfo.TextInfo;

            h1_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            bb1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            bb2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());

            h3_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());

            pt1.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[22][0].ToString().ToLower());
            pt2.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[23][0].ToString().ToLower());
            pt3.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[30][0].ToString().ToLower());
            pt4.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[31][0].ToString().ToLower());
            //pt5.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[32][0].ToString().ToLower());//24_01
            pt6.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[33][0].ToString().ToLower());

            lbl1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            lbl2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[20][0].ToString().ToLower());
            lbl3_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower());
            lbl4_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[21][0].ToString().ToLower());
            lbl5_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
            lbl6_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[17][0].ToString().ToLower());
            lbl7_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            lbl8_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[16][0].ToString().ToLower());
            lbl9_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            lbl10_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            lbl11_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            lbl12_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[23][0].ToString().ToLower() + " (RM)");
            lbl13_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());

            lbl14_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            lbl15_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            lbl16_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[24][0].ToString().ToLower());
            lbl17_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[19][0].ToString());
            lbl18_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            lbl19_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            lbl20_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[24][0].ToString().ToLower());
            lbl21_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[19][0].ToString());
            //lbl22_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[25][0].ToString().ToLower());//24_01
            //lbl23_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());//24_01
            //lbl24_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[18][0].ToString().ToLower());//24_01
            //lbl25_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[26][0].ToString().ToLower());//24_01
            //lbl26_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[27][0].ToString().ToLower());//24_01
            lbl27_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            lbl28_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            lbl29_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[28][0].ToString().ToLower() + " (RM) ");
            lbl30_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[29][0].ToString() + " (RM) ");


            Button19.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            Button15.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());

            Button5.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            Button7.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());

            Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());

            Button8.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            Button10.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            Button16.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());

            //Button6.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());//24_01
            //Button11.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());//24_01
            //Button17.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());//24_01

            Button12.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            Button14.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            Button18.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }

    }


    protected void grid1()
    {
        string sqry1 = string.Empty;
        Label25.Text = txt_tahu.Text;
        Label27.Text = DD_PBB.SelectedItem.Text;
        if (txt_tahu.Text != "" && DD_PBB.SelectedItem.Text != "")
        {
            sqry1 = "select *,Format(otd_work_dt,'dd/MM/yyyy') as tdt from hr_daily_ot"
                            + " where"
                            + " otd_staff_no='" + Kaki_no.Text + "' and otd_month='" + DD_PBB.SelectedValue + "' and otd_year='" + txt_tahu.Text + "'";

        }
        
        con.Open();
        DataTable ddicno = new DataTable();
        SqlCommand cmd = new SqlCommand(sqry1, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView7.DataSource = ds;
            GridView7.DataBind();
            int columncount = GridView7.Rows[0].Cells.Count;
            GridView7.Rows[0].Cells.Clear();
            GridView7.Rows[0].Cells.Add(new TableCell());
            GridView7.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView7.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
            hari_cuti.Text = "0";
        }
        else
        {
            DataTable get_otd_info2 = new DataTable();
            get_otd_info2 = DBCon.Ora_Execute_table("select * from hr_daily_ot where otd_staff_no='" + Kaki_no.Text + "' and otd_month='" + DD_PBB.SelectedValue + "' and otd_year='" + txt_tahu.Text + "'");
            GridView7.DataSource = ds;
            GridView7.DataBind();
            GridView7.FooterRow.Cells[4].Text = "JUMLAH JAM KLM :";
            GridView7.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            if (get_otd_info2.Rows.Count != 0)
            {
                
                ((System.Web.UI.WebControls.Label)GridView7.FooterRow.Cells[4].FindControl("lblTotal12")).Text = float.Parse(get_otd_info2.Rows[0]["otd_jumlah_hour"].ToString()).ToString();
                double n = (double.Parse(get_otd_info2.Rows[0]["otd_jumlah_hour"].ToString()) / 8);
                double d = Math.Floor(n);
                double v = n - d;
                double h = 0;
                if (v < 0.5) { h = 0; }
                if (v >= 0.5) { h = 0.5; }
                double t = d + h;
                hari_cuti.Text = Convert.ToString(t);
            }
            else
            {
                hari_cuti.Text = "0";
            }
           
        }

        con.Close();
    }
    void assgn_roles()
    {
        if (Session["New"] != null)
        {
            DataTable ddokdicno = new DataTable();
            ddokdicno = DBCon.Ora_Execute_table("select * from KK_User_Login where KK_userid = '" + Session["userid"].ToString() + "'");

            if (ddokdicno.Rows.Count != 0)
            {
                DataTable ddokdicno_1 = new DataTable();
                ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0066' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

                if (ddokdicno_1.Rows.Count != 0)
                {

                    gt_val1 = ddokdicno_1.Rows[0]["Add_chk"].ToString();
                    gt_val2 = ddokdicno_1.Rows[0]["Edit_chk"].ToString();

                    if (gt_val1 == "1")
                    {
                        Button15.Visible = true;
                        Button5.Visible = true;
                        Button7.Visible = true;
                        Button2.Visible = true;
                        Button4.Visible = true;
                        Button8.Visible = true;
                        Button10.Visible = true;
                        //Button6.Visible = true;//24_01
                        //Button11.Visible = true;//24_01
                        Button12.Visible = true;
                        Button14.Visible = true;
                    }
                    else
                    {
                        Button5.Visible = false;
                        Button15.Visible = false;
                        Button7.Visible = false;
                        Button2.Visible = false;
                        Button4.Visible = false;
                        Button8.Visible = false;
                        Button10.Visible = false;
                        //Button6.Visible = false;//24_01
                        //Button11.Visible = false;//24_01
                        Button12.Visible = false;
                        Button14.Visible = false;
                    }

                }
            }
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    void view_details()
    {
        try
        {
            if (Kaki_no.Text != "")
            {

                DataTable select_kaki1 = new DataTable();
                select_kaki1 = DBCon.Ora_Execute_table("select * from hr_staff_profile where '" + Kaki_no.Text + "' IN (stf_staff_no,stf_name)");
                if (select_kaki1.Rows.Count != 0)
                {
                    Applcn_no1.Text = select_kaki1.Rows[0]["stf_staff_no"].ToString();
                    DataTable select_kaki1_pos = new DataTable();
                    select_kaki1_pos = DBCon.Ora_Execute_table("select * from hr_post_his where pos_staff_no='" + Applcn_no1.Text + "' and pos_end_dt='9999-12-31'");
                    if (select_kaki1_pos.Rows.Count != 0)
                    {
                        DataTable select_kaki = new DataTable();
                        //select_kaki = DBCon.Ora_Execute_table("select a.stf_name,ISNULL(hj.hr_jaba_desc,'') as hr_jaba_desc,ISNULL(hg.hr_gred_desc,'') as hr_gred_desc,ISNULL(rhj.hr_jaw_desc,'') as hr_jaw_desc,ISNULL(a.stf_bank_acc_no,'') as stf_bank_acc_no,ISNULL(rnb.Bank_Name,'') as Bank_Name,ISNULL(a.stf_bank_cd,'') as stf_bank_cd from (select * from hr_staff_profile as sp where sp.stf_staff_no='" + Kaki_no.Text + "') as a full outer join(select * from hr_post_his where pos_staff_no='" + Kaki_no.Text + "' and pos_end_dt='9999-12-31') as b left join Ref_hr_jabatan as hj on hj.hr_jaba_Code=b.pos_dept_cd left join Ref_hr_gred as hg on hg.hr_gred_Code=b.pos_grade_cd left join Ref_hr_Jawatan as rhj on rhj.hr_jaw_Code=b.pos_post_cd on b.pos_staff_no=a.stf_staff_no left join Ref_Nama_Bank as rnb on rnb.Bank_Code=a.stf_bank_cd");
                        select_kaki = DBCon.Ora_Execute_table("select s3.op_state_cd,sp.str_curr_org_cd,sp.stf_name,ISNULL(hj.hr_jaba_desc,'') as hr_jaba_desc,ISNULL(hg.hr_gred_desc,'') as hr_gred_desc,ISNULL(rhj.hr_jaw_desc,'') as hr_jaw_desc,ISNULL(sp.stf_bank_acc_no,'') as stf_bank_acc_no,ISNULL(rnb.Bank_Name,'') as Bank_Name,ISNULL(sp.stf_bank_cd,'') as stf_bank_cd,ho.org_name,o1.op_perg_name from hr_staff_profile as sp left join hr_post_his ph on ph.pos_staff_no=sp.stf_staff_no and pos_end_dt='9999-12-31' left join Ref_hr_jabatan as hj on hj.hr_jaba_Code=ph.pos_dept_cd left join Ref_hr_gred as hg on hg.hr_gred_Code=ph.pos_grade_cd left join Ref_hr_Jawatan as rhj on rhj.hr_jaw_Code=ph.pos_post_cd left join Ref_Nama_Bank as rnb on rnb.Bank_Code=sp.stf_bank_cd left join hr_organization ho on ho.org_gen_id=str_curr_org_cd left join hr_organization_pern s3 on s3.op_perg_code=stf_cur_sub_org left join hr_organization_pern o1 on o1.op_perg_code=sp.stf_cur_sub_org where sp.stf_staff_no='" + Applcn_no1.Text + "'");
                        if (select_kaki.Rows.Count != 0)
                        {
                            s_nama.Text = select_kaki.Rows[0]["stf_name"].ToString();
                            Label22.Text = Applcn_no1.Text;
                            Label24.Text = select_kaki.Rows[0]["stf_name"].ToString();
                            txt_org.Text = select_kaki.Rows[0]["org_name"].ToString();
                            Label29.Text = select_kaki.Rows[0]["op_state_cd"].ToString();
                            
                            s_gred.Text = select_kaki.Rows[0]["hr_gred_desc"].ToString();
                            s_jab.Text = select_kaki.Rows[0]["hr_jaba_desc"].ToString();
                            s_jaw.Text = select_kaki.Rows[0]["hr_jaw_desc"].ToString();
                            dd_s_bank.SelectedValue = select_kaki.Rows[0]["stf_bank_cd"].ToString().Trim();
                            s_bno.Text = select_kaki.Rows[0]["stf_bank_acc_no"].ToString();
                            TextBox2.Text = select_kaki.Rows[0]["op_perg_name"].ToString();

                        }
                        all_grid();
                    }
                    else
                    {
                        all_grid();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Rekod Tidak Dijumpai');", true);
                    }

                }
                else
                {
                    all_grid();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Rekod Tidak Dijumpai');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Sila Masukkan Input Carian');", true);
                all_grid();
            }

        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Rekod Tidak Dijumpai');", true);

        }
    }

    void month()
    {
        DataSet Ds = new DataSet();//24_01
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

    void month1()
    {
        DataSet Ds = new DataSet();
        try
        {
            DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);

            for (int i = 1; i < 13; i++)
            {
                DropDownList2.Items.Add(new ListItem(info.GetMonthName(i), i.ToString("00")));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void Type_klm()
    {
        DataSet Ds = new DataSet();//24_01
        try
        {
            string com = "select typeklm_cd,UPPER(typeklm_desc) as typeklm_desc from Ref_hr_type_klm where Status = 'A' order by typeklm_cd";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList1.DataSource = dt;
            DropDownList1.DataTextField = "typeklm_desc";
            DropDownList1.DataValueField = "typeklm_cd";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void Type_tunggakan()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select * from Ref_hr_tunggakan where Status = 'A' order by hr_tung_code";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList3.DataSource = dt;
            DropDownList3.DataTextField = "hr_tung_desc";
            DropDownList3.DataValueField = "hr_tung_code";
            DropDownList3.DataBind();
            DropDownList3.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void btn_mklm_simp_Click(object sender, EventArgs e)
    {
        if (Kaki_no.Text != "" && DropDownList1.SelectedValue != "" && TextBox3.Text != "")
        {
            //DateTime mnth1 = DateTime.ParseExact(TextBox3.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //string mth = mnth1.ToString("MM");
            //string year = mnth1.ToString("yyyy");
            act_dt = txt_tahu.Text + "-" + DD_PBB.SelectedValue;
            //act_dt = year + "-" + mth;
            string bsal = string.Empty;
            DataTable dd_hrsal = new DataTable();
            dd_hrsal = DBCon.Ora_Execute_table("select * from hr_salary where slr_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt + "') between FORMAT(slr_eff_dt,'yyyy-MM') And FORMAT(slr_end_dt,'yyyy-MM')");

            if (dd_hrsal.Rows.Count != 0)
            {
                if (dd_hrsal.Rows.Count == 2)
                {
                    DataTable dd_hrsal_tot = new DataTable();
                    dd_hrsal_tot = DBCon.Ora_Execute_table("select *,day(slr_eff_dt) d1,day(slr_end_dt) d2,datediff(day, slr_eff_dt, dateadd(month, 1, slr_eff_dt)) as tdays,datediff(day, DATEADD(mm, 1, slr_eff_dt), dateadd(month, 1, DATEADD(mm, 1, slr_eff_dt))) as tdays1,((day(slr_end_dt)) * slr_salary_amt) / datediff(day, DATEADD(mm, 1, slr_eff_dt), dateadd(month, 1, DATEADD(mm, 1, slr_eff_dt))) as a,(((datediff(day, slr_eff_dt, dateadd(month, 1, slr_eff_dt)) - day(slr_eff_dt)) + 1) * slr_salary_amt) / datediff(day, slr_eff_dt, dateadd(month, 1, slr_eff_dt)) as b,((datediff(day, slr_eff_dt, dateadd(month, 1, slr_eff_dt)) - day(slr_eff_dt)) * slr_salary_amt) / datediff(day, slr_eff_dt, dateadd(month, 1, slr_eff_dt)) as bsal from hr_salary where slr_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt + "') between FORMAT(slr_eff_dt,'yyyy-MM') And FORMAT(slr_end_dt,'yyyy-MM')");
                    string aa = (double.Parse(dd_hrsal_tot.Rows[0]["a"].ToString()) + double.Parse(dd_hrsal_tot.Rows[1]["b"].ToString())).ToString("C").Replace("$", "").Replace("RM", "");
                    bsal = aa;
                }
                else
                {
                    DataTable dd_hrsal_1 = new DataTable();
                    dd_hrsal_1 = DBCon.Ora_Execute_table("select slr_salary_amt from hr_salary where slr_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt + "') between FORMAT(slr_eff_dt,'yyyy-MM') And FORMAT(slr_end_dt,'yyyy-MM')");
                    bsal = double.Parse(dd_hrsal_1.Rows[0]["slr_salary_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                }
            }
            else
            {
                bsal = "0.00";
            }

            DataTable dd_hrsal1 = new DataTable();
            dd_hrsal1 = DBCon.Ora_Execute_table("select fx.fxa_staff_no,sum(fx.fxa_allowance_amt) as samt from hr_fixed_allowance as fx where ('" + act_dt + "' between FORMAT(fx.fxa_eff_dt,'yyyy-MM') And FORMAT(fx.fxa_end_dt,'yyyy-MM')) and fx.fxa_staff_no='" + Applcn_no1.Text + "' group by fx.fxa_staff_no");
            //dd_hrsal1 = DBCon.Ora_Execute_table("select fx.fxa_staff_no,sum(fx.fxa_allowance_amt) as samt from hr_fixed_allowance as fx where fx.fxa_staff_no='" + Applcn_no1 + "' group by fx.fxa_staff_no");
            if (dd_hrsal1.Rows.Count != 0)
            {
                decimal vv1 = decimal.Parse(dd_hrsal1.Rows[0]["samt"].ToString());
                et_amt = vv1.ToString();
            }
            else
            {
                et_amt = "0.00";
            }
            string gh_bek = string.Empty;
            DataTable get_hai_bekerja = new DataTable();
            get_hai_bekerja = DBCon.Ora_Execute_table("select * from hr_hari_kerja where hk_year='" + txt_tahu.Text + "' and hk_month='" + DD_PBB.SelectedValue + "'");
            if (get_hai_bekerja.Rows.Count != 0)
            {
                gh_bek = get_hai_bekerja.Rows[0]["hk_day"].ToString();
            }
            else
            {
                gh_bek = "1";
            }

            string strJumlah = string.Empty;
            //double ss1 = Math.Round((double.Parse(TextBox4.Text) / double.Parse(TextBox25.Text)), 2); // 13_05_2019
            double ss1 = double.Parse(TextBox4.Text) / double.Parse(TextBox25.Text);
            //strJumlah = ((Math.Round(double.Parse(bsal) / double.Parse(TextBox20.Text), 2) * (ss1) * double.Parse(TextBox21.Text))).ToString("C").Replace("RM", "").Replace("$", "");// 13_05_2019
            strJumlah = (Math.Round((((double.Parse(bsal) + double.Parse(et_amt)) / double.Parse(TextBox20.Text)) * (ss1)) * double.Parse(TextBox21.Text), 2)).ToString("C").Replace("RM", "").Replace("$", "");



            DateTime today1 = DateTime.ParseExact(TextBox3.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string mkdt = today1.ToString("yyyy-MM-dd");
            DataTable ins_aud = new DataTable();

            DataTable dd_hrsal_ot = new DataTable();
            dd_hrsal_ot = DBCon.Ora_Execute_table("select * from hr_ot where otl_staff_no='" + Applcn_no1.Text + "' and otl_month='" + DD_PBB.SelectedValue + "' and otl_year='" + txt_tahu.Text + "' and otl_work_dt='" + mkdt + "' and otl_ot_type_cd='" + DropDownList1.SelectedValue + "'");
            if (dd_hrsal_ot.Rows.Count == 0)
            {
                string Status1 = string.Empty;
                string Inssql = "insert into hr_ot (otl_staff_no,otl_month,otl_year,otl_work_dt,otl_ot_type_cd,otl_work_hour,otl_ot_amt,otl_ot_sts_cd,otl_crt_id,otl_crt_dt,otl_remark)values('" + Applcn_no1.Text + "','" + DD_PBB.SelectedValue + "','" + txt_tahu.Text + "','" + mkdt + "','" + DropDownList1.SelectedValue + "','" + TextBox4.Text + "','" + strJumlah + "','01','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','"+TextBox22.Text+"')";
                Status = DBCon.Ora_Execute_CommamdText(Inssql);
                if (Status == "SUCCESS")
                {
                    //DataTable sel_staff = new DataTable();
                    //sel_staff = DBCon.Ora_Execute_table("select stf_staff_no,stf_kod_akaun,stf_name from hr_staff_profile where stf_staff_no='" + Applcn_no1.Text + "'");
                    //if (sel_staff.Rows.Count != 0)
                    //{
                    //    DataTable sel_staff_kod = new DataTable();
                    //    sel_staff_kod = DBCon.Ora_Execute_table("select * from KW_Ref_Carta_Akaun where kod_akaun='" + sel_staff.Rows[0]["stf_kod_akaun"].ToString() + "'");
                    //    string Inssql1 = "INSERT INTO KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,kat_akaun,GL_invois_no,ref2,Tax_type,Tot_Amt,GL_process_dt,GL_desc1,GL_desc2,GL_nama_kod,crt_id,cr_dt,GL_sts) VALUES ('12.02.05','" + strJumlah + "','0.00','" + sel_staff_kod.Rows[0]["kat_akaun"].ToString() + "','','" + sel_staff.Rows[0]["stf_name"].ToString() + "','','0.00','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','Kerja Lebih Masa','','" + sel_staff.Rows[0]["stf_kod_akaun"].ToString() + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A')";
                    //    Status1 = DBCon.Ora_Execute_CommamdText(Inssql1);

                    //}
                    DropDownList1.SelectedValue = "";
                    TextBox3.Text = "";
                    TextBox4.Text = "";
                    all_grid();
                    service.audit_trail("P0066", "Kerja Lebih Masa Simpan", "NO KAKITANGAN", Applcn_no1.Text);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Kerja Lebih Masa Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
            }
            else
            {
                all_grid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {

            all_grid();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
        gv_list5();
    }


    protected void btn_tung_simp_Click(object sender, EventArgs e)
    {
        if (Kaki_no.Text != "" && DropDownList2.SelectedValue != "" && TextBox9.Text != "" && TextBox10.Text != "" && DropDownList3.SelectedValue != "")
        {
            DataTable dd_hrsal_ot = new DataTable();
            dd_hrsal_ot = DBCon.Ora_Execute_table("select * from hr_tunggakan where tun_staff_no='" + Applcn_no1.Text + "' and tun_month='" + DropDownList2.SelectedValue + "' and tun_year='" + TextBox9.Text + "' and tun_type_cd='" + DropDownList3.SelectedValue + "'");
            if (dd_hrsal_ot.Rows.Count == 0)
            {
                string Status1 = string.Empty;
                string Inssql = "insert into hr_tunggakan (tun_staff_no,tun_month,tun_year,tun_type_cd,tun_amt,tun_sts_cd,catatan,tun_crt_id,tun_crt_dt,tun_mnth_desc)values('" + Applcn_no1.Text + "','" + DropDownList2.SelectedValue + "','" + TextBox9.Text + "','" + DropDownList3.SelectedValue + "','" + TextBox10.Text + "','A','" + tung_catatan.Value + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + DropDownList2.SelectedItem.Text + "')";
                Status = DBCon.Ora_Execute_CommamdText(Inssql);
                if (Status == "SUCCESS")
                {
                    rst_tunggakan();
                    service.audit_trail("P0066", "Lain-Lain Simpan", "NO KAKITANGAN", Applcn_no1.Text);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Tunggakan Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
            }
            else
            {
                all_grid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {

            all_grid();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
        gv_list7();
    }

    protected void btn_tung_kem_Click(object sender, EventArgs e)
    {
        if (Kaki_no.Text != "" && DropDownList2.SelectedValue != "" && TextBox9.Text != "" && TextBox10.Text != "" && DropDownList3.SelectedValue != "")
        {

            DataTable dd_hrsal_ot = new DataTable();
            dd_hrsal_ot = DBCon.Ora_Execute_table("select * from hr_tunggakan where tun_staff_no='" + Applcn_no1.Text + "' and tun_month='" + DropDownList2.SelectedValue + "' and tun_year='" + TextBox9.Text + "' and tun_type_cd='" + DropDownList3.SelectedValue + "' and Id = '" + TextBox11.Text + "'");
            if (dd_hrsal_ot.Rows.Count != 0)
            {
                string Status1 = string.Empty;
                string Inssql = "Update hr_tunggakan set tun_month='" + DropDownList2.SelectedValue + "',tun_mnth_desc='" + DropDownList2.SelectedItem.Text + "',tun_year='" + TextBox9.Text + "',tun_type_cd='" + DropDownList3.SelectedValue + "',tun_amt='" + TextBox10.Text + "',catatan='" + tung_catatan.Value + "',tun_upd_id='" + Session["New"].ToString() + "',tun_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where tun_staff_no='" + Applcn_no1.Text + "' and tun_month='" + DropDownList2.SelectedValue + "' and tun_year='" + TextBox9.Text + "' and tun_type_cd='" + DropDownList3.SelectedValue + "' and Id = '" + TextBox11.Text + "'";
                Status = DBCon.Ora_Execute_CommamdText(Inssql);
                if (Status == "SUCCESS")
                {
                    rst_tunggakan();
                    service.audit_trail("P0066", "Lain-Lain Kemaskini", "NO KAKITANGAN", Applcn_no1.Text);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
            }
            else
            {
                all_grid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {

            all_grid();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
        gv_list7();
    }

    void rst_tunggakan()
    {
        Button21.Visible = false;
        Button20.Visible = true;
        DropDownList3.SelectedValue = "";
        DropDownList2.SelectedValue = DateTime.Now.ToString("MM");
        TextBox9.Text = DateTime.Now.ToString("yyyy");
        TextBox10.Text = "";
        tung_catatan.Value = "";
        TextBox11.Text = "";
        gv_list7();
        all_grid();
    }
    protected void rset_Click_tung(object sender, EventArgs e)
    {
        rst_tunggakan();
    }

    protected void lnkView_Click_ro(object sender, EventArgs e)
    {
        // act_dt = txt_tahu.Text + "-" + DD_PBB.SelectedValue;
        LinkButton btnButton = sender as LinkButton;
        GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
        System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("crt_dt");

        string abc = lblTitle.Text;

        //DateTime today1 = DateTime.ParseExact(abc, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        string ctdt = abc;
        DataTable ddokdicno = new DataTable();
        ddokdicno = DBCon.Ora_Execute_table("select ot.otl_staff_no,FORMAT(ot.otl_work_dt,'dd/MM/yyyy', 'en-us') as otl_work_dt,ot.otl_work_hour,ot.otl_ot_amt,ot.otl_ot_sts_cd,FORMAT(ot.otl_crt_dt,'yyyy-MM-dd HH:mm:ss', 'en-us') as otl_crt_dt,ot.otl_ot_type_cd,ot.otl_month,ot.otl_year,otl_remark from hr_ot as ot where otl_staff_no = '" + Applcn_no1.Text + "' and otl_crt_dt ='" + ctdt + "'");
        //string stffno = ddokdicno.Rows[0][0].ToString();
        //DD_TUNT.Enabled = false;
        Button6.Visible = false;
        if (gt_val2 == "1")
        {
            Button9.Visible = true;
        }
        else
        {
            Button9.Visible = false;
        }

        TextBox3.Text = ddokdicno.Rows[0]["otl_work_dt"].ToString();
        //TextBox3.Attributes.Add("Style", "Pointer-events:None;");
        //DropDownList1.Attributes.Add("Style", "Pointer-events:None;");
        TextBox14.Text = abc;
        DropDownList1.SelectedValue = ddokdicno.Rows[0]["otl_ot_type_cd"].ToString().Trim();

        TextBox4.Text = ddokdicno.Rows[0]["otl_work_hour"].ToString();

        //DataTable dt = new DataTable();
        //dt = DBCon.Ora_Execute_table("select typeklm_weight,typeklm_Y from   Ref_hr_type_klm where typeklm_desc='" + DropDownList1.SelectedItem.Text + "'");
        //if (dt.Rows.Count > 0)
        //{
        //    if (dt.Rows[0][0].ToString() != "")
        //    {
        //        TextBox21.Text = dt.Rows[0][0].ToString();
        //    }
        //    else
        //    {
        //        TextBox21.Text = "0";
        //    }
        //    TextBox20.Text = dt.Rows[0][1].ToString();
        //}
        get_klm();

        //TextBox21.Text = double.Parse(ddokdicno.Rows[0]["otl_ot_amt"].ToString()).ToString("C").Replace("RM","").Replace("$", "");

        DD_PBB.SelectedValue = ddokdicno.Rows[0]["otl_month"].ToString();
        txt_tahu.Text = ddokdicno.Rows[0]["otl_year"].ToString();
        if (ddokdicno.Rows[0]["otl_remark"].ToString() != "")
        {
            TextBox22.Text = ddokdicno.Rows[0]["otl_remark"].ToString();
        }
        else
        {
            TextBox22.Text = "KLM Bagi "+ DD_PBB.SelectedItem.Text +" "+ txt_tahu.Text;
        }

        all_grid();
        gv_list5();
    }

    protected void lnkView_Click_tung(object sender, EventArgs e)
    {
        // act_dt = txt_tahu.Text + "-" + DD_PBB.SelectedValue;
        LinkButton btnButton = sender as LinkButton;
        GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
        System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("tun_crt_dt");

        string abc = lblTitle.Text;

        //DateTime today1 = DateTime.ParseExact(abc, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        string ctdt = abc;
        DataTable ddokdicno_tun = new DataTable();
        ddokdicno_tun = DBCon.Ora_Execute_table("select * from hr_tunggakan where tun_staff_no = '" + Applcn_no1.Text + "' and Id ='" + ctdt + "'");
        //string stffno = ddokdicno.Rows[0][0].ToString();
        //DD_TUNT.Enabled = false;
        Button20.Visible = false;
        Button21.Visible = true;
        TextBox11.Text = ctdt;
        TextBox9.Text = ddokdicno_tun.Rows[0]["tun_year"].ToString();
        TextBox10.Text = double.Parse(ddokdicno_tun.Rows[0]["tun_amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
        DropDownList3.SelectedValue = ddokdicno_tun.Rows[0]["tun_type_cd"].ToString().Trim();
        DropDownList2.SelectedValue = ddokdicno_tun.Rows[0]["tun_month"].ToString();
        tung_catatan.Value = ddokdicno_tun.Rows[0]["catatan"].ToString();

        all_grid();
        gv_list7();
    }

    protected void rset_Click4(object sender, EventArgs e)
    {
        gv_list5();
        Button9.Visible = false;
        if (gt_val1 == "1")
        {
            Button6.Visible = true;
        }
        else
        {
            Button6.Visible = false;
        }
        TextBox3.Attributes.Remove("Style");
        DropDownList1.Attributes.Remove("Style");

        DropDownList1.SelectedValue = "";
        TextBox3.Text = "";
        TextBox4.Text = "";
        TextBox21.Text = "";
        DD_PBB.SelectedValue = DateTime.Now.ToString("MM");//24_01
        Wday();
        txt_tahu.Text = DateTime.Now.ToString("yyyy");//24_01
        TextBox22.Text = "KLM Bagi " + DD_PBB.SelectedItem.Text + " " + txt_tahu.Text;
        all_grid();
    }

    void Wday()
    {
        //if (DD_PBB.SelectedItem.Text != "")
        //{
        //    DateTime now = DateTime.Now;
        //    int dt = Convert.ToInt32(DD_PBB.SelectedValue);
        //    var startDate = new DateTime(now.Year, dt , 1);
        //    DateTime dt1 = startDate;
        //    string fdate = dt1.ToString("yyyy/MM/dd");
        //    var endDate = startDate.AddMonths(1).AddDays(-1);
        //    DateTime dt2 = endDate;
        //    string tdate = dt2.ToString("yyyy/MM/dd");
        //    DataTable dt3 = new DataTable();
        //    //dt3 = DBCon.Ora_Execute_table("select tot - cnt as wday from (select count(*) cnt,DAY(DATEADD(DD,-1,DATEADD(MM,DATEDIFF(MM,-1,'" + fdate + "'),0))) as tot from hr_holiday where  hol_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fdate + "'), 0) and hol_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tdate + "'), +1) and hol_state_cd='"+ Session["user_state"].ToString() + "') a");

        //    dt3 = DBCon.Ora_Execute_table("select (DAY(DATEADD(DD,-1,DATEADD(MM,DATEDIFF(MM,-1,'" + fdate + "'),0))) - count(*)) as wday from hr_holiday where hol_dt>=DATEADD (day, DATEDIFF(day, 0, '" + fdate + "'), 0) and hol_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tdate + "'), +0) and hol_state_cd='" + Label29.Text + "' and hol_cancel_ind='N'");
        //    if (dt3.Rows.Count > 0)
        //    {
        //        TextBox20.Text = dt3.Rows[0]["wday"].ToString();
        //    }
        //}
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Load DropDownList2
        all_grid();
        gv_list5();
        Wday();
        TextBox22.Text = "KLM Bagi " + DD_PBB.SelectedItem.Text +" " + txt_tahu.Text;
        grid1();
       

    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        get_klm();
        gv_list5();
        all_grid();
    }

    void get_klm()
    {
        DataTable dt = new DataTable();
        dt = DBCon.Ora_Execute_table("select typeklm_weight,typeklm_Y,typeklm_T from   Ref_hr_type_klm where typeklm_desc='" + DropDownList1.SelectedItem.Text + "'");
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0][0].ToString() != "")
            {
                TextBox21.Text = dt.Rows[0][0].ToString();
            }
            else
            {
                TextBox21.Text = "0";
            }
            TextBox20.Text = dt.Rows[0][1].ToString();
            TextBox25.Text = dt.Rows[0][2].ToString();
        }
    }

    protected void btn_mklm_kem_Click(object sender, EventArgs e)
    {
        if (Kaki_no.Text != "" && DropDownList1.SelectedValue != "" && TextBox3.Text != "")
        {
            //DateTime mnth1 = DateTime.ParseExact(TextBox3.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //string mth = mnth1.ToString("MM");
            //string year = mnth1.ToString("yyyy");
            //act_dt = year + "-" + mth;
            act_dt = txt_tahu.Text + "-" + DD_PBB.SelectedValue;
            string bsal = string.Empty;
            DataTable dd_hrsal = new DataTable();
            dd_hrsal = DBCon.Ora_Execute_table("select * from hr_salary where slr_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt + "') between FORMAT(slr_eff_dt,'yyyy-MM') And FORMAT(slr_end_dt,'yyyy-MM')");

            if (dd_hrsal.Rows.Count != 0)
            {
                if (dd_hrsal.Rows.Count == 2)
                {
                    DataTable dd_hrsal_tot = new DataTable();
                    dd_hrsal_tot = DBCon.Ora_Execute_table("select *,day(slr_eff_dt) d1,day(slr_end_dt) d2,datediff(day, slr_eff_dt, dateadd(month, 1, slr_eff_dt)) as tdays,datediff(day, DATEADD(mm, 1, slr_eff_dt), dateadd(month, 1, DATEADD(mm, 1, slr_eff_dt))) as tdays1,((day(slr_end_dt)) * slr_salary_amt) / datediff(day, DATEADD(mm, 1, slr_eff_dt), dateadd(month, 1, DATEADD(mm, 1, slr_eff_dt))) as a,(((datediff(day, slr_eff_dt, dateadd(month, 1, slr_eff_dt)) - day(slr_eff_dt)) + 1) * slr_salary_amt) / datediff(day, slr_eff_dt, dateadd(month, 1, slr_eff_dt)) as b,((datediff(day, slr_eff_dt, dateadd(month, 1, slr_eff_dt)) - day(slr_eff_dt)) * slr_salary_amt) / datediff(day, slr_eff_dt, dateadd(month, 1, slr_eff_dt)) as bsal from hr_salary where slr_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt + "') between FORMAT(slr_eff_dt,'yyyy-MM') And FORMAT(slr_end_dt,'yyyy-MM')");
                    string aa = (double.Parse(dd_hrsal_tot.Rows[0]["a"].ToString()) + double.Parse(dd_hrsal_tot.Rows[1]["b"].ToString())).ToString("C").Replace("$", "").Replace("RM", "");
                    bsal = aa;
                }
                else
                {
                    DataTable dd_hrsal_1 = new DataTable();
                    dd_hrsal_1 = DBCon.Ora_Execute_table("select slr_salary_amt from hr_salary where slr_staff_no='" + Applcn_no1.Text + "' and ('" + act_dt + "') between FORMAT(slr_eff_dt,'yyyy-MM') And FORMAT(slr_end_dt,'yyyy-MM')");
                    bsal = double.Parse(dd_hrsal_1.Rows[0]["slr_salary_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                }
            }
            else
            {
                bsal = "0.00";
            }

            DataTable dd_hrsal1 = new DataTable();
            dd_hrsal1 = DBCon.Ora_Execute_table("select fx.fxa_staff_no,sum(fx.fxa_allowance_amt) as samt from hr_fixed_allowance as fx where ('" + act_dt + "' between FORMAT(fx.fxa_eff_dt,'yyyy-MM') And FORMAT(fx.fxa_end_dt,'yyyy-MM')) and fx.fxa_staff_no='" + Applcn_no1.Text + "' group by fx.fxa_staff_no");
            //dd_hrsal1 = DBCon.Ora_Execute_table("select fx.fxa_staff_no,sum(fx.fxa_allowance_amt) as samt from hr_fixed_allowance as fx where fx.fxa_staff_no='" + Applcn_no1 + "' group by fx.fxa_staff_no");
            if (dd_hrsal1.Rows.Count != 0)
            {
                decimal vv1 = decimal.Parse(dd_hrsal1.Rows[0]["samt"].ToString());
                et_amt = vv1.ToString();
            }
            else
            {
                et_amt = "0.00";
            }
            string strJumlah = string.Empty;

            string gh_bek = string.Empty;
            DataTable get_hai_bekerja = new DataTable();
            get_hai_bekerja = DBCon.Ora_Execute_table("select * from hr_hari_kerja where hk_year='" + txt_tahu.Text + "' and hk_month='" + DD_PBB.SelectedValue + "'");
            if (get_hai_bekerja.Rows.Count != 0)
            {
                gh_bek = get_hai_bekerja.Rows[0]["hk_day"].ToString();
            }
            else
            {
                gh_bek = "1";
            }

            
            double ss1 = double.Parse(TextBox4.Text) / double.Parse(TextBox25.Text);
            //strJumlah = ((Math.Round(double.Parse(bsal) / double.Parse(TextBox20.Text), 2) * (ss1) * double.Parse(TextBox21.Text))).ToString("C").Replace("RM", "").Replace("$", "");
            strJumlah = (Math.Round((((double.Parse(bsal) + double.Parse(et_amt)) / double.Parse(TextBox20.Text)) * (ss1)) * double.Parse(TextBox21.Text), 2)).ToString("C").Replace("RM", "").Replace("$", "");



            //strJumlah = (double.Parse(bsal) / (double.Parse(TextBox20.Text) * (ss1) * double.Parse(TextBox21.Text))).ToString("C").Replace("RM", "").Replace("$", "");

            string ctdt = TextBox14.Text;

            DateTime today2 = DateTime.ParseExact(TextBox3.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string mkdt = today2.ToString("yyyy-MM-dd");

            DataTable dd_hrsal_ot = new DataTable();
            dd_hrsal_ot = DBCon.Ora_Execute_table("select count(*) as cnt from hr_ot where otl_staff_no='" + Applcn_no1.Text + "' and otl_month='" + DD_PBB.SelectedValue + "' and otl_year='" + txt_tahu.Text + "' and otl_work_dt='" + mkdt + "' and otl_ot_type_cd='" + DropDownList1.SelectedValue + "'");
            if (dd_hrsal_ot.Rows[0]["cnt"].ToString() == "1")
            {
                DataTable ins_aud = new DataTable();
                DBCon.Execute_CommamdText("Update hr_ot SET otl_remark='"+ TextBox22.Text +"',otl_ot_type_cd='" + DropDownList1.SelectedValue + "',otl_month='" + DD_PBB.SelectedValue + "',otl_year='" + txt_tahu.Text + "',otl_work_hour='" + TextBox4.Text + "',otl_ot_amt='" + strJumlah + "',otl_upd_id='" + Session["New"].ToString() + "',otl_upd_dt='" + DateTime.Now + "' where otl_staff_no='" + Applcn_no1.Text + "' and otl_crt_dt='" + ctdt + "'");


                Button9.Visible = false;
                if (gt_val1 == "1")
                {
                    Button6.Visible = true;
                }
                else
                {
                    Button6.Visible = false;
                }
                TextBox3.Attributes.Remove("Style");
                DropDownList1.Attributes.Remove("Style");

                DropDownList1.SelectedValue = "";
                TextBox3.Text = "";
                TextBox4.Text = "";
                all_grid();
                service.audit_trail("P0066", "Kerja Lebih Masa Kemaskini", "NO KAKITANGAN", Applcn_no1.Text);
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Kerja Lebih Masa Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
            else
            {
                all_grid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Sudah Wujud.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {
            all_grid();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
        gv_list5();
    }

    protected void btn_mklm_hapus_Click(object sender, EventArgs e)
    {
        string rcount = string.Empty;
        int count = 0;
        foreach (GridViewRow gvrow in GridView5.Rows)
        {
            var rb = gvrow.FindControl("RadioButton5") as System.Web.UI.WebControls.CheckBox;
            if (rb.Checked == true)
            {
                count++;
            }
            rcount = count.ToString();
        }
        if (rcount != "0")
        {
            foreach (GridViewRow row in GridView5.Rows)
            {
                System.Web.UI.WebControls.CheckBox rbn = new System.Web.UI.WebControls.CheckBox();
                rbn = (System.Web.UI.WebControls.CheckBox)row.FindControl("RadioButton5");
                if (rbn.Checked == true)
                {
                    int RowIndex = row.RowIndex;
                    DataTable ddicno = new DataTable();
                    string varName1 = ((System.Web.UI.WebControls.Label)row.FindControl("crt_dt")).Text.ToString(); //this store the  value in varName1
                    //DateTime today1 = DateTime.ParseExact(varName1, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    //string ctdt = today1.ToString("yyyy-MM-dd HH:mm:ss");
                    SqlCommand ins_peng = new SqlCommand("delete from hr_ot where otl_crt_dt='" + varName1 + "' and otl_staff_no='" + Applcn_no1.Text + "'", con);
                    con.Open();
                    int i = ins_peng.ExecuteNonQuery();
                    con.Close();
                    service.audit_trail("P0066", "Kerja Lebih Masa Hapus", "NO KAKITANGAN", Applcn_no1.Text);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Kerja Lebih Masa Berjaya Dihapuskan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    //Ins_aud();
                    Button6.Visible = true;
                    Button9.Visible = false;
                    TextBox3.Attributes.Remove("Style");
                    DropDownList1.Attributes.Remove("Style");
                    all_grid();
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
        gv_list5();
    }

    protected void btn_tung_hapus_Click(object sender, EventArgs e)
    {
        string rcount = string.Empty;
        int count = 0;
        foreach (GridViewRow gvrow in GridView6.Rows)
        {
            var rb = gvrow.FindControl("RadioButton5") as System.Web.UI.WebControls.CheckBox;
            if (rb.Checked == true)
            {
                count++;
            }
            rcount = count.ToString();
        }
        if (rcount != "0")
        {
            foreach (GridViewRow row in GridView6.Rows)
            {
                System.Web.UI.WebControls.CheckBox rbn = new System.Web.UI.WebControls.CheckBox();
                rbn = (System.Web.UI.WebControls.CheckBox)row.FindControl("RadioButton5");
                if (rbn.Checked == true)
                {
                    int RowIndex = row.RowIndex;
                    DataTable ddicno = new DataTable();
                    string varName1 = ((System.Web.UI.WebControls.Label)row.FindControl("tun_crt_dt")).Text.ToString(); //this store the  value in varName1
                    //DateTime today1 = DateTime.ParseExact(varName1, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    //string ctdt = today1.ToString("yyyy-MM-dd HH:mm:ss");
                    SqlCommand ins_peng = new SqlCommand("delete from hr_tunggakan where Id='" + varName1 + "' and tun_staff_no='" + Applcn_no1.Text + "'", con);
                    con.Open();
                    int i = ins_peng.ExecuteNonQuery();
                    con.Close();
                    rst_tunggakan();
                    service.audit_trail("P0066", "Lain-Lain Hapus", "NO KAKITANGAN", Applcn_no1.Text);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Tunggakan Dihapuskan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
        gv_list7();
    }

    protected void simp_Click_bns(object sender, EventArgs e)
    {
        try
        {

            if (Kaki_no.Text != "" && TextBox1.Text != "" && TextBox8.Text != "")
            {
                DateTime today2 = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                phdate2 = today2.ToString("yyyy-MM-dd");

                DateTime today3 = DateTime.ParseExact(TextBox8.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string end_dt = today3.ToString("yyyy-MM-dd");

                if ((today3 - today2).TotalDays >= 0)
                {



                    DataTable ddokdicno2 = new DataTable();
                    ddokdicno2 = DBCon.Ora_Execute_table("select * from hr_Bonus where bns_staff_no='" + Applcn_no1.Text + "' and bns_eff_dt='" + phdate2 + "' and bns_end_dt='" + end_dt + "'");
                    string Status1 = string.Empty;
                    if (ddokdicno2.Rows.Count == 0)
                    {
                        //DBCon.Ora_Execute_table("INSERT INTO hr_Bonus (bns_staff_no,bns_eff_dt,bns_end_dt,bns_amt,bns_kpi_amt,bns_crt_id,bns_crt_dt) VALUES ('" + Applcn_no1.Text + "','" + phdate2 + "','" + end_dt + "','" + TextBox5.Text + "','" + TextBox7.Text + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");

                        string Inssql = "INSERT INTO hr_Bonus (bns_staff_no,bns_eff_dt,bns_end_dt,bns_amt,bns_kpi_amt,bns_crt_id,bns_crt_dt) VALUES ('" + Applcn_no1.Text + "','" + phdate2 + "','" + end_dt + "','" + TextBox5.Text + "','" + TextBox7.Text + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql);

                        if (Status == "SUCCESS")
                        {
                            //DataTable sel_staff = new DataTable();
                            //sel_staff = DBCon.Ora_Execute_table("select stf_staff_no,stf_kod_akaun,stf_name from hr_staff_profile where stf_staff_no='" + Applcn_no1.Text + "'");
                            //if (sel_staff.Rows.Count != 0)
                            //{
                            //    DataTable sel_staff_kod = new DataTable();
                            //    sel_staff_kod = DBCon.Ora_Execute_table("select * from KW_Ref_Carta_Akaun where kod_akaun='" + sel_staff.Rows[0]["stf_kod_akaun"].ToString() + "'");

                            //    decimal cmp_amt = (Convert.ToDecimal(TextBox5.Text) + Convert.ToDecimal(TextBox7.Text));
                            //    string Inssql1 = "INSERT INTO KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,kat_akaun,GL_invois_no,ref2,Tax_type,Tot_Amt,GL_process_dt,GL_desc1,GL_desc2,GL_nama_kod,crt_id,cr_dt,GL_sts) VALUES ('12.02.06','" + cmp_amt + "','0.00','" + sel_staff_kod.Rows[0]["kat_akaun"].ToString() + "','','" + sel_staff.Rows[0]["stf_name"].ToString() + "','','0.00','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','BONUS','','" + sel_staff.Rows[0]["stf_kod_akaun"].ToString() + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A')";
                            //    Status1 = DBCon.Ora_Execute_CommamdText(Inssql1);

                            //}
                            DataTable sel_phis1 = new DataTable();
                            sel_phis1 = DBCon.Ora_Execute_table("select top(1) FORMAT(bns_eff_dt,'yyyy-MM-dd', 'en-us') as s1 from hr_Bonus where bns_staff_no='" + Applcn_no1.Text + "' and bns_eff_dt!='" + phdate2 + "' order by bns_eff_dt desc");

                            if (sel_phis1.Rows.Count != 0)
                            {
                                DateTime time1_pos = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                string pdt1 = time1_pos.AddDays(-1).ToString("yyyy-MM-dd");

                                string upssql1 = "update hr_Bonus set bns_end_dt='" + pdt1 + "',bns_upd_id ='" + Session["New"].ToString() + "',bns_upd_dt ='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where bns_staff_no ='" + Applcn_no1.Text + "' and bns_eff_dt ='" + sel_phis1.Rows[0]["s1"].ToString() + "'";
                                Status = DBCon.Ora_Execute_CommamdText(upssql1);

                            }

                            Button12.Visible = true;
                            Button13.Visible = false;
                            TextBox1.Text = "";
                            TextBox8.Text = "31/12/9999";
                            TextBox5.Text = "";
                            TextBox7.Text = "";
                            service.audit_trail("P0066", "Bonus Simpan", "NO KAKITANGAN", Applcn_no1.Text);
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Bonus Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                        }
                        all_grid();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Bonus Yang Telah Sedia ada Berjaya.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    all_grid();
                    TextBox8.Text = "31/12/9999";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Tarikh Tamat Tidak Sah.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                all_grid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            all_grid();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Disimpan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
        gv_list6();
    }

    protected void lnkView_Click_bonus(object sender, EventArgs e)
    {
        // act_dt = txt_tahu.Text + "-" + DD_PBB.SelectedValue;
        LinkButton btnButton = sender as LinkButton;
        GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
        System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("lb_52");

        string abc = lblTitle.Text;

        //DateTime today1 = DateTime.ParseExact(abc, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        string ctdt = abc;
        DataTable ddokdicno = new DataTable();
        ddokdicno = DBCon.Ora_Execute_table("select bns_staff_no,FORMAT(bns_eff_dt,'dd/MM/yyyy', 'en-us') as bns_eff_dt,FORMAT(bns_end_dt,'dd/MM/yyyy', 'en-us') as bns_end_dt,bns_amt,bns_kpi_amt from hr_Bonus as ot where bns_staff_no = '" + Applcn_no1.Text + "' and Id ='" + abc + "'");
        //string stffno = ddokdicno.Rows[0][0].ToString();
        //DD_TUNT.Enabled = false;
        Button12.Visible = false;
        if (gt_val2 == "1")
        {
            Button13.Visible = true;
        }
        else
        {
            Button13.Visible = false;
        }
        TextBox7.Text = double.Parse(ddokdicno.Rows[0]["bns_kpi_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "").Replace(",", "");
        TextBox5.Text = double.Parse(ddokdicno.Rows[0]["bns_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "").Replace(",", "");
        TextBox6.Text = abc;
        TextBox1.Text = ddokdicno.Rows[0]["bns_eff_dt"].ToString();
        TextBox8.Text = ddokdicno.Rows[0]["bns_end_dt"].ToString();
        all_grid();
        gv_list6();
    }

    protected void rset_Click5(object sender, EventArgs e)
    {
        gv_list6();
        Button13.Visible = false;
        if (gt_val1 == "1")
        {
            Button12.Visible = true;
        }
        else
        {
            Button12.Visible = false;
        }
        TextBox1.Text = "";
        TextBox8.Text = "31/12/9999";
        TextBox5.Text = "";
        TextBox7.Text = "";
        all_grid();
    }
    protected void kem_Click_bns(object sender, EventArgs e)
    {
        try
        {
            if (Kaki_no.Text != "" && TextBox1.Text != "")
            {
                string d2 = TextBox1.Text;
                DateTime today2 = DateTime.ParseExact(d2, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                phdate2 = today2.ToString("yyyy-MM-dd");

                string d3 = TextBox8.Text;
                DateTime today3 = DateTime.ParseExact(d3, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string end_dt = today3.ToString("yyyy-MM-dd");
                if ((today3 - today2).TotalDays > 0)
                {

                    DataTable ddokdicno2 = new DataTable();
                    ddokdicno2 = DBCon.Ora_Execute_table("select * from hr_Bonus where bns_staff_no='" + Applcn_no1.Text + "' and bns_eff_dt='" + phdate2 + "' and bns_end_dt='" + end_dt + "' and Id != '" + TextBox6.Text + "'");

                    if (ddokdicno2.Rows.Count == 0)
                    {
                        DBCon.Ora_Execute_table("UPDATE hr_Bonus set bns_eff_dt='" + phdate2 + "',bns_end_dt='" + end_dt + "',bns_amt='" + TextBox5.Text + "',bns_kpi_amt='" + TextBox7.Text + "',bns_upd_id='" + Session["New"].ToString() + "',bns_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Id = '" + TextBox6.Text + "'");

                        Button13.Visible = false;
                        if (gt_val1 == "1")
                        {
                            Button12.Visible = true;
                        }
                        else
                        {
                            Button12.Visible = false;
                        }
                        TextBox1.Text = "";
                        TextBox8.Text = "31/12/9999";
                        TextBox5.Text = "";
                        TextBox7.Text = "";
                        service.audit_trail("P0066", "Bonus Kemaskini", "NO KAKITANGAN", Applcn_no1.Text);
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Bonus Berjaya Dikemeskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                        all_grid();
                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Bonus Yang Telah Sedia ada Berjaya.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    all_grid();
                    TextBox8.Text = "31/12/9999";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Tarikh Tamat Tidak Sah.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                all_grid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            all_grid();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Disimpan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
        gv_list6();
    }

    protected void hapus_Click_bns(object sender, EventArgs e)
    {

        string rcount = string.Empty;
        int count = 0;
        foreach (GridViewRow gvrow in GridView4.Rows)
        {
            var rb = gvrow.FindControl("rd_bonus") as System.Web.UI.WebControls.CheckBox;
            if (rb.Checked == true)
            {
                count++;
            }
            rcount = count.ToString();
        }
        if (rcount != "0")
        {
            foreach (GridViewRow row in GridView4.Rows)
            {
                System.Web.UI.WebControls.CheckBox rbn = new System.Web.UI.WebControls.CheckBox();
                rbn = (System.Web.UI.WebControls.CheckBox)row.FindControl("rd_bonus");
                if (rbn.Checked == true)
                {
                    int RowIndex = row.RowIndex;
                    DataTable ddicno = new DataTable();
                    string varName1 = ((System.Web.UI.WebControls.Label)row.FindControl("lb_52")).Text.ToString(); //this store the  value in varName1

                    SqlCommand ins_peng = new SqlCommand("delete from hr_Bonus where Id='" + varName1 + "' and bns_staff_no='" + Applcn_no1.Text + "'", con);
                    con.Open();
                    int i = ins_peng.ExecuteNonQuery();
                    con.Close();
                    service.audit_trail("P0066", "Bonus Hapus", "NO KAKITANGAN", Applcn_no1.Text);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Bonus Berjaya Dihapuskan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    //Ins_aud();
                    Button12.Visible = true;
                    Button13.Visible = false;
                    all_grid();
                }
            }
        }
        else
        {
            all_grid();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
        gv_list6();
    }

    void grid5()
    {
        string sq1 = string.Empty;//24_01
        if (Kaki_no.Text != "")
        {
            sq1 = "select ot.otl_staff_no,FORMAT(ot.otl_work_dt,'dd/MM/yyyy', 'en-us') as otl_work_dt,tk.typeklm_desc,ot.otl_work_hour,ot.otl_ot_amt,case when ot.otl_ot_sts_cd = '01' then 'BARU' else 'TELAH DIPROSES' end as stsname,FORMAT(ot.otl_crt_dt,'yyyy-MM-dd  HH:mm:ss', 'en-us') as otl_crt_dt,otl_remark from hr_ot as ot left join Ref_hr_type_klm as tk on tk.typeklm_cd=ot.otl_ot_type_cd where otl_staff_no='" + Applcn_no1.Text + "'";
        }
        else
        {
            sq1 = "select ot.otl_staff_no,FORMAT(ot.otl_work_dt,'dd/MM/yyyy', 'en-us') as otl_work_dt,tk.typeklm_desc,ot.otl_work_hour,ot.otl_ot_amt,case when ot.otl_ot_sts_cd = '01' then 'BARU' else 'TELAH DIPROSES' end as stsname,FORMAT(ot.otl_crt_dt,'yyyy-MM-dd  HH:mm:ss', 'en-us') as otl_crt_dt,otl_remark from hr_ot as ot left join Ref_hr_type_klm as tk on tk.typeklm_cd=ot.otl_ot_type_cd where otl_staff_no=''";
        }

        con.Open();
        SqlCommand cmd = new SqlCommand("" + sq1 + "", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView5.DataSource = ds;
            GridView5.DataBind();
            int columncount = GridView5.Rows[0].Cells.Count;
            GridView5.Rows[0].Cells.Clear();
            GridView5.Rows[0].Cells.Add(new TableCell());
            GridView5.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView5.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
            TextBox16.Text = "0.00";
        }
        else
        {
            GridView5.DataSource = ds;
            GridView5.DataBind();


        }
        con.Close();//24_01
      //  cumulative();
       // TextBox20.Text = tot_jenis.ToString();
    }

    void grid6()
    {
        string sq2 = string.Empty;
        if (Kaki_no.Text != "")
        {
            sq2 = "select Id,bns_staff_no,FORMAT(bns_eff_dt,'dd/MM/yyyy', 'en-us') as bns_eff_dt,FORMAT(bns_end_dt,'dd/MM/yyyy', 'en-us') as bns_end_dt,bns_amt,bns_kpi_amt,FORMAT(bns_crt_dt,'yyyy-MM-dd HH:mm:ss', 'en-us') as bns_crt_dt from hr_Bonus where bns_staff_no='" + Applcn_no1.Text + "'";
        }
        else
        {
            sq2 = "select Id,bns_staff_no,FORMAT(bns_eff_dt,'dd/MM/yyyy', 'en-us') as bns_eff_dt,FORMAT(bns_end_dt,'dd/MM/yyyy', 'en-us') as bns_end_dt,bns_amt,bns_kpi_amt,FORMAT(bns_crt_dt,'yyyy-MM-dd HH:mm:ss', 'en-us') as bns_crt_dt from hr_Bonus where bns_staff_no='' ";
        }

        con.Open();
        SqlCommand cmd = new SqlCommand("" + sq2 + "", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView4.DataSource = ds;
            GridView4.DataBind();
            int columncount = GridView4.Rows[0].Cells.Count;
            GridView4.Rows[0].Cells.Clear();
            GridView4.Rows[0].Cells.Add(new TableCell());
            GridView4.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView4.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";

        }
        else
        {
            GridView4.DataSource = ds;
            GridView4.DataBind();


        }
        con.Close();
        //cumulative();
        //TextBox20.Text = tot_jenis.ToString();
    }


    void grid7()
    {
        string sq1 = string.Empty;
        if (Kaki_no.Text != "")
        {
            sq1 = "select *,hr_tung_desc from hr_tunggakan left join Ref_hr_tunggakan on hr_tung_Code=tun_type_cd   where tun_staff_no='" + Applcn_no1.Text + "'";
        }
        else
        {
            sq1 = "select *,hr_tung_desc from hr_tunggakan left join Ref_hr_tunggakan on hr_tung_Code=tun_type_cd   where tun_staff_no=''";
        }

        con.Open();
        SqlCommand cmd = new SqlCommand("" + sq1 + "", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView6.DataSource = ds;
            GridView6.DataBind();
            int columncount = GridView6.Rows[0].Cells.Count;
            GridView6.Rows[0].Cells.Clear();
            GridView6.Rows[0].Cells.Add(new TableCell());
            GridView6.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView6.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
        }
        else
        {
            GridView6.DataSource = ds;
            GridView6.DataBind();


        }
        con.Close();
    }


    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetCompletionList(string prefixText, int count)
    {

        using (SqlConnection con = new SqlConnection())
        {

            con.ConnectionString = ConfigurationManager.ConnectionStrings["KoopConnectionString"].ConnectionString;
            DBConnection dbcon1 = new DBConnection();
            DataTable qry1 = new DataTable();
            qry1 = dbcon1.Ora_Execute_table("select stf_staff_no from hr_staff_profile where stf_staff_no LIKE '%" + prefixText + "%'");
            if (qry1.Rows.Count != 0)
            {
                using (SqlCommand com = new SqlCommand())
                {
                    com.CommandText = "select stf_staff_no from hr_staff_profile where stf_staff_no LIKE '%' + @Search + '%'";
                    com.Parameters.AddWithValue("@Search", prefixText);
                    com.Connection = con;
                    con.Open();
                    List<string> countryNames = new List<string>();
                    using (SqlDataReader sdr = com.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            countryNames.Add(sdr["stf_staff_no"].ToString());

                        }
                    }

                    con.Close();
                    return countryNames;
                }
            }
            else
            {
                using (SqlCommand com = new SqlCommand())
                {
                    com.CommandText = "select stf_staff_no,stf_name from hr_staff_profile where stf_name LIKE '%' + @Search + '%'";
                    com.Parameters.AddWithValue("@Search", prefixText);
                    com.Connection = con;
                    con.Open();
                    List<string> countryNames = new List<string>();
                    using (SqlDataReader sdr = com.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            countryNames.Add(sdr["stf_name"].ToString());

                        }
                    }

                    con.Close();
                    return countryNames;
                }
            }
        }
    }


    void BankBind()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select Bank_Code, UPPER(Bank_Name) as Bank_Name from Ref_Nama_Bank  WHERE Status = 'A' order by Bank_Name";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_s_bank.DataSource = dt;
            dd_s_bank.DataTextField = "Bank_Name";
            dd_s_bank.DataValueField = "Bank_Code";
            dd_s_bank.DataBind();
            dd_s_bank.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    void SebabBind()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select seb_cd, UPPER(seb_desc) as seb_desc from Ref_hr_sebab  WHERE Status = 'A' order by seb_cd";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_sebab.DataSource = dt;
            dd_sebab.DataTextField = "seb_desc";
            dd_sebab.DataValueField = "seb_cd";
            dd_sebab.DataBind();
            dd_sebab.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void JelaunBind()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select hr_elau_Code, UPPER(hr_elau_desc) as hr_elau_desc from Ref_hr_jenis_elaun  WHERE Status = 'A' order by hr_elau_desc";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ET_jelaun.DataSource = dt;
            ET_jelaun.DataTextField = "hr_elau_desc";
            ET_jelaun.DataValueField = "hr_elau_Code";
            ET_jelaun.DataBind();
            ET_jelaun.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            LL_jelaun.DataSource = dt;
            LL_jelaun.DataTextField = "hr_elau_desc";
            LL_jelaun.DataValueField = "hr_elau_Code";
            LL_jelaun.DataBind();
            LL_jelaun.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    void all_grid()
    {
        grid_1();
        grid_2();
        grid_3();
        grid5();
        grid6();
        grid7();
    }


    protected void insert_bk1(object sender, EventArgs e)
    {
        if (Kaki_no.Text != "")
        {
            DataTable dd_stf = new DataTable();
            dd_stf = DBCon.Ora_Execute_table("select * from hr_staff_profile where stf_staff_no='" + Applcn_no1.Text + "'");
            if (dd_stf.Rows.Count > 0)
            {
                DBCon.Ora_Execute_table("update hr_staff_profile set stf_bank_acc_no='" + s_bno.Text + "',stf_bank_cd='" + dd_s_bank.SelectedValue + "',stf_upd_id='" + Session["New"].ToString() + "',stf_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where stf_staff_no='" + Applcn_no1.Text + "'");
                service.audit_trail("P0066", "Bank Simpan", "NO KAKITANGAN", Applcn_no1.Text);
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Bank Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                all_grid();
                gv_list1();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                all_grid();
                gv_list1();
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan No Kakitangan / Nama Kakitangan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            all_grid();
            gv_list1();

        }
    }

    void gv_list1()
    {
        p1.Attributes.Add("class", "tab-pane");
        p2.Attributes.Add("class", "tab-pane");
        p3.Attributes.Add("class", "tab-pane");
        p4.Attributes.Add("class", "tab-pane");//24_01
        p5.Attributes.Add("class", "tab-pane");
        p6.Attributes.Add("class", "tab-pane active");

        pp1.Attributes.Remove("class");
        pp2.Attributes.Remove("class");
        pp3.Attributes.Remove("class");
        pp4.Attributes.Remove("class");//24_01
        pp5.Attributes.Remove("class");
        pp6.Attributes.Add("class", "active");
    }

    void gv_list2()
    {
        p1.Attributes.Add("class", "tab-pane active");
        p2.Attributes.Add("class", "tab-pane");
        p3.Attributes.Add("class", "tab-pane");
        p4.Attributes.Add("class", "tab-pane");//24_01
        p5.Attributes.Add("class", "tab-pane");
        p6.Attributes.Add("class", "tab-pane");

        pp6.Attributes.Remove("class");
        pp2.Attributes.Remove("class");
        pp3.Attributes.Remove("class");
        pp4.Attributes.Remove("class");//24_01
        pp5.Attributes.Remove("class");
        pp1.Attributes.Add("class", "active");
    }

    void gv_list3()
    {
        p1.Attributes.Add("class", "tab-pane");
        p2.Attributes.Add("class", "tab-pane active");
        p3.Attributes.Add("class", "tab-pane");
        p4.Attributes.Add("class", "tab-pane");//24_01
        p5.Attributes.Add("class", "tab-pane");
        p6.Attributes.Add("class", "tab-pane");

        pp6.Attributes.Remove("class");
        pp1.Attributes.Remove("class");
        pp3.Attributes.Remove("class");
        pp4.Attributes.Remove("class");//24_01
        pp5.Attributes.Remove("class");
        pp2.Attributes.Add("class", "active");
    }

    void gv_list4()
    {
        p1.Attributes.Add("class", "tab-pane");
        p2.Attributes.Add("class", "tab-pane");
        p3.Attributes.Add("class", "tab-pane active");
        p4.Attributes.Add("class", "tab-pane");//24_01
        p5.Attributes.Add("class", "tab-pane");
        p6.Attributes.Add("class", "tab-pane");

        pp6.Attributes.Remove("class");
        pp1.Attributes.Remove("class");
        pp2.Attributes.Remove("class");
        pp4.Attributes.Remove("class");//24_01
        pp5.Attributes.Remove("class");
        pp3.Attributes.Add("class", "active");
    }

    void gv_list5()
    {
        p1.Attributes.Add("class", "tab-pane");
        p2.Attributes.Add("class", "tab-pane");
        p3.Attributes.Add("class", "tab-pane");
        p4.Attributes.Add("class", "tab-pane active");//24_01
        p5.Attributes.Add("class", "tab-pane");
        p6.Attributes.Add("class", "tab-pane");

        pp6.Attributes.Remove("class");
        pp1.Attributes.Remove("class");
        pp2.Attributes.Remove("class");
        pp3.Attributes.Remove("class");
        pp5.Attributes.Remove("class");
        pp4.Attributes.Add("class", "active");//24_01
    }

    void gv_list6()
    {
        p1.Attributes.Add("class", "tab-pane");
        p2.Attributes.Add("class", "tab-pane");
        p3.Attributes.Add("class", "tab-pane");
        p4.Attributes.Add("class", "tab-pane");//24_01
        p5.Attributes.Add("class", "tab-pane active");
        p6.Attributes.Add("class", "tab-pane");

        pp6.Attributes.Remove("class");
        pp1.Attributes.Remove("class");
        pp2.Attributes.Remove("class");
        pp3.Attributes.Remove("class");
        pp4.Attributes.Remove("class");//24_01
        pp5.Attributes.Add("class", "active");
    }

    void gv_list7()
    {
        p1.Attributes.Add("class", "tab-pane");
        p2.Attributes.Add("class", "tab-pane");
        p3.Attributes.Add("class", "tab-pane");
        p4.Attributes.Add("class", "tab-pane");//24_01
        p5.Attributes.Add("class", "tab-pane");
        p7.Attributes.Add("class", "tab-pane active");
        p6.Attributes.Add("class", "tab-pane");

        pp6.Attributes.Remove("class");
        pp1.Attributes.Remove("class");
        pp2.Attributes.Remove("class");
        pp3.Attributes.Remove("class");
        pp4.Attributes.Remove("class");//24_01
        pp5.Attributes.Remove("class");
        pp7.Attributes.Add("class", "active");
    }

    protected void rset_Click1(object sender, EventArgs e)
    {
        gv_list2();
        GP_date.Text = "";
        GE_date.Text = "31/12/9999";
        GP_amaun.Text = "";
        dd_sebab.SelectedValue = "";
        if (gt_val1 == "1")
        {
            Button5.Visible = true;
            Button5.Text = "Simpan";
        }
        else
        {
            Button5.Visible = false;
        }
        GP_date.Attributes.Remove("Disabled");
        all_grid();
        GP_rno.Text = "0";

    }
    protected void insert_Click1(object sender, EventArgs e)
    {
        try
        {

            if (Kaki_no.Text != "" && GP_date.Text != "")
            {

                if (GP_date.Text != "" && GE_date.Text != "")
                {
                    string d1 = GP_date.Text;
                    DateTime today1 = DateTime.ParseExact(d1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    phdate1 = today1.ToString("yyyy-MM-dd");

                    //if (GE_date.Text != "")
                    //{
                    string d2 = GE_date.Text;
                    DateTime today2 = DateTime.ParseExact(d2, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    phdate2 = today2.ToString("yyyy-MM-dd");
                    //}
                    //else
                    //{
                    //    phdate2 = "";
                    //}

                    if ((today2 - today1).TotalDays >= 0)
                    {


                        if (GP_rno.Text == "0")
                        {
                            DataTable ddokdicno2 = new DataTable();
                            ddokdicno2 = DBCon.Ora_Execute_table("select * from hr_salary where slr_staff_no='" + Applcn_no1.Text + "' and slr_eff_dt='" + phdate1 + "'");

                            if (ddokdicno2.Rows.Count == 0)
                            {

                                string Inssql = "INSERT INTO hr_salary (slr_staff_no,slr_eff_dt,slr_end_dt,slr_salary_amt,slr_reason_cd,slr_crt_id,slr_crt_dt) VALUES ('" + Applcn_no1.Text + "','" + phdate1 + "','" + phdate2 + "','" + GP_amaun.Text + "','" + dd_sebab.SelectedValue + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                                Status = DBCon.Ora_Execute_CommamdText(Inssql);
                                //DBCon.Ora_Execute_table("INSERT INTO hr_salary (slr_staff_no,slr_eff_dt,slr_end_dt,slr_salary_amt,slr_reason_cd,slr_crt_id,slr_crt_dt) VALUES ('" + Applcn_no1.Text + "','" + phdate1 + "','" + phdate2 + "','" + GP_amaun.Text + "','" + dd_sebab.SelectedValue + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')");
                                if (Status == "SUCCESS")
                                {
                                    string Status1 = string.Empty;
                                    //DataTable sel_staff = new DataTable();
                                    //sel_staff = DBCon.Ora_Execute_table("select stf_staff_no,stf_kod_akaun,stf_name from hr_staff_profile where stf_staff_no='" + Applcn_no1.Text + "'");
                                    //if (sel_staff.Rows.Count != 0)
                                    //{
                                    //    DataTable sel_staff_kod = new DataTable();
                                    //    sel_staff_kod = DBCon.Ora_Execute_table("select * from KW_Ref_Carta_Akaun where kod_akaun='" + sel_staff.Rows[0]["stf_kod_akaun"].ToString() + "'");

                                    //    string Inssql1 = "INSERT INTO KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,kat_akaun,GL_invois_no,ref2,Tax_type,Tot_Amt,GL_process_dt,GL_desc1,GL_desc2,GL_nama_kod,crt_id,cr_dt,GL_sts) VALUES ('12.02.02','" + GP_amaun.Text + "','0.00','" + sel_staff_kod.Rows[0]["kat_akaun"].ToString() + "','','" + sel_staff.Rows[0]["stf_name"].ToString() + "','','0.00','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','Gaji Pokok','','" + sel_staff.Rows[0]["stf_kod_akaun"].ToString() + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A')";
                                    //    Status1 = DBCon.Ora_Execute_CommamdText(Inssql1);

                                    //}

                                    DataTable sel_phis1 = new DataTable();
                                    sel_phis1 = DBCon.Ora_Execute_table("select top(1) FORMAT(slr_eff_dt,'yyyy-MM-dd', 'en-us') as s1 from hr_salary where slr_staff_no='" + Applcn_no1.Text + "' and slr_eff_dt !='" + phdate1 + "' order by slr_eff_dt desc");

                                    if (sel_phis1.Rows.Count != 0)
                                    {
                                        DateTime time1_pos = DateTime.ParseExact(GP_date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                        string pdt1 = time1_pos.AddDays(-1).ToString("yyyy-MM-dd");

                                        string upssql1 = "update hr_salary set slr_end_dt='" + pdt1 + "',slr_upd_id ='" + Session["New"].ToString() + "',slr_upd_dt ='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where slr_staff_no ='" + Applcn_no1.Text + "' and slr_eff_dt ='" + sel_phis1.Rows[0]["s1"].ToString() + "'";
                                        Status = DBCon.Ora_Execute_CommamdText(upssql1);

                                    }


                                    GP_date.Text = "";
                                    GE_date.Text = "31/12/9999";
                                    GP_amaun.Text = "";
                                    dd_sebab.SelectedValue = "";
                                    service.audit_trail("P0066", "Gaji Pokok Simpan", "NO KAKITANGAN", Applcn_no1.Text);
                                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Gaji Pokok Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                                }
                                all_grid();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Gaji yang telah sedia ada Berjaya.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                            }
                        }
                        else
                        {
                            string Inssql1 = "update hr_salary set slr_end_dt='" + phdate2 + "',slr_salary_amt='" + GP_amaun.Text + "',slr_reason_cd='" + dd_sebab.SelectedValue + "',slr_upd_id='" + Session["New"].ToString() + "',slr_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where slr_staff_no='" + Applcn_no1.Text + "' and slr_eff_dt='" + phdate1 + "'";
                            Status = DBCon.Ora_Execute_CommamdText(Inssql1);
                            //DBCon.Ora_Execute_table("update hr_salary set slr_end_dt='"+ phdate2 +"',slr_salary_amt='" + GP_amaun.Text + "',slr_reason_cd='" + dd_sebab.SelectedValue + "',slr_upd_id='" + Session["New"].ToString() + "',slr_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where slr_staff_no='" + Applcn_no1.Text + "' and slr_eff_dt='" + phdate1 + "'");


                            if (Status == "SUCCESS")
                            {
                                DBCon.Ora_Execute_table("update hr_staff_profile set stf_bank_acc_no='" + s_bno.Text + "',stf_bank_cd='" + dd_s_bank.SelectedValue + "',stf_upd_id='" + Session["New"].ToString() + "',stf_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where stf_staff_no='" + Applcn_no1.Text + "'");

                                //DataTable sel_phis1 = new DataTable();
                                //sel_phis1 = DBCon.Ora_Execute_table("select top(1) FORMAT(slr_eff_dt,'yyyy-MM-dd', 'en-us') as s1 from hr_salary where slr_staff_no='" + Applcn_no1.Text + "' and slr_eff_dt !='" + phdate1 + "' order by slr_eff_dt desc");

                                //if (sel_phis1.Rows.Count != 0)
                                //{
                                //    DateTime time1_pos = DateTime.ParseExact(GP_date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                //    string pdt1 = time1_pos.AddDays(-1).ToString("yyyy-MM-dd");

                                //    string upssql1 = "update hr_salary set slr_end_dt='" + pdt1 + "',slr_upd_id ='" + Session["New"].ToString() + "',slr_upd_dt ='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where slr_staff_no ='" + Applcn_no1.Text + "' and slr_eff_dt ='" + sel_phis1.Rows[0]["s1"].ToString() + "'";
                                //    Status = DBCon.Ora_Execute_CommamdText(upssql1);

                                //}

                                GP_date.Text = "";
                                GE_date.Text = "31/12/9999";
                                GP_amaun.Text = "";
                                dd_sebab.SelectedValue = "";

                                if (gt_val1 == "1")
                                {
                                    Button5.Visible = true;
                                    Button5.Text = "Simpan";
                                }
                                else
                                {
                                    Button5.Visible = false;
                                }
                                GP_date.Attributes.Remove("Disabled");
                                all_grid();
                                GP_rno.Text = "0";
                                service.audit_trail("P0066", "Gaji Pokok Kemaskini", "NO KAKITANGAN", Applcn_no1.Text);
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Gaji Pokok Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                            }
                        }
                    }

                    else
                    {
                        GE_date.Text = "31/12/9999";
                        all_grid();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Tarikh Tamat Tidak Sah.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    all_grid();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila masukkan Field Tarikh.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                all_grid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

            }
            gv_list2();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnButton = sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
            System.Web.UI.WebControls.Label staffno = (System.Web.UI.WebControls.Label)gvRow.FindControl("st_no");
            System.Web.UI.WebControls.Label stdate = (System.Web.UI.WebControls.Label)gvRow.FindControl("Label2");
            System.Web.UI.WebControls.Label enddate = (System.Web.UI.WebControls.Label)gvRow.FindControl("Label3e");
            string stno = staffno.Text;
            string sdate = stdate.Text;

            string d1 = sdate;
            DateTime today1 = DateTime.ParseExact(d1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            phdate1 = today1.ToString("yyyy-MM-dd");


            DataTable ann_leave_det = new DataTable();
            ann_leave_det = DBCon.Ora_Execute_table("select slr_staff_no,FORMAT(slr_eff_dt,'dd/MM/yyyy', 'en-us') as slr_eff_dt,FORMAT(slr_end_dt,'dd/MM/yyyy', 'en-us') as slr_end_dt,slr_salary_amt,slr_reason_cd from hr_salary where slr_staff_no='" + stno + "' and slr_eff_dt='" + phdate1 + "'");
            if (ann_leave_det.Rows.Count != 0)
            {
                GP_date.Attributes.Add("Disabled", "Disabled");
                GP_date.Text = ann_leave_det.Rows[0]["slr_eff_dt"].ToString().Trim();
                GE_date.Text = ann_leave_det.Rows[0]["slr_end_dt"].ToString().Trim();
                decimal at1 = decimal.Parse(ann_leave_det.Rows[0]["slr_salary_amt"].ToString());
                GP_amaun.Text = at1.ToString("C").Replace("$", "").Replace("RM", "").Replace(",", "");
                dd_sebab.SelectedValue = ann_leave_det.Rows[0]["slr_reason_cd"].ToString().Trim();

                if (gt_val2 == "1")
                {
                    Button5.Visible = true;
                    Button5.Text = "Kemaskini";
                }
                else
                {
                    Button5.Visible = false;
                }
                all_grid();
                GP_rno.Text = "1";
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        gv_list2();
    }

    protected void rset_Click2(object sender, EventArgs e)
    {
        fixpromo_en.Attributes.Remove("Style");
        gv_list3();
        ET_mdate.Text = "";
        ET_sdate.Text = "31/12/9999";
        ET_amaun.Text = "";
        TextBox12.Text = "";
        TextBox13.Text = "";
        TextBox15.Text = "";
        fix_promo.Checked = false;
        ET_jelaun.SelectedValue = "";
        ET_jelaun.Attributes.Remove("Disabled");
        Button2.Text = "Simpan";
        all_grid();
        ET_rno.Text = "0";
    }

    protected void insert_Click2(object sender, EventArgs e)
    {
        try
        {
            string pr_sdt = string.Empty, pr_edt = string.Empty;
            int promo_cd = 0;
            if (Kaki_no.Text != "" && ET_jelaun.SelectedValue != "")
            {
                if (ET_mdate.Text != "" && ET_sdate.Text != "")
                {
                    string E1 = ET_mdate.Text;
                    DateTime Edt1 = DateTime.ParseExact(E1, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                    etdate1 = Edt1.ToString("yyyy-mm-dd");

                    string E2 = ET_sdate.Text;
                    DateTime Edt2 = DateTime.ParseExact(E2, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                    etdate2 = Edt2.ToString("yyyy-mm-dd");
                    if ((Edt2 - Edt1).TotalDays > 0)
                    {
                        if (ET_rno.Text == "0")
                        {
                            if(fix_promo.Checked == true)
                            {
                                string p_dt1 = TextBox12.Text;
                                DateTime pst_dt = DateTime.ParseExact(p_dt1, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                                pr_sdt = pst_dt.ToString("yyyy-mm-dd");

                                string p_dt2 = TextBox13.Text;
                                DateTime pet_dt = DateTime.ParseExact(p_dt2, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                                pr_edt = pet_dt.ToString("yyyy-mm-dd");

                                promo_cd = 1;
                            }

                            DataTable ddokdicno2 = new DataTable();
                            ddokdicno2 = DBCon.Ora_Execute_table("select * from hr_fixed_allowance where fxa_staff_no='" + Applcn_no1.Text + "' and fxa_allowance_type_cd='" + ET_jelaun.SelectedValue + "' and fxa_eff_dt='" + etdate1 + "'");

                            if (ddokdicno2.Rows.Count == 0)
                            {

                                string Status1 = string.Empty;
                                string Inssql = "INSERT INTO hr_fixed_allowance (fxa_staff_no,fxa_allowance_type_cd,fxa_eff_dt,fxa_end_dt,fxa_allowance_amt,fxa_crt_id,fxa_crt_dt,fxa_pst_dt,fxa_ped_dt,fxa_promo,fxa_promo_amt) VALUES ('" + Applcn_no1.Text + "','" + ET_jelaun.SelectedValue + "','" + etdate1 + "','" + etdate2 + "','" + ET_amaun.Text + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','"+ pr_sdt + "','" + pr_edt + "','" + promo_cd + "','"+ TextBox15.Text + "')";
                                Status = DBCon.Ora_Execute_CommamdText(Inssql);
                                if (Status == "SUCCESS")
                                {
                                    DataTable sel_phis1 = new DataTable();
                                    sel_phis1 = DBCon.Ora_Execute_table("select top(1) FORMAT(fxa_eff_dt,'yyyy-MM-dd', 'en-us') as s1 from hr_fixed_allowance where fxa_staff_no='" + Applcn_no1.Text + "' and fxa_eff_dt !='" + etdate1 + "' and fxa_allowance_type_cd='" + ET_jelaun.SelectedValue + "' order by fxa_eff_dt desc");

                                    if (sel_phis1.Rows.Count != 0)
                                    {
                                        DateTime time1_pos = DateTime.ParseExact(ET_mdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                        string pdt1 = time1_pos.AddDays(-1).ToString("yyyy-MM-dd");

                                        string upssql1 = "update hr_fixed_allowance set fxa_end_dt='" + pdt1 + "',fxa_upd_id ='" + Session["New"].ToString() + "',fxa_upd_dt ='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where fxa_staff_no ='" + Applcn_no1.Text + "' and fxa_allowance_type_cd='" + ET_jelaun.SelectedValue + "' and fxa_eff_dt ='" + sel_phis1.Rows[0]["s1"].ToString() + "'";
                                        Status = DBCon.Ora_Execute_CommamdText(upssql1);
                                    }


                                    ET_mdate.Text = "";
                                    ET_sdate.Text = "31/12/9999";
                                    ET_amaun.Text = "";
                                    ET_jelaun.SelectedValue = "";
                                    TextBox12.Text = "";
                                    TextBox13.Text = "";
                                    TextBox15.Text = "";
                                    fix_promo.Checked = false;
                                    service.audit_trail("P0066", "Elaun Tetap Simpan", "NO KAKITANGAN", Applcn_no1.Text);
                                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Elaun Tetap Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                                    all_grid();
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                            }
                        }
                        else
                        {

                            DBCon.Ora_Execute_table("update hr_fixed_allowance set fxa_eff_dt='" + etdate1 + "',fxa_end_dt='" + etdate2 + "',fxa_allowance_amt='" + ET_amaun.Text + "',fxa_upd_id='" + Session["New"].ToString() + "',fxa_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where fxa_staff_no='" + Applcn_no1.Text + "' and fxa_allowance_type_cd='" + ET_jelaun.SelectedValue + "'");

                            ET_mdate.Text = "";
                            ET_sdate.Text = "31/12/9999";
                            ET_amaun.Text = "";
                            ET_jelaun.SelectedValue = "";
                            ET_jelaun.Attributes.Remove("Disabled");
                            Button2.Text = "Simpan";
                            all_grid();
                            ET_rno.Text = "0";
                            service.audit_trail("P0066", "Elaun Tetap Kemaskini", "NO KAKITANGAN", Applcn_no1.Text);
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Elaun Tetap Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                        }
                    }
                    else
                    {
                        all_grid();
                        ET_sdate.Text = "31/12/9999";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Tarikh Tamat Tidak Sah.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    all_grid();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila masukkan Field Tarikh.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                all_grid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        gv_list3();
    }

    protected void lnkView_Click1(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnButton = sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
            System.Web.UI.WebControls.Label staffno = (System.Web.UI.WebControls.Label)gvRow.FindControl("est_no");
            System.Web.UI.WebControls.Label eallcd = (System.Web.UI.WebControls.Label)gvRow.FindControl("eall_cd");
            System.Web.UI.WebControls.Label fdt = (System.Web.UI.WebControls.Label)gvRow.FindControl("Label2");
            DateTime pst_dt = DateTime.ParseExact(fdt.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string stno = staffno.Text;
            string eacd = eallcd.Text;


            DataTable ann_leave_det = new DataTable();
            ann_leave_det = DBCon.Ora_Execute_table("select fxa_staff_no,FORMAT(fxa_eff_dt,'dd/MM/yyyy', 'en-us') as fxa_eff_dt,FORMAT(fxa_end_dt,'dd/MM/yyyy', 'en-us') as fxa_end_dt,fxa_allowance_type_cd,fxa_allowance_amt,FORMAT(fxa_pst_dt,'dd/MM/yyyy', 'en-us') as fxa_pst_dt,FORMAT(fxa_ped_dt,'dd/MM/yyyy', 'en-us') as fxa_ped_dt,fxa_promo,fxa_promo_amt from hr_fixed_allowance where fxa_staff_no='" + stno + "' and fxa_allowance_type_cd='" + eacd + "' and fxa_eff_dt='"+ pst_dt.ToString("yyyy-MM-dd") + "'");
            if (ann_leave_det.Rows.Count != 0)
            {
                ET_jelaun.Attributes.Add("Disabled", "Disabled");
                ET_mdate.Text = ann_leave_det.Rows[0]["fxa_eff_dt"].ToString().Trim();
                ET_sdate.Text = ann_leave_det.Rows[0]["fxa_end_dt"].ToString().Trim();
                decimal at1 = decimal.Parse(ann_leave_det.Rows[0]["fxa_allowance_amt"].ToString());
                ET_amaun.Text = at1.ToString("C").Replace("$", "").Replace("RM", "");
                ET_jelaun.SelectedValue = ann_leave_det.Rows[0]["fxa_allowance_type_cd"].ToString().Trim();

                TextBox12.Text = ann_leave_det.Rows[0]["fxa_pst_dt"].ToString().Trim();
                TextBox13.Text = ann_leave_det.Rows[0]["fxa_ped_dt"].ToString().Trim();
                fixpromo_en.Attributes.Add("Style", "pointer-events:none;");
                if (ann_leave_det.Rows[0]["fxa_promo"].ToString() == "1")
                {
                    fix_promo.Checked = true;
                    fixpromo_show.Attributes.Remove("Style");
                }
                else
                {
                    fixpromo_show.Attributes.Add("Style","display:none;");
                    fix_promo.Checked = false;
                }
                if (ann_leave_det.Rows[0]["fxa_promo_amt"].ToString() != "")
                {
                    TextBox15.Text = double.Parse(ann_leave_det.Rows[0]["fxa_promo_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                }
                else
                {
                    TextBox15.Text = "";
                }

                //if (gt_val2 == "1")
                //{
                Button2.Visible = true;
                Button2.Text = "Kemaskini";
                //}
                //else
                //{
                //Button2.Visible = false;
                //}
                all_grid();
                ET_rno.Text = "1";
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        gv_list3();
    }

    protected void rset_Click3(object sender, EventArgs e)
    {
        gv_list4();
        LL_mdate.Text = "";
        LL_sdate.Text = "31/12/9999";
        LL_amaun.Text = "";
        LL_jelaun.SelectedValue = "";
        TextBox17.Text = "";
        TextBox18.Text = "";
        TextBox19.Text = "";
        xta_promo.Checked = false;
        LL_jelaun.Attributes.Remove("Disabled");
        Button8.Text = "Simpan";
        all_grid();
        LLE_rno.Text = "0";
    }
    protected void insert_Click3(object sender, EventArgs e)
    {
        try
        {
            //pp1.Attributes.Remove("class");
            //pp3.Attributes.Add("class", "active");
            //p3.Attributes.Add("class", "active");

            string pr_sdt = string.Empty, pr_edt = string.Empty;
            int promo_cd = 0;
            if (Kaki_no.Text != "" && LL_jelaun.SelectedValue != "")
            {
                if (LL_mdate.Text != "" && LL_sdate.Text != "")
                {
                    string E1 = LL_mdate.Text;
                    DateTime Edt1 = DateTime.ParseExact(E1, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                    etdate1 = Edt1.ToString("yyyy-mm-dd");

                    string E2 = LL_sdate.Text;
                    DateTime Edt2 = DateTime.ParseExact(E2, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                    etdate2 = Edt2.ToString("yyyy-mm-dd");
                    if ((Edt2 - Edt1).TotalDays > 0)
                    {
                        if (LLE_rno.Text == "0")
                        {

                            if (xta_promo.Checked == true)
                            {
                                string p_dt1 = TextBox17.Text;
                                DateTime pst_dt = DateTime.ParseExact(p_dt1, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                                pr_sdt = pst_dt.ToString("yyyy-mm-dd");

                                string p_dt2 = TextBox18.Text;
                                DateTime pet_dt = DateTime.ParseExact(p_dt2, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                                pr_edt = pet_dt.ToString("yyyy-mm-dd");

                                promo_cd = 1;
                            }

                            DataTable ddokdicno2 = new DataTable();
                            ddokdicno2 = DBCon.Ora_Execute_table("select * from hr_extra_allowance where xta_staff_no='" + Applcn_no1.Text + "' and xta_allowance_type_cd='" + LL_jelaun.SelectedValue + "' and xta_eff_dt='" + etdate1 + "'");

                            if (ddokdicno2.Rows.Count == 0)
                            {
                                string Status1 = string.Empty;
                                string Inssql = "INSERT INTO hr_extra_allowance (xta_staff_no,xta_allowance_type_cd,xta_eff_dt,xta_end_dt,xta_allowance_amt,xta_crt_id,xta_crt_dt,xta_pst_dt,xta_ped_dt,xta_promo,xta_promo_amt) VALUES ('" + Applcn_no1.Text + "','" + LL_jelaun.SelectedValue + "','" + etdate1 + "','" + etdate2 + "','" + LL_amaun.Text + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','"+ pr_sdt + "','" + pr_edt + "','" + promo_cd + "','"+ TextBox19.Text +"')";
                                Status = DBCon.Ora_Execute_CommamdText(Inssql);
                                if (Status == "SUCCESS")
                                {

                                    DataTable sel_phis1 = new DataTable();
                                    sel_phis1 = DBCon.Ora_Execute_table("select top(1) FORMAT(xta_eff_dt,'yyyy-MM-dd', 'en-us') as s1 from hr_extra_allowance where xta_staff_no='" + Applcn_no1.Text + "' and xta_eff_dt !='" + etdate1 + "' and xta_allowance_type_cd='" + LL_jelaun.SelectedValue + "' order by xta_eff_dt desc");

                                    if (sel_phis1.Rows.Count != 0)
                                    {
                                        DateTime time1_pos = DateTime.ParseExact(LL_mdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                        string pdt1 = time1_pos.AddDays(-1).ToString("yyyy-MM-dd");

                                        string upssql1 = "update hr_extra_allowance set xta_end_dt='" + pdt1 + "',xta_upd_id ='" + Session["New"].ToString() + "',xta_upd_dt ='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where xta_staff_no ='" + Applcn_no1.Text + "' and xta_allowance_type_cd='" + LL_jelaun.SelectedValue + "' and xta_eff_dt ='" + sel_phis1.Rows[0]["s1"].ToString() + "'";
                                        Status = DBCon.Ora_Execute_CommamdText(upssql1);
                                    }

                                    LL_mdate.Text = "";
                                    LL_sdate.Text = "31/12/9999";
                                    LL_amaun.Text = "";
                                    LL_jelaun.SelectedValue = "";
                                    TextBox17.Text = "";
                                    TextBox18.Text = "";
                                    TextBox19.Text = "";
                                    xta_promo.Checked = false;
                                    service.audit_trail("P0066", "Lain-Lain Elaun Simpan", "NO KAKITANGAN", Applcn_no1.Text);
                                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Lain-Lain Elaun Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                                    all_grid();
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                            }
                        }
                        else
                        {


                            DBCon.Ora_Execute_table("update hr_extra_allowance set xta_eff_dt='" + etdate1 + "',xta_end_dt='" + etdate2 + "',xta_allowance_amt='" + LL_amaun.Text + "',xta_upd_id='" + Session["New"].ToString() + "',xta_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where xta_staff_no='" + Applcn_no1.Text + "' and xta_allowance_type_cd='" + LL_jelaun.SelectedValue + "'");

                            LL_mdate.Text = "";
                            LL_sdate.Text = "31/12/9999";
                            LL_amaun.Text = "";
                            LL_jelaun.SelectedValue = "";
                            LL_jelaun.Attributes.Remove("Disabled");
                            Button8.Text = "Simpan";
                            all_grid();
                            LLE_rno.Text = "0";
                            service.audit_trail("P0066", "Lain-Lain Elaun Kemaskini", "NO KAKITANGAN", Applcn_no1.Text);
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Lain-Lain Elaun Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                        }
                    }
                    else
                    {
                        all_grid();
                        LL_sdate.Text = "31/12/9999";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Tarikh Tamat Tidak Sah.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    all_grid();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila masukkan Field Tarikh.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                all_grid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        gv_list4();
    }

    protected void lnkView_Click2(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnButton = sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
            System.Web.UI.WebControls.Label staffno = (System.Web.UI.WebControls.Label)gvRow.FindControl("lst_no");
            System.Web.UI.WebControls.Label eallcd = (System.Web.UI.WebControls.Label)gvRow.FindControl("lall_cd");
            System.Web.UI.WebControls.Label fdt = (System.Web.UI.WebControls.Label)gvRow.FindControl("Label21");
            DateTime pst_dt = DateTime.ParseExact(fdt.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string stno = staffno.Text;
            string eacd = eallcd.Text;


            DataTable ann_leave_det = new DataTable();
            ann_leave_det = DBCon.Ora_Execute_table("select xta_staff_no,FORMAT(xta_eff_dt,'dd/MM/yyyy', 'en-us') as xta_eff_dt,FORMAT(xta_end_dt,'dd/MM/yyyy', 'en-us') as xta_end_dt,xta_allowance_type_cd,xta_allowance_amt,FORMAT(xta_pst_dt,'dd/MM/yyyy', 'en-us') as xta_pst_dt,FORMAT(xta_ped_dt,'dd/MM/yyyy', 'en-us') as xta_ped_dt,xta_promo,xta_promo_amt from hr_extra_allowance where xta_staff_no='" + stno + "' and xta_eff_dt='"+ pst_dt.ToString("yyyy-MM-dd") + "' and xta_allowance_type_cd='" + eacd + "'");
            if (ann_leave_det.Rows.Count != 0)
            {
                LL_jelaun.Attributes.Add("Disabled", "Disabled");
                LL_mdate.Text = ann_leave_det.Rows[0]["xta_eff_dt"].ToString().Trim();
                LL_sdate.Text = ann_leave_det.Rows[0]["xta_end_dt"].ToString().Trim();
                decimal at1 = decimal.Parse(ann_leave_det.Rows[0]["xta_allowance_amt"].ToString());
                LL_amaun.Text = at1.ToString("C").Replace("$", "").Replace("RM", "");
                LL_jelaun.SelectedValue = ann_leave_det.Rows[0]["xta_allowance_type_cd"].ToString().Trim();


                TextBox17.Text = ann_leave_det.Rows[0]["xta_pst_dt"].ToString().Trim();
                TextBox18.Text = ann_leave_det.Rows[0]["xta_ped_dt"].ToString().Trim();
                xta_promo_en.Attributes.Add("Style", "pointer-events:none;");
                if (ann_leave_det.Rows[0]["xta_promo"].ToString() == "1")
                {
                    xta_promo.Checked = true;
                    xtapromo_show.Attributes.Remove("Style");
                }
                else
                {
                    xtapromo_show.Attributes.Add("Style", "display:none;");
                    xta_promo.Checked = false;
                }
                if (ann_leave_det.Rows[0]["xta_promo_amt"].ToString() != "")
                {
                    TextBox19.Text = double.Parse(ann_leave_det.Rows[0]["xta_promo_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                }
                else
                {
                    TextBox19.Text = "";
                }

                //if (gt_val2 == "1")
                //{
                Button8.Visible = true;
                Button8.Text = "Kemaskini";
                //}
                //else
                //{
                //    Button8.Visible = false;
                //}
                all_grid();
                LLE_rno.Text = "1";
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        gv_list4();
    }

    protected void insert_Click4(object sender, EventArgs e)
    {
        try
        {

            if (Kaki_no.Text != "" && GP_date.Text != "")
            {
                if (GP_rno.Text == "0")
                {
                    DataTable ddokdicno2 = new DataTable();
                    ddokdicno2 = DBCon.Ora_Execute_table("select * from hr_salary where 1.	slr_staff_no='" + Applcn_no1.Text + "' and ann_min_service='" + GP_date.Text + "'");

                    if (ddokdicno2.Rows.Count == 0)
                    {
                        string d1 = GP_date.Text;
                        DateTime today1 = DateTime.ParseExact(d1, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                        phdate1 = today1.ToString("yyyy-mm-dd");

                        DBCon.Ora_Execute_table("INSERT INTO hr_salary (slr_staff_no,slr_eff_dt,slr_salary_amt,slr_reason_cd,slr_crt_id,slr_crt_dt) VALUES ('" + Applcn_no1.Text + "','" + phdate1 + "','" + GP_amaun.Text + "','" + dd_sebab.SelectedValue + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')");

                        Kaki_no.Text = "";
                        GP_date.Text = "";
                        GP_amaun.Text = "";
                        dd_sebab.SelectedValue = "";
                        service.audit_trail("P0066", "Gaji Pokok Simpan", "NO KAKITANGAN", Applcn_no1.Text);
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Gaji Pokok Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                        all_grid();

                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    string d1 = GP_date.Text;
                    DateTime today1 = DateTime.ParseExact(d1, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                    phdate1 = today1.ToString("yyyy-mm-dd");

                    DBCon.Ora_Execute_table("update hr_salary set slr_salary_amt='" + GP_amaun.Text + "',slr_reason_cd='" + dd_sebab.SelectedValue + "',slr_upd_id='" + Session["New"].ToString() + "',slr_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where slr_staff_no='" + Applcn_no1.Text + "' and slr_eff_dt='" + phdate1 + "'");

                    Kaki_no.Text = "";
                    GP_date.Text = "";
                    GP_amaun.Text = "";
                    dd_sebab.SelectedValue = "";
                    Button5.Text = "Simpan";
                    all_grid();
                    GP_rno.Text = "0";
                    service.audit_trail("P0066", "Gaji Pokok Kemaskini", "NO KAKITANGAN", Applcn_no1.Text);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Gaji Pokok Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
            }
            else
            {
                all_grid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void grid_1()
    {
        SqlCommand cmd2 = new SqlCommand("select hs.slr_staff_no,FORMAT(hs.slr_eff_dt,'dd/MM/yyyy') slr_eff_dt,case when FORMAT(ISNULL(hs.slr_end_dt,''),'dd/MM/yyyy') = '01/01/1900' then '' else FORMAT(ISNULL(hs.slr_end_dt,''),'dd/MM/yyyy') end slr_end_dt,hs.slr_salary_amt,ISNULL(rs.seb_desc,'') as seb_desc from hr_salary as hs left join hr_staff_profile as hsp on hsp.stf_staff_no=hs.slr_staff_no left join Ref_hr_sebab as rs on rs.seb_cd=hs.slr_reason_cd where hs.slr_staff_no='" + Applcn_no1.Text + "' ORDER BY hs.slr_eff_dt DESC", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            GridView1.DataSource = ds2;
            GridView1.DataBind();
            int columncount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView1.Rows[0].Cells[0].Text = "<strong><center>Maklumat Carian Tidak Dijumpai</center></strong>";
        }
        else
        {
            GridView1.DataSource = ds2;
            GridView1.DataBind();
        }
    }

    void grid_2()
    {
        SqlCommand cmd2 = new SqlCommand("select hfa.fxa_staff_no,hfa.fxa_allowance_type_cd,hfa.fxa_eff_dt,hfa.fxa_end_dt,je.hr_elau_desc,hfa.fxa_allowance_amt,FORMAT(hfa.fxa_crt_dt,'yyyy-MM-dd HH:mm:ss') as fxacrt_dt,case when FORMAT(hfa.fxa_pst_dt,'dd/MM/yyyy') = '01/01/1900' then '' else  FORMAT(hfa.fxa_pst_dt,'dd/MM/yyyy') end as fxa_pst_dt,case when FORMAT(hfa.fxa_ped_dt,'dd/MM/yyyy') ='01/01/1900' then '' else FORMAT(hfa.fxa_ped_dt,'dd/MM/yyyy') end as fxa_ped_dt,case when fxa_promo = '1' then 'YES' else 'NO' end as promo,fxa_promo_amt from hr_fixed_allowance as hfa left join hr_staff_profile as hsp on hsp.stf_staff_no=hfa.fxa_staff_no left join Ref_hr_jenis_elaun as je on je.hr_elau_Code=hfa.fxa_allowance_type_cd where hfa.fxa_staff_no='" + Applcn_no1.Text + "' ORDER BY hfa.fxa_eff_dt DESC", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            GridView2.DataSource = ds2;
            GridView2.DataBind();
            int columncount = GridView2.Rows[0].Cells.Count;
            GridView2.Rows[0].Cells.Clear();
            GridView2.Rows[0].Cells.Add(new TableCell());
            GridView2.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView2.Rows[0].Cells[0].Text = "<strong><center>Maklumat Carian Tidak Dijumpai</center></strong>";
        }
        else
        {
            GridView2.DataSource = ds2;
            GridView2.DataBind();
        }
    }

    protected void grvMergeHeader_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Fixed";
            HeaderCell.ColumnSpan = 5;
            HeaderGridRow.Cells.Add(HeaderCell);
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            //HeaderCell = new TableCell();
            //HeaderCell.Text = "Promosi";
            //HeaderCell.ColumnSpan = 5;
            //HeaderGridRow.Cells.Add(HeaderCell);
            //HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            //GridView2.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
        void grid_3()
    {
        SqlCommand cmd2 = new SqlCommand("select hfa.xta_staff_no,hfa.xta_allowance_type_cd,hfa.xta_eff_dt,hfa.xta_end_dt,je.hr_elau_desc,hfa.xta_allowance_amt,FORMAT(hfa.xta_crt_dt,'yyyy-MM-dd HH:mm:ss') as crt_dt,case when FORMAT(xta_pst_dt,'dd/MM/yyyy') = '01/01/1900' then '' else  FORMAT(xta_pst_dt,'dd/MM/yyyy') end as xta_pst_dt,case when FORMAT(xta_ped_dt,'dd/MM/yyyy') ='01/01/1900' then '' else FORMAT(xta_ped_dt,'dd/MM/yyyy') end as xta_ped_dt,case when xta_promo = '1' then 'YES' else 'NO' end as promo,xta_promo_amt from hr_extra_allowance as hfa left join hr_staff_profile as hsp on hsp.stf_staff_no=hfa.xta_staff_no left join Ref_hr_jenis_elaun as je on je.hr_elau_Code=hfa.xta_allowance_type_cd where hfa.xta_staff_no='" + Applcn_no1.Text + "' ORDER BY hfa.xta_eff_dt DESC", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            GridView3.DataSource = ds2;
            GridView3.DataBind();
            int columncount = GridView3.Rows[0].Cells.Count;
            GridView3.Rows[0].Cells.Clear();
            GridView3.Rows[0].Cells.Add(new TableCell());
            GridView3.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView3.Rows[0].Cells[0].Text = "<strong><center>Maklumat Carian Tidak Dijumpai</center></strong>";
        }
        else
        {
            GridView3.DataSource = ds2;
            GridView3.DataBind();
        }
    }

    protected void grvMergeHeader_RowCreated1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Fixed";
            HeaderCell.ColumnSpan = 5;
            HeaderGridRow.Cells.Add(HeaderCell);
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            //HeaderCell = new TableCell();
            //HeaderCell.Text = "Promosi";
            //HeaderCell.ColumnSpan = 5;
            //HeaderGridRow.Cells.Add(HeaderCell);
            //HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            //GridView3.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void gvSelected_PageIndexChanging_1(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        grid_1();

    }

    protected void gvSelected_PageIndexChanging_2(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        grid_2();

    }

    protected void gvSelected_PageIndexChanging_3(object sender, GridViewPageEventArgs e)
    {
        GridView3.PageIndex = e.NewPageIndex;
        grid_3();

    }

    protected void gvSelected_PageIndexChanging_4(object sender, GridViewPageEventArgs e)
    {
        GridView5.PageIndex = e.NewPageIndex;
        grid5();

    }

    protected void gvSelected_PageIndexChanging_6(object sender, GridViewPageEventArgs e)
    {
        GridView6.PageIndex = e.NewPageIndex;
        grid5();

    }

    protected void gvSelected_PageIndexChanging_5(object sender, GridViewPageEventArgs e)
    {
        GridView4.PageIndex = e.NewPageIndex;
        grid6();

    }

    protected void hapus_click1(object sender, EventArgs e)
    {
        try
        {
            if (Kaki_no.Text != "")
            {
                string rcount = string.Empty;
                int count = 0;
                foreach (GridViewRow gvrow in GridView1.Rows)
                {
                    var rb = gvrow.FindControl("rbtnSelect1") as System.Web.UI.WebControls.CheckBox;
                    if (rb.Checked == true)
                    {
                        count++;
                    }
                    rcount = count.ToString();
                }
                if (rcount != "0")
                {
                    foreach (GridViewRow gvrow in GridView1.Rows)
                    {
                        var a_no = (System.Web.UI.WebControls.Label)gvrow.FindControl("st_no");
                        var a_id = (System.Web.UI.WebControls.Label)gvrow.FindControl("Label2");

                        string d1 = a_id.Text;
                        DateTime today1 = DateTime.ParseExact(d1, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                        phdate1 = today1.ToString("yyyy-mm-dd");

                        var checkbox = gvrow.FindControl("rbtnSelect1") as System.Web.UI.WebControls.CheckBox;
                        if (checkbox.Checked == true)
                        {
                            DBCon.Execute_CommamdText("DELETE from hr_salary where slr_staff_no='" + a_no.Text + "' and slr_eff_dt='" + phdate1 + "'");
                        }
                    }

                    all_grid();
                    clr_pokok();
                    Button5.Text = "Simpan";
                    GP_rno.Text = "0";
                    service.audit_trail("P0066", "Gaji Pokok Hapus", "NO KAKITANGAN", Applcn_no1.Text);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Gaji Pokok Berjaya Dihapuskan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                all_grid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
        gv_list2();
    }

    void clr_pokok()
    {
        GP_date.Text = "";
        GE_date.Text = "31/12/9999";
        GP_date.Attributes.Remove("Disabled");
        GP_amaun.Text = "";
        dd_sebab.SelectedValue = "";
    }

    protected void hapus_click2(object sender, EventArgs e)
    {
        try
        {
            if (Kaki_no.Text != "")
            {
                string rcount = string.Empty;
                int count = 0;
                foreach (GridViewRow gvrow in GridView2.Rows)
                {
                    var rb = gvrow.FindControl("rbtnSelect2") as System.Web.UI.WebControls.CheckBox;
                    if (rb.Checked == true)
                    {
                        count++;
                    }
                    rcount = count.ToString();
                }
                if (rcount != "0")
                {
                    foreach (GridViewRow gvrow in GridView2.Rows)
                    {
                        var a_no = (System.Web.UI.WebControls.Label)gvrow.FindControl("est_no");
                        var a_id = (System.Web.UI.WebControls.Label)gvrow.FindControl("eall_cd");
                        var fct_dt = (System.Web.UI.WebControls.Label)gvrow.FindControl("fxactdt");


                        var checkbox = gvrow.FindControl("rbtnSelect2") as System.Web.UI.WebControls.CheckBox;
                        if (checkbox.Checked == true)
                        {
                            DBCon.Execute_CommamdText("DELETE from hr_fixed_allowance where fxa_staff_no='" + a_no.Text + "' and fxa_allowance_type_cd='" + a_id.Text + "' and fxa_crt_dt='" + fct_dt.Text + "'");
                        }
                    }
                    ET_mdate.Text = "";
                    ET_sdate.Text = "31/12/9999";
                    ET_amaun.Text = "";
                    ET_jelaun.SelectedValue = "";
                    ET_jelaun.Attributes.Remove("Disabled");
                    Button2.Text = "Simpan";
                    ET_rno.Text = "0";
                    all_grid();
                    service.audit_trail("P0066", "Elaun Tetap Hapus", "NO KAKITANGAN", Applcn_no1.Text);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Elaun Tetap Berjaya Dihapuskan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                all_grid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
        gv_list3();
    }

    protected void hapus_click3(object sender, EventArgs e)
    {
        try
        {
            if (Kaki_no.Text != "")
            {
                string rcount = string.Empty;
                int count = 0;
                foreach (GridViewRow gvrow in GridView3.Rows)
                {
                    var rb = gvrow.FindControl("rbtnSelect3") as System.Web.UI.WebControls.CheckBox;
                    if (rb.Checked == true)
                    {
                        count++;
                    }
                    rcount = count.ToString();
                }
                if (rcount != "0")
                {
                    foreach (GridViewRow gvrow in GridView3.Rows)
                    {
                        var a_no = (System.Web.UI.WebControls.Label)gvrow.FindControl("lst_no");
                        var a_id = (System.Web.UI.WebControls.Label)gvrow.FindControl("lall_cd");
                        var c_dt = (System.Web.UI.WebControls.Label)gvrow.FindControl("xta_crtdt");

                        var checkbox = gvrow.FindControl("rbtnSelect3") as System.Web.UI.WebControls.CheckBox;
                        if (checkbox.Checked == true)
                        {
                            DBCon.Execute_CommamdText("DELETE from hr_extra_allowance where xta_staff_no='" + a_no.Text + "' and xta_allowance_type_cd='" + a_id.Text + "' and xta_crt_dt='" + c_dt.Text + "'");
                        }
                    }
                    LL_mdate.Text = "";
                    LL_sdate.Text = "31/12/9999";
                    LL_amaun.Text = "";
                    LL_jelaun.SelectedValue = "";
                    LL_jelaun.Attributes.Remove("Disabled");
                    Button8.Text = "Simpan";
                    LLE_rno.Text = "0";
                    all_grid();
                    service.audit_trail("P0066", "Lain-lain Elaun Hapus", "NO KAKITANGAN", Applcn_no1.Text);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Lain-lain Elaun Berjaya Dihapuskan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                all_grid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
        gv_list4();
    }


    protected void click_rst(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../SUMBER_MANUSIA/HR_SELE_GAJI.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../SUMBER_MANUSIA/HR_SELE_GAJI_view.aspx");
    }

    protected void fix_promo_changed(object sender, EventArgs e)
    {
        if (fix_promo.Checked == true)
        {
            fixpromo_show.Attributes.Remove("Style");
        }
        else
        {
            fixpromo_show.Attributes.Add("Style","display:none");
        }
        all_grid();
        gv_list3();

    }
    protected void xta_promo_changed(object sender, EventArgs e)
    {
        if (xta_promo.Checked == true)
        {
            xtapromo_show.Attributes.Remove("Style");
        }
        else
        {
            xtapromo_show.Attributes.Add("Style", "display:none");
        }
        all_grid();
        gv_list4();

    }

}