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

public partial class PP_SMPembiayaan : System.Web.UI.Page
{
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string str;
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    SqlCommand com;
    DataTable dt = new DataTable();
    string Status = string.Empty;
    string level, userid;
    string cc_no = string.Empty;
    string ses_no = string.Empty, appno1 = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {

        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Button3);
        scriptManager.RegisterPostBackControl(this.Button1);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {

                level = Session["level"].ToString();
                userid = Session["New"].ToString();

                txtname.Attributes.Add("Readonly", "Readonly");
                txtwil.Attributes.Add("Readonly", "Readonly");
                txtjumla.Attributes.Add("Readonly", "Readonly");
                txtcaw.Attributes.Add("Readonly", "Readonly");
                txttempoh.Attributes.Add("Readonly", "Readonly");
                txtamaun.Attributes.Add("Readonly", "Readonly");
                txttemp.Attributes.Add("Readonly", "Readonly");
                TextBox2.Attributes.Add("Readonly", "Readonly");
                TextBox3.Attributes.Add("Readonly", "Readonly");
                TextBox4.Attributes.Add("Readonly", "Readonly");
                TextBox5.Attributes.Add("Readonly", "Readonly");
                TextBox6.Attributes.Add("Readonly", "Readonly");
                TextBox7.Attributes.Add("Readonly", "Readonly");

                Pengenalan();
                NegriBind();
                Hubungan();
                if (Session["sess_no"] != "" && Session["sess_no"] != null)
                {
                    Applcn_no.Text = Session["sess_no"].ToString();
                    srch();
                }

              
                var samp = Request.Url.Query;

                if (samp != "")
                {
                    if (service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"])) != "")
                    {
                        Applcn_no.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    }
                    else if (Request.QueryString["spi_icno"].ToString() != "")
                    {
                        Applcn_no.Text = Request.QueryString["spi_icno"].ToString();
                    }
                    srch();
                    Button9.Visible = false;
                    Button10.Visible = false;                    
                }
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }

    }


    void Pengenalan()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select Description,Description_Code from Ref_Jenis_Pengenalan WHERE Status = 'A' order by Description";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);

            DD_Pengenalan2.DataSource = dt;
            DD_Pengenalan2.DataTextField = "Description";
            DD_Pengenalan2.DataValueField = "Description_Code";
            DD_Pengenalan2.DataBind();
            DD_Pengenalan2.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            DD_Pengenalan3.DataSource = dt;
            DD_Pengenalan3.DataTextField = "Description";
            DD_Pengenalan3.DataValueField = "Description_Code";
            DD_Pengenalan3.DataBind();
            DD_Pengenalan3.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            //DD_Pengenalan4.DataSource = dt;
            //DD_Pengenalan4.DataTextField = "Description";
            //DD_Pengenalan4.DataValueField = "Description_Code";
            //DD_Pengenalan4.DataBind();
            //DD_Pengenalan4.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void tab1()
    {
        p5.Attributes.Add("class", "tab-pane active");
        p6.Attributes.Add("class", "tab-pane");
        //p7.Attributes.Add("class", "tab-pane");

        Li3.Attributes.Add("class", "active");
        Li4.Attributes.Remove("class");
        //Li5.Attributes.Remove("class");

    }

    void tab2()
    {
        p6.Attributes.Add("class", "tab-pane active");
        p5.Attributes.Add("class", "tab-pane");
        //p7.Attributes.Add("class", "tab-pane");

        Li4.Attributes.Add("class", "active");
        Li3.Attributes.Remove("class");
        //Li5.Attributes.Remove("class");

    }

    //void tab3()
    //{
    //    p7.Attributes.Add("class", "tab-pane active");
    //    p6.Attributes.Add("class", "tab-pane");
    //    p5.Attributes.Add("class", "tab-pane");

    //    Li5.Attributes.Add("class", "active");
    //    Li4.Attributes.Remove("class");
    //    Li3.Attributes.Remove("class");

    //}

    protected void RB1_CheckedChanged(object sender, EventArgs e)
    {
        if (RB1.Checked == true)
        {
            RB2.Checked = false;
            RB3.Checked = false;
            RB4.Checked = false;
        }
        tab1();

    }

    protected void RB1_1_CheckedChanged(object sender, EventArgs e)
    {
        if (RB21.Checked == true)
        {
            RB22.Checked = false;
            RB23.Checked = false;
            RB24.Checked = false;
        }
        tab2();
    }

    protected void RB2_1_CheckedChanged(object sender, EventArgs e)
    {
        //if (RB31.Checked == true)
        //{
        //    RB32.Checked = false;
        //    RB33.Checked = false;
        //    RB34.Checked = false;
        //}
        //tab3();
    }
    protected void RB2_CheckedChanged(object sender, EventArgs e)
    {
        if (RB2.Checked == true)
        {
            RB1.Checked = false;
            RB3.Checked = false;
            RB4.Checked = false;
        }
        tab1();
    }

    protected void RB1_2_CheckedChanged(object sender, EventArgs e)
    {

        if (RB22.Checked == true)
        {
            RB21.Checked = false;
            RB23.Checked = false;
            RB24.Checked = false;
        }
        tab2();
    }

    protected void RB2_2_CheckedChanged(object sender, EventArgs e)
    {

        //if (RB32.Checked == true)
        //{
        //    RB31.Checked = false;
        //    RB33.Checked = false;
        //    RB34.Checked = false;
        //}
        //tab3();

    }
    protected void RB3_CheckedChanged(object sender, EventArgs e)
    {
        if (RB3.Checked == true)
        {
            RB1.Checked = false;
            RB2.Checked = false;
            RB4.Checked = false;
        }
        tab1();

    }

    protected void RB1_3_CheckedChanged(object sender, EventArgs e)
    {

        if (RB23.Checked == true)
        {
            RB21.Checked = false;
            RB22.Checked = false;
            RB24.Checked = false;
        }
        tab2();

    }

    protected void RB2_3_CheckedChanged(object sender, EventArgs e)
    {

        //if (RB33.Checked == true)
        //{
        //    RB31.Checked = false;
        //    RB32.Checked = false;
        //    RB34.Checked = false;
        //}
        //tab3();
    }

    protected void RB4_CheckedChanged(object sender, EventArgs e)
    {
        if (RB4.Checked == true)
        {
            RB1.Checked = false;
            RB2.Checked = false;
            RB3.Checked = false;
        }
        tab1();
    }

    protected void RB1_4_CheckedChanged(object sender, EventArgs e)
    {

        if (RB24.Checked == true)
        {
            RB21.Checked = false;
            RB22.Checked = false;
            RB23.Checked = false;
        }
        tab2();
    }
    protected void RB2_4_CheckedChanged(object sender, EventArgs e)
    {
        //if (RB34.Checked == true)
        //{
        //    RB31.Checked = false;
        //    RB32.Checked = false;
        //    RB33.Checked = false;
        //}
        //tab3();
    }

    protected void RB11_CheckedChanged(object sender, EventArgs e)
    {
        if (RB11.Checked == true)
        {
            RB12.Checked = false;
        }
        tab1();
    }
    protected void RB111_CheckedChanged(object sender, EventArgs e)
    {

        if (TJ21.Checked == true)
        {
            TJ22.Checked = false;
        }
        tab2();
    }

    protected void RB211_CheckedChanged(object sender, EventArgs e)
    {

        //if (TJ31.Checked == true)
        //{
        //    TJ32.Checked = false;
        //}
        //tab3();
    }
    protected void RB12_CheckedChanged(object sender, EventArgs e)
    {
        if (RB12.Checked == true)
        {
            RB11.Checked = false;
        }
        tab1();
    }
    protected void RB112_CheckedChanged(object sender, EventArgs e)
    {

        if (TJ22.Checked == true)
        {
            TJ21.Checked = false;
        }
        tab2();
    }
    protected void RB212_CheckedChanged(object sender, EventArgs e)
    {

        //if (TJ32.Checked == true)
        //{
        //    TJ31.Checked = false;
        //}
        //tab3();
    }


    void NegriBind()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select Decription,Decription_Code from Ref_Negeri  WHERE Status = 'A' order by Decription";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList1.DataSource = dt;
            DropDownList1.DataTextField = "Decription";
            DropDownList1.DataValueField = "Decription_Code";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            DropDownList2.DataSource = dt;
            DropDownList2.DataTextField = "Decription";
            DropDownList2.DataValueField = "Decription_Code";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            DD_NegriBind5.DataSource = dt;
            DD_NegriBind5.DataTextField = "Decription";
            DD_NegriBind5.DataValueField = "Decription_Code";
            DD_NegriBind5.DataBind();
            DD_NegriBind5.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            DD_NegriBind6.DataSource = dt;
            DD_NegriBind6.DataTextField = "Decription";
            DD_NegriBind6.DataValueField = "Decription_Code";
            DD_NegriBind6.DataBind();
            DD_NegriBind6.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            DD_NegriBind7.DataSource = dt;
            DD_NegriBind7.DataTextField = "Decription";
            DD_NegriBind7.DataValueField = "Decription_Code";
            DD_NegriBind7.DataBind();
            DD_NegriBind7.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            DD_NegriBind8.DataSource = dt;
            DD_NegriBind8.DataTextField = "Decription";
            DD_NegriBind8.DataValueField = "Decription_Code";
            DD_NegriBind8.DataBind();
            DD_NegriBind8.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            //DD_NegriBind9.DataSource = dt;
            //DD_NegriBind9.DataTextField = "Decription";
            //DD_NegriBind9.DataValueField = "Decription_Code";
            //DD_NegriBind9.DataBind();
            //DD_NegriBind9.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            //DD_NegriBind10.DataSource = dt;
            //DD_NegriBind10.DataTextField = "Decription";
            //DD_NegriBind10.DataValueField = "Decription_Code";
            //DD_NegriBind10.DataBind();
            //DD_NegriBind10.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void Hubungan()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select Contact_Name,Contact_Code from Ref_Hubungan order by Contact_Name";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList3.DataSource = dt;
            DropDownList3.DataTextField = "Contact_Name";
            DropDownList3.DataValueField = "Contact_Code";
            DropDownList3.DataBind();
            DropDownList3.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            DropDownList4.DataSource = dt;
            DropDownList4.DataTextField = "Contact_Name";
            DropDownList4.DataValueField = "Contact_Code";
            DropDownList4.DataBind();
            DropDownList4.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnsrch_Click(object sender, EventArgs e)
    {
        NegriBind();
        srch();

    }

    protected void srch()
    {
        DataTable gua1 = new DataTable();
        DataTable gua2 = new DataTable();
        DataTable gua3 = new DataTable();
        try
        {
            Session.Remove("sess_no");
            if (Applcn_no.Text != "")
            {

                DataTable ddicno = new DataTable();
                ddicno = DBCon.Ora_Execute_table("select distinct app_applcn_no from jpa_application JA where JA.app_sts_cd='Y' and '" + Applcn_no.Text + "' IN(JA.app_applcn_no, JA.app_new_icno)");

                if (ddicno.Rows.Count != 0)
                {
                    DataTable select_app = new DataTable();
                    select_app = DBCon.Ora_Execute_table("select * from (select JA.app_applcn_no,JA.app_new_icno,JA.app_name,RW.Wilayah_Name,ISNULL(RB.branch_desc, '') AS branch_desc,JA.app_loan_amt,JA.appl_loan_dur,JA.app_permnt_address,ja.app_permnt_postcode,ja.app_permnt_state_cd,ja.app_phone_h,ja.app_phone_m,ja.app_phone_o,ja.app_mailing_address,JA.app_mailing_postcode,ja.app_mailing_state_cd,ISNULL(JA.app_cumm_installment_amt,'') as app_cumm_installment_amt,ISNULL(JA.app_cumm_pay_amt,'') as app_cumm_pay_amt,ISNULL(JA.app_backdated_amt,'') as app_backdated_amt,ISNULL(JA.app_cumm_profit_amt,'') as app_cumm_profit_amt,ISNULL(JA.app_cumm_saving_amt,'') as app_cumm_saving_amt,ISNULL(JA.app_bal_loan_amt,'') as app_bal_loan_amt,ISNULL(JA.app_current_jbb_ind,'') as app_current_jbb_ind,ISNULL(CASE WHEN CONVERT(DATE, app_end_pay_dt) = '1900-01-01' THEN '' ELSE CONVERT(CHAR(10), app_end_pay_dt, 103) END, '') AS app_end_pay_dt from jpa_application as JA  Left Join jpa_calculate_fee as JC ON JC.cal_applcn_no=JA.app_applcn_no Left Join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd Left join ref_branch AS RB ON RB.branch_cd=JA.app_branch_cd where JA.app_applcn_no = '" + ddicno.Rows[0]["app_applcn_no"] + "') as a full outer join (select * from jpa_relative where rel_applcn_no='" + ddicno.Rows[0]["app_applcn_no"] + "') as b on a.app_applcn_no=b.rel_applcn_no");

                    txtname.Text = select_app.Rows[0]["app_name"].ToString();
                    txtwil.Text = select_app.Rows[0]["Wilayah_Name"].ToString();
                    //decimal amt1 = (decimal)select_app.Rows[0]["app_loan_amt"];
                    txtjumla.Text = double.Parse(select_app.Rows[0]["app_loan_amt"].ToString()).ToString("C").Replace("$", "");
                    txtcaw.Text = select_app.Rows[0]["branch_desc"].ToString();
                    txttempoh.Text = select_app.Rows[0]["appl_loan_dur"].ToString();

                    Textarea1.Value = select_app.Rows[0]["app_permnt_address"].ToString();
                    Textarea2.Value = select_app.Rows[0]["app_mailing_address"].ToString();
                    txttn16d.Text = select_app.Rows[0]["app_permnt_postcode"].ToString();
                    txttp.Text = select_app.Rows[0]["app_mailing_postcode"].ToString();
                    DropDownList2.SelectedValue = select_app.Rows[0]["app_permnt_state_cd"].ToString();
                    DropDownList1.SelectedValue = select_app.Rows[0]["app_mailing_state_cd"].ToString();
                    TextBox10.Text = select_app.Rows[0]["app_phone_h"].ToString();
                    TextBox8.Text = select_app.Rows[0]["app_phone_m"].ToString();
                    TextBox11.Text = select_app.Rows[0]["app_phone_o"].ToString();
                    decimal amt4 = 0M;
                    if (select_app.Rows[0]["app_backdated_amt"].ToString() != "0")
                    {
                        decimal amt2 = (decimal)select_app.Rows[0]["app_backdated_amt"];

                        decimal amt3 = (decimal)select_app.Rows[0]["app_cumm_installment_amt"];
                        if (amt2 == 0 || amt3 == 0)
                        {
                            amt4 = 0;
                        }
                        else
                        {
                            amt4 = amt2 / amt3;
                        }

                        TextBox1.Text = double.Parse(amt4.ToString()).ToString("C").Replace("$", "");
                    }

                    if (select_app.Rows[0]["app_current_jbb_ind"].ToString() == "N")
                    {
                        TextBox6.Text = "NORMAL";
                    }
                    else if (select_app.Rows[0]["app_current_jbb_ind"].ToString() == "P")
                    {
                        TextBox6.Text = "PJS";
                    }
                    else if (select_app.Rows[0]["app_current_jbb_ind"].ToString() == "E")
                    {
                        TextBox6.Text = "PANJANG TEMPOH";
                    }
                    else if (select_app.Rows[0]["app_current_jbb_ind"].ToString() == "H")
                    {
                        TextBox6.Text = "TANGGUH";
                    }
                    else if (select_app.Rows[0]["app_current_jbb_ind"].ToString() == "L")
                    {
                        TextBox6.Text = "HAPUS KIRA";
                    }
                    else
                    {
                        TextBox6.Text = "";
                    }
                    TextBox7.Text = select_app.Rows[0]["app_end_pay_dt"].ToString();


                    decimal a1 = (decimal)select_app.Rows[0]["app_cumm_installment_amt"];
                    txtamaun.Text = a1.ToString("C").Replace("$", "").Replace("RM", "");
                    decimal a2 = (decimal)select_app.Rows[0]["app_cumm_pay_amt"];
                    txttemp.Text = a2.ToString("C").Replace("$", "").Replace("RM", "");
                    decimal a3 = (decimal)select_app.Rows[0]["app_backdated_amt"];
                    TextBox2.Text = a3.ToString("C").Replace("$", "").Replace("RM", "");
                    decimal a4 = (decimal)select_app.Rows[0]["app_cumm_profit_amt"];
                    TextBox3.Text = a4.ToString("C").Replace("$", "").Replace("RM", "");
                    decimal a5 = (decimal)select_app.Rows[0]["app_cumm_saving_amt"];
                    TextBox4.Text = a5.ToString("C").Replace("$", "").Replace("RM", "");
                    decimal a6 = (decimal)select_app.Rows[0]["app_bal_loan_amt"];
                    TextBox5.Text = a6.ToString("C").Replace("$", "").Replace("RM", "");


                    if (select_app.Rows.Count >= 1)
                    {
                        TextBox12.Text = select_app.Rows[0]["rel_name"].ToString();
                        TextBox13.Text = select_app.Rows[0]["rel_new_icno"].ToString();
                        TextBox14.Text = select_app.Rows[0]["rel_phone"].ToString();
                        DropDownList4.SelectedValue = select_app.Rows[0]["rel_relation_cd"].ToString();
                    }
                    else
                    {
                        TextBox12.Text = "";
                        TextBox13.Text = "";
                        TextBox14.Text = "";
                        DropDownList4.SelectedValue = "";
                    }
                    if (select_app.Rows.Count >= 2)
                    {
                        TextBox15.Text = select_app.Rows[1]["rel_name"].ToString();
                        TextBox16.Text = select_app.Rows[1]["rel_new_icno"].ToString();
                        TextBox17.Text = select_app.Rows[1]["rel_phone"].ToString();
                        DropDownList3.SelectedValue = select_app.Rows[1]["rel_relation_cd"].ToString();
                    }
                    else
                    {
                        TextBox15.Text = "";
                        TextBox16.Text = "";
                        TextBox17.Text = "";
                        DropDownList3.SelectedValue = "";
                    }


                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
                DataTable gu1 = new DataTable();
                gu1 = DBCon.Ora_Execute_table("select gua_seq_no from jpa_guarantor where gua_applcn_no='" + Applcn_no.Text + "' order by gua_seq_no");
                if (gu1.Rows.Count == 1)
                {

                    gua1 = DBCon.Ora_Execute_table("select gua_name,gua_phone,gua_permanent_address,gua_permanent_postcode,gua_permanent_state_cd,gua_mailing_address,gua_mailing_postcode,gua_mailing_state_cd,gua_ic_type_cd,gua_icno,gua_job,gua_job_sector_ind,gua_job_status_ind,gua_nett_income,gua_gross_income,gua_employer_name,gua_employer_phone,gua_employer_phone_ext from jpa_guarantor where gua_applcn_no='" + Applcn_no.Text + "' and gua_seq_no='1'  order by gua_seq_no");
                    if (gua1.Rows.Count != 0)
                    {
                        TextBox27.Text = gua1.Rows[0]["gua_name"].ToString();
                        TextBox35.Text = gua1.Rows[0]["gua_phone"].ToString();
                        TextArea7.Value = gua1.Rows[0]["gua_permanent_address"].ToString();
                        TextArea8.Value = gua1.Rows[0]["gua_mailing_address"].ToString();
                        TextArea7.Value = gua1.Rows[0]["gua_permanent_address"].ToString();
                        TextBox62.Text = gua1.Rows[0]["gua_permanent_postcode"].ToString();
                        TextBox63.Text = gua1.Rows[0]["gua_mailing_postcode"].ToString();
                        DD_NegriBind5.SelectedValue = gua1.Rows[0]["gua_permanent_state_cd"].ToString();
                        DD_NegriBind6.SelectedValue = gua1.Rows[0]["gua_mailing_state_cd"].ToString();
                        DD_Pengenalan2.SelectedValue = gua1.Rows[0]["gua_ic_type_cd"].ToString();
                        TextBox28.Text = gua1.Rows[0]["gua_icno"].ToString();
                        TextBox29.Text = gua1.Rows[0]["gua_job"].ToString();
                        if (gua1.Rows[0]["gua_job_sector_ind"].ToString() == "GOV")
                        {
                            RB1.Checked = true;
                            RB2.Checked = false;
                            RB3.Checked = false;
                            RB4.Checked = false;
                        }
                        else if (gua1.Rows[0]["gua_job_sector_ind"].ToString() == "GLC")
                        {
                            RB1.Checked = false;
                            RB2.Checked = true;
                            RB3.Checked = false;
                            RB4.Checked = false;
                        }
                        else if (gua1.Rows[0]["gua_job_sector_ind"].ToString() == "GLI")
                        {
                            RB1.Checked = false;
                            RB2.Checked = false;
                            RB3.Checked = true;
                            RB4.Checked = false;
                        }
                        else if (gua1.Rows[0]["gua_job_sector_ind"].ToString() == "BHD")
                        {
                            RB1.Checked = false;
                            RB2.Checked = false;
                            RB3.Checked = false;
                            RB4.Checked = true;
                        }
                        else if (gua1.Rows[0]["gua_job_sector_ind"].ToString() == "")
                        {
                            RB1.Checked = false;
                            RB2.Checked = false;
                            RB3.Checked = false;
                            RB4.Checked = false;
                        }
                        if (gua1.Rows[0]["gua_job_status_ind"].ToString() == "T")
                        {
                            RB11.Checked = true;
                            RB12.Checked = false;
                        }
                        else if (gua1.Rows[0]["gua_job_status_ind"].ToString() == "K")
                        {
                            RB11.Checked = false;
                            RB12.Checked = true;
                        }
                        decimal net_amt2 = (decimal)gua1.Rows[0]["gua_nett_income"];
                        TextBox31.Text = net_amt2.ToString("C").Replace("RM", "").Replace("$", "");
                        decimal gross_amt2 = (decimal)gua1.Rows[0]["gua_gross_income"];
                        TextBox33.Text = gross_amt2.ToString("C").Replace("RM", "").Replace("$", "");

                        TextBox34.Text = gua1.Rows[0]["gua_employer_name"].ToString();
                        TextBox30.Text = gua1.Rows[0]["gua_employer_phone"].ToString();
                        TextBox32.Text = gua1.Rows[0]["gua_employer_phone_ext"].ToString();
                    }
                }
                else if (gu1.Rows.Count == 2)
                {

                    gua1 = DBCon.Ora_Execute_table("select gua_name,gua_phone,gua_permanent_address,gua_permanent_postcode,gua_permanent_state_cd,gua_mailing_address,gua_mailing_postcode,gua_mailing_state_cd,gua_ic_type_cd,gua_icno,gua_job,gua_job_sector_ind,gua_job_status_ind,gua_nett_income,gua_gross_income,gua_employer_name,gua_employer_phone,gua_employer_phone_ext from jpa_guarantor where gua_applcn_no='" + Applcn_no.Text + "' and gua_seq_no='1'  order by gua_seq_no");
                    if (gua1.Rows.Count != 0)
                    {
                        TextBox27.Text = gua1.Rows[0]["gua_name"].ToString();
                        TextBox35.Text = gua1.Rows[0]["gua_phone"].ToString();
                        TextArea7.Value = gua1.Rows[0]["gua_permanent_address"].ToString();
                        TextArea8.Value = gua1.Rows[0]["gua_mailing_address"].ToString();
                        TextArea7.Value = gua1.Rows[0]["gua_permanent_address"].ToString();
                        TextBox62.Text = gua1.Rows[0]["gua_permanent_postcode"].ToString();
                        TextBox63.Text = gua1.Rows[0]["gua_mailing_postcode"].ToString();
                        DD_NegriBind5.SelectedValue = gua1.Rows[0]["gua_permanent_state_cd"].ToString();
                        DD_NegriBind6.SelectedValue = gua1.Rows[0]["gua_mailing_state_cd"].ToString();
                        DD_Pengenalan2.SelectedValue = gua1.Rows[0]["gua_ic_type_cd"].ToString();
                        TextBox28.Text = gua1.Rows[0]["gua_icno"].ToString();
                        TextBox29.Text = gua1.Rows[0]["gua_job"].ToString();
                        if (gua1.Rows[0]["gua_job_sector_ind"].ToString() == "GOV")
                        {
                            RB1.Checked = true;
                            RB2.Checked = false;
                            RB3.Checked = false;
                            RB4.Checked = false;
                        }
                        else if (gua1.Rows[0]["gua_job_sector_ind"].ToString() == "GLC")
                        {
                            RB1.Checked = false;
                            RB2.Checked = true;
                            RB3.Checked = false;
                            RB4.Checked = false;
                        }
                        else if (gua1.Rows[0]["gua_job_sector_ind"].ToString() == "GLI")
                        {
                            RB1.Checked = false;
                            RB2.Checked = false;
                            RB3.Checked = true;
                            RB4.Checked = false;
                        }
                        else if (gua1.Rows[0]["gua_job_sector_ind"].ToString() == "BHD")
                        {
                            RB1.Checked = false;
                            RB2.Checked = false;
                            RB3.Checked = false;
                            RB4.Checked = true;
                        }
                        else if (gua1.Rows[0]["gua_job_sector_ind"].ToString() == "")
                        {
                            RB1.Checked = false;
                            RB2.Checked = false;
                            RB3.Checked = false;
                            RB4.Checked = false;
                        }
                        if (gua1.Rows[0]["gua_job_status_ind"].ToString() == "T")
                        {
                            RB11.Checked = true;
                            RB12.Checked = false;
                        }
                        else if (gua1.Rows[0]["gua_job_status_ind"].ToString() == "K")
                        {
                            RB11.Checked = false;
                            RB12.Checked = true;
                        }

                        decimal net_amt2 = (decimal)gua1.Rows[0]["gua_nett_income"];
                        TextBox31.Text = net_amt2.ToString("C").Replace("RM", "").Replace("$", "");
                        decimal gross_amt2 = (decimal)gua1.Rows[0]["gua_gross_income"];
                        TextBox33.Text = gross_amt2.ToString("C").Replace("RM", "").Replace("$", "");


                        TextBox34.Text = gua1.Rows[0]["gua_employer_name"].ToString();
                        TextBox30.Text = gua1.Rows[0]["gua_employer_phone"].ToString();
                        TextBox32.Text = gua1.Rows[0]["gua_employer_phone_ext"].ToString();
                    }


                    gua2 = DBCon.Ora_Execute_table("select * from jpa_guarantor where gua_applcn_no='" + Applcn_no.Text + "' and gua_seq_no='2'  order by gua_seq_no");
                    if (gua2.Rows.Count != 0)
                    {
                        TextBox36.Text = gua2.Rows[0]["gua_name"].ToString();
                        TextBox37.Text = gua2.Rows[0]["gua_phone"].ToString();
                        TextArea5.Value = gua2.Rows[0]["gua_permanent_address"].ToString();
                        TextArea6.Value = gua2.Rows[0]["gua_mailing_address"].ToString();
                        TextBox60.Text = gua2.Rows[0]["gua_permanent_postcode"].ToString();
                        TextBox61.Text = gua2.Rows[0]["gua_mailing_postcode"].ToString();
                        DD_NegriBind7.SelectedValue = gua2.Rows[0]["gua_permanent_state_cd"].ToString();
                        DD_NegriBind8.SelectedValue = gua2.Rows[0]["gua_mailing_state_cd"].ToString();
                        DD_Pengenalan3.SelectedValue = gua2.Rows[0]["gua_ic_type_cd"].ToString();
                        TextBox38.Text = gua2.Rows[0]["gua_icno"].ToString();
                        TextBox39.Text = gua2.Rows[0]["gua_job"].ToString();
                        if (gua2.Rows[0]["gua_job_sector_ind"].ToString() == "GOV")
                        {
                            RB21.Checked = true;
                            RB22.Checked = false;
                            RB23.Checked = false;
                            RB24.Checked = false;
                        }
                        else if (gua2.Rows[0]["gua_job_sector_ind"].ToString() == "GLC")
                        {
                            RB21.Checked = false;
                            RB22.Checked = true;
                            RB23.Checked = false;
                            RB24.Checked = false;
                        }
                        else if (gua2.Rows[0]["gua_job_sector_ind"].ToString() == "GLI")
                        {
                            RB21.Checked = false;
                            RB22.Checked = false;
                            RB23.Checked = true;
                            RB24.Checked = false;
                        }
                        else if (gua2.Rows[0]["gua_job_sector_ind"].ToString() == "BHD")
                        {
                            RB21.Checked = false;
                            RB22.Checked = false;
                            RB23.Checked = false;
                            RB24.Checked = true;
                        }
                        else if (gua2.Rows[0]["gua_job_sector_ind"].ToString() == "")
                        {
                            RB21.Checked = false;
                            RB22.Checked = false;
                            RB23.Checked = false;
                            RB24.Checked = false;
                        }
                        if (gua2.Rows[0]["gua_job_status_ind"].ToString() == "T")
                        {
                            TJ21.Checked = true;
                            TJ22.Checked = false;
                        }
                        else if (gua2.Rows[0]["gua_job_status_ind"].ToString() == "K")
                        {
                            TJ21.Checked = false;
                            TJ22.Checked = true;
                        }
                        decimal net_amt2 = (decimal)gua2.Rows[0]["gua_nett_income"];
                        TextBox40.Text = net_amt2.ToString("C").Replace("RM", "").Replace("$", "");
                        decimal gross_amt2 = (decimal)gua2.Rows[0]["gua_gross_income"];
                        TextBox41.Text = gross_amt2.ToString("C").Replace("RM", "").Replace("$", "");

                        TextBox42.Text = gua2.Rows[0]["gua_employer_name"].ToString();
                        TextBox43.Text = gua2.Rows[0]["gua_employer_phone"].ToString();
                        TextBox44.Text = gua2.Rows[0]["gua_employer_phone_ext"].ToString();
                    }
                }
                else if (gu1.Rows.Count == 3)
                {


                    gua1 = DBCon.Ora_Execute_table("select gua_name,gua_phone,gua_permanent_address,gua_permanent_postcode,gua_permanent_state_cd,gua_mailing_address,gua_mailing_postcode,gua_mailing_state_cd,gua_ic_type_cd,gua_icno,gua_job,gua_job_sector_ind,gua_job_status_ind,gua_nett_income,gua_gross_income,gua_employer_name,gua_employer_phone,gua_employer_phone_ext from jpa_guarantor where gua_applcn_no='" + Applcn_no.Text + "' and gua_seq_no='1'  order by gua_seq_no");
                    if (gua1.Rows.Count != 0)
                    {
                        TextBox27.Text = gua1.Rows[0]["gua_name"].ToString();
                        TextBox35.Text = gua1.Rows[0]["gua_phone"].ToString();
                        TextArea7.Value = gua1.Rows[0]["gua_permanent_address"].ToString();
                        TextArea8.Value = gua1.Rows[0]["gua_mailing_address"].ToString();
                        TextArea7.Value = gua1.Rows[0]["gua_permanent_address"].ToString();
                        TextBox62.Text = gua1.Rows[0]["gua_permanent_postcode"].ToString();
                        TextBox63.Text = gua1.Rows[0]["gua_mailing_postcode"].ToString();
                        DD_NegriBind5.SelectedValue = gua1.Rows[0]["gua_permanent_state_cd"].ToString();
                        DD_NegriBind6.SelectedValue = gua1.Rows[0]["gua_mailing_state_cd"].ToString();
                        DD_Pengenalan2.SelectedValue = gua1.Rows[0]["gua_ic_type_cd"].ToString();
                        TextBox28.Text = gua1.Rows[0]["gua_icno"].ToString();
                        TextBox29.Text = gua1.Rows[0]["gua_job"].ToString();
                        if (gua1.Rows[0]["gua_job_sector_ind"].ToString() == "GOV")
                        {
                            RB1.Checked = true;
                            RB2.Checked = false;
                            RB3.Checked = false;
                            RB4.Checked = false;
                        }
                        else if (gua1.Rows[0]["gua_job_sector_ind"].ToString() == "GLC")
                        {
                            RB1.Checked = false;
                            RB2.Checked = true;
                            RB3.Checked = false;
                            RB4.Checked = false;
                        }
                        else if (gua1.Rows[0]["gua_job_sector_ind"].ToString() == "GLI")
                        {
                            RB1.Checked = false;
                            RB2.Checked = false;
                            RB3.Checked = true;
                            RB4.Checked = false;
                        }
                        else if (gua1.Rows[0]["gua_job_sector_ind"].ToString() == "BHD")
                        {
                            RB1.Checked = false;
                            RB2.Checked = false;
                            RB3.Checked = false;
                            RB4.Checked = true;
                        }
                        else if (gua1.Rows[0]["gua_job_sector_ind"].ToString() == "")
                        {
                            RB1.Checked = false;
                            RB2.Checked = false;
                            RB3.Checked = false;
                            RB4.Checked = false;
                        }
                        if (gua1.Rows[0]["gua_job_status_ind"].ToString() == "T")
                        {
                            RB11.Checked = true;
                            RB12.Checked = false;
                        }
                        else if (gua1.Rows[0]["gua_job_status_ind"].ToString() == "K")
                        {
                            RB11.Checked = false;
                            RB12.Checked = true;
                        }
                        decimal net_amt2 = (decimal)gua1.Rows[0]["gua_nett_income"];
                        TextBox31.Text = net_amt2.ToString("C").Replace("RM", "").Replace("$", "");
                        decimal gross_amt2 = (decimal)gua1.Rows[0]["gua_gross_income"];
                        TextBox33.Text = gross_amt2.ToString("C").Replace("RM", "").Replace("$", "");

                        TextBox34.Text = gua1.Rows[0]["gua_employer_name"].ToString();
                        TextBox30.Text = gua1.Rows[0]["gua_employer_phone"].ToString();
                        TextBox32.Text = gua1.Rows[0]["gua_employer_phone_ext"].ToString();
                    }



                    gua2 = DBCon.Ora_Execute_table("select * from jpa_guarantor where gua_applcn_no='" + Applcn_no.Text + "' and gua_seq_no='2'  order by gua_seq_no");
                    if (gua2.Rows.Count != 0)
                    {
                        TextBox36.Text = gua2.Rows[0]["gua_name"].ToString();
                        TextBox37.Text = gua2.Rows[0]["gua_phone"].ToString();
                        TextArea5.Value = gua2.Rows[0]["gua_permanent_address"].ToString();
                        TextArea6.Value = gua2.Rows[0]["gua_mailing_address"].ToString();
                        TextBox60.Text = gua2.Rows[0]["gua_permanent_postcode"].ToString();
                        TextBox61.Text = gua2.Rows[0]["gua_mailing_postcode"].ToString();
                        DD_NegriBind7.SelectedValue = gua2.Rows[0]["gua_permanent_state_cd"].ToString();
                        DD_NegriBind8.SelectedValue = gua2.Rows[0]["gua_mailing_state_cd"].ToString();
                        DD_Pengenalan3.SelectedValue = gua2.Rows[0]["gua_ic_type_cd"].ToString();
                        TextBox38.Text = gua2.Rows[0]["gua_icno"].ToString();
                        TextBox39.Text = gua2.Rows[0]["gua_job"].ToString();
                        if (gua2.Rows[0]["gua_job_sector_ind"].ToString() == "GOV")
                        {
                            RB21.Checked = true;
                            RB22.Checked = false;
                            RB23.Checked = false;
                            RB24.Checked = false;
                        }
                        else if (gua2.Rows[0]["gua_job_sector_ind"].ToString() == "GLC")
                        {
                            RB21.Checked = false;
                            RB22.Checked = true;
                            RB23.Checked = false;
                            RB24.Checked = false;
                        }
                        else if (gua2.Rows[0]["gua_job_sector_ind"].ToString() == "GLI")
                        {
                            RB21.Checked = false;
                            RB22.Checked = false;
                            RB23.Checked = true;
                            RB24.Checked = false;
                        }
                        else if (gua2.Rows[0]["gua_job_sector_ind"].ToString() == "BHD")
                        {
                            RB21.Checked = false;
                            RB22.Checked = false;
                            RB23.Checked = false;
                            RB24.Checked = true;
                        }
                        else if (gua2.Rows[0]["gua_job_sector_ind"].ToString() == "")
                        {
                            RB21.Checked = false;
                            RB22.Checked = false;
                            RB23.Checked = false;
                            RB24.Checked = false;
                        }
                        if (gua2.Rows[0]["gua_job_status_ind"].ToString() == "T")
                        {
                            TJ21.Checked = true;
                            TJ22.Checked = false;
                        }
                        else if (gua2.Rows[0]["gua_job_status_ind"].ToString() == "K")
                        {
                            TJ21.Checked = false;
                            TJ22.Checked = true;
                        }
                        decimal net_amt2 = (decimal)gua2.Rows[0]["gua_nett_income"];
                        TextBox40.Text = net_amt2.ToString("C").Replace("RM", "").Replace("$", "");
                        decimal gross_amt2 = (decimal)gua2.Rows[0]["gua_gross_income"];
                        TextBox41.Text = gross_amt2.ToString("C").Replace("RM", "").Replace("$", "");

                        TextBox42.Text = gua2.Rows[0]["gua_employer_name"].ToString();
                        TextBox43.Text = gua2.Rows[0]["gua_employer_phone"].ToString();
                        TextBox44.Text = gua2.Rows[0]["gua_employer_phone_ext"].ToString();
                    }

                    //gua3 = DBCon.Ora_Execute_table("select * from jpa_guarantor where gua_applcn_no='" + Applcn_no.Text + "' and gua_seq_no='3'  order by gua_seq_no");
                    //if (gua3.Rows.Count != 0)
                    //{
                    //    TextBox45.Text = gua3.Rows[0]["gua_name"].ToString();
                    //    TextBox46.Text = gua3.Rows[0]["gua_phone"].ToString();
                    //    TextArea9.Value = gua3.Rows[0]["gua_permanent_address"].ToString();
                    //    TextArea10.Value = gua3.Rows[0]["gua_mailing_address"].ToString();
                    //    TextBox58.Text = gua3.Rows[0]["gua_permanent_postcode"].ToString();
                    //    TextBox59.Text = gua3.Rows[0]["gua_mailing_postcode"].ToString();
                    //    DD_NegriBind9.SelectedValue = gua3.Rows[0]["gua_permanent_state_cd"].ToString();
                    //    DD_NegriBind10.SelectedValue = gua3.Rows[0]["gua_mailing_state_cd"].ToString();
                    //    DD_Pengenalan4.SelectedValue = gua3.Rows[0]["gua_ic_type_cd"].ToString();
                    //    TextBox47.Text = gua3.Rows[0]["gua_icno"].ToString();
                    //    TextBox48.Text = gua3.Rows[0]["gua_job"].ToString();
                    //    if (gua3.Rows[0]["gua_job_sector_ind"].ToString() == "GOV")
                    //    {
                    //        RB31.Checked = true;
                    //        RB32.Checked = false;
                    //        RB33.Checked = false;
                    //        RB34.Checked = false;
                    //    }
                    //    else if (gua3.Rows[0]["gua_job_sector_ind"].ToString() == "GLC")
                    //    {
                    //        RB31.Checked = false;
                    //        RB32.Checked = true;
                    //        RB33.Checked = false;
                    //        RB34.Checked = false;
                    //    }
                    //    else if (gua3.Rows[0]["gua_job_sector_ind"].ToString() == "GLI")
                    //    {
                    //        RB31.Checked = false;
                    //        RB32.Checked = false;
                    //        RB33.Checked = true;
                    //        RB34.Checked = false;
                    //    }
                    //    else if (gua3.Rows[0]["gua_job_sector_ind"].ToString() == "")
                    //    {
                    //        RB31.Checked = false;
                    //        RB32.Checked = false;
                    //        RB33.Checked = false;
                    //        RB34.Checked = false;
                    //    }

                    //    if (gua3.Rows[0]["gua_job_status_ind"].ToString() == "T")
                    //    {
                    //        TJ31.Checked = true;
                    //        TJ32.Checked = false;
                    //    }
                    //    else if (gua3.Rows[0]["gua_job_status_ind"].ToString() == "K")
                    //    {
                    //        TJ31.Checked = false;
                    //        TJ32.Checked = true;
                    //    }
                    //    decimal net_amt2 = (decimal)gua3.Rows[0]["gua_nett_income"];
                    //    TextBox49.Text = net_amt2.ToString("C").Replace("RM", "").Replace("$", "");
                    //    decimal gross_amt2 = (decimal)gua3.Rows[0]["gua_gross_income"];
                    //    TextBox50.Text = gross_amt2.ToString("C").Replace("RM", "").Replace("$", "");

                    //    TextBox51.Text = gua3.Rows[0]["gua_employer_name"].ToString();
                    //    TextBox52.Text = gua3.Rows[0]["gua_employer_phone"].ToString();
                    //    TextBox53.Text = gua3.Rows[0]["gua_employer_phone_ext"].ToString();
                    //}
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Ic OR NO KP Baru',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Semak Isu',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }

    protected void clk_update(object sender, EventArgs e)
    {
        if (Applcn_no.Text != "")
        {
            var ic_count = Applcn_no.Text.Length;
            DataTable app_icno = new DataTable();
            app_icno = DBCon.Ora_Execute_table("select app_applcn_no from jpa_application JA where '" + Applcn_no.Text + "' IN(JA.app_applcn_no, JA.app_new_icno)");
            if (ic_count == 12)
            {
                cc_no = app_icno.Rows[0]["app_applcn_no"].ToString();
            }
            else
            {
                cc_no = Applcn_no.Text;
            }
            if (Textarea1.Value != "" && txttn16d.Text != "" && DropDownList2.SelectedValue != "" && Textarea2.Value != "" && txttp.Text != "" && DropDownList1.SelectedValue != "" && TextBox12.Text != "" && TextBox14.Text != "")
            {
                DataTable app_icno_rel = new DataTable();
                app_icno_rel = DBCon.Ora_Execute_table("select count(*) as cnt from jpa_relative where rel_applcn_no='" + app_icno.Rows[0]["app_applcn_no"].ToString() + "'");

                string upd_application = "update jpa_application set app_permnt_address='" + Textarea1.Value + "',app_permnt_postcode='" + txttn16d.Text + "',app_permnt_state_cd='" + DropDownList2.SelectedValue + "',app_phone_h='" + TextBox10.Text + "',app_phone_m='" + TextBox8.Text + "',app_phone_o='" + TextBox11.Text + "',app_mailing_address='" + Textarea2.Value + "',app_mailing_postcode='" + txttp.Text + "',app_mailing_state_cd='" + DropDownList1.SelectedValue + "' where app_applcn_no='" + cc_no + "'";
                Status = Dblog.Ora_Execute_CommamdText(upd_application);
                string upd_rel1 = "UPDATE jpa_relative SET rel_new_icno='" + TextBox13.Text + "',rel_name='" + TextBox12.Text.Trim().ToUpper() + "',rel_phone='" + TextBox14.Text + "',rel_relation_cd='" + DropDownList4.SelectedValue + "' where rel_applcn_no='" + cc_no + "' and rel_seq_no='1'";
                Status = Dblog.Ora_Execute_CommamdText(upd_rel1);
                if (app_icno_rel.Rows[0]["cnt"].ToString() == "2")
                {
                    string upd_rel2 = "UPDATE jpa_relative SET rel_new_icno='" + TextBox16.Text + "',rel_name='" + TextBox15.Text.Trim().ToUpper() + "',rel_phone='" + TextBox17.Text + "',rel_relation_cd='" + DropDownList3.SelectedValue + "' where rel_applcn_no='" + cc_no + "' and rel_seq_no='2'";
                    Status = Dblog.Ora_Execute_CommamdText(upd_rel2);
                }
                if (app_icno_rel.Rows[0]["cnt"].ToString() == "1" && TextBox15.Text != "" && TextBox17.Text != "")
                {
                    string upd_rel2 = "INSERT INTO jpa_relative (rel_applcn_no,rel_seq_no,rel_new_icno,rel_name,rel_phone,rel_relation_cd,Created_date) VALUES ('" + app_icno.Rows[0]["app_applcn_no"].ToString() + "','2','" + TextBox16.Text + "','" + TextBox15.Text.Replace("'", "''").ToUpper() + "','" + TextBox17.Text + "','" + DropDownList3.SelectedValue + "','" + DateTime.Now + "')";
                    Status = Dblog.Ora_Execute_CommamdText(upd_rel2);
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Red Mark Field Should be Mandatory',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila mesti Masukkan Kata-kata',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    protected void btn_rstclick(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void clk_log_tugasan(object sender, EventArgs e)
    {
        if (Applcn_no.Text != "")
        {
            var ic_count = Applcn_no.Text.Length;
            DataTable app_icno = new DataTable();
            app_icno = DBCon.Ora_Execute_table("select app_applcn_no from jpa_application JA where JA.app_new_icno= '" + Applcn_no.Text + "'");
            if (ic_count == 12)
            {
                cc_no = app_icno.Rows[0]["app_applcn_no"].ToString();
            }
            else
            {
                cc_no = Applcn_no.Text;
            }

            Response.Redirect("PP_LOG_TUGASAN.aspx?spi_icno=" + cc_no);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila mesti Masukkan Kata-kata',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }

    }

    protected void clk_Bercagar(object sender, EventArgs e)
    {
        if (Applcn_no.Text != "")
        {
            var ic_count = Applcn_no.Text.Length;
            DataTable app_icno = new DataTable();
            app_icno = DBCon.Ora_Execute_table("select app_applcn_no from jpa_application JA where JA.app_applcn_no= '" + Applcn_no.Text + "'");
            if (ic_count == 12)
            {
                cc_no = app_icno.Rows[0]["app_applcn_no"].ToString();
            }
            else
            {
                cc_no = Applcn_no.Text;
            }
            Response.Redirect("../Pelaburan_Anggota/PP_Guaman_cagaran.aspx?spi_icno=" + cc_no);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila mesti Masukkan Kata-kata',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }

    }

    protected void clk_Penjamin(object sender, EventArgs e)
    {
        if (Applcn_no.Text != "")
        {
            var ic_count = Applcn_no.Text.Length;
            DataTable app_icno = new DataTable();
            app_icno = DBCon.Ora_Execute_table("select app_applcn_no from jpa_application JA where JA.app_applcn_no= '" + Applcn_no.Text + "'");
            if (ic_count == 12)
            {
                cc_no = app_icno.Rows[0]["app_applcn_no"].ToString();
            }
            else
            {
                cc_no = Applcn_no.Text;
            }
            Response.Redirect("../Pelaburan_Anggota/PP_Guaman_penjamin.aspx?spi_icno=" + cc_no);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila mesti Masukkan Kata-kata',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }

    }

    protected void click_jbb(object sender, EventArgs e)
    {

        try
        {
            if (Applcn_no.Text != "")
            {
                string url = "../Pelaburan_Anggota/semak_jbb.aspx?jbb_icno=" + Applcn_no.Text;

                string s = "window.open('" + url + "', 'popup_window', 'width=11000,height=700,left=100,top=100,resizable=yes');";

                ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Ic OR NO KP Baru',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void click_batal(object sender, EventArgs e)
    {
        if (Session["sess_fdt"].ToString() != "" && Session["sess_tdt"].ToString() != "" && Session["sess_jp"].ToString() != "" && Session["sess_sp"].ToString() != "" && Session["sess_fdt"].ToString() != null && Session["sess_tdt"].ToString() != null && Session["sess_jp"].ToString() != null && Session["sess_sp"].ToString() != null)
        {
            Response.Redirect("../Pelaburan_Anggota/PP_MAK_PEMBIAYAAN.aspx");
        }

    }

    protected void clk_cetak(object sender, EventArgs e)
    {
        if (Applcn_no.Text != "")
        {
            var ic_count = Applcn_no.Text.Length;
            DataTable app_icno = new DataTable();
            app_icno = DBCon.Ora_Execute_table("select app_applcn_no from jpa_application JA where JA.app_new_icno= '" + Applcn_no.Text + "'");
            if (ic_count == 12)
            {
                cc_no = app_icno.Rows[0]["app_applcn_no"].ToString();
            }
            else
            {
                cc_no = Applcn_no.Text;
            }


            //Path
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            //dt = DBCon.Ora_Execute_table("select JA.app_applcn_no,JA.app_new_icno,JA.app_name,JA.app_apply_amt,JA.app_apply_dur,RW.Wilayah_Name,RB.branch_desc,RT.tujuan_desc,RJ1.Jawatan_desc as desc1,RJ2.Jawatan_desc as desc2,RJ3.Jawatan_desc as desc3,JJA.* from jpa_application as JA Left join jpa_jkkpa_approval as JJA ON JJA.jkk_applcn_no=JA.app_applcn_no left join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd left join ref_branch as RB ON RB.branch_cd=JA.app_branch_cd left join ref_tujuan as RT ON RT.tujuan_cd = JA.app_loan_purpose_cd left join ref_jawatan as RJ1 on RJ1.Jawatan_Code=JJA.jkk_post1 left join ref_jawatan as RJ2 on RJ2.Jawatan_Code=JJA.jkk_post2 left join ref_jawatan as RJ3 on RJ3.Jawatan_Code=JJA.jkk_post3 where JA.app_new_icno='" + Icno.Text + "'");
            dt = DBCon.Ora_Execute_table("select app_applcn_no from jpa_application JA where JA.app_applcn_no='" + cc_no + "'");
            DataTable select_app_rt = new DataTable();
            select_app_rt = DBCon.Ora_Execute_table("select *,rh.Contact_Name from (select JA.app_applcn_no,JA.app_new_icno,JA.app_name,RW.Wilayah_Name,ISNULL(RB.branch_desc, '') AS branch_desc,JA.app_loan_amt,JA.appl_loan_dur,JA.app_permnt_address,ja.app_permnt_postcode,ja.app_permnt_state_cd,ja.app_phone_h,ja.app_phone_m,ja.app_phone_o,ja.app_mailing_address,JA.app_mailing_postcode,ja.app_mailing_state_cd,ISNULL(JA.app_cumm_installment_amt,'') as app_cumm_installment_amt,ISNULL(JA.app_cumm_pay_amt,'') as app_cumm_pay_amt,ISNULL(JA.app_backdated_amt,'') as app_backdated_amt,ISNULL(JA.app_cumm_profit_amt,'') as app_cumm_profit_amt,ISNULL(JA.app_cumm_saving_amt,'') as app_cumm_saving_amt,ISNULL(JA.app_bal_loan_amt,'') as app_bal_loan_amt,ISNULL(JA.app_current_jbb_ind,'') as app_current_jbb_ind,ISNULL(CASE WHEN CONVERT(DATE, app_end_pay_dt) = '1900-01-01' THEN '' ELSE CONVERT(CHAR(10), app_end_pay_dt, 103) END, '') AS app_end_pay_dt,rn.Decription as pscd,rn1.Decription mscd from jpa_application as JA  Left Join jpa_calculate_fee as JC ON JC.cal_applcn_no=JA.app_applcn_no Left Join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd Left join ref_branch AS RB ON RB.branch_cd=JA.app_branch_cd left join Ref_Negeri as rn on rn.Decription_Code=ja.app_permnt_state_cd left join Ref_Negeri as rn1 on rn1.Decription_Code=ja.app_mailing_state_cd where JA.app_applcn_no = '" + cc_no + "') as a full outer join (select * from jpa_relative where rel_applcn_no='" + cc_no + "') as b left join Ref_Hubungan as rh on rh.Contact_Code=b.rel_relation_cd on a.app_applcn_no=b.rel_applcn_no");

            Rptviwer_kelulusan.Reset();
            ds.Tables.Add(dt);

            Rptviwer_kelulusan.LocalReport.DataSources.Clear();

            Rptviwer_kelulusan.LocalReport.ReportPath = "Pelaburan_Anggota/pp_smp.rdlc";
            ReportDataSource rds = new ReportDataSource("pp_smp", dt);

            decimal amt1 = (decimal)select_app_rt.Rows[0]["app_loan_amt"];

            decimal amt2 = 0M, amt3 = 0M, amt4 = 0M;
            string jbb_desc = string.Empty;
            if (select_app_rt.Rows[0]["app_backdated_amt"].ToString() != "0")
            {
                amt2 = (decimal)select_app_rt.Rows[0]["app_backdated_amt"];

                amt3 = (decimal)select_app_rt.Rows[0]["app_cumm_installment_amt"];
                if (amt2 == 0 && amt3 == 0)
                {
                    amt4 = 0;
                }
                else
                {
                    amt4 = amt2 / amt3;
                }

            }

            if (select_app_rt.Rows[0]["app_current_jbb_ind"].ToString() == "N")
            {
                jbb_desc = "NORMAL";
            }
            else if (select_app_rt.Rows[0]["app_current_jbb_ind"].ToString() == "P")
            {
                jbb_desc = "PJS";
            }
            else if (select_app_rt.Rows[0]["app_current_jbb_ind"].ToString() == "E")
            {
                jbb_desc = "PANJANG TEMPOH";
            }
            else if (select_app_rt.Rows[0]["app_current_jbb_ind"].ToString() == "H")
            {
                jbb_desc = "TANGGUH";
            }
            else if (select_app_rt.Rows[0]["app_current_jbb_ind"].ToString() == "L")
            {
                jbb_desc = "HAPUS KIRA";
            }
            else
            {
                jbb_desc = "";
            }

            decimal a1 = (decimal)select_app_rt.Rows[0]["app_cumm_installment_amt"];
            decimal a2 = (decimal)select_app_rt.Rows[0]["app_cumm_pay_amt"];
            decimal a3 = (decimal)select_app_rt.Rows[0]["app_backdated_amt"];
            decimal a4 = (decimal)select_app_rt.Rows[0]["app_cumm_profit_amt"];
            decimal a5 = (decimal)select_app_rt.Rows[0]["app_cumm_saving_amt"];
            decimal a6 = (decimal)select_app_rt.Rows[0]["app_bal_loan_amt"];

            string rel0 = string.Empty, rel1 = string.Empty, rel2 = string.Empty, rel3 = string.Empty;
            string rel01 = string.Empty, rel11 = string.Empty, rel21 = string.Empty, rel31 = string.Empty;
            if (select_app_rt.Rows.Count >= 1)
            {
                rel0 = select_app_rt.Rows[0]["rel_name"].ToString();
                rel1 = select_app_rt.Rows[0]["rel_new_icno"].ToString();
                rel2 = select_app_rt.Rows[0]["rel_phone"].ToString();
                rel3 = select_app_rt.Rows[0]["Contact_Name"].ToString();
            }
            else
            {
                rel0 = "";
                rel1 = "";
                rel2 = "";
                rel3 = "";
            }

            if (select_app_rt.Rows.Count >= 2)
            {
                rel01 = select_app_rt.Rows[1]["rel_name"].ToString();
                rel11 = select_app_rt.Rows[1]["rel_new_icno"].ToString();
                rel21 = select_app_rt.Rows[1]["rel_phone"].ToString();
                rel31 = select_app_rt.Rows[1]["Contact_Name"].ToString();
            }
            else
            {
                rel01 = "";
                rel11 = "";
                rel21 = "";
                rel31 = "";
            }

            DataTable select_app_rt_gua = new DataTable();
            select_app_rt_gua = DBCon.Ora_Execute_table("select *,rn.Decription as pscd,rn1.Decription as mscd,jp.Description as itype from jpa_guarantor as jg left join Ref_Negeri as rn on rn.Decription_Code=jg.gua_permanent_state_cd left join Ref_Negeri as rn1 on rn1.Decription_Code=jg.gua_mailing_state_cd left join Ref_Jenis_Pengenalan as jp on jp.Description_Code=jg.gua_ic_type_cd where gua_applcn_no='" + cc_no + "'  order by gua_seq_no");

            decimal net_amt2 = 0M, gross_amt2 = 0M, net_amt12 = 0M, gross_amt12 = 0M, net_amt22 = 0M, gross_amt22 = 0M;
            string gua1 = string.Empty, gua2 = string.Empty, gua3 = string.Empty, gua4 = string.Empty, gua5 = string.Empty, gua6 = string.Empty, gua7 = string.Empty, gua8 = string.Empty, gua9 = string.Empty, gua10 = string.Empty, gua11 = string.Empty, gua12 = string.Empty, gua13 = string.Empty, gua14 = string.Empty, gua15 = string.Empty, gua16 = string.Empty, gua17 = string.Empty, gua18 = string.Empty, gua19 = string.Empty, gua20 = string.Empty;
            string gua011 = string.Empty, gua012 = string.Empty, gua013 = string.Empty, gua014 = string.Empty, gua015 = string.Empty, gua016 = string.Empty, gua017 = string.Empty, gua018 = string.Empty, gua019 = string.Empty, gua110 = string.Empty, gua111 = string.Empty, gua112 = string.Empty, gua113 = string.Empty, gua114 = string.Empty, gua115 = string.Empty, gua116 = string.Empty, gua117 = string.Empty, gua118 = string.Empty, gua119 = string.Empty, gua120 = string.Empty;
            string gua121 = string.Empty, gua22 = string.Empty, gua23 = string.Empty, gua24 = string.Empty, gua25 = string.Empty, gua26 = string.Empty, gua27 = string.Empty, gua28 = string.Empty, gua29 = string.Empty, gua210 = string.Empty, gua211 = string.Empty, gua212 = string.Empty, gua213 = string.Empty, gua214 = string.Empty, gua215 = string.Empty, gua216 = string.Empty, gua217 = string.Empty, gua218 = string.Empty, gua219 = string.Empty, gua220 = string.Empty;
            if (select_app_rt_gua.Rows.Count >= 1)
            {

                gua1 = select_app_rt_gua.Rows[0]["gua_name"].ToString();
                gua2 = select_app_rt_gua.Rows[0]["gua_phone"].ToString();
                gua3 = select_app_rt_gua.Rows[0]["gua_permanent_address"].ToString();
                gua4 = select_app_rt_gua.Rows[0]["gua_mailing_address"].ToString();
                gua5 = select_app_rt_gua.Rows[0]["gua_permanent_address"].ToString();
                gua6 = select_app_rt_gua.Rows[0]["gua_permanent_postcode"].ToString();
                gua7 = select_app_rt_gua.Rows[0]["gua_mailing_postcode"].ToString();
                gua8 = select_app_rt_gua.Rows[0]["pscd"].ToString();
                gua9 = select_app_rt_gua.Rows[0]["mscd"].ToString();
                gua10 = select_app_rt_gua.Rows[0]["itype"].ToString();
                gua11 = select_app_rt_gua.Rows[0]["gua_icno"].ToString();
                gua12 = select_app_rt_gua.Rows[0]["gua_job"].ToString();
                if (select_app_rt_gua.Rows[0]["gua_job_sector_ind"].ToString() == "GOV")
                {
                    gua13 = "Agensi / Jabatan / Kementerian Kerajaan Malaysia";
                }
                else if (select_app_rt_gua.Rows[0]["gua_job_sector_ind"].ToString() == "GLC")
                {
                    gua13 = "Syarikat Berkaitan Kerajaan (GLC)";
                }
                else if (select_app_rt_gua.Rows[0]["gua_job_sector_ind"].ToString() == "GLI")
                {
                    gua13 = "Syarikat Pelaburan Berkaitan Kerajaan (GLIC)";
                }
                else if (select_app_rt_gua.Rows[0]["gua_job_sector_ind"].ToString() == "BHD")
                {
                    gua13 = "Syarikat Berstatus Berhad";
                }
                else if (select_app_rt_gua.Rows[0]["gua_job_sector_ind"].ToString() == "")
                {
                    gua13 = "";
                }
                if (select_app_rt_gua.Rows[0]["gua_job_status_ind"].ToString() == "T")
                {
                    gua14 = "Tetap";
                }
                else if (select_app_rt_gua.Rows[0]["gua_job_status_ind"].ToString() == "K")
                {
                    gua14 = "Kontrak";
                }
                else
                {
                    gua14 = "";
                }
                net_amt2 = (decimal)select_app_rt_gua.Rows[0]["gua_nett_income"];

                gross_amt2 = (decimal)select_app_rt_gua.Rows[0]["gua_gross_income"];

                gua15 = select_app_rt_gua.Rows[0]["gua_employer_name"].ToString();
                gua16 = select_app_rt_gua.Rows[0]["gua_employer_phone"].ToString();
                gua17 = select_app_rt_gua.Rows[0]["gua_employer_phone_ext"].ToString();
            }

            if (select_app_rt_gua.Rows.Count >= 2)
            {
                gua011 = select_app_rt_gua.Rows[1]["gua_name"].ToString();
                gua012 = select_app_rt_gua.Rows[1]["gua_phone"].ToString();
                gua013 = select_app_rt_gua.Rows[1]["gua_permanent_address"].ToString();
                gua014 = select_app_rt_gua.Rows[1]["gua_mailing_address"].ToString();
                gua015 = select_app_rt_gua.Rows[1]["gua_permanent_address"].ToString();
                gua016 = select_app_rt_gua.Rows[1]["gua_permanent_postcode"].ToString();
                gua017 = select_app_rt_gua.Rows[1]["gua_mailing_postcode"].ToString();
                gua018 = select_app_rt_gua.Rows[1]["pscd"].ToString();
                gua019 = select_app_rt_gua.Rows[1]["mscd"].ToString();
                gua110 = select_app_rt_gua.Rows[1]["itype"].ToString();
                gua111 = select_app_rt_gua.Rows[1]["gua_icno"].ToString();
                gua112 = select_app_rt_gua.Rows[1]["gua_job"].ToString();
                if (select_app_rt_gua.Rows[1]["gua_job_sector_ind"].ToString() == "GOV")
                {
                    gua113 = "Agensi / Jabatan / Kementerian Kerajaan Malaysia";
                }
                else if (select_app_rt_gua.Rows[1]["gua_job_sector_ind"].ToString() == "GLC")
                {
                    gua113 = "Syarikat Berkaitan Kerajaan (GLC)";
                }
                else if (select_app_rt_gua.Rows[1]["gua_job_sector_ind"].ToString() == "GLI")
                {
                    gua113 = "Syarikat Pelaburan Berkaitan Kerajaan (GLIC)";
                }
                else if (select_app_rt_gua.Rows[1]["gua_job_sector_ind"].ToString() == "BHD")
                {
                    gua113 = "Syarikat Berstatus Berhad";
                }
                else if (select_app_rt_gua.Rows[1]["gua_job_sector_ind"].ToString() == "")
                {
                    gua113 = "";
                }
                if (select_app_rt_gua.Rows[1]["gua_job_status_ind"].ToString() == "T")
                {
                    gua114 = "Tetap";
                }
                else if (select_app_rt_gua.Rows[1]["gua_job_status_ind"].ToString() == "K")
                {
                    gua114 = "Kontrak";
                }
                else
                {
                    gua114 = "";
                }
                net_amt12 = (decimal)select_app_rt_gua.Rows[1]["gua_nett_income"];

                gross_amt12 = (decimal)select_app_rt_gua.Rows[1]["gua_gross_income"];

                gua115 = select_app_rt_gua.Rows[1]["gua_employer_name"].ToString();
                gua116 = select_app_rt_gua.Rows[1]["gua_employer_phone"].ToString();
                gua117 = select_app_rt_gua.Rows[1]["gua_employer_phone_ext"].ToString();

            }


            if (select_app_rt_gua.Rows.Count == 3)
            {
                gua121 = select_app_rt_gua.Rows[2]["gua_name"].ToString();
                gua22 = select_app_rt_gua.Rows[2]["gua_phone"].ToString();
                gua23 = select_app_rt_gua.Rows[2]["gua_permanent_address"].ToString();
                gua24 = select_app_rt_gua.Rows[2]["gua_mailing_address"].ToString();
                gua25 = select_app_rt_gua.Rows[2]["gua_permanent_address"].ToString();
                gua26 = select_app_rt_gua.Rows[2]["pscd"].ToString();
                gua27 = select_app_rt_gua.Rows[2]["mscd"].ToString();
                gua28 = select_app_rt_gua.Rows[2]["gua_permanent_state_cd"].ToString();
                gua29 = select_app_rt_gua.Rows[2]["gua_mailing_state_cd"].ToString();
                gua210 = select_app_rt_gua.Rows[2]["itype"].ToString();
                gua211 = select_app_rt_gua.Rows[2]["gua_icno"].ToString();
                gua212 = select_app_rt_gua.Rows[2]["gua_job"].ToString();
                if (select_app_rt_gua.Rows[2]["gua_job_sector_ind"].ToString() == "GOV")
                {
                    gua213 = "Agensi / Jabatan / Kementerian Kerajaan Malaysia";
                }
                else if (select_app_rt_gua.Rows[2]["gua_job_sector_ind"].ToString() == "GLC")
                {
                    gua213 = "Syarikat Berkaitan Kerajaan (GLC)";
                }
                else if (select_app_rt_gua.Rows[2]["gua_job_sector_ind"].ToString() == "GLI")
                {
                    gua213 = "Syarikat Pelaburan Berkaitan Kerajaan (GLIC)";
                }
                else if (select_app_rt_gua.Rows[2]["gua_job_sector_ind"].ToString() == "BHD")
                {
                    gua213 = "Syarikat Berstatus Berhad";
                }
                else if (select_app_rt_gua.Rows[2]["gua_job_sector_ind"].ToString() == "")
                {
                    gua213 = "";
                }
                if (select_app_rt_gua.Rows[2]["gua_job_status_ind"].ToString() == "T")
                {
                    gua214 = "Tetap";
                }
                else if (select_app_rt_gua.Rows[2]["gua_job_status_ind"].ToString() == "K")
                {
                    gua214 = "Kontrak";
                }
                else
                {
                    gua214 = "";
                }
                net_amt22 = (decimal)select_app_rt_gua.Rows[2]["gua_nett_income"];

                gross_amt22 = (decimal)select_app_rt_gua.Rows[2]["gua_gross_income"];

                gua215 = select_app_rt_gua.Rows[2]["gua_employer_name"].ToString();
                gua216 = select_app_rt_gua.Rows[2]["gua_employer_phone"].ToString();
                gua217 = select_app_rt_gua.Rows[2]["gua_employer_phone_ext"].ToString();
            }


            ReportParameter[] rptParams = new ReportParameter[]{
                new ReportParameter("appno", select_app_rt.Rows[0]["app_applcn_no"].ToString()),
                new ReportParameter("appname", select_app_rt.Rows[0]["app_name"].ToString()),
                     new ReportParameter("wilname", select_app_rt.Rows[0]["Wilayah_Name"].ToString()),
                     new ReportParameter("ln_amt", amt1.ToString("C").Replace("$","")),
                     new ReportParameter("cawname", select_app_rt.Rows[0]["branch_desc"].ToString()),
                     new ReportParameter("ln_dur", select_app_rt.Rows[0]["appl_loan_dur"].ToString()),
                     new ReportParameter("pt_address", select_app_rt.Rows[0]["app_permnt_address"].ToString()),
                     new ReportParameter("mg_address", select_app_rt.Rows[0]["app_mailing_address"].ToString()),
                     new ReportParameter("pt_pcode", select_app_rt.Rows[0]["app_permnt_postcode"].ToString()),
                     new ReportParameter("mg_pcode", select_app_rt.Rows[0]["app_mailing_postcode"].ToString()),
                     new ReportParameter("pt_stcode", select_app_rt.Rows[0]["pscd"].ToString()),
                     new ReportParameter("mg_stcode", select_app_rt.Rows[0]["mscd"].ToString()),
                     new ReportParameter("h_phone", select_app_rt.Rows[0]["app_phone_h"].ToString()),
                     new ReportParameter("m_phone", select_app_rt.Rows[0]["app_phone_m"].ToString()),
                     new ReportParameter("o_phone", select_app_rt.Rows[0]["app_phone_o"].ToString()),
                     new ReportParameter("bulan_tngg", amt4.ToString("C").Replace("$","")),
                     new ReportParameter("jbbdesc", jbb_desc),
                     new ReportParameter("end_pdt", select_app_rt.Rows[0]["app_end_pay_dt"].ToString()),
                     new ReportParameter("cumm_insamt", a1.ToString("C").Replace("$","")),
                     new ReportParameter("cum_pamt", a2.ToString("C").Replace("$","")),
                     new ReportParameter("bdateed_amt", a3.ToString("C").Replace("$","")),
                     new ReportParameter("cumm_proamt", a4.ToString("C").Replace("$","")),
                     new ReportParameter("cumm_savamt", a5.ToString("C").Replace("$","")),
                     new ReportParameter("bal_lnamt", a6.ToString("C").Replace("$","")),
                     new ReportParameter("r_name1", rel0),
                     new ReportParameter("r_icno1", rel1),
                     new ReportParameter("r_phone1", rel2),
                     new ReportParameter("r_rcd1", rel3),
                     new ReportParameter("r_name2", rel01),
                     new ReportParameter("r_icno2", rel11),
                     new ReportParameter("r_phone2", rel21),
                     new ReportParameter("r_rcd2", rel31),
                     //gua1
                     new ReportParameter("g1", gua1),
                     new ReportParameter("g2", gua2),
                     new ReportParameter("g3", gua3),
                     new ReportParameter("g4", gua4),
                     new ReportParameter("g5", gua5),
                     new ReportParameter("g6", gua6),
                     new ReportParameter("g7", gua7),
                     new ReportParameter("g8", gua8),
                     new ReportParameter("g9", gua9),
                     new ReportParameter("g10", gua10),
                     new ReportParameter("g11", gua11),
                     new ReportParameter("g12", gua12),
                     new ReportParameter("g13", gua13),
                     new ReportParameter("g14", gua14),
                     new ReportParameter("g15", net_amt2.ToString("C").Replace("$","")),
                     new ReportParameter("g16", gross_amt2.ToString("C").Replace("$","")),
                     new ReportParameter("g17", gua15),
                     new ReportParameter("g18", gua16),
                     new ReportParameter("g19", gua17),

                     //gua2
                     new ReportParameter("g011", gua011),
                     new ReportParameter("g012", gua012),
                     new ReportParameter("g013", gua013),
                     new ReportParameter("g014", gua014),
                     new ReportParameter("g015", gua015),
                     new ReportParameter("g016", gua016),
                     new ReportParameter("g017", gua017),
                     new ReportParameter("g018", gua018),
                     new ReportParameter("g019", gua019),
                     new ReportParameter("g0110", gua110),
                     new ReportParameter("g0111", gua111),
                     new ReportParameter("g0112", gua112),
                     new ReportParameter("g0113", gua113),
                     new ReportParameter("g0114", gua114),
                     new ReportParameter("g0115", net_amt12.ToString("C").Replace("$","")),
                     new ReportParameter("g0116", gross_amt12.ToString("C").Replace("$","")),
                     new ReportParameter("g0117", gua115),
                     new ReportParameter("g0118", gua116),
                     new ReportParameter("g0119", gua117),

                     //gua3
                     new ReportParameter("g021", gua121),
                     new ReportParameter("g022", gua22),
                     new ReportParameter("g023", gua23),
                     new ReportParameter("g024", gua24),
                     new ReportParameter("g025", gua25),
                     new ReportParameter("g026", gua26),
                     new ReportParameter("g027", gua27),
                     new ReportParameter("g028", gua28),
                     new ReportParameter("g029", gua29),
                     new ReportParameter("g0210", gua210),
                     new ReportParameter("g0211", gua211),
                     new ReportParameter("g0212", gua212),
                     new ReportParameter("g0213", gua213),
                     new ReportParameter("g0214", gua214),
                     new ReportParameter("g0215", net_amt22.ToString("C").Replace("$","")),
                     new ReportParameter("g0216", gross_amt22.ToString("C").Replace("$","")),
                     new ReportParameter("g0217", gua215),
                     new ReportParameter("g0218", gua216),
                     new ReportParameter("g0219", gua217)


                     };


            Rptviwer_kelulusan.LocalReport.SetParameters(rptParams);

            Rptviwer_kelulusan.LocalReport.DataSources.Add(rds);

            //Refresh
            Rptviwer_kelulusan.LocalReport.Refresh();
            Warning[] warnings;

            string[] streamids;

            string mimeType;

            string encoding;

            string extension;

            string fname = DateTime.Now.ToString("dd_MM_yyyy");

            string devinfo = "<DeviceInfo><ColorDepth>32</ColorDepth><DpiX>350</DpiX><DpiY>350</DpiY><OutputFormat>PDF</OutputFormat>" +
                   "  <PageWidth>12.20in</PageWidth>" +
                    "  <PageHeight>8.27in</PageHeight>" +
                    "  <MarginTop>0.1in</MarginTop>" +
                    "  <MarginLeft>0.5in</MarginLeft>" +
                     "  <MarginRight>0in</MarginRight>" +
                     "  <MarginBottom>0in</MarginBottom>" +
                   "</DeviceInfo>";

            byte[] bytes = Rptviwer_kelulusan.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);


            Response.Buffer = true;

            Response.Clear();

            Response.ClearHeaders();

            Response.ClearContent();

            Response.ContentType = "application/pdf";

            Response.AddHeader("content-disposition", "attachment; filename=SEMAKAN_MAKLUMAT_PEMBIAYAAN_" + select_app_rt.Rows[0]["app_applcn_no"].ToString() + "." + extension);

            Response.BinaryWrite(bytes);

            //Response.Write("<script>");
            //Response.Write("window.open('', '_newtab');");
            //Response.Write("</script>");
            Response.Flush();

            Response.End();

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila mesti Masukkan Kata-kata',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    protected void clk_bak(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Pelaburan_Anggota/PP_SMPembiayaan_view.aspx");
    }
}